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
using GamePieceOp = VassalSharp.tools.imageop.GamePieceOp;
namespace VassalSharp.counters
{
	
	/// <summary> Maintains an {@link Image} built from the {@link GamePiece#draw}
	/// method of a {@link GamePiece}
	/// </summary>
	/// <deprecated> Use {@link GamePieceOp} instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class PieceImage
	{
		virtual public bool Changed
		{
			get
			{
				return !System.Convert.ToString(piece.getProperty(VassalSharp.counters.Properties_Fields.VISIBLE_STATE)).Equals(lastState);
			}
			
		}
		private GamePiece piece;
		private System.String lastState = null;
		private System.Drawing.Image im;
		
		public PieceImage(GamePiece piece)
		{
			this.piece = piece;
		}
		
		public virtual System.Drawing.Image getImage(System.Windows.Forms.Control obs)
		{
			if (Changed)
			{
				lastState = System.Convert.ToString(piece.getProperty(VassalSharp.counters.Properties_Fields.VISIBLE_STATE));
				
				System.Drawing.Rectangle bbox = piece.boundingBox();
				im = new System.Drawing.Bitmap(System.Math.Max(bbox.Width, 1), System.Math.Max(bbox.Height, 1), (System.Drawing.Imaging.PixelFormat) System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				//UPGRADE_ISSUE: Method 'java.awt.image.BufferedImage.setRGB' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBufferedImagesetRGB_int_int_int_int_int[]_int_int'"
				((System.Drawing.Bitmap) im).setRGB(0, 0, bbox.Width, bbox.Height, new int[bbox.Width * bbox.Height], 0, bbox.Width);
				
				System.Drawing.Graphics g = (System.Drawing.Graphics) System.Drawing.Graphics.FromImage(im);
				piece.draw(g, - bbox.X, - bbox.Y, obs, 1.0);
				g.Dispose();
			}
			return im;
		}
	}
}