/*
* $Id$
*
* Copyright (c) 2007-2008 by Rodney Kinney
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
namespace VassalSharp.chat.peer2peer
{
	
	public class IpWatch : IThreadRunnable
	{
		private class AnonymousClassPropertyChangeListener
		{
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Out.WriteLine("Address = " + evt.NewValue); //$NON-NLS-1$
			}
		}
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: Constructor 'java.beans.PropertyChangeSupport.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			propSupport = new PropertyChangeSupport(this);
		}
		[System.ComponentModel.Browsable(true)]
		public  event SupportClass.PropertyChangeEventHandler PropertyChange;
		virtual public System.String CurrentIp
		{
			get
			{
				return currentIp;
			}
			
		}
		//UPGRADE_ISSUE: Class 'java.beans.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
		//UPGRADE_NOTE: The initialization of  'propSupport' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private PropertyChangeSupport propSupport;
		private System.String currentIp;
		private long wait = 1000;
		
		public IpWatch(long waitInterval)
		{
			InitBlock();
			wait = waitInterval;
			currentIp = findIp();
		}
		
		public IpWatch():this(1000)
		{
		}
		
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  addPropertyChangeListener(PropertyChangeListener l)
		{
			//UPGRADE_ISSUE: Method 'java.beans.PropertyChangeSupport.addPropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			propSupport.addPropertyChangeListener(l);
		}
		
		public virtual void  Run()
		{
			while (true)
			{
				System.String newIp = findIp();
				SupportClass.PropertyChangingEventArgs me19 = new SupportClass.PropertyChangingEventArgs("address", currentIp, newIp);
				if (PropertyChange != null)
					PropertyChange(this, me19); //$NON-NLS-1$
				currentIp = newIp;
				
				try
				{
					//UPGRADE_TODO: Method 'java.lang.Thread.sleep' was converted to 'System.Threading.Thread.Sleep' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangThreadsleep_long'"
					System.Threading.Thread.Sleep(new System.TimeSpan((System.Int64) 10000 * wait));
				}
				catch (System.Threading.ThreadInterruptedException ex)
				{
				}
			}
		}
		
		private System.String findIp()
		{
			try
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.net.InetAddress.getAllByName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.net.InetAddress.getHostName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.net.InetAddress.getLocalHost' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Net.IPAddress[] a = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostByAddress(System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName()).AddressList[0].ToString()).HostName).AddressList;
				//UPGRADE_NOTE: Final was removed from the declaration of 'buff '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				StringBuilder buff = new StringBuilder();
				for (int i = 0; i < a.Length; ++i)
				{
					buff.append(a[i].ToString());
					if (i < a.Length - 1)
					{
						buff.append(","); //$NON-NLS-1$
					}
				}
				return buff.toString();
			}
			// FIXME: review error message
			catch (System.Exception e)
			{
				return null;
			}
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			IpWatch w = new IpWatch();
			w.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener().propertyChange);
			System.Console.Out.WriteLine("Address = " + w.CurrentIp); //$NON-NLS-1$
			new SupportClass.ThreadClass(new System.Threading.ThreadStart(w.Run)).Start();
		}
	}
}