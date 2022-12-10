/*
* $Id$
*
* Copyright (c) 2000-2009 by Rodney Kinney, Brent Easton
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
using ChatServerConnection = VassalSharp.chat.ChatServerConnection;
using HttpMessageServer = VassalSharp.chat.HttpMessageServer;
using ServerStatus = VassalSharp.chat.ServerStatus;
using MessageBoard = VassalSharp.chat.messageboard.MessageBoard;
using MessageBoardControls = VassalSharp.chat.messageboard.MessageBoardControls;
using PeerPoolInfo = VassalSharp.chat.peer2peer.PeerPoolInfo;
using Resources = VassalSharp.i18n.Resources;
using MenuManager = VassalSharp.tools.menu.MenuManager;
namespace VassalSharp.chat.ui
{
	
	/// <summary> Description?</summary>
	[Serializable]
	public class ShowServerStatusAction:SupportClass.ActionSupport
	{
		private const long serialVersionUID = 1L;
		
		private static Window frame;
		
		public ShowServerStatusAction(ServerStatus status, System.Uri iconURL):this(status, iconURL, true)
		{
		}
		
		public ShowServerStatusAction(ServerStatus status, System.Uri iconURL, bool includeMessageControls)
		{
			if (frame == null)
			{
				frame = new Window(status, includeMessageControls);
			}
			if (iconURL == null)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
				putValue(NAME, Resources.getString("Chat.server_status")); //$NON-NLS-1$
			}
			else
			{
				//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.SMALL_ICON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionSMALL_ICON_f'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
				putValue(SMALL_ICON, new ImageIcon(iconURL));
			}
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
			//UPGRADE_ISSUE: Field 'javax.swing.Action.SHORT_DESCRIPTION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionSHORT_DESCRIPTION_f'"
			putValue(SHORT_DESCRIPTION, Resources.getString("Chat.display_connections")); //$NON-NLS-1$
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			frame.refresh();
		}
		
		[Serializable]
		private class Window:System.Windows.Forms.Form
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPeerPoolInfo' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPeerPoolInfo : PeerPoolInfo
			{
				public AnonymousClassPeerPoolInfo(System.String moduleName, Window enclosingInstance)
				{
					InitBlock(moduleName, enclosingInstance);
				}
				private void  InitBlock(System.String moduleName, Window enclosingInstance)
				{
					this.moduleName = moduleName;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable moduleName was copied into class AnonymousClassPeerPoolInfo. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.String moduleName;
				private Window enclosingInstance;
				virtual public System.String ModuleName
				{
					get
					{
						return moduleName;
					}
					
				}
				virtual public System.String UserName
				{
					get
					{
						return ((ChatServerConnection) GameModule.getGameModule().getServer()).UserInfo.getName();
					}
					
				}
				public Window Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
			}
			private const long serialVersionUID = 1L;
			
			private ServerStatusView view;
			private MessageBoardControls messageMgr;
			
			public Window(ServerStatus status, bool includeMessageControls):base()
			{
				this.Text = Resources.getString("Chat.server_status"); //$NON-NLS-1$
				Menu = MenuManager.Instance.getMenuBarFor(this);
				
				view = new ServerStatusView(status);
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.addPropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentaddPropertyChangeListener_javalangString_javabeansPropertyChangeListener'"
				view.addPropertyChangeListener(ServerStatusView.SELECTION_PROPERTY, this);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(view);
				if (includeMessageControls)
				{
					messageMgr = new MessageBoardControls();
					//UPGRADE_NOTE: Final was removed from the declaration of 'toolbar '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Windows.Forms.ToolBar temp_ToolBar;
					System.Windows.Forms.ImageList temp_ImageList;
					temp_ImageList = new System.Windows.Forms.ImageList();
					temp_ToolBar = new System.Windows.Forms.ToolBar();
					temp_ToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(SupportClass.ToolBarButtonClicked);
					temp_ToolBar.ImageList = temp_ImageList;
					System.Windows.Forms.ToolBar toolbar = temp_ToolBar;
					//UPGRADE_ISSUE: Method 'javax.swing.JToolBar.setFloatable' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJToolBarsetFloatable_boolean'"
					toolbar.setFloatable(false);
					toolbar.add(messageMgr.getCheckMessagesAction());
					toolbar.add(messageMgr.getPostMessageAction());
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					Controls.Add(toolbar);
					toolbar.Dock = System.Windows.Forms.DockStyle.Top;
					toolbar.SendToBack();
				}
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				pack();
				//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
				Size = new System.Drawing.Size(System.Math.Max(Size.Width, 400), System.Math.Max(Size.Height, 300));
				//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
				Toolkit.getDefaultToolkit();
				System.Drawing.Size d = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size;
				//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_int_int'"
				Location = new System.Drawing.Point(d.Width / 2 - Size.Width / 2, d.Height / 2 - Size.Height / 2);
			}
			
			public virtual void  refresh()
			{
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				if (!Visible)
				{
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					Visible = true;
				}
				else
				{
					BringToFront();
				}
				view.refresh();
			}
			
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				MessageBoard server = null;
				System.String name = null;
				if (evt.NewValue is VassalSharp.chat.ModuleSummary)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'moduleName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String moduleName = ((VassalSharp.chat.ModuleSummary) evt.NewValue).ModuleName;
					server = new HttpMessageServer(new AnonymousClassPeerPoolInfo(moduleName, this));
				}
				if (messageMgr != null)
				{
					messageMgr.setServer(server, name);
				}
			}
		}
	}
}