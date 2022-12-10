using System;
//using ImageOp = VassalSharp.tools.imageop.ImageOp;
namespace VassalSharp.tools
{
	
	/*
	* $Id$
	*
	* Copyright (c) 2005 by Rodney Kinney
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
	
	/// <summary> Used for lazy initialization of images in a DataArchive</summary>
	/// <seealso cref="DataArchive.addImageSource">
	/// </seealso>
	/// <deprecated> Use an {@link ImageOp} instead.
	/// </deprecated>
	public interface ImageSource
	{
		/// <summary>The lazy creation of the image </summary>
		System.Drawing.Image Image
		{
			get;
			
		}
	}
}