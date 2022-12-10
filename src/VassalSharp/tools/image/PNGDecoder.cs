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
namespace VassalSharp.tools.image
{
	
	/// <summary> A (partial) PNG decoder.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	class PNGDecoder
	{
		protected internal PNGDecoder()
		{
		}
		
		// critical chunks
		internal const int IHDR = 0x49484452;
		internal const int PLTE = 0x504c5445;
		internal const int IDAT = 0x49444154;
		internal const int IEND = 0x49454e44;
		
		// ancillary chunks
		internal const int bKGD = 0x624b4744;
		internal const int cHRM = 0x6348524d;
		internal const int gAMA = 0x67414d41;
		internal const int hIST = 0x68495354;
		internal const int iCCP = 0x69434350;
		internal const int iTXt = 0x69545874;
		internal const int pHYs = 0x70485973;
		internal const int sBIT = 0x73424954;
		internal const int sPLT = 0x73504c54;
		internal const int sRGB = 0x73524742;
		internal const int tEXt = 0x74455874;
		internal const int tIME = 0x74494d45;
		internal const int tRNS = 0x74524e53;
		internal const int zTXt = 0x7a545874;
		
		//UPGRADE_TODO: Literal detected as an unsigned long can generate compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1175'"
		internal const long sig = 0x89504e470d0a1a0aL;
		
		//UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
		public static bool decodeSignature(System.IO.BinaryReader in_Renamed)
		{
			// 5.2
			return in_Renamed.ReadInt64() == sig;
		}
		
		//UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
		public static Chunk decodeChunk(System.IO.BinaryReader in_Renamed)
		{
			// 5.3
			//UPGRADE_NOTE: Final was removed from the declaration of 'length '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int length = in_Renamed.ReadInt32();
			
			// Length should not exceed 2^31-1. Since ints are signed, any value
			// greater than 2^31-1 will look negative to us.
			if (length < 0)
			{
				throw new System.IO.IOException("chunk length out of range");
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int type = in_Renamed.ReadInt32();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] data = new sbyte[length];
			SupportClass.ReadInput(in_Renamed.BaseStream, data, 0, data.Length);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'crc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			long crc = in_Renamed.ReadInt32() & unchecked((int) 0x0000000ffffffffL);
			
			/*
			final CRC32 crc32 = new CRC32();
			crc32.update(type);
			crc32.update(data);
			if (crc != crc32.getValue())
			throw new IOException("corrupted " + type + " chunk");*/
			
			return new Chunk(type, data, crc);
		}
		
		public class Chunk
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			public int type;
			//UPGRADE_NOTE: Final was removed from the declaration of 'data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			public sbyte[] data;
			//UPGRADE_NOTE: Final was removed from the declaration of 'crc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			public long crc;
			
			internal Chunk(int type, sbyte[] data, long crc)
			{
				this.type = type;
				this.data = data;
				this.crc = crc;
			}
		}
	}
}