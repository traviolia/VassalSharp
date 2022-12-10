/*
*
* Copyright (c) 2000-2007 by Rodney Kinney
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
namespace VassalSharp.build.module
{
	
	/// <summary> Represents a step during the initialization of a game in which the player must specify some information See
	/// {@link GameState#addGameSetupStep(SetupStep)}
	/// 
	/// </summary>
	/// <author>  rkinney
	/// </author>
	public interface GameSetupStep
	{
		/// <returns> true if this step needs no further information, false if the player should be prompted for more information 
		/// </returns>
		bool Finished
		{
			get;
			
		}
		/// <summary>A human-understandable description of this step </summary>
		System.String StepTitle
		{
			get;
			
		}
		/// <summary>A GUI component that prompts the player for the needed information. If null, then no further information is needed </summary>
		System.Windows.Forms.Control Controls
		{
			get;
			
		}
		
		/// <summary>Apply the information gathered via the component to the game in progress </summary>
		void  finish();
	}
}