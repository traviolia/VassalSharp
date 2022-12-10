/*
* $Id$
*
* Copyright (c) 2000-2009 by Rodney Kinney, Brent Easton
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
using AudioFileFilter = VassalSharp.tools.filechooser.AudioFileFilter;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
namespace VassalSharp.configure
{
	
	/// <summary> Class for selecting an AudioClip while editing a module and adding it to
	/// module
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class AudioClipConfigurer:FileConfigurer
	{
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Control c = base.Controls;
				if (button == null)
				{
					button = new FormattedExpressionConfigurer.ExpressionButton(this, tf.Size.Height);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					((System.Windows.Forms.Panel) c).Controls.Add(button);
				}
				return c;
			}
			
		}
		protected internal static DirectoryConfigurer resourceDirPref;
		protected internal FormattedExpressionConfigurer.ExpressionButton button;
		
		public AudioClipConfigurer(System.String key, System.String name, ArchiveWriter archive):base(key, name)
		{
			this.archive = archive;
		}
		
		protected internal override FileChooser initFileChooser()
		{
			if (resourceDirPref == null)
			{
				resourceDirPref = new DirectoryConfigurer("audioDir", null);
				GameModule.getGameModule().getPrefs().addOption(null, resourceDirPref);
			}
			FileChooser fc = FileChooser.createFileChooser(GameModule.getGameModule().getFrame(), resourceDirPref);
			fc.FileFilter = new AudioFileFilter();
			return fc;
		}
		
		protected internal virtual void  addToArchive(System.IO.FileInfo f)
		{
			archive.addSound(f.FullName, f.Name);
		}
	}
}