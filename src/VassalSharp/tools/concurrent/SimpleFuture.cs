/*
* $Id$
*
* Copyright (c) 2009-2010 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.util.concurrent.CancellationException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using CancellationException = java.util.concurrent.CancellationException;
//UPGRADE_TODO: The type 'java.util.concurrent.CountDownLatch' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using CountDownLatch = java.util.concurrent.CountDownLatch;
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutionException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutionException = java.util.concurrent.ExecutionException;
//UPGRADE_TODO: The type 'java.util.concurrent.TimeUnit' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TimeUnit = java.util.concurrent.TimeUnit;
//UPGRADE_TODO: The type 'java.util.concurrent.TimeoutException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TimeoutException = java.util.concurrent.TimeoutException;
namespace VassalSharp.tools.concurrent
{
	
	/// <summary> An uninterruptable {@link SettableFuture}.
	/// 
	/// </summary>
	/// <param name="<V>">the result type returned by the {@link #get} method
	/// </param>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.11
	/// </since>
	public class SimpleFuture
	{
		/// <summary>{@inheritDoc} </summary>
		virtual public bool Cancelled
		{
			get
			{
				return cancelled;
			}
			
		}
		/// <summary>{@inheritDoc} </summary>
		virtual public bool Done
		{
			get
			{
				return cancelled || done.getCount() == 0;
			}
			
		}
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		virtual public System.Exception Exception
		{
			set
			{
				exception = value;
				done.countDown();
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< V > implements SettableFuture < V >
		protected internal V result = null;
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		protected internal System.Exception exception = null;
		protected internal volatile bool cancelled = false;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'done '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal CountDownLatch done = new CountDownLatch(1);
		
		/// <summary> {@inheritDoc}
		/// 
		/// <p>This implementation is uninteruptable, so ignores the parameter.</p>
		/// </summary>
		public virtual bool cancel(bool mayInterruptIfRunning)
		{
			// fail if already cancelled
			if (cancelled)
				return false;
			
			// cancel if running
			if (done.getCount() != 0)
			{
				cancelled = true;
				done.countDown();
			}
			
			return cancelled;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  set_Renamed(V r)
		{
			result = r;
			done.countDown();
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual V get_Renamed()
		{
			done.await();
			if (exception != null)
				throw new ExecutionException(exception);
			if (cancelled)
				throw new CancellationException();
			return result;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual V get_Renamed(long timeout, TimeUnit unit)
		{
			if (!done.await(timeout, unit))
				throw new TimeoutException();
			if (exception != null)
				throw new ExecutionException(exception);
			if (cancelled)
				throw new CancellationException();
			return result;
		}
	}
}