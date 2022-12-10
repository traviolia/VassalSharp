/*
* $Id$
*
* Copyright (c) 2008 by Brent Easton and Joel Uckelman
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
namespace VassalSharp.build.module.metadata
{
	
	/// <summary> 
	/// Class representing the metadata for an Importable file.
	/// 
	/// </summary>
	public class ImportMetaData:AbstractMetaData
	{
		override public System.String MetaDataVersion
		{
			get
			{
				return DATA_VERSION;
			}
			
		}
		override public System.String ZipEntryName
		{
			get
			{
				return null;
			}
			
		}
		
		public const System.String DATA_VERSION = "1";
		
		protected internal override void  addElements(System.Xml.XmlDocument doc, System.Xml.XmlElement root)
		{
			
		}
	}
}