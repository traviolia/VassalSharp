/*
 * Copyright (c) 2000-2009 by Rodney Kinney, Joel Uckelman
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
using System.Collections.Generic;

namespace VassalSharp.tools.io
{
	
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public interface FileArchive : IDisposable
	{
		/// <summary> Gets the path to the archive file.
		/// 
		/// </summary>
		/// <returns> the path as a <code>String</code>
		/// </returns>
		string Name
		{
			get;
			
		}
		/// <summary> Queries whether the archive is closed.
		/// 
		/// </summary>
		/// <returns> <code>true</code> if the archive is closed
		/// </returns>
		bool Closed
		{
			get;
			
		}
		/// <summary> Queries whether the archive has unsaved modifications.
		/// 
		/// </summary>
		/// <returns> <code>true</code> if the archive is modified
		/// </returns>
		bool Modified
		{
			get;
			
		}
		
		/// <summary> Gets the path to the archive file.
		/// 
		/// </summary>
		/// <returns> the path as a <code>File</code>
		/// </returns>
		File getFile();
		
		/// <summary> Gets an {@link InputStream} to read from the given file.
		/// 
		/// </summary>
		/// <param name="path">the path to the file in the archive
		/// </param>
		/// <returns> an <code>InputStream</code> containing the requested file
		/// </returns>
		/// <throws>  IOException </throws>
		System.IO.Stream getInputStream(string path);
		
		/// <summary> Gets an {@link OutputStream} to write to the given file.
		/// 
		/// </summary>
		/// <param name="path">the path to the file in the archive
		/// </param>
		/// <returns> an <code>OutputStream</code> for the requested file
		/// </returns>
		/// <throws>  IOException </throws>
		System.IO.Stream getOutputStream(string path);
		
		/// <summary> Adds a file to the archive.
		/// 
		/// </summary>
		/// <param name="path">the internal path of the file to be added
		/// </param>
		/// <param name="extPath">the external path of the file to be added
		/// </param>
		/// <throws>  IOException </throws>
		void  add(string path, string extPath);
		
		/// <summary> Adds a file to the archive.
		/// 
		/// </summary>
		/// <param name="path">the internal path of the file to be added
		/// </param>
		/// <param name="extPath">the external path to the file to be added
		/// </param>
		/// <throws>  IOException </throws>
		void  add(string path, File extPath);
		
		/// <summary> Adds the contents of a byte array to the archive.
		/// 
		/// </summary>
		/// <param name="path">the internal path of the file to be added
		/// </param>
		/// <param name="bytes">the bytes to be added
		/// </param>
		/// <throws>  IOException </throws>
		void  add(string path, sbyte[] bytes);
		
		/// <summary> Adds the contents of an {@link InputStream} to the archive.
		/// 
		/// </summary>
		/// <param name="path">the internal path of the file to be added
		/// </param>
		/// <param name="bytes">the <code>InputStream</code> to read from
		/// </param>
		/// <throws>  IOException </throws>
		void  add(string path, System.IO.Stream in_Renamed);
		
		/// <summary> Removes a file from the archive.
		/// 
		/// </summary>
		/// <param name="path">the path to the file in the archive
		/// </param>
		/// <returns> <code>true</code> if the file existed in the archive
		/// </returns>
		/// <throws>  IOException </throws>
		bool remove(string path);
		
		/// <summary> Reverts the archive to its last saved state.
		/// 
		/// </summary>
		/// <throws>  IOException </throws>
		void  revert();
		
		/// <summary> Forces all changes to the archive to disk.
		/// 
		/// </summary>
		/// <throws>  IOExcetpion </throws>
		void  flush();
		
		/// <summary> Closes the archive, writing all changes to disk.
		/// 
		/// </summary>
		/// <throws>  IOException </throws>
		void  close();
		
		/// <summary> Queries whether a file exists in the archive.
		/// 
		/// </summary>
		/// <param name="path">the path to the file in the archive
		/// </param>
		/// <returns> <code>true</code> if the file exists in the archive
		/// </returns>
		/// <throws>  IOException </throws>
		bool contains(string path);
		
		/// <summary> Gets the size of a file in the archive, in bytes.
		/// 
		/// </summary>
		/// <param name="path">the path to the file in the archive
		/// </param>
		/// <returns> the size of the file, in bytes
		/// </returns>
		/// <throws>  FileNotFoundException if <code>path</code> is not in the archive </throws>
		/// <throws>  IOException </throws>
		long getSize(string path);
		
		/// <summary> Gets the modification time of a file in the archive, in milliseconds
		/// since the epoch.
		/// 
		/// </summary>
		/// <param name="path">the path to the file in the archive
		/// </param>
		/// <returns> the mtime of the file
		/// </returns>
		/// <throws>  FileNotFoundException if <code>path</code> is not in the archive </throws>
		/// <throws>  IOException </throws>
		long getMTime(string path);

        /// <summary> Gets the list of files in the archive.
        /// 
        /// </summary>
        /// <returns> the list of files in the archive
        /// </returns>
        /// <throws>  IOException </throws>
        List<String> getFiles();

        /// <summary> Gets the list of files under a given directory of the archive.
        /// 
        /// </summary>
        /// <param name="root">the directory
        /// </param>
        /// <returns> the list of files under the given directory
        /// </returns>
        /// <throws>  FileNotFoundException if <code>root</code> is not in the archive </throws>
        /// <throws>  IOException </throws>
        List<String> getFiles(String root);
    }
}