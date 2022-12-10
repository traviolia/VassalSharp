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
#if NEVER_DEFINED
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using DataArchive = VassalSharp.tools.DataArchive;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ThrowableUtils = VassalSharp.tools.ThrowableUtils;
using IOUtils = VassalSharp.tools.io.IOUtils;
#endif 
namespace VassalSharp.build
{
#if NEVER_DEFINED
	/// <summary> This class holds static convenience methods for building {@link Buildable}
	/// objects.
	/// </summary>
	public abstract class Builder
	{

		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.build.Builder'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;

		/// <summary> General building algorithm.  For each subelement of the build
		/// Element, this method creates an instance of the class (which
		/// must implement Buildable) whose name matches the XML element
		/// tag name, builds that instance with the subelement, and adds it
		/// to the parent Buildable
		/// 
		/// This algorithm calls a component's {@link Buildable#build} method
		/// before calling its {@link Buildable#addTo} method
		/// 
		/// </summary>
		/// <param name="parent">the parent Buildable instance
		/// </param>
		public static void build(System.Xml.XmlElement e, Buildable parent)
		{
			if (e == null)
				return;

			for (System.Xml.XmlNode child = e.FirstChild; child != null; child = child.NextSibling)
			{
				if ((short)System.Xml.XmlNodeType.Element == System.Convert.ToInt16(child.NodeType))
				{
					try
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						Buildable b = create((System.Xml.XmlElement)child);
						if (parent != null)
						{
							b.addTo(parent);
							parent.add(b);
						}
					}
					catch (IllegalBuildException ex)
					{
						ErrorDialog.bug(ex);
					}
					catch (System.SystemException ex)
					{
						logger.error("Error building " + child.Name);
						throw ex;
					}
					catch (System.ApplicationException ex)
					{
						logger.error("Error building " + child.Name);
						throw ex;
					}
				}
			}
		}

