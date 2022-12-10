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
	public class MenuMarker : ChildProxy<System.Windows.Forms.Control>
	{
        protected internal ParentProxy<System.Windows.Forms.Control> parent;

        virtual public ParentProxy<System.Windows.Forms.Control> Parent
		{
			get
			{
				return parent;
			}
			
			set
			{
				this.parent = value;
			}
			
		}
		
		public virtual System.Windows.Forms.Control createPeer()
		{
			return null;
		}
    }
}