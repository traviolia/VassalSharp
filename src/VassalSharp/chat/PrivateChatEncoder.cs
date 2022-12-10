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
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.chat
{
	
	/// <summary>  Date: Mar 16, 2003</summary>
	public class PrivateChatEncoder : CommandEncoder
	{
		public const System.String COMMAND_PREFIX = "PRIV_CHAT"; //$NON-NLS-1$
		private PlayerEncoder playerEncoder;
		
		private PrivateChatManager pChatMgr;
		
		public PrivateChatEncoder(PlayerEncoder playerEncoder, PrivateChatManager pChatMgr)
		{
			this.playerEncoder = playerEncoder;
			this.pChatMgr = pChatMgr;
		}
		
		public virtual System.String encode(Command c)
		{
			if (c is PrivMsgCommand)
			{
				PrivMsgCommand cmd = (PrivMsgCommand) c;
				SequenceEncoder se = new SequenceEncoder(COMMAND_PREFIX, '/');
				se.append(playerEncoder.playerToString(cmd.Sender));
				se.append(cmd.Message);
				return se.Value;
			}
			else
			{
				return null;
			}
		}
		
		public virtual Command decode(System.String s)
		{
			if (s.StartsWith(COMMAND_PREFIX))
			{
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(s, '/');
				st.nextToken();
				Player sender = playerEncoder.stringToPlayer(st.nextToken());
				return new PrivMsgCommand(pChatMgr, sender, st.nextToken());
			}
			else
			{
				return null;
			}
		}
	}
}