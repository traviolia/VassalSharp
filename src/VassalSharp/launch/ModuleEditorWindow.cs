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
using ConfigureTree = VassalSharp.configure.ConfigureTree;
using SavedGameUpdaterDialog = VassalSharp.configure.SavedGameUpdaterDialog;
using Resources = VassalSharp.i18n.Resources;
using MenuManager = VassalSharp.tools.menu.MenuManager;
namespace VassalSharp.launch
{
	
	[Serializable]
	public class ModuleEditorWindow:EditorWindow
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction1:SupportClass.ActionSupport
		{
			private void  InitBlock(ModuleEditorWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ModuleEditorWindow enclosingInstance;
			public ModuleEditorWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction1(ModuleEditorWindow enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			} //$NON-NLS-1$
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				new SavedGameUpdaterDialog(Enclosing_Instance).Visible = true;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(ModuleEditorWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ModuleEditorWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ModuleEditorWindow enclosingInstance;
			public ModuleEditorWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				GameModule.getGameModule().save();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable1 : IThreadRunnable
		{
			public AnonymousClassRunnable1(ModuleEditorWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ModuleEditorWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ModuleEditorWindow enclosingInstance;
			public ModuleEditorWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				GameModule.getGameModule().saveAs();
			}
		}
		override public System.String EditorType
		{
			get
			{
				return "Module";
			}
			
		}
		private const long serialVersionUID = 1L;
		
		protected internal SupportClass.ActionSupport updateSavedGame;
		
		public ModuleEditorWindow(GameModule mod):base()
		{
			tree = new ConfigureTree(mod, helpWindow, this);
			treeStateChanged(false);
			scrollPane.Controls.Add(tree);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'mm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuManager mm = MenuManager.Instance;
			
			tree.populateEditMenu(this);
			
			mm.addAction("Editor.ModuleEditor.reference_manual", tree.HelpAction);
			
			updateSavedGame = new AnonymousClassAbstractAction1(this, Resources.getString("Editor.ModuleEditor.update_saved"));
			mm.addAction("Editor.ModuleEditor.update_saved", updateSavedGame);
			
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
			saveAction.setEnabled(true);
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
			saveAsAction.setEnabled(true);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			createUpdater.setEnabled(true);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			updateSavedGame.setEnabled(true);
			
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
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
		
		updateSavedGame = menu.add(Resources
		.getString("Editor.ModuleEditor.update_saved")); //$NON-NLS-1$
		updateSavedGame.setEnabled(false);
		updateSavedGame.addActionListener(new ActionListener() {
		public void actionPerformed(ActionEvent e) {
		new SavedGameUpdaterDialog(ModuleEditorWindow.this).setVisible(true);
		}
		});
		
		addMenuItem(MenuKey.UPDATE_SAVED, updateSavedGame);
		
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