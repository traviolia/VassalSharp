/*
* $Id$
*
* Copyright (c) 2000-2007 by Rodney Kinney, Joel Uckelman
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
using AutoConfigurable = VassalSharp.build.AutoConfigurable;
using Buildable = VassalSharp.build.Buildable;
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
using GameComponent = VassalSharp.build.module.GameComponent;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using AutoConfigurer = VassalSharp.configure.AutoConfigurer;
using ColorConfigurer = VassalSharp.configure.ColorConfigurer;
using Configurer = VassalSharp.configure.Configurer;
using ConfigurerFactory = VassalSharp.configure.ConfigurerFactory;
using IconConfigurer = VassalSharp.configure.IconConfigurer;
using VisibilityCondition = VassalSharp.configure.VisibilityCondition;
using GamePiece = VassalSharp.counters.GamePiece;
using ComponentI18nData = VassalSharp.i18n.ComponentI18nData;
using Resources = VassalSharp.i18n.Resources;
using Translatable = VassalSharp.i18n.Translatable;
using KeyStrokeSource = VassalSharp.tools.KeyStrokeSource;
using LaunchButton = VassalSharp.tools.LaunchButton;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using ScrollPane = VassalSharp.tools.ScrollPane;
namespace VassalSharp.build.module.map
{
	
	/// <summary> This is scaled version of a {@link Map} that gives an overview.
	/// Users can navigate around the Map by clicking on the GlobalMap, which
	/// draws a rectangular region of interest (ROI) indicating the current
	/// viewable area in the map window.
	/// </summary>
	public class GlobalMap : AutoConfigurable, GameComponent, Drawable
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(GlobalMap enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(GlobalMap enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GlobalMap enclosingInstance;
			public GlobalMap Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				Enclosing_Instance.scroll.Visible = !Enclosing_Instance.scroll.Visible;
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
		virtual public System.String[] AttributeNames
		{
			get
			{
				return new System.String[]{TOOLTIP, BUTTON_TEXT, ICON_NAME, HOTKEY, SCALE, COLOR};
			}
			
		}
		virtual public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[]{Resources.getString(Resources.TOOLTIP_TEXT), Resources.getString(Resources.BUTTON_TEXT), Resources.getString(Resources.BUTTON_ICON), Resources.getString("Editor.GlobalMap.show_hide"), Resources.getString("Editor.GlobalMap.scale_factor"), Resources.getString("Editor.GlobalMap.hilight")};
			}
			
		}
		private const long serialVersionUID = 2L;
		
		protected internal Map map;
		protected internal double scale = 0.19444444; // Zoom factor
		protected internal System.Drawing.Color rectColor = System.Drawing.Color.Black;
		//UPGRADE_NOTE: Final was removed from the declaration of 'launch '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal LaunchButton launch;
		
		protected internal CounterDetailViewer mouseOverViewer;
		//UPGRADE_NOTE: Final was removed from the declaration of 'scroll '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal ScrollPane scroll;
		//UPGRADE_NOTE: Final was removed from the declaration of 'view '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal View view;
		protected internal ComponentI18nData myI18nData;
		
		public GlobalMap()
		{
			InitBlock();
			view = new View();
			view.addMouseListener(view);
			
			scroll = new GlobalMapScrollPane(view);
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			//UPGRADE_TODO: Field 'javax.swing.border.EtchedBorder.RAISED' was converted to 'System.Windows.Forms.Border3DStyle.Raised' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingborderEtchedBorderRAISED_f'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(scroll.CreateGraphics(), 0, 0, scroll.Width, scroll.Height, (System.Windows.Forms.Border3DStyle.Etched & (System.Windows.Forms.Border3DStyle) System.Windows.Forms.Border3DStyle.Bump));
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentX' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentX_float'"
			scroll.setAlignmentX(0.0f);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentY_float'"
			scroll.setAlignmentY(0.0f);
			
			//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			ActionListener al = new AnonymousClassActionListener(this);
			
			launch = new LaunchButton(null, TOOLTIP, BUTTON_TEXT, HOTKEY, ICON_NAME, al);
			launch.setAttribute(TOOLTIP, "Show/Hide overview window");
			launch.setAttribute(HOTKEY, NamedKeyStroke.getNamedKeyStroke((int) System.Windows.Forms.Keys.O, (int) System.Windows.Forms.Keys.Control + (int) System.Windows.Forms.Keys.Shift));
		}
		
		/// <summary> Expects to be added to a {@link Map}. Adds itself as a {@link
		/// GameComponent} and a {@link Drawable}component
		/// </summary>
		public virtual void  addTo(Buildable b)
		{
			map = (Map) b;
			
			mouseOverViewer = new CounterViewer();
			
			GameModule.getGameModule().getGameState().addGameComponent(this);
			
			GameModule.getGameModule().addKeyStrokeSource(new KeyStrokeSource(view, 0));
			
			map.addDrawComponent(this);
			System.Windows.Forms.ToolBarButton temp_ToolBarButton;
			temp_ToolBarButton = new System.Windows.Forms.ToolBarButton(launch.Text);
			temp_ToolBarButton.ToolTipText = SupportClass.ToolTipSupport.getToolTipText(launch);
			map.ToolBar.Buttons.Add(temp_ToolBarButton);
			if (launch.Image != null)
			{
				map.ToolBar.ImageList.Images.Add(launch.Image);
				temp_ToolBarButton.ImageIndex = map.ToolBar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton.Tag = launch;
			launch.Tag = temp_ToolBarButton;
			
			if (b is Translatable)
			{
				I18nData.OwningComponent = (Translatable) b;
			}
			
			map.getLayeredPane().add(scroll, 100);
		}
		
		public virtual void  add(Buildable b)
		{
		}
		
		public virtual void  remove(Buildable b)
		{
		}
		
		public virtual void  removeFrom(Buildable b)
		{
			map = (Map) b;
			map.removeDrawComponent(this);
			map.ToolBar.Buttons.Remove((System.Windows.Forms.ToolBarButton) launch.Tag);
			GameModule.getGameModule().getGameState().removeGameComponent(this);
			
			map.getLayeredPane().remove(scroll);
		}
		
		public virtual void  build(System.Xml.XmlElement e)
		{
			AutoConfigurable.Util.buildAttributes(e, this);
		}
		
		protected internal const System.String SCALE = "scale";
		protected internal const System.String COLOR = "color";
		protected internal const System.String HOTKEY = "hotkey";
		protected internal const System.String ICON_NAME = "icon";
		protected internal const System.String TOOLTIP = "tooltip";
		protected internal const System.String BUTTON_TEXT = "buttonText";
		protected internal const System.String DEFAULT_ICON = "/images/overview.gif";
		
		public virtual VisibilityCondition getAttributeVisibility(System.String name)
		{
			return null;
		}
		
		public virtual void  setAttribute(System.String key, System.Object value_Renamed)
		{
			if (SCALE.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					value_Renamed = System.Double.Parse((System.String) value_Renamed);
				}
				scale = ((System.Double) value_Renamed);
			}
			else if (COLOR.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					value_Renamed = ColorConfigurer.stringToColor((System.String) value_Renamed);
				}
				rectColor = (System.Drawing.Color) value_Renamed;
			}
			else
			{
				launch.setAttribute(key, value_Renamed);
			}
		}
		
		public virtual System.String getAttributeValueString(System.String key)
		{
			if (SCALE.Equals(key))
			{
				return System.Convert.ToString(scale);
			}
			else if (COLOR.Equals(key))
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return ColorConfigurer.colorToString(ref rectColor);
			}
			else
			{
				return launch.getAttributeValueString(key);
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
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
		Double.
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
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void draw(Graphics g, Map m)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		view.repaint();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean drawAboveCounters()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return true;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Transform a point from Map coordinates to coordinates in the overview
	/// window
	/// 
	/// </summary>
	/// <param name="p">
	/// </param>
	/// <returns>
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Point componentCoordinates(Point p)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new Point((int)((p.x - map.getEdgeBuffer().width) * scale),
		(int)((p.y - map.getEdgeBuffer().height) * scale));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Transform a point from coordinates in the overview window to Map
	/// coordinates
	/// 
	/// </summary>
	/// <param name="p">
	/// </param>
	/// <returns>
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Point mapCoordinates(Point p)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new Point(
		(int) Math.round(p.x / scale) + map.getEdgeBuffer().width,
		(int) Math.round(p.y / scale) + map.getEdgeBuffer().height);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getToolTipText(MouseEvent e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Command getRestoreCommand()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setup(boolean show)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(show)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		scroll.setMaximumSize(scroll.getPreferredSize());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		scroll.setVisible(false);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	if(show && !map.getComponentsOf(CounterDetailViewer.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	class).isEmpty())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		view.addMouseListener(mouseOverViewer);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	view.addMouseMotionListener(mouseOverViewer);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	scroll.addKeyListener(mouseOverViewer);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		view.removeMouseListener(mouseOverViewer);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	view.removeMouseMotionListener(mouseOverViewer);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	scroll.removeKeyListener(mouseOverViewer);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public static String getConfigureTypeName()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return Resources.getString(Editor.GlobalMap.component_type); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getConfigureName()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Configurer getConfigurer()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new AutoConfigurer(this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
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
		return new Class < ? > [0];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void addPropertyChangeListener(java.beans.PropertyChangeListener l)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public HelpFile getHelpFile()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return HelpFile.getReferenceManualPage(Map.htm, OverviewWindow);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public org.w3c.dom.Element getBuildElement(org.w3c.dom.Document doc)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return AutoConfigurable.Util.getBuildElement(doc, this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	protected internal class CounterViewer:CounterDetailViewer
	{
		private void  InitBlock()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'oldPoint '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
			System.Drawing.Point oldPoint = currentMousePosition.getPoint();
			//UPGRADE_NOTE: Final was removed from the declaration of 'mapPoint '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Point mapPoint = this.map.componentCoordinates(mapCoordinates(oldPoint));
			
			//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.translatePoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventtranslatePoint_int_int'"
			currentMousePosition.translatePoint(mapPoint.X - oldPoint.X, mapPoint.Y - oldPoint.Y);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < GamePiece > l = super.getDisplayablePieces();
			//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.translatePoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventtranslatePoint_int_int'"
			currentMousePosition.translatePoint(oldPoint.X - mapPoint.X, oldPoint.Y - mapPoint.Y);
			return l;
		}
		override protected internal double Zoom
		{
			get
			{
				return scale;
			}
			
		}
		public CounterViewer()
		{
			InitBlock();
			this.map = this.map;
			this.view = this.view;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < GamePiece > getDisplayablePieces()
	}
	
	/// <summary> The scroll pane in which the map {@link View} is displayed.</summary>
	[Serializable]
	protected internal class GlobalMapScrollPane:ScrollPane
	{
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
		new virtual public System.Drawing.Rectangle Bounds
		{
			get
			{
				return base.Bounds;
			}
			
			set
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'availSize '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size availSize = map.getView().getParent().getSize();
				//UPGRADE_NOTE: Final was removed from the declaration of 'viewSize '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size viewSize = view.getPreferredSize();
				//UPGRADE_NOTE: Final was removed from the declaration of 'i '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Int32[] i = SupportClass.GetInsets(this);
				viewSize.Width += i[1] + i[2];
				viewSize.Height += i[0] + i[3];
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'hsbNeeded '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool hsbNeeded = availSize.Width < viewSize.Width;
				//UPGRADE_NOTE: Final was removed from the declaration of 'vsbNeeded '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool vsbNeeded = availSize.Height < viewSize.Height;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'realSize '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size realSize = new System.Drawing.Size(0, 0);
				
				if (availSize.Width < viewSize.Width)
				{
					realSize.Width = availSize.Width;
				}
				else if (vsbNeeded)
				{
					//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					//UPGRADE_ISSUE: Field 'javax.swing.JScrollPane.verticalScrollBar' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPaneverticalScrollBar_f'"
					realSize.Width = System.Math.Min(availSize.Width, viewSize.Width + verticalScrollBar.Size.Width);
				}
				else
				{
					realSize.Width = viewSize.Width;
				}
				
				if (availSize.Height < viewSize.Height)
				{
					realSize.Height = availSize.Height;
				}
				else if (hsbNeeded)
				{
					//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					//UPGRADE_ISSUE: Field 'javax.swing.JScrollPane.horizontalScrollBar' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanehorizontalScrollBar_f'"
					realSize.Height = System.Math.Min(availSize.Height, viewSize.Height + horizontalScrollBar.Size.Height);
				}
				else
				{
					realSize.Height = viewSize.Height;
				}
				
				base.SetBounds(0, 0, realSize.Width, realSize.Height);
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
		/// <summary> This funcion is overridden to make sure that the parent layout
		/// is redone when the GlobalMap is shown.
		/// </summary>
		new virtual public bool Visible
		{
			get
			{
				return base.Visible;
			}
			
			set
			{
				base.Visible = value;
				if (value)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_ISSUE: Interface 'java.awt.LayoutManager' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Method 'java.awt.Container.getLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainergetLayout'"
					LayoutManager l = Parent.getLayout();
					if (l is Map.InsetLayout)
					{
						//UPGRADE_ISSUE: Method 'java.awt.LayoutManager.layoutContainer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtLayoutManager'"
						l.layoutContainer(Parent);
					}
				}
			}
			
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.VERTICAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsVERTICAL_SCROLLBAR_AS_NEEDED_f'"
		//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.HORIZONTAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsHORIZONTAL_SCROLLBAR_AS_NEEDED_f'"
		public GlobalMapScrollPane(System.Windows.Forms.Control view):base(view, true, true)
		{
		}
		
		/// <returns> The display size of the entire zoomed overview map
		/// </returns>
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.JComponent.getPreferredSize' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public System.Drawing.Size getPreferredSize()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Size d = view.getPreferredSize();
			//UPGRADE_NOTE: Final was removed from the declaration of 'i '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Int32[] i = SupportClass.GetInsets(this);
			d.Width += i[1] + i[2];
			d.Height += i[0] + i[3];
			return d;
		}
		
		/// <returns> The maximum size of the zoomed overview map and scroll pane
		/// </returns>
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.JComponent.getMaximumSize' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public System.Drawing.Size getMaximumSize()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Size d = getPreferredSize();
			
			//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
			//UPGRADE_ISSUE: Field 'javax.swing.JScrollPane.verticalScrollBar' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPaneverticalScrollBar_f'"
			if (verticalScrollBar.Visible)
			{
				//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				//UPGRADE_ISSUE: Field 'javax.swing.JScrollPane.verticalScrollBar' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPaneverticalScrollBar_f'"
				d.Width += verticalScrollBar.Size.Width;
			}
			//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
			//UPGRADE_ISSUE: Field 'javax.swing.JScrollPane.horizontalScrollBar' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanehorizontalScrollBar_f'"
			if (horizontalScrollBar.Visible)
			{
				//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				//UPGRADE_ISSUE: Field 'javax.swing.JScrollPane.horizontalScrollBar' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanehorizontalScrollBar_f'"
				d.Height += horizontalScrollBar.Size.Height;
			}
			
			return d;
		}
	}
	
	/// <summary> The Map view that appears inside the ScrollPane</summary>
	[Serializable]
	protected internal class View:System.Windows.Forms.Panel
	{
		private const long serialVersionUID = 1L;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected override void  OnPaint(System.Windows.Forms.PaintEventArgs g_EventArg)
		{
			System.Drawing.Graphics g = null;
			if (g_EventArg != null)
				g = g_EventArg.Graphics;
			//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_float'"
			map.drawBoards(g, - (int) System.Math.Round((double) ((float) scale * map.getEdgeBuffer().width)), - (int) System.Math.Round((double) ((float) scale * map.getEdgeBuffer().height)), scale, this);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(GamePiece gp: map.getPieces())
			{
				System.Drawing.Point p = componentCoordinates(gp.getPosition());
				gp.draw(g, p.X, p.Y, this, scale);
			}
			
			mouseOverViewer.draw(g, map);
			
			// FIXME: use a Graphics2D for this
			// Draw a rectangle indicating the present viewing area
			g.setColor(rectColor);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle r = map.getView().getVisibleRect();
			//UPGRADE_NOTE: Final was removed from the declaration of 'ul '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Point ul = componentCoordinates(map.mapCoordinates(r.Location));
			//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int w = (int) (scale * r.Width / map.getZoom());
			//UPGRADE_NOTE: Final was removed from the declaration of 'h '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int h = (int) (scale * r.Height / map.getZoom());
			g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), ul.X, ul.Y, w, h);
			g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), ul.X - 1, ul.Y - 1, w + 2, h + 2);
		}
		
		public virtual void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
		}
		
		public virtual void  mouseEntered(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
		}
		
		public virtual void  mouseExited(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
		}
		
		public virtual void  mouseClicked(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
		}
		
		public virtual void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
			map.centerAt(mapCoordinates(e.getPoint()));
		}
		
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.JComponent.getPreferredSize' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public System.Drawing.Size getPreferredSize()
		{
			return new System.Drawing.Size((int) ((map.mapSize().width - 2 * map.getEdgeBuffer().width) * scale), (int) ((map.mapSize().height - 2 * map.getEdgeBuffer().height) * scale));
		}
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public ComponentI18nData getI18nData()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(myI18nData == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		myI18nData = new ComponentI18nData(this, GlobalMap);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return myI18nData;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}