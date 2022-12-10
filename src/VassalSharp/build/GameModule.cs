/*
* $Id$
*
* Copyright (c) 2000-2012 by Rodney Kinney, Brent Easton
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

using Microsoft.Extensions.Logging;

using System;
using VassalSharp.command;
using VassalSharp.build.module.properties;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using static SupportClass;
using VassalSharp.tools;

#if NEVER_DEFINED
//UPGRADE_TODO: The type 'org.apache.commons.codec.digest.DigestUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DigestUtils = org.apache.commons.codec.digest.DigestUtils;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using Info = VassalSharp.Info;
using BasicCommandEncoder = VassalSharp.build.module.BasicCommandEncoder;
using ChartWindow = VassalSharp.build.module.ChartWindow;
using Chatter = VassalSharp.build.module.Chatter;
using DiceButton = VassalSharp.build.module.DiceButton;
using DoActionButton = VassalSharp.build.module.DoActionButton;
using Documentation = VassalSharp.build.module.Documentation;
using GameState = VassalSharp.build.module.GameState;
using GlobalKeyCommand = VassalSharp.build.module.GlobalKeyCommand;
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using Inventory = VassalSharp.build.module.Inventory;
using Map = VassalSharp.build.module.Map;
using ModuleExtension = VassalSharp.build.module.ModuleExtension;
using MultiActionButton = VassalSharp.build.module.MultiActionButton;
using NotesWindow = VassalSharp.build.module.NotesWindow;
using PieceWindow = VassalSharp.build.module.PieceWindow;
using PlayerHand = VassalSharp.build.module.PlayerHand;
using PlayerRoster = VassalSharp.build.module.PlayerRoster;
using Plugin = VassalSharp.build.module.Plugin;
using PredefinedSetup = VassalSharp.build.module.PredefinedSetup;
using PrivateMap = VassalSharp.build.module.PrivateMap;
using PrototypesContainer = VassalSharp.build.module.PrototypesContainer;
using RandomTextButton = VassalSharp.build.module.RandomTextButton;
using ServerConnection = VassalSharp.build.module.ServerConnection;
using SpecialDiceButton = VassalSharp.build.module.SpecialDiceButton;
using StartupGlobalKeyCommand = VassalSharp.build.module.StartupGlobalKeyCommand;
using ToolbarMenu = VassalSharp.build.module.ToolbarMenu;
using WizardSupport = VassalSharp.build.module.WizardSupport;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using ModuleMetaData = VassalSharp.build.module.metadata.ModuleMetaData;
using ChangePropertyCommandEncoder = VassalSharp.build.module.properties.ChangePropertyCommandEncoder;
using MutablePropertiesContainer = VassalSharp.build.module.properties.MutablePropertiesContainer;
using MutableProperty = VassalSharp.build.module.properties.MutableProperty;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using TurnTracker = VassalSharp.build.module.turn.TurnTracker;
using PieceSlot = VassalSharp.build.widget.PieceSlot;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using Logger = VassalSharp.command.Logger;
using NullCommand = VassalSharp.command.NullCommand;
using CompoundValidityChecker = VassalSharp.configure.CompoundValidityChecker;
using MandatoryComponent = VassalSharp.configure.MandatoryComponent;
using GamePiece = VassalSharp.counters.GamePiece;
using ComponentI18nData = VassalSharp.i18n.ComponentI18nData;
using Localization = VassalSharp.i18n.Localization;
using Resources = VassalSharp.i18n.Resources;
using PlayerWindow = VassalSharp.launch.PlayerWindow;
using Prefs = VassalSharp.preferences.Prefs;
using ArchiveWriter = VassalSharp.tools.ArchiveWriter;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
using CRCUtils = VassalSharp.tools.CRCUtils;
using DataArchive = VassalSharp.tools.DataArchive;
using KeyStrokeListener = VassalSharp.tools.KeyStrokeListener;
using KeyStrokeSource = VassalSharp.tools.KeyStrokeSource;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
using ToolBarComponent = VassalSharp.tools.ToolBarComponent;
using WarningDialog = VassalSharp.tools.WarningDialog;
using WriteErrorDialog = VassalSharp.tools.WriteErrorDialog;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
using ImageTileSource = VassalSharp.tools.image.ImageTileSource;
using ImageTileDiskCache = VassalSharp.tools.image.tilecache.ImageTileDiskCache;
using IOUtils = VassalSharp.tools.io.IOUtils;
#else
//using BasicCommandEncoder = VassalSharp.build.module.BasicCommandEncoder;
//using Chatter = VassalSharp.build.module.Chatter;
//using DoActionButton = VassalSharp.build.module.DoActionButton;
//using GameState = VassalSharp.build.module.GameState;
//using Map = VassalSharp.build.module.Map;
//using ModuleExtension = VassalSharp.build.module.ModuleExtension;
//using PlayerRoster = VassalSharp.build.module.PlayerRoster;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using ModuleMetaData = VassalSharp.build.module.metadata.ModuleMetaData;
using MutableProperty = VassalSharp.build.module.properties.MutableProperty;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using MutablePropertiesContainer = VassalSharp.build.module.properties.MutablePropertiesContainer;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
//using GamePiece = VassalSharp.counters.GamePiece;
using ComponentI18nData = VassalSharp.i18n.ComponentI18nData;
using Localization = VassalSharp.i18n.Localization;
using Resources = VassalSharp.i18n.Resources;
//using PlayerWindow = VassalSharp.launch.PlayerWindow;
//using Plugin = VassalSharp.build.module.Plugin;
using Prefs = VassalSharp.preferences.Prefs;
using ArchiveWriter = VassalSharp.tools.ArchiveWriter;
//using ArrayUtils = VassalSharp.tools.ArrayUtils;
using DataArchive = VassalSharp.tools.DataArchive;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
//using ToolBarComponent = VassalSharp.tools.ToolBarComponent;
//using WizardSupport = VassalSharp.build.module.WizardSupport;
//using WarningDialog = VassalSharp.tools.WarningDialog;
//using WriteErrorDialog = VassalSharp.tools.WriteErrorDialog;
//using FileChooser = VassalSharp.tools.filechooser.FileChooser;
using ImageTileSource = VassalSharp.tools.image.ImageTileSource;
//using ImageTileDiskCache = VassalSharp.tools.image.tilecache.ImageTileDiskCache;
using IOUtils = VassalSharp.tools.io.IOUtils;
#endif

namespace VassalSharp.build
{

#if NEVER_DEFINED
	/// <summary> The GameModule class is the base class for a VASSAL module.  It is
	/// the root of the {@link Buildable} containment hierarchy.
	/// Components which are added directly to the GameModule are contained
	/// in the <code>VassalSharp.build.module</code> package.
	/// 
	/// <p>It is a singleton, and contains access points for many other classes,
	/// such as {@link DataArchive}, {@link ServerConnection}, {@link Logger},
	/// and {@link Prefs}.</p>
	/// </summary>
	public abstract class GameModule : AbstractConfigurable, CommandEncoder, ToolBarComponent, PropertySource, MutablePropertiesContainer, GpIdSupport
	{

		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		internal class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(GameModule enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(GameModule enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GameModule enclosingInstance;
			public GameModule Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
			
				for(Map map: Map.getMapList())
				{
					map.repaint();
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(GameModule enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(GameModule enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GameModule enclosingInstance;
			public GameModule Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs e)
			{
				e.Cancel = true;
				quit();
			}
		}
		private void  InitBlock()
		{
			propsContainer = new Impl();
			repaintOnPropertyChange = new AnonymousClassPropertyChangeListener(this);
			controlPanel = frame.ControlPanel;
			return ;
		
			new Class < ? > []
			{
			
				String.
			}
		}

		/// <returns> the top-level frame of the controls window
		/// </returns>
		virtual public System.Windows.Forms.Form Frame
		{
			get
			{
				return frame;
			}

		}
		virtual public WizardSupport WizardSupport
		{
			get
			{
				if (wizardSupport == null)
				{
					wizardSupport = new WizardSupport();
				}
				return wizardSupport;
			}

		}
		public static string ConfigureTypeName
		{
			get
			{
				return Resources.getString("Editor.GameModule.component_type"); //$NON-NLS-1$
			}

		}
		override public string[] AttributeNames
		{
			get
			{
				return new string[] { MODULE_NAME, MODULE_VERSION, DESCRIPTION, VASSAL_VERSION_CREATED, NEXT_PIECESLOT_ID };
			}

		}
		override public string[] AttributeDescriptions
		{
			get
			{
				return new string[] { Resources.getString("Editor.GameModule.name_label"), Resources.getString("Editor.GameModule.version_label"), Resources.getString("Editor.GameModule.description") };
			}

		}
		public abstract string MutablePropertiesContainerId { get; }
		public abstract System.Windows.Forms.ToolBar ToolBar { get; }
		public abstract int NextGpId { get; set; }
		//UPGRADE_NOTE: Final was removed from the declaration of 'log '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'log' was moved to static method 'VassalSharp.build.GameModule'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly ILogger log;

		protected internal const string DEFAULT_NAME = "Unnamed module"; //$NON-NLS-1$
		public const string MODULE_NAME = "name"; //$NON-NLS-1$
		public const string MODULE_VERSION = "version"; //$NON-NLS-1$
		public const string DESCRIPTION = "description";
		public const string VASSAL_VERSION_CREATED = "VassalVersion"; //$NON-NLS-1$
		/// <summary>The System property of this name will return a version identifier for the version of VASSAL being run </summary>
		public const string VASSAL_VERSION_RUNNING = "runningVassalVersion"; //$NON-NLS-1$
		public const string NEXT_PIECESLOT_ID = "nextPieceSlotId";
		public const string BUILDFILE = "buildFile";
		private static GameModule theModule;
		protected internal string moduleVersion = "0.0"; //$NON-NLS-1$
		protected internal string vassalVersionCreated = "0.0"; //$NON-NLS-1$
		protected internal string gameName = DEFAULT_NAME;
		protected internal string localizedGameName = null;
		protected internal string description = "";
		protected internal string lastSavedConfiguration;
		protected internal FileChooser fileChooser;
		protected internal System.Windows.Forms.FileDialog fileDialog;
		//UPGRADE_NOTE: The initialization of  'propsContainer' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal MutablePropertiesContainer propsContainer;
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		//UPGRADE_NOTE: The initialization of  'repaintOnPropertyChange' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal PropertyChangeEventHandler repaintOnPropertyChange;

		protected internal PlayerWindow frame = new PlayerWindow();
		//UPGRADE_NOTE: The initialization of  'controlPanel' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal System.Windows.Forms.Panel controlPanel;

		protected internal GameState theState;
		protected internal DataArchive archive;
		protected internal Prefs preferences;
		protected internal ILogger logger;

		protected internal Chatter chat;
		protected internal System.Random RNG = new SupportClass.SecureRandomSupport();
		protected internal ServerConnection server;
		
		protected internal ImageTileSource tcache;
		

		protected internal WizardSupport wizardSupport;

		//UPGRADE_ISSUE: Class 'java.beans.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
		protected internal PropertyChangeSupport idChangeSupport;
		
	
		protected List < KeyStrokeSource > keyStrokeSources = 
		new ArrayList < KeyStrokeSource >();
	
		protected List < KeyStrokeListener > keyStrokeListeners = 
		new ArrayList < KeyStrokeListener >();


		protected List<String> deferredChat = new List<String>();
		protected internal List<CommandEncoder> commandEncoders = new List<CommandEncoder>();

		protected internal int nextGpId = 0;

		protected internal bool loggingPaused = false;
		protected internal System.Object loggingLock = new System.Object();
		protected internal Command pausedCommands;

		/*
		* Store the currently building GpId source. Only meaningful while
		* the GameModule or an Extension is actually in the process of being built
		* during module/extension load.
		*/
		protected internal GpIdSupport gpidSupport = null;
		protected internal System.Int64 crc = 0;


		public virtual void  initFrameTitle()
		{
			frame.setTitle(getLocalizedGameName());
		}
		
		protected internal GameModule(DataArchive archive)
		{
			InitBlock();
			this.archive = archive;
			
			frame.Closing += new System.ComponentModel.CancelEventHandler(this.GameModule_Closing_DO_NOTHING_ON_CLOSE);
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			frame.Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
			
			System.Windows.Forms.ContainerControl temp_Container;
			temp_Container = new System.Windows.Forms.ContainerControl();
			temp_Container.Parent = frame;
			addKeyStrokeSource(new KeyStrokeSource(temp_Container, 2));
			
			validator = new CompoundValidityChecker(new MandatoryComponent(this, typeof(Documentation)), new MandatoryComponent(this, typeof(GlobalOptions)));
			
			addCommandEncoder(new ChangePropertyCommandEncoder(propsContainer));
		}

		/// <summary> Initialize the module</summary>
		protected internal abstract void build();


		public override void  setAttribute(string name, System.Object value_Renamed)
		{
			if (MODULE_NAME.Equals(name))
			{
				if (Localization.Instance.TranslationInProgress)
				{
					localizedGameName = ((string) value_Renamed);
				}
				else
				{
					gameName = ((string) value_Renamed);
				}
				setConfigureName((string) value_Renamed);
			}
			else if (MODULE_VERSION.Equals(name))
			{
				moduleVersion = ((string) value_Renamed);
			}
			else if (VASSAL_VERSION_CREATED.Equals(name))
			{
				vassalVersionCreated = ((string) value_Renamed);
				string runningVersion = Info.Version;
				if (Info.compareVersions(vassalVersionCreated, runningVersion) > 0)
				{
					WarningDialog.show("GameModule.version_warning", vassalVersionCreated, runningVersion);
				}
			}
			else if (NEXT_PIECESLOT_ID.Equals(name))
			{
				try
				{
					nextGpId = System.Int32.Parse((string) value_Renamed);
				}
				catch (System.FormatException e)
				{
					throw new IllegalBuildException(e);
				}
			}
			else if (DESCRIPTION.Equals(name))
			{
				description = ((string) value_Renamed);
			}
		}
		
		public override string getAttributeValueString(string name)
		{
			if (MODULE_NAME.Equals(name))
			{
				return gameName;
			}
			else if (MODULE_VERSION.Equals(name))
			{
				return moduleVersion;
			}
			else if (VASSAL_VERSION_CREATED.Equals(name))
			{
				return vassalVersionCreated;
			}
			else if (VASSAL_VERSION_RUNNING.Equals(name))
			{
				return Info.Version;
			}
			else if (NEXT_PIECESLOT_ID.Equals(name))
			{
				return System.Convert.ToString(nextGpId);
			}
			else if (DESCRIPTION.Equals(name))
			{
				return description;
			}
			return null;
		}
		
		/// <summary> 
		/// A valid verson format is "w.x.y[bz]", where
		/// 'w','x','y', and 'z' are integers.
		/// </summary>
		/// <returns> a negative number if <code>v2</code> is a later version
		/// the <code>v1</code>, a positive number if an earlier version,
		/// or zero if the versions are the same.
		/// 
		/// </returns>
		/// <deprecated> use {@link Info#compareVersions}
		/// </deprecated>
	
		Deprecated
		public static int compareVersions(string v1, string v2)
		{
			return Info.compareVersions(v1, v2);
		}
		
		public override void  addTo(Buildable b)
		{
		}
		
		public override void  removeFrom(Buildable parent)
		{
		}
		
		public override HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("GameModule.htm"); //$NON-NLS-1$
		}
		
	
		public Class < ? > [] getAttributeTypes()
	
		class, 
		String.
	
		class, 
		String.
	
		class
		public abstract string encode(VassalSharp.command.Command param1);
		public abstract string generateGpId();
		public abstract void  addMutableProperty(string param1, VassalSharp.build.module.properties.MutableProperty param2);
		public abstract System.Object getProperty(System.Object param1);
		public abstract System.Object getLocalizedProperty(System.Object param1);
		public abstract VassalSharp.build.module.properties.MutableProperty removeMutableProperty(string param1);
		public abstract VassalSharp.command.Command decode(string param1);
		public abstract VassalSharp.build.module.properties.MutableProperty getMutableProperty(string param1);
		private void  GameModule_Closing_DO_NOTHING_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 0);
		}
		static GameModule()
		{
			log = LoggerFactory.getLogger(typeof(GameModule));
		}
	}
	

	}
	

	public Class < ? > [] getAllowableConfigureComponents()

	{ 
		return new Class < ? > []

	{ 
		Map.

	class, 
	PieceWindow.

	class, 
	PrototypesContainer.

	class, 
	ToolbarMenu.

	class, 
	MultiActionButton.

	class, 
	DoActionButton.

	class, 
	DiceButton.

	class, 
	GlobalKeyCommand.

	class, 
	StartupGlobalKeyCommand.

	class, 
	Inventory.

	class,
	//                  InternetDiceButton.class,   // Disable internet dice button until Bones server can prevent email spamming

	RandomTextButton.

	class, 
	SpecialDiceButton.

	class, 
	PredefinedSetup.

	class, 
	ChartWindow.

	class, 
	PrivateMap.

	class, 
	PlayerHand.

	class, 
	NotesWindow.

	class, 
	TurnTracker.

	class

	};

	}
	
	/// <summary> The GameModule acts as the mediator for hotkey events.
	/// 
	/// Components that wish to fire hotkey events when they have the
	/// focus should register themselves using this method.  These events will be
	/// forwarded to all listeners that have registered themselves with {@link #addKeyStrokeListener}
	/// </summary>

	public

	void addKeyStrokeSource(KeyStrokeSource src)

	{ 
		keyStrokeSources.add(src);

	for(KeyStrokeListener l: keyStrokeListeners)

	{ 
		l.addKeyStrokeSource(src);

	}

	}
	
	/// <summary> The GameModule acts as the mediator for hotkey events.
	/// 
	/// Objects that react to hotkey events should register themselves
	/// using this method.  Any component that has been registered with {@link #addKeyStrokeSource}
	/// will forward hotkey events to listeners registered with this method.
	/// </summary>

	public

	void addKeyStrokeListener(KeyStrokeListener l)

	{ 
		keyStrokeListeners.add(l);

	for(KeyStrokeSource s: keyStrokeSources)

	{ 
		l.addKeyStrokeSource(s);

	}

	} 
	
	@ Deprecated

	public

	void fireKeyStroke(KeyStroke stroke)

	{ 
		if(stroke != null)

	{ 
		for(KeyStrokeListener l: keyStrokeListeners)

	{ 
		l.keyPressed(stroke);

	}

	}

	}
	

	public

	void fireKeyStroke(NamedKeyStroke stroke)

	{ 
		if(stroke != null && !stroke.isNull())

	{ 
		fireKeyStroke(stroke.getKeyStroke());

	}

	}

		/// <returns> the name of the game for this module
		/// </returns>

		public String getGameName()

		{
			return gameName;

		}

		public String getLocalizedGameName()

		{
			return localizedGameName == null ? gameName : localizedGameName;

		}

		public String getGameVersion()

		{
			return moduleVersion;

		}

	/// <summary>The {@link Prefs} key for the user's real name </summary>

	public static final String REAL_NAME = RealName; //$NON-NLS-1$
	/// <summary>The {@link Prefs} key for the user's secret name </summary>

	public static final String SECRET_NAME = SecretName; //$NON-NLS-1$
	/// <summary>The {@link Prefs} key for the user's personal info </summary>

	public static final String PERSONAL_INFO = Profile; //$NON-NLS-1$
	

	public

	void addIdChangeListener(PropertyChangeListener l)

	{ 
		idChangeSupport.addPropertyChangeListener(l);

	}
	

	public

	void removeIdChangeListener(PropertyChangeListener l)

	{ 
		idChangeSupport.removePropertyChangeListener(l);

	}

		/// <returns> the preferences for this module
		/// </returns>

		public Prefs getPrefs()
		{
			if (preferences == null)
			{
				setPrefs(new Prefs(Prefs.GlobalPrefs.Editor, gameName));
			}
			return preferences;
		}

		/// <summary> A set of preferences that applies to all modules</summary>
		/// <returns>
		/// </returns>
		public Prefs getGlobalPrefs()
		{
			return Prefs.GlobalPrefs;
		}

		public void setPrefs(Prefs p)
		{
			preferences = p;
			//preferences.Editor.initDialog(Form.ActiveForm);
		}

		public void setGlobalPrefs(Prefs p)
		{
		}

		/// <summary> 
		/// This method adds a {@link CommandEncoder} to the list of objects
		/// that will attempt to decode/encode a command
		/// </summary>
		/// <seealso cref="decode">
		/// </seealso>
		/// <seealso cref="encode">
		/// </seealso>
		public void addCommandEncoder(CommandEncoder ce)
		{
			commandEncoders.Add(ce);
		}


	/// <summary> This method removes a {@link CommandEncoder} from the list of objects
	/// that will attempt to decode/encode a command
	/// 
	/// 
	/// </summary>
	/// <seealso cref="decode">
	/// </seealso>
	/// <seealso cref="encode">
	/// </seealso>

	public

	void removeCommandEncoder(CommandEncoder ce)

	{ 
		commandEncoders = ArrayUtils.remove(commandEncoders, ce);

	}
	
	/// <summary> Central location to create any type of GamePiece from within VASSAL
	/// 
	/// </summary>
	/// <param name="type">
	/// </param>
	/// <returns>
	/// </returns>

	public GamePiece createPiece(String type)

	{ 
		for(int i = 0;

	i < commandEncoders.length;

	++ i)

	{ 
		if(commandEncoders [i] instanceof BasicCommandEncoder)

	{ 
		GamePiece p =((BasicCommandEncoder) commandEncoders [i]).createPiece(type);

	if(p != null)

	{ 
		return p;

	}

	}

	} 
	return null;

	}
	

	public GamePiece createPiece(String type, GamePiece inner)

	{ 
		for(int i = 0;

	i < commandEncoders.length;

	++ i)

	{ 
		if(commandEncoders [i] instanceof BasicCommandEncoder)

	{ 
		GamePiece p =((BasicCommandEncoder) commandEncoders [i]).createDecorator(type, inner);

	if(p != null)

	{ 
		return p;

	}

	}

	} 
	return null;

	}
	
	/// <summary> Display the given text in the control window's status line.
	/// Save the messages for later if the Chatter has not been initialised yet
	/// </summary>

	public

	void warn(String s)

	{ 
		if(chat == null)

	{ 
		deferredChat.add(s);

	} 
	else

	{ 
		chat.show( -  + s); //$NON-NLS-1$

	}

	}
	
	/// <returns> a single Random number generator that all objects may share
	/// </returns>

	public Random getRNG()

	{ 
		return RNG;

	}
	
	/// <returns> the object responsible for logging commands to a logfile
	/// </returns>

	public Logger getLogger()

	{ 
		return logger;

	}
	
	/// <summary> Set the object that displays chat text. Display any warning
	/// messages deferred during earlier initialisation
	/// </summary>

	public

	void setChatter(Chatter c)

	{ 
		chat = c;

	if(deferredChat.size() > 0)

	{ 
		for(String msg: deferredChat)

	{ 
		warn(msg);

	} 
	deferredChat.clear();

	}

	}
	

	public JComponent getControlPanel()

	{ 
		return controlPanel;

	}
	
	/// <returns> the object that displays chat text
	/// </returns>

	public Chatter getChatter()

	{ 
		return chat;

	}
	
	/// <summary> Uses the registered  {@link CommandEncoder}s
	/// to decode a String into a {@link Command}.
	/// </summary>

	public Command decode(String command)

	{ 
		if(command == null)

	{ 
		return null;

	} 
	else

	{ 
		Command c = null;

	for(int i = 0;

	i < commandEncoders.length && c == null;

	++ i)

	{ 
		c = commandEncoders [i].decode(command);

	} 
	if(c == null)

	{ 
		System.err.println(Failed to decode  + command); //$NON-NLS-1$

	} 
	return c;

	}

	}
	
	/// <summary> Uses the registered {@link CommandEncoder}s to encode a {@link Command} into a String object</summary>

	public String encode(Command c)

	{ 
		if(c == null)

	{ 
		return null;

	} 
	String s = null;

	for(int i = 0;

	i < commandEncoders.length && s == null;

	++ i)

	{ 
		s = commandEncoders [i].encode(c);

	} 
	if(s == null)

	{ 
		System.err.println(Failed to encode  + c); //$NON-NLS-1$

	} 
	return s;

	}
	
	/// <returns> a common FileChooser so that recent file locations
	/// can be remembered
	/// </returns>

	public FileChooser getFileChooser()

	{ 
		if(fileChooser == null)

	{ 
		fileChooser = FileChooser.createFileChooser(getFrame(), 
		getGameState().getSavedGameDirectoryPreference());

	} 
	else

	{ 
		fileChooser.resetChoosableFileFilters();

	fileChooser.rescanCurrentDirectory();

	} 
	
	return fileChooser;

	}
	
	/// <deprecated> Use {@link #getFileChooser} instead.
	/// </deprecated>

	Deprecated

	public FileDialog getFileDialog()

	{ 
		if(fileDialog == null)

	{ 
		fileDialog = new FileDialog(getFrame());

	File f = getGameState().getSavedGameDirectoryPreference().getFileValue();

	if(f != null)

	{ 
		fileDialog.setDirectory(f.getPath());

	} 
	fileDialog.setModal(true);

	} 
	else

	{ 
		fileDialog.setDirectory(fileDialog.getDirectory());

	} 
	return fileDialog;

	}
	
	/// <returns> the JToolBar of the command window
	/// </returns>

	public JToolBar getToolBar()

	{ 
		return frame.getToolBar();

	}
	
	/// <summary> Append the string to the title of the controls window and all Map windows</summary>
	/// <param name="s">If null, set the title to the default.
	/// </param>

	public

	void appendToTitle(String s)

	{ 
		if(s == null)

	{ 
		frame.setTitle(Resources.getString(GameModule.frame_title, getLocalizedGameName())); //$NON-NLS-1$

	} 
	else

	{ 
		frame.setTitle(frame.getTitle() + s);

	} 
	for(Map m: getComponentsOf(Map.

	class))

	{ 
		m.appendToTitle(s);

	}

	}
	
	/// <summary> Exit the application, prompting user to save if necessary</summary>

	public

	void quit()

	{ 
		if(shutDown())

	{ 
		System.exit(0);

	}

	}
	
	/// <summary> Prompt user to save open game and modules/extensions being edited</summary>
	/// <returns> true if shutDown should proceed, i.e. user did not cancel
	/// </returns>

	public boolean shutDown()

	{ 
		boolean cancelled;

	getGameState().setup(false);

	cancelled = getGameState().isGameStarted();
	

	if(!cancelled)

	{ 
		if(getDataArchive() instanceof ArchiveWriter 
		&& !buildString().equals(lastSavedConfiguration))

	{ 
		switch(JOptionPane.showConfirmDialog(frame, 
		Resources.getString(GameModule.save_module), //$NON-NLS-1$

	, JOptionPane.YES_NO_CANCEL_OPTION))

	{ //$NON-NLS-1$

	case JOptionPane.YES_OPTION: 
	save();

	break;

	case JOptionPane.CANCEL_OPTION:

	case JOptionPane.CLOSED_OPTION: 
	cancelled = true;

	}

	} 
	for(ModuleExtension ext: getComponentsOf(ModuleExtension.

	class))

	{ 
		cancelled = !ext.confirmExit();

	}

	} 
	
	if(!cancelled)

	{ 
		Prefs p = null;
	
	// write and close module prefs

	try

	{ 
		p = getPrefs();

	p.write();

	p.close();

	}

	catch(IOException e)

	{ 
		WriteErrorDialog.error(e, p.getFile());

	}

	finally

	{ 
		IOUtils.closeQuietly(p);

	}
	
	// write and close global prefs
	// Bug 10179 - Global prefs are now written out each time a preference is changed
	// try {
	//   p = getGlobalPrefs();
	//   p.write();
	//   p.close();
	// }
	// catch (IOException e) {
	//   WriteErrorDialog.error(e, p.getFile());
	// }
	// finally {
	//  IOUtils.closeQuietly(p);
	// }
	
	// close the module

	try

	{ 
		archive.close();

	}

	catch(IOException e)

	{ 
		ReadErrorDialog.error(e, archive.getName());

	} 
	
	log.info(Exiting);

	} 
	
	return !cancelled;

	}
	
	/// <summary> Encode the {@link Command}, send it to the server and write it
	/// to a logfile (if any is open)
	/// 
	/// </summary>
	/// <seealso cref="encode">
	/// </seealso>

	public

	void sendAndLog(Command c)

	{ 
		if(c != null && !c.isNull())

	{ 
		synchronized(loggingLock)

	{ 
		if(loggingPaused)

	{ 
		if(pausedCommands == null)

	{ 
		pausedCommands = c;

	} 
	else

	{ 
		pausedCommands.append(c);

	}

	} 
	else

	{ 
		getServer().sendToOthers(c);

	getLogger().log(c);

	}

	}

	}

	}
	
	/// <summary> Pause logging and return true if successful.
	/// Return false if logging already paused
	/// 
	/// While Paused, commands are accumulated into pausedCommands so that they
	/// can all be logged at the same time, and generate a single UNDO command.
	/// 
	/// </summary>
	/// <returns>
	/// </returns>

	public boolean pauseLogging()

	{ 
		synchronized(loggingLock)

	{ 
		if(loggingPaused)

	{ 
		return false;

	} 
	loggingPaused = true;

	pausedCommands = null;

	return true;

	}

	}
	
	/// <summary> Restart logging and return any outstanding commands</summary>

	public Command resumeLogging()

	{ 
		Command c = null;

	synchronized(loggingLock)

	{ 
		c = pausedCommands == null ? new NullCommand(): pausedCommands;

	pausedCommands = null;

	loggingPaused = false;

	} 
	return c;

	}
	
	/// <summary> Clear outstanding Commands
	/// Use where the calling level handles the sending of outstanding commands
	/// </summary>

	public

	void clearPausedCommands()

	{ 
		pausedCommands = null;

	}
	
	

	private static String userId = null;
	
	/// <returns> a String that uniquely identifies the user
	/// </returns>

	public static String getUserId()

	{ 
		return userId;

	}
	
	/// <summary> Set the identifier for the user</summary>

	public static

	void setUserId(String newId)

	{ 
		userId = newId;

	}
	
	/// <returns> the object reponsible for sending messages to the server
	/// </returns>

	public ServerConnection getServer()

	{ 
		return server;

	}

		/// <summary> Set the singleton GameModule and invoke {@link #build} on it.</summary>

		public static

		void init(GameModule module)
		{
			if (theModule != null)
			{
				throw new ArgumentNullException(nameof(module));
			}
			else
			{
				theModule = module;
				theModule.setGpIdSupport(theModule);
				try
				{
					theModule.build();
				}
				catch (IOException e)
				{
					theModule = null;
					throw e;
				}
			}

			/*
			*  If we are editing, check for duplicate, illegal or missing GamePiece Id's
			*  and update if necessary.
			*/

			if (theModule.DataArchive is ArchiveWriter)
			{
				theModule.checkGpIds();
			}

			/*
			* Tell any Plugin components that the build is complete so that they
			* can finish initialization.
			*/
			foreach (Plugin plugin in theModule.getComponentsOf<Plugin>())
			{
				plugin.init();
			}

		}


