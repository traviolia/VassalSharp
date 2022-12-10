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
using PeerInfo = org.litesoft.p2pchat.PeerInfo;
using Player = VassalSharp.chat.Player;
using SimplePlayer = VassalSharp.chat.SimplePlayer;
using SimpleStatus = VassalSharp.chat.SimpleStatus;
using PropertiesEncoder = VassalSharp.tools.PropertiesEncoder;
namespace VassalSharp.chat.peer2peer
{
	
	public class P2PPlayer:SimplePlayer
	{
		virtual public System.String Room
		{
			get
			{
				return props.Get(ROOM);
			}
			
			set
			{
				props[(System.String) ROOM] = (System.String) value;
				setProps();
			}
			
		}
		virtual public PeerInfo Info
		{
			get
			{
				return info;
			}
			
		}
		private const System.String ID = "id"; //$NON-NLS-1$
		private const System.String ROOM = "room"; //$NON-NLS-1$
		
		private PeerInfo info;
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		private System.Collections.Specialized.NameValueCollection props;
		
		public P2PPlayer(PeerInfo info)
		{
			this.info = info;
			if (info.getChatName() != null)
			{
				try
				{
					props = new PropertiesEncoder(info.getChatName()).Properties;
					setStats();
				}
				// FIXME: review error message
				catch (System.IO.IOException ex)
				{
					//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
					//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
					props = new System.Collections.Specialized.NameValueCollection();
					setProps();
				}
			}
			else
			{
				//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
				//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
				props = new System.Collections.Specialized.NameValueCollection();
				setProps();
			}
		}
		
		public virtual void  setStats(Player p)
		{
			setName(p.getName());
			setStatus(p.getStatus());
			setId(p.getId());
			setProps();
		}
		
		private void  setProps()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SimpleStatus s = (SimpleStatus) status;
			props[(System.String) SimpleStatus.NAME] = (System.String) getName();
			props[(System.String) SimpleStatus.LOOKING] = (System.String) System.Convert.ToString(s.Looking);
			props[(System.String) SimpleStatus.AWAY] = (System.String) System.Convert.ToString(s.Away);
			props[(System.String) SimpleStatus.PROFILE] = (System.String) s.Profile;
			props[(System.String) SimpleStatus.IP] = (System.String) s.Ip;
			props[(System.String) SimpleStatus.CLIENT] = (System.String) s.Client;
			props[(System.String) SimpleStatus.MODULE_VERSION] = (System.String) s.ModuleVersion;
			props[(System.String) SimpleStatus.CRC] = (System.String) s.Crc;
			info.setChatName(new PropertiesEncoder(props).StringValue);
		}
		
		public virtual void  setProperty(System.String key, System.String value_Renamed)
		{
			//UPGRADE_TODO: Method 'java.util.Properties.setProperty' was converted to 'System.Collections.Specialized.NameValueCollection.Item' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiessetProperty_javalangString_javalangString'"
			props[key] = value_Renamed;
		}
		
		private void  setStats()
		{
			setName(props[SimpleStatus.NAME] == null?"???":props[SimpleStatus.NAME]); //$NON-NLS-1$
			setStatus(new SimpleStatus("true".Equals(props.Get(SimpleStatus.LOOKING)), "true".Equals(props.Get(SimpleStatus.AWAY)), props[SimpleStatus.PROFILE] == null?"":props[SimpleStatus.PROFILE], props[SimpleStatus.CLIENT] == null?"":props[SimpleStatus.CLIENT], props[SimpleStatus.IP] == null?"":props[SimpleStatus.IP], props[SimpleStatus.MODULE_VERSION] == null?"":props[SimpleStatus.MODULE_VERSION], props[SimpleStatus.CRC] == null?"":props[SimpleStatus.CRC])); //$NON-NLS-1$
		}
		
		public override System.String getId()
		{
			return props.Get(ID);
		}
		
		public override void  setId(System.String id)
		{
			props[(System.String) ID] = (System.String) id;
			setProps();
		}
		
		public  override bool Equals(System.Object o)
		{
			if (o is P2PPlayer)
			{
				P2PPlayer p = (P2PPlayer) o;
				return getId() == null?info.equals(p.info):getId().Equals(p.getId());
			}
			else
			{
				return false;
			}
		}
		
		public virtual System.String summary()
		{
			return getName() + " [looking = " + ((SimpleStatus) status).Looking + ", away = " + ((SimpleStatus) getStatus()).Away + ", room = " + props.Get(ROOM) + ", host = " + Info.Addresses + "]"; //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$ //$NON-NLS-4$ //$NON-NLS-5$
		}
		//UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1306'"
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}