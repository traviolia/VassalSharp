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

using Microsoft.Extensions.Logging;

using System;
using System.IO;

using GameModule = VassalSharp.build.GameModule;
using IOUtils = VassalSharp.tools.io.IOUtils;
using ZipEntry = VassalSharp.tools.io.ZipEntry;
using ZipFile = VassalSharp.tools.io.ZipFile;

namespace VassalSharp.build.module.metadata
{
	
	/// <summary> 
	/// Class representing the metadata for a Module or an Extension. Modules
	/// and extensions can't be differentiated until either the metadata or the
	/// buildfile is parsed, so they share the same metadata structure.
	/// 
	/// </summary>
	/// <author>  Brent Easton
	/// </author>
	/// <since> 3.1.0
	/// 
	/// </since>
	public class ModuleMetaData : AbstractMetaData
	{
		virtual public string Name
		{
			get
			{
				return nameAttr == null?"":nameAttr.getValue();
			}
			
		}
		virtual public string LocalizedName
		{
			get
			{
				return nameAttr == null?"":nameAttr.getLocalizedValue();
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
		
		private static readonly ILogger logger;

		public const string ZIP_ENTRY_NAME = "moduledata";
		public const string DATA_VERSION = "1";
		
		protected internal Attribute_Renamed_Class nameAttr;
		
		public ModuleMetaData(ZipFile zip)
		{
			read(zip);
		}
		
		public ModuleMetaData(GameModule module):base()
		{
			nameAttr = new Attribute_Renamed_Class(module, GameModule.MODULE_NAME);
			setDescription(new Attribute_Renamed_Class(module, GameModule.DESCRIPTION));
			this.Version = module.getGameVersion();
		}
		
		public ModuleMetaData(string name, string version):base()
		{
			nameAttr = new Attribute_Renamed_Class(GameModule.MODULE_NAME, name);
			Version = version;
		}
		
		/// <summary> Add elements specific to a ModuleMetaData
		/// 
		/// </summary>
		/// <param name="doc">Document
		/// </param>
		/// <param name="root">Root element
		/// </param>
		protected internal override void  AddElements(System.Xml.XmlDocument doc, System.Xml.XmlElement root)
		{
			nameAttr.generateXML(doc, root, NAME_ELEMENT);
		}
		
		/// <summary> Read and validate a Module file.
		/// - Check it has a Zip Entry named buildfile
		/// - If it has a metadata file, read and parse it.
		/// 
		/// </summary>
		/// <param name="file">Module File
		/// </param>
		//UPGRADE_ISSUE: Class 'java.util.zip.ZipFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
		public virtual void  read(ZipFile zip)
		{
			version = "";

			//try
			//{
			//	// Try to parse the metadata. Failure is not catastrophic, we can
			//	// treat it like an old-style module with no metadata and parse
			//	// the first lines of the buildFile
			//	XmlSaxDefaultHandler handler = null;

			//	//UPGRADE_ISSUE: Class 'java.util.zip.ZipEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipEntry'"
			//	//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.getEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
			//	ZipEntry data = zip.getEntry(ZIP_ENTRY_NAME);
			//	if (data == null)
			//	{
			//		//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.getEntry' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
			//		data = zip.getEntry(GameModule.BUILDFILE);
			//		if (data == null)
			//			return;

			//		handler = new ModuleBuildFileXMLHandler(this);
			//	}
			//	else
			//	{
			//		handler = new MetadataXMLHandler(this);
			//	}

			//	System.IO.BufferedStream in_Renamed = null;
			//try
			//{
			//	//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.getInputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
			//	in_Renamed = new System.IO.BufferedStream(zip.getInputStream(data));

			//	lock (parser)
			//	{
			//		parser.setContentHandler(handler);
			//		parser.setDTDHandler(handler);
			//		parser.setEntityResolver(handler);
			//		parser.setErrorHandler(handler);
			//		parser.parse(new XmlSourceSupport(in_Renamed));
			//	}

			//	in_Renamed.Close();
			//}
			//finally
			//{
			//	IOUtils.closeQuietly(in_Renamed);
			//}

			////UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.close' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
			//zip.close();
			//}
			//catch (System.IO.IOException e)
			//{
			//	logger.error("", e);
			//}
			//catch (SAXEndException e)
			//{
			//	// Indicates End of module/extension parsing. not an error.
			//}
			////UPGRADE_TODO: Class 'org.xml.sax.SAXException' was converted to 'System.Xml.XmlException' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//catch (System.Xml.XmlException e)
			//{
			//	logger.error("", e);
			//}
			//finally
			//{
			//	//IOUtils.closeQuietly(zip);
			//}
		}

		////UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MetadataXMLHandler' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		///// <summary> XML Handler for parsing a Module/Extension metadata file</summary>
		//private class MetadataXMLHandler : XMLHandler
		//{
		//	public MetadataXMLHandler(ModuleMetaData enclosingInstance)
		//	{
		//		InitBlock(enclosingInstance);
		//	}
		//	private void InitBlock(ModuleMetaData enclosingInstance)
		//	{
		//		this.enclosingInstance = enclosingInstance;
		//	}
		//	private ModuleMetaData enclosingInstance;
		//	public ModuleMetaData Enclosing_Instance
		//	{
		//		get
		//		{
		//			return enclosingInstance;
		//		}

		//	}

		//	public override void endElement(string uri, string localName, string qName)
		//	{
		//		// handle all of the elements which have CDATA here
		//		if (VassalSharp.build.module.metadata.AbstractMetaData.NAME_ELEMENT.Equals(qName))
		//		{
		//			if (Enclosing_Instance.nameAttr == null)
		//			{
		//				Enclosing_Instance.nameAttr = new Attribute_Renamed_Class(VassalSharp.build.module.metadata.AbstractMetaData.NAME_ELEMENT, accumulator.ToString().trim());
		//			}
		//			else
		//			{
		//				Enclosing_Instance.nameAttr.addTranslation(language, accumulator.ToString().trim());
		//			}
		//		}
		//		else
		//		{
		//			base.endElement(uri, localName, qName);
		//		}
		//	}
		//}

		////UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ModuleBuildFileXMLHandler' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		///// <summary> XML Handle for parsing a Module buildFile. Used to read minimal data from
		///// modules saved prior to 3.1.0.
		///// </summary>
		//private class ModuleBuildFileXMLHandler : BuildFileXMLHandler
		//{
		//	public ModuleBuildFileXMLHandler(ModuleMetaData enclosingInstance)
		//	{
		//		InitBlock(enclosingInstance);
		//	}
		//	private void InitBlock(ModuleMetaData enclosingInstance)
		//	{
		//		this.enclosingInstance = enclosingInstance;
		//	}
		//	private ModuleMetaData enclosingInstance;
		//	public ModuleMetaData Enclosing_Instance
		//	{
		//		get
		//		{
		//			return enclosingInstance;
		//		}

		//	}

		//	public override void startElement(string uri, string localName, string qName, SaxAttributesSupport attrs)
		//	{
		//		base.startElement(uri, localName, qName, attrs);

		//		// handle element attributes we care about
		//		if (VassalSharp.build.module.metadata.AbstractMetaData.BUILDFILE_MODULE_ELEMENT1.Equals(qName) || VassalSharp.build.module.metadata.AbstractMetaData.BUILDFILE_MODULE_ELEMENT2.Equals(qName))
		//		{
		//			Enclosing_Instance.nameAttr = new Attribute_Renamed_Class(VassalSharp.build.module.metadata.AbstractMetaData.NAME_ELEMENT, getAttr(attrs, VassalSharp.build.module.metadata.AbstractMetaData.NAME_ATTR));
		//			Enclosing_Instance.Version = getAttr(attrs, VassalSharp.build.module.metadata.AbstractMetaData.VERSION_ATTR);
		//			Enclosing_Instance.VassalVersion = getAttr(attrs, VassalSharp.build.module.metadata.AbstractMetaData.VASSAL_VERSION_ATTR);
		//			Enclosing_Instance.setDescription(getAttr(attrs, VassalSharp.build.module.metadata.AbstractMetaData.DESCRIPTION_ATTR));
		//			throw new SAXEndException();
		//		}
		//	}
		//}
	}
}