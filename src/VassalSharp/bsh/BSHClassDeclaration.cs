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
	
	/// 
	[Serializable]
	class BSHClassDeclaration:SimpleNode
	{
		/// <summary>The class instance initializer method name.
		/// A BshMethod by this name is installed by the class delcaration into 
		/// the static class body namespace.  
		/// It is called once to initialize the static members of the class space 
		/// and each time an instances is created to initialize the instance
		/// members.
		/// </summary>
		internal const System.String CLASSINITNAME = "_bshClassInit";
		
		internal System.String name;
		internal Modifiers modifiers;
		internal int numInterfaces;
		internal bool extend;
		internal bool isInterface;
		
		internal BSHClassDeclaration(int id):base(id)
		{
		}
		
		
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			int child = 0;
			
			// resolve superclass if any
			System.Type superClass = null;
			if (extend)
			{
				BSHAmbiguousName superNode = (BSHAmbiguousName) jjtGetChild(child++);
				superClass = superNode.toClass(callstack, interpreter);
			}
			
			// Get interfaces
			System.Type[] interfaces = new System.Type[numInterfaces];
			for (int i = 0; i < numInterfaces; i++)
			{
				BSHAmbiguousName node = (BSHAmbiguousName) jjtGetChild(child++);
				interfaces[i] = node.toClass(callstack, interpreter);
				if (!interfaces[i].IsInterface)
					throw new EvalError("Type: " + node.text + " is not an interface!", this, callstack);
			}
			
			BSHBlock block;
			// Get the class body BSHBlock
			if (child < jjtGetNumChildren())
				block = (BSHBlock) jjtGetChild(child);
			else
				block = new BSHBlock(bsh.ParserTreeConstants_Fields.JJTBLOCK);
			
			try
			{
				return ClassGenerator.getClassGenerator().generateClass(name, modifiers, interfaces, superClass, block, isInterface, callstack, interpreter);
			}
			catch (UtilEvalError e)
			{
				throw e.toEvalError(this, callstack);
			}
		}
		
		public override System.String ToString()
		{
			return "ClassDeclaration: " + name;
		}
	}
}