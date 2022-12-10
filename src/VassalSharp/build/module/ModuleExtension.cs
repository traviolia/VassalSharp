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
#if NEVER_DEFINED
using System;
////UPGRADE_TODO: The type 'java.util.UUID' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using UUID = java.util.UUID;
////UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using Logger = org.slf4j.Logger;
////UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using LoggerFactory = org.slf4j.LoggerFactory;
using Info = VassalSharp.Info;
using AbstractBuildable = VassalSharp.build.AbstractBuildable;
using Buildable = VassalSharp.build.Buildable;
using Builder = VassalSharp.build.Builder;
using GameModule = VassalSharp.build.GameModule;
//using GpIdChecker = VassalSharp.build.GpIdChecker;
using GpIdSupport = VassalSharp.build.GpIdSupport;
//using IllegalBuildException = VassalSharp.build.IllegalBuildException;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using ExtensionMetaData = VassalSharp.build.module.metadata.ExtensionMetaData;
//using PieceSlot = VassalSharp.build.widget.PieceSlot;
using Command = VassalSharp.command.Command;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using Resources = VassalSharp.i18n.Resources;
using ArchiveWriter = VassalSharp.tools.ArchiveWriter;
using DataArchive = VassalSharp.tools.DataArchive;
using IOUtils = VassalSharp.tools.io.IOUtils;
#else
using System;
////UPGRADE_TODO: The type 'java.util.UUID' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using UUID = java.util.UUID;
////UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using Logger = org.slf4j.Logger;
////UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using LoggerFactory = org.slf4j.LoggerFactory;
using Info = VassalSharp.Info;
using AbstractBuildable = VassalSharp.build.AbstractBuildable;
using Buildable = VassalSharp.build.Buildable;
using Builder = VassalSharp.build.Builder;
using GameModule = VassalSharp.build.GameModule;
//using GpIdChecker = VassalSharp.build.GpIdChecker;
using GpIdSupport = VassalSharp.build.GpIdSupport;
//using IllegalBuildException = VassalSharp.build.IllegalBuildException;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using ExtensionMetaData = VassalSharp.build.module.metadata.ExtensionMetaData;
//using PieceSlot = VassalSharp.build.widget.PieceSlot;
using Command = VassalSharp.command.Command;
//using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using Resources = VassalSharp.i18n.Resources;
using ArchiveWriter = VassalSharp.tools.ArchiveWriter;
using DataArchive = VassalSharp.tools.DataArchive;
using IOUtils = VassalSharp.tools.io.IOUtils;
#endif

