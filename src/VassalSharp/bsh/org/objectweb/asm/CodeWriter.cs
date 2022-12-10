/// <summary> 
/// ASM: a very small and fast Java bytecode manipulation framework
/// Copyright (C) 2000 INRIA, France Telecom
/// Copyright (C) 2002 France Telecom
/// 
/// This library is free software; you can redistribute it and/or
/// modify it under the terms of the GNU Lesser General Public
/// License as published by the Free Software Foundation; either
/// version 2 of the License, or (at your option) any later version.
/// 
/// This library is distributed in the hope that it will be useful,
/// but WITHOUT ANY WARRANTY; without even the implied warranty of
/// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
/// Lesser General Public License for more details.
/// 
/// You should have received a copy of the GNU Lesser General Public
/// License along with this library; if not, write to the Free Software
/// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
/// 
/// Contact: Eric.Bruneton@rd.francetelecom.com
/// 
/// Author: Eric Bruneton
/// </summary>
using System;
namespace bsh.org.objectweb.asm
{
	
	/// <summary> A {@link CodeVisitor CodeVisitor} that generates Java bytecode instructions.
	/// Each visit method of this class appends the bytecode corresponding to the
	/// visited instruction to a byte vector, in the order these methods are called.
	/// </summary>
	
	public class CodeWriter : CodeVisitor
	{
		/// <summary> Returns the size of the bytecode of this method.
		/// 
		/// </summary>
		/// <returns> the size of the bytecode of this method.
		/// </returns>
		virtual internal int Size
		{
			
			
			get
			{
				if (resize)
				{
					// replaces the temporary jump opcodes introduced by Label.resolve.
					resizeInstructions(new int[0], new int[0], 0);
				}
				int size = 8;
				if (code.length > 0)
				{
					cw.newUTF8("Code");
					size += 18 + code.length + 8 * catchCount;
					if (localVar != null)
					{
						size += 8 + localVar.length;
					}
					if (lineNumber != null)
					{
						size += 8 + lineNumber.length;
					}
				}
				if (exceptionCount > 0)
				{
					cw.newUTF8("Exceptions");
					size += 8 + 2 * exceptionCount;
				}
				if ((access & bsh.org.objectweb.asm.Constants_Fields.ACC_SYNTHETIC) != 0)
				{
					cw.newUTF8("Synthetic");
					size += 6;
				}
				if ((access & bsh.org.objectweb.asm.Constants_Fields.ACC_DEPRECATED) != 0)
				{
					cw.newUTF8("Deprecated");
					size += 6;
				}
				return size;
			}
			
		}
		/// <summary> Returns the current size of the bytecode of this method. This size just
		/// includes the size of the bytecode instructions: it does not include the
		/// size of the Exceptions, LocalVariableTable, LineNumberTable, Synthetic
		/// and Deprecated attributes, if present.
		/// 
		/// </summary>
		/// <returns> the current size of the bytecode of this method.
		/// </returns>
		virtual protected internal int CodeSize
		{
			
			
			get
			{
				return code.length;
			}
			
		}
		/// <summary> Returns the current bytecode of this method. This bytecode only contains
		/// the instructions: it does not include the Exceptions, LocalVariableTable,
		/// LineNumberTable, Synthetic and Deprecated attributes, if present.
		/// 
		/// </summary>
		/// <returns> the current bytecode of this method. The bytecode is contained
		/// between the index 0 (inclusive) and the index {@link #getCodeSize
		/// getCodeSize} (exclusive).
		/// </returns>
		virtual protected internal sbyte[] Code
		{
			
			
			get
			{
				return code.data;
			}
			
		}
		
		/// <summary> <tt>true</tt> if preconditions must be checked at runtime or not.</summary>
		
		internal const bool CHECK = false;
		
		/// <summary> Next code writer (see {@link ClassWriter#firstMethod firstMethod}).</summary>
		
		internal CodeWriter next;
		
		/// <summary> The class writer to which this method must be added.</summary>
		
		private ClassWriter cw;
		
		/// <summary> The constant pool item that contains the name of this method.</summary>
		
		private Item name;
		
		/// <summary> The constant pool item that contains the descriptor of this method.</summary>
		
		private Item desc;
		
		/// <summary> Access flags of this method.</summary>
		
		private int access;
		
		/// <summary> Maximum stack size of this method.</summary>
		
		private int maxStack;
		
		/// <summary> Maximum number of local variables for this method.</summary>
		
		private int maxLocals;
		
		/// <summary> The bytecode of this method.</summary>
		
		private ByteVector code = new ByteVector();
		
		/// <summary> Number of entries in the catch table of this method.</summary>
		
		private int catchCount;
		
		/// <summary> The catch table of this method.</summary>
		
		private ByteVector catchTable;
		
		/// <summary> Number of exceptions that can be thrown by this method.</summary>
		
		private int exceptionCount;
		
		/// <summary> The exceptions that can be thrown by this method. More
		/// precisely, this array contains the indexes of the constant pool items
		/// that contain the internal names of these exception classes.
		/// </summary>
		
		private int[] exceptions;
		
		/// <summary> Number of entries in the LocalVariableTable attribute.</summary>
		
		private int localVarCount;
		
		/// <summary> The LocalVariableTable attribute.</summary>
		
		private ByteVector localVar;
		
		/// <summary> Number of entries in the LineNumberTable attribute.</summary>
		
		private int lineNumberCount;
		
		/// <summary> The LineNumberTable attribute.</summary>
		
		private ByteVector lineNumber;
		
		/// <summary> Indicates if some jump instructions are too small and need to be resized.</summary>
		
		private bool resize;
		
		// --------------------------------------------------------------------------
		// Fields for the control flow graph analysis algorithm (used to compute the
		// maximum stack size). A control flow graph contains one node per "basic
		// block", and one edge per "jump" from one basic block to another. Each node
		// (i.e., each basic block) is represented by the Label object that
		// corresponds to the first instruction of this basic block. Each node also
		// stores the list of its successors in the graph, as a linked list of Edge
		// objects.
		// --------------------------------------------------------------------------
		
		/// <summary> <tt>true</tt> if the maximum stack size and number of local variables must
		/// be automatically computed.
		/// </summary>
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'computeMaxs '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private bool computeMaxs;
		
		/// <summary> The (relative) stack size after the last visited instruction. This size is
		/// relative to the beginning of the current basic block, i.e., the true stack
		/// size after the last visited instruction is equal to the {@link
		/// Label#beginStackSize beginStackSize} of the current basic block plus
		/// <tt>stackSize</tt>.
		/// </summary>
		
		private int stackSize;
		
		/// <summary> The (relative) maximum stack size after the last visited instruction. This
		/// size is relative to the beginning of the current basic block, i.e., the
		/// true maximum stack size after the last visited instruction is equal to the
		/// {@link Label#beginStackSize beginStackSize} of the current basic block plus
		/// <tt>stackSize</tt>.
		/// </summary>
		
		private int maxStackSize;
		
		/// <summary> The current basic block. This block is the basic block to which the next
		/// instruction to be visited must be added.
		/// </summary>
		
		private Label currentBlock;
		
		/// <summary> The basic block stack used by the control flow analysis algorithm. This
		/// stack is represented by a linked list of {@link Label Label} objects,
		/// linked to each other by their {@link Label#next} field. This stack must
		/// not be confused with the JVM stack used to execute the JVM instructions!
		/// </summary>
		
		private Label blockStack;
		
		/// <summary> The stack size variation corresponding to each JVM instruction. This stack
		/// variation is equal to the size of the values produced by an instruction,
		/// minus the size of the values consumed by this instruction.
		/// </summary>
		
		private static int[] SIZE;
		
		// --------------------------------------------------------------------------
		// Fields to optimize the creation of {@link Edge Edge} objects by using a
		// pool of reusable objects. The (shared) pool is a linked list of Edge
		// objects, linked to each other by their {@link Edge#poolNext} field. Each
		// time a CodeWriter needs to allocate an Edge, it removes the first Edge
		// of the pool and adds it to a private list of Edge objects. After the end
		// of the control flow analysis algorithm, the Edge objects in the private
		// list of the CodeWriter are added back to the pool (by appending this
		// private list to the pool list; in order to do this in constant time, both
		// head and tail of the private list are stored in this CodeWriter).
		// --------------------------------------------------------------------------
		
