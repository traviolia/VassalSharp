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
using Chatter = VassalSharp.build.module.Chatter;
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Board = VassalSharp.build.module.map.boardPicker.Board;
using Zone = VassalSharp.build.module.map.boardPicker.board.mapgrid.Zone;
using PropertyNameSource = VassalSharp.build.module.properties.PropertyNameSource;
using AddPiece = VassalSharp.command.AddPiece;
using ChangePiece = VassalSharp.command.ChangePiece;
using Command = VassalSharp.command.Command;
using RemovePiece = VassalSharp.command.RemovePiece;
using Localization = VassalSharp.i18n.Localization;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using Resources = VassalSharp.i18n.Resources;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
using ScaledImagePainter = VassalSharp.tools.imageop.ScaledImagePainter;
namespace VassalSharp.counters
{
	
	/// <summary> Basic class for representing a physical component of the game Can be a counter, a card, or an overlay</summary>
	public class BasicPiece : TranslatablePiece, StateMergeable, PropertyNameSource
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < String > l = new ArrayList < String >();
			l.add(LOCATION_NAME);
			l.add(CURRENT_MAP);
			l.add(CURRENT_BOARD);
			l.add(CURRENT_ZONE);
			l.add(CURRENT_X);
			l.add(CURRENT_Y);
			l.add(OLD_LOCATION_NAME);
			l.add(OLD_MAP);
			l.add(OLD_BOARD);
			l.add(OLD_ZONE);
			l.add(OLD_X);
			l.add(OLD_Y);
			l.add(BASIC_NAME);
			l.add(PIECE_NAME);
			l.add(DECK_NAME);
			return l;
		}
		virtual public System.String Type
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SequenceEncoder se = new SequenceEncoder(cloneKey > 0?System.Convert.ToString(cloneKey):"", ';');
				return ID + se.append(deleteKey > 0?System.Convert.ToString(deleteKey):"").append(imageName).append(commonName).Value;
			}
			
		}
		virtual protected internal KeyCommand[] KeyCommands
		{
			get
			{
				if (commands == null)
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					final ArrayList < KeyCommand > l = new ArrayList < KeyCommand >();
					//UPGRADE_NOTE: Final was removed from the declaration of 'target '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					GamePiece target = Decorator.getOutermost(this);
					if (cloneKey > 0)
					{
						l.add(new KeyCommand("Clone", new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (cloneKey | (int) System.Windows.Forms.Keys.Control)), target));
					}
					if (deleteKey > 0)
					{
						l.add(new KeyCommand("Delete", new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (deleteKey | (int) System.Windows.Forms.Keys.Control)), target));
					}
					commands = l.toArray(new KeyCommand[l.size()]);
				}
				//UPGRADE_NOTE: Final was removed from the declaration of 'outer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GamePiece outer = Decorator.getOutermost(this);
				bool canAdjustPosition = outer.getMap() != null && outer.Parent != null && outer.Parent.topPiece() != Parent.bottomPiece();
				enableCommand("Move up", canAdjustPosition);
				enableCommand("Move down", canAdjustPosition);
				enableCommand("Move to top", canAdjustPosition);
				enableCommand("Move to bottom", canAdjustPosition);
				enableCommand("Clone", outer.getMap() != null);
				enableCommand("Delete", outer.getMap() != null);
				return commands;
			}
			
		}
		virtual public System.Drawing.Point Position
		{
			get
			{
				return Parent == null?new System.Drawing.Point(new System.Drawing.Size(pos)):Parent.Position;
			}
			
			set
			{
				if (getMap() != null && Parent == null)
				{
					getMap().repaint(getMap().boundingBoxOf(Decorator.getOutermost(this)));
				}
				pos = value;
				if (getMap() != null && Parent == null)
				{
					getMap().repaint(getMap().boundingBoxOf(Decorator.getOutermost(this)));
				}
			}
			
		}
		virtual public Stack Parent
		{
			get
			{
				return parent;
			}
			
			set
			{
				parent = value;
			}
			
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		virtual public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				return boundingBox();
			}
			
		}
		virtual public System.String LocalizedName
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'key '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String key = VassalSharp.i18n.TranslatablePiece_Fields.PREFIX + getName();
				return Localization.Instance.translate(key, getName());
			}
			
		}
		virtual public System.String State
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SequenceEncoder se = new SequenceEncoder(';');
				//UPGRADE_NOTE: Final was removed from the declaration of 'mapName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String mapName = map == null?"null":map.getIdentifier();
				se.append(mapName);
				//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point p = Position;
				se.append(p.X).append(p.Y);
				se.append(GpId);
				return se.Value;
			}
			
			set
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'outer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GamePiece outer = Decorator.getOutermost(this);
				//UPGRADE_NOTE: Final was removed from the declaration of 'oldMap '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Map oldMap = getMap();
				//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(value, ';');
				//UPGRADE_NOTE: Final was removed from the declaration of 'mapId '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String mapId = st.nextToken();
				Map newMap = null;
				if (!"null".Equals(mapId))
				{
					newMap = Map.getMapById(mapId);
					if (newMap == null)
					{
						Decorator.reportDataError(this, Resources.getString("Error.not_found", "Map"), "mapId=" + mapId);
					}
				}
				//UPGRADE_NOTE: Final was removed from the declaration of 'newPos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point newPos = new System.Drawing.Point(st.nextInt(0), st.nextInt(0));
				Position = newPos;
				if (newMap != oldMap)
				{
					if (newMap != null)
					{
						// This will remove from oldMap
						// and set the map to newMap
						newMap.addPiece(outer);
					}
					else if (oldMap != null)
					{
						oldMap.removePiece(outer);
						setMap(null);
					}
					else
					{
						setMap(null);
					}
				}
				GpId = st.nextToken("");
			}
			
		}
		virtual public System.String Id
		{
			get
			{
				return id;
			}
			
			set
			{
				this.id = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Get the Highlighter instance for drawing selected pieces. Note that since this is a static method, all pieces in a
		/// module will always use the same Highlighter
		/// </summary>
		/// <summary> Set the Highlighter for all pieces</summary>
		public static Highlighter Highlighter
		{
			get
			{
				if (highlighter == null)
				{
					highlighter = new ColoredBorder();
				}
				return highlighter;
			}
			
			set
			{
				highlighter = value;
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				return "Basic Piece";
			}
			
		}
		virtual public System.String GpId
		{
			get
			{
				System.String id = (System.String) getProperty(VassalSharp.counters.Properties_Fields.PIECE_ID);
				return id == null?"":id;
			}
			
			set
			{
				setProperty(VassalSharp.counters.Properties_Fields.PIECE_ID, value == null?"":value);
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("BasicPiece.htm");
			}
			
		}
		public const System.String ID = "piece;";
		private static Highlighter highlighter;
		/// <summary> Return information about the current location of the piece through getProperty():
		/// 
		/// LocationName - Current Location Name of piece as displayed in Chat Window CurrentX - Current X position CurrentY -
		/// Current Y position CurrentMap - Current Map name or "" if not on a map CurrentBoard - Current Board name or "" if
		/// not on a map CurrentZone - If the current map has a multi-zoned grid, then return the name of the Zone the piece is
		/// in, or "" if the piece is not in any zone, or not on a map
		/// </summary>
		public const System.String LOCATION_NAME = "LocationName";
		public const System.String CURRENT_MAP = "CurrentMap";
		public const System.String CURRENT_BOARD = "CurrentBoard";
		public const System.String CURRENT_ZONE = "CurrentZone";
		public const System.String CURRENT_X = "CurrentX";
		public const System.String CURRENT_Y = "CurrentY";
		public const System.String OLD_LOCATION_NAME = "OldLocationName";
		public const System.String OLD_MAP = "OldMap";
		public const System.String OLD_BOARD = "OldBoard";
		public const System.String OLD_ZONE = "OldZone";
		public const System.String OLD_X = "OldX";
		public const System.String OLD_Y = "OldY";
		public const System.String BASIC_NAME = "BasicName";
		public const System.String PIECE_NAME = "PieceName";
		public const System.String DECK_NAME = "DeckName";
		public const System.String DECK_POSITION = "DeckPosition";
		//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
		public static System.Drawing.Font POPUP_MENU_FONT = new System.Drawing.Font("Dialog", 11, System.Drawing.FontStyle.Regular);
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		protected internal System.Windows.Forms.ContextMenu popup;
		protected internal System.Drawing.Rectangle imageBounds;
		protected internal ScaledImagePainter imagePainter = new ScaledImagePainter();
		private Map map;
		private KeyCommand[] commands;
		private Stack parent;
		private System.Drawing.Point pos = new System.Drawing.Point(0, 0);
		private System.String id;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private java.util.Map < Object, Object > props;
		/// <deprecated> Moved into own traits, retained for backward compatibility 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		private char cloneKey;
		/// <deprecated> Moved into own traits, retained for backward compatibility 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		private char deleteKey;
		/// <deprecated> Replaced by
		/// </deprecated>
		/// <{link>  #srcOp}. </{link>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		protected internal System.Drawing.Image image;
		protected internal System.String imageName;
		private System.String commonName;
		
		public BasicPiece():this(ID + ";;;;")
		{
		}
		
		public BasicPiece(System.String type)
		{
			InitBlock();
			mySetType(type);
		}
		
		public virtual void  mySetType(System.String type)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken();
			cloneKey = st.nextChar('\x0000');
			deleteKey = st.nextChar('\x0000');
			imageName = st.nextToken();
			commonName = st.nextToken();
			imagePainter.ImageName = imageName;
			commands = null;
		}
		
		public virtual void  setMap(Map map)
		{
			if (map != this.map)
			{
				commands = null;
				this.map = map;
			}
		}
		
		public virtual Map getMap()
		{
			return Parent == null?map:Parent.getMap();
		}
		
		public virtual System.Object getProperty(System.Object key)
		{
			if (BASIC_NAME.Equals(key))
			{
				return getName();
			}
			else
				return getPublicProperty(key);
		}
		
		/*
		* Properties visible in a masked unit
		*/
		public virtual System.Object getPublicProperty(System.Object key)
		{
			if (VassalSharp.counters.Properties_Fields.KEY_COMMANDS.Equals(key))
			{
				return KeyCommands;
			}
			else if (LOCATION_NAME.Equals(key))
			{
				return getMap() == null?"":getMap().locationName(Position);
			}
			else if (PIECE_NAME.Equals(key))
			{
				return Decorator.getOutermost(this).getName();
			}
			else if (CURRENT_MAP.Equals(key))
			{
				return getMap() == null?"":getMap().getConfigureName();
			}
			else if (DECK_NAME.Equals(key))
			{
				return Parent is Deck?((Deck) Parent).DeckName:"";
			}
			else if (DECK_POSITION.Equals(key))
			{
				if (Parent is Deck)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'deck '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Deck deck = (Deck) Parent;
					//UPGRADE_NOTE: Final was removed from the declaration of 'size '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int size = deck.PieceCount;
					//UPGRADE_NOTE: Final was removed from the declaration of 'pos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int pos = deck.indexOf(Decorator.getOutermost(this));
					return System.Convert.ToString(size - pos + 1);
				}
				else
				{
					return "0";
				}
			}
			else if (CURRENT_BOARD.Equals(key))
			{
				if (getMap() != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Point tempAux = Position;
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					Board b = getMap().findBoard(ref tempAux);
					if (b != null)
					{
						return b.Name;
					}
				}
				return "";
			}
			else if (CURRENT_ZONE.Equals(key))
			{
				if (getMap() != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'z '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Point tempAux2 = Position;
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					Zone z = getMap().findZone(ref tempAux2);
					if (z != null)
					{
						return z.Name;
					}
				}
				return "";
			}
			else if (CURRENT_X.Equals(key))
			{
				return System.Convert.ToString(Position.X);
			}
			else if (CURRENT_Y.Equals(key))
			{
				return System.Convert.ToString(Position.Y);
			}
			else if (VassalSharp.counters.Properties_Fields.VISIBLE_STATE.Equals(key))
			{
				return "";
			}
			System.Object prop = props == null?null:props.get_Renamed(key);
			if (prop == null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'map '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Map map = getMap();
				//UPGRADE_NOTE: Final was removed from the declaration of 'zone '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point tempAux3 = Position;
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				Zone zone = (map == null?null:map.findZone(ref tempAux3));
				if (zone != null)
				{
					prop = zone.getProperty(key);
				}
				else if (map != null)
				{
					prop = map.getProperty(key);
				}
				else
				{
					prop = GameModule.getGameModule().getProperty(key);
				}
			}
			return prop;
		}
		
		public virtual System.Object getLocalizedProperty(System.Object key)
		{
			if (BASIC_NAME.Equals(key))
			{
				return LocalizedName;
			}
			else
			{
				return getLocalizedPublicProperty(key);
			}
		}
		
		/*
		* Properties visible in a masked unit
		*/
		public virtual System.Object getLocalizedPublicProperty(System.Object key)
		{
			if (VassalSharp.counters.Properties_Fields.KEY_COMMANDS.Equals(key))
			{
				return getProperty(key);
			}
			else if (LOCATION_NAME.Equals(key))
			{
				return getMap() == null?"":getMap().localizedLocationName(Position);
			}
			else if (PIECE_NAME.Equals(key))
			{
				return Decorator.getOutermost(this).getName();
			}
			else if (BASIC_NAME.Equals(key))
			{
				return LocalizedName;
			}
			else if (CURRENT_MAP.Equals(key))
			{
				return getMap() == null?"":getMap().LocalizedConfigureName;
			}
			else if (DECK_NAME.Equals(key))
			{
				return getProperty(key);
			}
			else if (DECK_POSITION.Equals(key))
			{
				if (Parent is Deck)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'deck '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Deck deck = (Deck) Parent;
					//UPGRADE_NOTE: Final was removed from the declaration of 'size '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int size = deck.PieceCount;
					//UPGRADE_NOTE: Final was removed from the declaration of 'pos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int pos = deck.indexOf(Decorator.getOutermost(this));
					return System.Convert.ToString(size - pos);
				}
				else
				{
					return "0";
				}
			}
			else if (CURRENT_BOARD.Equals(key))
			{
				if (getMap() != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Point tempAux = Position;
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					Board b = getMap().findBoard(ref tempAux);
					if (b != null)
					{
						return b.LocalizedName;
					}
				}
				return "";
			}
			else if (CURRENT_ZONE.Equals(key))
			{
				if (getMap() != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'z '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Point tempAux2 = Position;
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					Zone z = getMap().findZone(ref tempAux2);
					if (z != null)
					{
						return z.LocalizedName;
					}
				}
				return "";
			}
			else if (CURRENT_X.Equals(key))
			{
				return getProperty(key);
			}
			else if (CURRENT_Y.Equals(key))
			{
				return getProperty(key);
			}
			else if (VassalSharp.counters.Properties_Fields.VISIBLE_STATE.Equals(key))
			{
				return getProperty(key);
			}
			System.Object prop = props == null?null:props.get_Renamed(key);
			if (prop == null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'map '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Map map = getMap();
				//UPGRADE_NOTE: Final was removed from the declaration of 'zone '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point tempAux3 = Position;
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				Zone zone = (map == null?null:map.findZone(ref tempAux3));
				if (zone != null)
				{
					prop = zone.getLocalizedProperty(key);
				}
				else if (map != null)
				{
					prop = map.getLocalizedProperty(key);
				}
				else
				{
					prop = GameModule.getGameModule().getLocalizedProperty(key);
				}
			}
			return prop;
		}
		
		public virtual void  setProperty(System.Object key, System.Object val)
		{
			if (props == null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				props = new HashMap < Object, Object >();
			}
			if (val == null)
			{
				props.remove(key);
			}
			else
			{
				props.put(key, val);
			}
		}
		
		protected internal virtual System.Object prefsValue(System.String s)
		{
			return GameModule.getGameModule().getPrefs().getValue(s);
		}
		
		public virtual void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			if (imageBounds.IsEmpty)
			{
				imageBounds = boundingBox();
			}
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			imagePainter.draw(g, x + (int) (zoom * imageBounds.X), y + (int) (zoom * imageBounds.Y), zoom, obs);
		}
		
		private void  enableCommand(System.String name, bool enable)
		{
			for (int i = 0; i < commands.Length; ++i)
			{
				if (name.Equals(commands[i].Name))
				{
					commands[i].setEnabled(enable);
				}
			}
		}
		
		private bool isEnabled(System.Windows.Forms.KeyEventArgs stroke)
		{
			if (stroke == null)
			{
				return false;
			}
			for (int i = 0; i < commands.Length; ++i)
			{
				if (stroke.Equals(commands[i].KeyStroke))
				{
					return commands[i].isEnabled();
				}
			}
			return true;
		}
		
		public virtual System.Drawing.Rectangle boundingBox()
		{
			if (imageBounds.IsEmpty)
			{
				System.Drawing.Size tempAux = imagePainter.ImageSize;
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				imageBounds = ImageUtils.getBounds(ref tempAux);
			}
			System.Drawing.Rectangle temp_Rectangle;
			temp_Rectangle = imageBounds;
			return new System.Drawing.Rectangle(temp_Rectangle.Location, temp_Rectangle.Size);
		}
		
		public bool equals(GamePiece c)
		{
			return c == this;
		}
		
		public virtual System.String getName()
		{
			return commonName;
		}
		
		public virtual Command keyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			VassalSharp.counters.KeyCommand[] generatedAux = KeyCommands;
			if (!isEnabled(stroke))
			{
				return null;
			}
			Command comm = null;
			//UPGRADE_NOTE: Final was removed from the declaration of 'outer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GamePiece outer = Decorator.getOutermost(this);
			if (cloneKey != 0 && new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (cloneKey | (int) System.Windows.Forms.Keys.Control)).Equals(stroke))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'newPiece '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GamePiece newPiece = ((AddPiece) GameModule.getGameModule().decode(GameModule.getGameModule().encode(new AddPiece(outer)))).Target;
				newPiece.Id = null;
				GameModule.getGameModule().getGameState().addPiece(newPiece);
				newPiece.State = outer.State;
				comm = new AddPiece(newPiece);
				if (getMap() != null)
				{
					comm.append(getMap().placeOrMerge(newPiece, Position));
					KeyBuffer.Buffer.remove(outer);
					KeyBuffer.Buffer.add(newPiece);
					if (GlobalOptions.Instance.autoReportEnabled() && !true.Equals(outer.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_OTHERS)))
					{
						System.String s = "* " + outer.LocalizedName;
						//UPGRADE_NOTE: Final was removed from the declaration of 'loc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String loc = getMap().locationName(outer.Position);
						if (loc != null)
						{
							s += (" cloned in " + loc + " * ");
						}
						else
						{
							s += "cloned *";
						}
						//UPGRADE_NOTE: Final was removed from the declaration of 'report '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						Command report = new Chatter.DisplayText(GameModule.getGameModule().getChatter(), s);
						report.execute();
						comm = comm.append(report);
					}
				}
			}
			else if (deleteKey != 0 && new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (deleteKey | (int) System.Windows.Forms.Keys.Control)).Equals(stroke))
			{
				comm = new RemovePiece(outer);
				if (getMap() != null && GlobalOptions.Instance.autoReportEnabled() && !true.Equals(outer.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_OTHERS)))
				{
					System.String s = "* " + outer.LocalizedName;
					//UPGRADE_NOTE: Final was removed from the declaration of 'loc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String loc = getMap().locationName(outer.Position);
					if (loc != null)
					{
						s += (" deleted from " + loc + " * ");
					}
					else
					{
						s += " deleted *";
					}
					//UPGRADE_NOTE: Final was removed from the declaration of 'report '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Command report = new Chatter.DisplayText(GameModule.getGameModule().getChatter(), s);
					comm = comm.append(report);
				}
				comm.execute();
			}
			else if (getMap() != null && stroke.Equals(getMap().StackMetrics.getMoveUpKey()))
			{
				if (parent != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'oldState '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String oldState = parent.State;
					//UPGRADE_NOTE: Final was removed from the declaration of 'index '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int index = parent.indexOf(outer);
					if (index < parent.PieceCount - 1)
					{
						parent.insert(outer, index + 1);
						comm = new ChangePiece(parent.Id, oldState, parent.State);
					}
					else
					{
						getMap().getPieceCollection().moveToFront(parent);
					}
				}
				else
				{
					getMap().getPieceCollection().moveToFront(outer);
				}
			}
			else if (getMap() != null && stroke.Equals(getMap().StackMetrics.getMoveDownKey()))
			{
				if (parent != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'oldState '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String oldState = parent.State;
					//UPGRADE_NOTE: Final was removed from the declaration of 'index '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int index = parent.indexOf(outer);
					if (index > 0)
					{
						parent.insert(outer, index - 1);
						comm = new ChangePiece(parent.Id, oldState, parent.State);
					}
					else
					{
						getMap().getPieceCollection().moveToBack(parent);
					}
				}
				else
				{
					getMap().getPieceCollection().moveToBack(outer);
				}
			}
			else if (getMap() != null && stroke.Equals(getMap().StackMetrics.getMoveTopKey()))
			{
				parent = outer.Parent;
				if (parent != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'oldState '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String oldState = parent.State;
					if (parent.indexOf(outer) < parent.PieceCount - 1)
					{
						parent.insert(outer, parent.PieceCount - 1);
						comm = new ChangePiece(parent.Id, oldState, parent.State);
					}
					else
					{
						getMap().getPieceCollection().moveToFront(parent);
					}
				}
				else
				{
					getMap().getPieceCollection().moveToFront(outer);
				}
			}
			else if (getMap() != null && stroke.Equals(getMap().StackMetrics.getMoveBottomKey()))
			{
				parent = Parent;
				if (parent != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'oldState '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String oldState = parent.State;
					if (parent.indexOf(outer) > 0)
					{
						parent.insert(outer, 0);
						comm = new ChangePiece(parent.Id, oldState, parent.State);
					}
					else
					{
						getMap().getPieceCollection().moveToBack(parent);
					}
				}
				else
				{
					getMap().getPieceCollection().moveToBack(outer);
				}
			}
			return comm;
		}
		
		public virtual void  mergeState(System.String newState, System.String oldState)
		{
			if (!newState.Equals(oldState))
			{
				State = newState;
			}
		}
		
		public virtual PieceEditor getEditor()
		{
			return new Ed(this);
		}
		private class Ed : PieceEditor
		{
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
					return state;
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SequenceEncoder se = new SequenceEncoder(cloneKeyInput.getKey(), ';');
					//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String type = se.append(deleteKeyInput.getKey()).append(picker.getImageName()).append(pieceName.Text).Value;
					return BasicPiece.ID + type;
				}
				
			}
			private System.Windows.Forms.Panel panel;
			private KeySpecifier cloneKeyInput;
			private KeySpecifier deleteKeyInput;
			private System.Windows.Forms.TextBox pieceName;
			private ImagePicker picker;
			private System.String state;
			
			internal Ed(BasicPiece p)
			{
				state = p.State;
				initComponents(p);
			}
			
			private void  initComponents(BasicPiece p)
			{
				panel = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				panel.setLayout(new BoxLayout(panel, BoxLayout.X_AXIS));
				picker = new ImagePicker();
				picker.setImageName(p.imageName);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(picker);
				cloneKeyInput = new KeySpecifier(p.cloneKey);
				deleteKeyInput = new KeySpecifier(p.deleteKey);
				//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
				pieceName = new System.Windows.Forms.TextBox();
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				pieceName.Text = p.commonName;
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				pieceName.setMaximumSize(pieceName.Size);
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box col = Box.createVerticalBox();
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box row = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Name:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				row.Controls.Add(temp_Control);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				row.Controls.Add(pieceName);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				col.Controls.Add(row);
				if (p.cloneKey != 0)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					row = Box.createHorizontalBox();
					System.Windows.Forms.Label temp_label4;
					temp_label4 = new System.Windows.Forms.Label();
					temp_label4.Text = "To Clone:  ";
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control2;
					temp_Control2 = temp_label4;
					row.Controls.Add(temp_Control2);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					row.Controls.Add(cloneKeyInput);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					col.Controls.Add(row);
				}
				if (p.deleteKey != 0)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					row = Box.createHorizontalBox();
					System.Windows.Forms.Label temp_label6;
					temp_label6 = new System.Windows.Forms.Label();
					temp_label6.Text = "To Delete:  ";
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control3;
					temp_Control3 = temp_label6;
					row.Controls.Add(temp_Control3);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					row.Controls.Add(deleteKeyInput);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					col.Controls.Add(row);
				}
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(col);
			}
			
			public virtual void  reset(BasicPiece p)
			{
			}
		}
		
		public override System.String ToString()
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return base.ToString() + "[name=" + getName() + ",type=" + Type + ",state=" + State + "]";
		}
		
		public virtual PieceI18nData getI18nData()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PieceI18nData data = new PieceI18nData(this);
			data.add(commonName, "Basic piece name");
			return data;
		}
		
		/// <summary> Return Property names exposed by this trait</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getPropertyNames()
	}
}