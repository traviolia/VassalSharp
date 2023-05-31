/*
* $Id$
*
* Copyright (c) 2009 Brent Easton
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
	
	/// <summary> An expression consisting of an Integer only
	/// 
	/// </summary>
	public class IntExpression:Expression
	{
		private void  InitBlock()
		{
			return getExpression();
		}
		
		public IntExpression(int i)
		{
			InitBlock();
			try
			{
				setExpression(System.Convert.ToString(i));
			}
			catch (System.FormatException e)
			{
				setExpression("0");
			}
		}
		
		public System.String evaluate;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(PropertySource ps, Map < String, String > properties, 
		boolean localized)
		
		public override System.String toBeanShellString()
		{
			return getExpression();
		}
	}
}