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
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
using Reference = VassalSharp.tools.lang.Reference;
namespace VassalSharp.tools.image
{
	
	/// <summary> An interface for converting {@link BufferedImage}s from one type to
	/// another.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public interface ImageTypeConverter
	{
		/// <summary> Converts an image to the given type.
		/// 
		/// </summary>
		/// <param name="ref">a holder for the image
		/// </param>
		/// <param name="type">the type of image to return
		/// </param>
		/// <returns> a converted image
		/// 
		/// </returns>
		/// <throws>  ImageIOException if something goes wrong </throws>
		System.Drawing.Bitmap convert();
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	< BufferedImage > ref, int type) 
	throws ImageIOException;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}