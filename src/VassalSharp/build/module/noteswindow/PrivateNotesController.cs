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
using GameComponent = VassalSharp.build.module.GameComponent;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using TextConfigurer = VassalSharp.configure.TextConfigurer;
using Resources = VassalSharp.i18n.Resources;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.build.module.noteswindow
{
	
	/// <summary> Holds {@link PrivateText} objects, only displaying the one owned by the current user</summary>
	public class PrivateNotesController : GameComponent, CommandEncoder, SetPrivateTextCommand.Interface
	{
		virtual public System.Windows.Forms.Control Controls
		{
			get
			{
				if (controls == null)
				{
					//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					Box b = Box.createVerticalBox();
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = Resources.getString("Notes.invisible");
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					b.Controls.Add(temp_Control); //$NON-NLS-1$
					text = new TextConfigurer(null, null);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					b.Controls.Add(text.Controls);
					controls = b;
				}
				return controls;
			}
			
		}
		virtual public Command RestoreCommand
		{
			get
			{
				Command comm = null;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(PrivateText privateText: notes)
				{
					SetPrivateTextCommand c = new SetPrivateTextCommand(this, privateText);
					if (comm == null)
					{
						comm = c;
					}
					else
					{
						comm.append(c);
					}
				}
				return comm;
			}
			
		}
		public const System.String COMMAND_PREFIX = "PNOTE\t"; //$NON-NLS-1$
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private Set < PrivateText > notes;
		private System.String myLastSavedNotes;
		private System.Windows.Forms.Control controls;
		private TextConfigurer text;
		
		public PrivateNotesController()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			notes = new HashSet < PrivateText >();
		}
		
		public virtual void  addPrivateText(PrivateText p)
		{
			notes.remove(p);
			notes.add(p);
			if (p.Owner.Equals(GameModule.getUserId()))
			{
				text.setValue(p.Text);
			}
		}
		
		public virtual Command decode(System.String command)
		{
			Command c = null;
			if (command.StartsWith(COMMAND_PREFIX))
			{
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(command.Substring(COMMAND_PREFIX.Length), '\t');
				System.String owner = st.nextToken();
				System.String text = st.hasMoreTokens()?TextConfigurer.restoreNewlines(st.nextToken()):""; //$NON-NLS-1$
				return new SetPrivateTextCommand(this, new PrivateText(owner, text));
			}
			return c;
		}
		
		public virtual System.String encode(Command c)
		{
			System.String s = null;
			if (c is SetPrivateTextCommand)
			{
				PrivateText t = ((SetPrivateTextCommand) c).PrivateText;
				SequenceEncoder se = new SequenceEncoder('\t');
				s = COMMAND_PREFIX + se.append(t.Owner).append(TextConfigurer.escapeNewlines(t.Text)).Value;
			}
			return s;
		}
		
		public virtual void  setup(bool gameStarting)
		{
			if (!gameStarting)
			{
				notes.clear();
				text.setValue(""); //$NON-NLS-1$
			}
		}
		
		public virtual Command save()
		{
			Command comm = null;
			if (!myLastSavedNotes.Equals(text.getValue()))
			{
				comm = new SetPrivateTextCommand(this, new PrivateText(GameModule.getUserId(), (System.String) text.getValue()));
				comm.execute();
			}
			return comm;
		}
		
		public virtual void  captureState()
		{
			myLastSavedNotes = ((System.String) text.getValue());
		}
		
		public virtual void  restoreState()
		{
			text.setValue((System.Object) myLastSavedNotes);
		}
	}
}