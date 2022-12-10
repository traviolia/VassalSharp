/*
* $Id$
*
* Copyright (c) 2008 by Joel Uckelman
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
//UPGRADE_TODO: The type 'org.jdesktop.layout.GroupLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using GroupLayout = org.jdesktop.layout.GroupLayout;
//UPGRADE_TODO: The type 'org.jdesktop.layout.LayoutStyle' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using LayoutStyle = org.jdesktop.layout.LayoutStyle;
using DialogUtils = VassalSharp.tools.DialogUtils;
namespace VassalSharp.tools.swing
{
	
	/// <summary> Provides some basic kinds of dialogs.
	/// 
	/// </summary>
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	public class Dialogs
	{
		private Dialogs()
		{
		}
		
		public static void  showMessageDialog(System.Windows.Forms.Control parent, System.String title, System.String heading, System.String message, int messageType)
		{
			showMessageDialog(parent, title, heading, message, messageType, (System.Object) null, null);
		}
		
		public static void  showMessageDialog(System.Windows.Forms.Control parent, System.String title, System.String heading, System.String message, int messageType, System.Object key, System.String disableMsg)
		{
			showMessageDialog(parent, title, heading, message, messageType, null, key, disableMsg);
		}
		
		public static void  showMessageDialog(System.Windows.Forms.Control parent, System.String title, System.String heading, System.String message, int messageType, System.Drawing.Image icon, System.Object key, System.String disableMsg)
		{
			showDialog(parent, title, buildContents(heading, message), messageType, icon, (int) System.Windows.Forms.MessageBoxButtons.OK, null, (System.Object) null, key, disableMsg);
		}
		
		public static int showConfirmDialog(System.Windows.Forms.Control parent, System.String title, System.String heading, System.String message, int messageType, int optionType)
		{
			return showConfirmDialog(parent, title, heading, message, messageType, null, optionType, (System.Object) null, null);
		}
		
		public static int showConfirmDialog(System.Windows.Forms.Control parent, System.String title, System.String heading, System.String message, int messageType, int optionType, System.Object key, System.String disableMsg)
		{
			return showConfirmDialog(parent, title, heading, message, messageType, null, optionType, key, disableMsg);
		}
		
		public static int showConfirmDialog(System.Windows.Forms.Control parent, System.String title, System.String heading, System.String message, int messageType, System.Drawing.Image icon, int optionType, System.Object key, System.String disableMsg)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'o '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Object o = showDialog(parent, title, buildContents(heading, message), messageType, icon, optionType, null, (System.Object) null, key, disableMsg);
			
			if (o == null || !(o is System.Int32))
			{
				//UPGRADE_ISSUE: Field 'javax.swing.JOptionPane.CLOSED_OPTION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
				return JOptionPane.CLOSED_OPTION;
			}
			else
				return ((System.Int32) o);
		}
		
		public static System.Object showDialog(System.Windows.Forms.Control parent, System.String title, System.Windows.Forms.Control content, int messageType, System.Drawing.Image icon, int optionType, System.Object[] options, System.Object initialValue, System.Object key, System.String disableMsg)
		{
			// set up the "don't show again" check box, if applicable
			//UPGRADE_NOTE: Final was removed from the declaration of 'disableCheck '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.CheckBox disableCheck;
			
			if (key != null)
			{
				if (DialogUtils.isDisabled(key))
					return null;
				
				disableCheck = SupportClass.CheckBoxSupport.CreateCheckBox(disableMsg);
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
				//UPGRADE_NOTE: Final was removed from the declaration of 'layout '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GroupLayout layout = new GroupLayout(panel);
				panel.setLayout(layout);
				
				layout.setAutocreateGaps(true);
				layout.setAutocreateContainerGaps(false);
				
				layout.setHorizontalGroup(layout.createParallelGroup(GroupLayout.LEADING, true).add(content).add(disableCheck));
				
				layout.setVerticalGroup(layout.createSequentialGroup().add(content).addPreferredGap(LayoutStyle.UNRELATED, GroupLayout.DEFAULT_SIZE, System.Int32.MaxValue).add(disableCheck));
				
				content = panel;
			}
			else
			{
				disableCheck = null;
			}
			
			// build the option pane and dialog
			//UPGRADE_NOTE: Final was removed from the declaration of 'opt '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'javax.swing.JOptionPane' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.JOptionPane.JOptionPane' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
			JOptionPane opt = new JOptionPane(content, messageType, optionType, icon, options, initialValue);
			//UPGRADE_NOTE: Final was removed from the declaration of 'dialog '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'javax.swing.JOptionPane.createDialog' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
			System.Windows.Forms.Form dialog = opt.createDialog(parent, title);
			
			// FIXME: setModal() is obsolete. Use setModalityType() in 1.6+.
			//    d.setModalityType(JDialog.ModalityType.APPLICATION_MODAL);
			//UPGRADE_ISSUE: Method 'java.awt.Dialog.setModal' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtDialogsetModal_boolean'"
			dialog.setModal(true);
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			dialog.Closing += new System.ComponentModel.CancelEventHandler(Dialogs.Dialogs_Closing_DISPOSE_ON_CLOSE);
			dialog.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			dialog.pack();
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			SupportClass.SetPropertyAsVirtual(dialog, "Visible", true);
			
			if (disableCheck != null && disableCheck.Checked)
			{
				DialogUtils.setDisabled(key, true);
			}
			
			//UPGRADE_ISSUE: Method 'javax.swing.JOptionPane.getValue' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
			return opt.getValue();
		}
		
		/// <summary> Creates dialog contents with the given title and description.
		/// 
		/// </summary>
		/// <param name="title">the title
		/// </param>
		/// <param name="description">the description
		/// </param>
		private static System.Windows.Forms.Control buildContents(System.String title, System.String description)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'titleLabel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label;
			temp_label = new System.Windows.Forms.Label();
			temp_label.Text = title;
			System.Windows.Forms.Label titleLabel = temp_label;
			//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Font f = titleLabel.Font;
			//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
			titleLabel.Font = new System.Drawing.Font(f.FontFamily, (System.Drawing.FontStyle) System.Drawing.FontStyle.Bold, (int) f.Size * 1.2f);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'descriptionLabel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FlowLabel descriptionLabel = new FlowLabel(description);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
			//UPGRADE_NOTE: Final was removed from the declaration of 'layout '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			GroupLayout layout = new GroupLayout(panel);
			panel.setLayout(layout);
			
			layout.setAutocreateGaps(true);
			layout.setAutocreateContainerGaps(false);
			
			layout.setHorizontalGroup(layout.createParallelGroup(GroupLayout.LEADING, true).add(titleLabel).add(descriptionLabel));
			
			layout.setVerticalGroup(layout.createSequentialGroup().add(titleLabel).addPreferredGap(LayoutStyle.UNRELATED).add(descriptionLabel));
			
			return panel;
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'loremIpsum '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
			
			showMessageDialog(null, "Message Dialog", "This Is the Header", loremIpsum, (int) System.Windows.Forms.MessageBoxIcon.Information);
			
			showMessageDialog(null, "Message Dialog", "This Is the Header", loremIpsum, (int) System.Windows.Forms.MessageBoxIcon.Information, (System.Object) true, "Don't show this again");
			
			showMessageDialog(null, "Message Dialog", "This Is the Header", loremIpsum, (int) System.Windows.Forms.MessageBoxIcon.Information, (System.Object) true, "Don't show this again");
			
			showConfirmDialog(null, "Confirmation Dialog", "This Is the Header", loremIpsum, (int) System.Windows.Forms.MessageBoxIcon.Information, (int) System.Windows.Forms.MessageBoxButtons.YesNo);
			
			System.Environment.Exit(0);
		}
		private static void  Dialogs_Closing_DISPOSE_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 2);
		}
	}
}