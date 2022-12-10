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
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using GameModule = VassalSharp.build.GameModule;
using Map = VassalSharp.build.module.Map;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using GamePiece = VassalSharp.counters.GamePiece;
using Stack = VassalSharp.counters.Stack;
using ExpressionException = VassalSharp.script.expression.ExpressionException;
using WarningDialog = VassalSharp.tools.WarningDialog;
using IOUtils = VassalSharp.tools.io.IOUtils;
using BeanShellExpressionValidator = bsh.BeanShellExpressionValidator;
using EvalError = bsh.EvalError;
using NameSpace = bsh.NameSpace;
namespace VassalSharp.script
{
	
	/// <summary> 
	/// A BeanShell Interpreter customised to evaluate a single Vassal
	/// expression containing Vassal property references.
	/// All traits with the same expression will share the same Interpreter
	/// 
	/// Each ExpressionInterpreter has 2 levels of NameSpace:
	/// 1. Top level is a single global NameSpace that contains utility methods
	/// available to all ExpressionInterpreters. It is the parent of all
	/// level 2 NameSpaces.
	/// 2. Level 2 is a NameSpace for each unique expression that contains the
	/// parsed expression. All expressions in all traits that are the same
	/// will use the one Expression NameSpace.
	/// 
	/// </summary>
	[Serializable]
	public class ExpressionInterpreter:AbstractInterpreter
	{
		/// <summary> Return the current expression</summary>
		/// <returns> expression
		/// </returns>
		virtual public System.String Expression
		{
			get
			{
				return expression;
			}
			
		}
		
		private const long serialVersionUID = 1L;
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.script.ExpressionInterpreter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		protected internal const System.String INIT_SCRIPT = "/VASSAL/script/init_expression.bsh";
		new protected internal const System.String THIS = "_interp";
		new protected internal const System.String SOURCE = "_source";
		protected internal const System.String MAGIC1 = "_xyzzy";
		protected internal const System.String MAGIC2 = "_plugh";
		protected internal const System.String MAGIC3 = "_plover";
		
		
		// Top-level static NameSpace shared between all ExpressionInterpreters
		// Loaded with utility methods available to all interpreters
		protected internal static NameSpace topLevelNameSpace;
		
		protected internal NameSpace expressionNameSpace;
		
		//protected NameSpace localNameSpace;
		
		protected internal System.String expression;
		protected internal PropertySource source;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < String > variables = new ArrayList < String >();
		
		// Maintain a cache of all generated Interpreters. All Expressions
		// with the same Expression use the same Interpreter.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static HashMap < String, ExpressionInterpreter > cache = new HashMap < String, ExpressionInterpreter >();
		
		public static ExpressionInterpreter createInterpreter(System.String expr)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String e = expr == null?"":strip(expr);
			ExpressionInterpreter interpreter = cache.get_Renamed(e);
			if (interpreter == null)
			{
				interpreter = new ExpressionInterpreter(e);
				cache.put(e, interpreter);
			}
			return interpreter;
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
		
		/// <summary> Private constructor to build an ExpressionInterpreter. Interpreters
		/// can only be created by createInterpreter.
		/// 
		/// </summary>
		/// <param name="expr">Expression
		/// </param>
		/// <throws>  MalformedExpressionException </throws>
		private ExpressionInterpreter(System.String expr):base()
		{
			
			expression = expr;
			
			// Install the Vassal Class loader so that bsh can find Vassal classes
			//UPGRADE_ISSUE: Method 'java.lang.Class.getClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetClassLoader'"
			this.ClassLoader = this.GetType().getClassLoader();
			
			// Initialise the top-level name space if this is the first
			// expression to be created
			if (topLevelNameSpace == null)
			{
				initialiseStatic();
			}
			
			// Create the Expression level namespace as a child of the
			// top level namespace
			expressionNameSpace = new NameSpace(topLevelNameSpace, "expression");
			
			// Get a list of any variables used in the expression. These are
			// property names that will need to be evaluated at expression
			// evaluation time
			variables = new BeanShellExpressionValidator(expression).getVariables();
			
			// Build a method enclosing the expression. This saves the results
			// of the expression parsing, improving performance. Force return
			// value to a String as this is what Vassal is expecting.
			NameSpace = expressionNameSpace;
			if (expression.Length > 0)
			{
				try
				{
					eval("String " + MAGIC2 + "() { " + MAGIC3 + "=" + expression + "; return " + MAGIC3 + ".toString();}");
				}
				catch (EvalError e)
				{
					throw new ExpressionException(Expression);
				}
			}
			
			// Add a link to this Interpreter into the new NameSpace for callbacks from
			// BeanShell back to us
			setVar(THIS, this);
		}
		
