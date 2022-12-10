/*
* $Id$
*
* Copyright (c) 2008-2009 Brent Easton
*
* This library is free software; you can redistribute it and/or modify it under
* the terms of the GNU Library General Public License (LGPL) as published by
* the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful, but WITHOUT
* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
* FOR A PARTICULAR PURPOSE. See the GNU Library General Public License for more
* details.
*
* You should have received a copy of the GNU Library General Public License
* along with this library; if not, copies are available at
* http://www.opensource.org.
*/
using System;
using BasicCommandEncoder = VassalSharp.build.module.BasicCommandEncoder;
using CalculatedProperty = VassalSharp.counters.CalculatedProperty;
using Decorator = VassalSharp.counters.Decorator;
using GamePiece = VassalSharp.counters.GamePiece;
namespace VassalSharp.script
{
	
	/// <summary> Used by the Bsh Plugin</summary>
	/// <author>  Brent
	/// 
	/// </author>
	public class BshCommandEncoder:BasicCommandEncoder
	{
		
		public override Decorator createDecorator(System.String type, GamePiece inner)
		{
			if (type.StartsWith(CalculatedProperty.ID))
			{
				return new CalculatedProperty(type, inner);
			}
			return base.createDecorator(type, inner);
		}
	}
}