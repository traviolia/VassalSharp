/// <summary> 
/// ASM: a very small and fast Java bytecode manipulation framework
/// Copyright (C) 2000 INRIA, France Telecom
/// Copyright (C) 2002 France Telecom
/// 
/// This library is free software; you can redistribute it and/or
/// modify it under the terms of the GNU Lesser General Public
/// License as published by the Free Software Foundation; either
/// version 2 of the License, or (at your option) any later version.
/// 
/// This library is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
/// Lesser General Public License for more details.
/// 
/// You should have received a copy of the GNU Lesser General Public
/// License along with this library; if not, write to the Free Software
/// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
/// 
/// Contact: Eric.Bruneton@rd.francetelecom.com
/// 
/// Author: Eric Bruneton
/// </summary>
using System;
namespace bsh.org.objectweb.asm
{
	
	/// <summary> A dynamically extensible vector of bytes. This class is roughly equivalent to
	/// a DataOutputStream on top of a ByteArrayOutputStream, but is more efficient.
	/// </summary>
	
	sealed class ByteVector
	{
		
		/// <summary> The content of this vector.</summary>
		
		internal sbyte[] data;
		
		/// <summary> Actual number of bytes in this vector.</summary>
		
		internal int length;
		
		/// <summary> Constructs a new {@link ByteVector ByteVector} with a default initial size.</summary>
		
		public ByteVector()
		{
			data = new sbyte[64];
		}
		
		/// <summary> Constructs a new {@link ByteVector ByteVector} with the given initial size.
		/// 
		/// </summary>
		/// <param name="initialSize">the initial size of the byte vector to be constructed.
		/// </param>
		
		public ByteVector(int initialSize)
		{
			data = new sbyte[initialSize];
		}
		
		/// <summary> Puts a byte into this byte vector. The byte vector is automatically
		/// enlarged if necessary.
		/// 
		/// </summary>
		/// <param name="b">a byte.
		/// </param>
		/// <returns> this byte vector.
		/// </returns>
		
		public ByteVector put1(int b)
		{
			int length = this.length;
			if (length + 1 > data.Length)
			{
				enlarge(1);
			}
			data[length++] = (sbyte) b;
			this.length = length;
			return this;
		}
		
		/// <summary> Puts two bytes into this byte vector. The byte vector is automatically
		/// enlarged if necessary.
		/// 
		/// </summary>
		/// <param name="b1">a byte.
		/// </param>
		/// <param name="b2">another byte.
		/// </param>
		/// <returns> this byte vector.
		/// </returns>
		
		public ByteVector put11(int b1, int b2)
		{
			int length = this.length;
			if (length + 2 > this.data.Length)
			{
				enlarge(2);
			}
			sbyte[] data = this.data;
			data[length++] = (sbyte) b1;
			data[length++] = (sbyte) b2;
			this.length = length;
			return this;
		}
		
		/// <summary> Puts a short into this byte vector. The byte vector is automatically
		/// enlarged if necessary.
		/// 
		/// </summary>
		/// <param name="s">a short.
		/// </param>
		/// <returns> this byte vector.
		/// </returns>
		
		public ByteVector put2(int s)
		{
			int length = this.length;
			if (length + 2 > this.data.Length)
			{
				enlarge(2);
			}
			sbyte[] data = this.data;
			data[length++] = (sbyte) (SupportClass.URShift(s, 8));
			data[length++] = (sbyte) s;
			this.length = length;
			return this;
		}
		
		/// <summary> Puts a byte and a short into this byte vector. The byte vector is
		/// automatically enlarged if necessary.
		/// 
		/// </summary>
		/// <param name="b">a byte.
		/// </param>
		/// <param name="s">a short.
		/// </param>
		/// <returns> this byte vector.
		/// </returns>
		
		public ByteVector put12(int b, int s)
		{
			int length = this.length;
			if (length + 3 > this.data.Length)
			{
				enlarge(3);
			}
			sbyte[] data = this.data;
			data[length++] = (sbyte) b;
			data[length++] = (sbyte) (SupportClass.URShift(s, 8));
			data[length++] = (sbyte) s;
			this.length = length;
			return this;
		}
		
		/// <summary> Puts an int into this byte vector. The byte vector is automatically
		/// enlarged if necessary.
		/// 
		/// </summary>
		/// <param name="i">an int.
		/// </param>
		/// <returns> this byte vector.
		/// </returns>
		
