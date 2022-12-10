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
namespace VassalSharp.tools.image
{
	
	/// <summary> An exception indicating that an image file was not found.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	[Serializable]
	public class ImageNotFoundException:ImageIOException
	{
		private const long serialVersionUID = 1L;
		
		public ImageNotFoundException(System.String filename):base(new System.IO.FileInfo(filename))
		{
		}
		
		public ImageNotFoundException(System.IO.FileInfo file):base(file)
		{
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public ImageNotFoundException(System.String filename, System.Exception cause):base(filename, cause)
		{
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public ImageNotFoundException(System.IO.FileInfo file, System.Exception cause):base(file, cause)
		{
		}
	}
}