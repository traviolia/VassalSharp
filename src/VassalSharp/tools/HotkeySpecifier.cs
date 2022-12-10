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
namespace VassalSharp.tools
{
	
	/// <summary> Text component for specifying a hot key</summary>
	[Serializable]
	public class HotkeySpecifier:System.Windows.Forms.TextBox
	{
		static private System.Int32 state700;
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state700 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		virtual public int Key
		{
			get
			{
				return key;
			}
			
		}
		virtual public System.Windows.Forms.KeyEventArgs KeyStroke
		{
			get
			{
				return stroke;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		private int key;
		private System.Windows.Forms.KeyEventArgs stroke;
		
		public HotkeySpecifier():this(0)
		{
		}
		
		public HotkeySpecifier(int key)
		{
			this.key = key;
			//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
			//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyText' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue.ToString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyText_int'"
			Text = key == 0?"":((System.Windows.Forms.Keys) new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) key).KeyValue).ToString();
			stroke = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) key);
			KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.tools.HotkeySpecifier.keyDown);
			KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyPressed);
			KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyReleased);
			KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyTyped);
		}
		
		public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		}
		
		public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.KeyStroke.getKeyStrokeForEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingKeyStrokegetKeyStrokeForEvent_javaawteventKeyEvent'"
			stroke = KeyStroke.getKeyStrokeForEvent(event_sender, e);
		}
		
		public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
			Text = getString(stroke);
		}
		
		public static System.String getString(System.Windows.Forms.KeyEventArgs k)
		{
			//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyText' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue.ToString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyText_int'"
			System.String s = ((System.Windows.Forms.Keys) new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) k.KeyValue).KeyValue).ToString();
			if (((int) k.Modifiers & (int) System.Windows.Forms.Keys.Shift) > 0)
			{
				//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyText' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue.ToString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyText_int'"
				s = ((System.Windows.Forms.Keys) new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) System.Windows.Forms.Keys.ShiftKey).KeyValue).ToString() + "+" + s;
			}
			if (((int) k.Modifiers & (int) System.Windows.Forms.Keys.Control) > 0)
			{
				//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyText' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue.ToString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyText_int'"
				s = ((System.Windows.Forms.Keys) new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) System.Windows.Forms.Keys.ControlKey).KeyValue).ToString() + "+" + s;
			}
			//UPGRADE_ISSUE: Field 'java.awt.event.InputEvent.META_MASK' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			if (((int) k.Modifiers & KeyEvent.META_MASK) > 0)
			{
				//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyText' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue.ToString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyText_int'"
				//UPGRADE_ISSUE: Field 'java.awt.event.KeyEvent.VK_META' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventKeyEventVK_META_f'"
				s = ((System.Windows.Forms.Keys) new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) KeyEvent.VK_META).KeyValue).ToString() + "+" + s;
			}
			if (((int) k.Modifiers & (int) System.Windows.Forms.Keys.Alt) > 0)
			{
				//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyText' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue.ToString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyText_int'"
				s = ((System.Windows.Forms.Keys) new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) System.Windows.Forms.Keys.Menu).KeyValue).ToString() + "+" + s;
			}
			return s;
		}
		
		public static System.Windows.Forms.KeyEventArgs getStrokeForString(System.String s)
		{
			int index = s.IndexOf(',');
			return new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (System.Int32.Parse(s.Substring(0, (index) - (0))) | System.Int32.Parse(s.Substring(index + 1))));
		}
		
		public static System.String getStringForStroke(System.Windows.Forms.KeyEventArgs stroke)
		{
			return stroke.KeyValue + "," + (int) stroke.Modifiers;
		}
	}
}