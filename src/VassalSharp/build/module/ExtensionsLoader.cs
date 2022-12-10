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

using GameModule = VassalSharp.build.GameModule;
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using DirectoryConfigurer = VassalSharp.configure.DirectoryConfigurer;
using Resources = VassalSharp.i18n.Resources;
using DataArchive = VassalSharp.tools.DataArchive;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using System.Collections.Generic;
using System.IO;

namespace VassalSharp.build.module
{
	
	public class ExtensionsLoader : CommandEncoder
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(ExtensionsLoader extensionsLoader)
			{
				this.EntensionsLoader = extensionsLoader;
			}

            public ExtensionsLoader EntensionsLoader { get; }

            public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				EntensionsLoader.extMgr.setExtensionsDirectory((System.IO.FileInfo) evt.NewValue);
				EntensionsLoader.addExtensions();
			}
		}

		private static readonly Logger logger;

		public const string COMMAND_PREFIX = "EXT\t"; //$NON-NLS-1$
													  // Preferences key for the list of extensions to load
		public const string SPECIFY_DIR_IN_PREFS = "specifyExtensionDirInPrefs"; //$NON-NLS-1$
		public const string EXTENSION_DIR = "extensionDIR"; //$NON-NLS-1$

		protected HashSet<string> loadedExtensions = new HashSet<string>();
		protected Dictionary<string, string> loadedIds = new Dictionary<string, string>();

		protected internal ExtensionsManager extMgr;
		protected internal ExtensionsManager globalExtMgr;

		public virtual void addTo(GameModule mod)
		{
			extMgr = new ExtensionsManager(mod);
			globalExtMgr = new ExtensionsManager("ext");
			mod.addCommandEncoder(this);
			if ("true".Equals(GlobalOptions.Instance.getAttributeValueString(SPECIFY_DIR_IN_PREFS)))
			{
				DirectoryConfigurer config = new DirectoryConfigurer(EXTENSION_DIR, Resources.getString("ExtensionsLoader.extensions_directory")); //$NON-NLS-1$
				config.setValue((System.Object) null);
				GameModule.getGameModule().getPrefs().addOption(Resources.getString("ExtensionsLoader.extensions_tab"), config); //$NON-NLS-1$
				extMgr.setExtensionsDirectory(config.FileValue);
				if (config.FileValue == null)
				{
					config.setValue(new System.IO.FileInfo(extMgr.getExtensionsDirectory(false).FullName));
				}
				config.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
			}
			addExtensions();
		}
		
		protected internal virtual void  addExtensions()
		{
			foreach(FileInfo ext in globalExtMgr.getActiveExtensions())
			{
				if (!addExtension(ext))
				{
					globalExtMgr.setActive(ext, false);
				}
			}
			foreach (FileInfo ext in extMgr.getActiveExtensions())
			{
				if (!addExtension(ext))
				{
					GameModule.getGameModule().warn(Resources.getString("ExtensionsLoader.deactivating_extension", ext.getName()));
					extMgr.setActive(ext, false);
				}
			}
		}
		
		protected internal virtual bool addExtension(System.IO.FileInfo extension)
		{
			logger.info("Loading extension " + extension);
			string extname = extension.FullName;
			bool success = loadedExtensions.Contains(extname);
			if (!success)
			{
				try
				{
					ModuleExtension ext = createExtension(extname);
					ext.build();
					
					string id = ext.ExtensionId;
					string idMsg = "";
					if (id.Length > 0)
					{
						//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
						foreach(string loadedId in loadedIds.Keys)
						{
							if (loadedId.Equals(id))
							{
								idMsg = Resources.getString("ExtensionsLoader.id_conflict", extension.Name, id, loadedIds.get_Renamed(id));
							}
						}
						loadedIds.Add(id, extname);
					}
					
					string msg = getLoadedMessage(ext.Name, ext.Version);
					loadedExtensions.Add(extname);
					GameModule.getGameModule().warn(msg);
					logger.info(msg);
					
					if (idMsg.Length > 0)
					{
						GameModule.getGameModule().warn(idMsg);
						logger.info(idMsg);
					}
					success = true;
				}
				catch (System.IO.IOException e)
				{
					reportBuildError(e, extension.Name);
				}
				catch (LoadExtensionException e)
				{
					reportBuildError(e, extension.Name);
				}
			}
			return success;
		}
		
		protected internal virtual ModuleExtension createExtension(string extname)
		{
			return new ModuleExtension(new DataArchive(extname));
		}
		
		protected internal virtual string getLoadedMessage(string name, string version)
		{
			return Resources.getString("ExtensionsLoader.extension_loaded", name, version); //$NON-NLS-1$
		}
		
		private void  reportBuildError(System.Exception e, string name)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			string msg = e.Message;
			if (msg == null || msg.Length == 0)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				msg = e.GetType().FullName;
				msg = msg.Substring(msg.LastIndexOf('.'));
			}
			GameModule.getGameModule().warn(getErrorMessage(name, msg)); //$NON-NLS-1$
		}
		
		protected internal virtual string getErrorMessage(string name, string msg)
		{
			return Resources.getString("ExtensionsLoader.unable_to_load", name, msg);
		}
		
		public virtual Command decode(string command)
		{
			Command c = null;
			if (command.StartsWith(COMMAND_PREFIX))
			{
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(command.Substring(COMMAND_PREFIX.Length), '\t');
				c = new ModuleExtension.RegCmd(st.nextToken(), st.nextToken());
			}
			return c;
		}
		
		public virtual string encode(Command c)
		{
			string s = null;
			if (c is ModuleExtension.RegCmd)
			{
				ModuleExtension.RegCmd cmd = (ModuleExtension.RegCmd) c;
				SequenceEncoder se = new SequenceEncoder('\t');
				se.append(cmd.Name).append(cmd.Version);
				s = COMMAND_PREFIX + se.Value;
			}
			return s;
		}
		
		[Serializable]
		public class LoadExtensionException : Exception
		{
			
			public LoadExtensionException() : base()
			{
			}
			
			public LoadExtensionException(string message, System.Exception cause) : base(message, cause)
			{
			}
			
			public LoadExtensionException(string message) : base(message)
			{
			}
			
			public LoadExtensionException(System.Exception cause) : base(string.Empty, cause)
			{
			}
		}
		static ExtensionsLoader()
		{
			logger = LoggerFactory.getLogger(typeof(ExtensionsLoader));
		}
	}
}