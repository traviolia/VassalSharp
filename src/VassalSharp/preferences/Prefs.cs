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
using System.Collections.Generic;

////UPGRADE_TODO: The type 'java.io.Closeable' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using Closeable = java.io.Closeable;
////UPGRADE_TODO: The type 'java.nio.channels.Channels' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using Channels = java.nio.channels.Channels;
////UPGRADE_TODO: The type 'java.nio.channels.FileChannel' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using FileChannel = java.nio.channels.FileChannel;
////UPGRADE_TODO: The type 'java.nio.channels.FileLock' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using FileLock = java.nio.channels.FileLock;
////UPGRADE_TODO: The type 'org.apache.commons.io.FileUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using FileUtils = org.apache.commons.io.FileUtils;
////UPGRADE_TODO: The type 'org.apache.commons.lang.SystemUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
//using SystemUtils = org.apache.commons.lang.SystemUtils;

//using WizardSupport = VassalSharp.build.module.WizardSupport;

//using Info = VassalSharp.Info;
//using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
//using Configurer = VassalSharp.configure.Configurer;
//using DirectoryConfigurer = VassalSharp.configure.DirectoryConfigurer;
//using Resources = VassalSharp.i18n.Resources;
//using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
//using IOUtils = VassalSharp.tools.io.IOUtils;

namespace VassalSharp.preferences
{
	
	/// <summary> A set of preferences. Each set of preferences is identified by a name, and different sets may share a common editor,
	/// which is responsible for writing the preferences to disk
	/// </summary>
	public class Prefs : IDisposable
    {
        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
         }
        #endregion

        //      public PrefsEditor Editor
        //{
        //	get
        //	{
        //		return editor;
        //	}

        //}
        //virtual public System.IO.FileInfo File
        //{
        //	get
        //	{
        //		return file;
        //	}

        //}
        ///// <summary> A global set of preferences that exists independent of any individual module.
        ///// 
        ///// </summary>
        ///// <returns> the global <code>Prefs</code> object
        ///// </returns>
        //public static Prefs GlobalPrefs
        //{
        //	get
        //	{
        //		if (globalPrefs == null)
        //		{
        //			//UPGRADE_NOTE: Final was removed from the declaration of 'ed '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        //			PrefsEditor ed = new PrefsEditor();
        //			// The underscore prevents collisions with module prefs
        //			globalPrefs = new Prefs(ed, new System.IO.FileInfo(Info.PrefsDir.FullName + "\\" + "V_Global"));

        //			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        //			DirectoryConfigurer c = new DirectoryConfigurer(MODULES_DIR_KEY, null);
        //			//UPGRADE_TODO: Method 'java.lang.System.getProperty' was converted to 'System.Environment.GetEnvironmentVariable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangSystemgetProperty_javalangString'"
        //			c.setValue(new System.IO.FileInfo(System.Environment.GetEnvironmentVariable("userprofile")));
        //			globalPrefs.addOption(null, c);
        //		}

        //		return globalPrefs;
        //	}

        //}
        ///// <summary>Preferences key for the directory containing modules </summary>
        //public const string MODULES_DIR_KEY = "modulesDir"; // $NON_NLS-1$
        //public const string DISABLE_D3D = "disableD3d";

        //private static Prefs globalPrefs;

        ////UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
        //private Dictionary< String, Configurer > options = new Dictionary < String, Configurer >();
        ////UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
        ////UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
        //private System.Collections.Specialized.NameValueCollection storedValues = new System.Collections.Specialized.NameValueCollection();
        //private PrefsEditor editor;
        //private System.IO.FileInfo file;

        //public Prefs(PrefsEditor editor, string name):this(editor, new System.IO.FileInfo(Info.PrefsDir.FullName + "\\" + sanitize(name)))
        //{
        //}

        //protected internal Prefs(PrefsEditor editor, System.IO.FileInfo file)
        //{
        //	this.editor = editor;
        //	this.file = file;

        //	read();

        //	// FIXME: Use stringPropertyNames() in 1.6+
        //	// for (String key : storedValues.stringPropertyNames()) {
        //	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
        //	foreach (string key in storedValues.Keys)
        //	{
        //		string value_Renamed = storedValues.Get(key);
        //		options.TryGetValue(key, out Configurer c);
        //		if (c != null)
        //		{
        //			c.setValue(value_Renamed);
        //		}
        //	}

