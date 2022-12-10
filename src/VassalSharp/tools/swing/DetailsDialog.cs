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
* MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
* Library General Public License for more details.
*
* You should have received a copy of the GNU Library General Public
* License along with this library; if not, copies are available
* at http://www.opensource.org.
*/
using System;
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
using DialogUtils = VassalSharp.tools.DialogUtils;
namespace VassalSharp.tools.swing
{
	
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	public class DetailsDialog
	{
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public AnonymousClassRunnable(System.String loremIpsum)
			{
				InitBlock(loremIpsum);
			}
			private void  InitBlock(System.String loremIpsum)
			{
				this.loremIpsum = loremIpsum;
			}
			//UPGRADE_NOTE: Final variable loremIpsum was copied into class AnonymousClassRunnable. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.String loremIpsum;
			public virtual void  Run()
			{
				DetailsDialog.showDialog(null, "Test", "Test Header", loremIpsum, loremIpsum, "Don't show this dialog again", "Show Details", "Hide Details", (int) System.Windows.Forms.MessageBoxIcon.Exclamation, true);
				
				DetailsDialog.showDialog(null, "Test", "Test Header", loremIpsum, loremIpsum, "Don't show this dialog again", "Show Details", "Hide Details", (int) System.Windows.Forms.MessageBoxIcon.Error, (System.Object) null);
			}
		}
		
		public static void  showDialog(System.Windows.Forms.Control parent, System.String title, System.String header, System.String message, System.String details, System.String disableText, System.String showText, System.String hideText, int messageType, System.Object key)
		{
			// set a slightly larger, bold font for the header
			//UPGRADE_NOTE: Final was removed from the declaration of 'headerLabel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label;
			temp_label = new System.Windows.Forms.Label();
			temp_label.Text = header;
			System.Windows.Forms.Label headerLabel = temp_label;
			//UPGRADE_NOTE: Final was removed from the declaration of 'f '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Drawing.Font f = headerLabel.Font;
			//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
			headerLabel.Font = new System.Drawing.Font(f.FontFamily, (System.Drawing.FontStyle) System.Drawing.FontStyle.Bold, (int) f.Size * 1.2f);
			
			// put together the paragraphs of the message
			//UPGRADE_NOTE: Final was removed from the declaration of 'messageLabel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FlowLabel messageLabel = new FlowLabel(message);
			
			// set up the details view
			//UPGRADE_NOTE: Final was removed from the declaration of 'detailsArea '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.TextBox temp_TextBox;
			temp_TextBox = new System.Windows.Forms.TextBox();
			temp_TextBox.Multiline = true;
			temp_TextBox.WordWrap = false;
			temp_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			temp_TextBox.Text = details;
			System.Windows.Forms.TextBox detailsArea = temp_TextBox;
			detailsArea.ReadOnly = !false;
			detailsArea.WordWrap = true;
			//UPGRADE_ISSUE: Method 'javax.swing.JTextArea.setTabSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTextAreasetTabSize_int'"
			detailsArea.setTabSize(2);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'detailsScroll '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
			temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol.AutoScroll = true;
			temp_scrollablecontrol.Controls.Add(detailsArea);
			System.Windows.Forms.ScrollableControl detailsScroll = temp_scrollablecontrol;
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			SupportClass.SetPropertyAsVirtual(detailsScroll, "Visible", false);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'detailsButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			DetailsButton detailsButton = new DetailsButton(showText, hideText, detailsScroll);
			detailsButton.Buddy = messageLabel;
			
			// build the contents panel
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new System.Windows.Forms.Panel();
			panel.setLayout(new MigLayout("hidemode 3", "", key != null?"[]unrel[]unrel[]rel[]unrel[]":"[]unrel[]unrel[]rel[]"));
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(headerLabel);
			headerLabel.Dock = new System.Windows.Forms.DockStyle();
			headerLabel.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(messageLabel);
			messageLabel.Dock = new System.Windows.Forms.DockStyle();
			messageLabel.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(detailsButton);
			detailsButton.Dock = new System.Windows.Forms.DockStyle();
			detailsButton.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(detailsScroll);
			detailsScroll.Dock = new System.Windows.Forms.DockStyle();
			detailsScroll.BringToFront();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'disableCheck '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.CheckBox disableCheck;
			
			if (key != null)
			{
				disableCheck = SupportClass.CheckBoxSupport.CreateCheckBox(disableText);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				panel.Controls.Add(disableCheck);
				disableCheck.Dock = new System.Windows.Forms.DockStyle();
				disableCheck.BringToFront();
			}
			else
			{
				disableCheck = null;
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'dialog '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Method 'javax.swing.JOptionPane.createDialog' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.JOptionPane.JOptionPane' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
			System.Windows.Forms.Form dialog = new JOptionPane(panel, messageType, (int) System.Windows.Forms.MessageBoxButtons.OK).createDialog(parent, title);
			
			// FIXME: setModal() is obsolete. Use setModalityType() in 1.6+.
			//    d.setModalityType(JDialog.ModalityType.APPLICATION_MODAL);
			//UPGRADE_ISSUE: Method 'java.awt.Dialog.setModal' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtDialogsetModal_boolean'"
			dialog.setModal(true);
			dialog.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			dialog.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			dialog.Closing += new System.ComponentModel.CancelEventHandler(DetailsDialog.DetailsDialog_Closing_DISPOSE_ON_CLOSE);
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
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'loremIpsum '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
			
			//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
			SwingUtilities.invokeLater(new AnonymousClassRunnable(loremIpsum));
		}
		private static void  DetailsDialog_Closing_DISPOSE_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 2);
		}
	}
}