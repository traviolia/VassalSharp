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
namespace VassalSharp.tools.concurrent
{
	
	/// <summary> A progress handler.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.11
	/// </since>
	/// <deprecated> Moved to {@link VassalSharp.tools.swing.Progressor}.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public abstract class Progressor:VassalSharp.tools.swing.Progressor
	{
		/// <summary> Creates a <code>Progressor</code> with the given bounds.
		/// 
		/// </summary>
		/// <param name="init">the initial progress value
		/// </param>
		/// <param name="max">the maximum progress value
		/// 
		/// </param>
		/// <throws>  IllegalArgumentException if {@code init} is not in {@code [0,max]} </throws>
		/// <throws>  IllegalArgumentException if {@code max &lt; 0} </throws>
		public Progressor(int init, int max):base(init, max)
		{
		}
	}
}