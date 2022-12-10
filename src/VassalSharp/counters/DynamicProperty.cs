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
using GameModule = VassalSharp.build.GameModule;
using Map = VassalSharp.build.module.Map;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using PropertyChanger = VassalSharp.build.module.properties.PropertyChanger;
using PropertyChangerConfigurer = VassalSharp.build.module.properties.PropertyChangerConfigurer;
using PropertyPrompt = VassalSharp.build.module.properties.PropertyPrompt;
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using ChangeTracker = VassalSharp.command.ChangeTracker;
using Command = VassalSharp.command.Command;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using Configurer = VassalSharp.configure.Configurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using ListConfigurer = VassalSharp.configure.ListConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using FormattedString = VassalSharp.tools.FormattedString;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> Trait that contains a property accessible via getProperty() and updateable
	/// dynamically via key commands
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class DynamicProperty:Decorator, TranslatablePiece, PropertyPrompt.DialogParent, PropertyChangerConfigurer.Constraints
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassListConfigurer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassListConfigurer:ListConfigurer
		{
			private void  InitBlock(DynamicProperty enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private DynamicProperty enclosingInstance;
			public DynamicProperty Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			internal AnonymousClassListConfigurer(DynamicProperty enclosingInstance, System.String Param1, System.String Param2):base(Param1, Param2)
			{
				InitBlock(enclosingInstance);
			}
			protected internal override Configurer buildChildConfigurer()
			{
				return new DynamicKeyCommandConfigurer(Enclosing_Instance);
			}
		}
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < String > l = new ArrayList < String >();
			l.add(key);
			return l;
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				return piece.Shape;
			}
			
		}
		virtual public System.Windows.Forms.Control Component
		{
			get
			{
				return getMap() != null?getMap().getView().getTopLevelAncestor():GameModule.getGameModule().getFrame();
			}
			
		}
		virtual public System.String Key
		{
			get
			{
				return key;
			}
			
		}
		virtual public System.String Value
		{
			get
			{
				return value_Renamed;
			}
			
			set
			{
				Stack parent = Parent;
				Map map = getMap();
				
				value = formatValue(value);
				
				// If the property has changed the layer to which this piece belongs,
				// re-insert it into the map.
				// No need to re-insert pieces in Decks, it causes problems if they are NO_STACK
				if (map != null && !(Parent is Deck))
				{
					
					GamePiece outer = Decorator.getOutermost(this);
					if (parent == null)
					{
						System.Drawing.Point pos = Position;
						map.removePiece(outer);
						this.value_Renamed = value;
						map.placeOrMerge(outer, pos);
					}
					else
					{
						GamePiece other = parent.getPieceBeneath(outer);
						if (other == null)
						{
							other = parent.getPieceAbove(outer);
						}
						if (other == null)
						{
							System.Drawing.Point pos = parent.Position;
							map.removePiece(parent);
							this.value_Renamed = value;
							map.placeOrMerge(parent, pos);
						}
						else
						{
							this.value_Renamed = value;
							if (!map.getPieceCollection().canMerge(other, outer))
							{
								map.placeOrMerge(outer, parent.Position);
							}
						}
					}
				}
				else
				{
					this.value_Renamed = value;
				}
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				System.String s = "Dynamic Property";
				if (Key != null && Key.Length > 0)
				{
					s += (" - " + Key);
				}
				return s;
			}
			
		}
		virtual public VassalSharp.build.module.documentation.HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("DynamicProperty.htm");
			}
			
		}
		virtual public int MaximumValue
		{
			get
			{
				return maxValue;
			}
			
		}
		virtual public int MinimumValue
		{
			get
			{
				return minValue;
			}
			
		}
		virtual public bool Numeric
		{
			get
			{
				return numeric;
			}
			
		}
		virtual public bool Wrap
		{
			get
			{
				return wrap;
			}
			
		}
		virtual public PropertySource PropertySource
		{
			get
			{
				return Decorator.getOutermost(this);
			}
			
		}
		
		public const System.String ID = "PROP;";
		
		protected internal System.String value_Renamed;
		
		protected internal System.String key;
		protected internal bool numeric;
		protected internal int minValue;
		protected internal int maxValue;
		protected internal bool wrap;
		protected internal FormattedString format = new FormattedString();
		
		protected internal DynamicKeyCommand[] keyCommands;
		protected internal KeyCommand[] menuCommands;
		
		protected internal ListConfigurer keyCommandListConfig;
		
		public DynamicProperty():this(ID, null)
		{
		}
		
		public DynamicProperty(System.String type, GamePiece p)
		{
			InitBlock();
			setInner(p);
			keyCommandListConfig = new AnonymousClassListConfigurer(this, null, "Commands");
			mySetType(type);
		}
		
		public virtual void  mySetType(System.String s)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'sd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, ';');
			sd.nextToken(); // Skip over command prefix
			key = sd.nextToken("name");
			decodeConstraints(sd.nextToken(""));
			keyCommandListConfig.setValue(sd.nextToken(""));
			keyCommands = keyCommandListConfig.getListValue().toArray(new DynamicKeyCommand[keyCommandListConfig.getListValue().size()]);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < DynamicKeyCommand > l = new ArrayList < DynamicKeyCommand >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(DynamicKeyCommand dkc: keyCommands)
			{
				if (dkc.getName() != null && dkc.getName().length() > 0)
				{
					l.add(dkc);
				}
			}
			
			menuCommands = l.toArray(new KeyCommand[l.size()]);
		}
		
		protected internal virtual void  decodeConstraints(System.String s)
		{
			SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, ',');
			numeric = sd.nextBoolean(false);
			minValue = sd.nextInt(0);
			maxValue = sd.nextInt(100);
			wrap = sd.nextBoolean(false);
		}
		
		protected internal virtual System.String encodeConstraints()
		{
			return new SequenceEncoder(',').append(numeric).append(minValue).append(maxValue).append(wrap).Value;
		}
		
		public virtual void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			piece.draw(g, x, y, obs, zoom);
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			return piece.boundingBox();
		}
		
		public override System.Object getProperty(System.Object key)
		{
			if (key.Equals(Key))
			{
				return Value;
			}
			return base.getProperty(key);
		}
		
		public override System.Object getLocalizedProperty(System.Object key)
		{
			if (key.Equals(Key))
			{
				return Value;
			}
			else
			{
				return base.getLocalizedProperty(key);
			}
		}
		
		public override void  setProperty(System.Object key, System.Object value_Renamed)
		{
			if (key.Equals(Key))
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				Value = null == value_Renamed?null:value_Renamed.ToString();
			}
			else
			{
				base.setProperty(key, value_Renamed);
			}
		}
		
		public override System.String myGetState()
		{
			return Value;
		}
		
		public override void  mySetState(System.String state)
		{
			Value = state;
		}
		
		private System.String formatValue(System.String value_Renamed)
		{
			format.Format = value_Renamed;
			return format.getText(Decorator.getOutermost(this));
		}
		
		public override System.String myGetType()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(key);
			se.append(encodeConstraints());
			se.append(keyCommandListConfig.ValueString);
			return ID + se.Value;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			return menuCommands;
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tracker '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			ChangeTracker tracker = new ChangeTracker(this);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(DynamicKeyCommand dkc: keyCommands)
			{
				if (dkc.matches(stroke))
				{
					Value = dkc.propChanger.getNewValue(value_Renamed);
				}
			}
			
			return tracker.ChangeCommand;
		}
		
		/// <summary> Return Property names exposed by this trait</summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getPropertyNames()
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public override PieceI18nData getI18nData()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'commandNames '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] commandNames = new System.String[menuCommands.Length];
			//UPGRADE_NOTE: Final was removed from the declaration of 'commandDescs '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] commandDescs = new System.String[menuCommands.Length];
			
			for (int i = 0; i < menuCommands.Length; i++)
			{
				commandNames[i] = menuCommands[i].Name;
				commandDescs[i] = "Property " + key + ": Menu Command " + i;
			}
			
			return getI18nData(commandNames, commandDescs);
		}
		
		protected internal class Ed : PieceEditor
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassListConfigurer1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassListConfigurer1:ListConfigurer
			{
				private void  InitBlock(VassalSharp.counters.DynamicProperty m, Ed enclosingInstance)
				{
					this.m = m;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable m was copied into class AnonymousClassListConfigurer1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private VassalSharp.counters.DynamicProperty m;
				private Ed enclosingInstance;
				public Ed Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				internal AnonymousClassListConfigurer1(VassalSharp.counters.DynamicProperty m, Ed enclosingInstance, System.String Param1, System.String Param2):base(Param1, Param2)
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
					//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(nameConfig.ValueString);
					se.append(encodeConstraints());
					se.append(keyCommandListConfig.ValueString);
					return VassalSharp.counters.DynamicProperty.ID + se.Value;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					return initialValueConfig.ValueString;
				}
				
			}
			protected internal StringConfigurer nameConfig;
			protected internal StringConfigurer initialValueConfig;
			protected internal BooleanConfigurer numericConfig;
			protected internal IntConfigurer minConfig;
			protected internal IntConfigurer maxConfig;
			protected internal BooleanConfigurer wrapConfig;
			protected internal ListConfigurer keyCommandListConfig;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			protected internal Box controls;
			
			public Ed(DynamicProperty m)
			{
				keyCommandListConfig = new AnonymousClassListConfigurer1(m, this, null, "Key Commands");
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				keyCommandListConfig.setValue(
				new ArrayList < DynamicKeyCommand >(Arrays.asList(m.keyCommands)));
				//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				PropertyChangeListener l = new AnonymousClassPropertyChangeListener(this);
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				controls = Box.createVerticalBox();
				nameConfig = new StringConfigurer(null, "Name:  ", m.Key);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(nameConfig.Controls);
				initialValueConfig = new StringConfigurer(null, "Value:  ", m.Value);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(initialValueConfig.Controls);
				numericConfig = new BooleanConfigurer(null, "Is numeric:  ", m.Numeric);
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
			}
			
			protected internal virtual System.String encodeConstraints()
			{
				return new SequenceEncoder(',').append(numericConfig.ValueString).append(minConfig.ValueString).append(maxConfig.ValueString).append(wrapConfig.ValueString).Value;
			}
		}
		
		/// <summary> DynamicKeyCommand A class that represents an action to be performed on a
		/// Dynamic property
		/// </summary>
		[Serializable]
		protected internal class DynamicKeyCommand:KeyCommand
		{
			private const long serialVersionUID = 1L;
			
			protected internal PropertyChanger propChanger = null;
			
			public DynamicKeyCommand(System.String name, NamedKeyStroke key, GamePiece target, TranslatablePiece i18nPiece, PropertyChanger propChanger):base(name, key, target, i18nPiece)
			{
				this.propChanger = propChanger;
			}
		}
		
		/// <summary> 
		/// Configure a single Dynamic Key Command line
		/// </summary>
		protected internal class DynamicKeyCommandConfigurer:Configurer
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener
			{
				public AnonymousClassPropertyChangeListener(DynamicKeyCommandConfigurer enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(DynamicKeyCommandConfigurer enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private DynamicKeyCommandConfigurer enclosingInstance;
				public DynamicKeyCommandConfigurer Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
				{
					Enclosing_Instance.updateValue();
				}
			}
			override public System.String ValueString
			{
				get
				{
					SequenceEncoder se = new SequenceEncoder(':');
					se.append(commandConfig.ValueString).append(keyConfig.ValueString).append(propChangeConfig.ValueString);
					return se.Value;
				}
				
			}
			virtual public DynamicKeyCommand KeyCommand
			{
				get
				{
					return (DynamicKeyCommand) getValue();
				}
				
			}
			override public System.Windows.Forms.Control Controls
			{
				get
				{
					if (controls == null)
					{
						buildControls();
					}
					return controls;
				}
				
			}
			protected internal NamedHotKeyConfigurer keyConfig;
			protected internal PropertyChangerConfigurer propChangeConfig;
			protected internal StringConfigurer commandConfig;
			
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			protected internal Box controls = null;
			protected internal DynamicProperty target;
			
			public DynamicKeyCommandConfigurer(DynamicProperty target):base(target.Key, target.Key, new DynamicKeyCommand("Change value", NamedKeyStroke.getNamedKeyStroke('V', (int) System.Windows.Forms.Keys.Control), Decorator.getOutermost(target), target, new PropertyPrompt(target, "Change value of " + target.Key)))
			{
				commandConfig = new StringConfigurer(null, " Menu Command:  ", "Change value");
				keyConfig = new NamedHotKeyConfigurer(null, " Key Command:  ", NamedKeyStroke.getNamedKeyStroke('V', (int) System.Windows.Forms.Keys.Control));
				propChangeConfig = new PropertyChangerConfigurer(null, target.Key, target);
				propChangeConfig.setValue(new PropertyPrompt(target, " Change value of " + target.Key));
				
				//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				PropertyChangeListener pl = new AnonymousClassPropertyChangeListener(this);
				commandConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(pl.propertyChange);
				keyConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(pl.propertyChange);
				propChangeConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(pl.propertyChange);
				this.target = target;
			}
			
			public override void  setValue(System.Object value_Renamed)
			{
				if (!noUpdate && value_Renamed is DynamicKeyCommand && commandConfig != null)
				{
					DynamicKeyCommand dkc = (DynamicKeyCommand) value_Renamed;
					commandConfig.setValue(dkc.Name);
					keyConfig.setValue(dkc.NamedKeyStroke);
					propChangeConfig.setValue(dkc.propChanger);
				}
				base.setValue(value_Renamed);
			}
			
			public override void  setValue(System.String s)
			{
				SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s == null?"":s, ':');
				commandConfig.setValue(sd.nextToken(""));
				keyConfig.setValue(sd.nextNamedKeyStroke(null));
				propChangeConfig.setValue(sd.nextToken(""));
				updateValue();
			}
			
			protected internal virtual void  updateValue()
			{
				noUpdate = true;
				setValue(new DynamicKeyCommand(commandConfig.ValueString, keyConfig.ValueNamedKeyStroke, target, target, propChangeConfig.PropertyChanger));
				noUpdate = false;
			}
			
			protected internal virtual void  buildControls()
			{
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				controls = Box.createHorizontalBox();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(commandConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(keyConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				controls.Controls.Add(propChangeConfig.Controls);
			}
		}
	}
}