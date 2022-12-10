/*
* $Id$
*
* Copyright (c) 2004-2012 by Rodney Kinney, Brent Easton
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
using Chatter = VassalSharp.build.module.Chatter;
using Map = VassalSharp.build.module.Map;
using PlayerRoster = VassalSharp.build.module.PlayerRoster;
using DeckGlobalKeyCommand = VassalSharp.build.module.map.DeckGlobalKeyCommand;
using DrawPile = VassalSharp.build.module.map.DrawPile;
using StackMetrics = VassalSharp.build.module.map.StackMetrics;
using MutableProperty = VassalSharp.build.module.properties.MutableProperty;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using AddPiece = VassalSharp.command.AddPiece;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using NullCommand = VassalSharp.command.NullCommand;
using ColorConfigurer = VassalSharp.configure.ColorConfigurer;
using PropertyExpression = VassalSharp.configure.PropertyExpression;
using Localization = VassalSharp.i18n.Localization;
using Resources = VassalSharp.i18n.Resources;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using FormattedString = VassalSharp.tools.FormattedString;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using NamedKeyStrokeListener = VassalSharp.tools.NamedKeyStrokeListener;
using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
using ScrollPane = VassalSharp.tools.ScrollPane;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using WriteErrorDialog = VassalSharp.tools.WriteErrorDialog;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.counters
{
	
	/// <summary> A collection of pieces that behaves like a deck, i.e.: Doesn't move.
	/// Can't be expanded. Can be shuffled. Can be turned face-up and face-down.
	/// </summary>
	public class Deck:Stack, PlayerRoster.SideChangeListener
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassCommandEncoder' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		public class AnonymousClassCommandEncoder : CommandEncoder
		{
			public AnonymousClassCommandEncoder(Deck enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Deck enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual Command decode(System.String command)
			{
				Command c = null;
				if (command.StartsWith(VassalSharp.counters.Deck.LoadDeckCommand.PREFIX))
				{
					c = new LoadDeckCommand(Enclosing_Instance);
				}
				return c;
			}
			
			public virtual System.String encode(Command c)
			{
				System.String s = null;
				if (c is LoadDeckCommand)
				{
					s = VassalSharp.counters.Deck.LoadDeckCommand.PREFIX;
				}
				return s;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(Deck enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Deck enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				GameModule.getGameModule().sendAndLog(Enclosing_Instance.shuffle());
				Enclosing_Instance.repaintMap();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(Deck enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Deck enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				GameModule.getGameModule().sendAndLog(Enclosing_Instance.sendToDeck());
				Enclosing_Instance.repaintMap();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(Deck enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Deck enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				GameModule.getGameModule().sendAndLog(Enclosing_Instance.reverse());
				Enclosing_Instance.repaintMap();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPieceIterator' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPieceIterator:PieceIterator
		{
			public AnonymousClassPieceIterator(Deck enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Deck enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual GamePiece nextPiece()
			{
				GamePiece p = base.nextPiece();
				if (Enclosing_Instance.faceDown)
				{
					p.setProperty(VassalSharp.counters.Properties_Fields.OBSCURED_BY, VassalSharp.counters.Deck.NO_USER);
				}
				return p;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyCommand' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassKeyCommand:KeyCommand
		{
			private void  InitBlock(Deck enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			internal AnonymousClassKeyCommand(Deck enclosingInstance, System.String Param1, VassalSharp.tools.NamedKeyStroke Param2, VassalSharp.counters.GamePiece Param3):base(Param1, Param2, Param3)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				GameModule.getGameModule().sendAndLog(Enclosing_Instance.shuffle());
				Enclosing_Instance.repaintMap();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyCommand1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassKeyCommand1:KeyCommand
		{
			private void  InitBlock(Deck enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			internal AnonymousClassKeyCommand1(Deck enclosingInstance, System.String Param1, VassalSharp.tools.NamedKeyStroke Param2, VassalSharp.counters.GamePiece Param3):base(Param1, Param2, Param3)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				GameModule.getGameModule().sendAndLog(Enclosing_Instance.sendToDeck());
				Enclosing_Instance.repaintMap();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyCommand2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassKeyCommand2:KeyCommand
		{
			private void  InitBlock(Deck enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			internal AnonymousClassKeyCommand2(Deck enclosingInstance, System.String Param1, VassalSharp.tools.NamedKeyStroke Param2, VassalSharp.counters.GamePiece Param3):base(Param1, Param2, Param3)
			{
				InitBlock(enclosingInstance);
			} //$NON-NLS-1$ //$NON-NLS-2$
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Command c = Enclosing_Instance.setContentsFaceDown(!Enclosing_Instance.faceDown);
				GameModule.getGameModule().sendAndLog(c);
				Enclosing_Instance.repaintMap();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyCommand3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassKeyCommand3:KeyCommand
		{
			private void  InitBlock(Deck enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			internal AnonymousClassKeyCommand3(Deck enclosingInstance, System.String Param1, VassalSharp.tools.NamedKeyStroke Param2, VassalSharp.counters.GamePiece Param3):base(Param1, Param2, Param3)
			{
				InitBlock(enclosingInstance);
			} //$NON-NLS-1$
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Command c = Enclosing_Instance.reverse();
				GameModule.getGameModule().sendAndLog(c);
				Enclosing_Instance.repaintMap();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyCommand4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassKeyCommand4:KeyCommand
		{
			private void  InitBlock(Deck enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			internal AnonymousClassKeyCommand4(Deck enclosingInstance, System.String Param1, VassalSharp.tools.NamedKeyStroke Param2, VassalSharp.counters.GamePiece Param3):base(Param1, Param2, Param3)
			{
				InitBlock(enclosingInstance);
			} //$NON-NLS-1$
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.promptForDragCount();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyCommand5' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassKeyCommand5:KeyCommand
		{
			private void  InitBlock(Deck enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			internal AnonymousClassKeyCommand5(Deck enclosingInstance, System.String Param1, VassalSharp.tools.NamedKeyStroke Param2, VassalSharp.counters.GamePiece Param3):base(Param1, Param2, Param3)
			{
				InitBlock(enclosingInstance);
			} //$NON-NLS-1$
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.promptForNextDraw();
				Enclosing_Instance.repaintMap();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyCommand6' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassKeyCommand6:KeyCommand
		{
			private void  InitBlock(Deck enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			internal AnonymousClassKeyCommand6(Deck enclosingInstance, System.String Param1, VassalSharp.tools.NamedKeyStroke Param2, VassalSharp.counters.GamePiece Param3):base(Param1, Param2, Param3)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				GameModule.getGameModule().sendAndLog(Enclosing_Instance.saveDeck());
				Enclosing_Instance.repaintMap();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyCommand7' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassKeyCommand7:KeyCommand
		{
			private void  InitBlock(Deck enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			internal AnonymousClassKeyCommand7(Deck enclosingInstance, System.String Param1, VassalSharp.tools.NamedKeyStroke Param2, VassalSharp.counters.GamePiece Param3):base(Param1, Param2, Param3)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				GameModule.getGameModule().sendAndLog(Enclosing_Instance.loadDeck());
				Enclosing_Instance.repaintMap();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AvailablePiece' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_NOTE: Local class 'AvailablePiece' in method 'promptForNextDraw' was converted to  a nested class. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1022'"
		public class AvailablePiece : System.IComparable
		{
			private void  InitBlock(Deck enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< AvailablePiece >
			private GamePiece piece;
			
			public AvailablePiece(Deck enclosingInstance, GamePiece piece)
			{
				InitBlock(enclosingInstance);
				this.piece = piece;
			}
			
			public virtual int compareTo(AvailablePiece other)
			{
				if (other == null)
					return 1;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'otherProperty '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String otherProperty = (System.String) other.piece.getProperty(Enclosing_Instance.selectSortProperty);
				if (otherProperty == null)
					return 1;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'myProperty '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String myProperty = (System.String) piece.getProperty(Enclosing_Instance.selectSortProperty);
				if (myProperty == null)
					return - 1;
				
				return - String.CompareOrdinal(otherProperty, myProperty);
			}
			
			public override System.String ToString()
			{
				return Enclosing_Instance.selectDisplayProperty.getText(piece);
			}
			
			public  override bool Equals(System.Object o)
			{
				if (!(o is AvailablePiece))
					return false;
				return ((AvailablePiece) o).piece.Equals(piece);
			}
			//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			virtual public System.Int32 CompareTo(System.Object obj)
			{
				return 0;
			}
			//UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1306'"
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener3
		{
			public AnonymousClassActionListener3(System.Windows.Forms.ListBox list, VassalSharp.counters.Deck.AvailablePiece[] pieces, System.Windows.Forms.Form d, Deck enclosingInstance)
			{
				InitBlock(list, pieces, d, enclosingInstance);
			}
			private void  InitBlock(System.Windows.Forms.ListBox list, VassalSharp.counters.Deck.AvailablePiece[] pieces, System.Windows.Forms.Form d, Deck enclosingInstance)
			{
				this.list = list;
				this.pieces = pieces;
				this.d = d;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable list was copied into class AnonymousClassActionListener3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.ListBox list;
			//UPGRADE_NOTE: Final variable pieces was copied into class AnonymousClassActionListener3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.counters.Deck.AvailablePiece[] pieces;
			//UPGRADE_NOTE: Final variable d was copied into class AnonymousClassActionListener3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Form d;
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_TODO: Method 'javax.swing.JList.getSelectedIndices' was converted to 'System.Windows.Forms.ListBox.SelectedIndices' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListgetSelectedIndices'"
				int[] selection = list.SelectedIndices;
				if (selection.Length > 0)
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					nextDraw = new ArrayList < GamePiece >();
					for (int i = 0; i < selection.Length; ++i)
					{
						nextDraw.add(pieces[selection[i]].piece);
					}
				}
				else
				{
					nextDraw = null;
				}
				d.Dispose();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener4
		{
			public AnonymousClassActionListener4(System.Windows.Forms.Form d, Deck enclosingInstance)
			{
				InitBlock(d, enclosingInstance);
			}
			private void  InitBlock(System.Windows.Forms.Form d, Deck enclosingInstance)
			{
				this.d = d;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable d was copied into class AnonymousClassActionListener4. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Form d;
			private Deck enclosingInstance;
			public Deck Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				d.Dispose();
			}
		}
		private void  InitBlock()
		{
			selectDisplayProperty = new FormattedString("$" + BasicPiece.BASIC_NAME + "$");
			countProperty = new VassalSharp.build.module.properties.Impl("", this);
			countExpressions = new CountExpression[0];
			commandEncoder = new AnonymousClassCommandEncoder(this);
			ChangeTracker track = new ChangeTracker(this);
			removeAll();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(GamePiece child: c)
			{
				insertChild(child, pieceCount);
			}
			return track.ChangeCommand;
			ChangeTracker track = new ChangeTracker(this);
			removeAll();
			while (it.hasNext())
			{
				GamePiece child = it.next();
				insertChild(child, pieceCount);
			}
			return track.ChangeCommand;
		}
		virtual public PropertySource PropertySource
		{
			set
			{
				propertySource = value;
				if (globalCommands != null)
				{
					for (int i = 0; i < globalCommands.size(); i++)
					{
						globalCommands.get_Renamed(i).PropertySource = propertySource;
					}
				}
			}
			
		}
		virtual protected internal System.String[] GlobalCommands
		{
			get
			{
				System.String[] commands = new System.String[globalCommands.size()];
				for (int i = 0; i < globalCommands.size(); i++)
				{
					commands[i] = globalCommands.get_Renamed(i).encode();
				}
				return commands;
			}
			
			set
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				globalCommands = new ArrayList < DeckGlobalKeyCommand >(commands.length);
				for (int i = 0; i < value.Length; i++)
				{
					globalCommands.add(new DeckGlobalKeyCommand(value[i], propertySource));
				}
			}
			
		}
		virtual public System.String FaceDownOption
		{
			get
			{
				return faceDownOption;
			}
			
			set
			{
				this.faceDownOption = value;
				faceDown = !value.Equals(NEVER);
			}
			
		}
		/// <returns> true if cards are turned face up when drawn from this deck
		/// </returns>
		virtual public bool DrawFaceUp
		{
			get
			{
				return drawFaceUp;
			}
			
			set
			{
				this.drawFaceUp = value;
			}
			
		}
		virtual public System.Drawing.Size Size
		{
			get
			{
				return size;
			}
			
			set
			{
				this.size = value;
			}
			
		}
		virtual public System.String ShuffleOption
		{
			get
			{
				return shuffleOption;
			}
			
			set
			{
				this.shuffleOption = value;
			}
			
		}
		virtual public bool Shuffle
		{
			get
			{
				return shuffle_Renamed_Field;
			}
			
			set
			{
				this.shuffle_Renamed_Field = value;
			}
			
		}
		virtual public int MaxStack
		{
			get
			{
				return maxStack;
			}
			
			set
			{
				this.maxStack = value;
			}
			
		}
		override public int MaximumVisiblePieceCount
		{
			get
			{
				return System.Math.Min(pieceCount, maxStack);
			}
			
		}
		virtual public System.String[] CountExpressions
		{
			get
			{
				System.String[] fullstrings = new System.String[countExpressions.Length];
				for (int index = 0; index < countExpressions.Length; index++)
				{
					fullstrings[index] = countExpressions[index].FullString;
				}
				return fullstrings;
			}
			
			set
			{
				CountExpression[] c = new CountExpression[value.Length];
				int goodExpressionCount = 0;
				for (int index = 0; index < value.Length; index++)
				{
					CountExpression n = new CountExpression(value[index]);
					if (n.Name != null)
					{
						c[index] = n;
						goodExpressionCount++;
					}
				}
				
				this.countExpressions = ArrayUtils.copyOf(c, goodExpressionCount);
				while (countExpressions.Length > expressionProperties.size())
				{
					expressionProperties.add(new VassalSharp.build.module.properties.Impl("", this));
				}
				for (int i = 0; i < countExpressions.Length; i++)
				{
					expressionProperties.get_Renamed(i).setPropertyName(deckName + "_" + countExpressions[i].Name);
				}
			}
			
		}
		virtual public System.String FaceDownMsgFormat
		{
			get
			{
				return faceDownMsgFormat;
			}
			
			set
			{
				this.faceDownMsgFormat = value;
			}
			
		}
		virtual public System.String ReverseMsgFormat
		{
			get
			{
				return reverseMsgFormat;
			}
			
			set
			{
				this.reverseMsgFormat = value;
			}
			
		}
		virtual public System.String ReverseCommand
		{
			get
			{
				return reverseCommand;
			}
			
			set
			{
				reverseCommand = value;
			}
			
		}
		virtual public NamedKeyStroke ReverseKey
		{
			get
			{
				return reverseKey;
			}
			
			set
			{
				this.reverseKey = value;
			}
			
		}
		virtual public System.String ShuffleMsgFormat
		{
			get
			{
				return shuffleMsgFormat;
			}
			
			set
			{
				this.shuffleMsgFormat = value;
			}
			
		}
		virtual public NamedKeyStroke ShuffleKey
		{
			get
			{
				return shuffleKey;
			}
			
			set
			{
				this.shuffleKey = value;
			}
			
		}
		virtual public System.String ShuffleCommand
		{
			get
			{
				return shuffleCommand;
			}
			
			set
			{
				shuffleCommand = value;
			}
			
		}
		virtual public bool AllowMultipleDraw
		{
			get
			{
				return allowMultipleDraw;
			}
			
			set
			{
				this.allowMultipleDraw = value;
			}
			
		}
		virtual public bool AllowSelectDraw
		{
			get
			{
				return allowSelectDraw;
			}
			
			set
			{
				this.allowSelectDraw = value;
			}
			
		}
		virtual public bool ExpressionCounting
		{
			set
			{
				this.expressionCounting = value;
			}
			
		}
		virtual public bool Reversible
		{
			get
			{
				return reversible;
			}
			
			set
			{
				this.reversible = value;
			}
			
		}
		virtual public System.String DeckName
		{
			get
			{
				return deckName;
			}
			
			set
			{
				if (Localization.Instance.TranslationInProgress)
				{
					localizedDeckName = value;
				}
				else
				{
					deckName = value;
				}
				countProperty.setPropertyName(deckName + "_numPieces");
				for (int i = 0; i < countExpressions.Length; ++i)
				{
					expressionProperties.get_Renamed(i).setPropertyName(deckName + "_" + countExpressions[i].Name);
				}
			}
			
		}
		virtual public System.String LocalizedDeckName
		{
			get
			{
				return localizedDeckName == null?deckName:localizedDeckName;
			}
			
		}
		/// <summary> The popup menu text for the command that sends the entire deck to another
		/// deck
		/// 
		/// </summary>
		/// <returns>
		/// </returns>
		virtual public System.String ReshuffleCommand
		{
			get
			{
				return reshuffleCommand;
			}
			
			set
			{
				this.reshuffleCommand = value;
			}
			
		}
		virtual public NamedKeyStroke ReshuffleKey
		{
			get
			{
				return reshuffleKey;
			}
			
			set
			{
				this.reshuffleKey = value;
			}
			
		}
		/// <summary> The name of the {@link VassalSharp.build.module.map.DrawPile} to which the
		/// contents of this deck will be sent when the reshuffle command is selected
		/// </summary>
		virtual public System.String ReshuffleTarget
		{
			get
			{
				return reshuffleTarget;
			}
			
			set
			{
				this.reshuffleTarget = value;
			}
			
		}
		/// <summary> The message to send to the chat window when the deck is reshuffled to
		/// another deck
		/// 
		/// </summary>
		/// <returns>
		/// </returns>
		virtual public System.String ReshuffleMsgFormat
		{
			get
			{
				return reshuffleMsgFormat;
			}
			
			set
			{
				this.reshuffleMsgFormat = value;
			}
			
		}
		virtual public bool HotkeyOnEmpty
		{
			get
			{
				return hotkeyOnEmpty;
			}
			
			set
			{
				hotkeyOnEmpty = value;
			}
			
		}
		virtual public NamedKeyStroke NamedEmptyKey
		{
			get
			{
				return emptyKey;
			}
			
		}
		virtual public bool RestrictOption
		{
			get
			{
				return restrictOption;
			}
			
			set
			{
				this.restrictOption = value;
			}
			
		}
		virtual public PropertyExpression RestrictExpression
		{
			get
			{
				return restrictExpression;
			}
			
			set
			{
				this.restrictExpression = value;
			}
			
		}
		override public System.String Type
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SequenceEncoder se = new SequenceEncoder(';');
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				se.append(drawOutline).append(ColorConfigurer.colorToString(ref outlineColor)).append(System.Convert.ToString(size.Width)).append(System.Convert.ToString(size.Height)).append(faceDownOption).append(shuffleOption).append(System.Convert.ToString(allowMultipleDraw)).append(System.Convert.ToString(allowSelectDraw)).append(System.Convert.ToString(reversible)).append(reshuffleCommand).append(reshuffleTarget).append(reshuffleMsgFormat).append(deckName).append(shuffleMsgFormat).append(reverseMsgFormat).append(faceDownMsgFormat).append(drawFaceUp).append(persistable).append(shuffleKey).append(reshuffleKey).append(System.Convert.ToString(maxStack)).append(CountExpressions).append(expressionCounting).append(GlobalCommands).append(hotkeyOnEmpty).append(emptyKey).append(selectDisplayProperty.Format).append(selectSortProperty).append(restrictOption).append(restrictExpression).append(shuffleCommand).append(reverseCommand).append(reverseKey);
				return ID + se.Value;
			}
			
		}
		override public System.String State
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SequenceEncoder se = new SequenceEncoder(';');
				se.append(getMap() == null?"null":getMap().getIdentifier()).append(Position.X).append(Position.Y); //$NON-NLS-1$
				se.append(faceDown);
				//UPGRADE_NOTE: Final was removed from the declaration of 'se2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SequenceEncoder se2 = new SequenceEncoder(',');
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				if (se2.Value != null)
				{
					se.append(se2.Value);
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
				
				Map m = null;
				if (!"null".Equals(mapId))
				{
					//$NON-NLS-1$
					m = Map.getMapById(mapId);
					if (m == null)
					{
						ErrorDialog.dataError(new BadDataReport("No such map", mapId, null));
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
				
				faceDown = "true".Equals(st.nextToken()); //$NON-NLS-1$
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final ArrayList < GamePiece > l = new ArrayList < GamePiece >();
				if (st.hasMoreTokens())
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'st2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SequenceEncoder.Decoder st2 = new SequenceEncoder.Decoder(st.nextToken(), ',');
					while (st2.hasMoreTokens())
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						GamePiece p = GameModule.getGameModule().getGameState().getPieceForId(st2.nextToken());
						if (p != null)
						{
							l.add(p);
						}
					}
				}
				setContents(l);
				commands = null; // Force rebuild of popup menu
			}
			
		}
		virtual public bool DrawOutline
		{
			get
			{
				return drawOutline;
			}
			
			set
			{
				this.drawOutline = value;
			}
			
		}
		virtual public System.Drawing.Color OutlineColor
		{
			get
			{
				return outlineColor;
			}
			
			set
			{
				this.outlineColor = value;
			}
			
		}
		virtual public bool FaceDown
		{
			get
			{
				return faceDown;
			}
			
			set
			{
				this.faceDown = value;
			}
			
		}
		/// <summary> The color used to draw boxes representing cards underneath the top one. If
		/// null, then draw each card normally for face-up decks, and duplicate the top
		/// card for face-down decks
		/// 
		/// </summary>
		/// <returns>
		/// </returns>
		virtual protected internal System.Drawing.Color BlankColor
		{
			get
			{
				System.Drawing.Color c = System.Drawing.Color.White;
				if (getMap() != null)
				{
					c = getMap().StackMetrics.BlankColor;
				}
				return c;
			}
			
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				return boundingBox();
			}
			
		}
		virtual protected internal KeyCommand[] KeyCommands
		{
			get
			{
				if (commands == null)
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					ArrayList < KeyCommand > l = new ArrayList < KeyCommand >();
					KeyCommand c = null;
					if (USE_MENU.Equals(shuffleOption))
					{
						c = new AnonymousClassKeyCommand(this, shuffleCommand, ShuffleKey, this);
						l.add(c);
					}
					if (reshuffleCommand.Length > 0)
					{
						c = new AnonymousClassKeyCommand1(this, reshuffleCommand, ReshuffleKey, this);
						l.add(c);
					}
					if (USE_MENU.Equals(faceDownOption))
					{
						KeyCommand faceDownAction = new AnonymousClassKeyCommand2(this, faceDown?Resources.getString("Deck.face_up"):Resources.getString("Deck.face_down"), NamedKeyStroke.NULL_KEYSTROKE, this);
						l.add(faceDownAction);
					}
					if (reversible)
					{
						c = new AnonymousClassKeyCommand3(this, reverseCommand, NamedKeyStroke.NULL_KEYSTROKE, this);
						l.add(c);
					}
					if (allowMultipleDraw)
					{
						c = new AnonymousClassKeyCommand4(this, Resources.getString("Deck.draw_multiple"), NamedKeyStroke.NULL_KEYSTROKE, this);
						l.add(c);
					}
					if (allowSelectDraw)
					{
						c = new AnonymousClassKeyCommand5(this, Resources.getString("Deck.draw_specific"), NamedKeyStroke.NULL_KEYSTROKE, this);
						l.add(c);
					}
					if (persistable)
					{
						c = new AnonymousClassKeyCommand6(this, Resources.getString(Resources.SAVE), NamedKeyStroke.NULL_KEYSTROKE, this);
						l.add(c);
						c = new AnonymousClassKeyCommand7(this, Resources.getString(Resources.LOAD), NamedKeyStroke.NULL_KEYSTROKE, this);
						l.add(c);
					}
					
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(DeckGlobalKeyCommand cmd: globalCommands)
					{
						l.add(cmd.getKeyCommand(this));
					}
					
					commands = l.toArray(new KeyCommand[l.size()]);
				}
				
				for (int i = 0; i < commands.Length; ++i)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.getValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActiongetValue_javalangString'"
					//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
					if (Resources.getString("Deck.face_up").Equals(commands[i].getValue(Action.NAME)) && !faceDown)
					{
						//$NON-NLS-1$
						//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
						//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
						commands[i].putValue(Action.NAME, Resources.getString("Deck.face_down")); //$NON-NLS-1$
					}
					else
					{
						//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.getValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActiongetValue_javalangString'"
						//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
						if (Resources.getString("Deck.face_down").Equals(commands[i].getValue(Action.NAME)) && faceDown)
						{
							//$NON-NLS-1$
							//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
							//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
							commands[i].putValue(Action.NAME, Resources.getString("Deck.face_up")); //$NON-NLS-1$
						}
					}
				}
				return commands;
			}
			
		}
		/// <summary>Return true if this deck can be saved to and loaded from a file on disk </summary>
		virtual public bool Persistable
		{
			get
			{
				return persistable;
			}
			
			set
			{
				this.persistable = value;
			}
			
		}
		private System.IO.FileInfo SaveFileName
		{
			get
			{
				FileChooser fc = GameModule.getGameModule().getFileChooser();
				System.IO.FileInfo sf = fc.SelectedFile;
				if (sf != null)
				{
					System.String name = sf.FullName;
					if (name != null)
					{
						int index = name.LastIndexOf('.');
						if (index > 0)
						{
							name = name.Substring(0, (index) - (0)) + ".sav"; //$NON-NLS-1$
							fc.SelectedFile = new System.IO.FileInfo(name);
						}
					}
				}
				
				if (fc.showSaveDialog(map.getView()) != FileChooser.APPROVE_OPTION)
					return null;
				
				System.IO.FileInfo outputFile = fc.SelectedFile;
				bool tmpBool;
				if (System.IO.File.Exists(outputFile.FullName))
					tmpBool = true;
				else
					tmpBool = System.IO.Directory.Exists(outputFile.FullName);
				//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.NO_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				if (outputFile != null && tmpBool && shouldConfirmOverwrite() && (int) System.Windows.Forms.DialogResult.No == JOptionPane.showConfirmDialog(GameModule.getGameModule().getFrame(), Resources.getString("Deck.overwrite", outputFile.Name), Resources.getString("Deck.file_exists"), (int) System.Windows.Forms.MessageBoxButtons.YesNo))
				{
					outputFile = null;
				}
				
				return outputFile;
			}
			
		}
		private System.IO.FileInfo LoadFileName
		{
			get
			{
				FileChooser fc = GameModule.getGameModule().getFileChooser();
				fc.selectDotSavFile();
				if (fc.showOpenDialog(map.getView()) != FileChooser.APPROVE_OPTION)
					return null;
				return fc.SelectedFile;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Return the number of cards to be returned by next call to
		/// {@link #drawCards()}.
		/// </summary>
		/// <summary> Set the number of cards to be returned by next call to
		/// {@link #drawCards()}.
		/// 
		/// </summary>
		/// <param name="dragCount">
		/// </param>
		virtual public int DragCount
		{
			get
			{
				return dragCount;
			}
			
			set
			{
				this.dragCount = value;
			}
			
		}
		virtual public System.String SelectDisplayProperty
		{
			get
			{
				return selectDisplayProperty.Format;
			}
			
			set
			{
				this.selectDisplayProperty.Format = value;
			}
			
		}
		virtual public System.String SelectSortProperty
		{
			get
			{
				return selectSortProperty;
			}
			
			set
			{
				this.selectSortProperty = value;
			}
			
		}
		public const System.String ID = "deck;"; //$NON-NLS-1$
		public const System.String ALWAYS = "Always";
		public const System.String NEVER = "Never";
		public const System.String USE_MENU = "Via right-click Menu";
		public const System.String NO_USER = "nobody"; // Dummy user ID for turning
		protected internal static StackMetrics deckStackMetrics = new StackMetrics(false, 2, 2, 2, 2);
		// cards face down
		
		protected internal bool drawOutline = true;
		protected internal System.Drawing.Color outlineColor = System.Drawing.Color.Black;
		protected internal System.Drawing.Size size = new System.Drawing.Size(40, 40);
		protected internal bool shuffle_Renamed_Field = true;
		protected internal System.String faceDownOption = ALWAYS;
		protected internal System.String shuffleOption = ALWAYS;
		protected internal System.String shuffleCommand = "";
		protected internal bool allowMultipleDraw = false;
		protected internal bool allowSelectDraw = false;
		protected internal bool reversible = false;
		protected internal System.String reshuffleCommand = ""; //$NON-NLS-1$
		protected internal System.String reshuffleTarget;
		protected internal System.String reshuffleMsgFormat;
		protected internal NamedKeyStrokeListener reshuffleListener;
		protected internal NamedKeyStroke reshuffleKey;
		protected internal System.String reverseMsgFormat;
		protected internal System.String reverseCommand;
		protected internal NamedKeyStroke reverseKey;
		protected internal NamedKeyStrokeListener reverseListener;
		protected internal System.String shuffleMsgFormat;
		protected internal NamedKeyStrokeListener shuffleListener;
		protected internal NamedKeyStroke shuffleKey;
		protected internal System.String faceDownMsgFormat;
		protected internal bool drawFaceUp;
		protected internal bool persistable;
		//UPGRADE_NOTE: The initialization of  'selectDisplayProperty' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal FormattedString selectDisplayProperty;
		protected internal System.String selectSortProperty = "";
		//UPGRADE_NOTE: The initialization of  'countProperty' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal VassalSharp.build.module.properties.Impl countProperty;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < MutableProperty.Impl > expressionProperties = 
		new ArrayList < MutableProperty.Impl >();
		
		protected internal System.String deckName;
		protected internal System.String localizedDeckName;
		
		protected internal bool faceDown;
		protected internal int dragCount = 0;
		protected internal int maxStack = 10;
		//UPGRADE_NOTE: The initialization of  'countExpressions' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal CountExpression[] countExpressions;
		protected internal bool expressionCounting = false;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < GamePiece > nextDraw = null;
		protected internal KeyCommand[] commands;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < DeckGlobalKeyCommand > globalCommands = 
		new ArrayList < DeckGlobalKeyCommand >();
		protected internal bool hotkeyOnEmpty;
		protected internal NamedKeyStroke emptyKey;
		protected internal bool restrictOption;
		protected internal PropertyExpression restrictExpression = new PropertyExpression();
		protected internal PropertySource propertySource;
		
		//UPGRADE_NOTE: The initialization of  'commandEncoder' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal CommandEncoder commandEncoder;
		
		public Deck():this(ID)
		{
			PlayerRoster.addSideChangeListener(this);
		}
		
		public Deck(System.String type)
		{
			InitBlock();
			mySetType(type);
			PlayerRoster.addSideChangeListener(this);
		}
		
		public Deck(System.String type, PropertySource source):this(type)
		{
			propertySource = source;
		}
		
		public virtual void  sideChanged(System.String oldSide, System.String newSide)
		{
			updateCountsAll();
		}
		
		public virtual void  addGlobalKeyCommand(DeckGlobalKeyCommand globalCommand)
		{
			globalCommands.add(globalCommand);
		}
		
		public virtual void  removeGlobalKeyCommand(DeckGlobalKeyCommand globalCommand)
		{
			globalCommands.remove(globalCommand);
		}
		
		/// <summary> Update map-level count properties for all "expressions" of pieces that are configured
		/// to be counted.  These are held in the String[] countExpressions.
		/// </summary>
		private void  updateCountsAll()
		{
			if (!doesExpressionCounting() || getMap() == null)
			{
				return ;
			}
			//Clear out all of the registered count expressions
			for (int index = 0; index < countExpressions.Length; index++)
			{
				expressionProperties.get_Renamed(index).setPropertyValue("0"); //$NON-NLS-1$
			}
			//Increase all of the pieces with expressions specified in this deck
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
		}
		
		/// <summary> Update map-level count property for a piece located at index</summary>
		/// <param name="index,">increase
		/// </param>
		private void  updateCounts(int index, bool increase)
		{
			if (!doesExpressionCounting())
			{
				return ;
			}
			if (index >= 0 && index < contents.Length)
			{
				GamePiece p = getPieceAt(index);
				if (p == null)
				{
					//can't figure out the piece, do a full update
					updateCountsAll();
				}
				else
				{
					updateCounts(p, increase);
				}
			}
			else
			{
				//can't figure out the piece, do a full update
				updateCountsAll();
			}
		}
		
		/// <summary> Update map-level count property for a piece</summary>
		/// <param name="piece,">increase
		/// </param>
		private void  updateCounts(GamePiece p, bool increase)
		{
			if (!doesExpressionCounting() || getMap() == null)
			{
				return ;
			}
			//test all the expressions for this deck
			for (int index = 0; index < countExpressions.Length; index++)
			{
				VassalSharp.build.module.properties.Impl prop = expressionProperties.get_Renamed(index);
				FormattedString formatted = new FormattedString(countExpressions[index].Expression);
				PieceFilter f = PropertiesPieceFilter.parse(formatted.getText());
				if (f.accept(p))
				{
					System.String mapProperty = prop.getPropertyValue();
					if (mapProperty != null)
					{
						//UPGRADE_TODO: Method 'java.lang.Integer.decode' was converted to 'System.Int32.Parse' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
						int newValue = (System.Int32.Parse(mapProperty));
						if (increase)
						{
							newValue++;
						}
						else
						{
							newValue--;
						}
						prop.setPropertyValue(System.Convert.ToString(newValue));
					}
				}
			}
		}
		
		/// <summary> Set the <deckName>_numPieces property in the containing Map</summary>
		/// <param name="oldPieceCount">
		/// </param>
		protected internal virtual void  fireNumCardsProperty()
		{
			countProperty.setPropertyValue(System.Convert.ToString(pieceCount));
		}
		
		protected internal override void  insertPieceAt(GamePiece p, int index)
		{
			base.insertPieceAt(p, index);
			updateCounts(p, true);
			fireNumCardsProperty();
		}
		
		protected internal override void  removePieceAt(int index)
		{
			int startCount = pieceCount;
			updateCounts(index, false);
			base.removePieceAt(index);
			fireNumCardsProperty();
			if (hotkeyOnEmpty && emptyKey != null && startCount > 0 && pieceCount == 0)
			{
				GameModule.getGameModule().fireKeyStroke(emptyKey);
			}
		}
		
		public override void  removeAll()
		{
			base.removeAll();
			updateCountsAll();
			fireNumCardsProperty();
		}
		
		public override void  setMap(Map map)
		{
			if (map != getMap())
			{
				countProperty.removeFromContainer();
				if (map != null)
					countProperty.addTo(map);
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(MutableProperty.Impl prop: expressionProperties)
				{
					prop.removeFromContainer();
					if (map != null)
						prop.addTo(map);
				}
			}
			base.setMap(map);
			updateCountsAll();
			fireNumCardsProperty();
		}
		
		protected internal virtual void  mySetType(System.String type)
		{
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(type, ';');
			st.nextToken();
			drawOutline = st.nextBoolean(true);
			outlineColor = ColorConfigurer.stringToColor(st.nextToken("0,0,0")); //$NON-NLS-1$
			size = new System.Drawing.Size(st.nextInt(40), st.nextInt(40));
			faceDownOption = st.nextToken("Always"); //$NON-NLS-1$
			shuffleOption = st.nextToken("Always"); //$NON-NLS-1$
			allowMultipleDraw = st.nextBoolean(true);
			allowSelectDraw = st.nextBoolean(true);
			reversible = st.nextBoolean(true);
			reshuffleCommand = st.nextToken(""); //$NON-NLS-1$
			reshuffleTarget = st.nextToken(""); //$NON-NLS-1$
			reshuffleMsgFormat = st.nextToken(""); //$NON-NLS-1$
			DeckName = st.nextToken("Deck");
			shuffleMsgFormat = st.nextToken(""); //$NON-NLS-1$
			reverseMsgFormat = st.nextToken(""); //$NON-NLS-1$
			faceDownMsgFormat = st.nextToken(""); //$NON-NLS-1$
			drawFaceUp = st.nextBoolean(false);
			persistable = st.nextBoolean(false);
			shuffleKey = st.nextNamedKeyStroke(null);
			reshuffleKey = st.nextNamedKeyStroke(null);
			maxStack = st.nextInt(10);
			CountExpressions = st.nextStringArray(0);
			expressionCounting = st.nextBoolean(false);
			GlobalCommands = st.nextStringArray(0);
			hotkeyOnEmpty = st.nextBoolean(false);
			emptyKey = st.nextNamedKeyStroke(null);
			selectDisplayProperty.Format = st.nextToken("$" + BasicPiece.BASIC_NAME + "$");
			selectSortProperty = st.nextToken("");
			restrictOption = st.nextBoolean(false);
			restrictExpression.Expression = st.nextToken("");
			shuffleCommand = st.nextToken(Resources.getString("Deck.shuffle"));
			reverseCommand = st.nextToken(Resources.getString("Deck.reverse"));
			reverseKey = st.nextNamedKeyStroke(null);
			
			if (shuffleListener == null)
			{
				shuffleListener = new NamedKeyStrokeListener(new AnonymousClassActionListener(this));
				GameModule.getGameModule().addKeyStrokeListener(shuffleListener);
			}
			shuffleListener.setKeyStroke(ShuffleKey);
			
			if (reshuffleListener == null)
			{
				reshuffleListener = new NamedKeyStrokeListener(new AnonymousClassActionListener1(this));
				GameModule.getGameModule().addKeyStrokeListener(reshuffleListener);
			}
			reshuffleListener.setKeyStroke(ReshuffleKey);
			
			if (reverseListener == null)
			{
				reverseListener = new NamedKeyStrokeListener(new AnonymousClassActionListener2(this));
				GameModule.getGameModule().addKeyStrokeListener(reverseListener);
			}
			reverseListener.setKeyStroke(ReverseKey);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'myPile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			DrawPile myPile = DrawPile.findDrawPile(DeckName);
			if (myPile != null && myPile.getDeck() == null)
			{
				myPile.setDeck(this);
			}
		}
		
		public virtual bool doesExpressionCounting()
		{
			return expressionCounting;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public virtual System.Windows.Forms.KeyEventArgs getEmptyKey()
		{
			return emptyKey.KeyStroke;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public virtual void  setEmptyKey(System.Windows.Forms.KeyEventArgs k)
		{
			emptyKey = new NamedKeyStroke(k);
		}
		
		public virtual void  setEmptyKey(NamedKeyStroke k)
		{
			emptyKey = k;
		}
		
		/// <summary> Does the specified GamePiece meet the rules to be contained
		/// in this Deck.
		/// 
		/// </summary>
		/// <param name="piece">
		/// </param>
		/// <returns>
		/// </returns>
		public virtual bool mayContain(GamePiece piece)
		{
			if (!restrictOption || restrictExpression.Null)
			{
				return true;
			}
			else
			{
				return restrictExpression.accept(piece);
			}
		}
		
		/// <summary>Shuffle the contents of the Deck </summary>
		public virtual Command shuffle()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GamePiece[] a = new GamePiece[pieceCount];
			Array.Copy(contents, 0, a, 0, pieceCount);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < GamePiece > l = Arrays.asList(a);
			DragBuffer.Buffer.clear();
			Collections.shuffle(l, GameModule.getGameModule().getRNG());
			return setContents(l).append(reportCommand(shuffleMsgFormat, Resources.getString("Deck.shuffle"))); //$NON-NLS-1$
		}
		
		/// <summary> Return an iterator of pieces to be drawn from the Deck. Normally, a random
		/// piece will be drawn, but if the Deck supports it, the user may have
		/// specified a particular set of pieces or a fixed number of pieces to select
		/// with the next draw.
		/// </summary>
		public virtual PieceIterator drawCards()
		{;
			if (nextDraw != null)
			{
				it = nextDraw.iterator();
			}
			else if (PieceCount == 0)
			{
				it = Collections < GamePiece > emptyList().iterator();
			}
			else
			{
				int count = System.Math.Max(dragCount, System.Math.Min(1, PieceCount));
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final ArrayList < GamePiece > pieces = new ArrayList < GamePiece >();
				if (ALWAYS.Equals(shuffleOption))
				{
					// Instead of shuffling the entire deck, just pick <b>count</b> random elements
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					final ArrayList < Integer > indices = new ArrayList < Integer >();
					for (int i = 0; i < PieceCount; ++i)
					{
						indices.add(i);
					}
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'rng '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Random rng = GameModule.getGameModule().getRNG();
					
					while (count-- > 0 && indices.size() > 0)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'i '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						int i = rng.nextInt(indices.size());
						//UPGRADE_NOTE: Final was removed from the declaration of 'index '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						int index = indices.get_Renamed(i);
						indices.remove(i);
						//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						GamePiece p = getPieceAt(index);
						pieces.add(p);
					}
				}
				else
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					final Iterator < GamePiece > i = getPiecesReverseIterator();
					while (count-- > 0 && i.hasNext())
						pieces.add(i.next());
				}
				it = pieces.iterator();
			}
			dragCount = 0;
			nextDraw = null;
			return new AnonymousClassPieceIterator(this, it);
		}
		
		/// <summary>Set the contents of this Deck to a Collection of GamePieces </summary>
		protected internal Command setContents;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Collection < GamePiece > c)
		
		/// <summary> Set the contents of this Deck to an Iterator of GamePieces</summary>
		/// <deprecated> Use {@link #setContents(Collection<GamePiece>)} instead.
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		protected internal Command setContents;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Iterator < GamePiece > it)
		
		public virtual Command setContentsFaceDown(bool value_Renamed)
		{
			ChangeTracker t = new ChangeTracker(this);
			Command c = new NullCommand();
			faceDown = value_Renamed;
			return t.ChangeCommand.append(c).append(reportCommand(faceDownMsgFormat, value_Renamed?Resources.getString("Deck.face_down"):Resources.getString("Deck.face_up"))); //$NON-NLS-1$ //$NON-NLS-2$
		}
		
		/// <summary>Reverse the order of the contents of the Deck </summary>
		public virtual Command reverse()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < GamePiece > list = new ArrayList < GamePiece >();
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			return setContents(list).append(reportCommand(reverseMsgFormat, Resources.getString("Deck.reverse"))); //$NON-NLS-1$
		}
		
		public override Command pieceAdded(GamePiece p)
		{
			return null;
		}
		
		public override Command pieceRemoved(GamePiece p)
		{
			ChangeTracker tracker = new ChangeTracker(p);
			p.setProperty(VassalSharp.counters.Properties_Fields.OBSCURED_TO_OTHERS, Boolean.valueOf(FaceDown && !DrawFaceUp));
			return tracker.ChangeCommand;
		}
		
		public virtual void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			int count = System.Math.Min(PieceCount, maxStack);
			GamePiece top = (nextDraw != null && nextDraw.size() > 0)?nextDraw.get_Renamed(0):topPiece();
			
			if (top != null)
			{
				System.Object owner = top.getProperty(VassalSharp.counters.Properties_Fields.OBSCURED_BY);
				top.setProperty(VassalSharp.counters.Properties_Fields.OBSCURED_BY, (System.Object) (faceDown?NO_USER:null));
				System.Drawing.Color blankColor = BlankColor;
				System.Drawing.Rectangle r = System.Drawing.Rectangle.Truncate(top.Shape.GetBounds());
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				r.Location = new System.Drawing.Point(x + (int) (zoom * (r.X)), y + (int) (zoom * (r.Y)));
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				r.Size = new System.Drawing.Size((int) (zoom * r.Width), (int) (zoom * r.Height));
				for (int i = 0; i < count - 1; ++i)
				{
					if (!blankColor.IsEmpty)
					{
						SupportClass.GraphicsManager.manager.SetColor(g, blankColor);
						//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
						g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), r.X + (int) (zoom * 2 * i), r.Y - (int) (zoom * 2 * i), r.Width, r.Height);
						SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.Black);
						//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
						g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), r.X + (int) (zoom * 2 * i), r.Y - (int) (zoom * 2 * i), r.Width, r.Height);
					}
					else if (faceDown)
					{
						//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
						top.draw(g, x + (int) (zoom * 2 * i), y - (int) (zoom * 2 * i), obs, zoom);
					}
					else
					{
						//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
						getPieceAt(count - i - 1).draw(g, x + (int) (zoom * 2 * i), y - (int) (zoom * 2 * i), obs, zoom);
					}
				}
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				top.draw(g, x + (int) (zoom * 2 * (count - 1)), y - (int) (zoom * 2 * (count - 1)), obs, zoom);
				top.setProperty(VassalSharp.counters.Properties_Fields.OBSCURED_BY, owner);
			}
			else
			{
				if (drawOutline)
				{
					System.Drawing.Rectangle r = boundingBox();
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					r.Location = new System.Drawing.Point(x + (int) (zoom * r.X), y + (int) (zoom * r.Y));
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					r.Size = new System.Drawing.Size((int) (zoom * r.Width), (int) (zoom * r.Height));
					SupportClass.GraphicsManager.manager.SetColor(g, outlineColor);
					g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), r.X, r.Y, r.Width, r.Height);
				}
			}
		}
		
		public override StackMetrics getStackMetrics()
		{
			return deckStackMetrics;
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			GamePiece top = topPiece();
			System.Drawing.Size d = top == null?size:System.Drawing.Rectangle.Truncate(top.Shape.GetBounds()).Size;
			System.Drawing.Rectangle r = new System.Drawing.Rectangle(new System.Drawing.Point(0, 0), d);
			r.Offset((- r.Width) / 2, (- r.Height) / 2);
			for (int i = 0, n = MaximumVisiblePieceCount; i < n; ++i)
			{
				r.Y -= 2;
				r.Height += 2;
				r.Width += 2;
			}
			return r;
		}
		
		public override System.Object getProperty(System.Object key)
		{
			System.Object value_Renamed = null;
			if (VassalSharp.counters.Properties_Fields.NO_STACK.Equals(key))
			{
				value_Renamed = true;
			}
			else if (VassalSharp.counters.Properties_Fields.KEY_COMMANDS.Equals(key))
			{
				value_Renamed = KeyCommands;
			}
			return value_Renamed;
		}
		
		/*
		* Format command report as per module designers setup.
		*/
		protected internal virtual Command reportCommand(System.String format, System.String commandName)
		{
			Command c = null;
			FormattedString reportFormat = new FormattedString(format);
			reportFormat.setProperty(DrawPile.DECK_NAME, LocalizedDeckName);
			reportFormat.setProperty(DrawPile.COMMAND_NAME, commandName);
			System.String rep = reportFormat.getLocalizedText();
			if (rep.Length > 0)
			{
				c = new Chatter.DisplayText(GameModule.getGameModule().getChatter(), "* " + rep); //$NON-NLS-1$
				c.execute();
			}
			
			return c;
		}
		
		public virtual void  promptForDragCount()
		{
			while (true)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'javax.swing.JOptionPane.showInputDialog' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
				System.String s = JOptionPane.showInputDialog(Resources.getString("Deck.enter_the_number")); //$NON-NLS-1$
				if (s != null)
				{
					try
					{
						dragCount = System.Int32.Parse(s);
						dragCount = System.Math.Min(dragCount, PieceCount);
						if (dragCount >= 0)
							break;
					}
					catch (System.FormatException ex)
					{
						// Ignore if user doesn't enter a number
					}
				}
				else
				{
					break;
				}
			}
		}
		
		protected internal virtual void  promptForNextDraw()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.CreateDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_boolean'"
			//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
			System.Windows.Forms.Form d = SupportClass.DialogSupport.CreateDialog((System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), map.getView()));
			d.Text = Resources.getString("Deck.draw"); //$NON-NLS-1$
			//UPGRADE_ISSUE: Method 'javax.swing.JDialog.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJDialogsetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			d.setLayout(new BoxLayout(((System.Windows.Forms.ContainerControl) d), BoxLayout.Y_AXIS));
			
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'pieces '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			AvailablePiece[] pieces = new AvailablePiece[PieceCount];
			for (int i = 0; i < pieces.Length; ++i)
			{
				pieces[pieces.Length - i - 1] = new AvailablePiece(this, getPieceAt(i));
			}
			
			if (selectSortProperty != null && selectSortProperty.Length > 0)
			{
				//UPGRADE_TODO: Method 'java.util.Arrays.sort' was converted to 'System.Array.Sort' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilArrayssort_javalangObject[]'"
				System.Array.Sort(pieces);
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'list '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Constructor 'javax.swing.JList.JList' was converted to 'System.Windows.Forms.ListBox.ListBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListJList_javalangObject[]'"
			System.Windows.Forms.ListBox temp_ListBox;
			temp_ListBox = new System.Windows.Forms.ListBox();
			temp_ListBox.Items.AddRange(pieces);
			temp_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			System.Windows.Forms.ListBox list = temp_ListBox;
			//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.ListSelectionModel.MULTIPLE_INTERVAL_SELECTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			list.SelectionMode = (System.Windows.Forms.SelectionMode) System.Windows.Forms.SelectionMode.MultiExtended;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = new ScrollPane(list);
			d.Controls.Add(temp_Control);
			System.Windows.Forms.Label temp_label2;
			temp_label2 = new System.Windows.Forms.Label();
			temp_label2.Text = Resources.getString("Deck.select_cards");
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control2;
			temp_Control2 = temp_label2;
			d.Controls.Add(temp_Control2); //$NON-NLS-1$
			System.Windows.Forms.Label temp_label4;
			temp_label4 = new System.Windows.Forms.Label();
			temp_label4.Text = Resources.getString("Deck.then_click");
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control3;
			temp_Control3 = temp_label4;
			d.Controls.Add(temp_Control3); //$NON-NLS-1$
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box box = Box.createHorizontalBox();
			System.Windows.Forms.Button b = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.OK));
			b.Click += new System.EventHandler(new AnonymousClassActionListener3(list, pieces, d, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(b);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			box.Controls.Add(b);
			b = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.CANCEL));
			b.Click += new System.EventHandler(new AnonymousClassActionListener4(d, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(b);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			box.Controls.Add(b);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			d.Controls.Add(box);
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			d.pack();
			System.Windows.Forms.Form generatedAux9 = d.Owner;
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			d.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			SupportClass.SetPropertyAsVirtual(d, "Visible", true);
		}
		
		/// <summary> Combine the contents of this Deck with the contents of the deck specified
		/// by {@link #reshuffleTarget}
		/// </summary>
		public virtual Command sendToDeck()
		{
			Command c = null;
			nextDraw = null;
			DrawPile target = DrawPile.findDrawPile(reshuffleTarget);
			if (target != null)
			{
				if (reshuffleMsgFormat.Length > 0)
				{
					c = reportCommand(reshuffleMsgFormat, reshuffleCommand);
					if (c == null)
					{
						c = new NullCommand();
					}
				}
				else
				{
					c = new NullCommand();
				}
				// move cards to deck
				int cnt = PieceCount - 1;
				for (int i = cnt; i >= 0; i--)
				{
					c.append(target.addToContents(getPieceAt(i)));
				}
			}
			return c;
		}
		
		public override bool isExpanded()
		{
			return false;
		}
		
		private bool shouldConfirmOverwrite()
		{
			//UPGRADE_TODO: Method 'java.lang.System.getProperty' was converted to 'System.Environment.GetEnvironmentVariable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangSystemgetProperty_javalangString'"
			return System.Environment.GetEnvironmentVariable("OS").Trim().ToUpper().Equals("linux".ToUpper()); //$NON-NLS-1$ //$NON-NLS-2$
		}
		
		private Command saveDeck()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Command c = new NullCommand();
			GameModule.getGameModule().warn(Resources.getString("Deck.saving_deck")); //$NON-NLS-1$
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'saveFile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.FileInfo saveFile = SaveFileName;
			try
			{
				if (saveFile != null)
				{
					saveDeck(saveFile);
					GameModule.getGameModule().warn(Resources.getString("Deck.deck_saved")); //$NON-NLS-1$
				}
				else
				{
					GameModule.getGameModule().warn(Resources.getString("Deck.save_canceled")); //$NON-NLS-1$
				}
			}
			catch (System.IO.IOException e)
			{
				WriteErrorDialog.error(e, saveFile);
			}
			return c;
		}
		
		public virtual void  saveDeck(System.IO.FileInfo f)
		{
			Command comm = new LoadDeckCommand(null);
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			
			System.IO.StreamWriter out_Renamed = null;
			try
			{
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				//UPGRADE_TODO: Constructor 'java.io.FileWriter.FileWriter' was converted to 'System.IO.StreamWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileWriterFileWriter_javaioFile'"
				//UPGRADE_TODO: Class 'java.io.FileWriter' was converted to 'System.IO.StreamWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileWriter'"
				out_Renamed = new System.IO.StreamWriter(new System.IO.StreamWriter(f.FullName, false, System.Text.Encoding.Default).BaseStream, new System.IO.StreamWriter(f.FullName, false, System.Text.Encoding.Default).Encoding);
				GameModule.getGameModule().addCommandEncoder(commandEncoder);
				out_Renamed.write(GameModule.getGameModule().encode(comm));
				GameModule.getGameModule().removeCommandEncoder(commandEncoder);
				out_Renamed.Close();
			}
			finally
			{
				IOUtils.closeQuietly(out_Renamed);
			}
		}
		
		private Command loadDeck()
		{
			Command c = new NullCommand();
			GameModule.getGameModule().warn(Resources.getString("Deck.loading_deck")); //$NON-NLS-1$
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'loadFile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.FileInfo loadFile = LoadFileName;
			try
			{
				if (loadFile != null)
				{
					c = loadDeck(loadFile);
					GameModule.getGameModule().warn(Resources.getString("Deck.deck_loaded")); //$NON-NLS-1$
				}
				else
				{
					GameModule.getGameModule().warn(Resources.getString("Deck.load_canceled")); //$NON-NLS-1$
				}
			}
			catch (System.IO.IOException e)
			{
				ReadErrorDialog.error(e, loadFile);
			}
			
			return c;
		}
		
		public virtual Command loadDeck(System.IO.FileInfo f)
		{
			System.IO.StreamReader in_Renamed = null;
			System.String ds = null;
			try
			{
				//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				//UPGRADE_TODO: Constructor 'java.io.FileReader.FileReader' was converted to 'System.IO.StreamReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				in_Renamed = new System.IO.StreamReader(new System.IO.StreamReader(f.FullName, System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(f.FullName, System.Text.Encoding.Default).CurrentEncoding);
				ds = IOUtils.toString(in_Renamed);
				in_Renamed.Close();
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
			
			GameModule.getGameModule().addCommandEncoder(commandEncoder);
			Command c = GameModule.getGameModule().decode(ds);
			GameModule.getGameModule().removeCommandEncoder(commandEncoder);
			if (c is LoadDeckCommand)
			{
				/*
				* A LoadDeckCommand doesn't specify the deck to be changed (since the
				* saved deck can be loaded into any deck) so the Command we send to other
				* players is a ChangePiece command for this deck, which we need to place
				* after the AddPiece commands for the contents
				*/
				//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ChangeTracker t = new ChangeTracker(this);
				c.execute();
				//UPGRADE_NOTE: Final was removed from the declaration of 'sub '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Command[] sub = c.SubCommands;
				c = new NullCommand();
				for (int i = 0; i < sub.Length; ++i)
				{
					c.append(sub[i]);
				}
				c.append(t.ChangeCommand);
				updateCountsAll();
			}
			else
			{
				GameModule.getGameModule().warn(Resources.getString("Deck.not_a_saved_deck", f.Name)); //$NON-NLS-1$
				c = null;
			}
			return c;
		}
		
		/// <summary> Command to set the contents of this deck from a saved file. The contents
		/// are saved with whatever id's the pieces have in the game when the deck was
		/// saved, but new copies are created when the deck is re-loaded.
		/// 
		/// </summary>
		/// <author>  rkinney
		/// 
		/// </author>
		protected internal class LoadDeckCommand:Command
		{
			virtual public System.String TargetId
			{
				get
				{
					return target == null?"":target.Id; //$NON-NLS-1$
				}
				
			}
			public const System.String PREFIX = "DECK\t"; //$NON-NLS-1$
			private Deck target;
			
			public LoadDeckCommand(Deck target)
			{
				this.target = target;
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override void  executeCommand()
			{
				target.removeAll();
				Command[] sub = SubCommands;
				for (int i = 0; i < sub.Length; i++)
				{
					if (sub[i] is AddPiece)
					{
						GamePiece p = ((AddPiece) sub[i]).Target;
						// We set the id to null so that the piece will get a new id
						// when the AddPiece command executes
						p.Id = null;
						target.add(p);
					}
				}
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override Command myUndoCommand()
			{
				return null;
			}
		}
		
		/// <summary> An object that parses expression strings from the config window</summary>
		public class CountExpression
		{
			virtual public System.String Name
			{
				get
				{
					return name;
				}
				
			}
			virtual public System.String Expression
			{
				get
				{
					return expression;
				}
				
			}
			virtual public System.String FullString
			{
				get
				{
					return fullstring;
				}
				
			}
			private System.String fullstring;
			private System.String name;
			private System.String expression;
			public CountExpression(System.String expressionString)
			{
				System.String[] split = expressionString.split("\\s*:\\s*", 2); //$NON-NLS-1$
				if (split.Length == 2)
				{
					name = split[0];
					expression = split[1];
					fullstring = expressionString;
				}
			}
		}
		
		protected internal virtual void  repaintMap()
		{
			if (map != null)
			{
				map.repaint();
			}
		}
	}
}