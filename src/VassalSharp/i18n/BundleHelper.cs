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

using Microsoft.Extensions.Logging;

using System;

namespace VassalSharp.i18n
{
	
	/// <summary> Utility class for extracting strings from a {@link ResourceBundle}.
	/// 
	/// </summary>
	/// <author>  rodneykinney
	/// </author>
	public class BundleHelper
	{
		virtual public System.Resources.ResourceManager ResourceBundle
		{
			get
			{
				return bundle;
			}
			
		}
		private static readonly ILogger logger;
		
		private System.Resources.ResourceManager bundle;
		
		public BundleHelper(System.Resources.ResourceManager bundle)
		{
			this.bundle = bundle;
		}
		
		public virtual System.String getString(System.String id)
		{
			try
			{
				//UPGRADE_TODO: Method 'java.util.ResourceBundle.getString' was converted to 'System.Resources.ResourceManager.GetString()' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilResourceBundlegetString_javalangString'"
				return bundle.GetString(id);
			}
			catch (System.InvalidCastException e)
			{
				logger.LogError("No Translation: " + id);
			}
			catch (System.Resources.MissingManifestResourceException e)
			{
				logger.LogError("No Translation: " + id);
			}
			
			// fallback: return the key
			return id;
		}

#if NEVER_DEFINED
		private void InitBlock()
		{
			try
			{
				return String.format(getString(id), args);
			}
			catch (IllegalFormatException e)
			{
				logger.error("Illegal Message Format: " + id);
			}

			// fallback: return the key
			return id;
		}
		public System.String getString_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(String id, Object...args)
		static BundleHelper()
		{
			logger = LoggerFactory.getLogger(typeof(BundleHelper));
		}
#endif
	}
}