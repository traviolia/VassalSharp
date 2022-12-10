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
	public interface ParentProxy<in T> where T : System.Windows.Forms.Control
    {
		void add(ChildProxy<System.ComponentModel.Component> child);

		void insert(ChildProxy<System.ComponentModel.Component> child, int pos);

		void remove(ChildProxy<System.ComponentModel.Component> child);

		void remove(int pos);

		int getChildCount();

		ChildProxy<System.ComponentModel.Component>[] getChildren();

		ChildProxy<System.ComponentModel.Component> getChild(int pos);

		int getIndex(ChildProxy<System.ComponentModel.Component> child);
	}
}