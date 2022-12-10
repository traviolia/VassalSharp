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
using BshClassManager = bsh.BshClassManager;
using ClassSource = bsh.classpath.BshClassPath.ClassSource;
using DirClassSource = bsh.classpath.BshClassPath.DirClassSource;
using GeneratedClassSource = bsh.classpath.BshClassPath.GeneratedClassSource;
namespace bsh.classpath
{
	
	/// <summary>A classloader which can load one or more classes from specified sources.
	/// Because the classes are loaded via a single classloader they change as a
	/// group and any versioning cross dependencies can be managed.
	/// </summary>
	public class DiscreteFilesClassLoader:BshClassLoader
	{
		/// <summary>Map of class sources which also implies our coverage space.</summary>
		internal ClassSourceMap map;
		
		//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
		[Serializable]
		public class ClassSourceMap:System.Collections.Hashtable
		{
			public void  put(System.String name, ClassSource source)
			{
				base[name] = source;
			}
			public ClassSource get_Renamed(System.String name)
			{
				//UPGRADE_TODO: Method 'java.util.HashMap.get' was converted to 'System.Collections.Hashtable.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMapget_javalangObject'"
				return (ClassSource) base[name];
			}
		}
		
		public DiscreteFilesClassLoader(BshClassManager classManager, ClassSourceMap map):base(classManager)
		{
			this.map = map;
		}
		
		
		public override System.Type findClass(System.String name)
		{
			// Load it if it's one of our classes
			ClassSource source = map.get_Renamed(name);
			
			if (source != null)
			{
				sbyte[] code = source.getCode(name);
				//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.defineClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
				return defineClass(name, code, 0, code.Length);
			}
			// Let superclass BshClassLoader (URLClassLoader) findClass try 
			// to find the class...
			else
				return base.findClass(name);
		}
		
		public override System.String ToString()
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return base.ToString() + "for files: " + SupportClass.CollectionToString(map);
		}
	}
}