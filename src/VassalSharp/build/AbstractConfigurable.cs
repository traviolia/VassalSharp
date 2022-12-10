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
using Configurer = VassalSharp.configure.Configurer;
using VisibilityCondition = VassalSharp.configure.VisibilityCondition;
using ComponentI18nData = VassalSharp.i18n.ComponentI18nData;
using Localization = VassalSharp.i18n.Localization;
using Translatable = VassalSharp.i18n.Translatable;
using System.Collections.Generic;
using System.ComponentModel;
using static SupportClass;

namespace VassalSharp.build
{
	
	/// <summary> An abstract implementation of the Configurable interface. Takes care of most of the Configurable functionality</summary>
	public abstract class AbstractConfigurable : AbstractBuildable, AutoConfigurable
	{
		[System.ComponentModel.Browsable(true)]
		public event SupportClass.PropertyChangeEventHandler PropertyChange;

		virtual public string LocalizedConfigureName
		{
			get
			{
				return localizedName;
			}
			
		}

		/// <summary> 
		/// Return an array of Strings describing the attributes of this object. These strings are used as prompts in the
		/// Properties window for this object. The order of descriptions should be the same as the order of names in
		/// {@link AbstractBuildable#getAttributeNames}
		/// </summary>
		public abstract string[] AttributeDescriptions { get; }

		/// <summary> Return the i18n data for this component</summary>
		virtual public ComponentI18nData I18nData
		{
			get
			{
				if (myI18nData == null)
				{
					myI18nData = new ComponentI18nData(this, getI18nPrefix());
				}
				return myI18nData;
			}
			
		}
		virtual public Configurable[] ConfigureComponents
		{
			get
			{
				List < Configurable > l = new  List < Configurable >();
				foreach (Buildable b in getBuildables())
				{
					if (b is Configurable)
					{
						l.Add((Configurable) b);
					}
				}
				return l.ToArray();
			}
			
		}
		/// <summary> The default {@link Configurer} of an {@link AbstractConfigurable} class is an instance of {@link AutoConfigurer}</summary>
		virtual public Configurer Configurer
		{
			get
			{
				if (config == null)
				{
					config = new AutoConfigurer(this);
				}
				else
				{
					((AutoConfigurer) config).reset();
				}
				return config;
			}
			
		}
		public abstract System.Type[] AllowableConfigureComponents{get;}
		public virtual VassalSharp.build.module.documentation.HelpFile getHelpFile()
		{ 
			return null;
		}

		protected internal string name; // Language-independent name used for programmatic identification
		protected internal string localizedName; // Locale-sensitive name for on-screen display
		protected internal Configurer config;
		protected internal ComponentI18nData myI18nData;
		
		/// <summary> Remove a Buildable object from this object</summary>
		public virtual void  remove(Buildable b)
		{
			buildComponents.Remove(b);
		}
		
		public virtual string getConfigureName()
		{
			return name;
		}
		
		/// <summary> Sets the name and fires a PropertyChangeEvent</summary>
		public virtual void  setConfigureName(string s)
		{
			string oldName = name;
			SupportClass.PropertyChangingEventArgs me = new SupportClass.PropertyChangingEventArgs(VassalSharp.build.Configurable_Fields.NAME_PROPERTY, oldName, s);
			PropertyChange?.Invoke(this, me);

			if (Localization.Instance.TranslationInProgress)
			{
				localizedName = s;
			}
			else if (Localization.Instance.TranslationComplete)
			{
				name = s;
			}
			else
			{
				name = s;
				localizedName = s;
			}
		}
		
		/// <summary> Return the Class for the attributes of this object. Valid classes are: String, Integer, Double, Boolean, Image,
		/// Color, and KeyStroke
		/// The order of classes should be the same as the order of names in {@link AbstractBuildable#getAttributeNames}
		/// </summary>
		public abstract Type[] getAttributeTypes();
		
		/// <summary> 
        /// By default, all attributes are visible
		/// </summary>
		/// <param name="name">
		/// </param>
		/// <returns>
		/// </returns>
		public virtual VisibilityCondition getAttributeVisibility(string name)
		{
			return null;
		}
		
		/// <summary>
        /// Generate a standard prefix for i18n keys for attributes of this component - Classname.attributeName
        /// </summary>
		public virtual string getI18nPrefix()
		{
			string key = this.GetType().Name;
			if (getConfigureName() != null && getConfigureName().Length > 0)
			{
				key += ("." + getConfigureName());
			}
			return key + ".";
		}
		
		/// <summary>
        /// Over-ride the default attribute translatability.
        /// This is called by inidivdual components to force specific
		/// attributes to be translatable or not translatable
		/// </summary>
		protected internal virtual void  setAttributeTranslatable(string attr, bool b)
		{
			I18nData.setAttributeTranslatable(attr, b);
		}
		
		protected internal virtual void  setAllAttributesUntranslatable()
		{
			I18nData.setAllAttributesUntranslatable();
		}
		
		/// <summary> Set the owning translatable of this component</summary>
		public override void  add(Buildable b)
		{
			base.add(b);
			if (b is Translatable)
			{
				((Translatable) b).I18nData.OwningComponent = this;
			}
		}
		
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  addPropertyChangeListener(PropertyChangeEventHandler l)
		{
			PropertyChange += l;
		}

		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void removePropertyChangeListener(PropertyChangeEventHandler l)
		{
			PropertyChange -= l;
		}
		public abstract void  removeFrom(VassalSharp.build.Buildable param1);
	}
}