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
using Resources = VassalSharp.i18n.Resources;
using MenuManager = VassalSharp.tools.menu.MenuManager;
namespace VassalSharp.chat
{
	
	/// <summary> Manages {@link PrivateChatter} instances</summary>
	public class PrivateChatManager
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(VassalSharp.chat.Player sender, PrivateChatManager enclosingInstance)
			{
				InitBlock(sender, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.chat.Player sender, PrivateChatManager enclosingInstance)
			{
				this.sender = sender;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable sender was copied into class AnonymousClassWindowAdapter. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.chat.Player sender;
			private PrivateChatManager enclosingInstance;
			public PrivateChatManager Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs e)
			{
				e.Cancel = true;
				Enclosing_Instance.promptToBan(sender);
			}
		}
		private ChatServerConnection client;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < Entry > chatters;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < Player > banned;
		
		public PrivateChatManager(ChatServerConnection client)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			chatters = new ArrayList < Entry >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			banned = new ArrayList < Player >();
			this.client = client;
		}
		
		public virtual PrivateChatter getChatterFor(Player sender)
		{
			if (banned.contains(sender))
			{
				return null;
			}
			PrivateChatter chat = null;
			int index = chatters.indexOf(new Entry(sender, null));
			if (index >= 0)
			{
				chat = chatters.get_Renamed(index).chatter;
			}
			if (chat == null)
			{
				chat = new PrivateChatter(sender, client);
				chatters.add(new Entry(sender, chat));
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Form f = new System.Windows.Forms.Form();
				//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
				f.Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(sender, this).windowClosing);
				
				f.setTitle(Resources.getString("Chat.private_channel", sender.getName())); //$NON-NLS-1$
				f.Menu = MenuManager.Instance.getMenuBarFor(f);
				//UPGRADE_TODO: Method 'javax.swing.JFrame.getContentPane' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJFramegetContentPane'"
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				((System.Windows.Forms.ContainerControl) f).Controls.Add(chat);
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				f.pack();
				//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_int_int'"
				//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
				Toolkit.getDefaultToolkit();
				f.Location = new System.Drawing.Point(System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Width / 2 - f.Size.Width / 2, 0);
			}
			return chat;
		}
		
		private void  promptToBan(Player p)
		{
			//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.YES_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			if ((int) System.Windows.Forms.DialogResult.Yes == SupportClass.OptionPaneSupport.ShowConfirmDialog(null, Resources.getString("Chat.ignore_messages", p.getName()), null, (int) System.Windows.Forms.MessageBoxButtons.YesNo))
			{
				banned.add(p);
			}
		}
		
		private class Entry
		{
			private Player player;
			private PrivateChatter chatter;
			
			internal Entry(Player p, PrivateChatter chat)
			{
				if (p == null)
				{
					throw new System.NullReferenceException();
				}
				player = p;
				chatter = chat;
			}
			
			public  override bool Equals(System.Object o)
			{
				if (o is Entry)
				{
					return player.Equals(((Entry) o).player);
				}
				else
				{
					return false;
				}
			}
			//UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1306'"
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}
		}
	}
}