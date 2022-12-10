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
using IOUtils = VassalSharp.tools.io.IOUtils;
using TemporaryFileFactory = VassalSharp.tools.io.TemporaryFileFactory;
using Reference = VassalSharp.tools.lang.Reference;
namespace VassalSharp.tools.image
{
	
	/// <summary> Convert a {@link BufferedImage} to a different type by caching image
	/// data on disk.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class FileImageTypeConverter : ImageTypeConverter
	{
		private void  InitBlock()
		{
			if (ref_Renamed == null)
				throw new System.ArgumentException();
			
			// This is why we pass the image via a Reference:
			//
			// So long as the calling method holds no references to the image, we
			// can ensure that our reference to the source image is the only one.
			//
			// This converter doesn't require the source and destination images to
			// exist simultaneously, so when we finish with the source, we can null
			// its reference. Because there are no other references to the source
			// image, this will make it eligible for garbage collection.
			//
			// Hence, we can let the source image be gc'd before we create the
			// destination image, potentially saving a huge amount of RAM.
			//
			System.Drawing.Bitmap src = ref_Renamed.obj;
			ref_Renamed.obj = null;
			
			// we can't create images of TYPE_CUSTOM
			if (type == (int) System.Drawing.Imaging.PixelFormat.Undefined)
				throw new System.ArgumentException();
			
			System.IO.FileInfo tmp = null;
			try
			{
				tmp = tfactory.create();
			}
			catch (System.IO.IOException e)
			{
				throw new ImageIOException("", e);
			}
			
			try
			{
				// write the converted image data to a file
				System.IO.Stream out_Renamed = null;
				try
				{
					//UPGRADE_ISSUE: Constructor 'java.util.zip.GZIPOutputStream.GZIPOutputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPOutputStream'"
					//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javaioFile'"
					out_Renamed = new System.IO.BufferedStream(new GZIPOutputStream(new System.IO.FileStream(tmp.FullName, System.IO.FileMode.Create)));
					write(src, out_Renamed);
					out_Renamed.Close();
				}
				catch (System.IO.IOException e)
				{
					throw new ImageIOException(tmp, e);
				}
				finally
				{
					IOUtils.closeQuietly(out_Renamed);
				}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int w = src.Width;
				//UPGRADE_NOTE: Final was removed from the declaration of 'h '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int h = src.Height;
				
				// ensure that src can be gc'd before we create dst
				src = null;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'dst '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Bitmap dst = new BufferedImage(w, h, type);
				
				// read the converted image data back
				System.IO.Stream in_Renamed = null;
				try
				{
					//UPGRADE_ISSUE: Constructor 'java.util.zip.GZIPInputStream.GZIPInputStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipGZIPInputStream'"
					//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
					in_Renamed = new System.IO.BufferedStream(new GZIPInputStream(new System.IO.FileStream(tmp.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read)));
					read(in_Renamed, dst);
					in_Renamed.Close();
					return dst;
				}
				catch (System.IO.IOException e)
				{
					throw new ImageIOException(tmp, e);
				}
				finally
				{
					IOUtils.closeQuietly(in_Renamed);
				}
			}
			finally
			{
				// clean up the temporary file
				bool tmpBool;
				if (System.IO.File.Exists(tmp.FullName))
				{
					System.IO.File.Delete(tmp.FullName);
					tmpBool = true;
				}
				else if (System.IO.Directory.Exists(tmp.FullName))
				{
					System.IO.Directory.Delete(tmp.FullName);
					tmpBool = true;
				}
				else
					tmpBool = false;
				if (!tmpBool)
				{
					throw new ImageIOException(tmp, "failed to delete");
				}
			}
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'tfactory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal TemporaryFileFactory tfactory;
		
		/// <summary> Create a converter.
		/// 
		/// </summary>
		/// <param name="tmp">the temporary file to use as a cache
		/// </param>
		public FileImageTypeConverter(TemporaryFileFactory tfactory)
		{
			InitBlock();
			if (tfactory == null)
				throw new System.ArgumentException();
			this.tfactory = tfactory;
		}
		
		/// <summary> {@inheritDoc}
		/// 
		/// <b>WARNING: When this method is called, the sole reference to the image
		/// must be the one held by <code>ref</code> in order to allow the source
		/// image to be garbage collected after the image data is written to disk.</b>
		/// </summary>
		public System.Drawing.Bitmap convert;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Reference < BufferedImage > ref, int type) 
		throws ImageIOException
		
		protected internal virtual void  write(System.Drawing.Bitmap src, System.IO.Stream out_Renamed)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int w = src.Width;
			//UPGRADE_NOTE: Final was removed from the declaration of 'h '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int h = src.Height;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'bb '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ByteBuffer bb = ByteBuffer.allocate(4 * w);
			//UPGRADE_NOTE: Final was removed from the declaration of 'row '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int[] row = new int[w];
			
			for (int y = 0; y < h; ++y)
			{
				// get the row in ARGB format
				//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.getRGB' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImagegetRGB_int_int_int_int_int[]_int_int'"
				src.getRGB(0, y, w, 1, row, 0, w);
				
				// copy the row to the byte buffer
				bb.asIntBuffer().put(row);
				
				// write the row to the stream
				out_Renamed.write(bb.array());
			}
		}
		
		protected internal virtual void  read(System.IO.Stream in_Renamed, System.Drawing.Bitmap dst)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int w = dst.Width;
			//UPGRADE_NOTE: Final was removed from the declaration of 'h '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int h = dst.Height;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'bytes '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] bytes = new sbyte[4 * w];
			//UPGRADE_NOTE: Final was removed from the declaration of 'bb '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ByteBuffer bb = ByteBuffer.wrap(bytes);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'row '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int[] row = new int[w];
			
			for (int y = 0; y < h; ++y)
			{
				// read the row from the stream
				IOUtils.read(in_Renamed, bytes);
				
				// convert the bytes to an int[]
				bb.asIntBuffer().get_Renamed(row);
				
				// write the row back to the image
				//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.setRGB' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImagesetRGB_int_int_int_int_int[]_int_int'"
				dst.setRGB(0, y, w, 1, row, 0, w);
			}
		}
	}
}