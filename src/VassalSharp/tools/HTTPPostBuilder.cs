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
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.tools
{
	
	/// <summary> Builds HTTP POST requests conveniently.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class HTTPPostBuilder
	{
		public HTTPPostBuilder()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
			bw = new System.IO.StreamWriter(new System.IO.StreamWriter(bytes, System.Text.Encoding.Default).BaseStream, new System.IO.StreamWriter(bytes, System.Text.Encoding.Default).Encoding);
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'bytes '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.IO.MemoryStream bytes = new System.IO.MemoryStream();
		//UPGRADE_NOTE: Final was removed from the declaration of 'bw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'bw' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private System.IO.StreamWriter bw;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'boundary '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String boundary = "---------------------------" + randomString() + randomString() + randomString();
		
		private const System.String endl = "\r\n";
		
		private static System.Random rng = new System.Random();
		
		private static System.String randomString()
		{
			//UPGRADE_TODO: Method 'java.util.Random.nextLong' was converted to 'SupportClass.NextLong' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilRandomnextLong'"
			return System.Convert.ToString(SupportClass.NextLong(rng), 36);
		}
		
		/// <summary> Sets a parameter for an HTTP POST request.
		/// 
		/// </summary>
		/// <param name="name">the name of the parameter
		/// </param>
		/// <param name="value">the value of the parameter
		/// </param>
		/// <throws>  IOException in case of failure </throws>
		public virtual void  setParameter(System.String name, System.String value_Renamed)
		{
			bw.append("--").append(boundary).append(endl).append("Content-Disposition: form-data; name=\"").append(name).append(endl).append(endl).append(value_Renamed).append(endl);
		}
		
		/// <summary> Sets a file to be uploaded as part of an HTTP POST request.
		/// 
		/// </summary>
		/// <param name="name">the name of the parameter
		/// </param>
		/// <param name="file">the path to the file
		/// </param>
		/// <throws>  IOException in case of failure </throws>
		public virtual void  setParameter(System.String name, System.IO.FileInfo file)
		{
			System.IO.FileStream in_Renamed = null;
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
				in_Renamed = new System.IO.FileStream(file.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
				setParameter(name, file.FullName, in_Renamed);
				in_Renamed.Close();
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
		
		/// <summary> Sets a file to be uploaded as part of an HTTP POST request.
		/// 
		/// </summary>
		/// <param name="name">the name of the parameter
		/// </param>
		/// <param name="filename">the path to the file
		/// </param>
		/// <param name="in">an <code>InputStream</code> from which to read the file
		/// </param>
		/// <throws>  IOException in case of failure </throws>
		public virtual void  setParameter(System.String name, System.String filename, System.IO.Stream in_Renamed)
		{
			// write out the headers
			writeCommonFileHeaders(name, filename);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.net.URLConnection.guessContentTypeFromName' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLConnectionguessContentTypeFromName_javalangString'"
			System.String type = HttpURLConnection.guessContentTypeFromName(filename);
			bw.append(type == null?"application/octet-stream":type).append(endl).append(endl);
			
			// flush before we switch to writing bytes
			bw.Flush();
			
			// write the file to the byte buffer
			IOUtils.copy(in_Renamed, bytes);
			
			bw.append(endl);
		}
		
		/// <summary> Sets a file to be uploaded as part of an HTTP POST request.
		/// 
		/// </summary>
		/// <param name="name">the name of the parameter
		/// </param>
		/// <param name="filename">the path to the file
		/// </param>
		/// <param name="contents">a <code>String</code> containing the file
		/// </param>
		/// <throws>  IOException in case of failure </throws>
		public virtual void  setParameter(System.String name, System.String filename, System.String contents)
		{
			// write out the headers
			writeCommonFileHeaders(name, filename);
			
			// write out the contents at UTF-8
			bw.append("text/plain; charset=\"UTF-8\"").append(endl).append(endl).append(contents).append(endl);
		}
		
		private void  writeCommonFileHeaders(System.String name, System.String filename)
		{
			bw.append("--").append(boundary).append(endl).append("Content-Disposition: form-data; name=\"").append(name).append("\"; filename=\"").append(filename).append(endl).append("Content-Type: ");
		}
		
		private void  writeEnd()
		{
			bw.append("--").append(boundary).append("--").append(endl);
			bw.Close();
		}
		
		/// <summary> Submits an HTTP POST request to the given URL.
		/// This convenience method is equivalent to
		/// <code>HTTPPostBuilder.post(new URL(url))</code>.
		/// 
		/// </summary>
		/// <param name="url">the URL to receive the POST request
		/// </param>
		/// <returns> the reply
		/// </returns>
		/// <throws>  IOException in case of failure </throws>
		public virtual System.IO.Stream post(System.String url)
		{
			//UPGRADE_TODO: Class 'java.net.URL' was converted to a 'System.Uri' which does not throw an exception if a URL specifies an unknown protocol. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1132'"
			return post(new System.Uri(url));
		}
		
		/// <summary> Submits an HTTP POST request to the given URL.
		/// 
		/// </summary>
		/// <param name="url">the URL to receive the POST request
		/// </param>
		/// <returns> the reply
		/// </returns>
		/// <throws>  IOException in case of failure </throws>
		public virtual System.IO.Stream post(System.Uri url)
		{
			writeEnd();
			
			System.IO.Stream out_Renamed = null;
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'http '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Net.HttpWebRequest http = (System.Net.HttpWebRequest) System.Net.WebRequest.Create(url);
				
				http.Method = "POST";
				//UPGRADE_ISSUE: Method 'java.net.URLConnection.setDoInput' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLConnectionsetDoInput_boolean'"
				http.setDoInput(true);
				//UPGRADE_ISSUE: Method 'java.net.URLConnection.setDoOutput' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLConnectionsetDoOutput_boolean'"
				http.setDoOutput(true);
				//UPGRADE_ISSUE: Method 'java.net.URLConnection.setUseCaches' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLConnectionsetUseCaches_boolean'"
				http.setUseCaches(false);
				//UPGRADE_ISSUE: Method 'java.net.URLConnection.setAllowUserInteraction' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLConnectionsetAllowUserInteraction_boolean'"
				http.setAllowUserInteraction(false);
				
				SupportClass.URLConnectionSupport.SetRequestProperty(http, "Content-Type", "multipart/form-data; boundary=" + boundary);
				SupportClass.URLConnectionSupport.SetRequestProperty(http, "Content-Length", System.Convert.ToString(bytes.Length));
				
				out_Renamed = http.GetRequestStream();
				bytes.WriteTo(out_Renamed);
				out_Renamed.Close();
				
				return http.GetResponse().GetResponseStream();
			}
			finally
			{
				IOUtils.closeQuietly(out_Renamed);
			}
		}
	}
}