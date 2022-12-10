/*
* $Id$
*
* Copyright (c) 2000-2006 by Rodney Kinney, Brent Easton
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
using GamePiece = VassalSharp.counters.GamePiece;
using PlaceMarker = VassalSharp.counters.PlaceMarker;
namespace VassalSharp.i18n
{
	
	/// <summary> </summary>
	/// <author>  Brent Easton
	/// 
	/// A dummy AbstractConfigurable to hold a PlaceMarker or Replace definition while translating
	/// </author>
	public class TranslatableMarker:AbstractConfigurable
	{
		private void  InitBlock()
		{
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > [0];
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > [0];
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return "Marker Definition";
			}
			
		}
		override public ComponentI18nData I18nData
		{
			get
			{
				return new ComponentI18nData(this, markerDefinition);
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
				return new System.String[0];
			}
			
		}
		
		protected internal GamePiece markerDefinition;
		
		public TranslatableMarker(PlaceMarker p)
		{
			InitBlock();
			markerDefinition = p.createBaseMarker();
			setConfigureName(p.Description);
		}
		
		public override System.String getAttributeValueString(System.String attr)
		{
			return I18nData.getLocalUntranslatedValue(attr);
		}
		
		
		public override void  setAttribute(System.String attr, System.Object value_Renamed)
		{
			
		}
		
		public override void  add(Buildable child)
		{
		}
		
		public override void  addTo(Buildable parent)
		{
			
		}
		
		public override void  build(System.Xml.XmlElement e)
		{
			
		}
		
		public override System.Xml.XmlElement getBuildElement(System.Xml.XmlDocument doc)
		{
			return null;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAllowableConfigureComponents()
		
		public override HelpFile getHelpFile()
		{
			return null;
		}
		
		public override void  removeFrom(Buildable parent)
		{
			
		}
	}
}