		/// <summary> The head of the list of {@link Edge Edge} objects used by this {@link
		/// CodeWriter CodeWriter}. These objects, linked to each other by their
		/// {@link Edge#poolNext} field, are added back to the shared pool at the
		/// end of the control flow analysis algorithm.
		/// </summary>
		
		private Edge head;
		
		/// <summary> The tail of the list of {@link Edge Edge} objects used by this {@link
		/// CodeWriter CodeWriter}. These objects, linked to each other by their
		/// {@link Edge#poolNext} field, are added back to the shared pool at the
		/// end of the control flow analysis algorithm.
		/// </summary>
		
		private Edge tail;
		
		/// <summary> The shared pool of {@link Edge Edge} objects. This pool is a linked list
		/// of Edge objects, linked to each other by their {@link Edge#poolNext} field.
		/// </summary>
		
		private static Edge pool;
		
		// --------------------------------------------------------------------------
		// Static initializer
		// --------------------------------------------------------------------------
		
		// --------------------------------------------------------------------------
		// Constructor
		// --------------------------------------------------------------------------
		
		/// <summary> Constructs a CodeWriter.
		/// 
		/// </summary>
		/// <param name="cw">the class writer in which the method must be added.
		/// </param>
		/// <param name="computeMaxs"><tt>true</tt> if the maximum stack size and number of
		/// local variables must be automatically computed.
		/// </param>
		
		protected internal CodeWriter(ClassWriter cw, bool computeMaxs)
		{
			if (cw.firstMethod == null)
			{
				cw.firstMethod = this;
				cw.lastMethod = this;
			}
			else
			{
				cw.lastMethod.next = this;
				cw.lastMethod = this;
			}
			this.cw = cw;
			this.computeMaxs = computeMaxs;
			if (computeMaxs)
			{
				// pushes the first block onto the stack of blocks to be visited
				currentBlock = new Label();
				currentBlock.pushed = true;
				blockStack = currentBlock;
			}
		}
		
		/// <summary> Initializes this CodeWriter to define the bytecode of the specified method.
		/// 
		/// </summary>
		/// <param name="access">the method's access flags (see {@link Constants}).
		/// </param>
		/// <param name="name">the method's name.
		/// </param>
		/// <param name="desc">the method's descriptor (see {@link Type Type}).
		/// </param>
		/// <param name="exceptions">the internal names of the method's exceptions. May be
		/// <tt>null</tt>.
		/// </param>
		
		protected internal virtual void  init(int access, System.String name, System.String desc, System.String[] exceptions)
		{
			this.access = access;
			this.name = cw.newUTF8(name);
			this.desc = cw.newUTF8(desc);
			if (exceptions != null && exceptions.Length > 0)
			{
				exceptionCount = exceptions.Length;
				this.exceptions = new int[exceptionCount];
				for (int i = 0; i < exceptionCount; ++i)
				{
					this.exceptions[i] = cw.newClass(exceptions[i]).index;
				}
			}
			if (computeMaxs)
			{
				// updates maxLocals
				int size = getArgumentsAndReturnSizes(desc) >> 2;
				if ((access & bsh.org.objectweb.asm.Constants_Fields.ACC_STATIC) != 0)
				{
					--size;
				}
				if (size > maxLocals)
				{
					maxLocals = size;
				}
			}
		}
		
		// --------------------------------------------------------------------------
		// Implementation of the CodeVisitor interface
		// --------------------------------------------------------------------------
		
		public virtual void  visitInsn(int opcode)
		{
			if (computeMaxs)
			{
				// updates current and max stack sizes
				int size = stackSize + SIZE[opcode];
				if (size > maxStackSize)
				{
					maxStackSize = size;
				}
				stackSize = size;
				// if opcode == ATHROW or xRETURN, ends current block (no successor)
				if ((opcode >= bsh.org.objectweb.asm.Constants_Fields.IRETURN && opcode <= bsh.org.objectweb.asm.Constants_Fields.RETURN) || opcode == bsh.org.objectweb.asm.Constants_Fields.ATHROW)
				{
					if (currentBlock != null)
					{
						currentBlock.maxStackSize = maxStackSize;
						currentBlock = null;
					}
				}
			}
			// adds the instruction to the bytecode of the method
			code.put1(opcode);
		}
		
		public virtual void  visitIntInsn(int opcode, int operand)
		{
			if (computeMaxs && opcode != bsh.org.objectweb.asm.Constants_Fields.NEWARRAY)
			{
				// updates current and max stack sizes only if opcode == NEWARRAY
				// (stack size variation = 0 for BIPUSH or SIPUSH)
				int size = stackSize + 1;
				if (size > maxStackSize)
				{
					maxStackSize = size;
				}
				stackSize = size;
			}
			// adds the instruction to the bytecode of the method
			if (opcode == bsh.org.objectweb.asm.Constants_Fields.SIPUSH)
			{
				code.put12(opcode, operand);
			}
			else
			{
				// BIPUSH or NEWARRAY
				code.put11(opcode, operand);
			}
		}
		
		public virtual void  visitVarInsn(int opcode, int var)
		{
			if (computeMaxs)
			{
				// updates current and max stack sizes
				if (opcode == bsh.org.objectweb.asm.Constants_Fields.RET)
				{
					// no stack change, but end of current block (no successor)
					if (currentBlock != null)
					{
						currentBlock.maxStackSize = maxStackSize;
						currentBlock = null;
					}
				}
				else
				{
					// xLOAD or xSTORE
					int size = stackSize + SIZE[opcode];
					if (size > maxStackSize)
					{
						maxStackSize = size;
					}
					stackSize = size;
				}
				// updates max locals
				int n;
				if (opcode == bsh.org.objectweb.asm.Constants_Fields.LLOAD || opcode == bsh.org.objectweb.asm.Constants_Fields.DLOAD || opcode == bsh.org.objectweb.asm.Constants_Fields.LSTORE || opcode == bsh.org.objectweb.asm.Constants_Fields.DSTORE)
				{
					n = var + 2;
				}
				else
				{
					n = var + 1;
				}
				if (n > maxLocals)
				{
					maxLocals = n;
				}
			}
			// adds the instruction to the bytecode of the method
			if (var < 4 && opcode != bsh.org.objectweb.asm.Constants_Fields.RET)
			{
				int opt;
				if (opcode < bsh.org.objectweb.asm.Constants_Fields.ISTORE)
				{
					opt = 26 + ((opcode - bsh.org.objectweb.asm.Constants_Fields.ILOAD) << 2) + var;
				}
				else
				{
					opt = 59 + ((opcode - bsh.org.objectweb.asm.Constants_Fields.ISTORE) << 2) + var;
				}
				code.put1(opt);
			}
			else if (var >= 256)
			{
				code.put1(196).put12(opcode, var);
			}
			else
			{
				code.put11(opcode, var);
			}
		}
		
		public virtual void  visitTypeInsn(int opcode, System.String desc)
		{
			if (computeMaxs && opcode == bsh.org.objectweb.asm.Constants_Fields.NEW)
			{
				// updates current and max stack sizes only if opcode == NEW
				// (stack size variation = 0 for ANEWARRAY, CHECKCAST, INSTANCEOF)
				int size = stackSize + 1;
				if (size > maxStackSize)
				{
					maxStackSize = size;
				}
				stackSize = size;
			}
			// adds the instruction to the bytecode of the method
			code.put12(opcode, cw.newClass(desc).index);
		}
		
