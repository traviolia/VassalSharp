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
using Documentation = VassalSharp.build.module.Documentation;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using SavedGameUpdater = VassalSharp.tools.SavedGameUpdater;
using ScrollPane = VassalSharp.tools.ScrollPane;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.configure
{
	
	[Serializable]
	public class SavedGameUpdaterDialog:System.Windows.Forms.Form
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(SavedGameUpdaterDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SavedGameUpdaterDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SavedGameUpdaterDialog enclosingInstance;
			public SavedGameUpdaterDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.importPieceInfo();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(SavedGameUpdaterDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SavedGameUpdaterDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SavedGameUpdaterDialog enclosingInstance;
			public SavedGameUpdaterDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.exportPieceInfo();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(SavedGameUpdaterDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SavedGameUpdaterDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SavedGameUpdaterDialog enclosingInstance;
			public SavedGameUpdaterDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.chooseSavedGames();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDefaultListCellRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_ISSUE: Class 'javax.swing.DefaultListCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
		[Serializable]
		private class AnonymousClassDefaultListCellRenderer:DefaultListCellRenderer
		{
			public AnonymousClassDefaultListCellRenderer(SavedGameUpdaterDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SavedGameUpdaterDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SavedGameUpdaterDialog enclosingInstance;
			public SavedGameUpdaterDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			public System.Windows.Forms.Control getListCellRendererComponent(System.Windows.Forms.ListBox list, System.Object value_Renamed, int index, bool isSelected, bool cellHasFocus)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.DefaultListCellRenderer.getListCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
				base.getListCellRendererComponent(list, value_Renamed, index, isSelected, cellHasFocus);
				Text = ((System.IO.FileInfo) value_Renamed).Name;
				return this;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener3
		{
			public AnonymousClassActionListener3(SavedGameUpdaterDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SavedGameUpdaterDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SavedGameUpdaterDialog enclosingInstance;
			public SavedGameUpdaterDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.updateGames();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener4
		{
			public AnonymousClassActionListener4(SavedGameUpdaterDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SavedGameUpdaterDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SavedGameUpdaterDialog enclosingInstance;
			public SavedGameUpdaterDialog Enclosing_Instance
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
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(SavedGameUpdaterDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassRunnable1 : IThreadRunnable
			{
				public AnonymousClassRunnable1(System.IO.IOException e, AnonymousClassRunnable enclosingInstance)
				{
					InitBlock(e, enclosingInstance);
				}
				private void  InitBlock(System.IO.IOException e, AnonymousClassRunnable enclosingInstance)
				{
					this.e = e;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable e was copied into class AnonymousClassRunnable1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.IO.IOException e;
				private AnonymousClassRunnable enclosingInstance;
				public AnonymousClassRunnable Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  Run()
				{
					Enclosing_Instance.Enclosing_Instance.showErrorMessage(e, "Update failed", "Unable to save file");
				}
			}
			private void  InitBlock(SavedGameUpdaterDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SavedGameUpdaterDialog enclosingInstance;
			public SavedGameUpdaterDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				for (int i = 0, n = Enclosing_Instance.savedGamesModel.Count; i < n; ++i)
				{
					try
					{
						System.IO.FileInfo savedGame = (System.IO.FileInfo) Enclosing_Instance.savedGamesModel[i];
						Enclosing_Instance.updater.updateSavedGame(Enclosing_Instance.oldPieceInfo, savedGame);
						GameModule.getGameModule().warn("Updated " + savedGame.Name + " from version " + Enclosing_Instance.versionField.Text + " to " + GameModule.getGameModule().getGameVersion());
					}
					// FIXME: review error message
					catch (System.IO.IOException e)
					{
						IThreadRunnable showError = new AnonymousClassRunnable1(e, this);
						try
						{
							//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeAndWait' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
							SwingUtilities.invokeAndWait(showError);
						}
						// FIXME: review error message
						catch (System.Threading.ThreadInterruptedException e1)
						{
						}
						// FIXME: review error message
						catch (System.Reflection.TargetInvocationException e1)
						{
						}
					}
				}
				Enclosing_Instance.updateButton.Enabled = true;
			}
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
		private System.Windows.Forms.ListBox.ObjectCollection savedGamesModel;
		private SavedGameUpdater updater = new SavedGameUpdater();
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		private System.Collections.Specialized.NameValueCollection oldPieceInfo;
		private System.Windows.Forms.FileDialog fc;
		private const System.String VERSION_KEY = "moduleVerion";
		private const System.String MODULE_NAME_KEY = "moduleName";
		private System.Windows.Forms.Button updateButton;
		private System.Windows.Forms.TextBox versionField;
		
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public SavedGameUpdaterDialog(System.Windows.Forms.Form owner):base()
		{
			//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_boolean'"
			SupportClass.DialogSupport.SetDialog(this, owner);
			Text = "Update Saved Games";
			initComponents();
			//UPGRADE_TODO: Constructor may need to be changed depending on function performed by the 'System.Windows.Forms.FileDialog' object. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1270'"
			fc = new System.Windows.Forms.OpenFileDialog();
			fc.setCurrentDirectory(GameModule.getGameModule().getFileChooser().getCurrentDirectory());
		}
		
		private void  initComponents()
		{
			//UPGRADE_ISSUE: Method 'javax.swing.JDialog.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJDialogsetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			setLayout(new BoxLayout(((System.Windows.Forms.ContainerControl) this), BoxLayout.Y_AXIS));
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box versionBox = Box.createHorizontalBox();
			System.Windows.Forms.Label temp_label2;
			temp_label2 = new System.Windows.Forms.Label();
			temp_label2.Text = "Module version of saved games:  ";
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = temp_label2;
			versionBox.Controls.Add(temp_Control);
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			versionField = new System.Windows.Forms.TextBox();
			versionField.ReadOnly = !false;
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetMaximumSize'"
			versionField.setMaximumSize(new System.Drawing.Size(versionField.getMaximumSize().Width, versionField.Size.Height));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			versionBox.Controls.Add(versionField);
			System.Windows.Forms.Button importButton = SupportClass.ButtonSupport.CreateStandardButton("Import GamePiece info");
			importButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(importButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			versionBox.Controls.Add(importButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(versionBox);
			System.Windows.Forms.Button exportButton = SupportClass.ButtonSupport.CreateStandardButton("Export GamePiece info");
			exportButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(exportButton);
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box importExportBox = Box.createHorizontalBox();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			importExportBox.Controls.Add(importButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			importExportBox.Controls.Add(exportButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(importExportBox);
			
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box savedGamesBox = Box.createHorizontalBox();
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box left = Box.createVerticalBox();
			System.Windows.Forms.Label temp_label4;
			temp_label4 = new System.Windows.Forms.Label();
			temp_label4.Text = "Saved Games:";
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control2;
			temp_Control2 = temp_label4;
			left.Controls.Add(temp_Control2);
			System.Windows.Forms.Button chooseGamesButton = SupportClass.ButtonSupport.CreateStandardButton("Choose");
			chooseGamesButton.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(chooseGamesButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			left.Controls.Add(chooseGamesButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			savedGamesBox.Controls.Add(left);
			//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
			savedGamesModel = new System.Windows.Forms.ListBox.ObjectCollection(new System.Windows.Forms.ListBox());
			System.Windows.Forms.ListBox temp_ListBox;
			temp_ListBox = new System.Windows.Forms.ListBox();
			temp_ListBox.Items.AddRange(savedGamesModel);
			temp_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			System.Windows.Forms.ListBox savedGamesList = temp_ListBox;
			//UPGRADE_ISSUE: Method 'javax.swing.JList.setVisibleRowCount' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJListsetVisibleRowCount_int'"
			savedGamesList.setVisibleRowCount(5);
			//UPGRADE_ISSUE: Method 'javax.swing.JList.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJListsetCellRenderer_javaxswingListCellRenderer'"
			savedGamesList.setCellRenderer(new AnonymousClassDefaultListCellRenderer(this));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control3;
			temp_Control3 = new ScrollPane(savedGamesList);
			savedGamesBox.Controls.Add(temp_Control3);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(savedGamesBox);
			
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box buttonsBox = Box.createHorizontalBox();
			updateButton = SupportClass.ButtonSupport.CreateStandardButton("Update games");
			updateButton.Click += new System.EventHandler(new AnonymousClassActionListener3(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(updateButton);
			updateButton.Enabled = false;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonsBox.Controls.Add(updateButton);
			System.Windows.Forms.Button helpButton = SupportClass.ButtonSupport.CreateStandardButton("Help");
			
			HelpFile hf = null;
			try
			{
				hf = new HelpFile(null, new System.IO.FileInfo(new System.IO.FileInfo(Documentation.DocumentationBaseDir.FullName + "\\" + "ReferenceManual").FullName + "\\" + "SavedGameUpdater.htm"));
			}
			catch (System.UriFormatException ex)
			{
				ErrorDialog.bug(ex);
			}
			
			helpButton.Click += new System.EventHandler(new ShowHelpAction(hf.Contents, null).actionPerformed);
			SupportClass.CommandManager.CheckCommand(helpButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonsBox.Controls.Add(helpButton);
			System.Windows.Forms.Button closeButton = SupportClass.ButtonSupport.CreateStandardButton("Close");
			closeButton.Click += new System.EventHandler(new AnonymousClassActionListener4(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(closeButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonsBox.Controls.Add(closeButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(buttonsBox);
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
			System.Windows.Forms.Form generatedAux22 = Owner;
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		}
		
		private void  updateGames()
		{
			updateButton.Enabled = false;
			IThreadRunnable runnable = new AnonymousClassRunnable(this);
			new SupportClass.ThreadClass(new System.Threading.ThreadStart(runnable.Run)).Start();
		}
		
		private void  chooseSavedGames()
		{
			//UPGRADE_TODO: The following member does not work when object instance is defined as 'System.Windows.Forms.SaveFileDialog' type.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1269'"
			((System.Windows.Forms.OpenFileDialog) fc).Multiselect = true;
			//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JFileChooser.CANCEL_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.JFileChooser.showOpenDialog' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			if ((int) System.Windows.Forms.DialogResult.Cancel != (int) fc.ShowDialog(this))
			{
				System.IO.FileInfo[] selectedFiles = SupportClass.GetSelectedFiles(fc.FileNames);
				if (selectedFiles != null)
				{
					savedGamesModel.Clear();
					for (int i = 0; i < selectedFiles.Length; i++)
					{
						savedGamesModel.Add(selectedFiles[i]);
					}
				}
			}
		}
		
		private void  exportPieceInfo()
		{
			//UPGRADE_TODO: The following member does not work when object instance is defined as 'System.Windows.Forms.SaveFileDialog' type.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1269'"
			((System.Windows.Forms.OpenFileDialog) fc).Multiselect = false;
			//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JFileChooser.CANCEL_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.JFileChooser.showSaveDialog' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			if ((int) System.Windows.Forms.DialogResult.Cancel != (int) fc.ShowDialog(this))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				System.Collections.Specialized.NameValueCollection p = updater.PieceSlotsMap;
				p[(System.String) MODULE_NAME_KEY] = (System.String) GameModule.getGameModule().getGameName();
				p[(System.String) VERSION_KEY] = (System.String) GameModule.getGameModule().getGameVersion();
				
				System.IO.BufferedStream out_Renamed = null;
				try
				{
					//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javaioFile'"
					out_Renamed = new System.IO.BufferedStream(new System.IO.FileStream(new System.IO.FileInfo(fc.FileName).FullName, System.IO.FileMode.Create));
					//UPGRADE_ISSUE: Method 'java.util.Properties.store' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilPropertiesstore_javaioOutputStream_javalangString'"
					p.store(out_Renamed, null);
					out_Renamed.Close();
				}
				// FIXME: review error message
				catch (System.IO.IOException e)
				{
					showErrorMessage(e, "Export failed", "Unable to write info");
				}
				finally
				{
					IOUtils.closeQuietly(out_Renamed);
				}
			}
		}
		
		private void  importPieceInfo()
		{
			//UPGRADE_TODO: The following member does not work when object instance is defined as 'System.Windows.Forms.SaveFileDialog' type.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1269'"
			((System.Windows.Forms.OpenFileDialog) fc).Multiselect = false;
			//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JFileChooser.CANCEL_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.JFileChooser.showOpenDialog' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			if ((int) System.Windows.Forms.DialogResult.Cancel != (int) fc.ShowDialog(this))
			{
				//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				oldPieceInfo = new System.Collections.Specialized.NameValueCollection();
				
				System.IO.BufferedStream in_Renamed = null;
				try
				{
					//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
					in_Renamed = new System.IO.BufferedStream(new System.IO.FileStream(new System.IO.FileInfo(fc.FileName).FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read));
					//UPGRADE_TODO: Method 'java.util.Properties.load' was converted to 'System.Collections.Specialized.NameValueCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiesload_javaioInputStream'"
					oldPieceInfo = new System.Collections.Specialized.NameValueCollection(System.Configuration.ConfigurationSettings.AppSettings);
					in_Renamed.Close();
					
					System.String moduleVersion = oldPieceInfo.Get(VERSION_KEY);
					System.String moduleName = oldPieceInfo.Get(MODULE_NAME_KEY);
					if (!GameModule.getGameModule().getGameName().equals(moduleName))
					{
						showErrorMessage(null, "Import failed", "Imported info is from the wrong module:  " + moduleName);
						oldPieceInfo = null;
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						versionField.Text = null;
					}
					else if (GameModule.getGameModule().getGameVersion().equals(moduleVersion))
					{
						showErrorMessage(null, "Import failed", "Imported info is from the current version, " + moduleVersion + ".\nLoad the older version in the editor and export the GamePiece info,\nThen load this module again and import the older version's info");
						oldPieceInfo = null;
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						versionField.Text = null;
					}
					else
					{
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						versionField.Text = moduleVersion;
					}
				}
				// FIXME: review error message
				catch (System.IO.IOException e)
				{
					showErrorMessage(e, "Import failed", "Unable to import info");
					oldPieceInfo = null;
				}
				catch (System.ArgumentException e)
				{
					// catches malformed input files
					showErrorMessage(e, "Import failed", "Malformed input file");
					oldPieceInfo = null;
				}
				finally
				{
					IOUtils.closeQuietly(in_Renamed);
				}
			}
			updateButton.Enabled = oldPieceInfo != null;
		}
		
		private void  showErrorMessage(System.Exception e, System.String title, System.String defaultMessage)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.String msg = e == null?null:e.Message;
			if (msg == null)
			{
				msg = defaultMessage;
			}
			SupportClass.OptionPaneSupport.ShowMessageDialog(this, msg, title, (int) System.Windows.Forms.MessageBoxIcon.Error);
		}
	}
}