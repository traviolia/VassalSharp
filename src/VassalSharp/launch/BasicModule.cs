/*
* Copyright (c) 2000-2011 by Rodney Kinney, Brent Easton
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
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
using Buildable = VassalSharp.build.Buildable;
using Builder = VassalSharp.build.Builder;
using GameModule = VassalSharp.build.GameModule;
using BasicCommandEncoder = VassalSharp.build.module.BasicCommandEncoder;
using BasicLogger = VassalSharp.build.module.BasicLogger;
using Chatter = VassalSharp.build.module.Chatter;
using Documentation = VassalSharp.build.module.Documentation;
using GameRefresher = VassalSharp.build.module.GameRefresher;
using GameState = VassalSharp.build.module.GameState;
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using Map = VassalSharp.build.module.Map;
using PieceWindow = VassalSharp.build.module.PieceWindow;
using PlayerRoster = VassalSharp.build.module.PlayerRoster;
using PluginsLoader = VassalSharp.build.module.PluginsLoader;
using PrototypesContainer = VassalSharp.build.module.PrototypesContainer;
using GamePieceImageDefinitions = VassalSharp.build.module.gamepieceimage.GamePieceImageDefinitions;
using GlobalProperties = VassalSharp.build.module.properties.GlobalProperties;
using AddressBookServerConfigurer = VassalSharp.chat.AddressBookServerConfigurer;
using ChatServerFactory = VassalSharp.chat.ChatServerFactory;
using DynamicClient = VassalSharp.chat.DynamicClient;
using DynamicClientFactory = VassalSharp.chat.DynamicClientFactory;
using HybridClient = VassalSharp.chat.HybridClient;
using JabberClientFactory = VassalSharp.chat.jabber.JabberClientFactory;
using NodeClientFactory = VassalSharp.chat.node.NodeClientFactory;
using P2PClientFactory = VassalSharp.chat.peer2peer.P2PClientFactory;
using ChatServerControls = VassalSharp.chat.ui.ChatServerControls;
using Command = VassalSharp.command.Command;
using PasswordConfigurer = VassalSharp.configure.PasswordConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using TextConfigurer = VassalSharp.configure.TextConfigurer;
using Language = VassalSharp.i18n.Language;
using Resources = VassalSharp.i18n.Resources;
using PositionOption = VassalSharp.preferences.PositionOption;
using Prefs = VassalSharp.preferences.Prefs;
using DataArchive = VassalSharp.tools.DataArchive;
using ReflectionUtils = VassalSharp.tools.ReflectionUtils;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using IOUtils = VassalSharp.tools.io.IOUtils;
using MenuManager = VassalSharp.tools.menu.MenuManager;
namespace VassalSharp.launch
{
	
	public class BasicModule:GameModule
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener1
		{
			public AnonymousClassPropertyChangeListener1(BasicModule enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicModule enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicModule enclosingInstance;
			public BasicModule Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				//UPGRADE_ISSUE: Method 'java.beans.PropertyChangeSupport.firePropertyChange' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
				Enclosing_Instance.idChangeSupport.firePropertyChange(event_sender, evt);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener2
		{
			public AnonymousClassPropertyChangeListener2(BasicModule enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicModule enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicModule enclosingInstance;
			public BasicModule Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				//UPGRADE_ISSUE: Method 'java.beans.PropertyChangeSupport.firePropertyChange' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
				Enclosing_Instance.idChangeSupport.firePropertyChange(event_sender, evt);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener3
		{
			public AnonymousClassPropertyChangeListener3(BasicModule enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BasicModule enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicModule enclosingInstance;
			public BasicModule Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				GameModule.setUserId((System.String) evt.NewValue);
			}
		}
		private void  InitBlock()
		{
			if (getComponentsOf(componentClass).isEmpty())
			{
				addComponent(componentClass);
			}
			Buildable child = null;
			try
			{
				child = componentClass.getConstructor().newInstance();
			}
			//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception t)
			{
				ReflectionUtils.handleNewInstanceFailure(t, componentClass);
			}
			
			if (child != null)
			{
				child.build(null);
				child.addTo(this);
				add(child);
			}
		}
		[System.ComponentModel.Browsable(true)]
		public new  event SupportClass.PropertyChangeEventHandler PropertyChange;
		virtual public ChatServerControls ServerControls
		{
			get
			{
				return serverControls;
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'log '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'log' was moved to static method 'VassalSharp.launch.BasicModule'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger log;
		
		private static char COMMAND_SEPARATOR = (char) SupportClass.Identity((int) System.Windows.Forms.Keys.Escape);
		protected internal ChatServerControls serverControls;
		protected internal GameRefresher gameRefresher;
		
		public BasicModule(DataArchive archive):base(archive)
		{
			InitBlock();
		}
		
		protected internal override void  build()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'darch '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			DataArchive darch = getDataArchive();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.FileInfo f = new System.IO.FileInfo(darch.Name);
			bool tmpBool;
			if (System.IO.File.Exists(f.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(f.FullName);
			if (!tmpBool || SupportClass.FileLength(f) == 0)
			{
				// new module, no buildFile
				build(null);
			}
			else
			{
				// existing module
				System.IO.BufferedStream in_Renamed = null;
				try
				{
					try
					{
						in_Renamed = new System.IO.BufferedStream(darch.getInputStream(BUILDFILE));
					}
					// FIXME: review error message
					// FIXME: this should be more specific, to separate the case where
					// we have failed I/O from when we read ok but have no module
					catch (System.IO.IOException e)
					{
						throw (System.IO.IOException) new System.IO.IOException(Resources.getString("BasicModule.not_a_module")).initCause(e);
					}
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'doc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Xml.XmlDocument doc = Builder.createDocument(in_Renamed);
					build((System.Xml.XmlElement) doc.DocumentElement);
					in_Renamed.Close();
				}
				finally
				{
					IOUtils.closeQuietly(in_Renamed);
				}
			}
			
			MenuManager.Instance.addAction("Prefs.edit_preferences", getPrefs().getEditor().getEditAction());
			
			gameRefresher = new GameRefresher(this);
			gameRefresher.addTo(this);
			MenuManager.Instance.addAction("GameRefresher.refresh_counters", gameRefresher.RefreshAction);
		}
		
		public override void  build(System.Xml.XmlElement e)
		{
			/*
			* We determine the name of the module at the very beginning, so we
			* know which preferences to read.
			*/
			if (e != null)
			{
				gameName = e.GetAttribute(MODULE_NAME);
				if (e.GetAttribute(VASSAL_VERSION_CREATED).Length > 0)
				{
					vassalVersionCreated = e.GetAttribute(VASSAL_VERSION_CREATED);
				}
			}
			
			initIdentityPreferences();
			initImagePreferences();
			Prefs.initSharedGlobalPrefs();
			initGameState();
			initLogger();
			initServer();
			new PluginsLoader().addTo(this);
			if (e != null)
			{
				base.build(e);
				ensureComponent(typeof(GamePieceImageDefinitions));
				ensureComponent(typeof(GlobalProperties));
				ensureComponent(typeof(Language));
			}
			else
			{
				buildDefaultComponents();
			}
			initFrame();
		}
		
		protected internal virtual void  initIdentityPreferences()
		{
			//UPGRADE_ISSUE: Constructor 'java.beans.PropertyChangeSupport.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			idChangeSupport = new PropertyChangeSupport(this);
			StringConfigurer fullName = new StringConfigurer(GameModule.REAL_NAME, Resources.getString("Prefs.name_label"), Resources.getString("Prefs.newbie")); //$NON-NLS-1$ //$NON-NLS-2$
			fullName.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener1(this).propertyChange);
			TextConfigurer profile = new TextConfigurer(GameModule.PERSONAL_INFO, Resources.getString("Prefs.personal_info"), ""); //$NON-NLS-1$ //$NON-NLS-2$
			profile.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener2(this).propertyChange);
			//UPGRADE_TODO: Method 'java.lang.System.getProperty' was converted to 'System.Environment.UserName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangSystemgetProperty_javalangString'"
			StringConfigurer user = new PasswordConfigurer(GameModule.SECRET_NAME, Resources.getString("Prefs.password_label"), Resources.getString("Prefs.password_prompt", System.Environment.UserName)); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$ //$NON-NLS-4$
			user.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener3(this).propertyChange);
			GameModule.getGameModule().getPrefs().addOption(Resources.getString("Prefs.personal_tab"), fullName); //$NON-NLS-1$ //$NON-NLS-2$
			GameModule.getGameModule().getPrefs().addOption(Resources.getString("Prefs.personal_tab"), user); //$NON-NLS-1$ //$NON-NLS-2$
			GameModule.getGameModule().getPrefs().addOption(Resources.getString("Prefs.personal_tab"), profile); //$NON-NLS-1$
			GameModule.setUserId(user.ValueString);
		}
		
		protected internal virtual void  initImagePreferences()
		{
		}
		
		protected internal virtual void  initServer()
		{
			ChatServerFactory.register(NodeClientFactory.NODE_TYPE, new NodeClientFactory());
			ChatServerFactory.register(DynamicClientFactory.DYNAMIC_TYPE, new DynamicClientFactory());
			ChatServerFactory.register(P2PClientFactory.P2P_TYPE, new P2PClientFactory());
			ChatServerFactory.register(JabberClientFactory.JABBER_SERVER_TYPE, new JabberClientFactory());
			server = new DynamicClient();
			AddressBookServerConfigurer config = new AddressBookServerConfigurer("ServerImpl", "Server", (HybridClient) server);
			Prefs.GlobalPrefs.addOption(Resources.getString("Chat.server"), config); //$NON-NLS-1$
			serverControls = new ChatServerControls();
			serverControls.addTo(this);
		}
		
		protected internal virtual void  initLogger()
		{
			logger = new BasicLogger();
			((BasicLogger) logger).build(null);
			((BasicLogger) logger).addTo(this);
		}
		
		protected internal virtual void  initGameState()
		{
			theState = new GameState();
			theState.addTo(this);
			addCommandEncoder(theState);
		}
		
		public override Command decode(System.String command)
		{
			if (command == null)
			{
				return null;
			}
			Command c = null;
			//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(command, COMMAND_SEPARATOR);
			System.String first = st.nextToken();
			if (command.Equals(first))
			{
				c = decodeSubCommand(first);
			}
			else
			{
				Command next = null;
				c = decode(first);
				while (st.hasMoreTokens())
				{
					next = decode(st.nextToken());
					c = c == null?next:c.append(next);
				}
			}
			return c;
		}
		
		private Command decodeSubCommand(System.String subCommand)
		{
			Command c = null;
			for (int i = 0; i < commandEncoders.Length && c == null; ++i)
			{
				c = commandEncoders[i].decode(subCommand);
			}
			return c;
		}
		
		public override System.String encode(Command c)
		{
			if (c == null)
			{
				return null;
			}
			System.String s = encodeSubCommand(c);
			System.String s2;
			Command[] sub = c.SubCommands;
			if (sub.Length > 0)
			{
				SequenceEncoder se = new SequenceEncoder(s, COMMAND_SEPARATOR);
				for (int i = 0; i < sub.Length; ++i)
				{
					s2 = encode(sub[i]);
					if (s2 != null)
					{
						se.append(s2);
					}
				}
				s = se.Value;
			}
			return s;
		}
		
		private System.String encodeSubCommand(Command c)
		{
			System.String s = null;
			for (int i = 0; i < commandEncoders.Length && s == null; ++i)
			{
				s = commandEncoders[i].encode(c);
			}
			return s;
		}
		
		protected internal virtual void  buildDefaultComponents()
		{
			addComponent(typeof(BasicCommandEncoder));
			addComponent(typeof(Documentation));
			addComponent(typeof(PlayerRoster));
			addComponent(typeof(GlobalOptions));
			addComponent(typeof(Map));
			addComponent(typeof(GamePieceImageDefinitions));
			addComponent(typeof(GlobalProperties));
			addComponent(typeof(PrototypesContainer));
			addComponent(typeof(PieceWindow));
			addComponent(typeof(Chatter));
			addComponent(typeof(Language));
		}
		
		protected internal virtual void  initFrame()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'screen '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle screen = VassalSharp.Info.getScreenBounds(frame);
			
			if (GlobalOptions.Instance.UseSingleWindow)
			{
				// FIXME: annoying!
				//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_javaawtPoint'"
				frame.Location = screen.Location;
				//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
				frame.Size = new System.Drawing.Size(screen.Width, screen.Height / 3);
			}
			else
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'key '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String key = "BoundsOfGameModule"; //$NON-NLS-1$
				//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle r = new System.Drawing.Rectangle(0, 0, screen.Width, screen.Height / 4);
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				getPrefs().addOption(new PositionOption(key, frame, ref r));
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'mess '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String mess = Resources.getString("BasicModule.version_message", getLocalizedGameName(), moduleVersion); //$NON-NLS-1$
			warn(mess);
			log.warn(mess);
			initFrameTitle();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void ensureComponent(Class < ? extends Buildable > componentClass)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void addComponent(Class < ? extends Buildable > componentClass)
		
		/*
		* The module I18n key prefix is null for the top level.
		*/
		public override System.String getI18nPrefix()
		{
			return "";
		}
		static BasicModule()
		{
			log = LoggerFactory.getLogger(typeof(BasicModule));
		}
	}
}