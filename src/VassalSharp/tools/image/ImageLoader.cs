/*
* $Id$
*
* Copyright (c) 2009-2010 by Joel Uckelman
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
	
	/// <summary> An interface for loading images.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public interface ImageLoader
	{
		/// <summary> Loads an image.
		/// 
		/// </summary>
		/// <param name="name">the image name
		/// </param>
		/// <param name="in">the input stream
		/// </param>
		/// <param name="typeIfOpaque">the requested image type for opaque images
		/// </param>
		/// <param name="typeIfTransparent">the requested image type for transparent images
		/// </param>
		/// <param name="managed"><code>true</code> if a managed image should be returned
		/// </param>
		/// <returns> the image
		/// 
		/// </returns>
		/// <throws>  BrokenImageException if the image is faulty </throws>
		/// <throws>  UnrecognizedImageTypeException if the image type is not recognized </throws>
		/// <throws>  ImageIOException if reading the image goes wrong </throws>
		System.Drawing.Bitmap load(System.String name, System.IO.Stream in_Renamed, int typeIfOpaque, int typeIfTransparent, bool managed);
		
		/// <summary> Gets the size of an image.
		/// 
		/// </summary>
		/// <param name="name">the image name
		/// </param>
		/// <param name="in">the input stream
		/// </param>
		/// <returns> the size of the image
		/// 
		/// </returns>
		/// <throws>  BrokenImageException if the image is faulty </throws>
		/// <throws>  UnrecognizedImageTypeException if the image type is not recognized </throws>
		/// <throws>  ImageIOException if reading the image goes wrong </throws>
		System.Drawing.Size size(System.String name, System.IO.Stream in_Renamed);
	}
}