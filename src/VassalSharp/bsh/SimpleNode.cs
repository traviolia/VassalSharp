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
	/*
	Note: great care (and lots of typing) were taken to insure that the
	namespace and interpreter references are passed on the stack and not 
	(as they were erroneously before) installed in instance variables...
	Each of these node objects must be re-entrable to allow for recursive 
	situations.
	
	The only data which should really be stored in instance vars here should 
	be parse tree data... features of the node which should never change (e.g.
	the number of arguments, etc.)
	
	Exceptions would be public fields of simple classes that just publish
	data produced by the last eval()... data that is used immediately. We'll
	try to remember to mark these as transient to highlight them.
	*/
	//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
	[Serializable]
	public class SimpleNode : Node
	{
		[Serializable]
		internal class AnonymousClassSimpleNode:SimpleNode
		{
			override public int LineNumber
			{
				get
				{
					return - 1;
				}
				
			}
			override public System.String Text
			{
				get
				{
					return "<Compiled Java Code>";
				}
				
			}
			internal AnonymousClassSimpleNode(int Param1):base(Param1)
			{
			}
			public override System.String getSourceFile()
			{
				return "<Called from Java Code>";
			}
		}
		/// <summary>Get the line number of the starting token</summary>
		virtual public int LineNumber
		{
			get
			{
				return firstToken.beginLine;
			}
			
		}
		/// <summary>Get the text of the tokens comprising this node.</summary>
		virtual public System.String Text
		{
			get
			{
				System.Text.StringBuilder text = new System.Text.StringBuilder();
				Token t = firstToken;
				while (t != null)
				{
					text.Append(t.image);
					if (!t.image.Equals("."))
						text.Append(" ");
					if (t == lastToken || t.image.Equals("{") || t.image.Equals(";"))
						break;
					t = t.next;
				}
				
				return text.ToString();
			}
			
		}
		//UPGRADE_NOTE: The initialization of  'JAVACODE' was moved to static method 'bsh.SimpleNode'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		public static SimpleNode JAVACODE;
		
		protected internal Node parent;
		protected internal Node[] children;
		protected internal int id;
		internal Token firstToken, lastToken;
		
		/// <summary>the source of the text from which this was parsed </summary>
		internal System.String sourceFile;
		
		public SimpleNode(int i)
		{
			id = i;
		}
		
		public virtual void  jjtOpen()
		{
		}
		public virtual void  jjtClose()
		{
		}
		
		public virtual void  jjtSetParent(Node n)
		{
			parent = n;
		}
		public virtual Node jjtGetParent()
		{
			return parent;
		}
		//public SimpleNode getParent() { return (SimpleNode)parent; }
		
		public virtual void  jjtAddChild(Node n, int i)
		{
			if (children == null)
				children = new Node[i + 1];
			else if (i >= children.Length)
			{
				Node[] c = new Node[i + 1];
				Array.Copy(children, 0, c, 0, children.Length);
				children = c;
			}
			
			children[i] = n;
		}
		
		public virtual Node jjtGetChild(int i)
		{
			return children[i];
		}
		public virtual SimpleNode getChild(int i)
		{
			return (SimpleNode) jjtGetChild(i);
		}
		
		public virtual int jjtGetNumChildren()
		{
			return (children == null)?0:children.Length;
		}
		
		/*
		You can override these two methods in subclasses of SimpleNode to
		customize the way the node appears when the tree is dumped.  If
		your output uses more than one line you should override
		toString(String), otherwise overriding toString() is probably all
		you need to do.
		*/
		public override System.String ToString()
		{
			return bsh.ParserTreeConstants_Fields.jjtNodeName[id];
		}
		public virtual System.String toString(System.String prefix)
		{
			return prefix + ToString();
		}
		
		/*
		Override this method if you want to customize how the node dumps
		out its children.
		*/
		public virtual void  dump(System.String prefix)
		{
			System.Console.Out.WriteLine(toString(prefix));
			if (children != null)
			{
				for (int i = 0; i < children.Length; ++i)
				{
					SimpleNode n = (SimpleNode) children[i];
					if (n != null)
					{
						n.dump(prefix + " ");
					}
				}
			}
		}
		
		//  ---- BeanShell specific stuff hereafter ----  //
		
		/// <summary>Detach this node from its parent.
		/// This is primarily useful in node serialization.
		/// (see BSHMethodDeclaration)
		/// </summary>
		public virtual void  prune()
		{
			jjtSetParent(null);
		}
		
		/// <summary>This is the general signature for evaluation of a node.</summary>
		public virtual System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			throw new InterpreterError("Unimplemented or inappropriate for " + GetType().FullName);
		}
		
		/// <summary>Set the name of the source file (or more generally source) of
		/// the text from which this node was parsed.
		/// </summary>
		public virtual void  setSourceFile(System.String sourceFile)
		{
			this.sourceFile = sourceFile;
		}
		
		/// <summary>Get the name of the source file (or more generally source) of
		/// the text from which this node was parsed.
		/// This will recursively search up the chain of parent nodes until
		/// a source is found or return a string indicating that the source
		/// is unknown.
		/// </summary>
		public virtual System.String getSourceFile()
		{
			if (sourceFile == null)
				if (parent != null)
					return ((SimpleNode) parent).getSourceFile();
				else
					return "<unknown file>";
			else
				return sourceFile;
		}
		
		/// <summary>Get the ending line number of the starting token
		/// public int getEndLineNumber() {
		/// return lastToken.endLine;
		/// }
		/// </summary>
		static SimpleNode()
		{
			JAVACODE = new AnonymousClassSimpleNode(- 1);
		}
	}
}