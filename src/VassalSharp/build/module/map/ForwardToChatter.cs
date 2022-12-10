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
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using Map = VassalSharp.build.module.Map;
namespace VassalSharp.build.module.map
{
	
	/// <summary> This KeyListener forwards key event from a {@link Map} to the
	/// {@link VassalSharp.build.module.Chatter} The event is forwarded only if
	/// not consumed
	/// 
	/// </summary>
	/// <seealso cref="VassalSharp.build.module.Chatter.keyCommand">
	/// </seealso>
	/// <seealso cref="InputEvent.isConsumed">
	/// </seealso>
	public class ForwardToChatter : Buildable
	{
		public virtual void  build(System.Xml.XmlElement e)
		{
		}
		
		public virtual void  addTo(Buildable parent)
		{
			Map map = (Map) parent;
			map.getView().addKeyListener(this);
		}
		
		public virtual void  add(Buildable b)
		{
		}
		
		public virtual System.Xml.XmlElement getBuildElement(System.Xml.XmlDocument doc)
		{
			return doc.CreateElement(GetType().FullName);
		}
		
		public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			process(event_sender, e);
		}
		
		public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			process(event_sender, e);
		}
		
		public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			process(event_sender, e);
		}
		
		//UPGRADE_NOTE: This method is no longer necessary and it can be commented or removed. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1271'"
		private void  process(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (!e.Handled)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.KeyStroke.getKeyStrokeForEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingKeyStrokegetKeyStrokeForEvent_javaawteventKeyEvent'"
				GameModule.getGameModule().getChatter().keyCommand(KeyStroke.getKeyStrokeForEvent(e));
			}
		}
	}
}