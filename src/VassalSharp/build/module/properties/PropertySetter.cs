/*
* $Id$
*
* Copyright (c) 2006 by Rodney Kinney
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
using Constraints = VassalSharp.build.module.properties.PropertyChangerConfigurer.Constraints;
using FormattedString = VassalSharp.tools.FormattedString;
namespace VassalSharp.build.module.properties
{
	
	/// <summary> Provides a fixed value
	/// The value can be specified as a FormattedString property and evaluated at runtime
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class PropertySetter : PropertyChanger
	{
		virtual public System.String RawValue
		{
			get
			{
				return newValue;
			}
			
		}
		private System.String newValue;
		private PropertyChangerConfigurer.Constraints propSource;
		private FormattedString format;
		
		public PropertySetter(System.String newValue, PropertyChangerConfigurer.Constraints propSource)
		{
			this.newValue = newValue;
			this.propSource = propSource;
			if (propSource != null)
			{
				format = new FormattedString();
			}
		}
		
		public virtual System.String getNewValue(System.String oldValue)
		{
			System.String s = newValue;
			if (format != null)
			{
				format.Format = s;
				s = format.getText(propSource.PropertySource);
			}
			return s;
		}
		
		public virtual void  setNewValue(System.String newValue)
		{
			this.newValue = newValue;
		}
	}
}