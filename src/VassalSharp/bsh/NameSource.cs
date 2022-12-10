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
	
	/// <summary>This interface supports name completion, which is used primarily for 
	/// command line tools, etc.  It provides a flat source of "names" in a 
	/// space.  For example all of the classes in the classpath or all of the 
	/// variables in a namespace (or all of those).
	/// <p>
	/// NameSource is the lightest weight mechanism for sources which wish to
	/// support name completion.  In the future it might be better for NameSpace
	/// to implement NameCompletion directly in a more native and efficient 
	/// fasion.  However in general name competion is used for human interaction
	/// and therefore does not require high performance.
	/// <p>
	/// </summary>
	/// <seealso cref="bsh.util.NameCompletion">
	/// </seealso>
	/// <seealso cref="bsh.util.NameCompletionTable">
	/// </seealso>
	public interface NameSource
	{
		System.String[] AllNames
		{
			get;
			
		}
		void  addNameSourceListener(bsh.Listener listener);
		
	}
	//UPGRADE_NOTE: Interface 'Listener' was extracted from interface 'NameSource'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1025'"
	public interface Listener
	{
		void  nameSourceChanged(NameSource src);
		/// <summary>Provide feedback on the progress of mapping a namespace</summary>
		/// <param name="msg">is an update about what's happening
		/// </param>
		/// <perc>  is an integer in the range 0-100 indicating percentage done </perc>
		/// <summary>public void nameSourceMapping( 
		/// NameSource src, String msg, int perc );
		/// </summary>
	}
}