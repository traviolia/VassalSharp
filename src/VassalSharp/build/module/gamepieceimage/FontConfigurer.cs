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
using BooleanConfigurer = VassalSharp.configure.BooleanConfigurer;
using Configurer = VassalSharp.configure.Configurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.build.module.gamepieceimage
{
	
	/// <summary> A Configurer for {@link Font}values</summary>
	public class FontConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassItemListener
		{
			public AnonymousClassItemListener(FontConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(FontConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private FontConfigurer enclosingInstance;
			public FontConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs evt)
			{
				if (event_sender is System.Windows.Forms.MenuItem)
					((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
				Enclosing_Instance.updateValue();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(FontConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(FontConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private FontConfigurer enclosingInstance;
			public FontConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				Enclosing_Instance.updateValue();
			}
		}
		override public System.String ValueString
		{
			get
			{
				return encode((OutlineFont) value_Renamed);
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
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					p.setLayout(new BoxLayout(p, BoxLayout.Y_AXIS));
					
					//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					Box box = Box.createHorizontalBox();
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = "Font Family:  ";
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					box.Controls.Add(temp_Control);
					
					family = new System.Windows.Forms.ComboBox();
					//String[] s = GraphicsEnvironment.getLocalGraphicsEnvironment().getAvailableFontFamilyNames();
					for (int i = 0; i < FontManager.ALLOWABLE_FONTS.Length; ++i)
					{
						family.Items.Add(FontManager.ALLOWABLE_FONTS[i]);
					}
					family.SelectedItem = value_Renamed == null?FontManager.SANS_SERIF:(FontValue.FontFamily.Name);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					box.Controls.Add(family);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(box);
					
					size = new IntConfigurer(null, "Size:  ", (int) FontValue.Size);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(size.Controls);
					
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					box = Box.createHorizontalBox();
					bold = new BooleanConfigurer(null, "Bold", Bold);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					box.Controls.Add(bold.Controls);
					italic = new BooleanConfigurer(null, "Italic", Italic);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					box.Controls.Add(italic.Controls);
					outline = new BooleanConfigurer(null, "Outline", Outline);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					box.Controls.Add(outline.Controls);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(box);
					
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					box = Box.createHorizontalBox();
					System.Windows.Forms.Label temp_label4;
					temp_label4 = new System.Windows.Forms.Label();
					temp_label4.Text = "Sample:  ";
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control2;
					temp_Control2 = temp_label4;
					box.Controls.Add(temp_Control2);
					System.Windows.Forms.TextBox temp_text_box;
					//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_javalangString_int'"
					temp_text_box = new System.Windows.Forms.TextBox();
					temp_text_box.Text = "The quick brown fox";
					demo = temp_text_box;
					demo.ReadOnly = !false;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					box.Controls.Add(demo);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(box);
					
					updateValue();
					
					//UPGRADE_TODO: Interface 'java.awt.event.ItemListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					ItemListener l = new AnonymousClassItemListener(this);
					family.SelectedValueChanged += new System.EventHandler(l.itemStateChanged);
					
					//UPGRADE_TODO: Interface 'java.beans.PropertyChangeListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					PropertyChangeListener pc = new AnonymousClassPropertyChangeListener(this);
					size.PropertyChange += new SupportClass.PropertyChangeEventHandler(pc.propertyChange);
					bold.PropertyChange += new SupportClass.PropertyChangeEventHandler(pc.propertyChange);
					italic.PropertyChange += new SupportClass.PropertyChangeEventHandler(pc.propertyChange);
					outline.PropertyChange += new SupportClass.PropertyChangeEventHandler(pc.propertyChange);
				}
				return p;
			}
			
		}
		virtual protected internal OutlineFont FontValue
		{
			get
			{
				return (OutlineFont) getValue();
			}
			
		}
		virtual public bool Bold
		{
			get
			{
				return ((int) FontValue.Style & (int) System.Drawing.FontStyle.Bold) != 0;
			}
			
		}
		virtual public bool Italic
		{
			get
			{
				return ((int) FontValue.Style & (int) System.Drawing.FontStyle.Italic) != 0;
			}
			
		}
		virtual public bool Outline
		{
			get
			{
				return FontValue.Outline;
			}
			
		}
		
		protected internal System.Windows.Forms.Panel p;
		protected internal IntConfigurer size;
		protected internal BooleanConfigurer bold;
		protected internal BooleanConfigurer italic;
		protected internal BooleanConfigurer outline;
		protected internal System.Windows.Forms.ComboBox family;
		protected internal System.Windows.Forms.TextBox demo;
		
		public FontConfigurer(System.String key, System.String name):base(key, name)
		{
		}
		
		public FontConfigurer(System.String key, System.String name, OutlineFont f):base(key, name)
		{
			setValue(f);
		}
		
		public FontConfigurer(System.String key, System.String name, FontStyle f):base(key, name)
		{
			setValue(f.font);
			setName(f.getConfigureName());
		}
		
		public override void  setValue(System.String s)
		{
			setValue(decode(s));
		}
		
		protected internal virtual void  updateValue()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'style '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Field 'java.awt.Font.PLAIN' was converted to 'System.Drawing.FontStyle.Regular' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFontPLAIN_f'"
			int style = (int) System.Drawing.FontStyle.Regular | (bold.booleanValue()?(int) System.Drawing.FontStyle.Bold:0) | (italic.booleanValue()?(int) System.Drawing.FontStyle.Italic:0);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'font '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			OutlineFont font = new OutlineFont((System.String) family.SelectedItem, style, System.Int32.Parse(size.ValueString), outline.booleanValue());
			
			setValue(font);
			demo.Font = font;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
			System.Windows.Forms.Form w = (System.Windows.Forms.Form) Controls.Parent;
			if (w != null)
			{
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				w.pack();
			}
		}
		
		public static OutlineFont decode(System.String s)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'sd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, ',');
			//UPGRADE_TODO: Field 'java.awt.Font.PLAIN' was converted to 'System.Drawing.FontStyle.Regular' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFontPLAIN_f'"
			return new OutlineFont(sd.nextToken(FontManager.DIALOG), sd.nextInt((int) System.Drawing.FontStyle.Regular), sd.nextInt(10), sd.nextBoolean(false));
		}
		
		public static System.String encode(OutlineFont f)
		{
			SequenceEncoder se = new SequenceEncoder(f.Name, ',');
			se.append((int) f.Style);
			se.append((int) f.Size);
			se.append(f.Outline);
			return se.Value;
		}
	}
}