/*
* $Id$
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
	
	/// <summary> Represents the history and current state of connections to the chat
	/// room server
	/// 
	/// </summary>
	/// <author>  rkinney
	/// </author>
	public class ModuleSummary
	{
		virtual public System.String ModuleName
		{
			get
			{
				return moduleName;
			}
			
			set
			{
				this.moduleName = value;
			}
			
		}
		virtual public Room[] Rooms
		{
			get
			{
				return rooms.values().toArray(new Room[rooms.size()]);
			}
			
		}
		private System.String moduleName;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private Map < String, Room > rooms = new HashMap < String, Room >();
		
		public ModuleSummary(System.String moduleName)
		{
			this.moduleName = moduleName;
		}
		
		public ModuleSummary(System.String moduleName, Room[] rooms):this(moduleName)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Room r: rooms)
			{
				this.rooms.put(r.getName(), r);
			}
		}
		
		public virtual void  addRoom(Room r)
		{
			rooms.put(r.getName(), r);
		}
		
		public virtual SimpleRoom getRoom(System.String name)
		{
			return (SimpleRoom) rooms.get_Renamed(name);
		}
		
		public virtual int numPlayers()
		{
			Room[] roomsArray = Rooms;
			int n = 0;
			for (int i = 0; i < roomsArray.Length; ++i)
			{
				n += ((SimpleRoom) roomsArray[i]).numPlayers();
			}
			return n;
		}
		
		public override System.String ToString()
		{
			return moduleName;
		}
	}
	public interface ServerStatus
	{
		/// <summary>Return the current connections to the server </summary>
		ModuleSummary[] Status
		{
			get;
			
		}
		System.String[] SupportedTimeRanges
		{
			get;
			
		}
		
		/// <returns> the connections to the server within <code>time</code>
		/// milliseconds of the current time
		/// </returns>
		ModuleSummary[] getHistory(System.String timeRange);
		
	}
}