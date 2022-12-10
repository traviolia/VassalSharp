/*
* $Id$
*
* Copyright (c) 2010 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.nio.ByteBuffer' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ByteBuffer = java.nio.ByteBuffer;
//UPGRADE_TODO: The type 'java.nio.IntBuffer' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using IntBuffer = java.nio.IntBuffer;
//UPGRADE_TODO: The type 'org.apache.commons.codec.digest.DigestUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using DigestUtils = org.apache.commons.codec.digest.DigestUtils;
using ImageIOException = VassalSharp.tools.image.ImageIOException;
using ImageNotFoundException = VassalSharp.tools.image.ImageNotFoundException;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.tools.image.tilecache
{
	
	/// <summary> A class for reading and writing image tiles.
	/// 
	/// The VASSAL tile format consists of the 18-byte header, followed by gzipped
	/// 4-bpp image data. The header is the signature 'VASSAL' (6 bytes), the tile
	/// width (4 bytes), the tile height (4 bytes), and the image type (4 bytes).
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class TileUtils
	{
		
		private TileUtils()
		{
		}
		
		/// <summary> Reads an image tile file.
		/// 
		/// </summary>
		/// <param name="src">the path of the tile file
		/// </param>
		/// <returns> the tile image
		/// 
		/// </returns>
		/// <throws>  ImageIOException if the read fails </throws>
		/// <throws>  ImageNotFoundException if the file isn't found </throws>
		public static System.Drawing.Bitmap read(System.String src)
		{
			return read(new System.IO.FileInfo(src));
		}
		
		/// <summary> Reads an image tile file.
		/// 
		/// </summary>
		/// <param name="src">the path of the tile file
		/// </param>
		/// <returns> the tile image
		/// 
		/// </returns>
		/// <throws>  ImageIOException if the read fails </throws>
		/// <throws>  ImageNotFoundException if the file isn't found </throws>
		public static System.Drawing.Bitmap read(System.IO.FileInfo src)
		{
			System.IO.Stream in_Renamed = null;
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
				in_Renamed = new System.IO.BufferedStream(new System.IO.FileStream(src.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read));
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'img '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Bitmap img = read(in_Renamed);
				in_Renamed.Close();
				return img;
			}
			catch (System.IO.FileNotFoundException e)
			{
				throw new ImageNotFoundException(src, e);
			}
			catch (System.IO.IOException e)
			{
				throw new ImageIOException(src, e);
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
		
		/// <summary> Reads an image tile.
		/// 
		/// </summary>
		/// <param name="in">a stream containing the tile data
		/// </param>
		/// <returns> the tile image
		/// 
		/// </returns>
		/// <throws>  IOException if the read fails </throws>
		public static System.Drawing.Bitmap read(System.IO.Stream in_Renamed)
		{
			ByteBuffer bb;
			
			// read the header
			//UPGRADE_NOTE: Final was removed from the declaration of 'header '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] header = readHeader(in_Renamed);
			bb = ByteBuffer.wrap(header);
			
			// validate the signature
			//UPGRADE_NOTE: Final was removed from the declaration of 'sig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] sig = new sbyte[6];
			bb.get_Renamed(sig);
			checkSignature(sig);
			
			// get the dimensions and type
			//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int w = bb.getInt();
			//UPGRADE_NOTE: Final was removed from the declaration of 'h '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int h = bb.getInt();
			//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int type = bb.getInt();
			
			// read the image data
			//UPGRADE_NOTE: Final was removed from the declaration of 'cdata '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] cdata = IOUtils.toByteArray(in_Renamed);
			
			// decompress the image data
			System.IO.Stream zin = null;
			try
			{
				//UPGRADE_ISSUE: Constructor 'java.util.zip.GZIPInputStream.GZIPInputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPInputStream'"
				zin = new GZIPInputStream(new System.IO.MemoryStream(SupportClass.ToByteArray(cdata)));
				bb = ByteBuffer.wrap(IOUtils.toByteArray(zin));
				zin.Close();
			}
			finally
			{
				IOUtils.closeQuietly(zin);
			}
			
			// build the image
			//UPGRADE_NOTE: Final was removed from the declaration of 'img '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap img = new System.Drawing.Bitmap(w, h, (System.Drawing.Imaging.PixelFormat) type);
			
			// FIXME: This might decelerate the image? If so, then we should
			// make a copy.
			//UPGRADE_NOTE: Final was removed from the declaration of 'db '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.MemoryStream tempDataBuffer;
			tempDataBuffer = new System.IO.MemoryStream();
			//UPGRADE_TODO: Method 'java.awt.image.Raster.getDataBuffer' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageRastergetDataBuffer'"
			img.Save(tempDataBuffer, System.Drawing.Imaging.ImageFormat.Bmp);
			System.Collections.ArrayList db = (System.Collections.ArrayList) new System.Collections.ArrayList(tempDataBuffer.ToArray());
			//UPGRADE_NOTE: Final was removed from the declaration of 'data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int[] data = (int[]) db[0];
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'ib '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			IntBuffer ib = bb.asIntBuffer();
			ib.get_Renamed(data);
			
			/*
			if (ib.hasRemaining()) {
			// buffer contains garbage at the end!
			throw new IOException("found " + (4*ib.remaining()) + " more bytes!");
			}*/
			
			return img;
		}
		
		/// <summary> Reads the tile header from the stream.
		/// 
		/// </summary>
		/// <param name="in">the stream
		/// </param>
		/// <returns> the header
		/// 
		/// </returns>
		/// <throws>  IOException if the read fails or there is too little data </throws>
		internal static sbyte[] readHeader(System.IO.Stream in_Renamed)
		{
			// read the header
			//UPGRADE_NOTE: Final was removed from the declaration of 'header '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] header = new sbyte[18];
			if (IOUtils.read(in_Renamed, header) != header.Length)
			{
				throw new System.IO.IOException("header too short!");
			}
			
			return header;
		}
		
		/// <summary> Checks that the given byte array equals the tile signature.
		/// 
		/// </summary>
		/// <param name="sig">the byte array to check
		/// 
		/// </param>
		/// <throws>  IOException if the byte array is not the tile signature </throws>
		internal static void  checkSignature(sbyte[] sig)
		{
			if (!SupportClass.ArraySupport.Equals(SupportClass.ToByteArray(sig), SupportClass.ToByteArray("VASSAL")))
			{
				throw new System.IO.IOException("bad signature: got \"" + new System.String(SupportClass.ToCharArray(SupportClass.ToByteArray(sig))) + "\", expected \"VASSAL\"");
			}
		}
		
		/// <summary> Reads the dimensions of the tile in an image tile file.
		/// 
		/// </summary>
		/// <param name="src">the path of the tile file
		/// </param>
		/// <returns> the dimensions
		/// 
		/// </returns>
		/// <throws>  ImageIOException if the read fails </throws>
		/// <throws>  ImageNotFoundException if the file isn't found </throws>
		public static System.Drawing.Size size(System.String src)
		{
			return size(new System.IO.FileInfo(src));
		}
		
		/// <summary> Reads the dimensions of the tile in an image tile file.
		/// 
		/// </summary>
		/// <param name="src">the path of the tile file
		/// </param>
		/// <returns> the dimensions
		/// 
		/// </returns>
		/// <throws>  ImageIOException if the read fails </throws>
		/// <throws>  ImageNotFoundException if the file isn't found </throws>
		public static System.Drawing.Size size(System.IO.FileInfo src)
		{
			System.IO.Stream in_Renamed = null;
			try
			{
				// NB: We don't buffer here because we're reading only 18 bytes.
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
				in_Renamed = new System.IO.FileStream(src.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
				//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size d = size(in_Renamed);
				in_Renamed.Close();
				return d;
			}
			catch (System.IO.FileNotFoundException e)
			{
				throw new ImageNotFoundException(src, e);
			}
			catch (System.IO.IOException e)
			{
				throw new ImageIOException(src, e);
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
		
		/// <summary> Reads the dimensions of the tile from a stream.
		/// 
		/// </summary>
		/// <param name="in">the stream
		/// </param>
		/// <returns> the dimensions
		/// 
		/// </returns>
		/// <throws>  IOException if the read fails </throws>
		public static System.Drawing.Size size(System.IO.Stream in_Renamed)
		{
			ByteBuffer bb;
			
			// read the header
			//UPGRADE_NOTE: Final was removed from the declaration of 'header '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] header = readHeader(in_Renamed);
			bb = ByteBuffer.wrap(header);
			
			// validate the signature
			//UPGRADE_NOTE: Final was removed from the declaration of 'sig '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] sig = new sbyte[6];
			bb.get_Renamed(sig);
			checkSignature(sig);
			
			// get the dimensions
			return new Dimension(bb.getInt(), bb.getInt());
		}
		
		/// <summary> Write a tile image to a tile file.
		/// 
		/// </summary>
		/// <param name="tile">the image
		/// </param>
		/// <param name="dst">the tile file
		/// 
		/// </param>
		/// <throws>  ImageIOException if the write fails </throws>
		public static void  write(System.Drawing.Bitmap tile, System.String dst)
		{
			write(tile, new System.IO.FileInfo(dst));
		}
		
		/// <summary> Write a tile image to a tile file.
		/// 
		/// </summary>
		/// <param name="tile">the image
		/// </param>
		/// <param name="dst">the tile file
		/// 
		/// </param>
		/// <throws>  ImageIOException if the write fails </throws>
		public static void  write(System.Drawing.Bitmap tile, System.IO.FileInfo dst)
		{
			System.IO.Stream out_Renamed = null;
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javaioFile'"
				out_Renamed = new System.IO.BufferedStream(new System.IO.FileStream(dst.FullName, System.IO.FileMode.Create));
				write(tile, out_Renamed);
				out_Renamed.Close();
			}
			catch (System.IO.IOException e)
			{
				throw new ImageIOException(dst, e);
			}
			finally
			{
				IOUtils.closeQuietly(out_Renamed);
			}
		}
		
		/// <summary> Write a tile image to a stream.
		/// 
		/// </summary>
		/// <param name="tile">the image
		/// </param>
		/// <param name="out">the stream
		/// 
		/// </param>
		/// <throws>  ImageIOException if the write fails </throws>
		public static void  write(System.Drawing.Bitmap tile, System.IO.Stream out_Renamed)
		{
			ByteBuffer bb;
			
			// write the header
			bb = ByteBuffer.allocate(18);
			
			bb.put(SupportClass.ToSByteArray(SupportClass.ToByteArray("VASSAL"))).putInt(tile.Width).putInt(tile.Height).putInt((int) tile.PixelFormat);
			
			out_Renamed.write(bb.array());
			
			// write the tile data
			//UPGRADE_NOTE: Final was removed from the declaration of 'db '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.MemoryStream tempDataBuffer;
			tempDataBuffer = new System.IO.MemoryStream();
			//UPGRADE_TODO: Method 'java.awt.image.Raster.getDataBuffer' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageRastergetDataBuffer'"
			tile.Save(tempDataBuffer, System.Drawing.Imaging.ImageFormat.Bmp);
			System.Collections.ArrayList db = (System.Collections.ArrayList) new System.Collections.ArrayList(tempDataBuffer.ToArray());
			//UPGRADE_NOTE: Final was removed from the declaration of 'data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int[] data = (int[]) db[0];
			
			bb = ByteBuffer.allocate(4 * data.Length);
			bb.asIntBuffer().put(data);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'zout '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'java.util.zip.GZIPOutputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPOutputStream'"
			//UPGRADE_ISSUE: Constructor 'java.util.zip.GZIPOutputStream.GZIPOutputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPOutputStream'"
			GZIPOutputStream zout = new GZIPOutputStream(out_Renamed);
			zout.write(bb.array());
			//UPGRADE_ISSUE: Method 'java.util.zip.GZIPOutputStream.finish' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPOutputStream'"
			zout.finish();
		}
		
		/// <summary> Calculates the number of tiles needed to cover an image, summed over
		/// all sizes from 1:1 to the vanishing point.
		/// 
		/// </summary>
		/// <param name="i">the image dimensions
		/// </param>
		/// <param name="t">the tile dimensions
		/// </param>
		/// <returns> the number of tiles needed to cover the image
		/// 
		/// </returns>
		/// <throws>  IllegalArgumentException if any argument is nonpositive </throws>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public static int tileCount(ref System.Drawing.Size i, ref System.Drawing.Size t)
		{
			return tileCount(i.Width, i.Height, t.Width, t.Height);
		}
		
		/// <summary> Calculates the number of tiles needed to cover an image, summed over
		/// all sizes from 1:1 to the vanishing point.
		/// 
		/// </summary>
		/// <param name="iw">the image width
		/// </param>
		/// <param name="ih">the image height
		/// </param>
		/// <param name="tw">the tile width
		/// </param>
		/// <param name="th">the tile height
		/// </param>
		/// <returns> the number of tiles needed to cover the image
		/// 
		/// </returns>
		/// <throws>  IllegalArgumentException if any argument is nonpositive </throws>
		public static int tileCount(int iw, int ih, int tw, int th)
		{
			// TODO: Find a closed-form expression for this, if there is one.
			int tcount = 0;
			for (int div = 1; iw / div > 0 && ih / div > 0; div <<= 1)
			{
				tcount += tileCountAtScale(iw, ih, tw, th, div);
			}
			return tcount;
		}
		
		/// <summary> Calculates the number of tiles needed to cover an image at a given
		/// scale.
		/// 
		/// </summary>
		/// <param name="i">the image dimensions
		/// </param>
		/// <param name="t">the tile dimensions
		/// </param>
		/// <param name="div">the scale divisor
		/// </param>
		/// <returns> the number of tiles needed to cover the image
		/// 
		/// </returns>
		/// <throws>  IllegalArgumentException if any argument is nonpositive </throws>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public static int tileCountAtScale(ref System.Drawing.Size i, ref System.Drawing.Size t, int div)
		{
			return tileCountAtScale(i.Width, i.Height, t.Width, t.Height, div);
		}
		
		/// <summary> Calculates the number of tiles needed to cover an image at a given
		/// scale.
		/// 
		/// </summary>
		/// <param name="iw">the image width
		/// </param>
		/// <param name="ih">the image height
		/// </param>
		/// <param name="tw">the tile width
		/// </param>
		/// <param name="th">the tile height
		/// </param>
		/// <param name="div">the scale divisor
		/// </param>
		/// <returns> the number of tiles needed to cover the image
		/// 
		/// </returns>
		/// <throws>  IllegalArgumentException if any argument is nonpositive </throws>
		public static int tileCountAtScale(int iw, int ih, int tw, int th, int div)
		{
			if (iw < 1)
				throw new System.ArgumentException("iw = " + iw + " < 1");
			if (ih < 1)
				throw new System.ArgumentException("ih = " + ih + " < 1");
			if (tw < 1)
				throw new System.ArgumentException("tw = " + tw + " < 1");
			if (th < 1)
				throw new System.ArgumentException("th = " + th + " < 1");
			if (div < 1)
				throw new System.ArgumentException("div = " + div + " < 1");
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'cols '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int cols = (int) System.Math.Ceiling((double) (iw / div) / tw);
			//UPGRADE_NOTE: Final was removed from the declaration of 'rows '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int rows = (int) System.Math.Ceiling((double) (ih / div) / th);
			return cols * rows;
		}
		
		/// <summary> Gets the name of a tile file.
		/// 
		/// </summary>
		/// <param name="iname">the image name
		/// </param>
		/// <param name="tileX">the X coordinate of the tile
		/// </param>
		/// <param name="tileY">the Y coordinate of the tile
		/// </param>
		/// <param name="div">the scale divisor
		/// </param>
		/// <returns> the name of the tile file
		/// </returns>
		public static System.String tileName(System.String iname, int tileX, int tileY, int div)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'sha '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String sha = DigestUtils.shaHex(iname + "(" + tileX + "," + tileY + "@1:" + div);
			
			return sha.Substring(0, (1) - (0)) + '/' + sha.Substring(0, (2) - (0)) + '/' + sha;
		}
	}
}