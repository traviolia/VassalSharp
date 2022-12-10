/*
* $Id$
*
* Copyright (c) 2003 by Brent Easton and Rodney Kinney
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
*
* @author Brent Easton
*
* Enhanced Dice Button includes access to Internet Die Servers via the DieManager.
*
*/
using System;
using AutoConfigurable = VassalSharp.build.AutoConfigurable;
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using Configurer = VassalSharp.configure.Configurer;
using FormattedStringConfigurer = VassalSharp.configure.FormattedStringConfigurer;
using Resources = VassalSharp.i18n.Resources;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
namespace VassalSharp.build.module
{
	
	/// <summary> This component places a button into the controls window toolbar. Pressing the button generates random numbers and
	/// displays the result in the Chatter
	/// </summary>
	public class InternetDiceButton:DiceButton, GameComponent, CommandEncoder
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Class < ? > [] c = super.getAttributeTypes();
			for (int i = 0; i < c.length; ++i)
			{
				if (c[i] == typeof(ReportFormatConfig))
				{
					c[i] = typeof(InternetReportFormatConfig);
				}
			}
			return c;
		}
		new public static System.String ConfigureTypeName
		{
			get
			{
				return Resources.getString("Editor.InternetDiceButton.component_type"); //$NON-NLS-1$
			}
			
		}
		virtual public Command RestoreCommand
		{
			get
			{
				return new SetSecondaryEmail(dieManager.Server.SecondaryEmail);
			}
			
		}
		protected internal static DieManager dieManager;
		private const System.String COMMAND_PREFIX = "SEMAIL\t"; //$NON-NLS-1$
		/// <summary>Report format variale </summary>
		public const System.String DETAILS = "rollDetails"; //$NON-NLS-1$
		
		public InternetDiceButton():base()
		{
			InitBlock();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		
		public class InternetReportFormatConfig:ReportFormatConfig
		{
			public override Configurer getConfigurer(AutoConfigurable c, System.String key, System.String name)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'config '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				FormattedStringConfigurer config = (FormattedStringConfigurer) base.getConfigurer(c, key, name);
				config.setOptions(ArrayUtils.append(config.Options, VassalSharp.build.module.InternetDiceButton.DETAILS));
				return config;
			}
		}
		
		/// <summary> Ask the die manager to do our roll!</summary>
		protected internal override void  DR()
		{
			reportFormat.setProperty(NAME, LocalizedConfigureName);
			dieManager.roll(nDice, nSides, plus, reportTotal, LocalizedConfigureName, reportFormat);
		}
		
		/// <summary> Expects to be added to the DieManager.</summary>
		public override void  addTo(Buildable parent)
		{
			initDieManager();
			dieManager.addDieButton(this);
			GameModule.getGameModule().addCommandEncoder(this);
			GameModule.getGameModule().getGameState().addGameComponent(this);
			base.addTo(parent);
		}
		
		protected internal virtual void  initDieManager()
		{
			if (dieManager == null)
			{
				dieManager = new DieManager();
				dieManager.build(null);
			}
		}
		
		public override void  removeFrom(Buildable b)
		{
			dieManager.removeDieButton(this);
			GameModule.getGameModule().removeCommandEncoder(this);
			GameModule.getGameModule().getGameState().removeGameComponent(this);
			base.removeFrom(b);
		}
		
		public virtual void  setup(bool gameStarting)
		{
		}
		
		public virtual Command decode(System.String command)
		{
			Command comm = null;
			if (command.StartsWith(COMMAND_PREFIX))
			{
				comm = new SetSecondaryEmail(command.Substring(COMMAND_PREFIX.Length));
			}
			return comm;
		}
		
		public virtual System.String encode(Command c)
		{
			System.String s = null;
			if (c is SetSecondaryEmail)
			{
				s = COMMAND_PREFIX + ((SetSecondaryEmail) c).msg;
			}
			return s;
		}
		
		private class SetSecondaryEmail:Command
		{
			private System.String msg;
			
			internal SetSecondaryEmail(System.String s)
			{
				msg = s;
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override void  executeCommand()
			{
				VassalSharp.build.module.InternetDiceButton.dieManager.setSecondaryEmail(msg);
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override Command myUndoCommand()
			{
				return null;
			}
		}
		
		public override HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("GameModule.htm", "InternetDiceButton"); //$NON-NLS-1$ //$NON-NLS-2$
		}
	}
}