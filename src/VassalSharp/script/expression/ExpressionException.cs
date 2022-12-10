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
namespace VassalSharp.script.expression
{
	
	[Serializable]
	public class ExpressionException:System.Exception
	{
		virtual public System.String Expression
		{
			get
			{
				return expression;
			}
			
		}
		virtual public System.String Error
		{
			get
			{
				return error;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		protected internal System.String expression;
		protected internal System.String error;
		
		public ExpressionException(System.String s):this(s, "")
		{
		}
		
		public ExpressionException(System.String s, System.String e):base()
		{
			expression = s;
			error = e;
		}
	}
}