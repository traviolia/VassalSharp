/*
* $Id$
*
* Copyright (c) 2005 by Rodney Kinney, Brent Easton
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
namespace VassalSharp.build.module.gamepieceimage
{
	
	//UPGRADE_TODO: The class 'Font' is marked as Sealed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1147'"
	[Serializable]
	public class OutlineFont:System.Drawing.Font
	{
		virtual public bool Outline
		{
			get
			{
				return outline;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		protected internal bool outline;
		
		//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
		public OutlineFont(System.String name, int style, int size):base(name, size, (System.Drawing.FontStyle) style)
		{
		}
		
		//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
		public OutlineFont(System.String name, int style, int size, bool outline):base(name, size, (System.Drawing.FontStyle) style)
		{
			this.outline = outline;
		}
	}
}