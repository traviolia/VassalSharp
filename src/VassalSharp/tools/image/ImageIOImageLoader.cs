/*
* $Id$
*
* Copyright (c) 2009-2013 by Joel Uckelman
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
//UPGRADE_TODO: The type 'javax.imageio.ImageIO' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ImageIO = javax.imageio.ImageIO;
//UPGRADE_TODO: The type 'javax.imageio.ImageReader' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ImageReader = javax.imageio.ImageReader;
//UPGRADE_TODO: The type 'javax.imageio.stream.ImageInputStream' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ImageInputStream = javax.imageio.stream.ImageInputStream;
//UPGRADE_TODO: The type 'javax.imageio.stream.MemoryCacheImageInputStream' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MemoryCacheImageInputStream = javax.imageio.stream.MemoryCacheImageInputStream;
using IOUtils = VassalSharp.tools.io.IOUtils;
using RereadableInputStream = VassalSharp.tools.io.RereadableInputStream;
using Reference = VassalSharp.tools.lang.Reference;
namespace VassalSharp.tools.image
{
	
	/// <summary> An image loader which wraps {@link ImageIO}.
	/// 
	/// This class handles the assorted problems with various versions of
	/// {@link ImageIO}, ensuring that we can reliably load image files to
	/// {@link BufferedImages} with a predictable type.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class ImageIOImageLoader : ImageLoader
	{
		/// <summary> Loads an image.
		/// 
		/// </summary>
		/// <param name="name">the image name
		/// </param>
		/// <param name="in">the input stream
		/// </param>
		/// <returns> the image
		/// 
		/// </returns>
		/// <throws>  UnrecognizedImageTypeException if the image type is unknown </throws>
		/// <throws>  IOException if reading the image goes wrong </throws>
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			T run(String name, InputStream in) throws IOException;
			try
			{
				return w.run(name, in_Renamed);
			}
			catch (System.IndexOutOfRangeException e)
			{
				// Note: ImageIO can throw an ArrayIndexOutOfBoundsException for
				// some corrupt JPEGs. This problem is noted in Sun Bug 6351707,
				//
				// http://bugs.sun.com/bugdatabase/view_bug.do?bug_id=6351707
				//
				throw new BrokenImageException(name, e);
			}
			//UPGRADE_NOTE: Exception 'java.awt.color.CMMException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			catch (System.Exception e)
			{
				// Note: ImageIO can throw a CMMException for JPEGs which have
				// broken color profiles. This problem is noted in Sun Bugs 6444360
				// and 6839133.
				//
				// http://bugs.sun.com/view_bug.do?bug_id=6444360
				// http://bugs.sun.com/view_bug.do?bug_id=6839133
				//
				throw new BrokenImageException(name, e);
			}
			catch (System.ArgumentException e)
			{
				// Note: ImageIO can throw IllegalArgumentExceptions for certain
				// kinds of broken images, e.g., JPEGs which are in the RGB color
				// space but have non-RGB color profiles (see Bug 2673589 for an
				// example of this). This problem is noted in Sun Bug 6404011,
				//
				// http://bugs.sun.com/view_bug.do?bug_id=6404011
				//
				throw new BrokenImageException(name, e);
			}
			catch (ImageIOException e)
			{
				// Don't wrap ImageIOExceptions.
				throw e;
			}
			catch (System.IO.IOException e)
			{
				throw new ImageIOException(name, e);
			}
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'tconv '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal ImageTypeConverter tconv;
		
		/// <summary> Create an image loader.
		/// 
		/// </summary>
		/// <param name="tconv">the <code>ImageTypeConverter</code> to use for type
		/// conversions
		/// </param>
		public ImageIOImageLoader(ImageTypeConverter tconv)
		{
			InitBlock();
			this.tconv = tconv;
		}
		
		// Used to indicate whether this version of Java has the PNG iTXt bug.
		// This can be removed once we no longer support Java 1.5.
		protected internal static bool iTXtBug;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static final Set < Integer > skip_iTXt = 
		Collections.singleton(PNGDecoder.iTXt);
		
		// Used to indicate whether this version of Java has the JPEG color
		// correction bug.
		protected internal static bool YCbCrBug;
		
		/// <summary> Loads an image.
		/// 
		/// </summary>
		/// <param name="name">the image name
		/// </param>
		/// <param name="in">the input stream
		/// </param>
		/// <param name="typeIfOpaque">the requested image type for opaque images
		/// </param>
		/// <param name="typeIfTransparent">the requested image type for transparent images
		/// </param>
		/// <param name="managed"><code>true</code> if a managed image should be returned
		/// </param>
		/// <returns> the image
		/// 
		/// </returns>
		/// <throws>  BrokenImageException if the image is faulty </throws>
		/// <throws>  UnrecognizedImageTypeException if the image type is not recognized </throws>
		/// <throws>  ImageIOException if reading the image goes wrong </throws>
		public virtual System.Drawing.Bitmap load(System.String name, System.IO.Stream in_Renamed, int typeIfOpaque, int typeIfTransparent, bool managed)
		{
			//
			// ImageIO fails on the following types of images:
			//
			// Sun Bug 6788458: 8-bit/channel color type 2 (RGB) PNGs with tRNS chunks
			// Sun Bug 6541476: PNGs with iTXt chunks on Java 1.5
			// Sun Bug 6444360: JPEGs with corrupt color profiles
			// Sun Bug 6404011: JPEGs with corrupt color profiles on Java 1.5
			// Sun Bug 4712797: YCbCr JPEGs with no JFIF marker
			// Sun Bug 4776576: YCbCr JPEFs with no JFIF marker
			//
			// http://bugs.sun.com/view_bug.do?bug_id=6788458
			// http://bugs.sun.com/view_bug.do?bug_id=6541476
			// http://bugs.sun.com/view_bug.do?bug_id=6444360
			// http://bugs.sun.com/view_bug.do?bug_id=6404011
			// http://bugs.sun.com/view_bug.do?bug_id=4712797
			// http://bugs.sun.com/view_bug.do?bug_id=4776576
			//
			// Someday, when both ImageIO is fixed and everyone's JRE contains
			// that fix, we can do this the simple way.
			//
			
			bool fix_tRNS = false;
			int tRNS = 0x00000000;
			
			bool fix_YCbCr = false;
			
			System.Drawing.Bitmap img = null;
			RereadableInputStream rin = null;
			try
			{
				rin = new RereadableInputStream(in_Renamed);
				rin.mark(512);
				
				//UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
				System.IO.BinaryReader din = new System.IO.BinaryReader(rin);
				
				// Is this a PNG?
				if (PNGDecoder.decodeSignature(din))
				{
					// The PNG chunks refered to here are defined in the PNG
					// standard, found at http://www.w3.org/TR/PNG/
					PNGDecoder.Chunk ch = PNGDecoder.decodeChunk(din);
					
					// Sanity check: This is not a PNG if IHDR is not the first chunk.
					if (ch.type == PNGDecoder.IHDR)
					{
						// At present, ImageIO does not honor the tRNS chunk in 8-bit color
						// type 2 (RGB) PNGs. This is not a bug per se, as the PNG standard
						// the does not require compliant decoders to use ancillary chunks.
						// However, every other PNG decoder we can find *does* honor the
						// tRNS chunk for this type of image, and so the appearance for
						// users is that VASSAL is broken when their 8-bit RGB PNGs don't
						// show the correct transparency.
						
						// We check for type-2 8-bit PNGs with tRNS chunks.
						if (ch.data[8] == 8 && ch.data[9] == 2)
						{
							// This is an 8-bit-per-channel Truecolor image; we must check
							// whether there is a tRNS chunk, and if so, record the color
							// so that we can manually set transparency later.
							//
							// IHDR is required to be first, and tRNS is required to appear
							// before the first IDAT chunk; therefore, if we find an IDAT
							// we're done.
							
							for (; ; )
							{
								ch = PNGDecoder.decodeChunk(din);
								
								switch (ch.type)
								{
									
									case PNGDecoder.tRNS:  fix_tRNS = true; //UPGRADE_NOTE: Labeled break statement was changed to a goto statement. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1012'"
										goto DONE_PNG_brk;
									
									case PNGDecoder.IDAT:  fix_tRNS = false; //UPGRADE_NOTE: Labeled break statement was changed to a goto statement. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1012'"
										goto DONE_PNG_brk;
									
									default: 
										break;
									
								}
							}
							//UPGRADE_NOTE: Label 'DONE_PNG_brk' was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1011'"
DONE_PNG_brk: ;
							
							
							if (fix_tRNS)
							{
								if (ch.data.Length != 6)
								{
									// There is at least one piece of software (SplitImage) which
									// writes tRNS chunks for type 2 images which are only 3 bytes
									// long, and because this kind of thing is used by module
									// designers for slicing up scans of countersheets, we can
									// expect to see such crap from time to time.
									throw new BrokenImageException(name, "bad tRNS chunk length");
								}
								
								//
								// tRNS chunk: PNG Standard, 11.3.2.1
								//
								// tRNS data is stored as three 2-byte samples, but the high
								// byte of each sample is empty because we are dealing with
								// 8-bit-per-channel images.
								tRNS = unchecked((int) 0xff000000) | ((ch.data[1] & 0xff) << 16) | ((ch.data[3] & 0xff) << 8) | (ch.data[5] & 0xff);
							}
						}
						
						if (iTXtBug)
						{
							// Filter out iTXt chunks on JVMs with the iTXt bug.
							rin.reset();
							rin = new RereadableInputStream(new PNGChunkSkipInputStream(skip_iTXt, rin));
							rin.mark(1);
						}
					}
				}
				else if (YCbCrBug)
				{
					rin.reset();
					rin.mark(512);
					
					din = new System.IO.BinaryReader(rin);
					
					// Is this a JPEG?
					if (JPEGDecoder.decodeSignature(din))
					{
						// The case where ImageIO fails is when there is no JFIF marker,
						// no color profile, and three color components with the same
						// subsampling. In this case, ImageIO incorrectly assumes that
						// this image is RGB instead of YCbCr.
						
						JPEGDecoder.Chunk ch;
						fix_YCbCr = true;
						
						for (; ; )
						{
							ch = JPEGDecoder.decodeChunk(din);
							
							switch (ch.type)
							{
								
								case JPEGDecoder.SOF0: 
								case JPEGDecoder.SOF1: 
								case JPEGDecoder.SOF2: 
								case JPEGDecoder.SOF3: 
								case JPEGDecoder.SOF4: 
								case JPEGDecoder.SOF5: 
								case JPEGDecoder.SOF6: 
								case JPEGDecoder.SOF7: 
								case JPEGDecoder.SOF9: 
								case JPEGDecoder.SOF10: 
								case JPEGDecoder.SOF11: 
								case JPEGDecoder.SOF12: 
								case JPEGDecoder.SOF13: 
								case JPEGDecoder.SOF14: 
								case JPEGDecoder.SOF15: 
									// The JPEG standard requires any APPn markers to appear before
									// the first SOF marker, so if we see an SOF marker, we know
									// there are no APPn markers to find. Hence, we can decide now
									// whether this JPEG triggers the bug.
									fix_YCbCr = ch.data.Length == 15 && ch.data[5] == 3 && ch.data[7] == ch.data[10] && ch.data[7] == ch.data[13];
									//UPGRADE_NOTE: Labeled break statement was changed to a goto statement. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1012'"
									goto DONE_JPEG_brk;
								
								
								case JPEGDecoder.APP0: 
									if (ch.data.Length >= 4 && ch.data[0] == 'J' && ch.data[1] == 'F' && ch.data[2] == 'I' && ch.data[3] == 'F')
									{
										// We've seen a JFIF, this image is ok.
										fix_YCbCr = false;
										//UPGRADE_NOTE: Labeled break statement was changed to a goto statement. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1012'"
										goto DONE_JPEG_brk;
									}
									break;
								
								
								case JPEGDecoder.APP2: 
									// Check whether we have a color profile. If so, then ImageIO
									// can handle decoding the image.
									if (ch.data.Length >= 12 && ch.data[0] == 'I' && ch.data[1] == 'C' && ch.data[2] == 'C' && ch.data[3] == '_' && ch.data[4] == 'P' && ch.data[5] == 'R' && ch.data[6] == 'O' && ch.data[7] == 'F' && ch.data[8] == 'I' && ch.data[9] == 'L' && ch.data[10] == 'E' && ch.data[11] == 0x00)
									{
										// We have a color profile, this image is ok.
										fix_YCbCr = false;
										//UPGRADE_NOTE: Labeled break statement was changed to a goto statement. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1012'"
										goto DONE_JPEG_brk;
									}
									break;
								
								
								case JPEGDecoder.APP13: 
								case JPEGDecoder.APP14: 
									// Created by Photoshop, this image is ok.
									fix_YCbCr = false;
									//UPGRADE_NOTE: Labeled break statement was changed to a goto statement. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1012'"
									goto DONE_JPEG_brk;
								
								
								case JPEGDecoder.SOS: 
								// We've reached a Start of Scan marker. Following this
								// is not a normal segment, but instead a lot of raw data.
								// This probably shouldn't happen with a valid JPEG.
								case JPEGDecoder.EOI: 
									// We've reached the end. This probably shouldn't happen.
									//UPGRADE_NOTE: Labeled break statement was changed to a goto statement. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1012'"
									goto DONE_JPEG_brk;
								
								default: 
									break;
								
							}
						}
						//UPGRADE_NOTE: Label 'DONE_JPEG_brk' was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1011'"
DONE_JPEG_brk: ;
						
					}
				}
				
				// Load the image
				rin.reset();
				img = wrapImageIO(name, rin, readImage);
				rin.Close();
			}
			catch (ImageIOException e)
			{
				// Don't wrap ImageIOExceptions.
				throw e;
			}
			catch (System.IO.IOException e)
			{
				throw new ImageIOException(name, e);
			}
			finally
			{
				IOUtils.closeQuietly(rin);
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int type = img.getTransparency() == BufferedImage.OPAQUE && !fix_tRNS?typeIfOpaque:typeIfTransparent;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Reference < BufferedImage > ref = new Reference < BufferedImage >(img);
			
			if (fix_tRNS)
			{
				// Fix up transparency in type 2 Truecolor images.
				img = null;
				img = fix_tRNS(ref_Renamed, tRNS, type);
				ref_Renamed.obj = img;
			}
			else if (fix_YCbCr)
			{
				// Fix up color space in misinterpreted JPEGs.
				img = null;
				img = fix_YCbCr(ref_Renamed, type);
				ref_Renamed.obj = img;
			}
			
			// We convert the image in two cases:
			// 1) the image is not yet the requested type, or
			// 2) a managed image was requested, but the image
			//    was unmanaged by the transparency fix.
			if ((int) img.PixelFormat != type || (fix_tRNS && managed))
			{
				img = null;
				img = tconv.convert(ref_Renamed, type);
			}
			
			return img;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		interface Wrapper < T >
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected < T > T wrapImageIO(String name, InputStream in, Wrapper < T > w) 
		throws ImageIOException
		
		/// <summary>A functor for reading images. </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static Wrapper < BufferedImage > readImage = 
		new Wrapper < BufferedImage >()
		public virtual System.Drawing.Bitmap run(System.String name, System.IO.Stream in_Renamed)
		{
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'img '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap img = ImageIO.read(new MemoryCacheImageInputStream(in_Renamed));
			if (img == null)
				throw new UnrecognizedImageTypeException(name);
			
			return img;
		}
		static ImageIOImageLoader()
		{
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'jvmver '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'java.lang.System.getProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
				System.String jvmver = System_Renamed.getProperty("java.version");
				iTXtBug = jvmver == null || jvmver.StartsWith("1.5");
			}
			{
				System.Drawing.Bitmap img = null;
				
				System.IO.Stream in_Renamed = null;
				try
				{
					// We intentionally bypass the normal image loading system
					// in order to see how ImageIO loads the test image.
					//UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
					in_Renamed = typeof(ImageIOImageLoader).getResourceAsStream("/images/black.jpg");
					img = ImageIO.read(new MemoryCacheImageInputStream(in_Renamed));
					in_Renamed.Close();
				}
				catch (System.IO.IOException e)
				{
					// this should not happen
					throw new System.SystemException();
				}
				finally
				{
					IOUtils.closeQuietly(in_Renamed);
				}
				
				if (img == null)
				{
					// this should not happen
					throw new System.SystemException();
				}
				
				// The pixel in the image is supposed to be black. If the pixel is
				// green, then ImageIO is misinterpreting the YCbCr data as RGB. If
				// the pixel is turquoise, then ImageIO is misinterpreting the color
				// in yet another way, which will also lead to same YCbCr mangling.
				// (So far the turquoise pixel happens only with 1.7.0_21 and 1.7.0_25
				// JVMs on Linux...)
				//UPGRADE_NOTE: Final was removed from the declaration of 'pixel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int pixel = img.GetPixel(0, 0).ToArgb();
				switch (pixel)
				{
					
					case unchecked((int) 0xFF000000): 
						YCbCrBug = false;
						break;
					
					case unchecked((int) 0xFF008080): 
					case unchecked((int) 0xFF008700): 
						YCbCrBug = true;
						break;
					
					default: 
						// This JVM is broken in an unexpected way!
						throw new System.SystemException("Unexpected pixel value 0x" + String.format("%08x", pixel));
					
				}
			}
			// Workaround for Sun Bug 6986863:
			//
			//   http://bugs.sun.com/bugdatabase/view_bug.do?bug_id=6986863
			//
			// java.awt.color.ICC_Profile.getInstance() is static but isn't thread
			// safe (!) and is called from JPEGImageReader.getWidth(). This means that
			// not only is JPEGImageReader.getWidth() not thread-safe, but it's not
			// even thread-safe across different instances of JPEGImageReader. Nobody
			// will ever be trying to load more than one JPEG at a time, right? WTF?!
			//
			// To mitigate this, we attempt to turn off ProfileDeferralMgr, which will
			// stop calls to ProfileDeferralMgr.activateProfiles(), which is where the
			// race happens.
			{
				// Try to find the ProfileDeferralMgr. It's is not a public part of the
				// JDK and it changed packages at some point, so we look both places,
				// newer location first.
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				Class < ? > c = null;
				try
				{
					//UPGRADE_TODO: The differences in the format  of parameters for method 'java.lang.Class.forName'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					c = System.Type.GetType("sun.java2d.cmm.ProfileDeferralMgr");
				}
				//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception e)
				{
					try
					{
						//UPGRADE_TODO: The differences in the format  of parameters for method 'java.lang.Class.forName'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
						c = System.Type.GetType("sun.awt.color.ProfileDeferralMgr");
					}
					//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
					catch (System.Exception ignore)
					{
						// No ProfileDeferralMgr, so probably no bug either.
					}
				}
				
				if (c != null)
				{
					System.Reflection.FieldInfo df = null;
					try
					{
						df = c.getField("deferring");
					}
					catch (System.FieldAccessException ignore)
					{
						// Nothing we can do
					}
					
					if (df != null)
					{
						try
						{
							if ((bool) df.GetValue(null))
							{
								System.Reflection.MethodInfo am = null;
								try
								{
									am = c.getMethod("activateProfiles");
								}
								catch (System.MethodAccessException ignore)
								{
									// Nothing we can do
								}
								
								if (am != null)
								{
									try
									{
										am.invoke(null);
										df.SetValue(null, false);
									}
									catch (System.Reflection.TargetInvocationException ignore)
									{
										// Nothing we can do
									}
								}
							}
						}
						catch (System.UnauthorizedAccessException ignore)
						{
							// Nothing we can do
						}
					}
				}
			}
		}
	}
	
	
	/// <summary>A functor for reading image dimensions. </summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected static Wrapper < Dimension > readSize = new Wrapper < Dimension >()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	/// <summary> Gets the size of an image.
	/// 
	/// </summary>
	/// <param name="name">the image name
	/// </param>
	/// <param name="in">the input stream
	/// </param>
	/// <returns> the size of the image
	/// 
	/// </returns>
	/// <throws>  BrokenImageException if the image is faulty </throws>
	/// <throws>  UnrecognizedImageTypeException if the image type is unknown </throws>
	/// <throws>  IOException if reading the image goes wrong </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Dimension run(String name, InputStream in) throws IOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final ImageInputStream stream = new MemoryCacheImageInputStream(in);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final Iterator < ImageReader > i = ImageIO.getImageReaders(stream);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(!i.hasNext()) throw new UnrecognizedImageTypeException(name);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final ImageReader reader = i.next();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		reader.setInput(stream);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return new Dimension(reader.getWidth(0), reader.getHeight(0));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	finally
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		reader.dispose();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected BufferedImage fix_tRNS(Reference < BufferedImage > ref, 
	int tRNS, int type) throws ImageIOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		BufferedImage img = ref.obj;
	
	// Ensure that we are working with integer ARGB data. Whether it's
	// premultiplied doesn't matter, since fully transparent black pixels
	// are the same in both.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(img.getType() != BufferedImage.TYPE_INT_ARGB && 
	img.getType() != BufferedImage.TYPE_INT_ARGB_PRE)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	
	// If the requested type is not an ARGB one, then we convert to ARGB
	// for applying this fix.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(type != BufferedImage.TYPE_INT_ARGB && 
	type != BufferedImage.TYPE_INT_ARGB_PRE)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		type = BufferedImage.TYPE_INT_ARGB;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	img = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	img = tconv.convert(ref, type);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	// NB: This unmanages the image.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final DataBufferInt db =(DataBufferInt) img.getRaster().getDataBuffer();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final int [] data = db.getData();
	
	// Set all pixels of the transparent color to have alpha 0.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < data.length;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(data [i] == tRNS) data [i] = 00000000;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	return img;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected BufferedImage fix_YCbCr(Reference < BufferedImage > ref, int type) 
	throws ImageIOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		BufferedImage img = ref.obj;
	
	// Ensure that we are working with RGB or ARGB data.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(img.getType() != BufferedImage.TYPE_INT_RGB && 
	img.getType() != BufferedImage.TYPE_INT_ARGB)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		
		if(type != BufferedImage.TYPE_INT_RGB && 
		type != BufferedImage.TYPE_INT_ARGB)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		type = BufferedImage.TYPE_INT_ARGB;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	img = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	img = tconv.convert(ref, type);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	// NB: This unmanages the image.
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final DataBufferInt db =(DataBufferInt) img.getRaster().getDataBuffer();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final int [] data = db.getData();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	for(int i = 0;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	i < data.length;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	++ i)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final int y =(data [i] >> 16) & FF;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final int pb =((data [i] >> 8) & FF) - 128;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final int pr =(data [i] & FF) - 128;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final int a =(data [i] >> 24) & FF;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final int r =(int) Math.round(y + 1.402 * pr);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final int g =(int) Math.round(y - 0.34414 * pb - 0.71414 * pr);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final int b =(int) Math.round(y + 1.772 * pb);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	data [i] =(a << 24) |
	((r < 0 ? 0:(r > FF ? FF: r)) << 16) |
	((g < 0 ? 0:(g > FF ? FF: g)) << 8) |
	(b < 0 ? 0:(b > FF ? FF: b));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	return img;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	/// <summary> Gets the size of an image.
	/// 
	/// </summary>
	/// <param name="name">the image name
	/// </param>
	/// <param name="in">the input stream
	/// </param>
	/// <returns> the size of the image
	/// 
	/// </returns>
	/// <throws>  BrokenImageException if the image is faulty </throws>
	/// <throws>  UnrecognizedImageTypeException if the image type is not recognized </throws>
	/// <throws>  ImageIOException if reading the image goes wrong </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Dimension size(String name, InputStream in) throws ImageIOException
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return wrapImageIO(name, in, readSize);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}