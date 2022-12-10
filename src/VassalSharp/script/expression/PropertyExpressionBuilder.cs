/*
* $Id$
*
* Copyright (c) 2008-2009 by Brent Easton
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
using Configurer = VassalSharp.configure.Configurer;
using EditablePiece = VassalSharp.counters.EditablePiece;
using PropertiesPieceFilter = VassalSharp.counters.PropertiesPieceFilter;
namespace VassalSharp.script.expression
{
	
	[Serializable]
	public class PropertyExpressionBuilder:ExpressionBuilder
	{
		private const long serialVersionUID = 1L;
		
		public PropertyExpressionBuilder(Configurer c, System.Windows.Forms.Form parent):base(c, parent)
		{
		}
		
		public PropertyExpressionBuilder(Configurer c, System.Windows.Forms.Form parent, EditablePiece piece):base(c, parent, piece)
		{
		}
		
		/// <summary> Convert an old-style Property Match Expression to a BeanShell Expression</summary>
		/// <param name="s">Old-style string
		/// </param>
		/// <returns> expression
		/// </returns>
		public override System.String convert(System.String s)
		{
			return PropertiesPieceFilter.toBeanShellString(s);
		}
	}
}