/*
* $Id$
*
* Copyright (c) 2000-2009 by Rodney Kinney & Brent Easton
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
* FormattedStringConfigurer.
* Extended version of StringConfigure that provides a drop down list of options that can
* be inserted into the string
*/
using System;
namespace VassalSharp.configure
{
	
	public class FormattedStringConfigurer:StringConfigurer
	{
		virtual public System.String[] Options
		{
			get
			{
				System.String[] s = new System.String[optionsModel.Count];
				for (int i = 0; i < s.Length; ++i)
				{
					s[i] = ((System.String) optionsModel[i]);
				}
				return s;
			}
			
			set
			{
				optionsModel.Clear();
				optionsModel.Add("Insert");
				for (int i = 0; i < value.Length; i++)
				{
					optionsModel.Add(value[i]);
				}
				setListVisibility();
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (p == null)
				{
					System.Windows.Forms.Control generatedAux = base.Controls;
					
					nameField.Enter += new System.EventHandler(this.focusGained);
					nameField.Leave += new System.EventHandler(this.focusLost);
					//UPGRADE_TODO: Constructor 'javax.swing.JComboBox.JComboBox' was converted to 'System.Windows.Forms.ComboBox.ComboBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComboBoxJComboBox_javaxswingComboBoxModel'"
					dropList = new System.Windows.Forms.ComboBox();
					dropList.SelectedIndex = 0;
					dropList.Enabled = false;
					dropList.SelectedValueChanged += new System.EventHandler(this.actionPerformed);
					SupportClass.CommandManager.CheckCommand(dropList);
					
					setListVisibility();
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(dropList);
				}
				return p;
			}
			
		}
		
		//UPGRADE_TODO: Class 'javax.swing.DefaultComboBoxModel' was converted to 'System.Windows.Forms.ComboBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Windows.Forms.ComboBox.ObjectCollection optionsModel;
		private System.Windows.Forms.ComboBox dropList;
		
		public FormattedStringConfigurer(System.String key, System.String name):this(key, name, new System.String[0])
		{
		}
		
		public FormattedStringConfigurer(System.String key, System.String name, System.String[] options):base(key, name)
		{
			//UPGRADE_ISSUE: Constructor 'javax.swing.DefaultComboBoxModel.DefaultComboBoxModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultComboBoxModelDefaultComboBoxModel'"
			optionsModel = new DefaultComboBoxModel();
			Options = options;
		}
		
		private void  setListVisibility()
		{
			if (dropList != null)
			{
				dropList.Visible = optionsModel.Count > 1;
			}
		}
		/*
		* Drop-down list has been clicked, insert selected option onto string
		*/
		public virtual void  actionPerformed(System.Object event_sender, System.EventArgs arg0)
		{
			System.String item = "";
			
			int selectedIndex = dropList.SelectedIndex;
			
			if (selectedIndex > 0)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Object.toString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				item = "$" + optionsModel[selectedIndex] + "$";
				System.String work = nameField.Text;
				
				int pos = nameField.SelectionStart;
				// Cut out any selected text
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.text.JTextComponent.getSelectedText' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				if (nameField.SelectedText != null)
				{
					int start = nameField.SelectionStart;
					int end = nameField.SelectionStart + nameField.SelectionLength;
					work = work.Substring(0, (start) - (0)) + work.Substring(end);
					if (pos > work.Length)
					{
						pos = work.Length;
					}
				}
				
				System.String news = work.Substring(0, (pos) - (0)) + item + work.Substring(pos);
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				nameField.Text = news;
				nameField.SelectionStart = pos + item.Length;
				
				// Update the text field and repaint it
				noUpdate = true;
				setValue(nameField.Text);
				noUpdate = false;
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				nameField.Refresh();
			}
			// Send focus back to text field
			nameField.requestFocusInWindow();
		}
		
		/*
		* Focus gained on text field, so enable insert drop-down
		* and make sure it says 'Insert'
		*/
		public virtual void  focusGained(System.Object event_sender, System.EventArgs arg0)
		{
			dropList.SelectedIndex = 0;
			dropList.Enabled = true;
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			dropList.Refresh();
		}
		
		/*
		* Focus lost on text field, so disable insert drop-down
		*/
		public virtual void  focusLost(System.Object event_sender, System.EventArgs arg0)
		{
			dropList.Enabled = false;
		}
	}
}