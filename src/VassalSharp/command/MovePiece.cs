using System;
using GameModule = VassalSharp.build.GameModule;
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using Map = VassalSharp.build.module.Map;
using HighlightLastMoved = VassalSharp.build.module.map.HighlightLastMoved;
using BoundsTracker = VassalSharp.counters.BoundsTracker;
using Deck = VassalSharp.counters.Deck;
using DeckVisitor = VassalSharp.counters.DeckVisitor;
using DeckVisitorDispatcher = VassalSharp.counters.DeckVisitorDispatcher;
using GamePiece = VassalSharp.counters.GamePiece;
using PieceVisitorDispatcher = VassalSharp.counters.PieceVisitorDispatcher;
using Properties = VassalSharp.counters.Properties;
using Stack = VassalSharp.counters.Stack;
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
	
	/// <summary> Command that moves a piece to a new location and position within a stack.
	/// While this can be accomplished with a {@link ChangePiece} command, this
	/// command is safer in terms of recovering from changes to the game state that may have occurred
	/// since the command was created.  For instance, A {@link ChangePiece} command that adds
	/// a piece to a {@link VassalSharp.counters.Stack} will cause the piece to disappear if the
	/// stack has been deleted.  This Command will recover more gracefully.
	/// </summary>
	public class MovePiece:Command
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDeckVisitor' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDeckVisitor : DeckVisitor
		{
			public AnonymousClassDeckVisitor(System.Drawing.Point pt, VassalSharp.build.module.Map map, VassalSharp.counters.GamePiece p, MovePiece enclosingInstance)
			{
				InitBlock(pt, map, p, enclosingInstance);
			}
			private void  InitBlock(System.Drawing.Point pt, VassalSharp.build.module.Map map, VassalSharp.counters.GamePiece p, MovePiece enclosingInstance)
			{
				this.pt = pt;
				this.map = map;
				this.p = p;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable pt was copied into class AnonymousClassDeckVisitor. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Drawing.Point pt;
			//UPGRADE_NOTE: Final variable map was copied into class AnonymousClassDeckVisitor. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.build.module.Map map;
			//UPGRADE_NOTE: Final variable p was copied into class AnonymousClassDeckVisitor. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.counters.GamePiece p;
			private MovePiece enclosingInstance;
			public MovePiece Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual System.Object visitDeck(Deck d)
			{
				if (d.Position.Equals(pt))
				{
					return map.StackMetrics.merge(d, p);
				}
				else
				{
					return null;
				}
			}
			
			public virtual System.Object visitStack(Stack s)
			{
				if (s.Position.Equals(pt) && map.StackMetrics.StackingEnabled && !true.Equals(p.getProperty(VassalSharp.counters.Properties_Fields.NO_STACK)) && s.topPiece(Enclosing_Instance.playerId) != null && map.getPieceCollection().canMerge(p, s))
				{
					return map.StackMetrics.merge(s, p);
				}
				else
				{
					return null;
				}
			}
			
			public virtual System.Object visitDefault(GamePiece piece)
			{
				if (piece.Position.Equals(pt) && map.StackMetrics.StackingEnabled && !true.Equals(p.getProperty(VassalSharp.counters.Properties_Fields.NO_STACK)) && !true.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.NO_STACK)) && map.getPieceCollection().canMerge(p, piece))
				{
					System.String hiddenBy = (System.String) piece.getProperty(VassalSharp.counters.Properties_Fields.HIDDEN_BY);
					if (hiddenBy == null || hiddenBy.Equals(Enclosing_Instance.playerId))
					{
						return map.StackMetrics.merge(piece, p);
					}
					else
					{
						return null;
					}
				}
				else
				{
					return null;
				}
			}
		}
		virtual public System.String Id
		{
			get
			{
				return id;
			}
			
		}
		virtual public System.String NewMapId
		{
			get
			{
				return newMapId;
			}
			
		}
		virtual public System.String OldMapId
		{
			get
			{
				return oldMapId;
			}
			
		}
		virtual public System.Drawing.Point NewPosition
		{
			get
			{
				return newPosition;
			}
			
		}
		virtual public System.Drawing.Point OldPosition
		{
			get
			{
				return oldPosition;
			}
			
		}
		virtual public System.String NewUnderneathId
		{
			get
			{
				return newUnderneathId;
			}
			
		}
		virtual public System.String OldUnderneathId
		{
			get
			{
				return oldUnderneathId;
			}
			
		}
		virtual public System.String PlayerId
		{
			get
			{
				return playerId;
			}
			
		}
		override public System.String Details
		{
			get
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Point.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return "id=" + id + ",map=" + newMapId + ",position=" + newPosition + ",under=" + newUnderneathId;
			}
			
		}
		private System.String id;
		private System.String newMapId;
		private System.String oldMapId;
		private System.Drawing.Point newPosition;
		private System.Drawing.Point oldPosition;
		private System.String newUnderneathId;
		private System.String oldUnderneathId;
		private System.String playerId;
		
		/// <summary> </summary>
		/// <param name="id">The id of the piece being moved
		/// </param>
		/// <param name="newMapId">The id of the map being moved to
		/// </param>
		/// <param name="newPosition">the new position
		/// </param>
		/// <param name="newUnderneathId">The id of the piece which will be immediately beneath this piece in any containing Stack.  May be null
		/// </param>
		/// <param name="oldMapId">The id of the map being moved from
		/// </param>
		/// <param name="oldPosition">the old position
		/// </param>
		/// <param name="oldUnderneathId">The id of the piece which was immediately beneath this piece in its original containing Stack.
		/// </param>
		/// <param name="playerId">the id of the player making this move
		/// </param>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public MovePiece(System.String id, System.String newMapId, ref System.Drawing.Point newPosition, System.String newUnderneathId, System.String oldMapId, ref System.Drawing.Point oldPosition, System.String oldUnderneathId, System.String playerId)
		{
			this.id = id;
			this.newMapId = newMapId;
			this.oldMapId = oldMapId;
			this.newPosition = newPosition;
			this.oldPosition = oldPosition;
			this.newUnderneathId = newUnderneathId;
			this.oldUnderneathId = oldUnderneathId;
			this.playerId = playerId;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override void  executeCommand()
		{
			GamePiece piece = GameModule.getGameModule().getGameState().getPieceForId(id);
			if (piece != null)
			{
				BoundsTracker bounds = new BoundsTracker();
				bounds.addPiece(piece);
				Map newMap = Map.getMapById(newMapId);
				if (newMap != null)
				{
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					PieceVisitorDispatcher mergeFinder = createMergeFinder(newMap, piece, ref newPosition);
					if (newUnderneathId != null)
					{
						GamePiece under = GameModule.getGameModule().getGameState().getPieceForId(newUnderneathId);
						if (under != null && under.Position.Equals(newPosition))
						{
							newMap.StackMetrics.merge(under, piece);
						}
						else
						{
							if (newMap.apply(mergeFinder) == null)
							{
								newMap.placeAt(piece, newPosition);
							}
						}
					}
					else
					{
						if (newMap.apply(mergeFinder) == null)
						{
							newMap.placeAt(piece, newPosition);
						}
						if (piece.Parent != null)
						{
							piece.Parent.insert(piece, 0);
						}
					}
				}
				else
				{
					Map oldMap = Map.getMapById(oldMapId);
					if (oldMap != null)
					{
						oldMap.removePiece(piece);
					}
				}
				bounds.addPiece(piece);
				
				// Highlight the stack the piece was moved to
				HighlightLastMoved.setLastMoved(piece);
				
				bounds.repaint();
				if (piece.getMap() != null && GlobalOptions.Instance.centerOnOpponentsMove() && !true.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)))
				{
					piece.getMap().ensureVisible(piece.getMap().selectionBoundsOf(piece));
				}
			}
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override Command myUndoCommand()
		{
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			return new MovePiece(id, oldMapId, ref oldPosition, oldUnderneathId, newMapId, ref newPosition, newUnderneathId, playerId);
		}
		
		/// <summary> Creates a new {@link PieceVisitorDispatcher} that will create a {@link Command} object
		/// to merge the target piece with any applicable pieces at the target location
		/// </summary>
		/// <param name="map">
		/// </param>
		/// <param name="p">
		/// </param>
		/// <param name="pt">
		/// </param>
		/// <returns>
		/// </returns>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual PieceVisitorDispatcher createMergeFinder(Map map, GamePiece p, ref System.Drawing.Point pt)
		{
			PieceVisitorDispatcher dispatch = new DeckVisitorDispatcher(new AnonymousClassDeckVisitor(pt, map, p, this));
			return dispatch;
		}
	}
}