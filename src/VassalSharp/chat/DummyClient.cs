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
using GameModule = VassalSharp.build.GameModule;
using Chatter = VassalSharp.build.module.Chatter;
using PeerPoolInfo = VassalSharp.chat.peer2peer.PeerPoolInfo;
using ChatControlsInitializer = VassalSharp.chat.ui.ChatControlsInitializer;
using ChatServerControls = VassalSharp.chat.ui.ChatServerControls;
using MessageBoardControlsInitializer = VassalSharp.chat.ui.MessageBoardControlsInitializer;
using ServerStatusControlsInitializer = VassalSharp.chat.ui.ServerStatusControlsInitializer;
using Command = VassalSharp.command.Command;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat
{
	
	/// <summary> Empty server</summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class DummyClient : ChatServerConnection, ChatControlsInitializer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPeerPoolInfo' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPeerPoolInfo : PeerPoolInfo
		{
			public AnonymousClassPeerPoolInfo(DummyClient enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(DummyClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private DummyClient enclosingInstance;
			virtual public System.String ModuleName
			{
				get
				{
					return GameModule.getGameModule() == null?"<unnamed module>":GameModule.getGameModule().getGameName(); //$NON-NLS-1$
				}
				
			}
			virtual public System.String UserName
			{
				get
				{
					return GameModule.getGameModule() == null?"<" + Chatter.AnonymousUserName + ">":(System.String) GameModule.getGameModule().getPrefs().getValue(GameModule.REAL_NAME); //$NON-NLS-1$ //$NON-NLS-2$
				}
				
			}
			public DummyClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
		}
		virtual public ServerStatus StatusServer
		{
			get
			{
				return null;
			}
			
		}
		virtual public Player UserInfo
		{
			get
			{
				return playerInfo;
			}
			
			set
			{
				this.playerInfo = value;
			}
			
		}
		private Player playerInfo = new SimplePlayer("<nobody>"); //$NON-NLS-1$
		private HttpMessageServer httpMessageServer;
		private MessageBoardControlsInitializer msgControls;
		private ServerStatusControlsInitializer statusControls;
		
		
		public DummyClient()
		{
			PeerPoolInfo publicInfo = new AnonymousClassPeerPoolInfo(this);
			httpMessageServer = new HttpMessageServer(publicInfo);
			msgControls = new MessageBoardControlsInitializer(Resources.getString("Chat.messages"), httpMessageServer); //$NON-NLS-1$
			statusControls = new ServerStatusControlsInitializer(new CgiServerStatus());
		}
		
		public virtual Room[] getAvailableRooms()
		{
			return new Room[0];
		}
		
		public virtual Room getRoom()
		{
			return null;
		}
		
		public virtual void  sendTo(Player recipient, Command c)
		{
		}
		
		public virtual void  setRoom(Room r)
		{
		}
		
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  addPropertyChangeListener(System.String propertyName, PropertyChangeListener l)
		{
		}
		
		public virtual bool isConnected()
		{
			return false;
		}
		
		public virtual void  sendToOthers(Command c)
		{
		}
		
		public virtual void  setConnected(bool connect)
		{
		}
		
		public virtual void  initializeControls(ChatServerControls controls)
		{
			msgControls.initializeControls(controls);
			statusControls.initializeControls(controls);
			controls.RoomControlsVisible = true;
		}
		
		public virtual void  uninitializeControls(ChatServerControls controls)
		{
			msgControls.uninitializeControls(controls);
			statusControls.uninitializeControls(controls);
		}
	}
}