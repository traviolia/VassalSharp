/*
* Copyright (c) 2000-2007 by Rodney Kinney
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
using Info = VassalSharp.Info;
using GameModule = VassalSharp.build.GameModule;
using AbstractMetaData = VassalSharp.build.module.metadata.AbstractMetaData;
using ExtensionMetaData = VassalSharp.build.module.metadata.ExtensionMetaData;
using MetaDataFactory = VassalSharp.build.module.metadata.MetaDataFactory;
using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
using WriteErrorDialog = VassalSharp.tools.WriteErrorDialog;
using System.Collections.Generic;
using System.IO;

namespace VassalSharp.build.module
{
	
	/// <summary> Convenience class for managing extensions relative to a module file.
	/// Create extension directory as lazily as possible.
	/// 
	/// </summary>
	/// <author>  rodneykinney
	/// 
	/// </author>
	
	public class ExtensionsManager
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassFilenameFilter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_ISSUE: Interface 'java.io.FilenameFilter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioFilenameFilter'"
		internal class AnonymousClassFilenameFilter // : FilenameFilter
		{
			public AnonymousClassFilenameFilter(ExtensionsManager enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ExtensionsManager enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ExtensionsManager enclosingInstance;
			public ExtensionsManager Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual bool accept(System.IO.FileInfo dir, System.String name)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fileCandidate '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.IO.FileInfo fileCandidate = new System.IO.FileInfo(dir.FullName + "\\" + name);
				return !SupportClass.FileSupport.IsHidden(fileCandidate) && !System.IO.Directory.Exists(fileCandidate.FullName);
			}
		}
		//private void  InitBlock()
		//{
		//	filter = new AnonymousClassFilenameFilter(this);
		//	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//	final List < File > extensions = new ArrayList < File >(0);
		//	if (dir != null && dir.exists())
		//	{
		//		System.IO.FileInfo[] files = dir.listFiles(filter);
		//		if (files == null)
		//		{
		//			ReadErrorDialog.error(new System.IO.IOException(), dir);
		//		}
		//		else
		//		{
		//			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//			for(File file: files)
		//			{
		//				//UPGRADE_NOTE: Final was removed from the declaration of 'metadata '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//				AbstractMetaData metadata = MetaDataFactory.buildMetaData(file);
		//				if (metadata != null && metadata is ExtensionMetaData)
		//				{
		//					extensions.add(file);
		//				}
		//			}
		//		}
		//	}
		//	return extensions;
		//	return getExtensions(getExtensionsDirectory(false));
		//	return getExtensions(getInactiveExtensionsDirectory(false));
		//}
		private System.IO.FileInfo moduleFile;
		private System.IO.FileInfo extensionsDir;
		private System.IO.FileInfo inactiveDir;
		
		/// <summary> Tests if the specified file should be accepted as an module extension
		/// file. Currently we disallow any files that are hidden or "files" that
		/// are directories.
		/// </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'filter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_ISSUE: Interface 'java.io.FilenameFilter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioFilenameFilter'"
		//UPGRADE_NOTE: The initialization of  'filter' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		//private FilenameFilter filter;
		
		public ExtensionsManager(System.IO.FileInfo moduleFile)
		{
			this.moduleFile = moduleFile;
		}
		
		public ExtensionsManager(GameModule module)
		{
			//this.moduleFile = new File(GameModule.getGameModule().getDataArchive().getName());
		}
		
		/// <summary> Manage global extensions</summary>
		public ExtensionsManager(System.String dir)
		{
			extensionsDir = ensureExists(new System.IO.FileInfo(Info.HomeDir.FullName + "\\" + dir));
		}
		
		public virtual System.IO.FileInfo getExtensionsDirectory(bool mustExist)
		{
			if (extensionsDir == null && moduleFile != null)
			{
				System.IO.FileInfo dir;
				System.String dirName = moduleFile.FullName;
				int index = dirName.LastIndexOf('.');
				if (index > 0)
				{
					dirName = dirName.Substring(0, (index) - (0));
				}
				dir = new System.IO.FileInfo(dirName + "_ext");
				if (mustExist)
				{
					dir = ensureExists(dir);
				}
				extensionsDir = dir;
				if (extensionsDir == null)
				{
					return null;
				}
			}
			bool tmpBool;
			if (System.IO.File.Exists(extensionsDir.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(extensionsDir.FullName);
			if (mustExist && !tmpBool)
			{
				extensionsDir = ensureExists(extensionsDir);
			}
			return extensionsDir;
		}
		
		public virtual void  setExtensionsDirectory(System.IO.FileInfo dir)
		{
			extensionsDir = dir == null?null:ensureExists(dir);
		}
		
		
		/// <summary> Ensure a directory exists.</summary>
		/// <param name="dir">Directory
		/// </param>
		/// <returns> Directory as <code>File</code> object; otherwise <code>null</code> if an error occurs.
		/// </returns>
		protected internal virtual System.IO.FileInfo ensureExists(System.IO.FileInfo dir)
		{
			bool tmpBool;
			if (System.IO.File.Exists(dir.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(dir.FullName);
			if (tmpBool && !System.IO.Directory.Exists(dir.FullName))
			{
				WriteErrorDialog.error(new System.IO.IOException(dir + "is not a directory"), dir);
				return null;
			}
			else
			{
				bool tmpBool2;
				if (System.IO.File.Exists(dir.FullName))
					tmpBool2 = true;
				else
					tmpBool2 = System.IO.Directory.Exists(dir.FullName);
				//UPGRADE_TODO: Method 'java.io.File.mkdirs' was converted to 'System.IO.Directory.CreateDirectory' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFilemkdirs'"
				if (!tmpBool2 && System.IO.Directory.CreateDirectory(dir.FullName) == null)
				{
					WriteErrorDialog.error(new System.IO.IOException("Could not create " + dir), dir);
					return null;
				}
			}
			return dir;
		}
		
		public virtual System.IO.FileInfo getInactiveExtensionsDirectory(bool mustExist)
		{
			if (inactiveDir == null)
			{
				System.IO.FileInfo extDir = getExtensionsDirectory(mustExist);
				if (extDir == null)
				{
					return null;
				}
				inactiveDir = new System.IO.FileInfo(extDir.FullName + "\\" + "inactive");
				if (mustExist)
				{
					inactiveDir = ensureExists(inactiveDir);
					if (inactiveDir == null)
					{
						return null;
					}
				}
			}
			bool tmpBool;
			if (System.IO.File.Exists(inactiveDir.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(inactiveDir.FullName);
			if (mustExist && !tmpBool)
			{
				inactiveDir = ensureExists(inactiveDir);
			}
			return inactiveDir;
		}
		
		public virtual System.IO.FileInfo setActive(System.IO.FileInfo extension, bool active)
		{
			System.IO.FileInfo newExt;
			if (active)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'extensionsDirectory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.IO.FileInfo extensionsDirectory = getExtensionsDirectory(true);
				if (extensionsDirectory == null)
				{
					return extension;
				}
				newExt = new System.IO.FileInfo(extensionsDirectory.FullName + "\\" + extension.Name);
			}
			else
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'inactiveExtensionsDirectory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.IO.FileInfo inactiveExtensionsDirectory = getInactiveExtensionsDirectory(true);
				if (inactiveExtensionsDirectory == null)
				{
					return extension;
				}
				newExt = new System.IO.FileInfo(inactiveExtensionsDirectory.FullName + "\\" + extension.Name);
			}
			//UPGRADE_TODO: Method 'java.io.File.renameTo' was converted to ' System.IO.FileInfo.MoveTo' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFilerenameTo_javaioFile'"
			extension.MoveTo(newExt.FullName);
			return newExt;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < FileInfo > getExtensions(FileInfo dir)
		{
			return null;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < FileInfo > getActiveExtensions()
		{
			return null;
		}

		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < FileInfo > getInactiveExtensions()
		{
			return null;
		}

		public virtual bool isExtensionActive(System.IO.FileInfo extension)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			foreach (FileInfo f in getActiveExtensions())
			{
				if (f.Name.Equals(extension.Name))
				{
					return true;
				}
			}
			return false;
		}
	}
}