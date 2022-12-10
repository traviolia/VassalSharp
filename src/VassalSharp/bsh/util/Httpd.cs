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
namespace bsh.util
{
	
	/// <summary>A very simple httpd that supports the remote server mode.
	/// Files are loaded relative to the classpath (as resources).
	/// Warning: this is not secure!  This server can probably be duped into 
	/// serving any file on your system!  Beware!
	/// Note: at some point this should be recast as a beanshell script.
	/// </summary>
	public class Httpd:SupportClass.ThreadClass
	{
		internal System.Net.Sockets.TcpListener ss;
		
		[STAThread]
		public static void  Main(System.String[] argv)
		{
			new Httpd(System.Int32.Parse(argv[0])).Start();
		}
		
		public Httpd(int port)
		{
			System.Net.Sockets.TcpListener temp_tcpListener;
			temp_tcpListener = new System.Net.Sockets.TcpListener(System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0], port);
			temp_tcpListener.Start();
			ss = temp_tcpListener;
		}
		
		override public void  Run()
		{
			//		System.out.println("starting httpd...");
			try
			{
				while (true)
					new HttpdConnection(ss.AcceptTcpClient()).Start();
			}
			catch (System.IO.IOException e)
			{
				//UPGRADE_TODO: Method 'java.io.PrintStream.println' was converted to 'System.Console.Out.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintStreamprintln_javalangObject'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Out.WriteLine(e);
			}
		}
	}
	
	class HttpdConnection:SupportClass.ThreadClass
	{
		internal System.Net.Sockets.TcpClient client;
		internal System.IO.StreamReader in_Renamed;
		internal System.IO.Stream out_Renamed;
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		internal System.IO.StreamWriter pout;
		internal bool isHttp1;
		
		internal HttpdConnection(System.Net.Sockets.TcpClient client)
		{
			this.client = client;
			//UPGRADE_TODO: The differences in the type  of parameters for method 'java.lang.Thread.setPriority'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
			Priority = (System.Threading.ThreadPriority) ((int) System.Threading.ThreadPriority.Normal - 1);
		}
		
		override public void  Run()
		{
			try
			{
				//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				in_Renamed = new System.IO.StreamReader(new System.IO.StreamReader(client.GetStream(), System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(client.GetStream(), System.Text.Encoding.Default).CurrentEncoding);
				out_Renamed = client.GetStream();
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				pout = new System.IO.StreamWriter(out_Renamed);
				
				System.String request = in_Renamed.ReadLine();
				if (request == null)
					error(400, "Empty Request");
				
				if (request.ToLower().IndexOf("http/1.") != - 1)
				{
					System.String s;
					while ((!(s = in_Renamed.ReadLine()).Equals("")) && (s != null))
					{
						;
					}
					
					isHttp1 = true;
				}
				
				SupportClass.Tokenizer st = new SupportClass.Tokenizer(request);
				if (st.Count < 2)
					error(400, "Bad Request");
				else
				{
					System.String command = st.NextToken();
					if (command.Equals("GET"))
						serveFile(st.NextToken());
					else
						error(400, "Bad Request");
				}
				
				client.Close();
			}
			catch (System.IO.IOException e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Out.WriteLine("I/O error " + e);
				try
				{
					client.Close();
				}
				catch (System.Exception e2)
				{
				}
			}
		}
		
		private void  serveFile(System.String file)
		{
			// Do some mappings
			if (file.Equals("/"))
				file = "/remote/remote.html";
			
			if (file.StartsWith("/remote/"))
				file = "/bsh/util/lib/" + file.Substring(8);
			
			/*
			if(file.startsWith("/"))
			file = file.substring(1);
			if(file.endsWith("/") || file.equals(""))
			file = file + "index.html";
			
			if(!fileAccessOK(file))
			{
			error(403, "Forbidden");
			return;
			}
			*/
			
			// don't send java packages over... (e.g. swing)
			if (file.StartsWith("/java"))
				error(404, "Object Not Found");
			else
				try
				{
					System.Console.Out.WriteLine("sending file: " + file);
					sendFileData(file);
				}
				catch (System.IO.FileNotFoundException e)
				{
					error(404, "Object Not Found");
				}
		}
		
		private void  sendFileData(System.String file)
		{
			/*
			Why aren't resources being found when this runs on Win95?
			*/
			//UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
			System.IO.Stream fis = GetType().getResourceAsStream(file);
			if (fis == null)
				throw new System.IO.FileNotFoundException(file);
			long available;
			available = fis.Length - fis.Position;
			sbyte[] data = new sbyte[(fis is VassalSharp.tools.io.RereadableInputStream || fis is VassalSharp.tools.io.CompositeInputStream || fis is VassalSharp.tools.image.PNGChunkSkipInputStream)?(int) SupportClass.InvokeMethodAsVirtual(fis, "available", new System.Object[]{}):(int) available];
			
			if (isHttp1)
			{
				pout.WriteLine("HTTP/1.0 200 Document follows");
				
				pout.WriteLine("Content-length: " + data.Length);
				
				if (file.EndsWith(".gif"))
					pout.WriteLine("Content-type: image/gif");
				else if (file.EndsWith(".html") || file.EndsWith(".htm"))
					pout.WriteLine("Content-Type: text/html");
				else
					pout.WriteLine("Content-Type: application/octet-stream");
				
				pout.WriteLine();
			}
			
			int bytesread = 0;
			// Never, ever trust available()
			do 
			{
				bytesread = fis is VassalSharp.tools.image.PNGChunkSkipInputStream?((VassalSharp.tools.image.PNGChunkSkipInputStream) fis).read(data):SupportClass.ReadInput(fis, data, 0, data.Length);
				if (bytesread > 0)
					pout.Write(SupportClass.ToCharArray(data), 0, bytesread);
			}
			while (bytesread != - 1);
			pout.Flush();
		}
		
		private void  error(int num, System.String s)
		{
			s = "<html><h1>" + s + "</h1></html>";
			if (isHttp1)
			{
				pout.WriteLine("HTTP/1.0 " + num + " " + s);
				pout.WriteLine("Content-type: text/html");
				pout.WriteLine("Content-length: " + s.Length + "\n");
			}
			
			pout.WriteLine(s);
		}
	}
}