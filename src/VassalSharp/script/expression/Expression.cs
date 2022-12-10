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
using GameModule = VassalSharp.build.GameModule;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using GamePiece = VassalSharp.counters.GamePiece;
using PieceFilter = VassalSharp.counters.PieceFilter;
namespace VassalSharp.script.expression
{
	
	/// <summary> An abstract class representing an expression that can be evaluated.
	/// 
	/// Subclasses implement specific types of expression and the way they are
	/// evaluated.
	/// 
	/// </summary>
	public abstract class Expression
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPieceFilter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPieceFilter : PieceFilter
		{
			public AnonymousClassPieceFilter(Expression enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Expression enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Expression enclosingInstance;
			public Expression Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual bool accept(GamePiece piece)
			{
				return true;
			}
		}
		
		private System.String expression;
		
		public virtual void  setExpression(System.String s)
		{
			expression = s;
		}
		
		public virtual System.String getExpression()
		{
			return expression;
		}
		
		/// <summary> Each subclass must implement evaluate() to evaluate itself
		/// 
		/// </summary>
		/// <param name="ps">Property Source providing property values
		/// </param>
		/// <param name="properties">default property values
		/// </param>
		/// <param name="localized">localize property calls?
		/// </param>
		/// <returns> evaluated String.
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		abstract String evaluate(PropertySource ps, Map < String, String > properties, 
		boolean localized) throws ExpressionException;
		
		public virtual System.String evaluate()
		{
			return evaluate(null, null, false);
		}
		
		public virtual System.String evaluate(PropertySource ps)
		{
			return evaluate(ps, null, false);
		}
		
		public virtual System.String evaluate(PropertySource ps, bool localized)
		{
			return evaluate(ps, null, localized);
		}
		
		public virtual System.String evaluate(bool localized)
		{
			return evaluate(null, null, localized);
		}
		
		/// <summary> Return a PieceFilter using the expression.
		/// 
		/// </summary>
		/// <param name="ps">
		/// </param>
		/// <returns>
		/// </returns>
		public virtual PieceFilter getFilter(PropertySource ps)
		{
			return new AnonymousClassPieceFilter(this);
		}
		
		public virtual PieceFilter getFilter()
		{
			return getFilter(GameModule.getGameModule());
		}
		
		/// <summary> Output a BeanShell equivalent of this expression.
		/// 
		/// </summary>
		/// <returns> BeanShell equivalent
		/// </returns>
		public abstract System.String toBeanShellString();
		
		/// <summary> Factory method to create an appropriate expression based on the supplied
		/// String. The majority of expressions in a module are going to be blank,
		/// integers or simple strings, so return optimised Expression subclasses for
		/// these types.
		/// </summary>
		public static Expression createExpression(System.String s)
		{
			
			// A null expression?
			if (s == null || s.Trim().Length == 0)
			{
				return new NullExpression();
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String t = s.Trim();
			
			// BeanShell expression enclosed by braces?
			if (t.StartsWith("{") && t.EndsWith("}"))
			{
				return BeanShellExpression.createExpression(s);
			}
			
			// A simple integer expression
			try
			{
				return new IntExpression(System.Int32.Parse(t));
			}
			catch (System.FormatException e)
			{
				// Not an error
			}
			
			// An old-style Formatted String?
			if (t.IndexOf('$') >= 0)
			{
				return new FormattedStringExpression(t);
			}
			
			// Must be a plain String
			return new StringExpression(s);
		}
		
		/// <summary> Factory method to create a new Property Match Expression.
		/// 
		/// </summary>
		/// <param name="s">
		/// </param>
		/// <returns>
		/// </returns>
		public static Expression createPropertyExpression(System.String s)
		{
			
			// A null expression?
			if (s == null || s.Trim().Length == 0)
			{
				return new NullExpression();
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String t = s.Trim();
			
			// BeanShell expression?
			if (t.StartsWith("{") && t.EndsWith("}"))
			{
				return new BeanShellExpression(t.Substring(1, (t.Length - 1) - (1)));
			}
			
			// An old-style Property Match String
			return new PropertyMatchExpression(t);
		}
		
		/// <summary> Factory method to create a Beanshell expression of a value that
		/// is known to be a property name.
		/// Used to convert values such as the Follow proprty field in Embellishment
		/// 
		/// </summary>
		public static Expression createSimplePropertyExpression(System.String s)
		{
			
			// A null expression?
			if (s == null || s.Trim().Length == 0)
			{
				return new NullExpression();
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String t = s.Trim();
			
			// BeanShell expression?
			if (t.StartsWith("{") && t.EndsWith("}"))
			{
				return new BeanShellExpression(t.Substring(1, (t.Length - 1) - (1)));
			}
			
			return new SinglePropertyExpression(t);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override int GetHashCode()
		{
			return expression == null?0:expression.GetHashCode();
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public  override bool Equals(System.Object obj)
		{
			if (this == obj)
				return true;
			
			if (obj == null || GetType() != obj.GetType())
				return false;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'other '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Expression other = (Expression) obj;
			
			if (expression == null && other.expression != null)
				return false;
			
			return expression.Equals(other.expression);
		}
	}
}