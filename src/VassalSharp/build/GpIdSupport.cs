/*
* $Id$
*
* Copyright (c) 2008 by Brent Easton
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
namespace VassalSharp.build
{
	
	/// <summary> GameModule and ModuleExtension are the top-level components and must
	/// provide some Namespace services for generated GamePiece Identifiers.
	/// 
	/// Each Extension must have a unique Namespace String. Each PieceSlot is
	/// allocated a unique number in a series maintained for each top level
	/// component.
	/// 
	/// Each GampePiece generated is tied to it's originating PieceSlot by the
	/// NameSpace Id and the PieceSlot Id.
	/// </summary>
	public interface GpIdSupport
	{
		/// <summary> Accessors to check and update the next GpId if necessary.</summary>
		int NextGpId
		{
			get;
			
			set;
			
		}
		
		/// <summary> Generate a new PieceSlot Id, unique to this top-level component.
		/// 
		/// </summary>
		/// <returns> PieceSlot Id
		/// </returns>
		System.String generateGpId();
	}
}