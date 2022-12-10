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
using Player = VassalSharp.chat.Player;
using Room = VassalSharp.chat.Room;
using SimplePlayer = VassalSharp.chat.SimplePlayer;
using SimpleRoom = VassalSharp.chat.SimpleRoom;
namespace VassalSharp.chat.ui
{
	
	/// <summary> Adds mouse listeners to the RoomTree components: double-click to join a room, etc. Builds a popup when right-clicking
	/// on a player or room
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class RoomInteractionControlsInitializer : ChatControlsInitializer
	{
		static private System.Int32 state395;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMouseAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassMouseAdapter
		{
			public AnonymousClassMouseAdapter(RoomInteractionControlsInitializer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(RoomInteractionControlsInitializer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private RoomInteractionControlsInitializer enclosingInstance;
			public RoomInteractionControlsInitializer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs evt)
			{
				//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Windows.Forms.TreeView tree = (System.Windows.Forms.TreeView) event_sender;
				//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
				if (evt.isMetaDown())
				{
					//UPGRADE_ISSUE: Class 'javax.swing.tree.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
					//UPGRADE_ISSUE: Method 'javax.swing.JTree.getPathForLocation' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreegetPathForLocation_int_int'"
					TreePath path = tree.getPathForLocation(evt.X, evt.Y);
					if (path != null)
					{
						//UPGRADE_ISSUE: Method 'javax.swing.tree.TreePath.getLastPathComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
						System.Object target = ((System.Windows.Forms.TreeNode) path.getLastPathComponent()).Tag;
						if (target is Player)
						{
							//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
							System.Windows.Forms.ContextMenu popup = Enclosing_Instance.buildPopupForPlayer((SimplePlayer) target, tree);
							if (popup != null)
							{
								for (int i = 0, n = (int) popup.Controls.Count; i < n; ++i)
								{
									popup.Controls[i].Font = VassalSharp.chat.ui.RoomInteractionControlsInitializer.POPUP_MENU_FONT;
								}
								//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.show' was converted to 'System.Windows.Forms.ContextMenu.Show' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenushow_javaawtComponent_int_int'"
								popup.Show(tree, new System.Drawing.Point(evt.X, evt.Y));
							}
						}
					}
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMouseAdapter1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassMouseAdapter1
		{
			public AnonymousClassMouseAdapter1(RoomInteractionControlsInitializer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(RoomInteractionControlsInitializer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private RoomInteractionControlsInitializer enclosingInstance;
			public RoomInteractionControlsInitializer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs evt)
			{
				//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Windows.Forms.TreeView tree = (System.Windows.Forms.TreeView) event_sender;
				//UPGRADE_ISSUE: Class 'javax.swing.tree.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
				//UPGRADE_ISSUE: Method 'javax.swing.JTree.getPathForLocation' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreegetPathForLocation_int_int'"
				TreePath path = tree.getPathForLocation(evt.X, evt.Y);
				if (path != null)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.tree.TreePath.getLastPathComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
					System.Object target = ((System.Windows.Forms.TreeNode) path.getLastPathComponent()).Tag;
					if (target is Player)
					{
						//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
						if (evt.isMetaDown())
						{
							//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
							System.Windows.Forms.ContextMenu popup = Enclosing_Instance.buildPopupForPlayer((SimplePlayer) target, tree);
							for (int i = 0, n = (int) popup.Controls.Count; i < n; ++i)
							{
								popup.Controls[i].Font = VassalSharp.chat.ui.RoomInteractionControlsInitializer.POPUP_MENU_FONT;
							}
							//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.show' was converted to 'System.Windows.Forms.ContextMenu.Show' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenushow_javaawtComponent_int_int'"
							popup.Show(tree, new System.Drawing.Point(evt.X, evt.Y));
						}
					}
					else if (target is SimpleRoom)
					{
						//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
						if (evt.isMetaDown())
						{
							//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
							System.Windows.Forms.ContextMenu popup = Enclosing_Instance.buildPopupForRoom((VassalSharp.chat.Room) target, tree);
							if (popup != null)
							{
								for (int i = 0, n = (int) popup.Controls.Count; i < n; ++i)
								{
									popup.Controls[i].Font = VassalSharp.chat.ui.RoomInteractionControlsInitializer.POPUP_MENU_FONT;
								}
								//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.show' was converted to 'System.Windows.Forms.ContextMenu.Show' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenushow_javaawtComponent_int_int'"
								popup.Show(tree, new System.Drawing.Point(evt.X, evt.Y));
							}
						}
						else if (evt.Clicks == 2)
						{
							//UPGRADE_ISSUE: Method 'javax.swing.JTree.getRowForLocation' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreegetRowForLocation_int_int'"
							int row = tree.getRowForLocation(evt.X, evt.Y);
							//UPGRADE_ISSUE: Method 'javax.swing.JTree.isCollapsed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreeisCollapsed_int'"
							if (tree.isCollapsed(row))
							{
								//UPGRADE_ISSUE: Method 'javax.swing.JTree.expandRow' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreeexpandRow_int'"
								tree.expandRow(row);
							}
							else
							{
								//UPGRADE_ISSUE: Method 'javax.swing.JTree.collapseRow' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreecollapseRow_int'"
								tree.collapseRow(row);
							}
							Enclosing_Instance.doubleClickRoom((VassalSharp.chat.Room) target, tree);
						}
					}
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(VassalSharp.chat.ui.ChatServerControls controls, RoomInteractionControlsInitializer enclosingInstance)
			{
				InitBlock(controls, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.chat.ui.ChatServerControls controls, RoomInteractionControlsInitializer enclosingInstance)
			{
				this.controls = controls;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable controls was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.chat.ui.ChatServerControls controls;
			private RoomInteractionControlsInitializer enclosingInstance;
			public RoomInteractionControlsInitializer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.createRoom(controls.NewRoom.Text);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				controls.NewRoom.Text = ""; //$NON-NLS-1$
			}
		}
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state395 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'POPUP_MENU_FONT '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
		public static readonly System.Drawing.Font POPUP_MENU_FONT = new System.Drawing.Font("Dialog", 10, System.Drawing.FontStyle.Regular); //$NON-NLS-1$
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < PlayerActionFactory > playerActionFactories = 
		new ArrayList < PlayerActionFactory >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < RoomActionFactory > roomActionFactories = 
		new ArrayList < RoomActionFactory >();
		protected internal ChatServerConnection client;
		//UPGRADE_TODO: Class 'java.awt.event.MouseAdapter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		private MouseAdapter currentRoomPopupBuilder;
		//UPGRADE_TODO: Class 'java.awt.event.MouseAdapter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		private MouseAdapter roomPopupBuilder;
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		private ActionListener roomCreator;
		
		public RoomInteractionControlsInitializer(ChatServerConnection client):base()
		{
			this.client = client;
		}
		
		public virtual void  initializeControls(ChatServerControls controls)
		{
			currentRoomPopupBuilder = new AnonymousClassMouseAdapter(this);
			controls.CurrentRoom.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.chat.ui.RoomInteractionControlsInitializer.mouseDown);
			controls.CurrentRoom.Click += new System.EventHandler(currentRoomPopupBuilder.mouseClicked);
			controls.CurrentRoom.MouseEnter += new System.EventHandler(currentRoomPopupBuilder.mouseEntered);
			controls.CurrentRoom.MouseLeave += new System.EventHandler(currentRoomPopupBuilder.mouseExited);
			controls.CurrentRoom.MouseDown += new System.Windows.Forms.MouseEventHandler(currentRoomPopupBuilder.mousePressed);
			controls.CurrentRoom.MouseUp += new System.Windows.Forms.MouseEventHandler(currentRoomPopupBuilder.mouseReleased);
			roomPopupBuilder = new AnonymousClassMouseAdapter1(this);
			controls.RoomTree.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.chat.ui.RoomInteractionControlsInitializer.mouseDown);
			controls.RoomTree.Click += new System.EventHandler(roomPopupBuilder.mouseClicked);
			controls.RoomTree.MouseEnter += new System.EventHandler(roomPopupBuilder.mouseEntered);
			controls.RoomTree.MouseLeave += new System.EventHandler(roomPopupBuilder.mouseExited);
			controls.RoomTree.MouseDown += new System.Windows.Forms.MouseEventHandler(roomPopupBuilder.mousePressed);
			controls.RoomTree.MouseUp += new System.Windows.Forms.MouseEventHandler(roomPopupBuilder.mouseReleased);
			roomCreator = new AnonymousClassActionListener(controls, this);
			//UPGRADE_TODO: Method 'javax.swing.JTextField.addActionListener' was converted to 'System.Windows.Forms.TextBox.KeyPress' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldaddActionListener_javaawteventActionListener'"
			controls.NewRoom.KeyPress += new System.Windows.Forms.KeyPressEventHandler(roomCreator.actionPerformed);
		}
		
		protected internal virtual void  createRoom(System.String name)
		{
			client.setRoom(new SimpleRoom(name));
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public virtual System.Windows.Forms.ContextMenu buildPopupForRoom(Room room, System.Windows.Forms.TreeView tree)
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Windows.Forms.ContextMenu popup = new System.Windows.Forms.ContextMenu();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(RoomActionFactory f: roomActionFactories)
			{
				popup.add(f.getAction(room, tree));
			}
			return (int) popup.Controls.Count == 0?null:popup;
		}
		
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public virtual void  doubleClickRoom(Room room, System.Windows.Forms.TreeView tree)
		{
			if (!room.Equals(client.getRoom()))
			{
				new JoinRoomAction(room, client).actionPerformed(null);
			}
		}
		
		public virtual void  addPlayerActionFactory(PlayerActionFactory f)
		{
			playerActionFactories.add(f);
		}
		
		public virtual void  addRoomActionFactory(RoomActionFactory f)
		{
			roomActionFactories.add(f);
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public virtual System.Windows.Forms.ContextMenu buildPopupForPlayer(SimplePlayer target, System.Windows.Forms.TreeView tree)
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Windows.Forms.ContextMenu popup = new System.Windows.Forms.ContextMenu();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(PlayerActionFactory f: playerActionFactories)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SupportClass.ActionSupport a = f.getAction(target, tree);
				if (a != null)
				{
					System.Windows.Forms.MenuItem temp_MenuItem;
					temp_MenuItem = new System.Windows.Forms.MenuItem();
					temp_MenuItem.Click += new System.EventHandler(a.actionPerformed);
					popup.MenuItems.Add(temp_MenuItem);
					System.Windows.Forms.MenuItem generatedAux = temp_MenuItem;
				}
			}
			return (int) popup.Controls.Count == 0?null:popup;
		}
		
		public virtual void  uninitializeControls(ChatServerControls controls)
		{
			controls.RoomTree.MouseDown -= new System.Windows.Forms.MouseEventHandler(VassalSharp.chat.ui.RoomInteractionControlsInitializer.mouseDown);
			controls.RoomTree.Click -= new System.EventHandler(roomPopupBuilder.mouseClicked);
			controls.RoomTree.MouseEnter -= new System.EventHandler(roomPopupBuilder.mouseEntered);
			controls.RoomTree.MouseLeave -= new System.EventHandler(roomPopupBuilder.mouseExited);
			controls.RoomTree.MouseDown -= new System.Windows.Forms.MouseEventHandler(roomPopupBuilder.mousePressed);
			controls.RoomTree.MouseUp -= new System.Windows.Forms.MouseEventHandler(roomPopupBuilder.mouseReleased);
			controls.CurrentRoom.MouseDown -= new System.Windows.Forms.MouseEventHandler(VassalSharp.chat.ui.RoomInteractionControlsInitializer.mouseDown);
			controls.CurrentRoom.Click -= new System.EventHandler(currentRoomPopupBuilder.mouseClicked);
			controls.CurrentRoom.MouseEnter -= new System.EventHandler(currentRoomPopupBuilder.mouseEntered);
			controls.CurrentRoom.MouseLeave -= new System.EventHandler(currentRoomPopupBuilder.mouseExited);
			controls.CurrentRoom.MouseDown -= new System.Windows.Forms.MouseEventHandler(currentRoomPopupBuilder.mousePressed);
			controls.CurrentRoom.MouseUp -= new System.Windows.Forms.MouseEventHandler(currentRoomPopupBuilder.mouseReleased);
			//UPGRADE_TODO: Method 'javax.swing.JTextField.removeActionListener' was converted to 'System.Windows.Forms.TextBox.KeyPress' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldremoveActionListener_javaawteventActionListener'"
			controls.NewRoom.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(roomCreator.actionPerformed);
		}
	}
}