/// <summary> Unload the module</summary>

public static

void unload()

{
	if (theModule != null)

	{
		if (theModule.shutDown())

		{
			theModule = null;

		}

	}

}

// Saved the current buildString for comparison when we try and quit.

public

void updateLastSave()

	{ 
		lastSavedConfiguration = buildString();

	}
	

	public String generateGpId()

	{ 
		return String.valueOf(nextGpId ++);

	}
	

	public int getNextGpId()

	{ 
		return nextGpId;

	}
	

	public

	void setNextGpId(int id)

	{ 
		nextGpId = id;

	}


		public

		void setGpIdSupport(GpIdSupport s)
		{
			gpidSupport = s;
		}

		public GpIdSupport getGpIdSupport()
		{
			return gpidSupport;
		}

		/// <summary> Check every PieceSlot and PlaceMarker trait for duplicate,
		/// illegal or Missing GamePiece id's and update them if necessary
		/// </summary>
		protected void checkGpIds()

		{
			GpIdChecker checker = new GpIdChecker(this);
			//for (PieceSlot pieceSlot: theModule.getAllDescendantComponentsOf(PieceSlot._class))
			//{
			//	checker.add(pieceSlot);
			//}
			checker.fixErrors();
		}

		/// <returns> the object which stores data for the module
		/// </returns>
		public DataArchive DataArchive { get { return archive; } }


	/// <summary> If the module is being edited, return the writeable archive for the module</summary>

	public ArchiveWriter getArchiveWriter()

	{ 
		return archive.getWriter();

	}
	

	public ImageTileSource getImageTileSource()

	{ 
		if(tcache == null)

	{
	// FIXME: There's no guarantee that getGameName() and getGameVersion()
	// are properly set at this point.
	

	final String hstr = 
	DigestUtils.shaHex(getGameName() + _ + getGameVersion());
	

	final File tc = new File(Info.getConfDir(), tiles/ + hstr);

	tcache = new ImageTileDiskCache(tc.FullName);

	} 
	
	return tcache;

	}
	
	/// <summary> Is the module being translated into the user's Locale?  Localization is disabled when editing a module
	/// 
	/// </summary>
	/// <returns> true if the module/extension has been localized
	/// </returns>

	public boolean isLocalizationEnabled()

	{ 
		return getArchiveWriter() == null;

	}

		/// <returns> the singleton instance of GameModule
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static GameModule getGameModule()

		{
			return theModule;

		}

	/// <summary> Return the object responsible for tracking the state of a game.
	/// Only one game in progress is allowed;
	/// </summary>

	public GameState getGameState()

	{ 
		return theState;

	}
	

	public

	void saveAs()

	{ 
		save(true);

	}
	
	/// <summary> If the module is being edited, write the module data</summary>

	public

	void save()

	{ 
		save(false);

	}
	

	protected

	void save(boolean saveAs)

	{ 
		vassalVersionCreated = Info.getVersion();
	

	final ArchiveWriter writer = getArchiveWriter();
	

	try

	{
		(new ModuleMetaData(this)).save(writer);

	}

	catch(IOException e)

	{ 
		WriteErrorDialog.error(e, writer.getName());

	}
	

	try

	{ 
		final String save = buildString();

	writer.addFile(BUILDFILE, 
	new ByteArrayInputStream(save.getBytes(UTF-8))); //$NON-NLS-1$
	

	if(saveAs) writer.saveAs(true);

	else writer.save(true);
	

	lastSavedConfiguration = save;

	}

	catch(IOException e)

	{ 
		WriteErrorDialog.error(e, writer.getName());

	}

	}
	

	protected String buildString()

	{ 
		org.w3c.dom.Document doc = Builder.createNewDocument();

	doc.appendChild(getBuildElement(doc));

	return Builder.ToString(doc);

	}
	
	/// <summary> Return values of Global properties</summary>

	public Object getProperty(Object key)

	{ 
		if(GlobalOptions.PLAYER_SIDE.equals(key) || GlobalOptions.PLAYER_SIDE_ALT.equals(key))

	{ 
		String mySide = PlayerRoster.getMySide();

	return mySide == null ? : mySide; //$NON-NLS-1$

	} 
	else if(GlobalOptions.PLAYER_NAME.equals(key) || GlobalOptions.PLAYER_NAME_ALT.equals(key))

	{ 
		return getPrefs().getValue(GameModule.REAL_NAME);

	} 
	else if(GlobalOptions.PLAYER_ID.equals(key) || GlobalOptions.PLAYER_ID_ALT.equals(key))

	{ 
		return GlobalOptions.getInstance().getPlayerId();

	} 
	MutableProperty p = propsContainer.getMutableProperty(String.valueOf(key));

	return p == null ? null: p.getPropertyValue();

	}
	

	public MutableProperty getMutableProperty(String name)

	{ 
		return propsContainer.getMutableProperty(name);

	}
	

	public

	void addMutableProperty(String key, MutableProperty p)

	{ 
		propsContainer.addMutableProperty(key, p);

	p.addMutablePropertyChangeListener(repaintOnPropertyChange);

	}
	

	public MutableProperty removeMutableProperty(String key)

	{ 
		MutableProperty p = propsContainer.removeMutableProperty(key);

	if(p != null)

	{ 
		p.removeMutablePropertyChangeListener(repaintOnPropertyChange);

	} 
	return p;

	}
	

	public String getMutablePropertiesContainerId()

	{ 
		return Module;

	}
	

	public Object getLocalizedProperty(Object key)

	{ 
		if(GlobalOptions.PLAYER_SIDE.equals(key) || GlobalOptions.PLAYER_SIDE_ALT.equals(key))

	{ 
		String mySide = PlayerRoster.getMyLocalizedSide();

	return mySide == null ? : mySide; //$NON-NLS-1$

	} 
	else

	{ 
		return getProperty(key);

	}

	}
	

	public long getCrc()

	{ 
		if(crc == null)

	{ 
		crc = buildCrc();

	} 
	return crc.longValue();

	}
	

	protected Long buildCrc()

	{ 
		final List < File > files = new ArrayList < File >();

	if(getDataArchive().getArchive() != null)

	{ 
		files.add(new File(getDataArchive().getName()));

	} 
	
	for(ModuleExtension ext: getComponentsOf(ModuleExtension.

	class))

	{ 
		if(ext.getDataArchive().getArchive() != null)

	{ 
		files.add(new File(ext.getDataArchive().getName()));

	}

	}
	

	try

	{ 
		return CRCUtils.getCRC(files);

	}

	catch(IOException e)

	{ 
		log.error(Error generating CRC, e);

	return 0L;

	}

	}
	

	public ComponentI18nData getI18nData()

	{ 
		ComponentI18nData myI18nData = super.getI18nData();

	myI18nData.setAttributeTranslatable(MODULE_VERSION, false);

	return myI18nData;

	}

	//public int NextGpId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

	//public string MutablePropertiesContainerId => throw new NotImplementedException();

	//public ToolBar ToolBar => throw new NotImplementedException();

	public void addMutableProperty(string key, MutableProperty p)
		{
			throw new NotImplementedException();
		}

		public Command decode(string command)
		{
			throw new NotImplementedException();
		}

		public string encode(Command c)
		{
			throw new NotImplementedException();
		}

		public string generateGpId()
		{
			throw new NotImplementedException();
		}

		public object getLocalizedProperty(object key)
		{
			throw new NotImplementedException();
		}

		public MutableProperty getMutableProperty(string propertyName)
		{
			throw new NotImplementedException();
		}

		public object getProperty(object key)
		{
			throw new NotImplementedException();
		}

		public MutableProperty removeMutableProperty(string key)
		{
			throw new NotImplementedException();
		}

	}


