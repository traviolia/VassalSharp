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
using SimpleRunnableFuture = VassalSharp.tools.concurrent.SimpleRunnableFuture;
namespace VassalSharp.tools.swing
{
	
	/// <summary> A {@link RunnableFuture} for use on the Event Dispatch Thread.
	/// 
	/// </summary>
	/// <param name="<V>">the result type returned by the {@see #get} method
	/// </param>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public abstract class EDTRunnableFuture
	{
		/// <summary> Creates a {@link RunnableFuture} which will be run on the Event Dispatch
		/// Thread.
		/// </summary>
		private void  InitBlock()
		{
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< V > extends SimpleRunnableFuture < V >
		public EDTRunnableFuture()
		{
			InitBlock();
		}
		
		/// <summary> Creates a {@link RunnableFuture} which will be run on the Event Dispatch
		/// Thread.
		/// 
		/// </summary>
		/// <param name="result">the result to return
		/// </param>
		public EDTRunnableFuture(V result)
		{
			InitBlock();
			this.result = result;
		}
		
		/// <summary> This method is run on the EDT.
		/// 
		/// </summary>
		/// <throws>  Exception any exception </throws>
		protected internal abstract void  runOnEDT();
		
		/// <summary>{@inheritDoc} </summary>
		public void  run()
		{
			try
			{
				runOnEDT();
				set_Renamed(result);
			}
			//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception t)
			{
				setException(t);
			}
		}
	}
}