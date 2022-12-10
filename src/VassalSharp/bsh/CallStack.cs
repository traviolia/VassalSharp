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
	
	/// <summary>A stack of NameSpaces representing the call path.
	/// Each method invocation, for example, pushes a new NameSpace onto the stack.
	/// The top of the stack is always the current namespace of evaluation.
	/// <p>
	/// This is used to support the this.caller magic reference and to print
	/// script "stack traces" when evaluation errors occur.
	/// <p>
	/// Note: it would be awefully nice to use the java.util.Stack here.
	/// Sigh... have to stay 1.1 compatible.
	/// <p>
	/// Note: How can this be thread safe, you might ask?  Wouldn't a thread 
	/// executing various beanshell methods be mutating the callstack?  Don't we 
	/// need one CallStack per Thread in the interpreter?  The answer is that we do.
	/// Any java.lang.Thread enters our script via an external (hard) Java 
	/// reference via a This type interface, e.g.  the Runnable interface 
	/// implemented by This or an arbitrary interface implemented by XThis.  
	/// In that case the This invokeMethod() method (called by any interface that 
	/// it exposes) creates a new CallStack for each external call.
	/// <p>
	/// </summary>
	public class CallStack
	{
		private System.Collections.ArrayList stack = System.Collections.ArrayList.Synchronized(new System.Collections.ArrayList(2));
		
		public CallStack()
		{
		}
		
		public CallStack(NameSpace namespace_Renamed)
		{
			push(namespace_Renamed);
		}
		
		public virtual void  clear()
		{
			stack.Clear();
		}
		
		public virtual void  push(NameSpace ns)
		{
			stack.Insert(0, ns);
		}
		
		public virtual NameSpace top()
		{
			return get_Renamed(0);
		}
		
		/// <summary>zero based.</summary>
		public virtual NameSpace get_Renamed(int depth)
		{
			if (depth >= depth())
				return NameSpace.JAVACODE;
			else
				return (NameSpace) (stack[depth]);
		}
		
		/// <summary>This is kind of crazy, but used by the setNameSpace command.
		/// zero based.
		/// </summary>
		public virtual void  set_Renamed(int depth, NameSpace ns)
		{
			stack[depth] = ns;
		}
		
		public virtual NameSpace pop()
		{
			if (depth() < 1)
				throw new InterpreterError("pop on empty CallStack");
			NameSpace top = top();
			stack.RemoveAt(0);
			return top;
		}
		
		/// <summary>Swap in the value as the new top of the stack and return the old
		/// value.
		/// </summary>
		public virtual NameSpace swap(NameSpace newTop)
		{
			NameSpace oldTop = (NameSpace) (stack[0]);
			stack[0] = newTop;
			return oldTop;
		}
		
		public virtual int depth()
		{
			return stack.Count;
		}
		
		public virtual NameSpace[] toArray()
		{
			NameSpace[] nsa = new NameSpace[depth()];
			stack.CopyTo(nsa);
			return nsa;
		}
		
		public override System.String ToString()
		{
			System.Text.StringBuilder sb = new System.Text.StringBuilder();
			sb.Append("CallStack:\n");
			NameSpace[] nsa = toArray();
			for (int i = 0; i < nsa.Length; i++)
			{
				sb.Append("\t" + nsa[i] + "\n");
			}
			
			return sb.ToString();
		}
		
		/// <summary>Occasionally we need to freeze the callstack for error reporting
		/// purposes, etc.
		/// </summary>
		public virtual CallStack copy()
		{
			CallStack cs = new CallStack();
			cs.stack = (System.Collections.ArrayList) this.stack.Clone();
			return cs;
		}
	}
}