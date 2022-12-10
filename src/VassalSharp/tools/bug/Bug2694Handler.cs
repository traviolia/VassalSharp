/*
* $Id$
*
* Copyright (c) 2011 by Joel Uckelman
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
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ThrowableUtils = VassalSharp.tools.ThrowableUtils;
namespace VassalSharp.tools.bug
{
	
	public class Bug2694Handler : BugHandler
	{
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public virtual bool accept(System.Exception thrown)
		{
			//UPGRADE_NOTE: Exception 'java.lang.UnsatisfiedLinkError' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			if (thrown is System.Exception)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'trace '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				StackTraceElement[] trace = thrown.getStackTrace();
				if (trace.length > 0)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					StackTraceElement e = trace[0];
					if ("sun.awt.image.ImageRepresentation".Equals(e.getClassName()) && "setBytePixels".Equals(e.getMethodName()))
					{
						return true;
					}
				}
			}
			
			return false;
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public virtual void  handle(System.Exception thrown)
		{
			ErrorDialog.showDetails(thrown, ThrowableUtils.getStackTrace(thrown), "Error.bug2694");
		}
	}
}