/*
* $Id$
*
* Copyright (c) 2009 by Brent Easton
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
using HybridClient = VassalSharp.chat.HybridClient;
using Room = VassalSharp.chat.Room;
using ChatServerControls = VassalSharp.chat.ui.ChatServerControls;
using LockableRoomControls = VassalSharp.chat.ui.LockableRoomControls;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.jabber
{
	
	public class LockableJabberRoomControls:LockableRoomControls
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		new private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(LockableJabberRoomControls enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(LockableJabberRoomControls enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private LockableJabberRoomControls enclosingInstance;
			public LockableJabberRoomControls Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				if (Enclosing_Instance.client.isConnected())
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'props '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
					System.Collections.Specialized.NameValueCollection props = JabberRoom.configureNewRoom();
					if (props != null)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String name = props.Get(JabberRoom.CONFIG_NAME);
						if (name.Length > 0)
						{
							Enclosing_Instance.createRoom(props);
						}
					}
				}
				else
				{
					GameModule.getGameModule().warn(Resources.getString("Chat.must_connect")); //$NON-NLS-1$
				}
			}
		}
		private JabberClient JabberClient
		{
			get
			{
				JabberClient c = null;
				if (client is JabberClient)
				{
					c = (JabberClient) client;
				}
				else if (client is HybridClient && ((HybridClient) client).Delegate is JabberClient)
				{
					c = (JabberClient) ((HybridClient) client).Delegate;
				}
				return c;
			}
			
		}
		
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		protected internal ActionListener extendedRoomCreator;
		
		public override void  initializeControls(ChatServerControls controls)
		{
			base.initializeControls(controls);
			extendedRoomCreator = new AnonymousClassActionListener(this);
			controls.addExtendedNewRoomHandler(extendedRoomCreator);
		}
		
		public override void  uninitializeControls(ChatServerControls controls)
		{
			base.uninitializeControls(controls);
			controls.removeExtendedNewRoomHandler(extendedRoomCreator);
		}
		
		public LockableJabberRoomControls(JabberClient client):base((ChatServerConnection) client)
		{
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		protected internal override void  addLockRoomAction(System.Windows.Forms.ContextMenu popup, Room target)
		{
			JabberClient c = JabberClient;
			if (c != null)
			{
				System.Windows.Forms.MenuItem temp_MenuItem;
				temp_MenuItem = new System.Windows.Forms.MenuItem();
				temp_MenuItem.Click += new System.EventHandler(new LockRoomAction(this, (JabberRoom) target, c).actionPerformed);
				popup.MenuItems.Add(temp_MenuItem);
				System.Windows.Forms.MenuItem generatedAux = temp_MenuItem;
			}
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public override System.Windows.Forms.ContextMenu buildPopupForRoom(Room target, System.Windows.Forms.TreeView tree)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'popup '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Windows.Forms.ContextMenu popup = base.buildPopupForRoom(target, tree);
			System.Windows.Forms.MenuItem temp_MenuItem;
			temp_MenuItem = new System.Windows.Forms.MenuItem();
			temp_MenuItem.Click += new System.EventHandler(new RoomPropertiesAction(this, (JabberRoom) target).actionPerformed);
			popup.MenuItems.Add(temp_MenuItem);
			System.Windows.Forms.MenuItem generatedAux = temp_MenuItem;
			return popup;
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		protected internal virtual void  createRoom(System.Collections.Specialized.NameValueCollection props)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'room '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			JabberRoom room = ((JabberClient) client).getRoomByName(props.Get(JabberRoom.CONFIG_NAME));
			room.Config = props;
			client.setRoom(room);
		}
		
		protected internal override void  createRoom(System.String name)
		{
			if (client.isConnected())
			{
				if (name != null && name.Length > 0)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'room '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					JabberRoom room = ((JabberClient) client).getRoomByName(name);
					client.setRoom(room);
				}
			}
			else
			{
				GameModule.getGameModule().warn(Resources.getString("Chat.must_connect")); //$NON-NLS-1$
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'LockRoomAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		internal class LockRoomAction:SupportClass.ActionSupport
		{
			private void  InitBlock(LockableJabberRoomControls enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private LockableJabberRoomControls enclosingInstance;
			public LockableJabberRoomControls Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			private JabberClient client;
			private JabberRoom target;
			
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			public LockRoomAction(LockableJabberRoomControls enclosingInstance, JabberRoom target, JabberClient client):base(target.Locked?Resources.getString("Chat.unlock_room"):Resources.getString("Chat.lock_room"))
			{
				InitBlock(enclosingInstance); //$NON-NLS-1$
				
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
				setEnabled(target.OwnedByMe && !target.getName().Equals(client.getDefaultRoomName()));
				this.target = target;
				this.client = client;
			}
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				client.lockRoom(target);
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'RoomPropertiesAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		internal class RoomPropertiesAction:SupportClass.ActionSupport
		{
			private void  InitBlock(LockableJabberRoomControls enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private LockableJabberRoomControls enclosingInstance;
			public LockableJabberRoomControls Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			private JabberRoom target;
			
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			public RoomPropertiesAction(LockableJabberRoomControls enclosingInstance, JabberRoom room):base(Resources.getString("General.properties"))
			{
				InitBlock(enclosingInstance); //$NON-NLS-1$
				target = room;
			}
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				target.showConfig();
			}
		}
	}
}