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
using Command = VassalSharp.command.Command;
using Resources = VassalSharp.i18n.Resources;
using DataArchive = VassalSharp.tools.DataArchive;
namespace VassalSharp.build.module
{
	
	/// <summary> A ModulePlugin is basically identical to a ModuleExtension except
	/// that is loaded earlier and must throw Plugin specific error messages
	/// </summary>
	public class ModulePlugin:ModuleExtension
	{
		override public Command RestoreCommand
		{
			get
			{
				return new RegCmd(Name, Version);
			}
			
		}
		
		public ModulePlugin(DataArchive archive):base(archive)
		{
		}
		
		new public class RegCmd:ModuleExtension.RegCmd
		{
			virtual protected internal System.String NotLoadedMsg
			{
				get
				{
					return Resources.getString("ModulePlugin.load_plugin", Name, PluginsLoader.PluginDirectory); //$NON-NLS-1$
				}
				
			}
			
			public RegCmd(System.String name, System.String version):base(name, version)
			{
			}
			
			protected internal virtual System.String getVersionErrorMsg(System.String v)
			{
				return Resources.getString("ModulePlugin.wrong_plugin_version", Version, Name, v);
			}
		}
	}
}