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
	
	/// <summary> A label represents a position in the bytecode of a method. Labels are used
	/// for jump, goto, and switch instructions, and for try catch blocks.
	/// </summary>
	
	public class Label
	{
		
		/// <summary> The code writer to which this label belongs, or <tt>null</tt> if unknown.</summary>
		
		internal CodeWriter owner;
		
		/// <summary> Indicates if the position of this label is known.</summary>
		
		internal bool resolved;
		
		/// <summary> The position of this label in the code, if known.</summary>
		
		internal int position;
		
		/// <summary> Number of forward references to this label, times two.</summary>
		
		private int referenceCount;
		
		/// <summary> Informations about forward references. Each forward reference is described
		/// by two consecutive integers in this array: the first one is the position
		/// of the first byte of the bytecode instruction that contains the forward
		/// reference, while the second is the position of the first byte of the
		/// forward reference itself. In fact the sign of the first integer indicates
		/// if this reference uses 2 or 4 bytes, and its absolute value gives the
		/// position of the bytecode instruction.
		/// </summary>
		
		private int[] srcAndRefPositions;
		
		// --------------------------------------------------------------------------
		// Fields for the control flow graph analysis algorithm (used to compute the
		// maximum stack size). A control flow graph contains one node per "basic
		// block", and one edge per "jump" from one basic block to another. Each node
		// (i.e., each basic block) is represented by the Label object that
		// corresponds to the first instruction of this basic block. Each node also
		// stores the list of it successors in the graph, as a linked list of Edge
		// objects.
		// --------------------------------------------------------------------------
		
		/// <summary> The stack size at the beginning of this basic block.
		/// This size is initially unknown. It is computed by the control flow
		/// analysis algorithm (see {@link CodeWriter#visitMaxs visitMaxs}).
		/// </summary>
		
		internal int beginStackSize;
		
		/// <summary> The (relative) maximum stack size corresponding to this basic block. This
		/// size is relative to the stack size at the beginning of the basic block,
		/// i.e., the true maximum stack size is equal to {@link #beginStackSize
		/// beginStackSize} + {@link #maxStackSize maxStackSize}.
		/// </summary>
		
		internal int maxStackSize;
		
		/// <summary> The successors of this node in the control flow graph. These successors
		/// are stored in a linked list of {@link Edge Edge} objects, linked to each
		/// other by their {@link Edge#next} field.
		/// </summary>
		
		internal Edge successors;
		
		/// <summary> The next basic block in the basic block stack.
		/// See {@link CodeWriter#visitMaxs visitMaxs}.
		/// </summary>
		
		internal Label next;
		
		/// <summary> <tt>true</tt> if this basic block has been pushed in the basic block stack.
		/// See {@link CodeWriter#visitMaxs visitMaxs}.
		/// </summary>
		
		internal bool pushed;
		
		// --------------------------------------------------------------------------
		// Constructor
		// --------------------------------------------------------------------------
		
		/// <summary> Constructs a new label.</summary>
		
		public Label()
		{
		}
		
		// --------------------------------------------------------------------------
		// Methods to compute offsets and to manage forward references
		// --------------------------------------------------------------------------
		
		/// <summary> Puts a reference to this label in the bytecode of a method. If the position
		/// of the label is known, the offset is computed and written directly.
		/// Otherwise, a null offset is written and a new forward reference is declared
		/// for this label.
		/// 
		/// </summary>
		/// <param name="owner">the code writer that calls this method.
		/// </param>
		/// <param name="out">the bytecode of the method.
		/// </param>
		/// <param name="source">the position of first byte of the bytecode instruction that
		/// contains this label.
		/// </param>
		/// <param name="wideOffset"><tt>true</tt> if the reference must be stored in 4 bytes,
		/// or <tt>false</tt> if it must be stored with 2 bytes.
		/// </param>
		/// <throws>  IllegalArgumentException if this label has not been created by the </throws>
		/// <summary>      given code writer.
		/// </summary>
		
		internal virtual void  put(CodeWriter owner, ByteVector out_Renamed, int source, bool wideOffset)
		{
			if (CodeWriter.CHECK)
			{
				if (this.owner == null)
				{
					this.owner = owner;
				}
				else if (this.owner != owner)
				{
					throw new System.ArgumentException();
				}
			}
			if (resolved)
			{
				if (wideOffset)
				{
					out_Renamed.put4(position - source);
				}
				else
				{
					out_Renamed.put2(position - source);
				}
			}
			else
			{
				if (wideOffset)
				{
					addReference(- 1 - source, out_Renamed.length);
					out_Renamed.put4(- 1);
				}
				else
				{
					addReference(source, out_Renamed.length);
					out_Renamed.put2(- 1);
				}
			}
		}
		
		/// <summary> Adds a forward reference to this label. This method must be called only for
		/// a true forward reference, i.e. only if this label is not resolved yet. For
		/// backward references, the offset of the reference can be, and must be,
		/// computed and stored directly.
		/// 
		/// </summary>
		/// <param name="sourcePosition">the position of the referencing instruction. This
		/// position will be used to compute the offset of this forward reference.
		/// </param>
		/// <param name="referencePosition">the position where the offset for this forward
		/// reference must be stored.
		/// </param>
		
		private void  addReference(int sourcePosition, int referencePosition)
		{
			if (srcAndRefPositions == null)
			{
				srcAndRefPositions = new int[6];
			}
			if (referenceCount >= srcAndRefPositions.Length)
			{
				int[] a = new int[srcAndRefPositions.Length + 6];
				Array.Copy(srcAndRefPositions, 0, a, 0, srcAndRefPositions.Length);
				srcAndRefPositions = a;
			}
			srcAndRefPositions[referenceCount++] = sourcePosition;
			srcAndRefPositions[referenceCount++] = referencePosition;
		}
		
		/// <summary> Resolves all forward references to this label. This method must be called
		/// when this label is added to the bytecode of the method, i.e. when its
		/// position becomes known. This method fills in the blanks that where left in
		/// the bytecode by each forward reference previously added to this label.
		/// 
		/// </summary>
		/// <param name="owner">the code writer that calls this method.
		/// </param>
		/// <param name="position">the position of this label in the bytecode.
		/// </param>
		/// <param name="data">the bytecode of the method.
		/// </param>
		/// <returns> <tt>true</tt> if a blank that was left for this label was to small
		/// to store the offset. In such a case the corresponding jump instruction
		/// is replaced with a pseudo instruction (using unused opcodes) using an
		/// unsigned two bytes offset. These pseudo instructions will need to be
		/// replaced with true instructions with wider offsets (4 bytes instead of
		/// 2). This is done in {@link CodeWriter#resizeInstructions}.
		/// </returns>
		/// <throws>  IllegalArgumentException if this label has already been resolved, </throws>
		/// <summary>      or if it has not been created by the given code writer.
		/// </summary>
		
		internal virtual bool resolve(CodeWriter owner, int position, sbyte[] data)
		{
			if (CodeWriter.CHECK)
			{
				if (this.owner == null)
				{
					this.owner = owner;
				}
				if (resolved || this.owner != owner)
				{
					throw new System.ArgumentException();
				}
			}
			bool needUpdate = false;
			this.resolved = true;
			this.position = position;
			int i = 0;
			while (i < referenceCount)
			{
				int source = srcAndRefPositions[i++];
				int reference = srcAndRefPositions[i++];
				int offset;
				if (source >= 0)
				{
					offset = position - source;
					if (offset < System.Int16.MinValue || offset > System.Int16.MaxValue)
					{
						// changes the opcode of the jump instruction, in order to be able to
						// find it later (see resizeInstructions in CodeWriter). These
						// temporary opcodes are similar to jump instruction opcodes, except
						// that the 2 bytes offset is unsigned (and can therefore represent
						// values from 0 to 65535, which is sufficient since the size of a
						// method is limited to 65535 bytes).
						int opcode = data[reference - 1] & 0xFF;
						if (opcode <= bsh.org.objectweb.asm.Constants_Fields.JSR)
						{
							// changes IFEQ ... JSR to opcodes 202 to 217 (inclusive)
							data[reference - 1] = (sbyte) (opcode + 49);
						}
						else
						{
							// changes IFNULL and IFNONNULL to opcodes 218 and 219 (inclusive)
							data[reference - 1] = (sbyte) (opcode + 20);
						}
						needUpdate = true;
					}
					data[reference++] = (sbyte) (SupportClass.URShift(offset, 8));
					data[reference] = (sbyte) offset;
				}
				else
				{
					offset = position + source + 1;
					data[reference++] = (sbyte) (SupportClass.URShift(offset, 24));
					data[reference++] = (sbyte) (SupportClass.URShift(offset, 16));
					data[reference++] = (sbyte) (SupportClass.URShift(offset, 8));
					data[reference] = (sbyte) offset;
				}
			}
			return needUpdate;
		}
	}
}