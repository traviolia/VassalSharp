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
/*
* Created by IntelliJ IDEA.
* User: rkinney
* Date: Jul 21, 2002
* Time: 10:15:59 PM
* To change template for new interface use
* Code Style | Class Templates options (Tools | IDE Options).
*/
using System;
namespace VassalSharp.build.module.map.boardPicker.board.mapgrid
{
	
	/// <summary> Provides methods for assigning names to locations on a MapGrid, and drawing those locations when drawing a grid</summary>
	public interface GridNumbering
	{
		bool Visible
		{
			get;
			
		}
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		System.String locationName(ref System.Drawing.Point pt);
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		System.String localizedLocationName(ref System.Drawing.Point pt);
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		void  draw(System.Drawing.Graphics g, ref System.Drawing.Rectangle bounds, ref System.Drawing.Rectangle visibleRect, double scale, bool reversed);
		System.Drawing.Point getLocation(System.String location);
	}
}