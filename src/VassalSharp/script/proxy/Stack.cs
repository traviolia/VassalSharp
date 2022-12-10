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
namespace VassalSharp.script.proxy
{
	
	
	/// <summary> BeanShell proxy for the Vassal GamePiece object
	/// 
	/// BeanShell proxies control access to Vassal components and expose
	/// a consistent API to BeanShell Scripts.
	/// 
	/// PLEASE NOTE
	/// New methods may be added, but DO NOT remove or change the
	/// call signature of any existing methods
	/// 
	/// </summary>
	
	public class Stack:GamePiece
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < GamePiece > list = new ArrayList < GamePiece >(getPieceCount());
			for (int i = 0; i < PieceCount; i++)
			{
				list.add(new GamePiece(VassalStack.getPieceAt(i)));
			}
			return list;
		}
		override public Stack Parent
		{
			get
			{
				return null;
			}
			
		}
		private VassalSharp.counters.Stack VassalStack
		{
			get
			{
				return (VassalSharp.counters.Stack) getVassalGamePiece();
			}
			
		}
		virtual public int PieceCount
		{
			get
			{
				return VassalStack.PieceCount;
			}
			
		}
		
		public Stack(VassalSharp.counters.Stack s):base(s)
		{
			InitBlock();
		}
		
		public virtual GamePiece topPiece()
		{
			return new GamePiece(VassalStack.topPiece());
		}
		
		public virtual GamePiece bottomPiece()
		{
			return new GamePiece(VassalStack.bottomPiece());
		}
		
		public virtual GamePiece getPieceAt(int i)
		{
			return new GamePiece(VassalStack.getPieceAt(i));
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < GamePiece > getPieces()
	}
}