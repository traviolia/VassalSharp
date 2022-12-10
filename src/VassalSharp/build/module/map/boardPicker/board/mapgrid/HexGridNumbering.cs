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
* Date: Jul 25, 2002
* Time: 11:46:35 PM
* To change template for new class use
* Code Style | Class Templates options (Tools | IDE Options).
*/
using System;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using Buildable = VassalSharp.build.Buildable;
using HexGrid = VassalSharp.build.module.map.boardPicker.board.HexGrid;
using Labeler = VassalSharp.counters.Labeler;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
using ScrollPane = VassalSharp.tools.ScrollPane;
namespace VassalSharp.build.module.map.boardPicker.board.mapgrid
{
	
	public class HexGridNumbering:RegularGridNumbering
	{
		public HexGridNumbering()
		{
			InitBlock();
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassJPanel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassJPanel:System.Windows.Forms.Panel
		{
			public AnonymousClassJPanel(HexGridNumbering enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(HexGridNumbering enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private HexGridNumbering enclosingInstance;
			public HexGridNumbering Enclosing_Instance
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
				return new System.Drawing.Size(4 * (int) Enclosing_Instance.grid.getHexSize(), 4 * (int) Enclosing_Instance.grid.getHexWidth());
			}
		}
		//UPGRADE_NOTE: Local class 'TestPanel' in method 'main' was converted to  a nested class. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1022'"
		[Serializable]
		public class TestPanel:System.Windows.Forms.Panel
		{
			static private System.Int32 state146;
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassKeyAdapter
			{
				public AnonymousClassKeyAdapter(System.Windows.Forms.TextBox tf, TestPanel enclosingInstance)
				{
					InitBlock(tf, enclosingInstance);
				}
				private void  InitBlock(System.Windows.Forms.TextBox tf, TestPanel enclosingInstance)
				{
					this.tf = tf;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable tf was copied into class AnonymousClassKeyAdapter. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Windows.Forms.TextBox tf;
				private TestPanel enclosingInstance;
				public TestPanel Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
				{
					try
					{
						Enclosing_Instance.scale = System.Double.Parse(tf.Text);
						//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
						Enclosing_Instance.Refresh();
					}
					// FIXME: review error message
					catch (System.FormatException e1)
					{
						VassalSharp.build.module.map.boardPicker.board.mapgrid.HexGridNumbering.logger.error("", e1);
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassItemListener
			{
				public AnonymousClassItemListener(System.Windows.Forms.CheckBox reverseBox, TestPanel enclosingInstance)
				{
					InitBlock(reverseBox, enclosingInstance);
				}
				private void  InitBlock(System.Windows.Forms.CheckBox reverseBox, TestPanel enclosingInstance)
				{
					this.reverseBox = reverseBox;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable reverseBox was copied into class AnonymousClassItemListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Windows.Forms.CheckBox reverseBox;
				private TestPanel enclosingInstance;
				public TestPanel Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs e)
				{
					if (event_sender is System.Windows.Forms.MenuItem)
						((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
					Enclosing_Instance.reversed = reverseBox.Checked;
					//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
					Enclosing_Instance.Refresh();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassItemListener1
			{
				public AnonymousClassItemListener1(System.Windows.Forms.CheckBox sidewaysBox, TestPanel enclosingInstance)
				{
					InitBlock(sidewaysBox, enclosingInstance);
				}
				private void  InitBlock(System.Windows.Forms.CheckBox sidewaysBox, TestPanel enclosingInstance)
				{
					this.sidewaysBox = sidewaysBox;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable sidewaysBox was copied into class AnonymousClassItemListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Windows.Forms.CheckBox sidewaysBox;
				private TestPanel enclosingInstance;
				public TestPanel Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs e)
				{
					if (event_sender is System.Windows.Forms.MenuItem)
						((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
					Enclosing_Instance.grid.setAttribute(HexGrid.SIDEWAYS, sidewaysBox.Checked?true:false);
					//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
					Enclosing_Instance.Refresh();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassJPanel1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			[Serializable]
			private class AnonymousClassJPanel1:System.Windows.Forms.Panel
			{
				public AnonymousClassJPanel1(TestPanel enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(TestPanel enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private TestPanel enclosingInstance;
				public TestPanel Enclosing_Instance
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
					System.Drawing.Rectangle r = new System.Drawing.Rectangle(0, 0, Width, Height);
					g.FillRegion(new System.Drawing.SolidBrush(SupportClass.GraphicsManager.manager.GetBackColor(g)), new System.Drawing.Region(new System.Drawing.Rectangle(r.X, r.Y, r.Width, r.Height)));
					//UPGRADE_TODO: Method 'javax.swing.JComponent.getVisibleRect' was converted to 'System.Windows.Forms.Control.DisplayRectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentgetVisibleRect'"
					Enclosing_Instance.grid.forceDraw(g, r, DisplayRectangle, Enclosing_Instance.scale, Enclosing_Instance.reversed);
					//UPGRADE_TODO: Method 'javax.swing.JComponent.getVisibleRect' was converted to 'System.Windows.Forms.Control.DisplayRectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentgetVisibleRect'"
					System.Drawing.Rectangle tempAux = Bounds;
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					System.Drawing.Rectangle tempAux2 = DisplayRectangle;
					Enclosing_Instance.numbering.forceDraw(g, ref tempAux, ref tempAux2, Enclosing_Instance.scale, Enclosing_Instance.reversed);
				}
			}
			private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
			{
				state146 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
			}
			private const long serialVersionUID = 1L;
			
			private bool reversed;
			private double scale = 1.0;
			private HexGrid grid;
			private HexGridNumbering numbering;
			
			internal TestPanel()
			{
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
				/*
				setLayout(new BorderLayout());*/
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box b = Box.createHorizontalBox();
				//UPGRADE_NOTE: Final was removed from the declaration of 'tf '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.TextBox temp_text_box;
				temp_text_box = new System.Windows.Forms.TextBox();
				temp_text_box.Text = "1.0";
				System.Windows.Forms.TextBox tf = temp_text_box;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(tf);
				tf.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.build.module.map.boardPicker.board.mapgrid.HexGridNumbering.TestPanel.keyDown);
				tf.KeyUp += new System.Windows.Forms.KeyEventHandler(new AnonymousClassKeyAdapter(tf, this).keyReleased);
				//UPGRADE_NOTE: Final was removed from the declaration of 'reverseBox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.CheckBox reverseBox = SupportClass.CheckBoxSupport.CreateCheckBox("Reversed");
				reverseBox.CheckedChanged += new System.EventHandler(new AnonymousClassItemListener(reverseBox, this).itemStateChanged);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(reverseBox);
				//UPGRADE_NOTE: Final was removed from the declaration of 'sidewaysBox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.CheckBox sidewaysBox = SupportClass.CheckBoxSupport.CreateCheckBox("Sideways");
				sidewaysBox.CheckedChanged += new System.EventHandler(new AnonymousClassItemListener1(sidewaysBox, this).itemStateChanged);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(sidewaysBox);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
				Controls.Add(b);
				grid = new HexGrid();
				grid.setAttribute(HexGrid.COLOR, System.Drawing.Color.Black);
				numbering = new HexGridNumbering();
				numbering.setAttribute(HexGridNumbering.COLOR, System.Drawing.Color.Black);
				numbering.addTo(grid);
				System.Windows.Forms.Panel p = new AnonymousClassJPanel1(this);
				System.Drawing.Size d = new System.Drawing.Size(4000, 4000);
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				p.Size = d;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = new ScrollPane(p);
				Controls.Add(temp_Control);
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
		private void  InitBlock()
		{
			return ArrayUtils.append(base.getAttributeTypes(), typeof(System.Boolean));
		}
		virtual public HexGrid Grid
		{
			get
			{
				return grid;
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return ArrayUtils.append(base.AttributeDescriptions, "Odd-numbered rows numbered higher?");
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return ArrayUtils.append(base.AttributeNames, STAGGER);
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
				return (int) System.Math.Floor(grid.getContainer().Size.Height / grid.getHexWidth() + 0.5);
			}
			
		}
		virtual protected internal int MaxColumns
		{
			get
			{
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				return (int) System.Math.Floor(grid.getContainer().Size.Width / grid.getHexSize() + 0.5);
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.build.module.map.boardPicker.board.mapgrid.HexGridNumbering'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		private HexGrid grid;
		private bool stagger = true;
		
		public override void  addTo(Buildable parent)
		{
			grid = (HexGrid) parent;
			grid.setGridNumbering(this);
		}
		
		public const System.String STAGGER = "stagger";
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		
		public override void  setAttribute(System.String key, System.Object value_Renamed)
		{
			if (STAGGER.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.lang.Boolean.valueOf' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
					value_Renamed = System.Boolean.Parse((System.String) value_Renamed);
				}
				stagger = ((System.Boolean) value_Renamed);
			}
			else
			{
				base.setAttribute(key, value_Renamed);
			}
		}
		
		public override System.String getAttributeValueString(System.String key)
		{
			if (STAGGER.Equals(key))
			{
				return System.Convert.ToString(stagger);
			}
			else
			{
				return base.getAttributeValueString(key);
			}
		}
		
		/// <summary>Draw the numbering if visible </summary>
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
			if (size < 5)
			{
				return ;
			}
			
			System.Drawing.Graphics g2d = (System.Drawing.Graphics) g;
			System.Drawing.Drawing2D.Matrix oldT = g2d.Transform;
			if (reversed)
			{
				System.Drawing.Drawing2D.Matrix temp_Matrix;
				temp_Matrix = new System.Drawing.Drawing2D.Matrix();
				temp_Matrix.RotateAt((float) (System.Math.PI * (180 / System.Math.PI)), new System.Drawing.PointF((float) (bounds.X + .5 * bounds.Width), (float) (bounds.Y + .5 * bounds.Height)));
				System.Drawing.Drawing2D.Matrix t = temp_Matrix;
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.transform' was converted to 'System.Drawing.Graphics.Transform' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Dtransform_javaawtgeomAffineTransform'"
				g2d.Transform = t;
				//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.createTransformedShape' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformcreateTransformedShape_javaawtShape'"
				visibleRect = System.Drawing.Rectangle.Truncate(t.createTransformedShape(visibleRect).GetBounds());
			}
			
			if (!bounds.IntersectsWith(visibleRect))
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
			
			double deltaX = scale * grid.getHexWidth();
			double deltaY = scale * grid.getHexSize();
			
			if (grid.Sideways)
			{
				bounds = new System.Drawing.Rectangle(bounds.Y, bounds.X, bounds.Height, bounds.Width);
				region = new System.Drawing.Rectangle(region.Y, region.X, region.Height, region.Width);
			}
			
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int minCol = 2 * (int) System.Math.Floor((region.X - bounds.X - scale * grid.getOrigin().X) / (2 * deltaX));
			double xmin = bounds.X + scale * grid.getOrigin().X + deltaX * minCol;
			double xmax = region.X + region.Width + deltaX;
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int minRow = (int) System.Math.Floor((region.Y - bounds.Y - scale * grid.getOrigin().Y) / deltaY);
			double ymin = bounds.Y + scale * grid.getOrigin().Y + deltaY * minRow;
			double ymax = region.Y + region.Height + deltaY;
			
			//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
			//UPGRADE_TODO: Field 'java.awt.Font.PLAIN' was converted to 'System.Drawing.FontStyle.Regular' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFontPLAIN_f'"
			System.Drawing.Font f = new System.Drawing.Font("Dialog", size, System.Drawing.FontStyle.Regular);
			System.Drawing.Point p = new System.Drawing.Point(0, 0);
			int alignment = Labeler.TOP;
			//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
			int offset = - (int) System.Math.Round(deltaY / 2);
			if (grid.Sideways || rotateTextDegrees != 0)
			{
				alignment = Labeler.CENTER;
				offset = 0;
			}
			
			System.Drawing.Point gridp = new System.Drawing.Point(0, 0);
			
			System.Drawing.Point centerPoint = System.Drawing.Point.Empty;
			double radians = 0;
			if (rotateTextDegrees != 0)
			{
				radians = SupportClass.DegreesToRadians(rotateTextDegrees);
				g2d.RotateTransform((float) SupportClass.RadiansToDegrees(radians), System.Drawing.Drawing2D.MatrixOrder.Append);
			}
			
			for (double x = xmin; x < xmax; x += 2 * deltaX)
			{
				for (double y = ymin; y < ymax; y += deltaY)
				{
					
					//UPGRADE_TODO: Method 'java.awt.Point.setLocation' was converted to 'System.Drawing.Point.Point' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
					p = new System.Drawing.Point((System.Int32) System.Math.Round(x), (System.Int32) ((int) System.Math.Round(y) + offset));
					gridp = new System.Drawing.Point(p.X, p.Y - offset);
					grid.rotateIfSideways(p);
					
					// Convert from map co-ordinates to board co-ordinates
					gridp.Offset(- bounds.X, - bounds.Y);
					grid.rotateIfSideways(gridp);
					//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
					gridp.X = (int) System.Math.Round(gridp.X / scale);
					//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
					gridp.Y = (int) System.Math.Round(gridp.Y / scale);
					
					centerPoint = offsetLabelCenter(p, scale);
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					Labeler.drawLabel(g2d, getName(getRow(ref gridp), getColumn(ref gridp)), centerPoint.X, centerPoint.Y, f, Labeler.CENTER, alignment, color, null, null);
					
					//UPGRADE_TODO: Method 'java.awt.Point.setLocation' was converted to 'System.Drawing.Point.Point' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
					p = new System.Drawing.Point((System.Int32) System.Math.Round(x + deltaX), (System.Int32) ((int) System.Math.Round(y + deltaY / 2) + offset));
					gridp = new System.Drawing.Point(p.X, p.Y - offset);
					grid.rotateIfSideways(p);
					
					// Convert from map co-ordinates to board co-ordinates
					gridp.Offset(- bounds.X, - bounds.Y);
					grid.rotateIfSideways(gridp);
					//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
					gridp.X = (int) System.Math.Round(gridp.X / scale);
					//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
					gridp.Y = (int) System.Math.Round(gridp.Y / scale);
					
					centerPoint = offsetLabelCenter(p, scale);
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					Labeler.drawLabel(g2d, getName(getRow(ref gridp), getColumn(ref gridp)), centerPoint.X, centerPoint.Y, f, Labeler.CENTER, alignment, color, null, null);
				}
			}
			if (rotateTextDegrees != 0)
			{
				g2d.RotateTransform((float) SupportClass.RadiansToDegrees(- radians), System.Drawing.Drawing2D.MatrixOrder.Append);
			}
			g.SetClip(oldClip);
			g2d.Transform = oldT;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public virtual System.Drawing.Point getCenterPoint(int col, int row)
		{
			if (stagger)
			{
				if (grid.Sideways)
				{
					if (col % 2 != 0)
					{
						if (hDescending)
							row++;
						else
							row--;
					}
				}
				else
				{
					if (col % 2 != 0)
					{
						if (vDescending)
							row++;
						else
							row--;
					}
				}
			}
			
			if (grid.Sideways)
			{
				if (vDescending)
					col = MaxRows - col;
				if (hDescending)
					row = MaxColumns - row;
			}
			else
			{
				if (hDescending)
					col = MaxColumns - col;
				if (vDescending)
					row = MaxRows - row;
			}
			
			System.Drawing.Point p = new System.Drawing.Point(0, 0);
			
			p.X = (int) (col * grid.getHexWidth());
			p.X += grid.getOrigin().X;
			
			if (col % 2 == 0)
				p.Y = (int) (row * grid.getHexSize());
			else
				p.Y = (int) (row * grid.getHexSize() + grid.getHexSize() / 2);
			p.Y += grid.getOrigin().Y;
			
			grid.rotateIfSideways(p);
			return p;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public override int getColumn(ref System.Drawing.Point p)
		{
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			int x = getRawColumn(ref p);
			
			if (vDescending && grid.Sideways)
			{
				x = (MaxRows - x);
			}
			if (hDescending && !grid.Sideways)
			{
				x = (MaxColumns - x);
			}
			
			return x;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual int getRawColumn(ref System.Drawing.Point p)
		{
			p = new System.Drawing.Point(new System.Drawing.Size(p));
			grid.rotateIfSideways(p);
			int x = p.X - grid.getOrigin().X;
			
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			x = (int) System.Math.Floor(x / grid.getHexWidth() + 0.5);
			return x;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public override int getRow(ref System.Drawing.Point p)
		{
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			int ny = getRawRow(ref p);
			
			if (vDescending && !grid.Sideways)
			{
				ny = (MaxRows - ny);
			}
			if (hDescending && grid.Sideways)
			{
				ny = (MaxColumns - ny);
			}
			
			if (stagger)
			{
				if (grid.Sideways)
				{
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					if (getRawColumn(ref p) % 2 != 0)
					{
						if (hDescending)
						{
							ny--;
						}
						else
						{
							ny++;
						}
					}
				}
				else
				{
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					if (getRawColumn(ref p) % 2 != 0)
					{
						if (vDescending)
						{
							ny--;
						}
						else
						{
							ny++;
						}
					}
				}
			}
			return ny;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual int getRawRow(ref System.Drawing.Point p)
		{
			p = new System.Drawing.Point(new System.Drawing.Size(p));
			grid.rotateIfSideways(p);
			System.Drawing.Point origin = grid.getOrigin();
			double dx = grid.getHexWidth();
			double dy = grid.getHexSize();
			//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
			int nx = (int) System.Math.Round((p.X - origin.X) / dx);
			int ny;
			if (nx % 2 == 0)
			{
				//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
				ny = (int) System.Math.Round((p.Y - origin.Y) / dy);
			}
			else
			{
				//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
				ny = (int) System.Math.Round((p.Y - origin.Y - dy / 2) / dy);
			}
			return ny;
		}
		
		public override void  removeFrom(Buildable parent)
		{
			grid.setGridNumbering(null);
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			System.Windows.Forms.Form f = new System.Windows.Forms.Form();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control2;
			temp_Control2 = new TestPanel();
			f.Controls.Add(temp_Control2);
			//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
			Toolkit.getDefaultToolkit();
			System.Drawing.Size screenSize = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
			screenSize.Height -= 100;
			screenSize.Width -= 100;
			//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_javaawtDimension'"
			f.Size = screenSize;
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			f.Visible = true;
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			f.Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter().windowClosing);
		}
		static HexGridNumbering()
		{
			logger = LoggerFactory.getLogger(typeof(HexGridNumbering));
		}
	}
}