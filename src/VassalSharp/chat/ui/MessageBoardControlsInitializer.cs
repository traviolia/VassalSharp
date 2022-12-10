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
using MessageBoard = VassalSharp.chat.messageboard.MessageBoard;
using MessageBoardControls = VassalSharp.chat.messageboard.MessageBoardControls;
namespace VassalSharp.chat.ui
{
	
	/// <summary>Adds controls to post/retrieve message from a message board </summary>
	public class MessageBoardControlsInitializer : ChatControlsInitializer
	{
		
		private MessageBoardControls msgMgr;
		private System.Windows.Forms.Button checkMsgButton;
		private System.Windows.Forms.Button postMsgButton;
		
		public MessageBoardControlsInitializer(System.String name, MessageBoard board):base()
		{
			msgMgr = new MessageBoardControls();
			msgMgr.setServer(board, name);
		}
		
		public virtual void  initializeControls(ChatServerControls controls)
		{
			checkMsgButton = controls.Toolbar.add(msgMgr.getCheckMessagesAction());
			postMsgButton = controls.Toolbar.add(msgMgr.getPostMessageAction());
		}
		
		public virtual void  uninitializeControls(ChatServerControls controls)
		{
			controls.Toolbar.Buttons.Remove((System.Windows.Forms.ToolBarButton) checkMsgButton.Tag);
			controls.Toolbar.Buttons.Remove((System.Windows.Forms.ToolBarButton) postMsgButton.Tag);
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			controls.Toolbar.Refresh();
		}
	}
}