/*
* Copyright (c) 2007 by Rodney Kinney
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
namespace VassalSharp.tools.imageop
{
	
	/// <summary> Paints an image at arbitrary scale. Uses the ImageOp interfaces to
	/// lazily fetch and cache images.
	/// 
	/// </summary>
	/// <author>  rodneykinney
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class ScaledImagePainter
	{
		virtual public System.String ImageName
		{
			set
			{
				Source = value == null || value.Trim().Length == 0?null:Op.load(value);
			}
			
		}
		virtual public System.Drawing.Size ImageSize
		{
			get
			{
				return srcOp == null?new System.Drawing.Size(0, 0):srcOp.getSize();
			}
			
		}
		virtual public ImageOp Source
		{
			get
			{
				return srcOp;
			}
			
			set
			{
				this.srcOp = value;
				scaleOp = null;
			}
			
		}
		protected internal ImageOp srcOp;
		protected internal ScaleOp scaleOp;
		
		//UPGRADE_ISSUE: Interface 'java.awt.image.ImageObserver' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageObserver'"
		public virtual void  draw(System.Drawing.Graphics g, int x, int y, double scale, ImageObserver obs)
		{
			if (srcOp == null || scale <= 0)
				return ;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'img '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap img;
			if (scale == 1.0)
			{
				img = srcOp.getImage();
			}
			else
			{
				if (scaleOp == null || scaleOp.Scale != scale)
				{
					scaleOp = Op.scale(srcOp, scale);
				}
				img = scaleOp.getImage();
			}
			
			if (img == null)
				return ;
			
			//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
			g.DrawImage(img, x, y);
		}
	}
}