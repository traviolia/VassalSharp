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
using Unavailable = bsh.Capabilities.Unavailable;
namespace bsh
{
	
	/// <summary>ReflectManager is a dynamically loaded extension that supports extended
	/// reflection features supported by JDK1.2 and greater.
	/// In particular it currently supports accessible method and field access 
	/// supported by JDK1.2 and greater.
	/// </summary>
	public abstract class ReflectManager
	{
		private static ReflectManager rfm;
		
		/// <summary>Return the singleton bsh ReflectManager.</summary>
		/// <throws>  Unavailable </throws>
		public static ReflectManager getReflectManager()
		{
			if (rfm == null)
			{
				System.Type clas;
				try
				{
					//UPGRADE_TODO: The differences in the format  of parameters for method 'java.lang.Class.forName'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					clas = System.Type.GetType("bsh.reflect.ReflectManagerImpl");
					rfm = (ReflectManager) System.Activator.CreateInstance(clas);
				}
				catch (System.Exception e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new Unavailable("Reflect Manager unavailable: " + e);
				}
			}
			
			return rfm;
		}
		
		/// <summary>Reflect Manager Set Accessible.
		/// Convenience method to invoke the reflect manager.
		/// </summary>
		/// <throws>  Unavailable </throws>
		public static bool RMSetAccessible(System.Object obj)
		{
			return getReflectManager().setAccessible(obj);
		}
		
		/// <summary>Set a java.lang.reflect Field, Method, Constructor, or Array of
		/// accessible objects to accessible mode.
		/// </summary>
		/// <returns> true if the object was accessible or false if it was not.
		/// </returns>
		public abstract bool setAccessible(System.Object o);
	}
}