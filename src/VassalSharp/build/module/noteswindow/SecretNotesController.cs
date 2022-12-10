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
using BadDataReport = VassalSharp.build.BadDataReport;
using GameModule = VassalSharp.build.GameModule;
using Chatter = VassalSharp.build.module.Chatter;
using GameComponent = VassalSharp.build.module.GameComponent;
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using TextConfigurer = VassalSharp.configure.TextConfigurer;
using Resources = VassalSharp.i18n.Resources;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ScrollPane = VassalSharp.tools.ScrollPane;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using WarningDialog = VassalSharp.tools.WarningDialog;
namespace VassalSharp.build.module.noteswindow
{
	
	public class SecretNotesController : GameComponent, CommandEncoder, AddSecretNoteCommand.Interface
	{
		virtual public Command RestoreCommand
		{
			get
			{
				Command comm = null;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(SecretNote note: notes)
				{
					Command c = new AddSecretNoteCommand(this, note);
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
		public const System.String COMMAND_PREFIX = "SNOTE\t"; //$NON-NLS-1$
		
		private Controls controls;
		private System.Windows.Forms.Panel panel;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < SecretNote > notes;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < SecretNote > lastSavedNotes;
		
		// Secret Note display table columns
		public const int COL_HANDLE = 0;
		public const int COL_DTM = 1;
		public const int COL_NAME = 2;
		public const int COL_REVEALED = 3;
		
		// Date formatter to save and restore date/times in the save file
		//UPGRADE_NOTE: Final was removed from the declaration of 'INTERNAL_DATE_FORMATTER '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly System.Globalization.DateTimeFormatInfo INTERNAL_DATE_FORMATTER = SupportClass.GetDateTimeFormatInstance(3, 3, new System.Globalization.CultureInfo("en-US"));
		
		// Date formatter to display date/time to the player
		//UPGRADE_NOTE: Final was removed from the declaration of 'LOCAL_DATE_FORMATTER '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Method 'java.util.Locale.getDefault' was converted to 'System.Threading.Thread.CurrentThread.CurrentCulture' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public static readonly System.Globalization.DateTimeFormatInfo LOCAL_DATE_FORMATTER = SupportClass.GetDateTimeFormatInstance(3, 3, System.Threading.Thread.CurrentThread.CurrentCulture);
		
		public SecretNotesController()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			notes = new ArrayList < SecretNote >();
			controls = new Controls(this);
		}
		
		public virtual void  setup(bool gameStarting)
		{
			if (!gameStarting)
			{
				notes.clear();
				controls.refresh();
			}
		}
		
		public virtual Command decode(System.String command)
		{
			Command comm = null;
			if (command.StartsWith(COMMAND_PREFIX))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(command.Substring(COMMAND_PREFIX.Length), '\t');
				System.String name = st.nextToken();
				System.String owner = st.nextToken();
				bool hidden = "true".Equals(st.nextToken()); //$NON-NLS-1$
				System.String text = TextConfigurer.restoreNewlines(st.nextToken());
				System.String handle = ""; //$NON-NLS-1$
				//UPGRADE_TODO: The 'System.DateTime' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
				System.DateTime date = null;
				
				if (st.hasMoreTokens())
				{
					System.String formattedDate = st.nextToken();
					try
					{
						date = System.DateTime.Parse(formattedDate, INTERNAL_DATE_FORMATTER);
					}
					//UPGRADE_TODO: Class 'java.text.ParseException' was converted to 'System.FormatException' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javatextParseException'"
					catch (System.FormatException e)
					{
						ErrorDialog.dataError(new BadDataReport("Illegal date format", formattedDate, e));
					}
				}
				
				if (st.hasMoreTokens())
				{
					handle = st.nextToken();
				}
				
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				SecretNote note = new SecretNote(name, owner, text, hidden, ref date, handle);
				comm = new AddSecretNoteCommand(this, note);
			}
			return comm;
		}
		
		public virtual System.String encode(Command c)
		{
			System.String s = null;
			if (c is AddSecretNoteCommand)
			{
				SecretNote note = ((AddSecretNoteCommand) c).Note;
				SequenceEncoder se = new SequenceEncoder('\t');
				//UPGRADE_TODO: The 'System.DateTime' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
				System.String date = note.Date == null?"":SupportClass.FormatDateTime(INTERNAL_DATE_FORMATTER, note.Date); //$NON-NLS-1$
				s = COMMAND_PREFIX + se.append(note.Name).append(note.Owner).append(note.Hidden).append(TextConfigurer.escapeNewlines(note.Text)).append(date).append(note.Handle).Value;
			}
			return s;
		}
		
		public virtual void  addSecretNote(SecretNote note)
		{
			int index = notes.indexOf(note);
			if (index >= 0)
			{
				notes.set_Renamed(index, note);
			}
			else
			{
				notes.add(0, note);
			}
			controls.refresh();
		}
		
		
		public virtual System.Windows.Forms.Control getControls()
		{
			if (panel == null)
			{
				panel = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				panel.setLayout(new BoxLayout(panel, BoxLayout.Y_AXIS));
				System.Windows.Forms.Label temp_label;
				temp_label = new System.Windows.Forms.Label();
				temp_label.Text = Resources.getString("Notes.visible_once_revealed");
				System.Windows.Forms.Label l = temp_label; //$NON-NLS-1$
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentX' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentX_float'"
				l.setAlignmentX(0.0F);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(l);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(controls);
			}
			return panel;
		}
		
		public virtual Command save()
		{
			Command comm = null;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(SecretNote secretNote: notes)
			{
				int index = lastSavedNotes.indexOf(secretNote);
				if (index < 0 || lastSavedNotes.get_Renamed(index).isHidden() != secretNote.isHidden())
				{
					Command c = new AddSecretNoteCommand(this, secretNote);
					if (comm == null)
					{
						comm = c;
					}
					else
					{
						comm.append(c);
					}
					System.String msg;
					if (index < 0)
					{
						msg = "* " + Resources.getString("Notes.has_created", GlobalOptions.Instance.getPlayerId(), secretNote.getName()) + " *"; //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
					}
					else
					{
						msg = "* " + Resources.getString("Notes.has_revealed", GlobalOptions.Instance.getPlayerId(), secretNote.getName()) + " *"; //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
					}
					c = new Chatter.DisplayText(GameModule.getGameModule().getChatter(), msg);
					c.execute();
					comm.append(c);
				}
			}
			return comm;
		}
		
		public virtual void  captureState()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			lastSavedNotes = new ArrayList < SecretNote >(notes);
		}
		
