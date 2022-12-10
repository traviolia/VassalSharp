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
//UPGRADE_TODO: The type 'javax.imageio.ImageIO' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ImageIO = javax.imageio.ImageIO;
namespace VassalSharp.tools.image.tilecache
{
	
	/// <summary> Converts tile files to image files.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class TileToImage
	{
		/// <summary> Converts a tile file to an image file.
		/// 
		/// </summary>
		/// <param name="args">the first argument is the path of the source tile file,
		/// the second is the path of the PNG to write
		/// 
		/// </param>
		/// <throws>  IOException if something goes wrong </throws>
		[STAThread]
		public static void  Main(System.String[] args)
		{
			// Oh we have no heads, we have no HEADS!
			//UPGRADE_ISSUE: Method 'java.lang.System.setProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
			System_Renamed.setProperty("java.awt.headless", "true");
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'tfile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.FileInfo tfile = new System.IO.FileInfo(args[0]);
			//UPGRADE_NOTE: Final was removed from the declaration of 'img '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap img = TileUtils.read(tfile);
			//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javalangString'"
			ImageIO.write(img, "PNG", new System.IO.FileStream(args[1], System.IO.FileMode.Create));
		}
	}
}