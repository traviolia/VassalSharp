/*
* $Id$
*
* Copyright (c) 2008 Joel Uckelman
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
	
	/// <summary> An adapter for converting {@link Iterator}s into {@link Enumeration}s.
	/// Consider altering the code requiring the <code>Enumeration</code> instead
	/// of using this class. <code>Enumeration</code>s generally should not be
	/// used in new code.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	/// <seealso cref="IterableEnumeration">
	/// </seealso>
	public class EnumeratedIterator
	{
		public EnumeratedIterator()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			this.i = i;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< T > implements Enumeration < T >
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private final Iterator < T > i;
		
		/// <summary> Creates an <code>EnumeratedIterator</code> from the given
		/// <code>Iterator</code>.
		/// 
		/// </summary>
		/// <param name="i">the <code>Iterator</code> on which this will iterate
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public EnumeratedIterator(Iterator < T > i)
		
		/// <summary>{@inheritDoc} </summary>
		public virtual bool hasMoreElements()
		{
			return i.hasNext();
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual T nextElement()
		{
			return i.next();
		}
	}
}