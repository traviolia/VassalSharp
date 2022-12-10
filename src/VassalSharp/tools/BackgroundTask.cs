/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
	
	/// <summary> Utility task for starting a thread that performs one task,
	/// {@link #doFirst}, then queues another another
	/// task, {@link #doLater}, for the Event Handler thread to execute
	/// This is basically a simple version of Sun's SwingWorker class.
	/// 
	/// </summary>
	/// <deprecated> Use {@link SwingWorker} now that we ship the JAR for it.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public abstract class BackgroundTask
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(BackgroundTask enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BackgroundTask enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BackgroundTask enclosingInstance;
			public BackgroundTask Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				Enclosing_Instance.doLater();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable1 : IThreadRunnable
		{
			public AnonymousClassRunnable1(IThreadRunnable later, BackgroundTask enclosingInstance)
			{
				InitBlock(later, enclosingInstance);
			}
			private void  InitBlock(IThreadRunnable later, BackgroundTask enclosingInstance)
			{
				this.later = later;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable later was copied into class AnonymousClassRunnable1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private IThreadRunnable later;
			private BackgroundTask enclosingInstance;
			public BackgroundTask Enclosing_Instance
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
					Enclosing_Instance.doFirst();
				}
				//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception t)
				{
					if (t is bsh.TargetError)
						((bsh.TargetError) t).printStackTrace();
					else
						SupportClass.WriteStackTrace(t, Console.Error);
				}
				finally
				{
					//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
					SwingUtilities.invokeLater(later);
				}
			}
		}
		public abstract void  doFirst();
		
		public abstract void  doLater();
		
		public virtual SupportClass.ThreadClass start()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'later '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			IThreadRunnable later = new AnonymousClassRunnable(this);
			IThreadRunnable first = new AnonymousClassRunnable1(later, this);
			SupportClass.ThreadClass t = new SupportClass.ThreadClass(new System.Threading.ThreadStart(first.Run));
			t.Start();
			return t;
		}
	}
}