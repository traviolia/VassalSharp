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
using Configurable = VassalSharp.build.Configurable;
using PieceSlot = VassalSharp.build.widget.PieceSlot;
namespace VassalSharp.counters
{
	
	/// <summary> Edits an entire set of GamePieces at once</summary>
	[Serializable]
	public class MassPieceDefiner:PieceDefiner
	{
		private void  InitBlock()
		{
			GamePiece p = definers.get_Renamed(0).definer.getPiece();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < Class < ? extends GamePiece >> types = 
			new ArrayList < Class < ? extends GamePiece >>();
			while (p is Decorator)
			{
				types.add(p.GetType());
				p = ((Decorator) p).piece;
			}
			types.add(p.GetType());
			return types;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Iterator < Class < ? extends GamePiece >> i = template.iterator();
			while (p is Decorator && i.hasNext())
			{
				if (p.GetType() != i.next())
				{
					return false;
				}
				p = ((Decorator) p).piece;
			}
			return i.hasNext()?p.GetType() == i.next() && !i.hasNext():false;
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < Entry > definers;
		
		public MassPieceDefiner(Configurable top):base()
		{
			InitBlock();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			definers = new ArrayList < Entry >();
			changed = false;
			init(top);
			if (!definers.isEmpty())
			{
				setPiece(definers.get_Renamed(0).slot.getPiece());
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				List < Class < ? extends GamePiece >> template = getTemplate();
				
				for (int i = 0; i < definers.size(); ++i)
				{
					GamePiece p = definers.get_Renamed(i).definer.getPiece();
					if (!matchesTemplate(p, template))
					{
						definers.remove(i--);
					}
				}
			}
		}
		
		private void  init(Configurable c)
		{
			if (c is PieceSlot)
			{
				PieceDefiner def = new Def(this);
				def.setPiece(((PieceSlot) c).Piece);
				definers.add(new Entry((PieceSlot) c, def));
			}
			Configurable[] child = c.ConfigureComponents;
			for (int i = 0; i < child.Length; ++i)
			{
				init(child[i]);
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < Class < ? extends GamePiece >> getTemplate()
		
		private bool matchesTemplate;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(GamePiece p, 
		List < Class < ? extends GamePiece >> template)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override void  addTrait(Decorator c)
		{
			base.addTrait(c);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Entry e: definers)
			{
				e.definer.addTrait(c);
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override void  removeTrait(int index)
		{
			base.removeTrait(index);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Entry e: definers)
			{
				e.definer.removeTrait(index);
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override void  moveDecoratorUp(int index)
		{
			base.moveDecoratorUp(index);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Entry e: definers)
			{
				e.definer.moveDecoratorUp(index);
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override void  moveDecoratorDown(int index)
		{
			base.moveDecoratorDown(index);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Entry e: definers)
			{
				e.definer.moveDecoratorDown(index);
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override void  paste()
		{
			base.paste();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Entry e: definers)
			{
				e.definer.paste();
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override bool edit(int index)
		{
			bool result = base.edit(index);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Entry e: definers)
			{
				e.definer.edit(index);
			}
			return result;
		}
		
		public virtual void  save()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Entry e: definers)
			{
				e.slot.setPiece(e.definer.getPiece());
			}
		}
		
		private class Entry
		{
			private PieceSlot slot;
			private PieceDefiner definer;
			
			internal Entry(PieceSlot slot, PieceDefiner definer)
			{
				this.slot = slot;
				this.definer = definer;
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Def' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class Def:PieceDefiner
		{
			public Def(MassPieceDefiner enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(MassPieceDefiner enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MassPieceDefiner enclosingInstance;
			public MassPieceDefiner Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			protected internal override bool edit(int index)
			{
				System.Object o = Enclosing_Instance.inUseModel[index];
				if (!(o is EditablePiece))
				{
					return false;
				}
				PieceEditor template = ((EditablePiece) o).getEditor();
				EditablePiece myPiece = (EditablePiece) this.inUseModel[index];
				myPiece.mySetType(template.Type);
				if (myPiece is Decorator)
				{
					((Decorator) myPiece).mySetState(template.State);
				}
				else
				{
					myPiece.State = template.State;
				}
				return true;
			}
		}
	}
}