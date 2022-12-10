/*
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
using ChatServerConnection = VassalSharp.chat.ChatServerConnection;
using Player = VassalSharp.chat.Player;
using SimplePlayer = VassalSharp.chat.SimplePlayer;
using SimpleStatus = VassalSharp.chat.SimpleStatus;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.ui
{
	
	public class SimpleStatusControlsInitializer : ChatControlsInitializer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(SimpleStatusControlsInitializer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SimpleStatusControlsInitializer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SimpleStatusControlsInitializer enclosingInstance;
			public SimpleStatusControlsInitializer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				if (Enclosing_Instance.client != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Player p = Enclosing_Instance.client.UserInfo;
					SimpleStatus s = (SimpleStatus) p.getStatus();
					s = new SimpleStatus(!s.Looking, s.Away, s.Profile, s.Client, s.Ip, s.ModuleVersion, s.Crc);
					Enclosing_Instance.client.UserInfo = new SimplePlayer(p.getId(), p.getName(), s);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(VassalSharp.chat.ui.ChatServerControls controls, SimpleStatusControlsInitializer enclosingInstance)
			{
				InitBlock(controls, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.chat.ui.ChatServerControls controls, SimpleStatusControlsInitializer enclosingInstance)
			{
				this.controls = controls;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable controls was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.chat.ui.ChatServerControls controls;
			private SimpleStatusControlsInitializer enclosingInstance;
			public SimpleStatusControlsInitializer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs evt)
			{
				if (Enclosing_Instance.client != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Player p = Enclosing_Instance.client.UserInfo;
					SimpleStatus s = (SimpleStatus) p.getStatus();
					s = new SimpleStatus(s.Looking, true, s.Profile, s.Client, s.Ip, s.ModuleVersion, s.Crc);
					Enclosing_Instance.client.UserInfo = new SimplePlayer(p.getId(), p.getName(), s);
					SupportClass.OptionPaneSupport.ShowMessageDialog(controls.RoomTree, Resources.getString("Chat.im_back"), Resources.getString("Chat.away_from_keyboard"), (int) System.Windows.Forms.MessageBoxIcon.None); //$NON-NLS-1$ //$NON-NLS-2$
					s = (SimpleStatus) p.getStatus();
					s = new SimpleStatus(s.Looking, false, s.Profile, s.Client, s.Ip, s.ModuleVersion, s.Crc);
					Enclosing_Instance.client.UserInfo = new SimplePlayer(p.getId(), p.getName(), s);
				}
			}
		}
		private ChatServerConnection client;
		private bool includeLooking;
		private System.Windows.Forms.Button lookingBox;
		private System.Windows.Forms.Button awayButton;
		
		/// <summary> Entry Point for P2P client - 'Looking for Game' does not make sense.</summary>
		public SimpleStatusControlsInitializer(ChatServerConnection client, bool includeLooking):base()
		{
			this.client = client;
			this.includeLooking = includeLooking;
		}
		
		public SimpleStatusControlsInitializer(ChatServerConnection client):this(client, true)
		{
		}
		
		public virtual void  initializeControls(ChatServerControls controls)
		{
			System.Uri imageURL;
			
			if (includeLooking)
			{
				lookingBox = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString("Chat.looking_for_a_game")); //$NON-NLS-1$
				lookingBox.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(lookingBox);
				//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_javaawtDimension'"
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetMinimumSize'"
				lookingBox.Size = lookingBox.getMinimumSize();
				GetType();
				//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
				imageURL = new System.Uri(System.IO.Path.GetFullPath("/images/playerLooking.gif")); //$NON-NLS-1$
				if (imageURL != null)
				{
					if (lookingBox is VassalSharp.tools.LaunchButton)
						((VassalSharp.tools.LaunchButton) lookingBox).setToolTipText(lookingBox.Text);
					else
						SupportClass.ToolTipSupport.setToolTipText(lookingBox, lookingBox.Text);
					lookingBox.Text = ""; //$NON-NLS-1$
					//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
					lookingBox.Image = new ImageIcon(imageURL);
				}
			}
			
			awayButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString("Chat.away_from_keyboard")); //$NON-NLS-1$
			awayButton.Click += new System.EventHandler(new AnonymousClassActionListener1(controls, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(awayButton);
			GetType();
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			imageURL = new System.Uri(System.IO.Path.GetFullPath("/images/playerAway.gif")); //$NON-NLS-1$
			if (imageURL != null)
			{
				if (awayButton is VassalSharp.tools.LaunchButton)
					((VassalSharp.tools.LaunchButton) awayButton).setToolTipText(awayButton.Text);
				else
					SupportClass.ToolTipSupport.setToolTipText(awayButton, awayButton.Text);
				awayButton.Text = ""; //$NON-NLS-1$
				//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
				awayButton.Image = new ImageIcon(imageURL);
			}
			
			if (includeLooking)
			{
				System.Windows.Forms.ToolBarButton temp_ToolBarButton;
				temp_ToolBarButton = new System.Windows.Forms.ToolBarButton(lookingBox.Text);
				temp_ToolBarButton.ToolTipText = SupportClass.ToolTipSupport.getToolTipText(lookingBox);
				controls.Toolbar.Buttons.Add(temp_ToolBarButton);
				if (lookingBox.Image != null)
				{
					controls.Toolbar.ImageList.Images.Add(lookingBox.Image);
					temp_ToolBarButton.ImageIndex = controls.Toolbar.ImageList.Images.Count - 1;
				}
				temp_ToolBarButton.Tag = lookingBox;
				lookingBox.Tag = temp_ToolBarButton;
			}
			System.Windows.Forms.ToolBarButton temp_ToolBarButton2;
			temp_ToolBarButton2 = new System.Windows.Forms.ToolBarButton(awayButton.Text);
			temp_ToolBarButton2.ToolTipText = SupportClass.ToolTipSupport.getToolTipText(awayButton);
			controls.Toolbar.Buttons.Add(temp_ToolBarButton2);
			if (awayButton.Image != null)
			{
				controls.Toolbar.ImageList.Images.Add(awayButton.Image);
				temp_ToolBarButton2.ImageIndex = controls.Toolbar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton2.Tag = awayButton;
			awayButton.Tag = temp_ToolBarButton2;
		}
		
		public virtual void  uninitializeControls(ChatServerControls controls)
		{
			if (includeLooking)
			{
				controls.Toolbar.Buttons.Remove((System.Windows.Forms.ToolBarButton) lookingBox.Tag);
			}
			controls.Toolbar.Buttons.Remove((System.Windows.Forms.ToolBarButton) awayButton.Tag);
		}
	}
}