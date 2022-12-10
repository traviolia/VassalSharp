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
	
	/// <summary>All of the reflection API code lies here.  It is in the form of static
	/// utilities.  Maybe this belongs in LHS.java or a generic object
	/// wrapper class.
	/// </summary>
	/*
	Note: This class is messy.  The method and field resolution need to be
	rewritten.  Various methods in here catch NoSuchMethod or NoSuchField
	exceptions during their searches.  These should be rewritten to avoid
	having to catch the exceptions.  Method lookups are now cached at a high 
	level so they are less important, however the logic is messy.*/
	class Reflect
	{
		/// <summary>Invoke method on arbitrary object instance.
		/// invocation may be static (through the object instance) or dynamic.
		/// Object may be a bsh scripted object (bsh.This type).
		/// </summary>
		/// <returns> the result of the method call
		/// </returns>
		public static System.Object invokeObjectMethod(System.Object object_Renamed, System.String methodName, System.Object[] args, Interpreter interpreter, CallStack callstack, SimpleNode callerInfo)
		{
			// Bsh scripted object
			if (object_Renamed is This && !This.isExposedThisMethod(methodName))
				return ((This) object_Renamed).invokeMethod(methodName, args, interpreter, callstack, callerInfo, false);
			
			// Plain Java object, find the java method
			try
			{
				BshClassManager bcm = interpreter == null?null:interpreter.ClassManager;
				System.Type clas = object_Renamed.GetType();
				
				System.Reflection.MethodInfo method = resolveExpectedJavaMethod(bcm, clas, object_Renamed, methodName, args, false);
				
				return invokeMethod(method, object_Renamed, args);
			}
			catch (UtilEvalError e)
			{
				throw e.toEvalError(callerInfo, callstack);
			}
		}
		
		/// <summary>Invoke a method known to be static.
		/// No object instance is needed and there is no possibility of the 
		/// method being a bsh scripted method.
		/// </summary>
		public static System.Object invokeStaticMethod(BshClassManager bcm, System.Type clas, System.String methodName, System.Object[] args)
		{
			Interpreter.debug("invoke static Method");
			System.Reflection.MethodInfo method = resolveExpectedJavaMethod(bcm, clas, (System.Object) null, methodName, args, true);
			return invokeMethod(method, (System.Object) null, args);
		}
		
		/// <summary>Invoke the Java method on the specified object, performing needed
		/// type mappings on arguments and return values.
		/// </summary>
		/// <param name="args">may be null
		/// </param>
		internal static System.Object invokeMethod(System.Reflection.MethodInfo method, System.Object object_Renamed, System.Object[] args)
		{
			if (args == null)
				args = new System.Object[0];
			
			logInvokeMethod("Invoking method (entry): ", method, args);
			
			// Map types to assignable forms, need to keep this fast...
			System.Object[] tmpArgs = new System.Object[args.Length];
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.Type[] types = method.GetParameters();
			try
			{
				for (int i = 0; i < args.Length; i++)
					tmpArgs[i] = Types.castObject(args[i], types[i], Types.ASSIGNMENT);
			}
			catch (UtilEvalError e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new InterpreterError("illegal argument type in method invocation: " + e);
			}
			
			// unwrap any primitives
			tmpArgs = Primitive.unwrap(tmpArgs);
			
			logInvokeMethod("Invoking method (after massaging values): ", method, tmpArgs);
			
			try
			{
				System.Object returnValue = method.Invoke(object_Renamed, (System.Object[]) tmpArgs);
				if (returnValue == null)
					returnValue = Primitive.NULL;
				System.Type returnType = method.ReturnType;
				
				return Primitive.wrap(returnValue, returnType);
			}
			catch (System.UnauthorizedAccessException e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new ReflectError("Cannot access method " + StringUtil.methodString(method.Name, method.GetParameters()) + " in '" + method.DeclaringType + "' :" + e);
			}
		}
		
		public static System.Object getIndex(System.Object array, int index)
		{
			if (Interpreter.DEBUG)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				Interpreter.debug("getIndex: " + array + ", index=" + index);
			}
			try
			{
				System.Object val = ((System.Array) array).GetValue(index);
				return Primitive.wrap(val, array.GetType().GetElementType());
			}
			catch (System.IndexOutOfRangeException e1)
			{
				throw new UtilTargetError(e1);
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new ReflectError("Array access:" + e);
			}
		}
		
		public static void  setIndex(System.Object array, int index, System.Object val)
		{
			try
			{
				val = Primitive.unwrap(val);
				((System.Array) array).SetValue(val, index);
			}
			catch (System.ArrayTypeMismatchException e2)
			{
				throw new UtilTargetError(e2);
			}
			catch (System.ArgumentException e1)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new UtilTargetError(new System.ArrayTypeMismatchException(e1.ToString()));
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new ReflectError("Array access:" + e);
			}
		}
		
		public static System.Object getStaticFieldValue(System.Type clas, System.String fieldName)
		{
			return getFieldValue(clas, (System.Object) null, fieldName, true);
		}
		
		
		public static System.Object getObjectFieldValue(System.Object object_Renamed, System.String fieldName)
		{
			if (object_Renamed is This)
				return ((This) object_Renamed).namespace_Renamed.getVariable(fieldName);
			else
			{
				try
				{
					return getFieldValue(object_Renamed.GetType(), object_Renamed, fieldName, false);
				}
				catch (ReflectError e)
				{
					// no field, try property acces
					
					if (hasObjectPropertyGetter(object_Renamed.GetType(), fieldName))
						return getObjectProperty(object_Renamed, fieldName);
					else
						throw e;
				}
			}
		}
		
		internal static LHS getLHSStaticField(System.Type clas, System.String fieldName)
		{
			System.Reflection.FieldInfo f = resolveExpectedJavaField(clas, fieldName, true);
			return new LHS(f);
		}
		
		/// <summary>Get an LHS reference to an object field.
		/// This method also deals with the field style property access.
		/// In the field does not exist we check for a property setter.
		/// </summary>
		internal static LHS getLHSObjectField(System.Object object_Renamed, System.String fieldName)
		{
			if (object_Renamed is This)
			{
				// I guess this is when we pass it as an argument?
				// Setting locally
				bool recurse = false;
				return new LHS(((This) object_Renamed).namespace_Renamed, fieldName, recurse);
			}
			
			try
			{
				System.Reflection.FieldInfo f = resolveExpectedJavaField(object_Renamed.GetType(), fieldName, false);
				return new LHS(object_Renamed, f);
			}
			catch (ReflectError e)
			{
				// not a field, try property access
				if (hasObjectPropertySetter(object_Renamed.GetType(), fieldName))
					return new LHS(object_Renamed, fieldName);
				else
					throw e;
			}
		}
		
		private static System.Object getFieldValue(System.Type clas, System.Object object_Renamed, System.String fieldName, bool staticOnly)
		{
			try
			{
				System.Reflection.FieldInfo f = resolveExpectedJavaField(clas, fieldName, staticOnly);
				
				System.Object value_Renamed = f.GetValue(object_Renamed);
				System.Type returnType = f.FieldType;
				return Primitive.wrap(value_Renamed, returnType);
			}
			catch (System.NullReferenceException e)
			{
				// shouldn't happen
				throw new ReflectError("???" + fieldName + " is not a static field.");
			}
			catch (System.UnauthorizedAccessException e)
			{
				throw new ReflectError("Can't access field: " + fieldName);
			}
		}
		
		/*
		Note: this method and resolveExpectedJavaField should be rewritten
		to invert this logic so that no exceptions need to be caught
		unecessarily.  This is just a temporary impl.
		@return the field or null if not found
		*/
		protected internal static System.Reflection.FieldInfo resolveJavaField(System.Type clas, System.String fieldName, bool staticOnly)
		{
			try
			{
				return resolveExpectedJavaField(clas, fieldName, staticOnly);
			}
			catch (ReflectError e)
			{
				return null;
			}
		}
		
		/// <throws>  ReflectError if the field is not found. </throws>
		/*
		Note: this should really just throw NoSuchFieldException... need
		to change related signatures and code.
		*/
		protected internal static System.Reflection.FieldInfo resolveExpectedJavaField(System.Type clas, System.String fieldName, bool staticOnly)
		{
			System.Reflection.FieldInfo field;
			try
			{
				if (Capabilities.haveAccessibility())
					field = findAccessibleField(clas, fieldName);
				// Class getField() finds only public (and in interfaces, etc.)
				else
				{
					//UPGRADE_TODO: The differences in the expected value  of parameters for method 'java.lang.Class.getField'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					field = clas.GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
				}
			}
			catch (System.FieldAccessException e)
			{
				throw new ReflectError("No such field: " + fieldName);
			}
			catch (System.Security.SecurityException e)
			{
				throw new UtilTargetError("Security Exception while searching fields of: " + clas, e);
			}
			
			if (staticOnly && !field.IsStatic)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new UtilEvalError("Can't reach instance field: " + fieldName + " from static context: " + clas.FullName);
			}
			
			return field;
		}
		
		/// <summary>Used when accessibility capability is available to locate an occurrance
		/// of the field in the most derived class or superclass and set its 
		/// accessibility flag.
		/// Note that this method is not needed in the simple non accessible
		/// case because we don't have to hunt for fields.
		/// Note that classes may declare overlapping private fields, so the 
		/// distinction about the most derived is important.  Java doesn't normally
		/// allow this kind of access (super won't show private variables) so 
		/// there is no real syntax for specifying which class scope to use...
		/// </summary>
		/// <returns> the Field or throws NoSuchFieldException
		/// </returns>
		/// <throws>  NoSuchFieldException if the field is not found </throws>
		/*
		This method should be rewritten to use getFields() and avoid catching
		exceptions during the search.
		*/
		private static System.Reflection.FieldInfo findAccessibleField(System.Type clas, System.String fieldName)
		{
			System.Reflection.FieldInfo field;
			
			// Quick check catches public fields include those in interfaces
			try
			{
				//UPGRADE_TODO: The differences in the expected value  of parameters for method 'java.lang.Class.getField'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				field = clas.GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static);
				ReflectManager.RMSetAccessible(field);
				return field;
			}
			catch (System.FieldAccessException e)
			{
			}
			
			// Now, on with the hunt...
			while (clas != null)
			{
				try
				{
					//UPGRADE_TODO: The differences in the expected value  of parameters for method 'java.lang.Class.getDeclaredField'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					field = clas.GetField(fieldName, System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Static);
					ReflectManager.RMSetAccessible(field);
					return field;
					
					// Not found, fall through to next class
				}
				catch (System.FieldAccessException e)
				{
				}
				
				clas = clas.BaseType;
			}
			throw new System.FieldAccessException(fieldName);
		}
		
		/// <summary>This method wraps resolveJavaMethod() and expects a non-null method
		/// result. If the method is not found it throws a descriptive ReflectError.
		/// </summary>
		protected internal static System.Reflection.MethodInfo resolveExpectedJavaMethod(BshClassManager bcm, System.Type clas, System.Object object_Renamed, System.String name, System.Object[] args, bool staticOnly)
		{
			if (object_Renamed == Primitive.NULL)
				throw new UtilTargetError(new System.NullReferenceException("Attempt to invoke method " + name + " on null value"));
			
			System.Type[] types = Types.getTypes(args);
			System.Reflection.MethodInfo method = resolveJavaMethod(bcm, clas, name, types, staticOnly);
			
			if (method == null)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new ReflectError((staticOnly?"Static method ":"Method ") + StringUtil.methodString(name, types) + " not found in class'" + clas.FullName + "'");
			}
			
			return method;
		}
		
		/// <summary>The full blown resolver method.  All other method invocation methods
		/// delegate to this.  The method may be static or dynamic unless
		/// staticOnly is set (in which case object may be null).
		/// If staticOnly is set then only static methods will be located.
		/// <p/>
		/// This method performs caching (caches discovered methods through the
		/// class manager and utilizes cached methods.)
		/// <p/>
		/// This method determines whether to attempt to use non-public methods
		/// based on Capabilities.haveAccessibility() and will set the accessibilty
		/// flag on the method as necessary.
		/// <p/>
		/// If, when directed to find a static method, this method locates a more 
		/// specific matching instance method it will throw a descriptive exception 
		/// analogous to the error that the Java compiler would produce.
		/// Note: as of 2.0.x this is a problem because there is no way to work
		/// around this with a cast. 
		/// <p/>
		/// </summary>
		/// <param name="staticOnly">The method located must be static, the object param may be null.
		/// </param>
		/// <returns> the method or null if no matching method was found.
		/// </returns>
		protected internal static System.Reflection.MethodInfo resolveJavaMethod(BshClassManager bcm, System.Type clas, System.String name, System.Type[] types, bool staticOnly)
		{
			if (clas == null)
				throw new InterpreterError("null class");
			
			// Lookup previously cached method
			System.Reflection.MethodInfo method = null;
			if (bcm == null)
				Interpreter.debug("resolveJavaMethod UNOPTIMIZED lookup");
			else
				method = bcm.getResolvedMethod(clas, name, types, staticOnly);
			
			if (method == null)
			{
				bool publicOnly = !Capabilities.haveAccessibility();
				// Searching for the method may, itself be a priviledged action
				try
				{
					method = findOverloadedMethod(clas, name, types, publicOnly);
				}
				catch (System.Security.SecurityException e)
				{
					throw new UtilTargetError("Security Exception while searching methods of: " + clas, e);
				}
				
				checkFoundStaticMethod(method, staticOnly, clas);
				
				// This is the first time we've seen this method, set accessibility
				// Note: even if it's a public method, we may have found it in a
				// non-public class
				if (method != null && !publicOnly)
				{
					try
					{
						ReflectManager.RMSetAccessible(method);
					}
					catch (UtilEvalError e)
					{
						/*ignore*/
					}
				}
				
				// If succeeded cache the resolved method.
				if (method != null && bcm != null)
					bcm.cacheResolvedMethod(clas, types, method);
			}
			
			return method;
		}
		
		/// <summary>Get the candidate methods by searching the class and interface graph
		/// of baseClass and resolve the most specific.
		/// </summary>
		/// <returns> the method or null for not found
		/// </returns>
		private static System.Reflection.MethodInfo findOverloadedMethod(System.Type baseClass, System.String methodName, System.Type[] types, bool publicOnly)
		{
			if (Interpreter.DEBUG)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				Interpreter.debug("Searching for method: " + StringUtil.methodString(methodName, types) + " in '" + baseClass.FullName + "'");
			}
			
			System.Reflection.MethodInfo[] methods = getCandidateMethods(baseClass, methodName, types.Length, publicOnly);
			
			if (Interpreter.DEBUG)
				Interpreter.debug("Looking for most specific method: " + methodName);
			System.Reflection.MethodInfo method = findMostSpecificMethod(types, methods);
			
			return method;
		}
		
		/// <summary>Climb the class and interface inheritence graph of the type and collect
		/// all methods matching the specified name and criterion.  If publicOnly
		/// is true then only public methods in *public* classes or interfaces will
		/// be returned.  In the normal (non-accessible) case this addresses the
		/// problem that arises when a package private class or private inner class
		/// implements a public interface or derives from a public type.
		/// <p/>
		/// This method primarily just delegates to gatherMethodsRecursive()
		/// </summary>
		/// <seealso cref="gatherMethodsRecursive(">
		/// Class, String, int, boolean, java.util.Vector)
		/// </seealso>
		internal static System.Reflection.MethodInfo[] getCandidateMethods(System.Type baseClass, System.String methodName, int numArgs, bool publicOnly)
		{
			System.Collections.ArrayList candidates = gatherMethodsRecursive(baseClass, methodName, numArgs, publicOnly, null);
			
			// return the methods in an array
			System.Reflection.MethodInfo[] ma = new System.Reflection.MethodInfo[candidates.Count];
			candidates.CopyTo(ma);
			return ma;
		}
		
		/// <summary>Accumulate all methods, optionally including non-public methods,
		/// class and interface, in the inheritence tree of baseClass.
		/// This method is analogous to Class getMethods() which returns all public
		/// methods in the inheritence tree.
		/// In the normal (non-accessible) case this also addresses the problem
		/// that arises when a package private class or private inner class
		/// implements a public interface or derives from a public type.  In other
		/// words, sometimes we'll find public methods that we can't use directly
		/// and we have to find the same public method in a parent class or
		/// interface.
		/// </summary>
		/// <returns> the candidate methods vector
		/// </returns>
		private static System.Collections.ArrayList gatherMethodsRecursive(System.Type baseClass, System.String methodName, int numArgs, bool publicOnly, System.Collections.ArrayList candidates)
		{
			if (candidates == null)
				candidates = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			
			// Add methods of the current class to the vector.
			// In public case be careful to only add methods from a public class
			// and to use getMethods() instead of getDeclaredMethods()
			// (This addresses secure environments)
			if (publicOnly)
			{
				if (isPublic(baseClass))
					addCandidates(baseClass.GetMethods(), methodName, numArgs, publicOnly, candidates);
			}
			else
				addCandidates(baseClass.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Static), methodName, numArgs, publicOnly, candidates);
			
			// Does the class or interface implement interfaces?
			System.Type[] intfs = baseClass.GetInterfaces();
			for (int i = 0; i < intfs.Length; i++)
				gatherMethodsRecursive(intfs[i], methodName, numArgs, publicOnly, candidates);
			
			// Do we have a superclass? (interfaces don't, etc.)
			System.Type superclass = baseClass.BaseType;
			if (superclass != null)
				gatherMethodsRecursive(superclass, methodName, numArgs, publicOnly, candidates);
			
			return candidates;
		}
		
		private static System.Collections.ArrayList addCandidates(System.Reflection.MethodInfo[] methods, System.String methodName, int numArgs, bool publicOnly, System.Collections.ArrayList candidates)
		{
			for (int i = 0; i < methods.Length; i++)
			{
				System.Reflection.MethodInfo m = methods[i];
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				if (m.Name.Equals(methodName) && (m.GetParameters().Length == numArgs) && (!publicOnly || isPublic(m)))
					candidates.Add(m);
			}
			return candidates;
		}
		
		/// <summary>Primary object constructor
		/// This method is simpler than those that must resolve general method
		/// invocation because constructors are not inherited.
		/// <p/>
		/// This method determines whether to attempt to use non-public constructors
		/// based on Capabilities.haveAccessibility() and will set the accessibilty
		/// flag on the method as necessary.
		/// <p/>
		/// </summary>
		internal static System.Object constructObject(System.Type clas, System.Object[] args)
		{
			if (clas.IsInterface)
				throw new ReflectError("Can't create instance of an interface: " + clas);
			
			System.Object obj = null;
			System.Type[] types = Types.getTypes(args);
			System.Reflection.ConstructorInfo con = null;
			
			// Find the constructor.
			// (there are no inherited constructors to worry about)
			System.Reflection.ConstructorInfo[] constructors = Capabilities.haveAccessibility()?clas.GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly):clas.GetConstructors();
			
			if (Interpreter.DEBUG)
				Interpreter.debug("Looking for most specific constructor: " + clas);
			con = findMostSpecificConstructor(types, constructors);
			if (con == null)
				throw cantFindConstructor(clas, types);
			
			if (!isPublic(con))
				try
				{
					ReflectManager.RMSetAccessible(con);
				}
				catch (UtilEvalError e)
				{
					/*ignore*/
				}
			
			args = Primitive.unwrap(args);
			try
			{
				obj = con.Invoke(args);
			}
			//UPGRADE_NOTE: Exception 'java.lang.InstantiationException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception e)
			{
				throw new ReflectError("The class " + clas + " is abstract ");
			}
			catch (System.UnauthorizedAccessException e)
			{
				throw new ReflectError("We don't have permission to create an instance." + "Use setAccessibility(true) to enable access.");
			}
			catch (System.ArgumentException e)
			{
				throw new ReflectError("The number of arguments was wrong");
			}
			if (obj == null)
				throw new ReflectError("Couldn't construct the object");
			
			return obj;
		}
		
		/*
		This method should parallel findMostSpecificMethod()
		The only reason it can't be combined is that Method and Constructor
		don't have a common interface for their signatures
		*/
		internal static System.Reflection.ConstructorInfo findMostSpecificConstructor(System.Type[] idealMatch, System.Reflection.ConstructorInfo[] constructors)
		{
			int match = findMostSpecificConstructorIndex(idealMatch, constructors);
			return (match == - 1)?null:constructors[match];
		}
		
		internal static int findMostSpecificConstructorIndex(System.Type[] idealMatch, System.Reflection.ConstructorInfo[] constructors)
		{
			System.Type[][] candidates = new System.Type[constructors.Length][];
			for (int i = 0; i < candidates.Length; i++)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Constructor.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				candidates[i] = constructors[i].GetParameters();
			}
			
			return findMostSpecificSignature(idealMatch, candidates);
		}
		
		/// <summary>Find the best match for signature idealMatch.
		/// It is assumed that the methods array holds only valid candidates
		/// (e.g. method name and number of args already matched).
		/// This method currently does not take into account Java 5 covariant
		/// return types... which I think will require that we find the most
		/// derived return type of otherwise identical best matches.
		/// </summary>
		/// <seealso cref="findMostSpecificSignature(Class[], Class[][])">
		/// </seealso>
		/// <param name="methods">the set of candidate method which differ only in the
		/// types of their arguments.
		/// </param>
		internal static System.Reflection.MethodInfo findMostSpecificMethod(System.Type[] idealMatch, System.Reflection.MethodInfo[] methods)
		{
			// copy signatures into array for findMostSpecificMethod()
			System.Type[][] candidateSigs = new System.Type[methods.Length][];
			for (int i = 0; i < methods.Length; i++)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				candidateSigs[i] = methods[i].GetParameters();
			}
			
			int match = findMostSpecificSignature(idealMatch, candidateSigs);
			return match == - 1?null:methods[match];
		}
		
		/// <summary>Implement JLS 15.11.2
		/// Return the index of the most specific arguments match or -1 if no
		/// match is found.
		/// This method is used by both methods and constructors (which
		/// unfortunately don't share a common interface for signature info).
		/// </summary>
		/// <returns> the index of the most specific candidate
		/// </returns>
		/*
		Note: Two methods which are equally specific should not be allowed by
		the Java compiler.  In this case BeanShell currently chooses the first
		one it finds.  We could add a test for this case here (I believe) by
		adding another isSignatureAssignable() in the other direction between
		the target and "best" match.  If the assignment works both ways then
		neither is more specific and they are ambiguous.  I'll leave this test
		out for now because I'm not sure how much another test would impact
		performance.  Method selection is now cached at a high level, so a few
		friendly extraneous tests shouldn't be a problem.
		*/
		internal static int findMostSpecificSignature(System.Type[] idealMatch, System.Type[][] candidates)
		{
			for (int round = Types.FIRST_ROUND_ASSIGNABLE; round <= Types.LAST_ROUND_ASSIGNABLE; round++)
			{
				System.Type[] bestMatch = null;
				int bestMatchIndex = - 1;
				
				for (int i = 0; i < candidates.Length; i++)
				{
					System.Type[] targetMatch = candidates[i];
					
					// If idealMatch fits targetMatch and this is the first match
					// or targetMatch is more specific than the best match, make it
					// the new best match.
					if (Types.isSignatureAssignable(idealMatch, targetMatch, round) && ((bestMatch == null) || Types.isSignatureAssignable(targetMatch, bestMatch, Types.JAVA_BASE_ASSIGNABLE)))
					{
						bestMatch = targetMatch;
						bestMatchIndex = i;
					}
				}
				
				if (bestMatch != null)
					return bestMatchIndex;
			}
			
			return - 1;
		}
		
		private static System.String accessorName(System.String getorset, System.String propName)
		{
			return getorset + System.Convert.ToString(System.Char.ToUpper(propName[0])) + propName.Substring(1);
		}
		
		public static bool hasObjectPropertyGetter(System.Type clas, System.String propName)
		{
			System.String getterName = accessorName("get", propName);
			try
			{
				clas.GetMethod(getterName, (new System.Type[0] == null)?new System.Type[0]:(System.Type[]) new System.Type[0]);
				return true;
			}
			catch (System.MethodAccessException e)
			{
				/* fall through */
			}
			getterName = accessorName("is", propName);
			try
			{
				System.Reflection.MethodInfo m = clas.GetMethod(getterName, (new System.Type[0] == null)?new System.Type[0]:(System.Type[]) new System.Type[0]);
				return (m.ReturnType == System.Type.GetType("System.Boolean"));
			}
			catch (System.MethodAccessException e)
			{
				return false;
			}
		}
		
		public static bool hasObjectPropertySetter(System.Type clas, System.String propName)
		{
			System.String setterName = accessorName("set", propName);
			System.Reflection.MethodInfo[] methods = clas.GetMethods();
			
			// we don't know the right hand side of the assignment yet.
			// has at least one setter of the right name?
			for (int i = 0; i < methods.Length; i++)
				if (methods[i].Name.Equals(setterName))
					return true;
			return false;
		}
		
		public static System.Object getObjectProperty(System.Object obj, System.String propName)
		{
			System.Object[] args = new System.Object[]{};
			
			Interpreter.debug("property access: ");
			System.Reflection.MethodInfo method = null;
			
			System.Exception e1 = null, e2 = null;
			try
			{
				System.String accessorName = accessorName("get", propName);
				method = resolveExpectedJavaMethod(null, obj.GetType(), obj, accessorName, args, false);
			}
			catch (System.Exception e)
			{
				e1 = e;
			}
			if (method == null)
				try
				{
					System.String accessorName = accessorName("is", propName);
					method = resolveExpectedJavaMethod(null, obj.GetType(), obj, accessorName, args, false);
					if (method.ReturnType != System.Type.GetType("System.Boolean"))
						method = null;
				}
				catch (System.Exception e)
				{
					e2 = e;
				}
			if (method == null)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new ReflectError("Error in property getter: " + e1 + (e2 != null?" : " + e2:""));
			}
			
			try
			{
				return invokeMethod(method, obj, args);
			}
			catch (System.Reflection.TargetInvocationException e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new UtilEvalError("Property accessor threw exception: " + e.GetBaseException());
			}
		}
		
		public static void  setObjectProperty(System.Object obj, System.String propName, System.Object value_Renamed)
		{
			System.String accessorName = accessorName("set", propName);
			System.Object[] args = new System.Object[]{value_Renamed};
			
			Interpreter.debug("property access: ");
			try
			{
				System.Reflection.MethodInfo method = resolveExpectedJavaMethod(null, obj.GetType(), obj, accessorName, args, false);
				invokeMethod(method, obj, args);
			}
			catch (System.Reflection.TargetInvocationException e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new UtilEvalError("Property accessor threw exception: " + e.GetBaseException());
			}
		}
		
		/// <summary>Return a more human readable version of the type name.
		/// Specifically, array types are returned with postfix "[]" dimensions.
		/// e.g. return "int []" for integer array instead of "class [I" as
		/// would be returned by Class getName() in that case.
		/// </summary>
		public static System.String normalizeClassName(System.Type type)
		{
			if (!type.IsArray)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return type.FullName;
			}
			
			System.Text.StringBuilder className = new System.Text.StringBuilder();
			try
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				className.Append(getArrayBaseType(type).FullName + " ");
				for (int i = 0; i < getArrayDimensions(type); i++)
					className.Append("[]");
			}
			catch (ReflectError e)
			{
				/*shouldn't happen*/
			}
			
			return className.ToString();
		}
		
		/// <summary>returns the dimensionality of the Class
		/// returns 0 if the Class is not an array class
		/// </summary>
		public static int getArrayDimensions(System.Type arrayClass)
		{
			if (!arrayClass.IsArray)
				return 0;
			
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return arrayClass.FullName.LastIndexOf('[') + 1; // why so cute?
		}
		
		/// <summary>Returns the base type of an array Class.
		/// throws ReflectError if the Class is not an array class.
		/// </summary>
		public static System.Type getArrayBaseType(System.Type arrayClass)
		{
			if (!arrayClass.IsArray)
				throw new ReflectError("The class is not an array.");
			
			return arrayClass.GetElementType();
		}
		
		/// <summary>A command may be implemented as a compiled Java class containing one or
		/// more static invoke() methods of the correct signature.  The invoke()
		/// methods must accept two additional leading arguments of the interpreter
		/// and callstack, respectively. e.g. invoke(interpreter, callstack, ... )
		/// This method adds the arguments and invokes the static method, returning
		/// the result.
		/// </summary>
		public static System.Object invokeCompiledCommand(System.Type commandClass, System.Object[] args, Interpreter interpreter, CallStack callstack)
		{
			// add interpereter and namespace to args list
			System.Object[] invokeArgs = new System.Object[args.Length + 2];
			invokeArgs[0] = interpreter;
			invokeArgs[1] = callstack;
			Array.Copy(args, 0, invokeArgs, 2, args.Length);
			BshClassManager bcm = interpreter.ClassManager;
			try
			{
				return Reflect.invokeStaticMethod(bcm, commandClass, "invoke", invokeArgs);
			}
			catch (System.Reflection.TargetInvocationException e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new UtilEvalError("Error in compiled command: " + e.GetBaseException());
			}
			catch (ReflectError e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new UtilEvalError("Error invoking compiled command: " + e);
			}
		}
		
		private static void  logInvokeMethod(System.String msg, System.Reflection.MethodInfo method, System.Object[] args)
		{
			if (Interpreter.DEBUG)
			{
				Interpreter.debug(msg + method + " with args:");
				for (int i = 0; i < args.Length; i++)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					Interpreter.debug("args[" + i + "] = " + args[i] + " type = " + args[i].GetType());
				}
			}
		}
		
		private static void  checkFoundStaticMethod(System.Reflection.MethodInfo method, bool staticOnly, System.Type clas)
		{
			// We're looking for a static method but found an instance method
			if (method != null && staticOnly && !isStatic(method))
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new UtilEvalError("Cannot reach instance method: " + StringUtil.methodString(method.Name, method.GetParameters()) + " from static context: " + clas.FullName);
			}
		}
		
		private static ReflectError cantFindConstructor(System.Type clas, System.Type[] types)
		{
			if (types.Length == 0)
				return new ReflectError("Can't find default constructor for: " + clas);
			else
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return new ReflectError("Can't find constructor: " + StringUtil.methodString(clas.FullName, types) + " in class: " + clas.FullName);
			}
		}
		
		private static bool isPublic(System.Type c)
		{
			return c.IsPublic;
		}
		private static bool isPublic(System.Reflection.MethodInfo m)
		{
			return m.IsPublic;
		}
		private static bool isPublic(System.Reflection.ConstructorInfo c)
		{
			return c.IsPublic;
		}
		private static bool isStatic(System.Reflection.MethodInfo m)
		{
			return m.IsStatic;
		}
	}
}