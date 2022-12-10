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
//UPGRADE_TODO: The type 'java.util.concurrent.CancellationException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using CancellationException = java.util.concurrent.CancellationException;
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutionException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutionException = java.util.concurrent.ExecutionException;
//UPGRADE_TODO: The type 'java.util.concurrent.Future' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Future = java.util.concurrent.Future;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using OpCache = VassalSharp.tools.opcache.OpCache;
namespace VassalSharp.tools.imageop
{
	
	/// <summary> An abstract representation of an operation which may be applied to an
	/// {@link Image}. <code>ImageOp</code> is the base class for all such
	/// operations. The results of all operations are memoized (using a
	/// memory-sensitive cache), so retrieving results is both fast and
	/// memory-efficient.
	/// 
	/// <p><b>Warning:</b> For efficiency reasons, the methods {@link #getImage}
	/// and {@link #getTile} do <em>not</em> return <code>Image</code>s
	/// defensively, nor do the {@code Future<Image>}s returned by
	/// {@link #getFutureImage} and {@link #getFutureTile}. That is, the
	/// <code>Image</code>  returned is possibly the one retained internally by
	/// the <code>ImageOp</code>. Therefore, <code>Image</code>s obtained from
	/// an <code>ImageOp</code> <em>must not</em> be altered, as this might
	/// interfere with image caching. If an <code>Image</code> obtained this way
	/// needs to be modified, copy the <code>Image</code> first and alter the
	/// copy.</p>
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public abstract class AbstractOpImpl:VassalSharp.tools.opcache.AbstractOpImpl
	{
		/// <summary>The cached size of this operation's resulting <code>Image</code>. </summary>
		private void  InitBlock()
		{
			return getFuture(obs);
			return getFutureTile(p.x, p.y, obs);
		}
		/// <summary> A utility method for retrieving the size of the computed
		/// <code>Image</code> from the cache if the <code>Image</code>
		/// is cached.
		/// 
		/// </summary>
		/// <returns> the size of the cached <code>Image</code>, or
		/// <code>null</code> if the <code>Image</code> isn't cached
		/// </returns>
		virtual protected internal System.Drawing.Size SizeFromCache
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'im '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Bitmap im = cache.getIfDone(newKey());
				return im == null?System.Drawing.Size.Empty:new System.Drawing.Size(im.Width, im.Height);
			}
			
		}
		/// <summary>{@inheritDoc} </summary>
		virtual public System.Drawing.Size Size
		{
			get
			{
				if (size.IsEmpty)
					fixSize();
				return new System.Drawing.Size(new System.Drawing.Point(size));
			}
			
		}
		/// <summary>{@inheritDoc} </summary>
		virtual public int Width
		{
			get
			{
				if (size.IsEmpty)
					fixSize();
				return size.Width;
			}
			
		}
		/// <summary>{@inheritDoc} </summary>
		virtual public int Height
		{
			get
			{
				if (size.IsEmpty)
					fixSize();
				return size.Height;
			}
			
		}
		/// <summary>{@inheritDoc} </summary>
		public abstract System.Drawing.Size TileSize{get;}
		/// <summary>{@inheritDoc} </summary>
		public abstract int TileHeight{get;}
		/// <summary>{@inheritDoc} </summary>
		public abstract int TileWidth{get;}
		/// <summary>{@inheritDoc} </summary>
		public abstract int NumXTiles{get;}
		/// <summary>{@inheritDoc} </summary>
		public abstract int NumYTiles{get;}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< BufferedImage > 
		implements ImageOp
		protected internal System.Drawing.Size size;
		
		/// <summary>The cache which contains calculated <code>Image</code>s. </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'cache '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		new protected internal static readonly OpCache cache = new OpCache();
		
		public static void  clearCache()
		{
			cache.clear();
		}
		
		public AbstractOpImpl():base(cache)
		{
			InitBlock();
		}
		
		/// <summary>{@inheritDoc} </summary>
		public abstract override System.Drawing.Bitmap eval();
		
		/// <summary>{@inheritDoc} </summary>
		public virtual System.Drawing.Bitmap getImage()
		{
			try
			{
				return getImage(null);
			}
			catch (CancellationException e)
			{
				// FIXME: bug until we permit cancellation
				ErrorDialog.bug(e);
			}
			catch (System.Threading.ThreadInterruptedException e)
			{
				ErrorDialog.bug(e);
			}
			catch (ExecutionException e)
			{
				if (!Op.handleException(e))
					ErrorDialog.bug(e);
			}
			
			return null;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual System.Drawing.Bitmap getImage(ImageOpObserver obs)
		{
			return get_Renamed(obs);
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Future < BufferedImage > getFutureImage(ImageOpObserver obs) 
		throws ExecutionException
		
		/// <summary> Sets the <code>size</code> which is used by {@link getSize},
		/// {@link getHeight}, and {@link getWidth}.
		/// </summary>
		protected internal abstract void  fixSize();
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual System.Drawing.Bitmap getTile(ref System.Drawing.Point p, ImageOpObserver obs)
		{
			return getTile(p.X, p.Y, obs);
		}
		
		/// <summary>{@inheritDoc} </summary>
		public abstract System.Drawing.Bitmap getTile(int tileX, int tileY, ImageOpObserver obs);
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Future < BufferedImage > getFutureTile(Point p, ImageOpObserver obs) 
		throws ExecutionException
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		abstract Future < BufferedImage > getFutureTile(
		int tileX, int tileY, ImageOpObserver obs) throws ExecutionException;
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual ImageOp getTileOp(ref System.Drawing.Point p)
		{
			return getTileOp(p.X, p.Y);
		}
		
		/// <summary>{@inheritDoc} </summary>
		public abstract ImageOp getTileOp(int tileX, int tileY);
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public abstract System.Drawing.Point[] getTileIndices(ref System.Drawing.Rectangle rect);
	}
}