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
	
	/// <summary>A specialized namespace	for Blocks (e.g. the body of a "for" statement).
	/// The Block acts like a child namespace but only for typed variables 
	/// declared within it (block local scope) or untyped variables explicitly set 
	/// in it via setBlockVariable().  Otherwise variable assignment 
	/// (including untyped variable usage) acts like it is part of the containing
	/// block.  
	/// <p>
	/// </summary>
	/*
	Note: This class essentially just delegates most of its methods to its
	parent.  The setVariable() indirection is very small.  We could probably
	fold this functionality back into the base NameSpace as a special case.
	But this has changed a few times so I'd like to leave this abstraction for
	now.*/
	[Serializable]
	class BlockNameSpace:NameSpace
	{
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary>This method recurses to find the nearest non-BlockNameSpace parent.
		/// public NameSpace getParent() 
		/// {
		/// NameSpace parent = super.getParent();
		/// if ( parent instanceof BlockNameSpace )
		/// return parent.getParent();
		/// else
		/// return parent;
		/// }
		/// </summary>
		/// <summary>do we need this? </summary>
		private NameSpace NonBlockParent
		{
			get
			{
				NameSpace parent = base.Parent;
				if (parent is BlockNameSpace)
					return ((BlockNameSpace) parent).NonBlockParent;
				else
					return parent;
			}
			
		}
		public BlockNameSpace(NameSpace parent):base(parent, parent.Name + "/BlockNameSpace")
		{
		}
		
		/// <summary>Override the standard namespace behavior to make assignments
		/// happen in our parent (enclosing) namespace, unless the variable has
		/// already been assigned here via a typed declaration or through
		/// the special setBlockVariable() (used for untyped args in try/catch).
		/// <p>
		/// i.e. only allow typed var declaration to happen in this namespace.
		/// Typed vars are handled in the ordinary way local scope.  All untyped
		/// assignments are delegated to the enclosing context.
		/// </summary>
		/*
		Note: it may see like with the new 1.3 scoping this test could be
		removed, but it cannot.  When recurse is false we still need to set the
		variable in our parent, not here.
		*/
		public override void  setVariable(System.String name, System.Object value_Renamed, bool strictJava, bool recurse)
		{
			if (weHaveVar(name))
			// set the var here in the block namespace
				base.setVariable(name, value_Renamed, strictJava, false);
			// set the var in the enclosing (parent) namespace
			else
				Parent.setVariable(name, value_Renamed, strictJava, recurse);
		}
		
		/// <summary>Set an untyped variable in the block namespace.
		/// The BlockNameSpace would normally delegate this set to the parent.
		/// Typed variables are naturally set locally.
		/// This is used in try/catch block argument. 
		/// </summary>
		public virtual void  setBlockVariable(System.String name, System.Object value_Renamed)
		{
			base.setVariable(name, value_Renamed, false, false);
		}
		
		/// <summary>We have the variable: either it was declared here with a type, giving
		/// it block local scope or an untyped var was explicitly set here via
		/// setBlockVariable().
		/// </summary>
		private bool weHaveVar(System.String name)
		{
			// super.variables.containsKey( name ) not any faster, I checked
			try
			{
				return base.getVariableImpl(name, false) != null;
			}
			catch (UtilEvalError e)
			{
				return false;
			}
		}
		
		/// <summary>Get the actual BlockNameSpace 'this' reference.
		/// <p/>
		/// Normally a 'this' reference to a BlockNameSpace (e.g. if () { } )
		/// resolves to the parent namespace (e.g. the namespace containing the
		/// "if" statement).  However when code inside the BlockNameSpace needs to
		/// resolve things relative to 'this' we must use the actual block's 'this'
		/// reference.  Name.java is smart enough to handle this using
		/// getBlockThis().
		/// </summary>
		/// <seealso cref="getThis( Interpreter )">
		/// This getBlockThis( Interpreter declaringInterpreter ) 
		/// {
		/// return super.getThis( declaringInterpreter );
		/// }
		/// </seealso>
		
		//
		// Begin methods which simply delegate to our parent (enclosing scope) 
		//
		
		/// <summary>Get a 'this' reference is our parent's 'this' for the object closure.
		/// e.g. Normally a 'this' reference to a BlockNameSpace (e.g. if () { } )
		/// resolves to the parent namespace (e.g. the namespace containing the
		/// "if" statement). 
		/// </summary>
		/// <seealso cref="getBlockThis( Interpreter )">
		/// </seealso>
		internal override This getThis(Interpreter declaringInterpreter)
		{
			return NonBlockParent.getThis(declaringInterpreter);
		}
		
		/// <summary>super is our parent's super</summary>
		public override This getSuper(Interpreter declaringInterpreter)
		{
			return NonBlockParent.getSuper(declaringInterpreter);
		}
		
		/// <summary>delegate import to our parent</summary>
		public override void  importClass(System.String name)
		{
			Parent.importClass(name);
		}
		
		/// <summary>delegate import to our parent</summary>
		public override void  importPackage(System.String name)
		{
			Parent.importPackage(name);
		}
		
		public override void  setMethod(System.String name, BshMethod method)
		{
			Parent.setMethod(name, method);
		}
		protected BlockNameSpace(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info, context)
		{
		}
		//UPGRADE_NOTE: A parameterless constructor was added for a serializable class to avoid compile errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1268'"
		public BlockNameSpace()
		{
		}
		public override void  GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}