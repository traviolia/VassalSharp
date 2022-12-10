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
//UPGRADE_TODO: The type 'java.util.regex.Pattern' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Pattern = java.util.regex.Pattern;
namespace bsh
{
	
	/// <summary>Wrapper for primitive types in Bsh.  This is package public because it 
	/// is used in the implementation of some bsh commands.
	/// See the note in LHS.java about wrapping objects.
	/// </summary>
	/*
	Note: this class is final because we may test == Primitive.class in places.
	If we need to change that search for those tests.*/
	[Serializable]
	public sealed class Primitive : ParserConstants
	{
		/// <summary>Return the primitive value stored in its java.lang wrapper class</summary>
		public System.Object Value
		{
			get
			{
				if (value_Renamed == Special.NULL_VALUE)
					return null;
				else if (value_Renamed == Special.VOID_TYPE)
					throw new InterpreterError("attempt to unwrap void type");
				else
					return value_Renamed;
			}
			
		}
		/// <summary>Get the corresponding Java primitive TYPE class for this Primitive.</summary>
		/// <returns> the primitive TYPE class type of the value or Void.TYPE for
		/// Primitive.VOID or null value for type of Primitive.NULL
		/// </returns>
		public System.Type Type
		{
			get
			{
				if (this == Primitive.VOID)
				{
					return System.Type.GetType("System.Void");
				}
				
				// NULL return null as type... we currently use null type to indicate 
				// loose typing throughout bsh.
				if (this == Primitive.NULL)
					return null;
				
				return unboxType(value_Renamed.GetType());
			}
			
		}
		/// <summary>Determine if this primitive is a numeric type.
		/// i.e. not boolean, null, or void (but including char)
		/// </summary>
		public bool Number
		{
			get
			{
				return (!(value_Renamed is System.Boolean) && !(this == NULL) && !(this == VOID));
			}
			
		}
		/*
		static Hashtable primitiveToWrapper = new Hashtable();
		static Hashtable wrapperToPrimitive = new Hashtable();
		static {
		primitiveToWrapper.put( Boolean.TYPE, Boolean.class );
		primitiveToWrapper.put( Byte.TYPE, Byte.class );
		primitiveToWrapper.put( Short.TYPE, Short.class );
		primitiveToWrapper.put( Character.TYPE, Character.class );
		primitiveToWrapper.put( Integer.TYPE, Integer.class );
		primitiveToWrapper.put( Long.TYPE, Long.class );
		primitiveToWrapper.put( Float.TYPE, Float.class );
		primitiveToWrapper.put( Double.TYPE, Double.class );
		wrapperToPrimitive.put( Boolean.class, Boolean.TYPE );
		wrapperToPrimitive.put( Byte.class, Byte.TYPE );
		wrapperToPrimitive.put( Short.class, Short.TYPE );
		wrapperToPrimitive.put( Character.class, Character.TYPE );
		wrapperToPrimitive.put( Integer.class, Integer.TYPE );
		wrapperToPrimitive.put( Long.class, Long.TYPE );
		wrapperToPrimitive.put( Float.class, Float.TYPE );
		wrapperToPrimitive.put( Double.class, Double.TYPE );
		}
		*/
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		static final Map < Class, Class > wrapperMap = new HashMap < Class, Class >();
		
		/// <summary>The primitive value stored in its java.lang wrapper class </summary>
		private System.Object value_Renamed;
		
		[Serializable]
		private class Special
		{
			internal Special()
			{
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'NULL_VALUE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			public static readonly Special NULL_VALUE = new Special();
			//UPGRADE_NOTE: Final was removed from the declaration of 'VOID_TYPE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			public static readonly Special VOID_TYPE = new Special();
		}
		
		/*
		NULL means "no value".
		This ia a placeholder for primitive null value.
		*/
		//UPGRADE_NOTE: Final was removed from the declaration of 'NULL '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly Primitive NULL = new Primitive(Special.NULL_VALUE);
		
		/// <summary>VOID means "no type".
		/// Strictly speaking, this makes no sense here.  But for practical
		/// reasons we'll consider the lack of a type to be a special value.
		/// </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'VOID '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly Primitive VOID = new Primitive(Special.VOID_TYPE);
		
		// private to prevent invocation with param that isn't a primitive-wrapper
		public Primitive(System.Object value_Renamed)
		{
			if (value_Renamed == null)
				throw new InterpreterError("Use Primitve.NULL instead of Primitive(null)");
			
			if (value_Renamed != Special.NULL_VALUE && value_Renamed != Special.VOID_TYPE && !isWrapperType(value_Renamed.GetType()))
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new InterpreterError("Not a wrapper type: " + value_Renamed);
			}
			
			this.value_Renamed = value_Renamed;
		}
		
		public Primitive(bool value_Renamed):this(Boolean.valueOf(value_Renamed))
		{
		}
		public Primitive(sbyte value_Renamed):this(Byte.valueOf(value_Renamed))
		{
		}
		public Primitive(short value_Renamed):this(Short.valueOf(value_Renamed))
		{
		}
		public Primitive(char value_Renamed):this(Character.valueOf(value_Renamed))
		{
		}
		public Primitive(int value_Renamed):this(Integer.valueOf(value_Renamed))
		{
		}
		public Primitive(long value_Renamed):this(Long.valueOf(value_Renamed))
		{
		}
		public Primitive(float value_Renamed):this(Float.valueOf(value_Renamed))
		{
		}
		public Primitive(double value_Renamed):this(Double.valueOf(value_Renamed))
		{
		}
		
