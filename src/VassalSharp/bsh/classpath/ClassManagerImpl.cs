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
using ClassSource = bsh.classpath.BshClassPath.ClassSource;
using JarClassSource = bsh.classpath.BshClassPath.JarClassSource;
using GeneratedClassSource = bsh.classpath.BshClassPath.GeneratedClassSource;
using BshClassManager = bsh.BshClassManager;
using ClassPathException = bsh.ClassPathException;
using Interpreter = bsh.Interpreter;
// for debug()
using UtilEvalError = bsh.UtilEvalError;
namespace bsh.classpath
{
	
	/// <summary><pre>
	/// Manage all classloading in BeanShell.
	/// Allows classpath extension and class file reloading.
	/// This class holds the implementation of the BshClassManager so that it
	/// can be separated from the core package.
	/// This class currently relies on 1.2 for BshClassLoader and weak references.
	/// Is there a workaround for weak refs?  If so we could make this work
	/// with 1.1 by supplying our own classloader code...
	/// See "http://www.beanshell.org/manual/classloading.html" for details
	/// on the bsh classloader architecture.
	/// Bsh has a multi-tiered class loading architecture.  No class loader is
	/// created unless/until a class is generated, the classpath is modified, 
	/// or a class is reloaded.
	/// Note: we may need some synchronization in here
	/// Note on jdk1.2 dependency:
	/// We are forced to use weak references here to accomodate all of the 
	/// fleeting namespace listeners.  (NameSpaces must be informed if the class 
	/// space changes so that they can un-cache names).  I had the interesting 
	/// thought that a way around this would be to implement BeanShell's own 
	/// garbage collector...  Then I came to my senses and said - screw it, 
	/// class re-loading will require 1.2.
	/// ---------------------
	/// Classloading precedence:
	/// in-script evaluated class (scripted class)
	/// in-script added / modified classpath
	/// optionally, external classloader
	/// optionally, thread context classloader
	/// plain Class.forName()
	/// source class (.java file in classpath)
	/// </pre>
	/// </summary>
	public class ClassManagerImpl:BshClassManager
	{
		//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
		virtual public ClassLoader BaseLoader
		{
			get
			{
				return baseLoader;
			}
			
		}
		internal const System.String BSH_PACKAGE = "bsh";
		/// <summary>The classpath of the base loader.  Initially and upon reset() this is
		/// an empty instance of BshClassPath.  It grows as paths are added or is
		/// reset when the classpath is explicitly set.  This could also be called
		/// the "extension" class path, but is not strictly confined to added path
		/// (could be set arbitrarily by setClassPath())
		/// </summary>
		private BshClassPath baseClassPath;
		private bool superImport;
		
		/// <summary>This is the full blown classpath including baseClassPath (extensions),
		/// user path, and java bootstrap path (rt.jar)
		/// This is lazily constructed and further (and more importantly) lazily
		/// intialized in components because mapping the full path could be
		/// expensive.
		/// The full class path is a composite of:
		/// baseClassPath (user extension) : userClassPath : bootClassPath
		/// in that order.
		/// </summary>
		private BshClassPath fullClassPath;
		
		// ClassPath Change listeners
		private System.Collections.ArrayList listeners = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
		//UPGRADE_ISSUE: Class 'java.lang.ref.ReferenceQueue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangrefReferenceQueue'"
		//UPGRADE_ISSUE: Constructor 'java.lang.ref.ReferenceQueue.ReferenceQueue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangrefReferenceQueue'"
		private ReferenceQueue refQueue = new ReferenceQueue();
		
		/// <summary>This handles extension / modification of the base classpath
		/// The loader to use where no mapping of reloaded classes exists.
		/// The baseLoader is initially null meaning no class loader is used.
		/// </summary>
		private BshClassLoader baseLoader;
		
		/// <summary>Map by classname of loaders to use for reloaded classes</summary>
		private System.Collections.IDictionary loaderMap;
		
