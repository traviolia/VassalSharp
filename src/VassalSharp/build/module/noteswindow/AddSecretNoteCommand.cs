using System;
using Command = VassalSharp.command.Command;
namespace VassalSharp.build.module.noteswindow
{
	
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
	
	/// <summary> When executed, adds a {@link SecretNote} to a specified List.</summary>
	public class AddSecretNoteCommand:Command
	{
		virtual public SecretNote Note
		{
			get
			{
				return note;
			}
			
		}
		private AddSecretNoteCommand.Interface i;
		private SecretNote note;
		
		public AddSecretNoteCommand(AddSecretNoteCommand.Interface i, SecretNote note)
		{
			this.i = i;
			this.note = note;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override void  executeCommand()
		{
			i.addSecretNote(note);
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override Command myUndoCommand()
		{
			return null;
		}
		
		public interface Interface
		{
			void  addSecretNote(SecretNote note);
		}
	}
}