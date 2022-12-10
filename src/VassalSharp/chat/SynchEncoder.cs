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
namespace VassalSharp.chat
{
	
	/// <summary> Date: Mar 16, 2003</summary>
	public class SynchEncoder : CommandEncoder
	{
		public const System.String COMMAND_PREFIX = "SYNC"; //$NON-NLS-1$
		private PlayerEncoder playerEncoder;
		private ChatServerConnection client;
		
		public SynchEncoder(PlayerEncoder playerEncoder, ChatServerConnection client)
		{
			this.playerEncoder = playerEncoder;
			this.client = client;
		}
		
		public virtual Command decode(System.String s)
		{
			if (s.StartsWith(COMMAND_PREFIX))
			{
				Player p = playerEncoder.stringToPlayer(s.Substring(COMMAND_PREFIX.Length));
				return new SynchCommand(p, client);
			}
			else
			{
				return null;
			}
		}
		
		public virtual System.String encode(Command c)
		{
			if (c is SynchCommand)
			{
				SynchCommand cmd = (SynchCommand) c;
				return COMMAND_PREFIX + playerEncoder.playerToString(cmd.Player);
			}
			else
			{
				return null;
			}
		}
	}
}