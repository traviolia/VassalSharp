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
	
	/// <summary>This class handles both while(){} statements and do{}while() statements.</summary>
	[Serializable]
	class BSHWhileStatement:SimpleNode, ParserConstants
	{
		public bool isDoStatement;
		
		internal BSHWhileStatement(int id):base(id)
		{
		}
		
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			int numChild = jjtGetNumChildren();
			
			// Order of body and condition is swapped for do / while
			SimpleNode condExp, body = null;
			
			if (isDoStatement)
			{
				condExp = (SimpleNode) jjtGetChild(1);
				body = (SimpleNode) jjtGetChild(0);
			}
			else
			{
				condExp = (SimpleNode) jjtGetChild(0);
				if (numChild > 1)
				// has body, else just for side effects
					body = (SimpleNode) jjtGetChild(1);
			}
			
			bool doOnceFlag = isDoStatement;
			while (doOnceFlag || BSHIfStatement.evaluateCondition(condExp, callstack, interpreter))
			{
				if (body == null)
				// no body?
					continue;
				
				System.Object ret = body.eval(callstack, interpreter);
				
				bool breakout = false;
				if (ret is ReturnControl)
				{
					switch (((ReturnControl) ret).kind)
					{
						
						case bsh.ParserConstants_Fields.RETURN: 
							return ret;
						
						
						case bsh.ParserConstants_Fields.CONTINUE: 
							continue;
						
						
						case bsh.ParserConstants_Fields.BREAK: 
							breakout = true;
							break;
						}
				}
				if (breakout)
					break;
				
				doOnceFlag = false;
			}
			
			return Primitive.VOID;
		}
	}
}