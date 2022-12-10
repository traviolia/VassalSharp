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
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
using StringEnumConfigurer = VassalSharp.configure.StringEnumConfigurer;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
namespace VassalSharp.preferences
{
	
	/// <summary> A Drop-down list preference.</summary>
	public class EnumPreference:BasicPreference
	{
		public EnumPreference()
		{
			InitBlock();
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(EnumPreference enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(EnumPreference enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private EnumPreference enclosingInstance;
			public EnumPreference Enclosing_Instance
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
			return typeof(System.String);
			return ArrayUtils.append(base.getAttributeTypes(), typeof(System.String[]));
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return "Drop-down List Preference";
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return ArrayUtils.append(base.AttributeNames, LIST);
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return ArrayUtils.append(base.AttributeDescriptions, "List Values:  ");
			}
			
		}
		virtual public Configurer PreferenceConfigurer
		{
			get
			{
				if (config == null)
				{
					config = new StringEnumConfigurer(getVariableName(), getDescription(), options);
					config.setValue(defaultValue);
					config.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				}
				return config;
			}
			
		}
		
		public const System.String LIST = "list";
		
		protected internal System.String defaultValue = "";
		protected internal System.String[] options = new System.String[0];
		new protected internal StringEnumConfigurer config;
		
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
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		
		public override void  setAttribute(System.String key, System.Object value_Renamed)
		{
			if (LIST.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					value_Renamed = StringArrayConfigurer.stringToArray((System.String) value_Renamed);
				}
				options = (System.String[]) value_Renamed;
				if (config != null)
				{
					config.setValidValues(options);
				}
			}
			else
				base.setAttribute(key, value_Renamed);
		}
		
		public override System.String getAttributeValueString(System.String key)
		{
			if (LIST.Equals(key))
			{
				return StringArrayConfigurer.arrayToString(options);
			}
			else
				return base.getAttributeValueString(key);
		}
	}
}