/*
* $Id: SinglePropertyExpression.java 7725 2011-07-31 18:51:43Z uckelman $
*
* Copyright (c) 2013 Brent Easton
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
using PropertySource = VassalSharp.build.module.properties.PropertySource;
namespace VassalSharp.script.expression
{
	
	/// <summary> An expression consisting of a single property name
	/// 
	/// </summary>
	public class SinglePropertyExpression:Expression
	{
		private void  InitBlock()
		{
			System.String value_Renamed = null;
			try
			{
				if (properties != null)
				{
					value_Renamed = properties.get_Renamed(getExpression());
				}
				if (value_Renamed == null && ps != null)
				{
					if (localized)
					{
						value_Renamed = ((System.String) ps.getLocalizedProperty(getExpression()));
					}
					else
					{
						value_Renamed = ((System.String) ps.getProperty(getExpression()));
					}
				}
			}
			catch (System.Exception ex)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new ExpressionException(getExpression(), ex.Message);
			}
			return value_Renamed == null?"":value_Renamed;
		}
		
		public SinglePropertyExpression(System.String ex)
		{
			InitBlock();
			if (ex.StartsWith("$") && ex.EndsWith("$"))
			{
				setExpression(ex.Substring(1, (ex.Length - 1) - (1)));
			}
			else
			{
				setExpression(ex);
			}
		}
		
		public System.String evaluate;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(PropertySource ps, Map < String, String > properties, boolean localized) 
		throws ExpressionException
		
		public override System.String toBeanShellString()
		{
			return BeanShellExpression.convertProperty(getExpression());
		}
	}
}