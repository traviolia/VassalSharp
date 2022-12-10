/*
* $Id$
*
* Copyright (c) 2000-2009 by Rodney Kinney, Joel Uckelman, Brent Easton
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
using JabberPlayer = VassalSharp.chat.jabber.JabberPlayer;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat
{
	
	/// <summary> A window that displays information on a {@link VassalSharp.chat.SimplePlayer}</summary>
	[Serializable]
	public class PlayerInfoWindow:System.Windows.Forms.Form
	{
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public PlayerInfoWindow(System.Windows.Forms.Form f, SimplePlayer p):base()
		{
			SupportClass.DialogSupport.SetDialog(this, f, p.getName());
			setLayout(new MigLayout("insets dialog", "[align right][fill,grow]", "")); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
			
			// player name
			//UPGRADE_NOTE: Final was removed from the declaration of 'name_f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int generatedAux = p.getName().Length;
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			System.Windows.Forms.TextBox name_f = new System.Windows.Forms.TextBox();
			//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
			name_f.Text = p.getName();
			name_f.ReadOnly = !false;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'name_l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label;
			temp_label = new System.Windows.Forms.Label();
			temp_label.Text = Resources.getString("Chat.real_name");
			System.Windows.Forms.Label name_l = temp_label; //$NON-NLS-1$
			//UPGRADE_ISSUE: Method 'javax.swing.JLabel.setLabelFor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJLabelsetLabelFor_javaawtComponent'"
			name_l.setLabelFor(name_f);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(name_l);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(name_f);
			name_f.Dock = new System.Windows.Forms.DockStyle();
			name_f.BringToFront(); //$NON-NLS-1$
			
			// Server login
			if (p is JabberPlayer)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'login '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String login = ((JabberPlayer) p).RawJid;
				//UPGRADE_NOTE: Final was removed from the declaration of 'login_f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
				System.Windows.Forms.TextBox login_f = new System.Windows.Forms.TextBox();
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				login_f.Text = login;
				login_f.ReadOnly = !false;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'login_l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = Resources.getString("Chat.server_login");
				System.Windows.Forms.Label login_l = temp_label2; //$NON-NLS-1$
				//UPGRADE_ISSUE: Method 'javax.swing.JLabel.setLabelFor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJLabelsetLabelFor_javaawtComponent'"
				login_l.setLabelFor(login_f);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(login_l);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(login_f);
				login_f.Dock = new System.Windows.Forms.DockStyle();
				login_f.BringToFront(); //$NON-NLS-1$
			}
			
			// IP address
			//UPGRADE_NOTE: Final was removed from the declaration of 'ip '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String ip = ((SimpleStatus) p.getStatus()).Ip;
			if (ip != null && ip.Length > 0)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'ip_f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.TextBox ip_f = new System.Windows.Forms.TextBox();
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				ip_f.Text = ip;
				ip_f.ReadOnly = !false;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'ip_l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Label temp_label3;
				temp_label3 = new System.Windows.Forms.Label();
				temp_label3.Text = Resources.getString("Chat.ip_address");
				System.Windows.Forms.Label ip_l = temp_label3; //$NON-NLS-1$
				//UPGRADE_ISSUE: Method 'javax.swing.JLabel.setLabelFor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJLabelsetLabelFor_javaawtComponent'"
				ip_l.setLabelFor(ip_f);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(ip_l);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(ip_f);
				ip_f.Dock = new System.Windows.Forms.DockStyle();
				ip_f.BringToFront(); //$NON-NLS-1$
			}
			
			// client version
			System.String client = ((SimpleStatus) p.getStatus()).Client;
			if (client == null || client.Length == 0)
				client = "< 3.1"; //$NON-NLS-1$
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'client_f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.TextBox client_f = new System.Windows.Forms.TextBox();
			//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
			client_f.Text = client;
			client_f.ReadOnly = !false;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'client_l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label4;
			temp_label4 = new System.Windows.Forms.Label();
			temp_label4.Text = Resources.getString("Chat.client_version");
			System.Windows.Forms.Label client_l = temp_label4; //$NON-NLS-1$
			//UPGRADE_ISSUE: Method 'javax.swing.JLabel.setLabelFor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJLabelsetLabelFor_javaawtComponent'"
			client_l.setLabelFor(client_f);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(client_l);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(client_f);
			client_f.Dock = new System.Windows.Forms.DockStyle();
			client_f.BringToFront(); //$NON-NLS-1$
			
			// module version
			//UPGRADE_NOTE: Final was removed from the declaration of 'moduleVersion '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String moduleVersion = ((SimpleStatus) p.getStatus()).ModuleVersion;
			if (moduleVersion != null && moduleVersion.Length > 0)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'mver_f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.TextBox mver_f = new System.Windows.Forms.TextBox();
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				mver_f.Text = moduleVersion;
				mver_f.ReadOnly = !false;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'mver_l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Label temp_label5;
				temp_label5 = new System.Windows.Forms.Label();
				temp_label5.Text = Resources.getString("Chat.module_version");
				System.Windows.Forms.Label mver_l = temp_label5; //$NON-NLS-1$
				//UPGRADE_ISSUE: Method 'javax.swing.JLabel.setLabelFor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJLabelsetLabelFor_javaawtComponent'"
				mver_l.setLabelFor(mver_f);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(mver_l);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(mver_f);
				mver_f.Dock = new System.Windows.Forms.DockStyle();
				mver_f.BringToFront(); //$NON-NLS-1$
			}
			
			// module checksum
			//UPGRADE_NOTE: Final was removed from the declaration of 'csum '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String csum = ((SimpleStatus) p.getStatus()).Crc;
			if (csum != null && csum.Length > 0)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'csum_f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.TextBox csum_f = new System.Windows.Forms.TextBox();
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				csum_f.Text = csum;
				csum_f.ReadOnly = !false;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'csum_l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Label temp_label6;
				temp_label6 = new System.Windows.Forms.Label();
				temp_label6.Text = Resources.getString("Chat.module_checksum");
				System.Windows.Forms.Label csum_l = temp_label6; //$NON-NLS-1$
				//UPGRADE_ISSUE: Method 'javax.swing.JLabel.setLabelFor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJLabelsetLabelFor_javaawtComponent'"
				csum_l.setLabelFor(csum_f);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(csum_l);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(csum_f);
				csum_f.Dock = new System.Windows.Forms.DockStyle();
				csum_f.BringToFront(); //$NON-NLS-1$
			}
			
			// looking for a game?
			//UPGRADE_NOTE: Final was removed from the declaration of 'looking_b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.CheckBox looking_b = SupportClass.CheckBoxSupport.CreateCheckBox(Resources.getString("Chat.looking_for_a_game")); //$NON-NLS-1$
			looking_b.Checked = ((SimpleStatus) p.getStatus()).Looking;
			looking_b.Enabled = false;
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(looking_b);
			looking_b.Dock = new System.Windows.Forms.DockStyle();
			looking_b.BringToFront(); //$NON-NLS-1$
			
			// away from keyboard?
			//UPGRADE_NOTE: Final was removed from the declaration of 'away_b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.CheckBox away_b = SupportClass.CheckBoxSupport.CreateCheckBox(Resources.getString("Chat.away_from_keyboard")); //$NON-NLS-1$
			away_b.Checked = ((SimpleStatus) p.getStatus()).Away;
			away_b.Enabled = false;
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(away_b);
			away_b.Dock = new System.Windows.Forms.DockStyle();
			away_b.BringToFront(); //$NON-NLS-1$
			
			// personal info
			//UPGRADE_NOTE: Final was removed from the declaration of 'pinfo_a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.TextBox temp_TextBox;
			temp_TextBox = new System.Windows.Forms.TextBox();
			temp_TextBox.Multiline = true;
			temp_TextBox.WordWrap = false;
			temp_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			System.Windows.Forms.TextBox pinfo_a = temp_TextBox;
			//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
			pinfo_a.Text = ((SimpleStatus) p.getStatus()).Profile;
			pinfo_a.ReadOnly = !false;
			pinfo_a.WordWrap = true;
			pinfo_a.WordWrap = true;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'pinfo_s '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
			temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol.AutoScroll = true;
			temp_scrollablecontrol.Controls.Add(pinfo_a);
			System.Windows.Forms.ScrollableControl pinfo_s = temp_scrollablecontrol;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'pinfo_l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label7;
			temp_label7 = new System.Windows.Forms.Label();
			temp_label7.Text = Resources.getString("Chat.personal_info");
			System.Windows.Forms.Label pinfo_l = temp_label7; //$NON-NLS-1$
			//UPGRADE_ISSUE: Method 'javax.swing.JLabel.setLabelFor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJLabelsetLabelFor_javaawtComponent'"
			pinfo_l.setLabelFor(pinfo_s);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(pinfo_l);
			pinfo_l.Dock = new System.Windows.Forms.DockStyle();
			pinfo_l.BringToFront(); //$NON-NLS-1$
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(pinfo_s);
			pinfo_s.Dock = new System.Windows.Forms.DockStyle();
			pinfo_s.BringToFront(); //$NON-NLS-1$
			
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
		}
	}
}