/*
* $Id$
*
* Copyright (c) 2007 by Rodney Kinney, Brent Easton
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
using System.Collections.ObjectModel;
using Info = VassalSharp.Info;
//using StringEnumConfigurer = VassalSharp.build.module.gamepieceimage.StringEnumConfigurer;
using Prefs = VassalSharp.preferences.Prefs;
//using ErrorDialog = VassalSharp.tools.ErrorDialog;
namespace VassalSharp.i18n
{
	
	public class Resources
	{
#if NEVER_DEFINED
		private class AnonymousClassStringEnumConfigurer:StringEnumConfigurer
		{
			public AnonymousClassStringEnumConfigurer(Resources enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDefaultListCellRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			//UPGRADE_ISSUE: Class 'javax.swing.DefaultListCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
			[Serializable]
			private class AnonymousClassDefaultListCellRenderer : DefaultListCellRenderer
			{
				public AnonymousClassDefaultListCellRenderer(AnonymousClassStringEnumConfigurer enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(AnonymousClassStringEnumConfigurer enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private AnonymousClassStringEnumConfigurer enclosingInstance;
				public AnonymousClassStringEnumConfigurer Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				private const long serialVersionUID = 1L;
				
				public System.Windows.Forms.Control getListCellRendererComponent(System.Windows.Forms.ListBox list, System.Object value_Renamed, int index, bool isSelected, bool cellHasFocus)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.DefaultListCellRenderer.getListCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultListCellRenderer'"
					System.Windows.Forms.Label l = (System.Windows.Forms.Label) base.getListCellRendererComponent(list, value_Renamed, index, isSelected, cellHasFocus);
					//UPGRADE_TODO: The equivalent in .NET for method 'java.util.Locale.getDisplayLanguage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					l.Text = new Locale((string) value_Renamed).DisplayName;
					return l;
				}
			}
			private void  InitBlock(Resources enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Resources enclosingInstance;
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					if (box == null)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Windows.Forms.Control c = base.Controls;
						box.setRenderer(new AnonymousClassDefaultListCellRenderer(this));
						return c;
					}
					else
					{
						return base.Controls;
					}
				}
				
			}
			public Resources Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
		}
		private void  InitBlock()
		{
			bundleLoader = new VassalPropertyClassLoader(this);
			return Instance.supportedLocales;
			//UPGRADE_ISSUE: Method 'java.util.ResourceBundle.getKeys' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilResourceBundlegetKeys'"
			return Collections.list(Instance.vassalBundle.ResourceBundle.getKeys());
			//UPGRADE_ISSUE: Method 'java.util.ResourceBundle.getKeys' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilResourceBundlegetKeys'"
			return Collections.list(editorBundle.ResourceBundle.getKeys());
			return Instance.getBundleForKey(id).getString(id, params_Renamed);
		}

#endif
		private static Resources Instance
		{
			get
			{
				lock (typeof(Resources))
				{
					if (instance == null)
					{
						instance = new Resources();
					}
				}
				return instance;
			}
			
		}

#if NEVER_DEFINED
		virtual protected internal BundleHelper EditorBundle
		{
			get
			{
				if (editorBundle == null)
				{
					//UPGRADE_TODO: Make sure that resources used in this class are valid resource files. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1078'"
					System.Threading.Thread.CurrentThread.CurrentUICulture = locale;
					editorBundle = new BundleHelper(System.Resources.ResourceManager.CreateFileBasedResourceManager("VassalSharp.i18n.Editor", "", null));
				}
				return editorBundle;
			}
			
		}
		virtual protected internal BundleHelper VassalBundle
		{
			get
			{
				if (vassalBundle == null)
				{
					//UPGRADE_TODO: Make sure that resources used in this class are valid resource files. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1078'"
					System.Threading.Thread.CurrentThread.CurrentUICulture = locale;
					vassalBundle = new BundleHelper(System.Resources.ResourceManager.CreateFileBasedResourceManager("VassalSharp.i18n.VASSAL", "", null));
				}
				return vassalBundle;
			}
			
		}
#endif
		public static System.Globalization.CultureInfo Locale
		{
			get
			{
				return Instance.locale;
			}
			
			set
			{
				Instance.InstanceLocale = value;
			}
			
		}

		private System.Globalization.CultureInfo InstanceLocale
		{
			set
			{
				locale = value;
				//editorBundle = null;
				//vassalBundle = null;
				////UPGRADE_TODO: Method 'javax.swing.UIManager.put' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UIManager.put("OptionPane.yesButtonText", getInstanceString(YES)); //$NON-NLS-1$
				////UPGRADE_TODO: Method 'javax.swing.UIManager.put' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UIManager.put("OptionPane.cancelButtonText", getInstanceString(CANCEL)); //$NON-NLS-1$
				////UPGRADE_TODO: Method 'javax.swing.UIManager.put' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UIManager.put("OptionPane.noButtonText", getInstanceString(NO)); //$NON-NLS-1$
				////UPGRADE_TODO: Method 'javax.swing.UIManager.put' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				//UIManager.put("OptionPane.okButtonText", getInstanceString(OK)); //$NON-NLS-1$
			}
			
		}
		private static Resources instance;

#if NEVER_DEFINED
		/*
		* Translation of VASSAL is handled by standard Java I18N tools.
		*
		* vassalBundle - Resource Bundle for the VASSAL player interface editorBundle - Resource Bundle for the Module Editor
		*
		* These are implemented as PropertyResourceBundles, normally to be found in the VASSAL jar file. VASSAL will search
		* first in the VASSAL install directory for bundles, then follow the standard Java Class Path
		*/
		protected internal BundleHelper vassalBundle;
		protected internal BundleHelper editorBundle;

		//UPGRADE_NOTE: The initialization of  'bundleLoader' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private VassalPropertyClassLoader bundleLoader;
		
		/// <summary>Preferences key for the user's Locale </summary>
		public const string LOCALE_PREF_KEY = "Locale";
		
		// Note: The Locale ctor takes the lower-case two-letter ISO language code.
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected final List < Locale > supportedLocales = 
		new ArrayList < Locale >(Arrays.asList(
		Locale.ENGLISH, 
		Locale.GERMAN, 
		Locale.FRENCH, 
		Locale.ITALIAN, 
		new Locale(es), // Spanish
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Locale.JAPANESE, 
		new Locale(nl) // Dutch
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		)
		);
