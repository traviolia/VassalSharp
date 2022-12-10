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
//UPGRADE_TODO: The type 'org.apache.commons.lang.builder.HashCodeBuilder' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using HashCodeBuilder = org.apache.commons.lang.builder.HashCodeBuilder;
using GeneralFilter = VassalSharp.tools.image.GeneralFilter;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
namespace VassalSharp.tools.imageop
{
	
	/// <summary> An {@link ImageOp} which scales its source.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class ScaleOpBitmapImpl:AbstractTiledOpImpl, ScaleOp
	{
		private void  InitBlock()
		{
			return Collections < VassalSharp.tools.opcache.Op;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< ? >> singletonList(sop);
		}
		virtual public System.Collections.Hashtable Hints
		{
			get
			{
				return hints;
			}
			
		}
		/// <summary> Returns the scale factor.
		/// 
		/// </summary>
		/// <returns> the scale factor, in the range <code>(0,Double.MAX_VALUE]</code>.
		/// </returns>
		virtual public double Scale
		{
			get
			{
				return scale;
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'sop '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal ImageOp sop;
		//UPGRADE_NOTE: Final was removed from the declaration of 'scale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal double scale;
		//UPGRADE_NOTE: Final was removed from the declaration of 'hints '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.Collections.Hashtable hints;
		//UPGRADE_NOTE: Final was removed from the declaration of 'hash '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int hash;
		
		// FIXME: We try to always use the same hints object because hints is
		// used in our equals() and RenderingHints.equals() is ridiculously slow
		// if a full comparison is made. This way hints == defaultHints, usually,
		// and so a quick equality comparison succeeds.
		//UPGRADE_NOTE: Final was removed from the declaration of 'defaultHints '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'defaultHints' was moved to static method 'VassalSharp.tools.imageop.ScaleOpBitmapImpl'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.Collections.Hashtable defaultHints;
		
		/// <summary> Constructs an <code>ImageOp</code> which will scale
		/// the image produced by its source <code>ImageOp</code>.
		/// 
		/// </summary>
		/// <param name="sop">the source operation
		/// </param>
		/// <param name="scale">the scale factor
		/// </param>
		public ScaleOpBitmapImpl(ImageOp sop, double scale):this(sop, scale, defaultHints)
		{
		}
		
		/// <summary> Constructs an <code>ImageOp</code> which will scale
		/// the image produced by its source <code>ImageOp</code>.
		/// 
		/// </summary>
		/// <param name="sop">the source operation
		/// </param>
		/// <param name="scale">the scale factor
		/// </param>
		/// <param name="hints">rendering hints
		/// </param>
		public ScaleOpBitmapImpl(ImageOp sop, double scale, System.Collections.Hashtable hints)
		{
			InitBlock();
			if (sop == null)
				throw new System.ArgumentException("Attempt to scale null image");
			if (scale <= 0)
				throw new System.ArgumentException("Cannot scale image at " + scale);
			
			this.sop = sop;
			this.scale = scale;
			this.hints = hints;
			
			hash = new HashCodeBuilder().append(sop).append(scale).append(hints).toHashCode();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < VassalSharp.tools.opcache.Op < ? >> getSources()
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <throws>  Exception passed up from the source <code>ImageOp</code>. </throws>
		public override System.Drawing.Bitmap eval()
		{
			return ImageUtils.transform(sop.getImage(null), scale, 0.0, hints);
		}
		
		/// <summary>{@inheritDoc} </summary>
		protected internal override void  fixSize()
		{
			if ((size = SizeFromCache).IsEmpty)
			{
				System.Drawing.Rectangle tempAux = new Rectangle(sop.getSize());
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				size = ImageUtils.transform(ref tempAux, scale, 0.0).Size;
			}
		}
		
		protected internal override ImageOp createTileOp(int tileX, int tileY)
		{
			return new TileOp(this, tileX, tileY);
		}
		
		private class TileOp:AbstractTileOpImpl
		{
			private void  InitBlock()
			{
				return Collections < VassalSharp.tools.opcache.Op;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				< ? >> singletonList(sop);
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'sop '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private ImageOp sop;
			//UPGRADE_NOTE: Final was removed from the declaration of 'dx0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: Final was removed from the declaration of 'dy0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: Final was removed from the declaration of 'dw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: Final was removed from the declaration of 'dh '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int dx0;
			private int dy0;
			private int dw;
			private int dh;
			//UPGRADE_NOTE: Final was removed from the declaration of 'scale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private double scale;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			SuppressWarnings(unused)
			//UPGRADE_NOTE: Final was removed from the declaration of 'hints '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private System.Collections.Hashtable hints;
			//UPGRADE_NOTE: Final was removed from the declaration of 'hash '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int hash;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'downFilter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private static readonly GeneralFilter.Filter downFilter = new GeneralFilter.Lanczos3Filter();
			//UPGRADE_NOTE: Final was removed from the declaration of 'upFilter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private static readonly GeneralFilter.Filter upFilter = new GeneralFilter.MitchellFilter();
			
			public TileOp(ScaleOpBitmapImpl rop, int tileX, int tileY)
			{
				if (rop == null)
					throw new System.ArgumentException();
				
				if (tileX < 0 || tileX >= rop.NumXTiles || tileY < 0 || tileY >= rop.NumYTiles)
					throw new System.IndexOutOfRangeException();
				
				sop = rop.sop;
				
				scale = rop.Scale;
				hints = rop.Hints;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'sr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle sr = new System.Drawing.Rectangle(0, 0, (int) (sop.getWidth() * scale), (int) (sop.getHeight() * scale));
				
				dx0 = tileX * rop.TileWidth;
				dy0 = tileY * rop.TileHeight;
				dw = System.Math.Min(rop.TileWidth, sr.Width - dx0);
				dh = System.Math.Min(rop.TileHeight, sr.Height - dy0);
				
				size = new System.Drawing.Size(dw, dh);
				
				hash = new HashCodeBuilder().append(sop).append(dx0).append(dy0).append(dw).append(dh).toHashCode();
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public List < VassalSharp.tools.opcache.Op < ? >> getSources()
			
			public override System.Drawing.Bitmap eval()
			{
				if (dw < 1 || dh < 1)
					return ImageUtils.NULL_IMAGE;
				
				// ensure that src is a type which GeneralFilter can handle
				//UPGRADE_NOTE: Final was removed from the declaration of 'src '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Bitmap src = ImageUtils.coerceToIntType(sop.getImage(null));
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'sr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle sr = new System.Drawing.Rectangle(0, 0, (int) (sop.getWidth() * scale), (int) (sop.getHeight() * scale));
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'dstR '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Class 'java.awt.image.WritableRaster' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageWritableRaster'"
				//UPGRADE_TODO: Method 'java.awt.image.WritableRaster.createWritableTranslatedChild' was converted to 'System.Drawing.Bitmap.Clone' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageWritableRastercreateWritableTranslatedChild_int_int'"
				//UPGRADE_ISSUE: Method 'java.awt.image.ColorModel.createCompatibleWritableRaster' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageColorModelcreateCompatibleWritableRaster_int_int'"
				//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.getColorModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImagegetColorModel'"
				System.Drawing.Bitmap dstR = (System.Drawing.Bitmap) src.getColorModel().createCompatibleWritableRaster(dw, dh).Clone();
				// zoom! zoom!
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				GeneralFilter.zoom(dstR, ref sr, src, scale < 1.0f?downFilter:upFilter);
				
				//UPGRADE_ISSUE: Constructor 'java.awt.image.BufferedImage.BufferedImage' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImageBufferedImage_javaawtimageColorModel_javaawtimageWritableRaster_boolean_javautilHashtable'"
				//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.getColorModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImagegetColorModel'"
				//UPGRADE_TODO: Method 'java.awt.image.WritableRaster.createWritableTranslatedChild' was converted to 'System.Drawing.Bitmap.Clone' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageWritableRastercreateWritableTranslatedChild_int_int'"
				//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.isAlphaPremultiplied' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImageisAlphaPremultiplied'"
				return ImageUtils.toCompatibleImage(new BufferedImage(src.getColorModel(), (System.Drawing.Bitmap) dstR.Clone(), src.isAlphaPremultiplied(), null));
			}
			
			protected internal override void  fixSize()
			{
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public  override bool Equals(System.Object o)
			{
				if (this == o)
					return true;
				if (o == null || o.GetType() != this.GetType())
					return false;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'op '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				TileOp op = (TileOp) o;
				return dx0 == op.dx0 && dy0 == op.dy0 && dw == op.dw && dh == op.dh && scale == op.scale && sop.equals(op.sop);
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public override int GetHashCode()
			{
				return hash;
			}
			
			/// <summary>{@inheritDoc} </summary>
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public override System.String ToString()
			{
				return GetType().FullName + "[sop=" + sop + ",scale=" + scale + ",dx0=" + dx0 + ",dy0=" + dy0 + ",dw=" + dw + ",dy=" + dh + "]";
			}
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public  override bool Equals(System.Object o)
		{
			if (this == o)
				return true;
			if (o == null || o.GetType() != this.GetType())
				return false;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'op '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ScaleOpBitmapImpl op = (ScaleOpBitmapImpl) o;
			return scale == op.scale && sop.equals(op.sop) && hints.Equals(op.hints);
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override int GetHashCode()
		{
			return hash;
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.String ToString()
		{
			return GetType().FullName + "[sop=" + sop + ",scale=" + scale + ",hints=" + hints + "]";
		}
		static ScaleOpBitmapImpl()
		{
			defaultHints = ImageUtils.DefaultHints;
		}
	}
}