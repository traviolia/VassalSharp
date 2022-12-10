/*
* $Id$
*
* Copyright (c) 2005 by Scott Giese, Rodney Kinney, Brent Easton
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
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using MapShader = VassalSharp.build.module.map.MapShader;
using Board = VassalSharp.build.module.map.boardPicker.Board;
using GeometricGrid = VassalSharp.build.module.map.boardPicker.board.GeometricGrid;
using MapGrid = VassalSharp.build.module.map.boardPicker.board.MapGrid;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using ChooseComponentDialog = VassalSharp.configure.ChooseComponentDialog;
using ColorConfigurer = VassalSharp.configure.ColorConfigurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using Resources = VassalSharp.i18n.Resources;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <author>  Scott Giese sgiese@sprintmail.com
	/// 
	/// Displays a transparency surrounding the GamePiece which represents the Area of Effect of the GamePiece
	/// </author>
	public class AreaOfEffect:Decorator, TranslatablePiece, MapShader.ShadedPiece
	{
		virtual public System.String Description
		{
			get
			{
				System.String d = "Area Of Effect";
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
				return piece.Shape;
			}
			
		}
		virtual protected internal int Radius
		{
			get
			{
				if (fixedRadius)
				{
					return radius;
				}
				else
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String r = (System.String) Decorator.getOutermost(this).getProperty(radiusMarker);
					try
					{
						return System.Int32.Parse(r);
					}
					catch (System.FormatException e)
					{
						reportDataError(this, Resources.getString("Error.non_number_error"), "radius[" + radiusMarker + "]=" + r, e);
						return 0;
					}
				}
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("AreaOfEffect.htm");
			}
			
		}
		public const System.String ID = "AreaOfEffect;";
		//UPGRADE_NOTE: Final was removed from the declaration of 'defaultTransparencyColor '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal static readonly System.Drawing.Color defaultTransparencyColor = Color.GRAY;
		protected internal const float defaultTransparencyLevel = 0.3F;
		protected internal const int defaultRadius = 1;
		
		protected internal System.Drawing.Color transparencyColor;
		protected internal float transparencyLevel;
		protected internal int radius;
		protected internal bool alwaysActive;
		protected internal bool active;
		protected internal System.String activateCommand;
		protected internal NamedKeyStroke activateKey;
		protected internal KeyCommand[] commands;
		protected internal System.String mapShaderName;
		protected internal MapShader shader;
		protected internal KeyCommand keyCommand;
		protected internal bool fixedRadius = true;
		protected internal System.String radiusMarker = "";
		protected internal System.String description = "";
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public AreaOfEffect():this(ID + ColorConfigurer.colorToString(ref defaultTransparencyColor), null)
		{
		}
		
		public AreaOfEffect(System.String type, GamePiece inner)
		{
			mySetType(type);
			setInner(inner);
		}
		
		public override System.String myGetType()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(';');
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			se.append(ref transparencyColor);
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			se.append((int) (transparencyLevel * 100));
			se.append(radius);
			se.append(alwaysActive);
			se.append(activateCommand);
			se.append(activateKey);
			se.append(mapShaderName == null?"":mapShaderName);
			se.append(fixedRadius);
			se.append(radiusMarker);
			se.append(description);
			
			return ID + se.Value;
		}
		
		public virtual void  mySetType(System.String type)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken(); // Discard ID
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			transparencyColor = st.nextColor(ref defaultTransparencyColor);
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			transparencyLevel = st.nextInt((int) (defaultTransparencyLevel * 100)) / 100.0F;
			radius = st.nextInt(defaultRadius);
			alwaysActive = st.nextBoolean(true);
			activateCommand = st.nextToken("Show Area");
			activateKey = st.nextNamedKeyStroke(null);
			keyCommand = new KeyCommand(activateCommand, activateKey, Decorator.getOutermost(this), this);
			mapShaderName = st.nextToken("");
			if (mapShaderName.Length == 0)
			{
				mapShaderName = null;
			}
			fixedRadius = st.nextBoolean(true);
			radiusMarker = st.nextToken("");
			description = st.nextToken("");
			shader = null;
			commands = null;
		}
		
		// State does not change during the game
		public override System.String myGetState()
		{
			return alwaysActive?"":System.Convert.ToString(active);
		}
		
		// State does not change during the game
		public override void  mySetState(System.String newState)
		{
			if (!alwaysActive)
			{
				active = "true".Equals(newState);
			}
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			// TODO: Need the context of the parent Component, because the transparency is only drawn
			// on a Map.View object.  Because this context is not known, the bounding box returned by
			// this method does not encompass the bounds of the transparency.  The result of this is
			// that portions of the transparency will not be drawn after scrolling the Map window.
			return piece.boundingBox();
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			if ((alwaysActive || active) && mapShaderName == null)
			{
				// The transparency is only drawn on a Map.View component. Only the
				// GamePiece is drawn within other windows (Counter Palette, etc.).
				if (obs is Map.View && getMap() != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'g2d '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Graphics g2d = (System.Drawing.Graphics) g;
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'oldColor '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Color oldColor = SupportClass.GraphicsManager.manager.GetColor(g2d);
					SupportClass.GraphicsManager.manager.SetColor(g2d, transparencyColor);
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'oldComposite '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_ISSUE: Interface 'java.awt.Composite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComposite'"
					//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.getComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DgetComposite'"
					Composite oldComposite = g2d.getComposite();
					//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetComposite_javaawtComposite'"
					//UPGRADE_ISSUE: Method 'java.awt.AlphaComposite.getInstance' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
					//UPGRADE_ISSUE: Field 'java.awt.AlphaComposite.SRC_OVER' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
					g2d.setComposite(AlphaComposite.getInstance(AlphaComposite.SRC_OVER, transparencyLevel));
					
					System.Drawing.Region a = getArea();
					
					if (zoom != 1.0)
					{
						//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.createTransformedShape' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformcreateTransformedShape_javaawtShape'"
						System.Drawing.Drawing2D.Matrix temp_Matrix;
						temp_Matrix = new System.Drawing.Drawing2D.Matrix();
						temp_Matrix.Scale((float) zoom, (float) zoom);
						a = new System.Drawing.Region(temp_Matrix.createTransformedShape(a));
					}
					
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.fill' was converted to 'System.Drawing.Graphics.FillPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Dfill_javaawtShape'"
					g2d.FillPath(a);
					
					SupportClass.GraphicsManager.manager.SetColor(g2d, oldColor);
					//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetComposite_javaawtComposite'"
					g2d.setComposite(oldComposite);
				}
			}
			
			// Draw the GamePiece
			piece.draw(g, x, y, obs, zoom);
		}
		
		protected internal virtual System.Drawing.Region getArea()
		{
			System.Drawing.Region a;
			//UPGRADE_NOTE: Final was removed from the declaration of 'map '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Map map = getMap();
			// Always draw the area centered on the piece's current position
			// (For instance, don't draw it at an offset if it's in an expanded stack)
			//UPGRADE_NOTE: Final was removed from the declaration of 'mapPosition '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Point mapPosition = Position;
			//UPGRADE_NOTE: Final was removed from the declaration of 'myRadius '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int myRadius = Radius;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'board '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			Board board = map.findBoard(ref mapPosition);
			//UPGRADE_NOTE: Final was removed from the declaration of 'grid '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MapGrid grid = board == null?null:board.getGrid();
			
			if (grid is GeometricGrid)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'gGrid '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GeometricGrid gGrid = (GeometricGrid) grid;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'boardBounds '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle boardBounds = board.bounds();
				//UPGRADE_NOTE: Final was removed from the declaration of 'boardPosition '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point boardPosition = new System.Drawing.Point(mapPosition.X - boardBounds.X, mapPosition.Y - boardBounds.Y);
				
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				a = gGrid.getGridShape(ref boardPosition, myRadius); // In board co-ords
				//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Drawing2D.Matrix temp_Matrix;
				temp_Matrix = new System.Drawing.Drawing2D.Matrix();
				temp_Matrix.Translate((float) boardBounds.X, (float) boardBounds.Y);
				System.Drawing.Drawing2D.Matrix t = temp_Matrix; // Translate back to map co-ords
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'mag '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				double mag = board.getMagnification();
				if (mag != 1.0)
				{
					t.Translate((System.Single) boardPosition.X, (System.Single) boardPosition.Y);
					t.Scale((System.Single) mag, (System.Single) mag);
					t.Translate((System.Single) (- boardPosition.X), (System.Single) (- boardPosition.Y));
				}
				System.Drawing.Region temp_Region;
				temp_Region = a.Clone();
				temp_Region.Transform(t);
				a = temp_Region;
			}
			else
			{
				a = new System.Drawing.Region(SupportClass.Ellipse2DSupport.CreateEllipsePath((float) (mapPosition.X - myRadius), (float) (mapPosition.Y - myRadius), (float) (myRadius * 2), (float) (myRadius * 2)));
			}
			return a;
		}
		
		// No hot-keys
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			if (commands == null)
			{
				if (alwaysActive || activateCommand.Length == 0)
				{
					commands = new KeyCommand[0];
				}
				else
				{
					commands = new KeyCommand[]{keyCommand};
				}
			}
			return commands;
		}
		
		// No hot-keys
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			Command c = null;
			myGetKeyCommands();
			if (!alwaysActive && keyCommand.matches(stroke))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ChangeTracker t = new ChangeTracker(this);
				active = !active;
				c = t.ChangeCommand;
			}
			return c;
		}
		
		public override PieceEditor getEditor()
		{
			return new TraitEditor(this);
		}
		
		public virtual System.Drawing.Region getArea(MapShader shader)
		{
			System.Drawing.Region a = null;
			//UPGRADE_NOTE: Final was removed from the declaration of 'shaded '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MapShader.ShadedPiece shaded = (MapShader.ShadedPiece) Decorator.getDecorator(piece, typeof(MapShader.ShadedPiece));
			if (shaded != null)
			{
				a = shaded.getArea(shader);
			}
			if (alwaysActive || active)
			{
				if (shader.getConfigureName().Equals(mapShaderName))
				{
					System.Drawing.Region myArea = getArea();
					if (a == null)
					{
						a = myArea;
					}
					else
					{
						//UPGRADE_TODO: Method 'java.awt.geom.Area.add' was converted to 'System.Drawing.Region.Union' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomAreaadd_javaawtgeomArea'"
						a.Union(myArea);
					}
				}
			}
			return a;
		}
		
		protected internal class TraitEditor : PieceEditor
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener
			{
				public AnonymousClassActionListener(System.Windows.Forms.TextBox tf, TraitEditor enclosingInstance)
				{
					InitBlock(tf, enclosingInstance);
				}
				private void  InitBlock(System.Windows.Forms.TextBox tf, TraitEditor enclosingInstance)
				{
					this.tf = tf;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable tf was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Windows.Forms.TextBox tf;
				private TraitEditor enclosingInstance;
				public TraitEditor Enclosing_Instance
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
					ChooseComponentDialog d = new ChooseComponentDialog((System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), Enclosing_Instance.panel), typeof(MapShader));
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					d.Visible = true;
					if (d.Target != null)
					{
						Enclosing_Instance.mapShaderId = d.Target.getConfigureName();
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						tf.Text = Enclosing_Instance.mapShaderId;
					}
					else
					{
						Enclosing_Instance.mapShaderId = null;
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						tf.Text = "";
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener
			{
				public AnonymousClassPropertyChangeListener(TraitEditor enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(TraitEditor enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private TraitEditor enclosingInstance;
				public TraitEditor Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
				{
					Enclosing_Instance.updateRangeVisibility();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener1
			{
				public AnonymousClassPropertyChangeListener1(TraitEditor enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(TraitEditor enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private TraitEditor enclosingInstance;
				public TraitEditor Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
				{
					Enclosing_Instance.updateCommandVisibility();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener2
			{
				public AnonymousClassPropertyChangeListener2(TraitEditor enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(TraitEditor enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private TraitEditor enclosingInstance;
				public TraitEditor Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
				{
					Enclosing_Instance.updateFillVisibility();
				}
			}
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return panel;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return "false";
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'alwaysActiveSelected '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					bool alwaysActiveSelected = true.Equals(alwaysActive.getValue());
					//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(transparencyColorValue.ValueString);
					se.append(transparencyValue.ValueString);
					se.append(radiusValue.ValueString);
					se.append(alwaysActiveSelected);
					se.append(activateCommand.ValueString);
					se.append(activateKey.ValueString);
					if (true.Equals(useMapShader.getValue()) && mapShaderId != null)
					{
						se.append(mapShaderId);
					}
					else
					{
						se.append("");
					}
					se.append(fixedRadius.ValueString);
					se.append(radiusMarker.ValueString);
					se.append(descConfig.ValueString);
					
					return AreaOfEffect.ID + se.Value;
				}
				
			}
			protected internal System.Windows.Forms.Panel panel;
			protected internal ColorConfigurer transparencyColorValue;
			protected internal IntConfigurer transparencyValue;
			protected internal IntConfigurer radiusValue;
			protected internal BooleanConfigurer alwaysActive;
			protected internal StringConfigurer activateCommand;
			protected internal NamedHotKeyConfigurer activateKey;
			protected internal BooleanConfigurer useMapShader;
			protected internal BooleanConfigurer fixedRadius;
			protected internal StringConfigurer radiusMarker;
			protected internal StringConfigurer descConfig;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			protected internal Box selectShader;
			protected internal System.String mapShaderId;
			
			protected internal TraitEditor(AreaOfEffect trait)
			{
				panel = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				panel.setLayout(new BoxLayout(panel, BoxLayout.Y_AXIS));
				
				System.Windows.Forms.Label temp_label2;
				//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.SwingConstants.CENTER' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Contributed by Scott Giese (sgiese@sprintmail.com)";
				temp_label2.ImageAlign = (System.Drawing.ContentAlignment) System.Windows.Forms.HorizontalAlignment.Center;
				temp_label2.TextAlign = (System.Drawing.ContentAlignment) System.Windows.Forms.HorizontalAlignment.Center;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				panel.Controls.Add(temp_Control);
				//UPGRADE_ISSUE: Constructor 'javax.swing.JSeparator.JSeparator' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJSeparator'"
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = new JSeparator();
				panel.Controls.Add(temp_Control2);
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = " ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control3;
				temp_Control3 = temp_label4;
				panel.Controls.Add(temp_Control3);
				
				descConfig = new StringConfigurer(null, "Description:  ", trait.description);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(descConfig.Controls);
				
				useMapShader = new BooleanConfigurer(null, "Use Map Shading?", trait.mapShaderName != null);
				mapShaderId = trait.mapShaderName;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(useMapShader.Controls);
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				selectShader = Box.createHorizontalBox();
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(selectShader);
				//UPGRADE_NOTE: Final was removed from the declaration of 'l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Label temp_label5;
				temp_label5 = new System.Windows.Forms.Label();
				temp_label5.Text = "Map Shading:  ";
				System.Windows.Forms.Label l = temp_label5;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				selectShader.Controls.Add(l);
				//UPGRADE_NOTE: Final was removed from the declaration of 'tf '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
				System.Windows.Forms.TextBox tf = new System.Windows.Forms.TextBox();
				tf.ReadOnly = !false;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				selectShader.Controls.Add(tf);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				tf.Text = trait.mapShaderName;
				//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button b = SupportClass.ButtonSupport.CreateStandardButton("Select");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				selectShader.Controls.Add(b);
				b.Click += new System.EventHandler(new AnonymousClassActionListener(tf, this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(b);
				
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				transparencyColorValue = new ColorConfigurer(null, "Fill Color:  ", ref trait.transparencyColor);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(transparencyColorValue.Controls);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				transparencyValue = new IntConfigurer(null, "Opacity (%):  ", (int) (trait.transparencyLevel * 100));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(transparencyValue.Controls);
				
				fixedRadius = new BooleanConfigurer(null, "Fixed Radius?", Boolean.valueOf(trait.fixedRadius));
				fixedRadius.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(fixedRadius.Controls);
				
				radiusValue = new IntConfigurer(null, "Radius: ", trait.radius);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(radiusValue.Controls);
				
				radiusMarker = new StringConfigurer(null, "Radius Marker: ", trait.radiusMarker);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(radiusMarker.Controls);
				
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				alwaysActive = new BooleanConfigurer(null, "Always visible?", ref trait.alwaysActive?true:false);
				activateCommand = new StringConfigurer(null, "Toggle visible command:  ", trait.activateCommand);
				activateKey = new NamedHotKeyConfigurer(null, "Toggle visible keyboard shortcut:  ", trait.activateKey);
				
				updateRangeVisibility();
				
				alwaysActive.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener1(this).propertyChange);
				updateCommandVisibility();
				
				useMapShader.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener2(this).propertyChange);
				updateFillVisibility();
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(alwaysActive.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(activateCommand.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(activateKey.Controls);
			}
			
			protected internal virtual void  updateFillVisibility()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'useShader '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool useShader = true.Equals(useMapShader.getValue());
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(transparencyColorValue.Controls, "Visible", !useShader);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(transparencyValue.Controls, "Visible", !useShader);
				selectShader.Visible = useShader;
				repack();
			}
			
			protected internal virtual void  updateRangeVisibility()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fixedRange '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool fixedRange = fixedRadius.booleanValue();
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(radiusValue.Controls, "Visible", fixedRange);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(radiusMarker.Controls, "Visible", !fixedRange);
				repack();
			}
			
			protected internal virtual void  updateCommandVisibility()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'alwaysActiveSelected '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool alwaysActiveSelected = true.Equals(alwaysActive.getValue());
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(activateCommand.Controls, "Visible", !alwaysActiveSelected);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(activateKey.Controls, "Visible", !alwaysActiveSelected);
				repack();
			}
			
			protected internal virtual void  repack()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
				System.Windows.Forms.Form w = (System.Windows.Forms.Form) alwaysActive.Controls.Parent;
				if (w != null)
				{
					//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
					w.pack();
				}
			}
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(activateCommand, getCommandDescription(description, "Toggle Visible command"));
		}
	}
}