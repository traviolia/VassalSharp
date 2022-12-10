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
using ArchiveWriter = VassalSharp.tools.ArchiveWriter;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
using ImageFileFilter = VassalSharp.tools.filechooser.ImageFileFilter;
namespace VassalSharp.configure
{
	
	/// <summary> A Configurer for {@link java.awt.Image} values. Allows the user to select an
	/// image file and writes it to a {@link ArchiveWriter}.
	/// </summary>
	public class ImageConfigurer:FileConfigurer
	{
		protected internal static DirectoryConfigurer resourceDirPref;
		
		public ImageConfigurer(System.String key, System.String name, ArchiveWriter archive):base(key, name)
		{
			this.archive = archive;
			editable = false;
		}
		
		protected internal override FileChooser initFileChooser()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'gm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GameModule gm = GameModule.getGameModule();
			
			if (resourceDirPref == null)
			{
				resourceDirPref = new DirectoryConfigurer("imageDir", null);
				gm.getPrefs().addOption(null, resourceDirPref);
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FileChooser fc = FileChooser.createFileChooser(gm.Frame, resourceDirPref);
			fc.FileFilter = new ImageFileFilter();
			return fc;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override void  addToArchive(System.IO.FileInfo f)
		{
			archive.addImage(f.FullName, f.Name);
		}
	}
}