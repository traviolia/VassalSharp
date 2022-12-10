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
/*
* Copyright (c) 2003 by Rodney Kinney.  All rights reserved.
* Date: Jun 4, 2003
*/
using System;
using ArgsParser = VassalSharp.tools.ArgsParser;
namespace VassalSharp.chat.node
{
	
	/// <summary> The server-side Main class</summary>
	public class Server:SupportClass.ThreadClass
	{
		private class AnonymousClassSocketWatcher : SocketWatcher
		{
			public virtual void  handleMessage(System.String msg)
			{
				System.Console.Error.WriteLine(msg);
			}
			
			public virtual void  socketClosed(SocketHandler handler)
			{
			}
		}
		private class AnonymousClassSocketWatcher1 : SocketWatcher
		{
			public virtual void  handleMessage(System.String msg)
			{
				System.Console.Error.WriteLine(msg);
			}
			
			public virtual void  socketClosed(SocketHandler handler)
			{
			}
		}
		private AsynchronousServerNode rootNode;
		private System.Net.Sockets.TcpListener socket;
		
		public Server(AsynchronousServerNode rootNode, int port)
		{
			this.rootNode = rootNode;
			System.Net.Sockets.TcpListener temp_tcpListener;
			temp_tcpListener = new System.Net.Sockets.TcpListener(System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0], port);
			temp_tcpListener.Start();
			socket = temp_tcpListener;
			System.Console.Error.WriteLine("Started server on port " + port); //$NON-NLS-1$
			Start();
		}
		
		override public void  Run()
		{
			int consecutiveFailures = 0;
			while (consecutiveFailures < 10)
			{
				try
				{
					System.Net.Sockets.TcpClient s = socket.AcceptTcpClient();
					new PlayerNode(s, rootNode);
					consecutiveFailures = 0;
				}
				// FIXME: review error message
				catch (System.Exception e)
				{
					if (e is bsh.TargetError)
						((bsh.TargetError) e).printStackTrace();
					else
						SupportClass.WriteStackTrace(e, Console.Error);
					consecutiveFailures++;
				}
			}
			System.Environment.Exit(1);
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Collections.Specialized.NameValueCollection p = new ArgsParser(args).Properties;
			
			int port = System.Int32.Parse(p["port"] == null?"5050":p["port"]); //$NON-NLS-1$ //$NON-NLS-2$
			System.String reportURL = p["URL"] == null?"http://www.vassalengine.org/util/":p["URL"]; //$NON-NLS-1$ //$NON-NLS-2$
			if ("null".Equals(reportURL))
			{
				//$NON-NLS-1$
				reportURL = null;
			}
			if (!"true".Equals(p.Get("test")))
			{
				//$NON-NLS-1$ //$NON-NLS-2$
				new Server(new AsynchronousServerNode(reportURL), port);
				new LockWatcher(1000L * 60 * 30, 1000L * 60, port).Start();
			}
			if (p.Get("test") != null)
			{
				//$NON-NLS-1$
				System.Net.Sockets.TcpClient soc = new System.Net.Sockets.TcpClient("localHost", port); //$NON-NLS-1$
				SocketHandler handler = new BufferedSocketHandler(soc, new AnonymousClassSocketWatcher());
				handler.start();
				handler.writeLine(Protocol.encodeRegisterCommand("rk", "test/Main Room", "")); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'reader '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				System.IO.StreamReader reader = new System.IO.StreamReader(new System.IO.StreamReader(System.Console.OpenStandardInput(), System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(System.Console.OpenStandardInput(), System.Text.Encoding.Default).CurrentEncoding);
				try
				{
					System.String line;
					while ((line = reader.ReadLine()) != null)
					{
						if (line.StartsWith("JOIN"))
						{
							//$NON-NLS-1$
							//UPGRADE_NOTE: Final was removed from the declaration of 'room '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							System.String room = line.Substring("JOIN".Length).Trim(); //$NON-NLS-1$
							handler.writeLine(Protocol.encodeJoinCommand("test/" + room)); //$NON-NLS-1$
						}
						else if (line.StartsWith("BYE"))
						{
							//$NON-NLS-1$
							handler.close();
						}
						else if (line.StartsWith("HELLO"))
						{
							//$NON-NLS-1$
							soc = new System.Net.Sockets.TcpClient("localHost", port); //$NON-NLS-1$
							handler = new BufferedSocketHandler(soc, new AnonymousClassSocketWatcher1());
							handler.start();
							handler.writeLine(Protocol.encodeRegisterCommand("rk", "test/Main Room", "")); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
						}
						else if (line.StartsWith("*"))
						{
							//$NON-NLS-1$
							int length = System.Int32.Parse(line.Substring(1));
							//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							StringBuilder buffer = new StringBuilder();
							for (int i = 0; i < length; ++i)
							{
								char c = (char) ('a' + i % 10);
								if (c == 'a')
								{
									c = 'A';
								}
								buffer.append(c);
							}
							System.String msg = Protocol.encodeForwardCommand("test/*", buffer.toString()); //$NON-NLS-1$
							handler.writeLine(msg);
						}
						else
						{
							handler.writeLine(line);
						}
					}
				}
				finally
				{
					try
					{
						reader.Close();
					}
					// FIXME: review error message
					catch (System.IO.IOException e)
					{
						SupportClass.WriteStackTrace(e, Console.Error);
					}
				}
			}
		}
	}
}