/// <summary>**************************************************************************
/// *
/// This file is part of the BeanShell Java Scripting distribution.          *
/// Documentation and updates may be found at http://www.beanshell.org/      *
/// *
/// Sun Public License Notice:                                               *
/// *
/// The contents of this file are subject to the Sun Public License Version  *
/// 1.0 (the "License"); you may not use this file except in compliance with *
/// the License. A copy of the License is available at http://www.sun.com    * 
/// *
/// The Original Code is BeanShell. The Initial Developer of the Original    *
/// Code is Pat Niemeyer. Portions created by Pat Niemeyer are Copyright     *
/// (C) 2000.  All Rights Reserved.                                          *
/// *
/// GNU Public License Notice:                                               *
/// *
/// Alternatively, the contents of this file may be used under the terms of  *
/// the GNU Lesser General Public License (the "LGPL"), in which case the    *
/// provisions of LGPL are applicable instead of those above. If you wish to *
/// allow use of your version of this file only under the  terms of the LGPL *
/// and not to allow others to use your version of this file under the SPL,  *
/// indicate your decision by deleting the provisions above and replace      *
/// them with the notice and other provisions required by the LGPL.  If you  *
/// do not delete the provisions above, a recipient may use your version of  *
/// this file under either the SPL or the LGPL.                              *
/// *
/// Patrick Niemeyer (pat@pat.net)                                           *
/// Author of Learning Java, O'Reilly & Associates                           *
/// http://www.pat.net/~pat/                                                 *
/// *
/// ***************************************************************************
/// </summary>
using System;
using StringUtil = bsh.StringUtil;
using NameSource = bsh.NameSource;
namespace bsh.util
{
	
	/// <summary>NameCompletionTable is a utility that implements simple name completion for 
	/// a collection of names, NameSources, and other NameCompletionTables.
	/// This implementation uses a trivial linear search and comparison...  
	/// </summary>
	[Serializable]
	public class NameCompletionTable:System.Collections.ArrayList, NameCompletion
	{
		/// <summary>Unimplemented - need a collection here </summary>
		internal NameCompletionTable table;
		internal System.Collections.IList sources;
		
		/// <summary>Unimplemented - need a collection of sources here</summary>
		
		
		public NameCompletionTable()
		{
		}
		
		/// <summary>Add a NameCompletionTable, which is more optimized than the more
		/// general NameSource
		/// </summary>
		public void  add(NameCompletionTable table)
		{
			/** Unimplemented - need a collection here */
			if (this.table != null)
				throw new System.SystemException("Unimplemented usage error");
			
			this.table = table;
		}
		
		/// <summary>Add a NameSource which is monitored for names.
		/// Unimplemented - behavior is broken... no updates
		/// </summary>
		public void  add(NameSource source)
		{
			/*
			Unimplemented -
			Need to add an inner class util here that holds the source and
			monitors it by registering a listener
			*/
			if (sources == null)
				sources = new System.Collections.ArrayList();
			
			sources.Add(source);
		}
		
		/// <summary>Add any matching names to list (including any from other tables)</summary>
		protected internal virtual void  getMatchingNames(System.String part, System.Collections.IList found)
		{
			// check our table
			for (int i = 0; i < Count; i++)
			{
				System.String name = (System.String) this[i];
				if (name.StartsWith(part))
					found.Add(name);
			}
			
			// Check other tables.
			/** Unimplemented - need a collection here */
			if (table != null)
				table.getMatchingNames(part, found);
			
			// Check other sources
			// note should add caching in source adapters
			if (sources != null)
				for (int i = 0; i < sources.Count; i++)
				{
					NameSource src = (NameSource) sources[i];
					System.String[] names = src.AllNames;
					for (int j = 0; j < names.Length; j++)
						if (names[j].StartsWith(part))
							found.Add(names[j]);
				}
		}
		
		public virtual System.String[] completeName(System.String part)
		{
			System.Collections.IList found = new System.Collections.ArrayList();
			getMatchingNames(part, found);
			
			if (found.Count == 0)
				return new System.String[0];
			
			// Find the max common prefix
			System.String maxCommon = (System.String) found[0];
			for (int i = 1; i < found.Count && maxCommon.Length > 0; i++)
			{
				maxCommon = StringUtil.maxCommonPrefix(maxCommon, (System.String) found[i]);
				
				// if maxCommon gets as small as part, stop trying
				if (maxCommon.Equals(part))
					break;
			}
			
			// Return max common or all ambiguous
			if (maxCommon.Length > part.Length)
				return new System.String[]{maxCommon};
			else
				return (System.String[]) (SupportClass.ICollectionSupport.ToArray(found, new System.String[0]));
		}
		
		/// <summary>class SourceCache implements NameSource.Listener
		/// {
		/// NameSource src;
		/// SourceMonitor( NameSource src ) {
		/// this.src = src;
		/// }
		/// public void nameSourceChanged( NameSource src ) { 
		/// }
		/// }
		/// </summary>
	}
}