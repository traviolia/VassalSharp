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
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
using DeobfuscatingInputStream = VassalSharp.tools.io.DeobfuscatingInputStream;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.tools
{
	
	/// <summary> Converts an file created with {@link Obfuscator} back into plain text.
	/// Additionally, plain text will be passed through unchanged.
	/// 
	/// </summary>
	/// <deprecated> Use {@link DeobfuscatingInputStream} instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class Deobfuscator
	{
		virtual public System.String String
		{
			get
			{
				return plain;
			}
			
		}
		private System.String plain;
		
		public Deobfuscator(System.IO.Stream in_Renamed)
		{
			System.String s = null;
			try
			{
				s = IOUtils.toString(in_Renamed, "UTF-8");
				in_Renamed.Close();
			}
			catch (System.IO.IOException e)
			{
				// should never happen
				ErrorDialog.bug(e);
				throw e;
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
			
			int offset = Obfuscator.HEADER.Length;
			if (s.StartsWith(Obfuscator.HEADER) && s.Length > offset + 1)
			{
				//UPGRADE_TODO: Method 'java.lang.Integer.parseInt' was converted to 'System.Convert.ToInt32' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				sbyte key = (sbyte) System.Convert.ToInt32(s.Substring(offset, (offset + 2) - (offset)), 16);
				offset += 2;
				sbyte[] bytes = new sbyte[(s.Length - offset) / 2];
				for (int i = 0; i < bytes.Length; ++i)
				{
					//UPGRADE_TODO: Method 'java.lang.Integer.parseInt' was converted to 'System.Convert.ToInt32' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					bytes[i] = (sbyte) (System.Convert.ToInt32(s.Substring(offset++, (++offset) - (offset++)), 16) ^ key);
				}
				//UPGRADE_TODO: The differences in the Format  of parameters for constructor 'java.lang.String.String'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
				plain = System.Text.Encoding.GetEncoding("UTF-8").GetString(SupportClass.ToByteArray(bytes));
			}
			else
			{
				plain = s;
			}
		}
		
		// Convert an obfuscated file into a plain-text file
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javalangString'"
			Deobfuscator d = new Deobfuscator(args.Length > 0?new System.IO.FileStream(args[0], System.IO.FileMode.Open, System.IO.FileAccess.Read):System.Console.In);
			IOUtils.copy(IOUtils.toInputStream(d.String, "UTF-8"), System.Console.OpenStandardOutput());
			System.Environment.Exit(0);
		}
	}
}