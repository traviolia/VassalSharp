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
using RangedRunnable = VassalSharp.tools.concurrent.RangedRunnable;
using Pair = VassalSharp.tools.lang.Pair;
namespace VassalSharp.tools.swing
{
	
	/// <summary> A progress handler.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.11
	/// </since>
	public abstract class Progressor:RangedRunnable
	{
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Gets the progress as a percentage.
		/// 
		/// </summary>
		/// <returns> the progress percentage
		/// </returns>
		/// <summary> Sets the progress as a percentage.
		/// 
		/// </summary>
		/// <param name="pct">the progress percentage
		/// </param>
		virtual public int Pct
		{
			get
			{
				return (100 * get_Renamed()) / max;
			}
			
			set
			{
				set_Renamed((value * max) / 100);
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Integer >
		//UPGRADE_NOTE: Final was removed from the declaration of 'max '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int max;
		
		/// <summary> Creates a <code>Progressor</code> with the given bounds.
		/// 
		/// </summary>
		/// <param name="init">the initial progress value
		/// </param>
		/// <param name="max">the maximum progress value
		/// 
		/// </param>
		/// <throws>  IllegalArgumentException if {@code init} is not in {@code [0,max]} </throws>
		/// <throws>  IllegalArgumentException if {@code max &lt; 0} </throws>
		public Progressor(int init, int max):base(init)
		{
			
			if (init < 0)
				throw new System.ArgumentException();
			if (init > max)
				throw new System.ArgumentException();
			if (max < 0)
				throw new System.ArgumentException();
			
			this.max = max;
		}
		
		/// <summary> Sets the progress.
		/// 
		/// Setting the progress submits the {@link Runnable} only when it would
		/// result in a change in the progress percentage.
		/// 
		/// </summary>
		/// <param name="prog">the new progress value
		/// </param>
		/// <throws>  IllegalArgumentException if {@code prog} is not in {@code [0,max]} </throws>
		/// <throws>  IllegalArgumentException if {@code prog &lt;= } current value </throws>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'set'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  set_Renamed(int prog)
		{
			lock (this)
			{
				if (prog < 0)
					throw new System.ArgumentException("prog == " + prog);
				if (prog > max)
					throw new System.ArgumentException("prog == " + prog);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'cur '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int cur = get_Renamed();
				if (prog == cur)
					return ;
				
				if (prog < cur)
				{
					throw new System.ArgumentException("prog == " + prog + ", get() == " + get_Renamed());
				}
				
				// submit only if this change affects the integer percentage
				setLast(prog, (100 * prog) / max > (100 * range.first) / max);
			}
		}
		
		/// <summary> Gets the progress.
		/// 
		/// </summary>
		/// <returns> the progress value
		/// </returns>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'get'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual int get_Renamed()
		{
			lock (this)
			{
				return range.second != null?range.second:range.first;
			}
		}
		
		/// <summary> Increments the progress.</summary>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'increment'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  increment()
		{
			lock (this)
			{
				add(1);
			}
		}
		
		/// <summary> Adds some amount to the progress.</summary>
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'add'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  add(int p)
		{
			lock (this)
			{
				if (p != 0)
					set_Renamed(get_Renamed() + p);
			}
		}
		
		/// <summary> Updates the current progress.
		/// 
		/// </summary>
		/// <param name="prog">the range of progress since the last update
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		abstract
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void run(Pair < Integer, Integer > prog);
		
		/// <summary> Submits this {@link Runnable} to be run on the Event Dispatch Thread.</summary>
		protected internal override void  submit()
		{
			EDT.execute(this);
		}
	}
}