/*
* $Id$
*
* Copyright (c) 2000-2006 by Rodney Kinney
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
using PlayerRoster = VassalSharp.build.module.PlayerRoster;
namespace VassalSharp.counters
{
	
	/// <summary> Access is granted if {@link VassalSharp.build.module.PlayerRoster#getMySide()}
	/// matches
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class SideAccess : PieceAccess
	{
		public static SideAccess Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new SideAccess();
				}
				return instance;
			}
			
		}
		virtual public System.String CurrentPlayerId
		{
			get
			{
				return PlayerRoster.getMySide();
			}
			
		}
		private static SideAccess instance;
		
		public virtual bool currentPlayerHasAccess(System.String id)
		{
			return id == null || (!GlobalAccess.HideAll && id.Equals(CurrentPlayerId));
		}
		
		public virtual bool currentPlayerCanModify(System.String ownerId)
		{
			System.String currentPlayerId = CurrentPlayerId;
			return currentPlayerId != null && (ownerId == null || ownerId.Equals(currentPlayerId));
		}
	}
}