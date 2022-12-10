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
////UPGRADE_TODO: The type 'java.util.concurrent.ExecutionException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using ExecutionException = java.util.concurrent.ExecutionException;
////UPGRADE_TODO: The type 'org.jdesktop.swingworker.SwingWorker' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using SwingWorker = org.jdesktop.swingworker.SwingWorker;
////UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using LoggerFactory = org.slf4j.LoggerFactory;
//using Info = VassalSharp.Info;
//using GameModule = VassalSharp.build.GameModule;
//using PieceCollection = VassalSharp.build.module.map.PieceCollection;
//using AbstractMetaData = VassalSharp.build.module.metadata.AbstractMetaData;
//using MetaDataFactory = VassalSharp.build.module.metadata.MetaDataFactory;
//using SaveMetaData = VassalSharp.build.module.metadata.SaveMetaData;
//using AddPiece = VassalSharp.command.AddPiece;
//using AlertCommand = VassalSharp.command.AlertCommand;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
//using CommandFilter = VassalSharp.command.CommandFilter;
//using ConditionalCommand = VassalSharp.command.ConditionalCommand;
//using Logger = VassalSharp.command.Logger;
//using NullCommand = VassalSharp.command.NullCommand;
//using DirectoryConfigurer = VassalSharp.configure.DirectoryConfigurer;
//using GamePiece = VassalSharp.counters.GamePiece;
//using Resources = VassalSharp.i18n.Resources;
//using Launcher = VassalSharp.launch.Launcher;
//using ErrorDialog = VassalSharp.tools.ErrorDialog;
//using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
//using ThrowableUtils = VassalSharp.tools.ThrowableUtils;
//using WarningDialog = VassalSharp.tools.WarningDialog;
//using WriteErrorDialog = VassalSharp.tools.WriteErrorDialog;
//using FileChooser = VassalSharp.tools.filechooser.FileChooser;
//using LogAndSaveFileFilter = VassalSharp.tools.filechooser.LogAndSaveFileFilter;
//using DeobfuscatingInputStream = VassalSharp.tools.io.DeobfuscatingInputStream;
//using FastByteArrayOutputStream = VassalSharp.tools.io.FastByteArrayOutputStream;
//using FileArchive = VassalSharp.tools.io.FileArchive;
//using IOUtils = VassalSharp.tools.io.IOUtils;
//using ObfuscatingOutputStream = VassalSharp.tools.io.ObfuscatingOutputStream;
//using ZipArchive = VassalSharp.tools.io.ZipArchive;
//using MenuManager = VassalSharp.tools.menu.MenuManager;
//using Dialogs = VassalSharp.tools.swing.Dialogs;
namespace VassalSharp.build.module
{
	
