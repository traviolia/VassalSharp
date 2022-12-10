/*
* $Id$
*
* Copyright (c) 2008-2009 Brent Easton
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
using Plugin = VassalSharp.build.module.Plugin;
using CalculatedProperty = VassalSharp.counters.CalculatedProperty;
namespace VassalSharp.script
{
	
	public class BshPlugin:Plugin
	{
		
		public BshPlugin():base()
		{
		}
		
		public override void  init()
		{
			registerGamePiece(new CalculatedProperty());
			registerCommandEncoder(new BshCommandEncoder());
		}
	}
}