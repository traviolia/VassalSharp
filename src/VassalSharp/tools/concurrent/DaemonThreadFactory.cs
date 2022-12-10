/*
* $Id$
*
* Copyright (c) 2010 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.util.concurrent.ThreadFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ThreadFactory = java.util.concurrent.ThreadFactory;
//UPGRADE_TODO: The type 'java.util.concurrent.atomic.AtomicInteger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using AtomicInteger = java.util.concurrent.atomic.AtomicInteger;
namespace VassalSharp.tools.concurrent
{
	
	/// <summary> A thread factory which creates daemon threads. Running daemon threads,
	/// unlike regular threads, do not prevent the application from shutting down.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class DaemonThreadFactory : ThreadFactory
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'id '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal AtomicInteger id = new AtomicInteger(0);
		//UPGRADE_NOTE: Final was removed from the declaration of 'basename '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.String basename;
		
		/// <summary> Creates a thread factory.
		/// 
		/// </summary>
		/// <param name="basename">the base name to use for threads created by this factory
		/// </param>
		public DaemonThreadFactory(System.String basename)
		{
			this.basename = basename;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual SupportClass.ThreadClass newThread(IThreadRunnable r)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String name = basename + "-" + id.getAndIncrement();
			//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SupportClass.ThreadClass t = new SupportClass.ThreadClass(new System.Threading.ThreadStart(r.Run), name);
			t.IsBackground = true;
			return t;
		}
	}
}