		/// <summary>Used by BshClassManager singleton constructor</summary>
		public ClassManagerImpl()
		{
			reset();
		}
		
		/// <returns> the class or null
		/// </returns>
		public override System.Type classForName(System.String name)
		{
			// check positive cache
			System.Type c = (System.Type) absoluteClassCache[name];
			if (c != null)
				return c;
			
			// check negative cache
			if (absoluteNonClasses[name] != null)
			{
				if (Interpreter.DEBUG)
					Interpreter.debug("absoluteNonClass list hit: " + name);
				return null;
			}
			
			if (Interpreter.DEBUG)
				Interpreter.debug("Trying to load class: " + name);
			
			// Check explicitly mapped (reloaded) class...
			//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
			ClassLoader overlayLoader = getLoaderForClass(name);
			if (overlayLoader != null)
			{
				try
				{
					//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.loadClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
					c = overlayLoader.loadClass(name);
				}
				catch (System.Exception e)
				{
					// used to squeltch this... changed for 1.3
					// see BshClassManager
				}
				catch (System.ApplicationException e2)
				{
					throw noClassDefFound(name, e2);
				}
				
				// Should be there since it was explicitly mapped
				// throw an error?
			}
			
			// insure that core classes are loaded from the same loader
			if (c == null)
			{
				if (name.StartsWith(BSH_PACKAGE))
					try
					{
						//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.loadClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
						//UPGRADE_ISSUE: Method 'java.lang.Class.getClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetClassLoader'"
						c = typeof(Interpreter).getClassLoader().loadClass(name);
					}
					//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
					catch (System.Exception e)
					{
					}
			}
			
			// Check classpath extension / reloaded classes
			if (c == null)
			{
				if (baseLoader != null)
					try
					{
						//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.loadClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
						c = baseLoader.loadClass(name);
					}
					//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
					catch (System.Exception e)
					{
					}
			}
			
			// Optionally try external classloader
			if (c == null)
			{
				if (externalClassLoader != null)
					try
					{
						//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.loadClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
						c = externalClassLoader.loadClass(name);
					}
					//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
					catch (System.Exception e)
					{
					}
			}
			
			// Optionally try context classloader
			// Note that this might be a security violation
			// is catching the SecurityException sufficient for all environments?
			// or do we need a way to turn this off completely?
			if (c == null)
			{
				try
				{
					//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
					//UPGRADE_ISSUE: Method 'java.lang.Thread.getContextClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangThreadgetContextClassLoader'"
					ClassLoader contextClassLoader = SupportClass.ThreadClass.Current().getContextClassLoader();
					if (contextClassLoader != null)
					{
						//UPGRADE_TODO: Method 'java.lang.Class.forName' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
						c = Class.forName(name, true, contextClassLoader);
					}
				}
				//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception e)
				{
					// fall through
				}
				catch (System.Security.SecurityException e)
				{
				} // fall through
			}
			
			// try plain class forName()
			if (c == null)
				try
				{
					c = plainClassForName(name);
				}
				//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception e)
				{
				}
			
			// Try scripted class
			if (c == null)
				c = loadSourceClass(name);
			
			// Cache result (or null for not found)
			// Note: plainClassForName already caches, so it will be redundant
			// in that case, however this process only happens once
			cacheClassInfo(name, c);
			
			return c;
		}
		
		/// <summary>Get a resource URL using the BeanShell classpath</summary>
		/// <param name="path">should be an absolute path
		/// </param>
		public override System.Uri getResource(System.String path)
		{
			System.Uri url = null;
			if (baseLoader != null)
			// classloader wants no leading slash
			{
				//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.getResource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
				url = baseLoader.getResource(path.Substring(1));
			}
			if (url == null)
				url = base.getResource(path);
			return url;
		}
		
