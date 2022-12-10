using System;
namespace bsh
{
	
	[Serializable]
	public class Variable
	{
		/// <summary>A type of null means loosely typed variable </summary>
		virtual public System.Type Type
		{
			get
			{
				return type;
			}
			
		}
		virtual public System.String TypeDescriptor
		{
			get
			{
				return typeDescriptor;
			}
			
		}
		virtual public Modifiers Modifiers
		{
			get
			{
				return modifiers;
			}
			
		}
		virtual public System.String Name
		{
			get
			{
				return name;
			}
			
		}
		internal const int DECLARATION = 0;
		internal const int ASSIGNMENT = 1;
		/// <summary>A null type means an untyped variable </summary>
		internal System.String name;
		internal System.Type type = null;
		internal System.String typeDescriptor;
		internal System.Object value_Renamed;
		internal Modifiers modifiers;
		internal LHS lhs;
		
		internal Variable(System.String name, System.Type type, LHS lhs)
		{
			this.name = name;
			this.lhs = lhs;
			this.type = type;
		}
		
		internal Variable(System.String name, System.Object value_Renamed, Modifiers modifiers):this(name, (System.Type) null, value_Renamed, modifiers)
		{
		}
		
		/// <summary>This constructor is used in class generation.</summary>
		internal Variable(System.String name, System.String typeDescriptor, System.Object value_Renamed, Modifiers modifiers):this(name, (System.Type) null, value_Renamed, modifiers)
		{
			this.typeDescriptor = typeDescriptor;
		}
		
		/// <param name="value">may be null if this 
		/// </param>
		internal Variable(System.String name, System.Type type, System.Object value_Renamed, Modifiers modifiers)
		{
			
			this.name = name;
			this.type = type;
			this.modifiers = modifiers;
			setValue(value_Renamed, DECLARATION);
		}
		
		/// <summary>Set the value of the typed variable.</summary>
		/// <param name="value">should be an object or wrapped bsh Primitive type.
		/// if value is null the appropriate default value will be set for the
		/// type: e.g. false for boolean, zero for integer types.
		/// </param>
		public virtual void  setValue(System.Object value_Renamed, int context)
		{
			
			// check this.value
			if (hasModifier("final") && this.value_Renamed != null)
				throw new UtilEvalError("Final variable, can't re-assign.");
			
			if (value_Renamed == null)
				value_Renamed = Primitive.getDefaultValue(type);
			
			if (lhs != null)
			{
				lhs.assign(value_Renamed, false);
				return ;
			}
			
			// TODO: should add isJavaCastable() test for strictJava
			// (as opposed to isJavaAssignable())
			if (type != null)
				value_Renamed = Types.castObject(value_Renamed, type, context == DECLARATION?Types.CAST:Types.ASSIGNMENT);
			
			this.value_Renamed = value_Renamed;
		}
		
		/*
		Note: UtilEvalError here comes from lhs.getValue().
		A Variable can represent an LHS for the case of an imported class or
		object field.
		*/
		internal virtual System.Object getValue()
		{
			if (lhs != null)
				return lhs.Value;
			
			return value_Renamed;
		}
		
		public virtual bool hasModifier(System.String name)
		{
			return modifiers != null && modifiers.hasModifier(name);
		}
		
		public override System.String ToString()
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			return "Variable: " + base.ToString() + " " + name + ", type:" + type + ", value:" + value_Renamed + ", lhs = " + lhs;
		}
	}
}