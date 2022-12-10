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
using Player = VassalSharp.chat.Player;
using Room = VassalSharp.chat.Room;
using SimplePlayer = VassalSharp.chat.SimplePlayer;
using SimpleRoom = VassalSharp.chat.SimpleRoom;
using SimpleStatus = VassalSharp.chat.SimpleStatus;
namespace VassalSharp.chat.ui
{
	
	/// <summary>Cell render component for {@link RoomTree} </summary>
	//UPGRADE_ISSUE: Class 'javax.swing.tree.DefaultTreeCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
	[Serializable]
	public class RoomTreeRenderer:DefaultTreeCellRenderer
	{
		private const long serialVersionUID = 1L;
		
		private System.Drawing.Image away;
		private System.Drawing.Image looking;
		
		public RoomTreeRenderer()
		{
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			System.Uri image = new System.Uri(System.IO.Path.GetFullPath("/images/playerAway.gif")); //$NON-NLS-1$
			if (image != null)
			{
				//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
				away = new ImageIcon(image);
			}
			
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			image = new System.Uri(System.IO.Path.GetFullPath("/images/playerLooking.gif")); //$NON-NLS-1$
			if (image != null)
			{
				//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
				looking = new ImageIcon(image);
			}
		}
		
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public System.Windows.Forms.Control getTreeCellRendererComponent(System.Windows.Forms.TreeView tree, System.Object value_Renamed, bool sel, bool expanded, bool leaf, int row, bool hasFocus)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.tree.DefaultTreeCellRenderer.getTreeCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
			base.getTreeCellRendererComponent(tree, value_Renamed, sel, expanded, leaf, row, hasFocus);
			
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.putClientProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentputClientProperty_javalangObject_javalangObject'"
			putClientProperty("html.disable", true); //$NON-NLS-1$
			
			System.Object item = ((System.Windows.Forms.TreeNode) value_Renamed).Tag;
			if (item is Player)
			{
				if (((SimpleStatus) ((Player) item).getStatus()).Away)
				{
					Image = away;
				}
				else if (((SimpleStatus) ((SimplePlayer) item).getStatus()).Looking)
				{
					Image = looking;
				}
				else
				{
					Image = null;
				}
			}
			else if (item is SimpleRoom)
			{
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				Text = Text + " (" + players.size() + ")"; //$NON-NLS-1$ //$NON-NLS-2$
			}
			return this;
		}
	}
}