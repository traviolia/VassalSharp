/*
* $Id$
*
* Copyright (c) 2010 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutorService' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutorService = java.util.concurrent.ExecutorService;
//UPGRADE_TODO: The type 'java.util.concurrent.LinkedBlockingQueue' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LinkedBlockingQueue = java.util.concurrent.LinkedBlockingQueue;
//UPGRADE_TODO: The type 'java.util.concurrent.ThreadPoolExecutor' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ThreadPoolExecutor = java.util.concurrent.ThreadPoolExecutor;
//UPGRADE_TODO: The type 'java.util.concurrent.TimeUnit' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TimeUnit = java.util.concurrent.TimeUnit;
using DaemonThreadFactory = VassalSharp.tools.concurrent.DaemonThreadFactory;
using FallbackImageTypeConverter = VassalSharp.tools.image.FallbackImageTypeConverter;
using ImageIOImageLoader = VassalSharp.tools.image.ImageIOImageLoader;
using ImageLoader = VassalSharp.tools.image.ImageLoader;
using ImageTypeConverter = VassalSharp.tools.image.ImageTypeConverter;
using IOUtils = VassalSharp.tools.io.IOUtils;
using TemporaryFileFactory = VassalSharp.tools.io.TemporaryFileFactory;
//UPGRADE_TODO: The type 'VassalSharp.tools.lang.Callback' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Callback = VassalSharp.tools.lang.Callback;
namespace VassalSharp.tools.image.tilecache
{
	
	/// <summary> Converts an image file to tile files.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class ImageToTiles
	{
		public ImageToTiles()
		{
			InitBlock();
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassTemporaryFileFactory' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		public class AnonymousClassTemporaryFileFactory : TemporaryFileFactory
		{
			public AnonymousClassTemporaryFileFactory(ImageToTiles enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ImageToTiles enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ImageToTiles enclosingInstance;
			public ImageToTiles Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual System.IO.FileInfo create()
			{
				//UPGRADE_ISSUE: Method 'java.io.File.createTempFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioFilecreateTempFile_javalangString_javalangString_javaioFile'"
				return File.createTempFile("img", null, new File(tpath));
			}
		}
		private void  InitBlock()
		{
			tfac = new AnonymousClassTemporaryFileFactory(this);
			itc = new FallbackImageTypeConverter(tfac);
			loader = new ImageIOImageLoader(itc);
			in_Renamed = new FileInputStream(ipath);
			src = loader.load(ipath, in_Renamed, (int) System.Drawing.Imaging.PixelFormat.Format32bppRgb, (int) System.Drawing.Imaging.PixelFormat.Format32bppPArgb, false);
			in_Renamed.Close();
			IOUtils.closeQuietly(in_Renamed);
		}
		/// <summary> Converts an image file to tile files.
		/// 
		/// </summary>
		/// <param name="args">the first argument is the path of the source image file,
		/// the second argument is the destination path for the tile files, the
		/// third and fourth arguments are the tile width and height
		/// 
		/// </param>
		/// <throws>  IOException if someting goes wrong </throws>
		[STAThread]
		public static void  Main(System.String[] args)
		{
			// Oh we have no heads, we have no HEADS!
			//UPGRADE_ISSUE: Method 'java.lang.System.setProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
			System_Renamed.setProperty("java.awt.headless", "true");
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'ipath '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String ipath = args[0];
			//UPGRADE_NOTE: Final was removed from the declaration of 'tpath '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String tpath = args[1];
			//UPGRADE_NOTE: Final was removed from the declaration of 'tw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int tw = System.Int32.Parse(args[2]);
			//UPGRADE_NOTE: Final was removed from the declaration of 'th '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int th = System.Int32.Parse(args[3]);
			
			// TODO: Determine what the optimal number of threads is.
			//UPGRADE_NOTE: Final was removed from the declaration of 'runtime '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Diagnostics.Process runtime = System.Diagnostics.Process.GetCurrentProcess();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ExecutorService exec = new ThreadPoolExecutor(
			runtime.availableProcessors(), 
			runtime.availableProcessors() + 1, 
			60, TimeUnit.SECONDS, 
			new LinkedBlockingQueue < Runnable >(), 
			new DaemonThreadFactory(ImageToTiles.
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class.getSimpleName())
		);
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'tfac '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'tfac' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		internal TemporaryFileFactory tfac;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'itc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'itc' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		internal ImageTypeConverter itc;
		//UPGRADE_NOTE: Final was removed from the declaration of 'loader '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'loader' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		internal ImageLoader loader;
		
		internal System.Drawing.Bitmap src = null;
		internal System.IO.Stream in_Renamed = null;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		try
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		finally
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'iname '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		internal System.String iname = new File(ipath).Name;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		final Callback < Void > dotter = new Callback < Void >()
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  receive(ref System.Void obj)
		{
			System.Console.Out.Write('.');
		}
	}
	
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final TileSlicer slicer = new TileSlicerImpl();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	slicer.slice(src, iname, tpath, tw, th, exec, dotter);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	exec.shutdown();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	System.out.println();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}