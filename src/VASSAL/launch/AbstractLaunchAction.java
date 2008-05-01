/*
 * $Id$
 *
 * Copyright (c) 2008 by Joel Uckelman 
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Library General Public
 * License (LGPL) as published by the Free Software Foundation.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Library General Public License for more details.
 *
 * You should have received a copy of the GNU Library General Public
 * License along with this library; if not, copies are available
 * at http://www.opensource.org.
 */

package VASSAL.launch;

import java.awt.Cursor;
import java.awt.Window;
import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.File;
import java.io.InputStream;
import java.io.IOException;
import java.io.OutputStream;
import java.net.ServerSocket;
import java.net.Socket;
import java.awt.event.ActionEvent;
import java.util.ArrayList;
import java.util.Collections;
import java.util.HashSet;
import java.util.HashMap;
import java.util.List;
import java.util.Map;
import java.util.Set;
import java.util.concurrent.CancellationException;
import java.util.concurrent.ExecutionException;
import javax.swing.AbstractAction;

// FIXME: switch back to javax.swing.SwingWorker on move to Java 1.6
//import javax.swing.SwingWorker;
import org.jdesktop.swingworker.SwingWorker;

import VASSAL.Info;
import VASSAL.build.module.GlobalOptions;
import VASSAL.configure.DirectoryConfigurer;
import VASSAL.preferences.Prefs;
import VASSAL.tools.ErrorLog;
import VASSAL.tools.FileChooser;
import VASSAL.tools.IOUtils;

public abstract class AbstractLaunchAction extends AbstractAction {
  private static final long serialVersionUID = 1L;
  
  public static final int DEFAULT_INITIAL_HEAP = 256;
  public static final int DEFAULT_MAXIMUM_HEAP = 512;

  protected final Window window; 
  protected File module;
  protected final String entryPoint;
  protected final String[] args;

  protected static final Set<File> editing =
    Collections.synchronizedSet(new HashSet<File>());
  protected static final Map<File,Integer> using =
    Collections.synchronizedMap(new HashMap<File,Integer>());

  protected static final List<CommandClient> children =
    Collections.synchronizedList(new ArrayList<CommandClient>());

  public AbstractLaunchAction(String name, Window window, String entryPoint,
                              String[] args, File module) {
    super(name);
    this.window = window;
    this.entryPoint = entryPoint;
    this.args = args;
    this.module = module;
  }
  
  public static boolean isInUse(File f) {
    return using.containsKey(f);
  }
  
  public static boolean isEditing(File f) {
    return editing.contains(f);
  }

  public static boolean shutDown() {
    for (CommandClient child : children) {
      try {
        if ("NOK".equals(child.request("REQUEST_CLOSE"))) return false;
      }
      catch (IOException e) {
        ErrorLog.warn(e);
      }
    }
    return true;
  }

  public void actionPerformed(ActionEvent e) {
    window.setCursor(Cursor.getPredefinedCursor(Cursor.WAIT_CURSOR));
    getLaunchTask().execute();
  }

  protected abstract LaunchTask getLaunchTask(); 

  protected File promptForModule() {
    // prompt the user to pick a module
    final FileChooser fc = FileChooser.createFileChooser(window,
      (DirectoryConfigurer)
        Prefs.getGlobalPrefs().getOption(Prefs.MODULES_DIR_KEY));

    if (fc.showOpenDialog() == FileChooser.APPROVE_OPTION) {
      module = fc.getSelectedFile();
      if (module != null && !module.exists()) module = null;
    }
    
    return module;
  }

  protected class LaunchTask extends SwingWorker<Void,Void> {
    // module might be reassigned before the task is over, keep a local copy
    protected final File mod = AbstractLaunchAction.this.module; 

    protected ServerSocket serverSocket;
    protected Socket clientSocket;
  
    protected CommandClient cmdC;
    protected CommandServer cmdS;

