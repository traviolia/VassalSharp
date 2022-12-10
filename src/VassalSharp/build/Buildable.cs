/*
 * VassalSharp - Copyright (c) 2021 Martin Traviolia.
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

namespace VassalSharp.build
{
	
	/// <summary>
    /// The instances of a Buildable class can be initialized from a configuration file.
	/// The configuration file is an XML file in which each XML element represents an object.
    /// These Buildable objects are built with a containment hierarchy that mirrors the XML structure.
	/// </summary>
	public interface Buildable
	{
		/// <summary> 
        /// Build the object
        /// </summary>
		/// <param name="e">the XML element containing the object data</param>
		void  Build(System.Xml.XmlElement e);
		
		/// <summary>
        /// Adds this component to its parent.
		/// In order to make Buildable objects extensible, the child is reponsible for adding itself to the parent.
        /// That way, Buildable subcomponents can be defined in an extension package
		/// without needing to modify the containing class.
		/// </summary>
		void  addTo(Buildable parent);
		
		/// <summary> 
        /// Adds a child component.
        /// Both this method and {@link #addTo} are invoked when adding a child to a parent
		/// </summary>
		void  add(Buildable child);
		
		/// <returns> 
        /// an XML element from which
		/// this component can be built
		/// </returns>
		System.Xml.XmlElement getBuildElement(System.Xml.XmlDocument doc);
	}
}