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
	
	/// <summary>Implement casts.
	/// I think it should be possible to simplify some of the code here by
	/// using the Types.getAssignableForm() method, but I haven't looked 
	/// into it.
	/// </summary>
	[Serializable]
	class BSHCastExpression:SimpleNode
	{
		
		public BSHCastExpression(int id):base(id)
		{
		}
		
		/// <returns> the result of the cast.
		/// </returns>
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			NameSpace namespace_Renamed = callstack.top();
			System.Type toType = ((BSHType) jjtGetChild(0)).getType(callstack, interpreter);
			SimpleNode expression = (SimpleNode) jjtGetChild(1);
			
			// evaluate the expression
			System.Object fromValue = expression.eval(callstack, interpreter);
			System.Type fromType = fromValue.GetType();
			
			// TODO: need to add isJavaCastable() test for strictJava
			// (as opposed to isJavaAssignable())
			try
			{
				return Types.castObject(fromValue, toType, Types.CAST);
			}
			catch (UtilEvalError e)
			{
				throw e.toEvalError(this, callstack);
			}
		}
	}
}