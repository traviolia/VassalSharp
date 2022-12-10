/*
* $Id$
*
* Copyright (c) 2010 by Joel Uckelman
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
	
	/// <summary> Holds a mutable reference.
	/// 
	/// This class is useful in cases where a reference to an object must be
	/// passed to a method without the caller retaining a reference to that
	/// object itself.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class Reference
	{
		private void  InitBlock()
		{
			// FIXME: Replace this class with Mutable<T> from Apache Commons lang
			// once we move from 2.5 to 3.0.
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< T >
		
		public T obj;
		
		/// <summary> Creates a <code>Reference</code>.
		/// 
		/// </summary>
		/// <param name="obj">the referenced object
		/// </param>
		public Reference(T obj)
		{
			InitBlock();
			this.obj = obj;
		}
	}
}