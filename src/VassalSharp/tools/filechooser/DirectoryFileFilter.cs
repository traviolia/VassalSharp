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
	
	/// <summary> A FileFilter which accepts directories only.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	public class DirectoryFileFilter:FileFilter
	{
		public override bool accept(System.IO.FileInfo f)
		{
			return System.IO.Directory.Exists(f.FullName);
		}
		
		public override System.String getDescription()
		{
			return "Directories";
		}
	}
}