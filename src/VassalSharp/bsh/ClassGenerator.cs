using System;
using Unavailable = bsh.Capabilities.Unavailable;
namespace bsh
{
	
	public abstract class ClassGenerator
	{
		private static ClassGenerator cg;
		
		public static ClassGenerator getClassGenerator()
		{
			if (cg == null)
			{
				try
				{
					//UPGRADE_TODO: The differences in the format  of parameters for method 'java.lang.Class.forName'  may cause compilation errors.  "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1092'"
					System.Type clas = System.Type.GetType("bsh.ClassGeneratorImpl");
					cg = (ClassGenerator) System.Activator.CreateInstance(clas);
				}
				catch (System.Exception e)
				{
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					throw new Unavailable("ClassGenerator unavailable: " + e);
				}
			}
			
			return cg;
		}
		
		/// <summary>Parse the BSHBlock for the class definition and generate the class.</summary>
		public abstract System.Type generateClass(System.String name, Modifiers modifiers, System.Type[] interfaces, System.Type superClass, BSHBlock block, bool isInterface, CallStack callstack, Interpreter interpreter);
		
		/// <summary>Invoke a super.method() style superclass method on an object instance.
		/// This is not a normal function of the Java reflection API and is
		/// provided by generated class accessor methods.
		/// </summary>
		public abstract System.Object invokeSuperclassMethod(BshClassManager bcm, System.Object instance, System.String methodName, System.Object[] args);
		
		/// <summary>Change the parent of the class instance namespace.
		/// This is currently used for inner class support.
		/// Note: This method will likely be removed in the future.
		/// </summary>
		public abstract void  setInstanceNameSpaceParent(System.Object instance, System.String className, NameSpace parent);
	}
}