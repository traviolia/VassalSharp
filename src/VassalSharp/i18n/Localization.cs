/*
 * Copyright (c) 2000-2007 by Rodney Kinney
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

using GameModule = VassalSharp.build.GameModule;

using SingleChildInstance = VassalSharp.configure.SingleChildInstance;

namespace VassalSharp.i18n
{
	/// <summary>
	/// Singleton class for managing the translation of a module into other languages</summary>
	/// <author>
	/// rodneykinney
	/// </author>
	public class Localization : Language
	{
		public static Localization Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new Localization();
				}
				return instance;
			}
			
		}
		/// <summary> Return a list of translations available for editing.
		/// 
		/// </summary>
		/// <returns> Array of available translations
		/// </returns>
		virtual public string[] TranslationList
		{
			get
			{
				translations.Sort();
				string[] s = new string[translations.Count];
				int idx = 0;
				foreach (Translation t in translations)
				{
					s[idx++] = t.getDescription();
				}
				return s;
			}
			
		}
		virtual public bool TranslationInProgress
		{
			get
			{
				return translationInProgress;
			}
			
		}
		virtual public bool TranslationComplete
		{
			get
			{
				return translationComplete;
			}
			
		}

		//private static readonly Logger logger;

		private static Localization instance;
		
		private Localization()
		{
			moduleBundle = Resources.MODULE_BUNDLE;
			//UPGRADE_TODO: Method 'java.util.Locale.getLanguage' was converted to 'System.Globalization.CultureInfo.TwoLetterISOLanguageName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilLocalegetLanguage'"
			languageBundle = moduleBundle + "_" + Resources.Locale.TwoLetterISOLanguageName; //$NON-NLS-1$
			countryBundle = languageBundle + "_" + new System.Globalization.RegionInfo(Resources.Locale.LCID).TwoLetterISORegionName; //$NON-NLS-1$
			moduleBundle += ".properties"; //$NON-NLS-1$
			languageBundle += ".properties"; //$NON-NLS-1$
			countryBundle += ".properties"; //$NON-NLS-1$
			validator = new SingleChildInstance(GameModule.getGameModule(), GetType());
		}
		protected internal string moduleBundle;
		protected internal string languageBundle;
		protected internal string countryBundle;
		protected List<Translation> moduleTranslations = new List<Translation>();
		protected List<Translation> languageTranslations = new List<Translation>();
		protected List<Translation> countryTranslations = new List<Translation>();
		protected List<Translation> translations = new List<Translation>();

#if NEVER_DEFINED

		/*
		* Master translation property list
		*/
		protected internal VassalResourceBundle masterBundle;
		
		/// <summary> Return a specified translation
		/// 
		/// </summary>
		/// <param name="description">
		/// </param>
		/// <returns> Translation object
		/// </returns>
		public virtual Translation getTranslation(string description)
		{
			foreach (Translation t in translations)
			{
				if (t.getDescription().equals(description))
				{
					return t;
				}
			}
			return null;
		}
		
