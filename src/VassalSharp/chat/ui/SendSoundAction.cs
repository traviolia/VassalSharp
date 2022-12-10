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
using SoundEncoder = VassalSharp.chat.SoundEncoder;
using SoundConfigurer = VassalSharp.configure.SoundConfigurer;
using Resources = VassalSharp.i18n.Resources;
using Prefs = VassalSharp.preferences.Prefs;
namespace VassalSharp.chat.ui
{
	
	/// <summary> Send a wake-up sound to another player
	/// - Can't wake-up oneself
	/// - No wake-ups in the default room
	/// - No wake-ups to people in different rooms
	/// - No wake-up to the same person in the same room until at least 5 seconds has passed.
	/// </summary>
	[Serializable]
	public class SendSoundAction:SupportClass.ActionSupport
	{
		private class AnonymousClassPlayerActionFactory : PlayerActionFactory
		{
			public AnonymousClassPlayerActionFactory(VassalSharp.chat.ChatServerConnection client, System.String name, System.String soundKey)
			{
				InitBlock(client, name, soundKey);
			}
			private void  InitBlock(VassalSharp.chat.ChatServerConnection client, System.String name, System.String soundKey)
			{
				this.client = client;
				this.name = name;
				this.soundKey = soundKey;
			}
			//UPGRADE_NOTE: Final variable client was copied into class AnonymousClassPlayerActionFactory. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.chat.ChatServerConnection client;
			//UPGRADE_NOTE: Final variable name was copied into class AnonymousClassPlayerActionFactory. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.String name;
			//UPGRADE_NOTE: Final variable soundKey was copied into class AnonymousClassPlayerActionFactory. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.String soundKey;
			//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			public virtual SupportClass.ActionSupport getAction(SimplePlayer p, System.Windows.Forms.TreeView tree)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Room r = client.getRoom();
				if (client is LockableChatServerConnection && ((LockableChatServerConnection) client).isDefaultRoom(r))
				{
					return null;
				}
				return new SendSoundAction(name, client, soundKey, p);
			}
		}
		private const long serialVersionUID = 1L;
		private static Room lastRoom;
		private static Player lastPlayer;
		private static long lastSound = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
		
		private ChatServerConnection client;
		private Player target;
		private System.String soundKey;
		
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		public SendSoundAction(System.String name, ChatServerConnection client, System.String soundKey, Player target):base(name)
		{
			this.client = client;
			this.soundKey = soundKey;
			this.target = target;
			
			// Find which room our target player is in
			Room targetRoom = null;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Room room: client.getAvailableRooms())
			{
				if (room.getPlayerList().contains(target))
				{
					targetRoom = room;
				}
			}
			
			if (target != null && GameModule.getGameModule() != null && !target.Equals(client.UserInfo) && client.getRoom() != null && client.getRoom().Equals(targetRoom) && (!targetRoom.Equals(lastRoom) || !target.Equals(lastPlayer) || ((System.DateTime.Now.Ticks - 621355968000000000) / 10000 - lastSound) > SoundEncoder.Cmd.TOO_SOON))
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
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			client.sendTo(target, new SoundEncoder.Cmd(soundKey, client.UserInfo));
			lastPlayer = target;
			lastRoom = client.getRoom();
			lastSound = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
		}
		
		public static PlayerActionFactory factory(ChatServerConnection client, System.String name, System.String soundKey, System.String defaultSoundFile)
		{
			if (GameModule.getGameModule() != null)
			{
				Prefs.GlobalPrefs.addOption(Resources.getString("Prefs.sounds_tab"), new SoundConfigurer(soundKey, name, defaultSoundFile)); //$NON-NLS-1$
			}
			return new AnonymousClassPlayerActionFactory(client, name, soundKey);
		}
	}
}