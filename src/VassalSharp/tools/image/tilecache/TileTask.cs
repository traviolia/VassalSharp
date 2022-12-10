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
//UPGRADE_TODO: The type 'java.util.concurrent.Callable' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Callable = java.util.concurrent.Callable;
namespace VassalSharp.tools.image.tilecache
{
	
	/// <summary> Slices one tile from an image and writes it to disk.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
	public class TileTask : Callable
	{
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Void >
		//UPGRADE_NOTE: Final was removed from the declaration of 'src '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.Drawing.Bitmap src;
		//UPGRADE_NOTE: Final was removed from the declaration of 'dst '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.IO.FileInfo dst;
		//UPGRADE_NOTE: Final was removed from the declaration of 'tx '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int tx;
		//UPGRADE_NOTE: Final was removed from the declaration of 'ty '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int ty;
		//UPGRADE_NOTE: Final was removed from the declaration of 'tw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int tw;
		//UPGRADE_NOTE: Final was removed from the declaration of 'th '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int th;
		//UPGRADE_NOTE: Final was removed from the declaration of 'dw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int dw;
		//UPGRADE_NOTE: Final was removed from the declaration of 'dh '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int dh;
		
		/// <param name="src">the source image
		/// </param>
		/// <param name="dst">the destination file
		/// </param>
		/// <param name="tx">the tile column
		/// </param>
		/// <param name="ty">the tile row
		/// </param>
		/// <param name="tw">the standard tile width
		/// </param>
		/// <param name="th">the standard tile height
		/// </param>
		/// <param name="dw">the width of the whole destination image
		/// </param>
		/// <param name="dh">the height of the whole destination image
		/// </param>
		public TileTask(System.Drawing.Bitmap src, System.IO.FileInfo dst, int tx, int ty, int tw, int th, int dw, int dh)
		{
			this.src = src;
			this.dst = dst;
			this.tx = tx;
			this.ty = ty;
			this.tw = tw;
			this.th = th;
			this.dw = dw;
			this.dh = dh;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual System.Void call()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap tile = sliceTile();
			TileUtils.write(tile, dst);
			//UPGRADE_TODO: The 'System.Void' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
			return null;
		}
		
		protected internal virtual System.Drawing.Bitmap sliceTile()
		{
			// get actual tile width, height (edge tiles can be less than full size)
			//UPGRADE_NOTE: Final was removed from the declaration of 'atw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int atw = System.Math.Min(tw, dw - tx * tw);
			//UPGRADE_NOTE: Final was removed from the declaration of 'ath '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int ath = System.Math.Min(th, dh - ty * th);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int type = (int) src.PixelFormat;
			
			// slice the tile from the source image
			//UPGRADE_NOTE: Final was removed from the declaration of 'tile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap tile = new System.Drawing.Bitmap(atw, ath, (System.Drawing.Imaging.PixelFormat) type);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(tile);
			//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
			g.DrawImage(src, SupportClass.CreateRectangle(0, 0, atw, ath), SupportClass.CreateRectangle(tx * tw, ty * th, tx * tw + atw, ty * th + ath), System.Drawing.GraphicsUnit.Pixel);
			g.Dispose();
			
			return tile;
		}
	}
}