        //	editor.addPrefs(this);
        //}

        //public virtual void  addOption(Configurer o)
        //{
        //	addOption(Resources.getString("Prefs.general_tab"), o); //$NON-NLS-1$
        //}

        //public virtual void  addOption(string category, Configurer o)
        //{
        //	addOption(category, o, null);
        //}

        ///// <summary> Add a configurable property to the preferences in the given category
        ///// 
        ///// </summary>
        ///// <param name="category">the tab under which to add the Configurer's controls in the editor window. If null, do not add controls.
        ///// 
        ///// </param>
        ///// <param name="prompt">If non-null and the value was not read from the preferences file on initialization (i.e. first-time
        ///// setup), prompt the user for an initial value
        ///// </param>
        //public virtual void  addOption(string category, Configurer o, string prompt)
        //{
        //	if (o != null && ! options.ContainsKey(o.Key))
        //	{
        //		options.Add(o.Key, o);
        //		string val = storedValues.Get(o.Key);
        //		if (val != null)
        //		{
        //			o.setValue(val);
        //			prompt = null;
        //		}
        //		if (category != null && o.Controls != null)
        //		{
        //			editor.addOption(category, o, prompt);
        //		}
        //	}
        //}

        //public virtual void  setValue(string option, System.Object value_Renamed)
        //{
        //	options.TryGetValue(option, out Configurer c);
        //	c?.setValue(value_Renamed);
        //}

        //public virtual Configurer getOption(string s)
        //{
        //	options.TryGetValue(s, out Configurer c);
        //	return c;
        //}

        ///// <param name="key">
        ///// </param>
        ///// <returns> the value of the preferences setting stored under key
        ///// </returns>
        //public virtual System.Object getValue(string key)
        //{
        //	options.TryGetValue(key, out Configurer c);
        //	return c?.getValue();
        //}

        ///// <summary> Return the value of a given preference.
        ///// 
        ///// </summary>
        ///// <param name="key">the name of the preference to retrieve
        ///// </param>
        ///// <returns> the value of this option read from the Preferences file at startup, or <code>null</code> if no value is
        ///// undefined
        ///// </returns>
        //public virtual string getStoredValue(string key)
        //{
        //	return storedValues.Get(key);
        //}

        //public static string sanitize(string str)
        //{
        //	/*
        //	Java gives us no way of checking whether a string is a valid
        //	filename on the filesystem we're using. Filenames matching
        //	[0-9A-Za-z_]+ are safe pretty much everywhere. Any code point
        //	in [0-9A-Za-z] is passed through; every other code point c is
        //	escaped as "_hex(c)_". This mapping is a surjection and will
        //	produce filenames safe on every sane filesystem, so long as the
        //	input strings are not too long.
        //	*/
        //	System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //	for (int i = 0; i < str.Length; ++i)
        //	{
        //		//UPGRADE_NOTE: Final was removed from the declaration of 'cp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        //		int cp = (int) str[i];
        //		if (('0' <= cp && cp <= '9') || ('A' <= cp && cp <= 'Z') || ('a' <= cp && cp <= 'z'))
        //		{
        //			sb.Append((char) cp);
        //		}
        //		else
        //		{
        //			sb.Append('_').Append(System.Convert.ToString(cp, 16).ToUpper()).Append('_');
        //		}
        //	}

        //	return sb.ToString();
        //}

        //protected internal virtual void  read()
        //{
        //	System.IO.Stream in_Renamed = null;
        //	try
        //	{
        //		//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
        //		in_Renamed = new System.IO.BufferedStream(new System.IO.FileStream(file.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read));
        //		storedValues.Clear();
        //		//UPGRADE_TODO: Method 'java.util.Properties.load' was converted to 'System.Collections.Specialized.NameValueCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiesload_javaioInputStream'"
        //		storedValues = new System.Collections.Specialized.NameValueCollection(System.Configuration.ConfigurationSettings.AppSettings);
        //		in_Renamed.Close();
        //	}
        //	catch (System.IO.FileNotFoundException e)
        //	{
        //		// First time for this module, not an error.
        //	}
        //	catch (System.IO.IOException e)
        //	{
        //		ReadErrorDialog.errorNoI18N(e, file);
        //	}
        //	finally
        //	{
        //		IOUtils.closeQuietly(in_Renamed);
        //	}
        //}

