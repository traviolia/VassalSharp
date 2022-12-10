/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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

// FIXME: Why is this in configure instead of build.module.documentation?
using System;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using HelpWindow = VassalSharp.build.module.documentation.HelpWindow;
using Resources = VassalSharp.i18n.Resources;
using BrowserSupport = VassalSharp.tools.BrowserSupport;
namespace VassalSharp.configure
{
	
	/// <summary> Action that displays a {@link HelpWindow}</summary>
	[Serializable]
	public class ShowHelpAction:SupportClass.ActionSupport
	{
		private const long serialVersionUID = 1L;
		
		private HelpWindow helpWindow;
		private System.Uri contents;
		
		public ShowHelpAction(System.Uri contents, System.Uri iconURL):this((HelpWindow) null, contents, iconURL)
		{
		}
		
		public ShowHelpAction(System.String key, System.Uri contents, System.Uri iconURL):this(contents, iconURL)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
			//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
			putValue(Action.NAME, Resources.getString(key));
		}
		
		public ShowHelpAction(HelpWindow helpWindow, HelpFile contents, System.Uri iconURL):this(helpWindow, contents == null?null:contents.Contents, iconURL)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionsetEnabled_boolean'"
			setEnabled(contents != null);
		}
		
		public ShowHelpAction(HelpWindow helpWindow, System.Uri contents, System.Uri iconURL)
		{
			this.helpWindow = helpWindow;
			this.contents = contents;
			if (iconURL != null)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.SMALL_ICON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionSMALL_ICON_f'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
				putValue(Action.SMALL_ICON, new ImageIcon(iconURL));
			}
			
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
			//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
			putValue(Action.NAME, Resources.getString(Resources.HELP));
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			if (contents != null)
			{
				if (helpWindow == null)
				{
					BrowserSupport.openURL(contents.ToString());
				}
				else
				{
					helpWindow.update(contents);
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					helpWindow.Visible = true;
				}
			}
		}
	}
}