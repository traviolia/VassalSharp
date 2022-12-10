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
using ConsoleInterface = bsh.ConsoleInterface;
using StringUtil = bsh.StringUtil;
using ClassPathException = bsh.ClassPathException;
using NameSource = bsh.NameSource;
namespace bsh.classpath
{
	
	/// <summary>A BshClassPath encapsulates knowledge about a class path of URLs.
	/// It can maps all classes the path which may include:
	/// jar/zip files and base dirs
	/// A BshClassPath may composite other BshClassPaths as components of its
	/// path and will reflect changes in those components through its methods
	/// and listener interface.
	/// Classpath traversal is done lazily when a call is made to 
	/// getClassesForPackage() or getClassSource()
	/// or can be done explicitily through insureInitialized().
	/// Feedback on mapping progress is provided through the MappingFeedback
	/// interface.
	/// Design notes:
	/// Several times here we traverse ourselves and our component paths to
	/// produce a composite view of some thing relating to the path.  This would
	/// be an opportunity for a visitor pattern.
	/// </summary>
	public class BshClassPath : ClassPathListener, NameSource
	{
		virtual public System.Uri[] Path
		{
			// mutators
			
			
			set
			{
				reset();
				add(value);
			}
			
		}
		/// <summary>Get the path components including any component paths.</summary>
		virtual public System.Uri[] PathComponents
		{
			get
			{
				return (System.Uri[]) SupportClass.ICollectionSupport.ToArray(FullPath, new System.Uri[0]);
			}
			
		}
		/// <summary>Get the full path including component paths.
		/// (component paths listed first, in order)
		/// Duplicate path components are removed.
		/// </summary>
		virtual protected internal System.Collections.IList FullPath
		{
			get
			{
				System.Collections.IList list = new System.Collections.ArrayList();
				if (compPaths != null)
				{
					for (int i = 0; i < compPaths.Count; i++)
					{
						System.Collections.IList l = ((BshClassPath) compPaths[i]).FullPath;
						// take care to remove dups
						// wish we had an ordered set collection
						System.Collections.IEnumerator it = l.GetEnumerator();
						//UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratorhasNext'"
						while (it.MoveNext())
						{
							//UPGRADE_TODO: Method 'java.util.Iterator.next' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratornext'"
							System.Object o = it.Current;
							if (!list.Contains(o))
								list.Add(o);
						}
					}
				}
				SupportClass.ICollectionSupport.AddAll(list, path);
				return list;
			}
			
		}
		virtual public System.String[] AllNames
		{
			get
			{
				insureInitialized();
				
				System.Collections.IList names = new System.Collections.ArrayList();
				System.Collections.IEnumerator it = PackagesSet.GetEnumerator();
				//UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratorhasNext'"
				while (it.MoveNext())
				{
					//UPGRADE_TODO: Method 'java.util.Iterator.next' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratornext'"
					System.String pack = (System.String) it.Current;
					SupportClass.ICollectionSupport.AddAll(names, removeInnerClassNames(getClassesForPackage(pack)));
				}
				
				if (nameCompletionIncludesUnqNames)
				{
					//UPGRADE_TODO: Method 'java.util.HashMap.keySet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMapkeySet'"
					SupportClass.ICollectionSupport.AddAll(names, new SupportClass.HashSetSupport(getUnqualifiedNameTable().Keys));
				}
				
				return (System.String[]) SupportClass.ICollectionSupport.ToArray(names, new System.String[0]);
			}
			
		}
		public static System.Uri[] UserClassPathComponents
		{
			get
			{
				if (userClassPathComp != null)
					return userClassPathComp;
				
				//UPGRADE_ISSUE: Method 'java.lang.System.getProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
				System.String cp = System_Renamed.getProperty("java.class.path");
				System.String[] paths = StringUtil.split(cp, System.IO.Path.PathSeparator.ToString());
				
				System.Uri[] urls = new System.Uri[paths.Length];
				try
				{
					for (int i = 0; i < paths.Length; i++)
					// We take care to get the canonical path first.
					// Java deals with relative paths for it's bootstrap loader
					// but JARClassLoader doesn't.
						urls[i] = SupportClass.FileSupport.ToUri(new System.IO.FileInfo(new System.IO.FileInfo(paths[i]).FullName));
				}
				catch (System.IO.IOException e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new ClassPathException("can't parse class path: " + e);
				}
				
				userClassPathComp = urls;
				return urls;
			}
			
		}
		/// <summary>Get a list of all of the known packages</summary>
		virtual public SupportClass.SetSupport PackagesSet
		{
			get
			{
				insureInitialized();
				//UPGRADE_TODO: Class 'java.util.HashSet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashSet'"
				SupportClass.SetSupport set_Renamed = new SupportClass.HashSetSupport();
				//UPGRADE_TODO: Method 'java.util.Map.keySet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilMapkeySet'"
				set_Renamed.AddAll(new SupportClass.HashSetSupport(packageMap.Keys));
				
				if (compPaths != null)
					for (int i = 0; i < compPaths.Count; i++)
					{
						//UPGRADE_TODO: Method 'java.util.Map.keySet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilMapkeySet'"
						set_Renamed.AddAll(new SupportClass.HashSetSupport(((BshClassPath) compPaths[i]).packageMap.Keys));
					}
				return set_Renamed;
			}
			
		}
		/// <summary>A BshClassPath initialized to the user path
		/// from java.class.path
		/// </summary>
		public static BshClassPath UserClassPath
		{
			get
			{
				if (userClassPath == null)
					userClassPath = new BshClassPath("User Class Path", UserClassPathComponents);
				return userClassPath;
			}
			
		}
		/// <summary>Get the boot path including the lib/rt.jar if possible.</summary>
		public static BshClassPath BootClassPath
		{
			get
			{
				if (bootClassPath == null)
				{
					try
					{
						//String rtjar = System.getProperty("java.home")+"/lib/rt.jar";
						System.String rtjar = RTJarPath;
						System.Uri url = SupportClass.FileSupport.ToUri(new System.IO.FileInfo(rtjar));
						bootClassPath = new BshClassPath("Boot Class Path", new System.Uri[]{url});
					}
					catch (System.UriFormatException e)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						throw new ClassPathException(" can't find boot jar: " + e);
					}
				}
				return bootClassPath;
			}
			
		}
		private static System.String RTJarPath
		{
			get
			{
				//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
				System.String urlString = new System.Uri(System.IO.Path.GetFullPath("/java/lang/String.class")).ToString();
				
				if (!urlString.StartsWith("jar:file:"))
					return null;
				
				int i = urlString.IndexOf("!");
				if (i == - 1)
					return null;
				
				return urlString.Substring("jar:file:".Length, (i) - ("jar:file:".Length));
			}
			
		}
		internal System.String name;
		
