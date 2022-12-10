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
using GameModule = VassalSharp.build.GameModule;
namespace VassalSharp.tools
{
	
	/// <summary> Utility subclass of {@link FormattedString} which automatically includes
	/// variables for Player name, side, and id
	/// </summary>
	/// <deprecated> use FormattedString constructor with defaultProperties
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class PlayerIdFormattedString:FormattedString
	{
		public PlayerIdFormattedString():this("")
		{
		}
		
		public PlayerIdFormattedString(System.String s):base(s, GameModule.getGameModule())
		{
		}
	}
}