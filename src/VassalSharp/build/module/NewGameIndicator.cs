/*
* $Id$
*
* Copyright (c) 2004 by Rodney Kinney
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
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
namespace VassalSharp.build.module
{
	
	/// <summary> Provides information about whether a game was started from File->New Game
	/// or loaded from a saved game
	/// </summary>
	public class NewGameIndicator : GameComponent, CommandEncoder
	{
		virtual public Command RestoreCommand
		{
			get
			{
				return new MarkGameNotNew(this);
			}
			
		}
		/// <summary> </summary>
		/// <returns> true if the current game was started from the menu,
		/// false if it was loaded from a saved game or logfile.
		/// </returns>
		virtual public bool NewGame
		{
			get
			{
				return isNewGame_Renamed_Field;
			}
			
		}
		private System.String command;
		private bool isNewGame_Renamed_Field;
		
		public NewGameIndicator(System.String command)
		{
			this.command = command;
			GameModule.getGameModule().getGameState().addGameComponent(this);
			GameModule.getGameModule().addCommandEncoder(this);
		}
		
		public virtual void  setup(bool gameStarting)
		{
			if (!gameStarting)
			{
				isNewGame_Renamed_Field = true;
			}
		}
		
		public virtual Command decode(System.String command)
		{
			Command c = null;
			if (command.StartsWith(this.command))
			{
				return new MarkGameNotNew(this);
			}
			return c;
		}
		
		public virtual System.String encode(Command c)
		{
			System.String s = null;
			if (c is MarkGameNotNew && ((MarkGameNotNew) c).indicator == this)
			{
				s = command;
			}
			return s;
		}
		
		public class MarkGameNotNew:Command
		{
			private NewGameIndicator indicator;
			
			public MarkGameNotNew(NewGameIndicator indicator)
			{
				this.indicator = indicator;
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override void  executeCommand()
			{
				indicator.isNewGame_Renamed_Field = false;
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override Command myUndoCommand()
			{
				return null;
			}
		}
	}
}