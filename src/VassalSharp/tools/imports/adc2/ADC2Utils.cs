/*
* Copyright (c) 2007 by Michael Kiefte
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
using FileFormatException = VassalSharp.tools.imports.FileFormatException;
namespace VassalSharp.tools.imports.adc2
{
	
	/// <summary> Common utilities for importing ADC2 modules to VassalSharp.
	/// 
	/// </summary>
	/// <author>  Michael Kiefte
	/// 
	/// </author>
	
	public class ADC2Utils
	{
		
		[Serializable]
		public class NoMoreBlocksException:System.IO.EndOfStreamException
		{
			private const long serialVersionUID = 1L;
			
			internal NoMoreBlocksException(System.String name):base(name)
			{
			}
		}
		
		public const System.String MODULE_EXTENSION = ".ops";
		public const System.String MAP_EXTENSION = ".map";
		public const System.String SET_EXTENSION = ".set";
		
		public const System.String MODULE_DESCRIPTION = "ADC2 Game Module";
		public const System.String MAP_DESCRIPTION = "ADC2 Map Board";
		public const System.String SET_DESCRIPTION = "ADC2 Symbol Set";
		internal const System.String TYPE = "Type";
		
		// ADCs default pallet which is referenced by index.
		//UPGRADE_NOTE: Final was removed from the declaration of 'defaultColorPallet '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Constructor 'java.awt.Color.Color' was converted to 'System.Drawing.Color.FromArgb' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtColorColor_int'"
		public static readonly System.Drawing.Color[] defaultColorPallet = new System.Drawing.Color[]{System.Drawing.Color.FromArgb(0x000000), System.Drawing.Color.FromArgb(0x808080), System.Drawing.Color.FromArgb(0x800000), System.Drawing.Color.FromArgb(0x808000), System.Drawing.Color.FromArgb(0x008000), System.Drawing.Color.FromArgb(0x008080), System.Drawing.Color.FromArgb(0x000080), System.Drawing.Color.FromArgb(0x800080), System.Drawing.Color.FromArgb(0x808040), System.Drawing.Color.FromArgb(0x004040), System.Drawing.Color.FromArgb(0x0080ff), System.Drawing.Color.FromArgb(0x004080), System.Drawing.Color.FromArgb(0x4000ff), System.Drawing.Color.FromArgb(0x804000), System.Drawing.Color.FromArgb(0xffffff), System.Drawing.Color.FromArgb(0xc0c0c0), System.Drawing.Color.FromArgb(0xff0000), System.Drawing.Color.FromArgb(0xffff00), System.Drawing.Color.FromArgb(0x00ff00), System.Drawing.Color.FromArgb(0x00ffff), System.Drawing.Color.FromArgb(0x0000ff), System.Drawing.Color.FromArgb(0xff00ff), System.Drawing.Color.FromArgb(0xffff80), System.Drawing.Color.FromArgb(0x00ff80), System.Drawing.Color.FromArgb(0x80ffff), System.Drawing.Color.FromArgb(0x8080ff), System.Drawing.Color.FromArgb(0xff0080), System.Drawing.Color.FromArgb(0xff8040), System.Drawing.Color.FromArgb(0x010101), System.Drawing.Color.FromArgb(0x0e0e0e), System.Drawing.Color.FromArgb(0x1c1c1c), System.Drawing.Color.FromArgb(0x2a2a2a), System.Drawing.Color.FromArgb(0x383838), System.Drawing.Color.FromArgb(0x464646), System.Drawing.Color.FromArgb(0x545454), System.Drawing.Color.FromArgb(0x626262), System.Drawing.Color.FromArgb(0x707070), System.Drawing.Color.FromArgb(0x7e7e7e), System.Drawing.Color.FromArgb(0x8c8c8c), System.Drawing.Color.FromArgb(0x9a9a9a), System.Drawing.Color.FromArgb(0xa8a8a8), System.Drawing.Color.FromArgb(0xb6b6b6), System.Drawing.Color.FromArgb(0xc4c4c4), System.Drawing.Color.FromArgb(0xd2d2d2), System.Drawing.Color.FromArgb(0xe0e0e0), System.Drawing.Color.FromArgb(0xeeeeee), System.Drawing.Color.FromArgb(0x330000), 
			System.Drawing.Color.FromArgb(0x660000), System.Drawing.Color.FromArgb(0x990000), System.Drawing.Color.FromArgb(0xcc0000), System.Drawing.Color.FromArgb(0xc1441a), System.Drawing.Color.FromArgb(0x003300), System.Drawing.Color.FromArgb(0x333300), System.Drawing.Color.FromArgb(0x663300), System.Drawing.Color.FromArgb(0x993300), System.Drawing.Color.FromArgb(0xcc3300), System.Drawing.Color.FromArgb(0xff3300), System.Drawing.Color.FromArgb(0x006600), System.Drawing.Color.FromArgb(0x336600), System.Drawing.Color.FromArgb(0x666600), System.Drawing.Color.FromArgb(0x996600), System.Drawing.Color.FromArgb(0xcc6600), System.Drawing.Color.FromArgb(0xff6600), System.Drawing.Color.FromArgb(0x009900), System.Drawing.Color.FromArgb(0x339900), System.Drawing.Color.FromArgb(0x669900), System.Drawing.Color.FromArgb(0x999900), System.Drawing.Color.FromArgb(0xcc9900), System.Drawing.Color.FromArgb(0xff9900), System.Drawing.Color.FromArgb(0x00cc00), System.Drawing.Color.FromArgb(0x33cc00), System.Drawing.Color.FromArgb(0x66cc00), System.Drawing.Color.FromArgb(0x99cc00), System.Drawing.Color.FromArgb(0xcccc00), System.Drawing.Color.FromArgb(0xffcc00), System.Drawing.Color.FromArgb(0x00ea00), System.Drawing.Color.FromArgb(0x33ff00), System.Drawing.Color.FromArgb(0x66ff00), System.Drawing.Color.FromArgb(0x99ff00), System.Drawing.Color.FromArgb(0xccff00), System.Drawing.Color.FromArgb(0xffff00), System.Drawing.Color.FromArgb(0x000033), System.Drawing.Color.FromArgb(0x330033), System.Drawing.Color.FromArgb(0x660033), System.Drawing.Color.FromArgb(0x990033), System.Drawing.Color.FromArgb(0xcc0033), System.Drawing.Color.FromArgb(0xff0033), System.Drawing.Color.FromArgb(0x003333), System.Drawing.Color.FromArgb(0x663333), System.Drawing.Color.FromArgb(0x993333), System.Drawing.Color.FromArgb(0xcc3333), System.Drawing.Color.FromArgb(0xff3333), System.Drawing.Color.FromArgb(0x006633), System.Drawing.Color.FromArgb(0x336633), System.Drawing.Color.FromArgb(0x666633), System.Drawing.Color.FromArgb(0x996633), System.Drawing.Color.
			FromArgb(0xcc6633), System.Drawing.Color.FromArgb(0xff6633), System.Drawing.Color.FromArgb(0x009933), System.Drawing.Color.FromArgb(0x339933), System.Drawing.Color.FromArgb(0x669933), System.Drawing.Color.FromArgb(0x999933), System.Drawing.Color.FromArgb(0xcc9933), System.Drawing.Color.FromArgb(0xff9933), System.Drawing.Color.FromArgb(0x00cc33), System.Drawing.Color.FromArgb(0x33cc33), System.Drawing.Color.FromArgb(0x66cc33), System.Drawing.Color.FromArgb(0x99cc33), System.Drawing.Color.FromArgb(0xcccc33), System.Drawing.Color.FromArgb(0xffcc33), System.Drawing.Color.FromArgb(0x00ff33), System.Drawing.Color.FromArgb(0x33ff33), System.Drawing.Color.FromArgb(0x66ff33), System.Drawing.Color.FromArgb(0x99ff33), System.Drawing.Color.FromArgb(0xccff33), System.Drawing.Color.FromArgb(0xffff33), System.Drawing.Color.FromArgb(0x000066), System.Drawing.Color.FromArgb(0x330066), System.Drawing.Color.FromArgb(0x660066), System.Drawing.Color.FromArgb(0x990066), System.Drawing.Color.FromArgb(0xcc0066), System.Drawing.Color.FromArgb(0xff0066), System.Drawing.Color.FromArgb(0x003366), System.Drawing.Color.FromArgb(0x333366), System.Drawing.Color.FromArgb(0x663366), System.Drawing.Color.FromArgb(0x993366), System.Drawing.Color.FromArgb(0xcc3366), System.Drawing.Color.FromArgb(0xff3366), System.Drawing.Color.FromArgb(0x006666), System.Drawing.Color.FromArgb(0x336666), System.Drawing.Color.FromArgb(0x996666), System.Drawing.Color.FromArgb(0xcc6666), System.Drawing.Color.FromArgb(0xff6666), System.Drawing.Color.FromArgb(0x009966), System.Drawing.Color.FromArgb(0x339966), System.Drawing.Color.FromArgb(0x669966), System.Drawing.Color.FromArgb(0x999966), System.Drawing.Color.FromArgb(0xcc9966), System.Drawing.Color.FromArgb(0xff9966), System.Drawing.Color.FromArgb(0x00cc66), System.Drawing.Color.FromArgb(0x33cc66), System.Drawing.Color.FromArgb(0x66cc66), System.Drawing.Color.FromArgb(0x99cc66), System.Drawing.Color.FromArgb(0xcccc66), System.Drawing.Color.FromArgb(0xffcc66), System.Drawing.Color.FromArgb(0x00ff66), 
			System.Drawing.Color.FromArgb(0x33ff66), System.Drawing.Color.FromArgb(0x66ff66), System.Drawing.Color.FromArgb(0x99ff66), System.Drawing.Color.FromArgb(0xccff66), System.Drawing.Color.FromArgb(0xffff66), System.Drawing.Color.FromArgb(0x000099), System.Drawing.Color.FromArgb(0x330099), System.Drawing.Color.FromArgb(0x660099), System.Drawing.Color.FromArgb(0x990099), System.Drawing.Color.FromArgb(0xcc0099), System.Drawing.Color.FromArgb(0xff0099), System.Drawing.Color.FromArgb(0x003399), System.Drawing.Color.FromArgb(0x333399), System.Drawing.Color.FromArgb(0x663399), System.Drawing.Color.FromArgb(0x993399), System.Drawing.Color.FromArgb(0xcc3399), System.Drawing.Color.FromArgb(0xff3399), System.Drawing.Color.FromArgb(0x006699), System.Drawing.Color.FromArgb(0x336699), System.Drawing.Color.FromArgb(0x666699), System.Drawing.Color.FromArgb(0x996699), System.Drawing.Color.FromArgb(0xcc6699), System.Drawing.Color.FromArgb(0xff6699), System.Drawing.Color.FromArgb(0x009999), System.Drawing.Color.FromArgb(0x339999), System.Drawing.Color.FromArgb(0x669999), System.Drawing.Color.FromArgb(0xcc9999), System.Drawing.Color.FromArgb(0xff9999), System.Drawing.Color.FromArgb(0x00cc99), System.Drawing.Color.FromArgb(0x33cc99), System.Drawing.Color.FromArgb(0x66cc99), System.Drawing.Color.FromArgb(0x99cc99), System.Drawing.Color.FromArgb(0xcccc99), System.Drawing.Color.FromArgb(0xffcc99), System.Drawing.Color.FromArgb(0x00ff99), System.Drawing.Color.FromArgb(0x33ff99), System.Drawing.Color.FromArgb(0x66ff99), System.Drawing.Color.FromArgb(0x99ff99), System.Drawing.Color.FromArgb(0xccff99), System.Drawing.Color.FromArgb(0xffff99), System.Drawing.Color.FromArgb(0x0000cc), System.Drawing.Color.FromArgb(0x3300cc), System.Drawing.Color.FromArgb(0x6600cc), System.Drawing.Color.FromArgb(0x9900cc), System.Drawing.Color.FromArgb(0xcc00cc), System.Drawing.Color.FromArgb(0xff00cc), System.Drawing.Color.FromArgb(0x0033cc), System.Drawing.Color.FromArgb(0x3333cc), System.Drawing.Color.FromArgb(0x6633cc), System.Drawing.Color.
			FromArgb(0x9933cc), System.Drawing.Color.FromArgb(0xcc33cc), System.Drawing.Color.FromArgb(0xff33cc), System.Drawing.Color.FromArgb(0x0066cc), System.Drawing.Color.FromArgb(0x3366cc), System.Drawing.Color.FromArgb(0x6666cc), System.Drawing.Color.FromArgb(0x9966cc), System.Drawing.Color.FromArgb(0xcc66cc), System.Drawing.Color.FromArgb(0xff66cc), System.Drawing.Color.FromArgb(0x0099cc), System.Drawing.Color.FromArgb(0x3399cc), System.Drawing.Color.FromArgb(0x6699cc), System.Drawing.Color.FromArgb(0x9999cc), System.Drawing.Color.FromArgb(0xcc99cc), System.Drawing.Color.FromArgb(0xff99cc), System.Drawing.Color.FromArgb(0x00cccc), System.Drawing.Color.FromArgb(0x33cccc), System.Drawing.Color.FromArgb(0x66cccc), System.Drawing.Color.FromArgb(0x99cccc), System.Drawing.Color.FromArgb(0xffcccc), System.Drawing.Color.FromArgb(0x00ffcc), System.Drawing.Color.FromArgb(0x33ffcc), System.Drawing.Color.FromArgb(0x66ffcc), System.Drawing.Color.FromArgb(0x99ffcc), System.Drawing.Color.FromArgb(0xccffcc), System.Drawing.Color.FromArgb(0xffffcc), System.Drawing.Color.FromArgb(0x0000ff), System.Drawing.Color.FromArgb(0x3300ff), System.Drawing.Color.FromArgb(0x6600ff), System.Drawing.Color.FromArgb(0x9900ff), System.Drawing.Color.FromArgb(0xcc00ff), System.Drawing.Color.FromArgb(0xff00ff), System.Drawing.Color.FromArgb(0x0033ff), System.Drawing.Color.FromArgb(0x3333ff), System.Drawing.Color.FromArgb(0x6633ff), System.Drawing.Color.FromArgb(0x9933ff), System.Drawing.Color.FromArgb(0xcc33ff), System.Drawing.Color.FromArgb(0xff33ff), System.Drawing.Color.FromArgb(0x0066ff), System.Drawing.Color.FromArgb(0x3366ff), System.Drawing.Color.FromArgb(0x6666ff), System.Drawing.Color.FromArgb(0x9966ff), System.Drawing.Color.FromArgb(0xcc66ff), System.Drawing.Color.FromArgb(0xff66ff), System.Drawing.Color.FromArgb(0x0099ff), System.Drawing.Color.FromArgb(0x3399ff), System.Drawing.Color.FromArgb(0x6699ff), System.Drawing.Color.FromArgb(0x9999ff), System.Drawing.Color.FromArgb(0xcc99ff), System.Drawing.Color.FromArgb(0xff99ff), 
			System.Drawing.Color.FromArgb(0x00ccff), System.Drawing.Color.FromArgb(0x33ccff), System.Drawing.Color.FromArgb(0x66ccff), System.Drawing.Color.FromArgb(0x99ccff), System.Drawing.Color.FromArgb(0xccccff), System.Drawing.Color.FromArgb(0xffccff), System.Drawing.Color.FromArgb(0x00ffff), System.Drawing.Color.FromArgb(0x33ffff), System.Drawing.Color.FromArgb(0x66ffff), System.Drawing.Color.FromArgb(0x99ffff), System.Drawing.Color.FromArgb(0xccffff)};
		
		// can never be instantiated
		private ADC2Utils()
		{
		}
		
		private const long serialVersionUID = 1L;
		
		/// <summary> Read a base-250 2-byte big-endian word from a <code>DataInputStream</code>.
		/// This is the default (and only) encoding for words imported modules.
		/// </summary>
		//UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
		internal static int readBase250Word(System.IO.BinaryReader in_Renamed)
		{
			return in_Renamed.ReadByte() * 250 + in_Renamed.ReadByte();
		}
		
		/// <summary> Read a base-250 4-byte big-endian word from a <code>DataInputStream</code>.
		/// This is the default encoding for integers in ADC2 modules (why?).
		/// </summary>
		//UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
		internal static int readBase250Integer(System.IO.BinaryReader in_Renamed)
		{
			return readBase250Word(in_Renamed) * 62500 + readBase250Word(in_Renamed);
		}
		
		internal const int BLOCK_SEPARATOR = - 2;
		
		/// <summary> Read a block separator byte from an import module file and throw an exception if it
		/// doesn't match.
		/// </summary>
		//UPGRADE_TODO: Class 'java.io.DataInputStream' was converted to 'System.IO.BinaryReader' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioDataInputStream'"
		internal static void  readBlockHeader(System.IO.BinaryReader in_Renamed, System.String string_Renamed)
		{
			try
			{
				int header = (sbyte) in_Renamed.ReadByte();
				if (header != BLOCK_SEPARATOR)
					throw new FileFormatException("Invalid " + string_Renamed + " block header.");
			}
			// FIXME: review error message
			catch (System.IO.EndOfStreamException e)
			{
				throw new NoMoreBlocksException(string_Renamed);
			}
			long available;
			available = in_Renamed.BaseStream.Length - in_Renamed.BaseStream.Position;
			if ((int) available == 0)
				throw new NoMoreBlocksException(string_Renamed);
		}
		
		/// <summary> Returns a color from the default ADC pallet</summary>
		public static System.Drawing.Color getColorFromIndex(int index)
		{
			assert(index >= 0 && index < defaultColorPallet.Length);
			return defaultColorPallet[index];
		}
	}
}