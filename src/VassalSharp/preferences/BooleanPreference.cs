/*
* $Id$
*
* Copyright (c) 2000-2006 by Rodney Kinney, Brent Easton
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
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using Configurer = VassalSharp.configure.Configurer;
namespace VassalSharp.preferences
{
	
	/// <summary> A Checkbox Module Preference.</summary>
	public class BooleanPreference:BasicPreference
	{
		public BooleanPreference()
		{
			InitBlock();
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(BooleanPreference enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BooleanPreference enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BooleanPreference enclosingInstance;
			public BooleanPreference Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				updateGlobalProperty(Enclosing_Instance.config.ValueString);
			}
		}
		private void  InitBlock()
		{
			return typeof(System.Boolean);
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return "Checkbox Preference";
			}
			
		}
		virtual public Configurer PreferenceConfigurer
		{
			get
			{
				if (config == null)
				{
					config = new BooleanConfigurer(getVariableName(), getDescription(), defaultValue);
					config.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				}
				return config;
			}
			
		}
		
		protected internal bool defaultValue = false;
		new protected internal BooleanConfigurer config;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > getDefaultClass()
		
		public virtual System.String getDefaultValue()
		{
			return Boolean.toString(defaultValue);
		}
		
		public virtual void  setDefaultValue(System.Object value_Renamed)
		{
			if (value_Renamed is System.String)
			{
				//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.lang.Boolean.valueOf' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
				value_Renamed = System.Boolean.Parse((System.String) value_Renamed);
			}
			defaultValue = ((System.Boolean) value_Renamed);
		}
	}
}