/*
* $Id$
*
* Copyright (c) 2004 by Rodney Kinney
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
using Chatter = VassalSharp.build.module.Chatter;
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using Map = VassalSharp.build.module.Map;
using AddPiece = VassalSharp.command.AddPiece;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using MovePiece = VassalSharp.command.MovePiece;
using NullCommand = VassalSharp.command.NullCommand;
using GamePiece = VassalSharp.counters.GamePiece;
using MovementMarkable = VassalSharp.counters.MovementMarkable;
using PieceAccess = VassalSharp.counters.PieceAccess;
using Properties = VassalSharp.counters.Properties;
using Stack = VassalSharp.counters.Stack;
using FormattedString = VassalSharp.tools.FormattedString;
namespace VassalSharp.build.module.map
{
	
	/// <summary> Builds an auto-report message for a collection of Move Commands</summary>
	public class MovementReporter
	{
		virtual public Command ReportCommand
		{
			get
			{
				VassalSharp.counters.GlobalAccess.hideAll();
				
				Command c = new NullCommand();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(MoveSummary ms: movesToReport)
				{
					Map fromMap = Map.getMapById(ms.getOldMapId());
					Map toMap = Map.getMapById(ms.getNewMapId());
					format.clearProperties();
					if (fromMap == null)
					{
						format.setFormat(toMap.getCreateFormat());
					}
					else if (fromMap != toMap)
					{
						format.setFormat(toMap.getMoveToFormat());
					}
					else
					{
						format.setFormat(toMap.getMoveWithinFormat());
					}
					if (format.Format.Length == 0)
					{
						break;
					}
					format.setProperty(Map.PIECE_NAME, ms.getPieceName());
					format.setProperty(Map.LOCATION, getLocation(toMap, ms.getNewPosition()));
					if (fromMap != null)
					{
						format.setProperty(Map.OLD_MAP, fromMap.LocalizedConfigureName);
						format.setProperty(Map.OLD_LOCATION, getLocation(fromMap, ms.getOldPosition()));
					}
					format.setProperty(Map.MAP_NAME, toMap.LocalizedConfigureName);
					
					System.String moveText = format.getLocalizedText();
					
					if (moveText.Length > 0)
					{
						c = c.append(new Chatter.DisplayText(GameModule.getGameModule().getChatter(), "* " + moveText));
					}
				}
				VassalSharp.counters.GlobalAccess.revertAll();
				return c;
			}
			
		}
		protected internal FormattedString format = new FormattedString();
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < MoveSummary > movesToReport = new ArrayList < MoveSummary >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < MoveSummary > movesToMark = new ArrayList < MoveSummary >();
		
		public MovementReporter(Command moveCommand)
		{
			extractMoveCommands(moveCommand);
		}
		
		protected internal virtual void  extractMoveCommands(Command c)
		{
			MoveSummary summary = null;
			if (c is AddPiece)
			{
				AddPiece addPiece = ((AddPiece) c);
				if (shouldReport(addPiece))
				{
					summary = createMoveSummary(addPiece);
				}
			}
			else if (c is MovePiece)
			{
				MovePiece movePiece = (MovePiece) c;
				if (shouldReport(movePiece))
				{
					summary = createMoveSummary(movePiece);
				}
			}
			if (summary != null)
			{
				// Do we already have an instance that represents movement
				// between the same two map positions on the same two maps?
				int index = movesToReport.indexOf(summary);
				if (index >= 0 && c is MovePiece && shouldReport((MovePiece) c))
				{
					MoveSummary existing = movesToReport.get_Renamed(index);
					existing.append((MovePiece) c);
				}
				else
				{
					movesToReport.add(summary);
				}
				if (shouldMarkMoved(summary))
				{
					movesToMark.add(summary);
				}
			}
			Command[] sub = c.SubCommands;
			for (int i = 0; i < sub.Length; i++)
			{
				extractMoveCommands(sub[i]);
			}
		}
		
		protected internal virtual MoveSummary createMoveSummary(AddPiece c)
		{
			return new MoveSummary(c);
		}
		
		protected internal virtual MoveSummary createMoveSummary(MovePiece c)
		{
			return new MoveSummary(c);
		}
		
		
		/// <summary> Mark all pieces with the {@link MovementMarkable} trait</summary>
		/// <returns> the equivalent Command
		/// </returns>
		public virtual Command markMovedPieces()
		{
			Command c = null;
			if (!movesToMark.isEmpty())
			{
				c = new NullCommand();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(MoveSummary ms: movesToMark)
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(GamePiece p: ms.pieces)
					{
						c.append(markMoved(p));
					}
				}
			}
			return c;
		}
		
		public virtual Command markMoved(GamePiece p)
		{
			Command c = null;
			if (p is Stack)
			{
				c = new NullCommand();
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			}
			else if (p.getProperty(VassalSharp.counters.Properties_Fields.MOVED) != null)
			{
				if (p.Id != null)
				{
					ChangeTracker comm = new ChangeTracker(p);
					p.setProperty(VassalSharp.counters.Properties_Fields.MOVED, (System.Object) true);
					c = comm.ChangeCommand;
				}
			}
			return c;
		}
		
		
		protected internal virtual bool shouldMarkMoved(MoveSummary summary)
		{
			System.String option = Map.getMapById(summary.NewMapId).getAttributeValueString(Map.MARK_MOVED);
			if (option == null)
			{
				option = GlobalOptions.Instance.getAttributeValueString(GlobalOptions.MARK_MOVED);
			}
			if (GlobalOptions.ALWAYS.Equals(option))
			{
				return true;
			}
			else if (GlobalOptions.NEVER.Equals(option))
			{
				return false;
			}
			else
			{
				return true.Equals(GameModule.getGameModule().getPrefs().getValue(Map.MARK_MOVED));
			}
		}
		
		protected internal virtual bool shouldReport(AddPiece addPiece)
		{
			GamePiece target = addPiece.Target;
			if (target != null && !(target is Stack) && !true.Equals(target.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)) && !true.Equals(target.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_OTHERS)))
			{
				return true;
			}
			else
			{
				return false;
			}
		}
		
		protected internal virtual bool shouldReport(MovePiece movePiece)
		{
			GamePiece target = GameModule.getGameModule().getGameState().getPieceForId(movePiece.Id);
			if (target == null)
			{
				return false;
			}
			if (target is Stack)
			{
				GamePiece top = ((Stack) target).topPiece(null);
				return top != null;
			}
			else
			{
				return !true.Equals(target.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)) && !true.Equals(target.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_OTHERS));
			}
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual System.String getLocation(Map map, ref System.Drawing.Point p)
		{
			return map.localizedLocationName(p);
		}
		
		/// <summary> A version of the MovementReporter for reporting the movement of
		/// Invisible pieces. Replace the locations with '?'
		/// </summary>
		public class HiddenMovementReporter:MovementReporter
		{
			
			public HiddenMovementReporter(Command moveCommand):base(moveCommand)
			{
			}
			
			protected internal override MoveSummary createMoveSummary(AddPiece c)
			{
				return new HiddenMoveSummary(c);
			}
			
			protected internal override MoveSummary createMoveSummary(MovePiece c)
			{
				return new HiddenMoveSummary(c);
			}
			
			protected internal override bool shouldReport(AddPiece addPiece)
			{
				GamePiece target = addPiece.Target;
				if (target != null && !(target is Stack) && (true.Equals(target.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)) || true.Equals(target.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_OTHERS))))
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			
			protected internal override bool shouldReport(MovePiece movePiece)
			{
				GamePiece target = GameModule.getGameModule().getGameState().getPieceForId(movePiece.Id);
				if (target == null)
				{
					return false;
				}
				if (target is Stack)
				{
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
					return false;
				}
				else
				{
					return true.Equals(target.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)) || true.Equals(target.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_OTHERS));
				}
			}
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			protected internal override System.String getLocation(Map map, ref System.Drawing.Point p)
			{
				return "?";
			}
		}
		
		public class MoveSummary
		{
			virtual public System.String NewMapId
			{
				get
				{
					return newMapId;
				}
				
			}
			virtual public System.Drawing.Point NewPosition
			{
				get
				{
					return newPosition;
				}
				
			}
			virtual public System.String OldMapId
			{
				get
				{
					return oldMapId;
				}
				
			}
			virtual public System.Drawing.Point OldPosition
			{
				get
				{
					return oldPosition;
				}
				
			}
			virtual public System.String PieceName
			{
				get
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'names '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					StringBuilder names = new StringBuilder();
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
					return names.toString();
				}
				
			}
			protected internal System.String oldMapId, newMapId;
			protected internal System.Drawing.Point oldPosition, newPosition;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			protected List < GamePiece > pieces = new ArrayList < GamePiece >();
			
			public MoveSummary(AddPiece c)
			{
				GamePiece target = c.Target;
				newMapId = target.getMap().getIdentifier();
				newPosition = target.Position;
				pieces.add(target);
			}
			
			public MoveSummary(MovePiece c)
			{
				GamePiece target = GameModule.getGameModule().getGameState().getPieceForId(c.Id);
				oldMapId = c.OldMapId;
				newMapId = c.NewMapId;
				oldPosition = c.OldPosition;
				newPosition = c.NewPosition;
				if (target != null)
				{
					pieces.add(target);
				}
			}
			
			public  override bool Equals(System.Object o)
			{
				if (this == o)
					return true;
				if (!(o is MoveSummary))
					return false;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'moveSummary '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				MoveSummary moveSummary = (MoveSummary) o;
				
				if (!newPosition.Equals(moveSummary.newPosition))
					return false;
				if (!newMapId.Equals(moveSummary.newMapId))
					return false;
				if (oldMapId != null?!oldMapId.Equals(moveSummary.oldMapId):moveSummary.oldMapId != null)
					return false;
				if (oldMapId != null)
				{
					// If there is no old map, then ignore the old position for equals() purposes.
					if (!oldPosition.IsEmpty?!oldPosition.Equals(moveSummary.oldPosition):!moveSummary.oldPosition.IsEmpty)
						return false;
				}
				
				return true;
			}
			
			public override int GetHashCode()
			{
				int result;
				result = (oldMapId != null?oldMapId.GetHashCode():0);
				result = 29 * result + newMapId.GetHashCode();
				result = 29 * result + newPosition.GetHashCode();
				if (oldMapId != null)
				{
					result = 29 * result + (!oldPosition.IsEmpty?oldPosition.GetHashCode():0);
				}
				return result;
			}
			
			public virtual void  append(MovePiece movePiece)
			{
				GamePiece target = GameModule.getGameModule().getGameState().getPieceForId(movePiece.Id);
				if (target != null && !pieces.contains(target))
				{
					pieces.add(target);
				}
			}
		}
		
		public class HiddenMoveSummary:MoveSummary
		{
			override public System.String PieceName
			{
				get
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'names '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					StringBuilder names = new StringBuilder();
					bool first = true;
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
					return names.toString();
				}
				
			}
			
			public HiddenMoveSummary(AddPiece c):base(c)
			{
			}
			
			public HiddenMoveSummary(MovePiece c):base(c)
			{
			}
			
			protected internal virtual bool isInvisible(GamePiece piece)
			{
				return true.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)) || true.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_OTHERS));
			}
		}
	}
}