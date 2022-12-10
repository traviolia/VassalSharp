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
using CgiServerStatus = VassalSharp.chat.CgiServerStatus;
using WelcomeMessageServer = VassalSharp.chat.WelcomeMessageServer;
using MessageBoard = VassalSharp.chat.messageboard.MessageBoard;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.node
{
	
	public class SocketNodeClient:NodeClient, SocketWatcher
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassNodeServerInfo' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassNodeServerInfo : NodeServerInfo
		{
			public AnonymousClassNodeServerInfo(System.String host, int port, SocketNodeClient enclosingInstance)
			{
				InitBlock(host, port, enclosingInstance);
			}
			private void  InitBlock(System.String host, int port, SocketNodeClient enclosingInstance)
			{
				this.host = host;
				this.port = port;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable host was copied into class AnonymousClassNodeServerInfo. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.String host;
			//UPGRADE_NOTE: Final variable port was copied into class AnonymousClassNodeServerInfo. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private int port;
			private SocketNodeClient enclosingInstance;
			virtual public System.String HostName
			{
				get
				{
					return host;
				}
				
			}
			virtual public int Port
			{
				get
				{
					return port;
				}
				
			}
			public SocketNodeClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
		}
		private SocketHandler sender;
		protected internal NodeServerInfo serverInfo;
		
		public SocketNodeClient(System.String moduleName, System.String playerId, CommandEncoder encoder, NodeServerInfo serverInfo, MessageBoard msgSvr, WelcomeMessageServer welcomer):base(moduleName, playerId, encoder, msgSvr, welcomer)
		{
			this.serverInfo = serverInfo;
			serverStatus = new CgiServerStatus();
		}
		
		public SocketNodeClient(System.String moduleName, System.String playerId, CommandEncoder encoder, System.String host, int port, MessageBoard msgSvr, WelcomeMessageServer welcomer):this(moduleName, playerId, encoder, new AnonymousClassNodeServerInfo(host, port, this), msgSvr, welcomer)
		{
		}
		
		public override void  send(System.String command)
		{
			sender.writeLine(command);
		}
		
		protected internal override void  initializeConnection()
		{
			System.Net.Sockets.TcpClient s = new System.Net.Sockets.TcpClient(serverInfo.HostName, serverInfo.Port);
			sender = new BufferedSocketHandler(s, this);
			sender.start();
		}
		
		protected internal override void  closeConnection()
		{
			SocketHandler s = sender;
			sender = null;
			s.close();
		}
		
		public override bool isConnected()
		{
			return sender != null;
		}
		
		public virtual void  socketClosed(SocketHandler handler)
		{
			if (sender != null)
			{
				SupportClass.PropertyChangingEventArgs me17 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.STATUS, null, Resources.getString("Server.lost_connection"));
				if (PropertyChange != null)
					PropertyChange(this, me17); //$NON-NLS-1$
				SupportClass.PropertyChangingEventArgs me18 = new SupportClass.PropertyChangingEventArgs(VassalSharp.build.module.ServerConnection_Fields.CONNECTED, null, false);
				if (PropertyChange != null)
					PropertyChange(this, me18);
				sender = null;
			}
		}
		
		public virtual void  handleMessage(System.String msg)
		{
			handleMessageFromServer(msg);
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual void  updateConfig(System.Collections.Specialized.NameValueCollection params_Renamed)
		{
			// No Configurable parameters
		}
	}
}