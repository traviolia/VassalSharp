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
using System;
using Map = VassalSharp.build.module.Map;
using Board = VassalSharp.build.module.map.boardPicker.Board;
using MapGrid = VassalSharp.build.module.map.boardPicker.board.MapGrid;
namespace VassalSharp.counters
{
	
	/// <summary> Accepts all pieces within a specified range of a given point on a map</summary>
	public class RangeFilter : PieceFilter
	{
		private Map map;
		private System.Drawing.Point position;
		private MapGrid grid;
		private int range;
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public RangeFilter(Map map, ref System.Drawing.Point location, int range)
		{
			this.position = location;
			this.map = map;
			this.range = range;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			Board b = map.findBoard(ref location);
			if (b != null)
			{
				grid = b.getGrid();
			}
		}
		
		public virtual bool accept(GamePiece piece)
		{
			bool accept = false;
			if (piece.getMap() == map)
			{
				System.Drawing.Point pos = piece.Position;
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
				int theRange = grid != null?grid.range(ref position, ref pos):(int) System.Math.Round(SupportClass.PointFSupport.Distance(position, pos));
				accept = theRange <= range;
			}
			return accept;
		}
	}
}