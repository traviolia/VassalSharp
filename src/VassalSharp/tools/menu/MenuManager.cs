/*
 * Copyright (c) 2008 by Joel Uckelman
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
using System.Collections.Generic;

namespace VassalSharp.tools.menu
{
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public abstract class MenuManager
	{

		protected internal static MenuManager instance;

		public static MenuManager Instance
		{
			get
			{
				if (instance == null)
					throw new System.SystemException();
				return instance;
			}
			
		}
		
		public MenuManager()
		{
			if (instance != null)
				throw new System.SystemException();
			instance = this;
		}
		
		public abstract System.Windows.Forms.MenuStrip getMenuBarFor(System.Windows.Forms.Form fc);
		
		public abstract MenuBarProxy getMenuBarProxyFor(System.Windows.Forms.Form fc);
		
		private Dictionary < String, List < MenuItemProxy >> actionLocations = new Dictionary < String, List < MenuItemProxy >>();
		private Dictionary< String, MenuMarker > markers = new Dictionary< String, MenuMarker >();

		public virtual MenuItemProxy addKey(System.String key)
		{
			actionLocations.TryGetValue(key, out List<MenuItemProxy> items);
			if (items == null)
			{
				items = new List<MenuItemProxy>();
				actionLocations.Add(key, items);
			}

			MenuItemProxy item = new MenuItemProxy();
			items.Add(item);
			return item;
		}

		public List < MenuItemProxy > getItems(String key)
		{
			actionLocations.TryGetValue(key, out List<MenuItemProxy> value);
			return value;
		}

		public virtual void  addAction(System.String key, SupportClass.ActionSupport a)
		{
			actionLocations.TryGetValue(key, out List<MenuItemProxy> items);
			if (items != null)
			{
				foreach (MenuItemProxy i in items)
				{
					i.Action = a;
				}
			}
		}
		
		public virtual void  removeAction(System.String key)
		{
			addAction(key, null);
		}
		
		public virtual MenuMarker getMarker(System.String key)
		{
			markers.TryGetValue(key, out MenuMarker value);
			return value;
		}
		
		public virtual MenuMarker addMarker(System.String key)
		{
			MenuMarker marker = new MenuMarker();
			markers.Add(key, marker);
			return marker;
		}
		
		public void addToSection(String key, ChildProxy<System.ComponentModel.Component> item)
		{
			MenuMarker start = getMarker(key + ".start");
			MenuMarker end = getMarker(key + ".end");
			ParentProxy<System.Windows.Forms.Control> parent = end.Parent;

			int startPos = parent.getIndex(start);
			int endPos = parent.getIndex(end);

			// do separator check if this is the first item in the group
			if (parent is MenuProxy && startPos + 1 == endPos)
			{
				// check from the end of the group to the next visible item
				// and insert a separator if that item is not one
				if (nextVisibleItemNotASeparator(end))
				{
					((MenuProxy)parent).insertSeparator(endPos + 1);
				}

				// check from the start of the group to the previous visible
				// item and insert a separator if that item is not one
				if (prevVisibleItemNotASeparator(start))
				{
					((MenuProxy)parent).insertSeparator(startPos);
				}
			}

			// insert the item between the markers
			parent.insert(item, endPos);
		}

		public void removeFromSection(String key, ChildProxy<System.ComponentModel.Component> item)
		{
			MenuMarker start = getMarker(key + ".start");
			MenuMarker end = getMarker(key + ".end");
			ParentProxy<System.Windows.Forms.Control> parent = end.Parent;

			// remove the item
			parent.remove(item);

			// do separator check if this was the last item in the group
			if (parent is MenuProxy)
			{
				int startPos = parent.getIndex(start);
				int endPos = parent.getIndex(end);
				if (startPos + 1 == endPos)
				{
					if (visibleItemBefore(start))
					{
						// if we have a group on each side, or before but not after;
						// remove our top separator
						parent.remove(startPos - 1);
					}
					else if (visibleItemAfter(end))
					{
						// we have a group after, but none before;
						// remove our bottom separator
						parent.remove(endPos + 1);
					}
					// otherwise, we were the sole group, our parent has no visible
					// items now
				}
			}
		}

		private bool visibleItemAfter(ChildProxy<System.ComponentModel.Component> child)
		{
			ParentProxy<System.Windows.Forms.Control> parent = child.Parent;
			int count = parent.getChildCount();
			for (int i = parent.getIndex(child) + 1; i < count; i++)
			{
				ChildProxy<System.ComponentModel.Component> c = parent.getChild(i);
				if (!(c is MenuMarker))
					return true;
			}
			return false;
		}

		private bool visibleItemBefore(ChildProxy<System.ComponentModel.Component> child)
		{
			ParentProxy<System.Windows.Forms.Control> parent = child.Parent;
			for (int i = parent.getIndex(child) - 1; i >= 0; i++)
			{
				ChildProxy<System.ComponentModel.Component> c = parent.getChild(i);
				if (!(c is MenuMarker))
					return true;
			}
			return false;
		}

		private bool nextVisibleItemNotASeparator(ChildProxy<System.ComponentModel.Component> child)
		{
			ParentProxy<System.Windows.Forms.Control> parent = child.Parent;
			int count = parent.getChildCount();
			for (int i = parent.getIndex(child) + 1; i < count; i++)
			{
				ChildProxy<System.ComponentModel.Component> c = parent.getChild(i);
				if (c is MenuMarker)
					continue;
				return !(c is SeparatorProxy);
			}
			return false;
		}

		private bool prevVisibleItemNotASeparator(ChildProxy<System.ComponentModel.Component> child)
		{
			ParentProxy<System.Windows.Forms.Control> parent = child.Parent;
			for (int i = parent.getIndex(child) - 1; i >= 0; i--)
			{
				ChildProxy<System.ComponentModel.Component> c = parent.getChild(i);
				if (c is MenuMarker)
					continue;
				return !(c is SeparatorProxy);
			}
			return false;
		}
	}
}