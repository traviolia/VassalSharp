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

namespace VassalSharp.tools.menu
{
	/// <author>Joel Uckelman</author>
	/// <since>3.1.0</since>
	public class MenuBarProxy : AbstractParent<System.Windows.Forms.MenuStrip>
	{
		public MenuBarProxy()
		{
		}

        public override System.Windows.Forms.MenuStrip createPeer()
		{
			System.Windows.Forms.MenuStrip mb = new System.Windows.Forms.MenuStrip();
			
			foreach (ChildProxy<System.ComponentModel.Component> child in children)
			{
				var peer = (System.Windows.Forms.ToolStripItem) child.createPeer();
				if (peer != null)
				{
					mb.Items.Add(peer);
				}
			}

            peers.Add(new WeakReference<System.Windows.Forms.MenuStrip>(mb));
            return mb;
        }
    }
}