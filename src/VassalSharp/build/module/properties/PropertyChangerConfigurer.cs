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
using StringEnumConfigurer = VassalSharp.build.module.gamepieceimage.StringEnumConfigurer;
using Configurer = VassalSharp.configure.Configurer;
using FormattedExpressionConfigurer = VassalSharp.configure.FormattedExpressionConfigurer;
using FormattedStringArrayConfigurer = VassalSharp.configure.FormattedStringArrayConfigurer;
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.build.module.properties
{
	
	/// <summary> Configurer instance that allows a module editor to specify a
	/// PropertyChanger, i.e. the way in which a dynamic property will be
	/// updated by a player during a game
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class PropertyChangerConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(PropertyChangerConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PropertyChangerConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PropertyChangerConfigurer enclosingInstance;
			public PropertyChangerConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				Enclosing_Instance.updateValue();
				Enclosing_Instance.updateControls();
			}
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (controls == null)
				{
					//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					PropertyChangeListener l = new AnonymousClassPropertyChangeListener(this);
					controls = new System.Windows.Forms.Panel();
					//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					controls.setLayout(new BoxLayout(controls, BoxLayout.X_AXIS));
					typeConfig = new StringEnumConfigurer(null, "Type:  ", new System.String[]{PLAIN_TYPE, INCREMENT_TYPE, PROMPT_TYPE, SELECT_TYPE});
					typeConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(l.propertyChange);
					valueConfig = new FormattedExpressionConfigurer(null, "New Value:  ", "", constraints);
					valueConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(l.propertyChange);
					promptConfig = new StringConfigurer(null, "Prompt:  ");
					promptConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(l.propertyChange);
					incrConfig = new FormattedExpressionConfigurer(null, "Increment by:  ", "", constraints);
					incrConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(l.propertyChange);
					validValuesConfig = new FormattedStringArrayConfigurer(null, "Valid Values", constraints);
					validValuesConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(l.propertyChange);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(typeConfig.Controls);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(valueConfig.Controls);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(promptConfig.Controls);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(incrConfig.Controls);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(validValuesConfig.Controls);
					updateControls();
				}
				return controls;
			}
			
		}
		override public System.String ValueString
		{
			get
			{
				PropertyChanger propChanger = PropertyChanger;
				SequenceEncoder se = new SequenceEncoder(',');
				if (propChanger != null)
				{
					switch (typeToCode.get_Renamed(propChanger.GetType()).charValue())
					{
						
						case PROMPT_CODE: 
							se.append(PROMPT_CODE).append(((PropertyPrompt) propChanger).Prompt);
							break;
						
						case INCR_CODE: 
							se.append(INCR_CODE).append(((IncrementProperty) propChanger).Increment);
							break;
						
						case ENUM_CODE: 
							se.append(ENUM_CODE).append(((PropertyPrompt) propChanger).Prompt).append(((EnumeratedPropertyPrompt) propChanger).ValidValues);
							break;
						
						case PLAIN_CODE: 
							se.append(PLAIN_CODE).append(((PropertySetter) propChanger).RawValue);
							break;
						}
				}
				return se.Value;
			}
			
		}
		virtual public PropertyChanger PropertyChanger
		{
			get
			{
				return (PropertyChanger) getValue();
			}
			
		}
		protected internal const System.String PLAIN_TYPE = "Set value directly";
		protected internal const System.String INCREMENT_TYPE = "Increment numeric value";
		protected internal const System.String PROMPT_TYPE = "Prompt user";
		protected internal const System.String SELECT_TYPE = "Prompt user to select from list";
		protected internal const char PLAIN_CODE = 'P';
		protected internal const char PROMPT_CODE = 'R';
		protected internal const char ENUM_CODE = 'E';
		protected internal const char INCR_CODE = 'I';
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static final Map < Class < ? extends PropertyChanger >, Character > typeToCode = 
		new HashMap < Class < ? extends PropertyChanger >, Character >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static final Map < Class < ? extends PropertyChanger >, String > typeToDescription = 
		new HashMap < Class < ? extends PropertyChanger >, String >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static final Map < String, Character > descriptionToCode = 
		new HashMap < String, Character >();
		protected internal PropertyChangerConfigurer.Constraints constraints;
		protected internal System.Windows.Forms.Panel controls;
		protected internal StringEnumConfigurer typeConfig;
		protected internal FormattedExpressionConfigurer valueConfig;
		protected internal StringConfigurer promptConfig;
		protected internal FormattedExpressionConfigurer incrConfig;
		protected internal StringArrayConfigurer validValuesConfig;
		
		public PropertyChangerConfigurer(System.String key, System.String name, PropertyChangerConfigurer.Constraints constraints):base(key, name)
		{
			this.constraints = constraints;
			setValue(new PropertySetter("", null));
		}
		
		protected internal virtual void  updateControls()
		{
			PropertyChanger pc = PropertyChanger;
			typeConfig.setValue(typeToDescription.get_Renamed(pc.GetType()));
			if (pc is PropertySetter)
			{
				valueConfig.setValue(((PropertySetter) pc).RawValue);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(valueConfig.Controls, "Visible", true);
			}
			else
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(valueConfig.Controls, "Visible", false);
			}
			if (pc is IncrementProperty)
			{
				incrConfig.setValue(System.Convert.ToString(((IncrementProperty) pc).Increment));
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(incrConfig.Controls, "Visible", true);
			}
			else
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(incrConfig.Controls, "Visible", false);
			}
			if (pc is PropertyPrompt)
			{
				promptConfig.setValue(((PropertyPrompt) pc).Prompt);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(promptConfig.Controls, "Visible", true);
			}
			else
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(promptConfig.Controls, "Visible", false);
			}
			if (pc is EnumeratedPropertyPrompt)
			{
				validValuesConfig.setValue(((EnumeratedPropertyPrompt) pc).ValidValues);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(validValuesConfig.Controls, "Visible", true);
			}
			else
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(validValuesConfig.Controls, "Visible", false);
			}
		}
		
		protected internal virtual void  updateValue()
		{
			PropertyChanger p;
			switch (descriptionToCode.get_Renamed(typeConfig.ValueString).charValue())
			{
				
				case PROMPT_CODE: 
					p = new PropertyPrompt(constraints, promptConfig.ValueString);
					break;
				
				case INCR_CODE: 
					p = new IncrementProperty(this, incrConfig.ValueString, constraints);
					break;
				
				case ENUM_CODE: 
					p = new EnumeratedPropertyPrompt(constraints, promptConfig.ValueString, validValuesConfig.StringArray, constraints);
					break;
				
				case PLAIN_CODE: 
				default: 
					p = new PropertySetter(valueConfig.ValueString, constraints);
					break;
				}
			setValue(p);
		}
		
		public override void  setValue(System.String s)
		{
			PropertyChanger p;
			if (s == null || s.Length == 0)
			{
				s = Character.toString(PLAIN_CODE);
			}
			SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, ',');
			switch (sd.nextChar(PLAIN_CODE))
			{
				
				case PROMPT_CODE: 
					p = new PropertyPrompt(constraints, sd.nextToken("Enter new value"));
					break;
				
				case INCR_CODE: 
					p = new IncrementProperty(this, sd.nextToken("1"), constraints);
					break;
				
				case ENUM_CODE: 
					p = new EnumeratedPropertyPrompt(constraints, sd.nextToken("Select new value"), sd.nextStringArray(0), constraints);
					break;
				
				case PLAIN_CODE: 
				default: 
					p = new PropertySetter(sd.nextToken("new value"), constraints);
					break;
				}
			setValue(p);
		}
		public interface Constraints:PropertyPrompt.Constraints, IncrementProperty.Constraints, PropertySource
		{
		}
		static PropertyChangerConfigurer()
		{
			{
				typeToCode.put(typeof(PropertySetter), PLAIN_CODE);
				typeToCode.put(typeof(PropertyPrompt), PROMPT_CODE);
				typeToCode.put(typeof(NumericPropertyPrompt), PROMPT_CODE);
				typeToCode.put(typeof(IncrementProperty), INCR_CODE);
				typeToCode.put(typeof(EnumeratedPropertyPrompt), ENUM_CODE);
				typeToDescription.put(typeof(PropertySetter), PLAIN_TYPE);
				typeToDescription.put(typeof(PropertyPrompt), PROMPT_TYPE);
				typeToDescription.put(typeof(NumericPropertyPrompt), PROMPT_TYPE);
				typeToDescription.put(typeof(IncrementProperty), INCREMENT_TYPE);
				typeToDescription.put(typeof(EnumeratedPropertyPrompt), SELECT_TYPE);
				descriptionToCode.put(PLAIN_TYPE, PLAIN_CODE);
				descriptionToCode.put(INCREMENT_TYPE, INCR_CODE);
				descriptionToCode.put(PROMPT_TYPE, PROMPT_CODE);
				descriptionToCode.put(SELECT_TYPE, ENUM_CODE);
			}
		}
	}
}