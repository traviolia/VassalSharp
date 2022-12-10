/*
* $Id$
*
* Copyright (c) 2009-2013 Brent Easton
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
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using GamePiece = VassalSharp.counters.GamePiece;
using PieceFilter = VassalSharp.counters.PieceFilter;
using Resources = VassalSharp.i18n.Resources;
using ExpressionInterpreter = VassalSharp.script.ExpressionInterpreter;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
namespace VassalSharp.script.expression
{
	
	/// <summary> A basic beanShell expression</summary>
	public class BeanShellExpression:Expression
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPieceFilter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		new private class AnonymousClassPieceFilter : PieceFilter
		{
			public AnonymousClassPieceFilter(BeanShellExpression enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BeanShellExpression enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BeanShellExpression enclosingInstance;
			public BeanShellExpression Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual bool accept(GamePiece piece)
			{
				System.String result = null;
				try
				{
					result = Enclosing_Instance.evaluate(piece);
				}
				catch (ExpressionException e)
				{
					ErrorDialog.dataError(new BadDataReport(Resources.getString("Error.expression_error"), "Expression=" + Enclosing_Instance.getExpression() + ", Error=" + e.Error, e));
				}
				return "true".Equals(result);
			}
		}
		private void  InitBlock()
		{
			if (interpreter == null)
			{
				interpreter = ExpressionInterpreter.createInterpreter(strip(getExpression()));
			}
			return interpreter.evaluate(ps, localized);
		}
		
		protected internal ExpressionInterpreter interpreter;
		
		public BeanShellExpression(System.String s)
		{
			InitBlock();
			setExpression("{" + s + "}");
		}
		
		/// <summary> Evaluate this expression using a BeanShell Interpreter</summary>
		public System.String evaluate;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(PropertySource ps, Map < String, String > properties, 
		boolean localized) throws ExpressionException
		
		
		public override System.String toBeanShellString()
		{
			return strip(getExpression());
		}
		
		protected internal static System.String strip(System.String expr)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String s = expr.Trim();
			if (s.StartsWith("{") && s.EndsWith("}"))
			{
				return s.Substring(1, (s.Length - 1) - (1));
			}
			return expr;
		}
		
		/// <summary> Return a PieceFilter that selects GamePieces that cause
		/// this expression to evaluate to true
		/// </summary>
		public override PieceFilter getFilter(PropertySource ps)
		{
			return new AnonymousClassPieceFilter(this);
		}
		
		/// <summary> Convert a Property name to it's BeanShell equivalent.
		/// 
		/// </summary>
		/// <param name="property">name
		/// </param>
		/// <returns> beanshell equivalent
		/// </returns>
		public static System.String convertProperty(System.String prop)
		{
			// Null Expression
			if (prop == null || prop.Length == 0)
			{
				return "";
			}
			
			// Already a bsh exopression?
			if (isBeanShellExpression(prop))
			{
				return strip(prop);
			}
			
			// Check it follows Java variable rules
			bool ok = (System.Char.IsLetter(prop[0]) || prop[0].CompareTo('$') == 0 || prop[0].CompareTo('_') == 0);
			if (ok)
			{
				for (int i = 1; i < prop.Length && ok; i++)
				{
					//UPGRADE_ISSUE: Method 'java.lang.Character.isJavaIdentifierPart' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangCharacterisJavaIdentifierPart_char'"
					ok = Character.isJavaIdentifierPart(prop[i]);
				}
			}
			
			// If not a Java variable, wrap it in GetProperty()
			return ok?prop:"GetProperty(\"" + prop + "\")";
		}
		
		public static bool isBeanShellExpression(System.String expr)
		{
			return expr.StartsWith("{") && expr.EndsWith("}");
		}
		
		public static bool isJavaIdentifier(System.String s)
		{
			if (s == null || s.Length == 0)
			{
				return false;
			}
			
			if (!(System.Char.IsLetter(s[0]) || s[0].CompareTo('$') == 0 || s[0].CompareTo('_') == 0))
			{
				return false;
			}
			
			for (int i = 1; i < s.Length; ++i)
			{
				//UPGRADE_ISSUE: Method 'java.lang.Character.isJavaIdentifierPart' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangCharacterisJavaIdentifierPart_char'"
				if (!Character.isJavaIdentifierPart(s[i]))
				{
					return false;
				}
			}
			
			return true;
		}
		
		/// <summary> Create a BeanShellExpression.
		/// 
		/// The expression may or may not be surrounded by {}.
		/// 
		/// Create null, integer and simple Expressions as their basic type to
		/// ensure efficient evaluation.
		/// </summary>
		public static new Expression createExpression(System.String s)
		{
			System.String expr;
			//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String t = s.Trim();
			
			if (t.StartsWith("{") && t.EndsWith("}"))
			{
				expr = t.Substring(1, (t.Length - 1) - (1)).Trim();
			}
			else
			{
				expr = t;
			}
			
			if (expr.Trim().Length == 0)
			{
				return new NullExpression();
			}
			
			try
			{
				return new IntExpression(System.Int32.Parse(expr));
			}
			catch (System.FormatException e)
			{
				// Not an error
			}
			
			// Return a single String as a string without quotes
			//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'System.String.IndexOf' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
			if (expr.Length > 1 && expr.StartsWith("\"") && expr.EndsWith("\"") && expr.IndexOf('"', 1) == expr.Length - 1)
			{
				return new StringExpression(expr.Substring(1, (expr.Length - 1) - (1)));
			}
			
			// Return a generalised Beanshell expression
			return new BeanShellExpression(expr);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public  override bool Equals(System.Object bse)
		{
			return base.Equals(bse);
		}
		//UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1306'"
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}