		/// <summary> Initialise the static elements of this class. Create a Top Level
		/// NameSpace using the Vassal class loader, load useful classes and
		/// read and process the init_expression.bsh file to load scripted
		/// methods available to expressions.
		/// </summary>
		protected internal virtual void  initialiseStatic()
		{
			topLevelNameSpace = new NameSpace((NameSpace) null, ClassManager, "topLevel");
			NameSpace = topLevelNameSpace;
			NameSpace.importClass("VassalSharp.build.module.properties.PropertySource");
			NameSpace.importClass("VassalSharp.script.ExpressionInterpreter");
			
			// Read the Expression initialisation script into the top level namespace
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			System.Uri ini = new System.Uri(System.IO.Path.GetFullPath(INIT_SCRIPT));
			logger.info("Attempting to load " + INIT_SCRIPT + " URI generated=" + ini.ToString());
			System.IO.StreamReader in_Renamed = null;
			try
			{
				//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				in_Renamed = new System.IO.StreamReader(new System.IO.StreamReader(System.Net.WebRequest.Create(ini).GetResponse().GetResponseStream(), System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(System.Net.WebRequest.Create(ini).GetResponse().GetResponseStream(), System.Text.Encoding.Default).CurrentEncoding);
				
				try
				{
					eval(in_Renamed);
				}
				catch (EvalError e)
				{
					logger.error("Error trying to read init script: " + ini.ToString());
					WarningDialog.show(e, "");
				}
			}
			catch (System.IO.IOException e)
			{
				logger.error("Error trying to read init script: " + ini.ToString());
				WarningDialog.show(e, "");
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
		
		/// <summary> Evaluate the expression, setting the value of any undefined
		/// values to the matching Vassal property value. Primitives must
		/// be wrapped.
		/// 
		/// </summary>
		/// <returns> result
		/// </returns>
		public virtual System.String evaluate(PropertySource ps)
		{
			return evaluate(ps, false);
		}
		
		public virtual System.String evaluate(PropertySource ps, bool localized)
		{
			
			if (Expression.Length == 0)
			{
				return "";
			}
			
			// Default to the GameModule to satisfy properties if no
			// GamePiece supplied.
			source = ps == null?GameModule.getGameModule():ps;
			
			NameSpace = expressionNameSpace;
			
			// Bind each undeclared variable with the value of the
			// corresponding Vassal property. Allow for old-style $variable$ references
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String var: variables)
			{
				System.String name = var;
				if (name.Length > 2 && name.StartsWith("$") && name.EndsWith("$"))
				{
					name = name.Substring(1, (name.Length - 1) - (1));
				}
				System.Object prop = localized?source.getLocalizedProperty(name):source.getProperty(name);
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.String value_Renamed = prop == null?"":prop.ToString();
				if (value_Renamed == null)
				{
					setVar(var, "");
				}
				else if ("true".Equals(value_Renamed))
				{
					setVar(var, true);
				}
				else if ("false".Equals(value_Renamed))
				{
					setVar(var, false);
				}
				else
				{
					try
					{
						setVar(var, System.Int32.Parse(value_Renamed));
					}
					catch (System.FormatException e)
					{
						try
						{
							//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Float.floatValue' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							setVar(var, (float) System.Single.Parse(value_Renamed));
						}
						catch (System.FormatException e1)
						{
							setVar(var, value_Renamed);
						}
					}
				}
			}
			
			// Re-evaluate the pre-parsed expression now that the undefined variables have
			// been bound to their Vassal property values.
			
			setVar(THIS, this);
			setVar(SOURCE, source);
			
			System.String result = "";
			try
			{
				eval(MAGIC1 + "=" + MAGIC2 + "()");
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				result = get_Renamed(MAGIC1).ToString();
			}
			catch (EvalError e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String s = e.RawMessage;
				//UPGRADE_NOTE: Final was removed from the declaration of 'search '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String search = MAGIC2 + "();'' : ";
				//UPGRADE_NOTE: Final was removed from the declaration of 'pos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int pos = s.IndexOf(search);
				throw new ExpressionException(Expression, s.Substring(pos + search.Length));
			}
			
			return result;
		}
		
		public virtual System.String evaluate()
		{
			return Expression.Length == 0?"":evaluate(GameModule.getGameModule());
		}
		
		/// <summary> Convert a String value into a wrapped primitive object if possible.
		/// Note this is a non-static copy of BeanShell.wrap(). Callbacks from
		/// beanshell (e.g. getProperty) fail if an attempt is made to call a static method.
		/// 
		/// </summary>
		/// <param name="value">
		/// </param>
		/// <returns> wrapped value
		/// </returns>
		public virtual System.Object wrap(System.String value_Renamed)
		{
			if (value_Renamed == null)
			{
				return "";
			}
			else if ("true".Equals(value_Renamed))
			{
				return true;
			}
			else if ("false".Equals(value_Renamed))
			{
				return false;
			}
			else
			{
				try
				{
					return System.Int32.Parse(value_Renamed);
				}
				catch (System.FormatException e)
				{
					return value_Renamed;
				}
			}
		}
		
		/// <summary>**************************************************************
		/// Callbacks from BeanShell Expressions to Vassal
		/// 
		/// </summary>
		
		public virtual System.Object getProperty(System.String name)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Object value_Renamed = source.getProperty(name);
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return value_Renamed == null?"":wrap(value_Renamed.ToString());
		}
		
