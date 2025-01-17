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
	class BSHIfStatement:SimpleNode
	{
		internal BSHIfStatement(int id):base(id)
		{
		}
		
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			System.Object ret = null;
			
			if (evaluateCondition((SimpleNode) jjtGetChild(0), callstack, interpreter))
				ret = ((SimpleNode) jjtGetChild(1)).eval(callstack, interpreter);
			else if (jjtGetNumChildren() > 2)
				ret = ((SimpleNode) jjtGetChild(2)).eval(callstack, interpreter);
			
			if (ret is ReturnControl)
				return ret;
			else
				return Primitive.VOID;
		}
		
		public static bool evaluateCondition(SimpleNode condExp, CallStack callstack, Interpreter interpreter)
		{
			System.Object obj = condExp.eval(callstack, interpreter);
			if (obj is Primitive)
			{
				if (obj == Primitive.VOID)
					throw new EvalError("Condition evaluates to void type", condExp, callstack);
				obj = ((Primitive) obj).Value;
			}
			
			if (obj is System.Boolean)
				return ((System.Boolean) obj);
			else
				throw new EvalError("Condition must evaluate to a Boolean or boolean.", condExp, callstack);
		}
	}
}