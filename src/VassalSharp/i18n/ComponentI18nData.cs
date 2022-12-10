/*
 * Copyright (c) 2000-2006 by Rodney Kinney, Brent Easton
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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using AutoConfigurable = VassalSharp.build.AutoConfigurable;
using Configurable = VassalSharp.build.Configurable;
//using BasicPiece = VassalSharp.counters.BasicPiece;
//using Decorator = VassalSharp.counters.Decorator;
//using GamePiece = VassalSharp.counters.GamePiece;
//using PlaceMarker = VassalSharp.counters.PlaceMarker;

namespace VassalSharp.i18n
{
	
	/// <summary>
    /// Object encapsulating the internationalization information for a component.
	/// The majority of translatable components subclass AbstractConfigurable,
	/// but some extend JFrame or JDialog and implement Configurable or AutoConfigurable.
	/// 
	/// AbstractConfigurable components are almost completely handled within the
	/// AbstractConfigurable base class. AutoConfigurable/Configurable components
	/// must call a different constructor and supply additional information.
	/// </summary>
	/// <author>  Brent Easton
	/// </author>
	public class ComponentI18nData
	{
		/// <summary>
        /// Return a unique Key prefix identifying this component.
        /// </summary>
		virtual public string Prefix
		{
			get
			{
				return prefix;
			}
			
			set
			{
				prefix = value;
			}
			
		}

		/// <summary>
        /// Return a unique key prefix including a full path of parent prefixes.
        /// All Translatable Pieces share a common prefix.
		/// </summary>
		/// <returns>
		/// </returns>
		virtual public string FullPrefix
		{
			get
			{
				if (VassalSharp.i18n.TranslatablePiece_Fields.PREFIX.Equals(prefix))
				{
					return prefix;
				}
				string fullPrefix = OwningComponent == null?"":OwningComponent.I18nData.FullPrefix;
				if (fullPrefix.Length > 0 && prefix.Length > 0)
				{
					fullPrefix += "."; //$NON-NLS-1$
				}
				return fullPrefix + prefix;
			}
			
		}

		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary> Return the owning Translatable of this component</summary>
		/// <summary> Set the owning Translatable of this component</summary>
		virtual public Translatable OwningComponent
		{
			get
			{
				return parent;
			}
			set
			{
				parent = value;
			}
		}

		/// <summary>
        /// Return true if this component has any translatable attributes, or if any of its children are translatable
		/// </summary>
		/// <returns> component translatable status
		/// </returns>
		virtual public bool Translatable
		{
			get
			{
				if (translatableProperties.Count > 0)
				{
					return true;
				}
				foreach (Translatable child in children)
				{
					if (child.I18nData.Translatable)
					{
						return true;
					}
				}
				return false;
			}
			
		}

        protected internal string prefix;
        protected internal Translatable parent;
        protected internal Configurable myComponent;
        protected IDictionary<string, Property> translatableProperties = new Dictionary<string, Property>();
        protected IDictionary<string, Property> allProperties = new Dictionary<string, Property>();
        protected List<Translatable> children = new List<Translatable>();

        /// <summary>
        /// Build from an AbstractConfigurable. The parent will be set from
        /// AbstractConfigurable.add(). untranslatedValues will be filled in as
        /// attributes are translated.
        /// </summary>
        /// <param name="c">AbstractConfigurable component
        /// </param>
        /// <param name="prefix">I18n Prefix
        /// </param>
        public ComponentI18nData(AbstractConfigurable c, string prefix)
		{
			init(c, prefix, c.AttributeNames, c.getAttributeTypes(), c.AttributeDescriptions);
		}
		
		public ComponentI18nData(AbstractConfigurable c, string prefix, List < string > names, List <Type> types, List < string > descriptions)
		{
			init(c, prefix, names.ToArray(), types.ToArray(), descriptions.ToArray());
		}

		/// <summary> Build from an AutoConfigurable
		/// 
		/// </summary>
		/// <param name="c">AutoConfgurable component
		/// </param>
		/// <param name="prefix">I18n prefix
		/// </param>
		public ComponentI18nData(AutoConfigurable c, string prefix)
		{
			this.prefix = prefix;
			parent = null;
			init(c, prefix, c.AttributeNames, c.getAttributeTypes(), c.AttributeDescriptions);
		}

		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected void init(Configurable c, string pfx, string[] names, Type[] types, string[] descriptions)
		{
			bool[] translatable = new bool[types.Length];
			for (int i = 0; i < types.Length; i++)
			{
				translatable[i] = types[i] != null && (types[i].Equals(typeof(string)) || typeof(TranslatableConfigurerFactory).IsAssignableFrom(types[i]));
			}
			init(c, pfx, names, descriptions, translatable);
		}

		protected internal virtual void  init(Configurable c, string pfx, string[] names, string[] descriptions, bool[] translatable)
		{
			prefix = pfx;
			myComponent = c;
			foreach (Configurable child in myComponent.ConfigureComponents)
			{
				children.Add(child);
			}

			for (int i = 0; i < translatable.Length; i++)
			{
				Property p = new Property(names[i], descriptions[i]);
				allProperties.Add(names[i], p);
				if (translatable[i])
				{
					translatableProperties.Add(names[i], p);
				}
			}
		}
		
		/// <summary> Build from a Configurable. Configurable does not support
		/// getAttributeNames() getAttributeTypes() or getAttributeValueString(),
		/// so more information must be supplied.
		/// 
		/// </summary>
		/// <param name="c">Component
		/// </param>
		/// <param name="prefix">I18n prefix
		/// </param>
		/// <param name="parent">parent translatable
		/// </param>
		/// <param name="names">Array of attribute names
		/// </param>
		/// <param name="translatable">Array of Attribute translatable status
		/// </param>
		public ComponentI18nData(Configurable c, string prefix, Translatable parent, string[] names, bool[] translatable, string[] descriptions)
		{
			myComponent = c;
			this.prefix = prefix;
			this.parent = parent;
			init(c, prefix, names, descriptions, translatable);
		}
		
		public ComponentI18nData(Configurable c, string prefix, Translatable parent):this(c, prefix, parent, new string[0], new bool[0], new string[0])
		{
		}
		
		public ComponentI18nData(Configurable c, string prefix):this(c, prefix, (Translatable) null)
		{
		}

#if NEVER_DEFINED

		/// <summary> Special build for PrototypeDefinition and PieceSlot</summary>
		public ComponentI18nData(Configurable c, GamePiece piece)
		{
			InitBlock();
			myComponent = c;
			this.prefix = VassalSharp.i18n.TranslatablePiece_Fields.PREFIX;
			parent = null;
			for (GamePiece p = piece; p != null; )
			{
				if (p is TranslatablePiece)
				{
					PieceI18nData pieceData = ((TranslatablePiece) p).getI18nData();
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					foreach (PieceI18nData.Property prop in pieceData.getProperties())
					{
						Property property = new Property(prop.Name, prop.Description);
						translatableProperties.put(prop.Name, property);
						allProperties.put(prop.Name, property);
					}
				}
				if (p is PlaceMarker)
				{
					if (((PlaceMarker) p).MarkerStandalone)
					{
						children.Add(new TranslatableMarker((PlaceMarker) p));
					}
				}
				if (p is BasicPiece)
				{
					p = null;
				}
				else
				{
					p = ((Decorator) p).getInner();
				}
			}
		}
#endif

		/// <summary> Return a list of all of the translatable Keys for attributes of this Translatable item.</summary>
		public ICollection<string> getAttributeKeys()
		{
			return translatableProperties.Keys;
		}

		/// <summary> Is the specified attribute allowed to be translated?
		/// 
		/// </summary>
		/// <param name="attr">Attribute name
		/// </param>
		/// <returns> is translatable
		/// </returns>
		public virtual bool isAttributeTranslatable(string attr)
		{
			return translatableProperties.ContainsKey(attr);
		}

		/// <summary> Force a specified attribute to be translatable/not translatable
		/// 
		/// </summary>
		/// <param name="attribute">Attribute name
		/// </param>
		/// <param name="set">translatable status
		/// </param>
		public virtual void setAttributeTranslatable(string attribute, bool set_Renamed)
		{
			if (set_Renamed)
			{
				allProperties.TryGetValue(attribute, out Property p);
				translatableProperties.Add(attribute, p);
			}
			else
			{
				translatableProperties.Remove(attribute);
			}
		}
		
		/// <summary> Convenience method to force all attributes to be not translatable</summary>
		public virtual void  setAllAttributesUntranslatable()
		{
			translatableProperties.Clear();
		}

		/// <summary> Apply a translatation to the specified attribute. Record the untranslated value in the untranslatedValues array and
		/// set the new value into the real attribute
		/// 
		/// </summary>
		/// <param name="attr">Attribute name
		/// </param>
		/// <param name="value">Translated value
		/// </param>
		public virtual void  applyTranslation(string attr, string value_Renamed)
		{
			translatableProperties.TryGetValue(attr, out Property p);
			if (attr != null)
			{
				p.UntranslatedValue = myComponent.getAttributeValueString(attr);
				myComponent.SetAttribute(attr, value_Renamed);
			}
		}
		
		/// <summary> Return description for named Attribute
		/// 
		/// </summary>
		/// <param name="attr">
		/// </param>
		/// <returns> description
		/// </returns>
		public virtual string getAttributeDescription(string attr)
		{
			allProperties.TryGetValue(attr, out Property p);
			return p?.Description;
		}
		
		/// <summary> Return the pre-translation value stored in this Object.
		/// 
		/// </summary>
		/// <param name="attr">Attribute Name
		/// </param>
		/// <returns> untranslated value
		/// </returns>
		public virtual string getLocalUntranslatedValue(string attr)
		{
			string val;
			allProperties.TryGetValue(attr, out Property p);
			if (p == null || p.UntranslatedValue == null)
			{
				val = myComponent.getAttributeValueString(attr);
			}
			else
			{
				val = p.UntranslatedValue;
			}
			return val;
		}

		/// <summary> Set an untranslatedValue for the specified attribute. Used by components that do not subclass AbstractConfigurable
		/// 
		/// </summary>
		/// <param name="attr">Attribute name
		/// </param>
		/// <param name="value">untranslated value
		/// </param>
		public virtual void setUntranslatedValue(string attr, string value_Renamed)
		{
			if (allProperties.TryGetValue(attr, out Property p))
				p.UntranslatedValue = value_Renamed;
		}

		/*
		* Return a translation of an attribute
		*/
		public virtual string getTranslatedValue(string attr, Translation translation)
		{
			string fullKey = FullPrefix + attr;
			return translation.translate(fullKey);
		}
		
		/// <summary> Return all child Translatable components of this component
		/// 
		/// </summary>
		/// <returns> Child translatables
		/// </returns>
		public IReadOnlyCollection< Translatable > getChildren()
		{
			return children.AsReadOnly();
		}

		/// <summary> Return true if this component or any of its children have at least one translatable attribute with a non-null value
		/// that does not have a translation in the supplied translation.
		/// 
		/// </summary>
		/// <param name="t">Translation
		/// </param>
		/// <returns> true if translation of this component is not complete
		/// </returns>
		public virtual bool hasUntranslatedAttributes(Translation t)
		{
			if (t == null)
			{
				return false;
			}
			/*
			* Check attributes of this component first
			*/
			foreach (Property p in translatableProperties.Values)
			{
				string currentValue = myComponent.getAttributeValueString(p.Name);
				if (currentValue != null && currentValue.Length > 0)
				{
					string translation = getTranslatedValue(p.Name, t);
					if (translation == null || translation.Length == 0)
					{
						return true;
					}
				}
			}
			/*
			* Check Children
			*/
			foreach (Translatable child in children)
			{
				if (child.I18nData.hasUntranslatedAttributes(t))
				{
					return true;
				}
			}
			/*
			* Nothing left untranslated!
			*/
			return false;
		}

        /// <summary>An attribute of a Configurable component that can be translated into another language </summary>
        public class Property
        {
            virtual public string Description
            {
                get
                {
                    return description;
                }

            }
            virtual public string Name
            {
                get
                {
                    return name;
                }

            }
            virtual public string UntranslatedValue
			{
				get
				{
					return untranslatedValue;
				}
				
				set
				{
					this.untranslatedValue = value;
				}
				
			}
			private string name;
			private string description;
			private string untranslatedValue;
			
			public Property(string name, string description):base()
			{
				this.name = name;
				this.description = description;
				this.untranslatedValue = name;
			}
		}

	}
}