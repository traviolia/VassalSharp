/*
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
using ErrorDialog = VassalSharp.tools.ErrorDialog;
namespace VassalSharp.launch
{
	
	/// <deprecated> Use {@link Editor}, {@link Player}, and {@link ModuleManager}
	/// as entry points for VASSAL instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class Main
	{
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_ISSUE: Method 'java.lang.System.setProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
			System_Renamed.setProperty("swing.aatext", "true"); //$NON-NLS-1$ //$NON-NLS-2$
			//UPGRADE_ISSUE: Method 'java.lang.System.setProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
			System_Renamed.setProperty("swing.boldMetal", "false"); //$NON-NLS-1$ //$NON-NLS-2$
			//UPGRADE_ISSUE: Method 'java.lang.System.setProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
			System_Renamed.setProperty("awt.useSystemAAFontSettings", "on"); //$NON-NLS-1$ //$NON-NLS-2$
			
			ErrorDialog.show(null, null, "Obsolete Entry Point", "The Entry Point VassalSharp.launch.Main is Obsolete", "You have attempted to start VASSAL from the VassalSharp.launch.Main entry point. This entry point is no longer current. The current entry points are VassalSharp.launch.ModuleManager, VassalSharp.launch.Player, and VassalSharp.launch.Editor.\n\nIf this message makes no sense to you, or you were trying to load a module, please ask for help at the VASSAL Forum.");
		}
	}
}