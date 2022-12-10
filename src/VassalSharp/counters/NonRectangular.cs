/*
* $Id$
*
* Copyright (c) 2003 by Rodney Kinney
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
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
using Op = VassalSharp.tools.imageop.Op;
namespace VassalSharp.counters
{
	
	/// <summary> A trait for assigning an arbitrary shape to a {@link GamePiece}
	/// 
	/// </summary>
	/// <seealso cref="GamePiece.getShape">
	/// </seealso>
	public class NonRectangular:Decorator, EditablePiece
	{
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				return shape != null?shape:piece.Shape;
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				return "Non-Rectangular";
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("NonRectangular.htm");
			}
			
		}
		public const System.String ID = "nonRect;";
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private static HashMap < String, Shape > shapeCache = new HashMap < String, Shape >();
		
		private System.String type;
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Drawing.Drawing2D.GraphicsPath shape;
		
		public NonRectangular():this(ID, null)
		{
		}
		
		public NonRectangular(System.String type, GamePiece inner)
		{
			mySetType(type);
			setInner(inner);
		}
		
		public override void  mySetState(System.String newState)
		{
		}
		
		public override System.String myGetState()
		{
			return "";
		}
		
		public override System.String myGetType()
		{
			return type;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			return new KeyCommand[0];
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			return null;
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			piece.draw(g, x, y, obs, zoom);
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			return piece.boundingBox();
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		public virtual void  mySetType(System.String type)
		{
			this.type = type;
			//UPGRADE_NOTE: Final was removed from the declaration of 'shapeSpec '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String shapeSpec = type.Substring(ID.Length);
			shape = buildPath(shapeSpec);
		}
		
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Drawing.Drawing2D.GraphicsPath buildPath(System.String spec)
		{
			//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Drawing.Drawing2D.GraphicsPath sh = shapeCache.get_Renamed(spec);
			
			if (sh == null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'path '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Drawing2D.GraphicsPath temp_GraphicsPath;
				temp_GraphicsPath = new System.Drawing.Drawing2D.GraphicsPath();
				temp_GraphicsPath.FillMode = System.Drawing.Drawing2D.FillMode.Winding;
				System.Drawing.Drawing2D.GraphicsPath path = temp_GraphicsPath;
				//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SupportClass.Tokenizer st = new SupportClass.Tokenizer(spec, ",");
				if (st.HasMoreTokens())
				{
					while (st.HasMoreTokens())
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'token '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String token = st.NextToken();
						switch (token[0])
						{
							
							case 'c': 
								path.CloseFigure();
								break;
							
							case 'm': 
								//UPGRADE_TODO: Method 'java.awt.geom.GeneralPath.moveTo' was converted to 'System.Drawing.Drawing2D.GraphicsPath.AddLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomGeneralPathmoveTo_float_float'"
								//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
								path.AddLine(System.Int32.Parse(st.NextToken()), System.Int32.Parse(st.NextToken()), System.Int32.Parse(st.NextToken()), System.Int32.Parse(st.NextToken()));
								break;
							
							case 'l': 
								path.AddLine(path.PathPoints[path.PathPoints.Length - 1].X, path.PathPoints[path.PathPoints.Length - 1].Y, System.Int32.Parse(st.NextToken()), System.Int32.Parse(st.NextToken()));
								break;
							}
					}
					sh = new System.Drawing.Region(path);
					shapeCache.put(spec, sh);
				}
			}
			
			return sh;
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this, this);
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Ed' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class Ed : PieceEditor
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassJPanel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			[Serializable]
			private class AnonymousClassJPanel:System.Windows.Forms.Panel
			{
				public AnonymousClassJPanel(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
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
					//UPGRADE_NOTE: Final was removed from the declaration of 'g2d '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Graphics g2d = (System.Drawing.Graphics) g;
					SupportClass.GraphicsManager.manager.SetColor(g2d, System.Drawing.Color.White);
					g2d.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g2d), 0, 0, Width, Height);
					if (Enclosing_Instance.shape != null)
					{
						g2d.TranslateTransform((System.Single) (Width / 2), (System.Single) (Height / 2));
						SupportClass.GraphicsManager.manager.SetColor(g2d, System.Drawing.Color.Black);
						//UPGRADE_TODO: Method 'java.awt.Graphics2D.fill' was converted to 'System.Drawing.Graphics.FillPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Dfill_javaawtShape'"
						g2d.FillPath(Enclosing_Instance.shape);
					}
				}
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.JComponent.getPreferredSize' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public System.Drawing.Size getPreferredSize()
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Size d = Enclosing_Instance.shape == null?new System.Drawing.Size(60, 60):System.Drawing.Rectangle.Truncate(Enclosing_Instance.shape.GetBounds()).Size;
					d.Width = System.Math.Max(d.Width, 60);
					d.Height = System.Math.Max(d.Height, 60);
					return d;
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassImagePicker' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			[Serializable]
			private class AnonymousClassImagePicker:ImagePicker
			{
				public AnonymousClassImagePicker(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				private const long serialVersionUID = 1L;
				
				public override void  setImageName(System.String name)
				{
					base.setImageName(name);
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'img '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Image img = Op.load(name).getImage();
					if (img != null)
						Enclosing_Instance.ShapeFromImage = img;
				}
			}
			private void  InitBlock(NonRectangular enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private NonRectangular enclosingInstance;
			virtual public System.Drawing.Image ShapeFromImage
			{
				set
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
					//UPGRADE_ISSUE: Member 'java.awt.Cursor.getPredefinedCursor' was converted to 'System.Windows.Forms.Cursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
					//UPGRADE_ISSUE: Member 'java.awt.Cursor.HAND_CURSOR' was converted to 'System.Windows.Forms.Cursors.Hand' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
					controls.getTopLevelAncestor().Cursor = System.Windows.Forms.Cursors.Hand;
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'bi '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Bitmap bi = ImageUtils.toBufferedImage(value);
					//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int w = bi.Width;
					//UPGRADE_NOTE: Final was removed from the declaration of 'h '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int h = bi.Height;
					//UPGRADE_NOTE: Final was removed from the declaration of 'pixels '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.getRGB' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImagegetRGB_int_int_int_int_int[]_int_int'"
					int[] pixels = bi.getRGB(0, 0, w, h, new int[w * h], 0, w);
					
					// build the outline in strips
					//UPGRADE_NOTE: Final was removed from the declaration of 'outline '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Region outline = new System.Drawing.Region();
					for (int y = 0; y < h; ++y)
					{
						int left = - 1;
						for (int x = 0; x < w; ++x)
						{
							if (((SupportClass.URShift(pixels[x + y * w], 24)) & 0xff) > 0)
							{
								if (left < 0)
								{
									left = x;
								}
							}
							else if (left > - 1)
							{
								//UPGRADE_TODO: Method 'java.awt.geom.Area.add' was converted to 'System.Drawing.Region.Union' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomAreaadd_javaawtgeomArea'"
								outline.Union(new System.Drawing.Region(new System.Drawing.Rectangle(left, y, x - left, 1)));
								left = - 1;
							}
						}
						
						if (left > - 1)
						{
							//UPGRADE_TODO: Method 'java.awt.geom.Area.add' was converted to 'System.Drawing.Region.Union' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomAreaadd_javaawtgeomArea'"
							outline.Union(new System.Drawing.Region(new System.Drawing.Rectangle(left, y, w - left, 1)));
						}
					}
					
					// FIXME: should be 2.0 to avoid integer arithemtic?
					//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.createTransformedShape' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformcreateTransformedShape_javaawtShape'"
					System.Drawing.Drawing2D.Matrix temp_Matrix;
					temp_Matrix = new System.Drawing.Drawing2D.Matrix();
					temp_Matrix.Translate((float) ((- w) / 2), (float) ((- h) / 2));
					shape = temp_Matrix.createTransformedShape(outline);
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'wd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
					System.Windows.Forms.Form wd = (System.Windows.Forms.Form) controls.Parent;
					if (wd != null)
					{
						//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
						wd.pack();
					}
					
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
					controls.getTopLevelAncestor().Cursor = null;
				}
				
			}
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return controls;
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					StringBuilder buffer = new StringBuilder();
					if (shape != null)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'it '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						//UPGRADE_TODO: Interface 'java.awt.geom.PathIterator' was converted to 'System.Drawing.Drawing2D.GraphicsPathIterator' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
						new System.Drawing.Drawing2D.Matrix();
						//UPGRADE_TODO: Method 'java.awt.Shape.getPathIterator' was converted to 'System.Drawing.Drawing2D.GraphicsPathIterator.GraphicsPathIterator' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtShapegetPathIterator_javaawtgeomAffineTransform'"
						System.Drawing.Drawing2D.GraphicsPathIterator it = new System.Drawing.Drawing2D.GraphicsPathIterator(shape);
						//UPGRADE_NOTE: Final was removed from the declaration of 'pts '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						float[] pts = new float[6];
						//UPGRADE_ISSUE: Method 'java.awt.geom.PathIterator.isDone' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomPathIteratorisDone'"
						while (!it.isDone())
						{
							//UPGRADE_ISSUE: Method 'java.awt.geom.PathIterator.currentSegment' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomPathIteratorcurrentSegment_float[]'"
							switch (it.currentSegment(pts))
							{
								
								//UPGRADE_ISSUE: Field 'java.awt.geom.PathIterator.SEG_MOVETO' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomPathIteratorSEG_MOVETO_f'"
								case PathIterator.SEG_MOVETO: 
									//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_float'"
									buffer.append('m').append(',').append((int) System.Math.Round((double) pts[0])).append(',').append((int) System.Math.Round((double) pts[1]));
									break;
								
								//UPGRADE_ISSUE: Field 'java.awt.geom.PathIterator.SEG_LINETO' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomPathIteratorSEG_LINETO_f'"
								case PathIterator.SEG_LINETO: 
								//UPGRADE_ISSUE: Field 'java.awt.geom.PathIterator.SEG_CUBICTO' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomPathIteratorSEG_CUBICTO_f'"
								case PathIterator.SEG_CUBICTO: 
								//UPGRADE_ISSUE: Field 'java.awt.geom.PathIterator.SEG_QUADTO' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomPathIteratorSEG_QUADTO_f'"
								case PathIterator.SEG_QUADTO: 
									//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_float'"
									buffer.append('l').append(',').append((int) System.Math.Round((double) pts[0])).append(',').append((int) System.Math.Round((double) pts[1]));
									break;
								
								//UPGRADE_ISSUE: Field 'java.awt.geom.PathIterator.SEG_CLOSE' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomPathIteratorSEG_CLOSE_f'"
								case PathIterator.SEG_CLOSE: 
									buffer.append('c');
									break;
								}
							//UPGRADE_ISSUE: Method 'java.awt.geom.PathIterator.next' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomPathIteratornext'"
							it.next();
							//UPGRADE_ISSUE: Method 'java.awt.geom.PathIterator.isDone' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomPathIteratorisDone'"
							if (!it.isDone())
							{
								buffer.append(',');
							}
						}
					}
					return VassalSharp.counters.NonRectangular.ID + buffer.toString();
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return "";
				}
				
			}
			public NonRectangular Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			private System.Drawing.Drawing2D.GraphicsPath shape;
			private System.Windows.Forms.Panel controls;
			
			internal Ed(NonRectangular enclosingInstance, NonRectangular p)
			{
				InitBlock(enclosingInstance);
				shape = p.shape;
				controls = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				controls.setLayout(new BoxLayout(controls, BoxLayout.X_AXIS));
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'shapePanel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Panel shapePanel = new AnonymousClassJPanel(this);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(shapePanel);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'picker '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ImagePicker picker = new AnonymousClassImagePicker(this);
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.border.TitledBorder.TitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingborderTitledBorder'"
				System.Windows.Forms.ControlPaint.DrawBorder3D(picker.CreateGraphics(), 0, 0, picker.Width, picker.Height, new TitledBorder("Use image shape"));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(picker);
			}
		}
	}
}