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
using ThrowableUtils = VassalSharp.tools.ThrowableUtils;
namespace VassalSharp.tools.logging
{
	
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	[Serializable]
	public class LogEntry
	{
		private const long serialVersionUID = 1L;
		
		public const int MESSAGE = 0;
		public const int WARNING = 1;
		public const int ERROR = 2;
		public const int BUG = 3;
		public const int DEBUG = 4;
		public const int SYSTEM = 5;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'timestamp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public long timestamp;
		//UPGRADE_NOTE: Final was removed from the declaration of 'pid '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public int pid;
		//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public int type;
		//UPGRADE_NOTE: Final was removed from the declaration of 'message '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public System.String message;
		//UPGRADE_NOTE: Final was removed from the declaration of 'trace '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public System.String trace;
		//UPGRADE_NOTE: Final was removed from the declaration of 'wait '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public bool wait;
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public LogEntry(int pid, int type, System.Exception thrown, System.String message, bool wait):this((System.DateTime.Now.Ticks - 621355968000000000) / 10000, pid, type, thrown, message, wait)
		{
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public LogEntry(long timestamp, int pid, int type, System.Exception thrown, System.String message, bool wait)
		{
			
			if (thrown == null && message == null)
				throw new System.ArgumentException();
			
			this.timestamp = timestamp;
			this.pid = pid;
			this.type = type;
			this.message = message;
			this.trace = thrown != null?ThrowableUtils.getStackTrace(thrown):null;
			this.wait = wait;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.String ToString()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'sb '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder sb = new StringBuilder();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'dateFormat '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Constructor 'java.text.SimpleDateFormat.SimpleDateFormat' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javatextSimpleDateFormat'"
			System.Globalization.DateTimeFormatInfo dateFormat = new SimpleDateFormat("dd MMM yyyy HH:mm:ss.SSS", new System.Globalization.CultureInfo("en"));
			//UPGRADE_ISSUE: Method 'java.text.DateFormat.setTimeZone' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javatextDateFormatsetTimeZone_javautilTimeZone'"
			//UPGRADE_ISSUE: Method 'java.util.TimeZone.getTimeZone' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilTimeZonegetTimeZone_javalangString'"
			dateFormat.setTimeZone(TimeZone.getTimeZone("UTC"));
			
			sb.append(dateFormat.format(timestamp)).append(' ').append(pid);
			
			if (message != null)
			{
				sb.append(' ').append(message);
				if (trace != null)
				{
					sb.append("\n").append(trace);
				}
			}
			else
			{
				if (trace != null)
				{
					sb.append(' ').append(trace);
				}
			}
			
			return sb.toString();
		}
	}
}