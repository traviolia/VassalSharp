/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
namespace VassalSharp.counters
{
	
	/// <summary> An interface that defines how to draw GamePieces that are selected
	/// 
	/// </summary>
	/// <seealso cref="BasicPiece.getHighlighter">
	/// </seealso>
	public interface Highlighter
	{
		void  draw(GamePiece p, System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom);
		
		System.Drawing.Rectangle boundingBox(GamePiece p);
	}
}