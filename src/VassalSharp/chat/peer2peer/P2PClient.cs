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
using ActivePeer = org.litesoft.p2pchat.ActivePeer;
using ActivePeerManager = org.litesoft.p2pchat.ActivePeerManager;
using MyInfo = org.litesoft.p2pchat.MyInfo;
using PeerInfo = org.litesoft.p2pchat.PeerInfo;
using PendingPeerManager = org.litesoft.p2pchat.PendingPeerManager;
using UserDialog = org.litesoft.p2pchat.UserDialog;
using GameModule = VassalSharp.build.GameModule;
using Chatter = VassalSharp.build.module.Chatter;
using ChatServerConnection = VassalSharp.chat.ChatServerConnection;
using Player = VassalSharp.chat.Player;
using PlayerEncoder = VassalSharp.chat.PlayerEncoder;
using Room = VassalSharp.chat.Room;
using ServerStatus = VassalSharp.chat.ServerStatus;
using SimplePlayer = VassalSharp.chat.SimplePlayer;
using SimpleRoom = VassalSharp.chat.SimpleRoom;
using SimpleStatus = VassalSharp.chat.SimpleStatus;
using SoundEncoder = VassalSharp.chat.SoundEncoder;
using SynchEncoder = VassalSharp.chat.SynchEncoder;
using WelcomeMessageServer = VassalSharp.chat.WelcomeMessageServer;
using Message = VassalSharp.chat.messageboard.Message;
using MessageBoard = VassalSharp.chat.messageboard.MessageBoard;
using ChatControlsInitializer = VassalSharp.chat.ui.ChatControlsInitializer;
using ChatServerControls = VassalSharp.chat.ui.ChatServerControls;
using RoomInteractionControlsInitializer = VassalSharp.chat.ui.RoomInteractionControlsInitializer;
using ShowProfileAction = VassalSharp.chat.ui.ShowProfileAction;
using SimpleStatusControlsInitializer = VassalSharp.chat.ui.SimpleStatusControlsInitializer;
using SynchAction = VassalSharp.chat.ui.SynchAction;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using Resources = VassalSharp.i18n.Resources;
using PropertiesEncoder = VassalSharp.tools.PropertiesEncoder;
namespace VassalSharp.chat.peer2peer
{
	
