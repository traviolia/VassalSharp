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
using Map = VassalSharp.build.module.Map;
namespace VassalSharp.counters
{
	
	/// <summary> Records the bounding boxes of GamePieces.  Use addPiece() to
	/// record the bounding box of a GamePiece at a certain time.  Use
	/// repaint() to repaint the appropriate areas of the maps to which the
	/// added pieces belonged.
	/// </summary>
	public class BoundsTracker
	{
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private Set < Map > maps;
		
		public BoundsTracker()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			maps = new HashSet < Map >();
		}
		
		public virtual void  clear()
		{
			maps.clear();
		}
		
		public virtual void  addPiece(GamePiece p)
		{
			if (p.getMap() != null)
			{
				maps.add(p.getMap());
			}
		}
		
		public virtual void  repaint()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Map m: maps)
			{
				m.repaint();
			}
		}
	}
}