/*
* $Id:
*
* Copyright (c) 2000-2013 by Rodney Kinney, Brent Easton
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
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using GameModule = VassalSharp.build.GameModule;
using ChatServerConnection = VassalSharp.chat.ChatServerConnection;
using ChatServerFactory = VassalSharp.chat.ChatServerFactory;
using CommandDecoder = VassalSharp.chat.CommandDecoder;
using HttpMessageServer = VassalSharp.chat.HttpMessageServer;
using Message = VassalSharp.chat.messageboard.Message;
using Command = VassalSharp.command.Command;
using NullCommand = VassalSharp.command.NullCommand;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.peer2peer
{
	
	/// <author>  rkinney
	/// </author>
	public class P2PClientFactory:ChatServerFactory
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(P2PClientFactory enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(P2PClientFactory enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private P2PClientFactory enclosingInstance;
			public P2PClientFactory Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'mess '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String mess = (System.String) evt.NewValue;
				GameModule.getGameModule().warn(mess);
				VassalSharp.chat.peer2peer.P2PClientFactory.logger.info(mess);
			}
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.chat.peer2peer.P2PClientFactory'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		public const System.String P2P_TYPE = "peer2peer"; //$NON-NLS-1$
		public const System.String P2P_LISTEN_PORT = "listenPort"; //$NON-NLS-1$
		public const System.String P2P_MODE_KEY = "mode"; //$NON-NLS-1$
		public const System.String P2P_SERVER_MODE = "server"; //$NON-NLS-1$
		public const System.String P2P_CLIENT_MODE = "client"; //$NON-NLS-1$
		public const System.String P2P_SERVER_IP = "serverIp"; //$NON-NLS-1$
		public const System.String P2P_SERVER_PORT = "serverPort"; //$NON-NLS-1$
		public const System.String P2P_SERVER_NAME = "serverName"; //$NON-NLS-1$
		public const System.String P2P_SERVER_PW = "serverPw"; //$NON-NLS-1$
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public override ChatServerConnection buildServer(System.Collections.Specialized.NameValueCollection param)
		{
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'httpMessageServer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			HttpMessageServer httpMessageServer = new P2PMessageServer(this);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'server '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			P2PClient server = new P2PClient(GameModule.getGameModule(), httpMessageServer, httpMessageServer, new DirectPeerPool(param), param);
			server.addPropertyChangeListener(VassalSharp.chat.ChatServerConnection_Fields.STATUS, new AnonymousClassPropertyChangeListener(this));
			server.addPropertyChangeListener(VassalSharp.chat.ChatServerConnection_Fields.INCOMING_MSG, new CommandDecoder());
			return server;
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'P2PMessageServer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		internal class P2PMessageServer:HttpMessageServer
		{
			private class AnonymousClassPeerPoolInfo : PeerPoolInfo
			{
				virtual public System.String ModuleName
				{
					get
					{
						return GameModule.getGameModule() == null?Resources.getString("Chat.unknown_module"):GameModule.getGameModule().getGameName(); //$NON-NLS-1$
					}
					
				}
				virtual public System.String UserName
				{
					get
					{
						return GameModule.getUserId();
					}
					
				}
			}
			private void  InitBlock(P2PClientFactory enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private P2PClientFactory enclosingInstance;
			override public Command WelcomeMessage
			{
				get
				{
					return new NullCommand();
				}
				
			}
			override public Message[] Messages
			{
				get
				{
					return null;
				}
				
			}
			public P2PClientFactory Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			public P2PMessageServer(P2PClientFactory enclosingInstance):base(new AnonymousClassPeerPoolInfo())
			{
				InitBlock(enclosingInstance);
			}
			
			public override void  postMessage(System.String content)
			{
				return ;
			}
		}
		static P2PClientFactory()
		{
			logger = LoggerFactory.getLogger(typeof(ChatServerFactory));
		}
	}
}