		/// <summary> Create an instance of a class from an XML element and build it.
		/// 
		/// The <code>.class</code> file for the named class may be either
		/// in the System's classpath or else within the {@link DataArchive}
		/// of the {@link GameModule}.
		/// 
		/// </summary>
		/// <throws>  IllegalBuildException if something goes wrong when loading </throws>
		/// <summary> the class or creating an instance of it
		/// </summary>
		public static Buildable create(System.Xml.XmlElement e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'mod '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GameModule mod = GameModule.getGameModule();
			//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			string name = e.Name;

			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: The differences in the format  of parameters for method 'java.lang.Class.forName'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				Buildable b = (Buildable)(mod == null ? System.Type.GetType(name) : mod.getDataArchive().loadClass(name)).getConstructor().newInstance();
				b.build(e);
				return b;
			}
			//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception t)
			{
				// find and rethrow causes which are not bugs
				ThrowableUtils.throwRecent(typeof(System.OutOfMemoryException), t);

				//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				//UPGRADE_NOTE: Exception 'java.lang.InstantiationException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				//UPGRADE_NOTE: Exception 'java.lang.ExceptionInInitializerError' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				if (t is System.InvalidCastException || t is System.Exception || t is System.UnauthorizedAccessException || t is System.ArgumentException || t is System.Exception || t is System.Reflection.TargetInvocationException || t is System.MethodAccessException || t is System.Security.SecurityException || t is System.Exception || t is System.ApplicationException)
				{
					// one of the standard classloading problems occured
					throw new IllegalBuildException("failed to load class " + name, t);
				}
				else if (t is System.ApplicationException)
				{
					// some unusual problem occurred
					throw (System.ApplicationException)t;
				}
				else if (t is System.SystemException)
				{
					// some unusual problem occurred
					throw (System.SystemException)t;
				}
				else
				{
					// this should never happen
					throw new IllegalStateException(t);
				}
			}
		}

		/// <summary> Read an XML document from an InputStream</summary>
		public static System.Xml.XmlDocument createDocument(System.IO.Stream in_Renamed)
		{
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'doc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Xml.XmlDocument tempDocument;
				//UPGRADE_ISSUE: Method 'javax.xml.parsers.DocumentBuilderFactory.newInstance' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxxmlparsersDocumentBuilderFactory'"
				DocumentBuilderFactory.newInstance();
				//UPGRADE_TODO: Method 'javax.xml.parsers.DocumentBuilder.parse' was converted to 'System.Xml.XmlDocument.Load' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxxmlparsersDocumentBuilderparse_javaioInputStream'"
				tempDocument = (System.Xml.XmlDocument)new System.Xml.XmlDocument().Clone();
				tempDocument.Load(in_Renamed);
				System.Xml.XmlDocument doc = tempDocument;
				in_Renamed.Close();
				return doc;
			}
			//UPGRADE_ISSUE: Class 'javax.xml.parsers.ParserConfigurationException' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxxmlparsersParserConfigurationException'"
			catch (ParserConfigurationException e)
			{
				ErrorDialog.bug(e);
				return null;
			}
			//UPGRADE_TODO: Class 'org.xml.sax.SAXException' was converted to 'System.Xml.XmlException' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			catch (System.Xml.XmlException e)
			{
				// FIXME: switch to IOException(Throwable) ctor in Java 1.6
				throw (System.IO.IOException)new System.IO.IOException().initCause(e);
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}

		/// <summary> Create a new XML document</summary>
		public static System.Xml.XmlDocument createNewDocument()
		{
			try
			{
				//UPGRADE_TODO: Class 'javax.xml.parsers.DocumentBuilder' was converted to 'System.Xml.XmlDocument' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxxmlparsersDocumentBuilder'"
				return new System.Xml.XmlDocument();
			}
			//UPGRADE_ISSUE: Class 'javax.xml.parsers.ParserConfigurationException' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxxmlparsersParserConfigurationException'"
			catch (ParserConfigurationException e)
			{
				ErrorDialog.bug(e);
				return null;
			}
		}

		/// <summary> Write an XML document to a Writer</summary>
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Writer' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public static void writeDocument(System.Xml.XmlDocument doc, System.IO.StreamWriter writer)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'source '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'javax.xml.transform.dom.DOMSource' was converted to 'DomSourceSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxxmltransformdomDOMSource'"
			BasicSourceSupport source = new DomSourceSupport(doc);

			// Prepare the output file
			//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			BasicResultSupport result = new GenericResultSupport(writer);

			// Write the DOM document to the file
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'xformer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Method 'javax.xml.transform.TransformerFactory.newTransformer' was converted to 'SupportClass.TransformerSupport.NewTransform' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxxmltransformTransformerFactorynewTransformer'"
				TransformerSupport xformer = TransformerSupport.NewTransform(TransformerSupport.NewInstance());
				//UPGRADE_ISSUE: Method 'javax.xml.transform.Transformer.setOutputProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxxmltransformTransformersetOutputProperty_javalangString_javalangString'"
				//UPGRADE_ISSUE: Field 'javax.xml.transform.OutputKeys.INDENT' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxxmltransformOutputKeys'"
				xformer.setOutputProperty(OutputKeys.INDENT, "yes"); //$NON-NLS-1$
																	 //UPGRADE_ISSUE: Method 'javax.xml.transform.Transformer.setOutputProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxxmltransformTransformersetOutputProperty_javalangString_javalangString'"
				xformer.setOutputProperty("{http://xml.apache.org/xslt}indent-amount", "4"); //$NON-NLS-1$ //$NON-NLS-2$
				xformer.DoTransform(source, result);
			}
			catch (System.Xml.Xsl.XsltException e)
			{
				// FIXME: switch to IOException(Throwable) ctor in Java 1.6
				throw (System.IO.IOException)new System.IO.IOException().initCause(e);
			}
		}

		/// <summary> Return the decoded text contents of an Element node</summary>
		public static string getText(System.Xml.XmlElement e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder buffer = new StringBuilder();
			//UPGRADE_NOTE: Final was removed from the declaration of 'sub '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Method 'org.w3c.dom.Node.getChildNodes' was converted to 'System.Xml.XmlNode.ChildNodes' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Xml.XmlNodeList sub = e.ChildNodes;
			for (int i = 0; i < sub.Count; ++i)
			{
				if (System.Convert.ToInt16(sub.Item(i).NodeType) == (short)System.Xml.XmlNodeType.Text)
				{
					buffer.append(((System.Xml.XmlText)sub.Item(i)).Data);
				}
				else if (System.Convert.ToInt16(sub.Item(i).NodeType) == (short)System.Xml.XmlNodeType.EntityReference)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.ToString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					buffer.append(sub.Item(i).FirstChild.ToString());
				}
			}
			return buffer.ToString().trim();
		}

		/// <returns> a String representation of an XML document
		/// </returns>
		public static string ToString(System.Xml.XmlDocument doc)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.StringWriter w = new System.IO.StringWriter();
			try
			{
				writeDocument(doc, w);
				return w.ToString();
			}
			// FIXME: review error message
			catch (System.IO.IOException e)
			{
				//      IOErrorDialog.error(e);
				return ""; //$NON-NLS-1$
			}
		}

		[STAThread]
		public static void Main(string[] args)
		{
			System.Xml.XmlDocument doc = createNewDocument();
			System.Xml.XmlElement e = doc.CreateElement("test"); //$NON-NLS-1$
			System.Xml.XmlElement e1 = doc.CreateElement("sub1"); //$NON-NLS-1$
			e.AppendChild(e1);
			System.Xml.XmlElement e2 = doc.CreateElement("sub2"); //$NON-NLS-1$
																  //UPGRADE_TODO: Method 'org.w3c.dom.Element.setAttribute' was converted to 'System.Xml.XmlElement.SetAttribute' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_orgw3cdomElementsetAttribute_javalangString_javalangString'"
			e2.SetAttribute("one", "1"); //$NON-NLS-1$ //$NON-NLS-2$
										 //UPGRADE_TODO: Method 'org.w3c.dom.Element.setAttribute' was converted to 'System.Xml.XmlElement.SetAttribute' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_orgw3cdomElementsetAttribute_javalangString_javalangString'"
			e2.SetAttribute("two", "2"); //$NON-NLS-1$ //$NON-NLS-2$
			e.AppendChild(e2);
			System.Xml.XmlElement e3 = doc.CreateElement("sub3"); //$NON-NLS-1$
			System.Xml.XmlElement e4 = doc.CreateElement("sub4"); //$NON-NLS-1$
			e4.AppendChild(doc.CreateTextNode("4 > 2")); //$NON-NLS-1$
			e3.AppendChild(e4);
			e.AppendChild(e3);
			doc.AppendChild(e);
			System.Console.Error.WriteLine(ToString(doc));
			System.Console.Error.WriteLine("StringBuilder"); //$NON-NLS-1$
			StringBuilder buf = new StringBuilder(300000);
			for (int i = 0; i < 500000; ++i)
			{
				buf.append("  "); //$NON-NLS-1$
				if (i % 10000 == 0)
				{
					//UPGRADE_TODO: Method 'java.io.PrintStream.println' was converted to 'System.Console.Error.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintStreamprintln_javalangObject'"
					System.Console.Error.WriteLine(buf.length()); //$NON-NLS-1$
				}
			}
		}
		static Builder()
		{
			logger = LoggerFactory.getLogger(typeof(Builder));
		}
	}
