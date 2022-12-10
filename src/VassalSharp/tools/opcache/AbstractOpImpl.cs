/*
* $Id$
*
* Copyright (c) 2007-2008 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.util.concurrent.ConcurrentHashMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ConcurrentHashMap = java.util.concurrent.ConcurrentHashMap;
//UPGRADE_TODO: The type 'java.util.concurrent.ConcurrentMap' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ConcurrentMap = java.util.concurrent.ConcurrentMap;
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutionException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutionException = java.util.concurrent.ExecutionException;
//UPGRADE_TODO: The type 'java.util.concurrent.Future' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Future = java.util.concurrent.Future;
namespace VassalSharp.tools.opcache
{
	
	/// <summary> An abstract representation of an operation. <code>AbstractOpImpl</code>
	/// is the base class for all such operations. The results of all operations
	/// are memoized (using a memory-sensitive cache), so retrieving results is
	/// both fast and memory-efficient.
	/// 
	/// <p><b>Warning:</b> For efficiency reasons, the methods {@link #get()} and
	/// {@link #get(OpObserver)} do <em>not</em> return defensively, nor do the
	/// {@code Future}s returned by {@link #getFuture(OpObserver)}. That is, the
	/// object returned is possibly the one retained internally by the
	/// <code>AbstractOpImpl</code>. Therefore, objects obtained from an
	/// <code>AbstractOpImpl</code> <em>must not</em> be altered, as this might
	/// interfere with caching. If an object obtained this way needs to be
	/// modified, copy the object first and alter the copy.</p>
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public abstract class AbstractOpImpl
	{
		/// <summary>The cache which contains calculated results. </summary>
		private void  InitBlock()
		{
			return cache.get_Renamed(newKey(), obs);
			return cache.getFuture(newKey(), obs);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			OpCache.Key < V > key =(OpCache.Key < V >) kcache.get(this);
			if (key == null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final OpCache.Key < V > nkey = new OpCache.Key < V >(this, 0);
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				key =(OpCache.Key < V >) kcache.putIfAbsent(this, nkey);
				if (key == null)
					key = nkey;
			}
			
			return key;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< V > implements Op < V >
		//UPGRADE_NOTE: Final was removed from the declaration of 'cache '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal OpCache cache;
		
		/// <param name="cache">the cache for storing our result
		/// </param>
		public AbstractOpImpl(OpCache cache)
		{
			InitBlock();
			this.cache = cache;
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		abstract List < Op < ? >> getSources();
		
		/// <summary>{@inheritDoc} </summary>
		public abstract V eval();
		
		/// <summary>{@inheritDoc} </summary>
		public virtual V get_Renamed()
		{
			return cache.get_Renamed(newKey());
		}
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <throws>  CancellationException if the operation was cancelled </throws>
		/// <throws>  InterruptedException if the operation was interrupted </throws>
		/// <throws>  ExecutionException if the operation failed </throws>
		public V get_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(OpObserver < V > obs) throws CancellationException, 
		InterruptedException, 
		ExecutionException
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <throws>  CancellationException if the operation was cancelled </throws>
		/// <throws>  InterruptedException if the operation was interrupted </throws>
		/// <throws>  ExecutionException if the operation failed </throws>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Future < V > getFuture(OpObserver < V > obs) throws ExecutionException
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private static final ConcurrentMap < Op < ? >, OpCache.Key < ? >> kcache = 
		new ConcurrentHashMap < Op < ? >, OpCache.Key < ? >>();
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public OpCache.Key < V > newKey()
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		public virtual void  update()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final OpCache.Key < V > key =(OpCache.Key < V >) kcache.get(this);
			if (key != null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final OpCache.Key < V > nkey = new OpCache.Key < V >(this, key.version + 1);
				kcache.replace(this, key, nkey);
			}
		}
	}
}