		public virtual System.Object getLocalizedProperty(System.String name)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Object value_Renamed = source.getLocalizedProperty(name);
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return value_Renamed == null?"":wrap(value_Renamed.ToString());
		}
		
		/// <summary> SumStack(property) function
		/// Total the value of the named property in all counters in the
		/// same stack as the specified piece.
		/// 
		/// </summary>
		/// <param name="property">Property Name
		/// </param>
		/// <param name="ps">GamePiece
		/// </param>
		/// <returns> total
		/// </returns>
		public virtual System.Object sumStack(System.String property, PropertySource ps)
		{
			int result = 0;
			if (ps is GamePiece)
			{
				Stack s = ((GamePiece) ps).Parent;
				if (s != null)
				{
					for (int i = 0; i < s.PieceCount; i++)
					{
						
						try
						{
							//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							result += System.Int32.Parse(s.getPieceAt(i).getProperty(property).ToString());
						}
						catch (System.Exception e)
						{
							// Anything at all goes wrong trying to add the property, just ignore it and treat as 0
						}
					}
				}
			}
			return result;
		}
		
		/// <summary> SumLocation(property) function
		/// Total the value of the named property in all counters in the
		/// same location as the specified piece.
		/// 
		/// * WARNING * This WILL be inneficient as the number of counters on the
		/// map increases.
		/// 
		/// </summary>
		/// <param name="property">Property Name
		/// </param>
		/// <param name="ps">GamePiece
		/// </param>
		/// <returns> total
		/// </returns>
		public virtual System.Object sumLocation(System.String property, PropertySource ps)
		{
			int result = 0;
			if (ps is GamePiece)
			{
				GamePiece p = (GamePiece) ps;
				Map m = p.getMap();
				if (m != null)
				{
					System.String here = m.locationName(p.Position);
					GamePiece[] pieces = m.getPieces();
					for (int i = 0; i < pieces.Length; i++)
					{
						if (here.Equals(m.locationName(pieces[i].Position)))
						{
							if (pieces[i] is Stack)
							{
								Stack s = (Stack) pieces[i];
								for (int j = 0; j < s.PieceCount; j++)
								{
									try
									{
										//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
										result += System.Int32.Parse(s.getPieceAt(j).getProperty(property).ToString());
									}
									catch (System.FormatException e)
									{
										//
									}
								}
							}
							else
							{
								try
								{
									//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
									result += System.Int32.Parse(pieces[i].getProperty(property).ToString());
								}
								catch (System.FormatException e)
								{
									//
								}
							}
						}
					}
				}
			}
			return result;
		}
		protected ExpressionInterpreter(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info, context)
		{
		}
		//UPGRADE_NOTE: A parameterless constructor was added for a serializable class to avoid compile errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1268'"
		public ExpressionInterpreter()
		{
		}
		public override void  GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
		static ExpressionInterpreter()
		{
			logger = LoggerFactory.getLogger(typeof(ExpressionInterpreter));
		}
	}
}