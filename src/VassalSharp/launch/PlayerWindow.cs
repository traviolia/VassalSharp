/*
* $Id$
*
* Copyright (c) 2000-2008 by Rodney Kinney, Joel Uckelman
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
//using SystemUtils = org.apache.commons.lang.SystemUtils;
//using Documentation = VassalSharp.build.module.Documentation;
//using ShowHelpAction = VassalSharp.configure.ShowHelpAction;
using Resources = VassalSharp.i18n.Resources;
//using ApplicationIcons = VassalSharp.tools.ApplicationIcons;
//using ErrorDialog = VassalSharp.tools.ErrorDialog;
//using WrapLayout = VassalSharp.tools.WrapLayout;
using MenuBarProxy = VassalSharp.tools.menu.MenuBarProxy;
using MenuManager = VassalSharp.tools.menu.MenuManager;
using MenuProxy = VassalSharp.tools.menu.MenuProxy;
namespace VassalSharp.launch
{
	
	[Serializable]
	public class PlayerWindow:System.Windows.Forms.Form
	{
		private void  InitBlock()
		{
			System.Windows.Forms.ToolBar temp_ToolBar;
			System.Windows.Forms.ImageList temp_ImageList;
			temp_ImageList = new System.Windows.Forms.ImageList();
			temp_ToolBar = new System.Windows.Forms.ToolBar();
			temp_ToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(SupportClass.ToolBarButtonClicked);
			temp_ToolBar.ImageList = temp_ImageList;
			toolBar = temp_ToolBar;
		}
		virtual public System.Windows.Forms.ToolBar ToolBar
		{
			get
			{
				return toolBar;
			}
			
		}
		virtual public System.Windows.Forms.Panel ControlPanel
		{
			get
			{
				return controlPanel;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'toolBar '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'toolBar' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal System.Windows.Forms.ToolBar toolBar;
		//UPGRADE_NOTE: Final was removed from the declaration of 'controlPanel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.Windows.Forms.Panel controlPanel = new System.Windows.Forms.Panel();
		
		public PlayerWindow()
		{
			InitBlock();
			Text = "VASSAL";
			//UPGRADE_ISSUE: Method 'javax.swing.JFrame.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJFramesetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			/*
			setLayout(new BorderLayout());*/
			Closing += new System.ComponentModel.CancelEventHandler(this.PlayerWindow_Closing_EXIT_ON_CLOSE);
			
			ApplicationIcons.setFor(this);
			
			// setup menubar and actions
			//UPGRADE_NOTE: Final was removed from the declaration of 'mm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuManager mm = MenuManager.Instance;
			//UPGRADE_NOTE: Final was removed from the declaration of 'mb '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuBarProxy mb = mm.getMenuBarProxyFor(this);
			
			// file menu
			//UPGRADE_NOTE: Final was removed from the declaration of 'fileMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuProxy fileMenu = new MenuProxy(Resources.getString("General.file"));
			fileMenu.setMnemonic(Resources.getString("General.file.shortcut")[0]);
			
			fileMenu.add(mm.addMarker("PredefinedSetup.start"));
			fileMenu.add(mm.addMarker("PredefinedSetup.end"));
			
			fileMenu.add(mm.addKey("GameState.new_game"));
			fileMenu.add(mm.addKey("GameState.load_game"));
			fileMenu.add(mm.addKey("GameState.save_game"));
			fileMenu.add(mm.addKey("GameState.save_game_as"));
			fileMenu.add(mm.addKey("GameState.close_game"));
			fileMenu.addSeparator();
			fileMenu.add(mm.addKey("BasicLogger.begin_logfile"));
			fileMenu.add(mm.addKey("BasicLogger.end_logfile"));
			
			if (SystemUtils.IS_OS_MAC_OSX)
			{
				fileMenu.add(mm.addMarker("Editor.File.start"));
				fileMenu.add(mm.addMarker("Editor.File.end"));
			}
			else
			{
				fileMenu.addSeparator();
				fileMenu.add(mm.addKey("Prefs.edit_preferences"));
				fileMenu.addSeparator();
				fileMenu.add(mm.addKey("General.quit"));
			}
			
			mm.addAction("General.quit", new ShutDownAction());
			
			// help menu
			//UPGRADE_NOTE: Final was removed from the declaration of 'helpMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuProxy helpMenu = new MenuProxy(Resources.getString("General.help"));
			
			// FIMXE: setting mnemonic from first letter could cause collisions in
			// some languages
			helpMenu.setMnemonic(Resources.getString("General.help.shortcut")[0]);
			
			helpMenu.add(mm.addMarker("Documentation.VassalSharp.start"));
			helpMenu.add(mm.addKey("General.help"));
			helpMenu.add(mm.addKey("Help.user_guide"));
			helpMenu.add(mm.addMarker("Documentation.VassalSharp.end"));
			
			helpMenu.addSeparator();
			helpMenu.add(mm.addMarker("Documentation.Module.start"));
			helpMenu.add(mm.addMarker("Documentation.Module.end"));
			
			helpMenu.add(mm.addKey("Documentation.about_module"));
			
			if (!SystemUtils.IS_OS_MAC_OSX)
			{
				helpMenu.add(mm.addKey("AboutScreen.about_vassal"));
			}
			
			// Tools menu
			//UPGRADE_NOTE: Final was removed from the declaration of 'toolsMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuProxy toolsMenu = new MenuProxy(Resources.getString("General.tools"));
			toolsMenu.setMnemonic(Resources.getString("General.tools.shortcut")[0]);
			
			toolsMenu.add(mm.addKey("GameRefresher.refresh_counters"));
			
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
			
			mm.addAction("AboutScreen.about_vassal", new AboutVASSALAction(this));
			
			mb.add(fileMenu);
			mb.add(toolsMenu);
			mb.add(mm.addMarker("Editor.MenuBar.start"));
			mb.add(mm.addMarker("Editor.MenuBar.end"));
			mb.add(helpMenu);
			
			Menu = mm.getMenuBarFor(this);
			
			// build toolbar
			toolBar.Tag = new WrapLayout((int) System.Drawing.ContentAlignment.TopLeft, 0, 0);
			toolBar.Layout += new System.Windows.Forms.LayoutEventHandler(SupportClass.FlowLayoutResize);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentX' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentX_float'"
			toolBar.setAlignmentX(0.0f);
			//UPGRADE_ISSUE: Method 'javax.swing.JToolBar.setFloatable' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJToolBarsetFloatable_boolean'"
			toolBar.setFloatable(false);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(toolBar);
			toolBar.Dock = System.Windows.Forms.DockStyle.Top;
			toolBar.SendToBack();
			
			// build central area
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			/*
			controlPanel.setLayout(new BorderLayout());*/
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			controlPanel.Size = new System.Drawing.Size(800, 600);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(controlPanel);
			controlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			controlPanel.BringToFront();
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
		}
		private void  PlayerWindow_Closing_EXIT_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 3);
		}
	}
}