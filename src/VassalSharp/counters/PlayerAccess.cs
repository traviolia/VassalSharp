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
using GameModule = VassalSharp.build.GameModule;
namespace VassalSharp.counters
{
	
	/// <summary> Access is granted if {@link GameModule#getUserId()} matches
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class PlayerAccess : PieceAccess
	{
		public static PlayerAccess Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new PlayerAccess();
				}
				return instance;
			}
			
		}
		virtual public System.String CurrentPlayerId
		{
			get
			{
				return GameModule.getUserId();
			}
			
		}
		private static PlayerAccess instance;
		
		public virtual bool currentPlayerHasAccess(System.String id)
		{
			return id == null || (!GlobalAccess.HideAll && id.Equals(CurrentPlayerId));
		}
		
		public virtual bool currentPlayerCanModify(System.String ownerId)
		{
			return ownerId == null || ownerId.Equals(CurrentPlayerId);
		}
	}
}