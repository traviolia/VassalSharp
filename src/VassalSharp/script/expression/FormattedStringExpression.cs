/*
* $Id$
*
* Copyright (c) 2009-2012 Brent Easton
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
using PropertySource = VassalSharp.build.module.properties.PropertySource;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.script.expression
{
	
	/// <summary> Report Format or old-style Formatted String expression containing at
	/// least one $variable$ name reference
	/// 
	/// </summary>
	public class FormattedStringExpression:Expression
	{
		private void  InitBlock()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder buffer = new StringBuilder();
			//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(getExpression(), '$');
			bool isProperty = true;
			while (st.hasMoreTokens())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'token '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String token = st.nextToken();
				isProperty = !isProperty;
				if (token.Length > 0)
				{
					/*
					* Only even numbered tokens with at least one token after them are valid $propertName$ strings.
					*/
					if (!isProperty || !st.hasMoreTokens())
					{
						buffer.append(token);
					}
					else if (properties != null && properties.containsKey(token))
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.String value_Renamed = properties.get_Renamed(token);
						if (value_Renamed != null)
						{
							buffer.append(value_Renamed);
						}
					}
					else if (ps != null)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'value '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Object value_Renamed = localized?ps.getLocalizedProperty(token):ps.getProperty(token);
						if (value_Renamed != null)
						{
							//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							buffer.append(value_Renamed.ToString());
						}
						else if (!localized)
						{
							buffer.append(token);
						}
					}
					else
					{
						buffer.append(token);
					}
				}
			}
			
			return buffer.toString();
		}
		
		public FormattedStringExpression(System.String s)
		{
			InitBlock();
			setExpression(s);
		}
		
		/// <summary> Evaluate this expression.
		/// NB. Code moved from FormattedString.java
		/// </summary>
		public System.String evaluate;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(PropertySource ps, Map < String, String > properties, 
		boolean localized)
		
		/// <summary> Convert to a BeanShell expression</summary>
		public override System.String toBeanShellString()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String s = getExpression();
			
			try
			{
				System.Int32.Parse(s);
				return s;
			}
			catch (System.FormatException e)
			{
				// Not an error
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringBuilder buffer = new StringBuilder();
			//UPGRADE_NOTE: Final was removed from the declaration of 'st '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(s, '$');
			bool isProperty = true;
			bool first = true;
			while (st.hasMoreTokens())
			{
				System.String token = st.nextToken();
				isProperty = !isProperty;
				if (token.Length > 0)
				{
					/*
					* Only even numbered tokens with at least one token after them are valid $propertName$ strings.
					*/
					if (!first)
					{
						buffer.append("+");
					}
					if (isProperty && st.hasMoreTokens())
					{
						buffer.append(BeanShellExpression.convertProperty(token));
					}
					else
					{
						buffer.append("\"");
						buffer.append(token);
						buffer.append("\"");
					}
					first = false;
				}
			}
			
			return buffer.toString();
		}
	}
}