		/// <summary>The URL path components </summary>
		private System.Collections.IList path;
		/// <summary>Ordered list of components BshClassPaths </summary>
		private System.Collections.IList compPaths;
		
		/// <summary>Set of classes in a package mapped by package name </summary>
		private System.Collections.IDictionary packageMap;
		/// <summary>Map of source (URL or File dir) of every clas </summary>
		private System.Collections.IDictionary classSource;
		/// <summary>The packageMap and classSource maps have been built. </summary>
		private bool mapsInitialized;
		
		private UnqualifiedNameTable unqNameTable;
		
		/// <summary>This used to be configurable, but now we always include them.</summary>
		private bool nameCompletionIncludesUnqNames = true;
		
		internal System.Collections.ArrayList listeners = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
		
		// constructors
		
		public BshClassPath(System.String name)
		{
			this.name = name;
			reset();
		}
		
		public BshClassPath(System.String name, System.Uri[] urls):this(name)
		{
			add(urls);
		}
		
		// end constructors
		
		/// <summary>Add the specified BshClassPath as a component of our path.
		/// Changes in the bcp will be reflected through us.
		/// </summary>
		public virtual void  addComponent(BshClassPath bcp)
		{
			if (compPaths == null)
				compPaths = new System.Collections.ArrayList();
			compPaths.Add(bcp);
			bcp.addListener(this);
		}
		
