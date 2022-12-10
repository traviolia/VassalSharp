/*
*
* Copyright (c) 2000-2009 by Rodney Kinney, Brent Easton
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
using PeerPoolInfo = VassalSharp.chat.peer2peer.PeerPoolInfo;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.node
{
	
	/// <author>  rkinney
	/// </author>
	public class NodeClientFactory:ChatServerFactory
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassNodeServerInfo' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassNodeServerInfo : NodeServerInfo
		{
			public AnonymousClassNodeServerInfo(System.String host, int port, NodeClientFactory enclosingInstance)
			{
				InitBlock(host, port, enclosingInstance);
			}
			private void  InitBlock(System.String host, int port, NodeClientFactory enclosingInstance)
			{
				this.host = host;
				this.port = port;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable host was copied into class AnonymousClassNodeServerInfo. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.String host;
			//UPGRADE_NOTE: Final variable port was copied into class AnonymousClassNodeServerInfo. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private int port;
			private NodeClientFactory enclosingInstance;
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
			public NodeClientFactory Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPeerPoolInfo' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPeerPoolInfo : PeerPoolInfo
		{
			public AnonymousClassPeerPoolInfo(NodeClientFactory enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(NodeClientFactory enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private NodeClientFactory enclosingInstance;
			virtual public System.String ModuleName
			{
				get
				{
					return GameModule.getGameModule() == null?VassalSharp.chat.node.NodeClientFactory.UNNAMED_MODULE:GameModule.getGameModule().getGameName();
				}
				
			}
			virtual public System.String UserName
			{
				get
				{
					return GameModule.getGameModule() == null?VassalSharp.chat.node.NodeClientFactory.UNKNOWN_USER:(System.String) GameModule.getGameModule().getPrefs().getValue(GameModule.REAL_NAME);
				}
				
			}
			public NodeClientFactory Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(NodeClientFactory enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(NodeClientFactory enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private NodeClientFactory enclosingInstance;
			public NodeClientFactory Enclosing_Instance
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
				VassalSharp.chat.node.NodeClientFactory.logger.error("", mess);
			}
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.chat.node.NodeClientFactory'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'UNNAMED_MODULE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'UNNAMED_MODULE' was moved to static method 'VassalSharp.chat.node.NodeClientFactory'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly System.String UNNAMED_MODULE; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'UNKNOWN_USER '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'UNKNOWN_USER' was moved to static method 'VassalSharp.chat.node.NodeClientFactory'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly System.String UNKNOWN_USER; //$NON-NLS-1$
		public const System.String NODE_TYPE = "node"; //$NON-NLS-1$
		public const System.String NODE_HOST = "nodeHost"; //$NON-NLS-1$
		public const System.String NODE_PORT = "nodePort"; //$NON-NLS-1$
		
		public NodeClientFactory()
		{
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public override ChatServerConnection buildServer(System.Collections.Specialized.NameValueCollection param)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'host '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String host = param[NODE_HOST] == null?"game.vassalengine.org":param[NODE_HOST]; //$NON-NLS-1$
			//UPGRADE_NOTE: Final was removed from the declaration of 'port '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int port = System.Int32.Parse(param[NODE_PORT] == null?"5050":param[NODE_PORT]); //$NON-NLS-1$
			NodeServerInfo nodeServerInfo = new AnonymousClassNodeServerInfo(host, port, this);
			PeerPoolInfo publicInfo = new AnonymousClassPeerPoolInfo(this);
			HttpMessageServer httpMessageServer = new HttpMessageServer(publicInfo);
			SocketNodeClient server = new SocketNodeClient(GameModule.getGameModule().getGameName(), GameModule.getUserId() + "." + (System.DateTime.Now.Ticks - 621355968000000000) / 10000, GameModule.getGameModule(), nodeServerInfo, httpMessageServer, httpMessageServer); //$NON-NLS-1$
			GameModule.getGameModule().getPrefs().getOption(GameModule.REAL_NAME).fireUpdate();
			GameModule.getGameModule().getPrefs().getOption(GameModule.PERSONAL_INFO).fireUpdate();
			server.addPropertyChangeListener(VassalSharp.chat.ChatServerConnection_Fields.STATUS, new AnonymousClassPropertyChangeListener(this));
			server.addPropertyChangeListener(VassalSharp.chat.ChatServerConnection_Fields.INCOMING_MSG, new CommandDecoder());
			return server;
		}
		static NodeClientFactory()
		{
			logger = LoggerFactory.getLogger(typeof(NodeClientFactory));
			UNNAMED_MODULE = Resources.getString("Chat.unknown_module");
			UNKNOWN_USER = Resources.getString("Chat.unknown_user");
		}
	}
}