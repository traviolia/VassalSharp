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


/* Generated By:JJTree: Do not edit this line. Node.java */
using System;
namespace bsh
{
	
	/*
	All BSH nodes must implement this interface.  It provides basic
	machinery for constructing the parent and child relationships
	between nodes.*/
	//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
	public interface Node
	{
		/// <summary>This method is called after the node has been made the current
		/// node.  It indicates that child nodes can now be added to it.
		/// </summary>
		void  jjtOpen();
		
		/// <summary>This method is called after all the child nodes have been
		/// added.
		/// </summary>
		void  jjtClose();
		
		/// <summary>This pair of methods are used to inform the node of its
		/// parent.
		/// </summary>
		void  jjtSetParent(Node n);
		Node jjtGetParent();
		
		/// <summary>This method tells the node to add its argument to the node's
		/// list of children.
		/// </summary>
		void  jjtAddChild(Node n, int i);
		
		/// <summary>This method returns a child node.  The children are numbered
		/// from zero, left to right.
		/// </summary>
		Node jjtGetChild(int i);
		
		/// <summary>Return the number of children the node has.</summary>
		int jjtGetNumChildren();
	}
}