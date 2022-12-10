/*
* $Id$
*
* Copyright (c) 2009-2010 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.io.Closeable' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Closeable = java.io.Closeable;
namespace VassalSharp.tools.io
{
	
	/// <summary> An interface for providing read access to files.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public interface FileStore:Closeable
	{
		/// <summary> Queries whether a file exists in the store.
		/// 
		/// </summary>
		/// <param name="path">the path to the file in the store
		/// </param>
		/// <returns> <code>true</code> if the file exists in the store
		/// 
		/// </returns>
		/// <throws>  IOException </throws>
		bool contains(System.String path);
		
		/// <summary> Gets an {@link InputStream} to read from the given file.
		/// 
		/// </summary>
		/// <param name="path">the path to the file in the store
		/// </param>
		/// <returns> an <code>InputStream</code> containing the requested file
		/// 
		/// </returns>
		/// <throws>  IOException </throws>
		System.IO.Stream getInputStream(System.String path);
		
		/// <summary> Gets the size of a file in the store, in bytes.
		/// 
		/// </summary>
		/// <param name="path">the path to the file in the store
		/// </param>
		/// <returns> the size of the file, in bytes
		/// 
		/// </returns>
		/// <throws>  FileNotFoundException if <code>path</code> is not in the store </throws>
		/// <throws>  IOException </throws>
		long getSize(System.String path);
		
		/// <summary> Gets the modification time of a file in the store, in milliseconds
		/// since the epoch.
		/// 
		/// </summary>
		/// <param name="path">the path to the file in the store
		/// </param>
		/// <returns> the mtime of the file
		/// 
		/// </returns>
		/// <throws>  FileNotFoundException if <code>path</code> is not in the store </throws>
		/// <throws>  IOException </throws>
		long getMTime(System.String path);
		
		/// <summary> Gets the list of files in the store.
		/// 
		/// </summary>
		/// <returns> the list of files in the store
		/// 
		/// </returns>
		/// <throws>  IOException </throws>
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public List < String > getFiles() throws IOException;
	
	/// <summary> Gets the list of files under a given directory of the store.
	/// 
	/// </summary>
	/// <param name="root">the directory
	/// </param>
	/// <returns> the list of files under the given directory
	/// 
	/// </returns>
	/// <throws>  FileNotFoundException if <code>root</code> is not in the store </throws>
	/// <throws>  IOException </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public List < String > getFiles(String root) throws IOException;
	
	/// <summary> Closes the store.
	/// 
	/// </summary>
	/// <throws>  IOException </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void close() throws IOException;
	
	/// <summary> Checks whether the store is closed.
	/// 
	/// </summary>
	/// <returns> <code>true</code> if the archive is closed
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean isClosed();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}