		public virtual void  visitFieldInsn(int opcode, System.String owner, System.String name, System.String desc)
		{
			if (computeMaxs)
			{
				int size;
				// computes the stack size variation
				char c = desc[0];
				switch (opcode)
				{
					
					case bsh.org.objectweb.asm.Constants_Fields.GETSTATIC: 
						size = stackSize + (c == 'D' || c == 'J'?2:1);
						break;
					
					case bsh.org.objectweb.asm.Constants_Fields.PUTSTATIC: 
						size = stackSize + (c == 'D' || c == 'J'?- 2:- 1);
						break;
					
					case bsh.org.objectweb.asm.Constants_Fields.GETFIELD: 
						size = stackSize + (c == 'D' || c == 'J'?1:0);
						break;
						//case Constants.PUTFIELD:
					
					default: 
						size = stackSize + (c == 'D' || c == 'J'?- 3:- 2);
						break;
					
				}
				// updates current and max stack sizes
				if (size > maxStackSize)
				{
					maxStackSize = size;
				}
				stackSize = size;
			}
			// adds the instruction to the bytecode of the method
			code.put12(opcode, cw.newField(owner, name, desc).index);
		}
		
		public virtual void  visitMethodInsn(int opcode, System.String owner, System.String name, System.String desc)
		{
			Item i;
			if (opcode == bsh.org.objectweb.asm.Constants_Fields.INVOKEINTERFACE)
			{
				i = cw.newItfMethod(owner, name, desc);
			}
			else
			{
				i = cw.newMethod(owner, name, desc);
			}
			int argSize = i.intVal;
			if (computeMaxs)
			{
				// computes the stack size variation. In order not to recompute several
				// times this variation for the same Item, we use the intVal field of
				// this item to store this variation, once it has been computed. More
				// precisely this intVal field stores the sizes of the arguments and of
				// the return value corresponding to desc.
				if (argSize == 0)
				{
					// the above sizes have not been computed yet, so we compute them...
					argSize = getArgumentsAndReturnSizes(desc);
					// ... and we save them in order not to recompute them in the future
					i.intVal = argSize;
				}
				int size;
				if (opcode == bsh.org.objectweb.asm.Constants_Fields.INVOKESTATIC)
				{
					size = stackSize - (argSize >> 2) + (argSize & 0x03) + 1;
				}
				else
				{
					size = stackSize - (argSize >> 2) + (argSize & 0x03);
				}
				// updates current and max stack sizes
				if (size > maxStackSize)
				{
					maxStackSize = size;
				}
				stackSize = size;
			}
			// adds the instruction to the bytecode of the method
			if (opcode == bsh.org.objectweb.asm.Constants_Fields.INVOKEINTERFACE)
			{
				if (!computeMaxs)
				{
					if (argSize == 0)
					{
						argSize = getArgumentsAndReturnSizes(desc);
						i.intVal = argSize;
					}
				}
				code.put12(bsh.org.objectweb.asm.Constants_Fields.INVOKEINTERFACE, i.index).put11(argSize >> 2, 0);
			}
			else
			{
				code.put12(opcode, i.index);
			}
		}
		
		public virtual void  visitJumpInsn(int opcode, Label label)
		{
			if (CHECK)
			{
				if (label.owner == null)
				{
					label.owner = this;
				}
				else if (label.owner != this)
				{
					throw new System.ArgumentException();
				}
			}
			if (computeMaxs)
			{
				if (opcode == bsh.org.objectweb.asm.Constants_Fields.GOTO)
				{
					// no stack change, but end of current block (with one new successor)
					if (currentBlock != null)
					{
						currentBlock.maxStackSize = maxStackSize;
						addSuccessor(stackSize, label);
						currentBlock = null;
					}
				}
				else if (opcode == bsh.org.objectweb.asm.Constants_Fields.JSR)
				{
					if (currentBlock != null)
					{
						addSuccessor(stackSize + 1, label);
					}
				}
				else
				{
					// updates current stack size (max stack size unchanged because stack
					// size variation always negative in this case)
					stackSize += SIZE[opcode];
					if (currentBlock != null)
					{
						addSuccessor(stackSize, label);
					}
				}
			}
			// adds the instruction to the bytecode of the method
			if (label.resolved && label.position - code.length < System.Int16.MinValue)
			{
				// case of a backward jump with an offset < -32768. In this case we
				// automatically replace GOTO with GOTO_W, JSR with JSR_W and IFxxx <l>
				// with IFNOTxxx <l'> GOTO_W <l>, where IFNOTxxx is the "opposite" opcode
				// of IFxxx (i.e., IFNE for IFEQ) and where <l'> designates the
				// instruction just after the GOTO_W.
				if (opcode == bsh.org.objectweb.asm.Constants_Fields.GOTO)
				{
					code.put1(200); // GOTO_W
				}
				else if (opcode == bsh.org.objectweb.asm.Constants_Fields.JSR)
				{
					code.put1(201); // JSR_W
				}
				else
				{
					code.put1(opcode <= 166?((opcode + 1) ^ 1) - 1:opcode ^ 1);
					code.put2(8); // jump offset
					code.put1(200); // GOTO_W
				}
				label.put(this, code, code.length - 1, true);
			}
			else
			{
				// case of a backward jump with an offset >= -32768, or of a forward jump
				// with, of course, an unknown offset. In these cases we store the offset
				// in 2 bytes (which will be increased in resizeInstructions, if needed).
				code.put1(opcode);
				label.put(this, code, code.length - 1, false);
			}
		}
		
		public virtual void  visitLabel(Label label)
		{
			if (CHECK)
			{
				if (label.owner == null)
				{
					label.owner = this;
				}
				else if (label.owner != this)
				{
					throw new System.ArgumentException();
				}
			}
			if (computeMaxs)
			{
				if (currentBlock != null)
				{
					// ends current block (with one new successor)
					currentBlock.maxStackSize = maxStackSize;
					addSuccessor(stackSize, label);
				}
				// begins a new current block,
				// resets the relative current and max stack sizes
				currentBlock = label;
				stackSize = 0;
				maxStackSize = 0;
			}
			// resolves previous forward references to label, if any
			resize |= label.resolve(this, code.length, code.data);
		}
		
		public virtual void  visitLdcInsn(System.Object cst)
		{
			Item i = cw.newCst(cst);
			if (computeMaxs)
			{
				int size;
				// computes the stack size variation
				if (i.type == ClassWriter.LONG || i.type == ClassWriter.DOUBLE)
				{
					size = stackSize + 2;
				}
				else
				{
					size = stackSize + 1;
				}
				// updates current and max stack sizes
				if (size > maxStackSize)
				{
					maxStackSize = size;
				}
				stackSize = size;
			}
			// adds the instruction to the bytecode of the method
			int index = i.index;
			if (i.type == ClassWriter.LONG || i.type == ClassWriter.DOUBLE)
			{
				code.put12(20, index);
			}
			else if (index >= 256)
			{
				code.put12(19, index);
			}
			else
			{
				code.put11(bsh.org.objectweb.asm.Constants_Fields.LDC, index);
			}
		}
		
		public virtual void  visitIincInsn(int var, int increment)
		{
			if (computeMaxs)
			{
				// updates max locals only (no stack change)
				int n = var + 1;
				if (n > maxLocals)
				{
					maxLocals = n;
				}
			}
			// adds the instruction to the bytecode of the method
			if ((var > 255) || (increment > 127) || (increment < - 128))
			{
				code.put1(196).put12(bsh.org.objectweb.asm.Constants_Fields.IINC, var).put2(increment);
			}
			else
			{
				code.put1(bsh.org.objectweb.asm.Constants_Fields.IINC).put11(var, increment);
			}
		}
		
		public virtual void  visitTableSwitchInsn(int min, int max, Label dflt, Label[] labels)
		{
			if (computeMaxs)
			{
				// updates current stack size (max stack size unchanged)
				--stackSize;
				// ends current block (with many new successors)
				if (currentBlock != null)
				{
					currentBlock.maxStackSize = maxStackSize;
					addSuccessor(stackSize, dflt);
					for (int i = 0; i < labels.Length; ++i)
					{
						addSuccessor(stackSize, labels[i]);
					}
					currentBlock = null;
				}
			}
			// adds the instruction to the bytecode of the method
			int source = code.length;
			code.put1(bsh.org.objectweb.asm.Constants_Fields.TABLESWITCH);
			while (code.length % 4 != 0)
			{
				code.put1(0);
			}
			dflt.put(this, code, source, true);
			code.put4(min).put4(max);
			for (int i = 0; i < labels.Length; ++i)
			{
				labels[i].put(this, code, source, true);
			}
		}
		
