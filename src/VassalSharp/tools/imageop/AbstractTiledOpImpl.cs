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
	
	/// <summary> The abstract base class for {@link ImageOp}s which support multiple tiles.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public abstract class AbstractTiledOpImpl:AbstractOpImpl
	{
		public AbstractTiledOpImpl()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			if (tileX < 0 || tileX >= numXTiles || tileY < 0 || tileY >= numYTiles)
				throw new System.IndexOutOfRangeException();
			
			return getTileOp(tileX, tileY).getFutureImage(obs);
		}
		/// <summary>{@inheritDoc} </summary>
		override public System.Drawing.Size TileSize
		{
			get
			{
				if (tileSize.IsEmpty)
					fixTileSize();
				return new System.Drawing.Size(new System.Drawing.Point(tileSize));
			}
			
		}
		/// <summary>{@inheritDoc} </summary>
		override public int TileHeight
		{
			get
			{
				if (tileSize.IsEmpty)
					fixTileSize();
				return tileSize.Height;
			}
			
		}
		/// <summary>{@inheritDoc} </summary>
		override public int TileWidth
		{
			get
			{
				if (tileSize.IsEmpty)
					fixTileSize();
				return tileSize.Width;
			}
			
		}
		/// <summary>{@inheritDoc} </summary>
		override public int NumXTiles
		{
			get
			{
				if (tileSize.IsEmpty)
					fixTileSize();
				return numXTiles;
			}
			
		}
		/// <summary>{@inheritDoc} </summary>
		override public int NumYTiles
		{
			get
			{
				if (tileSize.IsEmpty)
					fixTileSize();
				return numYTiles;
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'DEFAULT_TILE_SIZE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly System.Drawing.Size DEFAULT_TILE_SIZE = new System.Drawing.Size(256, 256);
		
		/// <summary>The standard size of this <code>ImageOp</code>s tiles. </summary>
		protected internal System.Drawing.Size tileSize;
		
		/// <summary>The number of tiles along the x-axis. </summary>
		protected internal int numXTiles;
		
		/// <summary>The number of tiles along the y-axis. </summary>
		protected internal int numYTiles;
		
		/// <summary>The tiles already created, stored as <code>y*numXTiles + x</code>. </summary>
		protected internal ImageOp[] tiles;
		
		/// <summary> Sets the <code>tileSize</code> which is used by {@link getTileSize},
		/// {@link getTileHeight}, {@link getTileWidth}, {@link getNumXTiles},
		/// {@link getNumYTiles}, and all other tile methods.
		/// </summary>
		protected internal virtual void  fixTileSize()
		{
			if (size.IsEmpty)
				fixSize();
			
			tileSize = DEFAULT_TILE_SIZE;
			
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			numXTiles = (int) System.Math.Ceiling((double) size.Width / tileSize.Width);
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			numYTiles = (int) System.Math.Ceiling((double) size.Height / tileSize.Height);
			
			tiles = new ImageOp[numXTiles * numYTiles];
		}
		
		/// <summary> Returns the <code>ImageOp</code> which produces tile
		/// <code>(tileX,tileY)</code>, creating it if necessary.
		/// 
		/// </summary>
		/// <returns> the <code>ImageOp</code> for tile <code>(tileX,tileY)</code>
		/// </returns>
		public override ImageOp getTileOp(int tileX, int tileY)
		{
			ImageOp top = tiles[tileY * numXTiles + tileX];
			if (top == null)
			{
				top = tiles[tileY * numXTiles + tileX] = createTileOp(tileX, tileY);
			}
			
			return top;
		}
		
		protected internal abstract ImageOp createTileOp(int tileX, int tileY);
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <throws>  IndexOutOfBoundsException unless {@code 0 <= tileX < numXTiles} </throws>
		/// <summary> and {@code 0 <= tileY < numYTiles}.
		/// </summary>
		public virtual System.Drawing.Bitmap getTile(int tileX, int tileY, ImageOpObserver obs)
		{
			if (tileX < 0 || tileX >= numXTiles || tileY < 0 || tileY >= numYTiles)
				throw new System.IndexOutOfRangeException();
			
			return getTileOp(tileX, tileY).getImage(obs);
		}
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <throws>  IndexOutOfBoundsException unless {@code 0 <= tileX < numXTiles} </throws>
		/// <summary> and {@code 0 <= tileY < numYTiles}.
		/// </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Future < BufferedImage > getFutureTile(
		int tileX, int tileY, ImageOpObserver obs) throws ExecutionException
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <throws>  IllegalArgumentException if <code>rect == null</code>. </throws>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public override System.Drawing.Point[] getTileIndices(ref System.Drawing.Rectangle rect)
		{
			if (rect.IsEmpty)
				throw new System.ArgumentException();
			
			if (size.IsEmpty || tileSize.IsEmpty)
				fixTileSize();
			
			// FIXME: maybe do this without creating new Rectangles
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.Rectangle.intersection' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.Drawing.Size temp_Size;
			temp_Size = size;
			rect = System.Drawing.Rectangle.Intersect(rect, new System.Drawing.Rectangle(0, 0, temp_Size.Width, temp_Size.Height));
			if (rect.IsEmpty)
			{
				return new System.Drawing.Point[0];
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'minTileX '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int minTileX = rect.X / tileSize.Width;
			//UPGRADE_NOTE: Final was removed from the declaration of 'minTileY '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int minTileY = rect.Y / tileSize.Height;
			//UPGRADE_NOTE: Final was removed from the declaration of 'maxTileX '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int maxTileX = (rect.X + rect.Width - 1) / tileSize.Width;
			//UPGRADE_NOTE: Final was removed from the declaration of 'maxTileY '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int maxTileY = (rect.Y + rect.Height - 1) / tileSize.Height;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'tilesInRect '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Point[] tilesInRect = new System.Drawing.Point[(maxTileX - minTileX + 1) * (maxTileY - minTileY + 1)];
			
			// FIXME: Maybe do this by keeping a MRU cache of Points.
			// Maybe not, profiling shows that this isn't causing the gc to run much.
			int offset = 0;
			for (int ty = minTileY; ty <= maxTileY; ++ty)
			{
				for (int tx = minTileX; tx <= maxTileX; ++tx)
				{
					tilesInRect[offset++] = new System.Drawing.Point(tx, ty);
				}
			}
			
			return tilesInRect;
		}
	}
}