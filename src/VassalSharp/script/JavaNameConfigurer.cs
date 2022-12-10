/*
* $Id$
*
* Copyright (c) 2008-2009 by Brent Easton
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
using Configurer = VassalSharp.configure.Configurer;
namespace VassalSharp.script
{
	
	/// <summary> A Configurer for a String that enforces the string to be a valid
	/// Java name
	/// </summary>
	public class JavaNameConfigurer:Configurer
	{
		static private System.Int32 state677;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassKeyAdapter
		{
			public AnonymousClassKeyAdapter(JavaNameConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(JavaNameConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private JavaNameConfigurer enclosingInstance;
			public JavaNameConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs evt)
			{
				Enclosing_Instance.noUpdate = true;
				System.String v = Enclosing_Instance.nameField.Text;
				int caret = Enclosing_Instance.nameField.SelectionStart;
				System.Text.StringBuilder buffer = new System.Text.StringBuilder();
				for (int i = 0; i < v.Length; i++)
				{
					char c = v[i];
					if (c == '$' || c == '_' || (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
					{
						buffer.Append(c);
					}
					else if (i > 0 && c >= '0' && c <= '9')
					{
						buffer.Append(c);
					}
				}
				System.String newString = buffer.ToString();
				Enclosing_Instance.setValue(newString);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				Enclosing_Instance.nameField.Text = newString;
				if (newString.Length < v.Length)
				{
					caret--;
				}
				Enclosing_Instance.nameField.SelectionStart = caret;
				Enclosing_Instance.noUpdate = false;
			}
		}
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state677 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		override public System.String ValueString
		{
			get
			{
				return (System.String) value_Renamed;
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
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = getName();
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					p.Controls.Add(temp_Control);
					nameField = buildTextField();
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetMaximumSize'"
					nameField.setMaximumSize(new System.Drawing.Size(nameField.getMaximumSize().Width, nameField.Size.Height));
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					nameField.Text = ValueString;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(nameField);
					nameField.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.script.JavaNameConfigurer.keyDown);
					nameField.KeyUp += new System.Windows.Forms.KeyEventHandler(new AnonymousClassKeyAdapter(this).keyReleased);
				}
				return p;
			}
			
		}
		protected internal System.Windows.Forms.Panel p;
		protected internal System.Windows.Forms.TextBox nameField;
		
		public JavaNameConfigurer(System.String key, System.String name):this(key, name, "")
		{
		}
		
		public JavaNameConfigurer(System.String key, System.String name, System.String val):base(key, name, val)
		{
		}
		
		public override void  setValue(System.String s)
		{
			if (!noUpdate && nameField != null)
			{
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				nameField.Text = s;
			}
			setValue((System.Object) s);
		}
		
		protected internal virtual System.Windows.Forms.TextBox buildTextField()
		{
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			return new System.Windows.Forms.TextBox();
		}
	}
}