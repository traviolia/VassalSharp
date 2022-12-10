/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
namespace VassalSharp.tools
{
	
	/// <summary> Utility class for decoding URL-encoded strings
	/// 
	/// </summary>
	/// <deprecated> Use {@link java.net.URLDecoder} instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class Decoder
	{
		
		public static System.String URLdecode(System.String s)
		{
			System.IO.MemoryStream out_Renamed = new System.IO.MemoryStream(s.Length);
			
			for (int i = 0; i < s.Length; ++i)
			{
				char c = s[i];
				if (c == '+')
				{
					c = ' ';
				}
				else if (c == '%')
				{
					try
					{
						//UPGRADE_TODO: Method 'java.lang.Integer.parseInt' was converted to 'System.Convert.ToInt32' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
						c = (char) System.Convert.ToInt32(s.Substring(i + 1, (i + 3) - (i + 1)), 16);
						i += 2;
					}
					catch (System.Exception e)
					{
						return s;
					}
				}
				out_Renamed.WriteByte((System.Byte) c);
			}
			char[] tmpChar;
			byte[] tmpByte;
			tmpByte = out_Renamed.GetBuffer();
			tmpChar = new char[out_Renamed.Length];
			System.Array.Copy(tmpByte, 0, tmpChar, 0, tmpChar.Length);
			return new System.String(tmpChar);
		}
	}
}