		public virtual void  add(System.Uri[] urls)
		{
			//UPGRADE_TODO: Method 'java.util.Arrays.asList' was converted to 'System.Collections.ArrayList' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilArraysasList_javalangObject[]'"
			SupportClass.ICollectionSupport.AddAll(path, new System.Collections.ArrayList(urls));
			if (mapsInitialized)
				map(urls);
		}
		
		public virtual void  add(System.Uri url)
		{
			path.Add(url);
			if (mapsInitialized)
				map(url);
		}
		
		/// <summary>Return the set of class names in the specified package
		/// including all component paths.
		/// </summary>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'getClassesForPackage'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual SupportClass.SetSupport getClassesForPackage(System.String pack)
		{
			lock (this)
			{
				insureInitialized();
				//UPGRADE_TODO: Class 'java.util.HashSet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashSet'"
				SupportClass.SetSupport set_Renamed = new SupportClass.HashSetSupport();
				System.Collections.ICollection c = (System.Collections.ICollection) packageMap[pack];
				if (c != null)
					set_Renamed.AddAll(c);
				
				if (compPaths != null)
					for (int i = 0; i < compPaths.Count; i++)
					{
						c = ((BshClassPath) compPaths[i]).getClassesForPackage(pack);
						if (c != null)
							set_Renamed.AddAll(c);
					}
				return set_Renamed;
			}
		}
		
		/// <summary>Return the source of the specified class which may lie in component 
		/// path.
		/// </summary>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'getClassSource'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual ClassSource getClassSource(System.String className)
		{
			lock (this)
			{
				// Before triggering classpath mapping (initialization) check for
				// explicitly set class sources (e.g. generated classes).  These would
				// take priority over any found in the classpath anyway.
				ClassSource cs = (ClassSource) classSource[className];
				if (cs != null)
					return cs;
				
				insureInitialized(); // trigger possible mapping
				
				cs = (ClassSource) classSource[className];
				if (cs == null && compPaths != null)
					for (int i = 0; i < compPaths.Count && cs == null; i++)
						cs = ((BshClassPath) compPaths[i]).getClassSource(className);
				return cs;
			}
		}
		
		/// <summary>Explicitly set a class source.  This is used for generated classes, but
		/// could potentially be used to allow a user to override which version of
		/// a class from the classpath is located.
		/// </summary>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'setClassSource'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  setClassSource(System.String className, ClassSource cs)
		{
			lock (this)
			{
				classSource[className] = cs;
			}
		}
		
		/// <summary>If the claspath map is not initialized, do it now.
		/// If component maps are not do them as well...
		/// Random note:
		/// Should this be "insure" or "ensure".  I know I've seen "ensure" used
		/// in the JDK source.  Here's what Webster has to say:
		/// Main Entry:ensure Pronunciation:in-'shur
		/// Function:transitive verb Inflected
		/// Form(s):ensured; ensuring : to make sure,
		/// certain, or safe : GUARANTEE synonyms ENSURE,
		/// INSURE, ASSURE, SECURE mean to make a thing or
		/// person sure. ENSURE, INSURE, and ASSURE are
		/// interchangeable in many contexts where they
		/// indicate the making certain or inevitable of an
		/// outcome, but INSURE sometimes stresses the
		/// taking of necessary measures beforehand, and
		/// ASSURE distinctively implies the removal of
		/// doubt and suspense from a person's mind. SECURE
		/// implies action taken to guard against attack or
		/// loss.
		/// </summary>
		public virtual void  insureInitialized()
		{
			insureInitialized(true);
		}
		
