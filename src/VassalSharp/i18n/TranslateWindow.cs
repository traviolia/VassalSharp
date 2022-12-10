/*
* $Id$
*
* Copyright (c) 2000-2007 by Rodney Kinney, Brent Easton
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
using System.Runtime.InteropServices;
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using HelpWindow = VassalSharp.build.module.documentation.HelpWindow;
using ConfigureTree = VassalSharp.configure.ConfigureTree;
using PropertiesWindow = VassalSharp.configure.PropertiesWindow;
using ShowHelpAction = VassalSharp.configure.ShowHelpAction;
using WriteErrorDialog = VassalSharp.tools.WriteErrorDialog;
namespace VassalSharp.i18n
{
	
	/// <summary> Window for editing translations of a {@link Configurable} object</summary>
	[Serializable]
	public class TranslateWindow:System.Windows.Forms.Form
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(TranslateWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TranslateWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateWindow enclosingInstance;
			public TranslateWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs we)
			{
				we.Cancel = true;
				Enclosing_Instance.cancel();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(TranslateWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TranslateWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateWindow enclosingInstance;
			public TranslateWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.commitTableEdit();
				System.String selectedTranslation = (System.String) ((System.Windows.Forms.ComboBox) event_sender).SelectedItem;
				Enclosing_Instance.changeLanguage(selectedTranslation);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(TranslateWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TranslateWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateWindow enclosingInstance;
			public TranslateWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.getNewTranslation();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassFocusListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassFocusListener
		{
			public AnonymousClassFocusListener(TranslateWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TranslateWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateWindow enclosingInstance;
			public TranslateWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  focusGained(System.Object event_sender, System.EventArgs e)
			{
			}
			public virtual void  focusLost(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.commitTableEdit();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(TranslateWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TranslateWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateWindow enclosingInstance;
			public TranslateWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				try
				{
					Enclosing_Instance.save();
				}
				catch (System.IO.IOException e1)
				{
					WriteErrorDialog.error(e1, GameModule.getGameModule().getArchiveWriter().getName());
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener3
		{
			public AnonymousClassActionListener3(TranslateWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TranslateWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateWindow enclosingInstance;
			public TranslateWindow Enclosing_Instance
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
		virtual protected internal System.Windows.Forms.Control HeaderPanel
		{
			get
			{
				System.Windows.Forms.Panel langPanel = new System.Windows.Forms.Panel();
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Language:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				langPanel.Controls.Add(temp_Control);
				langBox = SupportClass.ComboBoxSupport.CreateComboBox(Localization.Instance.TranslationList);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				langPanel.Controls.Add(langBox);
				boxListener = new AnonymousClassActionListener(this);
				langBox.SelectedValueChanged += new System.EventHandler(boxListener.actionPerformed);
				SupportClass.CommandManager.CheckCommand(langBox);
				if (Localization.Instance.TranslationList.Length > 0)
				{
					langBox.SelectedIndex = 0;
				}
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMinimumSize_javaawtDimension'"
				langPanel.setMinimumSize(new System.Drawing.Size(800, 0));
				
				System.Windows.Forms.Button addButton = SupportClass.ButtonSupport.CreateStandardButton("Add translation");
				addButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(addButton);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				langPanel.Controls.Add(addButton);
				
				return langPanel;
			}
			
		}
		virtual protected internal System.Windows.Forms.Control ButtonPanel
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'buttonBox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Panel buttonBox = new System.Windows.Forms.Panel();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'helpButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button helpButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.HELP));
				helpButton.Click += new System.EventHandler(new ShowHelpAction(HelpFile.getReferenceManualPage("Translations.htm", "module").getContents(), null).actionPerformed);
				SupportClass.CommandManager.CheckCommand(helpButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				buttonBox.Controls.Add(helpButton);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'okButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button okButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.OK));
				okButton.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(okButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				buttonBox.Controls.Add(okButton);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'cancelButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button cancelButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.CANCEL));
				cancelButton.Click += new System.EventHandler(new AnonymousClassActionListener3(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(cancelButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				buttonBox.Controls.Add(cancelButton);
				
				return buttonBox;
			}
			
		}
		private const long serialVersionUID = 1L;
		protected internal static System.Drawing.Color TRANSLATION_NEEDED_COLOR
		{
			get
			{
				return TRANSLATION_NEEDED_COLOR_Renamed;
			}
			
			set
			{
				TRANSLATION_NEEDED_COLOR_Renamed = value;
			}
			
		}
		protected internal static System.Drawing.Color TRANSLATION_NEEDED_COLOR_Renamed = System.Drawing.Color.Red;
		protected internal static System.Drawing.Color TRANSLATION_DONE_COLOR
		{
			get
			{
				return TRANSLATION_DONE_COLOR_Renamed;
			}
			
			set
			{
				TRANSLATION_DONE_COLOR_Renamed = value;
			}
			
		}
		protected internal static System.Drawing.Color TRANSLATION_DONE_COLOR_Renamed = System.Drawing.Color.Blue;
		protected internal static System.Drawing.Color NO_TRANSLATION_NEEDED_COLOR
		{
			get
			{
				return NO_TRANSLATION_NEEDED_COLOR_Renamed;
			}
			
			set
			{
				NO_TRANSLATION_NEEDED_COLOR_Renamed = value;
			}
			
		}
		protected internal static System.Drawing.Color NO_TRANSLATION_NEEDED_COLOR_Renamed = System.Drawing.Color.Black;
		
		protected internal Translatable target;
		
		protected internal System.String[] keys;
		//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		protected internal System.Windows.Forms.DataGrid keyTable;
		protected internal Translatable keyTarget;
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		protected internal System.Windows.Forms.TreeView tree;
		protected internal Translation currentTranslation = null;
		protected internal System.Windows.Forms.ComboBox langBox;
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		protected internal ActionListener boxListener;
		protected internal int lastSelectedLangIndex;
		protected internal System.String currentKey = ""; //$NON-NLS-1$
		protected internal ConfigureTree myConfigureTree;
		protected internal CopyButton[] copyButtons;
		
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public TranslateWindow(System.Windows.Forms.Form owner, bool modal, Translatable target, HelpWindow helpWindow, ConfigureTree tree):base()
		{
			//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_boolean'"
			SupportClass.DialogSupport.SetDialog(this, owner);
			this.target = target;
			myConfigureTree = tree;
			initComponents();
		}
		
		protected internal virtual void  initComponents()
		{
			Text = "Translate " + VassalSharp.configure.ConfigureTree.getConfigureName((Configurable) target);
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			new BorderLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			System.Windows.Forms.Panel mainPanel = new System.Windows.Forms.Panel();
			/*
			* Place Language selector above Tree and Keys
			*/
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			mainPanel.Controls.Add(HeaderPanel);
			HeaderPanel.Dock = new System.Windows.Forms.DockStyle();
			HeaderPanel.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = buildMainPanel();
			mainPanel.Controls.Add(temp_Control);
			temp_Control.Dock = System.Windows.Forms.DockStyle.Fill;
			temp_Control.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			mainPanel.Controls.Add(ButtonPanel);
			ButtonPanel.Dock = new System.Windows.Forms.DockStyle();
			ButtonPanel.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(mainPanel);
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
			System.Windows.Forms.Control generatedAux4 = Parent;
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Closing += new System.ComponentModel.CancelEventHandler(this.TranslateWindow_Closing_DO_NOTHING_ON_CLOSE);
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
		}
		
		/// <summary> User has clicked on the Add Translation button. Create a new
		/// PropertiesWindow for a translation and display it.
		/// 
		/// </summary>
		protected internal virtual void  getNewTranslation()
		{
			Translation t = new Translation();
			//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.getAncestorOfClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
			//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
			PropertiesWindow w = new MyPropertiesWindow((System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), this), false, t, null, this);
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			w.Visible = true;
		}
		
		/// <summary> Called from MyPropertiesWindow when the user saves the new translation</summary>
		/// <param name="target">new Translation
		/// </param>
		protected internal virtual void  refreshTranslationList(Configurable target)
		{
			Language language = GameModule.getGameModule().getComponentsOf(typeof(Language)).iterator().next();
			if (language != null)
			{
				myConfigureTree.externalInsert(language, target);
			}
			langBox.Items.Clear();
			System.String[] langs = Localization.Instance.TranslationList;
			for (int i = 0; i < langs.Length; i++)
			{
				langBox.Items.Add(langs[i]);
			}
			langBox.SelectedItem = ((Translation) target).getDescription();
			keyTable.Enabled = true;
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			tree.Refresh();
		}
		
		[Serializable]
		protected internal class MyPropertiesWindow:PropertiesWindow
		{
			private const long serialVersionUID = 1L;
			protected internal Configurable myTarget;
			protected internal TranslateWindow owningWindow;
			//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
			public MyPropertiesWindow(System.Windows.Forms.Form owner, bool modal, Configurable target, HelpWindow helpWindow, TranslateWindow tw):base(owner, modal, target, helpWindow)
			{
				myTarget = target;
				owningWindow = tw;
			}
			public override void  save()
			{
				base.save();
				owningWindow.refreshTranslationList(myTarget);
			}
			public override void  cancel()
			{
				Dispose();
			}
		}
		
		protected internal virtual System.Windows.Forms.Control buildMainPanel()
		{
			System.Windows.Forms.Panel keyPanel = buildKeyTablePanel();
			
			/*
			* Tree of all components from target component down
			*/
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			new BorderLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			System.Windows.Forms.Panel treePanel = new System.Windows.Forms.Panel();
			MyTreeNode top = new MyTreeNode(target);
			createNodes(top);
			tree = SupportClass.TreeSupport.CreateTreeView(top);
			//UPGRADE_ISSUE: Method 'javax.swing.tree.TreeSelectionModel.setSelectionMode' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreeSelectionModel'"
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreegetSelectionModel'"
			//UPGRADE_ISSUE: Field 'javax.swing.tree.TreeSelectionModel.SINGLE_TREE_SELECTION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreeSelectionModel'"
			tree.getSelectionModel().setSelectionMode(TreeSelectionModel.SINGLE_TREE_SELECTION);
			tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.valueChanged);
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.setSelectionRow' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetSelectionRow_int'"
			tree.setSelectionRow(0);
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetCellRenderer_javaxswingtreeTreeCellRenderer'"
			tree.setCellRenderer(new MyTreeCellRenderer(this));
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.HORIZONTAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsHORIZONTAL_SCROLLBAR_AS_NEEDED_f'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
			//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.VERTICAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsVERTICAL_SCROLLBAR_AS_NEEDED_f'"
			temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol.AutoScroll = true;
			temp_scrollablecontrol.Controls.Add(tree);
			System.Windows.Forms.ScrollableControl treeScroll = temp_scrollablecontrol;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			treePanel.Controls.Add(treeScroll);
			treeScroll.Dock = System.Windows.Forms.DockStyle.Fill;
			treeScroll.BringToFront();
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMinimumSize_javaawtDimension'"
			treePanel.setMinimumSize(new System.Drawing.Size(400, 100));
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			treePanel.Size = new System.Drawing.Size(800, 300);
			
			/*
			* First split between Tree display and Keys
			*/
			//UPGRADE_TODO: Class 'javax.swing.JSplitPane' was converted to 'SupportClass.SplitterPanelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			SupportClass.SplitterPanelSupport split1 = new SupportClass.SplitterPanelSupport((int) System.Windows.Forms.Orientation.Vertical, treePanel, keyPanel);
			//UPGRADE_ISSUE: Method 'javax.swing.JSplitPane.setResizeWeight' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJSplitPanesetResizeWeight_double'"
			split1.setResizeWeight(0.5);
			
			return split1;
		}
		
		protected internal virtual System.Windows.Forms.Panel buildKeyTablePanel()
		{
			/*
			* Key Panel - Table of Keys for the component currently selected in the
			* Tree Panel
			*/
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			new BorderLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			System.Windows.Forms.Panel keyPanel = new System.Windows.Forms.Panel();
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMinimumSize_javaawtDimension'"
			keyPanel.setMinimumSize(new System.Drawing.Size(800, 100));
			keyTable = new MyTable(this);
			//UPGRADE_ISSUE: Method 'javax.swing.JTable.setSelectionMode' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablesetSelectionMode_int'"
			//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.ListSelectionModel.SINGLE_SELECTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			keyTable.setSelectionMode((int) System.Windows.Forms.SelectionMode.One);
			
			keyTable.Enter += new System.EventHandler(new AnonymousClassFocusListener(this).focusGained);
			keyTable.Leave += new System.EventHandler(new AnonymousClassFocusListener(this).focusLost);
			
			//UPGRADE_ISSUE: Method 'javax.swing.ListSelectionModel.addListSelectionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingListSelectionModeladdListSelectionListener_javaxswingeventListSelectionListener'"
			//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionModel'"
			keyTable.getSelectionModel().addListSelectionListener(this);
			keyTable.Enabled = currentTranslation != null;
			
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.HORIZONTAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsHORIZONTAL_SCROLLBAR_AS_NEEDED_f'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
			//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.VERTICAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsVERTICAL_SCROLLBAR_AS_NEEDED_f'"
			temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol.AutoScroll = true;
			temp_scrollablecontrol.Controls.Add(keyTable);
			System.Windows.Forms.ScrollableControl keyScroll = temp_scrollablecontrol;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			keyPanel.Controls.Add(keyScroll);
			keyScroll.Dock = System.Windows.Forms.DockStyle.Fill;
			keyScroll.BringToFront();
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMinimumSize_javaawtDimension'"
			keyPanel.setMinimumSize(new System.Drawing.Size(400, 100));
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			keyPanel.Size = new System.Drawing.Size(800, 200);
			return keyPanel;
		}
		
		// Workaround for JRE Bug 4709394 - Cell editing lost when JTable loses
		// focus. Call this all over the place!
		protected internal virtual void  commitTableEdit()
		{
			if (keyTable != null && ((System.Data.DataTable) keyTable.DataSource).DefaultView[keyTable.CurrentCell.RowNumber].IsEdit)
			{
				int row = SupportClass.GetEditingRow(keyTable);
				int column = SupportClass.GetEditingColumn(keyTable);
				if (row != - 1 && column != - 1)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JTable.editCellAt' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTableeditCellAt_int_int'"
					keyTable.editCellAt(row, column);
				}
			}
		}
		
		/// <summary> New Language selected from the drop-down box</summary>
		protected internal virtual void  changeLanguage(System.String selectedTranslation)
		{
			if (currentTranslation != null)
			{
				if (currentTranslation.isDirty())
				{
					try
					{
						if (!querySave())
						{
							langBox.SelectedValueChanged -= new System.EventHandler(boxListener.actionPerformed);
							langBox.setSelectedItem(lastSelectedLangIndex);
							langBox.SelectedValueChanged += new System.EventHandler(boxListener.actionPerformed);
							SupportClass.CommandManager.CheckCommand(langBox);
						}
					}
					catch (System.IO.IOException e)
					{
						WriteErrorDialog.error(e, GameModule.getGameModule().getArchiveWriter().getName());
					}
				}
			}
			currentTranslation = Localization.Instance.getTranslation(selectedTranslation);
			lastSelectedLangIndex = langBox.SelectedIndex;
			if (keyTable != null)
			{
				((MyTableModel) keyTable.DataSource).update();
			}
		}
		
		/// <summary> When a new node is selected, display keys for the new component in the keys
		/// table
		/// </summary>
		public virtual void  valueChanged(System.Object event_sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			commitTableEdit();
			MyTreeNode node = (MyTreeNode) tree.SelectedNode;
			if (node == null)
				return ;
			keys = node.Target.I18nData.getAttributeKeys().toArray(new System.String[0]);
			copyButtons = new CopyButton[keys.Length];
			keyTarget = node.Target;
			//UPGRADE_ISSUE: Method 'javax.swing.table.AbstractTableModel.fireTableStructureChanged' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableAbstractTableModelfireTableStructureChanged'"
			//UPGRADE_TODO: Class 'javax.swing.table.AbstractTableModel' was converted to 'System.Data.DataTable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			((System.Data.DataTable) keyTable.DataSource).fireTableStructureChanged();
			if (keys != null && keys.Length > 0)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionModel'"
				keyTable.getSelectionModel().SetSelectionInterval(0, 0);
			}
			((MyTableModel) keyTable.DataSource).update();
		}
		
		
		/// <summary> When a key is selected in the table, display the source and translated
		/// texts in the right hand panels
		/// </summary>
		public virtual void  valueChanged(System.Object event_sender, System.EventArgs e)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.event.ListSelectionEvent.getValueIsAdjusting' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventListSelectionEventgetValueIsAdjusting'"
			if (e.getValueIsAdjusting())
				return ;
			
			//UPGRADE_TODO: Interface 'javax.swing.ListSelectionModel' was converted to 'SupportClass.ListSelectionModelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: The method 'javax.swing.event.ListSelectionEvent.getSource' needs to be in a event handling method in order to be properly converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1171'"
			SupportClass.ListSelectionModelSupport lsm = (SupportClass.ListSelectionModelSupport) e.getSource();
			if (lsm.SelectedItems.Count.Equals(0))
			{
				return ;
			}
			else
			{
				System.String key = keys[lsm.GetMinSelectionIndex()];
				currentKey = keyTarget.I18nData.FullPrefix + key; //$NON-NLS-1$
			}
		}
		
		/// <summary> Create the nodes for the JTree display</summary>
		
		protected internal virtual void  createNodes(MyTreeNode top)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Translatable child: top.getTarget().getI18nData().getChildren())
			{
				MyTreeNode childNode = new MyTreeNode(child);
				createNodes(childNode);
				top.Nodes.Add(childNode);
			}
		}
		
		public static System.String getDisplayName(Translatable t)
		{
			if (t == null)
			{
				return ""; //$NON-NLS-1$
			}
			System.String type = ConfigureTree.getConfigureName(t.GetType());
			System.String name = ""; //$NON-NLS-1$
			if (t is Configurable)
			{
				name = ((Configurable) t).getConfigureName();
			}
			System.String s = (name == null)?"":(name + " "); //$NON-NLS-1$ //$NON-NLS-2$
			return s + " [" + type + "]"; //$NON-NLS-1$ //$NON-NLS-2$
		}
		
		/// <summary> Cancel button clicked. Check for outstanding changes.</summary>
		protected internal virtual void  cancel()
		{
			commitTableEdit();
			if (currentTranslation != null)
			{
				if (currentTranslation.isDirty())
				{
					try
					{
						if (!querySave())
						{
							return ;
						}
					}
					catch (System.IO.IOException e)
					{
						WriteErrorDialog.error(e, GameModule.getGameModule().getArchiveWriter().getName());
					}
				}
			}
			Dispose();
		}
		
		protected internal virtual bool querySave()
		{
			switch (SupportClass.OptionPaneSupport.ShowConfirmDialog(this, "Do you want to save these changes now?", "Unsaved Changes", (int) System.Windows.Forms.MessageBoxButtons.YesNoCancel))
			{
				
				//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.YES_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				case (int) System.Windows.Forms.DialogResult.Yes: 
					saveTranslation();
					return true;
				
				//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.NO_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				case (int) System.Windows.Forms.DialogResult.No: 
					reloadTranslation();
					return true;
				
				//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.CANCEL_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				case (int) System.Windows.Forms.DialogResult.Cancel: 
					return false;
				}
			return true;
		}
		
		/// <summary> Save button clicked</summary>
		/// <throws>  IOException </throws>
		protected internal virtual void  save()
		{
			commitTableEdit();
			if (saveTranslation())
			{
				Dispose();
			}
		}
		
		/// <summary> Save the current Translation</summary>
		/// <throws>  IOException </throws>
		protected internal virtual bool saveTranslation()
		{
			if (currentTranslation != null)
			{
				currentTranslation.saveProperties();
			}
			return true;
		}
		
		/// <summary> Reload the current translation from the archive</summary>
		/// <throws>  IOException </throws>
		protected internal virtual void  reloadTranslation()
		{
			if (currentTranslation != null)
			{
				currentTranslation.reloadProperties();
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MyTable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Custome JTable to support CopyButtons in JTable cells
		/// 
		/// </summary>
		//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		[Serializable]
		internal class MyTable:System.Windows.Forms.DataGrid
		{
			private void  InitBlock(TranslateWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateWindow enclosingInstance;
			public TranslateWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			private const long serialVersionUID = 1L;
			
			//UPGRADE_ISSUE: Constructor 'javax.swing.JTable.JTable' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000'"
			public MyTable(TranslateWindow enclosingInstance):base(new MyTableModel(enclosingInstance))
			{
				InitBlock(enclosingInstance);
				//UPGRADE_ISSUE: Method 'javax.swing.JTable.setDefaultRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablesetDefaultRenderer_javalangClass_javaxswingtableTableCellRenderer'"
				setDefaultRenderer(typeof(System.Windows.Forms.Control), new JComponentCellRenderer());
				//UPGRADE_ISSUE: Method 'javax.swing.JTable.setDefaultEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablesetDefaultEditor_javalangClass_javaxswingtableTableCellEditor'"
				setDefaultEditor(typeof(System.Windows.Forms.Control), new JComponentCellEditor());
			}
			
			//UPGRADE_ISSUE: Interface 'javax.swing.table.TableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableCellRenderer'"
			//UPGRADE_NOTE: The equivalent of method 'javax.swing.JTable.getCellRenderer' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public TableCellRenderer getCellRenderer(int row, int column)
			{
				//UPGRADE_TODO: Class 'javax.swing.table.TableColumn' was converted to 'System.Data.DataColumn' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Data.DataColumn tableColumn = ((System.Data.DataTable) DataSource).Columns[column];
				//UPGRADE_ISSUE: Interface 'javax.swing.table.TableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableCellRenderer'"
				//UPGRADE_TODO: Method 'javax.swing.table.TableColumn.getCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				TableCellRenderer renderer = tableColumn.getCellRenderer();
				if (renderer == null)
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					Class < ? > c = getColumnClass(column);
					if (c.equals(typeof(System.Object)))
					{
						System.Object o = ((System.Data.DataTable) DataSource).Rows[row][column];
						if (o != null)
							c = ((System.Data.DataTable) DataSource).Rows[row][column].GetType();
					}
					renderer = getDefaultRenderer(c);
				}
				return renderer;
			}
			
			//UPGRADE_ISSUE: Interface 'javax.swing.table.TableCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableCellEditor'"
			//UPGRADE_NOTE: The equivalent of method 'javax.swing.JTable.getCellEditor' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public TableCellEditor getCellEditor(int row, int column)
			{
				//UPGRADE_TODO: Class 'javax.swing.table.TableColumn' was converted to 'System.Data.DataColumn' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Data.DataColumn tableColumn = ((System.Data.DataTable) DataSource).Columns[column];
				//UPGRADE_ISSUE: Interface 'javax.swing.table.TableCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableCellEditor'"
				//UPGRADE_TODO: Method 'javax.swing.table.TableColumn.getCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				TableCellEditor editor = tableColumn.getCellEditor();
				if (editor == null)
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					Class < ? > c = getColumnClass(column);
					if (c.equals(typeof(System.Object)))
					{
						System.Object o = ((System.Data.DataTable) DataSource).Rows[row][column];
						if (o != null)
							c = ((System.Data.DataTable) DataSource).Rows[row][column].GetType();
					}
					editor = getDefaultEditor(c);
				}
				return editor;
			}
		}
		
		/// <summary> Custome Cell Renderer to support CopyButtons in JTable cells
		/// 
		/// </summary>
		//UPGRADE_ISSUE: Interface 'javax.swing.table.TableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableCellRenderer'"
		protected internal class JComponentCellRenderer : TableCellRenderer
		{
			//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			public virtual System.Windows.Forms.Control getTableCellRendererComponent(System.Windows.Forms.DataGrid table, System.Object value_Renamed, bool isSelected, bool hasFocus, int row, int column)
			{
				return (System.Windows.Forms.Control) value_Renamed;
			}
		}
		
		/// <summary> Custom CellEditor to support CopyButtons in JTable cells
		/// 
		/// </summary>
		//UPGRADE_ISSUE: Interface 'javax.swing.table.TableCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableCellEditor'"
		//UPGRADE_ISSUE: Interface 'javax.swing.tree.TreeCellEditor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreeCellEditor'"
		[Serializable]
		protected internal class JComponentCellEditor : TableCellEditor, TreeCellEditor
		{
			virtual public System.Windows.Forms.Control Component
			{
				get
				{
					return editorComponent;
				}
				
			}
			
			private const long serialVersionUID = 1L;
			
			protected internal System.Collections.Hashtable listenerList = new System.Collections.Hashtable();
			[NonSerialized]
			protected internal System.EventArgs changeEvent = null;
			
			protected internal System.Windows.Forms.Control editorComponent = null;
			protected internal System.Windows.Forms.Control container = null; // Can be tree or table
			
			
			public virtual System.Object getCellEditorValue()
			{
				return editorComponent;
			}
			
			public virtual bool isCellEditable(System.Object event_sender, System.EventArgs anEvent)
			{
				return true;
			}
			
			public virtual bool shouldSelectCell(System.Object event_sender, System.EventArgs anEvent)
			{
				//UPGRADE_ISSUE: Method 'java.awt.AWTEvent.getID' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAWTEventgetID'"
				//UPGRADE_ISSUE: Field 'java.awt.event.MouseEvent.MOUSE_PRESSED' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventMOUSE_PRESSED_f'"
				if (editorComponent != null && anEvent is System.Windows.Forms.MouseEventArgs && ((System.Windows.Forms.MouseEventArgs) anEvent).getID() == MouseEvent.MOUSE_PRESSED)
				{
					//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getDeepestComponentAt' was converted to 'System.Windows.Forms.Control.GetChildAtPoint' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetDeepestComponentAt_javaawtComponent_int_int'"
					System.Windows.Forms.Control dispatchComponent = editorComponent.GetChildAtPoint(new System.Drawing.Point(3, 3));
					//UPGRADE_ISSUE: Method 'javax.swing.AbstractButton.setSelected' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractButtonsetSelected_boolean'"
					((CopyButton) dispatchComponent).setSelected(true);
				}
				return false;
			}
			
			public virtual bool stopCellEditing()
			{
				return true;
			}
			
			public virtual void  cancelCellEditing()
			{
			}
			
			//UPGRADE_TODO: Interface 'javax.swing.event.CellEditorListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			public virtual void  addCellEditorListener(CellEditorListener l)
			{
			}
			
			//UPGRADE_TODO: Interface 'javax.swing.event.CellEditorListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			public virtual void  removeCellEditorListener(CellEditorListener l)
			{
			}
			
			// implements javax.swing.tree.TreeCellEditor
			//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			public virtual System.Windows.Forms.Control getTreeCellEditorComponent(System.Windows.Forms.TreeView tree, System.Object value_Renamed, bool isSelected, bool expanded, bool leaf, int row)
			{
				//      String stringValue = tree.convertValueToText(value, isSelected,
				//        expanded, leaf, row, false);
				
				editorComponent = (System.Windows.Forms.Control) value_Renamed;
				container = tree;
				return editorComponent;
			}
			
			// implements javax.swing.table.TableCellEditor
			//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			public virtual System.Windows.Forms.Control getTableCellEditorComponent(System.Windows.Forms.DataGrid table, System.Object value_Renamed, bool isSelected, int row, int column)
			{
				
				editorComponent = (System.Windows.Forms.Control) value_Renamed;
				container = table;
				return editorComponent;
			}
		} // End of class JComponentCellEditor
		
		
		/// <summary> Custom Key Table Model</summary>
		internal const int ATTR_COL = 0;
		internal const int SOURCE_COL = 1;
		internal const int CC_COL = 2;
		internal const int TRAN_COL = 3;
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MyTableModel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_TODO: Class 'javax.swing.table.AbstractTableModel' was converted to 'System.Data.DataTable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		[Serializable]
		internal class MyTableModel:System.Data.DataTable
		{
			public MyTableModel(TranslateWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TranslateWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateWindow enclosingInstance;
			public TranslateWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			private const long serialVersionUID = 1L;
			
			//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.getColumnCount' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public int getColumnCount()
			{
				return 4;
			}
			
			//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.getColumnName' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public System.String getColumnName(int col)
			{
				switch (col)
				{
					
					case VassalSharp.i18n.TranslateWindow.ATTR_COL: 
						return "Attribute";
					
					case VassalSharp.i18n.TranslateWindow.SOURCE_COL: 
						return "Source Text";
					
					case VassalSharp.i18n.TranslateWindow.CC_COL: 
						return "cc";
					
					case VassalSharp.i18n.TranslateWindow.TRAN_COL: 
						return "Translation";
					}
				return null;
			}
			
			//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.getRowCount' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public int getRowCount()
			{
				return Enclosing_Instance.keys == null?0:Enclosing_Instance.keys.Length;
			}
			
			//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.getValueAt' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public System.Object getValueAt(int row, int col)
			{
				switch (col)
				{
					
					case VassalSharp.i18n.TranslateWindow.ATTR_COL: 
						return Enclosing_Instance.keys == null?null:Enclosing_Instance.keyTarget.I18nData.getAttributeDescription(Enclosing_Instance.keys[row]);
					
					case VassalSharp.i18n.TranslateWindow.SOURCE_COL: 
						return Enclosing_Instance.keys == null?null:Enclosing_Instance.keyTarget.getAttributeValueString(Enclosing_Instance.keys[row]);
					
					case VassalSharp.i18n.TranslateWindow.CC_COL: 
						if (Enclosing_Instance.copyButtons[row] == null)
						{
							Enclosing_Instance.copyButtons[row] = new CopyButton(enclosingInstance, row);
						}
						return Enclosing_Instance.copyButtons[row];
					
					
					case VassalSharp.i18n.TranslateWindow.TRAN_COL: 
						if (Enclosing_Instance.currentTranslation != null)
						{
							System.String key = Enclosing_Instance.keyTarget.I18nData.FullPrefix + Enclosing_Instance.keys[row]; //$NON-NLS-1$
							return Enclosing_Instance.currentTranslation.translate(key);
						}
						break;
					}
				return null;
			}
			
			//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.setValueAt' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public void  setValueAt(System.Object value_Renamed, int row, int col)
			{
				if (col == VassalSharp.i18n.TranslateWindow.TRAN_COL)
				{
					Enclosing_Instance.currentTranslation.setProperty(Enclosing_Instance.currentKey, (System.String) value_Renamed);
					Enclosing_Instance.copyButtons[row].checkEnabled();
					//UPGRADE_ISSUE: Method 'javax.swing.table.AbstractTableModel.fireTableCellUpdated' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableAbstractTableModelfireTableCellUpdated_int_int'"
					fireTableCellUpdated(row, col);
					//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
					Enclosing_Instance.tree.Refresh();
				}
			}
			
			//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.isCellEditable' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public bool isCellEditable(int row, int col)
			{
				return col == VassalSharp.i18n.TranslateWindow.TRAN_COL || col == VassalSharp.i18n.TranslateWindow.CC_COL;
			}
			
			public virtual void  update()
			{
				//UPGRADE_ISSUE: Method 'javax.swing.table.AbstractTableModel.fireTableStructureChanged' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableAbstractTableModelfireTableStructureChanged'"
				this.fireTableStructureChanged();
				//UPGRADE_TODO: Method 'javax.swing.table.TableColumn.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				((System.Data.DataTable) Enclosing_Instance.keyTable.DataSource).Columns[VassalSharp.i18n.TranslateWindow.ATTR_COL].setCellRenderer(new MyTableCellRenderer(enclosingInstance, Enclosing_Instance.keyTarget));
				//UPGRADE_ISSUE: Method 'javax.swing.table.TableColumn.setMaxWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableColumnsetMaxWidth_int'"
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				((System.Data.DataTable) Enclosing_Instance.keyTable.DataSource).Columns[VassalSharp.i18n.TranslateWindow.CC_COL].setMaxWidth(25);
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MyTableCellRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Custom table cell renderer - Change color of key names based on translation
		/// status
		/// </summary>
		//UPGRADE_ISSUE: Class 'javax.swing.table.DefaultTableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
		[Serializable]
		internal class MyTableCellRenderer:DefaultTableCellRenderer
		{
			private void  InitBlock(TranslateWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateWindow enclosingInstance;
			public TranslateWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			private const long serialVersionUID = 1L;
			protected internal Translatable target;
			
			public MyTableCellRenderer(TranslateWindow enclosingInstance, Translatable target)
			{
				InitBlock(enclosingInstance);
				this.target = target;
			}
			
			//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			public System.Windows.Forms.Control getTableCellRendererComponent(System.Windows.Forms.DataGrid table, System.Object value_Renamed, bool isSelected, bool hasFocus, int row, int col)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.table.DefaultTableCellRenderer.getTableCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
				System.Windows.Forms.Control c = base.getTableCellRendererComponent(table, value_Renamed, isSelected, hasFocus, row, col);
				
				System.String fullKey = target.I18nData.FullPrefix + Enclosing_Instance.keys[row]; //$NON-NLS-1$
				System.String translation = Enclosing_Instance.currentTranslation == null?"":Enclosing_Instance.currentTranslation.translate(fullKey);
				System.String originalValue = target.getAttributeValueString(Enclosing_Instance.keys[row]);
				
				if (originalValue == null || originalValue.Length == 0)
				{
					c.ForeColor = VassalSharp.i18n.TranslateWindow.NO_TRANSLATION_NEEDED_COLOR;
				}
				else
				{
					if (translation == null || translation.Length == 0)
					{
						c.ForeColor = VassalSharp.i18n.TranslateWindow.TRANSLATION_NEEDED_COLOR;
					}
					else
					{
						c.ForeColor = VassalSharp.i18n.TranslateWindow.TRANSLATION_DONE_COLOR;
					}
				}
				
				return c;
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'CopyButton' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Custom button to copy source to translation
		/// 
		/// </summary>
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		[Serializable]
		protected internal class CopyButton:System.Windows.Forms.Button
		{
			private void  InitBlock(TranslateWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateWindow enclosingInstance;
			public TranslateWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			internal int row;
			public CopyButton(TranslateWindow enclosingInstance, int i):base()
			{
				InitBlock(enclosingInstance);
				this.Text = "->";
				row = i;
				Click += new System.EventHandler(this.actionPerformed);
				SupportClass.CommandManager.CheckCommand(this);
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractButton.setMargin' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractButtonsetMargin_javaawtInsets'"
				setMargin(new System.Int32[]{1, 1, 1, 1});
				checkEnabled();
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				System.String key = Enclosing_Instance.keys[row];
				Enclosing_Instance.currentKey = Enclosing_Instance.keyTarget.I18nData.FullPrefix + key; //$NON-NLS-1$
				Enclosing_Instance.currentTranslation.setProperty(Enclosing_Instance.currentKey, Enclosing_Instance.keyTarget.getAttributeValueString(Enclosing_Instance.keys[row]));
				checkEnabled();
				((MyTableModel) Enclosing_Instance.keyTable.DataSource).update();
			}
			public virtual void  checkEnabled()
			{
				if (Enclosing_Instance.keyTarget != null && Enclosing_Instance.keys != null && Enclosing_Instance.keys[row] != null)
				{
					System.String t = Enclosing_Instance.currentTranslation == null?"":Enclosing_Instance.currentTranslation.translate(Enclosing_Instance.keyTarget.I18nData.FullPrefix + Enclosing_Instance.keys[row]); //$NON-NLS-1$
					Enabled = t == null || t.Length == 0;
				}
				else
				{
					Enabled = true;
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MyTreeCellRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Custom tree cell renderer - Change color of component names based on
		/// translation status of children
		/// </summary>
		//UPGRADE_ISSUE: Class 'javax.swing.tree.DefaultTreeCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
		[Serializable]
		internal class MyTreeCellRenderer:DefaultTreeCellRenderer
		{
			public MyTreeCellRenderer(TranslateWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TranslateWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TranslateWindow enclosingInstance;
			public TranslateWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			private const long serialVersionUID = 1L;
			
			//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			public System.Windows.Forms.Control getTreeCellRendererComponent(System.Windows.Forms.TreeView tree, System.Object value_Renamed, bool sel, bool expanded, bool leaf, int row, bool hasFocus)
			{
				
				//UPGRADE_ISSUE: Method 'javax.swing.tree.DefaultTreeCellRenderer.getTreeCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
				System.Windows.Forms.Control c = base.getTreeCellRendererComponent(tree, value_Renamed, sel, expanded, leaf, row, hasFocus);
				
				Translatable t = ((MyTreeNode) value_Renamed).Target;
				if (t.I18nData.hasUntranslatedAttributes(Enclosing_Instance.currentTranslation))
				{
					c.ForeColor = VassalSharp.i18n.TranslateWindow.TRANSLATION_NEEDED_COLOR;
				}
				else
				{
					c.ForeColor = VassalSharp.i18n.TranslateWindow.NO_TRANSLATION_NEEDED_COLOR;
				}
				return c;
			}
		}
		
		/// <summary> Custom Tree Node implementation</summary>
		[Serializable]
		protected internal class MyTreeNode:System.Windows.Forms.TreeNode
		{
			virtual public Translatable Target
			{
				get
				{
					return component;
				}
				
			}
			private const long serialVersionUID = 1L;
			internal Translatable component;
			
			public MyTreeNode(Translatable t)
			{
				component = t;
			}
			
			public override System.String ToString()
			{
				return VassalSharp.i18n.TranslateWindow.getDisplayName(component);
			}
		}
		private void  TranslateWindow_Closing_DO_NOTHING_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 0);
		}
	}
}