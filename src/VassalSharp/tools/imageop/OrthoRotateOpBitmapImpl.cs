/*
* $Id$
*
* Copyright (c) 2007-2008 by Joel Uckelman
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
	
	public class OrthoRotateOpBitmapImpl:AbstractTiledOpImpl, RotateOp
	{
		private void  InitBlock()
		{
			return Collections < VassalSharp.tools.opcache.Op;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< ? >> singletonList(sop);
		}
		virtual public double Angle
		{
			get
			{
				return angle * 90;
			}
			
		}
		virtual public System.Collections.Hashtable Hints
		{
			get
			{
				//    return ImageUtils.getDefaultHints();
				return null;
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'sop '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private ImageOp sop;
		//UPGRADE_NOTE: Final was removed from the declaration of 'angle '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int angle;
		//UPGRADE_NOTE: Final was removed from the declaration of 'hash '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int hash;
		
		public OrthoRotateOpBitmapImpl(ImageOp sop, int angle)
		{
			InitBlock();
			if (sop == null)
				throw new System.ArgumentException();
			
			angle = (360 + (angle % 360)) % 360; // put angle in [0,360)
			if (angle % 90 != 0)
				throw new System.ArgumentException();
			
			// angle is now in { 0, 90, 180, 270 }.
			
			this.sop = sop;
			this.angle = angle / 90;
			
			hash = new HashCodeBuilder().append(sop).append(angle).toHashCode();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < VassalSharp.tools.opcache.Op < ? >> getSources()
		
		public override System.Drawing.Bitmap eval()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'src '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap src = sop.getImage(null);
			if (size.IsEmpty)
				fixSize();
			
			// remain opaque if our parent image is
			//UPGRADE_NOTE: Final was removed from the declaration of 'dst '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap dst = ImageUtils.createCompatibleImage(size.Width, size.Height, src.getTransparency() != BufferedImage.OPAQUE);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dst);
			//UPGRADE_TODO: Method 'java.awt.Graphics2D.rotate' was converted to 'System.Drawing.Graphics.RotateTransform' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Drotate_double_double_double'"
			g.TranslateTransform((float) (src.Width / 2.0), (float) (src.Height / 2.0));
			g.RotateTransform((float) SupportClass.RadiansToDegrees(System.Math.PI / 2.0 * angle), System.Drawing.Drawing2D.MatrixOrder.Append);
			//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
			g.DrawImage(src, 0, 0);
			g.Dispose();
			
			return dst;
		}
		
		protected internal override void  fixSize()
		{
			if ((size = SizeFromCache).IsEmpty)
			{
				size = sop.getSize();
				
				// transpose dimensions for 90- and 270-degree rotations
				if (angle == 1 || angle == 3)
					size = new System.Drawing.Size(size.Height, size.Width);
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
			//UPGRADE_NOTE: Final was removed from the declaration of 'angle '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int angle;
			//UPGRADE_NOTE: Final was removed from the declaration of 'hash '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private int hash;
			
			public TileOp(OrthoRotateOpBitmapImpl rop, int tileX, int tileY)
			{
				if (rop == null)
					throw new System.ArgumentException();
				
				if (tileX < 0 || tileX >= rop.NumXTiles || tileY < 0 || tileY >= rop.NumYTiles)
					throw new System.IndexOutOfRangeException();
				
				this.angle = rop.angle;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'sx0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_NOTE: Final was removed from the declaration of 'sy0 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_NOTE: Final was removed from the declaration of 'sx1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_NOTE: Final was removed from the declaration of 'sy1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int sx0;
				int sy0;
				int sx1;
				int sy1;
				
				switch (angle)
				{
					
					case 0: 
						sx0 = tileX * rop.tileSize.Width;
						sy0 = tileY * rop.tileSize.Height;
						sx1 = System.Math.Min((tileX + 1) * rop.tileSize.Width, rop.size.Width);
						sy1 = System.Math.Min((tileY + 1) * rop.tileSize.Height, rop.size.Height);
						break;
					
					case 1: 
						sx0 = tileY * rop.tileSize.Height;
						sy0 = tileX * rop.tileSize.Width;
						sx1 = System.Math.Min((tileY + 1) * rop.tileSize.Height, rop.size.Height);
						sy1 = System.Math.Min((tileX + 1) * rop.tileSize.Width, rop.size.Width);
						break;
					
					case 2: 
						sx1 = rop.size.Width - tileX * rop.tileSize.Width;
						sy1 = rop.size.Height - tileY * rop.tileSize.Height;
						sx0 = rop.size.Width - System.Math.Min((tileX + 1) * rop.tileSize.Width, rop.size.Width);
						sy0 = rop.size.Height - System.Math.Min((tileY + 1) * rop.tileSize.Height, rop.size.Height);
						break;
					
					case 3: 
					default: 
						sx1 = rop.size.Height - tileY * rop.tileSize.Height;
						sy1 = rop.size.Width - tileX * rop.tileSize.Width;
						sx0 = rop.size.Height - System.Math.Min((tileY + 1) * rop.tileSize.Height, rop.size.Height);
						sy0 = rop.size.Width - System.Math.Min((tileX + 1) * rop.tileSize.Width, rop.size.Width);
						break;
					}
				
				size = new System.Drawing.Size(sx1 - sx0, sy1 - sy0);
				
				sop = new CropOpBitmapImpl(rop.sop, sx0, sy0, sx1, sy1);
				
				hash = new HashCodeBuilder().append(sop).append(angle).toHashCode();
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public List < VassalSharp.tools.opcache.Op < ? >> getSources()
			
			public override System.Drawing.Bitmap eval()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'src '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Bitmap src = sop.getImage(null);
				
				// remain opaque if our parent image is
				//UPGRADE_NOTE: Final was removed from the declaration of 'dst '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Bitmap dst = ImageUtils.createCompatibleImage(size.Width, size.Height, src.getTransparency() != BufferedImage.OPAQUE);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(dst);
				//UPGRADE_TODO: Method 'java.awt.Graphics2D.rotate' was converted to 'System.Drawing.Graphics.RotateTransform' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2Drotate_double_double_double'"
				g.TranslateTransform((float) (src.Width / 2.0), (float) (src.Height / 2.0));
				g.RotateTransform((float) SupportClass.RadiansToDegrees(System.Math.PI / 2.0 * angle), System.Drawing.Drawing2D.MatrixOrder.Append);
				//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				g.DrawImage(src, 0, 0);
				g.Dispose();
				
				return dst;
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
				return angle == op.angle && sop.equals(op.sop);
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public override int GetHashCode()
			{
				return hash;
			}
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
			OrthoRotateOpBitmapImpl op = (OrthoRotateOpBitmapImpl) o;
			return angle == op.Angle && sop.equals(op.sop);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override int GetHashCode()
		{
			return hash;
		}
	}
}