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
namespace VassalSharp.tools.image
{
	
	/*
	This class is the result of much trial and error with using timings
	and a profiler. Most things which are odd are that way because
	they're faster. Please profile any changes to ensure that they do not
	ruin our hard-won performance.
	
	Profiling information:
	
	java -Xmx1024M -cp classes VassalSharp.tools.image.GeneralFilterTest cc.png 0.406 0
	java -Xmx1024M -cp classes VassalSharp.tools.image.GeneralFilterTest cc.png 0.406 1
	java -Xmx1024M -cp classes VassalSharp.tools.image.GeneralFilterTest cc.png 0.406 2
	
	cc.png is a 3100x2500 32-bit image.
	
	RGBA PRE RGB
	32000               original version, not comitted
	1261         r32   precalculate xcontrib, ycontrib; large work[]
	1171         r33   precalculate ycontrib only, single-column work[]
	848         r34   check for constant-color areas
	727         r41   double -> float
	678         r45   copy scr part from src to Raster (not BufferedImage)
	659         r55   let zoom() create destination BufferedImage
	777         r65   downsampling narrow, short images fixed; upsampling fixed
	699         r2494 split cases for with/without alpha channel
	518         r5516 removed useless checks, removed useless CList field
	529 532 466 r5894 added case for premultiplied ARGB
	525 597 468 r5896 correctly premultiply, scale, unpremultiply for ARGB
	535 483 408 r7416 don't copy src rectangle, use src data directly
	531 476 412 r7417 moved common expressions out of loops*/
	