		public virtual void  visitLookupSwitchInsn(Label dflt, int[] keys, Label[] labels)
		{
			if (computeMaxs)
			{
				// updates current stack size (max stack size unchanged)
				--stackSize;
				// ends current block (with many new successors)
				if (currentBlock != null)
				{
					currentBlock.maxStackSize = maxStackSize;
					addSuccessor(stackSize, dflt);
					for (int i = 0; i < labels.Length; ++i)
					{
						addSuccessor(stackSize, labels[i]);
					}
					currentBlock = null;
				}
			}
			// adds the instruction to the bytecode of the method
			int source = code.length;
			code.put1(bsh.org.objectweb.asm.Constants_Fields.LOOKUPSWITCH);
			while (code.length % 4 != 0)
			{
				code.put1(0);
			}
			dflt.put(this, code, source, true);
			code.put4(labels.Length);
			for (int i = 0; i < labels.Length; ++i)
			{
				code.put4(keys[i]);
				labels[i].put(this, code, source, true);
			}
		}
		
		public virtual void  visitMultiANewArrayInsn(System.String desc, int dims)
		{
			if (computeMaxs)
			{
				// updates current stack size (max stack size unchanged because stack
				// size variation always negative or null)
				stackSize += 1 - dims;
			}
			// adds the instruction to the bytecode of the method
			Item classItem = cw.newClass(desc);
			code.put12(bsh.org.objectweb.asm.Constants_Fields.MULTIANEWARRAY, classItem.index).put1(dims);
		}
		
		public virtual void  visitTryCatchBlock(Label start, Label end, Label handler, System.String type)
		{
			if (CHECK)
			{
				if (start.owner != this || end.owner != this || handler.owner != this)
				{
					throw new System.ArgumentException();
				}
				if (!start.resolved || !end.resolved || !handler.resolved)
				{
					throw new System.ArgumentException();
				}
			}
			if (computeMaxs)
			{
				// pushes handler block onto the stack of blocks to be visited
				if (!handler.pushed)
				{
					handler.beginStackSize = 1;
					handler.pushed = true;
					handler.next = blockStack;
					blockStack = handler;
				}
			}
			++catchCount;
			if (catchTable == null)
			{
				catchTable = new ByteVector();
			}
			catchTable.put2(start.position);
			catchTable.put2(end.position);
			catchTable.put2(handler.position);
			catchTable.put2(type != null?cw.newClass(type).index:0);
		}
		
		public virtual void  visitMaxs(int maxStack, int maxLocals)
		{
			if (computeMaxs)
			{
				// true (non relative) max stack size
				int max = 0;
				// control flow analysis algorithm: while the block stack is not empty,
				// pop a block from this stack, update the max stack size, compute
				// the true (non relative) begin stack size of the successors of this
				// block, and push these successors onto the stack (unless they have
				// already been pushed onto the stack). Note: by hypothesis, the {@link
				// Label#beginStackSize} of the blocks in the block stack are the true
				// (non relative) beginning stack sizes of these blocks.
				Label stack = blockStack;
				while (stack != null)
				{
					// pops a block from the stack
					Label l = stack;
					stack = stack.next;
					// computes the true (non relative) max stack size of this block
					int start = l.beginStackSize;
					int blockMax = start + l.maxStackSize;
					// updates the global max stack size
					if (blockMax > max)
					{
						max = blockMax;
					}
					// analyses the successors of the block
					Edge b = l.successors;
					while (b != null)
					{
						l = b.successor;
						// if this successor has not already been pushed onto the stack...
						if (!l.pushed)
						{
							// computes the true beginning stack size of this successor block
							l.beginStackSize = start + b.stackSize;
							// pushes this successor onto the stack
							l.pushed = true;
							l.next = stack;
							stack = l;
						}
						b = b.next;
					}
				}
				this.maxStack = max;
				// releases all the Edge objects used by this CodeWriter
				lock (SIZE)
				{
					// appends the [head ... tail] list at the beginning of the pool list
					if (tail != null)
					{
						tail.poolNext = pool;
						pool = head;
					}
				}
			}
			else
			{
				this.maxStack = maxStack;
				this.maxLocals = maxLocals;
			}
		}
		
		public virtual void  visitLocalVariable(System.String name, System.String desc, Label start, Label end, int index)
		{
			if (CHECK)
			{
				if (start.owner != this || !start.resolved)
				{
					throw new System.ArgumentException();
				}
				if (end.owner != this || !end.resolved)
				{
					throw new System.ArgumentException();
				}
			}
			if (localVar == null)
			{
				cw.newUTF8("LocalVariableTable");
				localVar = new ByteVector();
			}
			++localVarCount;
			localVar.put2(start.position);
			localVar.put2(end.position - start.position);
			localVar.put2(cw.newUTF8(name).index);
			localVar.put2(cw.newUTF8(desc).index);
			localVar.put2(index);
		}
		
		public virtual void  visitLineNumber(int line, Label start)
		{
			if (CHECK)
			{
				if (start.owner != this || !start.resolved)
				{
					throw new System.ArgumentException();
				}
			}
			if (lineNumber == null)
			{
				cw.newUTF8("LineNumberTable");
				lineNumber = new ByteVector();
			}
			++lineNumberCount;
			lineNumber.put2(start.position);
			lineNumber.put2(line);
		}
		
		// --------------------------------------------------------------------------
		// Utility methods: control flow analysis algorithm
		// --------------------------------------------------------------------------
		
		/// <summary> Computes the size of the arguments and of the return value of a method.
		/// 
		/// </summary>
		/// <param name="desc">the descriptor of a method.
		/// </param>
		/// <returns> the size of the arguments of the method (plus one for the implicit
		/// this argument), argSize, and the size of its return value, retSize,
		/// packed into a single int i = <tt>(argSize << 2) | retSize</tt>
		/// (argSize is therefore equal to <tt>i >> 2</tt>, and retSize to
		/// <tt>i & 0x03</tt>).
		/// </returns>
		
		private static int getArgumentsAndReturnSizes(System.String desc)
		{
			int n = 1;
			int c = 1;
			while (true)
			{
				char car = desc[c++];
				if (car == ')')
				{
					car = desc[c];
					return n << 2 | (car == 'V'?0:(car == 'D' || car == 'J'?2:1));
				}
				else if (car == 'L')
				{
					while (desc[c++] != ';')
					{
					}
					n += 1;
				}
				else if (car == '[')
				{
					while ((car = desc[c]) == '[')
					{
						++c;
					}
					if (car == 'D' || car == 'J')
					{
						n -= 1;
					}
				}
				else if (car == 'D' || car == 'J')
				{
					n += 2;
				}
				else
				{
					n += 1;
				}
			}
		}
		
		/// <summary> Adds a successor to the {@link #currentBlock currentBlock} block.
		/// 
		/// </summary>
		/// <param name="stackSize">the current (relative) stack size in the current block.
		/// </param>
		/// <param name="successor">the successor block to be added to the current block.
		/// </param>
		
		private void  addSuccessor(int stackSize, Label successor)
		{
			Edge b;
			// creates a new Edge object or reuses one from the shared pool
			lock (SIZE)
			{
				if (pool == null)
				{
					b = new Edge();
				}
				else
				{
					b = pool;
					// removes b from the pool
					pool = pool.poolNext;
				}
			}
			// adds the previous Edge to the list of Edges used by this CodeWriter
			if (tail == null)
			{
				tail = b;
			}
			b.poolNext = head;
			head = b;
			// initializes the previous Edge object...
			b.stackSize = stackSize;
			b.successor = successor;
			// ...and adds it to the successor list of the currentBlock block
			b.next = currentBlock.successors;
			currentBlock.successors = b;
		}
		
		// --------------------------------------------------------------------------
		// Utility methods: dump bytecode array
		// --------------------------------------------------------------------------
		
		/// <summary> Puts the bytecode of this method in the given byte vector.
		/// 
		/// </summary>
		/// <param name="out">the byte vector into which the bytecode of this method must be
		/// copied.
		/// </param>
		
