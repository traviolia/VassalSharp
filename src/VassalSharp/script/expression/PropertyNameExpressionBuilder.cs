/*
* $Id: PropertyNameExpressionBuilder.java 7725 2011-07-31 18:51:43Z uckelman $
*
* Copyright (c) 2008-2013 by Brent Easton
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
namespace VassalSharp.script.expression
{
	
	/// <summary> A Builder for a field that can contain a property Name or a beanshell expression.</summary>
	/// <author>  Brent
	/// 
	/// </author>
	[Serializable]
	public class PropertyNameExpressionBuilder:ExpressionBuilder
	{
		private const long serialVersionUID = 1L;
		
		public PropertyNameExpressionBuilder(Configurer c, System.Windows.Forms.Form parent):base(c, parent)
		{
		}
		
		public PropertyNameExpressionBuilder(Configurer c, System.Windows.Forms.Form parent, EditablePiece piece):base(c, parent, piece)
		{
			expression.setValue((BeanShellExpression.convertProperty(target.ValueString)));
		}
		
		/// <summary> Convert a property name to an equivalent Beanshell expression</summary>
		public override System.String convert(System.String s)
		{
			return "{" + s + "}";
		}
		
		/// <summary> Save entered expression to the target.
		/// If a single property name has been entered, then return it as a simple name, not as an expression
		/// </summary>
		public override void  save()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'expr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String expr = expression.ValueString.Trim();
			if (BeanShellExpression.isJavaIdentifier(expr))
			{
				target.setValue(expr);
				Dispose();
				return ;
			}
			
			if (expr.StartsWith("GetProperty(\"") && expr.EndsWith("\")") && (expr.Length - expr.replaceAll("\"", "").length()) == 2)
			{
				target.setValue(expr.Substring(13, (expr.Length - 2) - (13)));
				Dispose();
				return ;
			}
			
			base.save();
		}
	}
}