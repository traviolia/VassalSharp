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
namespace VassalSharp.configure
{
	
	/// <summary> Configurer for specifing key masks (CTRL, SHIFT, CTRL-ALT, etc.)</summary>
	public class KeyModifiersConfigurer:Configurer
	{
		static private System.Int32 state437;
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state437 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (controls == null)
				{
					controls = new System.Windows.Forms.Panel();
					//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					controls.setLayout(new BoxLayout(controls, BoxLayout.X_AXIS));
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = getName();
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					controls.Controls.Add(temp_Control);
					//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
					tf = new System.Windows.Forms.TextBox();
					tf.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.configure.KeyModifiersConfigurer.keyDown);
					tf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyPressed);
					tf.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyReleased);
					tf.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyTyped);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(tf);
					setValue(getValue());
				}
				return controls;
			}
			
		}
		override public System.String ValueString
		{
			get
			{
				System.Object val = getValue();
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return val != null?val.ToString():"0";
			}
			
		}
		private System.Windows.Forms.Panel controls;
		private System.Windows.Forms.TextBox tf;
		
		public KeyModifiersConfigurer(System.String key, System.String name):base(key, name, 0)
		{
		}
		
		public override void  setValue(System.String s)
		{
			if (s == null)
			{
				s = "0";
			}
			try
			{
				setValue((System.Object) System.Int32.Parse(s));
			}
			catch (System.FormatException e)
			{
				// Set to null if saved value is not a number
				setValue(null);
			}
		}
		
		public override void  setValue(System.Object o)
		{
			if (!noUpdate && tf != null)
			{
				if (o is System.Int32 && ((System.Int32) o) != 0)
				{
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyModifiersText' was converted to 'System.Windows.Forms.KeyEventArgs.Modifiers.ToString' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyModifiersText_int'"
					tf.Text = ((System.Windows.Forms.Keys) new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) ((System.Int32) o)).Modifiers).ToString().ToUpper();
				}
				else
				{
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					tf.Text = "";
				}
			}
			base.setValue(o);
		}
		
		public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
		}
		
		public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		}
		
		public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			//UPGRADE_NOTE: The 'java.awt.event.InputEvent.getModifiers' method simulation might not work for some controls. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1284'"
			setValue(state437);
		}
	}
}