/*
* $Id$
*
* Copyright (c) 2008-2009 by Brent Easton
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
using EvalError = bsh.EvalError;
using ParseException = bsh.ParseException;
namespace VassalSharp.script
{
	
	public class CompileResult
	{
		virtual public bool Success
		{
			get
			{
				return success;
			}
			
		}
		virtual public System.String Message
		{
			get
			{
				System.String message = "";
				if (error != null)
				{
					if (error is ParseException)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						message = error.Message;
					}
					else
					{
						return error.ErrorLineNumber + ": " + error.ErrorText;
					}
				}
				return message;
			}
			
		}
		protected internal bool success;
		protected internal EvalError error = null;
		
		public CompileResult()
		{
			success = true;
		}
		
		public CompileResult(EvalError e)
		{
			success = false;
			error = e;
		}
		
		public virtual void  printStackTrace()
		{
			if (error != null)
			{
				SupportClass.WriteStackTrace(error, Console.Error);
			}
		}
	}
}