		internal void  put(ByteVector out_Renamed)
		{
			out_Renamed.put2(access).put2(name.index).put2(desc.index);
			int attributeCount = 0;
			if (code.length > 0)
			{
				++attributeCount;
			}
			if (exceptionCount > 0)
			{
				++attributeCount;
			}
			if ((access & bsh.org.objectweb.asm.Constants_Fields.ACC_SYNTHETIC) != 0)
			{
				++attributeCount;
			}
			if ((access & bsh.org.objectweb.asm.Constants_Fields.ACC_DEPRECATED) != 0)
			{
				++attributeCount;
			}
			out_Renamed.put2(attributeCount);
			if (code.length > 0)
			{
				int size = 12 + code.length + 8 * catchCount;
				if (localVar != null)
				{
					size += 8 + localVar.length;
				}
				if (lineNumber != null)
				{
					size += 8 + lineNumber.length;
				}
				out_Renamed.put2(cw.newUTF8("Code").index).put4(size);
				out_Renamed.put2(maxStack).put2(maxLocals);
				out_Renamed.put4(code.length).putByteArray(code.data, 0, code.length);
				out_Renamed.put2(catchCount);
				if (catchCount > 0)
				{
					out_Renamed.putByteArray(catchTable.data, 0, catchTable.length);
				}
				attributeCount = 0;
				if (localVar != null)
				{
					++attributeCount;
				}
				if (lineNumber != null)
				{
					++attributeCount;
				}
				out_Renamed.put2(attributeCount);
				if (localVar != null)
				{
					out_Renamed.put2(cw.newUTF8("LocalVariableTable").index);
					out_Renamed.put4(localVar.length + 2).put2(localVarCount);
					out_Renamed.putByteArray(localVar.data, 0, localVar.length);
				}
				if (lineNumber != null)
				{
					out_Renamed.put2(cw.newUTF8("LineNumberTable").index);
					out_Renamed.put4(lineNumber.length + 2).put2(lineNumberCount);
					out_Renamed.putByteArray(lineNumber.data, 0, lineNumber.length);
				}
			}
			if (exceptionCount > 0)
			{
				out_Renamed.put2(cw.newUTF8("Exceptions").index).put4(2 * exceptionCount + 2);
				out_Renamed.put2(exceptionCount);
				for (int i = 0; i < exceptionCount; ++i)
				{
					out_Renamed.put2(exceptions[i]);
				}
			}
			if ((access & bsh.org.objectweb.asm.Constants_Fields.ACC_SYNTHETIC) != 0)
			{
				out_Renamed.put2(cw.newUTF8("Synthetic").index).put4(0);
			}
			if ((access & bsh.org.objectweb.asm.Constants_Fields.ACC_DEPRECATED) != 0)
			{
				out_Renamed.put2(cw.newUTF8("Deprecated").index).put4(0);
			}
		}
		
		// --------------------------------------------------------------------------
		// Utility methods: instruction resizing (used to handle GOTO_W and JSR_W)
		// --------------------------------------------------------------------------
		
		/// <summary> Resizes the designated instructions, while keeping jump offsets and
		/// instruction addresses consistent. This may require to resize other existing
		/// instructions, or even to introduce new instructions: for example,
		/// increasing the size of an instruction by 2 at the middle of a method can
		/// increases the offset of an IFEQ instruction from 32766 to 32768, in which
		/// case IFEQ 32766 must be replaced with IFNEQ 8 GOTO_W 32765. This, in turn,
		/// may require to increase the size of another jump instruction, and so on...
		/// All these operations are handled automatically by this method.
		/// <p>
		/// <i>This method must be called after all the method that is being built has
		/// been visited</i>. In particular, the {@link Label Label} objects used to
		/// construct the method are no longer valid after this method has been called.
		/// 
		/// </summary>
		/// <param name="indexes">current positions of the instructions to be resized. Each
		/// instruction must be designated by the index of its <i>last</i> byte,
		/// plus one (or, in other words, by the index of the <i>first</i> byte of
		/// the <i>next</i> instruction).
		/// </param>
		/// <param name="sizes">the number of bytes to be <i>added</i> to the above
		/// instructions. More precisely, for each i &lt; <tt>len</tt>,
		/// <tt>sizes</tt>[i] bytes will be added at the end of the instruction
		/// designated by <tt>indexes</tt>[i] or, if <tt>sizes</tt>[i] is
		/// negative, the <i>last</i> |<tt>sizes[i]</tt>| bytes of the instruction
		/// will be removed (the instruction size <i>must not</i> become negative
		/// or null). The gaps introduced by this method must be filled in
		/// "manually" in the array returned by the {@link #getCode getCode}
		/// method.
		/// </param>
		/// <param name="len">the number of instruction to be resized. Must be smaller than or
		/// equal to <tt>indexes</tt>.length and <tt>sizes</tt>.length.
		/// </param>
		/// <returns> the <tt>indexes</tt> array, which now contains the new positions of
		/// the resized instructions (designated as above).
		/// </returns>
		
