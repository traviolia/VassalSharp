/*
* $Id$
*
* Copyright (c) 2000-2008 by Rodney Kinney, Joel Uckelman
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
//UPGRADE_TODO: The type 'javax.swing.JSpinner' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using JSpinner = javax.swing.JSpinner;
//UPGRADE_TODO: The type 'javax.swing.SpinnerNumberModel' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SpinnerNumberModel = javax.swing.SpinnerNumberModel;
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using AutoConfigurable = VassalSharp.build.AutoConfigurable;
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using GameComponent = VassalSharp.build.module.GameComponent;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using Configurer = VassalSharp.configure.Configurer;
using ConfigurerFactory = VassalSharp.configure.ConfigurerFactory;
using IconConfigurer = VassalSharp.configure.IconConfigurer;
using SingleChildInstance = VassalSharp.configure.SingleChildInstance;
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
using Resources = VassalSharp.i18n.Resources;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using LaunchButton = VassalSharp.tools.LaunchButton;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
namespace VassalSharp.build.module.map
{
	
	/// <summary> Controls the zooming in/out of a {@link Map} window.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	public class Zoomer:AbstractConfigurable, GameComponent
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(Zoomer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Zoomer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Zoomer enclosingInstance;
			public Zoomer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				zoomIn();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(Zoomer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Zoomer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Zoomer enclosingInstance;
			public Zoomer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				zoomOut();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(Zoomer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Zoomer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Zoomer enclosingInstance;
			public Zoomer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				if (Enclosing_Instance.zoomPickButton.Visible)
				{
					//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.show' was converted to 'System.Windows.Forms.ContextMenu.Show' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenushow_javaawtComponent_int_int'"
					Enclosing_Instance.zoomMenu.Show(Enclosing_Instance.zoomPickButton, new System.Drawing.Point(0, Enclosing_Instance.zoomPickButton.Height));
				}
			}
		}
		private void  InitBlock()
		{
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > []
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				null, // ZOOM_START is handled by the LevelConfigurer
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				LevelConfig.
			}
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return Resources.getString("Editor.Zoom.component_type"); //$NON-NLS-1$
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[]{ZOOM_START, ZOOM_LEVELS, IN_TOOLTIP, IN_BUTTON_TEXT, IN_ICON_NAME, ZOOM_IN, PICK_TOOLTIP, PICK_BUTTON_TEXT, PICK_ICON_NAME, ZOOM_PICK, OUT_TOOLTIP, OUT_BUTTON_TEXT, OUT_ICON_NAME, ZOOM_OUT};
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[]{"", Resources.getString("Editor.Zoom.preset"), Resources.getString("Editor.Zoom.in_tooltip"), Resources.getString("Editor.Zoom.in_button"), Resources.getString("Editor.Zoom.in_icon"), Resources.getString("Editor.Zoom.in_key"), Resources.getString("Editor.Zoom.select_tooltip"), Resources.getString("Editor.Zoom.select_button"), Resources.getString("Editor.Zoom.select_icon"), Resources.getString("Editor.Zoom.select_key"), Resources.getString("Editor.Zoom.out_tooltip"), Resources.getString("Editor.Zoom.out_button"), Resources.getString("Editor.Zoom.out_icon"), Resources.getString("Editor.Zoom.out_key")};
			}
			
		}
		protected internal Map map;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		protected internal double zoom = 1.0;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		protected internal int zoomLevel = 0;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		protected internal int zoomStart = 1;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		protected internal double[] zoomFactor;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		protected internal int maxZoom = 4;
		
		protected internal LaunchButton zoomInButton;
		protected internal LaunchButton zoomPickButton;
		protected internal LaunchButton zoomOutButton;
		protected internal ZoomMenu zoomMenu;
		
		protected internal State state;
		
		// the default zoom levels are powers of 1.6
		//UPGRADE_NOTE: Final was removed from the declaration of 'defaultZoomLevels '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal static readonly double[] defaultZoomLevels = new double[]{1.0 / 1.6 / 1.6, 1.0 / 1.6, 1.0, 1.6};
		
		protected internal const int defaultInitialZoomLevel = 2;
		
		/// <summary> Stores the state information for the {@link Zoomer}. This class
		/// exists to keep the <code>Zoomer</code> data separate from the
		/// <code>Zoomer</code> GUI.
		/// 
		/// <p>Predefined zoom levels are stored in <code>levels</code>. If we are
		/// in a predefined zoom level, then <code>custom == -1</code> and
		/// <code>levels[cur]</code> is the current zoom factor. If we are in
		/// a user-defined zoom level, then <code>custom</code> is the current
		/// zoom factor and <code>cur</code> is the greatest value such that
		/// {@code custom < level[cur]}}.</p>
		/// 
		/// </summary>
		/// <author>  Joel Uckelman
		/// </author>
		/// <since> 3.1.0
		/// </since>
		protected internal class State
		{
			private void  InitBlock()
			{
				levels = new double[l.size()];
				
				int i = 0;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Double d: l) levels [i ++] = d;
				System.Array.Sort(levels);
				
				cur = this.initial = initial;
				custom = - 1;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final ArrayList < Double > l = new ArrayList < Double >(levels.length);
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(double d: levels) l.add(d);
				return l;
			}
			virtual public double Zoom
			{
				get
				{
					return custom < 0?levels[cur]:custom;
				}
				
				set
				{
					if (value <= 0.0)
					{
						// This should never happen, it's just a kludge to make sure that
						// we continue having valid data even if our caller is wrong.
						//UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MIN_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						value = System.Double.MinValue;
					}
					
					cur = System.Array.BinarySearch(levels, (System.Object) value);
					if (cur < 0)
					{
						// if z is not a level, set cur to the next level > z
						cur = - cur - 1;
						
						// check whether we are close to a level
						if (cur < levels.Length && System.Math.Abs(value - levels[cur]) < 0.005)
						{
							custom = - 1;
						}
						else if (cur > 0 && System.Math.Abs(value - levels[cur - 1]) < 0.005)
						{
							--cur;
							custom = - 1;
						}
						else
						{
							custom = value;
						}
					}
					else
					{
						// custom is negative when we are in a predefined zoom level
						custom = - 1;
					}
				}
				
			}
			virtual public int Level
			{
				get
				{
					return cur;
				}
				
				set
				{
					cur = value;
					custom = - 1;
				}
				
			}
			virtual public int InitialLevel
			{
				get
				{
					return initial;
				}
				
			}
			virtual public int LevelCount
			{
				get
				{
					return levels.Length;
				}
				
			}
			private double custom;
			//UPGRADE_NOTE: Final was removed from the declaration of 'levels '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private double[] levels;
			private int cur;
			//UPGRADE_NOTE: Final was removed from the declaration of 'initial '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int initial;
			
			public State(double[] levels, int initial)
			{
				this.levels = levels;
				System.Array.Sort(this.levels);
				
				cur = this.initial = initial;
				custom = - 1;
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public State(Collection < Double > l, int initial)
			
			public virtual bool atLevel()
			{
				return custom < 0;
			}
			
			public virtual void  lowerLevel()
			{
				if (custom >= 0)
					custom = - 1;
				--cur;
			}
			
			public virtual void  higherLevel()
			{
				if (custom < 0)
					++cur;
				else
					custom = - 1;
			}
			
			public virtual bool hasLowerLevel()
			{
				return cur > 0;
			}
			
			public virtual bool hasHigherLevel()
			{
				return custom < 0?cur < levels.Length - 1:cur < levels.Length;
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public List < Double > getLevels()
		}
		
		public Zoomer()
		{
			InitBlock();
			state = new State(defaultZoomLevels, defaultInitialZoomLevel);
			
			//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			ActionListener zoomIn = new AnonymousClassActionListener(this);
			
			//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			ActionListener zoomOut = new AnonymousClassActionListener1(this);
			
			zoomMenu = new ZoomMenu();
			
			//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			ActionListener zoomPick = new AnonymousClassActionListener2(this);
			
			zoomPickButton = new LaunchButton(null, PICK_TOOLTIP, PICK_BUTTON_TEXT, ZOOM_PICK, PICK_ICON_NAME, zoomPick);
			zoomPickButton.setAttribute(PICK_TOOLTIP, Resources.getString("Zoomer.zoom_select")); //$NON-NLS-1$
			zoomPickButton.setAttribute(PICK_ICON_NAME, PICK_DEFAULT_ICON);
			
			zoomInButton = new LaunchButton(null, IN_TOOLTIP, IN_BUTTON_TEXT, ZOOM_IN, IN_ICON_NAME, zoomIn);
			zoomInButton.setAttribute(IN_TOOLTIP, Resources.getString("Zoomer.zoom_in")); //$NON-NLS-1$
			zoomInButton.setAttribute(IN_ICON_NAME, IN_DEFAULT_ICON);
			
			zoomOutButton = new LaunchButton(null, OUT_TOOLTIP, OUT_BUTTON_TEXT, ZOOM_OUT, OUT_ICON_NAME, zoomOut);
			zoomOutButton.setAttribute(OUT_TOOLTIP, Resources.getString("Zoomer.zoom_out")); //$NON-NLS-1$
			zoomOutButton.setAttribute(OUT_ICON_NAME, OUT_DEFAULT_ICON);
			
			setConfigureName(null);
			
			init();
		}
		
		protected internal virtual void  init()
		{
			zoomInButton.Enabled = state.hasHigherLevel();
			zoomPickButton.Enabled = true;
			zoomOutButton.Enabled = state.hasLowerLevel();
			zoomMenu.initZoomItems();
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
		InIconConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		NamedKeyStroke.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		String.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		String.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		PickIconConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		NamedKeyStroke.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		String.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		String.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		OutIconConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		NamedKeyStroke.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	public class InIconConfig : ConfigurerFactory
	{
		public virtual Configurer getConfigurer(AutoConfigurable c, System.String key, System.String name)
		{
			return new IconConfigurer(key, name, IN_DEFAULT_ICON);
		}
	}
	
	public class PickIconConfig : ConfigurerFactory
	{
		public virtual Configurer getConfigurer(AutoConfigurable c, System.String key, System.String name)
		{
			return new IconConfigurer(key, name, PICK_DEFAULT_ICON);
		}
	}
	
	public class OutIconConfig : ConfigurerFactory
	{
		public virtual Configurer getConfigurer(AutoConfigurable c, System.String key, System.String name)
		{
			return new IconConfigurer(key, name, OUT_DEFAULT_ICON);
		}
	}
	
	public class LevelConfig : ConfigurerFactory
	{
		public virtual Configurer getConfigurer(AutoConfigurable c, System.String key, System.String name)
		{
			return new LevelConfigurer((Zoomer) c, key, name);
		}
	}
	
	/// <summary> The {@link Configurer} for {@link #ZOOM_LEVELS} and {@link #ZOOM_START}.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	protected internal class LevelConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(LevelConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(LevelConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private LevelConfigurer enclosingInstance;
			public LevelConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.addLevel();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDocumentListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_TODO: Interface 'javax.swing.event.DocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		private class AnonymousClassDocumentListener : DocumentListener
		{
			public AnonymousClassDocumentListener(VassalSharp.build.module.map.Zoomer z, LevelConfigurer enclosingInstance)
			{
				InitBlock(z, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.build.module.map.Zoomer z, LevelConfigurer enclosingInstance)
			{
				this.z = z;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable z was copied into class AnonymousClassDocumentListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.build.module.map.Zoomer z;
			private LevelConfigurer enclosingInstance;
			public LevelConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  changedUpdate(DocumentEvent e)
			{
			}
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  insertUpdate(DocumentEvent e)
			{
				validate();
			}
			//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
			public virtual void  removeUpdate(DocumentEvent e)
			{
				validate();
			}
			
			private const System.String pattern = "^(\\d*[1-9]\\d*(/\\d*[1-9]\\d*|\\.\\d*)?|0*\\.\\d*[1-9]\\d*)$"; //$NON-NLS-1$
			
			private void  validate()
			{
				// valid entries match the pattern and aren't already in the list
				//UPGRADE_NOTE: Final was removed from the declaration of 'text '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String text = Enclosing_Instance.levelField.Text;
				Enclosing_Instance.addButton.Enabled = text.matches(pattern) && !Enclosing_Instance.z.state.getLevels().contains(Enclosing_Instance.parseLevel(text));
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(LevelConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(LevelConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private LevelConfigurer enclosingInstance;
			public LevelConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				if (Enclosing_Instance.addButton.Enabled)
					Enclosing_Instance.addLevel();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(VassalSharp.build.module.map.Zoomer z, LevelConfigurer enclosingInstance)
			{
				InitBlock(z, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.build.module.map.Zoomer z, LevelConfigurer enclosingInstance)
			{
				this.z = z;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable z was copied into class AnonymousClassActionListener2. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.build.module.map.Zoomer z;
			private LevelConfigurer enclosingInstance;
			public LevelConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				// get the zoom level index to be removed
				//UPGRADE_NOTE: Final was removed from the declaration of 'rm_level '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int rm_level = Enclosing_Instance.levelList.SelectedIndex;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final List < Double > l = z.state.getLevels();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'new_init '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int new_init;
				if (rm_level == z.state.InitialLevel)
				{
					// we're deleting the initial level; keep it the same position
					new_init = System.Math.Min(rm_level, z.state.LevelCount - 2);
					l.remove(rm_level);
				}
				else
				{
					// find the new index of the old initial level
					//UPGRADE_NOTE: Final was removed from the declaration of 'old_init_val '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Double old_init_val = l.get_Renamed(z.state.InitialLevel);
					l.remove(rm_level);
					new_init = l.indexOf(old_init_val);
				}
				
				// adjust the state
				z.state = new State(l, new_init);
				z.init();
				Enclosing_Instance.model.updateModel();
				
				// adjust the selection
				Enclosing_Instance.levelList.setSelectedIndex(Math.max(Math.min(rm_level, l.size() - 1), 0));
				Enclosing_Instance.updateButtons();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener3
		{
			public AnonymousClassActionListener3(VassalSharp.build.module.map.Zoomer z, LevelConfigurer enclosingInstance)
			{
				InitBlock(z, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.build.module.map.Zoomer z, LevelConfigurer enclosingInstance)
			{
				this.z = z;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable z was copied into class AnonymousClassActionListener3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.build.module.map.Zoomer z;
			private LevelConfigurer enclosingInstance;
			public LevelConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				// set the new initial scale level
				//UPGRADE_NOTE: Final was removed from the declaration of 'i '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int i = Enclosing_Instance.levelList.SelectedIndex;
				z.state = new State(Enclosing_Instance.z.state.getLevels(), i);
				z.init();
				Enclosing_Instance.model.updateModel();
				Enclosing_Instance.updateButtons();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassListSelectionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassListSelectionListener
		{
			public AnonymousClassListSelectionListener(LevelConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(LevelConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private LevelConfigurer enclosingInstance;
			public LevelConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  valueChanged(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.updateButtons();
			}
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				return panel;
			}
			
		}
		override public System.String ValueString
		{
			get
			{
				return null;
			}
			
		}
		private Zoomer z;
		
		private System.Windows.Forms.Panel panel;
		private LevelModel model;
		private System.Windows.Forms.ListBox levelList;
		private System.Windows.Forms.Button addButton;
		private System.Windows.Forms.Button removeButton;
		private System.Windows.Forms.Button initialButton;
		private System.Windows.Forms.TextBox levelField;
		
		public LevelConfigurer(Zoomer z, System.String key, System.String name):base(key, name)
		{
			this.z = z;
			
			panel = new System.Windows.Forms.Panel();
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			panel.setLayout(new BoxLayout(panel, BoxLayout.X_AXIS));
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'leftBox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box leftBox = Box.createVerticalBox();
			//UPGRADE_NOTE: Final was removed from the declaration of 'addBox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box addBox = Box.createHorizontalBox();
			
			// Add button
			addButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.ADD));
			addButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(addButton);
			
			addButton.Enabled = false;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			addBox.Controls.Add(addButton);
			
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			levelField = new System.Windows.Forms.TextBox();
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
			levelField.setMaximumSize(new System.Drawing.Size(System.Int32.MaxValue, levelField.Size.Height));
			
			// validator for the level entry field
			//UPGRADE_ISSUE: Method 'javax.swing.text.Document.addDocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
			//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
			((System.String) levelField.Text).addDocumentListener(new AnonymousClassDocumentListener(z, this));
			
			// rely on addButton to do the validation
			//UPGRADE_TODO: Method 'javax.swing.JTextField.addActionListener' was converted to 'System.Windows.Forms.TextBox.KeyPress' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldaddActionListener_javaawteventActionListener'"
			levelField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(new AnonymousClassActionListener1(this).actionPerformed);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			addBox.Controls.Add(levelField);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			leftBox.Controls.Add(addBox);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'buttonBox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box buttonBox = Box.createHorizontalBox();
			
			// Remove button
			removeButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.REMOVE));
			removeButton.Click += new System.EventHandler(new AnonymousClassActionListener2(z, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(removeButton);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonBox.Controls.Add(removeButton);
			
			// Set Initial button
			initialButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString("Editor.zoom.set_initial")); //$NON-NLS-1$
			initialButton.Click += new System.EventHandler(new AnonymousClassActionListener3(z, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(initialButton);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonBox.Controls.Add(initialButton);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			leftBox.Controls.Add(buttonBox);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'explanation '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label;
			temp_label = new System.Windows.Forms.Label();
			temp_label.Text = Resources.getString("Editor.zoom.initial_zoom");
			System.Windows.Forms.Label explanation = temp_label; //$NON-NLS-1$
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentX' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentX_float'"
			//UPGRADE_ISSUE: Field 'java.awt.Component.CENTER_ALIGNMENT' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentCENTER_ALIGNMENT_f'"
			explanation.setAlignmentX(JLabel.CENTER_ALIGNMENT);
			
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalStrut' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = Box.createVerticalStrut(explanation.Size.Height);
			leftBox.Controls.Add(temp_Control);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			leftBox.Controls.Add(explanation);
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalStrut' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control2;
			temp_Control2 = Box.createVerticalStrut(explanation.Size.Height);
			leftBox.Controls.Add(temp_Control2);
			
			// level list
			model = new LevelModel(this);
			System.Windows.Forms.ListBox temp_ListBox;
			temp_ListBox = new System.Windows.Forms.ListBox();
			temp_ListBox.Items.AddRange(model);
			temp_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			levelList = temp_ListBox;
			//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.ListSelectionModel.SINGLE_SELECTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			levelList.SelectionMode = (System.Windows.Forms.SelectionMode) System.Windows.Forms.SelectionMode.One;
			//UPGRADE_TODO: Method 'javax.swing.JList.setSelectedIndex' was converted to 'System.Windows.Forms.ListBox.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetSelectedIndex_int'"
			levelList.SelectedIndex = 0;
			
			levelList.SelectedValueChanged += new System.EventHandler(new AnonymousClassListSelectionListener(this).valueChanged);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'pane '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'javax.swing.JSplitPane' was converted to 'SupportClass.SplitterPanelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			SupportClass.SplitterPanelSupport pane = new SupportClass.SplitterPanelSupport((int) System.Windows.Forms.Orientation.Horizontal);
			pane.FirstControl = leftBox;
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
			temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol.AutoScroll = true;
			temp_scrollablecontrol.Controls.Add(levelList);
			pane.SecondControl = temp_scrollablecontrol;
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			panel.Controls.Add(pane);
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.border.TitledBorder.TitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingborderTitledBorder'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(panel.CreateGraphics(), 0, 0, panel.Width, panel.Height, new TitledBorder(name));
			updateButtons();
		}
		
		/// <summary> Parse a <code>String</code> to a <code>double</code>.
		/// Accepts fractions as "n/d".
		/// </summary>
		protected internal virtual double parseLevel(System.String text)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] s = text.split("/"); //$NON-NLS-1$
			try
			{
				return s.Length > 1?System.Double.Parse(s[0]) / System.Double.Parse(s[1]):System.Double.Parse(s[0]);
			}
			catch (System.FormatException ex)
			{
				// should not happen, text already validated
				ErrorDialog.bug(ex);
			}
			return 0.0;
		}
		
		/// <summary> Add a level to the level list. This method expects that the
		/// intput has already been validated.
		/// </summary>
		protected internal virtual void  addLevel()
		{
			// get the initial scale level
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < Double > l = z.state.getLevels();
			//UPGRADE_NOTE: Final was removed from the declaration of 'old_init_val '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Double old_init_val = l.get_Renamed(z.state.InitialLevel);
			
			// add the new scale level
			//UPGRADE_NOTE: Final was removed from the declaration of 'new_level_val '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			double new_level_val = parseLevel(levelField.Text);
			l.add(new_level_val);
			Collections.sort(l);
			
			// find the initial scale index
			//UPGRADE_NOTE: Final was removed from the declaration of 'new_init '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int new_init = l.indexOf(old_init_val);
			
			// adjust the state
			z.state = new State(l, new_init);
			z.init();
			model.updateModel();
			
			// adjust the selection
			//UPGRADE_NOTE: Final was removed from the declaration of 'new_level '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int new_level = l.indexOf(new_level_val);
			//UPGRADE_TODO: Method 'javax.swing.JList.setSelectedIndex' was converted to 'System.Windows.Forms.ListBox.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetSelectedIndex_int'"
			levelList.SelectedIndex = new_level;
			
			//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
			levelField.Text = "";
			updateButtons();
		}
		
		/// <summary> Ensures that the buttons are properly en- or disabled.</summary>
		protected internal virtual void  updateButtons()
		{
			removeButton.Enabled = z.state.LevelCount > 1;
			initialButton.Enabled = levelList.SelectedIndex != z.state.InitialLevel;
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'LevelModel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> A {@link ListModel} built on the {@link State}.</summary>
		//UPGRADE_TODO: Class 'javax.swing.AbstractListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractListModel'"
		[Serializable]
		protected internal class LevelModel:System.Windows.Forms.ListBox.ObjectCollection
		{
			public LevelModel(LevelConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(LevelConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private LevelConfigurer enclosingInstance;
			//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			public override System.Object this[int i]
			{
				get
				{
					return Enclosing_Instance.z.state.getLevels().get_Renamed(i) + (Enclosing_Instance.z.state.InitialLevel == i?" *":""); //$NON-NLS-1$ //$NON-NLS-2$
				}
				
				set
				{
					base[i] = value;
				}
				
			}
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			new virtual public int Count
			{
				get
				{
					return Enclosing_Instance.z.state.LevelCount;
				}
				
			}
			public LevelConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			public virtual void  updateModel()
			{
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractListModel.fireContentsChanged' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractListModelfireContentsChanged_javalangObject_int_int'"
				fireContentsChanged(this, 0, Enclosing_Instance.z.state.LevelCount - 1);
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override void  setValue(System.Object o)
		{
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override void  setValue(System.String s)
		{
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String ZOOM_START = zoomStart; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String ZOOM_LEVELS = zoomLevels; //$NON-NLS-1$
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String ZOOM_IN = zoomInKey; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String IN_TOOLTIP = inTooltip; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String IN_BUTTON_TEXT = inButtonText; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String IN_ICON_NAME = inIconName; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String IN_DEFAULT_ICON = /images/zoomIn.gif; //$NON-NLS-1$
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String ZOOM_PICK = zoomPickKey; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String PICK_TOOLTIP = pickTooltip; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String PICK_BUTTON_TEXT = pickButtonText; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String PICK_ICON_NAME = pickIconName; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String PICK_DEFAULT_ICON = /images/zoom.png; //$NON-NLS-1$
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String ZOOM_OUT = zoomOutKey; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String OUT_TOOLTIP = outTooltip; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String OUT_BUTTON_TEXT = outButtonText; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String OUT_ICON_NAME = outIconName; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static final String OUT_DEFAULT_ICON = /images/zoomOut.gif; //$NON-NLS-1$
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void addTo(Buildable b)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		GameModule.getGameModule().getGameState().addGameComponent(this);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	map =(Map) b;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	validator = new SingleChildInstance(map, getClass());
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	map.setZoomer(this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	map.getToolBar().add(zoomInButton);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	map.getToolBar().add(zoomPickButton);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	map.getToolBar().add(zoomOutButton);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getAttributeValueString(String key)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(ZOOM_START.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// Notes:
	//
	// 1. ZOOM_START is one-based, not zero-based.
	// 2. The levels in state run from zoomed out to zoomed in,
	// while the levels coming from outside Zoomer run from
	// zoomed in to zoomed out. Hence we reverse the initial
	// zoom level being returned here.
	//
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return String.valueOf(state.getLevelCount() - state.getInitialLevel());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(ZOOM_LEVELS.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final List < Double > levels = state.getLevels();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final String [] s = new String [levels.size()];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < s.length;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		s [i] = levels.get(i).toString();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	return StringArrayConfigurer.arrayToString(s);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	else if(zoomInButton.getAttributeValueString(key) != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return zoomInButton.getAttributeValueString(key);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(zoomPickButton.getAttributeValueString(key) != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return zoomPickButton.getAttributeValueString(key);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return zoomOutButton.getAttributeValueString(key);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setAttribute(String key, Object val)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(ZOOM_START.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(val instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		val = Integer.valueOf((String) val);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	if(val != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// Notes:
	//
	// 1. ZOOM_START is one-based, not zero-based.
	// 2. The levels in state run from zoomed out to zoomed in,
	// while the levels coming from outside Zoomer run from
	// zoomed in to zoomed out. Hence we reverse the initial
	// zoom level being set here.
	//
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final List < Double > levels = state.getLevels();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final int initial = 
	Math.max(0, Math.min(levels.size() - 1, levels.size() -(Integer) val));
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	state = new State(levels, initial);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(deprecatedFactor > 0 && deprecatedMax > 0)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// zero these to prevent further adjustments due to old properties
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	deprecatedFactor = 0.0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	deprecatedMax = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	init();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(ZOOM_LEVELS.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(val instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		val = StringArrayConfigurer.stringToArray((String) val);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	if(val != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// dump into a set to remove duplicates
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final HashSet < Double > levels = new HashSet < Double >();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(String s:(String []) val)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		levels.add(Double.valueOf(s));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	state = new State(levels, 
	Math.min(state.getInitialLevel(), levels.size() - 1));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	init();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(FACTOR.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ // deprecated key
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(val instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		val = Double.valueOf((String) val);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	if(val != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		deprecatedFactor =(Double) val;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(deprecatedFactor > 0 && deprecatedMax > 0)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		adjustStateForFactorAndMax();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(MAX.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ // deprecated key
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(val instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		val = Integer.valueOf((String) val);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	if(val != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		deprecatedMax =(Integer) val;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(deprecatedFactor > 0 && deprecatedMax > 0)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		adjustStateForFactorAndMax();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// FIXME: does having this as an extremal case cause weird behavior for
	// unrecognized keys?
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	zoomInButton.setAttribute(key, val);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	zoomPickButton.setAttribute(key, val);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	zoomOutButton.setAttribute(key, val);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	// begin deprecated keys
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private static final String FACTOR = factor; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private static final String MAX = max; //$NON-NLS-1$
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private int deprecatedMax = - 1;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private double deprecatedFactor = - 1.0;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void adjustStateForFactorAndMax()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final double [] levels = new double [deprecatedMax + 1];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < levels.length;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i) 
	levels [i] = Math.pow(deprecatedFactor, -(i - 1));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final int initial = Math.min(state.getInitialLevel(), levels.length - 1);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	state = new State(levels, initial);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	init();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	// end deprecated keys
	
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
	void removeFrom(Buildable b)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		map =(Map) b;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	map.setZoomer(null);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	map.getToolBar().remove(zoomInButton);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	map.getToolBar().remove(zoomPickButton);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	map.getToolBar().remove(zoomOutButton);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public double getZoomFactor()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return state.getZoom();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected Point getMapCenter()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final Rectangle r = map.getView().getVisibleRect();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return map.mapCoordinates(new Point(r.x + r.width / 2, r.y + r.height / 2));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void updateZoomer(Point center)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		zoomInButton.setEnabled(state.hasHigherLevel());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	zoomOutButton.setEnabled(state.hasLowerLevel());
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	zoomMenu.updateZoom();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final Dimension d = map.getPreferredSize();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	map.getView().setBounds(0, 0, d.width, d.height); // calls revalidate()
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	map.centerAt(center);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	map.repaint(true);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setZoomLevel(int l)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final Point center = getMapCenter();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	state.setLevel(l);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	updateZoomer(center);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setZoomFactor(double z)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final Point center = getMapCenter();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	state.setZoom(z);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	updateZoomer(center);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void zoomIn()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(state.hasHigherLevel())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final Point center = getMapCenter();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	state.higherLevel();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	updateZoomer(center);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void zoomOut()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(state.hasLowerLevel())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final Point center = getMapCenter();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	state.lowerLevel();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	updateZoomer(center);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public HelpFile getHelpFile()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return HelpFile.getReferenceManualPage(Map.htm, Zoom); //$NON-NLS-1$ //$NON-NLS-2$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setup(boolean gameStarting)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(!gameStarting)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		zoomInButton.setEnabled(state.hasHigherLevel());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	zoomOutButton.setEnabled(state.hasLowerLevel());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	zoomPickButton.setEnabled(gameStarting);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Command getRestoreCommand()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> The menu which displays zoom levels.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
	[Serializable]
	protected internal class ZoomMenu:System.Windows.Forms.ContextMenu
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'other '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.Windows.Forms.MenuItem other;
		//UPGRADE_NOTE: Final was removed from the declaration of 'sep '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.JPopupMenu.Separator' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		protected internal JPopupMenu.Separator sep;
		//UPGRADE_NOTE: Final was removed from the declaration of 'bg '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.ButtonGroup' was converted to 'System.Collections.ArrayList' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		protected internal System.Collections.ArrayList bg;
		
		private const System.String OTHER = "Other..."; //$NON-NLS-1$
		private const System.String FIT_WIDTH = "Fit Width"; //$NON-NLS-1$
		private const System.String FIT_HEIGHT = "Fit Height"; //$NON-NLS-1$
		private const System.String FIT_VISIBLE = "Fit Visible"; //$NON-NLS-1$
		
		private const long serialVersionUID = 1L;
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public ZoomMenu():base()
		{
			
			//UPGRADE_TODO: Constructor 'javax.swing.JPopupMenu.Separator.Separator' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			sep = new JPopupMenu.Separator(this);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(sep);
			
			//UPGRADE_TODO: Class 'javax.swing.ButtonGroup' was converted to 'System.Collections.ArrayList' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			bg = new System.Collections.ArrayList();
			
			System.Windows.Forms.MenuItem temp_menuitem;
			//UPGRADE_TODO: Constructor 'javax.swing.JRadioButtonMenuItem.JRadioButtonMenuItem' was converted to 'System.Windows.Forms.MenuItem' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJRadioButtonMenuItemJRadioButtonMenuItem_javalangString'"
			temp_menuitem = new System.Windows.Forms.MenuItem();
			temp_menuitem.RadioCheck = true;
			temp_menuitem.Text = Resources.getString("Zoomer.ZoomMenu.other");
			other = temp_menuitem; //$NON-NLS-1$
			SupportClass.CommandManager.SetCommand(other, OTHER);
			other.Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(other);
			bg.Add((System.Object) other);
			//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.add' was converted to 'SupportClass.AddMenuItem' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenuadd_javaxswingJMenuItem'"
			SupportClass.AddMenuItem(this, other);
			
			MenuItems.Add("-");
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'fw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem fw = new System.Windows.Forms.MenuItem(Resources.getString("Zoomer.ZoomMenu.fit_width")); //$NON-NLS-1$
			SupportClass.CommandManager.SetCommand(fw, FIT_WIDTH);
			fw.Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(fw);
			//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.add' was converted to 'SupportClass.AddMenuItem' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenuadd_javaxswingJMenuItem'"
			SupportClass.AddMenuItem(this, fw);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'fh '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem fh = new System.Windows.Forms.MenuItem(Resources.getString("Zoomer.ZoomMenu.fit_height")); //$NON-NLS-1$
			SupportClass.CommandManager.SetCommand(fh, FIT_HEIGHT);
			fh.Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(fh);
			//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.add' was converted to 'SupportClass.AddMenuItem' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenuadd_javaxswingJMenuItem'"
			SupportClass.AddMenuItem(this, fh);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'fv '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem fv = new System.Windows.Forms.MenuItem(Resources.getString("Zoomer.ZoomMenu.fit_visible")); //$NON-NLS-1$
			SupportClass.CommandManager.SetCommand(fv, FIT_VISIBLE);
			fv.Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(fv);
			//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.add' was converted to 'SupportClass.AddMenuItem' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenuadd_javaxswingJMenuItem'"
			SupportClass.AddMenuItem(this, fv);
		}
		
		public virtual void  initZoomItems()
		{
			while (Controls[0] != sep)
				MenuItems.RemoveAt(0);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < Double > levels = state.getLevels();
			for (int i = 0; i < levels.size(); ++i)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'zs '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String zs = Long.toString(Math.round(levels.get_Renamed(i) * 100)) + "%"; //$NON-NLS-1$
				//UPGRADE_NOTE: Final was removed from the declaration of 'item '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.MenuItem temp_menuitem;
				//UPGRADE_TODO: Constructor 'javax.swing.JRadioButtonMenuItem.JRadioButtonMenuItem' was converted to 'System.Windows.Forms.MenuItem' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJRadioButtonMenuItemJRadioButtonMenuItem_javalangString'"
				temp_menuitem = new System.Windows.Forms.MenuItem();
				temp_menuitem.RadioCheck = true;
				temp_menuitem.Text = zs;
				System.Windows.Forms.MenuItem item = temp_menuitem;
				SupportClass.CommandManager.SetCommand(item, System.Convert.ToString(i));
				item.Click += new System.EventHandler(this.actionPerformed);
				SupportClass.CommandManager.CheckCommand(item);
				bg.Add((System.Object) item);
				//UPGRADE_ISSUE: Method 'javax.swing.JPopupMenu.insert' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJPopupMenuinsert_javaawtComponent_int'"
				insert(item, 0);
			}
			
			((System.Windows.Forms.MenuItem) getComponent(state.getLevelCount() - state.getLevel() - 1)).Checked = true;
		}
		
		public virtual void  actionPerformed(System.Object event_sender, System.EventArgs a)
		{
			try
			{
				setZoomLevel(System.Int32.Parse(SupportClass.CommandManager.GetCommand(event_sender)));
				return ;
			}
			catch (System.FormatException e)
			{
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'cmd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String cmd = SupportClass.CommandManager.GetCommand(event_sender);
			
			if (OTHER.Equals(cmd))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'dialog '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
				ZoomDialog dialog = new ZoomDialog((System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), map.getView()), Resources.getString("Zoomer.ZoomDialog.title"), true); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				dialog.Visible = true;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'z '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				double z = dialog.Result / 100.0;
				if (z > 0 && z != state.getZoom())
				{
					setZoomFactor(z);
				}
			}
			// FIXME: should be map.getSize() for consistency?
			else if (FIT_WIDTH.Equals(cmd))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'vd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size vd = map.getView().getVisibleRect().getSize();
				//UPGRADE_NOTE: Final was removed from the declaration of 'md '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size md = map.mapSize();
				//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Dimension.getWidth' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				setZoomFactor((System.Int32) vd.Width / (System.Int32) md.Width);
			}
			else if (FIT_HEIGHT.Equals(cmd))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'vd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size vd = map.getView().getVisibleRect().getSize();
				//UPGRADE_NOTE: Final was removed from the declaration of 'md '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size md = map.mapSize();
				//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Dimension.getHeight' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				setZoomFactor((System.Int32) vd.Height / (System.Int32) md.Height);
			}
			else if (FIT_VISIBLE.Equals(cmd))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'vd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size vd = map.getView().getVisibleRect().getSize();
				//UPGRADE_NOTE: Final was removed from the declaration of 'md '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size md = map.mapSize();
				//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Dimension.getWidth' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Dimension.getHeight' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				setZoomFactor(System.Math.Min((System.Int32) vd.Width / (System.Int32) md.Width, (System.Int32) vd.Height / (System.Int32) md.Height));
			}
			else
			{
				// this should not happen!
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				assert false;
			}
		}
		
		public virtual void  updateZoom()
		{
			if (state.atLevel())
			{
				((System.Windows.Forms.MenuItem) getComponent(state.getLevelCount() - state.getLevel() - 1)).Checked = true;
			}
			else
			{
				other.Checked = true;
			}
		}
	}
	
	/// <summary> The dialog for setting custom zoom levels.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	[Serializable]
	protected internal class ZoomDialog:System.Windows.Forms.Form
	{
		virtual public double Result
		{
			get
			{
				return result;
			}
			
		}
		protected internal double result;
		//UPGRADE_NOTE: Final was removed from the declaration of 'ratioNumeratorSpinner '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal JSpinner ratioNumeratorSpinner;
		//UPGRADE_NOTE: Final was removed from the declaration of 'ratioDenominatorSpinner '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal JSpinner ratioDenominatorSpinner;
		//UPGRADE_NOTE: Final was removed from the declaration of 'percentSpinner '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal JSpinner percentSpinner;
		//UPGRADE_NOTE: Final was removed from the declaration of 'ratioNumeratorModel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal SpinnerNumberModel ratioNumeratorModel;
		//UPGRADE_NOTE: Final was removed from the declaration of 'ratioDenominatorModel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal SpinnerNumberModel ratioDenominatorModel;
		//UPGRADE_NOTE: Final was removed from the declaration of 'percentModel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal SpinnerNumberModel percentModel;
		//UPGRADE_NOTE: Final was removed from the declaration of 'okButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.Windows.Forms.Button okButton;
		
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		public ZoomDialog(System.Windows.Forms.Form owner, System.String title, bool modal):base()
		{
			//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_javalangString_boolean'"
			SupportClass.DialogSupport.SetDialog(this, owner, title);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'hsep '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int hsep = 5;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'controlsPane '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Constructor 'java.awt.GridBagLayout.GridBagLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagLayout'"
			new GridBagLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			System.Windows.Forms.Panel controlsPane = new System.Windows.Forms.Panel();
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'java.awt.GridBagConstraints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			//UPGRADE_ISSUE: Constructor 'java.awt.GridBagConstraints.GridBagConstraints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			GridBagConstraints c = new GridBagConstraints();
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.fill' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.HORIZONTAL' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.fill = GridBagConstraints.HORIZONTAL;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'linset '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Int32[] linset = new System.Int32[]{0, 0, 11, 11};
			//UPGRADE_NOTE: Final was removed from the declaration of 'dinset '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Int32[] dinset = new System.Int32[]{0, 0, 0, 0};
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'ratioLabel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label;
			temp_label = new System.Windows.Forms.Label();
			temp_label.Text = Resources.getString("Zoomer.ZoomDialog.zoom_ratio");
			System.Windows.Forms.Label ratioLabel = temp_label; //$NON-NLS-1$
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.gridx = 0;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridy' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.gridy = 0;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.weightx = 0;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weighty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.weighty = 0;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.insets' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.insets = linset;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.anchor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.anchor = GridBagConstraints.LINE_START;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			controlsPane.Controls.Add(ratioLabel);
			ratioLabel.Dock = new System.Windows.Forms.DockStyle();
			ratioLabel.BringToFront();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'ratioBox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.Box.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			Box ratioBox = new Box(BoxLayout.X_AXIS);
			//UPGRADE_ISSUE: Method 'javax.swing.JLabel.setLabelFor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJLabelsetLabelFor_javaawtComponent'"
			ratioLabel.setLabelFor(ratioBox);
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.gridx = 1;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridy' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.gridy = 0;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.weightx = 1;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weighty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.weighty = 0;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.insets' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.insets = dinset;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.anchor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.anchor = GridBagConstraints.LINE_START;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			controlsPane.Controls.Add(ratioBox);
			ratioBox.Dock = new System.Windows.Forms.DockStyle();
			ratioBox.BringToFront();
			
			ratioNumeratorModel = new SpinnerNumberModel(1, 1, 256, 1);
			ratioNumeratorSpinner = new JSpinner(ratioNumeratorModel);
			ratioNumeratorSpinner.addChangeListener(this);
			ratioBox.add(ratioNumeratorSpinner);
			
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalStrut' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = Box.createHorizontalStrut(hsep);
			ratioBox.Controls.Add(temp_Control);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'ratioColon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label2;
			temp_label2 = new System.Windows.Forms.Label();
			temp_label2.Text = ":";
			System.Windows.Forms.Label ratioColon = temp_label2; //$NON-NLS-1$
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			ratioBox.Controls.Add(ratioColon);
			
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalStrut' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control2;
			temp_Control2 = Box.createHorizontalStrut(hsep);
			ratioBox.Controls.Add(temp_Control2);
			
			ratioDenominatorModel = new SpinnerNumberModel(1, 1, 256, 1);
			ratioDenominatorSpinner = new JSpinner(ratioDenominatorModel);
			ratioDenominatorSpinner.addChangeListener(this);
			ratioBox.add(ratioDenominatorSpinner);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'percentLabel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label3;
			temp_label3 = new System.Windows.Forms.Label();
			temp_label3.Text = Resources.getString("Zoomer.ZoomDialog.zoom_percent");
			System.Windows.Forms.Label percentLabel = temp_label3; //$NON-NLS-1$
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.gridx = 0;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridy' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.gridy = 1;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.weightx = 0;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weighty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.weighty = 0;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.insets' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.insets = linset;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.anchor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.anchor = GridBagConstraints.LINE_START;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			controlsPane.Controls.Add(percentLabel);
			percentLabel.Dock = new System.Windows.Forms.DockStyle();
			percentLabel.BringToFront();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'percentBox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.Box.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			Box percentBox = new Box(BoxLayout.X_AXIS);
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.gridx = 1;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.gridy' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.gridy = 1;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weightx' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.weightx = 1;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.weighty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.weighty = 0;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.insets' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.insets = dinset;
			//UPGRADE_ISSUE: Field 'java.awt.GridBagConstraints.anchor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGridBagConstraints'"
			c.anchor = GridBagConstraints.LINE_START;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			controlsPane.Controls.Add(percentBox);
			percentBox.Dock = new System.Windows.Forms.DockStyle();
			percentBox.BringToFront();
			
			percentModel = new SpinnerNumberModel(state.getZoom() * 100.0, 0.39, 25600.0, 10.0);
			percentSpinner = new JSpinner(percentModel);
			percentLabel.setLabelFor(percentSpinner);
			percentSpinner.addChangeListener(this);
			percentBox.add(percentSpinner);
			
			updateRatio();
			
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalStrut' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control3;
			temp_Control3 = Box.createHorizontalStrut(hsep);
			percentBox.Controls.Add(temp_Control3);
			//UPGRADE_NOTE: Final was removed from the declaration of 'percentSign '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label4;
			temp_label4 = new System.Windows.Forms.Label();
			temp_label4.Text = "%";
			System.Windows.Forms.Label percentSign = temp_label4; //$NON-NLS-1$
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			percentBox.Controls.Add(percentSign);
			
			// buttons
			//UPGRADE_NOTE: Final was removed from the declaration of 'buttonBox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.Box.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			Box buttonBox = new Box(BoxLayout.X_AXIS);
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalGlue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control4;
			temp_Control4 = Box.createHorizontalGlue();
			buttonBox.Controls.Add(temp_Control4);
			
			okButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.OK));
			okButton.Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(okButton);
			//UPGRADE_TODO: Method 'javax.swing.JRootPane.setDefaultButton' was converted to 'System.Windows.Forms.Form.AcceptButton' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJRootPanesetDefaultButton_javaxswingJButton'"
			okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.AcceptButton = okButton;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonBox.Controls.Add(okButton);
			
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalStrut' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control5;
			temp_Control5 = Box.createHorizontalStrut(hsep);
			buttonBox.Controls.Add(temp_Control5);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'cancelButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Button cancelButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.CANCEL));
			cancelButton.Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(cancelButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonBox.Controls.Add(cancelButton);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'okDim '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Drawing.Size okDim = okButton.Size;
			//UPGRADE_NOTE: Final was removed from the declaration of 'cancelDim '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Drawing.Size cancelDim = cancelButton.Size;
			//UPGRADE_NOTE: Final was removed from the declaration of 'buttonDimension '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Size buttonDimension = new System.Drawing.Size(System.Math.Max(okDim.Width, cancelDim.Width), System.Math.Max(okDim.Height, cancelDim.Height));
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			okButton.Size = buttonDimension;
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			cancelButton.Size = buttonDimension;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'contentPane '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Control contentPane = (System.Windows.Forms.Control) ((System.Windows.Forms.ContainerControl) this);
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.border.EmptyBorder.EmptyBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingborderEmptyBorderEmptyBorder_int_int_int_int'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(contentPane.CreateGraphics(), 0, 0, contentPane.Width, contentPane.Height, new EmptyBorder(12, 12, 11, 11));
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			/*
			contentPane.setLayout(new BorderLayout(0, 11));*/
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			contentPane.Controls.Add(controlsPane);
			controlsPane.Dock = System.Windows.Forms.DockStyle.Fill;
			controlsPane.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			contentPane.Controls.Add(buttonBox);
			buttonBox.Dock = new System.Windows.Forms.DockStyle();
			buttonBox.BringToFront();
			
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
		}
		
		public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			result = event_sender == okButton?percentModel.getNumber().doubleValue():0.0;
			
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			Visible = false;
		}
		
		public virtual void  stateChanged(System.Object event_sender, System.EventArgs e)
		{
			if (event_sender == ratioNumeratorSpinner || event_sender == ratioDenominatorSpinner)
			{
				updatePercent();
			}
			else if (event_sender == percentSpinner)
			{
				updateRatio();
			}
		}
		
		private void  updatePercent()
		{
			// disconnect listener to prevent circularity
			percentSpinner.removeChangeListener(this);
			
			percentModel.setValue(ratioNumeratorModel.getNumber().doubleValue() / ratioDenominatorModel.getNumber().doubleValue() * 100.0);
			
			percentSpinner.addChangeListener(this);
		}
		
		private void  updateRatio()
		{
			// Warning: Heavy maths ahead!
			//
			// This algorithm borrowed from gimpzoommodel.c in LIBGIMP:
			// http://svn.gnome.org/viewcvs/gimp/trunk/libgimpwidgets/gimpzoommodel.c
			//
			// See also http://www.virtualdub.org/blog/pivot/entry.php?id=81
			// for a discussion of calculating continued fractions by convergeants.
			
			double z = percentModel.getNumber().doubleValue() / 100.0;
			
			// we want symmetric behavior, so find reciprocal when zooming out
			bool swapped = false;
			if (z < 1.0)
			{
				z = 1.0 / z;
				swapped = true;
			}
			
			// calculate convergeants
			int p0 = 1;
			int q0 = 0;
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int p1 = (int) System.Math.Floor(z);
			int q1 = 1;
			int p2;
			int q2;
			
			double r = z - p1;
			double next_cf;
			
			while (System.Math.Abs(r) >= 0.0001 && System.Math.Abs((double) p1 / q1 - z) > 0.0001)
			{
				r = 1.0 / r;
				next_cf = System.Math.Floor(r);
				
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				p2 = (int) (next_cf * p1 + p0);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				q2 = (int) (next_cf * q1 + q0);
				
				// We limit the numerator and denominator to be 256 or less,
				// and also exclude absurd ratios like 170:171.
				if (p2 > 256 || q2 > 256 || (p2 > 1 && q2 > 1 && p2 * q2 > 200))
					break;
				
				// remember the last two fractions
				p0 = p1;
				p1 = p2;
				q0 = q1;
				q1 = q2;
				
				r -= next_cf;
			}
			
			z = (double) p1 / q1;
			
			// hard upper and lower bounds for zoom ratio
			if (z > 256.0)
			{
				p1 = 256;
				q1 = 1;
			}
			else if (z < 1.0 / 256.0)
			{
				p1 = 1;
				q1 = 256;
			}
			
			if (swapped)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int tmp = p1;
				p1 = q1;
				q1 = tmp;
			}
			
			// disconnect listeners to prevent circularity
			ratioNumeratorSpinner.removeChangeListener(this);
			ratioDenominatorSpinner.removeChangeListener(this);
			
			ratioNumeratorModel.setValue(p1);
			ratioDenominatorModel.setValue(q1);
			
			ratioNumeratorSpinner.addChangeListener(this);
			ratioDenominatorSpinner.addChangeListener(this);
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}