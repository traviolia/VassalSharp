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
using System.Runtime.InteropServices;
namespace bsh
{
	
	[Serializable]
	public class DelayedEvalBshMethod:BshMethod
	{
		virtual public System.String ReturnTypeDescriptor
		{
			get
			{
				return returnTypeDescriptor;
			}
			
		}
		override public System.Type ReturnType
		{
			get
			{
				if (returnTypeNode == null)
					return null;
				
				// BSHType will cache the type for us
				try
				{
					return returnTypeNode.evalReturnType(callstack, interpreter);
				}
				catch (EvalError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new InterpreterError("can't eval return type: " + e);
				}
			}
			
		}
		virtual public System.String[] ParamTypeDescriptors
		{
			get
			{
				return paramTypeDescriptors;
			}
			
		}
		override public System.Type[] ParameterTypes
		{
			get
			{
				// BSHFormalParameters will cache the type for us
				try
				{
					return (System.Type[]) paramTypesNode.eval(callstack, interpreter);
				}
				catch (EvalError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new InterpreterError("can't eval param types: " + e);
				}
			}
			
		}
		internal System.String returnTypeDescriptor;
		internal BSHReturnType returnTypeNode;
		internal System.String[] paramTypeDescriptors;
		internal BSHFormalParameters paramTypesNode;
		
		// used for the delayed evaluation...
		[NonSerialized]
		internal CallStack callstack;
		[NonSerialized]
		internal Interpreter interpreter;
		
		/// <summary>This constructor is used in class generation.  It supplies String type
		/// descriptors for return and parameter class types and allows delay of 
		/// the evaluation of those types until they are requested.  It does this
		/// by holding BSHType nodes, as well as an evaluation callstack, and
		/// interpreter which are called when the class types are requested. 
		/// </summary>
		/*
		Note: technically I think we could get by passing in only the
		current namespace or perhaps BshClassManager here instead of 
		CallStack and Interpreter.  However let's just play it safe in case
		of future changes - anywhere you eval a node you need these.
		*/
		internal DelayedEvalBshMethod(System.String name, System.String returnTypeDescriptor, BSHReturnType returnTypeNode, System.String[] paramNames, System.String[] paramTypeDescriptors, BSHFormalParameters paramTypesNode, BSHBlock methodBody, NameSpace declaringNameSpace, Modifiers modifiers, CallStack callstack, Interpreter interpreter):base(name, null, paramNames, null, methodBody, declaringNameSpace, modifiers)
		{
			
			this.returnTypeDescriptor = returnTypeDescriptor;
			this.returnTypeNode = returnTypeNode;
			this.paramTypeDescriptors = paramTypeDescriptors;
			this.paramTypesNode = paramTypesNode;
			this.callstack = callstack;
			this.interpreter = interpreter;
		}
	}
}