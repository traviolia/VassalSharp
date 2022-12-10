/*
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
//UPGRADE_TODO: The type 'org.apache.commons.codec.binary.Base64' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Base64 = org.apache.commons.codec.binary.Base64;
using Info = VassalSharp.Info;
using GameModule = VassalSharp.build.GameModule;
using CgiServerStatus = VassalSharp.chat.CgiServerStatus;
using Compressor = VassalSharp.chat.Compressor;
using InviteCommand = VassalSharp.chat.InviteCommand;
using InviteEncoder = VassalSharp.chat.InviteEncoder;
using LockableChatServerConnection = VassalSharp.chat.LockableChatServerConnection;
using LockableRoom = VassalSharp.chat.LockableRoom;
using MainRoomChecker = VassalSharp.chat.MainRoomChecker;
using Player = VassalSharp.chat.Player;
using PlayerEncoder = VassalSharp.chat.PlayerEncoder;
using PrivateChatEncoder = VassalSharp.chat.PrivateChatEncoder;
using PrivateChatManager = VassalSharp.chat.PrivateChatManager;
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
using InviteAction = VassalSharp.chat.ui.InviteAction;
using KickAction = VassalSharp.chat.ui.KickAction;
using LockableRoomTreeRenderer = VassalSharp.chat.ui.LockableRoomTreeRenderer;
using MessageBoardControlsInitializer = VassalSharp.chat.ui.MessageBoardControlsInitializer;
using PrivateMessageAction = VassalSharp.chat.ui.PrivateMessageAction;
using RoomInteractionControlsInitializer = VassalSharp.chat.ui.RoomInteractionControlsInitializer;
using SendSoundAction = VassalSharp.chat.ui.SendSoundAction;
using ServerStatusControlsInitializer = VassalSharp.chat.ui.ServerStatusControlsInitializer;
using ShowProfileAction = VassalSharp.chat.ui.ShowProfileAction;
using SimpleStatusControlsInitializer = VassalSharp.chat.ui.SimpleStatusControlsInitializer;
using SynchAction = VassalSharp.chat.ui.SynchAction;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using Resources = VassalSharp.i18n.Resources;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
using PropertiesEncoder = VassalSharp.tools.PropertiesEncoder;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.chat.node
{
	
	/// <author>  rkinney
	/// </author>
	public abstract class NodeClient : LockableChatServerConnection, PlayerEncoder, ChatControlsInitializer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(NodeClient enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(NodeClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private NodeClient enclosingInstance;
			public NodeClient Enclosing_Instance
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
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener1
		{
			public AnonymousClassPropertyChangeListener1(NodeClient enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(NodeClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private NodeClient enclosingInstance;
			public NodeClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				SimplePlayer p = (SimplePlayer) Enclosing_Instance.UserInfo;
				SimpleStatus s = (SimpleStatus) p.getStatus();
				s = new SimpleStatus(s.Looking, s.Away, (System.String) evt.NewValue, s.Client, s.Ip, s.ModuleVersion, s.Crc);
				p.setStatus(s);
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
		virtual public Player UserInfo
		{
			get
			{
				return me;
			}
			
			set
			{
				me.setName(value.getName());
				me.setStatus(value.getStatus());
				sendStats();
				SupportClass.PropertyChangingEventArgs me12 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.PLAYER_INFO, null, value);
				if (PropertyChange != null)
					PropertyChange(this, me12);
			}
			
		}
		virtual public NodePlayer MyInfo
		{
			get
			{
				return me;
			}
			
		}
		virtual public MessageBoard MessageServer
		{
			get
			{
				return msgSvr;
			}
			
		}
		virtual public Message[] Messages
		{
			get
			{
				return msgSvr.Messages;
			}
			
		}
		public const System.String ZIP_HEADER = "!ZIP!"; //$NON-NLS-1$
		//UPGRADE_ISSUE: Class 'java.beans.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
		//UPGRADE_NOTE: The initialization of  'propSupport' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal PropertyChangeSupport propSupport;
		protected internal NodePlayer me;
		protected internal SimpleRoom currentRoom;
		protected internal System.String defaultRoomName = VassalSharp.chat.ChatServerConnection_Fields.DEFAULT_ROOM_NAME; //$NON-NLS-1$
		protected internal NodeRoom[] allRooms = new NodeRoom[0];
		protected internal MessageBoard msgSvr;
		protected internal WelcomeMessageServer welcomer;
		protected internal ServerStatus serverStatus;
		protected internal System.String moduleName;
		protected internal System.String playerId;
		protected internal MainRoomChecker checker = new MainRoomChecker();
		protected internal int compressionLimit = 1000;
		protected internal CommandEncoder encoder;
		protected internal MessageBoardControlsInitializer messageBoardControls;
		protected internal RoomInteractionControlsInitializer roomControls;
		protected internal ServerStatusControlsInitializer serverStatusControls;
		protected internal SimpleStatusControlsInitializer playerStatusControls;
		protected internal SoundEncoder soundEncoder;
		protected internal PrivateChatEncoder privateChatEncoder;
		protected internal SynchEncoder synchEncoder;
		protected internal InviteEncoder inviteEncoder;
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		protected internal PropertyChangeListener nameChangeListener;
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		protected internal PropertyChangeListener profileChangeListener;
		protected internal NodeRoom pendingSynchToRoom;
		
		public NodeClient(System.String moduleName, System.String playerId, CommandEncoder encoder, MessageBoard msgSvr, WelcomeMessageServer welcomer)
		{
			InitBlock();
			this.encoder = encoder;
			this.msgSvr = msgSvr;
			this.welcomer = welcomer;
			this.playerId = playerId;
			this.moduleName = moduleName;
			serverStatus = new CgiServerStatus();
			me = new NodePlayer(playerId);
			messageBoardControls = new MessageBoardControlsInitializer(Resources.getString("Chat.messages"), msgSvr); //$NON-NLS-1$
			roomControls = new LockableNodeRoomControls(this);
			roomControls.addPlayerActionFactory(ShowProfileAction.factory());
			roomControls.addPlayerActionFactory(SynchAction.factory(this));
			PrivateChatManager privateChatManager = new PrivateChatManager(this);
			roomControls.addPlayerActionFactory(PrivateMessageAction.factory(this, privateChatManager));
			roomControls.addPlayerActionFactory(SendSoundAction.factory(this, Resources.getString("Chat.send_wakeup"), "wakeUpSound", "phone1.wav")); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
			roomControls.addPlayerActionFactory(InviteAction.factory(this));
			roomControls.addPlayerActionFactory(KickAction.factory(this));
			serverStatusControls = new ServerStatusControlsInitializer(serverStatus);
			playerStatusControls = new SimpleStatusControlsInitializer(this);
			synchEncoder = new SynchEncoder(this, this);
			privateChatEncoder = new PrivateChatEncoder(this, privateChatManager);
			soundEncoder = new SoundEncoder(this);
			inviteEncoder = new InviteEncoder(this);
			nameChangeListener = new AnonymousClassPropertyChangeListener(this);
			profileChangeListener = new AnonymousClassPropertyChangeListener1(this);
		}
		
		public virtual void  setConnected(bool connect)
		{
			if (connect)
			{
				if (!isConnected())
				{
					try
					{
						NodePlayer oldPlayer = me;
						me = new NodePlayer(playerId);
						UserInfo = oldPlayer;
						initializeConnection();
						Command welcomeMessage = welcomer.WelcomeMessage;
						if (welcomeMessage != null)
						{
							welcomeMessage.execute();
						}
						registerNewConnection();
					}
					// FIXME: review error message
					catch (System.IO.IOException e)
					{
						SupportClass.PropertyChangingEventArgs me10 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.STATUS, null, Resources.getString("Chat.unable_to_establish", e.Message));
						if (PropertyChange != null)
							PropertyChange(this, me10); //$NON-NLS-1$
					}
				}
			}
			else
			{
				if (isConnected())
				{
					closeConnection();
				}
				currentRoom = null;
				allRooms = new NodeRoom[0];
			}
			SupportClass.PropertyChangingEventArgs me11 = new SupportClass.PropertyChangingEventArgs(VassalSharp.build.module.ServerConnection_Fields.CONNECTED, null, isConnected()?true:false);
			if (PropertyChange != null)
				PropertyChange(this, me11);
		}
		
		protected internal virtual void  registerNewConnection()
		{
			System.String path = new SequenceEncoder(moduleName, '/').append(defaultRoomName).Value;
			send(Protocol.encodeRegisterCommand(me.getId(), path, new PropertiesEncoder(me.toProperties()).StringValue));
			if (GameModule.getGameModule() != null)
			{
				System.String username = (System.String) GameModule.getGameModule().getPrefs().getValue("Login"); //$NON-NLS-1$
				if (username != null)
				{
					send(Protocol.encodeLoginCommand(username));
				}
			}
		}
		
		protected internal abstract void  closeConnection();
		
		protected internal abstract void  initializeConnection();
		
		public abstract void  send(System.String command);
		
		public virtual void  setDefaultRoomName(System.String defaultRoomName)
		{
			this.defaultRoomName = defaultRoomName;
		}
		
		public virtual System.String getDefaultRoomName()
		{
			return defaultRoomName;
		}
		
		public virtual bool isDefaultRoom(Room r)
		{
			return r == null?false:r.getName().Equals(getDefaultRoomName());
		}
		
		protected internal virtual void  sendStats()
		{
			if (isConnected())
			{
				send(Protocol.encodeStatsCommand(new PropertiesEncoder(me.toProperties()).StringValue));
			}
		}
		
		public virtual void  sendToOthers(Command c)
		{
			sendToOthers(encoder.encode(c));
		}
		
		public virtual void  sendToAll(System.String msg)
		{
			if (currentRoom != null)
			{
				System.String path = new SequenceEncoder(moduleName, '/').append(currentRoom.getName()).Value;
				forward(path, msg);
			}
		}
		
		public virtual void  forward(System.String receipientPath, System.String msg)
		{
			if (isConnected() && currentRoom != null && msg != null)
			{
				msg = checker.filter(msg, defaultRoomName, currentRoom.getName());
				if (msg.Length > compressionLimit)
				{
					try
					{
						//UPGRADE_TODO: Method 'java.lang.String.getBytes' was converted to 'System.Text.Encoding.GetEncoding(string).GetBytes(string)' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangStringgetBytes_javalangString'"
						msg = ZIP_HEADER + Base64.encodeBase64String(Compressor.compress(SupportClass.ToSByteArray(System.Text.Encoding.GetEncoding("UTF-8").GetBytes(msg))));
					}
					// FIXME: review error message
					catch (System.IO.IOException e)
					{
						SupportClass.WriteStackTrace(e, Console.Error);
					}
				}
				send(Protocol.encodeForwardCommand(receipientPath, msg));
			}
		}
		
		public virtual void  sendToOthers(System.String msg)
		{
			if (currentRoom != null)
			{
				System.String path = new SequenceEncoder(moduleName, '/').append(currentRoom.getName()).append("~" + me.getId()).Value; //$NON-NLS-1$
				forward(path, msg);
			}
		}
		
		public virtual void  sendTo(Player recipient, Command c)
		{
			System.String path = new SequenceEncoder(moduleName, '/').append("*").append(((NodePlayer) recipient).getId()).Value; //$NON-NLS-1$
			forward(path, encoder.encode(c));
		}
		
		public virtual void  doKick(Player kickee)
		{
			send(Protocol.encodeKickCommand(kickee.getId()));
		}
		
		public virtual bool isKickable(Player kickee)
		{
			if (kickee != null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'room '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Room room = getRoom();
				// Is this a locked room?
				if (room is LockableRoom && ((LockableRoom) room).Locked)
				{
					if (room is NodeRoom)
					{
						// Is the target player in the same room?
						if (((NodeRoom) room).contains(kickee))
						{
							//UPGRADE_NOTE: Final was removed from the declaration of 'owner '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							System.String owner = ((NodeRoom) room).getOwner();
							// Do I own this room and the target is not me?
							if (owner != null && owner.Equals(UserInfo.getId()) && !owner.Equals(kickee.getId()))
							{
								return true;
							}
						}
					}
				}
			}
			return false;
		}
		
		public virtual bool isInvitable(Player invitee)
		{
			if (invitee != null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'room '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Room room = getRoom();
				if (room is NodeRoom)
				{
					// Is the target player in a different room?
					if (!((NodeRoom) room).contains(invitee))
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'owner '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String owner = ((NodeRoom) room).getOwner();
						// Do I own this room and the target is not me?
						if (owner != null && owner.Equals(UserInfo.getId()) && !owner.Equals(invitee.getId()))
						{
							return true;
						}
					}
				}
			}
			return false;
		}
		
		/// <summary> Send Invitation to another player to join the current room
		/// 
		/// </summary>
		/// <param name="invitee">Player to invite
		/// </param>
		public virtual void  sendInvite(Player invitee)
		{
			sendTo(invitee, new InviteCommand(me.getName(), me.getId(), getRoom().getName()));
		}
		
		/// <summary> Process an invitation request from a player to join a room
		/// 
		/// </summary>
		/// <param name="player">Inviting player name
		/// </param>
		/// <param name="room">Inviting room
		/// </param>
		public virtual void  doInvite(System.String playerId, System.String roomName)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Room room: getAvailableRooms())
			{
				if (room.getName().equals(roomName))
				{
					if (room is NodeRoom)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'owner '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String owner = ((NodeRoom) room).getOwner();
						if (owner != null && owner.Equals(playerId))
						{
							setRoom(room, playerId);
							return ;
						}
					}
				}
			}
		}
		
		public virtual Room getRoom()
		{
			return currentRoom;
		}
		
		public virtual Room[] getAvailableRooms()
		{
			return allRooms;
		}
		
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  addPropertyChangeListener(System.String propertyName, PropertyChangeListener l)
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
		
		public virtual void  lockRoom(LockableRoom r)
		{
			if (r is NodeRoom)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'n '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				NodeRoom n = (NodeRoom) r;
				if (n.getOwner().Equals(me.getId()))
				{
					n.toggleLock();
					sendRoomInfo(n);
					SupportClass.PropertyChangingEventArgs me13 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.AVAILABLE_ROOMS, null, allRooms);
					if (PropertyChange != null)
						PropertyChange(this, me13);
				}
			}
		}
		
		public virtual void  sendRoomInfo(NodeRoom r)
		{
			Node dummy = new Node(null, r.getName(), new PropertiesEncoder(r.Info).StringValue);
			if (isConnected())
			{
				System.String msg = Protocol.encodeRoomsInfo(new Node[]{dummy});
				send(msg);
			}
		}
		
		public virtual void  setRoom(Room r)
		{
			setRoom(r, null);
		}
		
		public virtual void  setRoom(Room r, System.String password)
		{
			if (isConnected())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'newRoom '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String newRoom = r.getName();
				//UPGRADE_NOTE: Final was removed from the declaration of 'newPath '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String newPath = new SequenceEncoder(moduleName, '/').append(newRoom).Value;
				System.String msg = Protocol.encodeJoinCommand(newPath, password);
				send(msg);
				// Request a synch if we are not the owner
				if (r is NodeRoom)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'room '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					NodeRoom room = (NodeRoom) r;
					if (newRoom.Equals(defaultRoomName))
					{
						GameModule.getGameModule().getGameState().setup(false);
					}
					else if (!room.isOwner(me))
					{
						// We are not actually recorded as being in the new room until we get
						// an update back from
						// the server. Record a Synch required to the new room.
						pendingSynchToRoom = room;
						GameModule.getGameModule().warn(Resources.getString("Chat.synchronize_pending"));
					}
				}
			}
		}
		
		/// <summary> Process a message received from the server
		/// 
		/// </summary>
		/// <param name="msg">Encoded message
		/// </param>
		public virtual void  handleMessageFromServer(System.String msg)
		{
			Node n;
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Collections.Specialized.NameValueCollection p;
			if ((n = Protocol.decodeListCommand(msg)) != null)
			{
				Node mod = n.getChild(moduleName);
				if (mod != null)
				{
					updateRooms(mod);
				}
				// Rooms have been updated with any new players (including us), so perform
				// a Synchronize
				// for a move to a new room if needed.
				if (pendingSynchToRoom != null)
				{
					//UPGRADE_TODO: Method 'javax.swing.AbstractAction.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					new SynchAction(pendingSynchToRoom.OwningPlayer, this).actionPerformed(null);
					pendingSynchToRoom = null;
					GameModule.getGameModule().warn(Resources.getString("Chat.synchronize_complete"));
				}
			}
			else if ((p = Protocol.decodeRoomsInfo(msg)) != null)
			{
				for (int i = 0; i < allRooms.Length; ++i)
				{
					System.String infoString = p.Get(allRooms[i].getName());
					if (infoString != null && infoString.Length > 0)
					{
						try
						{
							//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
							System.Collections.Specialized.NameValueCollection info = new PropertiesEncoder(infoString).Properties;
							allRooms[i].Info = info;
						}
						// FIXME: review error message
						catch (System.IO.IOException e)
						{
							SupportClass.WriteStackTrace(e, Console.Error);
						}
					}
				}
				SupportClass.PropertyChangingEventArgs me14 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.ROOM, null, currentRoom);
				if (PropertyChange != null)
					PropertyChange(this, me14);
				SupportClass.PropertyChangingEventArgs me15 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.AVAILABLE_ROOMS, null, allRooms);
				if (PropertyChange != null)
					PropertyChange(this, me15);
			}
			else if (Protocol.decodeRegisterRequest(msg))
			{
				registerNewConnection();
			}
			else
			{
				if (msg.StartsWith(ZIP_HEADER))
				{
					try
					{
						msg = new String(Compressor.decompress(Base64.decodeBase64(msg.Substring(ZIP_HEADER.Length))), "UTF-8");
					}
					// FIXME: review error message
					catch (System.IO.IOException e)
					{
						SupportClass.WriteStackTrace(e, Console.Error);
					}
				}
				SupportClass.PropertyChangingEventArgs me16 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.INCOMING_MSG, null, msg);
				if (PropertyChange != null)
					PropertyChange(this, me16);
			}
		}
		
		protected internal virtual void  updateRooms(Node module)
		{
			Node[] roomNodes = module.Children;
			NodeRoom[] rooms = new NodeRoom[roomNodes.Length];
			int defaultRoomIndex = - 1;
			for (int i = 0; i < roomNodes.Length; ++i)
			{
				Node[] playerNodes = roomNodes[i].Children;
				NodePlayer[] players = new NodePlayer[playerNodes.Length];
				bool containsMe = false;
				for (int j = 0; j < playerNodes.Length; ++j)
				{
					players[j] = new NodePlayer(playerNodes[j].Id);
					if (players[j].Equals(me))
					{
						containsMe = true;
					}
					try
					{
						//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
						System.Collections.Specialized.NameValueCollection p = new PropertiesEncoder(playerNodes[j].getInfo()).Properties;
						players[j].Info = p;
						if (players[j].Equals(me))
						{
							me.Info = p;
						}
					}
					// FIXME: review error message
					catch (System.IO.IOException e)
					{
						SupportClass.WriteStackTrace(e, Console.Error);
					}
				}
				
				rooms[i] = new NodeRoom(roomNodes[i].Id, players);
				
				// Lock room to start with. The ROOM_INFO message will unlock
				// any rooms that are not locked. Prevents unwanted clients from
				// connecting while room is in an undefined state.
				if (!rooms[i].getName().Equals(defaultRoomName))
				{
					rooms[i].lock_Renamed();
				}
				
				try
				{
					if (roomNodes[i].getInfo() != null)
					{
						rooms[i].Info = new PropertiesEncoder(roomNodes[i].getInfo()).Properties;
					}
				}
				// FIXME: review error message
				catch (System.IO.IOException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
				}
				if (containsMe)
				{
					currentRoom = rooms[i];
				}
				if (defaultRoomName.Equals(rooms[i].getName()))
				{
					defaultRoomIndex = i;
				}
			}
			if (defaultRoomIndex < 0)
			{
				allRooms = ArrayUtils.prepend(rooms, new NodeRoom(defaultRoomName));
			}
			else
			{
				allRooms = rooms;
				NodeRoom swap = allRooms[0];
				allRooms[0] = allRooms[defaultRoomIndex];
				allRooms[defaultRoomIndex] = swap;
			}
			// Do not fire a PropertyChange request, The server will be following
			// immediately
			// with a Room List refresh which can cause Icons to flash unexpectedly.
			// propSupport.firePropertyChange(ROOM, null, currentRoom);
			// propSupport.firePropertyChange(AVAILABLE_ROOMS, null, allRooms);
		}
		
		public virtual void  postMessage(System.String msg)
		{
			msgSvr.postMessage(msg);
		}
		
		public virtual Player stringToPlayer(System.String s)
		{
			NodePlayer p = null;
			try
			{
				PropertiesEncoder propEncoder = new PropertiesEncoder(s);
				p = new NodePlayer(null);
				p.Info = propEncoder.Properties;
			}
			// FIXME: review error message
			catch (System.IO.IOException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
			}
			return p;
		}
		
		public virtual System.String playerToString(Player p)
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Collections.Specialized.NameValueCollection props = ((NodePlayer) p).toProperties();
			return new PropertiesEncoder(props).StringValue;
		}
		
		public virtual void  initializeControls(ChatServerControls controls)
		{
			playerStatusControls.initializeControls(controls);
			messageBoardControls.initializeControls(controls);
			roomControls.initializeControls(controls);
			serverStatusControls.initializeControls(controls);
			controls.RoomControlsVisible = true;
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GameModule g = GameModule.getGameModule();
			g.addCommandEncoder(synchEncoder);
			g.addCommandEncoder(privateChatEncoder);
			g.addCommandEncoder(soundEncoder);
			g.addCommandEncoder(inviteEncoder);
			me.setName((System.String) g.getPrefs().getValue(GameModule.REAL_NAME));
			g.getPrefs().getOption(GameModule.REAL_NAME).addPropertyChangeListener(nameChangeListener);
			SimpleStatus s = (SimpleStatus) me.getStatus();
			s = new SimpleStatus(s.Looking, s.Away, (System.String) g.getPrefs().getValue(GameModule.PERSONAL_INFO), Info.Version, s.Ip, g.getGameVersion() + ((g.getArchiveWriter() == null)?"":" (Editing)"), Long.toHexString(g.getCrc()));
			me.setStatus(s);
			g.getPrefs().getOption(GameModule.PERSONAL_INFO).addPropertyChangeListener(profileChangeListener);
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetCellRenderer_javaxswingtreeTreeCellRenderer'"
			controls.RoomTree.setCellRenderer(new LockableRoomTreeRenderer());
		}
		
		public virtual void  uninitializeControls(ChatServerControls controls)
		{
			messageBoardControls.uninitializeControls(controls);
			roomControls.uninitializeControls(controls);
			serverStatusControls.uninitializeControls(controls);
			playerStatusControls.uninitializeControls(controls);
			GameModule.getGameModule().removeCommandEncoder(synchEncoder);
			GameModule.getGameModule().removeCommandEncoder(privateChatEncoder);
			GameModule.getGameModule().removeCommandEncoder(soundEncoder);
			GameModule.getGameModule().removeCommandEncoder(inviteEncoder);
			GameModule.getGameModule().getPrefs().getOption(GameModule.REAL_NAME).removePropertyChangeListener(nameChangeListener);
			GameModule.getGameModule().getPrefs().getOption(GameModule.PERSONAL_INFO).removePropertyChangeListener(profileChangeListener);
		}
		public abstract bool isConnected();
	}
}