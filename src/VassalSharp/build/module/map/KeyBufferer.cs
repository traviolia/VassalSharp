/*
* $Id$
*
* Copyright (c) 2000-2007 by Rodney Kinney, Joel Uckelman
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
using GameModule = VassalSharp.build.GameModule;
using Map = VassalSharp.build.module.Map;
using ColoredBorder = VassalSharp.counters.ColoredBorder;
using Deck = VassalSharp.counters.Deck;
using DeckVisitor = VassalSharp.counters.DeckVisitor;
using EventFilter = VassalSharp.counters.EventFilter;
using GamePiece = VassalSharp.counters.GamePiece;
using Immobilized = VassalSharp.counters.Immobilized;
using KeyBuffer = VassalSharp.counters.KeyBuffer;
using PieceFinder = VassalSharp.counters.PieceFinder;
using PieceVisitorDispatcher = VassalSharp.counters.PieceVisitorDispatcher;
using Properties = VassalSharp.counters.Properties;
using Stack = VassalSharp.counters.Stack;
namespace VassalSharp.build.module.map
{
	
	/// <summary> This component listens for mouse clicks on a map and draws the selection
	/// rectangle.
	/// 
	/// If the user clicks on a {@link GamePiece}, that piece is added to the
	/// {@link KeyBuffer}. {@link #draw(Graphics, Map)} is responsible for
	/// drawing the mouse selection rectangle, and
	/// {@link #mouseDragged(MouseEvent)} is responsible for triggering repaint
	/// events as the selection rectangle is moved.
	/// 
	/// </summary>
	/// <seealso cref="Map.addLocalMouseListener">
	/// </seealso>
	public class KeyBufferer : Buildable, Drawable
	{
		protected internal Map map;
		protected internal System.Drawing.Rectangle selection;
		protected internal System.Drawing.Point anchor;
		protected internal System.Drawing.Color color = System.Drawing.Color.Black;
		protected internal int thickness = 3;
		
		public virtual void  addTo(Buildable b)
		{
			map = (Map) b;
			map.addLocalMouseListenerFirst(this);
			map.getView().addMouseMotionListener(this);
			map.addDrawComponent(this);
		}
		
		public virtual void  add(Buildable b)
		{
		}
		
		public virtual System.Xml.XmlElement getBuildElement(System.Xml.XmlDocument doc)
		{
			return doc.CreateElement(GetType().FullName);
		}
		
		public virtual void  build(System.Xml.XmlElement e)
		{
		}
		
		public void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isConsumed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			if (e.isConsumed())
			{
				return ;
			}
			GamePiece p = map.findPiece(new System.Drawing.Point(e.X, e.Y), VassalSharp.counters.PieceFinder_Fields.PIECE_IN_STACK);
			// Don't clear the buffer until we find the clicked-on piece
			// Because selecting a piece affects its visibility
			EventFilter filter = null;
			if (p != null)
			{
				filter = (EventFilter) p.getProperty(VassalSharp.counters.Properties_Fields.SELECT_EVENT_FILTER);
			}
			bool ignoreEvent = filter != null && filter.rejectEvent(event_sender, e);
			if (p != null && !ignoreEvent)
			{
				bool movingStacksPickupUnits = ((System.Boolean) GameModule.getGameModule().getPrefs().getValue(Map.MOVING_STACKS_PICKUP_UNITS));
				if (!KeyBuffer.Buffer.contains(p))
				{
					if (!(System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift) && !(System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control))
					{
						KeyBuffer.Buffer.clear();
					}
					// RFE 1629255 - If the top piece of an unexpanded stack is left-clicked
					// while not selected, then select all of the pieces in the stack
					// RFE 1659481 - Control clicking only deselects
					if (!(System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control))
					{
						//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.isMetaDown' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
						if (movingStacksPickupUnits || p.Parent == null || p.Parent.isExpanded() || e.isMetaDown() || true.Equals(p.getProperty(VassalSharp.counters.Properties_Fields.SELECTED)))
						{
							KeyBuffer.Buffer.add(p);
						}
						else
						{
							Stack s = p.Parent;
							for (int i = 0; i < s.PieceCount; i++)
							{
								KeyBuffer.Buffer.add(s.getPieceAt(i));
							}
						}
					}
					// End RFE 1629255
				}
				else
				{
					// RFE 1659481 Ctrl-click deselects clicked units
					if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control) && true.Equals(p.getProperty(VassalSharp.counters.Properties_Fields.SELECTED)))
					{
						Stack s = p.Parent;
						if (s == null)
						{
							KeyBuffer.Buffer.remove(p);
						}
						else if (!s.isExpanded())
						{
							for (int i = 0; i < s.PieceCount; i++)
							{
								KeyBuffer.Buffer.remove(s.getPieceAt(i));
							}
						}
					}
					// End RFE 1659481
				}
				if (p.Parent != null)
				{
					map.getPieceCollection().moveToFront(p.Parent);
				}
				else
				{
					map.getPieceCollection().moveToFront(p);
				}
			}
			else
			{
				if (!(System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift) && !(System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control))
				{
					// No deselect if shift key down
					KeyBuffer.Buffer.clear();
				}
				anchor = map.componentCoordinates(new System.Drawing.Point(e.X, e.Y));
				selection = new System.Drawing.Rectangle(anchor.X, anchor.Y, 0, 0);
				if (map.getHighlighter() is ColoredBorder)
				{
					ColoredBorder b = (ColoredBorder) map.getHighlighter();
					color = b.Color;
					thickness = b.Thickness;
				}
			}
		}
		
		public void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs evt)
		{
			if (!selection.IsEmpty)
			{
				selection.setLocation(map.mapCoordinates(selection.Location));
				selection.Width /= (int) (map.Zoom);
				selection.Height /= (int) (map.Zoom);
				PieceVisitorDispatcher d = createDragSelector(!(System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control), (System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Alt));
				// RFE 1659481 Don't clear the entire selection buffer if either shift
				// or control is down - we select/deselect lassoed counters instead
				if (!(System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift) && !(System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control))
				{
					KeyBuffer.Buffer.clear();
				}
				map.apply(d);
				repaintSelectionRect();
			}
			selection = System.Drawing.Rectangle.Empty;
		}
		
		/// <summary> This PieceVisitorDispatcher determines what to do with pieces on the
		/// map when the player finished dragging a rectangle to select pieces
		/// 
		/// </summary>
		/// <returns>
		/// </returns>
		protected internal virtual PieceVisitorDispatcher createDragSelector(bool selecting, bool altDown)
		{
			return new PieceVisitorDispatcher(new KBDeckVisitor(this, selecting, altDown));
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'KBDeckVisitor' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		public class KBDeckVisitor : DeckVisitor
		{
			private void  InitBlock(KeyBufferer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private KeyBufferer enclosingInstance;
			public KeyBufferer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			internal bool selecting = false;
			internal bool altDown = false;
			
			public KBDeckVisitor(KeyBufferer enclosingInstance, bool b, bool c)
			{
				InitBlock(enclosingInstance);
				selecting = b;
				altDown = c;
			}
			
			public virtual System.Object visitDeck(Deck d)
			{
				return null;
			}
			
			public virtual System.Object visitStack(Stack s)
			{
				if (s.topPiece() != null)
				{
					if (s.isExpanded())
					{
						System.Drawing.Point[] pos = new System.Drawing.Point[s.PieceCount];
						Enclosing_Instance.map.StackMetrics.getContents(s, pos, null, null, s.Position.X, s.Position.Y);
						for (int i = 0; i < pos.Length; ++i)
						{
							if (Enclosing_Instance.selection.Contains(pos[i]))
							{
								if (selecting)
								{
									KeyBuffer.Buffer.add(s.getPieceAt(i));
								}
								else
								{
									KeyBuffer.Buffer.remove(s.getPieceAt(i));
								}
							}
						}
					}
					else if (Enclosing_Instance.selection.Contains(s.Position))
					{
						for (int i = 0, n = s.PieceCount; i < n; ++i)
						{
							if (selecting)
							{
								KeyBuffer.Buffer.add(s.getPieceAt(i));
							}
							else
							{
								KeyBuffer.Buffer.remove(s.getPieceAt(i));
							}
						}
					}
				}
				return null;
			}
			
			// Handle non-stacked units, including Does Not Stack units
			// Does Not Stack units deselect normally once selected
			public virtual System.Object visitDefault(GamePiece p)
			{
				if (Enclosing_Instance.selection.Contains(p.Position) && !true.Equals(p.getProperty(VassalSharp.counters.Properties_Fields.INVISIBLE_TO_ME)))
				{
					if (selecting)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'filter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						EventFilter filter = (EventFilter) p.getProperty(VassalSharp.counters.Properties_Fields.SELECT_EVENT_FILTER);
						//UPGRADE_NOTE: Final was removed from the declaration of 'altSelect '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						bool altSelect = (altDown && filter is Immobilized.UseAlt);
						if (filter == null || altSelect)
						{
							KeyBuffer.Buffer.add(p);
						}
					}
					else
					{
						KeyBuffer.Buffer.remove(p);
					}
				}
				return null;
			}
		}
		
		protected internal virtual void  repaintSelectionRect()
		{
			/*
			* Repaint strategy: There is no reason to repaint the interior of
			* the selection rectangle, as we didn't paint over it in the first
			* place. Instead, we repaint only the four (slender) rectangles
			* which the stroke of the selection rectangle filled. We have to
			* call a repaint on both the old selection rectangle and the new
			* in order to prevent drawing artifacts. The KeyBuffer handles
			* repainting pieces which have been (de)selected, so we don't
			* worry about those.
			*
			* Area drawn:
			*                selection.x
			*                     |
			*                  ___________________
			*   selection.y __ |__|__|_____|__|__| __
			*                  |__|__|_____|__|__|   |
			*                  |  |  |     |  |  |   |
			*                  |  |  |     |  |  |   | selection.height
			*                  |__|__|_____|__|__|   |
			* ~thickness/2 --{ |__|__|_____|__|__| __|
			* ~thickness/2 --{ |__|__|_____|__|__|
			*
			*                     |___________|
			*                    selection.width
			*/
			//UPGRADE_NOTE: Final was removed from the declaration of 'ht '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int ht = thickness / 2 + thickness % 2;
			//UPGRADE_NOTE: Final was removed from the declaration of 'ht2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int ht2 = 2 * ht;
			
			// left
			map.getView().repaint(selection.X - ht, selection.Y - ht, ht2, selection.Height + ht2);
			// right
			map.getView().repaint(selection.X + selection.Width - ht, selection.Y - ht, ht2, selection.Height + ht2);
			// top
			map.getView().repaint(selection.X - ht, selection.Y - ht, selection.Width + ht2, ht2);
			// bottom
			map.getView().repaint(selection.X - ht, selection.Y + selection.Width - ht, selection.Width + ht2, ht2);
		}
		
		/// <summary> Sets the new location of the selection rectangle.</summary>
		public virtual void  mouseDragged(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (!selection.IsEmpty)
			{
				repaintSelectionRect();
				
				selection.X = System.Math.Min(e.X, anchor.X);
				selection.Y = System.Math.Min(e.Y, anchor.Y);
				selection.Width = System.Math.Abs(e.X - anchor.X);
				selection.Height = System.Math.Abs(e.Y - anchor.Y);
				
				repaintSelectionRect();
			}
		}
		
		public virtual void  mouseMoved(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
		}
		
		public virtual void  draw(System.Drawing.Graphics g, Map map)
		{
			if (!selection.IsEmpty)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'g2d '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Graphics g2d = (System.Drawing.Graphics) g;
				//UPGRADE_NOTE: Final was removed from the declaration of 'str '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.getStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DgetStroke'"
				System.Drawing.Pen str = SupportClass.GraphicsManager.manager.GetPen(g2d);
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.setStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DsetStroke_javaawtStroke'"
				//UPGRADE_TODO: Constructor 'java.awt.BasicStroke.BasicStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtBasicStrokeBasicStroke_float'"
				SupportClass.GraphicsManager.manager.SetPen(g2d, new System.Drawing.Pen(System.Drawing.Brushes.Black, thickness));
				SupportClass.GraphicsManager.manager.SetColor(g2d, color);
				g2d.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g2d), selection.X, selection.Y, selection.Width, selection.Height);
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.setStroke' was converted to 'System.Drawing.Pen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DsetStroke_javaawtStroke'"
				SupportClass.GraphicsManager.manager.SetPen(g2d, str);
			}
		}
		
		public virtual bool drawAboveCounters()
		{
			return true;
		}
	}
}