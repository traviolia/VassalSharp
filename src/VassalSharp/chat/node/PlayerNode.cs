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
/*
* Copyright (c) 2003 by Rodney Kinney.  All rights reserved.
* Date: May 9, 2003
*/
using System;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.chat.node
{
	
	/// <summary> Node representing a single player.
	/// A leaf node in a hierarchical server.
	/// Reads and writes directly to a socket
	/// {@link #getInfo} returns an encoded {@link java.util.Properties} object with real name, profile, etc.
	/// </summary>
	public class PlayerNode:Node, SocketWatcher
	{
		override public System.String Id
		{
			get
			{
				return id;
			}
			
		}
		override public bool Leaf
		{
			get
			{
				return true;
			}
			
		}
		private SocketHandler input;
		protected internal System.String id;
		protected internal System.String info;
		private AsynchronousServerNode server;
		private static ConnectionLimiter connLimiter = new ConnectionLimiter();
		
		public PlayerNode(System.Net.Sockets.TcpClient socket, AsynchronousServerNode server):base(null, null, null)
		{
			this.server = server;
			this.input = new BufferedSocketHandler(socket, this);
			input.start();
		}
		
		public override void  send(System.String msg)
		{
			input.writeLine(msg);
		}
		
		// Always update IP on client info in case client 'forgets' their IP
		public override System.String getInfo()
		{
			//UPGRADE_ISSUE: Method 'java.net.Socket.getInetAddress' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetSocketgetInetAddress'"
			System.String ip = input.sock.getInetAddress().ToString();
			return info + (ip.Length > 0?"|ip=" + ip:"");
		}
		
		public  override bool Equals(System.Object o)
		{
			if (this == o)
				return true;
			if (!(o is PlayerNode))
				return false;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'player '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			PlayerNode player = (PlayerNode) o;
			
			if (!id.Equals(player.id))
				return false;
			
			return true;
		}
		
		public override int GetHashCode()
		{
			return id.GetHashCode();
		}
		
		public virtual void  handleMessage(System.String line)
		{
			System.String[] info;
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Collections.Specialized.NameValueCollection p;
			System.String cmd;
			if ((info = Protocol.decodeRegisterCommand(line)) != null)
			{
				id = info[0];
				this.info = info[2];
				server.registerNode(info[1], this);
			}
			else if ((info = Protocol.decodeJoinCommand(line)) != null)
			{
				// Requests to move to a locked room must be accompanied by the rooms 'password' which
				// is the owner of the room. This allows 'Invited' clients to join Locked rooms.
				// Rooms are reused, so clients are allowed to enter an empty locked room without a password.
				//UPGRADE_NOTE: Final was removed from the declaration of 'sd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(info[0], '/');
				sd.nextToken("");
				//UPGRADE_NOTE: Final was removed from the declaration of 'joinRoomName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String joinRoomName = sd.nextToken("");
				//UPGRADE_NOTE: Final was removed from the declaration of 'room '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Node room = server.getModule(this).getDescendant(joinRoomName);
				if (room != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'locked '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					bool locked = "true".Equals(room.getInfoProperty(NodeRoom.LOCKED));
					if (locked && room.Children.Length > 0)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'owner '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String owner = room.getInfoProperty(NodeRoom.OWNER);
						if (info.Length < 2 || !owner.Equals(info[1]))
						{
							return ;
						}
					}
				}
				server.move(this, info[0]);
			}
			else if ((info = Protocol.decodeForwardCommand(line)) != null)
			{
				server.forward(info[0], info[1]);
			}
			else if ((info = Protocol.decodeStatsCommand(line)) != null)
			{
				this.info = info[0];
				server.updateInfo(this);
			}
			else if ((info = Protocol.decodeKickCommand(line)) != null)
			{
				server.kick(this, info[0]);
			}
			else if ((p = Protocol.decodeRoomsInfo(line)) != null)
			{
				// FIXME: Use Properties.stringPropertyNames() in 1.6+.
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
			}
			else if ((cmd = Protocol.decodeLoginCommand(line)) != null)
			{
				connLimiter.register(cmd, input);
			}
		}
		
		public virtual void  socketClosed(SocketHandler handler)
		{
			server.disconnect(this);
		}
	}
}