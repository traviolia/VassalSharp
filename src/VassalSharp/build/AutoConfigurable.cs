/*
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

using AutoConfigurer = VassalSharp.configure.AutoConfigurer;
using VisibilityCondition = VassalSharp.configure.VisibilityCondition;
using Localization = VassalSharp.i18n.Localization;

namespace VassalSharp.build
{
	
	/// <summary> A class that implements AutoConfigurable can use the {@link AutoConfigurer}
	/// class to automatically build a property editor.
	/// </summary>
	public interface AutoConfigurable : Configurable
	{
		/// <returns> an array of Strings giving all attributes of this Buildable
		/// component that will be written to/read from an XML element
		/// </returns>
		string[] AttributeNames
		{
			get;
		}

		/// <summary>
        /// Return an array of Strings describing the attributes of this object.
        /// These strings are used as prompts in the Properties window for this object.
		/// </summary>
		string[] AttributeDescriptions
		{
			get;
		}
		
		/// <summary>
        /// Called by the {@link #build} method, where <code>value</code> is the String value read by the XML attribute.
		/// Can also be called with Object value to set the attribute.
		/// </summary>
		void SetAttribute(string key, System.Object value_Renamed);
		
		/// <summary>
        /// Called by the {@link #getBuildElement} method to write the attributes into an XML element
		/// </summary>
		new string getAttributeValueString(string key);

        /// <summary>
        /// Return the Class for the attributes of this object.
        /// Valid classes are:  String, Integer, Double, Boolean, Image, File, Color, and KeyStroke
        /// </summary>
        Type[] getAttributeTypes();
	
	    /// <summary> Because attributes are not always applicable in all cases, this method returns an interface
	    /// to determine when the controls for specifying the named attribute should be visible.
	    /// </summary>
	    /// <param name="name">
	    /// </param>
	    /// <returns> null if the attribute controls should always be visible;
	    /// </returns>
        VisibilityCondition getAttributeVisibility(String name);

    }

    public class Util
	{
		public static void  buildAttributes(System.Xml.XmlElement e, AutoConfigurable parent)
		{
			if (e != null)
			{
				System.Xml.XmlNamedNodeMap n = (System.Xml.XmlAttributeCollection) e.Attributes;
				for (int i = 0; i < n.Count; ++i)
				{
					System.Xml.XmlAttribute att = (System.Xml.XmlAttribute) n.Item(i);
					parent.SetAttribute(att.Name, att.Value);
					Localization.Instance.saveTranslatableAttribute(parent, att.Name, att.Value);
				}
			}
		}
		
		public static System.Xml.XmlElement getBuildElement(System.Xml.XmlDocument doc, AutoConfigurable parent)
		{
			System.Xml.XmlElement el = doc.CreateElement(parent.GetType().FullName);
			string[] names = parent.AttributeNames;
			for (int i = 0; i < names.Length; ++i)
			{
				string val = parent.getAttributeValueString(names[i]);
				if (val != null)
				{
					//UPGRADE_TODO: Method 'org.w3c.dom.Element.setAttribute' was converted to 'System.Xml.XmlElement.SetAttribute' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_orgw3cdomElementsetAttribute_javalangString_javalangString'"
					el.SetAttribute(names[i], val);
				}
			}
			return el;
		}
	}
}