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
using Map = VassalSharp.build.module.Map;
namespace VassalSharp.build.module.map
{
	
	/// <summary> A component may implement the Drawable interface if it has
	/// graphical information the should be drawn whenever the Map is
	/// drawn.
	/// 
	/// </summary>
	/// <seealso cref="Map.addDrawComponent">
	/// </seealso>
	public interface Drawable
	{
		void  draw(System.Drawing.Graphics g, Map map);
		
		bool drawAboveCounters();
	}
}