/*
* $Id$
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
using ServerConnection = VassalSharp.build.module.ServerConnection;
using ChatServerConnection = VassalSharp.chat.ChatServerConnection;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.ui
{
	
	/// <summary>Adds Connect/Disconnect button to the server controls toolbar </summary>
	public class BasicChatControlsInitializer : ChatControlsInitializer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction:SupportClass.ActionSupport
		{
			private void  InitBlock(BasicChatControlsInitializer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicChatControlsInitializer enclosingInstance;
			public BasicChatControlsInitializer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction(BasicChatControlsInitializer enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			} //$NON-NLS-1$
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				Enclosing_Instance.client.setConnected(true);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction1:SupportClass.ActionSupport
		{
			private void  InitBlock(BasicChatControlsInitializer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicChatControlsInitializer enclosingInstance;
			public BasicChatControlsInitializer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction1(BasicChatControlsInitializer enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			} //$NON-NLS-1$
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				Enclosing_Instance.client.setConnected(false);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(VassalSharp.chat.ui.ChatServerControls controls, BasicChatControlsInitializer enclosingInstance)
			{
				InitBlock(controls, enclosingInstance);
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassRunnable : IThreadRunnable
			{
				public AnonymousClassRunnable(SupportClass.PropertyChangingEventArgs evt, VassalSharp.chat.ui.ChatServerControls controls, AnonymousClassPropertyChangeListener enclosingInstance)
				{
					InitBlock(evt, controls, enclosingInstance);
				}
				private void  InitBlock(SupportClass.PropertyChangingEventArgs evt, VassalSharp.chat.ui.ChatServerControls controls, AnonymousClassPropertyChangeListener enclosingInstance)
				{
					this.evt = evt;
					this.controls = controls;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable evt was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private SupportClass.PropertyChangingEventArgs evt;
				//UPGRADE_NOTE: Final variable controls was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.chat.ui.ChatServerControls controls;
				private AnonymousClassPropertyChangeListener enclosingInstance;
				public AnonymousClassPropertyChangeListener Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  Run()
				{
					bool connected = true.Equals(evt.NewValue);
					//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
					Enclosing_Instance.Enclosing_Instance.connectAction.setEnabled(!connected);
					//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
					Enclosing_Instance.Enclosing_Instance.disconnectAction.setEnabled(connected);
					if (!connected)
					{
						controls.RoomTree.Rooms = new VassalSharp.chat.Room[0];
						controls.CurrentRoom.Rooms = new VassalSharp.chat.Room[0];
					}
				}
			}
			private void  InitBlock(VassalSharp.chat.ui.ChatServerControls controls, BasicChatControlsInitializer enclosingInstance)
			{
				this.controls = controls;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable controls was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.chat.ui.ChatServerControls controls;
			private BasicChatControlsInitializer enclosingInstance;
			public BasicChatControlsInitializer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.invokeLater(new AnonymousClassRunnable(evt, controls, this));
			}
		}
		private SupportClass.ActionSupport connectAction;
		private SupportClass.ActionSupport disconnectAction;
		private ChatServerConnection client;
		private System.Windows.Forms.Button connectButton;
		private System.Windows.Forms.Button disconnectButton;
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		private PropertyChangeListener connectionListener;
		
		public BasicChatControlsInitializer(ChatServerConnection client):base()
		{
			this.client = client;
		}
		
		public virtual void  initializeControls(ChatServerControls controls)
		{
			System.Windows.Forms.ToolBar toolbar = controls.Toolbar;
			
			connectAction = new AnonymousClassAbstractAction(this, Resources.getString("Chat.connect"));
			
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			System.Uri imageURL = new System.Uri(System.IO.Path.GetFullPath("/images/connect.gif")); //$NON-NLS-1$
			if (imageURL != null)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.Action.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.SHORT_DESCRIPTION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionSHORT_DESCRIPTION_f'"
				//UPGRADE_ISSUE: Method 'javax.swing.Action.getValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActiongetValue_javalangString'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
				connectAction.putValue(Action.SHORT_DESCRIPTION, connectAction.getValue(Action.NAME));
				//UPGRADE_ISSUE: Method 'javax.swing.Action.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
				connectAction.putValue(Action.NAME, ""); //$NON-NLS-1$
				//UPGRADE_ISSUE: Method 'javax.swing.Action.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.SMALL_ICON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionSMALL_ICON_f'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
				connectAction.putValue(Action.SMALL_ICON, new ImageIcon(imageURL));
			}
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			connectAction.setEnabled(true);
			
			disconnectAction = new AnonymousClassAbstractAction1(this, Resources.getString("Chat.disconnect"));
			
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			imageURL = new System.Uri(System.IO.Path.GetFullPath("/images/disconnect.gif")); //$NON-NLS-1$
			if (imageURL != null)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.Action.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.SHORT_DESCRIPTION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionSHORT_DESCRIPTION_f'"
				//UPGRADE_ISSUE: Method 'javax.swing.Action.getValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActiongetValue_javalangString'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
				disconnectAction.putValue(Action.SHORT_DESCRIPTION, disconnectAction.getValue(Action.NAME));
				//UPGRADE_ISSUE: Method 'javax.swing.Action.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
				disconnectAction.putValue(Action.NAME, ""); //$NON-NLS-1$
				//UPGRADE_ISSUE: Method 'javax.swing.Action.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.SMALL_ICON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionSMALL_ICON_f'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
				disconnectAction.putValue(Action.SMALL_ICON, new ImageIcon(imageURL));
			}
			
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			disconnectAction.setEnabled(false);
			System.Windows.Forms.ToolBarButton temp_ToolBarButton;
			System.Windows.Forms.Button temp_Button;
			temp_Button = SupportClass.ButtonSupport.CreateStandardButton(connectAction.Description, connectAction.Icon);
			temp_Button.Click += new System.EventHandler(connectAction.actionPerformed);
			temp_ToolBarButton = new System.Windows.Forms.ToolBarButton(temp_Button.Text);
			toolbar.Buttons.Add(temp_ToolBarButton);
			if (temp_Button.Image != null)
			{
				toolbar.ImageList.Images.Add(temp_Button.Image);
				temp_ToolBarButton.ImageIndex = toolbar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton.Tag = temp_Button;
			temp_Button.Tag = temp_ToolBarButton;
			connectButton = temp_Button;
			System.Windows.Forms.ToolBarButton temp_ToolBarButton2;
			System.Windows.Forms.Button temp_Button2;
			temp_Button2 = SupportClass.ButtonSupport.CreateStandardButton(disconnectAction.Description, disconnectAction.Icon);
			temp_Button2.Click += new System.EventHandler(disconnectAction.actionPerformed);
			temp_ToolBarButton2 = new System.Windows.Forms.ToolBarButton(temp_Button2.Text);
			toolbar.Buttons.Add(temp_ToolBarButton2);
			if (temp_Button2.Image != null)
			{
				toolbar.ImageList.Images.Add(temp_Button2.Image);
				temp_ToolBarButton2.ImageIndex = toolbar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton2.Tag = temp_Button2;
			temp_Button2.Tag = temp_ToolBarButton2;
			disconnectButton = temp_Button2;
			
			connectionListener = new AnonymousClassPropertyChangeListener(controls, this);
			
			client.addPropertyChangeListener(VassalSharp.build.module.ServerConnection_Fields.CONNECTED, connectionListener);
		}
		
		public virtual void  uninitializeControls(ChatServerControls controls)
		{
			controls.Toolbar.Buttons.Remove((System.Windows.Forms.ToolBarButton) connectButton.Tag);
			controls.Toolbar.Buttons.Remove((System.Windows.Forms.ToolBarButton) disconnectButton.Tag);
		}
	}
}