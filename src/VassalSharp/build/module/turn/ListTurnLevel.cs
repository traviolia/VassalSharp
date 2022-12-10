/*
* $Id$
*
* Copyright (c) 2005 by Rodney Kinney, Brent Easton
*
* This library is free software; you can redistribute it and/or modify it under
* the terms of the GNU Library General Public License (LGPL) as published by
* the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful, but WITHOUT
* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
* FOR A PARTICULAR PURPOSE. See the GNU Library General Public License for more
* details.
*
* You should have received a copy of the GNU Library General Public License
* along with this library; if not, copies are available at
* http://www.opensource.org.
*/
using System;
using GameModule = VassalSharp.build.GameModule;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
using StringEnumConfigurer = VassalSharp.configure.StringEnumConfigurer;
using VisibilityCondition = VassalSharp.configure.VisibilityCondition;
using ComponentI18nData = VassalSharp.i18n.ComponentI18nData;
using Resources = VassalSharp.i18n.Resources;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
using FormattedString = VassalSharp.tools.FormattedString;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.build.module.turn
{
	
	public class ListTurnLevel:TurnLevel
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		new private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(ListTurnLevel enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ListTurnLevel enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ListTurnLevel enclosingInstance;
			public ListTurnLevel Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				System.String option = ((StringEnumConfigurer) event_sender).ValueString;
				for (int i = 0; i < Enclosing_Instance.list.Length; i++)
				{
					if (option.Equals(Enclosing_Instance.list[i]))
					{
						Enclosing_Instance.current = i;
						Enclosing_Instance.myValue.setPropertyValue(Enclosing_Instance.ValueString);
					}
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassVisibilityCondition' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		public class AnonymousClassVisibilityCondition : VisibilityCondition
		{
			public AnonymousClassVisibilityCondition(ListTurnLevel enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ListTurnLevel enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ListTurnLevel enclosingInstance;
			public ListTurnLevel Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual bool shouldBeVisible()
			{
				return Enclosing_Instance.configFirst;
			}
		}
		private void  InitBlock()
		{
			return ArrayUtils.append(base.getAttributeTypes(), typeof(System.String[]), typeof(System.Boolean), typeof(System.Boolean), typeof(System.String));
			promptCond = new AnonymousClassVisibilityCondition(this);
		}
		override protected internal System.String State
		{
			/*
			* Generate the state of the level
			*/
			
			get
			{
				SequenceEncoder se = new SequenceEncoder(';');
				se.append(current);
				se.append(currentSubLevel);
				se.append(first);
				System.String[] s = new System.String[active.Length];
				for (int i = 0; i < s.Length; i++)
				{
					s[i] = active[i] + ""; //$NON-NLS-1$
				}
				se.append(s);
				for (int i = 0; i < TurnLevelCount; i++)
				{
					se.append(getTurnLevel(i).State);
				}
				return se.Value;
			}
			
			/*
			* Set the state of the level
			*/
			
			set
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'sd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(value, ';');
				current = sd.nextInt(start);
				currentSubLevel = sd.nextInt(0); // change to 0 as default due to issue 3500
				first = sd.nextInt(0);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String[] s = sd.nextStringArray(0);
				active = new bool[list.Length];
				//UPGRADE_NOTE: Final was removed from the declaration of 'l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int l = System.Math.Min(s.Length, active.Length);
				for (int i = 0; i < l; i++)
				{
					active[i] = s[i].Equals("true"); //$NON-NLS-1$
				}
				
				for (int i = 0; i < TurnLevelCount; i++)
				{
					getTurnLevel(i).State = sd.nextToken(""); //$NON-NLS-1$
				}
				
				myValue.setPropertyValue(ValueString);
			}
			
		}
		override protected internal System.String ValueString
		{
			get
			{
				if (current >= 0 && current <= (list.Length - 1))
				{
					return list[current];
				}
				else
				{
					return ""; //$NON-NLS-1$
				}
			}
			
		}
		override protected internal System.String LongestValueName
		{
			/*
			* (non-Javadoc)
			*
			* @see turn.TurnLevel#getLongestValueName()
			*/
			
			get
			{
				System.String s = "X"; //$NON-NLS-1$
				for (int i = 0; i < list.Length; i++)
				{
					if (list[i].Length > s.Length)
					{
						s = list[i];
					}
				}
				return s;
			}
			
		}
		override protected internal bool Active
		{
			/* A list turn level is active only if at least one item is active */
			
			get
			{
				for (int i = 0; i < active.Length; i++)
				{
					if (active[i])
					{
						return true;
					}
				}
				return false;
			}
			
		}
		override protected internal System.Windows.Forms.Control SetControl
		{
			get
			{
				
				StringEnumConfigurer config = new StringEnumConfigurer("", " " + getConfigureName() + ":  ", list); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
				config.setValue(list[current]);
				config.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				
				return config.Controls;
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return ArrayUtils.append(base.AttributeDescriptions, "List of Items", "Allow players to hide items in this list?", "Allow players to change which item goes first?", "Prompt to players to select which item goes first:  ");
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return ArrayUtils.append(base.AttributeNames, LIST, CONFIG_LIST, CONFIG_FIRST, PROMPT);
			}
			
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return "List";
			}
			
		}
		override public bool Configurable
		{
			get
			{
				
				if (configFirst || configList)
				{
					return true;
				}
				else
				{
					return base.Configurable;
				}
			}
			
		}
		override public ComponentI18nData I18nData
		{
			get
			{
				ComponentI18nData myI18nData = base.I18nData;
				myI18nData.setAttributeTranslatable(LIST, true);
				return myI18nData;
			}
			
		}
		
		protected internal const System.String LIST = "list"; //$NON-NLS-1$
		protected internal const System.String CONFIG_LIST = "configList"; //$NON-NLS-1$
		protected internal const System.String CONFIG_FIRST = "configFirst"; //$NON-NLS-1$
		protected internal const System.String PROMPT = "prompt"; //$NON-NLS-1$
		
		protected internal int first = 0;
		protected internal System.String[] list = new System.String[0];
		protected internal bool[] active = new bool[0];
		
		protected internal bool configList = false;
		protected internal bool configFirst = false;
		protected internal System.String prompt = null;
		
		protected internal System.Windows.Forms.Form configDialog;
		protected internal System.Windows.Forms.Control setControls;
		
		public ListTurnLevel():base()
		{
			InitBlock();
			turnFormat = new FormattedString("$" + LEVEL_VALUE + "$"); //$NON-NLS-1$ //$NON-NLS-2$
		}
		
		/*
		* Reset counter to initial state
		*/
		protected internal override void  reset()
		{
			base.reset();
			for (int i = 0; i < active.Length; i++)
			{
				active[i] = true;
			}
			setLow();
		}
		
		protected internal override void  setLow()
		{
			current = first;
			base.setLow();
		}
		
		protected internal override void  setHigh()
		{
			current = first;
			current--;
			if (current < 0)
			{
				current = list.Length - 1;
			}
			base.setHigh();
		}
		
		/*
		* Advance this level. 1. If there are any sub-levels, Advance the current
		* sub-level first. 2. If the sublevels roll over, then advance the counter 3.
		* If LOOP is reached, roll over the counter
		*/
		protected internal override void  advance()
		{
			base.advance();
			
			if (TurnLevelCount == 0 || (TurnLevelCount > 0 && hasSubLevelRolledOver()))
			{
				int idx = current;
				bool done = false;
				for (int i = 0; i < list.Length && !done; i++)
				{
					idx++;
					if (idx >= list.Length)
					{
						idx = 0;
					}
					if (idx == first)
					{
						rolledOver = true;
					}
					done = active[idx];
				}
				current = idx;
				if (!done)
				{
					rolledOver = true;
				}
			}
			myValue.setPropertyValue(ValueString);
		}
		
		protected internal override void  retreat()
		{
			base.retreat();
			
			if (TurnLevelCount == 0 || (TurnLevelCount > 0 && hasSubLevelRolledOver()))
			{
				int idx = current;
				bool done = false;
				for (int i = 0; i < list.Length && !done; i++)
				{
					if (idx == first)
					{
						rolledOver = true;
					}
					idx--;
					if (idx < 0 || idx > (list.Length - 1))
					{
						idx = list.Length - 1;
					}
					done = active[idx];
				}
				current = idx;
			}
			myValue.setPropertyValue(ValueString);
		}
		
		protected internal override void  buildConfigMenu(System.Windows.Forms.MenuItem configMenu)
		{
			System.Windows.Forms.MenuItem menu = ConfigMenu;
			if (menu != null)
			{
				configMenu.MenuItems.Add(menu);
			}
			
			if (configFirst || configList)
			{
				System.Windows.Forms.MenuItem item = new JMenuItem(Resources.getString("TurnTracker.configure2", getConfigureName())); //$NON-NLS-1$
				item.Click += new System.EventHandler(this.actionPerformed);
				SupportClass.CommandManager.CheckCommand(item);
				configMenu.MenuItems.Add(item);
			}
		}
		
		// Configure which Items are active
		public virtual void  actionPerformed(System.Object event_sender, System.EventArgs arg0)
		{
			configDialog = new ConfigDialog(this);
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			SupportClass.SetPropertyAsVirtual(configDialog, "Visible", true);
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
				list = ((System.String[]) value_Renamed);
				active = new bool[list.Length];
				for (int i = 0; i < active.Length; i++)
				{
					active[i] = true;
				}
			}
			else if (CONFIG_LIST.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.lang.Boolean.valueOf' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
					value_Renamed = System.Boolean.Parse((System.String) value_Renamed);
				}
				configList = ((System.Boolean) value_Renamed);
			}
			else if (CONFIG_FIRST.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.lang.Boolean.valueOf' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
					value_Renamed = System.Boolean.Parse((System.String) value_Renamed);
				}
				configFirst = ((System.Boolean) value_Renamed);
			}
			else if (PROMPT.Equals(key))
			{
				prompt = ((System.String) value_Renamed);
			}
			else
			{
				base.setAttribute(key, value_Renamed);
			}
		}
		
		public override System.String getAttributeValueString(System.String key)
		{
			if (LIST.Equals(key))
			{
				return StringArrayConfigurer.arrayToString(list);
			}
			else if (CONFIG_LIST.Equals(key))
			{
				return configList + ""; //$NON-NLS-1$
			}
			else if (CONFIG_FIRST.Equals(key))
			{
				return configFirst + ""; //$NON-NLS-1$
			}
			else if (PROMPT.Equals(key))
			{
				return prompt;
			}
			else
				return base.getAttributeValueString(key);
		}
		
		public override HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("TurnTracker.htm", "List"); //$NON-NLS-1$ //$NON-NLS-2$
		}
		
		public override VisibilityCondition getAttributeVisibility(System.String name)
		{
			if (PROMPT.Equals(name))
			{
				return promptCond;
			}
			else
			{
				return null;
			}
		}
		
		//UPGRADE_NOTE: The initialization of  'promptCond' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private VisibilityCondition promptCond;
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ConfigDialog' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		protected internal class ConfigDialog:System.Windows.Forms.Form
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener1
			{
				public AnonymousClassPropertyChangeListener1(ConfigDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(ConfigDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private ConfigDialog enclosingInstance;
				public ConfigDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
				{
					System.String option = ((StringEnumConfigurer) event_sender).ValueString;
					for (int i = 0; i < Enclosing_Instance.Enclosing_Instance.list.Length; i++)
					{
						if (Enclosing_Instance.Enclosing_Instance.list[i].Equals(option))
						{
							Enclosing_Instance.Enclosing_Instance.first = i;
						}
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassPropertyChangeListener2
			{
				public AnonymousClassPropertyChangeListener2(ConfigDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(ConfigDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private ConfigDialog enclosingInstance;
				public ConfigDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
				{
					BooleanConfigurer b = (BooleanConfigurer) event_sender;
					System.String option = b.getName();
					for (int i = 0; i < Enclosing_Instance.Enclosing_Instance.list.Length; i++)
					{
						if (Enclosing_Instance.Enclosing_Instance.list[i].Equals(option))
						{
							Enclosing_Instance.Enclosing_Instance.active[i] = ((BooleanConfigurer) event_sender).booleanValue();
						}
					}
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener
			{
				public AnonymousClassActionListener(ConfigDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(ConfigDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private ConfigDialog enclosingInstance;
				public ConfigDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.Visible = false;
				}
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener1
			{
				public AnonymousClassActionListener1(ConfigDialog enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(ConfigDialog enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private ConfigDialog enclosingInstance;
				public ConfigDialog Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					Enclosing_Instance.Visible = false;
				}
			}
			private void  InitBlock(ListTurnLevel enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ListTurnLevel enclosingInstance;
			public ListTurnLevel Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			private const long serialVersionUID = 1L;
			
			public ConfigDialog(ListTurnLevel enclosingInstance):base(GameModule.getGameModule().getFrame(), Resources.getString("TurnTracker.configure2", Enclosing_Instance.getConfigureName()))
			{
				InitBlock(enclosingInstance); //$NON-NLS-1$
				//UPGRADE_ISSUE: Method 'javax.swing.JDialog.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJDialogsetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				setLayout(new BoxLayout(((System.Windows.Forms.ContainerControl) this), BoxLayout.Y_AXIS));
				
				if (Enclosing_Instance.configFirst)
				{
					if (Enclosing_Instance.prompt == null)
					{
						Enclosing_Instance.prompt = "First " + Enclosing_Instance.getConfigureName() + " each " + Enclosing_Instance.parent.getConfigureName();
					}
					StringEnumConfigurer firstItem = new StringEnumConfigurer("", Enclosing_Instance.prompt + " :  ", Enclosing_Instance.list); //$NON-NLS-1$ //$NON-NLS-2$
					firstItem.setValue(Enclosing_Instance.list[Enclosing_Instance.first]);
					firstItem.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener1(this).propertyChange);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					Controls.Add(firstItem.Controls);
				}
				
				if (Enclosing_Instance.configList)
				{
					
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = Resources.getString("TurnTracker.turn_off");
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					Controls.Add(temp_Control); //$NON-NLS-1$
					for (int i = 0; i < Enclosing_Instance.list.Length; i++)
					{
						
						BooleanConfigurer b = new BooleanConfigurer(null, Enclosing_Instance.list[i], Boolean.valueOf(Enclosing_Instance.active[i]));
						b.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener2(this).propertyChange);
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
						Controls.Add(b.Controls);
					}
				}
				
				System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
				
				System.Windows.Forms.Button saveButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.SAVE));
				if (saveButton is VassalSharp.tools.LaunchButton)
					((VassalSharp.tools.LaunchButton) saveButton).setToolTipText(Resources.getString("TurnTracker.save_changes"));
				else
					SupportClass.ToolTipSupport.setToolTipText(saveButton, Resources.getString("TurnTracker.save_changes")); //$NON-NLS-1$
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(saveButton);
				saveButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(saveButton);
				
				System.Windows.Forms.Button cancelButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.CANCEL));
				if (cancelButton is VassalSharp.tools.LaunchButton)
					((VassalSharp.tools.LaunchButton) cancelButton).setToolTipText(Resources.getString("TurnTracker.discard_changes"));
				else
					SupportClass.ToolTipSupport.setToolTipText(cancelButton, Resources.getString("TurnTracker.discard_changes")); //$NON-NLS-1$
				cancelButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
				SupportClass.CommandManager.CheckCommand(cancelButton);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				p.Controls.Add(cancelButton);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(p);
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				pack();
			}
		}
	}
}