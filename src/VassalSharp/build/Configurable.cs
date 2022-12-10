/*
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
using System.ComponentModel;

using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Configurer = VassalSharp.configure.Configurer;
using Translatable = VassalSharp.i18n.Translatable;
using static SupportClass;

namespace VassalSharp.build
{
	
	/// <summary> An object that implements the Configurable interface can, in
	/// addition to being built from a configuration XML file, be
	/// manipulated by the user directly in the VASSAL module editor via
	/// the Configuration window.
	/// </summary>
	public struct Configurable_Fields{
		public readonly static string NAME_PROPERTY = "name"; //$NON-NLS-1$
	}

	public interface Configurable : Translatable
	{
		//UPGRADE_NOTE: Members of interface 'Configurable' were extracted into structure 'Configurable_Fields'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1045'"
		/// <returns> an array of Configurer objects representing
		/// the Configurable children of this Configurable object
		/// </returns>
		Configurable[] ConfigureComponents
		{
			get;
			
		}
		/// <summary>
        /// Return a {@link Configurer} object which can be used to set the attributes of this object
		/// </summary>
		Configurer Configurer
		{
			get;
			
		}
		/// <summary>
        /// Return a list of valid sub-component Classes.
        /// If a Class appears in this list, then instances of that class may be added
		/// to this component from the Configuration Window.
		/// </summary>
		System.Type[] AllowableConfigureComponents
		{
			get;
			
		}
		
		/// <summary> Remove this component from its parent</summary>
		void  removeFrom(Buildable parent);
		
		
		/// <summary> Remove a child component</summary>
		void  remove(Buildable child);
		
		/// <summary> The name of this Configurable Object</summary>
		string getConfigureName();

		/// <returns> a HelpFilte describing how to use and configure
		/// this component
		/// </returns>
		HelpFile getHelpFile();
		
		/// <summary>
		/// Add a PropertyChangeListener.  A PropertyChangeEvent should be fired
		/// with property name {@link #NAME_PROPERTY} when the value returned from
		/// {@link #getConfigureName} has changed
		/// </summary>
		void  addPropertyChangeListener(PropertyChangeEventHandler l);
	}
}