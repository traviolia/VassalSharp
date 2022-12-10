/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
/*
* Created by IntelliJ IDEA.
* User: rkinney
* Date: Sep 3, 2002
* Time: 10:13:28 PM
* To change template for new class use
* Code Style | Class Templates options (Tools | IDE Options).
*/
using System;
using GameModule = VassalSharp.build.GameModule;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using Configurer = VassalSharp.configure.Configurer;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.build.module
{
	
	/// <summary> Determines whether players are allowed to unmask other players pieces.  The module designer may
	/// set the option to always on, always off, or let the players determine it with a Preferences setting.
	/// </summary>
	public class ObscurableOptions : CommandEncoder, GameComponent
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(ObscurableOptions enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ObscurableOptions enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ObscurableOptions enclosingInstance;
			public ObscurableOptions Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				if (true.Equals(evt.NewValue))
				{
					ObscurableOptions.Instance.allow(GameModule.getUserId());
					System.String side = PlayerRoster.getMySide();
					if (side != null)
					{
						ObscurableOptions.Instance.allow(side);
					}
				}
				else
				{
					ObscurableOptions.Instance.disallow(GameModule.getUserId());
					System.String side = PlayerRoster.getMySide();
					if (side != null)
					{
						ObscurableOptions.Instance.disallow(side);
					}
				}
				GameModule.getGameModule().getServer().sendToOthers(new SetAllowed(VassalSharp.build.module.ObscurableOptions.instance.allowed));
			}
		}
		/// <summary> Return the current global ObscurableOptions</summary>
		/// <returns> global Options
		/// </returns>
		public static ObscurableOptions Instance
		{
			get
			{
				return instance;
			}
			
		}
		/// <summary> Set the text accompanying the "Allow opponent to unmask" control in the Preferences</summary>
		virtual public System.String Prompt
		{
			set
			{
				Configurer c = GameModule.getGameModule().getPrefs().getOption(PREFS_KEY);
				if (c != null)
				{
					c.setName(value);
				}
			}
			
		}
		virtual public Command RestoreCommand
		{
			get
			{
				return new SetAllowed(allowed);
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'instance '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly ObscurableOptions instance = new ObscurableOptions();
		
		public const System.String COMMAND_ID = "UNMASK\t"; //$NON-NLS-1$
		public const System.String PREFS_KEY = "OpponentUnmaskable"; //$NON-NLS-1$
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < String > allowed = new ArrayList < String >();
		private System.Boolean override_Renamed;
		
		private ObscurableOptions()
		{
		}
		
		/// <summary> Create a private set of ObscurableOptions. If no setting are passed,
		/// use the current global settings.
		/// </summary>
		/// <param name="settings">encoded settings
		/// </param>
		public ObscurableOptions(System.String settings):this()
		{
			if (settings != null && settings.Length > 0)
			{
				decodeOptions(settings);
			}
			else
			{
				decodeOptions(Instance.encodeOptions());
			}
		}
		
		public virtual void  allowSome(System.String preferencesPrompt)
		{
			Configurer c = new BooleanConfigurer(PREFS_KEY, preferencesPrompt);
			GameModule.getGameModule().getPrefs().addOption(c);
			c.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
			if (true.Equals(c.getValue()))
			{
				allow(GameModule.getUserId());
			}
			else
			{
				disallow(GameModule.getUserId());
			}
		}
		
		public virtual void  allowAll()
		{
			override_Renamed = true;
		}
		
		public virtual void  allowNone()
		{
			override_Renamed = false;
		}
		
		public virtual void  allow(System.String id)
		{
			if (!allowed.contains(id))
			{
				allowed.add(id);
			}
		}
		
		public virtual void  disallow(System.String id)
		{
			allowed.remove(id);
		}
		
		public virtual Command decode(System.String command)
		{
			if (command.StartsWith(COMMAND_ID))
			{
				command = command.Substring(COMMAND_ID.Length);
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ArrayList < String > l = new ArrayList < String >();
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(command, '\t');
				while (st.hasMoreTokens())
				{
					l.add(st.nextToken());
				}
				return new SetAllowed(l);
			}
			else
			{
				return null;
			}
		}
		
		public virtual System.String encode(Command c)
		{
			if (c is SetAllowed)
			{
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				if (l.isEmpty())
				{
					return COMMAND_ID;
				}
				else
				{
					SequenceEncoder se = new SequenceEncoder('\t');
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					for(String s: l)
					{
						se.append(s);
					}
					return COMMAND_ID + se.Value;
				}
			}
			else
			{
				return null;
			}
		}
		
		/// <summary> Encode the current ObscurableOptions as a String</summary>
		/// <returns> encoded options
		/// </returns>
		public virtual System.String encodeOptions()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder('|');
			//UPGRADE_TODO: The 'System.Boolean' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
			if (override_Renamed == null)
			{
				se.append("");
			}
			else
			{
				se.append(override_Renamed);
			}
			se.append(allowed.size());
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String who: allowed)
			{
				se.append(who);
			}
			
			return se.Value;
		}
		
		/// <summary> Set the current options from an encoded string</summary>
		/// <param name="s">encoded string
		/// </param>
		public virtual void  decodeOptions(System.String s)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'sd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, '|');
			System.String setting = sd.nextToken("");
			if (setting.Length == 0)
			{
				//UPGRADE_TODO: The 'System.Boolean' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
				override_Renamed = null;
			}
			else
			{
				override_Renamed = setting.Equals("true");
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'count '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int count = sd.nextInt(0);
			allowed.clear();
			for (int i = 0; i < count; i++)
			{
				setting = sd.nextToken("");
				if (setting.Length > 0)
				{
					allowed.add(setting);
				}
			}
		}
		
		public virtual void  setup(bool gameStarting)
		{
			if (!gameStarting)
			{
				allowed.clear();
			}
			else if (true.Equals(GameModule.getGameModule().getPrefs().getValue(PREFS_KEY)))
			{
				allow(GameModule.getUserId());
			}
		}
		
		/// <returns> true if pieces belonging to the given id are unmaskable by
		/// other players
		/// </returns>
		public virtual bool isUnmaskable(System.String id)
		{
			//UPGRADE_TODO: The 'System.Boolean' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
			return override_Renamed != null?override_Renamed:allowed.contains(id);
		}
		
		public class SetAllowed:Command
		{
			public SetAllowed()
			{
				InitBlock();
			}
			private void  InitBlock()
			{
				this.allowed = allowed;
				this.allowed = allowed;
				return allowed;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private List < String > allowed;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public SetAllowed(List < String > allowed)
			
			/// <deprecated> Use {@link #SetAllowed(List<String>)} instead. 
			/// </deprecated>
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Deprecated
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public SetAllowed(Vector < String > allowed)
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public List < String > getAllowedIds()
			
			//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override void  executeCommand()
			{
				//UPGRADE_TODO: The 'System.Boolean' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
				ObscurableOptions.Instance.override_Renamed = null;
				ObscurableOptions.Instance.allowed = this.allowed;
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override Command myUndoCommand()
			{
				return null;
			}
		}
	}
}