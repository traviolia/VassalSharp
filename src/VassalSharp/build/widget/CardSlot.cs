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
using GameModule = VassalSharp.build.GameModule;
using Configurer = VassalSharp.configure.Configurer;
using BasicPiece = VassalSharp.counters.BasicPiece;
using GamePiece = VassalSharp.counters.GamePiece;
using Obscurable = VassalSharp.counters.Obscurable;
namespace VassalSharp.build.widget
{
	
	/// <summary> A CardSlot is identical to a PieceSlot except that it is initialized
	/// with a card-like GamePiece.
	/// </summary>
	public class CardSlot:PieceSlot
	{
		new public static System.String ConfigureTypeName
		{
			get
			{
				return "Card";
			}
			
		}
		override public Configurer Configurer
		{
			get
			{
				if (Piece == null)
				{
					GamePiece theCard = GameModule.getGameModule().createPiece(BasicPiece.ID + ";;;;");
					theCard = GameModule.getGameModule().createPiece(Obscurable.ID + "F;;Face down;B", theCard);
					Piece = theCard;
				}
				return base.Configurer;
			}
			
		}
		public CardSlot()
		{
		}
		
		public CardSlot(PieceSlot piece):this()
		{
			copyFrom(piece);
		}
	}
}