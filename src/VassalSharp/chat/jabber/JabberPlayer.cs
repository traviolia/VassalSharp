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
//UPGRADE_TODO: The type 'org.jivesoftware.smack.util.StringUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using StringUtils = org.jivesoftware.smack.util.StringUtils;
using SimplePlayer = VassalSharp.chat.SimplePlayer;
using SimpleStatus = VassalSharp.chat.SimpleStatus;
namespace VassalSharp.chat.jabber
{
	
	public class JabberPlayer:SimplePlayer
	{
		virtual public System.String Jid
		{
			get
			{
				return jid;
			}
			
		}
		virtual public JabberRoom JoinedRoom
		{
			get
			{
				return joinedRoom;
			}
			
		}
		virtual public System.String RawJid
		{
			get
			{
				if (jid.contains("/"))
				{
					//$NON-NLS-1$
					return StringUtils.parseName(jid) + "@" + StringUtils.parseServer(jid); //$NON-NLS-1$
				}
				return jid;
			}
			
		}
		virtual public System.String LoginName
		{
			get
			{
				return StringUtils.parseName(jid);
			}
			
		}
		private System.String jid;
		private JabberRoom joinedRoom;
		
		private JabberPlayer(System.String jid):base(jid, "???", new SimpleStatus())
		{ //$NON-NLS-1$
			this.jid = jid;
		}
		
		public  override bool Equals(System.Object o)
		{
			return o is JabberPlayer && jid.Equals(((JabberPlayer) o).jid);
		}
		
		public override int GetHashCode()
		{
			return jid.GetHashCode();
		}
		
		public override System.String ToString()
		{
			return name + " (" + StringUtils.parseName(jid) + ")"; //$NON-NLS-1$ //$NON-NLS-2$
		}
		
		public virtual void  join(JabberRoom room)
		{
			if (joinedRoom != null)
			{
				joinedRoom.removePlayer(this);
			}
			room.addPlayer(this);
			joinedRoom = room;
		}
		
		public static System.String xmppAddressToJid(System.String participant)
		{
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'address '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String address = StringUtils.parseServer(participant);
			//UPGRADE_NOTE: Final was removed from the declaration of 'parts '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] parts = address.split("\\."); //$NON-NLS-1$
			//UPGRADE_NOTE: Final was removed from the declaration of 'server '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String server = parts[parts.Length - 1];
			//UPGRADE_NOTE: Final was removed from the declaration of 'nick '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String nick = StringUtils.parseResource(participant);
			
			return nick + "@" + server + JabberClient.JID_RESOURCE; //$NON-NLS-1$
		}
		
		public class Manager
		{
			public Manager()
			{
				InitBlock();
			}
			private void  InitBlock()
			{
				return jidToPlayer.values();
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private Map < String, JabberPlayer > jidToPlayer = new HashMap < String, JabberPlayer >();
			
			public virtual JabberPlayer getPlayer(System.String jid)
			{
				if (jid == null)
				{
					return null;
				}
				JabberPlayer p = jidToPlayer.get_Renamed(jid);
				if (p == null)
				{
					p = new JabberPlayer(jid);
					jidToPlayer.put(jid, p);
				}
				return p;
			}
			
			public virtual JabberPlayer getPlayerByLogin(JabberClient client, System.String login)
			{
				return getPlayer(login + "@" + client.getHost() + JabberClient.JID_RESOURCE); //$NON-NLS-1$
			}
			
			//UPGRADE_NOTE: Synchronized keyword was removed from method 'deletePlayer'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
			public virtual void  deletePlayer(System.String jid)
			{
				lock (this)
				{
					jidToPlayer.remove(jid);
				}
			}
			
			//UPGRADE_NOTE: Synchronized keyword was removed from method 'clear'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
			public virtual void  clear()
			{
				lock (this)
				{
					jidToPlayer.clear();
				}
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public Collection < JabberPlayer > getAllPlayers()
		}
	}
}