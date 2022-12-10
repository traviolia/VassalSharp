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
using AbstractBuildable = VassalSharp.build.AbstractBuildable;
using Buildable = VassalSharp.build.Buildable;
namespace VassalSharp.build.module
{
	
	/// <summary> This is now a dummy class</summary>
	
	public class ConnectionWindow:AbstractBuildable
	{
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[0];
			}
			
		}
		public ConnectionWindow()
		{
		}
		
		public override void  addTo(Buildable b)
		{
		}
		
		public override void  setAttribute(System.String name, System.Object value_Renamed)
		{
		}
		
		public override System.String getAttributeValueString(System.String name)
		{
			return null;
		}
	}
}