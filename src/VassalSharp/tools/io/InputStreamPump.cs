/*
* $Id$
*
* Copyright (c) 2008-2010 by Joel Uckelman
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
namespace VassalSharp.tools.io
{
	
	/// <summary> Pumps an {@link InputStream}.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public interface InputStreamPump:IThreadRunnable
	{
		/// <summary> Sets the input stream.
		/// 
		/// </summary>
		/// <param name="in">the input stream
		/// </param>
		/// <throws>  UnsupportedOperationException if called after the pump is started </throws>
		System.IO.Stream InputStream
		{
			set;
			
		}
	}
}