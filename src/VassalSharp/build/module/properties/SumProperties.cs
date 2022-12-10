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
using GamePiece = VassalSharp.counters.GamePiece;
namespace VassalSharp.build.module.properties
{
	
	/// <summary> For property names of the form sum(name), returns the value of
	/// the named property summed over a list of pieces.
	/// 
	/// </summary>
	/// <author>  rkinney
	/// </author>
	public class SumProperties : PropertySource
	{
		public SumProperties()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			this.pieces = pieces;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected Collection < GamePiece > pieces;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public SumProperties(Collection < GamePiece > pieces)
		
		public virtual System.Object getProperty(System.Object key)
		{
			System.Object value_Renamed = null;
			//UPGRADE_NOTE: Final was removed from the declaration of 'keyString '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.String keyString = key.ToString();
			if (keyString.StartsWith("sum(") && keyString.EndsWith(")"))
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'propertyName '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String propertyName = keyString.Substring(4, (keyString.Length - 1) - (4));
				int sum = 0;
				bool indeterminate = false;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(GamePiece p: pieces)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'val '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Object val = p.getLocalizedProperty(propertyName);
					if (val != null)
					{
						try
						{
							//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							sum += System.Int32.Parse(val.ToString());
						}
						catch (System.FormatException e)
						{
						}
					}
					else
					{
						indeterminate = true;
					}
				}
				
				if (sum == 0 && indeterminate)
				{
					value_Renamed = "?";
				}
				else
				{
					value_Renamed = sum + (indeterminate?"+?":"");
				}
			}
			else if (pieces.size() > 0)
			{
				value_Renamed = pieces.iterator().next().getProperty(key);
			}
			return value_Renamed;
		}
		
		public virtual System.Object getLocalizedProperty(System.Object key)
		{
			return getProperty(key);
		}
	}
}