/*
* $Id$
*
* Copyright (c) 2000-2009 by Rodney Kinney, Joel Uckelman
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
using System.Text;
using ColorConfigurer = VassalSharp.configure.ColorConfigurer;
//using HotKeyConfigurer = VassalSharp.configure.HotKeyConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using PropertyExpression = VassalSharp.configure.PropertyExpression;
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
namespace VassalSharp.tools
{
	
	/// <summary>
	/// Encodes a sequence of Strings into a single String with a given delimiter.
	/// Escapes the delimiter character if it occurs in the element strings.
	/// 
	/// This is a very handy class for storing structured data into flat text and
	/// quite a bit faster than parsing an XML document.
	/// 
	/// For example, a structure such as {A,{B,C}} can be encoded with
	/// 
	/// <pre>
	/// new SequenceEncoder("A",',').append(new SequenceEncoder("B",',').append("C").getValue()).getValue()
	/// </pre>
	/// 
	/// which returns <code>A,B\,C</code>
	/// 
	/// and then extracted with
	/// 
	/// <pre>
	/// SequenceEncoder.Decoder st = new SequenceEncoder.Decoder("A,B\,C",',');
	/// String A = st.nextToken();
	/// SequenceEncoder.Decoder BC = new SequenceEncoder.Decoder(st.nextToken(),',');
	/// String B = BC.nextToken();
	/// String C = BC.nextToken();
	/// </pre>
	/// </summary>
	public class SequenceEncoder
	{
		virtual public System.String Value
		{
			get
			{
				return buffer != null?buffer.ToString():null;
			}
			
		}
		private StringBuilder buffer;
		//UPGRADE_NOTE: Final was removed from the declaration of 'delimit '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private char delimit;
		
		public SequenceEncoder(char delimiter)
		{
			delimit = delimiter;
		}
		
		public SequenceEncoder(System.String val, char delimiter):this(delimiter)
		{
			append(val);
		}
		
		public virtual SequenceEncoder append(System.String s)
		{
			// start the buffer, or add delimiter after previous token
			if (buffer == null)
			{
				buffer = new StringBuilder();
			}
			else
			{
				buffer.Append(delimit);
			}
			
			if (s != null)
			{
				if (s.EndsWith("\\") || (s.StartsWith("'") && s.EndsWith("'")))
				{
					buffer.Append("'");
					appendEscapedString(s);
					buffer.Append("'");
				}
				else
				{
					appendEscapedString(s);
				}
			}
			
			return this;
		}
		
		public virtual SequenceEncoder append(char c)
		{
			return append(System.Convert.ToString(c));
		}
		
		public virtual SequenceEncoder append(int i)
		{
			return append(System.Convert.ToString(i));
		}
		
		public virtual SequenceEncoder append(long l)
		{
			return append(System.Convert.ToString(l));
		}
		
		public virtual SequenceEncoder append(double d)
		{
			return append(System.Convert.ToString(d));
		}
		
		public virtual SequenceEncoder append(bool b)
		{
			return append(System.Convert.ToString(b));
		}
		
		public virtual SequenceEncoder append(System.Windows.Forms.KeyEventArgs stroke)
		{
			//return append(HotKeyConfigurer.encode(stroke));
			throw new InvalidOperationException();
		}
		
		public virtual SequenceEncoder append(NamedKeyStroke stroke)
		{
			return append(NamedHotKeyConfigurer.encode(stroke));
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public virtual SequenceEncoder append(ref System.Drawing.Color color)
		{
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			return append(ColorConfigurer.colorToString(ref color));
		}
		
		public virtual SequenceEncoder append(System.String[] s)
		{
			return append(StringArrayConfigurer.arrayToString(s));
		}
		
		public virtual SequenceEncoder append(PropertyExpression p)
		{
			return append(p.Expression);
		}
		
		private void  appendEscapedString(System.String s)
		{
			int begin = 0;
			int end = s.IndexOf((System.Char) delimit);
			
			while (begin <= end)
			{
				buffer.Append(s.Substring(begin, (end) - (begin))).Append('\\');
				begin = end;
				//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'System.String.IndexOf' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
				end = s.IndexOf((System.Char) delimit, end + 1);
			}
			
			buffer.Append(s.Substring(begin));
		}
		
		public class Decoder : System.Collections.IEnumerator
		{
			public virtual System.Object Current
			{
				get
				{
					return nextToken();
				}
				
			}

			private System.String val;
			//UPGRADE_NOTE: Final was removed from the declaration of 'delimit '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			private char delimit;
			
			public Decoder(System.String value_Renamed, char delimiter)
			{
				val = value_Renamed;
				delimit = delimiter;
			}
			
			public virtual bool hasMoreTokens()
			{
				return val != null;
			}
			
			public virtual System.String nextToken()
			{
				if (!hasMoreTokens())
					throw new System.ArgumentOutOfRangeException();
				
				System.String value_Renamed;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'i '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int i = val.IndexOf((System.Char) delimit);
				if (i < 0)
				{
					value_Renamed = val;
					val = null;
				}
				else
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'buffer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					StringBuilder buffer = new StringBuilder();
					int begin = 0;
					int end = i;
					while (begin < end)
					{
						if (val[end - 1] == '\\')
						{
							buffer.Append(val.Substring(begin, (end - 1) - (begin)));
							begin = end;
							//UPGRADE_WARNING: Method 'java.lang.String.indexOf' was converted to 'System.String.IndexOf' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
							end = val.IndexOf((System.Char) delimit, end + 1);
						}
						else
						{
							break;
						}
					}
					
					if (end < 0)
					{
						buffer.Append(val.Substring(begin));
						val = null;
					}
					else
					{
						buffer.Append(val.Substring(begin, (end) - (begin)));
						val = end >= val.Length - 1?"":val.Substring(end + 1);
					}
					
					value_Renamed = buffer.ToString();
				}
				
				if (value_Renamed.StartsWith("'") && value_Renamed.EndsWith("'") && value_Renamed.Length > 1)
				{
					value_Renamed = value_Renamed.Substring(1, (value_Renamed.Length - 1) - (1));
				}
				
				return value_Renamed;
			}
			
			public virtual bool MoveNext()
			{
				return hasMoreTokens();
			}
			
			//UPGRADE_NOTE: The equivalent of method 'java.util.Iterator.remove' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
			public virtual void  remove()
			{
				throw new System.NotSupportedException();
			}
			
			public virtual Decoder copy()
			{
				return new Decoder(val, delimit);
			}
			
			/// <summary> Parse the next token into an integer</summary>
			/// <param name="defaultValue">Return this value if no more tokens, or next token doesn't parse to an integer
			/// </param>
			/// <returns>
			/// </returns>
			public virtual int nextInt(int defaultValue)
			{
				if (val != null)
				{
					try
					{
						defaultValue = System.Int32.Parse(nextToken());
					}
					catch (System.FormatException e)
					{
					}
				}
				return defaultValue;
			}
			
			public virtual long nextLong(long defaultValue)
			{
				if (val != null)
				{
					try
					{
						defaultValue = System.Int64.Parse(nextToken());
					}
					catch (System.FormatException e)
					{
					}
				}
				return defaultValue;
			}
			
			public virtual double nextDouble(double defaultValue)
			{
				if (val != null)
				{
					try
					{
						defaultValue = System.Double.Parse(nextToken());
					}
					catch (System.FormatException e)
					{
					}
				}
				return defaultValue;
			}
			
			public virtual bool nextBoolean(bool defaultValue)
			{
				return val != null?"true".Equals(nextToken()):defaultValue;
			}
			
			/// <summary> Return the first character of the next token</summary>
			/// <param name="defaultValue">Return this value if no more tokens, or if next token has zero length
			/// </param>
			/// <returns>
			/// </returns>
			public virtual char nextChar(char defaultValue)
			{
				if (val != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String s = nextToken();
					defaultValue = s.Length > 0?s[0]:defaultValue;
				}
				return defaultValue;
			}
			
			public virtual System.Windows.Forms.KeyEventArgs nextKeyStroke(char defaultValue)
			{
				return nextKeyStroke(new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (defaultValue | (int) System.Windows.Forms.Keys.Control)));
			}
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			public virtual System.Drawing.Color nextColor(ref System.Drawing.Color defaultValue)
			{
				if (val != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String s = nextToken();
					if (s.Length > 0)
					{
						defaultValue = ColorConfigurer.stringToColor(s);
					}
					else
					{
						defaultValue = System.Drawing.Color.Empty;
					}
				}
				return defaultValue;
			}
			
			public virtual System.Windows.Forms.KeyEventArgs nextKeyStroke(System.Windows.Forms.KeyEventArgs defaultValue)
			{
				if (val != null)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String s = nextToken();
					if (s.Length == 0)
					{
						defaultValue = null;
					}
					else if (s.IndexOf(',') < 0)
					{
						defaultValue = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (s[0] | (int) System.Windows.Forms.Keys.Control));
					}
					else
					{
						//defaultValue = HotKeyConfigurer.decode(s);
						throw new NotImplementedException();
					}
				}
				return defaultValue;
			}
			
			public virtual NamedKeyStroke nextNamedKeyStroke(char defaultValue)
			{
				return nextNamedKeyStroke(NamedKeyStroke.getNamedKeyStroke(defaultValue, (int) System.Windows.Forms.Keys.Control));
			}
			
			public virtual NamedKeyStroke nextNamedKeyStroke()
			{
				return nextNamedKeyStroke(NamedKeyStroke.NULL_KEYSTROKE);
			}
			
			public virtual NamedKeyStroke nextNamedKeyStroke(NamedKeyStroke defaultValue)
			{
				if (val != null)
				{
					System.String s = nextToken();
					if (s.Length == 0)
					{
						defaultValue = null;
					}
					else if (s.IndexOf(',') < 0)
					{
						defaultValue = NamedKeyStroke.getNamedKeyStroke(s[0], (int) System.Windows.Forms.Keys.Control);
					}
					else
					{
						defaultValue = NamedHotKeyConfigurer.decode(s);
					}
				}
				return defaultValue == null?NamedKeyStroke.NULL_KEYSTROKE:defaultValue;
			}
			
			/// <summary> Return the next token, or the default value if there are no more tokens</summary>
			/// <param name="defaultValue">
			/// </param>
			/// <returns>
			/// </returns>
			public virtual System.String nextToken(System.String defaultValue)
			{
				return val != null?nextToken():defaultValue;
			}
			
			public virtual System.String[] nextStringArray(int minLength)
			{
				System.String[] retVal;
				if (val != null)
				{
					retVal = StringArrayConfigurer.stringToArray(nextToken());
				}
				else
				{
					retVal = new System.String[0];
				}
				
				if (retVal.Length < minLength)
				{
					Array.Copy(retVal, retVal, minLength);
				}
				
				return retVal;
			}

			virtual public void  Reset()
			{
			}
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			SequenceEncoder se = new SequenceEncoder(',');
			for (int i = 0; i < args.Length; ++i)
			{
				se.append(args[i]);
			}
			System.Console.Out.WriteLine(se.Value);
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(se.Value, ',');
			while (st.hasMoreTokens())
			{
				System.Console.Out.WriteLine(st.nextToken());
			}
		}
	}
}