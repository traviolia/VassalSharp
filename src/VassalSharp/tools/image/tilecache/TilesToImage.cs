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
//UPGRADE_TODO: The type 'java.util.regex.Matcher' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Matcher = java.util.regex.Matcher;
//UPGRADE_TODO: The type 'java.util.regex.Pattern' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Pattern = java.util.regex.Pattern;
//UPGRADE_TODO: The type 'javax.imageio.ImageIO' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ImageIO = javax.imageio.ImageIO;
namespace VassalSharp.tools.image.tilecache
{
	
	/// <summary> Reconstitute an image from tile files.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class TilesToImage
	{
		//UPGRADE_ISSUE: Interface 'java.io.FileFilter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioFileFilter'"
		private class AnonymousClassFileFilter : FileFilter
		{
			public AnonymousClassFileFilter(System.String base_Renamed)
			{
				InitBlock(base_Renamed);
			}
			private void  InitBlock(System.String base_Renamed)
			{
				this.base_Renamed = base_Renamed;
			}
			//UPGRADE_NOTE: Final variable base_Renamed was copied into class AnonymousClassFileFilter. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.String base_Renamed;
			public virtual bool accept(System.IO.FileInfo pathname)
			{
				return pathname.FullName.StartsWith(base_Renamed);
			}
		}
		/// <summary> Converts tile files to an image file.
		/// 
		/// </summary>
		/// <param name="args">the first argument is the path of the basename of the tile
		/// files, the second argument is the scale factor, the third is the path
		/// of the PNG to write
		/// 
		/// </param>
		/// <throws>  IOException if something goes wrong </throws>
		[STAThread]
		public static void  Main(System.String[] args)
		{
			// Oh we have no heads, we have no HEADS!
			//UPGRADE_ISSUE: Method 'java.lang.System.setProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
			System_Renamed.setProperty("java.awt.headless", "true");
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'base '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String base_Renamed = args[0];
			//UPGRADE_NOTE: Final was removed from the declaration of 'scale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String scale = args[1];
			//UPGRADE_NOTE: Final was removed from the declaration of 'dpath '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String dpath = args[2];
			
			System.Drawing.Size d;
			
			// find the tile in the upper left corner to get tile size
			d = TileUtils.size(base_Renamed + "(0,0)@1:" + scale);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'tw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int tw = d.Width;
			//UPGRADE_NOTE: Final was removed from the declaration of 'th '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int th = d.Height;
			
			// find the tile in the lower right corner to get the number of rows
			// and columns, as well as the width of the last column and the height
			// of the last row
			//UPGRADE_NOTE: Final was removed from the declaration of 'bdir '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.FileInfo bdir = new System.IO.FileInfo(new System.IO.FileInfo(base_Renamed).DirectoryName);
			//UPGRADE_NOTE: Final was removed from the declaration of 'filter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Interface 'java.io.FileFilter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioFileFilter'"
			FileFilter filter = new AnonymousClassFileFilter(base_Renamed);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Pattern p = Pattern.compile(base_Renamed + "\\((\\d+),(\\d+)\\)@1:");
			int max_row = 0;
			int max_col = 0;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(File f: bdir.listFiles(filter))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Matcher m = p.matcher(f.getPath());
				if (m.lookingAt())
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int c = Integer.parseInt(m.group(1));
					//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int r = Integer.parseInt(m.group(2));
					
					if (c > max_col)
						max_col = c;
					if (r > max_row)
						max_row = r;
				}
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'tcols '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int tcols = max_col + 1;
			//UPGRADE_NOTE: Final was removed from the declaration of 'trows '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int trows = max_row + 1;
			
			d = TileUtils.size(base_Renamed + "(" + max_col + "," + max_row + ")@1:" + scale);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'tw_last '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int tw_last = d.Width;
			//UPGRADE_NOTE: Final was removed from the declaration of 'th_last '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int th_last = d.Height;
			
			// create the new image
			//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int w = tw * (tcols - 1) + tw_last;
			//UPGRADE_NOTE: Final was removed from the declaration of 'h '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int h = th * (trows - 1) + th_last;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'img '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap img = new System.Drawing.Bitmap(w, h, (System.Drawing.Imaging.PixelFormat) System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
			
			// TODO: We could do this faster by making it multithreaded, since
			// writes to the destination image never overlap.
			for (int tx = 0; tx < tcols; ++tx)
			{
				for (int ty = 0; ty < trows; ++ty)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'tfile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.IO.FileInfo tfile = new System.IO.FileInfo(base_Renamed + "(" + tx + "," + ty + ")@1:" + scale);
					//UPGRADE_NOTE: Final was removed from the declaration of 'tile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Bitmap tile = TileUtils.read(tfile);
					
					//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
					g.DrawImage(tile, tx * tw, ty * th);
				}
			}
			
			// write the cobbled image
			//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javalangString'"
			ImageIO.write(img, "PNG", new System.IO.FileStream(dpath, System.IO.FileMode.Create));
		}
	}
}