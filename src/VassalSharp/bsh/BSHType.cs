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
	class BSHType:SimpleNode, BshClassManager.Listener
	{
		virtual internal SimpleNode TypeNode
		{
			get
			{
				return (SimpleNode) jjtGetChild(0);
			}
			
		}
		/// <summary>baseType is used during evaluation of full type and retained for the
		/// case where we are an array type.
		/// In the case where we are not an array this will be the same as type.
		/// </summary>
		virtual public System.Type BaseType
		{
			get
			{
				return baseType;
			}
			
		}
		/// <summary>If we are an array type this will be non zero and indicate the 
		/// dimensionality of the array.  e.g. 2 for String[][];
		/// </summary>
		virtual public int ArrayDims
		{
			get
			{
				return arrayDims;
			}
			
		}
		/// <summary>baseType is used during evaluation of full type and retained for the
		/// case where we are an array type.
		/// In the case where we are not an array this will be the same as type.
		/// </summary>
		private System.Type baseType;
		/// <summary>If we are an array type this will be non zero and indicate the 
		/// dimensionality of the array.  e.g. 2 for String[][];
		/// </summary>
		private int arrayDims;
		
		/// <summary>Internal cache of the type.  Cleared on classloader change.</summary>
		private System.Type type;
		
		internal System.String descriptor;
		
		internal BSHType(int id):base(id)
		{
		}
		
		/// <summary>Used by the grammar to indicate dimensions of array types 
		/// during parsing.
		/// </summary>
		public virtual void  addArrayDimension()
		{
			arrayDims++;
		}
		
		/// <summary>Returns a class descriptor for this type.
		/// If the type is an ambiguous name (object type) evaluation is 
		/// attempted through the namespace in order to resolve imports.
		/// If it is not found and the name is non-compound we assume the default
		/// package for the name.
		/// </summary>
		public virtual System.String getTypeDescriptor(CallStack callstack, Interpreter interpreter, System.String defaultPackage)
		{
			// return cached type if available
			if (this.descriptor != null)
				return this.descriptor;
			
			System.String descriptor;
			//  first node will either be PrimitiveType or AmbiguousName
			SimpleNode node = TypeNode;
			if (node is BSHPrimitiveType)
				descriptor = getTypeDescriptor(((BSHPrimitiveType) node).type);
			else
			{
				System.String clasName = ((BSHAmbiguousName) node).text;
				BshClassManager bcm = interpreter.ClassManager;
				// Note: incorrect here - we are using the hack in bsh class
				// manager that allows lookup by base name.  We need to eliminate
				// this limitation by working through imports.  See notes in class
				// manager.
				System.String definingClass = bcm.getClassBeingDefined(clasName);
				
				System.Type clas = null;
				if (definingClass == null)
				{
					try
					{
						clas = ((BSHAmbiguousName) node).toClass(callstack, interpreter);
					}
					catch (EvalError e)
					{
						//throw new InterpreterError("unable to resolve type: "+e);
						// ignore and try default package
						//System.out.println("BSHType: "+node+" class not found");
					}
				}
				else
					clasName = definingClass;
				
				if (clas != null)
				{
					//System.out.println("found clas: "+clas);
					descriptor = getTypeDescriptor(clas);
				}
				else
				{
					if (defaultPackage == null || Name.isCompound(clasName))
						descriptor = "L" + clasName.Replace('.', '/') + ";";
					else
						descriptor = "L" + defaultPackage.Replace('.', '/') + "/" + clasName + ";";
				}
			}
			
			for (int i = 0; i < arrayDims; i++)
				descriptor = "[" + descriptor;
			
			this.descriptor = descriptor;
			//System.out.println("BSHType: returning descriptor: "+descriptor);
			return descriptor;
		}
		
		public virtual System.Type getType(CallStack callstack, Interpreter interpreter)
		{
			// return cached type if available
			if (type != null)
				return type;
			
			//  first node will either be PrimitiveType or AmbiguousName
			SimpleNode node = TypeNode;
			if (node is BSHPrimitiveType)
				baseType = ((BSHPrimitiveType) node).Type;
			else
				baseType = ((BSHAmbiguousName) node).toClass(callstack, interpreter);
			
			if (arrayDims > 0)
			{
				try
				{
					// Get the type by constructing a prototype array with
					// arbitrary (zero) length in each dimension.
					int[] dims = new int[arrayDims]; // int array default zeros
					System.Object obj = System.Array.CreateInstance(baseType, (System.Int32[]) dims);
					type = obj.GetType();
				}
				catch (System.Exception e)
				{
					throw new EvalError("Couldn't construct array type", this, callstack);
				}
			}
			else
				type = baseType;
			
			// hack... sticking to first interpreter that resolves this
			// see comments on type instance variable
			interpreter.ClassManager.addListener(this);
			
			return type;
		}
		
		public virtual void  classLoaderChanged()
		{
			type = null;
			baseType = null;
		}
		
		public static System.String getTypeDescriptor(System.Type clas)
		{
			if (clas == System.Type.GetType("System.Boolean"))
				return "Z";
			if (clas == System.Type.GetType("System.Char"))
				return "C";
			if (clas == System.Type.GetType("System.SByte"))
				return "B";
			if (clas == System.Type.GetType("System.Int16"))
				return "S";
			if (clas == System.Type.GetType("System.Int32"))
				return "I";
			if (clas == System.Type.GetType("System.Int64"))
				return "J";
			if (clas == System.Type.GetType("System.Single"))
				return "F";
			if (clas == System.Type.GetType("System.Double"))
				return "D";
			if (clas == System.Type.GetType("System.Void"))
				return "V";
			// Is getName() ok?  test with 1.1
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.String name = clas.FullName.Replace('.', '/');
			
			if (name.StartsWith("[") || name.EndsWith(";"))
				return name;
			else
				return "L" + name.Replace('.', '/') + ";";
		}
	}
}