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
#if NEVER_DEFINED
using System;
//using Info = VassalSharp.Info;
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
using Translation = VassalSharp.i18n.Translation;
////using ArchiveWriter = VassalSharp.tools.ArchiveWriter;
////using ErrorDialog = VassalSharp.tools.ErrorDialog;
////using FastByteArrayOutputStream = VassalSharp.tools.io.FastByteArrayOutputStream;
//using FileArchive = VassalSharp.tools.io.FileArchive;
//using IOUtils = VassalSharp.tools.io.IOUtils;
using System.Xml;
using System.Collections.Generic;
#else
using System;
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
using Translation = VassalSharp.i18n.Translation;
using ArchiveWriter = VassalSharp.tools.ArchiveWriter;
using FileArchive = VassalSharp.tools.io.FileArchive;
using IOUtils = VassalSharp.tools.io.IOUtils;
using System.Xml;
using System.Collections.Generic;
#endif 

namespace VassalSharp.build.module.metadata
{

#if NEVER_DEFINED
	/// <summary> 
	/// Base class representing the metadata for a Saved Game, Module or Extension.
	/// 
	/// </summary>
	/// <author>Brent Easton</author>
	/// <since> 3.1.0</since>
	public abstract class AbstractMetaData
	{
		virtual public string Version
		{
			get
			{
				return version == null ? "" : version;
			}

			set
			{
				version = value;
			}

		}
		virtual public string VassalVersion
		{
			get
			{
				return vassalVersion == null ? "" : vassalVersion;
			}

			set
			{
				vassalVersion = value;
			}

		}
		virtual public string LocalizedDescription
		{
			get
			{
				return descriptionAttr == null ? "" : descriptionAttr.getLocalizedValue();
			}

		}
		/// <summary> Return the Entry name for the metatdata file
		/// 
		/// </summary>
		/// <returns> Zip Entry name
		/// </returns>
		public abstract string ZipEntryName { get; }

		/// <summary> Return the version of the metadata structure
		/// 
		/// </summary>
		/// <returns> version
		/// </returns>
		public abstract string MetaDataVersion { get; }


		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.build.module.metadata.AbstractMetaData'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		//private static readonly Logger logger;

		protected internal const string TRUE = "true";
		protected internal const string FALSE = "false";

		protected internal const string NAME_ATTR = "name";
		protected internal const string VERSION_ATTR = "version";
		protected internal const string VASSAL_VERSION_ATTR = "vassalVersion";
		protected internal const string DESCRIPTION_ATTR = "description";
		protected internal const string EXTENSION_ATTR = "extension";
		protected internal const string MODULE_NAME_ATTR = "moduleName";
		protected internal const string MODULE_VERSION_ATTR = "moduleVersion";
		protected internal const string LANG_ATTR = "lang";

		protected internal const string ROOT_ELEMENT = "data";
		protected internal const string VERSION_ELEMENT = "version";
		protected internal const string VASSAL_VERSION_ELEMENT = "VassalVersion";
		protected internal const string MODULE_NAME_ELEMENT = "moduleName";
		protected internal const string MODULE_VERSION_ELEMENT = "moduleVersion";
		protected internal const string DESCRIPTION_ELEMENT = "description";
		protected internal const string NAME_ELEMENT = "name";
		protected internal const string DATE_SAVED_ELEMENT = "dateSaved";

		protected internal const string BUILDFILE_MODULE_ELEMENT1 = "VassalSharp.launch.BasicModule";
		protected internal const string BUILDFILE_MODULE_ELEMENT2 = "VassalSharp.build.GameModule";
		protected internal const string BUILDFILE_EXTENSION_ELEMENT = "VassalSharp.build.module.ModuleExtension";

		protected internal string version;
		protected internal string vassalVersion;
		protected internal Attribute_Renamed_Class descriptionAttr;

		public AbstractMetaData()
		{
			VassalVersion = Info.Version;
		}

		public virtual void setDescription(Attribute_Renamed_Class desc)
		{
			descriptionAttr = desc;
		}

