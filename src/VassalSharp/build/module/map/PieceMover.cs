/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney, Jim Urbas
* Refactoring of DragHandler Copyright 2011 Pieter Geerkens
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
//UPGRADE_TODO: The type 'java.awt.dnd.DragSourceMotionListener' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DragSourceMotionListener = java.awt.dnd.DragSourceMotionListener;
//UPGRADE_TODO: The type 'org.apache.commons.lang.SystemUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SystemUtils = org.apache.commons.lang.SystemUtils;
using AbstractBuildable = VassalSharp.build.AbstractBuildable;
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using GameComponent = VassalSharp.build.module.GameComponent;
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using Map = VassalSharp.build.module.Map;
using Board = VassalSharp.build.module.map.boardPicker.Board;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using NullCommand = VassalSharp.command.NullCommand;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using BasicPiece = VassalSharp.counters.BasicPiece;
using BoundsTracker = VassalSharp.counters.BoundsTracker;
using Deck = VassalSharp.counters.Deck;
using DeckVisitor = VassalSharp.counters.DeckVisitor;
using DeckVisitorDispatcher = VassalSharp.counters.DeckVisitorDispatcher;
using Decorator = VassalSharp.counters.Decorator;
using DragBuffer = VassalSharp.counters.DragBuffer;
using EventFilter = VassalSharp.counters.EventFilter;
using GamePiece = VassalSharp.counters.GamePiece;
using Highlighter = VassalSharp.counters.Highlighter;
using KeyBuffer = VassalSharp.counters.KeyBuffer;
using PieceCloner = VassalSharp.counters.PieceCloner;
using PieceFinder = VassalSharp.counters.PieceFinder;
using PieceIterator = VassalSharp.counters.PieceIterator;
using PieceSorter = VassalSharp.counters.PieceSorter;
using PieceVisitorDispatcher = VassalSharp.counters.PieceVisitorDispatcher;
using Properties = VassalSharp.counters.Properties;
using Stack = VassalSharp.counters.Stack;
using LaunchButton = VassalSharp.tools.LaunchButton;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
using Op = VassalSharp.tools.imageop.Op;
namespace VassalSharp.build.module.map
{
	
