/*
 * Copyright (c) 2000-2008 by Rodney Kinney, Joel Uckelman
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

#if NEVER_DEFINED
using System;
using GameModule = VassalSharp.build.GameModule;
using DirectoryConfigurer = VassalSharp.configure.DirectoryConfigurer;
using Launcher = VassalSharp.launch.Launcher;
using Prefs = VassalSharp.preferences.Prefs;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
//using SVGImageUtils = VassalSharp.tools.image.svg.SVGImageUtils;
//using Op = VassalSharp.tools.imageop.Op;
using FileArchive = VassalSharp.tools.io.FileArchive;
using IOUtils = VassalSharp.tools.io.IOUtils;
// MLT replace with .Net class => using ZipArchive = VassalSharp.tools.io.ZipArchive;

#else

using System;
using GameModule = VassalSharp.build.GameModule;
//using DirectoryConfigurer = VassalSharp.configure.DirectoryConfigurer;
//using Launcher = VassalSharp.launch.Launcher;
using Prefs = VassalSharp.preferences.Prefs;
//using FileChooser = VassalSharp.tools.filechooser.FileChooser;
//using SVGImageUtils = VassalSharp.tools.image.svg.SVGImageUtils;
//using Op = VassalSharp.tools.imageop.Op;
using FileArchive = VassalSharp.tools.io.FileArchive;
using ZipFile = VassalSharp.tools.io.ZipFile;
using IOUtils = VassalSharp.tools.io.IOUtils;
#endif

namespace VassalSharp.tools
{

#if NEVER_DEFINED
	/// <summary> An ArchiveWriter is a writeable DataArchive. New files may be added
	/// with the {@link #addFile} and {@link #addImage} methods.
	/// </summary>
	public class ArchiveWriter : DataArchive
	{
		private string archiveName;
		private bool isTempArchive = false;
		
		/// <summary> Create a new writeable archive.
		/// 
		/// </summary>
		/// <param name="zipName">the name of the archive. If null, the user will be
		/// prompted for a filename when saving. If not null, new entries will
		/// be added to the named archive. If the file exists and is not a zip
		/// archive, it will be overwritten.
		/// </param>
		public ArchiveWriter(string zipName)
		{
			archiveName = zipName;
			
			if (archiveName == null)
			{
				isTempArchive = true;
				try
				{
					//archiveName = File.createTempFile("tmp", ".zip").FullName;
				}
				catch (System.IO.IOException e)
				{
					WriteErrorDialog.error(e, archiveName);
				}
			}
			
			////UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//System.IO.FileInfo f = new System.IO.FileInfo(archiveName);
			//try
			//{
			//	bool tmpBool;
			//	if (System.IO.File.Exists(f.FullName))
			//		tmpBool = true;
			//	else
			//		tmpBool = System.IO.Directory.Exists(f.FullName);
			//	if (tmpBool)
			//	{
			//		try
			//		{
			//			archive = new ZipArchive(archiveName);
			//		}
			//		catch (System.IO.IOException e1)
			//		{
			//			// the file is not a valid ZIP archive, truncate it
			//			archive = new ZipArchive(archiveName, true);
			//		}
			//	}
			//	else
			//	{
			//		archive = new ZipArchive(archiveName);
			//	}
			//}
			//catch (System.IO.IOException e)
			//{
			//	archive = null;
			//	WriteErrorDialog.error(e, archiveName);
			//}
		}
		
		public ArchiveWriter(FileArchive archive)
		{
			archiveName = archive.Name;
			this.archive = archive;
		}

		//public ArchiveWriter(ZipFile archive)
		//{
		//	//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.getName' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
		//	archiveName = archive.getName();
		//	try
		//	{
		//		this.archive = new ZipArchive(archiveName);
		//	}
		//	catch (System.IO.IOException e)
		//	{
		//		archive = null;
		//		WriteErrorDialog.error(e, archiveName);
		//	}
		//}
		
		/// <summary> Add an image file to the archive. The file will be copied into an
		/// "images" directory in the archive. Storing another image with the
		/// same name will overwrite the previous image.
		/// 
		/// </summary>
		/// <param name="path">the full path of the image file on the user's filesystem
		/// </param>
		/// <param name="name">the name under which to store the image in the archive
		/// </param>
		public virtual void  addImage(string path, string name)
		{
			//// check SVG for external references and pull them in
			//if (name.ToLower().EndsWith(".svg"))
			//{
			//	//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			//	try
			//	{
			//		exrefs = SVGImageUtils.getExternalReferences(path);
			//	}
			//	catch (System.IO.IOException e)
			//	{
			//		ReadErrorDialog.error(e, name);
			//		return ;
			//	}
				
			//	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//	for(String s: exrefs)
			//	{
			//		//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//		System.IO.FileInfo f = new File(s);
					
			//		sbyte[] buf = null;
			//		try
			//		{
			//			buf = SVGImageUtils.relativizeExternalReferences(s);
			//		}
			//		catch (System.IO.IOException e)
			//		{
			//			ReadErrorDialog.error(e, f);
			//			continue;
			//		}
					
			//		addFile(imageDir + f.Name, buf);
			//	}
			//}
			//// otherwise just add what we were given
			//else
			//{
			//	addFile(path, imageDir + name);
			//}
			
			//Op.load(name).update();
			//localImages = null;
		}
		
		public virtual void  addImage(string name, sbyte[] contents)
		{
			addFile(imageDir + name, contents);
			//localImages = null;
		}
		
		public virtual void  addSound(string path, string fileName)
		{
			addFile(path, soundDir + fileName);
		}

		public virtual bool isImageAdded(string name)
		{
			try
			{
				return archive.contains(imageDir + name);
			}
			catch (System.IO.IOException e)
			{
				return false;
			}
		}
		
		public virtual void  removeImage(string name)
		{
			try
			{
				archive.remove(imageDir + name);
			}
			catch (System.IO.IOException e)
			{
				WriteErrorDialog.error(e, archive.Name);
			}
			
			//localImages = null;
		}
		
		/// <summary> Copy a file from the user's filesystem to the archive.
		/// 
		/// </summary>
		/// <param name="path">the full path of the file on the user's filesystem
		/// </param>
		/// <param name="fileName">the name under which to store the file in the archive
		/// </param>
		public virtual void  addFile(string path, string fileName)
		{
			try
			{
				archive.add(fileName, path);
			}
			catch (System.IO.IOException e)
			{
				WriteErrorDialog.error(e, archive.Name);
			}
		}
		
		/// <summary> Copy an <code>InputStream</code> into the archive
		/// 
		/// </summary>
		/// <param name="fileName">the name under which to store the contents of the stream
		/// </param>
		/// <param name="in">the stream to copy
		/// </param>
		public virtual void  addFile(string fileName, System.IO.Stream in_Renamed)
		{
			System.IO.Stream out_Renamed = null;
			try
			{
				//out_Renamed = archive.getOutputStream(fileName);
				//IOUtils.copy(in_Renamed, out_Renamed);
				//out_Renamed.Close();
			}
			catch (System.IO.IOException e)
			{
				WriteErrorDialog.error(e, archive.Name);
			}
			finally
			{
				IOUtils.closeQuietly(out_Renamed);
			}
		}
		
		public virtual void  addFile(string fileName, sbyte[] content)
		{
			try
			{
				archive.add(fileName, content);
			}
			catch (System.IO.IOException e)
			{
				WriteErrorDialog.error(e, archive.Name);
			}
		}
		
		public virtual void  save()
		{
			save(false);
		}
		
		public virtual void  save(bool notifyModuleManager)
		{
			if (isTempArchive)
				saveAs(notifyModuleManager);
			else
				write(archive, notifyModuleManager);
		}
		
		public virtual void  saveAs()
		{
			saveAs(false);
		}
		
		protected internal virtual void  write(FileArchive fa, bool notifyModuleManager)
		{
			fa.flush();
			
			// FIXME: use a listener here?
			if (notifyModuleManager)
			{
				Launcher.Instance.sendSaveCmd(fa.getFile());
			}
		}
		
		public virtual void  saveAs(bool notifyModuleManager)
		{
			////UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//FileChooser fc = FileChooser.createFileChooser(GameModule.getGameModule().getFrame(), (DirectoryConfigurer) Prefs.GlobalPrefs.getOption(Prefs.MODULES_DIR_KEY));
			//if (fc.showSaveDialog() != FileChooser.APPROVE_OPTION)
			//	return ;
			////UPGRADE_NOTE: Final was removed from the declaration of 'filename '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//string filename = fc.SelectedFile.FullName;
			
			//if ((System.Object) filename != (System.Object) archive.Name)
			//{
			//	// Copy the current state to the new archive.
			//	//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//	FileArchive tmp = archive;
				
			//	archive = new ZipArchive(tmp, filename);
			//	archiveName = filename;
			//	archive.flush();
				
			//	tmp.revert();
			//	tmp.close();
				
			//	write(archive, notifyModuleManager);
				
			//	if (isTempArchive)
			//	{
			//		bool tmpBool;
			//		if (System.IO.File.Exists(tmp.getFile().FullName))
			//		{
			//			System.IO.File.Delete(tmp.getFile().FullName);
			//			tmpBool = true;
			//		}
			//		else if (System.IO.Directory.Exists(tmp.getFile().FullName))
			//		{
			//			System.IO.Directory.Delete(tmp.getFile().FullName);
			//			tmpBool = true;
			//		}
			//		else
			//			tmpBool = false;
			//		bool generatedAux = tmpBool;
			//		isTempArchive = false;
			//	}
			//}
			//else
			//{
			//	write(archive, notifyModuleManager);
			//}
		}
		
		/// <summary> If the ArchiveWriter was initialized with non-null file name, then
		/// write the contents of the archive to the named archive. If it was
		/// initialized with a null name, prompt the user to select a new file
		/// into which to write archive.
		/// </summary>
		public virtual void  write()
		{
			write(false);
		}

		public virtual void  write(bool notifyModuleManager)
		{
			save(notifyModuleManager);
		}
	}

#else

	/// <summary> An ArchiveWriter is a writeable DataArchive. New files may be added
	/// with the {@link #addFile} and {@link #addImage} methods.
	/// </summary>
	public class ArchiveWriter : DataArchive
	{

		/// <summary> Create a new writeable archive.
		/// 
		/// </summary>
		/// <param name="zipName">the name of the archive. If null, the user will be
		/// prompted for a filename when saving. If not null, new entries will
		/// be added to the named archive. If the file exists and is not a zip
		/// archive, it will be overwritten.
		/// </param>
		public ArchiveWriter(string zipName) : base(zipName)
		{
		}
		
		public ArchiveWriter(FileArchive archive) : base("dummy")

		{
		}

        public ArchiveWriter(ZipFile archive) : base("dummy")
		{
        }

        /// <summary> Add an image file to the archive. The file will be copied into an
        /// "images" directory in the archive. Storing another image with the
        /// same name will overwrite the previous image.
        /// 
        /// </summary>
        /// <param name="path">the full path of the image file on the user's filesystem
        /// </param>
        /// <param name="name">the name under which to store the image in the archive
        /// </param>
        public virtual void  addImage(string path, string name)
		{
		}
		
		public virtual void  addImage(string name, sbyte[] contents)
		{
		}
		
		public virtual void  addSound(string path, string fileName)
		{
		}

		public virtual bool isImageAdded(string name)
		{
			return false;
		}
		
		public virtual void  removeImage(string name)
		{
		}
		
		/// <summary> Copy a file from the user's filesystem to the archive.
		/// 
		/// </summary>
		/// <param name="path">the full path of the file on the user's filesystem
		/// </param>
		/// <param name="fileName">the name under which to store the file in the archive
		/// </param>
		public virtual void  addFile(string path, string fileName)
		{
		}
		
		/// <summary> Copy an <code>InputStream</code> into the archive
		/// 
		/// </summary>
		/// <param name="fileName">the name under which to store the contents of the stream
		/// </param>
		/// <param name="in">the stream to copy
		/// </param>
		public virtual void  addFile(string fileName, System.IO.Stream in_Renamed)
		{
		}
		
		public virtual void  addFile(string fileName, sbyte[] content)
		{
		}
		
		public virtual void  save()
		{
		}
		
		public virtual void  save(bool notifyModuleManager)
		{
		}
		
		public virtual void  saveAs()
		{
		}
		
		public virtual void  saveAs(bool notifyModuleManager)
		{
		}
		
		/// <summary> If the ArchiveWriter was initialized with non-null file name, then
		/// write the contents of the archive to the named archive. If it was
		/// initialized with a null name, prompt the user to select a new file
		/// into which to write archive.
		/// </summary>
		public virtual void  write()
		{
		}

		public virtual void  write(bool notifyModuleManager)
		{
		}
	}
#endif
}
