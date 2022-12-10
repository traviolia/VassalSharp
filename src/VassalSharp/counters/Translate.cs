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
using GameModule = VassalSharp.build.GameModule;
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using MovementReporter = VassalSharp.build.module.map.MovementReporter;
using Board = VassalSharp.build.module.map.boardPicker.Board;
using Command = VassalSharp.command.Command;
using NullCommand = VassalSharp.command.NullCommand;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using FormattedExpressionConfigurer = VassalSharp.configure.FormattedExpressionConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using FormattedString = VassalSharp.tools.FormattedString;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> Give a piece a command that moves it a fixed amount in a particular
	/// direction, optionally tracking the current rotation of the piece.
	/// </summary>
	public class Translate:Decorator, TranslatablePiece
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassNullCommand' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassNullCommand:NullCommand
		{
			public AnonymousClassNullCommand(Translate enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Translate enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Translate enclosingInstance;
			override public bool Null
			{
				get
				{
					return false;
				}
				
			}
			public Translate Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
		}
		virtual public System.String Description
		{
			get
			{
				System.String d = "Move fixed distance";
				if (description.Length > 0)
				{
					d += (" - " + description);
				}
				return d;
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
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("Translate.htm");
			}
			
		}
		private const System.String _0 = "0";
		public const System.String ID = "translate;";
		protected internal KeyCommand[] commands;
		protected internal System.String commandName;
		protected internal NamedKeyStroke keyCommand;
		protected internal FormattedString xDist = new FormattedString("");
		protected internal FormattedString xIndex = new FormattedString("");
		protected internal FormattedString xOffset = new FormattedString("");
		protected internal FormattedString yDist = new FormattedString("");
		protected internal FormattedString yIndex = new FormattedString("");
		protected internal FormattedString yOffset = new FormattedString("");
		protected internal System.String description;
		protected internal bool moveStack;
		protected internal KeyCommand moveCommand;
		protected internal static MoveExecuter mover;
		
		public Translate():this(ID + "Move Forward", null)
		{
		}
		
		public Translate(System.String type, GamePiece inner)
		{
			mySetType(type);
			setInner(inner);
		}
		
		public virtual void  mySetType(System.String type)
		{
			type = type.Substring(ID.Length);
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			commandName = st.nextToken("Move Forward");
			keyCommand = st.nextNamedKeyStroke('M');
			xDist.Format = st.nextToken(_0);
			yDist.Format = st.nextToken("60");
			moveStack = st.nextBoolean(true);
			xIndex.Format = st.nextToken(_0);
			yIndex.Format = st.nextToken(_0);
			xOffset.Format = st.nextToken(_0);
			yOffset.Format = st.nextToken(_0);
			description = st.nextToken("");
			commands = null;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			if (commands == null)
			{
				moveCommand = new KeyCommand(commandName, keyCommand, Decorator.getOutermost(this), this);
				if (commandName.Length > 0 && keyCommand != null && !keyCommand.Null)
				{
					commands = new KeyCommand[]{moveCommand};
				}
				else
				{
					commands = new KeyCommand[0];
				}
			}
			moveCommand.setEnabled(getMap() != null);
			return commands;
		}
		
		public override System.String myGetState()
		{
			return "";
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(commandName).append(keyCommand).append(xDist.Format).append(yDist.Format).append(moveStack).append(xIndex.Format).append(yIndex.Format).append(xOffset.Format).append(yOffset.Format).append(description);
			return ID + se.Value;
		}
		
		public override Command keyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			myGetKeyCommands();
			if (moveCommand.matches(stroke))
			{
				// Delay the execution of the inner piece's key event until this piece has moved
				return myKeyEvent(stroke);
			}
			else
			{
				return base.keyEvent(stroke);
			}
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			myGetKeyCommands();
			Command c = null;
			if (moveCommand.matches(stroke))
			{
				setOldProperties();
				if (mover == null)
				{
					mover = new MoveExecuter();
					mover.KeyEvent = stroke;
					//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
					SwingUtilities.invokeLater(mover);
				}
				GamePiece target = findTarget(stroke);
				if (target != null)
				{
					c = moveTarget(target);
				}
				mover.addKeyEventTarget(piece);
				// Return a non-null command to indicate that a change actually happened
				c = new AnonymousClassNullCommand(this);
			}
			return c;
		}
		
		protected internal virtual Command moveTarget(GamePiece target)
		{
			// Has this piece already got a move scheduled? If so, then we
			// need to use the endpoint of any existing moves as our
			// starting point.
			System.Drawing.Point p = mover.getUpdatedPosition(target);
			
			// First move, so use the current location.
			if (p.IsEmpty)
			{
				p = new System.Drawing.Point(new System.Drawing.Size(Position));
			}
			
			// Perform the move fixed distance
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			translate(ref p);
			
			// Handle rotation of the piece
			FreeRotator myRotation = (FreeRotator) Decorator.getDecorator(this, typeof(FreeRotator));
			if (myRotation != null)
			{
				System.Drawing.PointF myPosition = new System.Drawing.Point(new System.Drawing.Size(Position));
				System.Drawing.PointF p2d = new System.Drawing.Point(new System.Drawing.Size(p));
				//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.transform' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformtransform_javaawtgeomPoint2D_javaawtgeomPoint2D'"
				System.Drawing.Drawing2D.Matrix temp_Matrix;
				temp_Matrix = new System.Drawing.Drawing2D.Matrix();
				temp_Matrix.RotateAt((float) (myRotation.CumulativeAngleInRadians * (180 / System.Math.PI)), new System.Drawing.PointF((float) myPosition.X, (float) myPosition.Y));
				System.Drawing.PointF tempAux = System.Drawing.PointF.Empty;
				p2d = temp_Matrix.transform(p2d, tempAux);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				p = new System.Drawing.Point((int) p2d.X, (int) p2d.Y);
			}
			
			// And snap to the grid if required.
			if (!true.Equals(Decorator.getOutermost(this).getProperty(VassalSharp.counters.Properties_Fields.IGNORE_GRID)))
			{
				p = getMap().snapTo(p);
			}
			
			// Add to the list of scheduled moves
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			mover.add(target.getMap(), target, ref p);
			return null;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual void  translate(ref System.Drawing.Point p)
		{
			int x = 0;
			int y = 0;
			//UPGRADE_NOTE: Final was removed from the declaration of 'outer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GamePiece outer = Decorator.getOutermost(this);
			//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			Board b = outer.getMap().findBoard(ref p);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'Xdist '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int Xdist = xDist.getTextAsInt(outer, "Xdistance", this);
			//UPGRADE_NOTE: Final was removed from the declaration of 'Xindex '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int Xindex = xIndex.getTextAsInt(outer, "Xindex", this);
			//UPGRADE_NOTE: Final was removed from the declaration of 'Xoffset '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int Xoffset = xOffset.getTextAsInt(outer, "Xoffset", this);
			
			x = Xdist + Xindex * Xoffset;
			if (b != null)
			{
				x = (int) Math.round(b.getMagnification() * x);
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'Ydist '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int Ydist = yDist.getTextAsInt(outer, "Ydistance", this);
			//UPGRADE_NOTE: Final was removed from the declaration of 'Yindex '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int Yindex = yIndex.getTextAsInt(outer, "Yindex", this);
			//UPGRADE_NOTE: Final was removed from the declaration of 'Yoffset '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int Yoffset = yOffset.getTextAsInt(outer, "Yoffset", this);
			
			y = Ydist + Yindex * Yoffset;
			if (b != null)
			{
				y = (int) Math.round(b.getMagnification() * y);
			}
			
			p.Offset(x, - y);
		}
		
		protected internal virtual GamePiece findTarget(System.Windows.Forms.KeyEventArgs stroke)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'outer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GamePiece outer = Decorator.getOutermost(this);
			GamePiece target = outer;
			if (moveStack && outer.Parent != null && !outer.Parent.isExpanded())
			{
				// Only move entire stack if this is the top piece
				// Otherwise moves the stack too far if the whole stack is multi-selected
				if (outer != outer.Parent.topPiece(GameModule.getUserId()))
				{
					target = null;
				}
				else
				{
					target = outer.Parent;
				}
			}
			return target;
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
			return new Editor(this);
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(commandName, getCommandDescription(description, "Move Fixed Distance command"));
		}
		
		
		public class Editor : PieceEditor
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener
			{
				public AnonymousClassPropertyChangeListener(Editor enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Editor enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Editor enclosingInstance;
				public Editor Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
				{
					Enclosing_Instance.updateAdvancedVisibility();
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
					se.append(name.ValueString).append(key.ValueString).append(xDist.ValueString).append(yDist.ValueString).append(moveStack.ValueString).append(xIndexInput.ValueString).append(yIndexInput.ValueString).append(xOffsetInput.ValueString).append(yOffsetInput.ValueString).append(descInput.ValueString);
					return VassalSharp.counters.Translate.ID + se.Value;
				}
				
			}
			private FormattedExpressionConfigurer xDist;
			private FormattedExpressionConfigurer yDist;
			private StringConfigurer name;
			private NamedHotKeyConfigurer key;
			private System.Windows.Forms.Panel controls;
			private BooleanConfigurer moveStack;
			protected internal BooleanConfigurer advancedInput;
			protected internal FormattedExpressionConfigurer xIndexInput;
			protected internal FormattedExpressionConfigurer xOffsetInput;
			protected internal FormattedExpressionConfigurer yIndexInput;
			protected internal FormattedExpressionConfigurer yOffsetInput;
			protected internal StringConfigurer descInput;
			
			public Editor(Translate t)
			{
				controls = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				controls.setLayout(new BoxLayout(controls, BoxLayout.Y_AXIS));
				descInput = new StringConfigurer(null, "Description:  ", t.description);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(descInput.Controls);
				name = new StringConfigurer(null, "Command Name:  ", t.commandName);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(name.Controls);
				key = new NamedHotKeyConfigurer(null, "Keyboard shortcut:  ", t.keyCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(key.Controls);
				xDist = new FormattedExpressionConfigurer(null, "Distance to the right:  ", t.xDist.Format, t);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(xDist.Controls);
				yDist = new FormattedExpressionConfigurer(null, "Distance upwards:  ", t.yDist.Format, t);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(yDist.Controls);
				moveStack = new BooleanConfigurer(null, "Move entire stack?", Boolean.valueOf(t.moveStack));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(moveStack.Controls);
				
				advancedInput = new BooleanConfigurer(null, "Advanced Options", false);
				advancedInput.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(advancedInput.Controls);
				
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box b = Box.createHorizontalBox();
				xIndexInput = new FormattedExpressionConfigurer(null, "Additional offset to the right:  ", t.xIndex.Format, t);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(xIndexInput.Controls);
				xOffsetInput = new FormattedExpressionConfigurer(null, " times ", t.xOffset.Format, t);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(xOffsetInput.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(b);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				b = Box.createHorizontalBox();
				yIndexInput = new FormattedExpressionConfigurer(null, "Additional offset upwards:  ", t.yIndex.Format, t);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(yIndexInput.Controls);
				yOffsetInput = new FormattedExpressionConfigurer(null, " times ", t.yOffset.Format, t);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(yOffsetInput.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(b);
				
				updateAdvancedVisibility();
			}
			
			private void  updateAdvancedVisibility()
			{
				bool visible = advancedInput.booleanValue();
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(xIndexInput.Controls, "Visible", visible);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(xOffsetInput.Controls, "Visible", visible);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(yIndexInput.Controls, "Visible", visible);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(yOffsetInput.Controls, "Visible", visible);
				//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
				System.Windows.Forms.Form w = (System.Windows.Forms.Form) controls.Parent;
				if (w != null)
				{
					//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
					w.pack();
				}
			}
		}
		
		/// <summary> Batches up all the movement commands resulting from a single KeyEvent
		/// and executes them at once. Ensures that pieces that are moving won't
		/// be merged with other moving pieces until they've been moved.
		/// </summary>
		public class MoveExecuter : IThreadRunnable
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDeckVisitor' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassDeckVisitor : DeckVisitor
			{
				public AnonymousClassDeckVisitor(Map.Merger merger, MoveExecuter enclosingInstance)
				{
					InitBlock(merger, enclosingInstance);
				}
				private void  InitBlock(Map.Merger merger, MoveExecuter enclosingInstance)
				{
					this.merger = merger;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable merger was copied into class AnonymousClassDeckVisitor. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private Map.Merger merger;
				private MoveExecuter enclosingInstance;
				public MoveExecuter Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual System.Object visitDeck(Deck d)
				{
					return merger.visitDeck(d);
				}
				
				public virtual System.Object visitStack(Stack s)
				{
					if (!pieces.contains(s) && move.map.getPieceCollection().canMerge(s, move.piece))
					{
						return merger.visitStack(s);
					}
					else
					{
						return null;
					}
				}
				
				public virtual System.Object visitDefault(GamePiece p)
				{
					if (!pieces.contains(p) && move.map.getPieceCollection().canMerge(p, move.piece))
					{
						return merger.visitDefault(p);
					}
					else
					{
						return null;
					}
				}
			}
			virtual public System.Windows.Forms.KeyEventArgs KeyEvent
			{
				set
				{
					this.stroke = value;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private List < Move > moves = new ArrayList < Move >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private Set < GamePiece > pieces = new HashSet < GamePiece >();
			private System.Windows.Forms.KeyEventArgs stroke;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private List < GamePiece > innerPieces = new ArrayList < GamePiece >();
			
			public virtual void  Run()
			{
				VassalSharp.counters.Translate.mover = null;
				Command comm = new NullCommand();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(final Move move: moves)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'merger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Map.Merger merger = new Map.Merger(move.map, move.pos, move.piece);
					DeckVisitor v = new AnonymousClassDeckVisitor(merger, this);
					
					DeckVisitorDispatcher dispatch = new DeckVisitorDispatcher(v);
					Command c = move.map.apply(dispatch);
					if (c == null)
					{
						c = move.map.placeAt(move.piece, move.pos);
						// Apply Auto-move key
						if (move.map.getMoveKey() != null)
						{
							c.append(Decorator.getOutermost(move.piece).keyEvent(move.map.getMoveKey()));
						}
					}
					comm.append(c);
					if (move.piece.getMap() == move.map)
					{
						move.map.ensureVisible(move.map.selectionBoundsOf(move.piece));
					}
					pieces.remove(move.piece);
					move.map.repaint();
				}
				MovementReporter r = new MovementReporter(comm);
				if (GlobalOptions.Instance.autoReportEnabled())
				{
					Command reportCommand = r.ReportCommand;
					if (reportCommand != null)
					{
						reportCommand.execute();
					}
					comm.append(reportCommand);
				}
				comm.append(r.markMovedPieces());
				if (stroke != null)
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(GamePiece gamePiece: innerPieces)
					{
						comm.append(gamePiece.keyEvent(stroke));
					}
				}
				GameModule.getGameModule().sendAndLog(comm);
			}
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			public virtual void  add(Map map, GamePiece piece, ref System.Drawing.Point pos)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				moves.add(new Move(map, piece, ref pos));
				pieces.add(piece);
			}
			
			public virtual void  addKeyEventTarget(GamePiece piece)
			{
				innerPieces.add(piece);
			}
			
			/// <summary> Return the updated position of a piece that has a move
			/// calculation recorded
			/// 
			/// </summary>
			/// <param name="target">piece to check
			/// </param>
			/// <returns> updated position
			/// </returns>
			public virtual System.Drawing.Point getUpdatedPosition(GamePiece target)
			{
				System.Drawing.Point p = System.Drawing.Point.Empty;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Move move: moves)
				{
					if (move.piece == target)
					{
						p = move.pos;
					}
				}
				return p;
			}
			
			private class Move
			{
				private Map map;
				private GamePiece piece;
				private System.Drawing.Point pos;
				
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				public Move(Map map, GamePiece piece, ref System.Drawing.Point pos)
				{
					this.map = map;
					this.piece = piece;
					this.pos = pos;
				}
			}
		}
	}
}