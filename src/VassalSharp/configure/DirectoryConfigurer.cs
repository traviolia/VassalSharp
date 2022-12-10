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
using GameModule = VassalSharp.build.GameModule;
using Prefs = VassalSharp.preferences.Prefs;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
namespace VassalSharp.configure
{
	
	/// <summary> A Configurer for picking file directories</summary>
	public class DirectoryConfigurer:FileConfigurer
	{
		
		public DirectoryConfigurer(System.String key, System.String name):base(key, name)
		{
		}
		
		protected internal override FileChooser initFileChooser()
		{
			FileChooser fc = FileChooser.createFileChooser(null, startingDirectory, FileChooser.DIRECTORIES_ONLY);
			if (startingDirectory == null && GameModule.getGameModule() != null)
			{
				fc.CurrentDirectory = (System.IO.FileInfo) Prefs.GlobalPrefs.getValue(Prefs.MODULES_DIR_KEY);
			}
			return fc;
		}
		
		public override void  setValue(System.Object o)
		{
			System.IO.FileInfo f = (System.IO.FileInfo) o;
			if (f != null && !System.IO.Directory.Exists(f.FullName))
			{
				f = null;
			}
			base.setValue(f);
		}
		
		protected internal override void  addToArchive(System.IO.FileInfo f)
		{
		}
		
		public override void  chooseNewValue()
		{
			System.Windows.Forms.Control generatedAux = Controls;
			if (getValue() != null)
			{
				fc.CurrentDirectory = (System.IO.FileInfo) getValue();
			}
			if (fc.showOpenDialog(Controls) == FileChooser.APPROVE_OPTION)
			{
				setValue(fc.SelectedFile);
			}
		}
	}
}