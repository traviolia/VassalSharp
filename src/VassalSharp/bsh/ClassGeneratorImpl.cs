using System;
namespace bsh
{
	
	/// <author>  Pat Niemeyer (pat@pat.net)
	/// </author>
	public class ClassGeneratorImpl:ClassGenerator
	{
		public override System.Type generateClass(System.String name, Modifiers modifiers, System.Type[] interfaces, System.Type superClass, BSHBlock block, bool isInterface, CallStack callstack, Interpreter interpreter)
		{
			// Delegate to the static method
			return generateClassImpl(name, modifiers, interfaces, superClass, block, isInterface, callstack, interpreter);
		}
		
		public override System.Object invokeSuperclassMethod(BshClassManager bcm, System.Object instance, System.String methodName, System.Object[] args)
		{
			// Delegate to the static method
			return invokeSuperclassMethodImpl(bcm, instance, methodName, args);
		}
		
		/// <summary>Change the parent of the class instance namespace.
		/// This is currently used for inner class support.
		/// Note: This method will likely be removed in the future.
		/// </summary>
		// This could be static
		public override void  setInstanceNameSpaceParent(System.Object instance, System.String className, NameSpace parent)
		{
			This ithis = ClassGeneratorUtil.getClassInstanceThis(instance, className);
			ithis.NameSpace.Parent = parent;
		}
		
		/// <summary>Parse the BSHBlock for for the class definition and generate the class
		/// using ClassGenerator.
		/// </summary>
		public static System.Type generateClassImpl(System.String name, Modifiers modifiers, System.Type[] interfaces, System.Type superClass, BSHBlock block, bool isInterface, CallStack callstack, Interpreter interpreter)
		{
			// Scripting classes currently requires accessibility
			// This can be eliminated with a bit more work.
			try
			{
				Capabilities.Accessibility = true;
			}
			catch (Capabilities.Unavailable e)
			{
				throw new EvalError("Defining classes currently requires reflective Accessibility.", block, callstack);
			}
			
			NameSpace enclosingNameSpace = callstack.top();
			System.String packageName = enclosingNameSpace.Package;
			System.String className = enclosingNameSpace.isClass?(enclosingNameSpace.Name + "$" + name):name;
			System.String fqClassName = packageName == null?className:packageName + "." + className;
			
			BshClassManager bcm = interpreter.ClassManager;
			// Race condition here...
			bcm.definingClass(fqClassName);
			
			// Create the class static namespace
			NameSpace classStaticNameSpace = new NameSpace(enclosingNameSpace, className);
			classStaticNameSpace.isClass = true;
			
			callstack.push(classStaticNameSpace);
			
			// Evaluate any inner class class definitions in the block 
			// effectively recursively call this method for contained classes first
			block.evalBlock(callstack, interpreter, true, ClassNodeFilter.CLASSCLASSES);
			
			// Generate the type for our class
			Variable[] variables = getDeclaredVariables(block, callstack, interpreter, packageName);
			DelayedEvalBshMethod[] methods = getDeclaredMethods(block, callstack, interpreter, packageName);
			
			ClassGeneratorUtil classGenerator = new ClassGeneratorUtil(modifiers, className, packageName, superClass, interfaces, variables, methods, classStaticNameSpace, isInterface);
			sbyte[] code = classGenerator.generateClass();
			
			// if debug, write out the class file to debugClasses directory
			//UPGRADE_ISSUE: Method 'java.lang.System.getProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangSystem'"
			System.String dir = System_Renamed.getProperty("debugClasses");
			if (dir != null)
				try
				{
					//UPGRADE_TODO: Constructor 'java.io.FileOutputStream.FileOutputStream' was converted to 'System.IO.FileStream.FileStream' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaioFileOutputStreamFileOutputStream_javalangString'"
					System.IO.FileStream out_Renamed = new System.IO.FileStream(dir + "/" + className + ".class", System.IO.FileMode.Create);
					SupportClass.WriteOutput(out_Renamed, code);
					out_Renamed.Close();
				}
				catch (System.IO.IOException e)
				{
				}
			
			// Define the new class in the classloader
			System.Type genClass = bcm.defineClass(fqClassName, code);
			
			// import the unq name into parent
			enclosingNameSpace.importClass(fqClassName.Replace('$', '.'));
			
			try
			{
				classStaticNameSpace.setLocalVariable(ClassGeneratorUtil.BSHINIT, block, false);
			}
			catch (UtilEvalError e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new InterpreterError("unable to init static: " + e);
			}
			
			// Give the static space its class static import
			// important to do this after all classes are defined
			classStaticNameSpace.ClassStatic = genClass;
			
			// evaluate the static portion of the block in the static space
			block.evalBlock(callstack, interpreter, true, ClassNodeFilter.CLASSSTATIC);
			
			callstack.pop();
			
			if (!genClass.IsInterface)
			{
				// Set the static bsh This callback 
				System.String bshStaticFieldName = ClassGeneratorUtil.BSHSTATIC + className;
				try
				{
					LHS lhs = Reflect.getLHSStaticField(genClass, bshStaticFieldName);
					lhs.assign(classStaticNameSpace.getThis(interpreter), false);
				}
				catch (System.Exception e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new InterpreterError("Error in class gen setup: " + e);
				}
			}
			
			bcm.doneDefiningClass(fqClassName);
			return genClass;
		}
		
