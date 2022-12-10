/*
* $Id: MenuScroller.java 7738 2011-08-03 18:38:35Z uckelman $
*
* Copyright (c) 2012 by Brent Easton
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

/// <(#)MenuScroller.java>   1.5.0 04/02/12 </(#)MenuScroller.java>
using System;
namespace VassalSharp.tools.menu
{
	
	/// <summary> A class that provides scrolling capabilities to a long menu dropdown or
	/// popup menu.  A number of items can optionally be frozen at the top and/or
	/// bottom of the menu.
	/// <P>
	/// <B>Implementation note:</B>  The default number of items to display
	/// at a time is 15, and the default scrolling interval is 125 milliseconds.
	/// <P>
	/// 
	/// </summary>
	/// <version>  1.5.0 04/05/12
	/// </version>
	/// <author>  Darryl
	/// </author>
	public class MenuScroller
	{
		private void  InitBlock()
		{
			menuListener = new MenuScrollListener(this);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			UP(9, 1, 9), 
			DOWN(1, 9, 1);
			//UPGRADE_NOTE: Final was removed from the declaration of 'xPoints '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int[] xPoints = new int[]{1, 5, 9};
			//UPGRADE_NOTE: Final was removed from the declaration of 'yPoints '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int[] yPoints;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			MenuIcon(int...yPoints)
			{
				this.yPoints = yPoints;
			}
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the scroll interval in milliseconds
		/// 
		/// </summary>
		/// <returns> the scroll interval in milliseconds
		/// </returns>
		/// <summary> Sets the scroll interval in milliseconds
		/// 
		/// </summary>
		/// <param name="interval">the scroll interval in milliseconds
		/// </param>
		/// <throws>  IllegalArgumentException if interval is 0 or negative </throws>
		virtual public int Interval
		{
			get
			{
				return interval;
			}
			
			set
			{
				if (value <= 0)
				{
					throw new System.ArgumentException("interval must be greater than 0");
				}
				upItem.Interval = value;
				downItem.Interval = value;
				this.interval = value;
			}
			
		}
		/// <summary> Sets the number of items in the scrolling portion of the menu.
		/// 
		/// </summary>
		/// <param name="scrollCount">the number of items to display at a time
		/// </param>
		/// <throws>  IllegalArgumentException if scrollCount is 0 or negative </throws>
		virtual public int ScrollCount
		{
			set
			{
				if (value <= 0)
				{
					throw new System.ArgumentException("scrollCount must be greater than 0");
				}
				this.scrollCount = value;
				//UPGRADE_ISSUE: Method 'javax.swing.MenuSelectionManager.clearSelectedPath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingMenuSelectionManager'"
				//UPGRADE_ISSUE: Method 'javax.swing.MenuSelectionManager.defaultManager' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingMenuSelectionManager'"
				MenuSelectionManager.defaultManager().clearSelectedPath();
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the number of items fixed at the top of the menu or popup menu.
		/// 
		/// </summary>
		/// <returns> the number of items
		/// </returns>
		/// <summary> Sets the number of items to fix at the top of the menu or popup menu.
		/// 
		/// </summary>
		/// <param name="topFixedCount">the number of items
		/// </param>
		virtual public int TopFixedCount
		{
			get
			{
				return topFixedCount;
			}
			
			set
			{
				if (firstIndex <= value)
				{
					firstIndex = value;
				}
				else
				{
					firstIndex += (value - this.topFixedCount);
				}
				this.topFixedCount = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Returns the number of items fixed at the bottom of the menu or popup menu.
		/// 
		/// </summary>
		/// <returns> the number of items
		/// </returns>
		/// <summary> Sets the number of items to fix at the bottom of the menu or popup menu.
		/// 
		/// </summary>
		/// <param name="bottomFixedCount">the number of items
		/// </param>
		virtual public int BottomFixedCount
		{
			get
			{
				return bottomFixedCount;
			}
			
			set
			{
				this.bottomFixedCount = value;
			}
			
		}
		virtual public int IconWidth
		{
			get
			{
				return 0;
			}
			
		}
		virtual public int IconHeight
		{
			get
			{
				return 10;
			}
			
		}
		
		//private JMenu menu;
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		private System.Windows.Forms.ContextMenu menu;
		private System.Windows.Forms.Control[] menuItems;
		private MenuScrollItem upItem;
		private MenuScrollItem downItem;
		//UPGRADE_NOTE: Final was removed from the declaration of 'menuListener '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'menuListener' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private MenuScrollListener menuListener;
		private int scrollCount;
		private int interval;
		private int topFixedCount;
		private int bottomFixedCount;
		private int firstIndex = 0;
		private int keepVisibleIndex = - 1;
		
		/// <summary> Registers a menu to be scrolled with the default number of items to
		/// display at a time and the default scrolling interval.
		/// 
		/// </summary>
		/// <param name="menu">the menu
		/// </param>
		/// <returns> the MenuScroller
		/// </returns>
		public static MenuScroller setScrollerFor(System.Windows.Forms.MenuItem menu)
		{
			return new MenuScroller(menu);
		}
		
		/// <summary> Registers a popup menu to be scrolled with the default number of items to
		/// display at a time and the default scrolling interval.
		/// 
		/// </summary>
		/// <param name="menu">the popup menu
		/// </param>
		/// <returns> the MenuScroller
		/// </returns>
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public static MenuScroller setScrollerFor(System.Windows.Forms.ContextMenu menu)
		{
			return new MenuScroller(menu);
		}
		
		/// <summary> Registers a menu to be scrolled with the default number of items to
		/// display at a time and the specified scrolling interval.
		/// 
		/// </summary>
		/// <param name="menu">the menu
		/// </param>
		/// <param name="scrollCount">the number of items to display at a time
		/// </param>
		/// <returns> the MenuScroller
		/// </returns>
		/// <throws>  IllegalArgumentException if scrollCount is 0 or negative </throws>
		public static MenuScroller setScrollerFor(System.Windows.Forms.MenuItem menu, int scrollCount)
		{
			return new MenuScroller(menu, scrollCount);
		}
		
		/// <summary> Registers a popup menu to be scrolled with the default number of items to
		/// display at a time and the specified scrolling interval.
		/// 
		/// </summary>
		/// <param name="menu">the popup menu
		/// </param>
		/// <param name="scrollCount">the number of items to display at a time
		/// </param>
		/// <returns> the MenuScroller
		/// </returns>
		/// <throws>  IllegalArgumentException if scrollCount is 0 or negative </throws>
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public static MenuScroller setScrollerFor(System.Windows.Forms.ContextMenu menu, int scrollCount)
		{
			return new MenuScroller(menu, scrollCount);
		}
		
		/// <summary> Registers a menu to be scrolled, with the specified number of items to
		/// display at a time and the specified scrolling interval.
		/// 
		/// </summary>
		/// <param name="menu">the menu
		/// </param>
		/// <param name="scrollCount">the number of items to be displayed at a time
		/// </param>
		/// <param name="interval">the scroll interval, in milliseconds
		/// </param>
		/// <returns> the MenuScroller
		/// </returns>
		/// <throws>  IllegalArgumentException if scrollCount or interval is 0 or negative </throws>
		public static MenuScroller setScrollerFor(System.Windows.Forms.MenuItem menu, int scrollCount, int interval)
		{
			return new MenuScroller(menu, scrollCount, interval);
		}
		
		/// <summary> Registers a popup menu to be scrolled, with the specified number of items to
		/// display at a time and the specified scrolling interval.
		/// 
		/// </summary>
		/// <param name="menu">the popup menu
		/// </param>
		/// <param name="scrollCount">the number of items to be displayed at a time
		/// </param>
		/// <param name="interval">the scroll interval, in milliseconds
		/// </param>
		/// <returns> the MenuScroller
		/// </returns>
		/// <throws>  IllegalArgumentException if scrollCount or interval is 0 or negative </throws>
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public static MenuScroller setScrollerFor(System.Windows.Forms.ContextMenu menu, int scrollCount, int interval)
		{
			return new MenuScroller(menu, scrollCount, interval);
		}
		
		/// <summary> Registers a menu to be scrolled, with the specified number of items
		/// to display in the scrolling region, the specified scrolling interval,
		/// and the specified numbers of items fixed at the top and bottom of the
		/// menu.
		/// 
		/// </summary>
		/// <param name="menu">the menu
		/// </param>
		/// <param name="scrollCount">the number of items to display in the scrolling portion
		/// </param>
		/// <param name="interval">the scroll interval, in milliseconds
		/// </param>
		/// <param name="topFixedCount">the number of items to fix at the top.  May be 0.
		/// </param>
		/// <param name="bottomFixedCount">the number of items to fix at the bottom. May be 0
		/// </param>
		/// <throws>  IllegalArgumentException if scrollCount or interval is 0 or </throws>
		/// <summary> negative or if topFixedCount or bottomFixedCount is negative
		/// </summary>
		/// <returns> the MenuScroller
		/// </returns>
		public static MenuScroller setScrollerFor(System.Windows.Forms.MenuItem menu, int scrollCount, int interval, int topFixedCount, int bottomFixedCount)
		{
			return new MenuScroller(menu, scrollCount, interval, topFixedCount, bottomFixedCount);
		}
		
		/// <summary> Registers a popup menu to be scrolled, with the specified number of items
		/// to display in the scrolling region, the specified scrolling interval,
		/// and the specified numbers of items fixed at the top and bottom of the
		/// popup menu.
		/// 
		/// </summary>
		/// <param name="menu">the popup menu
		/// </param>
		/// <param name="scrollCount">the number of items to display in the scrolling portion
		/// </param>
		/// <param name="interval">the scroll interval, in milliseconds
		/// </param>
		/// <param name="topFixedCount">the number of items to fix at the top.  May be 0
		/// </param>
		/// <param name="bottomFixedCount">the number of items to fix at the bottom.  May be 0
		/// </param>
		/// <throws>  IllegalArgumentException if scrollCount or interval is 0 or </throws>
		/// <summary> negative or if topFixedCount or bottomFixedCount is negative
		/// </summary>
		/// <returns> the MenuScroller
		/// </returns>
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public static MenuScroller setScrollerFor(System.Windows.Forms.ContextMenu menu, int scrollCount, int interval, int topFixedCount, int bottomFixedCount)
		{
			return new MenuScroller(menu, scrollCount, interval, topFixedCount, bottomFixedCount);
		}
		
		/// <summary> Constructs a <code>MenuScroller</code> that scrolls a menu with the
		/// default number of items to display at a time, and default scrolling
		/// interval.
		/// 
		/// </summary>
		/// <param name="menu">the menu
		/// </param>
		public MenuScroller(System.Windows.Forms.MenuItem menu):this(menu, 15)
		{
		}
		
		/// <summary> Constructs a <code>MenuScroller</code> that scrolls a popup menu with the
		/// default number of items to display at a time, and default scrolling
		/// interval.
		/// 
		/// </summary>
		/// <param name="menu">the popup menu
		/// </param>
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public MenuScroller(System.Windows.Forms.ContextMenu menu):this(menu, 15)
		{
		}
		
		/// <summary> Constructs a <code>MenuScroller</code> that scrolls a menu with the
		/// specified number of items to display at a time, and default scrolling
		/// interval.
		/// 
		/// </summary>
		/// <param name="menu">the menu
		/// </param>
		/// <param name="scrollCount">the number of items to display at a time
		/// </param>
		/// <throws>  IllegalArgumentException if scrollCount is 0 or negative </throws>
		public MenuScroller(System.Windows.Forms.MenuItem menu, int scrollCount):this(menu, scrollCount, 150)
		{
		}
		
		/// <summary> Constructs a <code>MenuScroller</code> that scrolls a popup menu with the
		/// specified number of items to display at a time, and default scrolling
		/// interval.
		/// 
		/// </summary>
		/// <param name="menu">the popup menu
		/// </param>
		/// <param name="scrollCount">the number of items to display at a time
		/// </param>
		/// <throws>  IllegalArgumentException if scrollCount is 0 or negative </throws>
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public MenuScroller(System.Windows.Forms.ContextMenu menu, int scrollCount):this(menu, scrollCount, 150)
		{
		}
		
		/// <summary> Constructs a <code>MenuScroller</code> that scrolls a menu with the
		/// specified number of items to display at a time, and specified scrolling
		/// interval.
		/// 
		/// </summary>
		/// <param name="menu">the menu
		/// </param>
		/// <param name="scrollCount">the number of items to display at a time
		/// </param>
		/// <param name="interval">the scroll interval, in milliseconds
		/// </param>
		/// <throws>  IllegalArgumentException if scrollCount or interval is 0 or negative </throws>
		public MenuScroller(System.Windows.Forms.MenuItem menu, int scrollCount, int interval):this(menu, scrollCount, interval, 0, 0)
		{
		}
		
		/// <summary> Constructs a <code>MenuScroller</code> that scrolls a popup menu with the
		/// specified number of items to display at a time, and specified scrolling
		/// interval.
		/// 
		/// </summary>
		/// <param name="menu">the popup menu
		/// </param>
		/// <param name="scrollCount">the number of items to display at a time
		/// </param>
		/// <param name="interval">the scroll interval, in milliseconds
		/// </param>
		/// <throws>  IllegalArgumentException if scrollCount or interval is 0 or negative </throws>
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public MenuScroller(System.Windows.Forms.ContextMenu menu, int scrollCount, int interval):this(menu, scrollCount, interval, 0, 0)
		{
		}
		
		/// <summary> Constructs a <code>MenuScroller</code> that scrolls a menu with the
		/// specified number of items to display in the scrolling region, the
		/// specified scrolling interval, and the specified numbers of items fixed at
		/// the top and bottom of the menu.
		/// 
		/// </summary>
		/// <param name="menu">the menu
		/// </param>
		/// <param name="scrollCount">the number of items to display in the scrolling portion
		/// </param>
		/// <param name="interval">the scroll interval, in milliseconds
		/// </param>
		/// <param name="topFixedCount">the number of items to fix at the top.  May be 0
		/// </param>
		/// <param name="bottomFixedCount">the number of items to fix at the bottom.  May be 0
		/// </param>
		/// <throws>  IllegalArgumentException if scrollCount or interval is 0 or </throws>
		/// <summary> negative or if topFixedCount or bottomFixedCount is negative
		/// </summary>
		public MenuScroller(System.Windows.Forms.MenuItem menu, int scrollCount, int interval, int topFixedCount, int bottomFixedCount):this(tempContextMenu, scrollCount, interval, topFixedCount, bottomFixedCount)
		{
			System.Windows.Forms.ContextMenu tempContextMenu;
			tempContextMenu = new System.Windows.Forms.ContextMenu();
			tempContextMenu.MergeMenu(menu);
		}
		
		/// <summary> Constructs a <code>MenuScroller</code> that scrolls a popup menu with the
		/// specified number of items to display in the scrolling region, the
		/// specified scrolling interval, and the specified numbers of items fixed at
		/// the top and bottom of the popup menu.
		/// 
		/// </summary>
		/// <param name="menu">the popup menu
		/// </param>
		/// <param name="scrollCount">the number of items to display in the scrolling portion
		/// </param>
		/// <param name="interval">the scroll interval, in milliseconds
		/// </param>
		/// <param name="topFixedCount">the number of items to fix at the top.  May be 0
		/// </param>
		/// <param name="bottomFixedCount">the number of items to fix at the bottom.  May be 0
		/// </param>
		/// <throws>  IllegalArgumentException if scrollCount or interval is 0 or </throws>
		/// <summary> negative or if topFixedCount or bottomFixedCount is negative
		/// </summary>
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public MenuScroller(System.Windows.Forms.ContextMenu menu, int scrollCount, int interval, int topFixedCount, int bottomFixedCount)
		{
			InitBlock();
			if (scrollCount <= 0 || interval <= 0)
			{
				throw new System.ArgumentException("scrollCount and interval must be greater than 0");
			}
			if (topFixedCount < 0 || bottomFixedCount < 0)
			{
				throw new System.ArgumentException("topFixedCount and bottomFixedCount cannot be negative");
			}
			
			upItem = new MenuScrollItem(MenuIcon.UP, - 1);
			downItem = new MenuScrollItem(MenuIcon.DOWN, + 1);
			ScrollCount = scrollCount;
			Interval = interval;
			TopFixedCount = topFixedCount;
			BottomFixedCount = bottomFixedCount;
			
			this.menu = menu;
			//UPGRADE_NOTE: Some methods of the 'javax.swing.event.PopupMenuListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			menu.Popup += new System.EventHandler(menuListener.popupMenuWillBecomeVisible);
		}
		
		/// <summary> Returns the number of items in the scrolling portion of the menu.
		/// 
		/// </summary>
		/// <returns> the number of items to display at a time
		/// </returns>
		public virtual int getscrollCount()
		{
			return scrollCount;
		}
		
		/// <summary> Scrolls the specified item into view each time the menu is opened.  Call this method with
		/// <code>null</code> to restore the default behavior, which is to show the menu as it last
		/// appeared.
		/// 
		/// </summary>
		/// <param name="item">the item to keep visible
		/// </param>
		/// <seealso cref="keepVisible(int)">
		/// </seealso>
		public virtual void  keepVisible(System.Windows.Forms.MenuItem item)
		{
			if (item == null)
			{
				keepVisibleIndex = - 1;
			}
			else
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JPopupMenu.getComponentIndex' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJPopupMenugetComponentIndex_javaawtComponent'"
				int index = menu.getComponentIndex(item);
				keepVisibleIndex = index;
			}
		}
		
		/// <summary> Scrolls the item at the specified index into view each time the menu is opened.  Call this
		/// method with <code>-1</code> to restore the default behavior, which is to show the menu as
		/// it last appeared.
		/// 
		/// </summary>
		/// <param name="index">the index of the item to keep visible
		/// </param>
		/// <seealso cref="keepVisible(javax.swing.JMenuItem)">
		/// </seealso>
		public virtual void  keepVisible(int index)
		{
			keepVisibleIndex = index;
		}
		
		/// <summary> Removes this MenuScroller from the associated menu and restores the
		/// default behavior of the menu.
		/// </summary>
		public virtual void  dispose()
		{
			if (menu != null)
			{
				menu.Popup -= new System.EventHandler(menuListener.popupMenuWillBecomeVisible);
				menu = null;
			}
		}
		
		/// <summary> Ensures that the <code>dispose</code> method of this MenuScroller is
		/// called when there are no more refrences to it.
		/// 
		/// </summary>
		/// <exception cref="Throwable">if an error occurs.
		/// </exception>
		/// <seealso cref="MenuScroller.dispose()">
		/// </seealso>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		~MenuScroller()
		{
			dispose();
		}
		
		private void  refreshMenu()
		{
			if (menuItems != null && menuItems.Length > 0)
			{
				firstIndex = System.Math.Max(topFixedCount, firstIndex);
				firstIndex = System.Math.Min(menuItems.Length - bottomFixedCount - scrollCount, firstIndex);
				
				upItem.Enabled = firstIndex > topFixedCount;
				downItem.Enabled = firstIndex + scrollCount < menuItems.Length - bottomFixedCount;
				
				menu.Controls.Clear();
				for (int i = 0; i < topFixedCount; i++)
				{
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					menu.Controls.Add(menuItems[i]);
				}
				if (topFixedCount > 0)
				{
					menu.MenuItems.Add("-");
				}
				
				menu.MenuItems.Add(upItem);
				for (int i = firstIndex; i < scrollCount + firstIndex; i++)
				{
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					menu.Controls.Add(menuItems[i]);
				}
				menu.MenuItems.Add(downItem);
				
				if (bottomFixedCount > 0)
				{
					menu.MenuItems.Add("-");
				}
				for (int i = menuItems.Length - bottomFixedCount; i < menuItems.Length; i++)
				{
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					menu.Controls.Add(menuItems[i]);
				}
				
				//UPGRADE_ISSUE: The equivalent in .NET for method 'java.awt.Component.getParent' returns a different type. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1224'"
				System.Windows.Forms.Control parent = (System.Windows.Forms.Control) upItem.Parent;
				parent.Invalidate();
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				parent.Refresh();
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MenuScrollListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class MenuScrollListener
		{
			public MenuScrollListener(MenuScroller enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(MenuScroller enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MenuScroller enclosingInstance;
			public MenuScroller Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			public virtual void  popupMenuWillBecomeVisible(System.Object event_sender, System.EventArgs e)
			{
				setMenuItems();
			}
			
			public virtual void  popupMenuWillBecomeInvisible(System.Object event_sender, System.EventArgs e)
			{
				restoreMenuItems();
			}
			
			public virtual void  popupMenuCanceled(System.Object event_sender, System.EventArgs e)
			{
				restoreMenuItems();
			}
			
			private void  setMenuItems()
			{
				//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				System.Windows.Forms.Control[] temp_array;
				System.Windows.Forms.ContextMenu temp_container;
				temp_container = Enclosing_Instance.menu;
				temp_array = new System.Windows.Forms.Control[temp_container.Controls.Count];
				temp_container.Controls.CopyTo(temp_array, 0);
				Enclosing_Instance.menuItems = temp_array;
				if (Enclosing_Instance.keepVisibleIndex >= Enclosing_Instance.topFixedCount && Enclosing_Instance.keepVisibleIndex <= Enclosing_Instance.menuItems.Length - Enclosing_Instance.bottomFixedCount && (Enclosing_Instance.keepVisibleIndex > Enclosing_Instance.firstIndex + Enclosing_Instance.scrollCount || Enclosing_Instance.keepVisibleIndex < Enclosing_Instance.firstIndex))
				{
					Enclosing_Instance.firstIndex = System.Math.Min(Enclosing_Instance.firstIndex, Enclosing_Instance.keepVisibleIndex);
					Enclosing_Instance.firstIndex = System.Math.Max(Enclosing_Instance.firstIndex, Enclosing_Instance.keepVisibleIndex - Enclosing_Instance.scrollCount + 1);
				}
				if (Enclosing_Instance.menuItems.Length > Enclosing_Instance.topFixedCount + Enclosing_Instance.scrollCount + Enclosing_Instance.bottomFixedCount)
				{
					Enclosing_Instance.refreshMenu();
				}
			}
			
			private void  restoreMenuItems()
			{
				Enclosing_Instance.menu.Controls.Clear();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Component component: menuItems)
				{
					Enclosing_Instance.menu.add(component);
				}
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MenuScrollTimer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class MenuScrollTimer:System.Timers.Timer
		{
			private void  InitBlock(MenuScroller enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MenuScroller enclosingInstance;
			public MenuScroller Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			public MenuScrollTimer(MenuScroller enclosingInstance, int increment, int interval):base(interval)
			{
				InitBlock(enclosingInstance);
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MenuScrollItem' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class MenuScrollItem:System.Windows.Forms.MenuItem
		{
			private void  InitBlock(MenuScroller enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MenuScroller enclosingInstance;
			virtual public int Interval
			{
				set
				{
					timer.Interval = (System.Double) value;
				}
				
			}
			public MenuScroller Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			private MenuScrollTimer timer;
			
			public MenuScrollItem(MenuScroller enclosingInstance, MenuIcon icon, int increment)
			{
				InitBlock(enclosingInstance);
				setIcon(icon);
				setDisabledIcon(icon);
				timer = new MenuScrollTimer(enclosingInstance, increment, Enclosing_Instance.interval);
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractButton.addChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractButtonaddChangeListener_javaxswingeventChangeListener'"
				addChangeListener(this);
			}
			
			public virtual void  stateChanged(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JMenuItem.isArmed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJMenuItemisArmed'"
				if (isArmed() && !timer.Enabled)
				{
					timer.Start();
				}
				//UPGRADE_ISSUE: Method 'javax.swing.JMenuItem.isArmed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJMenuItemisArmed'"
				if (!isArmed() && timer.Enabled)
				{
					timer.Stop();
				}
			}
		}
		
		private static enum_Renamed MenuIcon;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		implements Icon
		
		public virtual void  paintIcon(System.Windows.Forms.Control c, System.Drawing.Graphics g, int x, int y)
		{
			System.Drawing.Size size = c.Size;
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Graphics.create' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.Drawing.Graphics temp_Graphics;
			temp_Graphics = SupportClass.GraphicsManager.CreateGraphics(g);
			temp_Graphics.SetClip(new System.Drawing.Rectangle(size.Width / 2 - 5, size.Height / 2 - 5, 10, 10));
			System.Drawing.Graphics g2 = temp_Graphics;
			g2.setColor(Color.GRAY);
			g2.DrawPath(SupportClass.GraphicsManager.manager.GetPen(g2), SupportClass.CreateGraphicsPath(xPoints, yPoints, 3));
			if (c.Enabled)
			{
				g2.setColor(Color.BLACK);
				g2.FillPath(SupportClass.GraphicsManager.manager.GetPaint(g2), SupportClass.CreateGraphicsPath(xPoints, yPoints, 3));
			}
			g2.Dispose();
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}