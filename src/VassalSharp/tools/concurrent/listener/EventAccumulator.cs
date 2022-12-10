/*
* $Id$
*
* Copyright (c) 2008-2009 by Joel Uckelman
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
using Pair = VassalSharp.tools.lang.Pair;
namespace VassalSharp.tools.concurrent.listener
{
	
	/// <summary> An {@link EventListener} which accumulates the events it receives into
	/// a {@link List}.
	/// 
	/// </summary>
	/// <since> 3.2.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class EventAccumulator
	{
		private void  InitBlock()
		{
			this.col = col;
			return col;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		< T > implements EventListener < T >
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected final Collection < Pair < Object, T >> col;
		
		public EventAccumulator()
		{
			InitBlock();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			this(new ArrayList < Pair < Object, T >>());
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public EventAccumulator(Collection < Pair < Object, T >> col)
		
		/// <summary>{@inheritDoc} </summary>
		public virtual void  receive(System.Object src, T event_Renamed)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			col.add(new Pair < Object, T >(src, event));
		}
		
		/// <summary> Gets the collection of received source-event pairs.
		/// 
		/// </summary>
		/// <returns> the collection of sources and events received
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Collection < Pair < Object, T >> events()
	}
}