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
	
	/// <summary> Implement binary expressions... Note: this is too complicated... need some
	/// cleanup and simplification.
	/// 
	/// </summary>
	/// <seealso cref="Primitive.binaryOperation">
	/// </seealso>
	[Serializable]
	class BSHBinaryExpression:SimpleNode, ParserConstants
	{
		public int kind;
		
		internal BSHBinaryExpression(int id):base(id)
		{
		}
		
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			System.Object lhs = ((SimpleNode) jjtGetChild(0)).eval(callstack, interpreter);
			
			/*
			* Doing instanceof? Next node is a type.
			*/
			if (kind == bsh.ParserConstants_Fields.INSTANCEOF)
			{
				// null object ref is not instance of any type
				if (lhs == Primitive.NULL)
					return new Primitive(false);
				
				System.Type rhs = ((BSHType) jjtGetChild(1)).getType(callstack, interpreter);
				/*
				* // primitive (number or void) cannot be tested for instanceof if (lhs
				* instanceof Primitive) throw new
				* EvalError("Cannot be instance of primitive type." );
				*/
				/*
				* Primitive (number or void) is not normally an instanceof anything. But
				* for internal use we'll test true for the bsh.Primitive class. i.e. (5
				* instanceof bsh.Primitive) will be true
				*/
				if (lhs is Primitive)
					if (rhs == typeof(bsh.Primitive))
						return new Primitive(true);
					else
						return new Primitive(false);
				
				// General case - performe the instanceof based on assignability
				bool ret = Types.isJavaBaseAssignable(rhs, lhs.GetType());
				return new Primitive(ret);
			}
			
			// The following two boolean checks were tacked on.
			// This could probably be smoothed out.
			
			/*
			* Look ahead and short circuit evaluation of the rhs if: we're a boolean
			* AND and the lhs is false.
			*/
			if (kind == bsh.ParserConstants_Fields.BOOL_AND || kind == bsh.ParserConstants_Fields.BOOL_ANDX)
			{
				System.Object obj = lhs;
				if (isPrimitiveValue(lhs))
					obj = ((Primitive) lhs).Value;
				if (obj is System.Boolean && (((System.Boolean) obj) == false))
					return new Primitive(false);
			}
			/*
			* Look ahead and short circuit evaluation of the rhs if: we're a boolean
			* AND and the lhs is false.
			*/
			if (kind == bsh.ParserConstants_Fields.BOOL_OR || kind == bsh.ParserConstants_Fields.BOOL_ORX)
			{
				System.Object obj = lhs;
				if (isPrimitiveValue(lhs))
					obj = ((Primitive) lhs).Value;
				if (obj is System.Boolean && (((System.Boolean) obj) == true))
					return new Primitive(true);
			}
			
			// end stuff that was tacked on for boolean short-circuiting.
			
			/*
			* Are both the lhs and rhs either wrappers or primitive values? do binary
			* op
			*/
			bool isLhsWrapper = isWrapper(lhs);
			System.Object rhs2 = ((SimpleNode) jjtGetChild(1)).eval(callstack, interpreter);
			bool isRhsWrapper = isWrapper(rhs2);
			if ((isLhsWrapper || isPrimitiveValue(lhs)) && (isRhsWrapper || isPrimitiveValue(rhs2)))
			{
				// Special case for EQ on two wrapper objects
				if ((isLhsWrapper && isRhsWrapper && kind == bsh.ParserConstants_Fields.EQ))
				{
					/*
					* Don't auto-unwrap wrappers (preserve identity semantics) FALL THROUGH
					* TO OBJECT OPERATIONS BELOW.
					*/
				}
				else
				{
					try
					{
						return Primitive.binaryOperation(lhs, rhs2, kind);
					}
					catch (UtilEvalError e)
					{
						/* VASSAL - Try again, converting both sides to Strings, report the original error if it fails */
						try
						{
							//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							return Primitive.binaryOperation(lhs.ToString(), rhs2.ToString(), kind);
						}
						catch (UtilEvalError e2)
						{
							throw e.toEvalError(this, callstack);
						}
					}
				}
			}
			/*
			* VASSAL Special cases
			* 1. If one side is an Integer and the other is the null String, replace
			*    the null String with a 0.
			* 2. If one side is a Primitive and the other side is a non-null String, convert the Primitive
			*    to a String so that + will work. 
			*/
			else
			{
				// lhs in an Integer and rhs is the null string
				if (lhs is Primitive && ((Primitive) lhs).Value is System.Int32 && rhs2.Equals(""))
				{
					try
					{
						return Primitive.binaryOperation(lhs, new Primitive(0), kind);
					}
					catch (UtilEvalError e)
					{
						throw e.toEvalError(this, callstack);
					}
				}
				// lhs is the null string and rhs is an Integer
				else if (rhs2 is Primitive && ((Primitive) rhs2).Value is System.Int32 && lhs.Equals(""))
				{
					try
					{
						return Primitive.binaryOperation(new Primitive(0), rhs2, kind);
					}
					catch (UtilEvalError e)
					{
						throw e.toEvalError(this, callstack);
					}
				}
				// lhs is primitive, right hand side is string
				else if ((isLhsWrapper || isPrimitiveValue(lhs)) && rhs2 is System.String)
				{
					try
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						return Primitive.binaryOperation(lhs.ToString(), rhs2, kind);
					}
					catch (UtilEvalError e)
					{
						throw e.toEvalError(this, callstack);
					}
				}
				// lhs is string, rhs is primitiv
				else if ((isRhsWrapper || isPrimitiveValue(rhs2)) && lhs is System.String)
				{
					try
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						return Primitive.binaryOperation(lhs, rhs2.ToString(), kind);
					}
					catch (UtilEvalError e)
					{
						throw e.toEvalError(this, callstack);
					}
				}
			}
			/*
			* Doing the following makes it hard to use untyped vars... e.g. if ( arg ==
			* null ) ...what if arg is a primitive? The answer is that we should test
			* only if the var is typed...? need to get that info here...
			* 
			* else { // Do we have a mixture of primitive values and non-primitives ?
			* // (primitiveValue = not null, not void)
			* 
			* int primCount = 0; if ( isPrimitiveValue( lhs ) ) ++primCount; if (
			* isPrimitiveValue( rhs ) ) ++primCount;
			* 
			* if ( primCount > 1 ) // both primitive types, should have been handled
			* above throw new InterpreterError("should not be here"); else if (
			* primCount == 1 ) // mixture of one and the other throw new
			* EvalError("Operator: '" + tokenImage[kind]
			* +"' inappropriate for object / primitive combination.", this, callstack
			* );
			* 
			* // else fall through to handle both non-primitive types
			* 
			* // end check for primitive and non-primitive mix }
			*/
			
			/*
			* Treat lhs and rhs as arbitrary objects and do the operation. (including
			* NULL and VOID represented by their Primitive types)
			*/
			// System.out.println("binary op arbitrary obj: {"+lhs+"}, {"+rhs+"}");
			
			/*
			* VASSAL - Handle Strings as a type of Primitive - allow comparison operators on Strings. 
			*/
			if (lhs is System.String && rhs2 is System.String)
			{
				try
				{
					return Primitive.binaryOperation(lhs, rhs2, kind);
				}
				catch (UtilEvalError e)
				{
					throw e.toEvalError(this, callstack);
				}
			}
			else
			{
				if (lhs is Primitive || rhs2 is Primitive)
					if (lhs == Primitive.VOID || rhs2 == Primitive.VOID)
						throw new EvalError("illegal use of undefined variable, class, or 'void' literal", this, callstack);
					else if (lhs == Primitive.NULL || rhs2 == Primitive.NULL)
						throw new EvalError("illegal use of null value or 'null' literal", this, callstack);
				
				throw new EvalError("Operator: '" + bsh.ParserConstants_Fields.tokenImage[kind] + "' inappropriate for objects", this, callstack);
			}
		}
		
		/*
		* VASSAL - Convert Strings to a Primitive if possible
		*/
		private System.Object convert(System.String obj)
		{
			if (obj.Equals("true"))
			{
				return new Primitive(true);
			}
			else if (obj.Equals("false"))
			{
				return new Primitive(false);
			}
			int i;
			try
			{
				i = System.Int32.Parse(obj);
			}
			catch (System.FormatException e)
			{
				// Not an Error
				return null;
			}
			return new Primitive(i);
		}
		
		/*
		* object is a non-null and non-void Primitive type
		*/
		private bool isPrimitiveValue(System.Object obj)
		{
			return ((obj is Primitive) && (obj != Primitive.VOID) && (obj != Primitive.NULL));
		}
		
		/*
		* object is a java.lang wrapper for boolean, char, or number type
		*/
		private bool isWrapper(System.Object obj)
		{
			return (obj is System.Boolean || obj is System.Char || obj is System.ValueType);
		}
	}
}