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
namespace VassalSharp.tools.imageop
{
	
	/// <summary> An abstract representation of an operation which may be applied to an
	/// {@link Image}. <code>ImageOp</code> is the interface for all such
	/// operations. The results of all operations are memoized (using a
	/// memory-sensitive cache), so retrieving results is both fast and
	/// memory-efficient.
	/// 
	/// <p><b>Warning:</b> For efficiency reasons, no images retrieved from
	/// an <code>ImageOp</code> are returned defensively. That is, the
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
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	interface ImageOp extends VassalSharp.tools.opcache.Op < BufferedImage >
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	
	/// <summary> The image computation itself happens in this method.
	/// 
	/// <p><b>Warning:</b> This method is not intended to be called from
	/// anywhere except {@link #getImage}.</p>
	/// 
	/// </summary>
	/// <throws>  Exception The operation represented by this <code>ImageOp</code> </throws>
	/// <summary> could be anything, so any exception may be thrown.
	/// </summary>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public BufferedImage eval() throws Exception;
	
	/// <summary> Calculates the <code>BufferedImage</code> produced by this operation.
	/// Calls to this method are memoized to prevent redundant computations.
	/// 
	/// <p><b>Warning:</b> <code>BufferedImage</code>s returned by this method
	/// <em>must not</em> be modified.</p>
	/// 
	/// </summary>
	/// <returns> the resulting <code>BufferedImage</code>
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public BufferedImage getImage();
	
	/// <summary> Calculates the <code>BufferedImage</code> produced by this operation, and
	/// reports completion or failure to the specified
	/// <code>ImageOpObserver</code>. Calls to this method are memoized
	/// to prevent redundant computations. If a non-<code>null</code> observer
	/// is given, then the operation may be done asynchronously. If the
	/// observer is <code>null</code>, then this method will block on
	/// completion of the operation.
	/// 
	/// <p> When a non-blocking call is made (i.e., when
	/// <code>obs != null</code>), the cache is checked and if the image is
	/// found, it is returned immediately. If the image is already being
	/// calculated, <code>obs</code> is notified when the pre-existing request
	/// completes. Otherwise, a new request is queued and <code>obs</code> is
	/// notified when that completes.</p>
	/// 
	/// <p>When a blocking call is made (i.e., when <code>obs == null</code>),
	/// the cache is checked and if the image is found, it is returned
	/// immediately. If the image is already being calculated, this method
	/// blocks on the completion of the existing calculation. Otherwise,
	/// a new calculation is started and this method blocks on it. In
	/// all cases, when a calculation is completed, the result is cached.</p>
	/// 
	/// <p><b>Warning:</b> <code>BufferedImage</code>s returned by this method
	/// <em>must not</em> be modified.</p>
	/// 
	/// </summary>
	/// <param name="obs">the observer to be notified on completion
	/// </param>
	/// <returns> the resulting <code>BufferedImage</code>
	/// </returns>
	/// <throws>  CancellationException if the operation was cancelled </throws>
	/// <throws>  InterruptedException if the operation was interrupted </throws>
	/// <throws>  ExecutionException if the operation failed </throws>
	/// <seealso cref="getTile">
	/// </seealso>
	/// <seealso cref="getFutureTile">
	/// </seealso>
	/// <seealso cref="getFutureImage">
	/// </seealso>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public BufferedImage getImage(ImageOpObserver obs) 
	throws CancellationException, InterruptedException, ExecutionException;
	
	/// <summary> Submits a request for the <code>BufferedImage</code> produced by this
	/// operation, and returns a reference to that request.
	/// 
	/// If a non-<code>null</code> observer is given, then the operation may
	/// be done asynchronously. If the observer is <code>null</code>, then
	/// this method will block on completion of the operation.
	/// 
	/// <p>This implementaion uses a memory-sensitive cache to memoize
	/// calls to <code>getFutureImage</code>. It returns a
	/// {@code Future<BufferedImage>} so that the request may be cancelled if no
	/// longer needed.</p>
	/// 
	/// <p><code>Future</code>s are returned immediately, except in the
	/// case where the is no observer and no pre-existing <code>Future</code>
	/// for this <code>ImageOp</code>'s <code>BufferedImage</code>, in which
	/// case this method blocks on completion of the computation.</p>
	/// 
	/// <p><b>Warning:</b> <code>BufferedImage</code>s obtained from the
	/// <code>Future</code>s returned by this method <em>must not</em> be
	/// modified.</p>
	/// 
	/// </summary>
	/// <param name="obs">the observer to be notified on completion
	/// </param>
	/// <returns> a <code>Future</code> for the resulting <code>BufferedImage</code>
	/// </returns>
	/// <throws>  ExecutionException if the operation failed </throws>
	/// <seealso cref="getTile">
	/// </seealso>
	/// <seealso cref="getFutureTile">
	/// </seealso>
	/// <seealso cref="getImage">
	/// </seealso>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Future < BufferedImage > getFutureImage(ImageOpObserver obs) 
	throws ExecutionException;
	
