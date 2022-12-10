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
using GameModule = VassalSharp.build.GameModule;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Drawable = VassalSharp.build.module.map.Drawable;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using Resources = VassalSharp.i18n.Resources;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using GamePieceOp = VassalSharp.tools.imageop.GamePieceOp;
using Op = VassalSharp.tools.imageop.Op;
using RotateScaleOp = VassalSharp.tools.imageop.RotateScaleOp;
namespace VassalSharp.counters
{
	
	/// <summary> A Decorator that rotates a GamePiece to an arbitrary angle</summary>
	public class FreeRotator:Decorator, EditablePiece, Drawable, TranslatablePiece
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < String > l = new ArrayList < String >();
			l.add(name + FACING);
			l.add(name + DEGREES);
			return l;
		}
		virtual protected internal GamePieceOp GpOp
		{
			get
			{
				if (gpOp == null)
				{
					if (getInner() != null)
					{
						gpOp = Op.piece(getInner());
					}
				}
				return gpOp;
			}
			
		}
		virtual public double Angle
		{
			get
			{
				return useUnrotatedShape?0.0:validAngles[angleIndex];
			}
			
			set
			{
				if (validAngles.Length == 1)
				{
					validAngles[angleIndex] = value;
				}
				else
				{
					// Find nearest valid angle
					int newIndex = angleIndex;
					double minDist = System.Math.Abs((validAngles[angleIndex] - value + 360) % 360);
					for (int i = 0; i < validAngles.Length; ++i)
					{
						if (minDist > System.Math.Abs((validAngles[i] - value + 360) % 360))
						{
							newIndex = i;
							minDist = System.Math.Abs((validAngles[i] - value + 360) % 360);
						}
					}
					angleIndex = newIndex;
				}
			}
			
		}
		virtual public double CumulativeAngle
		{
			get
			{
				double angle = Angle;
				// Add cumulative angle of any other FreeRotator trait in this piece
				FreeRotator nextRotation = (FreeRotator) Decorator.getDecorator(getInner(), typeof(FreeRotator));
				if (nextRotation != null)
				{
					angle += nextRotation.CumulativeAngle;
				}
				return angle;
			}
			
		}
		virtual public double CumulativeAngleInRadians
		{
			get
			{
				return (- PI_180) * CumulativeAngle;
			}
			
		}
		virtual public System.Drawing.Rectangle RotatedBounds
		{
			get
			{
				return boundingBox();
			}
			
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Drawing.Drawing2D.GraphicsPath s = piece.Shape;
				
				if (Angle == 0.0)
				{
					return s;
				}
				
				//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.createTransformedShape' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformcreateTransformedShape_javaawtShape'"
				System.Drawing.Drawing2D.Matrix temp_Matrix;
				temp_Matrix = new System.Drawing.Drawing2D.Matrix();
				temp_Matrix.RotateAt((float) (AngleInRadians * (180 / System.Math.PI)), new System.Drawing.PointF((float) centerX(), (float) centerY()));
				return temp_Matrix.createTransformedShape(s);
			}
			
		}
		virtual public double AngleInRadians
		{
			get
			{
				return (- PI_180) * Angle;
			}
			
		}
		private System.Drawing.Point GhostPosition
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Drawing2D.Matrix temp_Matrix;
				temp_Matrix = new System.Drawing.Drawing2D.Matrix();
				temp_Matrix.RotateAt((float) ((- PI_180) * (tempAngle - Angle) * (180 / System.Math.PI)), new System.Drawing.PointF((float) (pivot.X + centerX()), (float) (pivot.Y + centerY())));
				System.Drawing.Drawing2D.Matrix t = temp_Matrix;
				//UPGRADE_NOTE: Final was removed from the declaration of 'newPos2D '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.PointF newPos2D = new System.Drawing.PointF(Position.X, Position.Y);
				//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.transform' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformtransform_javaawtgeomPoint2D_javaawtgeomPoint2D'"
				t.transform(newPos2D, newPos2D);
				//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
				return new System.Drawing.Point((int) System.Math.Round((double) newPos2D.X), (int) System.Math.Round((double) newPos2D.Y));
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				System.String d = "Can Rotate";
				if (name.Length > 0)
				{
					d += (" - " + name);
				}
				return d;
			}
			
		}
		virtual public VassalSharp.build.module.documentation.HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("Rotate.htm");
			}
			
		}
		public const System.String ID = "rotate;";
		
		public const System.String FACING = "_Facing";
		
		public const System.String DEGREES = "_Degrees";
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'PI_180 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly double PI_180 = System.Math.PI / 180.0;
		
		protected internal KeyCommand setAngleCommand;
		protected internal KeyCommand rotateCWCommand;
		protected internal KeyCommand rotateCCWCommand;
		protected internal KeyCommand[] commands;
		protected internal NamedKeyStroke setAngleKey;
		protected internal System.String setAngleText = "Rotate";
		protected internal NamedKeyStroke rotateCWKey;
		protected internal System.String rotateCWText = "Rotate CW";
		protected internal NamedKeyStroke rotateCCWKey;
		protected internal System.String rotateCCWText = "Rotate CCW";
		protected internal System.String name = "Rotate";
		
		// for Random Rotate
		protected internal KeyCommand rotateRNDCommand;
		protected internal System.String rotateRNDText = "";
		protected internal NamedKeyStroke rotateRNDKey;
		// END for Random Rotate
		
		protected internal bool useUnrotatedShape;
		
		protected internal double[] validAngles = new double[]{0.0};
		protected internal int angleIndex = 0;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected java.util.Map < Double, Image > images = 
		new HashMap < Double, Image >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected java.util.Map < Double, Rectangle > bounds = 
		new HashMap < Double, Rectangle >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		protected internal PieceImage unrotated;
		protected internal GamePieceOp gpOp;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected java.util.Map < Double, RotateScaleOp > rotOp = 
		new HashMap < Double, RotateScaleOp >();
		
		protected internal double tempAngle, startAngle;
		protected internal System.Drawing.Point pivot;
		protected internal bool drawGhost;
		
		protected internal Map startMap;
		protected internal System.Drawing.Point startPosition;
		
		public FreeRotator():this(ID + "6;];[;Rotate CW;Rotate CCW;;;;", null)
		{
		}
		
		public FreeRotator(System.String type, GamePiece inner)
		{
			InitBlock();
			mySetType(type);
			setInner(inner);
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		public override void  setInner(GamePiece p)
		{
			// The GamePiece stack can be in an invalid state during a setInner()
			// call, so cannot regenerate gpOp now.
			gpOp = null;
			base.setInner(p);
		}
		
		private double centerX()
		{
			// The center is not on a vertex for pieces with odd widths.
			return (piece.boundingBox().Width % 2) / 2.0;
		}
		
		private double centerY()
		{
			// The center is not on a vertex for pieces with odd heights.
			return (piece.boundingBox().Height % 2) / 2.0;
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle b = piece.boundingBox();
			//UPGRADE_NOTE: Final was removed from the declaration of 'angle '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			double angle = Angle;
			
			if (angle == 0.0)
			{
				return b;
			}
			
			System.Drawing.Rectangle r;
			if ((GpOp != null && GpOp.Changed) || (r = bounds.get_Renamed(angle)).IsEmpty)
			{
				
				//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.createTransformedShape' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformcreateTransformedShape_javaawtShape'"
				System.Drawing.Drawing2D.Matrix temp_Matrix;
				temp_Matrix = new System.Drawing.Drawing2D.Matrix();
				temp_Matrix.RotateAt((float) (AngleInRadians * (180 / System.Math.PI)), new System.Drawing.PointF((float) centerX(), (float) centerY()));
				r = System.Drawing.Rectangle.Truncate(temp_Matrix.createTransformedShape(b).GetBounds());
				bounds.put(angle, r);
			}
			
			System.Drawing.Rectangle temp_Rectangle;
			temp_Rectangle = r;
			return new System.Drawing.Rectangle(temp_Rectangle.Location, temp_Rectangle.Size);
		}
		
		/// <deprecated> Use {@link boundingBox()} instead. 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		
		public virtual void  mySetType(System.String type)
		{
			type = type.Substring(ID.Length);
			//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			validAngles = new double[System.Int32.Parse(st.nextToken())];
			for (int i = 0; i < validAngles.Length; ++i)
			{
				validAngles[i] = (- i) * (360.0 / validAngles.Length);
			}
			if (validAngles.Length == 1)
			{
				setAngleKey = st.nextNamedKeyStroke(null);
				if (st.hasMoreTokens())
				{
					setAngleText = st.nextToken();
				}
			}
			else
			{
				rotateCWKey = st.nextNamedKeyStroke(null);
				rotateCCWKey = st.nextNamedKeyStroke(null);
				rotateCWText = st.nextToken("");
				rotateCCWText = st.nextToken("");
			}
			// for random rotation
			rotateRNDKey = st.nextNamedKeyStroke(null);
			rotateRNDText = st.nextToken("");
			// end for random rotation
			name = st.nextToken("");
			commands = null;
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			if (Angle == 0.0)
			{
				piece.draw(g, x, y, obs, zoom);
			}
			else
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'angle '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				double angle = Angle;
				RotateScaleOp op;
				
				if (GpOp != null && GpOp.Changed)
				{
					gpOp = Op.piece(piece);
					bounds.clear();
					rotOp.clear();
					op = Op.rotateScale(gpOp, angle, zoom);
					rotOp.put(angle, op);
				}
				else
				{
					op = rotOp.get_Renamed(angle);
					if (op == null || op.Scale != zoom)
					{
						op = Op.rotateScale(gpOp, angle, zoom);
						rotOp.put(angle, op);
					}
				}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle r = boundingBox();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'img '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Image img = op.getImage();
				if (img != null)
				{
					//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					g.DrawImage(img, x + (int) (zoom * r.X), y + (int) (zoom * r.Y));
				}
			}
		}
		
		public virtual void  draw(System.Drawing.Graphics g, Map map)
		{
			if (drawGhost)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point p = map.componentCoordinates(GhostPosition);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'g2d '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Graphics.create' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Drawing.Graphics g2d = (System.Drawing.Graphics) SupportClass.GraphicsManager.CreateGraphics(g);
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.transform' was converted to 'System.Drawing.Graphics.Transform' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Dtransform_javaawtgeomAffineTransform'"
				System.Drawing.Drawing2D.Matrix temp_Matrix;
				temp_Matrix = new System.Drawing.Drawing2D.Matrix();
				temp_Matrix.RotateAt((float) ((- PI_180) * tempAngle * (180 / System.Math.PI)), new System.Drawing.PointF((float) (p.X + centerX()), (float) (p.Y + centerY())));
				g2d.Transform = temp_Matrix;
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetComposite_javaawtComposite'"
				//UPGRADE_ISSUE: Method 'java.awt.AlphaComposite.getInstance' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
				//UPGRADE_ISSUE: Field 'java.awt.AlphaComposite.SRC_OVER' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
				g2d.setComposite(AlphaComposite.getInstance(AlphaComposite.SRC_OVER, 0.5f));
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_INTERPOLATION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_INTERPOLATION_f'"
				g2d.setRenderingHint(RenderingHints.KEY_INTERPOLATION, (System.Object) System.Drawing.Drawing2D.InterpolationMode.Bilinear);
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_ANTIALIASING_f'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_ANTIALIAS_ON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_ANTIALIAS_ON_f'"
				g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
				piece.draw(g2d, p.X, p.Y, map.getView(), map.Zoom);
				g2d.Dispose();
			}
		}
		
		public virtual bool drawAboveCounters()
		{
			return true;
		}
		
		public override System.String myGetType()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(validAngles.Length);
			if (validAngles.Length == 1)
			{
				se.append(setAngleKey);
				se.append(setAngleText);
			}
			else
			{
				se.append(rotateCWKey).append(rotateCCWKey).append(rotateCWText).append(rotateCCWText);
			}
			// for random rotation
			se.append(rotateRNDKey).append(rotateRNDText);
			// end for random rotation
			se.append(name);
			return ID + se.Value;
		}
		
		public override System.String myGetState()
		{
			if (validAngles.Length == 1)
			{
				return System.Convert.ToString(validAngles[0]);
			}
			else
			{
				return System.Convert.ToString(angleIndex);
			}
		}
		
		public override void  mySetState(System.String state)
		{
			if (validAngles.Length == 1)
			{
				try
				{
					validAngles[0] = System.Double.Parse(state);
				}
				catch (System.FormatException e)
				{
					reportDataError(this, Resources.getString("Error.non_number_error"), "Angle=" + state, e);
				}
			}
			else
			{
				try
				{
					angleIndex = System.Int32.Parse(state);
				}
				catch (System.FormatException e)
				{
					reportDataError(this, Resources.getString("Error.non_number_error"), "Fixed Angle Index=" + state, e);
				}
			}
		}
		
		public override KeyCommand[] myGetKeyCommands()
		{
			if (commands == null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final ArrayList < KeyCommand > l = new ArrayList < KeyCommand >();
				//UPGRADE_NOTE: Final was removed from the declaration of 'outer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GamePiece outer = Decorator.getOutermost(this);
				setAngleCommand = new KeyCommand(setAngleText, setAngleKey, outer, this);
				rotateCWCommand = new KeyCommand(rotateCWText, rotateCWKey, outer, this);
				rotateCCWCommand = new KeyCommand(rotateCCWText, rotateCCWKey, outer, this);
				
				// for random rotation
				rotateRNDCommand = new KeyCommand(rotateRNDText, rotateRNDKey, outer, this);
				// end random rotation
				
				if (validAngles.Length == 1)
				{
					if (setAngleText.Length > 0)
					{
						l.add(setAngleCommand);
					}
					else
					{
						setAngleCommand.setEnabled(false);
					}
					rotateCWCommand.setEnabled(false);
					rotateCCWCommand.setEnabled(false);
				}
				else
				{
					if (rotateCWText.Length > 0 && rotateCCWText.Length > 0)
					{
						l.add(rotateCWCommand);
						l.add(rotateCCWCommand);
					}
					else if (rotateCWText.Length > 0)
					{
						l.add(rotateCWCommand);
						rotateCCWCommand.setEnabled(rotateCCWKey != null);
					}
					else if (rotateCCWText.Length > 0)
					{
						l.add(rotateCCWCommand);
						rotateCWCommand.setEnabled(rotateCWKey != null);
					}
					setAngleCommand.setEnabled(false);
				}
				// for random rotate
				if (rotateRNDText.Length > 0)
				{
					l.add(rotateRNDCommand);
				}
				// end for random rotate
				commands = l.toArray(new KeyCommand[l.size()]);
			}
			setAngleCommand.setEnabled(getMap() != null && validAngles.Length == 1 && setAngleText.Length > 0);
			return commands;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			myGetKeyCommands();
			Command c = null;
			if (setAngleCommand.matches(stroke))
			{
				beginInteractiveRotate();
			}
			else if (rotateCWCommand.matches(stroke))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'tracker '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ChangeTracker tracker = new ChangeTracker(this);
				angleIndex = (angleIndex + 1) % validAngles.Length;
				c = tracker.ChangeCommand;
			}
			else if (rotateCCWCommand.matches(stroke))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'tracker '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ChangeTracker tracker = new ChangeTracker(this);
				angleIndex = (angleIndex - 1 + validAngles.Length) % validAngles.Length;
				c = tracker.ChangeCommand;
			}
			// for random rotation
			else if (rotateRNDCommand.matches(stroke))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'tracker '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ChangeTracker tracker = new ChangeTracker(this);
				// get random #
				//UPGRADE_NOTE: Final was removed from the declaration of 'rand '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Random rand = GameModule.getGameModule().getRNG();
				if (validAngles.Length == 1)
				{
					// we are a free rotate, set angle to 0-360 use setAngle(double)
					Angle = rand.NextDouble() * 360;
				}
				else
				{
					// we are set rotate, set angleIndex to a number between 0 and
					// validAngles.lenth
					angleIndex = (rand.Next(validAngles.Length));
				}
				c = tracker.ChangeCommand;
			}
			// end random rotation
			return c;
		}
		
		public virtual void  beginInteractiveRotate()
		{
			startPosition = Position;
			startMap = getMap();
			startMap.pushMouseListener(this);
			startMap.addDrawComponent(this);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'view '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Control view = startMap.getView();
			view.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouseMoved);
			//UPGRADE_ISSUE: Member 'java.awt.Cursor.getPredefinedCursor' was converted to 'System.Windows.Forms.Cursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
			//UPGRADE_ISSUE: Member 'java.awt.Cursor.CROSSHAIR_CURSOR' was converted to 'System.Windows.Forms.Cursors.Cross' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
			view.Cursor = System.Windows.Forms.Cursors.Cross;
			
			startMap.disableKeyListeners();
			
			pivot = Position;
		}
		
		public virtual void  endInteractiveRotate()
		{
			if (startMap != null)
			{
				startMap.getView().setCursor(null);
				startMap.removeDrawComponent(this);
				startMap.popMouseListener();
				startMap.getView().removeMouseMotionListener(this);
				startMap.enableKeyListeners();
				drawGhost = false;
				startMap = null;
			}
		}
		
		/// <summary> Has the piece been moved by a Global key command since interactive
		/// rotate mode was turned on?
		/// </summary>
		public virtual bool hasPieceMoved()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Map m = getMap();
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Point p = Position;
			return m == null || m != startMap || p.IsEmpty || !p.Equals(startPosition);
		}
		
		/// <summary>The point around which the piece will pivot while rotating interactively </summary>
		public virtual void  setPivot(int x, int y)
		{
			pivot = new System.Drawing.Point(x, y);
		}
		
		public virtual void  mouseClicked(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseEntered(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseExited(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (hasPieceMoved())
			{
				endInteractiveRotate();
				return ;
			}
			drawGhost = true;
			System.Drawing.Point tempAux = new System.Drawing.Point(e.X, e.Y);
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			System.Drawing.Point tempAux2 = Position;
			startAngle = getRelativeAngle(ref tempAux, ref tempAux2);
		}
		
		public virtual void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (hasPieceMoved())
			{
				endInteractiveRotate();
				return ;
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Map m = getMap();
			
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'ghostPosition '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point ghostPosition = GhostPosition;
				Command c = null;
				//UPGRADE_NOTE: Final was removed from the declaration of 'tracker '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ChangeTracker tracker = new ChangeTracker(this);
				if (!Position.Equals(ghostPosition))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'outer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					GamePiece outer = Decorator.getOutermost(this);
					outer.setProperty(VassalSharp.counters.Properties_Fields.MOVED, (System.Object) true);
					c = m.placeOrMerge(outer, m.snapTo(ghostPosition));
				}
				Angle = tempAngle;
				c = tracker.ChangeCommand.append(c);
				
				GameModule.getGameModule().sendAndLog(c);
			}
			finally
			{
				endInteractiveRotate();
			}
		}
		
		public override void  setProperty(System.Object key, System.Object val)
		{
			if (VassalSharp.counters.Properties_Fields.USE_UNROTATED_SHAPE.Equals(key))
			{
				useUnrotatedShape = true.Equals(val);
			}
			base.setProperty(key, val);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.Object getLocalizedProperty(System.Object key)
		{
			if ((name + FACING).Equals(key))
			{
				return System.Convert.ToString(angleIndex + 1);
			}
			else if ((name + DEGREES).Equals(key))
			{
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				return System.Convert.ToString((int) (System.Math.Abs(validAngles[angleIndex])));
			}
			else
			{
				return base.getLocalizedProperty(key);
			}
		}
		
		public override System.Object getProperty(System.Object key)
		{
			if ((name + FACING).Equals(key))
			{
				return System.Convert.ToString(angleIndex + 1);
			}
			else if ((name + DEGREES).Equals(key))
			{
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				return System.Convert.ToString((int) (System.Math.Abs(validAngles[angleIndex])));
			}
			else
			{
				return base.getProperty(key);
			}
		}
		
		public virtual void  mouseDragged(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (drawGhost)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'mousePos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point mousePos = getMap().mapCoordinates(new System.Drawing.Point(e.X, e.Y));
				//UPGRADE_NOTE: Final was removed from the declaration of 'myAngle '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				double myAngle = getRelativeAngle(ref mousePos, ref pivot);
				tempAngle = Angle - (myAngle - startAngle) / PI_180;
			}
			getMap().repaint();
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		private double getRelativeAngle(ref System.Drawing.Point p, ref System.Drawing.Point origin)
		{
			double myAngle;
			if (p.Y == origin.Y)
			{
				myAngle = p.X < origin.X?(- System.Math.PI) / 2.0:System.Math.PI / 2.0;
			}
			else
			{
				myAngle = System.Math.Atan((double) (p.X - origin.X) / (double) (origin.Y - p.Y));
				if (origin.Y < p.Y)
				{
					myAngle += System.Math.PI;
				}
			}
			return myAngle;
		}
		
		public virtual void  mouseMoved(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (hasPieceMoved())
			{
				endInteractiveRotate();
				return ;
			}
		}
		
		/// <summary> Return a full-scale cached image of this piece, rotated to the appropriate
		/// angle.
		/// 
		/// </summary>
		/// <param name="angle">
		/// </param>
		/// <param name="obs">
		/// </param>
		/// <returns>
		/// </returns>
		/// <deprecated> Use a {@link GamePieceOp} if you need this Image.
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public virtual System.Drawing.Image getRotatedImage(double angle, System.Windows.Forms.Control obs)
		{
			if (gpOp == null)
				return null;
			
			if (gpOp.Changed)
				gpOp = Op.piece(piece);
			
			return Op.rotateScale(gpOp, angle, 1.0).getImage();
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(new System.String[]{setAngleText, rotateCWText, rotateCCWText, rotateRNDText}, new System.String[]{getCommandDescription(name, "Set Angle command"), getCommandDescription(name, "Rotate CW command"), getCommandDescription(name, "Rotate CCW command"), getCommandDescription(name, "Rotate Random command")});
		}
		
		/// <summary> Return Property names exposed by this trait</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getPropertyNames()
		
		private class Ed : PieceEditor
		{
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return panel;
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SequenceEncoder se = new SequenceEncoder(';');
					if (true.Equals(anyConfig.getValue()))
					{
						se.append("1").append(anyKeyConfig.ValueString).append(anyCommand.Text == null?"":anyCommand.Text.Trim());
					}
					else
					{
						se.append(facingsConfig.ValueString).append(cwKeyConfig.ValueString).append(ccwKeyConfig.ValueString).append(cwCommand.Text == null?"":cwCommand.Text.Trim()).append(ccwCommand.Text == null?"":ccwCommand.Text.Trim());
					}
					// random rotate
					se.append(rndKeyConfig.ValueString).append(rndCommand.Text == null?"":rndCommand.Text.Trim());
					// end random rotate
					se.append(nameConfig.ValueString);
					return VassalSharp.counters.FreeRotator.ID + se.Value;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return "0";
				}
				
			}
			private BooleanConfigurer anyConfig;
			private NamedHotKeyConfigurer anyKeyConfig;
			private IntConfigurer facingsConfig;
			private NamedHotKeyConfigurer cwKeyConfig;
			private NamedHotKeyConfigurer ccwKeyConfig;
			// random rotate
			private NamedHotKeyConfigurer rndKeyConfig;
			// end random rotate
			private StringConfigurer nameConfig;
			
			private System.Windows.Forms.TextBox anyCommand;
			private System.Windows.Forms.TextBox cwCommand;
			private System.Windows.Forms.TextBox ccwCommand;
			private System.Windows.Forms.TextBox rndCommand;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			private Box anyControls;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			private Box cwControls;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			private Box ccwControls;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			private Box rndControls;
			
			private System.Windows.Forms.Panel panel;
			
			public Ed(FreeRotator p)
			{
				nameConfig = new StringConfigurer(null, "Description:  ", p.name);
				cwKeyConfig = new NamedHotKeyConfigurer(null, "Command to rotate clockwise:  ", p.rotateCWKey);
				ccwKeyConfig = new NamedHotKeyConfigurer(null, "Command to rotate counterclockwise:  ", p.rotateCCWKey);
				// random rotate
				rndKeyConfig = new NamedHotKeyConfigurer(null, "Command to rotate randomly:  ", p.rotateRNDKey);
				// end random rotate
				anyConfig = new BooleanConfigurer(null, "Allow arbitrary rotations", Boolean.valueOf(p.validAngles.Length == 1));
				anyKeyConfig = new NamedHotKeyConfigurer(null, "Command to rotate:  ", p.setAngleKey);
				facingsConfig = new IntConfigurer(null, "Number of allowed facings:  ", p.validAngles.Length == 1?6:p.validAngles.Length);
				
				panel = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				panel.setLayout(new BoxLayout(panel, BoxLayout.Y_AXIS));
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(nameConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(facingsConfig.Controls);
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				cwControls = Box.createHorizontalBox();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				cwControls.Controls.Add(cwKeyConfig.Controls);
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = " Menu text:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				cwControls.Controls.Add(temp_Control);
				//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
				cwCommand = new System.Windows.Forms.TextBox();
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				cwCommand.setMaximumSize(cwCommand.Size);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				cwCommand.Text = p.rotateCWText;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				cwControls.Controls.Add(cwCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(cwControls);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				ccwControls = Box.createHorizontalBox();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				ccwControls.Controls.Add(ccwKeyConfig.Controls);
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = " Menu text:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label4;
				ccwControls.Controls.Add(temp_Control2);
				//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
				ccwCommand = new System.Windows.Forms.TextBox();
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				ccwCommand.setMaximumSize(ccwCommand.Size);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				ccwCommand.Text = p.rotateCCWText;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				ccwControls.Controls.Add(ccwCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(ccwControls);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(anyConfig.Controls);
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				anyControls = Box.createHorizontalBox();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				anyControls.Controls.Add(anyKeyConfig.Controls);
				System.Windows.Forms.Label temp_label6;
				temp_label6 = new System.Windows.Forms.Label();
				temp_label6.Text = " Menu text:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control3;
				temp_Control3 = temp_label6;
				anyControls.Controls.Add(temp_Control3);
				//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
				anyCommand = new System.Windows.Forms.TextBox();
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				anyCommand.setMaximumSize(anyCommand.Size);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				anyCommand.Text = p.setAngleText;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				anyControls.Controls.Add(anyCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(anyControls);
				
				// random rotate
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				rndControls = Box.createHorizontalBox();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				rndControls.Controls.Add(rndKeyConfig.Controls);
				System.Windows.Forms.Label temp_label8;
				temp_label8 = new System.Windows.Forms.Label();
				temp_label8.Text = " Menu text:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control4;
				temp_Control4 = temp_label8;
				rndControls.Controls.Add(temp_Control4);
				//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
				rndCommand = new System.Windows.Forms.TextBox();
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				rndCommand.setMaximumSize(rndCommand.Size);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				rndCommand.Text = p.rotateRNDText;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				rndControls.Controls.Add(rndCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(rndControls);
				// end random rotate
				
				anyConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(this.propertyChange);
				propertyChange(null);
			}
			
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'any '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool any = true.Equals(anyConfig.getValue());
				anyControls.Visible = any;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(facingsConfig.Controls, "Visible", !any);
				cwControls.Visible = !any;
				ccwControls.Visible = !any;
				panel.Invalidate();
			}
		}
	}
}