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
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
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
//UPGRADE_TODO: The type 'java.util.concurrent.Future' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Future = java.util.concurrent.Future;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
namespace VassalSharp.tools.concurrent
{
	
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1
	/// </since>
	public class FutureUtils
	{
		private void  InitBlock()
		{
			try
			{
				future.get_Renamed();
			}
			catch (CancellationException e)
			{
				e.printStackTrace();
			}
			catch (ExecutionException e)
			{
				ErrorDialog.bug(e);
			}
			catch (System.Threading.ThreadInterruptedException e)
			{
				SupportClass.WriteStackTrace(e, Console.Error);
			}
		}
		private FutureUtils()
		{
			InitBlock();
		}
		
		/// <summary> Waits on a {@link Future} to complete.
		/// 
		/// </summary>
		/// <param name="future">the <code>Future</code> on which to wait
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void wait(Future < ? > future)
	}
}