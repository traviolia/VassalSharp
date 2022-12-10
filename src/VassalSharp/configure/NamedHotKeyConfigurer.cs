/*
* $Id$
*
* Copyright (c) 2008 by Rodney Kinney, Brent Easton
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
using NamedKeyManager = VassalSharp.tools.NamedKeyManager;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
namespace VassalSharp.configure
{
	
	/// <summary> A Configurer for {@link NamedKeyStroke} values</summary>
	public class NamedHotKeyConfigurer:Configurer
	{
		static private System.Int32 state442;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassKeyListener
		{
			public AnonymousClassKeyListener(NamedHotKeyConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(NamedHotKeyConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private NamedHotKeyConfigurer enclosingInstance;
			public NamedHotKeyConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
			{
				//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyCode' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyCode'"
				switch (e.KeyValue)
				{
					
					case (int) System.Windows.Forms.Keys.Delete: 
					case (int) System.Windows.Forms.Keys.Back: 
						if (Enclosing_Instance.keyName.Text.Length == 0)
						{
							Enclosing_Instance.named = false;
							Enclosing_Instance.setValue(NamedKeyStroke.NULL_KEYSTROKE);
							Enclosing_Instance.updateVisibility();
						}
						else
						{
							Enclosing_Instance.setValue(new NamedKeyStroke(NamedKeyManager.MarkerKeyStroke, Enclosing_Instance.keyName.Text));
						}
						break;
					
					case (int) System.Windows.Forms.Keys.ShiftKey: 
					case (int) System.Windows.Forms.Keys.ControlKey: 
					//UPGRADE_ISSUE: Field 'java.awt.event.KeyEvent.VK_META' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventKeyEventVK_META_f'"
					case KeyEvent.VK_META: 
					case (int) System.Windows.Forms.Keys.Menu: 
						break;
					
					default: 
						//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.event.KeyEvent.getKeyChar' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						if (Enclosing_Instance.isPrintableAscii((char) e.KeyValue))
						{
							Enclosing_Instance.setValue(new NamedKeyStroke(NamedKeyManager.MarkerKeyStroke, Enclosing_Instance.keyName.Text));
						}
						else
						{
							Enclosing_Instance.named = false;
							Enclosing_Instance.setValue(NamedKeyStroke.getKeyStrokeForEvent(event_sender, e));
							Enclosing_Instance.updateVisibility();
						}
						break;
					
				}
			}
			public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
			{
			}
			public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
			{
			}
		}
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state442 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		override public System.String ValueString
		{
			get
			{
				return encode((NamedKeyStroke) getValue());
			}
			
		}
		virtual public NamedKeyStroke ValueNamedKeyStroke
		{
			get
			{
				return (NamedKeyStroke) value_Renamed;
			}
			
		}
		virtual public bool Enabled
		{
			set
			{
				tf.Enabled = value;
				keyName.Enabled = value;
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
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetMaximumSize'"
					tf.setMaximumSize(new System.Drawing.Size(tf.getMaximumSize().Width, tf.Size.Height));
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					tf.Text = keyToString();
					tf.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.configure.NamedHotKeyConfigurer.keyDown);
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
					
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					keyName.Text = ValueNamedKeyStroke == null?null:ValueNamedKeyStroke.Name;
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetMaximumSize'"
					keyName.setMaximumSize(new System.Drawing.Size(keyName.getMaximumSize().Width, keyName.Size.Height));
					keyName.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.configure.NamedHotKeyConfigurer.keyDown);
					keyName.KeyDown += new System.Windows.Forms.KeyEventHandler(new AnonymousClassKeyListener(this).keyPressed);
					keyName.KeyUp += new System.Windows.Forms.KeyEventHandler(new AnonymousClassKeyListener(this).keyReleased);
					keyName.KeyPress += new System.Windows.Forms.KeyPressEventHandler(new AnonymousClassKeyListener(this).keyTyped);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(keyName);
					updateVisibility();
				}
				return p;
			}
			
		}
		virtual public bool Named
		{
			get
			{
				return named;
			}
			
		}
		//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
		private System.Windows.Forms.TextBox tf = new System.Windows.Forms.TextBox();
		private System.Windows.Forms.Panel p;
		private bool named;
		//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
		private System.Windows.Forms.TextBox keyName = new System.Windows.Forms.TextBox();
		private char lastChar;
		
		public NamedHotKeyConfigurer(System.String key, System.String name):this(key, name, new NamedKeyStroke())
		{
		}
		
		public NamedHotKeyConfigurer(System.String key, System.String name, NamedKeyStroke val):base(key, name, val)
		{
			named = val != null && val.Named;
		}
		
		public override void  setValue(System.Object o)
		{
			base.setValue(o);
			named = value_Renamed != null && ((NamedKeyStroke) value_Renamed).Named;
			if (!named)
			{
				if (tf != null && !tf.Text.Equals(keyToString()))
				{
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					tf.Text = keyToString();
				}
			}
		}
		
		public virtual System.String keyToString()
		{
			return getString((NamedKeyStroke) getValue());
		}
		
		public override System.Object getValue()
		{
			return base.getValue();
		}
		
		public override void  setValue(System.String s)
		{
			setValue(s == null?null:decode(s));
		}
		
		protected internal virtual void  updateVisibility()
		{
			tf.Visible = !Named;
			keyName.Visible = Named;
			lastChar = (char) (0);
			//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
			System.Windows.Forms.Form w = (System.Windows.Forms.Form) p.Parent;
			if (w != null)
			{
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				w.pack();
			}
		}
		
		public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.event.KeyEvent.getKeyChar' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			lastChar = e.KeyChar;
		}
		
		public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyCode' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyCode'"
			switch (e.KeyValue)
			{
				
				case (int) System.Windows.Forms.Keys.Delete: 
				case (int) System.Windows.Forms.Keys.Back: 
					setValue(NamedKeyStroke.NULL_KEYSTROKE);
					break;
				
				case (int) System.Windows.Forms.Keys.ShiftKey: 
				case (int) System.Windows.Forms.Keys.ControlKey: 
				//UPGRADE_ISSUE: Field 'java.awt.event.KeyEvent.VK_META' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventKeyEventVK_META_f'"
				case KeyEvent.VK_META: 
				case (int) System.Windows.Forms.Keys.Menu: 
					break;
				
				default: 
					//UPGRADE_NOTE: Final was removed from the declaration of 'namedStroke '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					NamedKeyStroke namedStroke = ValueNamedKeyStroke;
					if (namedStroke != null)
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'thisChar '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.event.KeyEvent.getKeyChar' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
						int thisChar = (char) e.KeyValue;
						if (isPrintableAscii(lastChar) && isPrintableAscii(thisChar))
						{
							//UPGRADE_NOTE: Final was removed from the declaration of 'name '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
							//UPGRADE_TODO: The equivalent in .NET for method 'java.awt.event.KeyEvent.getKeyChar' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
							System.String name = "" + lastChar + (char) e.KeyValue;
							named = true;
							//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
							keyName.Text = name;
							setValue(new NamedKeyStroke(name));
							updateVisibility();
							//UPGRADE_TODO: Method 'javax.swing.JComponent.requestFocus' was converted to 'System.Windows.Forms.Control.Focus' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentrequestFocus'"
							keyName.Focus();
							break;
						}
					}
					setValue(NamedKeyStroke.getKeyStrokeForEvent(event_sender, e));
					break;
				
			}
		}
		
		public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (!named)
			{
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				tf.Text = getString((NamedKeyStroke) getValue());
			}
		}
		
		protected internal virtual bool isPrintableAscii(char c)
		{
			return isPrintableAscii((int) c);
		}
		
		protected internal virtual bool isPrintableAscii(int i)
		{
			return i >= ' ' && i <= '~';
		}
		
		/// <summary> A plain text representation of a KeyStroke.  Doesn't differ much
		/// from {@link KeyEvent#getKeyText}
		/// </summary>
		public static System.String getString(NamedKeyStroke k)
		{
			return (k == null || k.Null)?"":getString(k.Stroke);
		}
		
		public static System.String getFancyString(NamedKeyStroke k)
		{
			System.String s = getString(k);
			if (s.Length > 0)
			{
				s = "[" + s + "]";
			}
			return s;
		}
		
		public static System.String getString(System.Windows.Forms.KeyEventArgs k)
		{
			return NamedKeyManager.isNamed(k)?"":HotKeyConfigurer.getString(k);
		}
		
		/// <summary> Decode a String into a NamedKeyStroke</summary>
		public static NamedKeyStroke decode(System.String s)
		{
			if (s == null)
			{
				return NamedKeyStroke.NULL_KEYSTROKE;
			}
			System.String[] parts = s.split(",");
			if (parts.Length < 2)
			{
				return NamedKeyStroke.NULL_KEYSTROKE;
			}
			
			try
			{
				System.Windows.Forms.KeyEventArgs stroke = new System.Windows.Forms.KeyEventArgs((System.Windows.Forms.Keys) (System.Int32.Parse(parts[0]) | System.Int32.Parse(parts[1])));
				System.String name = null;
				if (parts.Length > 2)
				{
					name = parts[2];
				}
				return new NamedKeyStroke(stroke, name);
			}
			catch (System.Exception e)
			{
				return NamedKeyStroke.NULL_KEYSTROKE;
			}
		}
		
		/// <summary> Encode a NamedKeyStroke into a String</summary>
		public static System.String encode(NamedKeyStroke stroke)
		{
			if (stroke == null)
			{
				return "";
			}
			System.Windows.Forms.KeyEventArgs key = stroke.Stroke;
			if (key == null)
			{
				return "";
			}
			System.String s = key.KeyValue + "," + (int) key.Modifiers;
			if (stroke.Named)
			{
				s += ("," + stroke.Name);
			}
			return s;
		}
	}
}