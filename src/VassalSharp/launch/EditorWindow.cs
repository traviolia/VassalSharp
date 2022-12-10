/*
* $Id$
*
* Copyright (c) 2000-2008 by Brent Easton, Rodney Kinney, Joel Uckelman
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
//UPGRADE_TODO: The type 'org.apache.commons.lang.SystemUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SystemUtils = org.apache.commons.lang.SystemUtils;
using GameModule = VassalSharp.build.GameModule;
using Documentation = VassalSharp.build.module.Documentation;
using HelpWindow = VassalSharp.build.module.documentation.HelpWindow;
using ConfigureTree = VassalSharp.configure.ConfigureTree;
using ModuleUpdaterDialog = VassalSharp.configure.ModuleUpdaterDialog;
using SaveAction = VassalSharp.configure.SaveAction;
using SaveAsAction = VassalSharp.configure.SaveAsAction;
using ShowHelpAction = VassalSharp.configure.ShowHelpAction;
using ValidationReport = VassalSharp.configure.ValidationReport;
using ValidationReportDialog = VassalSharp.configure.ValidationReportDialog;
using Resources = VassalSharp.i18n.Resources;
using ApplicationIcons = VassalSharp.tools.ApplicationIcons;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using URLUtils = VassalSharp.tools.URLUtils;
//UPGRADE_TODO: The type 'VassalSharp.tools.menu.ChildProxy' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ChildProxy = VassalSharp.tools.menu.ChildProxy;
using MenuBarProxy = VassalSharp.tools.menu.MenuBarProxy;
using MenuManager = VassalSharp.tools.menu.MenuManager;
using MenuProxy = VassalSharp.tools.menu.MenuProxy;
namespace VassalSharp.launch
{
	
	/// <summary> EditorWindow is the base class for the three top-level component
	/// editors :- ModuleEditorWindow, ExtensionEditorWindow, PluginEditorWindow
	/// 
	/// </summary>
	/// <author>  Brent Easton
	/// </author>
	[Serializable]
	public abstract class EditorWindow:System.Windows.Forms.Form
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(EditorWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(EditorWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private EditorWindow enclosingInstance;
			public EditorWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs e)
			{
				e.Cancel = true;
				Enclosing_Instance.close();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassSaveAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassSaveAction:SaveAction
		{
			public AnonymousClassSaveAction(EditorWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(EditorWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private EditorWindow enclosingInstance;
			public EditorWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.save();
				Enclosing_Instance.treeStateChanged(false);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassSaveAsAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassSaveAsAction:SaveAsAction
		{
			public AnonymousClassSaveAsAction(EditorWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(EditorWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private EditorWindow enclosingInstance;
			public EditorWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.saveAs();
				Enclosing_Instance.treeStateChanged(false);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction:SupportClass.ActionSupport
		{
			private void  InitBlock(EditorWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private EditorWindow enclosingInstance;
			public EditorWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction(EditorWindow enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				new ModuleUpdaterDialog(Enclosing_Instance).Visible = true;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassCallBack' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassCallBack : ValidationReportDialog.CallBack
		{
			public AnonymousClassCallBack(IThreadRunnable save, EditorWindow enclosingInstance)
			{
				InitBlock(save, enclosingInstance);
			}
			private void  InitBlock(IThreadRunnable save, EditorWindow enclosingInstance)
			{
				this.save = save;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable save was copied into class AnonymousClassCallBack. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private IThreadRunnable save;
			private EditorWindow enclosingInstance;
			public EditorWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  ok()
			{
				save.Run();
			}
			
			public virtual void  cancel()
			{
			}
		}
		private void  InitBlock()
		{
			helpWindow = new HelpWindow(Resources.getString("Editor.ModuleEditor.reference_manual"), null);
			System.Windows.Forms.ToolBar temp_ToolBar;
			System.Windows.Forms.ImageList temp_ImageList;
			temp_ImageList = new System.Windows.Forms.ImageList();
			temp_ToolBar = new System.Windows.Forms.ToolBar();
			temp_ToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(SupportClass.ToolBarButtonClicked);
			temp_ToolBar.ImageList = temp_ImageList;
			toolBar = temp_ToolBar;
		}
		public abstract System.String EditorType{get;}
		private const long serialVersionUID = 1L;
		
		protected internal SaveAction saveAction;
		protected internal SaveAsAction saveAsAction;
		protected internal System.Windows.Forms.MenuItem componentHelpItem;
		protected internal SupportClass.ActionSupport createUpdater;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'helpWindow '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'helpWindow' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal HelpWindow helpWindow;
		
		protected internal ConfigureTree tree;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'toolBar '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'toolBar' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal System.Windows.Forms.ToolBar toolBar;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'scrollPane '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		protected internal System.Windows.Forms.ScrollableControl scrollPane;
		
		protected internal EditorWindow()
		{
			InitBlock();
			Text = "VASSAL " + EditorType + " Editor";
			//UPGRADE_ISSUE: Method 'javax.swing.JFrame.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJFramesetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			/*
			setLayout(new BorderLayout());*/
			
			ApplicationIcons.setFor(this);
			
			Closing += new System.ComponentModel.CancelEventHandler(this.EditorWindow_Closing_DO_NOTHING_ON_CLOSE);
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
			
			//UPGRADE_ISSUE: Method 'javax.swing.JToolBar.setFloatable' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJToolBarsetFloatable_boolean'"
			toolBar.setFloatable(false);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(toolBar);
			toolBar.Dock = System.Windows.Forms.DockStyle.Top;
			toolBar.SendToBack();
			
			// setup menubar and actions
			//UPGRADE_NOTE: Final was removed from the declaration of 'mm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuManager mm = MenuManager.Instance;
			//UPGRADE_NOTE: Final was removed from the declaration of 'mb '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuBarProxy mb = mm.getMenuBarProxyFor(this);
			
			// file menu
			if (SystemUtils.IS_OS_MAC_OSX)
			{
				mm.addToSection("Editor.File", mm.addKey("Editor.save"));
				mm.addToSection("Editor.File", mm.addKey("Editor.save_as"));
			}
			else
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fileMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				MenuProxy fileMenu = new MenuProxy(Resources.getString("General.file"));
				
				// FIMXE: setting nmemonic from first letter could cause collisions in
				// some languages
				fileMenu.setMnemonic(Resources.getString("General.file.shortcut")[0]);
				
				fileMenu.add(mm.addKey("Editor.save"));
				fileMenu.add(mm.addKey("Editor.save_as"));
				fileMenu.addSeparator();
				fileMenu.add(mm.addKey("General.quit"));
				mb.add(fileMenu);
			}
			
			// edit menu
			//UPGRADE_NOTE: Final was removed from the declaration of 'editMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuProxy editMenu = new MenuProxy(Resources.getString("General.edit"));
			editMenu.setMnemonic(Resources.getString("General.edit.shortcut")[0]);
			
			editMenu.add(mm.addKey("Editor.cut"));
			editMenu.add(mm.addKey("Editor.copy"));
			editMenu.add(mm.addKey("Editor.paste"));
			editMenu.add(mm.addKey("Editor.move"));
			editMenu.addSeparator();
			editMenu.add(mm.addKey("Editor.ModuleEditor.properties"));
			editMenu.add(mm.addKey("Editor.ModuleEditor.translate"));
			
			// tools menu
			//UPGRADE_NOTE: Final was removed from the declaration of 'toolsMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuProxy toolsMenu = new MenuProxy(Resources.getString("General.tools"));
			toolsMenu.setMnemonic(Resources.getString("General.tools.shortcut")[0]);
			
			toolsMenu.add(mm.addKey("create_module_updater"));
			toolsMenu.add(mm.addKey("Editor.ModuleEditor.update_saved"));
			
			if (SystemUtils.IS_OS_MAC_OSX)
			{
				mm.addToSection("Editor.MenuBar", editMenu);
				mm.addToSection("Editor.MenuBar", toolsMenu);
			}
			else
			{
				mb.add(editMenu);
				mb.add(toolsMenu);
			}
			
			// help menu
			if (SystemUtils.IS_OS_MAC_OSX)
			{
				mm.addToSection("Documentation.VASSAL", mm.addKey("Editor.ModuleEditor.reference_manual"));
			}
			else
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'helpMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				MenuProxy helpMenu = new MenuProxy(Resources.getString("General.help"));
				
				// FIMXE: setting nmemonic from first letter could cause collisions in
				// some languages
				helpMenu.setMnemonic(Resources.getString("General.help.shortcut")[0]);
				
				helpMenu.add(mm.addKey("General.help"));
				helpMenu.add(mm.addKey("Help.user_guide"));
				helpMenu.add(mm.addKey("Editor.ModuleEditor.reference_manual"));
				helpMenu.addSeparator();
				helpMenu.add(mm.addKey("AboutScreen.about_vassal"));
				mb.add(helpMenu);
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'mask '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getMenuShortcutKeyMask' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
			//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
			int mask = Toolkit.getDefaultToolkit().getMenuShortcutKeyMask();
			
			saveAction = new AnonymousClassSaveAction(this);
			
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
			saveAction.setEnabled(false);
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
			//UPGRADE_ISSUE: Field 'javax.swing.Action.ACCELERATOR_KEY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionACCELERATOR_KEY_f'"
			saveAction.putValue(Action.ACCELERATOR_KEY, new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.S | mask)));
			mm.addAction("Editor.save", saveAction);
			System.Windows.Forms.ToolBarButton temp_ToolBarButton;
			System.Windows.Forms.Button temp_Button;
			temp_Button = SupportClass.ButtonSupport.CreateStandardButton(saveAction.Description, saveAction.Icon);
			temp_Button.Click += new System.EventHandler(saveAction.actionPerformed);
			temp_ToolBarButton = new System.Windows.Forms.ToolBarButton(temp_Button.Text);
			toolBar.Buttons.Add(temp_ToolBarButton);
			if (temp_Button.Image != null)
			{
				toolBar.ImageList.Images.Add(temp_Button.Image);
				temp_ToolBarButton.ImageIndex = toolBar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton.Tag = temp_Button;
			temp_Button.Tag = temp_ToolBarButton;
			
			saveAsAction = new AnonymousClassSaveAsAction(this);
			
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
			saveAsAction.setEnabled(false);
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
			//UPGRADE_ISSUE: Field 'javax.swing.Action.ACCELERATOR_KEY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionACCELERATOR_KEY_f'"
			saveAsAction.putValue(Action.ACCELERATOR_KEY, new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.A | mask)));
			mm.addAction("Editor.save_as", saveAsAction);
			System.Windows.Forms.ToolBarButton temp_ToolBarButton2;
			System.Windows.Forms.Button temp_Button2;
			temp_Button2 = SupportClass.ButtonSupport.CreateStandardButton(saveAsAction.Description, saveAsAction.Icon);
			temp_Button2.Click += new System.EventHandler(saveAsAction.actionPerformed);
			temp_ToolBarButton2 = new System.Windows.Forms.ToolBarButton(temp_Button2.Text);
			toolBar.Buttons.Add(temp_ToolBarButton2);
			if (temp_Button2.Image != null)
			{
				toolBar.ImageList.Images.Add(temp_Button2.Image);
				temp_ToolBarButton2.ImageIndex = toolBar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton2.Tag = temp_Button2;
			temp_Button2.Tag = temp_ToolBarButton2;
			
			mm.addAction("General.quit", new ShutDownAction());
			// FXIME: mnemonics should be language-dependant
			//    mm.getAction("General.quit").setMnemonic('Q');
			
			createUpdater = new AnonymousClassAbstractAction(this, "Create " + EditorType + " updater");
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			createUpdater.setEnabled(false);
			mm.addAction("create_module_updater", createUpdater);
			
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'url '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Uri url = new System.IO.FileInfo(Documentation.DocumentationBaseDir.FullName + "\\" + "README.html").toURI().toURL();
				mm.addAction("General.help", new ShowHelpAction(url, null));
			}
			catch (System.UriFormatException e)
			{
				ErrorDialog.bug(e);
			}
			
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'url '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Uri url = new System.IO.FileInfo(Documentation.DocumentationBaseDir.FullName + "\\" + "userguide/userguide.pdf").toURI().toURL();
				mm.addAction("Help.user_guide", new ShowHelpAction("Help.user_guide", url, null));
			}
			catch (System.UriFormatException e)
			{
				ErrorDialog.bug(e);
			}
			
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'url '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Uri url = new System.IO.FileInfo(Documentation.DocumentationBaseDir.FullName + "\\" + "ReferenceManual/index.htm").toURI().toURL();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'helpAction '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				helpWindow.GetType();
				//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
				ShowHelpAction helpAction = new ShowHelpAction(url, new System.Uri(System.IO.Path.GetFullPath("/images/Help16.gif")));
				
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.SHORT_DESCRIPTION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionSHORT_DESCRIPTION_f'"
				helpAction.putValue(Action.SHORT_DESCRIPTION, Resources.getString("Editor.ModuleEditor.reference_manual")); //$NON-NLS-1$
				System.Windows.Forms.ToolBarButton temp_ToolBarButton3;
				System.Windows.Forms.Button temp_Button3;
				temp_Button3 = SupportClass.ButtonSupport.CreateStandardButton(helpAction.Description, helpAction.Icon);
				temp_Button3.Click += new System.EventHandler(helpAction.actionPerformed);
				temp_ToolBarButton3 = new System.Windows.Forms.ToolBarButton(temp_Button3.Text);
				toolBar.Buttons.Add(temp_ToolBarButton3);
				if (temp_Button3.Image != null)
				{
					toolBar.ImageList.Images.Add(temp_Button3.Image);
					temp_ToolBarButton3.ImageIndex = toolBar.ImageList.Images.Count - 1;
				}
				temp_ToolBarButton3.Tag = temp_Button3;
				temp_Button3.Tag = temp_ToolBarButton3;
			}
			catch (System.UriFormatException e)
			{
				ErrorDialog.bug(e);
			}
			
			mm.addAction("AboutScreen.about_vassal", new AboutVASSALAction(this));
			
			Menu = mm.getMenuBarFor(this);
			
			// the presence of the panel prevents a NullPointerException on packing
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			panel.Size = new System.Drawing.Size(250, 400);
			
			//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.HORIZONTAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsHORIZONTAL_SCROLLBAR_AS_NEEDED_f'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
			//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.VERTICAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsVERTICAL_SCROLLBAR_AS_NEEDED_f'"
			temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol.AutoScroll = true;
			temp_scrollablecontrol.Controls.Add(panel);
			scrollPane = temp_scrollablecontrol;
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(scrollPane);
			scrollPane.Dock = System.Windows.Forms.DockStyle.Fill;
			scrollPane.BringToFront();
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
		}
		
		protected internal virtual MenuProxy findMenuProxy(System.String name, MenuBarProxy mb)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(ChildProxy < ? > c: mb.getChildren())
			{
				if (c is MenuProxy)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					MenuProxy m = (MenuProxy) c;
					if (name.Equals(m.Text))
						return m;
				}
			}
			return null;
		}
		
		/*
		* Each component must Save, SaveAs and close itself
		*/
		protected internal abstract void  save();
		protected internal abstract void  saveAs();
		
		protected internal virtual void  close()
		{
			GameModule.getGameModule().quit();
		}
		
		protected internal virtual void  saver(IThreadRunnable save)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'report '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ValidationReport report = new ValidationReport();
			GameModule.getGameModule().validate(GameModule.getGameModule(), report);
			if (report.getWarnings().size() == 0)
			{
				save.Run();
			}
			else
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				new ValidationReportDialog(report, new AnonymousClassCallBack(save, this)).Visible = true;
			}
		}
		
		/// <summary> Called by the enclosed ConfigureTree or ExtensionTree when it's dirty
		/// state is changed. The implementing class should override this if they
		/// need to take action like changing menu availability.
		/// 
		/// </summary>
		/// <param name="changed">true if the tree is in a changed (dirty) state
		/// </param>
		public virtual void  treeStateChanged(bool changed)
		{
		}
		private void  EditorWindow_Closing_DO_NOTHING_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 0);
		}
	}
}