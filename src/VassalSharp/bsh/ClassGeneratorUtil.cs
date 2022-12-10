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
using bsh.org.objectweb.asm;
using Type = bsh.org.objectweb.asm.Type;
namespace bsh
{
	
	/// <summary>ClassGeneratorUtil utilizes the ASM (www.objectweb.org) bytecode generator 
	/// by Eric Bruneton in order to generate class "stubs" for BeanShell at
	/// runtime.  
	/// <p>
	/// Stub classes contain all of the fields of a BeanShell scripted class
	/// as well as two "callback" references to BeanShell namespaces: one for
	/// static methods and one for instance methods.  Methods of the class are
	/// delegators which invoke corresponding methods on either the static or
	/// instance bsh object and then unpack and return the results.  The static
	/// namespace utilizes a static import to delegate variable access to the
	/// class' static fields.  The instance namespace utilizes a dynamic import
	/// (i.e. mixin) to delegate variable access to the class' instance variables.
	/// <p>
	/// Constructors for the class delegate to the static initInstance() method of 
	/// ClassGeneratorUtil to initialize new instances of the object.  initInstance()
	/// invokes the instance intializer code (init vars and instance blocks) and
	/// then delegates to the corresponding scripted constructor method in the
	/// instance namespace.  Constructors contain special switch logic which allows
	/// the BeanShell to control the calling of alternate constructors (this() or
	/// super() references) at runtime.
	/// <p>
	/// Specially named superclass delegator methods are also generated in order to
	/// allow BeanShell to access overridden methods of the superclass (which
	/// reflection does not normally allow).
	/// <p>
	/// </summary>
	/// <author>  Pat Niemeyer
	/// </author>
	/*
	Notes:
	It would not be hard to eliminate the use of org.objectweb.asm.Type from
	this class, making the distribution a tiny bit smaller.*/
	public class ClassGeneratorUtil : Constants
	{
		/// <summary>The name of the static field holding the reference to the bsh
		/// static This (the callback namespace for static methods) 
		/// </summary>
		internal const System.String BSHSTATIC = "_bshStatic";
		
		/// <summary>The name of the instance field holding the reference to the bsh
		/// instance This (the callback namespace for instance methods) 
		/// </summary>
		internal const System.String BSHTHIS = "_bshThis";
		
		/// <summary>The prefix for the name of the super delegate methods. e.g.
		/// _bshSuperfoo() is equivalent to super.foo() 
		/// </summary>
		internal const System.String BSHSUPER = "_bshSuper";
		
		/// <summary>The bsh static namespace variable name of the instance initializer </summary>
		internal const System.String BSHINIT = "_bshInstanceInitializer";
		
		/// <summary>The bsh static namespace variable that holds the constructor methods </summary>
		internal const System.String BSHCONSTRUCTORS = "_bshConstructors";
		
		/// <summary>The switch branch number for the default constructor. 
		/// The value -1 will cause the default branch to be taken. 
		/// </summary>
		internal const int DEFAULTCONSTRUCTOR = - 1;
		
		internal const System.String OBJECT = "Ljava/lang/Object;";
		
		internal System.String className;
		/// <summary>fully qualified class name (with package) e.g. foo/bar/Blah </summary>
		internal System.String fqClassName;
		internal System.Type superClass;
		internal System.String superClassName;
		internal System.Type[] interfaces;
		internal Variable[] vars;
		internal System.Reflection.ConstructorInfo[] superConstructors;
		internal DelayedEvalBshMethod[] constructors;
		internal DelayedEvalBshMethod[] methods;
		internal NameSpace classStaticNameSpace;
		internal Modifiers classModifiers;
		internal bool isInterface;
		
		/// <param name="packageName">e.g. "com.foo.bar"
		/// </param>
		public ClassGeneratorUtil(Modifiers classModifiers, System.String className, System.String packageName, System.Type superClass, System.Type[] interfaces, Variable[] vars, DelayedEvalBshMethod[] bshmethods, NameSpace classStaticNameSpace, bool isInterface)
		{
			this.classModifiers = classModifiers;
			this.className = className;
			if (packageName != null)
				this.fqClassName = packageName.Replace('.', '/') + "/" + className;
			else
				this.fqClassName = className;
			if (superClass == null)
				superClass = typeof(System.Object);
			this.superClass = superClass;
			this.superClassName = Type.getInternalName(superClass);
			if (interfaces == null)
				interfaces = new System.Type[0];
			this.interfaces = interfaces;
			this.vars = vars;
			this.classStaticNameSpace = classStaticNameSpace;
			this.superConstructors = superClass.GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly);
			
			// Split the methods into constructors and regular method lists
			System.Collections.IList consl = new System.Collections.ArrayList();
			System.Collections.IList methodsl = new System.Collections.ArrayList();
			System.String classBaseName = getBaseName(className); // for inner classes
			for (int i = 0; i < bshmethods.Length; i++)
				if (bshmethods[i].Name.Equals(classBaseName))
					consl.Add(bshmethods[i]);
				else
					methodsl.Add(bshmethods[i]);
			
			this.constructors = (DelayedEvalBshMethod[]) SupportClass.ICollectionSupport.ToArray(consl, new DelayedEvalBshMethod[0]);
			this.methods = (DelayedEvalBshMethod[]) SupportClass.ICollectionSupport.ToArray(methodsl, new DelayedEvalBshMethod[0]);
			
			try
			{
				classStaticNameSpace.setLocalVariable(BSHCONSTRUCTORS, constructors, false);
			}
			catch (UtilEvalError e)
			{
				throw new InterpreterError("can't set cons var");
			}
			
