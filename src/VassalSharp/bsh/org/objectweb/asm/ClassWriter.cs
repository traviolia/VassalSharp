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
	
	/// <summary> A {@link ClassVisitor ClassVisitor} that generates Java class files. More
	/// precisely this visitor generates a byte array conforming to the Java class
	/// file format. It can be used alone, to generate a Java class "from scratch",
	/// or with one or more {@link ClassReader ClassReader} and adapter class
	/// visitor to generate a modified class from one or more existing Java classes.
	/// </summary>
	
	public class ClassWriter : ClassVisitor
	{
		
		/// <summary> The type of CONSTANT_Class constant pool items.</summary>
		
		internal const int CLASS = 7;
		
		/// <summary> The type of CONSTANT_Fieldref constant pool items.</summary>
		
		internal const int FIELD = 9;
		
		/// <summary> The type of CONSTANT_Methodref constant pool items.</summary>
		
		internal const int METH = 10;
		
		/// <summary> The type of CONSTANT_InterfaceMethodref constant pool items.</summary>
		
		internal const int IMETH = 11;
		
		/// <summary> The type of CONSTANT_String constant pool items.</summary>
		
		internal const int STR = 8;
		
		/// <summary> The type of CONSTANT_Integer constant pool items.</summary>
		
		internal const int INT = 3;
		
		/// <summary> The type of CONSTANT_Float constant pool items.</summary>
		
		internal const int FLOAT = 4;
		
		/// <summary> The type of CONSTANT_Long constant pool items.</summary>
		
		internal const int LONG = 5;
		
		/// <summary> The type of CONSTANT_Double constant pool items.</summary>
		
		internal const int DOUBLE = 6;
		
		/// <summary> The type of CONSTANT_NameAndType constant pool items.</summary>
		
		internal const int NAME_TYPE = 12;
		
		/// <summary> The type of CONSTANT_Utf8 constant pool items.</summary>
		
		internal const int UTF8 = 1;
		
		/// <summary> Index of the next item to be added in the constant pool.</summary>
		
		private short index;
		
		/// <summary> The constant pool of this class.</summary>
		
		private ByteVector pool;
		
		/// <summary> The constant pool's hash table data.</summary>
		
		private Item[] table;
		
		/// <summary> The threshold of the constant pool's hash table.</summary>
		
		private int threshold;
		
		/// <summary> The access flags of this class.</summary>
		
		private int access;
		
		/// <summary> The constant pool item that contains the internal name of this class.</summary>
		
		private int name;
		
		/// <summary> The constant pool item that contains the internal name of the super class
		/// of this class.
		/// </summary>
		
		private int superName;
		
		/// <summary> Number of interfaces implemented or extended by this class or interface.</summary>
		
		private int interfaceCount;
		
		/// <summary> The interfaces implemented or extended by this class or interface. More
		/// precisely, this array contains the indexes of the constant pool items
		/// that contain the internal names of these interfaces.
		/// </summary>
		
		private int[] interfaces;
		
		/// <summary> The constant pool item that contains the name of the source file from
		/// which this class was compiled.
		/// </summary>
		
		private Item sourceFile;
		
		/// <summary> Number of fields of this class.</summary>
		
		private int fieldCount;
		
		/// <summary> The fields of this class.</summary>
		
		private ByteVector fields;
		
		/// <summary> <tt>true</tt> if the maximum stack size and number of local variables must
		/// be automatically computed.
		/// </summary>
		
		private bool computeMaxs;
		
		/// <summary> The methods of this class. These methods are stored in a linked list of
		/// {@link CodeWriter CodeWriter} objects, linked to each other by their {@link
		/// CodeWriter#next} field. This field stores the first element of this list.
		/// </summary>
		
		internal CodeWriter firstMethod;
		
		/// <summary> The methods of this class. These methods are stored in a linked list of
		/// {@link CodeWriter CodeWriter} objects, linked to each other by their {@link
		/// CodeWriter#next} field. This field stores the last element of this list.
		/// </summary>
		
		internal CodeWriter lastMethod;
		
		/// <summary> The number of entries in the InnerClasses attribute.</summary>
		
		private int innerClassesCount;
		
		/// <summary> The InnerClasses attribute.</summary>
		
		private ByteVector innerClasses;
		
		/// <summary> A reusable key used to look for items in the hash {@link #table table}.</summary>
		
		internal Item key;
		
		/// <summary> A reusable key used to look for items in the hash {@link #table table}.</summary>
		
		internal Item key2;
		
		/// <summary> A reusable key used to look for items in the hash {@link #table table}.</summary>
		
		internal Item key3;
		
		/// <summary> The type of instructions without any label.</summary>
		
		internal const int NOARG_INSN = 0;
		
		/// <summary> The type of instructions with an signed byte label.</summary>
		
		internal const int SBYTE_INSN = 1;
		
		/// <summary> The type of instructions with an signed short label.</summary>
		
		internal const int SHORT_INSN = 2;
		
		/// <summary> The type of instructions with a local variable index label.</summary>
		
		internal const int VAR_INSN = 3;
		
		/// <summary> The type of instructions with an implicit local variable index label.</summary>
		
		internal const int IMPLVAR_INSN = 4;
		
		/// <summary> The type of instructions with a type descriptor argument.</summary>
		
		internal const int TYPE_INSN = 5;
		
		/// <summary> The type of field and method invocations instructions.</summary>
		
		internal const int FIELDORMETH_INSN = 6;
		
		/// <summary> The type of the INVOKEINTERFACE instruction.</summary>
		
		internal const int ITFMETH_INSN = 7;
		
		/// <summary> The type of instructions with a 2 bytes bytecode offset label.</summary>
		
		internal const int LABEL_INSN = 8;
		
		/// <summary> The type of instructions with a 4 bytes bytecode offset label.</summary>
		
		internal const int LABELW_INSN = 9;
		
		/// <summary> The type of the LDC instruction.</summary>
		
		internal const int LDC_INSN = 10;
		
		/// <summary> The type of the LDC_W and LDC2_W instructions.</summary>
		
		internal const int LDCW_INSN = 11;
		
		/// <summary> The type of the IINC instruction.</summary>
		
		internal const int IINC_INSN = 12;
		
		/// <summary> The type of the TABLESWITCH instruction.</summary>
		
		internal const int TABL_INSN = 13;
		
		/// <summary> The type of the LOOKUPSWITCH instruction.</summary>
		
		internal const int LOOK_INSN = 14;
		
		/// <summary> The type of the MULTIANEWARRAY instruction.</summary>
		
		internal const int MANA_INSN = 15;
		
		/// <summary> The type of the WIDE instruction.</summary>
		
		internal const int WIDE_INSN = 16;
		
		/// <summary> The instruction types of all JVM opcodes.</summary>
		
		internal static sbyte[] TYPE;
		
		// --------------------------------------------------------------------------
		// Static initializer
		// --------------------------------------------------------------------------
		
		// --------------------------------------------------------------------------
		// Constructor
		// --------------------------------------------------------------------------
		
		/// <summary> Constructs a new {@link ClassWriter ClassWriter} object.
		/// 
		/// </summary>
		/// <param name="computeMaxs"><tt>true</tt> if the maximum stack size and the maximum
		/// number of local variables must be automatically computed. If this flag
		/// is <tt>true</tt>, then the arguments of the {@link
		/// CodeVisitor#visitMaxs visitMaxs} method of the {@link CodeVisitor
		/// CodeVisitor} returned by the {@link #visitMethod visitMethod} method
		/// will be ignored, and computed automatically from the signature and
		/// the bytecode of each method.
		/// </param>
		
		public ClassWriter(bool computeMaxs)
		{
			index = 1;
			pool = new ByteVector();
			table = new Item[64];
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			threshold = (int) (0.75d * table.Length);
			key = new Item();
			key2 = new Item();
			key3 = new Item();
			this.computeMaxs = computeMaxs;
		}
		
		// --------------------------------------------------------------------------
		// Implementation of the ClassVisitor interface
		// --------------------------------------------------------------------------
		
		public virtual void  visit(int access, System.String name, System.String superName, System.String[] interfaces, System.String sourceFile)
		{
			this.access = access;
			this.name = newClass(name).index;
			this.superName = superName == null?0:newClass(superName).index;
			if (interfaces != null && interfaces.Length > 0)
			{
				interfaceCount = interfaces.Length;
				this.interfaces = new int[interfaceCount];
				for (int i = 0; i < interfaceCount; ++i)
				{
					this.interfaces[i] = newClass(interfaces[i]).index;
				}
			}
			if (sourceFile != null)
			{
				newUTF8("SourceFile");
				this.sourceFile = newUTF8(sourceFile);
			}
			if ((access & bsh.org.objectweb.asm.Constants_Fields.ACC_DEPRECATED) != 0)
			{
				newUTF8("Deprecated");
			}
		}
		
		public virtual void  visitInnerClass(System.String name, System.String outerName, System.String innerName, int access)
		{
			if (innerClasses == null)
			{
				newUTF8("InnerClasses");
				innerClasses = new ByteVector();
			}
			++innerClassesCount;
			innerClasses.put2(name == null?0:newClass(name).index);
			innerClasses.put2(outerName == null?0:newClass(outerName).index);
			innerClasses.put2(innerName == null?0:newUTF8(innerName).index);
			innerClasses.put2(access);
		}
		
		public virtual void  visitField(int access, System.String name, System.String desc, System.Object value_Renamed)
		{
			++fieldCount;
			if (fields == null)
			{
				fields = new ByteVector();
			}
			fields.put2(access).put2(newUTF8(name).index).put2(newUTF8(desc).index);
			int attributeCount = 0;
			if (value_Renamed != null)
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
			fields.put2(attributeCount);
			if (value_Renamed != null)
			{
				fields.put2(newUTF8("ConstantValue").index);
				fields.put4(2).put2(newCst(value_Renamed).index);
			}
			if ((access & bsh.org.objectweb.asm.Constants_Fields.ACC_SYNTHETIC) != 0)
			{
				fields.put2(newUTF8("Synthetic").index).put4(0);
			}
			if ((access & bsh.org.objectweb.asm.Constants_Fields.ACC_DEPRECATED) != 0)
			{
				fields.put2(newUTF8("Deprecated").index).put4(0);
			}
		}
		
		public virtual CodeVisitor visitMethod(int access, System.String name, System.String desc, System.String[] exceptions)
		{
			CodeWriter cw = new CodeWriter(this, computeMaxs);
			cw.init(access, name, desc, exceptions);
			return cw;
		}
		
		public virtual void  visitEnd()
		{
		}
		
		// --------------------------------------------------------------------------
		// Other public methods
		// --------------------------------------------------------------------------
		
		/// <summary> Returns the bytecode of the class that was build with this class writer.
		/// 
		/// </summary>
		/// <returns> the bytecode of the class that was build with this class writer.
		/// </returns>
		
		public virtual sbyte[] toByteArray()
		{
			// computes the real size of the bytecode of this class
			int size = 24 + 2 * interfaceCount;
			if (fields != null)
			{
				size += fields.length;
			}
			int nbMethods = 0;
			CodeWriter cb = firstMethod;
			while (cb != null)
			{
				++nbMethods;
				size += cb.Size;
				cb = cb.next;
			}
			size += pool.length;
			int attributeCount = 0;
			if (sourceFile != null)
			{
				++attributeCount;
				size += 8;
			}
			if ((access & bsh.org.objectweb.asm.Constants_Fields.ACC_DEPRECATED) != 0)
			{
				++attributeCount;
				size += 6;
			}
			if (innerClasses != null)
			{
				++attributeCount;
				size += 8 + innerClasses.length;
			}
			// allocates a byte vector of this size, in order to avoid unnecessary
			// arraycopy operations in the ByteVector.enlarge() method
			ByteVector out_Renamed = new ByteVector(size);
			out_Renamed.put4(unchecked((int) 0xCAFEBABE)).put2(3).put2(45);
			out_Renamed.put2(index).putByteArray(pool.data, 0, pool.length);
			out_Renamed.put2(access).put2(name).put2(superName);
			out_Renamed.put2(interfaceCount);
			for (int i = 0; i < interfaceCount; ++i)
			{
				out_Renamed.put2(interfaces[i]);
			}
			out_Renamed.put2(fieldCount);
			if (fields != null)
			{
				out_Renamed.putByteArray(fields.data, 0, fields.length);
			}
			out_Renamed.put2(nbMethods);
			cb = firstMethod;
			while (cb != null)
			{
				cb.put(out_Renamed);
				cb = cb.next;
			}
			out_Renamed.put2(attributeCount);
			if (sourceFile != null)
			{
				out_Renamed.put2(newUTF8("SourceFile").index).put4(2).put2(sourceFile.index);
			}
			if ((access & bsh.org.objectweb.asm.Constants_Fields.ACC_DEPRECATED) != 0)
			{
				out_Renamed.put2(newUTF8("Deprecated").index).put4(0);
			}
			if (innerClasses != null)
			{
				out_Renamed.put2(newUTF8("InnerClasses").index);
				out_Renamed.put4(innerClasses.length + 2).put2(innerClassesCount);
				out_Renamed.putByteArray(innerClasses.data, 0, innerClasses.length);
			}
			return out_Renamed.data;
		}
		
		// --------------------------------------------------------------------------
		// Utility methods: constant pool management
		// --------------------------------------------------------------------------
		
		/// <summary> Adds a number or string constant to the constant pool of the class being
		/// build. Does nothing if the constant pool already contains a similar item.
		/// 
		/// </summary>
		/// <param name="cst">the value of the constant to be added to the constant pool. This
		/// parameter must be an {@link java.lang.Integer Integer}, a {@link
		/// java.lang.Float Float}, a {@link java.lang.Long Long}, a {@link
		/// java.lang.Double Double} or a {@link String String}.
		/// </param>
		/// <returns> a new or already existing constant item with the given value.
		/// </returns>
		
		internal virtual Item newCst(System.Object cst)
		{
			if (cst is System.Int32)
			{
				int val = ((System.Int32) cst);
				return newInteger(val);
			}
			else if (cst is System.Single)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Float.floatValue' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				float val = (float) ((System.Single) cst);
				return newFloat(val);
			}
			else if (cst is System.Int64)
			{
				long val = (long) ((System.Int64) cst);
				return newLong(val);
			}
			else if (cst is System.Double)
			{
				double val = ((System.Double) cst);
				return newDouble(val);
			}
			else if (cst is System.String)
			{
				return newString((System.String) cst);
			}
			else
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				throw new System.ArgumentException("value " + cst);
			}
		}
		
		/// <summary> Adds an UTF string to the constant pool of the class being build. Does
		/// nothing if the constant pool already contains a similar item.
		/// 
		/// </summary>
		/// <param name="value">the String value.
		/// </param>
		/// <returns> a new or already existing UTF8 item.
		/// </returns>
		
		internal virtual Item newUTF8(System.String value_Renamed)
		{
			key.set_Renamed(UTF8, value_Renamed, null, null);
			Item result = get_Renamed(key);
			if (result == null)
			{
				pool.put1(UTF8).putUTF(value_Renamed);
				result = new Item(index++, key);
				put(result);
			}
			return result;
		}
		
		/// <summary> Adds a class reference to the constant pool of the class being build. Does
		/// nothing if the constant pool already contains a similar item.
		/// 
		/// </summary>
		/// <param name="value">the internal name of the class.
		/// </param>
		/// <returns> a new or already existing class reference item.
		/// </returns>
		
		internal virtual Item newClass(System.String value_Renamed)
		{
			key2.set_Renamed(CLASS, value_Renamed, null, null);
			Item result = get_Renamed(key2);
			if (result == null)
			{
				pool.put12(CLASS, newUTF8(value_Renamed).index);
				result = new Item(index++, key2);
				put(result);
			}
			return result;
		}
		
		/// <summary> Adds a field reference to the constant pool of the class being build. Does
		/// nothing if the constant pool already contains a similar item.
		/// 
		/// </summary>
		/// <param name="owner">the internal name of the field's owner class.
		/// </param>
		/// <param name="name">the field's name.
		/// </param>
		/// <param name="desc">the field's descriptor.
		/// </param>
		/// <returns> a new or already existing field reference item.
		/// </returns>
		
		internal virtual Item newField(System.String owner, System.String name, System.String desc)
		{
			key3.set_Renamed(FIELD, owner, name, desc);
			Item result = get_Renamed(key3);
			if (result == null)
			{
				put122(FIELD, newClass(owner).index, newNameType(name, desc).index);
				result = new Item(index++, key3);
				put(result);
			}
			return result;
		}
		
		/// <summary> Adds a method reference to the constant pool of the class being build. Does
		/// nothing if the constant pool already contains a similar item.
		/// 
		/// </summary>
		/// <param name="owner">the internal name of the method's owner class.
		/// </param>
		/// <param name="name">the method's name.
		/// </param>
		/// <param name="desc">the method's descriptor.
		/// </param>
		/// <returns> a new or already existing method reference item.
		/// </returns>
		
		internal virtual Item newMethod(System.String owner, System.String name, System.String desc)
		{
			key3.set_Renamed(METH, owner, name, desc);
			Item result = get_Renamed(key3);
			if (result == null)
			{
				put122(METH, newClass(owner).index, newNameType(name, desc).index);
				result = new Item(index++, key3);
				put(result);
			}
			return result;
		}
		
		/// <summary> Adds an interface method reference to the constant pool of the class being
		/// build. Does nothing if the constant pool already contains a similar item.
		/// 
		/// </summary>
		/// <param name="ownerItf">the internal name of the method's owner interface.
		/// </param>
		/// <param name="name">the method's name.
		/// </param>
		/// <param name="desc">the method's descriptor.
		/// </param>
		/// <returns> a new or already existing interface method reference item.
		/// </returns>
		
		internal virtual Item newItfMethod(System.String ownerItf, System.String name, System.String desc)
		{
			key3.set_Renamed(IMETH, ownerItf, name, desc);
			Item result = get_Renamed(key3);
			if (result == null)
			{
				put122(IMETH, newClass(ownerItf).index, newNameType(name, desc).index);
				result = new Item(index++, key3);
				put(result);
			}
			return result;
		}
		
		/// <summary> Adds an integer to the constant pool of the class being build. Does nothing
		/// if the constant pool already contains a similar item.
		/// 
		/// </summary>
		/// <param name="value">the int value.
		/// </param>
		/// <returns> a new or already existing int item.
		/// </returns>
		
		private Item newInteger(int value_Renamed)
		{
			key.set_Renamed(value_Renamed);
			Item result = get_Renamed(key);
			if (result == null)
			{
				pool.put1(INT).put4(value_Renamed);
				result = new Item(index++, key);
				put(result);
			}
			return result;
		}
		
		/// <summary> Adds a float to the constant pool of the class being build. Does nothing if
		/// the constant pool already contains a similar item.
		/// 
		/// </summary>
		/// <param name="value">the float value.
		/// </param>
		/// <returns> a new or already existing float item.
		/// </returns>
		
		private Item newFloat(float value_Renamed)
		{
			key.set_Renamed(value_Renamed);
			Item result = get_Renamed(key);
			if (result == null)
			{
				//UPGRADE_ISSUE: Method 'java.lang.Float.floatToIntBits' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangFloatfloatToIntBits_float'"
				pool.put1(FLOAT).put4(Float.floatToIntBits(value_Renamed));
				result = new Item(index++, key);
				put(result);
			}
			return result;
		}
		
		/// <summary> Adds a long to the constant pool of the class being build. Does nothing if
		/// the constant pool already contains a similar item.
		/// 
		/// </summary>
		/// <param name="value">the long value.
		/// </param>
		/// <returns> a new or already existing long item.
		/// </returns>
		
		private Item newLong(long value_Renamed)
		{
			key.set_Renamed(value_Renamed);
			Item result = get_Renamed(key);
			if (result == null)
			{
				pool.put1(LONG).put8(value_Renamed);
				result = new Item(index, key);
				put(result);
				index = (short) (index + 2);
			}
			return result;
		}
		
		/// <summary> Adds a double to the constant pool of the class being build. Does nothing
		/// if the constant pool already contains a similar item.
		/// 
		/// </summary>
		/// <param name="value">the double value.
		/// </param>
		/// <returns> a new or already existing double item.
		/// </returns>
		
		private Item newDouble(double value_Renamed)
		{
			key.set_Renamed(value_Renamed);
			Item result = get_Renamed(key);
			if (result == null)
			{
				//UPGRADE_ISSUE: Method 'java.lang.Double.doubleToLongBits' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangDoubledoubleToLongBits_double'"
				pool.put1(DOUBLE).put8(Double.doubleToLongBits(value_Renamed));
				result = new Item(index, key);
				put(result);
				index = (short) (index + 2);
			}
			return result;
		}
		
		/// <summary> Adds a string to the constant pool of the class being build. Does nothing
		/// if the constant pool already contains a similar item.
		/// 
		/// </summary>
		/// <param name="value">the String value.
		/// </param>
		/// <returns> a new or already existing string item.
		/// </returns>
		
		private Item newString(System.String value_Renamed)
		{
			key2.set_Renamed(STR, value_Renamed, null, null);
			Item result = get_Renamed(key2);
			if (result == null)
			{
				pool.put12(STR, newUTF8(value_Renamed).index);
				result = new Item(index++, key2);
				put(result);
			}
			return result;
		}
		
		/// <summary> Adds a name and type to the constant pool of the class being build. Does
		/// nothing if the constant pool already contains a similar item.
		/// 
		/// </summary>
		/// <param name="name">a name.
		/// </param>
		/// <param name="desc">a type descriptor.
		/// </param>
		/// <returns> a new or already existing name and type item.
		/// </returns>
		
		private Item newNameType(System.String name, System.String desc)
		{
			key2.set_Renamed(NAME_TYPE, name, desc, null);
			Item result = get_Renamed(key2);
			if (result == null)
			{
				put122(NAME_TYPE, newUTF8(name).index, newUTF8(desc).index);
				result = new Item(index++, key2);
				put(result);
			}
			return result;
		}
		
		/// <summary> Returns the constant pool's hash table item which is equal to the given
		/// item.
		/// 
		/// </summary>
		/// <param name="key">a constant pool item.
		/// </param>
		/// <returns> the constant pool's hash table item which is equal to the given
		/// item, or <tt>null</tt> if there is no such item.
		/// </returns>
		
		private Item get_Renamed(Item key)
		{
			Item[] tab = table;
			int hashCode = key.hashCode;
			int index = (hashCode & 0x7FFFFFFF) % tab.Length;
			for (Item i = tab[index]; i != null; i = i.next)
			{
				if (i.hashCode == hashCode && key.isEqualTo(i))
				{
					return i;
				}
			}
			return null;
		}
		
		/// <summary> Puts the given item in the constant pool's hash table. The hash table
		/// <i>must</i> not already contains this item.
		/// 
		/// </summary>
		/// <param name="i">the item to be added to the constant pool's hash table.
		/// </param>
		
		private void  put(Item i)
		{
			if (this.index > threshold)
			{
				int oldCapacity = table.Length;
				Item[] oldMap = table;
				int newCapacity = oldCapacity * 2 + 1;
				Item[] newMap = new Item[newCapacity];
				//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
				threshold = (int) (newCapacity * 0.75);
				table = newMap;
				for (int j = oldCapacity; j-- > 0; )
				{
					for (Item old = oldMap[j]; old != null; )
					{
						Item e = old;
						old = old.next;
						int index = (e.hashCode & 0x7FFFFFFF) % newCapacity;
						e.next = newMap[index];
						newMap[index] = e;
					}
				}
			}
			int index2 = (i.hashCode & 0x7FFFFFFF) % table.Length;
			i.next = table[index2];
			table[index2] = i;
		}
		
		/// <summary> Puts one byte and two shorts into the constant pool.
		/// 
		/// </summary>
		/// <param name="b">a byte.
		/// </param>
		/// <param name="s1">a short.
		/// </param>
		/// <param name="s2">another short.
		/// </param>
		
		private void  put122(int b, int s1, int s2)
		{
			pool.put12(b, s1).put2(s2);
		}
		/// <summary> Computes the instruction types of JVM opcodes.</summary>
		static ClassWriter()
		{
			
			{
				int i;
				sbyte[] b = new sbyte[220];
				System.String s = "AAAAAAAAAAAAAAAABCKLLDDDDDEEEEEEEEEEEEEEEEEEEEAAAAAAAADDDDDEEEEEEEEE" + "EEEEEEEEEEEAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAMAAA" + "AAAAAAAAAAAAAAAAAIIIIIIIIIIIIIIIIDNOAAAAAAGGGGGGGHAFBFAAFFAAQPIIJJII" + "IIIIIIIIIIIIIIII";
				for (i = 0; i < b.Length; ++i)
				{
					b[i] = (sbyte) (s[i] - 'A');
				}
				TYPE = b;
				
				/* code to generate the above string
				
				// SBYTE_INSN instructions
				b[Constants.NEWARRAY] = SBYTE_INSN;
				b[Constants.BIPUSH] = SBYTE_INSN;
				
				// SHORT_INSN instructions
				b[Constants.SIPUSH] = SHORT_INSN;
				
				// (IMPL)VAR_INSN instructions
				b[Constants.RET] = VAR_INSN;
				for (i = Constants.ILOAD; i <= Constants.ALOAD; ++i) {
				b[i] = VAR_INSN;
				}
				for (i = Constants.ISTORE; i <= Constants.ASTORE; ++i) {
				b[i] = VAR_INSN;
				}
				for (i = 26; i <= 45; ++i) { // ILOAD_0 to ALOAD_3
				b[i] = IMPLVAR_INSN;
				}
				for (i = 59; i <= 78; ++i) { // ISTORE_0 to ASTORE_3
				b[i] = IMPLVAR_INSN;
				}
				
				// TYPE_INSN instructions
				b[Constants.NEW] = TYPE_INSN;
				b[Constants.ANEWARRAY] = TYPE_INSN;
				b[Constants.CHECKCAST] = TYPE_INSN;
				b[Constants.INSTANCEOF] = TYPE_INSN;
				
				// (Set)FIELDORMETH_INSN instructions
				for (i = Constants.GETSTATIC; i <= Constants.INVOKESTATIC; ++i) {
				b[i] = FIELDORMETH_INSN;
				}
				b[Constants.INVOKEINTERFACE] = ITFMETH_INSN;
				
				// LABEL(W)_INSN instructions
				for (i = Constants.IFEQ; i <= Constants.JSR; ++i) {
				b[i] = LABEL_INSN;
				}
				b[Constants.IFNULL] = LABEL_INSN;
				b[Constants.IFNONNULL] = LABEL_INSN;
				b[200] = LABELW_INSN; // GOTO_W
				b[201] = LABELW_INSN; // JSR_W
				// temporary opcodes used internally by ASM - see Label and CodeWriter
				for (i = 202; i < 220; ++i) {
				b[i] = LABEL_INSN;
				}
				
				// LDC(_W) instructions
				b[Constants.LDC] = LDC_INSN;
				b[19] = LDCW_INSN; // LDC_W
				b[20] = LDCW_INSN; // LDC2_W
				
				// special instructions
				b[Constants.IINC] = IINC_INSN;
				b[Constants.TABLESWITCH] = TABL_INSN;
				b[Constants.LOOKUPSWITCH] = LOOK_INSN;
				b[Constants.MULTIANEWARRAY] = MANA_INSN;
				b[196] = WIDE_INSN; // WIDE
				
				for (i = 0; i < b.length; ++i) {
				System.err.print((char)('A' + b[i]));
				}
				System.err.println();
				*/
			}
		}
	}
}