#else

	/// <summary> The GameModule class is the base class for a VASSAL module.  It is
	/// the root of the {@link Buildable} containment hierarchy.
	/// Components which are added directly to the GameModule are contained
	/// in the <code>VassalSharp.build.module</code> package.
	/// 
	/// <p>It is a singleton, and contains access points for many other classes,
	/// such as {@link DataArchive}, {@link ServerConnection}, {@link Logger},
	/// and {@link Prefs}.</p>
	/// </summary>
	public abstract class GameModule : AbstractConfigurable, CommandEncoder, ToolBarComponent, PropertySource, MutablePropertiesContainer, GpIdSupport
	{
		/// <returns> the top-level frame of the controls window
		/// </returns>
		virtual public System.Windows.Forms.Form Frame
		{
			get
			{
				return null;
			}
		}

		//virtual public WizardSupport WizardSupport
		//{
		//	get
		//	{
		//		return null;
		//	}
		//}
		public static string ConfigureTypeName
		{
			get
			{
				return Resources.getString("Editor.GameModule.component_type"); //$NON-NLS-1$
			}
		}
		override public string[] AttributeNames
		{
			get
			{
				return new string[] { MODULE_NAME, MODULE_VERSION, DESCRIPTION, VASSAL_VERSION_CREATED, NEXT_PIECESLOT_ID };
			}
		}
		override public string[] AttributeDescriptions
		{
			get
			{
				return new string[] { Resources.getString("Editor.GameModule.name_label"), Resources.getString("Editor.GameModule.version_label"), Resources.getString("Editor.GameModule.description") };
			}
		}
		public abstract string MutablePropertiesContainerId { get; }
		public abstract System.Windows.Forms.ToolStrip ToolBar { get; }
		public abstract int NextGpId { get; set; }

		public const string MODULE_NAME = "name"; //$NON-NLS-1$
		public const string MODULE_VERSION = "version"; //$NON-NLS-1$
		public const string DESCRIPTION = "description";
		public const string VASSAL_VERSION_CREATED = "VassalVersion"; //$NON-NLS-1$
		/// <summary>The System property of this name will return a version identifier for the version of VASSAL being run </summary>
		public const string VASSAL_VERSION_RUNNING = "runningVassalVersion"; //$NON-NLS-1$
		public const string NEXT_PIECESLOT_ID = "nextPieceSlotId";
		public const string BUILDFILE = "buildFile";


		public virtual void initFrameTitle() { }

		public override void SetAttribute(string name, System.Object value_Renamed) { }

		public override string getAttributeValueString(string name) { return null; }

		/// <summary> 
		/// A valid verson format is "w.x.y[bz]", where
		/// 'w','x','y', and 'z' are integers.
		/// </summary>
		/// <returns> a negative number if <code>v2</code> is a later version
		/// the <code>v1</code>, a positive number if an earlier version,
		/// or zero if the versions are the same.
		/// 
		/// </returns>
		/// <deprecated> use {@link Info#compareVersions}
		/// </deprecated>

		public static int compareVersions(string v1, string v2)
		{
			return 0;
		}

		public override void addTo(Buildable b)
		{
		}

		public override void removeFrom(Buildable parent)
		{
		}

		public override HelpFile getHelpFile()
		{
			return null;
		}

		/// <summary> The GameModule acts as the mediator for hotkey events.
		/// 
		/// Components that wish to fire hotkey events when they have the
		/// focus should register themselves using this method.  These events will be
		/// forwarded to all listeners that have registered themselves with {@link #addKeyStrokeListener}
		/// </summary>

		public void addKeyStrokeSource(KeyStrokeSource src)
		{
		}

		/// <summary> The GameModule acts as the mediator for hotkey events.
		/// 
		/// Objects that react to hotkey events should register themselves
		/// using this method.  Any component that has been registered with {@link #addKeyStrokeSource}
		/// will forward hotkey events to listeners registered with this method.
		/// </summary>

		public void addKeyStrokeListener(KeyStrokeListener l)
		{
		}

		public
		void fireKeyStroke(KeyStroke stroke)
		{
		}

		public
		void fireKeyStroke(NamedKeyStroke stroke)
		{
		}
		/// <returns> the name of the game for this module
		/// </returns>

		public String getGameName()
		{
			return null;
		}

		public String getLocalizedGameName()
		{
			return null;
		}

		public String getGameVersion()
		{
			return null;
		}
		/// <summary>The {@link Prefs} key for the user's real name </summary>

		public static String REAL_NAME = "RealName"; //$NON-NLS-1$
		/// <summary>The {@link Prefs} key for the user's secret name </summary>

		public static String SECRET_NAME = "SecretName"; //$NON-NLS-1$
		/// <summary>The {@link Prefs} key for the user's personal info </summary>

		public static String PERSONAL_INFO = "Profile"; //$NON-NLS-1$

		//public void addIdChangeListener(PropertyChangeListener l)
		//{
		//}

		//public void removeIdChangeListener(PropertyChangeListener l)
		//{
		//}

		/// <returns> the preferences for this module
		/// </returns>
		public Prefs getPrefs()
		{
			return null;
		}

		/// <summary> A set of preferences that applies to all modules</summary>
		/// <returns>
		/// </returns>
		public Prefs getGlobalPrefs()
		{
			return null;
		}

		public void setPrefs(Prefs p)
		{
		}

		public void setGlobalPrefs(Prefs p)
		{
		}

		/// <summary> 
		/// This method adds a {@link CommandEncoder} to the list of objects
		/// that will attempt to decode/encode a command
		/// </summary>
		/// <seealso cref="decode">
		/// </seealso>
		/// <seealso cref="encode">
		/// </seealso>
		public void addCommandEncoder(CommandEncoder ce)
		{
		}

		/// <summary> This method removes a {@link CommandEncoder} from the list of objects
		/// that will attempt to decode/encode a command
		/// 
		/// 
		/// </summary>
		/// <seealso cref="decode">
		/// </seealso>
		/// <seealso cref="encode">
		/// </seealso>

		public void removeCommandEncoder(CommandEncoder ce)
		{
		}

		/// <summary> Central location to create any type of GamePiece from within VASSAL
		/// 
		/// </summary>
		/// <param name="type">
		/// </param>
		/// <returns>
		/// </returns>
		//public GamePiece createPiece(String type)
		//{
		//	return null;
		//}

		//public GamePiece createPiece(String type, GamePiece inner)
		//{
		//	return null;
		//}

		/// <summary> Display the given text in the control window's status line.
		/// Save the messages for later if the Chatter has not been initialised yet
		/// </summary>
		public void warn(String s)
		{
		}

		/// <returns> a single Random number generator that all objects may share
		/// </returns>
		public Random getRNG()
		{
			return null;
		}

		/// <returns> the object responsible for logging commands to a logfile
		/// </returns>
		public ILogger getLogger()
		{
			return null;
		}

		///// <summary> Set the object that displays chat text. Display any warning
		///// messages deferred during earlier initialisation
		///// </summary>
		//public void setChatter(Chatter c)
		//{
		//}

		public System.Windows.Forms.Panel getControlPanel()
		{
			return null;
		}

		///// <returns> the object that displays chat text
		///// </returns>
		//public Chatter getChatter()
		//{
		//	return null;
		//}

		/// <summary> Uses the registered  {@link CommandEncoder}s
		/// to decode a String into a {@link Command}.
		/// </summary>
		public Command decode(String command)
		{
			return null;
		}

		/// <summary> Uses the registered {@link CommandEncoder}s to encode a {@link Command} into a String object</summary>
		public String encode(Command c)
		{
			return null;
		}

		///// <returns> a common FileChooser so that recent file locations
		///// can be remembered
		///// </returns>
		//public FileChooser getFileChooser()
		//{
		//	return null;
		//}

		/// <deprecated> Use {@link #getFileChooser} instead.
		/// </deprecated>
		public FileDialog getFileDialog()
		{
			return null;
		}

		/// <returns> the JToolBar of the command window
		/// </returns>
		public System.Windows.Forms.ToolStrip getToolBar()
		{
			return null;
		}

		/// <summary> Append the string to the title of the controls window and all Map windows</summary>
		/// <param name="s">If null, set the title to the default.
		/// </param>
		public void appendToTitle(String s)
		{
		}

		/// <summary> Exit the application, prompting user to save if necessary</summary>
		public void quit()
		{
		}

		/// <summary> Prompt user to save open game and modules/extensions being edited</summary>
		/// <returns> true if shutDown should proceed, i.e. user did not cancel
		/// </returns>
		public bool shutDown()
		{
			return false;
		}

		/// <summary> Encode the {@link Command}, send it to the server and write it
		/// to a logfile (if any is open)
		/// 
		/// </summary>
		/// <seealso cref="encode">
		/// </seealso>
		public void sendAndLog(Command c)
		{
		}

		/// <summary> Pause logging and return true if successful.
		/// Return false if logging already paused
		/// 
		/// While Paused, commands are accumulated into pausedCommands so that they
		/// can all be logged at the same time, and generate a single UNDO command.
		/// 
		/// </summary>
		/// <returns>
		/// </returns>
		public bool pauseLogging()
		{
			return true;
		}

		/// <summary> Restart logging and return any outstanding commands</summary>
		public Command resumeLogging()
		{
			return null;
		}

		/// <summary> Clear outstanding Commands
		/// Use where the calling level handles the sending of outstanding commands
		/// </summary>

		public void clearPausedCommands()

		{

		}



		private static String userId = null;

		/// <returns> a String that uniquely identifies the user
		/// </returns>

		public static String getUserId()

		{
			return userId;

		}

		/// <summary> Set the identifier for the user</summary>

		public static

		void setUserId(String newId)

		{
			userId = newId;

		}

		///// <returns> the object reponsible for sending messages to the server
		///// </returns>
		//public ServerConnection getServer()
		//{
		//	return null;
		//}

		/// <summary> Set the singleton GameModule and invoke {@link #build} on it.</summary>
		public static

		void init(GameModule module)
		{
		}

		/// <summary> Unload the module</summary>

		public static

		void unload()

		{
		}

		// Saved the current buildString for comparison when we try and quit.

		public void updateLastSave()

		{

		}


		public String generateGpId()

		{
			return null;

		}


		public int getNextGpId()

		{
			return 0;

		}


		public void setNextGpId(int id)

		{

		}

		public void setGpIdSupport(GpIdSupport s)
		{
		}

		public GpIdSupport getGpIdSupport()
		{
			return null;
		}


		/// <returns> the object which stores data for the module
		/// </returns>
		public DataArchive DataArchive { get { return null; } }

		/// <summary> If the module is being edited, return the writeable archive for the module</summary>

		public ArchiveWriter getArchiveWriter()

		{
			return null;

		}


		public ImageTileSource getImageTileSource()
		{
			return null;
		}

		/// <summary> Is the module being translated into the user's Locale?  Localization is disabled when editing a module
		/// 
		/// </summary>
		/// <returns> true if the module/extension has been localized
		/// </returns>

		public bool isLocalizationEnabled()

		{
			return false;

		}
		/// <returns> the singleton instance of GameModule
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static GameModule getGameModule()

		{
			return null;

		}

		///// <summary> Return the object responsible for tracking the state of a game.
		///// Only one game in progress is allowed;
		///// </summary>
		//public GameState getGameState()
		//{
		//	return null;
		//}


		public void saveAs()

		{

		}

		/// <summary> If the module is being edited, write the module data</summary>

		public void save()

		{

		}


		protected

		void save(bool saveAs)

		{
		}




		/// <summary> Return values of Global properties</summary>

		public Object getProperty(Object key)

		{
			return null;
		}


		public MutableProperty getMutableProperty(String name)

		{
			return null;

		}


		public void addMutableProperty(String key, MutableProperty p)

		{

		}


		public MutableProperty removeMutableProperty(String key)

		{
			return null;

		}


		public String getMutablePropertiesContainerId()

		{
			return null;

		}


		public Object getLocalizedProperty(Object key)

		{
			return null;
		}


		public long getCrc()

		{
			return 0;
		}



		public ComponentI18nData getI18nData()

		{
			return null;

		}

		//public int NextGpId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

		//public string MutablePropertiesContainerId => throw new NotImplementedException();

		//public ToolBar ToolBar => throw new NotImplementedException();

	}
#endif
}
