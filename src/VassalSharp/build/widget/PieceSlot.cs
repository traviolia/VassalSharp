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
using Buildable = VassalSharp.build.Buildable;
using Builder = VassalSharp.build.Builder;
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
using GpIdSupport = VassalSharp.build.GpIdSupport;
using Widget = VassalSharp.build.Widget;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using HelpWindow = VassalSharp.build.module.documentation.HelpWindow;
using HelpWindowExtension = VassalSharp.build.module.documentation.HelpWindowExtension;
using MenuDisplayer = VassalSharp.build.module.map.MenuDisplayer;
using AbstractDragHandler = VassalSharp.build.module.map.PieceMover.AbstractDragHandler;
using AddPiece = VassalSharp.command.AddPiece;
using Command = VassalSharp.command.Command;
using Configurer = VassalSharp.configure.Configurer;
using BasicPiece = VassalSharp.counters.BasicPiece;
using Decorator = VassalSharp.counters.Decorator;
using DragBuffer = VassalSharp.counters.DragBuffer;
using GamePiece = VassalSharp.counters.GamePiece;
using KeyBuffer = VassalSharp.counters.KeyBuffer;
using PieceCloner = VassalSharp.counters.PieceCloner;
using PieceDefiner = VassalSharp.counters.PieceDefiner;
using PlaceMarker = VassalSharp.counters.PlaceMarker;
using Properties = VassalSharp.counters.Properties;
using ComponentI18nData = VassalSharp.i18n.ComponentI18nData;
namespace VassalSharp.build.widget
{
	
