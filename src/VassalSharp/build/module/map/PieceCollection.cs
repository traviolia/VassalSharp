/*
* $Id$
*
* Copyright (c) 2004 by Rodney Kinney
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
using GamePiece = VassalSharp.counters.GamePiece;
namespace VassalSharp.build.module.map
{
	
	/// <summary> Manages a collection of {@link VassalSharp.counters.GamePiece}s in a {@link VassalSharp.build.module.Map}</summary>
	public interface PieceCollection
	{
		/// <summary>Return all pieces in the collection, regardless of visibility </summary>
		GamePiece[] AllPieces
		{
			get;
			
		}
		/// <summary>Reposition a piece to the front of all others in the same layer</summary>
		void  moveToFront(GamePiece p);
		/// <summary>Reposition a piece to the back of all others in the same layer</summary>
		void  moveToBack(GamePiece p);
		/// <summary>Return all currently-visible pieces in the collection as a read-only array </summary>
		GamePiece[] getPieces();
		/// <summary>Return true if the two pieces can be merged into a single stack </summary>
		bool canMerge(GamePiece p1, GamePiece p2);
		/// <summary> Returns the index of a piece.  When painting the map, pieces
		/// are drawn in order of index 
		/// </summary>
		int indexOf(GamePiece p);
		/// <summary>Removes the piece </summary>
		void  remove(GamePiece p);
		/// <summary>Adds a piece </summary>
		void  add(GamePiece p);
		/// <summary>Remove all pieces </summary>
		void  clear();
	}
}