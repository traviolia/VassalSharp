/*
* $Id$
*
* Copyright (c) 2004-2012 by Rodney Kinney, Brent Easton
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
using Board = VassalSharp.build.module.map.boardPicker.Board;
using MapGrid = VassalSharp.build.module.map.boardPicker.board.MapGrid;
namespace VassalSharp.build.module.map.boardPicker.board.mapgrid
{
	
	/// <summary> A Component that can contain a {@link VassalSharp.build.module.map.boardPicker.board.MapGrid}</summary>
	public interface GridContainer
	{
		MapGrid Grid
		{
			set;
			
		}
		System.Drawing.Size Size
		{
			get;
			
		}
		void  removeGrid(MapGrid grid);
		Board getBoard();
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		bool contains(ref System.Drawing.Point point);
	}
}