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
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
using GpIdSupport = VassalSharp.build.GpIdSupport;
using BasicCommandEncoder = VassalSharp.build.module.BasicCommandEncoder;
using Chatter = VassalSharp.build.module.Chatter;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using CardSlot = VassalSharp.build.widget.CardSlot;
using PieceSlot = VassalSharp.build.widget.PieceSlot;
using AddPiece = VassalSharp.command.AddPiece;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using ChooseComponentPathDialog = VassalSharp.configure.ChooseComponentPathDialog;
using ConfigurerWindow = VassalSharp.configure.ConfigurerWindow;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using Resources = VassalSharp.i18n.Resources;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using ComponentPathBuilder = VassalSharp.tools.ComponentPathBuilder;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> This Decorator defines a key command to places another counter on top of this one.</summary>
	public class PlaceMarker:Decorator, TranslatablePiece
	{
		/// <returns> true if the marker is defined from scratch. Return false if the marker is defined as a component in the
		/// Game Piece Palette
		/// </returns>
		virtual public bool MarkerStandalone
		{
			get
			{
				return markerSpec != null && markerSpec.StartsWith(BasicCommandEncoder.ADD);
			}
			
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				return piece.Shape;
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				System.String d = "Place Marker";
				if (description.Length > 0)
				{
					d += (" - " + description);
				}
				return d;
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("Marker.htm");
			}
			
		}
		virtual public System.String GpId
		{
			get
			{
				return gpId;
			}
			
			set
			{
				gpId = value;
			}
			
		}
		public const System.String ID = "placemark;";
		protected internal KeyCommand command;
		protected internal NamedKeyStroke key;
		protected internal System.String markerSpec;
		protected internal System.String markerText = "";
		protected internal int xOffset = 0;
		protected internal int yOffset = 0;
		protected internal bool matchRotation = false;
		protected internal KeyCommand[] commands;
		protected internal NamedKeyStroke afterBurnerKey;
		protected internal System.String description = "";
		protected internal System.String gpId = "";
		protected internal System.String newGpId;
		protected internal GpIdSupport gpidSupport; // The component that generates unique Slot Id's for us
		protected internal const int STACK_TOP = 0;
		protected internal const int STACK_BOTTOM = 1;
		protected internal const int ABOVE = 2;
		protected internal const int BELOW = 3;
		protected internal int placement = STACK_TOP;
		protected internal bool above;
		
		public PlaceMarker():this(ID + "Place Marker;M;null;null;null", null)
		{
		}
		
		public PlaceMarker(System.String type, GamePiece inner)
		{
			mySetType(type);
			setInner(inner);
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			return piece.boundingBox();
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			piece.draw(g, x, y, obs, zoom);
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			command.setEnabled(getMap() != null && markerSpec != null);
			return commands;
		}
		
		public override System.String myGetState()
		{
			return "";
		}
		
		public override System.String myGetType()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(command.Name).append(key).append(markerSpec == null?"null":markerSpec).append(markerText == null?"null":markerText).append(xOffset).append(yOffset).append(matchRotation).append(afterBurnerKey).append(description).append(gpId).append(placement).append(above);
			return ID + se.Value;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			myGetKeyCommands();
			if (command.matches(stroke))
			{
				return placeMarker();
			}
			else
			{
				return null;
			}
		}
		
		protected internal virtual Command placeMarker()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Map m = getMap();
			if (m == null)
				return null;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'marker '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GamePiece marker = createMarker();
			if (marker == null)
				return null;
			
			Command c = null;
			//UPGRADE_NOTE: Final was removed from the declaration of 'outer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GamePiece outer = getOutermost(this);
			System.Drawing.Point p = Position;
			p.Offset(xOffset, - yOffset);
			if (matchRotation)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'myRotation '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				FreeRotator myRotation = (FreeRotator) Decorator.getDecorator(outer, typeof(FreeRotator));
				//UPGRADE_NOTE: Final was removed from the declaration of 'markerRotation '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				FreeRotator markerRotation = (FreeRotator) Decorator.getDecorator(marker, typeof(FreeRotator));
				if (myRotation != null && markerRotation != null)
				{
					markerRotation.Angle = myRotation.Angle;
					//UPGRADE_NOTE: Final was removed from the declaration of 'myPosition '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.PointF myPosition = new System.Drawing.Point(new System.Drawing.Size(Position));
					System.Drawing.PointF markerPosition = new System.Drawing.Point(new System.Drawing.Size(p));
					//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.transform' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformtransform_javaawtgeomPoint2D_javaawtgeomPoint2D'"
					System.Drawing.Drawing2D.Matrix temp_Matrix;
					temp_Matrix = new System.Drawing.Drawing2D.Matrix();
					temp_Matrix.RotateAt((float) (myRotation.AngleInRadians * (180 / System.Math.PI)), new System.Drawing.PointF((float) myPosition.X, (float) myPosition.Y));
					System.Drawing.PointF tempAux = System.Drawing.PointF.Empty;
					markerPosition = temp_Matrix.transform(markerPosition, tempAux);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					p = new System.Drawing.Point((int) markerPosition.X, (int) markerPosition.Y);
				}
			}
			
			if (!true.Equals(marker.getProperty(VassalSharp.counters.Properties_Fields.IGNORE_GRID)))
			{
				p = getMap().snapTo(p);
			}
			
			if (m.StackMetrics.StackingEnabled && !true.Equals(marker.getProperty(VassalSharp.counters.Properties_Fields.NO_STACK)) && !true.Equals(outer.getProperty(VassalSharp.counters.Properties_Fields.NO_STACK)) && m.getPieceCollection().canMerge(outer, marker))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'parent '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Stack parent = Parent;
				GamePiece target = outer;
				int index = - 1;
				switch (placement)
				{
					
					case ABOVE: 
						target = outer;
						break;
					
					case BELOW: 
						index = parent == null?0:parent.indexOf(outer);
						break;
					
					case STACK_BOTTOM: 
						index = 0;
						break;
					
					case STACK_TOP: 
						target = parent == null?outer:parent;
						break;
					}
				c = m.StackMetrics.merge(target, marker);
				if (index >= 0)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'ct '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					ChangeTracker ct = new ChangeTracker(parent);
					parent.insert(marker, index);
					c = c.append(ct.ChangeCommand);
				}
			}
			else
			{
				c = m.placeAt(marker, p);
			}
			
			if (afterBurnerKey != null && !afterBurnerKey.Null)
			{
				marker.setProperty(VassalSharp.counters.Properties_Fields.SNAPSHOT, PieceCloner.Instance.clonePiece(marker));
				c.append(marker.keyEvent(afterBurnerKey.KeyStroke));
			}
			
			if (getProperty(VassalSharp.counters.Properties_Fields.SELECTED) == (System.Object) true)
				selectMarker(marker);
			
			if (markerText != null)
			{
				if (!true.Equals(outer.getProperty(VassalSharp.counters.Properties_Fields.OBSCURED_TO_OTHERS)) && !true.Equals(outer.getProperty(VassalSharp.counters.Properties_Fields.OBSCURED_TO_ME)) && !true.Equals(outer.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_OTHERS)))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'location '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String location = m.locationName(Position);
					if (location != null)
					{
						Command display = new Chatter.DisplayText(GameModule.getGameModule().getChatter(), " * " + location + ":  " + outer.getName() + " " + markerText + " * ");
						display.execute();
						c = c == null?display:c.append(display);
					}
				}
			}
			
			return c;
		}
		
		protected internal virtual void  selectMarker(GamePiece marker)
		{
			if (marker.getProperty(VassalSharp.counters.Properties_Fields.SELECT_EVENT_FILTER) == null)
			{
				if (marker.Parent != null && marker.Parent.Equals(Parent))
				{
					KeyBuffer.Buffer.add(marker);
				}
			}
		}
		
		/// <summary> The marker, with prototypes fully expanded
		/// 
		/// </summary>
		/// <returns>
		/// </returns>
		public virtual GamePiece createMarker()
		{
			GamePiece piece = createBaseMarker();
			if (piece == null)
			{
				piece = new BasicPiece();
				newGpId = GpId;
			}
			else
			{
				piece = PieceCloner.Instance.clonePiece(piece);
			}
			piece.setProperty(VassalSharp.counters.Properties_Fields.PIECE_ID, newGpId);
			return piece;
		}
		
		/// <summary> The marker, with prototypes unexpanded
		/// 
		/// </summary>
		/// <returns>
		/// </returns>
		public virtual GamePiece createBaseMarker()
		{
			if (markerSpec == null)
			{
				return null;
			}
			GamePiece piece = null;
			if (MarkerStandalone)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'comm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				AddPiece comm = (AddPiece) GameModule.getGameModule().decode(markerSpec);
				piece = comm.Target;
				piece.State = comm.State;
				newGpId = GpId;
			}
			else
			{
				try
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Configurable[] c = ComponentPathBuilder.Instance.getPath(markerSpec);
					//UPGRADE_NOTE: Final was removed from the declaration of 'conf '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Configurable conf = c[c.Length - 1];
					
					if (conf is PieceSlot)
					{
						piece = ((PieceSlot) conf).Piece;
						newGpId = ((PieceSlot) conf).GpId;
					}
				}
				catch (ComponentPathBuilder.PathFormatException e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					reportDataError(this, Resources.getString("Resources.place_error"), e.Message + " markerSpec=" + markerSpec, e);
				}
			}
			return piece;
		}
		
		public override void  mySetState(System.String newState)
		{
		}
		
		public virtual void  mySetType(System.String type)
		{
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken();
			System.String name = st.nextToken();
			key = st.nextNamedKeyStroke(null);
			command = new KeyCommand(name, key, this, this);
			if (name.Length > 0 && key != null)
			{
				commands = new KeyCommand[]{command};
			}
			else
			{
				commands = new KeyCommand[0];
			}
			markerSpec = st.nextToken();
			if ("null".Equals(markerSpec))
			{
				markerSpec = null;
			}
			markerText = st.nextToken("null");
			if ("null".Equals(markerText))
			{
				markerText = null;
			}
			xOffset = st.nextInt(0);
			yOffset = st.nextInt(0);
			matchRotation = st.nextBoolean(false);
			afterBurnerKey = st.nextNamedKeyStroke(null);
			description = st.nextToken("");
			GpId = st.nextToken("");
			placement = st.nextInt(STACK_TOP);
			above = st.nextBoolean(false);
			gpidSupport = GameModule.getGameModule().getGpIdSupport();
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(command.Name, getCommandDescription(description, "Place Marker command"));
		}
		
		public virtual void  updateGpId(GpIdSupport s)
		{
			gpidSupport = s;
			updateGpId();
		}
		
		public virtual void  updateGpId()
		{
			GpId = gpidSupport.generateGpId();
		}
		
		protected internal class Ed : PieceEditor
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener
			{
				public AnonymousClassActionListener(Ed enclosingInstance)
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
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.markerSlotPath = null;
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					new ConfigurerWindow(Enclosing_Instance.pieceInput.Configurer).Visible = true;
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener1
			{
				public AnonymousClassActionListener1(Ed enclosingInstance)
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
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.getAncestorOfClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
					//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
					ChoosePieceDialog d = new ChoosePieceDialog((System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), Enclosing_Instance.p), typeof(PieceSlot));
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					d.Visible = true;
					if (d.Target is PieceSlot)
					{
						Enclosing_Instance.pieceInput.Piece = ((PieceSlot) d.Target).Piece;
					}
					if (d.Path != null)
					{
						Enclosing_Instance.markerSlotPath = ComponentPathBuilder.Instance.getId(d.Path);
						Enclosing_Instance.slotId = "";
					}
					else
					{
						Enclosing_Instance.markerSlotPath = null;
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener2
			{
				public AnonymousClassActionListener2(Ed enclosingInstance)
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
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(Enclosing_Instance.aboveConfig.Controls, "Visible", ((System.Windows.Forms.CheckBox) Enclosing_Instance.matchRotationConfig.Controls).Checked);
				}
			}
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return p;
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
					se.append(commandInput.ValueString);
					se.append(keyInput.ValueString);
					if (pieceInput.Piece == null)
					{
						se.append("null");
					}
					else if (markerSlotPath != null)
					{
						se.append(markerSlotPath);
					}
					else
					{
						System.String spec = GameModule.getGameModule().encode(new AddPiece(pieceInput.Piece));
						se.append(spec);
					}
					se.append("null"); // Older versions specified a text message to echo. Now performed by the ReportState trait,
					// but we remain backward-compatible.
					se.append(xOffsetConfig.ValueString);
					se.append(yOffsetConfig.ValueString);
					se.append(matchRotationConfig.ValueString);
					se.append(afterBurner.ValueString);
					se.append(descConfig.ValueString);
					se.append(slotId);
					se.append(placementConfig.SelectedIndex);
					se.append(aboveConfig == null?"false":aboveConfig.ValueString);
					return VassalSharp.counters.PlaceMarker.ID + se.Value;
				}
				
			}
			private NamedHotKeyConfigurer keyInput;
			private StringConfigurer commandInput;
			private PieceSlot pieceInput;
			private System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
			private System.String markerSlotPath;
			protected internal System.Windows.Forms.Button defineButton = SupportClass.ButtonSupport.CreateStandardButton("Define Marker");
			protected internal System.Windows.Forms.Button selectButton = SupportClass.ButtonSupport.CreateStandardButton("Select");
			protected internal IntConfigurer xOffsetConfig = new IntConfigurer(null, "Horizontal offset:  ");
			protected internal IntConfigurer yOffsetConfig = new IntConfigurer(null, "Vertical offset:  ");
			protected internal BooleanConfigurer matchRotationConfig;
			protected internal BooleanConfigurer aboveConfig;
			protected internal System.Windows.Forms.ComboBox placementConfig;
			protected internal NamedHotKeyConfigurer afterBurner;
			protected internal StringConfigurer descConfig;
			private System.String slotId;
			
			protected internal Ed(PlaceMarker piece)
			{
				matchRotationConfig = createMatchRotationConfig();
				aboveConfig = createAboveConfig();
				descConfig = new StringConfigurer(null, "Description:  ", piece.description);
				keyInput = new NamedHotKeyConfigurer(null, "Keyboard Command:  ", piece.key);
				afterBurner = new NamedHotKeyConfigurer(null, "Keystroke to apply after placement:  ", piece.afterBurnerKey);
				commandInput = new StringConfigurer(null, "Command:  ", piece.command.Name);
				GamePiece marker = piece.createBaseMarker();
				pieceInput = new PieceSlot(marker);
				pieceInput.updateGpId(piece.gpidSupport);
				pieceInput.GpId = piece.GpId;
				markerSlotPath = piece.markerSpec;
				p = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				p.setLayout(new BoxLayout(p, BoxLayout.Y_AXIS));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(descConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(commandInput.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(keyInput.Controls);
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box b = Box.createHorizontalBox();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(pieceInput.Component);
				defineButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(defineButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(defineButton);
				selectButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(selectButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(selectButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(b);
				xOffsetConfig.setValue(piece.xOffset);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(xOffsetConfig.Controls);
				yOffsetConfig.setValue(piece.yOffset);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(yOffsetConfig.Controls);
				matchRotationConfig.setValue(Boolean.valueOf(piece.matchRotation));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(matchRotationConfig.Controls);
				if (aboveConfig != null)
				{
					aboveConfig.setValue(Boolean.valueOf(piece.above));
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(aboveConfig.Controls);
					//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
					((System.Windows.Forms.CheckBox) matchRotationConfig.Controls).Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(((System.Windows.Forms.CheckBox) matchRotationConfig.Controls));
					aboveConfig.Controls.setVisible(Boolean.valueOf(piece.matchRotation));
				}
				placementConfig = SupportClass.ComboBoxSupport.CreateComboBox(new System.String[]{"On top of stack", "On bottom of stack", "Above this piece", "Below this piece"});
				placementConfig.SelectedIndex = piece.placement;
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box placementBox = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Place marker:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				placementBox.Controls.Add(temp_Control);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				placementBox.Controls.Add(placementConfig);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(placementBox);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(afterBurner.Controls);
				slotId = piece.GpId;
			}
			
			protected internal virtual BooleanConfigurer createMatchRotationConfig()
			{
				return new BooleanConfigurer(null, "Match Rotation?");
			}
			
			protected internal virtual BooleanConfigurer createAboveConfig()
			{
				return null;
			}
			[Serializable]
			public class ChoosePieceDialog:ChooseComponentPathDialog
			{
				public ChoosePieceDialog()
				{
					InitBlock();
				}
				private void  InitBlock()
				{
					base(owner, targetClass);
				}
				private const long serialVersionUID = 1L;
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				public ChoosePieceDialog(Frame owner, Class < PieceSlot > targetClass)
				
				protected internal override bool isValidTarget(System.Object selected)
				{
					return base.isValidTarget(selected) || typeof(CardSlot).IsInstanceOfType(selected);
				}
			}
		}
	}
}