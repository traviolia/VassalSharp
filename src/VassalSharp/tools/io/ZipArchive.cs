/*
* $Id$
*
* Copyright (c) 2009 by Joel Uckelman
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
using System.IO;

namespace VassalSharp.tools.io
{

#if NEVER_DEFINED
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import static VassalSharp.tools.IterableEnumeration.iterate;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.BufferedInputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.BufferedOutputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.ByteArrayInputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.File;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.FileInputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.FileNotFoundException;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.FileOutputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.FilterInputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.IOException;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.InputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.io.OutputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.ArrayList;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.HashMap;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.List;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.Map;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.concurrent.locks.Lock;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.concurrent.locks.ReadWriteLock;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.zip.CRC32;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.zip.CheckedOutputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.zip.Checksum;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.zip.ZipEntry;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.zip.ZipFile;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.zip.ZipInputStream;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import java.util.zip.ZipOutputStream;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import org.apache.commons.io.FileUtils;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.Info;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	import VassalSharp.tools.concurrent.CountingReadWriteLock;
#endif

#if NEVER_DEFINED
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public class ZipArchive : FileArchive
	{
		private void  InitBlock()
		{
			r = rwl.readLock();
			w = rwl.writeLock();
			r.lock_Renamed();
			try
			{
				openIfClosed();
				return ;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				new ArrayList < String >(entries.keySet());
			}
			finally
			{
				r.unlock();
			}
			if (root.length() == 0)
			{
				return getFiles();
			}
			
			r.lock_Renamed();
			try
			{
				openIfClosed();
				
				// FIXME: directories need not have entries in the ZipFile!
				//      if (!entries.containsKey(root))
				//        throw new FileNotFoundException(root + " not in archive");
				
				root += '/';
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final ArrayList < String > names = new ArrayList < String >();
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String n: entries.keySet())
				{
					if (n.startsWith(root))
					{
						names.add(n);
					}
				}
				
				return names;
			}
			finally
			{
				r.unlock();
			}
		}
		/// <summary>{@inheritDoc} </summary>
		virtual public string Name
		{
			get
			{
				return archiveFile.getPath();
			}
			
		}
		/// <summary>{@inheritDoc} </summary>
		virtual public bool Closed
		{
			get
			{
				return closed;
			}
			
		}
		/// <summary>{@inheritDoc} </summary>
		virtual public bool Modified
		{
			get
			{
				return modified;
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'archiveFile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private File archiveFile;
		private ZipFile zipFile;
		
		private bool modified = false;
		private bool closed = true;
		
		private class Entry
		{
			public ZipEntry ze;
			public File file;
			
			public Entry(ZipEntry ze, File file)
			{
				this.ze = ze;
				this.file = file;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public override string ToString()
			{
				return GetType().FullName + "[file=\"" + file + "\", ze=\"" + ze + "\"]";
			}
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private final Map < String, Entry > entries = new HashMap < String, Entry >();
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'rwl '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ReadWriteLock rwl = new CountingReadWriteLock();
		//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'r' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private Lock r;
		//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'w' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private Lock w;
		
		/// <summary> Opens a ZIP archive.
		/// 
		/// </summary>
		/// <param name="path">the name of the archive
		/// </param>
		/// <throws>  IOException </throws>
		public ZipArchive(string path):this(path, false)
		{
		}
		
		/// <summary> Opens a ZIP archive.
		/// 
		/// </summary>
		/// <param name="file">the name of the archive
		/// </param>
		/// <throws>  IOException </throws>
		public ZipArchive(File file):this(file, false)
		{
		}
		
		/// <summary> Opens a ZIP archive.
		/// 
		/// </summary>
		/// <param name="path">the name of the archive
		/// </param>
		/// <param name="truncate">if <code>true</code>, truncate the archive file on open
		/// </param>
		/// <throws>  IOException </throws>
		public ZipArchive(string path, bool truncate):this(new File(path), truncate)
		{
		}
		
		/// <summary> Opens a ZIP archive.
		/// 
		/// </summary>
		/// <param name="file">the name of the archive
		/// </param>
		/// <param name="truncate">if <code>true</code>, truncate the archive file on open
		/// </param>
		/// <throws>  IOException </throws>
		public ZipArchive(File file, bool truncate)
		{
			InitBlock();
			if (file == null)
				throw new System.ArgumentException();
			this.archiveFile = file;
			
			if (truncate)
			{
				archiveFile.delete();
			}
		}
		
		/// <summary> Copies a ZIP archive.
		/// 
		/// </summary>
		/// <param name="src">the name of the source archive
		/// </param>
		/// <param name="dst">the name of the destination archive
		/// </param>
		/// <throws>  IOException </throws>
		public ZipArchive(FileArchive src, string dst):this(src, new File(dst))
		{
		}
		
		/// <summary> Copies a ZIP archive.
		/// 
		/// </summary>
		/// <param name="src">the name of the source archive
		/// </param>
		/// <param name="dst">the name of the destination archive
		/// </param>
		/// <throws>  IOException </throws>
		public ZipArchive(FileArchive src, File dst):this(dst, true)
		{
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'buf '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] buf = new sbyte[8192];
			
			// copy each entry to the new archive
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String name: src.getFiles())
			{
				InputStream in_Renamed = null;
				try
				{
					in_Renamed = src.getInputStream(name);
					
					OutputStream out_Renamed = null;
					try
					{
						out_Renamed = getOutputStream(name);
						IOUtils.copy(in_Renamed, out_Renamed, buf);
						out_Renamed.close();
					}
					finally
					{
						IOUtils.closeQuietly(out_Renamed);
					}
					
					in_Renamed.close();
				}
				finally
				{
					IOUtils.closeQuietly(in_Renamed);
				}
			}
			
			flush();
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual File getFile()
		{
			return archiveFile;
		}
		
		/// <summary> {@inheritDoc}
		/// 
		/// <b>Note:</b> It is impeative the that calling code ensures that this
		/// stream is eventually closed, since the returned stream holds a read
		/// lock on the archive.
		/// </summary>
		public virtual InputStream getInputStream(string path)
		{
			r.lock_Renamed();
			try
			{
				openIfClosed();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Entry e = entries.get_Renamed(path);
				if (e == null)
				{
					throw new FileNotFoundException(path + " not in archive");
				}
				
				InputStream in_Renamed = null;
				if (e.file != null)
				{
					in_Renamed = new FileInputStream(e.file);
				}
				else if (zipFile != null)
				{
					// NB: Undocumented, but ZipFile.getInputStream can return null!
					in_Renamed = zipFile.getInputStream(e.ze);
				}
				
				if (in_Renamed == null)
				{
					throw new FileNotFoundException(path + " not in archive");
				}
				
				return new ZipArchiveInputStream(in_Renamed);
			}
			catch (IOException ex)
			{
				r.unlock();
				throw ex;
			}
		}
		
		/// <summary> {@inheritDoc}
		/// 
		/// <b>Note:</b> It is imperative the that calling code ensures that this
		/// stream is eventually closed, since the returned stream holds a write
		/// lock on the archive.
		/// </summary>
		public virtual OutputStream getOutputStream(string path)
		{
			return getOutputStream(path, true);
		}
		
		/// <summary> Gets an {@link OutputStream} to write to the given file.
		/// 
		/// <b>Note:</b> It is imperative the that calling code ensures that this
		/// stream is eventually closed, since the returned stream holds a write
		/// lock on the archive.
		/// 
		/// </summary>
		/// <param name="path">the path to the file in the archive
		/// </param>
		/// <param name="compress">whether to compress the file
		/// </param>
		/// <returns> an <code>OutputStream</code> for the requested file
		/// </returns>
		/// <throws>  IOException </throws>
		public virtual OutputStream getOutputStream(string path, bool compress)
		{
			w.lock_Renamed();
			try
			{
				openIfClosed();
				
				modified = true;
				
				// set up new ZipEntry
				//UPGRADE_NOTE: Final was removed from the declaration of 'ze '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ZipEntry ze = new ZipEntry(path);
				ze.setMethod(compress?ZipEntry.DEFLATED:ZipEntry.STORED);
				
				// create new temp file
				//UPGRADE_NOTE: Final was removed from the declaration of 'tf '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				File tf = File.createTempFile("zip", ".tmp", Info.getTempDir());
				
				// set up new Entry
				//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Entry e = new Entry(ze, tf);
				//UPGRADE_NOTE: Final was removed from the declaration of 'old '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Entry old = entries.put(path, e);
				
				// clean up old temp file
				if (old != null && old.file != null)
				{
					old.file.delete();
				}
				
				return new ZipArchiveOutputStream(new FileOutputStream(e.file), new CRC32(), e.ze);
			}
			catch (IOException ex)
			{
				w.unlock();
				throw ex;
			}
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  add(string path, string extPath)
		{
			add(path, new File(extPath));
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  add(string path, File extPath)
		{
			FileInputStream in_Renamed = null;
			try
			{
				in_Renamed = new FileInputStream(extPath);
				add(path, in_Renamed);
				in_Renamed.close();
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  add(string path, sbyte[] bytes)
		{
			add(path, new ByteArrayInputStream(bytes));
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  add(string path, InputStream in_Renamed)
		{
			OutputStream out_Renamed = null;
			try
			{
				out_Renamed = getOutputStream(path);
				IOUtils.copy(in_Renamed, out_Renamed);
				out_Renamed.close();
			}
			finally
			{
				IOUtils.closeQuietly(out_Renamed);
			}
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual bool remove(string path)
		{
			w.lock_Renamed();
			try
			{
				openIfClosed();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Entry e = entries.remove(path);
				if (e != null)
				{
					modified = true;
					
					if (e.file != null)
					{
						e.file.delete();
					}
				}
				
				return e != null;
			}
			finally
			{
				w.unlock();
			}
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  revert()
		{
			w.lock_Renamed();
			try
			{
				if (!modified)
				{
					return ;
				}
				
				// delete all temporary files
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Entry e: entries.values())
				{
					if (e != null && e.file != null)
					{
						e.file.delete();
					}
				}
				
				modified = false;
			}
			finally
			{
				w.unlock();
			}
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  flush()
		{
			w.lock_Renamed();
			try
			{
				if (modified)
				{
					writeToDisk();
				}
			}
			finally
			{
				w.unlock();
			}
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  close()
		{
			w.lock_Renamed();
			try
			{
				if (closed)
				{
					return ;
				}
				else if (modified)
				{
					writeToDisk();
				}
				else if (zipFile != null)
				{
					zipFile.close();
					zipFile = null;
					
					closed = true;
					entries.clear();
				}
			}
			finally
			{
				w.unlock();
			}
		}
		
		private void  writeToDisk()
		{
			// write all files to a temporary zip archive
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmpFile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			File tmpFile = File.createTempFile("tmp", ".zip", archiveFile.getParentFile());
			
			ZipOutputStream out_Renamed = null;
			try
			{
				out_Renamed = new ZipOutputStream(new BufferedOutputStream(new FileOutputStream(tmpFile)));
				out_Renamed.setLevel(9);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'buf '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				sbyte[] buf = new sbyte[8192];
				
				if (zipFile != null)
				{
					zipFile.close();
					zipFile = null;
					
					// copy unmodified file into the temp archive
					ZipInputStream in_Renamed = null;
					try
					{
						in_Renamed = new ZipInputStream(new BufferedInputStream(new FileInputStream(archiveFile)));
						
						ZipEntry ze = null;
						while ((ze = in_Renamed.getNextEntry()) != null)
						{
							// skip modified or removed entries
							//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							Entry e = entries.get_Renamed(ze.Name);
							if (e == null || e.file != null)
								continue;
							
							// We can't reuse entries for compressed files because there's
							// no way to reset all fields to acceptable values.
							if (ze.getMethod() == ZipEntry.DEFLATED)
							{
								//UPGRADE_NOTE: Final was removed from the declaration of 'nze '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
								ZipEntry nze = new ZipEntry(ze.Name);
								nze.setTime(ze.getTime());
								ze = nze;
							}
							
							out_Renamed.putNextEntry(ze);
							IOUtils.copy(in_Renamed, out_Renamed, buf);
							
							entries.remove(ze.Name);
						}
						
						in_Renamed.close();
					}
					finally
					{
						IOUtils.closeQuietly(in_Renamed);
					}
				}
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Entry e: entries.values())
				{
					// skip removed or unmodified files
					if (e == null || e.file == null)
						continue;
					
					// write new or modified file into the temp archive
					FileInputStream in_Renamed = null;
					try
					{
						in_Renamed = new FileInputStream(e.file);
						e.ze.setTime(e.file.lastModified());
						out_Renamed.putNextEntry(e.ze);
						IOUtils.copy(in_Renamed, out_Renamed, buf);
						in_Renamed.close();
					}
					finally
					{
						IOUtils.closeQuietly(in_Renamed);
					}
				}
				
				out_Renamed.close();
			}
			finally
			{
				IOUtils.closeQuietly(out_Renamed);
			}
			
			// Replace old archive with temp archive.
			if (!tmpFile.renameTo(archiveFile))
			{
				try
				{
					FileUtils.forceDelete(archiveFile);
					FileUtils.moveFile(tmpFile, archiveFile);
				}
				catch (IOException e)
				{
					string err = "Unable to overwrite " + archiveFile.getAbsolutePath() + ": ";
					
					if (!archiveFile.exists())
					{
						err += " file does not exist.";
					}
					else if (!archiveFile.canWrite())
					{
						err += " file is not writable.";
					}
					else if (!archiveFile.isFile())
					{
						err += " not a normal file.";
					}
					
					err += (" Data written to " + tmpFile.getAbsolutePath() + " instead.");
					throw (IOException) new IOException(err).initCause(e);
				}
			}
			
			// Delete all temporary files
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Entry e: entries.values())
			{
				if (e != null && e.file != null)
				{
					e.file.delete();
				}
			}
			
			closed = true;
			modified = false;
			entries.clear();
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual bool contains(string path)
		{
			r.lock_Renamed();
			try
			{
				openIfClosed();
				return entries.containsKey(path);
			}
			finally
			{
				r.unlock();
			}
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual long getSize(string path)
		{
			r.lock_Renamed();
			try
			{
				openIfClosed();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Entry e = entries.get_Renamed(path);
				if (e == null)
				{
					throw new FileNotFoundException(path + " not in archive");
				}
				
				return e.file == null?e.ze.getSize():e.file.length();
			}
			finally
			{
				r.unlock();
			}
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual long getMTime(string path)
		{
			r.lock_Renamed();
			try
			{
				openIfClosed();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Entry e = entries.get_Renamed(path);
				if (e == null)
				{
					throw new FileNotFoundException(path + " not in archive");
				}
				
				return e.file == null?e.ze.getTime():e.file.lastModified();
			}
			finally
			{
				r.unlock();
			}
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getFiles() throws IOException
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getFiles(String root) throws IOException
		
		/// <summary>Rebuilds the {@link ZipEntries} from our underlying {@link ZipFile}. </summary>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'readEntries'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		private void  readEntries()
		{
			lock (this)
			{
				entries.clear();
				
				if (archiveFile.exists() && archiveFile.length() > 0)
				{
					zipFile = new ZipFile(archiveFile);
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(ZipEntry e: iterate(zipFile.entries()))
					{
						entries.put(e.Name, new Entry(e, null));
					}
				}
			}
		}
		
		/// <summary>Opens the archive if it is closed. </summary>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'openIfClosed'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		private void  openIfClosed()
		{
			lock (this)
			{
				if (closed)
				{
					readEntries();
					modified = false;
					closed = false;
				}
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ZipArchiveInputStream' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary>An {@link InputStream} which releases the read lock on close. </summary>
		private class ZipArchiveInputStream:FilterInputStream
		{
			private void  InitBlock(ZipArchive enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ZipArchive enclosingInstance;
			public ZipArchive Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public ZipArchiveInputStream(ZipArchive enclosingInstance, InputStream in_Renamed):base(in_Renamed)
			{
				InitBlock(enclosingInstance);
				
				if (in_Renamed == null)
				{
					throw new System.NullReferenceException("in == null");
				}
			}
			
			private bool closed = false;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public virtual void  close()
			{
				if (closed)
				{
					return ;
				}
				
				try
				{
					base.close();
				}
				finally
				{
					Enclosing_Instance.r.unlock();
					closed = true;
				}
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ZipArchiveOutputStream' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> An {@link OutputStream} which calculates a checksum, counts bytes
		/// written, and releases the write lock on close.
		/// </summary>
		private class ZipArchiveOutputStream:CheckedOutputStream
		{
			private void  InitBlock(ZipArchive enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ZipArchive enclosingInstance;
			public ZipArchive Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private ZipEntry entry;
			private long count = 0;
			
			public ZipArchiveOutputStream(ZipArchive enclosingInstance, OutputStream out_Renamed, Checksum cksum, ZipEntry e):base(out_Renamed, cksum)
			{
				InitBlock(enclosingInstance);
				
				if (out_Renamed == null)
				{
					throw new System.NullReferenceException("out == null");
				}
				
				if (cksum == null)
				{
					throw new System.NullReferenceException("cksum == null");
				}
				
				if (e == null)
				{
					throw new System.NullReferenceException("e == null");
				}
				
				entry = e;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public virtual void  write(sbyte[] bytes, int off, int len)
			{
				base.write(bytes, off, len);
				count += len;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public virtual void  write(int b)
			{
				base.write(b);
				++count;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public virtual void  flush()
			{
				base.flush();
				entry.setSize(count);
				entry.setCrc(getChecksum().getValue());
			}
			
			private bool closed = false;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public virtual void  close()
			{
				if (closed)
				{
					return ;
				}
				
				try
				{
					base.close();
				}
				finally
				{
					Enclosing_Instance.w.unlock();
					closed = true;
				}
			}
		}
		
		[STAThread]
		public static void  Main(string[] args)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'archive '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ZipArchive archive = new ZipArchive("test.zip");
			
			// write test
			archive.add("NOTES", "NOTES");
			archive.add("README.txt", "README.txt");
			
			archive.flush();
			
			// read test
			
			InputStream in_Renamed = null;
			try
			{
				in_Renamed = archive.getInputStream("NOTES");
				IOUtils.copy(in_Renamed, System.Console.OpenStandardOutput());
				in_Renamed.close();
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
			
			archive.close();
		}
	}
#else
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public class ZipArchive : FileArchive
	{
		/// <summary>{@inheritDoc} </summary>
		virtual public string Name
		{
			get
			{
				return string.Empty;
			}
			
		}
		/// <summary>{@inheritDoc} </summary>
		virtual public bool Closed
		{
			get
			{
				return true;
			}
			
		}
		/// <summary>{@inheritDoc} </summary>
		virtual public bool Modified
		{
			get
			{
				return false;
			}
			
		}

		/// <summary> Opens a ZIP archive.
		/// 
		/// </summary>
		/// <param name="path">the name of the archive
		/// </param>
		/// <throws>  IOException </throws>
		public ZipArchive(string path):this(path, false)
		{
		}
		
		/// <summary> Opens a ZIP archive.
		/// 
		/// </summary>
		/// <param name="file">the name of the archive
		/// </param>
		/// <throws>  IOException </throws>
		public ZipArchive(File file):this(file, false)
		{
		}
		
		/// <summary> Opens a ZIP archive.
		/// 
		/// </summary>
		/// <param name="path">the name of the archive
		/// </param>
		/// <param name="truncate">if <code>true</code>, truncate the archive file on open
		/// </param>
		/// <throws>  IOException </throws>
		public ZipArchive(string path, bool truncate):this(new File(path), truncate)
		{
		}
		
		/// <summary> Opens a ZIP archive.
		/// 
		/// </summary>
		/// <param name="file">the name of the archive
		/// </param>
		/// <param name="truncate">if <code>true</code>, truncate the archive file on open
		/// </param>
		/// <throws>  IOException </throws>
		public ZipArchive(File file, bool truncate)
		{
		}
		
		/// <summary> Copies a ZIP archive.
		/// 
		/// </summary>
		/// <param name="src">the name of the source archive
		/// </param>
		/// <param name="dst">the name of the destination archive
		/// </param>
		/// <throws>  IOException </throws>
		public ZipArchive(FileArchive src, string dst):this(src, new File(dst))
		{
		}
		
		/// <summary> Copies a ZIP archive.
		/// 
		/// </summary>
		/// <param name="src">the name of the source archive
		/// </param>
		/// <param name="dst">the name of the destination archive
		/// </param>
		/// <throws>  IOException </throws>
		public ZipArchive(FileArchive src, File dst):this(dst, true)
		{
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual File getFile()
		{
			return null;
		}
		
		/// <summary> {@inheritDoc}
		/// 
		/// <b>Note:</b> It is impeative the that calling code ensures that this
		/// stream is eventually closed, since the returned stream holds a read
		/// lock on the archive.
		/// </summary>
		public virtual Stream getInputStream(string path)
		{
			return null;
		}

		/// <summary> {@inheritDoc}
		/// 
		/// <b>Note:</b> It is imperative the that calling code ensures that this
		/// stream is eventually closed, since the returned stream holds a write
		/// lock on the archive.
		/// </summary>
		public virtual Stream getOutputStream(string path)
		{
			return null;
		}

		/// <summary> Gets an {@link OutputStream} to write to the given file.
		/// 
		/// <b>Note:</b> It is imperative the that calling code ensures that this
		/// stream is eventually closed, since the returned stream holds a write
		/// lock on the archive.
		/// 
		/// </summary>
		/// <param name="path">the path to the file in the archive
		/// </param>
		/// <param name="compress">whether to compress the file
		/// </param>
		/// <returns> an <code>OutputStream</code> for the requested file
		/// </returns>
		/// <throws>  IOException </throws>
		public virtual Stream getOutputStream(string path, bool compress)
		{
			return null;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  add(string path, string extPath)
		{
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  add(string path, File extPath)
		{
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  add(string path, sbyte[] bytes)
		{
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  add(string path, Stream in_Renamed)
		{
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual bool remove(string path)
		{
			return true;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  revert()
		{
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  flush()
		{
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  close()
		{
		}
		
		private void  writeToDisk()
		{
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual bool contains(string path)
		{
			return false;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual long getSize(string path)
		{
			return 0;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual long getMTime(string path)
		{
			return 0;
		}

		public List<string> getFiles() { return null; }

		public List<string> getFiles(String root) { return null; }

		/// <summary>Rebuilds the {@link ZipEntries} from our underlying {@link ZipFile}. </summary>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'readEntries'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		private void  readEntries()
		{
		}

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        //UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ZipArchiveInputStream' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
        /// <summary>An {@link InputStream} which releases the read lock on close. </summary>
        private class ZipArchiveInputStream // :FilterInputStream
		{
			public ZipArchiveInputStream(ZipArchive enclosingInstance, Stream in_Renamed) //:base(in_Renamed)
			{
			}
			
			private bool closed = false;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public virtual void  close()
			{
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ZipArchiveOutputStream' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> An {@link OutputStream} which calculates a checksum, counts bytes
		/// written, and releases the write lock on close.
		/// </summary>
		private class ZipArchiveOutputStream  //: CheckedOutputStream
		{
			
			public ZipArchiveOutputStream(ZipArchive enclosingInstance, Stream out_Renamed, long /*Checksum*/ cksum, ZipEntry e) //:base(out_Renamed, cksum)
			{
			}
			public virtual void  write(sbyte[] bytes, int off, int len)
			{
			}
			public virtual void  write(int b)
			{
			}
			public virtual void  flush()
			{
			}
			
			public virtual void  close()
			{
			}
		}
		
	}
#endif

}