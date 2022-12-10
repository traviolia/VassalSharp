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
namespace VassalSharp.tools.swing
{
	
	/// <author>  Joel Uckelman
	/// </author>
	/// <since> 3.1.0
	/// </since>
	[Serializable]
	public class DetailsButton:System.Windows.Forms.Button
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction:SupportClass.ActionSupport
		{
			private void  InitBlock(DetailsButton enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private DetailsButton enclosingInstance;
			public DetailsButton Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString_javaxswingIcon'"
			internal AnonymousClassAbstractAction(DetailsButton enclosingInstance, System.String Param1, System.Drawing.Image Param2):base(Param1, Param2)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				Enclosing_Instance.Expanded = !Enclosing_Instance.expander.Visible;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassComponentAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassComponentAdapter
		{
			public AnonymousClassComponentAdapter(DetailsButton enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(DetailsButton enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private DetailsButton enclosingInstance;
			public DetailsButton Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			public void  componentResized(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				if (!Enclosing_Instance.expander.Visible)
				{
					//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
					Enclosing_Instance.expander.Size = new System.Drawing.Size(Enclosing_Instance.buddy.Width, Enclosing_Instance.expander.Height);
				}
			}
		}
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
				//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.TextBox temp_TextBox;
				temp_TextBox = new System.Windows.Forms.TextBox();
				temp_TextBox.Multiline = true;
				temp_TextBox.WordWrap = false;
				temp_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
				temp_TextBox.Text = loremIpsum;
				System.Windows.Forms.TextBox a = temp_TextBox;
				a.WordWrap = true;
				a.WordWrap = true;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'sp1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
				System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
				temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
				temp_scrollablecontrol.AutoScroll = true;
				temp_scrollablecontrol.Controls.Add(a);
				System.Windows.Forms.ScrollableControl sp1 = temp_scrollablecontrol;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.TextBox temp_TextBox2;
				temp_TextBox2 = new System.Windows.Forms.TextBox();
				temp_TextBox2.Multiline = true;
				temp_TextBox2.WordWrap = false;
				temp_TextBox2.ScrollBars = System.Windows.Forms.ScrollBars.Both;
				temp_TextBox2.Text = loremIpsum;
				System.Windows.Forms.TextBox b = temp_TextBox2;
				b.WordWrap = true;
				b.WordWrap = true;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'sp2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
				System.Windows.Forms.ScrollableControl temp_scrollablecontrol2;
				temp_scrollablecontrol2 = new System.Windows.Forms.ScrollableControl();
				temp_scrollablecontrol2.AutoScroll = true;
				temp_scrollablecontrol2.Controls.Add(b);
				System.Windows.Forms.ScrollableControl sp2 = temp_scrollablecontrol2;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'db '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				DetailsButton db = new DetailsButton("Show", "Hide", sp2);
				db.Buddy = sp1;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'contents '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Panel contents = new System.Windows.Forms.Panel();
				contents.setLayout(new MigLayout("hidemode 3", "", "[]unrel[]rel[]"));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				contents.Controls.Add(sp1);
				sp1.Dock = new System.Windows.Forms.DockStyle();
				sp1.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				contents.Controls.Add(db);
				db.Dock = new System.Windows.Forms.DockStyle();
				db.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				contents.Controls.Add(sp2);
				sp2.Dock = new System.Windows.Forms.DockStyle();
				sp2.BringToFront();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Form d = SupportClass.DialogSupport.CreateDialog();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				d.Controls.Add(contents);
				d.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
				//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
				d.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
				d.Closing += new System.ComponentModel.CancelEventHandler(this.AnonymousClassRunnable_Closing_DISPOSE_ON_CLOSE);
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				d.pack();
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(d, "Visible", true);
			}
			private void  AnonymousClassRunnable_Closing_DISPOSE_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
			{
				e.Cancel = true;
				SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 2);
			}
		}
		private class AnonymousClassRunnable1 : IThreadRunnable
		{
			public AnonymousClassRunnable1(System.String loremIpsum)
			{
				InitBlock(loremIpsum);
			}
			private void  InitBlock(System.String loremIpsum)
			{
				this.loremIpsum = loremIpsum;
			}
			//UPGRADE_NOTE: Final variable loremIpsum was copied into class AnonymousClassRunnable1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.String loremIpsum;
			public virtual void  Run()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'a '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Label temp_label;
				temp_label = new System.Windows.Forms.Label();
				temp_label.Text = "This is an expanding pane.";
				System.Windows.Forms.Label a = temp_label;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.TextBox temp_TextBox;
				temp_TextBox = new System.Windows.Forms.TextBox();
				temp_TextBox.Multiline = true;
				temp_TextBox.WordWrap = false;
				temp_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
				temp_TextBox.Text = loremIpsum;
				System.Windows.Forms.TextBox b = temp_TextBox;
				b.WordWrap = true;
				b.WordWrap = true;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'sp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
				System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
				temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
				temp_scrollablecontrol.AutoScroll = true;
				temp_scrollablecontrol.Controls.Add(b);
				System.Windows.Forms.ScrollableControl sp = temp_scrollablecontrol;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'db '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				DetailsButton db = new DetailsButton("Show", "Hide", sp);
				db.Buddy = a;
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'contents '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Panel contents = new System.Windows.Forms.Panel();
				contents.setLayout(new MigLayout("hidemode 3", "", "[]unrel[]rel[]unrel[]"));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				contents.Controls.Add(a);
				a.Dock = new System.Windows.Forms.DockStyle();
				a.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				contents.Controls.Add(db);
				db.Dock = new System.Windows.Forms.DockStyle();
				db.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				contents.Controls.Add(sp);
				sp.Dock = new System.Windows.Forms.DockStyle();
				sp.BringToFront();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = SupportClass.CheckBoxSupport.CreateCheckBox("Disable?");
				contents.Controls.Add(temp_Control);
				temp_Control.Dock = new System.Windows.Forms.DockStyle();
				temp_Control.BringToFront();
				
				//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_ISSUE: Method 'javax.swing.JOptionPane.createDialog' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.JOptionPane.JOptionPane' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJOptionPane'"
				System.Windows.Forms.Form d = new JOptionPane(contents, (int) System.Windows.Forms.MessageBoxIcon.Error, (int) System.Windows.Forms.MessageBoxButtons.OK).createDialog(null, "Test");
				
				//UPGRADE_ISSUE: Method 'java.awt.Dialog.setModal' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtDialogsetModal_boolean'"
				d.setModal(true);
				d.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
				//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
				d.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
				d.Closing += new System.ComponentModel.CancelEventHandler(this.AnonymousClassRunnable1_Closing_DISPOSE_ON_CLOSE);
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				d.pack();
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(d, "Visible", true);
			}
			private void  AnonymousClassRunnable1_Closing_DISPOSE_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
			{
				e.Cancel = true;
				SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 2);
			}
		}
		virtual public System.Windows.Forms.Control Expander
		{
			set
			{
				if (expander == null)
				{
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(value, "Visible", false);
				}
				expander = value;
			}
			
		}
		virtual public System.String ButtonShowText
		{
			set
			{
				showText = value;
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				if (!expander.Visible)
					Text = showText;
			}
			
		}
		virtual public System.String ButtonHideText
		{
			set
			{
				hideText = value;
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				if (expander.Visible)
					Text = hideText;
			}
			
		}
		/// <summary> Sets the buddy component for the expanding component.
		/// The width of the expanding component is adjusted to match the width of
		/// the buddy component when the expanding component is invisible.
		/// 
		/// </summary>
		/// <param name="comp">the buddy component
		/// </param>
		virtual public System.Windows.Forms.Control Buddy
		{
			set
			{
				buddy = value;
				
				buddy.Resize += new System.EventHandler(new AnonymousClassComponentAdapter(this).componentResized);
			}
			
		}
		virtual public bool Expanded
		{
			set
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
				System.Windows.Forms.Form w = (System.Windows.Forms.Form) expander.Parent;
				//UPGRADE_NOTE: Final was removed from the declaration of 'ws '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Size ws = w.Size;
				
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				if (!expander.Visible)
				{
					Text = hideText;
					Image = expandedIcon;
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(expander, "Visible", true);
					
					ws.Height += eh;
					
					if (!expander.isPreferredSizeSet())
					{
						//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_int_int'"
						expander.Size = new System.Drawing.Size(buddy.Width, 300);
					}
				}
				else
				{
					Text = showText;
					Image = collapsedIcon;
					eh = expander.Height;
					//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
					//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
					//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
					SupportClass.SetPropertyAsVirtual(expander, "Visible", false);
					ws.Height -= eh;
				}
				
				fixSize(w);
				//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_javaawtDimension'"
				w.Size = ws;
				w.PerformLayout();
			}
			
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'collapsedIcon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Method 'javax.swing.UIManager.getIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		protected internal static readonly System.Drawing.Image collapsedIcon = UIManager.getIcon("Tree.collapsedIcon");
		//UPGRADE_NOTE: Final was removed from the declaration of 'expandedIcon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Method 'javax.swing.UIManager.getIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		protected internal static readonly System.Drawing.Image expandedIcon = UIManager.getIcon("Tree.expandedIcon");
		
		protected internal System.String showText;
		protected internal System.String hideText;
		
		protected internal System.Windows.Forms.Control expander;
		protected internal System.Windows.Forms.Control buddy;
		
		protected internal static int eh = 300;
		
		public DetailsButton(System.String showText, System.String hideText):this(showText, hideText, null, null)
		{
		}
		
		public DetailsButton(System.String showText, System.String hideText, System.Windows.Forms.Control expander):this(showText, hideText, expander, null)
		{
		}
		
		public DetailsButton(System.String showText, System.String hideText, System.Windows.Forms.Control expander, System.Windows.Forms.Control buddy)
		{
			this.showText = showText;
			this.hideText = hideText;
			
			if (expander != null)
				Expander = expander;
			
			//UPGRADE_WARNING: At least one expression was used more than once in the target code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1181'"
			Click += new System.EventHandler(new AnonymousClassAbstractAction(this, showText, collapsedIcon).actionPerformed);
			Text = new AnonymousClassAbstractAction(this, showText, collapsedIcon).Description;
			Image = new AnonymousClassAbstractAction(this, showText, collapsedIcon).Icon;
			
			if (buddy != null)
				Buddy = buddy;
			
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractButton.setBorderPainted' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractButtonsetBorderPainted_boolean'"
			setBorderPainted(false);
			//UPGRADE_ISSUE: Method 'javax.swing.AbstractButton.setContentAreaFilled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingAbstractButtonsetContentAreaFilled_boolean'"
			setContentAreaFilled(false);
		}
		
		protected internal virtual void  fixSize(System.Windows.Forms.Control c)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Component comp: c.getComponents())
			{
				if (comp != expander && comp is System.Windows.Forms.Control)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'con '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Windows.Forms.Control con = (System.Windows.Forms.Control) comp;
					
					if (!con.Contains(expander))
					{
						//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
						System.Drawing.Size d = con.Size;
						con.setPreferredSize(d);
					}
					
					//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					if (!(con is System.Windows.Forms.ScrollableControl))
						fixSize(con);
				}
				else
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Size d = comp.getSize();
					comp.setPreferredSize(d);
				}
			}
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'loremIpsum '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.\n\nLorem ipsum dolor sit amet, consectetur adipisicing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
			
			//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
			SwingUtilities.invokeLater(new AnonymousClassRunnable(loremIpsum));
			
			//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
			SwingUtilities.invokeLater(new AnonymousClassRunnable1(loremIpsum));
		}
	}
}