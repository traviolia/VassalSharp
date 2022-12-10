/*
* $Id$
*
* Copyright (c) 2008 by Joel Uckelman
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
namespace VassalSharp.tools.io
{
	
	/// <summary> Some general purpose file manipulation utilities.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	/// <deprecated> Use {@link org.apache.commons.io.FileUtils} instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class FileUtils
	{
		private void  InitBlock()
		{
			// delete children, depth first
			if (parent.isDirectory())
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(File child: parent.listFiles())
				{
					recursiveDeleteHelper(child, failed);
				}
			}
			
			// store leaves which can't be deleted in the failed list
			if (!parent.delete())
				failed.add(parent);
		}
		private FileUtils()
		{
			InitBlock();
		}
		
		/// <summary> Deletes a file.
		/// 
		/// </summary>
		/// <param name="file">the file to delete
		/// </param>
		/// <throws>  IOException if the file cannot be deleted </throws>
		public static void  delete(System.IO.FileInfo file)
		{
			bool tmpBool;
			if (System.IO.File.Exists(file.FullName))
			{
				System.IO.File.Delete(file.FullName);
				tmpBool = true;
			}
			else if (System.IO.Directory.Exists(file.FullName))
			{
				System.IO.Directory.Delete(file.FullName);
				tmpBool = true;
			}
			else
				tmpBool = false;
			if (!tmpBool)
				throw new System.IO.IOException("Failed to delete " + file.FullName);
		}
		
		/// <summary> Creates all directories named by a path.
		/// 
		/// </summary>
		/// <param name="dir">the path to create
		/// </param>
		/// <throws>  IOException if the path cannot be created </throws>
		public static void  mkdirs(System.IO.FileInfo dir)
		{
			//UPGRADE_TODO: Method 'java.io.File.mkdirs' was converted to 'System.IO.Directory.CreateDirectory' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFilemkdirs'"
			if (!System.IO.Directory.CreateDirectory(dir.FullName))
				throw new System.IO.IOException("Failed to create directory " + dir.FullName);
		}
		
		/// <summary> Recursively deletes all files in the tree rooted at the given path.
		/// 
		/// </summary>
		/// <param name="base">the root to delete
		/// </param>
		/// <throws>  IOException if some file cannot be deleted </throws>
		public static void  recursiveDelete(System.IO.FileInfo base_Renamed)
		{
			// we delete as many files as we can
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < File > failed = new ArrayList < File >();
			recursiveDeleteHelper(base_Renamed, failed);
			
			// if any deletions failed, we list them
			if (!failed.isEmpty())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'sb '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				StringBuilder sb = new StringBuilder("Failed to delete");
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(File f: failed) sb.append( ).append(f.getAbsolutePath());
				throw new IOException(sb.toString());
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private static
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void recursiveDeleteHelper(File parent, List < File > failed)
		
		/// <summary> Tries very hard to move a file.
		/// 
		/// </summary>
		/// <param name="src">the source file
		/// </param>
		/// <param name="dst">the destination file
		/// </param>
		/// <throws>  IOException on failure </throws>
		public static void  move(System.IO.FileInfo src, System.IO.FileInfo dst)
		{
			if (src.Equals(dst))
				return ;
			
			// done if File.renameTo() works on the first shot
			//UPGRADE_TODO: Method 'java.io.File.renameTo' was converted to ' System.IO.FileInfo.MoveTo' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFilerenameTo_javaioFile'"
			if (src.MoveTo(dst.FullName))
				return ;
			// otherwise, maybe we're on a platform where we must delete dst first
			bool tmpBool;
			if (System.IO.File.Exists(dst.FullName))
			{
				System.IO.File.Delete(dst.FullName);
				tmpBool = true;
			}
			else if (System.IO.Directory.Exists(dst.FullName))
			{
				System.IO.Directory.Delete(dst.FullName);
				tmpBool = true;
			}
			else
				tmpBool = false;
			bool generatedAux = tmpBool;
			//UPGRADE_TODO: Method 'java.io.File.renameTo' was converted to ' System.IO.FileInfo.MoveTo' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFilerenameTo_javaioFile'"
			if (src.MoveTo(dst.FullName))
				return ;
			
			// otherwise, do the copy manually
			System.IO.Stream in_Renamed = null;
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
				in_Renamed = new System.IO.FileStream(src.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
				System.IO.Stream out_Renamed = null;
				try
				{
					//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javaioFile'"
					out_Renamed = new System.IO.FileStream(dst.FullName, System.IO.FileMode.Create);
					IOUtils.copy(in_Renamed, out_Renamed);
					out_Renamed.Close();
				}
				finally
				{
					IOUtils.closeQuietly(out_Renamed);
				}
				
				in_Renamed.Close();
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
			
			bool tmpBool2;
			if (System.IO.File.Exists(src.FullName))
			{
				System.IO.File.Delete(src.FullName);
				tmpBool2 = true;
			}
			else if (System.IO.Directory.Exists(src.FullName))
			{
				System.IO.Directory.Delete(src.FullName);
				tmpBool2 = true;
			}
			else
				tmpBool2 = false;
			bool generatedAux2 = tmpBool2;
		}
	}
}