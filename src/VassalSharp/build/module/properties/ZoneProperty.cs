/*
* $Id$
*
* Copyright (c) 2000-2006 by Brent Easton, Rodney Kinney
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
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using Map = VassalSharp.build.module.Map;
using Board = VassalSharp.build.module.map.boardPicker.Board;
using Zone = VassalSharp.build.module.map.boardPicker.board.mapgrid.Zone;
namespace VassalSharp.build.module.properties
{
	
	/// <summary> </summary>
	/// <author>  Brent Easton
	/// 
	/// A class implementing a cut-down Global property at the Zone level. Zone Properties
	/// do not have Toolbar butttons, but are controlled by SetGlobalProperty traits in
	/// counters.
	/// </author>
	public class ZoneProperty:GlobalProperty
	{
		private void  InitBlock()
		{
			return new System.Type[0];
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return "Global Property";
			}
			
		}
		virtual protected internal System.String PropertyId
		{
			get
			{
				System.String zoneName = parentZone == null?null:parentZone.Name;
				Board board = parentZone == null?null:parentZone.getBoard();
				System.String boardName = board == null?null:board.Name;
				Map map = board == null?null:board.Map;
				System.String mapName = map == null?null:map.getConfigureName();
				return getConfigureName() + "\t" + zoneName + "\t" + boardName + "\t" + mapName;
			}
			
		}
		
		protected internal Zone parentZone;
		
		public ZoneProperty():base()
		{
			InitBlock();
		}
		
		public ZoneProperty(GlobalProperty p):base(p)
		{
			InitBlock();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAllowableConfigureComponents()
		
		public override void  addTo(Buildable parent)
		{
			property.addTo((MutablePropertiesContainer) parent);
			GameModule.getGameModule().addCommandEncoder(this);
			GameModule.getGameModule().getGameState().addGameComponent(this);
			propertySource = (PropertySource) parent;
			parentZone = (Zone) parent;
		}
	}
}