/*
* $Id$
*
* Copyright (c) 2006 by Joel Uckelman
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
using DirectoryConfigurer = VassalSharp.configure.DirectoryConfigurer;
namespace VassalSharp.tools
{
	
	/// <summary> FileChooser provides a wrapper for {@link javax.swing.JFileChooser} and
	/// {@link java.awt.FileDialog}, selecting whichever is preferred on the
	/// user's OS. <code>FileChooser</code>'s methods mirror those of
	/// <code>JFileChooser</code>.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <deprecated> Moved to {@link VassalSharp.tools.filechooser.FileChooser}.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public abstract class FileChooser:VassalSharp.tools.filechooser.FileChooser
	{
		private FileChooser(System.Windows.Forms.Control parent, DirectoryConfigurer pref):base(parent, pref)
		{
		}
	}
}