	/// <summary> GeneralFilter is a pluggable image resampler.
	/// 
	/// <p><code>GeneralFilter</code> up- or down-samples images or parts of
	/// images using any one of the various filters contained in it as internal
	/// classes.</p>
	/// 
	/// <p><code>GeneralFilter</code> is based on <code>filter_rcg.c</code>, which
	/// contains modifications made by Ray Gardener to <code>filter.c</code>,
	/// originally by Dale Schumacher. <code>filter.c</code> appeared in
	/// 
	/// <blockquote>Dale Schumacher. "General Filtered Image Rescaling".
	/// <em>Graphics Gems III</em>. David Kirk, ed. Academic Press. 1994.
	/// pp. 8&ndash;16, 414&ndash;424.</blockquote>
	/// 
	/// and the source for <code>filter.c</code> and <code>filter_rcg.c</code>
	/// are available
	/// <a href="http://tog.acm.org/GraphicsGems/gems.html#gemsiii">here</a>.
	/// Both <code>filter.c</code> and <code>filter_rcg.c</code> are in the
	/// Public Domain.</p>
	/// 
	/// <p>The filters provided here are intended for scaling, though other filters
	/// could be created which resample in other ways.</p>
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	public sealed class GeneralFilter
	{
		/// <summary>Do not instantiate this class. </summary>
		private GeneralFilter()
		{
		}
		
		private sealed class CList
		{
			public int n; // number of source pixels
			public int pixel; // starting source pixel
			public float[] weight; // source pixel weights
		}
		
		/// <summary>The abstract base class for filters. </summary>
		public abstract class Filter
		{
			public abstract float SamplingRadius{get;}
			public abstract float apply(float t);
		}
		
		/// <summary>A Hermite filter. </summary>
		public sealed class HermiteFilter:Filter
		{
			override public float SamplingRadius
			{
				get
				{
					return 1.0f;
				}
				
			}
			public override float apply(float t)
			{
				// f(t) = 2|t|^3 - 3|t|^2 + 1, -1 <= t <= 1
				if (t < 0.0f)
					t = - t;
				if (t < 1.0f)
					return (2.0f * t - 3.0f) * t * t + 1.0f;
				return 0.0f;
			}
		}
		
		/// <summary>A box filter. </summary>
		public sealed class BoxFilter:Filter
		{
			override public float SamplingRadius
			{
				get
				{
					return 0.5f;
				}
				
			}
			public override float apply(float t)
			{
				if (t > - 0.5f && t <= 0.5f)
					return 1.0f;
				return 0.0f;
			}
		}
		
		/// <summary>A triangle, or bilinear, filter. </summary>
		public sealed class TriangleFilter
		{
			public float SamplingRadius
			{
				get
				{
					return 1.0f;
				}
				
			}
			public float apply(float t)
			{
				if (t < 0.0f)
					t = - t;
				if (t < 1.0f)
					return 1.0f - t;
				return 0.0f;
			}
		}
		
		/// <summary>A Lanczos filter with radius 3. </summary>
		public sealed class Lanczos3Filter:Filter
		{
			override public float SamplingRadius
			{
				get
				{
					return 3.0f;
				}
				
			}
			private float sinc(float t)
			{
				if (t == 0.0f)
					return 1.0f;
				t = (float) (t * System.Math.PI);
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				return (float) (System.Math.Sin(t) / t);
			}
			
			public override float apply(float t)
			{
				if (t < - 3.0f)
					return 0.0f;
				if (t < 0.0f)
					return sinc(- t) * sinc((- t) / 3.0f);
				if (t < 3.0f)
					return sinc(t) * sinc(t / 3.0f);
				return 0.0f;
			}
		}
		
		/// <summary>A Mitchell filter. </summary>
		public sealed class MitchellFilter:Filter
		{
			override public float SamplingRadius
			{
				get
				{
					return 2.0f;
				}
				
			}
			private const float B = 1.0f / 3.0f;
			private const float C = 1.0f / 3.0f;
			//UPGRADE_NOTE: Final was removed from the declaration of 'P0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: The initialization of  'P0' was moved to static method 'VassalSharp.tools.image.GeneralFilter.MitchellFilter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private static readonly float P0;
			//UPGRADE_NOTE: Final was removed from the declaration of 'P2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: The initialization of  'P2' was moved to static method 'VassalSharp.tools.image.GeneralFilter.MitchellFilter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private static readonly float P2;
			//UPGRADE_NOTE: Final was removed from the declaration of 'P3 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: The initialization of  'P3' was moved to static method 'VassalSharp.tools.image.GeneralFilter.MitchellFilter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private static readonly float P3;
			//UPGRADE_NOTE: Final was removed from the declaration of 'Q0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: The initialization of  'Q0' was moved to static method 'VassalSharp.tools.image.GeneralFilter.MitchellFilter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private static readonly float Q0;
			//UPGRADE_NOTE: Final was removed from the declaration of 'Q1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: The initialization of  'Q1' was moved to static method 'VassalSharp.tools.image.GeneralFilter.MitchellFilter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private static readonly float Q1;
			//UPGRADE_NOTE: Final was removed from the declaration of 'Q2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: The initialization of  'Q2' was moved to static method 'VassalSharp.tools.image.GeneralFilter.MitchellFilter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private static readonly float Q2;
			//UPGRADE_NOTE: Final was removed from the declaration of 'Q3 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: The initialization of  'Q3' was moved to static method 'VassalSharp.tools.image.GeneralFilter.MitchellFilter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			private static readonly float Q3;
			
			public override float apply(float t)
			{
				if (t < - 2.0f)
					return 0.0f;
				if (t < - 1.0f)
					return Q0 - t * (Q1 - t * (Q2 - t * Q3));
				if (t < 0.0f)
					return P0 + t * t * (P2 - t * P3);
				if (t < 1.0f)
					return P0 + t * t * (P2 + t * P3);
				if (t < 2.0f)
					return Q0 + t * (Q1 + t * (Q2 + t * Q3));
				return 0.0f;
			}
			static MitchellFilter()
			{
				P0 = (6.0f - 2.0f * B) / 6.0f;
				P2 = (- 18.0f + 12.0f * B + 6.0f * C) / 6.0f;
				P3 = (12.0f - 9.0f * B - 6.0f * C) / 6.0f;
				Q0 = (8.0f * B + 24.0f * C) / 6.0f;
				Q1 = ((- 12.0f) * B - 48.0f * C) / 6.0f;
				Q2 = (6.0f * B + 30.0f * C) / 6.0f;
				Q3 = ((- 1.0f) * B - 6.0f * C) / 6.0f;
			}
		}
		
		/// <summary>A Bell filter. </summary>
		public sealed class BellFilter:Filter
		{
			override public float SamplingRadius
			{
				get
				{
					return 1.5f;
				}
				
			}
			public override float apply(float t)
			{
				// box (*) box (*) box
				if (t < 0.0f)
					t = - t;
				if (t < 0.5f)
					return 0.75f - (t * t);
				if (t < 1.5f)
				{
					t -= 1.5f;
					return 0.5f * (t * t);
				}
				return 0.0f;
			}
		}
		
		/// <summary>A B-spline filter. </summary>
		public sealed class BSplineFilter:Filter
		{
			override public float SamplingRadius
			{
				get
				{
					return 2.0f;
				}
				
			}
			public override float apply(float t)
			{
				// box (*) box (*) box (*) box
				
				if (t < 0.0f)
					t = - t;
				
				if (t < 1.0f)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'tt '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					float tt = t * t;
					return (0.5f * tt * t) - tt + (2.0f / 3.0f);
				}
				else if (t < 2.0f)
				{
					t = 2.0f - t;
					return (1.0f / 6.0f) * (t * t * t);
				}
				
				return 0.0f;
			}
		}
		
