/*
* $Id$
*
* Copyright (c) 2007-2010 by Joel Uckelman
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
namespace VassalSharp.tools
{
	
	/// <summary> Provides static methods for calculating hash codes.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	/// <deprecated> Use {@link org.apache.commons.lang.builder.HashCodeBuilder}
	/// instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public sealed class HashCode
	{
		private void  InitBlock()
		{
			return Arrays.hashCode(a);
		}
		private HashCode()
		{
			InitBlock();
		}
		
		public static int hash(bool value_Renamed)
		{
			return value_Renamed?1:0;
		}
		
		public static int hash(sbyte value_Renamed)
		{
			return (int) value_Renamed;
		}
		
		public static int hash(char value_Renamed)
		{
			return (int) value_Renamed;
		}
		
		public static int hash(short value_Renamed)
		{
			return (int) value_Renamed;
		}
		
		public static int hash(int value_Renamed)
		{
			return value_Renamed;
		}
		
		public static int hash(long value_Renamed)
		{
			return (int) (value_Renamed ^ (SupportClass.URShift(value_Renamed, 32)));
		}
		
		public static int hash(float value_Renamed)
		{
			//UPGRADE_ISSUE: Method 'java.lang.Float.floatToIntBits' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangFloatfloatToIntBits_float'"
			return Float.floatToIntBits(value_Renamed);
		}
		
		public static int hash(double value_Renamed)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'bits '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'java.lang.Double.doubleToLongBits' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangDoubledoubleToLongBits_double'"
			long bits = Double.doubleToLongBits(value_Renamed);
			return (int) (bits ^ (SupportClass.URShift(bits, 32)));
		}
		
		public static int hash(System.Object value_Renamed)
		{
			return value_Renamed == null?0:value_Renamed.GetHashCode();
		}
		
		public static int hash(bool[] a)
		{
			return Arrays.hashCode(a);
		}
		
		public static int hash(sbyte[] a)
		{
			return Arrays.hashCode(a);
		}
		
		public static int hash(char[] a)
		{
			return Arrays.hashCode(a);
		}
		
		public static int hash(short[] a)
		{
			return Arrays.hashCode(a);
		}
		
		public static int hash(int[] a)
		{
			return Arrays.hashCode(a);
		}
		
		public static int hash(long[] a)
		{
			return Arrays.hashCode(a);
		}
		
		public static int hash(float[] a)
		{
			return Arrays.hashCode(a);
		}
		
		public static int hash(double[] a)
		{
			return Arrays.hashCode(a);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static final < T > int hash(final T [] a)
	}
}