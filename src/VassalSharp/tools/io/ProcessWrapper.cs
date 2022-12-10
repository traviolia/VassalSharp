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
	
	/// <summary> A wrapper for returning processes from a {@link ProcessLauncher}.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public class ProcessWrapper
	{
		public ProcessWrapper()
		{
		}

		public System.IO.Stream stdout;
		public System.IO.Stream stderr;
		public System.IO.Stream stdin;
		
		/// <summary> Create a <code>ProcessWrapper</code>.
		/// 
		/// </summary>
		/// <param name="future">the future for the process
		/// </param>
		/// <param name="stdout">the process' STDOUT
		/// </param>
		/// <param name="stderr">the process' STDERR
		/// </param>
		/// <param name="stdin"> the process' STDIN
		/// </param>
		ProcessWrapper(System.IO.Stream stdout, System.IO.Stream stderr, System.IO.Stream stdin)
		{
			this.stdout = stdout;
			this.stderr = stderr;
			this.stdin = stdin;
		}
	}
}