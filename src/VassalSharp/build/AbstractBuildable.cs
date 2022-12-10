/*
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

using System;

using PropertyNameSource = VassalSharp.build.module.properties.PropertyNameSource;
using ValidationReport = VassalSharp.configure.ValidationReport;
using ValidityChecker = VassalSharp.configure.ValidityChecker;
using Localization = VassalSharp.i18n.Localization;
using Translatable = VassalSharp.i18n.Translatable;
using System.Collections.Generic;

namespace VassalSharp.build
{
	/// <summary>
	/// Abstract implementation of the Buildable interface. To make a Buildable
	/// component, inherit from this class. Once you implement the abstract methods
	/// and specify the Buildable attributes of this class, the build process will
	/// automatically create instances from the configuration file.
	/// </summary>
	public abstract class AbstractBuildable : Buildable, ValidityChecker, PropertyNameSource
	{
		/// <summary>
		/// The children of this component in the Buildable component hierarchy.
		/// </summary>
		protected List<Buildable> buildComponents = new List<Buildable>();

		/// <summary>
		/// Subclasses use this field to provide a <code>ValidityChecker</code>
		/// to be used in the <code>Validate</code> method.
		/// </summary>
		protected ValidityChecker validator;

		/// <summary>
		/// The names of all the Buildable attributes of this Buildable component.
		/// </summary>
		public abstract string[] AttributeNames { get; }

		public AbstractBuildable()
		{
		}
		
		/// <summary> 
        /// Build this component by getting all XML attributes of the XML element and
		/// calling {@link #setAttribute} with the String value of the attribute
		/// </summary>
		public virtual void Build(System.Xml.XmlElement e)
		{
			if (e != null)
			{
				System.Xml.XmlAttributeCollection c = e.Attributes;
				for (int i = 0; i < c.Count; ++i)
				{
					System.Xml.XmlAttribute att = c[i];
					SetAttribute(att.Name, att.Value);
					
					/*
					* Save a record of all Attributes for later translation. Need to save
					* all attributes, not just translatable ones as the current component
					* has not been completely built yet and a ComponentI18nData object
					* cannot be built.
					*/
					if (this is Translatable)
					{
						Localization.Instance.saveTranslatableAttribute((Translatable) this, att.Name, att.Value);
					}
				}
				Builder.build(e, this);
			}
		}
		
		/// <summary>
        /// Sets an attribute value for this component. The <code>key</code>
		/// parameter will be one of those listed in {@link #getAttributeNames}.
        /// If the <code>value</code> parameter is a String, it will be the value
		/// returned by {@link #getAttributeValueString} for the same
		/// <code>key</code>. If the implementing class extends
		/// {@link AbstractConfigurable}, then <code>value</code> will be an
		/// instance of the corresponding Class listed in
		/// {@link AbstractConfigurable#getAttributeTypes}
		/// 
		/// </summary>
		/// <param name="key">the name of the attribute. Will be one of those listed in
		/// {@link #getAttributeNames}
		/// </param>
		public abstract void SetAttribute(string key, object value);
		
		/// <summary>
        /// Return a String representation of the attribute with the given name. When
		/// initializing a module, this String value will be passed to
		/// {@link #setAttribute}.
		/// </summary>
		/// <param name="key">the name of the attribute. Will be one of those listed in
		/// {@link #getAttributeNames}
		/// </param>
		public abstract string getAttributeValueString(string key);

		/// <returns> all build components that are an instance of the given class
		/// </returns>
		/// <deprecated> Use {@link #getComponentsOf(Class<T>)} instead.
		/// </deprecated>
		public IEnumerator<T> getComponents<T>()
		{
			return getComponentsOf<T>().GetEnumerator();
		}

		/// <returns> all build components that are an instance of the given class
		/// </returns>
		public List<T> getComponentsOf<T>()
		{
			List<T> l = new List<T>();
			foreach (Buildable b in buildComponents)
			{
				if (b.GetType() == typeof(T))
				{
					l.Add((T) b);
				}
			}
			return l;
		}

		/// <summary> Recursively descend the build tree and return an enumeration of all
		/// components that are instances of the given class
		/// 
		/// </summary>
		/// <param name="target">
		/// </param>
		/// <returns>
		/// </returns>
		/// <deprecated> Use {@link #getAllDescendantComponentsOf(Class<T>)} instead.
		/// </deprecated>
		public IEnumerator<T> getAllDescendantComponents<T>() where T : AbstractBuildable
		{
			return getAllDescendantComponentsOf<T>().GetEnumerator();
		}

		/// <summary> Recursively descend the build tree and return a {@link List} of all
		/// components that are instances of the given class
		/// 
		/// </summary>
		/// <param name="target">
		/// </param>
		/// <returns>
		/// </returns>
		public List<T> getAllDescendantComponentsOf<T>() where T : AbstractBuildable
		{
			List<T> l = new List<T>();
			addComponents(l);
			return l;
		}

		private void addComponents<T>(List<T> l) where T : AbstractBuildable
		{
			if (typeof(T) == this.GetType())
			{
				l.Add((T) this);
			}

			foreach (Buildable b in buildComponents)
			{
				if (typeof(T) == b.GetType())
				{
					l.Add((T) b);
				}
				else if (b is AbstractBuildable)
				{
					((AbstractBuildable)b).addComponents(l);
				}
			}
		}

		public virtual System.Xml.XmlElement getBuildElement(System.Xml.XmlDocument doc)
		{
			System.Xml.XmlElement el = doc.CreateElement(GetType().FullName);
			string[] names = AttributeNames;
			for (int i = 0; i < names.Length; ++i)
			{
				string val = getAttributeValueString(names[i]);
				if (val != null)
				{
					//UPGRADE_TODO: Method 'org.w3c.dom.Element.setAttribute' was converted to 'System.Xml.XmlElement.SetAttribute' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_orgw3cdomElementsetAttribute_javalangString_javalangString'"
					el.SetAttribute(names[i], val);
				}
			}
			
			foreach (Buildable b in getBuildables())
			{
				el.AppendChild(b.getBuildElement(doc));
			}
			return el;
		}
		
		/// <summary> Add a Buildable object to this object</summary>
		public virtual void add(Buildable b)
		{
			buildComponents.Add(b);
		}
		
		/// <summary>
		/// Returns an enumeration of Buildable objects which are the direct children
		/// of this object in the Buildable containment hierarchy. The
		/// {@link #getBuildElement} method uses these objects to construct the XML
		/// element from which this object can be built.
		/// </summary>
		/// <deprecated> Use {@link #getBuildables()} instead.</deprecated>
		public IEnumerator< Buildable > getBuildComponents()
		{
			return buildComponents.GetEnumerator();
		}

		/// <summary>
		/// Returns a Collection of Buildable objects which are the direct children
		/// of this object in the Buildable containment hierarchy. The
		/// {@link #getBuildElement} method uses these objects to construct the XML
		/// element from which this object can be built.
		/// </summary>
		public IReadOnlyCollection< Buildable > getBuildables()
		{
			return buildComponents.AsReadOnly();
		}

		public virtual void validate(Buildable target, ValidationReport report)
		{
			if (validator != null)
			{
				validator.validate(target, report);
			}
			foreach (Buildable child in buildComponents)
			{
				if (child is ValidityChecker)
				{
					((ValidityChecker) child).validate(child, report);
				}
			}
		}
		
		/// <summary>
		/// Default implementation of PropertyNameSource - No properties exposed
		/// </summary>
		public virtual List <string> PropertyNames { get { return new List<string>(); } }

		public abstract void  addTo(VassalSharp.build.Buildable param1);
	}
}