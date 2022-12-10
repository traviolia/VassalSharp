/*
* $Id$
*
* Copyright (c) 2000-2008 by Rodney Kinney
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
using BadDataReport = VassalSharp.build.BadDataReport;
using Resources = VassalSharp.i18n.Resources;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using FormattedString = VassalSharp.tools.FormattedString;
namespace VassalSharp.build.module.properties
{
	
	/// <summary> Increments a property by a given value.
	/// The value can be specified as a FormattedString property and evaluated at runtime
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class IncrementProperty : PropertyChanger
	{
		virtual public System.String Increment
		{
			get
			{
				return format.Format;
			}
			
		}
		protected internal IncrementProperty.Constraints constraints;
		protected internal FormattedString format = new FormattedString();
		protected internal PropertyChangerConfigurer prop;
		
		public IncrementProperty(PropertyChangerConfigurer prop, System.String incr, IncrementProperty.Constraints constraints):base()
		{
			this.prop = prop;
			this.constraints = constraints;
			format.Format = incr;
		}
		
		public virtual System.String getNewValue(System.String oldValue)
		{
			int value_Renamed = 0;
			try
			{
				if (oldValue == null || oldValue.Length == 0)
				{
					value_Renamed = 0;
				}
				else
				{
					value_Renamed = System.Int32.Parse(oldValue);
				}
			}
			catch (System.FormatException e)
			{
				ErrorDialog.dataError(new BadDataReport(Resources.getString("Error.non_number_error"), "Increment " + prop.getName() + ": oldValue " + "=" + oldValue, e));
				return oldValue;
			}
			
			try
			{
				
				int incr = System.Int32.Parse(format.getText(constraints.PropertySource));
				if (constraints.Wrap)
				{
					if (value_Renamed + incr > constraints.MaximumValue)
					{
						value_Renamed = constraints.MinimumValue + (value_Renamed + incr - constraints.MaximumValue - 1);
					}
					else if (value_Renamed + incr < constraints.MinimumValue)
					{
						value_Renamed = constraints.MaximumValue + (value_Renamed + incr - constraints.MinimumValue + 1);
					}
					else
					{
						value_Renamed += incr;
					}
				}
				else
				{
					value_Renamed += incr;
					value_Renamed = System.Math.Min(constraints.MaximumValue, value_Renamed);
					value_Renamed = System.Math.Max(constraints.MinimumValue, value_Renamed);
				}
				return System.Convert.ToString(value_Renamed);
			}
			catch (System.FormatException e)
			{
				ErrorDialog.dataError(new BadDataReport(Resources.getString("Error.non_number_error"), "Increment " + prop.getName() + ": format=" + format.Format + ", value=" + format.getText(constraints), e));
				return oldValue;
			}
		}
		
		public interface Constraints:PropertySource
		{
			int MinimumValue
			{
				get;
				
			}
			int MaximumValue
			{
				get;
				
			}
			bool Wrap
			{
				get;
				
			}
			PropertySource PropertySource
			{
				get;
				
			}
		}
	}
}