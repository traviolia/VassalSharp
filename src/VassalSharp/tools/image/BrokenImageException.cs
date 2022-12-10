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
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
namespace VassalSharp.tools.image
{
	
	/// <summary> An exception indicating that an image file is broken in some way.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	[Serializable]
	public class BrokenImageException:ImageIOException
	{
		private const long serialVersionUID = 1L;
		
		protected internal BrokenImageException():base()
		{
		}
		
		public BrokenImageException(System.String filename):base(filename)
		{
		}
		
		public BrokenImageException(System.IO.FileInfo file):base(file)
		{
		}
		
		public BrokenImageException(System.String filename, System.String message):base(filename, message)
		{
		}
		
		public BrokenImageException(System.IO.FileInfo file, System.String message):base(file, message)
		{
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public BrokenImageException(System.String filename, System.Exception cause):base(filename, cause)
		{
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public BrokenImageException(System.IO.FileInfo file, System.Exception cause):base(file, cause)
		{
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public BrokenImageException(System.String filename, System.String message, System.Exception cause):base(filename, message, cause)
		{
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public BrokenImageException(System.IO.FileInfo file, System.String message, System.Exception cause):base(file, message, cause)
		{
		}
	}
}