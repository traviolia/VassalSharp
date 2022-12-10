/*
* $Id$
*
* Copyright (c) 2000-2013 by Rodney Kinney, Joel Uckelman, Brent Easton
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
//UPGRADE_TODO: The type 'org.apache.commons.lang.SystemUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SystemUtils = org.apache.commons.lang.SystemUtils;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using Info = VassalSharp.Info;
using GameModule = VassalSharp.build.GameModule;
using AbstractMetaData = VassalSharp.build.module.metadata.AbstractMetaData;
using ImportMetaData = VassalSharp.build.module.metadata.ImportMetaData;
using MetaDataFactory = VassalSharp.build.module.metadata.MetaDataFactory;
using ModuleMetaData = VassalSharp.build.module.metadata.ModuleMetaData;
using Resources = VassalSharp.i18n.Resources;
using DataArchive = VassalSharp.tools.DataArchive;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ThrowableUtils = VassalSharp.tools.ThrowableUtils;
using WarningDialog = VassalSharp.tools.WarningDialog;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
using ImportAction = VassalSharp.tools.imports.ImportAction;
using IPCMessage = VassalSharp.tools.ipc.IPCMessage;
using MacOSXMenuManager = VassalSharp.tools.menu.MacOSXMenuManager;
using MenuBarProxy = VassalSharp.tools.menu.MenuBarProxy;
using MenuManager = VassalSharp.tools.menu.MenuManager;
namespace VassalSharp.launch
{
	
	public class Editor:Launcher
	{
		[STAThread]
		public static void  Main(System.String[] args)
		{
			new Editor(args);
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.launch.Editor'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		protected internal Editor(System.String[] args):base(args)
		{
		}
		
		protected internal override MenuManager createMenuManager()
		{
			return SystemUtils.IS_OS_MAC_OSX?new MacOSXMenuManager():new EditorMenuManager();
		}
		
		protected internal override void  launch()
		{
			IPCMessage msg = null;
			
			switch (lr.mode)
			{
				
				case EDIT: 
					new EditModuleAction(lr.module).loadModule(lr.module);
					msg = new AbstractLaunchAction.NotifyOpenModuleOk(lr);
					break;
				
				case IMPORT: 
					new ImportAction(null).loadModule(lr.importFile);
					msg = new AbstractLaunchAction.NotifyImportModuleOk(lr);
					break;
				
				case NEW: 
					new CreateModuleAction(null).performAction(null);
					msg = new AbstractLaunchAction.NotifyNewModuleOk(lr);
					break;
				
				case EDIT_EXT: 
					GameModule.init(new BasicModule(new DataArchive(lr.module.getPath())));
					GameModule.getGameModule().getFrame().setVisible(true);
					new EditExtensionAction(lr.extension).performAction(null);
					msg = new AbstractLaunchAction.NotifyOpenModuleOk(lr);
					break;
				
				case NEW_EXT: 
					GameModule.init(new BasicModule(new DataArchive(lr.module.getPath())));
					//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Windows.Forms.Form f = GameModule.getGameModule().getFrame();
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					f.Visible = true;
					new NewExtensionAction(f).performAction(null);
					msg = new AbstractLaunchAction.NotifyOpenModuleOk(lr);
					break;
				}
			
			if (ipc != null)
			{
				try
				{
					ipc.send(msg);
				}
				catch (System.IO.IOException e)
				{
					// This is not fatal, since we've successfully opened the module,
					// but possibly this means that the Module Manager has died.
					ErrorDialog.showDetails(e, ThrowableUtils.getStackTrace(e), "Error.socket_error");
				}
			}
		}
		
		[Serializable]
		public class NewModuleLaunchAction:AbstractLaunchAction
		{
			private const long serialVersionUID = 1L;
			
			public NewModuleLaunchAction(ModuleManagerWindow mm):base(Resources.getString("Main.new_module"), mm, typeof(Editor).FullName, new LaunchRequest(LaunchRequest.Mode.NEW))
			{
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			protected internal override LaunchTask getLaunchTask()
			{
				return new LaunchTask(this);
			}
		}
		
		[Serializable]
		public class ImportLaunchAction:AbstractLaunchAction
		{
			private const long serialVersionUID = 1L;
			
			public ImportLaunchAction(ModuleManagerWindow mm, System.IO.FileInfo module):base(Resources.getString("Main.import_module"), mm, typeof(Editor).FullName, new LaunchRequest(LaunchRequest.Mode.IMPORT, module))
			{
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			protected internal override LaunchTask getLaunchTask()
			{
				return new LaunchTask(this);
			}
		}
		
		[Serializable]
		public class PromptImportLaunchAction:ImportLaunchAction
		{
			private const long serialVersionUID = 1L;
			
			public PromptImportLaunchAction(ModuleManagerWindow mm):base(mm, null)
			{
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				// prompt the user to pick a module
				if (promptForFile() == null)
					return ;
				
				base.actionPerformed(event_sender, e);
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			protected internal override System.IO.FileInfo promptForFile()
			{
				// prompt the use to pick a module
				//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				FileChooser fc = ImportAction.getFileChooser(window);
				
				if (fc.showOpenDialog() == FileChooser.APPROVE_OPTION)
				{
					lr.importFile = fc.SelectedFile;
					
					if (lr.importFile != null)
					{
						if (lr.importFile.exists())
						{
							//UPGRADE_NOTE: Final was removed from the declaration of 'metadata '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							AbstractMetaData metadata = MetaDataFactory.buildMetaData(lr.importFile);
							if (metadata == null || !(metadata is ImportMetaData))
							{
								ErrorDialog.show("Error.invalid_import_file", lr.importFile.getAbsolutePath());
								logger.error("Import of " + lr.importFile.getAbsolutePath() + " failed: unrecognized import type");
								lr.importFile = null;
							}
						}
						else
						{
							lr.importFile = null;
						}
					}
				}
				
				return lr.importFile;
			}
		}
		
		[Serializable]
		public class LaunchAction:AbstractLaunchAction
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassLaunchTask' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassLaunchTask:LaunchTask
			{
				public AnonymousClassLaunchTask(LaunchAction enclosingInstance):base(enclosingInstance.Enclosing_Instance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(LaunchAction enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private LaunchAction enclosingInstance;
				public new LaunchAction Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				Override
				protected internal override void  done()
				{
					base.done();
					
					// register that this module is no longer being edited
					editing.remove(lr.module);
					//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
					Enclosing_Instance.setEnabled(true);
				}
			}
			private const long serialVersionUID = 1L;
			
			public LaunchAction(ModuleManagerWindow mm, System.IO.FileInfo module):base(Resources.getString("Main.edit_module_specific"), mm, typeof(Editor).FullName, new LaunchRequest(LaunchRequest.Mode.EDIT, module))
			{
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
				setEnabled(!editing.contains(module) && !using_Renamed.containsKey(module));
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				AbstractMetaData data = MetaDataFactory.buildMetaData(lr.module);
				if (data != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'vv '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String vv = data.VassalVersion;
					
					// don't permit editing of modules significantly newer than us
					if (Info.isModuleTooNew(vv))
					{
						ErrorDialog.show("Error.module_too_new", lr.module.getPath(), vv, Info.Version);
						return ;
					}
					
					// don't permit loading of VASL saved with 3.1 or earlier
					if (data is ModuleMetaData)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'md '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						ModuleMetaData md = (ModuleMetaData) data;
						if (Info.compareVersions(md.VassalVersion, "3.2.0") < 0)
						{
							if ("VASL".Equals(md.Name))
							{
								ErrorDialog.show("Error.VASL_too_old", Info.Version);
								return ;
							}
							else if ("VSQL".Equals(md.Name))
							{
								ErrorDialog.show("Error.VSQL_too_old", Info.Version);
								return ;
							}
						}
					}
					
					// warn user if editing this module would update it to our version
					if (Info.hasOldFormat(vv))
					{
						WarningDialog.show("Warning.module_will_be_updated", lr.module.getPath(), Info.Version, "3.2");
					}
				}
				
				// register that this module is being edited
				if (editing.contains(lr.module) || using_Renamed.containsKey(lr.module))
					return ;
				editing.add(lr.module);
				
				base.actionPerformed(event_sender, e);
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			protected internal override LaunchTask getLaunchTask()
			{
				return new AnonymousClassLaunchTask(this);
			}
		}
		
		[Serializable]
		public class ListLaunchAction:LaunchAction
		{
			private const long serialVersionUID = 1L;
			
			public ListLaunchAction(ModuleManagerWindow mm, System.IO.FileInfo module):base(mm, module)
			{
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				base.actionPerformed(event_sender, e);
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
				setEnabled(false);
			}
		}
		
		[Serializable]
		public class PromptLaunchAction:LaunchAction
		{
			private const long serialVersionUID = 1L;
			
			public PromptLaunchAction(ModuleManagerWindow mm):base(mm, null)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
				putValue(Action.NAME, Resources.getString("Main.edit_module"));
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				// prompt the user to pick a module
				if (promptForFile() == null)
					return ;
				
				base.actionPerformed(event_sender, e);
			}
		}
		
		private class EditorMenuManager:MenuManager
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'editorBar '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private MenuBarProxy editorBar = new MenuBarProxy();
			//UPGRADE_NOTE: Final was removed from the declaration of 'playerBar '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private MenuBarProxy playerBar = new MenuBarProxy();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public override System.Windows.Forms.MainMenu getMenuBarFor(System.Windows.Forms.Form fc)
			{
				if (fc is PlayerWindow)
					return playerBar.createPeer();
				else if (fc is EditorWindow)
					return editorBar.createPeer();
				else
					return null;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public override MenuBarProxy getMenuBarProxyFor(System.Windows.Forms.Form fc)
			{
				if (fc is PlayerWindow)
					return playerBar;
				else if (fc is EditorWindow)
					return editorBar;
				else
					return null;
			}
		}
		static Editor()
		{
			logger = LoggerFactory.getLogger(typeof(Editor));
		}
	}
}