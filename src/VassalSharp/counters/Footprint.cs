/*
* $Id$
*
* Copyright (c) 2000-2012 by Rodney Kinney, Brent Easton, Joel Uckelman
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
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using ColorConfigurer = VassalSharp.configure.ColorConfigurer;
using DoubleConfigurer = VassalSharp.configure.DoubleConfigurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
namespace VassalSharp.counters
{
	
	/// <summary> Displays a movement trail indicating where a piece has been moved</summary>
	public class Footprint:MovementMarkable
	{
		private void  InitBlock()
		{
			return Collections.enumeration(pointList);
		}
		override public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("MovementTrail.htm");
			}
			
		}
		private System.Drawing.Rectangle BB
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'bb '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle bb = piece.boundingBox();
				//UPGRADE_NOTE: Final was removed from the declaration of 'pos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point pos = piece.Position;
				
				bb.X += pos.X;
				bb.Y += pos.Y;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'circleDiameter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int circleDiameter = 2 * circleRadius;
				//UPGRADE_NOTE: Final was removed from the declaration of 'pr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle pr = new System.Drawing.Rectangle();
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(final Point p: pointList)
				{
					pr.setBounds(p.x - circleRadius, p.y - circleRadius, circleDiameter, circleDiameter);
					SupportClass.RectangleSupport.AddRectangleToRectangle(ref bb, pr);
				}
				
				bb.X -= pos.X;
				bb.Y -= pos.Y;
				
				return bb;
			}
			
		}
		override public System.String Description
		{
			get
			{
				return "Movement trail";
			}
			
		}
		/// <summary> Global Visibility means all players see the same trail
		/// Local Visibility means each player controls their own trail visibility
		/// </summary>
		virtual protected internal bool TrailVisible
		{
			get
			{
				if (globallyVisible)
				{
					return globalVisibility || trailKey == null;
				}
				else
				{
					return localVisibility || trailKey == null;
				}
			}
			
		}
		/// <summary> Return the boundingBox including the trail</summary>
		virtual public System.Drawing.Rectangle MyBoundingBox
		{
			get
			{
				if (myBoundingBox.IsEmpty)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'bb '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Rectangle bb = piece.boundingBox();
					//UPGRADE_NOTE: Final was removed from the declaration of 'pos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Point pos = piece.Position;
					
					bb.X += pos.X;
					bb.Y += pos.Y;
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'circleDiameter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int circleDiameter = 2 * circleRadius;
					//UPGRADE_NOTE: Final was removed from the declaration of 'pr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Rectangle pr = new System.Drawing.Rectangle();
					
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(final Point p: pointList)
					{
						pr.setBounds(p.x - circleRadius, p.y - circleRadius, circleDiameter, circleDiameter);
						SupportClass.RectangleSupport.AddRectangleToRectangle(ref bb, pr);
					}
					
					bb.X -= pos.X;
					bb.Y -= pos.Y;
					
					myBoundingBox = bb;
				}
				return myBoundingBox;
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
		
		new public const System.String ID = "footprint;";
		private KeyCommand[] commands;
		
		// State Variables (Saved in logfile/sent to opponent)
		protected internal bool globalVisibility = false; // Shared trail visibility (if globallyVisible == true)
		protected internal System.String startMapId = ""; // Map Id trail started on
		// List of points
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < Point > pointList = new ArrayList < Point >();
		
		// Type Variables (Configured in Ed)
		protected internal NamedKeyStroke trailKey; // Control Key to invoke
		protected internal System.String menuCommand; // Menu Command
		protected internal bool initiallyVisible = false; // Are Trails initially visible?
		protected internal bool globallyVisible = false; // Are Trails shared between players?
		protected internal int circleRadius; // Radius of trail point circle
		protected internal int selectedTransparency; // Transparency of trail when unit is selected
		protected internal int unSelectedTransparency; // Transparency of trail when unit is selected/unselected
		protected internal System.Drawing.Color lineColor; // Color of Trail lines
		protected internal System.Drawing.Color fillColor; // Color of Trail circle fill
		protected internal int edgePointBuffer; // How far Off-map to draw trail points (pixels)?
		protected internal int edgeDisplayBuffer; // How far Off-map to draw trail lines (pixels)?
		
		// Defaults for Type variables
		protected internal const char DEFAULT_TRAIL_KEY = 'T';
		protected internal const System.String DEFAULT_MENU_COMMAND = "Movement Trail";
		//UPGRADE_NOTE: Final was removed from the declaration of 'DEFAULT_INITIALLY_VISIBLE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal static readonly System.Boolean DEFAULT_INITIALLY_VISIBLE = false;
		//UPGRADE_NOTE: Final was removed from the declaration of 'DEFAULT_GLOBALLY_VISIBLE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal static readonly System.Boolean DEFAULT_GLOBALLY_VISIBLE = false;
		protected internal const int DEFAULT_CIRCLE_RADIUS = 10;
		//UPGRADE_NOTE: Final was removed from the declaration of 'DEFAULT_FILL_COLOR '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal static readonly System.Drawing.Color DEFAULT_FILL_COLOR = Color.WHITE;
		//UPGRADE_NOTE: Final was removed from the declaration of 'DEFAULT_LINE_COLOR '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal static readonly System.Drawing.Color DEFAULT_LINE_COLOR = Color.BLACK;
		protected internal const int DEFAULT_SELECTED_TRANSPARENCY = 100;
		protected internal const int DEFULT_UNSELECTED_TRANSPARENCY = 50;
		protected internal const int DEFAULT_EDGE_POINT_BUFFER = 20;
		protected internal const int DEFAULT_EDGE_DISPLAY_BUFFER = 30;
		protected internal const float LINE_WIDTH = 1.0f;
		
		// Local Variables
		protected internal System.Drawing.Rectangle myBoundingBox;
		protected internal System.Drawing.Font font;
		protected internal double lastZoom;
		protected internal bool localVisibility;
		
		protected internal double lineWidth;
		private KeyCommand showTrailCommand;
		
		public Footprint():base(Footprint.ID, null)
		{
			InitBlock();
		}
		
		public Footprint(System.String type, GamePiece p)
		{
			InitBlock();
			mySetType(type);
			setInner(p);
		}
		
		/// <deprecated> Use {@link #pointList} directly. 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected Enumeration < Point > getPointList()
		
		public override void  mySetState(System.String newState)
		{
			pointList.clear();
			//UPGRADE_NOTE: Final was removed from the declaration of 'ss '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder ss = new SequenceEncoder.Decoder(newState, ';');
			globalVisibility = ss.nextBoolean(initiallyVisible);
			startMapId = ss.nextToken("");
			//UPGRADE_NOTE: Final was removed from the declaration of 'items '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int items = ss.nextInt(0);
			for (int i = 0; i < items; i++)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'point '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String point = ss.nextToken("");
				if (point.Length != 0)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'sp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SequenceEncoder.Decoder sp = new SequenceEncoder.Decoder(point, ',');
					//UPGRADE_NOTE: Final was removed from the declaration of 'x '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int x = sp.nextInt(0);
					//UPGRADE_NOTE: Final was removed from the declaration of 'y '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int y = sp.nextInt(0);
					pointList.add(new System.Drawing.Point(x, y));
				}
			}
		}
		
		public override System.String myGetState()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(globalVisibility).append(startMapId).append(pointList.size());
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Point p: pointList)
			{
				se.append(p.x + "," + p.y);
			}
			
			return se.Value;
		}
		
		/// <summary> Type is the character command that toggles footprint visiblity</summary>
		public override void  mySetType(System.String type)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken();
			
			trailKey = st.nextNamedKeyStroke(DEFAULT_TRAIL_KEY);
			menuCommand = st.nextToken(DEFAULT_MENU_COMMAND);
			initiallyVisible = st.nextBoolean(DEFAULT_INITIALLY_VISIBLE);
			globallyVisible = st.nextBoolean(DEFAULT_GLOBALLY_VISIBLE);
			circleRadius = st.nextInt(DEFAULT_CIRCLE_RADIUS);
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			fillColor = st.nextColor(ref DEFAULT_FILL_COLOR);
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			lineColor = st.nextColor(ref DEFAULT_LINE_COLOR);
			selectedTransparency = st.nextInt(DEFAULT_SELECTED_TRANSPARENCY);
			unSelectedTransparency = st.nextInt(DEFULT_UNSELECTED_TRANSPARENCY);
			edgePointBuffer = st.nextInt(DEFAULT_EDGE_POINT_BUFFER);
			edgeDisplayBuffer = st.nextInt(DEFAULT_EDGE_DISPLAY_BUFFER);
			lineWidth = st.nextDouble(LINE_WIDTH);
			
			commands = null;
			showTrailCommand = null;
			
			if (initiallyVisible)
			{
				localVisibility = true;
				globalVisibility = true;
			}
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			se.append(trailKey).append(menuCommand).append(initiallyVisible).append(globallyVisible).append(circleRadius).append(ref fillColor).append(ref lineColor).append(selectedTransparency).append(unSelectedTransparency).append(edgePointBuffer).append(edgeDisplayBuffer).append(lineWidth);
			return ID + se.Value;
		}
		
		public override void  setProperty(System.Object key, System.Object val)
		{
			if (VassalSharp.counters.Properties_Fields.MOVED.Equals(key))
			{
				setMoved(true.Equals(val));
				piece.setProperty(key, val); // Pass on to MovementMarkable
				myBoundingBox = System.Drawing.Rectangle.Empty;
			}
			else
			{
				base.setProperty(key, val);
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.Object getLocalizedProperty(System.Object key)
		{
			if (VassalSharp.counters.Properties_Fields.MOVED.Equals(key))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Object value_Renamed = piece.getProperty(key);
				return value_Renamed == null?base.getProperty(key):value_Renamed;
			}
			return base.getLocalizedProperty(key);
		}
		
		public override System.Object getProperty(System.Object key)
		{
			// If this piece has a real MovementMarkable trait,
			// use it to store the MOVED status
			if (VassalSharp.counters.Properties_Fields.MOVED.Equals(key))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Object value_Renamed = piece.getProperty(key);
				return value_Renamed == null?base.getProperty(key):value_Renamed;
			}
			return base.getProperty(key);
		}
		
		/// <summary> setMoved is called with an argument of true each time the piece is moved.
		/// The argument is false when the unit is marked as not moved.
		/// </summary>
		public override void  setMoved(bool justMoved)
		{
			if (justMoved)
			{
				recordCurrentPosition();
				//UPGRADE_NOTE: Final was removed from the declaration of 'map '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Map map = getMap();
				startMapId = map != null?map.getId():null;
			}
			else
			{
				clearTrail();
			}
			redraw();
		}
		
		protected internal virtual void  recordCurrentPosition()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'here '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Point here = this.Position;
			if (pointList.isEmpty() || !pointList.get_Renamed(pointList.size() - 1).equals(here))
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				addPoint(ref here);
			}
			else
			{
				myBoundingBox = System.Drawing.Rectangle.Empty;
			}
		}
		
		protected internal virtual void  clearTrail()
		{
			pointList.clear();
			System.Drawing.Point tempAux = Position;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			addPoint(ref tempAux);
			localVisibility = initiallyVisible;
			globalVisibility = initiallyVisible;
		}
		
		/// <summary> Add Point to list and adjust the overall boundingBox to encompass the
		/// trail.
		/// </summary>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual void  addPoint(ref System.Drawing.Point p)
		{
			pointList.add(p);
			myBoundingBox = System.Drawing.Rectangle.Empty;
		}
		
		public virtual void  redraw()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Map m = getMap();
			if (m != null)
			{
				m.repaint(MyBoundingBox);
			}
		}
		
		// FIXME: This method is inefficient.
		public override void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			piece.draw(g, x, y, obs, zoom);
			
			// Do nothing when piece is not on a map, we are drawing the map
			// to something other than its normal view, or the trail is invisible,
			if (getMap() == null || getMap().getView() != obs || !TrailVisible)
			{
				return ;
			}
			
			/*
			* If we have changed Maps, then start a new trail. Note that this check is
			* here because setMoved is called before the piece has been moved.
			*/
			//UPGRADE_NOTE: Final was removed from the declaration of 'currentMap '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String currentMap = getMap().getId();
			if (!currentMap.Equals(startMapId))
			{
				startMapId = currentMap;
				clearTrail();
				return ;
			}
			
			// Anything to draw?
			if (pointList.isEmpty())
			{
				return ;
			}
			
			/*
			* If we are asked to be drawn at a different zoom from the current map zoom
			* setting, then don't draw the trail as it will be in the wrong place.
			* (i.e. Mouse-over viewer)
			*/
			double mapZoom = zoom;
			if (this.getMap() != null)
			{
				mapZoom = getMap().Zoom;
				if (zoom != mapZoom)
				{
					return ;
				}
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'g2d '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Graphics g2d = (System.Drawing.Graphics) g;
			//UPGRADE_NOTE: Final was removed from the declaration of 'selected '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			bool selected = true.Equals(Decorator.getOutermost(this).getProperty(VassalSharp.counters.Properties_Fields.SELECTED));
			//UPGRADE_NOTE: Final was removed from the declaration of 'transparencyPercent '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int transparencyPercent = System.Math.Max(0, System.Math.Min(100, selected?selectedTransparency:unSelectedTransparency));
			//UPGRADE_NOTE: Final was removed from the declaration of 'transparency '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			float transparency = transparencyPercent / 100.0f;
			//UPGRADE_NOTE: Final was removed from the declaration of 'oldComposite '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Interface 'java.awt.Composite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComposite'"
			//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.getComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DgetComposite'"
			Composite oldComposite = g2d.getComposite();
			//UPGRADE_NOTE: Final was removed from the declaration of 'oldStroke '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Method 'java.awt.Graphics2D.getStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DgetStroke'"
			System.Drawing.Pen oldStroke = SupportClass.GraphicsManager.manager.GetPen(g2d);
			//UPGRADE_NOTE: Final was removed from the declaration of 'oldColor '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Color oldColor = SupportClass.GraphicsManager.manager.GetColor(g2d);
			
			/*
			* newClip is an overall clipping region made up of the Map itself and a
			* border of edgeDisplayBuffer pixels. No drawing at all outside this area.
			* mapRect is made of the Map and a edgePointBuffer pixel border. Trail
			* points are not drawn outside this area.
			*/
			//UPGRADE_NOTE: Final was removed from the declaration of 'mapHeight '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int mapHeight = getMap().mapSize().height;
			//UPGRADE_NOTE: Final was removed from the declaration of 'mapWidth '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int mapWidth = getMap().mapSize().width;
			//UPGRADE_NOTE: Final was removed from the declaration of 'edgeHeight '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int edgeHeight = System.Int32.Parse(getMap().getAttributeValueString(Map.EDGE_HEIGHT));
			//UPGRADE_NOTE: Final was removed from the declaration of 'edgeWidth '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int edgeWidth = System.Int32.Parse(getMap().getAttributeValueString(Map.EDGE_WIDTH));
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'edgeClipHeight '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int edgeClipHeight = System.Math.Min(edgeHeight, edgeDisplayBuffer);
			//UPGRADE_NOTE: Final was removed from the declaration of 'edgeClipWidth '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int edgeClipWidth = System.Math.Min(edgeWidth, edgeDisplayBuffer);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'clipX '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int clipX = edgeWidth - edgeClipWidth;
			//UPGRADE_NOTE: Final was removed from the declaration of 'clipY '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int clipY = edgeHeight - edgeClipHeight;
			//UPGRADE_NOTE: Final was removed from the declaration of 'width '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int width = mapWidth - 2 * (edgeWidth + edgeClipWidth);
			//UPGRADE_NOTE: Final was removed from the declaration of 'height '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int height = mapHeight - 2 * (edgeHeight + edgeClipHeight);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'newClip '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			System.Drawing.Rectangle newClip = new System.Drawing.Rectangle((int) (clipX * zoom), (int) (clipY * zoom), (int) (width * zoom), (int) (height * zoom));
			//UPGRADE_NOTE: Final was removed from the declaration of 'circleRect '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle circleRect = new System.Drawing.Rectangle(edgeWidth - edgePointBuffer, edgeHeight - edgePointBuffer, mapWidth + 2 * edgePointBuffer, mapHeight + 2 * edgePointBuffer);
			//UPGRADE_NOTE: Final was removed from the declaration of 'visibleRect '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle visibleRect = getMap().getView().getVisibleRect();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'oldClip '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_ISSUE: Method 'java.awt.Graphics.getClip' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphicsgetClip'"
			System.Drawing.Drawing2D.GraphicsPath oldClip = g2d.getClip();
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Rectangle.intersection' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			g2d.SetClip(System.Drawing.Rectangle.Intersect(newClip, visibleRect));
			if (oldClip != null)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Rectangle.intersection' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Graphics.getClipBounds' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				g2d.SetClip(System.Drawing.Rectangle.Intersect(System.Drawing.Rectangle.Truncate(oldClip.GetBounds()), System.Drawing.Rectangle.Truncate(g.ClipBounds)));
			}
			
			//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetComposite_javaawtComposite'"
			//UPGRADE_ISSUE: Method 'java.awt.AlphaComposite.getInstance' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
			//UPGRADE_ISSUE: Field 'java.awt.AlphaComposite.SRC_OVER' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
			g2d.setComposite(AlphaComposite.getInstance(AlphaComposite.SRC_OVER, transparency));
			//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
			//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_ANTIALIASING_f'"
			//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_ANTIALIAS_ON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_ANTIALIAS_ON_f'"
			g2d.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'thickness '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			float thickness = System.Math.Max(1.0f, (float) (zoom * lineWidth));
			//UPGRADE_TODO: Method 'java.awt.Graphics2D.setStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DsetStroke_javaawtStroke'"
			//UPGRADE_TODO: Constructor 'java.awt.BasicStroke.BasicStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtBasicStrokeBasicStroke_float'"
			SupportClass.GraphicsManager.manager.SetPen(g2d, new System.Drawing.Pen(System.Drawing.Brushes.Black, thickness));
			SupportClass.GraphicsManager.manager.SetColor(g2d, lineColor);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'here '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Point here = Position;
			
			/*
			* Draw the tracks between trail points
			*/
			int x1, y1, x2, y2;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Iterator < Point > i = pointList.iterator();
			System.Drawing.Point cur = i.next(), next;
			while (i.hasNext())
			{
				next = i.next();
				
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				x1 = (int) (cur.X * zoom);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				y1 = (int) (cur.Y * zoom);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				x2 = (int) (next.X * zoom);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				y2 = (int) (next.Y * zoom);
				
				drawTrack(g, x1, y1, x2, y2, zoom);
				
				cur = next;
			}
			
			if (!here.Equals(cur))
			{
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				x1 = (int) (cur.X * zoom);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				y1 = (int) (cur.Y * zoom);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				x2 = (int) (here.X * zoom);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				y2 = (int) (here.Y * zoom);
				
				drawTrack(g, x1, y1, x2, y2, zoom);
			}
			
			/*
			* And draw the points themselves.
			*/
			int elementCount = - 1;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(final Point p: pointList)
			{
				++elementCount;
				
				if (circleRect.contains(p) && !p.equals(here))
				{
					drawPoint(g, p, zoom, elementCount);
					
					// Is there an Icon to draw in the circle?
					System.Drawing.Image image = getTrailImage(elementCount);
					x1 = (int) ((p.x - circleRadius) * zoom);
					y1 = (int) ((p.y - circleRadius) * zoom);
					if (selected && image != null)
					{
						if (zoom == 1.0)
						{
							//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
							g.DrawImage(image, x1, y1);
						}
						else
						{
							System.Drawing.Image scaled = ImageUtils.transform((System.Drawing.Bitmap) image, zoom, 0.0);
							//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
							g.DrawImage(scaled, x1, y1);
						}
					}
					
					// Or some text?
					//UPGRADE_NOTE: Final was removed from the declaration of 'text '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String text = getTrailText(elementCount);
					if (selected && text != null)
					{
						if (this.font == null || lastZoom != mapZoom)
						{
							x1 = (int) (p.x * zoom);
							y1 = (int) (p.y * zoom);
							//UPGRADE_NOTE: Final was removed from the declaration of 'font '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
							//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
							//UPGRADE_TODO: Field 'java.awt.Font.PLAIN' was converted to 'System.Drawing.FontStyle.Regular' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFontPLAIN_f'"
							System.Drawing.Font font = new System.Drawing.Font("Dialog", (int) (circleRadius * 1.4 * zoom), System.Drawing.FontStyle.Regular);
							System.Drawing.Color tempAux = System.Drawing.Color.Empty;
							System.Drawing.Color tempAux2 = System.Drawing.Color.Empty;
							//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
							Labeler.drawLabel(g, text, x1, y1, font, Labeler.CENTER, Labeler.CENTER, ref lineColor, ref tempAux, ref tempAux2);
						}
						lastZoom = mapZoom;
					}
				}
			}
			
			//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetComposite_javaawtComposite'"
			g2d.setComposite(oldComposite);
			//UPGRADE_TODO: Method 'java.awt.Graphics2D.setStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DsetStroke_javaawtStroke'"
			SupportClass.GraphicsManager.manager.SetPen(g2d, oldStroke);
			SupportClass.GraphicsManager.manager.SetColor(g2d, oldColor);
			g.SetClip(oldClip);
		}
		
		/// <summary> Draw a Circle at the given point.
		/// Override this method to do something different (eg. display an Icon)
		/// </summary>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual void  drawPoint(System.Drawing.Graphics g, ref System.Drawing.Point p, double zoom, int elementCount)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'x '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int x = (int) ((p.X - circleRadius) * zoom);
			//UPGRADE_NOTE: Final was removed from the declaration of 'y '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int y = (int) ((p.Y - circleRadius) * zoom);
			//UPGRADE_NOTE: Final was removed from the declaration of 'radius '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int radius = (int) (2 * circleRadius * zoom);
			SupportClass.GraphicsManager.manager.SetColor(g, fillColor);
			g.FillEllipse(SupportClass.GraphicsManager.manager.GetPaint(g), x, y, radius, radius);
			SupportClass.GraphicsManager.manager.SetColor(g, lineColor);
			g.DrawEllipse(SupportClass.GraphicsManager.manager.GetPen(g), x, y, radius, radius);
		}
		
		/// <summary> Draw a track from one Point to another.
		/// Don't draw under the circle as it shows
		/// through with transparency turned on.
		/// </summary>
		protected internal virtual void  drawTrack(System.Drawing.Graphics g, int x1, int y1, int x2, int y2, double zoom)
		{
			double lastSqrt = - 1;
			int lastDistSq = - 1;
			
			int distSq = (x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1);
			if (distSq != lastDistSq)
			{
				lastDistSq = distSq;
				lastSqrt = System.Math.Sqrt(distSq);
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'xDiff '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int xDiff = (int) ((circleRadius * (x2 - x1) * zoom) / lastSqrt);
			//UPGRADE_NOTE: Final was removed from the declaration of 'yDiff '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int yDiff = (int) ((circleRadius * (y2 - y1) * zoom) / lastSqrt);
			
			g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), x1 + xDiff, y1 + yDiff, x2 - xDiff, y2 - yDiff);
		}
		
		/// <summary> Override this method to return an Image to display within each trail circle</summary>
		protected internal virtual System.Drawing.Image getTrailImage(int elementCount)
		{
			return null;
		}
		
		/// <summary> Override this method to return text to display within each trail circle.
		/// Note, there will normally be only room for 1 character.
		/// </summary>
		protected internal virtual System.String getTrailText(int elementCount)
		{
			return null;
		}
		
		/// <summary> Return a bounding box covering the whole trail if it is visible, otherwise
		/// just return the standard piece bounding box
		/// </summary>
		public override System.Drawing.Rectangle boundingBox()
		{
			System.Drawing.Rectangle temp_Rectangle;
			temp_Rectangle = MyBoundingBox;
			return TrailVisible && getMap() != null?new System.Drawing.Rectangle(temp_Rectangle.Location, temp_Rectangle.Size):piece.boundingBox();
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		public override KeyCommand[] myGetKeyCommands()
		{
			if (commands == null)
			{
				if (trailKey != null && !trailKey.Null)
				{
					showTrailCommand = new KeyCommand(menuCommand, trailKey, Decorator.getOutermost(this), this);
				}
				if (showTrailCommand != null && menuCommand.Length > 0)
				{
					commands = new KeyCommand[]{showTrailCommand};
				}
				else
				{
					commands = new KeyCommand[0];
				}
			}
			if (showTrailCommand != null)
			{
				showTrailCommand.setEnabled(getMap() != null);
			}
			return commands;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			myGetKeyCommands();
			if (showTrailCommand != null && showTrailCommand.matches(stroke))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'tracker '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ChangeTracker tracker = new ChangeTracker(this);
				if (globallyVisible)
				{
					globalVisibility = !globalVisibility;
				}
				else
				{
					localVisibility = !localVisibility;
				}
				redraw();
				return tracker.ChangeCommand;
			}
			return null;
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public override PieceI18nData getI18nData()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PieceI18nData data = base.getI18nData();
			data.add(menuCommand, "Show Movement Trail command");
			return data;
		}
		
		/// <summary> Key Command Global Visibility Circle Radius Fill Color Line Color Selected
		/// Transparency Unselected Transparency Edge Buffer Display Limit Edge Buffer
		/// Point Limit
		/// </summary>
		new protected internal class Ed : PieceEditor
		{
			virtual public System.String State
			{
				get
				{
					return System.Convert.ToString(gv.booleanValue());
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(VassalSharp.counters.Footprint.ID).append(trailKeyInput.ValueString).append(mc.ValueString).append(iv.ValueString).append(gv.ValueString).append(cr.ValueString).append(fc.ValueString).append(lc.ValueString).append(st.ValueString).append(ut.ValueString).append(pb.ValueString).append(db.ValueString).append(lw.ValueString);
					return se.Value;
				}
				
			}
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return controls;
				}
				
			}
			private NamedHotKeyConfigurer trailKeyInput;
			private System.Windows.Forms.Panel controls;
			private StringConfigurer mc;
			private BooleanConfigurer iv;
			private BooleanConfigurer gv;
			private IntConfigurer cr;
			private ColorConfigurer fc;
			private ColorConfigurer lc;
			private IntConfigurer st;
			private IntConfigurer ut;
			private IntConfigurer pb;
			private IntConfigurer db;
			private DoubleConfigurer lw;
			
			public Ed(Footprint p)
			{
				controls = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				controls.setLayout(new BoxLayout(controls, BoxLayout.Y_AXIS));
				
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box b;
				trailKeyInput = new NamedHotKeyConfigurer(null, "Key Command:  ", p.trailKey);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(trailKeyInput.Controls);
				
				mc = new StringConfigurer(null, "Menu Command:  ", p.menuCommand);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(mc.Controls);
				
				iv = new BooleanConfigurer(null, "Trails start visible?", Boolean.valueOf(p.initiallyVisible));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(iv.Controls);
				
				gv = new BooleanConfigurer(null, "Trails are visible to all players?", Boolean.valueOf(p.globallyVisible));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(gv.Controls);
				
				cr = new IntConfigurer(null, "Circle Radius:  ", p.circleRadius);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(cr.Controls);
				
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				fc = new ColorConfigurer(null, "Circle Fill Color:  ", ref p.fillColor);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(fc.Controls);
				
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				lc = new ColorConfigurer(null, "Line Color:  ", ref p.lineColor);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(lc.Controls);
				
				lw = new DoubleConfigurer(null, "Line thickness:  ", p.lineWidth);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(lw.Controls);
				
				st = new IntConfigurer(null, "Selected Unit Trail Transparency (0-100):  ", p.selectedTransparency);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(st.Controls);
				
				ut = new IntConfigurer(null, "Unselected Unit Trail Transparency (0-100):  ", p.unSelectedTransparency);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(ut.Controls);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				b = Box.createHorizontalBox();
				pb = new IntConfigurer(null, "Display Trail Points Off-map for ", p.edgePointBuffer);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(pb.Controls);
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "pixels";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				b.Controls.Add(temp_Control);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(b);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				b = Box.createHorizontalBox();
				db = new IntConfigurer(null, "Display Trails Off-map for  ", p.edgeDisplayBuffer);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(db.Controls);
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = "pixels";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label4;
				b.Controls.Add(temp_Control2);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(b);
			}
		}
	}
}