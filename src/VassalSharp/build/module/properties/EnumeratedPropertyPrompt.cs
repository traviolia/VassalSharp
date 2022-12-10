/*
* $Id$
*
* Copyright (c) 2000-2011 by Rodney Kinney, Brent Easton
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
using Expression = VassalSharp.script.expression.Expression;
using ExpressionException = VassalSharp.script.expression.ExpressionException;
namespace VassalSharp.build.module.properties
{
	
	/// <summary> Prompts user to select from a list</summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class EnumeratedPropertyPrompt:PropertyPrompt
	{
		virtual public System.String[] ValidValues
		{
			get
			{
				return validValues;
			}
			
		}
		protected internal System.String[] validValues;
		protected internal Expression[] valueExpressions;
		protected internal PropertyPrompt.DialogParent dialogParent;
		protected internal PropertyPrompt.Constraints propertySource;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public EnumeratedPropertyPrompt(PropertyPrompt.DialogParent dialogParent, System.String prompt, System.String[] validValues):this(dialogParent, prompt, validValues, null)
		{
		}
		
		public EnumeratedPropertyPrompt(PropertyPrompt.DialogParent dialogParent, System.String prompt, System.String[] validValues, PropertyPrompt.Constraints propertySource):base(null, prompt)
		{
			this.validValues = validValues;
			valueExpressions = new Expression[validValues.Length];
			for (int i = 0; i < validValues.Length; i++)
			{
				valueExpressions[i] = Expression.createExpression(validValues[i]);
			}
			this.dialogParent = dialogParent;
			this.propertySource = propertySource;
		}
		
		public override System.String getNewValue(System.String oldValue)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'finalValues '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] finalValues = new System.String[valueExpressions.Length];
			for (int i = 0; i < finalValues.Length; i++)
			{
				System.String value_Renamed;
				try
				{
					if (propertySource == null)
					{
						value_Renamed = valueExpressions[i].evaluate();
					}
					else
					{
						value_Renamed = valueExpressions[i].evaluate(propertySource.getPropertySource());
					}
				}
				catch (ExpressionException e)
				{
					value_Renamed = valueExpressions[i].getExpression();
				}
				finalValues[i] = value_Renamed;
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'newValue '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String newValue = (System.String) JOptionPane.showInputDialog(dialogParent.getComponent(), promptText, null, (int) System.Windows.Forms.MessageBoxIcon.Question, null, finalValues, oldValue);
			return newValue == null?oldValue:newValue;
		}
	}
}