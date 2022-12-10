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
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
using Info = VassalSharp.Info;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using TemporaryFileFactory = VassalSharp.tools.io.TemporaryFileFactory;
namespace VassalSharp.tools.image
{
	
	public class ImageUtils
	{
		public class AnonymousClassTemporaryFileFactory : TemporaryFileFactory
		{
			public virtual System.IO.FileInfo create()
			{
				//UPGRADE_ISSUE: Method 'java.io.File.createTempFile' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaioFilecreateTempFile_javalangString_javalangString_javaioFile'"
				return File.createTempFile("img", null, Info.TempDir);
			}
		}
		public static bool HighQualityScaling
		{
			set
			{
			}
			
		}
		public static System.Collections.Hashtable DefaultHints
		{
			get
			{
				return new RenderingHints(defaultHints);
			}
			
		}
		public static bool MacRetina
		{
			get
			{
				return IS_MAC_RETINA;
			}
			
		}
		private static bool Headless
		{
			get
			{
				return GraphicsEnvironment.Headless;
			}
			
		}
		//UPGRADE_ISSUE: Class 'java.awt.GraphicsConfiguration' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphicsConfiguration'"
		private static GraphicsConfiguration GraphicsConfiguration
		{
			get
			{
				//UPGRADE_ISSUE: Method 'java.awt.GraphicsDevice.getDefaultConfiguration' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphicsDevice'"
				//UPGRADE_TODO: Method 'java.awt.GraphicsEnvironment.getDefaultScreenDevice' was converted to 'System.Windows.Forms.Screen' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphicsEnvironmentgetDefaultScreenDevice'"
				return System.Windows.Forms.Screen.PrimaryScreen.getDefaultConfiguration();
			}
			
		}
		public static int CompatibleTranslucentImageType
		{
			get
			{
				return compatTranslImageType;
			}
			
		}
		private ImageUtils()
		{
		}
		
		// FIXME: We should fix this, eventually.
		// negative, because historically we've done it this way
		//UPGRADE_NOTE: Final was removed from the declaration of 'DEGTORAD '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly double DEGTORAD = (- System.Math.PI) / 180.0;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'upscale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly GeneralFilter.Filter upscale = new GeneralFilter.MitchellFilter();
		//UPGRADE_NOTE: Final was removed from the declaration of 'downscale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly GeneralFilter.Filter downscale = new GeneralFilter.Lanczos3Filter();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public const System.String SCALER_ALGORITHM = "scalerAlgorithm"; //$NON-NLS-1$
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private static final Map < RenderingHints.Key, Object > defaultHints = 
		new HashMap < RenderingHints.Key, Object >();
		
		/// <deprecated> All scaling is done with the high-quality scaler now. 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public static System.Drawing.Rectangle transform(ref System.Drawing.Rectangle srect, double scale, double angle)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Drawing2D.Matrix temp_Matrix;
			temp_Matrix = new System.Drawing.Drawing2D.Matrix();
			temp_Matrix.RotateAt((float) (DEGTORAD * angle * (180 / System.Math.PI)), new System.Drawing.PointF((float) (srect.GetBounds().X + (srect.GetBounds().Width / 2)), (float) (srect.GetBounds().Y + (srect.GetBounds().Height / 2))));
			System.Drawing.Drawing2D.Matrix t = temp_Matrix;
			t.Scale((System.Single) scale, (System.Single) scale);
			//UPGRADE_ISSUE: Method 'java.awt.geom.AffineTransform.createTransformedShape' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtgeomAffineTransformcreateTransformedShape_javaawtShape'"
			return System.Drawing.Rectangle.Truncate(t.createTransformedShape(srect).GetBounds());
		}
		
		public static System.Drawing.Bitmap transform(System.Drawing.Bitmap src, double scale, double angle)
		{
			return transform(src, scale, angle, DefaultHints);
		}
		