        ///// <summary> Store this set of preferences</summary>
        //public virtual void  save()
        //{
        //	throw new NotImplementedException();

        //	//storedValues.Clear();

        //	//// ensure that the prefs dir exists
        //	//bool tmpBool;
        //	//if (System.IO.File.Exists(Info.PrefsDir.FullName))
        //	//	tmpBool = true;
        //	//else
        //	//	tmpBool = System.IO.Directory.Exists(Info.PrefsDir.FullName);
        //	//if (!tmpBool)
        //	//{
        //	//	FileUtils.forceMkdir(Info.PrefsDir);
        //	//}

        //	////UPGRADE_TODO: Class 'java.io.RandomAccessFile' was converted to 'System.IO.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioRandomAccessFile'"
        //	//System.IO.FileStream raf = null;
        //	//try
        //	//{
        //	//	raf = SupportClass.RandomAccessFileSupport.CreateRandomAccessFile(file, "rw");
        //	//	//UPGRADE_NOTE: Final was removed from the declaration of 'ch '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        //	//	FileChannel ch = raf.getChannel();

        //	//	// lock the prefs file
        //	//	//UPGRADE_NOTE: Final was removed from the declaration of 'lock '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        //	//	FileLock lock_Renamed = ch.lock_Renamed();

        //	//	// read the old key-value pairs
        //	//	//UPGRADE_NOTE: Final was removed from the declaration of 'in '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        //	//	System.IO.Stream in_Renamed = Channels.newInputStream(ch);
        //	//	//UPGRADE_TODO: Method 'java.util.Properties.load' was converted to 'System.Collections.Specialized.NameValueCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiesload_javaioInputStream'"
        //	//	storedValues = new System.Collections.Specialized.NameValueCollection(System.Configuration.ConfigurationSettings.AppSettings);

        //	//	// merge in the current key-value pairs
        //	//	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
        //	//	for(Configurer c: options.values())
        //	//	{
        //	//		//UPGRADE_NOTE: Final was removed from the declaration of 'val '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        //	//		string val = c.getValueString();
        //	//		if (val != null)
        //	//		{
        //	//			storedValues[(string) c.getKey()] = (string) val;
        //	//		}
        //	//	}

        //	//	// write back the key-value pairs
        //	//	ch.truncate(0);
        //	//	ch.position(0);
        //	//	//UPGRADE_NOTE: Final was removed from the declaration of 'out '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
        //	//	System.IO.Stream out_Renamed = Channels.newOutputStream(ch);
        //	//	//UPGRADE_ISSUE: Method 'java.util.Properties.store' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilPropertiesstore_javaioOutputStream_javalangString'"
        //	//	storedValues.store(out_Renamed, null);
        //	//	out_Renamed.Flush();
        //	//}
        //	//finally
        //	//{
        //	//	// also closes the channel, the streams, and releases the lock
        //	//	IOUtils.closeQuietly(raf);
        //	//}
        //}

        ///// <summary>Save these preferences and write to disk. </summary>
        //public virtual void  write()
        //{
        //	save();
        //}

        //public virtual void  close()
        //{
        //	save();
        //	if (this == globalPrefs)
        //	{
        //		globalPrefs = null;
        //	}
        //}

        ///// <summary> Initialize visible Global Preferences that are shared between the
        ///// Module Manager and the Editor/Player.
        ///// 
        ///// </summary>
        //public static void  initSharedGlobalPrefs()
        //{
        //	VassalSharp.preferences.Prefs generatedAux = GlobalPrefs;


        //	System.Boolean tempAux2 = true;
        //	BooleanConfigurer wizardConf = new BooleanConfigurer(WizardSupport.WELCOME_WIZARD_KEY, Resources.getString("WizardSupport.ShowWizard"), ref tempAux2);

        //	globalPrefs.addOption(wizardConf);
        //}
    }
}