		/// <summary> Filters the entire source image.
		/// 
		/// This is a convenience function which calls
		/// {@link zoom(WritableRaster, Rectangle, BufferedImage, Filter)},
		/// setting the destination rectangle as the bounds of the destination
		/// tile.
		/// 
		/// </summary>
		/// <param name="dst">the destination rectangle
		/// </param>
		/// <param name="src">the soure image
		/// </param>
		/// <param name="filter">the filter to apply
		/// </param>
		/// <throws>  ClassCastException if <code>src</code> does not store its data </throws>
		/// <summary> in a {@link DataBufferInt}
		/// </summary>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public static System.Drawing.Bitmap zoom(ref System.Drawing.Rectangle dst, System.Drawing.Bitmap src, Filter filter)
		{
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'dstR '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'java.awt.image.WritableRaster' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageWritableRaster'"
			//UPGRADE_ISSUE: Method 'java.awt.image.ColorModel.createCompatibleWritableRaster' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageColorModelcreateCompatibleWritableRaster_int_int'"
			//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.getColorModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImagegetColorModel'"
			System.Drawing.Bitmap dstR = src.getColorModel().createCompatibleWritableRaster(dst.Width, dst.Height);
			System.Drawing.Rectangle tempAux = System.Drawing.Rectangle.Truncate(SupportClass.RasterBoundsSupport(dstR));
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			zoom(dstR, ref tempAux, src, filter);
			
			// FIXME: check whether this affects hardware acceleration
			//UPGRADE_ISSUE: Constructor 'java.awt.image.BufferedImage.BufferedImage' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImageBufferedImage_javaawtimageColorModel_javaawtimageWritableRaster_boolean_javautilHashtable'"
			//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.getColorModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImagegetColorModel'"
			//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.isAlphaPremultiplied' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImageisAlphaPremultiplied'"
			return new BufferedImage(src.getColorModel(), dstR, src.isAlphaPremultiplied(), null);
		}
		
