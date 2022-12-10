/*
* $Id$
*
* Copyright (c) 2004 by Rodney Kinney
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
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.build.module.map.boardPicker.board.mapgrid
{
	
	[Serializable]
	public class PolygonEditor:System.Windows.Forms.Panel
	{
		static private System.Int32 state149;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(PolygonEditor enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PolygonEditor enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PolygonEditor enclosingInstance;
			public PolygonEditor Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				if (Enclosing_Instance.selected >= 0)
				{
					for (int i = Enclosing_Instance.selected; i < Enclosing_Instance.polygon.PointCount - 1; ++i)
					{
						//UPGRADE_TODO: Field 'java.awt.Polygon.xpoints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
						SupportClass.GetXPoints(Enclosing_Instance.polygon)[i] = SupportClass.GetXPoints(Enclosing_Instance.polygon)[i + 1];
						//UPGRADE_TODO: Field 'java.awt.Polygon.ypoints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
						SupportClass.GetYPoints(Enclosing_Instance.polygon)[i] = SupportClass.GetYPoints(Enclosing_Instance.polygon)[i + 1];
					}
					Enclosing_Instance.polygon.PointCount--;
					Enclosing_Instance.selected = - 1;
					//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
					Enclosing_Instance.Refresh();
				}
			}
		}
		private class AnonymousClassWindowAdapter
		{
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs e)
			{
				e.Cancel = true;
				System.Environment.Exit(0);
			}
		}
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state149 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		virtual public System.Drawing.Drawing2D.GraphicsPath Polygon
		{
			get
			{
				return polygon;
			}
			
			set
			{
				this.polygon = value;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		private System.Drawing.Drawing2D.GraphicsPath polygon;
		private int selected = - 1;
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		protected internal System.Windows.Forms.ScrollableControl myScroll;
		
		public PolygonEditor(System.Drawing.Drawing2D.GraphicsPath p)
		{
			polygon = p;
			reset();
		}
		
		protected internal virtual void  reset()
		{
			if (polygon == null || polygon.PointCount == 0)
			{
				setupForCreate();
			}
			else
			{
				setupForEdit();
			}
		}
		
		public virtual System.Drawing.Drawing2D.GraphicsPath clonePolygon()
		{
			//UPGRADE_TODO: Constructor 'java.awt.Polygon.Polygon' was converted to 'SupportClass.CreateGraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtPolygonPolygon_int[]_int[]_int'"
			return SupportClass.CreateGraphicsPath(SupportClass.GetXPoints(polygon), SupportClass.GetYPoints(polygon), polygon.PointCount);
		}
		
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public virtual void  setScroll(System.Windows.Forms.ScrollableControl scroll)
		{
			myScroll = scroll;
		}
		
		private void  setupForCreate()
		{
			DefineRectangle dr = new DefineRectangle(this);
			MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.build.module.map.boardPicker.board.mapgrid.PolygonEditor.mouseDown);
			MouseDown += new System.Windows.Forms.MouseEventHandler(dr.mousePressed);
			MouseUp += new System.Windows.Forms.MouseEventHandler(dr.mouseReleased);
		}
		
		private void  setupForEdit()
		{
			ModifyPolygon mp = new ModifyPolygon(this);
			MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.build.module.map.boardPicker.board.mapgrid.PolygonEditor.mouseDown);
			MouseDown += new System.Windows.Forms.MouseEventHandler(mp.mousePressed);
			MouseUp += new System.Windows.Forms.MouseEventHandler(mp.mouseReleased);
			//UPGRADE_ISSUE: Method 'java.awt.Component.addMouseMotionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentaddMouseMotionListener_javaawteventMouseMotionListener'"
			addMouseMotionListener(mp);
			//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			ActionListener l = new AnonymousClassActionListener(this);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.registerKeyboardAction' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentregisterKeyboardAction_javaawteventActionListener_javaxswingKeyStroke_int'"
			registerKeyboardAction(l, new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.Delete | 0)), 2);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.registerKeyboardAction' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentregisterKeyboardAction_javaawteventActionListener_javaxswingKeyStroke_int'"
			registerKeyboardAction(l, new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.Back | 0)), 2);
			//UPGRADE_TODO: Method 'javax.swing.JComponent.requestFocus' was converted to 'System.Windows.Forms.Control.Focus' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentrequestFocus'"
			Focus();
			selected = 2;
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			Refresh();
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  center(ref System.Drawing.Point p)
		{
			//UPGRADE_TODO: Method 'javax.swing.JComponent.getVisibleRect' was converted to 'System.Windows.Forms.Control.DisplayRectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentgetVisibleRect'"
			System.Drawing.Rectangle r = this.DisplayRectangle;
			if (r.Width == 0)
			{
				r.Width = 600;
				r.Height = 600;
			}
			int x = p.X - r.Width / 2;
			int y = p.Y - r.Height / 2;
			if (x < 0)
				x = 0;
			if (y < 0)
				y = 0;
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.scrollRectToVisible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentscrollRectToVisible_javaawtRectangle'"
			scrollRectToVisible(new System.Drawing.Rectangle(x, y, r.Width, r.Height));
		}
		
		public static void  reset(System.Drawing.Drawing2D.GraphicsPath p, System.String path)
		{
			p.reset();
			SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(path, ';');
			while (sd.hasMoreTokens())
			{
				System.String s = sd.nextToken();
				SequenceEncoder.Decoder pd = new SequenceEncoder.Decoder(s, ',');
				if (pd.hasMoreTokens())
				{
					try
					{
						int x = System.Int32.Parse(pd.nextToken().Trim());
						if (pd.hasMoreTokens())
						{
							int y = System.Int32.Parse(pd.nextToken().Trim());
							//UPGRADE_TODO: Method 'java.awt.Polygon.addPoint' was converted to 'SupportClass.AddPointToGraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtPolygonaddPoint_int_int'"
							SupportClass.AddPointToGraphicsPath(p, x, y);
						}
					}
					// FIXME: review error message
					catch (System.FormatException e)
					{
					}
				}
			}
		}
		
		public static System.String polygonToString(System.Drawing.Drawing2D.GraphicsPath p)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder s = new StringBuilder();
			for (int i = 0; i < p.PointCount; i++)
			{
				//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_float'"
				s.append((int) System.Math.Round((double) SupportClass.GetXPoints(p)[i])).append(',').append((int) System.Math.Round((double) SupportClass.GetYPoints(p)[i]));
				if (i < (p.PointCount - 1))
				{
					s.append(';');
				}
			}
			return s.toString();
		}
		
		protected override void  OnPaint(System.Windows.Forms.PaintEventArgs g_EventArg)
		{
			System.Drawing.Graphics g = null;
			if (g_EventArg != null)
				g = g_EventArg.Graphics;
			paintBackground(g);
			if (polygon != null && polygon.PointCount > 0)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'g2d '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Graphics g2d = (System.Drawing.Graphics) g;
				
				SupportClass.GraphicsManager.manager.SetColor(g2d, System.Drawing.Color.White);
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetComposite_javaawtComposite'"
				//UPGRADE_ISSUE: Method 'java.awt.AlphaComposite.getInstance' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
				//UPGRADE_ISSUE: Field 'java.awt.AlphaComposite.SRC_OVER' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
				g2d.setComposite(AlphaComposite.getInstance(AlphaComposite.SRC_OVER, 0.5F));
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.fill' was converted to 'System.Drawing.Graphics.FillPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Dfill_javaawtShape'"
				g2d.FillPath(polygon);
				
				if (selected >= 0 && selected < SupportClass.GetXPoints(polygon).Length)
				{
					SupportClass.GraphicsManager.manager.SetColor(g2d, System.Drawing.Color.Red);
					int x = SupportClass.GetXPoints(polygon)[selected];
					int y = SupportClass.GetYPoints(polygon)[selected];
					g2d.FillEllipse(SupportClass.GraphicsManager.manager.GetPaint(g2d), x - 10, y - 10, 20, 20);
				}
				
				g2d.setComposite(AlphaComposite.SrcAtop);
				SupportClass.GraphicsManager.manager.SetColor(g2d, System.Drawing.Color.Black);
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.setStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DsetStroke_javaawtStroke'"
				//UPGRADE_TODO: Constructor 'java.awt.BasicStroke.BasicStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtBasicStrokeBasicStroke_float'"
				SupportClass.GraphicsManager.manager.SetPen(g2d, new System.Drawing.Pen(System.Drawing.Brushes.Black, 2.0F));
				g2d.DrawPath(SupportClass.GraphicsManager.manager.GetPen(g2d), polygon);
			}
		}
		
		protected internal virtual void  paintBackground(System.Drawing.Graphics g)
		{
			base.OnPaint(new System.Windows.Forms.PaintEventArgs(g, base.Bounds));
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ModifyPolygon' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class ModifyPolygon
		{
			public ModifyPolygon(PolygonEditor enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PolygonEditor enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PolygonEditor enclosingInstance;
			public PolygonEditor Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			// implements java.awt.event.MouseMotionListener
			public void  mouseDragged(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
			{
				moveSelectedPoint(event_sender, e);
				if (SupportClass.SwingUtilsSupport.IsMouseLeft(e))
				{
					System.Drawing.Point tempAux = new System.Drawing.Point(e.X, e.Y);
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					scrollAtEdge(ref tempAux, 15);
				}
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Enclosing_Instance.Refresh();
			}
			
			private void  moveSelectedPoint(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
			{
				if (Enclosing_Instance.selected >= 0 && Enclosing_Instance.selected < SupportClass.GetXPoints(Enclosing_Instance.polygon).Length)
				{
					//UPGRADE_TODO: Field 'java.awt.Polygon.xpoints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					SupportClass.GetXPoints(Enclosing_Instance.polygon)[Enclosing_Instance.selected] = e.X;
					//UPGRADE_TODO: Field 'java.awt.Polygon.ypoints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					SupportClass.GetYPoints(Enclosing_Instance.polygon)[Enclosing_Instance.selected] = e.Y;
				}
			}
			
			// implements java.awt.event.MouseListener
			public void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
			{
				moveSelectedPoint(event_sender, e);
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Enclosing_Instance.Refresh();
			}
			
			// implements java.awt.event.MouseListener
			public void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
			{
				Enclosing_Instance.selected = - 1;
				double minDist = System.Single.MaxValue;
				for (int i = 0; i < Enclosing_Instance.polygon.PointCount; ++i)
				{
					double dist = SupportClass.PointFSupport.Distance((float) SupportClass.GetXPoints(Enclosing_Instance.polygon)[i], (float) SupportClass.GetYPoints(Enclosing_Instance.polygon)[i], (float) e.X, (float) e.Y);
					if (dist < minDist)
					{
						minDist = dist;
						Enclosing_Instance.selected = i;
					}
				}
				//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
				if (e.isMetaDown())
				{
					//UPGRADE_TODO: Method 'java.awt.Polygon.addPoint' was converted to 'SupportClass.AddPointToGraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtPolygonaddPoint_int_int'"
					SupportClass.AddPointToGraphicsPath(Enclosing_Instance.polygon, e.X, e.Y);
					if (Enclosing_Instance.selected >= 0)
					{
						for (int i = Enclosing_Instance.polygon.PointCount - 1; i > Enclosing_Instance.selected; --i)
						{
							//UPGRADE_TODO: Field 'java.awt.Polygon.xpoints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
							SupportClass.GetXPoints(Enclosing_Instance.polygon)[i] = SupportClass.GetXPoints(Enclosing_Instance.polygon)[i - 1];
							//UPGRADE_TODO: Field 'java.awt.Polygon.ypoints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
							SupportClass.GetYPoints(Enclosing_Instance.polygon)[i] = SupportClass.GetYPoints(Enclosing_Instance.polygon)[i - 1];
						}
						//UPGRADE_TODO: Field 'java.awt.Polygon.xpoints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
						SupportClass.GetXPoints(Enclosing_Instance.polygon)[Enclosing_Instance.selected] = e.X;
						//UPGRADE_TODO: Field 'java.awt.Polygon.ypoints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
						SupportClass.GetYPoints(Enclosing_Instance.polygon)[Enclosing_Instance.selected] = e.Y;
					}
				}
			}
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			public virtual void  scrollAtEdge(ref System.Drawing.Point evtPt, int dist)
			{
				
				//UPGRADE_ISSUE: Method 'javax.swing.JViewport.getViewPosition' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJViewport'"
				//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
				System.Drawing.Point p = new System.Drawing.Point(evtPt.X - Enclosing_Instance.myScroll.getViewport().getViewPosition().X, evtPt.Y - Enclosing_Instance.myScroll.getViewport().getViewPosition().Y);
				int dx = 0, dy = 0;
				if (p.X < dist && p.X >= 0)
					dx = - 1;
				//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
				if (p.X >= Enclosing_Instance.myScroll.getViewport().Size.Width - dist && p.X < Enclosing_Instance.myScroll.getViewport().Size.Width)
					dx = 1;
				if (p.Y < dist && p.Y >= 0)
					dy = - 1;
				//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
				if (p.Y >= Enclosing_Instance.myScroll.getViewport().Size.Height - dist && p.Y < Enclosing_Instance.myScroll.getViewport().Size.Height)
					dy = 1;
				
				if (dx != 0 || dy != 0)
				{
					System.Drawing.Rectangle temp_Rectangle;
					//UPGRADE_ISSUE: Method 'javax.swing.JViewport.getViewRect' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJViewport'"
					//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
					temp_Rectangle = Enclosing_Instance.myScroll.getViewport().getViewRect();
					System.Drawing.Rectangle r = new System.Drawing.Rectangle(temp_Rectangle.Location, temp_Rectangle.Size);
					r.Offset(2 * dist * dx, 2 * dist * dy);
					//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Rectangle.intersection' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					r = System.Drawing.Rectangle.Intersect(r, new System.Drawing.Rectangle(new System.Drawing.Point(0, 0), Enclosing_Instance.Size));
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.scrollRectToVisible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentscrollRectToVisible_javaawtRectangle'"
					Enclosing_Instance.scrollRectToVisible(r);
				}
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'DefineRectangle' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class DefineRectangle
		{
			public DefineRectangle(PolygonEditor enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			static private System.Int32 state149;
			private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
			{
				state149 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
			}
			private void  InitBlock(PolygonEditor enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PolygonEditor enclosingInstance;
			public PolygonEditor Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			// implements java.awt.event.MouseListener
			public void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
			{
				Enclosing_Instance.polygon = new System.Drawing.Drawing2D.GraphicsPath();
				//UPGRADE_TODO: Method 'java.awt.Polygon.addPoint' was converted to 'SupportClass.AddPointToGraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtPolygonaddPoint_int_int'"
				SupportClass.AddPointToGraphicsPath(Enclosing_Instance.polygon, e.X, e.Y);
				//UPGRADE_TODO: Method 'java.awt.Polygon.addPoint' was converted to 'SupportClass.AddPointToGraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtPolygonaddPoint_int_int'"
				SupportClass.AddPointToGraphicsPath(Enclosing_Instance.polygon, e.X, e.Y);
				//UPGRADE_TODO: Method 'java.awt.Polygon.addPoint' was converted to 'SupportClass.AddPointToGraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtPolygonaddPoint_int_int'"
				SupportClass.AddPointToGraphicsPath(Enclosing_Instance.polygon, e.X, e.Y);
				//UPGRADE_TODO: Method 'java.awt.Polygon.addPoint' was converted to 'SupportClass.AddPointToGraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtPolygonaddPoint_int_int'"
				SupportClass.AddPointToGraphicsPath(Enclosing_Instance.polygon, e.X, e.Y);
				//UPGRADE_ISSUE: Method 'java.awt.Component.addMouseMotionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentaddMouseMotionListener_javaawteventMouseMotionListener'"
				Enclosing_Instance.addMouseMotionListener(this);
			}
			
			// implements java.awt.event.MouseMotionListener
			public void  mouseDragged(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
			{
				//UPGRADE_TODO: Field 'java.awt.Polygon.xpoints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				SupportClass.GetXPoints(Enclosing_Instance.polygon)[1] = e.X;
				//UPGRADE_TODO: Field 'java.awt.Polygon.xpoints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				SupportClass.GetXPoints(Enclosing_Instance.polygon)[2] = e.X;
				//UPGRADE_TODO: Field 'java.awt.Polygon.ypoints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				SupportClass.GetYPoints(Enclosing_Instance.polygon)[2] = e.Y;
				//UPGRADE_TODO: Field 'java.awt.Polygon.ypoints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				SupportClass.GetYPoints(Enclosing_Instance.polygon)[3] = e.Y;
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Enclosing_Instance.Refresh();
			}
			
			public void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
			{
				Enclosing_Instance.MouseDown -= new System.Windows.Forms.MouseEventHandler(VassalSharp.build.module.map.boardPicker.board.mapgrid.PolygonEditor.DefineRectangle.mouseDown);
				//UPGRADE_ISSUE: Method 'java.awt.Component.removeMouseMotionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentremoveMouseMotionListener_javaawteventMouseMotionListener'"
				Enclosing_Instance.removeMouseMotionListener(this);
				Enclosing_Instance.setupForEdit();
			}
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			System.Windows.Forms.Form f = new System.Windows.Forms.Form();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = new PolygonEditor(null);
			f.Controls.Add(temp_Control);
			//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
			f.Size = new System.Drawing.Size(500, 500);
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			f.Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter().windowClosing);
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			f.Visible = true;
		}
	}
}