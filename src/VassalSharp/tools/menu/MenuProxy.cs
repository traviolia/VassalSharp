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
	public class MenuProxy : AbstractParent<System.Windows.Forms.MenuStrip>
	{
		private string _text;
		private char mnemonic = (char)(0);

		virtual public string Text
		{
			get
			{
				return _text;
			}

			set
			{
				this._text = value;
				forEachPeer((menu) => {
					menu.Text = _text;
				});
			}

		}

		public MenuProxy()
		{
		}

		public MenuProxy(string text)
		{
			this._text = text;
		}

		public virtual SeparatorProxy addSeparator()
		{
			SeparatorProxy sep = new SeparatorProxy();
			add(sep);
			return sep;
		}

		public virtual SeparatorProxy insertSeparator(int pos)
		{
			SeparatorProxy sep = new SeparatorProxy();
			insert(sep, pos);
			return sep;
		}

		public override System.Windows.Forms.MenuStrip createPeer()
		{
			System.Windows.Forms.MenuStrip menu = new System.Windows.Forms.MenuStrip();
			menu.Text = _text;
			if (mnemonic != 0)
			{
				menu.setMnemonic(mnemonic);
			}

			foreach (ChildProxy<System.ComponentModel.Component> child in children)
			{
				var peer = (System.Windows.Forms.ToolStripMenuItem)child.createPeer();
				if (peer != null) menu.DropDownItems.Add(peer);
			}

			peers.Add(new WeakReference<System.Windows.Forms.MenuStrip>(menu));
			return menu;
		}

		public void setMnemonic(char mnemonic)
		{
			this.mnemonic = mnemonic;
			forEachPeer((menu) => {
				menu.setMnemonic(mnemonic);
			});
		}
	}
}