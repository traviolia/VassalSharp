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
using BeanShell = VassalSharp.script.BeanShell;
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
	/// </summary>
	public class GamePiece
	{
		virtual public System.String Name
		{
			get
			{
				return vassalGamePiece.getName();
			}
			
		}
		virtual public Map Map
		{
			get
			{
				return new Map(vassalGamePiece.getMap());
			}
			
		}
		virtual public System.Drawing.Point Position
		{
			get
			{
				return vassalGamePiece.Position;
			}
			
		}
		virtual public Stack Parent
		{
			get
			{
				return new Stack(vassalGamePiece.Parent);
			}
			
		}
		
		private VassalSharp.counters.GamePiece vassalGamePiece;
		
		public GamePiece(VassalSharp.counters.GamePiece g)
		{
			setVassalGamePiece(g);
		}
		
		protected internal virtual void  setVassalGamePiece(VassalSharp.counters.GamePiece g)
		{
			vassalGamePiece = g;
		}
		
		internal virtual VassalSharp.counters.GamePiece getVassalGamePiece()
		{
			return vassalGamePiece;
		}
		
		public virtual System.Object getProperty(System.String key)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return BeanShell.wrap(vassalGamePiece.getProperty(key).ToString());
		}
	}
}