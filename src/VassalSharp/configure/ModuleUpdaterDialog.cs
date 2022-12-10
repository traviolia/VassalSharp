/*
* $Id$
*
* Copyright (c) 2005 by Rodney Kinney
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
//UPGRADE_TODO: The type 'java.awt.HeadlessException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using HeadlessException = java.awt.HeadlessException;
using GameModule = VassalSharp.build.GameModule;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ZipUpdater = VassalSharp.tools.ZipUpdater;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
namespace VassalSharp.configure
{
	
	[Serializable]
	public class ModuleUpdaterDialog:System.Windows.Forms.Form
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(System.Windows.Forms.Button saveButton, VassalSharp.configure.FileConfigurer fileConfig, ModuleUpdaterDialog enclosingInstance)
			{
				InitBlock(saveButton, fileConfig, enclosingInstance);
			}
			private void  InitBlock(System.Windows.Forms.Button saveButton, VassalSharp.configure.FileConfigurer fileConfig, ModuleUpdaterDialog enclosingInstance)
			{
				this.saveButton = saveButton;
				this.fileConfig = fileConfig;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable saveButton was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Button saveButton;
			//UPGRADE_NOTE: Final variable fileConfig was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.FileConfigurer fileConfig;
			private ModuleUpdaterDialog enclosingInstance;
			public ModuleUpdaterDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				saveButton.Enabled = fileConfig.getValue() != null;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(VassalSharp.configure.FileConfigurer fileConfig, ModuleUpdaterDialog enclosingInstance)
			{
				InitBlock(fileConfig, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.configure.FileConfigurer fileConfig, ModuleUpdaterDialog enclosingInstance)
			{
				this.fileConfig = fileConfig;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable fileConfig was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.FileConfigurer fileConfig;
			private ModuleUpdaterDialog enclosingInstance;
			public ModuleUpdaterDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				FileChooser fc = GameModule.getGameModule().getFileChooser();
				if (fc.showSaveDialog(Enclosing_Instance.Owner) != FileChooser.APPROVE_OPTION)
					return ;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'output '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.IO.FileInfo output = fc.SelectedFile;
				ZipUpdater updater = null;
				try
				{
					updater = new ZipUpdater((System.IO.FileInfo) fileConfig.getValue());
					updater.createUpdater(new File(GameModule.getGameModule().getArchiveWriter().getName()), output);
				}
				// FIXME: review error message
				catch (System.IO.IOException e1)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.String msg = e1.Message;
					if (msg == null)
					{
						msg = "Unable to create updater.";
					}
					SupportClass.OptionPaneSupport.ShowMessageDialog(Enclosing_Instance, msg, "Error writing updater", (int) System.Windows.Forms.MessageBoxIcon.Error);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(ModuleUpdaterDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ModuleUpdaterDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ModuleUpdaterDialog enclosingInstance;
			public ModuleUpdaterDialog Enclosing_Instance
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
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public ModuleUpdaterDialog(System.Windows.Forms.Form owner):base()
		{
			//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_boolean'"
			SupportClass.DialogSupport.SetDialog(this, owner);
			Text = "Module Updater";
			//UPGRADE_ISSUE: Method 'javax.swing.JDialog.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJDialogsetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			setLayout(new BoxLayout(((System.Windows.Forms.ContainerControl) this), BoxLayout.Y_AXIS));
			//UPGRADE_NOTE: Final was removed from the declaration of 'fileConfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FileConfigurer fileConfig = new FileConfigurer(null, "File containing older version:  ");
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(fileConfig.Controls);
			
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box b = Box.createHorizontalBox();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'saveButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Button saveButton = SupportClass.ButtonSupport.CreateStandardButton("Create Updater");
			saveButton.Enabled = false;
			fileConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(saveButton, fileConfig, this).propertyChange);
			saveButton.Click += new System.EventHandler(new AnonymousClassActionListener(fileConfig, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(saveButton);
			
			System.Windows.Forms.Button cancelButton = SupportClass.ButtonSupport.CreateStandardButton("Close");
			cancelButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(cancelButton);
			System.Windows.Forms.Button helpButton = SupportClass.ButtonSupport.CreateStandardButton("Help");
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(b);
			HelpFile hf = null;
			try
			{
				hf = new HelpFile(null, new System.IO.FileInfo(new System.IO.FileInfo(VassalSharp.build.module.Documentation.DocumentationBaseDir.FullName + "\\" + "ReferenceManual").FullName + "\\" + "ModuleUpdater.htm"));
			}
			catch (System.UriFormatException ex)
			{
				ErrorDialog.bug(ex);
			}
			
			helpButton.Click += new System.EventHandler(new ShowHelpAction(hf.Contents, null).actionPerformed);
			SupportClass.CommandManager.CheckCommand(helpButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			b.Controls.Add(saveButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			b.Controls.Add(helpButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			b.Controls.Add(cancelButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(b);
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
			System.Windows.Forms.Form generatedAux10 = Owner;
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		}
	}
}