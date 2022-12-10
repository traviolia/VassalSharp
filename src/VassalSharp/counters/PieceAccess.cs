/*
* $Id$
*
* Copyright (c) 2000-2006 by Rodney Kinney
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
	
	/// <summary> Defines ability of a player to access a GamePiece</summary>
	/// <author>  rkinney
	/// 
	/// </author>
	/// <summary> Global utility class to temporarily restrict access to all piece</summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class GlobalAccess
	{
		public static bool HideAll
		{
			get
			{
				return allHidden;
			}
			
		}
		private static bool allHidden = false;
		public static void  hideAll()
		{
			allHidden = true;
		}
		public static void  revertAll()
		{
			allHidden = false;
		}
	}
	public interface PieceAccess
	{
		/// <summary> Return a String identifying the current player</summary>
		System.String CurrentPlayerId
		{
			get;
			
		}
		/// <summary> Return true if the current player can access this piece,
		/// given that the piece is owned by the player with the given id
		/// </summary>
		bool currentPlayerHasAccess(System.String ownerId);
		
		/// <summary> Return true if the current player can take (or relinquish)
		/// control of this piece
		/// </summary>
		bool currentPlayerCanModify(System.String ownerId);
		
	}
}