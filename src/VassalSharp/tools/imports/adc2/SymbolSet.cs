/*
* $Id$
*
* Copyright (c) 2008 by Michael Kiefte
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
//UPGRADE_TODO: The type 'javax.imageio.ImageIO' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ImageIO = javax.imageio.ImageIO;
using GameModule = VassalSharp.build.GameModule;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
using BMPFileFilter = VassalSharp.tools.filechooser.BMPFileFilter;
using FileFormatException = VassalSharp.tools.imports.FileFormatException;
using ImportAction = VassalSharp.tools.imports.ImportAction;
using Importer = VassalSharp.tools.imports.Importer;
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.tools.imports.adc2
{
	
	/// <summary> ADC2 game piece and terrain symbols.
	/// 
	/// </summary>
	/// <author>  Michael Kiefte
	/// 
	/// </author>
	public class SymbolSet:Importer
	{
		public SymbolSet()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			// an enum is overkill here...
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			SQUARE, HEX
		}
		/// <returns> The most frequently occuring dimension for game pieces in this module.
		/// </returns>
		virtual public System.Drawing.Size ModalSize
		{
			get
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				final HashMap < Dimension, Integer > histogram = 
				new HashMap < Dimension, Integer >();
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(SymbolData piece: gamePieceData)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'im '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Bitmap im = piece.getImage();
					//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Size d = new System.Drawing.Size(im.Width, im.Height);
					//UPGRADE_NOTE: Final was removed from the declaration of 'i '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Int32 i = histogram.get_Renamed(d);
					//UPGRADE_TODO: The 'System.Int32' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
					histogram.put(d, i == null?1:i + 1);
				}
				
				int max = 0;
				//UPGRADE_NOTE: Final was removed from the declaration of 'maxDim '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size maxDim = new System.Drawing.Size(0, 0);
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Map.Entry < Dimension, Integer > e: histogram.entrySet())
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Size d = e.getKey();
					//UPGRADE_NOTE: Final was removed from the declaration of 'n '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int n = e.getValue();
					
					if (n > max)
					{
						max = n;
						maxDim.Height = d.Height;
						maxDim.Width = d.Width;
					}
				}
				
				return maxDim;
			}
			
		}
		/// <returns> Map board symbol size corresponding to the current zoom level.
		/// </returns>
		virtual internal int MapBoardSymbolSize
		{
			get
			{
				return mapBoardSymbolSize[zoomLevel];
			}
			
		}
		/// <returns> Hex or square?
		/// </returns>
		virtual internal Shape MapBoardSymbolShape
		{
			get
			{
				return symbolShape;
			}
			
		}
		
		private const int OLD_SYMBOL_SET_FORMAT = 0xFD;
		
		/// <summary> Shape of the terrain elements.</summary>
		internal enum_Renamed Shape;
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SymbolData' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/// <summary> Contains all of the information for a single game piece or terrain icon</summary>
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		public class SymbolData
		{
			private void  InitBlock(SymbolSet enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SymbolSet enclosingInstance;
			/// <summary> Get the mask associated with this symbol. Returns <code>null</code> it this
			/// is a mask, if it is from an older version file, or if there is no mask.
			/// </summary>
			virtual protected internal SymbolData Mask
			{
				get
				{
					if (Enclosing_Instance.ignoreMask || isMask)
						return null;
					// mask index is base 1
					else if (maskIndex > 0 && maskIndex <= Enclosing_Instance.maskData.Length)
						return Enclosing_Instance.maskData[maskIndex - 1];
					else
						return null;
				}
				
			}
			/// <summary> Returns the archive file name corresponding to the image for this
			/// symbol. If called, will actually write the image to the archive in
			/// order to get the file name itself.
			/// 
			/// </summary>
			/// <throws>  IOException if unable to read the image files for this symbol. </throws>
			virtual internal System.String FileName
			{
				get
				{
					if (fileName == null)
						writeToArchive();
					return fileName;
				}
				
			}
			/// <summary> Returns true if all alpha values are zero.</summary>
			virtual internal bool Transparent
			{
				get
				{
					//UPGRADE_TODO: The 'System.Boolean' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
					if (transparent == null)
					{
						System.Drawing.Bitmap image = getImage();
						transparent = true;
						for (int i = 0; i < image.Width; ++i)
						{
							for (int j = 0; j < image.Height; ++j)
							{
								if (image.GetPixel(i, j).ToArgb() != 0)
								{
									transparent = false;
									//UPGRADE_NOTE: Labeled break statement was changed to a goto statement. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1012'"
									goto search_brk;
								}
							}
						}
						//UPGRADE_NOTE: Label 'search_brk' was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1011'"
search_brk: ;
						
					}
					return transparent;
				}
				
			}
			public SymbolSet Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			/// <summary> Shared bitmap of all symbols in either the terrain or game piece set.
			/// Cannot be static as there are three different possible shared images
			/// corresponding to game pieces, terrain features, and bitmasks.
			/// </summary>
			//UPGRADE_NOTE: Final was removed from the declaration of 'bitmap '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private System.Drawing.Bitmap bitmap;
			
			/// <summary> Actual name of image file for this symbol in archive. May not be the same as the name
			/// provided in the configuration file if duplicates exist.
			/// </summary>
			private System.String fileName;
			
			/// <summary> Actual image which is lazily generated on request.</summary>
			private System.Drawing.Bitmap img;
			
			/// <summary> Prevents infinite loops when applying masks to symbol images.</summary>
			//UPGRADE_NOTE: Final was removed from the declaration of 'isMask '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private bool isMask;
			
			/// <summary> Base 1 index into SymbolData array of masks. 0 means no mask.</summary>
			private int maskIndex;
			
			/// <summary> Actual name given in the configuration file--not the archive file name
			/// as duplicates are permitted in ADC2.
			/// </summary>
			private System.String name;
			
			/// <summary> Rectangle in shared bitmap <code>img</code>.</summary>
			private System.Drawing.Rectangle rect;
			
			/// <summary> <code>Boolean.TRUE</code> if the image is completely transparent (invisible)
			/// and <code>null</code> if it has not yet been checked.
			/// </summary>
			private System.Boolean transparent;
			
			internal SymbolData(SymbolSet enclosingInstance, System.Drawing.Bitmap bitmap, bool isMask)
			{
				InitBlock(enclosingInstance);
				this.bitmap = bitmap;
				this.isMask = isMask;
			}
			
			/// <summary> Read symbol data from configuration file.
			/// 
			/// </summary>
			/// <returns>   <code>this</code>, so that methods may be chained in series
			/// following <code>read</code>.
			/// </returns>
			//UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
			protected internal virtual SymbolData read(System.IO.BinaryReader in_Renamed)
			{
				name = readNullTerminatedString(in_Renamed);
				
				// Have to read in all the zoom level sizes for the first one
				// so that zooming in VASSAL can approximate the zoom behaviour of the
				// imported module.
				bool readAllZoomLevels = false;
				if (Enclosing_Instance.mapBoardSymbolSize == null)
				{
					Enclosing_Instance.mapBoardSymbolSize = new int[3];
					readAllZoomLevels = true;
				}
				
				// Alternate indexing style (to be implemented):
				// if (header == -3)
				//    pict.maskIndex = in.readUnsignedByte();
				// else
				maskIndex = Enclosing_Instance.header == VassalSharp.tools.imports.adc2.SymbolSet.OLD_SYMBOL_SET_FORMAT?in_Renamed.ReadByte():ADC2Utils.readBase250Word(in_Renamed);
				for (int i = 0; i < 3; ++i)
				{
					int x1 = in_Renamed.ReadInt32();
					int y1 = in_Renamed.ReadInt32();
					int x2 = in_Renamed.ReadInt32();
					int y2 = in_Renamed.ReadInt32();
					int width = x2 - x1 + 1;
					int height = y2 - y1 + 1;
					if (readAllZoomLevels)
						Enclosing_Instance.mapBoardSymbolSize[i] = height; // or width--they should be square
					if (i == VassalSharp.tools.imports.adc2.SymbolSet.zoomLevel)
						rect = new System.Drawing.Rectangle(x1, y1, width, height);
				}
				return this;
			}
			
			/// <summary> Get image corresponding to this symbol. Generates the image and applies
			/// optional mask if not already done so.
			/// </summary>
			/// <param name="rect2">width and height are taken from this for otherwise invalid masks
			/// </param>
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			private System.Drawing.Bitmap getImage(ref System.Drawing.Rectangle rect2)
			{
				if (img == null)
				{
					if (isMask && (rect.Width <= 0 || rect.Height <= 0 || rect.Width + rect.X > bitmap.Width || rect.Height + rect.Y > bitmap.Height))
					{
						// Images with invalid masks appear to be completely transparent.
						// This is a hassle generating new ones all the time, but there's nothing
						// to say that the real mask can't be different sizes at every call,
						// and anything else seems like overkill -- so this is an ugly kludge.
						// Hopefully, this crime against nature doesn't happen very often.
						return new System.Drawing.Bitmap(rect2.Width, rect2.Height, (System.Drawing.Imaging.PixelFormat) System.Drawing.Imaging.PixelFormat.Format32bppArgb);
					}
					img = (System.Drawing.Bitmap) bitmap.Clone(new System.Drawing.Rectangle(rect.X, rect.Y, rect.Width, rect.Height), bitmap.PixelFormat);
					if (Mask != null)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'bi '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Drawing.Bitmap bi = new System.Drawing.Bitmap(rect.Width, rect.Height, (System.Drawing.Imaging.PixelFormat) System.Drawing.Imaging.PixelFormat.Format32bppArgb);
						//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bi);
						//UPGRADE_TODO: Method 'java.awt.Graphics2D.drawImage' was converted to 'System.Drawing.Graphics.DrawImage' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DdrawImage_javaawtimageBufferedImage_javaawtimageBufferedImageOp_int_int'"
						g.DrawImage(img, 0, 0);
						g.setComposite(AlphaComposite.DstAtop);
						//UPGRADE_TODO: Method 'java.awt.Graphics2D.drawImage' was converted to 'System.Drawing.Graphics.DrawImage' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGraphics2DdrawImage_javaawtimageBufferedImage_javaawtimageBufferedImageOp_int_int'"
						//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
						g.DrawImage(Mask.getImage(ref rect), 0, 0);
						img = bi;
					}
				}
				return img;
			}
			
			internal virtual System.Drawing.Bitmap getImage()
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				return getImage(ref rect);
			}
			
			/// <summary> Write symbol image to archive and return archive file name. Will only
			/// write to archive once.
			/// </summary>
			protected internal virtual void  writeToArchive()
			{
				// only gets written once. if filename is not null, then we've
				// already been written
				if (fileName == null)
				{
					// this condition is really just a failsafe check
					fileName = getUniqueImageFileName(name);
					//UPGRADE_NOTE: Final was removed from the declaration of 'out '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.IO.MemoryStream out_Renamed = new System.IO.MemoryStream();
					ImageIO.write(getImage(), "png", out_Renamed);
					GameModule.getGameModule().getArchiveWriter().addImage(fileName, SupportClass.ToSByteArray(out_Renamed.ToArray()));
				}
			}
		}
		
		// Permute negative of red band (doesn't matter which colour) to alpha band.
		// masks are originally black where alpha should be 1.0 and white where
		// alpha should be 0.0.
		//UPGRADE_NOTE: Final was removed from the declaration of 'THREE_BAND_MATRIX'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly float[][] THREE_BAND_MATRIX = new float[][]{new float[]{0.0f, 0.0f, 0.0f, 0.0f}, new float[]{0.0f, 0.0f, 0.0f, 0.0f}, new float[]{0.0f, 0.0f, 0.0f, 0.0f}, new float[]{- 1.0f, 0.0f, 0.0f, 255.0f}};
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'ONE_BAND_MATRIX'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly float[][] ONE_BAND_MATRIX = new float[][]{new float[]{0.0f, 0.0f}, new float[]{0.0f, 0.0f}, new float[]{0.0f, 0.0f}, new float[]{- 255.0f, 255.0f}};
		
		/// <summary> Convert a black-and-white bitmap to a mask image.</summary>
		private static System.Drawing.Bitmap generateAlphaMask(System.Drawing.Bitmap img)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'op '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'java.awt.image.BandCombineOp' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBandCombineOp'"
			BandCombineOp op;
			//UPGRADE_ISSUE: Method 'java.awt.image.SampleModel.getNumBands' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageSampleModelgetNumBands'"
			if (img.getNumBands() == 1)
			{
				//UPGRADE_ISSUE: Constructor 'java.awt.image.BandCombineOp.BandCombineOp' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBandCombineOp'"
				op = new BandCombineOp(ONE_BAND_MATRIX, null);
			}
			else
			{
				//UPGRADE_ISSUE: Constructor 'java.awt.image.BandCombineOp.BandCombineOp' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBandCombineOp'"
				op = new BandCombineOp(THREE_BAND_MATRIX, null);
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'bi '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Bitmap bi = new System.Drawing.Bitmap(img.Width, img.Height, (System.Drawing.Imaging.PixelFormat) System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			//UPGRADE_ISSUE: Method 'java.awt.image.BandCombineOp.filter' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtimageBandCombineOp'"
			op.filter(img, bi);
			return bi;
		}
		
		/// <summary> Unit symbols</summary>
		private SymbolData[] gamePieceData;
		
		/// <summary> Terrain symbols</summary>
		private SymbolData[] mapBoardData;
		
		/// <summary> Symbol size for all three zoom levels. Used to set zoom scale in VassalSharp.</summary>
		private int[] mapBoardSymbolSize;
		
		/// <summary> Mask symbols. Only used by SymbolData when requesting a mask index.
		/// Doesn't get used at all in Version I sets (where <code>ignoreMask</code> is true).
		/// </summary>
		private SymbolData[] maskData = null;
		
		/// <summary> Hex or square.</summary>
		private Shape symbolShape;
		
		/// <summary> Zoom level to import</summary>
		private const int zoomLevel = 2;
		
		/// <summary> Ignore mask despite specified mask indeces.  True for older configuration files.</summary>
		private bool ignoreMask;
		
		private bool isCardSet;
		
		private int header;
		
		internal System.Drawing.Bitmap underlay;
		
		/// <summary> Read symbol images based on basename and suffix. Bitmap filenames are of the form
		/// <tt>name + "-CN.bmp"</tt> where C is the specified suffix ('M' for masks; 'U' for
		/// game pieces; 'T' for terrain symbols) and N is the base-1 zoom level.
		/// 
		/// </summary>
		/// <param name="filename">base file name. Should be the same as the base file name of the
		/// symbol set file.
		/// </param>
		/// <param name="suffix">  single character suffix
		/// </param>
		internal virtual System.Drawing.Bitmap loadSymbolImage(System.String filename, char suffix, bool queryIfNotFound)
		{
			System.String fn;
			if (suffix == '\x0000')
				fn = filename + (zoomLevel + 1) + ".bmp";
			else
				fn = filename + '-' + suffix + (zoomLevel + 1) + ".bmp";
			System.IO.FileInfo f = action.getCaseInsensitiveFile(new System.IO.FileInfo(fn), null, queryIfNotFound, new BMPFileFilter());
			if (f == null && queryIfNotFound)
			{
				throw new System.IO.FileNotFoundException("Missing bitmap file: " + fn);
			}
			else if (f == null)
			{
				return null;
			}
			else
			{
				return ImageIO.read(f);
			}
		}
		
		internal virtual System.Drawing.Bitmap loadSymbolImage(System.String filename, char suffix)
		{
			return loadSymbolImage(filename, suffix, true);
		}
		
		/// <summary> Read dimensions from input file. The dimensions must occur in triplets in the
		/// configuration file--each one corresponding to a zoom level.
		/// Only the third dimension, corresponding to zoom level 3, is returned.
		/// </summary>
		//UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
		internal virtual System.Drawing.Size readDimension(System.IO.BinaryReader in_Renamed)
		{
			System.Drawing.Size d = System.Drawing.Size.Empty;
			for (int i = 0; i < 3; ++i)
			{
				int width = in_Renamed.ReadInt32();
				int height = in_Renamed.ReadInt32();
				if (zoomLevel == i)
					d = new System.Drawing.Size(width, height);
			}
			return d;
		}
		
		/// <summary> Returns the <code>SymbolData</code> corresponding to the game piece at
		/// the specified index.
		/// 
		/// </summary>
		/// <returns>      <code>null</code> if the index is out of bounds.
		/// </returns>
		internal virtual SymbolData getGamePiece(int index)
		{
			if (index >= 0 && index < gamePieceData.Length)
				return gamePieceData[index];
			else
				return null;
		}
		
		/// <summary> Returns the <code>SymbolData</code> corresponding to the terrain symbol at
		/// the specified index.
		/// 
		/// </summary>
		/// <returns>      <code>null</code> if the index is out of bounds.
		/// </returns>
		internal virtual SymbolData getMapBoardSymbol(int index)
		{
			if (index >= 0 & index < mapBoardData.Length)
				return mapBoardData[index];
			else
				return null;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual System.Drawing.Size getMaxSize(ref System.Drawing.Size max)
		{
			if (max.IsEmpty)
				max = new System.Drawing.Size(0, 0);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(SymbolData piece: gamePieceData)
			{
				System.Drawing.Bitmap im = piece.getImage();
				if (im.Width > max.Width)
					max.Width = im.Width;
				if (im.Height > max.Height)
					max.Height = im.Height;
			}
			return max;
		}
		
		public virtual System.Drawing.Size getMaxSize()
		{
			System.Drawing.Size tempAux = System.Drawing.Size.Empty;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			return getMaxSize(ref tempAux);
		}
		
		/// <returns> Map board symbol size corresponding to the specified zoom level
		/// </returns>
		internal virtual double getZoomFactor(int zoomLevel)
		{
			return (double) mapBoardSymbolSize[zoomLevel] / (double) mapBoardSymbolSize[SymbolSet.zoomLevel];
		}
		
		/// <summary> Read a card set from the specified file.</summary>
		/// <throws>  IOException </throws>
		internal virtual void  importCardSet(ImportAction a, System.IO.FileInfo f)
		{
			isCardSet = true;
			importFile(a, f);
		}
		
		/// <summary> Read a symbol set from the specified file.</summary>
		protected internal virtual void  load(System.IO.FileInfo f)
		{
			base.load(f);
			//UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
			System.IO.BinaryReader in_Renamed = null;
			
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
				in_Renamed = new System.IO.BinaryReader(new System.IO.BufferedStream(new System.IO.FileStream(f.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read)));
				
				// if header is 0xFD, then mask indeces are one-byte long. Otherwise, if
				// the header is anything else greater than 0xFA, then mask indeces are base-250
				// two-byte words.
				header = in_Renamed.ReadByte();
				if (header < 0xFA)
					throw new FileFormatException("Invalid Symbol Set Header");
				
				// comletely overridden by the map file
				/* int orientation = */ in_Renamed.ReadByte(); // 1=vertical; 2=horizontal; 3=grid
				
				int mapStyle = (sbyte) in_Renamed.ReadByte(); // 0=grid; all other values=hex
				switch (mapStyle)
				{
					
					case 1: 
						symbolShape = Shape.HEX;
						break;
					
					default: 
						symbolShape = Shape.SQUARE;
						break;
					
				}
				
				int symSetVersion = (sbyte) in_Renamed.ReadByte(); // 1=version 1
				switch (symSetVersion)
				{
					
					case 0: 
						ignoreMask = false;
						break;
					
					default: 
						ignoreMask = true;
						break;
					
				}
				if (isCardSet)
					ignoreMask = true;
				
				// bitmap dimensions are completely ignored
				int nMapBoardSymbols = ADC2Utils.readBase250Word(in_Renamed);
				mapBoardData = new SymbolData[nMapBoardSymbols];
				/* terrainBitmapDims = */ readDimension(in_Renamed);
				
				int nGamePieceSymbols = ADC2Utils.readBase250Word(in_Renamed);
				gamePieceData = new SymbolData[nGamePieceSymbols];
				/* unitBitmapDims = */ readDimension(in_Renamed);
				
				int nMasks = ADC2Utils.readBase250Word(in_Renamed);
				/* maskBitmapDims = */ readDimension(in_Renamed);
				maskData = new SymbolData[nMasks];
				
				System.String baseName = stripExtension(f.FullName);
				
				// load images
				System.Drawing.Bitmap mapBoardImages = null;
				if (!isCardSet)
					mapBoardImages = loadSymbolImage(baseName, 't');
				for (int i = 0; i < nMapBoardSymbols; ++i)
				{
					mapBoardData[i] = new SymbolData(this, mapBoardImages, false).read(in_Renamed);
					// check for size consistency. Not sure what to do if they're not
					// all the same size or not square
					if (!isCardSet)
					{
						if (mapBoardData[i].rect.Height != mapBoardData[0].rect.Height || mapBoardData[i].rect.Width != mapBoardData[0].rect.Width)
							throw new FileFormatException("Map board image dimensions are inconsistent");
						if (mapBoardData[i].rect.Width != mapBoardData[i].rect.Height)
							throw new FileFormatException("Map board image dimensions are not square");
					}
				}
				
				System.Drawing.Bitmap gamePieceImages;
				if (isCardSet)
				{
					gamePieceImages = loadSymbolImage(baseName);
				}
				else
				{
					gamePieceImages = loadSymbolImage(baseName, 'u');
				}
				
				for (int i = 0; i < nGamePieceSymbols; ++i)
				{
					gamePieceData[i] = new SymbolData(this, gamePieceImages, false).read(in_Renamed);
				}
				
				if (!ignoreMask)
				{
					System.Drawing.Bitmap maskImages = loadSymbolImage(baseName, 'm');
					// convert binary bitmap to RGBA alpha mask
					maskImages = generateAlphaMask(maskImages);
					
					for (int i = 0; i < nMasks; ++i)
						maskData[i] = new SymbolData(this, maskImages, true).read(in_Renamed);
				}
				
				//UPGRADE_TODO: Method 'java.io.FilterInputStream.close' was converted to 'System.IO.BinaryReader.Close' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFilterInputStreamclose'"
				in_Renamed.Close();
				
				/* See if there is a single-image underlay for the map. */
				underlay = loadSymbolImage(baseName, 'z', false);
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
			
			readPermutationFile(f);
		}
		
		private System.Drawing.Bitmap loadSymbolImage(System.String string_Renamed)
		{
			return loadSymbolImage(string_Renamed, '\x0000');
		}
		
		/// <summary> Read an SDX file if one exists. This is a list of image indeces starting with terrain
		/// separated by newlines. Only piece images are actually permuted.
		/// 
		/// </summary>
		/// <param name="f">- Set file.
		/// </param>
		/// <throws>  IOException </throws>
		protected internal virtual void  readPermutationFile(System.IO.FileInfo f)
		{
			System.IO.FileInfo sdx = new File(forceExtension(f.FullName, "sdx"));
			sdx = action.getCaseInsensitiveFile(sdx, f, false, null);
			if (sdx != null)
			{
				// must reorder image indeces
				System.IO.StreamReader input = null;
				
				try
				{
					//UPGRADE_TODO: The differences in the expected value  of parameters for constructor 'java.io.BufferedReader.BufferedReader'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
					//UPGRADE_TODO: Constructor 'java.io.FileReader.FileReader' was converted to 'System.IO.StreamReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					input = new System.IO.StreamReader(new System.IO.StreamReader(sdx.FullName, System.Text.Encoding.Default).BaseStream, new System.IO.StreamReader(sdx.FullName, System.Text.Encoding.Default).CurrentEncoding);
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'pieces '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SymbolData[] pieces = ArrayUtils.copyOf(gamePieceData);
					
					System.String line = null;
					try
					{
						for (int i = 0; i < mapBoardData.Length; ++i)
						{
							line = input.ReadLine();
						}
						for (int i = 0; i < pieces.Length; ++i)
						{
							line = input.ReadLine();
							int idx = System.Int32.Parse(line);
							pieces[i] = gamePieceData[idx - 1];
						}
					}
					catch (System.IO.EndOfStreamException e)
					{
					}
					catch (System.IndexOutOfRangeException e)
					{
						throw new FileFormatException("SDX file has out-of-bounds index \"" + line + "\".");
					}
					catch (System.FormatException e)
					{
						throw new FileFormatException("SDX file has invalid index \"" + line + "\".");
					}
					finally
					{
						gamePieceData = pieces;
					}
					
					input.Close();
				}
				finally
				{
					IOUtils.closeQuietly(input);
				}
			}
		}
		
		/// <summary> Write all of the game pieces to the archive.  Mainly for testing or if only
		/// the symbol set is imported.
		/// </summary>
		public virtual void  writeToArchive()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(SymbolData piece: gamePieceData) 
			piece.writeToArchive();
			// for testing purposes only
			//    for (SymbolData terrain : mapBoardData)
			//      terrain.writeToArchive();
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public virtual bool isValidImportFile(System.IO.FileInfo f)
		{
			//UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
			System.IO.BinaryReader in_Renamed = null;
			try
			{
				//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
				in_Renamed = new System.IO.BinaryReader(new System.IO.FileStream(f.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read));
				bool valid = in_Renamed.ReadByte() >= 0xFA;
				//UPGRADE_TODO: Method 'java.io.FilterInputStream.close' was converted to 'System.IO.BinaryReader.Close' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFilterInputStreamclose'"
				in_Renamed.Close();
				return valid;
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
	}
}