	/// <summary> The GameState represents the state of the game currently being played.
	/// Only one game can be open at once.
	/// </summary>
	/// <seealso cref="GameModule.getGameState">
	/// </seealso>
	public class GameState : CommandEncoder
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction:SupportClass.ActionSupport
		{
			private void  InitBlock(GameState enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GameState enclosingInstance;
			public GameState Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction(GameState enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.loadGame();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction1:SupportClass.ActionSupport
		{
			private void  InitBlock(GameState enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GameState enclosingInstance;
			public GameState Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction1(GameState enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.saveGame();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction2:SupportClass.ActionSupport
		{
			private void  InitBlock(GameState enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GameState enclosingInstance;
			public GameState Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction2(GameState enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.saveGameAs();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction3:SupportClass.ActionSupport
		{
			private void  InitBlock(GameState enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GameState enclosingInstance;
			public GameState Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction3(GameState enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.setup(false);
				Enclosing_Instance.setup(true);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction4:SupportClass.ActionSupport
		{
			private void  InitBlock(GameState enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GameState enclosingInstance;
			public GameState Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction4(GameState enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.setup(false);
			}
		}
		////UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassCommandFilter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//private class AnonymousClassCommandFilter:CommandFilter
		//{
		//	public AnonymousClassCommandFilter(GameState enclosingInstance)
		//	{
		//		InitBlock(enclosingInstance);
		//	}
		//	private void  InitBlock(GameState enclosingInstance)
		//	{
		//		this.enclosingInstance = enclosingInstance;
		//	}
		//	private GameState enclosingInstance;
		//	public GameState Enclosing_Instance
		//	{
		//		get
		//		{
		//			return enclosingInstance;
		//		}
				
		//	}
		//	protected internal override bool accept(Command c)
		//	{
		//		return c is BasicLogger.LogCommand;
		//	}
		//}

#if NEVER_DEFINED
		private void  InitBlock()
		{
			return Collections.enumeration(gameComponents);
			return Collections.unmodifiableCollection(gameComponents);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < GameSetupStep > l = new ArrayList < GameSetupStep >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(GameSetupStep step: setupSteps)
			{
				if (!step.isFinished())
				{
					l.add(step);
				}
			}
			return l.iterator();
			return Collections.enumeration(pieces.values());
			return pieces.values();
		}
		/// <returns> true if the game state is different from when it was last saved
		/// </returns>
		virtual public bool Modified
		{
			get
			{
				System.String s = saveString();
				return s != null && !s.Equals(lastSave);
			}
			
			set
			{
				if (value)
				{
					lastSave = null;
				}
				else
				{
					lastSave = saveString();
				}
			}
			
		}
		virtual public bool Updating
		{
			get
			{
				return this.gameUpdating;
			}
			//
			// END FIXME
			//
			
		}
		/// <summary>Return true if a game is currently in progress </summary>
		virtual public bool GameStarted
		{
			get
			{
				return gameStarted;
			}
			
		}
		private System.IO.FileInfo SaveFile
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				FileChooser fc = GameModule.getGameModule().getFileChooser();
				fc.selectDotSavFile();
				fc.addChoosableFileFilter(new LogAndSaveFileFilter());
				
				if (fc.showSaveDialog() != FileChooser.APPROVE_OPTION)
					return null;
				
				System.IO.FileInfo file = fc.SelectedFile;
				if (file.Name.IndexOf('.') == - 1)
					file = new System.IO.FileInfo(file.DirectoryName + "\\" + file.Name + ".vsav");
				
				return file;
			}
			
		}
		/// <returns> a String identifier guaranteed to be different from the
		/// id of all GamePieces in the game
		/// 
		/// </returns>
		/// <seealso cref="GamePiece.getId">
		/// </seealso>
		virtual public System.String NewPieceId
		{
			get
			{
				long time = (System.DateTime.Now.Ticks - 621355968000000000) / 10000;
				System.String id = System.Convert.ToString(time);
				while (pieces.get_Renamed(id) != null)
				{
					id = System.Convert.ToString(++time);
				}
				return id;
			}
			
		}
		/// <summary> Return a {@link Command} that, when executed, will restore the
		/// game to its current state.  Invokes {@link GameComponent#getRestoreCommand}
		/// on each registered {@link GameComponent} 
		/// </summary>
		virtual public Command RestoreCommand
		{
			get
			{
				//UPGRADE_ISSUE: Method 'javax.swing.Action.isEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionisEnabled'"
				if (!saveGame_Renamed_Field.isEnabled())
				{
					return null;
				}
				Command c = new SetupCommand(false);
				c.append(checkVersionCommand());
				c.append(getRestorePiecesCommand());
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(GameComponent gc: gameComponents)
				{
					c.append(gc.RestoreCommand);
				}
				c.append(new SetupCommand(true));
				return c;
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'log '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'log' was moved to static method 'VassalSharp.build.module.GameState'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly org.slf4j.Logger log;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected Map < String, GamePiece > pieces = new HashMap < String, GamePiece >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < GameComponent > gameComponents = new ArrayList < GameComponent >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < GameSetupStep > setupSteps = new ArrayList < GameSetupStep >();
		protected internal SupportClass.ActionSupport loadGame_Renamed_Field, saveGame_Renamed_Field, saveGameAs_Renamed_Field, newGame, closeGame;
		protected internal System.String lastSave;
		protected internal System.IO.FileInfo lastSaveFile = null;
		protected internal DirectoryConfigurer savedGameDirectoryPreference;
		protected internal System.String loadComments;
		
		public GameState()
		{
			InitBlock();
		}
		
		/// <summary> Expects to be added to a GameModule.  Adds <code>New</code>,
		/// <code>Load</code>, <code>Close</code>, and <code>Save</code>
		/// entries to the <code>File</code> menu of the controls window
		/// </summary>
		public virtual void  addTo(GameModule mod)
		{
			loadGame_Renamed_Field = new AnonymousClassAbstractAction(this, Resources.getString("GameState.load_game"));
			// FIMXE: setting nmemonic from first letter could cause collisions in
			// some languages
			//UPGRADE_ISSUE: Field 'javax.swing.Action.MNEMONIC_KEY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionMNEMONIC_KEY_f'"
			loadGame_Renamed_Field.putValue(Action.MNEMONIC_KEY, (int) Resources.getString("GameState.load_game.shortcut")[0]);
			
			saveGame_Renamed_Field = new AnonymousClassAbstractAction1(this, Resources.getString("GameState.save_game"));
			// FIMXE: setting nmemonic from first letter could cause collisions in
			// some languages
			//UPGRADE_ISSUE: Field 'javax.swing.Action.MNEMONIC_KEY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionMNEMONIC_KEY_f'"
			saveGame_Renamed_Field.putValue(Action.MNEMONIC_KEY, (int) Resources.getString("GameState.save_game.shortcut")[0]);
			
			saveGameAs_Renamed_Field = new AnonymousClassAbstractAction2(this, Resources.getString("GameState.save_game_as"));
			// FIMXE: setting nmemonic from first letter could cause collisions in
			// some languages
			//UPGRADE_ISSUE: Field 'javax.swing.Action.MNEMONIC_KEY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionMNEMONIC_KEY_f'"
			saveGameAs_Renamed_Field.putValue(Action.MNEMONIC_KEY, (int) Resources.getString("GameState.save_game_as.shortcut")[0]);
			
			newGame = new AnonymousClassAbstractAction3(this, Resources.getString("GameState.new_game"));
			// FIMXE: setting nmemonic from first letter could cause collisions in
			// some languages
			//UPGRADE_ISSUE: Field 'javax.swing.Action.MNEMONIC_KEY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionMNEMONIC_KEY_f'"
			newGame.putValue(Action.MNEMONIC_KEY, (int) Resources.getString("GameState.new_game.shortcut")[0]);
			
			closeGame = new AnonymousClassAbstractAction4(this, Resources.getString("GameState.close_game"));
			// FIMXE: setting nmemonic from first letter could cause collisions in
			// some languages
			//UPGRADE_ISSUE: Field 'javax.swing.Action.MNEMONIC_KEY' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionMNEMONIC_KEY_f'"
			closeGame.putValue(Action.MNEMONIC_KEY, (int) Resources.getString("GameState.close_game.shortcut")[0]);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'mm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			MenuManager mm = MenuManager.Instance;
			mm.addAction("GameState.new_game", newGame);
			mm.addAction("GameState.load_game", loadGame_Renamed_Field);
			mm.addAction("GameState.save_game", saveGame_Renamed_Field);
			mm.addAction("GameState.save_game_as", saveGameAs_Renamed_Field);
			mm.addAction("GameState.close_game", closeGame);
			
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			saveGame_Renamed_Field.setEnabled(gameStarting);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			saveGameAs_Renamed_Field.setEnabled(gameStarting);
			//UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			closeGame.setEnabled(gameStarting);
		}
		
		/// <summary> Add a {@link GameComponent} to the list of objects that will
		/// be notified when a game is started/ended
		/// </summary>
		public virtual void  addGameComponent(GameComponent theComponent)
		{
			gameComponents.add(theComponent);
		}
		
		/// <summary> Remove a {@link GameComponent} from the list of objects that will
		/// be notified when a game is started/ended
		/// </summary>
		public virtual void  removeGameComponent(GameComponent theComponent)
		{
			gameComponents.remove(theComponent);
		}
		
		/// <returns> an enumeration of all {@link GameComponent} objects
		/// that have been added to this GameState
		/// </returns>
		/// <deprecated> Use {@link #getGameComponents()} instead.
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Enumeration < GameComponent > getGameComponentsEnum()
		
		/// <returns> a Collection of all {@link GameComponent} objects
		/// that have been added to this GameState
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Collection < GameComponent > getGameComponents()
		
		/// <summary>Add a {@link GameSetupStep} </summary>
		public virtual void  addGameSetupStep(GameSetupStep step)
		{
			setupSteps.add(step);
		}
		
		/// <summary>Remove a {@link GameSetupStep} </summary>
		public virtual void  removeGameSetupStep(GameSetupStep step)
		{
			setupSteps.remove(step);
		}
		
		/// <returns> an iterator of all {@link GameSetupStep}s that are not
		/// yet finished
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < GameSetupStep > getUnfinishedSetupSteps()
		
		/* Using an instance variable allows us to shut down in the
		middle of a startup. */
		private bool gameStarting = false;
		private bool gameStarted = false;
		
		//
		// FIXME: This will become unnecessary when we do model-view separation.
		//
		private volatile bool gameUpdating = false;
		
		/// <summary> Start a game for updating (via editor).
		/// <em>NOTE: This method is not for use in custom code.</em>
		/// </summary>
		public virtual void  setup(bool gameStarting, bool gameUpdating)
		{
			this.gameUpdating = gameUpdating;
			setup(gameStarting);
		}
		
		/// <summary> Indicated game update is completed and game is saved.</summary>
		public virtual void  updateDone()
		{
			this.gameUpdating = false;
		}
#endif		
		/// <summary> Start/end a game.  Prompt to save if the game state has been
		/// modified since last save.  Invoke {@link GameComponent#setup}
		/// on all registered {@link GameComponent} objects.
		/// </summary>
		public virtual void  setup(bool gameStarting)
		{
			//if (!gameStarting && gameStarted && Modified)
			//{
			//	switch (JOptionPane.showConfirmDialog(GameModule.getGameModule().getFrame(), Resources.getString("GameState.save_game_query"), Resources.getString("GameState.game_modified"), (int) System.Windows.Forms.MessageBoxButtons.YesNoCancel))
			//	{
					
			//		//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.YES_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//		case (int) System.Windows.Forms.DialogResult.Yes: 
			//			saveGame();
			//			break;
					
			//		//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.CANCEL_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//		case (int) System.Windows.Forms.DialogResult.Cancel: 
			//		//UPGRADE_ISSUE: Field 'javax.swing.JOptionPane.CLOSED_OPTION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
			//		case JOptionPane.CLOSED_OPTION: 
			//			return ;
			//		}
			//}
			
			//this.gameStarting = gameStarting;
			//if (!gameStarting)
			//{
			//	pieces.clear();
			//}
			
			////UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			//newGame.setEnabled(!gameStarting);
			////UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			//saveGame_Renamed_Field.setEnabled(gameStarting);
			////UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			//saveGameAs_Renamed_Field.setEnabled(gameStarting);
			////UPGRADE_ISSUE: Method 'javax.swing.Action.setEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionsetEnabled_boolean'"
			//closeGame.setEnabled(gameStarting);
			
			//if (gameStarting)
			//{
			//	//UPGRADE_ISSUE: Method 'javax.swing.Action.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionputValue_javalangString_javalangObject'"
			//	//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
			//	loadGame_Renamed_Field.putValue(Action.NAME, Resources.getString("GameState.load_continuation"));
			//	GameModule.getGameModule().getWizardSupport().showGameSetupWizard();
			//}
			//else
			//{
			//	//UPGRADE_ISSUE: Method 'javax.swing.Action.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionputValue_javalangString_javalangObject'"
			//	//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
			//	loadGame_Renamed_Field.putValue(Action.NAME, Resources.getString("GameState.load_game"));
			//	GameModule.getGameModule().appendToTitle(null);
			//}
			
			//gameStarted &= this.gameStarting;
			////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//for(GameComponent gc: gameComponents)
			//{
			//	gc.setup(this.gameStarting);
			//}
			
			//gameStarted |= this.gameStarting;
			//lastSave = gameStarting?saveString():null;
		}

		/// <summary> Read the game from a savefile.  The contents of the file is
		/// sent to {@link GameModule#decode} and translated into a
		/// {@link Command}, which is then executed.  The command read from the
		/// file should be that returned by {@link #getRestoreCommand}.
		/// </summary>
		public virtual void  loadGame()
		{
			////UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//GameModule g = GameModule.getGameModule();
			
			//loadComments = "";
			////UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//FileChooser fc = g.getFileChooser();
			//fc.addChoosableFileFilter(new LogAndSaveFileFilter());
			
			//if (fc.showOpenDialog() != FileChooser.APPROVE_OPTION)
			//	return ;
			
			////UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//System.IO.FileInfo f = fc.SelectedFile;
			//try
			//{
			//	bool tmpBool;
			//	if (System.IO.File.Exists(f.FullName))
			//		tmpBool = true;
			//	else
			//		tmpBool = System.IO.Directory.Exists(f.FullName);
			//	if (!tmpBool)
			//		throw new System.IO.FileNotFoundException("Unable to locate " + f.FullName);
				
			//	// Check the Save game for validity
			//	//UPGRADE_NOTE: Final was removed from the declaration of 'metaData '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//	AbstractMetaData metaData = MetaDataFactory.buildMetaData(f);
			//	if (metaData == null || !(metaData is SaveMetaData))
			//	{
			//		WarningDialog.show("GameState.invalid_save_file", f.FullName);
			//		return ;
			//	}
				
			//	// Check it belongs to this module and matches the version if is a
			//	// post 3.0 save file
			//	//UPGRADE_NOTE: Final was removed from the declaration of 'saveData '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//	SaveMetaData saveData = (SaveMetaData) metaData;
			//	System.String saveModuleVersion = "?";
			//	if (saveData.ModuleData != null)
			//	{
			//		loadComments = saveData.LocalizedDescription;
			//		//UPGRADE_NOTE: Final was removed from the declaration of 'saveModuleName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//		System.String saveModuleName = saveData.ModuleName;
			//		saveModuleVersion = saveData.ModuleVersion;
			//		//UPGRADE_NOTE: Final was removed from the declaration of 'moduleName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//		System.String moduleName = g.getGameName();
			//		//UPGRADE_NOTE: Final was removed from the declaration of 'moduleVersion '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//		System.String moduleVersion = g.getGameVersion();
			//		System.String message = null;
					
			//		if (!saveModuleName.Equals(moduleName))
			//		{
			//			message = Resources.getString("GameState.load_module_mismatch", f.Name, saveModuleName, moduleName);
			//		}
			//		else if (!saveModuleVersion.Equals(moduleVersion))
			//		{
			//			message = Resources.getString("GameState.load_version_mismatch", f.Name, saveModuleVersion, moduleVersion);
			//		}
					
			//		if (message != null)
			//		{
			//			//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.YES_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			//			if (SupportClass.OptionPaneSupport.ShowConfirmDialog(null, message, Resources.getString("GameState.load_mismatch"), (int) System.Windows.Forms.MessageBoxButtons.YesNo, (int) System.Windows.Forms.MessageBoxIcon.Question) != (int) System.Windows.Forms.DialogResult.Yes)
			//			{
			//				g.warn(Resources.getString("GameState.cancel_load", f.Name));
			//				return ;
			//			}
			//		}
			//	}
				
			//	log.info("Loading save game " + f.FullName + ", created with module version " + saveModuleVersion);
				
			//	if (gameStarted)
			//	{
			//		loadContinuation(f);
			//	}
			//	else
			//	{
			//		loadGameInBackground(f);
			//	}
				
			//	lastSaveFile = f;
			//}
			//catch (System.IO.IOException e)
			//{
			//	ReadErrorDialog.error(e, f);
			//}
			///*
			//String msg = Resources.getString("GameState.unable_to_load", f.getName());  //$NON-NLS-1$
			//if (e.getMessage() != null) {
			//msg += "\n" + e.getMessage();  //$NON-NLS-1$
			//}
			//JOptionPane.showMessageDialog(GameModule.getGameModule().getFrame(),
			//msg, Resources.getString("GameState.load_error"), JOptionPane.ERROR_MESSAGE);  //$NON-NLS-1$
			//}
			//else {
			//// FIXME: give more specific error message
			//// FIXME: maybe deprecate warn()?
			//GameModule.getGameModule().warn(Resources.getString("GameState.unable_to_find", f.getPath()));  //$NON-NLS-1$
			//}*/
		}

#if NEVER_DEFINED
		protected internal virtual System.String saveString()
		{
			return GameModule.getGameModule().encode(RestoreCommand);
		}
		
		protected internal virtual bool checkForOldSaveFile(System.IO.FileInfo f)
		{
			bool tmpBool;
			if (System.IO.File.Exists(f.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(f.FullName);
			if (tmpBool)
			{
				// warn user if overwriting a save from an old version
				//UPGRADE_NOTE: Final was removed from the declaration of 'md '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				AbstractMetaData md = MetaDataFactory.buildMetaData(f);
				if (md != null && md is SaveMetaData)
				{
					if (Info.hasOldFormat(md.VassalVersion))
					{
						//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.CANCEL_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						return Dialogs.showConfirmDialog(GameModule.getGameModule().getFrame(), Resources.getString("Warning.save_will_be_updated_title"), Resources.getString("Warning.save_will_be_updated_heading"), Resources.getString("Warning.save_will_be_updated_message", f.FullName, "3.2"), (int) System.Windows.Forms.MessageBoxIcon.Exclamation, (int) System.Windows.Forms.MessageBoxButtons.OKCancel) != (int) System.Windows.Forms.DialogResult.Cancel;
					}
				}
			}
			
			return true;
		}
#endif
        /// <summary>Saves the game to an existing file, or prompts for a new one. </summary>
        public virtual void  saveGame()
		{
			////UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//GameModule g = GameModule.getGameModule();
			
			//g.warn(Resources.getString("GameState.saving_game")); //$NON-NLS-1$
			
			//if (lastSaveFile != null)
			//{
			//	if (!checkForOldSaveFile(lastSaveFile))
			//	{
			//		return ;
			//	}
				
			//	try
			//	{
			//		saveGame(lastSaveFile);
			//		g.warn(Resources.getString("GameState.game_saved")); //$NON-NLS-1$
			//	}
			//	catch (System.IO.IOException e)
			//	{
			//		WriteErrorDialog.error(e, lastSaveFile);
			//		/*
			//		Logger.log(err);
			//		GameModule.getGameModule().warn(Resources.getString("GameState.save_failed"));  //$NON-NLS-1$*/
			//	}
			//}
			//else
			//{
			//	saveGameAs();
			//}
		}
		
		/// <summary>Prompts the user for a file into which to save the game </summary>
		public virtual void  saveGameAs()
		{
			////UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//GameModule g = GameModule.getGameModule();
			
			//g.warn(Resources.getString("GameState.saving_game")); //$NON-NLS-1$
			
			////UPGRADE_NOTE: Final was removed from the declaration of 'saveFile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//System.IO.FileInfo saveFile = SaveFile;
			//if (saveFile == null)
			//{
			//	g.warn(Resources.getString("GameState.save_canceled")); //$NON-NLS-1$
			//}
			//else
			//{
			//	if (!checkForOldSaveFile(saveFile))
			//	{
			//		return ;
			//	}
				
			//	try
			//	{
			//		saveGame(saveFile);
			//		lastSaveFile = saveFile;
			//		g.warn(Resources.getString("GameState.game_saved")); //$NON-NLS-1$
			//	}
			//	catch (System.IO.IOException e)
			//	{
			//		WriteErrorDialog.error(e, saveFile);
			//		/*
			//		Logger.log(err);
			//		GameModule.getGameModule().warn(Resources.getString("GameState.save_failed"));  //$NON-NLS-1$*/
			//	}
			//}
		}
#if NEVER_DEFINED		
		/// <summary>  Add a {@link GamePiece} to the current game.
		/// The GameState keeps track of all GamePieces in the system,
		/// regardless of which {@link Map} they belong to (if any)
		/// </summary>
		public virtual void  addPiece(GamePiece p)
		{
			if (p.Id == null)
			{
				p.Id = NewPieceId;
			}
			pieces.put(p.Id, p);
		}
		
		/// <returns> the {@link GamePiece} in the current game with the given id
		/// </returns>
		public virtual GamePiece getPieceForId(System.String id)
		{
			return id == null?null:pieces.get_Renamed(id);
		}
		
		/// <summary>  Remove a {@link GamePiece} from the current game</summary>
		public virtual void  removePiece(System.String id)
		{
			if (id != null)
			{
				pieces.remove(id);
			}
		}
		
		public virtual void  loadContinuation(System.IO.FileInfo f)
		{
			GameModule.getGameModule().warn(Resources.getString("GameState.loading", f.Name)); //$NON-NLS-1$
			Command c = decodeSavedGame(f);
			CommandFilter filter = new AnonymousClassCommandFilter(this);
			c = filter.apply(c);
			if (c != null)
			{
				c.execute();
			}
			System.String msg = Resources.getString("GameState.loaded", f.Name); //$NON-NLS-1$
			if (loadComments != null && loadComments.Length > 0)
			{
				msg += (": " + loadComments);
			}
			GameModule.getGameModule().warn(msg);
		}
		
		/// <returns> an Enumeration of all {@link GamePiece}s in the game
		/// </returns>
		/// <deprecated> Use {@link #getAllPieces()} instead.
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Enumeration < GamePiece > getPieces()
		
		/// <returns> a Collection of all {@link GamePiece}s in the game 
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Collection < GamePiece > getAllPieces()
		
		public class SetupCommand:Command
		{
			virtual public bool GameStarting
			{
				get
				{
					return gameStarting;
				}
				
			}
			private bool gameStarting;
			
			public SetupCommand(bool gameStarting)
			{
				this.gameStarting = gameStarting;
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override void  executeCommand()
			{
				GameModule.getGameModule().getGameState().setup(gameStarting);
			}
			
			//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
			public override Command myUndoCommand()
			{
				return null;
			}
		}
		
		public const System.String SAVEFILE_ZIP_ENTRY = "savedGame"; //$NON-NLS-1$
		
		private Command checkVersionCommand()
		{
			System.String runningVersion = GameModule.getGameModule().getAttributeValueString(GameModule.VASSAL_VERSION_RUNNING);
			ConditionalCommand.Condition cond = new ConditionalCommand.Lt(GameModule.VASSAL_VERSION_RUNNING, runningVersion);
			Command c = new ConditionalCommand(new ConditionalCommand.Condition[]{cond}, new AlertCommand(Resources.getString("GameState.version_mismatch", runningVersion))); //$NON-NLS-1$
			System.String moduleName = GameModule.getGameModule().getAttributeValueString(GameModule.MODULE_NAME);
			System.String moduleVersion = GameModule.getGameModule().getAttributeValueString(GameModule.MODULE_VERSION);
			cond = new ConditionalCommand.Lt(GameModule.MODULE_VERSION, moduleVersion);
			c.append(new ConditionalCommand(new ConditionalCommand.Condition[]{cond}, new AlertCommand(Resources.getString("GameState.version_mismatch2", moduleName, moduleVersion)))); //$NON-NLS-1$
			return c;
		}
#endif 		
		/// <summary> A GameState recognizes instances of {@link SetupCommand}</summary>
		public virtual System.String encode(Command c)
		{
            //if (c is SetupCommand)
            //{
            //	return ((SetupCommand) c).GameStarting?END_SAVE:BEGIN_SAVE;
            //}
            //else
            //{
            //	return null;
            //}
            return null;
		}
		
		/// <summary> A GameState recognizes instances of {@link SetupCommand}</summary>
		public virtual Command decode(System.String theCommand)
		{
            //if (BEGIN_SAVE.Equals(theCommand))
            //{
            //	return new SetupCommand(false);
            //}
            //else if (END_SAVE.Equals(theCommand))
            //{
            //	return new SetupCommand(true);
            //}
            //else
            //{
            //	return null;
            //}
            return null;
		}
		public const System.String BEGIN_SAVE = "begin_save"; //$NON-NLS-1$
		public const System.String END_SAVE = "end_save"; //$NON-NLS-1$
		
		public virtual void  saveGame(System.IO.FileInfo f)
		{
			//// FIXME: Extremely inefficient! Write directly to ZipArchive OutputStream
			////UPGRADE_NOTE: Final was removed from the declaration of 'save '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//System.String save = saveString();
			////UPGRADE_NOTE: Final was removed from the declaration of 'ba '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//FastByteArrayOutputStream ba = new FastByteArrayOutputStream();
			//System.IO.Stream out_Renamed = null;
			//try
			//{
			//	out_Renamed = new ObfuscatingOutputStream(ba);
			//	//UPGRADE_TODO: Method 'java.lang.String.getBytes' was converted to 'System.Text.Encoding.GetEncoding(string).GetBytes(string)' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangStringgetBytes_javalangString'"
			//	sbyte[] temp_sbyteArray;
			//	temp_sbyteArray = SupportClass.ToSByteArray(System.Text.Encoding.GetEncoding("UTF-8").GetBytes(save));
			//	out_Renamed.Write(SupportClass.ToByteArray(temp_sbyteArray), 0, temp_sbyteArray.Length);
			//	out_Renamed.Close();
			//}
			//finally
			//{
			//	IOUtils.closeQuietly(out_Renamed);
			//}
			
			//FileArchive archive = null;
			//try
			//{
			//	archive = new ZipArchive(f);
			//	archive.add(SAVEFILE_ZIP_ENTRY, ba.toInputStream());
			//	(new SaveMetaData()).save(archive);
			//	archive.close();
			//}
			//finally
			//{
			//	IOUtils.closeQuietly(archive);
			//}
			
			//Launcher.Instance.sendSaveCmd(f);
			
			//Modified = false;
		}
#if NEVER_DEFINED		
		public virtual void  loadGameInBackground(System.IO.FileInfo f)
		{
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
				loadGameInBackground(f.Name, new System.IO.BufferedStream(new System.IO.FileStream(f.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read)));
			}
			catch (System.IO.IOException e)
			{
				ReadErrorDialog.error(e, f);
			}
		}
		
		public virtual void  loadGameInBackground(System.String shortName, System.IO.Stream in_Renamed)
		{
			GameModule.getGameModule().warn(Resources.getString("GameState.loading", shortName)); //$NON-NLS-1$
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'frame '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Form frame = GameModule.getGameModule().getFrame();
			//UPGRADE_ISSUE: Member 'java.awt.Cursor.getPredefinedCursor' was converted to 'System.Windows.Forms.Cursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
			//UPGRADE_ISSUE: Member 'java.awt.Cursor.WAIT_CURSOR' was converted to 'System.Windows.Forms.Cursors.WaitCursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
			frame.Cursor = System.Windows.Forms.Cursors.WaitCursor;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			new SwingWorker < Command, Void >()
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				Override
			}
		}
		public virtual Command doInBackground()
		{
			try
			{
				return decodeSavedGame(in_Renamed);
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal virtual void  done()
		{
			try
			{
				Command loadCommand = null;
				System.String msg = null;
				try
				{
					loadCommand = get_Renamed();
					
					if (loadCommand != null)
					{
						msg = Resources.getString("GameState.loaded", shortName); //$NON-NLS-1$
						if (loadComments != null && loadComments.Length > 0)
						{
							msg += (": " + loadComments);
						}
					}
					else
					{
						msg = Resources.getString("GameState.invalid_savefile", shortName); //$NON-NLS-1$
					}
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
					ErrorDialog.bug(e);
				}
				// FIXME: review error message
				catch (ExecutionException e)
				{
					// FIXME: This is a temporary hack to catch OutOfMemoryErrors; there should
					// be a better, more uniform and more permanent way of handling these, since
					// an OOME is neither a VASSAL bug, a module bug, nor due to bad data.
					//UPGRADE_NOTE: Final was removed from the declaration of 'oom '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.OutOfMemoryException oom = ThrowableUtils.getAncestor(typeof(System.OutOfMemoryException), e);
					if (oom != null)
					{
						ErrorDialog.bug(e);
					}
					else
					{
						log.error("", e);
					}
					msg = Resources.getString("GameState.error_loading", shortName);
				}
				
				if (loadCommand != null)
				{
					loadCommand.execute();
				}
				
				GameModule.getGameModule().warn(msg);
				Logger logger = GameModule.getGameModule().getLogger();
				if (logger is BasicLogger)
				{
					((BasicLogger) logger).queryNewLogFile(true);
				}
			}
			finally
			{
				//UPGRADE_ISSUE: Member 'java.awt.Cursor.getPredefinedCursor' was converted to 'System.Windows.Forms.Cursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
				//UPGRADE_ISSUE: Member 'java.awt.Cursor.DEFAULT_CURSOR' was converted to 'System.Windows.Forms.Cursors.Default' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
				frame.setCursor(System.Windows.Forms.Cursors.Default);
			}
		}
		static GameState()
		{
			log = LoggerFactory.getLogger(typeof(GameState));
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	.execute();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <returns> a Command that, when executed, will add all pieces currently
	/// in the game. Used when saving a game.
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Command getRestorePiecesCommand()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// TODO remove stacks that were empty when the game was loaded and are still empty now
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final List < GamePiece > pieceList = new ArrayList < GamePiece >(pieces.values());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Collections.sort(pieceList, new Comparator < GamePiece >()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private final Map < GamePiece, Integer > indices = new HashMap < GamePiece, Integer >();
	
	// Cache indices because indexOf() is linear;
	// otherwise sorting would be quadratic.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private int indexOf(GamePiece p, VassalSharp.build.module.Map m)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		Integer pi = indices.get(p);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(pi == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		indices.put(p, pi = m.getPieceCollection().indexOf(p));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return pi;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public int compare(GamePiece a, GamePiece b)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final VassalSharp.build.module.Map amap = a.getMap(), bmap = b.getMap();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(amap == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return bmap == null ?
	// order by id if neither piece is on a map
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	a.getId().compareTo(b.getId()):
	// nonnull map sorts before null map
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	- 1;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(bmap == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// null map sorts after nonnull map
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return 1;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(amap == bmap)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// same map, sort according to piece list
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return indexOf(a, amap) - indexOf(b, bmap);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// different maps, order by map
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return amap.getId().compareTo(bmap.getId());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	});
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final Command c = new NullCommand();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(GamePiece p: pieceList)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		c.append(new AddPiece(p));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return c;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Read a saved game and translate it into a Command.  Executing the
	/// command will load the saved game.
	/// 
	/// </summary>
	/// <param name="fileName">
	/// </param>
	/// <returns>
	/// </returns>
	/// <throws>  IOException </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Command decodeSavedGame(File saveFile) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return decodeSavedGame(
		new BufferedInputStream(new FileInputStream(saveFile)));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Command decodeSavedGame(InputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ZipInputStream zipInput = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		zipInput = new ZipInputStream(in);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(ZipEntry entry = zipInput.getNextEntry();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	entry != null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	entry = zipInput.getNextEntry())
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(SAVEFILE_ZIP_ENTRY.equals(entry.getName()))
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		InputStream din = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		din = new DeobfuscatingInputStream(zipInput);
	// FIXME: ToString() is very inefficient, make decode() use the stream directly
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final Command c = GameModule.getGameModule().decode(
	IOUtils.ToString(din, UTF-8));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	din.close();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return c;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	finally
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		IOUtils.closeQuietly(din);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	zipInput.close();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	finally
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		IOUtils.closeQuietly(zipInput);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	// FIXME: give more specific error message
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	throw new IOException(Invalid saveFile format);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public DirectoryConfigurer getSavedGameDirectoryPreference()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(savedGameDirectoryPreference == null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		savedGameDirectoryPreference = new DirectoryConfigurer(savedGameDir, null);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	GameModule.getGameModule().getPrefs().addOption(null, savedGameDirectoryPreference);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	return savedGameDirectoryPreference;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
    #endif
	}
}