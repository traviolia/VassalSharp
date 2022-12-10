/*
* $Id$
*
* Copyright (c) 2000-2006 by Rodney Kinney
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
using Info = VassalSharp.Info;
using GameModule = VassalSharp.build.GameModule;
namespace VassalSharp.command
{
	
	/// <summary> Evaluates properties of the GameModule and conditionally executes
	/// another Command if all values are satisfied.
	/// </summary>
	public class ConditionalCommand:Command
	{
		virtual public Command Delegate
		{
			get
			{
				return delegate_Renamed;
			}
			
		}
		virtual public Condition[] Conditions
		{
			get
			{
				return conditions;
			}
			
		}
		private Condition[] conditions;
		/// <summary>Command to execute if the condition is accepted </summary>
		private Command delegate_Renamed;
		
		public ConditionalCommand(Condition[] conditions, Command delegate_Renamed)
		{
			this.conditions = conditions;
			this.delegate_Renamed = delegate_Renamed;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override void  executeCommand()
		{
			for (int i = 0; i < conditions.Length; ++i)
			{
				if (!conditions[i].Satisfied)
				{
					return ;
				}
			}
			delegate_Renamed.execute();
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override Command myUndoCommand()
		{
			return null;
		}
		
		/// <summary> The class representing a condition that must be satisfied if the
		/// Command is to be executed
		/// </summary>
		public abstract class Condition
		{
			public abstract bool Satisfied{get;}
		}
		
		public class Eq:Condition
		{
			public Eq()
			{
				InitBlock();
			}
			private void  InitBlock()
			{
				this.property = property;
				this.allowed = allowed; this.property = property;
				this.allowed = allowed;
				return Collections.unmodifiableList(allowed);
				return Collections.enumeration(allowed);
			}
			virtual public System.String Property
			{
				get
				{
					return property;
				}
				
			}
			override public bool Satisfied
			{
				get
				{
					System.String propertyValue = GameModule.getGameModule().getAttributeValueString(property);
					return allowed.contains(propertyValue);
				}
				
			}
			/// <summary>The property to be checked </summary>
			private System.String property;
			/// <summary>To pass the check the value of the property
			/// must match one of these values. 
			/// </summary>
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			private List < String > allowed;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public Eq(String property, List < String > allowed)
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Deprecated
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public Eq(String property, Vector < String > allowed)
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public List < String > getValueList()
			
			/// <deprecated> Use {@link #getValueList()} instead. 
			/// </deprecated>
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Deprecated
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public Enumeration < String > getValues()
		}
		
		public class Not:Condition
		{
			override public bool Satisfied
			{
				get
				{
					return !sub.Satisfied;
				}
				
			}
			virtual public Condition SubCondition
			{
				get
				{
					return sub;
				}
				
			}
			private Condition sub;
			
			public Not(Condition sub)
			{
				this.sub = sub;
			}
		}
		
		public class Lt:Condition
		{
			virtual public System.String Property
			{
				get
				{
					return property;
				}
				
			}
			virtual public System.String Value
			{
				get
				{
					return value_Renamed;
				}
				
			}
			override public bool Satisfied
			{
				// FIXME: what versions are being compared here?
				
				get
				{
					System.String propertyValue = GameModule.getGameModule().getAttributeValueString(property);
					return Info.compareVersions(propertyValue, value_Renamed) < 0;
				}
				
			}
			private System.String property;
			private System.String value_Renamed;
			
			public Lt(System.String property, System.String value_Renamed)
			{
				this.property = property;
				this.value_Renamed = value_Renamed;
			}
		}
		
		public class Gt:Condition
		{
			virtual public System.String Property
			{
				get
				{
					return property;
				}
				
			}
			virtual public System.String Value
			{
				get
				{
					return value_Renamed;
				}
				
			}
			override public bool Satisfied
			{
				// FIXME: what versions are being compared here?
				
				get
				{
					System.String propertyValue = GameModule.getGameModule().getAttributeValueString(property);
					return Info.compareVersions(propertyValue, value_Renamed) > 0;
				}
				
			}
			private System.String property;
			private System.String value_Renamed;
			
			public Gt(System.String property, System.String value_Renamed)
			{
				this.property = property;
				this.value_Renamed = value_Renamed;
			}
		}
	}
}