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
	
	[Serializable]
	class BSHLiteral:SimpleNode
	{
		public System.Object value_Renamed;
		
		internal BSHLiteral(int id):base(id)
		{
		}
		
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			if (value_Renamed == null)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new InterpreterError("Null in bsh literal: " + value_Renamed);
			}
			
			return value_Renamed;
		}
		
		private char getEscapeChar(char ch)
		{
			switch (ch)
			{
				
				case 'b': 
					ch = '\b';
					break;
				
				
				case 't': 
					ch = '\t';
					break;
				
				
				case 'n': 
					ch = '\n';
					break;
				
				
				case 'f': 
					ch = '\f';
					break;
				
				
				case 'r': 
					ch = '\r';
					break;
					
					// do nothing - ch already contains correct character
				
				case '"': 
				case '\'': 
				case '\\': 
					break;
				}
			
			return ch;
		}
		
		public virtual void  charSetup(System.String str)
		{
			char ch = str[0];
			if (ch == '\\')
			{
				// get next character
				ch = str[1];
				
				if (System.Char.IsDigit(ch))
				{
					//UPGRADE_TODO: Method 'java.lang.Integer.parseInt' was converted to 'System.Convert.ToInt32' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					ch = (char) System.Convert.ToInt32(str.Substring(1), 8);
				}
				else
					ch = getEscapeChar(ch);
			}
			
			value_Renamed = new Primitive(Character.valueOf(ch).charValue());
		}
		
		internal virtual void  stringSetup(System.String str)
		{
			System.Text.StringBuilder buffer = new System.Text.StringBuilder();
			for (int i = 0; i < str.Length; i++)
			{
				char ch = str[i];
				if (ch == '\\')
				{
					// get next character
					ch = str[++i];
					
					if (System.Char.IsDigit(ch))
					{
						int endPos = i;
						
						// check the next two characters
						while (endPos < i + 2)
						{
							if (System.Char.IsDigit(str[endPos + 1]))
								endPos++;
							else
								break;
						}
						
						//UPGRADE_TODO: Method 'java.lang.Integer.parseInt' was converted to 'System.Convert.ToInt32' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
						ch = (char) System.Convert.ToInt32(str.Substring(i, (endPos + 1) - (i)), 8);
						i = endPos;
					}
					else
						ch = getEscapeChar(ch);
				}
				
				buffer.Append(ch);
			}
			
			value_Renamed = String.Intern(buffer.ToString());
		}
	}
}