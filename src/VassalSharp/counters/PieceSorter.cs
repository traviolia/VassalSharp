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
namespace VassalSharp.counters
{
	
	/// <summary> Sorts GamePieces according to their position:
	/// If on different Maps, order by Map id
	/// If in different Stacks order by Stack position on the Map
	/// If in the same Stack, order by position within the Stack
	/// </summary>
	public class PieceSorter : System.Collections.IComparer
	{
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< GamePiece >
		public virtual int compare(GamePiece p1, GamePiece p2)
		{
			if (p1.getMap() == null)
			{
				return p2.getMap() == null?0:1;
			}
			else if (p2.getMap() == null)
			{
				return - 1;
			}
			
			if (p1.getMap() != p2.getMap())
			{
				return p1.getMap().getId().compareTo(p2.getMap().getId());
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 's1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Stack s1 = p1 is Stack?(Stack) p1:p1.Parent;
			//UPGRADE_NOTE: Final was removed from the declaration of 's2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Stack s2 = p2 is Stack?(Stack) p2:p2.Parent;
			
			if (s1 == null)
			{
				return s2 == null?0:1;
			}
			else if (s2 == null)
			{
				return - 1;
			}
			else
			{
				int result = p1.getMap().indexOf(s1) - p2.getMap().indexOf(s2);
				if (result == 0)
				{
					// Pieces must be in the same stack
					result = s1.indexOf(p1) - s2.indexOf(p2);
				}
				return result;
			}
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		virtual public System.Int32 Compare(System.Object x, System.Object y)
		{
			return 0;
		}
	}
}