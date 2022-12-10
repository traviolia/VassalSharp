/*
* $Id$
*
* Copyright (c) 2004 by Ben Smith
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
namespace VassalSharp.tools
{
	
	/// <summary> Extension of java.awt.image.AreaAveragingScaleFilter.  Uses the
	/// same algorithm but makes sure all images are scaled using area
	/// averaging.  Ensures there is no fallback to ReplicateScaleFilter. 
	/// </summary>
	//UPGRADE_TODO: The class 'Size' is marked as Sealed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1147'"
	public class ImprovedAveragingScaleFilter:System.Drawing.Size
	{
		private int savedWidth, savedHeight;
		private int[] savedPixels;
		private static System.Drawing.Color defaultCM = SupportClass.ColorSupport.GetRGBDefault();
		
		public ImprovedAveragingScaleFilter(int savedWidth, int savedHeight, int destWidth, int destHeight):base(destWidth, destHeight)
		{
			this.savedWidth = savedWidth;
			this.savedHeight = savedHeight;
			this.Width = destWidth;
			this.Height = destHeight;
			savedPixels = new int[savedWidth * savedHeight];
		}
		
		//UPGRADE_NOTE: The equivalent of method 'java.awt.image.ImageFilter.setColorModel' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public void  setColorModel(ref System.Drawing.Color model)
		{
			// Change color model to model you are generating
			//UPGRADE_ISSUE: Method 'java.awt.image.ImageConsumer.setColorModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
			consumer.setColorModel(defaultCM);
		}
		
		//UPGRADE_NOTE: The equivalent of method 'java.awt.image.AreaAveragingScaleFilter.setHints' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  setHints(int hintflags)
		{
			//UPGRADE_ISSUE: Method 'java.awt.image.ImageConsumer.setHints' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
			//UPGRADE_ISSUE: Field 'java.awt.image.ImageConsumer.TOPDOWNLEFTRIGHT' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
			//UPGRADE_ISSUE: Field 'java.awt.image.ImageConsumer.COMPLETESCANLINES' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
			//UPGRADE_ISSUE: Field 'java.awt.image.ImageConsumer.SINGLEPASS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
			//UPGRADE_ISSUE: Field 'java.awt.image.ImageConsumer.SINGLEFRAME' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
			consumer.setHints(TOPDOWNLEFTRIGHT | COMPLETESCANLINES | SINGLEPASS | (hintflags & SINGLEFRAME));
		}
		
		//UPGRADE_NOTE: The equivalent of method 'java.awt.image.AreaAveragingScaleFilter.setPixels' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public void  setPixels(int x, int y, int width, int height, ref System.Drawing.Color cm, sbyte[] pixels, int offset, int scansize)
		{
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			setThePixels(x, y, width, height, ref cm, pixels, offset, scansize);
		}
		
		//UPGRADE_NOTE: The equivalent of method 'java.awt.image.AreaAveragingScaleFilter.setPixels' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public void  setPixels(int x, int y, int width, int height, ref System.Drawing.Color cm, int[] pixels, int offset, int scansize)
		{
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			setThePixels(x, y, width, height, ref cm, pixels, offset, scansize);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		private void  setThePixels(int x, int y, int width, int height, ref System.Drawing.Color cm, System.Object pixels, int offset, int scansize)
		{
			
			int sourceOffset = offset;
			int destinationOffset = y * savedWidth + x;
			bool bytearray = (pixels is sbyte[]);
			for (int yy = 0; yy < height; yy++)
			{
				for (int xx = 0; xx < width; xx++)
					if (bytearray)
						savedPixels[destinationOffset++] = SupportClass.ColorSupport.GetRGBFromColor(((sbyte[]) pixels)[sourceOffset++] & 0xff);
					else
						savedPixels[destinationOffset++] = SupportClass.ColorSupport.GetRGBFromColor(((int[]) pixels)[sourceOffset++]);
				sourceOffset += (scansize - width);
				destinationOffset += (savedWidth - width);
			}
		}
		
		//UPGRADE_NOTE: The equivalent of method 'java.awt.image.ImageFilter.imageComplete' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  imageComplete(int status)
		{
			//UPGRADE_ISSUE: Field 'java.awt.image.ImageConsumer.IMAGEABORTED' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
			//UPGRADE_ISSUE: Field 'java.awt.image.ImageConsumer.IMAGEERROR' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
			if ((status == IMAGEABORTED) || (status == IMAGEERROR))
			{
				//UPGRADE_ISSUE: Method 'java.awt.image.ImageConsumer.imageComplete' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
				consumer.imageComplete(status);
				return ;
			}
			else
			{
				// get orig image width and height
				int[] pixels = new int[savedWidth];
				int position;
				for (int yy = 0; yy < savedHeight; yy++)
				{
					position = 0;
					int start = yy * savedWidth;
					for (int xx = 0; xx < savedWidth; xx++)
					{
						pixels[position++] = savedPixels[start + xx];
					}
					//UPGRADE_ISSUE: Method 'java.awt.image.AreaAveragingScaleFilter.setPixels' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageAreaAveragingScaleFiltersetPixels_int_int_int_int_javaawtimageColorModel_int[]_int_int'"
					base.setPixels(0, yy, savedWidth, 1, defaultCM, pixels, 0, savedWidth);
				}
				//UPGRADE_ISSUE: Method 'java.awt.image.ImageConsumer.imageComplete' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
				consumer.imageComplete(status);
			}
		}
	}
}