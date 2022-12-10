/*
* $Id$
*
* Copyright (c) 2006 by Rodney Kinney
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
	public class JarArchive : DataArchive
	{
		override public System.String Name
		{
			get
			{
				return prefix != null?prefix:base.Name;
			}
			
		}
		protected internal System.String prefix;
		
		public JarArchive():this(null)
		{
		}
		
		public JarArchive(System.String prefix):base()
		{
			this.prefix = prefix;
		}

		public override System.Uri getURL(System.String fileName)
		{
			GetType();
			System.Uri url = new System.Uri(System.IO.Path.GetFullPath(getAbsolutePath(fileName)));
			if (url != null)
				return url;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			foreach (DataArchive ext in extensions)
			{
				try
				{
					return ext.getURL(fileName);
				}
				catch (System.IO.FileNotFoundException e)
				{
					// not found in this extension, try the next
				}
			}
			
			throw new System.IO.FileNotFoundException("\'" + fileName + "\' not found in " + Name);
		}

		public override System.IO.Stream getInputStream(System.String fileName)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'in '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.lang.Class.getResourceAsStream' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangClassgetResourceAsStream_javalangString'"
			System.IO.Stream in_Renamed = null; // GetType().getResourceAsStream(getAbsolutePath(fileName));
			if (in_Renamed != null)
				return in_Renamed;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			foreach (DataArchive ext in extensions)
			{
				try
				{
					return ext.getInputStream(fileName);
				}
				catch (System.IO.FileNotFoundException e)
				{
					// not found in this extension, try the next
				}
			}
			
			throw new System.IO.FileNotFoundException("\'" + fileName + "\' not found in " + Name);
		}
		
		protected internal virtual System.String getAbsolutePath(System.String file)
		{
			return (prefix != null?"/" + prefix:"") + "/" + file;
		}

		/// <deprecated> Use {@link #getInputStream(String)} instead. 
		/// </deprecated>
		public override System.IO.Stream getFileStream(System.String fileName)
		{
			return getInputStream(fileName);
		}
	}
}