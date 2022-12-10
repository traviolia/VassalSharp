/// <summary>**************************************************************************
/// *
/// This file is part of the BeanShell Java Scripting distribution.          *
/// Documentation and updates may be found at http://www.beanshell.org/      *
/// *
/// Sun Public License Notice:                                               *
/// *
/// The contents of this file are subject to the Sun Public License Version  *
/// 1.0 (the "License"); you may not use this file except in compliance with *
/// the License. A copy of the License is available at http://www.sun.com    * 
/// *
/// The Original Code is BeanShell. The Initial Developer of the Original    *
/// Code is Pat Niemeyer. Portions created by Pat Niemeyer are Copyright     *
/// (C) 2000.  All Rights Reserved.                                          *
/// *
/// GNU Public License Notice:                                               *
/// *
/// Alternatively, the contents of this file may be used under the terms of  *
/// the GNU Lesser General Public License (the "LGPL"), in which case the    *
/// provisions of LGPL are applicable instead of those above. If you wish to *
/// allow use of your version of this file only under the  terms of the LGPL *
/// and not to allow others to use your version of this file under the SPL,  *
/// indicate your decision by deleting the provisions above and replace      *
/// them with the notice and other provisions required by the LGPL.  If you  *
/// do not delete the provisions above, a recipient may use your version of  *
/// this file under either the SPL or the LGPL.                              *
/// *
/// Patrick Niemeyer (pat@pat.net)                                           *
/// Author of Learning Java, O'Reilly & Associates                           *
/// http://www.pat.net/~pat/                                                 *
/// *
/// ***************************************************************************
/// </summary>
using System;
using bsh;
namespace bsh.util
{
	
	
	/// <summary>Misc utilities for the bsh.util package.
	/// Nothing in the core language (bsh package) should depend on this.
	/// Note: that promise is currently broken... fix it.
	/// </summary>
	public class Util
	{
		/*
		public static ConsoleInterface makeConsole() {
		if ( bsh.Capabilities.haveSwing() )
		return new JConsole();
		else
		return new AWTConsole();
		}
		*/
		
		internal static System.Windows.Forms.Form splashScreen;
		/*
		This could live in the desktop script.
		However we'd like to get it on the screen as quickly as possible.
		*/
		public static void  startSplashScreen()
		{
			int width = 275, height = 148;
			//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
			System.Windows.Forms.Form temp_Form;
			temp_Form = new System.Windows.Forms.Form();
			temp_Form.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			temp_Form.Owner = new System.Windows.Forms.Form();
			temp_Form.ShowInTaskbar = false;
			System.Windows.Forms.Form win = temp_Form;
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			win.pack();
			BshCanvas can = new BshCanvas();
			//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
			can.Size = new System.Drawing.Size(width, height); // why is this necessary?
			//UPGRADE_ISSUE: Class 'java.awt.Toolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
			//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
			Toolkit tk = Toolkit.getDefaultToolkit();
			System.Drawing.Size dim = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
			win.SetBounds(dim.Width / 2 - width / 2, dim.Height / 2 - height / 2, width, height);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
			win.Controls.Add(can);
			//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getImage' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			System.Drawing.Image img = tk.getImage(new System.Uri(System.IO.Path.GetFullPath("/bsh/util/lib/splash.gif")));
			//UPGRADE_ISSUE: Class 'java.awt.MediaTracker' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtMediaTracker'"
			//UPGRADE_ISSUE: Constructor 'java.awt.MediaTracker.MediaTracker' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtMediaTracker'"
			MediaTracker mt = new MediaTracker(can);
			//UPGRADE_ISSUE: Method 'java.awt.MediaTracker.addImage' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtMediaTracker'"
			mt.addImage(img, 0);
			try
			{
				//UPGRADE_ISSUE: Method 'java.awt.MediaTracker.waitForAll' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtMediaTracker'"
				mt.waitForAll();
			}
			catch (System.Exception e)
			{
			}
			System.Drawing.Graphics gr = can.BufferedGraphics;
			//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
			gr.DrawImage(img, 0, 0);
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			SupportClass.SetPropertyAsVirtual(win, "Visible", true);
			win.BringToFront();
			splashScreen = win;
		}
		
		public static void  endSplashScreen()
		{
			if (splashScreen != null)
				splashScreen.Dispose();
		}
	}
}