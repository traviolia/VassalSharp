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
	
	/// <summary>The default CollectionManager (which remains Java 1.1 compatible) 
	/// supports iteration over objects of type:
	/// Enumeration, Vector, String, StringBuffer and array.
	/// The dynamically loaded CollectionManagerImpl supports additional types when
	/// it is present.
	/// </summary>
	/// <seealso cref="BshIterable.java">
	/// </seealso>
	public class CollectionManager
	{
		private static CollectionManager manager;
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'getCollectionManager'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public static CollectionManager getCollectionManager()
		{
			lock (typeof(bsh.CollectionManager))
			{
				if (manager == null && Capabilities.classExists("java.util.Collection"))
				{
					System.Type clas;
					try
					{
						//UPGRADE_TODO: The differences in the format  of parameters for method 'java.lang.Class.forName'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
						clas = System.Type.GetType("bsh.collection.CollectionManagerImpl");
						manager = (CollectionManager) System.Activator.CreateInstance(clas);
					}
					catch (System.Exception e)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						Interpreter.debug("unable to load CollectionManagerImpl: " + e);
					}
				}
				
				if (manager == null)
					manager = new CollectionManager(); // default impl
				
				return manager;
			}
		}
		
		
		public virtual bool isBshIterable(System.Object obj)
		{
			// This could be smarter...
			try
			{
				getBshIterator(obj);
				return true;
			}
			catch (System.ArgumentException e)
			{
				return false;
			}
		}
		
		public virtual BshIterator getBshIterator(System.Object obj)
		{
			return new BasicBshIterator(obj);
		}
		
		public virtual bool isMap(System.Object obj)
		{
			return obj is System.Collections.Hashtable;
		}
		
		public virtual System.Object getFromMap(System.Object map, System.Object key)
		{
			return ((System.Collections.Hashtable) map)[key];
		}
		
		public virtual System.Object putInMap(System.Object map, System.Object key, System.Object value_Renamed)
		{
			System.Object tempObject;
			tempObject = ((System.Collections.Hashtable) map)[key];
			((System.Collections.Hashtable) map)[key] = value_Renamed;
			return tempObject;
		}
		
		/// <summary>Determine dynamically if the target is an iterator by the presence of a
		/// pair of next() and hasNext() methods.
		/// public static boolean isIterator() { }
		/// </summary>
		
		/// <summary> An implementation that works with JDK 1.1</summary>
		public class BasicBshIterator : BshIterator
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassEnumeration' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassEnumeration : System.Collections.IEnumerator
			{
				public AnonymousClassEnumeration(System.Object array, BasicBshIterator enclosingInstance)
				{
					InitBlock(array, enclosingInstance);
				}
				private void  InitBlock(System.Object array, BasicBshIterator enclosingInstance)
				{
					this.array = array;
					this.enclosingInstance = enclosingInstance;
					length = ((System.Array) array).Length;
				}
				private System.Object tempAuxObj;
				public virtual bool MoveNext()
				{
					bool result = hasMoreElements();
					if (result)
					{
						tempAuxObj = nextElement();
					}
					return result;
				}
				public virtual void  Reset()
				{
					tempAuxObj = null;
				}
				public virtual System.Object Current
				{
					get
					{
						return tempAuxObj;
					}
					
				}
				//UPGRADE_NOTE: Final variable array was copied into class AnonymousClassEnumeration. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Object array;
				private BasicBshIterator enclosingInstance;
				public BasicBshIterator Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				//UPGRADE_NOTE: The initialization of  'length' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
				internal int index = 0, length;
				//UPGRADE_NOTE: The equivalent of method 'java.util.Enumeration.nextElement' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public virtual System.Object nextElement()
				{
					return ((System.Array) array).GetValue(index++);
				}
				//UPGRADE_NOTE: The equivalent of method 'java.util.Enumeration.hasMoreElements' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public virtual bool hasMoreElements()
				{
					return index < length;
				}
			}
			internal System.Collections.IEnumerator enumeration;
			
			/// <summary> Construct a basic BasicBshIterator
			/// 
			/// </summary>
			/// <param name="The">object over which we are iterating
			/// 
			/// </param>
			/// <throws>  java.lang.IllegalArgumentException If the argument is not a </throws>
			/// <summary> supported (i.e. iterable) type.
			/// 
			/// </summary>
			/// <throws>  java.lang.NullPointerException If the argument is null </throws>
			public BasicBshIterator(System.Object iterateOverMe)
			{
				enumeration = createEnumeration(iterateOverMe);
			}
			
			/// <summary> Create an enumeration over the given object
			/// 
			/// </summary>
			/// <param name="iterateOverMe">Object of type Enumeration, Vector, String, 
			/// StringBuffer or an array
			/// 
			/// </param>
			/// <returns> an enumeration
			/// 
			/// </returns>
			/// <throws>  java.lang.IllegalArgumentException If the argument is not a </throws>
			/// <summary> supported (i.e. iterable) type.
			/// 
			/// </summary>
			/// <throws>  java.lang.NullPointerException If the argument is null </throws>
			protected internal virtual System.Collections.IEnumerator createEnumeration(System.Object iterateOverMe)
			{
				if (iterateOverMe == null)
					throw new System.NullReferenceException("Object arguments passed to " + "the BasicBshIterator constructor cannot be null.");
				
				if (iterateOverMe is System.Collections.IEnumerator)
					return (System.Collections.IEnumerator) iterateOverMe;
				
				if (iterateOverMe is System.Collections.ArrayList)
					return ((System.Collections.ArrayList) iterateOverMe).GetEnumerator();
				
				if (iterateOverMe.GetType().IsArray)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'array '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Object array = iterateOverMe;
					return new AnonymousClassEnumeration(array, this);
				}
				
				if (iterateOverMe is System.String)
					return createEnumeration(((System.String) iterateOverMe).ToCharArray());
				
				if (iterateOverMe is System.Text.StringBuilder)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					return createEnumeration(iterateOverMe.ToString().ToCharArray());
				}
				
				throw new System.ArgumentException("Cannot enumerate object of type " + iterateOverMe.GetType());
			}
			
			/// <summary> Fetch the next object in the iteration
			/// 
			/// </summary>
			/// <returns> The next object
			/// </returns>
			public virtual System.Object next()
			{
				//UPGRADE_TODO: Method 'java.util.Enumeration.nextElement' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationnextElement'"
				return enumeration.Current;
			}
			
			/// <summary> Returns true if and only if there are more objects available
			/// via the <code>next()</code> method
			/// 
			/// </summary>
			/// <returns> The next object
			/// </returns>
			public virtual bool hasNext()
			{
				//UPGRADE_TODO: Method 'java.util.Enumeration.hasMoreElements' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationhasMoreElements'"
				return enumeration.MoveNext();
			}
		}
	}
}