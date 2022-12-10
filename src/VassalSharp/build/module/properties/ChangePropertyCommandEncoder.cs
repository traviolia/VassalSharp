/*
* $Id$
* *
* Copyright (c) 2000-2012 by Rodney Kinney, Brent Easton
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
using Command = VassalSharp.command.Command;
using CommandEncoder = VassalSharp.command.CommandEncoder;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.build.module.properties
{
	
	/// <summary> Encoder for {@link ChangePropertyCommand}s
	/// 
	/// </summary>
	/// <author>  rodneykinney
	/// 
	/// </author>
	public class ChangePropertyCommandEncoder : CommandEncoder
	{
		protected internal const System.String COMMAND_PREFIX = "MutableProperty\t";
		private MutablePropertiesContainer container;
		
		public ChangePropertyCommandEncoder(MutablePropertiesContainer container):base()
		{
			this.container = container;
		}
		
		public virtual Command decode(System.String command)
		{
			Command c = null;
			if (command.StartsWith(COMMAND_PREFIX))
			{
				command = command.Substring(COMMAND_PREFIX.Length);
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(command, '\t');
				System.String key = st.nextToken(null);
				System.String oldValue = st.nextToken(null);
				System.String newValue = st.nextToken(null);
				System.String containerId = st.nextToken("");
				
				if (key != null)
				{
					/*
					* NB. If there is no containerID in the command, then it is a pre-bug fix command. Legacy
					* behaviour is to execute the change on the first matching property found in any container
					*/
					if (containerId.Length == 0 || containerId.Equals(container.MutablePropertiesContainerId))
					{
						MutableProperty p = container.getMutableProperty(key);
						if (p != null)
						{
							c = new ChangePropertyCommand(p, key, oldValue, newValue);
						}
					}
				}
			}
			return c;
		}
		
		public virtual System.String encode(Command c)
		{
			System.String s = null;
			if (c is ChangePropertyCommand)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'cpc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				ChangePropertyCommand cpc = (ChangePropertyCommand) c;
				//UPGRADE_NOTE: Final was removed from the declaration of 'our_cid '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String our_cid = container.MutablePropertiesContainerId;
				//UPGRADE_NOTE: Final was removed from the declaration of 'their_cid '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String their_cid = cpc.Property.Parent.MutablePropertiesContainerId;
				if (our_cid.Equals(their_cid))
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'se '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					SequenceEncoder se = new SequenceEncoder('\t');
					se.append(cpc.PropertyName).append(cpc.OldValue).append(cpc.NewValue).append(our_cid);
					s = COMMAND_PREFIX + se.Value;
				}
			}
			return s;
		}
	}
}