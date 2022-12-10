/*
* $Id$
*
* Copyright (c) 2007 Joel Uckelman
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
namespace VassalSharp.tools
{
	
	/// <summary> An adapter for converting {@link Enumeration}s into {@link Iterable}s.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	/// <seealso cref="IterableIterator">
	/// </seealso>
	public sealed class IterableEnumeration
	{
		public IterableEnumeration()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			this.e = e;
			return this;
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new IterableEnumeration < T >(e);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< T > implements Iterable < T >, Iterator < T >
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private final Enumeration < T > e;
		
		/// <summary> Creates an <code>IterableEnumeration</code> from the given
		/// <code>Enumeration</code>.
		/// 
		/// </summary>
		/// <param name="e">the <code>Enumeration</code> on which this will iterate
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public IterableEnumeration(Enumeration < T > e)
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < T > iterator()
		
		/// <summary>{@inheritDoc} </summary>
		public bool hasNext()
		{
			return e.hasMoreElements();
		}
		
		/// <summary>{@inheritDoc} </summary>
		public T next()
		{
			return e.nextElement();
		}
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <throws>  UnsupportedOperationException becuase <code>Enumeration</code> </throws>
		/// <summary> do not permit removal of elements.
		/// </summary>
		public void  remove()
		{
			throw new System.NotSupportedException();
		}
		
		/// <summary> A static convenience method for making an <code>Enumeration<code>
		/// into an <code>Iterable</code>.
		/// 
		/// </summary>
		/// <param name="e">the <code>Enumeration</code>
		/// </param>
		/// <returns> an <code>Iterable</code> wrapping <code>e</code>
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T > Iterable < T > iterate(Enumeration < T > e)
	}
}