/*
* $Id$
*
* Copyright (c) 2008 by Joel Uckelman
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
namespace VassalSharp.launch
{
	
	/// <summary> A socket client for communication between the {@link ModuleManager}
	/// and its children {@link Player} and {@link Editor} processes. Requests
	/// are sent from, and replies recieved by this class.
	/// 
	/// </summary>
	/// <seealso cref="CommandServer">
	/// </seealso>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	/// <deprecated> Use {@link ObjectOutputStream} with {@link SerializableSingal}
	/// instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class CommandClient
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'in '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'java.io.ObjectInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectInputStream'"
		private System.IO.BinaryReader in_Renamed;
		//UPGRADE_NOTE: Final was removed from the declaration of 'out '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'java.io.ObjectOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectOutputStream'"
		private System.IO.BinaryWriter out_Renamed;
		
		/// <summary> Create a new <code>CommandClient</code>.
		/// 
		/// </summary>
		/// <param name="clientSocket">the socket on which to communicate
		/// </param>
		/// <throws>  IOException if something goes wrong with the socket </throws>
		public CommandClient(System.Net.Sockets.TcpClient clientSocket)
		{
			//UPGRADE_TODO: Class 'java.io.ObjectOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectOutputStream'"
			out_Renamed = new System.IO.BinaryWriter(clientSocket.GetStream());
			//UPGRADE_TODO: Class 'java.io.ObjectInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectInputStream'"
			in_Renamed = new System.IO.BinaryReader(clientSocket.GetStream());
		}
		
		/// <summary> Send a request to the socket listener and recieve a reply.
		/// This method is synchronized to ensure that only one thread
		/// sends a requests over the socket at a time.
		/// 
		/// </summary>
		/// <param name="cmd">the command to send to the socket listener
		/// </param>
		/// <returns> the reply object from the socket listener
		/// </returns>
		/// <throws>  IOException if something goes wrong with the socket </throws>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'request'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual System.Object request(System.Object cmd)
		{
			lock (this)
			{
				//UPGRADE_TODO: Method 'java.io.ObjectOutputStream.writeObject' was converted to 'SupportClass.Serialize' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectOutputStreamwriteObject_javalangObject'"
				SupportClass.Serialize(out_Renamed, cmd);
				try
				{
					//UPGRADE_WARNING: Method 'java.io.ObjectInputStream.readObject' was converted to 'SupportClass.Deserialize' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
					return SupportClass.Deserialize(in_Renamed);
				}
				//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception e)
				{
					ErrorDialog.bug(e);
					return null;
				}
			}
		}
	}
}