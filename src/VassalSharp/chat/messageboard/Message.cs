/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
	
	/// <summary> A message in a message board</summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class Message
	{
		virtual public System.String Sender
		{
			get
			{
				return sender;
			}
			
		}
		virtual public System.String Text
		{
			get
			{
				return text;
			}
			
		}
		virtual public System.DateTime Date
		{
			get
			{
				return creationTime;
			}
			
		}
		private System.String sender;
		private System.String text;
		private System.DateTime creationTime;
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public Message(System.String sender, System.String text):this(sender, text, ref tempAux)
		{
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public Message(System.String sender, System.String text, ref System.DateTime created)
		{
			this.sender = sender;
			this.text = text;
			creationTime = created;
		}
		private static System.DateTime tempAux = System.DateTime.Now;
	}
}