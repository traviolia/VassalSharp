/*
* $Id$
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
//UPGRADE_TODO: The type 'java.util.concurrent.BlockingQueue' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using BlockingQueue = java.util.concurrent.BlockingQueue;
//UPGRADE_TODO: The type 'java.util.concurrent.LinkedBlockingQueue' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LinkedBlockingQueue = java.util.concurrent.LinkedBlockingQueue;
namespace VassalSharp.chat.node
{
	
	/// <summary> Copyright (c) 2003 by Rodney Kinney.  All rights reserved.
	/// Date: Aug 27, 2003
	/// </summary>
	public abstract class SocketHandler
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(SocketHandler enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SocketHandler enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SocketHandler enclosingInstance;
			public SocketHandler Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				System.String line;
				try
				{
					while ((line = Enclosing_Instance.readNext()) != null)
					{
						if (VassalSharp.chat.node.SocketHandler.SIGN_OFF.Equals(line))
						{
							break;
						}
						else if (line.Length > 0)
						{
							try
							{
								Enclosing_Instance.handler.handleMessage(line);
							}
							// FIXME: review error message
							catch (System.Exception e)
							{
								// Handler threw an exception.  Keep reading.
								//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
								System.Console.Error.WriteLine("Caught " + e.GetType().FullName + " handling " + line); //$NON-NLS-1$ //$NON-NLS-2$
								if (e is bsh.TargetError)
									((bsh.TargetError) e).printStackTrace();
								else
									SupportClass.WriteStackTrace(e, Console.Error);
							}
						}
					}
				}
				// FIXME: review error message
				catch (System.IO.IOException ignore)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.String msg = ignore.GetType().FullName;
					msg = msg.Substring(msg.LastIndexOf('.') + 1);
					//          System.err.println("Caught " + msg + "(" + ignore.getMessage() + ") reading socket.");
				}
				Enclosing_Instance.closeSocket();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable1 : IThreadRunnable
		{
			public AnonymousClassRunnable1(SocketHandler enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SocketHandler enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SocketHandler enclosingInstance;
			public SocketHandler Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				System.String line;
				try
				{
					while (true)
					{
						if ((line = Enclosing_Instance.Line) != null)
						{
							Enclosing_Instance.writeNext(line);
							if (VassalSharp.chat.node.SocketHandler.SIGN_OFF.Equals(line))
								break;
						}
					}
				}
				// FIXME: review error message
				catch (System.IO.IOException ignore)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.String msg = ignore.GetType().FullName;
					msg = msg.Substring(msg.LastIndexOf('.') + 1);
					//          System.err.println("Caught " + msg + "(" + ignore.getMessage() + ") writing to socket.");
				}
				Enclosing_Instance.closeSocket();
			}
		}
		private System.String Line
		{
			get
			{
				try
				{
					return writeQueue.take();
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
				}
				
				return null;
			}
			
		}
		protected internal System.Net.Sockets.TcpClient sock;
		protected internal SocketWatcher handler;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private BlockingQueue < String > writeQueue = new LinkedBlockingQueue < String >();
		private bool isOpen = true;
		private SupportClass.ThreadClass readThread;
		private SupportClass.ThreadClass writeThread;
		private const System.String SIGN_OFF = "!BYE"; //$NON-NLS-1$
		
		public SocketHandler(System.Net.Sockets.TcpClient sock, SocketWatcher handler)
		{
			this.sock = sock;
			this.handler = handler;
		}
		
		public virtual void  start()
		{
			if (readThread == null)
			{
				readThread = startReadThread();
			}
			if (writeThread == null)
			{
				writeThread = startWriteThread();
			}
		}
		
		private SupportClass.ThreadClass startReadThread()
		{
			IThreadRunnable runnable = new AnonymousClassRunnable(this);
			//UPGRADE_ISSUE: Method 'java.net.Socket.getInetAddress' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetSocketgetInetAddress'"
			SupportClass.ThreadClass t = new SupportClass.ThreadClass(new System.Threading.ThreadStart(runnable.Run), "read " + sock.getInetAddress());
			t.Start();
			return t;
		}
		
		private SupportClass.ThreadClass startWriteThread()
		{
			IThreadRunnable runnable = new AnonymousClassRunnable1(this);
			//UPGRADE_ISSUE: Method 'java.net.Socket.getInetAddress' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetSocketgetInetAddress'"
			SupportClass.ThreadClass t = new SupportClass.ThreadClass(new System.Threading.ThreadStart(runnable.Run), "write " + sock.getInetAddress());
			t.Start();
			return t;
		}
		
		protected internal abstract void  closeStreams();
		
		protected internal abstract System.String readNext();
		
		protected internal abstract void  writeNext(System.String line);
		
		public virtual void  writeLine(System.String pMessage)
		{
			try
			{
				writeQueue.put(pMessage);
			}
			catch (System.Threading.ThreadInterruptedException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
			}
		}
		
		public virtual void  close()
		{
			writeLine(SIGN_OFF);
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'closeSocket'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		private void  closeSocket()
		{
			lock (this)
			{
				if (isOpen)
				{
					try
					{
						closeStreams();
					}
					// FIXME: review error message
					catch (System.IO.IOException ignore)
					{
					}
					try
					{
						sock.Close();
					}
					// FIXME: review error message
					catch (System.IO.IOException ignore)
					{
					}
					
					close();
					isOpen = false;
					handler.socketClosed(this);
				}
			}
		}
	}
}