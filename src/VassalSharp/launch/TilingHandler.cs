/*
* $Id$
*
* Copyright (c) 2010, 2011 by Joel Uckelman
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
using System;
namespace VassalSharp.launch
{
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static VassalSharp.tools.image.tilecache.ZipFileImageTilerState.STARTING_IMAGE;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static VassalSharp.tools.image.tilecache.ZipFileImageTilerState.TILE_WRITTEN;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static VassalSharp.tools.image.tilecache.ZipFileImageTilerState.TILING_FINISHED;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.awt.Dimension;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.awt.event.ActionEvent;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.awt.event.ActionListener;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.DataInputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.File;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.IOException;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.InputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.PrintWriter;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.net.InetAddress;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.net.ServerSocket;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.net.Socket;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.ArrayList;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.Arrays;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.List;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.Set;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.concurrent.CancellationException;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.concurrent.ExecutionException;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.apache.commons.io.FileUtils;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.apache.commons.lang.SystemUtils;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.slf4j.Logger;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.slf4j.LoggerFactory;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.Info;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.DataArchive;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.image.ImageUtils;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.image.tilecache.ImageTileDiskCache;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.image.tilecache.TileUtils;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.io.FileArchive;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.io.FileStore;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.io.IOUtils;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.io.InputOutputStreamPump;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.io.InputStreamPump;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.io.ProcessLauncher;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.io.ProcessWrapper;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.lang.Pair;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.swing.EDT;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.swing.ProgressDialog;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.swing.Progressor;
	
	/// <summary> A launcher for the process which tiles large images.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class TilingHandler
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassProgressor' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassProgressor : Progressor
		{
			public AnonymousClassProgressor(ProgressDialog pd, TilingHandler enclosingInstance)
			{
				InitBlock(pd, enclosingInstance);
			}
			private void  InitBlock(ProgressDialog pd, TilingHandler enclosingInstance)
			{
				this.pd = pd;
				this.enclosingInstance = enclosingInstance;
				Enclosing_Instance.pd.setProgress((100 * prog.second) / max);
			}
			//UPGRADE_NOTE: Final variable pd was copied into class AnonymousClassProgressor. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private ProgressDialog pd;
			private TilingHandler enclosingInstance;
			public TilingHandler Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			protected
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			void run(Pair < Integer, Integer > prog)
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(ProgressDialog pd, ProcessWrapper proc, TilingHandler enclosingInstance)
			{
				InitBlock(pd, proc, enclosingInstance);
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener
			{
				public AnonymousClassActionListener(ProgressDialog pd, ProcessWrapper proc, AnonymousClassRunnable enclosingInstance)
				{
					InitBlock(pd, proc, enclosingInstance);
				}
				private void  InitBlock(ProgressDialog pd, ProcessWrapper proc, AnonymousClassRunnable enclosingInstance)
				{
					this.pd = pd;
					this.proc = proc;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable pd was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private ProgressDialog pd;
				//UPGRADE_NOTE: Final variable proc was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private ProcessWrapper proc;
				private AnonymousClassRunnable enclosingInstance;
				public AnonymousClassRunnable Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(ActionEvent e)
				{
					Enclosing_Instance.Enclosing_Instance.pd.setVisible(false);
					Enclosing_Instance.Enclosing_Instance.proc.future.cancel(true);
				}
			}
			private void  InitBlock(ProgressDialog pd, ProcessWrapper proc, TilingHandler enclosingInstance)
			{
				this.pd = pd;
				this.proc = proc;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable pd was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private ProgressDialog pd;
			//UPGRADE_NOTE: Final variable proc was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private ProcessWrapper proc;
			private TilingHandler enclosingInstance;
			public TilingHandler Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				Enclosing_Instance.pd.addActionListener(new AnonymousClassActionListener(pd, proc, this));
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable1 : IThreadRunnable
		{
			public AnonymousClassRunnable1(ProgressDialog pd, System.String ipath, TilingHandler enclosingInstance)
			{
				InitBlock(pd, ipath, enclosingInstance);
			}
			private void  InitBlock(ProgressDialog pd, System.String ipath, TilingHandler enclosingInstance)
			{
				this.pd = pd;
				this.ipath = ipath;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable pd was copied into class AnonymousClassRunnable1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private ProgressDialog pd;
			//UPGRADE_NOTE: Final variable ipath was copied into class AnonymousClassRunnable1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.String ipath;
			private TilingHandler enclosingInstance;
			public TilingHandler Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				Enclosing_Instance.pd.setLabel("Tiling " + ipath);
				if (!Enclosing_Instance.pd.isVisible())
					Enclosing_Instance.pd.setVisible(true);
			}
		}
		private void  InitBlock()
		{
			// build a list of all multi-tile images and count tiles
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Set < String > images = archive.getImageNameSet();
			
			int maxpix = 0; // number of pixels in the largest image
			int tcount = 0; // tile count
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'fa '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FileArchive fa = archive.getArchive();
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String iname: images)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'ipath '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String ipath = DataArchive.IMAGE_DIR + iname;
				
				// skip images with fresh tiles
				if (isFresh(fa, tcache, ipath))
					continue;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'idim '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Dimension idim;
				try
				{
					idim = getImageSize(archive, ipath);
				}
				catch (IOException e)
				{
					// skip images we can't read
					failed.add(Pair.of(ipath, e));
					continue;
				}
				
				// count the tiles at all sizes if we have more than one tile at 1:1
				//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int t = TileUtils.tileCountAtScale(idim, tdim, 1) > 1?TileUtils.tileCount(idim, tdim):0;
				
				if (t == 0)
					continue;
				
				tcount += t;
				multi.add(ipath);
				
				// check whether this image has the most pixels
				if (idim.width * idim.height > maxpix)
				{
					maxpix = idim.width * idim.height;
				}
			}
			
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Pair < Integer, Integer >(tcount, maxpix);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'lo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			InetAddress lo = InetAddress.getByName(null);
			//UPGRADE_NOTE: Final was removed from the declaration of 'ssock '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ServerSocket ssock = new ServerSocket(0, 0, lo);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'port '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int port = ssock.getLocalPort();
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < String > args = new ArrayList < String >();
			//UPGRADE_ISSUE: Method 'java.lang.System.getProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
			//UPGRADE_TODO: Method 'java.lang.System.getProperty' was converted to 'System.Environment.GetEnvironmentVariable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangSystemgetProperty_javalangString'"
			args.addAll(Arrays.asList(new System.String[]{Info.javaBinPath, "-classpath", System_Renamed.getProperty("java.class.path"), "-Xmx" + maxheap + "M", "-DVassalSharp.id=" + pid, "-Duser.home=" + System.Environment.GetEnvironmentVariable("userprofile"), "-DVassalSharp.port=" + port, "VassalSharp.tools.image.tilecache.ZipFileImageTiler", aname, cdir.getAbsolutePath(), System.Convert.ToString(tdim.width), System.Convert.ToString(tdim.height)}));
			
			// get the progress dialog
			//UPGRADE_NOTE: Final was removed from the declaration of 'pd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ProgressDialog pd = ProgressDialog.createOnEDT(ModuleManagerWindow.Instance, "Processing Image Tiles", " ");
			
			// set up the process
			//UPGRADE_NOTE: Final was removed from the declaration of 'outP '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			InputStreamPump outP = new InputOutputStreamPump(null, System.Console.OpenStandardOutput());
			//UPGRADE_NOTE: Final was removed from the declaration of 'errP '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			InputStreamPump errP = new InputOutputStreamPump(null, System.Console.Error);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'proc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ProcessWrapper proc = new ProcessLauncher().launch(null, outP, errP, args.toArray(new System.String[args.size()]));
			
			// write the image paths to child's stdin, one per line
			PrintWriter stdin = null;
			try
			{
				stdin = new PrintWriter(proc.stdin);
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String m: multi)
				{
					stdin.println(m);
				}
			}
			finally
			{
				IOUtils.closeQuietly(stdin);
			}
			
			Socket csock = null;
			DataInputStream in_Renamed = null;
			try
			{
				csock = ssock.accept();
				csock.shutdownOutput();
				
				in_Renamed = new DataInputStream(csock.getInputStream());
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'progressor '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Progressor progressor = new AnonymousClassProgressor(pd, this, 0, tcount);
				
				// setup the cancel button in the progress dialog
				EDT.execute(new AnonymousClassRunnable(pd, proc, this));
				
				bool done = false;
				sbyte type;
				while (!done)
				{
					type = in_Renamed.readByte();
					
					switch (type)
					{
						
						case STARTING_IMAGE: 
							//UPGRADE_NOTE: Final was removed from the declaration of 'ipath '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							System.String ipath = in_Renamed.readUTF();
							
							EDT.execute(new AnonymousClassRunnable1(pd, ipath, this));
							break;
						
						
						case TILE_WRITTEN: 
							progressor.increment();
							
							if (progressor.get_Renamed() >= tcount)
							{
								pd.setVisible(false);
							}
							break;
						
						
						case TILING_FINISHED: 
							done = true;
							break;
						
						
						default: 
							throw new System.SystemException("bad type: " + type);
						
					}
				}
				
				in_Renamed.close();
				csock.close();
				ssock.close();
			}
			catch (IOException e)
			{
				
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
				IOUtils.closeQuietly(csock);
				IOUtils.closeQuietly(ssock);
			}
			
			// wait for the tiling process to end
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'retval '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int retval = proc.future.get_Renamed();
				if (retval != 0)
				{
					throw new IOException("return value == " + retval);
				}
			}
			catch (ExecutionException e)
			{
				// should never happen
				throw new IllegalStateException(e);
			}
			catch (System.Threading.ThreadInterruptedException e)
			{
				// should never happen
				throw new IllegalStateException(e);
			}
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.launch.TilingHandler'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'aname '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.String aname;
		//UPGRADE_NOTE: Final was removed from the declaration of 'cdir '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal File cdir;
		//UPGRADE_NOTE: Final was removed from the declaration of 'tdim '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal Dimension tdim;
		//UPGRADE_NOTE: Final was removed from the declaration of 'maxheap_limit '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int maxheap_limit;
		//UPGRADE_NOTE: Final was removed from the declaration of 'pid '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int pid;
		
		/// <summary> Creates a {@code TilingHandler}.
		/// 
		/// </summary>
		/// <param name="aname">the path to the ZIP archive
		/// </param>
		/// <param name="cdir">the tile cache diretory
		/// </param>
		/// <param name="tdim">the tile size
		/// </param>
		/// <param name="pid">the id of the child process
		/// </param>
		public TilingHandler(System.String aname, File cdir, Dimension tdim, int mhlim, int pid)
		{
			InitBlock();
			this.aname = aname;
			this.cdir = cdir;
			this.tdim = tdim;
			this.maxheap_limit = mhlim;
			this.pid = pid;
		}
		
		protected internal virtual bool isFresh(FileArchive archive, FileStore tcache, System.String ipath)
		{
			// look at the first 1:1 tile
			//UPGRADE_NOTE: Final was removed from the declaration of 'tpath '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String tpath = TileUtils.tileName(ipath, 0, 0, 1);
			
			// check whether the image is older than the tile
			//UPGRADE_NOTE: Final was removed from the declaration of 'imtime '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			long imtime = archive.getMTime(ipath);
			
			return imtime > 0 && imtime <= tcache.getMTime(tpath);
		}
		
		protected internal virtual Dimension getImageSize(DataArchive archive, System.String ipath)
		{
			InputStream in_Renamed = null;
			try
			{
				in_Renamed = archive.getInputStream(ipath);
				//UPGRADE_NOTE: Final was removed from the declaration of 'id '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Dimension id = ImageUtils.getImageSize(ipath, in_Renamed);
				in_Renamed.close();
				return id;
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected Pair < Integer, Integer > findImages(
		DataArchive archive, 
		FileStore tcache, 
		List < String > multi, 
		List < Pair < String, IOException >> failed) throws IOException
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void runSlicer(List < String > multi, final int tcount, int maxheap) 
		throws CancellationException, IOException
		
		protected internal virtual void  makeHashDirs()
		{
			for (int i = 0; i < 16; ++i)
			{
				for (int j = 0; j < 16; ++j)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					File d = new File(String.format("%s/%1x/%1x%1x", cdir, i, i, j));
					FileUtils.forceMkdir(d);
				}
			}
		}
		
		protected internal virtual void  cleanup()
		{
			FileUtils.forceDelete(cdir);
		}
		
		/// <summary> Slices the tiles.
		/// 
		/// </summary>
		/// <throws>  IOException if one occurs </throws>
		public virtual void  sliceTiles()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < String > multi = new ArrayList < String >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < Pair < String, IOException >> failed = 
			new ArrayList < Pair < String, IOException >>();
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Pair < Integer, Integer > s;
			DataArchive archive = null;
			try
			{
				archive = new DataArchive(aname);
				//UPGRADE_NOTE: Final was removed from the declaration of 'tcache '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				FileStore tcache = new ImageTileDiskCache(cdir.getAbsolutePath());
				s = findImages(archive, tcache, multi, failed);
				archive.close();
			}
			finally
			{
				IOUtils.closeQuietly(archive);
			}
			
			// nothing to do if no images need tiling
			if (multi.isEmpty())
			{
				logger.info("No images to tile.");
				return ;
			}
			
			// ensure that the tile directories exist
			makeHashDirs();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'tcount '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int tcount = s.first;
			//UPGRADE_NOTE: Final was removed from the declaration of 'max_data_mbytes '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int max_data_mbytes = (4 * s.second) >> 20;
			
			// fix the max heap
			
			// This was determined empirically.
			//UPGRADE_NOTE: Final was removed from the declaration of 'maxheap_estimated '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int maxheap_estimated = (int) (1.66 * max_data_mbytes + 150);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'maxheap '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int maxheap = System.Math.Min(maxheap_estimated, maxheap_limit);
			
			// slice, and cleanup on failure
			try
			{
				runSlicer(multi, s.first, maxheap);
			}
			catch (CancellationException e)
			{
				cleanup();
				throw e;
			}
			catch (IOException e)
			{
				cleanup();
				throw e;
			}
		}
		static TilingHandler()
		{
			logger = LoggerFactory.getLogger(typeof(TilingHandler));
		}
	}
}