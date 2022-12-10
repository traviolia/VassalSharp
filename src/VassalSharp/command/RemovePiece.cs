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
using Map = VassalSharp.build.module.Map;
using HighlightLastMoved = VassalSharp.build.module.map.HighlightLastMoved;
using GamePiece = VassalSharp.counters.GamePiece;
using KeyBuffer = VassalSharp.counters.KeyBuffer;
using Stack = VassalSharp.counters.Stack;
namespace VassalSharp.command
{
	
	/// <summary> This Command removed a {@link GamePiece} from a game.  Its undo
	/// Command is {@link AddPiece}.  
	/// </summary>
	public class RemovePiece:Command
	{
		virtual public GamePiece Target
		{
			get
			{
				return target;
			}
			
		}
		virtual public System.String Id
		{
			get
			{
				return target == null?id:target.Id;
			}
			
		}
		private Command undo = null;
		private GamePiece target;
		private System.String id;
		
		public RemovePiece(GamePiece p)
		{
			target = p;
		}
		
		public RemovePiece(System.String id)
		{
			this.id = id;
		}
		
		/// <summary> Removes a piece by invoking {@link Map#removePiece} if the
		/// piece belongs to a {@link Map}, followed by {@link
		/// GameState#removePiece}.
		/// </summary>
		//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override void  executeCommand()
		{
			if (target == null)
			{
				target = GameModule.getGameModule().getGameState().getPieceForId(id);
				if (target == null)
					return ;
			}
			
			undo = new AddPiece(target, target.State);
			
			System.Drawing.Rectangle r = System.Drawing.Rectangle.Empty;
			//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Map m = target.getMap();
			//UPGRADE_NOTE: Final was removed from the declaration of 'parent '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Stack parent = target.Parent;
			
			// Highlight the stack the piece was removed from - Ben
			HighlightLastMoved.setLastMoved(target);
			
			if (m != null)
			{
				r = parent == null?m.boundingBoxOf(target):m.boundingBoxOf(parent);
				m.removePiece(target);
				target.setMap(null);
			}
			
			if (parent != null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'stateWithPiece '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String stateWithPiece = parent.State;
				parent.remove(target);
				undo = undo.append(new ChangePiece(parent.Id, parent.State, stateWithPiece));
				target.Parent = null;
			}
			
			if (m != null)
			{
				m.repaint(r);
			}
			
			GameModule.getGameModule().getGameState().removePiece(target.Id);
			KeyBuffer.Buffer.remove(target);
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override Command myUndoCommand()
		{
			if (undo == null && target != null)
			{
				undo = new AddPiece(target);
			}
			return undo;
		}
	}
}