			this.isInterface = isInterface;
		}
		
		/// <summary>Generate the class bytecode for this class.</summary>
		public virtual sbyte[] generateClass()
		{
			// Force the class public for now...
			int classMods = getASMModifiers(classModifiers) | bsh.org.objectweb.asm.Constants_Fields.ACC_PUBLIC;
			if (isInterface)
				classMods |= bsh.org.objectweb.asm.Constants_Fields.ACC_INTERFACE;
			
			System.String[] interfaceNames = new System.String[interfaces.Length];
			for (int i = 0; i < interfaces.Length; i++)
				interfaceNames[i] = Type.getInternalName(interfaces[i]);
			
			System.String sourceFile = "BeanShell Generated via ASM (www.objectweb.org)";
			ClassWriter cw = new ClassWriter(false);
			cw.visit(classMods, fqClassName, superClassName, interfaceNames, sourceFile);
			
			if (!isInterface)
			{
				// Generate the bsh instance 'This' reference holder field
				generateField(BSHTHIS + className, "Lbsh/This;", bsh.org.objectweb.asm.Constants_Fields.ACC_PUBLIC, cw);
				
				// Generate the static bsh static reference holder field
				generateField(BSHSTATIC + className, "Lbsh/This;", bsh.org.objectweb.asm.Constants_Fields.ACC_PUBLIC + bsh.org.objectweb.asm.Constants_Fields.ACC_STATIC, cw);
			}
			
			// Generate the fields
			for (int i = 0; i < vars.Length; i++)
			{
				System.String type = vars[i].TypeDescriptor;
				
				// Don't generate private or loosely typed fields
				// Note: loose types aren't currently parsed anyway...
				if (vars[i].hasModifier("private") || type == null)
					continue;
				
				int modifiers;
				if (isInterface)
					modifiers = bsh.org.objectweb.asm.Constants_Fields.ACC_PUBLIC | bsh.org.objectweb.asm.Constants_Fields.ACC_STATIC | bsh.org.objectweb.asm.Constants_Fields.ACC_FINAL;
				else
					modifiers = getASMModifiers(vars[i].Modifiers);
				
				generateField(vars[i].Name, type, modifiers, cw);
			}
			
			// Generate the constructors
			bool hasConstructor = false;
			for (int i = 0; i < constructors.Length; i++)
			{
				// Don't generate private constructors
				if (constructors[i].hasModifier("private"))
					continue;
				
				int modifiers = getASMModifiers(constructors[i].Modifiers);
				generateConstructor(i, constructors[i].ParamTypeDescriptors, modifiers, cw);
				hasConstructor = true;
			}
			
			// If no other constructors, generate a default constructor
			if (!isInterface && !hasConstructor)
				generateConstructor(DEFAULTCONSTRUCTOR, new System.String[0], bsh.org.objectweb.asm.Constants_Fields.ACC_PUBLIC, cw);
			
			// Generate the delegate methods
			for (int i = 0; i < methods.Length; i++)
			{
				System.String returnType = methods[i].ReturnTypeDescriptor;
				
				// Don't generate private /*or loosely return typed */ methods
				if (methods[i].hasModifier("private"))
					continue;
				
				int modifiers = getASMModifiers(methods[i].Modifiers);
				if (isInterface)
					modifiers |= (bsh.org.objectweb.asm.Constants_Fields.ACC_PUBLIC | bsh.org.objectweb.asm.Constants_Fields.ACC_ABSTRACT);
				
				generateMethod(className, fqClassName, methods[i].Name, returnType, methods[i].ParamTypeDescriptors, modifiers, cw);
				
				bool isStatic = (modifiers & bsh.org.objectweb.asm.Constants_Fields.ACC_STATIC) > 0;
				bool overridden = classContainsMethod(superClass, methods[i].Name, methods[i].ParamTypeDescriptors);
				if (!isStatic && overridden)
					generateSuperDelegateMethod(superClassName, methods[i].Name, returnType, methods[i].ParamTypeDescriptors, modifiers, cw);
			}
			
			return cw.toByteArray();
		}
		
		/// <summary>Translate bsh.Modifiers into ASM modifier bitflags.</summary>
		internal static int getASMModifiers(Modifiers modifiers)
		{
			int mods = 0;
			if (modifiers == null)
				return mods;
			
			if (modifiers.hasModifier("public"))
				mods += bsh.org.objectweb.asm.Constants_Fields.ACC_PUBLIC;
			if (modifiers.hasModifier("protected"))
				mods += bsh.org.objectweb.asm.Constants_Fields.ACC_PROTECTED;
			if (modifiers.hasModifier("static"))
				mods += bsh.org.objectweb.asm.Constants_Fields.ACC_STATIC;
			if (modifiers.hasModifier("synchronized"))
				mods += bsh.org.objectweb.asm.Constants_Fields.ACC_SYNCHRONIZED;
			if (modifiers.hasModifier("abstract"))
				mods += bsh.org.objectweb.asm.Constants_Fields.ACC_ABSTRACT;
			
			return mods;
		}
		
		/// <summary>Generate a field - static or instance.</summary>
		internal static void  generateField(System.String fieldName, System.String type, int modifiers, ClassWriter cw)
		{
			cw.visitField(modifiers, fieldName, type, (System.Object) null);
		}
		
