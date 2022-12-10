/*
* $Id$
*
* Copyright (c) 2007-2010 by Joel Uckelman
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
	
	/// <summary> An interface for classes which supply image tiles.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public interface ImageTileSource
	{
		/// <summary> Gets an image tile.
		/// 
		/// </summary>
		/// <param name="name">the image name
		/// </param>
		/// <param name="tileX">the X coordinate of the tile
		/// </param>
		/// <param name="tileY">the Y coordinate of the tile
		/// </param>
		/// <param name="scale">the scale of the tile
		/// </param>
		/// <returns> the tile
		/// 
		/// </returns>
		/// <throws>  IOException if the tile can't be read </throws>
		System.Drawing.Bitmap getTile(System.String name, int tileX, int tileY, double scale);
		
		/// <summary> Gets the size of an image tile.
		/// 
		/// </summary>
		/// <param name="name">the image name
		/// </param>
		/// <param name="tileX">the X coordinate of the tile
		/// </param>
		/// <param name="tileY">the Y coordinate of the tile
		/// </param>
		/// <param name="scale">the scale of the tile
		/// </param>
		/// <returns> the size of the tile
		/// 
		/// </returns>
		/// <throws>  IOException if the tile can't be read </throws>
		System.Drawing.Size getTileSize(System.String name, int tileX, int tileY, double scale);
		
		/// <summary> Checks whether an image tile exists.
		/// 
		/// </summary>
		/// <param name="name">the image name
		/// </param>
		/// <param name="tileX">the X coordinate of the tile
		/// </param>
		/// <param name="tileY">the Y coordinate of the tile
		/// </param>
		/// <param name="scale">the scale of the tile
		/// 
		/// </param>
		/// <throws>  IOException if the image can't be read </throws>
		bool tileExists(System.String name, int tileX, int tileY, double scale);
	}
}