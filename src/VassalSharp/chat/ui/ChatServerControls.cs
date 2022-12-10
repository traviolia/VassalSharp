/*
* $Id$
*
* Copyright (c) 2000-2013 by Rodney Kinney, Brent Easton
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
using AbstractBuildable = VassalSharp.build.AbstractBuildable;
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using ChatServerConnection = VassalSharp.chat.ChatServerConnection;
using ServerAddressBook = VassalSharp.chat.ServerAddressBook;
using IconConfigurer = VassalSharp.configure.IconConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using Resources = VassalSharp.i18n.Resources;
using PositionOption = VassalSharp.preferences.PositionOption;
using VisibilityOption = VassalSharp.preferences.VisibilityOption;
using ComponentSplitter = VassalSharp.tools.ComponentSplitter;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using NamedKeyStrokeListener = VassalSharp.tools.NamedKeyStrokeListener;
using MenuManager = VassalSharp.tools.menu.MenuManager;
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
namespace VassalSharp.chat.ui
{
	
	public class ChatServerControls:AbstractBuildable
	{
		static private System.Int32 state389;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassTreeWillExpandListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassTreeWillExpandListener
		{
			public AnonymousClassTreeWillExpandListener(ChatServerControls enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ChatServerControls enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ChatServerControls enclosingInstance;
			public ChatServerControls Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  treeWillCollapse(System.Object event_sender, System.Windows.Forms.TreeViewCancelEventArgs evt)
			{
				//UPGRADE_TODO: Constructor 'javax.swing.tree.ExpandVetoException.ExpandVetoException' was converted to 'System.Exception.Exception' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtreeExpandVetoExceptionExpandVetoException_javaxswingeventTreeExpansionEvent'"
				new System.Exception();
				evt.Cancel = true;
			}
			
			public virtual void  treeWillExpand(System.Object event_sender, System.Windows.Forms.TreeViewCancelEventArgs evt)
			{
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(ChatServerControls enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ChatServerControls enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ChatServerControls enclosingInstance;
			public ChatServerControls Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				ServerAddressBook.editCurrentServer(!Enclosing_Instance.client.isConnected());
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMouseAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassMouseAdapter
		{
			public AnonymousClassMouseAdapter(ChatServerControls enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ChatServerControls enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ChatServerControls enclosingInstance;
			public ChatServerControls Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  mouseClicked(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
				if (!Enclosing_Instance.client.isConnected() && e.isMetaDown())
				{
					Enclosing_Instance.showChangeServerMenu();
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(ChatServerControls enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ChatServerControls enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ChatServerControls enclosingInstance;
			public ChatServerControls Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				Enclosing_Instance.toggleVisible();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(VassalSharp.configure.IconConfigurer iconConfig, ChatServerControls enclosingInstance)
			{
				InitBlock(iconConfig, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.configure.IconConfigurer iconConfig, ChatServerControls enclosingInstance)
			{
				this.iconConfig = iconConfig;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable iconConfig was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.IconConfigurer iconConfig;
			private ChatServerControls enclosingInstance;
			public ChatServerControls Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				Enclosing_Instance.launch.Image = iconConfig.IconValue;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener1
		{
			public AnonymousClassPropertyChangeListener1(VassalSharp.tools.NamedKeyStrokeListener l, VassalSharp.configure.NamedHotKeyConfigurer keyConfig, ChatServerControls enclosingInstance)
			{
				InitBlock(l, keyConfig, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.tools.NamedKeyStrokeListener l, VassalSharp.configure.NamedHotKeyConfigurer keyConfig, ChatServerControls enclosingInstance)
			{
				this.l = l;
				this.keyConfig = keyConfig;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable l was copied into class AnonymousClassPropertyChangeListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.tools.NamedKeyStrokeListener l;
			//UPGRADE_NOTE: Final variable keyConfig was copied into class AnonymousClassPropertyChangeListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.NamedHotKeyConfigurer keyConfig;
			private ChatServerControls enclosingInstance;
			public ChatServerControls Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				l.setKeyStroke(keyConfig.ValueNamedKeyStroke);
				Enclosing_Instance.launch.setToolTipText(Resources.getString("Chat.server_controls_tooltip", NamedHotKeyConfigurer.getString(l.getKeyStroke()))); //$NON-NLS-1$
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(ChatServerControls enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			//UPGRADE_NOTE: Delegate might have a different return value and generate a compilation error. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1309'"
			public delegate ~unresolved generatedDelegate();
			private void  InitBlock(ChatServerControls enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ChatServerControls enclosingInstance;
			public ChatServerControls Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				Enclosing_Instance.splitter.Invoke(new generatedDelegate(Enclosing_Instance.splitter.showComponent), new object[]{});
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener2
		{
			public AnonymousClassPropertyChangeListener2(ChatServerControls enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassRunnable1 : IThreadRunnable
			{
				public AnonymousClassRunnable1(SupportClass.PropertyChangingEventArgs evt, AnonymousClassPropertyChangeListener2 enclosingInstance)
				{
					InitBlock(evt, enclosingInstance);
				}
				private void  InitBlock(SupportClass.PropertyChangingEventArgs evt, AnonymousClassPropertyChangeListener2 enclosingInstance)
				{
					this.evt = evt;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable evt was copied into class AnonymousClassRunnable1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private SupportClass.PropertyChangingEventArgs evt;
				private AnonymousClassPropertyChangeListener2 enclosingInstance;
				public AnonymousClassPropertyChangeListener2 Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  Run()
				{
					Enclosing_Instance.Enclosing_Instance.roomTree.Rooms = (VassalSharp.chat.Room[]) evt.NewValue;
				}
			}
			private void  InitBlock(ChatServerControls enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ChatServerControls enclosingInstance;
			public ChatServerControls Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'runnable '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				IThreadRunnable runnable = new AnonymousClassRunnable1(evt, this);
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.invokeLater(runnable);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener3
		{
			public AnonymousClassPropertyChangeListener3(ChatServerControls enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassRunnable1 : IThreadRunnable
			{
				public AnonymousClassRunnable1(SupportClass.PropertyChangingEventArgs evt, AnonymousClassPropertyChangeListener3 enclosingInstance)
				{
					InitBlock(evt, enclosingInstance);
				}
				private void  InitBlock(SupportClass.PropertyChangingEventArgs evt, AnonymousClassPropertyChangeListener3 enclosingInstance)
				{
					this.evt = evt;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable evt was copied into class AnonymousClassRunnable1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private SupportClass.PropertyChangingEventArgs evt;
				private AnonymousClassPropertyChangeListener3 enclosingInstance;
				public AnonymousClassPropertyChangeListener3 Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  Run()
				{
					if (evt.NewValue == null)
					{
						Enclosing_Instance.Enclosing_Instance.currentRoom.Rooms = new VassalSharp.chat.Room[0];
					}
					else
					{
						Enclosing_Instance.Enclosing_Instance.currentRoom.Rooms = new VassalSharp.chat.Room[]{(VassalSharp.chat.Room) evt.NewValue};
						//UPGRADE_NOTE: Final was removed from the declaration of 'root '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						//UPGRADE_TODO: Method 'javax.swing.tree.TreeModel.getRoot' was converted to 'System.Windows.Forms.TreeNode.TreeView.Nodes' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtreeTreeModelgetRoot'"
						//UPGRADE_TODO: Method 'javax.swing.JTree.getModel' was converted to 'SupportClass.TreeSupport.GetModel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTreegetModel'"
						System.Object root = SupportClass.TreeSupport.GetModel(Enclosing_Instance.Enclosing_Instance.currentRoom).TreeView.Nodes[0];
						//UPGRADE_NOTE: Final was removed from the declaration of 'room '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						//UPGRADE_TODO: Method 'javax.swing.JTree.getModel' was converted to 'SupportClass.TreeSupport.GetModel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTreegetModel'"
						SupportClass.TreeSupport.GetModel(Enclosing_Instance.Enclosing_Instance.currentRoom);
						//UPGRADE_TODO: Method 'javax.swing.tree.TreeModel.getChild' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtreeTreeModelgetChild_javalangObject_int'"
						System.Object room = ((System.Windows.Forms.TreeNode) root).Nodes[0];
						//UPGRADE_ISSUE: Method 'javax.swing.JTree.expandPath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreeexpandPath_javaxswingtreeTreePath'"
						//UPGRADE_ISSUE: Constructor 'javax.swing.tree.TreePath.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
						Enclosing_Instance.Enclosing_Instance.currentRoom.expandPath(new TreePath(new System.Object[]{root, room}));
					}
				}
			}
			private void  InitBlock(ChatServerControls enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ChatServerControls enclosingInstance;
			public ChatServerControls Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'runnable '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				IThreadRunnable runnable = new AnonymousClassRunnable1(evt, this);
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.invokeLater(runnable);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener4
		{
			public AnonymousClassPropertyChangeListener4(ChatServerControls enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ChatServerControls enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ChatServerControls enclosingInstance;
			public ChatServerControls Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				Enclosing_Instance.updateConfigServerToolTipText();
			}
		}
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state389 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		virtual public System.Windows.Forms.Control ExtendedControls
		{
			get
			{
				return null;
			}
			
		}
		virtual public System.Windows.Forms.Panel Controls
		{
			get
			{
				return controlPanel;
			}
			
		}
		virtual public ChatServerConnection Client
		{
			get
			{
				return client;
			}
			
			set
			{
				client = value;
				if (value is ChatControlsInitializer)
				{
					if (basicControls != null)
					{
						basicControls.uninitializeControls(this);
					}
					if (oldClient != null)
					{
						oldClient.uninitializeControls(this);
					}
					basicControls = new BasicChatControlsInitializer(value);
					basicControls.initializeControls(this);
					((ChatControlsInitializer) value).initializeControls(this);
					oldClient = (ChatControlsInitializer) value;
				}
				//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				PropertyChangeListener roomUpdater = new AnonymousClassPropertyChangeListener2(this);
				client.addPropertyChangeListener(VassalSharp.chat.ChatServerConnection_Fields.AVAILABLE_ROOMS, roomUpdater);
				//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				PropertyChangeListener currentRoomUpdater = new AnonymousClassPropertyChangeListener3(this);
				client.addPropertyChangeListener(VassalSharp.chat.ChatServerConnection_Fields.ROOM, currentRoomUpdater);
				client.addPropertyChangeListener(VassalSharp.build.module.ServerConnection_Fields.CONNECTED, new AnonymousClassPropertyChangeListener4(this));
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[0];
			}
			
		}
		virtual public System.Windows.Forms.ToolBar Toolbar
		{
			get
			{
				return toolbar;
			}
			
		}
		virtual public RoomTree CurrentRoom
		{
			get
			{
				return currentRoom;
			}
			
		}
		virtual public System.Windows.Forms.TextBox NewRoom
		{
			get
			{
				return newRoom;
			}
			
		}
		virtual public bool RoomControlsVisible
		{
			set
			{
				newRoom.Visible = value;
				newRoomLabel.Visible = value;
			}
			
		}
		virtual public RoomTree RoomTree
		{
			get
			{
				return roomTree;
			}
			
		}
		
		protected internal RoomTree currentRoom;
		protected internal System.Windows.Forms.TextBox newRoom;
		protected internal System.Windows.Forms.Label newRoomLabel;
		protected internal System.Windows.Forms.ToolBar toolbar;
		protected internal RoomTree roomTree;
		protected internal System.Windows.Forms.Button newRoomButton;
		
		protected internal System.Windows.Forms.Button launch;
		protected internal ChatServerConnection client;
		protected internal System.Windows.Forms.Panel controlPanel;
		protected internal ComponentSplitter.SplitPane splitter;
		protected internal ChatControlsInitializer oldClient;
		protected internal BasicChatControlsInitializer basicControls;
		protected internal System.Windows.Forms.Button configServerButton;
		protected internal System.String configServerText;
		
		public ChatServerControls()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'split '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'javax.swing.JSplitPane' was converted to 'SupportClass.SplitterPanelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			SupportClass.SplitterPanelSupport split = new SupportClass.SplitterPanelSupport((int) System.Windows.Forms.Orientation.Horizontal);
			//UPGRADE_ISSUE: Method 'javax.swing.JSplitPane.setResizeWeight' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJSplitPanesetResizeWeight_double'"
			split.setResizeWeight(0.5);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'roomPanel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel roomPanel = new JPanel(new MigLayout("fill, nogrid, hidemode 3"));
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			//UPGRADE_ISSUE: Method 'javax.swing.BorderFactory.createTitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBorderFactorycreateTitledBorder_javaxswingborderBorder_javalangString'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(roomPanel.CreateGraphics(), 0, 0, roomPanel.Width, roomPanel.Height, BorderFactory.createTitledBorder(System.Windows.Forms.Border3DStyle.Raised, Resources.getString("Chat.active_games")));
			
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			newRoom = new System.Windows.Forms.TextBox();
			System.Windows.Forms.Label temp_label;
			temp_label = new System.Windows.Forms.Label();
			temp_label.Text = Resources.getString("Chat.new_game");
			newRoomLabel = temp_label;
			//UPGRADE_ISSUE: Method 'javax.swing.JLabel.setLabelFor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJLabelsetLabelFor_javaawtComponent'"
			newRoomLabel.setLabelFor(newRoom);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			roomPanel.Controls.Add(newRoomLabel);
			newRoomLabel.Dock = new System.Windows.Forms.DockStyle();
			newRoomLabel.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			roomPanel.Controls.Add(newRoom);
			newRoom.Dock = new System.Windows.Forms.DockStyle();
			newRoom.BringToFront();
			
			newRoomButton = SupportClass.ButtonSupport.CreateStandardButton("..."); //$NON-NLS-1$
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			newRoomButton.Size = new System.Drawing.Size(20, 20);
			newRoomButton.Visible = false;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			roomPanel.Controls.Add(newRoomButton);
			newRoomButton.Dock = new System.Windows.Forms.DockStyle();
			newRoomButton.BringToFront();
			
			roomTree = new RoomTree();
			//UPGRADE_NOTE: Final was removed from the declaration of 'roomScroll '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
			temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol.AutoScroll = true;
			temp_scrollablecontrol.Controls.Add(roomTree);
			System.Windows.Forms.ScrollableControl roomScroll = temp_scrollablecontrol;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			roomPanel.Controls.Add(roomScroll);
			roomScroll.Dock = new System.Windows.Forms.DockStyle();
			roomScroll.BringToFront();
			
			split.FirstControl = roomPanel;
			currentRoom = new RoomTree();
			currentRoom.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(new AnonymousClassTreeWillExpandListener(this).treeWillCollapse);
			currentRoom.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(new AnonymousClassTreeWillExpandListener(this).treeWillExpand);
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol2;
			temp_scrollablecontrol2 = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol2.AutoScroll = true;
			temp_scrollablecontrol2.Controls.Add(currentRoom);
			System.Windows.Forms.ScrollableControl scroll = temp_scrollablecontrol2;
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			//UPGRADE_ISSUE: Method 'javax.swing.BorderFactory.createTitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBorderFactorycreateTitledBorder_javaxswingborderBorder_javalangString'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(scroll.CreateGraphics(), 0, 0, scroll.Width, scroll.Height, BorderFactory.createTitledBorder(System.Windows.Forms.Border3DStyle.Raised, Resources.getString("Chat.current_game"))); //$NON-NLS-1$
			split.SecondControl = scroll;
			split.SplitterLocation = 160;
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			split.Size = new System.Drawing.Size(320, 120);
			controlPanel = new System.Windows.Forms.Panel();
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			/*
			controlPanel.setLayout(new BorderLayout());*/
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
			controlPanel.Controls.Add(split); //$NON-NLS-1$
			System.Windows.Forms.ToolBar temp_ToolBar;
			System.Windows.Forms.ImageList temp_ImageList;
			temp_ImageList = new System.Windows.Forms.ImageList();
			temp_ToolBar = new System.Windows.Forms.ToolBar();
			temp_ToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(SupportClass.ToolBarButtonClicked);
			temp_ToolBar.ImageList = temp_ImageList;
			toolbar = temp_ToolBar;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
			controlPanel.Controls.Add(toolbar); //$NON-NLS-1$
			System.Windows.Forms.ToolBarButton separator;
			separator = new System.Windows.Forms.ToolBarButton();
			separator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			toolbar.Buttons.Add(separator);
			
			configServerButton = new System.Windows.Forms.Button();
			configServerButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(configServerButton);
			configServerButton.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.chat.ui.ChatServerControls.mouseDown);
			configServerButton.Click += new System.EventHandler(new AnonymousClassMouseAdapter(this).mouseClicked);
			System.Windows.Forms.ToolBarButton temp_ToolBarButton;
			temp_ToolBarButton = new System.Windows.Forms.ToolBarButton(configServerButton.Text);
			temp_ToolBarButton.ToolTipText = SupportClass.ToolTipSupport.getToolTipText(configServerButton);
			toolbar.Buttons.Add(temp_ToolBarButton);
			if (configServerButton.Image != null)
			{
				toolbar.ImageList.Images.Add(configServerButton.Image);
				temp_ToolBarButton.ImageIndex = toolbar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton.Tag = configServerButton;
			configServerButton.Tag = temp_ToolBarButton;
		}
		
		private void  showChangeServerMenu()
		{
			ServerAddressBook.changeServerPopup(configServerButton);
		}
		
		public virtual void  updateClientDisplay(System.Drawing.Image icon, System.String text)
		{
			configServerButton.Image = icon;
			configServerText = text;
			updateConfigServerToolTipText();
		}
		
		private void  updateConfigServerToolTipText()
		{
			if (client.isConnected())
			{
				if (configServerButton is VassalSharp.tools.LaunchButton)
					((VassalSharp.tools.LaunchButton) configServerButton).setToolTipText(configServerText);
				else
					SupportClass.ToolTipSupport.setToolTipText(configServerButton, configServerText);
			}
			else
			{
				if (configServerButton is VassalSharp.tools.LaunchButton)
					((VassalSharp.tools.LaunchButton) configServerButton).setToolTipText("<html><center>" + configServerText + "<br>" + "Right-click to change server");
				else
					SupportClass.ToolTipSupport.setToolTipText(configServerButton, "<html><center>" + configServerText + "<br>" + "Right-click to change server");
			}
		}
		
		public override void  addTo(Buildable b)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'gm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GameModule gm = GameModule.getGameModule();
			Client = (ChatServerConnection) gm.getServer();
			launch = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString("Chat.server")); //$NON-NLS-1$
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentY_float'"
			launch.setAlignmentY(0.0F);
			//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			ActionListener al = new AnonymousClassActionListener1(this);
			launch.Click += new System.EventHandler(al.actionPerformed);
			SupportClass.CommandManager.CheckCommand(launch);
			//UPGRADE_NOTE: Final was removed from the declaration of 'l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			NamedKeyStrokeListener l = new NamedKeyStrokeListener(al);
			l.setKeyStroke(NamedKeyStroke.getNamedKeyStroke((int) System.Windows.Forms.Keys.S, (int) System.Windows.Forms.Keys.Alt));
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			System.Uri iconURL = new System.Uri(System.IO.Path.GetFullPath("/images/connect.gif")); //$NON-NLS-1$
			if (iconURL != null)
			{
				//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
				launch.Image = new ImageIcon(iconURL);
				launch.Text = null;
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'iconConfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			IconConfigurer iconConfig = new IconConfigurer("serverControlsIcon", Resources.getString("Chat.server_controls_button_icon"), "/images/connect.gif"); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
			iconConfig.setValue("/images/connect.gif"); //$NON-NLS-1$
			GlobalOptions.Instance.addOption(iconConfig);
			iconConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(iconConfig, this).propertyChange);
			iconConfig.fireUpdate();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'keyConfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			NamedHotKeyConfigurer keyConfig = new NamedHotKeyConfigurer("serverControlsHotKey", Resources.getString("Chat.server_controls_hotkey"), l.NamedKeyStroke); //$NON-NLS-1$ //$NON-NLS-2$
			GlobalOptions.Instance.addOption(keyConfig);
			keyConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener1(l, keyConfig, this).propertyChange);
			keyConfig.fireUpdate();
			
			gm.addKeyStrokeListener(l);
			System.Windows.Forms.ToolBarButton temp_ToolBarButton;
			temp_ToolBarButton = new System.Windows.Forms.ToolBarButton(launch.Text);
			temp_ToolBarButton.ToolTipText = SupportClass.ToolTipSupport.getToolTipText(launch);
			gm.ToolBar.Buttons.Add(temp_ToolBarButton);
			if (launch.Image != null)
			{
				gm.ToolBar.ImageList.Images.Add(launch.Image);
				temp_ToolBarButton.ImageIndex = gm.ToolBar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton.Tag = launch;
			launch.Tag = temp_ToolBarButton;
		}
		
		public virtual void  toggleVisible()
		{
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
			if (controlPanel.getTopLevelAncestor() == null)
			{
				if (GlobalOptions.Instance.UseSingleWindow)
				{
					splitter = new ComponentSplitter().splitRight(GameModule.getGameModule().getControlPanel(), controlPanel, false);
					splitter.revalidate();
					//UPGRADE_NOTE: Final was removed from the declaration of 'runnable '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					IThreadRunnable runnable = new AnonymousClassRunnable(this);
					//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
					SwingUtilities.invokeLater(runnable);
				}
				else
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'frame '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Windows.Forms.Form frame = SupportClass.FormSupport.CreateForm(Resources.getString("Chat.server")); //$NON-NLS-1$
					frame.Closing += new System.ComponentModel.CancelEventHandler(this.ChatServerControls_Closing_HIDE_ON_CLOSE);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					frame.Controls.Add(controlPanel);
					frame.Menu = MenuManager.Instance.getMenuBarFor(frame);
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'key '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String key = "BoundsOfClientWindow"; //$NON-NLS-1$
					//UPGRADE_NOTE: Final was removed from the declaration of 'pos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					PositionOption pos = new VisibilityOption(key, frame);
					GameModule.getGameModule().getPrefs().addOption(pos);
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					frame.Visible = true;
				}
			}
			else if (splitter != null)
			{
				splitter.toggleVisibility();
			}
			else
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(controlPanel.getTopLevelAncestor(), "Visible", !controlPanel.getTopLevelAncestor().Visible);
			}
		}
		
		public override void  setAttribute(System.String name, System.Object value_Renamed)
		{
		}
		
		public override System.String getAttributeValueString(System.String name)
		{
			return null;
		}
		
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  addExtendedNewRoomHandler(ActionListener l)
		{
			newRoomButton.Click += new System.EventHandler(l.actionPerformed);
			SupportClass.CommandManager.CheckCommand(newRoomButton);
			newRoomButton.Visible = true;
		}
		
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  removeExtendedNewRoomHandler(ActionListener l)
		{
			newRoomButton.Click -= new System.EventHandler(l.actionPerformed);
			newRoomButton.Visible = false;
		}
		private void  ChatServerControls_Closing_HIDE_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 1);
		}
	}
}