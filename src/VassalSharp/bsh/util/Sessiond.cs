/// <summary>**************************************************************************
/// *
/// This file is part of the BeanShell Java Scripting distribution.          *
/// Documentation and updates may be found at http://www.beanshell.org/      *
/// *
/// Sun Public License Notice:                                               *
/// *
/// The contents of this file are subject to the Sun Public License Version  *
/// 1.0 (the "License"); you may not use this file except in compliance with *
/// the License. A copy of the License is available at http://www.sun.com    * 
/// *
/// The Original Code is BeanShell. The Initial Developer of the Original    *
/// Code is Pat Niemeyer. Portions created by Pat Niemeyer are Copyright     *
/// (C) 2000.  All Rights Reserved.                                          *
/// *
/// GNU Public License Notice:                                               *
/// *
/// Alternatively, the contents of this file may be used under the terms of  *
/// the GNU Lesser General Public License (the "LGPL"), in which case the    *
/// provisions of LGPL are applicable instead of those above. If you wish to *
/// allow use of your version of this file only under the  terms of the LGPL *
/// and not to allow others to use your version of this file under the SPL,  *
/// indicate your decision by deleting the provisions above and replace      *
/// them with the notice and other provisions required by the LGPL.  If you  *
/// do not delete the provisions above, a recipient may use your version of  *
/// this file under either the SPL or the LGPL.                              *
/// *
/// Patrick Niemeyer (pat@pat.net)                                           *
/// Author of Learning Java, O'Reilly & Associates                           *
/// http://www.pat.net/~pat/                                                 *
/// *
/// ***************************************************************************
/// </summary>
using System;
using bsh;
namespace bsh.util
{
	
	/// <summary>BeanShell remote session server.
	/// Starts instances of bsh for client connections.
	/// Note: the sessiond effectively maps all connections to the same interpreter
	/// (shared namespace).
	/// </summary>
	public class Sessiond:SupportClass.ThreadClass
	{
		private System.Net.Sockets.TcpListener ss;
		internal NameSpace globalNameSpace;
		
		/*
		public static void main(String argv[]) throws IOException
		{
		new Sessiond( Integer.parseInt(argv[0])).start();
		}
		*/
		
		public Sessiond(NameSpace globalNameSpace, int port)
		{
			System.Net.Sockets.TcpListener temp_tcpListener;
			temp_tcpListener = new System.Net.Sockets.TcpListener(System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0], port);
			temp_tcpListener.Start();
			ss = temp_tcpListener;
			this.globalNameSpace = globalNameSpace;
		}
		
		override public void  Run()
		{
			try
			{
				while (true)
					new SessiondConnection(globalNameSpace, ss.AcceptTcpClient()).Start();
			}
			catch (System.IO.IOException e)
			{
				//UPGRADE_TODO: Method 'java.io.PrintStream.println' was converted to 'System.Console.Out.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintStreamprintln_javalangObject'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Out.WriteLine(e);
			}
		}
	}
	
	class SessiondConnection:SupportClass.ThreadClass
	{
		internal NameSpace globalNameSpace;
		internal System.Net.Sockets.TcpClient client;
		
		internal SessiondConnection(NameSpace globalNameSpace, System.Net.Sockets.TcpClient client)
		{
			this.client = client;
			this.globalNameSpace = globalNameSpace;
		}
		
		override public void  Run()
		{
			try
			{
				System.IO.Stream in_Renamed = client.GetStream();
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				System.IO.StreamWriter out_Renamed = new System.IO.StreamWriter(client.GetStream());
				Interpreter i = new Interpreter(new System.IO.StreamReader(in_Renamed, System.Text.Encoding.Default), out_Renamed, out_Renamed, true, globalNameSpace);
				i.ExitOnEOF = false; // don't exit interp
				i.Run();
			}
			catch (System.IO.IOException e)
			{
				//UPGRADE_TODO: Method 'java.io.PrintStream.println' was converted to 'System.Console.Out.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintStreamprintln_javalangObject'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Out.WriteLine(e);
			}
		}
	}
}