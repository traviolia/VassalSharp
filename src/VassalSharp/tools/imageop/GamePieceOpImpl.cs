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
//UPGRADE_TODO: The type 'java.util.concurrent.CancellationException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using CancellationException = java.util.concurrent.CancellationException;
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutionException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutionException = java.util.concurrent.ExecutionException;
//UPGRADE_TODO: The type 'java.util.concurrent.Future' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Future = java.util.concurrent.Future;
using GamePiece = VassalSharp.counters.GamePiece;
using Properties = VassalSharp.counters.Properties;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
//UPGRADE_TODO: The type 'VassalSharp.tools.opcache.Op' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Op = VassalSharp.tools.opcache.Op;
//UPGRADE_TODO: The type 'VassalSharp.tools.opcache.OpObserver' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using OpObserver = VassalSharp.tools.opcache.OpObserver;
namespace VassalSharp.tools.imageop
{
	
	/// <summary> An {@link ImageOp} which uses a {@link GamePiece} as its source.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class GamePieceOpImpl:AbstractTileOpImpl, GamePieceOp
	{
		private void  InitBlock()
		{
			// GamePieceOpImpl CANNOT be called asynchronously becuase it cannot
			// reliably report on its dependencies.
			if (obs != null)
				throw new System.NotSupportedException();
			return base.get_Renamed(obs); // GamePieceOpImpl CANNOT be called asynchronously becuase it cannot
			// reliably report on its dependencies.
			if (obs != null)
				throw new System.NotSupportedException();
			return base.getFuture(obs);
			return Collections.emptyList();
		}
		/// <summary> Returns the source <code>GamePiece</code>.
		/// 
		/// </summary>
		/// <returns> the source
		/// </returns>
		virtual public GamePiece Piece
		{
			get
			{
				return piece;
			}
			
		}
		virtual public System.String State
		{
			get
			{
				return state;
			}
			
		}
		/// <summary> Returns <code>true</code> iff the source <code>GamePiece</code>
		/// has changed state.
		/// 
		/// </summary>
		/// <returns> <code>true</code> iff the source has changed.
		/// </returns>
		virtual public bool Changed
		{
			get
			{
				return !state.Equals(piece.getProperty(VassalSharp.counters.Properties_Fields.VISIBLE_STATE));
			}
			
		}
		/// <summary>The image source for this <code>ImageOp</code>. </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'piece '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private GamePiece piece;
		//UPGRADE_NOTE: Final was removed from the declaration of 'state '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String state;
		//UPGRADE_NOTE: Final was removed from the declaration of 'hash '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int hash;
		
		/// <summary> Constructs an <code>ImageOp</code> which will produce an image
		/// from the given <code>GamePiece</code>.
		/// 
		/// </summary>
		/// <param name="gp">the source
		/// </param>
		/// <throws>  IllegalArgumentException if <code>gp == null</code>. </throws>
		public GamePieceOpImpl(GamePiece gp)
		{
			InitBlock();
			if (gp == null)
				throw new System.ArgumentException();
			piece = gp;
			state = System.Convert.ToString(piece.getProperty(VassalSharp.counters.Properties_Fields.VISIBLE_STATE));
			hash = piece.GetHashCode();
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		new public System.Drawing.Bitmap get_Renamed;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(OpObserver < BufferedImage > obs) 
		throws CancellationException, InterruptedException, ExecutionException
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Future < BufferedImage > getFuture(OpObserver < BufferedImage > obs) 
		throws ExecutionException
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < Op < ? >> getSources()
		
		/// <summary>{@inheritDoc} </summary>
		public override System.Drawing.Bitmap eval()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Rectangle b = piece.boundingBox();
			//UPGRADE_NOTE: Final was removed from the declaration of 'im '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap im = ImageUtils.createCompatibleTranslucentImage(System.Math.Max(b.Width, 1), System.Math.Max(b.Height, 1));
			//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(im);
			piece.draw(g, - b.X, - b.Y, null, 1.0);
			g.Dispose();
			return im;
		}
		
		/// <summary>{@inheritDoc} </summary>
		protected internal override void  fixSize()
		{
			size = piece.boundingBox().Size;
			if (size.Width < 1)
				size.Width = 1;
			if (size.Height < 1)
				size.Height = 1;
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
			
			GamePieceOp op = (GamePieceOp) o;
			return piece.Equals(op.Piece) && state.Equals(op.State);
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