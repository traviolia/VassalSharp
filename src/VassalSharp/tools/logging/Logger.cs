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
using System;
//UPGRADE_TODO: The type 'java.util.concurrent.Future' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Future = java.util.concurrent.Future;
using Info = VassalSharp.Info;
using SimpleFuture = VassalSharp.tools.concurrent.SimpleFuture;
namespace VassalSharp.tools.logging
{
	
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	/// <deprecated> Use {@link org.slf4j.Logger} instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class Logger
	{
		private void  InitBlock()
		{
			return logAndWait(null, message, MESSAGE);
			return logAndWait(thrown, null, ERROR);
			return logAndWait(thrown, message, ERROR);
			return logAndWait(null, message, type);
			return logAndWait(thrown, null, type);
			return enqueue(new LogEntry(pid, type, thrown, message, true));
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final SimpleFuture < ? > f = new SimpleFuture < Void >();
			f.set_Renamed(null);
			return f;
		}
		public static LogListener[] LogListeners
		{
			get
			{
				return new LogListener[0];
			}
			
		}
		private Logger()
		{
			InitBlock();
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'MESSAGE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'MESSAGE' was moved to static method 'VassalSharp.tools.logging.Logger'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		public static readonly int MESSAGE;
		//UPGRADE_NOTE: Final was removed from the declaration of 'WARNING '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'WARNING' was moved to static method 'VassalSharp.tools.logging.Logger'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		public static readonly int WARNING;
		//UPGRADE_NOTE: Final was removed from the declaration of 'ERROR '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'ERROR' was moved to static method 'VassalSharp.tools.logging.Logger'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		public static readonly int ERROR;
		//UPGRADE_NOTE: Final was removed from the declaration of 'BUG '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'BUG' was moved to static method 'VassalSharp.tools.logging.Logger'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		public static readonly int BUG;
		//UPGRADE_NOTE: Final was removed from the declaration of 'DEBUG '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'DEBUG' was moved to static method 'VassalSharp.tools.logging.Logger'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		public static readonly int DEBUG;
		//UPGRADE_NOTE: Final was removed from the declaration of 'SYSTEM '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'SYSTEM' was moved to static method 'VassalSharp.tools.logging.Logger'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		public static readonly int SYSTEM;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'pid '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'pid' was moved to static method 'VassalSharp.tools.logging.Logger'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly int pid;
		
		public static void  log(System.String message)
		{
			log(null, message, MESSAGE);
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public static void  log(System.Exception thrown)
		{
			log(thrown, null, ERROR);
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public static void  log(System.Exception thrown, System.String message)
		{
			log(thrown, message, ERROR);
		}
		
		public static void  log(System.String message, int type)
		{
			log(null, message, type);
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public static void  log(System.Exception thrown, int type)
		{
			log(thrown, null, type);
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public static void  log(System.Exception thrown, System.String message, int type)
		{
			enqueue(new LogEntry(pid, type, thrown, message, false));
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > logAndWait(String message)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > logAndWait(Throwable thrown)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > logAndWait(Throwable thrown, String message)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > logAndWait(String message, int type)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > logAndWait(Throwable thrown, int type)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > logAndWait(Throwable thrown, 
		String message, int type)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > enqueue(final LogEntry entry)
		
		public static void  addLogListener(LogListener l)
		{
		}
		
		public static void  removeLogListener(LogListener l)
		{
		}
		static Logger()
		{
			MESSAGE = LogEntry.MESSAGE;
			WARNING = LogEntry.WARNING;
			ERROR = LogEntry.ERROR;
			BUG = LogEntry.BUG;
			DEBUG = LogEntry.DEBUG;
			SYSTEM = LogEntry.SYSTEM;
			pid = Info.InstanceID;
		}
	}
}