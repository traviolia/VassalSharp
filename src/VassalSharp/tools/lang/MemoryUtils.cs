/*
* $Id$
*
* Copyright (c) 2009-2013 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.lang.management.ManagementFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ManagementFactory = java.lang.management.ManagementFactory;
//UPGRADE_TODO: The type 'com.sun.management.OperatingSystemMXBean' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using OperatingSystemMXBean = com.sun.management.OperatingSystemMXBean;
namespace VassalSharp.tools.lang
{
	
	/// <summary> A utility class for getting information about system memory.</summary>
	public class MemoryUtils
	{
		/// <summary> Gets the amount of physical memory (RAM) in this machine, in bytes.
		/// 
		/// </summary>
		/// <returns> the amount of RAM, in bytes; or -1 if the amount of RAM
		/// cannot be queried.
		/// </returns>
		public static long PhysicalMemory
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'o '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Object o = ManagementFactory.getOperatingSystemMXBean();
				
				try
				{
					if (o is OperatingSystemMXBean)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'osb '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						OperatingSystemMXBean osb = (OperatingSystemMXBean) o;
						return osb.getTotalPhysicalMemorySize();
					}
				}
				catch (System.ApplicationException e)
				{
					// com.sun.management.OperatingSystemMXBean doesn't exist in this JVM
				}
				
				// We didn't get a com.sun.management.OperatingSystemMXBean.
				return - 1;
			}
			
		}
		protected internal MemoryUtils()
		{
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'ram '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			long ram = PhysicalMemory;
			if (ram >= 0)
			{
				System.Console.Out.WriteLine("System reports " + (ram >> 20) + "MB RAM");
			}
			else
			{
				System.Console.Out.WriteLine("Could not determine amount of RAM");
			}
		}
	}
}