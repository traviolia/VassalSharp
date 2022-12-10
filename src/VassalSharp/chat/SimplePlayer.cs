/*
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
using Chatter = VassalSharp.build.module.Chatter;
namespace VassalSharp.chat
{
	
	
	/// <summary> Simple Player bean</summary>
	public class SimplePlayer : VassalSharp.chat.Player
	{
		protected internal System.String id;
		protected internal System.String name;
		protected internal PlayerStatus status;
		
		public SimplePlayer(System.String id, System.String name, PlayerStatus status):base()
		{
			this.id = id;
			this.name = name;
			this.status = status;
		}
		
		public SimplePlayer(System.String name):this(name, name, new SimpleStatus())
		{
		}
		
		public SimplePlayer():this((System.String) null)
		{
		}
		
		public override System.String ToString()
		{
			return name;
		}
		
		public virtual System.String getName()
		{
			if (name == null || name.Length == 0 || name.Trim().Length == 0 || name.Equals("<nobody>"))
			{
				//$NON-NLS-1$
				return "(" + Chatter.AnonymousUserName + ")"; //$NON-NLS-1$  //$NON-NLS-2$
			}
			return name;
		}
		
		public virtual void  setName(System.String name)
		{
			this.name = name;
		}
		
		public  override bool Equals(System.Object o)
		{
			if (o is Player)
			{
				return id != null && id.Equals(((Player) o).getId());
			}
			else
			{
				return false;
			}
		}
		
		public virtual System.String getId()
		{
			return id;
		}
		
		public virtual PlayerStatus getStatus()
		{
			return status;
		}
		
		public virtual void  setId(System.String id)
		{
			this.id = id;
		}
		
		public virtual void  setStatus(PlayerStatus status)
		{
			this.status = status;
		}
		
		public virtual void  updateStatus()
		{
			if (status is SimpleStatus)
			{
				((SimpleStatus) status).updateStatus();
			}
		}
		//UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1306'"
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}