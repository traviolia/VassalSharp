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
using PeerInfo = org.litesoft.p2pchat.PeerInfo;
using Player = VassalSharp.chat.Player;
using Room = VassalSharp.chat.Room;
using SimpleRoom = VassalSharp.chat.SimpleRoom;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.peer2peer
{
	
	public class RoomManager
	{
		private void  InitBlock()
		{
			defaultRoom = new SimpleRoom(Resources.getString("Chat.main_room"));
		}
		virtual public Room[] Rooms
		{
			get
			{
				return rooms.toArray(new Room[rooms.size()]);
				/*
				//  System.err.println("--------");
				Room[] r = new Room[rooms.size()];
				for (int i = 0; i < r.length; ++i) {
				r[i] = (VassalSharp.chat.Room) rooms.elementAt(i);
				//      System.err.println("Room "+r[i]);
				//      for (int j=0;j<r[i].players.length;++j) {
				//    System.err.println("  "+((P2PPlayer)r[i].players[j]).summary());
				//      }
				}
				return r;*/
			}
			
		}
		virtual public System.String DefaultRoomName
		{
			set
			{
				defaultRoom.setName(value);
			}
			
		}
		virtual public VassalSharp.chat.Room DefaultRoom
		{
			get
			{
				return defaultRoom;
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < Room > rooms = new ArrayList < Room >();
		//UPGRADE_NOTE: The initialization of  'defaultRoom' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private SimpleRoom defaultRoom; //$NON-NLS-1$
		
		public RoomManager()
		{
			InitBlock();
			rooms.add(defaultRoom);
		}
		
		public virtual Room[] update(PeerInfo info)
		{
			P2PPlayer p = new P2PPlayer(info);
			Room oldRoom = getRoomContaining(p);
			Room newRoom = new SimpleRoom(p.Room);
			if (rooms.contains(newRoom))
			{
				newRoom = rooms.get_Renamed(rooms.indexOf(newRoom));
			}
			else
			{
				rooms.add(newRoom);
			}
			newRoom.addPlayer(p);
			if (oldRoom != null && !oldRoom.Equals(newRoom))
			{
				oldRoom.removePlayer(p);
				if (oldRoom.getPlayerList().size() == 0 && !oldRoom.Equals(defaultRoom))
				{
					rooms.remove(oldRoom);
				}
			}
			return Rooms;
		}
		
		public virtual Room[] remove(PeerInfo info)
		{
			P2PPlayer p = new P2PPlayer(info);
			for (int i = 0; i < rooms.size(); ++i)
			{
				Room r = rooms.get_Renamed(i);
				r.removePlayer(p);
				if (r.getPlayerList().size() == 0 && !r.Equals(defaultRoom))
				{
					rooms.remove(i--);
				}
			}
			return Rooms;
		}
		
		public virtual P2PPlayer getPlayerById(System.String id)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Room r: rooms)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Player p: r.getPlayerList())
				{
					P2PPlayer p2pp = (P2PPlayer) p;
					if (id.Equals(p2pp.getId()))
					{
						return p2pp;
					}
				}
			}
			return null;
		}
		
		public virtual SimpleRoom getRoomContaining(Player p)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Room r: rooms)
			{
				SimpleRoom sr = (SimpleRoom) r;
				if (sr.contains(p))
				{
					return sr;
				}
			}
			return null;
		}
		
		public virtual void  clear()
		{
			rooms.clear();
			defaultRoom.Players = new Player[0];
			rooms.add(defaultRoom);
		}
	}
}