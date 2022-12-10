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
using System.Collections.Generic;

namespace VassalSharp.tools
{
	
	/// <summary> Displays an image centered on the screen</summary>
	[Serializable]
	public class SplashScreen:System.Windows.Forms.Form
	{
		static private System.Int32 state766;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMouseAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassMouseAdapter
		{
			public AnonymousClassMouseAdapter(SplashScreen enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SplashScreen enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SplashScreen enclosingInstance;
			public SplashScreen Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs evt)
			{
				Enclosing_Instance.Visible = false;
			}
		}
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state766 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
		//UPGRADE_TODO: More than one of the Java class members are converted to this same member in .NET. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1231'"
		new virtual public bool Visible
		{
			get
			{
				return base.Visible;
			}
			
			set
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				base.Visible = value;
				if (value)
				{
					BringToFront();
				}
			}
			
		}
		private const long serialVersionUID = 1L;
		
		private static List < SplashScreen > instances = new List < SplashScreen >();
		
		public SplashScreen(System.Drawing.Image im)
		{
			SupportClass.WindowSupport.SetWindow(this);
			instances.Add(this);
			System.Windows.Forms.Label temp_label2;
			temp_label2 = new System.Windows.Forms.Label();
			temp_label2.Image = (System.Drawing.Image) im.Clone();
			System.Windows.Forms.Control temp_Control;
			temp_Control = temp_label2;
			Controls.Add(temp_Control);
			this.PerformLayout();

			System.Drawing.Size d = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
			Location = new System.Drawing.Point(d.Width / 2 - Size.Width / 2, d.Height / 2 - Size.Height / 2);
			MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.tools.SplashScreen.mouseDown);
			MouseUp += new System.Windows.Forms.MouseEventHandler(new AnonymousClassMouseAdapter(this).mouseReleased);
		}
		
		public static void  sendAllToBack()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			foreach (SplashScreen s in instances)
			{
				s.SendToBack();
			}
		}
		
		public static void  disposeAll()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			foreach (SplashScreen s in instances)
			{
				s.Dispose();
			}
		}
	}
}