/*
* $Id$
*
* Copyright (c) 2008 by Joel Uckelman
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
//UPGRADE_TODO: The type 'com.apple.eawt.Application' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Application = com.apple.eawt.Application;
//UPGRADE_TODO: The type 'com.apple.eawt.ApplicationAdapter' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ApplicationAdapter = com.apple.eawt.ApplicationAdapter;
//UPGRADE_TODO: The type 'com.apple.eawt.ApplicationEvent' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ApplicationEvent = com.apple.eawt.ApplicationEvent;
namespace VassalSharp.tools.menu
{
	
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class MacOSXMenuManager:MenuManager
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassApplicationAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassApplicationAdapter : ApplicationAdapter
		{
			public AnonymousClassApplicationAdapter(SupportClass.ActionSupport action, MacOSXMenuManager enclosingInstance)
			{
				InitBlock(action, enclosingInstance);
			}
			private void  InitBlock(SupportClass.ActionSupport action, MacOSXMenuManager enclosingInstance)
			{
				this.action = action;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable action was copied into class AnonymousClassApplicationAdapter. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private SupportClass.ActionSupport action;
			private MacOSXMenuManager enclosingInstance;
			public MacOSXMenuManager Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public virtual void  handleQuit(ApplicationEvent e)
			{
				e.setHandled(false);
				//UPGRADE_TODO: Method 'java.awt.event.ActionListener.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				action.actionPerformed(null);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassApplicationAdapter1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassApplicationAdapter1 : ApplicationAdapter
		{
			public AnonymousClassApplicationAdapter1(SupportClass.ActionSupport action, MacOSXMenuManager enclosingInstance)
			{
				InitBlock(action, enclosingInstance);
			}
			private void  InitBlock(SupportClass.ActionSupport action, MacOSXMenuManager enclosingInstance)
			{
				this.action = action;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable action was copied into class AnonymousClassApplicationAdapter1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private SupportClass.ActionSupport action;
			private MacOSXMenuManager enclosingInstance;
			public MacOSXMenuManager Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public virtual void  handlePreferences(ApplicationEvent e)
			{
				e.setHandled(true);
				//UPGRADE_TODO: Method 'java.awt.event.ActionListener.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				action.actionPerformed(null);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(Application app, MacOSXMenuManager enclosingInstance)
			{
				InitBlock(app, enclosingInstance);
			}
			private void  InitBlock(Application app, MacOSXMenuManager enclosingInstance)
			{
				this.app = app;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable app was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private Application app;
			private MacOSXMenuManager enclosingInstance;
			public MacOSXMenuManager Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				if ("enabled".Equals(e.PropertyName))
				{
					app.setEnabledPreferencesMenu((System.Boolean) e.NewValue);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassApplicationAdapter2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassApplicationAdapter2 : ApplicationAdapter
		{
			public AnonymousClassApplicationAdapter2(SupportClass.ActionSupport action, MacOSXMenuManager enclosingInstance)
			{
				InitBlock(action, enclosingInstance);
			}
			private void  InitBlock(SupportClass.ActionSupport action, MacOSXMenuManager enclosingInstance)
			{
				this.action = action;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable action was copied into class AnonymousClassApplicationAdapter2. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private SupportClass.ActionSupport action;
			private MacOSXMenuManager enclosingInstance;
			public MacOSXMenuManager Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public virtual void  handleAbout(ApplicationEvent e)
			{
				e.setHandled(true);
				//UPGRADE_TODO: Method 'java.awt.event.ActionListener.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				action.actionPerformed(null);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener1
		{
			public AnonymousClassPropertyChangeListener1(Application app, MacOSXMenuManager enclosingInstance)
			{
				InitBlock(app, enclosingInstance);
			}
			private void  InitBlock(Application app, MacOSXMenuManager enclosingInstance)
			{
				this.app = app;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable app was copied into class AnonymousClassPropertyChangeListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private Application app;
			private MacOSXMenuManager enclosingInstance;
			public MacOSXMenuManager Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				if ("enabled".Equals(e.PropertyName))
				{
					app.setEnabledAboutMenu((System.Boolean) e.NewValue);
				}
			}
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'menuBar '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private MenuBarProxy menuBar = new MenuBarProxy();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.Windows.Forms.MainMenu getMenuBarFor(System.Windows.Forms.Form fc)
		{
			return menuBar.createPeer();
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override MenuBarProxy getMenuBarProxyFor(System.Windows.Forms.Form fc)
		{
			return menuBar;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override MenuItemProxy addKey(System.String key)
		{
			// don't reserve slots for Quit, Preferences, or About on Macs
			if ("General.quit".Equals(key) || "Prefs.edit_preferences".Equals(key) || "AboutScreen.about_vassal".Equals(key))
				return null;
			
			return base.addKey(key);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override void  addAction(System.String key, SupportClass.ActionSupport action)
		{
			// Quit, Preferences, and About go on the special application menu
			
			if ("General.quit".Equals(key))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'app '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Application app = Application.getApplication();
				app.addApplicationListener(new AnonymousClassApplicationAdapter(action, this));
				
				// no need to track enabled state, quit is always active
			}
			else if ("Prefs.edit_preferences".Equals(key))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'app '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Application app = Application.getApplication();
				app.addApplicationListener(new AnonymousClassApplicationAdapter1(action, this));
				
				app.addPreferencesMenuItem();
				//UPGRADE_ISSUE: Method 'javax.swing.Action.isEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionisEnabled'"
				app.setEnabledPreferencesMenu(action.isEnabled());
				
				// track the enabled state of the prefs action
				//UPGRADE_ISSUE: Method 'javax.swing.Action.addPropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionaddPropertyChangeListener_javabeansPropertyChangeListener'"
				action.addPropertyChangeListener(new AnonymousClassPropertyChangeListener(app, this));
			}
			else if ("AboutScreen.about_vassal".Equals(key))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'app '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Application app = Application.getApplication();
				app.addApplicationListener(new AnonymousClassApplicationAdapter2(action, this));
				
				app.addAboutMenuItem();
				//UPGRADE_ISSUE: Method 'javax.swing.Action.isEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionisEnabled'"
				app.setEnabledAboutMenu(action.isEnabled());
				
				// track the enabled state of the prefs action
				//UPGRADE_ISSUE: Method 'javax.swing.Action.addPropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionaddPropertyChangeListener_javabeansPropertyChangeListener'"
				action.addPropertyChangeListener(new AnonymousClassPropertyChangeListener1(app, this));
			}
			else
			{
				// this is not one of the special actions
				base.addAction(key, action);
			}
		}
	}
}