/*
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
//UPGRADE_TODO: The type 'org.jivesoftware.smack.packet.Presence' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Presence = org.jivesoftware.smack.packet.Presence;
using SimpleStatus = VassalSharp.chat.SimpleStatus;
namespace VassalSharp.chat.jabber
{
	
	public class JabberStatus:SimpleStatus
	{
		virtual public Presence.Mode Availability
		{
			get
			{
				Presence.Mode mode = Presence.Mode.away;
				if (Away)
				{
					mode = Presence.Mode.xa;
				}
				else if (Looking)
				{
					mode = Presence.Mode.chat;
				}
				return mode;
			}
			
		}
		public JabberStatus(SimpleStatus copy):base(copy.Looking, copy.Away, null)
		{
		}
	}
}