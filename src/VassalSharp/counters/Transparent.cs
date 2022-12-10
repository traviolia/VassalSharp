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
using GameModule = VassalSharp.build.GameModule;
namespace VassalSharp.counters
{
	
	/// <summary> A class that draws a GamePiece with a specifyable level of transparency</summary>
	/// <deprecated> No longer used by anything.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class Transparent
	{
		virtual public GamePiece Piece
		{
			get
			{
				return piece;
			}
			
			set
			{
				piece = value;
				opaque = new PieceImage(value);
			}
			
		}
		virtual public double Alpha
		{
			set
			{
				alpha = value;
			}
			
		}
		private double alpha = 0.2;
		private PieceImage opaque;
		private System.Drawing.Image im;
		private GamePiece piece;
		private System.Drawing.Point offset;
		
		public Transparent(GamePiece p)
		{
			Piece = p;
		}
		
		public virtual void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			if (alpha == 1.0)
			{
				piece.draw(g, x, y, obs, zoom);
				return ;
			}
			if (opaque.Changed)
			{
				
				//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Color.getRGB' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				int trans = VassalSharp.tools.TransparentFilter.getOffscreenEquivalent(obs.BackColor.ToArgb(), obs);
				VassalSharp.tools.TransparentFilter filter = new VassalSharp.tools.TransparentFilter();
				filter.setAlpha(alpha);
				filter.setAlpha(0.0, trans);
				im = opaque.getImage(obs);
				System.Drawing.Image im2 = new System.Drawing.Bitmap(im.Width, im.Height);
				System.Drawing.Graphics gg = (System.Drawing.Graphics) System.Drawing.Graphics.FromImage(im2);
				//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				gg.DrawImage(im, 0, 0);
				gg.Dispose();
				//UPGRADE_ISSUE: Method 'java.awt.Component.createImage' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentcreateImage_javaawtimageImageProducer'"
				im = obs.createImage(SupportClass.ImageSupport.FilterImage(im2, filter));
				offset = new System.Drawing.Point(piece.boundingBox().X, piece.boundingBox().Y);
				
				/*
				im = opaque.getImage(obs);
				final Image im2 = obs.createImage(im.getWidth(obs), im.getHeight(obs));
				final Graphics2D gg = (Graphics2D) im2.getGraphics();
				gg.setComposite(
				AlphaComposite.getInstance(AlphaComposite.SRC_OVER, (float) alpha));
				gg.drawImage(im, 0, 0, obs);
				gg.dispose();
				
				offset = new Point(piece.boundingBox().x,
				piece.boundingBox().y);*/
			}
			
			System.Drawing.Image scaled = im;
			if (zoom != 1.0)
			{
				scaled = GameModule.getGameModule().getDataArchive().getScaledImage(im, zoom);
			}
			//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			g.DrawImage(scaled, x + (int) (zoom * offset.X), y + (int) (zoom * offset.Y));
		}
	}
}