/*
* $Id$
*
* Copyright (c) 2000-2009 by Brent Easton, Rodney Kinney, Joel Uckelman
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

#if NEVER_DEFINED
using System;
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
//UPGRADE_TODO: The type 'org.apache.commons.codec.digest.DigestUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DigestUtils = org.apache.commons.codec.digest.DigestUtils;
//UPGRADE_TODO: The type 'org.apache.commons.io.FileUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using FileUtils = org.apache.commons.io.FileUtils;
//UPGRADE_TODO: The type 'org.apache.commons.lang.SystemUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SystemUtils = org.apache.commons.lang.SystemUtils;
//UPGRADE_TODO: The type 'org.jdesktop.swingx.JXTreeTable' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using JXTreeTable = org.jdesktop.swingx.JXTreeTable;
//UPGRADE_TODO: The type 'org.jdesktop.swingx.treetable.DefaultMutableTreeTableNode' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DefaultMutableTreeTableNode = org.jdesktop.swingx.treetable.DefaultMutableTreeTableNode;
//UPGRADE_TODO: The type 'org.jdesktop.swingx.treetable.DefaultTreeTableModel' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DefaultTreeTableModel = org.jdesktop.swingx.treetable.DefaultTreeTableModel;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using Info = VassalSharp.Info;
using Documentation = VassalSharp.build.module.Documentation;
using ExtensionsManager = VassalSharp.build.module.ExtensionsManager;
using AbstractMetaData = VassalSharp.build.module.metadata.AbstractMetaData;
using ExtensionMetaData = VassalSharp.build.module.metadata.ExtensionMetaData;
using MetaDataFactory = VassalSharp.build.module.metadata.MetaDataFactory;
using ModuleMetaData = VassalSharp.build.module.metadata.ModuleMetaData;
using SaveMetaData = VassalSharp.build.module.metadata.SaveMetaData;
using CgiServerStatus = VassalSharp.chat.CgiServerStatus;
using ServerStatusView = VassalSharp.chat.ui.ServerStatusView;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using DirectoryConfigurer = VassalSharp.configure.DirectoryConfigurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using ShowHelpAction = VassalSharp.configure.ShowHelpAction;
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
using Resources = VassalSharp.i18n.Resources;
using PositionOption = VassalSharp.preferences.PositionOption;
using Prefs = VassalSharp.preferences.Prefs;
using ApplicationIcons = VassalSharp.tools.ApplicationIcons;
using BrowserSupport = VassalSharp.tools.BrowserSupport;
using ComponentSplitter = VassalSharp.tools.ComponentSplitter;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using WriteErrorDialog = VassalSharp.tools.WriteErrorDialog;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
using ModuleExtensionFileFilter = VassalSharp.tools.filechooser.ModuleExtensionFileFilter;
using IOUtils = VassalSharp.tools.io.IOUtils;
using LogPane = VassalSharp.tools.logging.LogPane;
using CheckBoxMenuItemProxy = VassalSharp.tools.menu.CheckBoxMenuItemProxy;
using MenuBarProxy = VassalSharp.tools.menu.MenuBarProxy;
using MenuItemProxy = VassalSharp.tools.menu.MenuItemProxy;
using MenuManager = VassalSharp.tools.menu.MenuManager;
using MenuProxy = VassalSharp.tools.menu.MenuProxy;
using Dialogs = VassalSharp.tools.swing.Dialogs;
using UpdateCheckAction = VassalSharp.tools.version.UpdateCheckAction;
#else
using System;
////UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using MigLayout = net.miginfocom.swing.MigLayout;
////UPGRADE_TODO: The type 'org.apache.commons.codec.digest.DigestUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using DigestUtils = org.apache.commons.codec.digest.DigestUtils;
////UPGRADE_TODO: The type 'org.apache.commons.io.FileUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using FileUtils = org.apache.commons.io.FileUtils;
////UPGRADE_TODO: The type 'org.apache.commons.lang.SystemUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using SystemUtils = org.apache.commons.lang.SystemUtils;
////UPGRADE_TODO: The type 'org.jdesktop.swingx.JXTreeTable' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using JXTreeTable = org.jdesktop.swingx.JXTreeTable;
////UPGRADE_TODO: The type 'org.jdesktop.swingx.treetable.DefaultMutableTreeTableNode' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using DefaultMutableTreeTableNode = org.jdesktop.swingx.treetable.DefaultMutableTreeTableNode;
////UPGRADE_TODO: The type 'org.jdesktop.swingx.treetable.DefaultTreeTableModel' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using DefaultTreeTableModel = org.jdesktop.swingx.treetable.DefaultTreeTableModel;
////UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using Logger = org.slf4j.Logger;
////UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using LoggerFactory = org.slf4j.LoggerFactory;
//using Info = VassalSharp.Info;
//using Documentation = VassalSharp.build.module.Documentation;
//using ExtensionsManager = VassalSharp.build.module.ExtensionsManager;
//using AbstractMetaData = VassalSharp.build.module.metadata.AbstractMetaData;
//using ExtensionMetaData = VassalSharp.build.module.metadata.ExtensionMetaData;
//using MetaDataFactory = VassalSharp.build.module.metadata.MetaDataFactory;
//using ModuleMetaData = VassalSharp.build.module.metadata.ModuleMetaData;
//using SaveMetaData = VassalSharp.build.module.metadata.SaveMetaData;
//using CgiServerStatus = VassalSharp.chat.CgiServerStatus;
//using ServerStatusView = VassalSharp.chat.ui.ServerStatusView;
//using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
//using DirectoryConfigurer = VassalSharp.configure.DirectoryConfigurer;
//using IntConfigurer = VassalSharp.configure.IntConfigurer;
//using ShowHelpAction = VassalSharp.configure.ShowHelpAction;
//using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
//using Resources = VassalSharp.i18n.Resources;
//using PositionOption = VassalSharp.preferences.PositionOption;
//using Prefs = VassalSharp.preferences.Prefs;
//using ApplicationIcons = VassalSharp.tools.ApplicationIcons;
//using BrowserSupport = VassalSharp.tools.BrowserSupport;
//using ComponentSplitter = VassalSharp.tools.ComponentSplitter;
//using ErrorDialog = VassalSharp.tools.ErrorDialog;
//using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
//using WriteErrorDialog = VassalSharp.tools.WriteErrorDialog;
//using FileChooser = VassalSharp.tools.filechooser.FileChooser;
//using ModuleExtensionFileFilter = VassalSharp.tools.filechooser.ModuleExtensionFileFilter;
//using IOUtils = VassalSharp.tools.io.IOUtils;
//using LogPane = VassalSharp.tools.logging.LogPane;
//using CheckBoxMenuItemProxy = VassalSharp.tools.menu.CheckBoxMenuItemProxy;
//using MenuBarProxy = VassalSharp.tools.menu.MenuBarProxy;
//using MenuItemProxy = VassalSharp.tools.menu.MenuItemProxy;
//using MenuManager = VassalSharp.tools.menu.MenuManager;
//using MenuProxy = VassalSharp.tools.menu.MenuProxy;
//using Dialogs = VassalSharp.tools.swing.Dialogs;
//using UpdateCheckAction = VassalSharp.tools.version.UpdateCheckAction;
#endif
namespace VassalSharp.launch
{
#if NEVER_DEFINED
	[Serializable]
	public class ModuleManagerWindow:System.Windows.Forms.Form
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction:SupportClass.ActionSupport
		{
			public AnonymousClassAbstractAction(ModuleManagerWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ModuleManagerWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ModuleManagerWindow enclosingInstance;
			public ModuleManagerWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				if (!AbstractLaunchAction.shutDown())
					return ;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'gp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Prefs gp = Prefs.GlobalPrefs;
				try
				{
					gp.close();
				}
				catch (System.IO.IOException ex)
				{
					WriteErrorDialog.error(ex, gp.File);
				}
				finally
				{
					IOUtils.closeQuietly(gp);
				}
				
				try
				{
					ModuleManager.Instance.shutDown();
				}
				catch (System.IO.IOException ex)
				{
					ErrorDialog.bug(ex);
				}
				
				VassalSharp.launch.ModuleManagerWindow.logger.info("Exiting");
				System.Environment.Exit(0);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(SupportClass.ActionSupport shutDownAction, ModuleManagerWindow enclosingInstance)
			{
				InitBlock(shutDownAction, enclosingInstance);
			}
			private void  InitBlock(SupportClass.ActionSupport shutDownAction, ModuleManagerWindow enclosingInstance)
			{
				this.shutDownAction = shutDownAction;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable shutDownAction was copied into class AnonymousClassWindowAdapter. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private SupportClass.ActionSupport shutDownAction;
			private ModuleManagerWindow enclosingInstance;
			public ModuleManagerWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs e)
			{
				e.Cancel = true;
				//UPGRADE_TODO: Method 'javax.swing.AbstractAction.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				shutDownAction.actionPerformed(null);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction1:SupportClass.ActionSupport
		{
			private void  InitBlock(VassalSharp.configure.BooleanConfigurer serverStatusConfig, ModuleManagerWindow enclosingInstance)
			{
				this.serverStatusConfig = serverStatusConfig;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable serverStatusConfig was copied into class AnonymousClassAbstractAction1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.BooleanConfigurer serverStatusConfig;
			private ModuleManagerWindow enclosingInstance;
			public ModuleManagerWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction1(VassalSharp.configure.BooleanConfigurer serverStatusConfig, ModuleManagerWindow enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(serverStatusConfig, enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.serverStatusView.toggleVisibility();
				serverStatusConfig.setValue((System.Object) (serverStatusConfig.booleanValue()?false:true));
				if (Enclosing_Instance.serverStatusView.isVisible())
				{
					Enclosing_Instance.DividerLocation = Enclosing_Instance.PreferredDividerLocation;
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction2:SupportClass.ActionSupport
		{
			private void  InitBlock(ModuleManagerWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ModuleManagerWindow enclosingInstance;
			public ModuleManagerWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction2(ModuleManagerWindow enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.OK_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				if (Dialogs.showConfirmDialog(Enclosing_Instance, Resources.getString("ModuleManager.clear_tilecache_title"), Resources.getString("ModuleManager.clear_tilecache_heading"), Resources.getString("ModuleManager.clear_tilecache_message"), (int) System.Windows.Forms.MessageBoxIcon.Exclamation, (int) System.Windows.Forms.MessageBoxButtons.OKCancel) == (int) System.Windows.Forms.DialogResult.OK)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'tdir '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.IO.FileInfo tdir = new System.IO.FileInfo(Info.ConfDir.FullName + "\\" + "tiles");
					bool tmpBool;
					if (System.IO.File.Exists(tdir.FullName))
						tmpBool = true;
					else
						tmpBool = System.IO.Directory.Exists(tdir.FullName);
					if (tmpBool)
					{
						try
						{
							FileUtils.forceDelete(tdir);
							FileUtils.forceMkdir(tdir);
						}
						catch (System.IO.IOException e)
						{
							WriteErrorDialog.error(e, tdir);
						}
					}
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(ModuleManagerWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ModuleManagerWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ModuleManagerWindow enclosingInstance;
			public ModuleManagerWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				setPreferredDividerLocation((System.Int32) e.NewValue);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(int loc, ModuleManagerWindow enclosingInstance)
			{
				InitBlock(loc, enclosingInstance);
			}
			//UPGRADE_NOTE: Delegate might have a different return value and generate a compilation error. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1309'"
			public delegate ~unresolved generatedDelegate();
			private void  InitBlock(int loc, ModuleManagerWindow enclosingInstance)
			{
				this.loc = loc;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable loc was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private int loc;
			private ModuleManagerWindow enclosingInstance;
			public ModuleManagerWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				Enclosing_Instance.serverStatusView.Invoke(new generatedDelegate(Enclosing_Instance.serverStatusView.setDividerLocation), new object[]{loc});
			}
		}

		public static ModuleManagerWindow Instance
		{
			get
			{
				return instance;
			}
			
		}
		virtual public bool WaitCursor
		{
			set
			{
				//UPGRADE_ISSUE: Member 'java.awt.Cursor.getPredefinedCursor' was converted to 'System.Windows.Forms.Cursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
				//UPGRADE_ISSUE: Member 'java.awt.Cursor.WAIT_CURSOR' was converted to 'System.Windows.Forms.Cursors.WaitCursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
				//UPGRADE_ISSUE: Member 'java.awt.Cursor.DEFAULT_CURSOR' was converted to 'System.Windows.Forms.Cursors.Default' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
				Cursor = value?System.Windows.Forms.Cursors.WaitCursor:System.Windows.Forms.Cursors.Default;
			}
			
		}
		virtual protected internal int DividerLocation
		{
			set
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'loc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int loc = value;
				//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				IThreadRunnable r = new AnonymousClassRunnable(loc, this);
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.invokeLater(r);
			}
			
		}
		virtual protected internal int PreferredDividerLocation
		{
			get
			{
				return dividerLocationConfig.getIntValue(500);
			}
			
			set
			{
				dividerLocationConfig.setValue(value);
			}
			
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.launch.ModuleManagerWindow'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		private const System.String SHOW_STATUS_KEY = "showServerStatus";
		private const System.String DIVIDER_LOCATION_KEY = "moduleManagerDividerLocation";
		private const int COLUMNS = 4;
		private const int KEY_COLUMN = 0;
		private const int VERSION_COLUMN = 1;
		private const int VASSAL_COLUMN = 2;
		private const int SPARE_COLUMN = 3;
		//UPGRADE_NOTE: Final was removed from the declaration of 'columnHeadings '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly System.String[] columnHeadings = new System.String[COLUMNS];
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'moduleIcon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIcon'"
		private System.Drawing.Image moduleIcon;
		//UPGRADE_NOTE: Final was removed from the declaration of 'activeExtensionIcon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIcon'"
		private System.Drawing.Image activeExtensionIcon;
		//UPGRADE_NOTE: Final was removed from the declaration of 'inactiveExtensionIcon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIcon'"
		private System.Drawing.Image inactiveExtensionIcon;
		//UPGRADE_NOTE: Final was removed from the declaration of 'openGameFolderIcon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIcon'"
		private System.Drawing.Image openGameFolderIcon;
		//UPGRADE_NOTE: Final was removed from the declaration of 'closedGameFolderIcon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIcon'"
		private System.Drawing.Image closedGameFolderIcon;
		//UPGRADE_NOTE: Final was removed from the declaration of 'fileIcon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIcon'"
		private System.Drawing.Image fileIcon;
		
		private StringArrayConfigurer recentModuleConfig;
		private System.IO.FileInfo selectedModule;
		
		//UPGRADE_ISSUE: Class 'java.awt.CardLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
		private CardLayout modulePanelLayout;
		private System.Windows.Forms.Panel moduleView;
		private ComponentSplitter.SplitPane serverStatusView;
		
		private MyTreeNode rootNode;
		private MyTree tree;
		private MyTreeTableModel treeModel;
		private MyTreeNode selectedNode;
		
		private long lastExpansionTime;
		//UPGRADE_ISSUE: Class 'javax.swing.tree.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
		private TreePath lastExpansionPath;
		
		private IntConfigurer dividerLocationConfig;
		
		private static long doubleClickInterval;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'instance '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly ModuleManagerWindow instance = new ModuleManagerWindow();
		
		public ModuleManagerWindow()
		{
			Text = "VASSAL";
			//UPGRADE_ISSUE: Method 'javax.swing.JFrame.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJFramesetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//UPGRADE_TODO: Method 'javax.swing.JFrame.getContentPane' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJFramegetContentPane'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			setLayout(new BoxLayout(((System.Windows.Forms.ContainerControl) this), BoxLayout.X_AXIS));
			
			ApplicationIcons.setFor(this);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'shutDownAction '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SupportClass.ActionSupport shutDownAction = new AnonymousClassAbstractAction(this);
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
			//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
			shutDownAction.putValue(Action.NAME, Resources.getString(Resources.QUIT));
			
			Closing += new System.ComponentModel.CancelEventHandler(this.ModuleManagerWindow_Closing_DO_NOTHING_ON_CLOSE);
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(shutDownAction, this).windowClosing);
			
			// setup menubar and actions
			//UPGRADE_NOTE: Final was removed from the declaration of 'mm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuManager mm = MenuManager.Instance;
			//UPGRADE_NOTE: Final was removed from the declaration of 'mb '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuBarProxy mb = mm.getMenuBarProxyFor(this);
			
			// file menu
			//UPGRADE_NOTE: Final was removed from the declaration of 'fileMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuProxy fileMenu = new MenuProxy(Resources.getString("General.file"));
			fileMenu.setMnemonic(Resources.getString("General.file.shortcut")[0]);
			
			fileMenu.add(mm.addKey("Main.play_module"));
			fileMenu.add(mm.addKey("Main.edit_module"));
			fileMenu.add(mm.addKey("Main.new_module"));
			fileMenu.add(mm.addKey("Main.import_module"));
			fileMenu.addSeparator();
			
			if (!SystemUtils.IS_OS_MAC_OSX)
			{
				fileMenu.add(mm.addKey("Prefs.edit_preferences"));
				fileMenu.addSeparator();
				fileMenu.add(mm.addKey("General.quit"));
			}
			
			// tools menu
			//UPGRADE_NOTE: Final was removed from the declaration of 'toolsMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuProxy toolsMenu = new MenuProxy(Resources.getString("General.tools"));
			
			// Initialize Global Preferences
			Prefs.GlobalPrefs.Editor.initDialog(this);
			Prefs.initSharedGlobalPrefs();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'serverStatusConfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Boolean tempAux = false;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			BooleanConfigurer serverStatusConfig = new BooleanConfigurer(SHOW_STATUS_KEY, null, ref tempAux);
			Prefs.GlobalPrefs.addOption(null, serverStatusConfig);
			
			dividerLocationConfig = new IntConfigurer(DIVIDER_LOCATION_KEY, null, - 10);
			Prefs.GlobalPrefs.addOption(null, dividerLocationConfig);
			
			toolsMenu.add(new CheckBoxMenuItemProxy(new AnonymousClassAbstractAction1(serverStatusConfig, this, Resources.getString("Chat.server_status")), serverStatusConfig.booleanValue()));
			
			toolsMenu.add(new MenuItemProxy(new AnonymousClassAbstractAction2(this, Resources.getString("ModuleManager.clear_tilecache"))));
			
			// help menu
			//UPGRADE_NOTE: Final was removed from the declaration of 'helpMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuProxy helpMenu = new MenuProxy(Resources.getString("General.help"));
			helpMenu.setMnemonic(Resources.getString("General.help.shortcut")[0]);
			
			helpMenu.add(mm.addKey("General.help"));
			helpMenu.add(mm.addKey("Main.tour"));
			helpMenu.add(mm.addKey("Help.user_guide"));
			helpMenu.addSeparator();
			helpMenu.add(mm.addKey("UpdateCheckAction.update_check"));
			helpMenu.add(mm.addKey("Help.error_log"));
			
			if (!SystemUtils.IS_OS_MAC_OSX)
			{
				helpMenu.addSeparator();
				helpMenu.add(mm.addKey("AboutScreen.about_vassal"));
			}
			
			mb.add(fileMenu);
			mb.add(toolsMenu);
			mb.add(helpMenu);
			
			// add actions
			mm.addAction("Main.play_module", new Player.PromptLaunchAction(this));
			mm.addAction("Main.edit_module", new Editor.PromptLaunchAction(this));
			mm.addAction("Main.new_module", new Editor.NewModuleLaunchAction(this));
			mm.addAction("Main.import_module", new Editor.PromptImportLaunchAction(this));
			mm.addAction("Prefs.edit_preferences", Prefs.GlobalPrefs.Editor.EditAction);
			mm.addAction("General.quit", shutDownAction);
			
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
			
			mm.addAction("Main.tour", new LaunchTourAction(this));
			mm.addAction("AboutScreen.about_vassal", new AboutVASSALAction(this));
			mm.addAction("UpdateCheckAction.update_check", new UpdateCheckAction(this));
			mm.addAction("Help.error_log", new ShowErrorLogAction(this));
			
			Menu = mm.getMenuBarFor(this);
			
			// Load Icons
			//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			moduleIcon = new ImageIcon(new System.Uri(System.IO.Path.GetFullPath("/images/mm-module.png")));
			//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			activeExtensionIcon = new ImageIcon(new System.Uri(System.IO.Path.GetFullPath("/images/mm-extension-active.png")));
			//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			inactiveExtensionIcon = new ImageIcon(new System.Uri(System.IO.Path.GetFullPath("/images/mm-extension-inactive.png")));
			//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			openGameFolderIcon = new ImageIcon(new System.Uri(System.IO.Path.GetFullPath("/images/mm-gamefolder-open.png")));
			//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			closedGameFolderIcon = new ImageIcon(new System.Uri(System.IO.Path.GetFullPath("/images/mm-gamefolder-closed.png")));
			//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			fileIcon = new ImageIcon(new System.Uri(System.IO.Path.GetFullPath("/images/mm-file.png")));
			
			// build module controls
			//UPGRADE_NOTE: Final was removed from the declaration of 'moduleControls '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			new BorderLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			System.Windows.Forms.Panel moduleControls = new System.Windows.Forms.Panel();
			//UPGRADE_ISSUE: Constructor 'java.awt.CardLayout.CardLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			modulePanelLayout = new CardLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			moduleView = new System.Windows.Forms.Panel();
			buildTree();
			//UPGRADE_NOTE: Final was removed from the declaration of 'scroll '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Windows.Forms.ScrollableControl scroll = new JScrollPane(tree);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			moduleView.Controls.Add(scroll);
			scroll.Dock = new System.Windows.Forms.DockStyle();
			scroll.BringToFront();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'javax.swing.JEditorPane' was converted to 'System.Windows.Forms.RichTextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Windows.Forms.RichTextBox temp_richtextbox;
			//UPGRADE_TODO: Constructor 'javax.swing.JEditorPane.JEditorPane' was converted to 'System.Windows.Forms.RichTextBox.LoadFile' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJEditorPaneJEditorPane_javalangString_javalangString'"
			temp_richtextbox = new System.Windows.Forms.RichTextBox();
			temp_richtextbox.Text = Resources.getString("ModuleManager.quickstart");;
			System.Windows.Forms.RichTextBox l = temp_richtextbox;
			l.ReadOnly = !false;
			
			// Try to get background color and font from LookAndFeel;
			// otherwise, use dummy JLabel to get color and font.
			//UPGRADE_TODO: Method 'javax.swing.UIManager.getColor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			System.Drawing.Color bg = UIManager.getColor("control");
			//UPGRADE_TODO: Method 'javax.swing.UIManager.getFont' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			System.Drawing.Font font = UIManager.getFont("Label.font");
			
			if (bg.IsEmpty || font == null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'dummy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Label dummy = new System.Windows.Forms.Label();
				if (bg.IsEmpty)
					bg = dummy.BackColor;
				if (font == null)
					font = dummy.Font;
			}
			
			l.BackColor = bg;
			//UPGRADE_ISSUE: Method 'javax.swing.text.html.HTMLEditorKit.getStyleSheet' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtexthtmlHTMLEditorKit'"
			//UPGRADE_ISSUE: Method 'javax.swing.JEditorPane.getEditorKit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJEditorPanegetEditorKit'"
			//UPGRADE_ISSUE: Class 'javax.swing.text.html.HTMLEditorKit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtexthtmlHTMLEditorKit'"
			SupportClass.StyleSheetSupport.AddStyleSheetRule(((HTMLEditorKit) l.getEditorKit()).getStyleSheet(), "body { font: " + font.FontFamily.Name + " " + (int) font.Size + "pt }");
			
			//UPGRADE_NOTE: Some methods of the 'javax.swing.event.HyperlinkListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			l.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(BrowserSupport.Listener.hyperlinkUpdate);
			
			// FIXME: use MigLayout for this!
			// this is necessary to get proper vertical alignment
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Constructor 'java.awt.GridBagLayout.GridBagLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagLayout'"
			new GridBagLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'java.awt.GridBagConstraints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			//UPGRADE_ISSUE: Constructor 'java.awt.GridBagConstraints.GridBagConstraints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			GridBagConstraints c = new GridBagConstraints();
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.fill' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.HORIZONTAL' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.fill = GridBagConstraints.HORIZONTAL;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.anchor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.CENTER' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.anchor = GridBagConstraints.CENTER;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			p.Controls.Add(l);
			l.Dock = new System.Windows.Forms.DockStyle();
			l.BringToFront();
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			moduleView.Controls.Add(p);
			p.Dock = new System.Windows.Forms.DockStyle();
			p.BringToFront();
			//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			modulePanelLayout.show(moduleView, getModuleCount() == 0?"quickStart":"modules");
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			moduleControls.Controls.Add(moduleView);
			moduleView.Dock = System.Windows.Forms.DockStyle.Fill;
			moduleView.BringToFront();
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.border.TitledBorder.TitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingborderTitledBorder'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(moduleControls.CreateGraphics(), 0, 0, moduleControls.Width, moduleControls.Height, new TitledBorder(Resources.getString("ModuleManager.recent_modules")));
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(moduleControls);
			
			// build server status controls
			//UPGRADE_NOTE: Final was removed from the declaration of 'serverStatusControls '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ServerStatusView serverStatusControls = new ServerStatusView(new CgiServerStatus());
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.border.TitledBorder.TitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingborderTitledBorder'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(serverStatusControls.CreateGraphics(), 0, 0, serverStatusControls.Width, serverStatusControls.Height, new TitledBorder(Resources.getString("Chat.server_status")));
			
			serverStatusView = new ComponentSplitter().splitRight(moduleControls, serverStatusControls, false);
			serverStatusView.revalidate();
			
			// show the server status controls according to the prefs
			if (serverStatusConfig.booleanValue())
			{
				serverStatusView.showComponent();
			}
			
			DividerLocation = PreferredDividerLocation;
			serverStatusView.addPropertyChangeListener("dividerLocation", new AnonymousClassPropertyChangeListener(this));
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle r = Info.getScreenBounds(this);
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			serverStatusControls.Size = new System.Drawing.Size((int) (r.Width / 3.5), 0);
			
			//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
			Size = new System.Drawing.Size(3 * r.Width / 4, 3 * r.Height / 4);
			
			// Save/load the window position and size in prefs
			//UPGRADE_NOTE: Final was removed from the declaration of 'option '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PositionOption option = new PositionOption(PositionOption.key + "ModuleManager", this);
			Prefs.GlobalPrefs.addOption(option);
		}
		
		protected internal virtual void  buildTree()
		{
			recentModuleConfig = new StringArrayConfigurer("RecentModules", null);
			Prefs.GlobalPrefs.addOption(null, recentModuleConfig);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < String > missingModules = new ArrayList < String >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < ModuleInfo > moduleList = new ArrayList < ModuleInfo >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String s: recentModuleConfig.getStringArray())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'module '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ModuleInfo module = new ModuleInfo(s);
				bool tmpBool;
				if (System.IO.File.Exists(module.File.FullName))
					tmpBool = true;
				else
					tmpBool = System.IO.Directory.Exists(module.File.FullName);
				if (tmpBool && module.Valid)
				{
					moduleList.add(module);
				}
				else
				{
					missingModules.add(s);
				}
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String s: missingModules)
			{
				logger.info(Resources.getString("ModuleManager.removing_module", s));
				moduleList.remove(s);
				recentModuleConfig.removeValue(s);
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Collections.sort(moduleList, new Comparator < ModuleInfo >()
			{
			}
		}
		public virtual int compare(ModuleInfo f1, ModuleInfo f2)
		{
			return f1.compareTo(f2);
		}
		private void  ModuleManagerWindow_Closing_DO_NOTHING_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 0);
		}
		static ModuleManagerWindow()
		{
			logger = LoggerFactory.getLogger(typeof(ModuleManagerWindow));
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'dci '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDesktopProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
				//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
				System.Object dci = Toolkit.getDefaultToolkit().getDesktopProperty("awt.multiClickInterval");
				doubleClickInterval = dci is System.Int32?(System.Int32) dci:200L;
			}
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	rootNode = new MyTreeNode(new RootInfo());
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(ModuleInfo moduleInfo: moduleList)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final MyTreeNode moduleNode = new MyTreeNode(moduleInfo);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(ExtensionInfo ext: moduleInfo.getExtensions())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final MyTreeNode extensionNode = new MyTreeNode(ext);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	moduleNode.add(extensionNode);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	final ArrayList < File > missingFolders = new ArrayList < File >();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(File f: moduleInfo.getFolders())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(f.exists() && f.isDirectory())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final GameFolderInfo folderInfo = new GameFolderInfo(f, moduleInfo);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final MyTreeNode folderNode = new MyTreeNode(folderInfo);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	moduleNode.add(folderNode);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final ArrayList < File > l = new ArrayList < File >();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final File [] files = f.listFiles();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(files == null) continue;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(File f1: files)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(f1.isFile())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		l.add(f1);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	Collections.sort(l);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(File f2: l)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final SaveFileInfo fileInfo = new SaveFileInfo(f2, folderInfo);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(fileInfo.isValid() && fileInfo.belongsToModule())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final MyTreeNode fileNode = new MyTreeNode(fileInfo);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	folderNode.add(fileNode);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		missingFolders.add(f);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	for(File mf: missingFolders)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		logger.info(
		Resources.getString(ModuleManager.removing_folder, mf.getPath()));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	moduleInfo.removeFolder(mf);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	rootNode.add(moduleNode);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	updateModuleList();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	treeModel = new MyTreeTableModel(rootNode);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree = new MyTree(treeModel);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.setRootVisible(false);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.setEditable(false);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.setTreeCellRenderer(new MyTreeCellRenderer());
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.addMouseListener(new MouseAdapter()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		@ Override
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void mouseClicked(MouseEvent e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(e.getClickCount() == 2)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final TreePath path = 
		tree.getPathForLocation(e.getPoint().x, e.getPoint().y);
	
	// do nothing if not on a node, or if this node was expanded
	// or collapsed during the past doubleClickInterval milliseconds
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(path == null ||(lastExpansionPath == path && 
	e.getWhen() - lastExpansionTime <= doubleClickInterval)) return;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	selectedNode =(MyTreeNode) path.getLastPathComponent();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final int row = tree.getRowForPath(path);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(row < 0) return;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final AbstractInfo target =
	(AbstractInfo) selectedNode.getUserObject();
	
	// launch module or load save, otherwise expand or collapse node
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(target instanceof ModuleInfo)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final ModuleInfo modInfo =(ModuleInfo) target;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(modInfo.isModuleTooNew())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ErrorDialog.show(
		Error.module_too_new, 
		modInfo.getFile().getPath(), 
		modInfo.getVassalVersion(), 
		Info.getVersion()
		);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		((ModuleInfo) target).play();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(target instanceof SaveFileInfo)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		((SaveFileInfo) target).play();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(tree.isExpanded(row))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		tree.collapseRow(row);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		tree.expandRow(row);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	@ Override
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void mouseReleased(MouseEvent e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final TreePath path = 
		tree.getPathForLocation(e.getPoint().x, e.getPoint().y);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(path == null) return;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	selectedNode =(MyTreeNode) path.getLastPathComponent();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(e.isMetaDown())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final int row = tree.getRowForPath(path);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(row >= 0)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		tree.clearSelection();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.addRowSelectionInterval(row, row);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final AbstractInfo target =
	(AbstractInfo) selectedNode.getUserObject();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	target.buildPopup(row).show(tree, e.getX(), e.getY());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	});
	
	// We capture the time and location of clicks which would cause
	// expansion in order to distinguish these from clicks which
	// might launch a module or game.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.addTreeWillExpandListener(new TreeWillExpandListener()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void treeWillCollapse(TreeExpansionEvent e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		lastExpansionTime = System.currentTimeMillis();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	lastExpansionPath = e.getPath();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void treeWillExpand(TreeExpansionEvent e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		lastExpansionTime = System.currentTimeMillis();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	lastExpansionPath = e.getPath();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	});
	
	// This ensures that double-clicks always start the module but
	// doesn't prevent single-clicks on the handles from working.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.setToggleClickCount(3);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.addTreeSelectionListener(new TreeSelectionListener()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void valueChanged(TreeSelectionEvent e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final MyTreeNode node =(MyTreeNode) e.getPath().getLastPathComponent();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final AbstractInfo target = node.getNodeInfo();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(target instanceof ModuleInfo)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		setSelectedModule(target.getFile());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(node.getParent() != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		setSelectedModule(node.getParentModuleFile());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	});
	
	// FIXME: Width handling needs improvement. Also save in prefs
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.getColumnModel().getColumn(KEY_COLUMN).setMinWidth(250);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.getColumnModel().getColumn(VERSION_COLUMN)
	.setCellRenderer(new VersionCellRenderer());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.getColumnModel().getColumn(VERSION_COLUMN).setMinWidth(100);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.getColumnModel().getColumn(VASSAL_COLUMN)
	.setCellRenderer(new VersionCellRenderer());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.getColumnModel().getColumn(VASSAL_COLUMN).setMinWidth(100);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.getColumnModel().getColumn(SPARE_COLUMN).setMinWidth(10);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.getColumnModel().getColumn(SPARE_COLUMN).setPreferredWidth(600);
	
	// FIXME: How to set alignment of individual header components?
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	tree.getTableHeader().setAlignmentX(JComponent.CENTER_ALIGNMENT);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> A File has been saved or created by the Player or the Editor. Update
	/// the display as necessary.
	/// </summary>
	/// <param name="f">The file
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void update(File f)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final AbstractMetaData data = MetaDataFactory.buildMetaData(f);
	
	// Module.
	// If we already have this module added, just refresh it, otherwise add it in.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(data instanceof ModuleMetaData)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final MyTreeNode moduleNode = rootNode.findNode(f);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(moduleNode == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		addModule(f);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		moduleNode.refresh();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	// Extension.
	// Check to see if it has been saved into one of the extension directories
	// for any module we already know of. Refresh the module
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	else if(data instanceof ExtensionMetaData)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < rootNode.getChildCount();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i ++)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final MyTreeNode moduleNode = rootNode.getChild(i);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final ModuleInfo moduleInfo =(ModuleInfo) moduleNode.getNodeInfo();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(ExtensionInfo ext: moduleInfo.getExtensions())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(ext.getFile().equals(f))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		moduleNode.refresh();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	// Save Game or Log file.
	// If the parent of the save file is already recorded as a Game Folder,
	// pass the file off to the Game Folder to handle. Otherwise, ignore it.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	else if(data instanceof SaveMetaData)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < rootNode.getChildCount();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i ++)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final MyTreeNode moduleNode = rootNode.getChild(i);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final MyTreeNode folderNode = moduleNode.findNode(f.getParentFile());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(folderNode != null && 
	folderNode.getNodeInfo() instanceof GameFolderInfo)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		((GameFolderInfo) folderNode.getNodeInfo()).update(f);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	tree.repaint();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Return the number of Modules added to the Module Manager
	/// 
	/// </summary>
	/// <returns> Number of modules
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private int getModuleCount()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return rootNode.getChildCount();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public File getSelectedModule()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return selectedModule;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setSelectedModule(File selectedModule)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		this.selectedModule = selectedModule;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void addModule(File f)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(!rootNode.contains(f))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final ModuleInfo moduleInfo = new ModuleInfo(f);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(moduleInfo.isValid())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final MyTreeNode moduleNode = new MyTreeNode(moduleInfo);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	treeModel.insertNodeInto(moduleNode, rootNode, 
	rootNode.findInsertIndex(moduleInfo));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(ExtensionInfo ext: moduleInfo.getExtensions())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final MyTreeNode extensionNode = new MyTreeNode(ext);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	treeModel.insertNodeInto(extensionNode, moduleNode, 
	moduleNode.findInsertIndex(ext));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	updateModuleList();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void removeModule(File f)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final MyTreeNode moduleNode = rootNode.findNode(f);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	treeModel.removeNodeFromParent(moduleNode);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	updateModuleList();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public File getModuleByName(String name)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(name == null) return null;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < rootNode.getChildCount();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i ++)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final ModuleInfo module =
		(ModuleInfo) rootNode.getChild(i).getNodeInfo();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(name.equals(module.getModuleName())) return module.getFile();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void updateModuleList()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final List < String > l = new ArrayList < String >();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < rootNode.getChildCount();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i ++)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final ModuleInfo module =
		(ModuleInfo)(rootNode.getChild(i)).getNodeInfo();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	l.add(module.encode());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	recentModuleConfig.setValue(l.toArray(new String [l.size()]));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	modulePanelLayout.show(
	moduleView, getModuleCount() == 0 ? quickStart: modules);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary>************************************************************************
	/// Custom Tree table model:-
	/// - Return column count
	/// - Return column headings
	/// </summary>
	private class MyTreeTableModel:DefaultTreeTableModel
	{
		virtual public int ColumnCount
		{
			get
			{
				return COLUMNS;
			}
			
		}
		public MyTreeTableModel(MyTreeNode rootNode):base(rootNode)
		{
			columnHeadings[KEY_COLUMN] = Resources.getString("ModuleManager.module");
			columnHeadings[VERSION_COLUMN] = Resources.getString("ModuleManager.version");
			columnHeadings[VASSAL_COLUMN] = Resources.getString("ModuleManager.vassal_version");
			columnHeadings[SPARE_COLUMN] = Resources.getString("ModuleManager.description");
		}
		
		public virtual System.String getColumnName(int col)
		{
			return columnHeadings[col];
		}
		
		public virtual System.Object getValueAt(System.Object node, int column)
		{
			return ((MyTreeNode) node).getValueAt(column);
		}
	}
	
	/// <summary> Custom implementation of JXTreeTable
	/// Fix for bug on startup generating illegal column numbers
	/// 
	/// </summary>
	private class MyTree:JXTreeTable
	{
		private const long serialVersionUID = 1L;
		
		public MyTree(MyTreeTableModel treeModel):base(treeModel)
		{
		}
		
		// FIXME: Where's the rest of the comment???
		/// <summary> There appears to be a bug/strange interaction between JXTreetable and the ComponentSplitter
		/// when the Component
		/// </summary>
		public virtual System.String getToolTipText(System.Windows.Forms.MouseEventArgs event_Renamed)
		{
			if (getComponentAt(event_Renamed.X, event_Renamed.Y) == null)
				return null;
			return base.getToolTipText(event_Renamed);
		}
	}
	
	/// <summary> Custom Tree cell renderer:-
	/// - Add file name as tooltip
	/// - Handle expanded display (some nodes use the same icon for expanded/unexpanded)
	/// - Gray out inactve extensions
	/// - Gray out Save Games that belong to other modules
	/// </summary>
	//UPGRADE_ISSUE: Class 'javax.swing.tree.DefaultTreeCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
	[Serializable]
	private class MyTreeCellRenderer:DefaultTreeCellRenderer
	{
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public System.Windows.Forms.Control getTreeCellRendererComponent(System.Windows.Forms.TreeView tree, System.Object value_Renamed, bool selected, bool expanded, bool leaf, int row, bool hasFocus)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.tree.DefaultTreeCellRenderer.getTreeCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
			base.getTreeCellRendererComponent(tree, value_Renamed, selected, expanded, leaf, row, hasFocus);
			//UPGRADE_NOTE: Final was removed from the declaration of 'info '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			AbstractInfo info = ((MyTreeNode) value_Renamed).NodeInfo;
			Text = info.ToString();
			SupportClass.ToolTipSupport.setToolTipText(this, info.ToolTipText);
			Image = info.getIcon(expanded);
			ForeColor = info.TreeCellFgColor;
			return this;
		}
	}
	
	/// <summary>************************************************************************
	/// Custom cell render for Version column
	/// - Center data
	/// </summary>
	//UPGRADE_ISSUE: Class 'javax.swing.table.DefaultTableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
	[Serializable]
	private class VersionCellRenderer:DefaultTableCellRenderer
	{
		private const long serialVersionUID = 1L;
		
		//UPGRADE_ISSUE: Constructor 'javax.swing.table.DefaultTableCellRenderer.DefaultTableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
		public VersionCellRenderer():base()
		{
			//UPGRADE_TODO: Method 'javax.swing.JLabel.setHorizontalAlignment' was converted to 'System.Windows.Forms.Label.ImageAlign' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJLabelsetHorizontalAlignment_int'"
			this.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
		}
		
		//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public System.Windows.Forms.Control getTableCellRendererComponent(System.Windows.Forms.DataGrid table, System.Object value_Renamed, bool isSelected, bool hasFocus, int row, int column)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.table.DefaultTableCellRenderer.getTableCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
			base.getTableCellRendererComponent(table, value_Renamed, isSelected, hasFocus, row, column);
			return this;
		}
	}
	
	/// <summary>************************************************************************
	/// Custom TreeTable Node
	/// </summary>
	private class MyTreeNode:DefaultMutableTreeTableNode
	{
		virtual public AbstractInfo NodeInfo
		{
			get
			{
				return (AbstractInfo) getUserObject();
			}
			
		}
		virtual public System.IO.FileInfo File
		{
			get
			{
				return NodeInfo.File;
			}
			
		}
		/// <summary> Return the Module node enclosing this node
		/// 
		/// </summary>
		/// <returns> Parent Tree Node
		/// </returns>
		virtual public MyTreeNode ParentModuleNode
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'info '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				AbstractInfo info = NodeInfo;
				if (info is RootInfo)
				{
					return null;
				}
				else if (info is ModuleInfo)
				{
					return this;
				}
				else if ((MyTreeNode) getParent() == null)
				{
					return null;
				}
				else
				{
					return ((MyTreeNode) getParent()).ParentModuleNode;
				}
			}
			
		}
		/// <summary> Return the Module file of the Module node enclosing this node
		/// 
		/// </summary>
		/// <returns> Module File
		/// </returns>
		virtual public System.IO.FileInfo ParentModuleFile
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'parentNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				MyTreeNode parentNode = ParentModuleNode;
				return parentNode == null?null:parentNode.File;
			}
			
		}
		
		public MyTreeNode(AbstractInfo nodeInfo):base(nodeInfo)
		{
			nodeInfo.TreeNode = this;
		}
		
		public virtual void  refresh()
		{
			NodeInfo.refresh();
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public virtual void  setValueAt(System.Object aValue, int column)
		{
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public virtual System.Object getValueAt(int column)
		{
			return NodeInfo.getValueAt(column);
		}
		
		public virtual MyTreeNode getChild(int index)
		{
			return (MyTreeNode) base.getChildAt(index);
		}
		
		public virtual MyTreeNode findNode(System.IO.FileInfo f)
		{
			for (int i = 0; i < getChildCount(); i++)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'moduleNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				MyTreeNode moduleNode = getChild(i);
				
				// NB: we canonicalize because File.equals() does not
				// always return true when one File is a relative path.
				try
				{
					f = new System.IO.FileInfo(f.FullName);
				}
				catch (System.IO.IOException e)
				{
					f = new System.IO.FileInfo(f.FullName);
				}
				
				if (f.Equals(moduleNode.NodeInfo.File))
				{
					return moduleNode;
				}
			}
			return null;
		}
		
		public virtual bool contains(System.IO.FileInfo f)
		{
			return findNode(f) != null;
		}
		
		public virtual int findInsertIndex(AbstractInfo info)
		{
			for (int i = 0; i < getChildCount(); i++)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'childNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				MyTreeNode childNode = getChild(i);
				if (childNode.NodeInfo.compareTo(info) >= 0)
				{
					return i;
				}
			}
			return getChildCount();
		}
	}
	
	/// <summary>************************************************************************
	/// All tree nodes encapsulate a User-defined object holding the user
	/// data for that node. In the ModuleManager, all user-defined objects
	/// are subclasses of AbstractInfo
	/// </summary>
	//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
	abstract public class AbstractInfo : System.IComparable
	{
		virtual public System.IO.FileInfo File
		{
			get
			{
				return file;
			}
			
			set
			{
				if (value == null)
					return ;
				
				try
				{
					file = new System.IO.FileInfo(value.FullName);
				}
				catch (System.IO.IOException e)
				{
					file = new System.IO.FileInfo(value.FullName);
				}
			}
			
		}
		virtual public System.String ToolTipText
		{
			get
			{
				if (file == null)
				{
					return "";
				}
				else
				{
					return file.FullName;
				}
			}
			
		}
		virtual public bool Valid
		{
			get
			{
				return valid;
			}
			
			set
			{
				valid = value;
			}
			
		}
		virtual public System.String Error
		{
			get
			{
				return error;
			}
			
			set
			{
				error = value;
			}
			
		}
		virtual public System.String Version
		{
			get
			{
				return "";
			}
			
		}
		virtual public System.String VassalVersion
		{
			get
			{
				return "";
			}
			
		}
		virtual public System.String Comments
		{
			get
			{
				return "";
			}
			
		}
		virtual public MyTreeNode TreeNode
		{
			get
			{
				return node;
			}
			
			set
			{
				node = value;
			}
			
		}
		/// <summary> Return a String used to sort different types of AbstractInfo's that are
		/// children of the same parent.
		/// 
		/// </summary>
		/// <returns> sort key
		/// </returns>
		public abstract System.String SortKey{get;}
		/// <summary> Return the color of the text used to display the name in column 1.
		/// Over-ride this to change color depending on item state.
		/// 
		/// </summary>
		/// <returns> cell text color
		/// </returns>
		virtual public System.Drawing.Color TreeCellFgColor
		{
			get
			{
				return System.Drawing.Color.Black;
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< AbstractInfo >
		protected internal System.IO.FileInfo file;
		protected internal System.Drawing.Image openIcon;
		protected internal System.Drawing.Image closedIcon;
		protected internal bool valid = true;
		protected internal System.String error = "";
		protected internal MyTreeNode node;
		
		public AbstractInfo(System.IO.FileInfo f, System.Drawing.Image open, System.Drawing.Image closed)
		{
			File = f;
			setIcon(open, closed);
		}
		public AbstractInfo(System.IO.FileInfo f, System.Drawing.Image i):this(f, i, i)
		{
		}
		
		public AbstractInfo(System.IO.FileInfo f):this(f, null)
		{
		}
		
		public AbstractInfo()
		{
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.String ToString()
		{
			return file == null?"":file.Name;
		}
		
		public virtual int compareTo(AbstractInfo info)
		{
			return String.CompareOrdinal(SortKey, info.SortKey);
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual System.Windows.Forms.ContextMenu buildPopup(int row)
		{
			return null;
		}
		
		public virtual System.Drawing.Image getIcon(bool expanded)
		{
			return expanded?openIcon:closedIcon;
		}
		
		public virtual void  setIcon(System.Drawing.Image i)
		{
			setIcon(i, i);
		}
		
		public virtual void  setIcon(System.Drawing.Image open, System.Drawing.Image closed)
		{
			openIcon = open;
			closedIcon = closed;
		}
		
		public virtual System.String getValueAt(int column)
		{
			switch (column)
			{
				
				case KEY_COLUMN: 
					return ToString();
				
				case VERSION_COLUMN: 
					return Version;
				
				case VASSAL_COLUMN: 
					return VassalVersion;
				
				default: 
					return null;
				
			}
		}
		
		/// <summary> Refresh yourself and any children</summary>
		public virtual void  refresh()
		{
			refreshChildren();
		}
		
		public virtual void  refreshChildren()
		{
			for (int i = 0; i < node.getChildCount(); i++)
			{
				(node.getChild(i)).refresh();
			}
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		abstract public System.Int32 CompareTo(System.Object obj);
	}
	
	/// <summary>************************************************************************
	/// Root Node User Information - Root node is hidden, so not much action here.
	/// </summary>
	private class RootInfo:AbstractInfo
	{
		override public System.String SortKey
		{
			get
			{
				return "";
			}
			
		}
		public RootInfo():base(null)
		{
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		override public System.Int32 CompareTo(System.Object obj)
		{
			return 0;
		}
	}
	
	/// <summary>************************************************************************
	/// Module Node User Information
	/// </summary>
	public class ModuleInfo:AbstractInfo
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		internal class AnonymousClassAbstractAction:SupportClass.ActionSupport
		{
			private void  InitBlock(ModuleInfo enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ModuleInfo enclosingInstance;
			public ModuleInfo Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction(ModuleInfo enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				FileChooser fc = FileChooser.createFileChooser(this, (DirectoryConfigurer) Prefs.GlobalPrefs.getOption(Prefs.MODULES_DIR_KEY));
				if (fc.showOpenDialog() == FileChooser.APPROVE_OPTION)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'selectedFile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.IO.FileInfo selectedFile = fc.SelectedFile;
					//UPGRADE_NOTE: Final was removed from the declaration of 'testExtInfo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					ExtensionInfo testExtInfo = new ExtensionInfo(selectedFile, true, null);
					if (testExtInfo.Valid)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.IO.FileInfo f = Enclosing_Instance.ExtensionsManager.setActive(fc.SelectedFile, true);
						//UPGRADE_NOTE: Final was removed from the declaration of 'moduleNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						MyTreeNode moduleNode = rootNode.findNode(selectedModule);
						//UPGRADE_NOTE: Final was removed from the declaration of 'extInfo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						ExtensionInfo extInfo = new ExtensionInfo(f, true, (ModuleInfo) moduleNode.NodeInfo);
						if (extInfo.Valid)
						{
							//UPGRADE_NOTE: Final was removed from the declaration of 'extNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							MyTreeNode extNode = new MyTreeNode(extInfo);
							treeModel.insertNodeInto(extNode, moduleNode, moduleNode.findInsertIndex(extInfo));
						}
					}
					else
					{
						SupportClass.OptionPaneSupport.ShowMessageDialog(null, testExtInfo.Error, null, (int) System.Windows.Forms.MessageBoxIcon.Error);
					}
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		internal class AnonymousClassAbstractAction1:SupportClass.ActionSupport
		{
			private void  InitBlock(ModuleInfo enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ModuleInfo enclosingInstance;
			public ModuleInfo Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction1(ModuleInfo enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				FileChooser fc = FileChooser.createFileChooser(this, (DirectoryConfigurer) Prefs.GlobalPrefs.getOption(Prefs.MODULES_DIR_KEY), FileChooser.DIRECTORIES_ONLY);
				if (fc.showOpenDialog() == FileChooser.APPROVE_OPTION)
				{
					Enclosing_Instance.addFolder(fc.SelectedFile);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction2:SupportClass.ActionSupport
		{
			private void  InitBlock(ModuleInfo enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ModuleInfo enclosingInstance;
			public ModuleInfo Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction2(ModuleInfo enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				removeModule(Enclosing_Instance.file);
				Enclosing_Instance.cleanupTileCache();
			}
		}
		private void  InitBlock()
		{
			newExtensionAction = new NewExtensionLaunchAction(this);
			addExtensionAction = new AnonymousClassAbstractAction(this, Resources.getString("ModuleManager.add_extension"));
			addFolderAction = new AnonymousClassAbstractAction1(this, Resources.getString("ModuleManager.add_save_game_folder"));
			return gameFolders;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < ExtensionInfo > l = new ArrayList < ExtensionInfo >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(File f: extMgr.getActiveExtensions())
			{
				l.add(new ExtensionInfo(f, true, this));
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(File f: extMgr.getInactiveExtensions())
			{
				l.add(new ExtensionInfo(f, false, this));
			}
			Collections.sort(l);
			return l;
		}
		virtual protected internal bool ModuleTooNew
		{
			get
			{
				return metadata == null?false:Info.isModuleTooNew(metadata.VassalVersion);
			}
			
		}
		override public System.String VassalVersion
		{
			get
			{
				return metadata == null?"":metadata.VassalVersion;
			}
			
		}
		virtual public ExtensionsManager ExtensionsManager
		{
			get
			{
				return extMgr;
			}
			
		}
		virtual public bool InUse
		{
			/*
			* Is the module currently being Played or Edited?
			*/
			
			get
			{
				return AbstractLaunchAction.isInUse(file) || AbstractLaunchAction.isEditing(file);
			}
			
		}
		override public System.String Version
		{
			get
			{
				return metadata.Version;
			}
			
		}
		virtual public System.String LocalizedDescription
		{
			get
			{
				return metadata.LocalizedDescription;
			}
			
		}
		virtual public System.String ModuleName
		{
			get
			{
				return metadata.Name;
			}
			
		}
		override public System.String SortKey
		{
			get
			{
				return metadata == null?"":metadata.LocalizedName;
			}
			
		}
		override public System.Drawing.Color TreeCellFgColor
		{
			get
			{
				return Info.isModuleTooNew(VassalVersion)?Color.GRAY:Color.BLACK;
			}
			
		}
		
		private ExtensionsManager extMgr;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private SortedSet < File > gameFolders = new TreeSet < File >();
		private ModuleMetaData metadata;
		
		//UPGRADE_NOTE: The initialization of  'newExtensionAction' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private SupportClass.ActionSupport newExtensionAction;
		
		//UPGRADE_NOTE: The initialization of  'addExtensionAction' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private SupportClass.ActionSupport addExtensionAction;
		
		//UPGRADE_NOTE: The initialization of  'addFolderAction' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private SupportClass.ActionSupport addFolderAction;
		
		public ModuleInfo(System.IO.FileInfo f):base(f, moduleIcon)
		{
			InitBlock();
			extMgr = new ExtensionsManager(f);
			loadMetaData();
		}
		
		protected internal virtual void  loadMetaData()
		{
			AbstractMetaData data = MetaDataFactory.buildMetaData(file);
			if (data != null && data is ModuleMetaData)
			{
				Valid = true;
				metadata = (ModuleMetaData) data;
			}
			else
			{
				Valid = false;
			}
		}
		
		/// <summary> Initialise ModuleInfo based on a saved preference string.
		/// See encode().
		/// 
		/// </summary>
		/// <param name="s">Preference String
		/// </param>
		public ModuleInfo(System.String s)
		{
			InitBlock();
			SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, ';');
			File = new System.IO.FileInfo(sd.nextToken());
			setIcon(moduleIcon);
			loadMetaData();
			extMgr = new ExtensionsManager(File);
			while (sd.hasMoreTokens())
			{
				gameFolders.add(new System.IO.FileInfo(sd.nextToken()));
			}
		}
		
		/// <summary> Refresh this module and all children</summary>
		public override void  refresh()
		{
			loadMetaData();
			
			// Remove any missing children
			//UPGRADE_NOTE: Final was removed from the declaration of 'nodes '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MyTreeNode[] nodes = new MyTreeNode[TreeNode.getChildCount()];
			for (int i = 0; i < TreeNode.getChildCount(); i++)
			{
				nodes[i] = TreeNode.getChild(i);
			}
			for (int i = 0; i < nodes.Length; i++)
			{
				bool tmpBool;
				if (System.IO.File.Exists(nodes[i].File.FullName))
					tmpBool = true;
				else
					tmpBool = System.IO.Directory.Exists(nodes[i].File.FullName);
				if (!tmpBool)
				{
					treeModel.removeNodeFromParent(nodes[i]);
				}
			}
			
			// Refresh or add any existing children
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(ExtensionInfo ext: getExtensions())
			{
				MyTreeNode extNode = TreeNode.findNode(ext.getFile());
				if (extNode == null)
				{
					if (ext.isValid())
					{
						extNode = new MyTreeNode(ext);
						treeModel.insertNodeInto(extNode, TreeNode, TreeNode.findInsertIndex(ext));
					}
				}
				else
				{
					extNode.refresh();
				}
			}
		}
		
		/// <summary> Encode any information which needs to be recorded in the Preference entry for this module:-
		/// - Path to Module File
		/// - Paths to any child Save Game Folders
		/// 
		/// </summary>
		/// <returns> encoded data
		/// </returns>
		public virtual System.String encode()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(file.FullName, ';');
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(File f: gameFolders)
			{
				se.append(f.getPath());
			}
			return se.Value;
		}
		
		public virtual void  addFolder(System.IO.FileInfo f)
		{
			// try to create the directory if it doesn't exist
			bool tmpBool;
			if (System.IO.File.Exists(f.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(f.FullName);
			//UPGRADE_TODO: Method 'java.io.File.mkdirs' was converted to 'System.IO.Directory.CreateDirectory' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFilemkdirs'"
			if (!tmpBool && !System.IO.Directory.CreateDirectory(f.FullName))
			{
				SupportClass.OptionPaneSupport.ShowMessageDialog(this, Resources.getString("Install.error_unable_to_create", f.FullName), "Error", (int) System.Windows.Forms.MessageBoxIcon.Error);
				
				return ;
			}
			
			gameFolders.add(f);
			//UPGRADE_NOTE: Final was removed from the declaration of 'moduleNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MyTreeNode moduleNode = rootNode.findNode(selectedModule);
			//UPGRADE_NOTE: Final was removed from the declaration of 'folderInfo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GameFolderInfo folderInfo = new GameFolderInfo(f, (ModuleInfo) moduleNode.NodeInfo);
			//UPGRADE_NOTE: Final was removed from the declaration of 'folderNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MyTreeNode folderNode = new MyTreeNode(folderInfo);
			//UPGRADE_NOTE: Final was removed from the declaration of 'idx '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int idx = moduleNode.findInsertIndex(folderInfo);
			treeModel.insertNodeInto(folderNode, moduleNode, idx);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(File file: f.listFiles())
			{
				if (System.IO.File.Exists(file.FullName))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'fileInfo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SaveFileInfo fileInfo = new SaveFileInfo(file, folderInfo);
					if (fileInfo.Valid && fileInfo.belongsToModule())
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'fileNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						MyTreeNode fileNode = new MyTreeNode(fileInfo);
						treeModel.insertNodeInto(fileNode, folderNode, folderNode.findInsertIndex(fileInfo));
					}
				}
			}
			updateModuleList();
		}
		
		public virtual void  removeFolder(System.IO.FileInfo f)
		{
			gameFolders.remove(f);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public SortedSet < File > getFolders()
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < ExtensionInfo > getExtensions()
		
		public virtual void  play()
		{
			new Player.LaunchAction(this, file).actionPerformed(null);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public override System.Windows.Forms.ContextMenu buildPopup(int row)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Windows.Forms.ContextMenu m = new System.Windows.Forms.ContextMenu();
			//UPGRADE_NOTE: Final was removed from the declaration of 'playAction '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SupportClass.ActionSupport playAction = new Player.LaunchAction(this, file);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			playAction.setEnabled(!Info.isModuleTooNew(metadata.VassalVersion));
			System.Windows.Forms.MenuItem temp_MenuItem;
			temp_MenuItem = new System.Windows.Forms.MenuItem();
			temp_MenuItem.Click += new System.EventHandler(playAction.actionPerformed);
			m.MenuItems.Add(temp_MenuItem);
			System.Windows.Forms.MenuItem generatedAux = temp_MenuItem;
			//UPGRADE_NOTE: Final was removed from the declaration of 'editAction '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SupportClass.ActionSupport editAction = new Editor.ListLaunchAction(this, file);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			editAction.setEnabled(!Info.isModuleTooNew(metadata.VassalVersion));
			System.Windows.Forms.MenuItem temp_MenuItem2;
			temp_MenuItem2 = new System.Windows.Forms.MenuItem();
			temp_MenuItem2.Click += new System.EventHandler(editAction.actionPerformed);
			m.MenuItems.Add(temp_MenuItem2);
			System.Windows.Forms.MenuItem generatedAux2 = temp_MenuItem2;
			System.Windows.Forms.MenuItem temp_MenuItem3;
			temp_MenuItem3 = new System.Windows.Forms.MenuItem();
			temp_MenuItem3.Click += new System.EventHandler(new AnonymousClassAbstractAction2(this, Resources.getString("General.remove")).actionPerformed);
			m.MenuItems.Add(temp_MenuItem3);
			System.Windows.Forms.MenuItem generatedAux3 = temp_MenuItem3;
			
			m.MenuItems.Add("-");
			System.Windows.Forms.MenuItem temp_MenuItem4;
			temp_MenuItem4 = new System.Windows.Forms.MenuItem();
			temp_MenuItem4.Click += new System.EventHandler(addFolderAction.actionPerformed);
			m.MenuItems.Add(temp_MenuItem4);
			System.Windows.Forms.MenuItem generatedAux4 = temp_MenuItem4;
			m.MenuItems.Add("-");
			System.Windows.Forms.MenuItem temp_MenuItem5;
			temp_MenuItem5 = new System.Windows.Forms.MenuItem();
			temp_MenuItem5.Click += new System.EventHandler(newExtensionAction.actionPerformed);
			m.MenuItems.Add(temp_MenuItem5);
			System.Windows.Forms.MenuItem generatedAux5 = temp_MenuItem5;
			System.Windows.Forms.MenuItem temp_MenuItem6;
			temp_MenuItem6 = new System.Windows.Forms.MenuItem();
			temp_MenuItem6.Click += new System.EventHandler(addExtensionAction.actionPerformed);
			m.MenuItems.Add(temp_MenuItem6);
			System.Windows.Forms.MenuItem generatedAux6 = temp_MenuItem6;
			return m;
		}
		
		public virtual void  cleanupTileCache()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'hstr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String hstr = DigestUtils.shaHex(metadata.Name + "_" + metadata.Version);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'tdir '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.FileInfo tdir = new System.IO.FileInfo(Info.ConfDir.FullName + "\\" + "tiles/" + hstr);
			bool tmpBool;
			if (System.IO.File.Exists(tdir.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(tdir.FullName);
			if (tmpBool)
			{
				try
				{
					FileUtils.forceDelete(tdir);
				}
				catch (System.IO.IOException e)
				{
					WriteErrorDialog.error(e, tdir);
				}
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.String ToString()
		{
			return metadata.LocalizedName;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.String getValueAt(int column)
		{
			return column == SPARE_COLUMN?LocalizedDescription:base.getValueAt(column);
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		override public System.Int32 CompareTo(System.Object obj)
		{
			return 0;
		}
	}
	
	/// <summary>************************************************************************
	/// Extension Node User Information
	/// </summary>
	private class ExtensionInfo:AbstractInfo
	{
		virtual public bool Active
		{
			get
			{
				return active;
			}
			
			set
			{
				active = value;
				setIcon(active?activeExtensionIcon:inactiveExtensionIcon);
			}
			
		}
		override public System.String Version
		{
			get
			{
				return metadata == null?"":metadata.Version;
			}
			
		}
		override public System.String VassalVersion
		{
			get
			{
				return metadata == null?"":metadata.VassalVersion;
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				return metadata == null?"":metadata.getDescription();
			}
			
		}
		virtual public ExtensionsManager ExtensionsManager
		{
			get
			{
				return moduleInfo == null?null:moduleInfo.ExtensionsManager;
			}
			
		}
		override public System.Drawing.Color TreeCellFgColor
		{
			get
			{
				// FIXME: should get colors from LAF
				if (Active)
				{
					return metadata == null?System.Drawing.Color.Red:System.Drawing.Color.Black;
				}
				else
				{
					return metadata == null?System.Drawing.Color.Pink:System.Drawing.Color.Gray;
				}
			}
			
		}
		virtual public bool InUse
		{
			/*
			* Is the extension, or its owning module currently being Played or Edited?
			*/
			
			get
			{
				return AbstractLaunchAction.isInUse(file) || AbstractLaunchAction.isEditing(file);
			}
			
		}
		/// <summary> Sort Extensions by File Name</summary>
		override public System.String SortKey
		{
			get
			{
				return File.Name;
			}
			
		}
		
		private bool active;
		private ModuleInfo moduleInfo;
		private ExtensionMetaData metadata;
		
		public ExtensionInfo(System.IO.FileInfo file, bool active, ModuleInfo module):base(file, active?activeExtensionIcon:inactiveExtensionIcon)
		{
			this.active = active;
			moduleInfo = module;
			loadMetaData();
		}
		
		protected internal virtual void  loadMetaData()
		{
			AbstractMetaData data = MetaDataFactory.buildMetaData(file);
			if (data != null && data is ExtensionMetaData)
			{
				Valid = true;
				metadata = (ExtensionMetaData) data;
			}
			else
			{
				Error = Resources.getString("ModuleManager.invalid_extension");
				Valid = false;
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override void  refresh()
		{
			loadMetaData();
			Active = ExtensionsManager.isExtensionActive(File);
			tree.repaint();
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.String ToString()
		{
			System.String s = File.Name;
			System.String st = "";
			if (metadata == null)
			{
				st = Resources.getString("ModuleManager.invalid");
			}
			if (!active)
			{
				st += (st.Length > 0?",":"");
				st += Resources.getString("ModuleManager.inactive");
			}
			if (st.Length > 0)
			{
				s += (" (" + st + ")");
			}
			
			return s;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public override System.Windows.Forms.ContextMenu buildPopup(int row)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Windows.Forms.ContextMenu m = new System.Windows.Forms.ContextMenu();
			System.Windows.Forms.MenuItem temp_MenuItem;
			temp_MenuItem = new System.Windows.Forms.MenuItem();
			temp_MenuItem.Click += new System.EventHandler(new ActivateExtensionAction(this, Resources.getString(Active?"ModuleManager.deactivate":"ModuleManager.activate")).actionPerformed);
			m.MenuItems.Add(temp_MenuItem);
			System.Windows.Forms.MenuItem generatedAux = temp_MenuItem;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'editAction '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SupportClass.ActionSupport editAction = new EditExtensionLaunchAction(this, File, getSelectedModule());
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			editAction.setEnabled(!Info.isModuleTooNew(metadata.VassalVersion));
			System.Windows.Forms.MenuItem temp_MenuItem2;
			temp_MenuItem2 = new System.Windows.Forms.MenuItem();
			temp_MenuItem2.Click += new System.EventHandler(editAction.actionPerformed);
			m.MenuItems.Add(temp_MenuItem2);
			System.Windows.Forms.MenuItem generatedAux2 = temp_MenuItem2;
			return m;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.String getValueAt(int column)
		{
			return column == SPARE_COLUMN?Description:base.getValueAt(column);
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ActivateExtensionAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class ActivateExtensionAction:SupportClass.ActionSupport
		{
			private void  InitBlock(ExtensionInfo enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ExtensionInfo enclosingInstance;
			public ExtensionInfo Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			public ActivateExtensionAction(ExtensionInfo enclosingInstance, System.String s):base(s)
			{
				InitBlock(enclosingInstance);
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
				setEnabled(!Enclosing_Instance.InUse && !Enclosing_Instance.moduleInfo.InUse);
			}
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				Enclosing_Instance.File = Enclosing_Instance.ExtensionsManager.setActive(Enclosing_Instance.File, !Enclosing_Instance.Active);
				Enclosing_Instance.Active = Enclosing_Instance.ExtensionsManager.isExtensionActive(Enclosing_Instance.File);
				//UPGRADE_NOTE: Final was removed from the declaration of 'path '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class 'javax.swing.tree.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
				TreePath path = tree.getPathForRow(tree.getSelectedRow());
				//UPGRADE_NOTE: Final was removed from the declaration of 'extNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'javax.swing.tree.TreePath.getLastPathComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
				MyTreeNode extNode = (MyTreeNode) path.getLastPathComponent();
				treeModel.setValueAt("", extNode, 0);
			}
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		override public System.Int32 CompareTo(System.Object obj)
		{
			return 0;
		}
	}
	
	/// <summary>************************************************************************
	/// Saved Game Folder Node User Information
	/// </summary>
	private class GameFolderInfo:AbstractInfo
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction:SupportClass.ActionSupport
		{
			private void  InitBlock(GameFolderInfo enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GameFolderInfo enclosingInstance;
			public GameFolderInfo Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction(GameFolderInfo enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.refresh();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction1:SupportClass.ActionSupport
		{
			private void  InitBlock(GameFolderInfo enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GameFolderInfo enclosingInstance;
			public GameFolderInfo Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction1(GameFolderInfo enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'moduleNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				MyTreeNode moduleNode = rootNode.findNode(Enclosing_Instance.moduleInfo.File);
				//UPGRADE_NOTE: Final was removed from the declaration of 'folderNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				MyTreeNode folderNode = moduleNode.findNode(Enclosing_Instance.File);
				treeModel.removeNodeFromParent(folderNode);
				Enclosing_Instance.moduleInfo.removeFolder(Enclosing_Instance.File);
				updateModuleList();
			}
		}
		virtual public ModuleInfo ModuleInfo
		{
			get
			{
				return moduleInfo;
			}
			
		}
		/// <summary> Force Game Folders to sort after extensions</summary>
		override public System.String SortKey
		{
			get
			{
				return "~~~" + File.Name;
			}
			
		}
		protected internal System.String comment;
		protected internal ModuleInfo moduleInfo;
		protected internal long dtm;
		
		public GameFolderInfo(System.IO.FileInfo f, ModuleInfo m):base(f, openGameFolderIcon, closedGameFolderIcon)
		{
			moduleInfo = m;
			//UPGRADE_TODO: The equivalent in .NET for method 'java.io.File.lastModified' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			dtm = ((f.LastWriteTime.Ticks - 621355968000000000) / 10000);
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public override System.Windows.Forms.ContextMenu buildPopup(int row)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Windows.Forms.ContextMenu m = new System.Windows.Forms.ContextMenu();
			System.Windows.Forms.MenuItem temp_MenuItem;
			temp_MenuItem = new System.Windows.Forms.MenuItem();
			temp_MenuItem.Click += new System.EventHandler(new AnonymousClassAbstractAction(this, Resources.getString("General.refresh")).actionPerformed);
			m.MenuItems.Add(temp_MenuItem);
			System.Windows.Forms.MenuItem generatedAux = temp_MenuItem;
			m.MenuItems.Add("-");
			System.Windows.Forms.MenuItem temp_MenuItem2;
			temp_MenuItem2 = new System.Windows.Forms.MenuItem();
			temp_MenuItem2.Click += new System.EventHandler(new AnonymousClassAbstractAction1(this, Resources.getString("General.remove")).actionPerformed);
			m.MenuItems.Add(temp_MenuItem2);
			System.Windows.Forms.MenuItem generatedAux2 = temp_MenuItem2;
			
			return m;
		}
		
		public override void  refresh()
		{
			
			// Remove any files that no longer exist
			for (int i = TreeNode.getChildCount() - 1; i >= 0; i--)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fileNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				MyTreeNode fileNode = TreeNode.getChild(i);
				//UPGRADE_NOTE: Final was removed from the declaration of 'fileInfo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SaveFileInfo fileInfo = (SaveFileInfo) fileNode.NodeInfo;
				bool tmpBool;
				if (System.IO.File.Exists(fileInfo.File.FullName))
					tmpBool = true;
				else
					tmpBool = System.IO.Directory.Exists(fileInfo.File.FullName);
				if (!tmpBool)
				{
					treeModel.removeNodeFromParent(fileNode);
				}
			}
			
			// Refresh any that are. Only include Save files belonging to this
			// module, or that are pre vassal 3.1
			//UPGRADE_NOTE: Final was removed from the declaration of 'files '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.FileInfo[] files = SupportClass.FileSupport.GetFiles(File);
			if (files == null)
				return ;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(File f: files)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fdata '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				AbstractMetaData fdata = MetaDataFactory.buildMetaData(f);
				if (fdata != null)
				{
					if (fdata is SaveMetaData)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'moduleName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String moduleName = ((SaveMetaData) fdata).ModuleName;
						if (moduleName == null || moduleName.Length == 0 || moduleName.Equals(ModuleInfo.ModuleName))
						{
							update(f);
						}
					}
				}
			}
		}
		
		/// <summary> Update the display for the specified save File, or add it in if
		/// we don't already know about it.
		/// </summary>
		/// <param name="f">
		/// </param>
		public virtual void  update(System.IO.FileInfo f)
		{
			for (int i = 0; i < TreeNode.getChildCount(); i++)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fileInfo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SaveFileInfo fileInfo = (SaveFileInfo) (TreeNode.getChild(i)).NodeInfo;
				if (fileInfo.File.Equals(f))
				{
					fileInfo.refresh();
					return ;
				}
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'fileInfo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SaveFileInfo fileInfo2 = new SaveFileInfo(f, this);
			//UPGRADE_NOTE: Final was removed from the declaration of 'fileNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MyTreeNode fileNode = new MyTreeNode(fileInfo2);
			treeModel.insertNodeInto(fileNode, TreeNode, TreeNode.findInsertIndex(fileInfo2));
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		override public System.Int32 CompareTo(System.Object obj)
		{
			return 0;
		}
	}
	
	/// <summary>************************************************************************
	/// Saved Game File Node User Information
	/// </summary>
	private class SaveFileInfo:AbstractInfo
	{
		virtual protected internal System.IO.FileInfo ModuleFile
		{
			get
			{
				return folderInfo.ModuleInfo.File;
			}
			
		}
		override public System.Drawing.Color TreeCellFgColor
		{
			get
			{
				// FIXME: should get colors from LAF
				return belongsToModule()?System.Drawing.Color.Black:System.Drawing.Color.Gray;
			}
			
		}
		override public System.String Version
		{
			get
			{
				return metadata == null?"":metadata.ModuleVersion;
			}
			
		}
		/// <summary> Sort Save Files by file name</summary>
		override public System.String SortKey
		{
			get
			{
				return this.File.Name;
			}
			
		}
		
		protected internal GameFolderInfo folderInfo; // Owning Folder
		protected internal SaveMetaData metadata; // Save file metadata
		
		public SaveFileInfo(System.IO.FileInfo f, GameFolderInfo folder):base(f, fileIcon)
		{
			folderInfo = folder;
			loadMetaData();
		}
		
		protected internal virtual void  loadMetaData()
		{
			AbstractMetaData data = MetaDataFactory.buildMetaData(file);
			if (data != null && data is SaveMetaData)
			{
				metadata = (SaveMetaData) data;
				Valid = true;
			}
			else
			{
				Valid = false;
			}
		}
		
		public override void  refresh()
		{
			loadMetaData();
			tree.repaint();
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public override System.Windows.Forms.ContextMenu buildPopup(int row)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Windows.Forms.ContextMenu m = new System.Windows.Forms.ContextMenu();
			System.Windows.Forms.MenuItem temp_MenuItem;
			temp_MenuItem = new System.Windows.Forms.MenuItem();
			temp_MenuItem.Click += new System.EventHandler(new Player.LaunchAction(this, ModuleFile, file).actionPerformed);
			m.MenuItems.Add(temp_MenuItem);
			System.Windows.Forms.MenuItem generatedAux = temp_MenuItem;
			return m;
		}
		
		public virtual void  play()
		{
			new Player.LaunchAction(this, ModuleFile, file).actionPerformed(null);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.String getValueAt(int column)
		{
			return column == SPARE_COLUMN?buildComments():base.getValueAt(column);
		}
		
		private System.String buildComments()
		{
			System.String comments = "";
			if (!belongsToModule())
			{
				if (metadata != null && metadata.ModuleName.Length > 0)
				{
					comments = "[" + metadata.ModuleName + "] ";
				}
			}
			comments += (metadata == null?"":metadata.getDescription());
			return comments;
		}
		
		private bool belongsToModule()
		{
			return metadata != null && (metadata.ModuleName.Length == 0 || folderInfo.ModuleInfo.ModuleName.Equals(metadata.ModuleName));
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		override public System.Int32 CompareTo(System.Object obj)
		{
			return 0;
		}
	}
	
	/// <summary> Action to create a New Extension and edit it in another process.</summary>
	[Serializable]
	private class NewExtensionLaunchAction:AbstractLaunchAction
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassLaunchTask' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassLaunchTask:LaunchTask
		{
			public AnonymousClassLaunchTask(NewExtensionLaunchAction enclosingInstance):base(enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(NewExtensionLaunchAction enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private NewExtensionLaunchAction enclosingInstance;
			public new NewExtensionLaunchAction Enclosing_Instance
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
				
				// reduce the using count
				System.Int32 count = using_Renamed.get_Renamed(lr.module);
				if ((System.Object) count == 1)
					using_Renamed.remove(lr.module);
				else
					using_Renamed.put(lr.module, --count);
			}
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public NewExtensionLaunchAction(System.Windows.Forms.Form frame):base(Resources.getString("ModuleManager.new_extension"), frame, typeof(Editor).FullName, new LaunchRequest(LaunchRequest.Mode.NEW_EXT))
		{
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			lr.module = getSelectedModule();
			
			// register that this module is being used
			if (editing.contains(lr.module))
				return ;
			System.Int32 count = using_Renamed.get_Renamed(lr.module);
			//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
			using_Renamed.put(lr.module, count == null?1:++count);
			
			base.actionPerformed(event_sender, e);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override LaunchTask getLaunchTask()
		{
			return new AnonymousClassLaunchTask(this);
		}
	}
	
	/// <summary> Action to Edit an Extension in another process</summary>
	[Serializable]
	private class EditExtensionLaunchAction:AbstractLaunchAction
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassLaunchTask' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassLaunchTask:LaunchTask
		{
			public AnonymousClassLaunchTask(EditExtensionLaunchAction enclosingInstance):base(enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(EditExtensionLaunchAction enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private EditExtensionLaunchAction enclosingInstance;
			public new EditExtensionLaunchAction Enclosing_Instance
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
				
				// reduce the using count for module
				System.Int32 count = using_Renamed.get_Renamed(lr.module);
				if ((System.Object) count == 1)
					using_Renamed.remove(lr.module);
				else
					using_Renamed.put(lr.module, --count);
				
				// reduce that this extension is done being edited
				editing.remove(lr.extension);
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
				Enclosing_Instance.setEnabled(true);
			}
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public EditExtensionLaunchAction(System.Windows.Forms.Form frame, System.IO.FileInfo extension, System.IO.FileInfo module):base(Resources.getString("Editor.edit_extension"), frame, typeof(Editor).FullName, new LaunchRequest(LaunchRequest.Mode.EDIT_EXT, module, extension))
		{
			
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
			setEnabled(!using_Renamed.containsKey(module) && !editing.contains(module) && !editing.contains(extension) && !using_Renamed.containsKey(extension));
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			// check that neither this module nor this extension is being edited
			if (editing.contains(lr.module) || editing.contains(lr.extension))
				return ;
			
			// register that this module is being used
			System.Int32 count = using_Renamed.get_Renamed(lr.module);
			//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
			using_Renamed.put(lr.module, count == null?1:++count);
			
			// register that this extension is being edited
			editing.add(lr.extension);
			
			base.actionPerformed(event_sender, e);
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
			setEnabled(false);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override void  addFileFilters(FileChooser fc)
		{
			fc.addChoosableFileFilter(new ModuleExtensionFileFilter());
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override LaunchTask getLaunchTask()
		{
			return new AnonymousClassLaunchTask(this);
		}
	}
	
	[Serializable]
	private class ShowErrorLogAction:SupportClass.ActionSupport
	{
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		private System.Windows.Forms.Form frame;
		
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public ShowErrorLogAction(System.Windows.Forms.Form frame):base(Resources.getString("Help.error_log"))
		{
			this.frame = frame;
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			// FIXME: don't create a new one each time!
			//UPGRADE_NOTE: Final was removed from the declaration of 'logfile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.FileInfo logfile = new System.IO.FileInfo(Info.HomeDir.FullName + "\\" + "errorLog");
			//UPGRADE_NOTE: Final was removed from the declaration of 'lp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			LogPane lp = new LogPane(logfile);
			
			// FIXME: this should have its own key. Probably keys should be renamed
			// to reflect what they are labeling, e.g., Help.show_error_log_menu_item,
			// Help.error_log_dialog_title.
			//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Form d = SupportClass.DialogSupport.CreateDialog(frame, Resources.getString("Help.error_log"));
			d.setLayout(new MigLayout("insets 0"));
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol2;
			temp_scrollablecontrol2 = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol2.AutoScroll = true;
			temp_scrollablecontrol2.Controls.Add(lp);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = temp_scrollablecontrol2;
			d.Controls.Add(temp_Control);
			temp_Control.Dock = new System.Windows.Forms.DockStyle();
			temp_Control.BringToFront();
			
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			d.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			d.Closing += new System.ComponentModel.CancelEventHandler(this.ShowErrorLogAction_Closing_DISPOSE_ON_CLOSE);
			
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			d.pack();
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			SupportClass.SetPropertyAsVirtual(d, "Visible", true);
		}
		private void  ShowErrorLogAction_Closing_DISPOSE_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 2);
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}

#else
	[Serializable]
	public class ModuleManagerWindow : System.Windows.Forms.Form
	{
#if NEVER_DEFINED
 //UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction : SupportClass.ActionSupport
		{
			public AnonymousClassAbstractAction(ModuleManagerWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void InitBlock(ModuleManagerWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ModuleManagerWindow enclosingInstance;
			public ModuleManagerWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}

			}
			private const long serialVersionUID = 1L;

			public override void actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				if (!AbstractLaunchAction.shutDown())
					return;

				//UPGRADE_NOTE: Final was removed from the declaration of 'gp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Prefs gp = Prefs.GlobalPrefs;
				try
				{
					gp.close();
				}
				catch (System.IO.IOException ex)
				{
					WriteErrorDialog.error(ex, gp.File);
				}
				finally
				{
					IOUtils.closeQuietly(gp);
				}

				try
				{
					ModuleManager.Instance.shutDown();
				}
				catch (System.IO.IOException ex)
				{
					ErrorDialog.bug(ex);
				}

				VassalSharp.launch.ModuleManagerWindow.logger.info("Exiting");
				System.Environment.Exit(0);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(SupportClass.ActionSupport shutDownAction, ModuleManagerWindow enclosingInstance)
			{
				InitBlock(shutDownAction, enclosingInstance);
			}
			private void InitBlock(SupportClass.ActionSupport shutDownAction, ModuleManagerWindow enclosingInstance)
			{
				this.shutDownAction = shutDownAction;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable shutDownAction was copied into class AnonymousClassWindowAdapter. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private SupportClass.ActionSupport shutDownAction;
			private ModuleManagerWindow enclosingInstance;
			public ModuleManagerWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}

			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

			public void windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs e)
			{
				e.Cancel = true;
				//UPGRADE_TODO: Method 'javax.swing.AbstractAction.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				shutDownAction.actionPerformed(null);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction1 : SupportClass.ActionSupport
		{
			private void InitBlock(VassalSharp.configure.BooleanConfigurer serverStatusConfig, ModuleManagerWindow enclosingInstance)
			{
				this.serverStatusConfig = serverStatusConfig;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable serverStatusConfig was copied into class AnonymousClassAbstractAction1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.BooleanConfigurer serverStatusConfig;
			private ModuleManagerWindow enclosingInstance;
			public ModuleManagerWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}

			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction1(VassalSharp.configure.BooleanConfigurer serverStatusConfig, ModuleManagerWindow enclosingInstance, System.String Param1) : base(Param1)
			{
				InitBlock(serverStatusConfig, enclosingInstance);
			}
			private const long serialVersionUID = 1L;

			public override void actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.serverStatusView.toggleVisibility();
				serverStatusConfig.setValue((System.Object)(serverStatusConfig.booleanValue() ? false : true));
				if (Enclosing_Instance.serverStatusView.isVisible())
				{
					Enclosing_Instance.DividerLocation = Enclosing_Instance.PreferredDividerLocation;
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction2 : SupportClass.ActionSupport
		{
			private void InitBlock(ModuleManagerWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ModuleManagerWindow enclosingInstance;
			public ModuleManagerWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}

			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction2(ModuleManagerWindow enclosingInstance, System.String Param1) : base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;

			public override void actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.OK_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				if (Dialogs.showConfirmDialog(Enclosing_Instance, Resources.getString("ModuleManager.clear_tilecache_title"), Resources.getString("ModuleManager.clear_tilecache_heading"), Resources.getString("ModuleManager.clear_tilecache_message"), (int)System.Windows.Forms.MessageBoxIcon.Exclamation, (int)System.Windows.Forms.MessageBoxButtons.OKCancel) == (int)System.Windows.Forms.DialogResult.OK)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'tdir '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.IO.FileInfo tdir = new System.IO.FileInfo(Info.ConfDir.FullName + "\\" + "tiles");
					bool tmpBool;
					if (System.IO.File.Exists(tdir.FullName))
						tmpBool = true;
					else
						tmpBool = System.IO.Directory.Exists(tdir.FullName);
					if (tmpBool)
					{
						try
						{
							FileUtils.forceDelete(tdir);
							FileUtils.forceMkdir(tdir);
						}
						catch (System.IO.IOException e)
						{
							WriteErrorDialog.error(e, tdir);
						}
					}
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(ModuleManagerWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void InitBlock(ModuleManagerWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ModuleManagerWindow enclosingInstance;
			public ModuleManagerWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}

			}
			public virtual void propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				setPreferredDividerLocation((System.Int32)e.NewValue);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(int loc, ModuleManagerWindow enclosingInstance)
			{
				InitBlock(loc, enclosingInstance);
			}
			//UPGRADE_NOTE: Delegate might have a different return value and generate a compilation error. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1309'"
			public delegate ~unresolved generatedDelegate();
			private void InitBlock(int loc, ModuleManagerWindow enclosingInstance)
			{
				this.loc = loc;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable loc was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private int loc;
			private ModuleManagerWindow enclosingInstance;
			public ModuleManagerWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}

			}
			public virtual void Run()
			{
				Enclosing_Instance.serverStatusView.Invoke(new generatedDelegate(Enclosing_Instance.serverStatusView.setDividerLocation), new object[] { loc });
			}
		}

		public static ModuleManagerWindow Instance
		{
			get
			{
				return instance;
			}

		}
		virtual public bool WaitCursor
		{
			set
			{
				//UPGRADE_ISSUE: Member 'java.awt.Cursor.getPredefinedCursor' was converted to 'System.Windows.Forms.Cursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
				//UPGRADE_ISSUE: Member 'java.awt.Cursor.WAIT_CURSOR' was converted to 'System.Windows.Forms.Cursors.WaitCursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
				//UPGRADE_ISSUE: Member 'java.awt.Cursor.DEFAULT_CURSOR' was converted to 'System.Windows.Forms.Cursors.Default' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
				Cursor = value ? System.Windows.Forms.Cursors.WaitCursor : System.Windows.Forms.Cursors.Default;
			}

		}
		virtual protected internal int DividerLocation
		{
			set
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'loc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int loc = value;
				//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				IThreadRunnable r = new AnonymousClassRunnable(loc, this);
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.invokeLater(r);
			}

		}
		virtual protected internal int PreferredDividerLocation
		{
			get
			{
				return dividerLocationConfig.getIntValue(500);
			}

			set
			{
				dividerLocationConfig.setValue(value);
			}

		}
		private const long serialVersionUID = 1L;

		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.launch.ModuleManagerWindow'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;

		private const System.String SHOW_STATUS_KEY = "showServerStatus";
		private const System.String DIVIDER_LOCATION_KEY = "moduleManagerDividerLocation";
		private const int COLUMNS = 4;
		private const int KEY_COLUMN = 0;
		private const int VERSION_COLUMN = 1;
		private const int VASSAL_COLUMN = 2;
		private const int SPARE_COLUMN = 3;
		//UPGRADE_NOTE: Final was removed from the declaration of 'columnHeadings '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly System.String[] columnHeadings = new System.String[COLUMNS];

		//UPGRADE_NOTE: Final was removed from the declaration of 'moduleIcon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIcon'"
		private System.Drawing.Image moduleIcon;
		//UPGRADE_NOTE: Final was removed from the declaration of 'activeExtensionIcon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIcon'"
		private System.Drawing.Image activeExtensionIcon;
		//UPGRADE_NOTE: Final was removed from the declaration of 'inactiveExtensionIcon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIcon'"
		private System.Drawing.Image inactiveExtensionIcon;
		//UPGRADE_NOTE: Final was removed from the declaration of 'openGameFolderIcon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIcon'"
		private System.Drawing.Image openGameFolderIcon;
		//UPGRADE_NOTE: Final was removed from the declaration of 'closedGameFolderIcon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIcon'"
		private System.Drawing.Image closedGameFolderIcon;
		//UPGRADE_NOTE: Final was removed from the declaration of 'fileIcon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIcon'"
		private System.Drawing.Image fileIcon;

		private StringArrayConfigurer recentModuleConfig;
		private System.IO.FileInfo selectedModule;

		//UPGRADE_ISSUE: Class 'java.awt.CardLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
		private CardLayout modulePanelLayout;
		private System.Windows.Forms.Panel moduleView;
		private ComponentSplitter.SplitPane serverStatusView;

		private MyTreeNode rootNode;
		private MyTree tree;
		private MyTreeTableModel treeModel;
		private MyTreeNode selectedNode;

		private long lastExpansionTime;
		//UPGRADE_ISSUE: Class 'javax.swing.tree.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
		private TreePath lastExpansionPath;

		private IntConfigurer dividerLocationConfig;

		private static long doubleClickInterval;

		//UPGRADE_NOTE: Final was removed from the declaration of 'instance '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly ModuleManagerWindow instance = new ModuleManagerWindow();

		public ModuleManagerWindow()
		{
			Text = "VASSAL";
			//UPGRADE_ISSUE: Method 'javax.swing.JFrame.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJFramesetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//UPGRADE_TODO: Method 'javax.swing.JFrame.getContentPane' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJFramegetContentPane'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			setLayout(new BoxLayout(((System.Windows.Forms.ContainerControl)this), BoxLayout.X_AXIS));

			ApplicationIcons.setFor(this);

			//UPGRADE_NOTE: Final was removed from the declaration of 'shutDownAction '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SupportClass.ActionSupport shutDownAction = new AnonymousClassAbstractAction(this);
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
			//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
			shutDownAction.putValue(Action.NAME, Resources.getString(Resources.QUIT));

			Closing += new System.ComponentModel.CancelEventHandler(this.ModuleManagerWindow_Closing_DO_NOTHING_ON_CLOSE);
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(shutDownAction, this).windowClosing);

			// setup menubar and actions
			//UPGRADE_NOTE: Final was removed from the declaration of 'mm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuManager mm = MenuManager.Instance;
			//UPGRADE_NOTE: Final was removed from the declaration of 'mb '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuBarProxy mb = mm.getMenuBarProxyFor(this);

			// file menu
			//UPGRADE_NOTE: Final was removed from the declaration of 'fileMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuProxy fileMenu = new MenuProxy(Resources.getString("General.file"));
			fileMenu.setMnemonic(Resources.getString("General.file.shortcut")[0]);

			fileMenu.add(mm.addKey("Main.play_module"));
			fileMenu.add(mm.addKey("Main.edit_module"));
			fileMenu.add(mm.addKey("Main.new_module"));
			fileMenu.add(mm.addKey("Main.import_module"));
			fileMenu.addSeparator();

			if (!SystemUtils.IS_OS_MAC_OSX)
			{
				fileMenu.add(mm.addKey("Prefs.edit_preferences"));
				fileMenu.addSeparator();
				fileMenu.add(mm.addKey("General.quit"));
			}

			// tools menu
			//UPGRADE_NOTE: Final was removed from the declaration of 'toolsMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuProxy toolsMenu = new MenuProxy(Resources.getString("General.tools"));

			// Initialize Global Preferences
			Prefs.GlobalPrefs.Editor.initDialog(this);
			Prefs.initSharedGlobalPrefs();

			//UPGRADE_NOTE: Final was removed from the declaration of 'serverStatusConfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Boolean tempAux = false;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			BooleanConfigurer serverStatusConfig = new BooleanConfigurer(SHOW_STATUS_KEY, null, ref tempAux);
			Prefs.GlobalPrefs.addOption(null, serverStatusConfig);

			dividerLocationConfig = new IntConfigurer(DIVIDER_LOCATION_KEY, null, -10);
			Prefs.GlobalPrefs.addOption(null, dividerLocationConfig);

			toolsMenu.add(new CheckBoxMenuItemProxy(new AnonymousClassAbstractAction1(serverStatusConfig, this, Resources.getString("Chat.server_status")), serverStatusConfig.booleanValue()));

			toolsMenu.add(new MenuItemProxy(new AnonymousClassAbstractAction2(this, Resources.getString("ModuleManager.clear_tilecache"))));

			// help menu
			//UPGRADE_NOTE: Final was removed from the declaration of 'helpMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuProxy helpMenu = new MenuProxy(Resources.getString("General.help"));
			helpMenu.setMnemonic(Resources.getString("General.help.shortcut")[0]);

			helpMenu.add(mm.addKey("General.help"));
			helpMenu.add(mm.addKey("Main.tour"));
			helpMenu.add(mm.addKey("Help.user_guide"));
			helpMenu.addSeparator();
			helpMenu.add(mm.addKey("UpdateCheckAction.update_check"));
			helpMenu.add(mm.addKey("Help.error_log"));

			if (!SystemUtils.IS_OS_MAC_OSX)
			{
				helpMenu.addSeparator();
				helpMenu.add(mm.addKey("AboutScreen.about_vassal"));
			}

			mb.add(fileMenu);
			mb.add(toolsMenu);
			mb.add(helpMenu);

			// add actions
			mm.addAction("Main.play_module", new Player.PromptLaunchAction(this));
			mm.addAction("Main.edit_module", new Editor.PromptLaunchAction(this));
			mm.addAction("Main.new_module", new Editor.NewModuleLaunchAction(this));
			mm.addAction("Main.import_module", new Editor.PromptImportLaunchAction(this));
			mm.addAction("Prefs.edit_preferences", Prefs.GlobalPrefs.Editor.EditAction);
			mm.addAction("General.quit", shutDownAction);

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

			mm.addAction("Main.tour", new LaunchTourAction(this));
			mm.addAction("AboutScreen.about_vassal", new AboutVASSALAction(this));
			mm.addAction("UpdateCheckAction.update_check", new UpdateCheckAction(this));
			mm.addAction("Help.error_log", new ShowErrorLogAction(this));

			Menu = mm.getMenuBarFor(this);

			// Load Icons
			//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			moduleIcon = new ImageIcon(new System.Uri(System.IO.Path.GetFullPath("/images/mm-module.png")));
			//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			activeExtensionIcon = new ImageIcon(new System.Uri(System.IO.Path.GetFullPath("/images/mm-extension-active.png")));
			//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			inactiveExtensionIcon = new ImageIcon(new System.Uri(System.IO.Path.GetFullPath("/images/mm-extension-inactive.png")));
			//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			openGameFolderIcon = new ImageIcon(new System.Uri(System.IO.Path.GetFullPath("/images/mm-gamefolder-open.png")));
			//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			closedGameFolderIcon = new ImageIcon(new System.Uri(System.IO.Path.GetFullPath("/images/mm-gamefolder-closed.png")));
			//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			fileIcon = new ImageIcon(new System.Uri(System.IO.Path.GetFullPath("/images/mm-file.png")));

			// build module controls
			//UPGRADE_NOTE: Final was removed from the declaration of 'moduleControls '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			new BorderLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			System.Windows.Forms.Panel moduleControls = new System.Windows.Forms.Panel();
			//UPGRADE_ISSUE: Constructor 'java.awt.CardLayout.CardLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			modulePanelLayout = new CardLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			moduleView = new System.Windows.Forms.Panel();
			buildTree();
			//UPGRADE_NOTE: Final was removed from the declaration of 'scroll '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Windows.Forms.ScrollableControl scroll = new JScrollPane(tree);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			moduleView.Controls.Add(scroll);
			scroll.Dock = new System.Windows.Forms.DockStyle();
			scroll.BringToFront();

			//UPGRADE_NOTE: Final was removed from the declaration of 'l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'javax.swing.JEditorPane' was converted to 'System.Windows.Forms.RichTextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Windows.Forms.RichTextBox temp_richtextbox;
			//UPGRADE_TODO: Constructor 'javax.swing.JEditorPane.JEditorPane' was converted to 'System.Windows.Forms.RichTextBox.LoadFile' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJEditorPaneJEditorPane_javalangString_javalangString'"
			temp_richtextbox = new System.Windows.Forms.RichTextBox();
			temp_richtextbox.Text = Resources.getString("ModuleManager.quickstart"); ;
			System.Windows.Forms.RichTextBox l = temp_richtextbox;
			l.ReadOnly = !false;

			// Try to get background color and font from LookAndFeel;
			// otherwise, use dummy JLabel to get color and font.
			//UPGRADE_TODO: Method 'javax.swing.UIManager.getColor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			System.Drawing.Color bg = UIManager.getColor("control");
			//UPGRADE_TODO: Method 'javax.swing.UIManager.getFont' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			System.Drawing.Font font = UIManager.getFont("Label.font");

			if (bg.IsEmpty || font == null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'dummy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Label dummy = new System.Windows.Forms.Label();
				if (bg.IsEmpty)
					bg = dummy.BackColor;
				if (font == null)
					font = dummy.Font;
			}

			l.BackColor = bg;
			//UPGRADE_ISSUE: Method 'javax.swing.text.html.HTMLEditorKit.getStyleSheet' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtexthtmlHTMLEditorKit'"
			//UPGRADE_ISSUE: Method 'javax.swing.JEditorPane.getEditorKit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJEditorPanegetEditorKit'"
			//UPGRADE_ISSUE: Class 'javax.swing.text.html.HTMLEditorKit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtexthtmlHTMLEditorKit'"
			SupportClass.StyleSheetSupport.AddStyleSheetRule(((HTMLEditorKit)l.getEditorKit()).getStyleSheet(), "body { font: " + font.FontFamily.Name + " " + (int)font.Size + "pt }");

			//UPGRADE_NOTE: Some methods of the 'javax.swing.event.HyperlinkListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			l.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(BrowserSupport.Listener.hyperlinkUpdate);

			// FIXME: use MigLayout for this!
			// this is necessary to get proper vertical alignment
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Constructor 'java.awt.GridBagLayout.GridBagLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagLayout'"
			new GridBagLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'java.awt.GridBagConstraints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			//UPGRADE_ISSUE: Constructor 'java.awt.GridBagConstraints.GridBagConstraints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			GridBagConstraints c = new GridBagConstraints();
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.fill' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.HORIZONTAL' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.fill = GridBagConstraints.HORIZONTAL;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.anchor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.CENTER' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.anchor = GridBagConstraints.CENTER;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			p.Controls.Add(l);
			l.Dock = new System.Windows.Forms.DockStyle();
			l.BringToFront();

			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			moduleView.Controls.Add(p);
			p.Dock = new System.Windows.Forms.DockStyle();
			p.BringToFront();
			//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			modulePanelLayout.show(moduleView, getModuleCount() == 0 ? "quickStart" : "modules");
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			moduleControls.Controls.Add(moduleView);
			moduleView.Dock = System.Windows.Forms.DockStyle.Fill;
			moduleView.BringToFront();
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.border.TitledBorder.TitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingborderTitledBorder'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(moduleControls.CreateGraphics(), 0, 0, moduleControls.Width, moduleControls.Height, new TitledBorder(Resources.getString("ModuleManager.recent_modules")));

			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(moduleControls);

			// build server status controls
			//UPGRADE_NOTE: Final was removed from the declaration of 'serverStatusControls '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ServerStatusView serverStatusControls = new ServerStatusView(new CgiServerStatus());
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.border.TitledBorder.TitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingborderTitledBorder'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(serverStatusControls.CreateGraphics(), 0, 0, serverStatusControls.Width, serverStatusControls.Height, new TitledBorder(Resources.getString("Chat.server_status")));

			serverStatusView = new ComponentSplitter().splitRight(moduleControls, serverStatusControls, false);
			serverStatusView.revalidate();

			// show the server status controls according to the prefs
			if (serverStatusConfig.booleanValue())
			{
				serverStatusView.showComponent();
			}

			DividerLocation = PreferredDividerLocation;
			serverStatusView.addPropertyChangeListener("dividerLocation", new AnonymousClassPropertyChangeListener(this));

			//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle r = Info.getScreenBounds(this);
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			serverStatusControls.Size = new System.Drawing.Size((int)(r.Width / 3.5), 0);

			//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
			Size = new System.Drawing.Size(3 * r.Width / 4, 3 * r.Height / 4);

			// Save/load the window position and size in prefs
			//UPGRADE_NOTE: Final was removed from the declaration of 'option '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PositionOption option = new PositionOption(PositionOption.key + "ModuleManager", this);
			Prefs.GlobalPrefs.addOption(option);
		}

		protected internal virtual void buildTree()
		{
			recentModuleConfig = new StringArrayConfigurer("RecentModules", null);
			Prefs.GlobalPrefs.addOption(null, recentModuleConfig);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List<String> missingModules = new ArrayList<String>();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List<ModuleInfo> moduleList = new ArrayList<ModuleInfo>();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for (String s: recentModuleConfig.getStringArray())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'module '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ModuleInfo module = new ModuleInfo(s);
				bool tmpBool;
				if (System.IO.File.Exists(module.File.FullName))
					tmpBool = true;
				else
					tmpBool = System.IO.Directory.Exists(module.File.FullName);
				if (tmpBool && module.Valid)
				{
					moduleList.add(module);
				}
				else
				{
					missingModules.add(s);
				}
			}

			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for (String s in missingModules)
			{
				logger.info(Resources.getString("ModuleManager.removing_module", s));
				moduleList.remove(s);
				recentModuleConfig.removeValue(s);
			}

			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Collections.sort(moduleList, new Comparator<ModuleInfo>()
			{
			}
		}
		public virtual int compare(ModuleInfo f1, ModuleInfo f2)
		{
			return f1.compareTo(f2);
		}
		private void ModuleManagerWindow_Closing_DO_NOTHING_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form)sender, 0);
		}
		static ModuleManagerWindow()
		{
			logger = LoggerFactory.getLogger(typeof(ModuleManagerWindow));
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'dci '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDesktopProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
				//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
				System.Object dci = Toolkit.getDefaultToolkit().getDesktopProperty("awt.multiClickInterval");
				doubleClickInterval = dci is System.Int32 ? (System.Int32)dci : 200L;
			}
		}

		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		rootNode = new MyTreeNode(new RootInfo());
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(ModuleInfo moduleInfo: moduleList)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final MyTreeNode moduleNode = new MyTreeNode(moduleInfo);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(ExtensionInfo ext: moduleInfo.getExtensions())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final MyTreeNode extensionNode = new MyTreeNode(ext);
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		moduleNode.add(extensionNode);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}

	final ArrayList<File> missingFolders = new ArrayList<File>();

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
foreach (File f in moduleInfo.getFolders())
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	if (f.exists() && f.isDirectory())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		final GameFolderInfo folderInfo = new GameFolderInfo(f, moduleInfo);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final MyTreeNode folderNode = new MyTreeNode(folderInfo);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	moduleNode.add(folderNode);
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		final ArrayList<File> l = new ArrayList<File>();

	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final File[] files = f.listFiles();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		if (files == null) continue;

		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		for (File f1: files)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		{
			if (f1.isFile())
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			{
				l.add(f1);
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		}
		Collections.sort(l);

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
foreach (File f2 in l)
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	final SaveFileInfo fileInfo = new SaveFileInfo(f2, folderInfo);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if (fileInfo.isValid() && fileInfo.belongsToModule())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		final MyTreeNode fileNode = new MyTreeNode(fileInfo);
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		folderNode.add(fileNode);
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	else
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	missingFolders.add(f);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}

foreach (File mf in missingFolders)
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	logger.info(
	Resources.getString(ModuleManager.removing_folder, mf.getPath()));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	moduleInfo.removeFolder(mf);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}

rootNode.add(moduleNode);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	updateModuleList();

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
treeModel = new MyTreeTableModel(rootNode);
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree = new MyTree(treeModel);

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.setRootVisible(false);
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.setEditable(false);

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.setTreeCellRenderer(new MyTreeCellRenderer());

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.addMouseListener(new MouseAdapter()
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void mouseClicked(MouseEvent e)
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	if (e.getClickCount() == 2)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		final TreePath path =
		tree.getPathForLocation(e.getPoint().x, e.getPoint().y);

		// do nothing if not on a node, or if this node was expanded
		// or collapsed during the past doubleClickInterval milliseconds
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		if (path == null || (lastExpansionPath == path &&
		e.getWhen() - lastExpansionTime <= doubleClickInterval)) return;

		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		selectedNode = (MyTreeNode)path.getLastPathComponent();

		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		final int row = tree.getRowForPath(path);
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		if (row < 0) return;

		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		final AbstractInfo target =
		(AbstractInfo)selectedNode.getUserObject();

		// launch module or load save, otherwise expand or collapse node
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		if (target instanceof ModuleInfo)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	final ModuleInfo modInfo = (ModuleInfo)target;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if (modInfo.isModuleTooNew())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		ErrorDialog.show(
		Error.module_too_new,
		modInfo.getFile().getPath(),
		modInfo.getVassalVersion(),
		Info.getVersion()
		);
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		return;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		((ModuleInfo)target).play();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}
	else if (target instanceof SaveFileInfo)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	((SaveFileInfo)target).play();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}
	else if (tree.isExpanded(row))
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	tree.collapseRow(row);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}
else
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	tree.expandRow(row);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void mouseReleased(MouseEvent e)
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	final TreePath path =
	tree.getPathForLocation(e.getPoint().x, e.getPoint().y);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if (path == null) return;

	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	selectedNode = (MyTreeNode)path.getLastPathComponent();

	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if (e.isMetaDown())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		final int row = tree.getRowForPath(path);
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		if (row >= 0)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		{
			tree.clearSelection();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			tree.addRowSelectionInterval(row, row);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final AbstractInfo target =
			(AbstractInfo)selectedNode.getUserObject();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			target.buildPopup(row).show(tree, e.getX(), e.getY());
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	});

// We capture the time and location of clicks which would cause
// expansion in order to distinguish these from clicks which
// might launch a module or game.
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.addTreeWillExpandListener(new TreeWillExpandListener()
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void treeWillCollapse(TreeExpansionEvent e)
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	lastExpansionTime = System.currentTimeMillis();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	lastExpansionPath = e.getPath();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
public
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
void treeWillExpand(TreeExpansionEvent e)
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	lastExpansionTime = System.currentTimeMillis();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	lastExpansionPath = e.getPath();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	});

// This ensures that double-clicks always start the module but
// doesn't prevent single-clicks on the handles from working.
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.setToggleClickCount(3);

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.setSelectionMode(ListSelectionModel.SINGLE_SELECTION);
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.addTreeSelectionListener(new TreeSelectionListener()
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void valueChanged(TreeSelectionEvent e)
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	final MyTreeNode node = (MyTreeNode)e.getPath().getLastPathComponent();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final AbstractInfo target = node.getNodeInfo();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if (target instanceof ModuleInfo)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	setSelectedModule(target.getFile());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}
	else
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	if (node.getParent() != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		setSelectedModule(node.getParentModuleFile());
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	});

// FIXME: Width handling needs improvement. Also save in prefs
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.getColumnModel().getColumn(KEY_COLUMN).setMinWidth(250);

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.getColumnModel().getColumn(VERSION_COLUMN)
.setCellRenderer(new VersionCellRenderer());
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.getColumnModel().getColumn(VERSION_COLUMN).setMinWidth(100);

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.getColumnModel().getColumn(VASSAL_COLUMN)
.setCellRenderer(new VersionCellRenderer());
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.getColumnModel().getColumn(VASSAL_COLUMN).setMinWidth(100);

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.getColumnModel().getColumn(SPARE_COLUMN).setMinWidth(10);
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.getColumnModel().getColumn(SPARE_COLUMN).setPreferredWidth(600);

// FIXME: How to set alignment of individual header components?
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
tree.getTableHeader().setAlignmentX(JComponent.CENTER_ALIGNMENT);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> A File has been saved or created by the Player or the Editor. Update
	/// the display as necessary.
	/// </summary>
	/// <param name="f">The file
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void update(File f)
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	final AbstractMetaData data = MetaDataFactory.buildMetaData(f);

	// Module.
	// If we already have this module added, just refresh it, otherwise add it in.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if (data instanceof ModuleMetaData)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	final MyTreeNode moduleNode = rootNode.findNode(f);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if (moduleNode == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		addModule(f);
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		moduleNode.refresh();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}

	// Extension.
	// Check to see if it has been saved into one of the extension directories
	// for any module we already know of. Refresh the module
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
else if (data instanceof ExtensionMetaData)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	for (int i = 0;
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
i < rootNode.getChildCount();
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
i++)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		final MyTreeNode moduleNode = rootNode.getChild(i);
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		final ModuleInfo moduleInfo = (ModuleInfo)moduleNode.getNodeInfo();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		for (ExtensionInfo ext: moduleInfo.getExtensions())
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		{
			if (ext.getFile().equals(f))
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			{
				moduleNode.refresh();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				return;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}

	// Save Game or Log file.
	// If the parent of the save file is already recorded as a Game Folder,
	// pass the file off to the Game Folder to handle. Otherwise, ignore it.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
else if (data instanceof SaveMetaData)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	for (int i = 0;
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
i < rootNode.getChildCount();
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
i++)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		final MyTreeNode moduleNode = rootNode.getChild(i);
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		final MyTreeNode folderNode = moduleNode.findNode(f.getParentFile());
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		if (folderNode != null &&
		folderNode.getNodeInfo() instanceof GameFolderInfo)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		((GameFolderInfo)folderNode.getNodeInfo()).update(f);
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		return;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	tree.repaint();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Return the number of Modules added to the Module Manager
	/// 
	/// </summary>
	/// <returns> Number of modules
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private int getModuleCount()
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	return rootNode.getChildCount();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
public File getSelectedModule()
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	return selectedModule;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
private
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
void setSelectedModule(File selectedModule)
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	this.selectedModule = selectedModule;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
public
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
void addModule(File f)
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	if (!rootNode.contains(f))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		final ModuleInfo moduleInfo = new ModuleInfo(f);
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		if (moduleInfo.isValid())
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		{
			final MyTreeNode moduleNode = new MyTreeNode(moduleInfo);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			treeModel.insertNodeInto(moduleNode, rootNode,
			rootNode.findInsertIndex(moduleInfo));
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for (ExtensionInfo ext: moduleInfo.getExtensions())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	final MyTreeNode extensionNode = new MyTreeNode(ext);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	treeModel.insertNodeInto(extensionNode, moduleNode,
	moduleNode.findInsertIndex(ext));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}
updateModuleList();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void removeModule(File f)
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	final MyTreeNode moduleNode = rootNode.findNode(f);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	treeModel.removeNodeFromParent(moduleNode);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	updateModuleList();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
public File getModuleByName(String name)
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	if (name == null) return null;

	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for (int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < rootNode.getChildCount();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i++)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		final ModuleInfo module =
		(ModuleInfo)rootNode.getChild(i).getNodeInfo();

		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		if (name.equals(module.getModuleName())) return module.getFile();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}

	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}

//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
private
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
void updateModuleList()
//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
{
	final List<String> l = new ArrayList<String>();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for (int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < rootNode.getChildCount();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i++)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
		final ModuleInfo module =
		(ModuleInfo)(rootNode.getChild(i)).getNodeInfo();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		l.add(module.encode());
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	recentModuleConfig.setValue(l.toArray(new String[l.size()]));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	modulePanelLayout.show(
	moduleView, getModuleCount() == 0 ? quickStart : modules);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
}

/// <summary>************************************************************************
/// Custom Tree table model:-
/// - Return column count
/// - Return column headings
/// </summary>
private class MyTreeTableModel : DefaultTreeTableModel
{
	virtual public int ColumnCount
	{
		get
		{
			return COLUMNS;
		}

	}
	public MyTreeTableModel(MyTreeNode rootNode) : base(rootNode)
	{
		columnHeadings[KEY_COLUMN] = Resources.getString("ModuleManager.module");
		columnHeadings[VERSION_COLUMN] = Resources.getString("ModuleManager.version");
		columnHeadings[VASSAL_COLUMN] = Resources.getString("ModuleManager.vassal_version");
		columnHeadings[SPARE_COLUMN] = Resources.getString("ModuleManager.description");
	}

	public virtual System.String getColumnName(int col)
	{
		return columnHeadings[col];
	}

	public virtual System.Object getValueAt(System.Object node, int column)
	{
		return ((MyTreeNode)node).getValueAt(column);
	}
}

/// <summary> Custom implementation of JXTreeTable
/// Fix for bug on startup generating illegal column numbers
/// 
/// </summary>
private class MyTree : JXTreeTable
{
	private const long serialVersionUID = 1L;

	public MyTree(MyTreeTableModel treeModel) : base(treeModel)
	{
	}

	// FIXME: Where's the rest of the comment???
	/// <summary> There appears to be a bug/strange interaction between JXTreetable and the ComponentSplitter
	/// when the Component
	/// </summary>
	public virtual System.String getToolTipText(System.Windows.Forms.MouseEventArgs event_Renamed)
	{
		if (getComponentAt(event_Renamed.X, event_Renamed.Y) == null)
			return null;
		return base.getToolTipText(event_Renamed);
	}
}

/// <summary> Custom Tree cell renderer:-
/// - Add file name as tooltip
/// - Handle expanded display (some nodes use the same icon for expanded/unexpanded)
/// - Gray out inactve extensions
/// - Gray out Save Games that belong to other modules
/// </summary>
//UPGRADE_ISSUE: Class 'javax.swing.tree.DefaultTreeCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
[Serializable]
private class MyTreeCellRenderer : DefaultTreeCellRenderer
{
	private const long serialVersionUID = 1L;

	//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
	public System.Windows.Forms.Control getTreeCellRendererComponent(System.Windows.Forms.TreeView tree, System.Object value_Renamed, bool selected, bool expanded, bool leaf, int row, bool hasFocus)
	{
		//UPGRADE_ISSUE: Method 'javax.swing.tree.DefaultTreeCellRenderer.getTreeCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
		base.getTreeCellRendererComponent(tree, value_Renamed, selected, expanded, leaf, row, hasFocus);
		//UPGRADE_NOTE: Final was removed from the declaration of 'info '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		AbstractInfo info = ((MyTreeNode)value_Renamed).NodeInfo;
		Text = info.ToString();
		SupportClass.ToolTipSupport.setToolTipText(this, info.ToolTipText);
		Image = info.getIcon(expanded);
		ForeColor = info.TreeCellFgColor;
		return this;
	}
}

/// <summary>************************************************************************
/// Custom cell render for Version column
/// - Center data
/// </summary>
//UPGRADE_ISSUE: Class 'javax.swing.table.DefaultTableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
[Serializable]
private class VersionCellRenderer : DefaultTableCellRenderer
{
	private const long serialVersionUID = 1L;

	//UPGRADE_ISSUE: Constructor 'javax.swing.table.DefaultTableCellRenderer.DefaultTableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
	public VersionCellRenderer() : base()
	{
		//UPGRADE_TODO: Method 'javax.swing.JLabel.setHorizontalAlignment' was converted to 'System.Windows.Forms.Label.ImageAlign' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJLabelsetHorizontalAlignment_int'"
		this.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
	}

	//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
	public System.Windows.Forms.Control getTableCellRendererComponent(System.Windows.Forms.DataGrid table, System.Object value_Renamed, bool isSelected, bool hasFocus, int row, int column)
	{
		//UPGRADE_ISSUE: Method 'javax.swing.table.DefaultTableCellRenderer.getTableCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableDefaultTableCellRenderer'"
		base.getTableCellRendererComponent(table, value_Renamed, isSelected, hasFocus, row, column);
		return this;
	}
}

/// <summary>************************************************************************
/// Custom TreeTable Node
/// </summary>
private class MyTreeNode : DefaultMutableTreeTableNode
{
	virtual public AbstractInfo NodeInfo
	{
		get
		{
			return (AbstractInfo)getUserObject();
		}

	}
	virtual public System.IO.FileInfo File
	{
		get
		{
			return NodeInfo.File;
		}

	}
	/// <summary> Return the Module node enclosing this node
	/// 
	/// </summary>
	/// <returns> Parent Tree Node
	/// </returns>
	virtual public MyTreeNode ParentModuleNode
	{
		get
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'info '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			AbstractInfo info = NodeInfo;
			if (info is RootInfo)
			{
				return null;
			}
			else if (info is ModuleInfo)
			{
				return this;
			}
			else if ((MyTreeNode)getParent() == null)
			{
				return null;
			}
			else
			{
				return ((MyTreeNode)getParent()).ParentModuleNode;
			}
		}

	}
	/// <summary> Return the Module file of the Module node enclosing this node
	/// 
	/// </summary>
	/// <returns> Module File
	/// </returns>
	virtual public System.IO.FileInfo ParentModuleFile
	{
		get
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'parentNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MyTreeNode parentNode = ParentModuleNode;
			return parentNode == null ? null : parentNode.File;
		}

	}

	public MyTreeNode(AbstractInfo nodeInfo) : base(nodeInfo)
	{
		nodeInfo.TreeNode = this;
	}

	public virtual void refresh()
	{
		NodeInfo.refresh();
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	public virtual void setValueAt(System.Object aValue, int column)
	{
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	public virtual System.Object getValueAt(int column)
	{
		return NodeInfo.getValueAt(column);
	}

	public virtual MyTreeNode getChild(int index)
	{
		return (MyTreeNode)base.getChildAt(index);
	}

	public virtual MyTreeNode findNode(System.IO.FileInfo f)
	{
		for (int i = 0; i < getChildCount(); i++)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'moduleNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MyTreeNode moduleNode = getChild(i);

			// NB: we canonicalize because File.equals() does not
			// always return true when one File is a relative path.
			try
			{
				f = new System.IO.FileInfo(f.FullName);
			}
			catch (System.IO.IOException e)
			{
				f = new System.IO.FileInfo(f.FullName);
			}

			if (f.Equals(moduleNode.NodeInfo.File))
			{
				return moduleNode;
			}
		}
		return null;
	}

	public virtual bool contains(System.IO.FileInfo f)
	{
		return findNode(f) != null;
	}

	public virtual int findInsertIndex(AbstractInfo info)
	{
		for (int i = 0; i < getChildCount(); i++)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'childNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MyTreeNode childNode = getChild(i);
			if (childNode.NodeInfo.compareTo(info) >= 0)
			{
				return i;
			}
		}
		return getChildCount();
	}
}

/// <summary>************************************************************************
/// All tree nodes encapsulate a User-defined object holding the user
/// data for that node. In the ModuleManager, all user-defined objects
/// are subclasses of AbstractInfo
/// </summary>
//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
abstract public class AbstractInfo : System.IComparable
{
	virtual public System.IO.FileInfo File
	{
		get
		{
			return file;
		}

		set
		{
			if (value == null)
				return;

			try
			{
				file = new System.IO.FileInfo(value.FullName);
			}
			catch (System.IO.IOException e)
			{
				file = new System.IO.FileInfo(value.FullName);
			}
		}

	}
	virtual public System.String ToolTipText
	{
		get
		{
			if (file == null)
			{
				return "";
			}
			else
			{
				return file.FullName;
			}
		}

	}
	virtual public bool Valid
	{
		get
		{
			return valid;
		}

		set
		{
			valid = value;
		}

	}
	virtual public System.String Error
	{
		get
		{
			return error;
		}

		set
		{
			error = value;
		}

	}
	virtual public System.String Version
	{
		get
		{
			return "";
		}

	}
	virtual public System.String VassalVersion
	{
		get
		{
			return "";
		}

	}
	virtual public System.String Comments
	{
		get
		{
			return "";
		}

	}
	virtual public MyTreeNode TreeNode
	{
		get
		{
			return node;
		}

		set
		{
			node = value;
		}

	}
	/// <summary> Return a String used to sort different types of AbstractInfo's that are
	/// children of the same parent.
	/// 
	/// </summary>
	/// <returns> sort key
	/// </returns>
	public abstract System.String SortKey { get; }
	/// <summary> Return the color of the text used to display the name in column 1.
	/// Over-ride this to change color depending on item state.
	/// 
	/// </summary>
	/// <returns> cell text color
	/// </returns>
	virtual public System.Drawing.Color TreeCellFgColor
	{
		get
		{
			return System.Drawing.Color.Black;
		}

	}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< AbstractInfo >
		protected internal System.IO.FileInfo file;
	protected internal System.Drawing.Image openIcon;
	protected internal System.Drawing.Image closedIcon;
	protected internal bool valid = true;
	protected internal System.String error = "";
	protected internal MyTreeNode node;

	public AbstractInfo(System.IO.FileInfo f, System.Drawing.Image open, System.Drawing.Image closed)
	{
		File = f;
		setIcon(open, closed);
	}
	public AbstractInfo(System.IO.FileInfo f, System.Drawing.Image i) : this(f, i, i)
	{
	}

	public AbstractInfo(System.IO.FileInfo f) : this(f, null)
	{
	}

	public AbstractInfo()
	{
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	public override System.String ToString()
	{
		return file == null ? "" : file.Name;
	}

	public virtual int compareTo(AbstractInfo info)
	{
		return String.CompareOrdinal(SortKey, info.SortKey);
	}

	//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
	public virtual System.Windows.Forms.ContextMenu buildPopup(int row)
	{
		return null;
	}

	public virtual System.Drawing.Image getIcon(bool expanded)
	{
		return expanded ? openIcon : closedIcon;
	}

	public virtual void setIcon(System.Drawing.Image i)
	{
		setIcon(i, i);
	}

	public virtual void setIcon(System.Drawing.Image open, System.Drawing.Image closed)
	{
		openIcon = open;
		closedIcon = closed;
	}

	public virtual System.String getValueAt(int column)
	{
		switch (column)
		{

			case KEY_COLUMN:
				return ToString();

			case VERSION_COLUMN:
				return Version;

			case VASSAL_COLUMN:
				return VassalVersion;

			default:
				return null;

		}
	}

	/// <summary> Refresh yourself and any children</summary>
	public virtual void refresh()
	{
		refreshChildren();
	}

	public virtual void refreshChildren()
	{
		for (int i = 0; i < node.getChildCount(); i++)
		{
			(node.getChild(i)).refresh();
		}
	}
	//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
	abstract public System.Int32 CompareTo(System.Object obj);
}

/// <summary>************************************************************************
/// Root Node User Information - Root node is hidden, so not much action here.
/// </summary>
private class RootInfo : AbstractInfo
{
	override public System.String SortKey
	{
		get
		{
			return "";
		}

	}
	public RootInfo() : base(null)
	{
	}
	//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
	override public System.Int32 CompareTo(System.Object obj)
	{
		return 0;
	}
}

/// <summary>************************************************************************
/// Module Node User Information
/// </summary>
public class ModuleInfo : AbstractInfo
{
	//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
	[Serializable]
	internal class AnonymousClassAbstractAction : SupportClass.ActionSupport
	{
		private void InitBlock(ModuleInfo enclosingInstance)
		{
			this.enclosingInstance = enclosingInstance;
		}
		private ModuleInfo enclosingInstance;
		public ModuleInfo Enclosing_Instance
		{
			get
			{
				return enclosingInstance;
			}

		}
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		internal AnonymousClassAbstractAction(ModuleInfo enclosingInstance, System.String Param1) : base(Param1)
		{
			InitBlock(enclosingInstance);
		}

		private const long serialVersionUID = 1L;

		public override void actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FileChooser fc = FileChooser.createFileChooser(this, (DirectoryConfigurer)Prefs.GlobalPrefs.getOption(Prefs.MODULES_DIR_KEY));
			if (fc.showOpenDialog() == FileChooser.APPROVE_OPTION)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'selectedFile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.IO.FileInfo selectedFile = fc.SelectedFile;
				//UPGRADE_NOTE: Final was removed from the declaration of 'testExtInfo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ExtensionInfo testExtInfo = new ExtensionInfo(selectedFile, true, null);
				if (testExtInfo.Valid)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.IO.FileInfo f = Enclosing_Instance.ExtensionsManager.setActive(fc.SelectedFile, true);
					//UPGRADE_NOTE: Final was removed from the declaration of 'moduleNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					MyTreeNode moduleNode = rootNode.findNode(selectedModule);
					//UPGRADE_NOTE: Final was removed from the declaration of 'extInfo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					ExtensionInfo extInfo = new ExtensionInfo(f, true, (ModuleInfo)moduleNode.NodeInfo);
					if (extInfo.Valid)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'extNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						MyTreeNode extNode = new MyTreeNode(extInfo);
						treeModel.insertNodeInto(extNode, moduleNode, moduleNode.findInsertIndex(extInfo));
					}
				}
				else
				{
					SupportClass.OptionPaneSupport.ShowMessageDialog(null, testExtInfo.Error, null, (int)System.Windows.Forms.MessageBoxIcon.Error);
				}
			}
		}
	}
	//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
	[Serializable]
	internal class AnonymousClassAbstractAction1 : SupportClass.ActionSupport
	{
		private void InitBlock(ModuleInfo enclosingInstance)
		{
			this.enclosingInstance = enclosingInstance;
		}
		private ModuleInfo enclosingInstance;
		public ModuleInfo Enclosing_Instance
		{
			get
			{
				return enclosingInstance;
			}

		}
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		internal AnonymousClassAbstractAction1(ModuleInfo enclosingInstance, System.String Param1) : base(Param1)
		{
			InitBlock(enclosingInstance);
		}
		private const long serialVersionUID = 1L;

		public override void actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FileChooser fc = FileChooser.createFileChooser(this, (DirectoryConfigurer)Prefs.GlobalPrefs.getOption(Prefs.MODULES_DIR_KEY), FileChooser.DIRECTORIES_ONLY);
			if (fc.showOpenDialog() == FileChooser.APPROVE_OPTION)
			{
				Enclosing_Instance.addFolder(fc.SelectedFile);
			}
		}
	}
	//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
	[Serializable]
	private class AnonymousClassAbstractAction2 : SupportClass.ActionSupport
	{
		private void InitBlock(ModuleInfo enclosingInstance)
		{
			this.enclosingInstance = enclosingInstance;
		}
		private ModuleInfo enclosingInstance;
		public ModuleInfo Enclosing_Instance
		{
			get
			{
				return enclosingInstance;
			}

		}
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		internal AnonymousClassAbstractAction2(ModuleInfo enclosingInstance, System.String Param1) : base(Param1)
		{
			InitBlock(enclosingInstance);
		}
		private const long serialVersionUID = 1L;

		public override void actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			removeModule(Enclosing_Instance.file);
			Enclosing_Instance.cleanupTileCache();
		}
	}
	private void InitBlock()
	{
		newExtensionAction = new NewExtensionLaunchAction(this);
		addExtensionAction = new AnonymousClassAbstractAction(this, Resources.getString("ModuleManager.add_extension"));
		addFolderAction = new AnonymousClassAbstractAction1(this, Resources.getString("ModuleManager.add_save_game_folder"));
		return gameFolders;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		final List<ExtensionInfo> l = new ArrayList<ExtensionInfo>();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		for (File f: extMgr.getActiveExtensions())
		{
			l.add(new ExtensionInfo(f, true, this));
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		for (File f: extMgr.getInactiveExtensions())
		{
			l.add(new ExtensionInfo(f, false, this));
		}
		Collections.sort(l);
		return l;
	}
	virtual protected internal bool ModuleTooNew
	{
		get
		{
			return metadata == null ? false : Info.isModuleTooNew(metadata.VassalVersion);
		}

	}
	override public System.String VassalVersion
	{
		get
		{
			return metadata == null ? "" : metadata.VassalVersion;
		}

	}
	virtual public ExtensionsManager ExtensionsManager
	{
		get
		{
			return extMgr;
		}

	}
	virtual public bool InUse
	{
		/*
		* Is the module currently being Played or Edited?
		*/

		get
		{
			return AbstractLaunchAction.isInUse(file) || AbstractLaunchAction.isEditing(file);
		}

	}
	override public System.String Version
	{
		get
		{
			return metadata.Version;
		}

	}
	virtual public System.String LocalizedDescription
	{
		get
		{
			return metadata.LocalizedDescription;
		}

	}
	virtual public System.String ModuleName
	{
		get
		{
			return metadata.Name;
		}

	}
	override public System.String SortKey
	{
		get
		{
			return metadata == null ? "" : metadata.LocalizedName;
		}

	}
	override public System.Drawing.Color TreeCellFgColor
	{
		get
		{
			return Info.isModuleTooNew(VassalVersion) ? Color.GRAY : Color.BLACK;
		}

	}

	private ExtensionsManager extMgr;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private SortedSet<File> gameFolders = new TreeSet<File>();
	private ModuleMetaData metadata;

	//UPGRADE_NOTE: The initialization of  'newExtensionAction' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
	private SupportClass.ActionSupport newExtensionAction;

	//UPGRADE_NOTE: The initialization of  'addExtensionAction' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
	private SupportClass.ActionSupport addExtensionAction;

	//UPGRADE_NOTE: The initialization of  'addFolderAction' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
	private SupportClass.ActionSupport addFolderAction;

	public ModuleInfo(System.IO.FileInfo f) : base(f, moduleIcon)
	{
		InitBlock();
		extMgr = new ExtensionsManager(f);
		loadMetaData();
	}

	protected internal virtual void loadMetaData()
	{
		AbstractMetaData data = MetaDataFactory.buildMetaData(file);
		if (data != null && data is ModuleMetaData)
		{
			Valid = true;
			metadata = (ModuleMetaData)data;
		}
		else
		{
			Valid = false;
		}
	}

	/// <summary> Initialise ModuleInfo based on a saved preference string.
	/// See encode().
	/// 
	/// </summary>
	/// <param name="s">Preference String
	/// </param>
	public ModuleInfo(System.String s)
	{
		InitBlock();
		SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, ';');
		File = new System.IO.FileInfo(sd.nextToken());
		setIcon(moduleIcon);
		loadMetaData();
		extMgr = new ExtensionsManager(File);
		while (sd.hasMoreTokens())
		{
			gameFolders.add(new System.IO.FileInfo(sd.nextToken()));
		}
	}

	/// <summary> Refresh this module and all children</summary>
	public override void refresh()
	{
		loadMetaData();

		// Remove any missing children
		//UPGRADE_NOTE: Final was removed from the declaration of 'nodes '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		MyTreeNode[] nodes = new MyTreeNode[TreeNode.getChildCount()];
		for (int i = 0; i < TreeNode.getChildCount(); i++)
		{
			nodes[i] = TreeNode.getChild(i);
		}
		for (int i = 0; i < nodes.Length; i++)
		{
			bool tmpBool;
			if (System.IO.File.Exists(nodes[i].File.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(nodes[i].File.FullName);
			if (!tmpBool)
			{
				treeModel.removeNodeFromParent(nodes[i]);
			}
		}

		// Refresh or add any existing children
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		for (ExtensionInfo ext: getExtensions())
		{
			MyTreeNode extNode = TreeNode.findNode(ext.getFile());
			if (extNode == null)
			{
				if (ext.isValid())
				{
					extNode = new MyTreeNode(ext);
					treeModel.insertNodeInto(extNode, TreeNode, TreeNode.findInsertIndex(ext));
				}
			}
			else
			{
				extNode.refresh();
			}
		}
	}

	/// <summary> Encode any information which needs to be recorded in the Preference entry for this module:-
	/// - Path to Module File
	/// - Paths to any child Save Game Folders
	/// 
	/// </summary>
	/// <returns> encoded data
	/// </returns>
	public virtual System.String encode()
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		SequenceEncoder se = new SequenceEncoder(file.FullName, ';');
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		for (File f: gameFolders)
		{
			se.append(f.getPath());
		}
		return se.Value;
	}

	public virtual void addFolder(System.IO.FileInfo f)
	{
		// try to create the directory if it doesn't exist
		bool tmpBool;
		if (System.IO.File.Exists(f.FullName))
			tmpBool = true;
		else
			tmpBool = System.IO.Directory.Exists(f.FullName);
		//UPGRADE_TODO: Method 'java.io.File.mkdirs' was converted to 'System.IO.Directory.CreateDirectory' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFilemkdirs'"
		if (!tmpBool && !System.IO.Directory.CreateDirectory(f.FullName))
		{
			SupportClass.OptionPaneSupport.ShowMessageDialog(this, Resources.getString("Install.error_unable_to_create", f.FullName), "Error", (int)System.Windows.Forms.MessageBoxIcon.Error);

			return;
		}

		gameFolders.add(f);
		//UPGRADE_NOTE: Final was removed from the declaration of 'moduleNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		MyTreeNode moduleNode = rootNode.findNode(selectedModule);
		//UPGRADE_NOTE: Final was removed from the declaration of 'folderInfo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		GameFolderInfo folderInfo = new GameFolderInfo(f, (ModuleInfo)moduleNode.NodeInfo);
		//UPGRADE_NOTE: Final was removed from the declaration of 'folderNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		MyTreeNode folderNode = new MyTreeNode(folderInfo);
		//UPGRADE_NOTE: Final was removed from the declaration of 'idx '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		int idx = moduleNode.findInsertIndex(folderInfo);
		treeModel.insertNodeInto(folderNode, moduleNode, idx);

		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		for (File file: f.listFiles())
		{
			if (System.IO.File.Exists(file.FullName))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fileInfo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SaveFileInfo fileInfo = new SaveFileInfo(file, folderInfo);
				if (fileInfo.Valid && fileInfo.belongsToModule())
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'fileNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					MyTreeNode fileNode = new MyTreeNode(fileInfo);
					treeModel.insertNodeInto(fileNode, folderNode, folderNode.findInsertIndex(fileInfo));
				}
			}
		}
		updateModuleList();
	}

	public virtual void removeFolder(System.IO.FileInfo f)
	{
		gameFolders.remove(f);
	}

	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public SortedSet<File> getFolders()

		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public List<ExtensionInfo> getExtensions()


		public virtual void play()
	{
		new Player.LaunchAction(this, file).actionPerformed(null);
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
	public override System.Windows.Forms.ContextMenu buildPopup(int row)
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		System.Windows.Forms.ContextMenu m = new System.Windows.Forms.ContextMenu();
		//UPGRADE_NOTE: Final was removed from the declaration of 'playAction '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		SupportClass.ActionSupport playAction = new Player.LaunchAction(this, file);
		//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
		playAction.setEnabled(!Info.isModuleTooNew(metadata.VassalVersion));
		System.Windows.Forms.MenuItem temp_MenuItem;
		temp_MenuItem = new System.Windows.Forms.MenuItem();
		temp_MenuItem.Click += new System.EventHandler(playAction.actionPerformed);
		m.MenuItems.Add(temp_MenuItem);
		System.Windows.Forms.MenuItem generatedAux = temp_MenuItem;
		//UPGRADE_NOTE: Final was removed from the declaration of 'editAction '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		SupportClass.ActionSupport editAction = new Editor.ListLaunchAction(this, file);
		//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
		editAction.setEnabled(!Info.isModuleTooNew(metadata.VassalVersion));
		System.Windows.Forms.MenuItem temp_MenuItem2;
		temp_MenuItem2 = new System.Windows.Forms.MenuItem();
		temp_MenuItem2.Click += new System.EventHandler(editAction.actionPerformed);
		m.MenuItems.Add(temp_MenuItem2);
		System.Windows.Forms.MenuItem generatedAux2 = temp_MenuItem2;
		System.Windows.Forms.MenuItem temp_MenuItem3;
		temp_MenuItem3 = new System.Windows.Forms.MenuItem();
		temp_MenuItem3.Click += new System.EventHandler(new AnonymousClassAbstractAction2(this, Resources.getString("General.remove")).actionPerformed);
		m.MenuItems.Add(temp_MenuItem3);
		System.Windows.Forms.MenuItem generatedAux3 = temp_MenuItem3;

		m.MenuItems.Add("-");
		System.Windows.Forms.MenuItem temp_MenuItem4;
		temp_MenuItem4 = new System.Windows.Forms.MenuItem();
		temp_MenuItem4.Click += new System.EventHandler(addFolderAction.actionPerformed);
		m.MenuItems.Add(temp_MenuItem4);
		System.Windows.Forms.MenuItem generatedAux4 = temp_MenuItem4;
		m.MenuItems.Add("-");
		System.Windows.Forms.MenuItem temp_MenuItem5;
		temp_MenuItem5 = new System.Windows.Forms.MenuItem();
		temp_MenuItem5.Click += new System.EventHandler(newExtensionAction.actionPerformed);
		m.MenuItems.Add(temp_MenuItem5);
		System.Windows.Forms.MenuItem generatedAux5 = temp_MenuItem5;
		System.Windows.Forms.MenuItem temp_MenuItem6;
		temp_MenuItem6 = new System.Windows.Forms.MenuItem();
		temp_MenuItem6.Click += new System.EventHandler(addExtensionAction.actionPerformed);
		m.MenuItems.Add(temp_MenuItem6);
		System.Windows.Forms.MenuItem generatedAux6 = temp_MenuItem6;
		return m;
	}

	public virtual void cleanupTileCache()
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'hstr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		System.String hstr = DigestUtils.shaHex(metadata.Name + "_" + metadata.Version);

		//UPGRADE_NOTE: Final was removed from the declaration of 'tdir '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		System.IO.FileInfo tdir = new System.IO.FileInfo(Info.ConfDir.FullName + "\\" + "tiles/" + hstr);
		bool tmpBool;
		if (System.IO.File.Exists(tdir.FullName))
			tmpBool = true;
		else
			tmpBool = System.IO.Directory.Exists(tdir.FullName);
		if (tmpBool)
		{
			try
			{
				FileUtils.forceDelete(tdir);
			}
			catch (System.IO.IOException e)
			{
				WriteErrorDialog.error(e, tdir);
			}
		}
	}
	public override System.String ToString()
	{
		return metadata.LocalizedName;
	}
	public override System.String getValueAt(int column)
	{
		return column == SPARE_COLUMN ? LocalizedDescription : base.getValueAt(column);
	}
	//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
	override public System.Int32 CompareTo(System.Object obj)
	{
		return 0;
	}
}

