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
	class BSHMethodInvocation:SimpleNode
	{
		virtual internal BSHAmbiguousName NameNode
		{
			get
			{
				return (BSHAmbiguousName) jjtGetChild(0);
			}
			
		}
		virtual internal BSHArguments ArgsNode
		{
			get
			{
				return (BSHArguments) jjtGetChild(1);
			}
			
		}
		internal BSHMethodInvocation(int id):base(id)
		{
		}
		
		/// <summary>Evaluate the method invocation with the specified callstack and 
		/// interpreter
		/// </summary>
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			NameSpace namespace_Renamed = callstack.top();
			BSHAmbiguousName nameNode = NameNode;
			
			// Do not evaluate methods this() or super() in class instance space
			// (i.e. inside a constructor)
			if (namespace_Renamed.Parent != null && namespace_Renamed.Parent.isClass && (nameNode.text.Equals("super") || nameNode.text.Equals("this")))
				return Primitive.VOID;
			
			Name name = nameNode.getName(namespace_Renamed);
			System.Object[] args = ArgsNode.getArguments(callstack, interpreter);
			
			// This try/catch block is replicated is BSHPrimarySuffix... need to
			// factor out common functionality...
			// Move to Reflect?
			try
			{
				return name.invokeMethod(interpreter, args, callstack, this);
			}
			catch (ReflectError e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new EvalError("Error in method invocation: " + e.Message, this, callstack);
			}
			catch (System.Reflection.TargetInvocationException e)
			{
				System.String msg = "Method Invocation " + name;
				//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				System.Exception te = e.GetBaseException();
				
				/*
				Try to squeltch the native code stack trace if the exception
				was caused by a reflective call back into the bsh interpreter
				(e.g. eval() or source()
				*/
				bool isNative = true;
				if (te is EvalError)
					if (te is TargetError)
						isNative = ((TargetError) te).inNativeCode();
					else
						isNative = false;
				
				throw new TargetError(msg, te, this, callstack, isNative);
			}
			catch (UtilEvalError e)
			{
				throw e.toEvalError(this, callstack);
			}
		}
	}
}