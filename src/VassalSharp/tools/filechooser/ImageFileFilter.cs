/*
* $Id$
*
* Copyright (c) 2006-2008 by Joel Uckelman
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
using ImageUtils = VassalSharp.tools.image.ImageUtils;
namespace VassalSharp.tools.filechooser
{
	
	/// <summary> A FileFilter for GIF, JPEG, PNG, and SVG images. Used by file choosers
	/// to filter out files which aren't images.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	public class ImageFileFilter:ExtensionFileFilter
	{
		
		public ImageFileFilter():base("Image files", ImageUtils.IMAGE_SUFFIXES)
		{
		}
	}
}