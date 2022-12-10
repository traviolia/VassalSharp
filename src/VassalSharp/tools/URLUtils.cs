/*
* $Id$
*
* Copyright (c) 2008-2009 by Joel Uckelman
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
namespace VassalSharp.tools
{
	
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class URLUtils
	{
		private URLUtils()
		{
		}
		
		/// <summary> Returns a URL corresponding to a file.
		/// 
		/// </summary>
		/// <param name="f">the file for which the URL is wanted
		/// </param>
		/// <returns> the URL of the file
		/// </returns>
		/// <throws>  MalformedURLException if the URL can't be created </throws>
		public static System.Uri toURL(System.String f)
		{
			return toURL(new System.IO.FileInfo(f));
		}
		
		/// <summary> Returns a URL corresponding to a file.
		/// 
		/// </summary>
		/// <param name="f">the file for which the URL is wanted
		/// </param>
		/// <returns> the URL of the file
		/// </returns>
		/// <throws>  MalformedURLException if the URL can't be created </throws>
		public static System.Uri toURL(System.IO.FileInfo f)
		{
			return f.toURI().toURL();
		}
		
		/// <summary> Returns a URL corresponding to a JAR file.
		/// 
		/// </summary>
		/// <param name="f">the JAR file for which the URL is wanted
		/// </param>
		/// <returns> the URL of the JAR file
		/// </returns>
		/// <throws>  MalformedURLException if the URL can't be created </throws>
		public static System.Uri toJarURL(System.String f)
		{
			return toJarURL(new System.IO.FileInfo(f));
		}
		
		/// <summary> Returns a URL corresponding to a JAR file.
		/// 
		/// </summary>
		/// <param name="f">the JAR file for which the URL is wanted
		/// </param>
		/// <returns> the URL of the JAR file
		/// </returns>
		/// <throws>  MalformedURLException if the URL can't be created </throws>
		public static System.Uri toJarURL(System.IO.FileInfo f)
		{
			// As as workaround for Sun Bug 4523159, we urlencode all '!' in
			// our inner URL to prevent these from being misinterpreted by
			// Class.getResourceAsStream() as the JAR marker "!/".
			//UPGRADE_TODO: Class 'java.net.URL' was converted to a 'System.Uri' which does not throw an exception if a URL specifies an unknown protocol. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1132'"
			return new System.Uri("jar:" + toURL(f).ToString().replace("!", "%21") + "!/");
		}
	}
}