/*
* $Id$
*
* Copyright (c) 2008 by Brent Easton and Joel Uckelman
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
using GameModule = VassalSharp.build.GameModule;
using Resources = VassalSharp.i18n.Resources;
using FileArchive = VassalSharp.tools.io.FileArchive;
using IOUtils = VassalSharp.tools.io.IOUtils;
#else
using GameModule = VassalSharp.build.GameModule;
using Resources = VassalSharp.i18n.Resources;
using FileArchive = VassalSharp.tools.io.FileArchive;
using IOUtils = VassalSharp.tools.io.IOUtils;
using ZipFile = VassalSharp.tools.io.ZipFile;
#endif

namespace VassalSharp.build.module.metadata
{
#if NEVER_DEFINED
	/// <summary> Class representing the metadata for a Save Game/Log File. Details
	/// about the module this savegame was created with are saved in a
	/// seperate moduledata file in the saved game zip.
	/// 
	/// </summary>
	/// <author>  Brent Easton
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class SaveMetaData:AbstractMetaData
	{
		virtual public string ModuleName
		{
			get
			{
				return moduleData == null?"":moduleData.Name;
			}
			
		}
		virtual public string ModuleVersion
		{
			get
			{
				return moduleData == null?"":moduleData.Version;
			}
			
		}
		virtual public ModuleMetaData ModuleData
		{
			get
			{
				return moduleData;
			}
			
		}
		override public string ZipEntryName
		{
			get
			{
				return ZIP_ENTRY_NAME;
			}
			
		}
		override public string MetaDataVersion
		{
			get
			{
				return DATA_VERSION;
			}
			
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.build.module.metadata.SaveMetaData'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		public const string ZIP_ENTRY_NAME = "savedata";
		public const string DATA_VERSION = "1";
		public const string PROMPT_LOG_COMMENT = "promptLogComment";
		
		protected internal ModuleMetaData moduleData;
		
		public SaveMetaData():base()
		{
			
			string comments = "";
			if ((System.Boolean) GameModule.getGameModule().getPrefs().getValue(PROMPT_LOG_COMMENT))
			{
				comments = ((string) JOptionPane.showInputDialog(GameModule.getGameModule().getFrame(), Resources.getString("BasicLogger.enter_comments"), Resources.getString("BasicLogger.log_file_comments"), (int) System.Windows.Forms.MessageBoxIcon.None, null, null, ""));
				
				setDescription(new Attribute(DESCRIPTION_ELEMENT, comments));
			}
		}
		
		//UPGRADE_ISSUE: Class 'java.util.zip.ZipFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
		public SaveMetaData(ZipFile zip)
		{
			read(zip);
		}
		
		/// <summary> Write Save Game metadata to the specified Archive</summary>
		/// <param name="archive">Save game Archive
		/// </param>
		/// <throws>  IOException If anything goes wrong </throws>
		public override void  save(FileArchive archive)
		{
			base.save(archive);
			
			// Also save a copy of the current module metadata in the save file. Copy
			// module metadata from the module archive as it will contain full i18n
			// information.
			copyModuleMetadata(archive);
		}
		
		/// <summary> Add Elements specific to SaveMetaData</summary>
		protected internal override void  addElements(System.Xml.XmlDocument doc, System.Xml.XmlElement root)
		{
			return ;
		}
		
		/// <summary> Read and validate a Saved Game/Log file.
		/// Check that it has a Zip Entry named savedgame.
		/// If it has a metadata file, read and parse it.
		/// 
		/// </summary>
		/// <param name="file">Saved Game File
		/// </param>
		//UPGRADE_ISSUE: Class 'java.util.zip.ZipFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
		public virtual void  read(ZipFile zip)
		{
			try
			{
				// Try to parse the metadata. Failure is not catastrophic, we can
				// treat it like an old-style save with no metadata.
				//UPGRADE_NOTE: Final was removed from the declaration of 'data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Class 'java.util.zip.ZipEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipEntry'"
				//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.getEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
				ZipEntry data = zip.getEntry(ZipEntryName);
				if (data == null)
					return ;
				
				// set up the handler
				//UPGRADE_NOTE: Final was removed from the declaration of 'handler '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				XMLHandler handler = new XMLHandler();
				
				// parse! parse!
				System.IO.BufferedStream in_Renamed = null;
				try
				{
					//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.getInputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
					in_Renamed = new System.IO.BufferedStream(zip.getInputStream(data));
					
					lock (parser)
					{
						parser.setContentHandler(handler);
						parser.setDTDHandler(handler);
						parser.setEntityResolver(handler);
						parser.setErrorHandler(handler);
						parser.parse(new XmlSourceSupport(in_Renamed));
					}
					
					in_Renamed.Close();
				}
				finally
				{
					IOUtils.closeQuietly(in_Renamed);
				}
				
				// read the matching Module data
				moduleData = new ModuleMetaData(zip);
				
				//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.close' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
				zip.close();
			}
			catch (System.IO.IOException e)
			{
				logger.error("", e);
			}
			//UPGRADE_TODO: Class 'org.xml.sax.SAXException' was converted to 'System.Xml.XmlException' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			catch (System.Xml.XmlException e)
			{
				logger.error("", e);
			}
			finally
			{
				IOUtils.closeQuietly(zip);
			}
		}
		static SaveMetaData()
		{
			logger = LoggerFactory.getLogger(typeof(SaveMetaData));
		}
	}

#else

	/// <summary> Class representing the metadata for a Save Game/Log File. Details
	/// about the module this savegame was created with are saved in a
	/// seperate moduledata file in the saved game zip.
	/// 
	/// </summary>
	/// <author>  Brent Easton
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class SaveMetaData : AbstractMetaData
	{
		virtual public string ModuleName
		{
			get
			{
				return null;
			}

		}
		virtual public string ModuleVersion
		{
			get
			{
				return null;
			}

		}
		virtual public ModuleMetaData ModuleData
		{
			get
			{
				return null;
			}

		}
		override public string ZipEntryName
		{
			get
			{
				return null;
			}

		}
		override public string MetaDataVersion
		{
			get
			{
				return null;
			}

		}

		public const string ZIP_ENTRY_NAME = "savedata";
		public const string DATA_VERSION = "1";
		public const string PROMPT_LOG_COMMENT = "promptLogComment";


		public SaveMetaData() : base()
		{
		}

		public SaveMetaData(ZipFile zip)
		{
		}

		/// <summary> Write Save Game metadata to the specified Archive</summary>
		/// <param name="archive">Save game Archive
		/// </param>
		/// <throws>  IOException If anything goes wrong </throws>
		public override void save(FileArchive archive)
		{
		}

		/// <summary> Add Elements specific to SaveMetaData</summary>
		protected internal override void AddElements(System.Xml.XmlDocument doc, System.Xml.XmlElement root)
		{
			return;
		}

		/// <summary> Read and validate a Saved Game/Log file.
		/// Check that it has a Zip Entry named savedgame.
		/// If it has a metadata file, read and parse it.
		/// 
		/// </summary>
		/// <param name="file">Saved Game File
		/// </param>
		public virtual void read(ZipFile zip)
		{
		}
	}
#endif
}