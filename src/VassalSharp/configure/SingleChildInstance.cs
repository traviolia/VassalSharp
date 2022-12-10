/*
* $Id$
*
* Copyright (c) 2004 by Rodney Kinney
*
* This library is free software; you can redistribute it and/or
* modify it under the terms of the GNU Library General Public
* License (LGPL) as published by the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful,
* but WITHOUT ANY WARRANTY; without even the implied warranty of
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using Buildable = VassalSharp.build.Buildable;
namespace VassalSharp.configure
{
	
	/// <summary> Ensures that at most a single instance of a given type
	/// belongs to a given parent
	/// </summary>
	public class SingleChildInstance : ValidityChecker
	{
		public SingleChildInstance()
		{
		}

		private AbstractConfigurable target;
		private Type childClass;
		
		public SingleChildInstance(AbstractConfigurable target,  Type childClass)
		{
			this.childClass = childClass;
			this.target = target;
		}

		public virtual void validate(Buildable b, ValidationReport report)
		{
			//if (b == target && target.getComponentsOf(childClass).size() > 1)
			//{
			//	report.addWarning("No more than one " + ConfigureTree.getConfigureName(childClass) + " allowed in " + ConfigureTree.getConfigureName(target));
			//}
		}
	}
}