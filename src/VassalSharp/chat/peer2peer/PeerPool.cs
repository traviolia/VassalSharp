/*
* $Id:
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
using PeerInfo = org.litesoft.p2pchat.PeerInfo;
using PendingPeerManager = org.litesoft.p2pchat.PendingPeerManager;
namespace VassalSharp.chat.peer2peer
{
	
	/// <summary> Date: Mar 11, 2003</summary>
	public interface PeerPool
	{
		void  initialize(P2PPlayer myInfo, PendingPeerManager ppm);
		void  disconnect();
		void  connectFailed(PeerInfo peerInfo);
	}
}