	/// <summary> A Component that displays a GamePiece.
	/// 
	/// Can be added to any Widget but cannot contain any children Keyboard input on
	/// a PieceSlot is forwarded to the {@link GamePiece#keyEvent} method for the
	/// PieceSlot's GamePiece. Clicking on a PieceSlot initiates a drag
	/// </summary>
	public class PieceSlot:Widget
	{
		static private System.Int32 state323;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPopupMenuListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPopupMenuListener
		{
			public AnonymousClassPopupMenuListener(PieceSlot enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceSlot enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceSlot enclosingInstance;
			public PieceSlot Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  popupMenuCanceled(System.Object event_sender, System.EventArgs evt)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Enclosing_Instance.panel.Refresh();
			}
			
			public virtual void  popupMenuWillBecomeInvisible(System.Object event_sender, System.EventArgs evt)
			{
				Enclosing_Instance.clearExpandedPiece();
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Enclosing_Instance.panel.Refresh();
			}
			
			public virtual void  popupMenuWillBecomeVisible(System.Object event_sender, System.EventArgs evt)
			{
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDragGestureListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDragGestureListener
		{
			public AnonymousClassDragGestureListener(PieceSlot enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceSlot enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceSlot enclosingInstance;
			public PieceSlot Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  dragGestureRecognized(System.Object event_sender, System.Windows.Forms.MouseEventArgs dge)
			{
				Enclosing_Instance.startDrag();
				AbstractDragHandler.TheDragHandler.dragGestureRecognized(dge);
			}
		}
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state323 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state323 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private void  InitBlock()
		{
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > [0];
			return new System.Type[0];
		}
		/// <summary> Return defined GamePiece with prototypes unexpanded.
		/// 
		/// </summary>
		/// <returns> unexpanded piece
		/// </returns>
		virtual public GamePiece Piece
		{
			get
			{
				if (c == null && pieceDefinition != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'comm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					AddPiece comm = (AddPiece) GameModule.getGameModule().decode(pieceDefinition);
					if (comm == null)
					{
						System.Console.Error.WriteLine("Couldn't build piece " + pieceDefinition);
						pieceDefinition = null;
					}
					else
					{
						c = comm.Target;
						c.State = comm.State;
						
						//UPGRADE_NOTE: Final was removed from the declaration of 'size '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Drawing.Size size = panel.Size;
						c.Position = new System.Drawing.Point(size.Width / 2, size.Height / 2);
					}
				}
				
				if (c != null)
				{
					c.setProperty(VassalSharp.counters.Properties_Fields.PIECE_ID, GpId);
				}
				
				return c;
			}
			
			set
			{
				c = value;
				clearExpandedPiece();
				if (c != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'size '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Size size = panel.Size;
					c.Position = new System.Drawing.Point(size.Width / 2, size.Height / 2);
					name = Decorator.getInnermost(c).getName();
				}
				panel.Invalidate();
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				panel.Refresh();
				pieceDefinition = c == null?null:GameModule.getGameModule().encode(new AddPiece(c));
			}
			
		}
		/// <summary> Return defined GamePiece with prototypes fully expanded.
		/// 
		/// </summary>
		/// <returns> expanded piece
		/// </returns>
		virtual protected internal GamePiece ExpandedPiece
		{
			get
			{
				if (expanded == null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					GamePiece p = Piece;
					if (p != null)
					{
						// Possible when PlaceMarker is building
						expanded = PieceCloner.Instance.clonePiece(p);
					}
				}
				return expanded;
			}
			
		}
		virtual public System.Drawing.Size PreferredSize
		{
			get
			{
				if (c != null && panel.CreateGraphics() != null)
				{
					//      c.draw(panel.getGraphics(), 0, 0, panel, 1.0);
					return c.boundingBox().Size;
				}
				else
				{
					return new System.Drawing.Size(width, height);
				}
			}
			
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return "Single piece";
			}
			
		}
		override public System.Windows.Forms.Control Component
		{
			get
			{
				return panel;
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[0];
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[0];
			}
			
		}
		/// <returns> an array of Configurer objects representing the Buildable children
		/// of this Configurable object
		/// </returns>
		override public Configurable[] ConfigureComponents
		{
			get
			{
				return new Configurable[0];
			}
			
		}
		override public ComponentI18nData I18nData
		{
			get
			{
				/*
				* Piece can change due to editing, so cannot cache the I18nData
				*/
				return new ComponentI18nData(this, Piece);
			}
			
		}
		override public Configurer Configurer
		{
			get
			{
				return new MyConfigurer(this);
			}
			
		}
		virtual public System.String GpId
		{
			get
			{
				return gpId;
			}
			
			set
			{
				gpId = value;
			}
			
		}
		public const System.String GP_ID = "gpid";
		protected internal GamePiece c;
		protected internal GamePiece expanded;
		new protected internal System.String name;
		protected internal System.String pieceDefinition;
		//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
		protected internal static System.Drawing.Font FONT = new System.Drawing.Font("Dialog", 12, System.Drawing.FontStyle.Regular);
		protected internal System.Windows.Forms.Panel panel;
		protected internal int width, height;
		protected internal System.String gpId = ""; // Unique PieceSlot Id
		protected internal GpIdSupport gpidSupport;
		
		public PieceSlot()
		{
			InitBlock();
			panel = new PieceSlot.Panel(this, this);
			panel.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.build.widget.PieceSlot.mouseDown);
			panel.Click += new System.EventHandler(this.mouseClicked);
			panel.MouseEnter += new System.EventHandler(this.mouseEntered);
			panel.MouseLeave += new System.EventHandler(this.mouseExited);
			panel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mousePressed);
			panel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseReleased);
			panel.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.build.widget.PieceSlot.keyDown);
			panel.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyPressed);
			panel.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyReleased);
			panel.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyTyped);
		}
		
		public PieceSlot(PieceSlot piece):this()
		{
			copyFrom(piece);
		}
		
		public PieceSlot(CardSlot card):this((PieceSlot) card)
		{
		}
		
		protected internal virtual void  copyFrom(PieceSlot piece)
		{
			c = piece.c;
			name = piece.name;
			pieceDefinition = piece.pieceDefinition;
			gpidSupport = piece.gpidSupport;
			gpId = piece.gpId;
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Panel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		public class Panel:System.Windows.Forms.Panel
		{
			private void  InitBlock(PieceSlot enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceSlot enclosingInstance;
			public PieceSlot Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			protected internal PieceSlot pieceSlot;
			
			public Panel(PieceSlot enclosingInstance, PieceSlot slot):base()
			{
				InitBlock(enclosingInstance);
				setFocusTraversalKeysEnabled(false);
				pieceSlot = slot;
			}
			
			public virtual PieceSlot getPieceSlot()
			{
				return pieceSlot;
			}
			
			protected override void  OnPaint(System.Windows.Forms.PaintEventArgs g_EventArg)
			{
				System.Drawing.Graphics g = null;
				if (g_EventArg != null)
					g = g_EventArg.Graphics;
				Enclosing_Instance.paint(g);
			}
			
			//UPGRADE_NOTE: The equivalent of method 'javax.swing.JComponent.getPreferredSize' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public System.Drawing.Size getPreferredSize()
			{
				return Enclosing_Instance.PreferredSize;
			}
		}
		
		public PieceSlot(GamePiece p):this()
		{
			Piece = p;
		}
		
		protected internal virtual void  clearExpandedPiece()
		{
			expanded = null;
		}
		
		public virtual void  paint(System.Drawing.Graphics g)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'size '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Size size = panel.Size;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Color c = SupportClass.GraphicsManager.manager.GetColor(g);
			g.setColor(Color.WHITE);
			g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, 0, size.Width, size.Height);
			SupportClass.GraphicsManager.manager.SetColor(g, c);
			
			if (ExpandedPiece == null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Font fm = SupportClass.GraphicsManager.manager.GetFont(g);
				g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), 0, 0, size.Width - 1, size.Height - 1);
				SupportClass.GraphicsManager.manager.SetFont(g, FONT);
				//UPGRADE_TODO: Method 'java.awt.Graphics.drawString' was converted to 'System.Drawing.Graphics.DrawString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphicsdrawString_javalangString_int_int'"
				//UPGRADE_ISSUE: Method 'java.awt.FontMetrics.stringWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtFontMetricsstringWidth_javalangString'"
				g.DrawString(" nil ", SupportClass.GraphicsManager.manager.GetFont(g), SupportClass.GraphicsManager.manager.GetBrush(g), size.Width / 2 - fm.stringWidth(" nil ") / 2, size.Height / 2 - SupportClass.GraphicsManager.manager.GetFont(g).GetHeight());
			}
			else
			{
				ExpandedPiece.draw(g, size.Width / 2, size.Height / 2, panel, 1.0);
				
				// NB: The piece, not the expanded piece, receives events, so we check
				// the piece, not the expanded piece, for its selection status.
				if (true.Equals(Piece.getProperty(VassalSharp.counters.Properties_Fields.SELECTED)))
				{
					BasicPiece.Highlighter.draw(ExpandedPiece, g, size.Width / 2, size.Height / 2, panel, 1.0);
				}
			}
		}
		
		public virtual void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			KeyBuffer.Buffer.clear();
			Map.clearActiveMap();
			if (Piece != null)
			{
				KeyBuffer.Buffer.add(Piece);
			}
			
			clearExpandedPiece();
			//UPGRADE_TODO: Method 'javax.swing.JComponent.requestFocus' was converted to 'System.Windows.Forms.Control.Focus' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentrequestFocus'"
			panel.Focus();
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			panel.Refresh();
		}
		
		// Puts counter in DragBuffer. Call when mouse gesture recognized
		protected internal virtual void  startDrag()
		{
			
			// Recenter piece; panel may have been resized at some point resulting
			// in pieces with inaccurate positional information.
			//UPGRADE_NOTE: Final was removed from the declaration of 'size '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Size size = panel.Size;
			Piece.Position = new System.Drawing.Point(size.Width / 2, size.Height / 2);
			
			// Erase selection border to avoid leaving selected after mouse dragged out
			Piece.setProperty(VassalSharp.counters.Properties_Fields.SELECTED, (System.Object) null);
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			panel.Refresh();
			
			if (Piece != null)
			{
				KeyBuffer.Buffer.clear();
				DragBuffer.Buffer.clear();
				GamePiece newPiece = PieceCloner.Instance.clonePiece(Piece);
				newPiece.setProperty(VassalSharp.counters.Properties_Fields.PIECE_ID, GpId);
				DragBuffer.Buffer.add(newPiece);
			}
		}
		
		public virtual void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			if (Piece != null && e.isMetaDown())
			{
				//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				System.Windows.Forms.ContextMenu popup = MenuDisplayer.createPopup(Piece);
				//UPGRADE_NOTE: Some methods of the 'javax.swing.event.PopupMenuListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
				popup.Popup += new System.EventHandler(new AnonymousClassPopupMenuListener(this).popupMenuWillBecomeVisible);
				//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.show' was converted to 'System.Windows.Forms.ContextMenu.Show' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenushow_javaawtComponent_int_int'"
				popup.Show(panel, new System.Drawing.Point(e.X, e.Y));
			}
			
			clearExpandedPiece();
		}
		
		public virtual void  mouseClicked(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseEntered(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseExited(System.Object event_sender, System.EventArgs e)
		{
			KeyBuffer.Buffer.remove(Piece);
			clearExpandedPiece();
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			panel.Refresh();
		}
		
		public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.KeyStroke.getKeyStrokeForEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingKeyStrokegetKeyStrokeForEvent_javaawteventKeyEvent'"
			KeyBuffer.Buffer.keyCommand(KeyStroke.getKeyStrokeForEvent(event_sender, e));
			e.Handled = true;
			clearExpandedPiece();
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			panel.Refresh();
		}
		
		public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.KeyStroke.getKeyStrokeForEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingKeyStrokegetKeyStrokeForEvent_javaawteventKeyEvent'"
			KeyBuffer.Buffer.keyCommand(KeyStroke.getKeyStrokeForEvent(event_sender, e));
			e.Handled = true;
			clearExpandedPiece();
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			panel.Refresh();
		}
		
		public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.KeyStroke.getKeyStrokeForEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingKeyStrokegetKeyStrokeForEvent_javaawteventKeyEvent'"
			KeyBuffer.Buffer.keyCommand(KeyStroke.getKeyStrokeForEvent(event_sender, e));
			e.Handled = true;
			clearExpandedPiece();
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			panel.Refresh();
		}
		
		/// <summary> When building a PieceSlot, the text contents of the XML element are parsed
		/// into a String. The String is decoded using {@link GameModule#decode}. The
		/// resulting {@link Command} should be an instance of {@link AddPiece}. The
		/// piece referred to in the Command becomes the piece contained in the
		/// PieceSlot
		/// </summary>
		public virtual void  build(System.Xml.XmlElement e)
		{
			gpidSupport = GameModule.getGameModule().getGpIdSupport();
			if (e != null)
			{
				name = e.GetAttribute(NAME);
				gpId = e.GetAttribute(GP_ID) + "";
				if (name.Length == 0)
				{
					name = null;
				}
				try
				{
					width = System.Int32.Parse(e.GetAttribute(WIDTH));
					height = System.Int32.Parse(e.GetAttribute(HEIGHT));
				}
				catch (System.FormatException ex)
				{
					// Use default values.  Will be overwritten when module is saved
					width = 60;
					height = 60;
				}
				pieceDefinition = Builder.getText(e);
				c = null;
			}
		}
		
		public override void  addTo(Buildable parent)
		{
			//UPGRADE_ISSUE: Method 'java.awt.Component.setDropTarget' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentsetDropTarget_javaawtdndDropTarget'"
			panel.setDropTarget(AbstractDragHandler.makeDropTarget(panel, (int) System.Windows.Forms.DragDropEffects.Move, null));
			
			//UPGRADE_TODO: Interface 'java.awt.dnd.DragGestureListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			DragGestureListener dragGestureListener = new AnonymousClassDragGestureListener(this);
			//UPGRADE_ISSUE: Method 'java.awt.dnd.DragSource.getDefaultDragSource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000'"
			DragSource.getDefaultDragSource();
			panel.MouseDown += new System.Windows.Forms.MouseEventHandler(dragGestureListener.dragGestureRecognized);
		}
		
		public override System.Xml.XmlElement getBuildElement(System.Xml.XmlDocument doc)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'el '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Xml.XmlElement el = doc.CreateElement(GetType().FullName);
			//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String s = getConfigureName();
			if (s != null)
			{
				//UPGRADE_TODO: Method 'org.w3c.dom.Element.setAttribute' was converted to 'System.Xml.XmlElement.SetAttribute' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_orgw3cdomElementsetAttribute_javalangString_javalangString'"
				el.SetAttribute(NAME, s);
			}
			//UPGRADE_TODO: Method 'org.w3c.dom.Element.setAttribute' was converted to 'System.Xml.XmlElement.SetAttribute' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_orgw3cdomElementsetAttribute_javalangString_javalangString'"
			el.SetAttribute(GP_ID, gpId + "");
			//UPGRADE_TODO: Method 'org.w3c.dom.Element.setAttribute' was converted to 'System.Xml.XmlElement.SetAttribute' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_orgw3cdomElementsetAttribute_javalangString_javalangString'"
			el.SetAttribute(WIDTH, PreferredSize.Width + "");
			//UPGRADE_TODO: Method 'org.w3c.dom.Element.setAttribute' was converted to 'System.Xml.XmlElement.SetAttribute' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_orgw3cdomElementsetAttribute_javalangString_javalangString'"
			el.SetAttribute(HEIGHT, PreferredSize.Height + "");
			
			if (c != null || pieceDefinition != null)
			{
				el.appendChild(doc.createTextNode(c == null?pieceDefinition:GameModule.getGameModule().encode(new AddPiece(c))));
			}
			return el;
		}
		
		public override void  removeFrom(Buildable parent)
		{
		}
		
		public override System.String getConfigureName()
		{
			if (name != null)
			{
				return name;
			}
			else if (Piece != null)
			{
				return Decorator.getInnermost(Piece).getName();
			}
			else
			{
				return null;
			}
		}
		
		public override HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("GamePiece.htm");
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		
		public override void  setAttribute(System.String name, System.Object value_Renamed)
		{
		}
		
		/// <returns> an array of Configurer objects representing all possible classes of
		/// Buildable children of this Configurable object
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAllowableConfigureComponents()
		
		/*
		* Redirect getAttributeValueString() to return the attribute
		* values for the enclosed pieces
		*/
		public override System.String getAttributeValueString(System.String attr)
		{
			return I18nData.getLocalUntranslatedValue(attr);
		}
		
		/// <summary> Update the gpid for this PieceSlot, using the given {@link GpIdSupport}
		/// to generate the new id.
		/// </summary>
		public virtual void  updateGpId(GpIdSupport s)
		{
			gpidSupport = s;
			updateGpId();
		}
		
		/// <summary> Allocate a new gpid to this PieceSlot, plus to any PlaceMarker or
		/// Replace traits.
		/// </summary>
		public virtual void  updateGpId()
		{
			gpId = gpidSupport.generateGpId();
			GamePiece piece = Piece;
			updateGpId(piece);
			Piece = piece;
		}
		
		/// <summary> Allocate new gpid's in the given GamePiece
		/// 
		/// </summary>
		/// <param name="piece">GamePiece
		/// </param>
		public virtual void  updateGpId(GamePiece piece)
		{
			if (piece == null || piece is BasicPiece)
			{
				return ;
			}
			if (piece is PlaceMarker)
			{
				((PlaceMarker) piece).GpId = gpidSupport.generateGpId();
			}
			updateGpId(((Decorator) piece).getInner());
		}
		
		private class MyConfigurer:Configurer, HelpWindowExtension
		{
			virtual public HelpWindow BaseWindow
			{
				set
				{
				}
				
			}
			override public System.String ValueString
			{
				get
				{
					return null;
				}
				
			}
			override public System.Windows.Forms.Control Controls
			{
				get
				{
					return definer;
				}
				
			}
			private PieceDefiner definer;
			
			public MyConfigurer(PieceSlot slot):base(null, slot.getConfigureName(), slot)
			{
				definer = new PieceDefiner(slot.GpId, slot.gpidSupport);
				definer.setPiece(slot.Piece);
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Deprecated
			
			public override void  setValue(System.String s)
			{
				throw new System.NotSupportedException("Cannot set from String");
			}
			
			public override System.Object getValue()
			{
				PieceSlot slot = (PieceSlot) base.getValue();
				if (slot != null)
				{
					slot.Piece = definer.getPiece();
				}
				return slot;
			}
		}
	}
}