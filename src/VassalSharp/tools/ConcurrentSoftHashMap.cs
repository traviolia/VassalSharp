/*
* $Id$
*
* Copyright (c) 2007 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.util.concurrent.ConcurrentMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ConcurrentMap = java.util.concurrent.ConcurrentMap;
namespace VassalSharp.tools
{
	
	/// <summary> A memory-sensitive {@link ConcurrentMap} which stores the values
	/// in {@link SoftReference}s. This <code>ConcurrentMap</code> grows
	/// without bound, but when the JVM is under memory pressure, values
	/// held by it may be garbage collected.
	/// 
	/// <p>All methods except {@link #get} cause the <code>Map</code> to
	/// be cleared of key-value pairs for which the value has been garbage
	/// collected. Processing key-value pairs with dead values is <em>not</em>
	/// an atomic operation. Thus, it is possible, though unlikely, that more
	/// values will be garbage collected between the removal of dead key-value
	/// pairs and the return of the method in which this takes place.</p>
	/// 
	/// <p>This implementation does not permit <code>null</code> keys or
	/// values.</p>
	/// 
	/// </summary>
	/// <deprecated> Moved to {@link VassalSharp.tools.concurrent} package.
	/// </deprecated>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class ConcurrentSoftHashMap
	{
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< K, V > 
		extends VassalSharp.tools.concurrent.ConcurrentSoftHashMap < K, V >
	}
}