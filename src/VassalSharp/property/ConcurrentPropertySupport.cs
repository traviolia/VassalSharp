/*
* $Id$
*
* Copyright (c) 2009-2010 by Joel Uckelman
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
//UPGRADE_TODO: The type 'java.util.concurrent.ConcurrentHashMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ConcurrentHashMap = java.util.concurrent.ConcurrentHashMap;
//UPGRADE_TODO: The type 'java.util.concurrent.ConcurrentMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ConcurrentMap = java.util.concurrent.ConcurrentMap;
//UPGRADE_TODO: The type 'java.util.concurrent.CopyOnWriteArrayList' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using CopyOnWriteArrayList = java.util.concurrent.CopyOnWriteArrayList;
namespace VassalSharp.property
{
	
	/// <summary> A thread-safe implementation of {@link PropertySupport}.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class ConcurrentPropertySupport : PropertySupport
	{
		public ConcurrentPropertySupport()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			if (l == null)
				throw new System.ArgumentException();
			listeners.add(l);
			if (l == null)
				throw new System.ArgumentException();
			listeners.remove(l);
			return listeners.isEmpty();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			? 
			Collections.< PropertyListener < Object >> emptyList(): 
			new ArrayList < PropertyListener < Object >>(listeners);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < PropertyListener < ? >> empty = 
			new CopyOnWriteArrayList < PropertyListener < ? >>();
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			List < PropertyListener < ? >> list = plisteners.putIfAbsent(prop, empty);
			if (list == null)
				list = empty;
			
			list.add(l);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < PropertyListener < ? >> list = plisteners.get(prop);
			if (list != null)
				list.remove(l);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < PropertyListener < ? >> list = plisteners.get(prop);
			return list == null || list.isEmpty();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			? 
			Collections.< PropertyListener < ? super T >> emptyList(): 
			new ArrayList < PropertyListener < ? super T >>((List) list);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < PropertyListener < ? >> list = plisteners.get(prop);
			return list != null && !list.isEmpty();
			// do nothing if oldVal and newVal are the same
			if (oldVal == newVal || (oldVal != null && oldVal.equals(newVal)))
				return ;
			
			// notify all general listeners
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(PropertyListener < Object > l: listeners)
			{
				l.propertyChanged(src, prop, oldVal, newVal);
			}
			
			// notify all listeners on this property
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < PropertyListener < ? >> list = plisteners.get(prop);
			if (list != null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(PropertyListener < ? > l: list)
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					((PropertyListener < ? super T >) l).propertyChanged(
					src, prop, oldVal, newVal
					);
				}
			}
		}
		
		// FIXME: consider using WeakReferences for listeners to prevent memory leaks
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected final CopyOnWriteArrayList < PropertyListener < Object >> listeners = 
		new CopyOnWriteArrayList < PropertyListener < Object >>();
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <throws>  IllegalArgumentException if {@code l} is {@code null} </throws>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void addPropertyListener(PropertyListener < Object > l)
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <throws>  IllegalArgumentException if {@code l} is {@code null} </throws>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void removePropertyListener(PropertyListener < Object > l)
		
		/// <summary>{@inheritDoc} </summary>
		public virtual bool hasListeners()
		{
			return !listeners.isEmpty();
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < PropertyListener < Object >> getPropertyListeners()
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected final ConcurrentMap < Property < ? >, List < PropertyListener < ? >>> 
		plisteners = new ConcurrentHashMap < Property < ? >, List < PropertyListener < ? >>>();
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public < T >
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void addPropertyListener(Property < T > prop, 
		PropertyListener < ? super T > l)
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public < T >
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void removePropertyListener(Property < T > prop, 
		PropertyListener < ? super T > l)
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public < T > List < PropertyListener < ? super T >> 
		getPropertyListeners(Property < T > prop)
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public < T > boolean hasListeners(Property < T > prop)
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public < T >
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void fireChanged(Object src, Property < T > prop, 
		T oldVal, T newVal)
	}
}