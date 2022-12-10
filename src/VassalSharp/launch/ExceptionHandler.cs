/*
* $Id$
*
* Copyright (c) 2000-2008 by Rodney Kinney, Joel Uckelman
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
using System.Threading;
//using ErrorDialog = VassalSharp.tools.ErrorDialog;

namespace VassalSharp.launch
{
	
	/// <summary> Handles uncaught exceptions. None of the methods in this class are
	/// intended to be called in our code, but must be public in order to be
	/// accessible to {@link Thread} and {@link EventDispatchThread}.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class ExceptionHandler : Thread.UncaughtExceptionHandler
	{
		
		public ExceptionHandler()
		{
		}
		
		/// <summary> Handles uncaught exceptions.
		/// 
		/// </summary>
		/// <param name="thrown">the <code>Throwable</code> which was not caught.
		/// </param>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public virtual void  handle(System.Exception thrown)
		{
			//ErrorDialog.bug(thrown);
		}
		
		/// <summary> {@inheritDoc}
		/// 
		/// This method is part of the {@link Thread.UncaughtExceptionHandler}
		/// interface. Do not call this method directly.
		/// 
		/// </summary>
		/// <param name="thread">the thread where the exception occurred
		/// </param>
		/// <param name="thrown">the exception
		/// </param>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public virtual void  uncaughtException(SupportClass.ThreadClass thread, System.Exception thrown)
		{
			handle(thrown);
		}
		static ExceptionHandler()
		{
			// Sets this class to handle exceptions occurring on the EDT.
			// See Java code in {@link EventDispatchThread.handleException()}.
			// In the case of an exception, the class is instantiated using its
			// zero-argument constructor and {@link handle()} is called with the
			// Throwable from the EDT.
			{
				//UPGRADE_TODO: Method 'java.lang.System.getProperties' was converted to 'SupportClass.GetProperties' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangSystemgetProperties'"
				SupportClass.GetProperties()[(System.String) "sun.awt.exception.handler"] = (System.String) typeof(ExceptionHandler).FullName;
			}
		}
	}
}