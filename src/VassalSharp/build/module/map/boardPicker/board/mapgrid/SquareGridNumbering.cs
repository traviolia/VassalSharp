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
/*
* Created by IntelliJ IDEA.
* User: rkinney
* Date: Jul 24, 2002
* Time: 10:31:09 PM
* To change template for new class use
* Code Style | Class Templates options (Tools | IDE Options).
*/
using System;
using Buildable = VassalSharp.build.Buildable;
using SquareGrid = VassalSharp.build.module.map.boardPicker.board.SquareGrid;
using Labeler = VassalSharp.counters.Labeler;
namespace VassalSharp.build.module.map.boardPicker.board.mapgrid
{
	
	public class SquareGridNumbering:RegularGridNumbering
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassJPanel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassJPanel:System.Windows.Forms.Panel
		{
			public AnonymousClassJPanel(SquareGridNumbering enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SquareGridNumbering enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SquareGridNumbering enclosingInstance;
			public SquareGridNumbering Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			protected override void  OnPaint(System.Windows.Forms.PaintEventArgs g_EventArg)
			{
				System.Drawing.Graphics g = null;
				if (g_EventArg != null)
					g = g_EventArg.Graphics;
				g.FillRegion(new System.Drawing.SolidBrush(SupportClass.GraphicsManager.manager.GetBackColor(g)), new System.Drawing.Region(new System.Drawing.Rectangle(0, 0, Width, Height)));
				System.Drawing.Rectangle bounds = new System.Drawing.Rectangle(0, 0, Width, Height);
				Enclosing_Instance.grid.forceDraw(g, bounds, bounds, 1.0, false);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				Enclosing_Instance.forceDraw(g, ref bounds, ref bounds, 1.0, false);
			}
			
			//UPGRADE_NOTE: The equivalent of method 'javax.swing.JComponent.getPreferredSize' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public System.Drawing.Size getPreferredSize()
			{
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				return new System.Drawing.Size(3 * (int) Enclosing_Instance.grid.Dx, 3 * (int) Enclosing_Instance.grid.Dy);
			}
		}
		virtual protected internal System.Windows.Forms.Control GridVisualizer
		{
			get
			{
				if (visualizer == null)
				{
					visualizer = new AnonymousClassJPanel(this);
				}
				return visualizer;
			}
			
		}
		virtual protected internal int MaxRows
		{
			get
			{
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				return (int) (grid.getContainer().Size.Height / grid.Dy + 0.5);
			}
			
		}
		virtual protected internal int MaxColumns
		{
			get
			{
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				return (int) (grid.getContainer().Size.Width / grid.Dx + 0.5);
			}
			
		}
		
		private SquareGrid grid;
		
		public override void  addTo(Buildable parent)
		{
			grid = (SquareGrid) parent;
			grid.setGridNumbering(this);
		}
		
		/// <summary>Draw the numbering, if visible </summary>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public override void  draw(System.Drawing.Graphics g, ref System.Drawing.Rectangle bounds, ref System.Drawing.Rectangle visibleRect, double scale, bool reversed)
		{
			if (visible)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				forceDraw(g, ref bounds, ref visibleRect, scale, reversed);
			}
		}
		