		public static System.Drawing.Bitmap transform(System.Drawing.Bitmap src, double scale, double angle, System.Collections.Hashtable hints)
		{
			// bail on null source
			if (src == null)
				return null;
			
			// nothing to do, return source
			if (scale == 1.0 && angle == 0.0)
			{
				return src;
			}
			
			// return null image if scaling makes source vanish
			if (src.Width * scale == 0 || src.Height * scale == 0)
			{
				return NULL_IMAGE;
			}
			
			// use the default hints if we weren't given any
			if (hints == null)
				hints = DefaultHints;
			
			if (scale == 1.0 && angle % 90.0 == 0.0)
			{
				// this is an unscaled quadrant rotation, we can do this simply
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_INTERPOLATION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_INTERPOLATION_f'"
				SupportClass.PutElement(hints, RenderingHints.KEY_INTERPOLATION, (System.Object) System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor);
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_ANTIALIASING_f'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_ANTIALIAS_OFF' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_ANTIALIAS_OFF_f'"
				SupportClass.PutElement(hints, RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_OFF);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'ubox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle ubox = getBounds(src);
				//UPGRADE_NOTE: Final was removed from the declaration of 'tbox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				System.Drawing.Rectangle tbox = transform(ref ubox, scale, angle);
				
				// keep opaque destination for orthogonal rotation of an opaque source
				//UPGRADE_NOTE: Final was removed from the declaration of 'trans '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Bitmap trans = createCompatibleImage(tbox.Width, tbox.Height, src.getTransparency() != BufferedImage.OPAQUE);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 't '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Drawing2D.Matrix t = new System.Drawing.Drawing2D.Matrix();
				t.Translate((System.Single) (- tbox.X), (System.Single) (- tbox.Y));
				t.RotateAt((float) (DEGTORAD * angle * (180 / System.Math.PI)), new System.Drawing.PointF((float) (ubox.GetBounds().X + (ubox.GetBounds().Width / 2)), (float) (ubox.GetBounds().Y + (ubox.GetBounds().Height / 2))));
				t.Scale((System.Single) scale, (System.Single) scale);
				t.Translate((System.Single) ubox.X, (System.Single) ubox.Y);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(trans);
				//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHints_javautilMap'"
				g.setRenderingHints(hints);
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.drawImage' was converted to 'System.Drawing.Graphics.DrawImage' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DdrawImage_javaawtImage_javaawtgeomAffineTransform_javaawtimageImageObserver'"
				g.Transform = System.Object.Equals(t, null)?new System.Drawing.Drawing2D.Matrix(1, 0, 0, 1, 0, 0):t;
				g.DrawImage(src, 0, 0);
				g.Dispose();
				return trans;
			}
			else
			{
				if (angle != 0.0)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'ubox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Rectangle ubox = getBounds(src);
					// FIXME: this duplicates the standard scaling case
					// FIXME: check whether AffineTransformOp is faster
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'rbox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					System.Drawing.Rectangle rbox = transform(ref ubox, 1.0, angle);
					
					// keep opaque destination for orthogonal rotation of an opaque source
					//UPGRADE_NOTE: Final was removed from the declaration of 'rot '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Bitmap rot = createCompatibleImage(rbox.Width, rbox.Height, src.getTransparency() != BufferedImage.OPAQUE || angle % 90.0 != 0.0);
					
					// FIXME: rotation via bilinear interpolation probably decreases quality
					//UPGRADE_NOTE: Final was removed from the declaration of 'tx '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Drawing2D.Matrix tx = new System.Drawing.Drawing2D.Matrix();
					tx.Translate((System.Single) (- rbox.X), (System.Single) (- rbox.Y));
					tx.RotateAt((float) (DEGTORAD * angle * (180 / System.Math.PI)), new System.Drawing.PointF((float) (ubox.GetBounds().X + (ubox.GetBounds().Width / 2)), (float) (ubox.GetBounds().Y + (ubox.GetBounds().Height / 2))));
					tx.Translate((System.Single) ubox.X, (System.Single) ubox.Y);
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(rot);
					//UPGRADE_ISSUE: Method 'java.awt.Graphics2D.setRenderingHints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphics2DsetRenderingHints_javautilMap'"
					g.setRenderingHints(hints);
					//UPGRADE_TODO: Method 'java.awt.Graphics2D.drawImage' was converted to 'System.Drawing.Graphics.DrawImage' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DdrawImage_javaawtImage_javaawtgeomAffineTransform_javaawtimageImageObserver'"
					g.Transform = System.Object.Equals(tx, null)?new System.Drawing.Drawing2D.Matrix(1, 0, 0, 1, 0, 0):tx;
					g.DrawImage(src, 0, 0);
					g.Dispose();
					src = rot;
				}
				
				if (scale != 1.0)
				{
					src = coerceToIntType(src);
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'sbox '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Rectangle tempAux = getBounds(src);
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					System.Drawing.Rectangle sbox = transform(ref tempAux, scale, 0.0);
					
					// return null image if scaling makes source vanish
					if (sbox.Width == 0 || sbox.Height == 0)
					{
						return NULL_IMAGE;
					}
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'dst '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
					System.Drawing.Bitmap dst = GeneralFilter.zoom(ref sbox, src, scale > 1.0?upscale:downscale);
					
					return toCompatibleImage(dst);
				}
				else
				{
					return src;
				}
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public static System.Drawing.Bitmap transform(System.Drawing.Bitmap src, double scale, double angle, System.Collections.Hashtable hints, int quality)
		{
			return transform(src, scale, angle, hints);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(fallthrough)
		public static System.Drawing.Bitmap coerceToIntType(System.Drawing.Bitmap img)
		{
			// ensure that img is a type which GeneralFilter can handle
			switch ((int) img.PixelFormat)
			{
				
				case (int) System.Drawing.Imaging.PixelFormat.Format32bppRgb: 
				case (int) System.Drawing.Imaging.PixelFormat.Format32bppArgb: 
				case (int) System.Drawing.Imaging.PixelFormat.Format32bppPArgb: 
				case (int) System.Drawing.Imaging.PixelFormat.Format32bppRgb: 
					return img;
				
				default: 
					return toType(img, img.getTransparency() == BufferedImage.OPAQUE?(int) System.Drawing.Imaging.PixelFormat.Format32bppRgb:(CompatibleTranslucentImageType == (int) System.Drawing.Imaging.PixelFormat.Format32bppArgb?(int) System.Drawing.Imaging.PixelFormat.Format32bppArgb:(int) System.Drawing.Imaging.PixelFormat.Format32bppPArgb));
				
			}
		}
		
		/// <param name="im">
		/// </param>
		/// <returns> the boundaries of this image, where (0,0) is the
		/// pseudo-center of the image
		/// </returns>
		public static System.Drawing.Rectangle getBounds(System.Drawing.Bitmap im)
		{
			return new System.Drawing.Rectangle((- im.Width) / 2, (- im.Height) / 2, im.Width, im.Height);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public static System.Drawing.Rectangle getBounds(ref System.Drawing.Size d)
		{
			return new System.Drawing.Rectangle((- d.Width) / 2, (- d.Height) / 2, d.Width, d.Height);
		}
		
		/// <deprecated> Use {@link #getImageSize(String,InputStream)} instead. 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public static System.Drawing.Size getImageSize(System.IO.Stream in_Renamed)
		{
			return getImageSize("", in_Renamed);
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'tfac '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'tfac' was moved to static method 'VassalSharp.tools.image.ImageUtils'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly TemporaryFileFactory tfac;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'loader '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly ImageLoader loader = new ImageIOImageLoader(new FallbackImageTypeConverter(tfac));
		
		public static System.Drawing.Size getImageSize(System.String name, System.IO.Stream in_Renamed)
		{
			return loader.size(name, in_Renamed);
		}
		
		/// <deprecated> Use {@link #getImage(String,InputStream)} instead. 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public static System.Drawing.Bitmap getImage(System.IO.Stream in_Renamed)
		{
			return getImage("", in_Renamed);
		}
		
		public static System.Drawing.Bitmap getImageResource(System.String name)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'in '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
			System.IO.Stream in_Renamed = typeof(ImageUtils).getResourceAsStream(name);
			if (in_Renamed == null)
				throw new ImageNotFoundException(name);
			return getImage(name, in_Renamed);
		}
		
		public static System.Drawing.Bitmap getImage(System.String name, System.IO.Stream in_Renamed)
		{
			return loader.load(name, in_Renamed, compatOpaqueImageType, compatTranslImageType, true);
		}
		
		public static System.Drawing.Bitmap toType(System.Drawing.Bitmap src, int type)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'dst '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap dst = new System.Drawing.Bitmap(src.Width, src.Height, (System.Drawing.Imaging.PixelFormat) type);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dst);
			//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
			g.DrawImage(src, 0, 0);
			g.Dispose();
			
			return dst;
		}
		
		public static System.Drawing.Image forceLoad(System.Drawing.Image img)
		{
			// ensure that the image is loaded
			return (System.Drawing.Image) img.Clone();
		}
		
		public static bool isTransparent(System.Drawing.Image img)
		{
			// determine whether this image has an alpha channel
			//UPGRADE_NOTE: Final was removed from the declaration of 'pg '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SupportClass.PixelCapturer pg = new SupportClass.PixelCapturer(img, 0, 0, 1, 1, false);
			try
			{
				pg.CapturePixels();
			}
			catch (System.Threading.ThreadInterruptedException e)
			{
				ErrorDialog.bug(e);
			}
			
			return pg.GetColor().A == 0?false:true;
		}
		
		public static bool isTransparent(System.Drawing.Bitmap img)
		{
			return img.getTransparency() != BufferedImage.OPAQUE;
		}
		
		/// <summary> Transform an <code>Image</code> to a <code>BufferedImage</code>.
		/// 
		/// </summary>
		/// <param name="src">the <code>Image</code> to transform
		/// </param>
		public static System.Drawing.Bitmap toBufferedImage(System.Drawing.Image src)
		{
			if (src == null)
				return null;
			if (src is System.Drawing.Bitmap)
				return toCompatibleImage((System.Drawing.Bitmap) src);
			
			// ensure that the image is loaded
			src = forceLoad(src);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'dst '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap dst = createCompatibleImage(src.Width, src.Height, isTransparent(src));
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dst);
			//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
			g.DrawImage(src, 0, 0);
			g.Dispose();
			
			return dst;
		}
		
