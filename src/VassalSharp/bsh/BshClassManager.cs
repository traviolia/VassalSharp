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
	
	/// <summary>BshClassManager manages all classloading in BeanShell.
	/// It also supports a dynamically loaded extension (bsh.classpath package)
	/// which allows classpath extension and class file reloading.
	/// Currently the extension relies on 1.2 for BshClassLoader and weak 
	/// references.  
	/// See http://www.beanshell.org/manual/classloading.html for details
	/// on the bsh classloader architecture.
	/// <p>
	/// Bsh has a multi-tiered class loading architecture.  No class loader is
	/// used unless/until the classpath is modified or a class is reloaded.
	/// <p>
	/// </summary>
	/*
	Implementation notes:
	
	Note: we may need some synchronization in here
	
	Note on version dependency:  This base class is JDK 1.1 compatible,
	however we are forced to use weak references in the full featured
	implementation (the optional bsh.classpath package) to accomodate all of
	the fleeting namespace listeners as they fall out of scope.  (NameSpaces
	must be informed if the class space changes so that they can un-cache
	names).  
	<p>
	
	Perhaps a simpler idea would be to have entities that reference cached
	types always perform a light weight check with a counter / reference
	value and use that to detect changes in the namespace.  This puts the 
	burden on the consumer to check at appropriate times, but could eliminate
	the need for the listener system in many places and the necessity of weak 
	references in this package.
	<p>*/
	public class BshClassManager
	{
		/// <summary>Set an external class loader.  BeanShell will use this at the same 
		/// point it would otherwise use the plain Class.forName().
		/// i.e. if no explicit classpath management is done from the script
		/// (addClassPath(), setClassPath(), reloadClasses()) then BeanShell will
		/// only use the supplied classloader.  If additional classpath management
		/// is done then BeanShell will perform that in addition to the supplied
		/// external classloader.
		/// However BeanShell is not currently able to reload
		/// classes supplied through the external classloader.
		/// </summary>
		//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
		virtual public ClassLoader ClassLoader
		{
			set
			{
				externalClassLoader = value;
				classLoaderChanged();
			}
			
		}
		/// <summary>Identifier for no value item.  Use a hashtable as a Set. </summary>
		private static System.Object NOVALUE = new System.Object();
		/// <summary>The interpreter which created the class manager 
		/// This is used to load scripted classes from source files.
		/// </summary>
		private Interpreter declaringInterpreter;
		
		/// <summary>An external classloader supplied by the setClassLoader() command.</summary>
		//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
		protected internal ClassLoader externalClassLoader;
		
		/// <summary>Global cache for things we know are classes.
		/// Note: these should probably be re-implemented with Soft references.
		/// (as opposed to strong or Weak)
		/// </summary>
		[NonSerialized]
		protected internal System.Collections.Hashtable absoluteClassCache = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
		/// <summary>Global cache for things we know are *not* classes.
		/// Note: these should probably be re-implemented with Soft references.
		/// (as opposed to strong or Weak)
		/// </summary>
		[NonSerialized]
		protected internal System.Collections.Hashtable absoluteNonClasses = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
		
		/// <summary>Caches for resolved object and static methods.
		/// We keep these maps separate to support fast lookup in the general case
		/// where the method may be either.
		/// </summary>
		[NonSerialized]
		protected internal System.Collections.Hashtable resolvedObjectMethods = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
		[NonSerialized]
		protected internal System.Collections.Hashtable resolvedStaticMethods = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
		
		[NonSerialized]
		protected internal System.Collections.Hashtable definingClasses = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
		[NonSerialized]
		protected internal System.Collections.Hashtable definingClassesBaseNames = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
		
		/// <summary>Create a new instance of the class manager.  
		/// Class manager instnaces are now associated with the interpreter.
		/// </summary>
		/// <seealso cref="bsh.Interpreter.getClassManager()">
		/// </seealso>
		/// <seealso cref="bsh.Interpreter.setClassLoader( ClassLoader )">
		/// </seealso>
		public static BshClassManager createClassManager(Interpreter interpreter)
		{
			BshClassManager manager;
			
			// Do we have the necessary jdk1.2 packages and optional package?
			if (Capabilities.classExists("java.lang.ref.WeakReference") && Capabilities.classExists("java.util.HashMap") && Capabilities.classExists("bsh.classpath.ClassManagerImpl"))
				try
				{
					// Try to load the module
					// don't refer to it directly here or we're dependent upon it
					//UPGRADE_TODO: The differences in the format  of parameters for method 'java.lang.Class.forName'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					System.Type clas = System.Type.GetType("bsh.classpath.ClassManagerImpl");
					manager = (BshClassManager) System.Activator.CreateInstance(clas);
				}
				catch (System.Exception e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new InterpreterError("Error loading classmanager: " + e);
				}
			else
				manager = new BshClassManager();
			
			if (interpreter == null)
				interpreter = new Interpreter();
			manager.declaringInterpreter = interpreter;
			return manager;
		}
		
		public virtual bool classExists(System.String name)
		{
			return (classForName(name) != null);
		}
		
		/// <summary>Load the specified class by name, taking into account added classpath
		/// and reloaded classes, etc.
		/// Note: Again, this is just a trivial implementation. 
		/// See bsh.classpath.ClassManagerImpl for the fully functional class
		/// management package.
		/// </summary>
		/// <returns> the class or null
		/// </returns>
		public virtual System.Type classForName(System.String name)
		{
			if (isClassBeingDefined(name))
				throw new InterpreterError("Attempting to load class in the process of being defined: " + name);
			
			System.Type clas = null;
			try
			{
				clas = plainClassForName(name);
			}
			//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception e)
			{
				/*ignore*/
			}
			
			// try scripted class
			if (clas == null)
				clas = loadSourceClass(name);
			
			return clas;
		}
		
		// Move me to classpath/ClassManagerImpl???
		protected internal virtual System.Type loadSourceClass(System.String name)
		{
			System.String fileName = "/" + name.Replace('.', '/') + ".java";
			System.IO.Stream in_Renamed = getResourceAsStream(fileName);
			if (in_Renamed == null)
				return null;
			
			try
			{
				System.Console.Out.WriteLine("Loading class from source file: " + fileName);
				declaringInterpreter.eval(new System.IO.StreamReader(in_Renamed, System.Text.Encoding.Default));
			}
			catch (EvalError e)
			{
				// ignore
				//UPGRADE_TODO: Method 'java.io.PrintStream.println' was converted to 'System.Console.Error.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintStreamprintln_javalangObject'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Error.WriteLine(e);
			}
			try
			{
				return plainClassForName(name);
			}
			//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception e)
			{
				System.Console.Error.WriteLine("Class not found in source file: " + name);
				return null;
			}
		}
		
		/// <summary>Perform a plain Class.forName() or call the externally provided
		/// classloader.
		/// If a BshClassManager implementation is loaded the call will be 
		/// delegated to it, to allow for additional hooks.
		/// <p/>
		/// This simply wraps that bottom level class lookup call and provides a 
		/// central point for monitoring and handling certain Java version 
		/// dependent bugs, etc.
		/// </summary>
		/// <seealso cref="classForName( String )">
		/// </seealso>
		/// <returns> the class
		/// </returns>
		public virtual System.Type plainClassForName(System.String name)
		{
			System.Type c = null;
			
			try
			{
				if (externalClassLoader != null)
				{
					//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.loadClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
					c = externalClassLoader.loadClass(name);
				}
				else
				{
					//UPGRADE_TODO: The differences in the format  of parameters for method 'java.lang.Class.forName'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					c = System.Type.GetType(name);
				}
				
				cacheClassInfo(name, c);
				
				/*
				Original note: Jdk under Win is throwing these to
				warn about lower case / upper case possible mismatch.
				e.g. bsh.console bsh.Console
				
				Update: Prior to 1.3 we were squeltching NoClassDefFoundErrors 
				which was very annoying.  I cannot reproduce the original problem 
				and this was never a valid solution.  If there are legacy VMs that
				have problems we can include a more specific test for them here.
				*/
			}
			catch (System.ApplicationException e)
			{
				throw noClassDefFound(name, e);
			}
			
			return c;
		}
		
		/// <summary>Get a resource URL using the BeanShell classpath</summary>
		/// <param name="path">should be an absolute path
		/// </param>
		public virtual System.Uri getResource(System.String path)
		{
			System.Uri url = null;
			if (externalClassLoader != null)
			{
				// classloader wants no leading slash
				//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.getResource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
				url = externalClassLoader.getResource(path.Substring(1));
			}
			if (url == null)
			{
				//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
				url = new System.Uri(System.IO.Path.GetFullPath(path));
			}
			
			return url;
		}
		/// <summary>Get a resource stream using the BeanShell classpath</summary>
		/// <param name="path">should be an absolute path
		/// </param>
		public virtual System.IO.Stream getResourceAsStream(System.String path)
		{
			System.IO.Stream in_Renamed = null;
			if (externalClassLoader != null)
			{
				// classloader wants no leading slash
				//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
				in_Renamed = externalClassLoader.getResourceAsStream(path.Substring(1));
			}
			if (in_Renamed == null)
			{
				//UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
				in_Renamed = typeof(Interpreter).getResourceAsStream(path);
			}
			
			return in_Renamed;
		}
		
		/// <summary>Cache info about whether name is a class or not.</summary>
		/// <param name="value">
		/// if value is non-null, cache the class
		/// if value is null, set the flag that it is *not* a class to
		/// speed later resolution
		/// </param>
		public virtual void  cacheClassInfo(System.String name, System.Type value_Renamed)
		{
			if (value_Renamed != null)
				absoluteClassCache[name] = value_Renamed;
			else
				absoluteNonClasses[name] = NOVALUE;
		}
		
		/// <summary>Cache a resolved (possibly overloaded) method based on the 
		/// argument types used to invoke it, subject to classloader change.
		/// Static and Object methods are cached separately to support fast lookup
		/// in the general case where either will do.
		/// </summary>
		public virtual void  cacheResolvedMethod(System.Type clas, System.Type[] types, System.Reflection.MethodInfo method)
		{
			if (Interpreter.DEBUG)
				Interpreter.debug("cacheResolvedMethod putting: " + clas + " " + method);
			
			SignatureKey sk = new SignatureKey(clas, method.Name, types);
			if (method.IsStatic)
				resolvedStaticMethods[sk] = method;
			else
				resolvedObjectMethods[sk] = method;
		}
		
		/// <summary>Return a previously cached resolved method.</summary>
		/// <param name="onlyStatic">specifies that only a static method may be returned.
		/// </param>
		/// <returns> the Method or null
		/// </returns>
		protected internal virtual System.Reflection.MethodInfo getResolvedMethod(System.Type clas, System.String methodName, System.Type[] types, bool onlyStatic)
		{
			SignatureKey sk = new SignatureKey(clas, methodName, types);
			
			// Try static and then object, if allowed
			// Note that the Java compiler should not allow both.
			System.Reflection.MethodInfo method = (System.Reflection.MethodInfo) resolvedStaticMethods[sk];
			if (method == null && !onlyStatic)
				method = (System.Reflection.MethodInfo) resolvedObjectMethods[sk];
			
			if (Interpreter.DEBUG)
			{
				if (method == null)
					Interpreter.debug("getResolvedMethod cache MISS: " + clas + " - " + methodName);
				else
					Interpreter.debug("getResolvedMethod cache HIT: " + clas + " - " + method);
			}
			return method;
		}
		
		/// <summary>Clear the caches in BshClassManager</summary>
		/// <seealso cref="public void .reset() for external usage">
		/// </seealso>
		protected internal virtual void  clearCaches()
		{
			absoluteNonClasses = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
			absoluteClassCache = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
			resolvedObjectMethods = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
			resolvedStaticMethods = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
		}
		
		public virtual void  addClassPath(System.Uri path)
		{
		}
		
		/// <summary>Clear all loaders and start over.  No class loading.</summary>
		public virtual void  reset()
		{
			clearCaches();
		}
		
		/// <summary>Set a new base classpath and create a new base classloader.
		/// This means all types change. 
		/// </summary>
		public virtual void  setClassPath(System.Uri[] cp)
		{
			throw cmUnavailable();
		}
		
		/// <summary>Overlay the entire path with a new class loader.
		/// Set the base path to the user path + base path.
		/// No point in including the boot class path (can't reload thos).
		/// </summary>
		public virtual void  reloadAllClasses()
		{
			throw cmUnavailable();
		}
		
		/// <summary>Reloading classes means creating a new classloader and using it
		/// whenever we are asked for classes in the appropriate space.
		/// For this we use a DiscreteFilesClassLoader
		/// </summary>
		public virtual void  reloadClasses(System.String[] classNames)
		{
			throw cmUnavailable();
		}
		
		/// <summary>Reload all classes in the specified package: e.g. "com.sun.tools"
		/// The special package name "<unpackaged>" can be used to refer 
		/// to unpackaged classes.
		/// </summary>
		public virtual void  reloadPackage(System.String pack)
		{
			throw cmUnavailable();
		}
		
		/// <summary>This has been removed from the interface to shield the core from the
		/// rest of the classpath package. If you need the classpath you will have
		/// to cast the classmanager to its impl.
		/// public BshClassPath getClassPath() throws ClassPathException;
		/// </summary>
		
		/// <summary>Support for "import *;"
		/// Hide details in here as opposed to NameSpace.
		/// </summary>
		//UPGRADE_NOTE: Access modifiers of method 'doSuperImport' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public virtual void  doSuperImport()
		{
			throw cmUnavailable();
		}
		
		/// <summary>A "super import" ("import *") operation has been performed.</summary>
		protected internal virtual bool hasSuperImport()
		{
			return false;
		}
		
		/// <summary>Return the name or null if none is found,
		/// Throw an ClassPathException containing detail if name is ambigous.
		/// </summary>
		//UPGRADE_NOTE: Access modifiers of method 'getClassNameByUnqName' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public virtual System.String getClassNameByUnqName(System.String name)
		{
			throw cmUnavailable();
		}
		
		public virtual void  addListener(BshClassManager.Listener l)
		{
		}
		
		public virtual void  removeListener(BshClassManager.Listener l)
		{
		}
		
		public virtual void  dump(System.IO.StreamWriter pw)
		{
			//UPGRADE_TODO: Method 'java.io.PrintWriter.println' was converted to 'System.IO.TextWriter.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintWriterprintln_javalangString'"
			pw.WriteLine("BshClassManager: no class manager.");
		}
		
		/// <summary>Flag the class name as being in the process of being defined.
		/// The class manager will not attempt to load it.
		/// </summary>
		/*
		Note: this implementation is temporary. We currently keep a flat
		namespace of the base name of classes.  i.e. BeanShell cannot be in the
		process of defining two classes in different packages with the same
		base name.  To remove this limitation requires that we work through
		namespace imports in an analogous (or using the same path) as regular
		class import resolution.  This workaround should handle most cases 
		so we'll try it for now.
		*/
		protected internal virtual void  definingClass(System.String className)
		{
			System.String baseName = Name.suffix(className, 1);
			int i = baseName.IndexOf("$");
			if (i != - 1)
				baseName = baseName.Substring(i + 1);
			System.String cur = (System.String) definingClassesBaseNames[baseName];
			if (cur != null)
				throw new InterpreterError("Defining class problem: " + className + ": BeanShell cannot yet simultaneously define two or more " + "dependant classes of the same name.  Attempt to define: " + className + " while defining: " + cur);
			definingClasses[className] = NOVALUE;
			definingClassesBaseNames[baseName] = className;
		}
		
		protected internal virtual bool isClassBeingDefined(System.String className)
		{
			return definingClasses[className] != null;
		}
		
		/// <summary>This method is a temporary workaround used with definingClass.
		/// It is to be removed at some point.
		/// </summary>
		protected internal virtual System.String getClassBeingDefined(System.String className)
		{
			System.String baseName = Name.suffix(className, 1);
			return (System.String) definingClassesBaseNames[baseName];
		}
		
		/// <summary>Indicate that the specified class name has been defined and may be
		/// loaded normally.
		/// </summary>
		protected internal virtual void  doneDefiningClass(System.String className)
		{
			System.String baseName = Name.suffix(className, 1);
			definingClasses.Remove(className);
			definingClassesBaseNames.Remove(baseName);
		}
		
		/*
		The real implementation in the classpath.ClassManagerImpl handles
		reloading of the generated classes.
		*/
		public virtual System.Type defineClass(System.String name, sbyte[] code)
		{
			throw new InterpreterError("Can't create class (" + name + ") without class manager package.");
			/*
			Old implementation injected classes into the parent classloader.
			This was incorrect behavior for several reasons.  The biggest problem
			is that classes could therefore only be defined once across all
			executions of the script...  
			
			ClassLoader cl = this.getClass().getClassLoader();
			Class clas;
			try {
			clas = (Class)Reflect.invokeObjectMethod( 
			cl, "defineClass", 
			new Object [] { 
			name, code, 
			new Primitive( (int)0 )/offset/, 
			new Primitive( code.length )/len/ 
			}, 
			(Interpreter)null, (CallStack)null, (SimpleNode)null 
			);
			} catch ( Exception e ) {
			e.printStackTrace();
			throw new InterpreterError("Unable to define class: "+ e );
			}
			absoluteNonClasses.remove( name ); // may have been axed previously
			return clas;
			*/
		}
		
		protected internal virtual void  classLoaderChanged()
		{
		}
		
		/// <summary>Annotate the NoClassDefFoundError with some info about the class
		/// we were trying to load.
		/// </summary>
		protected internal static System.ApplicationException noClassDefFound(System.String className, System.ApplicationException e)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return new System.ApplicationException("A class required by class: " + className + " could not be loaded:\n" + e.ToString());
		}
		
		protected internal static UtilEvalError cmUnavailable()
		{
			return new Capabilities.Unavailable("ClassLoading features unavailable.");
		}
		
		public interface Listener
		{
			void  classLoaderChanged();
		}
		
		/// <summary>SignatureKey serves as a hash of a method signature on a class 
		/// for fast lookup of overloaded and general resolved Java methods. 
		/// <p>
		/// </summary>
		/*
		Note: is using SignatureKey in this way dangerous?  In the pathological
		case a user could eat up memory caching every possible combination of
		argument types to an untyped method.  Maybe we could be smarter about
		it by ignoring the types of untyped parameter positions?  The method
		resolver could return a set of "hints" for the signature key caching?
		
		There is also the overhead of creating one of these for every method
		dispatched.  What is the alternative?
		*/
		internal class SignatureKey
		{
			internal System.Type clas;
			internal System.Type[] types;
			internal System.String methodName;
			internal int hashCode_Renamed_Field = 0;
			
			internal SignatureKey(System.Type clas, System.String methodName, System.Type[] types)
			{
				this.clas = clas;
				this.methodName = methodName;
				this.types = types;
			}
			
			public override int GetHashCode()
			{
				if (hashCode_Renamed_Field == 0)
				{
					hashCode_Renamed_Field = clas.GetHashCode() * methodName.GetHashCode();
					if (types == null)
					// no args method
						return hashCode_Renamed_Field;
					for (int i = 0; i < types.Length; i++)
					{
						int hc = types[i] == null?21:types[i].GetHashCode();
						hashCode_Renamed_Field = hashCode_Renamed_Field * (i + 1) + hc;
					}
				}
				return hashCode_Renamed_Field;
			}
			
			public  override bool Equals(System.Object o)
			{
				SignatureKey target = (SignatureKey) o;
				if (types == null)
					return target.types == null;
				if (clas != target.clas)
					return false;
				if (!methodName.Equals(target.methodName))
					return false;
				if (types.Length != target.types.Length)
					return false;
				for (int i = 0; i < types.Length; i++)
				{
					if (types[i] == null)
					{
						if (!(target.types[i] == null))
							return false;
					}
					else if (!types[i].Equals(target.types[i]))
						return false;
				}
				
				return true;
			}
		}
	}
}