/// <summary>************************************************************************
/// Extension Node User Information
/// </summary>
private class ExtensionInfo : AbstractInfo
{
	virtual public bool Active
	{
		get
		{
			return active;
		}

		set
		{
			active = value;
			setIcon(active ? activeExtensionIcon : inactiveExtensionIcon);
		}

	}
	override public System.String Version
	{
		get
		{
			return metadata == null ? "" : metadata.Version;
		}

	}
	override public System.String VassalVersion
	{
		get
		{
			return metadata == null ? "" : metadata.VassalVersion;
		}

	}
	virtual public System.String Description
	{
		get
		{
			return metadata == null ? "" : metadata.getDescription();
		}

	}
	virtual public ExtensionsManager ExtensionsManager
	{
		get
		{
			return moduleInfo == null ? null : moduleInfo.ExtensionsManager;
		}

	}
	override public System.Drawing.Color TreeCellFgColor
	{
		get
		{
			// FIXME: should get colors from LAF
			if (Active)
			{
				return metadata == null ? System.Drawing.Color.Red : System.Drawing.Color.Black;
			}
			else
			{
				return metadata == null ? System.Drawing.Color.Pink : System.Drawing.Color.Gray;
			}
		}

	}
	virtual public bool InUse
	{
		/*
		* Is the extension, or its owning module currently being Played or Edited?
		*/

		get
		{
			return AbstractLaunchAction.isInUse(file) || AbstractLaunchAction.isEditing(file);
		}

	}
	/// <summary> Sort Extensions by File Name</summary>
	override public System.String SortKey
	{
		get
		{
			return File.Name;
		}

	}

