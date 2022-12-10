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
//UPGRADE_TODO: The type 'org.apache.commons.lang.builder.HashCodeBuilder' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using HashCodeBuilder = org.apache.commons.lang.builder.HashCodeBuilder;
using GameModule = VassalSharp.build.GameModule;
using DataArchive = VassalSharp.tools.DataArchive;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ImageIOException = VassalSharp.tools.image.ImageIOException;
using ImageTileSource = VassalSharp.tools.image.ImageTileSource;
namespace VassalSharp.tools.imageop
{
	
	/// <summary> An {@link ImageOp} which loads tiles from the tile cache.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class SourceOpDiskCacheBitmapImpl:AbstractTileOpImpl, SourceOp
	{
		private void  InitBlock()
		{
			return Collections.emptyList();
		}
		virtual protected internal System.Drawing.Size ImageSize
		{
			get
			{
				try
				{
					return tileSrc.getTileSize(name, tileX, tileY, scale);
				}
				catch (ImageIOException e)
				{
					if (!Op.handleException(e))
						ErrorDialog.bug(e);
				}
				
				return new System.Drawing.Size(0, 0);
			}
			
		}
		/// <summary> Returns the name of the image which {@link #getImage} will produce.
		/// 
		/// </summary>
		/// <returns> the name of the image in the {@link DataArchive}.
		/// </returns>
		virtual public System.String Name
		{
			get
			{
				return name;
			}
			
		}
		
		/// <summary>The name of the image file. </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.String name;
		
		/// <summary>The cached hash code of this object. </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'hash '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int hash;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'tileX '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int tileX;
		//UPGRADE_NOTE: Final was removed from the declaration of 'tileY '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal int tileY;
		protected internal double scale;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'tileSrc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal ImageTileSource tileSrc;
		
		/// <summary> Constructs an <code>ImageOp</code> which will load the given file.
		/// 
		/// </summary>
		/// <param name="name">the name of the image to load
		/// </param>
		/// <throws>  IllegalArgumentException </throws>
		/// <summary>    if <code>name</code> is <code>null</code>.
		/// </summary>
		public SourceOpDiskCacheBitmapImpl(System.String name, int tileX, int tileY, double scale):this(name, tileX, tileY, scale, GameModule.getGameModule().getImageTileSource())
		{
		}
		
		public SourceOpDiskCacheBitmapImpl(System.String name, int tileX, int tileY, double scale, ImageTileSource tileSrc)
		{
			InitBlock();
			if (name == null)
				throw new System.ArgumentException();
			if (name.Length == 0)
				throw new System.ArgumentException();
			if (tileX < 0)
				throw new System.ArgumentException();
			if (tileY < 0)
				throw new System.ArgumentException();
			if (scale <= 0)
				throw new System.ArgumentException();
			if (tileSrc == null)
				throw new System.ArgumentException();
			
			this.name = name;
			this.tileX = tileX;
			this.tileY = tileY;
			this.scale = scale;
			this.tileSrc = tileSrc;
			
			hash = new HashCodeBuilder().append(name).append(tileX).append(tileY).append(scale).toHashCode();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < VassalSharp.tools.opcache.Op < ? >> getSources()
		
		/// <summary> {@inheritDoc}
		/// 
		/// </summary>
		/// <throws>  IOException if the image cannot be loaded from the image file. </throws>
		public override System.Drawing.Bitmap eval()
		{
			return tileSrc.getTile(name, tileX, tileY, scale);
		}
		
		/// <summary>{@inheritDoc} </summary>
		protected internal override void  fixSize()
		{
			if ((size = SizeFromCache).IsEmpty)
			{
				size = ImageSize;
			}
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
			
			//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SourceOpDiskCacheBitmapImpl s = (SourceOpDiskCacheBitmapImpl) o;
			return name.Equals(s.name) && tileX == s.tileX && tileY == s.tileY && scale == s.scale;
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override int GetHashCode()
		{
			return hash;
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override System.String ToString()
		{
			return GetType().FullName + "[name=" + name + ",tileX=" + tileX + ",tileY=" + tileY + ",scale=" + scale + "]";
		}
	}
}