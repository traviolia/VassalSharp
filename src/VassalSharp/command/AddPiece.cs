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
using GameState = VassalSharp.build.module.GameState;
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using HighlightLastMoved = VassalSharp.build.module.map.HighlightLastMoved;
using GamePiece = VassalSharp.counters.GamePiece;
using Properties = VassalSharp.counters.Properties;
namespace VassalSharp.command
{
	
	/// <summary> This Command adds a {@link GamePiece} to a game.  Its undo
	/// Command is {@link RemovePiece}.
	/// </summary>
	public class AddPiece:Command
	{
		virtual public GamePiece Target
		{
			get
			{
				return target;
			}
			
		}
		virtual public System.String State
		{
			get
			{
				return state;
			}
			
		}
		private Command undo;
		private GamePiece target;
		private System.String state;
		
		public AddPiece(GamePiece p):this(p, p.State)
		{
		}
		
		public AddPiece(GamePiece p, System.String state)
		{
			target = p;
			this.state = state;
		}
		
		/// <summary> Adds a piece by invoking {@link GameState#addPiece}, followed by
		/// {@link GamePiece#setState}
		/// </summary>
		//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override void  executeCommand()
		{
			if (target != null)
			{
				GameModule.getGameModule().getGameState().addPiece(target);
				target.State = state;
				if (target.getMap() != null)
				{
					HighlightLastMoved.setLastMoved(target);
					if (GlobalOptions.Instance.centerOnOpponentsMove() && !true.Equals(target.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)))
					{
						target.getMap().ensureVisible(target.getMap().selectionBoundsOf(target));
						target.getMap().repaint();
					}
				}
			}
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override Command myUndoCommand()
		{
			if (undo == null)
			{
				undo = new RemovePiece(target);
			}
			return undo;
		}
	}
}