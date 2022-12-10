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
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
using Configurer = VassalSharp.configure.Configurer;
using Resources = VassalSharp.i18n.Resources;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.chat
{
	
	/// <summary> Improved version of ServerConfigurer that includes an Address Book of
	/// commonly visited Jabber servers and P2P clients.
	/// 
	/// </summary>
	public class AddressBookServerConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(AddressBookServerConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(AddressBookServerConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private AddressBookServerConfigurer enclosingInstance;
			public AddressBookServerConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				Enclosing_Instance.enableControls(true.Equals(evt.NewValue));
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener1
		{
			public AnonymousClassPropertyChangeListener1(AddressBookServerConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(AddressBookServerConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private AddressBookServerConfigurer enclosingInstance;
			public AddressBookServerConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				if (ServerAddressBook.CURRENT_SERVER.Equals(e.PropertyName))
				{
					Enclosing_Instance.addressBook.Frozen = true;
					//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
					Enclosing_Instance.setValue((System.Collections.Specialized.NameValueCollection) e.NewValue);
					Enclosing_Instance.addressBook.Frozen = false;
				}
			}
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				
				if (controls == null)
				{
					controls = new JPanel(new MigLayout());
					System.Windows.Forms.Label temp_label;
					temp_label = new System.Windows.Forms.Label();
					temp_label.Text = DISCONNECTED;
					header = temp_label;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					controls.Controls.Add(header);
					header.Dock = new System.Windows.Forms.DockStyle();
					header.BringToFront(); //$NON-NLS-1$
					addressBook = new ServerAddressBook();
					addressBook.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener1(this).propertyChange);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(addressBook.Controls);
				}
				
				return controls;
			}
			
		}
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		private System.Collections.Specialized.NameValueCollection ServerInfo
		{
			get
			{
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				System.Collections.Specialized.NameValueCollection p = (System.Collections.Specialized.NameValueCollection) getValue();
				if (p == null)
				{
					//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
					//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
					p = new System.Collections.Specialized.NameValueCollection();
				}
				return p;
			}
			
		}
		override public System.String ValueString
		{
			get
			{
				System.String s = ""; //$NON-NLS-1$
				System.IO.MemoryStream out_Renamed = new System.IO.MemoryStream();
				try
				{
					//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
					System.Collections.Specialized.NameValueCollection p = (System.Collections.Specialized.NameValueCollection) getValue();
					if (p != null)
					{
						//UPGRADE_ISSUE: Method 'java.util.Properties.store' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilPropertiesstore_javaioOutputStream_javalangString'"
						p.store(out_Renamed, null);
					}
					//UPGRADE_TODO: The differences in the Format  of parameters for constructor 'java.lang.String.String'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					s = System.Text.Encoding.GetEncoding(ENCODING).GetString(SupportClass.ToByteArray(SupportClass.ToSByteArray(out_Renamed.ToArray())));
				}
				// FIXME: review error message
				catch (System.IO.IOException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
				}
				// FIXME: review error message
				catch (System.IO.IOException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
				}
				finally
				{
					IOUtils.closeQuietly(out_Renamed);
				}
				return s;
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'CONNECTED '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'CONNECTED' was moved to static method 'VassalSharp.chat.AddressBookServerConfigurer'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly System.String CONNECTED; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'DISCONNECTED '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'DISCONNECTED' was moved to static method 'VassalSharp.chat.AddressBookServerConfigurer'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly System.String DISCONNECTED; //$NON-NLS-1$
		private const System.String ENCODING = "UTF-8"; //$NON-NLS-1$
		protected internal System.Windows.Forms.Control controls;
		protected internal ServerAddressBook addressBook;
		private HybridClient client;
		private System.Windows.Forms.Label header;
		
		public AddressBookServerConfigurer(System.String key, System.String name, HybridClient client):base(key, name, client)
		{
			this.client = client;
			client.addPropertyChangeListener(VassalSharp.build.module.ServerConnection_Fields.CONNECTED, new AnonymousClassPropertyChangeListener(this));
			System.Windows.Forms.Control generatedAux2 = Controls;
			setValue(addressBook.DefaultServerProperties);
			client.updateDisplayControls(addressBook.CurrentIcon, addressBook.CurrentDescription);
		}
		
		private void  enableControls(bool connected)
		{
			addressBook.Enabled = !connected;
			header.Text = connected?CONNECTED:DISCONNECTED;
		}
		
		public override void  setValue(System.Object o)
		{
			base.setValue(o);
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			if (!noUpdate && o is System.Collections.Specialized.NameValueCollection && controls != null)
			{
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				addressBook.setCurrentServer((System.Collections.Specialized.NameValueCollection) o);
			}
			if (client != null && !CONNECTED.Equals(header.Text))
			{
				client.Delegate = ChatServerFactory.build(ServerInfo);
				client.updateDisplayControls(addressBook.CurrentIcon, addressBook.CurrentDescription);
			}
		}
		
		public override void  setValue(System.String s)
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			System.Collections.Specialized.NameValueCollection p = new System.Collections.Specialized.NameValueCollection();
			try
			{
				//UPGRADE_TODO: Method 'java.lang.String.getBytes' was converted to 'System.Text.Encoding.GetEncoding(string).GetBytes(string)' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangStringgetBytes_javalangString'"
				new System.IO.MemoryStream(SupportClass.ToByteArray(SupportClass.ToSByteArray(System.Text.Encoding.GetEncoding(ENCODING).GetBytes(s))));
				//UPGRADE_TODO: Method 'java.util.Properties.load' was converted to 'System.Collections.Specialized.NameValueCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiesload_javaioInputStream'"
				p = new System.Collections.Specialized.NameValueCollection(System.Configuration.ConfigurationSettings.AppSettings);
			}
			// FIXME: review error message
			catch (System.IO.IOException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
			}
			// FIXME: review error message
			catch (System.IO.IOException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
			}
			setValue(p);
		}
		static AddressBookServerConfigurer()
		{
			CONNECTED = Resources.getString("Server.please_disconnect");
			DISCONNECTED = Resources.getString("ServerAddressBook.select_server");
		}
	}
}