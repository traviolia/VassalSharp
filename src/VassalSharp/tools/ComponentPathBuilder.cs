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
using Configurable = VassalSharp.build.Configurable;
using GameModule = VassalSharp.build.GameModule;
namespace VassalSharp.tools
{
	
	/// <summary> Provides an XPath-like syntax for identifying configuration components</summary>
	public class ComponentPathBuilder
	{
		public ComponentPathBuilder()
		{
			InitBlock();
		}
		private void  InitBlock()
		{
			if (st.hasMoreTokens())
			{
				System.String id = st.nextToken();
				System.String name = null;
				SequenceEncoder.Decoder st2 = new SequenceEncoder.Decoder(id, ':');
				System.String className = st2.nextToken();
				if (st2.hasMoreTokens())
				{
					name = st2.nextToken();
				}
				Configurable[] children = parent.getConfigureComponents();
				Configurable match = null;
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ArrayList < Configurable > partialMatches = new ArrayList < Configurable >();
				int i = - 1;
				while (++i < children.Length)
				{
					if (className.Equals(children[i].GetType().FullName))
					{
						partialMatches.add(children[i]);
						if (name == null?children[i].getConfigureName() == null:name.Equals(children[i].getConfigureName()))
						{
							match = children[i];
							break;
						}
					}
				}
				if (match != null)
				{
					path.add(match);
					addToPath(match, st, path);
				}
				else if (!partialMatches.isEmpty())
				{
					if (!st.hasMoreTokens())
					{
						path.add(partialMatches.get_Renamed(0));
					}
					else
					{
						//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
						//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
						for(Configurable candidate: partialMatches)
						{
							//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
							ArrayList < Configurable > l = new ArrayList < Configurable >();
							try
							{
								addToPath(candidate, st.copy(), l);
								subPath = l;
								// FIXME: adding to front of an ArrayList! Should we use LinkedList instead?
								subPath.add(0, candidate);
								break;
							}
							catch (PathFormatException e)
							{
								// No match found here.  Continue
							}
						}
						if (subPath != null)
						{
							path.addAll(subPath);
						}
						else
						{
							findFailed(className, name, parent);
						}
					}
				}
				else
				{
					findFailed(className, name, parent);
				}
			}
		}
		public static ComponentPathBuilder Instance
		{
			get
			{
				if (instance == null)
				{
					instance = new ComponentPathBuilder();
				}
				return instance;
			}
			
		}
		
		private static ComponentPathBuilder instance;
		
		
		/// <summary> Return a string identifying the specified {@link Configurable}
		/// components as a paththrough the configuration parent-child hierarchy.
		/// 
		/// </summary>
		/// <param name="targetPath">
		/// </param>
		/// <returns>
		/// </returns>
		public virtual System.String getId(Configurable[] targetPath)
		{
			SequenceEncoder se = new SequenceEncoder('/');
			for (int i = 0; i < targetPath.Length; ++i)
			{
				System.String name = targetPath[i].getConfigureName();
				SequenceEncoder se2 = new SequenceEncoder(targetPath[i].GetType().FullName, ':');
				if (name != null)
				{
					se2.append(name);
				}
				se.append(se2.Value);
			}
			return se.Value == null?"":se.Value;
		}
		
		/// <summary> Return a list of {@link Configurable} components specified by the
		/// given identifier.
		/// 
		/// </summary>
		/// <param name="id">
		/// </param>
		/// <returns>
		/// </returns>
		/// <throws>  PathFormatException if no such component exists </throws>
		public virtual Configurable[] getPath(System.String id)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < Configurable > list = new ArrayList < Configurable >();
			if (id.Length > 0)
			{
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(id, '/');
				addToPath(GameModule.getGameModule(), st, list);
			}
			return list.toArray(new Configurable[list.size()]);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void addToPath(Configurable parent, 
		SequenceEncoder.Decoder st, 
		List < Configurable > path) 
		throws PathFormatException
		
		private void  findFailed(System.String className, System.String name, Configurable parent)
		{
			
			System.String msgName = name;
			if (msgName == null)
			{
				msgName = className.Substring(className.LastIndexOf('.') + 1);
			}
			throw new PathFormatException("Could not find " + msgName + " in " + VassalSharp.configure.ConfigureTree.getConfigureName(parent.GetType()));
		}
		
		[Serializable]
		public class PathFormatException:System.Exception
		{
			private const long serialVersionUID = 1L;
			
			public PathFormatException(System.String message):base(message)
			{
			}
		}
	}
}