		/// <param name="topPath">indicates that this is the top level classpath
		/// component and it should send the startClassMapping message
		/// </param>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'insureInitialized'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		protected internal virtual void  insureInitialized(bool topPath)
		{
			lock (this)
			{
				// If we are the top path and haven't been initialized before
				// inform the listeners we are going to do expensive map
				if (topPath && !mapsInitialized)
					startClassMapping();
				
				// initialize components
				if (compPaths != null)
					for (int i = 0; i < compPaths.Count; i++)
						((BshClassPath) compPaths[i]).insureInitialized(false);
				
				// initialize ourself
				if (!mapsInitialized)
					map((System.Uri[]) SupportClass.ICollectionSupport.ToArray(path, new System.Uri[0]));
				
				if (topPath && !mapsInitialized)
					endClassMapping();
				
				mapsInitialized = true;
			}
		}
		
		
		/// <summary>Support for super import "*";
		/// Get the full name associated with the unqualified name in this 
		/// classpath.  Returns either the String name or an AmbiguousName object
		/// encapsulating the various names.
		/// </summary>
		public virtual System.String getClassNameByUnqName(System.String name)
		{
			insureInitialized();
			UnqualifiedNameTable unqNameTable = getUnqualifiedNameTable();
			
			//UPGRADE_TODO: Method 'java.util.HashMap.get' was converted to 'System.Collections.Hashtable.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMapget_javalangObject'"
			System.Object obj = unqNameTable[name];
			if (obj is AmbiguousName)
				throw new ClassPathException("Ambigous class names: " + SupportClass.CollectionToString(((AmbiguousName) obj).get_Renamed()));
			
			return (System.String) obj;
		}
		
		/*
		Note: we could probably do away with the unqualified name table
		in favor of a second name source
		*/
		private UnqualifiedNameTable getUnqualifiedNameTable()
		{
			if (unqNameTable == null)
				unqNameTable = buildUnqualifiedNameTable();
			return unqNameTable;
		}
		
		private UnqualifiedNameTable buildUnqualifiedNameTable()
		{
			UnqualifiedNameTable unqNameTable = new UnqualifiedNameTable();
			
			// add component names
			if (compPaths != null)
				for (int i = 0; i < compPaths.Count; i++)
				{
					//UPGRADE_TODO: Method 'java.util.Map.keySet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilMapkeySet'"
					SupportClass.SetSupport s = new SupportClass.HashSetSupport(((BshClassPath) compPaths[i]).classSource.Keys);
					System.Collections.IEnumerator it = s.GetEnumerator();
					//UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratorhasNext'"
					while (it.MoveNext())
					{
						//UPGRADE_TODO: Method 'java.util.Iterator.next' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratornext'"
						unqNameTable.add((System.String) it.Current);
					}
				}
			
			// add ours
			//UPGRADE_TODO: Method 'java.util.Map.keySet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilMapkeySet'"
			System.Collections.IEnumerator it2 = new SupportClass.HashSetSupport(classSource.Keys).GetEnumerator();
			//UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratorhasNext'"
			while (it2.MoveNext())
			{
				//UPGRADE_TODO: Method 'java.util.Iterator.next' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratornext'"
				unqNameTable.add((System.String) it2.Current);
			}
			
			return unqNameTable;
		}
		