		public override System.String ToString()
		{
			if (value_Renamed == Special.NULL_VALUE)
				return "null";
			else if (value_Renamed == Special.VOID_TYPE)
				return "void";
			else
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return value_Renamed.ToString();
			}
		}
		
		/// <summary>Perform a binary operation on two Primitives or wrapper types.
		/// If both original args were Primitives return a Primitive result
		/// else it was mixed (wrapper/primitive) return the wrapper type.
		/// The exception is for boolean operations where we will return the 
		/// primitive type either way.
		/// </summary>
		public static System.Object binaryOperation(System.Object obj1, System.Object obj2, int kind)
		{
			// special primitive types
			if (obj1 == NULL || obj2 == NULL)
				throw new UtilEvalError("Null value or 'null' literal in binary operation");
			if (obj1 == VOID || obj2 == VOID)
				throw new UtilEvalError("Undefined variable, class, or 'void' literal in binary operation");
			
			// keep track of the original types
			System.Type lhsOrgType = obj1.GetType();
			System.Type rhsOrgType = obj2.GetType();
			
			// Unwrap primitives
			if (obj1 is Primitive)
				obj1 = ((Primitive) obj1).Value;
			if (obj2 is Primitive)
				obj2 = ((Primitive) obj2).Value;
			
			System.Object[] operands = promotePrimitives(obj1, obj2);
			System.Object lhs = operands[0];
			System.Object rhs = operands[1];
			
			if (lhs.GetType() != rhs.GetType())
				throw new UtilEvalError("Type mismatch in operator.  " + lhs.GetType() + " cannot be used with " + rhs.GetType());
			
			System.Object result;
			try
			{
				result = binaryOperationImpl(lhs, rhs, kind);
			}
			catch (System.ArithmeticException e)
			{
				throw new UtilTargetError("Arithemetic Exception in binary op", e);
			}
			
			// If both original args were Primitives return a Primitive result
			// else it was mixed (wrapper/primitive) return the wrapper type
			// Exception is for boolean result, return the primitive
			if ((lhsOrgType == typeof(Primitive) && rhsOrgType == typeof(Primitive)) || result is System.Boolean)
				return new Primitive(result);
			else
				return result;
		}
		
