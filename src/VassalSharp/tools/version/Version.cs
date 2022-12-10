/*
* $Id$
*
* Copyright (c) 2008 by Joel Uckelman
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
namespace VassalSharp.tools.version
{
	
	/// <summary> A dotted-integer version, pre-parsed for easy comparison.
	/// 
	/// </summary>
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	/// <seealso cref="VersionTokenizer">
	/// </seealso>
	/// <seealso cref="VassalVersion">
	/// </seealso>
	public class Version : System.IComparable
	{
		virtual public bool Valid
		{
			get
			{
				return valid;
			}
			
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< Version >
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected final List < Integer > tokens = new ArrayList < Integer >();
		//UPGRADE_NOTE: Final was removed from the declaration of 'vstring '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal System.String vstring;
		//UPGRADE_NOTE: Final was removed from the declaration of 'valid '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		protected internal bool valid;
		
		/// <summary> A dotted-integer version.
		/// 
		/// </summary>
		/// <param name="v">a version string
		/// </param>
		public Version(System.String v):this(v, new SimpleVersionTokenizer(v))
		{
		}
		
		protected internal Version(System.String v, VersionTokenizer tok)
		{
			vstring = v;
			
			bool parsed;
			try
			{
				while (tok.hasNext())
					tokens.add(tok.next());
				parsed = true;
			}
			catch (VersionFormatException e)
			{
				parsed = false;
			}
			
			valid = parsed;
		}
		
		/// <summary> Compares dotted-integer version strings.
		/// 
		/// </summary>
		/// <returns> negative if {@code this < v}, positive if {@code this > v},
		/// and zero if {@code this == v} or if the parseable parts of the
		/// versions are equal.
		/// </returns>
		public virtual int compareTo(Version v)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Iterator < Integer > i = this.tokens.iterator();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final Iterator < Integer > j = v.tokens.iterator();
			
			// find the first token where this and v differ
			while (i.hasNext() && j.hasNext())
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int a = i.next();
				//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int b = j.next();
				
				if (a != b)
					return a - b;
			}
			
			// versions which match up to the point of invalidity are equal
			if (!this.Valid || !v.Valid)
				return 0;
			
			// otherwise, the shorter one is earlier; or they're the same
			return i.hasNext()?1:(j.hasNext()?- 1:0);
		}
		
		public override System.String ToString()
		{
			return vstring;
		}
		//UPGRADE_TODO: The following method was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		virtual public System.Int32 CompareTo(System.Object obj)
		{
			return 0;
		}
	}
}