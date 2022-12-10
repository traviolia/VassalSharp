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
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutionException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutionException = java.util.concurrent.ExecutionException;
namespace VassalSharp.tools.opcache
{
	
	/// <summary> An <code>OpObserver</code> is notified when its {@link Op} completes..
	/// 
	/// <p>Objects implementing <code>OpObserver</code> are intended to
	/// be passed as an argument to {@link Op.get(Key,OpObserver)} or
	/// {@link Op.getFuture(Key,OpObserver)} which which call back to
	/// {@link #imageOpChange} on completion.</p>
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	interface OpObserver < V >
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	/// <summary> Callback which is run when the observed <code>Op</code> completes
	/// successfully.
	/// 
	/// </summary>
	/// <param name="op">the successfully completed <code>Op</code>
	/// </param>
	/// <param name="val">the value of the <code>Op</code>
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void succeeded(Op < V > op, V val);
	
	/// <summary> Callback which is run when the observed <code>Op</code> is cancelled.
	/// 
	/// </summary>
	/// <param name="op">the cancelled <code>Op</code>
	/// </param>
	/// <param name="e">the <code>CancellationException</code> thrown when the
	/// <code>Op</code> was cancelled
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void cancelled(Op < V > op, CancellationException e);
	
	/// <summary> Callback which is run when the observed <code>Op</code> is interrupted.
	/// 
	/// </summary>
	/// <param name="op">the interrupted <code>Op</code>
	/// </param>
	/// <param name="e">the <code>InterruptedException</code> thrown when the
	/// <code>Op</code> was interrupted
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void interrupted(Op < V > op, InterruptedException e);
	
	/// <summary> Callback which is run when the observed <code>Op</code> fails.
	/// 
	/// </summary>
	/// <param name="op">the failed <code>Op</code>
	/// </param>
	/// <param name="e">the <code>ExecutionException</code> thrown when the
	/// <code>Op</code> failed
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void failed(Op < V > op, ExecutionException e);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}