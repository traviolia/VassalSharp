/*
* $Id$
*
* Copyright (c) 2005 by Rodney Kinney
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
using MassKeyCommand = VassalSharp.build.module.map.MassKeyCommand;
using VisibilityCondition = VassalSharp.configure.VisibilityCondition;
namespace VassalSharp.build.module
{
	
	/// <summary> This version of {@link MassKeyCommand} is added directly to a
	/// {@link VassalSharp.build.GameModule} and applies to all maps
	/// </summary>
	public class GlobalKeyCommand:MassKeyCommand
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassVisibilityCondition' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassVisibilityCondition : VisibilityCondition
		{
			public AnonymousClassVisibilityCondition(GlobalKeyCommand enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(GlobalKeyCommand enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GlobalKeyCommand enclosingInstance;
			public GlobalKeyCommand Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual bool shouldBeVisible()
			{
				return false;
			}
		}
		
		public override void  apply()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < Map > l = Map.getMapList();
			GameModule.getGameModule().sendAndLog(globalCommand.apply(l.toArray(new Map[l.size()]), getFilter()));
		}
		
		// Hide 'This Map only' option
		public override VisibilityCondition getAttributeVisibility(System.String name)
		{
			if (SINGLE_MAP.Equals(name))
			{
				return new AnonymousClassVisibilityCondition(this);
			}
			else
			{
				return base.getAttributeVisibility(name);
			}
		}
	}
}