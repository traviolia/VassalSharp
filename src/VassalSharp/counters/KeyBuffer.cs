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
using Command = VassalSharp.command.Command;
using NullCommand = VassalSharp.command.NullCommand;
namespace VassalSharp.counters
{
	
	
	public class KeyBuffer
	{
		private void  InitBlock()
		{
			return pieces.iterator();
			return Collections.enumeration(pieces);
			Collections.sort(pieces, comp);
		}
		public static KeyBuffer Buffer
		{
			get
			{
				if (theBuffer == null)
				{
					theBuffer = new KeyBuffer();
				}
				return theBuffer;
			}
			
		}
		virtual public bool Empty
		{
			get
			{
				return pieces.Empty;
			}
			
		}
		private static KeyBuffer theBuffer;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < GamePiece > pieces;
		private BoundsTracker bounds;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private Comparator < GamePiece > pieceSorter = new PieceSorter();
		
		private KeyBuffer()
		{
			InitBlock();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			pieces = new ArrayList < GamePiece >();
			bounds = new BoundsTracker();
		}
		
		public static void  init(KeyBuffer kb)
		{
			if (theBuffer == null)
				theBuffer = kb;
		}
		
		public virtual void  add(GamePiece p)
		{
			// FIXME: should we use a HashSet or LinkedHashSet instead to make contains()
			// checks faster? Is insertion order important?
			if (p != null && !pieces.contains(p))
			{
				pieces.add(p);
				p.setProperty(VassalSharp.counters.Properties_Fields.SELECTED, (System.Object) true);
			}
		}
		
		public virtual void  clear()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(GamePiece p: pieces)
			{
				p.setProperty(VassalSharp.counters.Properties_Fields.SELECTED, null);
			}
			pieces.clear();
		}
		
		public virtual void  remove(GamePiece p)
		{
			if (p != null)
			{
				p.setProperty(VassalSharp.counters.Properties_Fields.SELECTED, (System.Object) null);
				pieces.remove(p);
			}
		}
		
		public virtual bool contains(GamePiece p)
		{
			if (p is Stack)
			{
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				return true;
			}
			else
			{
				return pieces.contains(p);
			}
		}
		
		public virtual Command keyCommand(System.Windows.Forms.KeyEventArgs stroke)
		{
			sort(pieceSorter);
			Command comm = new NullCommand();
			
			bounds.clear();
			
			// Copy contents into new list, because contents may change
			// as a result of key commands
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < GamePiece > targets = new ArrayList < GamePiece >(pieces);
			// Reverse the order if this is a "Move Up" or "Move to Bottom" keystroke
			if (targets.size() > 0)
			{
				GamePiece top = targets.get_Renamed(0);
				if (top.getMap() != null)
				{
					if (stroke.Equals(top.getMap().StackMetrics.getMoveBottomKey()) || stroke.Equals(top.getMap().StackMetrics.getMoveUpKey()))
					{
						Collections.reverse(targets);
					}
				}
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(GamePiece p: targets)
			{
				bounds.addPiece(p);
				p.setProperty(VassalSharp.counters.Properties_Fields.SNAPSHOT, PieceCloner.Instance.clonePiece(p)); // save state prior to command
				Command c2 = p.keyEvent(stroke);
				comm = comm.append(c2);
				bounds.addPiece(p);
			}
			bounds.repaint();
			return comm;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < GamePiece > getPiecesIterator()
		
		/// <deprecated> Use {@link #getPiecesIterator()} instead. 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Enumeration < GamePiece > getPieces()
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void sort(Comparator < GamePiece > comp)
		
		/// <summary> </summary>
		/// <param name="stack">
		/// </param>
		/// <returns> true if a child of the specified Stack is selected
		/// </returns>
		public virtual bool containsChild(Stack stack)
		{
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			return false;
		}
	}
}