/*
* $Id$
*
* Copyright (c) 2000-2007 by Rodney Kinney
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
using GameModule = VassalSharp.build.GameModule;
using ExtensionsLoader = VassalSharp.build.module.ExtensionsLoader;
using DirectoryConfigurer = VassalSharp.configure.DirectoryConfigurer;
using Localization = VassalSharp.i18n.Localization;
using Resources = VassalSharp.i18n.Resources;
using Prefs = VassalSharp.preferences.Prefs;
using DataArchive = VassalSharp.tools.DataArchive;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
namespace VassalSharp.launch
{
	
	/// <summary> Loads a module in play mode
	/// 
	/// </summary>
	/// <author>  rodneykinney
	/// </author>
	[Serializable]
	public class LoadModuleAction:GameModuleAction
	{
		private const long serialVersionUID = 1L;
		private System.IO.FileInfo moduleFile;
		protected internal FileChooser fc;
		
		public LoadModuleAction(System.Windows.Forms.Control comp):base(Resources.getString("Main.play_module"), comp)
		{
		}
		
		public LoadModuleAction(System.IO.FileInfo moduleFile):base(Resources.getString("Main.play_module"), null)
		{
			this.moduleFile = moduleFile;
		}
		
		public override void  performAction(System.Object event_sender, System.EventArgs e)
		{
			actionCancelled = true;
			System.IO.FileInfo target = moduleFile;
			if (target == null)
			{
				if (fc == null)
				{
					fc = FileChooser.createFileChooser(comp, (DirectoryConfigurer) Prefs.GlobalPrefs.getOption(Prefs.MODULES_DIR_KEY));
				}
				
				if (fc.showOpenDialog() == FileChooser.APPROVE_OPTION)
				{
					System.IO.FileInfo f = fc.SelectedFile;
					bool tmpBool;
					if (System.IO.File.Exists(f.FullName))
						tmpBool = true;
					else
						tmpBool = System.IO.Directory.Exists(f.FullName);
					if (f != null && tmpBool)
					{
						target = f;
					}
				}
				
				// bail out if still no target
				if (target == null)
					return ;
			}
			
			loadModule(target);
			actionCancelled = false;
		}
		
		protected internal virtual void  loadModule(System.IO.FileInfo f)
		{
			//    final JFrame frame = ModuleManagerWindow.getInstance();
			//    frame.setCursor(Cursor.getPredefinedCursor(Cursor.WAIT_CURSOR));
			//
			//    try {
			GameModule.init(new BasicModule(new DataArchive(f.FullName)));
			//      ModuleManagerWindow.getInstance().addModule(f);
			Localization.Instance.translate();
			new ExtensionsLoader().addTo(GameModule.getGameModule());
			GameModule.getGameModule().getWizardSupport().showWelcomeWizard();
			
			//      frame.setVisible(false);
			//    }
			//    finally {
			//      frame.setCursor(Cursor.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
			//    }
		}
	}
}