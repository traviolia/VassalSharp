using System;
using GameModule = VassalSharp.build.GameModule;
using PlayerRoster = VassalSharp.build.module.PlayerRoster;
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
	
	/// <summary> Represents a text note with an owner and hidden/revealed status.
	/// This is an immutable object
	/// </summary>
	public class SecretNote
	{
		virtual public bool Hidden
		{
			get
			{
				return hidden;
			}
			
		}
		virtual public System.String Name
		{
			get
			{
				return name;
			}
			
		}
		virtual public System.String Owner
		{
			get
			{
				return owner;
			}
			
		}
		virtual public System.DateTime Date
		{
			get
			{
				return date;
			}
			
		}
		virtual public System.String Handle
		{
			get
			{
				return handle;
			}
			
		}
		virtual public System.String Text
		{
			get
			{
				return text;
			}
			
		}
		private System.String owner; // Owner's Password
		private System.String name; // Name of Note
		private System.String text; // Text of Note
		private bool hidden = true; // Is note still hidden?
		private System.DateTime date; // Date/Time stamp
		private System.String handle = ""; // Owner's handle //$NON-NLS-1$
		
		public SecretNote(System.String name, System.String owner, System.String text, bool hidden)
		{
			this.name = name;
			this.owner = owner;
			this.text = text;
			this.hidden = hidden;
			
			if (PlayerRoster.Active && PlayerRoster.getMySide() != null)
			{
				this.handle = PlayerRoster.getMySide();
			}
			else
			{
				this.handle = ((System.String) GameModule.getGameModule().getPrefs().getOption(GameModule.REAL_NAME).getValue());
			}
			this.date = System.DateTime.Now;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public SecretNote(System.String name, System.String owner, System.String text, bool hidden, ref System.DateTime created, System.String id):this(name, owner, text, hidden)
		{
			this.date = created;
			this.handle = id;
		}
		
		/// <summary> Two SecretNotes with the same owner and name are considered equal</summary>
		/// <param name="o">
		/// </param>
		/// <returns>
		/// </returns>
		public  override bool Equals(System.Object o)
		{
			if (this == o)
				return true;
			if (!(o is SecretNote))
				return false;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'secretNote '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SecretNote secretNote = (SecretNote) o;
			
			if (name != null?!name.Equals(secretNote.name):secretNote.name != null)
				return false;
			if (owner != null?!owner.Equals(secretNote.owner):secretNote.owner != null)
				return false;
			
			return true;
		}
		
		public override int GetHashCode()
		{
			int result;
			result = (owner != null?owner.GetHashCode():0);
			result = 29 * result + (name != null?name.GetHashCode():0);
			return result;
		}
	}
}