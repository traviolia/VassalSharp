/*
 * Copyright (c) 2007 by Rodney Kinney, Brent Easton
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

using EditablePiece = VassalSharp.counters.EditablePiece;

namespace VassalSharp.i18n
{
	
	/// <summary>
    /// Decorators that contain localizable elements must implement
	/// this interface
	/// </summary>
	/// <author>
    /// Brent Easton
	/// </author>
	public struct TranslatablePiece_Fields {
		public readonly static string PREFIX = "Piece.";
	}

	public interface TranslatablePiece : EditablePiece
	{
		//UPGRADE_NOTE: Members of interface 'TranslatablePiece' were extracted into structure 'TranslatablePiece_Fields'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1045'"
		
		/// <summary> Return a PieceI18nData object returning the I18n data about this GamePiece.</summary>
		PieceI18nData getI18nData();
	}
}