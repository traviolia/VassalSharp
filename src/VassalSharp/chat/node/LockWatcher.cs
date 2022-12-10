/*
*
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
namespace VassalSharp.chat.node
{
	
	/// <summary> Watches for thread lock on a server.  Kills the runtime if unable to establish new connection
	/// Copyright (c) 2003 by Rodney Kinney.  All rights reserved.
	/// Date: Jul 20, 2003
	/// </summary>
	public class LockWatcher:SupportClass.ThreadClass
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassSocketWatcher' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassSocketWatcher : SocketWatcher
		{
			public AnonymousClassSocketWatcher(SupportClass.ThreadClass t, LockWatcher enclosingInstance)
			{
				InitBlock(t, enclosingInstance);
			}
			private void  InitBlock(SupportClass.ThreadClass t, LockWatcher enclosingInstance)
			{
				this.t = t;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable t was copied into class AnonymousClassSocketWatcher. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private SupportClass.ThreadClass t;
			private LockWatcher enclosingInstance;
			public LockWatcher Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  handleMessage(System.String msg)
			{
				t.Interrupt();
			}
			
			public virtual void  socketClosed(SocketHandler handler)
			{
				System.Console.Error.WriteLine("Server closed socket"); //$NON-NLS-1$
			}
		}
		private long delay;
		private long timeout;
		private int port;
		
		/// <summary> </summary>
		/// <param name="delay">Time in milliseconds between connection attempts
		/// </param>
		/// <param name="timeout">Wait time in milliseconds to establish a new connection before terminating
		/// </param>
		public LockWatcher(long delay, long timeout, int port)
		{
			this.delay = delay;
			this.timeout = timeout;
			this.port = port;
		}
		
		override public void  Run()
		{
			while (true)
			{
				try
				{
					//UPGRADE_TODO: Method 'java.lang.Thread.sleep' was converted to 'System.Threading.Thread.Sleep' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangThreadsleep_long'"
					System.Threading.Thread.Sleep(new System.TimeSpan((System.Int64) 10000 * delay));
					pingServer();
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
					break;
				}
			}
		}
		
		private void  pingServer()
		{
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Net.Sockets.TcpClient s = new System.Net.Sockets.TcpClient("localhost", port); //$NON-NLS-1$
				//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SupportClass.ThreadClass t = new SupportClass.ThreadClass(new System.Threading.ThreadStart(new Timeout(this).Run));
				SocketWatcher watcher = new AnonymousClassSocketWatcher(t, this);
				SocketHandler sender = new BufferedSocketHandler(s, watcher);
				sender.start();
				t.Start();
				sender.writeLine(Protocol.encodeRegisterCommand("pinger", "ping/Main", "")); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
				try
				{
					t.Join();
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
				}
				
				sender.close();
			}
			// FIXME: review error message
			catch (System.IO.IOException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Timeout' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class Timeout : IThreadRunnable
		{
			public Timeout(LockWatcher enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(LockWatcher enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private LockWatcher enclosingInstance;
			public LockWatcher Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				try
				{
					//UPGRADE_TODO: Method 'java.lang.Thread.sleep' was converted to 'System.Threading.Thread.Sleep' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangThreadsleep_long'"
					System.Threading.Thread.Sleep(new System.TimeSpan((System.Int64) 10000 * Enclosing_Instance.timeout));
					System.Console.Error.WriteLine("No response from server in " + (Enclosing_Instance.timeout / 1000.0) + " seconds.  Terminating process"); //$NON-NLS-1$ //$NON-NLS-2$
					System.Environment.Exit(0);
				}
				// FIXME: review error message
				catch (System.Threading.ThreadInterruptedException e)
				{
					System.Console.Error.WriteLine("Ping"); //$NON-NLS-1$
					// Interrupt means response received from server
				}
			}
		}
	}
}