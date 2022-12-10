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
	
	/// <summary>A formal parameter declaration.
	/// For loose variable declaration type is null.
	/// </summary>
	[Serializable]
	class BSHFormalParameter:SimpleNode
	{
		//UPGRADE_NOTE: Final was removed from the declaration of 'UNTYPED'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly System.Type UNTYPED = null;
		public System.String name;
		// unsafe caching of type here
		public System.Type type;
		
		internal BSHFormalParameter(int id):base(id)
		{
		}
		
		public virtual System.String getTypeDescriptor(CallStack callstack, Interpreter interpreter, System.String defaultPackage)
		{
			if (jjtGetNumChildren() > 0)
				return ((BSHType) jjtGetChild(0)).getTypeDescriptor(callstack, interpreter, defaultPackage);
			// this will probably not get used
			else
				return "Ljava/lang/Object;"; // Object type
		}
		
		/// <summary>Evaluate the type.</summary>
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			if (jjtGetNumChildren() > 0)
				type = ((BSHType) jjtGetChild(0)).getType(callstack, interpreter);
			else
				type = UNTYPED;
			
			return type;
		}
	}
}