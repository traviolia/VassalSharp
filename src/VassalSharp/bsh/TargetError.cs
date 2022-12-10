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
	
	/// <summary>TargetError is an EvalError that wraps an exception thrown by the script	
	/// (or by code called from the script).  TargetErrors indicate exceptions 
	/// which can be caught within the script itself, whereas a general EvalError 
	/// indicates that the script cannot be evaluated further for some reason.
	/// </summary>
	/// <summary>If the exception is caught within the script it is automatically unwrapped,
	/// so the code looks like normal Java code.  If the TargetError is thrown
	/// from the eval() or interpreter.eval() method it may be caught and unwrapped
	/// to determine what exception was thrown.
	/// </summary>
	[Serializable]
	public class TargetError:EvalError
	{
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		virtual public System.Exception Target
		{
			get
			{
				// check for easy mistake
				if (target is System.Reflection.TargetInvocationException)
					return ((System.Reflection.TargetInvocationException) target).GetBaseException();
				else
					return target;
			}
			
		}
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		internal System.Exception target;
		internal bool inNativeCode_Renamed_Field;
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public TargetError(System.String msg, System.Exception t, SimpleNode node, CallStack callstack, bool inNativeCode):base(msg, node, callstack)
		{
			target = t;
			this.inNativeCode_Renamed_Field = inNativeCode;
		}
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public TargetError(System.Exception t, SimpleNode node, CallStack callstack):this("TargetError", t, node, callstack, false)
		{
		}
		
		public override System.String ToString()
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return base.ToString() + "\nTarget exception: " + printTargetError(target);
		}
		
		//UPGRADE_NOTE: The equivalent of method 'java.lang.Throwable.printStackTrace' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public void  printStackTrace()
		{
			System.IO.StreamWriter temp_writer;
			temp_writer = new System.IO.StreamWriter(System.Console.OpenStandardError(), System.Console.Error.Encoding);
			temp_writer.AutoFlush = true;
			printStackTrace(false, temp_writer);
		}
		
		//UPGRADE_NOTE: The equivalent of method 'java.lang.Throwable.printStackTrace' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public void  printStackTrace(System.IO.StreamWriter out_Renamed)
		{
			printStackTrace(false, out_Renamed);
		}
		
		//UPGRADE_ISSUE: Class hierarchy differences between 'java.io.PrintStream' and 'System.IO.StreamWriter' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		public virtual void  printStackTrace(bool debug, System.IO.StreamWriter out_Renamed)
		{
			if (debug)
			{
				SupportClass.WriteStackTrace((EvalError) this, out_Renamed);
				out_Renamed.WriteLine("--- Target Stack Trace ---");
			}
			if (target is bsh.TargetError)
				((bsh.TargetError) target).printStackTrace(out_Renamed);
			else
				SupportClass.WriteStackTrace(target, out_Renamed);
		}
		
		/// <summary>Generate a printable string showing the wrapped target exception.
		/// If the proxy mechanism is available, allow the extended print to
		/// check for UndeclaredThrowableException and print that embedded error.
		/// </summary>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public virtual System.String printTargetError(System.Exception t)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.String s = target.ToString();
			
			if (Capabilities.canGenerateInterfaces())
				s += ("\n" + xPrintTargetError(t));
			
			return s;
		}
		
		/// <summary>Extended form of print target error.
		/// This indirection is used to print UndeclaredThrowableExceptions 
		/// which are possible when the proxy mechanism is available.
		/// We are shielded from compile problems by using a bsh script.
		/// This is acceptable here because we're not in a critical path...
		/// Otherwise we'd need yet another dynamically loaded module just for this.
		/// </summary>
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public virtual System.String xPrintTargetError(System.Exception t)
		{
			System.String getTarget = "import java.lang.reflect.UndeclaredThrowableException;" + "String result=\"\";" + "while ( target instanceof UndeclaredThrowableException ) {" + "	target=target.getUndeclaredThrowable(); " + "	result+=\"Nested: \"+target.toString();" + "}" + "return result;";
			Interpreter i = new Interpreter();
			try
			{
				i.set_Renamed("target", t);
				return (System.String) i.eval(getTarget);
			}
			catch (EvalError e)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Throwable.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new InterpreterError("xprintarget: " + e.ToString());
			}
		}
		
		/// <summary>Return true if the TargetError was generated from native code.
		/// e.g. if the script called into a compiled java class which threw
		/// the excpetion.  We distinguish so that we can print the stack trace
		/// for the native code case... the stack trace would not be useful if
		/// the exception was generated by the script.  e.g. if the script
		/// explicitly threw an exception... (the stack trace would simply point
		/// to the bsh internals which generated the exception).
		/// </summary>
		public virtual bool inNativeCode()
		{
			return inNativeCode_Renamed_Field;
		}
	}
}