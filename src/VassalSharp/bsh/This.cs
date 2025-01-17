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
using System.Runtime.InteropServices;
namespace bsh
{
	
	/// <summary>'This' is the type of bsh scripted objects.
	/// A 'This' object is a bsh scripted object context.  It holds a namespace 
	/// reference and implements event listeners and various other interfaces.
	/// This holds a reference to the declaring interpreter for callbacks from
	/// outside of bsh.
	/// </summary>
	[Serializable]
	public class This : IThreadRunnable
	{
		virtual public NameSpace NameSpace
		{
			get
			{
				return namespace_Renamed;
			}
			
		}
		/// <summary>The namespace that this This reference wraps.</summary>
		internal NameSpace namespace_Renamed;
		
		/// <summary>This is the interpreter running when the This ref was created.
		/// It's used as a default interpreter for callback through the This
		/// where there is no current interpreter instance 
		/// e.g. interface proxy or event call backs from outside of bsh.
		/// </summary>
		[NonSerialized]
		internal Interpreter declaringInterpreter;
		
		/// <summary>getThis() is a factory for bsh.This type references.  The capabilities
		/// of ".this" references in bsh are version dependent up until jdk1.3.
		/// The version dependence was to support different default interface
		/// implementations.  i.e. different sets of listener interfaces which
		/// scripted objects were capable of implementing.  In jdk1.3 the 
		/// reflection proxy mechanism was introduced which allowed us to 
		/// implement arbitrary interfaces.  This is fantastic.
		/// A This object is a thin layer over a namespace, comprising a bsh object
		/// context.  We create it here only if needed for the namespace.
		/// Note: this method could be considered slow because of the way it 
		/// dynamically factories objects.  However I've also done tests where 
		/// I hard-code the factory to return JThis and see no change in the 
		/// rough test suite time.  This references are also cached in NameSpace.  
		/// </summary>
		internal static This getThis(NameSpace namespace_Renamed, Interpreter declaringInterpreter)
		{
			try
			{
				System.Type c;
				if (Capabilities.canGenerateInterfaces())
				{
					//UPGRADE_TODO: The differences in the format  of parameters for method 'java.lang.Class.forName'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					c = System.Type.GetType("bsh.XThis");
				}
				else if (Capabilities.haveSwing())
				{
					//UPGRADE_TODO: The differences in the format  of parameters for method 'java.lang.Class.forName'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					c = System.Type.GetType("bsh.JThis");
				}
				else
					return new This(namespace_Renamed, declaringInterpreter);
				
				return (This) Reflect.constructObject(c, new System.Object[]{namespace_Renamed, declaringInterpreter});
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new InterpreterError("internal error 1 in This: " + e);
			}
		}
		
		/// <summary>Get a version of this scripted object implementing the specified 
		/// interface.
		/// </summary>
		/*
		If this type of This implements it directly return this,
		else try complain that we don't have the proxy mechanism.
		*/
		public virtual System.Object getInterface(System.Type clas)
		{
			if (clas.IsInstanceOfType(this))
				return this;
			else
				throw new UtilEvalError("Dynamic proxy mechanism not available. " + "Cannot construct interface type: " + clas);
		}
		
		/// <summary>Get a version of this scripted object implementing the specified
		/// interfaces.
		/// </summary>
		public virtual System.Object getInterface(System.Type[] ca)
		{
			for (int i = 0; i < ca.Length; i++)
				if (!(ca[i].IsInstanceOfType(this)))
					throw new UtilEvalError("Dynamic proxy mechanism not available. " + "Cannot construct interface type: " + ca[i]);
			
			return this;
		}
		
		/*
		I wish protected access were limited to children and not also 
		package scope... I want this to be a singleton implemented by various
		children.  
		*/
		protected internal This(NameSpace namespace_Renamed, Interpreter declaringInterpreter)
		{
			this.namespace_Renamed = namespace_Renamed;
			this.declaringInterpreter = declaringInterpreter;
			//initCallStack( namespace );
		}
		
		public override System.String ToString()
		{
			return "'this' reference to Bsh object: " + namespace_Renamed;
		}
		
		public virtual void  Run()
		{
			try
			{
				invokeMethod("run", new System.Object[0]);
			}
			catch (EvalError e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				declaringInterpreter.error("Exception in runnable:" + e);
			}
		}
		
		/// <summary>Invoke specified method as from outside java code, using the 
		/// declaring interpreter and current namespace.
		/// The call stack will indicate that the method is being invoked from
		/// outside of bsh in native java code.
		/// Note: you must still wrap/unwrap args/return values using 
		/// Primitive/Primitive.unwrap() for use outside of BeanShell.
		/// </summary>
		/// <seealso cref="bsh.Primitive">
		/// </seealso>
		public virtual System.Object invokeMethod(System.String name, System.Object[] args)
		{
			// null callstack, one will be created for us 
			return invokeMethod(name, args, null, null, null, false);
		}
		
