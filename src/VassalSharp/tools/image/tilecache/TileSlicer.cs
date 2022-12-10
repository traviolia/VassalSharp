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
//UPGRADE_TODO: The type 'VassalSharp.tools.lang.Callback' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Callback = VassalSharp.tools.lang.Callback;
namespace VassalSharp.tools.image.tilecache
{
	
	/// <summary> Slices an image into tiles.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public interface TileSlicer
	{
		/// <summary> Slices an image into tiles.
		/// 
		/// </summary>
		/// <param name="src">the source image
		/// </param>
		/// <param name="iname">the basename for the tiles
		/// </param>
		/// <param name="tpath">the path for the tiles
		/// </param>
		/// <param name="tw">the tile width
		/// </param>
		/// <param name="th">the tile height
		/// </param>
		/// <param name="exec">the executor in which to run tasks
		/// </param>
		/// <param name="progress">a callback for indicating progress
		/// </param>
		void  slice(System.Drawing.Bitmap src, System.String iname, System.String tpath, int tw, int th, ExecutorService exec);
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Callback < Void > progress
	) throws IOException;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}