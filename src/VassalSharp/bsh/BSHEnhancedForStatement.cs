// Just testing...
using System;
namespace bsh
{
	
	/// <summary>Implementation of the enhanced for(:) statement.  
	/// This statement uses BshIterable to support iteration over a wide variety
	/// of iterable types.  Under JDK 1.1 this statement supports primitive and 
	/// Object arrays, Vectors, and enumerations.  Under JDK 1.2 and later it 
	/// additionally supports collections.
	/// </summary>
	/// <author>  Daniel Leuck 
	/// </author>
	/// <author>  Pat Niemeyer
	/// </author>
	[Serializable]
	class BSHEnhancedForStatement:SimpleNode, ParserConstants
	{
		internal System.String varName;
		
		internal BSHEnhancedForStatement(int id):base(id)
		{
		}
		
		public override System.Object eval(CallStack callstack, Interpreter interpreter)
		{
			System.Type elementType = null;
			SimpleNode expression, statement = null;
			
			NameSpace enclosingNameSpace = callstack.top();
			SimpleNode firstNode = ((SimpleNode) jjtGetChild(0));
			int nodeCount = jjtGetNumChildren();
			
			if (firstNode is BSHType)
			{
				elementType = ((BSHType) firstNode).getType(callstack, interpreter);
				expression = ((SimpleNode) jjtGetChild(1));
				if (nodeCount > 2)
					statement = ((SimpleNode) jjtGetChild(2));
			}
			else
			{
				expression = firstNode;
				if (nodeCount > 1)
					statement = ((SimpleNode) jjtGetChild(1));
			}
			
			BlockNameSpace eachNameSpace = new BlockNameSpace(enclosingNameSpace);
			callstack.swap(eachNameSpace);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'iteratee '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Object iteratee = expression.eval(callstack, interpreter);
			
			if (iteratee == Primitive.NULL)
				throw new EvalError("The collection, array, map, iterator, or " + "enumeration portion of a for statement cannot be null.", this, callstack);
			
			CollectionManager cm = CollectionManager.getCollectionManager();
			if (!cm.isBshIterable(iteratee))
				throw new EvalError("Can't iterate over type: " + iteratee.GetType(), this, callstack);
			BshIterator iterator = cm.getBshIterator(iteratee);
			
			System.Object returnControl = Primitive.VOID;
			while (iterator.hasNext())
			{
				try
				{
					if (elementType != null)
						eachNameSpace.setTypedVariable(varName, elementType, iterator.next(), new Modifiers());
					else
						eachNameSpace.setVariable(varName, iterator.next(), false);
				}
				catch (UtilEvalError e)
				{
					throw e.toEvalError("for loop iterator variable:" + varName, this, callstack);
				}
				
				bool breakout = false; // switch eats a multi-level break here?
				if (statement != null)
				// not empty statement
				{
					System.Object ret = statement.eval(callstack, interpreter);
					
					if (ret is ReturnControl)
					{
						switch (((ReturnControl) ret).kind)
						{
							
							case bsh.ParserConstants_Fields.RETURN: 
								returnControl = ret;
								breakout = true;
								break;
							
							
							case bsh.ParserConstants_Fields.CONTINUE: 
								break;
							
							
							case bsh.ParserConstants_Fields.BREAK: 
								breakout = true;
								break;
							}
					}
				}
				
				if (breakout)
					break;
			}
			
			callstack.swap(enclosingNameSpace);
			return returnControl;
		}
	}
}