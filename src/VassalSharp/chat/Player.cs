/*
*
* Copyright (c) 2000-2007 by Rodney Kinney
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
	
	/// <summary> Represents an occupant of a chat room, i.e. a VASSAL user connected to
	/// the server
	/// 
	/// </summary>
	/// <author>  rkinney
	/// </author>
	public interface Player
	{
		/// <summary>Unique identifier for this player </summary>
		System.String getId();
		/// <summary>Display name (nickname) for the player </summary>
		System.String getName();
		/// <summary>Return the current status of the player </summary>
		PlayerStatus getStatus();
	}
}