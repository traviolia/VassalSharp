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
using Map = VassalSharp.build.module.Map;
namespace VassalSharp.counters
{
	
	public class DragBuffer
	{
		static private System.Int32 state484;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMouseAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassMouseAdapter
		{
			public AnonymousClassMouseAdapter(DragBuffer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(DragBuffer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private DragBuffer enclosingInstance;
			public DragBuffer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
			{
				Enclosing_Instance.lastRelease = null;
				Enclosing_Instance.dropTarget = null;
				Enclosing_Instance.dropHandler = null;
			}
			
			public void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
			{
				((System.Windows.Forms.Control) event_sender).Cursor = null;
				System.Windows.Forms.Control source = (System.Windows.Forms.Control) event_sender;
				if (Enclosing_Instance.dropTarget == null)
				{
					//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.translatePoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventtranslatePoint_int_int'"
					System.Windows.Forms.Control temp_Control;
					//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.awt.Component.getLocationOnScreen' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
					temp_Control = source;
					System.Windows.Forms.Control temp_Control2;
					temp_Control2 = source;
					e.translatePoint(temp_Control.PointToScreen(temp_Control.Location).X, temp_Control2.PointToScreen(temp_Control2.Location).Y);
					Enclosing_Instance.lastRelease = e;
				}
				else
				{
					//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.translatePoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventtranslatePoint_int_int'"
					System.Windows.Forms.Control temp_Control3;
					//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.awt.Component.getLocationOnScreen' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
					temp_Control3 = source;
					System.Windows.Forms.Control temp_Control4;
					temp_Control4 = source;
					e.translatePoint(temp_Control3.PointToScreen(temp_Control3.Location).X, temp_Control4.PointToScreen(temp_Control4.Location).Y);
					//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.translatePoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventtranslatePoint_int_int'"
					System.Windows.Forms.Control temp_Control5;
					//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.awt.Component.getLocationOnScreen' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
					temp_Control5 = Enclosing_Instance.dropTarget;
					System.Windows.Forms.Control temp_Control6;
					temp_Control6 = Enclosing_Instance.dropTarget;
					e.translatePoint(- temp_Control5.PointToScreen(temp_Control5.Location).X, - temp_Control6.PointToScreen(temp_Control6.Location).Y);
					//UPGRADE_TODO: Method 'java.awt.event.MouseListener.mouseReleased' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					Enclosing_Instance.dropHandler.mouseReleased(event_sender, e);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMouseAdapter1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassMouseAdapter1
		{
			public AnonymousClassMouseAdapter1(java.awt.event_Renamed.MouseListener l, DragBuffer enclosingInstance)
			{
				InitBlock(l, enclosingInstance);
			}
			private void  InitBlock(java.awt.event_Renamed.MouseListener l, DragBuffer enclosingInstance)
			{
				this.l = l;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_TODO: Interface 'java.awt.event.MouseListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			//UPGRADE_NOTE: Final variable l was copied into class AnonymousClassMouseAdapter1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private java.awt.event_Renamed.MouseListener l;
			private DragBuffer enclosingInstance;
			public DragBuffer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  mouseEntered(System.Object event_sender, System.EventArgs e)
			{
				System.Windows.Forms.Control source = (System.Windows.Forms.Control) event_sender;
				if (source.Visible)
				{
					if (Enclosing_Instance.lastRelease != null)
					{
						//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.translatePoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventtranslatePoint_int_int'"
						System.Windows.Forms.Control temp_Control;
						//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.awt.Component.getLocationOnScreen' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
						temp_Control = source;
						System.Windows.Forms.Control temp_Control2;
						temp_Control2 = source;
						e.translatePoint(temp_Control.PointToScreen(temp_Control.Location).X, temp_Control2.PointToScreen(temp_Control2.Location).Y);
						//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
						System.Drawing.Point tempAux = e.getPoint();
						//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
						System.Drawing.Point tempAux2 = Enclosing_Instance.lastRelease.getPoint();
						if (Enclosing_Instance.isCloseEnough(ref tempAux, ref tempAux2))
						{
							//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.translatePoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventtranslatePoint_int_int'"
							System.Windows.Forms.Control temp_Control3;
							//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.awt.Component.getLocationOnScreen' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
							temp_Control3 = source;
							System.Windows.Forms.Control temp_Control4;
							temp_Control4 = source;
							e.translatePoint(- temp_Control3.PointToScreen(temp_Control3.Location).X, - temp_Control4.PointToScreen(temp_Control4.Location).Y);
							//UPGRADE_TODO: Method 'java.awt.event.MouseListener.mouseReleased' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
							//UPGRADE_TODO: Interface 'java.awt.event.MouseListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
							l.mouseReleased(event_sender, e);
						}
					}
					else
					{
						Enclosing_Instance.dropTarget = source;
						//UPGRADE_TODO: Interface 'java.awt.event.MouseListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
						Enclosing_Instance.dropHandler = l;
					}
				}
			}
		}
		private class AnonymousClassMouseAdapter2
		{
			public void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs evt)
			{
				//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.translatePoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventtranslatePoint_int_int'"
				System.Windows.Forms.Control temp_Control;
				//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.awt.Component.getLocationOnScreen' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
				temp_Control = ((System.Windows.Forms.Form) event_sender);
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = ((System.Windows.Forms.Form) event_sender);
				evt.translatePoint(temp_Control.PointToScreen(temp_Control.Location).X, temp_Control2.PointToScreen(temp_Control2.Location).Y);
				//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Point.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Error.WriteLine("Press at " + new System.Drawing.Point(evt.X, evt.Y));
			}
			
			public void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs evt)
			{
				//        evt.translatePoint(((JFrame)evt.getSource()).getLocationOnScreen().x,((JFrame)evt.getSource()).getLocationOnScreen().y);
				//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Point.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Error.WriteLine("Release at " + new System.Drawing.Point(evt.X, evt.Y));
			}
			
			public void  mouseEntered(System.Object event_sender, System.EventArgs evt)
			{
				//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.translatePoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventtranslatePoint_int_int'"
				System.Windows.Forms.Control temp_Control;
				//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.awt.Component.getLocationOnScreen' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
				temp_Control = ((System.Windows.Forms.Form) event_sender);
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = ((System.Windows.Forms.Form) event_sender);
				evt.translatePoint(temp_Control.PointToScreen(temp_Control.Location).X, temp_Control2.PointToScreen(temp_Control2.Location).Y);
				//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Point.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
				System.Console.Error.WriteLine("Enter at " + evt.getPoint());
			}
		}
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state484 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private void  InitBlock()
		{
			Collections.sort(pieces, comp);
		}
		public static DragBuffer Buffer
		{
			get
			{
				return theBuffer;
			}
			
		}
		virtual public Map FromMap
		{
			get
			{
				return dragFromMap;
			}
			
		}
		virtual public PieceIterator Iterator
		{
			get
			{
				return new PieceIterator(pieces.iterator());
			}
			
		}
		virtual public bool Empty
		{
			get
			{
				return pieces.Empty;
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'theBuffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly DragBuffer theBuffer = new DragBuffer();
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private final List < GamePiece > pieces = new ArrayList < GamePiece >();
		private System.Windows.Forms.MouseEventArgs lastRelease;
		private System.Windows.Forms.Control dropTarget;
		//UPGRADE_TODO: Interface 'java.awt.event.MouseListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		private MouseListener dropHandler;
		private Map dragFromMap;
		
		private DragBuffer()
		{
			InitBlock();
		}
		
		public virtual void  add(GamePiece p)
		{
			if (p != null && !pieces.contains(p) && !true.Equals(p.getProperty(VassalSharp.counters.Properties_Fields.RESTRICTED_MOVEMENT)))
			{
				if (p is Stack)
				{
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				}
				pieces.add(p);
				dragFromMap = p.getMap();
			}
		}
		
		public virtual void  clear()
		{
			pieces.clear();
		}
		
		public virtual void  addDragSource(System.Windows.Forms.Control c)
		{
			c.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.counters.DragBuffer.mouseDown);
			c.MouseDown += new System.Windows.Forms.MouseEventHandler(new AnonymousClassMouseAdapter(this).mousePressed);
			c.MouseUp += new System.Windows.Forms.MouseEventHandler(new AnonymousClassMouseAdapter(this).mouseReleased);
		}
		
		//UPGRADE_TODO: Interface 'java.awt.event.MouseListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  addDropTarget(System.Windows.Forms.Control c, MouseListener l)
		{
			c.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.counters.DragBuffer.mouseDown);
			c.MouseEnter += new System.EventHandler(new AnonymousClassMouseAdapter1(l, this).mouseEntered);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		private bool isCloseEnough(ref System.Drawing.Point p1, ref System.Drawing.Point p2)
		{
			return System.Math.Abs(p1.X - p2.X) < 3 && System.Math.Abs(p1.Y - p2.Y) < 3;
		}
		
		public virtual void  remove(GamePiece p)
		{
			pieces.remove(p);
		}
		
		public virtual bool contains(GamePiece p)
		{
			return pieces.contains(p);
		}
		
		public virtual System.String contents()
		{
			System.String s = "";
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			return s;
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			System.Windows.Forms.Form f1 = new System.Windows.Forms.Form();
			//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
			f1.Size = new System.Drawing.Size(200, 200);
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			f1.Visible = true;
			System.Windows.Forms.Form f2 = new System.Windows.Forms.Form();
			//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
			f2.Size = new System.Drawing.Size(200, 200);
			//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_int_int'"
			f2.Location = new System.Drawing.Point(200, 0);
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			f2.Visible = true;
			//UPGRADE_TODO: Interface 'java.awt.event.MouseListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			MouseListener l = new AnonymousClassMouseAdapter2();
			DragBuffer.Buffer.addDragSource(f1);
			DragBuffer.Buffer.addDropTarget(f2, l);
		}
		
		public virtual System.Windows.Forms.Cursor createDragCursor(System.Windows.Forms.Control comp)
		{
			System.Windows.Forms.Cursor c = null;
			if (!pieces.Empty)
			{
				//UPGRADE_ISSUE: Member 'java.awt.Cursor.getPredefinedCursor' was converted to 'System.Windows.Forms.Cursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
				//UPGRADE_ISSUE: Member 'java.awt.Cursor.HAND_CURSOR' was converted to 'System.Windows.Forms.Cursors.Hand' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
				c = System.Windows.Forms.Cursors.Hand;
			}
			return c;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void sort(Comparator < GamePiece > comp)
		
		/// <deprecated> Use {@link #sort(Comparator)} instead.
		/// </deprecated>
		internal Deprecated SuppressWarnings;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(unchecked)
		public virtual void  sort(VassalSharp.tools.Sort.Comparator comp)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			sort((Comparator < GamePiece >) comp);
		}
		
		/// <deprecated> 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public static void  init(DragBuffer db)
		{
		}
	}
}