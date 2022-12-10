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
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using SoundConfigurer = VassalSharp.configure.SoundConfigurer;
using Resources = VassalSharp.i18n.Resources;
using Prefs = VassalSharp.preferences.Prefs;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.chat
{
	
	/// <summary> Encodes commands that play sounds
	/// This class is used exclusively by the 'Send wake-up' server feature.
	/// Limit the number of wake-ups we will respond to in a row from the same player
	/// before querying if we want to ignore them in future.
	/// Wait at least 5 seconds before responding to a new wake-up.
	/// </summary>
	public class SoundEncoder : CommandEncoder
	{
		public const System.String COMMAND_PREFIX = "PLAY\t"; //$NON-NLS-1$
		private PlayerEncoder playerEncoder;
		
		public SoundEncoder(PlayerEncoder p)
		{
			playerEncoder = p;
		}
		
		public virtual Command decode(System.String command)
		{
			if (command.StartsWith(COMMAND_PREFIX))
			{
				SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(command, '\t');
				sd.nextToken();
				//UPGRADE_NOTE: Final was removed from the declaration of 'soundKey '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String soundKey = sd.nextToken();
				//UPGRADE_NOTE: Final was removed from the declaration of 'sender '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Player sender = playerEncoder.stringToPlayer(sd.nextToken("")); //$NON-NLS-1$
				return new Cmd(soundKey, sender);
			}
			else
			{
				return null;
			}
		}
		
		public virtual System.String encode(Command c)
		{
			System.String s = null;
			if (c is Cmd)
			{
				Cmd cmd = (Cmd) c;
				SequenceEncoder se = new SequenceEncoder('\t');
				se.append(cmd.soundKey);
				se.append(playerEncoder.playerToString(cmd.Sender));
				s = COMMAND_PREFIX + se.Value;
			}
			return s;
		}
		
		public class Cmd:Command
		{
			virtual public Player Sender
			{
				get
				{
					return sender;
				}
				
			}
			private const int TOO_MANY = 4;
			public const int TOO_SOON = 10 * 1000; // 10s
			private static long lastTime = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
			private static Player lastSender;
			private static int sendCount;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private static ArrayList < Player > banned = new ArrayList < Player >();
			private static bool updating = false;
			
			private System.String soundKey;
			private Player sender;
			
			public Cmd(System.String soundKey, Player player)
			{
				this.soundKey = soundKey;
				this.sender = player;
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override void  executeCommand()
			{
				/**
				* Ignore if we don't want to hear from this player anymore, we are
				* already processing a wake-up, or we have already processed a wake-up recently
				*/
				//UPGRADE_NOTE: Final was removed from the declaration of 'now '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				long now = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
				if (banned.contains(sender) || updating || (now - lastTime) < (TOO_SOON))
				{
					updating = false;
					return ;
				}
				
				updating = true;
				lastTime = now;
				//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SoundConfigurer c = (SoundConfigurer) Prefs.GlobalPrefs.getOption(soundKey);
				if (c != null)
				{
					c.play();
				}
				if (sender.Equals(lastSender))
				{
					if (sendCount++ >= TOO_MANY)
					{
						//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.YES_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						if ((int) System.Windows.Forms.DialogResult.Yes == SupportClass.OptionPaneSupport.ShowConfirmDialog(null, Resources.getString("Chat.ignore_wakeups", sender.getName()), null, (int) System.Windows.Forms.MessageBoxButtons.YesNo))
						{
							banned.add(sender);
						}
						else
						{
							sendCount = 1;
						}
					}
				}
				else
				{
					lastSender = sender;
					sendCount = 1;
				}
				updating = false;
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override Command myUndoCommand()
			{
				return null;
			}
		}
	}
}