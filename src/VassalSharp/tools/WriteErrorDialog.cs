/*
* $Id$
*
* Copyright (c) 2008 by Joel Uckelman
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
	
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class WriteErrorDialog
	{
		private WriteErrorDialog()
		{
		}
		
		/// <param name="t">the <code>Throwable</code> providing the stack trace
		/// </param>
		/// <param name="e">the write exception
		/// </param>
		/// <param name="filename">the file which was being written
		/// </param>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public static void  error(System.Exception t, System.IO.IOException e, System.String filename)
		{
			ErrorDialog.showDetails(t, ThrowableUtils.getStackTrace(e), "Error.file_write_error", filename);
		}
		
		/// <param name="t">the <code>Throwable</code> providing the stack trace
		/// </param>
		/// <param name="e">the write exception
		/// </param>
		/// <param name="file">the file which was being written
		/// </param>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public static void  error(System.Exception t, System.IO.IOException e, System.IO.FileInfo file)
		{
			error(t, e, file.FullName);
		}
		
		/// <param name="e">the write exception
		/// </param>
		/// <param name="filename">the file which was being written
		/// </param>
		public static void  error(System.IO.IOException e, System.String filename)
		{
			error(e, e, filename);
		}
		
		/// <param name="e">the write exception
		/// </param>
		/// <param name="file">the file which was being written
		/// </param>
		public static void  error(System.IO.IOException e, System.IO.FileInfo file)
		{
			error(e, e, file.FullName);
		}
	}
}