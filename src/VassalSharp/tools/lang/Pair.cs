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
namespace VassalSharp.tools.lang
{
	
	/// <summary> An immutable class for pairs of objects.
	/// 
	/// </summary>
	/// <param name="<A>">the type of the first object
	/// </param>
	/// <param name="<B>">the type of the second object
	/// 
	/// </param>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public sealed class Pair
	{
		private void  InitBlock()
		{
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Pair < A, B >(first, second);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< A, B >
		//UPGRADE_NOTE: Final was removed from the declaration of 'first '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public A first;
		//UPGRADE_NOTE: Final was removed from the declaration of 'second '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public B second;
		
		/// <summary> Creates a pair with the given values.
		/// 
		/// </summary>
		/// <param name="first">the first value
		/// </param>
		/// <param name="second">the second value
		/// </param>
		public Pair(A first, B second)
		{
			InitBlock();
			this.first = first;
			this.second = second;
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override int GetHashCode()
		{
			return (first == null?0:first.hashCode() * 31) + (second == null?0:second.hashCode());
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public  override bool Equals(System.Object o)
		{
			if (this == o)
				return true;
			if (o == null || o.GetType() != this.GetType())
				return false;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Pair < ?, ? > p =(Pair < ?, ? >) o;
			return (first == p.first || (first != null && first.equals(p.first))) && (second == p.second || (second != null && second.equals(p.second)));
		}
		
		/// <summary> A convenience method for creating pairs without specifying the types.
		/// 
		/// </summary>
		/// <param name="first">the first value
		/// </param>
		/// <param name="second">the second value
		/// </param>
		/// <returns> the pair containing {@code first} and (@code second}
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < A, B > Pair < A, B > of(A first, B second)
	}
}