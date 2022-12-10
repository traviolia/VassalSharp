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
namespace VassalSharp.launch
{
	
	public class ModuleManagerMacOSXStartUp:MacOSXStartUp
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassApplicationAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassApplicationAdapter : ApplicationAdapter
		{
			public AnonymousClassApplicationAdapter(ModuleManagerMacOSXStartUp enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ModuleManagerMacOSXStartUp enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ModuleManagerMacOSXStartUp enclosingInstance;
			public ModuleManagerMacOSXStartUp Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public virtual void  handleOpenFile(ApplicationEvent e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'filename '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String filename = e.getFilename();
				if (filename.EndsWith(".vmod"))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'lr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					LaunchRequest lr = new LaunchRequest();
					lr.mode = LaunchRequest.Mode.LOAD;
					lr.module = new System.IO.FileInfo(filename);
					ModuleManager.Instance.execute(lr);
					e.setHandled(true);
				}
				else
				{
					e.setHandled(false);
				}
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public virtual void  handleReOpenApplication(ApplicationEvent e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'lr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				LaunchRequest lr = new LaunchRequest();
				lr.mode = LaunchRequest.Mode.MANAGE;
				ModuleManager.Instance.execute(lr);
				e.setHandled(true);
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override void  initSystemProperties()
		{
			base.initSystemProperties();
			setupApplicationListeners();
		}
		
		protected internal virtual void  setupApplicationListeners()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'app '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Application app = Application.getApplication();
			app.addApplicationListener(new AnonymousClassApplicationAdapter(this));
		}
	}
}