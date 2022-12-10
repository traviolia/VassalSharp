/*
* $Id$
*
* Copyright (c) 2005 by Rodney Kinney
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
using GameModule = VassalSharp.build.GameModule;
using GameState = VassalSharp.build.module.GameState;
using PieceSlot = VassalSharp.build.widget.PieceSlot;
using Command = VassalSharp.command.Command;
using Decorator = VassalSharp.counters.Decorator;
using GamePiece = VassalSharp.counters.GamePiece;
using PieceCloner = VassalSharp.counters.PieceCloner;
using Replace = VassalSharp.counters.Replace;
using Stack = VassalSharp.counters.Stack;
namespace VassalSharp.tools
{
	
	public class SavedGameUpdater
	{
		public SavedGameUpdater()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'last '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Configurable last = l.size() == 0?GameModule.getGameModule():l.get_Renamed(l.size() - 1);
			
			if (last is PieceSlot)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'slot '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				PieceSlot slot = (PieceSlot) last;
				
				// Resolve prototypes
				//UPGRADE_NOTE: Final was removed from the declaration of 'clone '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GamePiece clone = PieceCloner.Instance.clonePiece(slot.Piece);
				
				p.setProperty(clone.Type, ComponentPathBuilder.Instance.getId(l.toArray(new Configurable[l.size()])));
			}
			else
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'children '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Configurable[] children = last.ConfigureComponents;
				for (int i = 0; i < children.Length; ++i)
				{
					l.add(children[i]);
					findPieceSlots(l, p);
					l.remove(children[i]);
				}
			}
		}
		/// <summary> Returns a mapping of GamePiece type to the id of a PieceSlot in the module
		/// This information is exported from an old module version, then imported into a new module version to update saved games
		/// </summary>
		/// <returns>
		/// </returns>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		virtual public System.Collections.Specialized.NameValueCollection PieceSlotsMap
		{
			get
			{
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
				System.Collections.Specialized.NameValueCollection p = new System.Collections.Specialized.NameValueCollection();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ArrayList < Configurable > l = new ArrayList < Configurable >();
				findPieceSlots(l, p);
				return p;
			}
			
		}
		
		/// <summary> </summary>
		/// <param name="pieceSlot">the imported piece-slot map from an earlier version of the module
		/// </param>
		/// <param name="savedGame">the save game to update.  The file gets overwritten.
		/// </param>
		/// <throws>  IOException </throws>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual void  updateSavedGame(System.Collections.Specialized.NameValueCollection pieceSlot, System.IO.FileInfo savedGame)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'gs '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GameState gs = GameModule.getGameModule().getGameState();
			
			gs.setup(false, true);
			gs.loadGameInBackground(savedGame);
			
			// FIXME: spin locks are bad, wait on a Future instead
			while (!gs.GameStarted)
			{
				try
				{
					//UPGRADE_TODO: Method 'java.lang.Thread.sleep' was converted to 'System.Threading.Thread.Sleep' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangThreadsleep_long'"
					System.Threading.Thread.Sleep(new System.TimeSpan((System.Int64) 10000 * 100));
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
				}
			}
			
			GamePiece[] gp_array = gs.getAllPieces().toArray(new GamePiece[0]);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(GamePiece p: gp_array)
			{
				if (!(p is Stack))
				{
					System.String slotId = pieceSlot.getProperty(p.getType());
					if (slotId != null)
					{
						Configurable[] path = null;
						try
						{
							path = ComponentPathBuilder.Instance.getPath(slotId);
							if (path != null && path.Length > 0 && path[path.Length - 1] is PieceSlot)
							{
								//UPGRADE_NOTE: Final was removed from the declaration of 'slot '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
								PieceSlot slot = (PieceSlot) path[path.Length - 1];
								if (!slot.Piece.Type.Equals(p.getType()))
								{
									if (!(p is Decorator))
									{
										GameModule.getGameModule().getChatter().show("Unable to replace " + p.getName() + ": Basic piece only");
									}
									else
									{
										ReplaceTrait r = new ReplaceTrait(p, slot.Piece);
										r.replacePiece();
									}
								}
							}
						}
						// FIXME: review error message
						catch (ComponentPathBuilder.PathFormatException ex)
						{
							//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							GameModule.getGameModule().getChatter().show("Unable to replace " + p.getName() + ": " + ex.Message);
						}
					}
					else
					{
						GameModule.getGameModule().getChatter().show("Unable to find slot for " + p.getName());
						GameModule.getGameModule().getChatter().show(p.getType());
					}
				}
			}
			
			gs.saveGame(savedGame);
			gs.updateDone();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void findPieceSlots(List < Configurable > l, Properties p)
		
		private class ReplaceTrait:Replace
		{
			private GamePiece replacement;
			
			public ReplaceTrait(GamePiece original, GamePiece replacement):base(Replace.ID + "Replace;R;dummy;;0;0;true", original)
			{
				setProperty(VassalSharp.counters.Properties_Fields.OUTER, original);
				original.setProperty(VassalSharp.counters.Properties_Fields.OUTER, (System.Object) null);
				this.replacement = replacement;
			}
			
			public override GamePiece createMarker()
			{
				GamePiece marker = PieceCloner.Instance.clonePiece(replacement);
				if (matchRotation)
				{
					matchTraits(getInner(), marker);
				}
				return marker;
			}
			
			public override Command replacePiece()
			{
				return base.replacePiece();
			}
		}
	}
}