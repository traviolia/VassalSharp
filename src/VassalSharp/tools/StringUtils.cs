/*
* $Id$
*
* Copyright (c) 2008-2009 by Joel Uckelman
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
//UPGRADE_TODO: The type 'com.google.common.base_Renamed.Strings' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Strings = com.google.common.base_Renamed.Strings;
namespace VassalSharp.tools
{
	
	/// <summary> A collection of methods for manipulating {@link String}s.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	/// <deprecated> Use {@link org.apache.commons.lang.StringUtils} instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class StringUtils
	{
		private void  InitBlock()
		{
			return join_Renamed_Field;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			, new ArrayIterator < String >(str));
			return join(delim, str.iterator());
			return join(delim, str);
			if (!i.hasNext())
				return "";
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'sb '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder sb = new StringBuilder(i.next());
			while (i.hasNext())
				sb.append(delim).append(i.next());
			
			return sb.toString();
		}
		private StringUtils()
		{
			InitBlock();
		}
		
		/// <summary> Joins an array of{@link Strings} into a single delimiter-separated
		/// <code>String</code>.
		/// 
		/// </summary>
		/// <param name="delim">the delimiter to appear between elements of <code>str</code>
		/// </param>
		/// <param name="str">the array of <code>String</code>s
		/// </param>
		/// <returns> a single delimited <code>String</code>
		/// </returns>
		public static System.String join_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(String delim, String...str)
		
		/// <deprecated> Use {@link #join(String,String...)} instead. 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public static System.String join(System.String[] str, System.String delim)
		{
			return join(delim, str);
		}
		
		/// <summary> Joins an {@link Iterable} of {@link Strings} into a single
		/// delimiter-separated <code>String</code>.
		/// 
		/// </summary>
		/// <param name="delim">the delimiter to appear between elements of <code>str</code>
		/// </param>
		/// <param name="str">the <code>Iterable</code> of <code>String</code>s
		/// </param>
		/// <returns> a single delimited <code>String</code>
		/// </returns>
		public static System.String join_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(String delim, Iterable < String > str)
		
		/// <deprecated> Use {@link #join(Collection<String>,String)} instead. 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public static System.String join_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(Collection < String > str, String delim)
		
		/// <summary> Joins a {@link Iterator} of {@link Strings} into a single
		/// delimiter-separated <code>String</code>.
		/// 
		/// </summary>
		/// <param name="delim">the delimiter to appear between elements of <code>str</code>
		/// </param>
		/// <param name="i">the <code>Iterator</code> of <code>String</code>
		/// </param>
		/// <returns> a single delimited <code>String</code>
		/// </returns>
		public static System.String join_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(String delim, Iterator < String > i)
	}
}