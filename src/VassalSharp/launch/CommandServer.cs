/*
* $Id$
*
* Copyright (c) 2008-2009 by Joel Uckelman
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
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ThrowableUtils = VassalSharp.tools.ThrowableUtils;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.launch
{
	
	/// <summary> The base class for socket servers for communication between the
	/// {@link ModuleManager} and its children {@link Player} and {@link Editor}
	/// processes. Concrete extensions will implement {@link #execute(Object)} to
	/// process incoming requests.
	/// 
	/// </summary>
	/// <seealso cref="CommandClient">
	/// </seealso>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	/// <deprecated> Use {@link SignalDispatcher} instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class CommandServer : IThreadRunnable
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'serverSocket '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Net.Sockets.TcpListener serverSocket;
		
		public CommandServer(System.Net.Sockets.TcpListener serverSocket)
		{
			this.serverSocket = serverSocket;
		}
		
		/// <summary> Initialize the {@link Command}. Subclasses handling commands
		/// which need access to some local state will override this method.
		/// </summary>
		public virtual void  init(Command command)
		{
		}
		
		public virtual void  Run()
		{
			System.Net.Sockets.TcpClient clientSocket = null;
			//UPGRADE_TODO: Class 'java.io.ObjectOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectOutputStream'"
			System.IO.BinaryWriter out_Renamed = null;
			//UPGRADE_TODO: Class 'java.io.ObjectInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectInputStream'"
			System.IO.BinaryReader in_Renamed = null;
			try
			{
				clientSocket = serverSocket.AcceptTcpClient();
				
				//UPGRADE_TODO: Class 'java.io.ObjectOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectOutputStream'"
				out_Renamed = new System.IO.BinaryWriter(clientSocket.GetStream());
				//UPGRADE_TODO: Class 'java.io.ObjectInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectInputStream'"
				in_Renamed = new System.IO.BinaryReader(clientSocket.GetStream());
				
				System.Object obj;
				System.Object result;
				try
				{
					//UPGRADE_WARNING: Method 'java.io.ObjectInputStream.readObject' was converted to 'SupportClass.Deserialize' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
					while ((obj = SupportClass.Deserialize(in_Renamed)) != null)
					{
						// Execute commands as they come and send back the reply
						if (obj is Command)
						{
							//UPGRADE_NOTE: Final was removed from the declaration of 'command '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							Command command = (Command) obj;
							init(command);
							result = command.execute();
						}
						else
						{
							result = "UNRECOGNIZED_COMMAND";
						}
						
						//UPGRADE_TODO: Method 'java.io.ObjectOutputStream.writeObject' was converted to 'SupportClass.Serialize' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectOutputStreamwriteObject_javalangObject'"
						SupportClass.Serialize(out_Renamed, result);
					}
				}
				catch (System.IO.EndOfStreamException e)
				{
					// Normal. This happens when the socket is closed from the other end.
				}
				catch (System.Net.Sockets.SocketException e)
				{
					// Normal. This happens when the socket is closed from the other end.
				}
				
				clientSocket.Close();
				serverSocket.Stop();
			}
			//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception e)
			{
				ErrorDialog.bug(e);
			}
			catch (System.IO.IOException e)
			{
				ErrorDialog.showDetails(e, ThrowableUtils.getStackTrace(e), "Error.socket_error");
			}
			finally
			{
				IOUtils.closeQuietly(clientSocket);
				IOUtils.closeQuietly(serverSocket);
			}
		}
	}
}