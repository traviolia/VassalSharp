/*
* $Id$
*
* Copyright (c) 2000-2003 by Brent Easton, Rodney Kinney
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
using Buildable = VassalSharp.build.Buildable;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using HelpWindow = VassalSharp.build.module.documentation.HelpWindow;
using Board = VassalSharp.build.module.map.boardPicker.Board;
using GridContainer = VassalSharp.build.module.map.boardPicker.board.mapgrid.GridContainer;
using GridNumbering = VassalSharp.build.module.map.boardPicker.board.mapgrid.GridNumbering;
using ConfigureTree = VassalSharp.configure.ConfigureTree;
using Configurer = VassalSharp.configure.Configurer;
using EditPropertiesAction = VassalSharp.configure.EditPropertiesAction;
using PropertiesWindow = VassalSharp.configure.PropertiesWindow;
using VisibilityCondition = VassalSharp.configure.VisibilityCondition;
using Resources = VassalSharp.i18n.Resources;
using AdjustableSpeedScrollPane = VassalSharp.tools.AdjustableSpeedScrollPane;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
namespace VassalSharp.build.module.map.boardPicker.board
{
	
	public class RegionGrid:AbstractConfigurable, MapGrid, ConfigureTree.Mutable
	{
		public RegionGrid()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			me = this;
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > []
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				Boolean.
			}
		}
		virtual public int FontSize
		{
			get
			{
				return fontSize;
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[]{SNAPTO, VISIBLE, FONT_SIZE};
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[]{Resources.getString("Editor.Grid.snap"), Resources.getString("Editor.IrregularGrid.draw"), Resources.getString("Editor.IrregularGrid.fonts")};
			}
			
		}
		private const long serialVersionUID = 1L;
		
		// AreaList is the table of Map areas
		// pointList is a cross-reference of points to Area names
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected Map < Point, Region > regionList = new HashMap < Point, Region >();
		protected internal GridContainer container;
		protected internal bool visible = false;
		protected internal static bool inConfig = false;
		protected internal int fontSize = 9; // Size square to display when configuring
		protected internal bool snapTo = true;
		protected internal Config regionConfigurer;
		
		protected internal GridNumbering gridNumbering;
		//UPGRADE_NOTE: The initialization of  'me' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		internal RegionGrid me;
		
		public virtual void  addRegion(Region a)
		{
			regionList.put(a.Origin, a);
			if (inConfig && regionConfigurer != null)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				regionConfigurer.view.Refresh();
			}
		}
		
		public virtual void  removeRegion(Region a)
		{
			regionList.remove(a.Origin);
		}
		
		public virtual void  removeAllRegions()
		{
			regionList.clear();
			buildComponents.clear();
		}
		
		public virtual GridNumbering getGridNumbering()
		{
			return gridNumbering;
		}
		
		public virtual void  setGridNumbering(GridNumbering gridNumbering)
		{
			this.gridNumbering = gridNumbering;
		}
		
		public const System.String SNAPTO = "snapto"; //$NON-NLS-1$
		public const System.String VISIBLE = "visible"; //$NON-NLS-1$
		public const System.String FONT_SIZE = "fontsize"; //$NON-NLS-1$
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Boolean.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		Integer.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Configurer getConfigurer()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final boolean buttonExists = config != null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final Configurer c = super.getConfigurer();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(!buttonExists)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final JButton b = new JButton(Resources.getString(Editor.IrregularGrid.define_regions)); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	b.addActionListener(new ActionListener()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void actionPerformed(ActionEvent e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		configureRegions();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	});
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	((Container) c.getControls()).add(b);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return c;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void addTo(Buildable b)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		container =(GridContainer) b;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	container.setGrid(this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void removeFrom(Buildable b)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		container.removeGrid(this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	container = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public static String getConfigureTypeName()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return Resources.getString(Editor.IrregularGrid.component_type); //$NON-NLS-1$
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
	public VassalSharp.build.module.documentation.HelpFile getHelpFile()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return HelpFile.getReferenceManualPage(IrregularGrid.htm); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getAttributeValueString(String key)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(VISIBLE.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(visible);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(FONT_SIZE.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(fontSize);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SNAPTO.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return String.valueOf(snapTo);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public VisibilityCondition getAttributeVisibility(String name)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(FONT_SIZE.equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new VisibilityCondition()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean shouldBeVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return visible;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return super.getAttributeVisibility(name);
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
		if(val == null) 
		return;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(VISIBLE.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		
		if(val instanceof Boolean)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		visible =((Boolean) val).booleanValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(val instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		visible = true.equals(val); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(FONT_SIZE.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(val instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		val = Integer.valueOf((String) val);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	fontSize =((Integer) val).intValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(SNAPTO.equals(key))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(val instanceof Boolean)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		snapTo =((Boolean) val).booleanValue();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(val instanceof String)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		snapTo = true.equals(val); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void configureRegions()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		for(Region r: regionList.values())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		r.setSelected(false);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	regionConfigurer = new Config(this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	regionConfigurer.setVisible(true);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	inConfig = true;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	// Force Regions to be drawn when configuring
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean isVisible()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return(visible || inConfig);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setVisible(boolean b)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		visible = b;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Board getBoard()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return container.getBoard();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Class < ? > [] getAllowableConfigureComponents()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new Class < ? > []
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ Region.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	class
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Point getLocation(String name) throws BadCoords
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		Region reg = findRegion(name);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(reg == null) 
	throw new BadCoords();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	else 
	return new Point(reg.getOrigin());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public int range(Point p1, Point p2)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return(int) Math.round(p1.distance(p2));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//
	// Locate nearest point
	//
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Point snapTo(Point p)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	
	//
	// Need at least one point to snap to and snapping needs to be pn.
	//
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(!snapTo || regionList.isEmpty())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return p;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	return doSnap(p);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean isLocationRestricted(Point p)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return snapTo;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//
	// Internal routine to find closest point for region name reporting
	//
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected Point doSnap(Point p)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		double distSq, minDistSq = Double.MAX_VALUE;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Point snapPoint = p;
	
	// Iterate over each grid point and determine the closest.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(Point checkPoint: regionList.keySet())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		distSq =
		(p.x - checkPoint.x) *(p.x - checkPoint.x) 
		+(p.y - checkPoint.y) *(p.y - checkPoint.y);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(distSq < minDistSq)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		minDistSq = distSq;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	snapPoint = checkPoint;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	return new Point(snapPoint);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String locationName(Point p)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		
		if(regionList.isEmpty())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	final Region region = regionList.get(doSnap(p));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return region != null ? region.getName(): null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String localizedLocationName(Point p)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		
		if(regionList.isEmpty())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	final Region region = regionList.get(doSnap(p));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return region != null ? region.getLocalizedName(): null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Return Region selected by Point</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Region getRegion(Point p)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		for(Region checkRegion: regionList.values())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(checkRegion.contains(p)) 
		return checkRegion;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Return Region by Name</summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Region findRegion(String name)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		for(Region checkRegion: regionList.values())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(checkRegion.getConfigureName().equals(name))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return checkRegion;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//
	// Get each region to draw labels and dots
	//
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void draw(
	Graphics g, 
	Rectangle bounds, 
	Rectangle visibleRect, 
	double scale, 
	boolean reversed)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(visible)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		forceDraw(g, bounds, visibleRect, scale, reversed);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void forceDraw(
	Graphics g, 
	Rectangle bounds, 
	Rectangle visibleRect, 
	double scale, 
	boolean reversed)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		
		for(Region r: regionList.values())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		r.draw(g, bounds, visibleRect, scale, reversed);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	return;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void unSelectAll()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		for(Region r: regionList.values())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		unSelect(r);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void unSelect(Region r)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		r.setSelected(false);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	[Serializable]
	public class Config:System.Windows.Forms.Form
	{
		static private System.Int32 state164;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(Config enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Config enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Config enclosingInstance;
			public Config Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs e)
			{
				e.Cancel = true;
				Enclosing_Instance.doCancel();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(Config enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Config enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Config enclosingInstance;
			public Config Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.close();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(Config enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Config enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Config enclosingInstance;
			public Config Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.doCancel();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPopupMenuListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPopupMenuListener
		{
			public AnonymousClassPopupMenuListener(Config enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Config enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Config enclosingInstance;
			public Config Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  popupMenuCanceled(System.Object event_sender, System.EventArgs evt)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Enclosing_Instance.view.Refresh();
			}
			
			public virtual void  popupMenuWillBecomeInvisible(System.Object event_sender, System.EventArgs evt)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Enclosing_Instance.view.Refresh();
			}
			
			public virtual void  popupMenuWillBecomeVisible(System.Object event_sender, System.EventArgs evt)
			{
			}
		}
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state164 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state164 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		virtual protected internal bool Dirty
		{
			set
			{
				dirty = value;
			}
			
		}
		virtual public System.Drawing.Rectangle SelectionRect
		{
			get
			{
				return selectionRect;
			}
			
		}
		virtual public System.Drawing.Rectangle SelectedBox
		{
			get
			{
				System.Drawing.Rectangle rect = System.Drawing.Rectangle.Empty;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Region r: selectedRegions)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'sel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Rectangle sel = r.SelectionRect;
					if (rect.IsEmpty)
					{
						rect = sel;
					}
					else
					{
						SupportClass.RectangleSupport.AddRectangleToRectangle(ref rect, sel);
					}
				}
				return rect;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		protected internal RegionGrid grid;
		protected internal Board board;
		
		protected internal System.Windows.Forms.Panel view;
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		protected internal System.Windows.Forms.ScrollableControl scroll;
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		protected internal System.Windows.Forms.ContextMenu myPopup;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < Region > selectedRegions = new ArrayList < Region >();
		protected internal Region lastClickedRegion = null;
		protected internal System.Drawing.Point lastClick
		{
			get
			{
				return lastClick_Renamed;
			}
			
			set
			{
				lastClick_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Point lastClick_Renamed;
		protected internal System.Drawing.Rectangle selectionRect
		{
			get
			{
				return selectionRect_Renamed;
			}
			
			set
			{
				selectionRect_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Rectangle selectionRect_Renamed = System.Drawing.Rectangle.Empty;
		protected internal System.Drawing.Point anchor
		{
			get
			{
				return anchor_Renamed;
			}
			
			set
			{
				anchor_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Point anchor_Renamed;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < Region > saveRegions;
		
		protected internal bool dirty = false;
		
		public Config(RegionGrid grid):base(Resources.getString("Editor.IrregularGrid.regions_for", grid.container.getBoard().Name))
		{ //$NON-NLS-1$
			board = grid.container.getBoard();
			this.grid = grid;
			initComponents();
			save();
		}
		
		// Main Entry Point
		protected internal virtual void  initComponents()
		{
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
			
			view = new Config.View(board, grid, this);
			
			view.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.build.module.map.boardPicker.board.Config.mouseDown);
			view.Click += new System.EventHandler(this.mouseClicked);
			view.MouseEnter += new System.EventHandler(this.mouseEntered);
			view.MouseLeave += new System.EventHandler(this.mouseExited);
			view.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mousePressed);
			view.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseReleased);
			view.MouseMove += new System.Windows.Forms.MouseEventHandler(this.mouseMoved);
			view.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.build.module.map.boardPicker.board.Config.keyDown);
			view.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyPressed);
			view.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyReleased);
			view.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyTyped);
			view.setFocusable(true);
			
			//UPGRADE_ISSUE: Field 'javax.swing.ScrollPaneConstants.VERTICAL_SCROLLBAR_ALWAYS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingScrollPaneConstants'"
			//UPGRADE_ISSUE: Field 'javax.swing.ScrollPaneConstants.HORIZONTAL_SCROLLBAR_ALWAYS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingScrollPaneConstants'"
			scroll = new AdjustableSpeedScrollPane(view, JScrollPane.VERTICAL_SCROLLBAR_ALWAYS, JScrollPane.HORIZONTAL_SCROLLBAR_ALWAYS);
			
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			scroll.Size = new System.Drawing.Size(800, 600);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(scroll);
			scroll.Dock = System.Windows.Forms.DockStyle.Fill;
			scroll.BringToFront();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'bottomPanel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box bottomPanel = Box.createVerticalBox();
			//UPGRADE_NOTE: Final was removed from the declaration of 'buttonPanel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel buttonPanel = new System.Windows.Forms.Panel();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'okButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Button okButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.OK));
			okButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(okButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonPanel.Controls.Add(okButton);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'canButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Button canButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.CANCEL));
			canButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(canButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonPanel.Controls.Add(canButton);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'mess '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label;
			temp_label = new System.Windows.Forms.Label();
			temp_label.Text = Resources.getString("Editor.IrregularGrid.drag_and_drop");
			System.Windows.Forms.Label mess = temp_label; //$NON-NLS-1$
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentY_float'"
			//UPGRADE_ISSUE: Field 'java.awt.Component.CENTER_ALIGNMENT' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentCENTER_ALIGNMENT_f'"
			mess.setAlignmentY(CENTER_ALIGNMENT);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			bottomPanel.Controls.Add(mess);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			bottomPanel.Controls.Add(buttonPanel);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(bottomPanel);
			bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			bottomPanel.SendToBack();
			
			scroll.Invalidate();
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			Refresh();
		}
		
		protected internal virtual void  doCancel()
		{
			if (dirty)
			{
				//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.YES_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				if ((int) System.Windows.Forms.DialogResult.Yes == SupportClass.OptionPaneSupport.ShowConfirmDialog(this, Resources.getString("Editor.IrregularGrid.changes_made"), "", (int) System.Windows.Forms.MessageBoxButtons.YesNo))
				{
					//$NON-NLS-1$
					restore();
					close();
				}
			}
			else
			{
				close();
			}
		}
		
		protected internal virtual void  close()
		{
			inConfig = false;
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			this.Visible = false;
		}
		
		public virtual void  init()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Region r: selectedRegions)
			{
				r.setSelected(false);
			}
		}
		
		/*
		* Clone a list of the existing regions in case we have to restore
		* after changes
		*/
		public virtual void  save()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			saveRegions = new ArrayList < Region >(grid.regionList.size());
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Region r: grid.regionList.values())
			{
				saveRegions.add(new Region(r));
			}
		}
		
		/*
		* Restore the original list of regions. Remove all existing regions,
		* then add the originals back in
		*/
		public virtual void  restore()
		{
			grid.removeAllRegions();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Region r: saveRegions)
			{
				r.addTo(grid);
				grid.add(r);
			}
		}
		
		/*
		* Scrolls the map in the containing JScrollPane
		* @param dx number of pixels to scroll horizontally
		* @param dy number of pixels to scroll vertically
		*/
		protected internal virtual void  doScroll(int dx, int dy)
		{
			System.Drawing.Rectangle temp_Rectangle;
			//UPGRADE_ISSUE: Method 'javax.swing.JViewport.getViewRect' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJViewport'"
			//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
			temp_Rectangle = scroll.getViewport().getViewRect();
			System.Drawing.Rectangle r = new System.Drawing.Rectangle(temp_Rectangle.Location, temp_Rectangle.Size);
			r.Offset(dx, dy);
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Rectangle.intersection' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			r = System.Drawing.Rectangle.Intersect(r, new System.Drawing.Rectangle(new System.Drawing.Point(0, 0), (view is VassalSharp.tools.ComponentSplitter.SplitPane || view is VassalSharp.counters.Obscurable.Ed.AnonymousClassJPanel || view is VassalSharp.counters.NonRectangular.Ed.AnonymousClassJPanel || view is VassalSharp.build.widget.PieceSlot.Panel || view is VassalSharp.build.module.map.View || view is VassalSharp.build.module.map.boardPicker.board.Config.View || view is VassalSharp.build.module.map.boardPicker.board.mapgrid.SquareGridNumbering.AnonymousClassJPanel || view is VassalSharp.build.module.map.boardPicker.board.mapgrid.HexGridNumbering.AnonymousClassJPanel || view is org.netbeans.modules.wizard.InstructionsPanel || view is org.netbeans.api.wizard.displayer.WizardDisplayerImpl.AnonymousClassJPanel)?(System.Drawing.Size) SupportClass.InvokeMethodAsVirtual(view, "getPreferredSize", new System.Object[]{}):view.Size));
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.scrollRectToVisible' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentscrollRectToVisible_javaawtRectangle'"
			view.scrollRectToVisible(r);
		}
		
		/// <summary> Scoll map so that the argument point is at least a certain distance from the visible edge</summary>
		/// <param name="evtPt">
		/// </param>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual void  scrollAtEdge(ref System.Drawing.Point evtPt, int dist)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'javax.swing.JViewport.getViewPosition' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJViewport'"
			//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
			System.Drawing.Point p = new System.Drawing.Point(evtPt.X - scroll.getViewport().getViewPosition().X, evtPt.Y - scroll.getViewport().getViewPosition().Y);
			int dx = 0, dy = 0;
			//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
			System.Drawing.Size viewSize = scroll.getViewport().Size;
			if (p.X < dist && p.X >= 0)
				dx = - 1;
			if (p.X >= viewSize.Width - dist && p.X < viewSize.Width)
				dx = 1;
			if (p.Y < dist && p.Y >= 0)
				dy = - 1;
			if (p.Y >= viewSize.Height - dist && p.Y < viewSize.Height)
				dy = 1;
			
			if (dx != 0 || dy != 0)
			{
				doScroll(2 * dist * dx, 2 * dist * dy);
			}
		}
		
		/* ------------------------------------------------------------------
		* The scrollpane client
		*/
		[Serializable]
		public class View:System.Windows.Forms.Panel, DragSourceMotionListener
		{
			private const long serialVersionUID = 1L;
			
			protected internal Board myBoard;
			protected internal RegionGrid grid;
			protected internal Config config;
			
			//UPGRADE_ISSUE: Class 'java.awt.dnd.DragSource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000'"
			//UPGRADE_ISSUE: Method 'java.awt.dnd.DragSource.getDefaultDragSource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000'"
			protected internal DragSource ds = DragSource.getDefaultDragSource();
			protected internal bool isDragging = false;
			protected internal System.Windows.Forms.Label dragCursor;
			//UPGRADE_TODO: Class 'javax.swing.JLayeredPane' was converted to 'System.Windows.Forms.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			protected internal System.Windows.Forms.Panel drawWin;
			protected internal System.Drawing.Point dragStart
			{
				get
				{
					return dragStart_Renamed;
				}
				
				set
				{
					dragStart_Renamed = value;
				}
				
			}
			protected internal System.Drawing.Point dragStart_Renamed;
			protected internal System.Drawing.Point lastDragLocation
			{
				get
				{
					return lastDragLocation_Renamed;
				}
				
				set
				{
					lastDragLocation_Renamed = value;
				}
				
			}
			protected internal System.Drawing.Point lastDragLocation_Renamed = new System.Drawing.Point(0, 0);
			protected internal System.Drawing.Point drawOffset
			{
				get
				{
					return drawOffset_Renamed;
				}
				
				set
				{
					drawOffset_Renamed = value;
				}
				
			}
			protected internal System.Drawing.Point drawOffset_Renamed = new System.Drawing.Point(0, 0);
			protected internal System.Drawing.Rectangle boundingBox
			{
				get
				{
					return boundingBox_Renamed;
				}
				
				set
				{
					boundingBox_Renamed = value;
				}
				
			}
			protected internal System.Drawing.Rectangle boundingBox_Renamed;
			protected internal int currentPieceOffsetX;
			protected internal int currentPieceOffsetY;
			protected internal int originalPieceOffsetX;
			protected internal int originalPieceOffsetY;
			
			public View(Board b, RegionGrid grid, Config config)
			{
				myBoard = b;
				this.grid = grid;
				this.config = config;
				this.AllowDrop = true;
				this.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragEnter_renamed);
				this.DragOver += new System.Windows.Forms.DragEventHandler(this.dragOver_renamed);
				this.DragLeave += new System.EventHandler(this.dragExit_renamed);
				this.DragDrop += new System.Windows.Forms.DragEventHandler(this.drop_renamed);
				//UPGRADE_ISSUE: Constructor 'java.awt.dnd.DropTarget.DropTarget' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTarget'"
				new DropTarget(this, (int) System.Windows.Forms.DragDropEffects.Move, this);
				this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.dragGestureRecognized);
				this.DragEnter += new System.Windows.Forms.DragEventHandler(this.dragEnter);
				this.DragOver += new System.Windows.Forms.DragEventHandler(this.dragOver);
				this.DragLeave += new System.EventHandler(this.dragExit);
				this.DragDrop += new System.Windows.Forms.DragEventHandler(this.dragDropEnd);
				setFocusTraversalKeysEnabled(false);
			}
			
			protected override void  OnPaint(System.Windows.Forms.PaintEventArgs g_EventArg)
			{
				System.Drawing.Graphics g = null;
				if (g_EventArg != null)
					g = g_EventArg.Graphics;
				//UPGRADE_TODO: Method 'javax.swing.JComponent.getVisibleRect' was converted to 'System.Windows.Forms.Control.DisplayRectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentgetVisibleRect'"
				System.Drawing.Rectangle b = DisplayRectangle;
				g.FillRegion(new System.Drawing.SolidBrush(SupportClass.GraphicsManager.manager.GetBackColor(g)), new System.Drawing.Region(new System.Drawing.Rectangle(b.X, b.Y, b.Width, b.Height)));
				myBoard.draw(g, 0, 0, 1.0, this);
				//UPGRADE_NOTE: Final was removed from the declaration of 'bounds '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle bounds = new Rectangle(new System.Drawing.Point(0, 0), myBoard.bounds().getSize());
				grid.forceDraw(g, bounds, bounds, 1.0, false);
				//UPGRADE_NOTE: Final was removed from the declaration of 'selection '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle selection = config.SelectionRect;
				if (!selection.IsEmpty)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'g2d '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Graphics g2d = (System.Drawing.Graphics) g;
					//UPGRADE_NOTE: Final was removed from the declaration of 'str '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.getStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DgetStroke'"
					System.Drawing.Pen str = SupportClass.GraphicsManager.manager.GetPen(g2d);
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.setStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DsetStroke_javaawtStroke'"
					//UPGRADE_TODO: Constructor 'java.awt.BasicStroke.BasicStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtBasicStrokeBasicStroke_float'"
					SupportClass.GraphicsManager.manager.SetPen(g2d, new System.Drawing.Pen(System.Drawing.Brushes.Black, 2.0f));
					g2d.setColor(Color.RED);
					g2d.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g2d), selection.X, selection.Y, selection.Width, selection.Height);
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.setStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DsetStroke_javaawtStroke'"
					SupportClass.GraphicsManager.manager.SetPen(g2d, str);
				}
			}
			
			//UPGRADE_WARNING: The equivalent to the 'javax.swing.JComponent.update' method has fewer parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1265'"
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			new virtual public void  Update()
			{
				// To avoid flicker, don't clear the display first *
				OnPaint(new System.Windows.Forms.PaintEventArgs(g, Bounds));
			}
			
			//UPGRADE_NOTE: The equivalent of method 'javax.swing.JComponent.getPreferredSize' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public System.Drawing.Size getPreferredSize()
			{
				return new Dimension(myBoard.bounds().width, myBoard.bounds().height);
			}
			
			public virtual void  dragEnter_renamed(System.Object event_sender, System.Windows.Forms.DragEventArgs arg0)
			{
				arg0.Effect = System.Windows.Forms.DragDropEffects.All;
			}
			
			//UPGRADE_ISSUE: Class 'java.awt.dnd.DropTargetEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTargetEvent'"
			public virtual void  dragExit_renamed(System.Object event_sender, System.EventArgs arg0)
			{
			}
			
			public virtual void  dragOver_renamed(System.Object event_sender, System.Windows.Forms.DragEventArgs arg0)
			{
			}
			
			public virtual void  drop_renamed(System.Object event_sender, System.Windows.Forms.DragEventArgs event_Renamed)
			{
				removeDragCursor();
				//UPGRADE_NOTE: Final was removed from the declaration of 'dragEnd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point dragEnd = SupportClass.GetLocation(event_Renamed);
				int x = dragEnd.X - dragStart.X;
				int y = dragEnd.Y - dragStart.Y;
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Region r: config.selectedRegions)
				{
					r.move(x, y, this);
					config.Dirty = true;
				}
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Refresh();
			}
			
			public virtual void  dropActionChanged(System.Object event_sender, System.Windows.Forms.DragEventArgs arg0)
			{
			}
			
			public virtual void  dragGestureRecognized(System.Object event_sender, System.Windows.Forms.MouseEventArgs dge)
			{
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'mousePosition '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point mousePosition = new System.Drawing.Point(dge.X, dge.Y);
				dragStart = new System.Drawing.Point(new System.Drawing.Size(mousePosition));
				//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Region r = grid.getRegion(mousePosition);
				if (r == null)
				{
					return ;
				}
				
				System.Drawing.Point piecePosition = new System.Drawing.Point(new System.Drawing.Size(r.Origin));
				
				originalPieceOffsetX = piecePosition.X - mousePosition.X;
				originalPieceOffsetY = piecePosition.Y - mousePosition.Y;
				
				drawWin = null;
				
				makeDragCursor();
				setDragCursor();
				
				SupportClass.SwingUtilsSupport.PointToScreen(ref drawOffset, drawWin);
				SupportClass.SwingUtilsSupport.PointToScreen(ref mousePosition, drawWin);
				moveDragCursor(mousePosition.X, mousePosition.Y);
				
				// begin dragging
				try
				{
					((System.Windows.Forms.Control) event_sender).DoDragDrop(new System.Windows.Forms.DataObject(""), System.Windows.Forms.DragDropEffects.All); //$NON-NLS-1$
					//UPGRADE_ISSUE: Method 'java.awt.dnd.DragGestureEvent.getDragSource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDragGestureEventgetDragSource'"
					dge.getDragSource().addDragSourceMotionListener(this);
				}
				catch (System.Exception e)
				{
					ErrorDialog.bug(e);
				}
			}
			
			public virtual void  dragDropEnd(System.Object event_sender, System.Windows.Forms.DragEventArgs arg0)
			{
				removeDragCursor();
				return ;
			}
			
			public virtual void  dragEnter(System.Object event_sender, System.Windows.Forms.DragEventArgs arg0)
			{
			}
			
			public virtual void  dragExit(System.Object event_sender, System.EventArgs arg0)
			{
			}
			
			public virtual void  dragOver(System.Object event_sender, System.Windows.Forms.DragEventArgs arg0)
			{
			}
			
			public virtual void  dropActionChanged(System.Object event_sender, System.Windows.Forms.DragEventArgs arg0)
			{
			}
			
			public virtual void  dragMouseMoved(System.Object event_sender, System.Windows.Forms.DragEventArgs event_Renamed)
			{
				if (!event_Renamed.getLocation().equals(lastDragLocation))
				{
					lastDragLocation = event_Renamed.getLocation();
					moveDragCursor(event_Renamed.getX(), event_Renamed.getY());
					//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
					if (dragCursor != null && !dragCursor.Visible)
					{
						dragCursor.Visible = true;
					}
				}
			}
			
			private void  removeDragCursor()
			{
				if (drawWin != null)
				{
					if (dragCursor != null)
					{
						dragCursor.Visible = false;
						drawWin.Controls.Remove(dragCursor);
					}
					drawWin = null;
				}
			}
			
			/// <summary>Moves the drag cursor on the current draw window </summary>
			protected internal virtual void  moveDragCursor(int dragX, int dragY)
			{
				if (drawWin != null)
				{
					//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_int_int'"
					dragCursor.Location = new System.Drawing.Point(dragX - drawOffset.X, dragY - drawOffset.Y);
				}
			}
			
			protected internal virtual void  setDragCursor()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'rootWin '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getRootPane' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetRootPane_javaawtComponent'"
				System.Windows.Forms.ContainerControl rootWin = (System.Windows.Forms.ContainerControl) this.Parent;
				if (rootWin != null)
				{
					// remove cursor from old window
					if (dragCursor.Parent != null)
					{
						dragCursor.Parent.Controls.Remove(dragCursor);
					}
					//UPGRADE_ISSUE: Method 'javax.swing.JRootPane.getLayeredPane' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJRootPanegetLayeredPane'"
					drawWin = rootWin.getLayeredPane();
					
					dragCursor.Visible = true;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					drawWin.Controls.Add(dragCursor);
					dragCursor.Dock = new System.Windows.Forms.DockStyle();
					dragCursor.BringToFront();
				}
			}
			
			private void  makeDragCursor()
			{
				// FIXME: make this an ImageOp?
				// create the cursor if necessary
				if (dragCursor == null)
				{
					dragCursor = new System.Windows.Forms.Label();
					dragCursor.Visible = false;
				}
				
				currentPieceOffsetX = originalPieceOffsetX;
				currentPieceOffsetY = originalPieceOffsetY;
				
				// Record sizing info and resize our cursor
				boundingBox = config.SelectedBox;
				drawOffset = new System.Drawing.Point(dragStart.X - boundingBox.X, dragStart.Y - boundingBox.Y);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'cursorImage '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Bitmap cursorImage = ImageUtils.createCompatibleTranslucentImage(boundingBox.Width, boundingBox.Height);
				//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(cursorImage);
				
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetComposite_javaawtComposite'"
				//UPGRADE_ISSUE: Method 'java.awt.AlphaComposite.getInstance' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
				//UPGRADE_ISSUE: Field 'java.awt.AlphaComposite.SRC_OVER' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
				g.setComposite(AlphaComposite.getInstance(AlphaComposite.SRC_OVER, 0.5f));
				
				// Draw each region into the drag cursor in the correct place
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Region r: config.selectedRegions)
				{
					int x = (- boundingBox.X) * 2;
					int y = (- boundingBox.Y) * 2;
					//UPGRADE_TODO: Method 'javax.swing.JComponent.getVisibleRect' was converted to 'System.Windows.Forms.Control.DisplayRectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentgetVisibleRect'"
					r.draw(g, boundingBox, DisplayRectangle, 1.0f, false, x, y);
				}
				
				g.Dispose();
				
				//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
				dragCursor.Size = new System.Drawing.Size(boundingBox.Width, boundingBox.Height);
				
				// store the bitmap in the cursor
				dragCursor.Image = (System.Drawing.Image) cursorImage.Clone();
			}
		}
		/* ------------------------------------------------------------------
		* End View
		*/
		
		/*
		* Mouse Listeners
		*/
		
		// Mouse clicked, see if it is on a Region Point
		public virtual void  mouseClicked(System.Object event_sender, System.EventArgs e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
			System.Drawing.Point p = e.getPoint();
			lastClick = p;
			
			//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			if (e.isMetaDown())
			{
				// Right click...menu
				doPopupMenu(event_sender, e);
			}
			else if (lastClickedRegion != null)
			{
				
				//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getClickCount' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetClickCount'"
				if (e.getClickCount() >= 2)
				{
					// Double click show properties
					if (lastClickedRegion.Configurer != null)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						SupportClass.ActionSupport a = new EditPropertiesAction(lastClickedRegion, null, this);
						if (a != null)
						{
							//UPGRADE_TODO: Method 'java.awt.event.ActionListener.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
							System.Object generatedAux = event_sender;
							a.actionPerformed(new System.EventArgs()); //$NON-NLS-1$
						}
					}
				}
			}
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			view.Refresh(); // Clean up selection
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'ADD_REGION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'ADD_REGION' was moved to static method 'VassalSharp.build.module.map.boardPicker.board.Config'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String ADD_REGION; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'DELETE_REGION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'DELETE_REGION' was moved to static method 'VassalSharp.build.module.map.boardPicker.board.Config'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String DELETE_REGION; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'PROPERTIES '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'PROPERTIES' was moved to static method 'VassalSharp.build.module.map.boardPicker.board.Config'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String PROPERTIES; //$NON-NLS-1$
		
		protected internal virtual void  doPopupMenu(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			myPopup = new System.Windows.Forms.ContextMenu();
			
			System.Windows.Forms.MenuItem menuItem = new System.Windows.Forms.MenuItem(ADD_REGION);
			menuItem.Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(menuItem);
			menuItem.Enabled = lastClickedRegion == null;
			myPopup.MenuItems.Add(menuItem);
			
			menuItem = new System.Windows.Forms.MenuItem();
			menuItem.Text = DELETE_REGION;
			menuItem.Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(menuItem);
			menuItem.Enabled = lastClickedRegion != null;
			
			myPopup.MenuItems.Add(menuItem);
			
			myPopup.MenuItems.Add("-");
			
			menuItem = new System.Windows.Forms.MenuItem();
			menuItem.Text = PROPERTIES;
			menuItem.Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(menuItem);
			menuItem.Enabled = lastClickedRegion != null;
			myPopup.MenuItems.Add(menuItem);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
			System.Drawing.Point p = e.getPoint();
			
			//UPGRADE_NOTE: Some methods of the 'javax.swing.event.PopupMenuListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			myPopup.Popup += new System.EventHandler(new AnonymousClassPopupMenuListener(this).popupMenuWillBecomeVisible);
			//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.show' was converted to 'System.Windows.Forms.ContextMenu.Show' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenushow_javaawtComponent_int_int'"
			myPopup.Show(((System.Windows.Forms.Control) event_sender), new System.Drawing.Point(p.X, p.Y));
		}
		
		public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'command '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String command = SupportClass.CommandManager.GetCommand(event_sender);
			
			if (command.Equals("close"))
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				this.Visible = false;
			}
			else if (command.Equals("showhide"))
			{
				grid.setVisible(!grid.Visible);
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				view.Refresh();
			}
			else if (command.Equals(ADD_REGION))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				Region r = new Region(ref lastClick);
				r.addTo(grid);
				grid.add(r);
				select(r);
				lastClickedRegion = r;
				Dirty = true;
				//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SupportClass.ActionSupport a = new EditPropertiesAction(lastClickedRegion, null, this);
				if (a != null)
				{
					//UPGRADE_TODO: Method 'java.awt.event.ActionListener.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					System.Object generatedAux = event_sender;
					a.actionPerformed(new System.EventArgs()); //$NON-NLS-1$
				}
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				view.Refresh();
			}
			else if (command.Equals(DELETE_REGION))
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Region r: selectedRegions)
				{
					r.removeFrom(grid);
					grid.remove(r);
					lastClickedRegion = null;
					Dirty = true;
				}
				selectedRegions.clear();
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				view.Refresh();
			}
			else if (command.Equals(PROPERTIES))
			{
				//$NON-NLS-1$
				if (lastClickedRegion != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SupportClass.ActionSupport a = new EditRegionAction(lastClickedRegion, null, this);
					if (a != null)
					{
						//UPGRADE_TODO: Method 'java.awt.event.ActionListener.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
						System.Object generatedAux3 = event_sender;
						a.actionPerformed(new System.EventArgs()); //$NON-NLS-1$
					}
				}
			}
		}
		
		/*
		* Version of EditProperties Action that repaints it's owning frame
		*/
		[Serializable]
		protected internal class EditRegionAction:EditPropertiesAction
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassWindowAdapter1
			{
				public AnonymousClassWindowAdapter1(EditRegionAction enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(EditRegionAction enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private EditRegionAction enclosingInstance;
				public EditRegionAction Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public void  windowClosed(System.Object event_sender, System.EventArgs e)
				{
					openWindows.remove(Enclosing_Instance.target);
					Enclosing_Instance.owner.Dirty = !Enclosing_Instance.region.Name.Equals(Enclosing_Instance.origRegion.Name) || !Enclosing_Instance.region.Origin.Equals(Enclosing_Instance.origRegion.Origin);
					//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
					Enclosing_Instance.owner.Refresh();
				}
			}
			
			internal Config owner;
			internal Region origRegion;
			internal Region region;
			
			private const long serialVersionUID = 1L;
			
			public EditRegionAction(Region target, HelpWindow helpWindow, Config dialogOwner):base(target, helpWindow, dialogOwner)
			{
				owner = dialogOwner;
				origRegion = new Region(target);
				region = target;
			}
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				PropertiesWindow w = openWindows.get_Renamed(target);
				if (w == null)
				{
					w = new PropertiesWindow(dialogOwner, false, target, helpWindow);
					//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
					w.Closed += new System.EventHandler(new AnonymousClassWindowAdapter1(this).windowClosed);
					openWindows.put(target, w);
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					w.Visible = true;
				}
				w.BringToFront();
			}
		}
		
		protected internal virtual void  select(Region r)
		{
			r.setSelected(true);
			if (!selectedRegions.contains(r))
			{
				selectedRegions.add(r);
			}
			view.Invalidate(r.SelectionRect);
		}
		
		protected internal virtual void  unselect(Region r)
		{
			if (r != null)
			{
				r.setSelected(false);
				selectedRegions.remove(r);
				if (lastClickedRegion == r)
				{
					lastClickedRegion = null;
				}
				view.Invalidate(r.SelectionRect);
			}
		}
		
		protected internal virtual void  unSelectAll()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Region r: selectedRegions)
			{
				r.setSelected(false);
				view.repaint(r.SelectionRect);
			}
			selectedRegions.clear();
		}
		
		public virtual void  mouseEntered(System.Object event_sender, System.EventArgs evPt)
		{
		}
		
		public virtual void  mouseExited(System.Object event_sender, System.EventArgs evPt)
		{
		}
		
		public virtual void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Point p = new System.Drawing.Point(e.X, e.Y);
			lastClick = p;
			lastClickedRegion = grid.getRegion(p);
			
			if (!(System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift) && !(System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control) && (lastClickedRegion == null || !lastClickedRegion.isSelected()))
			{
				unSelectAll();
			}
			
			if (lastClickedRegion == null)
			{
				anchor = p;
				selectionRect = new System.Drawing.Rectangle(anchor.X, anchor.Y, 0, 0);
			}
			else
			{
				if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control))
				{
					unselect(lastClickedRegion);
				}
				else
				{
					select(lastClickedRegion);
				}
			}
		}
		
		public virtual void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs evPt)
		{
			if (!selectionRect.IsEmpty)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Region r: grid.regionList.values())
				{
					if (selectionRect.contains(r.getOrigin()))
					{
						if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control))
						{
							unselect(r);
						}
						else
						{
							select(r);
						}
					}
				}
				selectionRect = System.Drawing.Rectangle.Empty;
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				view.Refresh();
			}
		}
		
		public virtual void  mouseMoved(System.Object event_sender, System.Windows.Forms.MouseEventArgs evPt)
		{
		}
		
		// Scroll map if necessary
		public virtual void  mouseDragged(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			if (!e.isMetaDown())
			{
				System.Drawing.Point tempAux = new System.Drawing.Point(e.X, e.Y);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				scrollAtEdge(ref tempAux, 15);
			}
			
			if (!selectionRect.IsEmpty)
			{
				// FIXME: inefficient, could be done with only one new Rectangle
				//UPGRADE_NOTE: Final was removed from the declaration of 'repaintRect '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle repaintRect = new System.Drawing.Rectangle(selectionRect.X - 1, selectionRect.Y - 1, selectionRect.Width + 3, selectionRect.Height + 3);
				
				selectionRect.X = System.Math.Min(e.X, anchor.X);
				selectionRect.Y = System.Math.Min(e.Y, anchor.Y);
				selectionRect.Width = System.Math.Abs(e.X - anchor.X);
				selectionRect.Height = System.Math.Abs(e.Y - anchor.Y);
				
				SupportClass.RectangleSupport.AddRectangleToRectangle(ref repaintRect, new System.Drawing.Rectangle(selectionRect.X - 1, selectionRect.Y - 1, selectionRect.Width + 3, selectionRect.Height + 3));
				view.Invalidate(repaintRect);
			}
		}
		
		public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			
			/*
			* Pass key onto window scroller if no region selected
			* or control key not used.
			*/
			if (selectedRegions.isEmpty() || !(System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control))
				return ;
			
			int dx = 0, dy = 0, delta = 1;
			
			if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift))
			{
				delta = 5;
			}
			
			//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyCode' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyCode'"
			switch (e.KeyValue)
			{
				
				case (int) System.Windows.Forms.Keys.Up: 
					dy = - delta;
					break;
				
				case (int) System.Windows.Forms.Keys.Down: 
					dy = delta;
					break;
				
				case (int) System.Windows.Forms.Keys.Left: 
					dx = - delta;
					break;
				
				case (int) System.Windows.Forms.Keys.Right: 
					dx = delta;
					break;
				
				default: 
					return ;
				
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Region r: selectedRegions)
			{
				r.move(dx, dy, view);
			}
			
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			view.Refresh();
			e.Handled = true;
		}
		
		public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
		}
		
		public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		}
		static Config()
		{
			ADD_REGION = Resources.getString("Editor.IrregularGrid.add_region");
			DELETE_REGION = Resources.getString("Editor.IrregularGrid.delete_region");
			PROPERTIES = Resources.getString("Editor.ModuleEditor.properties");
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}