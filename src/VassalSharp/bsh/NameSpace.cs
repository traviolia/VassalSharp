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
using System.Runtime.InteropServices;
namespace bsh
{
	
	/// <summary>A namespace	in which methods, variables, and imports (class names) live.  
	/// This is package public because it is used in the implementation of some 
	/// bsh commands.  However for normal use you should be using methods on 
	/// bsh.Interpreter to interact with your scripts.
	/// <p>
	/// A bsh.This object is a thin layer over a NameSpace that associates it with
	/// an Interpreter instance.  Together they comprise a Bsh scripted object 
	/// context.
	/// <p>
	/// Note: I'd really like to use collections here, but we have to keep this
	/// compatible with JDK1.1 
	/// </summary>
	/*
	Thanks to Slava Pestov (of jEdit fame) for import caching enhancements.
	Note: This class has gotten too big.  It should be broken down a bit.*/
	[Serializable]
	public class NameSpace : BshClassManager.Listener, NameSource, System.Runtime.Serialization.ISerializable
	{
		virtual internal System.Type ClassStatic
		{
			set
			{
				this.classStatic = value;
				importStatic(value);
			}
			
		}
		virtual internal System.Object ClassInstance
		{
			get
			{
				if (classInstance != null)
					return classInstance;
				
				if (classStatic != null)
					throw new UtilEvalError("Can't refer to class instance from static context.");
				else
				{
					throw new InterpreterError("Can't resolve class instance 'this' in: " + this);
				}
			}
			
			set
			{
				this.classInstance = value;
				importObject(value);
			}
			
		}
		/// <summary>The name of this namespace.  If the namespace is a method body
		/// namespace then this is the name of the method.  If it's a class or
		/// class instance then it's the name of the class.
		/// </summary>
		virtual public System.String Name
		{
			get
			{
				return this.nsName;
			}
			
			// End constructors
			
			
			set
			{
				this.nsName = value;
			}
			
		}
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary>Set the node associated with the creation of this namespace.
		/// This is used in debugging and to support the getInvocationLine()
		/// and getInvocationText() methods.
		/// </summary>
		virtual internal SimpleNode Node
		{
			get
			{
				if (callerInfoNode != null)
					return callerInfoNode;
				if (parent != null)
					return parent.Node;
				else
					return null;
			}
			
			set
			{
				callerInfoNode = value;
			}
			
		}
		/// <summary>Get the names of variables defined in this namespace.
		/// (This does not show variables in parent namespaces).
		/// </summary>
		virtual public System.String[] VariableNames
		{
			get
			{
				if (variables == null)
					return new System.String[0];
				else
					return enumerationToStringArray(variables.Keys.GetEnumerator());
			}
			
		}
		/// <summary>Get the names of methods declared in this namespace.
		/// (This does not include methods in parent namespaces).
		/// </summary>
		virtual public System.String[] MethodNames
		{
			get
			{
				if (methods == null)
					return new System.String[0];
				else
					return enumerationToStringArray(methods.Keys.GetEnumerator());
			}
			
		}
		/// <summary>Get the methods defined in this namespace.
		/// (This does not show methods in parent namespaces).
		/// Note: This will probably be renamed getDeclaredMethods()
		/// </summary>
		virtual public BshMethod[] Methods
		{
			get
			{
				if (methods == null)
					return new BshMethod[0];
				else
					return flattenMethodCollection(methods.Values.GetEnumerator());
			}
			
		}
		/// <summary>Get the parent namespace.
		/// Note: this isn't quite the same as getSuper().
		/// getSuper() returns 'this' if we are at the root namespace.
		/// </summary>
		virtual public NameSpace Parent
		{
			get
			{
				return parent;
			}
			
			set
			{
				this.parent = value;
				
				// If we are disconnected from root we need to handle the def imports
				if (value == null)
					loadDefaultImports();
			}
			
		}
		virtual public Variable[] DeclaredVariables
		{
			/*
			Get variables declared in this namespace.
			*/
			
			get
			{
				if (variables == null)
					return new Variable[0];
				Variable[] vars = new Variable[variables.Count];
				int i = 0;
				//UPGRADE_TODO: Method 'java.util.Enumeration.hasMoreElements' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationhasMoreElements'"
				for (System.Collections.IEnumerator e = variables.Values.GetEnumerator(); e.MoveNext(); )
				{
					//UPGRADE_TODO: Method 'java.util.Enumeration.nextElement' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationnextElement'"
					vars[i++] = (Variable) e.Current;
				}
				return vars;
			}
			
		}
		/// <summary>Implements NameSource</summary>
		/// <returns> all variable and method names in this and all parent
		/// namespaces
		/// </returns>
		virtual public System.String[] AllNames
		{
			get
			{
				System.Collections.ArrayList vec = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
				getAllNamesAux(vec);
				System.String[] names = new System.String[vec.Count];
				vec.CopyTo(names);
				return names;
			}
			
		}
		virtual public int InvocationLine
		{
			get
			{
				SimpleNode node = Node;
				if (node != null)
					return node.LineNumber;
				else
					return - 1;
			}
			
		}
		virtual public System.String InvocationText
		{
			get
			{
				SimpleNode node = Node;
				if (node != null)
					return node.Text;
				else
					return "<invoked from Java code>";
			}
			
		}
		/// <summary>Set the package name for classes defined in this namespace.
		/// Subsequent sets override the package.
		/// </summary>
		virtual internal System.String Package
		{
			get
			{
				if (packageName != null)
					return packageName;
				
				if (parent != null)
					return parent.Package;
				
				return null;
			}
			
			set
			{
				this.packageName = value;
			}
			
		}
		//UPGRADE_NOTE: Final was removed from the declaration of 'JAVACODE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly NameSpace JAVACODE = new NameSpace((BshClassManager) null, "Called from compiled Java code.");
		