	private bool active;
	private ModuleInfo moduleInfo;
	private ExtensionMetaData metadata;

	public ExtensionInfo(System.IO.FileInfo file, bool active, ModuleInfo module) : base(file, active ? activeExtensionIcon : inactiveExtensionIcon)
	{
		this.active = active;
		moduleInfo = module;
		loadMetaData();
	}

	protected internal virtual void loadMetaData()
	{
		AbstractMetaData data = MetaDataFactory.buildMetaData(file);
		if (data != null && data is ExtensionMetaData)
		{
			Valid = true;
			metadata = (ExtensionMetaData)data;
		}
		else
		{
			Error = Resources.getString("ModuleManager.invalid_extension");
			Valid = false;
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	public override void refresh()
	{
		loadMetaData();
		Active = ExtensionsManager.isExtensionActive(File);
		tree.repaint();
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	public override System.String ToString()
	{
		System.String s = File.Name;
		System.String st = "";
		if (metadata == null)
		{
			st = Resources.getString("ModuleManager.invalid");
		}
		if (!active)
		{
			st += (st.Length > 0 ? "," : "");
			st += Resources.getString("ModuleManager.inactive");
		}
		if (st.Length > 0)
		{
			s += (" (" + st + ")");
		}

		return s;
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
	public override System.Windows.Forms.ContextMenu buildPopup(int row)
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		System.Windows.Forms.ContextMenu m = new System.Windows.Forms.ContextMenu();
		System.Windows.Forms.MenuItem temp_MenuItem;
		temp_MenuItem = new System.Windows.Forms.MenuItem();
		temp_MenuItem.Click += new System.EventHandler(new ActivateExtensionAction(this, Resources.getString(Active ? "ModuleManager.deactivate" : "ModuleManager.activate")).actionPerformed);
		m.MenuItems.Add(temp_MenuItem);
		System.Windows.Forms.MenuItem generatedAux = temp_MenuItem;

		//UPGRADE_NOTE: Final was removed from the declaration of 'editAction '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		SupportClass.ActionSupport editAction = new EditExtensionLaunchAction(this, File, getSelectedModule());
		//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
		editAction.setEnabled(!Info.isModuleTooNew(metadata.VassalVersion));
		System.Windows.Forms.MenuItem temp_MenuItem2;
		temp_MenuItem2 = new System.Windows.Forms.MenuItem();
		temp_MenuItem2.Click += new System.EventHandler(editAction.actionPerformed);
		m.MenuItems.Add(temp_MenuItem2);
		System.Windows.Forms.MenuItem generatedAux2 = temp_MenuItem2;
		return m;
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	public override System.String getValueAt(int column)
	{
		return column == SPARE_COLUMN ? Description : base.getValueAt(column);
	}

	//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ActivateExtensionAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
	[Serializable]
	private class ActivateExtensionAction : SupportClass.ActionSupport
	{
		private void InitBlock(ExtensionInfo enclosingInstance)
		{
			this.enclosingInstance = enclosingInstance;
		}
		private ExtensionInfo enclosingInstance;
		public ExtensionInfo Enclosing_Instance
		{
			get
			{
				return enclosingInstance;
			}

		}
		private const long serialVersionUID = 1L;

		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		public ActivateExtensionAction(ExtensionInfo enclosingInstance, System.String s) : base(s)
		{
			InitBlock(enclosingInstance);
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
			setEnabled(!Enclosing_Instance.InUse && !Enclosing_Instance.moduleInfo.InUse);
		}

		public override void actionPerformed(System.Object event_sender, System.EventArgs evt)
		{
			Enclosing_Instance.File = Enclosing_Instance.ExtensionsManager.setActive(Enclosing_Instance.File, !Enclosing_Instance.Active);
			Enclosing_Instance.Active = Enclosing_Instance.ExtensionsManager.isExtensionActive(Enclosing_Instance.File);
			//UPGRADE_NOTE: Final was removed from the declaration of 'path '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'javax.swing.tree.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
			TreePath path = tree.getPathForRow(tree.getSelectedRow());
			//UPGRADE_NOTE: Final was removed from the declaration of 'extNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'javax.swing.tree.TreePath.getLastPathComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
			MyTreeNode extNode = (MyTreeNode)path.getLastPathComponent();
			treeModel.setValueAt("", extNode, 0);
		}
	}
	//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
	override public System.Int32 CompareTo(System.Object obj)
	{
		return 0;
	}
}

/// <summary>************************************************************************
/// Saved Game Folder Node User Information
/// </summary>
private class GameFolderInfo : AbstractInfo
{
	//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
	[Serializable]
	private class AnonymousClassAbstractAction : SupportClass.ActionSupport
	{
		private void InitBlock(GameFolderInfo enclosingInstance)
		{
			this.enclosingInstance = enclosingInstance;
		}
		private GameFolderInfo enclosingInstance;
		public GameFolderInfo Enclosing_Instance
		{
			get
			{
				return enclosingInstance;
			}

		}
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		internal AnonymousClassAbstractAction(GameFolderInfo enclosingInstance, System.String Param1) : base(Param1)
		{
			InitBlock(enclosingInstance);
		}
		private const long serialVersionUID = 1L;

		public override void actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			Enclosing_Instance.refresh();
		}
	}
	//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
	[Serializable]
	private class AnonymousClassAbstractAction1 : SupportClass.ActionSupport
	{
		private void InitBlock(GameFolderInfo enclosingInstance)
		{
			this.enclosingInstance = enclosingInstance;
		}
		private GameFolderInfo enclosingInstance;
		public GameFolderInfo Enclosing_Instance
		{
			get
			{
				return enclosingInstance;
			}

		}
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		internal AnonymousClassAbstractAction1(GameFolderInfo enclosingInstance, System.String Param1) : base(Param1)
		{
			InitBlock(enclosingInstance);
		}
		private const long serialVersionUID = 1L;

		public override void actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'moduleNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MyTreeNode moduleNode = rootNode.findNode(Enclosing_Instance.moduleInfo.File);
			//UPGRADE_NOTE: Final was removed from the declaration of 'folderNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MyTreeNode folderNode = moduleNode.findNode(Enclosing_Instance.File);
			treeModel.removeNodeFromParent(folderNode);
			Enclosing_Instance.moduleInfo.removeFolder(Enclosing_Instance.File);
			updateModuleList();
		}
	}
	virtual public ModuleInfo ModuleInfo
	{
		get
		{
			return moduleInfo;
		}

	}
	/// <summary> Force Game Folders to sort after extensions</summary>
	override public System.String SortKey
	{
		get
		{
			return "~~~" + File.Name;
		}

	}
	protected internal System.String comment;
	protected internal ModuleInfo moduleInfo;
	protected internal long dtm;

	public GameFolderInfo(System.IO.FileInfo f, ModuleInfo m) : base(f, openGameFolderIcon, closedGameFolderIcon)
	{
		moduleInfo = m;
		//UPGRADE_TODO: The equivalent in .NET for method 'java.io.File.lastModified' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
		dtm = ((f.LastWriteTime.Ticks - 621355968000000000) / 10000);
	}

	//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
	public override System.Windows.Forms.ContextMenu buildPopup(int row)
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		System.Windows.Forms.ContextMenu m = new System.Windows.Forms.ContextMenu();
		System.Windows.Forms.MenuItem temp_MenuItem;
		temp_MenuItem = new System.Windows.Forms.MenuItem();
		temp_MenuItem.Click += new System.EventHandler(new AnonymousClassAbstractAction(this, Resources.getString("General.refresh")).actionPerformed);
		m.MenuItems.Add(temp_MenuItem);
		System.Windows.Forms.MenuItem generatedAux = temp_MenuItem;
		m.MenuItems.Add("-");
		System.Windows.Forms.MenuItem temp_MenuItem2;
		temp_MenuItem2 = new System.Windows.Forms.MenuItem();
		temp_MenuItem2.Click += new System.EventHandler(new AnonymousClassAbstractAction1(this, Resources.getString("General.remove")).actionPerformed);
		m.MenuItems.Add(temp_MenuItem2);
		System.Windows.Forms.MenuItem generatedAux2 = temp_MenuItem2;

		return m;
	}

	public override void refresh()
	{

		// Remove any files that no longer exist
		for (int i = TreeNode.getChildCount() - 1; i >= 0; i--)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'fileNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MyTreeNode fileNode = TreeNode.getChild(i);
			//UPGRADE_NOTE: Final was removed from the declaration of 'fileInfo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SaveFileInfo fileInfo = (SaveFileInfo)fileNode.NodeInfo;
			bool tmpBool;
			if (System.IO.File.Exists(fileInfo.File.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(fileInfo.File.FullName);
			if (!tmpBool)
			{
				treeModel.removeNodeFromParent(fileNode);
			}
		}

		// Refresh any that are. Only include Save files belonging to this
		// module, or that are pre vassal 3.1
		//UPGRADE_NOTE: Final was removed from the declaration of 'files '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		System.IO.FileInfo[] files = SupportClass.FileSupport.GetFiles(File);
		if (files == null)
			return;

		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		for (File f: files)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'fdata '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			AbstractMetaData fdata = MetaDataFactory.buildMetaData(f);
			if (fdata != null)
			{
				if (fdata is SaveMetaData)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'moduleName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String moduleName = ((SaveMetaData)fdata).ModuleName;
					if (moduleName == null || moduleName.Length == 0 || moduleName.Equals(ModuleInfo.ModuleName))
					{
						update(f);
					}
				}
			}
		}
	}