		protected internal static bool IS_MAC_RETINA;
		
		protected internal static System.Drawing.Bitmap compatOpaqueImage;
		protected internal static System.Drawing.Bitmap compatTransImage;
		
		protected internal static int compatOpaqueImageType;
		protected internal static int compatTranslImageType;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'NULL_IMAGE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly System.Drawing.Bitmap NULL_IMAGE = createCompatibleImage(1, 1);
		
		public static int getCompatibleImageType()
		{
			return compatOpaqueImageType;
		}
		
		public static int getCompatibleImageType(bool transparent)
		{
			return transparent?compatTranslImageType:compatOpaqueImageType;
		}
		
		public static int getCompatibleImageType(System.Drawing.Bitmap img)
		{
			return getCompatibleImageType(isTransparent(img));
		}
		
		public static System.Drawing.Bitmap createCompatibleImage(int w, int h)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'cm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.getColorModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImagegetColorModel'"
			System.Drawing.Color cm = compatOpaqueImage.getColorModel();
			//UPGRADE_NOTE: Final was removed from the declaration of 'wr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'java.awt.image.WritableRaster' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageWritableRaster'"
			//UPGRADE_ISSUE: Method 'java.awt.image.ColorModel.createCompatibleWritableRaster' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageColorModelcreateCompatibleWritableRaster_int_int'"
			System.Drawing.Bitmap wr = cm.createCompatibleWritableRaster(w, h);
			//UPGRADE_ISSUE: Constructor 'java.awt.image.BufferedImage.BufferedImage' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImageBufferedImage_javaawtimageColorModel_javaawtimageWritableRaster_boolean_javautilHashtable'"
			//UPGRADE_ISSUE: Method 'java.awt.image.ColorModel.isAlphaPremultiplied' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageColorModelisAlphaPremultiplied'"
			return new BufferedImage(cm, wr, cm.isAlphaPremultiplied(), null);
		}
		
		public static System.Drawing.Bitmap createCompatibleImage(int w, int h, bool transparent)
		{
			return transparent?createCompatibleTranslucentImage(w, h):createCompatibleImage(w, h);
		}
		
		public static System.Drawing.Bitmap createCompatibleTranslucentImage(int w, int h)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'cm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.getColorModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImagegetColorModel'"
			System.Drawing.Color cm = compatTransImage.getColorModel();
			//UPGRADE_NOTE: Final was removed from the declaration of 'wr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'java.awt.image.WritableRaster' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageWritableRaster'"
			//UPGRADE_ISSUE: Method 'java.awt.image.ColorModel.createCompatibleWritableRaster' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageColorModelcreateCompatibleWritableRaster_int_int'"
			System.Drawing.Bitmap wr = cm.createCompatibleWritableRaster(w, h);
			//UPGRADE_ISSUE: Constructor 'java.awt.image.BufferedImage.BufferedImage' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImageBufferedImage_javaawtimageColorModel_javaawtimageWritableRaster_boolean_javautilHashtable'"
			//UPGRADE_ISSUE: Method 'java.awt.image.ColorModel.isAlphaPremultiplied' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageColorModelisAlphaPremultiplied'"
			return new BufferedImage(cm, wr, cm.isAlphaPremultiplied(), null);
		}
		
		public static System.Drawing.Bitmap toCompatibleImage(System.Drawing.Bitmap src)
		{
			//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.getColorModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImagegetColorModel'"
			if ((src.getColorModel().Equals(compatOpaqueImage.getColorModel()) && src.getTransparency() == compatOpaqueImage.getTransparency()) || (src.getColorModel().Equals(compatTransImage.getColorModel()) && src.getTransparency() == compatTransImage.getTransparency()))
			{
				return src;
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'dst '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap dst = createCompatibleImage(src.Width, src.Height, isTransparent(src));
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dst);
			//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
			g.DrawImage(src, 0, 0);
			g.Dispose();
			
			return dst;
		}
		
		public static bool isCompatibleImage(System.Drawing.Bitmap img)
		{
			return (int) img.PixelFormat == getCompatibleImageType(img.getTransparency() != BufferedImage.OPAQUE);
		}
		
		/*
		* What Image suffixes does Vassal know about?
		* Used by the MassPieceLoader to identify candidate images.
		*/
		public const System.String GIF_SUFFIX = ".gif";
		public const System.String PNG_SUFFIX = ".png";
		public const System.String SVG_SUFFIX = ".svg";
		public const System.String JPG_SUFFIX = ".jpg";
		public const System.String JPEG_SUFFIX = ".jpeg";
		//UPGRADE_NOTE: Final was removed from the declaration of 'IMAGE_SUFFIXES '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly System.String[] IMAGE_SUFFIXES = new System.String[]{GIF_SUFFIX, PNG_SUFFIX, SVG_SUFFIX, JPG_SUFFIX, JPEG_SUFFIX};
		
		public static bool hasImageSuffix(System.String name)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String s = name.ToLower();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String suffix: IMAGE_SUFFIXES)
			{
				if (s.endsWith(suffix))
				{
					return true;
				}
			}
			return false;
		}
		
		public static System.String stripImageSuffix(System.String name)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String s = name.ToLower();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String suffix: IMAGE_SUFFIXES)
			{
				if (s.endsWith(suffix))
				{
					return name.Substring(0, (name.Length - suffix.length()) - (0));
				}
			}
			return name;
		}
		static ImageUtils()
		{
			{
				// Initialise Image prefs prior to Preferences being read.
				
				// set up map for creating default RenderingHints
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_INTERPOLATION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_INTERPOLATION_f'"
				defaultHints.put(RenderingHints.KEY_INTERPOLATION, (System.Object) System.Drawing.Drawing2D.InterpolationMode.Bilinear);
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.KEY_ANTIALIASING' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsKEY_ANTIALIASING_f'"
				//UPGRADE_ISSUE: Field 'java.awt.RenderingHints.VALUE_ANTIALIAS_ON' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtRenderingHintsVALUE_ANTIALIAS_ON_f'"
				defaultHints.put(RenderingHints.KEY_ANTIALIASING, RenderingHints.VALUE_ANTIALIAS_ON);
			}
			tfac = new AnonymousClassTemporaryFileFactory();
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'o '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDesktopProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
				//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
				System.Object o = Toolkit.getDefaultToolkit().getDesktopProperty("apple.awt.contentScaleFactor");
				
				IS_MAC_RETINA = (o is System.ValueType) && System.Convert.ToDouble(((System.ValueType) o)) == 2.0;
			}
			{
				System.Drawing.Bitmap oimg;
				System.Drawing.Bitmap timg;
				
				if (Headless)
				{
					oimg = new System.Drawing.Bitmap(1, 1, (System.Drawing.Imaging.PixelFormat) System.Drawing.Imaging.PixelFormat.Format32bppRgb);
					timg = new System.Drawing.Bitmap(1, 1, (System.Drawing.Imaging.PixelFormat) System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				}
				else
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'gc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_ISSUE: Class 'java.awt.GraphicsConfiguration' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtGraphicsConfiguration'"
					GraphicsConfiguration gc = GraphicsConfiguration;
					
					oimg = gc.createCompatibleImage(1, 1, BufferedImage.OPAQUE);
					timg = gc.createCompatibleImage(1, 1, BufferedImage.TRANSLUCENT);
					
					// Bug workaround: MacOX X machines with Retina displays are incapable
					// of painting TYPE_INT_ARGB_PRE images, despite that these systems
					// return that type as the "compatible" image type.
					if (MacRetina)
					{
						if ((int) oimg.PixelFormat == (int) System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
						{
							oimg = new System.Drawing.Bitmap(1, 1, (System.Drawing.Imaging.PixelFormat) System.Drawing.Imaging.PixelFormat.Format32bppArgb);
						}
						
						if ((int) timg.PixelFormat == (int) System.Drawing.Imaging.PixelFormat.Format32bppPArgb)
						{
							timg = new System.Drawing.Bitmap(1, 1, (System.Drawing.Imaging.PixelFormat) System.Drawing.Imaging.PixelFormat.Format32bppArgb);
						}
					}
				}
				
				compatOpaqueImage = oimg;
				compatTransImage = timg;
				
				compatOpaqueImageType = (int) compatOpaqueImage.PixelFormat;
				compatTranslImageType = (int) compatTransImage.PixelFormat;
			}
		}
	}
}