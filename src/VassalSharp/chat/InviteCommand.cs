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
using NodeClient = VassalSharp.chat.node.NodeClient;
using Command = VassalSharp.command.Command;
using Resources = VassalSharp.i18n.Resources;
using Dialogs = VassalSharp.tools.swing.Dialogs;
namespace VassalSharp.chat
{
	
	/// <summary> A {@link Command} that, when executed, sends game synchronization
	/// information to a given {@link VassalSharp.chat.SimplePlayer}
	/// NB InviteCommand is not used by the Jabber Server/Client
	/// 
	/// </summary>
	public class InviteCommand:Command
	{
		virtual public System.String Room
		{
			get
			{
				return room;
			}
			
		}
		virtual public System.String Player
		{
			get
			{
				return player;
			}
			
		}
		virtual public System.String PlayerId
		{
			get
			{
				return playerId;
			}
			
		}
		override public bool Loggable
		{
			get
			{
				return false;
			}
			
		}
		private System.String room;
		private System.String player;
		private System.String playerId;
		private ChatServerConnection client;
		
		public InviteCommand(System.String player, System.String playerId, System.String room, ChatServerConnection client)
		{
			this.player = player;
			this.playerId = playerId;
			this.room = room;
			this.client = client;
		}
		
		public InviteCommand(System.String player, System.String playerId, System.String room):this(player, playerId, room, null)
		{
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override void  executeCommand()
		{
			if (client is NodeClient)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'i '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int i = Dialogs.showConfirmDialog(GameModule.getGameModule().getFrame(), Resources.getString("Chat.invite_heading"), Resources.getString("Chat.invite_heading"), Resources.getString("Chat.invitation", player, room), (int) System.Windows.Forms.MessageBoxIcon.Question, null, (int) System.Windows.Forms.MessageBoxButtons.YesNo, "Invite" + playerId, Resources.getString("Chat.ignore_invitation"));
				
				if (i == 0)
				{
					((NodeClient) client).doInvite(playerId, room);
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