		/// <summary>Get a resource stream using the BeanShell classpath</summary>
		/// <param name="path">should be an absolute path
		/// </param>
		public override System.IO.Stream getResourceAsStream(System.String path)
		{
			System.IO.Stream in_Renamed = null;
			if (baseLoader != null)
			{
				// classloader wants no leading slash
				//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
				in_Renamed = baseLoader.getResourceAsStream(path.Substring(1));
			}
			if (in_Renamed == null)
			{
				in_Renamed = base.getResourceAsStream(path);
			}
			return in_Renamed;
		}
		
		//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
		internal virtual ClassLoader getLoaderForClass(System.String name)
		{
			//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
			return (ClassLoader) loaderMap[name];
		}
		
		// Classpath mutators
		
		
		public override void  addClassPath(System.Uri path)
		{
			if (baseLoader == null)
				setClassPath(new System.Uri[]{path});
			else
			{
				// opportunity here for listener in classpath
				baseLoader.addURL(path);
				baseClassPath.add(path);
				classLoaderChanged();
			}
		}
		
		/// <summary>Clear all classloading behavior and class caches and reset to 
		/// initial state.
		/// </summary>
		public override void  reset()
		{
			baseClassPath = new BshClassPath("baseClassPath");
			baseLoader = null;
			//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
			loaderMap = new System.Collections.Hashtable();
			classLoaderChanged(); // calls clearCaches() for us.
		}
		
		/// <summary>Set a new base classpath and create a new base classloader.
		/// This means all types change. 
		/// </summary>
		public override void  setClassPath(System.Uri[] cp)
		{
			baseClassPath.Path = cp;
			initBaseLoader();
			//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
			loaderMap = new System.Collections.Hashtable();
			classLoaderChanged();
		}
		
		/// <summary>Overlay the entire path with a new class loader.
		/// Set the base path to the user path + base path.
		/// No point in including the boot class path (can't reload thos).
		/// </summary>
		public override void  reloadAllClasses()
		{
			BshClassPath bcp = new BshClassPath("temp");
			bcp.addComponent(baseClassPath);
			bcp.addComponent(BshClassPath.UserClassPath);
			setClassPath(bcp.PathComponents);
		}
		
		/// <summary>init the baseLoader from the baseClassPath</summary>
		private void  initBaseLoader()
		{
			baseLoader = new BshClassLoader(this, baseClassPath);
		}
		
		// class reloading
		
		/// <summary>Reloading classes means creating a new classloader and using it
		/// whenever we are asked for classes in the appropriate space.
		/// For this we use a DiscreteFilesClassLoader
		/// </summary>
		public override void  reloadClasses(System.String[] classNames)
		{
			// validate that it is a class here?
			
			// init base class loader if there is none...
			if (baseLoader == null)
				initBaseLoader();
			
			DiscreteFilesClassLoader.ClassSourceMap map = new DiscreteFilesClassLoader.ClassSourceMap();
			
			for (int i = 0; i < classNames.Length; i++)
			{
				System.String name = classNames[i];
				
				// look in baseLoader class path 
				ClassSource classSource = baseClassPath.getClassSource(name);
				
				// look in user class path 
				if (classSource == null)
				{
					BshClassPath.UserClassPath.insureInitialized();
					classSource = BshClassPath.UserClassPath.getClassSource(name);
				}
				
				// No point in checking boot class path, can't reload those.
				// else we could have used fullClassPath above.
				
				if (classSource == null)
					throw new ClassPathException("Nothing known about class: " + name);
				
				// JarClassSource is not working... just need to implement it's
				// getCode() method or, if we decide to, allow the BshClassManager
				// to handle it... since it is a URLClassLoader and can handle JARs
				if (classSource is JarClassSource)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new ClassPathException("Cannot reload class: " + name + " from source: " + classSource);
				}
				
				map.put(name, classSource);
			}
			
			// Create classloader for the set of classes
			//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
			ClassLoader cl = new DiscreteFilesClassLoader(this, map);
			
