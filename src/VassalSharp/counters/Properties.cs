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
	
	/// <summary> Defines the properties expected to be defined in each GamePiece</summary>
	public struct Properties_Fields{
		/// <summary> Return Boolean.TRUE if the piece cannot be seen by the user</summary>
		public readonly static System.String INVISIBLE_TO_ME = "Invisible";
		public readonly static System.String INVISIBLE_TO_OTHERS = "InvisibleToOthers";
		/// <summary> Return Boolean.TRUE if the piece's identity is not known to other players</summary>
		public readonly static System.String OBSCURED_TO_OTHERS = "ObscuredToOthers";
		/// <summary> Return Boolean.TRUE if the piece's identity is not known to the user</summary>
		public readonly static System.String OBSCURED_TO_ME = "Obscured";
		/// <summary> If a piece is obscured to other players, return a String identifying the player who obscured it
		/// 
		/// </summary>
		/// <seealso cref="VassalSharp.build.GameModule.getUserId">
		/// </seealso>
		public readonly static System.String OBSCURED_BY;
		/// <summary> If non-null, then return an instance of {@link EventFilter}. The piece will not respond to selection events if the
		/// filter rejects them
		/// </summary>
		public readonly static System.String SELECT_EVENT_FILTER = "selectEventFilter";
		/// <summary> If non-null, then return an instance of {@link EventFilter}. The piece will not respond to move events if the
		/// filter rejects them
		/// </summary>
		public readonly static System.String MOVE_EVENT_FILTER = "moveEventFilter";
		/// <summary> If a piece is hidden to other players, return a STring identifying the player who hit it
		/// 
		/// </summary>
		/// <seealso cref="VassalSharp.build.GameModule.getUserId">
		/// </seealso>
		public readonly static System.String HIDDEN_BY;
		/// <summary> Return Boolean.TRUE if the piece behaves more like a terrain feature than a playing piece</summary>
		public readonly static System.String TERRAIN = "Immobile";
		/// <summary> Return Boolean.TRUE if the piece should ignore map grids when being moved</summary>
		public readonly static System.String IGNORE_GRID = "IgnoreGrid";
		/// <summary>Return Boolean.TRUE if the piece does not form stacks </summary>
		public readonly static System.String NO_STACK = "NoStack";
		/// <summary> Return Boolean.TRUE if the piece has been marked as selected</summary>
		public readonly static System.String SELECTED = "Selected";
		/// <summary> Return a KeyCommand[] object representing the popup menu equivalencies for the key commands recognized by this
		/// piece
		/// </summary>
		public readonly static System.String KEY_COMMANDS = "KeyCommands";
		/// <summary> If this piece is a Decorator, return the decorated piece</summary>
		public readonly static System.String INNER = "Inner";
		/// <summary> If this piece decorated by a Decorator, return the Decorator</summary>
		public readonly static System.String OUTER = "Outer";
		/// <summary> Return Boolean.TRUE if this piece has Restricted Access</summary>
		public readonly static System.String RESTRICTED = "Restricted";
		/// <summary> Return Boolean.TRUE if this piece has movement restricted by a Restricted Access trait   </summary>
		public readonly static System.String RESTRICTED_MOVEMENT = "RestrictedMovement";
		/// <summary>Return Boolean.TRUE if this piece has been moved </summary>
		public readonly static System.String MOVED = "Moved";
		/// <summary>Used to store a duplicate of the target piece at some point in time </summary>
		public readonly static System.String SNAPSHOT = "snapshot";
		/// <summary> If Boolean.TRUE, then treat the piece as if it were not rotated. This effects the value returned by
		/// {@link GamePiece#getShape} and the way the piece is drawn.
		/// 
		/// </summary>
		/// <seealso cref="FreeRotator">
		/// </seealso>
		public readonly static System.String USE_UNROTATED_SHAPE = "useUnrotatedShape";
		/// <summary> Return a String representing the visible features of the piece. If this String changes value, then the piece should
		/// be refreshed
		/// </summary>
		public readonly static System.String VISIBLE_STATE = "visibleState";
		/// <summary>Return Boolean.TRUE if the piece can never be moved </summary>
		public readonly static System.String NON_MOVABLE = "cannotMove";
		/// <summary>Global Piece Id </summary>
		public readonly static System.String PIECE_ID = "PieceId";
		static Properties_Fields()
		{
			OBSCURED_BY = Obscurable.ID;
			HIDDEN_BY = Hideable.HIDDEN_BY;
		}
	}
	public interface Properties
	{
		//UPGRADE_NOTE: Members of interface 'Properties' were extracted into structure 'Properties_Fields'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1045'"
		
	}
}