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
using Configurer = VassalSharp.configure.Configurer;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.i18n
{
	
	/// <summary> Configure a Locale Value using full, localized Language and Country names
	/// 
	/// </summary>
	/// <author>  Brent Easton
	/// 
	/// </author>
	public class LocaleConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(LocaleConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(LocaleConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private LocaleConfigurer enclosingInstance;
			public LocaleConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.updateValue();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(LocaleConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(LocaleConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private LocaleConfigurer enclosingInstance;
			public LocaleConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.updateValue();
			}
		}
		override public System.String ValueString
		{
			get
			{
				return (System.String) value_Renamed;
			}
			
		}
		virtual public System.Globalization.CultureInfo ValueLocale
		{
			get
			{
				return stringToLocale((System.String) value_Renamed);
			}
			
		}
		virtual protected internal System.String Language
		{
			set
			{
				//UPGRADE_WARNING: Constructor 'java.util.Locale.Locale' was converted to 'System.Globalization.CultureInfo' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				System.Globalization.CultureInfo generatedAux = (new System.Globalization.CultureInfo(value + "-" + ""));
				//UPGRADE_TODO: The equivalent in .NET for method 'java.util.Locale.getDisplayLanguage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				//UPGRADE_TODO: Method 'java.util.Locale.getDefault' was converted to 'System.Threading.Thread.CurrentThread.CurrentCulture' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				System.String lang = new System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.LCID).NativeName;
				langBox.SelectedItem = lang;
			}
			
		}
		virtual protected internal System.String Country
		{
			set
			{
				System.String country;
				if (value.Length == 0)
				{
					country = ANY_COUNTRY;
				}
				else
				{
					//UPGRADE_WARNING: Constructor 'java.util.Locale.Locale' was converted to 'System.Globalization.CultureInfo' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
					//UPGRADE_TODO: Method 'java.util.Locale.getLanguage' was converted to 'System.Globalization.CultureInfo.TwoLetterISOLanguageName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilLocalegetLanguage'"
					//UPGRADE_TODO: Method 'java.util.Locale.getDefault' was converted to 'System.Threading.Thread.CurrentThread.CurrentCulture' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					System.Globalization.CultureInfo generatedAux2 = (new System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName + "-" + value));
					//UPGRADE_TODO: The equivalent in .NET for method 'java.util.Locale.getDisplayCountry' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					//UPGRADE_TODO: Method 'java.util.Locale.getDefault' was converted to 'System.Threading.Thread.CurrentThread.CurrentCulture' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					country = new System.Globalization.RegionInfo(System.Threading.Thread.CurrentThread.CurrentCulture.LCID).DisplayName;
				}
				countryBox.SelectedItem = country;
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (panel == null)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					panel = Box.createHorizontalBox();
					langBox = SupportClass.ComboBoxSupport.CreateComboBox(LanguageList);
					//UPGRADE_TODO: The equivalent in .NET for method 'java.util.Locale.getDisplayLanguage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					//UPGRADE_TODO: Method 'java.util.Locale.getDefault' was converted to 'System.Threading.Thread.CurrentThread.CurrentCulture' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					langBox.SelectedItem = System.Threading.Thread.CurrentThread.CurrentCulture.DisplayName;
					langBox.SelectedValueChanged += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(langBox);
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = "Language:  ";
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					panel.Controls.Add(temp_Control);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					panel.Controls.Add(langBox);
					
					countryBox = SupportClass.ComboBoxSupport.CreateComboBox(CountryList);
					countryBox.SelectedItem = ANY_COUNTRY;
					countryBox.SelectedValueChanged += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(countryBox);
					System.Windows.Forms.Label temp_label4;
					temp_label4 = new System.Windows.Forms.Label();
					temp_label4.Text = "  Country:  ";
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control2;
					temp_Control2 = temp_label4;
					panel.Controls.Add(temp_Control2);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					panel.Controls.Add(countryBox);
				}
				return panel;
			}
			
		}
		virtual protected internal System.String[] LanguageList
		{
			get
			{
				if (languageList == null)
				{
					System.String[] langs = SupportClass.GetLanguages();
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					ArrayList < String > sortedLangs = new ArrayList < String >();
					for (int i = 0; i < langs.Length; i++)
					{
						System.Globalization.CultureInfo generatedAux2 = (new Locale(langs[i]));
						//UPGRADE_TODO: The equivalent in .NET for method 'java.util.Locale.getDisplayLanguage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						//UPGRADE_TODO: Method 'java.util.Locale.getDefault' was converted to 'System.Threading.Thread.CurrentThread.CurrentCulture' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
						System.String lang = new System.Globalization.CultureInfo(System.Threading.Thread.CurrentThread.CurrentCulture.LCID).NativeName;
						languages.put(lang, langs[i]);
						sortedLangs.add(lang);
					}
					//UPGRADE_TODO: Method 'java.util.Locale.getDefault' was converted to 'System.Threading.Thread.CurrentThread.CurrentCulture' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					Collections.sort(sortedLangs, System.Threading.Thread.CurrentThread.CurrentCulture.CompareInfo);
					languageList = sortedLangs.toArray(new System.String[sortedLangs.size()]);
				}
				return languageList;
			}
			
		}
		virtual protected internal System.String[] CountryList
		{
			get
			{
				if (countryList == null)
				{
					//UPGRADE_TODO: Method 'java.util.Locale.getISOCountries' was converted to 'SupportClass.GetRegionsCodes' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilLocalegetISOCountries'"
					System.String[] c = SupportClass.GetRegionsCodes();
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					ArrayList < String > sortedCountries = new ArrayList < String >();
					for (int i = 0; i < c.Length; i++)
					{
						//UPGRADE_WARNING: Constructor 'java.util.Locale.Locale' was converted to 'System.Globalization.CultureInfo' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
						System.Globalization.CultureInfo generatedAux2 = (new System.Globalization.CultureInfo("en" + "-" + c[i]));
						//UPGRADE_TODO: The equivalent in .NET for method 'java.util.Locale.getDisplayCountry' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						//UPGRADE_TODO: Method 'java.util.Locale.getDefault' was converted to 'System.Threading.Thread.CurrentThread.CurrentCulture' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
						System.String country = new System.Globalization.RegionInfo(System.Threading.Thread.CurrentThread.CurrentCulture.LCID).DisplayName;
						countries.put(country, c[i]);
						sortedCountries.add(country);
					}
					//UPGRADE_TODO: Method 'java.util.Locale.getDefault' was converted to 'System.Threading.Thread.CurrentThread.CurrentCulture' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					Collections.sort(sortedCountries, System.Threading.Thread.CurrentThread.CurrentCulture.CompareInfo);
					countries.put(ANY_COUNTRY, "");
					sortedCountries.add(0, ANY_COUNTRY);
					countryList = sortedCountries.toArray(new System.String[sortedCountries.size()]);
				}
				return countryList;
			}
			
		}
		
		protected internal const System.String ANY_COUNTRY = "<Any Country>";
		//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
		protected internal Box panel;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static Map < String, String > languages = new HashMap < String, String >();
		protected internal static System.String[] languageList;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static Map < String, String > countries = new HashMap < String, String >();
		protected internal static System.String[] countryList;
		
		protected internal System.Windows.Forms.ComboBox langBox;
		protected internal System.Windows.Forms.ComboBox countryBox;
		
		public LocaleConfigurer(System.String key, System.String name):this(key, name, "")
		{
		}
		
		public LocaleConfigurer(System.String key, System.String name, System.Globalization.CultureInfo locale):base(key, name)
		{
			setValue(locale);
		}
		
		public LocaleConfigurer(System.String key, System.String name, System.String val):base(key, name, val)
		{
			setValue(val);
		}
		
		public virtual void  setValue(System.Globalization.CultureInfo l)
		{
			setValue(localeToString(l));
		}
		
		public override void  setValue(System.String s)
		{
			System.Windows.Forms.Control generatedAux = Controls;
			if (!noUpdate && langBox != null && countryBox != null)
			{
				SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, ',');
				//UPGRADE_TODO: Method 'java.util.Locale.getLanguage' was converted to 'System.Globalization.CultureInfo.TwoLetterISOLanguageName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilLocalegetLanguage'"
				//UPGRADE_TODO: Method 'java.util.Locale.getDefault' was converted to 'System.Threading.Thread.CurrentThread.CurrentCulture' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				Language = sd.nextToken(System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName);
				Country = sd.nextToken("");
			}
			setValue((System.Object) s);
		}
		
		protected internal virtual void  updateValue()
		{
			System.String language = languages.get_Renamed(langBox.SelectedItem);
			System.String country = countries.get_Renamed(countryBox.SelectedItem);
			
			setValue(language + "," + country);
		}
		
		public static System.Globalization.CultureInfo stringToLocale(System.String s)
		{
			SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, ',');
			//UPGRADE_WARNING: Constructor 'java.util.Locale.Locale' was converted to 'System.Globalization.CultureInfo' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
			return new System.Globalization.CultureInfo(sd.nextToken("") + "-" + sd.nextToken(""));
		}
		
		public static System.String localeToString(System.Globalization.CultureInfo l)
		{
			//UPGRADE_TODO: Method 'java.util.Locale.getLanguage' was converted to 'System.Globalization.CultureInfo.TwoLetterISOLanguageName' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilLocalegetLanguage'"
			return l.TwoLetterISOLanguageName + "," + new System.Globalization.RegionInfo(l.LCID).TwoLetterISORegionName;
		}
	}
}