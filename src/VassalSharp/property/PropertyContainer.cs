/*
* $Id$
*
* Copyright (c) 2009-2010 by Joel Uckelman
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
namespace VassalSharp.property
{
	
	/// <summary> Provides an interface for getting and setting {@link Property}s and
	/// listening for changes to them.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public interface PropertyContainer
	{
		/// <summary> Returns the {@link Property}s known to this container.
		/// 
		/// </summary>
		/// <returns> a set of known {@link Property}s
		/// </returns>
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public Set < Property < ? >> getProperties();
	
	/// <summary> Sets the value of a {@link Property}.
	/// 
	/// </summary>
	/// <param name="prop">the {@link Property}
	/// </param>
	/// <param name="value">the value
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T >
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setValue(Property < T > prop, T value);
	
	/// <summary> Gets the value of a {@link Property}.
	/// 
	/// </summary>
	/// <param name="prop">the {@link Property}
	/// </param>
	/// <returns> the value
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > T getValue(Property < T > prop);
	
	/// <summary> Adds a listener for all changes to this container.
	/// 
	/// </summary>
	/// <param name="listener">the listener to add
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void addPropertyListener(PropertyListener < Object > listener);
	
	/// <summary> Removes a listner for all changes to this container.
	/// 
	/// </summary>
	/// <param name="listener">the listener to remove
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void removePropertyListener(PropertyListener < Object > listener);
	
	/// <summary> Checks whether there are {@link PropertyListener}s for all changes
	/// to this container.
	/// 
	/// </summary>
	/// <returns> {@code true} if there are listeners
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public boolean hasListeners();
	
	/// <summary> Returns a list of {@link PropertyListener}s listening to all changes
	/// to this container.
	/// 
	/// </summary>
	/// <returns> a list of listeners
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public List < PropertyListener < Object >> getPropertyListeners();
	
	/// <summary> Adds a listener for a specific {@link Property}.
	/// 
	/// </summary>
	/// <param name="prop">the {@link Property} to listen on
	/// </param>
	/// <param name="listener">the listener to add
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T >
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void addPropertyListener(Property < T > prop, 
	PropertyListener < ? super T > listener);
	
	/// <summary> Removes a listener for a specific {@link Property}.
	/// 
	/// </summary>
	/// <param name="prop">the {@link Property} listened on
	/// </param>
	/// <param name="listener">the listener to remove
	/// </param>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T >
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void removePropertyListener(Property < T > prop, 
	PropertyListener < ? super T > listener);
	
	/// <summary> Returns a list of {@link PropertyListener}s listening to a given
	/// {@link Property}.
	/// 
	/// </summary>
	/// <param name="prop">the {@link Property} listened on
	/// </param>
	/// <returns> a list of listeners
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > List < PropertyListener < ? super T >> 
	getPropertyListeners(Property < T > prop);
	
	/// <summary> Checks whether there are {@link PropertyListener}s for a given
	/// {@link Property}.
	/// 
	/// </summary>
	/// <returns> {@code true} if there are listeners for the given property
	/// </returns>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public < T > boolean hasListeners(Property < T > prop);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}