			// map those classes the loader in the overlay map
			//UPGRADE_TODO: Method 'java.util.HashMap.keySet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMapkeySet'"
			System.Collections.IEnumerator it = new SupportClass.HashSetSupport(map.Keys).GetEnumerator();
			//UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratorhasNext'"
			while (it.MoveNext())
			{
				//UPGRADE_TODO: Method 'java.util.Iterator.next' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratornext'"
				loaderMap[(System.String) it.Current] = cl;
			}
			
			classLoaderChanged();
		}
		
		/// <summary>Reload all classes in the specified package: e.g. "com.sun.tools"
		/// The special package name "<unpackaged>" can be used to refer 
		/// to unpackaged classes.
		/// </summary>
		public override void  reloadPackage(System.String pack)
		{
			System.Collections.ICollection classes = baseClassPath.getClassesForPackage(pack);
			
			if (classes == null)
				classes = BshClassPath.UserClassPath.getClassesForPackage(pack);
			
			// no point in checking boot class path, can't reload those
			
			if (classes == null)
				throw new ClassPathException("No classes found for package: " + pack);
			
			reloadClasses((System.String[]) SupportClass.ICollectionSupport.ToArray(classes, new System.String[0]));
		}
		
		/// <summary>Unimplemented
		/// For this we'd have to store a map by location as well as name...
		/// public void reloadPathComponent( URL pc ) throws ClassPathException {
		/// throw new ClassPathException("Unimplemented!");
		/// }
		/// </summary>
		
		// end reloading
		
		/// <summary>Get the full blown classpath.</summary>
		public virtual BshClassPath getClassPath()
		{
			if (fullClassPath != null)
				return fullClassPath;
			
			fullClassPath = new BshClassPath("BeanShell Full Class Path");
			fullClassPath.addComponent(BshClassPath.UserClassPath);
			try
			{
				fullClassPath.addComponent(BshClassPath.BootClassPath);
			}
			catch (ClassPathException e)
			{
				System.Console.Error.WriteLine("Warning: can't get boot class path");
			}
			fullClassPath.addComponent(baseClassPath);
			
			return fullClassPath;
		}
		
		/// <summary>Support for "import *;"
		/// Hide details in here as opposed to NameSpace.
		/// </summary>
		public override void  doSuperImport()
		{
			// Should we prevent it from happening twice?
			
			try
			{
				getClassPath().insureInitialized();
				// prime the lookup table
				getClassNameByUnqName("");
				
				// always true now
				//getClassPath().setNameCompletionIncludeUnqNames(true);
			}
			catch (ClassPathException e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new UtilEvalError("Error importing classpath " + e);
			}
			
			superImport = true;
		}
		
		protected internal override bool hasSuperImport()
		{
			return superImport;
		}
		
		/// <summary>Return the name or null if none is found,
		/// Throw an ClassPathException containing detail if name is ambigous.
		/// </summary>
		public override System.String getClassNameByUnqName(System.String name)
		{
			return getClassPath().getClassNameByUnqName(name);
		}
		