	/// <summary> Update the display for the specified save File, or add it in if
	/// we don't already know about it.
	/// </summary>
	/// <param name="f">
	/// </param>
	public virtual void update(System.IO.FileInfo f)
	{
		for (int i = 0; i < TreeNode.getChildCount(); i++)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'fileInfo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SaveFileInfo fileInfo = (SaveFileInfo)(TreeNode.getChild(i)).NodeInfo;
			if (fileInfo.File.Equals(f))
			{
				fileInfo.refresh();
				return;
			}
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'fileInfo '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		SaveFileInfo fileInfo2 = new SaveFileInfo(f, this);
		//UPGRADE_NOTE: Final was removed from the declaration of 'fileNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		MyTreeNode fileNode = new MyTreeNode(fileInfo2);
		treeModel.insertNodeInto(fileNode, TreeNode, TreeNode.findInsertIndex(fileInfo2));
	}
	//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
	override public System.Int32 CompareTo(System.Object obj)
	{
		return 0;
	}
}

/// <summary>************************************************************************
/// Saved Game File Node User Information
/// </summary>
private class SaveFileInfo : AbstractInfo
{
	virtual protected internal System.IO.FileInfo ModuleFile
	{
		get
		{
			return folderInfo.ModuleInfo.File;
		}

	}
	override public System.Drawing.Color TreeCellFgColor
	{
		get
		{
			// FIXME: should get colors from LAF
			return belongsToModule() ? System.Drawing.Color.Black : System.Drawing.Color.Gray;
		}

	}
	override public System.String Version
	{
		get
		{
			return metadata == null ? "" : metadata.ModuleVersion;
		}

	}
	/// <summary> Sort Save Files by file name</summary>
	override public System.String SortKey
	{
		get
		{
			return this.File.Name;
		}

	}

