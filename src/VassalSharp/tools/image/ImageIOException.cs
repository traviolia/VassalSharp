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
	
	/// <summary> An exception indicating some I/O problem while reading an image file.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	[Serializable]
	public class ImageIOException:System.IO.IOException
	{
		virtual public System.IO.FileInfo File
		{
			get
			{
				return file;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'file '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.IO.FileInfo file;
		
		protected internal ImageIOException()
		{
			this.file = null;
		}
		
		public ImageIOException(System.String filename):this(new System.IO.FileInfo(filename))
		{
		}
		
		public ImageIOException(System.IO.FileInfo file):base(file.FullName)
		{
			this.file = file;
		}
		
		public ImageIOException(System.String filename, System.String message):this(new System.IO.FileInfo(filename), message)
		{
		}
		
		public ImageIOException(System.IO.FileInfo file, System.String message):base(file.FullName + ": " + message)
		{
			this.file = file;
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public ImageIOException(System.String filename, System.Exception cause):this(new System.IO.FileInfo(filename), cause)
		{
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public ImageIOException(System.IO.FileInfo file, System.Exception cause):this(file)
		{
			initCause(cause);
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public ImageIOException(System.String filename, System.String message, System.Exception cause):this(new System.IO.FileInfo(filename), message, cause)
		{
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public ImageIOException(System.IO.FileInfo file, System.String message, System.Exception cause):base(file.FullName + ": " + message)
		{
			this.file = file;
			initCause(cause);
		}
	}
}