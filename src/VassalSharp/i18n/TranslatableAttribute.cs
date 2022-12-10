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

namespace VassalSharp.i18n
{
	
	/// <summary> A Class representing an Attribute of a Component that can be translated.</summary>
	public class TranslatableAttribute
	{
		virtual public string UntranslatedValue
		{
			get
			{
				return originalValue;
			}
			
		}
		virtual public string Key
		{
			get
			{
				return component.I18nData.FullPrefix + attributeName; //$NON-NLS-1$
			}
			
		}
		virtual public bool Translatable
		{
			get
			{
				return component.I18nData.isAttributeTranslatable(attributeName);
			}
			
		}
		internal Translatable component;
		internal string attributeName;
		internal string originalValue;
		
		public TranslatableAttribute(Translatable t, string name, string value_Renamed)
		{
			component = t;
			attributeName = name;
			originalValue = value_Renamed;
		}
		
		public virtual void  applyTranslation(string translation)
		{
			if (translation != null && translation.Length > 0)
			{
				component.I18nData.applyTranslation(attributeName, translation);
			}
		}
	}
}