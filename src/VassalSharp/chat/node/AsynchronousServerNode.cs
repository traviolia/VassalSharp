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
/*
* Copyright (c) 2003 by Rodney Kinney.  All rights reserved.
* Date: May 9, 2003
*/
using System;
//UPGRADE_TODO: The type 'java.util.logging.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = java.util.logging.Logger;
using HttpRequestWrapper = VassalSharp.chat.HttpRequestWrapper;
using PropertiesEncoder = VassalSharp.tools.PropertiesEncoder;
namespace VassalSharp.chat.node
{
	
	
	/// <summary> Root node in a hierarchical server.
	/// Represents the server process itself.
	/// Children represent modules.
	/// Children of modules represent rooms.
	/// Children of rooms represent players.
	/// </summary>
	public class AsynchronousServerNode:ServerNode
	{
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.chat.node.AsynchronousServerNode'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static Logger logger;
		private StatusReporter statusReporter;
		private ReportContentsThread contentsReporter;
		
		public AsynchronousServerNode(System.String url):base()
		{
			init(url);
		}
		
		protected internal virtual void  init(System.String url)
		{
			statusReporter = new StatusReporter(url == null?null:new HttpRequestWrapper(url), this);
			contentsReporter = new ReportContentsThread(this);
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'sendContents'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		protected internal override void  sendContents(Node node)
		{
			lock (this)
			{
				contentsReporter.markChanged(node);
			}
		}
		
		public class ReportContentsThread:SupportClass.ThreadClass
		{
			private AsynchronousServerNode server;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private Set < Node > changed;
			private long lastGlobalUpdate;
			private const long GLOBAL_UPDATE_INTERVAL = 1000L * 120L;
			
			public ReportContentsThread(AsynchronousServerNode server)
			{
				this.server = server;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				changed = new HashSet < Node >();
				Start();
			}
			
			override public void  Run()
			{
				while (true)
				{
					try
					{
						lock (this)
						{
							System.Threading.Monitor.Wait(this);
							sendContents();
						}
					}
					catch (System.Threading.ThreadInterruptedException e)
					{
					}
				}
			}
			
			//UPGRADE_NOTE: Synchronized keyword was removed from method 'sendContents'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
			private void  sendContents()
			{
				lock (this)
				{
					server.statusReporter.updateContents(server.LeafDescendants);
					long time = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;;
					if (time - lastGlobalUpdate < GLOBAL_UPDATE_INTERVAL)
					{
						//UPGRADE_TODO: Method 'java.util.Arrays.asList' was converted to 'System.Collections.ArrayList' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilArraysasList_javalangObject[]'"
						modules = new System.Collections.ArrayList(server.Children).GetEnumerator();
						lastGlobalUpdate = time;
					}
					else
					{
						modules = changed.iterator();
					}
					while (modules.hasNext())
					{
						Node module = modules.next();
						VassalSharp.chat.node.AsynchronousServerNode.logger.fine("Sending contents of " + module.Id); //$NON-NLS-1$
						Node[] players = module.LeafDescendants;
						Node[] rooms = module.Children;
						
						// Check if any rooms have lost their first player
						for (int i = 1; i < rooms.Length; i++)
						{
							Node[] c = rooms[i].Children;
							if (c.Length > 0)
							{
								try
								{
									//UPGRADE_NOTE: Final was removed from the declaration of 'roomProps '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
									//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
									System.Collections.Specialized.NameValueCollection roomProps = new PropertiesEncoder(rooms[i].getInfo()).Properties;
									//UPGRADE_NOTE: Final was removed from the declaration of 'roomOwner '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
									System.String roomOwner = roomProps.Get("owner");
									//UPGRADE_NOTE: Final was removed from the declaration of 'playerId '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
									System.String playerId = new PropertiesEncoder(c[0].getInfo()).Properties.Get("id");
									if (roomOwner == null || (!roomOwner.Equals(playerId)))
									{
										//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
										roomProps["owner"] = playerId;
										rooms[i].setInfo(new PropertiesEncoder(roomProps).ToString());
									}
								}
								catch (System.IO.IOException e)
								{
									// Error encoding/decoding properties. Shouldn't happen.
									SupportClass.WriteStackTrace(e, Console.Error);
								}
							}
						}
						
						System.String listCommand = Protocol.encodeListCommand(players);
						VassalSharp.chat.node.AsynchronousServerNode.logger.finer(listCommand);
						module.send(listCommand);
						System.String roomInfo = Protocol.encodeRoomsInfo(rooms);
						module.send(roomInfo);
					}
					changed.clear();
				}
			}
			
			//UPGRADE_NOTE: Synchronized keyword was removed from method 'markChanged'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
			public virtual void  markChanged(Node module)
			{
				lock (this)
				{
					VassalSharp.chat.node.AsynchronousServerNode.logger.fine(module + " has changed"); //$NON-NLS-1$
					changed.add(module);
					System.Threading.Monitor.PulseAll(this);
				}
			}
		}
		static AsynchronousServerNode()
		{
			logger = Logger.getLogger(typeof(AsynchronousServerNode).FullName);
		}
	}
}