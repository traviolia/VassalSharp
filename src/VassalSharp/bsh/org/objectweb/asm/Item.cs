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
	
	/// <summary> A constant pool item. Constant pool items can be created with the 'newXXX'
	/// methods in the {@link ClassWriter} class.
	/// </summary>
	
	sealed class Item
	{
		
		/// <summary> Index of this item in the constant pool.</summary>
		
		internal short index;
		
		/// <summary> Type of this constant pool item. A single class is used to represent all
		/// constant pool item types, in order to minimize the bytecode size of this
		/// package. The value of this field is one of the constants defined in the
		/// {@link ClassWriter ClassWriter} class.
		/// </summary>
		
		internal int type;
		
		/// <summary> Value of this item, for a {@link ClassWriter#INT INT} item.</summary>
		
		internal int intVal;
		
		/// <summary> Value of this item, for a {@link ClassWriter#LONG LONG} item.</summary>
		
		internal long longVal;
		
		/// <summary> Value of this item, for a {@link ClassWriter#FLOAT FLOAT} item.</summary>
		
		internal float floatVal;
		
		/// <summary> Value of this item, for a {@link ClassWriter#DOUBLE DOUBLE} item.</summary>
		
		internal double doubleVal;
		
		/// <summary> First part of the value of this item, for items that do not hold a
		/// primitive value.
		/// </summary>
		
		internal System.String strVal1;
		
		/// <summary> Second part of the value of this item, for items that do not hold a
		/// primitive value.
		/// </summary>
		
		internal System.String strVal2;
		
		/// <summary> Third part of the value of this item, for items that do not hold a
		/// primitive value.
		/// </summary>
		
		internal System.String strVal3;
		
		/// <summary> The hash code value of this constant pool item.</summary>
		
		internal int hashCode;
		
		/// <summary> Link to another constant pool item, used for collision lists in the
		/// constant pool's hash table.
		/// </summary>
		
		internal Item next;
		
		/// <summary> Constructs an uninitialized {@link Item Item} object.</summary>
		
		internal Item()
		{
		}
		
		/// <summary> Constructs a copy of the given item.
		/// 
		/// </summary>
		/// <param name="index">index of the item to be constructed.
		/// </param>
		/// <param name="i">the item that must be copied into the item to be constructed.
		/// </param>
		
		internal Item(short index, Item i)
		{
			this.index = index;
			type = i.type;
			intVal = i.intVal;
			longVal = i.longVal;
			floatVal = i.floatVal;
			doubleVal = i.doubleVal;
			strVal1 = i.strVal1;
			strVal2 = i.strVal2;
			strVal3 = i.strVal3;
			hashCode = i.hashCode;
		}
		
		/// <summary> Sets this item to an {@link ClassWriter#INT INT} item.
		/// 
		/// </summary>
		/// <param name="intVal">the value of this item.
		/// </param>
		
		internal void  set_Renamed(int intVal)
		{
			this.type = ClassWriter.INT;
			this.intVal = intVal;
			this.hashCode = type + intVal;
		}
		
		/// <summary> Sets this item to a {@link ClassWriter#LONG LONG} item.
		/// 
		/// </summary>
		/// <param name="longVal">the value of this item.
		/// </param>
		
		internal void  set_Renamed(long longVal)
		{
			this.type = ClassWriter.LONG;
			this.longVal = longVal;
			this.hashCode = type + (int) longVal;
		}
		
		/// <summary> Sets this item to a {@link ClassWriter#FLOAT FLOAT} item.
		/// 
		/// </summary>
		/// <param name="floatVal">the value of this item.
		/// </param>
		
		internal void  set_Renamed(float floatVal)
		{
			this.type = ClassWriter.FLOAT;
			this.floatVal = floatVal;
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			this.hashCode = type + (int) floatVal;
		}
		
		/// <summary> Sets this item to a {@link ClassWriter#DOUBLE DOUBLE} item.
		/// 
		/// </summary>
		/// <param name="doubleVal">the value of this item.
		/// </param>
		
		internal void  set_Renamed(double doubleVal)
		{
			this.type = ClassWriter.DOUBLE;
			this.doubleVal = doubleVal;
			//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
			this.hashCode = type + (int) doubleVal;
		}
		
		/// <summary> Sets this item to an item that do not hold a primitive value.
		/// 
		/// </summary>
		/// <param name="type">the type of this item.
		/// </param>
		/// <param name="strVal1">first part of the value of this item.
		/// </param>
		/// <param name="strVal2">second part of the value of this item.
		/// </param>
		/// <param name="strVal3">third part of the value of this item.
		/// </param>
		
		internal void  set_Renamed(int type, System.String strVal1, System.String strVal2, System.String strVal3)
		{
			this.type = type;
			this.strVal1 = strVal1;
			this.strVal2 = strVal2;
			this.strVal3 = strVal3;
			switch (type)
			{
				
				case ClassWriter.UTF8: 
				case ClassWriter.STR: 
				case ClassWriter.CLASS: 
					hashCode = type + strVal1.GetHashCode();
					return ;
				
				case ClassWriter.NAME_TYPE: 
					hashCode = type + strVal1.GetHashCode() * strVal2.GetHashCode();
					return ;
					//case ClassWriter.FIELD:
					//case ClassWriter.METH:
					//case ClassWriter.IMETH:
				
				default: 
					hashCode = type + strVal1.GetHashCode() * strVal2.GetHashCode() * strVal3.GetHashCode();
					return ;
				
			}
		}
		
		/// <summary> Indicates if the given item is equal to this one.
		/// 
		/// </summary>
		/// <param name="i">the item to be compared to this one.
		/// </param>
		/// <returns> <tt>true</tt> if the given item if equal to this one,
		/// <tt>false</tt> otherwise.
		/// </returns>
		
		internal bool isEqualTo(Item i)
		{
			if (i.type == type)
			{
				switch (type)
				{
					
					case ClassWriter.INT: 
						return i.intVal == intVal;
					
					case ClassWriter.LONG: 
						return i.longVal == longVal;
					
					case ClassWriter.FLOAT: 
						return i.floatVal == floatVal;
					
					case ClassWriter.DOUBLE: 
						return i.doubleVal == doubleVal;
					
					case ClassWriter.UTF8: 
					case ClassWriter.STR: 
					case ClassWriter.CLASS: 
						return i.strVal1.Equals(strVal1);
					
					case ClassWriter.NAME_TYPE: 
						return i.strVal1.Equals(strVal1) && i.strVal2.Equals(strVal2);
						//case ClassWriter.FIELD:
						//case ClassWriter.METH:
						//case ClassWriter.IMETH:
					
					default: 
						return i.strVal1.Equals(strVal1) && i.strVal2.Equals(strVal2) && i.strVal3.Equals(strVal3);
					
				}
			}
			return false;
		}
	}
}