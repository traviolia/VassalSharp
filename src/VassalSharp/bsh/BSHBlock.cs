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
	
	//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
	[Serializable]
	public class BSHBlock:SimpleNode
	{
		public bool isSynchronized = false;
		
		internal BSHBlock(int id):base(id)
		{
		}
		
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			return eval(callstack, interpreter, false);
		}
		
		/// <param name="overrideNamespace">if set to true the block will be executed
		/// in the current namespace (not a subordinate one).
		/// <p>
		/// If true *no* new BlockNamespace will be swapped onto the stack and 
		/// the eval will happen in the current
		/// top namespace.  This is used by BshMethod, TryStatement, etc.  
		/// which must intialize the block first and also for those that perform 
		/// multiple passes in the same block.
		/// </param>
		public virtual System.Object eval(CallStack callstack, Interpreter interpreter, bool overrideNamespace)
		{
			System.Object syncValue = null;
			if (isSynchronized)
			{
				// First node is the expression on which to sync
				SimpleNode exp = ((SimpleNode) jjtGetChild(0));
				syncValue = exp.eval(callstack, interpreter);
			}
			
			System.Object ret;
			if (isSynchronized)
			// Do the actual synchronization
				lock (syncValue)
				{
					ret = evalBlock(callstack, interpreter, overrideNamespace, null);
				}
			else
				ret = evalBlock(callstack, interpreter, overrideNamespace, null);
			
			return ret;
		}
		
		internal virtual System.Object evalBlock(CallStack callstack, Interpreter interpreter, bool overrideNamespace, BSHBlock.NodeFilter nodeFilter)
		{
			System.Object ret = Primitive.VOID;
			NameSpace enclosingNameSpace = null;
			if (!overrideNamespace)
			{
				enclosingNameSpace = callstack.top();
				BlockNameSpace bodyNameSpace = new BlockNameSpace(enclosingNameSpace);
				
				callstack.swap(bodyNameSpace);
			}
			
			int startChild = isSynchronized?1:0;
			int numChildren = jjtGetNumChildren();
			
			try
			{
				/*
				Evaluate block in two passes: 
				First do class declarations then do everything else.
				*/
				for (int i = startChild; i < numChildren; i++)
				{
					SimpleNode node = ((SimpleNode) jjtGetChild(i));
					
					if (nodeFilter != null && !nodeFilter.isVisible(node))
						continue;
					
					if (node is BSHClassDeclaration)
						node.eval(callstack, interpreter);
				}
				for (int i = startChild; i < numChildren; i++)
				{
					SimpleNode node = ((SimpleNode) jjtGetChild(i));
					if (node is BSHClassDeclaration)
						continue;
					
					// filter nodes
					if (nodeFilter != null && !nodeFilter.isVisible(node))
						continue;
					
					ret = node.eval(callstack, interpreter);
					
					// statement or embedded block evaluated a return statement
					if (ret is ReturnControl)
						break;
				}
			}
			finally
			{
				// make sure we put the namespace back when we leave.
				if (!overrideNamespace)
					callstack.swap(enclosingNameSpace);
			}
			return ret;
		}
		
		public interface NodeFilter
		{
			bool isVisible(SimpleNode node);
		}
	}
}