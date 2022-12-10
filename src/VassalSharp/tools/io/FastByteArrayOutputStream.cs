/*
* Copyright (c) 2007-2008 by Joel Uckelman
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
	
	/// <summary> A {@link ByteArrayOutputStream} which does not retain its byte buffer,
	/// and can produce an {@link InputStream} sharing the same byte buffer.
	/// The advantage of this is that the byte buffer can be shared between the
	/// output and input, and provides better performance when the stream
	/// will be used only a single time and the buffer is full when
	/// {@link #toByteArray()} is called.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class FastByteArrayOutputStream:System.IO.MemoryStream
	{
		/// <summary> Creates a new byte array output stream. The buffer capacity is initially
		/// 32 bytes.
		/// </summary>
		public FastByteArrayOutputStream():base()
		{
		}
		
		/// <summary> Creates a new byte array output stream, with the specified buffer
		/// capacity, in bytes.
		/// 
		/// </summary>
		/// <param name="size">the initial size
		/// </param>
		public FastByteArrayOutputStream(int size):base(size)
		{
		}
		
		/// <summary> Creates a newly allocated byte array. Its size is the current size of
		/// this output stream. If the buffer is full, then the array returned is
		/// the buffer itself, not a copy, and a new empty buffer is created.
		/// Otherwise, a copy of valid portion of the buffer is returned.
		/// 
		/// </summary>
		/// <returns> the current contents of this output stream as a byte array
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_ISSUE: The equivalent in .NET for method 'java.io.ByteArrayOutputStream.toByteArray' returns a different type. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1224'"
		public override System.Byte[] ToArray()
		{
			//UPGRADE_ISSUE: Field 'java.io.ByteArrayOutputStream.buf' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioByteArrayOutputStreambuf_f'"
			if ((int) Length == buf.Length)
			{
				Length = (System.Int64) 0;
				//UPGRADE_NOTE: Final was removed from the declaration of 'ret '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Field 'java.io.ByteArrayOutputStream.buf' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioByteArrayOutputStreambuf_f'"
				sbyte[] ret = buf;
				//UPGRADE_ISSUE: Field 'java.io.ByteArrayOutputStream.buf' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioByteArrayOutputStreambuf_f'"
				buf = new sbyte[0];
				return ret;
			}
			else
			{
				return SupportClass.ToSByteArray(base.ToArray());
			}
		}
		
		/// <summary> Creates an <code>InputStream</code> which reads the bytes stored by
		/// this <code>FastByteArrayOutputStream</code>.
		/// 
		/// </summary>
		/// <returns> the <code>InputStream</code> for this buffer
		/// </returns>
		public virtual System.IO.Stream toInputStream()
		{
			//UPGRADE_ISSUE: Field 'java.io.ByteArrayOutputStream.buf' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioByteArrayOutputStreambuf_f'"
			return new System.IO.MemoryStream(SupportClass.ToByteArray(buf), 0, (int) Length);
		}
	}
}