/*
* $Id$
*
* Copyright (c) 2004 by Rodney Kinney
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
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.node
{
	
	/// <summary> Limits connections to the server to one per registered username</summary>
	public class ConnectionLimiter
	{
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private Map < String, SocketHandler > connections = 
		new HashMap < String, SocketHandler >();
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'register'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  register(System.String name, SocketHandler handler)
		{
			lock (this)
			{
				if (connections.containsKey(name))
				{
					kickOff(connections.get_Renamed(name));
				}
				connections.put(name, handler);
			}
		}
		
		private void  kickOff(SocketHandler handler)
		{
			handler.writeLine(Resources.getString("Chat.too_many")); //$NON-NLS-1$
			handler.close();
		}
	}
}