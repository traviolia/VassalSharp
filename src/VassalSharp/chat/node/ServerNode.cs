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
//UPGRADE_TODO: The type 'java.util.logging.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = java.util.logging.Logger;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
using PropertiesEncoder = VassalSharp.tools.PropertiesEncoder;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.chat.node
{
	
	public class ServerNode:Node
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassMsgSender' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassMsgSender : MsgSender
		{
			public AnonymousClassMsgSender(VassalSharp.chat.node.MsgSender[] senders, ServerNode enclosingInstance)
			{
				InitBlock(senders, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.chat.node.MsgSender[] senders, ServerNode enclosingInstance)
			{
				this.senders = senders;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable senders was copied into class AnonymousClassMsgSender. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.chat.node.MsgSender[] senders;
			private ServerNode enclosingInstance;
			public ServerNode Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  send(System.String msg)
			{
				for (int i = 0; i < senders.Length; ++i)
				{
					senders[i].send(msg);
				}
			}
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.chat.node.ServerNode'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		private SendContentsTask sendContents_Renamed_Field;
		
		public ServerNode():base(null, null, null)
		{
			sendContents_Renamed_Field = new SendContentsTask();
			System.Timers.Timer t = new System.Timers.Timer();
			//UPGRADE_ISSUE: Method 'java.util.Timer.schedule' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilTimerschedule_javautilTimerTask_long_long'"
			t.schedule(sendContents_Renamed_Field, 0, 1000);
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'forward'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  forward(System.String senderPath, System.String msg)
		{
			lock (this)
			{
				MsgSender target = getMsgSender(senderPath);
				target.send(msg);
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'getMsgSender'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual MsgSender getMsgSender(System.String path)
		{
			lock (this)
			{
				Node[] target = new Node[]{this};
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(path, '/');
				while (st.hasMoreTokens())
				{
					System.String childId = st.nextToken();
					if ("*".Equals(childId))
					{
						//$NON-NLS-1$
						//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
						ArrayList < Node > l = new ArrayList < Node >();
						//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
						for(Node node: target)
						{
							l.addAll(Arrays.asList(node.getChildren()));
						}
						target = l.toArray(new Node[l.size()]);
					}
					else if (childId.StartsWith("~"))
					{
						//$NON-NLS-1$
						childId = childId.Substring(1);
						//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
						ArrayList < Node > l = new ArrayList < Node >();
						//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
						for(Node node: target)
						{
							//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
							for(Node child: node.getChildren())
							{
								if (!childId.Equals(child.getId()))
								{
									l.add(child);
								}
							}
						}
						target = l.toArray(new Node[l.size()]);
					}
					else
					{
						int i = 0;
						int n = target.Length;
						for (; i < n; ++i)
						{
							Node child = target[i].getChild(childId);
							if (child != null)
							{
								target = new Node[]{child};
								break;
							}
						}
						if (i == n)
						{
							target = new Node[0];
						}
					}
				}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'senders '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				MsgSender[] senders = ArrayUtils.copyOf(target);
				
				return new AnonymousClassMsgSender(senders, this);
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'disconnect'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  disconnect(Node target)
		{
			lock (this)
			{
				Node mod = getModule(target);
				if (mod != null)
				{
					Node room = target.Parent;
					room.remove(target);
					if (room.Children.Length == 0)
					{
						room.Parent.remove(room);
					}
					if (mod.Children.Length == 0)
					{
						remove(mod);
					}
					sendContents(mod);
				}
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'sendContents'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		protected internal virtual void  sendContents(Node module)
		{
			lock (this)
			{
				sendContents_Renamed_Field.markChanged(module);
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'registerNode'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  registerNode(System.String parentPath, Node newNode)
		{
			lock (this)
			{
				Node newParent = Node.build(this, parentPath);
				newParent.add(newNode);
				Node module = getModule(newParent);
				if (module != null)
				{
					sendContents(module);
				}
			}
		}
		
		public virtual Node getModule(Node n)
		{
			Node module = n;
			while (module != null && module.Parent != this)
			{
				module = module.Parent;
			}
			return module;
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'move'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  move(Node target, System.String newParentPath)
		{
			lock (this)
			{
				Node oldMod = getModule(target);
				Node newParent = Node.build(this, newParentPath);
				newParent.add(target);
				Node mod = getModule(newParent);
				if (mod != null)
				{
					sendContents(mod);
				}
				if (oldMod != mod && oldMod != null)
				{
					sendContents(oldMod);
				}
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'updateInfo'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  updateInfo(Node target)
		{
			lock (this)
			{
				Node mod = getModule(target);
				if (mod != null)
				{
					sendContents(mod);
				}
			}
		}
		
		/// <summary> One client has requested to kick another out of a room. Validate that - Both players are in the same room - Kicker
		/// is the owner of the room
		/// 
		/// </summary>
		/// <param name="kickerId">Id of Kicking player
		/// </param>
		/// <param name="kickeeId">Id of Player to be kicked
		/// </param>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'kick'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  kick(PlayerNode kicker, System.String kickeeId)
		{
			lock (this)
			{
				// Check the kicker owns the room he is in
				//UPGRADE_NOTE: Final was removed from the declaration of 'roomNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Node roomNode = kicker.Parent;
				System.String roomOwnerId;
				try
				{
					roomOwnerId = new PropertiesEncoder(roomNode.getInfo()).Properties.Get(NodeRoom.OWNER);
				}
				catch (System.IO.IOException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
					return ;
				}
				if (roomOwnerId == null || !roomOwnerId.Equals(kicker.Id))
				{
					return ;
				}
				// Check the kickee belongs to the same room
				//UPGRADE_NOTE: Final was removed from the declaration of 'kickeeNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Node kickeeNode = roomNode.getChild(kickeeId);
				if (kickeeNode == null)
				{
					return ;
				}
				// Kick to the default room and tell them they have been kicked
				//UPGRADE_NOTE: Final was removed from the declaration of 'defaultRoomNode '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Node defaultRoomNode = roomNode.Parent.Children[0];
				move(kickeeNode, defaultRoomNode.Path);
			}
		}
		//UPGRADE_ISSUE: Class 'java.util.TimerTask' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilTimerTask'"
		private class SendContentsTask:TimerTask
		{
			// FIXME: should modules be wrapped by Collections.synchronizedMap()?
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private Set < Node > modules = new HashSet < Node >();
			
			public virtual void  markChanged(Node module)
			{
				lock (modules)
				{
					modules.add(module);
				}
			}
			
			public override void  Run()
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				HashSet < Node > s = new HashSet < Node >();
				lock (modules)
				{
					s.addAll(modules);
				}
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Node module: s)
				{
					VassalSharp.chat.node.ServerNode.logger.fine("Sending contents of " + module.getId()); //$NON-NLS-1$
					Node[] players = module.getLeafDescendants();
					Node[] rooms = module.getChildren();
					System.String listCommand = Protocol.encodeListCommand(players);
					module.send(listCommand);
					VassalSharp.chat.node.ServerNode.logger.finer(listCommand);
					System.String roomInfo = Protocol.encodeRoomsInfo(rooms);
					module.send(roomInfo);
					VassalSharp.chat.node.ServerNode.logger.finer(roomInfo);
				}
				lock (modules)
				{
					modules.clear();
				}
			}
		}
		static ServerNode()
		{
			logger = Logger.getLogger(typeof(ServerNode).FullName);
		}
	}
}