		// Begin instance data
		// Note: if we add something here we should reset it in the clear() method.
		
		/// <summary>The name of this namespace.  If the namespace is a method body
		/// namespace then this is the name of the method.  If it's a class or
		/// class instance then it's the name of the class.
		/// </summary>
		private System.String nsName;
		private NameSpace parent;
		private System.Collections.Hashtable variables;
		private System.Collections.Hashtable methods;
		
		protected internal System.Collections.Hashtable importedClasses;
		private System.Collections.ArrayList importedPackages;
		private System.Collections.ArrayList importedCommands;
		private System.Collections.ArrayList importedObjects;
		private System.Collections.ArrayList importedStatic;
		private System.String packageName;
		
		[NonSerialized]
		private BshClassManager classManager;
		
		// See notes in getThis()
		private This thisReference;
		
		/// <summary>Name resolver objects </summary>
		private System.Collections.Hashtable names;
		
		/// <summary>The node associated with the creation of this namespace.
		/// This is used support getInvocationLine() and getInvocationText(). 
		/// </summary>
		internal SimpleNode callerInfoNode;
		
		/// <summary>Note that the namespace is a method body namespace.  This is used for
		/// printing stack traces in exceptions.  
		/// </summary>
		internal bool isMethod;
		/// <summary>Note that the namespace is a class body or class instance namespace.  
		/// This is used for controlling static/object import precedence, etc.
		/// </summary>
		/*
		Note: We will ll move this behavior out to a subclass of 
		NameSpace, but we'll start here.
		*/
		internal bool isClass;
		internal System.Type classStatic;
		internal System.Object classInstance;
		
		
		/// <summary>Local class cache for classes resolved through this namespace using 
		/// getClass() (taking into account imports).  Only unqualified class names
		/// are cached here (those which might be imported).  Qualified names are 
		/// always absolute and are cached by BshClassManager.
		/// </summary>
		[NonSerialized]
		private System.Collections.Hashtable classCache;
		
		// End instance data
		
		// Begin constructors
		
		/// <parent>  the parent namespace of this namespace.  Child namespaces </parent>
		/// <summary>inherit all variables and methods of their parent and can (of course)
		/// override / shadow them.
		/// </summary>
		public NameSpace(NameSpace parent, System.String name):this(parent, null, name)
		{
		}
		
		public NameSpace(BshClassManager classManager, System.String name):this(null, classManager, name)
		{
		}
		
		public NameSpace(NameSpace parent, BshClassManager classManager, System.String name)
		{
			// We might want to do this here rather than explicitly in Interpreter
			// for global (see also prune())
			//if ( classManager == null && (parent == null ) )
			// create our own class manager?
			
			Name = name;
			Parent = parent;
			setClassManager(classManager);
			
			// Register for notification of classloader change
			if (classManager != null)
				classManager.addListener(this);
		}
		
		/// <summary>Resolve name to an object through this namespace.</summary>
		public virtual System.Object get_Renamed(System.String name, Interpreter interpreter)
		{
			CallStack callstack = new CallStack(this);
			return getNameResolver(name).toObject(callstack, interpreter);
		}
		
		/// <summary>Set the variable through this namespace.
		/// This method obeys the LOCALSCOPING property to determine how variables
		/// are set.
		/// <p>
		/// Note: this method is primarily intended for use internally.  If you use
		/// this method outside of the bsh package and wish to set variables with
		/// primitive values you will have to wrap them using bsh.Primitive.
		/// </summary>
		/// <seealso cref="bsh.Primitive">
		/// <p>
		/// Setting a new variable (which didn't exist before) or removing
		/// a variable causes a namespace change.
		/// </seealso>
		/// <param name="strictJava">specifies whether strict java rules are applied.
		/// </param>
		public virtual void  setVariable(System.String name, System.Object value_Renamed, bool strictJava)
		{
			// if localscoping switch follow strictJava, else recurse
			bool recurse = Interpreter.LOCALSCOPING?strictJava:true;
			setVariable(name, value_Renamed, strictJava, recurse);
		}
		
		/// <summary>Set a variable explicitly in the local scope.</summary>
		internal virtual void  setLocalVariable(System.String name, System.Object value_Renamed, bool strictJava)
		{
			setVariable(name, value_Renamed, strictJava, false);
		}
		
		/// <summary>Set the value of a the variable 'name' through this namespace.
		/// The variable may be an existing or non-existing variable.
		/// It may live in this namespace or in a parent namespace if recurse is 
		/// true.
		/// <p>
		/// Note: This method is not public and does *not* know about LOCALSCOPING.
		/// Its caller methods must set recurse intelligently in all situations 
		/// (perhaps based on LOCALSCOPING).
		/// <p>
		/// Note: this method is primarily intended for use internally.  If you use
		/// this method outside of the bsh package and wish to set variables with
		/// primitive values you will have to wrap them using bsh.Primitive.
		/// </summary>
		/// <seealso cref="bsh.Primitive">
		/// <p>
		/// Setting a new variable (which didn't exist before) or removing
		/// a variable causes a namespace change.
		/// </seealso>
		/// <param name="strictJava">specifies whether strict java rules are applied.
		/// </param>
		/// <param name="recurse">determines whether we will search for the variable in
		/// our parent's scope before assigning locally.
		/// </param>
		//UPGRADE_NOTE: Access modifiers of method 'setVariable' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public virtual void  setVariable(System.String name, System.Object value_Renamed, bool strictJava, bool recurse)
		{
			if (variables == null)
				variables = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
			
			// primitives should have been wrapped
			if (value_Renamed == null)
				throw new InterpreterError("null variable value");
			
			// Locate the variable definition if it exists.
			Variable existing = getVariableImpl(name, recurse);
			
			// Found an existing variable here (or above if recurse allowed)
			if (existing != null)
			{
				try
				{
					existing.setValue(value_Renamed, Variable.ASSIGNMENT);
				}
				catch (UtilEvalError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new UtilEvalError("Variable assignment: " + name + ": " + e.Message);
				}
			}
			// No previous variable definition found here (or above if recurse)
			else
			{
				if (strictJava)
					throw new UtilEvalError("(Strict Java mode) Assignment to undeclared variable: " + name);
				
				// If recurse, set global untyped var, else set it here.	
				//NameSpace varScope = recurse ? getGlobal() : this;
				// This modification makes default allocation local
				NameSpace varScope = this;
				
				varScope.variables[name] = new Variable(name, value_Renamed, null);
				
				// nameSpaceChanged() on new variable addition
				nameSpaceChanged();
			}
		}
		