		/// <summary>Invoke a method in this namespace with the specified args,
		/// interpreter reference, callstack, and caller info.
		/// <p>
		/// Note: If you use this method outside of the bsh package and wish to 
		/// use variables with primitive values you will have to wrap them using 
		/// bsh.Primitive.  Consider using This getInterface() to make a true Java
		/// interface for invoking your scripted methods.
		/// <p>
		/// This method also implements the default object protocol of toString(), 
		/// hashCode() and equals() and the invoke() meta-method handling as a 
		/// last resort.
		/// <p>
		/// Note: The invoke() meta-method will not catch the Object protocol
		/// methods (toString(), hashCode()...).  If you want to override them you 
		/// have to script them directly.
		/// <p>
		/// </summary>
		/// <seealso cref="bsh.This.invokeMethod(">
		/// String methodName, Object [] args, Interpreter interpreter, 
		/// CallStack callstack, SimpleNode callerInfo )
		/// </seealso>
		/// <param name="if">callStack is null a new CallStack will be created and
		/// initialized with this namespace.
		/// </param>
		/// <param name="declaredOnly">if true then only methods declared directly in the
		/// namespace will be visible - no inherited or imported methods will
		/// be visible.
		/// </param>
		/// <seealso cref="bsh.Primitive">
		/// </seealso>
		/*
		invokeMethod() here is generally used by outside code to callback
		into the bsh interpreter. e.g. when we are acting as an interface
		for a scripted listener, etc.  In this case there is no real call stack
		so we make a default one starting with the special JAVACODE namespace
		and our namespace as the next.
		*/
		public virtual System.Object invokeMethod(System.String methodName, System.Object[] args, Interpreter interpreter, CallStack callstack, SimpleNode callerInfo, bool declaredOnly)
		{
			/*
			Wrap nulls.
			This is a bit of a cludge to address a deficiency in the class
			generator whereby it does not wrap nulls on method delegate.  See
			Class Generator.java.  If we fix that then we can remove this.
			(just have to generate the code there.)
			*/
			if (args != null)
			{
				System.Object[] oa = new System.Object[args.Length];
				for (int i = 0; i < args.Length; i++)
					oa[i] = (args[i] == null?Primitive.NULL:args[i]);
				args = oa;
			}
			
			if (interpreter == null)
				interpreter = declaringInterpreter;
			if (callstack == null)
				callstack = new CallStack(namespace_Renamed);
			if (callerInfo == null)
				callerInfo = SimpleNode.JAVACODE;
			
			// Find the bsh method
			System.Type[] types = Types.getTypes(args);
			BshMethod bshMethod = null;
			try
			{
				bshMethod = namespace_Renamed.getMethod(methodName, types, declaredOnly);
			}
			catch (UtilEvalError e)
			{
				// leave null
			}
			
			if (bshMethod != null)
				return bshMethod.invoke(args, interpreter, callstack, callerInfo);
			
			/*
			No scripted method of that name.
			Implement the required part of the Object protocol:
			public int hashCode();
			public boolean equals(java.lang.Object);
			public java.lang.String toString();
			if these were not handled by scripted methods we must provide
			a default impl.
			*/
			// a default toString() that shows the interfaces we implement
			if (methodName.Equals("toString"))
			{
				return ToString();
			}
			
			// a default hashCode()
			if (methodName.Equals("hashCode"))
				return Integer.valueOf(this.GetHashCode());
			
			// a default equals() testing for equality with the This reference
			if (methodName.Equals("equals"))
			{
				System.Object obj = args[0];
				return Boolean.valueOf(this == obj);
			}
			
			// Look for a default invoke() handler method in the namespace
			// Note: this code duplicates that in NameSpace getCommand()
			// is that ok?
			try
			{
				bshMethod = namespace_Renamed.getMethod("invoke", new System.Type[]{null, null});
			}
			catch (UtilEvalError e)
			{
				/*leave null*/
			}
			
			// Call script "invoke( String methodName, Object [] args );
			if (bshMethod != null)
				return bshMethod.invoke(new System.Object[]{methodName, args}, interpreter, callstack, callerInfo);
			
			throw new EvalError("Method " + StringUtil.methodString(methodName, types) + " not found in bsh scripted object: " + namespace_Renamed.Name, callerInfo, callstack);
		}
		
		/// <summary>Bind a This reference to a parent's namespace with the specified
		/// declaring interpreter.  Also re-init the callstack.  It's necessary 
		/// to bind a This reference before it can be used after deserialization.
		/// This is used by the bsh load() command.
		/// <p>
		/// This is a static utility method because it's used by a bsh command
		/// bind() and the interpreter doesn't currently allow access to direct 
		/// methods of This objects (small hack)
		/// </summary>
		public static void  bind(This ths, NameSpace namespace_Renamed, Interpreter declaringInterpreter)
		{
			ths.namespace_Renamed.Parent = namespace_Renamed;
			ths.declaringInterpreter = declaringInterpreter;
		}
		
		/// <summary>Allow invocations of these method names on This type objects.
		/// Don't give bsh.This a chance to override their behavior.
		/// <p>
		/// If the method is passed here the invocation will actually happen on
		/// the bsh.This object via the regular reflective method invocation 
		/// mechanism.  If not, then the method is evaluated by bsh.This itself
		/// as a scripted method call.
		/// </summary>
		internal static bool isExposedThisMethod(System.String name)
		{
			return name.Equals("getClass") || name.Equals("invokeMethod") || name.Equals("getInterface") || name.Equals("wait") || name.Equals("notify") || name.Equals("notifyAll");
		}
	}
}