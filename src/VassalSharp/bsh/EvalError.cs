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
	
	/// <summary>EvalError indicates that we cannot continue evaluating the script
	/// or the script has thrown an exception.
	/// EvalError may be thrown for a script syntax error, an evaluation 
	/// error such as referring to an undefined variable, an internal error.
	/// <p>
	/// </summary>
	/// <seealso cref="TargetError">
	/// </seealso>
	[Serializable]
	public class EvalError:System.Exception
	{
		/// <summary>Print the error with line number and stack trace.</summary>
		public override System.String Message
		{
			get
			{
				System.String trace;
				if (node != null)
					trace = " : at Line: " + node.LineNumber + " : in file: " + node.getSourceFile() + " : " + node.Text;
				// Users should not normally see this.
				else
					trace = ": <at unknown location>";
				
				if (callstack != null)
					trace = trace + "\n" + ScriptStackTrace;
				
				return RawMessage + trace;
			}
			
		}
		/// <summary>The error has trace info associated with it. 
		/// i.e. It has an AST node that can print its location and source text.
		/// </summary>
		virtual internal SimpleNode Node
		{
			get
			{
				return node;
			}
			
			set
			{
				this.node = value;
			}
			
		}
		virtual public System.String ErrorText
		{
			get
			{
				if (node != null)
					return node.Text;
				else
					return "<unknown error>";
			}
			
		}
		virtual public int ErrorLineNumber
		{
			get
			{
				if (node != null)
					return node.LineNumber;
				else
					return - 1;
			}
			
		}
		virtual public System.String ScriptStackTrace
		{
			get
			{
				if (callstack == null)
					return "<Unknown>";
				
				System.String trace = "";
				CallStack stack = callstack.copy();
				while (stack.depth() > 0)
				{
					NameSpace ns = stack.pop();
					SimpleNode node = ns.Node;
					if (ns.isMethod)
					{
						trace = trace + "\nCalled from method: " + ns.Name;
						if (node != null)
							trace += (" : at Line: " + node.LineNumber + " : in file: " + node.getSourceFile() + " : " + node.Text);
					}
				}
				
				return trace;
			}
			
		}
		virtual public System.String RawMessage
		{
			get
			{
				return message;
			}
			
		}
		private SimpleNode node;
		
		// Note: no way to mutate the Throwable message, must maintain our own
		private System.String message;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'callstack '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private CallStack callstack;
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public EvalError(System.String s, SimpleNode node, CallStack callstack, System.Exception cause):this(s, node, callstack)
		{
			initCause(cause);
		}
		
		public EvalError(System.String s, SimpleNode node, CallStack callstack)
		{
			this.message = s;
			this.node = node;
			// freeze the callstack for the stack trace.
			this.callstack = callstack == null?null:callstack.copy();
		}
		
		/// <summary>Re-throw the error, prepending the specified message.</summary>
		public virtual void  reThrow(System.String msg)
		{
			prependMessage(msg);
			throw this;
		}
		
		public virtual System.String getErrorSourceFile()
		{
			if (node != null)
				return node.getSourceFile();
			else
				return "<unknown file>";
		}
		
		/// <summary>Prepend the message if it is non-null.</summary>
		private void  prependMessage(System.String s)
		{
			if (s == null)
				return ;
			
			if (message == null)
				message = s;
			else
				message = s + " : " + message;
		}
	}
}