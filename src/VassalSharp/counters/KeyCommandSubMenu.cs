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
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
namespace VassalSharp.counters
{
	
	/// <summary>Represents a sub-menu in a GamePiece's right-click drop-down menu </summary>
	[Serializable]
	public class KeyCommandSubMenu:KeyCommand
	{
		private void  InitBlock()
		{
			return commands.iterator();
		}
		virtual public System.String[] Commands
		{
			set
			{
				commands.clear();
				//UPGRADE_TODO: Method 'java.util.Arrays.asList' was converted to 'System.Collections.ArrayList' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilArraysasList_javalangObject[]'"
				commands.addAll(new System.Collections.ArrayList(value));
			}
			
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < String > commands = new ArrayList < String >();
		
		public KeyCommandSubMenu(System.String name, GamePiece target, TranslatablePiece i18nPiece):base(name, NamedKeyStroke.NULL_KEYSTROKE, target, i18nPiece)
		{
			InitBlock();
		}
		
		public override void  actionPerformed(System.Object event_sender, System.EventArgs evt)
		{
		}
		
		public virtual void  addCommand(System.String s)
		{
			commands.add(s);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Iterator < String > getCommands()
	}
}