		public ByteVector put4(int i)
		{
			int length = this.length;
			if (length + 4 > this.data.Length)
			{
				enlarge(4);
			}
			sbyte[] data = this.data;
			data[length++] = (sbyte) (SupportClass.URShift(i, 24));
			data[length++] = (sbyte) (SupportClass.URShift(i, 16));
			data[length++] = (sbyte) (SupportClass.URShift(i, 8));
			data[length++] = (sbyte) i;
			this.length = length;
			return this;
		}
		
		/// <summary> Puts a long into this byte vector. The byte vector is automatically
		/// enlarged if necessary.
		/// 
		/// </summary>
		/// <param name="l">a long.
		/// </param>
		/// <returns> this byte vector.
		/// </returns>
		
		public ByteVector put8(long l)
		{
			int length = this.length;
			if (length + 8 > this.data.Length)
			{
				enlarge(8);
			}
			sbyte[] data = this.data;
			int i = (int) (SupportClass.URShift(l, 32));
			data[length++] = (sbyte) (SupportClass.URShift(i, 24));
			data[length++] = (sbyte) (SupportClass.URShift(i, 16));
			data[length++] = (sbyte) (SupportClass.URShift(i, 8));
			data[length++] = (sbyte) i;
			i = (int) l;
			data[length++] = (sbyte) (SupportClass.URShift(i, 24));
			data[length++] = (sbyte) (SupportClass.URShift(i, 16));
			data[length++] = (sbyte) (SupportClass.URShift(i, 8));
			data[length++] = (sbyte) i;
			this.length = length;
			return this;
		}
		
		/// <summary> Puts a String in UTF format into this byte vector. The byte vector is
		/// automatically enlarged if necessary.
		/// 
		/// </summary>
		/// <param name="s">a String.
		/// </param>
		/// <returns> this byte vector.
		/// </returns>
		
		public ByteVector putUTF(System.String s)
		{
			int charLength = s.Length;
			int byteLength = 0;
			for (int i = 0; i < charLength; ++i)
			{
				char c = s[i];
				if (c >= '\x0001' && c <= '\x007F')
				{
					byteLength++;
				}
				else if (c > '\u07FF')
				{
					byteLength += 3;
				}
				else
				{
					byteLength += 2;
				}
			}
			if (byteLength > 65535)
			{
				throw new System.ArgumentException();
			}
			int length = this.length;
			if (length + 2 + byteLength > this.data.Length)
			{
				enlarge(2 + byteLength);
			}
			sbyte[] data = this.data;
			data[length++] = (sbyte) (SupportClass.URShift(byteLength, 8));
			data[length++] = (sbyte) (byteLength);
			for (int i = 0; i < charLength; ++i)
			{
				char c = s[i];
				if (c >= '\x0001' && c <= '\x007F')
				{
					data[length++] = (sbyte) c;
				}
				else if (c > '\u07FF')
				{
					data[length++] = (sbyte) (0xE0 | c >> 12 & 0xF);
					data[length++] = (sbyte) (0x80 | c >> 6 & 0x3F);
					data[length++] = (sbyte) (0x80 | c & 0x3F);
				}
				else
				{
					data[length++] = (sbyte) (0xC0 | c >> 6 & 0x1F);
					data[length++] = (sbyte) (0x80 | c & 0x3F);
				}
			}
			this.length = length;
			return this;
		}
		
		/// <summary> Puts an array of bytes into this byte vector. The byte vector is
		/// automatically enlarged if necessary.
		/// 
		/// </summary>
		/// <param name="b">an array of bytes. May be <tt>null</tt> to put <tt>len</tt> null
		/// bytes into this byte vector.
		/// </param>
		/// <param name="off">index of the fist byte of b that must be copied.
		/// </param>
		/// <param name="len">number of bytes of b that must be copied.
		/// </param>
		/// <returns> this byte vector.
		/// </returns>
		
		public ByteVector putByteArray(sbyte[] b, int off, int len)
		{
			if (length + len > data.Length)
			{
				enlarge(len);
			}
			if (b != null)
			{
				Array.Copy(b, off, data, length, len);
			}
			length += len;
			return this;
		}
		
		/// <summary> Enlarge this byte vector so that it can receive n more bytes.
		/// 
		/// </summary>
		/// <param name="size">number of additional bytes that this byte vector should be
		/// able to receive.
		/// </param>
		
		private void  enlarge(int size)
		{
			sbyte[] newData = new sbyte[System.Math.Max(2 * data.Length, length + size)];
			Array.Copy(data, 0, newData, 0, length);
			data = newData;
		}
	}
}