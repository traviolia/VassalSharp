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
//UPGRADE_TODO: The type 'javax.swing.text.DocumentFilter' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DocumentFilter = javax.swing.text.DocumentFilter;
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.util.StringUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using StringUtils = org.jivesoftware.smack.util.StringUtils;
using JabberClientFactory = VassalSharp.chat.jabber.JabberClientFactory;
using NodeClientFactory = VassalSharp.chat.node.NodeClientFactory;
using P2PClientFactory = VassalSharp.chat.peer2peer.P2PClientFactory;
using Configurer = VassalSharp.configure.Configurer;
using Resources = VassalSharp.i18n.Resources;
using MacOSXMenuManager = VassalSharp.tools.menu.MacOSXMenuManager;
namespace VassalSharp.chat
{
	
	/// <summary> Specifies the server implementation in the Preferences
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class ServerConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(ServerConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerConfigurer enclosingInstance;
			public ServerConfigurer Enclosing_Instance
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
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassItemListener
		{
			public AnonymousClassItemListener(ServerConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerConfigurer enclosingInstance;
			public ServerConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs e)
			{
				if (event_sender is System.Windows.Forms.MenuItem)
					((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
				//UPGRADE_ISSUE: Method 'java.awt.event.ItemEvent.getStateChange' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventItemEventgetStateChange'"
				//UPGRADE_ISSUE: Field 'java.awt.event.ItemEvent.SELECTED' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventItemEventSELECTED_f'"
				if (e.getStateChange() == ItemEvent.SELECTED)
				{
					Enclosing_Instance.noUpdate = true;
					Enclosing_Instance.setValue(Enclosing_Instance.buildJabberProperties());
					Enclosing_Instance.noUpdate = false;
				}
				Enclosing_Instance.jabberHostPrompt.Enabled = Enclosing_Instance.jabberButton.Checked;
				Enclosing_Instance.jabberHost.Enabled = Enclosing_Instance.jabberButton.Checked && Enclosing_Instance.jabberHostPrompt.Checked;
				Enclosing_Instance.jabberAccountName.Enabled = Enclosing_Instance.jabberButton.Checked;
				Enclosing_Instance.jabberPassword.Enabled = Enclosing_Instance.jabberButton.Checked;
				Enclosing_Instance.jabberAccountPrompt.Enabled = Enclosing_Instance.jabberButton.Checked;
				Enclosing_Instance.jabberPasswordPrompt.Enabled = Enclosing_Instance.jabberButton.Checked;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassItemListener1
		{
			public AnonymousClassItemListener1(ServerConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerConfigurer enclosingInstance;
			public ServerConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs e)
			{
				if (event_sender is System.Windows.Forms.MenuItem)
					((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
				Enclosing_Instance.jabberHost.Enabled = Enclosing_Instance.jabberHostPrompt.Checked && Enclosing_Instance.jabberButton.Checked;
				//UPGRADE_TODO: Method 'javax.swing.event.DocumentListener.changedUpdate' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				Enclosing_Instance.docListener.changedUpdate(null);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDocumentListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_TODO: Interface 'javax.swing.event.DocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		private class AnonymousClassDocumentListener : DocumentListener
		{
			public AnonymousClassDocumentListener(ServerConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerConfigurer enclosingInstance;
			public ServerConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  changedUpdate(DocumentEvent e)
			{
				updateValue();
			}
			
			private void  updateValue()
			{
				Enclosing_Instance.noUpdate = true;
				Enclosing_Instance.setValue(Enclosing_Instance.buildJabberProperties());
				Enclosing_Instance.noUpdate = false;
			}
			
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  insertUpdate(DocumentEvent e)
			{
				updateValue();
			}
			
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  removeUpdate(DocumentEvent e)
			{
				updateValue();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDocumentFilter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDocumentFilter : DocumentFilter
		{
			public AnonymousClassDocumentFilter(ServerConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerConfigurer enclosingInstance;
			public ServerConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public virtual void  replace(FilterBypass fb, int offset, int length, System.String text, System.Collections.IDictionary attrs)
			{
				if (text != null)
				{
					base.replace(fb, offset, length, StringUtils.escapeNode(text).toLowerCase(), attrs);
				}
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public virtual void  insertString(FilterBypass fb, int offset, System.String string_Renamed, System.Collections.IDictionary attr)
			{
				if (string_Renamed != null)
				{
					base.insertString(fb, offset, StringUtils.escapeNode(string_Renamed).toLowerCase(), attr);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassItemListener2
		{
			public AnonymousClassItemListener2(ServerConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerConfigurer enclosingInstance;
			public ServerConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs e)
			{
				if (event_sender is System.Windows.Forms.MenuItem)
					((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
				//UPGRADE_ISSUE: Method 'java.awt.event.ItemEvent.getStateChange' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventItemEventgetStateChange'"
				//UPGRADE_ISSUE: Field 'java.awt.event.ItemEvent.SELECTED' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventItemEventSELECTED_f'"
				if (e.getStateChange() == ItemEvent.SELECTED)
				{
					Enclosing_Instance.noUpdate = true;
					Enclosing_Instance.setValue(Enclosing_Instance.buildPeerProperties());
					Enclosing_Instance.noUpdate = false;
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassItemListener3
		{
			public AnonymousClassItemListener3(ServerConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerConfigurer enclosingInstance;
			public ServerConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs e)
			{
				if (event_sender is System.Windows.Forms.MenuItem)
					((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
				//UPGRADE_ISSUE: Method 'java.awt.event.ItemEvent.getStateChange' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventItemEventgetStateChange'"
				//UPGRADE_ISSUE: Field 'java.awt.event.ItemEvent.SELECTED' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventItemEventSELECTED_f'"
				if (e.getStateChange() == ItemEvent.SELECTED)
				{
					Enclosing_Instance.noUpdate = true;
					Enclosing_Instance.setValue(Enclosing_Instance.buildLegacyProperties());
					Enclosing_Instance.noUpdate = false;
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
					//UPGRADE_TODO: Class 'javax.swing.ButtonGroup' was converted to 'System.Collections.ArrayList' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					System.Collections.ArrayList group = new System.Collections.ArrayList();
					System.Windows.Forms.RadioButton temp_radiobutton;
					temp_radiobutton = new System.Windows.Forms.RadioButton();
					temp_radiobutton.Text = JABBER_BUTTON;
					jabberButton = temp_radiobutton;
					jabberButton.CheckedChanged += new System.EventHandler(new AnonymousClassItemListener(this).itemStateChanged);
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = Resources.getString("Server.account_name");
					jabberAccountPrompt = temp_label2; //$NON-NLS-1$
					jabberAccountPrompt.Enabled = false;
					jabberAccountName = new System.Windows.Forms.TextBox();
					jabberAccountName.Enabled = false;
					System.Windows.Forms.Label temp_label3;
					temp_label3 = new System.Windows.Forms.Label();
					temp_label3.Text = Resources.getString("Server.password");
					jabberPasswordPrompt = temp_label3; //$NON-NLS-1$
					jabberPasswordPrompt.Enabled = false;
					System.Windows.Forms.TextBox temp_TextBox;
					temp_TextBox = new System.Windows.Forms.TextBox();
					temp_TextBox.PasswordChar = '*';
					jabberPassword = temp_TextBox;
					jabberPassword.Enabled = false;
					jabberHostPrompt = SupportClass.CheckBoxSupport.CreateCheckBox(Resources.getString("Server.host")); //$NON-NLS-1$
					jabberHostPrompt.Enabled = false;
					//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
					jabberHost = new System.Windows.Forms.TextBox();
					jabberHost.Enabled = false;
					jabberHostPrompt.CheckedChanged += new System.EventHandler(new AnonymousClassItemListener1(this).itemStateChanged);
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					jabberHost.Text = JabberClientFactory.DEFAULT_JABBER_HOST + ":" + JabberClientFactory.DEFAULT_JABBER_PORT; //$NON-NLS-1$
					docListener = new AnonymousClassDocumentListener(this);
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
					//UPGRADE_ISSUE: Class 'javax.swing.text.AbstractDocument' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextAbstractDocument'"
					((AbstractDocument) jabberAccountName.Text).setDocumentFilter(new AnonymousClassDocumentFilter(this));
					//UPGRADE_ISSUE: Method 'javax.swing.text.Document.addDocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
					((System.String) jabberHost.Text).addDocumentListener(docListener);
					//UPGRADE_ISSUE: Method 'javax.swing.text.Document.addDocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
					((System.String) jabberAccountName.Text).addDocumentListener(docListener);
					//UPGRADE_ISSUE: Method 'javax.swing.text.Document.addDocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
					((System.String) jabberPassword.Text).addDocumentListener(docListener);
					// Disable Jabber server until next release
					//UPGRADE_ISSUE: Method 'java.lang.System.getProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
					if ("true".Equals(System_Renamed.getProperty("enableJabber")))
					{
						//$NON-NLS-1$ //$NON-NLS-2$
						group.Add((System.Object) jabberButton);
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
						controls.Controls.Add(jabberButton);
						jabberButton.Dock = new System.Windows.Forms.DockStyle();
						jabberButton.BringToFront(); //$NON-NLS-1$
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
						controls.Controls.Add(jabberAccountPrompt);
						jabberAccountPrompt.Dock = new System.Windows.Forms.DockStyle();
						jabberAccountPrompt.BringToFront(); //$NON-NLS-1$
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
						controls.Controls.Add(jabberAccountName);
						jabberAccountName.Dock = new System.Windows.Forms.DockStyle();
						jabberAccountName.BringToFront(); //$NON-NLS-1$
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
						controls.Controls.Add(jabberPasswordPrompt);
						jabberPasswordPrompt.Dock = new System.Windows.Forms.DockStyle();
						jabberPasswordPrompt.BringToFront(); //$NON-NLS-1$
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
						controls.Controls.Add(jabberPassword);
						jabberPassword.Dock = new System.Windows.Forms.DockStyle();
						jabberPassword.BringToFront(); //$NON-NLS-1$
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
						controls.Controls.Add(jabberHostPrompt);
						jabberHostPrompt.Dock = new System.Windows.Forms.DockStyle();
						jabberHostPrompt.BringToFront(); //$NON-NLS-1$
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
						controls.Controls.Add(jabberHost);
						jabberHost.Dock = new System.Windows.Forms.DockStyle();
						jabberHost.BringToFront(); //$NON-NLS-1$
					}
					System.Windows.Forms.RadioButton temp_radiobutton2;
					temp_radiobutton2 = new System.Windows.Forms.RadioButton();
					temp_radiobutton2.Text = P2P_BUTTON;
					p2pButton = temp_radiobutton2;
					p2pButton.CheckedChanged += new System.EventHandler(new AnonymousClassItemListener2(this).itemStateChanged);
					group.Add((System.Object) p2pButton);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					controls.Controls.Add(p2pButton);
					p2pButton.Dock = new System.Windows.Forms.DockStyle();
					p2pButton.BringToFront(); //$NON-NLS-1$
					System.Windows.Forms.RadioButton temp_radiobutton3;
					temp_radiobutton3 = new System.Windows.Forms.RadioButton();
					temp_radiobutton3.Text = LEGACY_BUTTON;
					legacyButton = temp_radiobutton3;
					legacyButton.CheckedChanged += new System.EventHandler(new AnonymousClassItemListener3(this).itemStateChanged);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(legacyButton);
					group.Add((System.Object) legacyButton);
				}
				return controls;
			}
			
		}
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		virtual protected internal System.Collections.Specialized.NameValueCollection JabberConfigProperties
		{
			get
			{
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
				System.Collections.Specialized.NameValueCollection p = new System.Collections.Specialized.NameValueCollection();
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				p[JabberClientFactory.JABBER_LOGIN] = jabberAccountName.Text;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				p[JabberClientFactory.JABBER_PWD] = new System.String(jabberPassword.Text.ToCharArray());
				System.String host = jabberHost.Text;
				System.String port = "5222"; //$NON-NLS-1$
				int idx = host.IndexOf(':'); //$NON-NLS-1$
				if (idx > 0)
				{
					port = host.Substring(idx + 1);
					host = host.Substring(0, (idx) - (0));
				}
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				p[JabberClientFactory.JABBER_HOST] = host;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				p[JabberClientFactory.JABBER_PORT] = port;
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
				return s;
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
				else
				{
					//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
					//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
					p = new System.Collections.Specialized.NameValueCollection(p);
				}
				if (DynamicClientFactory.DYNAMIC_TYPE.Equals(p.Get(ChatServerFactory.TYPE_KEY)))
				{
					p.Remove(JabberClientFactory.JABBER_HOST);
					p.Remove(JabberClientFactory.JABBER_PORT);
				}
				return p;
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'CONNECTED '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'CONNECTED' was moved to static method 'VassalSharp.chat.ServerConfigurer'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly System.String CONNECTED; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'DISCONNECTED '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'DISCONNECTED' was moved to static method 'VassalSharp.chat.ServerConfigurer'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly System.String DISCONNECTED; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'JABBER_BUTTON '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'JABBER_BUTTON' was moved to static method 'VassalSharp.chat.ServerConfigurer'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly System.String JABBER_BUTTON; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'P2P_BUTTON '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'P2P_BUTTON' was moved to static method 'VassalSharp.chat.ServerConfigurer'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly System.String P2P_BUTTON; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'LEGACY_BUTTON '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'LEGACY_BUTTON' was moved to static method 'VassalSharp.chat.ServerConfigurer'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly System.String LEGACY_BUTTON; //$NON-NLS-1$
		private const System.String ENCODING = "UTF-8"; //$NON-NLS-1$
		protected internal System.Windows.Forms.Control controls;
		private System.Windows.Forms.TextBox jabberHost;
		private HybridClient client;
		private System.Windows.Forms.RadioButton legacyButton;
		private System.Windows.Forms.RadioButton jabberButton;
		private System.Windows.Forms.TextBox jabberAccountName;
		private System.Windows.Forms.TextBox jabberPassword;
		private System.Windows.Forms.RadioButton p2pButton;
		private System.Windows.Forms.Label header;
		private System.Windows.Forms.CheckBox jabberHostPrompt;
		private System.Windows.Forms.Label jabberAccountPrompt;
		private System.Windows.Forms.Label jabberPasswordPrompt;
		//UPGRADE_TODO: Interface 'javax.swing.event.DocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		private DocumentListener docListener;
		
		//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public ServerConfigurer(System.String key, System.String name, HybridClient client):base(key, name, new System.Collections.Specialized.NameValueCollection())
		{
			this.client = client;
			client.addPropertyChangeListener(VassalSharp.build.module.ServerConnection_Fields.CONNECTED, new AnonymousClassPropertyChangeListener(this));
			System.Windows.Forms.Control generatedAux2 = Controls;
			setValue(buildLegacyProperties());
		}
		
		private void  enableControls(bool connected)
		{
			p2pButton.Enabled = !connected;
			legacyButton.Enabled = !connected;
			jabberButton.Enabled = !connected;
			jabberHostPrompt.Enabled = !connected;
			jabberHost.Enabled = !connected && jabberHostPrompt.Checked && jabberButton.Checked;
			jabberAccountName.Enabled = !connected && jabberButton.Checked;
			jabberPassword.Enabled = !connected && jabberButton.Checked;
			header.Text = connected?CONNECTED:DISCONNECTED;
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		protected internal virtual System.Collections.Specialized.NameValueCollection buildJabberProperties()
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			System.Collections.Specialized.NameValueCollection p = new System.Collections.Specialized.NameValueCollection();
			if (jabberHostPrompt.Checked)
			{
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				p[ChatServerFactory.TYPE_KEY] = JabberClientFactory.JABBER_SERVER_TYPE;
			}
			else
			{
				// Build a Jabber server with dynamically-determined host and server
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				p[ChatServerFactory.TYPE_KEY] = DynamicClientFactory.DYNAMIC_TYPE;
				//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
				p[DynamicClientFactory.DYNAMIC_TYPE] = JabberClientFactory.JABBER_SERVER_TYPE;
			}
			SupportClass.MapSupport.PutAll(p, JabberConfigProperties);
			return p;
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		protected internal virtual System.Collections.Specialized.NameValueCollection buildPeerProperties()
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			System.Collections.Specialized.NameValueCollection p = new System.Collections.Specialized.NameValueCollection();
			//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
			p[ChatServerFactory.TYPE_KEY] = P2PClientFactory.P2P_TYPE;
			SupportClass.MapSupport.PutAll(p, JabberConfigProperties);
			return p;
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		protected internal virtual System.Collections.Specialized.NameValueCollection buildLegacyProperties()
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			System.Collections.Specialized.NameValueCollection p = new System.Collections.Specialized.NameValueCollection();
			// Build a legacy server with dynamically-determined host and server
			//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
			p[ChatServerFactory.TYPE_KEY] = DynamicClientFactory.DYNAMIC_TYPE;
			//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
			p[DynamicClientFactory.DYNAMIC_TYPE] = NodeClientFactory.NODE_TYPE;
			SupportClass.MapSupport.PutAll(p, JabberConfigProperties);
			return p;
		}
		
		public override void  setValue(System.Object o)
		{
			base.setValue(o);
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			if (!noUpdate && o is System.Collections.Specialized.NameValueCollection && controls != null)
			{
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				System.Collections.Specialized.NameValueCollection p = (System.Collections.Specialized.NameValueCollection) o;
				System.String type = p[ChatServerFactory.TYPE_KEY] == null?JabberClientFactory.JABBER_SERVER_TYPE:p[ChatServerFactory.TYPE_KEY];
				System.String finalType = type;
				if (DynamicClientFactory.DYNAMIC_TYPE.Equals(type))
				{
					finalType = p.Get(DynamicClientFactory.DYNAMIC_TYPE);
				}
				if (NodeClientFactory.NODE_TYPE.Equals(finalType))
				{
					legacyButton.Checked = true;
				}
				else if (JabberClientFactory.JABBER_SERVER_TYPE.Equals(finalType))
				{
					jabberButton.Checked = true;
					jabberHostPrompt.Checked = type.Equals(finalType);
				}
				else if (P2PClientFactory.P2P_TYPE.Equals(finalType))
				{
					p2pButton.Checked = true;
				}
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				jabberAccountName.Text = p.Get(JabberClientFactory.JABBER_LOGIN);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				jabberPassword.Text = p.Get(JabberClientFactory.JABBER_PWD);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				jabberHost.Text = (p[JabberClientFactory.JABBER_HOST] == null?JabberClientFactory.DEFAULT_JABBER_HOST:p[JabberClientFactory.JABBER_HOST]) + ":" + (p[JabberClientFactory.JABBER_PORT] == null?JabberClientFactory.DEFAULT_JABBER_PORT:p[JabberClientFactory.JABBER_PORT]);
			}
			if (client != null && !CONNECTED.Equals(header.Text))
			{
				client.Delegate = ChatServerFactory.build(ServerInfo);
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
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			ChatServerFactory.register(NodeClientFactory.NODE_TYPE, new NodeClientFactory());
			ChatServerFactory.register(DynamicClientFactory.DYNAMIC_TYPE, new DynamicClientFactory());
			ChatServerFactory.register(P2PClientFactory.P2P_TYPE, new P2PClientFactory());
			ChatServerFactory.register(JabberClientFactory.JABBER_SERVER_TYPE, new JabberClientFactory());
			new MacOSXMenuManager();
			HybridClient c = new HybridClient();
			ServerConfigurer config = new ServerConfigurer("server", "server", c); //$NON-NLS-1$ //$NON-NLS-2$
			System.Windows.Forms.Form f = new System.Windows.Forms.Form();
			//UPGRADE_TODO: Method 'javax.swing.JFrame.getContentPane' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJFramegetContentPane'"
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			((System.Windows.Forms.ContainerControl) f).Controls.Add(config.Controls);
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			f.pack();
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			f.Visible = true;
			f.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE);
		}
		static ServerConfigurer()
		{
			CONNECTED = Resources.getString("Server.please_disconnect");
			DISCONNECTED = Resources.getString("Server.select_server_type");
			JABBER_BUTTON = Resources.getString("Server.jabber");
			P2P_BUTTON = Resources.getString("Server.direct");
			LEGACY_BUTTON = Resources.getString("Server.legacy");
		}
	}
}