/*
* $Id$
*
* Copyright (c) 2005-2012 by Rodney Kinney, Brent Easton
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
//UPGRADE_TODO: The type 'java.util.regex.Pattern' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Pattern = java.util.regex.Pattern;
using BeanShellExpression = VassalSharp.script.expression.BeanShellExpression;
using FormattedStringExpression = VassalSharp.script.expression.FormattedStringExpression;
namespace VassalSharp.counters
{
	
	/// <summary> Accepts pieces based on whether the piece has properties that
	/// match a given set of conditions
	/// </summary>
	public class PropertiesPieceFilter
	{
		public class AnonymousClassPieceFilter : PieceFilter
		{
			public virtual bool accept(GamePiece piece)
			{
				return true;
			}
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'CONDITIONS '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly Pattern[] CONDITIONS = new Pattern[]{Pattern.compile("!="), Pattern.compile("<="), Pattern.compile(">="), Pattern.compile(">"), Pattern.compile("<"), Pattern.compile("=~"), Pattern.compile("="), Pattern.compile("!~")};
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'AND '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly Pattern AND = Pattern.compile("&&");
		//UPGRADE_NOTE: Final was removed from the declaration of 'OR '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly Pattern OR = Pattern.compile("\\|\\|");
		
		//UPGRADE_NOTE: The initialization of  'ACCEPT_ALL' was moved to static method 'VassalSharp.counters.PropertiesPieceFilter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static PieceFilter ACCEPT_ALL;
		
		/// <summary> Return a PieceFilter parsed from a boolean expression such as
		/// prop1 = value1 && prop2 = value2 || prop3 = value3
		/// </summary>
		/// <param name="expression">
		/// </param>
		/// <returns>
		/// </returns>
		public static PieceFilter parse(System.String expression)
		{
			if (expression == null || expression.Length == 0)
			{
				return ACCEPT_ALL;
			}
			System.String[] s = OR.split(expression);
			PieceFilter f = null;
			if (s.Length > 1)
			{
				f = parse(s[0]);
				for (int i = 1; i < s.Length; ++i)
				{
					f = new BooleanOrPieceFilter(f, parse(s[i]));
				}
			}
			else
			{
				s = AND.split(expression);
				if (s.Length > 1)
				{
					f = parse(s[0]);
					for (int i = 1; i < s.Length; ++i)
					{
						f = new BooleanAndPieceFilter(f, parse(s[i]));
					}
				}
				else
				{
					for (int i = 0; i < CONDITIONS.length && f == null; i++)
					{
						if (expression.indexOf(CONDITIONS[i].pattern()) >= 0)
						{
							s = CONDITIONS[i].split(expression);
							System.String name = "";
							System.String value_Renamed = "";
							if (s.Length > 0)
							{
								name = s[0].Trim();
								if (s.Length > 1)
								{
									value_Renamed = s[1].Trim();
								}
								switch (i)
								{
									
									case 0: 
										f = new NE(name, value_Renamed);
										break;
									
									case 1: 
										f = new LE(name, value_Renamed);
										break;
									
									case 2: 
										f = new GE(name, value_Renamed);
										break;
									
									case 3: 
										f = new GT(name, value_Renamed);
										break;
									
									case 4: 
										f = new LT(name, value_Renamed);
										break;
									
									case 5: 
										f = new MATCH(name, value_Renamed);
										break;
									
									case 6: 
										f = new EQ(name, value_Renamed);
										break;
									
									case 7: 
										f = new NOT_MATCH(name, value_Renamed);
										break;
									}
							}
							break;
						}
					}
					if (f == null)
					{
						f = ACCEPT_ALL;
					}
				}
			}
			return f;
		}
		
		public static System.String toBeanShellString(System.String s)
		{
			return toBeanShellString(parse(s));
		}
		
		public static System.String toBeanShellString(PieceFilter f)
		{
			
			if (f is BooleanAndPieceFilter)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'and '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				BooleanAndPieceFilter and = (BooleanAndPieceFilter) f;
				return "(" + toBeanShellString(and.Filter1) + ") && (" + toBeanShellString(and.Filter2) + ")";
			}
			else if (f is BooleanOrPieceFilter)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'or '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				BooleanOrPieceFilter or = (BooleanOrPieceFilter) f;
				return "(" + toBeanShellString(or.Filter1) + ") || (" + toBeanShellString(or.Filter2) + ")";
			}
			else if (f is ComparisonFilter)
			{
				return ((ComparisonFilter) f).toBeanShellString();
			}
			return "";
		}
		
		private abstract class ComparisonFilter : PieceFilter
		{
			protected internal System.String name;
			protected internal System.String value_Renamed;
			protected internal System.Object alternate;
			
			public ComparisonFilter(System.String name, System.String value_Renamed)
			{
				this.name = name;
				this.value_Renamed = value_Renamed;
				if ("true".Equals(value_Renamed))
				{
					alternate = true;
				}
				else if ("false".Equals(value_Renamed))
				{
					alternate = false;
				}
			}
			
			protected internal virtual int compareTo(GamePiece piece)
			{
				System.String property = System.Convert.ToString(piece.getProperty(name));
				try
				{
					//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.lang.Integer.compareTo' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
					return System.Int32.Parse(property).CompareTo(System.Int32.Parse(value_Renamed));
				}
				catch (System.FormatException e)
				{
					// If both properties are not numbers, compare alphabetically
					return String.CompareOrdinal(property, value_Renamed);
				}
			}
			
			public abstract System.String toBeanShellString();
			
			protected internal virtual System.String toBeanShellName()
			{
				if (name.IndexOf('$') >= 0)
				{
					return "GetProperty(" + new FormattedStringExpression(name).toBeanShellString() + ")";
				}
				else
				{
					return BeanShellExpression.convertProperty(name);
				}
			}
			
			protected internal virtual System.String toBeanShellValue()
			{
				return new FormattedStringExpression(value_Renamed).toBeanShellString();
			}
			public abstract bool accept(VassalSharp.counters.GamePiece param1);
		}
		
		private class EQ:ComparisonFilter
		{
			public EQ(System.String name, System.String value_Renamed):base(name, value_Renamed)
			{
			}
			
			public override bool accept(GamePiece piece)
			{
				System.String property = System.Convert.ToString(piece.getProperty(name));
				bool retVal = value_Renamed.Equals(property);
				if (alternate != null)
				{
					retVal = retVal || alternate.Equals(property);
				}
				return retVal;
			}
			
			public override System.String ToString()
			{
				return "PropertiesPieceFilter[" + name + "==" + value_Renamed + "]";
			}
			
			public override System.String toBeanShellString()
			{
				return toBeanShellName() + "==" + toBeanShellValue();
			}
		}
		
		private class NE:ComparisonFilter
		{
			public NE(System.String name, System.String value_Renamed):base(name, value_Renamed)
			{
			}
			
			public override bool accept(GamePiece piece)
			{
				System.String property = System.Convert.ToString(piece.getProperty(name));
				bool retVal = !value_Renamed.Equals(property);
				if (alternate != null)
				{
					retVal = retVal && !alternate.Equals(property);
				}
				return retVal;
			}
			public override System.String ToString()
			{
				return "PropertiesPieceFilter[" + name + "!=" + value_Renamed + "]";
			}
			
			public override System.String toBeanShellString()
			{
				return toBeanShellName() + "!=" + toBeanShellValue();
			}
		}
		
		private class LT:ComparisonFilter
		{
			public LT(System.String name, System.String value_Renamed):base(name, value_Renamed)
			{
			}
			
			public override bool accept(GamePiece piece)
			{
				return compareTo(piece) < 0;
			}
			
			public override System.String ToString()
			{
				return "PropertiesPieceFilter[" + name + "<" + value_Renamed + "]";
			}
			
			public override System.String toBeanShellString()
			{
				return toBeanShellName() + "<" + toBeanShellValue();
			}
		}
		
		private class LE:ComparisonFilter
		{
			public LE(System.String name, System.String value_Renamed):base(name, value_Renamed)
			{
			}
			
			public override bool accept(GamePiece piece)
			{
				return compareTo(piece) <= 0;
			}
			
			public override System.String ToString()
			{
				return "PropertiesPieceFilter[" + name + "<=" + value_Renamed + "]";
			}
			
			public override System.String toBeanShellString()
			{
				return toBeanShellName() + "<=" + toBeanShellValue();
			}
		}
		
		private class GT:ComparisonFilter
		{
			public GT(System.String name, System.String value_Renamed):base(name, value_Renamed)
			{
			}
			
			public override bool accept(GamePiece piece)
			{
				return compareTo(piece) > 0;
			}
			
			public override System.String ToString()
			{
				return "PropertiesPieceFilter[" + name + ">" + value_Renamed + "]";
			}
			
			public override System.String toBeanShellString()
			{
				return toBeanShellName() + ">" + toBeanShellValue();
			}
		}
		
		private class GE:ComparisonFilter
		{
			public GE(System.String name, System.String value_Renamed):base(name, value_Renamed)
			{
			}
			
			public override bool accept(GamePiece piece)
			{
				return compareTo(piece) >= 0;
			}
			
			public override System.String ToString()
			{
				return "PropertiesPieceFilter[" + name + ">=" + value_Renamed + "]";
			}
			
			public override System.String toBeanShellString()
			{
				return toBeanShellName() + ">=" + toBeanShellValue();
			}
		}
		
		private class MATCH:ComparisonFilter
		{
			public MATCH(System.String name, System.String value_Renamed):base(name, value_Renamed)
			{
			}
			
			public override bool accept(GamePiece piece)
			{
				System.String property = System.Convert.ToString(piece.getProperty(name));
				return Pattern.matches(value_Renamed, property);
			}
			
			public override System.String ToString()
			{
				return "PropertiesPieceFilter[" + name + "~" + value_Renamed + "]";
			}
			
			public override System.String toBeanShellString()
			{
				return toBeanShellName() + "=~" + toBeanShellValue();
			}
		}
		
		private class NOT_MATCH:MATCH
		{
			public NOT_MATCH(System.String name, System.String value_Renamed):base(name, value_Renamed)
			{
			}
			
			public override bool accept(GamePiece piece)
			{
				return !base.accept(piece);
			}
			
			public override System.String ToString()
			{
				return "PropertiesPieceFilter[" + name + "!~" + value_Renamed + "]";
			}
			
			public override System.String toBeanShellString()
			{
				return toBeanShellName() + "!~" + toBeanShellValue();
			}
		}
		static PropertiesPieceFilter()
		{
			ACCEPT_ALL = new AnonymousClassPieceFilter();
		}
	}
}