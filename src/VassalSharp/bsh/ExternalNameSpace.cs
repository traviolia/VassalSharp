using System;
namespace bsh
{
	
	/// <summary>A namespace which maintains an external map of values held in variables in
	/// its scope.  This mechanism provides a standard collections based interface
	/// to the namespace as well as a convenient way to export and view values of
	/// the namespace without the ordinary BeanShell wrappers.   
	/// </p>
	/// Variables are maintained internally in the normal fashion to support
	/// meta-information (such as variable type and visibility modifiers), but
	/// exported and imported in a synchronized way.  Variables are exported each
	/// time they are written by BeanShell.  Imported variables from the map appear
	/// in the BeanShell namespace as untyped variables with no modifiers and
	/// shadow any previously defined variables in the scope. 
	/// <p/>
	/// Note: this class is inherentely dependent on Java 1.2, however it is not
	/// used directly by the core as other than type NameSpace, so no dependency is
	/// introduced.
	/// </summary>
	/*
	Implementation notes:  bsh methods are not currently expored to the
	external namespace.  All that would be required to add this is to override
	setMethod() and provide a friendlier view than vector (currently used) for
	overloaded forms (perhaps a map by method SignatureKey).*/
	[Serializable]
	public class ExternalNameSpace:NameSpace
	{
		//UPGRADE_NOTE: Respective javadoc comments were merged.  It should be changed in order to comply with .NET documentation conventions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1199'"
		/// <summary>Get the map view of this namespace.</summary>
		/// <summary>Set the external Map which to which this namespace synchronizes.
		/// The previous external map is detached from this namespace.  Previous
		/// map values are retained in the external map, but are removed from the
		/// BeanShell namespace.
		/// </summary>
		virtual public System.Collections.IDictionary Map
		{
			get
			{
				return externalMap;
			}
			
			set
			{
				// Detach any existing namespace to preserve it, then clear this
				// namespace and set the new one
				this.externalMap = null;
				clear();
				this.externalMap = value;
			}
			
		}
		
		override public System.String[] VariableNames
		{
			get
			{
				// union of the names in the internal namespace and external map
				//UPGRADE_TODO: Class 'java.util.HashSet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashSet'"
				SupportClass.SetSupport nameSet = new SupportClass.HashSetSupport();
				System.String[] nsNames = base.VariableNames;
				//UPGRADE_TODO: Method 'java.util.Arrays.asList' was converted to 'System.Collections.ArrayList' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilArraysasList_javalangObject[]'"
				nameSet.AddAll(new System.Collections.ArrayList(nsNames));
				//UPGRADE_TODO: Method 'java.util.Map.keySet' was converted to 'SupportClass.HashSetSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilMapkeySet'"
				nameSet.AddAll(new SupportClass.HashSetSupport(externalMap.Keys));
				return (System.String[]) SupportClass.ICollectionSupport.ToArray(nameSet, new System.String[0]);
			}
			
		}
		
		override public Variable[] DeclaredVariables
		{
			/*
			Note: the meaning of getDeclaredVariables() is not entirely clear, but
			the name (and current usage in class generation support) suggests that
			untyped variables should not be inclueded.  Therefore we do not
			currently have to add the external names here.
			*/
			
			get
			{
				return base.DeclaredVariables;
			}
			
		}
		private System.Collections.IDictionary externalMap;
		
		public ExternalNameSpace():this(null, "External Map Namespace", null)
		{
		}
		
		
		public ExternalNameSpace(NameSpace parent, System.String name, System.Collections.IDictionary externalMap):base(parent, name)
		{
			
			if (externalMap == null)
			{
				//UPGRADE_TODO: Class 'java.util.HashMap' was converted to 'System.Collections.Hashtable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilHashMap'"
				externalMap = new System.Collections.Hashtable();
			}
			
			this.externalMap = externalMap;
		}
		
		
		//UPGRADE_NOTE: Access modifiers of method 'setVariable' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override void  setVariable(System.String name, System.Object value_Renamed, bool strictJava, bool recurse)
		{
			base.setVariable(name, value_Renamed, strictJava, recurse);
			putExternalMap(name, value_Renamed);
		}
		
		
		public override void  unsetVariable(System.String name)
		{
			base.unsetVariable(name);
			externalMap.Remove(name);
		}
		
		
		/*
		Notes: This implmenetation of getVariableImpl handles the following
		cases:
		1) var in map not in local scope - var was added through map
		2) var in map and in local scope - var was added through namespace
		3) var not in map but in local scope - var was removed via map
		4) var not in map and not in local scope - non-existent var
		*/
		protected internal override Variable getVariableImpl(System.String name, bool recurse)
		{
			// check the external map for the variable name
			System.Object value_Renamed = externalMap[name];
			
			Variable var;
			if (value_Renamed == null)
			{
				// The var is not in external map and it should therefore not be
				// found in local scope (it may have been removed via the map).  
				// Clear it prophalactically.
				base.unsetVariable(name);
				
				// Search parent for var if applicable.
				var = base.getVariableImpl(name, recurse);
			}
			else
			{
				// Var in external map may be found in local scope with type and
				// modifier info.
				Variable localVar = base.getVariableImpl(name, false);
				
				// If not in local scope then it was added via the external map,
				// we'll wrap it and pass it along.  Else we'll use the local
				// version.
				if (localVar == null)
					var = new Variable(name, (System.Type) null, value_Renamed, (Modifiers) null);
				else
					var = localVar;
			}
			
			return var;
		}
		
		
		public override void  setTypedVariable(System.String name, System.Type type, System.Object value_Renamed, Modifiers modifiers)
		{
			base.setTypedVariable(name, type, value_Renamed, modifiers);
			putExternalMap(name, value_Renamed);
		}
		
		/*
		Note: we could override this method to allow bsh methods to appear in
		the external map.
		*/
		public override void  setMethod(System.String name, BshMethod method)
		{
			base.setMethod(name, method);
		}
		
		/*
		Note: kind of far-fetched, but... we could override this method to
		allow bsh methods to be inserted into this namespace via the map.
		*/
		public override BshMethod getMethod(System.String name, System.Type[] sig, bool declaredOnly)
		{
			return base.getMethod(name, sig, declaredOnly);
		}
		
		
		/*
		Note: this method should be overridden to add the names from the
		external map, as is done in getVariableNames();
		*/
		protected internal override void  getAllNamesAux(System.Collections.ArrayList vec)
		{
			base.getAllNamesAux(vec);
		}
		
		/// <summary>Clear all variables, methods, and imports from this namespace and clear
		/// all values from the external map (via Map clear()).
		/// </summary>
		public override void  clear()
		{
			base.clear();
			externalMap.Clear();
		}
		
		/// <summary>Place an unwrapped value in the external map.
		/// BeanShell primitive types are represented by their object wrappers, so
		/// it is not possible to differentiate between wrapper types and primitive
		/// types via the external Map.
		/// </summary>
		protected internal virtual void  putExternalMap(System.String name, System.Object value_Renamed)
		{
			if (value_Renamed is Variable)
				try
				{
					value_Renamed = unwrapVariable((Variable) value_Renamed);
				}
				catch (UtilEvalError ute)
				{
					// There should be no case for this.  unwrapVariable throws
					// UtilEvalError in some cases where it holds an LHS or array
					// index.
					throw new InterpreterError("unexpected UtilEvalError");
				}
			
			if (value_Renamed is Primitive)
				value_Renamed = Primitive.unwrap((Primitive) value_Renamed);
			
			externalMap[name] = value_Renamed;
		}
		protected ExternalNameSpace(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context):base(info, context)
		{
		}
		public override void  GetObjectData(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}