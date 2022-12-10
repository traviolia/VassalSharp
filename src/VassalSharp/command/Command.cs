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
using System;
using System.Collections.Generic;
using System.Text;

namespace VassalSharp.command
{
	
	/// <summary>
    /// Command is an abstract class that does something. Any action that takes
	/// place during a game should be encapsulated in a Command object. When
	/// performing actions during a game, corresponding Commands will be logged
	/// in the current logfile and/or sent to other players on the server.
	/// 
	/// Commands can be strung together into compound commands with the
	/// {@link #append} method.
	/// 
	/// </summary>
	/// <seealso cref="CommandEncoder">
	/// </seealso>
	public abstract class Command
	{
		virtual public Command[] SubCommands
		{
			get
			{
				return seq.ToArray();
			}
			
		}
		/// <returns> true if this command does nothing
		/// </returns>
		virtual public bool Null
		{
			get
			{
				return false;
			}
			
		}
		/// <summary> </summary>
		/// <returns> true if this command should be stored in a logfile
		/// </returns>
		virtual public bool Loggable
		{
			get
			{
				return !Null;
			}
			
		}
		/// <summary>
        /// Return true if this command has no sub-commands attached to it
		/// (other than null commands).
		/// </summary>
		/// <returns>
		/// </returns>
		virtual protected internal bool Atomic
		{
			get
			{
				foreach(Command c in seq)
				{
					if (!c.Null)
					{
						return false;
					}
				}
				return true;
			}
			
		}
		/// <summary>
        /// Detailed information for ToString()
        /// </summary>
		virtual public System.String Details
		{
			get
			{
				return null;
			}
			
		}
		/// <returns> 
        /// a Command that undoes not only this Command's action, but also
		/// the actions of all its subcommands.
		/// </returns>
		virtual public Command UndoCommand
		{
			get
			{
				if (undo == null)
				{
					undo = new NullCommand();
					undo = undo.append(myUndoCommand());
				}
				return undo;
			}
			

		}
		private  List < Command > seq = new  List < Command >();
		private Command undo;
		
		public Command()
		{
		}
		
		/// <summary> 
        /// Execute this command by first invoking {@link #executeCommand}, then
		/// invoking {@link #execute} recursively on all subcommands.
		/// </summary>
		public virtual void  execute()
		{
			try
			{
				executeCommand();
			}
			catch (System.Exception t)
			{
				handleFailure(t);
				
				List < Command > oldSeq = seq;
				stripSubCommands();
				seq = oldSeq;
			}
			
			foreach (Command cmd in seq)
			{
				try
				{
					cmd.execute();
				}
				catch (System.Exception t)
				{
					handleFailure(t);
				}
			}
		}
		
		private void  handleFailure(System.Exception t)
		{
			throw t;
		}
		
		/// <summary>
        /// Perform the action which this Command represents
        /// </summary>
		public abstract void  executeCommand();
		
		/// <summary>
        /// If the action can be undone, return a Command that performs the
		/// inverse action. The Command returned should only undo
		/// {@link #executeCommand}, not the actions of subcommands
		/// </summary>
		public abstract Command myUndoCommand();
		
		/// <summary> Remove all subcommands.</summary>
		public virtual void  stripSubCommands()
		{
			seq = new List < Command >();
		}
		
		/// <deprecated> Use {@link #isAtomic()}
		/// </deprecated>
		protected internal virtual bool hasNullSubcommands()
		{
			return Atomic;
		}
		
		public override System.String ToString()
		{
			StringBuilder sb = new StringBuilder(this.GetType().Name);
			System.String details = Details;
			if (details != null)
				sb.Append("[").Append(details).Append("]");
			
			foreach (Command c in seq) sb.Append('+').Append(c.ToString());
			
			return sb.ToString();
		}
		
		/// <summary> Append a subcommand to this Command.</summary>
		public virtual Command append(Command c)
		{
			Command retval = this;
			if (c != null && !c.Null)
			{
				if (Null)
				{
					retval = c;
				}
				seq.Add(c);
			}
			return retval;
		}
	}
}