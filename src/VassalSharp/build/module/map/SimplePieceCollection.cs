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
	
	/// <summary>Stores {@link VassalSharp.counters.GamePiece}s in a simple array </summary>
	public class SimplePieceCollection : PieceCollection
	{
		virtual public GamePiece[] AllPieces
		{
			get
			{
				return getPieces();
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private final ArrayList < GamePiece > pieces = new ArrayList < GamePiece >();
		
		/// <summary> Returns the index of a piece.  When painting the map, pieces
		/// are drawn in order of index 
		/// </summary>
		public virtual int indexOf(GamePiece p)
		{
			return pieces.indexOf(p);
		}
		
		public virtual bool canMerge(GamePiece p1, GamePiece p2)
		{
			return true;
		}
		
		public virtual void  add(GamePiece p)
		{
			pieces.add(p);
		}
		
		public virtual void  clear()
		{
			pieces.clear();
		}
		
		public virtual void  remove(GamePiece p)
		{
			removePieceAt(indexOf(p));
		}
		
		public virtual GamePiece[] getPieces()
		{
			return pieces.toArray(new GamePiece[pieces.size()]);
		}
		
		private void  removePieceAt(int gone)
		{
			if (gone >= 0)
			{
				pieces.remove(gone);
			}
		}
		
		public virtual void  reposition(GamePiece p, int pos)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'i '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int i = pieces.indexOf(p);
			if (i >= 0)
			{
				pieces.remove(i);
				pieces.add(pos, p);
			}
		}
		
		public virtual void  moveToBack(GamePiece p)
		{
			reposition(p, 0);
		}
		
		public virtual void  moveToFront(GamePiece p)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'i '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int i = pieces.indexOf(p);
			if (i >= 0)
			{
				pieces.remove(p);
				pieces.add(p);
			}
		}
	}
}