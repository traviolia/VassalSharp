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
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutionException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutionException = java.util.concurrent.ExecutionException;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using Resources = VassalSharp.i18n.Resources;
using BrowserSupport = VassalSharp.tools.BrowserSupport;
using AbstractUpdateCheckRequest = VassalSharp.tools.version.AbstractUpdateCheckRequest;
using VassalVersion = VassalSharp.tools.version.VassalVersion;
namespace VassalSharp.launch
{
	
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class UpdateCheckRequest:AbstractUpdateCheckRequest
	{
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.launch.UpdateCheckRequest'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override void  done()
		{
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'update '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				VassalVersion update = get_Renamed();
				if (update != null)
				{
					// running version is obsolete
					//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.YES_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					if (SupportClass.OptionPaneSupport.ShowConfirmDialog(ModuleManagerWindow.Instance, Resources.getString("UpdateCheckAction.update_available_message"), Resources.getString("UpdateCheckAction.update_available_title"), (int) System.Windows.Forms.MessageBoxButtons.YesNo, (int) System.Windows.Forms.MessageBoxIcon.Question) == (int) System.Windows.Forms.DialogResult.Yes)
					{
						BrowserSupport.openURL("https://sourceforge.net/project/showfiles.php?group_id=90612");
					}
				}
			}
			catch (System.Threading.ThreadInterruptedException e)
			{
				logger.error("", e);
			}
			catch (ExecutionException e)
			{
				logger.error("", e);
			}
		}
		static UpdateCheckRequest()
		{
			logger = LoggerFactory.getLogger(typeof(UpdateCheckRequest));
		}
	}
}