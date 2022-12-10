/*
* $Id$
*
* Copyright (c) 2000-2012 by Rodney Kinney, Brent Easton
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
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.XMPPException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using XMPPException = org.jivesoftware.smack.XMPPException;
//UPGRADE_TODO: The type 'org.jivesoftware.smack.util.StringUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using StringUtils = org.jivesoftware.smack.util.StringUtils;
//UPGRADE_TODO: The type 'org.jivesoftware.smackx.Form' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Form = org.jivesoftware.smackx.Form;
//UPGRADE_TODO: The type 'org.jivesoftware.smackx.muc.MultiUserChat' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MultiUserChat = org.jivesoftware.smackx.muc.MultiUserChat;
//UPGRADE_TODO: The type 'org.jivesoftware.smackx.muc.RoomInfo' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using RoomInfo = org.jivesoftware.smackx.muc.RoomInfo;
using Info = VassalSharp.Info;
using GameModule = VassalSharp.build.GameModule;
using LockableRoom = VassalSharp.chat.LockableRoom;
using Player = VassalSharp.chat.Player;
using SimpleRoom = VassalSharp.chat.SimpleRoom;
using SimpleStatus = VassalSharp.chat.SimpleStatus;
using Resources = VassalSharp.i18n.Resources;
using PropertiesEncoder = VassalSharp.tools.PropertiesEncoder;
using Dialogs = VassalSharp.tools.swing.Dialogs;
namespace VassalSharp.chat.jabber
{
	
	public class JabberRoom:SimpleRoom, LockableRoom
	{
		virtual public System.String JID
		{
			get
			{
				return jid;
			}
			
		}
		virtual public bool Locked
		{
			get
			{
				return info != null && info.isMembersOnly();
			}
			
		}
		virtual public RoomInfo Info
		{
			set
			{
				this.info = value;
			}
			
		}
		virtual public bool OwnedByMe
		{
			get
			{
				return ownedByMe;
			}
			
		}
		virtual public Player OwningPlayer
		{
			get
			{
				if (owners.size() == 0)
				{
					return null;
				}
				return getPlayer(owners.get_Renamed(0));
			}
			
		}
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		virtual public System.Collections.Specialized.NameValueCollection Config
		{
			set
			{
				config = value;
				setName(config.Get(CONFIG_NAME));
			}
			
		}
		virtual public bool StartLocked
		{
			get
			{
				return "true".Equals(config.Get(CONFIG_LOCKED)); //$NON-NLS-1$
			}
			
		}
		virtual public bool MatchCrc
		{
			get
			{
				return "true".Equals(config.Get(CONFIG_CRC_CHECK)); //$NON-NLS-1$
			}
			
		}
		virtual public System.String CheckCrc
		{
			get
			{
				return config.Get(CONFIG_CRC);
			}
			
		}
		virtual public System.String VassalOption
		{
			get
			{
				return config[CONFIG_VASSAL_VERSION] == null?ANY_OPTION:config[CONFIG_VASSAL_VERSION];
			}
			
		}
		virtual public System.String VassalVersion
		{
			get
			{
				return config[CONFIG_MIN_VASSAL_VERSION] == null?Info.Version:config[CONFIG_MIN_VASSAL_VERSION];
			}
			
		}
		virtual public System.String ModuleOption
		{
			get
			{
				return config[CONFIG_MODULE_VERSION] == null?ANY_OPTION:config[CONFIG_MODULE_VERSION];
			}
			
		}
		virtual public System.String ModuleVersion
		{
			get
			{
				return config.getProperty(CONFIG_MIN_MODULE_VERSION, GameModule.getGameModule().getGameVersion());
			}
			
		}
		public const System.String CONFIG_NAME = "name"; //$NON-NLS-1$
		public const System.String CONFIG_LOCKED = "locked"; //$NON-NLS-1$
		public const System.String CONFIG_VASSAL_VERSION = "vasVer"; //$NON-NLS-1$
		public const System.String CONFIG_MIN_VASSAL_VERSION = "minVasVer"; //$NON-NLS-1$
		public const System.String CONFIG_MODULE_VERSION = "modVer"; //$NON-NLS-1$
		public const System.String CONFIG_MIN_MODULE_VERSION = "minModVer"; //$NON-NLS-1$
		public const System.String CONFIG_CRC_CHECK = "crcCheck"; //$NON-NLS-1$
		public const System.String CONFIG_CRC = "crc"; //$NON-NLS-1$
		
		private const System.String JABBER_MEMBERSONLY = "muc#roomconfig_membersonly"; //$NON-NLS-1$
		private const System.String JABBER_ALLOW_INVITES = "muc#roomconfig_allowinvites"; //$NON-NLS-1$
		private const System.String JABBER_CHANGE_SUBJECT = "muc#roomconfig_changesubject"; //$NON-NLS-1$
		private const System.String JABBER_MODERATED = "muc#roomconfig_moderatedroom"; //$NON-NLS-1$
		private const System.String JABBER_PASSWORD_PROTECTED = "muc#roomconfig_passwordprotectedroom"; //$NON-NLS-1$
		private const System.String JABBER_PERSISTENT = "muc#roomconfig_persistentroom"; //$NON-NLS-1$
		private const System.String JABBER_PUBLIC_ROOM = "muc#roomconfig_publicroom"; //$NON-NLS-1$
		
		private System.String jid;
		private RoomInfo info;
		private bool ownedByMe;
		private JabberClient client;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private ArrayList < String > owners = new ArrayList < String >();
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
		private System.Collections.Specialized.NameValueCollection config = new System.Collections.Specialized.NameValueCollection();
		
		private JabberRoom(System.String name, System.String jid, RoomInfo info, JabberClient client):base(name)
		{
			this.jid = jid;
			this.info = info;
			this.client = client;
			config.Clear();
			config[(System.String) CONFIG_NAME] = (System.String) name;
		}
		
		public virtual void  toggleLock(MultiUserChat muc)
		{
			try
			{
				if (!Locked)
				{
					lock_Renamed(muc);
				}
				else
				{
					unlock(muc);
				}
				info = MultiUserChat.getRoomInfo(client.Connection, jid);
			}
			catch (XMPPException e)
			{
				e.printStackTrace();
				return ;
			}
		}
		
		protected internal virtual void  lock_Renamed(MultiUserChat muc)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'form '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Form form = muc.getConfigurationForm().createAnswerForm();
			form.setAnswer(JABBER_MEMBERSONLY, true);
			muc.sendConfigurationForm(form);
		}
		
		protected internal virtual void  unlock(MultiUserChat muc)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'form '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Form form = muc.getConfigurationForm().createAnswerForm();
			form.setAnswer(JABBER_MEMBERSONLY, false);
			muc.sendConfigurationForm(form);
		}
		
		public virtual MultiUserChat join(JabberClient client, JabberPlayer me)
		{
			MultiUserChat chat = new MultiUserChat(client.Connection, JID);
			chat.join(StringUtils.parseName(me.Jid));
			
			if (!chat.isJoined())
			{
				return null;
			}
			
			try
			{
				// This is necessary to create the room if it doesn't already exist
				// Configure the options we needs explicitly, don't depend on the server supplied defaults
				//UPGRADE_NOTE: Final was removed from the declaration of 'configForm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Form configForm = chat.getConfigurationForm().createAnswerForm();
				configForm.setAnswer(JABBER_MEMBERSONLY, StartLocked);
				configForm.setAnswer(JABBER_ALLOW_INVITES, false);
				configForm.setAnswer(JABBER_CHANGE_SUBJECT, false);
				configForm.setAnswer(JABBER_MODERATED, false);
				configForm.setAnswer(JABBER_PASSWORD_PROTECTED, false);
				configForm.setAnswer(JABBER_PERSISTENT, false);
				configForm.setAnswer(JABBER_PUBLIC_ROOM, true);
				
				chat.sendConfigurationForm(configForm);
				ownedByMe = true;
				owners.clear();
				addOwner(jid);
			}
			catch (XMPPException e)
			{
				// 403 code means the room already exists and user is not an owner
				if (e.getXMPPError() != null && e.getXMPPError().getCode() != 403)
				{
					throw e;
				}
			}
			
			chat.addMessageListener(client);
			return chat;
		}
		
		public  override bool Equals(System.Object o)
		{
			if (o is JabberRoom)
			{
				JabberRoom r = (JabberRoom) o;
				return r.jid.Equals(jid);
			}
			else
			{
				return false;
			}
		}
		
		public override int GetHashCode()
		{
			return jid.GetHashCode();
		}
		
		public virtual bool isOwner(System.String jid)
		{
			return owners.contains(jid);
		}
		
		public virtual void  addOwner(System.String jid)
		{
			if (!owners.contains(jid))
			{
				owners.add(jid);
			}
		}
		
		public virtual void  removeOwner(System.String jid)
		{
			owners.remove(jid);
		}
		
		public virtual System.String encodeConfig()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String s = new PropertiesEncoder(config).StringValue;
			return s == null?"":s; //$NON-NLS-1$
		}
		
		public virtual void  decodeConfig(System.String s)
		{
			try
			{
				config = new PropertiesEncoder(s).Properties;
				setName(config.Get(CONFIG_NAME));
			}
			catch (System.IO.IOException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
			}
		}
		
		public virtual void  showConfig()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			JabberRoomConfig c = new JabberRoomConfig(config, false);
			Dialogs.showDialog(null, Resources.getString("Chat.room_configuration"), c, (int) System.Windows.Forms.MessageBoxIcon.None, null, (int) System.Windows.Forms.MessageBoxButtons.OKCancel, null, (System.Object) null, (System.Object) null, null); //$NON-NLS-1$
		}
		
		/// <summary> Is the specified player allowed to join this room?</summary>
		/// <param name="p">A JabberPlayer
		/// </param>
		/// <returns> null = false, non-null = error message
		/// </returns>
		public virtual System.String canJoin(JabberPlayer p)
		{
			
			// Owner can always join
			if (OwnedByMe)
			{
				return null;
			}
			
			// Check Vassal Version
			System.String option = VassalOption;
			if (!ANY_OPTION.Equals(option))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'thisVassal '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String thisVassal = Info.Version;
				//UPGRADE_NOTE: Final was removed from the declaration of 'targetVassal '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String targetVassal = VassalVersion;
				if (MINIMUM_OPTION.Equals(option))
				{
					if (Info.compareVersions(thisVassal, targetVassal) < 1)
					{
						return Resources.getString("Chat.bad_min_vassal", thisVassal, targetVassal); //$NON-NLS-1$
					}
				}
				else
				{
					if (!thisVassal.Equals(targetVassal))
					{
						return Resources.getString("Chat.bad_vassal", thisVassal, targetVassal); //$NON-NLS-1$
					}
				}
			}
			// Check Module Version
			option = ModuleOption;
			if (!ANY_OPTION.Equals(option))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'thisModule '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String thisModule = GameModule.getGameModule().getGameVersion();
				//UPGRADE_NOTE: Final was removed from the declaration of 'targetModule '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String targetModule = ModuleVersion;
				if (MINIMUM_OPTION.Equals(option))
				{
					if (Info.compareVersions(thisModule, targetModule) < 1)
					{
						return Resources.getString("Chat.bad_min_module", thisModule, targetModule); //$NON-NLS-1$
					}
				}
				else
				{
					if (!thisModule.Equals(targetModule))
					{
						return Resources.getString("Chat.bad_module", thisModule, targetModule); //$NON-NLS-1$
					}
				}
			}
			
			// Check CRC
			if (MatchCrc)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'playerCRC '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String playerCRC = ((SimpleStatus) p.getStatus()).Crc;
				//UPGRADE_NOTE: Final was removed from the declaration of 'moduleCRC '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String moduleCRC = CheckCrc;
				if (!moduleCRC.Equals(playerCRC))
				{
					return Resources.getString("Chat.bad_crc"); //$NON-NLS-1$
				}
			}
			return null;
		}
		
		public class Manager
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private Map < String, JabberRoom > jidToRoom = new HashMap < String, JabberRoom >();
			
			public virtual JabberRoom getRoomByJID(JabberClient client, System.String jid)
			{
				return getRoomByJID(client, jid, "");
			}
			
			//UPGRADE_NOTE: Synchronized keyword was removed from method 'getRoomByJID'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
			public virtual JabberRoom getRoomByJID(JabberClient client, System.String jid, System.String defaultName)
			{
				lock (this)
				{
					if (jid == null)
					{
						return null;
					}
					JabberRoom newRoom = jidToRoom.get_Renamed(jid);
					if (newRoom == null)
					{
						System.String roomName = defaultName == null?"":defaultName; //$NON-NLS-1$
						RoomInfo info = null;
						try
						{
							info = MultiUserChat.getRoomInfo(client.Connection, jid);
						}
						// FIXME: review error message
						catch (XMPPException e)
						{
							e.printStackTrace();
						}
						newRoom = new JabberRoom(roomName, jid, info, client);
						jidToRoom.put(jid, newRoom);
					}
					return newRoom;
				}
			}
			
			//UPGRADE_NOTE: Synchronized keyword was removed from method 'getRoomByName'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
			public virtual JabberRoom getRoomByName(JabberClient client, System.String name)
			{
				lock (this)
				{
					System.String jid = StringUtils.escapeNode(client.Module + "-" + name).toLowerCase() + "@" + client.ConferenceService; //$NON-NLS-1$ //$NON-NLS-2$
					JabberRoom room = jidToRoom.get_Renamed(jid);
					if (room == null)
					{
						room = new JabberRoom(name, jid, null, client);
						jidToRoom.put(jid, room);
					}
					return room;
				}
			}
			
			public virtual void  deleteRoom(System.String jid)
			{
				jidToRoom.remove(jid);
			}
			
			//UPGRADE_NOTE: Synchronized keyword was removed from method 'clear'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
			public virtual void  clear()
			{
				lock (this)
				{
					jidToRoom.clear();
				}
			}
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public static System.Collections.Specialized.NameValueCollection configureNewRoom()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'config '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			JabberRoomConfig config = new JabberRoomConfig();
			//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Int32 result = ((System.Int32) Dialogs.showDialog(null, Resources.getString("Chat.create_new_room"), config, (int) System.Windows.Forms.MessageBoxIcon.None, null, (int) System.Windows.Forms.MessageBoxButtons.OKCancel, null, (System.Object) null, (System.Object) null, null)); //$NON-NLS-1$
			//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
			if (result != null && result == 0)
			{
				return config.Properties;
			}
			return null;
		}
		
		private const System.String MINIMUM_OPTION = "min"; //$NON-NLS-1$
		private const System.String ANY_OPTION = "any"; //$NON-NLS-1$
		private const System.String THIS_OPTION = "this"; //$NON-NLS-1$
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'MINIMUM_VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'MINIMUM_VERSION' was moved to static method 'VassalSharp.chat.jabber.JabberRoom'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly System.String MINIMUM_VERSION; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'ANY_VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'ANY_VERSION' was moved to static method 'VassalSharp.chat.jabber.JabberRoom'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly System.String ANY_VERSION; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'THIS_VERSION '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'THIS_VERSION' was moved to static method 'VassalSharp.chat.jabber.JabberRoom'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly System.String THIS_VERSION; //$NON-NLS-1$
		
		[Serializable]
		public class JabberRoomConfig:System.Windows.Forms.Panel
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassItemListener
			{
				public AnonymousClassItemListener(JabberRoomConfig enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(JabberRoomConfig enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private JabberRoomConfig enclosingInstance;
				public JabberRoomConfig Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs arg0)
				{
					if (event_sender is System.Windows.Forms.MenuItem)
						((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
					Enclosing_Instance.updateVisibility();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassItemListener1
			{
				public AnonymousClassItemListener1(JabberRoomConfig enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(JabberRoomConfig enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private JabberRoomConfig enclosingInstance;
				public JabberRoomConfig Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs arg0)
				{
					if (event_sender is System.Windows.Forms.MenuItem)
						((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
					Enclosing_Instance.updateVisibility();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener
			{
				public AnonymousClassActionListener(JabberRoomConfig enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(JabberRoomConfig enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private JabberRoomConfig enclosingInstance;
				public JabberRoomConfig Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.updateVisibility();
				}
			}
			virtual public bool UpdateEnabled
			{
				get
				{
					return updateEnabled;
				}
				
			}
			//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			new virtual public bool Enabled
			{
				get
				{
					return base.Enabled;
				}
				
				set
				{
					updateEnabled = value;
					updateVisibility();
				}
				
			}
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			virtual public System.Collections.Specialized.NameValueCollection Properties
			{
				get
				{
					//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
					//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
					System.Collections.Specialized.NameValueCollection props = new System.Collections.Specialized.NameValueCollection();
					props[(System.String) VassalSharp.chat.jabber.JabberRoom.CONFIG_NAME] = (System.String) roomNameConfig.Text;
					props[(System.String) VassalSharp.chat.jabber.JabberRoom.CONFIG_LOCKED] = (System.String) Boolean.toString(startLockedConfig.Checked);
					//UPGRADE_NOTE: Final was removed from the declaration of 'vv '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String vv = versionToOption((System.String) vassalVersionConfig.SelectedItem);
					props[(System.String) VassalSharp.chat.jabber.JabberRoom.CONFIG_VASSAL_VERSION] = (System.String) vv;
					if (!vv.Equals(VassalSharp.chat.jabber.JabberRoom.ANY_OPTION))
					{
						props[(System.String) VassalSharp.chat.jabber.JabberRoom.CONFIG_MIN_VASSAL_VERSION] = (System.String) minimumVassalVersionConfig.Text;
					}
					//UPGRADE_NOTE: Final was removed from the declaration of 'mv '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String mv = versionToOption((System.String) moduleVersionConfig.SelectedItem);
					props[(System.String) VassalSharp.chat.jabber.JabberRoom.CONFIG_MODULE_VERSION] = (System.String) mv;
					if (!mv.Equals(VassalSharp.chat.jabber.JabberRoom.ANY_OPTION))
					{
						props[(System.String) VassalSharp.chat.jabber.JabberRoom.CONFIG_MIN_MODULE_VERSION] = (System.String) minimumModuleVersionConfig.Text;
					}
					props[(System.String) VassalSharp.chat.jabber.JabberRoom.CONFIG_CRC_CHECK] = (System.String) Boolean.toString(matchCrcConfig.Checked);
					if (matchCrcConfig.Checked)
					{
						props[(System.String) VassalSharp.chat.jabber.JabberRoom.CONFIG_CRC] = (System.String) crcConfig.Text;
					}
					return props;
				}
				
			}
			private const long serialVersionUID = 1L;
			private System.Windows.Forms.TextBox roomNameConfig;
			private System.Windows.Forms.CheckBox startLockedConfig;
			private System.Windows.Forms.CheckBox matchCrcConfig;
			private System.Windows.Forms.TextBox crcConfig;
			private System.Windows.Forms.ComboBox vassalVersionConfig;
			private System.Windows.Forms.TextBox minimumVassalVersionConfig;
			private System.Windows.Forms.ComboBox moduleVersionConfig;
			private System.Windows.Forms.TextBox minimumModuleVersionConfig;
			private System.String vassalVersion;
			private System.String moduleVersion;
			private bool updateEnabled = true;
			
			internal static System.String versionToOption(System.String version)
			{
				if (VassalSharp.chat.jabber.JabberRoom.MINIMUM_VERSION.Equals(version))
				{
					return VassalSharp.chat.jabber.JabberRoom.MINIMUM_OPTION;
				}
				else if (VassalSharp.chat.jabber.JabberRoom.THIS_VERSION.Equals(version))
				{
					return VassalSharp.chat.jabber.JabberRoom.THIS_OPTION;
				}
				return VassalSharp.chat.jabber.JabberRoom.ANY_OPTION;
			}
			
			internal static System.String optionToVersion(System.String option)
			{
				if (VassalSharp.chat.jabber.JabberRoom.MINIMUM_OPTION.Equals(option))
				{
					return VassalSharp.chat.jabber.JabberRoom.MINIMUM_VERSION;
				}
				else if (VassalSharp.chat.jabber.JabberRoom.THIS_OPTION.Equals(option))
				{
					return VassalSharp.chat.jabber.JabberRoom.THIS_VERSION;
				}
				return VassalSharp.chat.jabber.JabberRoom.ANY_VERSION;
			}
			
			public JabberRoomConfig():base()
			{
				
				vassalVersion = Info.Version;
				moduleVersion = GameModule.getGameModule().getGameVersion();
				setLayout(new MigLayout("insets dialog", "[align right][fill,grow]", "")); //$NON-NLS-1$  //$NON-NLS-2$ //$NON-NLS-3$
				
				
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = Resources.getString("Chat.new_room_name");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				Controls.Add(temp_Control); //$NON-NLS-1$
				roomNameConfig = new System.Windows.Forms.TextBox();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(roomNameConfig);
				roomNameConfig.Dock = new System.Windows.Forms.DockStyle();
				roomNameConfig.BringToFront(); //$NON-NLS-1$
				
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = Resources.getString("Chat.start_locked");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label4;
				Controls.Add(temp_Control2); //$NON-NLS-1$
				startLockedConfig = new System.Windows.Forms.CheckBox();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(startLockedConfig);
				startLockedConfig.Dock = new System.Windows.Forms.DockStyle();
				startLockedConfig.BringToFront(); //$NON-NLS-1$
				
				System.Windows.Forms.Label temp_label6;
				temp_label6 = new System.Windows.Forms.Label();
				temp_label6.Text = Resources.getString("Chat.vassal_versions_allowed");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control3;
				temp_Control3 = temp_label6;
				Controls.Add(temp_Control3); //$NON-NLS-1$
				vassalVersionConfig = SupportClass.ComboBoxSupport.CreateComboBox(new System.String[]{VassalSharp.chat.jabber.JabberRoom.ANY_VERSION, VassalSharp.chat.jabber.JabberRoom.THIS_VERSION, VassalSharp.chat.jabber.JabberRoom.MINIMUM_VERSION}); //$NON-NLS-1$
				vassalVersionConfig.SelectedValueChanged += new System.EventHandler(new AnonymousClassItemListener(this).itemStateChanged);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(vassalVersionConfig);
				//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
				minimumVassalVersionConfig = new System.Windows.Forms.TextBox();
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				minimumVassalVersionConfig.Text = vassalVersion;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(minimumVassalVersionConfig);
				minimumVassalVersionConfig.Dock = new System.Windows.Forms.DockStyle();
				minimumVassalVersionConfig.BringToFront(); //$NON-NLS-1$
				
				System.Windows.Forms.Label temp_label8;
				temp_label8 = new System.Windows.Forms.Label();
				temp_label8.Text = Resources.getString("Chat.module_versions_allowed");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control4;
				temp_Control4 = temp_label8;
				Controls.Add(temp_Control4); //$NON-NLS-1$
				moduleVersionConfig = SupportClass.ComboBoxSupport.CreateComboBox(new System.String[]{VassalSharp.chat.jabber.JabberRoom.ANY_VERSION, VassalSharp.chat.jabber.JabberRoom.THIS_VERSION, VassalSharp.chat.jabber.JabberRoom.MINIMUM_VERSION});
				moduleVersionConfig.SelectedValueChanged += new System.EventHandler(new AnonymousClassItemListener1(this).itemStateChanged);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(moduleVersionConfig);
				//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
				minimumModuleVersionConfig = new System.Windows.Forms.TextBox();
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				minimumModuleVersionConfig.Text = moduleVersion;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(minimumModuleVersionConfig);
				minimumModuleVersionConfig.Dock = new System.Windows.Forms.DockStyle();
				minimumModuleVersionConfig.BringToFront(); //$NON-NLS-1$
				
				System.Windows.Forms.Label temp_label10;
				temp_label10 = new System.Windows.Forms.Label();
				temp_label10.Text = Resources.getString("Chat.crc_match");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control5;
				temp_Control5 = temp_label10;
				Controls.Add(temp_Control5); //$NON-NLS-1$
				matchCrcConfig = new System.Windows.Forms.CheckBox();
				matchCrcConfig.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(matchCrcConfig);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(matchCrcConfig);
				//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
				crcConfig = new System.Windows.Forms.TextBox();
				crcConfig.setText(Long.toHexString(GameModule.getGameModule().getCrc()));
				crcConfig.ReadOnly = !false;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				Controls.Add(crcConfig);
				crcConfig.Dock = new System.Windows.Forms.DockStyle();
				crcConfig.BringToFront(); //$NON-NLS-1$
				
				updateVisibility();
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			public JabberRoomConfig(System.Collections.Specialized.NameValueCollection props):this()
			{
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				roomNameConfig.Text = props.Get(VassalSharp.chat.jabber.JabberRoom.CONFIG_NAME);
				startLockedConfig.Checked = "true".Equals(props.Get(VassalSharp.chat.jabber.JabberRoom.CONFIG_LOCKED)); //$NON-NLS-1$
				vassalVersionConfig.SelectedItem = optionToVersion(props[VassalSharp.chat.jabber.JabberRoom.CONFIG_VASSAL_VERSION] == null?VassalSharp.chat.jabber.JabberRoom.ANY_VERSION:props[VassalSharp.chat.jabber.JabberRoom.CONFIG_VASSAL_VERSION]);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				minimumVassalVersionConfig.Text = props[VassalSharp.chat.jabber.JabberRoom.CONFIG_MIN_VASSAL_VERSION] == null?"":props[VassalSharp.chat.jabber.JabberRoom.CONFIG_MIN_VASSAL_VERSION]; //$NON-NLS-1$
				moduleVersionConfig.SelectedItem = optionToVersion(props[VassalSharp.chat.jabber.JabberRoom.CONFIG_MODULE_VERSION] == null?VassalSharp.chat.jabber.JabberRoom.ANY_VERSION:props[VassalSharp.chat.jabber.JabberRoom.CONFIG_MODULE_VERSION]);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				minimumModuleVersionConfig.Text = props[VassalSharp.chat.jabber.JabberRoom.CONFIG_MIN_MODULE_VERSION] == null?"":props[VassalSharp.chat.jabber.JabberRoom.CONFIG_MIN_MODULE_VERSION]; //$NON-NLS-1$
				matchCrcConfig.Checked = "true".Equals(props.Get(VassalSharp.chat.jabber.JabberRoom.CONFIG_CRC_CHECK)); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				crcConfig.Text = props.Get(VassalSharp.chat.jabber.JabberRoom.CONFIG_CRC);
			}
			
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			public JabberRoomConfig(System.Collections.Specialized.NameValueCollection props, bool enabled):this(props)
			{
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setEnabled' was converted to 'System.Windows.Forms.Control.Enabled' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetEnabled_boolean'"
				Enabled = false;
			}
			
			private void  updateVisibility()
			{
				minimumVassalVersionConfig.Visible = !VassalSharp.chat.jabber.JabberRoom.ANY_VERSION.Equals(vassalVersionConfig.SelectedItem);
				minimumModuleVersionConfig.Visible = !VassalSharp.chat.jabber.JabberRoom.ANY_VERSION.Equals(moduleVersionConfig.SelectedItem);
				crcConfig.Visible = matchCrcConfig.Checked;
				
				roomNameConfig.ReadOnly = !UpdateEnabled;
				startLockedConfig.Enabled = UpdateEnabled;
				vassalVersionConfig.Enabled = UpdateEnabled;
				minimumVassalVersionConfig.ReadOnly = !(UpdateEnabled && vassalVersionConfig.SelectedItem.Equals(VassalSharp.chat.jabber.JabberRoom.MINIMUM_VERSION));
				moduleVersionConfig.Enabled = UpdateEnabled;
				minimumModuleVersionConfig.ReadOnly = !(UpdateEnabled && moduleVersionConfig.SelectedItem.Equals(VassalSharp.chat.jabber.JabberRoom.MINIMUM_VERSION));
				matchCrcConfig.Enabled = UpdateEnabled;
			}
		}
		static JabberRoom()
		{
			MINIMUM_VERSION = Resources.getString("Chat.mimimum_version");
			ANY_VERSION = Resources.getString("Chat.any_version");
			THIS_VERSION = Resources.getString("Chat.this_version");
		}
	}
}