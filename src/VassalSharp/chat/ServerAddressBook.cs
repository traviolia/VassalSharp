/*
* $Id$
*
* Copyright (c) 2009-2013 by Brent Easton
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
//UPGRADE_TODO: The type 'java.net.NetworkInterface' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using NetworkInterface = java.net.NetworkInterface;
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
using GameModule = VassalSharp.build.GameModule;
using JabberClient = VassalSharp.chat.jabber.JabberClient;
using JabberClientFactory = VassalSharp.chat.jabber.JabberClientFactory;
using NodeClientFactory = VassalSharp.chat.node.NodeClientFactory;
using P2PClientFactory = VassalSharp.chat.peer2peer.P2PClientFactory;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using Resources = VassalSharp.i18n.Resources;
using Prefs = VassalSharp.preferences.Prefs;
using PropertiesEncoder = VassalSharp.tools.PropertiesEncoder;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using IconFactory = VassalSharp.tools.icon.IconFactory;
using IconFamily = VassalSharp.tools.icon.IconFamily;
using Dialogs = VassalSharp.tools.swing.Dialogs;
namespace VassalSharp.chat
{
	
	public class ServerAddressBook
	{
		static private System.Int32 state376;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassListSelectionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassListSelectionListener
		{
			public AnonymousClassListSelectionListener(ServerAddressBook enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerAddressBook enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerAddressBook enclosingInstance;
			public ServerAddressBook Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  valueChanged(System.Object event_sender, System.EventArgs evt)
			{
				Enclosing_Instance.updateButtonVisibility();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMouseAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassMouseAdapter
		{
			public AnonymousClassMouseAdapter(ServerAddressBook enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerAddressBook enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerAddressBook enclosingInstance;
			public ServerAddressBook Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  mouseClicked(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getClickCount' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetClickCount'"
				if (Enclosing_Instance.editButton.Enabled && e.getClickCount() == 2)
				{
					//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
					int index = Enclosing_Instance.myList.IndexFromPoint(e.getPoint());
					Enclosing_Instance.editServer(index);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(ServerAddressBook enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerAddressBook enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerAddressBook enclosingInstance;
			public ServerAddressBook Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.setCurrentServer(Enclosing_Instance.myList.SelectedIndex);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(ServerAddressBook enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerAddressBook enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerAddressBook enclosingInstance;
			public ServerAddressBook Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.addServer();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(ServerAddressBook enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerAddressBook enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerAddressBook enclosingInstance;
			public ServerAddressBook Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.removeServer(Enclosing_Instance.myList.SelectedIndex);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener3
		{
			public AnonymousClassActionListener3(ServerAddressBook enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerAddressBook enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerAddressBook enclosingInstance;
			public ServerAddressBook Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.editServer(Enclosing_Instance.myList.SelectedIndex);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener4
		{
			public AnonymousClassActionListener4(ServerAddressBook enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerAddressBook enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerAddressBook enclosingInstance;
			public ServerAddressBook Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs arg0)
			{
				Enclosing_Instance.addEntry(new PeerServerEntry(enclosingInstance));
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener5' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener5
		{
			public AnonymousClassActionListener5(ServerAddressBook enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerAddressBook enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerAddressBook enclosingInstance;
			public ServerAddressBook Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs arg0)
			{
				Enclosing_Instance.addEntry(new JabberEntry(enclosingInstance));
			}
		}
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state376 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private void  InitBlock()
		{
			LEAF_ICON_SIZE = IconFamily.SMALL;
			CONTROLS_ICON_SIZE = IconFamily.XSMALL;
			//UPGRADE_ISSUE: Constructor 'java.beans.PropertyChangeSupport.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			changeSupport = new PropertyChangeSupport(this);
		}
		[System.ComponentModel.Browsable(true)]
		public  event SupportClass.PropertyChangeEventHandler PropertyChange;
		public static ServerAddressBook Instance
		{
			get
			{
				return instance;
			}
			
		}
		public static System.String LocalAddress
		{
			get
			{
				if (localIPAddress == null)
				{
					try
					{
						localIPAddress = LocalHostLANAddress.ToString();
					}
					catch (System.Exception e)
					{
						localIPAddress = "?"; //$NON-NLS-1$
					}
				}
				return localIPAddress;
			}
			
		}
		/// <summary> Returns an <code>InetAddress</code> object encapsulating what is most likely the machine's LAN IP address.
		/// <p/>
		/// This method is intended for use as a replacement of JDK method <code>InetAddress.getLocalHost</code>, because
		/// that method is ambiguous on Linux systems. Linux systems enumerate the loopback network interface the same
		/// way as regular LAN network interfaces, but the JDK <code>InetAddress.getLocalHost</code> method does not
		/// specify the algorithm used to select the address returned under such circumstances, and will often return the
		/// loopback address, which is not valid for network communication. Details
		/// <a href="http://bugs.sun.com/bugdatabase/view_bug.do?bug_id=4665037">here</a>.
		/// <p/>
		/// This method will scan all IP addresses on all network interfaces on the host machine to determine the IP address
		/// most likely to be the machine's LAN address. If the machine has multiple IP addresses, this method will prefer
		/// a site-local IP address (e.g. 192.168.x.x or 10.10.x.x, usually IPv4) if the machine has one (and will return the
		/// first site-local address if the machine has more than one), but if the machine does not hold a site-local
		/// address, this method will return simply the first non-loopback address found (IPv4 or IPv6).
		/// <p/>
		/// If this method cannot find a non-loopback address using this selection algorithm, it will fall back to
		/// calling and returning the result of JDK method <code>InetAddress.getLocalHost</code>.
		/// <p/>
		/// 
		/// </summary>
		/// <throws>  UnknownHostException If the LAN address of the machine cannot be found. </throws>
		/// <summary> 
		/// Thanks to https://issues.apache.org/jira/browse/JCS-40 for this code
		/// </summary>
		private static System.Net.IPAddress LocalHostLANAddress
		{
			get
			{
				try
				{
					System.Net.IPAddress candidateAddress = null;
					// Iterate all NICs (network interface cards)...
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
					if (candidateAddress != null)
					{
						// We did not find a site-local address, but we found some other non-loopback address.
						// Server might have a non-site-local address assigned to its NIC (or it might be running
						// IPv6 which deprecates the "site-local" concept).
						// Return this non-loopback candidate address...
						return candidateAddress;
					}
					// At this point, we did not find a non-loopback address.
					// Fall back to returning whatever InetAddress.getLocalHost() returns...
					//UPGRADE_TODO: The equivalent in .NET for method 'java.net.InetAddress.getLocalHost' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.Net.IPAddress jdkSuppliedAddress = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0];
					if (jdkSuppliedAddress == null)
					{
						throw new System.Exception("The JDK InetAddress.getLocalHost() method unexpectedly returned null.");
					}
					return jdkSuppliedAddress;
				}
				catch (System.Exception e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.Exception unknownHostException = new System.Exception("Failed to determine LAN address: " + e);
					unknownHostException.initCause(e);
					throw unknownHostException;
				}
			}
			
		}
		virtual public System.Windows.Forms.Control Controls
		{
			get
			{
				if (controls == null)
				{
					
					controls = new JPanel(new MigLayout());
					addressConfig = new StringConfigurer(ADDRESS_PREF, null, ""); //$NON-NLS-1$
					Prefs.GlobalPrefs.addOption(null, addressConfig);
					//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
					addressBook = new System.Windows.Forms.ListBox.ObjectCollection(new System.Windows.Forms.ListBox());
					loadAddressBook();
					System.Windows.Forms.ListBox temp_ListBox;
					temp_ListBox = new System.Windows.Forms.ListBox();
					temp_ListBox.Items.AddRange(addressBook);
					temp_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
					myList = temp_ListBox;
					//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.ListSelectionModel.SINGLE_SELECTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					myList.SelectionMode = (System.Windows.Forms.SelectionMode) System.Windows.Forms.SelectionMode.One;
					//UPGRADE_ISSUE: Method 'javax.swing.JList.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJListsetCellRenderer_javaxswingListCellRenderer'"
					myList.setCellRenderer(new MyRenderer(this));
					myList.SelectedValueChanged += new System.EventHandler(new AnonymousClassListSelectionListener(this).valueChanged);
					myList.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.chat.ServerAddressBook.mouseDown);
					myList.Click += new System.EventHandler(new AnonymousClassMouseAdapter(this).mouseClicked);
					
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'scroll '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
					System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
					temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
					temp_scrollablecontrol.AutoScroll = true;
					temp_scrollablecontrol.Controls.Add(myList);
					System.Windows.Forms.ScrollableControl scroll = temp_scrollablecontrol;
					//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
					myList.Refresh();
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					controls.Controls.Add(scroll);
					scroll.Dock = new System.Windows.Forms.DockStyle();
					scroll.BringToFront(); //$NON-NLS-1$
					
					setButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString("ServerAddressBook.set_current")); //$NON-NLS-1$
					if (setButton is VassalSharp.tools.LaunchButton)
						((VassalSharp.tools.LaunchButton) setButton).setToolTipText(Resources.getString("ServerAddressBook.set_selected_server"));
					else
						SupportClass.ToolTipSupport.setToolTipText(setButton, Resources.getString("ServerAddressBook.set_selected_server")); //$NON-NLS-1$
					setButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(setButton);
					
					addButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.ADD));
					if (addButton is VassalSharp.tools.LaunchButton)
						((VassalSharp.tools.LaunchButton) addButton).setToolTipText(Resources.getString("ServerAddressBook.add_jabber_server"));
					else
						SupportClass.ToolTipSupport.setToolTipText(addButton, Resources.getString("ServerAddressBook.add_jabber_server")); //$NON-NLS-1$
					addButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(addButton);
					
					removeButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.REMOVE));
					if (removeButton is VassalSharp.tools.LaunchButton)
						((VassalSharp.tools.LaunchButton) removeButton).setToolTipText(Resources.getString("ServerAddressBook.remove_selected_server"));
					else
						SupportClass.ToolTipSupport.setToolTipText(removeButton, Resources.getString("ServerAddressBook.remove_selected_server")); //$NON-NLS-1$
					removeButton.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(removeButton);
					
					editButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.EDIT));
					if (editButton is VassalSharp.tools.LaunchButton)
						((VassalSharp.tools.LaunchButton) editButton).setToolTipText(Resources.getString("ServerAddressBook.edit_server"));
					else
						SupportClass.ToolTipSupport.setToolTipText(editButton, Resources.getString("ServerAddressBook.edit_server")); //$NON-NLS-1$
					editButton.Click += new System.EventHandler(new AnonymousClassActionListener3(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(editButton);
					
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					controls.Controls.Add(addButton);
					addButton.Dock = new System.Windows.Forms.DockStyle();
					addButton.BringToFront(); //$NON-NLS-1$
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					controls.Controls.Add(editButton);
					editButton.Dock = new System.Windows.Forms.DockStyle();
					editButton.BringToFront(); //$NON-NLS-1$
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					controls.Controls.Add(removeButton);
					removeButton.Dock = new System.Windows.Forms.DockStyle();
					removeButton.BringToFront(); //$NON-NLS-1$
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					controls.Controls.Add(setButton);
					setButton.Dock = new System.Windows.Forms.DockStyle();
					setButton.BringToFront(); //$NON-NLS-1$
					
					updateButtonVisibility();
				}
				return controls;
			}
			
		}
		virtual public bool Enabled
		{
			get
			{
				return enabled;
			}
			
			set
			{
				enabled = value;
				updateButtonVisibility();
			}
			
		}
		virtual public bool Frozen
		{
			set
			{
				frozen = value;
			}
			
		}
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		virtual protected internal System.Collections.Specialized.NameValueCollection CurrentServerProperties
		{
			get
			{
				return currentEntry.Properties;
			}
			
		}
		virtual public System.Drawing.Image CurrentIcon
		{
			get
			{
				return currentEntry.getIcon(CONTROLS_ICON_SIZE);
			}
			
		}
		virtual public System.String CurrentDescription
		{
			get
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return currentEntry.ToString();
			}
			
		}
		/// <summary> Set up the default server</summary>
		/// <returns>
		/// </returns>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		virtual public System.Collections.Specialized.NameValueCollection DefaultServerProperties
		{
			get
			{
				// return (new VassalJabberEntry()).getProperties();
				return (new LegacyEntry(this)).Properties;
			}
			
		}
		public const System.String CURRENT_SERVER = "currentServer"; //$NON-NLS-1$
		protected internal const System.String ADDRESS_PREF = "ServerAddressBook"; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'LEGACY_TYPE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'LEGACY_TYPE' was moved to static method 'VassalSharp.chat.ServerAddressBook'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String LEGACY_TYPE;
		//UPGRADE_NOTE: Final was removed from the declaration of 'DYNAMIC_TYPE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'DYNAMIC_TYPE' was moved to static method 'VassalSharp.chat.ServerAddressBook'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String DYNAMIC_TYPE;
		//UPGRADE_NOTE: Final was removed from the declaration of 'JABBER_TYPE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'JABBER_TYPE' was moved to static method 'VassalSharp.chat.ServerAddressBook'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String JABBER_TYPE;
		//UPGRADE_NOTE: Final was removed from the declaration of 'P2P_TYPE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'P2P_TYPE' was moved to static method 'VassalSharp.chat.ServerAddressBook'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String P2P_TYPE;
		//UPGRADE_NOTE: Final was removed from the declaration of 'P2P_MODE_KEY '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'P2P_MODE_KEY' was moved to static method 'VassalSharp.chat.ServerAddressBook'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String P2P_MODE_KEY;
		//UPGRADE_NOTE: Final was removed from the declaration of 'P2P_SERVER_MODE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'P2P_SERVER_MODE' was moved to static method 'VassalSharp.chat.ServerAddressBook'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String P2P_SERVER_MODE;
		//UPGRADE_NOTE: Final was removed from the declaration of 'P2P_CLIENT_MODE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'P2P_CLIENT_MODE' was moved to static method 'VassalSharp.chat.ServerAddressBook'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String P2P_CLIENT_MODE;
		// protected static final String PRIVATE_TYPE = PrivateClientFactory.PRIVATE_TYPE;
		//UPGRADE_NOTE: Final was removed from the declaration of 'TYPE_KEY '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'TYPE_KEY' was moved to static method 'VassalSharp.chat.ServerAddressBook'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String TYPE_KEY;
		protected internal const System.String DESCRIPTION_KEY = "description"; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'LEAF_ICON_SIZE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'LEAF_ICON_SIZE' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal int LEAF_ICON_SIZE;
		//UPGRADE_NOTE: Final was removed from the declaration of 'CONTROLS_ICON_SIZE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'CONTROLS_ICON_SIZE' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal int CONTROLS_ICON_SIZE;
		
		private bool frozen;
		private System.Windows.Forms.Control controls;
		private StringConfigurer addressConfig;
		private System.Windows.Forms.ListBox myList;
		//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
		private System.Windows.Forms.ListBox.ObjectCollection addressBook;
		private AddressBookEntry currentEntry;
		private bool enabled = true;
		//UPGRADE_ISSUE: Class 'java.beans.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
		//UPGRADE_NOTE: The initialization of  'changeSupport' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private PropertyChangeSupport changeSupport;
		private static ServerAddressBook instance;
		private static System.String localIPAddress;
		private static System.String externalIPAddress;
		
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button removeButton;
		private System.Windows.Forms.Button editButton;
		private System.Windows.Forms.Button setButton;
		
		public static void  editCurrentServer(bool connected)
		{
			instance.editCurrent(connected);
		}
		
		public static void  changeServerPopup(System.Windows.Forms.Control source)
		{
			instance.showPopup(source);
		}
		
		public static System.String getExternalAddress()
		{
			return getExternalAddress("?"); //$NON-NLS-1$
		}
		
		public static System.String getExternalAddress(System.String dflt)
		{
			if (externalIPAddress == null)
			{
				externalIPAddress = dflt;
				try
				{
					externalIPAddress = discoverMyIpAddressFromRemote();
				}
				catch (System.IO.IOException e)
				{
					externalIPAddress = "?"; //$NON-NLS-1$
				}
			}
			return externalIPAddress;
		}
		
		private static System.String discoverMyIpAddressFromRemote()
		{
			System.String theIp = null;
			HttpRequestWrapper r = new HttpRequestWrapper("http://www.vassalengine.org/util/getMyAddress"); //$NON-NLS-1$
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			if (!l.isEmpty())
			{
				theIp = l.get_Renamed(0);
			}
			else
			{
				throw new System.IO.IOException(Resources.getString("Server.empty_response")); //$NON-NLS-1$
			}
			return theIp;
		}
		
		public ServerAddressBook()
		{
			InitBlock();
			instance = this;
		}
		
		private void  updateButtonVisibility()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'index '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int index = myList.SelectedIndex;
			if (index >= 0)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				AddressBookEntry e = (AddressBookEntry) addressBook[index];
				editButton.Enabled = e.Editable && (Enabled || !e.Current);
				removeButton.Enabled = e.isRemovable() && !e.Current;
				setButton.Enabled = Enabled && !e.Current;
			}
			else
			{
				editButton.Enabled = false;
				removeButton.Enabled = false;
				setButton.Enabled = false;
			}
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual void  setCurrentServer(System.Collections.Specialized.NameValueCollection p)
		{
			
			// Check for Basic Types, regardless of other properties
			int index = 0;
			//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String type = p.Get(TYPE_KEY);
			//UPGRADE_NOTE: Final was removed from the declaration of 'dtype '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String dtype = p.Get(DYNAMIC_TYPE);
			//UPGRADE_NOTE: Final was removed from the declaration of 'ctype '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String ctype = p.Get(P2P_MODE_KEY);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Enumeration < ? > e = addressBook.elements();
			e.hasMoreElements();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				AddressBookEntry entry = (AddressBookEntry) e.nextElement();
				//UPGRADE_NOTE: Final was removed from the declaration of 'ep '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				System.Collections.Specialized.NameValueCollection ep = entry.Properties;
				
				if (SupportClass.EqualsSupport.Equals(ep, (System.Collections.IDictionary) p))
				{
					setCurrentServer(index);
					return ;
				}
				else if (DYNAMIC_TYPE.Equals(type) && DYNAMIC_TYPE.Equals(ep.Get(TYPE_KEY)) && ep.Get(DYNAMIC_TYPE).Equals(dtype))
				{
					setCurrentServer(index);
					return ;
				}
				else if (P2P_TYPE.Equals(type) && P2P_TYPE.Equals(ep.Get(TYPE_KEY)) && ep.Get(P2P_MODE_KEY).Equals(ctype))
				{
					setCurrentServer(index);
				}
				
				index++;
			}
			
			// Some Server we don't know about, add a server entry
			//UPGRADE_NOTE: Final was removed from the declaration of 'newEntry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			AddressBookEntry newEntry = buildEntry(p);
			if (newEntry != null)
			{
				addressBook.Add(newEntry);
				setCurrentServer(addressBook.IndexOf(newEntry));
			}
			saveAddressBook();
		}
		
		private void  setCurrentServer(AddressBookEntry e)
		{
			setCurrentServer(addressBook.IndexOf(e));
		}
		
		private void  setCurrentServer(int index)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			AddressBookEntry e = (AddressBookEntry) addressBook[index];
			if (currentEntry != null)
			{
				currentEntry.Current = false;
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'oldProps '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Collections.Specialized.NameValueCollection oldProps = currentEntry == null?null:currentEntry.Properties;
			currentEntry = e;
			currentEntry.Current = true;
			if (!frozen)
			{
				SupportClass.PropertyChangingEventArgs me41 = new SupportClass.PropertyChangingEventArgs(CURRENT_SERVER, oldProps, e.Properties);
				if (PropertyChange != null)
					PropertyChange(this, me41);
			}
			updateButtonVisibility();
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			myList.Refresh();
		}
		
		public virtual void  showPopup(System.Windows.Forms.Control source)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'popup '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Windows.Forms.ContextMenu popup = new System.Windows.Forms.ContextMenu();
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Enumeration < ? > e = addressBook.elements();
			e.hasMoreElements();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				AddressBookEntry entry = (AddressBookEntry) e.nextElement();
				//UPGRADE_NOTE: Final was removed from the declaration of 'item '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Windows.Forms.MenuItem item = new System.Windows.Forms.MenuItem(entry.ToString());
				//UPGRADE_NOTE: Final was removed from the declaration of 'action '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SupportClass.ActionSupport action = new MenuAction(this, entry);
				item.Click += new System.EventHandler(action.actionPerformed);
				item.Text = action.Description;
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractButton.setIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractButtonsetIcon_javaxswingIcon'"
				item.setIcon(entry.getIcon(IconFamily.SMALL));
				popup.MenuItems.Add(item);
			}
			//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.show' was converted to 'System.Windows.Forms.ContextMenu.Show' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenushow_javaawtComponent_int_int'"
			popup.Show(source, new System.Drawing.Point(0, 0));
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MenuAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class MenuAction:SupportClass.ActionSupport
		{
			private void  InitBlock(ServerAddressBook enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerAddressBook enclosingInstance;
			public ServerAddressBook Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			private AddressBookEntry entry;
			
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			public MenuAction(ServerAddressBook enclosingInstance, AddressBookEntry e):base(e.ToString())
			{
				InitBlock(enclosingInstance);
				entry = e;
			}
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				ServerAddressBook.Instance.setCurrentServer(entry);
			}
		}
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  addPropertyChangeListener(PropertyChangeListener l)
		{
			//UPGRADE_ISSUE: Method 'java.beans.PropertyChangeSupport.addPropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			changeSupport.addPropertyChangeListener(l);
		}
		
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  removePropertyChangeListener(PropertyChangeListener l)
		{
			//UPGRADE_ISSUE: Method 'java.beans.PropertyChangeSupport.removePropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			changeSupport.removePropertyChangeListener(l);
		}
		
		private void  editCurrent(bool connected)
		{
			if (currentEntry != null)
			{
				editServer(addressBook.IndexOf(currentEntry), connected);
			}
		}
		
		private void  editServer(int index)
		{
			editServer(index, true);
		}
		
		private void  editServer(int index, bool enabled)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			AddressBookEntry e = (AddressBookEntry) addressBook[index];
			//UPGRADE_NOTE: Final was removed from the declaration of 'current '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			bool current = e.Equals(currentEntry);
			//UPGRADE_NOTE: Final was removed from the declaration of 'oldProps '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Collections.Specialized.NameValueCollection oldProps = e.Properties;
			if (e.edit(enabled) && current)
			{
				SupportClass.PropertyChangingEventArgs me42 = new SupportClass.PropertyChangingEventArgs(CURRENT_SERVER, oldProps, e.Properties);
				if (PropertyChange != null)
					PropertyChange(this, me42);
			}
		}
		
		private void  removeServer(int index)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			AddressBookEntry e = (AddressBookEntry) addressBook[index];
			int i = JOptionPane.showConfirmDialog(GameModule.getGameModule().getFrame(), Resources.getString("ServerAddressBook.remove_server", e.Description)); //$NON-NLS-1$
			if (i == 0)
			{
				addressBook.RemoveAt(index);
				//UPGRADE_TODO: Method 'javax.swing.JList.setSelectedIndex' was converted to 'System.Windows.Forms.ListBox.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetSelectedIndex_int'"
				myList.SelectedIndex = - 1;
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				myList.Refresh();
				updateButtonVisibility();
				saveAddressBook();
			}
		}
		
		private void  addServer()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'popup '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Windows.Forms.ContextMenu popup = new System.Windows.Forms.ContextMenu();
			//UPGRADE_NOTE: Final was removed from the declaration of 'p2pItem '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem p2pItem = new System.Windows.Forms.MenuItem(Resources.getString("ServerAddressBook.peer_server"));
			p2pItem.Click += new System.EventHandler(new AnonymousClassActionListener4(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(p2pItem);
			//UPGRADE_NOTE: Final was removed from the declaration of 'jabItem '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem jabItem = new System.Windows.Forms.MenuItem(Resources.getString("ServerAddressBook.jabber_server"));
			jabItem.Click += new System.EventHandler(new AnonymousClassActionListener5(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(jabItem);
			//    final JMenuItem privateItem = new JMenuItem(Resources.getString("ServerAddressBook.private_server"));
			//    privateItem.addActionListener(new ActionListener() {
			//      public void actionPerformed(ActionEvent arg0) {
			//        addEntry(new PrivateEntry());
			//      }});
			popup.MenuItems.Add(p2pItem);
			//    popup.add(privateItem);
			popup.MenuItems.Add(jabItem);
			//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.show' was converted to 'System.Windows.Forms.ContextMenu.Show' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenushow_javaawtComponent_int_int'"
			popup.Show(addButton, new System.Drawing.Point(0, 0));
		}
		private void  addEntry(AddressBookEntry e)
		{
			if (e.edit())
			{
				addressBook.Add(e);
				saveAddressBook();
			}
		}
		
		private void  loadAddressBook()
		{
			decodeAddressBook(addressConfig.ValueString);
			
			// Remove any PeerClientEntry's, these are obsolete
			//UPGRADE_NOTE: Final was removed from the declaration of 'newAddressBook '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
			System.Windows.Forms.ListBox.ObjectCollection newAddressBook = new System.Windows.Forms.ListBox.ObjectCollection(new System.Windows.Forms.ListBox());
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Enumeration < ? > e = addressBook.elements();
			e.hasMoreElements();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				AddressBookEntry entry = (AddressBookEntry) e.nextElement();
				if (entry is LegacyEntry)
				{
					newAddressBook.Insert(0, entry);
				}
				else if (!(entry is PeerClientEntry))
				{
					newAddressBook.Add(entry);
				}
			}
			addressBook = newAddressBook;
			
			
			// Ensure that the Address Book has the basic
			// servers in it.
			bool legacy = false;
			bool jabber = false;
			bool peerServer = false;
			// boolean peerClient = false;
			bool updated = false;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Enumeration < ? > e = addressBook.elements();
			e.hasMoreElements();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				AddressBookEntry entry = (AddressBookEntry) e.nextElement();
				if (entry is LegacyEntry)
				{
					legacy = true;
				}
				else if (entry is VassalJabberEntry)
				{
					jabber = true;
				}
				else if (entry is PeerServerEntry)
				{
					peerServer = true;
				}
				//      else if (entry instanceof PeerClientEntry) {
				//        peerClient = true;
				//      }
			}
			if (!jabber)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				AddressBookEntry entry = new VassalJabberEntry(this);
				entry.Current = true;
				currentEntry = entry;
				addressBook.Add(entry);
				updated = true;
			}
			if (!legacy)
			{
				addressBook.Add(new LegacyEntry(this));
				updated = true;
			}
			if (!peerServer)
			{
				addressBook.Add(new PeerServerEntry(this));
				updated = true;
			}
			//    if (!peerClient) {
			//      addressBook.addElement(new PeerClientEntry());
			//      updated = true;
			//    }
			if (updated)
			{
				saveAddressBook();
			}
		}
		
		private void  saveAddressBook()
		{
			addressConfig.setValue(encodeAddressBook());
			if (myList != null)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				myList.Refresh();
			}
		}
		
		private System.String encodeAddressBook()
		{
			SequenceEncoder se = new SequenceEncoder(',');
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Enumeration < ? > e = addressBook.elements();
			e.hasMoreElements();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				AddressBookEntry entry = (AddressBookEntry) e.nextElement();
				if (entry != null)
				{
					se.append(entry.encode());
				}
			}
			return se.Value;
		}
		
		private void  decodeAddressBook(System.String s)
		{
			addressBook.Clear();
			for (SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, ','); sd.hasMoreTokens(); )
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'token '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String token = sd.nextToken(""); //$NON-NLS-1$
				if (token.Length > 0)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'entry '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					AddressBookEntry entry = buildEntry(token);
					if (entry != null)
					{
						addressBook.Add(buildEntry(token));
					}
				}
			}
		}
		
		/// <summary> Return an appropriately typed Entry, depending on the Server Properties
		/// passed
		/// 
		/// </summary>
		/// <param name="s">Encoded Server Properties
		/// </param>
		/// <returns> Entry
		/// </returns>
		private AddressBookEntry buildEntry(System.String s)
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			System.Collections.Specialized.NameValueCollection newProperties = new System.Collections.Specialized.NameValueCollection();
			try
			{
				newProperties = new PropertiesEncoder(s).Properties;
			}
			catch (System.IO.IOException e)
			{
				// FIXME: Error Message?
			}
			return buildEntry(newProperties);
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		private AddressBookEntry buildEntry(System.Collections.Specialized.NameValueCollection newProperties)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String type = newProperties.Get(TYPE_KEY);
			if (JABBER_TYPE.Equals(type))
			{
				return new JabberEntry(this, newProperties);
			}
			else if (DYNAMIC_TYPE.Equals(type))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'dtype '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String dtype = newProperties.Get(DYNAMIC_TYPE);
				if (JABBER_TYPE.Equals(dtype))
				{
					return new VassalJabberEntry(this, newProperties);
				}
				else if (LEGACY_TYPE.Equals(dtype))
				{
					return new LegacyEntry(this, newProperties);
				}
			}
			else if (P2P_TYPE.Equals(type))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'ctype '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String ctype = newProperties.Get(P2P_MODE_KEY);
				if (P2P_SERVER_MODE.Equals(ctype))
				{
					return new PeerServerEntry(this, newProperties);
				}
				else if (P2P_CLIENT_MODE.Equals(ctype))
				{
					return new PeerClientEntry(this, newProperties);
				}
			}
			return null;
		}
		
		/// <summary> Base class for an Address Book Entry
		/// 
		/// </summary>
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		abstract public class AddressBookEntry : System.IComparable
		{
			virtual protected internal System.String Description
			{
				get
				{
					return getProperty(VassalSharp.chat.ServerAddressBook.DESCRIPTION_KEY);
				}
				
				set
				{
					//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
					properties[VassalSharp.chat.ServerAddressBook.DESCRIPTION_KEY] = value;
				}
				
			}
			virtual protected internal bool Editable
			{
				get
				{
					return true;
				}
				
			}
			protected internal abstract System.String IconName{get;}
			virtual public System.String Type
			{
				get
				{
					return properties.Get(VassalSharp.chat.ServerAddressBook.TYPE_KEY);
				}
				
				set
				{
					//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
					properties[VassalSharp.chat.ServerAddressBook.TYPE_KEY] = value;
				}
				
			}
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			virtual public System.Collections.Specialized.NameValueCollection Properties
			{
				get
				{
					return properties;
				}
				
				set
				{
					properties = value;
				}
				
			}
			virtual public bool Current
			{
				get
				{
					return current;
				}
				
				set
				{
					current = value;
				}
				
			}
			virtual protected internal bool DescriptionEditable
			{
				get
				{
					return true;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< AddressBookEntry >
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			protected internal System.Collections.Specialized.NameValueCollection properties = new System.Collections.Specialized.NameValueCollection();
			protected internal bool current;
			
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			protected internal AddressBookEntry():this(enclosingInstance, new System.Collections.Specialized.NameValueCollection())
			{
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			protected internal AddressBookEntry(System.Collections.Specialized.NameValueCollection props)
			{
				properties = props;
			}
			
			public virtual System.String getProperty(System.String key)
			{
				return properties.Get(key);
			}
			
			public virtual void  setProperty(System.String key, System.String value_Renamed)
			{
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				properties[key] = value_Renamed;
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'isRemovable' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public virtual bool isRemovable()
			{
				return true;
			}
			
			protected internal virtual System.Drawing.Image getIcon(int size)
			{
				return IconFactory.getIcon(IconName, size);
			}
			
			public virtual System.String encode()
			{
				return new PropertiesEncoder(properties).StringValue;
			}
			
			public virtual int compareTo(AddressBookEntry target)
			{
				if (Type.Equals(target.Type))
				{
					return String.CompareOrdinal(Description, target.Description);
				}
				return String.CompareOrdinal(Type, target.Type);
			}
			
			public virtual bool edit()
			{
				return edit(true);
			}
			
			public virtual bool edit(bool enabled)
			{
				if (Editable)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'config '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					ServerConfig config = getEditor(Properties, enabled);
					//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Int32 result = (System.Int32) Dialogs.showDialog(null, Resources.getString("ServerAddressBook.edit_server_configuration"), config.Controls, (int) System.Windows.Forms.MessageBoxIcon.None, null, (int) System.Windows.Forms.MessageBoxButtons.OKCancel, null, (System.Object) null, (System.Object) null, null);
					//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
					if (result != null && result == 0)
					{
						if (enabled)
						{
							Properties = config.Properties;
							Enclosing_Instance.saveAddressBook();
						}
						return true;
					}
				}
				return false;
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			protected internal abstract void  setAdditionalProperties(System.Collections.Specialized.NameValueCollection props);
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			protected internal abstract void  getAdditionalProperties(System.Collections.Specialized.NameValueCollection props);
			
			protected internal abstract void  addAdditionalControls(System.Windows.Forms.Control c, bool enabled);
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			public virtual ServerConfig getEditor(System.Collections.Specialized.NameValueCollection p, bool enabled)
			{
				return new ServerConfig(this, p, this, enabled);
			}
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ServerConfig' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
			public class ServerConfig
			{
				private void  InitBlock(AddressBookEntry enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private AddressBookEntry enclosingInstance;
				virtual protected internal bool Enabled
				{
					get
					{
						return enabled;
					}
					
				}
				virtual public System.Windows.Forms.Control Controls
				{
					get
					{
						if (configControls == null)
						{
							configControls = new System.Windows.Forms.Panel();
							configControls.setLayout(new MigLayout("", "[align right]rel[]", "")); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
							System.Windows.Forms.Label temp_label2;
							temp_label2 = new System.Windows.Forms.Label();
							temp_label2.Image = IconFactory.getIcon(entry.IconName, IconFamily.LARGE);
							//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
							System.Windows.Forms.Control temp_Control;
							temp_Control = temp_label2;
							configControls.Controls.Add(temp_Control);
							temp_Control.Dock = new System.Windows.Forms.DockStyle();
							temp_Control.BringToFront(); //$NON-NLS-1$
							System.Windows.Forms.Label temp_label4;
							temp_label4 = new System.Windows.Forms.Label();
							temp_label4.Text = Resources.getString("Editor.description_label");
							//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
							System.Windows.Forms.Control temp_Control2;
							temp_Control2 = temp_label4;
							configControls.Controls.Add(temp_Control2); //$NON-NLS-1$
							//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
							configControls.Controls.Add(description);
							description.Dock = new System.Windows.Forms.DockStyle();
							description.BringToFront(); //$NON-NLS-1$
							entry.addAdditionalControls(configControls, enabled);
							description.ReadOnly = !(Enclosing_Instance.DescriptionEditable && Enabled);
						}
						return configControls;
					}
					
				}
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				virtual public System.Collections.Specialized.NameValueCollection Properties
				{
					get
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'props '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
						//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
						System.Collections.Specialized.NameValueCollection props = new System.Collections.Specialized.NameValueCollection();
						//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
						props[VassalSharp.chat.ServerAddressBook.DESCRIPTION_KEY] = description.Text;
						Enclosing_Instance.getAdditionalProperties(props);
						return props;
					}
					
				}
				public AddressBookEntry Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				protected internal System.Windows.Forms.Control configControls;
				protected internal System.Windows.Forms.TextBox description = new System.Windows.Forms.TextBox();
				protected internal AddressBookEntry entry;
				internal bool enabled;
				
				public ServerConfig(AddressBookEntry enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				public ServerConfig(AddressBookEntry enclosingInstance, System.Collections.Specialized.NameValueCollection props, AddressBookEntry entry, bool enabled):this(enclosingInstance)
				{
					this.entry = entry;
					this.enabled = enabled;
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					description.Text = props.Get(VassalSharp.chat.ServerAddressBook.DESCRIPTION_KEY);
					Enclosing_Instance.setAdditionalProperties(props);
				}
			}
			//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			abstract public System.Int32 CompareTo(System.Object obj);
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'JabberEntry' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Address Book entry for a user defined Jabber Server
		/// 
		/// </summary>
		private class JabberEntry:AddressBookEntry
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener6' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener6
			{
				public AnonymousClassActionListener6(JabberEntry enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(JabberEntry enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private JabberEntry enclosingInstance;
				public JabberEntry Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.test();
				}
			}
			private void  InitBlock(ServerAddressBook enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerAddressBook enclosingInstance;
			override protected internal System.String IconName
			{
				get
				{
					return "jabber"; //$NON-NLS-1$
				}
				
			}
			override protected internal bool DescriptionEditable
			{
				get
				{
					return true;
				}
				
			}
			public ServerAddressBook Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			private System.Windows.Forms.TextBox jabberHost = new System.Windows.Forms.TextBox();
			private System.Windows.Forms.TextBox jabberPort = new System.Windows.Forms.TextBox();
			private System.Windows.Forms.TextBox jabberUser = new System.Windows.Forms.TextBox();
			private System.Windows.Forms.TextBox jabberPw = new System.Windows.Forms.TextBox();
			private System.Windows.Forms.Button testButton;
			
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			public JabberEntry(ServerAddressBook enclosingInstance):this(enclosingInstance, new System.Collections.Specialized.NameValueCollection())
			{
				Type = VassalSharp.chat.ServerAddressBook.JABBER_TYPE;
				Description = ""; //$NON-NLS-1$
				setProperty(JabberClientFactory.JABBER_PORT, "5222"); //$NON-NLS-1$
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			public JabberEntry(ServerAddressBook enclosingInstance, System.Collections.Specialized.NameValueCollection props):base(props)
			{
				InitBlock(enclosingInstance);
			}
			
			public override System.String ToString()
			{
				return Resources.getString("ServerAddressBook.jabber_server") + " " + Description + " [" + getProperty(JabberClientFactory.JABBER_HOST) + ":" + getProperty(JabberClientFactory.JABBER_PORT) + " " + getProperty(JabberClientFactory.JABBER_LOGIN) + "/" + getProperty(JabberClientFactory.JABBER_PWD) + "]"; //$NON-NLS-1$
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			protected internal override void  setAdditionalProperties(System.Collections.Specialized.NameValueCollection props)
			{
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				jabberHost.Text = props.Get(JabberClientFactory.JABBER_HOST);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				jabberPort.Text = props.Get(JabberClientFactory.JABBER_PORT);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				jabberUser.Text = props.Get(JabberClientFactory.JABBER_LOGIN);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				jabberPw.Text = props.Get(JabberClientFactory.JABBER_PWD);
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			protected internal override void  getAdditionalProperties(System.Collections.Specialized.NameValueCollection props)
			{
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[JabberClientFactory.JABBER_HOST] = jabberHost.Text;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[JabberClientFactory.JABBER_PORT] = jabberPort.Text;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[JabberClientFactory.JABBER_LOGIN] = jabberUser.Text;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[JabberClientFactory.JABBER_PWD] = jabberPw.Text;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[VassalSharp.chat.ServerAddressBook.TYPE_KEY] = JabberClientFactory.JABBER_SERVER_TYPE;
			}
			
			protected internal override void  addAdditionalControls(System.Windows.Forms.Control c, bool enabled)
			{
				jabberHost.ReadOnly = !enabled;
				jabberPort.ReadOnly = !enabled;
				jabberUser.ReadOnly = !enabled;
				jabberPw.ReadOnly = !enabled;
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = Resources.getString("ServerAddressBook.jabber_host");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				c.Controls.Add(temp_Control); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				c.Controls.Add(jabberHost);
				jabberHost.Dock = new System.Windows.Forms.DockStyle();
				jabberHost.BringToFront(); //$NON-NLS-1$
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = Resources.getString("ServerAddressBook.port");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label4;
				c.Controls.Add(temp_Control2); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				c.Controls.Add(jabberPort);
				jabberPort.Dock = new System.Windows.Forms.DockStyle();
				jabberPort.BringToFront(); //$NON-NLS-1$
				System.Windows.Forms.Label temp_label6;
				temp_label6 = new System.Windows.Forms.Label();
				temp_label6.Text = Resources.getString("ServerAddressBook.user_name");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control3;
				temp_Control3 = temp_label6;
				c.Controls.Add(temp_Control3); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				c.Controls.Add(jabberUser);
				jabberUser.Dock = new System.Windows.Forms.DockStyle();
				jabberUser.BringToFront(); //$NON-NLS-1$
				System.Windows.Forms.Label temp_label8;
				temp_label8 = new System.Windows.Forms.Label();
				temp_label8.Text = Resources.getString("ServerAddressBook.password");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control4;
				temp_Control4 = temp_label8;
				c.Controls.Add(temp_Control4); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				c.Controls.Add(jabberPw);
				jabberPw.Dock = new System.Windows.Forms.DockStyle();
				jabberPw.BringToFront(); //$NON-NLS-1$
				
				testButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString("ServerAddressBook.test_connection")); //$NON-NLS-1$
				testButton.Click += new System.EventHandler(new AnonymousClassActionListener6(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(testButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				c.Controls.Add(testButton);
				testButton.Dock = new System.Windows.Forms.DockStyle();
				testButton.BringToFront(); //$NON-NLS-1$
			}
			
			protected internal virtual void  test()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.TextBox temp_TextBox;
				temp_TextBox = new System.Windows.Forms.TextBox();
				temp_TextBox.Multiline = true;
				temp_TextBox.WordWrap = false;
				temp_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
				System.Windows.Forms.TextBox result = temp_TextBox;
				result.setText(JabberClient.testConnection(jabberHost.Text, jabberPort.Text, jabberUser.Text, jabberPw.Text));
				try
				{
					Dialogs.showDialog(null, Resources.getString("ServerAddressBook.connection_test"), result, (int) System.Windows.Forms.MessageBoxIcon.Information, null, (int) System.Windows.Forms.MessageBoxButtons.OKCancel, null, (System.Object) null, (System.Object) null, null);
				}
				catch (System.SystemException ex)
				{
					SupportClass.WriteStackTrace(ex, Console.Error);
				}
			}
			//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			override public System.Int32 CompareTo(System.Object obj)
			{
				return 0;
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'VassalJabberEntry' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Address Book entry for the VASSAL Jabber server
		/// 
		/// </summary>
		private class VassalJabberEntry:AddressBookEntry
		{
			private void  InitBlock(ServerAddressBook enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerAddressBook enclosingInstance;
			override protected internal bool DescriptionEditable
			{
				get
				{
					return false;
				}
				
			}
			override protected internal System.String IconName
			{
				get
				{
					return "VASSAL-jabber"; //$NON-NLS-1$
				}
				
			}
			public ServerAddressBook Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			protected internal System.Windows.Forms.TextBox jabberUser = new System.Windows.Forms.TextBox();
			protected internal System.Windows.Forms.TextBox jabberPw = new System.Windows.Forms.TextBox();
			
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			public VassalJabberEntry(ServerAddressBook enclosingInstance):this(enclosingInstance, new System.Collections.Specialized.NameValueCollection())
			{
				Description = "VASSAL" + Resources.getString("ServerAddressBook.jabber_server"); //$NON-NLS-1$ //$NON-NLS-2$
				Type = VassalSharp.chat.ServerAddressBook.DYNAMIC_TYPE;
				setProperty(VassalSharp.chat.ServerAddressBook.DYNAMIC_TYPE, JabberClientFactory.JABBER_SERVER_TYPE);
				setProperty(JabberClientFactory.JABBER_LOGIN, ""); //$NON-NLS-1$
				setProperty(JabberClientFactory.JABBER_PWD, ""); //$NON-NLS-1$
				setProperty(DynamicClientFactory.URL, DynamicClient.JABBER_URL);
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			public VassalJabberEntry(ServerAddressBook enclosingInstance, System.Collections.Specialized.NameValueCollection props):base(props)
			{
				InitBlock(enclosingInstance);
			}
			
			public override System.String ToString()
			{
				System.String details;
				//UPGRADE_NOTE: Final was removed from the declaration of 'login '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String login = getProperty(JabberClientFactory.JABBER_LOGIN);
				//UPGRADE_NOTE: Final was removed from the declaration of 'pw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String pw = getProperty(JabberClientFactory.JABBER_PWD);
				if (login == null || login.Length == 0 || pw == null || pw.Length == 0)
				{
					details = Resources.getString("ServerAddressBook.login_details_required"); //$NON-NLS-1$
				}
				else
				{
					details = getProperty(JabberClientFactory.JABBER_LOGIN) + "/" + getProperty(JabberClientFactory.JABBER_PWD);
				}
				return Description + " [" + details + "]"; //$NON-NLS-1$ //$NON-NLS-2$
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'isRemovable' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override bool isRemovable()
			{
				return false;
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			protected internal override void  setAdditionalProperties(System.Collections.Specialized.NameValueCollection props)
			{
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				jabberUser.Text = props.Get(JabberClientFactory.JABBER_LOGIN);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				jabberPw.Text = props.Get(JabberClientFactory.JABBER_PWD);
				Type = VassalSharp.chat.ServerAddressBook.DYNAMIC_TYPE;
				setProperty(VassalSharp.chat.ServerAddressBook.DYNAMIC_TYPE, VassalSharp.chat.ServerAddressBook.JABBER_TYPE);
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			protected internal override void  getAdditionalProperties(System.Collections.Specialized.NameValueCollection props)
			{
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[JabberClientFactory.JABBER_LOGIN] = jabberUser.Text;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[JabberClientFactory.JABBER_PWD] = jabberPw.Text;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[VassalSharp.chat.ServerAddressBook.TYPE_KEY] = VassalSharp.chat.ServerAddressBook.DYNAMIC_TYPE;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[VassalSharp.chat.ServerAddressBook.DYNAMIC_TYPE] = VassalSharp.chat.ServerAddressBook.JABBER_TYPE;
			}
			
			protected internal override void  addAdditionalControls(System.Windows.Forms.Control c, bool enabled)
			{
				jabberUser.ReadOnly = !enabled;
				jabberPw.ReadOnly = !enabled;
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = Resources.getString("ServerAddressBook.user_name");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				c.Controls.Add(temp_Control); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				c.Controls.Add(jabberUser);
				jabberUser.Dock = new System.Windows.Forms.DockStyle();
				jabberUser.BringToFront(); //$NON-NLS-1$
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = Resources.getString("ServerAddressBook.password");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label4;
				c.Controls.Add(temp_Control2); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				c.Controls.Add(jabberPw);
				jabberPw.Dock = new System.Windows.Forms.DockStyle();
				jabberPw.BringToFront(); //$NON-NLS-1$
			}
			//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			override public System.Int32 CompareTo(System.Object obj)
			{
				return 0;
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'LegacyEntry' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Address Book entry for the VASSAL legacy server
		/// 
		/// </summary>
		private class LegacyEntry:AddressBookEntry
		{
			private void  InitBlock(ServerAddressBook enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerAddressBook enclosingInstance;
			override protected internal System.String IconName
			{
				get
				{
					return "VASSAL"; //$NON-NLS-1$
				}
				
			}
			override protected internal bool Editable
			{
				get
				{
					return false;
				}
				
			}
			override protected internal bool DescriptionEditable
			{
				get
				{
					return false;
				}
				
			}
			public ServerAddressBook Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			public LegacyEntry(ServerAddressBook enclosingInstance):this(enclosingInstance, new System.Collections.Specialized.NameValueCollection())
			{
				Description = Resources.getString("ServerAddressBook.legacy_server"); //$NON-NLS-1$
				Type = VassalSharp.chat.ServerAddressBook.DYNAMIC_TYPE;
				setProperty(DynamicClientFactory.DYNAMIC_TYPE, NodeClientFactory.NODE_TYPE);
				setProperty(DynamicClientFactory.URL, DynamicClient.LEGACY_URL);
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			public LegacyEntry(ServerAddressBook enclosingInstance, System.Collections.Specialized.NameValueCollection props):base(props)
			{
				InitBlock(enclosingInstance);
			}
			
			public override System.String ToString()
			{
				return Description;
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'isRemovable' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override bool isRemovable()
			{
				return false;
			}
			
			protected internal override void  addAdditionalControls(System.Windows.Forms.Control c, bool enabled)
			{
				
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			protected internal override void  getAdditionalProperties(System.Collections.Specialized.NameValueCollection props)
			{
				
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			protected internal override void  setAdditionalProperties(System.Collections.Specialized.NameValueCollection props)
			{
				
			}
			//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			override public System.Int32 CompareTo(System.Object obj)
			{
				return 0;
			}
		}
		
		/// <summary> Address Book entry for a Private VASSAL server
		/// 
		/// </summary>
		//  private class PrivateEntry extends AddressBookEntry {
		//
		//    private JTextField serverPort = new JTextField();
		//    private JTextField serverIp = new JTextField();
		//
		//    public PrivateEntry() {
		//      this(new Properties());
		//      setDescription(Resources.getString("ServerAddressBook.private_server")); //$NON-NLS-1$
		//      setType(PRIVATE_TYPE);
		//      setProperty(PrivateClientFactory.PORT, "5050"); //$NON-NLS-1$
		//    }
		//
		//    public PrivateEntry(Properties props) {
		//      super(props);
		//    }
		//
		//    public String toString() {
		//      return Resources.getString("ServerAddressBook.private_server") + " [" + getDescription() + "]";
		//    }
		//
		//    public String getDescription() {
		//      return super.getDescription() + " " + getProperty(PrivateClientFactory.URL) + ":" + getProperty(PrivateClientFactory.PORT);
		//    }
		//
		//    protected String getIconName() {
		//      return "VASSAL-private"; //$NON-NLS-1$
		//    }
		//
		//    protected boolean isRemovable() {
		//      return true;
		//    }
		//
		//    protected boolean isEditable() {
		//      return true;
		//    }
		//
		//    protected boolean isDescriptionEditable() {
		//      return true;
		//    }
		//
		//    protected void addAdditionalControls(JComponent c, boolean enabled) {
		//      serverIp.setEditable(enabled);
		//      c.add(new JLabel(Resources.getString("ServerAddressBook.server_ip"))); //$NON-NLS-1$
		//      c.add(serverIp, "wrap, growx, push"); //$NON-NLS-1$
		//
		//      serverPort.setEditable(enabled);
		//      c.add(new JLabel(Resources.getString("ServerAddressBook.server_port"))); //$NON-NLS-1$
		//      c.add(serverPort, "wrap, growx, push"); //$NON-NLS-1$
		//    }
		//
		//    protected void getAdditionalProperties(Properties props) {
		//      props.setProperty(PrivateClientFactory.URL, serverIp.getText());
		//      props.setProperty(PrivateClientFactory.PORT, serverPort.getText());
		//      props.setProperty(TYPE_KEY, PRIVATE_TYPE);
		//    }
		//
		//    protected void setAdditionalProperties(Properties props) {
		//      serverIp.setText(props.getProperty(PrivateClientFactory.URL));
		//      serverPort.setText(props.getProperty(PrivateClientFactory.PORT));
		//    }
		//
		//  }
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'PeerServerEntry' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Address Book Entry for a Peer to Peer connection
		/// 
		/// </summary>
		private class PeerServerEntry:AddressBookEntry
		{
			private void  InitBlock(ServerAddressBook enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerAddressBook enclosingInstance;
			override protected internal bool DescriptionEditable
			{
				get
				{
					return true;
				}
				
			}
			override protected internal System.String IconName
			{
				get
				{
					return "network-server"; //$NON-NLS-1$
				}
				
			}
			public ServerAddressBook Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			private System.Windows.Forms.TextBox listenPort = new System.Windows.Forms.TextBox();
			private System.Windows.Forms.TextBox serverPw = new System.Windows.Forms.TextBox();
			
			public PeerServerEntry(ServerAddressBook enclosingInstance):base()
			{
				InitBlock(enclosingInstance);
				Description = Resources.getString("ServerAddressBook.peer_server"); //$NON-NLS-1$
				Type = VassalSharp.chat.ServerAddressBook.P2P_TYPE;
				setProperty(VassalSharp.chat.ServerAddressBook.P2P_MODE_KEY, VassalSharp.chat.ServerAddressBook.P2P_SERVER_MODE);
				setProperty(P2PClientFactory.P2P_LISTEN_PORT, "5050"); //$NON-NLS-1$
				setProperty(P2PClientFactory.P2P_SERVER_PW, "xyzzy"); //$NON-NLS-1$
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			public PeerServerEntry(ServerAddressBook enclosingInstance, System.Collections.Specialized.NameValueCollection props):base(props)
			{
				InitBlock(enclosingInstance);
			}
			
			public override System.String ToString()
			{
				return Resources.getString("ServerAddressBook.peer_server") + " [" + getProperty(VassalSharp.chat.ServerAddressBook.DESCRIPTION_KEY) + "]";
			}
			
			public override bool isRemovable()
			{
				return true;
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			protected internal override void  setAdditionalProperties(System.Collections.Specialized.NameValueCollection p)
			{
				Type = VassalSharp.chat.ServerAddressBook.P2P_TYPE;
				setProperty(VassalSharp.chat.ServerAddressBook.P2P_MODE_KEY, VassalSharp.chat.ServerAddressBook.P2P_SERVER_MODE);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				listenPort.Text = p.Get(P2PClientFactory.P2P_LISTEN_PORT);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				serverPw.Text = p.Get(P2PClientFactory.P2P_SERVER_PW);
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			protected internal override void  getAdditionalProperties(System.Collections.Specialized.NameValueCollection props)
			{
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[VassalSharp.chat.ServerAddressBook.TYPE_KEY] = VassalSharp.chat.ServerAddressBook.P2P_TYPE;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[VassalSharp.chat.ServerAddressBook.P2P_MODE_KEY] = VassalSharp.chat.ServerAddressBook.P2P_SERVER_MODE;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[P2PClientFactory.P2P_LISTEN_PORT] = listenPort.Text;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[P2PClientFactory.P2P_SERVER_PW] = serverPw.Text;
			}
			
			protected internal override void  addAdditionalControls(System.Windows.Forms.Control c, bool enabled)
			{
				listenPort.ReadOnly = !enabled;
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = Resources.getString("ServerAddressBook.listen_port");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				c.Controls.Add(temp_Control); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				c.Controls.Add(listenPort);
				listenPort.Dock = new System.Windows.Forms.DockStyle();
				listenPort.BringToFront(); //$NON-NLS-1$
				
				serverPw.ReadOnly = !enabled;
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = Resources.getString("ServerAddressBook.server_password");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label4;
				c.Controls.Add(temp_Control2); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				c.Controls.Add(serverPw);
				serverPw.Dock = new System.Windows.Forms.DockStyle();
				serverPw.BringToFront(); //$NON-NLS-1$
				
				System.Windows.Forms.Label temp_label6;
				temp_label6 = new System.Windows.Forms.Label();
				temp_label6.Text = Resources.getString("Peer2Peer.internet_address");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control3;
				temp_Control3 = temp_label6;
				c.Controls.Add(temp_Control3); //$NON-NLS-1$
				//UPGRADE_NOTE: Final was removed from the declaration of 'externalIP '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.TextBox temp_text_box;
				temp_text_box = new System.Windows.Forms.TextBox();
				temp_text_box.Text = VassalSharp.chat.ServerAddressBook.getExternalAddress();
				System.Windows.Forms.TextBox externalIP = temp_text_box;
				externalIP.ReadOnly = !false;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				c.Controls.Add(externalIP);
				externalIP.Dock = new System.Windows.Forms.DockStyle();
				externalIP.BringToFront(); //$NON-NLS-1$
				
				if (!VassalSharp.chat.ServerAddressBook.LocalAddress.Equals(VassalSharp.chat.ServerAddressBook.getExternalAddress()))
				{
					System.Windows.Forms.Label temp_label8;
					temp_label8 = new System.Windows.Forms.Label();
					temp_label8.Text = Resources.getString("Peer2Peer.local_address");
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control4;
					temp_Control4 = temp_label8;
					c.Controls.Add(temp_Control4); //$NON-NLS-1$
					//UPGRADE_NOTE: Final was removed from the declaration of 'localIP '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Windows.Forms.TextBox temp_text_box2;
					temp_text_box2 = new System.Windows.Forms.TextBox();
					temp_text_box2.Text = VassalSharp.chat.ServerAddressBook.LocalAddress;
					System.Windows.Forms.TextBox localIP = temp_text_box2;
					localIP.ReadOnly = !false;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					c.Controls.Add(localIP);
					localIP.Dock = new System.Windows.Forms.DockStyle();
					localIP.BringToFront(); //$NON-NLS-1$
				}
			}
			//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			override public System.Int32 CompareTo(System.Object obj)
			{
				return 0;
			}
		}
		
		/// <summary> Address Book Entry for a Peer to Peer connection in Client Mode
		/// 
		/// </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'PeerClientEntry' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class PeerClientEntry:AddressBookEntry
		{
			private void  InitBlock(ServerAddressBook enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerAddressBook enclosingInstance;
			override protected internal bool DescriptionEditable
			{
				get
				{
					return false;
				}
				
			}
			override protected internal System.String IconName
			{
				get
				{
					return "network-idle"; //$NON-NLS-1$
				}
				
			}
			public ServerAddressBook Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			private System.Windows.Forms.TextBox listenPort = new System.Windows.Forms.TextBox();
			private System.Windows.Forms.TextBox serverName = new System.Windows.Forms.TextBox();
			private System.Windows.Forms.TextBox serverPort = new System.Windows.Forms.TextBox();
			private System.Windows.Forms.TextBox serverIp = new System.Windows.Forms.TextBox();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			SuppressWarnings(unused)
			public PeerClientEntry(ServerAddressBook enclosingInstance):base()
			{
				InitBlock(enclosingInstance);
				Description = Resources.getString("ServerAddressBook.peer_client"); //$NON-NLS-1$
				Type = VassalSharp.chat.ServerAddressBook.P2P_TYPE;
				setProperty(VassalSharp.chat.ServerAddressBook.P2P_MODE_KEY, VassalSharp.chat.ServerAddressBook.P2P_CLIENT_MODE);
				setProperty(P2PClientFactory.P2P_LISTEN_PORT, "5050"); //$NON-NLS-1$
				setProperty(P2PClientFactory.P2P_SERVER_NAME, ""); //$NON-NLS-1$
				setProperty(P2PClientFactory.P2P_SERVER_PORT, "5050"); //$NON-NLS-1$
				setProperty(P2PClientFactory.P2P_SERVER_IP, ""); //$NON-NLS-1$
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			public PeerClientEntry(ServerAddressBook enclosingInstance, System.Collections.Specialized.NameValueCollection props):base(props)
			{
				InitBlock(enclosingInstance);
			}
			
			public override System.String ToString()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'listenPort '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String listenPort = getProperty(P2PClientFactory.P2P_LISTEN_PORT);
				//UPGRADE_NOTE: Final was removed from the declaration of 'serverName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String serverName = getProperty(P2PClientFactory.P2P_SERVER_NAME);
				//UPGRADE_NOTE: Final was removed from the declaration of 'serverIp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String serverIp = getProperty(P2PClientFactory.P2P_SERVER_IP);
				//UPGRADE_NOTE: Final was removed from the declaration of 'serverPort '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String serverPort = getProperty(P2PClientFactory.P2P_SERVER_PORT);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'desc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Text.StringBuilder desc = new System.Text.StringBuilder(Resources.getString("ServerAddressBook.peer_client"));
				
				if (serverIp == null || serverIp.Length == 0)
				{
					desc.Append(" [");
					desc.Append(Resources.getString("ServerAddressBook.listening", listenPort));
					desc.Append("]");
				}
				else
				{
					if (serverName != null && serverName.Length > 0)
					{
						desc.Append(" - ");
						desc.Append(serverName);
					}
					if (serverIp != null && serverIp.Length > 0)
					{
						desc.Append(" [");
						desc.Append(serverIp);
						desc.Append(":");
						desc.Append(serverPort);
						desc.Append("]");
					}
				}
				
				return desc.ToString();
			}
			
			public override bool isRemovable()
			{
				return true;
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			protected internal override void  setAdditionalProperties(System.Collections.Specialized.NameValueCollection p)
			{
				Type = VassalSharp.chat.ServerAddressBook.P2P_TYPE;
				setProperty(VassalSharp.chat.ServerAddressBook.P2P_MODE_KEY, VassalSharp.chat.ServerAddressBook.P2P_CLIENT_MODE);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				listenPort.Text = p.Get(P2PClientFactory.P2P_LISTEN_PORT);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				serverIp.Text = p.Get(P2PClientFactory.P2P_SERVER_IP);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				serverPort.Text = p.Get(P2PClientFactory.P2P_SERVER_PORT);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				serverName.Text = p.Get(P2PClientFactory.P2P_SERVER_NAME);
				Description = ToString();
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			protected internal override void  getAdditionalProperties(System.Collections.Specialized.NameValueCollection props)
			{
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[VassalSharp.chat.ServerAddressBook.TYPE_KEY] = VassalSharp.chat.ServerAddressBook.P2P_TYPE;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[VassalSharp.chat.ServerAddressBook.P2P_MODE_KEY] = VassalSharp.chat.ServerAddressBook.P2P_CLIENT_MODE;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[P2PClientFactory.P2P_LISTEN_PORT] = listenPort.Text;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[P2PClientFactory.P2P_SERVER_IP] = serverIp.Text;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[P2PClientFactory.P2P_SERVER_PORT] = serverPort.Text;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				props[P2PClientFactory.P2P_SERVER_NAME] = serverName.Text;
			}
			
			protected internal override void  addAdditionalControls(System.Windows.Forms.Control c, bool enabled)
			{
				serverName.ReadOnly = !enabled;
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = Resources.getString("ServerAddressBook.server_name");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				c.Controls.Add(temp_Control); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				c.Controls.Add(serverName);
				serverName.Dock = new System.Windows.Forms.DockStyle();
				serverName.BringToFront(); //$NON-NLS-1$
				
				serverIp.ReadOnly = !enabled;
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = Resources.getString("ServerAddressBook.server_ip");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label4;
				c.Controls.Add(temp_Control2); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				c.Controls.Add(serverIp);
				serverIp.Dock = new System.Windows.Forms.DockStyle();
				serverIp.BringToFront(); //$NON-NLS-1$
				
				serverPort.ReadOnly = !enabled;
				System.Windows.Forms.Label temp_label6;
				temp_label6 = new System.Windows.Forms.Label();
				temp_label6.Text = Resources.getString("ServerAddressBook.server_port");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control3;
				temp_Control3 = temp_label6;
				c.Controls.Add(temp_Control3); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				c.Controls.Add(serverPort);
				serverPort.Dock = new System.Windows.Forms.DockStyle();
				serverPort.BringToFront(); //$NON-NLS-1$
				
				listenPort.ReadOnly = !enabled;
				System.Windows.Forms.Label temp_label8;
				temp_label8 = new System.Windows.Forms.Label();
				temp_label8.Text = Resources.getString("ServerAddressBook.invite_port");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control4;
				temp_Control4 = temp_label8;
				c.Controls.Add(temp_Control4); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				c.Controls.Add(listenPort);
				listenPort.Dock = new System.Windows.Forms.DockStyle();
				listenPort.BringToFront(); //$NON-NLS-1$
				
				System.Windows.Forms.Label temp_label10;
				temp_label10 = new System.Windows.Forms.Label();
				temp_label10.Text = Resources.getString("Peer2Peer.internet_address");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control5;
				temp_Control5 = temp_label10;
				c.Controls.Add(temp_Control5); //$NON-NLS-1$
				//UPGRADE_NOTE: Final was removed from the declaration of 'externalIP '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.TextBox temp_text_box;
				temp_text_box = new System.Windows.Forms.TextBox();
				temp_text_box.Text = VassalSharp.chat.ServerAddressBook.getExternalAddress();
				System.Windows.Forms.TextBox externalIP = temp_text_box;
				externalIP.ReadOnly = !false;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				c.Controls.Add(externalIP);
				externalIP.Dock = new System.Windows.Forms.DockStyle();
				externalIP.BringToFront(); //$NON-NLS-1$
				
				if (!VassalSharp.chat.ServerAddressBook.LocalAddress.Equals(VassalSharp.chat.ServerAddressBook.getExternalAddress()))
				{
					System.Windows.Forms.Label temp_label12;
					temp_label12 = new System.Windows.Forms.Label();
					temp_label12.Text = Resources.getString("Peer2Peer.local_address");
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control6;
					temp_Control6 = temp_label12;
					c.Controls.Add(temp_Control6); //$NON-NLS-1$
					//UPGRADE_NOTE: Final was removed from the declaration of 'localIP '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Windows.Forms.TextBox temp_text_box2;
					temp_text_box2 = new System.Windows.Forms.TextBox();
					temp_text_box2.Text = VassalSharp.chat.ServerAddressBook.LocalAddress;
					System.Windows.Forms.TextBox localIP = temp_text_box2;
					localIP.ReadOnly = !false;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					c.Controls.Add(localIP);
					localIP.Dock = new System.Windows.Forms.DockStyle();
					localIP.BringToFront(); //$NON-NLS-1$
				}
			}
			//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			override public System.Int32 CompareTo(System.Object obj)
			{
				return 0;
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MyRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Customised List Cell Renderer for the JList display: - Display the Icon
		/// appropriate to the Server Entry - Highlight the currently selected server
		/// 
		/// </summary>
		//UPGRADE_ISSUE: Class 'javax.swing.DefaultListCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
		[Serializable]
		private class MyRenderer:DefaultListCellRenderer
		{
			public MyRenderer(ServerAddressBook enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerAddressBook enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerAddressBook enclosingInstance;
			public ServerAddressBook Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			private System.Drawing.Font standardFont;
			private System.Drawing.Font highlightFont;
			
			public System.Windows.Forms.Control getListCellRendererComponent(System.Windows.Forms.ListBox list, System.Object value_Renamed, int index, bool isSelected, bool cellHasFocus)
			{
				
				//UPGRADE_ISSUE: Method 'javax.swing.DefaultListCellRenderer.getListCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
				base.getListCellRendererComponent(list, value_Renamed, index, isSelected, cellHasFocus);
				
				if (standardFont == null)
				{
					standardFont = Font;
					//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
					highlightFont = new System.Drawing.Font(standardFont.FontFamily.Name, (int) standardFont.Size, (System.Drawing.FontStyle) ((int) System.Drawing.FontStyle.Bold + (int) System.Drawing.FontStyle.Italic));
				}
				
				if (value_Renamed is AddressBookEntry)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					AddressBookEntry e = (AddressBookEntry) value_Renamed;
					Image = e.getIcon(Enclosing_Instance.LEAF_ICON_SIZE);
					if (e.Current)
					{
						Font = highlightFont;
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						Text = e.ToString() + Resources.getString("ServerAddressBook.current"); //$NON-NLS-1$
					}
					else
					{
						Font = standardFont;
					}
				}
				return this;
			}
		}
		static ServerAddressBook()
		{
			LEGACY_TYPE = NodeClientFactory.NODE_TYPE;
			DYNAMIC_TYPE = DynamicClientFactory.DYNAMIC_TYPE;
			JABBER_TYPE = JabberClientFactory.JABBER_SERVER_TYPE;
			P2P_TYPE = P2PClientFactory.P2P_TYPE;
			P2P_MODE_KEY = P2PClientFactory.P2P_MODE_KEY;
			P2P_SERVER_MODE = P2PClientFactory.P2P_SERVER_MODE;
			P2P_CLIENT_MODE = P2PClientFactory.P2P_CLIENT_MODE;
			TYPE_KEY = ChatServerFactory.TYPE_KEY;
		}
	}
}