/*
* $Id$
*
* Copyright (c) 2000-2012 by Rodney Kinney, Brent Easton
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
namespace VassalSharp.configure
{
	
	/// <summary> A Configurer for Integer values</summary>
	public class IntConfigurer : StringConfigurer
	{
		override public string ValueString
		{
			get
			{
				return value_Renamed == null?null:value_Renamed.ToString();
			}
			
		}
		public IntConfigurer(string key, string name) : this(key, name, 0)
		{
		}
		
		public IntConfigurer(string key, string name, int? val) : base(key, name, 4)
		{
			if (val != null)
			{
				setValue((System.Object) val.Value);
			}
		}
		
		public override void  setValue(string s)
		{
			int? i = null;
			try
			{
				i = int.Parse(s);
			}
			catch (System.FormatException e)
			{
				i = null;
			}
			if (i != null)
			{
				setValue((System.Object) i.Value);
			}
		}
		
		public virtual int getIntValue(int defaultValue)
		{
			if (getValue() is int)
			{
				return (int) getValue();
			}
			else
			{
				return defaultValue;
			}
		}
		
		public override void  setValue(System.Object o)
		{
			if (!noUpdate && nameField != null && o != null)
			{
				nameField.Text = o.ToString();
			}
			base.setValue(o);
		}
	}
}