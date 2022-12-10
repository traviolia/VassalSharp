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
//UPGRADE_TODO: The type 'java.util.regex.Matcher' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Matcher = java.util.regex.Matcher;
//UPGRADE_TODO: The type 'java.util.regex.Pattern' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using Pattern = java.util.regex.Pattern;
namespace VassalSharp.tools.version
{
	
	/// <summary> A finite-state machine for converting VASSAL version numbers into
	/// a series of integers. The integers thus returned from two different
	/// tokenizers may be compared to determine the temporal ordering of two
	/// VASSAL versions. Valid version strings are matched by the following
	/// regular expression: <code>\d+(.\d+)*(-(svn\d+|.+)?</code>. Old
	/// version numbers which are not valid by current standards (e.g., 3.0b6)
	/// may be successfully parsed far enough to determine their ordering with
	/// respect to post-3.1.0 versions.
	/// 
	/// <p>Nonnumeric parts of a version string are tokenized as follows:</p>
	/// <table>
	/// <tr><td>end-of-string</td><td>-1</td></tr>
	/// <tr><td><code>-</code> (tag delimiter)</td><td>-2</td></tr>
	/// <tr><td><code>svn\d+</code></td><td><code>\d+</code></td></tr>
	/// <tr><td>other tags</td><td>mapped to svn version</td></tr>
	/// </table>
	/// <p>This mapping ensures that of two version strings with the same
	/// version number, if one has a tag (the part starting with the hyphen) but
	/// the other does not, then one with the tag will have a lexically smaller
	/// token stream than the one without. E.g., 3.1.0-svn2708 &lt; 3.1.0,
	/// since the two token streams are <code>3 1 0 -2 2708 -1</code> and
	/// <code>3 1 0 -1</code>, respectively.</p>
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	/// <seealso cref="Version">
	/// </seealso>
	/// <seealso cref="VersionFormatException">
	/// </seealso>
	public class VassalVersionTokenizer : VersionTokenizer
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			NUM, DELIM, TAG, EOS, END state = State.NUM;
		}
		protected internal System.String v;
		
		protected internal enum_Renamed State;
		
		//UPGRADE_NOTE: The initialization of  'state' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal State state;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected static Map < String, Integer > tags = new HashMap < String, Integer >();
		
		/// <summary> Constructs a <code>VersionTokenizer</code> which operates on a
		/// version <code>String</code>.
		/// 
		/// </summary>
		/// <param name="version">the version <code>String</code> to parse
		/// </param>
		/// <throws>  IllegalArgumentException if <code>version == null</code>. </throws>
		public VassalVersionTokenizer(System.String version)
		{
			InitBlock();
			if (version == null)
				throw new System.ArgumentException();
			v = version;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual bool hasNext()
		{
			return v.Length > 0 || state == State.EOS;
		}
		
		/// <summary>{@inheritDoc} </summary>
		public virtual int next()
		{
			if (!hasNext())
				throw new System.ArgumentOutOfRangeException();
			
			int n;
			
			while (true)
			{
				switch (state)
				{
					
					case NUM:  // read a version number
						//UPGRADE_NOTE: Final was removed from the declaration of 'm '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						Matcher m = Pattern.compile("^\\d+").matcher(v);
						if (!m.lookingAt())
							throw new VersionFormatException();
						try
						{
							n = Integer.parseInt(m.group());
							if (n < 0)
								throw new VersionFormatException();
						}
						catch (System.FormatException e)
						{
							throw new VersionFormatException(e);
						}
						v = v.substring(m.end());
						state = v.Length == 0?State.EOS:State.DELIM;
						return n;
					
					case DELIM:  // eat delimiters
						switch (v[0])
						{
							
							case '.': 
								state = State.NUM;
								v = v.Substring(1);
								break;
							
							case '-': 
								state = State.TAG;
								v = v.Substring(1);
								return - 2;
							
							default: 
								throw new VersionFormatException();
							
						}
						break;
					
					case TAG:  // parse the tag
						if (v.StartsWith("svn"))
						{
							// report the svn version
							v = v.Substring(3);
							try
							{
								n = System.Int32.Parse(v);
								if (n < 0)
									throw new VersionFormatException();
							}
							catch (System.FormatException e)
							{
								throw new VersionFormatException(e);
							}
						}
						else if (tags.containsKey(v))
						{
							// convert the tag to an svn version
							n = tags.get_Renamed(v);
						}
						else
							throw new VersionFormatException();
						
						v = "";
						state = State.EOS;
						return n;
					
					case EOS:  // mark the end of the string
						state = State.END;
						return - 1;
					
					case END:  // this case is terminal
						throw new System.SystemException();
					}
			}
		}
		static VassalVersionTokenizer()
		{
			// This is the mapping for tags to svn versions. Only tags which cannot
			// be distinguished from the current version from the numeric portion
			// alone need to be maintined here. (E.g., the 3.1.0 tags can be removed
			// as soon as 3.1.1 is released.) We keep one tag for testing purposes.
			{
				// 3.2.0
				tags.put("beta1", 8193);
				tags.put("beta2", 8345);
				tags.put("beta3", 8383);
				tags.put("beta4", 8419);
			}
		}
	}
}