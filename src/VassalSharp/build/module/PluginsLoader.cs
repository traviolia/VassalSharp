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
using GameModule = VassalSharp.build.GameModule;
using Command = VassalSharp.command.Command;
using Resources = VassalSharp.i18n.Resources;
using DataArchive = VassalSharp.tools.DataArchive;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
using System.IO;

namespace VassalSharp.build.module
{
	
	/// <summary> Load Plugins.</summary>
	/// <author>  Brent Easton
	/// 
	/// </author>
	public class PluginsLoader:ExtensionsLoader
	{
		public static System.String PluginDirectory
		{
			get
			{
				return new ExtensionsManager("plugins").getExtensionsDirectory(false).FullName; //$NON-NLS-1$
			}
			
		}
		
		new public const System.String COMMAND_PREFIX = "PLUGIN\t"; //$NON-NLS-1$
		
		new private ExtensionsManager extMgr = new ExtensionsManager("plugins");
		
		public override void addTo(GameModule mod)
		{
			mod.addCommandEncoder(this);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			foreach (FileInfo ext in extMgr.getActiveExtensions())
			{
				addExtension(ext);
			}
		}
		protected internal override ModuleExtension createExtension(System.String extname)
		{
			return new ModulePlugin(new DataArchive(extname));
		}
		
		public override Command decode(System.String command)
		{
			Command c = null;
			if (command.StartsWith(COMMAND_PREFIX))
			{
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(command.Substring(COMMAND_PREFIX.Length), '\t');
				c = new ModulePlugin.RegCmd(st.nextToken(), st.nextToken());
			}
			else
			{
				c = base.decode(command);
			}
			return c;
		}
		
		public override System.String encode(Command c)
		{
			System.String s = null;
			if (c is ModulePlugin.RegCmd)
			{
				ModulePlugin.RegCmd cmd = (ModulePlugin.RegCmd) c;
				SequenceEncoder se = new SequenceEncoder('\t');
				se.append(cmd.Name).append(cmd.Version);
				s = COMMAND_PREFIX + se.Value;
			}
			else
			{
				s = base.encode(c);
			}
			return s;
		}
		
		protected internal override System.String getLoadedMessage(System.String name, System.String version)
		{
			return null; // Resources.getString("PluginsLoader.plugin_loaded", name, version); //$NON-NLS-1$
		}
		
		protected internal override System.String getErrorMessage(System.String name, System.String msg)
		{
			return null; // Resources.getString("PluginsLoader.unable_to_load", name, msg); //$NON-NLS-1$
		}
		
		/// <summary> Any components that are added to the module by a Plugin MUST
		/// implement PluginElement to prevent them being written to the
		/// buildFile when saving the module. Implemented by Plugin and
		/// ModuleExtension.
		/// </summary>
		public interface PluginElement
		{
		}
	}
}