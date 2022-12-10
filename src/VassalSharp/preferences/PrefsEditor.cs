/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
//using MigLayout = net.miginfocom.swing.MigLayout;
using VassalSharp.configure;
using Resources = VassalSharp.i18n.Resources;
//using ArchiveWriter = VassalSharp.tools.ArchiveWriter;
using SplashScreen = VassalSharp.tools.SplashScreen;
using WriteErrorDialog = VassalSharp.tools.WriteErrorDialog;
//using ZipArchive = VassalSharp.tools.io.ZipArchive;
using System.Collections.Generic;

namespace VassalSharp.preferences
{
	
	public class PrefsEditor
	{
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(PrefsEditor prefsEditor)
			{
				this.PrefsEditor = prefsEditor;
			}

            public PrefsEditor PrefsEditor { get; }

            public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs we)
			{
				we.Cancel = true;
				PrefsEditor.cancel();
			}
		}

		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(PrefsEditor enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PrefsEditor enclosingInstance;
			public PrefsEditor Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.save();
			}
		}

		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(PrefsEditor enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PrefsEditor enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PrefsEditor enclosingInstance;
			public PrefsEditor Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.cancel();
			}
		}

		private class AnonymousClassComponentAdapter
		{
			public AnonymousClassComponentAdapter(PrefsEditor enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PrefsEditor enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PrefsEditor enclosingInstance;
			public PrefsEditor Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  componentShown(System.Object event_sender, System.EventArgs e)
			{
				SplashScreen.sendAllToBack();
			}
		}

		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(PrefsEditor enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PrefsEditor enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PrefsEditor enclosingInstance;
			public PrefsEditor Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(Enclosing_Instance.setupDialog, "Visible", false);
			}
		}

		[Serializable]
		private class AnonymousClassAbstractAction:SupportClass.ActionSupport
		{
			private void  InitBlock(PrefsEditor enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PrefsEditor enclosingInstance;
			public PrefsEditor Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction(PrefsEditor enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			} //$NON-NLS-1$
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.storeValues();
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				Enclosing_Instance.dialog.PerformLayout();

				System.Drawing.Size d = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
				//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_int_int'"
				Enclosing_Instance.dialog.Location = new System.Drawing.Point(d.Width / 2 - Enclosing_Instance.dialog.Width / 2, 0);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(Enclosing_Instance.dialog, "Visible", true);
			}
		}
		virtual public System.Windows.Forms.Form Dialog
		{
			get
			{
				return dialog;
			}
			
		}

		//virtual public SupportClass.ActionSupport EditAction
		//{
		//	get
		//	{
		//		if (editAction == null)
		//		{
		//			editAction = new AnonymousClassAbstractAction(this, Resources.getString("Prefs.edit_preferences"));
		//			// FIMXE: setting nmemonic from first letter could cause collisions in
		//			// some languages
		//			//UPGRADE_ISSUE: Field 'javax.swing.Action.MNEMONIC_KEY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionMNEMONIC_KEY_f'"
		//			editAction.putValue(Action.MNEMONIC_KEY, (int) Resources.getString("Prefs.edit_preferences")[0]);
		//		}
		//		return editAction;
		//	}
			
		//}

		private System.Windows.Forms.Form dialog;
		private List < Configurer > options = new List < Configurer >();
		private List < Configurer > extras = new List < Configurer >();
		private bool iterating = false;
		private Dictionary< Configurer, Object > savedValues = new Dictionary < Configurer, Object >();
		private List < Prefs > prefs = new List < Prefs >();
		private System.Windows.Forms.TabControl optionsTab = new System.Windows.Forms.TabControl();
		private System.Windows.Forms.Form setupDialog;
		private System.IO.FileInfo pfile;
		private SupportClass.ActionSupport editAction;
		
		public PrefsEditor()
		{
		}

