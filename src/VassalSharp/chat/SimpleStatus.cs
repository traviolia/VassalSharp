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
using Info = VassalSharp.Info;
using GameModule = VassalSharp.build.GameModule;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.chat
{
	
	/// <summary> Immutable PlayerStatus class with flags indicating "looking for a game" and "away from keyboard" and a String profile
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class SimpleStatus : PlayerStatus
	{
		virtual public bool Away
		{
			get
			{
				return away;
			}
			
		}
		virtual public bool Looking
		{
			get
			{
				return looking;
			}
			
		}
		virtual public System.String Profile
		{
			get
			{
				return profile;
			}
			
		}
		virtual public System.String Client
		{
			get
			{
				return client;
			}
			
		}
		virtual public System.String Ip
		{
			get
			{
				return ip;
			}
			
		}
		virtual public System.String ModuleVersion
		{
			get
			{
				return moduleVersion;
			}
			
		}
		virtual public System.String Crc
		{
			get
			{
				return crc;
			}
			
		}
		
		public const System.String CRC = "crc"; //$NON-NLS-1$
		public const System.String MODULE_VERSION = "moduleVersion"; //$NON-NLS-1$
		public const System.String IP = "ip"; //$NON-NLS-1$
		public const System.String CLIENT = "client"; //$NON-NLS-1$
		public const System.String PROFILE = "profile"; //$NON-NLS-1$
		public const System.String AWAY = "away"; //$NON-NLS-1$
		public const System.String LOOKING = "looking"; //$NON-NLS-1$
		public const System.String NAME = "name"; //$NON-NLS-1$
		
		private bool looking;
		private bool away;
		private System.String profile;
		private System.String client;
		private System.String ip;
		private System.String moduleVersion;
		private System.String crc;
		
		public SimpleStatus():this(false, false, "")
		{ //$NON-NLS-1$
		}
		
		public SimpleStatus(bool looking, bool away):this(looking, away, "")
		{ //$NON-NLS-1$
		}
		
		public SimpleStatus(bool looking, bool away, System.String profile):this(looking, away, profile, "", "", "", "")
		{ //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$ //$NON-NLS-4$
		}
		
		public SimpleStatus(bool looking, bool away, System.String profile, System.String client, System.String ip, System.String module, System.String crc)
		{
			this.looking = looking;
			this.away = away;
			this.profile = profile;
			this.client = client;
			this.ip = ip;
			this.moduleVersion = module;
			this.crc = crc;
		}
		
		public static System.String encode(SimpleStatus s)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(',');
			se.append(s.looking);
			se.append(s.away);
			se.append(s.profile);
			se.append(s.client);
			se.append(s.ip);
			se.append(s.moduleVersion);
			se.append(s.crc);
			return se.Value;
		}
		
		public static SimpleStatus decode(System.String s)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'sd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, ',');
			return new SimpleStatus(sd.nextBoolean(false), sd.nextBoolean(false), sd.nextToken(""), sd.nextToken(""), sd.nextToken(""), sd.nextToken(""), sd.nextToken("")); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
		}
		
		/// <summary> Update variable parts of status</summary>
		public virtual void  updateStatus()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GameModule g = GameModule.getGameModule();
			profile = ((System.String) g.getPrefs().getValue(GameModule.PERSONAL_INFO));
			client = Info.Version;
			ip = ""; //$NON-NLS-1$
			try
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.net.InetAddress.getLocalHost' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				ip = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString();
			}
			catch (System.Exception e)
			{
			}
			moduleVersion = g.getGameVersion() + ((g.getArchiveWriter() == null)?"":" (Editing)"); //$NON-NLS-1$ //$NON-NLS-2$
			crc = Long.toHexString(g.Crc);
		}
	}
}