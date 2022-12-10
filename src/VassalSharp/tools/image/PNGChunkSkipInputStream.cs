/*
* $Id$
*
* Copyright (c) 2009 by Joel Uckelman
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
	
	class PNGChunkSkipInputStream:System.IO.Stream
	{
		public PNGChunkSkipInputStream()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			this.skip = skip;
			this.in_Renamed = new System.IO.BinaryReader(in_Renamed);
			
			if (!PNGDecoder.decodeSignature(this.in_Renamed))
				throw new System.IO.IOException();
			
			bout = new System.IO.MemoryStream();
			//UPGRADE_TODO: Class 'java.io.DataOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataOutputStream'"
			out_Renamed = new System.IO.BinaryWriter(bout);
			
			out_Renamed.Write(PNGDecoder.sig);
			flipBuffer();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public PNGChunkSkipInputStream(Set < Integer > skip, InputStream in) 
		throws IOException
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected final Set < Integer > skip;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'in '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
		protected internal System.IO.BinaryReader in_Renamed;
		
		protected internal System.IO.MemoryStream bin;
		//UPGRADE_NOTE: Final was removed from the declaration of 'bout '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.IO.MemoryStream bout;
		//UPGRADE_NOTE: Final was removed from the declaration of 'out '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'java.io.DataOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataOutputStream'"
		protected internal System.IO.BinaryWriter out_Renamed;
		
		protected internal bool seenIEND = false;
		
		//UPGRADE_NOTE: The equivalent of method 'java.io.InputStream.available' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public int available()
		{
			long available;
			available = bin.Length - bin.Position;
			return (int) available;
		}
		
		public override void  Close()
		{
			//UPGRADE_TODO: Method 'java.io.FilterInputStream.close' was converted to 'System.IO.BinaryReader.Close' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFilterInputStreamclose'"
			in_Renamed.Close();
		}
		
		//UPGRADE_NOTE: The equivalent of method 'java.io.InputStream.mark' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  mark(int readlimit)
		{
		}
		
		//UPGRADE_NOTE: The equivalent of method 'java.io.InputStream.markSupported' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public bool markSupported()
		{
			return false;
		}
		
		public override int ReadByte()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] b = new sbyte[1];
			return read(b) == - 1?- 1:b[0];
		}
		
		//UPGRADE_NOTE: The equivalent of method 'java.io.InputStream.read' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public int read(sbyte[] b)
		{
			return read(b, 0, b.Length);
		}
		
		//UPGRADE_NOTE: The equivalent of method 'java.io.InputStream.read' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public int read(sbyte[] b, int off, int len)
		{
			int ret = SupportClass.ReadInput(bin, b, off, len);
			
			if (ret == - 1 && !seenIEND)
			{
				PNGDecoder.Chunk ch;
				
				do 
				{
					ch = PNGDecoder.decodeChunk(in_Renamed);
					if (ch.type == PNGDecoder.IEND)
						seenIEND = true;
				}
				while (skip.contains(ch.type));
				
				encodeChunk(ch);
				flipBuffer();
				
				ret = SupportClass.ReadInput(bin, b, off, len);
			}
			
			return ret;
		}
		
		protected internal virtual void  encodeChunk(PNGDecoder.Chunk ch)
		{
			// write chunk data length
			out_Renamed.Write(ch.data.Length);
			
			// write chunk type
			out_Renamed.Write(ch.type);
			
			// write data
			out_Renamed.Write(SupportClass.ToByteArray(ch.data));
			
			// write checksum
			out_Renamed.Write((int) ch.crc);
		}
		
		protected internal virtual void  flipBuffer()
		{
			out_Renamed.Flush();
			bin = new System.IO.MemoryStream(SupportClass.ToByteArray(SupportClass.ToSByteArray(bout.ToArray())));
			//UPGRADE_ISSUE: Method 'java.io.ByteArrayOutputStream.reset' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioByteArrayOutputStreamreset'"
			bout.reset();
		}
		
		//UPGRADE_NOTE: The equivalent of method 'java.io.InputStream.reset' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  reset()
		{
			throw new System.IO.IOException();
		}
		
		//UPGRADE_NOTE: The equivalent of method 'java.io.InputStream.skip' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public long skip(long n)
		{
			throw new System.IO.IOException();
		}
		
		/*
		public static void main(String[] args) throws Exception {
		
		final InputStream fin = new PNGChunkSkipInputStream(
		java.util.Collections.singleton(PNGDecoder.iTXt),
		new java.io.FileInputStream("iTXt.png")
		);
		
		VassalSharp.tools.io.IOUtils.copy(fin, System.out);
		fin.close();
		}*/
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override void  Flush()
		{
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.Int64 Seek(System.Int64 offset, System.IO.SeekOrigin origin)
		{
			return 0;
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override void  SetLength(System.Int64 value)
		{
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.Int32 Read(System.Byte[] buffer, System.Int32 offset, System.Int32 count)
		{
			return 0;
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override void  Write(System.Byte[] buffer, System.Int32 offset, System.Int32 count)
		{
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.Boolean CanRead
		{
			get
			{
				return false;
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.Boolean CanSeek
		{
			get
			{
				return false;
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.Boolean CanWrite
		{
			get
			{
				return false;
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.Int64 Length
		{
			get
			{
				return 0;
			}
			
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		public override System.Int64 Position
		{
			get
			{
				return 0;
			}
			
			set
			{
			}
			
		}
	}
}