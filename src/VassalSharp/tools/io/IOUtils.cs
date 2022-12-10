/*
* $Id$
*
* Copyright (c) 2007-2010 by Joel Uckelman
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
//using FileChannel = java.nio.channels.FileChannel;
//using ImageInputStream = javax.imageio.stream.ImageInputStream;

namespace VassalSharp.tools.io
{
	
	/// <summary> General I/O stream manipulation utilities. This class provides static
	/// utility methods to reduce boilerplate I/O code.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class IOUtils // : org.apache.commons.io.IOUtils
	{
		protected internal IOUtils()
		{
		}
		
		/// <summary> Copies bytes from a <code>FileInputStream</code> to a
		/// <code>FileOutputStream</code>.
		/// 
		/// This method uses channels. The input file should not be written
		/// to during the copy.
		/// 
		/// </summary>
		/// <param name="in">the source
		/// </param>
		/// <param name="out">the destination
		/// </param>
		/// <throws>  IOException if one occurs while reading or writing </throws>
		public static int copy(System.IO.FileStream in_Renamed, System.IO.FileStream out_Renamed)
		{
			long count = copyLarge(in_Renamed, out_Renamed);
			return count > System.Int32.MaxValue?- 1:(int) count;
		}
		
		/// <summary> Copies bytes from a large (over 2GB) <code>FileInputStream</code> to a
		/// <code>FileOutputStream</code>.
		/// 
		/// This method uses channels. The input file should not be written
		/// to during the copy.
		/// 
		/// </summary>
		/// <param name="in">the source
		/// </param>
		/// <param name="out">the destination
		/// </param>
		/// <throws>  IOException if one occurs while reading or writing </throws>
		public static long copyLarge(System.IO.FileStream in_Renamed, System.IO.FileStream out_Renamed)
		{
			//FileChannel inc = in_Renamed.getChannel();
			//return inc.transferTo(0L, inc.size(), out_Renamed.getChannel());
			in_Renamed.CopyTo(out_Renamed);
			return 0;
		}

		/// <summary> Copies bytes from an <code>InputStream</code> to an
		/// <code>OutputStream</code> via a <code>byte</code> buffer. This
		/// method buffers input internally, so the input stream should not
		/// be a <code>BufferedInputStream</code>.
		/// 
		/// </summary>
		/// <param name="in">the source
		/// </param>
		/// <param name="out">the destination
		/// </param>
		/// <param name="buffer">the buffer
		/// </param>
		/// <returns> the number of bytes copied
		/// </returns>
		/// <throws>  IOException if one occurs while reading or writing </throws>
		public static int copy(System.IO.Stream in_Renamed, System.IO.Stream out_Renamed, int bufferSize)
		{
			long count = copyLarge(in_Renamed, out_Renamed, bufferSize);
			return count > System.Int32.MaxValue ? - 1 : (int) count;
		}
		
		/// <summary> Copies bytes from a large (over 2GB) <code>InputStream</code> to an
		/// <code>OutputStream</code> via a <code>byte</code> buffer. This
		/// method buffers input internally, so the input stream should not
		/// be a <code>BufferedInputStream</code>.
		/// 
		/// </summary>
		/// <param name="in">the source
		/// </param>
		/// <param name="out">the destination
		/// </param>
		/// <param name="buffer">the buffer
		/// </param>
		/// <returns> the number of bytes copied
		/// </returns>
		/// <throws>  IOException if one occurs while reading or writing </throws>
		public static long copyLarge(System.IO.Stream in_Renamed, System.IO.Stream out_Renamed, int bufferSize)
		{
			//long count = 0;
			//int n = 0;
			//while ((n = in_Renamed is VassalSharp.tools.image.PNGChunkSkipInputStream?((VassalSharp.tools.image.PNGChunkSkipInputStream) in_Renamed).read(buffer):SupportClass.ReadInput(in_Renamed, buffer, 0, buffer.Length)) != - 1)
			//{
			//	out_Renamed.Write(SupportClass.ToByteArray(buffer), 0, n);
			//	count += n;
			//}
			//return count;
			in_Renamed.CopyTo(out_Renamed, bufferSize);
			return 0;
		}

		/// <summary> Close an {@link ObjectInput} unconditionally. Equivalent to
		/// calling <code>o.close()</code> when <code>o</code> is nonnull.
		/// {@link IOException}s are swallowed, as there is generally
		/// nothing that can be done about exceptions on closing.
		/// 
		/// </summary>
		/// <param name="o">a (possibly <code>null</code>) <code>ObjectInput</code>
		/// </param>
		//UPGRADE_TODO: Interface 'java.io.ObjectInput' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectInput'"
		public static void closeQuietly(System.IO.BinaryReader o)
		{
			if (o == null)
				return;

			try
			{
				o.Close();
			}
			catch (System.IO.IOException e)
			{
				// ignore
			}
		}

		public static void closeQuietly(System.IO.Stream o)
		{
			if (o == null)
				return;

			try
			{
				o.Close();
			}
			catch (System.IO.IOException e)
			{
				// ignore
			}
		}

		public static void closeQuietly(System.IO.StreamWriter o)
		{
			if (o == null)
				return;

			try
			{
				o.Close();
			}
			catch (System.IO.IOException e)
			{
				// ignore
			}
		}

		public static void closeQuietly(System.Net.Sockets.TcpClient o)
		{
			if (o == null)
				return;

			try
			{
				o.Close();
			}
			catch (System.IO.IOException e)
			{
				// ignore
			}
		}

		public static void closeQuietly(System.Net.Sockets.TcpListener o)
		{
			if (o == null)
				return;

			try
			{
				o.Stop();
			}
			catch (System.IO.IOException e)
			{
				// ignore
			}
		}

		/// <summary> Close an {@link ObjectOutput} unconditionally. Equivalent to
		/// calling <code>o.close()</code> when <code>o</code> is nonnull.
		/// {@link IOException}s are swallowed, as there is generally
		/// nothing that can be done about exceptions on closing.
		/// 
		/// </summary>
		/// <param name="o">a (possibly <code>null</code>) <code>ObjectOutput</code>
		/// </param>
		//UPGRADE_TODO: Interface 'java.io.ObjectOutput' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectOutput'"
		public static void  closeQuietly(System.IO.BinaryWriter o)
		{
			if (o == null)
				return ;
			
			try
			{
				o.Close();
			}
			catch (System.IO.IOException e)
			{
				// ignore
			}
		}
		
		///// <summary> Close a {@link ZipFile} unconditionally. Equivalent to
		///// calling <code>z.close()</code> when <code>z</code> is nonnull.
		///// {@link IOException}s are swallowed, as there is generally
		///// nothing that can be done about exceptions on closing.
		///// 
		///// </summary>
		///// <param name="z">a (possibly <code>null</code>) <code>ZipFile</code>
		///// </param>
		//// Why doesn't ZipFile implement Closeable? Argh!
		//public static void  closeQuietly(ZipFile z)
		//{
		//	if (z == null)
		//		return ;
			
		//	try
		//	{
		//		//UPGRADE_ISSUE: Method 'java.util.zip.ZipFile.close' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipZipFile'"
		//		z.close();
		//	}
		//	catch (System.IO.IOException e)
		//	{
		//		// ignore
		//	}
		//}
		
		///// <summary> Close an {@link ImageInputStream} unconditionally. Equivalent to
		///// calling <code>s.close()</code> when <code>s</code> is nonnull.
		///// {@link IOException}s are quietly logged, as there is generally
		///// nothing that can be done about exceptions on closing.
		///// 
		///// </summary>
		///// <param name="s">a (possibly <code>null</code>) <code>ImageInputStream</code>
		///// </param>
		//// Why doesn't ImageInputStream implement Closeable? Argh!
		//public static void  closeQuietly(ImageInputStream s)
		//{
		//	if (s == null)
		//		return ;
			
		//	try
		//	{
		//		s.close();
		//	}
		//	catch (System.IO.IOException e)
		//	{
		//		// ignore
				
		//		// Note that ImageInputStreamImpl.close() rather ridiculously throws
		//		// an IOException if the stream is already closed. This is always done
		//		// via ImageInputStreamImpl.checkClosed().
		//	}
		//}
	}
}