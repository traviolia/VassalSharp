/*
 * Copyright (c) 2007 by Rodney Kinney, Brent Easton
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

namespace VassalSharp.i18n
{
	
	/// <summary>
	/// Container for definitions of Translations.
	/// </summary>
	public class Language : AbstractConfigurable
	{
		public Language()
		{
		}

		override public string[] AttributeDescriptions
		{
			get
			{
				return new string[0];
			}
			
		}
		override public string[] AttributeNames
		{
			get
			{
				return new string[0];
			}
			
		}
		virtual protected internal string Description
		{
			get
			{
				return "";
			}
			
		}
		
		public override Type [] getAttributeTypes()
		{
			return new Type[0];
		}

		public override string getAttributeValueString(string key)
		{
			return null;
		}
		
		public override void  SetAttribute(string key, System.Object value_Renamed)
		{
		}
		
		public override void  addTo(Buildable parent)
		{
		}

		public override Type[] AllowableConfigureComponents
		{
			get
			{
				return new Type[] { typeof(Translation) };
			}
		}

		public static String getConfigureTypeName()
		{ 
			return "Translations";
		}
	
		public HelpFile getHelpFile()
		{ 
			return null;
		}
	
		public override void removeFrom(Buildable parent)
		{
		}
	}
}