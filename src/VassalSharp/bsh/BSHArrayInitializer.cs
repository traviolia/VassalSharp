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
	class BSHArrayInitializer:SimpleNode
	{
		internal BSHArrayInitializer(int id):base(id)
		{
		}
		
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			throw new EvalError("Array initializer has no base type.", this, callstack);
		}
		
		/// <summary>Construct the array from the initializer syntax.</summary>
		/// <param name="baseType">the base class type of the array (no dimensionality)
		/// </param>
		/// <param name="dimensions">the top number of dimensions of the array 
		/// e.g. 2 for a String [][];
		/// </param>
		public virtual System.Object eval(System.Type baseType, int dimensions, CallStack callstack, Interpreter interpreter)
		{
			int numInitializers = jjtGetNumChildren();
			
			// allocate the array to store the initializers
			int[] dima = new int[dimensions]; // description of the array
			// The other dimensions default to zero and are assigned when 
			// the values are set.
			dima[0] = numInitializers;
			System.Object initializers = System.Array.CreateInstance(baseType, (System.Int32[]) dima);
			
			// Evaluate the initializers
			for (int i = 0; i < numInitializers; i++)
			{
				SimpleNode node = (SimpleNode) jjtGetChild(i);
				System.Object currentInitializer;
				if (node is BSHArrayInitializer)
				{
					if (dimensions < 2)
						throw new EvalError("Invalid Location for Intializer, position: " + i, this, callstack);
					currentInitializer = ((BSHArrayInitializer) node).eval(baseType, dimensions - 1, callstack, interpreter);
				}
				else
					currentInitializer = node.eval(callstack, interpreter);
				
				if (currentInitializer == Primitive.VOID)
					throw new EvalError("Void in array initializer, position" + i, this, callstack);
				
				// Determine if any conversion is necessary on the initializers.
				//
				// Quick test to see if conversions apply:
				// If the dimensionality of the array is 1 then the elements of
				// the initializer can be primitives or boxable types.  If it is
				// greater then the values must be array (object) types and there
				// are currently no conversions that we do on those.
				// If we have conversions on those in the future then we need to
				// get the real base type here instead of the dimensionless one.
				System.Object value_Renamed = currentInitializer;
				if (dimensions == 1)
				{
					// We do a bsh cast here.  strictJava should be able to affect
					// the cast there when we tighten control
					try
					{
						value_Renamed = Types.castObject(currentInitializer, baseType, Types.CAST);
					}
					catch (UtilEvalError e)
					{
						throw e.toEvalError("Error in array initializer", this, callstack);
					}
					// unwrap any primitive, map voids to null, etc.
					value_Renamed = Primitive.unwrap(value_Renamed);
				}
				
				// store the value in the array
				try
				{
					((System.Array) initializers).SetValue(value_Renamed, i);
				}
				catch (System.ArgumentException e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					Interpreter.debug("illegal arg" + e);
					throwTypeError(baseType, currentInitializer, i, callstack);
				}
				catch (System.ArrayTypeMismatchException e)
				{
					// I think this can happen
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					Interpreter.debug("arraystore" + e);
					throwTypeError(baseType, currentInitializer, i, callstack);
				}
			}
			
			return initializers;
		}
		
		private void  throwTypeError(System.Type baseType, System.Object initializer, int argNum, CallStack callstack)
		{
			System.String rhsType;
			if (initializer is Primitive)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				rhsType = ((Primitive) initializer).Type.FullName;
			}
			else
				rhsType = Reflect.normalizeClassName(initializer.GetType());
			
			throw new EvalError("Incompatible type: " + rhsType + " in initializer of array type: " + baseType + " at position: " + argNum, this, callstack);
		}
	}
}