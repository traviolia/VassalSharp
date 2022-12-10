/*
* $Id$
*
* Copyright (c) 2000-2013 by Brent Easton, Rodney Kinney
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
using GameModule = VassalSharp.build.GameModule;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using Zone = VassalSharp.build.module.map.boardPicker.board.mapgrid.Zone;
using MutablePropertiesContainer = VassalSharp.build.module.properties.MutablePropertiesContainer;
using MutableProperty = VassalSharp.build.module.properties.MutableProperty;
using Command = VassalSharp.command.Command;
using NullCommand = VassalSharp.command.NullCommand;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using Configurer = VassalSharp.configure.Configurer;
using FormattedExpressionConfigurer = VassalSharp.configure.FormattedExpressionConfigurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using ListConfigurer = VassalSharp.configure.ListConfigurer;
using PropertyNameExpressionConfigurer = VassalSharp.configure.PropertyNameExpressionConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using StringEnumConfigurer = VassalSharp.configure.StringEnumConfigurer;
using FormattedString = VassalSharp.tools.FormattedString;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> </summary>
	/// <author>  Brent Easton
	/// 
	/// A trait that allows counters to manipulate the value of Global properties.
	/// Uses the Property manipulation functionality of DynamicPropert, but
	/// applies them to Global Properties.
	/// </author>
	public class SetGlobalProperty:DynamicProperty
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: Constructor 'java.beans.PropertyChangeSupport.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
			propertyChangeSupport = new PropertyChangeSupport(this);
		}
		[System.ComponentModel.Browsable(true)]
		public new  event SupportClass.PropertyChangeEventHandler PropertyChange;
		override public System.String Description
		{
			get
			{
				System.String s = "Set Global Property";
				if (description.Length > 0)
				{
					s += (" - " + description);
				}
				return s;
			}
			
		}
		override public HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("SetGlobalProperty.htm");
			}
			
		}
		//UPGRADE_ISSUE: Class 'java.beans.PropertyChangeSupport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javabeansPropertyChangeSupport'"
		//UPGRADE_NOTE: The initialization of  'propertyChangeSupport' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal PropertyChangeSupport propertyChangeSupport;
		new public const System.String ID = "setprop;";
		public const System.String CURRENT_ZONE = "Current Zone/Current Map/Module";
		public const System.String NAMED_ZONE = "Named Zone";
		public const System.String NAMED_MAP = "Named Map";
		protected internal System.String description;
		protected internal System.String propertyLevel;
		protected internal System.String searchName;
		protected internal Decorator dec;
		
		public SetGlobalProperty():this(ID, null)
		{
		}
		
		public SetGlobalProperty(System.String type, GamePiece p):base(type, p)
		{
			InitBlock();
		}
		
		public override void  mySetType(System.String s)
		{
			SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, ';');
			sd.nextToken(); // Skip over command prefix
			key = sd.nextToken("name");
			decodeConstraints(sd.nextToken(""));
			keyCommandListConfig.setValue(sd.nextToken(""));
			keyCommands = keyCommandListConfig.getListValue().toArray(new DynamicKeyCommand[keyCommandListConfig.getListValue().size()]);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < DynamicKeyCommand > l = new ArrayList < DynamicKeyCommand >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(DynamicKeyCommand dkc: keyCommands)
			{
				if (dkc.getName() != null && dkc.getName().length() > 0)
				{
					l.add(dkc);
				}
			}
			menuCommands = l.toArray(new DynamicKeyCommand[l.size()]);
			description = sd.nextToken("");
			propertyLevel = sd.nextToken(CURRENT_ZONE);
			searchName = sd.nextToken("");
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(key);
			se.append(encodeConstraints());
			se.append(keyCommandListConfig.ValueString);
			se.append(description);
			se.append(propertyLevel);
			se.append(searchName);
			return ID + se.Value;
		}
		
		public override System.String myGetState()
		{
			return "";
		}
		
		public override void  mySetState(System.String state)
		{
		}
		
		/*
		* Duplicate code from Decorator for setProperty(), getProperty() Do not call super.xxxProperty() as we no longer
		* contain a DynamicProperty that can be manipulated, but you cannot call super.super.xxxProperty().
		*/
		public override System.Object getProperty(System.Object key)
		{
			if (VassalSharp.counters.Properties_Fields.KEY_COMMANDS.Equals(key))
			{
				return getKeyCommands();
			}
			else if (VassalSharp.counters.Properties_Fields.INNER.Equals(key))
			{
				return piece;
			}
			else if (VassalSharp.counters.Properties_Fields.OUTER.Equals(key))
			{
				return dec;
			}
			else if (VassalSharp.counters.Properties_Fields.VISIBLE_STATE.Equals(key))
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return myGetState() + piece.getProperty(key);
			}
			else
			{
				return piece.getProperty(key);
			}
		}
		
		public override System.Object getLocalizedProperty(System.Object key)
		{
			if (VassalSharp.counters.Properties_Fields.KEY_COMMANDS.Equals(key))
			{
				return getProperty(key);
			}
			else if (VassalSharp.counters.Properties_Fields.INNER.Equals(key))
			{
				return getProperty(key);
			}
			else if (VassalSharp.counters.Properties_Fields.OUTER.Equals(key))
			{
				return getProperty(key);
			}
			else if (VassalSharp.counters.Properties_Fields.VISIBLE_STATE.Equals(key))
			{
				return getProperty(key);
			}
			else
			{
				return piece.getLocalizedProperty(key);
			}
		}
		
		public override void  setProperty(System.Object key, System.Object val)
		{
			if (VassalSharp.counters.Properties_Fields.INNER.Equals(key))
			{
				setInner((GamePiece) val);
			}
			else if (VassalSharp.counters.Properties_Fields.OUTER.Equals(key))
			{
				dec = (Decorator) val;
			}
			else
			{
				piece.setProperty(key, val);
			}
		}
		
		/*
		* Locate the correct Global Variable to adjust and update its value. The named global variables must already be
		* defined in the appropriate component before a counter can update them. $xxxx$ names are allowed in both the
		* property name and the target containing map/zone name.
		*/
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			Command comm = new NullCommand();
			for (int i = 0; i < keyCommands.Length; i++)
			{
				if (keyCommands[i].matches(stroke))
				{
					MutableProperty prop = null;
					System.String propertyName = (new FormattedString(key)).getText(Decorator.getOutermost(this));
					
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					ArrayList < MutablePropertiesContainer > propertyContainers = 
					new ArrayList < MutablePropertiesContainer >();
					propertyContainers.add(0, GameModule.getGameModule());
					Map map = getMap();
					if (NAMED_MAP.Equals(propertyLevel))
					{
						System.String mapName = (new FormattedString(searchName)).getText(Decorator.getOutermost(this));
						map = Map.getMapById(mapName);
					}
					if (map != null)
					{
						propertyContainers.add(0, map);
					}
					Zone z = null;
					if (CURRENT_ZONE.Equals(propertyLevel) && getMap() != null)
					{
						System.Drawing.Point tempAux = Position;
						//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
						z = getMap().findZone(ref tempAux);
					}
					else if (NAMED_ZONE.Equals(propertyLevel) && getMap() != null)
					{
						System.String zoneName = (new FormattedString(searchName)).getText(Decorator.getOutermost(this));
						z = getMap().findZone(zoneName);
					}
					if (z != null)
					{
						propertyContainers.add(0, z);
					}
					prop = MutableProperty.Util.findMutableProperty(propertyName, propertyContainers);
					/*
					* Debugging could be painful, so print a useful message in the
					* Chat Window if no property can be found to update
					*/
					if (prop == null)
					{
						System.String s = "Set Global Property (" + description + "): Unable to locate Global Property named " + propertyName;
						if (!propertyLevel.Equals(CURRENT_ZONE))
						{
							s += (" in " + propertyLevel + " " + searchName);
						}
						GameModule.getGameModule().warn(s);
					}
					else
					{
						System.String oldValue = prop.getPropertyValue();
						System.String newValue = keyCommands[i].propChanger.getNewValue(oldValue);
						format.Format = newValue;
						newValue = format.getText(Decorator.getOutermost(this));
						comm = prop.setPropertyValue(newValue);
					}
				}
			}
			return comm;
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		new protected internal class Ed : PieceEditor
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassListConfigurer1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassListConfigurer1:ListConfigurer
			{
				private void  InitBlock(VassalSharp.counters.SetGlobalProperty m, Ed enclosingInstance)
				{
					this.m = m;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable m was copied into class AnonymousClassListConfigurer1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.counters.SetGlobalProperty m;
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				internal AnonymousClassListConfigurer1(VassalSharp.counters.SetGlobalProperty m, Ed enclosingInstance, System.String Param1, System.String Param2):base(Param1, Param2)
				{
					InitBlock(m, enclosingInstance);
				}
				protected internal override Configurer buildChildConfigurer()
				{
					return new DynamicKeyCommandConfigurer(m);
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener
			{
				public AnonymousClassPropertyChangeListener(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
				{
					bool isNumeric = Enclosing_Instance.numericConfig.booleanValue();
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(Enclosing_Instance.minConfig.Controls, "Visible", isNumeric);
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(Enclosing_Instance.maxConfig.Controls, "Visible", isNumeric);
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(Enclosing_Instance.wrapConfig.Controls, "Visible", isNumeric);
					Enclosing_Instance.keyCommandListConfig.repack();
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener1
			{
				public AnonymousClassPropertyChangeListener1(Ed enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Ed enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
				{
					Enclosing_Instance.updateVisibility();
				}
			}
			private void  InitBlock()
			{
				System.Windows.Forms.Label temp_label;
				temp_label = new System.Windows.Forms.Label();
				temp_label.Text = "map";
				mapLabel = temp_label;
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "zone";
				zoneLabel = temp_label2;
			}
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return controls;
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(nameConfig.ValueString);
					se.append(encodeConstraints());
					se.append(keyCommandListConfig.ValueString);
					se.append(descConfig.ValueString);
					se.append(levelConfig.ValueString);
					se.append(searchNameConfig.ValueString);
					return VassalSharp.counters.SetGlobalProperty.ID + se.Value;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return "";
				}
				
			}
			protected internal StringConfigurer descConfig;
			protected internal PropertyNameExpressionConfigurer nameConfig;
			protected internal BooleanConfigurer numericConfig;
			protected internal IntConfigurer minConfig;
			protected internal IntConfigurer maxConfig;
			protected internal BooleanConfigurer wrapConfig;
			protected internal ListConfigurer keyCommandListConfig;
			protected internal StringEnumConfigurer levelConfig;
			protected internal FormattedExpressionConfigurer searchNameConfig;
			//UPGRADE_NOTE: The initialization of  'mapLabel' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			protected internal System.Windows.Forms.Label mapLabel;
			//UPGRADE_NOTE: The initialization of  'zoneLabel' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			protected internal System.Windows.Forms.Label zoneLabel;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			protected internal Box controls;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			protected internal Box nameBox;
			
			public Ed(SetGlobalProperty m)
			{
				keyCommandListConfig = new AnonymousClassListConfigurer1(m, this, null, "Key Commands");
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				keyCommandListConfig.setValue(
				new ArrayList < DynamicKeyCommand >(Arrays.asList(m.keyCommands)));
				//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				PropertyChangeListener l = new AnonymousClassPropertyChangeListener(this);
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				controls = Box.createVerticalBox();
				descConfig = new StringConfigurer(null, "Description:  ", m.description);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(descConfig.Controls);
				nameConfig = new PropertyNameExpressionConfigurer(null, "Global Property Name:  ", m.Key, (EditablePiece) m);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(nameConfig.Controls);
				levelConfig = new StringEnumConfigurer(null, "", new System.String[]{VassalSharp.counters.SetGlobalProperty.CURRENT_ZONE, VassalSharp.counters.SetGlobalProperty.NAMED_ZONE, VassalSharp.counters.SetGlobalProperty.NAMED_MAP});
				levelConfig.setValue(m.propertyLevel);
				levelConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener1(this).propertyChange);
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box box = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Locate Property starting in the:   ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				box.Controls.Add(temp_Control);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(levelConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(box);
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				nameBox = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label4;
				temp_label4 = new System.Windows.Forms.Label();
				temp_label4.Text = "Name of ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = temp_label4;
				nameBox.Controls.Add(temp_Control2);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				nameBox.Controls.Add(mapLabel);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				nameBox.Controls.Add(zoneLabel);
				System.Windows.Forms.Label temp_label6;
				temp_label6 = new System.Windows.Forms.Label();
				temp_label6.Text = " containing property:  ";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control3;
				temp_Control3 = temp_label6;
				nameBox.Controls.Add(temp_Control3);
				searchNameConfig = new FormattedExpressionConfigurer(null, "", m.searchName, (EditablePiece) m);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				nameBox.Controls.Add(searchNameConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(nameBox);
				numericConfig = new BooleanConfigurer(null, "Is numeric?", m.Numeric);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(numericConfig.Controls);
				minConfig = new IntConfigurer(null, "Minimum value:  ", m.MinimumValue);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(minConfig.Controls);
				maxConfig = new IntConfigurer(null, "Maximum value:  ", m.MaximumValue);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(maxConfig.Controls);
				wrapConfig = new BooleanConfigurer(null, "Wrap?", m.Wrap);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(wrapConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(keyCommandListConfig.Controls);
				numericConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(l.propertyChange);
				numericConfig.fireUpdate();
				updateVisibility();
			}
			
			protected internal virtual void  updateVisibility()
			{
				mapLabel.Visible = levelConfig.ValueString.Equals(VassalSharp.counters.SetGlobalProperty.NAMED_MAP);
				zoneLabel.Visible = levelConfig.ValueString.Equals(VassalSharp.counters.SetGlobalProperty.NAMED_ZONE);
				nameBox.Visible = !levelConfig.ValueString.Equals(VassalSharp.counters.SetGlobalProperty.CURRENT_ZONE);
				//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
				System.Windows.Forms.Form w = (System.Windows.Forms.Form) controls.Parent;
				if (w != null)
				{
					//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
					w.pack();
				}
			}
			
			protected internal virtual System.String encodeConstraints()
			{
				return new SequenceEncoder(',').append(numericConfig.ValueString).append(minConfig.ValueString).append(maxConfig.ValueString).append(wrapConfig.ValueString).Value;
			}
		}
	}
}