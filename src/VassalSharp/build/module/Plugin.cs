/*
* $Id$
*
* Copyright (c) 2000-2007 by Rodney Kinney, Brent Easton
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

using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using CommandEncoder = VassalSharp.command.CommandEncoder;
//using ConfigureTree = VassalSharp.configure.ConfigureTree;
//using GamePiece = VassalSharp.counters.GamePiece;
//using PieceDefiner = VassalSharp.counters.PieceDefiner;

namespace VassalSharp.build.module
{
	
	/// <summary> Plugin is a general purpose component for use by Module Plugins that
	/// require some sort of initialisation. Module Plugins do not need
	/// to include a Plugin component if they consist only of code and
	/// image resources used by other parts of the module.
	/// 
	/// Plugin should be subclassed and added to the Module Plugin.
	/// 
	/// </summary>
	public class Plugin : AbstractConfigurable, PluginsLoader.PluginElement
	{
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return new System.String[0];
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return new System.String[0];
			}
			
		}
		
		/*
		* The module has not been built yet, so only minimal
		* initialisation should be performed in the constructor
		*/
		public Plugin()
		{
		}
		
		/// <summary> init() is called by the GameModule when the build of the module and
		/// all extensions is complete. Any initialisation that depends
		/// on other parts of the module should be implemented here
		/// </summary>
		public virtual void  init()
		{
			
		}
		
		///// <summary> Utility routine to register a GamePiece with the PieceDefiner
		///// so that it appears as an option in the list of traits
		///// </summary>
		///// <param name="p">GamePiece to register
		///// </param>
		//public virtual void  registerGamePiece(GamePiece p)
		//{
		//	PieceDefiner.addDefinition(p);
		//}
		
		/// <summary> Utility routine to register a CommandEncoder with the module</summary>
		/// <param name="encoder">
		/// </param>
		public virtual void  registerCommandEncoder(CommandEncoder encoder)
		{
			GameModule.getGameModule().addCommandEncoder(encoder);
		}
		
		/// <summary> Utility routine to register a new component with the module
		/// editor so that it appears in the right-click popup menu. To add
		/// components to the top module level, use BasicModule.class, not
		/// GameModule.class as the parent.
		/// </summary>
		/// <param name="parent">parent of the new component
		/// </param>
		/// <param name="child">new component
		/// </param>

		public

		void registerComponent<X,Y>(X parent, Y child) where X : Buildable where Y : Buildable
		{
			//ConfigureTree.addAdditionalComponent(parent, child);
			return;
		}


		public override Type[] getAttributeTypes()
		{
			return new Type[0];
		}
		
		public override System.String getAttributeValueString(System.String key)
		{
			return string.Empty;
		}
		
		public override void  setAttribute(System.String key, System.Object value_Renamed)
		{
			
		}
		

		public override Type[] AllowableConfigureComponents
		{
			get { return new Type[0]; }
		}
		
		public override HelpFile HelpFile
		{
			get
			{
				return null;
			}
		}
		
		public override void  removeFrom(Buildable parent)
		{
			
		}
		
		public override void  addTo(Buildable parent)
		{
			
		}
	}
}