		/// <summary>Remove the variable from the namespace.</summary>
		public virtual void  unsetVariable(System.String name)
		{
			if (variables != null)
			{
				variables.Remove(name);
				nameSpaceChanged();
			}
		}
		
		private System.String[] enumerationToStringArray(System.Collections.IEnumerator e)
		{
			System.Collections.ArrayList v = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			//UPGRADE_TODO: Method 'java.util.Enumeration.hasMoreElements' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationhasMoreElements'"
			while (e.MoveNext())
			{
				//UPGRADE_TODO: Method 'java.util.Enumeration.nextElement' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationnextElement'"
				v.Add(e.Current);
			}
			System.String[] sa = new System.String[v.Count];
			v.CopyTo(sa);
			return sa;
		}
		
		/// <summary>Flatten the vectors of overloaded methods to a single array.</summary>
		/// <seealso cref="getMethods()">
		/// </seealso>
		private BshMethod[] flattenMethodCollection(System.Collections.IEnumerator e)
		{
			System.Collections.ArrayList v = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			//UPGRADE_TODO: Method 'java.util.Enumeration.hasMoreElements' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationhasMoreElements'"
			while (e.MoveNext())
			{
				//UPGRADE_TODO: Method 'java.util.Enumeration.nextElement' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationnextElement'"
				System.Object o = e.Current;
				if (o is BshMethod)
					v.Add(o);
				else
				{
					System.Collections.ArrayList ov = (System.Collections.ArrayList) o;
					for (int i = 0; i < ov.Count; i++)
						v.Add(ov[i]);
				}
			}
			BshMethod[] bma = new BshMethod[v.Count];
			v.CopyTo(bma);
			return bma;
		}
		
		/// <summary>Get the parent namespace' This reference or this namespace' This
		/// reference if we are the top.
		/// </summary>
		public virtual This getSuper(Interpreter declaringInterpreter)
		{
			if (parent != null)
				return parent.getThis(declaringInterpreter);
			else
				return getThis(declaringInterpreter);
		}
		
		/// <summary>Get the top level namespace or this namespace if we are the top.
		/// Note: this method should probably return type bsh.This to be consistent
		/// with getThis();
		/// </summary>
		public virtual This getGlobal(Interpreter declaringInterpreter)
		{
			if (parent != null)
				return parent.getGlobal(declaringInterpreter);
			else
				return getThis(declaringInterpreter);
		}
		
		
		/// <summary>A This object is a thin layer over a namespace, comprising a bsh object
		/// context.  It handles things like the interface types the bsh object
		/// supports and aspects of method invocation on it.  
		/// <p>
		/// The declaringInterpreter is here to support callbacks from Java through
		/// generated proxies.  The scripted object "remembers" who created it for
		/// things like printing messages and other per-interpreter phenomenon
		/// when called externally from Java.
		/// </summary>
		/*
		Note: we need a singleton here so that things like 'this == this' work
		(and probably a good idea for speed).
		
		Caching a single instance here seems technically incorrect,
		considering the declaringInterpreter could be different under some
		circumstances.  (Case: a child interpreter running a source() / eval() 
		command ).  However the effect is just that the main interpreter that
		executes your script should be the one involved in call-backs from Java.
		
		I do not know if there are corner cases where a child interpreter would
		be the first to use a This reference in a namespace or if that would
		even cause any problems if it did...  We could do some experiments
		to find out... and if necessary we could cache on a per interpreter
		basis if we had weak references...  We might also look at skipping 
		over child interpreters and going to the parent for the declaring 
		interpreter, so we'd be sure to get the top interpreter.
		*/
		internal virtual This getThis(Interpreter declaringInterpreter)
		{
			if (thisReference == null)
				thisReference = This.getThis(this, declaringInterpreter);
			
			return thisReference;
		}
		
		public virtual BshClassManager getClassManager()
		{
			if (classManager != null)
				return classManager;
			if (parent != null && parent != JAVACODE)
				return parent.getClassManager();
			
			System.Console.Out.WriteLine("experiment: creating class manager");
			classManager = BshClassManager.createClassManager(null);
			
			//Interpreter.debug("No class manager namespace:" +this);
			return classManager;
		}
		
		internal virtual void  setClassManager(BshClassManager classManager)
		{
			this.classManager = classManager;
		}
		
		/// <summary>Used for serialization</summary>
		public virtual void  prune()
		{
			// Cut off from parent, we must have our own class manager.
			// Can't do this in the run() command (needs to resolve stuff)
			// Should we do it by default when we create a namespace will no
			// parent of class manager?
			
			if (this.classManager == null)
			// XXX if we keep the createClassManager in getClassManager then we can axe
			// this?
				setClassManager(BshClassManager.createClassManager(null));
			
			Parent = null;
		}
		
		/// <summary>Get the specified variable in this namespace or a parent namespace.
		/// <p>
		/// Note: this method is primarily intended for use internally.  If you use
		/// this method outside of the bsh package you will have to use 
		/// Primitive.unwrap() to get primitive values.
		/// </summary>
		/// <seealso cref="Primitive.unwrap( Object )">
		/// </seealso>
		/// <returns> The variable value or Primitive.VOID if it is not defined.
		/// </returns>
		public virtual System.Object getVariable(System.String name)
		{
			return getVariable(name, true);
		}
		