		protected internal virtual int[] resizeInstructions(int[] indexes, int[] sizes, int len)
		{
			sbyte[] b = code.data; // bytecode of the method
			int u, v, label; // indexes in b
			int i, j; // loop indexes
			
			// 1st step:
			// As explained above, resizing an instruction may require to resize another
			// one, which may require to resize yet another one, and so on. The first
			// step of the algorithm consists in finding all the instructions that
			// need to be resized, without modifying the code. This is done by the
			// following "fix point" algorithm:
			// - parse the code to find the jump instructions whose offset will need
			//   more than 2 bytes to be stored (the future offset is computed from the
			//   current offset and from the number of bytes that will be inserted or
			//   removed between the source and target instructions). For each such
			//   instruction, adds an entry in (a copy of) the indexes and sizes arrays
			//   (if this has not already been done in a previous iteration!)
			// - if at least one entry has been added during the previous step, go back
			//   to the beginning, otherwise stop.
			// In fact the real algorithm is complicated by the fact that the size of
			// TABLESWITCH and LOOKUPSWITCH instructions depends on their position in
			// the bytecode (because of padding). In order to ensure the convergence of
			// the algorithm, the number of bytes to be added or removed from these
			// instructions is over estimated during the previous loop, and computed
			// exactly only after the loop is finished (this requires another pass to
			// parse the bytecode of the method).
			
			int[] allIndexes = new int[len]; // copy of indexes
			int[] allSizes = new int[len]; // copy of sizes
			bool[] resize; // instructions to be resized
			int newOffset; // future offset of a jump instruction
			
			Array.Copy(indexes, 0, allIndexes, 0, len);
			Array.Copy(sizes, 0, allSizes, 0, len);
			resize = new bool[code.length];
			
			int state = 3; // 3 = loop again, 2 = loop ended, 1 = last pass, 0 = done
			do 
			{
				if (state == 3)
				{
					state = 2;
				}
				u = 0;
				while (u < b.Length)
				{
					int opcode = b[u] & 0xFF; // opcode of current instruction
					int insert = 0; // bytes to be added after this instruction
					
					switch (ClassWriter.TYPE[opcode])
					{
						
						case (sbyte) (ClassWriter.NOARG_INSN): 
						case (sbyte) (ClassWriter.IMPLVAR_INSN): 
							u += 1;
							break;
						
						case (sbyte) (ClassWriter.LABEL_INSN): 
							if (opcode > 201)
							{
								// converts temporary opcodes 202 to 217 (inclusive), 218 and 219
								// to IFEQ ... JSR (inclusive), IFNULL and IFNONNULL
								opcode = opcode < 218?opcode - 49:opcode - 20;
								label = u + readUnsignedShort(b, u + 1);
							}
							else
							{
								label = u + readShort(b, u + 1);
							}
							newOffset = getNewOffset(allIndexes, allSizes, u, label);
							if (newOffset < System.Int16.MinValue || newOffset > System.Int16.MaxValue)
							{
								if (!resize[u])
								{
									if (opcode == bsh.org.objectweb.asm.Constants_Fields.GOTO || opcode == bsh.org.objectweb.asm.Constants_Fields.JSR)
									{
										// two additional bytes will be required to replace this
										// GOTO or JSR instruction with a GOTO_W or a JSR_W
										insert = 2;
									}
									else
									{
										// five additional bytes will be required to replace this
										// IFxxx <l> instruction with IFNOTxxx <l'> GOTO_W <l>, where
										// IFNOTxxx is the "opposite" opcode of IFxxx (i.e., IFNE for
										// IFEQ) and where <l'> designates the instruction just after
										// the GOTO_W.
										insert = 5;
									}
									resize[u] = true;
								}
							}
							u += 3;
							break;
						
						case (sbyte) (ClassWriter.LABELW_INSN): 
							u += 5;
							break;
						
						case (sbyte) (ClassWriter.TABL_INSN): 
							if (state == 1)
							{
								// true number of bytes to be added (or removed) from this
								// instruction = (future number of padding bytes - current number
								// of padding byte) - previously over estimated variation =
								// = ((3 - newOffset%4) - (3 - u%4)) - u%4
								// = (-newOffset%4 + u%4) - u%4
								// = -(newOffset & 3)
								newOffset = getNewOffset(allIndexes, allSizes, 0, u);
								insert = - (newOffset & 3);
							}
							else if (!resize[u])
							{
								// over estimation of the number of bytes to be added to this
								// instruction = 3 - current number of padding bytes = 3 - (3 -
								// u%4) = u%4 = u & 3
								insert = u & 3;
								resize[u] = true;
							}
							// skips instruction
							u = u + 4 - (u & 3);
							u += 4 * (readInt(b, u + 8) - readInt(b, u + 4) + 1) + 12;
							break;
						
						case (sbyte) (ClassWriter.LOOK_INSN): 
							if (state == 1)
							{
								// like TABL_INSN
								newOffset = getNewOffset(allIndexes, allSizes, 0, u);
								insert = - (newOffset & 3);
							}
							else if (!resize[u])
							{
								// like TABL_INSN
								insert = u & 3;
								resize[u] = true;
							}
							// skips instruction
							u = u + 4 - (u & 3);
							u += 8 * readInt(b, u + 4) + 8;
							break;
						
						case (sbyte) (ClassWriter.WIDE_INSN): 
							opcode = b[u + 1] & 0xFF;
							if (opcode == bsh.org.objectweb.asm.Constants_Fields.IINC)
							{
								u += 6;
							}
							else
							{
								u += 4;
							}
							break;
						
						case (sbyte) (ClassWriter.VAR_INSN): 
						case (sbyte) (ClassWriter.SBYTE_INSN): 
						case (sbyte) (ClassWriter.LDC_INSN): 
							u += 2;
							break;
						
						case (sbyte) (ClassWriter.SHORT_INSN): 
						case (sbyte) (ClassWriter.LDCW_INSN): 
						case (sbyte) (ClassWriter.FIELDORMETH_INSN): 
						case (sbyte) (ClassWriter.TYPE_INSN): 
						case (sbyte) (ClassWriter.IINC_INSN): 
							u += 3;
							break;
						
						case (sbyte) (ClassWriter.ITFMETH_INSN): 
							u += 5;
							break;
							// case ClassWriter.MANA_INSN:
						
						default: 
							u += 4;
							break;
						
					}
					if (insert != 0)
					{
						// adds a new (u, insert) entry in the allIndexes and allSizes arrays
						int[] newIndexes = new int[allIndexes.Length + 1];
						int[] newSizes = new int[allSizes.Length + 1];
						Array.Copy(allIndexes, 0, newIndexes, 0, allIndexes.Length);
						Array.Copy(allSizes, 0, newSizes, 0, allSizes.Length);
						newIndexes[allIndexes.Length] = u;
						newSizes[allSizes.Length] = insert;
						allIndexes = newIndexes;
						allSizes = newSizes;
						if (insert > 0)
						{
							state = 3;
						}
					}
				}
				if (state < 3)
				{
					--state;
				}
			}
			while (state != 0);
			
			// 2nd step:
			// copies the bytecode of the method into a new bytevector, updates the
			// offsets, and inserts (or removes) bytes as requested.
			
			ByteVector newCode = new ByteVector(code.length);
			
			u = 0;
			while (u < code.length)
			{
				for (i = allIndexes.Length - 1; i >= 0; --i)
				{
					if (allIndexes[i] == u)
					{
						if (i < len)
						{
							if (sizes[i] > 0)
							{
								newCode.putByteArray(null, 0, sizes[i]);
							}
							else
							{
								newCode.length += sizes[i];
							}
							indexes[i] = newCode.length;
						}
					}
				}
				int opcode = b[u] & 0xFF;
				switch (ClassWriter.TYPE[opcode])
				{
					
					case (sbyte) (ClassWriter.NOARG_INSN): 
					case (sbyte) (ClassWriter.IMPLVAR_INSN): 
						newCode.put1(opcode);
						u += 1;
						break;
					
					case (sbyte) (ClassWriter.LABEL_INSN): 
						if (opcode > 201)
						{
							// changes temporary opcodes 202 to 217 (inclusive), 218 and 219
							// to IFEQ ... JSR (inclusive), IFNULL and IFNONNULL
							opcode = opcode < 218?opcode - 49:opcode - 20;
							label = u + readUnsignedShort(b, u + 1);
						}
						else
						{
							label = u + readShort(b, u + 1);
						}
						newOffset = getNewOffset(allIndexes, allSizes, u, label);
						if (newOffset < System.Int16.MinValue || newOffset > System.Int16.MaxValue)
						{
							// replaces GOTO with GOTO_W, JSR with JSR_W and IFxxx <l> with
							// IFNOTxxx <l'> GOTO_W <l>, where IFNOTxxx is the "opposite" opcode
							// of IFxxx (i.e., IFNE for IFEQ) and where <l'> designates the
							// instruction just after the GOTO_W.
							if (opcode == bsh.org.objectweb.asm.Constants_Fields.GOTO)
							{
								newCode.put1(200); // GOTO_W
							}
							else if (opcode == bsh.org.objectweb.asm.Constants_Fields.JSR)
							{
								newCode.put1(201); // JSR_W
							}
							else
							{
								newCode.put1(opcode <= 166?((opcode + 1) ^ 1) - 1:opcode ^ 1);
								newCode.put2(8); // jump offset
								newCode.put1(200); // GOTO_W
								newOffset -= 3; // newOffset now computed from start of GOTO_W
							}
							newCode.put4(newOffset);
						}
						else
						{
							newCode.put1(opcode);
							newCode.put2(newOffset);
						}
						u += 3;
						break;
					
					case (sbyte) (ClassWriter.LABELW_INSN): 
						label = u + readInt(b, u + 1);
						newOffset = getNewOffset(allIndexes, allSizes, u, label);
						newCode.put1(opcode);
						newCode.put4(newOffset);
						u += 5;
						break;
					
					case (sbyte) (ClassWriter.TABL_INSN): 
						// skips 0 to 3 padding bytes
						v = u;
						u = u + 4 - (v & 3);
						// reads and copies instruction
						int source = newCode.length;
						newCode.put1(bsh.org.objectweb.asm.Constants_Fields.TABLESWITCH);
						while (newCode.length % 4 != 0)
						{
							newCode.put1(0);
						}
						label = v + readInt(b, u); u += 4;
						newOffset = getNewOffset(allIndexes, allSizes, v, label);
						newCode.put4(newOffset);
						j = readInt(b, u); u += 4;
						newCode.put4(j);
						j = readInt(b, u) - j + 1; u += 4;
						newCode.put4(readInt(b, u - 4));
						for (; j > 0; --j)
						{
							label = v + readInt(b, u); u += 4;
							newOffset = getNewOffset(allIndexes, allSizes, v, label);
							newCode.put4(newOffset);
						}
						break;
					
					case (sbyte) (ClassWriter.LOOK_INSN): 
						// skips 0 to 3 padding bytes
						v = u;
						u = u + 4 - (v & 3);
						// reads and copies instruction
						source = newCode.length;
						newCode.put1(bsh.org.objectweb.asm.Constants_Fields.LOOKUPSWITCH);
						while (newCode.length % 4 != 0)
						{
							newCode.put1(0);
						}
						label = v + readInt(b, u); u += 4;
						newOffset = getNewOffset(allIndexes, allSizes, v, label);
						newCode.put4(newOffset);
						j = readInt(b, u); u += 4;
						newCode.put4(j);
						for (; j > 0; --j)
						{
							newCode.put4(readInt(b, u)); u += 4;
							label = v + readInt(b, u); u += 4;
							newOffset = getNewOffset(allIndexes, allSizes, v, label);
							newCode.put4(newOffset);
						}
						break;
					
					case (sbyte) (ClassWriter.WIDE_INSN): 
						opcode = b[u + 1] & 0xFF;
						if (opcode == bsh.org.objectweb.asm.Constants_Fields.IINC)
						{
							newCode.putByteArray(b, u, 6);
							u += 6;
						}
						else
						{
							newCode.putByteArray(b, u, 4);
							u += 4;
						}
						break;
					
					case (sbyte) (ClassWriter.VAR_INSN): 
					case (sbyte) (ClassWriter.SBYTE_INSN): 
					case (sbyte) (ClassWriter.LDC_INSN): 
						newCode.putByteArray(b, u, 2);
						u += 2;
						break;
					
					case (sbyte) (ClassWriter.SHORT_INSN): 
					case (sbyte) (ClassWriter.LDCW_INSN): 
					case (sbyte) (ClassWriter.FIELDORMETH_INSN): 
					case (sbyte) (ClassWriter.TYPE_INSN): 
					case (sbyte) (ClassWriter.IINC_INSN): 
						newCode.putByteArray(b, u, 3);
						u += 3;
						break;
					
					case (sbyte) (ClassWriter.ITFMETH_INSN): 
						newCode.putByteArray(b, u, 5);
						u += 5;
						break;
						// case MANA_INSN:
					
					default: 
						newCode.putByteArray(b, u, 4);
						u += 4;
						break;
					
				}
			}
			
			// updates the instructions addresses in the
			// catch, local var and line number tables
			if (catchTable != null)
			{
				b = catchTable.data;
				u = 0;
				while (u < catchTable.length)
				{
					writeShort(b, u, getNewOffset(allIndexes, allSizes, 0, readUnsignedShort(b, u)));
					writeShort(b, u + 2, getNewOffset(allIndexes, allSizes, 0, readUnsignedShort(b, u + 2)));
					writeShort(b, u + 4, getNewOffset(allIndexes, allSizes, 0, readUnsignedShort(b, u + 4)));
					u += 8;
				}
			}
			if (localVar != null)
			{
				b = localVar.data;
				u = 0;
				while (u < localVar.length)
				{
					label = readUnsignedShort(b, u);
					newOffset = getNewOffset(allIndexes, allSizes, 0, label);
					writeShort(b, u, newOffset);
					label += readUnsignedShort(b, u + 2);
					newOffset = getNewOffset(allIndexes, allSizes, 0, label) - newOffset;
					writeShort(b, u, newOffset);
					u += 10;
				}
			}
			if (lineNumber != null)
			{
				b = lineNumber.data;
				u = 0;
				while (u < lineNumber.length)
				{
					writeShort(b, u, getNewOffset(allIndexes, allSizes, 0, readUnsignedShort(b, u)));
					u += 4;
				}
			}
			
			// replaces old bytecodes with new ones
			code = newCode;
			
			// returns the positions of the resized instructions
			return indexes;
		}
		
