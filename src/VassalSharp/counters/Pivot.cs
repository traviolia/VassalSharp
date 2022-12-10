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
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using MovementReporter = VassalSharp.build.module.map.MovementReporter;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using MoveTracker = VassalSharp.command.MoveTracker;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using DoubleConfigurer = VassalSharp.configure.DoubleConfigurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> Provides commands to pivot a Game Piece around a given point</summary>
	public class Pivot:Decorator, TranslatablePiece
	{
		virtual public System.String Description
		{
			get
			{
				return "Can Pivot";
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("Pivot.htm");
			}
			
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				return getInner().Shape;
			}
			
		}
		public const System.String ID = "pivot;";
		public const System.String DEGREES = "_Degrees";
		protected internal int pivotX;
		protected internal int pivotY;
		protected internal double angle;
		protected internal System.String command;
		protected internal NamedKeyStroke key;
		protected internal bool fixedAngle;
		protected internal KeyCommand[] commands;
		protected internal KeyCommand pivotCommand;
		protected internal FreeRotator rotator;
		
		public Pivot():this(ID, null)
		{
		}
		
		public Pivot(System.String type, GamePiece inner)
		{
			mySetType(type);
			setInner(inner);
		}
		
		public virtual void  mySetType(System.String type)
		{
			type = type.Substring(ID.Length);
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			command = st.nextToken("Pivot");
			key = st.nextNamedKeyStroke(null);
			pivotX = st.nextInt(0);
			pivotY = st.nextInt(0);
			fixedAngle = st.nextBoolean(true);
			angle = st.nextDouble(90.0);
			commands = null;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			if (commands == null)
			{
				pivotCommand = new KeyCommand(command, key, Decorator.getOutermost(this), this);
				if (command.Length > 0 && key != null && !key.Null)
				{
					commands = new KeyCommand[]{pivotCommand};
				}
				else
				{
					commands = new KeyCommand[0];
				}
				rotator = (FreeRotator) Decorator.getDecorator(this, typeof(FreeRotator));
				pivotCommand.setEnabled(rotator != null);
			}
			return commands;
		}
		
		public override System.String myGetState()
		{
			return "";
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(command).append(key).append(pivotX).append(pivotY).append(fixedAngle).append(angle);
			return ID + se.Value;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			myGetKeyCommands();
			Command c = null;
			if (pivotCommand.matches(stroke))
			{
				if (fixedAngle)
				{
					ChangeTracker t = new ChangeTracker(this);
					double oldAngle = rotator.Angle;
					rotator.Angle = oldAngle - angle;
					double newAngle = rotator.Angle;
					if (getMap() != null)
					{
						setOldProperties();
						System.Drawing.Point pos = Position;
						//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
						pivotPoint(ref pos, (- System.Math.PI) * oldAngle / 180.0, (- System.Math.PI) * newAngle / 180.0);
						GamePiece outer = Decorator.getOutermost(this);
						if (!true.Equals(outer.getProperty(VassalSharp.counters.Properties_Fields.IGNORE_GRID)))
						{
							pos = getMap().snapTo(pos);
						}
						outer.setProperty(VassalSharp.counters.Properties_Fields.MOVED, (System.Object) true);
						c = t.ChangeCommand;
						MoveTracker moveTracker = new MoveTracker(outer);
						getMap().placeOrMerge(outer, pos);
						c = c.append(moveTracker.MoveCommand);
						MovementReporter r = new MovementReporter(c);
						Command reportCommand = r.ReportCommand;
						if (reportCommand != null)
						{
							reportCommand.execute();
						}
						c = c.append(reportCommand);
						c = c.append(r.markMovedPieces());
						getMap().ensureVisible(getMap().selectionBoundsOf(outer));
					}
					else
					{
						c = t.ChangeCommand;
					}
				}
				else if (getMap() != null)
				{
					setOldProperties();
					//UPGRADE_NOTE: Final was removed from the declaration of 'oldAngle '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					double oldAngle = rotator.AngleInRadians;
					System.Drawing.PointF pivot2D = new System.Drawing.PointF((float) pivotX, (float) pivotY);
					System.Drawing.Drawing2D.Matrix temp_Matrix;
					temp_Matrix = new System.Drawing.Drawing2D.Matrix();
					temp_Matrix.Rotate((float) (oldAngle * (180 / System.Math.PI)));
					System.Drawing.Drawing2D.Matrix t = temp_Matrix;
					//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.transform' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformtransform_javaawtgeomPoint2D_javaawtgeomPoint2D'"
					t.transform(pivot2D, pivot2D);
					rotator.beginInteractiveRotate();
					//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
					rotator.setPivot(Position.X + (int) System.Math.Round((double) pivot2D.X), Position.Y + (int) System.Math.Round((double) pivot2D.Y));
				}
			}
			// Apply map auto-move key
			if (c != null && getMap() != null && getMap().getMoveKey() != null)
			{
				c.append(Decorator.getOutermost(this).keyEvent(getMap().getMoveKey()));
			}
			return c;
		}
		
		/// <summary> Pivot the given point around the pivot point from oldAngle to newAngle</summary>
		/// <param name="oldAngle">
		/// </param>
		/// <param name="newAngle">
		/// </param>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		private void  pivotPoint(ref System.Drawing.Point p, double oldAngle, double newAngle)
		{
			System.Drawing.PointF pivot2D = new System.Drawing.PointF((float) pivotX, (float) pivotY);
			System.Drawing.Drawing2D.Matrix temp_Matrix;
			temp_Matrix = new System.Drawing.Drawing2D.Matrix();
			temp_Matrix.Rotate((float) (oldAngle * (180 / System.Math.PI)));
			System.Drawing.Drawing2D.Matrix t = temp_Matrix;
			//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.transform' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformtransform_javaawtgeomPoint2D_javaawtgeomPoint2D'"
			t.transform(pivot2D, pivot2D);
			System.Drawing.Drawing2D.Matrix temp_Matrix2;
			temp_Matrix2 = new System.Drawing.Drawing2D.Matrix();
			temp_Matrix2.RotateAt((float) ((newAngle - oldAngle) * (180 / System.Math.PI)), new System.Drawing.PointF((float) pivot2D.X, (float) pivot2D.Y));
			t = temp_Matrix2;
			System.Drawing.PointF newPos2D = new System.Drawing.PointF(0, 0);
			//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.transform' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformtransform_javaawtgeomPoint2D_javaawtgeomPoint2D'"
			t.transform(newPos2D, newPos2D);
			//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
			p.X += (int) System.Math.Round((double) newPos2D.X);
			//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
			p.Y += (int) System.Math.Round((double) newPos2D.Y);
		}
		
		public override void  mySetState(System.String newState)
		{
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			return getInner().boundingBox();
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			getInner().draw(g, x, y, obs, zoom);
		}
		
		public override System.String getName()
		{
			return getInner().getName();
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(command, "Pivot command");
		}
		
		
		public class Ed : PieceEditor
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener
			{
				public AnonymousClassPropertyChangeListener(Ed enclosingInstance)
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
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
				{
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(Enclosing_Instance.angle.Controls, "Visible", true.Equals(Enclosing_Instance.fixedAngle.getValue()));
				}
			}
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return controls;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return "";
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(command.ValueString).append(key.ValueString).append(xOff.ValueString).append(yOff.ValueString).append(true.Equals(fixedAngle.getValue())).append(angle.ValueString);
					return VassalSharp.counters.Pivot.ID + se.Value;
				}
				
			}
			private StringConfigurer command;
			private NamedHotKeyConfigurer key;
			private IntConfigurer xOff, yOff;
			private DoubleConfigurer angle;
			private BooleanConfigurer fixedAngle;
			private System.Windows.Forms.Panel controls;
			public Ed(Pivot p)
			{
				controls = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				controls.setLayout(new BoxLayout(controls, BoxLayout.Y_AXIS));
				
				command = new StringConfigurer(null, "Command:  ", p.command);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(command.Controls);
				
				key = new NamedHotKeyConfigurer(null, "Keyboard command:  ", p.key);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(key.Controls);
				
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box b = Box.createHorizontalBox();
				xOff = new IntConfigurer(null, "Pivot point:  ", p.pivotX);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(xOff.Controls);
				yOff = new IntConfigurer(null, ", ", p.pivotY);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(yOff.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(b);
				
				fixedAngle = new BooleanConfigurer(null, "Pivot through fixed angle?", Boolean.valueOf(p.fixedAngle));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(fixedAngle.Controls);
				
				angle = new DoubleConfigurer(null, "Angle:  ", p.angle);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(angle.Controls);
				
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(angle.Controls, "Visible", p.fixedAngle);
				fixedAngle.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
			}
		}
	}
}