		internal static System.Object binaryOperationImpl(System.Object lhs, System.Object rhs, int kind)
		{
			if (lhs is System.Boolean)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return booleanBinaryOperation(ref new System.Boolean[]{(System.Boolean) lhs}[0], ref new System.Boolean[]{(System.Boolean) rhs}[0], kind);
			}
			else if (lhs is System.Int32)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return intBinaryOperation(ref new System.Int32[]{(System.Int32) lhs}[0], ref new System.Int32[]{(System.Int32) rhs}[0], kind);
			}
			else if (lhs is System.Int64)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return longBinaryOperation(ref new System.Int64[]{(System.Int64) lhs}[0], ref new System.Int64[]{(System.Int64) rhs}[0], kind);
			}
			else if (lhs is System.Single)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return floatBinaryOperation(ref new System.Single[]{(System.Single) lhs}[0], ref new System.Single[]{(System.Single) rhs}[0], kind);
			}
			else if (lhs is System.Double)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return doubleBinaryOperation(ref new System.Double[]{(System.Double) lhs}[0], ref new System.Double[]{(System.Double) rhs}[0], kind);
			}
			/*
			* VASSAL - Allow comparison operators on Strings
			*/
			else if (lhs is System.String)
				return stringBinaryOperation((System.String) lhs, (System.String) rhs, kind);
			else
				throw new UtilEvalError("Invalid types in binary operator");
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		internal static System.Boolean booleanBinaryOperation(ref System.Boolean B1, ref System.Boolean B2, int kind)
		{
			bool lhs = B1;
			bool rhs = B2;
			
			switch (kind)
			{
				
				case bsh.ParserConstants_Fields.EQ: 
					return Boolean.valueOf(lhs == rhs);
				
				
				case bsh.ParserConstants_Fields.NE: 
					return Boolean.valueOf(lhs != rhs);
				
				
				case bsh.ParserConstants_Fields.BOOL_OR: 
				case bsh.ParserConstants_Fields.BOOL_ORX: 
					return Boolean.valueOf(lhs || rhs);
				
				
				case bsh.ParserConstants_Fields.BOOL_AND: 
				case bsh.ParserConstants_Fields.BOOL_ANDX: 
					return Boolean.valueOf(lhs && rhs);
				
				
				default: 
					throw new InterpreterError("unimplemented binary operator");
				
			}
		}
		
		// returns Object covering both Long and Boolean return types
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		internal static System.Object longBinaryOperation(ref System.Int64 L1, ref System.Int64 L2, int kind)
		{
			long lhs = (long) L1;
			long rhs = (long) L2;
			
			switch (kind)
			{
				
				// boolean
				case bsh.ParserConstants_Fields.LT: 
				case bsh.ParserConstants_Fields.LTX: 
					return Boolean.valueOf(lhs < rhs);
				
				
				case bsh.ParserConstants_Fields.GT: 
				case bsh.ParserConstants_Fields.GTX: 
					return Boolean.valueOf(lhs > rhs);
				
				
				case bsh.ParserConstants_Fields.EQ: 
					return Boolean.valueOf(lhs == rhs);
				
				
				case bsh.ParserConstants_Fields.LE: 
				case bsh.ParserConstants_Fields.LEX: 
					return Boolean.valueOf(lhs <= rhs);
				
				
				case bsh.ParserConstants_Fields.GE: 
				case bsh.ParserConstants_Fields.GEX: 
					return Boolean.valueOf(lhs >= rhs);
				
				
				case bsh.ParserConstants_Fields.NE: 
					return Boolean.valueOf(lhs != rhs);
					
					// arithmetic
				
				case bsh.ParserConstants_Fields.PLUS: 
					return Long.valueOf(lhs + rhs);
				
				
				case bsh.ParserConstants_Fields.MINUS: 
					return Long.valueOf(lhs - rhs);
				
				
				case bsh.ParserConstants_Fields.STAR: 
					return Long.valueOf(lhs * rhs);
				
				
				case bsh.ParserConstants_Fields.SLASH: 
					return Long.valueOf(lhs / rhs);
				
				
				case bsh.ParserConstants_Fields.MOD: 
					return Long.valueOf(lhs % rhs);
					
					// bitwise
				
				case bsh.ParserConstants_Fields.LSHIFT: 
				case bsh.ParserConstants_Fields.LSHIFTX: 
					return Long.valueOf(lhs << (int) rhs);
				
				
				case bsh.ParserConstants_Fields.RSIGNEDSHIFT: 
				case bsh.ParserConstants_Fields.RSIGNEDSHIFTX: 
					return Long.valueOf(lhs >> (int) rhs);
				
				
				case bsh.ParserConstants_Fields.RUNSIGNEDSHIFT: 
				case bsh.ParserConstants_Fields.RUNSIGNEDSHIFTX: 
					return Long.valueOf(SupportClass.URShift(lhs, rhs));
				
				
				case bsh.ParserConstants_Fields.BIT_AND: 
				case bsh.ParserConstants_Fields.BIT_ANDX: 
					return Long.valueOf(lhs & rhs);
				
				
				case bsh.ParserConstants_Fields.BIT_OR: 
				case bsh.ParserConstants_Fields.BIT_ORX: 
					return Long.valueOf(lhs | rhs);
				
				
				case bsh.ParserConstants_Fields.XOR: 
					return Long.valueOf(lhs ^ rhs);
				
				
				default: 
					throw new InterpreterError("Unimplemented binary long operator");
				
			}
		}
		
		// returns Object covering both Integer and Boolean return types
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		internal static System.Object intBinaryOperation(ref System.Int32 I1, ref System.Int32 I2, int kind)
		{
			int lhs = I1;
			int rhs = I2;
			
			switch (kind)
			{
				
				// boolean
				case bsh.ParserConstants_Fields.LT: 
				case bsh.ParserConstants_Fields.LTX: 
					return Boolean.valueOf(lhs < rhs);
				
				
				case bsh.ParserConstants_Fields.GT: 
				case bsh.ParserConstants_Fields.GTX: 
					return Boolean.valueOf(lhs > rhs);
				
				
				case bsh.ParserConstants_Fields.EQ: 
					return Boolean.valueOf(lhs == rhs);
				
				
				case bsh.ParserConstants_Fields.LE: 
				case bsh.ParserConstants_Fields.LEX: 
					return Boolean.valueOf(lhs <= rhs);
				
				
				case bsh.ParserConstants_Fields.GE: 
				case bsh.ParserConstants_Fields.GEX: 
					return Boolean.valueOf(lhs >= rhs);
				
				
				case bsh.ParserConstants_Fields.NE: 
					return Boolean.valueOf(lhs != rhs);
					
					// arithmetic
				
				case bsh.ParserConstants_Fields.PLUS: 
					return Integer.valueOf(lhs + rhs);
				
				
				case bsh.ParserConstants_Fields.MINUS: 
					return Integer.valueOf(lhs - rhs);
				
				
				case bsh.ParserConstants_Fields.STAR: 
					return Integer.valueOf(lhs * rhs);
				
				
				case bsh.ParserConstants_Fields.SLASH: 
					return Integer.valueOf(lhs / rhs);
				
				
				case bsh.ParserConstants_Fields.MOD: 
					return Integer.valueOf(lhs % rhs);
					
					// bitwise
				
				case bsh.ParserConstants_Fields.LSHIFT: 
				case bsh.ParserConstants_Fields.LSHIFTX: 
					return Integer.valueOf(lhs << rhs);
				
				
				case bsh.ParserConstants_Fields.RSIGNEDSHIFT: 
				case bsh.ParserConstants_Fields.RSIGNEDSHIFTX: 
					return Integer.valueOf(lhs >> rhs);
				
				
				case bsh.ParserConstants_Fields.RUNSIGNEDSHIFT: 
				case bsh.ParserConstants_Fields.RUNSIGNEDSHIFTX: 
					return Integer.valueOf(SupportClass.URShift(lhs, rhs));
				
				
				case bsh.ParserConstants_Fields.BIT_AND: 
				case bsh.ParserConstants_Fields.BIT_ANDX: 
					return Integer.valueOf(lhs & rhs);
				
				
				case bsh.ParserConstants_Fields.BIT_OR: 
				case bsh.ParserConstants_Fields.BIT_ORX: 
					return Integer.valueOf(lhs | rhs);
				
				
				case bsh.ParserConstants_Fields.XOR: 
					return Integer.valueOf(lhs ^ rhs);
				
				
				default: 
					throw new InterpreterError("Unimplemented binary integer operator");
				
			}
		}
		
		// Apply comparison operators to Strings
		internal static System.Object stringBinaryOperation(System.String lhs, System.String rhs, int kind)
		{
			switch (kind)
			{
				
				// boolean
				case bsh.ParserConstants_Fields.LT: 
				case bsh.ParserConstants_Fields.LTX: 
					return Boolean.valueOf(String.CompareOrdinal(lhs, rhs) < 0);
				
				
				case bsh.ParserConstants_Fields.GT: 
				case bsh.ParserConstants_Fields.GTX: 
					return Boolean.valueOf(String.CompareOrdinal(lhs, rhs) > 0);
				
				
				case bsh.ParserConstants_Fields.EQ: 
					return Boolean.valueOf(lhs.Equals(rhs));
				
				
				case bsh.ParserConstants_Fields.LE: 
				case bsh.ParserConstants_Fields.LEX: 
					return Boolean.valueOf(String.CompareOrdinal(lhs, rhs) <= 0);
				
				
				case bsh.ParserConstants_Fields.GE: 
				case bsh.ParserConstants_Fields.GEX: 
					return Boolean.valueOf(String.CompareOrdinal(lhs, rhs) >= 0);
				
				
				case bsh.ParserConstants_Fields.NE: 
					return Boolean.valueOf(!lhs.Equals(rhs));
				
				
				case bsh.ParserConstants_Fields.PLUS: 
					return lhs + rhs;
				
				
				case bsh.ParserConstants_Fields.MATCH: 
					return Boolean.valueOf(Pattern.matches(rhs, lhs));
				
				
				default: 
					throw new InterpreterError("Unimplemented binary String operator");
				
			}
		}
		
		// returns Object covering both Double and Boolean return types
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		internal static System.Object doubleBinaryOperation(ref System.Double D1, ref System.Double D2, int kind)
		{
			double lhs = D1;
			double rhs = D2;
			
			switch (kind)
			{
				
				// boolean
				case bsh.ParserConstants_Fields.LT: 
				case bsh.ParserConstants_Fields.LTX: 
					return Boolean.valueOf(lhs < rhs);
				
				
				case bsh.ParserConstants_Fields.GT: 
				case bsh.ParserConstants_Fields.GTX: 
					return Boolean.valueOf(lhs > rhs);
				
				
				case bsh.ParserConstants_Fields.EQ: 
					return Boolean.valueOf(lhs == rhs);
				
				
				case bsh.ParserConstants_Fields.LE: 
				case bsh.ParserConstants_Fields.LEX: 
					return Boolean.valueOf(lhs <= rhs);
				
				
				case bsh.ParserConstants_Fields.GE: 
				case bsh.ParserConstants_Fields.GEX: 
					return Boolean.valueOf(lhs >= rhs);
				
				
				case bsh.ParserConstants_Fields.NE: 
					return Boolean.valueOf(lhs != rhs);
					
					// arithmetic
				
				case bsh.ParserConstants_Fields.PLUS: 
					return Double.valueOf(lhs + rhs);
				
				
				case bsh.ParserConstants_Fields.MINUS: 
					return Double.valueOf(lhs - rhs);
				
				
				case bsh.ParserConstants_Fields.STAR: 
					return Double.valueOf(lhs * rhs);
				
				
				case bsh.ParserConstants_Fields.SLASH: 
					return Double.valueOf(lhs / rhs);
				
				
				case bsh.ParserConstants_Fields.MOD: 
					return Double.valueOf(lhs % rhs);
					
					// can't shift floating-point values
				
				case bsh.ParserConstants_Fields.LSHIFT: 
				case bsh.ParserConstants_Fields.LSHIFTX: 
				case bsh.ParserConstants_Fields.RSIGNEDSHIFT: 
				case bsh.ParserConstants_Fields.RSIGNEDSHIFTX: 
				case bsh.ParserConstants_Fields.RUNSIGNEDSHIFT: 
				case bsh.ParserConstants_Fields.RUNSIGNEDSHIFTX: 
					throw new UtilEvalError("Can't shift doubles");
				
				
				default: 
					throw new InterpreterError("Unimplemented binary double operator");
				
			}
		}
		// returns Object covering both Long and Boolean return types
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		internal static System.Object floatBinaryOperation(ref System.Single F1, ref System.Single F2, int kind)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Float.floatValue' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			float lhs = (float) F1;
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Float.floatValue' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			float rhs = (float) F2;
			
			switch (kind)
			{
				
				// boolean
				case bsh.ParserConstants_Fields.LT: 
				case bsh.ParserConstants_Fields.LTX: 
					return Boolean.valueOf(lhs < rhs);
				
				
				case bsh.ParserConstants_Fields.GT: 
				case bsh.ParserConstants_Fields.GTX: 
					return Boolean.valueOf(lhs > rhs);
				
				
				case bsh.ParserConstants_Fields.EQ: 
					return Boolean.valueOf(lhs == rhs);
				
				
				case bsh.ParserConstants_Fields.LE: 
				case bsh.ParserConstants_Fields.LEX: 
					return Boolean.valueOf(lhs <= rhs);
				
				
				case bsh.ParserConstants_Fields.GE: 
				case bsh.ParserConstants_Fields.GEX: 
					return Boolean.valueOf(lhs >= rhs);
				
				
				case bsh.ParserConstants_Fields.NE: 
					return Boolean.valueOf(lhs != rhs);
					
					// arithmetic
				
				case bsh.ParserConstants_Fields.PLUS: 
					return Float.valueOf(lhs + rhs);
				
				
				case bsh.ParserConstants_Fields.MINUS: 
					return Float.valueOf(lhs - rhs);
				
				
				case bsh.ParserConstants_Fields.STAR: 
					return Float.valueOf(lhs * rhs);
				
				
				case bsh.ParserConstants_Fields.SLASH: 
					return Float.valueOf(lhs / rhs);
				
				
				case bsh.ParserConstants_Fields.MOD: 
					return Float.valueOf(lhs % rhs);
					
					// can't shift floats
				
				case bsh.ParserConstants_Fields.LSHIFT: 
				case bsh.ParserConstants_Fields.LSHIFTX: 
				case bsh.ParserConstants_Fields.RSIGNEDSHIFT: 
				case bsh.ParserConstants_Fields.RSIGNEDSHIFTX: 
				case bsh.ParserConstants_Fields.RUNSIGNEDSHIFT: 
				case bsh.ParserConstants_Fields.RUNSIGNEDSHIFTX: 
					throw new UtilEvalError("Can't shift floats ");
				
				
				default: 
					throw new InterpreterError("Unimplemented binary float operator");
				
			}
		}
		
		/// <summary>Promote primitive wrapper type to to Integer wrapper type</summary>
		internal static System.Object promoteToInteger(System.Object wrapper)
		{
			if (wrapper is System.Char)
				return Integer.valueOf(((System.Char) wrapper));
			else if ((wrapper is System.SByte) || (wrapper is System.Int16))
				return Integer.valueOf(System.Convert.ToInt32(((System.ValueType) wrapper)));
			
			return wrapper;
		}
		
		/// <summary>Promote the pair of primitives to the maximum type of the two.
		/// e.g. [int,long]->[long,long]
		/// </summary>
		internal static System.Object[] promotePrimitives(System.Object lhs, System.Object rhs)
		{
			lhs = promoteToInteger(lhs);
			rhs = promoteToInteger(rhs);
			
			if ((lhs is System.ValueType) && (rhs is System.ValueType))
			{
				System.ValueType lnum = (System.ValueType) lhs;
				System.ValueType rnum = (System.ValueType) rhs;
				
				bool b;
				
				if ((b = (lnum is System.Double)) || (rnum is System.Double))
				{
					if (b)
						rhs = Double.valueOf(System.Convert.ToDouble(rnum));
					else
						lhs = Double.valueOf(System.Convert.ToDouble(lnum));
				}
				else if ((b = (lnum is System.Single)) || (rnum is System.Single))
				{
					if (b)
						rhs = Float.valueOf(System.Convert.ToSingle(rnum));
					else
						lhs = Float.valueOf(System.Convert.ToSingle(lnum));
				}
				else if ((b = (lnum is System.Int64)) || (rnum is System.Int64))
				{
					if (b)
						rhs = Long.valueOf(System.Convert.ToInt64(rnum));
					else
						lhs = Long.valueOf(System.Convert.ToInt64(lnum));
				}
			}
			
			return new System.Object[]{lhs, rhs};
		}
		
		public static Primitive unaryOperation(Primitive val, int kind)
		{
			if (val == NULL)
				throw new UtilEvalError("illegal use of null object or 'null' literal");
			if (val == VOID)
				throw new UtilEvalError("illegal use of undefined object or 'void' literal");
			
			System.Type operandType = val.Type;
			System.Object operand = promoteToInteger(val.Value);
			
			if (operand is System.Boolean)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return new Primitive(booleanUnaryOperation(ref new System.Boolean[]{(System.Boolean) operand}[0], kind));
			}
			else if (operand is System.Int32)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				int result = intUnaryOperation(ref new System.Int32[]{(System.Int32) operand}[0], kind);
				
				// ++ and -- must be cast back the original type
				if (kind == bsh.ParserConstants_Fields.INCR || kind == bsh.ParserConstants_Fields.DECR)
				{
					if (operandType == System.Type.GetType("System.SByte"))
						return new Primitive((sbyte) result);
					if (operandType == System.Type.GetType("System.Int16"))
						return new Primitive((short) result);
					if (operandType == System.Type.GetType("System.Char"))
						return new Primitive((char) result);
				}
				
				return new Primitive(result);
			}
			else if (operand is System.Int64)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return new Primitive(longUnaryOperation(ref new System.Int64[]{(System.Int64) operand}[0], kind));
			}
			else if (operand is System.Single)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return new Primitive(floatUnaryOperation(ref new System.Single[]{(System.Single) operand}[0], kind));
			}
			else if (operand is System.Double)
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return new Primitive(doubleUnaryOperation(ref new System.Double[]{(System.Double) operand}[0], kind));
			}
			else
				throw new InterpreterError("An error occurred.  Please call technical support.");
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		internal static bool booleanUnaryOperation(ref System.Boolean B, int kind)
		{
			bool operand = B;
			switch (kind)
			{
				
				case bsh.ParserConstants_Fields.BANG: 
					return !operand;
				
				default: 
					throw new UtilEvalError("Operator inappropriate for boolean");
				
			}
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		internal static int intUnaryOperation(ref System.Int32 I, int kind)
		{
			int operand = I;
			
			switch (kind)
			{
				
				case bsh.ParserConstants_Fields.PLUS: 
					return operand;
				
				case bsh.ParserConstants_Fields.MINUS: 
					return - operand;
				
				case bsh.ParserConstants_Fields.TILDE: 
					return ~ operand;
				
				case bsh.ParserConstants_Fields.INCR: 
					return operand + 1;
				
				case bsh.ParserConstants_Fields.DECR: 
					return operand - 1;
				
				default: 
					throw new InterpreterError("bad integer unaryOperation");
				
			}
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		internal static long longUnaryOperation(ref System.Int64 L, int kind)
		{
			long operand = (long) L;
			
			switch (kind)
			{
				
				case bsh.ParserConstants_Fields.PLUS: 
					return operand;
				
				case bsh.ParserConstants_Fields.MINUS: 
					return - operand;
				
				case bsh.ParserConstants_Fields.TILDE: 
					return ~ operand;
				
				case bsh.ParserConstants_Fields.INCR: 
					return operand + 1;
				
				case bsh.ParserConstants_Fields.DECR: 
					return operand - 1;
				
				default: 
					throw new InterpreterError("bad long unaryOperation");
				
			}
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		internal static float floatUnaryOperation(ref System.Single F, int kind)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Float.floatValue' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			float operand = (float) F;
			
			switch (kind)
			{
				
				case bsh.ParserConstants_Fields.PLUS: 
					return operand;
				
				case bsh.ParserConstants_Fields.MINUS: 
					return - operand;
				
				default: 
					throw new InterpreterError("bad float unaryOperation");
				
			}
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		internal static double doubleUnaryOperation(ref System.Double D, int kind)
		{
			double operand = D;
			
			switch (kind)
			{
				
				case bsh.ParserConstants_Fields.PLUS: 
					return operand;
				
				case bsh.ParserConstants_Fields.MINUS: 
					return - operand;
				
				default: 
					throw new InterpreterError("bad double unaryOperation");
				
			}
		}
		
		public int intValue()
		{
			if (value_Renamed is System.ValueType)
				return System.Convert.ToInt32(((System.ValueType) value_Renamed));
			else
				throw new UtilEvalError("Primitive not a number");
		}
		
		public bool booleanValue()
		{
			if (value_Renamed is System.Boolean)
				return ((System.Boolean) value_Renamed);
			else
				throw new UtilEvalError("Primitive not a boolean");
		}
		
		public System.ValueType numberValue()
		{
			System.Object value_Renamed = this.value_Renamed;
			
			// Promote character to Number type for these purposes
			if (value_Renamed is System.Char)
				value_Renamed = Integer.valueOf(((System.Char) value_Renamed));
			
			if (value_Renamed is System.ValueType)
				return (System.ValueType) value_Renamed;
			else
				throw new UtilEvalError("Primitive not a number");
		}
		
		/// <summary>Primitives compare equal with other Primitives containing an equal
		/// wrapped value.
		/// </summary>
		public  override bool Equals(System.Object obj)
		{
			if (obj is Primitive)
				return ((Primitive) obj).value_Renamed.Equals(this.value_Renamed);
			else
				return false;
		}
		
		/// <summary>The hash of the Primitive is tied to the hash of the wrapped value but
		/// shifted so that they are not the same.
		/// </summary>
		public override int GetHashCode()
		{
			return this.value_Renamed.GetHashCode() * 21; // arbitrary
		}
		
		/// <summary>Unwrap primitive values and map voids to nulls.
		/// Non Primitive types remain unchanged.
		/// </summary>
		/// <param name="obj">object type which may be bsh.Primitive
		/// </param>
		/// <returns> corresponding "normal" Java type, "unwrapping" 
		/// any bsh.Primitive types to their wrapper types.
		/// </returns>
		public static System.Object unwrap(System.Object obj)
		{
			// map voids to nulls for the outside world
			if (obj == Primitive.VOID)
				return null;
			
			// unwrap primitives
			if (obj is Primitive)
				return ((Primitive) obj).Value;
			else
				return obj;
		}
		
		/*
		Unwrap Primitive wrappers to their java.lang wrapper values.
		e.g. Primitive(42) becomes Integer(42)
		@see #unwrap( Object )
		*/
		public static System.Object[] unwrap(System.Object[] args)
		{
			System.Object[] oa = new System.Object[args.Length];
			for (int i = 0; i < args.Length; i++)
				oa[i] = unwrap(args[i]);
			return oa;
		}
		
		/*
		*/
		public static System.Object[] wrap(System.Object[] args, System.Type[] paramTypes)
		{
			if (args == null)
				return null;
			
			System.Object[] oa = new System.Object[args.Length];
			for (int i = 0; i < args.Length; i++)
				oa[i] = wrap(args[i], paramTypes[i]);
			return oa;
		}
		
		/// <summary>Wrap primitive values (as indicated by type param) and nulls in the 
		/// Primitive class.  Values not primitive or null are left unchanged.
		/// Primitive values are represented by their wrapped values in param value.
		/// <p/>
		/// The value null is mapped to Primitive.NULL.
		/// Any value specified with type Void.TYPE is mapped to Primitive.VOID.
		/// </summary>
		public static System.Object wrap(System.Object value_Renamed, System.Type type)
		{
			if (type == System.Type.GetType("System.Void"))
				return Primitive.VOID;
			
			if (value_Renamed == null)
				return Primitive.NULL;
			
			if (type.IsPrimitive)
				return new Primitive(value_Renamed);
			
			return value_Renamed;
		}
		
		
		/// <summary>Get the appropriate default value per JLS 4.5.4</summary>
		public static Primitive getDefaultValue(System.Type type)
		{
			if (type == null || !type.IsPrimitive)
				return Primitive.NULL;
			if (type == System.Type.GetType("System.Boolean"))
				return new Primitive(false);
			
			// non boolean primitive, get appropriate flavor of zero
			try
			{
				return new Primitive((int) 0).castToType(type, Types.CAST);
			}
			catch (UtilEvalError e)
			{
				throw new InterpreterError("bad cast");
			}
		}
		
		/// <summary>Get the corresponding java.lang wrapper class for the primitive TYPE
		/// class.
		/// e.g.  Integer.TYPE -> Integer.class
		/// </summary>
		public static System.Type boxType(System.Type primitiveType)
		{
			System.Type c = wrapperMap.get_Renamed(primitiveType);
			if (c != null)
				return c;
			throw new InterpreterError("Not a primitive type: " + primitiveType);
		}
		
		/// <summary>Get the corresponding primitive TYPE class for the java.lang wrapper
		/// class type.
		/// e.g.  Integer.class -> Integer.TYPE
		/// </summary>
		public static System.Type unboxType(System.Type wrapperType)
		{
			System.Type c = wrapperMap.get_Renamed(wrapperType);
			if (c != null)
				return c;
			throw new InterpreterError("Not a primitive wrapper type: " + wrapperType);
		}
		
		/// <summary>Cast this bsh.Primitive value to a new bsh.Primitive value
		/// This is usually a numeric type cast.  Other cases include:
		/// A boolean can be cast to boolen
		/// null can be cast to any object type and remains null
		/// Attempting to cast a void causes an exception
		/// </summary>
		/// <param name="toType">is the java object or primitive TYPE class
		/// </param>
		public Primitive castToType(System.Type toType, int operation)
		{
			return castPrimitive(toType, Type, this, false, operation);
		}
		
		/*
		Cast or check a cast of a primitive type to another type.
		Normally both types are primitive (e.g. numeric), but a null value
		(no type) may be cast to any type.
		<p/>
		
		@param toType is the target type of the cast.  It is normally a
		java primitive TYPE, but in the case of a null cast can be any object
		type.
		
		@param fromType is the java primitive TYPE type of the primitive to be
		cast or null, to indicate that the fromValue was null or void.
		
		@param fromValue is, optionally, the value to be converted.  If
		checkOnly is true fromValue must be null.  If checkOnly is false,
		fromValue must be non-null (Primitive.NULL is of course valid).
		*/
		internal static Primitive castPrimitive(System.Type toType, System.Type fromType, Primitive fromValue, bool checkOnly, int operation)
		{
			/*
			Lots of preconditions checked here...
			Once things are running smoothly we might comment these out
			(That's what assertions are for).
			*/
			if (checkOnly && fromValue != null)
				throw new InterpreterError("bad cast param 1");
			if (!checkOnly && fromValue == null)
				throw new InterpreterError("bad cast param 2");
			if (fromType != null && !fromType.IsPrimitive)
				throw new InterpreterError("bad fromType:" + fromType);
			if (fromValue == Primitive.NULL && fromType != null)
				throw new InterpreterError("inconsistent args 1");
			if (fromValue == Primitive.VOID && fromType != System.Type.GetType("System.Void"))
				throw new InterpreterError("inconsistent args 2");
			
			// can't cast void to anything
			if (fromType == System.Type.GetType("System.Void"))
				if (checkOnly)
					return Types.INVALID_CAST;
				else
					throw Types.castError(Reflect.normalizeClassName(toType), "void value", operation);
			
			// unwrap Primitive fromValue to its wrapper value, etc.
			System.Object value_Renamed = null;
			if (fromValue != null)
				value_Renamed = fromValue.Value;
			
			if (toType.IsPrimitive)
			{
				// Trying to cast null to primitive type?
				if (fromType == null)
					if (checkOnly)
						return Types.INVALID_CAST;
					else
						throw Types.castError("primitive type:" + toType, "Null value", operation);
				
				// fall through
			}
			else
			{
				// Trying to cast primitive to an object type
				// Primitive.NULL can be cast to any object type
				if (fromType == null)
					return checkOnly?Types.VALID_CAST:Primitive.NULL;
				
				if (checkOnly)
					return Types.INVALID_CAST;
				else
					throw Types.castError("object type:" + toType, "primitive value", operation);
			}
			
			// can only cast boolean to boolean
			if (fromType == System.Type.GetType("System.Boolean"))
			{
				if (toType != System.Type.GetType("System.Boolean"))
					if (checkOnly)
						return Types.INVALID_CAST;
					else
						throw Types.castError(toType, fromType, operation);
				
				return checkOnly?Types.VALID_CAST:fromValue;
			}
			
			// Do numeric cast
			
			// Only allow legal Java assignment unless we're a CAST operation
			if (operation == Types.ASSIGNMENT && !Types.isJavaAssignable(toType, fromType))
			{
				if (checkOnly)
					return Types.INVALID_CAST;
				else
					throw Types.castError(toType, fromType, operation);
			}
			
			return checkOnly?Types.VALID_CAST:new Primitive(castWrapper(toType, value_Renamed));
		}
		
		public static bool isWrapperType(System.Type type)
		{
			return wrapperMap.get_Renamed(type) != null && !type.IsPrimitive;
		}
		
		/// <summary>Cast a primitive value represented by its java.lang wrapper type to the
		/// specified java.lang wrapper type.  e.g.  Byte(5) to Integer(5) or
		/// Integer(5) to Byte(5) 
		/// </summary>
		/// <param name="toType">is the java TYPE type
		/// </param>
		/// <param name="value">is the value in java.lang wrapper.
		/// value may not be null.
		/// </param>
		internal static System.Object castWrapper(System.Type toType, System.Object value_Renamed)
		{
			if (!toType.IsPrimitive)
				throw new InterpreterError("invalid type in castWrapper: " + toType);
			if (value_Renamed == null)
				throw new InterpreterError("null value in castWrapper, guard");
			if (value_Renamed is System.Boolean)
			{
				if (toType != System.Type.GetType("System.Boolean"))
					throw new InterpreterError("bad wrapper cast of boolean");
				else
					return value_Renamed;
			}
			
			// first promote char to Number type to avoid duplicating code
			if (value_Renamed is System.Char)
				value_Renamed = Integer.valueOf(((System.Char) value_Renamed));
			
			if (!(value_Renamed is System.ValueType))
				throw new InterpreterError("bad type in cast");
			
			System.ValueType number = (System.ValueType) value_Renamed;
			
			if (toType == System.Type.GetType("System.SByte"))
				return Byte.valueOf((sbyte) System.Convert.ToSByte(number));
			if (toType == System.Type.GetType("System.Int16"))
				return Short.valueOf(System.Convert.ToInt16(number));
			if (toType == System.Type.GetType("System.Char"))
				return Character.valueOf((char) System.Convert.ToInt32(number));
			if (toType == System.Type.GetType("System.Int32"))
				return Integer.valueOf(System.Convert.ToInt32(number));
			if (toType == System.Type.GetType("System.Int64"))
				return Long.valueOf(System.Convert.ToInt64(number));
			if (toType == System.Type.GetType("System.Single"))
				return Float.valueOf(System.Convert.ToSingle(number));
			if (toType == System.Type.GetType("System.Double"))
				return Double.valueOf(System.Convert.ToDouble(number));
			
			throw new InterpreterError("error in wrapper cast");
		}
		static Primitive()
		{
			{
				wrapperMap.put(System.Type.GetType("System.Boolean"), typeof(System.Boolean));
				wrapperMap.put(System.Type.GetType("System.SByte"), typeof(System.SByte));
				wrapperMap.put(System.Type.GetType("System.Int16"), typeof(System.Int16));
				wrapperMap.put(System.Type.GetType("System.Char"), typeof(System.Char));
				wrapperMap.put(System.Type.GetType("System.Int32"), typeof(System.Int32));
				wrapperMap.put(System.Type.GetType("System.Int64"), typeof(System.Int64));
				wrapperMap.put(System.Type.GetType("System.Single"), typeof(System.Single));
				wrapperMap.put(System.Type.GetType("System.Double"), typeof(System.Double));
				wrapperMap.put(typeof(System.Boolean), System.Type.GetType("System.Boolean"));
				wrapperMap.put(typeof(System.SByte), System.Type.GetType("System.SByte"));
				wrapperMap.put(typeof(System.Int16), System.Type.GetType("System.Int16"));
				wrapperMap.put(typeof(System.Char), System.Type.GetType("System.Char"));
				wrapperMap.put(typeof(System.Int32), System.Type.GetType("System.Int32"));
				wrapperMap.put(typeof(System.Int64), System.Type.GetType("System.Int64"));
				wrapperMap.put(typeof(System.Single), System.Type.GetType("System.Single"));
				wrapperMap.put(typeof(System.Double), System.Type.GetType("System.Double"));
			}
		}
	}
}