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
using ServerStatus = VassalSharp.chat.ServerStatus;
namespace VassalSharp.chat.ui
{
	
	public class ServerStatusControlsInitializer : ChatControlsInitializer
	{
		protected internal System.Windows.Forms.Button showStatusButton;
		protected internal ServerStatus status;
		
		public ServerStatusControlsInitializer(ServerStatus status):base()
		{
			this.status = status;
		}
		
		public virtual void  initializeControls(ChatServerControls controls)
		{
			System.Windows.Forms.ToolBarButton temp_ToolBarButton;
			ShowServerStatusAction temp_Action;
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			temp_Action = new ShowServerStatusAction(status, new System.Uri(System.IO.Path.GetFullPath("/images/status.gif")));
			System.Windows.Forms.Button temp_Button;
			temp_Button = SupportClass.ButtonSupport.CreateStandardButton(temp_Action.Description, temp_Action.Icon);
			temp_Button.Click += new System.EventHandler(temp_Action.actionPerformed);
			temp_ToolBarButton = new System.Windows.Forms.ToolBarButton(temp_Button.Text);
			controls.Toolbar.Buttons.Add(temp_ToolBarButton);
			if (temp_Button.Image != null)
			{
				controls.Toolbar.ImageList.Images.Add(temp_Button.Image);
				temp_ToolBarButton.ImageIndex = controls.Toolbar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton.Tag = temp_Button;
			temp_Button.Tag = temp_ToolBarButton;
			showStatusButton = temp_Button; //$NON-NLS-1$
		}
		
		public virtual void  uninitializeControls(ChatServerControls controls)
		{
			controls.Toolbar.Buttons.Remove((System.Windows.Forms.ToolBarButton) showStatusButton.Tag);
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			controls.Toolbar.Refresh();
		}
	}
}