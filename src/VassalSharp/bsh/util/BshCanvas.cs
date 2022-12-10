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
	
	/// <summary>Scriptable Canvas with buffered graphics.
	/// Provides a Component that:
	/// 1) delegates calls to paint() to a bsh method called paint() 
	/// in a specific NameSpace.
	/// 2) provides a simple buffered image maintained by built in paint() that 
	/// is useful for simple immediate procedural rendering from scripts...  
	/// </summary>
	[Serializable]
	public class BshCanvas:System.Windows.Forms.Control
	{
		/// <summary>Get a buffered (persistent) image for drawing on this component</summary>
		virtual public System.Drawing.Graphics BufferedGraphics
		{
			get
			{
				System.Drawing.Size dim = Size;
				imageBuffer = new System.Drawing.Bitmap(dim.Width, dim.Height);
				return System.Drawing.Graphics.FromImage(imageBuffer);
			}
			
		}
		internal This ths;
		internal System.Drawing.Image imageBuffer;
		
		public BshCanvas()
		{
		}
		
		public BshCanvas(This ths)
		{
			this.ths = ths;
		}
		
		protected override void  OnPaint(System.Windows.Forms.PaintEventArgs g_EventArg)
		{
			System.Drawing.Graphics g = null;
			if (g_EventArg != null)
				g = g_EventArg.Graphics;
			// copy buffered image
			if (imageBuffer != null)
			{
				//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				g.DrawImage(imageBuffer, 0, 0);
			}
			
			// Delegate call to scripted paint() method
			if (ths != null)
			{
				try
				{
					ths.invokeMethod("paint", new System.Object[]{g});
				}
				catch (EvalError e)
				{
					if (Interpreter.DEBUG)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						Interpreter.debug("BshCanvas: method invocation error:" + e);
					}
				}
			}
		}
		
		//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
		new virtual public  void  SetBounds(int x, int y, int width, int height)
		{
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			Size = new System.Drawing.Size(width, height);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMinimumSize_javaawtDimension'"
			setMinimumSize(new System.Drawing.Size(width, height));
			base.SetBounds(x, y, width, height);
		}
	}
}