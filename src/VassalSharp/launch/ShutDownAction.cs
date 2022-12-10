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
using Resources = VassalSharp.i18n.Resources;
using Prefs = VassalSharp.preferences.Prefs;
using WriteErrorDialog = VassalSharp.tools.WriteErrorDialog;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.launch
{
	
	[Serializable]
	public class ShutDownAction:SupportClass.ActionSupport
	{
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		public ShutDownAction():base(Resources.getString(Resources.QUIT))
		{
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			if (GameModule.getGameModule() == null)
			{
				Prefs p = null;
				try
				{
					p = Prefs.GlobalPrefs;
					p.write();
					p.close();
				}
				catch (System.IO.IOException ex)
				{
					WriteErrorDialog.error(ex, Prefs.GlobalPrefs.File.FullName);
				}
				finally
				{
					IOUtils.closeQuietly(p);
				}
				
				System.Environment.Exit(0);
			}
			else if (GameModule.getGameModule().shutDown())
			{
				System.Environment.Exit(0);
			}
		}
	}
}