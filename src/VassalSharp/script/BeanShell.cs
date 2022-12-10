/*
* $Id$
*
* Copyright (c) 2008-2009 Brent Easton
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
using WarningDialog = VassalSharp.tools.WarningDialog;
using BeanShellExpressionValidator = bsh.BeanShellExpressionValidator;
using EvalError = bsh.EvalError;
using NameSpace = bsh.NameSpace;
namespace VassalSharp.script
{
	
	/// <summary> 
	/// Class encapsulating BeanShell support in Vassal
	/// 
	/// </summary>
	public class BeanShell
	{
		public static BeanShell Instance
		{
			get
			{
				return instance;
			}
			
		}
		/// <summary> Return the Vassal shared NameSpace
		/// 
		/// </summary>
		/// <returns> Global NameSpace
		/// </returns>
		virtual public NameSpace GlobalNameSpace
		{
			get
			{
				return globalInterpreter.NameSpace;
			}
			
		}
		
		private static BeanShell instance = new BeanShell();
		
		protected internal const System.String INIT_SCRIPT = "/VASSAL/script/init_script.bsh";
		
		/*
		* An interpreter for adding script methods to the global NameSpace
		*/
		protected internal ScriptInterpreter globalInterpreter;
		
		public BeanShell()
		{
			//UPGRADE_ISSUE: Method 'java.lang.Class.getClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetClassLoader'"
			globalInterpreter = new ScriptInterpreter(this.GetType().getClassLoader());
			init();
		}
		
		public virtual void  init()
		{
			// Read in the Vassal Script init script
			instance.GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			System.Uri ini = new System.Uri(System.IO.Path.GetFullPath(INIT_SCRIPT));
			System.IO.StreamReader in_Renamed = null;
			try
			{
				//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				in_Renamed = new System.IO.StreamReader(new System.IO.StreamReader(System.Net.WebRequest.Create(ini).GetResponse().GetResponseStream(), System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(System.Net.WebRequest.Create(ini).GetResponse().GetResponseStream(), System.Text.Encoding.Default).CurrentEncoding);
				CompileResult result = compile(in_Renamed);
				if (!result.Success)
				{
					result.printStackTrace();
				}
			}
			catch (System.IO.IOException e)
			{
				//FIXME: Error message
				WarningDialog.show(e, "");
			}
			finally
			{
				if (in_Renamed != null)
				{
					try
					{
						in_Renamed.Close();
					}
					catch (System.IO.IOException e)
					{
						//
					}
				}
			}
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual CompileResult compile(System.IO.StreamReader in_Renamed)
		{
			try
			{
				globalInterpreter.eval(in_Renamed);
			}
			catch (EvalError e)
			{
				return new CompileResult(e);
			}
			return new CompileResult();
		}
		
		/// <summary> Execute a Script named in a component DoAction or trait DoAction.
		/// Action Scripts take no parameters and return no value.
		/// </summary>
		/// <param name="script">
		/// </param>
		public virtual void  executeActionScript(System.String scriptName)
		{
			try
			{
				globalInterpreter.evaluate(scriptName + "();");
			}
			catch (EvalError e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
			}
		}
		
		/// <summary> Parse and validate a single expression or script. No evaluation or checking
		/// for undefined variables
		/// 
		/// </summary>
		/// <param name="expression">Expression to validate
		/// </param>
		public static bool validateExpression(System.String expression)
		{
			return new BeanShellExpressionValidator(expression).isValid();
		}
		
		/// <summary> Convert a String value into a wrapped primitive object if possible.
		/// 
		/// </summary>
		/// <param name="value">
		/// </param>
		/// <returns> wrapped value
		/// </returns>
		public static System.Object wrap(System.String value_Renamed)
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
	}
}