		/// <summary> Filters a portion of the source image.
		/// 
		/// </summary>
		/// <param name="dstR">the destination tile to calculate
		/// </param>
		/// <param name="dst_fr">the bounds of the whole destination image
		/// </param>
		/// <param name="srcI">the source image
		/// </param>
		/// <param name="filter">the filter to apply
		/// </param>
		/// <throws>  ClassCastException if <code>srcI</code> does not store its data </throws>
		/// <summary> in a {@link DataBufferInt}
		/// </summary>
		//UPGRADE_TODO: Class 'java.awt.image.WritableRaster' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageWritableRaster'"
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public static void  zoom(System.Drawing.Bitmap dstR, ref System.Drawing.Rectangle dst_fr, System.Drawing.Bitmap srcI, Filter filter)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'dst_data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.MemoryStream tempDataBuffer;
			tempDataBuffer = new System.IO.MemoryStream();
			//UPGRADE_TODO: Method 'java.awt.image.Raster.getDataBuffer' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageRastergetDataBuffer'"
			dstR.Save(tempDataBuffer, System.Drawing.Imaging.ImageFormat.Bmp);
			int[] dst_data = (int[]) ((System.Collections.ArrayList) new System.Collections.ArrayList(tempDataBuffer.ToArray()))[0];
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'src_type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int src_type;
			if (srcI.getTransparency() == BufferedImage.OPAQUE)
			{
				src_type = OPAQUE;
			}
			else
			{
				//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.isAlphaPremultiplied' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImageisAlphaPremultiplied'"
				if (srcI.isAlphaPremultiplied())
				{
					src_type = TRANS_PREMULT;
				}
				else
				{
					src_type = TRANS_UNPREMULT;
				}
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'dx0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int dx0 = System.Drawing.Rectangle.Truncate(SupportClass.RasterBoundsSupport(dstR)).X;
			//UPGRADE_NOTE: Final was removed from the declaration of 'dy0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int dy0 = System.Drawing.Rectangle.Truncate(SupportClass.RasterBoundsSupport(dstR)).Y;
			//UPGRADE_NOTE: Final was removed from the declaration of 'dx1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int dx1 = dx0 + dstR.Width - 1;
			//UPGRADE_NOTE: Final was removed from the declaration of 'dy1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int dy1 = dy0 + dstR.Height - 1;
			//UPGRADE_NOTE: Final was removed from the declaration of 'dw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int dw = dstR.Width;
			//UPGRADE_NOTE: Final was removed from the declaration of 'dh '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int dh = dstR.Height;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'dstWidth '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int dstWidth = dst_fr.Width;
			//UPGRADE_NOTE: Final was removed from the declaration of 'dstHeight '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int dstHeight = dst_fr.Height;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'srcWidth '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int srcWidth = srcI.Width;
			//UPGRADE_NOTE: Final was removed from the declaration of 'srcHeight '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int srcHeight = srcI.Height;
			
			// We want dstX0 * xscale = srcX0, except when that would make
			// xscale = 0; similarly for yscale.
			//UPGRADE_NOTE: Final was removed from the declaration of 'xscale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			float xscale = srcWidth == 1?dstWidth:(float) (dstWidth - 1) / (srcWidth - 1);
			//UPGRADE_NOTE: Final was removed from the declaration of 'yscale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			float yscale = srcHeight == 1?dstHeight:(float) (dstHeight - 1) / (srcHeight - 1);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'fwidth '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			float fwidth = filter.SamplingRadius;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'sx0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int sx0 = System.Math.Max(0, (int) System.Math.Floor((dx0 - fwidth) / xscale));
			//UPGRADE_NOTE: Final was removed from the declaration of 'sy0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int sy0 = System.Math.Max(0, (int) System.Math.Floor((dy0 - fwidth) / yscale));
			//UPGRADE_NOTE: Final was removed from the declaration of 'sx1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int sx1 = System.Math.Min(srcWidth - 1, (int) System.Math.Ceiling((dx1 + fwidth) / xscale));
			//UPGRADE_NOTE: Final was removed from the declaration of 'sy1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int sy1 = System.Math.Min(srcHeight - 1, (int) System.Math.Ceiling((dy1 + fwidth) / yscale));
			//UPGRADE_NOTE: Final was removed from the declaration of 'sw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int sw = sx1 - sx0 + 1;
			//UPGRADE_NOTE: Final was removed from the declaration of 'sh '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int sh = sy1 - sy0 + 1;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'src_data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.MemoryStream tempDataBuffer2;
			tempDataBuffer2 = new System.IO.MemoryStream();
			//UPGRADE_TODO: Method 'java.awt.image.Raster.getDataBuffer' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageRastergetDataBuffer'"
			srcI.Save(tempDataBuffer2, System.Drawing.Imaging.ImageFormat.Bmp);
			int[] src_data = (int[]) ((System.Collections.ArrayList) new System.Collections.ArrayList(tempDataBuffer2.ToArray()))[0];
			
			resample(src_data, false, sx0, sy0, sx1, sy1, sw, sh, src_type, srcWidth, srcHeight, dst_data, dx0, dy0, dx1, dy1, dw, dh, dstWidth, dstHeight, xscale, yscale, filter);
		}
		
