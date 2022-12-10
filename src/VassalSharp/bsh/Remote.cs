/// <summary>**************************************************************************
/// *
/// This file is part of the BeanShell Java Scripting distribution.          *
/// Documentation and updates may be found at http://www.beanshell.org/      *
/// *
/// Sun Public License Notice:                                               *
/// *
/// The contents of this file are subject to the Sun Public License Version  *
/// 1.0 (the "License"); you may not use this file except in compliance with *
/// the License. A copy of the License is available at http://www.sun.com    * 
/// *
/// The Original Code is BeanShell. The Initial Developer of the Original    *
/// Code is Pat Niemeyer. Portions created by Pat Niemeyer are Copyright     *
/// (C) 2000.  All Rights Reserved.                                          *
/// *
/// GNU Public License Notice:                                               *
/// *
/// Alternatively, the contents of this file may be used under the terms of  *
/// the GNU Lesser General Public License (the "LGPL"), in which case the    *
/// provisions of LGPL are applicable instead of those above. If you wish to *
/// allow use of your version of this file only under the  terms of the LGPL *
/// and not to allow others to use your version of this file under the SPL,  *
/// indicate your decision by deleting the provisions above and replace      *
/// them with the notice and other provisions required by the LGPL.  If you  *
/// do not delete the provisions above, a recipient may use your version of  *
/// this file under either the SPL or the LGPL.                              *
/// *
/// Patrick Niemeyer (pat@pat.net)                                           *
/// Author of Learning Java, O'Reilly & Associates                           *
/// http://www.pat.net/~pat/                                                 *
/// *
/// ***************************************************************************
/// </summary>
using System;
namespace bsh
{
	/// <summary>Remote executor class. Posts a script from the command line to a BshServlet
	/// or embedded  interpreter using (respectively) HTTP or the bsh telnet
	/// service. Output is printed to stdout and a numeric return value is scraped
	/// from the result.
	/// </summary>
	public class Remote
	{
		[STAThread]
		public static void  Main(System.String[] args)
		{
			if (args.Length < 2)
			{
				System.Console.Out.WriteLine("usage: Remote URL(http|bsh) file [ file ] ... ");
				System.Environment.Exit(1);
			}
			System.String url = args[0];
			System.String text = getFile(args[1]);
			int ret = eval(url, text);
			System.Environment.Exit(ret);
		}
		
		/// <summary>Evaluate text in the interpreter at url, returning a possible integer
		/// return value.
		/// </summary>
		public static int eval(System.String url, System.String text)
		{
			System.String returnValue = null;
			if (url.StartsWith("http:"))
			{
				returnValue = doHttp(url, text);
			}
			else if (url.StartsWith("bsh:"))
			{
				returnValue = doBsh(url, text);
			}
			else
				throw new System.IO.IOException("Unrecognized URL type." + "Scheme must be http:// or bsh://");
			
			try
			{
				return System.Int32.Parse(returnValue);
			}
			catch (System.Exception e)
			{
				// this convention may change...
				return 0;
			}
		}
		
		internal static System.String doBsh(System.String url, System.String text)
		{
			System.IO.Stream out_Renamed;
			System.IO.Stream in_Renamed;
			System.String host = "";
			System.String port = "";
			System.String returnValue = "-1";
			System.String orgURL = url;
			
			// Need some format checking here
			try
			{
				url = url.Substring(6); // remove the bsh://
				// get the index of the : between the host and the port is located
				int index = url.IndexOf(":");
				host = url.Substring(0, (index) - (0));
				port = url.Substring(index + 1, (url.Length) - (index + 1));
			}
			catch (System.Exception ex)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Error.WriteLine("Bad URL: " + orgURL + ": " + ex);
				return returnValue;
			}
			
