/*
* $Id$
*
* Copyright (c) 2000-2006 by Rodney Kinney
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
using PlayerRoster = VassalSharp.build.module.PlayerRoster;
namespace VassalSharp.counters
{
	
	/// <summary> Access is granted if {@link VassalSharp.build.module.PlayerRoster#getMySide()}
	/// is in a specified list
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class SpecifiedSideAccess : PieceAccess
	{
		public SpecifiedSideAccess()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			this.sides = Collections.unmodifiableList(sides);
			return sides;
		}
		virtual public System.String CurrentPlayerId
		{
			get
			{
				return PlayerRoster.getMySide();
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < String > sides;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public SpecifiedSideAccess(List < String > sides)
		
		public virtual bool currentPlayerHasAccess(System.String ownerId)
		{
			if (ownerId == null)
			{
				return true;
			}
			else if (sides.contains(ownerId))
			{
				return !GlobalAccess.HideAll && sides.contains(CurrentPlayerId);
			}
			else
			{
				return false;
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getSides()
		
		public virtual bool currentPlayerCanModify(System.String ownerId)
		{
			return sides.contains(CurrentPlayerId);
		}
	}
}