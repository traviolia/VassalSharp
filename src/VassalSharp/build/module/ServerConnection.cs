using System;
using Command = VassalSharp.command.Command;
namespace VassalSharp.build.module
{
	
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
	/// <summary> Represents the connection to a live server</summary>
	public struct ServerConnection_Fields{
		/// <summary>Name of the property fired when the connection is opened/clused. Value is Boolean.TRUE or Boolean.FALSE </summary>
		public readonly static System.String CONNECTED = "Connected"; //$NON-NLS-1$
	}
	public interface ServerConnection
	{
		//UPGRADE_NOTE: Members of interface 'ServerConnection' were extracted into structure 'ServerConnection_Fields'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1045'"
		
		/// <summary>Send a command to other players on the server </summary>
		void  sendToOthers(Command c);
		
		void  setConnected(bool connect);
		
		bool isConnected();
		
		/// <summary> Register a PropertyChangeListener. Changes to connection status triggers a PropertyChangeEvent, and concrete
		/// implementations may define other properties
		/// </summary>
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		void  addPropertyChangeListener(System.String propertyName, java.beans.PropertyChangeListener l);
	}
}