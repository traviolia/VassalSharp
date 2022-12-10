/// <summary>**************************************************************************
/// *
/// This file is part of the BeanShell Java Scripting distribution.          *
/// Documentation and updates may be found at http://www.beanshell.org/      *
/// *
/// Sun Public License Notice:                                               *
/// *
/// The contents of this file are subject to the Sun Public License Version  *
/// 1.0 (the "License"); you may not use this file except in compliance with *
/// the License. A copy of the License is available at http://www.sun.com    * 
/// *
/// The Original Code is BeanShell. The Initial Developer of the Original    *
/// Code is Pat Niemeyer. Portions created by Pat Niemeyer are Copyright     *
/// (C) 2000.  All Rights Reserved.                                          *
/// *
/// GNU Public License Notice:                                               *
/// *
/// Alternatively, the contents of this file may be used under the terms of  *
/// the GNU Lesser General Public License (the "LGPL"), in which case the    *
/// provisions of LGPL are applicable instead of those above. If you wish to *
/// allow use of your version of this file only under the  terms of the LGPL *
/// and not to allow others to use your version of this file under the SPL,  *
/// indicate your decision by deleting the provisions above and replace      *
/// them with the notice and other provisions required by the LGPL.  If you  *
/// do not delete the provisions above, a recipient may use your version of  *
/// this file under either the SPL or the LGPL.                              *
/// *
/// Patrick Niemeyer (pat@pat.net)                                           *
/// Author of Learning Java, O'Reilly & Associates                           *
/// http://www.pat.net/~pat/                                                 *
/// *
/// ***************************************************************************
/// </summary>
using System;
namespace bsh
{
	
	public class StringUtil
	{
		
		public static System.String[] split(System.String s, System.String delim)
		{
			System.Collections.ArrayList v = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			SupportClass.Tokenizer st = new SupportClass.Tokenizer(s, delim);
			while (st.HasMoreTokens())
				v.Add(st.NextToken());
			System.String[] sa = new System.String[v.Count];
			v.CopyTo(sa);
			return sa;
		}
		
		public static System.String[] bubbleSort(System.String[] in_Renamed)
		{
			System.Collections.ArrayList v = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			for (int i = 0; i < in_Renamed.Length; i++)
				v.Add(in_Renamed[i]);
			
			int n = v.Count;
			bool swap = true;
			while (swap)
			{
				swap = false;
				for (int i = 0; i < (n - 1); i++)
					if (String.CompareOrdinal(((System.String) v[i]), ((System.String) v[i + 1])) > 0)
					{
						System.String tmp = (System.String) v[i + 1];
						v.RemoveAt(i + 1);
						v.Insert(i, tmp);
						swap = true;
					}
			}
			
			System.String[] out_Renamed = new System.String[n];
			v.CopyTo(out_Renamed);
			return out_Renamed;
		}
		
		
		public static System.String maxCommonPrefix(System.String one, System.String two)
		{
			int i = 0;
			while (String.Compare(one, 0, two, 0, i) == 0)
				i++;
			return one.Substring(0, (i - 1) - (0));
		}
		
		public static System.String methodString(System.String name, System.Type[] types)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder(name + "(");
			if (types.Length > 0)
				sb.Append(" ");
			for (int i = 0; i < types.Length; i++)
			{
				System.Type c = types[i];
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				sb.Append(((c == null)?"null":c.FullName) + (i < (types.Length - 1)?", ":" "));
			}
			sb.Append(")");
			return sb.ToString();
		}
		
		/// <summary>Split a filename into dirName, baseName</summary>
		/// <returns> String [] { dirName, baseName }
		/// public String [] splitFileName( String fileName ) 
		/// { 
		/// String dirName, baseName;
		/// int i = fileName.lastIndexOf( File.separator );
		/// if ( i != -1 ) {
		/// dirName = fileName.substring(0, i);
		/// baseName = fileName.substring(i+1);
		/// } else
		/// baseName = fileName;
		/// return new String[] { dirName, baseName };
		/// }
		/// </returns>
		
		/// <summary>Hack - The real method is in Reflect.java which is not public.</summary>
		public static System.String normalizeClassName(System.Type type)
		{
			return Reflect.normalizeClassName(type);
		}
	}
}