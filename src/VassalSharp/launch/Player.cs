/*
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

using Info = VassalSharp.Info;
using GameModule = VassalSharp.build.GameModule;
using ExtensionsLoader = VassalSharp.build.module.ExtensionsLoader;
using ModuleExtension = VassalSharp.build.module.ModuleExtension;
using AbstractMetaData = VassalSharp.build.module.metadata.AbstractMetaData;
using MetaDataFactory = VassalSharp.build.module.metadata.MetaDataFactory;
using ModuleMetaData = VassalSharp.build.module.metadata.ModuleMetaData;
using Localization = VassalSharp.i18n.Localization;
using Resources = VassalSharp.i18n.Resources;
using DataArchive = VassalSharp.tools.DataArchive;
using JarArchive = VassalSharp.tools.JarArchive;
//using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ThrowableUtils = VassalSharp.tools.ThrowableUtils;
using VassalSharp.tools.menu;

namespace VassalSharp.launch
{
	
	/// <author>Joel Uckelman</author>
	/// <since>3.1.0</since>
	public class Player : Launcher
	{
		[STAThread]
		public static void  Main(string[] args)
		{
			new Player(args);
		}
		
		protected internal Player(string[] args) : base(args)
		{
		}
		
		protected internal override MenuManager createMenuManager()
		{
			return new PlayerMenuManager();
		}
		
		protected internal override void  launch()
		{
			if (lr.builtInModule)
			{
				GameModule.init(createModule(createDataArchive()));
				
				if (lr.autoext != null)
				{
		
					foreach (String ext in lr.autoext)
					{
						createExtension(ext).build();
					}
				}
				
				createExtensionsLoader().addTo(GameModule.getGameModule());
				Localization.Instance.translate();
				GameModule.getGameModule().WizardSupport.showWelcomeWizard();
			}
			else
			{
				GameModule.init(createModule(createDataArchive()));
				createExtensionsLoader().addTo(GameModule.getGameModule());
				Localization.Instance.translate();
				GameModule m = GameModule.getGameModule();
				if (lr.game != null)
				{
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					m.Frame.Visible = true;
					m.getGameState().loadGameInBackground(lr.game);
				}
				else
				{
					m.WizardSupport.showWelcomeWizard();
				}
			}
			
			if (ipc != null)
			{
				try
				{
					ipc.send(new AbstractLaunchAction.NotifyOpenModuleOk(lr));
				}
				catch (System.IO.IOException e)
				{
					// This is not fatal, since we've successfully opened the module,
					// but possibly this means that the Module Manager has died.
					//ErrorDialog.showDetails(e, ThrowableUtils.getStackTrace(e), "Error.socket_error");
				}
			}
		}
		
		protected internal virtual ExtensionsLoader createExtensionsLoader()
		{
			return new ExtensionsLoader();
		}
		
		protected internal virtual ModuleExtension createExtension(string name)
		{
			return new ModuleExtension(new JarArchive(name));
		}
		
		protected internal virtual DataArchive createDataArchive()
		{
			if (lr.builtInModule)
			{
				return new JarArchive();
			}
			else
			{
				return new DataArchive(lr.module.FullName);
			}
		}
		
		protected internal virtual GameModule createModule(DataArchive archive)
		{
			return new BasicModule(archive);
		}
		
		[Serializable]
		public class LaunchAction : AbstractLaunchAction
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassLaunchTask' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassLaunchTask : LaunchTask
			{
				public AnonymousClassLaunchTask(LaunchAction enclosingInstance) : base(enclosingInstance.Enclosing_Instance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(LaunchAction enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private LaunchAction enclosingInstance;
				public LaunchAction Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}

				protected internal override void  done()
				{
					base.done();
					
					// reduce the using count
					System.Int32 count = using_Renamed.get_Renamed(lr.module);
					if ((System.Object) count == 1)
						using_Renamed.remove(lr.module);
					else
						using_Renamed.put(lr.module, --count);
				}
			}
			private const long serialVersionUID = 1L;
			
			public LaunchAction(ModuleManagerWindow mm, System.IO.FileInfo module):base(Resources.getString("Main.play_module_specific"), mm, typeof(Player).FullName, new LaunchRequest(LaunchRequest.Mode.LOAD, module))
			{
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
				setEnabled(!editing.contains(module));
			}
			
			public LaunchAction(ModuleManagerWindow mm, System.IO.FileInfo module, System.IO.FileInfo saveGame):base(Resources.getString("General.open"), mm, typeof(Player).FullName, new LaunchRequest(LaunchRequest.Mode.LOAD, module, saveGame))
			{
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
				setEnabled(!editing.contains(module));
			}

			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				// don't permit loading of VASL saved with 3.1 or earlier
				//UPGRADE_NOTE: Final was removed from the declaration of 'data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				AbstractMetaData data = MetaDataFactory.buildMetaData(lr.module);
				if (data != null && data is ModuleMetaData)
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
				
				// register that this module is being used
				if (editing.contains(lr.module))
					return ;
				System.Int32 count = using_Renamed.get_Renamed(lr.module);
				//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
				using_Renamed.put(lr.module, count == null?1:++count);
				
				base.actionPerformed(event_sender, e);
			}

			protected internal override LaunchTask getLaunchTask()
			{
				return new AnonymousClassLaunchTask(this);
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
				putValue(Action.NAME, Resources.getString("Main.play_module"));
			}

			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				// prompt the user to pick a module
				if (promptForFile() == null)
					return ;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				AbstractMetaData data = MetaDataFactory.buildMetaData(lr.module);
				if (data != null && Info.isModuleTooNew(data.VassalVersion))
				{
					ErrorDialog.show("Error.module_too_new", lr.module.getPath(), data.VassalVersion, Info.Version);
					return ;
				}
				
				base.actionPerformed(event_sender, e);
			}
		}
		
		private class PlayerMenuManager : MenuManager
		{
			private MenuBarProxy menuBar = new MenuBarProxy();

			public override System.Windows.Forms.MenuStrip getMenuBarFor(System.Windows.Forms.Form fc)
			{
				return (fc is PlayerWindow) ? menuBar.createPeer():null;
			}

			public override MenuBarProxy getMenuBarProxyFor(System.Windows.Forms.Form fc)
			{
				return (fc is PlayerWindow)?menuBar:null;
			}
		}
	}
}