		public virtual void setDescription(string desc)
		{
			descriptionAttr = new Attribute_Renamed_Class(GameModule.DESCRIPTION, desc);
		}

		public virtual string getDescription()
		{
			return descriptionAttr == null ? "" : descriptionAttr.value_Renamed;
		}

#if NEVER_DEFINED

		public virtual void save(FileArchive archive)
		{
			System.IO.Stream out_Renamed = null;
			try
			{
				out_Renamed = archive.getOutputStream(ZipEntryName);
				save(out_Renamed);
				out_Renamed.Close();
			}
			finally
			{
				IOUtils.closeQuietly(out_Renamed);
			}
		}

		protected internal virtual void save(System.IO.Stream out_Renamed)
		{
			System.Xml.XmlDocument doc = null;
			System.Xml.XmlElement e = null;
			try
			{
				//UPGRADE_TODO: Class 'javax.xml.parsers.DocumentBuilder' was converted to 'System.Xml.XmlDocument' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxxmlparsersDocumentBuilder'"
				doc = new System.Xml.XmlDocument();

				//UPGRADE_NOTE: Final was removed from the declaration of 'root '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Xml.XmlElement root = doc.CreateElement(ROOT_ELEMENT);
				//UPGRADE_TODO: Method 'org.w3c.dom.Element.setAttribute' was converted to 'System.Xml.XmlElement.SetAttribute' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_orgw3cdomElementsetAttribute_javalangString_javalangString'"
				root.SetAttribute(VERSION_ATTR, MetaDataVersion);
				doc.AppendChild(root);

				if (Version != null)
				{
					e = doc.CreateElement(VERSION_ELEMENT);
					e.AppendChild(doc.CreateTextNode(Version));
					root.AppendChild(e);
				}

				if (VassalVersion != null)
				{
					e = doc.CreateElement(VASSAL_VERSION_ELEMENT);
					e.AppendChild(doc.CreateTextNode(VassalVersion));
					root.AppendChild(e);
				}

				e = doc.CreateElement(DATE_SAVED_ELEMENT);
				e.AppendChild(doc.CreateTextNode(System.Convert.ToString((System.DateTime.Now.Ticks - 621355968000000000) / 10000)));
				root.AppendChild(e);

				if (descriptionAttr != null)
				{
					descriptionAttr.generateXML(doc, root, DESCRIPTION_ELEMENT);
				}

				addElements(doc, root);
			}
			//UPGRADE_ISSUE: Class 'javax.xml.parsers.ParserConfigurationException' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxxmlparsersParserConfigurationException'"
			catch (ParserConfigurationException ex)
			{
				ErrorDialog.bug(ex);
				// FIXME: switch to IOException(Throwable) ctor in Java 1.6
				throw (System.IO.IOException)new System.IO.IOException().initCause(ex);
			}

			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'xformer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Method 'javax.xml.transform.TransformerFactory.newTransformer' was converted to 'SupportClass.TransformerSupport.NewTransform' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxxmltransformTransformerFactorynewTransformer'"
				TransformerSupport xformer = TransformerSupport.NewTransform(TransformerSupport.NewInstance());
				//UPGRADE_ISSUE: Method 'javax.xml.transform.Transformer.setOutputProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxxmltransformTransformersetOutputProperty_javalangString_javalangString'"
				//UPGRADE_ISSUE: Field 'javax.xml.transform.OutputKeys.INDENT' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxxmltransformOutputKeys'"
				xformer.setOutputProperty(OutputKeys.INDENT, "yes");
				//UPGRADE_ISSUE: Method 'javax.xml.transform.Transformer.setOutputProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxxmltransformTransformersetOutputProperty_javalangString_javalangString'"
				xformer.setOutputProperty("{http://xml.apache.org/xslt}indent-amount", "2");
				//UPGRADE_TODO: Class 'javax.xml.transform.dom.DOMSource' was converted to 'DomSourceSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxxmltransformdomDOMSource'"
				xformer.DoTransform(new DomSourceSupport(doc), new GenericResultSupport(out_Renamed));
			}
			catch (System.Xml.Xsl.XsltException ex)
			{
				ErrorDialog.bug(ex);
				// FIXME: switch to IOException(Throwable) ctor in Java 1.6
				throw (System.IO.IOException)new System.IO.IOException().initCause(ex);
			}
			//UPGRADE_ISSUE: Class 'javax.xml.transform.TransformerFactoryConfigurationError' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxxmltransformTransformerFactoryConfigurationError'"
			catch (TransformerFactoryConfigurationError ex)
			{
				ErrorDialog.bug(ex);
				// FIXME: switch to IOException(Throwable) ctor in Java 1.6
				throw (System.IO.IOException)new System.IO.IOException().initCause(ex);
			}
			catch (System.Xml.Xsl.XsltException ex)
			{
				// FIXME: switch to IOException(Throwable) ctor in Java 1.6
				throw (System.IO.IOException)new System.IO.IOException().initCause(ex);
			}
		}