		/// <summary>call map(url) for each url in the array</summary>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'map'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		internal virtual void  map(System.Uri[] urls)
		{
			lock (this)
			{
				for (int i = 0; i < urls.Length; i++)
					try
					{
						map(urls[i]);
					}
					catch (System.IO.IOException e)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						System.String s = "Error constructing classpath: " + urls[i] + ": " + e;
						errorWhileMapping(s);
					}
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'map'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		internal virtual void  map(System.Uri url)
		{
			lock (this)
			{
				System.String name = url.PathAndQuery;
				System.IO.FileInfo f = new System.IO.FileInfo(name);
				
				if (System.IO.Directory.Exists(f.FullName))
				{
					classMapping("Directory " + f.ToString());
					map(traverseDirForClasses(f), new DirClassSource(f));
				}
				else if (isArchiveFileName(name))
				{
					classMapping("Archive: " + url);
					map(searchJarForClasses(url), new JarClassSource(url));
				}
				/*
				else if ( isClassFileName( name ) )
				map( looseClass( name ), url );
				*/
				else
				{
					System.String s = "Not a classpath component: " + name;
					errorWhileMapping(s);
				}
			}
		}
		
		private void  map(System.String[] classes, System.Object source)
		{
			for (int i = 0; i < classes.Length; i++)
			{
				//System.out.println( classes[i] +": "+ source );
				mapClass(classes[i], source);
			}
		}
		
		private void  mapClass(System.String className, System.Object source)
		{
			// add to package map
			System.String[] sa = splitClassname(className);
			System.String pack = sa[0];
			System.String clas = sa[1];
			SupportClass.SetSupport set_Renamed = (SupportClass.SetSupport) packageMap[pack];
			if (set_Renamed == null)
			{
				//UPGRADE_TODO: Class 'java.util.HashSet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashSet'"
				set_Renamed = new SupportClass.HashSetSupport();
				packageMap[pack] = set_Renamed;
			}
			set_Renamed.Add(className);
			
			// Add to classSource map
			System.Object obj = classSource[className];
			// don't replace previously set (found earlier in classpath or
			// explicitly set via setClassSource() )
			if (obj == null)
				classSource[className] = source;
		}
		
		/// <summary>Clear everything and reset the path to empty.</summary>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'reset'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		private void  reset()
		{
			lock (this)
			{
				path = new System.Collections.ArrayList();
				compPaths = null;
				clearCachedStructures();
			}
		}
		
		/// <summary>Clear anything cached.  All will be reconstructed as necessary.</summary>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'clearCachedStructures'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		private void  clearCachedStructures()
		{
			lock (this)
			{
				mapsInitialized = false;
				//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
				packageMap = new System.Collections.Hashtable();
				//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
				classSource = new System.Collections.Hashtable();
				unqNameTable = null;
				nameSpaceChanged();
			}
		}
		
		public virtual void  classPathChanged()
		{
			clearCachedStructures();
			notifyListeners();
		}
		
		/*
		public void setNameCompletionIncludeUnqNames( boolean b ) {
		if ( nameCompletionIncludesUnqNames != b ) {
		nameCompletionIncludesUnqNames = b;
		nameSpaceChanged();
		}
		}*/
		
		// Begin Static stuff
		
		internal static System.String[] traverseDirForClasses(System.IO.FileInfo dir)
		{
			System.Collections.IList list = traverseDirForClassesAux(dir, dir);
			return (System.String[]) SupportClass.ICollectionSupport.ToArray(list, new System.String[0]);
		}
		
		internal static System.Collections.IList traverseDirForClassesAux(System.IO.FileInfo topDir, System.IO.FileInfo dir)
		{
			System.Collections.IList list = new System.Collections.ArrayList();
			System.String top = topDir.FullName;
			
			System.IO.FileInfo[] children = SupportClass.FileSupport.GetFiles(dir);
			for (int i = 0; i < children.Length; i++)
			{
				System.IO.FileInfo child = children[i];
				if (System.IO.Directory.Exists(child.FullName))
					SupportClass.ICollectionSupport.AddAll(list, traverseDirForClassesAux(topDir, child));
				else
				{
					System.String name = child.FullName;
					if (isClassFileName(name))
					{
						/* 
						Remove absolute (topdir) portion of path and leave 
						package-class part 
						*/
						if (name.StartsWith(top))
							name = name.Substring(top.Length + 1);
						else
							throw new System.IO.IOException("problem parsing paths");
						
						name = canonicalizeClassName(name);
						list.Add(name);
					}
				}
			}
			
			
			return list;
		}
		
		/// <summary>Get the class file entries from the Jar</summary>
		internal static System.String[] searchJarForClasses(System.Uri jar)
		{
			System.Collections.ArrayList v = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			System.IO.Stream in_Renamed = System.Net.WebRequest.Create(jar).GetResponse().GetResponseStream();
			//UPGRADE_ISSUE: Class 'java.util.zip.ZipInputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipInputStream'"
			//UPGRADE_ISSUE: Constructor 'java.util.zip.ZipInputStream.ZipInputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipInputStream'"
			ZipInputStream zin = new ZipInputStream(in_Renamed);
			
			//UPGRADE_ISSUE: Class 'java.util.zip.ZipEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipEntry'"
			ZipEntry ze;
			//UPGRADE_ISSUE: Method 'java.util.zip.ZipInputStream.getNextEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipInputStream'"
			while ((ze = zin.getNextEntry()) != null)
			{
				//UPGRADE_ISSUE: Method 'java.util.zip.ZipEntry.getName' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipEntry'"
				System.String name = ze.getName();
				if (isClassFileName(name))
					v.Add(canonicalizeClassName(name));
			}
			//UPGRADE_ISSUE: Method 'java.util.zip.ZipInputStream.close' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipInputStream'"
			zin.close();
			
			System.String[] sa = new System.String[v.Count];
			v.CopyTo(sa);
			return sa;
		}
		
		public static bool isClassFileName(System.String name)
		{
			return (name.ToLower().EndsWith(".class"));
			//&& (name.indexOf('$')==-1) );
		}
		
		public static bool isArchiveFileName(System.String name)
		{
			name = name.ToLower();
			return (name.EndsWith(".jar") || name.EndsWith(".zip"));
		}
		
		/// <summary>Create a proper class name from a messy thing.
		/// Turn / or \ into .,  remove leading class and trailing .class
		/// Note: this makes lots of strings... could be faster.
		/// </summary>
		public static System.String canonicalizeClassName(System.String name)
		{
			System.String classname = name.Replace('/', '.');
			classname = classname.Replace('\\', '.');
			if (classname.StartsWith("class "))
				classname = classname.Substring(6);
			if (classname.EndsWith(".class"))
				classname = classname.Substring(0, (classname.Length - 6) - (0));
			return classname;
		}
		
		/// <summary>Split class name into package and name</summary>
		public static System.String[] splitClassname(System.String classname)
		{
			classname = canonicalizeClassName(classname);
			
			int i = classname.LastIndexOf(".");
			System.String classn, packn;
			if (i == - 1)
			{
				// top level class
				classn = classname;
				packn = "<unpackaged>";
			}
			else
			{
				packn = classname.Substring(0, (i) - (0));
				classn = classname.Substring(i + 1);
			}
			return new System.String[]{packn, classn};
		}
		
		/// <summary>Return a new collection without any inner class names</summary>
		public static System.Collections.ICollection removeInnerClassNames(System.Collections.ICollection col)
		{
			System.Collections.IList list = new System.Collections.ArrayList();
			SupportClass.ICollectionSupport.AddAll(list, col);
			System.Collections.IEnumerator it = list.GetEnumerator();
			//UPGRADE_TODO: Method 'java.util.Iterator.hasNext' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratorhasNext'"
			while (it.MoveNext())
			{
				//UPGRADE_TODO: Method 'java.util.Iterator.next' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilIteratornext'"
				System.String name = (System.String) it.Current;
				if (name.IndexOf("$") != - 1)
				{
					//UPGRADE_ISSUE: Method 'java.util.Iterator.remove' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilIteratorremove'"
					it.remove();
				}
			}
			return list;
		}
		
		/// <summary>The user classpath from system property
		/// java.class.path
		/// </summary>
		
		internal static System.Uri[] userClassPathComp;
		
		public virtual void  addListener(ClassPathListener l)
		{
			listeners.Add(new System.WeakReference(l));
		}
		public virtual void  removeListener(ClassPathListener l)
		{
			listeners.Remove(l);
		}
		
		
		internal virtual void  notifyListeners()
		{
			//UPGRADE_TODO: Method 'java.util.Enumeration.hasMoreElements' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationhasMoreElements'"
			for (System.Collections.IEnumerator e = listeners.GetEnumerator(); e.MoveNext(); )
			{
				//UPGRADE_TODO: Method 'java.util.Enumeration.nextElement' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationnextElement'"
				System.WeakReference wr = (System.WeakReference) e.Current;
				//UPGRADE_ISSUE: Method 'java.lang.ref.Reference.get' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangrefReference'"
				ClassPathListener l = (ClassPathListener) wr.get_Renamed();
				if (l == null)
				// garbage collected
					listeners.Remove(wr);
				else
					l.classPathChanged();
			}
		}
		
		internal static BshClassPath userClassPath;
		
		internal static BshClassPath bootClassPath;
		
		public abstract class ClassSource
		{
			internal System.Object source;
			//UPGRADE_NOTE: Access modifiers of method 'getCode' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public abstract sbyte[] getCode(System.String className);
		}
		
		public class JarClassSource:ClassSource
		{
			virtual public System.Uri URL
			{
				get
				{
					return (System.Uri) source;
				}
				
			}
			internal JarClassSource(System.Uri url)
			{
				source = url;
			}
			/*
			Note: we should implement this for consistency, however our
			BshClassLoader can natively load from a JAR because it is a
			URLClassLoader... so it may be better to allow it to do it.
			*/
			public override sbyte[] getCode(System.String className)
			{
				throw new System.ApplicationException("Unimplemented");
			}
			public override System.String ToString()
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return "Jar: " + source;
			}
		}
		
