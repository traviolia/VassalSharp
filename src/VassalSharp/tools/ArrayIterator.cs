/*
* $Id$
*
* Copyright (c) 2009 by Joel Uckelman
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
	
	/// <summary> An {@link Iterator} for arrays.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public class ArrayIterator
	{
		public ArrayIterator()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			this.array = array;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< T > implements Iterator < T >
		
		private T[] array;
		private int pos = 0;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public ArrayIterator(T...array)
		
		/// <summary>{@inheritDoc} </summary>
		public virtual bool hasNext()
		{
			return pos < array.length;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual T next()
		{
			if (!hasNext())
				throw new System.ArgumentOutOfRangeException();
			return array[pos++];
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  remove()
		{
			throw new System.NotSupportedException();
		}
	}
}