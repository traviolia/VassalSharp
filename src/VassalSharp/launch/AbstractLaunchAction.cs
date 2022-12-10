/*
* $Id$
*
* Copyright (c) 2008-2009 by Joel Uckelman
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

using Microsoft.Extensions.Logging;

using System;
using System.IO;
using System.Collections.Generic;

#if NEVER_DEFINED
//UPGRADE_TODO: The type 'java.util.concurrent.CancellationException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using CancellationException = java.util.concurrent.CancellationException;
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutionException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutionException = java.util.concurrent.ExecutionException;
//UPGRADE_TODO: The type 'java.util.concurrent.Future' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Future = java.util.concurrent.Future;
//UPGRADE_TODO: The type 'java.util.concurrent.TimeUnit' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TimeUnit = java.util.concurrent.TimeUnit;
//UPGRADE_TODO: The type 'java.util.concurrent.TimeoutException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TimeoutException = java.util.concurrent.TimeoutException;
//UPGRADE_TODO: The type 'java.util.concurrent.atomic.AtomicInteger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using AtomicInteger = java.util.concurrent.atomic.AtomicInteger;
//UPGRADE_TODO: The type 'org.apache.commons.codec.digest.DigestUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DigestUtils = org.apache.commons.codec.digest.DigestUtils;
//UPGRADE_TODO: The type 'org.apache.commons.lang.SystemUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SystemUtils = org.apache.commons.lang.SystemUtils;
//UPGRADE_TODO: The type 'org.jdesktop.swingworker.SwingWorker' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SwingWorker = org.jdesktop.swingworker.SwingWorker;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using Info = VassalSharp.Info;
using ExtensionsManager = VassalSharp.build.module.ExtensionsManager;
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using AbstractMetaData = VassalSharp.build.module.metadata.AbstractMetaData;
using MetaDataFactory = VassalSharp.build.module.metadata.MetaDataFactory;
using ModuleMetaData = VassalSharp.build.module.metadata.ModuleMetaData;
using DirectoryConfigurer = VassalSharp.configure.DirectoryConfigurer;
using Prefs = VassalSharp.preferences.Prefs;
using ReadOnlyPrefs = VassalSharp.preferences.ReadOnlyPrefs;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ThrowableUtils = VassalSharp.tools.ThrowableUtils;
using WarningDialog = VassalSharp.tools.WarningDialog;
using FutureUtils = VassalSharp.tools.concurrent.FutureUtils;
//UPGRADE_TODO: The type 'VassalSharp.tools.concurrent.listener.EventListener' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using EventListener = VassalSharp.tools.concurrent.listener.EventListener;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
using ModuleFileFilter = VassalSharp.tools.filechooser.ModuleFileFilter;
using IOUtils = VassalSharp.tools.io.IOUtils;
using ProcessLauncher = VassalSharp.tools.io.ProcessLauncher;
using ProcessWrapper = VassalSharp.tools.io.ProcessWrapper;
using IPCMessage = VassalSharp.tools.ipc.IPCMessage;
using IPCMessenger = VassalSharp.tools.ipc.IPCMessenger;
using SimpleIPCMessage = VassalSharp.tools.ipc.SimpleIPCMessage;
using MemoryUtils = VassalSharp.tools.lang.MemoryUtils;
#else

using ExtensionsManager = VassalSharp.build.module.ExtensionsManager;
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using AbstractMetaData = VassalSharp.build.module.metadata.AbstractMetaData;
using MetaDataFactory = VassalSharp.build.module.metadata.MetaDataFactory;
using ModuleMetaData = VassalSharp.build.module.metadata.ModuleMetaData;
using DirectoryConfigurer = VassalSharp.configure.DirectoryConfigurer;
using Prefs = VassalSharp.preferences.Prefs;
//using ReadOnlyPrefs = VassalSharp.preferences.ReadOnlyPrefs;
using ThrowableUtils = VassalSharp.tools.ThrowableUtils;
//using FutureUtils = VassalSharp.tools.concurrent.FutureUtils;
using VassalSharp.tools.concurrent.listener;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
//using ModuleFileFilter = VassalSharp.tools.filechooser.ModuleFileFilter;
using IOUtils = VassalSharp.tools.io.IOUtils;
//using ProcessLauncher = VassalSharp.tools.io.ProcessLauncher;
//using ProcessWrapper = VassalSharp.tools.io.ProcessWrapper;
using IPCMessage = VassalSharp.tools.ipc.IPCMessage;
using IPCMessenger = VassalSharp.tools.ipc.IPCMessenger;
using SimpleIPCMessage = VassalSharp.tools.ipc.SimpleIPCMessage;
//using MemoryUtils = VassalSharp.tools.lang.MemoryUtils;

#endif
namespace VassalSharp.launch
{
	
	/// <summary> 
	/// The base class for {@link Action}s which launch processes from the
	/// {@link ModuleManagerWindow}.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	[Serializable]
	public abstract class AbstractLaunchAction : SupportClass.ActionSupport
	{
    private const long serialVersionUID = 1L;
		
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.launch.AbstractLaunchAction'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly ILogger logger;
		
		//
		// memory-related constants
		//
		protected internal static int PHYS_MEMORY;
		protected internal const int DEFAULT_INITIAL_HEAP = 256;
		protected internal const int DEFAULT_MAXIMUM_HEAP = 512;
		protected internal const int FAILSAFE_INITIAL_HEAP = 64;
		protected internal const int FAILSAFE_MAXIMUM_HEAP = 128;
		
		protected internal System.Windows.Forms.Form window;
		protected internal System.String entryPoint;
		protected internal LaunchRequest lr;

#if NEVER_DEFINED
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static final Set < File > editing = 
		Collections.synchronizedSet(new HashSet < File >());
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static final Map < File, Integer > using = 
		Collections.synchronizedMap(new HashMap < File, Integer >());
		
		/*
		protected static final List<ObjectOutputStream> children =
		Collections.synchronizedList(new ArrayList<ObjectOutputStream>());*/
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static final List < IPCMessenger > children = 
		Collections.synchronizedList(new ArrayList < IPCMessenger >());
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'nextId '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal static readonly AtomicInteger nextId = new AtomicInteger(1);
		
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		public AbstractLaunchAction(System.String name, System.Windows.Forms.Form window, System.String entryPoint, LaunchRequest lr):base(name)
		{
			
			this.window = window;
			this.entryPoint = entryPoint;
			this.lr = lr;
		}
		
		/// <param name="file">the file to check
		/// </param>
		/// <returns> <code>true</code> iff the file is in use
		/// </returns>
		public static bool isInUse(System.IO.FileInfo file)
		{
			return using_Renamed.containsKey(file);
		}
		
		/// <param name="file">the file to check
		/// </param>
		/// <returns> <code>true</code> iff the file is being edited
		/// </returns>
		public static bool isEditing(System.IO.FileInfo file)
		{
			return editing.contains(file);
		}
		
		/// <summary> Ask child processes to close.
		/// 
		/// </summary>
		/// <returns> <code>true</code> iff all child processes will terminate
		/// </returns>
		public static bool shutDown()
		{
			ModuleManagerWindow.Instance.SendToBack();
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < Future < IPCMessage >> futures = 
			new ArrayList < Future < IPCMessage >>();
			
			// must synchronize when iterating over a Collections.synchronizedList()
			lock (children)
			{

				for(IPCMessenger ipc: children)
				{
					try
					{
						futures.add(ipc.send(new Launcher.CloseRequest()));
					}
					catch (System.IO.IOException e)
					{
						// FIXME
						SupportClass.WriteStackTrace(e, Console.Error);
					}
				}
			}
			
			// FIXME: not working!
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Future < IPCMessage > f: futures)
			{
				try
				{
					if (f.get_Renamed() is Launcher.CloseReject)
					{
						System.Console.Out.WriteLine("rejected!");
						return false;
					}
				}
				catch (ExecutionException e)
				{
					e.printStackTrace();
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
				}
			}
			
			return true;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			ModuleManagerWindow.Instance.WaitCursor = true;
			getLaunchTask().execute();
		}
		
		protected internal abstract LaunchTask getLaunchTask();
		
		protected internal virtual System.IO.FileInfo promptForFile()
		{
			// prompt the user to pick a file
			//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FileChooser fc = FileChooser.createFileChooser(window, (DirectoryConfigurer) Prefs.GlobalPrefs.getOption(Prefs.MODULES_DIR_KEY));
			
			addFileFilters(fc);
			
			// loop until cancellation or we get an existing file
			if (fc.showOpenDialog() == FileChooser.APPROVE_OPTION)
			{
				lr.module = fc.SelectedFile;
				if (lr.module != null)
				{
					if (lr.module.exists())
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'metadata '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						AbstractMetaData metadata = MetaDataFactory.buildMetaData(lr.module);
						if (metadata == null || !(metadata is ModuleMetaData))
						{
							ErrorDialog.show("Error.invalid_vassal_module", lr.module.FullName);
							logger.error("-- Load of {} failed: Not a Vassal module", lr.module.FullName);
							lr.module = null;
						}
					}
					else
					{
						lr.module = null;
					}
					// FIXME: do something to warn about nonexistant file
					//        FileNotFoundDialog.warning(window, lr.module);
				}
			}
			
			return lr.module;
		}
		
		protected internal virtual void  addFileFilters(FileChooser fc)
		{
			fc.addChoosableFileFilter(new ModuleFileFilter());
		}