	public class P2PClient : ChatServerConnection, ChatControlsInitializer, UserDialog, PlayerEncoder
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(P2PClient enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(P2PClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private P2PClient enclosingInstance;
			public P2PClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				SimplePlayer p = (SimplePlayer) Enclosing_Instance.UserInfo;
				p.setName((System.String) evt.NewValue);
				Enclosing_Instance.UserInfo = p;
			}
		}
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: Constructor 'java.beans.PropertyChangeSupport.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			propSupport = new PropertyChangeSupport(this);
		}
		[System.ComponentModel.Browsable(true)]
		public  event SupportClass.PropertyChangeEventHandler PropertyChange;
		virtual public RoomManager RoomMgr
		{
			get
			{
				return roomMgr;
			}
			
		}
		virtual public Player UserInfo
		{
			get
			{
				return me;
			}
			
			set
			{
				if (me is P2PPlayer)
				{
					((P2PPlayer) me).setStats(value);
					if (isConnected())
					{
						SupportClass.PropertyChangingEventArgs me22 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.AVAILABLE_ROOMS, null, value);
						if (PropertyChange != null)
							PropertyChange(this, me22);
						SupportClass.PropertyChangingEventArgs me23 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.ROOM, null, value);
						if (PropertyChange != null)
							PropertyChange(this, me23);
						peerMgr.sendToAllNAME();
					}
				}
				else
				{
					me = (SimplePlayer) value;
					me.updateStatus();
				}
				SupportClass.PropertyChangingEventArgs me24 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.PLAYER_INFO, null, value);
				if (PropertyChange != null)
					PropertyChange(this, me24);
			}
			
		}
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
		virtual public ServerStatus StatusServer
		{
			get
			{
				return svrStatus;
			}
			
		}
		virtual public ActivePeerManager ActivePeerManager
		{
			set
			{
				peerMgr = value;
			}
			
		}
		virtual public PendingPeerManager PendingPeerManager
		{
			set
			{
				ppm = value;
			}
			
		}
		private SimplePlayer me;
		private PendingPeerManager ppm;
		protected internal ActivePeerManager peerMgr;
		private PeerPool pool;
		private MessageBoard msgSvr;
		private WelcomeMessageServer welcomeMessageServer;
		private RoomManager roomMgr;
		private RoomTracker tracker;
		//UPGRADE_ISSUE: Class 'java.beans.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
		//UPGRADE_NOTE: The initialization of  'propSupport' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private PropertyChangeSupport propSupport;
		private CommandEncoder encoder;
		private bool connected = false;
		private ServerStatus svrStatus;
		private RoomInteractionControlsInitializer roomControls;
		private SimpleStatusControlsInitializer playerStatusControls;
		private SoundEncoder soundEncoder;
		private SynchEncoder synchEncoder;
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		private PropertyChangeListener nameChangeListener;
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		private System.Collections.Specialized.NameValueCollection params_Renamed;
		
		//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public P2PClient(CommandEncoder encoder, MessageBoard msgSvr, WelcomeMessageServer welcomeMessageServer, PeerPool pool):this(encoder, msgSvr, welcomeMessageServer, pool, new System.Collections.Specialized.NameValueCollection())
		{
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public P2PClient(CommandEncoder encoder, MessageBoard msgSvr, WelcomeMessageServer welcomeMessageServer, PeerPool pool, System.Collections.Specialized.NameValueCollection param)
		{
			InitBlock();
			this.encoder = encoder;
			this.msgSvr = msgSvr;
			this.welcomeMessageServer = welcomeMessageServer;
			this.pool = pool;
			this.params_Renamed = param;
			ppm = new PendingPeerManager(this);
			//UPGRADE_NOTE: The Name property of a Thread in C# is write-once. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1140'"
			ppm.Name = "Pending Peer Manager"; //$NON-NLS-1$
			roomMgr = new RoomManager();
			tracker = new RoomTracker();
			me = new SimplePlayer("???"); //$NON-NLS-1$
			me.updateStatus();
			playerStatusControls = new SimpleStatusControlsInitializer(this, false);
			roomControls = new RoomInteractionControlsInitializer(this);
			roomControls.addPlayerActionFactory(ShowProfileAction.factory());
			roomControls.addPlayerActionFactory(SynchAction.factory(this));
			synchEncoder = new SynchEncoder(this, this);
			soundEncoder = new SoundEncoder(this);
			nameChangeListener = new AnonymousClassPropertyChangeListener(this);
		}
		
		public virtual void  sendToOthers(Command c)
		{
			sendToOthers(encoder.encode(c));
		}
		
		public virtual void  sendToAll(System.String msg)
		{
			if (isConnected())
			{
				sendToOthers(msg);
				showCHAT(((P2PPlayer) me).Info, msg);
			}
		}
		
		public virtual void  sendToOthers(System.String msg)
		{
			if (isConnected())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'myRoom '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Room myRoom = getRoom();
				//UPGRADE_NOTE: Final was removed from the declaration of 'pl '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Player[] pl = myRoom.getPlayerList().toArray(new Player[myRoom.getPlayerList().size()]);
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Player p: pl)
				{
					if (!p.equals(me))
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'peer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						ActivePeer peer = peerMgr.getPeerListenerByID(((P2PPlayer) p).Info.ID);
						if (peer != null)
						{
							peer.sendCHAT(msg);
						}
					}
				}
			}
		}
		
		public virtual void  sendTo(Player recipient, Command c)
		{
			if (peerMgr != null)
			{
				peerMgr.getPeerListenerByInfo(((P2PPlayer) recipient).Info).sendCHAT(encoder.encode(c));
			}
		}
		
		public virtual Room getRoom()
		{
			return roomMgr.getRoomContaining(me);
		}
		
		public virtual void  setRoom(Room r)
		{
			if (me is P2PPlayer)
			{
				((P2PPlayer) me).Room = r.getName();
				if (isConnected())
				{
					peerMgr.sendToAllNAME();
					SupportClass.PropertyChangingEventArgs me20 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.AVAILABLE_ROOMS, null, roomMgr.update(((P2PPlayer) me).Info));
					if (PropertyChange != null)
						PropertyChange(this, me20);
					Room newRoom = getRoom();
					SupportClass.PropertyChangingEventArgs me21 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.ROOM, null, newRoom);
					if (PropertyChange != null)
						PropertyChange(this, me21);
				}
			}
		}
		
		public virtual Room[] getAvailableRooms()
		{
			return roomMgr.Rooms;
		}
		
		public virtual void  setConnected(bool connect)
		{
			if (connect)
			{
				try
				{
					System.Int32 port;
					try
					{
						port = System.Int32.Parse(params_Renamed.Get(P2PClientFactory.P2P_LISTEN_PORT));
					}
					catch (System.FormatException ex)
					{
						port = Integer.valueOf(5050);
					}
					MyInfo info = new MyInfo(null, port);
					info.NetworkPw = params_Renamed.Get(P2PClientFactory.P2P_SERVER_PW);
					P2PPlayer p = new P2PPlayer(info);
					p.updateStatus();
					p.setName(me.getName());
					p.Room = roomMgr.DefaultRoom.getName();
					p.setId(GameModule.getUserId() + "." + (System.DateTime.Now.Ticks - 621355968000000000) / 10000); //$NON-NLS-1$
					UserInfo = p;
					pool.initialize(p, ppm);
					if (peerMgr == null)
					{
						peerMgr = new ActivePeerManager(info, this, ppm);
					}
					roomMgr.update(((P2PPlayer) me).Info);
					fireStatus(Resources.getString("Peer2Peer.server_connection_established", params_Renamed.Get(P2PClientFactory.P2P_LISTEN_PORT))); //$NON-NLS-1$
					SupportClass.PropertyChangingEventArgs me25 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.AVAILABLE_ROOMS, null, roomMgr.Rooms);
					if (PropertyChange != null)
						PropertyChange(this, me25);
					SupportClass.PropertyChangingEventArgs me26 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.ROOM, null, getRoom());
					if (PropertyChange != null)
						PropertyChange(this, me26);
					welcomeMessageServer.WelcomeMessage.execute();
					connected = true;
					SupportClass.PropertyChangingEventArgs me27 = new SupportClass.PropertyChangingEventArgs(VassalSharp.build.module.ServerConnection_Fields.CONNECTED, null, true);
					if (PropertyChange != null)
						PropertyChange(this, me27);
				}
				// FIXME: review error message
				catch (System.IO.IOException e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					fireStatus(Resources.getString("Peer2Peer.connection_error", e.Message)); //$NON-NLS-1$
					fireStatus(Resources.getString("Peer2Peer.disconnected")); //$NON-NLS-1$ //$NON-NLS-2$
					connected = false;
					SupportClass.PropertyChangingEventArgs me28 = new SupportClass.PropertyChangingEventArgs(VassalSharp.build.module.ServerConnection_Fields.CONNECTED, null, false);
					if (PropertyChange != null)
						PropertyChange(this, me28);
				}
			}
			else if (isConnected())
			{
				if (peerMgr != null)
				{
					peerMgr.clear();
				}
				roomMgr.clear();
				pool.disconnect();
				SupportClass.PropertyChangingEventArgs me29 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.AVAILABLE_ROOMS, null, new Room[0]);
				if (PropertyChange != null)
					PropertyChange(this, me29);
				SupportClass.PropertyChangingEventArgs me30 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.ROOM, new SimpleRoom(), null);
				if (PropertyChange != null)
					PropertyChange(this, me30);
				connected = false;
				SupportClass.PropertyChangingEventArgs me31 = new SupportClass.PropertyChangingEventArgs(VassalSharp.build.module.ServerConnection_Fields.CONNECTED, true, false);
				if (PropertyChange != null)
					PropertyChange(this, me31);
				fireStatus(Resources.getString("Peer2Peer.disconnected")); //$NON-NLS-1$ //$NON-NLS-2$
				ppm.finish();
			}
		}
		
		protected internal virtual void  fireStatus(System.String msg)
		{
			SupportClass.PropertyChangingEventArgs me32 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.STATUS, null, msg);
			if (PropertyChange != null)
				PropertyChange(this, me32);
		}
		
		public virtual bool isConnected()
		{
			return connected;
		}
		
		public virtual void  postMessage(System.String msg)
		{
			msgSvr.postMessage(msg);
		}
		
		public virtual Player stringToPlayer(System.String s)
		{
			return roomMgr.getPlayerById(s);
		}
		
		public virtual System.String playerToString(Player p)
		{
			return p.getId();
		}
		
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  addPropertyChangeListener(System.String propertyName, java.beans.PropertyChangeListener l)
		{
			//UPGRADE_ISSUE: Method 'java.beans.PropertyChangeSupport.addPropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			propSupport.addPropertyChangeListener(propertyName, l);
		}
		
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  addPropertyChangeListener(PropertyChangeListener l)
		{
			//UPGRADE_ISSUE: Method 'java.beans.PropertyChangeSupport.addPropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			propSupport.addPropertyChangeListener(l);
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'showUnrecognized'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  showUnrecognized(PeerInfo pPeerInfo, System.String pBadMessage)
		{
			lock (this)
			{
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'showStreamsFailed'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  showStreamsFailed(PeerInfo pPeerInfo)
		{
			lock (this)
			{
				P2PPlayer p = new P2PPlayer(pPeerInfo);
				SupportClass.PropertyChangingEventArgs me33 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.STATUS, null, Resources.getString("Peer2Peer.connection_lost", p.getName()));
				if (PropertyChange != null)
					PropertyChange(this, me33); //$NON-NLS-1$
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'showConnectFailed'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  showConnectFailed(PeerInfo pPeerInfo)
		{
			lock (this)
			{
				pool.connectFailed(pPeerInfo);
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'showConnect'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  showConnect(PeerInfo pPeerInfo)
		{
			lock (this)
			{
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'showDisconnect'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  showDisconnect(PeerInfo pPeerInfo)
		{
			lock (this)
			{
				SupportClass.PropertyChangingEventArgs me34 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.AVAILABLE_ROOMS, null, roomMgr.remove(pPeerInfo));
				if (PropertyChange != null)
					PropertyChange(this, me34);
				SupportClass.PropertyChangingEventArgs me35 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.ROOM, null, getRoom());
				if (PropertyChange != null)
					PropertyChange(this, me35);
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'showCHAT'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  showCHAT(PeerInfo pPeerInfo, System.String msg)
		{
			lock (this)
			{
				SupportClass.PropertyChangingEventArgs me36 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.INCOMING_MSG, null, msg);
				if (PropertyChange != null)
					PropertyChange(this, me36);
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'showPMSG'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  showPMSG(PeerInfo pPeerInfo, System.String msg)
		{
			lock (this)
			{
				showCHAT(pPeerInfo, msg);
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'showNAME'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  showNAME(PeerInfo pPeerInfo)
		{
			lock (this)
			{
				tracker.init(getRoom());
				SupportClass.PropertyChangingEventArgs me37 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.AVAILABLE_ROOMS, null, roomMgr.update(pPeerInfo));
				if (PropertyChange != null)
					PropertyChange(this, me37);
				Room myRoom = getRoom();
				SupportClass.PropertyChangingEventArgs me38 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.ROOM, null, myRoom);
				if (PropertyChange != null)
					PropertyChange(this, me38);
				tracker.finalize(myRoom);
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'showHELO'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  showHELO(PeerInfo pPeerInfo)
		{
			lock (this)
			{
				// We have received a connection request
				//UPGRADE_NOTE: Final was removed from the declaration of 'chatter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Chatter chatter = GameModule.getGameModule().getChatter();
				//UPGRADE_NOTE: Final was removed from the declaration of 'peer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ActivePeer peer = peerMgr.getPeerListenerByInfo(pPeerInfo);
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				System.Collections.Specialized.NameValueCollection props;
				System.String name, ip, details;
				try
				{
					props = new PropertiesEncoder(pPeerInfo.getChatName()).Properties;
					name = props.Get(SimpleStatus.NAME);
					ip = props.Get(SimpleStatus.IP);
					details = name + " (" + ip + ":" + pPeerInfo.Port + ")";
				}
				catch (System.IO.IOException ex)
				{
					details = "";
				}
				
				// Does the password of the new Peer match ours?
				if (!pPeerInfo.NetworkPw.Equals(params_Renamed.Get(P2PClientFactory.P2P_SERVER_PW)))
				{
					new Chatter.DisplayText(chatter, Resources.getString("Peer2Peer.bad_password", details)).execute();
					peer.finish();
					return ;
				}
				
				fireStatus(Resources.getString("Peer2Peer.connected", details));
				
				SupportClass.PropertyChangingEventArgs me39 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.AVAILABLE_ROOMS, null, roomMgr.update(pPeerInfo));
				if (PropertyChange != null)
					PropertyChange(this, me39);
				SupportClass.PropertyChangingEventArgs me40 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.ROOM, null, getRoom());
				if (PropertyChange != null)
					PropertyChange(this, me40);
			}
		}
		
		public virtual void  initializeControls(ChatServerControls controls)
		{
			playerStatusControls.initializeControls(controls);
			roomControls.initializeControls(controls);
			controls.RoomControlsVisible = false;
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GameModule g = GameModule.getGameModule();
			me.setName((System.String) g.getPrefs().getValue(GameModule.REAL_NAME));
			g.getPrefs().getOption(GameModule.REAL_NAME).addPropertyChangeListener(nameChangeListener);
			g.addCommandEncoder(synchEncoder);
			g.addCommandEncoder(soundEncoder);
			if (pool is ChatControlsInitializer)
			{
				((ChatControlsInitializer) pool).initializeControls(controls);
			}
		}
		
		public virtual void  uninitializeControls(ChatServerControls controls)
		{
			playerStatusControls.uninitializeControls(controls);
			roomControls.uninitializeControls(controls);
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GameModule g = GameModule.getGameModule();
			g.getPrefs().getOption(GameModule.REAL_NAME).removePropertyChangeListener(nameChangeListener);
			g.removeCommandEncoder(synchEncoder);
			g.removeCommandEncoder(soundEncoder);
			if (pool is ChatControlsInitializer)
			{
				((ChatControlsInitializer) pool).uninitializeControls(controls);
			}
		}
	}
}