		/// <summary> Reads an unsigned short value in the given byte array.
		/// 
		/// </summary>
		/// <param name="b">a byte array.
		/// </param>
		/// <param name="index">the start index of the value to be read.
		/// </param>
		/// <returns> the read value.
		/// </returns>
		
		internal static int readUnsignedShort(sbyte[] b, int index)
		{
			return ((b[index] & 0xFF) << 8) | (b[index + 1] & 0xFF);
		}
		
		/// <summary> Reads a signed short value in the given byte array.
		/// 
		/// </summary>
		/// <param name="b">a byte array.
		/// </param>
		/// <param name="index">the start index of the value to be read.
		/// </param>
		/// <returns> the read value.
		/// </returns>
		
		internal static short readShort(sbyte[] b, int index)
		{
			return (short) (((b[index] & 0xFF) << 8) | (b[index + 1] & 0xFF));
		}
		
		/// <summary> Reads a signed int value in the given byte array.
		/// 
		/// </summary>
		/// <param name="b">a byte array.
		/// </param>
		/// <param name="index">the start index of the value to be read.
		/// </param>
		/// <returns> the read value.
		/// </returns>
		
		internal static int readInt(sbyte[] b, int index)
		{
			return ((b[index] & 0xFF) << 24) | ((b[index + 1] & 0xFF) << 16) | ((b[index + 2] & 0xFF) << 8) | (b[index + 3] & 0xFF);
		}
		
		/// <summary> Writes a short value in the given byte array.
		/// 
		/// </summary>
		/// <param name="b">a byte array.
		/// </param>
		/// <param name="index">where the first byte of the short value must be written.
		/// </param>
		/// <param name="s">the value to be written in the given byte array.
		/// </param>
		
		internal static void  writeShort(sbyte[] b, int index, int s)
		{
			b[index] = (sbyte) (SupportClass.URShift(s, 8));
			b[index + 1] = (sbyte) s;
		}
		
		/// <summary> Computes the future value of a bytecode offset.
		/// <p>
		/// Note: it is possible to have several entries for the same instruction
		/// in the <tt>indexes</tt> and <tt>sizes</tt>: two entries (index=a,size=b)
		/// and (index=a,size=b') are equivalent to a single entry (index=a,size=b+b').
		/// 
		/// </summary>
		/// <param name="indexes">current positions of the instructions to be resized. Each
		/// instruction must be designated by the index of its <i>last</i> byte,
		/// plus one (or, in other words, by the index of the <i>first</i> byte of
		/// the <i>next</i> instruction).
		/// </param>
		/// <param name="sizes">the number of bytes to be <i>added</i> to the above
		/// instructions. More precisely, for each i < <tt>len</tt>,
		/// <tt>sizes</tt>[i] bytes will be added at the end of the instruction
		/// designated by <tt>indexes</tt>[i] or, if <tt>sizes</tt>[i] is
		/// negative, the <i>last</i> |<tt>sizes[i]</tt>| bytes of the instruction
		/// will be removed (the instruction size <i>must not</i> become negative
		/// or null).
		/// </param>
		/// <param name="begin">index of the first byte of the source instruction.
		/// </param>
		/// <param name="end">index of the first byte of the target instruction.
		/// </param>
		/// <returns> the future value of the given bytecode offset.
		/// </returns>
		
