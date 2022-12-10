/*
* $Id$
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
using Info = VassalSharp.Info;
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using AbstractMetaData = VassalSharp.build.module.metadata.AbstractMetaData;
using MetaDataFactory = VassalSharp.build.module.metadata.MetaDataFactory;
using SaveMetaData = VassalSharp.build.module.metadata.SaveMetaData;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using Logger = VassalSharp.command.Logger;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using IconConfigurer = VassalSharp.configure.IconConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using Resources = VassalSharp.i18n.Resources;
using Launcher = VassalSharp.launch.Launcher;
using KeyStrokeListener = VassalSharp.tools.KeyStrokeListener;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using NamedKeyStrokeListener = VassalSharp.tools.NamedKeyStrokeListener;
using WriteErrorDialog = VassalSharp.tools.WriteErrorDialog;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
using LogFileFilter = VassalSharp.tools.filechooser.LogFileFilter;
using FastByteArrayOutputStream = VassalSharp.tools.io.FastByteArrayOutputStream;
using FileArchive = VassalSharp.tools.io.FileArchive;
using IOUtils = VassalSharp.tools.io.IOUtils;
using ObfuscatingOutputStream = VassalSharp.tools.io.ObfuscatingOutputStream;
using ZipArchive = VassalSharp.tools.io.ZipArchive;
using MenuManager = VassalSharp.tools.menu.MenuManager;
using Dialogs = VassalSharp.tools.swing.Dialogs;
namespace VassalSharp.build.module
{
	
	public class BasicLogger : Logger, Buildable, GameComponent, CommandEncoder
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(VassalSharp.configure.IconConfigurer stepIconConfig, BasicLogger enclosingInstance)
			{
				InitBlock(stepIconConfig, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.configure.IconConfigurer stepIconConfig, BasicLogger enclosingInstance)
			{
				this.stepIconConfig = stepIconConfig;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable stepIconConfig was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.IconConfigurer stepIconConfig;
			private BasicLogger enclosingInstance;
			public BasicLogger Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.Action.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.SMALL_ICON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionSMALL_ICON_f'"
				Enclosing_Instance.stepAction.putValue(Action.SMALL_ICON, stepIconConfig.IconValue);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener1
		{
			public AnonymousClassPropertyChangeListener1(VassalSharp.configure.IconConfigurer undoIconConfig, BasicLogger enclosingInstance)
			{
				InitBlock(undoIconConfig, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.configure.IconConfigurer undoIconConfig, BasicLogger enclosingInstance)
			{
				this.undoIconConfig = undoIconConfig;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable undoIconConfig was copied into class AnonymousClassPropertyChangeListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.IconConfigurer undoIconConfig;
			private BasicLogger enclosingInstance;
			public BasicLogger Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.Action.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.SMALL_ICON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionSMALL_ICON_f'"
				Enclosing_Instance.undoAction.putValue(Action.SMALL_ICON, undoIconConfig.IconValue);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener2
		{
			public AnonymousClassPropertyChangeListener2(VassalSharp.tools.NamedKeyStrokeListener stepKeyListener, VassalSharp.configure.NamedHotKeyConfigurer stepKeyConfig, BasicLogger enclosingInstance)
			{
				InitBlock(stepKeyListener, stepKeyConfig, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.tools.NamedKeyStrokeListener stepKeyListener, VassalSharp.configure.NamedHotKeyConfigurer stepKeyConfig, BasicLogger enclosingInstance)
			{
				this.stepKeyListener = stepKeyListener;
				this.stepKeyConfig = stepKeyConfig;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable stepKeyListener was copied into class AnonymousClassPropertyChangeListener2. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.tools.NamedKeyStrokeListener stepKeyListener;
			//UPGRADE_NOTE: Final variable stepKeyConfig was copied into class AnonymousClassPropertyChangeListener2. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.NamedHotKeyConfigurer stepKeyConfig;
			private BasicLogger enclosingInstance;
			public BasicLogger Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				stepKeyListener.setKeyStroke(stepKeyConfig.ValueNamedKeyStroke);
				//UPGRADE_ISSUE: Method 'javax.swing.Action.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionputValue_javalangString_javalangObject'"
				//UPGRADE_ISSUE: Field 'javax.swing.Action.SHORT_DESCRIPTION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionSHORT_DESCRIPTION_f'"
				Enclosing_Instance.stepAction.putValue(Action.SHORT_DESCRIPTION, Resources.getString("BasicLogger.step_forward_tooltip2", NamedHotKeyConfigurer.getString(stepKeyListener.getKeyStroke()))); //$NON-NLS-1$
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		internal class AnonymousClassAbstractAction:SupportClass.ActionSupport
		{
			private void  InitBlock(BasicLogger enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicLogger enclosingInstance;
			public BasicLogger Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction(BasicLogger enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			} //$NON-NLS-1$
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				try
				{
					Enclosing_Instance.write();
					GameModule.getGameModule().warn(Resources.getString("BasicLogger.logfile_written")); //$NON-NLS-1$
					//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
					Enclosing_Instance.newLogAction.setEnabled(true);
					GameModule.getGameModule().appendToTitle(null);
					Enclosing_Instance.outputFile = null;
				}
				catch (System.IO.IOException ex)
				{
					WriteErrorDialog.error(ex, Enclosing_Instance.outputFile);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		internal class AnonymousClassAbstractAction1:SupportClass.ActionSupport
		{
			private void  InitBlock(BasicLogger enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicLogger enclosingInstance;
			public BasicLogger Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction1(BasicLogger enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			} //$NON-NLS-1$
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.beginOutput();
			}
		}
		private void  InitBlock()
		{
			stepAction = new StepAction(this);
			undoAction = new UndoAction(this);
			endLogAction = new AnonymousClassAbstractAction(this, Resources.getString("BasicLogger.end_logfile"));
			newLogAction = new AnonymousClassAbstractAction1(this, Resources.getString("BasicLogger.begin_logfile"));
		}
		virtual public bool Logging
		{
			get
			{
				return outputFile != null;
			}
			
		}
		virtual public Command RestoreCommand
		{
			get
			{
				return null;
			}
			
		}
		private System.IO.FileInfo SaveFile
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GameModule g = GameModule.getGameModule();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				FileChooser fc = g.getFileChooser();
				fc.addChoosableFileFilter(new LogFileFilter());
				
				System.String name = fc.SelectedFile == null?null:fc.SelectedFile.Name;
				if (name != null)
				{
					int index = name.LastIndexOf('.');
					if (index > 0)
					{
						name = name.Substring(0, (index) - (0)) + ".vlog"; //$NON-NLS-1$
						fc.SelectedFile = new System.IO.FileInfo(fc.SelectedFile.DirectoryName + "\\" + name);
					}
				}
				
				if (fc.showSaveDialog() != FileChooser.APPROVE_OPTION)
					return null;
				
				System.IO.FileInfo file = fc.SelectedFile;
				if (file.Name.IndexOf('.') == - 1)
					file = new System.IO.FileInfo(file.DirectoryName + "\\" + file.Name + ".vlog");
				
				// warn user if overwriting log from an old version
				bool tmpBool;
				if (System.IO.File.Exists(file.FullName))
					tmpBool = true;
				else
					tmpBool = System.IO.Directory.Exists(file.FullName);
				if (tmpBool)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'md '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					AbstractMetaData md = MetaDataFactory.buildMetaData(file);
					if (md != null && md is SaveMetaData)
					{
						if (Info.hasOldFormat(md.VassalVersion))
						{
							
							//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							int result = Dialogs.showConfirmDialog(g.Frame, Resources.getString("Warning.log_will_be_updated_title"), Resources.getString("Warning.log_will_be_updated_heading"), Resources.getString("Warning.log_will_be_updated_message", file.FullName, "3.2"), (int) System.Windows.Forms.MessageBoxIcon.Exclamation, (int) System.Windows.Forms.MessageBoxButtons.OKCancel);
							
							switch (result)
							{
								
								//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.CANCEL_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
								case (int) System.Windows.Forms.DialogResult.Cancel: 
								//UPGRADE_ISSUE: Field 'javax.swing.JOptionPane.CLOSED_OPTION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
								case JOptionPane.CLOSED_OPTION: 
									return null;
								}
						}
					}
				}
				
				return file;
			}
			
		}
		public const System.String BEGIN = "begin_log"; //$NON-NLS-1$
		public const System.String END = "end_log"; //$NON-NLS-1$
		public const System.String LOG = "LOG\t"; //$NON-NLS-1$
		public const System.String PROMPT_NEW_LOG = "PromptNewLog"; //$NON-NLS-1$
		public const System.String PROMPT_NEW_LOG_START = "PromptNewLogStart"; //$NON-NLS-1$
		public const System.String PROMPT_NEW_LOG_END = "PromptNewLogEnd"; //$NON-NLS-1$
		public const System.String PROMPT_LOG_COMMENT = "promptLogComment"; //$NON-NLS-1$
		protected internal const System.String STEP_ICON = "/images/StepForward16.gif"; //$NON-NLS-1$
		protected internal const System.String UNDO_ICON = "/images/Undo16.gif"; //$NON-NLS-1$
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < Command > logInput;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < Command > logOutput;
		protected internal int nextInput = 0;
		protected internal int nextUndo = - 1;
		protected internal Command beginningState;
		protected internal System.IO.FileInfo outputFile;
		//UPGRADE_NOTE: The initialization of  'stepAction' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal SupportClass.ActionSupport stepAction;
		protected internal SaveMetaData metadata;
		
		public BasicLogger():base()
		{
			InitBlock();
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			stepAction.setEnabled(false);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			undoAction.setEnabled(false);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			endLogAction.setEnabled(false);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			newLogAction.setEnabled(false);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			logInput = new ArrayList < Command >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			logOutput = new ArrayList < Command >();
		}
		
		public virtual void  build(System.Xml.XmlElement e)
		{
		}
		
		/// <summary> Expects to be added to a {@link GameModule}. Adds <code>Undo</code>,
		/// <code>Step</code>, and <code>End Log</code> buttons to the the control
		/// window toolbar. Registers {@link KeyStrokeListener}s for hotkey
		/// equivalents of each button.
		/// </summary>
		public virtual void  addTo(Buildable b)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'mod '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GameModule mod = GameModule.getGameModule();
			
			mod.addCommandEncoder(this);
			mod.getGameState().addGameComponent(this);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'mm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuManager mm = MenuManager.Instance;
			// FIMXE: setting nmemonic from first letter could cause collisions in
			// some languages
			//UPGRADE_ISSUE: Field 'javax.swing.Action.MNEMONIC_KEY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionMNEMONIC_KEY_f'"
			newLogAction.putValue(Action.MNEMONIC_KEY, (int) Resources.getString("BasicLogger.begin_logfile.shortcut")[0]);
			mm.addAction("BasicLogger.begin_logfile", newLogAction);
			// FIMXE: setting nmemonic from first letter could cause collisions in
			// some languages
			//UPGRADE_ISSUE: Field 'javax.swing.Action.MNEMONIC_KEY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionMNEMONIC_KEY_f'"
			endLogAction.putValue(Action.MNEMONIC_KEY, (int) Resources.getString("BasicLogger.end_logfile.shortcut")[0]);
			mm.addAction("BasicLogger.end_logfile", endLogAction);
			
			System.Windows.Forms.ToolBarButton temp_ToolBarButton;
			System.Windows.Forms.Button temp_Button;
			temp_Button = SupportClass.ButtonSupport.CreateStandardButton(undoAction.Description, undoAction.Icon);
			temp_Button.Click += new System.EventHandler(undoAction.actionPerformed);
			temp_ToolBarButton = new System.Windows.Forms.ToolBarButton(temp_Button.Text);
			mod.ToolBar.Buttons.Add(temp_ToolBarButton);
			if (temp_Button.Image != null)
			{
				mod.ToolBar.ImageList.Images.Add(temp_Button.Image);
				temp_ToolBarButton.ImageIndex = mod.ToolBar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton.Tag = temp_Button;
			temp_Button.Tag = temp_ToolBarButton;
			System.Windows.Forms.Button button = temp_Button;
			if (button is VassalSharp.tools.LaunchButton)
				((VassalSharp.tools.LaunchButton) button).setToolTipText(Resources.getString("BasicLogger.undo_last_move"));
			else
				SupportClass.ToolTipSupport.setToolTipText(button, Resources.getString("BasicLogger.undo_last_move")); //$NON-NLS-1$
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentY_float'"
			button.setAlignmentY((float) 0.0);
			
			System.Windows.Forms.ToolBarButton temp_ToolBarButton2;
			System.Windows.Forms.Button temp_Button2;
			temp_Button2 = SupportClass.ButtonSupport.CreateStandardButton(stepAction.Description, stepAction.Icon);
			temp_Button2.Click += new System.EventHandler(stepAction.actionPerformed);
			temp_ToolBarButton2 = new System.Windows.Forms.ToolBarButton(temp_Button2.Text);
			mod.ToolBar.Buttons.Add(temp_ToolBarButton2);
			if (temp_Button2.Image != null)
			{
				mod.ToolBar.ImageList.Images.Add(temp_Button2.Image);
				temp_ToolBarButton2.ImageIndex = mod.ToolBar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton2.Tag = temp_Button2;
			temp_Button2.Tag = temp_ToolBarButton2;
			button = temp_Button2;
			if (button is VassalSharp.tools.LaunchButton)
				((VassalSharp.tools.LaunchButton) button).setToolTipText(Resources.getString("BasicLogger.step_forward_tooltip"));
			else
				SupportClass.ToolTipSupport.setToolTipText(button, Resources.getString("BasicLogger.step_forward_tooltip")); //$NON-NLS-1$
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentY_float'"
			button.setAlignmentY((float) 0.0);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'stepKeyListener '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			NamedKeyStrokeListener stepKeyListener = new NamedKeyStrokeListener(stepAction, NamedKeyStroke.getNamedKeyStroke((int) System.Windows.Forms.Keys.PageDown, 0));
			mod.addKeyStrokeListener(stepKeyListener);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'newLogKeyListener '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: The equivalent in .NET for field 'java.awt.Event.ALT_MASK' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			KeyStrokeListener newLogKeyListener = new KeyStrokeListener(newLogAction, new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((int) System.Windows.Forms.Keys.W | (int) System.Windows.Forms.Keys.Alt)));
			mod.addKeyStrokeListener(newLogKeyListener);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'stepIconConfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			IconConfigurer stepIconConfig = new IconConfigurer("stepIcon", Resources.getString("BasicLogger.step_forward_button"), STEP_ICON); //$NON-NLS-1$ //$NON-NLS-2$
			stepIconConfig.setValue(STEP_ICON);
			GlobalOptions.Instance.addOption(stepIconConfig);
			stepIconConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(stepIconConfig, this).propertyChange);
			stepIconConfig.fireUpdate();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'undoIconConfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			IconConfigurer undoIconConfig = new IconConfigurer("undoIcon", Resources.getString("BasicLogger.undo_icon"), UNDO_ICON); //$NON-NLS-1$ //$NON-NLS-2$
			undoIconConfig.setValue(UNDO_ICON);
			GlobalOptions.Instance.addOption(undoIconConfig);
			undoIconConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener1(undoIconConfig, this).propertyChange);
			undoIconConfig.fireUpdate();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'stepKeyConfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			NamedHotKeyConfigurer stepKeyConfig = new NamedHotKeyConfigurer("stepHotKey", Resources.getString("BasicLogger.step_forward_hotkey"), stepKeyListener.NamedKeyStroke); //$NON-NLS-1$ //$NON-NLS-2$
			GlobalOptions.Instance.addOption(stepKeyConfig);
			stepKeyConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener2(stepKeyListener, stepKeyConfig, this).propertyChange);
			stepKeyConfig.fireUpdate();
			
			System.Boolean tempAux = false;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			BooleanConfigurer logOptionStart = new BooleanConfigurer(PROMPT_NEW_LOG_START, Resources.getString("BasicLogger.prompt_new_log_before"), ref tempAux); //$NON-NLS-1$
			mod.getPrefs().addOption(Resources.getString("Prefs.general_tab"), logOptionStart); //$NON-NLS-1$
			
			System.Boolean tempAux2 = true;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			BooleanConfigurer logOptionEnd = new BooleanConfigurer(PROMPT_NEW_LOG_END, Resources.getString("BasicLogger.prompt_new_log_after"), ref tempAux2); //$NON-NLS-1$
			mod.getPrefs().addOption(Resources.getString("Prefs.general_tab"), logOptionEnd); //$NON-NLS-1$
			
			System.Boolean tempAux3 = true;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			BooleanConfigurer logOptionComment = new BooleanConfigurer(PROMPT_LOG_COMMENT, Resources.getString("BasicLogger.enable_comments"), ref tempAux3); //$NON-NLS-1$
			mod.getPrefs().addOption(Resources.getString("Prefs.general_tab"), logOptionComment); //$NON-NLS-1$
		}
		
		public virtual System.Xml.XmlElement getBuildElement(System.Xml.XmlDocument doc)
		{
			return doc.CreateElement(GetType().FullName);
		}
		
		public virtual void  add(Buildable b)
		{
		}
		
		public virtual void  remove(Buildable b)
		{
		}
		
		public virtual void  setup(bool show)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			newLogAction.setEnabled(show);
			if (show)
			{
				logOutput.clear();
				nextInput = 0;
				nextUndo = - 1;
				beginningState = GameModule.getGameModule().getGameState().RestoreCommand;
			}
			else
			{
				//UPGRADE_ISSUE: Method 'javax.swing.Action.isEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionisEnabled'"
				if (endLogAction.isEnabled())
				{
					//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.YES_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					if (JOptionPane.showConfirmDialog(GameModule.getGameModule().getFrame(), Resources.getString("BasicLogger.save_log"), Resources.getString("BasicLogger.unsaved_log"), (int) System.Windows.Forms.MessageBoxButtons.YesNo) == (int) System.Windows.Forms.DialogResult.Yes)
					{
						try
						{
							write();
						}
						catch (System.IO.IOException e)
						{
							// BasicLogger is not a lumberjack
							WriteErrorDialog.error(e, outputFile);
						}
					}
				}
				
				logInput.clear();
				beginningState = null;
				//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
				undoAction.setEnabled(false);
				//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
				endLogAction.setEnabled(false);
				//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
				stepAction.setEnabled(false);
				outputFile = null;
			}
		}
		
		public virtual void  enableDrawing(bool show)
		{
		}
		
		protected internal virtual void  step()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Command c = logInput.get_Renamed(nextInput++);
			c.execute();
			GameModule.getGameModule().sendAndLog(c);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			stepAction.setEnabled(nextInput < logInput.size());
			if (!(nextInput < logInput.size()))
			{
				queryNewLogFile(false);
			}
		}
		
		/*
		* Check if user would like to create a new logfile
		*/
		public virtual void  queryNewLogFile(bool atStart)
		{
			System.String prefName;
			System.String prompt;
			if (Logging)
			{
				return ;
			}
			
			if (atStart)
			{
				prefName = PROMPT_NEW_LOG_START;
				prompt = Resources.getString("BasicLogger.replay_commencing"); //$NON-NLS-1$
			}
			else
			{
				prefName = PROMPT_NEW_LOG_END;
				prompt = Resources.getString("BasicLogger.replay_completed"); //$NON-NLS-1$
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GameModule g = GameModule.getGameModule();
			
			if (((System.Boolean) g.getPrefs().getValue(prefName)))
			{
				System.Object[] options = new System.Object[]{Resources.getString(Resources.YES), Resources.getString(Resources.NO), Resources.getString("BasicLogger.dont_prompt_again")};
				
				//UPGRADE_ISSUE: Method 'javax.swing.JOptionPane.showOptionDialog' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
				int result = JOptionPane.showOptionDialog(g.Frame, Resources.getString("BasicLogger.start_new_log_file", prompt), "", (int) System.Windows.Forms.MessageBoxButtons.YesNoCancel, (int) System.Windows.Forms.MessageBoxIcon.Question, null, options, options[0]);
				
				//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.YES_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				if (result == (int) System.Windows.Forms.DialogResult.Yes)
				{
					beginOutput();
				}
				else if (result == 2)
				{
					// Turn Preference Off
					g.getPrefs().setValue(prefName, false);
				}
			}
		}
		
		/// <summary> Write the logfile to a file. The file will have been selected when the logfile was begun.
		/// 
		/// </summary>
		public virtual void  write()
		{
			if (!logOutput.isEmpty())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'log '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Command log = beginningState;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Command c: logOutput)
				{
					log.append(new LogCommand(c, logInput, stepAction));
				}
				
				// FIXME: Extremely inefficient! Make encode write to an OutputStream
				//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String s = GameModule.getGameModule().encode(log);
				//UPGRADE_NOTE: Final was removed from the declaration of 'ba '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				FastByteArrayOutputStream ba = new FastByteArrayOutputStream();
				System.IO.Stream out_Renamed = null;
				try
				{
					out_Renamed = new ObfuscatingOutputStream(ba);
					//UPGRADE_TODO: Method 'java.lang.String.getBytes' was converted to 'System.Text.Encoding.GetEncoding(string).GetBytes(string)' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangStringgetBytes_javalangString'"
					sbyte[] temp_sbyteArray;
					temp_sbyteArray = SupportClass.ToSByteArray(System.Text.Encoding.GetEncoding("UTF-8").GetBytes(s));
					out_Renamed.Write(SupportClass.ToByteArray(temp_sbyteArray), 0, temp_sbyteArray.Length);
					out_Renamed.Close();
				}
				finally
				{
					IOUtils.closeQuietly(out_Renamed);
				}
				
				FileArchive archive = null;
				try
				{
					archive = new ZipArchive(outputFile);
					archive.add(GameState.SAVEFILE_ZIP_ENTRY, ba.toInputStream());
					metadata.save(archive);
					archive.close();
				}
				finally
				{
					IOUtils.closeQuietly(archive);
				}
				
				Launcher.Instance.sendSaveCmd(outputFile);
				
				GameModule.getGameModule().getGameState().setModified(false);
				//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
				undoAction.setEnabled(false);
			}
			
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			endLogAction.setEnabled(false);
		}
		
		protected internal virtual void  beginOutput()
		{
			outputFile = SaveFile;
			if (outputFile == null)
				return ;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'gm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GameModule gm = GameModule.getGameModule();
			
			logOutput.clear();
			beginningState = gm.getGameState().RestoreCommand;
			
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			undoAction.setEnabled(false);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			endLogAction.setEnabled(true);
			gm.appendToTitle(Resources.getString("BasicLogger.logging_to", outputFile.Name));
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			newLogAction.setEnabled(false);
			metadata = new SaveMetaData();
		}
		
		protected internal virtual void  undo()
		{
			Command lastOutput = logOutput.get_Renamed(nextUndo);
			Command lastInput = (nextInput > logInput.size() || nextInput < 1)?null:logInput.get_Renamed(nextInput - 1);
			if (lastInput == lastOutput)
			{
				while (nextInput-- > 0)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
					stepAction.setEnabled(true);
					if (logInput.get_Renamed(nextInput).getUndoCommand() != null)
					{
						break;
					}
				}
			}
			while (nextUndo-- > 0)
			{
				if (logOutput.get_Renamed(nextUndo).getUndoCommand() != null)
				{
					break;
				}
			}
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			undoAction.setEnabled(nextUndo >= 0);
			Command undo = lastOutput.UndoCommand;
			undo.execute();
			GameModule.getGameModule().getServer().sendToOthers(undo);
			logOutput.add(undo);
		}
		
		public virtual void  log(Command c)
		{
			if (c != null && c.Loggable)
			{
				logOutput.add(c);
				if (c.UndoCommand != null && !c.UndoCommand.Null)
				{
					nextUndo = logOutput.size() - 1;
				}
			}
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			undoAction.setEnabled(nextUndo >= 0);
		}
		
		/// <summary> Are there Input Steps yet to be replayed?</summary>
		public virtual bool hasMoreCommands()
		{
			return nextInput < logInput.size();
		}
		
		/// <summary> Recognizes a logging command. The logging command is a wrapper around an ordinary {@link Command} indicating that
		/// the wrapped command should be stored and executed in sequence (when the <code>Step</code> button is pressed)
		/// </summary>
		public virtual System.String encode(Command c)
		{
			if (c is LogCommand)
			{
				return LOG + GameModule.getGameModule().encode(((LogCommand) c).LoggedCommand);
			}
			else
			{
				return null;
			}
		}
		
		public virtual Command decode(System.String command)
		{
			if (command.StartsWith(LOG))
			{
				Command logged = GameModule.getGameModule().decode(command.Substring(LOG.Length));
				if (logged != null)
				{
					return new LogCommand(logged, logInput, stepAction);
				}
			}
			return null;
		}
		
		//UPGRADE_NOTE: The initialization of  'undoAction' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal SupportClass.ActionSupport undoAction;
		
		//UPGRADE_NOTE: The initialization of  'endLogAction' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal SupportClass.ActionSupport endLogAction;
		
		//UPGRADE_NOTE: The initialization of  'newLogAction' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal SupportClass.ActionSupport newLogAction;
		
		public class LogCommand:Command
		{
			public LogCommand()
			{
				InitBlock();
			}
			private void  InitBlock()
			{
				if (c is LogCommand)
				{
					throw new System.NotSupportedException(Resources.getString("BasicLogger.cant_log")); //$NON-NLS-1$
				}
				
				this.logInput = logInput;
				this.stepAction = stepAction;
				logged = c;
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Command sub: c.getSubCommands())
				{
					append(new LogCommand(sub, logInput, stepAction));
				}
				
				logged.stripSubCommands();
			}
			virtual public Command LoggedCommand
			{
				get
				{
					return logged;
				}
				
			}
			protected internal Command logged;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			protected List < Command > logInput;
			protected internal SupportClass.ActionSupport stepAction;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public LogCommand(Command c, List < Command > logInput, Action stepAction)
			
			//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override void  executeCommand()
			{
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override Command myUndoCommand()
			{
				return null;
			}
			
			public override void  execute()
			{
				Command c = assembleCommand();
				logInput.add(c);
				//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
				stepAction.setEnabled(true);
			}
			
			protected internal virtual Command assembleCommand()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Command c = logged;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Command sub: getSubCommands())
				{
					c.append(((LogCommand) sub).assembleCommand());
				}
				return c;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'StepAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		public class StepAction:SupportClass.ActionSupport
		{
			private void  InitBlock(BasicLogger enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicLogger enclosingInstance;
			public BasicLogger Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			public StepAction(BasicLogger enclosingInstance)
			{
				InitBlock(enclosingInstance);
				//UPGRADE_NOTE: Final was removed from the declaration of 'iconURL '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GetType();
				//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
				System.Uri iconURL = new System.Uri(System.IO.Path.GetFullPath(VassalSharp.build.module.BasicLogger.STEP_ICON));
				if (iconURL != null)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
					//UPGRADE_ISSUE: Field 'javax.swing.Action.SMALL_ICON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionSMALL_ICON_f'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
					putValue(Action.SMALL_ICON, new ImageIcon(iconURL));
				}
				else
				{
					//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
					//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
					putValue(Action.NAME, Resources.getString("BasicLogger.step")); //$NON-NLS-1$
				}
			}
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.step();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'UndoAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		public class UndoAction:SupportClass.ActionSupport
		{
			private void  InitBlock(BasicLogger enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BasicLogger enclosingInstance;
			public BasicLogger Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			public UndoAction(BasicLogger enclosingInstance)
			{
				InitBlock(enclosingInstance);
				GetType();
				//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
				System.Uri iconURL = new System.Uri(System.IO.Path.GetFullPath(VassalSharp.build.module.BasicLogger.UNDO_ICON));
				if (iconURL != null)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
					//UPGRADE_ISSUE: Field 'javax.swing.Action.SMALL_ICON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionSMALL_ICON_f'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
					putValue(Action.SMALL_ICON, new ImageIcon(iconURL));
				}
				else
				{
					//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
					//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
					putValue(Action.NAME, Resources.getString("BasicLogger.undo")); //$NON-NLS-1$
				}
			}
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.undo();
			}
		}
	}
}