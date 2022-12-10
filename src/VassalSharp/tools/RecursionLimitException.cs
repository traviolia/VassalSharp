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
using Loopable = VassalSharp.tools.RecursionLimiter.Loopable;
namespace VassalSharp.tools
{
	
	[Serializable]
	public class RecursionLimitException:System.Exception
	{
		virtual public System.String ComponentTypeName
		{
			get
			{
				return looper == null?"":looper.ComponentTypeName;
			}
			
		}
		virtual public System.String ComponentName
		{
			get
			{
				return looper == null?"":looper.ComponentName;
			}
			
		}
		private const long serialVersionUID = 1L;
		protected internal RecursionLimiter.Loopable looper;
		
		public RecursionLimitException(RecursionLimiter.Loopable l)
		{
			looper = l;
		}
	}
}