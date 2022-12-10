/*
* $Id$
*
* Copyright (c) 2006 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.awt.event_Renamed.MouseWheelEvent' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MouseWheelEvent = java.awt.event_Renamed.MouseWheelEvent;
//UPGRADE_TODO: The type 'java.awt.event_Renamed.MouseWheelListener' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MouseWheelListener = java.awt.event_Renamed.MouseWheelListener;
namespace VassalSharp.tools
{
	
	/// <summary>ScrollPane extends JScrollPane to have complete mouse-wheel functionality.
	/// The mouse wheel scrolls vertically when hovering over the view or vertical
	/// scrollbar, and horizontally when CTRL is depressed or when hovering over
	/// the horizontal scrollbar. ScrollPane uses the system default scroll speed.
	/// For scrollpanes which will contain large images (e.g., maps) use
	/// {@link AdjustableSpeedScrollPane}, which has a user-configurable scroll
	/// speed, instead.
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <seealso cref="javax.swing.JScrollPane">
	/// </seealso>
	/// <seealso cref="VassalSharp.tools.AdjustableSpeedScrollPane">
	/// </seealso>
	//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
	[Serializable]
	public class ScrollPane:System.Windows.Forms.ScrollableControl
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMouseWheelListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassMouseWheelListener : MouseWheelListener
		{
			public AnonymousClassMouseWheelListener(ScrollPane enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ScrollPane enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ScrollPane enclosingInstance;
			public ScrollPane Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  mouseWheelMoved(MouseWheelEvent e)
			{
				if (e.getScrollAmount() == 0)
					return ;
				
				if (e.getScrollType() == MouseWheelEvent.WHEEL_UNIT_SCROLL)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'bar '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_TODO: Class 'javax.swing.JScrollBar' was converted to 'System.Windows.Forms.ScrollBar' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					//UPGRADE_ISSUE: Field 'javax.swing.JScrollPane.horizontalScrollBar' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanehorizontalScrollBar_f'"
					//UPGRADE_ISSUE: Field 'javax.swing.JScrollPane.verticalScrollBar' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPaneverticalScrollBar_f'"
					System.Windows.Forms.ScrollBar bar = e.isShiftDown()?Enclosing_Instance.horizontalScrollBar:Enclosing_Instance.verticalScrollBar;
					//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
					if (bar == null || !bar.Visible)
						return ;
					
					int temp_Maxsize;
					//UPGRADE_WARNING: Method javax.swing.JScrollbar.setValue was converted to 'System.Windows.Forms.ScrollBar.Value' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
					temp_Maxsize = bar.Maximum - bar.LargeChange;
					//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
					bar.Value = bar.Value + e.getUnitsToScroll() * bar.SmallChange > temp_Maxsize?temp_Maxsize:bar.Value + e.getUnitsToScroll() * bar.SmallChange;
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMouseWheelListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassMouseWheelListener1 : MouseWheelListener
		{
			public AnonymousClassMouseWheelListener1(ScrollPane enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ScrollPane enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ScrollPane enclosingInstance;
			public ScrollPane Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  mouseWheelMoved(MouseWheelEvent e)
			{
				if (e.getScrollAmount() == 0)
					return ;
				
				if (e.getScrollType() == MouseWheelEvent.WHEEL_UNIT_SCROLL)
				{
					int temp_Maxsize;
					//UPGRADE_ISSUE: Field 'javax.swing.JScrollPane.verticalScrollBar' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPaneverticalScrollBar_f'"
					//UPGRADE_WARNING: Method javax.swing.JScrollbar.setValue was converted to 'System.Windows.Forms.ScrollBar.Value' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
					temp_Maxsize = Enclosing_Instance.verticalScrollBar.Maximum - Enclosing_Instance.verticalScrollBar.LargeChange;
					//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
					Enclosing_Instance.verticalScrollBar.Value = Enclosing_Instance.verticalScrollBar.Value + e.getUnitsToScroll() * Enclosing_Instance.verticalScrollBar.SmallChange > temp_Maxsize?temp_Maxsize:Enclosing_Instance.verticalScrollBar.Value + e.getUnitsToScroll() * Enclosing_Instance.verticalScrollBar.SmallChange;
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMouseWheelListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassMouseWheelListener2 : MouseWheelListener
		{
			public AnonymousClassMouseWheelListener2(ScrollPane enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ScrollPane enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ScrollPane enclosingInstance;
			public ScrollPane Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  mouseWheelMoved(MouseWheelEvent e)
			{
				if (e.getScrollAmount() == 0)
					return ;
				
				if (e.getScrollType() == MouseWheelEvent.WHEEL_UNIT_SCROLL)
				{
					int temp_Maxsize;
					//UPGRADE_ISSUE: Field 'javax.swing.JScrollPane.horizontalScrollBar' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanehorizontalScrollBar_f'"
					//UPGRADE_WARNING: Method javax.swing.JScrollbar.setValue was converted to 'System.Windows.Forms.ScrollBar.Value' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
					temp_Maxsize = Enclosing_Instance.horizontalScrollBar.Maximum - Enclosing_Instance.horizontalScrollBar.LargeChange;
					//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
					Enclosing_Instance.horizontalScrollBar.Value = Enclosing_Instance.horizontalScrollBar.Value + e.getUnitsToScroll() * Enclosing_Instance.horizontalScrollBar.SmallChange > temp_Maxsize?temp_Maxsize:Enclosing_Instance.horizontalScrollBar.Value + e.getUnitsToScroll() * Enclosing_Instance.horizontalScrollBar.SmallChange;
				}
			}
		}
		private const long serialVersionUID = 1L;
		
		/// <summary>Creates an empty (no viewport view) ScrollPane where both horizontal
		/// and vertical scrollbars appear when needed.
		/// </summary>
		//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.VERTICAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsVERTICAL_SCROLLBAR_AS_NEEDED_f'"
		//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.HORIZONTAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsHORIZONTAL_SCROLLBAR_AS_NEEDED_f'"
		public ScrollPane():this(null, true, true)
		{
		}
		
		/// <summary>Creates a ScrollPane that displays the contents of the specified
		/// component, where both horizontal and vertical scrollbars appear whenever
		/// the component's contents are larger than the view.
		/// </summary>
		/// <param name="view">the component to display in the scrollpane's viewport
		/// </param>
		//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.VERTICAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsVERTICAL_SCROLLBAR_AS_NEEDED_f'"
		//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.HORIZONTAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsHORIZONTAL_SCROLLBAR_AS_NEEDED_f'"
		public ScrollPane(System.Windows.Forms.Control view):this(view, true, true)
		{
		}
		
		/// <summary>Creates an empty (no viewport view) ScrollPane with specified scrollbar
		/// policies. The available policy settings are listed at
		/// {@link JScrollPane#setVerticalScrollBarPolicy} and
		/// {@link JScrollPane#setHorizontalScrollBarPolicy}.
		/// </summary>
		/// <param name="vsbPolicy">an integer that specifies the vertical scrollbar policy
		/// </param>
		/// <param name="hsbPolicy">an integer that specifies the horizontal scrollbar policy
		/// </param>
		public ScrollPane(int vsbPolicy, int hsbPolicy):this(null, vsbPolicy, hsbPolicy)
		{
		}
		
		/// <summary>Creates a ScrollPane that displays the view component in a viewport with
		/// the specified scrollbar policies. The available policy settings are
		/// listed at {@link JScrollPane#setVerticalScrollBarPolicy} and
		/// {@link JScrollPane#setHorizontalScrollBarPolicy}.
		/// </summary>
		/// <param name="view">the component to display in the scrollpane's viewport
		/// </param>
		/// <param name="vsbPolicy">an integer that specifies the vertical scrollbar policy
		/// </param>
		/// <param name="hsbPolicy">an integer that specifies the horizontal scrollbar policy
		/// </param>
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public ScrollPane(System.Windows.Forms.Control view, int vsbPolicy, int hsbPolicy):base()
		{
			this.AutoScroll = vsbPolicy;
			this.Controls.Add(view);
			
			// clear existing MouseWheelListeners
			MouseWheelListener[] listeners;
			
			listeners = getMouseWheelListeners();
			for (int i = 0; i < listeners.length; ++i)
				removeMouseWheelListener(listeners[i]);
			
			listeners = viewport.getMouseWheelListeners();
			for (int i = 0; i < listeners.length; ++i)
				viewport.removeMouseWheelListener(listeners[i]);
			
			listeners = verticalScrollBar.getMouseWheelListeners();
			for (int i = 0; i < listeners.length; ++i)
				verticalScrollBar.removeMouseWheelListener(listeners[i]);
			
			listeners = horizontalScrollBar.getMouseWheelListeners();
			for (int i = 0; i < listeners.length; ++i)
				horizontalScrollBar.removeMouseWheelListener(listeners[i]);
			
			// add our own MouseWheelListeners
			// NB: block scrolling isn't used with the mouse wheel
			viewport.addMouseWheelListener(new AnonymousClassMouseWheelListener(this));
			
			verticalScrollBar.addMouseWheelListener(new AnonymousClassMouseWheelListener1(this));
			
			horizontalScrollBar.addMouseWheelListener(new AnonymousClassMouseWheelListener2(this));
		}
	}
}