	/// <summary> This is a MouseListener that moves pieces onto a Map window</summary>
	public class PieceMover:AbstractBuildable, GameComponent, System.Collections.IComparer
	{
		public PieceMover()
		{
			InitBlock();
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMovable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassMovable:VassalSharp.counters.Movable
		{
			public AnonymousClassMovable(PieceMover enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceMover enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceMover enclosingInstance;
			public PieceMover Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public override System.Object visitDeck(Deck d)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'pos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point pos = d.Position;
				//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point p = new System.Drawing.Point(pt.X - pos.X, pt.Y - pos.Y);
				if (d.Shape.IsVisible(p))
				{
					return d;
				}
				else
				{
					return null;
				}
			}
			
			public override System.Object visitDefault(GamePiece piece)
			{
				GamePiece selected = null;
				if (this.map.StackMetrics.StackingEnabled && this.map.getPieceCollection().canMerge(Enclosing_Instance.dragging, piece))
				{
					if (this.map.isLocationRestricted(pt))
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'snap '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Drawing.Point snap = this.map.snapTo(pt);
						if (piece.Position.Equals(snap))
						{
							selected = piece;
						}
					}
					else
					{
						selected = (GamePiece) base.visitDefault(piece);
					}
				}
				
				if (selected != null && DragBuffer.Buffer.contains(selected) && selected.Parent != null && selected.Parent.topPiece() == selected)
				{
					selected = null;
				}
				return selected;
			}
			
			public override System.Object visitStack(Stack s)
			{
				GamePiece selected = null;
				if (this.map.StackMetrics.StackingEnabled && this.map.getPieceCollection().canMerge(Enclosing_Instance.dragging, s) && !DragBuffer.Buffer.contains(s) && s.topPiece() != null)
				{
					if (this.map.isLocationRestricted(pt) && !s.isExpanded())
					{
						if (s.Position.Equals(this.map.snapTo(pt)))
						{
							selected = s;
						}
					}
					else
					{
						selected = (GamePiece) base.visitStack(s);
					}
				}
				return selected;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDeckVisitor' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDeckVisitor : DeckVisitor
		{
			public AnonymousClassDeckVisitor(PieceMover enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceMover enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceMover enclosingInstance;
			public PieceMover Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual System.Object visitDeck(Deck d)
			{
				DragBuffer.Buffer.clear();
				for (PieceIterator it = d.drawCards(); it.hasMoreElements(); )
				{
					DragBuffer.Buffer.add(it.nextPiece());
				}
				return null;
			}
			
			public virtual System.Object visitStack(Stack s)
			{
				DragBuffer.Buffer.clear();
				// RFE 1629255 - Only add selected pieces within the stack to the DragBuffer
				// Add whole stack if all pieces are selected - better drag cursor
				int selectedCount = 0;
				for (int i = 0; i < s.PieceCount; i++)
				{
					if (true.Equals(s.getPieceAt(i).getProperty(VassalSharp.counters.Properties_Fields.SELECTED)))
					{
						selectedCount++;
					}
				}
				
				if (((System.Boolean) GameModule.getGameModule().getPrefs().getValue(Map.MOVING_STACKS_PICKUP_UNITS)) || s.PieceCount == 1 || s.PieceCount == selectedCount)
				{
					DragBuffer.Buffer.add(s);
				}
				else
				{
					for (int i = 0; i < s.PieceCount; i++)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						GamePiece p = s.getPieceAt(i);
						if (true.Equals(p.getProperty(VassalSharp.counters.Properties_Fields.SELECTED)))
						{
							DragBuffer.Buffer.add(p);
						}
					}
				}
				// End RFE 1629255
				if (KeyBuffer.Buffer.containsChild(s))
				{
					// If clicking on a stack with a selected piece, put all selected
					// pieces in other stacks into the drag buffer
					KeyBuffer.Buffer.sort(Enclosing_Instance);
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				}
				return null;
			}
			
			public virtual System.Object visitDefault(GamePiece selected)
			{
				DragBuffer.Buffer.clear();
				if (KeyBuffer.Buffer.contains(selected))
				{
					// If clicking on a selected piece, put all selected pieces into the
					// drag buffer
					KeyBuffer.Buffer.sort(Enclosing_Instance);
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				}
				else
				{
					DragBuffer.Buffer.clear();
					DragBuffer.Buffer.add(selected);
				}
				return null;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMovable1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassMovable1:VassalSharp.counters.Movable
		{
			public AnonymousClassMovable1(PieceMover enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceMover enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceMover enclosingInstance;
			public PieceMover Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public override System.Object visitDeck(Deck d)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'pos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point pos = d.Position;
				//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point p = new System.Drawing.Point(pt.X - pos.X, pt.Y - pos.Y);
				if (d.boundingBox().Contains(p) && d.PieceCount > 0)
				{
					return d;
				}
				else
				{
					return null;
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(PieceMover enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceMover enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceMover enclosingInstance;
			public PieceMover Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GamePiece[] p = Enclosing_Instance.map.getAllPieces();
				//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Command c = new NullCommand();
				for (int i = 0; i < p.Length; ++i)
				{
					c.append(Enclosing_Instance.markMoved(p[i], false));
				}
				GameModule.getGameModule().sendAndLog(c);
				Enclosing_Instance.map.repaint();
			}
		}
		/// <summary>The Preferences key for autoreporting moves. </summary>
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(GamePiece piece: pieces)
			{
				if (piece.getProperty(VassalSharp.counters.Properties_Fields.SNAPSHOT) == null)
				{
					piece.setProperty(VassalSharp.counters.Properties_Fields.SNAPSHOT, PieceCloner.Instance.clonePiece(piece));
				}
				comm.append(piece.keyEvent(key));
			}
		}
		virtual public Command RestoreCommand
		{
			get
			{
				return null;
			}
			
		}
		private System.String MarkOption
		{
			get
			{
				System.String value_Renamed = map.getAttributeValueString(Map.MARK_MOVED);
				if (value_Renamed == null)
				{
					value_Renamed = GlobalOptions.Instance.getAttributeValueString(GlobalOptions.MARK_MOVED);
				}
				return value_Renamed;
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[]{ICON_NAME};
			}
			
		}
		virtual protected internal GamePiece OldLocation
		{
			set
			{
				if (value is Stack)
				{
					for (int i = 0; i < ((Stack) value).PieceCount; i++)
					{
						Decorator.setOldProperties(((Stack) value).getPieceAt(i));
					}
				}
				else
					Decorator.setOldProperties(value);
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< GamePiece >
		public const System.String AUTO_REPORT = "autoReport"; //$NON-NLS-1$
		public const System.String NAME = "name";
		
		public const System.String HOTKEY = "hotkey";
		
		protected internal Map map;
		protected internal System.Drawing.Point dragBegin;
		protected internal GamePiece dragging;
		protected internal LaunchButton markUnmovedButton;
		protected internal System.String markUnmovedText;
		protected internal System.String markUnmovedIcon;
		public const System.String ICON_NAME = "icon"; //$NON-NLS-1$
		protected internal System.String iconName;
		
		// Selects drag target from mouse click on the Map
		protected internal PieceFinder dragTargetSelector;
		
		// Selects piece to merge with at the drop destination
		protected internal PieceFinder dropTargetSelector;
		
		// Processes drag target  after having been selected
		protected internal PieceVisitorDispatcher selectionProcessor;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected Comparator < GamePiece > pieceSorter = new PieceSorter();
		
		public override void  addTo(Buildable b)
		{
			dragTargetSelector = createDragTargetSelector();
			dropTargetSelector = createDropTargetSelector();
			selectionProcessor = createSelectionProcessor();
			map = (Map) b;
			map.addLocalMouseListener(this);
			GameModule.getGameModule().getGameState().addGameComponent(this);
			map.setDragGestureListener(DragHandler.TheDragHandler);
			map.PieceMover = this;
			setAttribute(Map.MARK_UNMOVED_TEXT, map.getAttributeValueString(Map.MARK_UNMOVED_TEXT));
			setAttribute(Map.MARK_UNMOVED_ICON, map.getAttributeValueString(Map.MARK_UNMOVED_ICON));
		}
		
		protected internal virtual MovementReporter createMovementReporter(Command c)
		{
			return new MovementReporter(c);
		}
		
		/// <summary> When the user completes a drag-drop operation, the pieces being
		/// dragged will either be combined with an existing piece on the map
		/// or else placed on the map without stack. This method returns a
		/// {@link PieceFinder} instance that determines which {@link GamePiece}
		/// (if any) to combine the being-dragged pieces with.
		/// 
		/// </summary>
		/// <returns>
		/// </returns>
		protected internal virtual PieceFinder createDropTargetSelector()
		{
			return new AnonymousClassMovable(this);
		}
		
		/// <summary> When the user clicks on the map, a piece from the map is selected by
		/// the dragTargetSelector. What happens to that piece is determined by
		/// the {@link PieceVisitorDispatcher} instance returned by this method.
		/// The default implementation does the following: If a Deck, add the top
		/// piece to the drag buffer If a stack, add it to the drag buffer.
		/// Otherwise, add the piece and any other multi-selected pieces to the
		/// drag buffer.
		/// 
		/// </summary>
		/// <seealso cref="createDragTargetSelector">
		/// </seealso>
		/// <returns>
		/// </returns>
		protected internal virtual PieceVisitorDispatcher createSelectionProcessor()
		{
			return new DeckVisitorDispatcher(new AnonymousClassDeckVisitor(this));
		}
		
		/// <summary> Returns the {@link PieceFinder} instance that will select a
		/// {@link GamePiece} for processing when the user clicks on the map.
		/// The default implementation is to return the first piece whose shape
		/// contains the point clicked on.
		/// 
		/// </summary>
		/// <returns>
		/// </returns>
		protected internal virtual PieceFinder createDragTargetSelector()
		{
			return new AnonymousClassMovable1(this);
		}
		
		public virtual void  setup(bool gameStarting)
		{
			if (gameStarting)
			{
				initButton();
			}
		}
		
		private System.Drawing.Image loadIcon(System.String name)
		{
			if (name == null || name.Length == 0)
				return null;
			return Op.load(name).getImage();
		}
		
		protected internal virtual void  initButton()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String value_Renamed = MarkOption;
			if (GlobalOptions.PROMPT.Equals(value_Renamed))
			{
				System.Boolean tempAux = true;
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				BooleanConfigurer config = new BooleanConfigurer(Map.MARK_MOVED, "Mark Moved Pieces", ref tempAux);
				GameModule.getGameModule().getPrefs().addOption(config);
			}
			
			if (!GlobalOptions.NEVER.Equals(value_Renamed))
			{
				if (markUnmovedButton == null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'al '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					ActionListener al = new AnonymousClassActionListener(this);
					
					markUnmovedButton = new LaunchButton("", NAME, HOTKEY, Map.MARK_UNMOVED_ICON, al);
					
					System.Drawing.Image img = null;
					if (iconName != null && iconName.Length > 0)
					{
						img = loadIcon(iconName);
						if (img != null)
						{
							markUnmovedButton.setAttribute(Map.MARK_UNMOVED_ICON, iconName);
						}
					}
					
					if (img == null)
					{
						img = loadIcon(markUnmovedIcon);
						if (img != null)
						{
							markUnmovedButton.setAttribute(Map.MARK_UNMOVED_ICON, markUnmovedIcon);
						}
					}
					
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentY_float'"
					markUnmovedButton.setAlignmentY(0.0F);
					markUnmovedButton.Text = markUnmovedText;
					markUnmovedButton.setToolTipText(map.getAttributeValueString(Map.MARK_UNMOVED_TOOLTIP));
					System.Windows.Forms.ToolBarButton temp_ToolBarButton;
					temp_ToolBarButton = new System.Windows.Forms.ToolBarButton(markUnmovedButton.Text);
					temp_ToolBarButton.ToolTipText = SupportClass.ToolTipSupport.getToolTipText(markUnmovedButton);
					map.ToolBar.Buttons.Add(temp_ToolBarButton);
					if (markUnmovedButton.Image != null)
					{
						map.ToolBar.ImageList.Images.Add(markUnmovedButton.Image);
						temp_ToolBarButton.ImageIndex = map.ToolBar.ImageList.Images.Count - 1;
					}
					temp_ToolBarButton.Tag = markUnmovedButton;
					markUnmovedButton.Tag = temp_ToolBarButton;
				}
			}
			else if (markUnmovedButton != null)
			{
				map.ToolBar.Buttons.Remove((System.Windows.Forms.ToolBarButton) markUnmovedButton.Tag);
				markUnmovedButton = null;
			}
		}
		
		public override System.String getAttributeValueString(System.String key)
		{
			return ICON_NAME.Equals(key)?iconName:null;
		}
		
		public override void  setAttribute(System.String key, System.Object value_Renamed)
		{
			if (ICON_NAME.Equals(key))
			{
				iconName = ((System.String) value_Renamed);
			}
			else if (Map.MARK_UNMOVED_TEXT.Equals(key))
			{
				if (markUnmovedButton != null)
				{
					markUnmovedButton.setAttribute(NAME, value_Renamed);
				}
				markUnmovedText = ((System.String) value_Renamed);
			}
			else if (Map.MARK_UNMOVED_ICON.Equals(key))
			{
				if (markUnmovedButton != null)
				{
					markUnmovedButton.setAttribute(Map.MARK_UNMOVED_ICON, value_Renamed);
				}
				markUnmovedIcon = ((System.String) value_Renamed);
			}
		}
		
		protected internal virtual bool isMultipleSelectionEvent(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			return (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift);
		}
		
		/// <summary>Invoked just before a piece is moved </summary>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual Command movedPiece(GamePiece p, ref System.Drawing.Point loc)
		{
			OldLocation = p;
			Command c = null;
			if (!loc.Equals(p.Position))
			{
				c = markMoved(p, true);
			}
			if (p.Parent != null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'removedCommand '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Command removedCommand = p.Parent.pieceRemoved(p);
				c = c == null?removedCommand:c.append(removedCommand);
			}
			return c;
		}
		
		public virtual Command markMoved(GamePiece p, bool hasMoved)
		{
			if (GlobalOptions.NEVER.Equals(MarkOption))
			{
				hasMoved = false;
			}
			
			Command c = new NullCommand();
			if (!hasMoved || shouldMarkMoved())
			{
				if (p is Stack)
				{
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				}
				else if (p.getProperty(VassalSharp.counters.Properties_Fields.MOVED) != null)
				{
					if (p.Id != null)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'comm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						ChangeTracker comm = new ChangeTracker(p);
						p.setProperty(VassalSharp.counters.Properties_Fields.MOVED, (System.Object) (hasMoved?true:false));
						c = comm.ChangeCommand;
					}
				}
			}
			return c;
		}
		
		protected internal virtual bool shouldMarkMoved()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'option '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String option = MarkOption;
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
		
		/// <summary> Moves pieces in DragBuffer to point p by generating a Command for
		/// each element in DragBuffer
		/// 
		/// </summary>
		/// <param name="map">Map
		/// </param>
		/// <param name="p">Point mouse released
		/// </param>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual Command movePieces(Map map, ref System.Drawing.Point p)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < GamePiece > allDraggedPieces = new ArrayList < GamePiece >();
			//UPGRADE_NOTE: Final was removed from the declaration of 'it '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PieceIterator it = DragBuffer.Buffer.Iterator;
			if (!it.hasMoreElements())
				return null;
			
			System.Drawing.Point offset = System.Drawing.Point.Empty;
			Command comm = new NullCommand();
			//UPGRADE_NOTE: Final was removed from the declaration of 'tracker '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			BoundsTracker tracker = new BoundsTracker();
			// Map of Point->List<GamePiece> of pieces to merge with at a given
			// location. There is potentially one piece for each Game Piece Layer.
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final HashMap < Point, List < GamePiece >> mergeTargets = 
			new HashMap < Point, List < GamePiece >>();
			while (it.hasMoreElements())
			{
				dragging = it.nextPiece();
				tracker.addPiece(dragging);
				/*
				* Take a copy of the pieces in dragging.
				* If it is a stack, it is cleared by the merging process.
				*/
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final ArrayList < GamePiece > draggedPieces = new ArrayList < GamePiece >(0);
				if (dragging is Stack)
				{
					int size = ((Stack) dragging).PieceCount;
					for (int i = 0; i < size; i++)
					{
						draggedPieces.add(((Stack) dragging).getPieceAt(i));
					}
				}
				else
				{
					draggedPieces.add(dragging);
				}
				
				if (!offset.IsEmpty)
				{
					p = new System.Drawing.Point(dragging.Position.X + offset.X, dragging.Position.Y + offset.Y);
				}
				
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				GamePiece mergeWith = null;
				// Find an already-moved piece that we can merge with at the destination
				// point
				if (mergeCandidates != null)
				{
					for (int i = 0, n = mergeCandidates.size(); i < n; ++i)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'candidate '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						GamePiece candidate = mergeCandidates.get_Renamed(i);
						if (map.getPieceCollection().canMerge(candidate, dragging))
						{
							mergeWith = candidate;
							mergeCandidates.set_Renamed(i, dragging);
							break;
						}
					}
				}
				
				// Now look for an already-existing piece at the destination point
				if (mergeWith == null)
				{
					mergeWith = map.findAnyPiece(p, dropTargetSelector);
					if (mergeWith == null && !true.Equals(dragging.getProperty(VassalSharp.counters.Properties_Fields.IGNORE_GRID)))
					{
						p = map.snapTo(p);
					}
					
					if (offset.IsEmpty)
					{
						offset = new System.Drawing.Point(p.X - dragging.Position.X, p.Y - dragging.Position.Y);
					}
					
					if (mergeWith != null && map.StackMetrics.StackingEnabled)
					{
						//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
						mergeCandidates = new ArrayList < GamePiece >();
						mergeCandidates.add(dragging);
						mergeCandidates.add(mergeWith);
						mergeTargets.put(p, mergeCandidates);
					}
				}
				
				if (mergeWith == null)
				{
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					comm = comm.append(movedPiece(dragging, ref p));
					comm = comm.append(map.placeAt(dragging, p));
					if (!(dragging is Stack) && !true.Equals(dragging.getProperty(VassalSharp.counters.Properties_Fields.NO_STACK)))
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'parent '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						Stack parent = map.StackMetrics.createStack(dragging);
						if (parent != null)
						{
							comm = comm.append(map.placeAt(parent, p));
						}
					}
				}
				else
				{
					// Do not add pieces to the Deck that are Obscured to us, or that
					// the Deck does not want to contain. Removing them from the
					// draggedPieces list will cause them to be left behind where the
					// drag started. NB. Pieces that have been dragged from a face-down
					// Deck will be be Obscued to us, but will be Obscured by the dummy
					// user Deck.NO_USER
					if (mergeWith is Deck)
					{
						//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
						final ArrayList < GamePiece > newList = new ArrayList < GamePiece >(0);
						//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
						for(GamePiece piece: draggedPieces)
						{
							if (((Deck) mergeWith).mayContain(piece))
							{
								//UPGRADE_NOTE: Final was removed from the declaration of 'isObscuredToMe '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
								bool isObscuredToMe = true.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.OBSCURED_TO_ME));
								if (!isObscuredToMe || (isObscuredToMe && Deck.NO_USER.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.OBSCURED_BY))))
								{
									newList.add(piece);
								}
							}
						}
						
						if (newList.size() != draggedPieces.size())
						{
							draggedPieces.clear();
							//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
							for(GamePiece piece: newList)
							{
								draggedPieces.add(piece);
							}
						}
					}
					
					// Add the remaining dragged counters to the target.
					// If mergeWith is a single piece (not a Stack), then we are merging
					// into an expanded Stack and the merge order must be reversed to
					// maintain the order of the merging pieces.
					if (mergeWith is Stack)
					{
						for (int i = 0; i < draggedPieces.size(); ++i)
						{
							comm = comm.append(movedPiece(draggedPieces.get_Renamed(i), mergeWith.Position));
							comm = comm.append(map.StackMetrics.merge(mergeWith, draggedPieces.get_Renamed(i)));
						}
					}
					else
					{
						for (int i = draggedPieces.size() - 1; i >= 0; --i)
						{
							comm = comm.append(movedPiece(draggedPieces.get_Renamed(i), mergeWith.Position));
							comm = comm.append(map.StackMetrics.merge(mergeWith, draggedPieces.get_Renamed(i)));
						}
					}
				}
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(GamePiece piece: draggedPieces)
				{
					KeyBuffer.Buffer.add(piece);
				}
				