		internal static Variable[] getDeclaredVariables(BSHBlock body, CallStack callstack, Interpreter interpreter, System.String defaultPackage)
		{
			System.Collections.IList vars = new System.Collections.ArrayList();
			for (int child = 0; child < body.jjtGetNumChildren(); child++)
			{
				SimpleNode node = (SimpleNode) body.jjtGetChild(child);
				if (node is BSHTypedVariableDeclaration)
				{
					BSHTypedVariableDeclaration tvd = (BSHTypedVariableDeclaration) node;
					Modifiers modifiers = tvd.modifiers;
					
					System.String type = tvd.getTypeDescriptor(callstack, interpreter, defaultPackage);
					
					BSHVariableDeclarator[] vardec = tvd.Declarators;
					for (int i = 0; i < vardec.Length; i++)
					{
						System.String name = vardec[i].name;
						try
						{
							Variable var = new Variable(name, type, (System.Object) null, modifiers);
							vars.Add(var);
						}
						catch (UtilEvalError e)
						{
							// value error shouldn't happen
						}
					}
				}
			}
			
			return (Variable[]) SupportClass.ICollectionSupport.ToArray(vars, new Variable[0]);
		}
		
		internal static DelayedEvalBshMethod[] getDeclaredMethods(BSHBlock body, CallStack callstack, Interpreter interpreter, System.String defaultPackage)
		{
			System.Collections.IList methods = new System.Collections.ArrayList();
			for (int child = 0; child < body.jjtGetNumChildren(); child++)
			{
				SimpleNode node = (SimpleNode) body.jjtGetChild(child);
				if (node is BSHMethodDeclaration)
				{
					BSHMethodDeclaration md = (BSHMethodDeclaration) node;
					md.insureNodesParsed();
					Modifiers modifiers = md.modifiers;
					System.String name = md.name;
					System.String returnType = md.getReturnTypeDescriptor(callstack, interpreter, defaultPackage);
					BSHReturnType returnTypeNode = md.ReturnTypeNode;
					BSHFormalParameters paramTypesNode = md.paramsNode;
					System.String[] paramTypes = paramTypesNode.getTypeDescriptors(callstack, interpreter, defaultPackage);
					
					DelayedEvalBshMethod bm = new DelayedEvalBshMethod(name, returnType, returnTypeNode, md.paramsNode.ParamNames, paramTypes, paramTypesNode, md.blockNode, null, modifiers, callstack, interpreter);
					
					methods.Add(bm);
				}
			}
			
			return (DelayedEvalBshMethod[]) SupportClass.ICollectionSupport.ToArray(methods, new DelayedEvalBshMethod[0]);
		}
		
		/// <summary>A node filter that filters nodes for either a class body static 
		/// initializer or instance initializer.  In the static case only static 
		/// members are passed, etc.  
		/// </summary>
		internal class ClassNodeFilter : BSHBlock.NodeFilter
		{
			public const int STATIC = 0;
			public const int INSTANCE = 1;
			public const int CLASSES = 2;
			
			//UPGRADE_NOTE: The initialization of  'CLASSSTATIC' was moved to static method 'bsh.ClassGeneratorImpl.ClassNodeFilter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			public static ClassNodeFilter CLASSSTATIC;
			//UPGRADE_NOTE: The initialization of  'CLASSINSTANCE' was moved to static method 'bsh.ClassGeneratorImpl.ClassNodeFilter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			public static ClassNodeFilter CLASSINSTANCE;
			//UPGRADE_NOTE: The initialization of  'CLASSCLASSES' was moved to static method 'bsh.ClassGeneratorImpl.ClassNodeFilter'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			public static ClassNodeFilter CLASSCLASSES;
			
			internal int context;
			
			internal ClassNodeFilter(int context)
			{
				this.context = context;
			}
			
			public virtual bool isVisible(SimpleNode node)
			{
				if (context == CLASSES)
					return node is BSHClassDeclaration;
				
				// Only show class decs in CLASSES
				if (node is BSHClassDeclaration)
					return false;
				
				if (context == STATIC)
					return isStatic(node);
				
				if (context == INSTANCE)
					return !isStatic(node);
				
				// ALL
				return true;
			}
			
			internal virtual bool isStatic(SimpleNode node)
			{
				if (node is BSHTypedVariableDeclaration)
					return ((BSHTypedVariableDeclaration) node).modifiers != null && ((BSHTypedVariableDeclaration) node).modifiers.hasModifier("static");
				
				if (node is BSHMethodDeclaration)
					return ((BSHMethodDeclaration) node).modifiers != null && ((BSHMethodDeclaration) node).modifiers.hasModifier("static");
				
				// need to add static block here
				if (node is BSHBlock)
					return false;
				
				return false;
			}
			static ClassNodeFilter()
			{
				CLASSSTATIC = new ClassNodeFilter(STATIC);
				CLASSINSTANCE = new ClassNodeFilter(INSTANCE);
				CLASSCLASSES = new ClassNodeFilter(CLASSES);
			}
		}
		
		public static System.Object invokeSuperclassMethodImpl(BshClassManager bcm, System.Object instance, System.String methodName, System.Object[] args)
		{
			System.String superName = ClassGeneratorUtil.BSHSUPER + methodName;
			
			// look for the specially named super delegate method
			System.Type clas = instance.GetType();
			System.Reflection.MethodInfo superMethod = Reflect.resolveJavaMethod(bcm, clas, superName, Types.getTypes(args), false);
			if (superMethod != null)
				return Reflect.invokeMethod(superMethod, instance, args);
			
			// No super method, try to invoke regular method
			// could be a superfluous "super." which is legal.
			System.Type superClass = clas.BaseType;
			superMethod = Reflect.resolveExpectedJavaMethod(bcm, superClass, instance, methodName, args, false);
			return Reflect.invokeMethod(superMethod, instance, args);
		}
	}
}