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
	public abstract class AbstractParent<T> : AbstractProxy<T>, ParentProxy<T> where T : System.Windows.Forms.Control
	{
		protected List<ChildProxy<System.ComponentModel.Component>> children = new List<ChildProxy<System.ComponentModel.Component>>();

		public AbstractParent()
		{
		}
		
		public void add(ChildProxy<System.ComponentModel.Component> child)
		{
			children.Add(child);
			child.Parent = (ParentProxy<System.Windows.Forms.Control>) this;

			if (child is MenuMarker) return;

			forEachPeer((peer) => peer.Controls.Add((System.Windows.Forms.Control) child.createPeer()));
		}


		protected int proxyIndexToRealIndex(int pos)
		{
			int j = -1;
			for (int i = 0; i <= pos; i++)
			{
				if (!(children[i] is MenuMarker)) j++;
			} 
			return j;
		}

		public void insert(ChildProxy<System.ComponentModel.Component> child, int pos)
		{
			children.Insert(pos, child);
			child.Parent = (ParentProxy<System.Windows.Forms.Control>) this;

			if (child is MenuMarker) return;

			int rpos = proxyIndexToRealIndex(pos);

			var newPeer = (System.Windows.Forms.Control)child.createPeer();
			forEachPeer((peer) => { peer.Controls.Add(newPeer); peer.Controls.SetChildIndex(newPeer, rpos); });
		}


		public void remove(ChildProxy<System.ComponentModel.Component> child)

		{
			if (children.Remove(child))
				child.Parent = null;
		}


		public void remove(int pos)

		{
			ChildProxy<System.ComponentModel.Component> child = children[pos];
			children.RemoveAt(pos);
			child.Parent = null;
		}


		public int getChildCount()
		{
			return children.Count;

		}


		public ChildProxy<System.ComponentModel.Component>[] getChildren()
		{
            return children.ToArray();

		}


		public ChildProxy<System.ComponentModel.Component> getChild(int pos)
		{
			return (ChildProxy<System.ComponentModel.Component>) children[pos];
		}


		public int getIndex(ChildProxy<System.ComponentModel.Component> child)

        {
            return children.IndexOf(child);
        }

    }
}