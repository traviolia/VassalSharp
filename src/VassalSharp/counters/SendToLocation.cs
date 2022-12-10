/*
* $Id$
*
* Copyright (c) 2003-2012 by Rodney Kinney, Brent Easton
* GridLocation modifications copyright (c) 2010-2011 by Pieter Geerkens
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
using Chatter = VassalSharp.build.module.Chatter;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Board = VassalSharp.build.module.map.boardPicker.Board;
using BadCoords = VassalSharp.build.module.map.boardPicker.board.MapGrid.BadCoords;
using Region = VassalSharp.build.module.map.boardPicker.board.Region;
using Zone = VassalSharp.build.module.map.boardPicker.board.mapgrid.Zone;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using ChooseComponentDialog = VassalSharp.configure.ChooseComponentDialog;
using FormattedExpressionConfigurer = VassalSharp.configure.FormattedExpressionConfigurer;
using FormattedStringConfigurer = VassalSharp.configure.FormattedStringConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using PropertyExpression = VassalSharp.configure.PropertyExpression;
using PropertyExpressionConfigurer = VassalSharp.configure.PropertyExpressionConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using StringEnumConfigurer = VassalSharp.configure.StringEnumConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using Resources = VassalSharp.i18n.Resources;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using FormattedString = VassalSharp.tools.FormattedString;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	/// <summary> This trait adds a command that sends a piece to another location. Options for the
	/// target location are:
	/// <li>Specified x,y co-ords on a named map/board</li>
	/// <li>The centre of a named Zone on a named map</li>
	/// <li>A named Region on a named map</li>
	/// <li>The location of another counter selected by a Property Match String</li>
	/// <li>A specified grid-location on a given board & map </li>
	/// <p>Once the target location is identified, it can be further offset in the X and Y directions
	/// by a set of multipliers.
	/// All Input Fields may use $...$ variable names
	/// </summary>
	public class SendToLocation:Decorator, TranslatablePiece
	{
		private System.Drawing.Point SendLocation
		{
			get
			{
				GamePiece outer = Decorator.getOutermost(this);
				map = null;
				System.Drawing.Point dest = System.Drawing.Point.Empty;
				// Home in on a counter
				if (destination.Equals(DEST_COUNTER.Substring(0, (1) - (0))))
				{
					GamePiece target = null;
					// Find first counter matching the properties
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(GamePiece piece: 
					GameModule.getGameModule().getGameState().getAllPieces())
					{
						if (piece is Stack)
						{
							Stack s = (Stack) piece;
							for (int i = 0; i < s.PieceCount; i++)
							{
								if (propertyFilter.accept(this, s.getPieceAt(i)))
								{
									target = s.getPieceAt(i);
									if (target != null)
										break;
								}
							}
						}
						else
						{
							if (propertyFilter.accept(this, piece))
							{
								target = piece;
							}
						}
						if (target != null)
							break;
					}
					// Determine target's position
					if (target != null)
					{
						map = target.getMap();
						if (map != null)
						{
							dest = target.Position;
						}
					}
				}
				// Location/Zone/Region processing all use specified map
				else
				{
					map = Map.getMapById(mapId.getText(outer));
					if (map == null)
					{
						map = getMap();
					}
					if (map != null)
					{
						Board b;
						switch (destination[0])
						{
							
							case 'G': 
								b = map.getBoardByName(boardName.getText(outer));
								if (b != null)
								{
									try
									{
										dest = b.getGrid().getLocation(gridLocation.getText(outer));
										if (!dest.IsEmpty)
											dest.translate(b.bounds().x, b.bounds().y);
									}
									catch (BadCoords e)
									{
										//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
										LogBadGridLocation(ref dest);
										reportDataError(this, Resources.getString("Error.not_found", "Grid Location"), map.getMapName());
										; // ignore SendTo request.
									}
								}
								break;
							
							case 'L': 
								//UPGRADE_NOTE: Final was removed from the declaration of 'xValue '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
								int xValue = x.getTextAsInt(outer, "Xlocation", this);
								//UPGRADE_NOTE: Final was removed from the declaration of 'yValue '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
								int yValue = y.getTextAsInt(outer, "YLocation", this);
								
								dest = new System.Drawing.Point(xValue, yValue);
								
								b = map.getBoardByName(boardName.getText(outer));
								if (b != null && !dest.IsEmpty)
								{
									dest.translate(b.bounds().x, b.bounds().y);
								}
								break;
							
							
							case 'Z': 
								//UPGRADE_NOTE: Final was removed from the declaration of 'zoneName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
								System.String zoneName = zone.getText(outer);
								Zone z = map.findZone(zoneName);
								if (z == null)
								{
									reportDataError(this, Resources.getString("Error.not_found", "Zone"), zone.debugInfo(zoneName, "Zone"));
								}
								else
								{
									System.Drawing.Rectangle r = z.getBounds();
									System.Drawing.Rectangle r2 = z.getBoard().bounds();
									dest = new System.Drawing.Point(r2.X + r.X + r.Width / 2, r2.Y + r.Y + r.Height / 2);
								}
								break;
							
							
							case 'R': 
								//UPGRADE_NOTE: Final was removed from the declaration of 'regionName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
								System.String regionName = region.getText(outer);
								Region r = map.findRegion(regionName);
								if (r == null)
								{
									reportDataError(this, Resources.getString("Error.not_found", "Region"), region.debugInfo(regionName, "Region"));
								}
								else
								{
									System.Drawing.Rectangle r2 = r.getBoard().bounds();
									if (r != null)
									{
										dest = new System.Drawing.Point(r.Origin.X + r2.X, r.Origin.Y + r2.Y);
									}
								}
								break;
							}
					}
				}
				
				// Offset destination by Advanced Options offsets
				if ((!dest.IsEmpty) && (destination[0] != 'G'))
				{
					dest = offsetDestination(dest.X, dest.Y, outer);
				}
				
				return dest;
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
				System.String d = "Send to Location";
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
				return HelpFile.getReferenceManualPage("SendToLocation.htm");
			}
			
		}
		public const System.String ID = "sendto;";
		private const System.String _0 = "0";
		public const System.String BACK_MAP = "backMap";
		public const System.String BACK_POINT = "backPoint";
		protected internal const System.String DEST_GRIDLOCATION = "Grid location on selected Map";
		protected internal const System.String DEST_LOCATION = "Location on selected Map";
		protected internal const System.String DEST_ZONE = "Zone on selected Map";
		protected internal const System.String DEST_REGION = "Region on selected Map";
		protected internal const System.String DEST_COUNTER = "Another counter, selected by properties";
		//UPGRADE_NOTE: Final was removed from the declaration of 'DEST_OPTIONS '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal static readonly System.String[] DEST_OPTIONS = new System.String[]{DEST_GRIDLOCATION, DEST_LOCATION, DEST_ZONE, DEST_REGION, DEST_COUNTER};
		protected internal KeyCommand[] command;
		protected internal System.String commandName;
		protected internal System.String backCommandName;
		protected internal NamedKeyStroke key;
		protected internal NamedKeyStroke backKey;
		protected internal FormattedString mapId = new FormattedString("");
		protected internal FormattedString boardName = new FormattedString("");
		protected internal FormattedString x = new FormattedString("");
		protected internal FormattedString xIndex = new FormattedString("");
		protected internal FormattedString xOffset = new FormattedString("");
		protected internal FormattedString y = new FormattedString("");
		protected internal FormattedString yIndex = new FormattedString("");
		protected internal FormattedString yOffset = new FormattedString("");
		protected internal FormattedString gridLocation = new FormattedString("");
		protected internal KeyCommand sendCommand;
		protected internal KeyCommand backCommand;
		protected internal System.String description;
		protected internal System.String destination;
		protected internal FormattedString zone = new FormattedString("");
		protected internal FormattedString region = new FormattedString("");
		protected internal PropertyExpression propertyFilter = new PropertyExpression("");
		private Map map;
		// private Point dest;
		
		public SendToLocation():this(ID + ";;;;0;0;;;", null)
		{
		}
		
		public SendToLocation(System.String type, GamePiece inner)
		{
			mySetType(type);
			setInner(inner);
		}
		
		public virtual void  mySetType(System.String type)
		{
			type = type.Substring(ID.Length);
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			commandName = st.nextToken("");
			key = st.nextNamedKeyStroke(null);
			mapId.Format = st.nextToken("");
			boardName.Format = st.nextToken("");
			x.Format = st.nextToken("0");
			y.Format = st.nextToken("0");
			backCommandName = st.nextToken("");
			backKey = st.nextNamedKeyStroke(null);
			xIndex.Format = st.nextToken("0");
			yIndex.Format = st.nextToken("0");
			xOffset.Format = st.nextToken("0");
			yOffset.Format = st.nextToken("0");
			description = st.nextToken("");
			destination = st.nextToken(DEST_LOCATION.Substring(0, (1) - (0)));
			if (destination.Length == 0)
			{
				destination = DEST_LOCATION.Substring(0, (1) - (0));
			}
			zone.Format = st.nextToken("");
			region.Format = st.nextToken("");
			propertyFilter.Expression = st.nextToken("");
			gridLocation.Format = st.nextToken("");
		}
		
		public override System.String myGetType()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(commandName).append(key).append(mapId.Format).append(boardName.Format).append(x.Format).append(y.Format).append(backCommandName).append(backKey).append(xIndex.Format).append(yIndex.Format).append(xOffset.Format).append(yOffset.Format).append(description).append(destination).append(zone.Format).append(region.Format).append(propertyFilter.Expression).append(gridLocation.Format);
			return ID + se.Value;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			if (command == null)
			{
				sendCommand = new KeyCommand(commandName, key, Decorator.getOutermost(this), this);
				backCommand = new KeyCommand(backCommandName, backKey, Decorator.getOutermost(this), this);
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ArrayList < KeyCommand > l = new ArrayList < KeyCommand >();
				if (commandName.Length > 0 && key != null && !key.Null)
				{
					l.add(sendCommand);
				}
				if (backCommandName.Length > 0 && backKey != null && !backKey.Null)
				{
					l.add(backCommand);
				}
				command = l.toArray(new KeyCommand[l.size()]);
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(KeyCommand c: command)
			{
				if (c.getName().equals(backCommandName))
				{
					c.setEnabled(getMap() != null && getProperty(BACK_MAP) != null && getProperty(BACK_POINT) != null);
				}
				else
				{
					System.Drawing.Point p = SendLocation;
					c.setEnabled(getMap() != null && !p.IsEmpty && (map != getMap() || !p.Equals(Position)));
				}
			}
			return command;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		private void  LogBadGridLocation(ref System.Drawing.Point p)
		{
			System.String s = "* " + Decorator.getOutermost(this).getName();
			if (getMap() == null)
			{
				s += "getMap is null";
			}
			else if (p.IsEmpty)
			{
				s += "p is null";
			}
			else
			{
				s += ("getMap: " + getMap().getMapName() + "; p: (" + p.X + "," + p.Y + "; Position: (" + Position.X + "," + Position.Y + "); map: " + map.getMapName() + ";");
			}
			new Chatter.DisplayText(GameModule.getGameModule().getChatter(), s).execute();
		}
		
		public override System.String myGetState()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			Map backMap = (Map) getProperty(BACK_MAP);
			if (backMap != null)
			{
				se.append(backMap.getIdentifier());
			}
			else
			{
				se.append("");
			}
			System.Drawing.Point backPoint = (System.Drawing.Point) getProperty(BACK_POINT);
			if (!backPoint.IsEmpty)
			{
				se.append(backPoint.X).append(backPoint.Y);
			}
			else
			{
				se.append("").append("");
			}
			return se.Value;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			Command c = null;
			myGetKeyCommands();
			if (sendCommand.matches(stroke))
			{
				GamePiece outer = Decorator.getOutermost(this);
				Stack parent = outer.Parent;
				System.Drawing.Point dest = SendLocation;
				if (map != null && !dest.IsEmpty)
				{
					if (map == getMap() && dest.Equals(Position))
					{
						// don't do anything if we're already there.
						return null;
					}
					setProperty(BACK_MAP, getMap());
					setProperty(BACK_POINT, Position);
					setOldProperties();
					if (!true.Equals(outer.getProperty(VassalSharp.counters.Properties_Fields.IGNORE_GRID)))
					{
						dest = map.snapTo(dest);
					}
					c = map.placeOrMerge(outer, dest);
					// Apply Auto-move key
					if (map.getMoveKey() != null)
					{
						c.append(outer.keyEvent(map.getMoveKey()));
					}
					if (parent != null)
					{
						c.append(parent.pieceRemoved(outer));
					}
				}
			}
			else if (backCommand.matches(stroke))
			{
				GamePiece outer = Decorator.getOutermost(this);
				Map backMap = (Map) getProperty(BACK_MAP);
				System.Drawing.Point backPoint = (System.Drawing.Point) getProperty(BACK_POINT);
				if (backMap != null && !backPoint.IsEmpty)
				{
					setOldProperties();
					c = backMap.placeOrMerge(outer, backPoint);
					//UPGRADE_NOTE: Final was removed from the declaration of 'tracker '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					ChangeTracker tracker = new ChangeTracker(this);
					setProperty(BACK_MAP, (System.Object) null);
					setProperty(BACK_POINT, (System.Object) null);
					c.append(tracker.ChangeCommand);
					
					// Apply Auto-move key
					if (backMap.getMoveKey() != null)
					{
						c.append(outer.keyEvent(backMap.getMoveKey()));
					}
				}
				setProperty(BACK_MAP, (System.Object) null);
				setProperty(BACK_POINT, (System.Object) null);
			}
			return c;
		}
		
		/*
		* Offset the destination by the Advanced Options offset
		*/
		protected internal virtual System.Drawing.Point offsetDestination(int x, int y, GamePiece outer)
		{
			int xPos = x + parse("xIndex", xIndex, outer) * parse("xOffset", xOffset, outer);
			int yPos = y + parse("yIndex", yIndex, outer) * parse("yOffset", yOffset, outer);
			return new System.Drawing.Point(xPos, yPos);
		}
		
		private int parse(System.String desc, FormattedString s, GamePiece outer)
		{
			int i = 0;
			System.String val = s.getText(outer, _0);
			try
			{
				i = System.Int32.Parse(val);
			}
			catch (System.FormatException e)
			{
				reportDataError(this, Resources.getString("Error.non_number_error"), s.debugInfo(val, desc), e);
			}
			return i;
		}
		
		public override void  mySetState(System.String newState)
		{
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(newState, ';');
			System.String mapId = st.nextToken("");
			if (mapId.Length > 0)
			{
				setProperty(BACK_MAP, Map.getMapById(mapId));
			}
			System.String x = st.nextToken("");
			System.String y = st.nextToken("");
			if (x.Length > 0 && y.Length > 0)
			{
				try
				{
					setProperty(BACK_POINT, new System.Drawing.Point(System.Int32.Parse(x), System.Int32.Parse(y)));
				}
				catch (System.FormatException e)
				{
					reportDataError(this, Resources.getString("Error.non_number_error"), "Back Point=(" + x + "," + y + ")", e);
				}
			}
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
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(new System.String[]{commandName, backCommandName}, new System.String[]{getCommandDescription(description, "Send command"), getCommandDescription(description, "Back command")});
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
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs arg0)
				{
					Enclosing_Instance.updateVisibility();
				}
			}
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
					Enclosing_Instance.selectMap();
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
					Enclosing_Instance.clearMap();
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
					Enclosing_Instance.selectBoard();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener3
			{
				public AnonymousClassActionListener3(Ed enclosingInstance)
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
					Enclosing_Instance.clearBoard();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener1
			{
				public AnonymousClassPropertyChangeListener1(Ed enclosingInstance)
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
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs arg0)
				{
					Enclosing_Instance.updateVisibility();
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
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(nameInput.ValueString).append(keyInput.ValueString).append(mapIdInput.ValueString).append(boardNameInput.ValueString).append(xInput.ValueString).append(yInput.ValueString).append(backNameInput.ValueString).append(backKeyInput.ValueString).append(xIndexInput.ValueString).append(yIndexInput.ValueString).append(xOffsetInput.ValueString).append(yOffsetInput.ValueString).append(descInput.ValueString).append(destInput.ValueString[0]).append(zoneInput.ValueString).append(regionInput.ValueString).append(propertyInput.ValueString).append(gridLocationInput.ValueString);
					return VassalSharp.counters.SendToLocation.ID + se.Value;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return "";
				}
				
			}
			protected internal StringConfigurer nameInput;
			protected internal StringConfigurer backNameInput;
			protected internal NamedHotKeyConfigurer keyInput;
			protected internal NamedHotKeyConfigurer backKeyInput;
			protected internal FormattedStringConfigurer mapIdInput;
			protected internal FormattedStringConfigurer boardNameInput;
			protected internal FormattedStringConfigurer xInput;
			protected internal FormattedStringConfigurer yInput;
			protected internal BooleanConfigurer advancedInput;
			protected internal FormattedStringConfigurer xIndexInput;
			protected internal FormattedStringConfigurer xOffsetInput;
			protected internal FormattedStringConfigurer yIndexInput;
			protected internal FormattedStringConfigurer yOffsetInput;
			protected internal StringConfigurer descInput;
			protected internal StringEnumConfigurer destInput;
			protected internal PropertyExpressionConfigurer propertyInput;
			protected internal FormattedStringConfigurer zoneInput;
			protected internal FormattedStringConfigurer regionInput;
			//protected Map map;
			protected internal System.Windows.Forms.Panel controls;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			protected internal Box mapControls;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			protected internal Box boardControls;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			protected internal Box advancedControls;
			protected internal StringConfigurer gridLocationInput;
			
			public Ed(SendToLocation p)
			{
				controls = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				controls.setLayout(new BoxLayout(controls, BoxLayout.Y_AXIS));
				
				descInput = new StringConfigurer(null, "Description:  ", p.description);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(descInput.Controls);
				
				nameInput = new StringConfigurer(null, "Command name:  ", p.commandName);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(nameInput.Controls);
				
				keyInput = new NamedHotKeyConfigurer(null, "Keyboard Command:  ", p.key);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(keyInput.Controls);
				
				backNameInput = new StringConfigurer(null, "Send Back Command name:  ", p.backCommandName);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(backNameInput.Controls);
				
				backKeyInput = new NamedHotKeyConfigurer(null, "Send Back Keyboard Command:  ", p.backKey);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(backKeyInput.Controls);
				
				destInput = new StringEnumConfigurer(null, "Destination:  ", VassalSharp.counters.SendToLocation.DEST_OPTIONS);
				destInput.setValue(VassalSharp.counters.SendToLocation.DEST_LOCATION);
				for (int i = 0; i < VassalSharp.counters.SendToLocation.DEST_OPTIONS.Length; i++)
				{
					if (VassalSharp.counters.SendToLocation.DEST_OPTIONS[i].Substring(0, (1) - (0)).Equals(p.destination))
					{
						destInput.setValue(VassalSharp.counters.SendToLocation.DEST_OPTIONS[i]);
					}
				}
				destInput.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(destInput.Controls);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				mapControls = Box.createHorizontalBox();
				mapIdInput = new FormattedExpressionConfigurer(null, "Map:  ", p.mapId.Format, p);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				mapControls.Controls.Add(mapIdInput.Controls);
				System.Windows.Forms.Button select = SupportClass.ButtonSupport.CreateStandardButton("Select");
				select.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(select);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				mapControls.Controls.Add(select);
				System.Windows.Forms.Button clear = SupportClass.ButtonSupport.CreateStandardButton("Clear");
				clear.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(clear);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				mapControls.Controls.Add(clear);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(mapControls);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				boardControls = Box.createHorizontalBox();
				boardNameInput = new FormattedExpressionConfigurer(null, "Board:  ", p.boardName.Format, p);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				boardControls.Controls.Add(boardNameInput.Controls);
				select = SupportClass.ButtonSupport.CreateStandardButton("Select");
				select.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(select);
				clear = SupportClass.ButtonSupport.CreateStandardButton("Clear");
				clear.Click += new System.EventHandler(new AnonymousClassActionListener3(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(clear);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				boardControls.Controls.Add(select);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				boardControls.Controls.Add(clear);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(boardControls);
				
				
				xInput = new FormattedExpressionConfigurer(null, "X Position:  ", p.x.Format, p);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(xInput.Controls);
				
				yInput = new FormattedExpressionConfigurer(null, "Y Position:  ", p.y.Format, p);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(yInput.Controls);
				
				zoneInput = new FormattedExpressionConfigurer(null, "Zone Name:  ", p.zone.Format, p);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(zoneInput.Controls);
				
				regionInput = new FormattedExpressionConfigurer(null, "Region Name:  ", p.region.Format, p);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(regionInput.Controls);
				
				propertyInput = new PropertyExpressionConfigurer(null, "Property Match:  ", p.propertyFilter);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(propertyInput.Controls);
				
				gridLocationInput = new StringConfigurer(null, "Grid Location:  ", p.gridLocation.Format);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(gridLocationInput.Controls);
				
				advancedInput = new BooleanConfigurer(null, "Advanced Options", false);
				advancedInput.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener1(this).propertyChange);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(advancedInput.Controls);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				advancedControls = Box.createHorizontalBox();
				xIndexInput = new FormattedExpressionConfigurer(null, "Additional X offset:  ", p.xIndex.Format, p);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				advancedControls.Controls.Add(xIndexInput.Controls);
				xOffsetInput = new FormattedExpressionConfigurer(null, " times ", p.xOffset.Format, p);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				advancedControls.Controls.Add(xOffsetInput.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(advancedControls);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				advancedControls = Box.createHorizontalBox();
				yIndexInput = new FormattedExpressionConfigurer(null, "Additional Y offset:  ", p.yIndex.Format, p);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				advancedControls.Controls.Add(yIndexInput.Controls);
				yOffsetInput = new FormattedExpressionConfigurer(null, " times ", p.yOffset.Format, p);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				advancedControls.Controls.Add(yOffsetInput.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(advancedControls);
				
				updateVisibility();
			}
			
			private void  updateVisibility()
			{
				//      boolean advancedVisible = advancedInput.booleanValue().booleanValue();
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(advancedInput.Controls, "Visible", !destInput.getValue().Equals(VassalSharp.counters.SendToLocation.DEST_GRIDLOCATION));
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				bool advancedVisible = advancedInput.booleanValue() && advancedInput.Controls.Visible;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(xIndexInput.Controls, "Visible", advancedVisible);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(xOffsetInput.Controls, "Visible", advancedVisible);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(yIndexInput.Controls, "Visible", advancedVisible);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(yOffsetInput.Controls, "Visible", advancedVisible);
				
				System.String destOption = destInput.ValueString;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(xInput.Controls, "Visible", destOption.Equals(VassalSharp.counters.SendToLocation.DEST_LOCATION));
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(yInput.Controls, "Visible", destOption.Equals(VassalSharp.counters.SendToLocation.DEST_LOCATION));
				mapControls.Visible = !destOption.Equals(VassalSharp.counters.SendToLocation.DEST_COUNTER);
				boardControls.Visible = destOption.Equals(VassalSharp.counters.SendToLocation.DEST_LOCATION) || destOption.Equals(VassalSharp.counters.SendToLocation.DEST_GRIDLOCATION);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(zoneInput.Controls, "Visible", destOption.Equals(VassalSharp.counters.SendToLocation.DEST_ZONE));
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(regionInput.Controls, "Visible", destOption.Equals(VassalSharp.counters.SendToLocation.DEST_REGION));
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(propertyInput.Controls, "Visible", destOption.Equals(VassalSharp.counters.SendToLocation.DEST_COUNTER));
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(gridLocationInput.Controls, "Visible", destOption.Equals(VassalSharp.counters.SendToLocation.DEST_GRIDLOCATION));
				
				//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
				System.Windows.Forms.Form w = (System.Windows.Forms.Form) controls.Parent;
				if (w != null)
				{
					//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
					w.pack();
				}
			}
			
			private void  clearBoard()
			{
				boardNameInput.setValue("");
			}
			
			private void  clearMap()
			{
				//map = null;
				mapIdInput.setValue("");
			}
			
			private void  selectBoard()
			{
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.getAncestorOfClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
				ChooseComponentDialog d = new ChooseComponentDialog((System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), controls), typeof(Board));
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				d.Visible = true;
				if (d.Target != null)
				{
					Board b = (Board) d.Target;
					boardNameInput.setValue(b.Name);
				}
			}
			
			private void  selectMap()
			{
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.getAncestorOfClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
				ChooseComponentDialog d = new ChooseComponentDialog((System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), controls), typeof(Map));
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				d.Visible = true;
				if (d.Target != null)
				{
					Map map = (Map) d.Target;
					mapIdInput.setValue(map.getMapName());
				}
			}
		}
	}
}