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
using ChatServerConnection = VassalSharp.chat.ChatServerConnection;
using Room = VassalSharp.chat.Room;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.ui
{
	
	/// <summary> When invoked, will join a game room on the server</summary>
	[Serializable]
	public class JoinRoomAction:SupportClass.ActionSupport
	{
		private class AnonymousClassRoomActionFactory : RoomActionFactory
		{
			public AnonymousClassRoomActionFactory(VassalSharp.chat.ChatServerConnection chatClient)
			{
				InitBlock(chatClient);
			}
			private void  InitBlock(VassalSharp.chat.ChatServerConnection chatClient)
			{
				this.chatClient = chatClient;
			}
			//UPGRADE_NOTE: Final variable chatClient was copied into class AnonymousClassRoomActionFactory. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.chat.ChatServerConnection chatClient;
			//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			public virtual SupportClass.ActionSupport getAction(Room p, System.Windows.Forms.TreeView tree)
			{
				return new JoinRoomAction(p, chatClient);
			}
		}
		private const long serialVersionUID = 1L;
		
		private Room r;
		private ChatServerConnection client;
		
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		public JoinRoomAction(Room r, ChatServerConnection client):base(Resources.getString("Chat.join_room"))
		{ //$NON-NLS-1$
			this.r = r;
			this.client = client;
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
			setEnabled(r != null && !r.Equals(client.getRoom()));
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
		{
			client.setRoom(r);
		}
		
		public static RoomActionFactory factory(ChatServerConnection chatClient)
		{
			return new AnonymousClassRoomActionFactory(chatClient);
		}
	}
}