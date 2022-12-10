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
using Player = VassalSharp.chat.Player;
using PrivateChatManager = VassalSharp.chat.PrivateChatManager;
using PrivateChatter = VassalSharp.chat.PrivateChatter;
using SimplePlayer = VassalSharp.chat.SimplePlayer;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.ui
{
	
	/// <summary> When invoked, will open a private message window to another player</summary>
	[Serializable]
	public class PrivateMessageAction:SupportClass.ActionSupport
	{
		private class AnonymousClassPlayerActionFactory : PlayerActionFactory
		{
			public AnonymousClassPlayerActionFactory(VassalSharp.chat.ChatServerConnection client, VassalSharp.chat.PrivateChatManager chatMgr)
			{
				InitBlock(client, chatMgr);
			}
			private void  InitBlock(VassalSharp.chat.ChatServerConnection client, VassalSharp.chat.PrivateChatManager chatMgr)
			{
				this.client = client;
				this.chatMgr = chatMgr;
			}
			//UPGRADE_NOTE: Final variable client was copied into class AnonymousClassPlayerActionFactory. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.chat.ChatServerConnection client;
			//UPGRADE_NOTE: Final variable chatMgr was copied into class AnonymousClassPlayerActionFactory. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.chat.PrivateChatManager chatMgr;
			//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			public virtual SupportClass.ActionSupport getAction(SimplePlayer p, System.Windows.Forms.TreeView tree)
			{
				return new PrivateMessageAction(p, client, chatMgr);
			}
		}
		private const long serialVersionUID = 1L;
		
		private Player p;
		private PrivateChatManager mgr;
		
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		public PrivateMessageAction(Player p, ChatServerConnection client, PrivateChatManager mgr):base(Resources.getString("Chat.private_msg"))
		{ //$NON-NLS-1$
			this.p = p;
			this.mgr = mgr;
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
			setEnabled(p != null && client != null && mgr != null && !p.Equals(client.UserInfo));
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
		{
			PrivateChatter chat = mgr.getChatterFor(p);
			// Chat is null of other player is ignoring us.
			if (chat != null)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
				System.Windows.Forms.Form f = (System.Windows.Forms.Form) chat.getTopLevelAncestor();
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(f, "Visible", true);
				f.BringToFront();
			}
		}
		
		public static PlayerActionFactory factory(ChatServerConnection client, PrivateChatManager chatMgr)
		{
			return new AnonymousClassPlayerActionFactory(client, chatMgr);
		}
	}
}