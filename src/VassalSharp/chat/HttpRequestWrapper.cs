/*
* $Id$
*
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
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.chat
{
	
	/// <summary> Performs Get and Post operations to a given URL</summary>
	public class HttpRequestWrapper
	{
		private void  InitBlock()
		{
			return doGet("", p); //$NON-NLS-1$
			System.IO.StreamReader in_Renamed = null;
			try
			{
				//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				in_Renamed = new System.IO.StreamReader(new InputStreamReader(is_Renamed, "UTF-8").BaseStream, new InputStreamReader(is_Renamed, "UTF-8").CurrentEncoding); //$NON-NLS-1$
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final ArrayList < String > l = new ArrayList < String >();
				System.String line;
				while ((line = in_Renamed.ReadLine()) != null)
					l.add(line);
				
				return l;
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
			url = baseURL + url;
			if (props != null)
			{
				url += "?"; //$NON-NLS-1$
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Enumeration < ? > e = props.keys();
				e.hasMoreElements();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				)
				{
					System.String key = (System.String) e.nextElement();
					System.String value_Renamed = props.getProperty(key);
					url += key + "=" + URLEncoder.encode(value_Renamed, "UTF-8"); //$NON-NLS-1$ //$NON-NLS-2$
					if (e.hasMoreElements())
					{
						url += "&"; //$NON-NLS-1$
					}
				}
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'conn '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Net.HttpWebRequest conn = (System.Net.HttpWebRequest) System.Net.WebRequest.Create(new URL(url));
			//UPGRADE_ISSUE: Method 'java.net.URLConnection.setUseCaches' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLConnectionsetUseCaches_boolean'"
			conn.setUseCaches(false);
			return readLines(conn.GetResponse().GetResponseStream());
			return doPost("", p); //$NON-NLS-1$
			url = baseURL + url;
			System.String content = ""; //$NON-NLS-1$
			if (props != null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Enumeration < ? > e = props.keys();
				e.hasMoreElements();
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				)
				{
					System.String key = (System.String) e.nextElement();
					System.String value_Renamed = props.getProperty(key);
					content += (key + "=" + URLEncoder.encode(value_Renamed, "UTF-8")); //$NON-NLS-1$ //$NON-NLS-2$
					if (e.hasMoreElements())
					{
						content += "&"; //$NON-NLS-1$
					}
				}
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'conn '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Net.HttpWebRequest conn = (System.Net.HttpWebRequest) System.Net.WebRequest.Create(new URL(url));
			//UPGRADE_ISSUE: Method 'java.net.URLConnection.setDoInput' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLConnectionsetDoInput_boolean'"
			conn.setDoInput(true);
			//UPGRADE_ISSUE: Method 'java.net.URLConnection.setDoOutput' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLConnectionsetDoOutput_boolean'"
			conn.setDoOutput(true);
			//UPGRADE_ISSUE: Method 'java.net.URLConnection.setUseCaches' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLConnectionsetUseCaches_boolean'"
			conn.setUseCaches(false);
			//      conn.setRequestProperty("Content-Type","application/x-www-form-urlencoded");
			
			//UPGRADE_TODO: Class 'java.io.DataOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataOutputStream'"
			System.IO.BinaryWriter out_Renamed = null;
			try
			{
				//UPGRADE_TODO: Class 'java.io.DataOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataOutputStream'"
				out_Renamed = new System.IO.BinaryWriter(conn.GetRequestStream());
				out_Renamed.Write(content);
			}
			finally
			{
				IOUtils.closeQuietly(out_Renamed);
			}
			
			return readLines(conn.GetResponse().GetResponseStream());
		}
		private System.String baseURL;
		
		public HttpRequestWrapper(System.String baseURL)
		{
			InitBlock();
			this.baseURL = baseURL;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > doGet(Properties p) throws IOException
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < String > readLines(InputStream is) throws IOException
		
		/// <summary> Perform a GET request</summary>
		/// <param name="url">the URL relative to the base URL
		/// </param>
		/// <param name="props">additional query parameters
		/// </param>
		/// <returns> a List of Strings, one for each line in the response
		/// </returns>
		/// <throws>  IOException </throws>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > doGet(String url, 
		Properties props) throws IOException
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > doPost(Properties p) throws IOException
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > doPost(String url, 
		Properties props) throws IOException
	}
}