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
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
namespace VassalSharp.configure
{
	
	/// <summary>Utility subclass of {@link FormattedStringConfigurer} which includes variable
	/// keys for player name, side, and id
	/// </summary>
	public class PlayerIdFormattedStringConfigurer:FormattedStringConfigurer
	{
		public PlayerIdFormattedStringConfigurer(System.String key, System.String name, System.String[] options):base(key, name)
		{
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'allOptions '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] allOptions = ArrayUtils.append(new System.String[]{GlobalOptions.PLAYER_NAME, GlobalOptions.PLAYER_SIDE, GlobalOptions.PLAYER_ID}, options);
			
			Options = allOptions;
		}
	}
}