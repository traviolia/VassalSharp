/*
* $Id$
*
* Copyright (c) 2010, 2011 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutorService' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutorService = java.util.concurrent.ExecutorService;
//UPGRADE_TODO: The type 'java.util.concurrent.Future' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Future = java.util.concurrent.Future;
using GeneralFilter = VassalSharp.tools.image.GeneralFilter;
//UPGRADE_TODO: The type 'VassalSharp.tools.lang.Callback' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Callback = VassalSharp.tools.lang.Callback;
namespace VassalSharp.tools.image.tilecache
{
	
	/// <summary> Slices an image into tiles.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class TileSlicerImpl : TileSlicer
	{
		public TileSlicerImpl()
		{
			InitBlock();
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassTaskMaker' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassTaskMaker : TileSlicerImpl.TaskMaker
		{
			public AnonymousClassTaskMaker(TileSlicerImpl enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TileSlicerImpl enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TileSlicerImpl enclosingInstance;
			public TileSlicerImpl Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual TileTask make(System.Drawing.Bitmap src, System.IO.FileInfo f, int tx, int ty, int tw, int th, int sw, int sh)
			{
				return new TileTask(src, f, tx, ty, tw, th, sw, sh);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassTaskMaker1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassTaskMaker1 : TileSlicerImpl.TaskMaker
		{
			public AnonymousClassTaskMaker1(TileSlicerImpl enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TileSlicerImpl enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TileSlicerImpl enclosingInstance;
			public TileSlicerImpl Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'filter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private GeneralFilter.Filter filter = new GeneralFilter.Lanczos3Filter();
			
			public virtual TileTask make(System.Drawing.Bitmap src, System.IO.FileInfo f, int tx, int ty, int tw, int th, int dw, int dh)
			{
				return new ScaledTileTask(src, f, filter, tx, ty, tw, th, dw, dh);
			}
		}
		private void  InitBlock()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'sw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int sw = src.getWidth();
			//UPGRADE_NOTE: Final was removed from the declaration of 'sh '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int sh = src.getHeight();
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < Future < Void >> futures = new ArrayList < Future < Void >>();
			
			// slice unscaled 1:1 tiles
			//UPGRADE_NOTE: Final was removed from the declaration of 'unscaled '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			TileSlicerImpl.TaskMaker unscaled = new AnonymousClassTaskMaker(this);
			
			queueTileTasks(src, iname, tpath, 1, tw, th, sw, sh, unscaled, exec, futures);
			
			// slice scaled tiles, starting at 1:2
			//UPGRADE_NOTE: Final was removed from the declaration of 'scaled '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			TileSlicerImpl.TaskMaker scaled = new AnonymousClassTaskMaker1(this);
			
			for (int div = 2; sw / div > 0 && sh / div > 0; div <<= 1)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'dw '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int dw = sw / div;
				//UPGRADE_NOTE: Final was removed from the declaration of 'dh '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int dh = sh / div;
				
				queueTileTasks(src, iname, tpath, div, tw, th, dw, dh, scaled, exec, futures);
			}
			
			// wait for all tiles to complete
			try
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Future < Void > f: futures)
				{
					f.get_Renamed();
					progress.receive(null);
				}
			}
			catch (CancellationException e)
			{
				// should never happen
				throw new IllegalStateException(e);
			}
			catch (ExecutionException e)
			{
				throw (System.IO.IOException) new System.IO.IOException().initCause(e);
			}
			catch (System.Threading.ThreadInterruptedException e)
			{
				// should never happen
				throw new IllegalStateException(e);
			}
			finally
			{
				// cancel everything if anything fails
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Future < Void > f: futures)
				{
					if (!f.isDone())
						f.cancel(true);
				}
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'tcols '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int tcols = (int) System.Math.Ceiling((double) dw / tw);
			//UPGRADE_NOTE: Final was removed from the declaration of 'trows '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			int trows = (int) System.Math.Ceiling((double) dh / th);
			
			for (int tx = 0; tx < tcols; ++tx)
			{
				for (int ty = 0; ty < trows; ++ty)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'tn '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String tn = TileUtils.tileName(iname, tx, ty, div);
					//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.IO.FileInfo f = new File(tpath, tn);
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'tt '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					TileTask tt = tm.make(src, f, tx, ty, tw, th, dw, dh);
					futures.add(exec.submit(tt));
				}
			}
		}
		/// <summary> Slices an image into tiles.
		/// 
		/// </summary>
		/// <param name="src">the source image
		/// </param>
		/// <param name="iname">the basename for the tiles
		/// </param>
		/// <param name="tpath">the path for the tiles
		/// </param>
		/// <param name="tw">the tile width
		/// </param>
		/// <param name="th">the tile height
		/// </param>
		/// <param name="exec">the executor in which to run tasks
		/// </param>
		/// <param name="progress">a callback for indicating progress
		/// </param>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void slice(
		BufferedImage src, 
		String iname, 
		String tpath, 
		int tw, 
		int th, 
		ExecutorService exec, 
		Callback < Void > progress
		) throws IOException
		
		protected internal interface TaskMaker
		{
			TileTask make(System.Drawing.Bitmap src, System.IO.FileInfo f, int tx, int ty, int tw, int th, int dw, int dh);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void queueTileTasks(
		BufferedImage src, 
		String iname, 
		String tpath, 
		int div, 
		int tw, 
		int th, 
		int dw, 
		int dh, 
		TaskMaker tm, 
		ExecutorService exec, 
		List < Future < Void >> futures
		)
	}
}