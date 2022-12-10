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
namespace VassalSharp.chat.jabber
{
	
	/// <author>  rkinney
	/// </author>
	public class JabberClientFactory:ChatServerFactory
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(JabberClientFactory enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(JabberClientFactory enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private JabberClientFactory enclosingInstance;
			public JabberClientFactory Enclosing_Instance
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
				VassalSharp.chat.jabber.JabberClientFactory.logger.error("", mess);
			}
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.chat.jabber.JabberClientFactory'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		public const System.String JABBER_SERVER_TYPE = "jabber"; //$NON-NLS-1$
		public const System.String JABBER_PWD = "jabberPassword"; //$NON-NLS-1$
		public const System.String JABBER_LOGIN = "jabberLogin"; //$NON-NLS-1$
		public const System.String JABBER_PORT = "jabberPort"; //$NON-NLS-1$
		public const System.String JABBER_HOST = "jabberHost"; //$NON-NLS-1$
		public const System.String DEFAULT_JABBER_PORT = "5222"; //$NON-NLS-1$
		public const System.String DEFAULT_JABBER_HOST = "localhost"; //$NON-NLS-1$
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public override ChatServerConnection buildServer(System.Collections.Specialized.NameValueCollection serverConfig)
		{
			System.String host = serverConfig[JABBER_HOST] == null?DEFAULT_JABBER_HOST:serverConfig[JABBER_HOST];
			int port = 5222;
			try
			{
				port = System.Int32.Parse(serverConfig[JABBER_PORT] == null?DEFAULT_JABBER_PORT:serverConfig[JABBER_PORT]);
			}
			// FIXME: review error message
			catch (System.FormatException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
			}
			ModuleAccountInfo account = new ModuleAccountInfo(serverConfig.Get(JABBER_LOGIN), serverConfig.Get(JABBER_PWD));
			JabberClient client = new JabberClient(GameModule.getGameModule(), host, port, account);
			client.addPropertyChangeListener(VassalSharp.chat.ChatServerConnection_Fields.STATUS, new AnonymousClassPropertyChangeListener(this));
			client.addPropertyChangeListener(VassalSharp.chat.ChatServerConnection_Fields.INCOMING_MSG, new CommandDecoder());
			return client;
		}
		static JabberClientFactory()
		{
			logger = LoggerFactory.getLogger(typeof(JabberClientFactory));
		}
	}
}