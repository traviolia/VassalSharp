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
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.configure
{
	
	/// <summary> A Configurer for {@link KeyStroke} values</summary>
	public class HotKeyConfigurer:Configurer
	{
		static private System.Int32 state435;
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state435 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		override public System.String ValueString
		{
			get
			{
				return encode((System.Windows.Forms.KeyEventArgs) getValue());
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (p == null)
				{
					p = new System.Windows.Forms.Panel();
					//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					p.setLayout(new BoxLayout(p, BoxLayout.X_AXIS));
					//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
					tf = new System.Windows.Forms.TextBox();
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetMaximumSize'"
					tf.setMaximumSize(new System.Drawing.Size(tf.getMaximumSize().Width, tf.Size.Height));
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					tf.Text = keyToString();
					tf.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.configure.HotKeyConfigurer.keyDown);
					tf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyPressed);
					tf.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyReleased);
					tf.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyTyped);
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = getName();
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					p.Controls.Add(temp_Control);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(tf);
				}
				return p;
			}
			
		}
		private System.Windows.Forms.TextBox tf;
		private System.Windows.Forms.Panel p;
		
		public HotKeyConfigurer(System.String key, System.String name):this(key, name, new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) 0))
		{
		}
		
		public HotKeyConfigurer(System.String key, System.String name, System.Windows.Forms.KeyEventArgs val):base(key, name, val)
		{
		}
		
		public override void  setValue(System.Object o)
		{
			base.setValue(o);
			if (tf != null && !tf.Text.Equals(keyToString()))
			{
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				tf.Text = keyToString();
			}
		}
		
		public virtual System.String keyToString()
		{
			return getString((System.Windows.Forms.KeyEventArgs) getValue());
		}
		
		public override void  setValue(System.String s)
		{
			setValue(s == null?null:decode(s));
		}
		
		public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		}
		
		public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyCode' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyCode'"
			switch (e.KeyValue)
			{
				
				case (int) System.Windows.Forms.Keys.Delete: 
				case (int) System.Windows.Forms.Keys.Back: 
					setValue(null);
					break;
				
				case (int) System.Windows.Forms.Keys.ShiftKey: 
				case (int) System.Windows.Forms.Keys.ControlKey: 
				//UPGRADE_ISSUE: Field 'java.awt.event.KeyEvent.VK_META' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventKeyEventVK_META_f'"
				case KeyEvent.VK_META: 
				case (int) System.Windows.Forms.Keys.Menu: 
					break;
				
				default: 
					//UPGRADE_ISSUE: Method 'javax.swing.KeyStroke.getKeyStrokeForEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingKeyStrokegetKeyStrokeForEvent_javaawteventKeyEvent'"
					setValue(KeyStroke.getKeyStrokeForEvent(event_sender, e));
					break;
				
			}
		}
		
		public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
			tf.Text = getString((System.Windows.Forms.KeyEventArgs) getValue());
		}
		
		/// <summary> A plain text representation of a KeyStroke.  Doesn't differ much
		/// from {@link KeyEvent#getKeyText}
		/// </summary>
		public static System.String getString(System.Windows.Forms.KeyEventArgs k)
		{
			if (k == null)
			{
				return null;
			}
			//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyText' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue.ToString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyText_int'"
			System.String s = ((System.Windows.Forms.Keys) new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) k.KeyValue).KeyValue).ToString();
			s = s.Replace(' ', '_');
			if (((int) k.Modifiers & (int) System.Windows.Forms.Keys.Shift) > 0)
			{
				s = Resources.getString("Keys.shift") + " " + s; //$NON-NLS-1$ //$NON-NLS-2$
			}
			if (((int) k.Modifiers & (int) System.Windows.Forms.Keys.Control) > 0)
			{
				s = Resources.getString("Keys.ctrl") + " " + s; //$NON-NLS-1$ //$NON-NLS-2$
			}
			//UPGRADE_ISSUE: Field 'java.awt.event.InputEvent.META_MASK' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventInputEvent'"
			if (((int) k.Modifiers & KeyEvent.META_MASK) > 0)
			{
				s = Resources.getString("Keys.meta") + " " + s; //$NON-NLS-1$ //$NON-NLS-2$
			}
			if (((int) k.Modifiers & (int) System.Windows.Forms.Keys.Alt) > 0)
			{
				s = Resources.getString("Keys.alt") + " " + s; //$NON-NLS-1$ //$NON-NLS-2$
			}
			return s.ToUpper();
		}
		
		/// <summary> Decode a String into a KeyStroke</summary>
		public static System.Windows.Forms.KeyEventArgs decode(System.String s)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'index '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int index = s.IndexOf(','); //$NON-NLS-1$
			if (index < 0)
				return null;
			
			try
			{
				return new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (System.Int32.Parse(s.Substring(0, (index) - (0))) | System.Int32.Parse(s.Substring(index + 1))));
			}
			// FIXME: review error message
			catch (System.FormatException e)
			{
				return null;
			}
			catch (System.ArgumentException e)
			{
				return null;
			}
		}
		
		/// <summary> Encode a KeyStroke into a String</summary>
		public static System.String encode(System.Windows.Forms.KeyEventArgs stroke)
		{
			return stroke == null?"":stroke.KeyValue + "," + (int) stroke.Modifiers; //$NON-NLS-1$ //$NON-NLS-2$
		}
	}
}