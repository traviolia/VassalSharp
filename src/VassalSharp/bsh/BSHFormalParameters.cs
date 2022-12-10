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
	class BSHFormalParameters:SimpleNode
	{
		virtual public System.String[] ParamNames
		{
			get
			{
				insureParsed();
				return paramNames;
			}
			
		}
		private System.String[] paramNames;
		/// <summary>For loose type parameters the paramTypes are null.</summary>
		// unsafe caching of types
		internal System.Type[] paramTypes;
		internal int numArgs;
		internal System.String[] typeDescriptors;
		
		internal BSHFormalParameters(int id):base(id)
		{
		}
		
		internal virtual void  insureParsed()
		{
			if (this.paramNames != null)
				return ;
			
			this.numArgs = jjtGetNumChildren();
			System.String[] paramNames = new System.String[numArgs];
			
			for (int i = 0; i < numArgs; i++)
			{
				BSHFormalParameter param = (BSHFormalParameter) jjtGetChild(i);
				paramNames[i] = param.name;
			}
			
			this.paramNames = paramNames;
		}
		
		public virtual System.String[] getTypeDescriptors(CallStack callstack, Interpreter interpreter, System.String defaultPackage)
		{
			if (typeDescriptors != null)
				return typeDescriptors;
			
			insureParsed();
			System.String[] typeDesc = new System.String[numArgs];
			
			for (int i = 0; i < numArgs; i++)
			{
				BSHFormalParameter param = (BSHFormalParameter) jjtGetChild(i);
				typeDesc[i] = param.getTypeDescriptor(callstack, interpreter, defaultPackage);
			}
			
			this.typeDescriptors = typeDesc;
			return typeDesc;
		}
		
		/// <summary>Evaluate the types.  
		/// Note that type resolution does not require the interpreter instance.
		/// </summary>
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			if (this.paramTypes != null)
				return this.paramTypes;
			
			insureParsed();
			System.Type[] paramTypes = new System.Type[numArgs];
			
			for (int i = 0; i < numArgs; i++)
			{
				BSHFormalParameter param = (BSHFormalParameter) jjtGetChild(i);
				paramTypes[i] = (System.Type) param.eval(callstack, interpreter);
			}
			
			this.paramTypes = paramTypes;
			
			return paramTypes;
		}
	}
}