#endif
		/// <summary> Record attributes as the module is being built for later translation</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected ISet < TranslatableAttribute > translatableItems = new HashSet < TranslatableAttribute >();
		
		/// <summary> Record an attribute that may need to be translated.
		/// 
		/// </summary>
		/// <param name="component">component to be translated
		/// </param>
		/// <param name="name">Attribute name to be translated
		/// </param>
		/// <param name="value">current value of attribute
		/// </param>
		public virtual void  saveTranslatableAttribute(Translatable component, string name, string value_Renamed)
		{
			if (GameModule.getGameModule().isLocalizationEnabled())
			{
				TranslatableAttribute ta = new TranslatableAttribute(component, name, value_Renamed);
				translatableItems.Add(ta);
			}
		}

		/// <summary> Translate the module. The module and all extensions have now been built,
		/// so all Translations are available and all attributes that need to be
		/// translated have been recorded. There may be multiple translations that
		/// match this Locale, merge them in order - Country over-rides Language
		/// over-rides default. NB - You cannot create a default translation
		/// (Module.properties) using the VASSAL editor, but a default file can be
		/// placed into a module or extension manually.
		/// 
		/// </summary>
		/// <throws>  IOException </throws>
		public virtual void  translate()
		{
			//if (GameModule.getGameModule().isLocalizationEnabled())
			//{
			//	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//	for(Translation t: moduleTranslations)
			//	{
			//		addBundle(t.getBundle());
			//	}
			//	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//	for(Translation t: languageTranslations)
			//	{
			//		addBundle(t.getBundle());
			//	}
			//	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//	for(Translation t: countryTranslations)
			//	{
			//		addBundle(t.getBundle());
			//	}
			//	if (masterBundle != null)
			//	{
			//		translationInProgress = true;
			//		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			//		for(TranslatableAttribute attr: translatableItems)
			//		{
			//			if (attr.isTranslatable())
			//			{
			//				string key = attr.getKey();
			//				try
			//				{
			//					//UPGRADE_TODO: Method 'java.util.ResourceBundle.getString' was converted to 'System.Resources.ResourceManager.GetString()' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilResourceBundlegetString_javalangString'"
			//					string translation = masterBundle.GetString(key);
			//					attr.applyTranslation(translation);
			//				}
			//				catch (System.Resources.MissingManifestResourceException e)
			//				{
			//					// Assume that the translated text is the same as the original
			//				}
			//			}
			//		}
			//		translationInProgress = false;
			//		translationComplete = true;
			//		logger.info("Translated");
			//	}
			//	translatableItems.clear();
			//	GameModule.getGameModule().initFrameTitle();
			//}
		}
#if NEVER_DEFINED		
		/// <summary> Translate an individual attribute.
		/// 
		/// </summary>
		/// <param name="key">Attribute Key
		/// </param>
		/// <param name="defaultValue">Default value if no translation available
		/// </param>
		/// <returns> translation
		/// </returns>
		public virtual string translate(string key, string defaultValue)
		{
			try
			{
				//UPGRADE_TODO: Method 'java.util.ResourceBundle.getString' was converted to 'System.Resources.ResourceManager.GetString()' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilResourceBundlegetString_javalangString'"
				return masterBundle == null?defaultValue:masterBundle.GetString(key);
			}
			catch (System.Resources.MissingManifestResourceException e)
			{
				return defaultValue;
			}
		}
		
		protected internal virtual void  addBundle(VassalResourceBundle child)
		{
			if (masterBundle == null)
			{
				masterBundle = child;
			}
			else
			{
				//UPGRADE_ISSUE: Method 'java.util.ResourceBundle.setParent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilResourceBundlesetParent_javautilResourceBundle'"
				child.setParent(masterBundle);
				masterBundle = child;
			}
		}
#endif

		protected internal bool translationInProgress = false;
		protected internal bool translationComplete = false;

#if NEVER_DEFINED		
		/// <summary> Called whenever a Translation is added to a module or extension.
		/// Check if the translation macthes our locale. If so, add it to the list
		/// of translations to use. There may multiple matching translations at
		/// Country, Language and Module level from different extensions.
		/// 
		/// </summary>
		/// <param name="t">Translation
		/// </param>
		public virtual void  addTranslation(Translation t)
		{
			/*
			* Play and Translate mode - keep a record of all translations that
			* match our locale from various extensions. These will be merged
			* into one after all are loaded.
			*/
			if (GameModule.getGameModule().isLocalizationEnabled())
			{
				Resources.addSupportedLocale(t.getLocale());
				if (moduleBundle.Equals(t.getBundleFileName()))
				{
					moduleTranslations.add(t);
				}
				else if (languageBundle.Equals(t.getBundleFileName()))
				{
					languageTranslations.add(t);
				}
				else if (countryBundle.Equals(t.getBundleFileName()))
				{
					countryTranslations.add(t);
				}
			}
			/*
			* Edit mode, keep a list of all translations available in this
			* Module or Extension to use in drop-down lists
			*/
			else
			{
				translations.add(t);
			}
		}
		
		public virtual void  removeTranslation(Translation t)
		{
			translations.remove(t);
		}
		static Localization()
		{
			logger = LoggerFactory.getLogger(typeof(Localization));
		}

#endif
	}
}