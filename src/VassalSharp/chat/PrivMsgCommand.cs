/*
*
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
//UPGRADE_TODO: The type 'java.awt.KeyboardFocusManager' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using KeyboardFocusManager = java.awt.KeyboardFocusManager;
using Command = VassalSharp.command.Command;
namespace VassalSharp.chat
{
	
	/// <summary> A Command that encapsulates a private chat message from another
	/// {@link VassalSharp.chat.SimplePlayer} 
	/// </summary>
	public class PrivMsgCommand:Command
	{
		/// <summary> Return true, as this command should not be logged</summary>
		override public bool Loggable
		{
			get
			{
				return false;
			}
			
		}
		virtual public Player Sender
		{
			get
			{
				return p;
			}
			
		}
		virtual public System.String Message
		{
			get
			{
				return msg;
			}
			
		}
		private PrivateChatManager mgr;
		private System.String msg;
		private Player p;
		
		public PrivMsgCommand(PrivateChatManager mgr, Player sender, System.String msg)
		{
			this.mgr = mgr;
			this.msg = msg;
			p = sender;
		}
		
		public override void  executeCommand()
		{
			PrivateChatter chat = mgr.getChatterFor(p);
			if (chat == null)
			{
				return ;
			}
			
			//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
			System.Windows.Forms.Form f = (System.Windows.Forms.Form) chat.Parent;
			//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
			if (!f.Visible)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(f, "Visible", true);
				System.Windows.Forms.Control c = KeyboardFocusManager.getCurrentKeyboardFocusManager().getFocusOwner();
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.isDescendingFrom' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				if (c == null || !SwingUtilities.isDescendingFrom(c, f))
				{
					//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
					java.awt.Toolkit.getDefaultToolkit();
					//UPGRADE_TODO: Method 'java.awt.Toolkit.beep' was converted to 'System.Console.Write' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					System.Console.Write("\a");
					for (int i = 0, j = (int) chat.Controls.Count; i < j; ++i)
					{
						//UPGRADE_TODO: Method 'java.awt.Container.getComponent' was converted to 'System.Windows.Forms.Control.Controls' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContainergetComponent_int'"
						if (chat.Controls[i] is System.Windows.Forms.TextBox)
						{
							//UPGRADE_TODO: Method 'java.awt.Container.getComponent' was converted to 'System.Windows.Forms.Control.Controls' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContainergetComponent_int'"
							(chat.Controls[i]).Focus();
							break;
						}
					}
				}
			}
			else
			{
				f.BringToFront();
			}
			chat.show(msg);
		}
		
		public override Command myUndoCommand()
		{
			return null;
		}
	}
}