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
	
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class MenuItemProxy : AbstractProxy<System.Windows.Forms.ToolStripMenuItem>
	{

		public override ParentProxy<System.Windows.Forms.Control> Parent
		{
			get
			{
				return _parent;
			}

			set
			{
				_parent = value;
				if (value == null)
					forEachPeer((peer) => {((System.Windows.Forms.ToolStripMenuItem) peer).Owner?.Items.Remove(peer); });
			}
		}
		virtual public SupportClass.ActionSupport Action
		{
			get
			{
				return action;
			}
			
			set
			{
				this.action = value;
                forEachPeer((item) => { SetAction(item); });
 			}
		}

        private void SetAction(System.Windows.Forms.ToolStripMenuItem  item)
        {
            if (action == null) {
                item.Visible = false;
            }
            else {
                item.Click += new System.EventHandler(action.actionPerformed);
                item.Text = action.Description;
                item.Visible = true;
            }
        }

        protected internal SupportClass.ActionSupport action;
		
		public MenuItemProxy() : this(null)
		{
		}
		
		public MenuItemProxy(SupportClass.ActionSupport action)
		{
			this.action = action;
		}

	    public override System.Windows.Forms.ToolStripMenuItem createPeer()
        {
            System.Windows.Forms.ToolStripMenuItem item = new System.Windows.Forms.ToolStripMenuItem();
            SetAction(item);

            peers.Add(new WeakReference<System.Windows.Forms.ToolStripMenuItem>(item));
            return item;
        }
    }
}