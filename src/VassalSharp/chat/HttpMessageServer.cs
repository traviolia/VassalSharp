/*
* $Id$
*
* Copyright (c) 2004 by Rodney Kinney
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
//UPGRADE_TODO: The type 'org.apache.commons.lang.StringUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using StringUtils = org.apache.commons.lang.StringUtils;
using GameModule = VassalSharp.build.GameModule;
using Message = VassalSharp.chat.messageboard.Message;
using MessageBoard = VassalSharp.chat.messageboard.MessageBoard;
using PeerPoolInfo = VassalSharp.chat.peer2peer.PeerPoolInfo;
using Command = VassalSharp.command.Command;
using NullCommand = VassalSharp.command.NullCommand;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.chat
{
	
	public class HttpMessageServer : MessageBoard, WelcomeMessageServer
	{
		virtual public Command WelcomeMessage
		{
			get
			{
				Command motd = new NullCommand();
				try
				{
					if (GameModule.getGameModule() != null)
					{
						//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
						for(String s: welcomeURL.doGet(prepareInfo()))
						{
							motd = motd.append(GameModule.getGameModule().decode(s));
						}
					}
				}
				catch (System.IO.IOException e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.Console.Error.WriteLine("IOException retrieving welcome message from " + welcomeURL); //$NON-NLS-1$
				}
				return motd;
			}
			
		}
		virtual public Message[] Messages
		{
			get
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final ArrayList < Message > msgList = new ArrayList < Message >();
				try
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(String msg: getMessagesURL.doGet(prepareInfo()))
					{
						try
						{
							SupportClass.Tokenizer st = new StringTokenizer(msg, "&"); //$NON-NLS-1$
							System.String s = st.NextToken();
							System.String sender = s.Substring(s.IndexOf('=') + 1); //$NON-NLS-1$
							System.String date = st.NextToken();
							date = date.Substring(date.IndexOf('=') + 1); //$NON-NLS-1$
							s = st.NextToken(""); //$NON-NLS-1$
							
							System.String content = StringUtils.join(new SequenceEncoder.Decoder(s.Substring(s.IndexOf('=') + 1), '|'), '\n');
							
							content = restorePercent(content);
							//UPGRADE_TODO: The 'System.DateTime' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
							System.DateTime created = null;
							try
							{
								long time = System.Int64.Parse(date);
								System.TimeZone t = System.TimeZone.CurrentTimeZone;
								//UPGRADE_TODO: Method 'java.util.TimeZone.getOffset' was converted to 'System.TimeZone.GetUtcOffset' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
								time += ((t.GetUtcOffset(new System.DateTime(SupportClass.CalendarManager.YEAR, SupportClass.CalendarManager.MONTH, SupportClass.CalendarManager.DAY_OF_YEAR)).Ticks) / 10000);
								//UPGRADE_TODO: Constructor 'java.util.Date.Date' was converted to 'System.DateTime.DateTime' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilDateDate_long'"
								created = new System.DateTime(time);
							}
							catch (System.FormatException e1)
							{
								created = System.DateTime.Now;
							}
							//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
							msgList.add(new Message(sender, content, ref created));
						}
						catch (System.ArgumentOutOfRangeException ex)
						{
							System.Console.Error.WriteLine("Badly formatted message in HttpMessageServer:  " + msg); //$NON-NLS-1$
						}
					}
				}
				catch (System.IO.IOException ex)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.Console.Error.WriteLine("IOException retrieving messages from " + getMessagesURL); //$NON-NLS-1$
				}
				return msgList.toArray(new Message[msgList.size()]);
			}
			
		}
		private HttpRequestWrapper welcomeURL;
		private HttpRequestWrapper getMessagesURL;
		private HttpRequestWrapper postMessageURL;
		private PeerPoolInfo info;
		
		public HttpMessageServer(PeerPoolInfo info):this("http://www.vassalengine.org/util/getMessages", "http://www.vassalengine.org/util/postMessage", "http://www.vassalengine.org/util/motd", info)
		{
		}
		
		public HttpMessageServer(System.String getMessagesURL, System.String postMessageURL, System.String welcomeURL, PeerPoolInfo info)
		{
			this.getMessagesURL = new HttpRequestWrapper(getMessagesURL);
			this.welcomeURL = new HttpRequestWrapper(welcomeURL);
			this.postMessageURL = new HttpRequestWrapper(postMessageURL);
			this.info = info;
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		private System.Collections.Specialized.NameValueCollection prepareInfo()
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			System.Collections.Specialized.NameValueCollection p = new System.Collections.Specialized.NameValueCollection();
			p[(System.String) "module"] = (System.String) info.ModuleName; //$NON-NLS-1$
			return p;
		}
		
		private System.String removePercent(System.String input)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'buff '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder buff = new StringBuilder();
			//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SupportClass.Tokenizer st = new SupportClass.Tokenizer(input, "%#", true); //$NON-NLS-1$
			while (st.HasMoreTokens())
			{
				System.String s = st.NextToken();
				switch (s[0])
				{
					
					case '%': 
						buff.append("/#/"); //$NON-NLS-1$
						break;
					
					case '#': 
						buff.append("/##/"); //$NON-NLS-1$
						break;
					
					default: 
						buff.append(s);
						break;
					
				}
			}
			return buff.toString();
		}
		
		private System.String restorePercent(System.String input)
		{
			for (int i = input.IndexOf("/#/"); i >= 0; i = input.IndexOf("/#/"))
			{
				//$NON-NLS-1$
				input = input.Substring(0, (i) - (0)) + "%" + input.Substring(i + 3); //$NON-NLS-1$
			}
			for (int i = input.IndexOf("/##/"); i >= 0; i = input.IndexOf("/##/"))
			{
				//$NON-NLS-1$
				input = input.Substring(0, (i) - (0)) + "#" + input.Substring(i + 4); //$NON-NLS-1$
			}
			return input;
		}
		
		public virtual void  postMessage(System.String content)
		{
			if (content == null || content.Length == 0)
			{
				return ;
			}
			content = removePercent(content);
			SequenceEncoder se = new SequenceEncoder('|');
			SupportClass.Tokenizer st = new SupportClass.Tokenizer(content, "\n\r"); //$NON-NLS-1$
			while (st.HasMoreTokens())
			{
				se.append(st.NextToken());
			}
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Collections.Specialized.NameValueCollection p = prepareInfo();
			p[(System.String) "sender"] = (System.String) info.UserName; //$NON-NLS-1$
			p[(System.String) "content"] = (System.String) se.Value; //$NON-NLS-1$
			try
			{
				postMessageURL.doPost(p);
			}
			// FIXME: review error message
			catch (System.IO.IOException ex)
			{
				SupportClass.WriteStackTrace(ex, Console.Error);
			}
		}
	}
}