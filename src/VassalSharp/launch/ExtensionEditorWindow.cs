/*
* Copyright (c) 2000-2008 by Rodney Kinney, Joel Uckelman, Brent Easton
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
using GameModule = VassalSharp.build.GameModule;
using ModuleExtension = VassalSharp.build.module.ModuleExtension;
using ExtensionTree = VassalSharp.configure.ExtensionTree;
using WriteErrorDialog = VassalSharp.tools.WriteErrorDialog;
using MenuManager = VassalSharp.tools.menu.MenuManager;
namespace VassalSharp.launch
{
	
	[Serializable]
	public class ExtensionEditorWindow:EditorWindow
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(ExtensionEditorWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ExtensionEditorWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ExtensionEditorWindow enclosingInstance;
			public ExtensionEditorWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				try
				{
					Enclosing_Instance.extension.save();
				}
				catch (System.IO.IOException e)
				{
					WriteErrorDialog.error(e, Enclosing_Instance.extension.DataArchive.Name);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable1 : IThreadRunnable
		{
			public AnonymousClassRunnable1(ExtensionEditorWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ExtensionEditorWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ExtensionEditorWindow enclosingInstance;
			public ExtensionEditorWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				try
				{
					Enclosing_Instance.extension.saveAs();
				}
				catch (System.IO.IOException e)
				{
					WriteErrorDialog.error(e, Enclosing_Instance.extension.DataArchive.Name);
				}
			}
		}
		override public System.String EditorType
		{
			get
			{
				return "Extension";
			}
			
		}
		
		private const long serialVersionUID = 1L;
		protected internal static ExtensionEditorWindow instance = null;
		protected internal ModuleExtension extension;
		
		public ExtensionEditorWindow(GameModule mod, ModuleExtension ext):base()
		{
			
			extension = ext;
			tree = new ExtensionTree(mod, helpWindow, ext, this);
			treeStateChanged(false);
			scrollPane.Controls.Add(tree);
			
			tree.populateEditMenu(this);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'mm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuManager mm = MenuManager.Instance;
			mm.addAction("Editor.ModuleEditor.reference_manual", tree.HelpAction);
			
			System.Windows.Forms.ToolBarButton separator;
			separator = new System.Windows.Forms.ToolBarButton();
			separator.Style = System.Windows.Forms.ToolBarButtonStyle.Separator;
			toolBar.Buttons.Add(separator);
			System.Windows.Forms.ToolBarButton temp_ToolBarButton;
			SupportClass.ActionSupport temp_Action;
			temp_Action = extension.getEditAction(SupportClass.DialogSupport.CreateDialog(this));
			System.Windows.Forms.Button temp_Button;
			temp_Button = SupportClass.ButtonSupport.CreateStandardButton(temp_Action.Description, temp_Action.Icon);
			temp_Button.Click += new System.EventHandler(temp_Action.actionPerformed);
			temp_ToolBarButton = new System.Windows.Forms.ToolBarButton(temp_Button.Text);
			toolBar.Buttons.Add(temp_ToolBarButton);
			if (temp_Button.Image != null)
			{
				toolBar.ImageList.Images.Add(temp_Button.Image);
				temp_ToolBarButton.ImageIndex = toolBar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton.Tag = temp_Button;
			temp_Button.Tag = temp_ToolBarButton;
			
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
			saveAction.setEnabled(true);
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
			saveAsAction.setEnabled(true);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			createUpdater.setEnabled(true);
			
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
		}
		
		public virtual void  moduleLoading(GameModule mod)
		{
			
		}
		
		/*
		protected void populateFileMenu(JMenu menu) {
		addSaveMenuItem(menu);
		addSaveAsMenuItem(menu);
		menu.addSeparator();
		addQuitMenuItem(menu);
		}
		
		protected void populateToolsMenu(JMenu menu) {
		addUpdaterMenuItem(menu);
		menu.addSeparator();
		addTranslateMenuItem(menu);
		}*/
		
		protected internal override void  save()
		{
			this.saver(new AnonymousClassRunnable(this));
		}
		
		protected internal override void  saveAs()
		{
			this.saver(new AnonymousClassRunnable1(this));
		}
	}
}