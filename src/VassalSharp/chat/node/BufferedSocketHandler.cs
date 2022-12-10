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
//UPGRADE_TODO: The type 'java.nio.charset.Charset' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Charset = java.nio.charset.Charset;
namespace VassalSharp.chat.node
{
	
	/// <summary> Copyright (c) 2003 by Rodney Kinney.  All rights reserved.
	/// Date: Aug 27, 2003
	/// </summary>
	public class BufferedSocketHandler:SocketHandler
	{
		protected internal System.IO.StreamReader reader;
		protected internal System.IO.StreamWriter writer;
		
		public BufferedSocketHandler(System.Net.Sockets.TcpClient sock, SocketWatcher handler):base(sock, handler)
		{
			//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
			//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
			reader = new System.IO.StreamReader(new InputStreamReader(sock.GetStream(), Charset.forName("UTF-8")).BaseStream, new InputStreamReader(sock.GetStream(), Charset.forName("UTF-8")).CurrentEncoding); //$NON-NLS-1$
			//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
			writer = new System.IO.StreamWriter(new OutputStreamWriter(sock.GetStream(), Charset.forName("UTF-8")).BaseStream, new OutputStreamWriter(sock.GetStream(), Charset.forName("UTF-8")).Encoding); //$NON-NLS-1$
		}
		
		protected internal override void  closeStreams()
		{
			writer.Close();
			reader.Close();
		}
		
		protected internal override System.String readNext()
		{
			return reader.ReadLine();
		}
		
		protected internal override void  writeNext(System.String line)
		{
			writer.Write(line + '\n');
			writer.Flush();
		}
	}
}