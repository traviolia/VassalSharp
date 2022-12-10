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
using ChatServerConnection = VassalSharp.chat.ChatServerConnection;
using HybridClient = VassalSharp.chat.HybridClient;
using Room = VassalSharp.chat.Room;
using LockableRoomControls = VassalSharp.chat.ui.LockableRoomControls;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.node
{
	
	public class LockableNodeRoomControls:LockableRoomControls
	{
		private NodeClient NodeClient
		{
			get
			{
				NodeClient c = null;
				if (client is NodeClient)
				{
					c = (NodeClient) client;
				}
				else if (client is HybridClient && ((HybridClient) client).Delegate is NodeClient)
				{
					c = (NodeClient) ((HybridClient) client).Delegate;
				}
				return c;
			}
			
		}
		
		public LockableNodeRoomControls(NodeClient client):base((ChatServerConnection) client)
		{
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		protected internal override void  addLockRoomAction(System.Windows.Forms.ContextMenu popup, Room target)
		{
			NodeClient c = NodeClient;
			if (c != null)
			{
				System.Windows.Forms.MenuItem temp_MenuItem;
				temp_MenuItem = new System.Windows.Forms.MenuItem();
				temp_MenuItem.Click += new System.EventHandler(new LockRoomAction(this, (NodeRoom) target, c).actionPerformed);
				popup.MenuItems.Add(temp_MenuItem);
				System.Windows.Forms.MenuItem generatedAux = temp_MenuItem;
			}
		}
		
		protected internal override void  createRoom(System.String name)
		{
			Room existing = null;
			Room[] rooms = client.getAvailableRooms();
			for (int i = 0; existing == null && i < rooms.Length; i++)
			{
				if (rooms[i].getName().Equals(name))
				{
					existing = rooms[i];
				}
			}
			NodeClient nodeClient = NodeClient;
			if (existing is NodeRoom)
			{
				// Join existing room if it is not locked
				if (!((NodeRoom) existing).Locked)
				{
					client.setRoom(existing);
				}
			}
			else if (existing == null && nodeClient != null)
			{
				// If running hierarchical server, create new room and set myself as the owner
				NodeRoom room = new NodeRoom(name);
				room.setOwner(nodeClient.MyInfo.getId());
				client.setRoom(room);
				nodeClient.sendRoomInfo(room);
			}
			else
			{
				// Default behavior
				base.createRoom(name);
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'LockRoomAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		internal class LockRoomAction:SupportClass.ActionSupport
		{
			private void  InitBlock(LockableNodeRoomControls enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private LockableNodeRoomControls enclosingInstance;
			public LockableNodeRoomControls Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			private NodeClient client;
			private NodeRoom target;
			
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			public LockRoomAction(LockableNodeRoomControls enclosingInstance, NodeRoom target, NodeClient client):base(target.Locked?Resources.getString("Chat.unlock_room"):Resources.getString("Chat.lock_room"))
			{
				InitBlock(enclosingInstance);
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
				setEnabled(client.MyInfo.getId().Equals(target.getOwner()) && !target.getName().Equals(client.getDefaultRoomName()));
				this.target = target;
				this.client = client;
			}
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				client.lockRoom(target);
			}
		}
	}
}