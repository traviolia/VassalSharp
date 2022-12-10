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
	
	/// <summary>This is a quick hack to turn empty lines entered interactively on the 
	/// command line into ';\n' empty lines for the interpreter.  It's just more 
	/// pleasant to be able to hit return on an empty line and see the prompt 
	/// reappear.
	/// </summary>
	/// <summary>This is *not* used when text is sourced from a file non-interactively.
	/// </summary>
	class CommandLineReader:System.IO.StreamReader
	{
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public CommandLineReader(System.IO.StreamReader in_Renamed):base(in_Renamed.BaseStream, in_Renamed.CurrentEncoding)
		{
		}
		
		internal const int normal = 0;
		internal const int lastCharNL = 1;
		internal const int sentSemi = 2;
		
		internal int state = lastCharNL;
		
		public  override int Read()
		{
			int b;
			
			if (state == sentSemi)
			{
				state = lastCharNL;
				return '\n';
			}
			
			// skip CR
			//UPGRADE_TODO: Method 'java.io.Reader.read' was converted to 'System.IO.StreamReader.Read' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioReaderread'"
			//UPGRADE_TODO: Field 'java.io.FilterReader.in' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			while ((b = in_Renamed.Read()) == '\r')
				;
			
			if (b == '\n')
				if (state == lastCharNL)
				{
					b = ';';
					state = sentSemi;
				}
				else
					state = lastCharNL;
			else
				state = normal;
			
			return b;
		}
		
		/// <summary>This is a degenerate implementation.
		/// I don't know how to keep this from blocking if we try to read more
		/// than one char...  There is no available() for Readers ??
		/// </summary>
		public  override int Read(System.Char[] buff, int off, int len)
		{
			int b = Read();
			if (b == - 1)
				return - 1;
			// EOF, not zero read apparently
			else
			{
				buff[off] = (char) b;
				return 1;
			}
		}
		
		// Test it
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.Reader' and 'System.IO.StreamReader' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.IO.StreamReader in_Renamed = new CommandLineReader(new System.IO.StreamReader(System.Console.OpenStandardInput(), System.Text.Encoding.Default));
			while (true)
			{
				//UPGRADE_TODO: Method 'java.io.Reader.read' was converted to 'System.IO.StreamReader.Read' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioReaderread'"
				System.Console.Out.WriteLine(in_Renamed.Read());
			}
		}
	}
}