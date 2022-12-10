/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
	
	/// <summary> Utility class for registering a component as a source of hotkey events</summary>
	/// <seealso cref="VassalSharp.build.GameModule.addKeyStrokeSource">
	/// </seealso>
	/// <seealso cref="VassalSharp.build.GameModule.addKeyStrokeListener">
	/// </seealso>
	public class KeyStrokeSource
	{
		virtual public System.Windows.Forms.Control Component
		{
			get
			{
				return c;
			}
			
		}
		virtual public int Mode
		{
			get
			{
				return mode;
			}
			
		}
		private System.Windows.Forms.Control c;
		private int mode;
		
		public KeyStrokeSource(System.Windows.Forms.Control c, int mode)
		{
			this.c = c;
			this.mode = mode;
		}
	}
}