		public const int OPAQUE = 0;
		public const int TRANS_PREMULT = 1;
		public const int TRANS_UNPREMULT = 2;
		
		public static void  resample(int[] src_data, bool src_data_consecutive, int sx0, int sy0, int sx1, int sy1, int sw, int sh, int src_type, int srcWidth, int srcHeight, int[] dst_data, int dx0, int dy0, int dx1, int dy1, int dw, int dh, int dstWidth, int dstHeight, float xscale, float yscale, Filter filter)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'work '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int[] work = new int[sh];
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'fwidth '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			float fwidth = filter.SamplingRadius;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'ycontrib '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			CList[] ycontrib = calc_contrib(dh, fwidth, yscale, dy0, sy0, sh, filter);
			//UPGRADE_NOTE: Final was removed from the declaration of 'xcontrib '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			CList[] xcontrib = calc_contrib(dw, fwidth, xscale, dx0, sx0, sw, filter);
			
			// apply the filter
			switch (src_type)
			{
				
				case OPAQUE: 
					// handle TYPE_INT_RGB, TYPE_INT_BGR
					if (src_data_consecutive)
					{
						for (int dx = 0; dx < dw; ++dx)
						{
							apply_h_opaque(0, 0, sh, sw, xcontrib[dx], src_data, work);
							apply_v_opaque(dh, ycontrib, work, dst_data, dx, dw);
						}
					}
					else
					{
						for (int dx = 0; dx < dw; ++dx)
						{
							apply_h_opaque(sx0, sy0, sh, srcWidth, xcontrib[dx], src_data, work);
							apply_v_opaque(dh, ycontrib, work, dst_data, dx, dw);
						}
					}
					break;
				
				case TRANS_PREMULT: 
					// handle TYPE_INT_ARGB_PRE
					if (src_data_consecutive)
					{
						for (int dx = 0; dx < dw; ++dx)
						{
							apply_h(0, 0, sh, sw, xcontrib[dx], src_data, work);
							apply_v(dh, ycontrib, work, dst_data, dx, dw);
						}
					}
					else
					{
						for (int dx = 0; dx < dw; ++dx)
						{
							apply_h(sx0, sy0, sh, srcWidth, xcontrib[dx], src_data, work);
							apply_v(dh, ycontrib, work, dst_data, dx, dw);
						}
					}
					break;
				
				case TRANS_UNPREMULT: 
					// handle TYPE_INT_ARGB
					
					// premultiply (copy of) source data
					//UPGRADE_NOTE: Final was removed from the declaration of 'pre_src_data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int[] pre_src_data = new int[src_data.Length];
					for (int i = 0; i < src_data.Length; ++i)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'unpre '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						int unpre = src_data[i];
						//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						int a = (SupportClass.URShift(unpre, 24)) & 0xff;
						
						if (a == 255)
						{
							pre_src_data[i] = unpre;
						}
						else
						{
							//UPGRADE_NOTE: Final was removed from the declaration of 'na '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							float na = a / 255.0f;
							
							//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
							pre_src_data[i] = a << 24 | ((int) (((SupportClass.URShift(unpre, 16)) & 0xff) * na + 0.5f)) << 16 | ((int) (((SupportClass.URShift(unpre, 8)) & 0xff) * na + 0.5f)) << 8 | ((int) (((unpre) & 0xff) * na + 0.5f));
						}
					}
					
