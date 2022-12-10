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
using System;
namespace VassalSharp.preferences
{
	
	/// <summary> A Preferences option controlling the visibility of a window</summary>
	public class VisibilityOption:PositionOption
	{
		override public System.String ValueString
		{
			get
			{
				return base.ValueString + "\t" + isVisible;
			}
			
		}
		private bool isVisible = true;
		public VisibilityOption(System.String key, System.Windows.Forms.Form f):base(key, f)
		{
			//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
			isVisible = f.Visible;
		}
		
		public override void  componentShown(System.Object event_sender, System.EventArgs e)
		{
			isVisible = true;
		}
		
		public override void  componentHidden(System.Object event_sender, System.EventArgs e)
		{
			isVisible = false;
		}
		
		public override void  setValue(System.String in_Renamed)
		{
			SupportClass.Tokenizer st = new SupportClass.Tokenizer(in_Renamed, "\t");
			base.setValue(st.NextToken());
			if (st.HasMoreTokens())
			{
				isVisible = "true".Equals(st.NextToken());
			}
			else
			{
				isVisible = true;
			}
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			SupportClass.SetPropertyAsVirtual(theFrame, "Visible", isVisible);
		}
	}
}