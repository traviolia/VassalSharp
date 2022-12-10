/*
* $Id$
*
* Copyright (c) 2009 by Brent Easton
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
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.tools
{
	
	/// <summary> Code for controlling looping common to both TriggerAction and DoActionButton</summary>
	public class LoopControl
	{
		
		// Limit number of loops before throwing a RecusionLimitException
		public const int LOOP_LIMIT = 500;
		
		// Loop Types - saved in buildfile
		public const System.String LOOP_COUNTED = "counted"; //$NON-NLS-1$
		public const System.String LOOP_WHILE = "while"; //$NON-NLS-1$
		public const System.String LOOP_UNTIL = "until"; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'LOOP_TYPES '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly System.String[] LOOP_TYPES = new System.String[]{LOOP_COUNTED, LOOP_UNTIL, LOOP_WHILE};
		
		// Localized description of loop types
		//UPGRADE_NOTE: Final was removed from the declaration of 'LOOP_TYPE_DESCS '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly System.String[] LOOP_TYPE_DESCS = new System.String[]{Resources.getString("Editor.LoopControl.repeat_fixed"), Resources.getString("Editor.LoopControl.repeat_until"), Resources.getString("Editor.LoopControl.repeat_while")}; //$NON-NLS-1$
		
		/// <summary> Convert a Loop Type to a localized description
		/// 
		/// </summary>
		/// <param name="type">loop type
		/// </param>
		/// <returns> localized description
		/// </returns>
		public static System.String loopTypeToDesc(System.String type)
		{
			for (int i = 0; i < LOOP_TYPES.Length; i++)
			{
				if (LOOP_TYPES[i].Equals(type))
				{
					return LOOP_TYPE_DESCS[i];
				}
			}
			return LOOP_TYPE_DESCS[0];
		}
		
		/// <summary> Convert a localized desciption of a loop type back to a raw type
		/// 
		/// </summary>
		/// <param name="desc">localized description of loop type
		/// </param>
		/// <returns> loop type
		/// </returns>
		public static System.String loopDescToType(System.String desc)
		{
			for (int i = 0; i < LOOP_TYPES.Length; i++)
			{
				if (LOOP_TYPE_DESCS[i].Equals(desc))
				{
					return LOOP_TYPES[i];
				}
				if (LOOP_TYPES[i].Equals(desc))
				{
					return desc;
				}
			}
			return LOOP_TYPES[0];
		}
	}
}