/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
using GridNumbering = VassalSharp.build.module.map.boardPicker.board.mapgrid.GridNumbering;
namespace VassalSharp.build.module.map.boardPicker.board
{
	
	/// <summary> A MapGrid overlays a map board to constrain
	/// the legal locations of GamePieces
	/// </summary>
	[Serializable]
	public sealed class BadCoords:System.Exception
	{
		private const long serialVersionUID = 1L;
		
		public BadCoords():base()
		{
		}
		
		public BadCoords(System.String s):base(s)
		{
		}
	}
	public interface MapGrid
	{
		/// <summary>Whether this grid should be drawn on the map </summary>
		bool Visible
		{
			get;
			
		}
		/// <returns> the nearest grid location to the given point
		/// </returns>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		System.Drawing.Point snapTo(ref System.Drawing.Point p);
		
		/// <returns> true if the given point may not be a local location.
		/// I.e., if this grid will attempt to snap it to the nearest grid location 
		/// </returns>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		bool isLocationRestricted(ref System.Drawing.Point p);
		
		/// <returns> a string describing the location containing the given point
		/// </returns>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		System.String locationName(ref System.Drawing.Point p);
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		System.String localizedLocationName(ref System.Drawing.Point p);
		
		/// <returns> A point p such that locationName(p).equals(location).
		/// </returns>
		/// <throws>  BadCoords if the location is not valid or formatted incorrectly. </throws>
		System.Drawing.Point getLocation(System.String location);
		
		/// <returns> the range between two points, in some unit appropriate
		/// to the grid (e.g. hexes or squares)
		/// </returns>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		int range(ref System.Drawing.Point p1, ref System.Drawing.Point p2);
		
		/// <summary> Draw the grid</summary>
		/// <param name="bounds">the boundaries of the grid (in magnified coordinates)
		/// </param>
		/// <param name="scale">the magnification factor
		/// </param>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		void  draw(System.Drawing.Graphics g, ref System.Drawing.Rectangle bounds, ref System.Drawing.Rectangle visibleRect, double scale, bool reversed);
		
		GridNumbering getGridNumbering();
		
	}
}