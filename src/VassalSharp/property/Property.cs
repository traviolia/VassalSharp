/*
 * Copyright (c) 2009-2010 by Joel Uckelman
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Library General Public
 * License (LGPL) as published by the Free Software Foundation.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * Library General Public License for more details.
 *
 * You should have received a copy of the GNU Library General Public
 * License along with this library; if not, copies are available
 * at http://www.opensource.org.
 */

using System;

namespace VassalSharp.property
{
	/// <summary>
	/// Represents a named property with a specified type.
	/// A {@code Property} may optionally specify a default value, to be
	/// used when no other value is available.
	/// </summary>
	/// <param name="<T>">the class of the value of this {@code Property}
	/// </param>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public sealed class Property
	{
		public Property()
		{
		}

		/// <summary>The name of this property. </summary>
		public System.String name;

		/// <summary>The class of the value of this property. </summary>
		public Type type;

		/// <summary>The default value of this property. </summary>
		public object def;

		private int hash;

		/// <summary> Creates a {@code Property} with {@code null} as its default value.
		/// 
		/// </summary>
		/// <param name="name">the name of the property
		/// </param>
		/// <param name="type">the class of the value of the property
		/// </param>
		/// <throws>  IllegalArgumentException if {@code name} or {@code type} </throws>
		/// <summary> is {@code null}
		/// </summary>
		public Property(String name, Type type) : this(name, type, null)
		{
		}

		/// <summary> Creates a {@code Property}.
		/// 
		/// </summary>
		/// <param name="name">the name of the property
		/// </param>
		/// <param name="type">the class of the value of the property
		/// </param>
		/// <param name="def"> the default value of the property
		/// </param>
		/// <throws>  IllegalArgumentException if {@code name} or {@code type} </throws>
		/// <summary> is {@code{ null}
		/// </summary>
		public Property(String name, Type type, object def)
		{
			if (name == null)
				throw new System.ArgumentException();
			if (type == null)
				throw new System.ArgumentException();

			this.name = name;
			this.type = type;
			this.def = def;

			// Note: The default value for a Property does not take part in
			// equality comparisons, so is not included in the hash code.
			hash = name.GetHashCode() ^ type.GetHashCode();
		}

		/// <summary>{@inheritDoc} </summary>
		public override int GetHashCode()
		{
			return hash;
		}

		/// <summary>{@inheritDoc} </summary>
		public override bool Equals(System.Object o)
		{
			if (this == o)
				return true;
			if (o == null || this.GetType() != o.GetType())
				return false;

			Property p = (Property)o;
			return name.Equals(p.name) && type.Equals(p.type);
		}
	}
}