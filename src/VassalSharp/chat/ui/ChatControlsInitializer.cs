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
namespace VassalSharp.chat.ui
{
	
	
	/// <summary> Interface for registering event listeners with the Swing components in a ChatServerControls component</summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public interface ChatControlsInitializer
	{
		/// <summary>Register all event listeners </summary>
		void  initializeControls(ChatServerControls controls);
		/// <summary>Remove all previously-registered event listeners </summary>
		void  uninitializeControls(ChatServerControls controls);
	}
}