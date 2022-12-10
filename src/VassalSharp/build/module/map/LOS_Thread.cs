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
//UPGRADE_TODO: The type 'org.apache.commons.lang.StringUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using StringUtils = org.apache.commons.lang.StringUtils;
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using AutoConfigurable = VassalSharp.build.AutoConfigurable;
using Buildable = VassalSharp.build.Buildable;
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Board = VassalSharp.build.module.map.boardPicker.Board;
using MapGrid = VassalSharp.build.module.map.boardPicker.board.MapGrid;
using ZonedGrid = VassalSharp.build.module.map.boardPicker.board.ZonedGrid;
using Zone = VassalSharp.build.module.map.boardPicker.board.mapgrid.Zone;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using ColorConfigurer = VassalSharp.configure.ColorConfigurer;
using Configurer = VassalSharp.configure.Configurer;
using ConfigurerFactory = VassalSharp.configure.ConfigurerFactory;
using IconConfigurer = VassalSharp.configure.IconConfigurer;
using PlayerIdFormattedStringConfigurer = VassalSharp.configure.PlayerIdFormattedStringConfigurer;
using StringEnum = VassalSharp.configure.StringEnum;
using VisibilityCondition = VassalSharp.configure.VisibilityCondition;
using GamePiece = VassalSharp.counters.GamePiece;
using Resources = VassalSharp.i18n.Resources;
using TranslatableConfigurerFactory = VassalSharp.i18n.TranslatableConfigurerFactory;
using FormattedString = VassalSharp.tools.FormattedString;
using LaunchButton = VassalSharp.tools.LaunchButton;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using UniqueIdManager = VassalSharp.tools.UniqueIdManager;
namespace VassalSharp.build.module.map
{
	