		/// <summary> Write common metadata to the specified Archive. Call addElements to
		/// add elements specific to particular concrete subclasses.
		/// 
		/// </summary>
		/// <param name="archive">Extension Archive
		/// </param>
		/// <throws>  IOException </throws>
		/// <summary>           If anything goes wrong
		/// </summary>
		public virtual void save(ArchiveWriter archive)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'out '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FastByteArrayOutputStream out_Renamed = new FastByteArrayOutputStream();
			save(out_Renamed);
			archive.addFile(ZipEntryName, out_Renamed.toInputStream());
		}

		/// <summary> Copy the Module metatdata from the current module into the specified
		/// archive.
		/// 
		/// </summary>
		/// <param name="archive">Archive to copy into
		/// </param>
		/// <throws>  IOException </throws>
		public virtual void copyModuleMetadata(ArchiveWriter archive)
		{
			copyModuleMetadata(archive.Archive);
		}

		public virtual void copyModuleMetadata(FileArchive archive)
		{
			System.IO.BufferedStream in_Renamed = null;
			try
			{
				in_Renamed = new BufferedInputStream(GameModule.getGameModule().getDataArchive().getInputStream(ModuleMetaData.ZIP_ENTRY_NAME));
				archive.add(ModuleMetaData.ZIP_ENTRY_NAME, in_Renamed);
				in_Renamed.Close();
			}
			catch (System.IO.FileNotFoundException e)
			{
				// No Metatdata in source module, create a fresh copy
				new ModuleMetaData(GameModule.getGameModule()).save(archive);
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
#endif

		/// <summary> Add elements specific to a MetaData subclass
		/// 
		/// </summary>
		/// <param name="doc">Document
		/// </param>
		/// <param name="root">Root element
		/// </param>
		protected internal abstract void addElements(System.Xml.XmlDocument doc, System.Xml.XmlElement root);

		/// <summary>**********************************************************************
		/// Utility Exception class, used to cancel SAX parsing
		/// 
		/// </summary>
		//UPGRADE_TODO: Class 'org.xml.sax.SAXException' was converted to 'System.Xml.XmlException' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		[Serializable]
		internal class SAXEndException : System.Xml.XmlException
		{
			private const long serialVersionUID = 1L;
		}

		/// <summary>**********************************************************************
		/// Utility class representing a Configurable attribute and its translations
		/// 
		/// </summary>
		public class Attribute_Renamed_Class
		{
			protected internal string attributeName;
			protected internal string value_Renamed;

			protected Dictionary<string, string> translations = new Dictionary<string, string>();

			/// <summary> Build Attribute class based on atrribute value and translations
			/// available in the current module
			/// 
			/// </summary>
			/// <param name="c">Target configurable
			/// </param>
			/// <param name="name">Attribute name
			/// </param>
			public Attribute_Renamed_Class(Configurable target, string name)
			{
				attributeName = name;
				value_Renamed = target.getAttributeValueString(attributeName);
				string key = target.I18nData.FullPrefix;
				if (key.Length > 0)
					key += ".";
				key += attributeName;

				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				foreach (Translation t in  GameModule.getGameModule().getAllDescendantComponentsOf<Translation>())
				{
					addTranslation(t.getLanguageCode(), t.translate(key));
				}
			}

			public Attribute_Renamed_Class(string attributeName, string value_Renamed)
			{
				this.attributeName = attributeName;
				this.value_Renamed = value_Renamed;
			}

			public virtual void addTranslation(string language, string value_Renamed)
			{
				if (value_Renamed != null)
				{
					translations.Add(language, value_Renamed);
				}
			}

			/// <summary>
			/// Return the untranslated value of this attribute
			/// </summary>
			public String getValue()
			{
				return this.value_Renamed;
			}

			/**
			 * Return the value of this attribute translated into the local
			 * language
			 *
			 * @return translated value
			 */
			public String getLocalizedValue()
			{
				string lang = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
				translations.TryGetValue(lang, out string tx);
				return tx ?? getValue();
			}
			/// <summary> Output metadata XML for this attribute, including translations
			/// 
			/// </summary>
			/// <param name="doc">Root document
			/// </param>
			/// <param name="root">Parent element
			/// </param>
			/// <param name="prefix">Attribute prefix
			/// </param>
			public virtual void generateXML(System.Xml.XmlDocument doc, System.Xml.XmlElement root, string prefix)
			{

				if (value_Renamed == null)
				{
					return;
				}

				System.Xml.XmlElement e = null;

				e = doc.CreateElement(prefix);
				e.AppendChild(doc.CreateTextNode(value_Renamed));
				root.AppendChild(e);

				foreach (KeyValuePair<string,string> en in translations)
				{
					e = doc.CreateElement(prefix);
					e.SetAttribute(LANG_ATTR, en.Value);
					e.AppendChild(doc.CreateTextNode(en.Key));
					root.AppendChild(e);
				}
			}
		}

#if NEVER_DEFINED

		static AbstractMetaData()
		{
			logger = LoggerFactory.getLogger(typeof(AbstractMetaData));
		}

		/// <summary> This is the shared parser for all subclasses of AbstractMetaData.
		/// We use a shared parser because the call to
		/// {@link XMLReaderFactory.createXMLReader()} is extremely expensive.
		/// All uses of this parser <i>must</i> be wrapped in a block synchronized
		/// on the parser itself.
		/// </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static XmlReader parser;

		// FIXME: Synchronizing on the parser will cause very bad performance if
		// multiple threads are trying to read metadata simultaneously. We should
		// build a mechanism by which we keep a pool of parsers, and allocate a
		// new one only when there is not an unused one available in the pool.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		static
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		parser = XMLReaderFactory.createXMLReader();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(SAXException e)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// This should never happen.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	ErrorDialog.bug(e);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary>**********************************************************************
	/// Base XML Handler for all metadata classes
	/// 
	/// </summary>
	class XMLHandler : XmlSaxDefaultHandler
{
	//UPGRADE_NOTE: Final was removed from the declaration of 'accumulator '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
	internal StringBuilder accumulator = new StringBuilder();
	protected internal string language = "";
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Override
		public override void startElement(string uri, string localName, string qName, SaxAttributesSupport attrs)
	{
		// clear the content accumulator
		accumulator.setLength(0);

		// Track language for localizable attributes
		language = getAttr(attrs, LANG_ATTR);
	}

	protected internal virtual string getAttr(SaxAttributesSupport attrs, string qName)
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		string value_Renamed = attrs.GetValue(qName);
		return value_Renamed == null ? "" : value_Renamed;
	}

	public override void endElement(string uri, string localName, string qName)
	{
		// handle all of the elements which have CDATA here

		string value_Renamed = accumulator.ToString().trim();

		if (VERSION_ELEMENT.equals(qName))
		{
			setVersion(value_Renamed);
		}
		else if (VASSAL_VERSION_ELEMENT.equals(qName))
		{
			setVassalVersion(value_Renamed);
		}
		else if (DESCRIPTION_ELEMENT.equals(qName))
		{
			if (descriptionAttr == null)
			{
				setDescription(new Attribute(DESCRIPTION_ELEMENT, value_Renamed));
			}
			else
			{
				descriptionAttr.addTranslation(language, value_Renamed);
			}
		}
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Override
		public override void characters(System.Char[] ch, int start, int length)
	{
		accumulator.append(ch, start, length);
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Override
		//UPGRADE_TODO: Class 'org.xml.sax.SAXParseException' was converted to 'System.xml.XmlException' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public override void warning(System.Xml.XmlException e)
	{
		logger.warn("", e);
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Override
		//UPGRADE_TODO: Class 'org.xml.sax.SAXParseException' was converted to 'System.xml.XmlException' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public override void error(System.Xml.XmlException e)
	{
		logger.error("", e);
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Override
		//UPGRADE_TODO: Class 'org.xml.sax.SAXParseException' was converted to 'System.xml.XmlException' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public override void fatalError(System.Xml.XmlException e)
	{
		throw e;
	}
}

/// <summary>**********************************************************************
/// XML Handler for parsing a buildFile. Used to read minimal data from
/// modules saved prior to 3.1.0.
/// </summary>
class BuildFileXMLHandler : XmlSaxDefaultHandler
{
	//UPGRADE_NOTE: Final was removed from the declaration of 'accumulator '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
	internal StringBuilder accumulator = new StringBuilder();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Override
	public override void startElement(string uri, string localName, string qName, SaxAttributesSupport attrs)
	{
		// clear the content accumulator
		accumulator.setLength(0);
	}

	protected internal virtual string getAttr(SaxAttributesSupport attrs, string qName)
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		string value_Renamed = attrs.GetValue(qName);
		return value_Renamed == null ? "" : value_Renamed;
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Override
	public override void endElement(string uri, string localName, string qName)
	{
		// handle all of the elements which have CDATA here
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Override
	public override void characters(System.Char[] ch, int start, int length)
	{
		accumulator.append(ch, start, length);
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Override
	//UPGRADE_TODO: Class 'org.xml.sax.SAXParseException' was converted to 'System.xml.XmlException' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
	public override void warning(System.Xml.XmlException e)
	{
		logger.warn("", e);
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Override
	//UPGRADE_TODO: Class 'org.xml.sax.SAXParseException' was converted to 'System.xml.XmlException' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
	public override void error(System.Xml.XmlException e)
	{
		logger.error("", e);
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Override
	//UPGRADE_TODO: Class 'org.xml.sax.SAXParseException' was converted to 'System.xml.XmlException' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
	public override void fatalError(System.Xml.XmlException e)
	{
		throw e;
	}
}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"

#endif

	}
	
#else

	/// <summary> 
	/// Base class representing the metadata for a Saved Game, Module or Extension.
	/// 
	/// </summary>
	/// <author>Brent Easton</author>
	/// <since> 3.1.0</since>
	public abstract class AbstractMetaData
	{
		virtual public string Version
		{
			get
			{
				return version == null ? "" : version;
			}

			set
			{
				version = value;
			}

		}
		virtual public string VassalVersion
		{
			get
			{
				return vassalVersion == null ? "" : vassalVersion;
			}

			set
			{
				vassalVersion = value;
			}

		}
		virtual public string LocalizedDescription
		{
			get
			{
				return descriptionAttr == null ? "" : descriptionAttr.getLocalizedValue();
			}

		}
		/// <summary> Return the Entry name for the metatdata file
		/// 
		/// </summary>
		/// <returns> Zip Entry name
		/// </returns>
		public abstract string ZipEntryName { get; }

		/// <summary> Return the version of the metadata structure
		/// 
		/// </summary>
		/// <returns> version
		/// </returns>
		public abstract string MetaDataVersion { get; }


		protected internal const string TRUE = "true";
		protected internal const string FALSE = "false";

		protected internal const string NAME_ATTR = "name";
		protected internal const string VERSION_ATTR = "version";
		protected internal const string VASSAL_VERSION_ATTR = "vassalVersion";
		protected internal const string DESCRIPTION_ATTR = "description";
		protected internal const string EXTENSION_ATTR = "extension";
		protected internal const string MODULE_NAME_ATTR = "moduleName";
		protected internal const string MODULE_VERSION_ATTR = "moduleVersion";
		protected internal const string LANG_ATTR = "lang";

		protected internal const string ROOT_ELEMENT = "data";
		protected internal const string VERSION_ELEMENT = "version";
		protected internal const string VASSAL_VERSION_ELEMENT = "VassalVersion";
		protected internal const string MODULE_NAME_ELEMENT = "moduleName";
		protected internal const string MODULE_VERSION_ELEMENT = "moduleVersion";
		protected internal const string DESCRIPTION_ELEMENT = "description";
		protected internal const string NAME_ELEMENT = "name";
		protected internal const string DATE_SAVED_ELEMENT = "dateSaved";

		protected internal const string BUILDFILE_MODULE_ELEMENT1 = "VassalSharp.launch.BasicModule";
		protected internal const string BUILDFILE_MODULE_ELEMENT2 = "VassalSharp.build.GameModule";
		protected internal const string BUILDFILE_EXTENSION_ELEMENT = "VassalSharp.build.module.ModuleExtension";

		protected internal string version;
		protected internal string vassalVersion;
		protected internal Attribute_Renamed_Class descriptionAttr;

		public AbstractMetaData()
		{
			VassalVersion = Info.Version;
		}

		public virtual void setDescription(Attribute_Renamed_Class desc)
		{
			descriptionAttr = desc;
		}

		public virtual void setDescription(string desc)
		{
			descriptionAttr = new Attribute_Renamed_Class(GameModule.DESCRIPTION, desc);
		}

		public virtual string getDescription()
		{
			return null;
		}


		public virtual void save(FileArchive archive)
		{
		}

		/// <summary> Write common metadata to the specified Archive. Call addElements to
		/// add elements specific to particular concrete subclasses.
		/// 
		/// </summary>
		/// <param name="archive">Extension Archive
		/// </param>
		/// <throws>  IOException </throws>
		/// <summary>           If anything goes wrong
		/// </summary>
		public virtual void save(ArchiveWriter archive)
		{
		}

		/// <summary> Copy the Module metatdata from the current module into the specified
		/// archive.
		/// 
		/// </summary>
		/// <param name="archive">Archive to copy into
		/// </param>
		/// <throws>  IOException </throws>
		public virtual void copyModuleMetadata(ArchiveWriter archive)
		{
		}

		public virtual void copyModuleMetadata(FileArchive archive)
		{
		}

		/// <summary> Add elements specific to a MetaData subclass
		/// 
		/// </summary>
		/// <param name="doc">Document
		/// </param>
		/// <param name="root">Root element
		/// </param>
		protected internal abstract void AddElements(System.Xml.XmlDocument doc, System.Xml.XmlElement root);

		/// <summary>**********************************************************************
		/// Utility class representing a Configurable attribute and its translations
		/// 
		/// </summary>
		public class Attribute_Renamed_Class
		{
			/// <summary> Build Attribute class based on atrribute value and translations
			/// available in the current module
			/// 
			/// </summary>
			/// <param name="c">Target configurable
			/// </param>
			/// <param name="name">Attribute name
			/// </param>
			public Attribute_Renamed_Class(Configurable target, string name)
			{
			}

			public Attribute_Renamed_Class(string attributeName, string value_Renamed)
			{
			}

			public virtual void addTranslation(string language, string value_Renamed)
			{
			}

			/// <summary>
			/// Return the untranslated value of this attribute
			/// </summary>
			public string getValue()
			{
				return null;
			}

			/**
			 * Return the value of this attribute translated into the local
			 * language
			 *
			 * @return translated value
			 */
			public string getLocalizedValue()
			{
				return null;
			}
			/// <summary> Output metadata XML for this attribute, including translations
			/// 
			/// </summary>
			/// <param name="doc">Root document
			/// </param>
			/// <param name="root">Parent element
			/// </param>
			/// <param name="prefix">Attribute prefix
			/// </param>
			public virtual void generateXML(System.Xml.XmlDocument doc, System.Xml.XmlElement root, string prefix)
			{

			}
		}
	}
#endif
}
