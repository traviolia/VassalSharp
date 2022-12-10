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
using Command = VassalSharp.command.Command;
using KeyBuffer = VassalSharp.counters.KeyBuffer;
namespace VassalSharp.build.module.map
{
	
	/// <summary> This KeyListener forwards key event from a {@link Map} to the
	/// {@link KeyBuffer}, where it is given to selected GamePieces to
	/// interpret.  The event is forwarded only if not consumed
	/// 
	/// </summary>
	/// <seealso cref="KeyBuffer">
	/// </seealso>
	/// <seealso cref="VassalSharp.counters.GamePiece.keyEvent">
	/// </seealso>
	/// <seealso cref="InputEvent.isConsumed">
	/// </seealso>
	public class ForwardToKeyBuffer : Buildable
	{
		private System.Windows.Forms.KeyEventArgs lastConsumedEvent;
		
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
		protected internal virtual void  process(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			// If we've consumed a KeyPressed event,
			// then automatically consume any following KeyTyped event
			// resulting from the same keypress
			// This prevents echoing characters to the Chat area if they're keycommand for selected pieces
			//UPGRADE_ISSUE: Method 'java.awt.event.InputEvent.getWhen' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			if (lastConsumedEvent != null && lastConsumedEvent.getWhen() == e.getWhen())
			{
				e.Handled = true;
			}
			else
			{
				lastConsumedEvent = null;
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyCode' was converted to 'System.Windows.Forms.KeyEventArgs.KeyChar' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyCode'"
			int c = (int) e.KeyChar;
			// Don't pass SHIFT or CONTROL only to counters
			if (!e.Handled && c != (int) System.Windows.Forms.Keys.ShiftKey && c != (int) System.Windows.Forms.Keys.ControlKey)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.KeyStroke.getKeyStrokeForEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingKeyStrokegetKeyStrokeForEvent_javaawteventKeyEvent'"
				Command comm = KeyBuffer.Buffer.keyCommand(KeyStroke.getKeyStrokeForEvent(e));
				if (comm != null && !comm.Null)
				{
					GameModule.getGameModule().sendAndLog(comm);
					e.Handled = true;
					lastConsumedEvent = e;
				}
			}
		}
	}
}