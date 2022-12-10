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
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using AutoConfigurable = VassalSharp.build.AutoConfigurable;
using Buildable = VassalSharp.build.Buildable;
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using PrivateNotesController = VassalSharp.build.module.noteswindow.PrivateNotesController;
using SecretNotesController = VassalSharp.build.module.noteswindow.SecretNotesController;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using NullCommand = VassalSharp.command.NullCommand;
using Configurer = VassalSharp.configure.Configurer;
using ConfigurerFactory = VassalSharp.configure.ConfigurerFactory;
using IconConfigurer = VassalSharp.configure.IconConfigurer;
using TextConfigurer = VassalSharp.configure.TextConfigurer;
using Resources = VassalSharp.i18n.Resources;
using LaunchButton = VassalSharp.tools.LaunchButton;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
namespace VassalSharp.build.module
{
	
	/// <summary> This is a {@link GameComponent} that allows players to type and
	/// save text notes during a game.  There is one set of shared public
	/// notes, and each player has a set of private notes visible only to
	/// him
	/// </summary>
	public class NotesWindow:AbstractConfigurable, GameComponent, CommandEncoder
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(NotesWindow enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(NotesWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private NotesWindow enclosingInstance;
			public NotesWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.captureState();
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(Enclosing_Instance.frame, "Visible", !Enclosing_Instance.frame.Visible);
			}
		}
		private void  InitBlock()
		{
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new Class < ? > []
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				String.
			}
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[]{BUTTON_TEXT, TOOLTIP, ICON, HOT_KEY};
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[]{Resources.getString(Resources.BUTTON_TEXT), Resources.getString(Resources.TOOLTIP_TEXT), Resources.getString(Resources.BUTTON_ICON), Resources.getString(Resources.HOTKEY_LABEL)};
			}
			
		}
		
		protected internal System.Windows.Forms.Form frame;
		protected internal LaunchButton launch;
		protected internal TextConfigurer scenarioNotes;
		protected internal TextConfigurer publicNotes;
		protected internal PrivateNotesController privateNotes;
		protected internal SecretNotesController secretNotes;
		protected internal const System.String SCENARIO_NOTE_COMMAND_PREFIX = "NOTES\t"; //$NON-NLS-1$
		protected internal const System.String PUBLIC_NOTE_COMMAND_PREFIX = "PNOTES\t"; //$NON-NLS-1$
		
		public const System.String HOT_KEY = "hotkey"; //$NON-NLS-1$
		public const System.String ICON = "icon"; //$NON-NLS-1$
		public const System.String BUTTON_TEXT = "buttonText"; //$NON-NLS-1$
		public const System.String TOOLTIP = "tooltip"; //$NON-NLS-1$
		
		protected internal System.String lastSavedScenarioNotes;
		protected internal System.String lastSavedPublicNotes;
		
		public NotesWindow()
		{
			InitBlock();
			privateNotes = new PrivateNotesController(this);
			secretNotes = new SecretNotesController(this);
			frame = new NotesDialog(this);
			frame.Text = Resources.getString("Notes.notes"); //$NON-NLS-1$
			//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			ActionListener al = new AnonymousClassActionListener(this);
			launch = new LaunchButton(Resources.getString("Notes.notes"), TOOLTIP, BUTTON_TEXT, HOT_KEY, ICON, al); //$NON-NLS-1$
			launch.setAttribute(ICON, "/images/notes.gif"); //$NON-NLS-1$
			launch.setToolTipText(Resources.getString("Notes.notes")); //$NON-NLS-1$
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			frame.pack();
			setup(false);
		}
		
		/// <summary> Capture this object's state, to be restored if the user hits "Cancel"</summary>
		protected internal virtual void  captureState()
		{
			lastSavedScenarioNotes = ((System.String) scenarioNotes.getValue());
			lastSavedPublicNotes = ((System.String) publicNotes.getValue());
			privateNotes.captureState();
			secretNotes.captureState();
		}
		
		public virtual void  cancel()
		{
			restoreState();
			privateNotes.restoreState();
			secretNotes.restoreState();
		}
		
		protected internal virtual void  restoreState()
		{
			scenarioNotes.setValue(lastSavedScenarioNotes);
			publicNotes.setValue(lastSavedPublicNotes);
		}
		
		protected internal virtual void  save()
		{
			Command c = new NullCommand();
			if (!lastSavedScenarioNotes.Equals(scenarioNotes.getValue()))
			{
				c.append(new SetScenarioNote(scenarioNotes.ValueString));
			}
			if (!lastSavedPublicNotes.Equals(publicNotes.getValue()))
			{
				c.append(new SetPublicNote(publicNotes.ValueString));
			}
			c.append(privateNotes.save());
			c.append(secretNotes.save());
			GameModule.getGameModule().sendAndLog(c);
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'NotesDialog' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		protected internal class NotesDialog:System.Windows.Forms.Form
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassWindowAdapter
			{
				public AnonymousClassWindowAdapter(NotesDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(NotesDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private NotesDialog enclosingInstance;
				public NotesDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs e)
				{
					e.Cancel = true;
					Enclosing_Instance.Enclosing_Instance.cancel();
					Enclosing_Instance.Visible = false;
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener1
			{
				public AnonymousClassActionListener1(NotesDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(NotesDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private NotesDialog enclosingInstance;
				public NotesDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.Enclosing_Instance.save();
					Enclosing_Instance.Visible = false;
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener2
			{
				public AnonymousClassActionListener2(NotesDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(NotesDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private NotesDialog enclosingInstance;
				public NotesDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.Enclosing_Instance.cancel();
					Enclosing_Instance.Visible = false;
				}
			}
			private void  InitBlock(NotesWindow enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private NotesWindow enclosingInstance;
			public NotesWindow Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			private const long serialVersionUID = 1L;
			
			protected internal NotesDialog(NotesWindow enclosingInstance):base(GameModule.getGameModule().getFrame())
			{
				InitBlock(enclosingInstance);
				initComponents();
				System.Windows.Forms.Form generatedAux = Owner;
				//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
				StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			}
			
			protected internal virtual void  initComponents()
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JDialog.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJDialogsetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				setLayout(new BoxLayout(((System.Windows.Forms.ContainerControl) this), BoxLayout.Y_AXIS));
				Closing += new System.ComponentModel.CancelEventHandler(this.NotesDialog_Closing_DO_NOTHING_ON_CLOSE);
				//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
				Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
				
				Enclosing_Instance.scenarioNotes = new TextConfigurer(null, null);
				Enclosing_Instance.publicNotes = new TextConfigurer(null, null);
				System.Windows.Forms.TabControl tab = new System.Windows.Forms.TabControl();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(tab);
				
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box b = Box.createVerticalBox();
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = Resources.getString("Notes.visible_to_all");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				b.Controls.Add(temp_Control); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(Enclosing_Instance.scenarioNotes.Controls);
				//UPGRADE_TODO: Method 'javax.swing.JTabbedPane.addTab' was converted to 'SupportClass.TabControlSupport.AddTab' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTabbedPaneaddTab_javalangString_javaawtComponent'"
				SupportClass.TabControlSupport.AddTab(tab, Resources.getString("Notes.scenario"), b); //$NON-NLS-1$
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				b = Box.createVerticalBox();
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = Resources.getString("Notes.visible_to_all");
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label4;
				b.Controls.Add(temp_Control2); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				b.Controls.Add(Enclosing_Instance.publicNotes.Controls);
				//UPGRADE_TODO: Method 'javax.swing.JTabbedPane.addTab' was converted to 'SupportClass.TabControlSupport.AddTab' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTabbedPaneaddTab_javalangString_javaawtComponent'"
				SupportClass.TabControlSupport.AddTab(tab, Resources.getString("Notes.public"), b); //$NON-NLS-1$
				
				//UPGRADE_TODO: Method 'javax.swing.JTabbedPane.addTab' was converted to 'SupportClass.TabControlSupport.AddTab' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTabbedPaneaddTab_javalangString_javaawtComponent'"
				SupportClass.TabControlSupport.AddTab(tab, Resources.getString("Notes.private"), Enclosing_Instance.privateNotes.Controls); //$NON-NLS-1$
				
				//UPGRADE_TODO: Method 'javax.swing.JTabbedPane.addTab' was converted to 'SupportClass.TabControlSupport.AddTab' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTabbedPaneaddTab_javalangString_javaawtComponent'"
				SupportClass.TabControlSupport.AddTab(tab, Resources.getString("Notes.delayed"), Enclosing_Instance.secretNotes.getControls()); //$NON-NLS-1$
				
				System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
				System.Windows.Forms.Button saveButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.SAVE));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(saveButton);
				saveButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(saveButton);
				System.Windows.Forms.Button cancelButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.CANCEL));
				cancelButton.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(cancelButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(cancelButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(p);
			}
			private void  NotesDialog_Closing_DO_NOTHING_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
			{
				e.Cancel = true;
				SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 0);
			}
		}
		
		
		public override HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("GameModule.htm", "NotesWindow"); //$NON-NLS-1$ //$NON-NLS-2$
		}
		
		public override void  setAttribute(System.String name, System.Object value_Renamed)
		{
			launch.setAttribute(name, value_Renamed);
		}
		
		public override System.String getAttributeValueString(System.String name)
		{
			return launch.getAttributeValueString(name);
		}
		
		public virtual System.String encode(Command c)
		{
			System.String s = null;
			if (c is SetScenarioNote)
			{
				s = SCENARIO_NOTE_COMMAND_PREFIX + ((SetScenarioNote) c).msg;
			}
			else if (c is SetPublicNote)
			{
				s = PUBLIC_NOTE_COMMAND_PREFIX + ((SetPublicNote) c).msg;
			}
			else
			{
				s = privateNotes.encode(c);
				if (s == null)
				{
					s = secretNotes.encode(c);
				}
			}
			return s;
		}
		
		public virtual Command decode(System.String command)
		{
			Command comm;
			if (command.StartsWith(SCENARIO_NOTE_COMMAND_PREFIX))
			{
				comm = new SetScenarioNote(command.Substring(SCENARIO_NOTE_COMMAND_PREFIX.Length));
			}
			else if (command.StartsWith(PUBLIC_NOTE_COMMAND_PREFIX))
			{
				comm = new SetPublicNote(command.Substring(PUBLIC_NOTE_COMMAND_PREFIX.Length));
			}
			else
			{
				comm = privateNotes.decode(command);
				if (comm == null)
				{
					comm = secretNotes.decode(command);
				}
			}
			return comm;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		String.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		IconConfig.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class, 
		NamedKeyStroke.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		class
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	public class IconConfig : ConfigurerFactory
	{
		public virtual Configurer getConfigurer(AutoConfigurable c, System.String key, System.String name)
		{
			return new IconConfigurer(key, name, ((NotesWindow) c).launch.getAttributeValueString(ICON));
		}
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Configurable [] getConfigureComponents()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new Configurable [0];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Class < ? > [] getAllowableConfigureComponents()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return new Class < ? > [0];
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public static String getConfigureTypeName()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return Resources.getString(Editor.NotesWindow.component_type); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Expects to be added to a {@link VassalSharp.build.GameModule}.  Adds a button to
	/// the controls window toolbar to show the window containing the
	/// notes 
	/// </summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void addTo(Buildable b)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		GameModule.getGameModule().getToolBar().add(launch);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	launch.setAlignmentY(0.0F);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().addCommandEncoder(this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().getGameState().addGameComponent(this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().addCommandEncoder(privateNotes);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().getGameState().addGameComponent(privateNotes);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().addCommandEncoder(secretNotes);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().getGameState().addGameComponent(secretNotes);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void removeFrom(Buildable b)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		GameModule.getGameModule().getToolBar().remove(launch);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().removeCommandEncoder(this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().getGameState().removeGameComponent(this);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().removeCommandEncoder(privateNotes);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().getGameState().removeGameComponent(privateNotes);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().removeCommandEncoder(secretNotes);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().getGameState().removeGameComponent(secretNotes);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setup(boolean show)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		launch.setEnabled(show);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(!show)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		scenarioNotes.setValue(); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	publicNotes.setValue(); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Command getRestoreCommand()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		Command c = new SetScenarioNote(scenarioNotes.getValueString());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	c.append(new SetPublicNote(publicNotes.getValueString()));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	c.append(privateNotes.getRestoreCommand());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	c.append(secretNotes.getRestoreCommand());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return c;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	protected internal class SetScenarioNote:Command
	{
		protected internal System.String msg;
		
		protected internal SetScenarioNote(System.String s)
		{
			msg = s;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override void  executeCommand()
		{
			scenarioNotes.setValue(msg);
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override Command myUndoCommand()
		{
			return null;
		}
	}
	
	protected internal class SetPublicNote:Command
	{
		protected internal System.String msg;
		
		protected internal SetPublicNote(System.String s)
		{
			msg = s;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override void  executeCommand()
		{
			publicNotes.setValue(msg);
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override Command myUndoCommand()
		{
			return null;
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}