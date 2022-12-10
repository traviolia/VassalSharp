/*
* $Id$
*
* Copyright (c) 2000-2008 by Rodney Kinney
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
	
	/// <summary> Prompts for an integer value</summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public class NumericPropertyPrompt:PropertyPrompt
	{
		private int min;
		private int max;
		private System.Windows.Forms.Control dialogParent;
		
		public NumericPropertyPrompt(System.Windows.Forms.Control dialogParent, System.String prompt, int minValue, int maxValue):base(null, prompt)
		{
			min = minValue;
			max = maxValue;
			this.dialogParent = dialogParent;
		}
		
		public override System.String getNewValue(System.String oldValue)
		{
			System.String s = null;
			do 
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JOptionPane.showInputDialog' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
				s = ((System.String) JOptionPane.showInputDialog(dialogParent, promptText, null, (int) System.Windows.Forms.MessageBoxIcon.Question, null, null, oldValue));
			}
			while (s != null && !isValidValue(s));
			return s;
		}
		
		private bool isValidValue(System.String s)
		{
			try
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int value_Renamed = System.Int32.Parse(s);
				return value_Renamed <= max && value_Renamed >= min;
			}
			catch (System.FormatException e)
			{
				return false;
			}
		}
	}
}