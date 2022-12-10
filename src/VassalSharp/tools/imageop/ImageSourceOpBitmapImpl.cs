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
//UPGRADE_TODO: The type 'VassalSharp.tools.opcache.Op' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Op = VassalSharp.tools.opcache.Op;
namespace VassalSharp.tools.imageop
{
	
	/// <summary> An {@link ImageOp} which uses a fixed image as its source.
	/// <code>ImageSourceOp</code> holds a reference to the <code>Image</code>
	/// it was constructed with in order to prevent it from being garbage
	/// collected from the soft cache, since this op has no way of recreating
	/// the source image.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class ImageSourceOpBitmapImpl:AbstractTiledOpImpl, SourceOp
	{
		private void  InitBlock()
		{
			return Collections.emptyList();
		}
		virtual public System.String Name
		{
			get
			{
				return null;
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'image '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Drawing.Bitmap image;
		//UPGRADE_NOTE: Final was removed from the declaration of 'hash '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int hash;
		
		/// <summary> Constructs an <code>ImageOp</code> which will hold and return
		/// the image it is constructed with.
		/// 
		/// </summary>
		/// <param name="image">the source image
		/// </param>
		/// <throws>  IllegalArgumentException if <code>image == null</code>. </throws>
		public ImageSourceOpBitmapImpl(System.Drawing.Bitmap image)
		{
			InitBlock();
			if (image == null)
				throw new System.ArgumentException();
			this.image = image;
			hash = image.GetHashCode();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < Op < ? >> getSources()
		
		/// <summary>{@inheritDoc} </summary>
		public override System.Drawing.Bitmap eval()
		{
			return image;
		}
		
		/// <summary>{@inheritDoc} </summary>
		protected internal override void  fixSize()
		{
			//UPGRADE_TODO: Method 'java.awt.image.BufferedImage.getWidth' was converted to 'System.Drawing.Bitmap.Width' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageBufferedImagegetWidth_javaawtimageImageObserver'"
			//UPGRADE_TODO: Method 'java.awt.image.BufferedImage.getHeight' was converted to 'System.Drawing.Bitmap.Height' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtimageBufferedImagegetHeight_javaawtimageImageObserver'"
			size = new System.Drawing.Size(image.Width, image.Height);
		}
		
		protected internal override ImageOp createTileOp(int tileX, int tileY)
		{
			return new SourceTileOpBitmapImpl(this, tileX, tileY);
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
			return image.Equals(((ImageSourceOpBitmapImpl) o).image);
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