/*
* $Id$
*
* Copyright (c) 2005 by Rodney Kinney, Brent Easton
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
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using VisibilityCondition = VassalSharp.configure.VisibilityCondition;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.build.module.turn
{
	
	public class CounterTurnLevel:TurnLevel
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		new private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(CounterTurnLevel enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(CounterTurnLevel enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private CounterTurnLevel enclosingInstance;
			public CounterTurnLevel Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				Enclosing_Instance.current = (System.Int32) ((IntConfigurer) event_sender).getValue();
				Enclosing_Instance.myValue.setPropertyValue(Enclosing_Instance.ValueString);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassVisibilityCondition' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		public class AnonymousClassVisibilityCondition : VisibilityCondition
		{
			public AnonymousClassVisibilityCondition(CounterTurnLevel enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(CounterTurnLevel enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private CounterTurnLevel enclosingInstance;
			public CounterTurnLevel Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual bool shouldBeVisible()
			{
				return Enclosing_Instance.loop;
			}
		}
		private void  InitBlock()
		{
			return ArrayUtils.append(base.getAttributeTypes(), typeof(System.Int32), typeof(System.Int32), typeof(System.Boolean), typeof(System.Int32));
			loopCond = new AnonymousClassVisibilityCondition(this);
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
				se.append(loop);
				se.append(loopLimit);
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
				SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(value, ';');
				current = sd.nextInt(start);
				currentSubLevel = sd.nextInt(0); // Change to 0 as default due to issue 3500
				loop = sd.nextBoolean(false);
				loopLimit = sd.nextInt(- 1);
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
				return System.Convert.ToString(current);
			}
			
		}
		override protected internal System.String LongestValueName
		{
			/* (non-Javadoc)
			* @see turn.TurnLevel#getLongestValueName()
			*/
			
			get
			{
				return start < 10000?"9999":System.Convert.ToString(start); //$NON-NLS-1$
			}
			
		}
		override protected internal System.Windows.Forms.Control SetControl
		{
			get
			{
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'config '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				IntConfigurer config = new IntConfigurer("", " " + getConfigureName() + ":  ", current); //$NON-NLS-1$ //$NON-NLS-2$ //$NON-NLS-3$
				config.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
				
				return config.Controls;
			}
			
		}
		override public System.String[] AttributeDescriptions
		{
			get
			{
				return ArrayUtils.append(base.AttributeDescriptions, "Start Value:  ", "Increment By:  ", "Loop?", "Maximum value:  ");
			}
			
		}
		override public System.String[] AttributeNames
		{
			get
			{
				return ArrayUtils.append(base.AttributeNames, START, INCR, LOOP, LOOP_LIMIT);
			}
			
		}
		public static System.String ConfigureTypeName
		{
			get
			{
				return "Counter";
			}
			
		}
		
		protected internal const System.String START = "start"; //$NON-NLS-1$
		protected internal const System.String INCR = "incr"; //$NON-NLS-1$
		protected internal const System.String LOOP = "loop"; //$NON-NLS-1$
		protected internal const System.String LOOP_LIMIT = "loopLimit"; //$NON-NLS-1$
		
		protected internal int incr = 1;
		protected internal bool loop = false;
		protected internal int loopLimit = - 1;
		
		public CounterTurnLevel():base()
		{
			InitBlock();
		}
		
		/*
		*  Reset counter to initial state
		*/
		protected internal override void  reset()
		{
			base.reset();
			setLow();
		}
		
		protected internal override void  setLow()
		{
			current = start;
			base.setLow();
		}
		
		protected internal override void  setHigh()
		{
			current = loopLimit;
			base.setHigh();
		}
		
		/*
		* Advance this level.
		* 1. If there are any sub-levels, Advance the current sub-level first.
		* 2. If the sublevels roll over, then advance the counter
		* 3. If LOOP is reached, roll over the counter
		*/
		protected internal override void  advance()
		{
			// Advance sub-levels
			base.advance();
			
			// If no sub-levels, or they rolled over, advance this level
			if (TurnLevelCount == 0 || (TurnLevelCount > 0 && hasSubLevelRolledOver()))
			{
				current += incr;
				if (loop && current > loopLimit)
				{
					current = start;
					RolledOver = true;
				}
			}
			myValue.setPropertyValue(ValueString);
		}
		
		protected internal override void  retreat()
		{
			// Retreat sub-levels
			base.retreat();
			
			// If no sub-levels, or they rolled over, retreat this level
			int oldCurrent = current;
			if (TurnLevelCount == 0 || (TurnLevelCount > 0 && hasSubLevelRolledOver()))
			{
				current -= incr;
				if (loop && oldCurrent <= start)
				{
					current = loopLimit;
					RolledOver = true;
				}
			}
			myValue.setPropertyValue(ValueString);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Class < ? > [] getAttributeTypes()
		
		public override void  setAttribute(System.String key, System.Object value_Renamed)
		{
			
			if (START.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					value_Renamed = System.Int32.Parse((System.String) value_Renamed);
				}
				start = ((System.Int32) value_Renamed);
				current = start;
				myValue.setPropertyValue(ValueString);
			}
			else if (INCR.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					value_Renamed = System.Int32.Parse((System.String) value_Renamed);
				}
				incr = ((System.Int32) value_Renamed);
			}
			else if (LOOP.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.lang.Boolean.valueOf' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
					value_Renamed = System.Boolean.Parse((System.String) value_Renamed);
				}
				loop = ((System.Boolean) value_Renamed);
			}
			else if (LOOP_LIMIT.Equals(key))
			{
				if (value_Renamed is System.String)
				{
					value_Renamed = System.Int32.Parse((System.String) value_Renamed);
				}
				loopLimit = ((System.Int32) value_Renamed);
			}
			else
			{
				base.setAttribute(key, value_Renamed);
			}
		}
		
		public override System.String getAttributeValueString(System.String key)
		{
			if (START.Equals(key))
			{
				return start + ""; //$NON-NLS-1$
			}
			else if (INCR.Equals(key))
			{
				return incr + ""; //$NON-NLS-1$
			}
			else if (LOOP.Equals(key))
			{
				return loop + ""; //$NON-NLS-1$
			}
			else if (LOOP_LIMIT.Equals(key))
			{
				return loopLimit + ""; //$NON-NLS-1$
			}
			else
				return base.getAttributeValueString(key);
		}
		
		public override HelpFile getHelpFile()
		{
			return HelpFile.getReferenceManualPage("TurnTracker.htm", "Counter"); //$NON-NLS-1$ //$NON-NLS-2$
		}
		
		public override VisibilityCondition getAttributeVisibility(System.String name)
		{
			if (LOOP_LIMIT.Equals(name))
			{
				return loopCond;
			}
			else
			{
				return null;
			}
		}
		
		//UPGRADE_NOTE: The initialization of  'loopCond' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private VisibilityCondition loopCond;
	}
}