	/// <summary> A class that allows the user to draw a straight line on a Map (LOS
	/// = Line Of Sight).  No automatic detection of obstacles is
	/// performed; the user must simply observe the thread against the
	/// image of the map.  However, if the user clicks on a board with a
	/// {@link Map Grid}, the thread may snap to the grid and report the
	/// distance between endpoints of the line
	/// 
	/// </summary>
	public class LOS_Thread:AbstractConfigurable, Drawable, Configurable, UniqueIdManager.Identifyable, CommandEncoder
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(LOS_Thread enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(LOS_Thread enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private LOS_Thread enclosingInstance;
			public LOS_Thread Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.launch();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(LOS_Thread enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(LOS_Thread enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private LOS_Thread enclosingInstance;
			public LOS_Thread Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				Enclosing_Instance.threadColor = (System.Drawing.Color) evt.NewValue;
			}
		}
		private void  InitBlock()
		{
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > []
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				String.
			}
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <returns> whether the thread should be drawn
		/// </returns>
		/// <summary> If true, draw the thread on the map</summary>
		virtual public bool Visible
		{
			get
			{
				return visible;
			}
			
			set
			{
				visible = value;
			}
			
		}
		/// <summary> The attributes of an LOS_Thread are:
		/// <pre>
		/// <code>NAME</code>:  the name of the Preferences tab
		/// <code>LABEL</code>:  the label of the button
		/// <code>HOTKEY</code>:  the hotkey equivalent of the button
		/// <code>DRAW_RANGE</code>:  If true, draw the distance between endpoints of the thread
		/// <code>RANGE_FOREGROUND</code>:  the color of the text when drawing the distance
		/// <code>RANGE_BACKGROUND</code>:  the color of the background rectangle when drawing the distance
		/// <code>HIDE_COUNTERS</code>:  If true, hide all {@link GamePiece}s on the map when drawing the thread
		/// </pre>
		/// </summary>
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[]{NAME, LABEL, TOOLTIP, ICON_NAME, HOTKEY, REPORT, PERSISTENCE, PERSISTENT_ICON_NAME, GLOBAL, SNAP_START, SNAP_END, DRAW_RANGE, RANGE_SCALE, RANGE_ROUNDING, HIDE_COUNTERS, HIDE_OPACITY, LOS_COLOR, RANGE_FOREGROUND, RANGE_BACKGROUND};
			}
			
		}
		virtual protected internal int Transparency
		{
			set
			{
				if (value < 0)
				{
					hideOpacity = 0;
				}
				else if (value > 100)
				{
					hideOpacity = 100;
				}
				else
				{
					hideOpacity = value;
				}
			}
			
		}
		/// <summary> With Global visibility, LOS_Thread now has a state that needs to be
		/// communicated to clients on other machines
		/// </summary>
		virtual public System.String State
		{
			
			
			get
			{
				SequenceEncoder se = new SequenceEncoder(';');
				se.append(anchor.X).append(anchor.Y).append(arrow.X).append(arrow.Y);
				se.append(persisting);
				se.append(mirroring);
				return se.Value;
			}
			
			set
			{
				SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(value, ';');
				anchor.X = sd.nextInt(anchor.X);
				anchor.Y = sd.nextInt(anchor.Y);
				arrow.X = sd.nextInt(arrow.X);
				arrow.Y = sd.nextInt(arrow.Y);
				Persisting = sd.nextBoolean(false);
				Mirroring = sd.nextBoolean(false);
			}
			
		}
		/// <summary> Commands controlling persistence are passed between players, so LOS Threads
		/// must have a unique ID.
		/// </summary>
		virtual public System.String Id
		{
			get
			{
				return threadId;
			}
			
			set
			{
				threadId = value;
			}
			
		}
		virtual protected internal bool Persisting
		{
			get
			{
				return persisting;
			}
			
			set
			{
				persisting = value;
				visible = value;
				Mirroring = false;
				if (persisting)
				{
					launch_Renamed_Field.setAttribute(ICON_NAME, persistentIconName);
				}
				else
				{
					launch_Renamed_Field.setAttribute(ICON_NAME, iconName);
					map.repaint();
				}
			}
			
		}
		virtual protected internal bool Mirroring
		{
			get
			{
				return mirroring;
			}
			
			set
			{
				mirroring = value;
				if (mirroring)
				{
					visible = true;
				}
			}
			
		}
		virtual protected internal System.Drawing.Point Anchor
		{
			get
			{
				return new System.Drawing.Point(new System.Drawing.Size(anchor));
			}
			
		}
		virtual protected internal System.Drawing.Point Arrow
		{
			get
			{
				return new System.Drawing.Point(new System.Drawing.Size(arrow));
			}
			
		}
		virtual protected internal int LosCheckCount
		{
			get
			{
				return checkList.size();
			}
			
		}
		virtual protected internal System.String LosCheckList
		{
			get
			{
				return StringUtils.join(checkList, ", ");
			}
			
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return Resources.getString("Editor.LosThread.component_type"); //$NON-NLS-1$
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[]{Resources.getString(Resources.NAME_LABEL), Resources.getString(Resources.BUTTON_TEXT), Resources.getString(Resources.TOOLTIP_TEXT), Resources.getString(Resources.BUTTON_ICON), Resources.getString(Resources.HOTKEY_LABEL), Resources.getString("Editor.report_format"), Resources.getString("Editor.LosThread.persistence"), Resources.getString("Editor.LosThread.icon_persist"), Resources.getString("Editor.LosThread.visible"), Resources.getString("Editor.LosThread.start_grid"), Resources.getString("Editor.LosThread.end_grid"), Resources.getString("Editor.LosThread.draw_range"), Resources.getString("Editor.LosThread.pixel_range"), Resources.getString("Editor.LosThread.round_fractions"), Resources.getString("Editor.LosThread.hidden"), Resources.getString("Editor.LosThread.opacity"), Resources.getString(Resources.COLOR_LABEL)};
			}
			
		}
		
		public const System.String LOS_THREAD_COMMAND = "LOS\t";
		
		public const System.String NAME = "threadName";
		public const System.String SNAP_LOS = "snapLOS";
		public const System.String SNAP_START = "snapStart";
		public const System.String SNAP_END = "snapEnd";
		public const System.String REPORT = "report";
		public const System.String PERSISTENCE = "persistence";
		public const System.String PERSISTENT_ICON_NAME = "persistentIconName";
		public const System.String GLOBAL = "global";
		public const System.String LOS_COLOR = "threadColor";
		public const System.String HOTKEY = "hotkey";
		public const System.String TOOLTIP = "tooltip";
		public const System.String ICON_NAME = "iconName";
		public const System.String LABEL = "label";
		public const System.String DRAW_RANGE = "drawRange";
		public const System.String HIDE_COUNTERS = "hideCounters";
		public const System.String HIDE_OPACITY = "hideOpacity";
		public const System.String RANGE_BACKGROUND = "rangeBg";
		public const System.String RANGE_FOREGROUND = "rangeFg";
		public const System.String RANGE_SCALE = "scale";
		public const System.String RANGE_ROUNDING = "round";
		public const System.String ROUND_UP = "Up";
		public const System.String ROUND_DOWN = "Down";
		public const System.String ROUND_OFF = "Nearest whole number";
		//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
		public static System.Drawing.Font RANGE_FONT = new System.Drawing.Font("Dialog", 11, System.Drawing.FontStyle.Regular);
		public const System.String DEFAULT_ICON = "/images/thread.gif";
		
		public const System.String FROM_LOCATION = "FromLocation";
		public const System.String TO_LOCATION = "ToLocation";
		public const System.String CHECK_COUNT = "NumberOfLocationsChecked";
		public const System.String CHECK_LIST = "AllLocationsChecked";
		public const System.String RANGE = "Range";
		
		public const System.String NEVER = "Never";
		public const System.String ALWAYS = "Always";
		public const System.String CTRL_CLICK = "Ctrl-Click & Drag";
		public const System.String WHEN_PERSISTENT = "When Persisting";
		
		protected internal static UniqueIdManager idMgr = new UniqueIdManager("LOS_Thread");
		
		protected internal bool retainAfterRelease = false;
		protected internal long lastRelease = 0;
		
		protected internal Map map;
		protected internal LaunchButton launch_Renamed_Field;
		protected internal System.Windows.Forms.KeyEventArgs hotkey;
		protected internal System.Drawing.Point anchor;
		protected internal System.Drawing.Point arrow;
		protected internal bool visible;
		protected internal bool drawRange_Renamed_Field;
		protected internal int rangeScale;
		protected internal double rangeRounding = 0.5;
		protected internal bool hideCounters;
		protected internal int hideOpacity = 0;
		protected internal System.String fixedColor;
		protected internal System.Drawing.Color threadColor = System.Drawing.Color.Black, rangeFg = System.Drawing.Color.White, rangeBg = System.Drawing.Color.Black;
		protected internal bool snapStart;
		protected internal bool snapEnd;
		protected internal System.Drawing.Point lastAnchor = new System.Drawing.Point(0, 0);
		protected internal System.Drawing.Point lastArrow = new System.Drawing.Point(0, 0);
		protected internal System.Drawing.Rectangle lastRangeRect = new System.Drawing.Rectangle();
		protected internal System.String anchorLocation = "";
		protected internal System.String lastLocation = "";
		protected internal System.String lastRange = "";
		protected internal FormattedString reportFormat = new FormattedString("$playerId$ Checked LOS from $" + FROM_LOCATION + "$ to $" + CHECK_LIST + "$");
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < String > checkList = new ArrayList < String >();
		protected internal System.String persistence = CTRL_CLICK;
		protected internal System.String persistentIconName;
		protected internal System.String global = ALWAYS;
		protected internal System.String threadId = "";
		protected internal bool persisting = false;
		protected internal bool mirroring = false;
		protected internal System.String iconName;
		protected internal bool ctrlWhenClick = false;
		protected internal bool initializing;
		
		public LOS_Thread()
		{
			InitBlock();
			anchor = new System.Drawing.Point(0, 0);
			arrow = new System.Drawing.Point(0, 0);
			visible = false;
			persisting = false;
			mirroring = false;
			//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			ActionListener al = new AnonymousClassActionListener(this);
			launch_Renamed_Field = new LaunchButton("Thread", TOOLTIP, LABEL, HOTKEY, ICON_NAME, al);
			launch_Renamed_Field.setAttribute(ICON_NAME, DEFAULT_ICON);
			launch_Renamed_Field.setAttribute(TOOLTIP, "Show LOS Thread");
		}
		
		/// <summary> Expects to be added to a {@link Map}.  Adds a button to the map
		/// window's toolbar.  Pushing the button pushes a MouseListener
		/// onto the Map that draws the thread.  Adds some entries to
		/// preferences
		/// 
		/// </summary>
		/// <seealso cref="Map.pushMouseListener">
		/// </seealso>
		public override void  addTo(Buildable b)
		{
			idMgr.add(this);
			map = (Map) b;
			map.getView().addMouseMotionListener(this);
			map.addDrawComponent(this);
			System.Windows.Forms.ToolBarButton temp_ToolBarButton;
			temp_ToolBarButton = new System.Windows.Forms.ToolBarButton(launch_Renamed_Field.Text);
			temp_ToolBarButton.ToolTipText = SupportClass.ToolTipSupport.getToolTipText(launch_Renamed_Field);
			map.ToolBar.Buttons.Add(temp_ToolBarButton);
			if (launch_Renamed_Field.Image != null)
			{
				map.ToolBar.ImageList.Images.Add(launch_Renamed_Field.Image);
				temp_ToolBarButton.ImageIndex = map.ToolBar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton.Tag = launch_Renamed_Field;
			launch_Renamed_Field.Tag = temp_ToolBarButton;
			GameModule.getGameModule().addCommandEncoder(this);
			GameModule.getGameModule().getPrefs().addOption(getConfigureName(), new BooleanConfigurer(SNAP_LOS, Resources.getString("LOS_Thread.snap_thread_preference")));
			
			if (fixedColor == null)
			{
				ColorConfigurer config = new ColorConfigurer(LOS_COLOR, Resources.getString("LOS_Thread.thread_color_preference"));
				GameModule.getGameModule().getPrefs().addOption(getConfigureName(), config);
				threadColor = (System.Drawing.Color) GameModule.getGameModule().getPrefs().getValue(LOS_COLOR);
				config.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				config.fireUpdate();
			}
		}
		
		public override void  removeFrom(Buildable b)
		{
			map = (Map) b;
			map.removeDrawComponent(this);
			map.ToolBar.Buttons.Remove((System.Windows.Forms.ToolBarButton) launch_Renamed_Field.Tag);
			GameModule.getGameModule().removeCommandEncoder(this);
			idMgr.remove(this);
		}
		
		public override void  setAttribute(System.String key, System.Object value_Renamed)
		{
			if (DRAW_RANGE.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.lang.Boolean.valueOf' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
					value_Renamed = System.Boolean.Parse((System.String) value_Renamed);
				}
				drawRange_Renamed_Field = ((System.Boolean) value_Renamed);
			}
			else if (NAME.Equals(key))
			{
				setConfigureName((System.String) value_Renamed);
			}
			else if (RANGE_SCALE.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					value_Renamed = System.Int32.Parse((System.String) value_Renamed);
				}
				rangeScale = ((System.Int32) value_Renamed);
			}
			else if (RANGE_ROUNDING.Equals(key))
			{
				if (ROUND_UP.Equals(value_Renamed))
				{
					rangeRounding = 1.0;
				}
				else if (ROUND_DOWN.Equals(value_Renamed))
				{
					rangeRounding = 0.0;
				}
				else
				{
					rangeRounding = 0.5;
				}
			}
			else if (HIDE_COUNTERS.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.lang.Boolean.valueOf' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
					value_Renamed = System.Boolean.Parse((System.String) value_Renamed);
				}
				hideCounters = ((System.Boolean) value_Renamed);
			}
			else if (HIDE_OPACITY.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					value_Renamed = System.Int32.Parse((System.String) value_Renamed);
				}
				Transparency = ((System.Int32) value_Renamed);
			}
			else if (RANGE_FOREGROUND.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					value_Renamed = ColorConfigurer.stringToColor((System.String) value_Renamed);
				}
				rangeFg = (System.Drawing.Color) value_Renamed;
			}
			else if (RANGE_BACKGROUND.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					value_Renamed = ColorConfigurer.stringToColor((System.String) value_Renamed);
				}
				rangeBg = (System.Drawing.Color) value_Renamed;
			}
			else if (LOS_COLOR.Equals(key))
			{
				if (value_Renamed is System.Drawing.Color)
				{
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					value_Renamed = ColorConfigurer.colorToString(ref new System.Drawing.Color[]{(System.Drawing.Color) value_Renamed}[0]);
				}
				fixedColor = ((System.String) value_Renamed);
				threadColor = ColorConfigurer.stringToColor(fixedColor);
			}
			else if (SNAP_START.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.lang.Boolean.valueOf' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
					value_Renamed = System.Boolean.Parse((System.String) value_Renamed);
				}
				snapStart = ((System.Boolean) value_Renamed);
			}
			else if (SNAP_END.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.lang.Boolean.valueOf' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
					value_Renamed = System.Boolean.Parse((System.String) value_Renamed);
				}
				snapEnd = ((System.Boolean) value_Renamed);
			}
			else if (REPORT.Equals(key))
			{
				reportFormat.Format = ((System.String) value_Renamed);
			}
			else if (PERSISTENCE.Equals(key))
			{
				persistence = ((System.String) value_Renamed);
			}
			else if (PERSISTENT_ICON_NAME.Equals(key))
			{
				persistentIconName = ((System.String) value_Renamed);
			}
			else if (GLOBAL.Equals(key))
			{
				global = ((System.String) value_Renamed);
			}
			else if (ICON_NAME.Equals(key))
			{
				iconName = ((System.String) value_Renamed);
				launch_Renamed_Field.setAttribute(ICON_NAME, iconName);
			}
			else
			{
				launch_Renamed_Field.setAttribute(key, value_Renamed);
			}
		}
		
		public override System.String getAttributeValueString(System.String key)
		{
			if (DRAW_RANGE.Equals(key))
			{
				return System.Convert.ToString(drawRange_Renamed_Field);
			}
			else if (NAME.Equals(key))
			{
				return getConfigureName();
			}
			else if (RANGE_SCALE.Equals(key))
			{
				return System.Convert.ToString(rangeScale);
			}
			else if (RANGE_ROUNDING.Equals(key))
			{
				if (rangeRounding == 1.0)
				{
					return ROUND_UP;
				}
				else if (rangeRounding == 0.0)
				{
					return ROUND_DOWN;
				}
				else
				{
					return ROUND_OFF;
				}
			}
			else if (HIDE_COUNTERS.Equals(key))
			{
				return System.Convert.ToString(hideCounters);
			}
			else if (HIDE_OPACITY.Equals(key))
			{
				return System.Convert.ToString(hideOpacity);
			}
			else if (RANGE_FOREGROUND.Equals(key))
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return ColorConfigurer.colorToString(ref rangeFg);
			}
			else if (RANGE_BACKGROUND.Equals(key))
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return ColorConfigurer.colorToString(ref rangeBg);
			}
			else if (LOS_COLOR.Equals(key))
			{
				return fixedColor;
			}
			else if (SNAP_START.Equals(key))
			{
				return System.Convert.ToString(snapStart);
			}
			else if (SNAP_END.Equals(key))
			{
				return System.Convert.ToString(snapEnd);
			}
			else if (REPORT.Equals(key))
			{
				return reportFormat.Format;
			}
			else if (PERSISTENCE.Equals(key))
			{
				return persistence;
			}
			else if (PERSISTENT_ICON_NAME.Equals(key))
			{
				return persistentIconName;
			}
			else if (GLOBAL.Equals(key))
			{
				return global;
			}
			else if (ICON_NAME.Equals(key))
			{
				return iconName;
			}
			else
			{
				return launch_Renamed_Field.getAttributeValueString(key);
			}
		}
		
		public virtual void  setup(bool show)
		{
			launch_Renamed_Field.Enabled = show;
		}
		
		public virtual void  draw(System.Drawing.Graphics g, Map m)
		{
			if (initializing || !visible)
			{
				return ;
			}
			SupportClass.GraphicsManager.manager.SetColor(g, threadColor);
			System.Drawing.Point mapAnchor = map.componentCoordinates(anchor);
			System.Drawing.Point mapArrow = map.componentCoordinates(arrow);
			g.DrawLine(SupportClass.GraphicsManager.manager.GetPen(g), mapAnchor.X, mapAnchor.Y, mapArrow.X, mapArrow.Y);
			Board b;
			
			if (drawRange_Renamed_Field)
			{
				if (rangeScale > 0)
				{
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int dist = (int) (rangeRounding + SupportClass.PointFSupport.Distance(new System.Drawing.Point(new System.Drawing.Size(anchor)), new System.Drawing.Point(new System.Drawing.Size(arrow))) / rangeScale);
					drawRange(g, dist);
				}
				else
				{
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					b = map.findBoard(ref anchor);
					MapGrid grid = null;
					if (b != null)
					{
						grid = b.getGrid();
					}
					if (grid != null && grid is ZonedGrid)
					{
						System.Drawing.Point bp = new System.Drawing.Point(new System.Drawing.Size(anchor));
						bp.translate(- b.bounds().x, - b.bounds().y);
						Zone z = ((ZonedGrid) b.getGrid()).findZone(bp);
						if (z != null)
						{
							grid = z.getGrid();
						}
					}
					if (grid != null)
					{
						//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
						drawRange(g, grid.range(ref anchor, ref arrow));
					}
				}
			}
			lastAnchor = anchor;
			lastArrow = arrow;
		}
		
		public virtual bool drawAboveCounters()
		{
			return true;
		}
		
		protected internal virtual void  launch()
		{
			if (!visible)
			{
				map.pushMouseListener(this);
				if (hideCounters)
				{
					map.setPieceOpacity(hideOpacity / 100.0f);
					map.repaint();
				}
				visible = true;
				anchor = new System.Drawing.Point(0, 0);
				arrow = new System.Drawing.Point(0, 0);
				retainAfterRelease = false;
				initializing = true;
			}
			else if (persisting)
			{
				Persisting = false;
			}
		}
		
		/// <summary>Since we register ourselves as a MouseListener using {@link
		/// Map#pushMouseListener}, these mouse events are received in map
		/// coordinates 
		/// </summary>
		public virtual void  mouseEntered(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseExited(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseClicked(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			initializing = false;
			if (visible && !persisting && !mirroring)
			{
				System.Drawing.Point p = new System.Drawing.Point(e.X, e.Y);
				if (true.Equals(GameModule.getGameModule().getPrefs().getValue(SNAP_LOS)) || snapStart)
				{
					p = map.snapTo(p);
				}
				anchor = p;
				anchorLocation = map.localizedLocationName(anchor);
				lastLocation = anchorLocation;
				lastRange = "";
				checkList.clear();
				ctrlWhenClick = (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control);
			}
		}
		
		public virtual void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (!persisting && !mirroring)
			{
				if (retainAfterRelease && !(ctrlWhenClick && persistence.Equals(CTRL_CLICK)))
				{
					retainAfterRelease = false;
					if (global.Equals(ALWAYS))
					{
						System.Drawing.Point tempAux = Anchor;
						//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
						System.Drawing.Point tempAux2 = Arrow;
						Command com = new LOSCommand(this, ref tempAux, ref tempAux2, false, true);
						GameModule.getGameModule().sendAndLog(com);
					}
				}
				else
				{
					//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.getWhen' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
					if (e.getWhen() != lastRelease)
					{
						visible = false;
						if (global.Equals(ALWAYS) || global.Equals(WHEN_PERSISTENT))
						{
							if (persistence.Equals(ALWAYS) || (ctrlWhenClick && persistence.Equals(CTRL_CLICK)))
							{
								anchor = lastAnchor;
								System.Drawing.Point tempAux3 = Anchor;
								//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
								System.Drawing.Point tempAux4 = Arrow;
								Command com = new LOSCommand(this, ref tempAux3, ref tempAux4, true, false);
								GameModule.getGameModule().sendAndLog(com);
								Persisting = true;
							}
							else
							{
								System.Drawing.Point tempAux5 = Anchor;
								//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
								System.Drawing.Point tempAux6 = Arrow;
								Command com = new LOSCommand(this, ref tempAux5, ref tempAux6, false, false);
								GameModule.getGameModule().sendAndLog(com);
							}
						}
						map.setPieceOpacity(1.0f);
						map.popMouseListener();
						map.repaint();
					}
				}
				//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.getWhen' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
				lastRelease = e.getWhen();
				
				if (LosCheckCount > 0)
				{
					reportFormat.setProperty(FROM_LOCATION, anchorLocation);
					reportFormat.setProperty(TO_LOCATION, lastLocation);
					reportFormat.setProperty(RANGE, lastRange);
					reportFormat.setProperty(CHECK_COUNT, System.Convert.ToString(LosCheckCount));
					reportFormat.setProperty(CHECK_LIST, LosCheckList);
					
					GameModule.getGameModule().getChatter().send(reportFormat.getLocalizedText());
				}
			}
			ctrlWhenClick = false;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual void  setEndPoints(ref System.Drawing.Point newAnchor, ref System.Drawing.Point newArrow)
		{
			anchor.X = newAnchor.X;
			anchor.Y = newAnchor.Y;
			arrow.X = newArrow.X;
			arrow.Y = newArrow.Y;
			map.repaint();
		}
		
		/// <summary>Since we register ourselves as a MouseMotionListener directly,
		/// these mouse events are received in component
		/// coordinates 
		/// </summary>
		public virtual void  mouseMoved(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
		}
		
		public virtual void  mouseDragged(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (visible && !persisting && !mirroring)
			{
				retainAfterRelease = true;
				
				System.Drawing.Point p = new System.Drawing.Point(e.X, e.Y);
				
				map.scrollAtEdge(p, 15);
				
				if (true.Equals(GameModule.getGameModule().getPrefs().getValue(SNAP_LOS)) || snapEnd)
				{
					p = map.componentCoordinates(map.snapTo(map.mapCoordinates(p)));
				}
				arrow = map.mapCoordinates(p);
				
				System.String location = map.localizedLocationName(arrow);
				if (!checkList.contains(location) && !location.Equals(anchorLocation))
				{
					checkList.add(location);
					lastLocation = location;
				}
				
				System.Drawing.Point mapAnchor = lastAnchor;
				System.Drawing.Point mapArrow = lastArrow;
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				int fudge = (int) (1.0 / map.Zoom * 2);
				System.Drawing.Rectangle r = new System.Drawing.Rectangle(System.Math.Min(mapAnchor.X, mapArrow.X) - fudge, System.Math.Min(mapAnchor.Y, mapArrow.Y) - fudge, System.Math.Abs(mapAnchor.X - mapArrow.X) + 1 + fudge * 2, System.Math.Abs(mapAnchor.Y - mapArrow.Y) + 1 + fudge * 2);
				map.repaint(r);
				
				if (drawRange_Renamed_Field)
				{
					System.Drawing.Rectangle temp_Rectangle;
					temp_Rectangle = lastRangeRect;
					r = new System.Drawing.Rectangle(temp_Rectangle.Location, temp_Rectangle.Size);
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					r.Width += (int) (r.Width / map.Zoom) + 1;
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					r.Height += (int) (r.Height / map.Zoom) + 1;
					map.repaint(r);
				}
			}
		}
		
		/// <summary> Writes text showing the range
		/// 
		/// </summary>
		/// <param name="range">the range to display, in whatever units returned
		/// by the {@link MapGrid} containing the thread 
		/// </param>
		public virtual void  drawRange(System.Drawing.Graphics g, int range)
		{
			System.Drawing.Point mapArrow = map.componentCoordinates(arrow);
			System.Drawing.Point mapAnchor = map.componentCoordinates(anchor);
			SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.Black);
			SupportClass.GraphicsManager.manager.SetFont(g, RANGE_FONT);
			//UPGRADE_NOTE: Final was removed from the declaration of 'fm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Font fm = SupportClass.GraphicsManager.manager.GetFont(g);
			//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder buffer = new StringBuilder();
			int dummy = range;
			while (dummy >= 1)
			{
				dummy = dummy / 10;
				buffer.append("8");
			}
			if (buffer.length() == 0)
			{
				buffer.append("8");
			}
			System.String rangeMess = Resources.getString("LOS_Thread.range");
			//UPGRADE_ISSUE: Method 'java.awt.FontMetrics.stringWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFontMetricsstringWidth_javalangString'"
			int wid = fm.stringWidth(" " + rangeMess + "  " + buffer.toString());
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.FontMetrics.getAscent' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			int hgt = SupportClass.GetAscent(fm) + 2;
			int w = mapArrow.X - mapAnchor.X;
			int h = mapArrow.Y - mapAnchor.Y;
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int x0 = mapArrow.X + (int) ((wid / 2 + 20) * w / System.Math.Sqrt(w * w + h * h));
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int y0 = mapArrow.Y + (int) ((hgt / 2 + 20) * h / System.Math.Sqrt(w * w + h * h));
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.FontMetrics.getAscent' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), x0 - wid / 2, y0 + hgt / 2 - SupportClass.GetAscent(fm), wid, hgt);
			SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.White);
			//UPGRADE_TODO: Method 'java.awt.Graphics.drawString' was converted to 'System.Drawing.Graphics.DrawString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphicsdrawString_javalangString_int_int'"
			//UPGRADE_ISSUE: Method 'java.awt.FontMetrics.stringWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFontMetricsstringWidth_javalangString'"
			g.DrawString(rangeMess + " " + range, SupportClass.GraphicsManager.manager.GetFont(g), SupportClass.GraphicsManager.manager.GetBrush(g), x0 - wid / 2 + fm.stringWidth(" "), y0 + hgt / 2 - SupportClass.GraphicsManager.manager.GetFont(g).GetHeight());
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.FontMetrics.getAscent' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			lastRangeRect = new System.Drawing.Rectangle(x0 - wid / 2, y0 + hgt / 2 - SupportClass.GetAscent(fm), wid + 1, hgt + 1);
			System.Drawing.Point np = map.mapCoordinates(new System.Drawing.Point(lastRangeRect.X, lastRangeRect.Y));
			lastRangeRect.X = np.X;
			lastRangeRect.Y = np.Y;
			lastRange = System.Convert.ToString(range);
		}
		
		public override VassalSharp.build.module.documentation.HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("Map.htm", "LOS");
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		String.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		String.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		IconConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		NamedKeyStroke.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		ReportFormatConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		PersistenceOptions.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		IconConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		GlobalOptions.
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
		Integer.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		RoundingOptions.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Integer.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Color.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	public class IconConfig : ConfigurerFactory
	{
		public virtual Configurer getConfigurer(AutoConfigurable c, System.String key, System.String name)
		{
			return new IconConfigurer(key, name, DEFAULT_ICON);
		}
	}
	
	public class ReportFormatConfig : TranslatableConfigurerFactory
	{
		public virtual Configurer getConfigurer(AutoConfigurable c, System.String key, System.String name)
		{
			return new PlayerIdFormattedStringConfigurer(key, name, new System.String[]{FROM_LOCATION, TO_LOCATION, RANGE, CHECK_COUNT, CHECK_LIST});
		}
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public VisibilityCondition getAttributeVisibility(String name)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		VisibilityCondition cond = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(RANGE_SCALE.equals(name) 
	|| RANGE_ROUNDING.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		cond = new VisibilityCondition()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean shouldBeVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return drawRange;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(HIDE_OPACITY.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		cond = new VisibilityCondition()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean shouldBeVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return hideCounters;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(PERSISTENT_ICON_NAME.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		cond = new VisibilityCondition()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean shouldBeVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return persistence.equals(CTRL_CLICK) || persistence.equals(ALWAYS);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	return cond;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	public class RoundingOptions:StringEnum
	{
		public override System.String[] getValidValues(AutoConfigurable target)
		{
			return new System.String[]{ROUND_UP, ROUND_DOWN, ROUND_OFF};
		}
	}
	
	public class PersistenceOptions:StringEnum
	{
		public override System.String[] getValidValues(AutoConfigurable target)
		{
			return new System.String[]{CTRL_CLICK, NEVER, ALWAYS};
		}
	}
	
	public class GlobalOptions:StringEnum
	{
		public override System.String[] getValidValues(AutoConfigurable target)
		{
			return new System.String[]{WHEN_PERSISTENT, NEVER, ALWAYS};
		}
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Configurable [] getConfigureComponents()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new Configurable [0];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Class < ? > [] getAllowableConfigureComponents()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new Class [0];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Command decode(String command)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		SequenceEncoder.Decoder sd = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(command.startsWith(LOS_THREAD_COMMAND + getId()))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		sd = new SequenceEncoder.Decoder(command, \t);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	sd.nextToken();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	sd.nextToken();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Point anchor = new Point(sd.nextInt(0), sd.nextInt(0));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Point arrow = new Point(sd.nextInt(0), sd.nextInt(0));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	boolean persisting = sd.nextBoolean(false);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	boolean mirroring = sd.nextBoolean(false);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return new LOSCommand(this, anchor, arrow, persisting, mirroring);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String encode(Command c)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(c instanceof LOSCommand)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		LOSCommand com =(LOSCommand) c;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	SequenceEncoder se = new SequenceEncoder(com.target.getId(), \t);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	se.append(com.newAnchor.x).append(com.newAnchor.y)
	.append(com.newArrow.x).append(com.newArrow.y)
	.append(com.newPersisting).append(com.newMirroring);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return LOS_THREAD_COMMAND + se.getValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	public class LOSCommand:Command
	{
		protected internal LOS_Thread target;
		protected internal System.String oldState;
		protected internal System.Drawing.Point newAnchor, oldAnchor;
		protected internal System.Drawing.Point newArrow, oldArrow;
		protected internal bool newPersisting, oldPersisting;
		protected internal bool newMirroring, oldMirroring;
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public LOSCommand(LOS_Thread oTarget, ref System.Drawing.Point anchor, ref System.Drawing.Point arrow, bool persisting, bool mirroring)
		{
			target = oTarget;
			oldAnchor = target.Anchor;
			oldArrow = target.Arrow;
			oldPersisting = target.Persisting;
			oldMirroring = target.Mirroring;
			newAnchor = anchor;
			newArrow = arrow;
			newPersisting = persisting;
			newMirroring = mirroring;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override void  executeCommand()
		{
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			target.setEndPoints(ref newAnchor, ref newArrow);
			target.Persisting = newPersisting;
			target.Mirroring = newMirroring;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override Command myUndoCommand()
		{
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			return new LOSCommand(target, ref oldAnchor, ref oldArrow, oldPersisting, oldMirroring);
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}