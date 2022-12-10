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
using Pair = VassalSharp.tools.lang.Pair;
namespace VassalSharp.tools.concurrent
{
	
	/// <summary> A {@link Runnable} which operates on a rolling range.
	/// 
	/// A {@code RangedRunnable} can be updated after it has been submitted but
	/// before it has been run. This cuts down on the number of times the
	/// {@code RangedRunnable} must be submitted, and is appropriate for values
	/// where only the most recent update matters.
	/// 
	/// </summary>
	/// <param name="<T>">the type of the lower and upper bounds in the range
	/// </param>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.11
	/// </since>
	public abstract class RangedRunnable
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Pair < T, T > flushed = range;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			range = new Pair < T, T >(flushed.second, null);
			submitted = false;
			return flushed;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< T > implements Runnable
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected Pair < T, T > range;
		protected internal bool submitted = false;
		
		/// <summary> Creates a new {@code RangedRunnable} with the given lower bound.
		/// 
		/// </summary>
		/// <param name="init">the initial lower bound
		/// </param>
		public RangedRunnable(T init)
		{
			InitBlock();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			range = new Pair < T, T >(init, null);
		}
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <seealso cref="run(Pair<T,T>)}.</p>">
		/// </seealso>
		public void  run()
		{
			run(flush());
		}
		
		/// <summary> Sets the value of the upper end of the range.
		/// 
		/// </summary>
		/// <param name="last">the value to set
		/// </param>
		/// <param name="submit">whether to also submit the change
		/// </param>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'setLast'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public void  setLast(T last, bool submit)
		{
			lock (this)
			{
				if (last != range.second)
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					range = new Pair < T, T >(range.first, last);
				}
				
				if (submit && !submitted)
				{
					submit();
					submitted = true;
				}
			}
		}
		
		/// <summary> Hands the {@code RangedRunnable} off to be executed.
		/// 
		/// </summary>
		/// <seealso cref="setLast}.</p>">
		/// </seealso>
		protected internal abstract void  submit();
		
		/// <summary> Processes the given range.
		/// 
		/// </summary>
		/// <param name="r">the range to process
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		abstract
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void run(Pair < T, T > r);
		
		/// <summary> Returns the old range and creates a new range adjacent to the old one.
		/// The upper bound of the old range becomes the lower bound of the new range.
		/// 
		/// </summary>
		/// <returns> the range being flushed
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private final synchronized Pair < T, T > flush()
	}
}