		public class DirClassSource:ClassSource
		{
			virtual public System.IO.FileInfo Dir
			{
				get
				{
					return (System.IO.FileInfo) source;
				}
				
			}
			internal DirClassSource(System.IO.FileInfo dir)
			{
				source = dir;
			}
			public override System.String ToString()
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return "Dir: " + source;
			}
			
			public override sbyte[] getCode(System.String className)
			{
				return readBytesFromFile(Dir, className);
			}
			
			public static sbyte[] readBytesFromFile(System.IO.FileInfo base_Renamed, System.String className)
			{
				System.String n = className.Replace('.', System.IO.Path.DirectorySeparatorChar) + ".class";
				System.IO.FileInfo file = new System.IO.FileInfo(base_Renamed.FullName + "\\" + n);
				
				bool tmpBool;
				if (System.IO.File.Exists(file.FullName))
					tmpBool = true;
				else
					tmpBool = System.IO.Directory.Exists(file.FullName);
				if (file == null || !tmpBool)
					return null;
				
				sbyte[] bytes;
				try
				{
					//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
					System.IO.FileStream fis = new System.IO.FileStream(file.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
					//UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
					System.IO.BinaryReader dis = new System.IO.BinaryReader(fis);
					
					bytes = new sbyte[(int) SupportClass.FileLength(file)];
					
					SupportClass.ReadInput(dis.BaseStream, bytes, 0, bytes.Length);
					//UPGRADE_TODO: Method 'java.io.FilterInputStream.close' was converted to 'System.IO.BinaryReader.Close' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFilterInputStreamclose'"
					dis.Close();
				}
				catch (System.IO.IOException ie)
				{
					throw new System.SystemException("Couldn't load file: " + file);
				}
				
				return bytes;
			}
		}
		
