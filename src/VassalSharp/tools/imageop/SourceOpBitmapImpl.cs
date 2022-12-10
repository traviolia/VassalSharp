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
using GameModule = VassalSharp.build.GameModule;
using DataArchive = VassalSharp.tools.DataArchive;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
using ImageIOException = VassalSharp.tools.image.ImageIOException;
using ImageNotFoundException = VassalSharp.tools.image.ImageNotFoundException;
using ImageUtils = VassalSharp.tools.image.ImageUtils;
using FileArchive = VassalSharp.tools.io.FileArchive;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.tools.imageop
{
	
	/// <summary> An {@link ImageOp} which loads an image from the {@link DataArchive}.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class SourceOpBitmapImpl:AbstractTiledOpImpl, SourceOp
	{
		private void  InitBlock()
		{
			return Collections.emptyList();
		}
		virtual protected internal System.IO.Stream InputStream
		{
			get
			{
				return darch != null?darch.getInputStream(name):farch.getInputStream(name);
			}
			
		}
		virtual protected internal System.Drawing.Size ImageSize
		{
			// FIXME: we need a way to invalidate ImageOps when an exception is thrown?
			// Maybe size should go to -1,-1 when invalid?
			
			
			get
			{
				try
				{
					System.IO.Stream in_Renamed = null;
					try
					{
						in_Renamed = InputStream;
						
						//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Drawing.Size d = ImageUtils.getImageSize(name, in_Renamed);
						in_Renamed.Close();
						return d;
					}
					catch (ImageIOException e)
					{
						// Don't wrap, just rethrow.
						throw e;
					}
					catch (System.IO.FileNotFoundException e)
					{
						throw new ImageNotFoundException(name, e);
					}
					catch (System.IO.IOException e)
					{
						throw new ImageIOException(name, e);
					}
					finally
					{
						IOUtils.closeQuietly(in_Renamed);
					}
				}
				catch (System.IO.IOException e)
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
		
		/// <summary>The archive file from which the image will be loaded </summary>
		//UPGRADE_NOTE: Final was removed from the declaration of 'darch '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal DataArchive darch;
		//UPGRADE_NOTE: Final was removed from the declaration of 'farch '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal FileArchive farch;
		
		/// <summary> Constructs an <code>ImageOp</code> which will load the given file.
		/// 
		/// </summary>
		/// <param name="name">the name of the image to load
		/// </param>
		/// <throws>  IllegalArgumentException </throws>
		/// <summary>    if <code>name</code> is <code>null</code>.
		/// </summary>
		public SourceOpBitmapImpl(System.String name):this(name, GameModule.getGameModule().getDataArchive())
		{
		}
		
		public SourceOpBitmapImpl(System.String name, DataArchive archive):this(name, archive, null)
		{
		}
		
		public SourceOpBitmapImpl(System.String name, FileArchive archive):this(name, null, archive)
		{
		}
		
		protected internal SourceOpBitmapImpl(System.String name, DataArchive da, FileArchive fa)
		{
			InitBlock();
			if (name == null)
			{
				throw new System.ArgumentException();
			}
			
			if (name.Length == 0)
			{
				throw new System.ArgumentException();
			}
			
			if (da == null && fa == null)
			{
				throw new System.ArgumentException();
			}
			
			this.name = name;
			this.darch = da;
			this.farch = fa;
			
			hash = name.GetHashCode() ^ (darch != null?darch.GetHashCode():farch.GetHashCode());
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < VassalSharp.tools.opcache.Op < ? >> getSources()
		
		/// <summary>  {@inheritDoc}
		/// 
		/// </summary>
		/// <throws>  IOException if the image cannot be loaded from the image file. </throws>
		public override System.Drawing.Bitmap eval()
		{
			System.IO.Stream in_Renamed = null;
			try
			{
				in_Renamed = InputStream;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'img '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Bitmap img = ImageUtils.getImage(name, in_Renamed);
				in_Renamed.Close();
				return img;
			}
			catch (ImageIOException e)
			{
				// Don't wrap, just rethrow.
				throw e;
			}
			catch (System.IO.FileNotFoundException e)
			{
				throw new ImageNotFoundException(name, e);
			}
			catch (System.IO.IOException e)
			{
				throw new ImageIOException(name, e);
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
		
		/// <summary>{@inheritDoc} </summary>
		protected internal override void  fixSize()
		{
			if ((size = SizeFromCache).IsEmpty)
			{
				size = ImageSize;
			}
		}
		
		protected internal override ImageOp createTileOp(int tileX, int tileY)
		{
			return new SourceTileOpBitmapImpl(this, tileX, tileY);
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
			SourceOpBitmapImpl s = (SourceOpBitmapImpl) o;
			return darch == s.darch && farch == s.farch && name.Equals(s.name);
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
			return GetType().FullName + "[name=" + name + "]";
		}
	}
}