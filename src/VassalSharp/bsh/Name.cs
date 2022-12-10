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
	
	/// <summary>What's in a name?  I'll tell you...
	/// Name() is a somewhat ambiguous thing in the grammar and so is this.
	/// <p>
	/// </summary>
	/// <summary>This class is a name resolver.  It holds a possibly ambiguous dot 
	/// separated name and reference to a namespace in which it allegedly lives.  
	/// It provides methods that attempt to resolve the name to various types of 
	/// entities: e.g. an Object, a Class, a declared scripted BeanShell method.
	/// <p>
	/// Name objects are created by the factory method NameSpace getNameResolver(), 
	/// which caches them subject to a class namespace change.  This means that 
	/// we can cache information about various types of resolution here.
	/// Currently very little if any information is cached.  However with a future
	/// "optimize" setting that defeats certain dynamic behavior we might be able
	/// to cache quite a bit.
	/// </summary>
	/*
	<strong>Implementation notes</strong>
	<pre>
	Thread safety: all of the work methods in this class must be synchronized
	because they share the internal intermediate evaluation state.
	
	Note about invokeMethod():  We could simply use resolveMethod and return
	the MethodInvoker (BshMethod or JavaMethod) however there is no easy way
	for the AST (BSHMehodInvocation) to use this as it doesn't have type
	information about the target to resolve overloaded methods.
	(In Java, overloaded methods are resolved at compile time... here they
	are, of necessity, dynamic).  So it would have to do what we do here
	and cache by signature.  We now do that for the client in Reflect.java.
	
	Note on this.caller resolution:
	Although references like these do work:
	
	this.caller.caller.caller...   // works
	
	the equivalent using successive calls:
	
	// does *not* work
	for( caller=this.caller; caller != null; caller = caller.caller );
	
	is prohibited by the restriction that you can only call .caller on a 
	literal	this or caller reference.  The effect is that magic caller 
	reference only works through the current 'this' reference.
	The real explanation is that This referernces do not really know anything
	about their depth on the call stack.  It might even be hard to define
	such a thing...
	
	For those purposes we provide :
	
	this.callstack
	
	</pre>*/
	[Serializable]
	class Name
	{
		// These do not change during evaluation
		public NameSpace namespace_Renamed;
		internal System.String value_Renamed = null;
		
		// ---------------------------------------------------------
		// The following instance variables mutate during evaluation and should
		// be reset by the reset() method where necessary
		
		// For evaluation
		/// <summary>Remaining text to evaluate </summary>
		private System.String evalName;
		/// <summary>The last part of the name evaluated.  This is really only used for
		/// this, caller, and super resolution.
		/// </summary>
		private System.String lastEvalName;
		private static System.String FINISHED = null; // null evalname and we're finished
		private System.Object evalBaseObject; // base object for current eval
		
		private int callstackDepth; // number of times eval hit 'this.caller'
		
		//  
		//  End mutable instance variables.
		// ---------------------------------------------------------
		
		// Begin Cached result structures
		// These are optimizations 
		
		// Note: it's ok to cache class resolution here because when the class
		// space changes the namespace will discard cached names.
		
		/// <summary>The result is a class </summary>
		internal System.Type asClass;
		
		/// <summary>The result is a static method call on the following class </summary>
		internal System.Type classOfStaticMethod;
		
		// End Cached result structures
		
		private void  reset()
		{
			evalName = value_Renamed;
			evalBaseObject = null;
			callstackDepth = 0;
		}
		
		/// <summary>This constructor should *not* be used in general. 
		/// Use NameSpace getNameResolver() which supports caching.
		/// </summary>
		/// <seealso cref="NameSpace getNameResolver().">
		/// </seealso>
		// I wish I could make this "friendly" to only NameSpace
		internal Name(NameSpace namespace_Renamed, System.String s)
		{
			this.namespace_Renamed = namespace_Renamed;
			value_Renamed = s;
		}
		
		/// <summary>Resolve possibly complex name to an object value.
		/// Throws EvalError on various failures.
		/// A null object value is indicated by a Primitive.NULL.
		/// A return type of Primitive.VOID comes from attempting to access
		/// an undefined variable.
		/// Some cases:
		/// myVariable
		/// myVariable.foo
		/// myVariable.foo.bar
		/// java.awt.GridBagConstraints.BOTH
		/// my.package.stuff.MyClass.someField.someField...
		/// Interpreter reference is necessary to allow resolution of 
		/// "this.interpreter" magic field.
		/// CallStack reference is necessary to allow resolution of 
		/// "this.caller" magic field.
		/// "this.callstack" magic field.
		/// </summary>
		public virtual System.Object toObject(CallStack callstack, Interpreter interpreter)
		{
			return toObject(callstack, interpreter, false);
		}
		
		/// <seealso cref="toObject()">
		/// </seealso>
		/// <param name="forceClass">if true then resolution will only produce a class.
		/// This is necessary to disambiguate in cases where the grammar knows
		/// that we want a class; where in general the var path may be taken.
		/// </param>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'toObject'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual System.Object toObject(CallStack callstack, Interpreter interpreter, bool forceClass)
		{
			lock (this)
			{
				reset();
				
				System.Object obj = null;
				while (evalName != null)
					obj = consumeNextObjectField(callstack, interpreter, forceClass, false);
				
				if (obj == null)
					throw new InterpreterError("null value in toObject()");
				
				return obj;
			}
		}
		
		private System.Object completeRound(System.String lastEvalName, System.String nextEvalName, System.Object returnObject)
		{
			if (returnObject == null)
				throw new InterpreterError("lastEvalName = " + lastEvalName);
			this.lastEvalName = lastEvalName;
			this.evalName = nextEvalName;
			this.evalBaseObject = returnObject;
			return returnObject;
		}
		
		/// <summary>Get the next object by consuming one or more components of evalName.  
		/// Often this consumes just one component, but if the name is a classname 
		/// it will consume all of the components necessary to make the class 
		/// identifier.
		/// </summary>
		private System.Object consumeNextObjectField(CallStack callstack, Interpreter interpreter, bool forceClass, bool autoAllocateThis)
		{
			/*
			Is it a simple variable name?
			Doing this first gives the correct Java precedence for vars 
			vs. imported class names (at least in the simple case - see
			tests/precedence1.bsh).  It should also speed things up a bit.
			*/
			if ((evalBaseObject == null && !isCompound(evalName)) && !forceClass)
			{
				System.Object obj = resolveThisFieldReference(callstack, namespace_Renamed, interpreter, evalName, false);
				
				if (obj != Primitive.VOID)
					return completeRound(evalName, FINISHED, obj);
			}
			
			/*
			Is it a bsh script variable reference?
			If we're just starting the eval of name (no base object)
			or we're evaluating relative to a This type reference check.
			*/
			System.String varName = prefix(evalName, 1);
			if ((evalBaseObject == null || evalBaseObject is This) && !forceClass)
			{
				if (Interpreter.DEBUG)
					Interpreter.debug("trying to resolve variable: " + varName);
				
				System.Object obj;
				// switch namespace and special var visibility
				if (evalBaseObject == null)
				{
					obj = resolveThisFieldReference(callstack, namespace_Renamed, interpreter, varName, false);
				}
				else
				{
					obj = resolveThisFieldReference(callstack, ((This) evalBaseObject).namespace_Renamed, interpreter, varName, true);
				}
				
				if (obj != Primitive.VOID)
				{
					// Resolved the variable
					if (Interpreter.DEBUG)
					{
						Interpreter.debug("resolved variable: " + varName + " in namespace: " + namespace_Renamed);
					}
					
					return completeRound(varName, suffix(evalName), obj);
				}
			}
			
			/*
			Is it a class name?
			If we're just starting eval of name try to make it, else fail.
			*/
			if (evalBaseObject == null)
			{
				if (Interpreter.DEBUG)
					Interpreter.debug("trying class: " + evalName);
				
				/*
				Keep adding parts until we have a class 
				*/
				System.Type clas = null;
				int i = 1;
				System.String className = null;
				for (; i <= countParts(evalName); i++)
				{
					className = prefix(evalName, i);
					if ((clas = namespace_Renamed.getClass(className)) != null)
						break;
				}
				
				if (clas != null)
				{
					return completeRound(className, suffix(evalName, countParts(evalName) - i), new ClassIdentifier(clas));
				}
				// not a class (or variable per above)
				if (Interpreter.DEBUG)
					Interpreter.debug("not a class, trying var prefix " + evalName);
			}
			
			// No variable or class found in 'this' type ref.
			// if autoAllocateThis then create one; a child 'this'.
			if ((evalBaseObject == null || evalBaseObject is This) && !forceClass && autoAllocateThis)
			{
				NameSpace targetNameSpace = (evalBaseObject == null)?namespace_Renamed:((This) evalBaseObject).namespace_Renamed;
				System.Object obj = new NameSpace(targetNameSpace, "auto: " + varName).getThis(interpreter);
				targetNameSpace.setVariable(varName, obj, false);
				return completeRound(varName, suffix(evalName), obj);
			}
			
			/*
			If we didn't find a class or variable name (or prefix) above
			there are two possibilities:
			
			- If we are a simple name then we can pass as a void variable 
			reference.
			- If we are compound then we must fail at this point.
			*/
			if (evalBaseObject == null)
			{
				if (!isCompound(evalName))
				{
					return completeRound(evalName, FINISHED, Primitive.VOID);
				}
				else
					throw new UtilEvalError("Class or variable not found: " + evalName);
			}
			
			/*
			--------------------------------------------------------
			After this point we're definitely evaluating relative to
			a base object.
			--------------------------------------------------------
			*/
			
			/*
			Do some basic validity checks.
			*/
			
			if (evalBaseObject == Primitive.NULL)
			// previous round produced null
				throw new UtilTargetError(new System.NullReferenceException("Null Pointer while evaluating: " + value_Renamed));
			
			if (evalBaseObject == Primitive.VOID)
			// previous round produced void
				throw new UtilEvalError("Undefined variable or class name while evaluating: " + value_Renamed);
			
			if (evalBaseObject is Primitive)
				throw new UtilEvalError("Can't treat primitive like an object. " + "Error while evaluating: " + value_Renamed);
			
			/* 
			Resolve relative to a class type
			static field, inner class, ?
			*/
			if (evalBaseObject is ClassIdentifier)
			{
				System.Type clas = ((ClassIdentifier) evalBaseObject).TargetClass;
				System.String field = prefix(evalName, 1);
				
				// Class qualified 'this' reference from inner class.
				// e.g. 'MyOuterClass.this'
				if (field.Equals("this"))
				{
					// find the enclosing class instance space of the class name
					NameSpace ns = namespace_Renamed;
					while (ns != null)
					{
						// getClassInstance() throws exception if not there
						if (ns.classInstance != null && ns.classInstance.GetType() == clas)
							return completeRound(field, suffix(evalName), ns.classInstance);
						ns = ns.Parent;
					}
					throw new UtilEvalError("Can't find enclosing 'this' instance of class: " + clas);
				}
				
				System.Object obj = null;
				// static field?
				try
				{
					if (Interpreter.DEBUG)
						Interpreter.debug("Name call to getStaticFieldValue, class: " + clas + ", field:" + field);
					obj = Reflect.getStaticFieldValue(clas, field);
				}
				catch (ReflectError e)
				{
					if (Interpreter.DEBUG)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						Interpreter.debug("field reflect error: " + e);
					}
				}
				
				// inner class?
				if (obj == null)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.String iclass = clas.FullName + "$" + field;
					System.Type c = namespace_Renamed.getClass(iclass);
					if (c != null)
						obj = new ClassIdentifier(c);
				}
				
				if (obj == null)
					throw new UtilEvalError("No static field or inner class: " + field + " of " + clas);
				
				return completeRound(field, suffix(evalName), obj);
			}
			
			/*
			If we've fallen through here we are no longer resolving to
			a class type.
			*/
			if (forceClass)
				throw new UtilEvalError(value_Renamed + " does not resolve to a class name.");
			
			/* 
			Some kind of field access?
			*/
			
			System.String field2 = prefix(evalName, 1);
			
			// length access on array? 
			if (field2.Equals("length") && evalBaseObject.GetType().IsArray)
			{
				System.Object obj = new Primitive(((System.Array) evalBaseObject).Length);
				return completeRound(field2, suffix(evalName), obj);
			}
			
			// Check for field on object 
			// Note: could eliminate throwing the exception somehow
			try
			{
				System.Object obj = Reflect.getObjectFieldValue(evalBaseObject, field2);
				return completeRound(field2, suffix(evalName), obj);
			}
			catch (ReflectError e)
			{
				/* not a field */
			}
			
			// if we get here we have failed
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			throw new UtilEvalError("Cannot access field: " + field2 + ", on object: " + evalBaseObject);
		}
		
		/// <summary>Resolve a variable relative to a This reference.
		/// This is the general variable resolution method, accomodating special
		/// fields from the This context.  Together the namespace and interpreter
		/// comprise the This context.  The callstack, if available allows for the
		/// this.caller construct.  
		/// Optionally interpret special "magic" field names: e.g. interpreter.
		/// <p/>
		/// </summary>
		/// <param name="callstack">may be null, but this is only legitimate in special
		/// cases where we are sure resolution will not involve this.caller.
		/// </param>
		/// <param name="namespace">the namespace of the this reference (should be the
		/// same as the top of the stack?
		/// </param>
		internal virtual System.Object resolveThisFieldReference(CallStack callstack, NameSpace thisNameSpace, Interpreter interpreter, System.String varName, bool specialFieldsVisible)
		{
			if (varName.Equals("this"))
			{
				/*
				Somewhat of a hack.  If the special fields are visible (we're
				operating relative to a 'this' type already) dissallow further
				.this references to prevent user from skipping to things like
				super.this.caller
				*/
				if (specialFieldsVisible)
					throw new UtilEvalError("Redundant to call .this on This type");
				
				// Allow getThis() to work through BlockNameSpace to the method
				// namespace
				// XXX re-eval this... do we need it?
				This ths = thisNameSpace.getThis(interpreter);
				thisNameSpace = ths.NameSpace;
				System.Object result = ths;
				
				NameSpace classNameSpace = getClassNameSpace(thisNameSpace);
				if (classNameSpace != null)
				{
					if (isCompound(evalName))
						result = classNameSpace.getThis(interpreter);
					else
						result = classNameSpace.ClassInstance;
				}
				
				return result;
			}
			
			/*
			Some duplication for "super".  See notes for "this" above
			If we're in an enclsing class instance and have a superclass
			instance our super is the superclass instance.
			*/
			if (varName.Equals("super"))
			{
				//if ( specialFieldsVisible )
				//throw new UtilEvalError("Redundant to call .this on This type");
				
				// Allow getSuper() to through BlockNameSpace to the method's super
				This ths = thisNameSpace.getSuper(interpreter);
				thisNameSpace = ths.NameSpace;
				// super is now the closure's super or class instance
				
				// XXXX re-evaluate this
				// can getSuper work by itself now?
				// If we're a class instance and the parent is also a class instance
				// then super means our parent.
				if (thisNameSpace.Parent != null && thisNameSpace.Parent.isClass)
					ths = thisNameSpace.Parent.getThis(interpreter);
				
				return ths;
			}
			
			System.Object obj = null;
			
			if (varName.Equals("global"))
				obj = thisNameSpace.getGlobal(interpreter);
			
			if (obj == null && specialFieldsVisible)
			{
				if (varName.Equals("namespace"))
					obj = thisNameSpace;
				else if (varName.Equals("variables"))
					obj = thisNameSpace.VariableNames;
				else if (varName.Equals("methods"))
					obj = thisNameSpace.MethodNames;
				else if (varName.Equals("interpreter"))
					if (lastEvalName.Equals("this"))
						obj = interpreter;
					else
						throw new UtilEvalError("Can only call .interpreter on literal 'this'");
			}
			
			if (obj == null && specialFieldsVisible && varName.Equals("caller"))
			{
				if (lastEvalName.Equals("this") || lastEvalName.Equals("caller"))
				{
					// get the previous context (see notes for this class)
					if (callstack == null)
						throw new InterpreterError("no callstack");
					obj = callstack.get_Renamed(++callstackDepth).getThis(interpreter);
				}
				else
					throw new UtilEvalError("Can only call .caller on literal 'this' or literal '.caller'");
				
				// early return
				return obj;
			}
			
			if (obj == null && specialFieldsVisible && varName.Equals("callstack"))
			{
				if (lastEvalName.Equals("this"))
				{
					// get the previous context (see notes for this class)
					if (callstack == null)
						throw new InterpreterError("no callstack");
					obj = callstack;
				}
				else
					throw new UtilEvalError("Can only call .callstack on literal 'this'");
			}
			
			
			if (obj == null)
				obj = thisNameSpace.getVariable(varName);
			
			if (obj == null)
				throw new InterpreterError("null this field ref:" + varName);
			
			return obj;
		}
		
		/// <returns> the enclosing class body namespace or null if not in a class.
		/// </returns>
		internal static NameSpace getClassNameSpace(NameSpace thisNameSpace)
		{
			// is a class instance
			//if ( thisNameSpace.classInstance != null )
			if (thisNameSpace.isClass)
				return thisNameSpace;
			
			if (thisNameSpace.isMethod && thisNameSpace.Parent != null && thisNameSpace.Parent.isClass)
				return thisNameSpace.Parent;
			
			return null;
		}
		
		/// <summary>Check the cache, else use toObject() to try to resolve to a class
		/// identifier.  
		/// </summary>
		/// <throws>  ClassNotFoundException on class not found. </throws>
		/// <throws>  ClassPathException (type of EvalError) on special case of  </throws>
		/// <summary>ambiguous unqualified name after super import. 
		/// </summary>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'toClass'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual System.Type toClass()
		{
			lock (this)
			{
				if (asClass != null)
					return asClass;
				
				reset();
				
				// "var" means untyped, return null class
				if (evalName.Equals("var"))
					return asClass = null;
				
				/* Try straightforward class name first */
				System.Type clas = namespace_Renamed.getClass(evalName);
				
				if (clas == null)
				{
					/* 
					Try toObject() which knows how to work through inner classes
					and see what we end up with 
					*/
					System.Object obj = null;
					try
					{
						// Null interpreter and callstack references.
						// class only resolution should not require them.
						obj = toObject(null, null, true);
					}
					catch (UtilEvalError e)
					{
					} // couldn't resolve it
					
					if (obj is ClassIdentifier)
						clas = ((ClassIdentifier) obj).TargetClass;
				}
				
				if (clas == null)
				{
					//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
					throw new System.Exception("Class: " + value_Renamed + " not found in namespace");
				}
				
				asClass = clas;
				return asClass;
			}
		}
		
		/*
		*/
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'toLHS'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual LHS toLHS(CallStack callstack, Interpreter interpreter)
		{
			lock (this)
			{
				// Should clean this up to a single return statement
				reset();
				LHS lhs;
				
				// Simple (non-compound) variable assignment e.g. x=5;
				if (!isCompound(evalName))
				{
					if (evalName.Equals("this"))
						throw new UtilEvalError("Can't assign to 'this'.");
					
					// Interpreter.debug("Simple var LHS...");
					lhs = new LHS(namespace_Renamed, evalName, false);
					return lhs;
				}
				
				// Field e.g. foo.bar=5;
				System.Object obj = null;
				try
				{
					while (evalName != null && isCompound(evalName))
					{
						obj = consumeNextObjectField(callstack, interpreter, false, true);
					}
				}
				catch (UtilEvalError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new UtilEvalError("LHS evaluation: " + e.Message);
				}
				
				// Finished eval and its a class.
				if (evalName == null && obj is ClassIdentifier)
					throw new UtilEvalError("Can't assign to class: " + value_Renamed);
				
				if (obj == null)
					throw new UtilEvalError("Error in LHS: " + value_Renamed);
				
				// e.g. this.x=5;  or someThisType.x=5;
				if (obj is This)
				{
					// dissallow assignment to magic fields
					if (evalName.Equals("namespace") || evalName.Equals("variables") || evalName.Equals("methods") || evalName.Equals("caller"))
						throw new UtilEvalError("Can't assign to special variable: " + evalName);
					
					Interpreter.debug("found This reference evaluating LHS");
					/*
					If this was a literal "super" reference then we allow recursion
					in setting the variable to get the normal effect of finding the
					nearest definition starting at the super scope.  On any other
					resolution qualified by a 'this' type reference we want to set
					the variable directly in that scope. e.g. this.x=5;  or 
					someThisType.x=5;
					
					In the old scoping rules super didn't do this.
					*/
					bool localVar = !lastEvalName.Equals("super");
					return new LHS(((This) obj).namespace_Renamed, evalName, localVar);
				}
				
				if (evalName != null)
				{
					try
					{
						if (obj is ClassIdentifier)
						{
							System.Type clas = ((ClassIdentifier) obj).TargetClass;
							lhs = Reflect.getLHSStaticField(clas, evalName);
							return lhs;
						}
						else
						{
							lhs = Reflect.getLHSObjectField(obj, evalName);
							return lhs;
						}
					}
					catch (ReflectError e)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						throw new UtilEvalError("Field access: " + e);
					}
				}
				
				throw new InterpreterError("Internal error in lhs...");
			}
		}
		
		/// <summary>Invoke the method identified by this name.
		/// Performs caching of method resolution using SignatureKey.
		/// <p>
		/// Name contains a wholely unqualfied messy name; resolve it to 
		/// ( object | static prefix ) + method name and invoke.
		/// <p>
		/// The interpreter is necessary to support 'this.interpreter' references
		/// in the called code. (e.g. debug());
		/// <p>
		/// <pre>
		/// Some cases:
		/// // dynamic
		/// local();
		/// myVariable.foo();
		/// myVariable.bar.blah.foo();
		/// // static
		/// java.lang.Integer.getInteger("foo");
		/// </pre>
		/// </summary>
		public virtual System.Object invokeMethod(Interpreter interpreter, System.Object[] args, CallStack callstack, SimpleNode callerInfo)
		{
			System.String methodName = Name.suffix(value_Renamed, 1);
			BshClassManager bcm = interpreter.ClassManager;
			NameSpace namespace_Renamed = callstack.top();
			
			// Optimization - If classOfStaticMethod is set then we have already 
			// been here and determined that this is a static method invocation.
			// Note: maybe factor this out with path below... clean up.
			if (classOfStaticMethod != null)
			{
				return Reflect.invokeStaticMethod(bcm, classOfStaticMethod, methodName, args);
			}
			
			if (!Name.isCompound(value_Renamed))
				return invokeLocalMethod(interpreter, args, callstack, callerInfo);
			
			// Note: if we want methods declared inside blocks to be accessible via
			// this.methodname() inside the block we could handle it here as a
			// special case.  See also resolveThisFieldReference() special handling
			// for BlockNameSpace case.  They currently work via the direct name
			// e.g. methodName().
			
			System.String prefix = Name.prefix(value_Renamed);
			
			// Superclass method invocation? (e.g. super.foo())
			if (prefix.Equals("super") && Name.countParts(value_Renamed) == 2)
			{
				// Allow getThis() to work through block namespaces first
				This ths = namespace_Renamed.getThis(interpreter);
				NameSpace thisNameSpace = ths.NameSpace;
				NameSpace classNameSpace = getClassNameSpace(thisNameSpace);
				if (classNameSpace != null)
				{
					System.Object instance = classNameSpace.ClassInstance;
					return ClassGenerator.getClassGenerator().invokeSuperclassMethod(bcm, instance, methodName, args);
				}
			}
			
			// Find target object or class identifier
			Name targetName = namespace_Renamed.getNameResolver(prefix);
			System.Object obj = targetName.toObject(callstack, interpreter);
			
			if (obj == Primitive.VOID)
			{
				throw new UtilEvalError("Attempt to resolve method: " + methodName + "() on undefined variable or class name: " + targetName);
			}
			
			// if we've got an object, resolve the method
			if (!(obj is ClassIdentifier))
			{
				
				if (obj is Primitive)
				{
					
					if (obj == Primitive.NULL)
						throw new UtilTargetError(new System.NullReferenceException("Null Pointer in Method Invocation"));
					
					// some other primitive
					// should avoid calling methods on primitive, as we do
					// in Name (can't treat primitive like an object message)
					// but the hole is useful right now.
					if (Interpreter.DEBUG)
						bsh.Interpreter.debug("Attempt to access method on primitive..." + " allowing bsh.Primitive to peek through for debugging");
				}
				
				// found an object and it's not an undefined variable
				return Reflect.invokeObjectMethod(obj, methodName, args, interpreter, callstack, callerInfo);
			}
			
			// It's a class
			
			// try static method
			if (Interpreter.DEBUG)
			{
				Interpreter.debug("invokeMethod: trying static - " + targetName);
			}
			
			System.Type clas = ((ClassIdentifier) obj).TargetClass;
			
			// cache the fact that this is a static method invocation on this class
			classOfStaticMethod = clas;
			
			if (clas != null)
				return Reflect.invokeStaticMethod(bcm, clas, methodName, args);
			
			// return null; ???
			throw new UtilEvalError("invokeMethod: unknown target: " + targetName);
		}
		
		/// <summary>Invoke a locally declared method or a bsh command.
		/// If the method is not already declared in the namespace then try
		/// to load it as a resource from the imported command path (e.g.
		/// /bsh/commands)
		/// </summary>
		/*
		Note: the bsh command code should probably not be here...  we need to
		scope it by the namespace that imported the command... so it probably
		needs to be integrated into NameSpace.
		*/
		private System.Object invokeLocalMethod(Interpreter interpreter, System.Object[] args, CallStack callstack, SimpleNode callerInfo)
		{
			if (Interpreter.DEBUG)
				Interpreter.debug("invokeLocalMethod: " + value_Renamed);
			if (interpreter == null)
				throw new InterpreterError("invokeLocalMethod: interpreter = null");
			
			System.String commandName = value_Renamed;
			System.Type[] argTypes = Types.getTypes(args);
			
			// Check for existing method
			BshMethod meth = null;
			try
			{
				meth = namespace_Renamed.getMethod(commandName, argTypes);
			}
			catch (UtilEvalError e)
			{
				throw e.toEvalError("Local method invocation", callerInfo, callstack);
			}
			
			// If defined, invoke it
			if (meth != null)
				return meth.invoke(args, interpreter, callstack, callerInfo);
			
			BshClassManager bcm = interpreter.ClassManager;
			
			// Look for a BeanShell command
			
			System.Object commandObject;
			try
			{
				commandObject = namespace_Renamed.getCommand(commandName, argTypes, interpreter);
			}
			catch (UtilEvalError e)
			{
				throw e.toEvalError("Error loading command: ", callerInfo, callstack);
			}
			
			// should try to print usage here if nothing found
			if (commandObject == null)
			{
				// Look for a default invoke() handler method in the namespace
				// Note: this code duplicates that in This.java... should it?
				// Call on 'This' can never be a command
				BshMethod invokeMethod = null;
				try
				{
					invokeMethod = namespace_Renamed.getMethod("invoke", new System.Type[]{null, null});
				}
				catch (UtilEvalError e)
				{
					throw e.toEvalError("Local method invocation", callerInfo, callstack);
				}
				
				if (invokeMethod != null)
					return invokeMethod.invoke(new System.Object[]{commandName, args}, interpreter, callstack, callerInfo);
				
				throw new EvalError("Command not found: " + StringUtil.methodString(commandName, argTypes), callerInfo, callstack);
			}
			
			if (commandObject is BshMethod)
				return ((BshMethod) commandObject).invoke(args, interpreter, callstack, callerInfo);
			
			if (commandObject is System.Type)
				try
				{
					return Reflect.invokeCompiledCommand(((System.Type) commandObject), args, interpreter, callstack);
				}
				catch (UtilEvalError e)
				{
					throw e.toEvalError("Error invoking compiled command: ", callerInfo, callstack);
				}
			
			throw new InterpreterError("invalid command type");
		}
		
		/*
		private String getHelp( String name )
		throws UtilEvalError
		{
		try {
		// should check for null namespace here
		return get( "bsh.help."+name, null/interpreter/ );
		} catch ( Exception e ) {
		return "usage: "+name;
		}
		}
		
		private String getHelp( Class commandClass )
		throws UtilEvalError
		{
		try {
		return (String)Reflect.invokeStaticMethod(
		null/bcm/, commandClass, "usage", null );
		} catch( Exception e )
		return "usage: "+name;
		}
		}*/
		
		// Static methods that operate on compound ('.' separated) names
		// I guess we could move these to StringUtil someday
		
		public static bool isCompound(System.String value_Renamed)
		{
			return value_Renamed.IndexOf('.') != - 1;
			//return countParts(value) > 1;
		}
		
		internal static int countParts(System.String value_Renamed)
		{
			if (value_Renamed == null)
				return 0;
			
			int count = 0;
			int index = - 1;
			//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'System.String.IndexOf' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
			while ((index = value_Renamed.IndexOf('.', index + 1)) != - 1)
				count++;
			return count + 1;
		}
		
		internal static System.String prefix(System.String value_Renamed)
		{
			if (!isCompound(value_Renamed))
				return null;
			
			return prefix(value_Renamed, countParts(value_Renamed) - 1);
		}
		
		internal static System.String prefix(System.String value_Renamed, int parts)
		{
			if (parts < 1)
				return null;
			
			int count = 0;
			int index = - 1;
			
			//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'System.String.IndexOf' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
			while (((index = value_Renamed.IndexOf('.', index + 1)) != - 1) && (++count < parts))
			{
				;
			}
			
			return (index == - 1)?value_Renamed:value_Renamed.Substring(0, (index) - (0));
		}
		
		internal static System.String suffix(System.String name)
		{
			if (!isCompound(name))
				return null;
			
			return suffix(name, countParts(name) - 1);
		}
		
		public static System.String suffix(System.String value_Renamed, int parts)
		{
			if (parts < 1)
				return null;
			
			int count = 0;
			int index = value_Renamed.Length + 1;
			
			//UPGRADE_WARNING: Method 'java.lang.String.lastIndexOf' was converted to 'System.String.LastIndexOf' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
			while (((index = value_Renamed.LastIndexOf('.', index - 1)) != - 1) && (++count < parts))
				;
			
			return (index == - 1)?value_Renamed:value_Renamed.Substring(index + 1);
		}
		
		// end compound name routines
		
		
		public override System.String ToString()
		{
			return value_Renamed;
		}
	}
}