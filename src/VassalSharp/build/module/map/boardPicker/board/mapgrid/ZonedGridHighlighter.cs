/*
* $Id$
*
* Copyright (c) 2000-2006 by Brent Easton, Rodney Kinney
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
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using Buildable = VassalSharp.build.Buildable;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using ZonedGrid = VassalSharp.build.module.map.boardPicker.board.ZonedGrid;
namespace VassalSharp.build.module.map.boardPicker.board.mapgrid
{
	
	/// <summary> </summary>
	/// <author>  Brent Easton
	/// A Container class for Zone Highlighters.
	/// 
	/// </author>
	public class ZonedGridHighlighter:AbstractConfigurable
	{
		private void  InitBlock()
		{
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > [0];
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > []
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ZoneHighlight.
			}
		}
		virtual public System.String Name
		{
			get
			{
				return name;
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[0];
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[0];
			}
			
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return "Zone Highlighters";
			}
			
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < ZoneHighlight > highlightList;
		protected internal System.String currentColorName;
		protected internal System.Drawing.Color currentColor;
		
		public ZonedGridHighlighter()
		{
			InitBlock();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			highlightList = new ArrayList < ZoneHighlight >();
		}
		
		public virtual void  addHighlight(ZoneHighlight h)
		{
			highlightList.add(h);
		}
		
		public virtual void  removeHighlight(ZoneHighlight h)
		{
			highlightList.remove(h);
		}
		
		public virtual ZoneHighlight getZoneHighlightByName(System.String highlightName)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(ZoneHighlight h: highlightList)
			{
				if (h.Name.equals(highlightName))
				{
					return h;
				}
			}
			return null;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		
		
		public override void  addTo(Buildable b)
		{
			((ZonedGrid) b).setZoneHighlighter(this);
		}
		
		public override void  removeFrom(Buildable b)
		{
			((ZonedGrid) b).setZoneHighlighter(null);
		}
		
		public override VassalSharp.build.module.documentation.HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("ZonedGrid.htm", "ZoneHighlighter");
		}
		
		public override System.String getAttributeValueString(System.String key)
		{
			return null;
		}
		
		public override void  setAttribute(System.String key, System.Object val)
		{
			
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAllowableConfigureComponents()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}