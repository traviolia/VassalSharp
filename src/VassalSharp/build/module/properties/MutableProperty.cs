/*
* $Id$
*
* Copyright (c) 2000-2009 by Rodney Kinney, Brent Easton
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

using static SupportClass;
using Command = VassalSharp.command.Command;

namespace VassalSharp.build.module.properties
{

	/// <summary>
	/// A container for a String property that can be updated
	/// </summary>
	/// <author>  rkinney
	/// </author>
	public interface MutableProperty
	{
		MutablePropertiesContainer Parent
		{
			get;

		}

		string getPropertyValue();

		Command setPropertyValue(string newValue);

		void addMutablePropertyChangeListener(PropertyChangeEventHandler l);

		void removeMutablePropertyChangeListener(PropertyChangeEventHandler l);

	}

	public static class Util
	{
		/// <summary> Look for a {@link MutableProperty} in the list of {@link MutablePropertiesContainer}. Return the first one
		/// found, searching the lists in order. The list may contain null references, which are skipped
		/// 
		/// </summary>
		/// <param name="propertyContainers">
		/// </param>
		/// <returns>
		/// </returns>
		public static MutableProperty findMutableProperty(String propertyName, List<MutablePropertiesContainer> propertyContainers)
		{
			MutableProperty p = null;
			foreach (MutablePropertiesContainer c in propertyContainers)
			{
				p = (c == null ? null : c.getMutableProperty(propertyName));
				if (p != null)
				{
					break;
				}
			}
			return p;
		}
	}
	/// <summary>
	/// Simple implementation of {@link MutableProperty} Support dynamic changing of the property name, provided that
	/// the {@link #addTo(MutablePropertiesContainer)} method is used to register this property with a properties
	/// container.
	/// </summary>
	/// <author> rkinney</author>
	public class Impl : MutableProperty
	{
		[System.ComponentModel.Browsable(true)]
		public  event SupportClass.PropertyChangeEventHandler PropertyChange;
		virtual public string PropertyName
		{
			set
			{
				if (parent != null)
				{
					parent.removeMutableProperty(propertyName);
					parent.addMutableProperty(value, this);
				}
				propertyName = value;
			}
			
		}
		virtual public string Name
		{
			get
			{
				return propertyName;
			}
			
		}
		virtual public MutablePropertiesContainer Parent
		{
			get
			{
				return parent;
			}
			
		}

		private string value_Renamed = "";
		private string propertyName;
		private MutablePropertiesContainer parent;
		
		// Maintain a static list of all Global Properties known to module
		private static List < Impl > allProperties = new List < Impl >();

		public static List < Impl > getAllProperties()
		{
			return allProperties;
		}

		/// <summary> </summary>
		/// <param name="source">
		/// will be the source of any {@link PropertyChangeEvent} fired by this object
		/// </param>
		public Impl(string propertyName, System.Object source)
		{
			this.propertyName = propertyName;
		}
		
		public virtual void  addMutablePropertyChangeListener(PropertyChangeEventHandler l)
		{
			PropertyChange += l;
		}

		public virtual void removeMutablePropertyChangeListener(PropertyChangeEventHandler l)
		{
			PropertyChange -= l;
		}

		public virtual void  addTo(MutablePropertiesContainer c)
		{
			parent = c;
			parent.addMutableProperty(propertyName, this);
			allProperties.Add(this);
		}
		
		public virtual void  removeFromContainer()
		{
			if (parent != null)
			{
				parent.removeMutableProperty(propertyName);
				allProperties.Remove(this);
			}
		}
		
		public virtual string getPropertyValue()
		{
			return value_Renamed;
		}
		
		protected internal virtual Command getChangeCommand(string oldValue, string newValue)
		{
			return new ChangePropertyCommand(this, propertyName, oldValue, newValue);
		}
		
		public virtual Command setPropertyValue(string newValue)
		{
			if (newValue == null)
			{
				newValue = "";
			}
			string oldValue = value_Renamed;
			Command c = getChangeCommand(value_Renamed, newValue);
			value_Renamed = newValue;
			SupportClass.PropertyChangingEventArgs me2 = new SupportClass.PropertyChangingEventArgs(propertyName, oldValue, newValue);
			if (PropertyChange != null)
				PropertyChange(this, me2);
			return c;
		}
	}
}