		/// <summary>Get the specified variable in this namespace.</summary>
		/// <param name="recurse">If recurse is true then we recursively search through 
		/// parent namespaces for the variable.
		/// <p>
		/// Note: this method is primarily intended for use internally.  If you use
		/// this method outside of the bsh package you will have to use 
		/// Primitive.unwrap() to get primitive values.
		/// </param>
		/// <seealso cref="Primitive.unwrap( Object )">
		/// </seealso>
		/// <returns> The variable value or Primitive.VOID if it is not defined.
		/// </returns>
		public virtual System.Object getVariable(System.String name, bool recurse)
		{
			Variable var = getVariableImpl(name, recurse);
			return unwrapVariable(var);
		}
		
		/// <summary>Locate a variable and return the Variable object with optional 
		/// recursion through parent name spaces.
		/// <p/>
		/// If this namespace is static, return only static variables.
		/// </summary>
		/// <returns> the Variable value or null if it is not defined
		/// </returns>
		protected internal virtual Variable getVariableImpl(System.String name, bool recurse)
		{
			Variable var = null;
			
			// Change import precedence if we are a class body/instance
			// Get imported first.
			if (var == null && isClass)
				var = getImportedVar(name);
			
			if (var == null && variables != null)
				var = (Variable) variables[name];
			
			// Change import precedence if we are a class body/instance
			if (var == null && !isClass)
				var = getImportedVar(name);
			
			// try parent
			if (recurse && (var == null) && (parent != null))
				var = parent.getVariableImpl(name, recurse);
			
			return var;
		}
		
		/// <summary>Unwrap a variable to its value.</summary>
		/// <returns> return the variable value.  A null var is mapped to 
		/// Primitive.VOID
		/// </returns>
		protected internal virtual System.Object unwrapVariable(Variable var)
		{
			return (var == null)?Primitive.VOID:var.getValue();
		}
		
		/// <deprecated> See #setTypedVariable( String, Class, Object, Modifiers )
		/// </deprecated>
		public virtual void  setTypedVariable(System.String name, System.Type type, System.Object value_Renamed, bool isFinal)
		{
			Modifiers modifiers = new Modifiers();
			if (isFinal)
				modifiers.addModifier(Modifiers.FIELD, "final");
			setTypedVariable(name, type, value_Renamed, modifiers);
		}
		
		/// <summary>Declare a variable in the local scope and set its initial value.
		/// Value may be null to indicate that we would like the default value 
		/// for the variable type. (e.g.  0 for integer types, null for object 
		/// types).  An existing typed variable may only be set to the same type.
		/// If an untyped variable of the same name exists it will be overridden 
		/// with the new typed var.
		/// The set will perform a Types.getAssignableForm() on the value if 
		/// necessary.
		/// <p>
		/// Note: this method is primarily intended for use internally.  If you use
		/// this method outside of the bsh package and wish to set variables with
		/// primitive values you will have to wrap them using bsh.Primitive.
		/// </summary>
		/// <seealso cref="bsh.Primitive">
		/// </seealso>
		/// <param name="value">If value is null, you'll get the default value for the type
		/// </param>
		/// <param name="modifiers">may be null
		/// </param>
		public virtual void  setTypedVariable(System.String name, System.Type type, System.Object value_Renamed, Modifiers modifiers)
		{
			//checkVariableModifiers( name, modifiers );
			
			if (variables == null)
				variables = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
			
			// Setting a typed variable is always a local operation.
			Variable existing = getVariableImpl(name, false);
			
			
			// Null value is just a declaration
			// Note: we might want to keep any existing value here instead of reset
			/*
			// Moved to Variable
			if ( value == null )
			value = Primitive.getDefaultValue( type );
			*/
			
			// does the variable already exist?
			if (existing != null)
			{
				// Is it typed?
				if (existing.Type != null)
				{
					// If it had a different type throw error.
					// This allows declaring the same var again, but not with
					// a different (even if assignable) type.
					if (existing.Type != type)
					{
						throw new UtilEvalError("Typed variable: " + name + " was previously declared with type: " + existing.Type);
					}
					else
					{
						// else set it and return
						existing.setValue(value_Renamed, Variable.DECLARATION);
						return ;
					}
				}
				// Careful here:
				// else fall through to override and install the new typed version
			}
			
			// Add the new typed var
			variables[name] = new Variable(name, type, value_Renamed, modifiers);
		}
		
		/// <summary>Dissallow static vars outside of a class</summary>
		/// <param name="name">is here just to allow the error message to use it
		/// protected void checkVariableModifiers( String name, Modifiers modifiers )
		/// throws UtilEvalError
		/// {
		/// if ( modifiers!=null && modifiers.hasModifier("static") )
		/// throw new UtilEvalError(
		/// "Can't declare static variable outside of class: "+name );
		/// }
		/// </param>
		
		/// <summary>Note: this is primarily for internal use.</summary>
		/// <seealso cref="Interpreter.source( String )">
		/// </seealso>
		/// <seealso cref="Interpreter.eval( String )">
		/// </seealso>
		public virtual void  setMethod(System.String name, BshMethod method)
		{
			//checkMethodModifiers( method );
			
			if (methods == null)
				methods = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
			
			System.Object m = methods[name];
			
			if (m == null)
				methods[name] = method;
			else if (m is BshMethod)
			{
				System.Collections.ArrayList v = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
				v.Add(m);
				v.Add(method);
				methods[name] = v;
			}
			// Vector
			else
				((System.Collections.ArrayList) m).Add(method);
		}
		
