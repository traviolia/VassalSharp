/*
* $Id: IntBuilder.java 7725 2011-07-31 18:51:43Z uckelman $
*
* Copyright (c) 2008-2012 Brent Easton
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

/*
* StrBuilder.
* Build an integer constant. Allow user to enter anything and clean it up by removing
* non number characters
*/
using System;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
namespace VassalSharp.script.expression
{
	
	[Serializable]
	public class IntBuilder:StrBuilder
	{
		private const long serialVersionUID = 1L;
		
		public IntBuilder(StringConfigurer c, System.Windows.Forms.Form parent):base(parent, "Number Builder", true)
		{
			target = c;
			build("Number");
		}
		
		
		protected internal override void  save()
		{
			bool negative = false;
			//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder result = new StringBuilder();
			System.String value_Renamed = entry.ValueString;
			
			if (value_Renamed.StartsWith("-"))
			{
				value_Renamed = value_Renamed.Substring(1);
				negative = true;
			}
			
			for (int i = 0; i < value_Renamed.Length; i++)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				char c = value_Renamed[i];
				if (c >= '0' && c <= '9')
				{
					result.append(c);
				}
			}
			
			value_Renamed = (negative?"-":"") + result.toString();
			if (value_Renamed.Length == 0 || value_Renamed.Equals("-"))
			{
				value_Renamed = "0";
			}
			
			target.setValue(value_Renamed);
			Dispose();
		}
	}
}