/*
* $Id$
*
* Copyright (c) 2012 by Joel Uckelman
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
namespace VassalSharp.tools.filechooser
{
	
	/// <summary> A FileFilter for PNG images. Used by file choosers
	/// to filter out files which aren't PNGs.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public class PNGFileFilter:ExtensionFileFilter
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'types'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly System.String[] types = new System.String[]{".png"};
		
		public PNGFileFilter():base("PNG images", types)
		{
		}
	}
}