/*
* $Id$
*
* Copyright (c) 2013 by Joel Uckelman
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
namespace VassalSharp.tools.image
{
	
	/// <summary> A (partial) JPEG decoder.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.2.3
	/// </since>
	class JPEGDecoder
	{
		protected internal JPEGDecoder()
		{
		}
		
		internal const int TEM = 0xFF01;
		internal const int SOF0 = 0xFFC0;
		internal const int SOF1 = 0xFFC1;
		internal const int SOF2 = 0xFFC2;
		internal const int SOF3 = 0xFFC3;
		internal const int SOF4 = 0xFFC4;
		internal const int SOF5 = 0xFFC5;
		internal const int SOF6 = 0xFFC6;
		internal const int SOF7 = 0xFFC7;
		internal const int SOF8 = 0xFFC8;
		internal const int SOF9 = 0xFFC9;
		internal const int SOF10 = 0xFFCA;
		internal const int SOF11 = 0xFFCB;
		internal const int SOF12 = 0xFFCC;
		internal const int SOF13 = 0xFFCD;
		internal const int SOF14 = 0xFFCE;
		internal const int SOF15 = 0xFFCF;
		internal const int RST0 = 0xFFD0;
		internal const int RST1 = 0xFFD1;
		internal const int RST2 = 0xFFD2;
		internal const int RST3 = 0xFFD3;
		internal const int RST4 = 0xFFD4;
		internal const int RST5 = 0xFFD5;
		internal const int RST6 = 0xFFD6;
		internal const int RST7 = 0xFFD7;
		internal const int SOI = 0xFFD8;
		internal const int EOI = 0xFFD9;
		internal const int SOS = 0xFFDA;
		internal const int APP0 = 0xFFE0;
		internal const int APP2 = 0xFFE2;
		internal const int APP13 = 0xFFED;
		internal const int APP14 = 0xFFEE;
		
		//UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
		public static bool decodeSignature(System.IO.BinaryReader in_Renamed)
		{
			return in_Renamed.ReadUInt16() == SOI;
		}
		
		//UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
		public static Chunk decodeChunk(System.IO.BinaryReader in_Renamed)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int type = in_Renamed.ReadUInt16(); // marker type
			
			if ((type & 0xFF00) != 0xFF00)
			{
				throw new System.IO.IOException("initial byte of chunk must be FF");
			}
			
			sbyte[] data;
			
			if (type == TEM || (RST0 <= type && type <= EOI))
			{
				// These chunks have no data
				data = new sbyte[0];
			}
			else
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'length '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int length = in_Renamed.ReadUInt16();
				data = new sbyte[length - 2]; // length is inclusive of its own 2 bytes
				SupportClass.ReadInput(in_Renamed.BaseStream, data, 0, data.Length);
			}
			
			// NB: This will blow up after reaching an SOS, due to it being follwed
			// by raw data instead of another chunk. If we want to find the next
			// chunk after an SOS, we have to scan for FF xx, where xx != 00.
			
			return new Chunk(type, data);
		}
		
		public class Chunk
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			public int type;
			//UPGRADE_NOTE: Final was removed from the declaration of 'data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			public sbyte[] data;
			
			internal Chunk(int type, sbyte[] data)
			{
				this.type = type;
				this.data = data;
			}
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
			System.IO.BinaryReader in_Renamed = null;
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javalangString'"
				in_Renamed = new System.IO.BinaryReader(new System.IO.FileStream(args[0], System.IO.FileMode.Open, System.IO.FileAccess.Read));
				
				if (!JPEGDecoder.decodeSignature(in_Renamed))
				{
					System.Console.Out.WriteLine("Not a JPEG");
				}
				
				JPEGDecoder.Chunk ch;
				do 
				{
					ch = JPEGDecoder.decodeChunk(in_Renamed);
					System.Console.Out.WriteLine("type == " + System.Convert.ToString(ch.type, 16) + ", length == " + ch.data.Length);
				}
				while (ch.type != JPEGDecoder.EOI);
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
	}
}