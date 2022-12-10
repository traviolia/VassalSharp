/*
* $Id$
*
* Copyright (c) 2004-2009 by Brent Easton, Rodney Kinney
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
using ComponentI18nData = VassalSharp.i18n.ComponentI18nData;
namespace VassalSharp.script
{
	
	public abstract class AbstractScript:AbstractConfigurable
	{
		public AbstractScript()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			return new System.Type[0]; return new System.Type[0];
		}
		virtual public System.String Description
		{
			get
			{
				return description;
			}
			
			set
			{
				this.description = value;
			}
			
		}
		virtual public System.String Script
		{
			get
			{
				return script;
			}
			
			set
			{
				this.script = value;
			}
			
		}
		override public ComponentI18nData I18nData
		{
			get
			{
				ComponentI18nData data = base.I18nData;
				data.setAllAttributesUntranslatable();
				return data;
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[0];
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[]{NAME, DESC, SCRIPT};
			}
			
		}
		
		public const System.String NAME = "name";
		public const System.String DESC = "desc";
		public const System.String SCRIPT = "script";
		
		private System.String description = "";
		private System.String script = "";
		
		public abstract CompileResult compile();
		
		/* --------------------------------------------
		* AbstractConfigurable implementation
		* Minimal due to custom Configurer
		*/
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAllowableConfigureComponents()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.String getAttributeValueString(System.String key)
		{
			if (NAME.Equals(key))
			{
				return getConfigureName();
			}
			else if (DESC.Equals(key))
			{
				return Description;
			}
			else if (SCRIPT.Equals(key))
			{
				return Script;
			}
			return null;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override void  setAttribute(System.String key, System.Object value_Renamed)
		{
			if (NAME.Equals(key))
			{
				setConfigureName((System.String) value_Renamed);
			}
			else if (DESC.Equals(key))
			{
				Description = ((System.String) value_Renamed);
			}
			else if (SCRIPT.Equals(key))
			{
				Script = ((System.String) value_Renamed);
			}
			return ;
		}
	}
}