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
using Map = VassalSharp.build.module.Map;
namespace VassalSharp.counters
{
	
	/// <summary> This interface defines selection criteria for finding a GamePiece in a Map</summary>
	public class StackOnly:Movable
	{
		public override System.Object visitDefault(GamePiece piece)
		{
			return null;
		}
		
		public override System.Object visitStack(Stack s)
		{
			GamePiece selected = (GamePiece) base.visitStack(s);
			if (selected != null && selected.Parent == s)
			{
				selected = s;
			}
			return selected;
		}
	}
	public class PieceInStack:Movable
	{
		public override System.Object visitStack(Stack s)
		{
			GamePiece selected = (GamePiece) base.visitStack(s);
			if (selected == s && !s.isExpanded())
			{
				selected = s.topPiece();
			}
			return selected;
		}
	}
	public class Movable : PieceFinder, DeckVisitor
	{
		private void  InitBlock()
		{
			dispatcher = new DeckVisitorDispatcher(this);
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		protected internal System.Drawing.Drawing2D.GraphicsPath[] shapes = new System.Drawing.Drawing2D.GraphicsPath[0];
		protected internal Map map;
		protected internal System.Drawing.Point pt;
		//UPGRADE_NOTE: The initialization of  'dispatcher' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal DeckVisitorDispatcher dispatcher;
		
		// This constructor is safe only if using the PieceFinder.select() method
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public Movable():this(null, ref tempAux)
		{
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public Movable(Map map, ref System.Drawing.Point pt)
		{
			this.map = map;
			this.pt = pt;
		}
		
		public virtual System.Object visitDeck(Deck d)
		{
			return null;
		}
		
		public virtual System.Object visitDefault(GamePiece piece)
		{
			GamePiece selected = null;
			//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Drawing.Drawing2D.GraphicsPath s = piece.Shape;
			System.Drawing.Point pos = piece.Position;
			System.Drawing.Point p = new System.Drawing.Point(pt.X - pos.X, pt.Y - pos.Y);
			if (s.IsVisible(p))
			{
				selected = piece;
			}
			return selected;
		}
		
		public virtual System.Object visitStack(Stack s)
		{
			GamePiece selected = null;
			if (shapes.Length < s.PieceCount)
			{
				//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				shapes = new System.Drawing.Drawing2D.GraphicsPath[s.PieceCount];
			}
			map.StackMetrics.getContents(s, null, shapes, null, s.Position.X, s.Position.Y);
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			return selected;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual GamePiece select(Map map, GamePiece piece, ref System.Drawing.Point pt)
		{
			this.map = map;
			this.pt = pt;
			return (GamePiece) dispatcher.accept(piece);
		}
		private static System.Drawing.Point tempAux = System.Drawing.Point.Empty;
	}
	public struct PieceFinder_Fields{
		/// <summary>Return a Stack overlapping the given point </summary>
		public readonly static PieceFinder STACK_ONLY;
		/// <summary> If a Stack overlaps the given point, return the piece containing that point if expanded,
		/// or the top piece if not expanded.
		/// 
		/// </summary>
		public readonly static PieceFinder PIECE_IN_STACK;
		/// <summary>Returns a Stack if unexpanded and overlapping the given point,
		/// or a piece within that stack if expanded and overlapping the given point
		/// </summary>
		public readonly static PieceFinder MOVABLE;
		static PieceFinder_Fields()
		{
			STACK_ONLY = new StackOnly();
			PIECE_IN_STACK = new PieceInStack();
			MOVABLE = new Movable();
		}
	}
	public interface PieceFinder
	{
		//UPGRADE_NOTE: Members of interface 'PieceFinder' were extracted into structure 'PieceFinder_Fields'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1045'"
		/// <summary>Return the argument GamePiece (or one of its children if a Stack) found at the given point on the given Map </summary>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		GamePiece select(Map map, GamePiece piece, ref System.Drawing.Point pt);
		
	}
}