		/// <seealso cref="getMethod( String, Class [], boolean )">
		/// </seealso>
		/// <seealso cref="getMethod( String, Class [] )">
		/// </seealso>
		public virtual BshMethod getMethod(System.String name, System.Type[] sig)
		{
			return getMethod(name, sig, false);
		}
		
		/// <summary>Get the bsh method matching the specified signature declared in 
		/// this name space or a parent.
		/// <p>
		/// Note: this method is primarily intended for use internally.  If you use
		/// this method outside of the bsh package you will have to be familiar
		/// with BeanShell's use of the Primitive wrapper class.
		/// </summary>
		/// <seealso cref="bsh.Primitive">
		/// </seealso>
		/// <returns> the BshMethod or null if not found
		/// </returns>
		/// <param name="declaredOnly">if true then only methods declared directly in this
		/// namespace will be found and no inherited or imported methods will
		/// be visible.
		/// </param>
		public virtual BshMethod getMethod(System.String name, System.Type[] sig, bool declaredOnly)
		{
			BshMethod method = null;
			
			// Change import precedence if we are a class body/instance
			// Get import first.
			if (method == null && isClass && !declaredOnly)
				method = getImportedMethod(name, sig);
			
			System.Object m = null;
			if (method == null && methods != null)
			{
				m = methods[name];
				
				// m contains either BshMethod or Vector of BshMethod
				if (m != null)
				{
					// unwrap 
					BshMethod[] ma;
					if (m is System.Collections.ArrayList)
					{
						System.Collections.ArrayList vm = (System.Collections.ArrayList) m;
						ma = new BshMethod[vm.Count];
						vm.CopyTo(ma);
					}
					else
						ma = new BshMethod[]{(BshMethod) m};
					
					// Apply most specific signature matching
					System.Type[][] candidates = new System.Type[ma.Length][];
					for (int i = 0; i < ma.Length; i++)
						candidates[i] = ma[i].ParameterTypes;
					
					int match = Reflect.findMostSpecificSignature(sig, candidates);
					if (match != - 1)
						method = ma[match];
				}
			}
			
			if (method == null && !isClass && !declaredOnly)
				method = getImportedMethod(name, sig);
			
			// try parent
			if (!declaredOnly && (method == null) && (parent != null))
				return parent.getMethod(name, sig);
			
			return method;
		}
		
		/// <summary>Import a class name.
		/// Subsequent imports override earlier ones
		/// </summary>
		public virtual void  importClass(System.String name)
		{
			if (importedClasses == null)
				importedClasses = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
			
			importedClasses[Name.suffix(name, 1)] = name;
			nameSpaceChanged();
		}
		
		/// <summary>subsequent imports override earlier ones</summary>
		public virtual void  importPackage(System.String name)
		{
			if (importedPackages == null)
				importedPackages = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			
			// If it exists, remove it and add it at the end (avoid memory leak)
			if (importedPackages.Contains(name))
				importedPackages.Remove(name);
			
			importedPackages.Add(name);
			nameSpaceChanged();
		}
		
		/// <summary>Import scripted or compiled BeanShell commands in the following package
		/// in the classpath.  You may use either "/" path or "." package notation.
		/// e.g. importCommands("/bsh/commands") or importCommands("bsh.commands")
		/// are equivalent.  If a relative path style specifier is used then it is
		/// made into an absolute path by prepending "/".
		/// </summary>
		public virtual void  importCommands(System.String name)
		{
			if (importedCommands == null)
				importedCommands = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			
			// dots to slashes
			name = name.Replace('.', '/');
			// absolute
			if (!name.StartsWith("/"))
				name = "/" + name;
			// remove trailing (but preserve case of simple "/")
			if (name.Length > 1 && name.EndsWith("/"))
				name = name.Substring(0, (name.Length - 1) - (0));
			
			// If it exists, remove it and add it at the end (avoid memory leak)
			if (importedCommands.Contains(name))
				importedCommands.Remove(name);
			
			importedCommands.Add(name);
			nameSpaceChanged();
		}
		
		/// <summary>A command is a scripted method or compiled command class implementing a 
		/// specified method signature.  Commands are loaded from the classpath
		/// and may be imported using the importCommands() method.
		/// <p/>
		/// This method searches the imported commands packages for a script or
		/// command object corresponding to the name of the method.  If it is a
		/// script the script is sourced into this namespace and the BshMethod for
		/// the requested signature is returned.  If it is a compiled class the
		/// class is returned.  (Compiled command classes implement static invoke()
		/// methods).
		/// <p/>
		/// The imported packages are searched in reverse order, so that later
		/// imports take priority.
		/// Currently only the first object (script or class) with the appropriate
		/// name is checked.  If another, overloaded form, is located in another
		/// package it will not currently be found.  This could be fixed.
		/// <p/>
		/// </summary>
		/// <returns> a BshMethod, Class, or null if no such command is found.
		/// </returns>
		/// <param name="name">is the name of the desired command method
		/// </param>
		/// <param name="argTypes">is the signature of the desired command method.
		/// </param>
		/// <throws>  UtilEvalError if loadScriptedCommand throws UtilEvalError </throws>
		/// <summary>i.e. on errors loading a script that was found
		/// </summary>
		public virtual System.Object getCommand(System.String name, System.Type[] argTypes, Interpreter interpreter)
		{
			if (Interpreter.DEBUG)
				Interpreter.debug("getCommand: " + name);
			BshClassManager bcm = interpreter.ClassManager;
			
			if (importedCommands != null)
			{
				// loop backwards for precedence
				for (int i = importedCommands.Count - 1; i >= 0; i--)
				{
					System.String path = (System.String) importedCommands[i];
					
					System.String scriptPath;
					if (path.Equals("/"))
						scriptPath = path + name + ".bsh";
					else
						scriptPath = path + "/" + name + ".bsh";
					
					Interpreter.debug("searching for script: " + scriptPath);
					
					System.IO.Stream in_Renamed = bcm.getResourceAsStream(scriptPath);
					
					if (in_Renamed != null)
						return loadScriptedCommand(in_Renamed, name, argTypes, scriptPath, interpreter);
					
					// Chop leading "/" and change "/" to "."
					System.String className;
					if (path.Equals("/"))
						className = name;
					else
						className = path.Substring(1).Replace('/', '.') + "." + name;
					
					Interpreter.debug("searching for class: " + className);
					System.Type clas = bcm.classForName(className);
					if (clas != null)
						return clas;
				}
			}
			
			if (parent != null)
				return parent.getCommand(name, argTypes, interpreter);
			else
				return null;
		}
		