	/// <summary> Returns the size of the <code>BufferedImage</code> which would be returned
	/// by {@link #getImage}. The size is cached so that it need not be
	/// recalculated on each call.
	/// 
	/// </summary>
	/// <returns> the size of the resulting <code>BufferedImage</code> in pixels
	/// </returns>
	/// <seealso cref="getHeight">
	/// </seealso>
	/// <seealso cref="getWidth">
	/// </seealso>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Dimension getSize();
	
	/// <summary> Returns the width of the <code>BufferedImage</code> which would be
	/// returned by {@link #getImage}. The width is cached so that it need not
	/// be recalculated on each call.
	/// 
	/// </summary>
	/// <returns> the width of the resulting <code>BufferedImage</code> in pixels
	/// </returns>
	/// <seealso cref="getHeight">
	/// </seealso>
	/// <seealso cref="getSize">
	/// </seealso>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public int getWidth();
	
	/// <summary> Returns the height of the <code>BufferedImage</code> which would be
	/// returned by {@link #getImage}. The height is cached so that it need
	/// not be recalculated on each call.
	/// 
	/// </summary>
	/// <returns> the height of the resulting <code>BufferedImage</code> in pixels
	/// </returns>
	/// <seealso cref="getWidth">
	/// </seealso>
	/// <seealso cref="getSize">
	/// </seealso>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public int getHeight();
	
	/// <summary> Returns the standard size of the <code>BufferedImage</code> tiles
	/// which are returned by {@link #getTile}. Tiles which are in the extreme
	/// right column will not have full width if the <code>BufferedImage</code>
	/// width is not an integral multiple of the tile width. Similarly, tiles in
	/// the bottom row will not have full height if the <code>BufferedImage</code>
	/// height is not an integral multiple of the tile height.
	/// 
	/// </summary>
	/// <returns> the size of <code>BufferedImage</code> tiles in pixels
	/// </returns>
	/// <seealso cref="getTileHeight">
	/// </seealso>
	/// <seealso cref="getTileWidth">
	/// </seealso>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Dimension getTileSize();
	
	/// <summary> Returns the standard height of the <code>BufferedImage</code> tiles
	/// which are returned by {@link #getTile}.
	/// 
	/// </summary>
	/// <returns> the height of <code>BufferedImage</code> tiles in pixels
	/// </returns>
	/// <seealso cref="getTileSize">
	/// </seealso>
	/// <seealso cref="getTileWidth">
	/// </seealso>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public int getTileHeight();
	
	/// <summary> Returns the standard width of the <code>BufferedImage</code> tiles which
	/// are returned by {@link #getTile}.
	/// 
	/// </summary>
	/// <returns> the width of <code>BufferedImage</code> tiles in pixels
	/// </returns>
	/// <seealso cref="getTileSize">
	/// </seealso>
	/// <seealso cref="getTileHeight">
	/// </seealso>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public int getTileWidth();
	
	/// <summary> Returns the number of tiles along the x-axis. There will always be at
	/// least one column of tiles. The number of columns <em>should</em>
	/// equal <code>(int) Math.ceil((double) getWidth() / getTileWidth())</code>.
	/// 
	/// </summary>
	/// <returns> the number of tiles along the x-axis
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public int getNumXTiles();
	
	/// <summary> Returns the number of tiles along the y-axis. There will always be at
	/// least one row of tiles. The number of rows <em>should</em> equal
	/// <code>(int) Math.ceil((double) getHeight() / getTileHeight())</code>.
	/// 
	/// </summary>
	/// <returns> the number of tiles along the y-axis
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public int getNumYTiles();
	
	/// <summary> Calculates tile <code>(p.x,p.y)</code>, and reports
	/// completion or failure to the specified <code>ImageOpObserver</code>.
	/// If a non-<code>null</code> observer is given, then the operation may
	/// be done asynchronously. If the observer is <code>null</code>, then
	/// this method will block on completion of the operation. Tiles are
	/// numbered from zero, so the tile in the upper-left corner of the main
	/// <code>BufferedImage</code> is <code>(0,0)</code>. Note that
	/// <code>p.x</code> and <code>p.y</code> are indices into the tile array,
	/// not pixel locations.
	/// 
	/// <p>This convenience method is equivalent to
	/// <code>getTile(p.x, p.y, obs)</code>.</p>
	/// 
	/// <p><b>Warning:</b> <code>BufferedImage</code>s returned by this method
	/// <em>must not</em> be modified.</p>
	/// 
	/// </summary>
	/// <param name="p">the position of the requested tile
	/// </param>
	/// <param name="obs">the observer
	/// </param>
	/// <returns> the resulting <code>BufferedImage</code>
	/// </returns>
	/// <throws>  CancellationException if the operation was cancelled </throws>
	/// <throws>  InterruptedException if the operation was interrupted </throws>
	/// <throws>  ExecutionException if the operation failed </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public BufferedImage getTile(Point p, ImageOpObserver obs) 
	throws CancellationException, InterruptedException, ExecutionException;
	
