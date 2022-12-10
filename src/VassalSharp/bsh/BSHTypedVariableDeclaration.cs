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
	class BSHTypedVariableDeclaration:SimpleNode
	{
		private BSHType TypeNode
		{
			get
			{
				return ((BSHType) jjtGetChild(0));
			}
			
		}
		virtual internal BSHVariableDeclarator[] Declarators
		{
			get
			{
				int n = jjtGetNumChildren();
				int start = 1;
				BSHVariableDeclarator[] bvda = new BSHVariableDeclarator[n - start];
				for (int i = start; i < n; i++)
				{
					bvda[i - start] = (BSHVariableDeclarator) jjtGetChild(i);
				}
				return bvda;
			}
			
		}
		public Modifiers modifiers;
		
		internal BSHTypedVariableDeclaration(int id):base(id)
		{
		}
		
		internal virtual System.Type evalType(CallStack callstack, Interpreter interpreter)
		{
			BSHType typeNode = TypeNode;
			return typeNode.getType(callstack, interpreter);
		}
		
		/// <summary>evaluate the type and one or more variable declarators, e.g.:
		/// int a, b=5, c;
		/// </summary>
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			try
			{
				NameSpace namespace_Renamed = callstack.top();
				BSHType typeNode = TypeNode;
				System.Type type = typeNode.getType(callstack, interpreter);
				
				BSHVariableDeclarator[] bvda = Declarators;
				for (int i = 0; i < bvda.Length; i++)
				{
					BSHVariableDeclarator dec = bvda[i];
					
					// Type node is passed down the chain for array initializers
					// which need it under some circumstances
					System.Object value_Renamed = dec.eval(typeNode, callstack, interpreter);
					
					try
					{
						namespace_Renamed.setTypedVariable(dec.name, type, value_Renamed, modifiers);
					}
					catch (UtilEvalError e)
					{
						throw e.toEvalError(this, callstack);
					}
				}
			}
			catch (EvalError e)
			{
				e.reThrow("Typed variable declaration");
			}
			
			return Primitive.VOID;
		}
		
		public virtual System.String getTypeDescriptor(CallStack callstack, Interpreter interpreter, System.String defaultPackage)
		{
			return TypeNode.getTypeDescriptor(callstack, interpreter, defaultPackage);
		}
	}
}