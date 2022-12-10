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
using System.Collections.Generic;
using AutoConfigurable = VassalSharp.build.AutoConfigurable;
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
//using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
//using ReflectionUtils = VassalSharp.tools.ReflectionUtils;

namespace VassalSharp.configure
{
	
	/// <summary> A Configurer for configuring Configurable components
	/// (Is that as redundant as it sounds?)
	/// Automatically builds a property editor with controls for setting all
	/// of the attributes of the target Configurable component
	/// </summary>
	public class AutoConfigurer : Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(AutoConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(AutoConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private AutoConfigurer enclosingInstance;
			public AutoConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				if (VassalSharp.build.Configurable_Fields.NAME_PROPERTY.Equals(evt.PropertyName))
				{
					Enclosing_Instance.setName((string) evt.NewValue);
				}
			}
		}

		override public string ValueString
		{
			get
			{
				return target.getConfigureName();
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				return p;
			}
			
		}
		protected internal System.Windows.Forms.Panel p;
		protected internal AutoConfigurable target;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < Configurer > configurers = new List < Configurer >();
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected Dictionary < String, VisibilityCondition > conditions;
		
		public AutoConfigurer(AutoConfigurable c) : base(null, c.getConfigureName())
		{			
			target = c;
			setValue(target);
#if NEVER_DEFINED
			target.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
			
			p = new System.Windows.Forms.Panel();
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			p.setLayout(new BoxLayout(p, BoxLayout.Y_AXIS));
			
			string[] name = c.AttributeNames;
			string[] prompt = c.AttributeDescriptions;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Type [] type = c.getAttributeTypes();
			
			int n = Math.Min(name.Length, Math.Min(prompt.Length, type.Length));
			for (int i = 0; i < n; ++i)
			{
				if (type[i] == null)
				{
					continue;
				}
				Configurer config;
				config = createConfigurer(type[i], name[i], prompt[i], target);
				if (config != null)
				{
					config.PropertyChange += new SupportClass.PropertyChangeEventHandler(this.propertyChange);
					config.setValue(target.getAttributeValueString(name[i]));
					//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					Box box = Box.createHorizontalBox();
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					box.Controls.Add(config.Controls);
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalGlue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = Box.createHorizontalGlue();
					box.Controls.Add(temp_Control);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(box);
					configurers.add(config);
				}
				setVisibility(name[i], c.getAttributeVisibility(name[i]));
			}
#endif
		}

		public static Configurer createConfigurer(Type type, String key, String prompt, AutoConfigurable target)
		{
#if NEVER_DEFINED
			Configurer config = null;
			
			if (typeof(string).IsAssignableFrom(type))
			{
				config = new StringConfigurer(key, prompt);
			}
			else if (typeof(System.Int32).IsAssignableFrom(type))
			{
				config = new IntConfigurer(key, prompt);
			}
			else if (typeof(System.Double).IsAssignableFrom(type))
			{
				config = new DoubleConfigurer(key, prompt);
			}
			else if (typeof(System.Boolean).IsAssignableFrom(type))
			{
				config = new BooleanConfigurer(key, prompt);
			}
			else if (typeof(System.Drawing.Image).IsAssignableFrom(type))
			{
				config = new ImageConfigurer(key, prompt, GameModule.getGameModule().getArchiveWriter());
			}
			else if (typeof(System.Drawing.Color).IsAssignableFrom(type))
			{
				config = new ColorConfigurer(key, prompt);
			}
			else if (typeof(System.Windows.Forms.KeyEventArgs).IsAssignableFrom(type))
			{
				config = new HotKeyConfigurer(key, prompt);
			}
			else if (typeof(NamedKeyStroke).IsAssignableFrom(type))
			{
				config = new NamedHotKeyConfigurer(key, prompt);
			}
			else if (typeof(System.IO.FileInfo).IsAssignableFrom(type))
			{
				config = new FileConfigurer(key, prompt, GameModule.getGameModule().getArchiveWriter());
			}
			else if (typeof(string[]).IsAssignableFrom(type))
			{
				config = new StringArrayConfigurer(key, prompt);
			}
			else if (typeof(System.Drawing.Image).IsAssignableFrom(type))
			{
				config = new IconConfigurer(key, prompt, null);
			}
			else if (typeof(PropertyExpression).IsAssignableFrom(type))
			{
				config = new PropertyExpressionConfigurer(key, prompt);
			}
			else if (typeof(StringEnum).IsAssignableFrom(type))
			{
				StringEnum se = null;
				try
				{
					se = (StringEnum) type.getConstructor().newInstance();
				}
				//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception t)
				{
					ReflectionUtils.handleNewInstanceFailure(t, type);
					config = new StringConfigurer(key, prompt);
				}
				
				if (se != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'validValues '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					string[] validValues = se.getValidValues(target);
					config = new StringEnumConfigurer(key, prompt, validValues);
				}
			}
			else if (typeof(ConfigurerFactory).IsAssignableFrom(type))
			{
				ConfigurerFactory cf = null;
				try
				{
					cf = (ConfigurerFactory) type.getConstructor().newInstance();
				}
				//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
				catch (System.Exception t)
				{
					ReflectionUtils.handleNewInstanceFailure(t, type);
				}
				
				if (cf != null)
				{
					config = cf.getConfigurer(target, key, prompt);
				}
			}
			else
			{
				throw new System.ArgumentException("Invalid class " + type.getName());
			}
			return config;
#endif
			return null;
		}

		public virtual void  reset()
		{
			string[] s = target.AttributeNames;
			for (int i = 0; i < s.Length; ++i)
			{
				Configurer config = getConfigurer(s[i]);
				if (config != null)
				{
					config.setValue(target.getAttributeValueString(s[i]));
				}
			}
		}
		
		public override void  setValue(string s)
		{
			throw new System.NotSupportedException("Can't set Configurable from String");
		}
		
		public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
		{
			target.SetAttribute(evt.PropertyName, evt.NewValue);
			checkVisibility();
		}
		
		public virtual void  setVisibility(string attribute, VisibilityCondition c)
		{
			if (c != null)
			{
				if (conditions == null)
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					conditions = new Dictionary < String, VisibilityCondition >();
				}
				conditions.Add(attribute, c);
				checkVisibility();
			}
		}
		
		protected internal virtual void  checkVisibility()
		{
			bool visChanged = false;
			if (conditions != null)
			{
				foreach (Configurer c in configurers)
				{
					conditions.TryGetValue(c.Key, out VisibilityCondition cond);
					if (cond != null)
					{
						if (c.Controls.Visible != cond.shouldBeVisible())
						{
							visChanged = true;
							c.Controls.Visible = cond.shouldBeVisible();
						}
					}
				}
				// Only repack the configurer if an item visiblity has changed.
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
				if (visChanged && p.FindForm() is System.Windows.Forms.Form)
				{
					//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
					((System.Windows.Forms.Form) p.FindForm()).PerformLayout();
				}
			}
		}
		
		public virtual Configurer getConfigurer(string attribute)
		{
			foreach (Configurer c in configurers)
			{
				if (attribute.Equals(c.Key))
				{
					return c;
				}
			}
			return null;
		}
	}
}