/*
* $Id$
*
* Copyright (c) 2008 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutorService' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutorService = java.util.concurrent.ExecutorService;
//UPGRADE_TODO: The type 'java.util.concurrent.Executors' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Executors = java.util.concurrent.Executors;
//UPGRADE_TODO: The type 'java.util.concurrent.Future' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Future = java.util.concurrent.Future;
namespace VassalSharp.tools
{
	
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class DialogUtils
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(DialogUtils enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(DialogUtils enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private DialogUtils enclosingInstance;
			public DialogUtils Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				try
				{
					SwingUtilities.invokeAndWait(runnable);
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
					ErrorDialog.bug(e);
				}
				catch (System.Reflection.TargetInvocationException e)
				{
					ErrorDialog.bug(e);
				}
			}
		}
		private void  InitBlock()
		{
			return ex.submit(new AnonymousClassRunnable(this));
		}
		private DialogUtils()
		{
			InitBlock();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private static final Set < Object > disabled = 
		Collections.synchronizedSet(new HashSet < Object >());
		
		public static bool isDisabled(System.Object key)
		{
			return disabled.contains(key);
		}
		
		public static bool setDisabled(System.Object key, bool disable)
		{
			// we synchronize here to make atomic getting the previous
			// value and (possibly) setting a new one
			lock (disabled)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'wasDisabled '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool wasDisabled = isDisabled(key);
				
				if (wasDisabled)
				{
					if (!disable)
						disabled.remove(key);
				}
				else
				{
					if (disable)
						disabled.add(key);
				}
				
				return wasDisabled;
			}
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'ex '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly ExecutorService ex = Executors.newSingleThreadExecutor();
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Future < ? > enqueue(final Runnable runnable)
	}
}