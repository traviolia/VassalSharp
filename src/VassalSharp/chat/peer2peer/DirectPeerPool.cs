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
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
using PeerInfo = org.litesoft.p2pchat.PeerInfo;
using PendingPeerManager = org.litesoft.p2pchat.PendingPeerManager;
using GameModule = VassalSharp.build.GameModule;
using ChatControlsInitializer = VassalSharp.chat.ui.ChatControlsInitializer;
using ChatServerControls = VassalSharp.chat.ui.ChatServerControls;
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
using Resources = VassalSharp.i18n.Resources;
using Prefs = VassalSharp.preferences.Prefs;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using Dialogs = VassalSharp.tools.swing.Dialogs;
namespace VassalSharp.chat.peer2peer
{
	
	/// <summary> Date: Mar 12, 2003</summary>
	public class DirectPeerPool : PeerPool, ChatControlsInitializer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(DirectPeerPool enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(DirectPeerPool enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private DirectPeerPool enclosingInstance;
			public DirectPeerPool Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(Enclosing_Instance.frame, "Visible", true);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(org.litesoft.p2pchat.PendingPeerManager ppm, DirectPeerPool enclosingInstance)
			{
				InitBlock(ppm, enclosingInstance);
			}
			private void  InitBlock(org.litesoft.p2pchat.PendingPeerManager ppm, DirectPeerPool enclosingInstance)
			{
				this.ppm = ppm;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable ppm was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private org.litesoft.p2pchat.PendingPeerManager ppm;
			private DirectPeerPool enclosingInstance;
			public DirectPeerPool Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.invite(ppm);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(DirectPeerPool enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(DirectPeerPool enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private DirectPeerPool enclosingInstance;
			public DirectPeerPool Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.addEntry();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener3
		{
			public AnonymousClassActionListener3(DirectPeerPool enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(DirectPeerPool enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private DirectPeerPool enclosingInstance;
			public DirectPeerPool Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.editEntry();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener4
		{
			public AnonymousClassActionListener4(DirectPeerPool enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(DirectPeerPool enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private DirectPeerPool enclosingInstance;
			public DirectPeerPool Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs arg0)
			{
				Enclosing_Instance.removeEntries();
			}
		}
		virtual protected internal bool ServerMode
		{
			get
			{
				return serverMode;
			}
			
		}
		protected internal const System.String ADDRESS_PREF = "PeerAddressBook"; //$NON-NLS-1$
		private AcceptPeerThread acceptThread;
		private System.Windows.Forms.Button inviteButton;
		private System.Windows.Forms.Form frame;
		private int listenPort;
		private static StringArrayConfigurer addressConfig;
		private System.Windows.Forms.Button invitePeerButton;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button editButton;
		private System.Windows.Forms.Button removeButton;
		private System.Windows.Forms.ListBox addressList;
		//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
		private System.Windows.Forms.ListBox.ObjectCollection addressBook;
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		private System.Collections.Specialized.NameValueCollection params_Renamed;
		private bool serverMode;
		
		//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public DirectPeerPool():this(new System.Collections.Specialized.NameValueCollection())
		{
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public DirectPeerPool(System.Collections.Specialized.NameValueCollection param)
		{
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			params_Renamed = new System.Collections.Specialized.NameValueCollection();
			SupportClass.MapSupport.PutAll(params_Renamed, param);
			serverMode = P2PClientFactory.P2P_SERVER_MODE.Equals(params_Renamed.Get(P2PClientFactory.P2P_MODE_KEY));
			inviteButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString("Peer2Peer.connect")); //$NON-NLS-1$
			inviteButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(inviteButton);
			inviteButton.Enabled = false;
			inviteButton.Visible = P2PClientFactory.P2P_SERVER_MODE.Equals(params_Renamed.Get(P2PClientFactory.P2P_MODE_KEY));
		}
		
		public virtual void  initialize(P2PPlayer myInfo, PendingPeerManager ppm)
		{
			listenPort = 5050;
			System.String port = params_Renamed.Get(P2PClientFactory.P2P_LISTEN_PORT);
			if (port != null && port.Length > 0)
			{
				try
				{
					listenPort = System.Int32.Parse(port);
				}
				catch (System.FormatException e)
				{
					// No error;
				}
			}
			
			acceptThread = new AcceptPeerThread(listenPort, ppm);
			//UPGRADE_NOTE: The Name property of a Thread in C# is write-once. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1140'"
			acceptThread.Name = "Accept Peer Thread"; //$NON-NLS-1$
			myInfo.Info.Port = acceptThread.Port;
			acceptThread.Start();
			if (frame == null)
			{
				initComponents(myInfo, ppm);
				inviteButton.Enabled = true;
			}
		}
		
		public virtual void  disconnect()
		{
			if (frame != null)
			{
				frame.Dispose();
				frame = null;
				inviteButton.Enabled = false;
			}
			if (acceptThread != null)
			{
				acceptThread.halt();
				acceptThread = null;
			}
		}
		
		public virtual void  connectFailed(PeerInfo peerInfo)
		{
			SupportClass.OptionPaneSupport.ShowMessageDialog(frame, Resources.getString("Peer2Peer.could_not_reach", peerInfo.Addresses, System.Convert.ToString(peerInfo.Port)), Resources.getString("Peer2Peer.invite_failed"), (int) System.Windows.Forms.MessageBoxIcon.Information); //$NON-NLS-1$
		}
		
		public virtual void  initComponents(P2PPlayer me, PendingPeerManager ppm)
		{
			
			// Retrieve Address Book from preference
			if (addressConfig == null)
			{
				addressConfig = new StringArrayConfigurer(ADDRESS_PREF, null);
				Prefs.GlobalPrefs.addOption(null, addressConfig);
			}
			System.String[] encodedEntries = addressConfig.StringArray;
			//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
			addressBook = new System.Windows.Forms.ListBox.ObjectCollection(new System.Windows.Forms.ListBox());
			System.Windows.Forms.ListBox temp_ListBox;
			temp_ListBox = new System.Windows.Forms.ListBox();
			temp_ListBox.Items.AddRange(addressBook);
			temp_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			addressList = temp_ListBox;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String e: encodedEntries)
			{
				addToList(new Entry(e));
			}
			
			//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
			System.Windows.Forms.Form owner = null;
			if (GameModule.getGameModule() != null)
			{
				owner = GameModule.getGameModule().getFrame();
			}
			frame = SupportClass.DialogSupport.CreateDialog(owner, Resources.getString("Peer2Peer.direct_connection")); //$NON-NLS-1$
			frame.Closing += new System.ComponentModel.CancelEventHandler(this.DirectPeerPool_Closing_HIDE_ON_CLOSE);
			frame.setLayout(new MigLayout());
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = new WTextArea(this, Resources.getString("Peer2Peer.other_players_address"));
			frame.Controls.Add(temp_Control);
			temp_Control.Dock = new System.Windows.Forms.DockStyle();
			temp_Control.BringToFront(); //$NON-NLS-1$ //$NON-NLS-2$
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'scroll '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
			temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol.AutoScroll = true;
			temp_scrollablecontrol.Controls.Add(addressList);
			System.Windows.Forms.ScrollableControl scroll = temp_scrollablecontrol;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			frame.Controls.Add(scroll);
			scroll.Dock = new System.Windows.Forms.DockStyle();
			scroll.BringToFront(); //$NON-NLS-1$
			
			invitePeerButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString("Peer2Peer.connect")); //$NON-NLS-1$
			if (invitePeerButton is VassalSharp.tools.LaunchButton)
				((VassalSharp.tools.LaunchButton) invitePeerButton).setToolTipText(Resources.getString("Peer2Peer.invite_button_tooltip"));
			else
				SupportClass.ToolTipSupport.setToolTipText(invitePeerButton, Resources.getString("Peer2Peer.invite_button_tooltip")); //$NON-NLS-1$))
			invitePeerButton.Click += new System.EventHandler(new AnonymousClassActionListener1(ppm, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(invitePeerButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			frame.Controls.Add(invitePeerButton);
			invitePeerButton.Dock = new System.Windows.Forms.DockStyle();
			invitePeerButton.BringToFront(); //$NON-NLS-1$
			
			addButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString("General.add")); //$NON-NLS-1$
			if (addButton is VassalSharp.tools.LaunchButton)
				((VassalSharp.tools.LaunchButton) addButton).setToolTipText(Resources.getString("Peer2Peer.add_button_tooltip"));
			else
				SupportClass.ToolTipSupport.setToolTipText(addButton, Resources.getString("Peer2Peer.add_button_tooltip")); //$NON-NLS-1$))
			addButton.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(addButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			frame.Controls.Add(addButton);
			addButton.Dock = new System.Windows.Forms.DockStyle();
			addButton.BringToFront(); //$NON-NLS-1$
			
			editButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString("General.edit")); //$NON-NLS-1$
			if (editButton is VassalSharp.tools.LaunchButton)
				((VassalSharp.tools.LaunchButton) editButton).setToolTipText(Resources.getString("Peer2Peer.edit_button_tooltip"));
			else
				SupportClass.ToolTipSupport.setToolTipText(editButton, Resources.getString("Peer2Peer.edit_button_tooltip")); //$NON-NLS-1$))
			editButton.Click += new System.EventHandler(new AnonymousClassActionListener3(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(editButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			frame.Controls.Add(editButton);
			editButton.Dock = new System.Windows.Forms.DockStyle();
			editButton.BringToFront(); //$NON-NLS-1$
			
			removeButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString("General.remove")); //$NON-NLS-1$
			if (removeButton is VassalSharp.tools.LaunchButton)
				((VassalSharp.tools.LaunchButton) removeButton).setToolTipText(Resources.getString("Peer2Peer.remove_button_tooltip"));
			else
				SupportClass.ToolTipSupport.setToolTipText(removeButton, Resources.getString("Peer2Peer.remove_button_tooltip")); //$NON-NLS-1$))
			removeButton.Click += new System.EventHandler(new AnonymousClassActionListener4(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(removeButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			frame.Controls.Add(removeButton);
			removeButton.Dock = new System.Windows.Forms.DockStyle();
			removeButton.BringToFront(); //$NON-NLS-1$
			
			
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			frame.pack();
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			frame.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			
			return ;
		}
		
		protected internal virtual void  invite(PendingPeerManager ppm)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'selected '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Method 'javax.swing.JList.getSelectedIndices' was converted to 'System.Windows.Forms.ListBox.SelectedIndices' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListgetSelectedIndices'"
			int[] selected = addressList.SelectedIndices;
			for (int i = 0; i < selected.Length; i++)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Entry entry = (Entry) addressBook[selected[i]];
				//UPGRADE_NOTE: Final was removed from the declaration of 'info '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				PeerInfo info = PeerInfo.deFormat(entry.Address + ":" + entry.Port + " " + entry.Description); //$NON-NLS-1$ //$NON-NLS-2$
				if (info != null)
				{
					ppm.addNewPeer(info);
					GameModule.getGameModule().warn(Resources.getString("Chat.invite_sent", entry.ToString())); //$NON-NLS-1$
				}
				else
				{
					SupportClass.OptionPaneSupport.ShowMessageDialog(frame, Resources.getString("Peer2Peer.invalid_format")); //$NON-NLS-1$
				}
			}
		}
		
		protected internal virtual void  addEntry()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Entry e = new Entry(this);
			if (e.edit())
			{
				if (!addressBook.Contains(e))
				{
					addToList(e);
					saveAddressBook();
				}
			}
		}
		
		protected internal virtual void  addToList(Entry e)
		{
			bool added = false;
			for (int i = 0; i < addressBook.Count && !added; i++)
			{
				if (e.compareTo((Entry) (addressBook[i])) < 0)
				{
					addressBook.Insert(i, e);
					added = true;
				}
			}
			if (!added)
			{
				addressBook.Add(e);
			}
		}
		
		protected internal virtual void  editEntry()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'index '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int index = addressList.SelectedIndex;
			if (index >= 0)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Entry e = (Entry) addressBook[index];
				if (e.edit())
				{
					addressBook.RemoveAt(index);
					addToList(e);
					saveAddressBook();
				}
			}
		}
		
		protected internal virtual void  removeEntries()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'selected '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Method 'javax.swing.JList.getSelectedIndices' was converted to 'System.Windows.Forms.ListBox.SelectedIndices' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListgetSelectedIndices'"
			int[] selected = addressList.SelectedIndices;
			if (selected.Length == 0)
			{
				return ;
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'entries '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Entry[] entries = new Entry[selected.Length];
			for (int i = 0; i < selected.Length; i++)
			{
				entries[i] = (Entry) addressBook[selected[i]];
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'queryPanel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel queryPanel = new JPanel(new MigLayout("", "10[][]10"));
			//UPGRADE_NOTE: Final was removed from the declaration of 'mess '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String mess = (entries.Length == 1?Resources.getString("Peer2Peer.remove_entry"):Resources.getString("Peer2Peer.remove_entries", entries.Length)); //$NON-NLS-1$ //$NON-NLS-2$
			System.Windows.Forms.Label temp_label2;
			temp_label2 = new System.Windows.Forms.Label();
			temp_label2.Text = mess;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = temp_label2;
			queryPanel.Controls.Add(temp_Control);
			temp_Control.Dock = new System.Windows.Forms.DockStyle();
			temp_Control.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control2;
			temp_Control2 = new System.Windows.Forms.Label();
			queryPanel.Controls.Add(temp_Control2);
			temp_Control2.Dock = new System.Windows.Forms.DockStyle();
			temp_Control2.BringToFront();
			for (int i = 0; i < entries.Length; i++)
			{
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = entries[i].ToString();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				System.Windows.Forms.Control temp_Control3;
				temp_Control3 = temp_label4;
				queryPanel.Controls.Add(temp_Control3);
				temp_Control3.Dock = new System.Windows.Forms.DockStyle();
				temp_Control3.BringToFront();
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Int32 result = (System.Int32) Dialogs.showDialog(null, Resources.getString("Peer2Peer.remove_entry"), queryPanel, (int) System.Windows.Forms.MessageBoxIcon.Question, null, (int) System.Windows.Forms.MessageBoxButtons.OKCancel, null, (System.Object) null, (System.Object) null, null);
			//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
			if (result != null && result == 0)
			{
				for (int i = 0; i < entries.Length; i++)
				{
					addressBook.Remove(entries[i]);
				}
				
				saveAddressBook();
			}
		}
		
		protected internal virtual void  saveAddressBook()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'entries '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] entries = new System.String[addressBook.Count];
			int i = 0;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Enumeration < ? > e = addressBook.elements();
			e.hasMoreElements();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			)
			{
				entries[i++] = ((Entry) e.nextElement()).encode();
			}
			addressConfig.setValue(entries);
		}
		
		public virtual void  initializeControls(ChatServerControls controls)
		{
			System.Windows.Forms.ToolBarButton temp_ToolBarButton;
			temp_ToolBarButton = new System.Windows.Forms.ToolBarButton(inviteButton.Text);
			temp_ToolBarButton.ToolTipText = SupportClass.ToolTipSupport.getToolTipText(inviteButton);
			controls.Toolbar.Buttons.Add(temp_ToolBarButton);
			if (inviteButton.Image != null)
			{
				controls.Toolbar.ImageList.Images.Add(inviteButton.Image);
				temp_ToolBarButton.ImageIndex = controls.Toolbar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton.Tag = inviteButton;
			inviteButton.Tag = temp_ToolBarButton;
		}
		
		public virtual void  uninitializeControls(ChatServerControls controls)
		{
			controls.Toolbar.Buttons.Remove((System.Windows.Forms.ToolBarButton) inviteButton.Tag);
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			controls.Toolbar.Refresh();
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Entry' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> A class representing the address of another player's computer.
		/// 
		/// </summary>
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		protected internal class Entry : System.IComparable
		{
			private void  InitBlock(DirectPeerPool enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				System.String description;
				System.String address;
				System.String port;
				System.Windows.Forms.TextBox descriptionField;
				System.Windows.Forms.TextBox addressField;
				System.Windows.Forms.TextBox portField;
			}
			private DirectPeerPool enclosingInstance;
			virtual public System.String Description
			{
				get
				{
					return description;
				}
				
			}
			virtual public System.String Address
			{
				get
				{
					return address;
				}
				
			}
			virtual public System.String Port
			{
				get
				{
					return port;
				}
				
			}
			public DirectPeerPool Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< Entry >
			
			public Entry(DirectPeerPool enclosingInstance):this(enclosingInstance, "", "", "5050", "")
			{
			}
			
			public Entry(DirectPeerPool enclosingInstance, System.String description, System.String address, System.String port, System.String passwd)
			{
				InitBlock(enclosingInstance);
				this.description = description;
				this.address = address;
				this.port = port;
			}
			
			public Entry(DirectPeerPool enclosingInstance, System.String s)
			{
				InitBlock(enclosingInstance);
				decode(s);
			}
			
			//    public String getPasswd() {
			//      return passwd;
			//    }
			
			public override System.String ToString()
			{
				return description + " [" + address + ":" + port; // + (getPasswd().length() == 0 ? "" : "/") +  getPasswd() + "]"; //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$ //$NON-NLS-4$
			}
			
			private void  decode(System.String s)
			{
				SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, '|');
				description = sd.nextToken(""); //$NON-NLS-1$
				address = sd.nextToken(""); //$NON-NLS-1$
				port = sd.nextToken("5050"); //$NON-NLS-1$
			}
			
			public virtual System.String encode()
			{
				SequenceEncoder se = new SequenceEncoder('|');
				se.append(description);
				se.append(address);
				se.append(port);
				return se.Value;
			}
			
			public virtual int compareTo(Entry e)
			{
				return String.CompareOrdinal(ToString(), e.ToString());
			}
			
			public virtual bool edit()
			{
				System.Windows.Forms.TextBox temp_text_box;
				temp_text_box = new System.Windows.Forms.TextBox();
				temp_text_box.Text = description;
				descriptionField = temp_text_box;
				System.Windows.Forms.TextBox temp_text_box2;
				temp_text_box2 = new System.Windows.Forms.TextBox();
				temp_text_box2.Text = address;
				addressField = temp_text_box2;
				System.Windows.Forms.TextBox temp_text_box3;
				temp_text_box3 = new System.Windows.Forms.TextBox();
				temp_text_box3.Text = port;
				portField = temp_text_box3;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'editPanel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Panel editPanel = new JPanel(new MigLayout("", "[align right]rel[]", "")); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = Resources.getString("Editor.description_label");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				editPanel.Controls.Add(temp_Control); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				editPanel.Controls.Add(descriptionField);
				descriptionField.Dock = new System.Windows.Forms.DockStyle();
				descriptionField.BringToFront(); //$NON-NLS-1$
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = Resources.getString("Chat.ip_address");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label4;
				editPanel.Controls.Add(temp_Control2); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				editPanel.Controls.Add(addressField);
				addressField.Dock = new System.Windows.Forms.DockStyle();
				addressField.BringToFront(); //$NON-NLS-1$
				System.Windows.Forms.Label temp_label6;
				temp_label6 = new System.Windows.Forms.Label();
				temp_label6.Text = Resources.getString("ServerAddressBook.port");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control3;
				temp_Control3 = temp_label6;
				editPanel.Controls.Add(temp_Control3); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				editPanel.Controls.Add(portField);
				portField.Dock = new System.Windows.Forms.DockStyle();
				portField.BringToFront(); //$NON-NLS-1$
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Int32 result = (System.Int32) Dialogs.showDialog(null, Resources.getString("Peer2Peer.add_peer_connection"), editPanel, (int) System.Windows.Forms.MessageBoxIcon.None, null, (int) System.Windows.Forms.MessageBoxButtons.OKCancel, null, (System.Object) null, (System.Object) null, null);
				
				//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
				if (result != null && (System.Object) result == 0)
				{
					description = descriptionField.Text;
					address = addressField.Text;
					port = portField.Text;
					return true;
				}
				
				return false;
			}
			//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			virtual public System.Int32 CompareTo(System.Object obj)
			{
				return 0;
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'WTextArea' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class WTextArea:System.Windows.Forms.TextBox
		{
			private void  InitBlock(DirectPeerPool enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private DirectPeerPool enclosingInstance;
			public DirectPeerPool Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			public WTextArea(DirectPeerPool enclosingInstance, System.String s):base()
			{
				InitBlock(enclosingInstance);
				this.Multiline = true;
				this.WordWrap = false;
				this.ScrollBars = System.Windows.Forms.ScrollBars.Both;
				this.Text = s;
				ReadOnly = !false;
				WordWrap = true;
				WordWrap = true;
				//UPGRADE_TODO: Method 'javax.swing.UIManager.getColor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				BackColor = UIManager.getColor("OptionPane.background");
			}
		}
		private void  DirectPeerPool_Closing_HIDE_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 1);
		}
	}
}