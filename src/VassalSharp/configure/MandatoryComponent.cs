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
	
	/// <summary> Requires that at least one child of a given type
	/// exist within a target component
	/// </summary>
	public class MandatoryComponent : ValidityChecker
	{
		public MandatoryComponent()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			this.requiredChildClass = requiredChildClass;
			this.target = target;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private Class < ? > requiredChildClass;
		private AbstractConfigurable target;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public MandatoryComponent(AbstractConfigurable target, 
		Class < ? > requiredChildClass)
		
		public virtual void  validate(Buildable b, ValidationReport report)
		{
			if (b == this.target && target.getComponentsOf(requiredChildClass).isEmpty())
			{
				report.addWarning(ConfigureTree.getConfigureName(target) + " must contain at least one " + ConfigureTree.getConfigureName(requiredChildClass));
			}
		}
	}
}