/*
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
//UPGRADE_TODO: The type 'org.jivesoftware.smack.PacketInterceptor' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using PacketInterceptor = org.jivesoftware.smack.PacketInterceptor;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.XMPPConnection' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using XMPPConnection = org.jivesoftware.smack.XMPPConnection;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.filter.PacketFilter' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using PacketFilter = org.jivesoftware.smack.filter.PacketFilter;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.packet.Packet' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Packet = org.jivesoftware.smack.packet.Packet;
namespace VassalSharp.chat.jabber
{
	
	/// <summary> Convenience class that combines a packet filter and interceptor, with logging of untrapped runtime exceptions</summary>
	/// <author>  rodneykinney
	/// 
	/// </author>
	public abstract class Interceptor : PacketInterceptor, PacketFilter
	{
		public virtual void  interceptPacket(Packet packet)
		{
			try
			{
				intercept(packet);
			}
			// FIXME: review error message
			catch (System.SystemException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
			}
		}
		
		protected internal abstract void  intercept(Packet p);
		
		public virtual bool accept(Packet packet)
		{
			try
			{
				return acceptPacket(packet);
			}
			// FIXME: review error message
			catch (System.SystemException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
				return false;
			}
		}
		
		protected internal abstract bool acceptPacket(Packet packet);
		
		public virtual void  addTo(XMPPConnection conn)
		{
			conn.addPacketWriterInterceptor(this, this);
		}
	}
}