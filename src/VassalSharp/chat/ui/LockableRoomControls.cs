/*
* $Id$
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
using LockableRoom = VassalSharp.chat.LockableRoom;
using Room = VassalSharp.chat.Room;
namespace VassalSharp.chat.ui
{
	
	
	/// <author>  rkinney
	/// </author>
	public abstract class LockableRoomControls:RoomInteractionControlsInitializer
	{
		
		protected internal JoinRoomAction joinAction;
		
		public LockableRoomControls(ChatServerConnection client):base(client)
		{
		}
		
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public override void  doubleClickRoom(Room room, System.Windows.Forms.TreeView tree)
		{
			if (!(room is LockableRoom) || !((LockableRoom) room).Locked)
			{
				base.doubleClickRoom(room, tree);
			}
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public override System.Windows.Forms.ContextMenu buildPopupForRoom(Room target, System.Windows.Forms.TreeView tree)
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Windows.Forms.ContextMenu popup = new System.Windows.Forms.ContextMenu();
			addJoinRoomAction(popup, target);
			addLockRoomAction(popup, target);
			return popup;
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		protected internal virtual void  addJoinRoomAction(System.Windows.Forms.ContextMenu popup, Room target)
		{
			joinAction = new JoinRoomAction(target, client);
			System.Windows.Forms.MenuItem temp_MenuItem;
			temp_MenuItem = new System.Windows.Forms.MenuItem();
			temp_MenuItem.Click += new System.EventHandler(joinAction.actionPerformed);
			popup.MenuItems.Add(temp_MenuItem);
			System.Windows.Forms.MenuItem generatedAux = temp_MenuItem;
			if (target is LockableRoom)
			{
				LockableRoom nr = (LockableRoom) target;
				if (nr.Locked)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
					joinAction.setEnabled(false);
				}
			}
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		protected internal abstract void  addLockRoomAction(System.Windows.Forms.ContextMenu popup, Room target);
	}
}