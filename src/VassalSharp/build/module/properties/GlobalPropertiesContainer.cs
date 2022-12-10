/*
* $Id$
*
* Copyright (c) 2000-2008 by Rodney Kinney
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
namespace VassalSharp.build.module.properties
{
	
	/// <summary> A component that can contain global properties</summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public interface GlobalPropertiesContainer
	{
		//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		PropertyChangeListener PropertyListener
		{
			get;
			
		}
		GlobalProperty getGlobalProperty(System.String propertyName);
	}
}