#endif

		//UPGRADE_TODO: Method 'java.util.Locale.getDefault' was converted to 'System.Threading.Thread.CurrentThread.CurrentCulture' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		protected internal System.Globalization.CultureInfo locale = System.Threading.Thread.CurrentThread.CurrentCulture;
		protected internal static string DATE_FORMAT = "{0,date}";

#if NEVER_DEFINED		
		private Resources()
		{
			InitBlock();
			init();
		}
		
		private void  init()
		{
			//UPGRADE_TODO: Method 'java.util.Locale.getDefault' was converted to 'System.Threading.Thread.CurrentThread.CurrentCulture' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Globalization.CultureInfo myLocale = System.Threading.Thread.CurrentThread.CurrentCulture;
			
			//UPGRADE_TODO: Make sure that resources used in this class are valid resource files. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1078'"
			System.Threading.Thread.CurrentThread.CurrentUICulture = myLocale;
			System.Resources.ResourceManager rb = System.Resources.ResourceManager.CreateFileBasedResourceManager("VassalSharp.i18n.VASSAL", "", null);
			
			// If the user has a resource bundle for their default language on their
			// local machine, add it to the list of supported locales
			//UPGRADE_TODO: Method 'java.util.Locale.getLanguage' was converted to 'System.Globalization.CultureInfo.TwoLetterISOLanguageName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilLocalegetLanguage'"
			if (System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals(myLocale.TwoLetterISOLanguageName))
			{
				addLocale(myLocale);
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < String > languages = new ArrayList < String >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Locale l: supportedLocales)
			{
				languages.add(l.getLanguage());
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			Prefs p = Prefs.GlobalPrefs;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'savedLocale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			string savedLocale = p.getStoredValue(LOCALE_PREF_KEY);
			
			if (savedLocale == null)
			{
				myLocale = supportedLocales.iterator().next();
			}
			else
			{
				myLocale = new Locale(savedLocale);
			}
			
			InstanceLocale = myLocale;
			//UPGRADE_NOTE: Final was removed from the declaration of 'localeConfig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringEnumConfigurer localeConfig = new AnonymousClassStringEnumConfigurer(this, Resources.LOCALE_PREF_KEY, getInstanceString("Prefs.language"), languages.toArray(new string[languages.size()]));
			
			//UPGRADE_TODO: Method 'java.util.Locale.getLanguage' was converted to 'System.Globalization.CultureInfo.TwoLetterISOLanguageName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilLocalegetLanguage'"
			localeConfig.setValue(locale.TwoLetterISOLanguageName);
			p.addOption(getInstanceString("Prefs.general_tab"), localeConfig);
			
			/*
			new PreferencesEditor() {
			public StringEnumEditor getEditor() {
			return new StringEnumEditor(
			prefs,
			LOCALE,
			getString("Prefs.language"),
			
			);
			}
			};*/
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static Collection < Locale > getSupportedLocales()
		
		public static void  addSupportedLocale(System.Globalization.CultureInfo l)
		{
			Instance.addLocale(l);
		}
		
		private void  addLocale(System.Globalization.CultureInfo l)
		{
			//UPGRADE_TODO: Method 'java.util.Locale.getLanguage' was converted to 'System.Globalization.CultureInfo.TwoLetterISOLanguageName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilLocalegetLanguage'"
			l = new Locale(l.TwoLetterISOLanguageName);
			if (!supportedLocales.contains(l))
			{
				supportedLocales.add(0, l);
				StringEnumConfigurer config = (StringEnumConfigurer) Prefs.GlobalPrefs.getOption(LOCALE_PREF_KEY);
				if (config != null)
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					ArrayList < String > valid = new ArrayList < String >(Arrays
					.asList(config.getValidValues()));
					//UPGRADE_TODO: Method 'java.util.Locale.getLanguage' was converted to 'System.Globalization.CultureInfo.TwoLetterISOLanguageName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilLocalegetLanguage'"
					valid.add(0, l.TwoLetterISOLanguageName);
					config.setValidValues(valid.toArray(new string[valid.size()]));
				}
			}
		}

		public static Collection<String> getVassalKeys()
		{ }

		public Collection<String> getEditorKeys()
		{ }
		
#endif
		/*
		* Translation of individual modules is handled differently.
		* There may be multiple Module.properties file active -
		* Potentially one in the module plus one in each Extension loaded.
		* These will be read into UberProperties structures
		* with each file loaded supplying defaults for subsequent files.
		*/
		protected internal const string MODULE_BUNDLE = "Module"; //$NON-NLS-1$
		
		/*
		* Commonly used i18n keys used in multiple components. By defining them
		* centrally, they will only have to be translated once. Reference to these
		* string should be made as follows:
		*
		* Resources.getString(Resources.VASSAL)
		*/
		public const string VASSAL = "General.VASSAL"; //$NON-NLS-1$
		public const string ADD = "General.add"; //$NON-NLS-1$
		public const string REMOVE = "General.remove"; //$NON-NLS-1$
		public const string INSERT = "General.insert"; //$NON-NLS-1$
		public const string YES = "General.yes"; //$NON-NLS-1$
		public const string NO = "General.no"; //$NON-NLS-1$
		public const string CANCEL = "General.cancel"; //$NON-NLS-1$
		public const string SAVE = "General.save"; //$NON-NLS-1$
		public const string OK = "General.ok"; //$NON-NLS-1$
		public const string MENU = "General.menu"; //$NON-NLS-1$
		public const string LOAD = "General.load"; //$NON-NLS-1$
		public const string QUIT = "General.quit"; //$NON-NLS-1$
		public const string EDIT = "General.edit"; //$NON-NLS-1$
		public const string NEW = "General.new"; //$NON-NLS-1$
		public const string FILE = "General.file"; //$NON-NLS-1$
		public const string TOOLS = "General.tools"; //$NON-NLS-1$
		public const string HELP = "General.help"; //$NON-NLS-1$
		public const string CLOSE = "General.close"; //$NON-NLS-1$
		public const string DATE_DISPLAY = "General.date_display"; //$NON-NLS-1$
		public const string NEXT = "General.next"; //$NON-NLS-1$
		public const string REFRESH = "General.refresh"; //$NON-NLS-1$
		public const string SELECT = "General.select"; //$NON-NLS-1$
		
		/*
		* All i18n keys for the Module Editor must commence with "Editor."
		* This allows us to use a single Resources.getString() call for both
		* resource bundles.
		*/
		public const string EDITOR_PREFIX = "Editor."; //$NON-NLS-1$
		
		/*
		* Common Editor labels that appear in many components.
		*/
		public const string BUTTON_TEXT = "Editor.button_text_label"; //$NON-NLS-1$
		public const string TOOLTIP_TEXT = "Editor.tooltip_text_label"; //$NON-NLS-1$
		public const string BUTTON_ICON = "Editor.button_icon_label"; //$NON-NLS-1$
		public const string HOTKEY_LABEL = "Editor.hotkey_label"; //$NON-NLS-1$
		public const string COLOR_LABEL = "Editor.color_label"; //$NON-NLS-1$
		public const string NAME_LABEL = "Editor.name_label"; //$NON-NLS-1$
		public const string DESCRIPTION = "Editor.description_label"; //$NON-NLS-1$
		/// <summary> Localize a user interface String.
		/// 
		/// </summary>
		/// <param name="id">String Id
		/// </param>
		/// <returns> Localized result
		/// </returns>
		public static string getString(string id)
		{
			return null; // Instance.getInstanceString(id);
		}
		
		private string getInstanceString(string id)
		{
			return null; // getBundleForKey(id).getString(id);
		}

#if NEVER_DEFINED		
		protected internal virtual BundleHelper getBundleForKey(string id)
		{
			return id.StartsWith(EDITOR_PREFIX)?EditorBundle:VassalBundle;
		}
		
		/// <summary> Localize a VASSAL user interface string
		/// 
		/// </summary>
		/// <param name="id">String id
		/// </param>
		/// <returns> Localized result
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public static string getVassalString(string id)
		{
			return Instance.VassalBundle.getString(id);
		}
		
		/// <summary> Localize a VASSAL Module Editor String
		/// 
		/// </summary>
		/// <param name="id">String Id
		/// </param>
		/// <returns> Localized Result
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public static string getEditorString(string id)
		{
			return Instance.EditorBundle.getString(id);
		}
		
		/// <summary> Localize a string using the supplied resource bundle
		/// 
		/// </summary>
		/// <param name="bundle">Resource bundle
		/// </param>
		/// <param name="id">String Id
		/// </param>
		/// <returns> Localized result
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static string getString(System.Resources.ResourceManager bundle, string id)
		{
			string s = null;
			try
			{
				//UPGRADE_TODO: Method 'java.util.ResourceBundle.getString' was converted to 'System.Resources.ResourceManager.GetString()' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilResourceBundlegetString_javalangString'"
				s = bundle.GetString(id);
			}
			catch (System.Exception ex)
			{
				System.Console.Error.WriteLine("No Translation: " + id);
			}
			// 2. Worst case, return the key
			if (s == null)
			{
				s = id;
			}
			return s;
		}
#endif		
		public static string getString(string id, params object[] opt)
		{
			return string.Empty; // find missing body
		}
		
		
		protected internal const string BASE_BUNDLE = "VassalSharp.properties";
		protected internal const string EN_BUNDLE = "VASSAL_en.properties";

#if NEVER_DEFINED
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'VassalPropertyClassLoader' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Custom Class Loader for loading VASSAL property files.
		/// Check first for files in the VASSAL home directory.
		/// 
		/// </summary>
		/// <author>  Brent Easton
		/// </author>
		//UPGRADE_ISSUE: Class 'java.lang.ClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
		public class VassalPropertyClassLoader:ClassLoader
		{
			public VassalPropertyClassLoader(Resources enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(Resources enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private Resources enclosingInstance;
			public Resources Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public System.Uri getResource(string name)
			{
				System.Uri url = getAResource(name);
				
				// If no English translation of Vassal is available (as will usually be the case),
				// drop back to the Base bundle.
				if (url == null && name.EndsWith(VassalSharp.i18n.Resources.EN_BUNDLE))
				{
					url = getAResource(name.Substring(0, (name.LastIndexOf('/') + 1) - (0)) + VassalSharp.i18n.Resources.BASE_BUNDLE);
				}
				
				return url;
			}
			
			public virtual System.Uri getAResource(string name)
			{
				System.Uri url = null;
				//UPGRADE_NOTE: Final was removed from the declaration of 'propFileName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				string propFileName = name.Substring(name.LastIndexOf('/') + 1);
				//UPGRADE_NOTE: Final was removed from the declaration of 'propFile '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.IO.FileInfo propFile = new System.IO.FileInfo(Info.HomeDir.FullName + "\\" + propFileName);
				bool tmpBool;
				if (System.IO.File.Exists(propFile.FullName))
					tmpBool = true;
				else
					tmpBool = System.IO.Directory.Exists(propFile.FullName);
				if (tmpBool)
				{
					try
					{
						url = propFile.toURI().toURL();
					}
					catch (System.UriFormatException e)
					{
						ErrorDialog.bug(e);
					}
				}
				
				// No openable file in home dir, so let Java find one for us in
				// the standard classpath.
				if (url == null)
				{
					//UPGRADE_ISSUE: Method 'java.lang.ClassLoader.getResource' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassLoader'"
					//UPGRADE_ISSUE: Method 'java.lang.Class.getClassLoader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetClassLoader'"
					url = this.GetType().getClassLoader().getResource(name);
				}
				
				return url;
			}
		}
		
		/// <summary> Return a standard formatted localised date</summary>
		/// <param name="date">date to format
		/// </param>
		/// <returns> formatted localized date
		/// </returns>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public static string formatDate(ref System.DateTime date)
		{
			//UPGRADE_TODO: Method 'java.text.Format.format' was converted to 'string.Format' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javatextFormatformat_javalangObject'"
			return string.Format(DATE_FORMAT, new System.Object[]{date});
		}
#endif
	}
}