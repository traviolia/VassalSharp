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
using ChatServerConnection = VassalSharp.chat.ChatServerConnection;
using LockableChatServerConnection = VassalSharp.chat.LockableChatServerConnection;
using Player = VassalSharp.chat.Player;
using Room = VassalSharp.chat.Room;
using SimplePlayer = VassalSharp.chat.SimplePlayer;
using SynchCommand = VassalSharp.chat.SynchCommand;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.ui
{
	
	/// <summary> When invoked, will request synchronization info from another player
	/// - Cannot Synch when in the default room
	/// - Cannot Synch with a player in a different room
	/// - Cannot Synch with yourself
	/// - Cannot Synch with any player in the same room within 15 seconds of your last synch in this room
	/// </summary>
	[Serializable]
	public class SynchAction:SupportClass.ActionSupport
	{
		private class AnonymousClassPlayerActionFactory : PlayerActionFactory
		{
			public AnonymousClassPlayerActionFactory(VassalSharp.chat.ChatServerConnection client)
			{
				InitBlock(client);
			}
			private void  InitBlock(VassalSharp.chat.ChatServerConnection client)
			{
				this.client = client;
			}
			//UPGRADE_NOTE: Final variable client was copied into class AnonymousClassPlayerActionFactory. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.chat.ChatServerConnection client;
			//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			public virtual SupportClass.ActionSupport getAction(SimplePlayer p, System.Windows.Forms.TreeView tree)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Room r = client.getRoom();
				if (client is LockableChatServerConnection && ((LockableChatServerConnection) client).isDefaultRoom(r))
				{
					return null;
				}
				return new SynchAction(p, client);
			}
		}
		private const long serialVersionUID = 1L;
		private const long TOO_SOON = 15 * 1000;
		private static Room lastRoom;
		private static long lastSync = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
		
		private Player p;
		private ChatServerConnection client;
		private Room targetRoom;
		
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		public SynchAction(Player p, ChatServerConnection client):base(Resources.getString("Chat.synchronize"))
		{ //$NON-NLS-1$
			this.p = p;
			this.client = client;
			
			// Find which room our target player is in
			targetRoom = null;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Room room: client.getAvailableRooms())
			{
				if (room.getPlayerList().contains(p))
				{
					targetRoom = room;
				}
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'now '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			long now = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
			
			if (p != null && GameModule.getGameModule() != null && !p.Equals(client.UserInfo) && client.getRoom() != null && client.getRoom().Equals(targetRoom) && (!targetRoom.Equals(lastRoom) || (now - lastSync) > TOO_SOON))
			{
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
				setEnabled(true);
			}
			else
			{
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
				setEnabled(false);
			}
		}
		
		public static void  clearSynchRoom()
		{
			lastRoom = null;
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.isEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionisEnabled'"
			if (isEnabled())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'now '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				long now = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
				if (!targetRoom.Equals(lastRoom) || (now - lastSync) > TOO_SOON)
				{
					GameModule.getGameModule().getGameState().setup(false);
					client.sendTo(p, new SynchCommand(client.UserInfo, client));
					lastSync = now;
				}
				lastRoom = targetRoom;
			}
		}
		
		
		public static PlayerActionFactory factory(ChatServerConnection client)
		{
			return new AnonymousClassPlayerActionFactory(client);
		}
	}
}