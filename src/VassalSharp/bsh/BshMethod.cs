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
	
	/// <summary>This represents an instance of a bsh method declaration in a particular
	/// namespace.  This is a thin wrapper around the BSHMethodDeclaration
	/// with a pointer to the declaring namespace.
	/// <p>
	/// When a method is located in a subordinate namespace or invoked from an 
	/// arbitrary namespace it must nontheless execute with its 'super' as the 
	/// context in which it was declared.
	/// <p/>
	/// </summary>
	/*
	Note: this method incorrectly caches the method structure.  It needs to
	be cleared when the classloader changes.*/
	[Serializable]
	public class BshMethod
	{
		/// <summary>Get the argument types of this method.
		/// loosely typed (untyped) arguments will be represented by null argument
		/// types.
		/// </summary>
		virtual public System.Type[] ParameterTypes
		{
			/*
			Note: bshmethod needs to re-evaluate arg types here
			This is broken.
			*/
			
			get
			{
				return cparamTypes;
			}
			
		}
		virtual public System.String[] ParameterNames
		{
			get
			{
				return paramNames;
			}
			
		}
		/// <summary>Get the return type of the method.</summary>
		/// <returns> Returns null for a loosely typed return value, 
		/// Void.TYPE for a void return type, or the Class of the type.
		/// </returns>
		virtual public System.Type ReturnType
		{
			/*
			Note: bshmethod needs to re-evaluate the method return type here.
			This is broken.
			*/
			
			get
			{
				return creturnType;
			}
			
		}
		virtual public Modifiers Modifiers
		{
			get
			{
				return modifiers;
			}
			
		}
		virtual public System.String Name
		{
			get
			{
				return name;
			}
			
		}
		/* 
		This is the namespace in which the method is set.
		It is a back-reference for the node, which needs to execute under this 
		namespace.  It is not necessary to declare this transient, because 
		we can only be saved as part of our namespace anyway... (currently).
		*/
		internal NameSpace declaringNameSpace;
		
		// Begin Method components
		
		internal Modifiers modifiers;
		private System.String name;
		private System.Type creturnType;
		
		// Arguments
		private System.String[] paramNames;
		private int numArgs;
		private System.Type[] cparamTypes;
		
		// Scripted method body
		internal BSHBlock methodBody;
		
		// Java Method, for a BshObject that delegates to a real Java method
		private System.Reflection.MethodInfo javaMethod;
		private System.Object javaObject;
		
		// End method components
		
		internal BshMethod(BSHMethodDeclaration method, NameSpace declaringNameSpace, Modifiers modifiers):this(method.name, method.returnType, method.paramsNode.ParamNames, method.paramsNode.paramTypes, method.blockNode, declaringNameSpace, modifiers)
		{
		}
		
		internal BshMethod(System.String name, System.Type returnType, System.String[] paramNames, System.Type[] paramTypes, BSHBlock methodBody, NameSpace declaringNameSpace, Modifiers modifiers)
		{
			this.name = name;
			this.creturnType = returnType;
			this.paramNames = paramNames;
			if (paramNames != null)
				this.numArgs = paramNames.Length;
			this.cparamTypes = paramTypes;
			this.methodBody = methodBody;
			this.declaringNameSpace = declaringNameSpace;
			this.modifiers = modifiers;
		}
		
		/*
		Create a BshMethod that delegates to a real Java method upon invocation.
		This is used to represent imported object methods.
		*/
		//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
		internal BshMethod(System.Reflection.MethodInfo method, System.Object object_Renamed):this(method.Name, method.ReturnType, null, method.GetParameters(), null, null, null)
		{
			
			this.javaMethod = method;
			this.javaObject = object_Renamed;
		}
		
		/// <summary>Invoke the declared method with the specified arguments and interpreter
		/// reference.  This is the simplest form of invoke() for BshMethod 
		/// intended to be used in reflective style access to bsh scripts.
		/// </summary>
		public virtual System.Object invoke(System.Object[] argValues, Interpreter interpreter)
		{
			return invoke(argValues, interpreter, null, null, false);
		}
		
		/// <summary>Invoke the bsh method with the specified args, interpreter ref,
		/// and callstack.
		/// callerInfo is the node representing the method invocation
		/// It is used primarily for debugging in order to provide access to the 
		/// text of the construct that invoked the method through the namespace.
		/// </summary>
		/// <param name="callerInfo">is the BeanShell AST node representing the method 
		/// invocation.  It is used to print the line number and text of 
		/// errors in EvalError exceptions.  If the node is null here error
		/// messages may not be able to point to the precise location and text
		/// of the error.
		/// </param>
		/// <param name="callstack">is the callstack.  If callstack is null a new one
		/// will be created with the declaring namespace of the method on top
		/// of the stack (i.e. it will look for purposes of the method 
		/// invocation like the method call occurred in the declaring 
		/// (enclosing) namespace in which the method is defined).
		/// </param>
		public virtual System.Object invoke(System.Object[] argValues, Interpreter interpreter, CallStack callstack, SimpleNode callerInfo)
		{
			return invoke(argValues, interpreter, callstack, callerInfo, false);
		}
		
		/// <summary>Invoke the bsh method with the specified args, interpreter ref,
		/// and callstack.
		/// callerInfo is the node representing the method invocation
		/// It is used primarily for debugging in order to provide access to the 
		/// text of the construct that invoked the method through the namespace.
		/// </summary>
		/// <param name="callerInfo">is the BeanShell AST node representing the method 
		/// invocation.  It is used to print the line number and text of 
		/// errors in EvalError exceptions.  If the node is null here error
		/// messages may not be able to point to the precise location and text
		/// of the error.
		/// </param>
		/// <param name="callstack">is the callstack.  If callstack is null a new one
		/// will be created with the declaring namespace of the method on top
		/// of the stack (i.e. it will look for purposes of the method 
		/// invocation like the method call occurred in the declaring 
		/// (enclosing) namespace in which the method is defined).
		/// </param>
		/// <param name="overrideNameSpace">
		/// When true the method is executed in the namespace on the top of the
		/// stack instead of creating its own local namespace.  This allows it
		/// to be used in constructors.
		/// </param>
		internal virtual System.Object invoke(System.Object[] argValues, Interpreter interpreter, CallStack callstack, SimpleNode callerInfo, bool overrideNameSpace)
		{
			if (argValues != null)
				for (int i = 0; i < argValues.Length; i++)
					if (argValues[i] == null)
						throw new System.ApplicationException("HERE!");
			
			if (javaMethod != null)
				try
				{
					return Reflect.invokeMethod(javaMethod, javaObject, argValues);
				}
				catch (ReflectError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new EvalError("Error invoking Java method: " + e, callerInfo, callstack);
				}
				catch (System.Reflection.TargetInvocationException e2)
				{
					throw new TargetError("Exception invoking imported object method.", e2, callerInfo, callstack, true);
				}
			
			// is this a syncrhonized method?
			if (modifiers != null && modifiers.hasModifier("synchronized"))
			{
				// The lock is our declaring namespace's This reference
				// (the method's 'super').  Or in the case of a class it's the
				// class instance.
				System.Object lock_Renamed;
				if (declaringNameSpace.isClass)
				{
					try
					{
						lock_Renamed = declaringNameSpace.ClassInstance;
					}
					catch (UtilEvalError e)
					{
						throw new InterpreterError("Can't get class instance for synchronized method.");
					}
				}
				else
					lock_Renamed = declaringNameSpace.getThis(interpreter); // ???
				
				lock (lock_Renamed)
				{
					return invokeImpl(argValues, interpreter, callstack, callerInfo, overrideNameSpace);
				}
			}
			else
				return invokeImpl(argValues, interpreter, callstack, callerInfo, overrideNameSpace);
		}
		
		private System.Object invokeImpl(System.Object[] argValues, Interpreter interpreter, CallStack callstack, SimpleNode callerInfo, bool overrideNameSpace)
		{
			System.Type returnType = ReturnType;
			System.Type[] paramTypes = ParameterTypes;
			
			// If null callstack
			if (callstack == null)
				callstack = new CallStack(declaringNameSpace);
			
			if (argValues == null)
				argValues = new System.Object[]{};
			
			// Cardinality (number of args) mismatch
			if (argValues.Length != numArgs)
			{
				/*
				// look for help string
				try {
				// should check for null namespace here
				String help = 
				(String)declaringNameSpace.get(
				"bsh.help."+name, interpreter );
				
				interpreter.println(help);
				return Primitive.VOID;
				} catch ( Exception e ) {
				throw eval error
				}
				*/
				throw new EvalError("Wrong number of arguments for local method: " + name, callerInfo, callstack);
			}
			
			// Make the local namespace for the method invocation
			NameSpace localNameSpace;
			if (overrideNameSpace)
				localNameSpace = callstack.top();
			else
			{
				localNameSpace = new NameSpace(declaringNameSpace, name);
				localNameSpace.isMethod = true;
			}
			// should we do this for both cases above?
			localNameSpace.Node = callerInfo;
			
			// set the method parameters in the local namespace
			for (int i = 0; i < numArgs; i++)
			{
				// Set typed variable
				if (paramTypes[i] != null)
				{
					try
					{
						argValues[i] = Types.castObject(argValues[i], paramTypes[i], Types.ASSIGNMENT);
					}
					catch (UtilEvalError e)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						throw new EvalError("Invalid argument: " + "`" + paramNames[i] + "'" + " for method: " + name + " : " + e.Message, callerInfo, callstack);
					}
					try
					{
						localNameSpace.setTypedVariable(paramNames[i], paramTypes[i], argValues[i], null);
					}
					catch (UtilEvalError e2)
					{
						throw e2.toEvalError("Typed method parameter assignment", callerInfo, callstack);
					}
				}
				// Set untyped variable
				// untyped param
				else
				{
					// getAssignable would catch this for typed param
					if (argValues[i] == Primitive.VOID)
						throw new EvalError("Undefined variable or class name, parameter: " + paramNames[i] + " to method: " + name, callerInfo, callstack);
					else
						try
						{
							localNameSpace.setLocalVariable(paramNames[i], argValues[i], interpreter.StrictJava);
						}
						catch (UtilEvalError e3)
						{
							throw e3.toEvalError(callerInfo, callstack);
						}
				}
			}
			
			// Push the new namespace on the call stack
			if (!overrideNameSpace)
				callstack.push(localNameSpace);
			
			// Invoke the block, overriding namespace with localNameSpace
			System.Object ret = methodBody.eval(callstack, interpreter, true);
			
			// save the callstack including the called method, just for error mess
			CallStack returnStack = callstack.copy();
			
			// Get back to caller namespace
			if (!overrideNameSpace)
				callstack.pop();
			
			ReturnControl retControl = null;
			if (ret is ReturnControl)
			{
				retControl = (ReturnControl) ret;
				
				// Method body can only use 'return' statment type return control.
				if (retControl.kind == bsh.ParserConstants_Fields.RETURN)
					ret = ((ReturnControl) ret).value_Renamed;
				// retControl.returnPoint is the Node of the return statement
				else
					throw new EvalError("'continue' or 'break' in method body", retControl.returnPoint, returnStack);
				
				// Check for explicit return of value from void method type.
				// retControl.returnPoint is the Node of the return statement
				if (returnType == System.Type.GetType("System.Void") && ret != Primitive.VOID)
					throw new EvalError("Cannot return value from void method", retControl.returnPoint, returnStack);
			}
			
			if (returnType != null)
			{
				// If return type void, return void as the value.
				if (returnType == System.Type.GetType("System.Void"))
					return Primitive.VOID;
				
				// return type is a class
				try
				{
					ret = Types.castObject(ret, returnType, Types.ASSIGNMENT);
				}
				catch (UtilEvalError e)
				{
					// Point to return statement point if we had one.
					// (else it was implicit return? What's the case here?)
					SimpleNode node = callerInfo;
					if (retControl != null)
						node = retControl.returnPoint;
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw e.toEvalError("Incorrect type returned from method: " + name + e.Message, node, callstack);
				}
			}
			
			return ret;
		}
		
		public virtual bool hasModifier(System.String name)
		{
			return modifiers != null && modifiers.hasModifier(name);
		}
		
		public override System.String ToString()
		{
			return "Scripted Method: " + StringUtil.methodString(name, ParameterTypes);
		}
	}
}