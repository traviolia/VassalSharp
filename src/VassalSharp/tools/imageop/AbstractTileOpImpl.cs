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
namespace VassalSharp.tools.imageop
{
	
	/// <summary> The abstract base class for {@link ImageOp}s which are
	/// a single tile. This class provides simpler implementations of the
	/// tile-related methods than does {@link AbstractTiledOp}, as objects
	/// instantiating this class' subclasses are all single tiles.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public abstract class AbstractTileOpImpl:AbstractOpImpl
	{
		public AbstractTileOpImpl()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			// we are but a single humble tile
			if (tileX != 0 || tileY != 0)
				throw new System.IndexOutOfRangeException();
			
			return getFutureImage(obs);
		}
		/// <summary> {@inheritDoc}
		/// 
		/// This implementation is equivalent to {@link #getSize}.
		/// </summary>
		override public System.Drawing.Size TileSize
		{
			get
			{
				return Size;
			}
			
		}
		/// <summary> {@inheritDoc}
		/// 
		/// This implementation is equivalent to {@link #getHeight}.
		/// </summary>
		override public int TileHeight
		{
			get
			{
				return Height;
			}
			
		}
		/// <summary> {@inheritDoc}
		/// 
		/// This implementation is equivalent to {@link #getWidth}.
		/// </summary>
		override public int TileWidth
		{
			get
			{
				return Width;
			}
			
		}
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <returns> <code>1</code>, always
		/// </returns>
		override public int NumXTiles
		{
			get
			{
				return 1;
			}
			
		}
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <returns> <code>1</code>, always
		/// </returns>
		override public int NumYTiles
		{
			get
			{
				return 1;
			}
			
		}
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <returns> <code>getImage(obs)</code>, because there is only one tile
		/// </returns>
		/// <throws>  IndexOutOfBoundsException </throws>
		/// <summary>    If <code>tileX != 0</code> or <code>tileY != 0</code>.
		/// </summary>
		public virtual System.Drawing.Bitmap getTile(int tileX, int tileY, ImageOpObserver obs)
		{
			// we are but a single humble tile
			if (tileX != 0 || tileY != 0)
				throw new System.IndexOutOfRangeException();
			
			return getImage(obs);
		}
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <returns> <code>getFutureImage(obs)</code>, because there is only one tile
		/// </returns>
		/// <throws>  IndexOutOfBoundsException </throws>
		/// <summary>    If <code>tileX != 0</code> or <code>tileY != 0</code>.
		/// </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Future < BufferedImage > getFutureTile(
		int tileX, int tileY, ImageOpObserver obs) throws ExecutionException
		
		public override ImageOp getTileOp(int tileX, int tileY)
		{
			// we are but a single humble tile
			if (tileX != 0 || tileY != 0)
				throw new System.IndexOutOfRangeException();
			
			return this;
		}
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <throws>  IllegalArgumentException if <code>rect == null</code>. </throws>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public override System.Drawing.Point[] getTileIndices(ref System.Drawing.Rectangle rect)
		{
			if (rect.IsEmpty)
				throw new System.ArgumentException();
			System.Drawing.Size temp_Size;
			temp_Size = size;
			return rect.IntersectsWith(new System.Drawing.Rectangle(0, 0, temp_Size.Width, temp_Size.Height))?new System.Drawing.Point[]{new System.Drawing.Point(0, 0)}:new System.Drawing.Point[0];
		}
	}
}