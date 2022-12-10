using System;
namespace VassalSharp.build.module.map.boardPicker.board
{
	
	/*
	* $Id$
	*
	* Copyright (c) 2005 by Rodney Kinney
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
	
	/// <summary> A Map grid that consists of a regular tiling of shapes</summary>
	public interface GeometricGrid:MapGrid
	{
		/// <summary> Return the Area representing a set of tiles on the grid</summary>
		/// <param name="center">the center of the tiles
		/// </param>
		/// <param name="range">the number of tiles outward from the center to include
		/// </param>
		/// <returns>
		/// </returns>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		System.Drawing.Region getGridShape(ref System.Drawing.Point center, int range);
	}
}