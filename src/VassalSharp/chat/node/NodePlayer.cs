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
* Date: May 11, 2003
*/
using System;
using Chatter = VassalSharp.build.module.Chatter;
using SimplePlayer = VassalSharp.chat.SimplePlayer;
using SimpleStatus = VassalSharp.chat.SimpleStatus;
namespace VassalSharp.chat.node
{
	
	/// <summary> A {@link SimplePlayer} subclass used in clients of the hierarchical server</summary>
	public class NodePlayer:SimplePlayer
	{
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		virtual public System.Collections.Specialized.NameValueCollection Info
		{
			set
			{
				name = value[SimpleStatus.NAME] == null?"???":value[SimpleStatus.NAME]; //$NON-NLS-1$
				if (name == null || name.Length == 0 || name.Trim().Length == 0 || name.Equals("<nobody>"))
				{
					name = "(" + Chatter.AnonymousUserName + ")";
				}
				id = value[ID] == null?id:value[ID];
				setStatus(new SimpleStatus("true".Equals(value.Get(SimpleStatus.LOOKING)), "true".Equals(value.Get(SimpleStatus.AWAY)), value[SimpleStatus.PROFILE] == null?"":value[SimpleStatus.PROFILE], value[SimpleStatus.CLIENT] == null?"":value[SimpleStatus.CLIENT], value[SimpleStatus.IP] == null?"":value[SimpleStatus.IP], value[SimpleStatus.MODULE_VERSION] == null?"":value[SimpleStatus.MODULE_VERSION], value[SimpleStatus.CRC] == null?"":value[SimpleStatus.CRC])); //$NON-NLS-1$
			}
			
		}
		public const System.String ID = "id"; //$NON-NLS-1$
		
		public NodePlayer(System.String id)
		{
			this.id = id;
		}
		
		public override System.String getId()
		{
			return id;
		}
		
		public  override bool Equals(System.Object o)
		{
			if (this == o)
				return true;
			if (!(o is NodePlayer))
				return false;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'hPlayer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			NodePlayer hPlayer = (NodePlayer) o;
			
			if (id != null?!id.Equals(hPlayer.id):hPlayer.id != null)
				return false;
			
			return true;
		}
		
		public override int GetHashCode()
		{
			return (id != null?id.GetHashCode():0);
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual System.Collections.Specialized.NameValueCollection toProperties()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'p1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			System.Collections.Specialized.NameValueCollection p1 = new System.Collections.Specialized.NameValueCollection();
			if (name != null)
			{
				p1[(System.String) SimpleStatus.NAME] = (System.String) name;
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'status '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SimpleStatus status = (SimpleStatus) getStatus();
			p1[(System.String) SimpleStatus.LOOKING] = (System.String) System.Convert.ToString(status.Looking);
			p1[(System.String) SimpleStatus.AWAY] = (System.String) System.Convert.ToString(status.Away);
			//UPGRADE_NOTE: Final was removed from the declaration of 'profile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String profile = status.Profile;
			if (profile != null)
			{
				p1[(System.String) SimpleStatus.PROFILE] = (System.String) profile;
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'client '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String client = status.Client;
			if (client != null)
			{
				p1[(System.String) SimpleStatus.CLIENT] = (System.String) client;
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'ip '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String ip = status.Ip;
			if (ip != null)
			{
				p1[(System.String) SimpleStatus.IP] = (System.String) ip;
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'moduleVersion '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String moduleVersion = status.ModuleVersion;
			if (moduleVersion != null)
			{
				p1[(System.String) SimpleStatus.MODULE_VERSION] = (System.String) moduleVersion;
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'crc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String crc = status.Crc;
			if (ip != null)
			{
				p1[(System.String) SimpleStatus.CRC] = (System.String) crc;
			}
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Collections.Specialized.NameValueCollection p = p1;
			p[(System.String) ID] = (System.String) (id == null?"":id);
			return p;
		}
	}
}