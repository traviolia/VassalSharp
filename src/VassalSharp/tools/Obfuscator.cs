/*
* $Id$
*
* Copyright (c) 2000-2006 by Rodney Kinney
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
using ObfuscatingOutputStream = VassalSharp.tools.io.ObfuscatingOutputStream;
namespace VassalSharp.tools
{
	
	/// <summary> Utility class that handles simple obfuscation of a file's contents,
	/// to prevent the casual cheat of hand-editing a logfile.
	/// 
	/// </summary>
	/// <author>  rkinney
	/// </author>
	/// <deprecated> Use {@link ObfuscatingOutputStream} instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class Obfuscator
	{
		public const System.String HEADER = "!VCSK";
		//UPGRADE_NOTE: Final was removed from the declaration of 'rand '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly System.Random rand = new System.Random();
		private System.String encrypted;
		private sbyte key;
		
		public Obfuscator(sbyte[] contents)
		{
			key = (sbyte) rand.Next(256);
			//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder buffer = new StringBuilder(HEADER);
			appendAsHex(buffer, key);
			for (int i = 0; i < contents.Length; ++i)
			{
				appendAsHex(buffer, (sbyte) (contents[i] ^ key));
			}
			encrypted = buffer.toString();
		}
		
		private void  appendAsHex(StringBuilder buffer, sbyte b)
		{
			buffer.append(System.Convert.ToString(SupportClass.URShift((b & 0xf0), 4), 16)[0]);
			buffer.append(System.Convert.ToString(b & 0x0f, 16)[0]);
		}
		
		public virtual void  write(System.IO.Stream out_Renamed)
		{
			//UPGRADE_TODO: Method 'java.lang.String.getBytes' was converted to 'System.Text.Encoding.GetEncoding(string).GetBytes(string)' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangStringgetBytes_javalangString'"
			sbyte[] temp_sbyteArray;
			temp_sbyteArray = SupportClass.ToSByteArray(System.Text.Encoding.GetEncoding("UTF-8").GetBytes(encrypted));
			out_Renamed.Write(SupportClass.ToByteArray(temp_sbyteArray), 0, temp_sbyteArray.Length);
		}
		
		// Convert a plain text file to an obfuscated file
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'in '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javalangString'"
			System.IO.Stream in_Renamed = args.Length > 0?new System.IO.FileStream(args[0], System.IO.FileMode.Open, System.IO.FileAccess.Read):System.Console.OpenStandardInput();
			//UPGRADE_NOTE: Final was removed from the declaration of 'data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] data;
			try
			{
				data = IOUtils.toByteArray(in_Renamed);
			}
			finally
			{
				in_Renamed.Close();
			}
			
			Obfuscator o = new Obfuscator(data);
			o.write(new System.IO.BufferedStream(System.Console.OpenStandardOutput()));
			System.Environment.Exit(0);
		}
	}
}