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
using ChatServerConnection = VassalSharp.chat.ChatServerConnection;
using DynamicClient = VassalSharp.chat.DynamicClient;
using HttpRequestWrapper = VassalSharp.chat.HttpRequestWrapper;
using ServerStatus = VassalSharp.chat.ServerStatus;
using WelcomeMessageServer = VassalSharp.chat.WelcomeMessageServer;
using Message = VassalSharp.chat.messageboard.Message;
using MessageBoard = VassalSharp.chat.messageboard.MessageBoard;
using DirectPeerPool = VassalSharp.chat.peer2peer.DirectPeerPool;
using P2PClient = VassalSharp.chat.peer2peer.P2PClient;
using PeerPoolInfo = VassalSharp.chat.peer2peer.PeerPoolInfo;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.node
{
	
	/// <summary> Copyright (c) 2003 by Rodney Kinney.  All rights reserved.
	/// Date: May 29, 2003
	/// </summary>
	public class NodeHybridClient:DynamicClient
	{
		virtual public Message[] Messages
		{
			get
			{
				return msgSvr.Messages;
			}
			
		}
		virtual public MessageBoard MessageServer
		{
			get
			{
				return msgSvr;
			}
			
		}
		private System.String AddressFromURL
		{
			get
			{
				HttpRequestWrapper r = new HttpRequestWrapper(addressURL);
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
				System.Collections.Specialized.NameValueCollection p = new System.Collections.Specialized.NameValueCollection();
				p[(System.String) "module"] = (System.String) info.ModuleName; //$NON-NLS-1$
				p[(System.String) "vassalVersion"] = (System.String) VassalSharp.Info.Version; //$NON-NLS-1$
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				if (l.isEmpty())
				{
					throw new System.IO.IOException(Resources.getString("Server.empty_response")); //$NON-NLS-1$
				}
				return l.get_Renamed(0);
			}
			
		}
		virtual public ServerStatus StatusServer
		{
			get
			{
				return status;
			}
			
			set
			{
				status = value;
			}
			
		}
		private System.String addressURL;
		private MessageBoard msgSvr;
		private WelcomeMessageServer welcomeMsgSvr;
		private PeerPoolInfo info;
		private CommandEncoder encoder;
		private ServerStatus status;
		
		public NodeHybridClient(CommandEncoder encoder, PeerPoolInfo info, MessageBoard msgSvr, WelcomeMessageServer welcomeMsgSvr, System.String addressURL)
		{
			this.addressURL = addressURL;
			this.encoder = encoder;
			this.info = info;
			this.msgSvr = msgSvr;
			this.welcomeMsgSvr = welcomeMsgSvr;
		}
		
		public virtual void  postMessage(System.String msg)
		{
			msgSvr.postMessage(msg);
		}
		
		protected internal override ChatServerConnection buildDelegate()
		{
			ChatServerConnection c = null;
			try
			{
				System.String address = AddressFromURL;
				int index = address.IndexOf(':'); //$NON-NLS-1$
				if (index < 0)
				{
					fireStatus(Resources.getString("Server.bad_address", address)); //$NON-NLS-1$
				}
				else
				{
					try
					{
						int port = System.Int32.Parse(address.Substring(index + 1));
						address = address.Substring(0, (index) - (0));
						c = new SocketNodeClient(info.ModuleName, info.UserName, encoder, address, port, msgSvr, welcomeMsgSvr);
					}
					// FIXME: review error message
					catch (System.FormatException ex)
					{
						fireStatus(Resources.getString("Server.bad_address2", address)); //$NON-NLS-1$
					}
				}
			}
			// FIXME: review error message
			catch (System.IO.IOException e)
			{
				fireStatus(Resources.getString("Server.bad_address3")); //$NON-NLS-1$
			}
			if (c == null)
			{
				fireStatus(Resources.getString("Server.peer_to_peer")); //$NON-NLS-1$
				c = new P2PClient(encoder, msgSvr, welcomeMsgSvr, new DirectPeerPool());
			}
			return c;
		}
	}
}