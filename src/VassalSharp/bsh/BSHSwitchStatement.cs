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
	
	[Serializable]
	class BSHSwitchStatement:SimpleNode, ParserConstants
	{
		
		public BSHSwitchStatement(int id):base(id)
		{
		}
		
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			int numchild = jjtGetNumChildren();
			int child = 0;
			SimpleNode switchExp = ((SimpleNode) jjtGetChild(child++));
			System.Object switchVal = switchExp.eval(callstack, interpreter);
			
			/*
			Note: this could be made clearer by adding an inner class for the
			cases and an object context for the child traversal.
			*/
			// first label
			BSHSwitchLabel label;
			System.Object node;
			ReturnControl returnControl = null;
			
			// get the first label
			if (child >= numchild)
				throw new EvalError("Empty switch statement.", this, callstack);
			label = ((BSHSwitchLabel) jjtGetChild(child++));
			
			// while more labels or blocks and haven't hit return control
			while (child < numchild && returnControl == null)
			{
				// if label is default or equals switchVal
				if (label.isDefault || primitiveEquals(switchVal, label.eval(callstack, interpreter), callstack, switchExp))
				{
					// execute nodes, skipping labels, until a break or return
					while (child < numchild)
					{
						node = jjtGetChild(child++);
						if (node is BSHSwitchLabel)
							continue;
						// eval it
						System.Object value_Renamed = ((SimpleNode) node).eval(callstack, interpreter);
						
						// should check to disallow continue here?
						if (value_Renamed is ReturnControl)
						{
							returnControl = (ReturnControl) value_Renamed;
							break;
						}
					}
				}
				else
				{
					// skip nodes until next label
					while (child < numchild)
					{
						node = jjtGetChild(child++);
						if (node is BSHSwitchLabel)
						{
							label = (BSHSwitchLabel) node;
							break;
						}
					}
				}
			}
			
			if (returnControl != null && returnControl.kind == bsh.ParserConstants_Fields.RETURN)
				return returnControl;
			else
				return Primitive.VOID;
		}
		
		/// <summary>Helper method for testing equals on two primitive or boxable objects.
		/// yuck: factor this out into Primitive.java
		/// </summary>
		private bool primitiveEquals(System.Object switchVal, System.Object targetVal, CallStack callstack, SimpleNode switchExp)
		{
			if (switchVal is Primitive || targetVal is Primitive)
				try
				{
					// binaryOperation can return Primitive or wrapper type 
					System.Object result = Primitive.binaryOperation(switchVal, targetVal, bsh.ParserConstants_Fields.EQ);
					result = Primitive.unwrap(result);
					return result.Equals(true);
				}
				catch (UtilEvalError e)
				{
					throw e.toEvalError("Switch value: " + switchExp.Text + ": ", this, callstack);
				}
			else
				return switchVal.Equals(targetVal);
		}
	}
}