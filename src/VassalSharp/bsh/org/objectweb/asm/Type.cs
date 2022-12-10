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
	
	/// <summary> A Java type. This class can be used to make it easier to manipulate type
	/// and method descriptors.
	/// </summary>
	
	public class Type
	{
		/// <summary> Returns the sort of this Java type.
		/// 
		/// </summary>
		/// <returns> {@link #VOID VOID}, {@link #BOOLEAN BOOLEAN}, {@link #CHAR CHAR},
		/// {@link #BYTE BYTE}, {@link #SHORT SHORT}, {@link #INT INT}, {@link
		/// #FLOAT FLOAT}, {@link #LONG LONG}, {@link #DOUBLE DOUBLE}, {@link
		/// #ARRAY ARRAY} or {@link #OBJECT OBJECT}.
		/// </returns>
		virtual public int Sort
		{
			
			
			get
			{
				return sort;
			}
			
		}
		/// <summary> Returns the number of dimensions of this array type.
		/// This method should only be used for an array type.
		/// 
		/// </summary>
		/// <returns> the number of dimensions of this array type.
		/// </returns>
		virtual public int Dimensions
		{
			
			
			get
			{
				int i = 1;
				while (buf[off + i] == '[')
				{
					++i;
				}
				return i;
			}
			
		}
		/// <summary> Returns the type of the elements of this array type.
		/// This method should only be used for an array type.
		/// 
		/// </summary>
		/// <returns> Returns the type of the elements of this array type.
		/// </returns>
		virtual public Type ElementType
		{
			
			
			get
			{
				return getType(buf, off + Dimensions);
			}
			
		}
		/// <summary> Returns the name of the class corresponding to this object type.
		/// This method should only be used for an object type.
		/// 
		/// </summary>
		/// <returns> the fully qualified name of the class corresponding to this object
		/// type.
		/// </returns>
		virtual public System.String ClassName
		{
			
			
			get
			{
				return new System.String(buf, off + 1, len - 2).Replace('/', '.');
			}
			
		}
		/// <summary> Returns the size of values of this type.
		/// 
		/// </summary>
		/// <returns> the size of values of this type, i.e., 2 for <tt>long</tt> and
		/// <tt>double</tt>, and 1 otherwise.
		/// </returns>
		virtual public int Size
		{
			
			
			get
			{
				return (sort == LONG || sort == DOUBLE?2:1);
			}
			
		}
		
		/// <summary> The sort of the <tt>void</tt> type. See {@link #getSort getSort}.</summary>
		
		public const int VOID = 0;
		
		/// <summary> The sort of the <tt>boolean</tt> type. See {@link #getSort getSort}.</summary>
		
		public const int BOOLEAN = 1;
		
		/// <summary> The sort of the <tt>char</tt> type. See {@link #getSort getSort}.</summary>
		
		public const int CHAR = 2;
		
		/// <summary> The sort of the <tt>byte</tt> type. See {@link #getSort getSort}.</summary>
		
		public const int BYTE = 3;
		
		/// <summary> The sort of the <tt>short</tt> type. See {@link #getSort getSort}.</summary>
		
		public const int SHORT = 4;
		
		/// <summary> The sort of the <tt>int</tt> type. See {@link #getSort getSort}.</summary>
		
		public const int INT = 5;
		
		/// <summary> The sort of the <tt>float</tt> type. See {@link #getSort getSort}.</summary>
		
		public const int FLOAT = 6;
		
		/// <summary> The sort of the <tt>long</tt> type. See {@link #getSort getSort}.</summary>
		
		public const int LONG = 7;
		
		/// <summary> The sort of the <tt>double</tt> type. See {@link #getSort getSort}.</summary>
		
		public const int DOUBLE = 8;
		
		/// <summary> The sort of array reference types. See {@link #getSort getSort}.</summary>
		
		public const int ARRAY = 9;
		
		/// <summary> The sort of object reference type. See {@link #getSort getSort}.</summary>
		
		public const int OBJECT = 10;
		
		/// <summary> The <tt>void</tt> type.</summary>
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'VOID_TYPE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly Type VOID_TYPE = new Type(VOID);
		
		/// <summary> The <tt>boolean</tt> type.</summary>
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'BOOLEAN_TYPE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly Type BOOLEAN_TYPE = new Type(BOOLEAN);
		
		/// <summary> The <tt>char</tt> type.</summary>
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'CHAR_TYPE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly Type CHAR_TYPE = new Type(CHAR);
		
		/// <summary> The <tt>byte</tt> type.</summary>
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'BYTE_TYPE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly Type BYTE_TYPE = new Type(BYTE);
		
		/// <summary> The <tt>short</tt> type.</summary>
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'SHORT_TYPE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly Type SHORT_TYPE = new Type(SHORT);
		
		/// <summary> The <tt>int</tt> type.</summary>
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'INT_TYPE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly Type INT_TYPE = new Type(INT);
		
		/// <summary> The <tt>float</tt> type.</summary>
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'FLOAT_TYPE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly Type FLOAT_TYPE = new Type(FLOAT);
		
		/// <summary> The <tt>long</tt> type.</summary>
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'LONG_TYPE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly Type LONG_TYPE = new Type(LONG);
		
		/// <summary> The <tt>double</tt> type.</summary>
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'DOUBLE_TYPE '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		public static readonly Type DOUBLE_TYPE = new Type(DOUBLE);
		
		// --------------------------------------------------------------------------
		// Fields
		// --------------------------------------------------------------------------
		
		/// <summary> The sort of this Java type.</summary>
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'sort '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private int sort;
		
		/// <summary> A buffer containing the descriptor of this Java type.
		/// This field is only used for reference types.
		/// </summary>
		
		private char[] buf;
		
		/// <summary> The offset of the descriptor of this Java type in {@link #buf buf}.
		/// This field is only used for reference types.
		/// </summary>
		
		private int off;
		
		/// <summary> The length of the descriptor of this Java type.</summary>
		
		private int len;
		
		// --------------------------------------------------------------------------
		// Constructors
		// --------------------------------------------------------------------------
		
		/// <summary> Constructs a primitive type.
		/// 
		/// </summary>
		/// <param name="sort">the sort of the primitive type to be constructed.
		/// </param>
		
		private Type(int sort)
		{
			this.sort = sort;
			this.len = 1;
		}
		
		/// <summary> Constructs a reference type.
		/// 
		/// </summary>
		/// <param name="sort">the sort of the reference type to be constructed.
		/// </param>
		/// <param name="buf">a buffer containing the descriptor of the previous type.
		/// </param>
		/// <param name="off">the offset of this descriptor in the previous buffer.
		/// </param>
		/// <param name="len">the length of this descriptor.
		/// </param>
		
		private Type(int sort, char[] buf, int off, int len)
		{
			this.sort = sort;
			this.buf = buf;
			this.off = off;
			this.len = len;
		}
		
		/// <summary> Returns the Java type corresponding to the given type descriptor.
		/// 
		/// </summary>
		/// <param name="typeDescriptor">a type descriptor.
		/// </param>
		/// <returns> the Java type corresponding to the given type descriptor.
		/// </returns>
		
		public static Type getType(System.String typeDescriptor)
		{
			return getType(typeDescriptor.ToCharArray(), 0);
		}
		
		/// <summary> Returns the Java type corresponding to the given class.
		/// 
		/// </summary>
		/// <param name="c">a class.
		/// </param>
		/// <returns> the Java type corresponding to the given class.
		/// </returns>
		
		public static Type getType(System.Type c)
		{
			if (c.IsPrimitive)
			{
				if (c == System.Type.GetType("System.Int32"))
				{
					return INT_TYPE;
				}
				else
				{
					if (c == System.Type.GetType("System.Void"))
					{
						return VOID_TYPE;
					}
					else if (c == System.Type.GetType("System.Boolean"))
					{
						return BOOLEAN_TYPE;
					}
					else if (c == System.Type.GetType("System.SByte"))
					{
						return BYTE_TYPE;
					}
					else if (c == System.Type.GetType("System.Char"))
					{
						return CHAR_TYPE;
					}
					else if (c == System.Type.GetType("System.Int16"))
					{
						return SHORT_TYPE;
					}
					else if (c == System.Type.GetType("System.Double"))
					{
						return DOUBLE_TYPE;
					}
					else if (c == System.Type.GetType("System.Single"))
					{
						return FLOAT_TYPE;
					}
					/*if (c == Long.TYPE)*/
					else
					{
						return LONG_TYPE;
					}
				}
			}
			else
			{
				return getType(getDescriptor(c));
			}
		}
		
		/// <summary> Returns the Java types corresponding to the argument types of the given
		/// method descriptor.
		/// 
		/// </summary>
		/// <param name="methodDescriptor">a method descriptor.
		/// </param>
		/// <returns> the Java types corresponding to the argument types of the given
		/// method descriptor.
		/// </returns>
		
		public static Type[] getArgumentTypes(System.String methodDescriptor)
		{
			char[] buf = methodDescriptor.ToCharArray();
			int off = 1;
			int size = 0;
			while (true)
			{
				char car = buf[off++];
				if (car == ')')
				{
					break;
				}
				else if (car == 'L')
				{
					while (buf[off++] != ';')
					{
					}
					++size;
				}
				else if (car != '[')
				{
					++size;
				}
			}
			Type[] args = new Type[size];
			off = 1;
			size = 0;
			while (buf[off] != ')')
			{
				args[size] = getType(buf, off);
				off += args[size].len;
				size += 1;
			}
			return args;
		}
		
		/// <summary> Returns the Java types corresponding to the argument types of the given
		/// method.
		/// 
		/// </summary>
		/// <param name="method">a method.
		/// </param>
		/// <returns> the Java types corresponding to the argument types of the given
		/// method.
		/// </returns>
		
		public static Type[] getArgumentTypes(System.Reflection.MethodInfo method)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.Type[] classes = method.GetParameters();
			Type[] types = new Type[classes.Length];
			for (int i = classes.Length - 1; i >= 0; --i)
			{
				types[i] = getType(classes[i]);
			}
			return types;
		}
		
		/// <summary> Returns the Java type corresponding to the return type of the given
		/// method descriptor.
		/// 
		/// </summary>
		/// <param name="methodDescriptor">a method descriptor.
		/// </param>
		/// <returns> the Java type corresponding to the return type of the given
		/// method descriptor.
		/// </returns>
		
		public static Type getReturnType(System.String methodDescriptor)
		{
			char[] buf = methodDescriptor.ToCharArray();
			return getType(buf, methodDescriptor.IndexOf(')') + 1);
		}
		
		/// <summary> Returns the Java type corresponding to the return type of the given
		/// method.
		/// 
		/// </summary>
		/// <param name="method">a method.
		/// </param>
		/// <returns> the Java type corresponding to the return type of the given
		/// method.
		/// </returns>
		
		public static Type getReturnType(System.Reflection.MethodInfo method)
		{
			return getType(method.ReturnType);
		}
		
		/// <summary> Returns the Java type corresponding to the given type descriptor.
		/// 
		/// </summary>
		/// <param name="buf">a buffer containing a type descriptor.
		/// </param>
		/// <param name="off">the offset of this descriptor in the previous buffer.
		/// </param>
		/// <returns> the Java type corresponding to the given type descriptor.
		/// </returns>
		
		private static Type getType(char[] buf, int off)
		{
			int len;
			switch (buf[off])
			{
				
				case 'V':  return VOID_TYPE;
				
				case 'Z':  return BOOLEAN_TYPE;
				
				case 'C':  return CHAR_TYPE;
				
				case 'B':  return BYTE_TYPE;
				
				case 'S':  return SHORT_TYPE;
				
				case 'I':  return INT_TYPE;
				
				case 'F':  return FLOAT_TYPE;
				
				case 'J':  return LONG_TYPE;
				
				case 'D':  return DOUBLE_TYPE;
				
				case '[': 
					len = 1;
					while (buf[off + len] == '[')
					{
						++len;
					}
					if (buf[off + len] == 'L')
					{
						++len;
						while (buf[off + len] != ';')
						{
							++len;
						}
					}
					return new Type(ARRAY, buf, off, len + 1);
					//case 'L':
				
				default: 
					len = 1;
					while (buf[off + len] != ';')
					{
						++len;
					}
					return new Type(OBJECT, buf, off, len + 1);
				
			}
		}
		
		// --------------------------------------------------------------------------
		// Accessors
		// --------------------------------------------------------------------------
		
		/// <summary> Returns the internal name of the class corresponding to this object type.
		/// The internal name of a class is its fully qualified name, where '.' are
		/// replaced by '/'.   * This method should only be used for an object type.
		/// 
		/// </summary>
		/// <returns> the internal name of the class corresponding to this object type.
		/// </returns>
		
		public virtual System.String getInternalName()
		{
			return new System.String(buf, off + 1, len - 2);
		}
		
		// --------------------------------------------------------------------------
		// Conversion to type descriptors
		// --------------------------------------------------------------------------
		
		/// <summary> Returns the descriptor corresponding to this Java type.
		/// 
		/// </summary>
		/// <returns> the descriptor corresponding to this Java type.
		/// </returns>
		
		public virtual System.String getDescriptor()
		{
			System.Text.StringBuilder buf = new System.Text.StringBuilder();
			getDescriptor(buf);
			return buf.ToString();
		}
		
		/// <summary> Returns the descriptor corresponding to the given argument and return
		/// types.
		/// 
		/// </summary>
		/// <param name="returnType">the return type of the method.
		/// </param>
		/// <param name="argumentTypes">the argument types of the method.
		/// </param>
		/// <returns> the descriptor corresponding to the given argument and return
		/// types.
		/// </returns>
		
		public static System.String getMethodDescriptor(Type returnType, Type[] argumentTypes)
		{
			System.Text.StringBuilder buf = new System.Text.StringBuilder();
			buf.Append('(');
			for (int i = 0; i < argumentTypes.Length; ++i)
			{
				argumentTypes[i].getDescriptor(buf);
			}
			buf.Append(')');
			returnType.getDescriptor(buf);
			return buf.ToString();
		}
		
		/// <summary> Appends the descriptor corresponding to this Java type to the given string
		/// buffer.
		/// 
		/// </summary>
		/// <param name="buf">the string buffer to which the descriptor must be appended.
		/// </param>
		
		private void  getDescriptor(System.Text.StringBuilder buf)
		{
			switch (sort)
			{
				
				case VOID:  buf.Append('V'); return ;
				
				case BOOLEAN:  buf.Append('Z'); return ;
				
				case CHAR:  buf.Append('C'); return ;
				
				case BYTE:  buf.Append('B'); return ;
				
				case SHORT:  buf.Append('S'); return ;
				
				case INT:  buf.Append('I'); return ;
				
				case FLOAT:  buf.Append('F'); return ;
				
				case LONG:  buf.Append('J'); return ;
				
				case DOUBLE:  buf.Append('D'); return ;
					//case ARRAY:
					//case OBJECT:
				
				default:  buf.Append(this.buf, off, len);
					break;
				
			}
		}
		
		// --------------------------------------------------------------------------
		// Direct conversion from classes to type descriptors,
		// without intermediate Type objects
		// --------------------------------------------------------------------------
		
		/// <summary> Returns the internal name of the given class. The internal name of a class
		/// is its fully qualified name, where '.' are replaced by '/'.
		/// 
		/// </summary>
		/// <param name="c">an object class.
		/// </param>
		/// <returns> the internal name of the given class.
		/// </returns>
		
		public static System.String getInternalName(System.Type c)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return c.FullName.Replace('.', '/');
		}
		
		/// <summary> Returns the descriptor corresponding to the given Java type.
		/// 
		/// </summary>
		/// <param name="c">an object class, a primitive class or an array class.
		/// </param>
		/// <returns> the descriptor corresponding to the given class.
		/// </returns>
		
		public static System.String getDescriptor(System.Type c)
		{
			System.Text.StringBuilder buf = new System.Text.StringBuilder();
			getDescriptor(buf, c);
			return buf.ToString();
		}
		
		/// <summary> Returns the descriptor corresponding to the given method.
		/// 
		/// </summary>
		/// <param name="m">a {@link Method Method} object.
		/// </param>
		/// <returns> the descriptor of the given method.
		/// </returns>
		
		public static System.String getMethodDescriptor(System.Reflection.MethodInfo m)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.reflect.Method.getParameterTypes' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			System.Type[] parameters = m.GetParameters();
			System.Text.StringBuilder buf = new System.Text.StringBuilder();
			buf.Append('(');
			for (int i = 0; i < parameters.Length; ++i)
			{
				getDescriptor(buf, parameters[i]);
			}
			buf.Append(')');
			getDescriptor(buf, m.ReturnType);
			return buf.ToString();
		}
		
		/// <summary> Appends the descriptor of the given class to the given string buffer.
		/// 
		/// </summary>
		/// <param name="buf">the string buffer to which the descriptor must be appended.
		/// </param>
		/// <param name="c">the class whose descriptor must be computed.
		/// </param>
		
		private static void  getDescriptor(System.Text.StringBuilder buf, System.Type c)
		{
			System.Type d = c;
			while (true)
			{
				if (d.IsPrimitive)
				{
					char car;
					if (d == System.Type.GetType("System.Int32"))
					{
						car = 'I';
					}
					else
					{
						if (d == System.Type.GetType("System.Void"))
						{
							car = 'V';
						}
						else if (d == System.Type.GetType("System.Boolean"))
						{
							car = 'Z';
						}
						else if (d == System.Type.GetType("System.SByte"))
						{
							car = 'B';
						}
						else if (d == System.Type.GetType("System.Char"))
						{
							car = 'C';
						}
						else if (d == System.Type.GetType("System.Int16"))
						{
							car = 'S';
						}
						else if (d == System.Type.GetType("System.Double"))
						{
							car = 'D';
						}
						else if (d == System.Type.GetType("System.Single"))
						{
							car = 'F';
						}
						/*if (d == Long.TYPE)*/
						else
						{
							car = 'J';
						}
					}
					buf.Append(car);
					return ;
				}
				else if (d.IsArray)
				{
					buf.Append('[');
					d = d.GetElementType();
				}
				else
				{
					buf.Append('L');
					//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Class.getName' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					System.String name = d.FullName;
					int len = name.Length;
					for (int i = 0; i < len; ++i)
					{
						char car = name[i];
						buf.Append(car == '.'?'/':car);
					}
					buf.Append(';');
					return ;
				}
			}
		}
		
		// --------------------------------------------------------------------------
		// Corresponding size and opcodes
		// --------------------------------------------------------------------------
		
		/// <summary> Returns a JVM instruction opcode adapted to this Java type.
		/// 
		/// </summary>
		/// <param name="opcode">a JVM instruction opcode. This opcode must be one of ILOAD,
		/// ISTORE, IALOAD, IASTORE, IADD, ISUB, IMUL, IDIV, IREM, INEG, ISHL,
		/// ISHR, IUSHR, IAND, IOR, IXOR and IRETURN.
		/// </param>
		/// <returns> an opcode that is similar to the given opcode, but adapted to this
		/// Java type. For example, if this type is <tt>float</tt> and
		/// <tt>opcode</tt> is IRETURN, this method returns FRETURN.
		/// </returns>
		
		public virtual int getOpcode(int opcode)
		{
			if (opcode == bsh.org.objectweb.asm.Constants_Fields.IALOAD || opcode == bsh.org.objectweb.asm.Constants_Fields.IASTORE)
			{
				switch (sort)
				{
					
					case VOID: 
						return opcode + 5;
					
					case BOOLEAN: 
					case BYTE: 
						return opcode + 6;
					
					case CHAR: 
						return opcode + 7;
					
					case SHORT: 
						return opcode + 8;
					
					case INT: 
						return opcode;
					
					case FLOAT: 
						return opcode + 2;
					
					case LONG: 
						return opcode + 1;
					
					case DOUBLE: 
						return opcode + 3;
						//case ARRAY:
						//case OBJECT:
					
					default: 
						return opcode + 4;
					
				}
			}
			else
			{
				switch (sort)
				{
					
					case VOID: 
						return opcode + 5;
					
					case BOOLEAN: 
					case CHAR: 
					case BYTE: 
					case SHORT: 
					case INT: 
						return opcode;
					
					case FLOAT: 
						return opcode + 2;
					
					case LONG: 
						return opcode + 1;
					
					case DOUBLE: 
						return opcode + 3;
						//case ARRAY:
						//case OBJECT:
					
					default: 
						return opcode + 4;
					
				}
			}
		}
	}
}