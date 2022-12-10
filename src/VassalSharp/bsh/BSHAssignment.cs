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
	class BSHAssignment:SimpleNode, ParserConstants
	{
		public int operator_Renamed;
		
		internal BSHAssignment(int id):base(id)
		{
		}
		
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			BSHPrimaryExpression lhsNode = (BSHPrimaryExpression) jjtGetChild(0);
			
			if (lhsNode == null)
				throw new InterpreterError("Error, null LHSnode");
			
			bool strictJava = interpreter.StrictJava;
			LHS lhs = lhsNode.toLHS(callstack, interpreter);
			if (lhs == null)
				throw new InterpreterError("Error, null LHS");
			
			// For operator-assign operations save the lhs value before evaluating
			// the rhs.  This is correct Java behavior for postfix operations
			// e.g. i=1; i+=i++; // should be 2 not 3
			System.Object lhsValue = null;
			if (operator_Renamed != bsh.ParserConstants_Fields.ASSIGN)
			// assign doesn't need the pre-value
				try
				{
					lhsValue = lhs.Value;
				}
				catch (UtilEvalError e)
				{
					throw e.toEvalError(this, callstack);
				}
			
			SimpleNode rhsNode = (SimpleNode) jjtGetChild(1);
			
			System.Object rhs;
			
			// implement "blocks" foo = { };
			// if ( rhsNode instanceof BSHBlock )
			//    rsh =
			// else
			rhs = rhsNode.eval(callstack, interpreter);
			
			if (rhs == Primitive.VOID)
				throw new EvalError("Void assignment.", this, callstack);
			
			try
			{
				switch (operator_Renamed)
				{
					
					case bsh.ParserConstants_Fields.ASSIGN: 
						return lhs.assign(rhs, strictJava);
					
					
					case bsh.ParserConstants_Fields.PLUSASSIGN: 
						return lhs.assign(operation(lhsValue, rhs, bsh.ParserConstants_Fields.PLUS), strictJava);
					
					
					case bsh.ParserConstants_Fields.MINUSASSIGN: 
						return lhs.assign(operation(lhsValue, rhs, bsh.ParserConstants_Fields.MINUS), strictJava);
					
					
					case bsh.ParserConstants_Fields.STARASSIGN: 
						return lhs.assign(operation(lhsValue, rhs, bsh.ParserConstants_Fields.STAR), strictJava);
					
					
					case bsh.ParserConstants_Fields.SLASHASSIGN: 
						return lhs.assign(operation(lhsValue, rhs, bsh.ParserConstants_Fields.SLASH), strictJava);
					
					
					case bsh.ParserConstants_Fields.ANDASSIGN: 
					case bsh.ParserConstants_Fields.ANDASSIGNX: 
						return lhs.assign(operation(lhsValue, rhs, bsh.ParserConstants_Fields.BIT_AND), strictJava);
					
					
					case bsh.ParserConstants_Fields.ORASSIGN: 
					case bsh.ParserConstants_Fields.ORASSIGNX: 
						return lhs.assign(operation(lhsValue, rhs, bsh.ParserConstants_Fields.BIT_OR), strictJava);
					
					
					case bsh.ParserConstants_Fields.XORASSIGN: 
						return lhs.assign(operation(lhsValue, rhs, bsh.ParserConstants_Fields.XOR), strictJava);
					
					
					case bsh.ParserConstants_Fields.MODASSIGN: 
						return lhs.assign(operation(lhsValue, rhs, bsh.ParserConstants_Fields.MOD), strictJava);
					
					
					case bsh.ParserConstants_Fields.LSHIFTASSIGN: 
					case bsh.ParserConstants_Fields.LSHIFTASSIGNX: 
						return lhs.assign(operation(lhsValue, rhs, bsh.ParserConstants_Fields.LSHIFT), strictJava);
					
					
					case bsh.ParserConstants_Fields.RSIGNEDSHIFTASSIGN: 
					case bsh.ParserConstants_Fields.RSIGNEDSHIFTASSIGNX: 
						return lhs.assign(operation(lhsValue, rhs, bsh.ParserConstants_Fields.RSIGNEDSHIFT), strictJava);
					
					
					case bsh.ParserConstants_Fields.RUNSIGNEDSHIFTASSIGN: 
					case bsh.ParserConstants_Fields.RUNSIGNEDSHIFTASSIGNX: 
						return lhs.assign(operation(lhsValue, rhs, bsh.ParserConstants_Fields.RUNSIGNEDSHIFT), strictJava);
					
					
					default: 
						throw new InterpreterError("unimplemented operator in assignment BSH");
					
				}
			}
			catch (UtilEvalError e)
			{
				throw e.toEvalError(this, callstack);
			}
		}
		
		private System.Object operation(System.Object lhs, System.Object rhs, int kind)
		{
			/*
			Implement String += value;
			According to the JLS, value may be anything.
			In BeanShell, we'll disallow VOID (undefined) values.
			(or should we map them to the empty string?)
			*/
			if (lhs is System.String && rhs != Primitive.VOID)
			{
				if (kind != bsh.ParserConstants_Fields.PLUS)
					throw new UtilEvalError("Use of non + operator with String LHS");
				
				return (System.String) lhs + rhs;
			}
			
			if (lhs is Primitive || rhs is Primitive)
				if (lhs == Primitive.VOID || rhs == Primitive.VOID)
					throw new UtilEvalError("Illegal use of undefined object or 'void' literal");
				else if (lhs == Primitive.NULL || rhs == Primitive.NULL)
					throw new UtilEvalError("Illegal use of null object or 'null' literal");
			
			
			if ((lhs is System.Boolean || lhs is System.Char || lhs is System.ValueType || lhs is Primitive) && (rhs is System.Boolean || rhs is System.Char || rhs is System.ValueType || rhs is Primitive))
			{
				return Primitive.binaryOperation(lhs, rhs, kind);
			}
			
			throw new UtilEvalError("Non primitive value in operator: " + lhs.GetType() + " " + bsh.ParserConstants_Fields.tokenImage[kind] + " " + rhs.GetType());
		}
	}
}