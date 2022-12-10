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
using Configurer = VassalSharp.configure.Configurer;
using TextConfigurer = VassalSharp.configure.TextConfigurer;
namespace VassalSharp.preferences
{
	
	/// <summary> A multi-line textbox Module Preference.</summary>
	public class TextPreference:BasicPreference
	{
		public TextPreference()
		{
			InitBlock();
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(TextPreference enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TextPreference enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TextPreference enclosingInstance;
			public TextPreference Enclosing_Instance
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
			return typeof(TextConfigurer);
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return "Text Box Preference";
			}
			
		}
		virtual public Configurer PreferenceConfigurer
		{
			get
			{
				if (config == null)
				{
					config = new TextConfigurer(getVariableName(), getDescription(), defaultValue);
					config.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				}
				return config;
			}
			
		}
		
		protected internal System.String defaultValue = "";
		new protected internal TextConfigurer config;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > getDefaultClass()
		
		public virtual System.String getDefaultValue()
		{
			return defaultValue;
		}
		
		public virtual void  setDefaultValue(System.Object value_Renamed)
		{
			defaultValue = ((System.String) value_Renamed);
		}
	}
}