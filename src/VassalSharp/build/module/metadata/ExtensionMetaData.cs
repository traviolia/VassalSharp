/* $Id$
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
using System;

using GameModule = VassalSharp.build.GameModule;
using ModuleExtension = VassalSharp.build.module.ModuleExtension;
using ArchiveWriter = VassalSharp.tools.ArchiveWriter;
using IOUtils = VassalSharp.tools.io.IOUtils;
using ZipFile = VassalSharp.tools.io.ZipFile;
using ZipEntry = VassalSharp.tools.io.ZipEntry;
using System.IO;

namespace VassalSharp.build.module.metadata
{
	public class ExtensionMetaData : AbstractMetaData
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

		public const string ZIP_ENTRY_NAME = "extensiondata";
		public const string DATA_VERSION = "1";
		
		protected internal const string UNIVERSAL_ELEMENT = "universal";
		protected internal const string UNIVERSAL_ATTR = "anyModule";
		
		protected internal ModuleMetaData moduleData;
		protected internal bool universal;
		
		/// <summary>
		/// Build an ExtensionMetaData for the given extension
		/// </summary>
		/// <param name="ext">Extension
		/// </param>
		public ExtensionMetaData(ModuleExtension ext):base()
		{
			Version = ext.Version;
			setDescription(new Attribute_Renamed_Class(ModuleExtension.DESCRIPTION, ext.Description));
			universal = ext.Universal;
		}
		
		/// <summary> Read Extension metadata from specified zip archive
		/// 
		/// </summary>
		/// <param name="zip">the archive
		/// </param>
		//UPGRADE_ISSUE: Class 'java.util.zip.ZipFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
		public ExtensionMetaData(ZipFile zip)
		{
			read(zip);
		}
		
		/// <summary> Write Extension metadata to the specified Archive</summary>
		/// <param name="archive">Save game Archive
		/// </param>
		/// <throws>  IOException If anything goes wrong </throws>
		public override void  save(ArchiveWriter archive)
		{
			base.save(archive);
			
			// Also save a copy of the current module metadata in the save file. Copy
			// module metadata from the module archive as it will contain full i18n
			// information.
			copyModuleMetadata(archive);
		}
		
		/// <summary> Add elements specific to an ExtensionMetaData
		/// 
		/// </summary>
		/// <param name="doc">Document
		/// </param>
		/// <param name="root">Root element
		/// </param>
		protected internal override void  AddElements(System.Xml.XmlDocument doc, System.Xml.XmlElement root)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Xml.XmlElement e = doc.CreateElement(UNIVERSAL_ELEMENT);
			e.AppendChild(doc.CreateTextNode(System.Convert.ToString(universal)));
			root.AppendChild(e);
		}
		
		/// <summary> Read and validate an Extension file.
		/// - Check it has a Zip Entry named buildfile
		/// - If it has a metadata file, read and parse it.
		/// 
		/// </summary>
		/// <param name="file">Module File
		/// </param>
		//UPGRADE_ISSUE: Class 'java.util.zip.ZipFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
		public virtual void  read(ZipFile zip)
		{
			//try
			//{
			//	// Try to parse the metadata. Failure is not catastrophic, we can
			//	// treat it like an old-style module with no metadata and parse
			//	// the first lines of the buildFile.
			//	XmlSaxDefaultHandler handler = null;

			//	ZipEntry data = zip.getEntry(ZipEntryName);
			//	if (data == null)
			//	{
			//		data = zip.getEntry(GameModule.BUILDFILE);
			//		handler = new ExtensionBuildFileXMLHandler(this);
			//	}
			//	else
			//	{
			//		handler = new MetadataXMLHandler(this);
			//	}

   //             // parse! parse!
   //             System.IO.BufferedStream in_Renamed = null;
   //             try
   //             {
   //                 in_Renamed = new System.IO.BufferedStream(zip.getInputStream(data));

   //                 lock (parser)
   //                 {
   //                     parser.setContentHandler(handler);
   //                     parser.setDTDHandler(handler);
   //                     parser.setEntityResolver(handler);
   //                     parser.setErrorHandler(handler);
   //                     parser.parse(new XmlSourceSupport(in_Renamed));
   //                 }

   //                 in_Renamed.Close();
   //             }
   //             finally
   //             {
   //                 IOUtils.closeQuietly(in_Renamed);
   //             }

   //             // read the matching Module data. A basic moduledata may have been
   //             // built when reading the buildFile, overwrite if we find a real
   //             // module metadata file
   //             //UPGRADE_NOTE: Final was removed from the declaration of 'buildFileModuleData '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
   //             ModuleMetaData buildFileModuleData = moduleData;
			//	moduleData = new ModuleMetaData(zip);
			//	if (moduleData == null)
			//	{
			//		moduleData = buildFileModuleData;
			//	}
				
			//	//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.close' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
			//	zip.close();
			//}
			//catch (System.IO.IOException e)
			//{
			//	logger.error("", e);
			//}
   //         catch (SAXEndException e)
   //         {
   //             // Indicates End of module/extension parsing. not an error.
   //         }
   //         //UPGRADE_TODO: Class 'org.xml.sax.SAXException' was converted to 'System.Xml.XmlException' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
   //         catch (System.Xml.XmlException e)
			//{
			//	logger.error("", e);
			//}
			//finally
			//{
			//	//IOUtils.closeQuietly(zip);
			//}
		}

        //UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MetadataXMLHandler' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
        /// <summary> XML Handler for parsing an Extension metadata file</summary>
        //private class MetadataXMLHandler:XMLHandler
        //{
        //	public MetadataXMLHandler(ExtensionMetaData enclosingInstance)
        //	{
        //		InitBlock(enclosingInstance);
        //	}
        //	private void  InitBlock(ExtensionMetaData enclosingInstance)
        //	{
        //		this.enclosingInstance = enclosingInstance;
        //	}
        //	private ExtensionMetaData enclosingInstance;
        //	public ExtensionMetaData Enclosing_Instance
        //	{
        //		get
        //		{
        //			return enclosingInstance;
        //		}

        //	}
        //	public override void  endElement(string uri, string localName, string qName)
        //	{
        //		// handle all of the elements which have CDATA here
        //		if (VassalSharp.build.module.metadata.ExtensionMetaData.UNIVERSAL_ELEMENT.Equals(qName))
        //		{
        //			Enclosing_Instance.universal = "true".Equals(accumulator.ToString().trim());
        //		}
        //		else
        //		{
        //			base.endElement(uri, localName, qName);
        //		}
        //	}
        //}

        //UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ExtensionBuildFileXMLHandler' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
        /// <summary> XML Handle for parsing an extension buildFile. Used to read minimal data from
        /// extensions saved prior to 3.1.0.
        /// </summary>
        //private class ExtensionBuildFileXMLHandler : BuildFileXMLHandler
        //{
        //	public ExtensionBuildFileXMLHandler(ExtensionMetaData enclosingInstance)
        //	{
        //		InitBlock(enclosingInstance);
        //	}
        //	private void  InitBlock(ExtensionMetaData enclosingInstance)
        //	{
        //		this.enclosingInstance = enclosingInstance;
        //	}
        //	private ExtensionMetaData enclosingInstance;
        //	public ExtensionMetaData Enclosing_Instance
        //	{
        //		get
        //		{
        //			return enclosingInstance;
        //		}

        //	}

        //	public override void  startElement(string uri, string localName, string qName, SaxAttributesSupport attrs)
        //	{
        //		base.startElement(uri, localName, qName, attrs);

        //		// handle element attributes we care about
        //		if (VassalSharp.build.module.metadata.AbstractMetaData.BUILDFILE_EXTENSION_ELEMENT.Equals(qName))
        //		{
        //			Enclosing_Instance.Version = getAttr(attrs, VassalSharp.build.module.metadata.AbstractMetaData.VERSION_ATTR);
        //			Enclosing_Instance.VassalVersion = getAttr(attrs, VassalSharp.build.module.metadata.AbstractMetaData.VASSAL_VERSION_ATTR);
        //			Enclosing_Instance.setDescription(getAttr(attrs, VassalSharp.build.module.metadata.AbstractMetaData.DESCRIPTION_ATTR));
        //			Enclosing_Instance.universal = "true".Equals(getAttr(attrs, VassalSharp.build.module.metadata.ExtensionMetaData.UNIVERSAL_ATTR));
        //			// Build a basic module metadata in case this is an older extension
        //			// with no metadata from the originating module
        //			//UPGRADE_NOTE: Final was removed from the declaration of 'moduleName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        //			string moduleName = getAttr(attrs, VassalSharp.build.module.metadata.AbstractMetaData.MODULE_NAME_ATTR);
        //			//UPGRADE_NOTE: Final was removed from the declaration of 'moduleVersion '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        //			string moduleVersion = getAttr(attrs, VassalSharp.build.module.metadata.AbstractMetaData.MODULE_VERSION_ATTR);
        //			Enclosing_Instance.moduleData = new ModuleMetaData(moduleName, moduleVersion);
        //			//throw new SAXEndException();
        //		}
        //	}
        //}
    }
}