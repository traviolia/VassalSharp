/*
* $Id$
*
* Copyright (c) 2008-2009 by Joel Uckelman
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
	
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class ArrayUtils
	{
		private void  InitBlock()
		{
			return copyOf(orig, orig.length);
			return (T[]) copyOf(orig, newLength, orig.getClass());
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			T[] copy = (T[]) Array.newInstance(newType.getComponentType(), newLength);
			System_Renamed.arraycopy(orig, 0, copy, 0, Math.min(orig.length, newLength));
			return copy;
			return (T[]) copyOfRange(orig, from, to, orig.getClass());
			//UPGRADE_NOTE: Final was removed from the declaration of 'newLength '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int newLength = to - from;
			if (newLength < 0)
				throw new System.ArgumentException();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			T[] copy = (T[]) Array.newInstance(newType.getComponentType(), newLength);
			System_Renamed.arraycopy(orig, from, copy, 0, Math.min(orig.length - from, newLength));
			return copy;
			return prepend;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			] >) orig.getClass(), orig, e); //UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			T[] tmp = (T[]) Array.newInstance(type.getComponentType(), orig.length + 1);
			tmp[0] = e;
			System_Renamed.arraycopy(orig, 0, tmp, 1, orig.length);
			return tmp;
			return append_Renamed_Field;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			] >) orig.getClass(), orig, e);
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			T[] tmp = copyOf(orig, orig.length + 1, type);
			tmp[orig.length] = e;
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			bool[] tmp = new bool[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, a.length);
			System_Renamed.arraycopy(b, 0, tmp, a.length, b.length);
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] tmp = new sbyte[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, a.length);
			System_Renamed.arraycopy(b, 0, tmp, a.length, b.length);
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			char[] tmp = new char[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, a.length);
			System_Renamed.arraycopy(b, 0, tmp, a.length, b.length);
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			double[] tmp = new double[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, a.length);
			System_Renamed.arraycopy(b, 0, tmp, a.length, b.length);
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			float[] tmp = new float[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, a.length);
			System_Renamed.arraycopy(b, 0, tmp, a.length, b.length);
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int[] tmp = new int[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, a.length);
			System_Renamed.arraycopy(b, 0, tmp, a.length, b.length);
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			long[] tmp = new long[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, a.length);
			System_Renamed.arraycopy(b, 0, tmp, a.length, b.length);
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			short[] tmp = new short[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, a.length);
			System_Renamed.arraycopy(b, 0, tmp, a.length, b.length);
			return tmp; return append_Renamed_Field;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			] >) a.getClass(), a, b); //UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			T[] tmp = (T[]) Array.newInstance(type.getComponentType(), a.length + b.length);
			System_Renamed.arraycopy(a, 0, tmp, 0, a.length);
			System_Renamed.arraycopy(b, 0, tmp, a.length, b.length);
			return tmp;
			return insert_Renamed_Field;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			] >) orig.getClass(), orig, pos, e); //UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			T[] tmp = (T[]) Array.newInstance(type.getComponentType(), orig.length + 1);
			System_Renamed.arraycopy(orig, 0, tmp, 0, pos);
			tmp[pos] = e;
			System_Renamed.arraycopy(orig, pos, tmp, pos + 1, orig.length - pos);
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			bool[] tmp = new bool[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, pos);
			System_Renamed.arraycopy(b, 0, tmp, pos, b.length);
			System_Renamed.arraycopy(a, pos, tmp, pos + b.length, a.length - pos);
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] tmp = new sbyte[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, pos);
			System_Renamed.arraycopy(b, 0, tmp, pos, b.length);
			System_Renamed.arraycopy(a, pos, tmp, pos + b.length, a.length - pos);
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			char[] tmp = new char[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, pos);
			System_Renamed.arraycopy(b, 0, tmp, pos, b.length);
			System_Renamed.arraycopy(a, pos, tmp, pos + b.length, a.length - pos);
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			double[] tmp = new double[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, pos);
			System_Renamed.arraycopy(b, 0, tmp, pos, b.length);
			System_Renamed.arraycopy(a, pos, tmp, pos + b.length, a.length - pos);
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			float[] tmp = new float[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, pos);
			System_Renamed.arraycopy(b, 0, tmp, pos, b.length);
			System_Renamed.arraycopy(a, pos, tmp, pos + b.length, a.length - pos);
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int[] tmp = new int[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, pos);
			System_Renamed.arraycopy(b, 0, tmp, pos, b.length);
			System_Renamed.arraycopy(a, pos, tmp, pos + b.length, a.length - pos);
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			long[] tmp = new long[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, pos);
			System_Renamed.arraycopy(b, 0, tmp, pos, b.length);
			System_Renamed.arraycopy(a, pos, tmp, pos + b.length, a.length - pos);
			return tmp;
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			short[] tmp = new short[a.length + b.length];
			System_Renamed.arraycopy(a, 0, tmp, 0, pos);
			System_Renamed.arraycopy(b, 0, tmp, pos, b.length);
			System_Renamed.arraycopy(a, pos, tmp, pos + b.length, a.length - pos);
			return tmp; return insert_Renamed_Field;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			] >) a.getClass(), a, pos, b); //UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			T[] tmp = (T[]) Array.newInstance(type.getComponentType(), a.length + b.length);
			System_Renamed.arraycopy(a, 0, tmp, 0, pos);
			System_Renamed.arraycopy(b, 0, tmp, pos, b.length);
			System_Renamed.arraycopy(a, pos, tmp, pos + b.length, a.length - pos);
			return tmp;
			for (int i = 0; i < orig.length; i++)
			{
				if (orig[i].equals(e))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					T[] tmp = (T[]) Array.newInstance(orig.getClass().getComponentType(), orig.length - 1);
					Array.Copy(orig, 0, tmp, 0, i);
					if (i < tmp.length)
						System_Renamed.arraycopy(orig, i + 1, tmp, i, orig.length - i - 1);
					return tmp;
				}
			}
			return orig;
		}
		private ArrayUtils()
		{
			InitBlock();
		}
		
		public static bool[] copyOf(bool[] orig)
		{
			return copyOf(orig, orig.Length);
		}
		
		public static sbyte[] copyOf(sbyte[] orig)
		{
			return copyOf(orig, orig.Length);
		}
		
		public static char[] copyOf(char[] orig)
		{
			return copyOf(orig, orig.Length);
		}
		
		public static double[] copyOf(double[] orig)
		{
			return copyOf(orig, orig.Length);
		}
		
		public static float[] copyOf(float[] orig)
		{
			return copyOf(orig, orig.Length);
		}
		
		public static int[] copyOf(int[] orig)
		{
			return copyOf(orig, orig.Length);
		}
		
		public static long[] copyOf(long[] orig)
		{
			return copyOf(orig, orig.Length);
		}
		
		public static short[] copyOf(short[] orig)
		{
			return copyOf(orig, orig.Length);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T > T [] copyOf(T [] orig)
		
		// FIXME: replace with Arrays.copyOf() in Java 1.6
		public static bool[] copyOf(bool[] orig, int newLength)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			bool[] copy = new bool[newLength];
			Array.Copy(orig, 0, copy, 0, System.Math.Min(orig.Length, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOf() in Java 1.6
		public static sbyte[] copyOf(sbyte[] orig, int newLength)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] copy = new sbyte[newLength];
			Array.Copy(orig, 0, copy, 0, System.Math.Min(orig.Length, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOf() in Java 1.6
		public static char[] copyOf(char[] orig, int newLength)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			char[] copy = new char[newLength];
			Array.Copy(orig, 0, copy, 0, System.Math.Min(orig.Length, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOf() in Java 1.6
		public static double[] copyOf(double[] orig, int newLength)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			double[] copy = new double[newLength];
			Array.Copy(orig, 0, copy, 0, System.Math.Min(orig.Length, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOf() in Java 1.6
		public static float[] copyOf(float[] orig, int newLength)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			float[] copy = new float[newLength];
			Array.Copy(orig, 0, copy, 0, System.Math.Min(orig.Length, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOf() in Java 1.6
		public static int[] copyOf(int[] orig, int newLength)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int[] copy = new int[newLength];
			Array.Copy(orig, 0, copy, 0, System.Math.Min(orig.Length, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOf() in Java 1.6
		public static long[] copyOf(long[] orig, int newLength)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			long[] copy = new long[newLength];
			Array.Copy(orig, 0, copy, 0, System.Math.Min(orig.Length, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOf() in Java 1.6
		public static short[] copyOf(short[] orig, int newLength)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			short[] copy = new short[newLength];
			Array.Copy(orig, 0, copy, 0, System.Math.Min(orig.Length, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOf() in Java 1.6
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T > T [] copyOf(T [] orig, int newLength)
		
		// FIXME: replace with Arrays.copyOf() in Java 1.6
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T, U > T [] copyOf(U [] orig, int newLength, 
		Class < ? extends T [] > newType)
		
		// FIXME: replace with Arrays.copyOfRange() in Java 1.6
		public static bool[] copyOfRange(bool[] orig, int from, int to)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'newLength '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int newLength = to - from;
			if (newLength < 0)
				throw new System.ArgumentException();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			bool[] copy = new bool[newLength];
			Array.Copy(orig, from, copy, 0, System.Math.Min(orig.Length - from, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOfRange() in Java 1.6
		public static sbyte[] copyOfRange(sbyte[] orig, int from, int to)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'newLength '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int newLength = to - from;
			if (newLength < 0)
				throw new System.ArgumentException();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] copy = new sbyte[newLength];
			Array.Copy(orig, from, copy, 0, System.Math.Min(orig.Length - from, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOfRange() in Java 1.6
		public static char[] copyOfRange(char[] orig, int from, int to)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'newLength '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int newLength = to - from;
			if (newLength < 0)
				throw new System.ArgumentException();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			char[] copy = new char[newLength];
			Array.Copy(orig, from, copy, 0, System.Math.Min(orig.Length - from, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOfRange() in Java 1.6
		public static double[] copyOfRange(double[] orig, int from, int to)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'newLength '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int newLength = to - from;
			if (newLength < 0)
				throw new System.ArgumentException();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			double[] copy = new double[newLength];
			Array.Copy(orig, from, copy, 0, System.Math.Min(orig.Length - from, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOfRange() in Java 1.6
		public static float[] copyOfRange(float[] orig, int from, int to)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'newLength '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int newLength = to - from;
			if (newLength < 0)
				throw new System.ArgumentException();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			float[] copy = new float[newLength];
			Array.Copy(orig, from, copy, 0, System.Math.Min(orig.Length - from, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOfRange() in Java 1.6
		public static int[] copyOfRange(int[] orig, int from, int to)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'newLength '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int newLength = to - from;
			if (newLength < 0)
				throw new System.ArgumentException();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int[] copy = new int[newLength];
			Array.Copy(orig, from, copy, 0, System.Math.Min(orig.Length - from, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOfRange() in Java 1.6
		public static long[] copyOfRange(long[] orig, int from, int to)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'newLength '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int newLength = to - from;
			if (newLength < 0)
				throw new System.ArgumentException();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			long[] copy = new long[newLength];
			Array.Copy(orig, from, copy, 0, System.Math.Min(orig.Length - from, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOfRange() in Java 1.6
		public static short[] copyOfRange(short[] orig, int from, int to)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'newLength '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int newLength = to - from;
			if (newLength < 0)
				throw new System.ArgumentException();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'copy '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			short[] copy = new short[newLength];
			Array.Copy(orig, from, copy, 0, System.Math.Min(orig.Length - from, newLength));
			return copy;
		}
		
		// FIXME: replace with Arrays.copyOfRange() in Java 1.6
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T > T [] copyOfRange(T [] orig, int from, int to)
		
		// FIXME: replace with Arrays.copyOfRange() in Java 1.6
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T, U > T [] copyOfRange(U [] orig, int from, int to, 
		Class < ? extends T [] > newType)
		
		public static bool[] prepend(bool[] orig, bool e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			bool[] tmp = new bool[orig.Length + 1];
			tmp[0] = e;
			Array.Copy(orig, 0, tmp, 1, orig.Length);
			return tmp;
		}
		
		public static sbyte[] prepend(sbyte[] orig, sbyte e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] tmp = new sbyte[orig.Length + 1];
			tmp[0] = e;
			Array.Copy(orig, 0, tmp, 1, orig.Length);
			return tmp;
		}
		
		public static char[] prepend(char[] orig, char e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			char[] tmp = new char[orig.Length + 1];
			tmp[0] = e;
			Array.Copy(orig, 0, tmp, 1, orig.Length);
			return tmp;
		}
		
		public static double[] prepend(double[] orig, double e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			double[] tmp = new double[orig.Length + 1];
			tmp[0] = e;
			Array.Copy(orig, 0, tmp, 1, orig.Length);
			return tmp;
		}
		
		public static float[] prepend(float[] orig, float e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			float[] tmp = new float[orig.Length + 1];
			tmp[0] = e;
			Array.Copy(orig, 0, tmp, 1, orig.Length);
			return tmp;
		}
		
		public static int[] prepend(int[] orig, int e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int[] tmp = new int[orig.Length + 1];
			tmp[0] = e;
			Array.Copy(orig, 0, tmp, 1, orig.Length);
			return tmp;
		}
		
		public static long[] prepend(long[] orig, long e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			long[] tmp = new long[orig.Length + 1];
			tmp[0] = e;
			Array.Copy(orig, 0, tmp, 1, orig.Length);
			return tmp;
		}
		
		public static short[] prepend(short[] orig, short e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			short[] tmp = new short[orig.Length + 1];
			tmp[0] = e;
			Array.Copy(orig, 0, tmp, 1, orig.Length);
			return tmp;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T > T [] prepend(T [] orig, T e)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T, X extends T, Y extends T > T [] prepend(Class < T [] > type, 
		X [] orig, Y e)
		
		public static bool[] append(bool[] orig, bool e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			bool[] tmp = copyOf(orig, orig.Length + 1);
			tmp[orig.Length] = e;
			return tmp;
		}
		
		public static sbyte[] append(sbyte[] orig, sbyte e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] tmp = copyOf(orig, orig.Length + 1);
			tmp[orig.Length] = e;
			return tmp;
		}
		
		public static char[] append(char[] orig, char e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			char[] tmp = copyOf(orig, orig.Length + 1);
			tmp[orig.Length] = e;
			return tmp;
		}
		
		public static double[] append(double[] orig, double e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			double[] tmp = copyOf(orig, orig.Length + 1);
			tmp[orig.Length] = e;
			return tmp;
		}
		
		public static float[] append(float[] orig, float e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			float[] tmp = copyOf(orig, orig.Length + 1);
			tmp[orig.Length] = e;
			return tmp;
		}
		
		public static int[] append(int[] orig, int e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int[] tmp = copyOf(orig, orig.Length + 1);
			tmp[orig.Length] = e;
			return tmp;
		}
		
		public static long[] append(long[] orig, long e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			long[] tmp = copyOf(orig, orig.Length + 1);
			tmp[orig.Length] = e;
			return tmp;
		}
		
		public static short[] append(short[] orig, short e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			short[] tmp = copyOf(orig, orig.Length + 1);
			tmp[orig.Length] = e;
			return tmp;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T > T [] append(T [] orig, T e)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T, X extends T, Y extends T > T [] append(Class < T [] > type, 
		X [] orig, Y e)
		
		public static bool[] append_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(boolean [] a, boolean...b)
		
		public static sbyte[] append_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(byte [] a, byte...b)
		
		public static char[] append_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(char [] a, char...b)
		
		public static double[] append_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(double [] a, double...b)
		
		public static float[] append_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(float [] a, float...b)
		
		public static int[] append_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(int [] a, int...b)
		
		public static long[] append_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(long [] a, long...b)
		
		public static short[] append_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(short [] a, short...b)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T > T [] append(T [] a, T...b)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T, X extends T, Y extends T > T [] append(Class < T [] > type, 
		X [] a, Y...b)
		
		public static bool[] insert(bool[] orig, int pos, bool e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			bool[] tmp = new bool[orig.Length + 1];
			Array.Copy(orig, 0, tmp, 0, pos);
			tmp[pos] = e;
			Array.Copy(orig, pos, tmp, pos, orig.Length - pos);
			return tmp;
		}
		
		public static sbyte[] insert(sbyte[] orig, int pos, sbyte e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			sbyte[] tmp = new sbyte[orig.Length + 1];
			Array.Copy(orig, 0, tmp, 0, pos);
			tmp[pos] = e;
			Array.Copy(orig, pos, tmp, pos, orig.Length - pos);
			return tmp;
		}
		
		public static char[] insert(char[] orig, int pos, char e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			char[] tmp = new char[orig.Length + 1];
			Array.Copy(orig, 0, tmp, 0, pos);
			tmp[pos] = e;
			Array.Copy(orig, pos, tmp, pos, orig.Length - pos);
			return tmp;
		}
		
		public static double[] insert(double[] orig, int pos, double e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			double[] tmp = new double[orig.Length + 1];
			Array.Copy(orig, 0, tmp, 0, pos);
			tmp[pos] = e;
			Array.Copy(orig, pos, tmp, pos, orig.Length - pos);
			return tmp;
		}
		
		public static float[] insert(float[] orig, int pos, float e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			float[] tmp = new float[orig.Length + 1];
			Array.Copy(orig, 0, tmp, 0, pos);
			tmp[pos] = e;
			Array.Copy(orig, pos, tmp, pos, orig.Length - pos);
			return tmp;
		}
		
		public static int[] insert(int[] orig, int pos, int e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int[] tmp = new int[orig.Length + 1];
			Array.Copy(orig, 0, tmp, 0, pos);
			tmp[pos] = e;
			Array.Copy(orig, pos, tmp, pos, orig.Length - pos);
			return tmp;
		}
		
		public static long[] insert(long[] orig, int pos, long e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			long[] tmp = new long[orig.Length + 1];
			Array.Copy(orig, 0, tmp, 0, pos);
			tmp[pos] = e;
			Array.Copy(orig, pos, tmp, pos, orig.Length - pos);
			return tmp;
		}
		
		public static short[] insert(short[] orig, int pos, short e)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			short[] tmp = new short[orig.Length + 1];
			Array.Copy(orig, 0, tmp, 0, pos);
			tmp[pos] = e;
			Array.Copy(orig, pos, tmp, pos, orig.Length - pos);
			return tmp;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T > T [] insert(T [] orig, int pos, T e)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T, X extends T, Y extends T > T [] insert(Class < T [] > type, 
		X [] orig, int pos, Y e)
		
		public static bool[] insert_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(boolean [] a, int pos, boolean...b)
		
		public static sbyte[] insert_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(byte [] a, int pos, byte...b)
		
		public static char[] insert_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(char [] a, int pos, char...b)
		
		public static double[] insert_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(double [] a, int pos, double...b)
		
		public static float[] insert_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(float [] a, int pos, float...b)
		
		public static int[] insert_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(int [] a, int pos, int...b)
		
		public static long[] insert_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(long [] a, int pos, long...b)
		
		public static short[] insert_Renamed_Field;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(short [] a, int pos, short...b)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T > T [] insert(T [] a, int pos, T...b)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T, X extends T, Y extends T > T [] insert(Class < T [] > type, 
		X [] a, int pos, Y...b)
		
		public static float[] remove(float[] orig, float e)
		{
			for (int i = 0; i < orig.Length; i++)
			{
				if (orig[i] == e)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					float[] tmp = new float[orig.Length - 1];
					Array.Copy(orig, 0, tmp, 0, i);
					if (i < tmp.Length)
						Array.Copy(orig, i + 1, tmp, i, orig.Length - i - 1);
					return tmp;
				}
			}
			return orig;
		}
		
		public static int[] remove(int[] orig, int e)
		{
			for (int i = 0; i < orig.Length; i++)
			{
				if (orig[i] == e)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int[] tmp = new int[orig.Length - 1];
					Array.Copy(orig, 0, tmp, 0, i);
					if (i < tmp.Length)
						Array.Copy(orig, i + 1, tmp, i, orig.Length - i - 1);
					return tmp;
				}
			}
			return orig;
		}
		
		public static long[] remove(long[] orig, long e)
		{
			for (int i = 0; i < orig.Length; i++)
			{
				if (orig[i] == e)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					long[] tmp = new long[orig.Length - 1];
					Array.Copy(orig, 0, tmp, 0, i);
					if (i < tmp.Length)
						Array.Copy(orig, i + 1, tmp, i, orig.Length - i - 1);
					return tmp;
				}
			}
			return orig;
		}
		
		public static short[] remove(short[] orig, short e)
		{
			for (int i = 0; i < orig.Length; i++)
			{
				if (orig[i] == e)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'tmp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					short[] tmp = new short[orig.Length - 1];
					Array.Copy(orig, 0, tmp, 0, i);
					if (i < tmp.Length)
						Array.Copy(orig, i + 1, tmp, i, orig.Length - i - 1);
					return tmp;
				}
			}
			return orig;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static < T > T [] remove(T [] orig, T e)
	}
}