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
	class BSHMethodDeclaration:SimpleNode
	{
		virtual internal BSHReturnType ReturnTypeNode
		{
			get
			{
				insureNodesParsed();
				return returnTypeNode;
			}
			
		}
		public System.String name;
		
		// Begin Child node structure evaluated by insureNodesParsed
		
		internal BSHReturnType returnTypeNode;
		internal BSHFormalParameters paramsNode;
		internal BSHBlock blockNode;
		// index of the first throws clause child node
		internal int firstThrowsClause;
		
		// End Child node structure evaluated by insureNodesParsed
		
		public Modifiers modifiers;
		
		// Unsafe caching of type here.
		internal System.Type returnType; // null (none), Void.TYPE, or a Class
		internal int numThrows = 0;
		
		internal BSHMethodDeclaration(int id):base(id)
		{
		}
		
		/// <summary>Set the returnTypeNode, paramsNode, and blockNode based on child
		/// node structure.  No evaluation is done here.
		/// </summary>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'insureNodesParsed'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		internal virtual void  insureNodesParsed()
		{
			lock (this)
			{
				if (paramsNode != null)
				// there is always a paramsNode
					return ;
				
				System.Object firstNode = jjtGetChild(0);
				firstThrowsClause = 1;
				if (firstNode is BSHReturnType)
				{
					returnTypeNode = (BSHReturnType) firstNode;
					paramsNode = (BSHFormalParameters) jjtGetChild(1);
					if (jjtGetNumChildren() > 2 + numThrows)
						blockNode = (BSHBlock) jjtGetChild(2 + numThrows); // skip throws
					++firstThrowsClause;
				}
				else
				{
					paramsNode = (BSHFormalParameters) jjtGetChild(0);
					blockNode = (BSHBlock) jjtGetChild(1 + numThrows); // skip throws
				}
			}
		}
		
		/// <summary>Evaluate the return type node.</summary>
		/// <returns> the type or null indicating loosely typed return
		/// </returns>
		internal virtual System.Type evalReturnType(CallStack callstack, Interpreter interpreter)
		{
			insureNodesParsed();
			if (returnTypeNode != null)
				return returnTypeNode.evalReturnType(callstack, interpreter);
			else
				return null;
		}
		
		internal virtual System.String getReturnTypeDescriptor(CallStack callstack, Interpreter interpreter, System.String defaultPackage)
		{
			insureNodesParsed();
			if (returnTypeNode == null)
				return null;
			else
				return returnTypeNode.getTypeDescriptor(callstack, interpreter, defaultPackage);
		}
		
		/// <summary>Evaluate the declaration of the method.  That is, determine the
		/// structure of the method and install it into the caller's namespace.
		/// </summary>
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			returnType = evalReturnType(callstack, interpreter);
			evalNodes(callstack, interpreter);
			
			// Install an *instance* of this method in the namespace.
			// See notes in BshMethod 
			
			// This is not good...
			// need a way to update eval without re-installing...
			// so that we can re-eval params, etc. when classloader changes
			// look into this
			
			NameSpace namespace_Renamed = callstack.top();
			BshMethod bshMethod = new BshMethod(this, namespace_Renamed, modifiers);
			try
			{
				namespace_Renamed.setMethod(name, bshMethod);
			}
			catch (UtilEvalError e)
			{
				throw e.toEvalError(this, callstack);
			}
			
			return Primitive.VOID;
		}
		
		private void  evalNodes(CallStack callstack, Interpreter interpreter)
		{
			insureNodesParsed();
			
			// validate that the throws names are class names
			for (int i = firstThrowsClause; i < numThrows + firstThrowsClause; i++)
				((BSHAmbiguousName) jjtGetChild(i)).toClass(callstack, interpreter);
			
			paramsNode.eval(callstack, interpreter);
			
			// if strictJava mode, check for loose parameters and return type
			if (interpreter.StrictJava)
			{
				for (int i = 0; i < paramsNode.paramTypes.Length; i++)
					if (paramsNode.paramTypes[i] == null)
					// Warning: Null callstack here.  Don't think we need
					// a stack trace to indicate how we sourced the method.
						throw new EvalError("(Strict Java Mode) Undeclared argument type, parameter: " + paramsNode.ParamNames[i] + " in method: " + name, this, null);
				
				if (returnType == null)
				// Warning: Null callstack here.  Don't think we need
				// a stack trace to indicate how we sourced the method.
					throw new EvalError("(Strict Java Mode) Undeclared return type for method: " + name, this, null);
			}
		}
		
		public override System.String ToString()
		{
			return "MethodDeclaration: " + name;
		}
	}
}