			try
			{
				System.Console.Out.WriteLine("Connecting to host : " + host + " at port : " + port);
				System.Net.Sockets.TcpClient s = new System.Net.Sockets.TcpClient(host, System.Int32.Parse(port) + 1);
				
				out_Renamed = s.GetStream();
				in_Renamed = s.GetStream();
				
				sendLine(text, out_Renamed);
				
				//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				System.IO.StreamReader bin = new System.IO.StreamReader(new System.IO.StreamReader(in_Renamed, System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(in_Renamed, System.Text.Encoding.Default).CurrentEncoding);
				System.String line;
				while ((line = bin.ReadLine()) != null)
					System.Console.Out.WriteLine(line);
				
				// Need to scrape a value from the last line?
				returnValue = "1";
				return returnValue;
			}
			catch (System.Exception ex)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Error.WriteLine("Error communicating with server: " + ex);
				return returnValue;
			}
		}
		
		private static void  sendLine(System.String line, System.IO.Stream outPipe)
		{
			sbyte[] temp_sbyteArray;
			temp_sbyteArray = SupportClass.ToSByteArray(SupportClass.ToByteArray(line));
			outPipe.Write(SupportClass.ToByteArray(temp_sbyteArray), 0, temp_sbyteArray.Length);
			outPipe.Flush();
		}
		
		
		/*
		TODO: this is not unicode friendly, nor is getFile()
		The output is urlencoded 8859_1 text.
		should probably be urlencoded UTF-8... how does the servlet determine
		the encoded charset?  I guess we're supposed to add a ";charset" clause
		to the content type?
		*/
		internal static System.String doHttp(System.String postURL, System.String text)
		{
			System.String returnValue = null;
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("bsh.client=Remote");
			sb.Append("&bsh.script=");
			sb.Append(System.Web.HttpUtility.UrlEncode(text));
			/*
			// This requires Java 1.3
			try {
			sb.append( URLEncoder.encode( text, "8859_1" ) );
			} catch ( UnsupportedEncodingException e ) {
			e.printStackTrace();
			}
			*/
			System.String formData = sb.ToString();
			
			try
			{
				//UPGRADE_TODO: Class 'java.net.URL' was converted to a 'System.Uri' which does not throw an exception if a URL specifies an unknown protocol. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1132'"
				System.Uri url = new System.Uri(postURL);
				System.Net.HttpWebRequest urlcon = (System.Net.HttpWebRequest) System.Net.WebRequest.Create(url);
				urlcon.Method = "POST";
				SupportClass.URLConnectionSupport.SetRequestProperty(urlcon, "Content-type", "application/x-www-form-urlencoded");
				//UPGRADE_ISSUE: Method 'java.net.URLConnection.setDoOutput' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLConnectionsetDoOutput_boolean'"
				urlcon.setDoOutput(true);
				//UPGRADE_ISSUE: Method 'java.net.URLConnection.setDoInput' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetURLConnectionsetDoInput_boolean'"
				urlcon.setDoInput(true);
				System.IO.StreamWriter temp_writer;
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				//UPGRADE_TODO: Constructor 'java.io.OutputStreamWriter.OutputStreamWriter' was converted to 'System.IO.StreamWriter.StreamWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioOutputStreamWriterOutputStreamWriter_javaioOutputStream_javalangString'"
				temp_writer = new System.IO.StreamWriter(new System.IO.StreamWriter(urlcon.GetRequestStream(), System.Text.Encoding.GetEncoding("8859_1")).BaseStream, new System.IO.StreamWriter(urlcon.GetRequestStream(), System.Text.Encoding.GetEncoding("8859_1")).Encoding);
				temp_writer.AutoFlush = true;
				System.IO.StreamWriter pout = temp_writer;
				pout.Write(formData);
				pout.Flush();
				
				// read results...
				//UPGRADE_ISSUE: Method 'java.net.HttpURLConnection.getResponseCode' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javanetHttpURLConnectiongetResponseCode'"
				int rc = urlcon.getResponseCode();
				if (rc != (int) System.Net.HttpStatusCode.OK)
					System.Console.Out.WriteLine("Error, HTTP response: " + rc);
				
				//UPGRADE_TODO: Method 'java.net.URLConnection.getHeaderField' was converted to 'System.Net.WebRequest.GetResponse.Headers.Get' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javanetURLConnectiongetHeaderField_javalangString'"
				returnValue = urlcon.GetResponse().Headers.Get("Bsh-Return");
				
				//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
				System.IO.StreamReader bin = new System.IO.StreamReader(new System.IO.StreamReader(urlcon.GetResponse().GetResponseStream(), System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(urlcon.GetResponse().GetResponseStream(), System.Text.Encoding.Default).CurrentEncoding);
				System.String line;
				while ((line = bin.ReadLine()) != null)
					System.Console.Out.WriteLine(line);
				
				System.Console.Out.WriteLine("Return Value: " + returnValue);
			}
			catch (System.UriFormatException e)
			{
				//UPGRADE_TODO: Method 'java.io.PrintStream.println' was converted to 'System.Console.Out.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintStreamprintln_javalangObject'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Out.WriteLine(e); // bad postURL
			}
			catch (System.IO.IOException e2)
			{
				//UPGRADE_TODO: Method 'java.io.PrintStream.println' was converted to 'System.Console.Out.WriteLine' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioPrintStreamprintln_javalangObject'"
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Out.WriteLine(e2); // I/O error
			}
			
			return returnValue;
		}
		
		/*
		Note: assumes default character encoding
		*/
		internal static System.String getFile(System.String name)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
			//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
			//UPGRADE_TODO: Constructor 'java.io.FileReader.FileReader' was converted to 'System.IO.StreamReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.IO.StreamReader bin = new System.IO.StreamReader(new System.IO.StreamReader(name, System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(name, System.Text.Encoding.Default).CurrentEncoding);
			System.String line;
			while ((line = bin.ReadLine()) != null)
				sb.Append(line).Append("\n");
			return sb.ToString();
		}
	}
}