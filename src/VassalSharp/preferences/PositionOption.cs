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
using BadDataReport = VassalSharp.build.BadDataReport;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
namespace VassalSharp.preferences
{
	
	public class PositionOption:VassalSharp.configure.Configurer
	{
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				return null;
			}
			
		}
		override public System.String ValueString
		{
			get
			{
				return bounds.X + "," + bounds.Y + "," + bounds.Width + "," + bounds.Height;
			}
			
		}
		new public static System.String key = "BoundsOf";
		private static System.Drawing.Point initialPos = new System.Drawing.Point(0, 0);
		
		protected internal System.Windows.Forms.Form theFrame;
		protected internal System.Drawing.Rectangle bounds;
		protected internal System.Drawing.Rectangle defaultValue;
		protected internal System.Drawing.Rectangle previousBounds;
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public PositionOption(System.String key, System.Windows.Forms.Form f, ref System.Drawing.Rectangle defaultValue):base(key, null, defaultValue)
		{
			adjustInitialOffset();
			theFrame = f;
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			theFrame.pack();
			setFrameBounds();
			this.defaultValue = defaultValue;
			//UPGRADE_WARNING: Extra logic should be included into componentHidden to know if the Component is hidden. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1144'"
			theFrame.VisibleChanged += new System.EventHandler(this.componentHidden);
			theFrame.Move += new System.EventHandler(this.componentMoved);
			theFrame.Resize += new System.EventHandler(this.componentResized);
			//UPGRADE_WARNING: Extra logic should be included into componentShown to know if the Component is visible. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1145'"
			theFrame.VisibleChanged += new System.EventHandler(this.componentShown);
		}
		
		private static void  adjustInitialOffset()
		{
			//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
			Toolkit.getDefaultToolkit();
			System.Drawing.Size d = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
			if (initialPos.X >= d.Width - 30 || initialPos.Y >= d.Height - 30)
			{
				initialPos = new System.Drawing.Point(0, 0);
			}
			else
			{
				initialPos.Offset(30, 30);
			}
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public PositionOption(System.String key, System.Windows.Forms.Form f):this(key, f, ref tempAux)
		{
		}
		
		public override System.Object getValue()
		{
			return bounds;
		}
		
		public override void  setValue(System.Object o)
		{
			if (o is System.Drawing.Rectangle)
			{
				System.Drawing.Rectangle temp_Rectangle;
				temp_Rectangle = (System.Drawing.Rectangle) o;
				bounds = new System.Drawing.Rectangle(temp_Rectangle.Location, temp_Rectangle.Size);
				if (theFrame != null)
				{
					setFrameBounds();
				}
			}
			base.setValue(o);
		}
		
		public override void  setValue(System.String in_Renamed)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SupportClass.Tokenizer st = new SupportClass.Tokenizer(in_Renamed, ",");
			try
			{
				setValue(new System.Drawing.Rectangle(System.Int32.Parse(st.NextToken()), System.Int32.Parse(st.NextToken()), System.Int32.Parse(st.NextToken()), System.Int32.Parse(st.NextToken())));
			}
			catch (System.FormatException e)
			{
				// This can happen if a VisibilityOption has the same name
				// as a PositionOption, either currently, or due to editing.
				// Don't throw a bug, just log it.
				if (in_Renamed.IndexOf('\t') > 0)
				{
					ErrorDialog.dataError(new BadDataReport("Map or Chart window with same name as piece Palette", Key, e));
				}
				else
				{
					ErrorDialog.bug(e);
				}
			}
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		private bool isOnScreen(ref System.Drawing.Point p)
		{
			//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
			Toolkit.getDefaultToolkit();
			//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
			Toolkit.getDefaultToolkit();
			return p.X < System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width && p.Y < System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height;
		}
		
		public virtual void  componentMoved(System.Object event_sender, System.EventArgs e)
		{
			if (theFrame.Visible)
			{
				System.Windows.Forms.Control temp_Control;
				//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.awt.Component.getLocationOnScreen' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
				temp_Control = theFrame;
				System.Drawing.Point p = temp_Control.PointToScreen(temp_Control.Location);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				if (isOnScreen(ref p))
				{
					// Save the previous size in case this is the start of a Maximize
					System.Drawing.Rectangle temp_Rectangle;
					temp_Rectangle = bounds;
					previousBounds = new System.Drawing.Rectangle(temp_Rectangle.Location, temp_Rectangle.Size);
					bounds.Location = p;
				}
			}
		}
		
		public virtual void  componentResized(System.Object event_sender, System.EventArgs e)
		{
			if (theFrame.Visible)
			{
				// A resize when the window is already maximised only happens when
				// a window is first resized. Record the pre-maximised bounds.
				//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
				if (theFrame is System.Windows.Forms.Form && !previousBounds.IsEmpty && ((((System.Windows.Forms.Form) theFrame).getExtendedState() & Frame.MAXIMIZED_BOTH) == Frame.MAXIMIZED_BOTH))
				{
					SupportClass.RectangleSupport.SetBoundsRectangle(ref bounds, previousBounds);
				}
				else
				{
					bounds.Size = theFrame.Size;
				}
			}
		}
		
		public virtual void  componentShown(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  componentHidden(System.Object event_sender, System.EventArgs e)
		{
		}
		
		protected internal virtual void  setFrameBounds()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'desktopBounds '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.awt.GraphicsEnvironment.getLocalGraphicsEnvironment' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphicsEnvironment'"
			System.Drawing.Rectangle desktopBounds = GraphicsEnvironment.getLocalGraphicsEnvironment().getMaximumWindowBounds();
			
			// Respect any existing bounds
			if (bounds.Width != 0 && bounds.Height != 0)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_javaawtDimension'"
				theFrame.Size = new System.Drawing.Size(System.Math.Abs(bounds.Width), System.Math.Abs(bounds.Height));
			}
			//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_javaawtPoint'"
			theFrame.Location = bounds.Location;
			
			// Reduce size to fit on desktop
			int width = theFrame.Size.Width > desktopBounds.Width?desktopBounds.Width:theFrame.Size.Width;
			int height = theFrame.Size.Height > desktopBounds.Height?desktopBounds.Height:theFrame.Size.Height;
			if (width != theFrame.Size.Width || height != theFrame.Size.Height)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
				theFrame.Size = new System.Drawing.Size(width, height);
			}
			
			// Slide whole window onto desktop if any part off desktop
			int x = theFrame.Location.X;
			int y = theFrame.Location.Y;
			
			if (x < desktopBounds.X)
				x = desktopBounds.X;
			if (y < desktopBounds.Y)
				y = desktopBounds.Y;
			
			if (x + theFrame.Size.Width > desktopBounds.X + desktopBounds.Width)
			{
				x = (desktopBounds.X + desktopBounds.Width) - theFrame.Size.Width;
			}
			if (y + theFrame.Size.Height > desktopBounds.Y + desktopBounds.Height)
			{
				y = (desktopBounds.Y + desktopBounds.Height) - theFrame.Size.Height;
			}
			if (x != theFrame.Location.X || y != theFrame.Location.Y)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_int_int'"
				theFrame.Location = new System.Drawing.Point(x, y);
			}
		}
		private static System.Drawing.Rectangle tempAux = new System.Drawing.Rectangle(initialPos, new System.Drawing.Size(0, 0));
	}
}