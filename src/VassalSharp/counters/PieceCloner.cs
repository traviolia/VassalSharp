/*
* $Id$
*
* Copyright (c) 2004-2008 by Rodney Kinney, Joel Uckelman
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
using GameModule = VassalSharp.build.GameModule;
using Map = VassalSharp.build.module.Map;
using AddPiece = VassalSharp.command.AddPiece;
using ReflectionUtils = VassalSharp.tools.ReflectionUtils;
namespace VassalSharp.counters
{
	
	/// <summary> Utility class for cloning {@link GamePiece}s</summary>
	public class PieceCloner
	{
		public static PieceCloner Instance
		{
			get
			{
				return instance;
			}
			
		}
		private static PieceCloner instance = new PieceCloner();
		
		// For use by subclasses
		protected internal PieceCloner()
		{
		}
		
		/// <summary> Create a new instance that is a clone of the given piece.
		/// 
		/// </summary>
		/// <returns> the new instance
		/// </returns>
		public virtual GamePiece clonePiece(GamePiece piece)
		{
			GamePiece clone = null;
			if (piece is BasicPiece)
			{
				clone = GameModule.getGameModule().createPiece(piece.Type);
				//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Map m = piece.getMap();
				
				// Temporarily set map to null so that clone won't be added to map
				piece.setMap(null);
				
				clone.State = piece.State;
				piece.setMap(m);
			}
			else if (piece is UsePrototype)
			{
				clone = clonePiece(((UsePrototype) piece).ExpandedInner);
			}
			else if (piece is EditablePiece && piece is Decorator)
			{
				try
				{
					clone = piece.GetType().getConstructor().newInstance();
					((Decorator) clone).setInner(clonePiece(((Decorator) piece).getInner()));
					((EditablePiece) clone).mySetType(((Decorator) piece).myGetType());
					((Decorator) clone).mySetState(((Decorator) piece).myGetState());
				}
				//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception t)
				{
					ReflectionUtils.handleNewInstanceFailure(t, piece.GetType());
				}
			}
			else
			{
				clone = ((AddPiece) GameModule.getGameModule().decode(GameModule.getGameModule().encode(new AddPiece(piece)))).Target;
				//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				Map m = piece.getMap();
				
				// Temporarily set map to null so that clone won't be added to map
				piece.setMap(null);
				
				clone.State = piece.State;
				piece.setMap(m);
			}
			return clone;
		}
	}
}