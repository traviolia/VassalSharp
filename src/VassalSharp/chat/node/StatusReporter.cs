/*
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
using HttpRequestWrapper = VassalSharp.chat.HttpRequestWrapper;
using SimpleStatus = VassalSharp.chat.SimpleStatus;
using PropertiesEncoder = VassalSharp.tools.PropertiesEncoder;
namespace VassalSharp.chat.node
{
	
	/// <summary> Copyright (c) 2003 by Rodney Kinney.  All rights reserved.
	/// Date: Jun 7, 2003
	/// </summary>
	public class StatusReporter : IThreadRunnable
	{
		private HttpRequestWrapper reportStatus;
		private System.String lastReportedContents;
		private System.String currentContents;
		private long sleepInterval = MIN_SLEEP;
		private const long MIN_SLEEP = 2000;
		private const long MAX_SLEEP = 1000 * 60 * 60 * 2;
		private AsynchronousServerNode server;
		
		public StatusReporter(HttpRequestWrapper reportStatus, AsynchronousServerNode server)
		{
			this.reportStatus = reportStatus;
			this.server = server;
			new SupportClass.ThreadClass(new System.Threading.ThreadStart(this.Run), "status reporter").Start();
		}
		
		public virtual void  updateContents(Node[] players)
		{
			if (reportStatus == null)
			{
				return ;
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder buffer = new StringBuilder();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Node pl: players)
			{
				Node mod = server.getModule(pl);
				try
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String name = new PropertiesEncoder(pl.getInfo()).Properties.Get(SimpleStatus.NAME);
					if (name != null)
					{
						buffer.append(mod.Id).append('\t').append(pl.getParent().getId()).append('\t').append(name).append('\n');
					}
				}
				// FIXME: review error message
				catch (System.IO.IOException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
				}
			}
			lock (this)
			{
				currentContents = buffer.toString();
			}
		}
		
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'sendContents'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		private void  sendContents()
		{
			lock (this)
			{
				if (currentContents != null && !currentContents.Equals(lastReportedContents))
				{
					try
					{
						//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
						//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
						System.Collections.Specialized.NameValueCollection props = new System.Collections.Specialized.NameValueCollection();
						props[(System.String) "STATUS"] = (System.String) currentContents; //$NON-NLS-1$
						reportStatus.doPost("updateConnections", props); //$NON-NLS-1$
						sleepInterval = MIN_SLEEP;
					}
					// FIXME: review error message
					catch (System.IO.IOException e)
					{
						sleepInterval = System.Math.Min(2 * sleepInterval, MAX_SLEEP);
					}
					lastReportedContents = currentContents;
					//UPGRADE_TODO: Method 'java.util.Date.toString' was converted to 'System.DateTime.ToString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilDatetoString'"
					System.Console.Error.WriteLine("----" + System.DateTime.Now.ToString("r")); //$NON-NLS-1$
					System.Console.Error.WriteLine(currentContents);
				}
			}
		}
		
		public virtual void  Run()
		{
			while (true)
			{
				try
				{
					//UPGRADE_TODO: Method 'java.lang.Thread.sleep' was converted to 'System.Threading.Thread.Sleep' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangThreadsleep_long'"
					System.Threading.Thread.Sleep(new System.TimeSpan((System.Int64) 10000 * sleepInterval));
					sendContents();
				}
				// FIXME: review error message
				catch (System.Threading.ThreadInterruptedException e)
				{
					SupportClass.WriteStackTrace(e, Console.Error);
				}
			}
		}
	}
}