/*
* $Id$
*
* Copyright (c) 2005 by Rodney Kinney, Brent Easton
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
using BadDataReport = VassalSharp.build.BadDataReport;
using Buildable = VassalSharp.build.Buildable;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Resources = VassalSharp.i18n.Resources;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
namespace VassalSharp.build.module.turn
{
	
	/// <summary> Generic Turn Component</summary>
	public class TurnComponent:AbstractConfigurable
	{
		public TurnComponent()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			return levels.iterator();
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > [0];
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > [0];
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[0];
			}
			
		}
		virtual protected internal int TurnLevelCount
		{
			get
			{
				return levels.size();
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[0];
			}
			
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < TurnLevel > levels = new ArrayList < TurnLevel >();
		
		public virtual void  addLevel(TurnLevel t)
		{
			levels.add(t);
		}
		
		public virtual void  removeLevel(TurnLevel t)
		{
			levels.remove(t);
		}
		
		protected internal virtual TurnLevel getTurnLevel(int i)
		{
			if (levels.size() == 0)
			{
				return null;
			}
			else
			{
				if (i >= levels.size())
				{
					ErrorDialog.dataError(new BadDataReport(Resources.getString("TurnTracker.level_error", getConfigureName(), getConfigureName(), i, levels.size()), getConfigureName()));
					return levels.get_Renamed(levels.size() - 1);
				}
			}
			return levels.get_Renamed(i);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < TurnLevel > getTurnLevels()
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		
		public override void  setAttribute(System.String key, System.Object value_Renamed)
		{
			
		}
		
		public override System.String getAttributeValueString(System.String key)
		{
			return null;
		}
		
		public override void  removeFrom(Buildable parent)
		{
		}
		
		public override HelpFile getHelpFile()
		{
			return null;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAllowableConfigureComponents()
		
		public override void  addTo(Buildable parent)
		{
		}
	}
}