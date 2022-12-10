/*
* $Id$
*
* Copyright (c) 2006-2008 by Joel Uckelman
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
	
	/// <summary> The file filter abstract base class for {@link VassalSharp.tools.FileChooser}.
	/// This class joins together the {@link javax.swing.filechooser.FileFilter}
	/// class and the {@link java.awt.FilenameFilter} interface so that a
	/// uniform file chooser may be used  with {@link VassalSharp.tools.FileChooser},
	/// regardless of the actual (i.e., navive or Swing) file chooser displayed.
	/// 
	/// </summary>
	/// <author>  uckelman
	/// </author>
	//UPGRADE_ISSUE: Class 'javax.swing.filechooser.FileFilter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingfilechooserFileFilter'"
	//UPGRADE_ISSUE: Interface 'java.io.FilenameFilter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioFilenameFilter'"
	public abstract class FileFilter:javax.swing.filechooser.FileFilter, java.io.FilenameFilter
	{
		/// <summary> The accept test used by Swing file choosers.
		/// 
		/// </summary>
		/// <returns> Whether the given file is accepted by this filter.
		/// </returns>
		abstract public bool accept(System.IO.FileInfo f);
		
		/// <summary> The accept test used by AWT file choosers.
		/// 
		/// </summary>
		/// <returns> Whether the given file is accepted by this filter.
		/// </returns>
		public virtual bool accept(System.IO.FileInfo dir, System.String name)
		{
			return accept(new System.IO.FileInfo(dir.FullName + "\\" + name));
		}
		
		/// <returns> A description of this filter to be displayed in the file chooser.
		/// </returns>
		abstract public System.String getDescription();
	}
}