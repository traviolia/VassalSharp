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
/*
* Created by IntelliJ IDEA.
* User: rkinney
* Date: Jul 20, 2002
* Time: 3:52:36 AM
* To change template for new class use
* Code Style | Class Templates options (Tools | IDE Options).
*/
using System;
using Configurer = VassalSharp.configure.Configurer;
namespace VassalSharp.build.module.gamepieceimage
{
	
	/// <summary> A Configurer that returns a String from among a list of possible values</summary>
	public class StringEnumConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(StringEnumConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StringEnumConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StringEnumConfigurer enclosingInstance;
			public StringEnumConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.noUpdate = true;
				Enclosing_Instance.setValue(Enclosing_Instance.box.SelectedItem);
				Enclosing_Instance.noUpdate = false;
			}
		}
		private class AnonymousClassPropertyChangeListener
		{
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				System.Console.Error.WriteLine(evt.PropertyName + " = " + evt.NewValue); //$NON-NLS-1$
			}
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (panel == null)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					panel = Box.createHorizontalBox();
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = name;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					panel.Controls.Add(temp_Control);
					box = ComboBox;
					if (isValidValue(getValue()))
					{
						box.SelectedItem = getValue();
					}
					else if (validValues.Length > 0)
					{
						box.SelectedIndex = 0;
					}
					box.SelectedValueChanged += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(box);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					panel.Controls.Add(box);
				}
				return panel;
			}
			
		}
		virtual public System.Windows.Forms.ComboBox ComboBox
		{
			get
			{
				System.Windows.Forms.ComboBox b = SupportClass.ComboBoxSupport.CreateComboBox(validValues);
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetMaximumSize'"
				//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				b.setMaximumSize(new System.Drawing.Size(b.getMaximumSize().Width, b.Size.Height));
				return b;
			}
			
		}
		virtual public System.String[] ValidValues
		{
			get
			{
				return validValues;
			}
			
			set
			{
				validValues = value;
				//UPGRADE_ISSUE: Method 'javax.swing.JComboBox.setModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComboBoxsetModel_javaxswingComboBoxModel'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.DefaultComboBoxModel.DefaultComboBoxModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultComboBoxModelDefaultComboBoxModel_javalangObject[]'"
				box.setModel(new DefaultComboBoxModel(validValues));
			}
			
		}
		override public System.String ValueString
		{
			get
			{
				return box != null?(System.String) box.SelectedItem:validValues[0];
			}
			
		}
		protected internal System.String[] validValues;
		protected internal System.Windows.Forms.ComboBox box;
		//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
		protected internal Box panel;
		
		public StringEnumConfigurer(System.String key, System.String name, System.String[] validValues):base(key, name)
		{
			this.validValues = validValues;
		}
		
		public virtual bool isValidValue(System.Object o)
		{
			for (int i = 0; i < validValues.Length; ++i)
			{
				if (validValues[i].Equals(o))
				{
					return true;
				}
			}
			return false;
		}
		
		public override void  setValue(System.Object o)
		{
			if (validValues == null || isValidValue(o))
			{
				base.setValue(o);
				if (!noUpdate && box != null)
				{
					box.SelectedItem = o;
				}
			}
		}
		
		public override void  setValue(System.String s)
		{
			setValue((System.Object) s);
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			System.Windows.Forms.Form f = new System.Windows.Forms.Form();
			StringEnumConfigurer c = new StringEnumConfigurer(null, "Pick one: ", new System.String[]{"one", "two", "three"}); //$NON-NLS-2$ //$NON-NLS-3$ //$NON-NLS-4$
			c.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener().propertyChange);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			f.Controls.Add(c.Controls);
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			f.pack();
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			f.Visible = true;
		}
	}
}