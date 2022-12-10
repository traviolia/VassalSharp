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
	public class InviteEncoder : CommandEncoder
	{
		public const System.String COMMAND_PREFIX = "INVITE\t"; //$NON-NLS-1$
		private ChatServerConnection client;
		
		public InviteEncoder(ChatServerConnection client)
		{
			this.client = client;
		}
		
		public virtual Command decode(System.String s)
		{
			if (s.StartsWith(COMMAND_PREFIX))
			{
				System.String[] info = s.split("\\t"); //$NON-NLS-1$
				if (info.Length == 4)
				{
					return new InviteCommand(info[1], info[2], info[3], client);
				}
			}
			return null;
		}
		
		public virtual System.String encode(Command c)
		{
			if (c is InviteCommand)
			{
				InviteCommand cmd = (InviteCommand) c;
				return COMMAND_PREFIX + cmd.Player + "\t" + cmd.PlayerId + "\t" + cmd.Room; //$NON-NLS-1$ //$NON-NLS-2$
			}
			else
			{
				return null;
			}
		}
	}
}