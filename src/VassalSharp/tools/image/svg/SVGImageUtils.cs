/*
* $Id$
*
* Copyright (c) 2008 by Joel Uckelman
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
//UPGRADE_TODO: The type 'org.apache.batik.dom.GenericDOMImplementation' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using GenericDOMImplementation = org.apache.batik.dom.GenericDOMImplementation;
//UPGRADE_TODO: The type 'org.apache.batik.dom.svg.SAXSVGDocumentFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SAXSVGDocumentFactory = org.apache.batik.dom.svg.SAXSVGDocumentFactory;
//UPGRADE_TODO: The type 'org.apache.batik.dom.util.DOMUtilities' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DOMUtilities = org.apache.batik.dom.util.DOMUtilities;
//UPGRADE_TODO: The type 'org.apache.batik.dom.util.SAXDocumentFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SAXDocumentFactory = org.apache.batik.dom.util.SAXDocumentFactory;
//UPGRADE_TODO: The type 'org.apache.batik.dom.util.XLinkSupport' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using XLinkSupport = org.apache.batik.dom.util.XLinkSupport;
//UPGRADE_TODO: The type 'org.apache.batik.dom.util.XMLSupport' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using XMLSupport = org.apache.batik.dom.util.XMLSupport;
//UPGRADE_TODO: The type 'org.apache.batik.util.XMLResourceDescriptor' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using XMLResourceDescriptor = org.apache.batik.util.XMLResourceDescriptor;
using ImageIOException = VassalSharp.tools.image.ImageIOException;
using ImageNotFoundException = VassalSharp.tools.image.ImageNotFoundException;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.tools.image.svg
{
	
	/// <summary> Utility methods for manipulating SVG images.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class SVGImageUtils
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < String > reflist = new ArrayList < String >();
			reflist.add(path);
			return getExternalReferences(path, reflist);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final HashSet < String > follow = new HashSet < String >();
			//UPGRADE_NOTE: Final was removed from the declaration of 'here '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.UriBuilder temp_uriBuilder;
			//UPGRADE_TODO: Class 'java.net.URL' was converted to a 'System.Uri' which does not throw an exception if a URL specifies an unknown protocol. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1132'"
			temp_uriBuilder = new System.UriBuilder("file", null);
			temp_uriBuilder.Path = new File(path).FullName;
			System.Uri here = temp_uriBuilder.Uri;
			
			System.Xml.XmlDocument doc = null;
			try
			{
				lock (factory)
				{
					doc = factory.createDocument(here.ToString());
				}
			}
			//UPGRADE_TODO: Class 'org.w3c.dom.DOMException' was converted to 'System.Exceptiont' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			catch (System.Exception e)
			{
				throw (System.IO.IOException) new System.IO.IOException().initCause(e);
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'usenodes '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Xml.XmlNodeList usenodes = doc.GetElementsByTagName("use");
			for (int i = 0; i < usenodes.Count; ++i)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'e '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Xml.XmlElement e = (System.Xml.XmlElement) usenodes.Item(i);
				//UPGRADE_NOTE: Final was removed from the declaration of 'url '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Uri url = new URL(new URL(e.getBaseURI()), XLinkSupport.getXLinkHref(e));
				// balk (for now) unless file is available on our filesystem
				if (url.Scheme.Equals("file"))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'refpath '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String refpath = url.AbsolutePath;
					if (!known.contains(refpath))
					{
						follow.add(refpath);
						known.add(refpath);
					}
				}
				else
				{
					throw new System.IO.IOException("unsupported protocol '" + url.Scheme + "' in xlink:href");
				}
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String s: follow)
			{
				known.addAll(getExternalReferences(s, known));
			}
			
			return known;
		}
		// NB: SAXSVGDocumentFactory isn't thread-safe, we have to synchronize on it.
		//UPGRADE_NOTE: Final was removed from the declaration of 'factory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal static readonly SAXSVGDocumentFactory factory = new SAXSVGDocumentFactory(XMLResourceDescriptor.getXMLParserClassName());
		
		private SVGImageUtils()
		{
			InitBlock();
		}
		
		/// <summary> Returns the default dimensions of the SVG image.
		/// 
		/// </summary>
		/// <returns> the image dimensions
		/// </returns>
		/// <throws>  IOException if the image cannot be read </throws>
		public static System.Drawing.Size getImageSize(System.IO.Stream in_Renamed)
		{
			return getImageSize("", in_Renamed);
		}
		
		/// <summary> Returns the default dimensions of the SVG image.
		/// 
		/// </summary>
		/// <returns> the image dimensions
		/// </returns>
		/// <throws>  IOException if the image cannot be read </throws>
		public static System.Drawing.Size getImageSize(System.String name, System.IO.Stream in_Renamed)
		{
			// get the SVG
			//UPGRADE_NOTE: Final was removed from the declaration of 'doc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Xml.XmlDocument doc;
			try
			{
				lock (factory)
				{
					doc = factory.createDocument(null, in_Renamed);
				}
				in_Renamed.Close();
			}
			//UPGRADE_TODO: Class 'org.w3c.dom.DOMException' was converted to 'System.Exceptiont' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			catch (System.Exception e)
			{
				throw new ImageIOException(name, e);
			}
			catch (System.IO.FileNotFoundException e)
			{
				throw new ImageNotFoundException(name, e);
			}
			catch (System.IO.IOException e)
			{
				throw new ImageIOException(name, e);
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
			
			// get the default image width and height
			//UPGRADE_NOTE: Final was removed from the declaration of 'root '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Xml.XmlElement root = (System.Xml.XmlElement) doc.DocumentElement;
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'width '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int width = (int) (Float.parseFloat(root.GetAttribute("width", null).replaceFirst("px", "")) + 0.5);
				//UPGRADE_NOTE: Final was removed from the declaration of 'height '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int height = (int) (Float.parseFloat(root.GetAttribute("height", null).replaceFirst("px", "")) + 0.5);
				
				return new System.Drawing.Size(width, height);
			}
			catch (System.FormatException e)
			{
				throw new ImageIOException(name, e);
			}
		}
		
		/// <summary> Conducts a recursive depth-first search for external references
		/// in the given SVG file.
		/// 
		/// </summary>
		/// <param name="path">the path of the file to check for external references
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static List < String > getExternalReferences(String path) 
		throws IOException
		
		/// <summary> Conducts a recursive depth-first search for external references
		/// in the SVG file named by path. This is a helper function for
		/// {@link #getExternalReferences}.
		/// 
		/// </summary>
		/// <param name="path">the path of the file to check for external references
		/// </param>
		/// <param name="known">the list of references already found
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static List < String > getExternalReferences(
		String path, List < String > known) throws IOException
		
		/// <summary> Rewrites external references contained in SVG files.
		/// 
		/// </summary>
		/// <param name="path">the path of the file to be processed
		/// </param>
		public static sbyte[] relativizeExternalReferences(System.String path)
		{
			// use the GenericDOMImplementation here because
			// SVGDOMImplementation adds unwanted attributes to SVG elements
			//UPGRADE_NOTE: Final was removed from the declaration of 'fac '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SAXDocumentFactory fac = new SAXDocumentFactory(new GenericDOMImplementation(), XMLResourceDescriptor.getXMLParserClassName());
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'here '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.UriBuilder temp_uriBuilder;
			//UPGRADE_TODO: Class 'java.net.URL' was converted to a 'System.Uri' which does not throw an exception if a URL specifies an unknown protocol. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1132'"
			temp_uriBuilder = new System.UriBuilder("file", null);
			temp_uriBuilder.Path = new System.IO.FileInfo(path).FullName;
			System.Uri here = temp_uriBuilder.Uri;
			//UPGRADE_NOTE: Final was removed from the declaration of 'sw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.StringWriter sw = new System.IO.StringWriter();
			
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'doc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Xml.XmlDocument doc = fac.createDocument(here.ToString());
				relativizeElement((System.Xml.XmlElement) doc.DocumentElement);
				DOMUtilities.writeDocument(doc, sw);
			}
			//UPGRADE_TODO: Class 'org.w3c.dom.DOMException' was converted to 'System.Exceptiont' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			catch (System.Exception e)
			{
				throw (System.IO.IOException) new System.IO.IOException().initCause(e);
			}
			
			sw.Flush();
			return SupportClass.ToSByteArray(SupportClass.ToByteArray(sw.ToString()));
		}
		
		protected internal static void  relativizeElement(System.Xml.XmlElement e)
		{
			// work from leaves to root in each subtree
			//UPGRADE_NOTE: Final was removed from the declaration of 'children '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Method 'org.w3c.dom.Node.getChildNodes' was converted to 'System.Xml.XmlNode.ChildNodes' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Xml.XmlNodeList children = e.ChildNodes;
			for (int i = 0; i < children.Count; ++i)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'n '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Xml.XmlNode n = children.Item(i);
				if (System.Convert.ToInt16(n.NodeType) == (short) System.Xml.XmlNodeType.Element)
					relativizeElement((System.Xml.XmlElement) n);
			}
			
			// relativize the xlink:href attribute if there is one
			if (e.hasAttributeNS(XLinkSupport.XLINK_NAMESPACE_URI, "href"))
			{
				try
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'url '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Uri url = new URL(new URL(e.getBaseURI()), XLinkSupport.getXLinkHref(e));
					//UPGRADE_NOTE: Final was removed from the declaration of 'anchor '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String anchor = url.Fragment;
					//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String name = new System.IO.FileInfo(url.AbsolutePath).Name;
					XLinkSupport.setXLinkHref(e, name + '#' + anchor);
				}
				// FIXME: review error message
				catch (System.UriFormatException ex)
				{
					//        ErrorLog.warn(ex);
				}
			}
			
			// remove xml:base attribute if there is one
			e.removeAttributeNS(XMLSupport.XML_NAMESPACE_URI, "base");
		}
	}
}