/*
* $Id$
*
* Copyright (c) 2008 by Joel Uckelman
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
//UPGRADE_TODO: The type 'org.jdesktop.layout.GroupLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using GroupLayout = org.jdesktop.layout.GroupLayout;
//UPGRADE_TODO: The type 'org.jdesktop.layout.LayoutStyle' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LayoutStyle = org.jdesktop.layout.LayoutStyle;
using GameModule = VassalSharp.build.GameModule;
using PasswordConfigurer = VassalSharp.configure.PasswordConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using Resources = VassalSharp.i18n.Resources;
using Prefs = VassalSharp.preferences.Prefs;
namespace VassalSharp.tools
{
	
	// FXIME: Would be better if this didn't set the username and password
	// directly, but instead had a static method for returning them.
	// FIXME: Could be made prettier if it didn't use Configurers, or if
	// we made Configurers prettier.
	
	/// <summary> A dialog for setting a username and password.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	[Serializable]
	public class UsernameAndPasswordDialog:System.Windows.Forms.Form
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(VassalSharp.configure.StringConfigurer nameConfig, VassalSharp.configure.StringConfigurer pwd, UsernameAndPasswordDialog enclosingInstance)
			{
				InitBlock(nameConfig, pwd, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.configure.StringConfigurer nameConfig, VassalSharp.configure.StringConfigurer pwd, UsernameAndPasswordDialog enclosingInstance)
			{
				this.nameConfig = nameConfig;
				this.pwd = pwd;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable nameConfig was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.StringConfigurer nameConfig;
			//UPGRADE_NOTE: Final variable pwd was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.StringConfigurer pwd;
			private UsernameAndPasswordDialog enclosingInstance;
			public UsernameAndPasswordDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Prefs p = GameModule.getGameModule().getPrefs();
				
				p.getOption(GameModule.REAL_NAME).setValue(nameConfig.ValueString);
				p.getOption(GameModule.SECRET_NAME).setValue(pwd.ValueString);
				
				try
				{
					p.write();
				}
				catch (System.IO.IOException ex)
				{
					WriteErrorDialog.error(ex, p.File);
				}
				
				Enclosing_Instance.Dispose();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(UsernameAndPasswordDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(UsernameAndPasswordDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private UsernameAndPasswordDialog enclosingInstance;
			public UsernameAndPasswordDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.Dispose();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(VassalSharp.configure.StringConfigurer nameConfig, VassalSharp.configure.StringConfigurer pwd, System.Windows.Forms.Label error, System.Windows.Forms.Button ok, VassalSharp.configure.StringConfigurer pwd2, UsernameAndPasswordDialog enclosingInstance)
			{
				InitBlock(nameConfig, pwd, error, ok, pwd2, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.configure.StringConfigurer nameConfig, VassalSharp.configure.StringConfigurer pwd, System.Windows.Forms.Label error, System.Windows.Forms.Button ok, VassalSharp.configure.StringConfigurer pwd2, UsernameAndPasswordDialog enclosingInstance)
			{
				this.nameConfig = nameConfig;
				this.pwd = pwd;
				this.error = error;
				this.ok = ok;
				this.pwd2 = pwd2;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable nameConfig was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.StringConfigurer nameConfig;
			//UPGRADE_NOTE: Final variable pwd was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.StringConfigurer pwd;
			//UPGRADE_NOTE: Final variable error was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Label error;
			//UPGRADE_NOTE: Final variable ok was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Button ok;
			//UPGRADE_NOTE: Final variable pwd2 was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.StringConfigurer pwd2;
			private UsernameAndPasswordDialog enclosingInstance;
			public UsernameAndPasswordDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				if (nameConfig.getValue() == null || "".Equals(nameConfig.getValue()))
				{
					//$NON-NLS-1$
					if (pwd.getValue() == null || "".Equals(pwd.getValue()))
					{
						error.Text = Resources.getString("WizardSupport.EnterNameAndPassword"); //$NON-NLS-1$
					}
					else
					{
						error.Text = Resources.getString("WizardSupport.EnterYourName"); //$NON-NLS-1$
					}
					error.ForeColor = System.Drawing.Color.Black;
					ok.Enabled = false;
				}
				else if (pwd.getValue() == null || "".Equals(pwd.getValue()))
				{
					//$NON-NLS-1$
					error.Text = Resources.getString("WizardSupport.EnterYourPassword"); //$NON-NLS-1$
					error.ForeColor = System.Drawing.Color.Black;
					ok.Enabled = false;
				}
				else if (pwd2.getValue() == null || "".Equals(pwd2.getValue()))
				{
					//$NON-NLS-1$
					error.Text = "Please confirm your password";
					error.ForeColor = System.Drawing.Color.Black;
					ok.Enabled = false;
				}
				else if (!pwd.getValue().Equals(pwd2.getValue()))
				{
					error.Text = Resources.getString("WizardSupport.PasswordsDontMatch"); //$NON-NLS-1$
					error.ForeColor = System.Drawing.Color.Red;
					ok.Enabled = false;
				}
				else
				{
					// everything is ok
					error.Text = ""; //$NON-NLS-1$
					error.ForeColor = System.Drawing.Color.Black;
					ok.Enabled = true;
				}
			}
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public UsernameAndPasswordDialog(System.Windows.Forms.Form parent):base()
		{
			//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_javalangString_boolean'"
			SupportClass.DialogSupport.SetDialog(this, parent, "Set Your Username and Password");
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Closing += new System.ComponentModel.CancelEventHandler(this.UsernameAndPasswordDialog_Closing_DISPOSE_ON_CLOSE);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'nameConfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringConfigurer nameConfig = new StringConfigurer(null, Resources.getString("WizardSupport.RealName")); //$NON-NLS-1$
			//UPGRADE_NOTE: Final was removed from the declaration of 'pwd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringConfigurer pwd = new PasswordConfigurer(null, Resources.getString("WizardSupport.Password")); //$NON-NLS-1$
			//UPGRADE_NOTE: Final was removed from the declaration of 'pwd2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringConfigurer pwd2 = new PasswordConfigurer(null, Resources.getString("WizardSupport.ConfirmPassword")); //$NON-NLS-1$
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'nc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Control nc = nameConfig.Controls;
			//UPGRADE_NOTE: Final was removed from the declaration of 'p1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Control p1 = pwd.Controls;
			//UPGRADE_NOTE: Final was removed from the declaration of 'p2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Control p2 = pwd2.Controls;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'note '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label;
			temp_label = new System.Windows.Forms.Label();
			temp_label.Text = Resources.getString("WizardSupport.NameAndPasswordDetails");
			System.Windows.Forms.Label note = temp_label;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'error '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label2;
			temp_label2 = new System.Windows.Forms.Label();
			temp_label2.Text = Resources.getString("WizardSupport.EnterNameAndPassword");
			System.Windows.Forms.Label error = temp_label2; //$NON-NLS-1$
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'ok '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Button ok = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.OK));
			ok.Enabled = false;
			ok.Click += new System.EventHandler(new AnonymousClassActionListener(nameConfig, pwd, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(ok);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'cancel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Button cancel = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.CANCEL));
			cancel.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(cancel);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'layout '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GroupLayout layout = new GroupLayout(panel);
			panel.setLayout(layout);
			
			/*
			// FIXME: This is the layout code for Java 1.6
			
			layout.setAutoCreateGaps(true);
			layout.setAutoCreateContainerGaps(true);
			
			layout.setHorizontalGroup(
			layout.createParallelGroup(GroupLayout.Alignment.LEADING, true)
			.addComponent(nc)
			.addComponent(p1)
			.addComponent(p2)
			.addComponent(note)
			.addGroup(layout.createSequentialGroup()
			.addGap(0, 0, Integer.MAX_VALUE)
			.addComponent(error)
			.addGap(0, 0, Integer.MAX_VALUE))
			.addGroup(layout.createSequentialGroup()
			.addGap(0, 0, Integer.MAX_VALUE)
			.addComponent(ok)
			.addComponent(cancel)));
			
			layout.setVerticalGroup(
			layout.createSequentialGroup()
			.addComponent(nc)
			.addComponent(p1)
			.addComponent(p2)
			.addComponent(note)
			.addComponent(error)
			.addPreferredGap(LayoutStyle.ComponentPlacement.UNRELATED,
			GroupLayout.DEFAULT_SIZE, Integer.MAX_VALUE)
			.addGroup(
			layout.createParallelGroup(GroupLayout.Alignment.BASELINE, false)
			.addComponent(ok)
			.addComponent(cancel)));
			
			layout.linkSize(ok, cancel);*/
			
			layout.setAutocreateGaps(true);
			layout.setAutocreateContainerGaps(true);
			
			layout.setHorizontalGroup(layout.createParallelGroup(GroupLayout.LEADING, true).add(nc).add(p1).add(p2).add(note).add(layout.createSequentialGroup().add(0, 0, System.Int32.MaxValue).add(error).add(0, 0, System.Int32.MaxValue)).add(layout.createSequentialGroup().add(0, 0, System.Int32.MaxValue).add(ok).add(cancel)));
			
			layout.setVerticalGroup(layout.createSequentialGroup().add(nc).add(p1).add(p2).add(note).add(error).addPreferredGap(LayoutStyle.UNRELATED, GroupLayout.DEFAULT_SIZE, System.Int32.MaxValue).add(layout.createParallelGroup(GroupLayout.BASELINE, false).add(ok).add(cancel)));
			
			layout.linkSize(new System.Windows.Forms.Control[]{ok, cancel});
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(panel);
			
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
			setMinimumSize(Size);
			
			// This listener handles validating the input, updating the error
			// message, and enabling the Ok button.
			//UPGRADE_NOTE: Final was removed from the declaration of 'pl '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			PropertyChangeListener pl = new AnonymousClassPropertyChangeListener(nameConfig, pwd, error, ok, pwd2, this);
			
			nameConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(pl.propertyChange);
			pwd.PropertyChange += new SupportClass.PropertyChangeEventHandler(pl.propertyChange);
			pwd2.PropertyChange += new SupportClass.PropertyChangeEventHandler(pl.propertyChange);
		}
		private void  UsernameAndPasswordDialog_Closing_DISPOSE_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 2);
		}
	}
}