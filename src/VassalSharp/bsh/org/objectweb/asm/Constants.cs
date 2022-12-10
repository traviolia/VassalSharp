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
	
	/// <summary> Defines the JVM opcodes, access flags and array type codes. This interface
	/// does not define all the JVM opcodes because some opcodes are automatically
	/// handled. For example, the xLOAD and xSTORE opcodes are automatically replaced
	/// by xLOAD_n and xSTORE_n opcodes when possible. The xLOAD_n and xSTORE_n
	/// opcodes are therefore not defined in this interface. Likewise for LDC,
	/// automatically replaced by LDC_W or LDC2_W when necessary, WIDE, GOTO_W and
	/// JSR_W.
	/// </summary>
	
	public struct Constants_Fields{
		// access flags
		
		public readonly static int ACC_PUBLIC = 1;
		public readonly static int ACC_PRIVATE = 2;
		public readonly static int ACC_PROTECTED = 4;
		public readonly static int ACC_STATIC = 8;
		public readonly static int ACC_FINAL = 16;
		public readonly static int ACC_SYNCHRONIZED = 32;
		public readonly static int ACC_VOLATILE = 64;
		public readonly static int ACC_TRANSIENT = 128;
		public readonly static int ACC_NATIVE = 256;
		public readonly static int ACC_INTERFACE = 512;
		public readonly static int ACC_ABSTRACT = 1024;
		public readonly static int ACC_STRICT = 2048;
		public readonly static int ACC_SUPER = 32;
		public readonly static int ACC_SYNTHETIC = 65536;
		public readonly static int ACC_DEPRECATED = 131072;
		// types for NEWARRAY
		
		public readonly static int T_BOOLEAN = 4;
		public readonly static int T_CHAR = 5;
		public readonly static int T_FLOAT = 6;
		public readonly static int T_DOUBLE = 7;
		public readonly static int T_BYTE = 8;
		public readonly static int T_SHORT = 9;
		public readonly static int T_INT = 10;
		public readonly static int T_LONG = 11;
		// opcodes                  // visit method (- = idem)
		
		public readonly static int NOP = 0; // visitInsn
		public readonly static int ACONST_NULL = 1; // -
		public readonly static int ICONST_M1 = 2; // -
		public readonly static int ICONST_0 = 3; // -
		public readonly static int ICONST_1 = 4; // -
		public readonly static int ICONST_2 = 5; // -
		public readonly static int ICONST_3 = 6; // -
		public readonly static int ICONST_4 = 7; // -
		public readonly static int ICONST_5 = 8; // -
		public readonly static int LCONST_0 = 9; // -
		public readonly static int LCONST_1 = 10; // -
		public readonly static int FCONST_0 = 11; // -
		public readonly static int FCONST_1 = 12; // -
		public readonly static int FCONST_2 = 13; // -
		public readonly static int DCONST_0 = 14; // -
		public readonly static int DCONST_1 = 15; // -
		public readonly static int BIPUSH = 16; // visitIntInsn
		public readonly static int SIPUSH = 17; // -
		public readonly static int LDC = 18; // visitLdcInsn
		//int LDC_W = 19;           // -
		//int LDC2_W = 20;          // -
		public readonly static int ILOAD = 21; // visitVarInsn
		public readonly static int LLOAD = 22; // -
		public readonly static int FLOAD = 23; // -
		public readonly static int DLOAD = 24; // -
		public readonly static int ALOAD = 25; // -
		//int ILOAD_0 = 26;         // -
		//int ILOAD_1 = 27;         // -
		//int ILOAD_2 = 28;         // -
		//int ILOAD_3 = 29;         // -
		//int LLOAD_0 = 30;         // -
		//int LLOAD_1 = 31;         // -
		//int LLOAD_2 = 32;         // -
		//int LLOAD_3 = 33;         // -
		//int FLOAD_0 = 34;         // -
		//int FLOAD_1 = 35;         // -
		//int FLOAD_2 = 36;         // -
		//int FLOAD_3 = 37;         // -
		//int DLOAD_0 = 38;         // -
		//int DLOAD_1 = 39;         // -
		//int DLOAD_2 = 40;         // -
		//int DLOAD_3 = 41;         // -
		//int ALOAD_0 = 42;         // -
		//int ALOAD_1 = 43;         // -
		//int ALOAD_2 = 44;         // -
		//int ALOAD_3 = 45;         // -
		public readonly static int IALOAD = 46; // visitInsn
		public readonly static int LALOAD = 47; // -
		public readonly static int FALOAD = 48; // -
		public readonly static int DALOAD = 49; // -
		public readonly static int AALOAD = 50; // -
		public readonly static int BALOAD = 51; // -
		public readonly static int CALOAD = 52; // -
		public readonly static int SALOAD = 53; // -
		public readonly static int ISTORE = 54; // visitVarInsn
		public readonly static int LSTORE = 55; // -
		public readonly static int FSTORE = 56; // -
		public readonly static int DSTORE = 57; // -
		public readonly static int ASTORE = 58; // -
		//int ISTORE_0 = 59;        // -
		//int ISTORE_1 = 60;        // -
		//int ISTORE_2 = 61;        // -
		//int ISTORE_3 = 62;        // -
		//int LSTORE_0 = 63;        // -
		//int LSTORE_1 = 64;        // -
		//int LSTORE_2 = 65;        // -
		//int LSTORE_3 = 66;        // -
		//int FSTORE_0 = 67;        // -
		//int FSTORE_1 = 68;        // -
		//int FSTORE_2 = 69;        // -
		//int FSTORE_3 = 70;        // -
		//int DSTORE_0 = 71;        // -
		//int DSTORE_1 = 72;        // -
		//int DSTORE_2 = 73;        // -
		//int DSTORE_3 = 74;        // -
		//int ASTORE_0 = 75;        // -
		//int ASTORE_1 = 76;        // -
		//int ASTORE_2 = 77;        // -
		//int ASTORE_3 = 78;        // -
		public readonly static int IASTORE = 79; // visitInsn
		public readonly static int LASTORE = 80; // -
		public readonly static int FASTORE = 81; // -
		public readonly static int DASTORE = 82; // -
		public readonly static int AASTORE = 83; // -
		public readonly static int BASTORE = 84; // -
		public readonly static int CASTORE = 85; // -
		public readonly static int SASTORE = 86; // -
		public readonly static int POP = 87; // -
		public readonly static int POP2 = 88; // -
		public readonly static int DUP = 89; // -
		public readonly static int DUP_X1 = 90; // -
		public readonly static int DUP_X2 = 91; // -
		public readonly static int DUP2 = 92; // -
		public readonly static int DUP2_X1 = 93; // -
		public readonly static int DUP2_X2 = 94; // -
		public readonly static int SWAP = 95; // -
		public readonly static int IADD = 96; // -
		public readonly static int LADD = 97; // -
		public readonly static int FADD = 98; // -
		public readonly static int DADD = 99; // -
		public readonly static int ISUB = 100; // -
		public readonly static int LSUB = 101; // -
		public readonly static int FSUB = 102; // -
		public readonly static int DSUB = 103; // -
		public readonly static int IMUL = 104; // -
		public readonly static int LMUL = 105; // -
		public readonly static int FMUL = 106; // -
		public readonly static int DMUL = 107; // -
		public readonly static int IDIV = 108; // -
		public readonly static int LDIV = 109; // -
		public readonly static int FDIV = 110; // -
		public readonly static int DDIV = 111; // -
		public readonly static int IREM = 112; // -
		public readonly static int LREM = 113; // -
		public readonly static int FREM = 114; // -
		public readonly static int DREM = 115; // -
		public readonly static int INEG = 116; // -
		public readonly static int LNEG = 117; // -
		public readonly static int FNEG = 118; // -
		public readonly static int DNEG = 119; // -
		public readonly static int ISHL = 120; // -
		public readonly static int LSHL = 121; // -
		public readonly static int ISHR = 122; // -
		public readonly static int LSHR = 123; // -
		public readonly static int IUSHR = 124; // -
		public readonly static int LUSHR = 125; // -
		public readonly static int IAND = 126; // -
		public readonly static int LAND = 127; // -
		public readonly static int IOR = 128; // -
		public readonly static int LOR = 129; // -
		public readonly static int IXOR = 130; // -
		public readonly static int LXOR = 131; // -
		public readonly static int IINC = 132; // visitIincInsn
		public readonly static int I2L = 133; // visitInsn
		public readonly static int I2F = 134; // -
		public readonly static int I2D = 135; // -
		public readonly static int L2I = 136; // -
		public readonly static int L2F = 137; // -
		public readonly static int L2D = 138; // -
		public readonly static int F2I = 139; // -
		public readonly static int F2L = 140; // -
		public readonly static int F2D = 141; // -
		public readonly static int D2I = 142; // -
		public readonly static int D2L = 143; // -
		public readonly static int D2F = 144; // -
		public readonly static int I2B = 145; // -
		public readonly static int I2C = 146; // -
		public readonly static int I2S = 147; // -
		public readonly static int LCMP = 148; // -
		public readonly static int FCMPL = 149; // -
		public readonly static int FCMPG = 150; // -
		public readonly static int DCMPL = 151; // -
		public readonly static int DCMPG = 152; // -
		public readonly static int IFEQ = 153; // visitJumpInsn
		public readonly static int IFNE = 154; // -
		public readonly static int IFLT = 155; // -
		public readonly static int IFGE = 156; // -
		public readonly static int IFGT = 157; // -
		public readonly static int IFLE = 158; // -
		public readonly static int IF_ICMPEQ = 159; // -
		public readonly static int IF_ICMPNE = 160; // -
		public readonly static int IF_ICMPLT = 161; // -
		public readonly static int IF_ICMPGE = 162; // -
		public readonly static int IF_ICMPGT = 163; // -
		public readonly static int IF_ICMPLE = 164; // -
		public readonly static int IF_ACMPEQ = 165; // -
		public readonly static int IF_ACMPNE = 166; // -
		public readonly static int GOTO = 167; // -
		public readonly static int JSR = 168; // -
		public readonly static int RET = 169; // visitVarInsn
		public readonly static int TABLESWITCH = 170; // visiTableSwitchInsn
		public readonly static int LOOKUPSWITCH = 171; // visitLookupSwitch
		public readonly static int IRETURN = 172; // visitInsn
		public readonly static int LRETURN = 173; // -
		public readonly static int FRETURN = 174; // -
		public readonly static int DRETURN = 175; // -
		public readonly static int ARETURN = 176; // -
		public readonly static int RETURN = 177; // -
		public readonly static int GETSTATIC = 178; // visitFieldInsn
		public readonly static int PUTSTATIC = 179; // -
		public readonly static int GETFIELD = 180; // -
		public readonly static int PUTFIELD = 181; // -
		public readonly static int INVOKEVIRTUAL = 182; // visitMethodInsn
		public readonly static int INVOKESPECIAL = 183; // -
		public readonly static int INVOKESTATIC = 184; // -
		public readonly static int INVOKEINTERFACE = 185; // -
		//int UNUSED = 186;         // NOT VISITED
		public readonly static int NEW = 187; // visitTypeInsn
		public readonly static int NEWARRAY = 188; // visitIntInsn
		public readonly static int ANEWARRAY = 189; // visitTypeInsn
		public readonly static int ARRAYLENGTH = 190; // visitInsn
		public readonly static int ATHROW = 191; // -
		public readonly static int CHECKCAST = 192; // visitTypeInsn
		public readonly static int INSTANCEOF = 193; // -
		public readonly static int MONITORENTER = 194; // visitInsn
		public readonly static int MONITOREXIT = 195; // -
		//int WIDE = 196;           // NOT VISITED
		public readonly static int MULTIANEWARRAY = 197; // visitMultiANewArrayInsn
		public readonly static int IFNULL = 198; // visitJumpInsn
		public readonly static int IFNONNULL = 199; // -
		//int GOTO_W = 200;         // -
		//int JSR_W = 201;          // -
	}
	public interface Constants
	{
		//UPGRADE_NOTE: Members of interface 'Constants' were extracted into structure 'Constants_Fields'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1045'"
		
	}
}