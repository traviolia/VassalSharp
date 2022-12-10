/*
* $Id$
*
* Copyright (c) 2007 by Rodney Kinney
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
using Player = VassalSharp.chat.Player;
using Room = VassalSharp.chat.Room;
namespace VassalSharp.chat.peer2peer
{
	
	/// <summary> Tracks players entering/exiting a room</summary>
	public class RoomTracker
	{
		private void  InitBlock()
		{
			return Collections.enumeration(joinedPlayers);
			return Collections.enumeration(leftPlayers);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < Player > joinedPlayers;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < Player > leftPlayers;
		
		public RoomTracker()
		{
			InitBlock();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			joinedPlayers = new ArrayList < Player >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			leftPlayers = new ArrayList < Player >();
		}
		
		public virtual void  init(Room r)
		{
			joinedPlayers.clear();
			leftPlayers.clear();
			leftPlayers.addAll(r.getPlayerList());
		}
		
		public virtual void  finalize(Room r)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Player p: r.getPlayerList())
			{
				if (!leftPlayers.contains(p))
				{
					joinedPlayers.add(p);
				}
				leftPlayers.remove(p);
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Enumeration < Player > getJoinedPlayers()
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Enumeration < Player > getLeftPlayers()
	}
}