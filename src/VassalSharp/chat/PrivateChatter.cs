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
using GameModule = VassalSharp.build.GameModule;
using Chatter = VassalSharp.build.module.Chatter;
using ServerConnection = VassalSharp.build.module.ServerConnection;
namespace VassalSharp.chat
{
	
	/// <summary> A window for private messages between the user and another player;</summary>
	[Serializable]
	public class PrivateChatter:Chatter
	{
		virtual public Player Player
		{
			get
			{
				return other;
			}
			
		}
		virtual public ServerConnection Client
		{
			get
			{
				return client;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		private ChatServerConnection client;
		private Player other;
		
		public PrivateChatter(Player other, ChatServerConnection client)
		{
			this.other = other;
			this.client = client;
		}
		
		/// <deprecated> 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public override System.String getHandle()
		{
			return GameModule.getGameModule().getChatter().getHandle();
		}
		
		public override void  send(System.String msg)
		{
			if (msg != null && msg.Length > 0)
			{
				show(msg);
				PrivMsgCommand c = new PrivMsgCommand(null, client.UserInfo, msg);
				client.sendTo(other, c);
			}
		}
	}
}