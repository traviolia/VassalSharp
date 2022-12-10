/*
* $Id$
*
* Copyright (c) 2009 by Joel Uckelman
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
using IPCMessage = VassalSharp.tools.ipc.IPCMessage;
namespace VassalSharp.launch
{
	
	/// <summary> The interface for objects passed by {@link CommandClient} to
	/// {@link CommandServer}.
	/// </summary>
	/// <deprecated> Use {@link IPCMessage} instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public interface Command
	{
		/// <summary> Execute the command.
		/// 
		/// </summary>
		/// <returns> the result
		/// </returns>
		System.Object execute();
	}
}