/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
* Created by IntelliJ IDEA.
* User: rkinney
* Date: Aug 31, 2002
* Time: 10:20:59 AM
* To change template for new class use
* Code Style | Class Templates options (Tools | IDE Options).
*/
using System;
namespace VassalSharp.tools
{
	
	/// <summary> Quicksort implementation so we can sort using JRE 1.1</summary>
	/// <deprecated> Use {@link java.util.Collections.sort} instead.
	/// </deprecated>
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	Deprecated
	public class Sort
	{
		public Sort()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			System.Object tmp = v.elementAt(i);
			v.setElementAt(v.elementAt(j), i);
			v.setElementAt(tmp, j);
			
			int i, last;
			
			if (left >= right)
			{
				// do nothing if array size < 2
				return ;
			}
			swap(v, left, (left + right) / 2);
			last = left;
			for (i = left + 1; i <= right; i++)
			{
				System.Object o1 = v.elementAt(i);
				System.Object o2 = v.elementAt(left);
				if (comp.compare(o1, o2) < 0)
				{
					swap(v, ++last, i);
				}
			}
			swap(v, left, last);
			quicksort(v, left, last - 1, comp);
			quicksort(v, last + 1, right, comp);
			quicksort(v, 0, v.size() - 1, comp);
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private static
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void swap(Vector < Object > v, int i, int j)
		
		//------------------------------------------------------------------
		/*
		* quicksort a vector of objects.
		*
		* @param v - a vector of objects
		* @param left - the start index - from where to begin sorting
		* @param right - the last index.
		*/
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private static
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void quicksort(
		Vector < Object > v, int left, int right, Comparator comp)
		//------------------------------------------------------------------
		/*
		* quicksort an array of objects.
		*
		* @param arr[] - an array of objects
		* @param left - the start index - from where to begin sorting
		* @param right - the last index.
		private static void quicksort(
		IComparable arr[], int left, int right, boolean ascending) {
		
		int i, last;
		
		if (left >= right) { // do nothing if array size < 2
		return;
		}
		swap(arr, left, (left+right) / 2);
		last = left;
		for (i = left+1; i <= right; i++) {
		if (ascending && arr[i].compareTo(arr[left]) < 0 ) {
		swap(arr, ++last, i);
		}
		else if (!ascending && arr[i].compareTo(arr[left]) < 0 ) {
		swap(arr, ++last, i);
		}
		}
		swap(arr, left, last);
		quicksort(arr, left, last-1,ascending);
		quicksort(arr, last+1, right,ascending);
		}*/
		//------------------------------------------------------------------
		/// <summary> Quicksort will rearrange elements when they are all equal. Make sure
		/// at least two elements differ
		/// public static boolean needsSorting(Vector v) {
		/// IComparable prev = null;
		/// IComparable curr;
		/// for (Enumeration e = v.elements(); e.hasMoreElements(); )
		/// {
		/// curr = (IComparable)e.nextElement();
		/// if (prev != null && prev.compareTo(curr) != 0)
		/// return true;
		/// prev = curr;
		/// }
		/// return false;
		/// }
		/// </summary>
		/*
		* Preform a sort using the specified comparitor object.
		*/
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void quicksort(Vector < Object > v, Comparator comp)
		
		/// <deprecated> Use {@link java.util.Comparator} instead.
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public interface Comparator
		{
			int compare(System.Object o1, System.Object o2);
		}
		
		/// <summary> Compares two String objects</summary>
		/// <deprecated> Use the natural ordering on Strings instead.
		/// </deprecated>
		/// <seealso cref="java.lang.String.compareTo(String)">
		/// </seealso>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		public class Alpha : Sort.Comparator
		{
			public virtual int compare(System.Object o1, System.Object o2)
			{
				System.String s1 = (System.String) o1;
				System.String s2 = (System.String) o2;
				int len = System.Math.Min(s1.Length, s2.Length);
				int result = 0;
				for (int i = 0; i < len; ++i)
				{
					result = (int) s1[i] - (int) s2[i];
					if (result != 0)
					{
						return result;
					}
				}
				if (s1.Length > len)
				{
					return 1;
				}
				else if (s2.Length > len)
				{
					return - 1;
				}
				else
				{
					return 0;
				}
			}
		}
	}
}