		public class GeneratedClassSource:ClassSource
		{
			internal GeneratedClassSource(sbyte[] bytecode)
			{
				source = bytecode;
			}
			public override sbyte[] getCode(System.String className)
			{
				return (sbyte[]) source;
			}
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			System.Uri[] urls = new System.Uri[args.Length];
			for (int i = 0; i < args.Length; i++)
				urls[i] = SupportClass.FileSupport.ToUri(new System.IO.FileInfo(args[i]));
			BshClassPath bcp = new BshClassPath("Test", urls);
		}
		
		public override System.String ToString()
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return "BshClassPath " + name + "(" + base.ToString() + ") path= " + SupportClass.CollectionToString(path) + "\n" + "compPaths = {" + SupportClass.CollectionToString(compPaths) + " }";
		}
		
		
		/*
		Note: we could probably do away with the unqualified name table
		in favor of a second name source
		*/
		//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
		[Serializable]
		internal class UnqualifiedNameTable:System.Collections.Hashtable
		{
			internal virtual void  add(System.String fullname)
			{
				System.String name = bsh.classpath.BshClassPath.splitClassname(fullname)[1];
				//UPGRADE_TODO: Method 'java.util.HashMap.get' was converted to 'System.Collections.Hashtable.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMapget_javalangObject'"
				System.Object have = base[name];
				
				if (have == null)
					base[name] = fullname;
				else if (have is AmbiguousName)
					((AmbiguousName) have).add(fullname);
				// String
				else
				{
					AmbiguousName an = new AmbiguousName();
					an.add((System.String) have);
					an.add(fullname);
					base[name] = an;
				}
			}
		}
		
		public class AmbiguousName
		{
			internal System.Collections.IList list = new System.Collections.ArrayList();
			public virtual void  add(System.String name)
			{
				list.Add(name);
			}
			public virtual System.Collections.IList get_Renamed()
			{
				//return (String[])list.toArray(new String[0]);
				return list;
			}
		}
		
		/// <summary>Fire the NameSourceListeners</summary>
		internal virtual void  nameSpaceChanged()
		{
			if (nameSourceListeners == null)
				return ;
			
			for (int i = 0; i < nameSourceListeners.Count; i++)
				((bsh.Listener) (nameSourceListeners[i])).nameSourceChanged(this);
		}
		
		internal System.Collections.IList nameSourceListeners;
		/// <summary>Implements NameSource
		/// Add a listener who is notified upon changes to names in this space.
		/// </summary>
		public virtual void  addNameSourceListener(bsh.Listener listener)
		{
			if (nameSourceListeners == null)
				nameSourceListeners = new System.Collections.ArrayList();
			nameSourceListeners.Add(listener);
		}
		
		/// <summary>only allow one for now </summary>
		internal static BshClassPath.MappingFeedback mappingFeedbackListener;
		
		
		public static void  addMappingFeedback(BshClassPath.MappingFeedback mf)
		{
			if (mappingFeedbackListener != null)
				throw new System.SystemException("Unimplemented: already a listener");
			mappingFeedbackListener = mf;
		}
		
		internal virtual void  startClassMapping()
		{
			if (mappingFeedbackListener != null)
				mappingFeedbackListener.startClassMapping();
			else
				System.Console.Error.WriteLine("Start ClassPath Mapping");
		}
		
		internal virtual void  classMapping(System.String msg)
		{
			if (mappingFeedbackListener != null)
			{
				mappingFeedbackListener.classMapping(msg);
			}
			else
				System.Console.Error.WriteLine("Mapping: " + msg);
		}
		
		internal virtual void  errorWhileMapping(System.String s)
		{
			if (mappingFeedbackListener != null)
				mappingFeedbackListener.errorWhileMapping(s);
			else
				System.Console.Error.WriteLine(s);
		}
		
		internal virtual void  endClassMapping()
		{
			if (mappingFeedbackListener != null)
				mappingFeedbackListener.endClassMapping();
			else
				System.Console.Error.WriteLine("End ClassPath Mapping");
		}
		
		public interface MappingFeedback
		{
			void  startClassMapping();
			
			/// <summary>Provide feedback on the progress of mapping the classpath</summary>
			/// <param name="msg">is a message about the path component being mapped
			/// </param>
			/// <perc>  is an integer in the range 0-100 indicating percentage done </perc>
			/// <summary>public void classMapping( String msg, int perc );
			/// </summary>
			
			/// <summary>Provide feedback on the progress of mapping the classpath</summary>
			void  classMapping(System.String msg);
			
			void  errorWhileMapping(System.String msg);
			
			void  endClassMapping();
		}
	}
}