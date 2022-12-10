/*
* $Id$
*
* Copyright (c) 2006 by Rodney Kinney
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
namespace VassalSharp.build.module.properties
{
	
	/// <summary> Prompts user for a new value
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class PropertyPrompt : PropertyChanger
	{
		virtual public System.String Prompt
		{
			get
			{
				return promptText;
			}
			
		}
		protected internal System.String promptText;
		protected internal PropertyPrompt.Constraints constraints;
		
		public PropertyPrompt(PropertyPrompt.Constraints constraints, System.String prompt)
		{
			this.constraints = constraints;
			this.promptText = prompt;
		}
		
		public virtual System.String getNewValue(System.String oldValue)
		{
			System.String newValue = null;
			if (constraints != null && constraints.Numeric)
			{
				newValue = new NumericPropertyPrompt(constraints.Component, promptText, constraints.MinimumValue, constraints.MaximumValue).getNewValue(oldValue);
			}
			else
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JOptionPane.showInputDialog' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
				newValue = ((System.String) JOptionPane.showInputDialog(constraints.Component, promptText, null, (int) System.Windows.Forms.MessageBoxIcon.Question, null, null, oldValue));
			}
			
			return newValue == null?oldValue:newValue;
		}
		
		public interface DialogParent
		{
			System.Windows.Forms.Control Component
			{
				get;
				
			}
		}
		
		public interface Constraints:PropertyPrompt.DialogParent
		{
			bool Numeric
			{
				get;
				
			}
			int MaximumValue
			{
				get;
				
			}
			int MinimumValue
			{
				get;
				
			}
			PropertySource PropertySource
			{
				get;
				
			}
		}
	}
}