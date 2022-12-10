/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
using System.Collections.Generic;

namespace VassalSharp.tools
{
	
	/// <summary> Utility class for associating an Action with a keystroke from multiple
	/// different component sources
	/// 
	/// </summary>
	/// <seealso cref="VassalSharp.build.GameModule.addKeyStrokeListener">
	/// </seealso>
	/// <seealso cref="VassalSharp.build.GameModule.addKeyStrokeSource">
	/// </seealso>
	public class KeyStrokeListener
	{
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		private ActionListener l;
		private KeyStroke key;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < KeyStrokeSource > sources = new List < KeyStrokeSource >();
		
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public KeyStrokeListener(ActionListener l, KeyStroke key)
		{
			this.l = l;
			setKeyStroke(key);
		}
		
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public KeyStrokeListener(ActionListener l):this(l, null)
		{
		}
		
		public virtual void  setKeyStroke(KeyStroke newKey)
		{
			if (newKey != null && newKey.KeyValue == 0)
			{
				newKey = null;
			}
			if (key != null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				foreach (KeyStrokeSource s in sources)
				{
					//s.Component.unregisterKeyboardAction(key);
				}
			}
			key = newKey;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			foreach(KeyStrokeSource s in sources)
			{
				addKeyStrokeSource(s);
			}
		}
		
		public virtual KeyStroke getKeyStroke()
		{
			return key;
		}
		
		public virtual void  keyPressed(System.Windows.Forms.KeyEventArgs stroke)
		{
			if (stroke != null && stroke.Equals(key))
			{
				//UPGRADE_TODO: Method 'java.awt.event.ActionListener.actionPerformed' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
				l.actionPerformed(new System.EventArgs());
			}
		}
		
		public virtual void  addKeyStrokeSource(KeyStrokeSource src)
		{
			if (!sources.Contains(src))
			{
				sources.Add(src);
			}
			if (key != null)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.registerKeyboardAction' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentregisterKeyboardAction_javaawteventActionListener_javaxswingKeyStroke_int'"
				//src.Component.registerKeyboardAction(l, key, src.Mode);
			}
		}
	}
}