		public override void  addListener(BshClassManager.Listener l)
		{
			//UPGRADE_ISSUE: Constructor 'java.lang.ref.WeakReference.WeakReference' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangrefWeakReferenceWeakReference_javalangObject_javalangrefReferenceQueue'"
			listeners.Add(new WeakReference(l, refQueue));
			
			// clean up old listeners
			//UPGRADE_ISSUE: Class 'java.lang.ref.Reference' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangrefReference'"
			Reference deadref;
			//UPGRADE_ISSUE: Method 'java.lang.ref.ReferenceQueue.poll' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangrefReferenceQueue'"
			while ((deadref = refQueue.poll()) != null)
			{
				System.Boolean tempBoolean;
				tempBoolean = listeners.Contains(deadref);
				listeners.Remove(deadref);
				bool ok = tempBoolean;
				if (ok)
				{
					//System.err.println("cleaned up weak ref: "+deadref);
				}
				else
				{
					if (Interpreter.DEBUG)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						Interpreter.debug("tried to remove non-existent weak ref: " + deadref);
					}
				}
			}
		}
		
		public override void  removeListener(BshClassManager.Listener l)
		{
			throw new System.ApplicationException("unimplemented");
		}
		
		/// <summary>Get the BeanShell classloader.
		/// public ClassLoader getClassLoader() {
		/// }
		/// </summary>
		
		/*
		Impl Notes:
		We add the bytecode source and the "reload" the class, which causes the
		BshClassLoader to be initialized and create a DiscreteFilesClassLoader
		for the bytecode.
		
		@exception ClassPathException can be thrown by reloadClasses
		*/
		public override System.Type defineClass(System.String name, sbyte[] code)
		{
			baseClassPath.setClassSource(name, new GeneratedClassSource(code));
			try
			{
				reloadClasses(new System.String[]{name});
			}
			catch (ClassPathException e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new bsh.InterpreterError("defineClass: " + e);
			}
			return classForName(name);
		}
		
		/// <summary>Clear global class cache and notify namespaces to clear their 
		/// class caches.
		/// The listener list is implemented with weak references so that we 
		/// will not keep every namespace in existence forever.
		/// </summary>
		protected internal override void  classLoaderChanged()
		{
			// clear the static caches in BshClassManager
			clearCaches();
			
			System.Collections.ArrayList toRemove = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10)); // safely remove
			//UPGRADE_TODO: Method 'java.util.Enumeration.hasMoreElements' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationhasMoreElements'"
			for (System.Collections.IEnumerator e = listeners.GetEnumerator(); e.MoveNext(); )
			{
				//UPGRADE_TODO: Method 'java.util.Enumeration.nextElement' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationnextElement'"
				System.WeakReference wr = (System.WeakReference) e.Current;
				//UPGRADE_ISSUE: Method 'java.lang.ref.Reference.get' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangrefReference'"
				BshClassManager.Listener l = (BshClassManager.Listener) wr.get_Renamed();
				if (l == null)
				// garbage collected
					toRemove.Add(wr);
				else
					l.classLoaderChanged();
			}
			//UPGRADE_TODO: Method 'java.util.Enumeration.hasMoreElements' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationhasMoreElements'"
			for (System.Collections.IEnumerator e = toRemove.GetEnumerator(); e.MoveNext(); )
			{
				//UPGRADE_TODO: Method 'java.util.Enumeration.nextElement' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationnextElement'"
				listeners.Remove(e.Current);
			}
		}
		
		public override void  dump(System.IO.StreamWriter i)
		{
			//UPGRADE_TODO: Method 'java.io.PrintWriter.println' was converted to 'System.IO.TextWriter.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintWriterprintln_javalangString'"
			i.WriteLine("Bsh Class Manager Dump: ");
			//UPGRADE_TODO: Method 'java.io.PrintWriter.println' was converted to 'System.IO.TextWriter.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintWriterprintln_javalangString'"
			i.WriteLine("----------------------- ");
			//UPGRADE_TODO: Method 'java.io.PrintWriter.println' was converted to 'System.IO.TextWriter.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintWriterprintln_javalangString'"
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			i.WriteLine("baseLoader = " + baseLoader);
			//UPGRADE_TODO: Method 'java.io.PrintWriter.println' was converted to 'System.IO.TextWriter.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintWriterprintln_javalangString'"
			i.WriteLine("loaderMap= " + SupportClass.CollectionToString(loaderMap));
			//UPGRADE_TODO: Method 'java.io.PrintWriter.println' was converted to 'System.IO.TextWriter.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintWriterprintln_javalangString'"
			i.WriteLine("----------------------- ");
			//UPGRADE_TODO: Method 'java.io.PrintWriter.println' was converted to 'System.IO.TextWriter.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintWriterprintln_javalangString'"
			i.WriteLine("baseClassPath = " + baseClassPath);
		}
	}
}