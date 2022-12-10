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
using Documentation = VassalSharp.build.module.Documentation;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.launch
{
	
	[Serializable]
	public class LaunchTourAction:AbstractLaunchAction
	{
		private const long serialVersionUID = 1L;
		
		public LaunchTourAction(System.Windows.Forms.Form window):base(Resources.getString("Main.tour"), window, typeof(Player).FullName, new LaunchRequest(LaunchRequest.Mode.LOAD, new System.IO.FileInfo(Documentation.DocumentationBaseDir.FullName + "\\" + "tour.mod"), new System.IO.FileInfo(Documentation.DocumentationBaseDir.FullName + "\\" + "tour.log")))
		{
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override LaunchTask getLaunchTask()
		{
			return new LaunchTask(this);
		}
	}
}