    @Override
    public Void doInBackground() throws Exception {
      // get heap setttings
      int initialHeap; 
      try {
        initialHeap = Integer.parseInt(Prefs.getGlobalPrefs()
          .getStoredValue(GlobalOptions.INITIAL_HEAP));
      }
      catch (NumberFormatException ex) {
        // don't show warning dialog, since this isn't fatal,
        // or even abnormal, e.g., in the case where this is
        // a new copy of VASSAL
        ex.printStackTrace();
        initialHeap = DEFAULT_INITIAL_HEAP;
      }

      int maximumHeap;
      try {
        maximumHeap = Integer.parseInt(Prefs.getGlobalPrefs()
          .getStoredValue(GlobalOptions.MAXIMUM_HEAP));
      }
      catch (NumberFormatException ex) {
        // don't show warning dialog, since this isn't fatal,
        // or even abnormal, e.g., in the case where this is
        // a new copy of VASSAL
        ex.printStackTrace();
        maximumHeap = DEFAULT_MAXIMUM_HEAP;
      }

      // create a socket for communicating which the child process
      final ServerSocket serverSocket = new ServerSocket(0);
      cmdS = new LaunchCommandServer(serverSocket);
      new Thread(cmdS).start();

      // build the child process
      final String[] pa =
        new String[6 + args.length + (mod == null ? 0 : 1)];
      pa[0] = "java";
      pa[1] = "-Xms" + initialHeap + "M";
      pa[2] = "-Xmx" + maximumHeap + "M";
      pa[3] = "-cp"; 
      pa[4] = System.getProperty("java.class.path");
      pa[5] = entryPoint; 
      System.arraycopy(args, 0, pa, 6, args.length);
      if (mod != null) pa[pa.length-1] = mod.getPath();

      final ProcessBuilder pb = new ProcessBuilder(pa);
      pb.directory(Info.getBinDir());

      final Process p = pb.start();

      // pump child's stderr to our own stderr
      new Thread(new StreamPump(p.getErrorStream(), System.err)).start();

      // write the port for this socket to child's stdin and close
      DataOutputStream dout = null;
      try {
        dout = new DataOutputStream(p.getOutputStream());
        dout.writeInt(serverSocket.getLocalPort());
      }
      finally {
        IOUtils.closeQuietly(dout);
      }

      // read the port for the child's socket from its stdout
      final DataInputStream din = new DataInputStream(p.getInputStream());
      final int childPort = din.readInt();

      // pump child's stdout to our own stdout
      new Thread(new StreamPump(p.getInputStream(), System.out)).start();

      // create the client for the child's socket
      cmdC = new CommandClient(new Socket((String) null, childPort));
      children.add(cmdC);

      // block until the process ends
      p.waitFor();
      return null;
    }

    @Override
    protected void done() {
      try {
        get();
      }
      catch (CancellationException e) {
      }
      catch (InterruptedException e) {
        ErrorLog.warn(e);
      }
      catch (ExecutionException e) {
        ErrorLog.warn(e);
      }
      finally {
        IOUtils.closeQuietly(clientSocket);
        IOUtils.closeQuietly(serverSocket);
        children.remove(cmdC);
      }    
    }
  }

  protected class LaunchCommandServer extends CommandServer {
    public LaunchCommandServer(ServerSocket serverSocket) {
      super(serverSocket);
    }

    @Override
    protected Object reply(Object cmd) {
      if ("NOTIFY_OPEN".equals(cmd)) {
        window.setCursor(Cursor.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
        return "OK";
      }
      else {
        return "UNRECOGNIZED_COMMAND";
      }
    }
  }

  private static class StreamPump implements Runnable {
    private final InputStream in;
    private final OutputStream out;

    public StreamPump(InputStream in, OutputStream out) {
      this.in = in;
      this.out = out;
    }

    public void run() {
      try {
        IOUtils.copy(in, out);
      }
      catch (IOException e) {
        ErrorLog.warn(e);
      }
    }
  }
}
