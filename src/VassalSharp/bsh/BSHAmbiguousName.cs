/// <summary>**************************************************************************
/// *
/// This file is part of the BeanShell Java Scripting distribution.          *
/// Documentation and updates may be found at http://www.beanshell.org/      *
/// *
/// Sun Public License Notice:                                               *
/// *
/// The contents of this file are subject to the Sun Public License Version  *
/// 1.0 (the "License"); you may not use this file except in compliance with *
/// the License. A copy of the License is available at http://www.sun.com    * 
/// *
/// The Original Code is BeanShell. The Initial Developer of the Original    *
/// Code is Pat Niemeyer. Portions created by Pat Niemeyer are Copyright     *
/// (C) 2000.  All Rights Reserved.                                          *
/// *
/// GNU Public License Notice:                                               *
/// *
/// Alternatively, the contents of this file may be used under the terms of  *
/// the GNU Lesser General Public License (the "LGPL"), in which case the    *
/// provisions of LGPL are applicable instead of those above. If you wish to *
/// allow use of your version of this file only under the  terms of the LGPL *
/// and not to allow others to use your version of this file under the SPL,  *
/// indicate your decision by deleting the provisions above and replace      *
/// them with the notice and other provisions required by the LGPL.  If you  *
/// do not delete the provisions above, a recipient may use your version of  *
/// this file under either the SPL or the LGPL.                              *
/// *
/// Patrick Niemeyer (pat@pat.net)                                           *
/// Author of Learning Java, O'Reilly & Associates                           *
/// http://www.pat.net/~pat/                                                 *
/// *
/// ***************************************************************************
/// </summary>
using System;
namespace bsh
{
	
	[Serializable]
	class BSHAmbiguousName:SimpleNode
	{
		public System.String text;
		
		internal BSHAmbiguousName(int id):base(id)
		{
		}
		
		public virtual Name getName(NameSpace namespace_Renamed)
		{
			return namespace_Renamed.getNameResolver(text);
		}
		
		public virtual System.Object toObject(CallStack callstack, Interpreter interpreter)
		{
			return toObject(callstack, interpreter, false);
		}
		
		internal virtual System.Object toObject(CallStack callstack, Interpreter interpreter, bool forceClass)
		{
			try
			{
				return getName(callstack.top()).toObject(callstack, interpreter, forceClass);
			}
			catch (UtilEvalError e)
			{
				//e.printStackTrace();
				throw e.toEvalError(this, callstack);
			}
		}
		
		public virtual System.Type toClass(CallStack callstack, Interpreter interpreter)
		{
			try
			{
				return getName(callstack.top()).toClass();
			}
			//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new EvalError(e.Message, this, callstack);
			}
			catch (UtilEvalError e2)
			{
				// ClassPathException is a type of UtilEvalError
				throw e2.toEvalError(this, callstack);
			}
		}
		
		public virtual LHS toLHS(CallStack callstack, Interpreter interpreter)
		{
			try
			{
				return getName(callstack.top()).toLHS(callstack, interpreter);
			}
			catch (UtilEvalError e)
			{
				throw e.toEvalError(this, callstack);
			}
		}
		
		/*
		The interpretation of an ambiguous name is context sensitive.
		We disallow a generic eval( ).
		*/
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			throw new InterpreterError("Don't know how to eval an ambiguous name!" + "  Use toObject() if you want an object.");
		}
		
		public override System.String ToString()
		{
			return "AmbigousName: " + text;
		}
	}
}