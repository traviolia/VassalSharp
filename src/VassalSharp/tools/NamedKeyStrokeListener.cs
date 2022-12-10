/*
* $Id$
*
* Copyright (c) 2009 by Brent Easton
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
namespace VassalSharp.tools
{
	/// <summary> Extension of KeyStrokeListener to support NamedKeyStroke's and
	/// store the supplied NamedKeyStroke. Some components use KeyStrokeListeners
	/// to store KeyStroke values.
	/// 
	/// </summary>
	public class NamedKeyStrokeListener:KeyStrokeListener
	{
		virtual public NamedKeyStroke NamedKeyStroke
		{
			get
			{
				return namedKeyStroke;
			}
			
		}
		
		private NamedKeyStroke namedKeyStroke;
		
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public NamedKeyStrokeListener(ActionListener l):base(l)
		{
		}
		
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public NamedKeyStrokeListener(ActionListener l, NamedKeyStroke key):this(l)
		{
			setKeyStroke(key);
		}
		
		public override void  setKeyStroke(KeyStroke newKey)
		{
			base.setKeyStroke(newKey);
			namedKeyStroke = new NamedKeyStroke(newKey.KeyValue, 0);
		}
		
		public virtual void  setKeyStroke(NamedKeyStroke newKey)
		{
			base.setKeyStroke(newKey == null?null:newKey.KeyStroke);
			namedKeyStroke = newKey;
		}
	}
}