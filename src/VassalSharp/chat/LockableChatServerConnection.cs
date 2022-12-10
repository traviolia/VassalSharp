/*
*
* Copyright (c) 2000-2009 by Rodney Kinney, Brent Easton
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
namespace VassalSharp.chat
{
	
	public interface LockableChatServerConnection:ChatServerConnection
	{
		
		/// <summary>Lock/Unlock the given room </summary>
		void  lockRoom(LockableRoom r);
		
		/// <summary>Can a player be invited to this room by me? </summary>
		bool isInvitable(Player invitee);
		
		/// <summary>Invite a player to this room </summary>
		void  sendInvite(Player p);
		
		/// <summary>Process Invitation </summary>
		void  doInvite(System.String playerId, System.String roomName);
		
		/// <summary>Can a player be kicked from this room by me? </summary>
		bool isKickable(Player kickee);
		
		/// <summary>Kick a player from this room </summary>
		void  doKick(Player p);
		
		/// <summary>Return the name of the default room </summary>
		System.String getDefaultRoomName();
		
		/// <summary>Is specified room the default room? </summary>
		bool isDefaultRoom(Room r);
	}
}