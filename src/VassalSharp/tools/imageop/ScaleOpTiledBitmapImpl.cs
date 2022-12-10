/*
* $Id$
*
* Copyright (c) 2009-2010 by Joel Uckelman
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
	
	
	/// <summary> An {@link ImageOp} which scales its source and cobbles scaled tiles
	/// from the tile cache.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class ScaleOpTiledBitmapImpl:ScaleOpBitmapImpl
	{
		
		public ScaleOpTiledBitmapImpl(ImageOp sop, double scale):this(sop, scale, defaultHints)
		{
		}
		
		public ScaleOpTiledBitmapImpl(ImageOp sop, double scale, System.Collections.Hashtable hints):base(sop, scale, hints)
		{
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override ImageOp createTileOp(int tileX, int tileY)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'iscale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			double iscale = 1.0 / scale;
			//UPGRADE_NOTE: Final was removed from the declaration of 'invPow2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			bool invPow2 = System.Math.Floor(iscale) == iscale && (((int) iscale) & (((int) iscale) - 1)) == 0;
			
			if (invPow2 && sop is SourceOp)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String name = ((SourceOp) sop).Name;
				return new SourceOpDiskCacheBitmapImpl(name, tileX, tileY, scale);
			}
			else
			{
				return new TileOp(this, tileX, tileY);
			}
		}
		
		new private class TileOp:AbstractTileOpImpl
		{
			private void  InitBlock()
			{
				return Arrays < VassalSharp.tools.opcache.Op;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				< ? >> asList(sop);
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'rsop '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private ImageOp rsop;
			//UPGRADE_NOTE: Final was removed from the declaration of 'sop '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private ImageOp[] sop;
			//UPGRADE_NOTE: Final was removed from the declaration of 'dx0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: Final was removed from the declaration of 'dy0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: Final was removed from the declaration of 'dx1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: Final was removed from the declaration of 'dy1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: Final was removed from the declaration of 'dw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: Final was removed from the declaration of 'dh '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int dx0;
			private int dy0;
			private int dx1;
			private int dy1;
			private int dw;
			private int dh;
			//UPGRADE_NOTE: Final was removed from the declaration of 'sx0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: Final was removed from the declaration of 'sy0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: Final was removed from the declaration of 'sx1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: Final was removed from the declaration of 'sy1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: Final was removed from the declaration of 'sw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: Final was removed from the declaration of 'sh '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int sx0;
			private int sy0;
			private int sx1;
			private int sy1;
			private int sw;
			private int sh;
			//UPGRADE_NOTE: Final was removed from the declaration of 'dd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private System.Drawing.Size dd;
			//UPGRADE_NOTE: Final was removed from the declaration of 'sd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private System.Drawing.Size sd;
			//UPGRADE_NOTE: Final was removed from the declaration of 'scale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private double scale;
			//UPGRADE_NOTE: Final was removed from the declaration of 'xscale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_NOTE: Final was removed from the declaration of 'yscale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private float xscale;
			private float yscale;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'tx '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int tx;
			//UPGRADE_NOTE: Final was removed from the declaration of 'ty '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int ty;
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
			
			public TileOp(ScaleOpTiledBitmapImpl rop, int tileX, int tileY)
			{
				if (rop == null)
					throw new System.ArgumentException();
				
				if (tileX < 0 || tileX >= rop.NumXTiles || tileY < 0 || tileY >= rop.NumYTiles)
					throw new System.IndexOutOfRangeException();
				
				tx = tileX;
				ty = tileY;
				
				scale = rop.Scale;
				hints = rop.Hints;
				
				// destination sizes and coordinates
				dd = rop.Size;
				
				dx0 = tileX * rop.TileWidth;
				dy0 = tileY * rop.TileHeight;
				dw = System.Math.Min(rop.TileWidth, dd.Width - dx0);
				dh = System.Math.Min(rop.TileHeight, dd.Height - dy0);
				
				dx1 = dx0 + dw - 1;
				dy1 = dy0 + dh - 1;
				
				size = new System.Drawing.Size(dw, dh);
				
				if (scale >= 1.0)
				{
					// we are upscaling
					rsop = rop.sop;
				}
				else
				{
					// cobble this tile from pyramid tiles at the lub size
					//UPGRADE_NOTE: Final was removed from the declaration of 'nscale '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					double nscale = 1.0 / (1 << (int) System.Math.Floor(System.Math.Log(1.0 / scale) / System.Math.Log(2)));
					
					rsop = new ScaleOpTiledBitmapImpl(rop.sop, nscale);
				}
				
				sd = rsop.getSize();
				
				// We want dx0 * xscale = sx0, unless that makes xscale = 0.
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				xscale = sd.Width == 1?dd.Width:(float) (dd.Width - 1) / (sd.Width - 1);
				
				// We want dy0 * yscale = sy0, unless that makes yscale = 0.
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				yscale = sd.Height == 1?dd.Height:(float) (dd.Height - 1) / (sd.Height - 1);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'fw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				float fw = scale < 1.0f?downFilter.getSamplingRadius():upFilter.getSamplingRadius();
				
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				sx0 = System.Math.Max(0, (int) System.Math.Floor((dx0 - fw) / xscale));
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				sy0 = System.Math.Max(0, (int) System.Math.Floor((dy0 - fw) / yscale));
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				sx1 = System.Math.Min(sd.Width - 1, (int) System.Math.Ceiling((dx1 + fw) / xscale));
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				sy1 = System.Math.Min(sd.Height - 1, (int) System.Math.Ceiling((dy1 + fw) / yscale));
				sw = sx1 - sx0 + 1;
				sh = sy1 - sy0 + 1;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'sr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Rectangle sr = new System.Drawing.Rectangle(sx0, sy0, sw, sh);
				//UPGRADE_NOTE: Final was removed from the declaration of 'stiles '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point[] stiles = rsop.getTileIndices(sr);
				
				sop = new ImageOp[stiles.Length];
				for (int i = 0; i < stiles.Length; ++i)
				{
					sop[i] = rsop.getTileOp(stiles[i]);
				}
				
				hash = new HashCodeBuilder().append(sop).append(dx0).append(dy0).append(dw).append(dh).append(tx).append(ty).toHashCode();
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public List < VassalSharp.tools.opcache.Op < ? >> getSources()
			
			public override System.Drawing.Bitmap eval()
			{
				if (dw < 1 || dh < 1)
					return ImageUtils.NULL_IMAGE;
				
				// cobble
				//UPGRADE_NOTE: Final was removed from the declaration of 'tiles '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Point[] tiles = rsop.getTileIndices(new System.Drawing.Rectangle(sx0, sy0, sw, sh));
				//UPGRADE_NOTE: Final was removed from the declaration of 'tw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int tw = rsop.getTileWidth();
				//UPGRADE_NOTE: Final was removed from the declaration of 'th '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int th = rsop.getTileHeight();
				
				System.Drawing.Bitmap src;
				//UPGRADE_NOTE: Final was removed from the declaration of 'src_trans '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				bool src_trans = ImageUtils.isTransparent(rsop.getTile(tiles[0], null));
				
				// make sure we have an int-type image
				// and match the transparency of the first tile
				switch (ImageUtils.getCompatibleImageType(rsop.getTile(tiles[0], null)))
				{
					
					case (int) System.Drawing.Imaging.PixelFormat.Format32bppRgb: 
					case (int) System.Drawing.Imaging.PixelFormat.Format32bppArgb: 
					case (int) System.Drawing.Imaging.PixelFormat.Format32bppPArgb: 
					case (int) System.Drawing.Imaging.PixelFormat.Format32bppRgb: 
						src = ImageUtils.createCompatibleImage(sw, sh, src_trans);
						break;
					
					default: 
						src = new System.Drawing.Bitmap(sw, sh, (System.Drawing.Imaging.PixelFormat) (src_trans?(int) System.Drawing.Imaging.PixelFormat.Format32bppPArgb:(int) System.Drawing.Imaging.PixelFormat.Format32bppRgb));
						break;
					
				}
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(src);
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Point tile: tiles)
				{
					g.drawImage(rsop.getTile(tile, null), tile.x * tw - sx0, tile.y * th - sy0, null);
				}
				
				g.Dispose();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'src_data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.IO.MemoryStream tempDataBuffer;
				tempDataBuffer = new System.IO.MemoryStream();
				//UPGRADE_TODO: Method 'java.awt.image.Raster.getDataBuffer' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageRastergetDataBuffer'"
				src.Save(tempDataBuffer, System.Drawing.Imaging.ImageFormat.Bmp);
				int[] src_data = (int[]) ((System.Collections.ArrayList) new System.Collections.ArrayList(tempDataBuffer.ToArray()))[0];
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'dstR '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Class 'java.awt.image.WritableRaster' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageWritableRaster'"
				//UPGRADE_ISSUE: Method 'java.awt.image.ColorModel.createCompatibleWritableRaster' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageColorModelcreateCompatibleWritableRaster_int_int'"
				//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.getColorModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImagegetColorModel'"
				System.Drawing.Bitmap dstR = src.getColorModel().createCompatibleWritableRaster(dw, dh);
				//UPGRADE_NOTE: Final was removed from the declaration of 'dst_data '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.IO.MemoryStream tempDataBuffer2;
				tempDataBuffer2 = new System.IO.MemoryStream();
				//UPGRADE_TODO: Method 'java.awt.image.Raster.getDataBuffer' was converted to 'System.Drawing.Bitmap' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageRastergetDataBuffer'"
				dstR.Save(tempDataBuffer2, System.Drawing.Imaging.ImageFormat.Bmp);
				int[] dst_data = (int[]) ((System.Collections.ArrayList) new System.Collections.ArrayList(tempDataBuffer2.ToArray()))[0];
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'src_type '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int src_type;
				if (!src_trans)
				{
					src_type = GeneralFilter.OPAQUE;
				}
				else
				{
					//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.isAlphaPremultiplied' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImageisAlphaPremultiplied'"
					if (src.isAlphaPremultiplied())
					{
						src_type = GeneralFilter.TRANS_PREMULT;
					}
					else
					{
						src_type = GeneralFilter.TRANS_UNPREMULT;
					}
				}
				
				GeneralFilter.resample(src_data, true, sx0, sy0, sx1, sy1, sw, sh, src_type, sd.Width, sd.Height, dst_data, dx0, dy0, dx1, dy1, dw, dh, dd.Width, dd.Height, xscale, yscale, scale < 1.0f?downFilter:upFilter);
				
				//UPGRADE_ISSUE: Constructor 'java.awt.image.BufferedImage.BufferedImage' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImageBufferedImage_javaawtimageColorModel_javaawtimageWritableRaster_boolean_javautilHashtable'"
				//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.getColorModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImagegetColorModel'"
				//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.isAlphaPremultiplied' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImageisAlphaPremultiplied'"
				return ImageUtils.toCompatibleImage(new BufferedImage(src.getColorModel(), dstR, src.isAlphaPremultiplied(), null));
			}
			
			protected internal override void  fixSize()
			{
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
				TileOp op = (TileOp) o;
				return dx0 == op.dx0 && dy0 == op.dy0 && dw == op.dw && dh == op.dh && tx == op.tx && ty == op.ty && scale == op.scale && Arrays.equals(sop, op.sop);
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
				return GetType().FullName + "[sop=" + Arrays.toString(sop) + ",scale=" + scale + ",dx0=" + dx0 + ",dy0=" + dy0 + ",dw=" + dw + ",dy=" + dh + "]";
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
			ScaleOpTiledBitmapImpl op = (ScaleOpTiledBitmapImpl) o;
			return scale == op.scale && sop.equals(op.sop) && hints.Equals(op.hints);
		}
		//UPGRADE_NOTE: The following method implementation was automatically added to preserve functionality. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1306'"
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}