		/// <summary>Generate a delegate method - static or instance.
		/// The generated code packs the method arguments into an object array
		/// (wrapping primitive types in bsh.Primitive), invokes the static or
		/// instance namespace invokeMethod() method, and then unwraps / returns
		/// the result.
		/// </summary>
		internal static void  generateMethod(System.String className, System.String fqClassName, System.String methodName, System.String returnType, System.String[] paramTypes, int modifiers, ClassWriter cw)
		{
			System.String[] exceptions = null;
			bool isStatic = (modifiers & bsh.org.objectweb.asm.Constants_Fields.ACC_STATIC) != 0;
			
			if (returnType == null)
			// map loose return type to Object
				returnType = OBJECT;
			
			System.String methodDescriptor = getMethodDescriptor(returnType, paramTypes);
			
			// Generate method body
			CodeVisitor cv = cw.visitMethod(modifiers, methodName, methodDescriptor, exceptions);
			
			if ((modifiers & bsh.org.objectweb.asm.Constants_Fields.ACC_ABSTRACT) != 0)
				return ;
			
			// Generate code to push the BSHTHIS or BSHSTATIC field 
			if (isStatic)
			{
				cv.visitFieldInsn(bsh.org.objectweb.asm.Constants_Fields.GETSTATIC, fqClassName, BSHSTATIC + className, "Lbsh/This;");
			}
			else
			{
				// Push 'this'
				cv.visitVarInsn(bsh.org.objectweb.asm.Constants_Fields.ALOAD, 0);
				
				// Get the instance field
				cv.visitFieldInsn(bsh.org.objectweb.asm.Constants_Fields.GETFIELD, fqClassName, BSHTHIS + className, "Lbsh/This;");
			}
			
			// Push the name of the method as a constant
			cv.visitLdcInsn(methodName);
			
			// Generate code to push arguments as an object array
			generateParameterReifierCode(paramTypes, isStatic, cv);
			
			// Push nulls for various args of invokeMethod
			cv.visitInsn(bsh.org.objectweb.asm.Constants_Fields.ACONST_NULL); // interpreter
			cv.visitInsn(bsh.org.objectweb.asm.Constants_Fields.ACONST_NULL); // callstack
			cv.visitInsn(bsh.org.objectweb.asm.Constants_Fields.ACONST_NULL); // callerinfo
			
			// Push the boolean constant 'true' (for declaredOnly)
			cv.visitInsn(bsh.org.objectweb.asm.Constants_Fields.ICONST_1);
			
			// Invoke the method This.invokeMethod( name, Class [] sig, boolean )
			cv.visitMethodInsn(bsh.org.objectweb.asm.Constants_Fields.INVOKEVIRTUAL, "bsh/This", "invokeMethod", Type.getMethodDescriptor(Type.getType(typeof(System.Object)), new Type[]{Type.getType(typeof(System.String)), Type.getType(typeof(System.Object[])), Type.getType(typeof(Interpreter)), Type.getType(typeof(CallStack)), Type.getType(typeof(SimpleNode)), Type.getType(System.Type.GetType("System.Boolean"))}));
			
			// Generate code to unwrap bsh Primitive types
			cv.visitMethodInsn(bsh.org.objectweb.asm.Constants_Fields.INVOKESTATIC, "bsh/Primitive", "unwrap", "(Ljava/lang/Object;)Ljava/lang/Object;");
			
			// Generate code to return the value
			generateReturnCode(returnType, cv);
			
			// Need to calculate this... just fudging here for now.
			cv.visitMaxs(20, 20);
		}
		
		/// <summary>Generate a constructor.</summary>
		internal virtual void  generateConstructor(int index, System.String[] paramTypes, int modifiers, ClassWriter cw)
		{
			/** offset after params of the args object [] var */
			//UPGRADE_NOTE: Final was removed from the declaration of 'argsVar '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int argsVar = paramTypes.Length + 1;
			/** offset after params of the ConstructorArgs var */
			//UPGRADE_NOTE: Final was removed from the declaration of 'consArgsVar '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int consArgsVar = paramTypes.Length + 2;
			
			System.String[] exceptions = null;
			System.String methodDescriptor = getMethodDescriptor("V", paramTypes);
			
			// Create this constructor method
			CodeVisitor cv = cw.visitMethod(modifiers, "<init>", methodDescriptor, exceptions);
			
			// Generate code to push arguments as an object array
			generateParameterReifierCode(paramTypes, false, cv);
			cv.visitVarInsn(bsh.org.objectweb.asm.Constants_Fields.ASTORE, argsVar);
			
			// Generate the code implementing the alternate constructor switch
			generateConstructorSwitch(index, argsVar, consArgsVar, cv);
			
			// Generate code to invoke the ClassGeneratorUtil initInstance() method
			
			// push 'this' 
			cv.visitVarInsn(bsh.org.objectweb.asm.Constants_Fields.ALOAD, 0);
			
			// Push the class/constructor name as a constant
			cv.visitLdcInsn(className);
			
			// Push arguments as an object array
			cv.visitVarInsn(bsh.org.objectweb.asm.Constants_Fields.ALOAD, argsVar);
			
			// invoke the initInstance() method
			cv.visitMethodInsn(bsh.org.objectweb.asm.Constants_Fields.INVOKESTATIC, "bsh/ClassGeneratorUtil", "initInstance", "(Ljava/lang/Object;Ljava/lang/String;[Ljava/lang/Object;)V");
			
			cv.visitInsn(bsh.org.objectweb.asm.Constants_Fields.RETURN);
			
			// Need to calculate this... just fudging here for now.
			cv.visitMaxs(20, 20);
		}
		
