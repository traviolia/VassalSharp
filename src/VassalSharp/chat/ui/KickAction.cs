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
using LockableChatServerConnection = VassalSharp.chat.LockableChatServerConnection;
using LockableRoom = VassalSharp.chat.LockableRoom;
using Room = VassalSharp.chat.Room;
using SimplePlayer = VassalSharp.chat.SimplePlayer;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.ui
{
	
	/// <summary> When invoked, will Kick another player out of his current room back to the Main Room.</summary>
	[Serializable]
	public class KickAction:SupportClass.ActionSupport
	{
		private class AnonymousClassPlayerActionFactory : PlayerActionFactory
		{
			public AnonymousClassPlayerActionFactory(VassalSharp.chat.LockableChatServerConnection client)
			{
				InitBlock(client);
			}
			private void  InitBlock(VassalSharp.chat.LockableChatServerConnection client)
			{
				this.client = client;
			}
			//UPGRADE_NOTE: Final variable client was copied into class AnonymousClassPlayerActionFactory. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.chat.LockableChatServerConnection client;
			//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			public virtual SupportClass.ActionSupport getAction(SimplePlayer p, System.Windows.Forms.TreeView tree)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Room r = client.getRoom();
				if (r is LockableRoom && !((LockableRoom) r).Locked)
				{
					return null;
				}
				return new KickAction(client, p);
			}
		}
		private const long serialVersionUID = 1L;
		
		private SimplePlayer kickee;
		private LockableChatServerConnection client;
		
		
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		public KickAction(LockableChatServerConnection client, SimplePlayer target):base(Resources.getString("Chat.kick"))
		{ //$NON-NLS-1$
			this.kickee = target;
			this.client = client;
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
			setEnabled(client.isKickable(kickee));
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.isEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionisEnabled'"
			if (isEnabled())
			{
				client.doKick(kickee);
				GameModule.getGameModule().warn(Resources.getString("Chat.kick_sent", kickee.getName())); //$NON-NLS-1$
			}
		}
		
		public static PlayerActionFactory factory(LockableChatServerConnection client)
		{
			return new AnonymousClassPlayerActionFactory(client);
		}
	}
}