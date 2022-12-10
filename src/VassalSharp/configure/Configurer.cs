/*
* $Id$
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
using static SupportClass;

namespace VassalSharp.configure
{
	
	/// <summary> A property editor class.  Wraps an Object value and provides
	/// methods for saving and restoring the Object from a String.  Also
	/// includes a {@link java.awt.Component} that can be placed into a
	/// property editing window, allowing the user to change the value
	/// interactively.
	/// 
	/// </summary>
	public abstract class Configurer
	{
		[System.ComponentModel.Browsable(true)]
		public  event SupportClass.PropertyChangeEventHandler PropertyChange;
		/// <summary> Unique identifier</summary>
		virtual public string Key
		{
			get
			{
				return key;
			}
			
		}
		/// <returns> a String representation of the Object value
		/// </returns>
		public abstract string ValueString{get;}
		/// <summary> If true, then don't fire PropertyChangeEvents when the value is reset</summary>
		virtual public bool Frozen
		{
			get
			{
				return frozen;
			}
			
			set
			{
				frozen = value;
			}
			
		}
		/// <summary> GUI interface for setting the option in an editing window</summary>
		public abstract System.Windows.Forms.Control Controls{get;}
		// FIXME: maybe parameterize this so that value can have the right type
		// in subclasses?
		public const string NAME_PROPERTY = "Configurer.name";
		//    public static final String VALUE_PROPERTY = "value";
		
		/// <summary>A String the uniquely identifies this property </summary>
		protected internal string key;
		/// <summary>A String that provides a short description of the property to the user </summary>
		protected internal string name;
		/// <summary>The value </summary>
		protected internal System.Object value_Renamed;
		/// <summary>When noUpdate is true, setting the value programmatically will not
		/// result in an update of the GUI Component 
		/// </summary>
		protected internal bool noUpdate = false;
		/// <summary>When frozen is true, setting the value programmatically will not
		/// result in a PropertyChangeEvent being fired 
		/// </summary>
		protected internal bool frozen = false;
		
		public Configurer(string key, string name):this(key, name, null)
		{
		}
		
		public Configurer(string key, string name, System.Object val)
		{
			this.key = key;
			this.name = name;
			setValue(val);
		}
		
		/// <summary> Plain English description of the Object</summary>
		public virtual string getName()
		{
			return name;
		}
		
		public virtual void  setName(string s)
		{
			string oldName = name;
			name = s;
			if (!frozen)
			{
				SupportClass.PropertyChangingEventArgs me43 = new SupportClass.PropertyChangingEventArgs(NAME_PROPERTY, oldName, name);
				if (PropertyChange != null)
					PropertyChange(this, me43);
			}
		}
		
		/// <summary> The Object value
		/// May be null if the Object has not been initialized
		/// </summary>
		public virtual System.Object getValue()
		{
			return value_Renamed;
		}
		
		/// <summary> Set the Object value</summary>
		public virtual void  setValue(System.Object o)
		{
			System.Object oldValue = getValue();
			value_Renamed = o;
			if (!frozen)
			{
				SupportClass.PropertyChangingEventArgs me44 = new SupportClass.PropertyChangingEventArgs(key, oldValue, value_Renamed);
				if (PropertyChange != null)
					PropertyChange(this, me44);
			}
		}
		
		/// <summary> Fire a PropertyChangeEvent as if the value had been set from null</summary>
		public virtual void  fireUpdate()
		{
			SupportClass.PropertyChangingEventArgs me45 = new SupportClass.PropertyChangingEventArgs(key, null, value_Renamed);
			if (PropertyChange != null)
				PropertyChange(this, me45);
		}
		
		/// <summary> Set the Object value from a String</summary>
		public abstract void  setValue(string s);
		
		/// <summary> Add a listener to be notified when the Object state changes</summary>
		public virtual void  addPropertyChangeListener(PropertyChangeEventHandler l)
		{
			PropertyChange += l; 
		}
		
		public virtual void  removePropertyChangeListener(PropertyChangeEventHandler l)
		{
			PropertyChange -= l;
		}
	}
}