		public virtual void  restoreState()
		{
			notes.clear();
			notes.addAll(lastSavedNotes);
			controls.refresh();
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Controls' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class Controls:System.Windows.Forms.Panel
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassListSelectionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassListSelectionListener
			{
				public AnonymousClassListSelectionListener(Controls enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Controls enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Controls enclosingInstance;
				public Controls Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  valueChanged(System.Object event_sender, System.EventArgs e)
				{
					//Ignore extra messages.
					//UPGRADE_ISSUE: Method 'javax.swing.event.ListSelectionEvent.getValueIsAdjusting' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventListSelectionEventgetValueIsAdjusting'"
					if (e.getValueIsAdjusting())
						return ;
					
					//UPGRADE_TODO: Interface 'javax.swing.ListSelectionModel' was converted to 'SupportClass.ListSelectionModelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					//UPGRADE_TODO: The method 'javax.swing.event.ListSelectionEvent.getSource' needs to be in a event handling method in order to be properly converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1171'"
					SupportClass.ListSelectionModelSupport lsm = (SupportClass.ListSelectionModelSupport) e.getSource();
					if (lsm.SelectedItems.Count.Equals(0))
					{
						//...//no rows are selected
					}
					else
					{
						Enclosing_Instance.displaySelected();
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener
			{
				public AnonymousClassActionListener(Controls enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Controls enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Controls enclosingInstance;
				public Controls Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.createNewNote();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener1
			{
				public AnonymousClassActionListener1(Controls enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Controls enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Controls enclosingInstance;
				public Controls Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.revealSelectedNote();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener2
			{
				public AnonymousClassActionListener2(VassalSharp.configure.StringConfigurer name, System.Windows.Forms.TextBox text, System.Windows.Forms.Form d, Controls enclosingInstance)
				{
					InitBlock(name, text, d, enclosingInstance);
				}
				private void  InitBlock(VassalSharp.configure.StringConfigurer name, System.Windows.Forms.TextBox text, System.Windows.Forms.Form d, Controls enclosingInstance)
				{
					this.name = name;
					this.text = text;
					this.d = d;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable name was copied into class AnonymousClassActionListener2. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.configure.StringConfigurer name;
				//UPGRADE_NOTE: Final variable text was copied into class AnonymousClassActionListener2. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Windows.Forms.TextBox text;
				//UPGRADE_NOTE: Final variable d was copied into class AnonymousClassActionListener2. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Windows.Forms.Form d;
				private Controls enclosingInstance;
				public Controls Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'note '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SecretNote note = new SecretNote(name.ValueString, GameModule.getUserId(), (System.String) Enclosing_Instance.text.getValue(), true);
					
					if (notes.contains(note))
					{
						WarningDialog.show(Enclosing_Instance, "Notes.note_exists");
					}
					else
					{
						notes.add(0, note);
						Enclosing_Instance.refresh();
						d.Dispose();
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener
			{
				public AnonymousClassPropertyChangeListener(System.Windows.Forms.Button okButton, VassalSharp.configure.StringConfigurer name, System.Windows.Forms.TextBox text, Controls enclosingInstance)
				{
					InitBlock(okButton, name, text, enclosingInstance);
				}
				private void  InitBlock(System.Windows.Forms.Button okButton, VassalSharp.configure.StringConfigurer name, System.Windows.Forms.TextBox text, Controls enclosingInstance)
				{
					this.okButton = okButton;
					this.name = name;
					this.text = text;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable okButton was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Windows.Forms.Button okButton;
				//UPGRADE_NOTE: Final variable name was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.configure.StringConfigurer name;
				//UPGRADE_NOTE: Final variable text was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Windows.Forms.TextBox text;
				private Controls enclosingInstance;
				public Controls Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
				{
					okButton.Enabled = name.ValueString != null && name.ValueString.Length > 0 && Enclosing_Instance.text.getValueString() != null && Enclosing_Instance.text.getValueString().length() > 0;
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener3
			{
				public AnonymousClassActionListener3(System.Windows.Forms.Form d, Controls enclosingInstance)
				{
					InitBlock(d, enclosingInstance);
				}
				private void  InitBlock(System.Windows.Forms.Form d, Controls enclosingInstance)
				{
					this.d = d;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable d was copied into class AnonymousClassActionListener3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private System.Windows.Forms.Form d;
				private Controls enclosingInstance;
				public Controls Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					d.Dispose();
				}
			}
			private void  InitBlock(SecretNotesController enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				columnNames = new System.String[]{Resources.getString("Notes.player"), Resources.getString("Notes.date_time"), Resources.getString("Notes.note_name"), Resources.getString("Notes.revealed")};
			}
			private SecretNotesController enclosingInstance;
			public SecretNotesController Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			private System.Windows.Forms.TextBox text;
			//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			private System.Windows.Forms.DataGrid table;
			private System.Windows.Forms.Button revealButton;
			
			//UPGRADE_NOTE: The initialization of  'columnNames' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private System.String[] columnNames;
			
			public Controls(SecretNotesController enclosingInstance)
			{
				InitBlock(enclosingInstance);
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
				
				System.Windows.Forms.DataGrid temp_DataGrid;
				temp_DataGrid = new System.Windows.Forms.DataGrid();
				temp_DataGrid.DataSource = new MyTableModel(this);
				table = temp_DataGrid;
				initColumns(table);
				
				//UPGRADE_ISSUE: Method 'javax.swing.JTable.setSelectionMode' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablesetSelectionMode_int'"
				//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.ListSelectionModel.SINGLE_SELECTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				table.setSelectionMode((int) System.Windows.Forms.SelectionMode.One);
				//UPGRADE_TODO: Interface 'javax.swing.ListSelectionModel' was converted to 'SupportClass.ListSelectionModelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionModel'"
				SupportClass.ListSelectionModelSupport rowSM = table.getSelectionModel();
				//UPGRADE_ISSUE: Method 'javax.swing.ListSelectionModel.addListSelectionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingListSelectionModeladdListSelectionListener_javaxswingeventListSelectionListener'"
				rowSM.addListSelectionListener(new AnonymousClassListSelectionListener(this));
				
				//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Windows.Forms.ScrollableControl secretScroll = new ScrollPane(table);
				//UPGRADE_ISSUE: Method 'javax.swing.JTable.setPreferredScrollableViewportSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablesetPreferredScrollableViewportSize_javaawtDimension'"
				table.setPreferredScrollableViewportSize(new System.Drawing.Size(500, 100));
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(secretScroll);
				
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box b = Box.createHorizontalBox();
				b.setAlignmentX(0.0F);
				
				System.Windows.Forms.Button newButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.NEW));
				newButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(newButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(newButton);
				
				revealButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString("Notes.reveal")); //$NON-NLS-1$
				revealButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(revealButton);
				revealButton.Enabled = false;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(revealButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(b);
				
				
				System.Windows.Forms.TextBox temp_TextBox;
				temp_TextBox = new System.Windows.Forms.TextBox();
				temp_TextBox.Multiline = true;
				temp_TextBox.WordWrap = false;
				temp_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
				text = temp_TextBox;
				text.ReadOnly = !false;
				//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Windows.Forms.ScrollableControl scroll = new ScrollPane(text);
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.border.TitledBorder.TitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingborderTitledBorder'"
				System.Windows.Forms.ControlPaint.DrawBorder3D(scroll.CreateGraphics(), 0, 0, scroll.Width, scroll.Height, new TitledBorder(Resources.getString("Notes.text"))); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(scroll);
			}
			
			//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			private void  initColumns(System.Windows.Forms.DataGrid t)
			{
				//UPGRADE_TODO: Class 'javax.swing.table.TableColumn' was converted to 'System.Data.DataColumn' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.Data.DataColumn column = null;
				for (int i = 0; i < columnNames.Length; i++)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.table.TableColumnModel.getColumn' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					column = ((System.Data.DataTable) t.DataSource).Columns[i];
					int width;
					switch (i)
					{
						
						case VassalSharp.build.module.noteswindow.SecretNotesController.COL_HANDLE: 
							width = 60;
							break;
						
						case VassalSharp.build.module.noteswindow.SecretNotesController.COL_DTM: 
							width = 100;
							break;
						
						case VassalSharp.build.module.noteswindow.SecretNotesController.COL_NAME: 
							width = 280;
							break;
						
						case VassalSharp.build.module.noteswindow.SecretNotesController.COL_REVEALED: 
							width = 60;
							break;
						
						default: 
							width = 100;
							break;
						
					}
					//UPGRADE_ISSUE: Method 'javax.swing.table.TableColumn.setPreferredWidth' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableColumnsetPreferredWidth_int'"
					column.setPreferredWidth(width);
				}
			}
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MyTableModel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			//UPGRADE_TODO: Class 'javax.swing.table.AbstractTableModel' was converted to 'System.Data.DataTable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			[Serializable]
			public class MyTableModel:System.Data.DataTable
			{
				public MyTableModel(Controls enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Controls enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
					return getValueAt(0, c).getClass();
				}
				private Controls enclosingInstance;
				public Controls Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				private const long serialVersionUID = 1L;
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.getColumnName' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public System.String getColumnName(int col)
				{
					return Enclosing_Instance.columnNames[col];
				}
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.getRowCount' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public int getRowCount()
				{
					return notes.size();
				}
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.getColumnCount' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public int getColumnCount()
				{
					return Enclosing_Instance.columnNames.Length;
				}
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.getValueAt' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public System.Object getValueAt(int row, int col)
				{
					SecretNote note = notes.get_Renamed(row);
					switch (col)
					{
						
						case VassalSharp.build.module.noteswindow.SecretNotesController.COL_HANDLE: 
							return note.Handle;
						
						case VassalSharp.build.module.noteswindow.SecretNotesController.COL_DTM: 
							//UPGRADE_TODO: The 'System.DateTime' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
							return note.Date == null?"":SupportClass.FormatDateTime(VassalSharp.build.module.noteswindow.SecretNotesController.LOCAL_DATE_FORMATTER, note.Date); //$NON-NLS-1$
						
						case VassalSharp.build.module.noteswindow.SecretNotesController.COL_NAME: 
							return note.Name;
						
						case VassalSharp.build.module.noteswindow.SecretNotesController.COL_REVEALED: 
							return Boolean.valueOf(!note.Hidden);
						
						default: 
							return null;
						
					}
				}
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				public Class < ? > getColumnClass(int c)
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.isCellEditable' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public bool isCellEditable(int row, int col)
				{
					return false;
				}
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.setValueAt' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public void  setValueAt(System.Object value_Renamed, int row, int col)
				{
				}
			}
			
			public virtual void  refresh()
			{
				table.DataSource = new MyTableModel(this);
				initColumns(table);
				displaySelected();
			}
			
			private void  revealSelectedNote()
			{
				int selectedRow = table.CurrentRowIndex;
				if (selectedRow < 0)
				{
					return ;
				}
				System.String selectedName = (System.String) ((System.Data.DataTable) table.DataSource).Rows[selectedRow][VassalSharp.build.module.noteswindow.SecretNotesController.COL_NAME];
				SecretNote note = Enclosing_Instance.getNoteForName(selectedName);
				
				if (note.Owner.Equals(GameModule.getUserId()))
				{
					System.DateTime tempAux = note.Date;
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					note = new SecretNote(note.Name, note.Owner, note.Text, false, ref tempAux, note.Handle);
					if (note != null)
					{
						int i = notes.indexOf(note);
						notes.set_Renamed(i, note);
						refresh();
					}
				}
			}
			
			public virtual void  createNewNote()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'parent '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.getAncestorOfClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				System.Windows.Forms.Form parent = (System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), this);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Form d;
				if (parent != null)
				{
					//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.CreateDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtDialog_boolean'"
					d = SupportClass.DialogSupport.CreateDialog(parent);
				}
				else
				{
					//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.CreateDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_boolean'"
					//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.getAncestorOfClass' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
					//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
					d = SupportClass.DialogSupport.CreateDialog((System.Windows.Forms.Form) SwingUtilities.getAncestorOfClass(typeof(System.Windows.Forms.Form), this));
				}
				
				d.Text = Resources.getString("Notes.delayed_note"); //$NON-NLS-1$
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				StringConfigurer name = new StringConfigurer(null, Resources.getString("Notes.name")); //$NON-NLS-1$
				//UPGRADE_NOTE: Final was removed from the declaration of 'text '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				TextConfigurer text = new TextConfigurer(null, Resources.getString("Notes.text")); //$NON-NLS-1$
				
				//UPGRADE_ISSUE: Method 'javax.swing.JDialog.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJDialogsetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				d.setLayout(new BoxLayout(((System.Windows.Forms.ContainerControl) d), BoxLayout.Y_AXIS));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				d.Controls.Add(name.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				d.Controls.Add(text.Controls);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'buttonPanel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box buttonPanel = Box.createHorizontalBox();
				//UPGRADE_NOTE: Final was removed from the declaration of 'okButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button okButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.OK));
				okButton.Click += new System.EventHandler(new AnonymousClassActionListener2(name, text, d, this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(okButton);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				PropertyChangeListener l = new AnonymousClassPropertyChangeListener(okButton, name, text, this);
				name.PropertyChange += new SupportClass.PropertyChangeEventHandler(l.propertyChange);
				text.PropertyChange += new SupportClass.PropertyChangeEventHandler(l.propertyChange);
				
				okButton.Enabled = false;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				buttonPanel.Controls.Add(okButton);
				//UPGRADE_NOTE: Final was removed from the declaration of 'cancelButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Button cancelButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.CANCEL));
				cancelButton.Click += new System.EventHandler(new AnonymousClassActionListener3(d, this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(cancelButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				d.Controls.Add(buttonPanel);
				
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				d.pack();
				System.Windows.Forms.Form generatedAux7 = d.Owner;
				//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
				d.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(d, "Visible", true);
			}
			
			public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs e)
			{
				if (event_sender is System.Windows.Forms.MenuItem)
					((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
				displaySelected();
			}
			
			private void  displaySelected()
			{
				revealButton.Enabled = false;
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				text.Text = ""; //$NON-NLS-1$
				
				int selectedRow = table.CurrentRowIndex;
				if (selectedRow < 0)
				{
					return ;
				}
				System.String selectedName = (System.String) ((System.Data.DataTable) table.DataSource).Rows[selectedRow][VassalSharp.build.module.noteswindow.SecretNotesController.COL_NAME];
				SecretNote note = Enclosing_Instance.getNoteForName(selectedName);
				
				if (note != null)
				{
					if (note.Owner.Equals(GameModule.getUserId()))
					{
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						text.Text = note.Text;
						revealButton.Enabled = note.Hidden;
					}
					else
					{
						//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
						text.Text = note.Hidden?Resources.getString("Notes.message_not_revealed"):note.Text; //$NON-NLS-1$
					}
				}
			}
		}
		
		public virtual SecretNote getNoteForName(System.String s)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(SecretNote n: notes)
			{
				if (n.getName().equals(s))
				{
					return n;
				}
			}
			return null;
		}
	}
}