		/// <summary>Generate a switch with a branch for each possible alternate
		/// constructor.  This includes all superclass constructors and all 
		/// constructors of this class.  The default branch of this switch is the
		/// default superclass constructor.
		/// <p>
		/// This method also generates the code to call the static
		/// ClassGeneratorUtil
		/// getConstructorArgs() method which inspects the scripted constructor to
		/// find the alternate constructor signature (if any) and evalute the
		/// arguments at runtime.  The getConstructorArgs() method returns the
		/// actual arguments as well as the index of the constructor to call. 
		/// </summary>
		internal virtual void  generateConstructorSwitch(int consIndex, int argsVar, int consArgsVar, CodeVisitor cv)
		{
			Label defaultLabel = new Label();
			Label endLabel = new Label();
			int cases = superConstructors.Length + constructors.Length;
			
			Label[] labels = new Label[cases];
			for (int i = 0; i < cases; i++)
				labels[i] = new Label();
			
			// Generate code to call ClassGeneratorUtil to get our switch index 
			// and give us args...
			
			// push super class name
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			cv.visitLdcInsn(superClass.FullName); // use superClassName var?
			
			// push class static This object
			cv.visitFieldInsn(bsh.org.objectweb.asm.Constants_Fields.GETSTATIC, fqClassName, BSHSTATIC + className, "Lbsh/This;");
			
			// push args
			cv.visitVarInsn(bsh.org.objectweb.asm.Constants_Fields.ALOAD, argsVar);
			
			// push this constructor index number onto stack
			cv.visitIntInsn(bsh.org.objectweb.asm.Constants_Fields.BIPUSH, consIndex);
			
			// invoke the ClassGeneratorUtil getConstructorsArgs() method
			cv.visitMethodInsn(bsh.org.objectweb.asm.Constants_Fields.INVOKESTATIC, "bsh/ClassGeneratorUtil", "getConstructorArgs", "(Ljava/lang/String;Lbsh/This;[Ljava/lang/Object;I)" + "Lbsh/ClassGeneratorUtil$ConstructorArgs;");
			
			// store ConstructorArgs in consArgsVar
			cv.visitVarInsn(bsh.org.objectweb.asm.Constants_Fields.ASTORE, consArgsVar);
			
			// Get the ConstructorArgs selector field from ConstructorArgs
			
			// push ConstructorArgs 
			cv.visitVarInsn(bsh.org.objectweb.asm.Constants_Fields.ALOAD, consArgsVar);
			cv.visitFieldInsn(bsh.org.objectweb.asm.Constants_Fields.GETFIELD, "bsh/ClassGeneratorUtil$ConstructorArgs", "selector", "I");
			
			// start switch
			cv.visitTableSwitchInsn(0, cases - 1, defaultLabel, labels);
			
			// generate switch body
			int index = 0;
			for (int i = 0; i < superConstructors.Length; i++, index++)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Constructor.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				doSwitchBranch(index, superClassName, getTypeDescriptors(superConstructors[i].GetParameters()), endLabel, labels, consArgsVar, cv);
			}
			for (int i = 0; i < constructors.Length; i++, index++)
				doSwitchBranch(index, fqClassName, constructors[i].ParamTypeDescriptors, endLabel, labels, consArgsVar, cv);
			
			// generate the default branch of switch
			cv.visitLabel(defaultLabel);
			// default branch always invokes no args super
			cv.visitVarInsn(bsh.org.objectweb.asm.Constants_Fields.ALOAD, 0); // push 'this' 
			cv.visitMethodInsn(bsh.org.objectweb.asm.Constants_Fields.INVOKESPECIAL, superClassName, "<init>", "()V");
			
