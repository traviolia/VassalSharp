/*
* Copyright (c) 2000-2007 by Rodney Kinney
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
using GameModule = VassalSharp.build.GameModule;
using Command = VassalSharp.command.Command;
namespace VassalSharp.chat
{
	
	/// <summary> Listens for incoming messages (PropertyChangeEvents with name {@link ChatServerConnection.INCOMING_MSG}) and
	/// interprets the message as a command to be executed
	/// 
	/// </summary>
	/// <author>  rodneykinney
	/// 
	/// </author>
	public class CommandDecoder
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(VassalSharp.command.Command c, CommandDecoder enclosingInstance)
			{
				InitBlock(c, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.command.Command c, CommandDecoder enclosingInstance)
			{
				this.c = c;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable c was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.command.Command c;
			private CommandDecoder enclosingInstance;
			public CommandDecoder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				c.execute();
				GameModule.getGameModule().getLogger().log(c);
			}
		}
		public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Command c = GameModule.getGameModule().decode((System.String) evt.NewValue);
			if (c != null)
			{
				IThreadRunnable runnable = new AnonymousClassRunnable(c, this);
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.invokeLater(runnable);
			}
		}
	}
}