		protected internal virtual BshMethod getImportedMethod(System.String name, System.Type[] sig)
		{
			// Try object imports
			if (importedObjects != null)
				for (int i = 0; i < importedObjects.Count; i++)
				{
					System.Object object_Renamed = importedObjects[i];
					System.Type clas = object_Renamed.GetType();
					System.Reflection.MethodInfo method = Reflect.resolveJavaMethod(getClassManager(), clas, name, sig, false);
					if (method != null)
						return new BshMethod(method, object_Renamed);
				}
			
			// Try static imports
			if (importedStatic != null)
				for (int i = 0; i < importedStatic.Count; i++)
				{
					System.Type clas = (System.Type) importedStatic[i];
					System.Reflection.MethodInfo method = Reflect.resolveJavaMethod(getClassManager(), clas, name, sig, true);
					if (method != null)
						return new BshMethod(method, (System.Object) null);
				}
			
			return null;
		}
		
		protected internal virtual Variable getImportedVar(System.String name)
		{
			// Try object imports
			if (importedObjects != null)
				for (int i = 0; i < importedObjects.Count; i++)
				{
					System.Object object_Renamed = importedObjects[i];
					System.Type clas = object_Renamed.GetType();
					System.Reflection.FieldInfo field = Reflect.resolveJavaField(clas, name, false);
					if (field != null)
						return new Variable(name, field.FieldType, new LHS(object_Renamed, field));
				}
			
			// Try static imports
			if (importedStatic != null)
				for (int i = 0; i < importedStatic.Count; i++)
				{
					System.Type clas = (System.Type) importedStatic[i];
					System.Reflection.FieldInfo field = Reflect.resolveJavaField(clas, name, true);
					if (field != null)
						return new Variable(name, field.FieldType, new LHS(field));
				}
			
			return null;
		}
		
		/// <summary>Load a command script from the input stream and find the BshMethod in
		/// the target namespace.
		/// </summary>
		/// <throws>  UtilEvalError on error in parsing the script or if the the </throws>
		/// <summary>method is not found after parsing the script.
		/// </summary>
		/*
		If we want to support multiple commands in the command path we need to
		change this to not throw the exception.
		*/
		private BshMethod loadScriptedCommand(System.IO.Stream in_Renamed, System.String name, System.Type[] argTypes, System.String resourcePath, Interpreter interpreter)
		{
			try
			{
				interpreter.eval(new System.IO.StreamReader(in_Renamed, System.Text.Encoding.Default), this, resourcePath);
			}
			catch (EvalError e)
			{
				/* 
				Here we catch any EvalError from the interpreter because we are
				using it as a tool to load the command, not as part of the
				execution path.
				*/
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				Interpreter.debug(e.ToString());
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.getMessage' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new UtilEvalError("Error loading script: " + e.Message);
			}
			
			// Look for the loaded command 
			BshMethod meth = getMethod(name, argTypes);
			/*
			if ( meth == null )
			throw new UtilEvalError("Loaded resource: " + resourcePath +
			"had an error or did not contain the correct method" );
			*/
			
			return meth;
		}
		
		/// <summary>Helper that caches class.</summary>
		internal virtual void  cacheClass(System.String name, System.Type c)
		{
			if (classCache == null)
			{
				classCache = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
				//cacheCount++; // debug
			}
			
			classCache[name] = c;
		}
		
		/// <summary>Load a class through this namespace taking into account imports.
		/// The class search will proceed through the parent namespaces if
		/// necessary.
		/// </summary>
		/// <returns> null if not found.
		/// </returns>
		public virtual System.Type getClass(System.String name)
		{
			System.Type c = getClassImpl(name);
			if (c != null)
				return c;
			// implement the recursion for getClassImpl()
			else if (parent != null)
				return parent.getClass(name);
			else
				return null;
		}
		
		/// <summary>Implementation of getClass() 
		/// Load a class through this namespace taking into account imports.
		/// <p>
		/// Check the cache first.  If an unqualified name look for imported 
		/// class or package.  Else try to load absolute name.
		/// <p>
		/// This method implements caching of unqualified names (normally imports).
		/// Qualified names are cached by the BshClassManager.
		/// Unqualified absolute class names (e.g. unpackaged Foo) are cached too
		/// so that we don't go searching through the imports for them each time.
		/// </summary>
		/// <returns> null if not found.
		/// </returns>
		private System.Type getClassImpl(System.String name)
		{
			System.Type c = null;
			
			// Check the cache
			if (classCache != null)
			{
				c = (System.Type) classCache[name];
				
				if (c != null)
					return c;
			}
			
			// Unqualified (simple, non-compound) name
			bool unqualifiedName = !Name.isCompound(name);
			
			// Unqualified name check imported
			if (unqualifiedName)
			{
				// Try imported class
				if (c == null)
					c = getImportedClassImpl(name);
				
				// if found as imported also cache it
				if (c != null)
				{
					cacheClass(name, c);
					return c;
				}
			}
			
			// Try absolute
			c = classForName(name);
			if (c != null)
			{
				// Cache unqualified names to prevent import check again
				if (unqualifiedName)
					cacheClass(name, c);
				return c;
			}
			
			// Not found
			if (Interpreter.DEBUG)
			{
				Interpreter.debug("getClass(): " + name + " not	found in " + this);
			}
			return null;
		}
		
