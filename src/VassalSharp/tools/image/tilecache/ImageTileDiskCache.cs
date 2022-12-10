/*
* $Id$
*
* Copyright (c) 2010 by Joel Uckelman
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
using ImageIOException = VassalSharp.tools.image.ImageIOException;
using ImageTileSource = VassalSharp.tools.image.ImageTileSource;
using FileStore = VassalSharp.tools.io.FileStore;
namespace VassalSharp.tools.image.tilecache
{
	
	/// <summary> An on-disk {@link ImageTileSource} and {@link FileStore} for image tiles.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class ImageTileDiskCache : ImageTileSource, FileStore
	{
		private void  InitBlock()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'files '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.FileInfo[] files = SupportClass.FileSupport.GetFiles(new System.IO.FileInfo(cpath));
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < String > names = new ArrayList < String >(files.length);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(File f: files) names.add(f.getPath());
			
			return names;
			//UPGRADE_NOTE: Final was removed from the declaration of 'files '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.IO.FileInfo[] files = SupportClass.FileSupport.GetFiles(new System.IO.FileInfo(cpath));
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < String > names = new ArrayList < String >(files.length);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(File f: files)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'path '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String path = f.getPath();
				if (path.startsWith(root))
				{
					names.add(path);
				}
			}
			
			return names;
		}
		/// <summary>{@inheritDoc} </summary>
		virtual public bool Closed
		{
			get
			{
				return false;
			}
			
		}
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'cpath '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.String cpath;
		
		/// <summary> Creates an {@code ImageTileDiskCache}.
		/// 
		/// </summary>
		/// <param name="cpath">path to the root directory of the cache
		/// </param>
		public ImageTileDiskCache(System.String cpath)
		{
			InitBlock();
			this.cpath = cpath;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual System.Drawing.Bitmap getTile(System.String name, int tileX, int tileY, double scale)
		{
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			return TileUtils.read(cpath + '/' + TileUtils.tileName(name, tileX, tileY, (int) (1.0 / scale)));
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual System.Drawing.Size getTileSize(System.String name, int tileX, int tileY, double scale)
		{
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			return TileUtils.size(cpath + '/' + TileUtils.tileName(name, tileX, tileY, (int) (1.0 / scale)));
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual bool tileExists(System.String name, int tileX, int tileY, double scale)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			System.IO.FileInfo f = new System.IO.FileInfo(cpath + '/' + TileUtils.tileName(name, tileX, tileY, (int) (1.0 / scale)));
			bool tmpBool;
			if (System.IO.File.Exists(f.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(f.FullName);
			return tmpBool && System.IO.File.Exists(f.FullName);
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual bool contains(System.String path)
		{
			bool tmpBool;
			if (System.IO.File.Exists(new System.IO.FileInfo(cpath + "/" + path).FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(new System.IO.FileInfo(cpath + "/" + path).FullName);
			return tmpBool;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual System.IO.Stream getInputStream(System.String path)
		{
			//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javalangString'"
			return new System.IO.FileStream(cpath + "/" + path, System.IO.FileMode.Open, System.IO.FileAccess.Read);
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual long getSize(System.String path)
		{
			return SupportClass.FileLength(new System.IO.FileInfo(cpath + "/" + path));
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual long getMTime(System.String path)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.io.File.lastModified' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return ((new System.IO.FileInfo(cpath + "/" + path).LastWriteTime.Ticks - 621355968000000000) / 10000);
		}
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getFiles() throws IOException
		
		/// <summary>{@inheritDoc} </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getFiles(String root) throws IOException
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  close()
		{
		}
	}
}