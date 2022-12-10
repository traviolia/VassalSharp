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
using System;
using Chatter = VassalSharp.build.module.Chatter;
using Command = VassalSharp.command.Command;
using CommandFilter = VassalSharp.command.CommandFilter;
namespace VassalSharp.chat
{
	
	/// <summary> Only passes commands that are allowed in the Main Room</summary>
	public class MainRoomFilter:CommandFilter
	{
		public MainRoomFilter()
		{
		}
		
		protected internal override bool accept(Command c)
		{
			return c is Chatter.DisplayText || c is PrivMsgCommand || c is SoundEncoder.Cmd;
		}
	}
}