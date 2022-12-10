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
using System;
namespace VassalSharp.counters
{
	
	/// <summary> Boolean Or of two PieceFilters</summary>
	public class BooleanOrPieceFilter : PieceFilter
	{
		virtual public PieceFilter Filter1
		{
			get
			{
				return filter1;
			}
			
		}
		virtual public PieceFilter Filter2
		{
			get
			{
				return filter2;
			}
			
		}
		private PieceFilter filter1;
		private PieceFilter filter2;
		
		public BooleanOrPieceFilter(PieceFilter filter1, PieceFilter filter2)
		{
			this.filter1 = filter1;
			this.filter2 = filter2;
		}
		
		public virtual bool accept(GamePiece piece)
		{
			return filter1.accept(piece) || filter2.accept(piece);
		}
	}
}