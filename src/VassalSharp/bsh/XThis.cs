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
	
	/// <summary>XThis is a dynamically loaded extension which extends This.java and adds 
	/// support for the generalized interface proxy mechanism introduced in 
	/// JDK1.3.  XThis allows bsh scripted objects to implement arbitrary 
	/// interfaces (be arbitrary event listener types).
	/// Note: This module relies on new features of JDK1.3 and will not compile
	/// with JDK1.2 or lower.  For those environments simply do not compile this
	/// class.
	/// Eventually XThis should become simply This, but for backward compatability
	/// we will maintain This without requiring support for the proxy mechanism.
	/// XThis stands for "eXtended This" (I had to call it something).
	/// </summary>
	/// <seealso cref="JThis	 See also JThis with explicit JFC support for compatability.">
	/// </seealso>
	/// <seealso cref="This">
	/// </seealso>
	[Serializable]
	public class XThis:This
	{
		private void  InitBlock()
		{
			invocationHandler = new Handler(this);
		}
		/// <summary>A cache of proxy interface handlers.
		/// Currently just one per interface.
		/// </summary>
		internal System.Collections.Hashtable interfaces;
		
		//UPGRADE_ISSUE: Interface 'java.lang.reflect.InvocationHandler' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectInvocationHandler'"
		//UPGRADE_NOTE: The initialization of  'invocationHandler' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		internal InvocationHandler invocationHandler;
		
		public XThis(NameSpace namespace_Renamed, Interpreter declaringInterp):base(namespace_Renamed, declaringInterp)
		{
			InitBlock();
		}
		
		public override System.String ToString()
		{
			return "'this' reference (XThis) to Bsh object: " + namespace_Renamed;
		}
		
		/// <summary>Get dynamic proxy for interface, caching those it creates.</summary>
		public override System.Object getInterface(System.Type clas)
		{
			return getInterface(new System.Type[]{clas});
		}
		
		/// <summary>Get dynamic proxy for interface, caching those it creates.</summary>
		public override System.Object getInterface(System.Type[] ca)
		{
			if (interfaces == null)
				interfaces = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
			
			// Make a hash of the interface hashcodes in order to cache them
			int hash = 21;
			for (int i = 0; i < ca.Length; i++)
				hash *= (ca[i].GetHashCode() + 3);
			System.Object hashKey = Integer.valueOf(hash);
			
			System.Object interf = interfaces[hashKey];
			
			if (interf == null)
			{
				//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
				//UPGRADE_ISSUE: Method 'java.lang.Class.getClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetClassLoader'"
				ClassLoader classLoader = ca[0].getClassLoader(); // ?
				//UPGRADE_ISSUE: Method 'java.lang.reflect.Proxy.newProxyInstance' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectProxy'"
				interf = Proxy.newProxyInstance(classLoader, ca, invocationHandler);
				interfaces[hashKey] = interf;
			}
			
			return interf;
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Handler' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary>This is the invocation handler for the dynamic proxy.
		/// <p>
		/// Notes:
		/// Inner class for the invocation handler seems to shield this unavailable
		/// interface from JDK1.2 VM...  
		/// </summary>
		/// <summary>I don't understand this.  JThis works just fine even if those
		/// classes aren't there (doesn't it?)  This class shouldn't be loaded
		/// if an XThis isn't instantiated in NameSpace.java, should it?
		/// </summary>
		//UPGRADE_ISSUE: Interface 'java.lang.reflect.InvocationHandler' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangreflectInvocationHandler'"
		[Serializable]
		internal class Handler : InvocationHandler
		{
			public Handler(XThis enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(XThis enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private XThis enclosingInstance;
			public XThis Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual System.Object invoke(System.Object proxy, System.Reflection.MethodInfo method, System.Object[] args)
			{
				try
				{
					return invokeImpl(proxy, method, args);
				}
				catch (TargetError te)
				{
					// Unwrap target exception.  If the interface declares that 
					// it throws the ex it will be delivered.  If not it will be 
					// wrapped in an UndeclaredThrowable
					throw te.Target;
				}
				catch (EvalError ee)
				{
					// Ease debugging...
					// XThis.this refers to the enclosing class instance
					if (Interpreter.DEBUG)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						Interpreter.debug("EvalError in scripted interface: " + Enclosing_Instance.ToString() + ": " + ee);
					}
					throw ee;
				}
			}
			
			public virtual System.Object invokeImpl(System.Object proxy, System.Reflection.MethodInfo method, System.Object[] args)
			{
				System.String methodName = method.Name;
				CallStack callstack = new CallStack(Enclosing_Instance.namespace_Renamed);
				
				/*
				If equals() is not explicitly defined we must override the 
				default implemented by the This object protocol for scripted
				object.  To support XThis equals() must test for equality with 
				the generated proxy object, not the scripted bsh This object;
				otherwise callers from outside in Java will not see a the 
				proxy object as equal to itself.
				*/
				BshMethod equalsMethod = null;
				try
				{
					equalsMethod = Enclosing_Instance.namespace_Renamed.getMethod("equals", new System.Type[]{typeof(System.Object)});
				}
				catch (UtilEvalError e)
				{
					/*leave null*/
				}
				if (methodName.Equals("equals") && equalsMethod == null)
				{
					System.Object obj = args[0];
					return Boolean.valueOf(proxy == obj);
				}
				
				/*
				If toString() is not explicitly defined override the default 
				to show the proxy interfaces.
				*/
				BshMethod toStringMethod = null;
				try
				{
					toStringMethod = Enclosing_Instance.namespace_Renamed.getMethod("toString", new System.Type[]{});
				}
				catch (UtilEvalError e)
				{
					/*leave null*/
				}
				
				if (methodName.Equals("toString") && toStringMethod == null)
				{
					System.Type[] ints = proxy.GetType().GetInterfaces();
					// XThis.this refers to the enclosing class instance
					System.Text.StringBuilder sb = new System.Text.StringBuilder(Enclosing_Instance.ToString() + "\nimplements:");
					for (int i = 0; i < ints.Length; i++)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						sb.Append(" " + ints[i].FullName + ((ints.Length > 1)?",":""));
					}
					return sb.ToString();
				}
				
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Type[] paramTypes = method.GetParameters();
				return Primitive.unwrap(Enclosing_Instance.invokeMethod(methodName, Primitive.wrap(args, paramTypes)));
			}
		}
		
	}
}