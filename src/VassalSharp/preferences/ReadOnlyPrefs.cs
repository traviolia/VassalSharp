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
using Info = VassalSharp.Info;
using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.preferences
{
	
	/// <summary> A simple preferences class which permits reading stored values.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class ReadOnlyPrefs
	{
		/// <summary> Return the module-independent global preferences.
		/// 
		/// </summary>
		/// <returns> a global preferences object
		/// </returns>
		public static ReadOnlyPrefs GlobalPrefs
		{
			get
			{
				return new ReadOnlyPrefs(new System.IO.FileInfo(Info.PrefsDir.FullName + "\\" + "V_Global"));
			}
			
		}
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
		protected internal System.Collections.Specialized.NameValueCollection storedValues = new System.Collections.Specialized.NameValueCollection();
		
		/// <param name="name">the module name of the preferences to read
		/// </param>
		public ReadOnlyPrefs(System.String name):this(new System.IO.FileInfo(Info.PrefsDir.FullName + "\\" + Prefs.sanitize(name)))
		{
		}
		
		protected internal ReadOnlyPrefs(System.IO.FileInfo file)
		{
			System.IO.BufferedStream in_Renamed = null;
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
				in_Renamed = new System.IO.BufferedStream(new System.IO.FileStream(file.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read));
				//UPGRADE_TODO: Method 'java.util.Properties.load' was converted to 'System.Collections.Specialized.NameValueCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiesload_javaioInputStream'"
				storedValues = new System.Collections.Specialized.NameValueCollection(System.Configuration.ConfigurationSettings.AppSettings);
				in_Renamed.Close();
			}
			catch (System.IO.FileNotFoundException e)
			{
				// First time for this module, not an error.
			}
			catch (System.IO.IOException e)
			{
				ReadErrorDialog.error(e, file);
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
		
		/// <summary> Return the value of a given preference.
		/// 
		/// </summary>
		/// <param name="key">the name of the preference to retrieve
		/// </param>
		/// <returns> the value of this option in the Preferences file, or
		/// <code>null</code> if undefined
		/// </returns>
		public virtual System.String getStoredValue(System.String key)
		{
			return storedValues.Get(key);
		}
	}
}