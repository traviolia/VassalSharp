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
namespace VassalSharp.counters
{
	
	public class ColoredBorder : Highlighter
	{
		virtual public System.Drawing.Color Color
		{
			get
			{
				return c;
			}
			
			set
			{
				this.c = value;
			}
			
		}
		virtual public int Thickness
		{
			get
			{
				return thickness;
			}
			
			set
			{
				this.thickness = value;
			}
			
		}
		protected internal System.Drawing.Color c;
		protected internal int thickness;
		
		// Additional Highlighters
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < Highlighter > highlighters = new ArrayList < Highlighter >();
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public ColoredBorder():this(ref tempAux, 3)
		{
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public ColoredBorder(ref System.Drawing.Color c, int thickness)
		{
			this.c = c;
			this.thickness = thickness;
		}
		
		public virtual void  addHighlighter(Highlighter h)
		{
			highlighters.add(h);
		}
		
		public virtual void  removeHighlighter(Highlighter h)
		{
			highlighters.remove(h);
		}
		
		public virtual void  draw(GamePiece p, System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			if (thickness > 0)
			{
				if (!c.IsEmpty)
				{
					// Find the border by outsetting the bounding box, and then scaling
					// the shape to fill the outset.
					//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					System.Drawing.Drawing2D.GraphicsPath s = p.Shape;
					//UPGRADE_NOTE: Final was removed from the declaration of 'br '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Rectangle br = System.Drawing.Rectangle.Truncate(s.GetBounds());
					
					// Don't bother if the shape is empty.
					if (!br.IsEmpty)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'xzoom '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						double xzoom = (br.Width + 1) / br.Width;
						//UPGRADE_NOTE: Final was removed from the declaration of 'yzoom '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						double yzoom = (br.Height + 1) / br.Height;
						//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Drawing.Drawing2D.Matrix temp_Matrix;
						temp_Matrix = new System.Drawing.Drawing2D.Matrix();
						temp_Matrix.Translate((float) x, (float) y);
						System.Drawing.Drawing2D.Matrix t = temp_Matrix;
						t.Scale((System.Single) (xzoom * zoom), (System.Single) (yzoom * zoom));
						
						//UPGRADE_NOTE: Final was removed from the declaration of 'g2d '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Drawing.Graphics g2d = (System.Drawing.Graphics) g;
						//UPGRADE_NOTE: Final was removed from the declaration of 'str '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						//UPGRADE_TODO: Method 'java.awt.Graphics2D.getStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DgetStroke'"
						System.Drawing.Pen str = SupportClass.GraphicsManager.manager.GetPen(g2d);
						//UPGRADE_TODO: Method 'java.awt.Graphics2D.setStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DsetStroke_javaawtStroke'"
						//UPGRADE_TODO: Constructor 'java.awt.BasicStroke.BasicStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtBasicStrokeBasicStroke_float'"
						//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
						SupportClass.GraphicsManager.manager.SetPen(g2d, new System.Drawing.Pen(System.Drawing.Brushes.Black, System.Math.Max(1, (long) System.Math.Round(zoom * thickness))));
						SupportClass.GraphicsManager.manager.SetColor(g2d, c);
						//UPGRADE_TODO: Method 'java.awt.Graphics2D.draw' was converted to 'System.Drawing.Graphics.DrawPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Ddraw_javaawtShape'"
						//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.createTransformedShape' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformcreateTransformedShape_javaawtShape'"
						g2d.DrawPath(SupportClass.GraphicsManager.manager.GetPen(g2d), t.createTransformedShape(s));
						//UPGRADE_TODO: Method 'java.awt.Graphics2D.setStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DsetStroke_javaawtStroke'"
						SupportClass.GraphicsManager.manager.SetPen(g2d, str);
					}
				}
				else
				{
					highlightSelectionBounds(p, g, x, y, obs, zoom);
				}
			}
			
			// Draw any additional highlighters
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Highlighter h: highlighters)
			{
				h.draw(p, g, x, y, obs, zoom);
			}
		}
		
		protected internal virtual void  highlightSelectionBounds(GamePiece p, System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			System.Drawing.Rectangle r = System.Drawing.Rectangle.Truncate(p.Shape.GetBounds());
			SupportClass.GraphicsManager.manager.SetColor(g, c);
			for (int i = 1; i < thickness; ++i)
			{
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), x + (int) (zoom * r.X) - i, y + (int) (zoom * r.Y) - i, (int) (zoom * r.Width) + 2 * i - 1, (int) (zoom * r.Height) + 2 * i - 1);
			}
		}
		
		public virtual System.Drawing.Rectangle boundingBox(GamePiece p)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle r = System.Drawing.Rectangle.Truncate(p.Shape.GetBounds());
			r.Offset(- thickness, - thickness);
			r.Size = new System.Drawing.Size(r.Width + 2 * thickness, r.Height + 2 * thickness);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Highlighter h: highlighters) r.add(h.boundingBox(p));
			return r;
		}
		private static System.Drawing.Color tempAux = System.Drawing.Color.Black;
	}
}