#else
	/// <summary>
	/// MLT - placeholder interface
	/// </summary>
	public abstract class Builder
	{

		/// <summary> General building algorithm.  For each subelement of the build
		/// Element, this method creates an instance of the class (which
		/// must implement Buildable) whose name matches the XML element
		/// tag name, builds that instance with the subelement, and adds it
		/// to the parent Buildable
		/// 
		/// This algorithm calls a component's {@link Buildable#build} method
		/// before calling its {@link Buildable#addTo} method
		/// 
		/// </summary>
		/// <param name="parent">the parent Buildable instance
		/// </param>
		public static void build(System.Xml.XmlElement e, Buildable parent) { }

		/// <summary> Create an instance of a class from an XML element and build it.
		/// 
		/// The <code>.class</code> file for the named class may be either
		/// in the System's classpath or else within the {@link DataArchive}
		/// of the {@link GameModule}.
		/// 
		/// </summary>
		/// <throws>  IllegalBuildException if something goes wrong when loading </throws>
		/// <summary> the class or creating an instance of it
		/// </summary>
		public static Buildable create(System.Xml.XmlElement e) { return null; }

		/// <summary> Read an XML document from an InputStream</summary>
		public static System.Xml.XmlDocument createDocument(System.IO.Stream in_Renamed) { return null; }

		/// <summary> Create a new XML document</summary>
		public static System.Xml.XmlDocument createNewDocument() { return null; }

		/// <summary> Write an XML document to a Writer</summary>
		public static void writeDocument(System.Xml.XmlDocument doc, System.IO.StreamWriter writer) { }

		/// <summary> Return the decoded text contents of an Element node</summary>
		public static string getText(System.Xml.XmlElement e) { return null; }

		/// <returns> a String representation of an XML document
		/// </returns>
		public static string ToString(System.Xml.XmlDocument doc) { return null; }
	}


#endif
}