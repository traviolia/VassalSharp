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
	
	/// <summary> BeanShell proxy for the Vassal Map object
	/// 
	/// BeanShell proxies control access to Vassal components and expose
	/// a consistent API to BeanShell Scripts.
	/// 
	/// PLEASE NOTE
	/// New methods may be added, but DO NOT remove or change the
	/// call signature of any existing methods
	/// 
	/// </summary>
	public class Map
	{
		private void  InitBlock()
		{
			VassalSharp.counters.GamePiece[] vPieces = vassalMap.getPieces();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < GamePiece > pieces = new ArrayList < GamePiece >(vPieces.length);
			for (int i = 0; i < vPieces.Length; i++)
			{
				pieces.add(new GamePiece(vPieces[i]));
			}
			return pieces;
		}
		virtual public System.String Name
		{
			get
			{
				return vassalMap.getMapName();
			}
			
		}
		
		private VassalSharp.build.module.Map vassalMap;
		
		public Map(VassalSharp.build.module.Map m)
		{
			InitBlock();
			vassalMap = m;
		}
		
		public virtual System.Object getProperty(System.String name)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return BeanShell.wrap(vassalMap.getProperty(name).ToString());
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < GamePiece > getPieces()
	}
}