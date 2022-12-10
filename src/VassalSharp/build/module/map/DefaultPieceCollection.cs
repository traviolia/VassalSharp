/*
* $Id$
*
* Copyright (c) 2004 by Rodney Kinney
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
using GamePiece = VassalSharp.counters.GamePiece;
using Properties = VassalSharp.counters.Properties;
namespace VassalSharp.build.module.map
{
	
	/// <summary> Default implementation of {@link PieceCollection} separates pieces into
	/// two layers:  stacking pieces always above non-stacking pieces
	/// </summary>
	public class DefaultPieceCollection:CompoundPieceCollection
	{
		public DefaultPieceCollection():base(2)
		{
		}
		
		public override int getLayerForPiece(GamePiece p)
		{
			return true.Equals(p.getProperty(VassalSharp.counters.Properties_Fields.NO_STACK))?0:1;
		}
	}
}