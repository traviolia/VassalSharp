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
//UPGRADE_TODO: The type 'java.util.concurrent.BlockingQueue' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using BlockingQueue = java.util.concurrent.BlockingQueue;
//UPGRADE_TODO: The type 'java.util.concurrent.Callable' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Callable = java.util.concurrent.Callable;
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutorService' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutorService = java.util.concurrent.ExecutorService;
namespace VassalSharp.tools.concurrent
{
	
	/// <summary> A {@link Callable} which calls a {@code Callable} retrieved from a queue.
	/// 
	/// {@code QueueJoiner} permits one queue of {@code Callable}s to be joined
	/// into a single {@code Callable} and inserted into another queue. One reason
	/// for doing this is to make a single queue of {@code Callable}s available
	/// from multiple threads. E.g., if a task run by an {@link ExecutorService}
	/// needs to submit other tasks to the same {@code ExecutorService}, those
	/// child tasks ey can be queued and submitted via a {@code QueueJoiner}, while
	/// the original task can work on the same queue using a second
	/// {@code QueueJoiner}. This prevents the {@code ExecutorService} from
	/// deadlocking in the event that the child tasks are unable to make it to the
	/// front of the {@code ExecutorService}'s queue, because the thread of the
	/// original task will eventually clear the queue on its own.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.11
	/// </since>
	public class QueueJoiner : Callable
	{
		public QueueJoiner()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			this.queue = queue;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Void >
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected final BlockingQueue < ? extends Callable < ? >> queue;
		
		/// <summary> Creates a {@link Callable} which calls a {@code Callable} retrieved
		/// from a queue.
		/// 
		/// </summary>
		/// <param name="queue">the queue
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public QueueJoiner(BlockingQueue < ? extends Callable < ? >> queue)
		
		/// <summary> Calls a {@link Callable} from the queue unless the queue is empty.
		/// 
		/// </summary>
		/// <throws>  Exception when the {@code Callable} from the queue throws </throws>
		public virtual System.Void call()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Callable < ? > c = queue.poll();
			if (c != null)
				c.call();
			//UPGRADE_TODO: The 'System.Void' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
			return null;
		}
	}
}