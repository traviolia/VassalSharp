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
using Decorator = VassalSharp.counters.Decorator;
using GamePiece = VassalSharp.counters.GamePiece;
namespace VassalSharp.command
{
	
	/// <summary> This class makes it more convenient to create a ChangePiece command.
	/// A TrackPiece is instantiated with a GamePiece and later finalized.
	/// The old state is the state of the GamePIece at instantiation, while
	/// the new state is the state of the GamePiece at finalization.
	/// </summary>
	/// <deprecated> Use {@link ChangeTracker}
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class TrackPiece:ChangePiece
	{
		override public System.String NewState
		{
			get
			{
				if (newState == null)
				{
					throw new System.SystemException("Must invoke finalize() before getting new state");
				}
				return base.NewState;
			}
			
		}
		private GamePiece piece;
		
		public TrackPiece(GamePiece p):base(p.Id, Decorator.getOutermost(p).State, null)
		{
			piece = p;
		}
		
		~TrackPiece()
		{
			newState = Decorator.getOutermost(piece).State;
		}
	}
}