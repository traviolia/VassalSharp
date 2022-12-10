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
using PieceFilter = VassalSharp.counters.PieceFilter;
using PropertiesPieceFilter = VassalSharp.counters.PropertiesPieceFilter;
using FormattedString = VassalSharp.tools.FormattedString;
namespace VassalSharp.script.expression
{
	
	/// <summary> An old-style Property Match expression.</summary>
	public class PropertyMatchExpression:Expression
	{
		private void  InitBlock()
		{
			return null;
		}
		virtual protected internal bool Dynamic
		{
			get
			{
				return getExpression() != null && getExpression().IndexOf('$') >= 0;
			}
			
		}
		
		protected internal PieceFilter filter;
		
		public PropertyMatchExpression(System.String s)
		{
			InitBlock();
			setExpression(s);
		}
		
		public System.String evaluate;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(PropertySource ps, Map < String, String > properties, 
		boolean localized)
		
		public override PieceFilter getFilter(PropertySource ps)
		{
			if (filter == null || Dynamic)
			{
				filter = PropertiesPieceFilter.parse(new FormattedString(getExpression()).getText(ps));
			}
			return filter;
		}
		
		public override System.String toBeanShellString()
		{
			return PropertiesPieceFilter.toBeanShellString(getExpression());
		}
	}
}