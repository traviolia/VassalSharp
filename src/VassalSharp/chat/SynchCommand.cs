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
using GameModule = VassalSharp.build.GameModule;
using Command = VassalSharp.command.Command;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat
{
	
	/// <summary> A {@link Command} that, when executed, sends game synchronization
	/// information to a given {@link VassalSharp.chat.SimplePlayer}
	/// 
	/// </summary>
	public class SynchCommand:Command
	{
		virtual public Player Player
		{
			get
			{
				return recipient;
			}
			
		}
		/// <summary> Don't log synchronization requests </summary>
		override public bool Loggable
		{
			get
			{
				return false;
			}
			
		}
		private Player recipient;
		private ChatServerConnection client;
		
		public SynchCommand(Player p, ChatServerConnection client)
		{
			recipient = p;
			this.client = client;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override void  executeCommand()
		{
			if (recipient != null)
			{
				GameModule.getGameModule().warn(Resources.getString("Server.sending_game_info", recipient.getName())); //$NON-NLS-1$
				Command synch = GameModule.getGameModule().getGameState().getRestoreCommand();
				if (synch != null)
				{
					client.sendTo(recipient, synch);
				}
			}
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override Command myUndoCommand()
		{
			return null;
		}
	}
}