/*
* $Id$
*
* Copyright (c) 2008 by Joel Uckelman
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
	
	/// <summary> A Configurer for Long values
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class LongConfigurer : StringConfigurer
	{
		override public string ValueString
		{
			get
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return value_Renamed == null?null:value_Renamed.ToString();
			}
			
		}
		public LongConfigurer(string key, string name) : this(key, name, 0L)
		{
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public LongConfigurer(string key, string name, long? val) : base(key, name)
		{
			if (val != null)
			{
				setValue((System.Object) val.Value);
			}
		}
		
		public override void  setValue(string s)
		{
			//UPGRADE_TODO: The 'System.Int64' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
			long? l = null;
			try
			{
				l = long.Parse(s);
			}
			catch (System.FormatException e)
			{
				l = null;
			}
			if (l != null)
			{
				setValue((System.Object) l.Value);
			}
		}
		
		public virtual long getLongValue(long defaultValue)
		{
			if (getValue() is long)
			{
				return (long) getValue();
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