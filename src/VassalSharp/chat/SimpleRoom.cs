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
namespace VassalSharp.chat
{
	
	/// <summary> A simple Room bean</summary>
	public class SimpleRoom : Room
	{
		private void  InitBlock()
		{
			return players.iterator();
			return Collections.unmodifiableList(players);
		}
		virtual public Player[] Players
		{
			set
			{
				this.players.clear();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Player p: players)
				{
					addPlayer(p);
				}
			}
			
		}
		private System.String name;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < Player > players = new ArrayList < Player >();
		
		public SimpleRoom()
		{
			InitBlock();
		}
		
		public SimpleRoom(System.String name):this(name, new Player[0])
		{
		}
		
		public SimpleRoom(System.String name, Player[] players)
		{
			InitBlock();
			this.name = name;
			Players = players;
		}
		
		public SimpleRoom(Room copy)
		{
			InitBlock();
			this.name = copy.getName();
			players.addAll(copy.getPlayerList());
		}
		
		public virtual System.String getName()
		{
			return name;
		}
		
		public virtual void  setName(System.String name)
		{
			this.name = name;
		}
		
		public virtual void  addPlayer(Player p)
		{
			int index = players.indexOf(p);
			if (index < 0)
			{
				players.add(p);
			}
			else
			{
				players.set_Renamed(index, p);
			}
		}
		
		public virtual Player getPlayer(System.String id)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Player player: players)
			{
				if (player.getId().equals(id))
				{
					return player;
				}
			}
			return null;
		}
		
		public virtual void  removePlayer(Player p)
		{
			players.remove(p);
		}
		
		public virtual bool contains(Player p)
		{
			return players.contains(p);
		}
		
		public override System.String ToString()
		{
			return name;
		}
		
		public virtual int numPlayers()
		{
			return players.size();
		}
		
		public override int GetHashCode()
		{
			return name.GetHashCode();
		}
		
		public  override bool Equals(System.Object o)
		{
			if (o is SimpleRoom)
			{
				return name == null?false:name.Equals(((SimpleRoom) o).name);
			}
			else
			{
				return false;
			}
		}
		
		/// <deprecated> Use {@link #getPlayerList()} instead. 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < Player > getPlayers()
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < Player > getPlayerList()
	}
}