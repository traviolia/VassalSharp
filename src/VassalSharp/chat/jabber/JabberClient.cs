/*
* $Id$
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
//UPGRADE_TODO: The type 'org.jivesoftware.smack.Chat' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Chat = org.jivesoftware.smack.Chat;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.ConnectionConfiguration' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ConnectionConfiguration = org.jivesoftware.smack.ConnectionConfiguration;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.PacketListener' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using PacketListener = org.jivesoftware.smack.PacketListener;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.XMPPConnection' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using XMPPConnection = org.jivesoftware.smack.XMPPConnection;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.XMPPException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using XMPPException = org.jivesoftware.smack.XMPPException;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.filter.AndFilter' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using AndFilter = org.jivesoftware.smack.filter.AndFilter;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.filter.FromContainsFilter' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using FromContainsFilter = org.jivesoftware.smack.filter.FromContainsFilter;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.filter.IQTypeFilter' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using IQTypeFilter = org.jivesoftware.smack.filter.IQTypeFilter;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.filter.MessageTypeFilter' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MessageTypeFilter = org.jivesoftware.smack.filter.MessageTypeFilter;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.filter.PacketFilter' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using PacketFilter = org.jivesoftware.smack.filter.PacketFilter;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.filter.PacketTypeFilter' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using PacketTypeFilter = org.jivesoftware.smack.filter.PacketTypeFilter;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.packet.IQ' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using IQ = org.jivesoftware.smack.packet.IQ;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.packet.Message' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Message = org.jivesoftware.smack.packet.Message;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.packet.Packet' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Packet = org.jivesoftware.smack.packet.Packet;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.packet.PacketExtension' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using PacketExtension = org.jivesoftware.smack.packet.PacketExtension;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.packet.Presence' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Presence = org.jivesoftware.smack.packet.Presence;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.packet.XMPPError' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using XMPPError = org.jivesoftware.smack.packet.XMPPError;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.util.StringUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using StringUtils = org.jivesoftware.smack.util.StringUtils;
//UPGRADE_TODO: The type 'org.jivesoftware.smackx.Form' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Form = org.jivesoftware.smackx.Form;
//UPGRADE_TODO: The type 'org.jivesoftware.smackx.muc.DefaultParticipantStatusListener' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DefaultParticipantStatusListener = org.jivesoftware.smackx.muc.DefaultParticipantStatusListener;
//UPGRADE_TODO: The type 'org.jivesoftware.smackx.muc.DefaultUserStatusListener' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DefaultUserStatusListener = org.jivesoftware.smackx.muc.DefaultUserStatusListener;
//UPGRADE_TODO: The type 'org.jivesoftware.smackx.muc.HostedRoom' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using HostedRoom = org.jivesoftware.smackx.muc.HostedRoom;
//UPGRADE_TODO: The type 'org.jivesoftware.smackx.muc.InvitationListener' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using InvitationListener = org.jivesoftware.smackx.muc.InvitationListener;
//UPGRADE_TODO: The type 'org.jivesoftware.smackx.muc.MultiUserChat' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MultiUserChat = org.jivesoftware.smackx.muc.MultiUserChat;
//UPGRADE_TODO: The type 'org.jivesoftware.smackx.muc.ParticipantStatusListener' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ParticipantStatusListener = org.jivesoftware.smackx.muc.ParticipantStatusListener;
//UPGRADE_TODO: The type 'org.jivesoftware.smackx.muc.UserStatusListener' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using UserStatusListener = org.jivesoftware.smackx.muc.UserStatusListener;
//UPGRADE_TODO: The type 'org.jivesoftware.smackx.packet.DiscoverItems' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DiscoverItems = org.jivesoftware.smackx.packet.DiscoverItems;
//UPGRADE_TODO: The type 'org.jivesoftware.smackx.packet.MUCUser' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MUCUser = org.jivesoftware.smackx.packet.MUCUser;
//UPGRADE_TODO: The type 'org.jivesoftware.smackx.packet.VCard' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using VCard = org.jivesoftware.smackx.packet.VCard;
using GameModule = VassalSharp.build.GameModule;
using LockableChatServerConnection = VassalSharp.chat.LockableChatServerConnection;
using LockableRoom = VassalSharp.chat.LockableRoom;
using Player = VassalSharp.chat.Player;
using PlayerEncoder = VassalSharp.chat.PlayerEncoder;
using PrivateChatEncoder = VassalSharp.chat.PrivateChatEncoder;
using PrivateChatManager = VassalSharp.chat.PrivateChatManager;
using Room = VassalSharp.chat.Room;
using ServerStatus = VassalSharp.chat.ServerStatus;
using SimpleStatus = VassalSharp.chat.SimpleStatus;
using SoundEncoder = VassalSharp.chat.SoundEncoder;
using SynchEncoder = VassalSharp.chat.SynchEncoder;
using MessageBoard = VassalSharp.chat.messageboard.MessageBoard;
using ChatControlsInitializer = VassalSharp.chat.ui.ChatControlsInitializer;
using ChatServerControls = VassalSharp.chat.ui.ChatServerControls;
using InviteAction = VassalSharp.chat.ui.InviteAction;
using KickAction = VassalSharp.chat.ui.KickAction;
using LockableRoomTreeRenderer = VassalSharp.chat.ui.LockableRoomTreeRenderer;
using PrivateMessageAction = VassalSharp.chat.ui.PrivateMessageAction;
using RoomInteractionControlsInitializer = VassalSharp.chat.ui.RoomInteractionControlsInitializer;
using SendSoundAction = VassalSharp.chat.ui.SendSoundAction;
using ShowProfileAction = VassalSharp.chat.ui.ShowProfileAction;
using SimpleStatusControlsInitializer = VassalSharp.chat.ui.SimpleStatusControlsInitializer;
using SynchAction = VassalSharp.chat.ui.SynchAction;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using Resources = VassalSharp.i18n.Resources;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using DeobfuscatingInputStream = VassalSharp.tools.io.DeobfuscatingInputStream;
using FastByteArrayOutputStream = VassalSharp.tools.io.FastByteArrayOutputStream;
using IOUtils = VassalSharp.tools.io.IOUtils;
using ObfuscatingOutputStream = VassalSharp.tools.io.ObfuscatingOutputStream;
using Dialogs = VassalSharp.tools.swing.Dialogs;
namespace VassalSharp.chat.jabber
{
	
	public class JabberClient : LockableChatServerConnection, PacketListener, ServerStatus, ChatControlsInitializer, PlayerEncoder
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(JabberClient enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(JabberClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private JabberClient enclosingInstance;
			public JabberClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				if (Enclosing_Instance.me != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SimpleStatus s = (SimpleStatus) Enclosing_Instance.me.getStatus();
					s.updateStatus();
					Enclosing_Instance.me.setStatus(s);
					Enclosing_Instance.me.setName((System.String) GameModule.getGameModule().getPrefs().getValue(GameModule.REAL_NAME));
				}
				if (Enclosing_Instance.monitor != null)
				{
					Enclosing_Instance.monitor.sendStatus(Enclosing_Instance.me);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDefaultUserStatusListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDefaultUserStatusListener : DefaultUserStatusListener
		{
			public AnonymousClassDefaultUserStatusListener(JabberClient enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(JabberClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private JabberClient enclosingInstance;
			public JabberClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  kicked(System.String kicker, System.String reason)
			{
				fireStatus(Resources.getString("Chat.kicked", Enclosing_Instance.getRoom().getName())); //$NON-NLS-1$
				Enclosing_Instance.setRoom(Enclosing_Instance.defaultRoom);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassInvitationListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassInvitationListener : InvitationListener
		{
			public AnonymousClassInvitationListener(JabberClient enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(JabberClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private JabberClient enclosingInstance;
			public JabberClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  invitationReceived(XMPPConnection conn, System.String room, System.String inviter, System.String reason, System.String password, Message mess)
			{
				if (VassalSharp.chat.jabber.JabberClient.INVITE.Equals(reason))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'playerLogin '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String playerLogin = inviter.split("@")[0]; //$NON-NLS-1$
					//UPGRADE_NOTE: Final was removed from the declaration of 'player '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Player player = Enclosing_Instance.playerMgr.getPlayer(inviter + VassalSharp.chat.jabber.JabberClient.JID_RESOURCE);
					//UPGRADE_NOTE: Final was removed from the declaration of 'playerName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String playerName = player.getName() + "(" + playerLogin + ")"; //$NON-NLS-1$ //$NON-NLS-2$
					//UPGRADE_NOTE: Final was removed from the declaration of 'roomName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String roomName = Enclosing_Instance.roomMgr.getRoomByJID(Enclosing_Instance, room).getName();
					//UPGRADE_NOTE: Final was removed from the declaration of 'i '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int i = Dialogs.showConfirmDialog(GameModule.getGameModule().getFrame(), Resources.getString("Chat.invite_heading"), Resources.getString("Chat.invite_heading"), Resources.getString("Chat.invitation", playerName, roomName), (int) System.Windows.Forms.MessageBoxIcon.Question, null, (int) System.Windows.Forms.MessageBoxButtons.YesNo, "Invite" + inviter, Resources.getString("Chat.ignore_invitation")); //$NON-NLS-1$
					if (i == 0)
					{
						Enclosing_Instance.doInvite(inviter, roomName);
					}
					else
					{
						MultiUserChat.decline(conn, room, inviter, ""); //$NON-NLS-1$
					}
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDefaultParticipantStatusListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassDefaultParticipantStatusListener : DefaultParticipantStatusListener
		{
			public AnonymousClassDefaultParticipantStatusListener(JabberClient enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(JabberClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private JabberClient enclosingInstance;
			public JabberClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  kicked(System.String participant, System.String arg1, System.String arg2)
			{
				revokeMembership(participant);
			}
			public virtual void  left(System.String participant)
			{
				revokeMembership(participant);
			}
			private void  revokeMembership(System.String participant)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'room '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				LockableRoom room = Enclosing_Instance.getCurrentRoom();
				if (room.Locked && room.isOwner(Enclosing_Instance.me.Jid))
				{
					try
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'jid '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String jid = JabberPlayer.xmppAddressToJid(participant);
						Enclosing_Instance.currentChat.revokeMembership(Enclosing_Instance.playerMgr.getPlayer(jid).getId());
					}
					catch (XMPPException e)
					{
						// TODO Auto-generated catch block
						e.printStackTrace();
					}
				}
			}
		}
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: Constructor 'java.beans.PropertyChangeSupport.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			propSupport = new PropertyChangeSupport(this);
		}
		[System.ComponentModel.Browsable(true)]
		public  event SupportClass.PropertyChangeEventHandler PropertyChange;
		virtual public System.String CurrentRoomJID
		{
			get
			{
				return currentChat == null?null:currentChat.getRoom();
			}
			
		}
		virtual public Player UserInfo
		{
			get
			{
				return playerMgr.getPlayerByLogin(this, account.UserName);
			}
			
			set
			{
				if (monitor != null)
				{
					monitor.sendStatus((JabberPlayer) value);
				}
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
				return this;
			}
			
		}
		virtual public XMPPConnection Connection
		{
			get
			{
				return conn;
			}
			
		}
		virtual public System.String Module
		{
			get
			{
				return "vassal-" + GameModule.getGameModule().getGameName(); //$NON-NLS-1$
			}
			
		}
		virtual public System.String ConferenceService
		{
			get
			{
				return conferenceService;
			}
			
		}
		virtual public System.String MonitorRoomJID
		{
			get
			{
				return StringUtils.escapeNode(Module) + "@" + ConferenceService; //$NON-NLS-1$
			}
			
		}
		private const System.String QUERY_ROOMS = "http://jabber.org/protocol/muc#rooms"; //$NON-NLS-1$
		private const System.String QUERY_USER = "http://jabber.org/protocol/muc#user"; //$NON-NLS-1$
		private const System.String INVITE = "Invite"; //$NON-NLS-1$
		private const System.String REAL_NAME = "realName"; //$NON-NLS-1$
		public const System.String JID_RESOURCE = "/VASSAL"; //$NON-NLS-1$
		public const System.String ROOM_CONFIG = "roomConfig"; //$NON-NLS-1$
		public const System.String ROOM_JID = "roomJid"; //$NON-NLS-1$
		public const System.String ROOM_NAME = "roomName"; //$NON-NLS-1$
		public const System.String OWNER = "owner"; //$NON-NLS-1$
		
		private MessageBoard msgSvr;
		private XMPPConnection conn;
		private System.String host;
		private int port = 5222;
		//UPGRADE_ISSUE: Class 'java.beans.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
		//UPGRADE_NOTE: The initialization of  'propSupport' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private PropertyChangeSupport propSupport;
		private JabberPlayer me;
		private System.String conferenceService;
		private MonitorRooms monitor;
		private CommandEncoder encoder;
		//UPGRADE_NOTE: Final was removed from the declaration of 'defaultRoom '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private JabberRoom defaultRoom;
		private MultiUserChat currentChat;
		private AccountInfo account;
		private SynchEncoder synchEncoder;
		protected internal SoundEncoder soundEncoder;
		protected internal PrivateChatEncoder privateChatEncoder;
		// protected MessageBoardControlsInitializer messageBoardControls;
		protected internal RoomInteractionControlsInitializer roomControls;
		// protected ServerStatusControlsInitializer serverStatusControls;
		protected internal SimpleStatusControlsInitializer playerStatusControls;
		protected internal JabberPlayer.Manager playerMgr = new JabberPlayer.Manager();
		protected internal JabberRoom.Manager roomMgr = new JabberRoom.Manager();
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		protected internal PropertyChangeListener idChangeListener;
		protected internal UserStatusListener kickListener;
		protected internal InvitationListener inviteListener;
		protected internal ParticipantStatusListener userListener;
		
		public JabberClient(CommandEncoder encoder, System.String host, int port, AccountInfo account)
		{
			InitBlock();
			//UPGRADE_ISSUE: Method 'java.lang.System.getProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
			XMPPConnection.DEBUG_ENABLED = "true".Equals(System_Renamed.getProperty("debugJabber"));
			this.host = host;
			this.conferenceService = "conference." + host; //$NON-NLS-1$
			this.encoder = encoder;
			this.account = account;
			defaultRoom = roomMgr.getRoomByName(this, VassalSharp.chat.ChatServerConnection_Fields.DEFAULT_ROOM_NAME);
			// messageBoardControls = new MessageBoardControlsInitializer(Resources.getString("Chat.messages"), msgSvr); //$NON-NLS-1$
			roomControls = new LockableJabberRoomControls(this);
			roomControls.addPlayerActionFactory(ShowProfileAction.factory());
			roomControls.addPlayerActionFactory(SynchAction.factory(this));
			//UPGRADE_NOTE: Final was removed from the declaration of 'privateChatManager '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PrivateChatManager privateChatManager = new PrivateChatManager(this);
			roomControls.addPlayerActionFactory(PrivateMessageAction.factory(this, privateChatManager));
			roomControls.addPlayerActionFactory(SendSoundAction.factory(this, Resources.getString("Chat.send_wakeup"), "wakeUpSound", "phone1.wav")); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
			roomControls.addPlayerActionFactory(InviteAction.factory(this));
			roomControls.addPlayerActionFactory(KickAction.factory(this));
			// serverStatusControls = new ServerStatusControlsInitializer(serverStatus);
			playerStatusControls = new SimpleStatusControlsInitializer(this);
			synchEncoder = new SynchEncoder(this, this);
			soundEncoder = new SoundEncoder(this);
			privateChatEncoder = new PrivateChatEncoder(this, privateChatManager);
			
			// Listen for changes to our name via VASSAL preferences
			idChangeListener = new AnonymousClassPropertyChangeListener(this);
			
			// Listen for someone kicking us from the current room
			kickListener = new AnonymousClassDefaultUserStatusListener(this);
			
			// Listen for someone inviting us to another room
			inviteListener = new AnonymousClassInvitationListener(this);
			
			// Listen for other clients leaving a room I own and revoke their membership
			userListener = new AnonymousClassDefaultParticipantStatusListener(this);
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
		
		public virtual bool isConnected()
		{
			return conn != null && conn.isConnected();
		}
		
		public virtual void  sendToOthers(Command c)
		{
			if (currentChat != null)
			{
				try
				{
					currentChat.sendMessage(encodeMessage(encoder.encode(c)));
				}
				// FIXME: review error message
				catch (XMPPException e)
				{
					reportXMPPException(e);
				}
			}
		}
		
		public virtual void  setConnected(bool connect)
		{
			if (connect)
			{
				if (!isConnected())
				{
					if (conn != null)
					{
						conn.disconnect();
					}
					try
					{
						playerMgr.clear();
						roomMgr.clear();
						System.String username = account.UserName;
						System.String password = account.Password;
						me = playerMgr.getPlayerByLogin(this, account.UserName);
						
						//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						GameModule g = GameModule.getGameModule();
						//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						SimpleStatus s = (SimpleStatus) me.getStatus();
						s.updateStatus();
						me.setStatus(s);
						me.setName((System.String) g.getPrefs().getValue(GameModule.REAL_NAME));
						
						ConnectionConfiguration config = new ConnectionConfiguration(host, port);
						config.setCompressionEnabled(true);
						config.setDebuggerEnabled(XMPPConnection.DEBUG_ENABLED);
						config.setReconnectionAllowed(true);
						
						conn = new XMPPConnection(config);
						conn.connect();
						conn.addConnectionListener(new ConnectionListener());
						try
						{
							conn.login(username, password, "VASSAL"); //$NON-NLS-1$
						}
						// FIXME: review error message
						catch (XMPPException e)
						{
							// Create the account if it doesn't exist
							//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
							Map < String, String > attributes = new HashMap < String, String >();
							attributes.put("name", me.getName()); //$NON-NLS-1$
							try
							{
								conn.getAccountManager().createAccount(username, password, attributes);
							}
							// FIXME: review error message
							catch (XMPPException createAccountError)
							{
								if (createAccountError.getXMPPError() != null && createAccountError.getXMPPError().getCode() == 409)
								{
									// Account already exists. Password is incorrect
									fireStatus(Resources.getString("Chat.invalid_password", username)); //$NON-NLS-1$
									setConnected(false);
									return ;
								}
								else
								{
									setConnected(false);
									throw createAccountError;
								}
							}
							
							// ejabberd servers require a reconnection after an account creation before
							// they will allow the user to login
							conn.disconnect();
							conn.connect();
							
							// Retry the login
							try
							{
								conn.login(username, password, "VASSAL"); //$NON-NLS-1$
							}
							catch (XMPPException retryError)
							{
								setConnected(false);
								throw retryError;
							}
							VCard c = new VCard();
							c.setNickName(me.getName());
							c.save(conn);
						}
						monitor = new MonitorRooms(this);
						monitor.init();
						SupportClass.PropertyChangingEventArgs me4 = new SupportClass.PropertyChangingEventArgs(VassalSharp.build.module.ServerConnection_Fields.CONNECTED, null, true);
						if (PropertyChange != null)
							PropertyChange(this, me4);
						fireStatus(Resources.getString("Server.connected", host + ":" + port)); //$NON-NLS-1$ //$NON-NLS-2$
						setRoom(defaultRoom);
						GameModule.getGameModule().addIdChangeListener(idChangeListener);
						MultiUserChat.addInvitationListener(conn, inviteListener);
					}
					// FIXME: review error message
					catch (XMPPException e)
					{
						reportXMPPException(e);
						if (e.getWrappedThrowable() != null && e.getWrappedThrowable().getLocalizedMessage() != null)
						{
							fireStatus(e.getWrappedThrowable().getMessage());
						}
						setConnected(false);
					}
				}
			}
			else
			{
				if (isConnected())
				{
					leaveCurrentRoom();
					if (monitor != null)
					{
						monitor.disconnect();
					}
					conn.disconnect();
					tidyConnection();
				}
			}
		}
		
		private void  tidyConnection()
		{
			conn = null;
			monitor = null;
			currentChat = null;
			SupportClass.PropertyChangingEventArgs me5 = new SupportClass.PropertyChangingEventArgs(VassalSharp.build.module.ServerConnection_Fields.CONNECTED, null, false);
			if (PropertyChange != null)
				PropertyChange(this, me5);
			playerMgr.clear();
			roomMgr.clear();
			fireStatus(Resources.getString("Server.disconnected", host + ":" + port)); //$NON-NLS-1$ //$NON-NLS-2$
		}
		
		private void  leaveCurrentRoom()
		{
			if (currentChat != null)
			{
				currentChat.leave();
				currentChat.removeMessageListener(this);
				currentChat.removeUserStatusListener(kickListener);
				currentChat.removeParticipantStatusListener(userListener);
				currentChat = null;
			}
		}
		
		public virtual void  initializeControls(ChatServerControls controls)
		{
			playerStatusControls.initializeControls(controls);
			// messageBoardControls.initializeControls(controls);
			roomControls.initializeControls(controls);
			// serverStatusControls.initializeControls(controls);
			controls.RoomControlsVisible = true;
			GameModule.getGameModule().addCommandEncoder(synchEncoder);
			GameModule.getGameModule().addCommandEncoder(privateChatEncoder);
			GameModule.getGameModule().addCommandEncoder(soundEncoder);
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetCellRenderer_javaxswingtreeTreeCellRenderer'"
			controls.RoomTree.setCellRenderer(new LockableRoomTreeRenderer());
		}
		
		public virtual void  uninitializeControls(ChatServerControls controls)
		{
			// messageBoardControls.uninitializeControls(controls);
			roomControls.uninitializeControls(controls);
			playerStatusControls.uninitializeControls(controls);
			// serverStatusControls.uninitializeControls(controls);
			GameModule.getGameModule().removeCommandEncoder(synchEncoder);
			GameModule.getGameModule().removeCommandEncoder(privateChatEncoder);
			GameModule.getGameModule().removeCommandEncoder(soundEncoder);
		}
		
		public virtual void  processPacket(Packet packet)
		{
			Message m = (Message) packet;
			if (!m.getFrom().equals(currentChat.getRoom() + "/" + currentChat.getNickname()))
			{
				//$NON-NLS-1$
				SupportClass.PropertyChangingEventArgs me6 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.INCOMING_MSG, null, decodeMessage(m.getBody()));
				if (PropertyChange != null)
					PropertyChange(this, me6);
			}
		}
		
		public virtual void  processServerMessage(System.String subject, System.String message)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GameModule g = GameModule.getGameModule();
			g.warn("##### " + Resources.getString("JabberClient.message_from_admin", host + ":" + port)); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
			if (subject != null)
			{
				g.warn(Resources.getString("JabberClient.subject") + subject); //$NON-NLS-1$
			}
			g.warn(message);
			g.warn("##### " + Resources.getString("JabberClient.end_message")); //$NON-NLS-1$ //$NON-NLS-2$
		}
		
		public virtual Room getRoom()
		{
			return monitor.getCurrentRoom();
		}
		
		public virtual JabberRoom getRoomByName(System.String name)
		{
			return roomMgr.getRoomByName(this, name);
		}
		
		public virtual LockableRoom getCurrentRoom()
		{
			return monitor.getCurrentRoom();
		}
		
		public virtual void  setRoom(System.String roomName)
		{
			setRoom(roomMgr.getRoomByName(this, roomName));
		}
		
		public virtual void  setRoom(Room r)
		{
			JabberRoom newRoom = null;
			try
			{
				if (r is JabberRoom)
				{
					newRoom = (JabberRoom) r;
				}
				else
				{
					newRoom = roomMgr.getRoomByName(this, r.getName());
				}
				if (!newRoom.Equals(getRoom()))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'failedToJoinMessage '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String failedToJoinMessage = newRoom.canJoin(me);
					if (failedToJoinMessage != null)
					{
						fireStatus(Resources.getString("Chat.failed_to_join", newRoom.getName(), failedToJoinMessage)); //$NON-NLS-1$
						return ;
					}
					leaveCurrentRoom();
					currentChat = newRoom.join(this, (JabberPlayer) UserInfo);
					if (newRoom.OwnedByMe)
					{
						currentChat.addParticipantStatusListener(userListener);
					}
					fireStatus(Resources.getString("Chat.joined_room", newRoom.getName())); //$NON-NLS-1$
					if (!newRoom.OwnedByMe && !isDefaultRoom(newRoom))
					{
						//UPGRADE_TODO: Method 'javax.swing.AbstractAction.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
						new SynchAction(newRoom.OwningPlayer, this).actionPerformed(null);
						GameModule.getGameModule().warn(Resources.getString("Chat.synchronize_complete")); //$NON-NLS-1$
					}
					else
					{
						SynchAction.clearSynchRoom();
					}
					currentChat.addUserStatusListener(kickListener);
					monitor.sendRoomChanged();
					monitor.sendStatus(me, newRoom);
				}
			}
			// FIXME: review error message
			catch (XMPPException e)
			{
				reportXMPPException(e);
				System.String mess = null;
				if (e.getXMPPError() != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'error '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					XMPPError error = e.getXMPPError();
					if (error.getCode() == 407)
					{
						mess = Resources.getString("Chat.not_a_member"); //$NON-NLS-1$
					}
					else
					{
						mess = e.getXMPPError().getMessage();
						if (mess == null)
						{
							mess = e.getXMPPError().getCondition();
						}
					}
				}
				else
				{
					mess = e.getMessage();
				}
				fireStatus(Resources.getString("Chat.failed_to_join", newRoom.getName(), mess)); //$NON-NLS-1$
			}
		}
		
		public virtual Room[] getAvailableRooms()
		{
			return monitor.getAvailableRooms();
		}
		
		protected internal virtual void  fireRoomsUpdated()
		{
			SupportClass.PropertyChangingEventArgs me7 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.AVAILABLE_ROOMS, null, getAvailableRooms());
			if (PropertyChange != null)
				PropertyChange(this, me7);
			SupportClass.PropertyChangingEventArgs me8 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.ROOM, null, getRoom());
			if (PropertyChange != null)
				PropertyChange(this, me8);
		}
		
		protected internal virtual void  fireStatus(System.String msg)
		{
			SupportClass.PropertyChangingEventArgs me9 = new SupportClass.PropertyChangingEventArgs(VassalSharp.chat.ChatServerConnection_Fields.STATUS, null, msg);
			if (PropertyChange != null)
				PropertyChange(this, me9);
		}
		
		public virtual System.String getDefaultRoomName()
		{
			return defaultRoom.getName();
		}
		
		public virtual bool isDefaultRoom(Room r)
		{
			return r == null?false:r.getName().Equals(getDefaultRoomName());
		}
		
		public virtual void  sendTo(Player recipient, Command c)
		{
			Chat chat = conn.getChatManager().createChat(((JabberPlayer) recipient).Jid, null);
			try
			{
				chat.sendMessage(encodeMessage(encoder.encode(c)));
			}
			// FIXME: review error message
			catch (XMPPException e)
			{
				reportXMPPException(e);
			}
		}
		
		
		/// <summary>Can a player be invited to this room? </summary>
		public virtual bool isInvitable(Player invitee)
		{
			// invitee is not me
			if (!invitee.Equals(me))
			{
				// invitee is in a different room
				//UPGRADE_NOTE: Final was removed from the declaration of 'room '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				JabberRoom room = monitor.getCurrentRoom();
				if (!room.contains(invitee))
				{
					// I own the current room and it is locked
					if (room.OwnedByMe && room.Locked)
					{
						return true;
					}
				}
			}
			return false;
		}
		
		/// <summary>Send invitation to player </summary>
		public virtual void  sendInvite(Player invitee)
		{
			try
			{
				currentChat.grantMembership(invitee.getId());
			}
			catch (XMPPException e)
			{
				ErrorDialog.bug(new Throwable("Unable to grant membership to room " + getCurrentRoom().getName() + " to player " + invitee.getId(), e)); //$NON-NLS-1$ //$NON-NLS-2$
			}
			currentChat.invite(((JabberPlayer) invitee).RawJid, INVITE); //$NON-NLS-1$
		}
		
		/// <summary>Process an invitation </summary>
		public virtual void  doInvite(System.String playerId, System.String roomName)
		{
			setRoom(roomName);
		}
		
		/// <summary>Is a player kickable from this room? </summary>
		public virtual bool isKickable(Player kickee)
		{
			// kickee is not me
			if (!kickee.Equals(me))
			{
				// kickee is in this room
				//UPGRADE_NOTE: Final was removed from the declaration of 'room '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				JabberRoom room = monitor.getCurrentRoom();
				if (room.contains(kickee))
				{
					// I own the current room and it is locked
					if (room.OwnedByMe && room.Locked)
					{
						return true;
					}
				}
			}
			return false;
		}
		
		/// <summary>Kick a player from this room </summary>
		public virtual void  doKick(Player kickee)
		{
			try
			{
				currentChat.kickParticipant(((JabberPlayer) kickee).LoginName, ""); //$NON-NLS-1$
			}
			catch (XMPPException e)
			{
				// TODO Error - unable to kick, I must not be owner???
				e.printStackTrace();
			}
		}
		
		private void  reportXMPPException(XMPPException e)
		{
			e.printStackTrace();
		}
		
		public static System.String unescapeNode(System.String node)
		{
			return StringUtils.unescapeNode(node);
		}
		
		/// <summary> Messages must be encoded to pass through Jabber:
		/// 1. To remove Escape characters (Vassal sub-command separator)
		/// 2. To hide the raw Vassal commands from observers on the chat room using a Jabber Client.
		/// 
		/// </summary>
		/// <param name="clearText">
		/// </param>
		/// <returns> encoded text
		/// </returns>
		protected internal virtual System.String encodeMessage(System.String clearText)
		{
			ObfuscatingOutputStream out_Renamed = null;
			//UPGRADE_NOTE: Final was removed from the declaration of 'ba '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FastByteArrayOutputStream ba = new FastByteArrayOutputStream();
			
			try
			{
				out_Renamed = new ObfuscatingOutputStream(ba);
				//UPGRADE_TODO: Method 'java.lang.String.getBytes' was converted to 'System.Text.Encoding.GetEncoding(string).GetBytes(string)' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangStringgetBytes_javalangString'"
				out_Renamed.Write(SupportClass.ToByteArray(SupportClass.ToSByteArray(System.Text.Encoding.GetEncoding("UTF-8").GetBytes(clearText)))); //$NON-NLS-1$
			}
			catch (System.IO.IOException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
			}
			catch (System.IO.IOException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
			}
			finally
			{
				IOUtils.closeQuietly(out_Renamed);
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'encodedText '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String encodedText = new System.String(SupportClass.ToCharArray(SupportClass.ToByteArray(SupportClass.ToSByteArray(ba.ToArray()))));
			IOUtils.closeQuietly(ba);
			return encodedText;
		}
		
		/// <summary> Encode text encoded by encodeMessage
		/// 
		/// </summary>
		/// <param name="encodedMessage">
		/// </param>
		/// <returns> decoded text
		/// </returns>
		protected internal virtual System.String decodeMessage(System.String encodedMessage)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'ba '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.MemoryStream ba = new System.IO.MemoryStream(SupportClass.ToByteArray(encodedMessage));
			DeobfuscatingInputStream in_Renamed = null;
			System.String clearText = "";
			
			try
			{
				in_Renamed = new DeobfuscatingInputStream(ba);
				clearText = IOUtils.toString(in_Renamed, "UTF-8"); //$NON-NLS-1$
			}
			catch (System.IO.IOException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
			}
			finally
			{
				IOUtils.closeQuietly(ba);
				IOUtils.closeQuietly(in_Renamed);
			}
			
			return clearText;
		}
		
		/// <summary> Toggle the lock state on the room.</summary>
		public virtual void  lockRoom(LockableRoom r)
		{
			if (r is JabberRoom)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'room '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				JabberRoom room = (JabberRoom) r;
				room.toggleLock(currentChat);
				try
				{
					monitor.sendRoomChanged();
				}
				catch (XMPPException e)
				{
					// Ignore errors - we don't want to know at this point
				}
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MonitorRooms' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> VASSAL clients join a common room, named for the module, from which they communicate information about which
		/// players have joined which rooms, etc.
		/// 
		/// </summary>
		/// <author>  rodneykinney
		/// 
		/// </author>
		private class MonitorRooms : PacketListener, ParticipantStatusListener
		{
			public MonitorRooms(JabberClient enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(JabberClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private JabberClient enclosingInstance;
			public JabberClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const System.String ROOM_CHANGE_ACTION = "changedRoom"; //$NON-NLS-1$
			private MultiUserChat monitorRoom;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private Comparator < Room > roomSortOrder = new Comparator < Room >()
			public virtual int compare(Room o1, Room o2)
			{
				if (o1.Equals(Enclosing_Instance.defaultRoom) && !o2.Equals(Enclosing_Instance.defaultRoom))
				{
					return - 1;
				}
				else if (o2.Equals(Enclosing_Instance.defaultRoom) && !o1.Equals(Enclosing_Instance.defaultRoom))
				{
					return 1;
				}
				else
				{
					return String.CompareOrdinal(o1.getName(), o2.getName());
				}
			}
		}
		
		
		public virtual void  init()
		{
			new TrackRooms(this).addTo(conn);
			new TrackStatus(this, MonitorRoomJID.ToLower()).addTo(conn);
			new ListenForChat(this).addTo(conn);
			monitorRoom = new MultiUserChat(conn, MonitorRoomJID);
			monitorRoom.addMessageListener(this);
			monitorRoom.addParticipantStatusListener(this);
			monitorRoom.join(StringUtils.parseName(conn.getUser()));
			try
			{
				// This is necessary to create the room if it doesn't already exist
				monitorRoom.sendConfigurationForm(new Form(Form.TYPE_SUBMIT));
			}
			catch (XMPPException ex)
			{
				// 403 code means the room already exists and user is not an owner
				if (ex.getXMPPError().getCode() != 403)
				{
					throw ex;
				}
			}
			sendStatus(me);
		}
		
		protected internal virtual void  sendStatus(JabberPlayer p)
		{
			sendStatus(p, null, p.JoinedRoom);
		}
		
		protected internal virtual void  sendStatus(JabberPlayer player, JabberRoom room)
		{
			sendStatus(player, null, room);
		}
		
		protected internal virtual void  sendStatus(JabberPlayer player, System.String recipient, JabberRoom room)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SimpleStatus s = (SimpleStatus) player.getStatus();
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Presence p = new Presence(Presence.Type.available);
			p.setStatus(""); //$NON-NLS-1$
			p.setMode(Presence.Mode.chat);
			p.setProperty(SimpleStatus.LOOKING, s.Looking);
			p.setProperty(SimpleStatus.AWAY, s.Away);
			p.setProperty(SimpleStatus.IP, s.Ip);
			p.setProperty(SimpleStatus.CLIENT, s.Client);
			p.setProperty(SimpleStatus.MODULE_VERSION, s.ModuleVersion);
			p.setProperty(SimpleStatus.CRC, s.Crc);
			p.setProperty(REAL_NAME, player.getName()); //$NON-NLS-1$
			if (room != null)
			{
				p.setProperty(ROOM_CONFIG, room.encodeConfig());
				p.setProperty(ROOM_JID, room.JID);
				p.setProperty(ROOM_NAME, room.getName());
			}
			p.setTo(recipient == null?monitorRoom.getRoom():recipient);
			conn.sendPacket(p);
		}
		
		public virtual Room[] getAvailableRooms()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Map < JabberRoom, List < JabberPlayer >> occupants = new HashMap < JabberRoom, List < JabberPlayer >>();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(JabberPlayer p: playerMgr.getAllPlayers())
			{
				JabberRoom room = p.getJoinedRoom();
				if (room != null)
				{
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
					if (l == null)
					{
						//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
						l = new ArrayList < JabberPlayer >();
						occupants.put(room, l);
					}
					l.add(p);
				}
			}
			if (!occupants.containsKey(defaultRoom))
			{
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				occupants.put(defaultRoom, l);
			}
			//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(JabberRoom room: rooms)
			{
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				room.setPlayers(l.toArray(new JabberPlayer[l.size()]));
			}
			Room[] roomArray = rooms.toArray(new Room[rooms.size()]);
			Arrays.sort(roomArray, roomSortOrder);
			return roomArray;
		}
		
		public virtual JabberRoom getCurrentRoom()
		{
			System.String jid = CurrentRoomJID;
			return roomMgr.getRoomByJID(this, jid);
		}
		
		public virtual void  sendRoomChanged()
		{
			Message m = monitorRoom.createMessage();
			m.setBody(ROOM_CHANGE_ACTION);
			monitorRoom.sendMessage(m);
		}
		
		public virtual void  disconnect()
		{
			monitorRoom.leave();
		}
		
		/// <summary> Take the room-local JID for a player (room@conference.server/nick) and change it into an absolute address for
		/// that player (login@server/VASSAL)
		/// 
		/// </summary>
		/// <param name="jid">
		/// </param>
		/// <returns>
		/// </returns>
		public virtual System.String getAbsolutePlayerJID(System.String jid)
		{
			return StringUtils.parseResource(jid) + "@" + host + JID_RESOURCE; //$NON-NLS-1$
		}
		
		private void  sendRoomQuery(System.String jid)
		{
			DiscoverItems disco = new DiscoverItems();
			disco.setType(IQ.Type.GET);
			disco.setTo(jid);
			disco.setNode(QUERY_ROOMS);
			conn.sendPacket(disco);
		}
		
		public virtual void  processPacket(Packet packet)
		{
			Message m = (Message) packet;
			if (ROOM_CHANGE_ACTION.equals(m.getBody()))
			{
				System.String jid = getAbsolutePlayerJID(packet.getFrom());
				playerMgr.getPlayer(getAbsolutePlayerJID(packet.getFrom()));
				sendRoomQuery(jid);
			}
		}
		
		public virtual void  joined(System.String participant)
		{
			playerMgr.getPlayer(getAbsolutePlayerJID(participant));
		}
		
		public virtual void  left(System.String participant)
		{
			System.String jid = getAbsolutePlayerJID(participant);
			playerMgr.deletePlayer(jid);
			fireRoomsUpdated();
		}
		
		public virtual void  kicked(System.String participant, System.String actor, System.String reason)
		{
		}
		
		public virtual void  voiceGranted(System.String participant)
		{
		}
		
		public virtual void  voiceRevoked(System.String participant)
		{
		}
		
		public virtual void  banned(System.String participant, System.String actor, System.String reason)
		{
		}
		
		public virtual void  membershipGranted(System.String participant)
		{
		}
		
		public virtual void  membershipRevoked(System.String participant)
		{
		}
		
		public virtual void  moderatorGranted(System.String participant)
		{
		}
		
		public virtual void  moderatorRevoked(System.String participant)
		{
		}
		
		public virtual void  ownershipGranted(System.String participant)
		{
		}
		
		public virtual void  ownershipRevoked(System.String participant)
		{
		}
		
		public virtual void  adminGranted(System.String participant)
		{
		}
		
		public virtual void  adminRevoked(System.String participant)
		{
		}
		
		public virtual void  nicknameChanged(System.String participant, System.String newNickname)
		{
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'TrackStatus' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class TrackStatus:PacketProcessor
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPacketFilter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			internal class AnonymousClassPacketFilter : PacketFilter
			{
				public AnonymousClassPacketFilter(TrackStatus enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(TrackStatus enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private TrackStatus enclosingInstance;
				public TrackStatus Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual bool accept(Packet packet)
				{
					bool accept = false;
					if (packet is Presence)
					{
						Presence p = (Presence) packet;
						if (p.getType() == Presence.Type.available && p.getMode() == Presence.Mode.chat)
						{
							System.String name = p.getFrom();
							if (name != null && name.ToLower().StartsWith(Enclosing_Instance.prefix))
							{
								accept = true;
							}
						}
					}
					return accept;
				}
			}
			private void  InitBlock(JabberClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				changeStatusFilter = new AnonymousClassPacketFilter(this);
			}
			private JabberClient enclosingInstance;
			public JabberClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			internal System.String prefix;
			
			//UPGRADE_NOTE: The initialization of  'changeStatusFilter' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private PacketFilter changeStatusFilter;
			
			public TrackStatus(JabberClient enclosingInstance, System.String prefix)
			{
				InitBlock(enclosingInstance);
				this.prefix = prefix;
			}
			
			public virtual bool acceptPacket(Packet packet)
			{
				return packet is Presence;
			}
			
			public virtual void  process(Packet packet)
			{
				// Process a change of status by another user
				if (changeStatusFilter.accept(packet))
				{
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Presence p = (Presence) packet;
					//UPGRADE_NOTE: Final was removed from the declaration of 'player '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					JabberPlayer player = Enclosing_Instance.playerMgr.getPlayer(getAbsolutePlayerJID(p.getFrom()));
					SimpleStatus status = (SimpleStatus) player.getStatus();
					//UPGRADE_NOTE: Final was removed from the declaration of 'profile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String profile = status == null?"":status.Profile; //$NON-NLS-1$
					//UPGRADE_NOTE: Final was removed from the declaration of 'looking '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Object looking = p.getProperty(SimpleStatus.LOOKING);
					//UPGRADE_NOTE: Final was removed from the declaration of 'away '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Object away = p.getProperty(SimpleStatus.AWAY);
					//UPGRADE_NOTE: Final was removed from the declaration of 'ip '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String ip = p.getProperty(SimpleStatus.IP).toString();
					//UPGRADE_NOTE: Final was removed from the declaration of 'client '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String client = p.getProperty(SimpleStatus.CLIENT).toString();
					//UPGRADE_NOTE: Final was removed from the declaration of 'moduleVersion '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String moduleVersion = p.getProperty(SimpleStatus.MODULE_VERSION).toString();
					//UPGRADE_NOTE: Final was removed from the declaration of 'crc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String crc = p.getProperty(SimpleStatus.CRC).toString();
					status = new SimpleStatus(looking == null?false:(System.Boolean) looking, away == null?false:(System.Boolean) away, profile, client, ip, moduleVersion, crc);
					player.setStatus(status);
					player.setName(System.Convert.ToString(p.getProperty(VassalSharp.chat.jabber.JabberClient.REAL_NAME)));
					Enclosing_Instance.fireRoomsUpdated();
				}
				// Track room ownership
				if (packet is Presence)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Presence p = (Presence) packet;
					if (p.getType().equals(Presence.Type.available))
					{
						PacketExtension ext = p.getExtension(VassalSharp.chat.jabber.JabberClient.QUERY_USER);
						JabberRoom room = null;
						if (ext != null && ext is MUCUser)
						{
							//UPGRADE_NOTE: Final was removed from the declaration of 'affiliation '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							System.String affiliation = ((MUCUser) ext).getItem().getAffiliation();
							//UPGRADE_NOTE: Final was removed from the declaration of 'jid '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							System.String jid = Enclosing_Instance.playerMgr.getPlayer(getAbsolutePlayerJID(p.getFrom())).getJid();
							System.String roomJid = (System.String) p.getProperty(VassalSharp.chat.jabber.JabberClient.ROOM_JID);
							//UPGRADE_NOTE: Final was removed from the declaration of 'roomConfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							System.String roomConfig = (System.String) p.getProperty(VassalSharp.chat.jabber.JabberClient.ROOM_CONFIG);
							//UPGRADE_NOTE: Final was removed from the declaration of 'roomName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							System.String roomName = (System.String) p.getProperty(VassalSharp.chat.jabber.JabberClient.ROOM_NAME);
							if (roomJid == null)
							{
								roomJid = StringUtils.parseName(p.getFrom()) + "@" + Enclosing_Instance.ConferenceService; //$NON-NLS-1$
							}
							room = Enclosing_Instance.roomMgr.getRoomByJID(Enclosing_Instance, roomJid, roomName);
							
							if (room != null)
							{
								if (VassalSharp.chat.jabber.JabberClient.OWNER.Equals(affiliation))
								{
									room.addOwner(jid);
								}
								else
								{
									room.removeOwner(jid);
								}
							}
							
							if (roomConfig != null && room != null)
							{
								room.decodeConfig(roomConfig);
							}
						}
					}
				}
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'TrackRooms' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Track available rooms
		/// 
		/// </summary>
		private class TrackRooms:PacketProcessor
		{
			private void  InitBlock(JabberClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				newPlayerFilter = new AndFilter(new PacketTypeFilter(typeof(Presence)), new FromContainsFilter(Enclosing_Instance.MonitorRoomJID));
			}
			private JabberClient enclosingInstance;
			public JabberClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private PacketFilter roomResponseFilter = new AndFilter(new IQTypeFilter(IQ.Type.RESULT), new PacketTypeFilter(typeof(DiscoverItems)));
			//UPGRADE_NOTE: The initialization of  'newPlayerFilter' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private PacketFilter newPlayerFilter;
			
			public TrackRooms(JabberClient enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			
			public virtual void  process(Packet packet)
			{
				if (roomResponseFilter.accept(packet))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					DiscoverItems result = (DiscoverItems) packet;
					//UPGRADE_NOTE: Final was removed from the declaration of 'player '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					JabberPlayer player = Enclosing_Instance.playerMgr.getPlayer(packet.getFrom());
					// Collect the entityID for each returned item
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
					Enclosing_Instance.fireRoomsUpdated();
				}
				else if (newPlayerFilter.accept(packet))
				{
					sendRoomQuery(getAbsolutePlayerJID(packet.getFrom()));
				}
			}
			
			public virtual bool acceptPacket(Packet packet)
			{
				bool accept = false;
				if (roomResponseFilter.accept(packet))
				{
					accept = VassalSharp.chat.jabber.JabberClient.QUERY_ROOMS.Equals(((DiscoverItems) packet).getNode());
				}
				else if (newPlayerFilter.accept(packet))
				{
					accept = ((Presence) packet).isAvailable();
				}
				return accept;
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ListenForChat' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Listen for any Private chat and pass it on to
		/// the client. (Also Synch's, private messages and wake ups).
		/// </summary>
		private class ListenForChat:PacketProcessor
		{
			public ListenForChat(JabberClient enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(JabberClient enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private JabberClient enclosingInstance;
			public JabberClient Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			private PacketFilter chatFilter = new MessageTypeFilter(Message.Type.chat);
			private PacketFilter serverMessageFilter = new MessageTypeFilter(Message.Type.normal);
			
			protected internal virtual bool acceptPacket(Packet packet)
			{
				if (chatFilter.accept(packet))
				{
					return true;
				}
				if (serverMessageFilter.accept(packet))
				{
					return ((Message) packet).getFrom().equals(Enclosing_Instance.Connection.getHost());
				}
				return false;
			}
			
			protected internal virtual void  process(Packet packet)
			{
				if (chatFilter.accept(packet))
				{
					Enclosing_Instance.processPacket(packet);
				}
				else
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Message m = ((Message) packet);
					Enclosing_Instance.processServerMessage(m.getSubject(), m.getBody());
				}
			}
		}
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public CommandEncoder getEncoder()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return encoder;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setEncoder(CommandEncoder encoder)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		this.encoder = encoder;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public ModuleSummary [] getHistory(String timeRange)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new ModuleSummary [0];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public ModuleSummary [] getStatus()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ArrayList < ModuleSummary > entries = new ArrayList < ModuleSummary >();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		for(Iterator < HostedRoom > iter = MultiUserChat.getHostedRooms(conn, conferenceService).iterator();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	iter.hasNext();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		HostedRoom room = iter.next();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	MultiUserChat.getRoomInfo(conn, room.getJid());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	// FIXME: review error message
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(XMPPException e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		e.printStackTrace();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return entries.toArray(new ModuleSummary [entries.size()]);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String [] getSupportedTimeRanges()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new String [0];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	private class ConnectionListener : org.jivesoftware.smack.ConnectionListener
	{
		public virtual void  connectionClosed()
		{
		}
		
		public virtual void  connectionClosedOnError(System.Exception e)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.String msg = e.Message;
			if (e is XMPPException)
			{
				XMPPException xe = (XMPPException) e;
				if (xe.getStreamError() != null && "conflict".Equals(xe.getStreamError().getCode()))
				{
					//$NON-NLS-1$
					msg = Resources.getString("Server.account_in_use"); //$NON-NLS-1$
				}
			}
			if (msg != null)
			{
				fireStatus(msg);
			}
			setConnected(false);
		}
		
		public virtual void  reconnectingIn(int seconds)
		{
		}
		
		public virtual void  reconnectionFailed(System.Exception e)
		{
		}
		
		public virtual void  reconnectionSuccessful()
		{
		}
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public static
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void main(String [] args)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		XMPPConnection.DEBUG_ENABLED = true;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	CommandEncoder c = new CommandEncoder()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Command decode(String command)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		System.err.println(command);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String encode(Command c)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final String username = args.length == 0 ? test: args [0]; //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final String password = args.length == 0 ? test: args [1]; //$NON-NLS-1$
	// JabberClient client = new JabberClient(c, "63.144.41.3", 5222, username, password);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	AccountInfo account = new AccountInfo()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getPassword()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return password;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getUserName()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return username;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getModule()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return JabberTestModule;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getRealName()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return username;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	JabberClient client = new JabberClient(c, localhost, 5222, account); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	client.addPropertyChangeListener(new PropertyChangeListener()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void propertyChange(PropertyChangeEvent evt)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		System.err.println(evt.getPropertyName() + = + evt.getNewValue()); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	});
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	ChatServerControls controls = new ChatServerControls();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	controls.setClient(client);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	JFrame f = new JFrame(username);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	f.add(controls.getControls());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	f.pack();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	f.setVisible(true);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	f.addWindowListener(new WindowAdapter()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		@ Override
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void windowClosing(WindowEvent e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		System.exit(0);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	});
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String getHost()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return host;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public String playerToString(Player p)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return((JabberPlayer) p).getJid();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Player stringToPlayer(String s)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return playerMgr.getPlayer(s);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public static String testConnection(String host, String port, String login, String passwd)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final StringBuffer text = new StringBuffer(Resources.getString(JabberClient.testing_connection) + host + : + port +   + login + / + passwd).append(\n); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$ //$NON-NLS-4$ //$NON-NLS-5$
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(host.length() == 0)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return text.append(Resources.getString(JabberClient.error_no_host)).toString(); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	int portNo = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		portNo = Integer.parseInt(port);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(NumberFormatException e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return text.append(Resources.getString(JabberClient.error_port_number)).toString(); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	final ConnectionConfiguration config = new ConnectionConfiguration(host, portNo);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	config.setCompressionEnabled(true);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	config.setDebuggerEnabled(XMPPConnection.DEBUG_ENABLED);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	config.setReconnectionAllowed(true);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final XMPPConnection conn = new XMPPConnection(config);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		text.append(Resources.getString(JabberClient.attempting_to_connect)).append(\n); //$NON-NLS-1$ //$NON-NLS-2$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	conn.connect();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	text.append(Resources.getString(JabberClient.success)).append(\n); //$NON-NLS-1$ //$NON-NLS-2$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	text.append(Resources.getString(JabberClient.attempting_to_login)).append(\n); //$NON-NLS-1$ //$NON-NLS-2$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		conn.login(login, passwd, VASSAL); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	text.append(Resources.getString(JabberClient.success)); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(XMPPException e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		text.append(Resources.getString(JabberClient.login_failed)).append(\n); //$NON-NLS-1$ //$NON-NLS-2$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	text.append(Resources.getString(JabberClient.attempting_to_create)).append(\n); //$NON-NLS-1$ //$NON-NLS-2$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final Map < String, String > attributes = new HashMap < String, String >();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	attributes.put(name, GameModule.getUserId()); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		conn.getAccountManager().createAccount(login, passwd, 
		attributes);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	text.append(Resources.getString(JabberClient.success)); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(XMPPException ex)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		text.append(Resources.getString(JabberClient.failed)).append(\n); //$NON-NLS-1$ //$NON-NLS-2$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(ex.getXMPPError() != null && ex.getXMPPError().getCode() == 409)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// Account already exists. Password is incorrect
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	text.append(Resources.getString(Chat.invalid_password)).append(\n); //$NON-NLS-1$ //$NON-NLS-2$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		text.append(formatXMPPError(ex));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(XMPPException e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		text.append(Resources.getString(JabberClient.failed)).append(\n); //$NON-NLS-1$ //$NON-NLS-2$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	text.append(formatXMPPError(e));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	finally
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		conn.disconnect();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return text.toString();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private static String formatXMPPError(XMPPException e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final XMPPError error = e.getXMPPError();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(error == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return Resources.getString(Server.server_error, e.getMessage(), , ); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return Resources.getString(Server.server_error, e //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	.getXMPPError().getMessage(), e.getXMPPError().getCondition(), e
	.getXMPPError().getCode());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}