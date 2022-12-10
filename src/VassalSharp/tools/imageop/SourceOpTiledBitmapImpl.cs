/*
* $Id$
*
* Copyright (c) 2009-2010 by Joel Uckelman
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
using DataArchive = VassalSharp.tools.DataArchive;
using FileArchive = VassalSharp.tools.io.FileArchive;
namespace VassalSharp.tools.imageop
{
	
	/// <summary> An {@link ImageOp} which loads an image from the {@link DataArchive},
	/// but produces tiles loaded from the tile cache.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class SourceOpTiledBitmapImpl:SourceOpBitmapImpl
	{
		/// <summary> Constructs an <code>ImageOp</code> which will load the given file.
		/// 
		/// </summary>
		/// <param name="name">the name of the image to load
		/// </param>
		/// <throws>  IllegalArgumentException </throws>
		/// <summary>    if <code>name</code> is <code>null</code>.
		/// </summary>
		public SourceOpTiledBitmapImpl(System.String name):base(name, GameModule.getGameModule().getDataArchive())
		{
		}
		
		public SourceOpTiledBitmapImpl(System.String name, DataArchive archive):base(name, archive)
		{
		}
		
		public SourceOpTiledBitmapImpl(System.String name, FileArchive archive):base(name, archive)
		{
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal override ImageOp createTileOp(int tileX, int tileY)
		{
			return new SourceOpDiskCacheBitmapImpl(name, tileX, tileY, 1.0);
		}
	}
}