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
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
namespace VassalSharp.tools
{
	
	/// <summary> Sets the transparency of colors in an image</summary>
	/// <deprecated> Use a Graphics2D and an AlphaComposite, or
	/// a {@link VassalSharp.tools.imageop} instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	//UPGRADE_ISSUE: Class 'java.awt.image.RGBImageFilter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageRGBImageFilter'"
	public class TransparentFilter:RGBImageFilter
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.tools.TransparentFilter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		private double alpha = 1.0;
		private int[] colors = new int[0];
		private double[] alphas = new double[0];
		
		public TransparentFilter():this(1.0)
		{
		}
		
		public TransparentFilter(double alpha)
		{
			this.alpha = alpha;
		}
		
		public virtual void  setAlpha(double alpha)
		{
			this.alpha = alpha;
		}
		
		public virtual void  setAlpha(double alpha, int color)
		{
			for (int i = 0; i < colors.Length; ++i)
			{
				if (color == colors[i])
				{
					alphas[i] = alpha;
					return ;
				}
			}
			int[] newColors = new int[colors.Length + 1];
			Array.Copy(colors, 0, newColors, 0, colors.Length);
			newColors[colors.Length] = color;
			colors = newColors;
			
			double[] newAlphas = new double[alphas.Length + 1];
			Array.Copy(alphas, 0, newAlphas, 0, alphas.Length);
			newAlphas[alphas.Length] = alpha;
			alphas = newAlphas;
		}
		
		/// <summary> For the given input color, return the color that this color
		/// will map to in an offscreen image created by the given Component
		/// </summary>
		public static int getOffscreenEquivalent(int color, System.Windows.Forms.Control obs)
		{
			System.Drawing.Image im = new System.Drawing.Bitmap(1, 1);
			System.Drawing.Graphics g = (System.Drawing.Graphics) System.Drawing.Graphics.FromImage(im);
			//UPGRADE_TODO: Constructor 'java.awt.Color.Color' was converted to 'System.Drawing.Color.FromArgb' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtColorColor_int'"
			SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.FromArgb(color));
			g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, 0, 1, 1);
			g.Dispose();
			
			int[] bg = new int[1];
			SupportClass.PixelCapturer pg = new SupportClass.PixelCapturer(im, 0, 0, 1, 1, bg, 0, 1);
			try
			{
				pg.CapturePixels();
			}
			catch (System.Threading.ThreadInterruptedException ex)
			{
				logger.error("", ex);
			}
			return bg[0];
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual void  setAlpha(double alpha, ref System.Drawing.Color c)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Color.getRGB' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			setAlpha(alpha, c.ToArgb());
		}
		
		public int filterRGB(int x, int y, int rgb)
		{
			double a = alpha;
			for (int i = 0; i < colors.Length; ++i)
			{
				if (rgb == colors[i])
				{
					a = alphas[i];
					break;
				}
			}
			rgb = 0xffffff & rgb;
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			sbyte trans = (sbyte) (a * 0xff);
			return (trans << 24) | rgb;
		}
		static TransparentFilter()
		{
			logger = LoggerFactory.getLogger(typeof(TransparentFilter));
		}
	}
}