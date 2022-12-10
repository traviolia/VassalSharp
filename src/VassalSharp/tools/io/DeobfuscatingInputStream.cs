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
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
namespace VassalSharp.tools.io
{
	
	/// <summary> A {@link FilterInputStream} which converts a file created with
	/// {@link ObfuscatingOutputStream} back into plain text.
	/// Additionally, plain text will be passed through unchanged.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.0
	/// </since>
	public class DeobfuscatingInputStream:System.IO.BinaryReader
	{
		
		/// <param name="in">the stream to wrap
		/// </param>
		/// <throws>  IOException </throws>
		public DeobfuscatingInputStream(System.IO.Stream in_Renamed):base(null)
		{
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'header '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] header = new sbyte[ObfuscatingOutputStream.HEADER.Length];
			readFully(in_Renamed, header, 0, header.Length);
			//UPGRADE_TODO: The differences in the Format  of parameters for constructor 'java.lang.String.String'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
			if (System.Text.Encoding.GetEncoding("UTF-8").GetString(SupportClass.ToByteArray(header)).Equals(ObfuscatingOutputStream.HEADER))
			{
				this.BaseStream = new DeobfuscatingInputStreamImpl(in_Renamed);
			}
			else
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'pin '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SupportClass.BackInputStream pin = new SupportClass.BackInputStream(in_Renamed, header.Length);
				pin.UnRead(SupportClass.ToByteArray(header));
				this.BaseStream = pin;
			}
		}
		
		/// <summary> Reads the given number of bytes.
		/// 
		/// </summary>
		/// <param name="in">the source
		/// </param>
		/// <param name="bytes">the destination
		/// </param>
		/// <param name="off">the offset into the destination array
		/// </param>
		/// <param name="len">the number of bytes to read
		/// </param>
		/// <throws>  IOException if <code>len</code> bytes cannot be read </throws>
		private static int readFully(System.IO.Stream in_Renamed, sbyte[] bytes, int off, int len)
		{
			int count;
			int n = 0;
			while (n < len)
			{
				count = (in_Renamed is VassalSharp.tools.io.RereadableInputStream || in_Renamed is VassalSharp.tools.io.CompositeInputStream || in_Renamed is VassalSharp.tools.image.PNGChunkSkipInputStream)?(int) SupportClass.InvokeMethodAsVirtual(in_Renamed, "read", new System.Object[]{bytes, off + n, len - n}):SupportClass.ReadInput(in_Renamed, bytes, off + n, len - n);
				if (count < 0)
					break;
				n += count;
			}
			
			return n;
		}
		
		private class DeobfuscatingInputStreamImpl:System.IO.BinaryReader
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'key '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private sbyte key;
			//UPGRADE_NOTE: Final was removed from the declaration of 'pair '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private sbyte[] pair = new sbyte[2];
			
			public DeobfuscatingInputStreamImpl(System.IO.Stream in_Renamed):base(in_Renamed)
			{
				
				VassalSharp.tools.io.DeobfuscatingInputStream.readFully(in_Renamed, pair, 0, 2);
				key = (sbyte) ((unhex(pair[0]) << 4) | unhex(pair[1]));
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			//UPGRADE_NOTE: The equivalent of method 'java.io.FilterInputStream.read' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public int read(sbyte[] bytes, int off, int len)
			{
				int b = 0;
				int i = 0;
				while (i < len && (b = Read()) >= 0)
					bytes[(i++) + off] = (sbyte) b;
				return b == - 1 && i == 0?- 1:i;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public  override int Read()
			{
				switch (VassalSharp.tools.io.DeobfuscatingInputStream.readFully((System.IO.Stream) BaseStream, pair, 0, 2))
				{
					
					case 0: 
						return - 1;
					
					case 1: 
						throw new System.IO.IOException();
					
					case 2: 
						return (((unhex(pair[0]) << 4) | unhex(pair[1])) ^ key) & 0xFF;
					
					default: 
						throw new System.IO.IOException();
					
				}
			}
			
			private int unhex(int i)
			{
				switch (i)
				{
					
					// digits 0-9
					case 0x30: 
					case 0x31: 
					case 0x32: 
					case 0x33: 
					case 0x34: 
					case 0x35: 
					case 0x36: 
					case 0x37: 
					case 0x38: 
					case 0x39: 
						return i - 0x30;
						// digits A-F
					
					case 0x41: 
					case 0x42: 
					case 0x43: 
					case 0x44: 
					case 0x45: 
					case 0x46: 
						return i - 0x37;
						// digits a-f
					
					case 0x61: 
					case 0x62: 
					case 0x63: 
					case 0x64: 
					case 0x65: 
					case 0x66: 
						return i - 0x57;
					
					default: 
						throw new System.IO.IOException(System.Convert.ToString(i));
					
				}
			}
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			System.IO.Stream in_Renamed = null;
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javalangString'"
				in_Renamed = new DeobfuscatingInputStream(args.Length > 0?new System.IO.FileStream(args[0], System.IO.FileMode.Open, System.IO.FileAccess.Read):System.Console.OpenStandardInput());
				IOUtils.copy(in_Renamed, System.Console.OpenStandardOutput());
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
			
			System.Environment.Exit(0);
		}
	}
}