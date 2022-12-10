/*
* $Id$
*
* Copyright (c) 2008-2009 by Joel Uckelman
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
//import VassalSharp.launch.AbstractLaunchAction.EnqueueLogEntry;
using System;
using CommandClient = VassalSharp.launch.CommandClient;
namespace VassalSharp.tools.logging
{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class CommandClientAdapter : LogListener
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'cmdC '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private CommandClient cmdC;
		
		public CommandClientAdapter(CommandClient cmdC)
		{
			this.cmdC = cmdC;
		}
		
		public virtual void  handle(LogEntry entry)
		{
			/*
			try {
			cmdC.request(new EnqueueLogEntry(entry));
			}
			catch (IOException e) {
			// FIXME: What to do here????
			}*/
		}
	}
}