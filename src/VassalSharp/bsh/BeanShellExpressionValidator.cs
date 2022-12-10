/*
* $Id: BeanShellExpressionValidator.java,v 1.1 2006/09/28 04:59:19 swampwallaby Exp $
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
namespace bsh
{
	
	/// <summary> Validate a single line BeanShell expression. 
	/// Build a list of variable references in the expression.
	/// 
	/// This Class must be defined in package bsh to allow access to 
	/// package visible elements in the bsh library.
	/// 
	/// </summary>
	public class BeanShellExpressionValidator
	{
		private void  InitBlock()
		{
			return variables;
			return methods;
		}
		
		protected internal System.String expression;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < String > variables = new ArrayList < String >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < String > methods = new ArrayList < String >();
		protected internal System.String error;
		protected internal bool valid = false;
		
		/// <summary> Build a new Validator and validate the expression</summary>
		/// <param name="expression">Expression to validate
		/// </param>
		public BeanShellExpressionValidator(System.String expression)
		{
			InitBlock();
			this.expression = expression;
			valid = validate();
		}
		
		/// <summary> Is the expression valid?</summary>
		/// <returns> valid
		/// </returns>
		public virtual bool isValid()
		{
			return valid;
		}
		
		protected internal virtual void  setValid(bool b)
		{
			valid = b;
		}
		
		/// <summary> Return a list of Variable references in the expression</summary>
		/// <returns> List of variables
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getVariables()
		
		/// <summary> Return a list of Methods called by the expression</summary>
		/// <returns> List of Methods
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getMethods()
		
		/// <summary> Return an Error Message if no valid</summary>
		/// <returns>
		/// </returns>
		public virtual System.String getError()
		{
			return error;
		}
		
		protected internal virtual void  setError(System.String s)
		{
			error = s;
		}
		
		/// <summary> Validate the expression
		/// 
		/// </summary>
		/// <returns> Expression validity
		/// </returns>
		protected internal virtual bool validate()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'expr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String expr = stripBraces(expression);
			
			setError("");
			try
			{
				Parser p = new Parser(new System.IO.StringReader(expr + ";"));
				for (; ; )
				{
					if (p.Line())
					{
						return true;
					}
					else
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'node '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						SimpleNode node = p.popNode();
						if (!processNode(node))
						{
							return false;
						}
					}
				}
			}
			catch (ParseException e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				setError(e.Message);
				return false;
			}
			catch (TokenMgrError e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				setError(e.Message);
				return false;
			}
		}
		
		/// <summary> If the expression is surrounded by Vassal expression braces {}
		/// replace them with spaces so that it will validate and report errors
		/// in the correct location
		/// 
		/// </summary>
		/// <param name="s">Expression
		/// </param>
		/// <returns> stripped expression
		/// </returns>
		public static System.String stripBraces(System.String s)
		{
			System.String expr = s;
			if (s.Trim().StartsWith("{") && s.Trim().EndsWith("}"))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'start '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int start = s.IndexOf("{");
				//UPGRADE_NOTE: Final was removed from the declaration of 'end '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int end = s.LastIndexOf("}");
				System.Text.StringBuilder buffer = new System.Text.StringBuilder(s.Length);
				for (int i = 0; i < s.Length; i++)
				{
					if (i == start || i == end)
					{
						buffer.Append(' ');
					}
					else
					{
						buffer.Append(s[i]);
					}
				}
				expr = buffer.ToString();
			}
			return expr;
		}
		
		/// <summary> Process a Parser Node and extract any Variable and Method references.
		/// Assignemnts are not allowed in an expression, so flag as an error
		/// </summary>
		/// <param name="node">Parser Node
		/// </param>
		protected internal virtual bool processNode(SimpleNode node)
		{
			if (node == null)
			{
				return true;
			}
			
			if (node is BSHAmbiguousName)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String name = ((BSHAmbiguousName) node).Text.Trim();
				if ((node.parent is BSHMethodInvocation))
				{
					if (!methods.contains(name))
					{
						methods.add(name);
					}
				}
				else if (!variables.contains(name))
				{
					variables.add(name);
				}
			}
			else if (node is BSHAssignment)
			{
				setError("Assignemts (=) not allowed in Expressions. See Help");
				return false;
			}
			else
			{
				if (node.children != null)
				{
					for (int i = 0; i < node.children.Length; i++)
					{
						if (!processNode(node.getChild(i)))
						{
							return false;
						}
					}
				}
			}
			return true;
		}
	}
}