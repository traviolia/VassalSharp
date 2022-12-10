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
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
namespace VassalSharp.counters
{
	
	/// <summary> If a GamePiece implements the EditablePiece interface, then
	/// it can be manipulated from the Configuration Tree via the PieceDefiner
	/// dialog.
	/// 
	/// All Decorator classes with a no-arg constructor will appear in the
	/// Available Traits list even if they don't implement EditablePiece 
	/// </summary>
	public interface EditablePiece : GamePiece
	{
		/// <summary>A plain-English description of this type of piece </summary>
		string Description
		{
			get;
			
		}
		HelpFile HelpFile
		{
			get;
			
		}
		
		/// <summary>Set the type information for this piece.  See {@link Decorator#myGetType} </summary>
		void  mySetType(string type);
		
		/// <summary>Get the configurer for this trait </summary>
		PieceEditor getEditor();
	}
}