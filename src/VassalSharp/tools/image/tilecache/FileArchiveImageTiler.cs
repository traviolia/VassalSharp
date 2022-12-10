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
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutorService' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutorService = java.util.concurrent.ExecutorService;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using ImageLoader = VassalSharp.tools.image.ImageLoader;
using FileArchive = VassalSharp.tools.io.FileArchive;
using IOUtils = VassalSharp.tools.io.IOUtils;
//UPGRADE_TODO: The type 'VassalSharp.tools.lang.Callback' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Callback = VassalSharp.tools.lang.Callback;
namespace VassalSharp.tools.image.tilecache
{
	
	/// <summary> Tiles images contained in a ZIP archive.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class FileArchiveImageTiler
	{
		public FileArchiveImageTiler()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String ipath: ipaths)
			{
				logger.info("Tiling {}", ipath);
				imageListener.receive(ipath);
				
				System.Drawing.Bitmap src = null;
				System.IO.Stream in_Renamed = null;
				try
				{
					in_Renamed = fa.getInputStream(ipath);
					src = loader.load(ipath, in_Renamed, (int) System.Drawing.Imaging.PixelFormat.Format32bppRgb, (int) System.Drawing.Imaging.PixelFormat.Format32bppPArgb, false);
					in_Renamed.Close();
				}
				catch (System.IO.IOException e)
				{
					logger.error("", e);
					continue;
				}
				finally
				{
					IOUtils.closeQuietly(in_Renamed);
				}
				
				slicer.slice(src, ipath, tpath, tw, th, exec, tileListener);
			}
			
			exec.shutdown();
			doneListener.receive(null);
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.tools.image.tilecache.FileArchiveImageTiler'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		/// <summary> Tile image contained in an archive.
		/// 
		/// </summary>
		/// <param name="fa">the file archive
		/// </param>
		/// <param name="tpath">path to the output directory
		/// </param>
		/// <param name="tw">tile width, in pixels
		/// </param>
		/// <param name="th">tile height, in pixels
		/// </param>
		/// <param name="ipaths">paths within the archive to images to be tiled
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void run(
		FileArchive fa, 
		final String tpath, 
		int tw, 
		int th, 
		String [] ipaths, 
		ExecutorService exec, 
		ImageLoader loader, 
		TileSlicer slicer, 
		Callback < String > imageListener, 
		Callback < Void > tileListener, 
		Callback < Void > doneListener
		) throws IOException
		static FileArchiveImageTiler()
		{
			logger = LoggerFactory.getLogger(typeof(FileArchiveImageTiler));
		}
	}
}