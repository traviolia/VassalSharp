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
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using WarningDialog = VassalSharp.tools.WarningDialog;
namespace VassalSharp.tools.version
{
	
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	[Serializable]
	public class UpdateCheckAction:SupportClass.ActionSupport
	{
		private const long serialVersionUID = 1L;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.tools.version.UpdateCheckAction'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'frame '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		private System.Windows.Forms.Form frame;
		
		//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public UpdateCheckAction(System.Windows.Forms.Form frame):base(Resources.getString("UpdateCheckAction.update_check"))
		{
			this.frame = frame;
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			new Request(this).execute();
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Request' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class Request:AbstractUpdateCheckRequest
		{
			public Request(UpdateCheckAction enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(UpdateCheckAction enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private UpdateCheckAction enclosingInstance;
			public UpdateCheckAction Enclosing_Instance
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
				try
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'update '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					VassalVersion update = get_Renamed();
					if (update == null)
					{
						// running version is current
						SupportClass.OptionPaneSupport.ShowMessageDialog(Enclosing_Instance.frame, Resources.getString("UpdateCheckAction.version_current_message"), Resources.getString("UpdateCheckAction.version_current_title"), (int) System.Windows.Forms.MessageBoxIcon.Information);
					}
					else
					{
						// running version is obsolete
						//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.YES_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						if (SupportClass.OptionPaneSupport.ShowConfirmDialog(Enclosing_Instance.frame, Resources.getString("UpdateCheckAction.update_available_message"), Resources.getString("UpdateCheckAction.update_available_title"), (int) System.Windows.Forms.MessageBoxButtons.YesNo, (int) System.Windows.Forms.MessageBoxIcon.Question) == (int) System.Windows.Forms.DialogResult.Yes)
						{
							BrowserSupport.openURL("https://sourceforge.net/project/showfiles.php?group_id=90612");
						}
					}
					return ;
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
					ErrorDialog.bug(e);
				}
				catch (ExecutionException e)
				{
					VassalSharp.tools.version.UpdateCheckAction.logger.error("", e);
				}
				
				WarningDialog.show(Enclosing_Instance.frame, "UpdateCheckAction.check_failed");
			}
		}
		static UpdateCheckAction()
		{
			logger = LoggerFactory.getLogger(typeof(UpdateCheckAction));
		}
	}
}