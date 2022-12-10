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
namespace VassalSharp.chat.messageboard
{
	
	
	/// <summary> Simple abstraction of a message board
	/// Date: Mar 11, 2003
	/// </summary>
	public interface MessageBoard:VassalSharp.chat.MessageServer
	{
		Message[] Messages
		{
			get;
			
		}
		
		void  postMessage(System.String msg);
	}
}