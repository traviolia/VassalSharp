/*
* $Id$
*
* Copyright (c) 2010 by Pieter Geerkens
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
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Command = VassalSharp.command.Command;
using VisibilityCondition = VassalSharp.configure.VisibilityCondition;
namespace VassalSharp.build.module
{
	
	/// <summary> A Global Key Command that is automatically invoked on game start-up,
	/// once the various Key Listeners have been started.
	/// <p>
	/// If multiple start-up commands need to be run, they should be combined
	/// in a MultiAction Button and then launched from a single instance of
	/// StartupGlobalKeyCommand, as the sequence in which multiple instances of
	/// StartupGlobalKeyCommand are fired is undetermined.
	/// 
	/// </summary>
	/// <author>  Pieter Geerkens
	/// 
	/// </author>
	public class StartupGlobalKeyCommand:GlobalKeyCommand, GameComponent
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassVisibilityCondition1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassVisibilityCondition1 : VisibilityCondition
		{
			public AnonymousClassVisibilityCondition1(StartupGlobalKeyCommand enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StartupGlobalKeyCommand enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StartupGlobalKeyCommand enclosingInstance;
			public StartupGlobalKeyCommand Enclosing_Instance
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
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassRunnable' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(StartupGlobalKeyCommand enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StartupGlobalKeyCommand enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StartupGlobalKeyCommand enclosingInstance;
			public StartupGlobalKeyCommand Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  Run()
			{
				Enclosing_Instance.apply();
			}
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return "Startup Global Key Command";
			}
			
		}
		virtual public Command RestoreCommand
		{
			get
			{
				return null; // No persistent state
			}
			
		}
		public StartupGlobalKeyCommand():base()
		{
			/* These four fields pertaining to the physical representation of the
			* GKC on the toolbar are not applicable in this implementation.
			*/
			launch.setAttribute(BUTTON_TEXT, "");
			launch.setAttribute(TOOLTIP, "");
			launch.setAttribute(ICON, "");
			launch.setAttribute(HOTKEY, "");
		}
		
		//---------------------- GlobalKeyCommand extension ---------------------
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override void  addTo(Buildable parent)
		{
			base.addTo(parent);
			GameModule.getGameModule().getGameState().addGameComponent(this);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("Map.htm", "StartupGlobalKeyCommand");
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override VisibilityCondition getAttributeVisibility(System.String key)
		{
			if (BUTTON_TEXT.Equals(key) || TOOLTIP.Equals(key) || ICON.Equals(key) || HOTKEY.Equals(key))
			{
				return new AnonymousClassVisibilityCondition1(this);
			}
			else
			{
				return base.getAttributeVisibility(key);
			}
		}
		
		//---------------------- GameComponent implementation ---------------------
		private bool hasStarted = false;
		
		public virtual void  setup(bool gameStarting)
		{
			if (gameStarting && !hasStarted)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
				SwingUtilities.invokeLater(new AnonymousClassRunnable(this));
			}
			hasStarted = gameStarting;
		}
	}
}