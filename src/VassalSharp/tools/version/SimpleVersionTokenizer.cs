/*
* $Id$
*
* Copyright (c) 2007 by Joel Uckelman
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
	
	/// <summary> A finite-state machine for converting version numbers into a series of
	/// integers. The integers thus returned from two different tokenizers may
	/// be compared to determine the temporal ordering of two versions. Valid
	/// version strings are matched by the following regular expression:
	/// <code>\d+(.\d+)*</code>. Invalid version numbers may be parsed up to
	/// the point where they become invalid. The final token in any completely
	/// parsed version is <code>-1</code>.
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
	public class SimpleVersionTokenizer : VersionTokenizer
	{
		private void  InitBlock()
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			NUM, DELIM, EOS, END state = State.NUM;
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
		public SimpleVersionTokenizer(System.String version)
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
							
							default: 
								throw new VersionFormatException();
							
						}
						break;
					
					case EOS:  // mark the end of the string
						state = State.END;
						return - 1;
					
					case END:  // this case is terminal
						throw new System.SystemException();
					}
			}
		}
	}
}