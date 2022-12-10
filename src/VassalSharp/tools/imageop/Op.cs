/*
* $Id$
*
* Copyright (c) 2007-2010 by Joel Uckelman
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
using BadDataReport = VassalSharp.build.BadDataReport;
using GamePiece = VassalSharp.counters.GamePiece;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ImageIOException = VassalSharp.tools.image.ImageIOException;
using ImageNotFoundException = VassalSharp.tools.image.ImageNotFoundException;
using UnrecognizedImageTypeException = VassalSharp.tools.image.UnrecognizedImageTypeException;
using OpFailedException = VassalSharp.tools.opcache.OpFailedException;
namespace VassalSharp.tools.imageop
{
	
	public class Op
	{
		protected internal Op()
		{
		}
		
		public static SourceOp load(System.String name)
		{
			if (!name.StartsWith("/"))
			{
				name = "images/" + name;
			}
			
			if (name.EndsWith(".svg"))
			{
				return new SourceOpSVGImpl(name);
			}
			else
			{
				return new SourceOpBitmapImpl(name);
			}
		}
		
		public static SourceOp load(System.Drawing.Bitmap image)
		{
			return new ImageSourceOpBitmapImpl(image);
		}
		
		public static SourceOp loadLarge(System.String name)
		{
			if (!name.StartsWith("/"))
			{
				name = "images/" + name;
			}
			
			if (name.EndsWith(".svg"))
			{
				return new SourceOpSVGImpl(name);
			}
			else
			{
				return new SourceOpTiledBitmapImpl(name);
			}
		}
		
		public static ScaleOp scale(ImageOp sop, double scale)
		{
			if (sop is SVGOp)
			{
				return new RotateScaleOpSVGImpl((SVGOp) sop, 0.0, scale);
			}
			else if (sop is SourceOpTiledBitmapImpl)
			{
				// use the tiled version only for tiled sources
				return new ScaleOpTiledBitmapImpl(sop, scale);
			}
			else
			{
				return new ScaleOpBitmapImpl(sop, scale);
			}
		}
		
		public static RotateOp rotate(ImageOp sop, double angle)
		{
			if (sop is SVGOp)
			{
				return new RotateScaleOpSVGImpl((SVGOp) sop, angle, 1.0);
			}
			else if (angle % 90.0 == 0.0)
			{
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				return new OrthoRotateOpBitmapImpl(sop, (int) angle);
			}
			else
			{
				return new RotateScaleOpBitmapImpl(sop, angle, 1.0);
			}
		}
		
		public static RotateScaleOp rotateScale(ImageOp sop, double angle, double scale)
		{
			if (sop is SVGOp)
			{
				return new RotateScaleOpSVGImpl((SVGOp) sop, angle, scale);
			}
			else
			{
				return new RotateScaleOpBitmapImpl(sop, angle, scale);
			}
		}
		
		public static CropOp crop(ImageOp sop, int x0, int y0, int x1, int y1)
		{
			return new CropOpBitmapImpl(sop, x0, y0, x1, y1);
		}
		
		public static GamePieceOp piece(GamePiece gp)
		{
			return new GamePieceOpImpl(gp);
		}
		
		public static void  clearCache()
		{
			AbstractOpImpl.clearCache();
		}
		
		public static bool handleException(System.Exception e)
		{
			//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
			for (System.Exception c = e; c != null; c = c.getCause())
			{
				if (c is OpFailedException)
				{
					// We ignore OpFailedExceptions since the original exceptions
					// which caused them have already been reported.
					return true;
				}
				else if (c is ImageNotFoundException)
				{
					ErrorDialog.dataError(new BadDataReport("Image not found", ((ImageNotFoundException) c).File.Name, null));
					return true;
				}
				else if (c is UnrecognizedImageTypeException)
				{
					ErrorDialog.dataError(new BadDataReport("Unrecognized image type", ((UnrecognizedImageTypeException) c).File.Name, c));
					return true;
				}
				else if (c is ImageIOException)
				{
					ErrorDialog.dataError(new BadDataReport("Error reading image", ((ImageIOException) c).File.Name, c));
					return true;
				}
			}
			
			return false;
		}
	}
}