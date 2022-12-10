/*
* $Id$
*
* Copyright (c) 2004-2009 by Rodney Kinney, Brent Easton
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
using Player = VassalSharp.chat.Player;
using Room = VassalSharp.chat.Room;
namespace VassalSharp.chat.ui
{
	
	/// <summary> Renders rooms with a "locked" icon if locked
	/// Change Owners name to display Red
	/// </summary>
	[Serializable]
	public class LockableRoomTreeRenderer:RoomTreeRenderer
	{
		private const long serialVersionUID = 1L;
		private System.Drawing.Image lockedIcon;
		private System.Drawing.Font nonOwnerFont = null;
		private System.Drawing.Font ownerFont = null;
		public LockableRoomTreeRenderer()
		{
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			System.Uri image = new System.Uri(System.IO.Path.GetFullPath("/images/lockedRoom.gif")); //$NON-NLS-1$
			if (image != null)
			{
				//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
				lockedIcon = new ImageIcon(image);
			}
		}
		
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public override System.Windows.Forms.Control getTreeCellRendererComponent(System.Windows.Forms.TreeView tree, System.Object value_Renamed, bool sel, bool expanded, bool leaf, int row, bool hasFocus)
		{
			System.Windows.Forms.Label l = (System.Windows.Forms.Label) base.getTreeCellRendererComponent(tree, value_Renamed, sel, expanded, leaf, row, hasFocus);
			System.Object item = ((System.Windows.Forms.TreeNode) value_Renamed).Tag;
			
			if (item is LockableRoom)
			{
				if (lockedIcon != null && ((LockableRoom) item).Locked)
				{
					l.Image = lockedIcon;
				}
			}
			else if (item is Player)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'roomNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.TreeNode roomNode = (System.Windows.Forms.TreeNode) ((System.Windows.Forms.TreeNode) value_Renamed).Parent;
				//UPGRADE_NOTE: Final was removed from the declaration of 'room '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Object room = roomNode.Tag;
				if (room is LockableRoom)
				{
					if (!VassalSharp.chat.ChatServerConnection_Fields.DEFAULT_ROOM_NAME.Equals(((Room) room).getName()) && ((LockableRoom) room).isOwner(((Player) item).getId()))
					{
						if (ownerFont == null)
						{
							nonOwnerFont = this.Font;
							//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
							ownerFont = new System.Drawing.Font(nonOwnerFont.Name, (int) nonOwnerFont.Size, (System.Drawing.FontStyle) ((int) nonOwnerFont.Style + (int) System.Drawing.FontStyle.Bold));
						}
						//UPGRADE_ISSUE: Method 'javax.swing.tree.DefaultTreeCellRenderer.setFont' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
						setFont(ownerFont);
						return l;
					}
				}
			}
			//UPGRADE_ISSUE: Method 'javax.swing.tree.DefaultTreeCellRenderer.setFont' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
			setFont(nonOwnerFont);
			return l;
		}
	}
}