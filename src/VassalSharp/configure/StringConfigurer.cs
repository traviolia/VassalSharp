/*
* $Id$
*
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
namespace VassalSharp.configure
{
	
	/// <summary> A Configurer for String values</summary>
	public class StringConfigurer : Configurer
	{
		////UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassDocumentListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		////UPGRADE_TODO: Interface 'javax.swing.event.DocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		//private class AnonymousClassDocumentListener : DocumentListener
		//{
		//	public AnonymousClassDocumentListener(StringConfigurer enclosingInstance)
		//	{
		//		InitBlock(enclosingInstance);
		//	}
		//	private void  InitBlock(StringConfigurer enclosingInstance)
		//	{
		//		this.enclosingInstance = enclosingInstance;
		//	}
		//	private StringConfigurer enclosingInstance;
		//	public StringConfigurer Enclosing_Instance
		//	{
		//		get
		//		{
		//			return enclosingInstance;
		//		}
				
		//	}
		//	//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
		//	public virtual void  insertUpdate(DocumentEvent e)
		//	{
		//		update();
		//	}
			
		//	//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
		//	public virtual void  removeUpdate(DocumentEvent e)
		//	{
		//		update();
		//	}
			
		//	//UPGRADE_ISSUE: Interface 'javax.swing.event.DocumentEvent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventDocumentEvent'"
		//	public virtual void  changedUpdate(DocumentEvent e)
		//	{
		//	}
			
		//	private void  update()
		//	{
		//		Enclosing_Instance.noUpdate = true;
		//		Enclosing_Instance.setValue(Enclosing_Instance.nameField.Text);
		//		Enclosing_Instance.noUpdate = false;
		//	}
		//}
		override public string ValueString
		{
			get
			{
				return (string) value_Renamed;
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (p == null)
				{
					p = new System.Windows.Forms.Panel();
                    ////UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
                    ////UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
                    ////UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
                    //p.setLayout(new BoxLayout(p, BoxLayout.X_AXIS));
                    System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = getName();
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					p.Controls.Add(temp_Control);
					nameField = buildTextField();
					////UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
					////UPGRADE_ISSUE: Method 'javax.swing.JComponent.getMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetMaximumSize'"
					//nameField.setMaximumSize(new System.Drawing.Size(nameField.getMaximumSize().Width, nameField.Size.Height));
					nameField.Text = ValueString;
					p.Controls.Add(nameField);
					////UPGRADE_ISSUE: Method 'javax.swing.text.Document.addDocumentListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtextDocument'"
					////UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.getDocument' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentgetDocument'"
					//((string) nameField.Text).addDocumentListener(new AnonymousClassDocumentListener(this));
				}
				return p;
			}
			
		}
		protected internal System.Windows.Forms.Panel p;
		protected internal System.Windows.Forms.TextBox nameField;
		protected internal int length;
		protected internal static int DEFAULT_LENGHTH = 12;
		
		public StringConfigurer(string key, string name) : this(key, name, "")
		{
		}
		
		public StringConfigurer(string key, string name, string val) : base(key, name, val)
		{
			length = DEFAULT_LENGHTH;
		}
		
		public StringConfigurer(string key, string name, int length) : this(key, name)
		{
			this.length = length > 0?length:DEFAULT_LENGHTH;
		}
		
		public override void  setValue(string s)
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