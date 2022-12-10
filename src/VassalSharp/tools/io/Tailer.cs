/*
* $Id$
*
* Copyright (c) 2010 by Joel Uckelman
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using DefaultEventListenerSupport = VassalSharp.tools.concurrent.listener.DefaultEventListenerSupport;
//UPGRADE_TODO: The type 'VassalSharp.tools.concurrent.listener.EventListener' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using EventListener = VassalSharp.tools.concurrent.listener.EventListener;
//UPGRADE_TODO: The type 'VassalSharp.tools.concurrent.listener.EventListenerSupport' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using EventListenerSupport = VassalSharp.tools.concurrent.listener.EventListenerSupport;
namespace VassalSharp.tools.io
{
	
	/// <summary> Tail a file. This class is designed to behave similarly to the UNIX
	/// command <code>tail -f</code>, watching the file for changes and
	/// reporting them to listeners. The tailer may be stopped when it is not
	/// needed; if it is restarted, it will remember its last position and
	/// resume reading where it left off.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public class Tailer
	{
		private void  InitBlock()
		{
			if (file == null)
				throw new System.ArgumentException("file == null");
			if (lsup == null)
				throw new System.ArgumentException("lsup == null");
			
			this.file = file;
			this.poll_interval = poll_interval;
			this.lsup = lsup;
			lsup.addEventListener(l);
			lsup.removeEventListener(l);
			return lsup.getEventListeners();
		}
		/// <summary> Checks whether the tailer is running.
		/// 
		/// </summary>
		/// <returns> <code>true</code> if the tailer is running
		/// </returns>
		virtual public bool Tailing
		{
			get
			{
				return tailing;
			}
			
		}
		/// <summary> Gets the file being tailed.
		/// 
		/// </summary>
		/// <returns> the file
		/// </returns>
		virtual public System.IO.FileInfo File
		{
			get
			{
				return file;
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.tools.io.Tailer'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		protected internal const long DEFAULT_POLL_INTERVAL = 1000L;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'file '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.IO.FileInfo file;
		//UPGRADE_NOTE: Final was removed from the declaration of 'poll_interval '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal long poll_interval;
		
		protected internal long position = 0L;
		protected internal volatile bool tailing = false;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected final EventListenerSupport < String > lsup;
		
		/// <summary> Creates a file tailer with the default polling interval.
		/// 
		/// </summary>
		/// <param name="file">the file to tail
		/// </param>
		public Tailer(System.IO.FileInfo file):this(file, DEFAULT_POLL_INTERVAL)
		{
		}
		
		/// <summary> Creates a file tailer.
		/// 
		/// </summary>
		/// <param name="file">the file to tail
		/// </param>
		/// <param name="poll_interval">the polling interval, in milliseconds
		/// </param>
		public Tailer(System.IO.FileInfo file, long poll_interval)
		{
			InitBlock();
			if (file == null)
				throw new System.ArgumentException("file == null");
			
			this.file = file;
			this.poll_interval = poll_interval;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			this.lsup = new DefaultEventListenerSupport < String >(this);
		}
		
		/// <summary> Creates a file tailer.
		/// 
		/// </summary>
		/// <param name="file">the file to tail
		/// </param>
		/// <param name="poll_interval">the polling interval, in milliseconds
		/// </param>
		/// <param name="lsup">the listener support
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Tailer(File file, long poll_interval, 
		EventListenerSupport < String > lsup)
		
		/// <summary> Starts tailing the file.</summary>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'start'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  start()
		{
			lock (this)
			{
				// NB: This method is synchronized to ensure that there is never more
				// than one tailer thread at at time.
				if (!tailing)
				{
					bool tmpBool;
					if (System.IO.File.Exists(file.FullName))
						tmpBool = true;
					else
						tmpBool = System.IO.Directory.Exists(file.FullName);
					if (!tmpBool)
					{
						throw new System.IO.IOException(file.FullName + " does not exist");
					}
					
					if (System.IO.Directory.Exists(file.FullName))
					{
						throw new System.IO.IOException(file.FullName + " is a directory");
					}
					
					tailing = true;
					new SupportClass.ThreadClass(new System.Threading.ThreadStart(new Monitor(this).Run), "tailing " + file.FullName).Start();
				}
			}
		}
		
		/// <summary> Stops tailing the file.</summary>
		public virtual void  stop()
		{
			tailing = false;
		}
		
		/// <summary> Adds an {@link EventListener}.
		/// 
		/// </summary>
		/// <param name="l">the listener to add
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void addEventListener(EventListener < ? super String > l)
		
		/// <summary> Removes an {@link EventListener}.
		/// 
		/// </summary>
		/// <param name="l">the listener to remove
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void removeEventListener(EventListener < ? super String > l)
		
		/// <summary> Checks whether there are any {@link EventListener}s.
		/// 
		/// </summary>
		/// <returns> <code>true</code> if there are any listeners
		/// </returns>
		public virtual bool hasEventListeners()
		{
			return lsup.hasEventListeners();
		}
		
		/// <summary> Gets the list of listerners.
		/// 
		/// </summary>
		/// <returns> the list of listeners
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < EventListener < ? super String >> getEventListeners()
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Monitor' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class Monitor : IThreadRunnable
		{
			public Monitor(Tailer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Tailer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Tailer enclosingInstance;
			public Tailer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				
				//UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
				System.IO.FileStream raf = null;
				try
				{
					raf = SupportClass.RandomAccessFileSupport.CreateRandomAccessFile(Enclosing_Instance.file, "r");
					
					// read until we're told to stop
					while (Enclosing_Instance.tailing)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'length '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						long length = raf.Length;
						
						if (length < Enclosing_Instance.position)
						{
							// file has been truncated, reopen it
							raf = SupportClass.RandomAccessFileSupport.CreateRandomAccessFile(Enclosing_Instance.file, "r");
							Enclosing_Instance.position = 0L;
						}
						else if (length > Enclosing_Instance.position)
						{
							// new lines have been written, read them
							raf.Seek(Enclosing_Instance.position, System.IO.SeekOrigin.Begin);
							
							System.String line;
							//UPGRADE_ISSUE: Method 'java.io.RandomAccessFile.readLine' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioRandomAccessFilereadLine'"
							while ((line = raf.readLine()) != null)
							{
								// readLine strips newlines, we put them back
								lsup.notify(line + "\n");
							}
							
							Enclosing_Instance.position = raf.Position;
						}
						
						// we have reached EOF, sleep
						//UPGRADE_TODO: Method 'java.lang.Thread.sleep' was converted to 'System.Threading.Thread.Sleep' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangThreadsleep_long'"
						System.Threading.Thread.Sleep(new System.TimeSpan((System.Int64) 10000 * Enclosing_Instance.poll_interval));
					}
				}
				catch (System.IO.IOException e)
				{
					// FIXME: there should be an error listener; we can't handle exceptions here
					VassalSharp.tools.io.Tailer.logger.error("", e);
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
					VassalSharp.tools.io.Tailer.logger.error("", e);
				}
				finally
				{
					IOUtils.closeQuietly(raf);
				}
			}
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Tailer t = new Tailer(new System.IO.FileInfo(args[0]));
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			t.addEventListener(new EventListener < String >()
			{
			}
		}
		public virtual void  receive(System.Object src, System.String s)
		{
			System.Console.Out.Write(s);
		}
		static Tailer()
		{
			logger = LoggerFactory.getLogger(typeof(Tailer));
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	t.start();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}