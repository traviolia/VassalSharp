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
using ServerConnection = VassalSharp.build.module.ServerConnection;
using Command = VassalSharp.command.Command;
namespace VassalSharp.chat
{
	
	/// <summary> Specialized interface for chat-room server connections</summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public struct ChatServerConnection_Fields{
		/// <summary>Property representing the currently-occupied room </summary>
		public readonly static System.String ROOM = "Room"; //$NON-NLS-1$
		/// <summary>Property representing the list of all rooms </summary>
		public readonly static System.String AVAILABLE_ROOMS = "AvailableRooms"; //$NON-NLS-1$
		/// <summary>Property representing an informational message (e.g. "Connection succeeded")</summary>
		public readonly static System.String STATUS = "Status"; //$NON-NLS-1$
		/// <summary>Property representing the current player's information </summary>
		public readonly static System.String PLAYER_INFO = "Player"; //$NON-NLS-1$
		/// <summary>Property representing a message received from the remove server </summary>
		public readonly static System.String INCOMING_MSG = "Msg"; //$NON-NLS-1$
		/// <summary>Property representing the StatusServer implementation </summary>
		public readonly static System.String STATUS_SERVER = "StatusServer"; //$NON-NLS-1$
		public readonly static System.String DEFAULT_ROOM_NAME = "Main Room"; //$NON-NLS-1$
	}
	public interface ChatServerConnection:ServerConnection
	{
		//UPGRADE_NOTE: Members of interface 'ChatServerConnection' were extracted into structure 'ChatServerConnection_Fields'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1045'"
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary>Return a Player instance representing the current player </summary>
		/// <summary>Set the Player instance representing this player </summary>
		Player UserInfo
		{
			get;
			
			set;
			
		}
		
		
		/// <summary>Return the room currently occupied by the player </summary>
		Room getRoom();
		
		/// <summary>Join the given room </summary>
		void  setRoom(Room r);
		
		/// <summary>Return an array of all rooms on the server </summary>
		Room[] getAvailableRooms();
		
		/// <summary>Send a Command to a particular player </summary>
		void  sendTo(Player recipient, Command c);
	}
}