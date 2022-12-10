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
namespace bsh.classpath
{
	
	/// <summary>One of the things BshClassLoader does is to address a deficiency in
	/// URLClassLoader that prevents us from specifying individual classes
	/// via URLs.
	/// </summary>
	//UPGRADE_ISSUE: Class 'java.net.URLClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLClassLoader'"
	public class BshClassLoader:URLClassLoader
	{
		virtual internal BshClassManager ClassManager
		{
			/*
			The superclass does something like this
			
			c = findLoadedClass(name);
			if null
			try
			if parent not null
			c = parent.loadClass(name, false);
			else
			c = findBootstrapClass(name);
			catch ClassNotFoundException 
			c = findClass(name);
			*/
			
			
			get
			{
				return classManager;
			}
			
		}
		internal BshClassManager classManager;
		
		/// <param name="bases">URLs JARClassLoader seems to require absolute paths 
		/// </param>
		//UPGRADE_ISSUE: Constructor 'java.net.URLClassLoader.URLClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLClassLoader'"
		public BshClassLoader(BshClassManager classManager, System.Uri[] bases):base(bases)
		{
			this.classManager = classManager;
		}
		
		/// <param name="bases">URLs JARClassLoader seems to require absolute paths 
		/// </param>
		public BshClassLoader(BshClassManager classManager, BshClassPath bcp):this(classManager, bcp.PathComponents)
		{
		}
		
		/// <summary>For use by children</summary>
		/// <param name="bases">URLs JARClassLoader seems to require absolute paths 
		/// </param>
		protected internal BshClassLoader(BshClassManager classManager):this(classManager, new System.Uri[]{})
		{
		}
		
		// public version of addURL
		public void  addURL(System.Uri url)
		{
			//UPGRADE_ISSUE: Method 'java.net.URLClassLoader.addURL' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLClassLoader'"
			base.addURL(url);
		}
		
		/// <summary>This modification allows us to reload classes which are in the 
		/// Java VM user classpath.  We search first rather than delegate to
		/// the parent classloader (or bootstrap path) first.
		/// An exception is for BeanShell core classes which are always loaded from
		/// the same classloader as the interpreter.
		/// </summary>
		public System.Type loadClass(System.String name, bool resolve)
		{
			System.Type c = null;
			
			/*
			Check first for classes loaded through this loader.
			The VM will not allow a class to be loaded twice.
			*/
			//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.findLoadedClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
			c = findLoadedClass(name);
			if (c != null)
				return c;
			
			// This is copied from ClassManagerImpl
			// We should refactor this somehow if it sticks around
			if (name.StartsWith(ClassManagerImpl.BSH_PACKAGE))
				try
				{
					//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.loadClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
					//UPGRADE_ISSUE: Method 'java.lang.Class.getClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetClassLoader'"
					return typeof(bsh.Interpreter).getClassLoader().loadClass(name);
				}
				//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception e)
				{
				}
			
			/*
			Try to find the class using our classloading mechanism.
			Note: I wish we didn't have to catch the exception here... slow
			*/
			try
			{
				c = findClass(name);
			}
			//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception e)
			{
			}
			
			if (c == null)
			{
				//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				throw new System.Exception("here in loaClass");
			}
			
			if (resolve)
			{
				//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.resolveClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
				resolveClass(c);
			}
			
			return c;
		}
		
		/// <summary>Find the correct source for the class...
		/// Try designated loader if any
		/// Try our URLClassLoader paths if any
		/// Try base loader if any
		/// Try system ???
		/// </summary>
		// add some caching for not found classes?
		protected internal System.Type findClass(System.String name)
		{
			// Deal with this cast somehow... maybe have this class use 
			// ClassManagerImpl type directly.
			// Don't add the method to BshClassManager... it's really an impl thing
			ClassManagerImpl bcm = (ClassManagerImpl) ClassManager;
			
			// Should we try to load the class ourselves or delegate?
			// look for overlay loader
			
			// Deal with this cast somehow... maybe have this class use 
			// ClassManagerImpl type directly.
			// Don't add the method to BshClassManager... it's really an impl thing
			//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
			ClassLoader cl = bcm.getLoaderForClass(name);
			
			System.Type c;
			
			// If there is a designated loader and it's not us delegate to it
			if (cl != null && cl != this)
				try
				{
					//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.loadClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
					return cl.loadClass(name);
				}
				//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception e)
				{
					//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new System.Exception("Designated loader could not find class: " + e);
				}
			
			// Let URLClassLoader try any paths it may have
			//UPGRADE_ISSUE: Method 'java.net.URLClassLoader.getURLs' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLClassLoader'"
			if (getURLs().Length > 0)
				try
				{
					//UPGRADE_ISSUE: Method 'java.net.URLClassLoader.findClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLClassLoader'"
					return base.findClass(name);
				}
				//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception e)
				{
					//System.out.println(
					//	"base loader here caught class not found: "+name );
				}
			
			
			// If there is a baseLoader and it's not us delegate to it
			cl = bcm.BaseLoader;
			
			if (cl != null && cl != this)
				try
				{
					//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.loadClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
					return cl.loadClass(name);
				}
				//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception e)
				{
				}
			
			// Try system loader
			return bcm.plainClassForName(name);
		}
	}
}