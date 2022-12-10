/*
* $Id$
*
* Copyright (c) 2008-2009 Brent Easton
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
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.tools
{
	
	/// <summary> Produce standard Vassal buttons.
	/// 
	/// </summary>
	public class ButtonFactory
	{
		public static System.Windows.Forms.Button OkButton
		{
			get
			{
				System.Windows.Forms.Button button = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.OK));
				if (button is VassalSharp.tools.LaunchButton)
					((VassalSharp.tools.LaunchButton) button).setToolTipText(Resources.getString(Resources.OK));
				else
					SupportClass.ToolTipSupport.setToolTipText(button, Resources.getString(Resources.OK));
				return button;
			}
			
		}
		public static System.Windows.Forms.Button CancelButton
		{
			get
			{
				System.Windows.Forms.Button button = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.CANCEL));
				if (button is VassalSharp.tools.LaunchButton)
					((VassalSharp.tools.LaunchButton) button).setToolTipText(Resources.getString(Resources.CANCEL));
				else
					SupportClass.ToolTipSupport.setToolTipText(button, Resources.getString(Resources.CANCEL));
				return button;
			}
			
		}
		
		public static System.Windows.Forms.Button getHelpButton()
		{
			System.Windows.Forms.Button button = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.HELP));
			if (button is VassalSharp.tools.LaunchButton)
				((VassalSharp.tools.LaunchButton) button).setToolTipText(Resources.getString(Resources.HELP));
			else
				SupportClass.ToolTipSupport.setToolTipText(button, Resources.getString(Resources.HELP));
			return button;
		}
		
		public static System.Windows.Forms.Button getHelpButton(SupportClass.ActionSupport a)
		{
			System.Windows.Forms.Button button = getHelpButton();
			button.Click += new System.EventHandler(a.actionPerformed);
			button.Text = a.Description;
			button.Image = a.Icon;
			return button;
		}
	}
}