/*
* $Id$
*
* Copyright (c) 2009 by Brent Easton
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
using IOUtils = VassalSharp.tools.io.IOUtils;
namespace VassalSharp.tools
{
	
	/// <summary> Some general purpose CRC utilities.
	/// 
	/// </summary>
	/// <author>  Brent Easton
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class CRCUtils
	{
		public CRCUtils()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'crc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'java.util.zip.CRC32' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipCRC32'"
			//UPGRADE_ISSUE: Constructor 'java.util.zip.CRC32.CRC32' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipCRC32'"
			CRC32 crc = new CRC32();
			//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] buffer = new sbyte[DEFAULT_BUFFER_SIZE];
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(File file: files)
			{
				buildCRC(file, crc, buffer);
			}
			//UPGRADE_ISSUE: Method 'java.util.zip.CRC32.getValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipCRC32'"
			return crc.getValue();
		}
		
		public const int DEFAULT_BUFFER_SIZE = 8192;
		
		/// <summary> Calculate a cumulative CRC over a series of files
		/// 
		/// NOTE: It is up to the calling routine to ensure that the order of
		/// Files in the list is consistent accross all platforms.
		/// 
		/// </summary>
		/// <param name="files">List of files
		/// </param>
		/// <returns> CRC
		/// </returns>
		/// <throws>  IOException </throws>
		public static long getCRC;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(List < File > files) throws IOException
		
		/// <summary> Internal routine to accumulate a CRC over a single file
		/// 
		/// </summary>
		/// <param name="file">File
		/// </param>
		/// <param name="crc">CRC32 to accumulate
		/// </param>
		/// <param name="bytes">size of File (out)
		/// </param>
		/// <param name="buffer">read buffer
		/// </param>
		/// <throws>  IOException </throws>
		//UPGRADE_ISSUE: Class 'java.util.zip.CRC32' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipCRC32'"
		private static void  buildCRC(System.IO.FileInfo file, CRC32 crc, sbyte[] buffer)
		{
			//UPGRADE_TODO: Constructor 'java.io.FileInputStream.FileInputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileInputStreamFileInputStream_javaioFile'"
			System.IO.Stream in_Renamed = new System.IO.FileStream(file.FullName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
			try
			{
				buildCRC(in_Renamed, crc, buffer);
			}
			finally
			{
				IOUtils.closeQuietly(in_Renamed);
			}
		}
		
		/// <summary> Internal routine to accumulate a CRC over a single InputStream
		/// 
		/// </summary>
		/// <param name="in">InputStream
		/// </param>
		/// <param name="crc">CRC32 to accumulate
		/// </param>
		/// <param name="bytes">number of bytes read
		/// </param>
		/// <param name="buffer">read buffer
		/// </param>
		/// <throws>  IOException </throws>
		//UPGRADE_ISSUE: Class 'java.util.zip.CRC32' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipCRC32'"
		private static void  buildCRC(System.IO.Stream in_Renamed, CRC32 crc, sbyte[] buffer)
		{
			
			int count;
			while ((count = in_Renamed is VassalSharp.tools.image.PNGChunkSkipInputStream?((VassalSharp.tools.image.PNGChunkSkipInputStream) in_Renamed).read(buffer):SupportClass.ReadInput(in_Renamed, buffer, 0, buffer.Length)) >= 0)
			{
				//UPGRADE_ISSUE: Method 'java.util.zip.CRC32.update' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javautilzipCRC32'"
				crc.update(buffer, 0, count);
			}
		}
	}
}