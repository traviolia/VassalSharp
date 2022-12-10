using System;
using GameModule = VassalSharp.build.GameModule;
using GamePiece = VassalSharp.counters.GamePiece;
namespace VassalSharp.command
{
	
	/*
	* $Id$
	*
	* Copyright (c) 2003 by Rodney Kinney
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
	
	/// <summary> Convenience class for building {@link MovePiece} commands.</summary>
	/// <seealso cref="ChangeTracker">
	/// </seealso>
	public class MoveTracker
	{
		private System.String UnderneathId
		{
			get
			{
				System.String id = null;
				if (piece.Parent != null)
				{
					int index = piece.Parent.indexOf(piece);
					if (index > 0)
					{
						id = piece.Parent.getPieceAt(index - 1).Id;
					}
				}
				return id;
			}
			
		}
		private System.String MapId
		{
			get
			{
				return piece.getMap() == null?null:piece.getMap().getIdentifier();
			}
			
		}
		virtual public Command MoveCommand
		{
			get
			{
				return new MovePiece(piece.Id, MapId, piece.Position, UnderneathId, oldMapId, oldPosition, oldUnderneathId, GameModule.getUserId());
			}
			
		}
		private System.String oldMapId;
		private System.Drawing.Point oldPosition;
		private System.String oldUnderneathId;
		private GamePiece piece;
		
		public MoveTracker(GamePiece piece)
		{
			this.piece = piece;
			oldMapId = MapId;
			oldPosition = piece.Position;
			oldUnderneathId = UnderneathId;
		}
	}
}