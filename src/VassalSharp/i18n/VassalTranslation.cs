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
using ReadErrorDialog = VassalSharp.tools.ReadErrorDialog;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.i18n
{
	
	/// <summary> Utility class to allow translation of VASSAL using the Component
	/// Translation mechanism.
	/// 
	/// </summary>
	/// <author>  Brent Easton
	/// </author>
	public class VassalTranslation:Translation
	{
		private void  InitBlock()
		{
			initkeys();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Class < ? > [] types = new Class < ? > [allKeys.length];
			Arrays.fill(types, typeof(System.String));
			return types;
		}
		virtual protected internal System.String Description
		{
			get
			{
				return "VASSAL";
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				initkeys();
				return allKeys;
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				initkeys();
				return allKeys;
			}
			
		}
		protected internal System.String[] allKeys;
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
		protected internal System.Collections.Specialized.NameValueCollection baseValues = new System.Collections.Specialized.NameValueCollection();
		
		public VassalTranslation()
		{
			InitBlock();
			setConfigureName("VASSAL");
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'is '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
			System.IO.Stream is_Renamed = GetType().getResourceAsStream("VassalSharp.properties");
			try
			{
				if (is_Renamed != null)
				{
					System.IO.BufferedStream in_Renamed = null;
					try
					{
						in_Renamed = new System.IO.BufferedStream(is_Renamed);
						//UPGRADE_TODO: Method 'java.util.Properties.load' was converted to 'System.Collections.Specialized.NameValueCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiesload_javaioInputStream'"
						baseValues = new System.Collections.Specialized.NameValueCollection(System.Configuration.ConfigurationSettings.AppSettings);
						in_Renamed.Close();
					}
					finally
					{
						IOUtils.closeQuietly(in_Renamed);
					}
				}
				else
				{
					throw new System.IO.FileNotFoundException("VassalSharp.properties not found");
				}
			}
			catch (System.IO.IOException e)
			{
				ReadErrorDialog.error(e, "VassalSharp.properties");
			}
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'getI18nPrefix' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override System.String getI18nPrefix()
		{
			return "";
		}
		
		public virtual void  clearProperties()
		{
			//UPGRADE_TODO: Format of property file may need to be changed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1089'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.util.Properties' and 'System.Collections.Specialized.NameValueCollection' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			localProperties = new System.Collections.Specialized.NameValueCollection();
			dirty = false;
			allKeys = null;
		}
		
		public override System.String getAttributeValueString(System.String key)
		{
			return baseValues.Get(key);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		
		protected internal virtual void  initkeys()
		{
			if (allKeys == null)
			{
				allKeys = Resources.getVassalKeys().toArray(new System.String[0]);
			}
		}
		
		public virtual void  saveProperties(System.IO.FileInfo file, System.Globalization.CultureInfo locale)
		{
			System.IO.BufferedStream out_Renamed = null;
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javaioFile'"
				out_Renamed = new System.IO.BufferedStream(new System.IO.FileStream(file.FullName, System.IO.FileMode.Create));
				//UPGRADE_ISSUE: Method 'java.util.Properties.store' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilPropertiesstore_javaioOutputStream_javalangString'"
				localProperties.store(out_Renamed, locale.DisplayName);
				out_Renamed.Close();
				dirty = false;
			}
			finally
			{
				IOUtils.closeQuietly(out_Renamed);
			}
		}
		
		protected internal virtual void  loadProperties(System.IO.Stream in_Renamed)
		{
			try
			{
				//UPGRADE_TODO: Method 'java.util.Properties.load' was converted to 'System.Collections.Specialized.NameValueCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilPropertiesload_javaioInputStream'"
				localProperties = new System.Collections.Specialized.NameValueCollection(System.Configuration.ConfigurationSettings.AppSettings);
				dirty = false;
				in_Renamed.Close();
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
	}
}