namespace VassalSharp.build.module
{

#if NEVER_DEFINED
	/// <summary> An optional extension to a GameModule
	/// Like a GameModule, it is built from scratch from a 'buildFile' in a DataArchive
	/// The components described in the buildFile are appended to components in the base DataArchive
	/// </summary>
	public class ModuleExtension : AbstractBuildable, GameComponent, PluginsLoader.PluginElement, GpIdSupport
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(ModuleExtension enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ModuleExtension enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ModuleExtension enclosingInstance;
			public ModuleExtension Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
                System.String s = string.Empty; // (System.String) JOptionPane.showInputDialog(GameModule.getGameModule().getFrame(), "Are you sure you wish to change the Extension Id?\n\nThe Extension Id links counters in existing save\ngames to the counter definitions in this Extension.\n\nIf you change the Id, then the Saved Game Updater\nmay not be able to update the counters from existing\nSaved Games.\n\nNew Extension Id:", "", (int) System.Windows.Forms.MessageBoxIcon.Exclamation, null, null, Enclosing_Instance.ExtensionId);
				if (s != null && !s.Equals(Enclosing_Instance.ExtensionId))
				{
					Enclosing_Instance.extensionId = s;
					Enclosing_Instance.updateGpIds();
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					Enclosing_Instance.idDisplay.Text = Enclosing_Instance.ExtensionId;
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(VassalSharp.configure.StringConfigurer config, VassalSharp.configure.StringConfigurer dconfig, VassalSharp.configure.BooleanConfigurer uconfig, System.Windows.Forms.Form d, ModuleExtension enclosingInstance)
			{
				InitBlock(config, dconfig, uconfig, d, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.configure.StringConfigurer config, VassalSharp.configure.StringConfigurer dconfig, VassalSharp.configure.BooleanConfigurer uconfig, System.Windows.Forms.Form d, ModuleExtension enclosingInstance)
			{
				this.config = config;
				this.dconfig = dconfig;
				this.uconfig = uconfig;
				this.d = d;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable config was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.StringConfigurer config;
			//UPGRADE_NOTE: Final variable dconfig was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.StringConfigurer dconfig;
			//UPGRADE_NOTE: Final variable uconfig was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.BooleanConfigurer uconfig;
			//UPGRADE_NOTE: Final variable d was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Form d;
			private ModuleExtension enclosingInstance;
			public ModuleExtension Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.setAttribute(VassalSharp.build.module.ModuleExtension.VERSION, config.getValue());
				Enclosing_Instance.setAttribute(VassalSharp.build.module.ModuleExtension.DESCRIPTION, dconfig.getValue());
				Enclosing_Instance.setAttribute(VassalSharp.build.module.ModuleExtension.UNIVERSAL, uconfig.getValue());
				d.Dispose();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(System.Windows.Forms.Form d, ModuleExtension enclosingInstance)
			{
				InitBlock(d, enclosingInstance);
			}
			private void  InitBlock(System.Windows.Forms.Form d, ModuleExtension enclosingInstance)
			{
				this.d = d;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable d was copied into class AnonymousClassActionListener2. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Form d;
			private ModuleExtension enclosingInstance;
			public ModuleExtension Enclosing_Instance
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
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction:SupportClass.ActionSupport
		{
			public AnonymousClassAbstractAction(System.Windows.Forms.Form d, ModuleExtension enclosingInstance)
			{
				InitBlock(d, enclosingInstance);
			}
			private void  InitBlock(System.Windows.Forms.Form d, ModuleExtension enclosingInstance)
			{
				this.d = d;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable d was copied into class AnonymousClassAbstractAction. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Windows.Forms.Form d;
			private ModuleExtension enclosingInstance;
			public ModuleExtension Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(d, "Visible", true);
			}
		}

		//private void  InitBlock()
		//{
		//	return ;
		//	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		//	new Class < ? > [0];
		//}
		virtual public System.String Version
		{
			get
			{
				return version;
			}
			
		}
		virtual public DataArchive DataArchive
		{
			get
			{
				return archive;
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				return description;
			}
			
		}
		virtual public bool Universal
		{
			get
			{
				return universal;
			}
			
		}
		virtual public int NextGpId
		{
			get
			{
				return nextGpId;
			}
			
			set
			{
				nextGpId = value;
			}
			
		}
		virtual public Command RestoreCommand
		{
			get
			{
				return new RegCmd(Name, version);
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[]{UNIVERSAL, VERSION, DESCRIPTION, BASE_MODULE_NAME, BASE_MODULE_VERSION, VASSAL_VERSION_CREATED, NEXT_PIECESLOT_ID, EXTENSION_ID};
			}
			
		}
		virtual public HelpFile HelpFile
		{
			get
			{
				return null;
			}
			
		}
		virtual public System.String ExtensionId
		{
			get
			{
				return extensionId;
			}
			
		}
		virtual public System.String Name
		{
			get
			{
				System.String name = "Extension";
				if (archive != null)
				{
					name = archive.Name;
					int index = name.LastIndexOf((System.Char) System.IO.Path.DirectorySeparatorChar);
					if (index < name.Length)
					{
						name = name.Substring(index + 1);
					}
					index = name.LastIndexOf('.');
					if (index > 0)
					{
						name = name.Substring(0, (index) - (0));
					}
				}
				return name;
			}
			
		}
		virtual protected internal System.String NotLoadedMsg
		{
			get
			{
                //return Resources.getString("ModuleExtension.load_extension", name, new ExtensionsManager(GameModule.getGameModule()).getExtensionsDirectory(false)); //$NON-NLS-1$
                return string.Empty;
			}
			
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.build.module.ModuleExtension'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		//private static readonly Logger logger;
		
		public const System.String BASE_MODULE_NAME = "module"; //$NON-NLS-1$
		public const System.String BASE_MODULE_VERSION = "moduleVersion"; //$NON-NLS-1$
		public const System.String VERSION = "version"; //$NON-NLS-1$
		public const System.String VASSAL_VERSION_CREATED = "vassalVersion"; //$NON-NLS-1$
		// NB The following key MUST sort before the other keys for universal modules to load
		public const System.String UNIVERSAL = "anyModule"; //$NON-NLS-1$
		public const System.String NEXT_PIECESLOT_ID = "nextPieceSlotId"; //$NON-NLS-1$
		public const System.String EXTENSION_ID = "extensionId"; //$NON-NLS-1$
		public const System.String DESCRIPTION = "description";

        private DataArchive archive;
        private System.String version = "0.0"; //$NON-NLS-1$
		protected internal bool universal = false;
		
		private System.String lastSave;
		private System.String vassalVersionCreated;
		private SupportClass.ActionSupport editAction;
		
		protected internal int nextGpId = 0;
		protected internal System.String extensionId = "";
		protected internal System.Windows.Forms.TextBox idDisplay;
		protected internal System.String description = "";
		
		public ModuleExtension(DataArchive archive)
		{
			//InitBlock();
			this.archive = archive;
		}
		
		public virtual void  build()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'fileName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String fileName = GameModule.BUILDFILE; //$NON-NLS-1$
			
			GameModule.getGameModule().DataArchive.addExtension(archive);
			
			// Record that we are currently building this Extension
			GameModule.getGameModule().setGpIdSupport(this);
			
			System.IO.BufferedStream in_Renamed = null;
			try
			{
				in_Renamed = new System.IO.BufferedStream(archive.getInputStream(fileName));
			}
			// FIXME: should this be a FileNotFoundException?
			catch (System.IO.IOException e)
			{
			}
			
			if (in_Renamed == null)
			{
				build(null);
			}
			else
			{
				try
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'doc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Xml.XmlDocument doc = Builder.createDocument(in_Renamed);
					build((System.Xml.XmlElement) doc.DocumentElement);
					in_Renamed.Close();
				}
				// FIXME: review error message
				catch (System.IO.IOException e)
				{
					throw new ExtensionsLoader.LoadExtensionException(e);
				}
				finally
				{
					//IOUtils.closeQuietly(in_Renamed);
				}
			}
			
			GameModule.getGameModule().add(this);
			GameModule.getGameModule().getGameState().addGameComponent(this);
			
			if (archive is ArchiveWriter)
			{
				lastSave = buildString();
				
				// Has an Extension Id been allocated yet?
				if (extensionId.Length == 0)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'id '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String id = Guid.NewGuid().ToString();
					extensionId = id.Substring(id.Length - 3);
				}
				
				// Fix missing, duplicate and illegal GamePieceId's
				//UPGRADE_NOTE: Final was removed from the declaration of 'checker '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GpIdChecker checker = new GpIdChecker(this);
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				foreach(Buildable b in getBuildables())
				{
					checkGpIds(b, checker);
				}
				checker.fixErrors();
			}
		}
		
		protected internal virtual void  checkGpIds(Buildable b, GpIdChecker checker)
		{
			//if (b is PieceSlot)
			//{
			//	checker.add((PieceSlot) b);
			//}
			//else if (b is ExtensionElement)
			//{
			//	checkGpIds(((ExtensionElement) b).Extension, checker);
			//}
			//else if (b is AbstractBuildable)
			//{
			//	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//	foreach (Buildable buildable in ((AbstractBuildable) b).getBuildables())
			//	{
			//		checkGpIds(buildable, checker);
			//	}
			//}
		}
		
		
		/// <summary> Allocate new gpid's to all PieceSlots defined in a Buildable and
		/// all of it's children
		/// 
		/// </summary>
		protected internal virtual void  updateGpIds()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			foreach (Buildable b in getBuildables())
			{
				updateGpIds(b);
			}
		}
		
		protected internal virtual void  updateGpIds(Buildable b)
		{
			//if (b is PieceSlot)
			//{
			//	((PieceSlot) b).updateGpId();
			//}
			//else if (b is ExtensionElement)
			//{
			//	updateGpIds(((ExtensionElement) b).Extension);
			//}
			//else if (b is AbstractBuildable)
			//{
			//	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//	foreach (Buildable buildable in ((AbstractBuildable) b).getBuildables())
			//	{
			//		updateGpIds(buildable);
			//	}
			//}
		}
		
		public virtual void  setup(bool gameStarting)
		{
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Type[] getAllowableConfigureComponents()
        {
            return null;
        }
		
		public virtual void  removeFrom(Buildable parent)
		{
			//throw new IllegalBuildException(Resources.getString("ModuleExtension.cannot_remove")); //$NON-NLS-1$
		}
		
		public virtual bool confirmExit()
		{
			bool confirm = true;
			if (archive is ArchiveWriter && !buildString().Equals(lastSave))
			{
				//switch (JOptionPane.showConfirmDialog(GameModule.getGameModule().getFrame(), Resources.getString("ModuleExtension.save_extension"), "", (int) System.Windows.Forms.MessageBoxButtons.YesNoCancel))
				//{
					
				//	//$NON-NLS-1$
				//	//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.YES_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				//	case (int) System.Windows.Forms.DialogResult.Yes: 
				//		try
				//		{
				//			save();
				//		}
				//		// FIXME: review error message
				//		catch (System.IO.IOException e)
				//		{
				//			confirm = false;
				//		}
				//		break;
					
				//	//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.JOptionPane.CANCEL_OPTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				//	case (int) System.Windows.Forms.DialogResult.Cancel: 
				//		confirm = false;
				//		break;
				//	}
			}
			return confirm;
		}
		
		public override System.String getAttributeValueString(System.String key)
		{
			System.String s = null;
			if (BASE_MODULE_NAME.Equals(key))
			{
				s = GameModule.getGameModule().getGameName();
			}
			else if (BASE_MODULE_VERSION.Equals(key))
			{
				s = GameModule.getGameModule().getGameVersion();
			}
			else if (VERSION.Equals(key))
			{
				s = version;
			}
			else if (VASSAL_VERSION_CREATED.Equals(key))
			{
				s = vassalVersionCreated;
			}
			else if (UNIVERSAL.Equals(key))
			{
				s = System.Convert.ToString(universal);
			}
			else if (NEXT_PIECESLOT_ID.Equals(key))
			{
				s = System.Convert.ToString(nextGpId);
			}
			else if (EXTENSION_ID.Equals(key))
			{
				s = extensionId;
			}
			else if (DESCRIPTION.Equals(key))
			{
				s = description;
			}
			return s;
		}

        public override void setAttribute(System.String key, System.Object value_Renamed)
        {
            //	if (BASE_MODULE_NAME.Equals(key))
            //	{
            //		if (!universal && !GameModule.getGameModule().getGameName().equals(value_Renamed))
            //		{
            //			throw new ExtensionsLoader.LoadExtensionException(Resources.getString("ModuleExtension.extension_built", Name, (System.String) value_Renamed)); //$NON-NLS-1$
            //		}
            //	}
            //	else if (BASE_MODULE_VERSION.Equals(key))
            //	{
            //		System.String version = (System.String) value_Renamed;
            //		if (!universal && Info.compareVersions(GameModule.getGameModule().getGameVersion(), version) < 0)
            //		{
            //			GameModule.getGameModule().warn(Resources.getString("ModuleExtension.wrong_module_version", Name, version, GameModule.getGameModule().getGameVersion(), GameModule.getGameModule().getGameName()));
            //		}
            //	}
            //	else if (VASSAL_VERSION_CREATED.Equals(key))
            //	{
            //		vassalVersionCreated = ((System.String) value_Renamed);
            //		System.String runningVersion = Info.Version;
            //		if (Info.compareVersions(vassalVersionCreated, runningVersion) > 0)
            //		{
            //			GameModule.getGameModule().warn(Resources.getString("ModuleExtension.wrong_vassal_version", Name, (System.String) value_Renamed, runningVersion));
            //		}
            //	}
            //	else if (VERSION.Equals(key))
            //	{
            //		version = ((System.String) value_Renamed);
            //	}
            //	else if (UNIVERSAL.Equals(key))
            //	{
            //		if (value_Renamed is System.String)
            //		{
            //			//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.lang.Boolean.valueOf' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
            //			value_Renamed = System.Boolean.Parse((System.String) value_Renamed);
            //		}
            //		universal = ((System.Boolean) value_Renamed);
            //	}
            //	else if (NEXT_PIECESLOT_ID.Equals(key))
            //	{
            //		try
            //		{
            //			nextGpId = System.Int32.Parse((System.String) value_Renamed);
            //		}
            //		// FIXME: review error message
            //		catch (System.FormatException e)
            //		{
            //		}
            //	}
            //	else if (EXTENSION_ID.Equals(key))
            //	{
            //		extensionId = ((System.String) value_Renamed);
            //	}
            //	else if (DESCRIPTION.Equals(key))
            //	{
            //		description = ((System.String) value_Renamed);
            //	}
        }

    /// <summary> Generate a new Unique GamePiece Id</summary>
    public virtual System.String generateGpId()
		{
			return extensionId + ":" + nextGpId++;
		}
		
		public override void  addTo(Buildable parent)
		{
		}
		
		public virtual System.String buildString()
		{
			System.Xml.XmlDocument doc = Builder.createNewDocument();
			doc.AppendChild(getBuildElement(doc));
			return Builder.ToString(doc);
		}
		
		protected internal virtual void  write(bool saveAs)
		{
			vassalVersionCreated = Info.Version;
			if (archive is ArchiveWriter)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ArchiveWriter w = (ArchiveWriter) archive;
				
				try
				{
					(new ExtensionMetaData(this)).save(w);
				}
				// FIXME: review error message
				catch (System.IO.IOException e)
				{
					logger.error("", e);
				}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'save '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String save = buildString();
				//UPGRADE_TODO: Method 'java.lang.String.getBytes' was converted to 'System.Text.Encoding.GetEncoding(string).GetBytes(string)' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangStringgetBytes_javalangString'"
				w.addFile(GameModule.BUILDFILE, new System.IO.MemoryStream(SupportClass.ToByteArray(SupportClass.ToSByteArray(System.Text.Encoding.GetEncoding("UTF-8").GetBytes(save))))); //$NON-NLS-1$
				
				if (saveAs)
					w.saveAs(true);
				else
					w.save(true);
				
				lastSave = save;
			}
			else
			{
				throw new System.IO.IOException("Read-only extension");
			}
		}
		
		public virtual void  save()
		{
			write(false);
		}
		
		public virtual void  saveAs()
		{
			write(true);
		}
		
		public virtual void  remove(ExtensionElement el)
		{
			//buildComponents.remove(el);
		}
		
		public virtual SupportClass.ActionSupport getEditAction(System.Windows.Forms.Form d)
		{
			//if (editAction == null)
			//{
			//	d.Name = Name;
			//	//UPGRADE_NOTE: Final was removed from the declaration of 'config '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//	StringConfigurer config = new StringConfigurer(VERSION, "Version:  ", version);
			//	//UPGRADE_ISSUE: Method 'javax.swing.JDialog.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJDialogsetLayout_javaawtLayoutManager'"
			//	//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//	//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//	d.setLayout(new BoxLayout(((System.Windows.Forms.ContainerControl) d), BoxLayout.Y_AXIS));
			//	//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			//	d.Controls.Add(config.Controls);
				
			//	//UPGRADE_NOTE: Final was removed from the declaration of 'dconfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//	StringConfigurer dconfig = new StringConfigurer(DESCRIPTION, "Description:  ", description);
			//	//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			//	d.Controls.Add(dconfig.Controls);
				
			//	/*
			//	* The Extension id should not normally be changed once saved games
			//	* have been created. Display a dialog with warnings.
			//	*/
			//	//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//	//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//	Box idBox = Box.createHorizontalBox();
			//	System.Windows.Forms.Label temp_label2;
			//	temp_label2 = new System.Windows.Forms.Label();
			//	temp_label2.Text = "Extension Id: ";
			//	//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			//	System.Windows.Forms.Control temp_Control;
			//	temp_Control = temp_label2;
			//	idBox.Controls.Add(temp_Control);
			//	//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			//	idDisplay = new System.Windows.Forms.TextBox();
			//	//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
			//	idDisplay.Text = extensionId;
			//	idDisplay.ReadOnly = !false;
			//	//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			//	idBox.Controls.Add(idDisplay);
			//	System.Windows.Forms.Button change = SupportClass.ButtonSupport.CreateStandardButton("Change");
			//	change.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			//	SupportClass.CommandManager.CheckCommand(change);
			//	//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			//	idBox.Controls.Add(change);
			//	//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			//	d.Controls.Add(idBox);
				
			//	//UPGRADE_NOTE: Final was removed from the declaration of 'uconfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//	BooleanConfigurer uconfig = new BooleanConfigurer(UNIVERSAL, "Allow loading with any module?", universal);
			//	//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			//	d.Controls.Add(uconfig.Controls);
				
			//	//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//	//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//	Box b = Box.createHorizontalBox();
			//	System.Windows.Forms.Button ok = SupportClass.ButtonSupport.CreateStandardButton("Save");
			//	ok.Click += new System.EventHandler(new AnonymousClassActionListener1(config, dconfig, uconfig, d, this).actionPerformed);
			//	SupportClass.CommandManager.CheckCommand(ok);
			//	//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			//	b.Controls.Add(ok);
			//	System.Windows.Forms.Button cancel = SupportClass.ButtonSupport.CreateStandardButton("Cancel");
			//	cancel.Click += new System.EventHandler(new AnonymousClassActionListener2(d, this).actionPerformed);
			//	SupportClass.CommandManager.CheckCommand(cancel);
			//	//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			//	b.Controls.Add(cancel);
			//	//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			//	d.Controls.Add(b);
			//	//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			//	d.pack();
			//	System.Windows.Forms.Control generatedAux14 = d.Parent;
			//	//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			//	d.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			//	editAction = new AnonymousClassAbstractAction(d, this);
			//	GetType();
			//	//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			//	System.Uri iconURL = new System.Uri(System.IO.Path.GetFullPath("/images/Edit16.gif")); //$NON-NLS-1$
			//	if (iconURL != null)
			//	{
			//		//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
			//		//UPGRADE_ISSUE: Field 'javax.swing.Action.SMALL_ICON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionSMALL_ICON_f'"
			//		//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
			//		editAction.putValue(Action.SMALL_ICON, new ImageIcon(iconURL));
			//	}
			//	else
			//	{
			//		//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
			//		//UPGRADE_ISSUE: Field 'javax.swing.Action.NAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionNAME_f'"
			//		editAction.putValue(Action.NAME, "Edit");
			//	}
			//	//UPGRADE_ISSUE: Method 'javax.swing.AbstractAction.putValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractActionputValue_javalangString_javalangObject'"
			//	//UPGRADE_ISSUE: Field 'javax.swing.Action.SHORT_DESCRIPTION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingActionSHORT_DESCRIPTION_f'"
			//	editAction.putValue(Action.SHORT_DESCRIPTION, "Extension Properties");
			//}
			return editAction;
		}

        /// <summary> A command that verifies that a certain extension has been loaded</summary>
        public class RegCmd : Command
        {
            private void InitBlock()
            {
                //if (ext.Name.equals(name))
                //{
                //    containsExtension = true;
                //    if (Info.compareVersions(ext.Version, version) > 0)
                //    {
                //        GameModule.getGameModule().warn(getVersionErrorMsg(ext.Version));
                //    }
                //    break;
                //}
                //GameModule.getGameModule().warn(Enclosing_Instance.NotLoadedMsg);
            }
            virtual public System.String Name
            {
                get
                {
                    return name;
                }

            }
            virtual public System.String Version
            {
                get
                {
                    return version;
                }

            }
            private System.String name;
            private System.String version;

            public RegCmd(System.String name, System.String version)
            {
                this.name = name;
                this.version = version;
            }

            //UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
            public override void executeCommand()
            {
                //bool containsExtension = false;
                ////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
                //foreach (ModuleExtension ext in GameModule.getGameModule().getComponentsOf(ModuleExtension.

            }
            ////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
            //class))
            ////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
            //if(!containsExtension)

            protected internal virtual System.String getVersionErrorMsg(System.String v)
            {
                //return Resources.getString("ModuleExtension.wrong_extension_version", version, name, v);
                return null;
            }

            public override Command myUndoCommand()
            {
                return null;
            }
        }
        //	static ModuleExtension()
        //	{
        //		logger = LoggerFactory.getLogger(typeof(ModuleExtension));
        //	}
        //}
    }
#else
    public class ModuleExtension : AbstractBuildable, GpIdSupport
    {
        public static Configurable DESCRIPTION { get; internal set; }

        public override string[] AttributeNames => throw new NotImplementedException();

        public int NextGpId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Version { get; internal set; }
        public bool Universal { get; internal set; }
        public string Description { get; internal set; }

        public override void addTo(Buildable param1)
        {
            throw new NotImplementedException();
        }

        public string generateGpId()
        {
            throw new NotImplementedException();
        }

        public override string getAttributeValueString(string key)
        {
            throw new NotImplementedException();
        }

        public override void SetAttribute(string key, object value_Renamed)
        {
            throw new NotImplementedException();
        }
    }
#endif
}