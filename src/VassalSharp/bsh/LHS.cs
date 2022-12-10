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
	
	/// <summary>An LHS is a wrapper for an variable, field, or property.  It ordinarily 
	/// holds the "left hand side" of an assignment and may be either resolved to 
	/// a value or assigned a value.
	/// <p>
	/// </summary>
	/// <summary>There is one special case here termed METHOD_EVAL where the LHS is used
	/// in an intermediate evaluation of a chain of suffixes and wraps a method
	/// invocation.  In this case it may only be resolved to a value and cannot be 
	/// assigned.  (You can't assign a value to the result of a method call e.g.
	/// "foo() = 5;").
	/// <p>
	/// </summary>
	[Serializable]
	class LHS : ParserConstants
	{
		virtual public System.Object Value
		{
			get
			{
				if (type == VARIABLE)
					return nameSpace.getVariable(varName);
				
				if (type == FIELD)
					try
					{
						System.Object o = field.GetValue(object_Renamed);
						return Primitive.wrap(o, field.FieldType);
					}
					catch (System.UnauthorizedAccessException e2)
					{
						throw new UtilEvalError("Can't read field: " + field);
					}
				
				if (type == PROPERTY)
					try
					{
						return Reflect.getObjectProperty(object_Renamed, propName);
					}
					catch (ReflectError e)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						Interpreter.debug(e.Message);
						throw new UtilEvalError("No such property: " + propName);
					}
				
				if (type == INDEX)
					try
					{
						return Reflect.getIndex(object_Renamed, index);
					}
					catch (System.Exception e)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						throw new UtilEvalError("Array access: " + e);
					}
				
				throw new InterpreterError("LHS type");
			}
			
		}
		internal NameSpace nameSpace;
		/// <summary>The assignment should be to a local variable </summary>
		internal bool localVar;
		
		/// <summary>Identifiers for the various types of LHS.</summary>
		internal const int VARIABLE = 0;
		internal const int FIELD = 1;
		internal const int PROPERTY = 2;
		internal const int INDEX = 3;
		internal const int METHOD_EVAL = 4;
		
		internal int type;
		
		internal System.String varName;
		internal System.String propName;
		internal System.Reflection.FieldInfo field;
		internal System.Object object_Renamed;
		internal int index;
		
		/// <summary>Variable LHS constructor.</summary>
		internal LHS(NameSpace nameSpace, System.String varName)
		{
			throw new System.ApplicationException("namespace lhs");
			/*
			type = VARIABLE;
			this.varName = varName;
			this.nameSpace = nameSpace;*/
		}
		
		/// <param name="localVar">if true the variable is set directly in the This
		/// reference's local scope.  If false recursion to look for the variable
		/// definition in parent's scope is allowed. (e.g. the default case for
		/// undefined vars going to global).
		/// </param>
		internal LHS(NameSpace nameSpace, System.String varName, bool localVar)
		{
			type = VARIABLE;
			this.localVar = localVar;
			this.varName = varName;
			this.nameSpace = nameSpace;
		}
		
		/// <summary>Static field LHS Constructor.
		/// This simply calls Object field constructor with null object.
		/// </summary>
		internal LHS(System.Reflection.FieldInfo field)
		{
			type = FIELD;
			this.object_Renamed = null;
			this.field = field;
		}
		
		/// <summary>Object field LHS Constructor.</summary>
		internal LHS(System.Object object_Renamed, System.Reflection.FieldInfo field)
		{
			if (object_Renamed == null)
				throw new System.NullReferenceException("constructed empty LHS");
			
			type = FIELD;
			this.object_Renamed = object_Renamed;
			this.field = field;
		}
		
		/// <summary>Object property LHS Constructor.</summary>
		internal LHS(System.Object object_Renamed, System.String propName)
		{
			if (object_Renamed == null)
				throw new System.NullReferenceException("constructed empty LHS");
			
			type = PROPERTY;
			this.object_Renamed = object_Renamed;
			this.propName = propName;
		}
		
		/// <summary>Array index LHS Constructor.</summary>
		internal LHS(System.Object array, int index)
		{
			if (array == null)
				throw new System.NullReferenceException("constructed empty LHS");
			
			type = INDEX;
			this.object_Renamed = array;
			this.index = index;
		}
		
		/// <summary>Assign a value to the LHS.</summary>
		public virtual System.Object assign(System.Object val, bool strictJava)
		{
			if (this.type == VARIABLE)
			{
				// Set the variable in namespace according to localVar flag
				if (localVar)
					nameSpace.setLocalVariable(varName, val, strictJava);
				else
					nameSpace.setVariable(varName, val, strictJava);
			}
			else if (this.type == FIELD)
			{
				try
				{
					System.Object fieldVal = val is Primitive?((Primitive) val).Value:val;
					
					// This should probably be in Reflect.java
					ReflectManager.RMSetAccessible(field);
					field.SetValue(object_Renamed, fieldVal);
					return val;
				}
				catch (System.NullReferenceException e)
				{
					throw new UtilEvalError("LHS (" + field.Name + ") not a static field.");
				}
				catch (System.UnauthorizedAccessException e2)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new UtilEvalError("LHS (" + field.Name + ") can't access field: " + e2);
				}
				catch (System.ArgumentException e3)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.String type = val is Primitive?((Primitive) val).Type.FullName:val.GetType().FullName;
					throw new UtilEvalError("Argument type mismatch. " + (val == null?"null":type) + " not assignable to field " + field.Name);
				}
			}
			else if (type == PROPERTY)
			{
				/*
				if ( object instanceof Hashtable )
				((Hashtable)object).put(propName, val);
				*/
				CollectionManager cm = CollectionManager.getCollectionManager();
				if (cm.isMap(object_Renamed))
					cm.putInMap(object_Renamed, propName, val);
				else
					try
					{
						Reflect.setObjectProperty(object_Renamed, propName, val);
					}
					catch (ReflectError e)
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						Interpreter.debug("Assignment: " + e.Message);
						throw new UtilEvalError("No such property: " + propName);
					}
			}
			else if (type == INDEX)
				try
				{
					Reflect.setIndex(object_Renamed, index, val);
				}
				catch (UtilTargetError e1)
				{
					// pass along target error
					throw e1;
				}
				catch (System.Exception e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new UtilEvalError("Assignment: " + e.Message);
				}
			else
				throw new InterpreterError("unknown lhs");
			
			return val;
		}
		
		public override System.String ToString()
		{
			return "LHS: " + ((field != null)?"field = " + field.ToString():"") + (varName != null?" varName = " + varName:"") + (nameSpace != null?" nameSpace = " + nameSpace.ToString():"");
		}
	}
}