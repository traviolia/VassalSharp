using System;
using Decorator = VassalSharp.counters.Decorator;
using GamePiece = VassalSharp.counters.GamePiece;
namespace VassalSharp.command
{
	
	/*
	* $Id$
	*
	* Copyright (c) 2003-2011 by Rodney Kinney, Brent Easton
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
	
	/// <summary> Utility class for generating {@link ChangePiece} commands.
	/// To use, construct a new ChangeTracker, make any changes
	/// to the target {@link VassalSharp.counters.GamePiece}, then invoke {@link #getChangeCommand}
	/// </summary>
	public class ChangeTracker
	{
		virtual public Command ChangeCommand
		{
			get
			{
				return new ChangePiece(piece.Id, oldState, Decorator.getOutermost(piece).State);
			}
			
		}
		virtual public bool Changed
		{
			get
			{
				return !oldState.Equals(Decorator.getOutermost(piece).State);
			}
			
		}
		private GamePiece piece;
		private System.String oldState;
		
		public ChangeTracker(GamePiece p)
		{
			oldState = Decorator.getOutermost(p).State;
			piece = p;
		}
	}
}