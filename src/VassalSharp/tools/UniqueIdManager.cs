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
using Buildable = VassalSharp.build.Buildable;
using ConfigureTree = VassalSharp.configure.ConfigureTree;
using ValidationReport = VassalSharp.configure.ValidationReport;
using ValidityChecker = VassalSharp.configure.ValidityChecker;
namespace VassalSharp.tools
{
	
	/// <summary> A class for assigning unique identifiers to objects.  Identifiers will be
	/// of the form prefix#, where prefix is specified at initialization and the #
	/// is an increasing digit. Components will have the same ID provided they
	/// are loaded in the same order.
	/// 
	/// Unfortunately, this approach is flawed. If a module is edited, saved games
	/// from previous versions can become broken. Worse, two players with different
	/// extensions loaded could have incompatible behavior.
	/// 
	/// The preferred way to have unique identifiers is to allow the user to provide
	/// names and use a {@link VassalSharp.configure.ValidityChecker} to ensure that the
	/// names are unique.  This class provides some support for using this approach
	/// while providing backward compatibility with old saved games and modules.
	/// 
	/// Usage:  an {@link Identifyable} instance invokes {@link #add}, typically
	/// during the {@link Buildable#build} method.  Classes can use the
	/// {@link #getIdentifier} method to look up an identifier for that instance,
	/// and can use {@link #findInstance} to look up a component by id.
	/// </summary>
	public class UniqueIdManager : ValidityChecker
	{
		private void  InitBlock()
		{
			return instances.iterator();
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < Identifyable > instances = new ArrayList < Identifyable >();
		private System.String prefix;
		
		public UniqueIdManager(System.String prefix)
		{
			InitBlock();
			this.prefix = prefix;
		}
		
		public virtual void  add(UniqueIdManager.Identifyable i)
		{
			i.Id = prefix + instances.size();
			instances.add(i);
		}
		
		public virtual void  remove(UniqueIdManager.Identifyable i)
		{
			int index = instances.indexOf(i);
			if (index >= 0)
			{
				for (int j = index + 1, n = instances.size(); j < n; ++j)
				{
					instances.get_Renamed(j).setId(prefix + (j - 1));
				}
				instances.remove(index);
			}
		}
		
		/// <summary> Make a best guess for a unique identifier for the target.
		/// Use {@link Identifyable#getConfigureName if non-null, otherwise
		/// use {@link Identifyable#getId
		/// </summary>
		/// <param name="target">
		/// </param>
		/// <returns>
		/// </returns>
		public static System.String getIdentifier(UniqueIdManager.Identifyable target)
		{
			System.String id = target.ConfigureName;
			if (id == null || id.Length == 0)
			{
				id = target.Id;
			}
			return id;
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < Identifyable > getAllInstances()
		
		/// <summary> Return the first instance whose name or id matches the argument</summary>
		/// <param name="id">
		/// </param>
		/// <returns>
		/// </returns>
		public virtual UniqueIdManager.Identifyable findInstance(System.String id)
		{
			if (id != null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Identifyable i: instances)
				{
					if (id.Equals(i.getConfigureName()) || id.Equals(i.getId()))
					{
						return i;
					}
				}
			}
			return null;
		}
		
		/// <summary>Ensures that no other instance of the same class has the same name </summary>
		public virtual void  validate(Buildable target, ValidationReport report)
		{
			if (target is UniqueIdManager.Identifyable)
			{
				UniqueIdManager.Identifyable iTarget = (UniqueIdManager.Identifyable) target;
				if (iTarget.ConfigureName == null || iTarget.ConfigureName.Length == 0)
				{
					report.addWarning("A " + ConfigureTree.getConfigureName(target.GetType()) + " has not been given a name");
				}
				else if (instances.contains(iTarget))
				{
					UniqueIdManager.Identifyable compare = null;
					//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				}
			}
		}
		
		/// <summary> An object with an identifier that can be manipulated by a
		/// {@link UniqueIdManager}
		/// </summary>
		public interface Identifyable
		{
			System.String Id
			{
				get;
				
				set;
				
			}
			System.String ConfigureName
			{
				get;
				// User-assigned name
				
			}
		}
	}
}