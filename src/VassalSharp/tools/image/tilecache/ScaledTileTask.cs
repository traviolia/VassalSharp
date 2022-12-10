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
using GeneralFilter = VassalSharp.tools.image.GeneralFilter;
namespace VassalSharp.tools.image.tilecache
{
	
	/// <summary> Slices one scaled tile from an image and writes it to disk.</summary>
	class ScaledTileTask:TileTask
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'filter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal GeneralFilter.Filter filter;
		
		/// <summary> Creates a scaled tile task.
		/// 
		/// </summary>
		/// <param name="src">the source image
		/// </param>
		/// <param name="filter">the resampling filter
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
		/// <param name="dw">the width of the whole scaled image
		/// </param>
		/// <param name="dh">the height of the whole scaled image
		/// </param>
		public ScaledTileTask(System.Drawing.Bitmap src, System.IO.FileInfo dst, GeneralFilter.Filter filter, int tx, int ty, int tw, int th, int dw, int dh):base(src, dst, tx, ty, tw, th, dw, dh)
		{
			this.filter = filter;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override System.Drawing.Bitmap sliceTile()
		{
			// get actual tile width, height (edge tiles can be less than full size)
			//UPGRADE_NOTE: Final was removed from the declaration of 'atw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int atw = System.Math.Min(tw, dw - tx * tw);
			//UPGRADE_NOTE: Final was removed from the declaration of 'ath '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int ath = System.Math.Min(th, dh - ty * th);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int type = (int) src.PixelFormat;
			
			// scale the tile from the source image
			//UPGRADE_NOTE: Final was removed from the declaration of 'tile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap tile = new System.Drawing.Bitmap(atw, ath, (System.Drawing.Imaging.PixelFormat) type);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'tileR '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'java.awt.image.WritableRaster' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageWritableRaster'"
			//UPGRADE_TODO: Method 'java.awt.image.WritableRaster.createWritableTranslatedChild' was converted to 'System.Drawing.Bitmap.Clone' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageWritableRastercreateWritableTranslatedChild_int_int'"
			System.Drawing.Bitmap tileR = (System.Drawing.Bitmap) tile.Clone();
			//UPGRADE_NOTE: Final was removed from the declaration of 'dstFR '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle dstFR = new System.Drawing.Rectangle(0, 0, dw, dh);
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			GeneralFilter.zoom(tileR, ref dstFR, src, filter);
			
			return tile;
		}
	}
}