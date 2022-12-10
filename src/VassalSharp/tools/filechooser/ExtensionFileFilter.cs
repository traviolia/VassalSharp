/*
* $Id$
*
* Copyright (c) 2006-2008 by Joel Uckelman
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
//UPGRADE_TODO: The type 'org.apache.commons.lang.StringUtils' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using StringUtils = org.apache.commons.lang.StringUtils;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
namespace VassalSharp.tools.filechooser
{
	
	/// <summary> A generic by-extension FileFilter.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	public class ExtensionFileFilter:FileFilter
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'types '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String[] types;
		//UPGRADE_NOTE: Final was removed from the declaration of 'desc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.String desc;
		
		/// <param name="desc">The description of this filter.
		/// </param>
		/// <param name="types">A list of the extensions accepted by this filter.
		/// </param>
		public ExtensionFileFilter(System.String desc, System.String[] types):base()
		{
			this.desc = desc + " (*" + StringUtils.join(types, ", *") + ")";
			this.types = ArrayUtils.copyOf(types);
		}
		
		/// <returns> Whether the given file is accepted by this filter.
		/// </returns>
		public override bool accept(System.IO.FileInfo f)
		{
			if (System.IO.Directory.Exists(f.FullName))
				return true;
			//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String name = f.Name.ToLower();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String type: types)
			{
				if (name.endsWith(type))
					return true;
			}
			return false;
		}
		
		/// <returns> The description of this filter.
		/// </returns>
		public override System.String getDescription()
		{
			return desc;
		}
	}
}