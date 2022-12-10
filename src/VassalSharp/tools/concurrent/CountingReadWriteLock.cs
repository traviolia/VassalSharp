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
//UPGRADE_TODO: The type 'java.util.concurrent.TimeUnit' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TimeUnit = java.util.concurrent.TimeUnit;
//UPGRADE_TODO: The type 'java.util.concurrent.locks.AbstractQueuedSynchronizer' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using AbstractQueuedSynchronizer = java.util.concurrent.locks.AbstractQueuedSynchronizer;
//UPGRADE_TODO: The type 'java.util.concurrent.locks.Condition' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Condition = java.util.concurrent.locks.Condition;
//UPGRADE_TODO: The type 'java.util.concurrent.locks.Lock' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Lock = java.util.concurrent.locks.Lock;
//UPGRADE_TODO: The type 'java.util.concurrent.locks.ReadWriteLock' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ReadWriteLock = java.util.concurrent.locks.ReadWriteLock;
//UPGRADE_TODO: The type 'java.util.concurrent.locks.ReentrantReadWriteLock' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ReentrantReadWriteLock = java.util.concurrent.locks.ReentrantReadWriteLock;
namespace VassalSharp.tools.concurrent
{
	
	/// <summary> A {@link ReadWriteLock} which counts locks held.
	/// 
	/// Locks are not associated with particular threads as with
	/// {@link ReentrantReadWriteLock}, so can be released from threads other
	/// than the the ones which acquired them.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class CountingReadWriteLock : ReadWriteLock
	{
		public CountingReadWriteLock()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			r = new ReadLock(this);
			w = new WriteLock(this);
			sync = new Sync();
		}
		/// <summary>{@inheritDoc} </summary>
		public virtual Lock readLock()
		{
			return r;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual Lock writeLock()
		{
			return w;
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'r' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal ReadLock r;
		//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'w' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal WriteLock w;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'sync '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'sync' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal Sync sync;
		
		protected internal abstract class BaseLock : Lock
		{
			public virtual void  lockInterruptibly()
			{
				throw new System.NotSupportedException();
			}
			
			public virtual Condition newCondition()
			{
				throw new System.NotSupportedException();
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ReadLock' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		protected internal class ReadLock:BaseLock
		{
			public ReadLock(CountingReadWriteLock enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(CountingReadWriteLock enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private CountingReadWriteLock enclosingInstance;
			public CountingReadWriteLock Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  lock_Renamed()
			{
				Enclosing_Instance.sync.acquireShared(0);
			}
			public virtual void  unlock()
			{
				Enclosing_Instance.sync.releaseShared(0);
			}
			
			public virtual bool tryLock()
			{
				return Enclosing_Instance.sync.tryAcquireShared(0) >= 0;
			}
			
			public virtual bool tryLock(long time, TimeUnit unit)
			{
				return Enclosing_Instance.sync.tryAcquireSharedNanos(0, unit.toNanos(time));
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'WriteLock' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		protected internal class WriteLock:BaseLock
		{
			public WriteLock(CountingReadWriteLock enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(CountingReadWriteLock enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private CountingReadWriteLock enclosingInstance;
			public CountingReadWriteLock Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  lock_Renamed()
			{
				Enclosing_Instance.sync.acquire(0);
			}
			public virtual void  unlock()
			{
				Enclosing_Instance.sync.release(0);
			}
			
			public virtual bool tryLock()
			{
				return Enclosing_Instance.sync.tryAcquire(0);
			}
			
			public virtual bool tryLock(long time, TimeUnit unit)
			{
				return Enclosing_Instance.sync.tryAcquireNanos(0, unit.toNanos(time));
			}
		}
		
		// Read states are positive, the write state is -1.
		// State 0 means that no locks are held.
		
		protected internal class Sync:AbstractQueuedSynchronizer
		{
			private const long serialVersionUID = 1L;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			protected internal virtual bool tryAcquire(int dummy)
			{
				return compareAndSetState(0, - 1);
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			protected internal virtual bool tryRelease(int dummy)
			{
				if (getState() != - 1)
				{
					//UPGRADE_NOTE: Exception 'java.lang.IllegalMonitorStateException' was converted to ?2 which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
					throw new System.Threading.SynchronizationLockException();
				}
				return compareAndSetState(- 1, 0);
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			protected internal virtual int tryAcquireShared(int dummy)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int s = getState();
				return s >= 0 && compareAndSetState(s, s + 1)?1:- 1;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			protected internal virtual bool tryReleaseShared(int dummy)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int s = getState();
				if (s < 1)
				{
					//UPGRADE_NOTE: Exception 'java.lang.IllegalMonitorStateException' was converted to ?2 which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
					throw new System.Threading.SynchronizationLockException();
				}
				return compareAndSetState(s, s - 1);
			}
		}
	}
}