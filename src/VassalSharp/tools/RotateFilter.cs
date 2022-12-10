/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
	
	/// <deprecated> Use {@link VassalSharp.tools.imageop.RotateOp} instead. 
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class RotateFilter:System.Object
	{
		[Serializable]
		private class AnonymousClassJFrame:System.Windows.Forms.Form
		{
			public AnonymousClassJFrame(System.Drawing.Image rotated, System.Drawing.Image unrotated)
			{
				InitBlock(rotated, unrotated);
			}
			private void  InitBlock(System.Drawing.Image rotated, System.Drawing.Image unrotated)
			{
				this.rotated = rotated;
				this.unrotated = unrotated;
			}
			//UPGRADE_NOTE: Final variable rotated was copied into class AnonymousClassJFrame. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Drawing.Image rotated;
			//UPGRADE_NOTE: Final variable unrotated was copied into class AnonymousClassJFrame. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.Drawing.Image unrotated;
			private const long serialVersionUID = 1L;
			
			protected override void  OnPaint(System.Windows.Forms.PaintEventArgs g_EventArg)
			{
				System.Drawing.Graphics g = null;
				if (g_EventArg != null)
					g = g_EventArg.Graphics;
				SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.Blue);
				g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, 0, Size.Width, Size.Height);
				//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				g.DrawImage(rotated, 100, 100);
				//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				g.DrawImage(unrotated, 0, 0);
				//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				g.DrawImage(unrotated, SupportClass.CreateRectangle(100 + unrotated.Width, unrotated.Height, 100, 0), SupportClass.CreateRectangle(0, 0, 0 + unrotated.Width, unrotated.Height), System.Drawing.GraphicsUnit.Pixel);
			}
		}
		private double angle;
		private double cos, sin;
		private System.Drawing.Rectangle rotatedSpace;
		private System.Drawing.Rectangle originalSpace;
		private System.Drawing.Color defaultRGBModel;
		private int[] inPixels, outPixels;
		
		public RotateFilter(double angle)
		{
			this.angle = angle * (System.Math.PI / 180);
			cos = System.Math.Cos(this.angle);
			sin = System.Math.Sin(this.angle);
			defaultRGBModel = SupportClass.ColorSupport.GetRGBDefault();
		}
		
		private void  transform(int x, int y, double[] out_Renamed)
		{
			out_Renamed[0] = (x * cos) + (y * sin);
			out_Renamed[1] = (y * cos) - (x * sin);
		}
		
		private void  transformBack(int x, int y, double[] out_Renamed)
		{
			out_Renamed[0] = (x * cos) - (y * sin);
			out_Renamed[1] = (y * cos) + (x * sin);
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  transformSpace(ref System.Drawing.Rectangle rect)
		{
			double[] out_Renamed = new double[2];
			//UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			double minx = System.Double.MaxValue;
			//UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MAX_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			double miny = System.Double.MaxValue;
			//UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MIN_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			double maxx = System.Double.MinValue;
			//UPGRADE_TODO: The equivalent in .NET for field 'java.lang.Double.MIN_VALUE' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			double maxy = System.Double.MinValue;
			int w = rect.Width;
			int h = rect.Height;
			int x = rect.X;
			int y = rect.Y;
			for (int i = 0; i < 4; i++)
			{
				switch (i)
				{
					
					case 0: 
						transform(x + 0, y + 0, out_Renamed);
						break;
					
					case 1: 
						transform(x + w - 1, y + 0, out_Renamed);
						break;
					
					case 2: 
						transform(x + 0, y + h - 1, out_Renamed);
						break;
					
					case 3: 
						transform(x + w - 1, y + h - 1, out_Renamed);
						break;
					}
				minx = System.Math.Min(minx, out_Renamed[0]);
				miny = System.Math.Min(miny, out_Renamed[1]);
				maxx = System.Math.Max(maxx, out_Renamed[0]);
				maxy = System.Math.Max(maxy, out_Renamed[1]);
			}
			/*
			rect.x = (int) Math.floor(minx);
			rect.y = (int) Math.floor(miny);
			rect.width = (int) Math.ceil(maxx) - rect.x;
			rect.height = (int) Math.ceil(maxy) - rect.y;*/
			//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
			rect.X = (int) System.Math.Round(minx);
			//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
			rect.Y = (int) System.Math.Round(miny);
			//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
			rect.Width = (int) System.Math.Round(maxx) - rect.X + 1;
			//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
			rect.Height = (int) System.Math.Round(maxy) - rect.Y + 1;
		}
		
		/// <summary> Tell the consumer the new dimensions based on our
		/// rotation of coordinate space.
		/// </summary>
		/// <seealso cref="ImageConsumer.setDimensions">
		/// </seealso>
		//UPGRADE_NOTE: The equivalent of method 'java.awt.image.ImageFilter.setDimensions' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  setDimensions(int width, int height)
		{
			originalSpace = new System.Drawing.Rectangle(0, 0, width, height);
			rotatedSpace = new System.Drawing.Rectangle(0, 0, width, height);
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			transformSpace(ref rotatedSpace);
			inPixels = new int[originalSpace.Width * originalSpace.Height];
			//UPGRADE_ISSUE: Method 'java.awt.image.ImageConsumer.setDimensions' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
			consumer.setDimensions(rotatedSpace.Width, rotatedSpace.Height);
		}
		
		/// <summary> Tell the consumer that we use the defaultRGBModel color model
		/// NOTE: This overrides whatever color model is used underneath us.
		/// </summary>
		/// <param name="model">contains the color model of the image or filter
		/// beneath us (preceding us)
		/// </param>
		/// <seealso cref="ImageConsumer.setColorModel">
		/// </seealso>
		//UPGRADE_NOTE: The equivalent of method 'java.awt.image.ImageFilter.setColorModel' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public void  setColorModel(ref System.Drawing.Color model)
		{
			//UPGRADE_ISSUE: Method 'java.awt.image.ImageConsumer.setColorModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
			consumer.setColorModel(defaultRGBModel);
		}
		
		/// <summary> Set the pixels in our image array from the passed
		/// array of bytes. Xlate the pixels into our default
		/// color model (RGB).
		/// </summary>
		/// <seealso cref="ImageConsumer.setPixels">
		/// </seealso>
		//UPGRADE_NOTE: The equivalent of method 'java.awt.image.ImageFilter.setPixels' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public void  setPixels(int x, int y, int w, int h, ref System.Drawing.Color model, sbyte[] pixels, int off, int scansize)
		{
			int index = y * originalSpace.Width + x;
			int srcindex = off;
			int srcinc = scansize - w;
			int indexinc = originalSpace.Width - w;
			for (int dy = 0; dy < h; dy++)
			{
				for (int dx = 0; dx < w; dx++)
				{
					inPixels[index++] = SupportClass.ColorSupport.GetRGBFromColor(pixels[srcindex++] & 0xff);
				}
				srcindex += srcinc;
				index += indexinc;
			}
		}
		
		/// <summary> Set the pixels in our image array from the passed
		/// array of integers. Xlate the pixels into our default
		/// color model (RGB).
		/// </summary>
		/// <seealso cref="ImageConsumer.setPixels">
		/// </seealso>
		//UPGRADE_NOTE: The equivalent of method 'java.awt.image.ImageFilter.setPixels' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public void  setPixels(int x, int y, int w, int h, ref System.Drawing.Color model, int[] pixels, int off, int scansize)
		{
			int index = y * originalSpace.Width + x;
			int srcindex = off;
			int srcinc = scansize - w;
			int indexinc = originalSpace.Width - w;
			for (int dy = 0; dy < h; dy++)
			{
				for (int dx = 0; dx < w; dx++)
				{
					inPixels[index++] = SupportClass.ColorSupport.GetRGBFromColor(pixels[srcindex++]);
				}
				srcindex += srcinc;
				index += indexinc;
			}
		}
		
		/// <summary> Notification that the image is complete and there will
		/// be no further setPixel calls.
		/// </summary>
		/// <seealso cref="ImageConsumer.imageComplete">
		/// </seealso>
		//UPGRADE_NOTE: The equivalent of method 'java.awt.image.ImageFilter.imageComplete' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  imageComplete(int status)
		{
			//UPGRADE_ISSUE: Field 'java.awt.image.ImageConsumer.IMAGEERROR' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
			//UPGRADE_ISSUE: Field 'java.awt.image.ImageConsumer.IMAGEABORTED' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
			if (status == IMAGEERROR || status == IMAGEABORTED)
			{
				//UPGRADE_ISSUE: Method 'java.awt.image.ImageConsumer.imageComplete' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
				consumer.imageComplete(status);
				return ;
			}
			double[] point = new double[2];
			int srcwidth = originalSpace.Width;
			int srcheight = originalSpace.Height;
			int outwidth = rotatedSpace.Width;
			int outheight = rotatedSpace.Height;
			int outx, outy, srcx, srcy;
			outPixels = new int[outwidth * outheight];
			outx = rotatedSpace.X;
			outy = rotatedSpace.Y;
			int index = 0;
			for (int y = 0; y < outheight; y++)
			{
				for (int x = 0; x < outwidth; x++)
				{
					// find the originalSpace point
					transformBack(outx + x, outy + y, point);
					//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
					srcx = (int) System.Math.Round(point[0]);
					//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
					srcy = (int) System.Math.Round(point[1]);
					// if this point is within the original image
					// retreive its pixel value and store in output
					// else write a zero into the space. (0 alpha = transparent)
					if (srcx < 0 || srcx >= srcwidth || srcy < 0 || srcy >= srcheight)
					{
						outPixels[index++] = 0;
					}
					else
					{
						outPixels[index++] = inPixels[(srcy * srcwidth) + srcx];
					}
				}
			}
			// write the entire new image to the consumer
			//UPGRADE_ISSUE: Method 'java.awt.image.ImageConsumer.setPixels' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
			consumer.setPixels(0, 0, outwidth, outheight, defaultRGBModel, outPixels, 0, outwidth);
			// tell consumer we are done
			//UPGRADE_ISSUE: Method 'java.awt.image.ImageConsumer.imageComplete' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageImageConsumer'"
			consumer.imageComplete(status);
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'unrotated '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
			Toolkit.getDefaultToolkit();
			System.Drawing.Image unrotated = System.Drawing.Image.FromFile("ASL/images/Climb1d.gif");
			
			System.Object filter = new RotateFilter(- 60.0);
			System.Drawing.Image producer = SupportClass.ImageSupport.FilterImage(unrotated, filter);
			//UPGRADE_NOTE: Final was removed from the declaration of 'rotated '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.awt.Component.createImage' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentcreateImage_javaawtimageImageProducer'"
			System.Drawing.Image rotated = new System.Windows.Forms.Label().createImage(producer);
			System.Windows.Forms.Form f = new AnonymousClassJFrame(rotated, unrotated);
			//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
			f.Size = new System.Drawing.Size(300, 300);
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			f.Visible = true;
		}
	}
}