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
	class BSHPrimarySuffix:SimpleNode
	{
		public const int CLASS = 0;
		public const int INDEX = 1;
		public const int NAME = 2;
		public const int PROPERTY = 3;
		
		public int operation;
		internal System.Object index;
		public System.String field;
		
		internal BSHPrimarySuffix(int id):base(id)
		{
		}
		
		/*
		Perform a suffix operation on the given object and return the 
		new value.
		<p>
		
		obj will be a Node when suffix evaluation begins, allowing us to
		interpret it contextually. (e.g. for .class) Thereafter it will be 
		an value object or LHS (as determined by toLHS).
		<p>
		
		We must handle the toLHS case at each point here.
		<p>
		*/
		public virtual System.Object doSuffix(System.Object obj, bool toLHS, CallStack callstack, Interpreter interpreter)
		{
			// Handle ".class" suffix operation
			// Prefix must be a BSHType
			if (operation == CLASS)
				if (obj is BSHType)
				{
					if (toLHS)
						throw new EvalError("Can't assign .class", this, callstack);
					NameSpace namespace_Renamed = callstack.top();
					return ((BSHType) obj).getType(callstack, interpreter);
				}
				else
					throw new EvalError("Attempt to use .class suffix on non class.", this, callstack);
			
			/*
			Evaluate our prefix if it needs evaluating first.
			If this is the first evaluation our prefix mayb be a Node 
			(directly from the PrimaryPrefix) - eval() it to an object.  
			If it's an LHS, resolve to a value.
			
			Note: The ambiguous name construct is now necessary where the node 
			may be an ambiguous name.  If this becomes common we might want to 
			make a static method nodeToObject() or something.  The point is 
			that we can't just eval() - we need to direct the evaluation to 
			the context sensitive type of result; namely object, class, etc.
			*/
			if (obj is SimpleNode)
				if (obj is BSHAmbiguousName)
					obj = ((BSHAmbiguousName) obj).toObject(callstack, interpreter);
				else
					obj = ((SimpleNode) obj).eval(callstack, interpreter);
			else if (obj is LHS)
				try
				{
					obj = ((LHS) obj).Value;
				}
				catch (UtilEvalError e)
				{
					throw e.toEvalError(this, callstack);
				}
			
			try
			{
				switch (operation)
				{
					
					case INDEX: 
						return doIndex(obj, toLHS, callstack, interpreter);
					
					
					case NAME: 
						return doName(obj, toLHS, callstack, interpreter);
					
					
					case PROPERTY: 
						return doProperty(toLHS, obj, callstack, interpreter);
					
					
					default: 
						throw new InterpreterError("Unknown suffix type");
					
				}
			}
			catch (ReflectError e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new EvalError("reflection error: " + e, this, callstack);
			}
			catch (System.Reflection.TargetInvocationException e)
			{
				throw new TargetError("target exception", e.GetBaseException(), this, callstack, true);
			}
		}
		
		/*
		Field access, .length on array, or a method invocation
		Must handle toLHS case for each.
		*/
		private System.Object doName(System.Object obj, bool toLHS, CallStack callstack, Interpreter interpreter)
		{
			try
			{
				// .length on array
				if (field.Equals("length") && obj.GetType().IsArray)
					if (toLHS)
						throw new EvalError("Can't assign array length", this, callstack);
					else
						return new Primitive(((System.Array) obj).Length);
				
				// field access
				if (jjtGetNumChildren() == 0)
					if (toLHS)
						return Reflect.getLHSObjectField(obj, field);
					else
						return Reflect.getObjectFieldValue(obj, field);
				
				// Method invocation
				// (LHS or non LHS evaluation can both encounter method calls)
				System.Object[] oa = ((BSHArguments) jjtGetChild(0)).getArguments(callstack, interpreter);
				
				// TODO:
				// Note: this try/catch block is copied from BSHMethodInvocation
				// we need to factor out this common functionality and make sure
				// we handle all cases ... (e.g. property style access, etc.)
				// maybe move this to Reflect ?
				try
				{
					return Reflect.invokeObjectMethod(obj, field, oa, interpreter, callstack, this);
				}
				catch (ReflectError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new EvalError("Error in method invocation: " + e.Message, this, callstack);
				}
				catch (System.Reflection.TargetInvocationException e)
				{
					System.String msg = "Method Invocation " + field;
					//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
					System.Exception te = e.GetBaseException();
					
					/*
					Try to squeltch the native code stack trace if the exception
					was caused by a reflective call back into the bsh interpreter
					(e.g. eval() or source()
					*/
					bool isNative = true;
					if (te is EvalError)
						if (te is TargetError)
							isNative = ((TargetError) te).inNativeCode();
						else
							isNative = false;
					
					throw new TargetError(msg, te, this, callstack, isNative);
				}
			}
			catch (UtilEvalError e)
			{
				throw e.toEvalError(this, callstack);
			}
		}
		
		
		internal static int getIndexAux(System.Object obj, CallStack callstack, Interpreter interpreter, SimpleNode callerInfo)
		{
			if (!obj.GetType().IsArray)
				throw new EvalError("Not an array", callerInfo, callstack);
			
			int index;
			try
			{
				System.Object indexVal = ((SimpleNode) callerInfo.jjtGetChild(0)).eval(callstack, interpreter);
				if (!(indexVal is Primitive))
					indexVal = Types.castObject(indexVal, System.Type.GetType("System.Int32"), Types.ASSIGNMENT);
				index = ((Primitive) indexVal).intValue();
			}
			catch (UtilEvalError e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				Interpreter.debug("doIndex: " + e);
				throw e.toEvalError("Arrays may only be indexed by integer types.", callerInfo, callstack);
			}
			
			return index;
		}
		
		/// <summary>array index.
		/// Must handle toLHS case.
		/// </summary>
		private System.Object doIndex(System.Object obj, bool toLHS, CallStack callstack, Interpreter interpreter)
		{
			int index = getIndexAux(obj, callstack, interpreter, this);
			if (toLHS)
				return new LHS(obj, index);
			else
				try
				{
					return Reflect.getIndex(obj, index);
				}
				catch (UtilEvalError e)
				{
					throw e.toEvalError(this, callstack);
				}
		}
		
		/// <summary>Property access.
		/// Must handle toLHS case.
		/// </summary>
		private System.Object doProperty(bool toLHS, System.Object obj, CallStack callstack, Interpreter interpreter)
		{
			if (obj == Primitive.VOID)
				throw new EvalError("Attempt to access property on undefined variable or class name", this, callstack);
			
			if (obj is Primitive)
				throw new EvalError("Attempt to access property on a primitive", this, callstack);
			
			System.Object value_Renamed = ((SimpleNode) jjtGetChild(0)).eval(callstack, interpreter);
			
			if (!(value_Renamed is System.String))
				throw new EvalError("Property expression must be a String or identifier.", this, callstack);
			
			if (toLHS)
				return new LHS(obj, (System.String) value_Renamed);
			
			// Property style access to Hashtable or Map
			CollectionManager cm = CollectionManager.getCollectionManager();
			if (cm.isMap(obj))
			{
				System.Object val = cm.getFromMap(obj, value_Renamed);
				return (val == null?(val = Primitive.NULL):val);
			}
			
			try
			{
				return Reflect.getObjectProperty(obj, (System.String) value_Renamed);
			}
			catch (UtilEvalError e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw e.toEvalError("Property: " + value_Renamed, this, callstack);
			}
			catch (ReflectError e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new EvalError("No such property: " + value_Renamed, this, callstack);
			}
		}
	}
}