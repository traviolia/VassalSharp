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
	
	/// <summary>name [ = initializer ]
	/// evaluate name and return optional initializer
	/// </summary>
	[Serializable]
	class BSHVariableDeclarator:SimpleNode
	{
		// The token.image text of the name... never changes.
		public System.String name;
		
		internal BSHVariableDeclarator(int id):base(id)
		{
		}
		
		/// <summary>Evaluate the optional initializer value.  
		/// (The name was set at parse time.)
		/// A variable declarator can be evaluated with or without preceding
		/// type information. Currently the type info is only used by array 
		/// initializers in the case where there is no explicitly declared type.
		/// </summary>
		/// <param name="typeNode">is the BSHType node.  Its info is passed through to any
		/// variable intializer children for the case where the array initializer
		/// does not declare the type explicitly. e.g.
		/// int [] a = { 1, 2 };
		/// typeNode may be null to indicate no type information available.
		/// </param>
		public virtual System.Object eval(BSHType typeNode, CallStack callstack, Interpreter interpreter)
		{
			// null value means no value
			System.Object value_Renamed = null;
			
			if (jjtGetNumChildren() > 0)
			{
				SimpleNode initializer = (SimpleNode) jjtGetChild(0);
				
				/*
				If we have type info and the child is an array initializer
				pass it along...  Else use the default eval style.
				(This allows array initializer to handle the problem... 
				allowing for future enhancements in loosening types there).	
				*/
				if ((typeNode != null) && initializer is BSHArrayInitializer)
					value_Renamed = ((BSHArrayInitializer) initializer).eval(typeNode.BaseType, typeNode.ArrayDims, callstack, interpreter);
				else
					value_Renamed = initializer.eval(callstack, interpreter);
			}
			
			if (value_Renamed == Primitive.VOID)
				throw new EvalError("Void initializer.", this, callstack);
			
			return value_Renamed;
		}
		
		public override System.String ToString()
		{
			return "BSHVariableDeclarator " + name;
		}
	}
}