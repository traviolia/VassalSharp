/*
* $Id$
*
* Copyright (c) 2007 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.util.concurrent.BlockingQueue' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using BlockingQueue = java.util.concurrent.BlockingQueue;
//UPGRADE_TODO: The type 'java.util.concurrent.Future' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Future = java.util.concurrent.Future;
//UPGRADE_TODO: The type 'java.util.concurrent.LinkedBlockingQueue' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LinkedBlockingQueue = java.util.concurrent.LinkedBlockingQueue;
//UPGRADE_TODO: The type 'java.util.concurrent.ThreadPoolExecutor' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ThreadPoolExecutor = java.util.concurrent.ThreadPoolExecutor;
//UPGRADE_TODO: The type 'java.util.concurrent.TimeUnit' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TimeUnit = java.util.concurrent.TimeUnit;
//UPGRADE_TODO: The type 'org.jdesktop.swingworker.SwingWorker' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SwingWorker = org.jdesktop.swingworker.SwingWorker;
namespace VassalSharp.tools
{
	
	/// <summary> 
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class ThreadManager
	{
		private void  InitBlock()
		{
			//System.out.println("thread queue: " + requestQueue.size());
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			(Future < V >) threadPool.submit(task);
			return threadPool.submit(req);
			
			/*
			public static <V> Future<V> submit(RunnableFuture<V> task) {
			System.out.println("thread queue: " + requestQueue.size());
			return (Future<V>) threadPool.submit(task);
			}*/
		}
		private ThreadManager()
		{
			InitBlock();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private static final BlockingQueue < Runnable > requestQueue = 
		new LinkedBlockingQueue < Runnable >();
		/*
		new LinkedBlockingQueue<Runnable>() {
		@Override
		public boolean offer(Runnable r) {
		boolean ret = super.offer(r);
		if (ret) System.out.println("queued " + r);
		else System.out.println("failed to queue " + r);
		
		for (Iterator<Runnable> i = requestQueue.iterator(); i.hasNext(); ) {
		System.out.println("in queue: " + i.next());
		}
		
		return ret;
		}
		
		@Override
		public Runnable take() throws InterruptedException {
		Runnable r = super.take();
		System.out.println("took " + r);
		
		for (Iterator<Runnable> i = requestQueue.iterator(); i.hasNext(); ) {
		System.out.println("in queue: " + i.next());
		}
		
		return r;
		}
		
		@Override
		public Runnable poll(long timeout, TimeUnit unit) throws InterruptedException {
		Runnable r = super.poll(timeout, unit);
		System.out.println("polled " + r);
		
		for (Iterator<Runnable> i = requestQueue.iterator(); i.hasNext(); ) {
		System.out.println("in queue: " + i.next());
		}
		
		return r;
		}
		};*/
		
		/*
		private static final BlockingQueue<Runnable> requestQueue =
		new PriorityBlockingQueue<Runnable>(20, new PriorityOrdering());
		
		private static final class PriorityOrdering implements Comparator<Runnable> {
		public int compare(Runnable x, Runnable y) {
		return
		(x instanceof Prioritizable ? ((Prioritizable) x).getPriority() : 0) -
		(y instanceof Prioritizable ? ((Prioritizable) y).getPriority() : 0);
		}
		}*/
		
		private sealed class Ex:ThreadPoolExecutor
		{
			public Ex()
			{
				InitBlock();
			}
			private void  InitBlock()
			{
				base(corePoolSize, maximumPoolSize, keepAliveTime, unit, workQueue);
				execute(req);
				return req;
				
				/*
				@Override
				protected <T> RunnableFuture<T> newTaskFor(Runnable runnable, T value) {
				return runnable instanceof RunnableFuture ?
				(RunnableFuture<T>) runnable : super.newTaskFor(runnable, value);
				}*/
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public Ex(int corePoolSize, int maximumPoolSize, long keepAliveTime, 
			TimeUnit unit, BlockingQueue < Runnable > workQueue)
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public < V > Future < V > submit(SwingWorker < V, ? > req)
		}
		
		//  private static final ExecutorService threadPool =
		//    Executors.newFixedThreadPool(2);
		//    Executors.newCachedThreadPool();
		//    new ThreadPoolExecutor(2, 2, 60, TimeUnit.SECONDS, requestQueue);
		//UPGRADE_NOTE: Final was removed from the declaration of 'threadPool '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly Ex threadPool = new Ex(2, 2, 60, TimeUnit.SECONDS, requestQueue);
		
		// FIXME: RunnableFuture does not exist in Java 1.5. Remove this
		// once we switch to 1.6.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < V > Future < V > submit(Runnable task)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < V > Future < V > submit(SwingWorker < V, ? > req)
	}
}