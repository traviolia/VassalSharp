/*
* $Id$
*
* Copyright (c) 2000-2011 by Rodney Kinney, Brent Easton
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
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
using Resources = VassalSharp.i18n.Resources;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
using ScrollPane = VassalSharp.tools.ScrollPane;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.configure
{
	
	/// <summary> A Configurer that returns an array of Strings</summary>
	public class StringArrayConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(StringArrayConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StringArrayConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StringArrayConfigurer enclosingInstance;
			public StringArrayConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				System.String s = Enclosing_Instance.TextValue;
				Enclosing_Instance.addValue(s);
				Enclosing_Instance.TextValue = "";
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(StringArrayConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StringArrayConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StringArrayConfigurer enclosingInstance;
			public StringArrayConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				System.Object[] o = SupportClass.GetSelectedItems(Enclosing_Instance.list);
				for (int i = 0; i < o.Length; ++i)
				{
					Enclosing_Instance.removeValue((System.String) o[i]);
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(StringArrayConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(StringArrayConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private StringArrayConfigurer enclosingInstance;
			public StringArrayConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				if (Enclosing_Instance.value_Renamed == null)
				{
					Enclosing_Instance.addValue(Enclosing_Instance.TextValue);
				}
				else
				{
					int pos = Enclosing_Instance.list.SelectedIndex;
					if (pos < 0)
						pos = Enclosing_Instance.list.Items.Count;
					Enclosing_Instance.setValue(ArrayUtils.insert((System.String[]) Enclosing_Instance.value_Renamed, pos, Enclosing_Instance.TextValue));
					Enclosing_Instance.TextValue = "";
					//UPGRADE_TODO: Method 'javax.swing.JList.setSelectedIndex' was converted to 'System.Windows.Forms.ListBox.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetSelectedIndex_int'"
					Enclosing_Instance.list.SelectedIndex = pos + 1;
				}
			}
		}
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(VassalSharp.configure.StringArrayConfigurer c)
			{
				InitBlock(c);
			}
			private void  InitBlock(VassalSharp.configure.StringArrayConfigurer c)
			{
				this.c = c;
			}
			//UPGRADE_NOTE: Final variable c was copied into class AnonymousClassPropertyChangeListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.StringArrayConfigurer c;
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				System.Console.Error.WriteLine(c.getName() + " = " + c.ValueString);
			}
		}
		//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
		virtual public System.Windows.Forms.ListBox.ObjectCollection Model
		{
			get
			{
				return model;
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (panel == null)
				{
					panel = new System.Windows.Forms.Panel();
					//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.border.TitledBorder.TitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingborderTitledBorder'"
					System.Windows.Forms.ControlPaint.DrawBorder3D(panel.CreateGraphics(), 0, 0, panel.Width, panel.Height, new TitledBorder(name));
					panel.setLayout(new MigLayout("fill"));
					
					//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					Box buttonBox = Box.createHorizontalBox();
					//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					Box leftBox = Box.createVerticalBox();
					
					//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
					model = new System.Windows.Forms.ListBox.ObjectCollection(new System.Windows.Forms.ListBox());
					updateModel();
					
					System.Windows.Forms.ListBox temp_ListBox;
					temp_ListBox = new System.Windows.Forms.ListBox();
					temp_ListBox.Items.AddRange(model);
					temp_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
					list = temp_ListBox;
					//UPGRADE_TODO: Method 'javax.swing.JList.setPrototypeCellValue' was converted to 'System.Windows.Forms.ListBox.Width' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetPrototypeCellValue_javalangObject'"
					list.Width = (System.Int32) "MMMMMMMM";
					//UPGRADE_ISSUE: Method 'javax.swing.JList.setVisibleRowCount' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJListsetVisibleRowCount_int'"
					list.setVisibleRowCount(2);
					//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.ListSelectionModel.SINGLE_SELECTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
					list.SelectionMode = (System.Windows.Forms.SelectionMode) System.Windows.Forms.SelectionMode.One;
					
					System.Windows.Forms.Button addButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.ADD));
					addButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(addButton);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					buttonBox.Controls.Add(addButton);
					
					System.Windows.Forms.Button removeButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.REMOVE));
					removeButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(removeButton);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					buttonBox.Controls.Add(removeButton);
					
					System.Windows.Forms.Button insertButton = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString(Resources.INSERT));
					//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
					ActionListener insertAction = new AnonymousClassActionListener2(this);
					insertButton.Click += new System.EventHandler(insertAction.actionPerformed);
					SupportClass.CommandManager.CheckCommand(insertButton);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					buttonBox.Controls.Add(insertButton);
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'textComponent '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Windows.Forms.Control textComponent = TextComponent;
					addTextActionListener(insertAction);
					
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					leftBox.Controls.Add(textComponent);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					leftBox.Controls.Add(buttonBox);
					
					//UPGRADE_TODO: Class 'javax.swing.JSplitPane' was converted to 'SupportClass.SplitterPanelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					SupportClass.SplitterPanelSupport pane = new SupportClass.SplitterPanelSupport((int) System.Windows.Forms.Orientation.Horizontal);
					pane.FirstControl = leftBox;
					pane.SecondControl = new ScrollPane(list);
					
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					panel.Controls.Add(pane);
					pane.Dock = new System.Windows.Forms.DockStyle();
					pane.BringToFront();
				}
				return panel;
			}
			
		}
		virtual protected internal System.Windows.Forms.Control TextComponent
		{
			get
			{
				if (textField == null)
				{
					//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
					textField = new System.Windows.Forms.TextBox();
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
					textField.setMaximumSize(new System.Drawing.Size(System.Int32.MaxValue, textField.Size.Height));
				}
				return textField;
			}
			
		}
		virtual protected internal System.String TextValue
		{
			get
			{
				return textField.Text;
			}
			
			set
			{
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				textField.Text = value;
			}
			
		}
		virtual public System.String[] StringArray
		{
			get
			{
				if (value_Renamed is System.String[])
				{
					return (System.String[]) value_Renamed;
				}
				else
				{
					return EMPTY;
				}
			}
			
		}
		override public System.String ValueString
		{
			get
			{
				return arrayToString(StringArray);
			}
			
		}
		protected internal System.Windows.Forms.Panel panel;
		protected internal System.Windows.Forms.ListBox list;
		//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
		protected internal System.Windows.Forms.ListBox.ObjectCollection model;
		//UPGRADE_NOTE: Final was removed from the declaration of 'EMPTY '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private static readonly System.String[] EMPTY = new System.String[0];
		protected internal System.Windows.Forms.TextBox textField;
		
		public StringArrayConfigurer(System.String key, System.String name, System.Object val):base(key, name, val)
		{
		}
		
		public StringArrayConfigurer(System.String key, System.String name):base(key, name)
		{
		}
		
		public virtual void  addValue(System.String s)
		{
			setValue(value_Renamed == null?new System.String[]{s}:ArrayUtils.append((System.String[]) value_Renamed, s));
		}
		
		public virtual void  removeValue(System.String s)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'oldValue '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] oldValue = StringArray;
			//UPGRADE_NOTE: Final was removed from the declaration of 'newValue '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] newValue = ArrayUtils.remove(oldValue, s);
			if (oldValue != newValue)
				setValue(newValue);
		}
		
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		protected internal virtual void  addTextActionListener(ActionListener a)
		{
			//UPGRADE_TODO: Method 'javax.swing.JTextField.addActionListener' was converted to 'System.Windows.Forms.TextBox.KeyPress' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldaddActionListener_javaawteventActionListener'"
			textField.KeyPress += new System.Windows.Forms.KeyPressEventHandler(a.actionPerformed);
		}
		
		public static System.String arrayToString(System.String[] s)
		{
			if (s == null || s.Length == 0)
			{
				return "";
			}
			SequenceEncoder se = new SequenceEncoder(',');
			for (int i = 0; i < s.Length; ++i)
			{
				se.append(s[i] != null?s[i]:"");
			}
			return se.Value;
		}
		
		public override void  setValue(System.Object o)
		{
			if (o == null)
			{
				o = EMPTY;
			}
			base.setValue(o);
			updateModel();
		}
		
		public override void  setValue(System.String s)
		{
			System.String[] val = stringToArray(s);
			setValue(val);
		}
		
		public static System.String[] stringToArray(System.String s)
		{
			if (s == null || s.Length == 0)
			{
				return EMPTY;
			}
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(s, ',');
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < String > l = new ArrayList < String >();
			while (st.hasMoreTokens())
			{
				l.add(st.nextToken());
			}
			return l.toArray(new System.String[l.size()]);
		}
		
		protected internal virtual void  updateModel()
		{
			if (model != null)
			{
				model.Clear();
				System.String[] s = StringArray;
				for (int i = 0; i < s.Length; ++i)
				{
					model.Add(s[i]);
				}
			}
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			System.Windows.Forms.Form f = new System.Windows.Forms.Form();
			//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringArrayConfigurer c = new StringArrayConfigurer(null, "Visible to these players:  ");
			c.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(c).propertyChange);
			c.setValue("Rack,Shack,Benny");
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