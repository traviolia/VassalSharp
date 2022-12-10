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
using BadDataReport = VassalSharp.build.BadDataReport;
using GameModule = VassalSharp.build.GameModule;
using GameState = VassalSharp.build.module.GameState;
using Map = VassalSharp.build.module.Map;
using StackMetrics = VassalSharp.build.module.map.StackMetrics;
using Command = VassalSharp.command.Command;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
using EnumeratedIterator = VassalSharp.tools.EnumeratedIterator;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> A collection of GamePieces which can be moved as a single unit</summary>
	public class Stack : GamePiece, StateMergeable
	{
		private void  InitBlock()
		{
			return new AllPieceIterator(this);
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new EnumeratedIterator < GamePiece >(new AllPieceIterator());
			//    return new AllPieceEnum();
			return new ReversePieceIterator(this);
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new EnumeratedIterator < GamePiece >(new ReversePieceIterator());
			//    return new ReversePieceEnum();
			return new VisibleOrderIterator(this);
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new EnumeratedIterator < GamePiece >(new VisibleOrderIterator());
			//    return new VisibleOrderEnum();
		}
		virtual public int PieceCount
		{
			get
			{
				return pieceCount;
			}
			
		}
		/// <summary> Return the number of pieces that could possible be drawn in the stack, regardless of visibility to any particular player</summary>
		/// <returns>
		/// </returns>
		virtual public int MaximumVisiblePieceCount
		{
			get
			{
				return pieceCount;
			}
			
		}
		virtual public System.String LocalizedName
		{
			get
			{
				return getName(true);
			}
			
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		virtual public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				System.Drawing.Region a = new System.Drawing.Region();
				//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Drawing.Drawing2D.GraphicsPath[] childBounds = new System.Drawing.Drawing2D.GraphicsPath[PieceCount];
				StackMetrics metrics = getMap() == null?getDefaultMetrics():getMap().StackMetrics;
				metrics.getContents(this, null, childBounds, null, 0, 0);
				//UPGRADE_NOTE: Final was removed from the declaration of 'visibleFilter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				PieceIterator visibleFilter = PieceIterator.visible(getPiecesIterator());
				while (visibleFilter.hasMoreElements())
				{
					GamePiece p = visibleFilter.nextPiece();
					//UPGRADE_TODO: Method 'java.awt.geom.Area.add' was converted to 'System.Drawing.Region.Union' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtgeomAreaadd_javaawtgeomArea'"
					a.Union(new System.Drawing.Region(childBounds[indexOf(p)]));
				}
				return a;
			}
			
		}
		virtual public System.String State
		{
			get
			{
				SequenceEncoder se = new SequenceEncoder(';');
				se.append(getMap() == null?"null":getMap().getIdentifier()).append(Position.X).append(Position.Y);
				for (int i = 0; i < pieceCount; ++i)
				{
					se.append(contents[i].Id);
				}
				return se.Value;
			}
			
			set
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(value, ';');
				//UPGRADE_NOTE: Final was removed from the declaration of 'mapId '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String mapId = st.nextToken();
				Position = new System.Drawing.Point(st.nextInt(0), st.nextInt(0));
				pieceCount = 0;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'gs '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GameState gs = GameModule.getGameModule().getGameState();
				while (st.hasMoreTokens())
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'child '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					GamePiece child = gs.getPieceForId(st.nextToken());
					if (child != null)
						insertChild(child, pieceCount);
				}
				
				Map m = null;
				if (!"null".Equals(mapId))
				{
					m = Map.getMapById(mapId);
					if (m == null)
					{
						ErrorDialog.dataError(new BadDataReport("Could not find map", mapId, null));
					}
				}
				
				if (m != getMap())
				{
					if (m != null)
					{
						m.addPiece(this);
					}
					else
					{
						setMap(null);
					}
				}
			}
			
		}
		virtual public System.String Type
		{
			get
			{
				return TYPE;
			}
			
		}
		virtual public System.Drawing.Point Position
		{
			get
			{
				return new System.Drawing.Point(new System.Drawing.Size(pos));
			}
			
			set
			{
				pos = value;
			}
			
		}
		virtual public Stack Parent
		{
			get
			{
				return null;
			}
			
			set
			{
				if (value != null)
				{
					ErrorDialog.dataError(new BadDataReport("Cannot add stack to another stack", ToString(), null));
				}
			}
			
		}
		virtual public System.String Id
		{
			get
			{
				return id;
			}
			
			set
			{
				this.id = value;
			}
			
		}
		public const System.String TYPE = "stack";
		protected internal const int INCR = 5;
		protected internal GamePiece[] contents = new GamePiece[INCR];
		protected internal int pieceCount = 0;
		
		protected internal System.Drawing.Point pos = new System.Drawing.Point(0, 0);
		
		private System.String id;
		private bool expanded = false;
		
		protected internal Map map;
		private static StackMetrics defaultMetrics;
		
		public Stack():this(null)
		{
		}
		
		public Stack(GamePiece p)
		{
			InitBlock();
			if (p != null)
			{
				setMap(p.getMap());
				Position = new System.Drawing.Point(new System.Drawing.Size(p.Position));
				add(p);
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < GamePiece > getPiecesIterator()
		
		/// <returns> an Enumeration of the pieces in the stack, from the bottom up This
		/// is a clone of the contents so add/remove operations during read
		/// won't affect it.
		/// </returns>
		/// <deprecated>
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Enumeration < GamePiece > getPieces()
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < GamePiece > getPiecesReverseIterator()
		
		/// <summary> Return an enumeration of the pieces in the start, from the top down
		/// 
		/// </summary>
		/// <returns>
		/// </returns>
		/// <deprecated>
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Enumeration < GamePiece > getPiecesInReverseOrder()
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < GamePiece > getPiecesInVisibleOrderIterator()
		
		/// <summary> Returns pieces in the order in which they are visible to the player --
		/// topmost first In other words, selected pieces first, then unselected pieces
		/// from the top to the bottom.
		/// </summary>
		/// <deprecated>
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Enumeration < GamePiece > getPiecesInVisibleOrder()
		
		public virtual void  remove(GamePiece p)
		{
			removePieceAt(indexOf(p));
			p.Parent = null;
			if (getMap() != null)
			{
				getMap().repaint();
			}
		}
		
		protected internal virtual void  removePieceAt(int index)
		{
			if (index >= 0 && index < pieceCount)
			{
				pieceCount--;
				for (int i = index; i < pieceCount; ++i)
				{
					contents[i] = contents[i + 1];
				}
				expanded = expanded && pieceCount > 1;
			}
		}
		
		/// <summary> Perform some action on a GamePiece that has just been removed this Stack</summary>
		/// <param name="p">
		/// </param>
		/// <returns> a {@link Command} that performs the equivalent action when executed
		/// </returns>
		public virtual Command pieceRemoved(GamePiece p)
		{
			return null;
		}
		
		protected internal virtual void  insertPieceAt(GamePiece p, int index)
		{
			if (index < 0)
			{
				index = 0;
			}
			else if (index > pieceCount)
			{
				index = pieceCount;
			}
			
			if (pieceCount >= contents.Length)
			{
				GamePiece[] newContents = new GamePiece[contents.Length + INCR];
				Array.Copy(contents, 0, newContents, 0, pieceCount);
				contents = newContents;
			}
			
			for (int i = pieceCount; i > index; --i)
			{
				contents[i] = contents[i - 1];
			}
			
			contents[index] = p;
			pieceCount++;
		}
		
		public virtual void  removeAll()
		{
			pieceCount = 0;
			expanded = false;
		}
		
		public virtual int indexOf(GamePiece p)
		{
			int index = - 1;
			for (int i = 0; i < pieceCount; ++i)
			{
				if (p == contents[i])
				{
					index = i;
					break;
				}
			}
			return index;
		}
		
		public virtual GamePiece getPieceAt(int index)
		{
			return contents[index];
		}
		
		/// <summary> Adds a piece to the stack. If the piece already exists in the stack, moves
		/// it to the top
		/// 
		/// </summary>
		/// <param name="c">
		/// </param>
		public virtual void  add(GamePiece c)
		{
			insert(c, pieceCount);
		}
		
		/// <summary> Adds a GamePiece to this Stack. Slightly more efficient than
		/// {@link #insert} because it assumes the piece does not already belong to
		/// this Stack.
		/// 
		/// </summary>
		/// <param name="child">
		/// </param>
		/// <param name="index">
		/// </param>
		public virtual void  insertChild(GamePiece child, int index)
		{
			if (child.Parent != null)
			{
				child.Parent.remove(child);
			}
			else if (child.getMap() != null)
			{
				child.getMap().removePiece(child);
			}
			child.Parent = this;
			insertPieceAt(child, index);
		}
		
		/// <summary> Inserts a child GamePiece at a given index. If the child piece already
		/// belongs to this Stack, it will be repositioned to the given index.
		/// 
		/// </summary>
		/// <param name="p">
		/// </param>
		/// <param name="pos">
		/// </param>
		public virtual void  insert(GamePiece p, int pos)
		{
			if (p == null)
			{
				return ;
			}
			pos = System.Math.Max(pos, 0);
			pos = System.Math.Min(pos, pieceCount);
			int index = indexOf(p);
			if (index >= 0)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'origExpanded '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool origExpanded = isExpanded(); // Bug #2766794
				if (pos > index)
				{
					insertPieceAt(p, pos + 1);
					removePieceAt(index);
				}
				else
				{
					removePieceAt(index);
					insertPieceAt(p, pos);
				}
				setExpanded(origExpanded);
			}
			else
			{
				insertChild(p, pos);
			}
		}
		
		/// <summary> Perform some action on a GamePiece that has just been added to this Stack</summary>
		/// <param name="p">
		/// </param>
		/// <returns> a {@link Command} that performs the equivalent action when executed
		/// </returns>
		public virtual Command pieceAdded(GamePiece p)
		{
			return null;
		}
		
		/// <summary> If the <code>obs</code> parameter is a {@link Map}, delegate drawing of
		/// this Stack to the {@link StackMetrics} of that Map. If <code>obs</code>
		/// is not a Map, use the default StackMetrics
		/// 
		/// </summary>
		/// <seealso cref="StackMetrics.draw">
		/// </seealso>
		/// <seealso cref="getDefaultMetrics">
		/// </seealso>
		public virtual void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			if (obs is Map.View)
			{
				((Map.View) obs).getMap().getStackMetrics().draw(this, g, x, y, obs, zoom);
			}
			else
			{
				getDefaultMetrics().draw(this, g, x, y, obs, zoom);
			}
		}
		
		/// <summary> Return a comma-separated list of the names of the pieces in this Stack</summary>
		public virtual System.String getName(bool localized)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'val '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder val = new StringBuilder();
			//UPGRADE_NOTE: Final was removed from the declaration of 'visibleFilter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PieceIterator visibleFilter = PieceIterator.visible(getPiecesReverseIterator());
			while (visibleFilter.hasMoreElements())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GamePiece p = visibleFilter.nextPiece();
				val.append(localized?p.LocalizedName:p.getName());
				if (val.length() > 0 && visibleFilter.hasMoreElements())
				{
					val.append(", ");
				}
			}
			return val.toString();
		}
		
		public virtual System.String getName()
		{
			return getName(false);
		}
		
		public virtual System.Drawing.Rectangle boundingBox()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle r = new System.Drawing.Rectangle();
			//UPGRADE_NOTE: Final was removed from the declaration of 'childBounds '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle[] childBounds = new System.Drawing.Rectangle[PieceCount];
			getMap().StackMetrics.getContents(this, null, null, childBounds, 0, 0);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'visibleFilter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PieceIterator visibleFilter = PieceIterator.visible(getPiecesIterator());
			while (visibleFilter.hasMoreElements())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GamePiece p = visibleFilter.nextPiece();
				SupportClass.RectangleSupport.AddRectangleToRectangle(ref r, childBounds[indexOf(p)]);
			}
			
			return r;
		}
		
		public virtual void  selectNext(GamePiece c)
		{
			KeyBuffer.Buffer.remove(c);
			if (pieceCount > 1 && indexOf(c) >= 0)
			{
				int newSelectedIndex = indexOf(c) == pieceCount - 1?pieceCount - 2:indexOf(c) + 1;
				for (int i = 0; i < pieceCount; ++i)
				{
					if (indexOf(contents[i]) == newSelectedIndex)
					{
						KeyBuffer.Buffer.add(contents[i]);
						return ;
					}
				}
			}
		}
		
		public virtual GamePiece getPieceBeneath(GamePiece p)
		{
			int index = indexOf(p);
			while (index-- > 0)
			{
				if (!true.Equals(contents[index].getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)))
				{
					return contents[index];
				}
			}
			return null;
		}
		
		public virtual GamePiece getPieceAbove(GamePiece p)
		{
			int index = indexOf(p);
			while (++index < PieceCount)
			{
				if (!true.Equals(contents[index].getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)))
				{
					return contents[index];
				}
			}
			return null;
		}
		
		/// <returns> the top visible piece in this stack 
		/// </returns>
		public virtual GamePiece topPiece()
		{
			for (int i = pieceCount - 1; i >= 0; --i)
			{
				if (!true.Equals(contents[i].getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)))
				{
					return contents[i];
				}
			}
			return null;
		}
		
		/// <returns> the top piece in this stack that is visible to the player with the
		/// given id
		/// </returns>
		/// <param name="playerId">
		/// </param>
		/// <seealso cref="GameModule.getUserId">
		/// </seealso>
		public virtual GamePiece topPiece(System.String playerId)
		{
			for (int i = pieceCount - 1; i >= 0; --i)
			{
				System.String hiddenBy = (System.String) contents[i].getProperty(VassalSharp.counters.Properties_Fields.HIDDEN_BY);
				if (hiddenBy == null || hiddenBy.Equals(playerId))
				{
					return contents[i];
				}
			}
			return null;
		}
		
		/// <returns> the bottom piece in this stack that is visible to the player with
		/// the given id
		/// </returns>
		/// <param name="playerId">
		/// </param>
		/// <seealso cref="GameModule.getUserId">
		/// </seealso>
		public virtual GamePiece bottomPiece(System.String playerId)
		{
			for (int i = 0; i < pieceCount; ++i)
			{
				System.String hiddenBy = (System.String) contents[i].getProperty(VassalSharp.counters.Properties_Fields.HIDDEN_BY);
				if (hiddenBy == null || hiddenBy.Equals(playerId))
				{
					return contents[i];
				}
			}
			return null;
		}
		
		/// <returns> the bottom visible piece in this stack 
		/// </returns>
		public virtual GamePiece bottomPiece()
		{
			for (int i = 0; i < pieceCount; ++i)
			{
				if (!true.Equals(contents[i].getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)))
				{
					return contents[i];
				}
			}
			return null;
		}
		
		/// <returns> Number of GamePieces that are visible to me
		/// </returns>
		protected internal virtual int nVisible()
		{
			int nv = 0;
			//UPGRADE_NOTE: Final was removed from the declaration of 'visibleFilter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PieceIterator visibleFilter = PieceIterator.visible(getPiecesIterator());
			while (visibleFilter.hasMoreElements())
			{
				visibleFilter.nextPiece();
				nv++;
			}
			return nv;
		}
		
		public virtual Command keyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			GamePiece p = topPiece();
			if (p != null)
			{
				return p.keyEvent(stroke);
			}
			else
			{
				return null;
			}
		}
		
		public virtual bool isExpanded()
		{
			return expanded;
		}
		
		public virtual void  setExpanded(bool b)
		{
			expanded = b && PieceCount > 1;
		}
		
		/// <summary> Compute the difference between <code>newState</code> and
		/// <code>oldState</code> and appy that difference to the current state
		/// 
		/// </summary>
		/// <param name="newState">
		/// </param>
		/// <param name="oldState">
		/// </param>
		public virtual void  mergeState(System.String newState, System.String oldState)
		{
			System.String mergedState = newState;
			if (!oldState.Equals(State))
			{
				SequenceEncoder.Decoder stNew = new SequenceEncoder.Decoder(newState, ';');
				SequenceEncoder.Decoder stOld = new SequenceEncoder.Decoder(oldState, ';');
				SequenceEncoder merge = new SequenceEncoder(';');
				merge.append(stNew.nextToken());
				stOld.nextToken();
				merge.append(stNew.nextToken());
				stOld.nextToken();
				merge.append(stNew.nextToken());
				stOld.nextToken();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ArrayList < String > newContents = new ArrayList < String >();
				while (stNew.hasMoreTokens())
				{
					newContents.add(stNew.nextToken());
				}
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ArrayList < String > oldContents = new ArrayList < String >();
				while (stOld.hasMoreTokens())
				{
					oldContents.add(stOld.nextToken());
				}
				for (int i = 0, j = PieceCount; i < j; ++i)
				{
					System.String id = getPieceAt(i).Id;
					if (!newContents.contains(id) && !oldContents.contains(id))
					{
						int index = i == 0?- 1:newContents.indexOf(getPieceAt(i - 1).Id);
						newContents.add(index + 1, id);
					}
				}
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String s: newContents)
				{
					merge.append(s);
				}
				mergedState = merge.Value;
			}
			State = mergedState;
		}
		
		public virtual void  setProperty(System.Object key, System.Object val)
		{
		}
		
		public override System.String ToString()
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return base.ToString() + "[" + getName() + "]";
		}
		
		/// <summary> Calls setProperty() on each piece in this stack
		/// 
		/// </summary>
		/// <param name="key">
		/// </param>
		/// <param name="val">
		/// </param>
		public virtual void  setPropertyOnContents(System.Object key, System.Object val)
		{
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
		}
		
		public virtual System.Object getProperty(System.Object key)
		{
			return null;
		}
		
		public virtual System.Object getLocalizedProperty(System.Object key)
		{
			return getProperty(key);
		}
		
		public virtual void  setMap(Map map)
		{
			this.map = map;
		}
		
		public virtual Map getMap()
		{
			return map;
		}
		
		public static void  setDefaultMetrics(StackMetrics s)
		{
			defaultMetrics = s;
		}
		
		public virtual StackMetrics getStackMetrics(Map m)
		{
			return m == null?getDefaultMetrics():m.StackMetrics;
		}
		
		public virtual StackMetrics getStackMetrics()
		{
			return getStackMetrics(getMap());
		}
		
		public virtual StackMetrics getDefaultMetrics()
		{
			if (defaultMetrics == null)
			{
				setDefaultMetrics(new StackMetrics());
			}
			return defaultMetrics;
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'VisibleOrderIterator' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class VisibleOrderIterator : System.Collections.IEnumerator
		{
			private void  InitBlock(Stack enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				index = Enclosing_Instance.pieceCount - 1;
			}
			private Stack enclosingInstance;
			public virtual System.Object Current
			{
				get
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'ret '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					GamePiece ret = next_Renamed_Field;
					next_Renamed_Field = findNext();
					return ret;
				}
				
			}
			public Stack Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< GamePiece >
			private GamePiece next_Renamed_Field;
			//UPGRADE_NOTE: The initialization of  'index' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private int index;
			private bool doingSelected = true;
			
			public VisibleOrderIterator(Stack enclosingInstance)
			{
				InitBlock(enclosingInstance);
				next_Renamed_Field = findNext();
			}
			
			public virtual bool MoveNext()
			{
				return next_Renamed_Field != null;
			}
			
			private GamePiece findNext()
			{
				GamePiece ret = null;
				while (index >= 0)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					GamePiece p = Enclosing_Instance.getPieceAt(index--);
					if (doingSelected ^ !true.Equals(p.getProperty(VassalSharp.counters.Properties_Fields.SELECTED)))
					{
						ret = p;
						break;
					}
				}
				
				if (ret == null && doingSelected)
				{
					doingSelected = false;
					index = Enclosing_Instance.pieceCount - 1;
					ret = findNext();
				}
				return ret;
			}
			
			//UPGRADE_NOTE: The equivalent of method 'java.util.Iterator.remove' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public virtual void  remove()
			{
				throw new System.NotSupportedException();
			}
			//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			virtual public void  Reset()
			{
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AllPieceIterator' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AllPieceIterator : System.Collections.IEnumerator
		{
			private void  InitBlock(Stack enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Stack enclosingInstance;
			public virtual System.Object Current
			{
				get
				{
					return p[index++];
				}
				
			}
			public Stack Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< GamePiece >
			private int index = 0;
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private GamePiece[] p;
			
			public AllPieceIterator(Stack enclosingInstance)
			{
				InitBlock(enclosingInstance);
				p = ArrayUtils.copyOf(Enclosing_Instance.contents, Enclosing_Instance.pieceCount);
			}
			
			public virtual bool MoveNext()
			{
				return index < p.Length;
			}
			
			//UPGRADE_NOTE: The equivalent of method 'java.util.Iterator.remove' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public virtual void  remove()
			{
				throw new System.NotSupportedException();
			}
			//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			virtual public void  Reset()
			{
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ReversePieceIterator' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class ReversePieceIterator : System.Collections.IEnumerator
		{
			private void  InitBlock(Stack enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				index = Enclosing_Instance.pieceCount - 1;
			}
			private Stack enclosingInstance;
			public virtual System.Object Current
			{
				get
				{
					return p[index--];
				}
				
			}
			public Stack Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< GamePiece >
			//UPGRADE_NOTE: The initialization of  'index' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private int index;
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private GamePiece[] p;
			
			public ReversePieceIterator(Stack enclosingInstance)
			{
				InitBlock(enclosingInstance);
				p = ArrayUtils.copyOf(Enclosing_Instance.contents, Enclosing_Instance.pieceCount);
			}
			
			public virtual bool MoveNext()
			{
				return index >= 0;
			}
			
			//UPGRADE_NOTE: The equivalent of method 'java.util.Iterator.remove' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public virtual void  remove()
			{
				throw new System.NotSupportedException();
			}
			//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			virtual public void  Reset()
			{
			}
		}
	}
}