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
namespace VassalSharp.tools.version
{
	
	/// <summary> An {@link Exception} indicating a malformed VASSAL version string.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	/// <seealso cref="Version">
	/// </seealso>
	/// <seealso cref="VersionTokenizer">
	/// </seealso>
	[Serializable]
	public class VersionFormatException:System.Exception
	{
		private const long serialVersionUID = 1L;
		
		/// <summary>{@inheritDoc} </summary>
		public VersionFormatException()
		{
		}
		
		/// <summary>{@inheritDoc} </summary>
		public VersionFormatException(System.String message):base(message)
		{
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public VersionFormatException(System.String message, System.Exception cause):base(message, cause)
		{
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public VersionFormatException(System.Exception cause):base(cause)
		{
		}
	}
}