		/// <summary>Try to make the name into an imported class.
		/// This method takes into account only imports (class or package)
		/// found directly in this NameSpace (no parent chain).
		/// </summary>
		private System.Type getImportedClassImpl(System.String name)
		{
			// Try explicitly imported class, e.g. import foo.Bar;
			System.String fullname = null;
			if (importedClasses != null)
				fullname = ((System.String) importedClasses[name]);
			
			// not sure if we should really recurse here for explicitly imported
			// class in parent...  
			
			if (fullname != null)
			{
				/*
				Found the full name in imported classes.
				*/
				// Try to make the full imported name
				System.Type clas = classForName(fullname);
				
				// Handle imported inner class case
				if (clas == null)
				{
					// Imported full name wasn't found as an absolute class
					// If it is compound, try to resolve to an inner class.  
					// (maybe this should happen in the BshClassManager?)
					
					if (Name.isCompound(fullname))
						try
						{
							clas = getNameResolver(fullname).toClass();
						}
						//UPGRADE_NOTE: Exception 'java.lang.ClassNotFoundException' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
						catch (System.Exception e)
						{
							/* not a class */
						}
					else if (Interpreter.DEBUG)
						Interpreter.debug("imported unpackaged name not found:" + fullname);
					
					// If found cache the full name in the BshClassManager
					if (clas != null)
					{
						// (should we cache info in not a class case too?)
						getClassManager().cacheClassInfo(fullname, clas);
						return clas;
					}
				}
				else
					return clas;
				
				// It was explicitly imported, but we don't know what it is.
				// should we throw an error here??
				return null;
			}
			
			/*
			Try imported packages, e.g. "import foo.bar.*;"
			in reverse order of import...
			(give later imports precedence...)
			*/
			if (importedPackages != null)
				for (int i = importedPackages.Count - 1; i >= 0; i--)
				{
					System.String s = ((System.String) importedPackages[i]) + "." + name;
					System.Type c = classForName(s);
					if (c != null)
						return c;
				}
			
			BshClassManager bcm = getClassManager();
			/*
			Try super import if available
			Note: we do this last to allow explicitly imported classes
			and packages to take priority.  This method will also throw an
			error indicating ambiguity if it exists...
			*/
			if (bcm.hasSuperImport())
			{
				System.String s = bcm.getClassNameByUnqName(name);
				if (s != null)
					return classForName(s);
			}
			
			return null;
		}
		
		private System.Type classForName(System.String name)
		{
			return getClassManager().classForName(name);
		}
		
		/// <summary>Helper for implementing NameSource</summary>
		protected internal virtual void  getAllNamesAux(System.Collections.ArrayList vec)
		{
			System.Collections.IEnumerator varNames = variables.Keys.GetEnumerator();
			//UPGRADE_TODO: Method 'java.util.Enumeration.hasMoreElements' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationhasMoreElements'"
			while (varNames.MoveNext())
			{
				//UPGRADE_TODO: Method 'java.util.Enumeration.nextElement' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationnextElement'"
				vec.Add(varNames.Current);
			}
			
			System.Collections.IEnumerator methodNames = methods.Keys.GetEnumerator();
			//UPGRADE_TODO: Method 'java.util.Enumeration.hasMoreElements' was converted to 'System.Collections.IEnumerator.MoveNext' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationhasMoreElements'"
			while (methodNames.MoveNext())
			{
				//UPGRADE_TODO: Method 'java.util.Enumeration.nextElement' was converted to 'System.Collections.IEnumerator.Current' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilEnumerationnextElement'"
				vec.Add(methodNames.Current);
			}
			
			if (parent != null)
				parent.getAllNamesAux(vec);
		}
		
		internal System.Collections.ArrayList nameSourceListeners;
		/// <summary>Implements NameSource
		/// Add a listener who is notified upon changes to names in this space.
		/// </summary>
		public virtual void  addNameSourceListener(bsh.Listener listener)
		{
			if (nameSourceListeners == null)
				nameSourceListeners = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			nameSourceListeners.Add(listener);
		}
		
		/// <summary>Perform "import *;" causing the entire classpath to be mapped.
		/// This can take a while.
		/// </summary>
		public virtual void  doSuperImport()
		{
			getClassManager().doSuperImport();
		}
		
		
		public override System.String ToString()
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return "NameSpace: " + (nsName == null?base.ToString():nsName + " (" + base.ToString() + ")") + (isClass?" (isClass) ":"") + (isMethod?" (method) ":"") + (classStatic != null?" (class static) ":"") + (classInstance != null?" (class instance) ":"");
		}
		
		/*
		For serialization.
		Don't serialize non-serializable objects.
		*/
		//UPGRADE_TODO: Class 'java.io.ObjectOutputStream' was converted to 'System.IO.BinaryWriter' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioObjectOutputStream'"
		//UPGRADE_TODO: Method 'writeObject' was converted to 'GetObjectData' and its parameters were changed. The code must be reviewed in order to assure that no calls to non-member methods of the converted parameters are made. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1192'"
		//UPGRADE_NOTE: Synchronized keyword was removed from method 'writeObject'. Lock expression was added. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1027'"
		public virtual void  GetObjectData(System.Runtime.Serialization.SerializationInfo s, System.Runtime.Serialization.StreamingContext context)
		{
			lock (this)
			{
				// clear name resolvers... don't know if this is necessary.
				names = null;
				
				SupportClass.DefaultWriteObject(s, context, this);
			}
		}
		