#endif

		protected internal class LaunchTask // : SwingWorker
		{
			public LaunchTask(AbstractLaunchAction enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(AbstractLaunchAction launchAction)
			{
				this.launchAction = launchAction;
				id = VassalSharp.launch.AbstractLaunchAction.nextId.getAndIncrement();
				lr = new LaunchRequest(this.launchAction.lr);
			}

            public AbstractLaunchAction launchAction { get; private set; }

			//UPGRADE_NOTE: The initialization of  'id' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			protected internal int id;
			
			// lr might be modified before the task is over, keep a local copy
			//UPGRADE_NOTE: Final was removed from the declaration of 'lr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: The initialization of  'lr' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			protected internal LaunchRequest lr;
			
			protected internal System.Net.Sockets.TcpListener serverSocket = null;
			protected internal System.Net.Sockets.TcpClient clientSocket = null;
			
			protected internal IPCMessenger ipc = null;

			public virtual void doInBackground()
			{
				// FIXME: this should be in an abstract method and farmed out to subclasses
				// send some basic information to the log
				if (lr.module != null)
				{
					VassalSharp.launch.AbstractLaunchAction.logger.LogInformation("Loading module file {}", lr.module.FullName);
					
					// slice tiles for module
					System.String aname = lr.module.FullName;
					ModuleMetaData meta = new ModuleMetaData(new ModuleMetaData.ZipFile(aname));
					//System.String hstr = DigestUtils.shaHex(meta.Name + "_" + meta.Version);
					
					////UPGRADE_NOTE: Final was removed from the declaration of 'cdir '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//System.IO.FileInfo cdir = new System.IO.FileInfo(Info.ConfDir.FullName + "\\" + "tiles/" + hstr);
					
					////UPGRADE_NOTE: Final was removed from the declaration of 'th '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//TilingHandler th = new TilingHandler(aname, cdir, new System.Drawing.Size(256, 256), VassalSharp.launch.AbstractLaunchAction.PHYS_MEMORY, VassalSharp.launch.AbstractLaunchAction.nextId.getAndIncrement());
					
					//try
					//{
					//	th.sliceTiles();
					//}
					//catch (CancellationException e)
					//{
					//	cancel(true);
					//	//UPGRADE_TODO: The 'System.Void' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
					//	return null;
					//}
					
					// slice tiles for extensions
					//UPGRADE_NOTE: Final was removed from the declaration of 'mgr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					ExtensionsManager mgr = new ExtensionsManager(lr.module);
	
					foreach (FileInfo ext in mgr.getActiveExtensions())
					{
						////UPGRADE_NOTE: Final was removed from the declaration of 'eth '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						//TilingHandler eth = new TilingHandler(ext.FullName, cdir, new System.Drawing.Size(256, 256), VassalSharp.launch.AbstractLaunchAction.PHYS_MEMORY, VassalSharp.launch.AbstractLaunchAction.nextId.getAndIncrement());
						
						//try
						//{
						//	eth.sliceTiles();
						//}
						//catch (CancellationException e)
						//{
						//	cancel(true);
						//	//UPGRADE_TODO: The 'System.Void' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
						//	return null;
						//}
					}
				}
				
				if (lr.game != null)
				{
					VassalSharp.launch.AbstractLaunchAction.logger.LogInformation("Loading game file {}", lr.game.FullName);
				}
				
				if (lr.importFile != null)
				{
					VassalSharp.launch.AbstractLaunchAction.logger.LogInformation("Importing module file {}", lr.importFile.FullName);
				}
				// end FIXME
				
				// set default heap sizes
				int initialHeap = VassalSharp.launch.AbstractLaunchAction.DEFAULT_INITIAL_HEAP;
				int maximumHeap = VassalSharp.launch.AbstractLaunchAction.DEFAULT_MAXIMUM_HEAP;
				
				System.String moduleName = null;
				
				// FIXME: this should be in an abstract method and farmed out to subclasses,
				// rather than a case structure for each kind of thing which may be loaded.
				// find module-specific heap settings, if any
				if (lr.module != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					AbstractMetaData data = MetaDataFactory.buildMetaData(lr.module);
					
					if (data == null)
					{
						//ErrorDialog.show("Error.invalid_vassal_file", lr.module.FullName);
						//ModuleManagerWindow.Instance.WaitCursor = false;
						return;
					}
					
					if (data is ModuleMetaData)
					{
						moduleName = ((ModuleMetaData) data).Name;
						
						// log the module name
						VassalSharp.launch.AbstractLaunchAction.logger.LogInformation("Loading module {}", moduleName);
						
						//// read module prefs
						//ReadOnlyPrefs p = new ReadOnlyPrefs(moduleName);
						
						// read initial heap size
						initialHeap = getHeapSize(p, GlobalOptions.INITIAL_HEAP, VassalSharp.launch.AbstractLaunchAction.DEFAULT_INITIAL_HEAP);
						
						// read maximum heap size
						maximumHeap = getHeapSize(p, GlobalOptions.MAXIMUM_HEAP, VassalSharp.launch.AbstractLaunchAction.DEFAULT_MAXIMUM_HEAP);
					}
				}
				else if (lr.importFile != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Prefs p = Prefs.GlobalPrefs;
					
					// read initial heap size
					initialHeap = getHeapSize(p, GlobalOptions.INITIAL_HEAP, VassalSharp.launch.AbstractLaunchAction.DEFAULT_INITIAL_HEAP);
					
					// read maximum heap size
					maximumHeap = getHeapSize(p, GlobalOptions.MAXIMUM_HEAP, VassalSharp.launch.AbstractLaunchAction.DEFAULT_MAXIMUM_HEAP);
				}
				// end FIXME
				
				//
				// Heap size sanity checks: fall back to failsafe heap sizes in
				// case the given initial or maximum heap is not usable.
				//
				
				// FIXME: The heap size messages are too nonspecific. They should
				// differientiate between loading a module and importing a module,
				// since the heap sizes are set in different places for those two
				// actions.
				// maximum heap must fit in physical RAM
				if (maximumHeap > VassalSharp.launch.AbstractLaunchAction.PHYS_MEMORY)
				{
					initialHeap = VassalSharp.launch.AbstractLaunchAction.FAILSAFE_INITIAL_HEAP;
					maximumHeap = VassalSharp.launch.AbstractLaunchAction.FAILSAFE_MAXIMUM_HEAP;
					
					//FutureUtils.wait(WarningDialog.show("Warning.maximum_heap_too_large", VassalSharp.launch.AbstractLaunchAction.FAILSAFE_MAXIMUM_HEAP));
				}
				// maximum heap must be at least the failsafe size
				else if (maximumHeap < VassalSharp.launch.AbstractLaunchAction.FAILSAFE_MAXIMUM_HEAP)
				{
					initialHeap = VassalSharp.launch.AbstractLaunchAction.FAILSAFE_INITIAL_HEAP;
					maximumHeap = VassalSharp.launch.AbstractLaunchAction.FAILSAFE_MAXIMUM_HEAP;
					
					//FutureUtils.wait(WarningDialog.show("Warning.maximum_heap_too_small", VassalSharp.launch.AbstractLaunchAction.FAILSAFE_MAXIMUM_HEAP));
				}
				// initial heap must be at least the failsafe size
				else if (initialHeap < VassalSharp.launch.AbstractLaunchAction.FAILSAFE_INITIAL_HEAP)
				{
					initialHeap = VassalSharp.launch.AbstractLaunchAction.FAILSAFE_INITIAL_HEAP;
					maximumHeap = VassalSharp.launch.AbstractLaunchAction.FAILSAFE_MAXIMUM_HEAP;
					
					//FutureUtils.wait(WarningDialog.show("Warning.initial_heap_too_small", VassalSharp.launch.AbstractLaunchAction.FAILSAFE_INITIAL_HEAP));
				}
				// initial heap must be less than or equal to maximum heap
				else if (initialHeap > maximumHeap)
				{
					initialHeap = VassalSharp.launch.AbstractLaunchAction.FAILSAFE_INITIAL_HEAP;
					maximumHeap = VassalSharp.launch.AbstractLaunchAction.FAILSAFE_MAXIMUM_HEAP;
					
					//FutureUtils.wait(WarningDialog.show("Warning.initial_heap_too_large", VassalSharp.launch.AbstractLaunchAction.FAILSAFE_INITIAL_HEAP));
				}
				
				// create a socket for communicating which the child process
				//UPGRADE_NOTE: Final was removed from the declaration of 'lo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.net.InetAddress.getByName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Net.IPAddress lo = System.Net.Dns.Resolve(null).AddressList[0];
				System.Net.Sockets.TcpListener temp_tcpListener;
				temp_tcpListener = new System.Net.Sockets.TcpListener(new System.Net.IPEndPoint(lo, 0));
				temp_tcpListener.Start();
				serverSocket = temp_tcpListener;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'port '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int port = ((System.Net.IPEndPoint) serverSocket.LocalEndpoint).Port;
				
				// build the argument list

				List < String > al = new List < String >();
					al.Add(""); // Info.javaBinPath);
				al.Add(""); // reserved for initial heap
				al.Add(""); // reserved for maximum heap
				al.Add("-DVassalSharp.id=" + id); // instance id
				al.Add("-DVassalSharp.port=" + port); // MM socket port
				
				// pass on the user's home, if it's set
				//UPGRADE_TODO: Method 'java.lang.System.getProperty' was converted to 'System.Environment.GetEnvironmentVariable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangSystemgetProperty_javalangString'"
				System.String userHome = System.Environment.GetEnvironmentVariable("userprofile");
				if (userHome != null)
					al.Add("-Duser.home=" + userHome);
				
				// pass on the user's working dir, if it's set
				//UPGRADE_TODO: Method 'java.lang.System.getProperty' was converted to 'System.Environment.CurrentDirectory' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangSystemgetProperty_javalangString'"
				System.String userDir = System.Environment.CurrentDirectory;
				if (userDir != null)
					al.Add("-Duser.dir=" + userDir);

					// pass on VASSAL's home dir, if it's set
					//UPGRADE_ISSUE: Method 'java.lang.System.getProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
					System.String vHome = null; // System_Renamed.getProperty("VassalSharp.home");
				if (vHome != null)
					al.Add("-DVassalSharp.home=" + vHome);
				
				//// set the classpath
				//al.Add("-cp");
				////UPGRADE_ISSUE: Method 'java.lang.System.getProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
				//al.Add(System_Renamed.getProperty("java.class.path"));
				
				// Disable the 2D to Direct3D pipeline?
				System.Boolean disableD3d = (System.Boolean) Prefs.GlobalPrefs.getValue(Prefs.DISABLE_D3D);
				if (true.Equals(disableD3d))
				{
					al.Add("-Dsun.java2d.d3d=false");
				}
				
				al.Add(launchAction.entryPoint);
				
				al.AddRange(lr.toArgs());
				
				System.String[] args = al.ToArray();
				
				// try to start a child process with the given heap sizes
				args[1] = "-Xms" + initialHeap + "M";
				args[2] = "-Xmx" + maximumHeap + "M";
				
				
				ProcessWrapper proc = new ProcessLauncher().launch(args);
				
				try
				{
					proc.future.get_Renamed(1000L, TimeUnit.MILLISECONDS);
				}
				catch (CancellationException e)
				{
					cancel(true);
					//UPGRADE_TODO: The 'System.Void' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
					return null;
				}
				catch (ExecutionException e)
				{
					VassalSharp.launch.AbstractLaunchAction.logger.LogError("", e);
				}
				catch (TimeoutException e)
				{
					// this is expected
				}
				
				// if launch failed, use conservative heap sizes
				if (proc.future.isDone())
				{
					args[1] = "-Xms" + VassalSharp.launch.AbstractLaunchAction.FAILSAFE_INITIAL_HEAP + "M";
					args[2] = "-Xmx" + VassalSharp.launch.AbstractLaunchAction.FAILSAFE_MAXIMUM_HEAP + "M";
					proc = new ProcessLauncher().launch(args);
					
					try
					{
						proc.future.get_Renamed(1000L, TimeUnit.MILLISECONDS);
					}
					catch (ExecutionException e)
					{
						VassalSharp.launch.AbstractLaunchAction.logger.error("", e);
					}
					catch (TimeoutException e)
					{
						// this is expected
					}
					
					if (proc.future.isDone())
					{
						throw new System.IO.IOException("failed to start child process");
					}
					else
					{
						FutureUtils.wait(WarningDialog.show("Warning.maximum_heap_too_large", VassalSharp.launch.AbstractLaunchAction.FAILSAFE_MAXIMUM_HEAP));
					}
				}
				
				clientSocket = serverSocket.AcceptTcpClient();
				ipc = new IPCMessenger(clientSocket);
				
				ipc.addEventListener(typeof(NotifyOpenModuleOk), new NotifyOpenModuleOkListener());
				
				ipc.addEventListener(typeof(NotifyNewModuleOk), new NotifyNewModuleOkListener());
				
				ipc.addEventListener(typeof(NotifyImportModuleOk), new NotifyImportModuleOkListener());
				
				ipc.addEventListener(typeof(NotifyOpenModuleFailed), new NotifyOpenModuleFailedListener());
				
				ipc.addEventListener(typeof(NotifySaveFileOk), new NotifySaveFileOkListener());
				
				ipc.start();
				
				children.add(ipc);
				
				// block until the process ends
				try
				{
					proc.future.get_Renamed();
				}
				catch (ExecutionException e)
				{
					VassalSharp.launch.AbstractLaunchAction.logger.error("", e);
				}
				
			}
			
			//protected internal virtual int getHeapSize(ReadOnlyPrefs p, System.String key, int defaultHeap)
			//{
			//	// read heap size, if it exists
			//	//UPGRADE_NOTE: Final was removed from the declaration of 'val '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//	System.String val = p.getStoredValue(key);
			//	if (val == null)
			//		return defaultHeap;
				
			//	try
			//	{
			//		return System.Int32.Parse(val);
			//	}
			//	catch (System.FormatException ex)
			//	{
			//		return - 1;
			//	}
			//}
			
			//protected internal virtual int getHeapSize(Prefs p, System.String key, int defaultHeap)
			//{
			//	// read heap size, if it exists
			//	//UPGRADE_NOTE: Final was removed from the declaration of 'val '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//	System.Object val = p.getValue(key);
			//	if (val == null)
			//		return defaultHeap;
				
			//	try
			//	{
			//		//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.ToString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//		return System.Int32.Parse(val.ToString());
			//	}
			//	catch (System.FormatException ex)
			//	{
			//		return - 1;
			//	}
			//}

			protected internal virtual void  done()
			{
				try
				{
					get_Renamed();
				}
				catch (CancellationException e)
				{
					// this means that loading was cancelled
					ModuleManagerWindow.Instance.WaitCursor = false;
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
					ErrorDialog.bug(e);
				}
				catch (ExecutionException e)
				{
					// determine what kind of exception occurred
					//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
					System.Exception c = e.getCause();
					if (c is System.IO.IOException)
					{
						ErrorDialog.showDetails(e, ThrowableUtils.getStackTrace(e), "Error.socket_error");
					}
					else
					{
						ErrorDialog.bug(e);
					}
				}
				finally
				{
					IOUtils.closeQuietly(clientSocket);
					IOUtils.closeQuietly(serverSocket);
					children.remove(ipc);
				}
			}
		}

#if NEVER_DEFINED
		//
		// Commands
		//
		
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		[Serializable]
		public abstract class LaunchRequestMessage:SimpleIPCMessage
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'lr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			protected internal LaunchRequest lr;
			
			public LaunchRequestMessage(LaunchRequest lr)
			{
				this.lr = lr;
			}
		}
		
		[Serializable]
		public class NotifyOpenModuleOk:LaunchRequestMessage
		{
			private const long serialVersionUID = 1L;
			
			public NotifyOpenModuleOk(LaunchRequest lr):base(lr)
			{
			}
		}
		
		[Serializable]
		public class NotifyNewModuleOk:LaunchRequestMessage
		{
			private const long serialVersionUID = 1L;
			
			public NotifyNewModuleOk(LaunchRequest lr):base(lr)
			{
			}
		}
		
		[Serializable]
		public class NotifyImportModuleOk:LaunchRequestMessage
		{
			private const long serialVersionUID = 1L;
			
			public NotifyImportModuleOk(LaunchRequest lr):base(lr)
			{
			}
		}
		
		[Serializable]
		public class NotifyOpenModuleFailed:LaunchRequestMessage
		{
			private const long serialVersionUID = 1L;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'thrown '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			public System.Exception thrown;
			
			//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			public NotifyOpenModuleFailed(LaunchRequest lr, System.Exception thrown):base(lr)
			{
				this.thrown = thrown;
			}
		}
		
		[Serializable]
		public class NotifySaveFileOk:SimpleIPCMessage
		{
			private const long serialVersionUID = 1L;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'file '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			public System.IO.FileInfo file;
			
			public NotifySaveFileOk(System.IO.FileInfo file)
			{
				this.file = file;
			}
		}
		
		//
		// Listeners
		//
		
		protected internal class NotifyOpenModuleOkListener : EventListener
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassRunnable : IThreadRunnable
			{
				public AnonymousClassRunnable(VassalSharp.launch.AbstractLaunchAction.NotifyOpenModuleOk msg, NotifyOpenModuleOkListener enclosingInstance)
				{
					InitBlock(msg, enclosingInstance);
				}
				private void  InitBlock(VassalSharp.launch.AbstractLaunchAction.NotifyOpenModuleOk msg, NotifyOpenModuleOkListener enclosingInstance)
				{
					this.msg = msg;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable msg was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.launch.AbstractLaunchAction.NotifyOpenModuleOk msg;
				private NotifyOpenModuleOkListener enclosingInstance;
				public NotifyOpenModuleOkListener Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  Run()
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'mmw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					ModuleManagerWindow mmw = ModuleManagerWindow.Instance;
					mmw.addModule(msg.lr.module);
					mmw.WaitCursor = false;
				}
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< NotifyOpenModuleOk >
			public virtual void  receive(System.Object src, NotifyOpenModuleOk msg)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.invokeLater(new AnonymousClassRunnable(msg, this));
			}
		}
		
		protected internal class NotifyNewModuleOkListener : EventListener
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassRunnable : IThreadRunnable
			{
				public AnonymousClassRunnable(NotifyNewModuleOkListener enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(NotifyNewModuleOkListener enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private NotifyNewModuleOkListener enclosingInstance;
				public NotifyNewModuleOkListener Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  Run()
				{
					ModuleManagerWindow.Instance.WaitCursor = false;
				}
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< NotifyNewModuleOk >
			public virtual void  receive(System.Object src, NotifyNewModuleOk msg)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.invokeLater(new AnonymousClassRunnable(this));
			}
		}
		
		protected internal class NotifyImportModuleOkListener : EventListener
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassRunnable : IThreadRunnable
			{
				public AnonymousClassRunnable(NotifyImportModuleOkListener enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(NotifyImportModuleOkListener enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private NotifyImportModuleOkListener enclosingInstance;
				public NotifyImportModuleOkListener Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  Run()
				{
					ModuleManagerWindow.Instance.WaitCursor = false;
				}
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< NotifyImportModuleOk >
			public virtual void  receive(System.Object src, NotifyImportModuleOk msg)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.invokeLater(new AnonymousClassRunnable(this));
			}
		}
		
		protected internal class NotifyOpenModuleFailedListener : EventListener
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassRunnable : IThreadRunnable
			{
				public AnonymousClassRunnable(NotifyOpenModuleFailedListener enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(NotifyOpenModuleFailedListener enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private NotifyOpenModuleFailedListener enclosingInstance;
				public NotifyOpenModuleFailedListener Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  Run()
				{
					ModuleManagerWindow.Instance.WaitCursor = false;
				}
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< NotifyOpenModuleFailed >
			public virtual void  receive(System.Object src, NotifyOpenModuleFailed msg)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.invokeLater(new AnonymousClassRunnable(this));
				
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				ErrorDialog.showDetails(msg.thrown, ThrowableUtils.getStackTrace(msg.thrown), "Error.module_load_failed", msg.thrown.Message);
			}
		}
		
		protected internal class NotifySaveFileOkListener : EventListener
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassRunnable : IThreadRunnable
			{
				public AnonymousClassRunnable(VassalSharp.launch.AbstractLaunchAction.NotifySaveFileOk msg, NotifySaveFileOkListener enclosingInstance)
				{
					InitBlock(msg, enclosingInstance);
				}
				private void  InitBlock(VassalSharp.launch.AbstractLaunchAction.NotifySaveFileOk msg, NotifySaveFileOkListener enclosingInstance)
				{
					this.msg = msg;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable msg was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.launch.AbstractLaunchAction.NotifySaveFileOk msg;
				private NotifySaveFileOkListener enclosingInstance;
				public NotifySaveFileOkListener Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  Run()
				{
					ModuleManagerWindow.Instance.update(msg.file);
				}
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< NotifySaveFileOk >
			public virtual void  receive(System.Object rc, NotifySaveFileOk msg)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.invokeLater(new AnonymousClassRunnable(msg, this));
			}
		}
		static AbstractLaunchAction()
		{
			logger = LoggerFactory.getLogger(typeof(AbstractLaunchAction));
			{
				// Determine how much physical RAM this machine has
				// Assume 4GB if we can't determine how much RAM there is
				//UPGRADE_NOTE: Final was removed from the declaration of 'physMemoryBytes '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				long physMemoryBytes = MemoryUtils.PhysicalMemory;
				PHYS_MEMORY = physMemoryBytes < 0?4096:(int) (physMemoryBytes >> 20);
			}
		}
    #endif
	}
}