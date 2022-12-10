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
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using BoundsTracker = VassalSharp.counters.BoundsTracker;
using GamePiece = VassalSharp.counters.GamePiece;
using Properties = VassalSharp.counters.Properties;
using StateMergeable = VassalSharp.counters.StateMergeable;
namespace VassalSharp.command
{
	
	/// <summary> This Command changes the state of a {@link GamePiece}.  Its undo
	/// Command is another ChangePiece with the new and old states
	/// reversed.  
	/// </summary>
	public class ChangePiece:Command
	{
		virtual public System.String Id
		{
			get
			{
				return id;
			}
			
		}
		virtual public System.String NewState
		{
			get
			{
				return newState;
			}
			
		}
		virtual public System.String OldState
		{
			get
			{
				return oldState;
			}
			
		}
		override public bool Null
		{
			get
			{
				return newState.Equals(oldState) && Atomic;
			}
			
		}
		override public System.String Details
		{
			get
			{
				return "id=" + id + ",oldState=" + oldState + ",newState=" + newState;
			}
			
		}
		protected internal System.String newState, oldState;
		private System.String id;
		private Command undo;
		
		/// <param name="id">the id of the {@link GamePiece} to change
		/// </param>
		/// <param name="oldState">the previous state of the piece
		/// </param>
		/// <param name="newState">the new state of the piece
		/// </param>
		public ChangePiece(System.String id, System.String oldState, System.String newState)
		{
			this.id = id;
			this.newState = newState;
			this.oldState = oldState;
		}
		
		/// <summary> When using this constructor, the previous state will be set
		/// to that of the piece when this Command is executed.
		/// </summary>
		/// <deprecated>
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public ChangePiece(System.String id, System.String newState):this(id, null, newState)
		{
		}
		
		/// <summary> Changes the state of a {@link GamePiece} by invoking {@link GamePiece#setState}</summary>
		//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override void  executeCommand()
		{
			GamePiece target = GameModule.getGameModule().getGameState().getPieceForId(id);
			if (target != null)
			{
				BoundsTracker bounds = new BoundsTracker();
				bounds.addPiece(target);
				if (oldState != null)
				{
					if (target is StateMergeable)
					{
						((StateMergeable) target).mergeState(newState, oldState);
					}
					else
					{
						target.State = newState;
					}
				}
				else
				{
					oldState = target.State;
					target.State = newState;
				}
				bounds.addPiece(target);
				bounds.repaint();
				if (target.getMap() != null && GlobalOptions.Instance.centerOnOpponentsMove() && !true.Equals(target.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)))
				{
					target.getMap().ensureVisible(target.getMap().selectionBoundsOf(target));
				}
			}
		}
		
		public override Command append(Command c)
		{
			Command last = this;
			Command[] sub = SubCommands;
			if (sub.Length > 0)
			{
				last = sub[sub.Length - 1];
			}
			if (c is ChangePiece && last is ChangePiece && ((ChangePiece) c).id != null && ((ChangePiece) c).id.Equals(((ChangePiece) last).id) && ((ChangePiece) c).newState != null)
			{
				((ChangePiece) last).newState = ((ChangePiece) c).newState;
				sub = c.SubCommands;
				for (int i = 0; i < sub.Length; ++i)
				{
					append(sub[i]);
				}
				return this;
			}
			else
			{
				return base.append(c);
			}
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override Command myUndoCommand()
		{
			if (undo == null && oldState != null)
			{
				undo = new ChangePiece(id, newState, oldState);
			}
			return undo;
		}
	}
}