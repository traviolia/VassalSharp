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
* Date: May 14, 2003
*/
using System;
using PropertiesEncoder = VassalSharp.tools.PropertiesEncoder;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.chat.node
{
	
	/// <summary> Utility method for encoding server-related commands into strings. Messages
	/// sent or interpreted by the server are encoded here. Messages sent from one
	/// client to another are simply forwarded as strings without being decoded.
	/// </summary>
	public class Protocol
	{
		public const System.String REGISTER = "REG\t"; //$NON-NLS-1$
		public const System.String REG_REQUEST = "REG_REQUEST\t"; //$NON-NLS-1$
		public const System.String JOIN = "JOIN\t"; //$NON-NLS-1$
		public const System.String FORWARD = "FWD\t"; //$NON-NLS-1$
		public const System.String STATS = "STATS\t"; //$NON-NLS-1$
		public const System.String LIST = "LIST\t"; //$NON-NLS-1$
		public const System.String CONTENTS = "SERVER_CONTENTS\t"; //$NON-NLS-1$
		public const System.String NODE_INFO = "NODE_INFO\t"; //$NON-NLS-1$
		public const System.String ROOM_INFO = "ROOM_INFO\t"; //$NON-NLS-1$
		public const System.String LOGIN = "LOGIN\t"; //$NON-NLS-1$
		public const System.String KICK = "KICK\t"; //$NON-NLS-1$
		
		/// <summary> Contains registration information sent when a client initially connects to
		/// the server
		/// 
		/// </summary>
		/// <param name="id">
		/// </param>
		/// <param name="initialPath">
		/// </param>
		/// <param name="info">
		/// </param>
		/// <returns>
		/// </returns>
		public static System.String encodeRegisterCommand(System.String id, System.String initialPath, System.String info)
		{
			System.String msg = new SequenceEncoder(id, '\t').append(initialPath).append(info).Value;
			return REGISTER + msg;
		}
		
		/// <seealso cref="encodeRegisterCommand">
		/// </seealso>
		/// <returns> senderId, newParentPath, senderInfo
		/// </returns>
		public static System.String[] decodeRegisterCommand(System.String cmd)
		{
			System.String[] info = null;
			if (cmd.StartsWith(REGISTER))
			{
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(cmd.Substring(REGISTER.Length), '\t');
				info = new System.String[]{st.nextToken(), st.nextToken(), st.nextToken()};
			}
			return info;
		}
		
		/// <summary> Sent when a player wishes to join a room
		/// 
		/// </summary>
		/// <param name="newParentPath">
		/// </param>
		/// <returns>
		/// </returns>
		public static System.String encodeJoinCommand(System.String newParentPath)
		{
			return JOIN + newParentPath;
		}
		
		/// <summary> Sent when a player is invited to join a locked room
		/// 
		/// </summary>
		/// <param name="newParentPath">
		/// </param>
		/// <param name="password">
		/// </param>
		/// <returns>
		/// </returns>
		public static System.String encodeJoinCommand(System.String newParentPath, System.String password)
		{
			return JOIN + newParentPath + "\t" + password;
		}
		
		/// <seealso cref="encodeJoinCommand">
		/// </seealso>
		/// <returns> newParentPath
		/// </returns>
		public static System.String[] decodeJoinCommand(System.String cmd)
		{
			System.String[] info = null;
			if (cmd.StartsWith(JOIN))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'parts '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String[] parts = cmd.split("\\t");
				if (parts.Length == 2)
				{
					info = new System.String[]{parts[1]};
				}
				else if (parts.Length == 3)
				{
					info = new System.String[]{parts[1], parts[2]};
				}
			}
			return info;
		}
		
		/// <summary> Forward a message to other client nodes
		/// 
		/// </summary>
		/// <param name="recipientPath">a path name specifying the indented recipients of the message
		/// </param>
		/// <param name="message">
		/// </param>
		/// <returns>
		/// </returns>
		public static System.String encodeForwardCommand(System.String recipientPath, System.String message)
		{
			System.String msg = new SequenceEncoder(recipientPath, '\t').append(message).Value;
			return FORWARD + msg;
		}
		
		/// <seealso cref="encodeForwardCommand">
		/// </seealso>
		/// <returns> recipientPath, message
		/// </returns>
		public static System.String[] decodeForwardCommand(System.String cmd)
		{
			System.String[] info = null;
			if (cmd.StartsWith(FORWARD))
			{
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(cmd.Substring(FORWARD.Length), '\t');
				info = new System.String[]{st.nextToken(), st.nextToken()};
			}
			return info;
		}
		
		/// <summary> Sent when a player updates his personal information
		/// 
		/// </summary>
		/// <param name="info">
		/// the encoded properties of the {@link PlayerNode} corresponding to
		/// the player
		/// </param>
		/// <seealso cref="Node.setInfo">
		/// </seealso>
		/// <returns>
		/// </returns>
		public static System.String encodeStatsCommand(System.String info)
		{
			return STATS + info;
		}
		
		/// <seealso cref="encodeStatsCommand">
		/// </seealso>
		/// <returns> path, playerProperties
		/// </returns>
		public static System.String[] decodeStatsCommand(System.String cmd)
		{
			System.String[] info = null;
			if (cmd.StartsWith(STATS))
			{
				info = new System.String[]{cmd.Substring(STATS.Length)};
			}
			return info;
		}
		
		/// <summary> Sent when the info for a particular (non-player) node is updated
		/// 
		/// </summary>
		/// <param name="n">
		/// </param>
		/// <returns>
		/// </returns>
		public static System.String encodeNodeInfoCommand(Node n)
		{
			System.String info = n.getInfo();
			if (info == null)
			{
				info = ""; //$NON-NLS-1$
			}
			return NODE_INFO + new SequenceEncoder(n.Path, '=').append(info).Value;
		}
		
		/// <summary> </summary>
		/// <param name="cmd">
		/// </param>
		/// <returns> path, info
		/// </returns>
		/// <seealso cref="encodeNodeInfoCommand">
		/// </seealso>
		public static System.String[] decodeNodeInfoCommand(System.String cmd)
		{
			System.String[] s = null;
			if (cmd.StartsWith(NODE_INFO))
			{
				s = new System.String[2];
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(cmd.Substring(NODE_INFO.Length), '=');
				s[0] = st.nextToken();
				s[1] = st.nextToken();
			}
			return s;
		}
		
		/// <summary> Sent when a room's info changes, or to update all rooms' info at once
		/// 
		/// </summary>
		/// <param name="rooms">
		/// </param>
		/// <returns>
		/// </returns>
		public static System.String encodeRoomsInfo(Node[] rooms)
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			System.Collections.Specialized.NameValueCollection p = new System.Collections.Specialized.NameValueCollection();
			for (int i = 0; i < rooms.Length; ++i)
			{
				if (rooms[i].getInfo() != null && rooms[i].getInfo().Length > 0)
				{
					//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
					p[rooms[i].Id] = rooms[i].getInfo();
				}
			}
			System.String value_Renamed = new PropertiesEncoder(p).StringValue;
			return value_Renamed == null?ROOM_INFO:ROOM_INFO + value_Renamed;
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public static System.Collections.Specialized.NameValueCollection decodeRoomsInfo(System.String cmd)
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Collections.Specialized.NameValueCollection p = null;
			if (cmd.StartsWith(ROOM_INFO))
			{
				try
				{
					p = new PropertiesEncoder(cmd.Substring(ROOM_INFO.Length)).Properties;
				}
				// FIXME: review error message
				catch (System.IO.IOException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
				}
			}
			return p;
		}
		
		/// <summary> A dump of the current connections to the server. Includes a path name and
		/// info for each player node
		/// 
		/// </summary>
		/// <param name="nodes">
		/// </param>
		/// <returns>
		/// </returns>
		public static System.String encodeListCommand(Node[] nodes)
		{
			SequenceEncoder list = new SequenceEncoder('\t');
			for (int i = 0; i < nodes.Length; ++i)
			{
				if (nodes[i].Path != null && nodes[i].getInfo() != null)
				{
					SequenceEncoder info = new SequenceEncoder('=');
					info.append(nodes[i].Path).append(nodes[i].getInfo());
					list.append(info.Value);
				}
			}
			System.String value_Renamed = list.Value;
			return value_Renamed == null?LIST:LIST + value_Renamed;
		}
		
		/// <seealso cref="encodeListCommand">
		/// </seealso>
		/// <param name="cmd">
		/// </param>
		/// <returns>
		/// </returns>
		public static Node decodeListCommand(System.String cmd)
		{
			Node node = null;
			if (cmd.StartsWith(LIST))
			{
				Node root = new Node(null, null, null);
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(cmd.Substring(LIST.Length), '\t');
				while (st.hasMoreTokens())
				{
					System.String nodeInfo = st.nextToken();
					SequenceEncoder.Decoder st2 = new SequenceEncoder.Decoder(nodeInfo, '=');
					System.String path = st2.nextToken();
					System.String info = st2.nextToken();
					Node.build(root, path).setInfo(info);
				}
				node = root;
			}
			return node;
		}
		
		public static bool decodeRegisterRequest(System.String cmd)
		{
			return cmd.StartsWith(REG_REQUEST);
		}
		
		public static System.String encodeRegisterRequest()
		{
			return REG_REQUEST;
		}
		
		/// <summary> Sent when associating a connection with a given username
		/// 
		/// </summary>
		/// <param name="username">
		/// </param>
		/// <seealso cref="ConnectionLimiter">
		/// </seealso>
		public static System.String encodeLoginCommand(System.String username)
		{
			return LOGIN + username;
		}
		
		public static System.String decodeLoginCommand(System.String cmd)
		{
			System.String username = null;
			if (cmd.StartsWith(LOGIN))
			{
				username = cmd.Substring(LOGIN.Length);
			}
			return username;
		}
		
		/// <summary> Sent by the owner of a room to kick another player back to the Main Room
		/// 
		/// </summary>
		/// <param name="p">
		/// </param>
		/// <returns>
		/// </returns>
		public static System.String encodeKickCommand(System.String kickeeId)
		{
			return KICK + kickeeId;
		}
		
		public static System.String[] decodeKickCommand(System.String cmd)
		{
			System.String[] player = null;
			if (cmd.StartsWith(KICK))
			{
				player = new System.String[]{cmd.Substring(KICK.Length)};
			}
			return player;
		}
		
		/// <summary> A dump of the current connections to the server. Includes a path name and
		/// info for each player node, and info for each room node as well
		/// 
		/// </summary>
		/// <param name="nodes">
		/// </param>
		/// <returns> public static String encodeContentsCommand(Node[] nodes) {
		/// SequenceEncoder list = new SequenceEncoder('\t'); for (int i = 0; i <
		/// nodes.length; ++i) { list.append(nodes[i].getPathAndInfo()); }
		/// return CONTENTS + list.getValue(); }
		/// </returns>
		
		/// <seealso cref="encodeContentsCommand">
		/// </seealso>
		/// <param name="cmd">
		/// </param>
		/// <returns> public static Node decodeContentsCommand(String cmd) { Node node =
		/// null; if (cmd.startsWith(CONTENTS)) { Node root = new Node(null,
		/// null, null); SequenceEncoder.Decoder st = new
		/// SequenceEncoder.Decoder(cmd.substring(CONTENTS.length()), '\t');
		/// while (st.hasMoreTokens()) { String pathAndInfo = st.nextToken();
		/// Node.build(root, pathAndInfo); } node = root; } return node; }
		/// </returns>
	}
}