			// done with switch
			cv.visitLabel(endLabel);
		}
		
		/*
		Generate a branch of the constructor switch.  This method is called by
		generateConstructorSwitch.
		The code generated by this method assumes that the argument array is 
		on the stack.
		*/
		internal static void  doSwitchBranch(int index, System.String targetClassName, System.String[] paramTypes, Label endLabel, Label[] labels, int consArgsVar, CodeVisitor cv)
		{
			cv.visitLabel(labels[index]);
			//cv.visitLineNumber( index, labels[index] );
			cv.visitVarInsn(bsh.org.objectweb.asm.Constants_Fields.ALOAD, 0); // push this before args
			
			// Unload the arguments from the ConstructorArgs object
			for (int i = 0; i < paramTypes.Length; i++)
			{
				System.String type = paramTypes[i];
				System.String method = null;
				if (type.Equals("Z"))
					method = "getBoolean";
				else if (type.Equals("B"))
					method = "getByte";
				else if (type.Equals("C"))
					method = "getChar";
				else if (type.Equals("S"))
					method = "getShort";
				else if (type.Equals("I"))
					method = "getInt";
				else if (type.Equals("J"))
					method = "getLong";
				else if (type.Equals("D"))
					method = "getDouble";
				else if (type.Equals("F"))
					method = "getFloat";
				else
					method = "getObject";
				
				// invoke the iterator method on the ConstructorArgs
				cv.visitVarInsn(bsh.org.objectweb.asm.Constants_Fields.ALOAD, consArgsVar); // push the ConstructorArgs
				System.String className = "bsh/ClassGeneratorUtil$ConstructorArgs";
				System.String retType;
				if (method.Equals("getObject"))
					retType = OBJECT;
				else
					retType = type;
				cv.visitMethodInsn(bsh.org.objectweb.asm.Constants_Fields.INVOKEVIRTUAL, className, method, "()" + retType);
				// if it's an object type we must do a check cast
				if (method.Equals("getObject"))
					cv.visitTypeInsn(bsh.org.objectweb.asm.Constants_Fields.CHECKCAST, descriptorToClassName(type));
			}
			
			// invoke the constructor for this branch
			System.String descriptor = getMethodDescriptor("V", paramTypes);
			cv.visitMethodInsn(bsh.org.objectweb.asm.Constants_Fields.INVOKESPECIAL, targetClassName, "<init>", descriptor);
			cv.visitJumpInsn(bsh.org.objectweb.asm.Constants_Fields.GOTO, endLabel);
		}
		
		internal static System.String getMethodDescriptor(System.String returnType, System.String[] paramTypes)
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder("(");
			for (int i = 0; i < paramTypes.Length; i++)
				sb.Append(paramTypes[i]);
			sb.Append(")" + returnType);
			return sb.ToString();
		}
		
		/// <summary>Generate a superclass method delegate accessor method.
		/// These methods are specially named methods which allow access to
		/// overridden methods of the superclass (which the Java reflection API
		/// normally does not allow).
		/// </summary>
		// Maybe combine this with generateMethod()
		internal static void  generateSuperDelegateMethod(System.String superClassName, System.String methodName, System.String returnType, System.String[] paramTypes, int modifiers, ClassWriter cw)
		{
			System.String[] exceptions = null;
			
			if (returnType == null)
			// map loose return to Object
				returnType = OBJECT;
			
			System.String methodDescriptor = getMethodDescriptor(returnType, paramTypes);
			
			// Add method body
			CodeVisitor cv = cw.visitMethod(modifiers, "_bshSuper" + methodName, methodDescriptor, exceptions);
			
			cv.visitVarInsn(bsh.org.objectweb.asm.Constants_Fields.ALOAD, 0);
			// Push vars
			int localVarIndex = 1;
			for (int i = 0; i < paramTypes.Length; ++i)
			{
				if (isPrimitive(paramTypes[i]))
					cv.visitVarInsn(bsh.org.objectweb.asm.Constants_Fields.ILOAD, localVarIndex);
				else
					cv.visitVarInsn(bsh.org.objectweb.asm.Constants_Fields.ALOAD, localVarIndex);
				localVarIndex += ((paramTypes[i].Equals("D") || paramTypes[i].Equals("J"))?2:1);
			}
			
			cv.visitMethodInsn(bsh.org.objectweb.asm.Constants_Fields.INVOKESPECIAL, superClassName, methodName, methodDescriptor);
			
			generatePlainReturnCode(returnType, cv);
			
			// Need to calculate this... just fudging here for now.
			cv.visitMaxs(20, 20);
		}
		
		internal virtual bool classContainsMethod(System.Type clas, System.String methodName, System.String[] paramTypes)
		{
			while (clas != null)
			{
				System.Reflection.MethodInfo[] methods = clas.GetMethods(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly | System.Reflection.BindingFlags.Static);
				for (int i = 0; i < methods.Length; i++)
				{
					if (methods[i].Name.Equals(methodName))
					{
						//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						System.String[] methodParamTypes = getTypeDescriptors(methods[i].GetParameters());
						bool found = true;
						for (int j = 0; j < methodParamTypes.Length; j++)
						{
							if (!paramTypes[j].Equals(methodParamTypes[j]))
							{
								found = false;
								break;
							}
						}
						if (found)
							return true;
					}
				}
				
				clas = clas.BaseType;
			}
			
			return false;
		}
		
		/// <summary>Generate return code for a normal bytecode</summary>
		internal static void  generatePlainReturnCode(System.String returnType, CodeVisitor cv)
		{
			if (returnType.Equals("V"))
				cv.visitInsn(bsh.org.objectweb.asm.Constants_Fields.RETURN);
			else if (isPrimitive(returnType))
			{
				int opcode = bsh.org.objectweb.asm.Constants_Fields.IRETURN;
				if (returnType.Equals("D"))
					opcode = bsh.org.objectweb.asm.Constants_Fields.DRETURN;
				else if (returnType.Equals("F"))
					opcode = bsh.org.objectweb.asm.Constants_Fields.FRETURN;
				else if (returnType.Equals("J"))
				//long
					opcode = bsh.org.objectweb.asm.Constants_Fields.LRETURN;
				
				cv.visitInsn(opcode);
			}
			else
			{
				cv.visitTypeInsn(bsh.org.objectweb.asm.Constants_Fields.CHECKCAST, descriptorToClassName(returnType));
				cv.visitInsn(bsh.org.objectweb.asm.Constants_Fields.ARETURN);
			}
		}
		
		/// <summary>Generates the code to reify the arguments of the given method.
		/// For a method "int m (int i, String s)", this code is the bytecode
		/// corresponding to the "new Object[] { new bsh.Primitive(i), s }" 
		/// expression.
		/// </summary>
		/// <author>  Eric Bruneton
		/// </author>
		/// <author>  Pat Niemeyer
		/// </author>
		/// <param name="cv">the code visitor to be used to generate the bytecode.
		/// </param>
		/// <param name="isStatic">the enclosing methods is static
		/// </param>
		public static void  generateParameterReifierCode(System.String[] paramTypes, bool isStatic, CodeVisitor cv)
		{
			cv.visitIntInsn(bsh.org.objectweb.asm.Constants_Fields.SIPUSH, paramTypes.Length);
			cv.visitTypeInsn(bsh.org.objectweb.asm.Constants_Fields.ANEWARRAY, "java/lang/Object");
			int localVarIndex = isStatic?0:1;
			for (int i = 0; i < paramTypes.Length; ++i)
			{
				System.String param = paramTypes[i];
				cv.visitInsn(bsh.org.objectweb.asm.Constants_Fields.DUP);
				cv.visitIntInsn(bsh.org.objectweb.asm.Constants_Fields.SIPUSH, i);
				if (isPrimitive(param))
				{
					int opcode;
					if (param.Equals("F"))
					{
						opcode = bsh.org.objectweb.asm.Constants_Fields.FLOAD;
					}
					else if (param.Equals("D"))
					{
						opcode = bsh.org.objectweb.asm.Constants_Fields.DLOAD;
					}
					else if (param.Equals("J"))
					{
						opcode = bsh.org.objectweb.asm.Constants_Fields.LLOAD;
					}
					else
					{
						opcode = bsh.org.objectweb.asm.Constants_Fields.ILOAD;
					}
					
					System.String type = "bsh/Primitive";
					cv.visitTypeInsn(bsh.org.objectweb.asm.Constants_Fields.NEW, type);
					cv.visitInsn(bsh.org.objectweb.asm.Constants_Fields.DUP);
					cv.visitVarInsn(opcode, localVarIndex);
					System.String desc = param; // ok?
					cv.visitMethodInsn(bsh.org.objectweb.asm.Constants_Fields.INVOKESPECIAL, type, "<init>", "(" + desc + ")V");
				}
				else
				{
					// Technically incorrect here - we need to wrap null values
					// as bsh.Primitive.NULL.  However the This.invokeMethod()
					// will do that much for us.
					// We need to generate a conditional here to test for null
					// and return Primitive.NULL
					cv.visitVarInsn(bsh.org.objectweb.asm.Constants_Fields.ALOAD, localVarIndex);
				}
				cv.visitInsn(bsh.org.objectweb.asm.Constants_Fields.AASTORE);
				localVarIndex += ((param.Equals("D") || param.Equals("J"))?2:1);
			}
		}
		
		/// <summary>Generates the code to unreify the result of the given method.  For a
		/// method "int m (int i, String s)", this code is the bytecode
		/// corresponding to the "((Integer)...).intValue()" expression.
		/// </summary>
		/// <param name="cv">the code visitor to be used to generate the bytecode.
		/// </param>
		/// <author>  Eric Bruneton
		/// </author>
		/// <author>  Pat Niemeyer
		/// </author>
		public static void  generateReturnCode(System.String returnType, CodeVisitor cv)
		{
			if (returnType.Equals("V"))
			{
				cv.visitInsn(bsh.org.objectweb.asm.Constants_Fields.POP);
				cv.visitInsn(bsh.org.objectweb.asm.Constants_Fields.RETURN);
			}
			else if (isPrimitive(returnType))
			{
				int opcode = bsh.org.objectweb.asm.Constants_Fields.IRETURN;
				System.String type;
				System.String meth;
				if (returnType.Equals("B"))
				{
					type = "java/lang/Byte";
					meth = "byteValue";
				}
				else if (returnType.Equals("I"))
				{
					type = "java/lang/Integer";
					meth = "intValue";
				}
				else if (returnType.Equals("Z"))
				{
					type = "java/lang/Boolean";
					meth = "booleanValue";
				}
				else if (returnType.Equals("D"))
				{
					opcode = bsh.org.objectweb.asm.Constants_Fields.DRETURN;
					type = "java/lang/Double";
					meth = "doubleValue";
				}
				else if (returnType.Equals("F"))
				{
					opcode = bsh.org.objectweb.asm.Constants_Fields.FRETURN;
					type = "java/lang/Float";
					meth = "floatValue";
				}
				else if (returnType.Equals("J"))
				{
					opcode = bsh.org.objectweb.asm.Constants_Fields.LRETURN;
					type = "java/lang/Long";
					meth = "longValue";
				}
				else if (returnType.Equals("C"))
				{
					type = "java/lang/Character";
					meth = "charValue";
				}
				/*if (returnType.equals("S") )*/
				else
				{
					type = "java/lang/Short";
					meth = "shortValue";
				}
				
				System.String desc = returnType;
				cv.visitTypeInsn(bsh.org.objectweb.asm.Constants_Fields.CHECKCAST, type); // type is correct here
				cv.visitMethodInsn(bsh.org.objectweb.asm.Constants_Fields.INVOKEVIRTUAL, type, meth, "()" + desc);
				cv.visitInsn(opcode);
			}
			else
			{
				cv.visitTypeInsn(bsh.org.objectweb.asm.Constants_Fields.CHECKCAST, descriptorToClassName(returnType));
				cv.visitInsn(bsh.org.objectweb.asm.Constants_Fields.ARETURN);
			}
		}
		
		/// <summary>Evaluate the arguments (if any) for the constructor specified by
		/// the constructor index.  Return the ConstructorArgs object which
		/// contains the actual arguments to the alternate constructor and also the
		/// index of that constructor for the constructor switch.
		/// </summary>
		/// <param name="consArgs">the arguments to the constructor.  These are necessary in
		/// the evaluation of the alt constructor args.  e.g. Foo(a) { super(a); }
		/// </param>
		/// <returns> the ConstructorArgs object containing a constructor selector
		/// and evaluated arguments for the alternate constructor
		/// </returns>
		public static ConstructorArgs getConstructorArgs(System.String superClassName, This classStaticThis, System.Object[] consArgs, int index)
		{
			DelayedEvalBshMethod[] constructors;
			try
			{
				constructors = (DelayedEvalBshMethod[]) classStaticThis.NameSpace.getVariable(BSHCONSTRUCTORS);
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new InterpreterError("unable to get instance initializer: " + e);
			}
			
			if (index == DEFAULTCONSTRUCTOR)
			// auto-gen default constructor
				return ConstructorArgs.DEFAULT; // use default super constructor
			
			DelayedEvalBshMethod constructor = constructors[index];
			
			if (constructor.methodBody.jjtGetNumChildren() == 0)
				return ConstructorArgs.DEFAULT; // use default super constructor
			
			// Determine if the constructor calls this() or super()
			System.String altConstructor = null;
			BSHArguments argsNode = null;
			SimpleNode firstStatement = (SimpleNode) constructor.methodBody.jjtGetChild(0);
			if (firstStatement is BSHPrimaryExpression)
				firstStatement = (SimpleNode) firstStatement.jjtGetChild(0);
			if (firstStatement is BSHMethodInvocation)
			{
				BSHMethodInvocation methodNode = (BSHMethodInvocation) firstStatement;
				BSHAmbiguousName methodName = methodNode.NameNode;
				if (methodName.text.Equals("super") || methodName.text.Equals("this"))
				{
					altConstructor = methodName.text;
					argsNode = methodNode.ArgsNode;
				}
			}
			
			if (altConstructor == null)
				return ConstructorArgs.DEFAULT; // use default super constructor
			
			// Make a tmp namespace to hold the original constructor args for
			// use in eval of the parameters node
			NameSpace consArgsNameSpace = new NameSpace(classStaticThis.NameSpace, "consArgs");
			System.String[] consArgNames = constructor.ParameterNames;
			System.Type[] consArgTypes = constructor.ParameterTypes;
			for (int i = 0; i < consArgs.Length; i++)
			{
				try
				{
					consArgsNameSpace.setTypedVariable(consArgNames[i], consArgTypes[i], consArgs[i], null);
				}
				catch (UtilEvalError e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new InterpreterError("err setting local cons arg:" + e);
				}
			}
			
			// evaluate the args
			
			CallStack callstack = new CallStack();
			callstack.push(consArgsNameSpace);
			System.Object[] args = null;
			Interpreter interpreter = classStaticThis.declaringInterpreter;
			
			try
			{
				args = argsNode.getArguments(callstack, interpreter);
			}
			catch (EvalError e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new InterpreterError("Error evaluating constructor args: " + e);
			}
			
			System.Type[] argTypes = Types.getTypes(args);
			args = Primitive.unwrap(args);
			System.Type superClass = interpreter.ClassManager.classForName(superClassName);
			if (superClass == null)
				throw new InterpreterError("can't find superclass: " + superClassName);
			System.Reflection.ConstructorInfo[] superCons = superClass.GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.DeclaredOnly);
			
			// find the matching super() constructor for the args
			if (altConstructor.Equals("super"))
			{
				int i = Reflect.findMostSpecificConstructorIndex(argTypes, superCons);
				if (i == - 1)
					throw new InterpreterError("can't find constructor for args!");
				return new ConstructorArgs(i, args);
			}
			
			// find the matching this() constructor for the args
			System.Type[][] candidates = new System.Type[constructors.Length][];
			for (int i = 0; i < candidates.Length; i++)
				candidates[i] = constructors[i].ParameterTypes;
			int i2 = Reflect.findMostSpecificSignature(argTypes, candidates);
			if (i2 == - 1)
				throw new InterpreterError("can't find constructor for args 2!");
			// this() constructors come after super constructors in the table
			
			int selector = i2 + superCons.Length;
			int ourSelector = index + superCons.Length;
			
			// Are we choosing ourselves recursively through a this() reference?
			if (selector == ourSelector)
				throw new InterpreterError("Recusive constructor call.");
			
			return new ConstructorArgs(selector, args);
		}
		
		/// <summary>Initialize an instance of the class.
		/// This method is called from the generated class constructor to evaluate
		/// the instance initializer and scripted constructor in the instance
		/// namespace.
		/// </summary>
		public static void  initInstance(System.Object instance, System.String className, System.Object[] args)
		{
			System.Type[] sig = Types.getTypes(args);
			CallStack callstack = new CallStack();
			Interpreter interpreter;
			NameSpace instanceNameSpace;
			
			// check to see if the instance has already been initialized
			// (the case if using a this() alternate constuctor)
			This instanceThis = getClassInstanceThis(instance, className);
			
			// XXX clean up this conditional
			if (instanceThis == null)
			{
				// Create the instance 'This' namespace, set it on the object
				// instance and invoke the instance initializer
				
				// Get the static This reference from the proto-instance
				This classStaticThis = getClassStaticThis(instance.GetType(), className);
				interpreter = classStaticThis.declaringInterpreter;
				
				// Get the instance initializer block from the static This 
				BSHBlock instanceInitBlock;
				try
				{
					instanceInitBlock = (BSHBlock) classStaticThis.NameSpace.getVariable(BSHINIT);
				}
				catch (System.Exception e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new InterpreterError("unable to get instance initializer: " + e);
				}
				
				// Create the instance namespace
				instanceNameSpace = new NameSpace(classStaticThis.NameSpace, className);
				instanceNameSpace.isClass = true;
				
				// Set the instance This reference on the instance
				instanceThis = instanceNameSpace.getThis(interpreter);
				try
				{
					LHS lhs = Reflect.getLHSObjectField(instance, BSHTHIS + className);
					lhs.assign(instanceThis, false);
				}
				catch (System.Exception e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new InterpreterError("Error in class gen setup: " + e);
				}
				
				// Give the instance space its object import
				instanceNameSpace.ClassInstance = instance;
				
				// should use try/finally here to pop ns
				callstack.push(instanceNameSpace);
				
				// evaluate the instance portion of the block in it
				try
				{
					// Evaluate the initializer block
					instanceInitBlock.evalBlock(callstack, interpreter, true, ClassGeneratorImpl.ClassNodeFilter.CLASSINSTANCE);
				}
				catch (System.Exception e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new InterpreterError("Error in class initialization: " + e);
				}
				
				callstack.pop();
			}
			else
			{
				// The object instance has already been initialzed by another
				// constructor.  Fall through to invoke the constructor body below.
				interpreter = instanceThis.declaringInterpreter;
				instanceNameSpace = instanceThis.NameSpace;
			}
			
			// invoke the constructor method from the instanceThis 
			
			System.String constructorName = getBaseName(className);
			try
			{
				// Find the constructor (now in the instance namespace)
				BshMethod constructor = instanceNameSpace.getMethod(constructorName, sig, true);
				
				// if args, we must have constructor
				if (args.Length > 0 && constructor == null)
					throw new InterpreterError("Can't find constructor: " + className);
				
				// Evaluate the constructor
				if (constructor != null)
					constructor.invoke(args, interpreter, callstack, null, false);
			}
			catch (System.Exception e)
			{
				if (e is TargetError)
					e = (System.Exception) ((TargetError) e).Target;
				if (e is System.Reflection.TargetInvocationException)
					e = (System.Exception) ((System.Reflection.TargetInvocationException) e).GetBaseException();
				if (e is bsh.TargetError)
					((bsh.TargetError) e).printStackTrace(System.Console.Error);
				else
					SupportClass.WriteStackTrace(e, System.Console.Error);
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new InterpreterError("Error in class initialization: " + e);
			}
		}
		
		/// <summary>Get the static bsh namespace field from the class.</summary>
		/// <param name="className">may be the name of clas itself or a superclass of clas.
		/// </param>
		internal static This getClassStaticThis(System.Type clas, System.String className)
		{
			try
			{
				return (This) Reflect.getStaticFieldValue(clas, BSHSTATIC + className);
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new InterpreterError("Unable to get class static space: " + e);
			}
		}
		
		/// <summary>Get the instance bsh namespace field from the object instance.</summary>
		/// <returns> the class instance This object or null if the object has not
		/// been initialized.
		/// </returns>
		internal static This getClassInstanceThis(System.Object instance, System.String className)
		{
			try
			{
				System.Object o = Reflect.getObjectFieldValue(instance, BSHTHIS + className);
				return (This) Primitive.unwrap(o); // unwrap Primitive.Null to null
			}
			catch (System.Exception e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new InterpreterError("Generated class: Error getting This" + e);
			}
		}
		
		/// <summary>Does the type descriptor string describe a primitive type?</summary>
		private static bool isPrimitive(System.String typeDescriptor)
		{
			return typeDescriptor.Length == 1; // right?
		}
		
		internal static System.String[] getTypeDescriptors(System.Type[] cparams)
		{
			System.String[] sa = new System.String[cparams.Length];
			for (int i = 0; i < sa.Length; i++)
				sa[i] = BSHType.getTypeDescriptor(cparams[i]);
			return sa;
		}
		
		/// <summary>If a non-array object type, remove the prefix "L" and suffix ";".</summary>
		// Can this be factored out...?  
		// Should be be adding the L...; here instead?
		private static System.String descriptorToClassName(System.String s)
		{
			if (s.StartsWith("[") || !s.StartsWith("L"))
				return s;
			return s.Substring(1, (s.Length - 1) - (1));
		}
		
		private static System.String getBaseName(System.String className)
		{
			int i = className.IndexOf("$");
			if (i == - 1)
				return className;
			
			return className.Substring(i + 1);
		}
		
		/// <summary>A ConstructorArgs object holds evaluated arguments for a constructor
		/// call as well as the index of a possible alternate selector to invoke.
		/// This object is used by the constructor switch.
		/// </summary>
		/// <seealso cref="generateConstructor( int , String [] , int , ClassWriter )">
		/// </seealso>
		public class ConstructorArgs
		{
			private void  InitBlock()
			{
				selector = bsh.ClassGeneratorUtil.DEFAULTCONSTRUCTOR;
			}
			virtual public bool Boolean
			{
				get
				{
					return ((System.Boolean) next());
				}
				
			}
			virtual public sbyte Byte
			{
				get
				{
					return (sbyte) ((System.SByte) next());
				}
				
			}
			virtual public char Char
			{
				get
				{
					return ((System.Char) next());
				}
				
			}
			virtual public short Short
			{
				get
				{
					return (short) ((System.Int16) next());
				}
				
			}
			virtual public int Int
			{
				get
				{
					return ((System.Int32) next());
				}
				
			}
			virtual public long Long
			{
				get
				{
					return (long) ((System.Int64) next());
				}
				
			}
			virtual public double Double
			{
				get
				{
					return ((System.Double) next());
				}
				
			}
			virtual public float Float
			{
				get
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Float.floatValue' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					return (float) ((System.Single) next());
				}
				
			}
			virtual public System.Object Object
			{
				get
				{
					return next();
				}
				
			}
			/// <summary>A ConstructorArgs which calls the default constructor </summary>
			public static ConstructorArgs DEFAULT = new ConstructorArgs();
			
			//UPGRADE_NOTE: The initialization of  'selector' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			public int selector;
			internal System.Object[] args;
			internal int arg = 0;
			/// <summary>The index of the constructor to call.</summary>
			
			internal ConstructorArgs()
			{
			}
			
			internal ConstructorArgs(int selector, System.Object[] args)
			{
				this.selector = selector;
				this.args = args;
			}
			
			internal virtual System.Object next()
			{
				return args[arg++];
			}
		}
	}
}