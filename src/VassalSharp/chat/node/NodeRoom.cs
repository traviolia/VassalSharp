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
using LockableRoom = VassalSharp.chat.LockableRoom;
using Player = VassalSharp.chat.Player;
using SimpleRoom = VassalSharp.chat.SimpleRoom;
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
namespace VassalSharp.chat.node
{
	
	/// <summary> Subclass of {@link SimpleRoom} for use on the client side of a hierarchical server</summary>
	public class NodeRoom:SimpleRoom, LockableRoom
	{
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		virtual public System.Collections.Specialized.NameValueCollection Info
		{
			get
			{
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
				System.Collections.Specialized.NameValueCollection p = new System.Collections.Specialized.NameValueCollection();
				if (owner != null)
				{
					//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
					p[OWNER] = owner;
				}
				if (locked)
				{
					//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
					p[LOCKED] = "true"; //$NON-NLS-1$
				}
				if (members != null)
				{
					//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
					p[MEMBERS] = StringArrayConfigurer.arrayToString(members);
				}
				return p;
			}
			
			set
			{
				owner = value[OWNER] == null?owner:value[OWNER];
				members = StringArrayConfigurer.stringToArray(value[MEMBERS] == null?StringArrayConfigurer.arrayToString(members):value[MEMBERS]);
				locked = "true".Equals(value.Get(LOCKED)); //$NON-NLS-1$
			}
			
		}
		virtual public System.String[] Members
		{
			get
			{
				return members;
			}
			
		}
		virtual public Player OwningPlayer
		{
			get
			{
				return getPlayer(owner);
			}
			
		}
		virtual public bool Locked
		{
			get
			{
				return locked;
			}
			
		}
		public const System.String OWNER = "owner"; //$NON-NLS-1$
		public const System.String MEMBERS = "members"; //$NON-NLS-1$
		public const System.String STATUS = "status"; //$NON-NLS-1$
		public const System.String LOCKED = "locked"; //$NON-NLS-1$
		
		private System.String owner; // The userId (as in GameModule.getUserId() of the player who created the room
		private System.String[] members; // The players who were in the room at the time it was closed;
		private bool locked; // If locked, no new players may join
		
		public NodeRoom()
		{
		}
		
		public virtual void  setOwner(System.String owner)
		{
			this.owner = owner;
		}
		
		public NodeRoom(System.String name):base(name)
		{
		}
		
		public NodeRoom(System.String name, Player[] players):base(name, players)
		{
		}
		
		/// <summary> Set the members to be the list of players currently in the room</summary>
		public virtual void  setMembersToCurrentPlayers()
		{
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			NodePlayer[] p = l.toArray(new NodePlayer[l.size()]);
			members = new System.String[p.Length];
			for (int i = 0; i < p.Length; i++)
			{
				members[i] = p[i].getId();
			}
		}
		
		public virtual bool isMember(NodePlayer p)
		{
			bool isMember = false;
			if (members != null)
			{
				for (int i = 0; !isMember && i < members.Length; i++)
				{
					isMember = p.getId().Equals(members[i]);
				}
			}
			return isMember;
		}
		
		public virtual bool isOwner(NodePlayer p)
		{
			return p != null && owner != null && owner.Equals(p.getId());
		}
		
		public virtual bool isOwner(System.String jid)
		{
			return jid.Equals(owner);
		}
		
		public virtual System.String getOwner()
		{
			return owner;
		}
		
		public virtual void  lock_Renamed()
		{
			locked = true;
		}
		
		public virtual void  toggleLock()
		{
			locked = !locked;
		}
	}
}