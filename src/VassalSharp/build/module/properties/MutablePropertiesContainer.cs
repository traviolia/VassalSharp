/*
* $Id$
*
* Copyright (c) 2006-2012 by Rodney Kinney, Brent Easton
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
using System.Collections.Generic;

namespace VassalSharp.build.module.properties
{

    public interface MutablePropertiesContainer
    {
        /// <summary>Return a unique Id for the container </summary>
        System.String MutablePropertiesContainerId
        {


            get;

        }
        /// <summary> Add a property under the given key</summary>
        /// <param name="key">
        /// </param>
        /// <param name="p">
        /// </param>
        void addMutableProperty(System.String key, MutableProperty p);

        /// <summary> Remove the property with the given key</summary>
        /// <param name="key">
        /// </param>
        MutableProperty removeMutableProperty(System.String key);

        /// <summary>Find a GlobalProperty object with the given name </summary>
        MutableProperty getMutableProperty(System.String propertyName);

    }

    /// <summary> A component that can contain mutable (updateable) properties
    /// 
    /// </summary>
    /// <author>  rkinney
    /// 
    /// </author>
    /// <summary> Simple implementation of {@link MutablePropertiesContainer}</summary>
    /// <author>  rkinney
    /// 
    /// </author>
    public class MutablePropertiesContainerImpl : MutablePropertiesContainer
	{
		virtual public System.String MutablePropertiesContainerId
		{
			get
			{
				return id;
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private Dictionary < String, MutableProperty > props = new Dictionary<string, MutableProperty>();
		private System.String id;
		
		public MutablePropertiesContainerImpl():this("")
		{
		}
		
		public MutablePropertiesContainerImpl(System.String id)
		{
			this.id = id;
		}
		
		public virtual void  addMutableProperty(System.String key, MutableProperty p)
		{
			props.Add(key, p);
		}
		
		public virtual MutableProperty getMutableProperty(System.String propertyName)
		{
			props.TryGetValue(propertyName, out MutableProperty value);
            return value;
		}

        public virtual MutableProperty removeMutableProperty(System.String key)
        {
            props.TryGetValue(key, out MutableProperty value);
            props.Remove(key);
            return value;
        }
    }
}