/*
* $Id$
*
* Copyright (c) 2000-2009 by Rodney Kinney, Joel Uckelman
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
namespace VassalSharp.tools.io
{
	
	/// <summary> A {@link FilterOutputStream} which handles simple obfuscation of a file's
	/// contents, to prevent the casual cheat of hand-editing.
	/// 
	/// </summary>
	/// <author>  uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public class ObfuscatingOutputStream:System.IO.BinaryWriter
	{
		public const System.String HEADER = "!VCSK";
		//UPGRADE_NOTE: Final was removed from the declaration of 'rand '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly System.Random rand = new System.Random();
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'key '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private sbyte key;
		//UPGRADE_NOTE: Final was removed from the declaration of 'pair '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private sbyte[] pair = new sbyte[2];
		
		/// <param name="out">the stream to wrap
		/// </param>
		/// <throws>  IOException </throws>
		public ObfuscatingOutputStream(System.IO.Stream out_Renamed):this(out_Renamed, (sbyte) rand.Next(256))
		{
		}
		
		/// <param name="out">the stream to wrap
		/// </param>
		/// <param name="key">the byte to use as the key
		/// </param>
		/// <throws>  IOException </throws>
		public ObfuscatingOutputStream(System.IO.Stream out_Renamed, sbyte key):base(out_Renamed)
		{
			this.key = key;
			
			//UPGRADE_TODO: Method 'java.lang.String.getBytes' was converted to 'System.Text.Encoding.GetEncoding(string).GetBytes(string)' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangStringgetBytes_javalangString'"
			sbyte[] temp_sbyteArray;
			temp_sbyteArray = SupportClass.ToSByteArray(System.Text.Encoding.GetEncoding("UTF-8").GetBytes(HEADER));
			out_Renamed.Write(SupportClass.ToByteArray(temp_sbyteArray), 0, temp_sbyteArray.Length);
			
			pair[0] = hex[SupportClass.URShift((key & 0xF0), 4)];
			pair[1] = hex[key & 0x0F];
			sbyte[] temp_sbyteArray2;
			temp_sbyteArray2 = pair;
			out_Renamed.Write(SupportClass.ToByteArray(temp_sbyteArray2), 0, temp_sbyteArray2.Length);
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public  override void  Write(System.Byte[] bytes, int off, int len)
		{
			for (int i = 0; i < len; ++i)
				write(bytes[off + i]);
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'hex'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly sbyte[] hex = new sbyte[]{(sbyte) '0', (sbyte) '1', (sbyte) '2', (sbyte) '3', (sbyte) '4', (sbyte) '5', (sbyte) '6', (sbyte) '7', (sbyte) '8', (sbyte) '9', (sbyte) 'a', (sbyte) 'b', (sbyte) 'c', (sbyte) 'd', (sbyte) 'e', (sbyte) 'f'};
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_NOTE: The equivalent of method 'java.io.FilterOutputStream.write' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  write(int b)
		{
			b ^= key;
			
			pair[0] = hex[SupportClass.URShift((b & 0xF0), 4)];
			pair[1] = hex[b & 0x0F];
			sbyte[] temp_sbyteArray;
			temp_sbyteArray = pair;
			BaseStream.Write(SupportClass.ToByteArray(temp_sbyteArray), 0, temp_sbyteArray.Length);
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			System.IO.Stream in_Renamed = null;
			System.IO.Stream out_Renamed = null;
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javalangString'"
				in_Renamed = args.Length > 0?new System.IO.FileStream(args[0], System.IO.FileMode.Open, System.IO.FileAccess.Read):System.Console.OpenStandardInput();
				out_Renamed = new ObfuscatingOutputStream(new System.IO.BufferedStream(System.Console.OpenStandardOutput()));
				IOUtils.copy(in_Renamed, out_Renamed);
				out_Renamed.Close();
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
			
			System.Environment.Exit(0);
		}
	}
}