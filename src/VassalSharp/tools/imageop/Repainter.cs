/*
* $Id$
*
* Copyright (c) 2007 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.util.concurrent.CancellationException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using CancellationException = java.util.concurrent.CancellationException;
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutionException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutionException = java.util.concurrent.ExecutionException;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
//UPGRADE_TODO: The type 'VassalSharp.tools.opcache.Op' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Op = VassalSharp.tools.opcache.Op;
namespace VassalSharp.tools.imageop
{
	
	/// <summary> An <code>ImageOpObserver</code> which repaints {@link Component}s.
	/// This class stores a reference to a <code>Component</code> and a
	/// rectangle of that component which will be repainted when
	/// {@link #imageOpChange} is called and <code>success</code> is true.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class Repainter : ImageOpObserver
	{
		private void  InitBlock()
		{
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint_int_int_int_int'"
			c.Refresh();
			ErrorDialog.bug(e);
			ErrorDialog.bug(e);
			if (!VassalSharp.tools.imageop.Op.handleException(e))
			{
				ErrorDialog.bug(e);
			}
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.Windows.Forms.Control c;
		//UPGRADE_NOTE: Final was removed from the declaration of 'x '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int x;
		//UPGRADE_NOTE: Final was removed from the declaration of 'y '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int y;
		//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int w;
		//UPGRADE_NOTE: Final was removed from the declaration of 'h '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int h;
		
		/// <summary> Creates a <code>Repainter</code> for the specified component and
		/// rectangle. <code>Repainter</code>s will usually be constructed
		/// during paint operations on the Event Dispatch Thread, and so will be
		/// created in enormous quantities. In order to minimize object creation,
		/// always use this constructor rather
		/// {@link #Repainter(Component c, Rectangle r)}, except in the case where
		/// the <code>Rectangle</code> already exists.
		/// 
		/// </summary>
		/// <param name="c">the component to repaint
		/// </param>
		/// <param name="x">the x coordinate of the upper-left corner of the
		/// rectangle to repaint
		/// </param>
		/// <param name="y">the y coordinate of the upper-left corner of the
		/// rectangle to repaint
		/// </param>
		/// <param name="w">the width of the rectangle to repaint
		/// </param>
		/// <param name="h">the height of the rectangle to repaint
		/// 
		/// </param>
		/// <throws>  IllegalArgumentException if <code>c == null</code> </throws>
		public Repainter(System.Windows.Forms.Control c, int x, int y, int w, int h)
		{
			InitBlock();
			if (c == null)
				throw new System.ArgumentException();
			
			this.c = c;
			this.x = x;
			this.y = y;
			this.w = w;
			this.h = h;
		}
		
		/// <summary> Creates a <code>Repainter</code> for the specified component and area.
		/// 
		/// </summary>
		/// <param name="c">the component to be repainted
		/// </param>
		/// <param name="r">the area to be repainted
		/// 
		/// </param>
		/// <throws>  IllegalArgumentException if <code>c == null</code> </throws>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public Repainter(System.Windows.Forms.Control c, ref System.Drawing.Rectangle r)
		{
			InitBlock();
			if (c == null)
				throw new System.ArgumentException();
			this.c = c;
			
			if (r.IsEmpty)
				r = c.Bounds;
			
			this.x = r.X;
			this.y = r.Y;
			this.w = r.Width;
			this.h = r.Height;
		}
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <param name="op">{@inheritDoc}
		/// </param>
		/// <param name="success">repaint the <code>Component</code> iff <code>true</code>
		/// </param>
		public virtual void  imageOpChange(ImageOp op, bool success)
		{
			if (success)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint_int_int_int_int'"
				c.Refresh();
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void succeeded(Op < BufferedImage > op, BufferedImage img)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void cancelled(Op < BufferedImage > op, CancellationException e)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void interrupted(Op < BufferedImage > op, InterruptedException e)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void failed(Op < BufferedImage > op, ExecutionException e)
	}
}