	protected internal GameFolderInfo folderInfo; // Owning Folder
	protected internal SaveMetaData metadata; // Save file metadata

	public SaveFileInfo(System.IO.FileInfo f, GameFolderInfo folder) : base(f, fileIcon)
	{
		folderInfo = folder;
		loadMetaData();
	}

	protected internal virtual void loadMetaData()
	{
		AbstractMetaData data = MetaDataFactory.buildMetaData(file);
		if (data != null && data is SaveMetaData)
		{
			metadata = (SaveMetaData)data;
			Valid = true;
		}
		else
		{
			Valid = false;
		}
	}

	public override void refresh()
	{
		loadMetaData();
		tree.repaint();
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
	public override System.Windows.Forms.ContextMenu buildPopup(int row)
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		System.Windows.Forms.ContextMenu m = new System.Windows.Forms.ContextMenu();
		System.Windows.Forms.MenuItem temp_MenuItem;
		temp_MenuItem = new System.Windows.Forms.MenuItem();
		temp_MenuItem.Click += new System.EventHandler(new Player.LaunchAction(this, ModuleFile, file).actionPerformed);
		m.MenuItems.Add(temp_MenuItem);
		System.Windows.Forms.MenuItem generatedAux = temp_MenuItem;
		return m;
	}

	public virtual void play()
	{
		new Player.LaunchAction(this, ModuleFile, file).actionPerformed(null);
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	public override System.String getValueAt(int column)
	{
		return column == SPARE_COLUMN ? buildComments() : base.getValueAt(column);
	}

	private System.String buildComments()
	{
		System.String comments = "";
		if (!belongsToModule())
		{
			if (metadata != null && metadata.ModuleName.Length > 0)
			{
				comments = "[" + metadata.ModuleName + "] ";
			}
		}
		comments += (metadata == null ? "" : metadata.getDescription());
		return comments;
	}

	private bool belongsToModule()
	{
		return metadata != null && (metadata.ModuleName.Length == 0 || folderInfo.ModuleInfo.ModuleName.Equals(metadata.ModuleName));
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
	override public System.Int32 CompareTo(System.Object obj)
	{
		return 0;
	}
}

/// <summary> Action to create a New Extension and edit it in another process.</summary>
[Serializable]
private class NewExtensionLaunchAction : AbstractLaunchAction
{
	//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassLaunchTask' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
	private class AnonymousClassLaunchTask : LaunchTask
	{
		public AnonymousClassLaunchTask(NewExtensionLaunchAction enclosingInstance) : base(enclosingInstance)
		{
			InitBlock(enclosingInstance);
		}
		private void InitBlock(NewExtensionLaunchAction enclosingInstance)
		{
			this.enclosingInstance = enclosingInstance;
		}
		private NewExtensionLaunchAction enclosingInstance;
		public new NewExtensionLaunchAction Enclosing_Instance
		{
			get
			{
				return enclosingInstance;
			}

		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

		protected internal override void done()
		{
			base.done();

			// reduce the using count
			System.Int32 count = using_Renamed.get_Renamed(lr.module);
			if ((System.Object)count == 1)
				using_Renamed.remove(lr.module);
			else
				using_Renamed.put(lr.module, --count);
		}
	}
	private const long serialVersionUID = 1L;

	//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
	public NewExtensionLaunchAction(System.Windows.Forms.Form frame) : base(Resources.getString("ModuleManager.new_extension"), frame, typeof(Editor).FullName, new LaunchRequest(LaunchRequest.Mode.NEW_EXT))
	{
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	public override void actionPerformed(System.Object event_sender, System.EventArgs e)
	{
		lr.module = getSelectedModule();

		// register that this module is being used
		if (editing.contains(lr.module))
			return;
		System.Int32 count = using_Renamed.get_Renamed(lr.module);
		//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
		using_Renamed.put(lr.module, count == null ? 1 : ++count);

		base.actionPerformed(event_sender, e);
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	protected internal override LaunchTask getLaunchTask()
	{
		return new AnonymousClassLaunchTask(this);
	}
}

/// <summary> Action to Edit an Extension in another process</summary>
[Serializable]
private class EditExtensionLaunchAction : AbstractLaunchAction
{
	//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassLaunchTask' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
	private class AnonymousClassLaunchTask : LaunchTask
	{
		public AnonymousClassLaunchTask(EditExtensionLaunchAction enclosingInstance) : base(enclosingInstance)
		{
			InitBlock(enclosingInstance);
		}
		private void InitBlock(EditExtensionLaunchAction enclosingInstance)
		{
			this.enclosingInstance = enclosingInstance;
		}
		private EditExtensionLaunchAction enclosingInstance;
		public new EditExtensionLaunchAction Enclosing_Instance
		{
			get
			{
				return enclosingInstance;
			}

		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

		protected internal override void done()
		{
			base.done();

			// reduce the using count for module
			System.Int32 count = using_Renamed.get_Renamed(lr.module);
			if ((System.Object)count == 1)
				using_Renamed.remove(lr.module);
			else
				using_Renamed.put(lr.module, --count);

			// reduce that this extension is done being edited
			editing.remove(lr.extension);
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
			Enclosing_Instance.setEnabled(true);
		}
	}
	private const long serialVersionUID = 1L;

	//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
	public EditExtensionLaunchAction(System.Windows.Forms.Form frame, System.IO.FileInfo extension, System.IO.FileInfo module) : base(Resources.getString("Editor.edit_extension"), frame, typeof(Editor).FullName, new LaunchRequest(LaunchRequest.Mode.EDIT_EXT, module, extension))
	{

		//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
		setEnabled(!using_Renamed.containsKey(module) && !editing.contains(module) && !editing.contains(extension) && !using_Renamed.containsKey(extension));
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	public override void actionPerformed(System.Object event_sender, System.EventArgs e)
	{
		// check that neither this module nor this extension is being edited
		if (editing.contains(lr.module) || editing.contains(lr.extension))
			return;

		// register that this module is being used
		System.Int32 count = using_Renamed.get_Renamed(lr.module);
		//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
		using_Renamed.put(lr.module, count == null ? 1 : ++count);

		// register that this extension is being edited
		editing.add(lr.extension);

		base.actionPerformed(event_sender, e);
		//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
		setEnabled(false);
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	protected internal override void addFileFilters(FileChooser fc)
	{
		fc.addChoosableFileFilter(new ModuleExtensionFileFilter());
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

	protected internal override LaunchTask getLaunchTask()
	{
		return new AnonymousClassLaunchTask(this);
	}
}

[Serializable]
private class ShowErrorLogAction : SupportClass.ActionSupport
{
	private const long serialVersionUID = 1L;

	//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
	private System.Windows.Forms.Form frame;

	//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
	//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
	public ShowErrorLogAction(System.Windows.Forms.Form frame) : base(Resources.getString("Help.error_log"))
	{
		this.frame = frame;
	}

	public override void actionPerformed(System.Object event_sender, System.EventArgs e)
	{
		// FIXME: don't create a new one each time!
		//UPGRADE_NOTE: Final was removed from the declaration of 'logfile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		System.IO.FileInfo logfile = new System.IO.FileInfo(Info.HomeDir.FullName + "\\" + "errorLog");
		//UPGRADE_NOTE: Final was removed from the declaration of 'lp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		LogPane lp = new LogPane(logfile);

		// FIXME: this should have its own key. Probably keys should be renamed
		// to reflect what they are labeling, e.g., Help.show_error_log_menu_item,
		// Help.error_log_dialog_title.
		//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		System.Windows.Forms.Form d = SupportClass.DialogSupport.CreateDialog(frame, Resources.getString("Help.error_log"));
		d.setLayout(new MigLayout("insets 0"));
		//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
		System.Windows.Forms.ScrollableControl temp_scrollablecontrol2;
		temp_scrollablecontrol2 = new System.Windows.Forms.ScrollableControl();
		temp_scrollablecontrol2.AutoScroll = true;
		temp_scrollablecontrol2.Controls.Add(lp);
		//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
		System.Windows.Forms.Control temp_Control;
		temp_Control = temp_scrollablecontrol2;
		d.Controls.Add(temp_Control);
		temp_Control.Dock = new System.Windows.Forms.DockStyle();
		temp_Control.BringToFront();

		//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
		d.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		d.Closing += new System.ComponentModel.CancelEventHandler(this.ShowErrorLogAction_Closing_DISPOSE_ON_CLOSE);

		//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
		d.pack();
		//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
		//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
		//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
		SupportClass.SetPropertyAsVirtual(d, "Visible", true);
	}
	private void ShowErrorLogAction_Closing_DISPOSE_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs e)
	{
		e.Cancel = true;
		SupportClass.CloseOperation((System.Windows.Forms.Form)sender, 2);
	}
#endif
}
#endif
	}