/*
* Copyright (c) 2000-2006 by Rodney Kinney
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
namespace VassalSharp.command
{
	
	/// <summary>This command, when executed, displays a Dialog box with a message </summary>
	public class AlertCommand:Command
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(AlertCommand enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(AlertCommand enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private AlertCommand enclosingInstance;
			public AlertCommand Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				JOptionPane.showMessageDialog(GameModule.getGameModule() == null?null:GameModule.getGameModule().getFrame(), Enclosing_Instance.msg);
			}
		}
		virtual public System.String Message
		{
			get
			{
				return msg;
			}
			
		}
		private System.String msg;
		
		public AlertCommand(System.String msg)
		{
			this.msg = msg;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override void  executeCommand()
		{
			IThreadRunnable runnable = new AnonymousClassRunnable(this);
			//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
			SwingUtilities.invokeLater(runnable);
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override Command myUndoCommand()
		{
			return null;
		}
	}
}