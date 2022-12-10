/*
* $Id$
*
* Copyright (c) 2007 by Joel Uckelman
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
using ImageUtils = VassalSharp.tools.image.ImageUtils;
namespace VassalSharp.tools.imageop
{
	
	/// <summary> An {@link ImageOp} which crops its source.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class CropOpBitmapImpl:AbstractTiledOpImpl, CropOp
	{
		private void  InitBlock()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tiles '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Point[] tiles = sop.getTileIndices(new System.Drawing.Rectangle(x0, y0, x1 - x0, y1 - y0));
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < VassalSharp.tools.opcache.Op < ? >> ops = 
			new ArrayList < VassalSharp.tools.opcache.Op < ? >>(tiles.length);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Point tile: tiles) ops.add(sop.getTileOp(tile));
			
			return ops;
		}
		/// <summary> Returns the crop rectangle.
		/// 
		/// </summary>
		/// <returns> the rectangle to be cropped.
		/// </returns>
		virtual public System.Drawing.Rectangle Rect
		{
			get
			{
				return new System.Drawing.Rectangle(x0, y0, size.Width, size.Height);
			}
			
		}
		virtual public int X0
		{
			get
			{
				return x0;
			}
			
		}
		virtual public int Y0
		{
			get
			{
				return y0;
			}
			
		}
		virtual public int X1
		{
			get
			{
				return x1;
			}
			
		}
		virtual public int Y1
		{
			get
			{
				return y1;
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'sop '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ImageOp sop;
		//UPGRADE_NOTE: Final was removed from the declaration of 'x0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: Final was removed from the declaration of 'y0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: Final was removed from the declaration of 'x1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: Final was removed from the declaration of 'y1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int x0;
		private int y0;
		private int x1;
		private int y1;
		//UPGRADE_NOTE: Final was removed from the declaration of 'hash '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int hash;
		
		/// <summary> Constructs an <code>ImageOp</code> which will crop the image
		/// produced by its source <code>ImageOp</code>.
		/// 
		/// </summary>
		/// <param name="sop">the source operation
		/// </param>
		/// <param name="x0">the x coordinate of the upper-left corner
		/// </param>
		/// <param name="y0">the y coordinate of the upper-left corner
		/// </param>
		/// <param name="x1">the x coordinate of the lower-right corner
		/// </param>
		/// <param name="y1">the y coordinate of the lower-right corner
		/// </param>
		public CropOpBitmapImpl(ImageOp sop, int x0, int y0, int x1, int y1)
		{
			InitBlock();
			if (sop == null)
			{
				throw new System.ArgumentException();
			}
			
			if (x0 < 0)
			{
				throw new System.ArgumentException("left = " + x0);
			}
			
			if (y0 < 0)
			{
				throw new System.ArgumentException("top = " + y0);
			}
			
			if (x1 <= x0)
			{
				throw new System.ArgumentException("left = " + x0 + ", right = " + x1);
			}
			
			if (y1 <= y0)
			{
				throw new System.ArgumentException("top = " + y0 + ", bottom = " + y1);
			}
			
			this.sop = sop;
			this.x0 = x0;
			this.y0 = y0;
			this.x1 = x1;
			this.y1 = y1;
			
			size = new System.Drawing.Size(x1 - x0, y1 - y0);
			
			hash = new HashCodeBuilder().append(sop).append(x0).append(y0).append(x1).append(y1).toHashCode();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < VassalSharp.tools.opcache.Op < ? >> getSources()
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <throws>  Exception passed up from the source <code>ImageOp</code>. </throws>
		/// <summary> 
		/// </summary>
		public override System.Drawing.Bitmap eval()
		{
			// cobble source from tiles
			//UPGRADE_NOTE: Final was removed from the declaration of 'tiles '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Point[] tiles = sop.getTileIndices(new System.Drawing.Rectangle(x0, y0, x1 - x0, y1 - y0));
			//UPGRADE_NOTE: Final was removed from the declaration of 'tw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int tw = sop.getTileWidth();
			//UPGRADE_NOTE: Final was removed from the declaration of 'th '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int th = sop.getTileHeight();
			
			// match the transparency of the first tile
			//UPGRADE_NOTE: Final was removed from the declaration of 'dst '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap dst = ImageUtils.createCompatibleImage(size.Width, size.Height, sop.getTile(tiles[0], null).getTransparency() != BufferedImage.OPAQUE);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dst);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Point tile: tiles)
			{
				g.drawImage(sop.getTile(tile, null), tile.x * tw - x0, tile.y * th - y0, null);
			}
			
			g.Dispose();
			
			return dst;
		}
		
		protected internal override void  fixSize()
		{
		}
		
		protected internal override ImageOp createTileOp(int tileX, int tileY)
		{
			return new CropOpBitmapImpl(this, tileX * tileSize.Width, tileY * tileSize.Height, System.Math.Min((tileX + 1) * tileSize.Width, size.Width), System.Math.Min((tileY + 1) * tileSize.Height, size.Height));
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
			CropOpBitmapImpl op = (CropOpBitmapImpl) o;
			return x0 == op.X0 && y0 == op.Y0 && x1 == op.X1 && y1 == op.Y1 && sop.equals(op.sop);
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override int GetHashCode()
		{
			return hash;
		}
	}
}