/*
* $Id$
*
* Copyright (c) 2003-2013 by David Sullivan, Rodney Kinney,
* Brent Easton, and Joel Uckelman.
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
//UPGRADE_TODO: The type 'java.awt.dnd.DragSourceMotionListener' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DragSourceMotionListener = java.awt.dnd.DragSourceMotionListener;
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using AutoConfigurable = VassalSharp.build.AutoConfigurable;
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using IllegalBuildException = VassalSharp.build.IllegalBuildException;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Board = VassalSharp.build.module.map.boardPicker.Board;
using Zone = VassalSharp.build.module.map.boardPicker.board.mapgrid.Zone;
using SumProperties = VassalSharp.build.module.properties.SumProperties;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using ColorConfigurer = VassalSharp.configure.ColorConfigurer;
using Configurer = VassalSharp.configure.Configurer;
using ConfigurerFactory = VassalSharp.configure.ConfigurerFactory;
using FormattedStringConfigurer = VassalSharp.configure.FormattedStringConfigurer;
using HotKeyConfigurer = VassalSharp.configure.HotKeyConfigurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using PropertyExpression = VassalSharp.configure.PropertyExpression;
using SingleChildInstance = VassalSharp.configure.SingleChildInstance;
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
using StringEnum = VassalSharp.configure.StringEnum;
using VisibilityCondition = VassalSharp.configure.VisibilityCondition;
using BasicPiece = VassalSharp.counters.BasicPiece;
using ColoredBorder = VassalSharp.counters.ColoredBorder;
using Deck = VassalSharp.counters.Deck;
using DeckVisitorDispatcher = VassalSharp.counters.DeckVisitorDispatcher;
using GamePiece = VassalSharp.counters.GamePiece;
using Labeler = VassalSharp.counters.Labeler;
using PieceFilter = VassalSharp.counters.PieceFilter;
using PieceFinder = VassalSharp.counters.PieceFinder;
using PieceIterator = VassalSharp.counters.PieceIterator;
using Properties = VassalSharp.counters.Properties;
using Stack = VassalSharp.counters.Stack;
using Resources = VassalSharp.i18n.Resources;
using FormattedString = VassalSharp.tools.FormattedString;
namespace VassalSharp.build.module.map
{
	
	/// <summary> This is a {@link Drawable} class that draws the counters horizontally when
	/// the mouse is held over a stack with the control key down.
	/// 
	/// </summary>
	/// <author>  David Sullivan
	/// </author>
	/// <version>  1.0
	/// </version>
	public class CounterDetailViewer:AbstractConfigurable, Drawable, DragSourceMotionListener
	{
		static private System.Int32 state173;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(CounterDetailViewer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(CounterDetailViewer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private CounterDetailViewer enclosingInstance;
			public CounterDetailViewer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				if (Enclosing_Instance.mouseInView)
					Enclosing_Instance.showDetails();
			}
		}
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state173 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state173 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private void  InitBlock()
		{
			summaryReportFormat = new FormattedString("$" + BasicPiece.LOCATION_NAME + "$");
			emptyHexReportFormat = new FormattedString("$" + BasicPiece.LOCATION_NAME + "$");
			
			System.Object owner = null;
			
			fixBounds(pieces);
			
			if (bounds.Width > 0)
			{
				
				System.Drawing.Rectangle visibleRect = comp.getVisibleRect();
				bounds.X = System.Math.Min(bounds.X, visibleRect.X + visibleRect.Width - bounds.Width);
				if (bounds.X < visibleRect.X)
					bounds.X = visibleRect.X;
				bounds.Y = System.Math.Min(bounds.Y, visibleRect.Y + visibleRect.Height - bounds.Height) - (TextUnderCounters?15:0);
				int minY = visibleRect.Y + (textVisible?g.getFontMetrics().getHeight() + 6:0);
				if (bounds.Y < minY)
					bounds.Y = minY;
				
				if (!bgColor.IsEmpty)
				{
					g.setColor(bgColor);
					g.fillRect(bounds.X, bounds.Y, bounds.Width, bounds.Height);
				}
				if (!fgColor.IsEmpty)
				{
					g.setColor(fgColor);
					g.drawRect(bounds.X - 1, bounds.Y - 1, bounds.Width + 1, bounds.Height + 1);
					g.drawRect(bounds.X - 2, bounds.Y - 2, bounds.Width + 3, bounds.Height + 3);
				}
				//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Drawing.Drawing2D.GraphicsPath oldClip = g.getClip();
				
				int borderOffset = borderWidth;
				double graphicsZoom = graphicsZoomLevel;
				for (int i = 0; i < pieces.size(); i++)
				{
					// Draw the next piece
					// pt is the location of the left edge of the piece
					GamePiece piece = pieces.get_Renamed(i);
					System.Drawing.Rectangle pieceBounds = getBounds(piece);
					if (unrotatePieces)
						piece.setProperty(VassalSharp.counters.Properties_Fields.USE_UNROTATED_SHAPE, (System.Object) true);
					g.setClip(bounds.X - 3, bounds.Y - 3, bounds.Width + 5, bounds.Height + 5);
					//UPGRADE_NOTE: Final was removed from the declaration of 'parent '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Stack parent = piece.Parent;
					if (parent is Deck)
					{
						owner = piece.getProperty(VassalSharp.counters.Properties_Fields.OBSCURED_BY);
						//UPGRADE_NOTE: Final was removed from the declaration of 'faceDown '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						bool faceDown = ((Deck) parent).FaceDown;
						piece.setProperty(VassalSharp.counters.Properties_Fields.OBSCURED_BY, (System.Object) (faceDown?Deck.NO_USER:null));
					}
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					piece.draw(g, bounds.X - (int) (pieceBounds.X * graphicsZoom) + borderOffset, bounds.Y - (int) (pieceBounds.Y * graphicsZoom) + borderWidth, comp, graphicsZoom);
					if (parent is Deck)
						piece.setProperty(VassalSharp.counters.Properties_Fields.OBSCURED_BY, owner);
					if (unrotatePieces)
						piece.setProperty(VassalSharp.counters.Properties_Fields.USE_UNROTATED_SHAPE, (System.Object) false);
					g.setClip(oldClip);
					
					if (TextUnderCounters)
					{
						System.String text = counterReportFormat.getLocalizedText(piece);
						if (text.Length > 0)
						{
							//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
							int x = bounds.X - (int) (pieceBounds.X * graphicsZoom) + borderOffset;
							int y = bounds.Y + bounds.Height + 10;
							drawLabel(g, new System.Drawing.Point(x, y), text, Labeler.CENTER, Labeler.CENTER);
						}
					}
					
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					bounds.Offset((int) (pieceBounds.Width * graphicsZoom), 0);
					borderOffset += borderWidth;
				}
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(GamePiece piece: pieces)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'pieceBounds '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size pieceBounds = getBounds(piece).Size;
				//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
				bounds.Width += (int) System.Math.Round(pieceBounds.Width * graphicsZoomLevel) + borderWidth;
				//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
				bounds.Height = System.Math.Max(bounds.Height, (int) System.Math.Round(pieceBounds.Height * graphicsZoomLevel) + borderWidth * 2);
			}
			
			bounds.Width += borderWidth;
			bounds.Y -= bounds.Height;
			/*
			* Label with the location If the counter viewer is being displayed, then
			* place the location name just above the left hand end of the counters. If
			* no counter viewer (i.e. single piece or expanded stack), then place the
			* location name above the centre of the first piece in the stack.
			*/
			System.String report = "";
			int x = bounds.X - bounds.Width;
			int y = bounds.Y - 5;
			System.String offboard = Resources.getString("Map.offboard"); //$NON-NLS-1$
			
			if (displayablePieces.isEmpty())
			{
				//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
				System.Drawing.Point mapPt = map.mapCoordinates(currentMousePosition.getPoint());
				System.Drawing.Point snapPt = map.snapTo(mapPt);
				System.String locationName = map.localizedLocationName(snapPt);
				emptyHexReportFormat.setProperty(BasicPiece.LOCATION_NAME, locationName.Equals(offboard)?"":locationName);
				emptyHexReportFormat.setProperty(BasicPiece.CURRENT_MAP, map.getLocalizedMapName());
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				Board b = map.findBoard(ref snapPt);
				System.String boardName = (b == null)?"":b.LocalizedName;
				emptyHexReportFormat.setProperty(BasicPiece.CURRENT_BOARD, boardName);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				Zone z = map.findZone(ref snapPt);
				System.String zone = (z == null)?"":z.LocalizedName;
				emptyHexReportFormat.setProperty(BasicPiece.CURRENT_ZONE, zone);
				report = emptyHexReportFormat.getLocalizedText();
				x -= g.getFontMetrics().stringWidth(report) / 2;
			}
			else
			{
				GamePiece topPiece = displayablePieces.get_Renamed(0);
				System.String locationName = (System.String) topPiece.getLocalizedProperty(BasicPiece.LOCATION_NAME);
				emptyHexReportFormat.setProperty(BasicPiece.LOCATION_NAME, locationName.Equals(offboard)?"":locationName);
				report = summaryReportFormat.getLocalizedText(new SumProperties(displayablePieces));
				x += borderWidth * pieces.size() + 2;
			}
			
			if (report.Length > 0)
			{
				drawLabel(g, new System.Drawing.Point(x, y), report, Labeler.RIGHT, Labeler.BOTTOM);
			}
			GamePiece[] allPieces = map.getPieces(); // All pieces from bottom up
			
			//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
			Visitor visitor = new Visitor(new Filter(this), map, map.mapCoordinates(currentMousePosition.getPoint()));
			DeckVisitorDispatcher dispatcher = new DeckVisitorDispatcher(visitor);
			
			/*
			* Process pieces from the top down to make it easier to check for top layer
			* only.
			*/
			for (int i = allPieces.Length - 1; i >= 0; i--)
			{
				dispatcher.accept(allPieces[i]);
			}
			
			return visitor.getPieces();
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > []
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				String.
			}
		}
		virtual protected internal bool TextUnderCounters
		{
			get
			{
				return textVisible && counterReportFormat.Format.Length > 0;
			}
			
		}
		virtual protected internal double Zoom
		{
			get
			{
				return map.Zoom;
			}
			
		}
		virtual protected internal int PreferredDelay
		{
			get
			{
				return (System.Int32) GameModule.getGameModule().getPrefs().getValue(PREFERRED_DELAY);
			}
			
		}
		override public Configurer Configurer
		{
			/*
			* Compatibility. If this component has not yet been saved by this version of
			* vassal, convert the old-style options to new and update the version.
			*/
			
			get
			{
				
				// New version 2 viewer being created
				if (map == null)
				{
					version = LATEST_VERSION;
				}
				// Previous version needing upgrading?
				else if (!version.Equals(LATEST_VERSION))
				{
					upgrade();
				}
				return base.Configurer;
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[]{VERSION, DELAY, HOTKEY, BG_COLOR, FG_COLOR, MINIMUM_DISPLAYABLE, ZOOM_LEVEL, DRAW_PIECES, DRAW_PIECES_AT_ZOOM, GRAPH_SINGLE_DEPRECATED, BORDER_WIDTH, SHOW_TEXT, SHOW_TEXT_SINGLE_DEPRECATED, FONT_SIZE, SUMMARY_REPORT_FORMAT, COUNTER_REPORT_FORMAT, EMPTY_HEX_REPORT_FORMAT, DISPLAY, LAYER_LIST, PROPERTY_FILTER, SHOW_NOSTACK, SHOW_MOVE_SELECTED, SHOW_NON_MOVABLE, UNROTATE_PIECES, SHOW_DECK};
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[]{Resources.getString("Editor.MouseOverStackViewer.version"), Resources.getString("Editor.MouseOverStackViewer.recommend_delay"), Resources.getString("Editor.MouseOverStackViewer.keyboard_shortcut"), Resources.getString("Editor.MouseOverStackViewer.bg_color"), Resources.getString("Editor.MouseOverStackViewer.text_color"), Resources.getString("Editor.MouseOverStackViewer.display_pieces"), Resources.getString("Editor.MouseOverStackViewer.display_zoom"), Resources.getString("Editor.MouseOverStackViewer.draw_pieces"), Resources.getString("Editor.MouseOverStackViewer.draw_zoom"), Resources.getString("Editor.MouseOverStackViewer.display_graphics_obselete"), Resources.getString("Editor.MouseOverStackViewer.piece_gap"), Resources.getString("Editor.MouseOverStackViewer.display_text"), Resources.getString("Editor.MouseOverStackViewer.display_text_obsolete"), Resources.getString("Editor.MouseOverStackViewer.font_size"), Resources.getString("Editor.MouseOverStackViewer.summary_text"), Resources.getString("Editor.MouseOverStackViewer.text_below"), Resources.getString("Editor.MouseOverStackViewer.text_empty"), Resources.getString("Editor.MouseOverStackViewer.include_pieces"), Resources.getString("Editor.MouseOverStackViewer.listed_layers"), Resources.getString("Editor.MouseOverStackViewer.piece_filter"), Resources.getString("Editor.MouseOverStackViewer.non_stacking"), Resources.getString("Editor.MouseOverStackViewer.move_selected"), Resources.getString("Editor.MouseOverStackViewer.non_moveable"), Resources.getString("Editor.MouseOverStackViewer.unrotated_state"), Resources.getString("Editor.MouseOverStackViewer.top_deck")};
			}
			
		}
		
		public const System.String LATEST_VERSION = "2";
		public const System.String USE_KEYBOARD = "ShowCounterDetails";
		public const System.String PREFERRED_DELAY = "PreferredDelay";
		
		public const System.String DELAY = "delay";
		public const System.String ALWAYS_SHOW_LOC = "alwaysshowloc";
		public const System.String DRAW_PIECES = "showgraph";
		public const System.String GRAPH_SINGLE_DEPRECATED = "showgraphsingle";
		public const System.String MINIMUM_DISPLAYABLE = "minDisplayPieces";
		public const System.String HOTKEY = "hotkey";
		
		public const System.String SHOW_TEXT = "showtext";
		public const System.String SHOW_TEXT_SINGLE_DEPRECATED = "showtextsingle";
		public const System.String ZOOM_LEVEL = "zoomlevel";
		public const System.String DRAW_PIECES_AT_ZOOM = "graphicsZoom";
		public const System.String BORDER_WIDTH = "borderWidth";
		public const System.String SHOW_NOSTACK = "showNoStack";
		public const System.String SHOW_MOVE_SELECTED = "showMoveSelectde";
		public const System.String SHOW_NON_MOVABLE = "showNonMovable";
		public const System.String SHOW_DECK = "showDeck";
		public const System.String UNROTATE_PIECES = "unrotatePieces";
		public const System.String DISPLAY = "display";
		public const System.String LAYER_LIST = "layerList";
		public const System.String SUMMARY_REPORT_FORMAT = "summaryReportFormat";
		public const System.String COUNTER_REPORT_FORMAT = "counterReportFormat";
		public const System.String EMPTY_HEX_REPORT_FORMAT = "emptyHexReportForma";
		public const System.String VERSION = "version";
		public const System.String FG_COLOR = "fgColor";
		public const System.String BG_COLOR = "bgColor";
		public const System.String FONT_SIZE = "fontSize";
		public const System.String PROPERTY_FILTER = "propertyFilter";
		
		public const System.String TOP_LAYER = "from top-most layer only";
		public const System.String ALL_LAYERS = "from all layers";
		public const System.String INC_LAYERS = "from listed layers only";
		public const System.String EXC_LAYERS = "from layers other than those listed";
		public const System.String FILTER = "by using a property filter";
		
		public const System.String SUM = "sum(propertyName)";
		
		protected internal System.Windows.Forms.KeyEventArgs hotkey = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.Space | (int) System.Windows.Forms.Keys.Control));
		protected internal Map map;
		protected internal int delay = 700;
		protected internal System.Timers.Timer delayTimer;
		
		protected internal bool graphicsVisible = false;
		protected internal bool textVisible = false;
		protected internal System.Windows.Forms.MouseEventArgs currentMousePosition;
		
		protected internal int minimumDisplayablePieces = 2;
		protected internal bool alwaysShowLoc = false;
		protected internal bool drawPieces = true;
		protected internal bool drawSingleDeprecated = false;
		protected internal bool showText = false;
		protected internal bool showTextSingleDeprecated = false;
		protected internal bool unrotatePieces = false;
		protected internal bool showDeck = false;
		protected internal double zoomLevel = 1.0;
		protected internal double graphicsZoomLevel = 1.0;
		protected internal int borderWidth = 0;
		protected internal bool showNoStack = false;
		protected internal bool showMoveSelected = false;
		protected internal bool showNonMovable = false;
		protected internal System.String displayWhat = TOP_LAYER;
		protected internal System.String[] displayLayers = new System.String[0];
		//UPGRADE_NOTE: The initialization of  'summaryReportFormat' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal FormattedString summaryReportFormat;
		protected internal FormattedString counterReportFormat = new FormattedString("");
		//UPGRADE_NOTE: The initialization of  'emptyHexReportFormat' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal FormattedString emptyHexReportFormat;
		protected internal System.String version = "";
		protected internal System.Drawing.Color fgColor = System.Drawing.Color.Black;
		protected internal System.Drawing.Color bgColor;
		protected internal int fontSize = 9;
		protected internal PropertyExpression propertyFilter = new PropertyExpression();
		
		protected internal System.Drawing.Rectangle bounds;
		protected internal bool mouseInView = true;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < GamePiece > displayablePieces = null;
		
		/// <summary>the JComponent which is repainted when the detail viewer changes </summary>
		protected internal System.Windows.Forms.Control view;
		
		public CounterDetailViewer()
		{
			InitBlock();
			// Set up the timer; this isn't the real delay---we always check the
			// preferences for that.
			delayTimer = new System.Timers.Timer();
			delayTimer.Elapsed += new System.Timers.ElapsedEventHandler(new AnonymousClassActionListener(this).actionPerformed);
			delayTimer.Interval = delay;
			
			delayTimer.AutoReset = false;
		}
		
		public override void  addTo(Buildable b)
		{
			map = (Map) b;
			view = map.getView();
			validator = new SingleChildInstance(map, GetType());
			map.addDrawComponent(this);
			System.String keyDesc = hotkey == null?"":"(" + HotKeyConfigurer.getString(hotkey) + ")";
			GameModule.getGameModule().getPrefs().addOption(Resources.getString("Prefs.general_tab"), new BooleanConfigurer(USE_KEYBOARD, Resources.getString("CounterDetailViewer.use_prompt", keyDesc), false));
			GameModule.getGameModule().getPrefs().addOption(Resources.getString("Prefs.general_tab"), new IntConfigurer(PREFERRED_DELAY, Resources.getString("CounterDetailViewer.delay_prompt"), delay));
			
			view.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouseMoved);
			view.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.build.module.map.CounterDetailViewer.mouseDown);
			view.Click += new System.EventHandler(this.mouseClicked);
			view.MouseEnter += new System.EventHandler(this.mouseEntered);
			view.MouseLeave += new System.EventHandler(this.mouseExited);
			view.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mousePressed);
			view.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseReleased);
			view.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.build.module.map.CounterDetailViewer.keyDown);
			view.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyPressed);
			view.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyReleased);
			view.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyTyped);
			//UPGRADE_ISSUE: Method 'java.awt.dnd.DragSource.getDefaultDragSource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000'"
			DragSource.getDefaultDragSource().addDragSourceMotionListener(this);
			
			setAttributeTranslatable(VERSION, false);
			setAttributeTranslatable(SUMMARY_REPORT_FORMAT, true);
			setAttributeTranslatable(COUNTER_REPORT_FORMAT, true);
		}
		
		public virtual void  draw(System.Drawing.Graphics g, Map map)
		{
			//UPGRADE_TODO: Method 'javax.swing.JComponent.getVisibleRect' was converted to 'System.Windows.Forms.Control.DisplayRectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentgetVisibleRect'"
			//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
			if (currentMousePosition != null && view.DisplayRectangle.Contains(currentMousePosition.getPoint()))
			{
				//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
				System.Drawing.Point tempAux = currentMousePosition.getPoint();
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				draw(g, ref tempAux, view);
			}
		}
		
		public virtual bool drawAboveCounters()
		{
			return true;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  draw(System.Drawing.Graphics g, ref System.Drawing.Point pt, System.Windows.Forms.Control comp)
		{
			
			if (!graphicsVisible && !textVisible)
			{
				return ;
			}
			
			bounds = new System.Drawing.Rectangle(pt.X, pt.Y, 0, 0);
			
			if (graphicsVisible)
			{
				drawGraphics(g, pt, comp, displayablePieces);
			}
			
			if (textVisible)
			{
				drawText(g, pt, comp, displayablePieces);
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		// Required for backward compatibility
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual void  drawGraphics(System.Drawing.Graphics g, ref System.Drawing.Point pt, System.Windows.Forms.Control comp, PieceIterator pi)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < GamePiece > a = new ArrayList < GamePiece >();
			while (pi.hasMoreElements())
			{
				a.add(pi.nextPiece());
			}
			drawGraphics(g, pt, comp, a);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void drawGraphics(Graphics g, Point pt, JComponent comp, List < GamePiece > pieces)
		
		/// <summary>Set the bounds field large enough to accommodate the given set of pieces </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void fixBounds(List < GamePiece > pieces)
		
		protected internal virtual System.Drawing.Rectangle getBounds(GamePiece piece)
		{
			if (unrotatePieces)
				piece.setProperty(VassalSharp.counters.Properties_Fields.USE_UNROTATED_SHAPE, (System.Object) true);
			System.Drawing.Rectangle pieceBounds = System.Drawing.Rectangle.Truncate(piece.Shape.GetBounds());
			if (unrotatePieces)
				piece.setProperty(VassalSharp.counters.Properties_Fields.USE_UNROTATED_SHAPE, (System.Object) false);
			return pieceBounds;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		// Required for backward compatibility
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual void  drawText(System.Drawing.Graphics g, ref System.Drawing.Point pt, System.Windows.Forms.Control comp, PieceIterator pi)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < GamePiece > a = new ArrayList < GamePiece >();
			while (pi.hasMoreElements())
			{
				a.add(pi.nextPiece());
			}
			drawText(g, pt, comp, a);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void drawText(Graphics g, Point pt, JComponent comp, List < GamePiece > pieces)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		// Required for backward compatibility
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual void  drawLabel(System.Drawing.Graphics g, ref System.Drawing.Point pt, System.String label)
		{
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			drawLabel(g, ref pt, label, Labeler.RIGHT, Labeler.BOTTOM);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual void  drawLabel(System.Drawing.Graphics g, ref System.Drawing.Point pt, System.String label, int hAlign, int vAlign)
		{
			
			if (label != null)
			{
				System.Drawing.Color labelFgColor = fgColor.IsEmpty?System.Drawing.Color.Black:fgColor;
				System.Drawing.Graphics g2d = ((System.Drawing.Graphics) g);
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_TEXT_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_TEXT_ANTIALIASING_f'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_TEXT_ANTIALIAS_OFF' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_TEXT_ANTIALIAS_OFF_f'"
				g2d.setRenderingHint(RenderingHints.KEY_TEXT_ANTIALIASING, RenderingHints.VALUE_TEXT_ANTIALIAS_OFF);
				//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
				//UPGRADE_TODO: Field 'java.awt.Font.PLAIN' was converted to 'System.Drawing.FontStyle.Regular' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFontPLAIN_f'"
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				Labeler.drawLabel(g, label, pt.X, pt.Y, new System.Drawing.Font("Dialog", fontSize, System.Drawing.FontStyle.Regular), hAlign, vAlign, ref labelFgColor, ref bgColor, ref labelFgColor);
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_TEXT_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_TEXT_ANTIALIASING_f'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_TEXT_ANTIALIAS_ON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_TEXT_ANTIALIAS_ON_f'"
				g2d.setRenderingHint(RenderingHints.KEY_TEXT_ANTIALIASING, RenderingHints.VALUE_TEXT_ANTIALIAS_ON);
			}
		}
		
		protected internal virtual void  showDetails()
		{
			
			displayablePieces = getDisplayablePieces();
			
			/*
			* Visibility Rules: Stack - Depends on setting of showGraphics/showText
			* Single Unit - Depends on setting of showGraphics/showText and
			* showGraphicsSingle/showTextSingle and stack must not be expanded. Empty
			* space - Depends on setting of
			*/
			
			double zoom = Zoom;
			if (displayablePieces.size() < minimumDisplayablePieces)
			{
				if (displayablePieces.size() > 0)
				{
					graphicsVisible = zoom < zoomLevel;
					textVisible = zoom < zoomLevel && (summaryReportFormat.Format.Length > 0 || counterReportFormat.Format.Length > 0);
				}
				else
				{
					textVisible = (minimumDisplayablePieces == 0 && emptyHexReportFormat.Format.Length > 0);
					graphicsVisible = false;
				}
			}
			else
			{
				graphicsVisible = drawPieces;
				textVisible = showText && (summaryReportFormat.Format.Length > 0 || counterReportFormat.Format.Length > 0);
			}
			map.repaint();
		}
		
		/// <summary> Build an ArrayList of pieces to be displayed in order from bottom up, based
		/// on selection criteria setup in config.
		/// </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < GamePiece > getDisplayablePieces()
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Filter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Utility class to select the pieces we wish to view.</summary>
		protected internal class Filter : PieceFilter
		{
			private void  InitBlock(CounterDetailViewer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private CounterDetailViewer enclosingInstance;
			public CounterDetailViewer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			protected internal int topLayer;
			
			public Filter(CounterDetailViewer enclosingInstance)
			{
				InitBlock(enclosingInstance);
				topLayer = - 1;
			}
			
			public virtual bool accept(GamePiece piece)
			{
				return accept(piece, 0, "");
			}
			
			public virtual bool accept(GamePiece piece, int layer, System.String layerName)
			{
				
				// Is it visible to us?
				if (true.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)))
				{
					return false;
				}
				
				// If it Does Not Stack, do we want to see it?
				if (true.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.NO_STACK)) && !Enclosing_Instance.showNoStack)
				{
					return false;
				}
				
				if (true.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.NON_MOVABLE)) && !Enclosing_Instance.showNonMovable)
				{
					return false;
				}
				
				if (true.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.TERRAIN)) && !Enclosing_Instance.showMoveSelected)
				{
					return false;
				}
				
				
				// Deck?
				if (piece.Parent is Deck && !Enclosing_Instance.showDeck)
				{
					return false;
				}
				
				// Select by property filter
				if (Enclosing_Instance.displayWhat.Equals(VassalSharp.build.module.map.CounterDetailViewer.FILTER))
				{
					return Enclosing_Instance.propertyFilter.accept(piece);
				}
				// Looking at All Layers accepts anything.
				else if (Enclosing_Instance.displayWhat.Equals(VassalSharp.build.module.map.CounterDetailViewer.ALL_LAYERS))
				{
					return true;
				}
				else
				{
					
					if (topLayer < 0)
					{
						topLayer = layer;
					}
					
					// Pieces are passed to us top down, so only display the top-most layer
					if (Enclosing_Instance.displayWhat.Equals(VassalSharp.build.module.map.CounterDetailViewer.TOP_LAYER))
					{
						return layer == topLayer;
					}
					// Include pieces on named layers only
					else if (Enclosing_Instance.displayWhat.Equals(VassalSharp.build.module.map.CounterDetailViewer.INC_LAYERS))
					{
						for (int i = 0; i < Enclosing_Instance.displayLayers.Length; i++)
						{
							if (layerName.Equals(Enclosing_Instance.displayLayers[i]))
							{
								return true;
							}
						}
					}
					// Exclude pieces from named layers.
					else if (Enclosing_Instance.displayWhat.Equals(VassalSharp.build.module.map.CounterDetailViewer.EXC_LAYERS))
					{
						for (int i = 0; i < Enclosing_Instance.displayLayers.Length; i++)
						{
							if (layerName.Equals(Enclosing_Instance.displayLayers[i]))
							{
								return false;
							}
						}
						return true;
					}
				}
				
				// Ignore anything else
				return false;
			}
		}
		
		/*
		* Utility class to visit Map pieces, apply the filter and return a list of
		* pieces we are interested in.
		*/
		protected internal class Visitor:VassalSharp.counters.Movable
		{
			private void  InitBlock()
			{
				return pieces;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			protected List < GamePiece > pieces;
			protected internal Filter filter = null;
			protected internal CompoundPieceCollection collection;
			protected internal int lastLayer = - 1;
			protected internal int insertPos = 0;
			protected internal System.Drawing.Point foundPieceAt;
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			public Visitor(Filter filter, Map map, ref System.Drawing.Point pt):base(map, pt)
			{
				if (map.getPieceCollection() is CompoundPieceCollection)
				{
					collection = (CompoundPieceCollection) map.getPieceCollection();
				}
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				pieces = new ArrayList < GamePiece >();
				this.filter = filter;
			}
			
			public virtual System.Object visitDeck(Deck d)
			{
				if (foundPieceAt.IsEmpty)
				{
					GamePiece top = d.topPiece();
					if (top != null && !true.Equals(top.getProperty(VassalSharp.counters.Properties_Fields.OBSCURED_TO_ME)))
					{
						System.Drawing.Rectangle r = (System.Drawing.Rectangle) d.Shape;
						r.X += d.Position.X;
						r.Y += d.Position.Y;
						if (r.contains(pt))
						{
							apply(top);
						}
					}
				}
				return null;
			}
			
			public virtual System.Object visitStack(Stack s)
			{
				bool addContents = foundPieceAt.IsEmpty?base.visitStack(s) != null:foundPieceAt.Equals(s.Position);
				if (addContents)
				{
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				}
				return null;
			}
			
			public virtual System.Object visitDefault(GamePiece p)
			{
				if (foundPieceAt.IsEmpty?base.visitDefault(p) != null:foundPieceAt.Equals(p.Position))
				{
					apply(p);
				}
				return null;
			}
			
			/*
			* Insert accepted pieces into the start of the array since we are being
			* passed pieces from the top down.
			*/
			protected internal virtual void  apply(GamePiece p)
			{
				int layer = 0;
				System.String layerName = "";
				
				layer = collection.getLayerForPiece(p);
				layerName = collection.getLayerNameForPiece(p);
				
				if (filter == null || filter.accept(p, layer, layerName))
				{
					if (layer != lastLayer)
					{
						insertPos = 0;
						lastLayer = layer;
					}
					
					if (foundPieceAt.IsEmpty)
					{
						foundPieceAt = p.Position;
					}
					
					pieces.add(insertPos++, p);
				}
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public List < GamePiece > getPieces()
		}
		
		public virtual void  mouseMoved(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			// clear details when mouse moved
			if (graphicsVisible || textVisible)
			{
				hideDetails();
			}
			else
			{
				currentMousePosition = e;
				
				if (false.Equals(GameModule.getGameModule().getPrefs().getValue(USE_KEYBOARD)))
				{
					
					// Restart timer
					if (delayTimer.Enabled)
						delayTimer.Stop();
					//UPGRADE_ISSUE: Method 'javax.swing.Timer.setInitialDelay' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingTimersetInitialDelay_int'"
					delayTimer.setInitialDelay(PreferredDelay);
					delayTimer.Start();
				}
			}
		}
		
		public virtual void  mouseDragged(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			mouseMoved(event_sender, e);
		}
		
		public virtual void  mouseClicked(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseEntered(System.Object event_sender, System.EventArgs e)
		{
			mouseInView = true;
		}
		
		public virtual void  mouseExited(System.Object event_sender, System.EventArgs e)
		{
			mouseInView = false;
		}
		
		public virtual void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (delayTimer.Enabled)
				delayTimer.Stop();
		}
		
		public virtual void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			mouseInView = true;
			if (delayTimer.Enabled)
				delayTimer.Stop();
		}
		
		public virtual void  dragMouseMoved(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
		{
			// This prevents the viewer from popping up during piece drags.
			if (delayTimer.Enabled)
				delayTimer.Stop();
		}
		
		public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		}
		
		public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (hotkey != null && true.Equals(GameModule.getGameModule().getPrefs().getValue(USE_KEYBOARD)))
			{
				//UPGRADE_ISSUE: Method 'javax.swing.KeyStroke.getKeyStrokeForEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingKeyStrokegetKeyStrokeForEvent_javaawteventKeyEvent'"
				if (hotkey.Equals(KeyStroke.getKeyStrokeForEvent(event_sender, e)))
				{
					showDetails();
				}
				else
				{
					hideDetails();
				}
			}
		}
		
		public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
		}
		
		protected internal virtual void  hideDetails()
		{
			graphicsVisible = false;
			textVisible = false;
			map.repaint();
		}
		
		protected internal virtual void  upgrade()
		{
			
			if (!drawPieces && !showText)
			{
				minimumDisplayablePieces = System.Int32.MaxValue;
			}
			else if (drawSingleDeprecated)
			{
				minimumDisplayablePieces = 1;
			}
			else
			{
				minimumDisplayablePieces = 2;
			}
			
			fgColor = map.getHighlighter() is ColoredBorder?((ColoredBorder) map.getHighlighter()).Color:System.Drawing.Color.Black;
			
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Color.getRed' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Color.getGreen' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Color.getBlue' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			bgColor = System.Drawing.Color.FromArgb(255 - (int) fgColor.R, 255 - (int) fgColor.G, 255 - (int) fgColor.B);
			
			version = LATEST_VERSION;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Integer.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		KeyStroke.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Color.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Color.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		MinConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Double.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Double.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Integer.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Integer.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		ReportFormatConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		CounterFormatConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		EmptyFormatConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		DisplayConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		String [].
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		PropertyExpression.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	public class DisplayConfig:StringEnum
	{
		public override System.String[] getValidValues(AutoConfigurable target)
		{
			return new System.String[]{TOP_LAYER, ALL_LAYERS, INC_LAYERS, EXC_LAYERS, FILTER};
		}
	}
	
	public class MinConfig:StringEnum
	{
		public override System.String[] getValidValues(AutoConfigurable target)
		{
			return new System.String[]{"0", "1", "2"};
		}
	}
	
	public class EmptyFormatConfig : ConfigurerFactory
	{
		public virtual Configurer getConfigurer(AutoConfigurable c, System.String key, System.String name)
		{
			return new FormattedStringConfigurer(key, name, new System.String[]{BasicPiece.LOCATION_NAME, BasicPiece.CURRENT_MAP, BasicPiece.CURRENT_BOARD, BasicPiece.CURRENT_ZONE});
		}
	}
	
	public class ReportFormatConfig : ConfigurerFactory
	{
		public virtual Configurer getConfigurer(AutoConfigurable c, System.String key, System.String name)
		{
			return new FormattedStringConfigurer(key, name, new System.String[]{BasicPiece.LOCATION_NAME, BasicPiece.CURRENT_MAP, BasicPiece.CURRENT_BOARD, BasicPiece.CURRENT_ZONE, SUM});
		}
	}
	
	public class CounterFormatConfig : ConfigurerFactory
	{
		public virtual Configurer getConfigurer(AutoConfigurable c, System.String key, System.String name)
		{
			return new FormattedStringConfigurer(key, name, new System.String[]{BasicPiece.PIECE_NAME});
		}
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Class < ? > [] getAllowableConfigureComponents()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new Class < ? > [0];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public HelpFile getHelpFile()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return HelpFile.getReferenceManualPage(Map.htm, StackViewer);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void removeFrom(Buildable parent)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		map.removeDrawComponent(this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	view.removeMouseMotionListener(this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setAttribute(String name, Object value)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(DELAY.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		value = Integer.valueOf((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	if(value != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		delay =((Integer) value).intValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(HOTKEY.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		hotkey = HotKeyConfigurer.decode((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		hotkey =(KeyStroke) value;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(DRAW_PIECES.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof Boolean)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		drawPieces =((Boolean) value).booleanValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		drawPieces = true.equals(value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(GRAPH_SINGLE_DEPRECATED.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof Boolean)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		drawSingleDeprecated =((Boolean) value).booleanValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		drawSingleDeprecated = true.equals(value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SHOW_TEXT.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof Boolean)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		showText =((Boolean) value).booleanValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		showText = true.equals(value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SHOW_TEXT_SINGLE_DEPRECATED.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof Boolean)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		showTextSingleDeprecated =((Boolean) value).booleanValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		showTextSingleDeprecated = true.equals(value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(ZOOM_LEVEL.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		value = Double.valueOf((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	zoomLevel =((Double) value).doubleValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(DRAW_PIECES_AT_ZOOM.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		value = Double.valueOf((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	graphicsZoomLevel =((Double) value).doubleValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(BORDER_WIDTH.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		value = Integer.valueOf((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	borderWidth =((Integer) value).intValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SHOW_NOSTACK.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof Boolean)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		showNoStack =((Boolean) value).booleanValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		showNoStack = true.equals(value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SHOW_MOVE_SELECTED.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof Boolean)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		showMoveSelected =((Boolean) value).booleanValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		showMoveSelected = true.equals(value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SHOW_NON_MOVABLE.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof Boolean)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		showNonMovable =((Boolean) value).booleanValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		showNonMovable = true.equals(value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SHOW_DECK.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof Boolean)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		showDeck =((Boolean) value).booleanValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		showDeck = true.equals(value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(UNROTATE_PIECES.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof Boolean)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		unrotatePieces =((Boolean) value).booleanValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		unrotatePieces = true.equals(value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(DISPLAY.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		displayWhat =(String) value;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(LAYER_LIST.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		value = StringArrayConfigurer.stringToArray((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	displayLayers =(String []) value;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(EMPTY_HEX_REPORT_FORMAT.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		emptyHexReportFormat.setFormat((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SUMMARY_REPORT_FORMAT.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		summaryReportFormat.setFormat((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(COUNTER_REPORT_FORMAT.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		counterReportFormat.setFormat((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(MINIMUM_DISPLAYABLE.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		minimumDisplayablePieces = Integer.parseInt((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(NumberFormatException e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		throw new IllegalBuildException(e);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(VERSION.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		version =(String) value;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(FG_COLOR.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		value = ColorConfigurer.stringToColor((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	fgColor = value == null ? Color.black:(Color) value;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(BG_COLOR.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		value = ColorConfigurer.stringToColor((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	bgColor =(Color) value;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(FONT_SIZE.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(value instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		value = Integer.valueOf((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	if(value != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		fontSize =((Integer) value).intValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(PROPERTY_FILTER.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		propertyFilter.setExpression((String) value);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getAttributeValueString(String name)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(DELAY.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(delay);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(HOTKEY.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return HotKeyConfigurer.encode(hotkey);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(DRAW_PIECES.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(drawPieces);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(GRAPH_SINGLE_DEPRECATED.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(drawSingleDeprecated);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SHOW_TEXT.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(showText);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SHOW_TEXT_SINGLE_DEPRECATED.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(showTextSingleDeprecated);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(ZOOM_LEVEL.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(zoomLevel);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(DRAW_PIECES_AT_ZOOM.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(graphicsZoomLevel);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(BORDER_WIDTH.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(borderWidth);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SHOW_NOSTACK.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(showNoStack);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SHOW_MOVE_SELECTED.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(showMoveSelected);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SHOW_NON_MOVABLE.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(showNonMovable);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SHOW_DECK.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(showDeck);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(UNROTATE_PIECES.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(unrotatePieces);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(DISPLAY.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return displayWhat;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(LAYER_LIST.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return StringArrayConfigurer.arrayToString(displayLayers);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(EMPTY_HEX_REPORT_FORMAT.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return emptyHexReportFormat.getFormat();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SUMMARY_REPORT_FORMAT.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return summaryReportFormat.getFormat();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(COUNTER_REPORT_FORMAT.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return counterReportFormat.getFormat();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(MINIMUM_DISPLAYABLE.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(minimumDisplayablePieces);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(VERSION.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return version;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(FG_COLOR.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return ColorConfigurer.colorToString(fgColor);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(BG_COLOR.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return ColorConfigurer.colorToString(bgColor);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(FONT_SIZE.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(fontSize);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(PROPERTY_FILTER.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return propertyFilter.getExpression();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else 
	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public static String getConfigureTypeName()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return Resources.getString(Editor.MouseOverStackViewer.component_type); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public VisibilityCondition getAttributeVisibility(String name)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(BORDER_WIDTH.equals(name) || DRAW_PIECES_AT_ZOOM.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new VisibilityCondition()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean shouldBeVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return drawPieces;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(FONT_SIZE.equals(name) || SUMMARY_REPORT_FORMAT.equals(name) || COUNTER_REPORT_FORMAT.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new VisibilityCondition()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean shouldBeVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return showText;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(DRAW_PIECES.equals(name) || SHOW_TEXT.equals(name) || SHOW_NOSTACK.equals(name) || SHOW_DECK.equals(name) || DISPLAY.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new VisibilityCondition()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean shouldBeVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return true;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(LAYER_LIST.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new VisibilityCondition()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean shouldBeVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return(displayWhat.equals(INC_LAYERS) || displayWhat.equals(EXC_LAYERS));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(PROPERTY_FILTER.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new VisibilityCondition()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean shouldBeVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return displayWhat.equals(FILTER);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(EMPTY_HEX_REPORT_FORMAT.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new VisibilityCondition()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean shouldBeVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return showText && minimumDisplayablePieces == 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SHOW_MOVE_SELECTED.equals(name) || SHOW_NON_MOVABLE.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new VisibilityCondition()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean shouldBeVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return showNoStack;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	/*
	* The following fields are not to be displayed. They are either obsolete
	* or maintained for backward compatibility
	*/
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	else if(VERSION.equals(name) || SHOW_TEXT_SINGLE_DEPRECATED.equals(name) || GRAPH_SINGLE_DEPRECATED.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new VisibilityCondition()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean shouldBeVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return false;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}