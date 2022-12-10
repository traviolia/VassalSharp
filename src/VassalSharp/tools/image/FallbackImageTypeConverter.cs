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
//UPGRADE_TODO: The type 'org.apache.commons.lang.SystemUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SystemUtils = org.apache.commons.lang.SystemUtils;
//UPGRADE_TODO: The type 'org.slf4j.Logger' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Logger = org.slf4j.Logger;
//UPGRADE_TODO: The type 'org.slf4j.LoggerFactory' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LoggerFactory = org.slf4j.LoggerFactory;
using TemporaryFileFactory = VassalSharp.tools.io.TemporaryFileFactory;
using Reference = VassalSharp.tools.lang.Reference;
namespace VassalSharp.tools.image
{
	
	/// <summary> Convert a {@link BufferedImage} to a different type, falling back to
	/// conversion on disk if convertion in memory fails.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class FallbackImageTypeConverter : ImageTypeConverter
	{
		private void  InitBlock()
		{
			/*
			* Having an OutOfMemoryException while converting in memory is
			* apparently catastrophic on Apple's Java 6 JVM (and possibly also
			* on their Java 5 JVM as well). In-memory tiling also uses far more
			* memory than it should on Apple's Java 6 JVM due to
			* Graphics2D.drawImage making an intermediate copy of the image data.
			* Hence, we ensure that when using Java 5 or 6 on Mac OS X, we never
			* try in-memory conversion for images which can't comfortably have
			* three copies existing simultaneously in memory.
			*/
			return !SystemUtils.IS_OS_MAC_OSX || (!SystemUtils.IS_JAVA_1_6 && !SystemUtils.IS_JAVA_1_5) || 4 * ref_Renamed.obj.getHeight() * ref_Renamed.obj.getWidth() <= System.Diagnostics.Process.GetCurrentProcess().maxMemory() / 4;
			if (tryConvertingInMemory(ref_Renamed))
			{
				try
				{
					return memory_converter.convert(ref_Renamed, type);
				}
				catch (System.OutOfMemoryException e)
				{
					// This is ok, we just don't have enough free heap for the conversion.
					logger.info("Switching to FileImageTypeConverter...");
				}
			}
			
			// Try converting on disk instead.
			return file_converter.convert(ref_Renamed, type);
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'logger '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'logger' was moved to static method 'VassalSharp.tools.image.FallbackImageTypeConverter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private static readonly Logger logger;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'tfactory '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal TemporaryFileFactory tfactory;
		//UPGRADE_NOTE: Final was removed from the declaration of 'memory_converter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal ImageTypeConverter memory_converter;
		//UPGRADE_NOTE: Final was removed from the declaration of 'file_converter '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal ImageTypeConverter file_converter;
		
		/// <summary> Create a converter.
		/// 
		/// </summary>
		/// <param name="tfactory">the temporary file factory
		/// </param>
		public FallbackImageTypeConverter(TemporaryFileFactory tfactory):this(tfactory, new MemoryImageTypeConverter(), new FileImageTypeConverter(tfactory))
		{
		}
		
		/// <summary> Create a converter.
		/// 
		/// </summary>
		/// <param name="tfactory">the temporary file factory
		/// </param>
		/// <param name="memory_converter">the in-memory image converter
		/// </param>
		/// <param name="file_converter">the on-disk image converter
		/// </param>
		internal FallbackImageTypeConverter(TemporaryFileFactory tfactory, ImageTypeConverter memory_converter, ImageTypeConverter file_converter)
		{
			InitBlock();
			this.tfactory = tfactory;
			this.memory_converter = memory_converter;
			this.file_converter = file_converter;
		}
		
		private bool tryConvertingInMemory;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Reference < BufferedImage > ref)
		
		/// <summary>{@inheritDoc} </summary>
		public System.Drawing.Bitmap convert;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Reference < BufferedImage > ref, int type) 
		throws ImageIOException
		static FallbackImageTypeConverter()
		{
			logger = LoggerFactory.getLogger(typeof(FallbackImageTypeConverter));
		}
	}
}