#if NEVER_DEFINED
		
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public virtual void  initDialog(System.Windows.Forms.Form parent)
		{
			if (dialog == null)
			{
				//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.CreateDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_boolean'"
				dialog = SupportClass.DialogSupport.CreateDialog(parent);
				dialog.Text = Resources.getString("Prefs.preferences"); //$NON-NLS-1$
				dialog.Closing += new System.ComponentModel.CancelEventHandler(this.PrefsEditor_Closing_DO_NOTHING_ON_CLOSE);
				
				// Handle window closing correctly.
				//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
				dialog.Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'ok '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button ok = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.OK));
				ok.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(ok);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'cancel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button cancel = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.CANCEL));
				cancel.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(cancel);
				
				dialog.setLayout(new MigLayout("insets dialog"));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				dialog.Controls.Add(optionsTab);
				optionsTab.Dock = new System.Windows.Forms.DockStyle();
				optionsTab.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				dialog.Controls.Add(ok);
				ok.Dock = new System.Windows.Forms.DockStyle();
				ok.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				dialog.Controls.Add(cancel);
				cancel.Dock = new System.Windows.Forms.DockStyle();
				cancel.BringToFront();
			}
		}
		
		public virtual void  addPrefs(Prefs p)
		{
			prefs.add(p);
		}
		
		public virtual void  addOption(System.String category, Configurer c, System.String prompt)
		{
			if (prompt != null)
			{
				if (setupDialog == null)
				{
					//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.CreateDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_boolean'"
					//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
					setupDialog = SupportClass.DialogSupport.CreateDialog((System.Windows.Forms.Form) null);
					setupDialog.Text = Resources.getString("Prefs.initial_setup"); //$NON-NLS-1$
					//UPGRADE_ISSUE: Method 'javax.swing.JDialog.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJDialogsetLayout_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					setupDialog.setLayout(new BoxLayout(((System.Windows.Forms.ContainerControl) setupDialog), BoxLayout.Y_AXIS));
					setupDialog.Closing += new System.ComponentModel.CancelEventHandler(this.PrefsEditor_Closing_DO_NOTHING_ON_CLOSE);
					//UPGRADE_WARNING: Extra logic should be included into componentShown to know if the Component is visible. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1145'"
					setupDialog.VisibleChanged += new System.EventHandler(new AnonymousClassComponentAdapter(this).componentShown);
				}
				System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = prompt;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				p.Controls.Add(temp_Control);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				setupDialog.Controls.Add(p);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				setupDialog.Controls.Add(c.Controls);
				System.Windows.Forms.Button b = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.OK));
				b.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(b);
				p = new System.Windows.Forms.Panel();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(b);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				setupDialog.Controls.Add(p);
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				setupDialog.pack();
				//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
				Toolkit.getDefaultToolkit();
				System.Drawing.Size d = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
				//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_int_int'"
				setupDialog.Location = new System.Drawing.Point(d.Width / 2 - setupDialog.Size.Width / 2, d.Height / 2 - setupDialog.Size.Height / 2);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(setupDialog, "Visible", true);
				setupDialog.Controls.Clear();
			}
			addOption(category, c);
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'addOption'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  addOption(System.String category, Configurer c)
		{
			lock (this)
			{
				if (category == null)
				{
					category = Resources.getString("Prefs.general_tab"); //$NON-NLS-1$
				}
				
				System.Windows.Forms.Panel pan = null;
				
				//UPGRADE_ISSUE: Method 'javax.swing.JTabbedPane.indexOfTab' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTabbedPaneindexOfTab_javalangString'"
				int i = optionsTab.indexOfTab(category);
				if (i == - 1)
				{
					// No match
					pan = new System.Windows.Forms.Panel();
					//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					pan.setLayout(new BoxLayout(pan, BoxLayout.Y_AXIS));
					//UPGRADE_TODO: Method 'javax.swing.JTabbedPane.addTab' was converted to 'SupportClass.TabControlSupport.AddTab' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTabbedPaneaddTab_javalangString_javaawtComponent'"
					SupportClass.TabControlSupport.AddTab(optionsTab, category, pan);
				}
				else
				{
					pan = (System.Windows.Forms.Panel) optionsTab.TabPages[i];
				}
				
				if (iterating)
				{
					extras.add(c);
				}
				else
				{
					options.add(c);
				}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box b = Box.createHorizontalBox();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(c.Controls);
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalGlue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = Box.createHorizontalGlue();
				b.Controls.Add(temp_Control);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				pan.Controls.Add(b);
			}
		}
#endif	

		private void  storeValues()
		{
			lock (this)
			{
				savedValues.Clear();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				foreach (Configurer c in options)
				{
					c.Frozen = true;
					if (c.getValue() != null)
					{
						savedValues.Add(c, c.getValue());
					}
				}
			}
		}
		
		protected internal virtual void  cancel()
		{
			lock (this)
			{
				foreach (Configurer c in options)
				{
					if (savedValues.TryGetValue(c, out object o))
						c.setValue(o);
					c.Frozen = false;
				}
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(dialog, "Visible", false);
			}
		}
		
		protected internal virtual void  save()
		{
			lock (this)
			{
				iterating = true;
				foreach (Configurer c in options)
				{
					bool hasSavedValue = savedValues.TryGetValue(c, out object theValue);
					if ((!hasSavedValue && c.getValue() != null) || (hasSavedValue && !theValue.Equals(c.getValue())))
					{
						c.fireUpdate();
					}
					c.Frozen = false;
				}

				iterating = false;
				options.AddRange(extras);
				extras.Clear();
				
				write();
				
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(dialog, "Visible", false);
			}
		}
		
		public virtual void  write()
		{
			foreach (Prefs p in prefs)
			{
				try
				{
					p.save();
				}
				catch (System.IO.IOException e)
				{
					WriteErrorDialog.error(e, p.File);
				}
			}
		}
		
		public virtual void  close()
		{
			write();
		}

		private void  PrefsEditor_Closing_DO_NOTHING_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 0);
		}
	}
}