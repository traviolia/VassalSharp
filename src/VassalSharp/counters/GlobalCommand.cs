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
using GameModule = VassalSharp.build.GameModule;
using Chatter = VassalSharp.build.module.Chatter;
using Map = VassalSharp.build.module.Map;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using Command = VassalSharp.command.Command;
using NullCommand = VassalSharp.command.NullCommand;
using FormattedString = VassalSharp.tools.FormattedString;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using RecursionLimitException = VassalSharp.tools.RecursionLimitException;
using RecursionLimiter = VassalSharp.tools.RecursionLimiter;
using Loopable = VassalSharp.tools.RecursionLimiter.Loopable;
namespace VassalSharp.counters
{
	
	/// <summary> Applies a given keyboard command to all counters on a map</summary>
	public class GlobalCommand
	{
		virtual public PropertySource PropertySource
		{
			set
			{
				source = value;
			}
			
		}
		virtual public System.String ReportFormat
		{
			get
			{
				return reportFormat.Format;
			}
			
			set
			{
				this.reportFormat.Format = value;
			}
			
		}
		virtual public bool ReportSingle
		{
			get
			{
				return reportSingle;
			}
			
			set
			{
				this.reportSingle = value;
			}
			
		}
		/// <summary> Set the number of pieces to select from a deck that the command will apply to.  A value <0 means to apply to all pieces in the deck</summary>
		/// <param name="selectFromDeck">
		/// </param>
		virtual public int SelectFromDeck
		{
			get
			{
				return selectFromDeck;
			}
			
			set
			{
				this.selectFromDeck = value;
			}
			
		}
		protected internal System.Windows.Forms.KeyEventArgs keyStroke;
		protected internal bool reportSingle;
		protected internal int selectFromDeck = - 1;
		protected internal FormattedString reportFormat = new FormattedString();
		protected internal RecursionLimiter.Loopable owner;
		protected internal PropertySource source;
		
		public GlobalCommand(RecursionLimiter.Loopable l):this(l, null)
		{
		}
		
		public GlobalCommand(RecursionLimiter.Loopable l, PropertySource p)
		{
			owner = l;
			source = p;
		}
		
		public virtual void  setKeyStroke(System.Windows.Forms.KeyEventArgs keyStroke)
		{
			this.keyStroke = keyStroke;
		}
		
		public virtual void  setKeyStroke(NamedKeyStroke keyStroke)
		{
			this.keyStroke = keyStroke.KeyStroke;
		}
		
		public virtual System.Windows.Forms.KeyEventArgs getKeyStroke()
		{
			return keyStroke;
		}
		
		public virtual Command apply(Map m, PieceFilter filter)
		{
			return apply(new Map[]{m}, filter);
		}
		/// <summary> Apply the key command to all pieces that pass the given filter on all the given maps
		/// 
		/// </summary>
		/// <param name="m">
		/// </param>
		/// <param name="filter">
		/// </param>
		/// <returns> a the corresponding {@link Command}
		/// </returns>
		public virtual Command apply(Map[] m, PieceFilter filter)
		{
			Command c = new NullCommand();
			try
			{
				if (reportSingle)
				{
					Map.ChangeReportingEnabled = false;
				}
				RecursionLimiter.startExecution(owner);
				System.String reportText = reportFormat.getLocalizedText(source);
				if (reportText.Length > 0)
				{
					c = new Chatter.DisplayText(GameModule.getGameModule().getChatter(), "*" + reportText);
					c.execute();
				}
				for (int mapI = 0; mapI < m.Length; ++mapI)
				{
					Visitor visitor = new Visitor(this, c, filter, keyStroke);
					DeckVisitorDispatcher dispatcher = new DeckVisitorDispatcher(visitor);
					GamePiece[] p = m[mapI].getPieces();
					for (int i = 0; i < p.Length; ++i)
					{
						dispatcher.accept(p[i]);
					}
					visitor.Tracker.repaint();
					c = visitor.Command;
				}
			}
			catch (RecursionLimitException e)
			{
				RecursionLimiter.infiniteLoop(e);
			}
			finally
			{
				RecursionLimiter.endExecution();
				if (reportSingle)
				{
					Map.ChangeReportingEnabled = true;
				}
			}
			
			return c;
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Visitor' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		protected internal class Visitor : DeckVisitor
		{
			private void  InitBlock(GlobalCommand enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GlobalCommand enclosingInstance;
			virtual public Command Command
			{
				get
				{
					return command;
				}
				
			}
			virtual public BoundsTracker Tracker
			{
				get
				{
					return tracker;
				}
				
			}
			public GlobalCommand Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private Command command;
			private BoundsTracker tracker;
			private PieceFilter filter;
			private System.Windows.Forms.KeyEventArgs stroke;
			
			public Visitor(GlobalCommand enclosingInstance, Command command, PieceFilter filter, System.Windows.Forms.KeyEventArgs stroke)
			{
				InitBlock(enclosingInstance);
				this.command = command;
				tracker = new BoundsTracker();
				this.filter = filter;
				this.stroke = stroke;
			}
			
			public virtual System.Object visitDeck(Deck d)
			{
				System.Object target = null;
				if (Enclosing_Instance.selectFromDeck != 0)
				{
					d.DragCount = Enclosing_Instance.selectFromDeck < 0?d.PieceCount:Enclosing_Instance.selectFromDeck;
					for (PieceIterator it = d.drawCards(); it.hasMoreElements(); )
					{
						apply(it.nextPiece());
					}
				}
				return target;
			}
			
			public virtual System.Object visitStack(Stack s)
			{
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				return null;
			}
			
			public virtual System.Object visitDefault(GamePiece p)
			{
				apply(p);
				return null;
			}
			
			private void  apply(GamePiece p)
			{
				if (filter == null || filter.accept(p))
				{
					tracker.addPiece(p);
					p.setProperty(VassalSharp.counters.Properties_Fields.SNAPSHOT, PieceCloner.Instance.clonePiece(p));
					command.append(p.keyEvent(stroke));
					tracker.addPiece(p);
				}
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override int GetHashCode()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'prime '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int prime = 31;
			int result = 1;
			result = prime * result + ((keyStroke == null)?0:keyStroke.GetHashCode());
			result = prime * result + ((reportFormat == null)?0:reportFormat.GetHashCode());
			result = prime * result + (reportSingle?1231:1237);
			result = prime * result + selectFromDeck;
			return result;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public  override bool Equals(System.Object obj)
		{
			if (this == obj)
				return true;
			if (obj == null)
				return false;
			if (GetType() != obj.GetType())
				return false;
			GlobalCommand other = (GlobalCommand) obj;
			if (keyStroke == null)
			{
				if (other.keyStroke != null)
					return false;
			}
			else if (!keyStroke.Equals(other.keyStroke))
				return false;
			if (reportFormat == null)
			{
				if (other.reportFormat != null)
					return false;
			}
			else if (!reportFormat.Equals(other.reportFormat))
				return false;
			if (reportSingle != other.reportSingle)
				return false;
			if (selectFromDeck != other.selectFromDeck)
				return false;
			return true;
		}
	}
}