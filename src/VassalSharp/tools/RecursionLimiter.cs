/*
* $Id$
*
* Copyright (c) 2009 by Brent Easton
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
namespace VassalSharp.tools
{
	
	/// <summary> Utility class to detect and report Infinite Loops within
	/// Vassal components
	/// 
	/// </summary>
	public class RecursionLimiter
	{
		public static bool ReportingInfiniteLoop
		{
			get
			{
				return reporting;
			}
			
		}
		protected internal const int RECURSION_LIMIT = 50;
		protected internal static int recursionDepth = 0;
		protected internal static bool reporting = false;
		
		public static void  startExecution(RecursionLimiter.Loopable looper)
		{
			if (++recursionDepth > RECURSION_LIMIT)
			{
				throw new RecursionLimitException(looper);
			}
		}
		
		public static void  endExecution()
		{
			--recursionDepth;
		}
		
		/// <summary> Report an Infinite Loop</summary>
		/// <param name="e">
		/// </param>
		public static void  infiniteLoop(RecursionLimitException e)
		{
			reporting = true;
			ErrorDialog.showDetails(e, ThrowableUtils.getStackTrace(e), "Error.infinite_loop", e.ComponentTypeName, e.ComponentName);
			reporting = false;
		}
		
		public interface Loopable
		{
			System.String ComponentTypeName
			{
				get;
				
			}
			System.String ComponentName
			{
				get;
				
			}
		}
	}
}