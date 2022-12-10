/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
namespace VassalSharp.command
{
	
	/// <summary> Translates a {@link Command} to and from a string
	/// Although Commands can be linked into compound commands,
	/// each CommandEncoder need only handle single (not compound) commands
	/// </summary>
	public interface CommandEncoder
	{
		
		/// <summary>Translate a String into a {@link Command} </summary>
		Command decode(System.String command);
		/// <summary>Translate a {@link Command} into a String </summary>
		System.String encode(Command c);
	}
}