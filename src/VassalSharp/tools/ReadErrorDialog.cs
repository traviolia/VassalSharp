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
	
	/// <summary> Utility class for reporting an IOException reading from the local system or a resource bundled with the VASSAL engine</summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class ReadErrorDialog
	{
		private ReadErrorDialog()
		{
		}
		
		/// <param name="t">the <code>Throwable</code> providing the stack trace
		/// </param>
		/// <param name="e">the read exception
		/// </param>
		/// <param name="filename">the file which was being read
		/// </param>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public static void  error(System.Exception t, System.IO.IOException e, string filename)
		{
			//if (e is System.IO.FileNotFoundException)
			//{
			//	// file is missing
			//	//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//	WarningDialog.showDisableable(t, (System.Object) (e.GetType().FullName + "@" + filename), "Error.file_not_found", filename);
			//}
			//else
			//{
			//	// something more serious happened during I/O
			//	//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//	ErrorDialog.showDisableable(t, (System.Object) (e.GetType().FullName + "@" + filename), "Error.file_read_error", filename);
			//}
		}
		
		/// <param name="t">the <code>Throwable</code> providing the stack trace
		/// </param>
		/// <param name="e">the read exception
		/// </param>
		/// <param name="file">the file which was being read
		/// </param>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public static void  error(System.Exception t, System.IO.IOException e, System.IO.FileInfo file)
		{
			error(t, e, file.FullName);
		}
		
		/// <param name="e">the read exception
		/// </param>
		/// <param name="filename">the file which was being read
		/// </param>
		public static void  error(System.IO.IOException e, string filename)
		{
			error(e, e, filename);
		}
		
		/// <param name="e">the read exception
		/// </param>
		/// <param name="file">the file which was being read
		/// </param>
		public static void  error(System.IO.IOException e, System.IO.FileInfo file)
		{
			error(e, e, file.FullName);
		}
		
		public static void  errorNoI18N(System.IO.IOException e, string filename)
		{
			////UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//ErrorDialog.showDisableable(null, e, (System.Object) (e.GetType().FullName + "@" + filename), "Unable to Read File", "Unable to Read File", "VASSAL was unable to read the file '" + filename + "'.");
		}
		
		public static void  errorNoI18N(System.IO.IOException e, System.IO.FileInfo file)
		{
			errorNoI18N(e, file.FullName);
		}
	}
}