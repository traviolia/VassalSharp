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
	class BSHUnaryExpression:SimpleNode, ParserConstants
	{
		public int kind;
		public bool postfix = false;
		
		internal BSHUnaryExpression(int id):base(id)
		{
		}
		
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			SimpleNode node = (SimpleNode) jjtGetChild(0);
			
			// If this is a unary increment of decrement (either pre or postfix)
			// then we need an LHS to which to assign the result.  Otherwise
			// just do the unary operation for the value.
			try
			{
				if (kind == bsh.ParserConstants_Fields.INCR || kind == bsh.ParserConstants_Fields.DECR)
				{
					LHS lhs = ((BSHPrimaryExpression) node).toLHS(callstack, interpreter);
					return lhsUnaryOperation(lhs, interpreter.StrictJava);
				}
				else
					return unaryOperation(node.eval(callstack, interpreter), kind);
			}
			catch (UtilEvalError e)
			{
				throw e.toEvalError(this, callstack);
			}
		}
		
		private System.Object lhsUnaryOperation(LHS lhs, bool strictJava)
		{
			if (Interpreter.DEBUG)
				Interpreter.debug("lhsUnaryOperation");
			System.Object prevalue, postvalue;
			prevalue = lhs.Value;
			postvalue = unaryOperation(prevalue, kind);
			
			System.Object retVal;
			if (postfix)
				retVal = prevalue;
			else
				retVal = postvalue;
			
			lhs.assign(postvalue, strictJava);
			return retVal;
		}
		
		private System.Object unaryOperation(System.Object op, int kind)
		{
			if (op is System.Boolean || op is System.Char || op is System.ValueType)
				return primitiveWrapperUnaryOperation(op, kind);
			
			if (!(op is Primitive))
				throw new UtilEvalError("Unary operation " + bsh.ParserConstants_Fields.tokenImage[kind] + " inappropriate for object");
			
			
			return Primitive.unaryOperation((Primitive) op, kind);
		}
		
		private System.Object primitiveWrapperUnaryOperation(System.Object val, int kind)
		{
			System.Type operandType = val.GetType();
			System.Object operand = Primitive.promoteToInteger(val);
			
			if (operand is System.Boolean)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return Boolean.valueOf(Primitive.booleanUnaryOperation(ref new System.Boolean[]{(System.Boolean) operand}[0], kind));
			}
			else if (operand is System.Int32)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				int result = Primitive.intUnaryOperation(ref new System.Int32[]{(System.Int32) operand}[0], kind);
				
				// ++ and -- must be cast back the original type
				if (kind == bsh.ParserConstants_Fields.INCR || kind == bsh.ParserConstants_Fields.DECR)
				{
					if (operandType == System.Type.GetType("System.SByte"))
						return Byte.valueOf((sbyte) result);
					if (operandType == System.Type.GetType("System.Int16"))
						return Short.valueOf((short) result);
					if (operandType == System.Type.GetType("System.Char"))
						return Character.valueOf((char) result);
				}
				
				return Integer.valueOf(result);
			}
			else if (operand is System.Int64)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return Long.valueOf(Primitive.longUnaryOperation(ref new System.Int64[]{(System.Int64) operand}[0], kind));
			}
			else if (operand is System.Single)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return Float.valueOf(Primitive.floatUnaryOperation(ref new System.Single[]{(System.Single) operand}[0], kind));
			}
			else if (operand is System.Double)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return Double.valueOf(Primitive.doubleUnaryOperation(ref new System.Double[]{(System.Double) operand}[0], kind));
			}
			else
				throw new InterpreterError("An error occurred.  Please call technical support.");
		}
	}
}