	/// <summary> Calculates tile <code>(tileX,tileY)</code>, and reports
	/// completion or failure to the specified <code>ImageOpObserver</code>.
	/// If a non-<code>null</code> observer is given, then the operation may
	/// be done asynchronously. If the observer is <code>null</code>, then
	/// this method will block on completion of the operation. Tiles are
	/// numbered from zero, so the tile in the upper-left corner of the main
	/// <code>BufferedImage</code> is <code>(0,0)</code>. Note that
	/// <code>tileX</code> and <code>tileY</code> are indices into the tile
	/// array, not pixel locations.
	/// 
	/// <p><b>Warning:</b> <code>BufferedImage</code>s returned by this method
	/// <em>must not</em> be modified.</p>
	/// 
	/// </summary>
	/// <param name="tileX">the x position of the requested tile
	/// </param>
	/// <param name="tileY">the y position of the requested tile
	/// </param>
	/// <param name="obs">the observer to be notified on completion
	/// </param>
	/// <returns> the resulting <code>BufferedImage</code>
	/// </returns>
	/// <throws>  CancellationException if the operation was cancelled </throws>
	/// <throws>  InterruptedException if the operation was interrupted </throws>
	/// <throws>  ExecutionException if the operation failed </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public BufferedImage getTile(int tileX, int tileY, ImageOpObserver obs) 
	throws CancellationException, InterruptedException, ExecutionException;
	
	/// <summary> Submits a request for tile <code>(tileX,tileY)</code>, and returns a
	/// reference to that request. If a non-<code>null</code> observer is given,
	/// then the operation may be done asynchronously. If the observer is
	/// <code>null</code>, then this method will block on completion of the
	/// operation. Tiles are numbered from zero, so the tile in the upper-left
	/// corner of the main <code>BufferedImage</code> is <code>(0,0)</code>.
	/// Note that <code>tileX</code> and <code>tileY</code> are indices into the
	/// tile array, not pixel locations.
	/// 
	/// <p>This convenience method is equivalent to
	/// <code>getFutureTile(p.x, p.y, obs)</code>.</p>
	/// 
	/// <p><b>Warning:</b> <code>BufferedImage</code>s obtained from the
	/// <code>Future</code>s returned by this method <em>must not</em> be
	/// modified.</p>
	/// 
	/// </summary>
	/// <param name="p">the position of the requested tile
	/// </param>
	/// <param name="obs">the observer to be notified on completion
	/// </param>
	/// <returns> a <code>Future</code> for the resulting <code>BufferedImage</code>
	/// </returns>
	/// <throws>  ExecutionException if the operation failed </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Future < BufferedImage > getFutureTile(Point p, ImageOpObserver obs) 
	throws ExecutionException;
	
	/// <summary> Submits a request for tile <code>(tileX,tileY)</code>, and returns a
	/// reference to that request. If a non-<code>null</code> observer is given,
	/// then the operation may be done asynchronously. If the observer is
	/// <code>null</code>, then this method will block on completion of the
	/// operation. Tiles are numbered from zero, so the tile in the upper-left
	/// corner of the main <code>BufferedImage</code> is <code>(0,0)</code>.
	/// Note that <code>tileX</code> and <code>tileY</code> are indices into the
	/// tile array, not pixel locations.
	/// 
	/// <p><b>Warning:</b> <code>BufferedImage</code>s obtained from the
	/// <code>Future</code>s returned by this method <em>must not</em> be
	/// modified.</p>
	/// 
	/// </summary>
	/// <param name="tileX">the x position of the requested tile
	/// </param>
	/// <param name="tileY">the y position of the requested tile
	/// </param>
	/// <param name="obs">the observer to be notified on completion
	/// </param>
	/// <returns> a <code>Future</code> for the resulting <code>BufferedImage</code>
	/// </returns>
	/// <throws>  ExecutionException if the operation failed </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Future < BufferedImage > getFutureTile(
	int tileX, int tileY, ImageOpObserver obs) throws ExecutionException;
	
	/// <summary> Returns an <code>ImageOp</code> which can produce the requested tile.
	/// 
	/// <p>This convenience method is equivalent to
	/// <code>getTileOp(p.x, p.y)</code>.</p>
	/// 
	/// </summary>
	/// <param name="p">the position of the requested tile
	/// </param>
	/// <returns> the <code>ImageOp</code> which produces the requested tile
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public ImageOp getTileOp(Point p);
	
	/// <summary> Returns an <code>ImageOp</code> which can produce the requested tile.
	/// 
	/// </summary>
	/// <param name="tileX">the x position of the requested tile
	/// </param>
	/// <param name="tileY">the y position of the requested tile
	/// </param>
	/// <returns> the <code>ImageOp</code> which produces the requested tile
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public ImageOp getTileOp(int tileX, int tileY);
	
	/// <summary> Returns an array of <code>Point</code>s representing the tiles
	/// intersecting the given <code>Rectangle</code>.
	/// 
	/// </summary>
	/// <param name="rect">the rectangle
	/// </param>
	/// <returns> the positions of the tiles hit by the rectangle
	/// </returns>
	/// <throws>  IllegalArgumentException if <code>rect == null</code>. </throws>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Point [] getTileIndices(Rectangle rect);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}