		internal static int getNewOffset(int[] indexes, int[] sizes, int begin, int end)
		{
			int offset = end - begin;
			for (int i = 0; i < indexes.Length; ++i)
			{
				if (begin < indexes[i] && indexes[i] <= end)
				{
					// forward jump
					offset += sizes[i];
				}
				else if (end < indexes[i] && indexes[i] <= begin)
				{
					// backward jump
					offset -= sizes[i];
				}
			}
			return offset;
		}
		/// <summary> Computes the stack size variation corresponding to each JVM instruction.</summary>
		static CodeWriter()
		{
			
			{
				int i;
				int[] b = new int[202];
				System.String s = "EFFFFFFFFGGFFFGGFFFEEFGFGFEEEEEEEEEEEEEEEEEEEEDEDEDDDDDCDCDEEEEEEEEE" + "EEEEEEEEEEEBABABBBBDCFFFGGGEDCDCDCDCDCDCDCDCDCDCEEEEDDDDDDDCDCDCEFEF" + "DDEEFFDEDEEEBDDBBDDDDDDCCCCCCCCEFEDDDCDCDEEEEEEEEEEFEEEEEEDDEEDDEE";
				for (i = 0; i < b.Length; ++i)
				{
					b[i] = s[i] - 'E';
				}
				SIZE = b;
				
				/* code to generate the above string
				
				int NA = 0; // not applicable (unused opcode or variable size opcode)
				
				b = new int[] {
				0,  //NOP,             // visitInsn
				1,  //ACONST_NULL,     // -
				1,  //ICONST_M1,       // -
				1,  //ICONST_0,        // -
				1,  //ICONST_1,        // -
				1,  //ICONST_2,        // -
				1,  //ICONST_3,        // -
				1,  //ICONST_4,        // -
				1,  //ICONST_5,        // -
				2,  //LCONST_0,        // -
				2,  //LCONST_1,        // -
				1,  //FCONST_0,        // -
				1,  //FCONST_1,        // -
				1,  //FCONST_2,        // -
				2,  //DCONST_0,        // -
				2,  //DCONST_1,        // -
				1,  //BIPUSH,          // visitIntInsn
				1,  //SIPUSH,          // -
				1,  //LDC,             // visitLdcInsn
				NA, //LDC_W,           // -
				NA, //LDC2_W,          // -
				1,  //ILOAD,           // visitVarInsn
				2,  //LLOAD,           // -
				1,  //FLOAD,           // -
				2,  //DLOAD,           // -
				1,  //ALOAD,           // -
				NA, //ILOAD_0,         // -
				NA, //ILOAD_1,         // -
				NA, //ILOAD_2,         // -
				NA, //ILOAD_3,         // -
				NA, //LLOAD_0,         // -
				NA, //LLOAD_1,         // -
				NA, //LLOAD_2,         // -
				NA, //LLOAD_3,         // -
				NA, //FLOAD_0,         // -
				NA, //FLOAD_1,         // -
				NA, //FLOAD_2,         // -
				NA, //FLOAD_3,         // -
				NA, //DLOAD_0,         // -
				NA, //DLOAD_1,         // -
				NA, //DLOAD_2,         // -
				NA, //DLOAD_3,         // -
				NA, //ALOAD_0,         // -
				NA, //ALOAD_1,         // -
				NA, //ALOAD_2,         // -
				NA, //ALOAD_3,         // -
				-1, //IALOAD,          // visitInsn
				0,  //LALOAD,          // -
				-1, //FALOAD,          // -
				0,  //DALOAD,          // -
				-1, //AALOAD,          // -
				-1, //BALOAD,          // -
				-1, //CALOAD,          // -
				-1, //SALOAD,          // -
				-1, //ISTORE,          // visitVarInsn
				-2, //LSTORE,          // -
				-1, //FSTORE,          // -
				-2, //DSTORE,          // -
				-1, //ASTORE,          // -
				NA, //ISTORE_0,        // -
				NA, //ISTORE_1,        // -
				NA, //ISTORE_2,        // -
				NA, //ISTORE_3,        // -
				NA, //LSTORE_0,        // -
				NA, //LSTORE_1,        // -
				NA, //LSTORE_2,        // -
				NA, //LSTORE_3,        // -
				NA, //FSTORE_0,        // -
				NA, //FSTORE_1,        // -
				NA, //FSTORE_2,        // -
				NA, //FSTORE_3,        // -
				NA, //DSTORE_0,        // -
				NA, //DSTORE_1,        // -
				NA, //DSTORE_2,        // -
				NA, //DSTORE_3,        // -
				NA, //ASTORE_0,        // -
				NA, //ASTORE_1,        // -
				NA, //ASTORE_2,        // -
				NA, //ASTORE_3,        // -
				-3, //IASTORE,         // visitInsn
				-4, //LASTORE,         // -
				-3, //FASTORE,         // -
				-4, //DASTORE,         // -
				-3, //AASTORE,         // -
				-3, //BASTORE,         // -
				-3, //CASTORE,         // -
				-3, //SASTORE,         // -
				-1, //POP,             // -
				-2, //POP2,            // -
				1,  //DUP,             // -
				1,  //DUP_X1,          // -
				1,  //DUP_X2,          // -
				2,  //DUP2,            // -
				2,  //DUP2_X1,         // -
				2,  //DUP2_X2,         // -
				0,  //SWAP,            // -
				-1, //IADD,            // -
				-2, //LADD,            // -
				-1, //FADD,            // -
				-2, //DADD,            // -
				-1, //ISUB,            // -
				-2, //LSUB,            // -
				-1, //FSUB,            // -
				-2, //DSUB,            // -
				-1, //IMUL,            // -
				-2, //LMUL,            // -
				-1, //FMUL,            // -
				-2, //DMUL,            // -
				-1, //IDIV,            // -
				-2, //LDIV,            // -
				-1, //FDIV,            // -
				-2, //DDIV,            // -
				-1, //IREM,            // -
				-2, //LREM,            // -
				-1, //FREM,            // -
				-2, //DREM,            // -
				0,  //INEG,            // -
				0,  //LNEG,            // -
				0,  //FNEG,            // -
				0,  //DNEG,            // -
				-1, //ISHL,            // -
				-1, //LSHL,            // -
				-1, //ISHR,            // -
				-1, //LSHR,            // -
				-1, //IUSHR,           // -
				-1, //LUSHR,           // -
				-1, //IAND,            // -
				-2, //LAND,            // -
				-1, //IOR,             // -
				-2, //LOR,             // -
				-1, //IXOR,            // -
				-2, //LXOR,            // -
				0,  //IINC,            // visitIincInsn
				1,  //I2L,             // visitInsn
				0,  //I2F,             // -
				1,  //I2D,             // -
				-1, //L2I,             // -
				-1, //L2F,             // -
				0,  //L2D,             // -
				0,  //F2I,             // -
				1,  //F2L,             // -
				1,  //F2D,             // -
				-1, //D2I,             // -
				0,  //D2L,             // -
				-1, //D2F,             // -
				0,  //I2B,             // -
				0,  //I2C,             // -
				0,  //I2S,             // -
				-3, //LCMP,            // -
				-1, //FCMPL,           // -
				-1, //FCMPG,           // -
				-3, //DCMPL,           // -
				-3, //DCMPG,           // -
				-1, //IFEQ,            // visitJumpInsn
				-1, //IFNE,            // -
				-1, //IFLT,            // -
				-1, //IFGE,            // -
				-1, //IFGT,            // -
				-1, //IFLE,            // -
				-2, //IF_ICMPEQ,       // -
				-2, //IF_ICMPNE,       // -
				-2, //IF_ICMPLT,       // -
				-2, //IF_ICMPGE,       // -
				-2, //IF_ICMPGT,       // -
				-2, //IF_ICMPLE,       // -
				-2, //IF_ACMPEQ,       // -
				-2, //IF_ACMPNE,       // -
				0,  //GOTO,            // -
				1,  //JSR,             // -
				0,  //RET,             // visitVarInsn
				-1, //TABLESWITCH,     // visiTableSwitchInsn
				-1, //LOOKUPSWITCH,    // visitLookupSwitch
				-1, //IRETURN,         // visitInsn
				-2, //LRETURN,         // -
				-1, //FRETURN,         // -
				-2, //DRETURN,         // -
				-1, //ARETURN,         // -
				0,  //RETURN,          // -
				NA, //GETSTATIC,       // visitFieldInsn
				NA, //PUTSTATIC,       // -
				NA, //GETFIELD,        // -
				NA, //PUTFIELD,        // -
				NA, //INVOKEVIRTUAL,   // visitMethodInsn
				NA, //INVOKESPECIAL,   // -
				NA, //INVOKESTATIC,    // -
				NA, //INVOKEINTERFACE, // -
				NA, //UNUSED,          // NOT VISITED
				1,  //NEW,             // visitTypeInsn
				0,  //NEWARRAY,        // visitIntInsn
				0,  //ANEWARRAY,       // visitTypeInsn
				0,  //ARRAYLENGTH,     // visitInsn
				NA, //ATHROW,          // -
				0,  //CHECKCAST,       // visitTypeInsn
				0,  //INSTANCEOF,      // -
				-1, //MONITORENTER,    // visitInsn
				-1, //MONITOREXIT,     // -
				NA, //WIDE,            // NOT VISITED
				NA, //MULTIANEWARRAY,  // visitMultiANewArrayInsn
				-1, //IFNULL,          // visitJumpInsn
				-1, //IFNONNULL,       // -
				NA, //GOTO_W,          // -
				NA, //JSR_W,           // -
				};
				for (i = 0; i < b.length; ++i) {
				System.err.print((char)('E' + b[i]));
				}
				System.err.println();
				*/
			}
		}
	}
}