		/// <summary>Draw the numbering, even if not visible </summary>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  forceDraw(System.Drawing.Graphics g, ref System.Drawing.Rectangle bounds, ref System.Drawing.Rectangle visibleRect, double scale, bool reversed)
		{
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int size = (int) (scale * fontSize + 0.5);
			if (size < 5 || !bounds.IntersectsWith(visibleRect))
			{
				return ;
			}
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Rectangle.intersection' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.Drawing.Rectangle region = System.Drawing.Rectangle.Intersect(bounds, visibleRect);
			//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_ISSUE: Method 'java.awt.Graphics.getClip' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphicsgetClip'"
			System.Drawing.Drawing2D.GraphicsPath oldClip = g.getClip();
			if (oldClip != null)
			{
				System.Drawing.Region clipArea = new System.Drawing.Region(oldClip);
				clipArea.Intersect(new System.Drawing.Region(region));
				g.SetClip(clipArea);
			}
			
			double deltaX = scale * grid.Dx;
			double deltaY = scale * grid.Dy;
			
			System.Drawing.Point centerPoint = System.Drawing.Point.Empty;
			System.Drawing.Graphics g2d = (System.Drawing.Graphics) g;
			double radians = 0;
			if (rotateTextDegrees != 0)
			{
				radians = SupportClass.DegreesToRadians(rotateTextDegrees);
				g2d.RotateTransform((float) SupportClass.RadiansToDegrees(radians), System.Drawing.Drawing2D.MatrixOrder.Append);
			}
			
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int minCol = reversed?(int) System.Math.Ceiling((bounds.X - scale * grid.getOrigin().X + bounds.Width - region.X) / deltaX):(int) System.Math.Floor((region.X - bounds.X - scale * grid.getOrigin().X) / deltaX);
			double xmin = reversed?bounds.X - scale * grid.getOrigin().X + bounds.Width - deltaX * minCol:bounds.X + scale * grid.getOrigin().X + deltaX * minCol;
			double xmax = region.X + region.Width + deltaX;
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int minRow = reversed?(int) System.Math.Ceiling((bounds.Y - scale * grid.getOrigin().Y + bounds.Height - region.Y) / deltaY):(int) System.Math.Floor((region.Y - bounds.Y - scale * grid.getOrigin().Y) / deltaY);
			double ymin = reversed?bounds.Y - scale * grid.getOrigin().Y + bounds.Height - deltaY * minRow:bounds.Y + scale * grid.getOrigin().Y + deltaY * minRow;
			double ymax = region.Y + region.Height + deltaY;
			
			//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
			//UPGRADE_TODO: Field 'java.awt.Font.PLAIN' was converted to 'System.Drawing.FontStyle.Regular' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFontPLAIN_f'"
			System.Drawing.Font f = new System.Drawing.Font("Dialog", size, System.Drawing.FontStyle.Regular);
			int column = minCol;
			for (double x = xmin; x < xmax; x += deltaX, column += (reversed?- 1:1))
			{
				int printRow, printColumn;
				int row = minRow;
				for (double y = ymin; y < ymax; y += deltaY, row += (reversed?- 1:1))
				{
					printRow = row;
					printColumn = column;
					if (vDescending)
					{
						printRow = MaxRows - row;
					}
					if (hDescending)
					{
						printColumn = MaxColumns - column;
					}
					
					// When rotating text, keep basic label position as in center along edge
					int newX = 0, newY = 0;
					switch (rotateTextDegrees)
					{
						
						case 90: 
							//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
							newX = (int) (x + deltaX / 2);
							//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
							newY = (int) y;
							break;
						
						case 180: 
							//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
							newX = (int) x;
							//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
							newY = (int) (y + deltaY / 2);
							break;
						
						case 270: 
							//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
							newX = (int) (x - deltaX / 2);
							//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
							newY = (int) y;
							break;
						
						default: 
							//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
							newX = (int) x;
							//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
							newY = (int) (y - deltaY / 2);
							break;
						
					}
					
					centerPoint = offsetLabelCenter(newX, newY, scale);
					Labeler.drawLabel(g, getName(printRow, printColumn), centerPoint.X, centerPoint.Y, f, Labeler.CENTER, Labeler.TOP, color, null, null);
				}
			}
			if (rotateTextDegrees != 0)
			{
				g2d.RotateTransform((float) SupportClass.RadiansToDegrees(- radians), System.Drawing.Drawing2D.MatrixOrder.Append);
			}
			g.SetClip(oldClip);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public override int getColumn(ref System.Drawing.Point p)
		{
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int col = (int) System.Math.Floor((p.X - grid.getOrigin().X) / grid.Dx + 0.5);
			if (hDescending)
			{
				return (MaxColumns - col);
			}
			else
			{
				return col;
			}
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public override int getRow(ref System.Drawing.Point p)
		{
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int row = (int) ((p.Y - grid.getOrigin().Y) / grid.Dy + 0.5);
			if (vDescending)
			{
				return (MaxRows - row);
			}
			else
			{
				return row;
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public virtual System.Drawing.Point getCenterPoint(int col, int row)
		{
			if (vDescending)
				row = MaxRows - row;
			if (hDescending)
				col = MaxColumns - col;
			
			// TODO: invoke grid.snapTo
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			return new System.Drawing.Point((int) (col * grid.Dx + grid.getOrigin().X), (int) (row * grid.Dy + grid.getOrigin().Y));
		}
		
		public override void  removeFrom(Buildable parent)
		{
			grid.setGridNumbering(null);
		}
	}
}