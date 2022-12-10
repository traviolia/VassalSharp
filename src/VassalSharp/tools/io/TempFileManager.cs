/*
* $Id$
*
* Copyright (c) 2007 by Joel Uckelman
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
//UPGRADE_TODO: The type 'org.apache.commons.io.FileUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using FileUtils = org.apache.commons.io.FileUtils;
//UPGRADE_TODO: The type 'org.apache.commons.lang.SystemUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SystemUtils = org.apache.commons.lang.SystemUtils;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using Info = VassalSharp.Info;
namespace VassalSharp.tools.io
{
	
	/// <summary> Handles temporary files. <code>TempFileManager</code> cleans up
	/// stale temporary files when the singleton is constructed, and ones
	/// created by the current session on exit through a shutdown hook.
	/// 
	/// <p>A temporary director is created in <code>"user.dir" + "/tmp"</code>
	/// for each <code>TempFileManager</code> instance, which, since it is
	/// a singleton, amounts to one temporary directory per VASSAL instance.
	/// Each session directory has a corresponding lock file, which indicates
	/// that the VASSAL instance associated with that directory is live.
	/// Directories which are not live will be cleaned up on creation of a
	/// <code>TempFileManager</code> instance.</p>
	/// 
	/// <p>Due to Sun Bugs
	/// <a href="http://bugs.sun.com/bugdatabase/view_bug.do?bug_id=4171239">
	/// #4171239</a>,
	/// <a href="http://bugs.sun.com/bugdatabase/view_bug.do?bug_id=4724038">
	/// #4724038</a>, and
	/// <a href="http://bugs.sun.com/bugdatabase/view_bug.do?bug_id=6359560">
	/// #6359560</a>, {@link File.deleteOnExit()} is unreliable on Windows,
	/// and there is no way to unmap memory-mapped files so that they may
	/// be deleted by {@link File.delete()}. This class overcomes these
	/// shortcomings by handling temporary files directly.</p>
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	/// <deprecated> Create temporary files with {@link File.createTempFile()}
	/// in {@link VassalSharp.Info.getTempDir()}.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class TempFileManager
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassThread' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassThread:SupportClass.ThreadClass
		{
			public AnonymousClassThread(TempFileManager enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TempFileManager enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TempFileManager enclosingInstance;
			public TempFileManager Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			override public void  Run()
			{
				// Run the garbage collector and finalize repeatedly, with
				// exponentially increasing pauses, until we succeed at deleting
				// the whole session temp directory or we hit the sleep limit.
				long sleep = 1;
				//UPGRADE_NOTE: Final was removed from the declaration of 'maxsleep '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				long maxsleep = 1024;
				while (true)
				{
					System.GC.Collect();
					//UPGRADE_ISSUE: Method 'java.lang.System.runFinalization' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
					System_Renamed.runFinalization();
					
					try
					{
						Enclosing_Instance.cleanupSessionRoot();
						break;
					}
					catch (System.IO.IOException e)
					{
						if (sleep > maxsleep)
						{
							// just log, since shutdown hooks don't have long to run
							VassalSharp.tools.io.TempFileManager.logger.error("", e);
							break;
						}
						
						try
						{
							//UPGRADE_TODO: Method 'java.lang.Thread.sleep' was converted to 'System.Threading.Thread.Sleep' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangThreadsleep_long'"
							System.Threading.Thread.Sleep(new System.TimeSpan((System.Int64) 10000 * sleep));
						}
						catch (System.Threading.ThreadInterruptedException ignore)
						{
						}
						
						sleep *= 2;
					}
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassThread1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassThread1:SupportClass.ThreadClass
		{
			public AnonymousClassThread1(TempFileManager enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TempFileManager enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TempFileManager enclosingInstance;
			public TempFileManager Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			override public void  Run()
			{
				try
				{
					Enclosing_Instance.cleanupSessionRoot();
				}
				catch (System.IO.IOException e)
				{
					// just log, since shutdown hooks don't have long to run
					VassalSharp.tools.io.TempFileManager.logger.error("", e);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassFileFilter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_ISSUE: Interface 'java.io.FileFilter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioFileFilter'"
		private class AnonymousClassFileFilter : FileFilter
		{
			public AnonymousClassFileFilter(TempFileManager enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TempFileManager enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TempFileManager enclosingInstance;
			public TempFileManager Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual bool accept(System.IO.FileInfo f)
			{
				return System.IO.Directory.Exists(f.FullName) && f.Name.StartsWith(VassalSharp.tools.io.TempFileManager.DIR_PREFIX);
			}
		}
		/// <summary> Returns the singleton for this class.
		/// 
		/// </summary>
		/// <returns> an instance of this class
		/// </returns>
		public static TempFileManager Instance
		{
			get
			{
				return instance;
			}
			
		}
		virtual public System.IO.FileInfo SessionRoot
		{
			get
			{
				if (sessionRoot == null)
					sessionRoot = createSessionRoot();
				return new System.IO.FileInfo(sessionRoot.ToString());
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.tools.io.TempFileManager'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'tmpRoot '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.IO.FileInfo tmpRoot;
		private System.IO.FileInfo sessionRoot;
		private System.IO.FileInfo lock_Renamed;
		
		private const System.String DIR_PREFIX = "vassal-";
		
		private TempFileManager()
		{
			//
			// set up for cleanup on shutdown
			//
			if (SystemUtils.IS_OS_WINDOWS)
			{
				//UPGRADE_ISSUE: Method 'java.lang.Runtime.addShutdownHook' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000'"
				System.Diagnostics.Process.GetCurrentProcess().addShutdownHook(new AnonymousClassThread(this));
			}
			else
			{
				//UPGRADE_ISSUE: Method 'java.lang.Runtime.addShutdownHook' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000'"
				System.Diagnostics.Process.GetCurrentProcess().addShutdownHook(new AnonymousClassThread1(this));
			}
			
			tmpRoot = Info.TempDir;
			
			//
			// clean up stale temporary files
			//
			bool tmpBool;
			if (System.IO.File.Exists(tmpRoot.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(tmpRoot.FullName);
			if (tmpBool && System.IO.Directory.Exists(tmpRoot.FullName))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'filter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Interface 'java.io.FileFilter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioFileFilter'"
				FileFilter filter = new AnonymousClassFileFilter(this);
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(File f: tmpRoot.listFiles(filter))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'lock '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.IO.FileInfo lock_Renamed = new File(f.getParent(), f.getName() + ".lck");
					bool tmpBool2;
					if (System.IO.File.Exists(lock_Renamed.FullName))
						tmpBool2 = true;
					else
						tmpBool2 = System.IO.Directory.Exists(lock_Renamed.FullName);
					if (!tmpBool2)
					{
						try
						{
							FileUtils.forceDelete(f);
						}
						catch (System.IO.IOException e)
						{
							logger.error("", e);
						}
					}
				}
			}
		}
		
		private void  cleanupSessionRoot()
		{
			bool tmpBool;
			if (System.IO.File.Exists(lock_Renamed.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(lock_Renamed.FullName);
			if (tmpBool)
				FileUtils.forceDelete(lock_Renamed);
			FileUtils.forceDelete(sessionRoot);
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'instance '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly TempFileManager instance = new TempFileManager();
		
		private System.IO.FileInfo createSessionRoot()
		{
			// ensure that we have a good temporary root
			bool tmpBool;
			if (System.IO.File.Exists(tmpRoot.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(tmpRoot.FullName);
			bool tmpBool2;
			if (System.IO.File.Exists(tmpRoot.FullName))
			{
				System.IO.File.Delete(tmpRoot.FullName);
				tmpBool2 = true;
			}
			else if (System.IO.Directory.Exists(tmpRoot.FullName))
			{
				System.IO.Directory.Delete(tmpRoot.FullName);
				tmpBool2 = true;
			}
			else
				tmpBool2 = false;
			if (!tmpBool || (!System.IO.Directory.Exists(tmpRoot.FullName) && !tmpBool2))
			{
				FileUtils.forceMkdir(tmpRoot);
			}
			
			// get the name for our session root
			//UPGRADE_NOTE: Final was removed from the declaration of 'dir '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.io.File.createTempFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioFilecreateTempFile_javalangString_javalangString_javaioFile'"
			System.IO.FileInfo dir = File.createTempFile(DIR_PREFIX, "", tmpRoot);
			// delete it in case a file was created
			bool tmpBool3;
			if (System.IO.File.Exists(dir.FullName))
			{
				System.IO.File.Delete(dir.FullName);
				tmpBool3 = true;
			}
			else if (System.IO.Directory.Exists(dir.FullName))
			{
				System.IO.Directory.Delete(dir.FullName);
				tmpBool3 = true;
			}
			else
				tmpBool3 = false;
			bool generatedAux = tmpBool3;
			
			// create our lock file before creating the directory to prevent
			// a race with another instance of VASSAL
			lock_Renamed = new System.IO.FileInfo(tmpRoot.FullName + "\\" + dir.Name + ".lck");
			SupportClass.FileSupport.CreateNewFile(lock_Renamed);
			//UPGRADE_ISSUE: Method 'java.io.File.deleteOnExit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioFiledeleteOnExit'"
			lock_Renamed.deleteOnExit();
			
			// now create our session root directory
			FileUtils.forceMkdir(dir);
			
			return dir;
		}
		
		/// <summary> Creates a new directory with the given name in the session temporary
		/// directory.
		/// 
		/// </summary>
		/// <param name="dirname">the name of the directory to create
		/// </param>
		/// <throws>  IOException if the directory cannot be created. </throws>
		public virtual System.IO.FileInfo createTempDir(System.String dirname)
		{
			if (sessionRoot == null)
				sessionRoot = createSessionRoot();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'dir '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.FileInfo dir = new System.IO.FileInfo(sessionRoot.FullName + "\\" + dirname);
			FileUtils.forceMkdir(dir);
			return dir;
		}
		
		/// <summary> Creates a new empty file in the session temporary directory, using the
		/// given prefix and suffix strings to generate its name. This method
		/// is otherwise the same as {@link File.createTempFile(String,String)}.
		/// 
		/// </summary>
		/// <param name="prefix">
		/// </param>
		/// <param name="suffix">
		/// </param>
		/// <returns> the created file
		/// </returns>
		/// <throws>  IOException if a file could not be created </throws>
		public virtual System.IO.FileInfo createTempFile(System.String prefix, System.String suffix)
		{
			if (sessionRoot == null)
				sessionRoot = createSessionRoot();
			//UPGRADE_ISSUE: Method 'java.io.File.createTempFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioFilecreateTempFile_javalangString_javalangString_javaioFile'"
			return File.createTempFile(prefix, suffix, sessionRoot);
		}
		static TempFileManager()
		{
			logger = LoggerFactory.getLogger(typeof(TempFileManager));
		}
	}
}