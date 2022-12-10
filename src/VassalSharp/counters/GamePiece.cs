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

//using BasicCommandEncoder = VassalSharp.build.module.BasicCommandEncoder;
//using GameState = VassalSharp.build.module.GameState;
//using Map = VassalSharp.build.module.Map;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
//using ChangePiece = VassalSharp.command.ChangePiece;
//using Command = VassalSharp.command.Command;

namespace VassalSharp.counters
{
	
	/// <summary> Basic class for representing a physical component of the game</summary>
	public interface GamePiece : PropertySource
	{
		/// <returns> the location of this piece on its owning {@link Map}
		/// </returns>
		System.Drawing.Point Position { get; set; }

		/// <summary>
        /// The shape of the piece from the user's viewpoint.  This defines the area
		/// in which the user must click to select or move the piece, for example.
		/// Like {@link #boundingBox}, it assumes the position is (0,0) and must be translated
		/// to the actual location where the piece is being drawn.
		/// </summary>
		System.Drawing.Drawing2D.GraphicsPath Shape { get; }

#if NEVER_DEFINED
		/// <returns> the {@link Stack} to which this piece belongs, or null if it doesn't belong to a stack.
		/// </returns>
		Stack Parent { get; set; }
#endif 
		/// <summary>And the translated name for this piece </summary>
		string LocalizedName { get; }

		/// <summary> Each GamePiece must have a unique String identifier</summary>
		/// <seealso cref="GameState.getNewPieceId">
		/// </seealso>
		string Id { get; set; }

		/// <summary>
        /// The type information is information that does not change
		/// during the course of a game.  Image file names, popup menu
		/// command names, etc., all should be reflected in the type.
		/// </summary>
		/// <seealso cref="BasicCommandEncoder">
		/// </seealso>
		string Type { get; }

		/// <summary>
        /// The state information is information that can change during
		/// the course of a game.  State information is saved when the game
		/// is saved and is transferred between players on the server.  For
		/// example, the relative order of pieces in a stack is state
		/// information, but whether the stack is expanded is not 
		/// </summary>
		string State { get; set; }

#if NEVER_DEFINED
		/// <summary>Each GamePiece belongs to a single {@link Map} </summary>
        Map Map { get; set; }
        //void setMap(Map map);
        //Map getMap();
#endif
        /// <summary> Draw this GamePiece</summary>
        /// <param name="g">
        /// </param>
        /// <param name="x">x-location of the center of the piece
        /// </param>
        /// <param name="y">y-location of the center of the piece
        /// </param>
        /// <param name="obs">the Component on which this piece is being drawn
        /// </param>
        /// <param name="zoom">the scaling factor.
        /// </param>
        void draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom);
		
		/// <summary> The area which this GamePiece occupies when
		/// drawn at the point (0,0)
		/// </summary>
		System.Drawing.Rectangle boundingBox();

#if NEVER_DEFINED
		/// <summary> Keyboard events are forward to this method when a piece is selected
		/// The GamePiece can respond in any way it likes
		/// 
		/// </summary>
		/// <returns> a {@link Command} that, when executed, will invoke
		/// the same response.  Usually a {@link ChangePiece} command.
		/// 
		/// </returns>
		/// <seealso cref="VassalSharp.build.module.map.ForwardToKeyBuffer">
		/// </seealso>
		Command keyEvent(System.Windows.Forms.KeyEventArgs stroke);
#endif
		/// <summary>The plain English name for this piece </summary>
		string getName();
		
		/// <summary>
        /// Other properties, possibly game-specific, can be associated with a piece.
		/// The properties may or may not need to be encoded
		/// in the piece's {@link #getState} method.  
		/// </summary>
		void  setProperty(System.Object key, System.Object val);
		
        // MLT - should be getter in PropertySource that goes with the preceeding set
		//new System.Object getProperty(System.Object key);
	}
}