					if (src_data_consecutive)
					{
						for (int dx = 0; dx < dw; ++dx)
						{
							apply_h(0, 0, sh, sw, xcontrib[dx], pre_src_data, work);
							apply_v(dh, ycontrib, work, dst_data, dx, dw);
						}
					}
					else
					{
						for (int dx = 0; dx < dw; ++dx)
						{
							apply_h(sx0, sy0, sh, srcWidth, xcontrib[dx], pre_src_data, work);
							apply_v(dh, ycontrib, work, dst_data, dx, dw);
						}
					}
					
					// unpremultiply destination data
					for (int i = 0; i < dst_data.Length; ++i)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'pre '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						int pre = dst_data[i];
						//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						int a = (SupportClass.URShift(pre, 24)) & 0xff;
						
						if (a == 255)
						{
							continue;
						}
						else
						{
							//UPGRADE_NOTE: Final was removed from the declaration of 'inv_na '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							float inv_na = 255.0f / a;
							
							//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
							dst_data[i] = a << 24 | ((int) (((SupportClass.URShift(pre, 16)) & 0xff) * inv_na + 0.5f)) << 16 | ((int) (((SupportClass.URShift(pre, 8)) & 0xff) * inv_na + 0.5f)) << 8 | ((int) (((pre) & 0xff) * inv_na + 0.5f));
						}
					}
					break;
				
				default: 
					throw new System.ArgumentException();
				
			}
		}
		
		private static CList[] calc_contrib(int dl, float fwidth, float scale, int d0, int s0, int sl, Filter filter)
		{
			// Calculate filter contributions for each destination strip
			//UPGRADE_NOTE: Final was removed from the declaration of 'contrib '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			CList[] contrib = new CList[dl];
			for (int i = 0; i < contrib.Length; i++)
				contrib[i] = new CList();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'blur '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			float blur = 1.0f;
			//UPGRADE_NOTE: Final was removed from the declaration of 'kscale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			float kscale = 1.0f / (blur * System.Math.Max(1.0f / scale, 1.0f));
			//UPGRADE_NOTE: Final was removed from the declaration of 'width '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			float width = fwidth / kscale;
			
			for (int i = 0; i < dl; i++)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'center '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				float center = (i + d0 + 0.5f) / scale;
				//UPGRADE_NOTE: Final was removed from the declaration of 'start '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				int start = (int) System.Math.Max(center - width + 0.5f, s0);
				//UPGRADE_NOTE: Final was removed from the declaration of 'stop '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				int stop = (int) System.Math.Min(center + width + 0.5f, s0 + sl);
				//UPGRADE_NOTE: Final was removed from the declaration of 'numContrib '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int numContrib = stop - start;
				
				contrib[i].n = numContrib;
				contrib[i].pixel = start - s0;
				contrib[i].weight = new float[numContrib];
				
				float density = 0.0f;
				for (int n = 0; n < numContrib; n++)
				{
					density += (contrib[i].weight[n] = filter.apply(kscale * (start + n - center + 0.5f)));
				}
				
				if (density != 0.0f && density != 1.0f)
				{
					for (int j = 0; j < numContrib; j++)
					{
						contrib[i].weight[j] /= density;
					}
				}
			}
			
			return contrib;
		}
		
		private static void  apply_h(int sx0, int sy0, int sh, int stride, CList xcontrib, int[] src, int[] work)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			CList c = xcontrib;
			//UPGRADE_NOTE: Final was removed from the declaration of 'max '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int max = c.n;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'base '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int base_Renamed = sx0 + c.pixel + sy0 * stride;
			
			// Apply pre-computed filter to sample horizontally from src to work
			for (int k = 0; k < sh; k++)
			{
				float s_a = 0.0f; // alpha sample
				float s_r = 0.0f; // red sample
				float s_g = 0.0f; // green sample
				float s_b = 0.0f; // blue sample
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'pos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int pos = base_Renamed + k * stride;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'pel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int pel = src[pos];
				bool bPelDelta = false;
				
				// Check for areas of constant color. It is *much* faster to
				// to check first and then calculate weights only if needed.
				for (int j = 0; j < max; j++)
				{
					if (c.weight[j] == 0.0f)
						continue;
					if (src[pos + j] != pel)
					{
						bPelDelta = true; break;
					}
				}
				
				if (bPelDelta)
				{
					// There is a color change from 0 to max; we need to use weights.
					for (int j = 0; j < max; j++)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						float w = c.weight[j];
						//UPGRADE_NOTE: Final was removed from the declaration of 'sd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						int sd = src[pos + j];
						
						s_a += ((SupportClass.URShift(sd, 24)) & 0xff) * w;
						s_r += ((SupportClass.URShift(sd, 16)) & 0xff) * w;
						s_g += ((SupportClass.URShift(sd, 8)) & 0xff) * w;
						s_b += ((sd) & 0xff) * w;
					}
					
					// Ugly, but fast.
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					work[k] = (s_a > 255?255:(s_a < 0?0:(int) (s_a + 0.5f))) << 24 | (s_r > 255?255:(s_r < 0?0:(int) (s_r + 0.5f))) << 16 | (s_g > 255?255:(s_g < 0?0:(int) (s_g + 0.5f))) << 8 | (s_b > 255?255:(s_b < 0?0:(int) (s_b + 0.5f)));
				}
				else
				{
					// If there's no color change from 0 to max, maintain that.
					work[k] = pel;
				}
			}
		}
		
		private static void  apply_h_opaque(int sx0, int sy0, int sh, int stride, CList xcontrib, int[] src, int[] work)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			CList c = xcontrib;
			//UPGRADE_NOTE: Final was removed from the declaration of 'max '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int max = c.n;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'base '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int base_Renamed = sx0 + c.pixel + sy0 * stride;
			
			// Apply pre-computed filter to sample horizontally from src to work
			for (int k = 0; k < sh; k++)
			{
				float s_r = 0.0f; // red sample
				float s_g = 0.0f; // green sample
				float s_b = 0.0f; // blue sample
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'pos '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int pos = base_Renamed + k * stride;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'pel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int pel = src[pos];
				bool bPelDelta = false;
				
				// Check for areas of constant color. It is *much* faster to
				// to check first and then calculate weights only if needed.
				for (int j = 0; j < max; j++)
				{
					if (c.weight[j] == 0.0f)
						continue;
					if (src[pos + j] != pel)
					{
						bPelDelta = true; break;
					}
				}
				
				if (bPelDelta)
				{
					// There is a color change from 0 to max; we need to use weights.
					for (int j = 0; j < max; j++)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						float w = c.weight[j];
						//UPGRADE_NOTE: Final was removed from the declaration of 'sd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						int sd = src[pos + j];
						
						s_r += ((SupportClass.URShift(sd, 16)) & 0xff) * w;
						s_g += ((SupportClass.URShift(sd, 8)) & 0xff) * w;
						s_b += ((sd) & 0xff) * w;
					}
					
					// Ugly, but fast.
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					work[k] = (s_r > 255?255:(s_r < 0?0:(int) (s_r + 0.5f))) << 16 | (s_g > 255?255:(s_g < 0?0:(int) (s_g + 0.5f))) << 8 | (s_b > 255?255:(s_b < 0?0:(int) (s_b + 0.5f)));
				}
				else
				{
					// If there's no color change from 0 to max, maintain that.
					work[k] = pel;
				}
			}
		}
		
		private static void  apply_v(int dh, CList[] ycontrib, int[] work, int[] dst, int dx, int dw)
		{
			// Apply pre-computed filter to sample vertically from work to dst
			for (int i = 0; i < dh; i++)
			{
				float s_a = 0.0f; // alpha sample
				float s_r = 0.0f; // red sample
				float s_g = 0.0f; // green sample
				float s_b = 0.0f; // blue sample
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				CList c = ycontrib[i];
				//UPGRADE_NOTE: Final was removed from the declaration of 'max '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int max = c.n;
				//UPGRADE_NOTE: Final was removed from the declaration of 'pel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int pel = work[c.pixel];
				bool bPelDelta = false;
				
				// Check for areas of constant color. It is *much* faster to
				// to check first and then calculate weights only if needed.
				for (int j = 0; j < max; j++)
				{
					if (c.weight[j] == 0.0f)
						continue;
					if (work[c.pixel + j] != pel)
					{
						bPelDelta = true; break;
					}
				}
				
				if (bPelDelta)
				{
					// There is a color change from 0 to max; we need to use weights.
					for (int j = 0; j < max; j++)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						float w = c.weight[j];
						//UPGRADE_NOTE: Final was removed from the declaration of 'wd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						int wd = work[c.pixel + j];
						
						s_a += ((SupportClass.URShift(wd, 24)) & 0xff) * w;
						s_r += ((SupportClass.URShift(wd, 16)) & 0xff) * w;
						s_g += ((SupportClass.URShift(wd, 8)) & 0xff) * w;
						s_b += ((wd) & 0xff) * w;
					}
					
					// working in premultiplied domain, must clamp R,G,B to A
					//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					int a = s_a > 255?255:(s_a < 0?0:(int) (s_a + 0.5f));
					
					// Ugly, but fast.
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					dst[dx + i * dw] = a << 24 | (s_r > a?a:(s_r < 0?0:(int) (s_r + 0.5f))) << 16 | (s_g > a?a:(s_g < 0?0:(int) (s_g + 0.5f))) << 8 | (s_b > a?a:(s_b < 0?0:(int) (s_b + 0.5f)));
				}
				else
				{
					// If there's no color change from 0 to max, maintain that.
					
					// working in premultiplied domain, must clamp R,G,B to A
					//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int a = (SupportClass.URShift(pel, 24)) & 0xff;
					//UPGRADE_NOTE: Final was removed from the declaration of 'r '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int r = (SupportClass.URShift(pel, 16)) & 0xff;
					//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int g = (SupportClass.URShift(pel, 8)) & 0xff;
					//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int b = (pel) & 0xff;
					
					dst[dx + i * dw] = a << 24 | (r > a?a:r) << 16 | (g > a?a:g) << 8 | (b > a?a:b);
				}
			}
		}
		
		private static void  apply_v_opaque(int dh, CList[] ycontrib, int[] work, int[] dst, int dx, int dw)
		{
			// Apply pre-computed filter to sample vertically from work to dst
			for (int i = 0; i < dh; i++)
			{
				float s_r = 0.0f; // red sample
				float s_g = 0.0f; // green sample
				float s_b = 0.0f; // blue sample
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				CList c = ycontrib[i];
				//UPGRADE_NOTE: Final was removed from the declaration of 'max '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int max = c.n;
				//UPGRADE_NOTE: Final was removed from the declaration of 'pel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int pel = work[c.pixel];
				bool bPelDelta = false;
				
				// Check for areas of constant color. It is *much* faster to
				// to check first and then calculate weights only if needed.
				for (int j = 0; j < max; j++)
				{
					if (c.weight[j] == 0.0f)
						continue;
					if (work[c.pixel + j] != pel)
					{
						bPelDelta = true; break;
					}
				}
				
				if (bPelDelta)
				{
					// There is a color change from 0 to max; we need to use weights.
					for (int j = 0; j < max; j++)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						float w = c.weight[j];
						//UPGRADE_NOTE: Final was removed from the declaration of 'wd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						int wd = work[c.pixel + j];
						
						s_r += ((SupportClass.URShift(wd, 16)) & 0xff) * w;
						s_g += ((SupportClass.URShift(wd, 8)) & 0xff) * w;
						s_b += ((wd) & 0xff) * w;
					}
					
					// Ugly, but fast.
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					dst[dx + i * dw] = (s_r > 255?255:(s_r < 0?0:(int) (s_r + 0.5f))) << 16 | (s_g > 255?255:(s_g < 0?0:(int) (s_g + 0.5f))) << 8 | (s_b > 255?255:(s_b < 0?0:(int) (s_b + 0.5f)));
				}
				else
				{
					// If there's no color change from 0 to max, maintain that.
					dst[dx + i * dw] = pel;
				}
			}
		}
	}
}