				// Record each individual piece moved
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(GamePiece piece: draggedPieces)
				{
					allDraggedPieces.add(piece);
				}
				
				tracker.addPiece(dragging);
			}
			
			if (GlobalOptions.Instance.autoReportEnabled())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'report '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Command report = createMovementReporter(comm).ReportCommand.append(new MovementReporter.HiddenMovementReporter(comm).ReportCommand);
				report.execute();
				comm = comm.append(report);
			}
			
			// Apply key after move to each moved piece
			if (map.getMoveKey() != null)
			{
				applyKeyAfterMove(allDraggedPieces, comm, map.getMoveKey());
			}
			
			tracker.repaint();
			return comm;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void applyKeyAfterMove(List < GamePiece > pieces, 
		Command comm, KeyStroke key)
		
		/// <summary> This listener is used for faking drag-and-drop on Java 1.1 systems
		/// 
		/// </summary>
		/// <param name="e">
		/// </param>
		public virtual void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (canHandleEvent(event_sender, e))
			{
				selectMovablePieces(event_sender, e);
			}
		}
		
		/// <summary>Place the clicked-on piece into the {@link DragBuffer} </summary>
		protected internal virtual void  selectMovablePieces(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GamePiece p = map.findPiece(new System.Drawing.Point(e.X, e.Y), dragTargetSelector);
			dragBegin = new System.Drawing.Point(e.X, e.Y);
			if (p != null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'filter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				EventFilter filter = (EventFilter) p.getProperty(VassalSharp.counters.Properties_Fields.MOVE_EVENT_FILTER);
				if (filter == null || !filter.rejectEvent(e))
				{
					selectionProcessor.accept(p);
				}
				else
				{
					DragBuffer.Buffer.clear();
				}
			}
			else
			{
				DragBuffer.Buffer.clear();
			}
			// show/hide selection boxes
			map.repaint();
		}
		
		/// <deprecated> Use {@link #selectMovablePieces(MouseEvent)}. 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual void  selectMovablePieces(ref System.Drawing.Point point)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GamePiece p = map.findPiece(point, dragTargetSelector);
			dragBegin = point;
			selectionProcessor.accept(p);
			// show/hide selection boxes
			map.repaint();
		}
		
		protected internal virtual bool canHandleEvent(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isConsumed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			return !(System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift) && !(System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control) && !e.isMetaDown() && e.Clicks < 2 && !e.isConsumed();
		}
		
		/// <summary> Return true if this point is "close enough" to the point at which
		/// the user pressed the mouse to be considered a mouse click (such
		/// that no moves are done)
		/// </summary>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual bool isClick(ref System.Drawing.Point pt)
		{
			bool isClick = false;
			if (!dragBegin.IsEmpty)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				Board b = map.findBoard(ref pt);
				bool useGrid = b != null && b.getGrid() != null;
				if (useGrid)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'it '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					PieceIterator it = DragBuffer.Buffer.Iterator;
					//UPGRADE_NOTE: Final was removed from the declaration of 'dragging '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					GamePiece dragging = it.hasMoreElements()?it.nextPiece():null;
					useGrid = dragging != null && !true.Equals(dragging.getProperty(VassalSharp.counters.Properties_Fields.IGNORE_GRID)) && (dragging.Parent == null || !dragging.Parent.isExpanded());
				}
				if (useGrid)
				{
					if (map.Equals(DragBuffer.Buffer.FromMap))
					{
						if (map.snapTo(pt).equals(map.snapTo(dragBegin)))
						{
							isClick = true;
						}
					}
				}
				else
				{
					if (System.Math.Abs(pt.X - dragBegin.X) <= 5 && System.Math.Abs(pt.Y - dragBegin.Y) <= 5)
					{
						isClick = true;
					}
				}
			}
			return isClick;
		}
		
		public virtual void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (canHandleEvent(event_sender, e))
			{
				System.Drawing.Point tempAux = new System.Drawing.Point(e.X, e.Y);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				if (!isClick(ref tempAux))
				{
					System.Drawing.Point tempAux2 = new System.Drawing.Point(e.X, e.Y);
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					performDrop(ref tempAux2);
				}
			}
			dragBegin = System.Drawing.Point.Empty;
			map.getView().setCursor(null);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual void  performDrop(ref System.Drawing.Point p)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'move '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			Command move = movePieces(map, ref p);
			GameModule.getGameModule().sendAndLog(move);
			if (move != null)
			{
				DragBuffer.Buffer.clear();
			}
		}
		
		public virtual void  mouseEntered(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseExited(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseClicked(System.Object event_sender, System.EventArgs e)
		{
		}
		
		/// <summary> Implement Comparator to sort the contents of the drag buffer before
		/// completing the drag. This sorts the contents to be in the same order
		/// as the pieces were in their original parent stack.
		/// </summary>
		public virtual int compare(GamePiece p1, GamePiece p2)
		{
			return pieceSorter.compare(p1, p2);
		}
		
		/// <summary>Common functionality for DragHandler for cases with and without
		/// drag image support. <p>
		/// NOTE: DragSource.isDragImageSupported() returns false for j2sdk1.4.2_02 on
		/// Windows 2000
		/// 
		/// </summary>
		/// <author>  Pieter Geerkens
		/// </author>
		abstract public class AbstractDragHandler : DragSourceMotionListener
		{
			public AbstractDragHandler()
			{
				InitBlock();
			}
			private void  InitBlock()
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final ArrayList < Point > relativePositions = new ArrayList < Point >();
				//UPGRADE_NOTE: Final was removed from the declaration of 'dragContents '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				PieceIterator dragContents = DragBuffer.Buffer.Iterator;
				//UPGRADE_NOTE: Final was removed from the declaration of 'firstPiece '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GamePiece firstPiece = dragContents.nextPiece();
				GamePiece lastPiece = firstPiece;
				
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				currentPieceOffsetX = (int) (originalPieceOffsetX / dragPieceOffCenterZoom * zoom + 0.5);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				currentPieceOffsetY = (int) (originalPieceOffsetY / dragPieceOffCenterZoom * zoom + 0.5);
				
				boundingBox = System.Drawing.Rectangle.Truncate(firstPiece.Shape.GetBounds());
				boundingBox.Width *= zoom;
				boundingBox.Height *= zoom;
				boundingBox.X *= zoom;
				boundingBox.Y *= zoom;
				if (doOffset)
				{
					calcDrawOffset();
				}
				
				relativePositions.add(new System.Drawing.Point(0, 0));
				int stackCount = 0;
				while (dragContents.hasMoreElements())
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'nextPiece '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					GamePiece nextPiece = dragContents.nextPiece();
					//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Rectangle r = System.Drawing.Rectangle.Truncate(nextPiece.Shape.GetBounds());
					r.Width *= zoom;
					r.Height *= zoom;
					r.X *= zoom;
					r.Y *= zoom;
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Point p = new System.Drawing.Point((int) Math.round(zoom * (nextPiece.Position.X - firstPiece.Position.X)), (int) Math.round(zoom * (nextPiece.Position.Y - firstPiece.Position.Y)));
					r.Offset(p.X, p.Y);
					
					if (nextPiece.Position.Equals(lastPiece.Position))
					{
						stackCount++;
						//UPGRADE_NOTE: Final was removed from the declaration of 'sm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						StackMetrics sm = getStackMetrics(nextPiece);
						r.Offset(sm.unexSepX * stackCount, (- sm.unexSepY) * stackCount);
					}
					
					SupportClass.RectangleSupport.AddRectangleToRectangle(ref boundingBox, r);
					relativePositions.add(p);
					lastPiece = nextPiece;
				}
				return relativePositions;
				//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Graphics g = image.createGraphics();
				
				int index = 0;
				System.Drawing.Point lastPos = System.Drawing.Point.Empty;
				int stackCount = 0;
				for (PieceIterator dragContents = DragBuffer.Buffer.Iterator; dragContents.hasMoreElements(); )
				{
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'piece '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					GamePiece piece = dragContents.nextPiece();
					//UPGRADE_NOTE: Final was removed from the declaration of 'pos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Point pos = relativePositions.get_Renamed(index++);
					//UPGRADE_NOTE: Final was removed from the declaration of 'map '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Map map = piece.getMap();
					
					if (piece is Stack)
					{
						stackCount = 0;
						piece.draw(g, EXTRA_BORDER - boundingBox.X + pos.X, EXTRA_BORDER - boundingBox.Y + pos.Y, map == null?target:map.getView(), zoom);
					}
					else
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'offset '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Drawing.Point offset = new System.Drawing.Point(0, 0);
						if (pos.Equals(lastPos))
						{
							stackCount++;
							//UPGRADE_NOTE: Final was removed from the declaration of 'sm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							StackMetrics sm = getStackMetrics(piece);
							offset.X = sm.unexSepX * stackCount;
							offset.Y = sm.unexSepY * stackCount;
						}
						else
						{
							stackCount = 0;
						}
						
						//UPGRADE_NOTE: Final was removed from the declaration of 'x '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						int x = EXTRA_BORDER - boundingBox.X + pos.X + offset.X;
						//UPGRADE_NOTE: Final was removed from the declaration of 'y '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						int y = EXTRA_BORDER - boundingBox.Y + pos.Y - offset.Y;
						piece.draw(g, x, y, map == null?target:map.getView(), zoom);
						
						//UPGRADE_NOTE: Final was removed from the declaration of 'highlighter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						Highlighter highlighter = map == null?BasicPiece.Highlighter:map.getHighlighter();
						highlighter.draw(piece, g, x, y, null, zoom);
					}
					
					lastPos = pos;
				}
				
				g.Dispose();
			}
			/// <summary>returns the singleton DragHandler instance </summary>
			static public AbstractDragHandler TheDragHandler
			{
				get
				{
					return theDragHandler;
				}
				
				set
				{
					theDragHandler = value;
				}
				
			}
			abstract protected internal int OffsetMult{get;}
			/// <summary> creates or moves cursor object to given JLayeredPane. Usually called by setDrawWinToOwnerOf()</summary>
			//UPGRADE_TODO: Class 'javax.swing.JLayeredPane' was converted to 'System.Windows.Forms.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			private System.Windows.Forms.Panel DrawWin
			{
				set
				{
					if (value != drawWin)
					{
						// remove cursor from old window
						if (dragCursor.Parent != null)
						{
							dragCursor.Parent.Controls.Remove(dragCursor);
						}
						if (drawWin != null)
						{
							drawWin.Invalidate(dragCursor.Bounds);
						}
						drawWin = value;
						calcDrawOffset();
						dragCursor.Visible = false;
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
						drawWin.Controls.Add(dragCursor);
						dragCursor.Dock = new System.Windows.Forms.DockStyle();
						dragCursor.BringToFront();
					}
				}
				
			}
			/// <summary> creates or moves cursor object to given window. Called when drag operation begins in a window or the cursor is
			/// dragged over a new drop-target window
			/// </summary>
			virtual public System.Windows.Forms.Control DrawWinToOwnerOf
			{
				set
				{
					if (value != null)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'rootWin '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getRootPane' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetRootPane_javaawtComponent'"
						System.Windows.Forms.ContainerControl rootWin = (System.Windows.Forms.ContainerControl) value.Parent;
						if (rootWin != null)
						{
							//UPGRADE_ISSUE: Method 'javax.swing.JRootPane.getLayeredPane' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJRootPanegetLayeredPane'"
							DrawWin = rootWin.getLayeredPane();
						}
					}
				}
				
			}
			//UPGRADE_ISSUE: Method 'java.awt.dnd.DragSource.isDragImageSupported' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000'"
			private static AbstractDragHandler theDragHandler = DragSource.isDragImageSupported()?(SystemUtils.IS_OS_MAC_OSX?new DragHandlerMacOSX():new DragHandler()):new DragHandlerNoImage();
			
			internal const int CURSOR_ALPHA = 127; // psuedo cursor is 50% transparent
			internal const int EXTRA_BORDER = 4; // psuedo cursor is includes a 4 pixel border
			
			
			protected internal System.Windows.Forms.Label dragCursor; // An image label. Lives on current DropTarget's
			// LayeredPane.
			//      private BufferedImage dragImage; // An image label. Lives on current DropTarget's LayeredPane.
			private System.Drawing.Point drawOffset = new System.Drawing.Point(0, 0); // translates event coords to local
			// drawing coords
			private System.Drawing.Rectangle boundingBox; // image bounds
			private int originalPieceOffsetX; // How far drag STARTED from gamepiece's
			// center
			private int originalPieceOffsetY; // I.e. on original map
			protected internal double dragPieceOffCenterZoom = 1.0; // zoom at start of drag
			private int currentPieceOffsetX; // How far cursor is CURRENTLY off-center,
			// a function of
			// dragPieceOffCenter{X,Y,Zoom}
			private int currentPieceOffsetY; // I.e. on current map (which may have
			// different zoom
			protected internal double dragCursorZoom = 1.0; // Current cursor scale (zoom)
			internal System.Windows.Forms.Control dragWin; // the component that initiated the drag operation
			internal System.Windows.Forms.Control dropWin; // the drop target the mouse is currently over
			//UPGRADE_TODO: Class 'javax.swing.JLayeredPane' was converted to 'System.Windows.Forms.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			internal System.Windows.Forms.Panel drawWin; // the component that owns our psuedo-cursor
			// Seems there can be only one DropTargetListener a drop target. After we
			// process a drop target
			// event, we manually pass the event on to this listener.
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			java.util.Map < Component, DropTargetListener > dropTargetListeners = 
			new HashMap < Component, DropTargetListener >();
			
			/// <summary> Creates a new DropTarget and hooks us into the beginning of a
			/// DropTargetListener chain. DropTarget events are not multicast;
			/// there can be only one "true" listener.
			/// </summary>
			//UPGRADE_ISSUE: Class 'java.awt.dnd.DropTarget' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTarget'"
			//UPGRADE_TODO: Interface 'java.awt.dnd.DropTargetListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			static public DropTarget makeDropTarget(System.Windows.Forms.Control theComponent, int dndContants, DropTargetListener dropTargetListener)
			{
				if (dropTargetListener != null)
				{
					DragHandler.TheDragHandler.dropTargetListeners.put(theComponent, dropTargetListener);
				}
				theComponent.AllowDrop = true;
				theComponent.DragEnter += new System.Windows.Forms.DragEventHandler(DragHandler.TheDragHandler.dragEnter_renamed);
				theComponent.DragOver += new System.Windows.Forms.DragEventHandler(DragHandler.TheDragHandler.dragOver_renamed);
				theComponent.DragLeave += new System.EventHandler(DragHandler.TheDragHandler.dragExit_renamed);
				theComponent.DragDrop += new System.Windows.Forms.DragEventHandler(DragHandler.TheDragHandler.drop_renamed);
				//UPGRADE_ISSUE: Constructor 'java.awt.dnd.DropTarget.DropTarget' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTarget'"
				return new DropTarget(theComponent, dndContants, DragHandler.TheDragHandler);
			}
			
			static public void  removeDropTarget(System.Windows.Forms.Control theComponent)
			{
				DragHandler.TheDragHandler.dropTargetListeners.remove(theComponent);
			}
			
			//UPGRADE_TODO: Interface 'java.awt.dnd.DropTargetListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			//UPGRADE_ISSUE: Class 'java.awt.dnd.DropTargetEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTargetEvent'"
			protected internal virtual DropTargetListener getListener(System.Object event_sender, DropTargetEvent e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'component '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'java.awt.dnd.DropTargetContext.getComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTargetContext'"
				//UPGRADE_ISSUE: Method 'java.awt.dnd.DropTargetEvent.getDropTargetContext' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTargetEvent'"
				System.Windows.Forms.Control component = e.getDropTargetContext().getComponent();
				return dropTargetListeners.get_Renamed(component);
			}
			
			/// <summary>Moves the drag cursor on the current draw window </summary>
			protected internal virtual void  moveDragCursor(int dragX, int dragY)
			{
				if (drawWin != null)
				{
					//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_int_int'"
					dragCursor.Location = new System.Drawing.Point(dragX - drawOffset.X, dragY - drawOffset.Y);
				}
			}
			
			/// <summary>Removes the drag cursor from the current draw window </summary>
			protected internal virtual void  removeDragCursor()
			{
				if (drawWin != null)
				{
					if (dragCursor != null)
					{
						dragCursor.Visible = false;
						drawWin.Controls.Remove(dragCursor);
					}
					drawWin = null;
				}
			}
			
			/// <summary>calculates the offset between cursor dragCursor positions </summary>
			private void  calcDrawOffset()
			{
				if (drawWin != null)
				{
					// drawOffset is the offset between the mouse location during a drag
					// and the upper-left corner of the cursor
					// accounts for difference between event point (screen coords)
					// and Layered Pane position, boundingBox and off-center drag
					drawOffset.X = - boundingBox.X - currentPieceOffsetX + EXTRA_BORDER;
					drawOffset.Y = - boundingBox.Y - currentPieceOffsetY + EXTRA_BORDER;
					SupportClass.SwingUtilsSupport.PointToScreen(ref drawOffset, drawWin);
				}
			}
			
			/// <summary>Common functionality abstracted from makeDragImage and makeDragCursor
			/// 
			/// </summary>
			/// <param name="zoom">
			/// </param>
			/// <param name="doOffset">
			/// </param>
			/// <param name="target">
			/// </param>
			/// <param name="setSize">
			/// </param>
			/// <returns>
			/// </returns>
			internal virtual System.Drawing.Bitmap makeDragImageCursorCommon(double zoom, bool doOffset, System.Windows.Forms.Control target, bool setSize)
			{
				// FIXME: Should be an ImageOp.
				dragCursorZoom = zoom;
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final List < Point > relativePositions = buildBoundingBox(zoom, doOffset);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int w = boundingBox.Width + EXTRA_BORDER * 2;
				//UPGRADE_NOTE: Final was removed from the declaration of 'h '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int h = boundingBox.Height + EXTRA_BORDER * 2;
				
				System.Drawing.Bitmap image = ImageUtils.createCompatibleTranslucentImage(w, h);
				
				drawDragImage(image, target, relativePositions, zoom);
				
				if (setSize)
				{
					//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
					dragCursor.Size = new System.Drawing.Size(w, h);
				}
				image = featherDragImage(image, w, h, EXTRA_BORDER);
				
				return image;
			}
			
			/// <summary> Creates the image to use when dragging based on the zoom factor
			/// passed in.
			/// 
			/// </summary>
			/// <param name="zoom">DragBuffer.getBuffer
			/// </param>
			/// <returns> dragImage
			/// </returns>
			private System.Drawing.Bitmap makeDragImage(double zoom)
			{
				return makeDragImageCursorCommon(zoom, false, null, false);
			}
			
			/// <summary> Installs the cursor image into our dragCursor JLabel.
			/// Sets current zoom. Should be called at beginning of drag
			/// and whenever zoom changes. INPUT: DragBuffer.getBuffer OUTPUT:
			/// dragCursorZoom cursorOffCenterX cursorOffCenterY boundingBox
			/// </summary>
			/// <param name="zoom">DragBuffer.getBuffer
			/// 
			/// </param>
			protected internal virtual void  makeDragCursor(double zoom)
			{
				// create the cursor if necessary
				if (dragCursor == null)
				{
					dragCursor = new System.Windows.Forms.Label();
					dragCursor.Visible = false;
				}
				dragCursor.Image = (System.Drawing.Image) makeDragImageCursorCommon(zoom, true, dragCursor, true).Clone();
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private List < Point > buildBoundingBox(double zoom, boolean doOffset)
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			void drawDragImage(BufferedImage image, Component target, 
			List < Point > relativePositions, double zoom)
			
			private StackMetrics getStackMetrics(GamePiece piece)
			{
				StackMetrics sm = null;
				//UPGRADE_NOTE: Final was removed from the declaration of 'map '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Map map = piece.getMap();
				if (map != null)
				{
					sm = map.StackMetrics;
				}
				if (sm == null)
				{
					sm = new StackMetrics();
				}
				return sm;
			}
			
			private System.Drawing.Bitmap featherDragImage(System.Drawing.Bitmap src, int w, int h, int b)
			{
				// FIXME: This should be redone so that we draw the feathering onto the
				// destination first, and then pass the Graphics2D on to draw the pieces
				// directly over it. Presently this doesn't work because some of the
				// pieces screw up the Graphics2D when passed it... The advantage to doing
				// it this way is that we create only one BufferedImage instead of two.
				//UPGRADE_NOTE: Final was removed from the declaration of 'dst '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Bitmap dst = ImageUtils.createCompatibleTranslucentImage(w, h);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dst);
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHint_javaawtRenderingHintsKey_javalangObject'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_ANTIALIASING_f'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_ANTIALIAS_ON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_ANTIALIAS_ON_f'"
				g.setRenderingHint(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
				
				// paint the rectangle occupied by the piece at specified alpha
				SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.FromArgb(CURSOR_ALPHA, 0xff, 0xff, 0xff));
				g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, 0, w, h);
				
				// feather outwards
				for (int f = 0; f < b; ++f)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'alpha '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int alpha = CURSOR_ALPHA * (f + 1) / b;
					SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.FromArgb(alpha, 0xff, 0xff, 0xff));
					g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), f, f, w - 2 * f, h - 2 * f);
				}
				
				// paint in the source image
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setComposite' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetComposite_javaawtComposite'"
				//UPGRADE_ISSUE: Method 'java.awt.AlphaComposite.getInstance' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
				//UPGRADE_ISSUE: Field 'java.awt.AlphaComposite.SRC_IN' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtAlphaComposite'"
				g.setComposite(AlphaComposite.getInstance(AlphaComposite.SRC_IN));
				//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				g.DrawImage(src, 0, 0);
				g.Dispose();
				
				return dst;
			}
			
			///////////////////////////////////////////////////////////////////////////
			// DRAG GESTURE LISTENER INTERFACE
			//
			// EVENT uses SCALED, DRAG-SOURCE coordinate system.
			// PIECE uses SCALED, OWNER (arbitrary) coordinate system
			//
			///////////////////////////////////////////////////////////////////////////
			/// <summary>Fires after user begins moving the mouse several pixels over a map. </summary>
			public virtual void  dragGestureRecognized(System.Object event_sender, System.Windows.Forms.MouseEventArgs dge)
			{
				try
				{
					beginDragging(dge);
				}
				// FIXME: Fix by replacing AWT Drag 'n Drop with Swing DnD.
				// Catch and ignore spurious DragGestures
				catch (System.Exception e)
				{
				}
			}
			
			protected internal virtual System.Drawing.Point dragGestureRecognizedPrep(System.Object event_sender, System.Windows.Forms.MouseEventArgs dge)
			{
				// Ensure the user has dragged on a counter before starting the drag.
				//UPGRADE_NOTE: Final was removed from the declaration of 'db '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				DragBuffer db = DragBuffer.Buffer;
				if (db.Empty)
					return System.Drawing.Point.Empty;
				
				// Remove any Immovable pieces from the DragBuffer that were
				// selected in a selection rectangle, unless they are being
				// dragged from a piece palette (i.e., getMap() == null).
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final List < GamePiece > pieces = new ArrayList < GamePiece >();
				for (PieceIterator i = db.Iterator; i.hasMoreElements(); pieces.add(i.nextPiece()))
					;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(GamePiece piece: pieces)
				{
					if (this.piece.getMap() != null && true.Equals(this.piece.getProperty(VassalSharp.counters.Properties_Fields.NON_MOVABLE)))
					{
						db.remove(this.piece);
					}
				}
				
				// Bail out if this leaves no pieces to drag.
				if (db.Empty)
					return System.Drawing.Point.Empty;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'piece '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GamePiece piece = db.Iterator.nextPiece();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'map '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: The method 'java.awt.dnd.DragGestureEvent.getComponent' needs to be in a event handling method in order to be properly converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1171'"
				//UPGRADE_TODO: The method 'java.awt.dnd.DragGestureEvent.getComponent' needs to be in a event handling method in order to be properly converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1171'"
				Map map = (System.Windows.Forms.Control) dge.getComponent() is Map.View?((Map.View) dge.getComponent()).getMap():null;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'mousePosition '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point mousePosition = (map == null)?new System.Drawing.Point(dge.X, dge.Y):map.componentCoordinates(new System.Drawing.Point(dge.X, dge.Y));
				System.Drawing.Point piecePosition = (map == null)?piece.Position:map.componentCoordinates(piece.Position);
				// If DragBuffer holds a piece with invalid coordinates (for example, a
				// card drawn from a deck), drag from center of piece
				if (piecePosition.X <= 0 || piecePosition.Y <= 0)
				{
					piecePosition = mousePosition;
				}
				// Account for offset of piece within stack
				// We do this even for un-expanded stacks, since the offset can
				// still be significant if the stack is large
				dragPieceOffCenterZoom = map == null?1.0:map.Zoom;
				if (piece.Parent != null && map != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'offset '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Point offset = piece.Parent.getStackMetrics().relativePosition(piece.Parent, piece);
					//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
					piecePosition.Offset((int) System.Math.Round(offset.X * dragPieceOffCenterZoom), (int) System.Math.Round(offset.Y * dragPieceOffCenterZoom));
				}
				
				// dragging from UL results in positive offsets
				originalPieceOffsetX = piecePosition.X - mousePosition.X;
				originalPieceOffsetY = piecePosition.Y - mousePosition.Y;
				//UPGRADE_TODO: The method 'java.awt.dnd.DragGestureEvent.getComponent' needs to be in a event handling method in order to be properly converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1171'"
				dragWin = (System.Windows.Forms.Control) dge.getComponent();
				drawWin = null;
				dropWin = null;
				return mousePosition;
			}
			
			protected internal virtual void  beginDragging(System.Object event_sender, System.Windows.Forms.MouseEventArgs dge)
			{
				// this call is needed to instantiate the boundingBox object
				//UPGRADE_NOTE: Final was removed from the declaration of 'bImage '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Bitmap bImage = makeDragImage(dragPieceOffCenterZoom);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'dragPointOffset '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point dragPointOffset = new System.Drawing.Point(OffsetMult * (boundingBox.X + currentPieceOffsetX - EXTRA_BORDER), OffsetMult * (boundingBox.Y + currentPieceOffsetY - EXTRA_BORDER));
				
				((System.Windows.Forms.Control) event_sender).DoDragDrop(new System.Windows.Forms.DataObject(""), System.Windows.Forms.DragDropEffects.All);
				
				//UPGRADE_ISSUE: Method 'java.awt.dnd.DragGestureEvent.getDragSource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDragGestureEventgetDragSource'"
				dge.getDragSource().addDragSourceMotionListener(this);
			}
			
			///////////////////////////////////////////////////////////////////////////
			// DRAG SOURCE LISTENER INTERFACE
			//
			///////////////////////////////////////////////////////////////////////////
			public virtual void  dragDropEnd(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'ds '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class 'java.awt.dnd.DragSource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000'"
				//UPGRADE_ISSUE: Method 'java.awt.dnd.DragSourceContext.getDragSource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDragSourceContext'"
				//UPGRADE_ISSUE: Method 'java.awt.dnd.DragSourceEvent.getDragSourceContext' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDragSourceEventgetDragSourceContext'"
				DragSource ds = e.getDragSourceContext().getDragSource();
				ds.removeDragSourceMotionListener(this);
			}
			
			public virtual void  dragEnter(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
			{
			}
			
			public virtual void  dragExit(System.Object event_sender, System.EventArgs e)
			{
			}
			
			public virtual void  dragOver(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
			{
			}
			
			public virtual void  dropActionChanged(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
			{
			}
			
			///////////////////////////////////////////////////////////////////////////
			// DRAG SOURCE MOTION LISTENER INTERFACE
			//
			// EVENT uses UNSCALED, SCREEN coordinate system
			//
			///////////////////////////////////////////////////////////////////////////
			// Used to check for real mouse movement.
			// Warning: dragMouseMoved fires 8 times for each point on development
			// system (Win2k)
			protected internal System.Drawing.Point lastDragLocation = new System.Drawing.Point(0, 0);
			
			/// <summary>Moves cursor after mouse </summary>
			abstract public void  dragMouseMoved(System.Object event_sender, System.Windows.Forms.DragEventArgs e);
			
			///////////////////////////////////////////////////////////////////////////
			// DROP TARGET INTERFACE
			//
			// EVENT uses UNSCALED, DROP-TARGET coordinate system
			///////////////////////////////////////////////////////////////////////////
			/// <summary>switches current drawWin when mouse enters a new DropTarget </summary>
			public virtual void  dragEnter_renamed(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'forward '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Interface 'java.awt.dnd.DropTargetListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				DropTargetListener forward = getListener(e);
				if (forward != null)
				{
					//UPGRADE_TODO: Method 'java.awt.dnd.DropTargetListener.dragEnter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					forward.dragEnter(e);
				}
			}
			
			/// <summary> Last event of the drop operation. We adjust the drop point for
			/// off-center drag, remove the cursor, and pass the event along
			/// listener chain.
			/// </summary>
			public virtual void  drop_renamed(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
			{
				// EVENT uses UNSCALED, DROP-TARGET coordinate system
				SupportClass.GetLocation(e).Offset(currentPieceOffsetX, currentPieceOffsetY);
				//UPGRADE_NOTE: Final was removed from the declaration of 'forward '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Interface 'java.awt.dnd.DropTargetListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				DropTargetListener forward = getListener(e);
				if (forward != null)
				{
					//UPGRADE_TODO: Method 'java.awt.dnd.DropTargetListener.drop' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					forward.drop(e);
				}
			}
			
			/// <summary>ineffectual. Passes event along listener chain </summary>
			//UPGRADE_ISSUE: Class 'java.awt.dnd.DropTargetEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTargetEvent'"
			public virtual void  dragExit_renamed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'forward '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Interface 'java.awt.dnd.DropTargetListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				DropTargetListener forward = getListener(e);
				if (forward != null)
				{
					//UPGRADE_TODO: Method 'java.awt.dnd.DropTargetListener.dragExit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					forward.dragExit(e);
				}
			}
			
			/// <summary>ineffectual. Passes event along listener chain </summary>
			public virtual void  dragOver_renamed(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'forward '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Interface 'java.awt.dnd.DropTargetListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				DropTargetListener forward = getListener(e);
				if (forward != null)
				{
					//UPGRADE_TODO: Method 'java.awt.dnd.DropTargetListener.dragOver' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					forward.dragOver(e);
				}
			}
			
			/// <summary>ineffectual. Passes event along listener chain </summary>
			public virtual void  dropActionChanged(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'forward '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Interface 'java.awt.dnd.DropTargetListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				DropTargetListener forward = getListener(e);
				if (forward != null)
				{
					//UPGRADE_TODO: Method 'java.awt.dnd.DropTargetListener.dropActionChanged' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					forward.dropActionChanged(e);
				}
			}
		}
		
		/// <summary> Implements a psudo-cursor that follows the mouse cursor when user
		/// drags gamepieces. Supports map zoom by resizing cursor when it enters
		/// a drop target of type Map.View.
		/// 
		/// </summary>
		/// <author>  Jim Urbas
		/// </author>
		/// <version>  0.4.2
		/// 
		/// </version>
		public class DragHandlerNoImage:AbstractDragHandler
		{
			override protected internal int OffsetMult
			{
				get
				{
					return 1;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public override void  dragGestureRecognized(System.Object event_sender, System.Windows.Forms.MouseEventArgs dge)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'mousePosition '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point mousePosition = dragGestureRecognizedPrep(dge);
				if (mousePosition.IsEmpty)
					return ;
				
				makeDragCursor(dragPieceOffCenterZoom);
				DrawWinToOwnerOf = dragWin;
				SupportClass.SwingUtilsSupport.PointToScreen(ref mousePosition, drawWin);
				moveDragCursor(mousePosition.X, mousePosition.Y);
				
				base.dragGestureRecognized(dge);
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public override void  dragDropEnd(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
			{
				removeDragCursor();
				base.dragDropEnd(e);
			}
			
			public override void  dragMouseMoved(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
			{
				if (!e.getLocation().equals(lastDragLocation))
				{
					lastDragLocation = e.getLocation();
					moveDragCursor(e.getX(), e.getY());
					//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
					if (dragCursor != null && !dragCursor.Visible)
					{
						dragCursor.Visible = true;
					}
				}
			}
			
			public override void  dragEnter_renamed(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'newDropWin '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'java.awt.dnd.DropTargetContext.getComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTargetContext'"
				//UPGRADE_ISSUE: Method 'java.awt.dnd.DropTargetEvent.getDropTargetContext' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTargetEvent'"
				System.Windows.Forms.Control newDropWin = e.getDropTargetContext().getComponent();
				if (newDropWin != dropWin)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'newZoom '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					double newZoom = newDropWin is Map.View?((Map.View) newDropWin).getMap().getZoom():1.0;
					if (System.Math.Abs(newZoom - dragCursorZoom) > 0.01)
					{
						makeDragCursor(newZoom);
					}
					//UPGRADE_ISSUE: Method 'java.awt.dnd.DropTargetContext.getComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTargetContext'"
					//UPGRADE_ISSUE: Method 'java.awt.dnd.DropTargetEvent.getDropTargetContext' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtdndDropTargetEvent'"
					DrawWinToOwnerOf = e.getDropTargetContext().getComponent();
					dropWin = newDropWin;
				}
				base.dragEnter(e);
			}
			
			public override void  drop_renamed(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
			{
				removeDragCursor();
				base.drop(e);
			}
		}
		
		/// <summary>Implementation of AbstractDragHandler when DragImage is supported by JRE
		/// 
		/// </summary>
		/// <Author>  Pieter Geerkens </Author>
		public class DragHandler:AbstractDragHandler
		{
			override protected internal int OffsetMult
			{
				get
				{
					return - 1;
				}
				
			}
			public override void  dragGestureRecognized(System.Object event_sender, System.Windows.Forms.MouseEventArgs dge)
			{
				if (dragGestureRecognizedPrep(dge).IsEmpty)
					return ;
				base.dragGestureRecognized(dge);
			}
			
			public override void  dragMouseMoved(System.Object event_sender, System.Windows.Forms.DragEventArgs e)
			{
			}
		}
		
		public class DragHandlerMacOSX:DragHandler
		{
			override protected internal int OffsetMult
			{
				get
				{
					return 1;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		virtual public System.Int32 Compare(System.Object x, System.Object y)
		{
			return 0;
		}
		static PieceMover()
		{
			// We force the loading of these classes because otherwise they would
			// be loaded when the user initiates the first drag, which makes the
			// start of the drag choppy.
			{
				try
				{
					//UPGRADE_TODO: The differences in the format  of parameters for method 'java.lang.Class.forName'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					System.Type.GetType(typeof(MovementReporter).FullName);
					//UPGRADE_TODO: The differences in the format  of parameters for method 'java.lang.Class.forName'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					System.Type.GetType(typeof(KeyBuffer).FullName);
				}
				//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception e)
				{
					throw new IllegalStateException(e); // impossible
				}
			}
		}
	}
}