		/// <summary>Invoke a method in this namespace with the specified args and
		/// interpreter reference.  No caller information or call stack is
		/// required.  The method will appear as if called externally from Java.
		/// <p>
		/// </summary>
		/// <seealso cref="bsh.This.invokeMethod(">
		/// String methodName, Object [] args, Interpreter interpreter, 
		/// CallStack callstack, SimpleNode callerInfo, boolean )
		/// </seealso>
		public virtual System.Object invokeMethod(System.String methodName, System.Object[] args, Interpreter interpreter)
		{
			return invokeMethod(methodName, args, interpreter, null, null);
		}
		
		/// <summary>This method simply delegates to This.invokeMethod();
		/// <p>
		/// </summary>
		/// <seealso cref="bsh.This.invokeMethod(">
		/// String methodName, Object [] args, Interpreter interpreter, 
		/// CallStack callstack, SimpleNode callerInfo )
		/// </seealso>
		public virtual System.Object invokeMethod(System.String methodName, System.Object[] args, Interpreter interpreter, CallStack callstack, SimpleNode callerInfo)
		{
			return getThis(interpreter).invokeMethod(methodName, args, interpreter, callstack, callerInfo, false);
		}
		
		/// <summary>Clear all cached classes and names</summary>
		public virtual void  classLoaderChanged()
		{
			nameSpaceChanged();
		}
		
		/// <summary>Clear all cached classes and names</summary>
		public virtual void  nameSpaceChanged()
		{
			classCache = null;
			names = null;
		}
		
		/// <summary>Import standard packages.  Currently:
		/// <pre>
		/// importClass("bsh.EvalError");
		/// importClass("bsh.Interpreter");
		/// importPackage("javax.swing.event");
		/// importPackage("javax.swing");
		/// importPackage("java.awt.event");
		/// importPackage("java.awt");
		/// importPackage("java.net");
		/// importPackage("java.util");
		/// importPackage("java.io");
		/// importPackage("java.lang");
		/// importCommands("/bsh/commands");
		/// </pre>
		/// </summary>
		public virtual void  loadDefaultImports()
		{
			/**
			Note: the resolver looks through these in reverse order, per
			precedence rules...  so for max efficiency put the most common
			ones later.
			*/
			importClass("bsh.EvalError");
			importClass("bsh.Interpreter");
			importPackage("javax.swing.event");
			importPackage("javax.swing");
			importPackage("java.awt.event");
			importPackage("java.awt");
			importPackage("java.net");
			importPackage("java.util");
			importPackage("java.io");
			importPackage("java.lang");
			importCommands("/bsh/commands");
		}
		
		/// <summary>This is the factory for Name objects which resolve names within
		/// this namespace (e.g. toObject(), toClass(), toLHS()).
		/// <p>
		/// This was intended to support name resolver caching, allowing 
		/// Name objects to cache info about the resolution of names for 
		/// performance reasons.  However this not proven useful yet.  
		/// <p>
		/// We'll leave the caching as it will at least minimize Name object
		/// creation.
		/// <p>
		/// (This method would be called getName() if it weren't already used for 
		/// the simple name of the NameSpace)
		/// <p>
		/// This method was public for a time, which was a mistake.  
		/// Use get() instead.
		/// </summary>
		internal virtual Name getNameResolver(System.String ambigname)
		{
			if (names == null)
				names = System.Collections.Hashtable.Synchronized(new System.Collections.Hashtable());
			
			Name name = (Name) names[ambigname];
			
			if (name == null)
			{
				name = new Name(this, ambigname);
				names[ambigname] = name;
			}
			
			return name;
		}
		
		/// <summary>This is a helper method for working inside of bsh scripts and commands.
		/// In that context it is impossible to see a ClassIdentifier object
		/// for what it is.  Attempting to access a method on a ClassIdentifier
		/// will look like a static method invocation.  
		/// </summary>
		/// <summary>This method is in NameSpace for convenience (you don't have to import
		/// bsh.ClassIdentifier to use it );
		/// </summary>
		public static System.Type identifierToClass(ClassIdentifier ci)
		{
			return ci.TargetClass;
		}
		
		
		/// <summary>Clear all variables, methods, and imports from this namespace.
		/// If this namespace is the root, it will be reset to the default 
		/// imports.
		/// </summary>
		/// <seealso cref="loadDefaultImports()">
		/// </seealso>
		public virtual void  clear()
		{
			variables = null;
			methods = null;
			importedClasses = null;
			importedPackages = null;
			importedCommands = null;
			importedObjects = null;
			if (parent == null)
				loadDefaultImports();
			classCache = null;
			names = null;
		}
		
		/// <summary>Import a compiled Java object's methods and variables into this 
		/// namespace.  When no scripted method / command or variable is found
		/// locally in this namespace method / fields of the object will be
		/// checked.  Objects are checked in the order of import with later imports
		/// taking precedence.
		/// <p/>
		/// </summary>
		/*
		Note: this impor pattern is becoming common... could factor it out into
		an importedObject Vector class.
		*/
		public virtual void  importObject(System.Object obj)
		{
			if (importedObjects == null)
				importedObjects = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			
			// If it exists, remove it and add it at the end (avoid memory leak)
			if (importedObjects.Contains(obj))
				importedObjects.Remove(obj);
			
			importedObjects.Add(obj);
			nameSpaceChanged();
		}
		
		
		public virtual void  importStatic(System.Type clas)
		{
			if (importedStatic == null)
				importedStatic = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(10));
			
			// If it exists, remove it and add it at the end (avoid memory leak)
			if (importedStatic.Contains(clas))
				importedStatic.Remove(clas);
			
			importedStatic.Add(clas);
			nameSpaceChanged();
		}
		protected NameSpace(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			SupportClass.DefaultReadObject(info, context, this);
		}
		//UPGRADE_NOTE: A parameterless constructor was added for a serializable class to avoid compile errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1268'"
		public NameSpace()
		{
		}
		static NameSpace()
		{
			{
				JAVACODE.isMethod = true;
			}
		}
	}
}