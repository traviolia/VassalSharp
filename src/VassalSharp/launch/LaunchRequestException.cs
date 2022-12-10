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
using VassalSharp.i18n;

namespace VassalSharp.launch
{
	
	/// <summary> The {@link Exception} thrown by {@link LaunchRequest} when command line
	/// arguments cannot be parsed.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	[Serializable]
	public class LaunchRequestException : System.Exception
	{
		public LaunchRequestException()
		{
		}
		private const long serialVersionUID = 1L;
		
		/// <summary> Constructs a new exception with the argument list interpolated
		/// into the i18n string specified by the key.
		/// 
		/// </summary>
		/// <param name="key">{@link Resources} key
		/// </param>
		/// <param name="vals">{@link Resources} arguments
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public LaunchRequestException(String key, params String[] vals) : base(Resources.getString(key, (System.Object[])vals))
		{ 
		}
	}
}