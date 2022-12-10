/*
* $Id$
*
* Copyright (c) 2004 by Rodney Kinney
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
using GameModule = VassalSharp.build.GameModule;
using ScrollPane = VassalSharp.tools.ScrollPane;
namespace VassalSharp.configure
{
	
	/// <summary> Dialog for reporting the results of validating a GameModule</summary>
	[Serializable]
	public class ValidationReportDialog:System.Windows.Forms.Form
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(ValidationReportDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ValidationReportDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ValidationReportDialog enclosingInstance;
			public ValidationReportDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.callback.cancel();
				Enclosing_Instance.Dispose();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(ValidationReportDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ValidationReportDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ValidationReportDialog enclosingInstance;
			public ValidationReportDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.callback.ok();
				Enclosing_Instance.Dispose();
			}
		}
		private const long serialVersionUID = 1L;
		
		private ValidationReportDialog.CallBack callback;
		
		public ValidationReportDialog(ValidationReport report, ValidationReportDialog.CallBack cb):base(GameModule.getGameModule().getFrame(), false)
		{
			Text = "Problems found in module";
			this.callback = cb;
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box reportBox = Box.createVerticalBox();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(reportBox);
			System.Windows.Forms.Panel buttonPanel = new System.Windows.Forms.Panel();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(buttonPanel);
			buttonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			buttonPanel.SendToBack();
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < String > warnings = report.getWarnings();
			switch (warnings.size())
			{
				
				case 0: 
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = "No problems found";
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					reportBox.Controls.Add(temp_Control);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control2;
					temp_Control2 = createOkButton();
					buttonPanel.Controls.Add(temp_Control2);
					break;
				
				case 1: 
					System.Windows.Forms.Label temp_label4;
					temp_label4 = new System.Windows.Forms.Label();
					temp_label4.Text = "A problem was found in this module.";
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control3;
					temp_Control3 = temp_label4;
					reportBox.Controls.Add(temp_Control3);
					System.Windows.Forms.Label temp_label6;
					temp_label6 = new System.Windows.Forms.Label();
					temp_label6.Text = warnings.get_Renamed(0) + ".";
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control4;
					temp_Control4 = temp_label6;
					reportBox.Controls.Add(temp_Control4);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control5;
					temp_Control5 = createOkButton();
					buttonPanel.Controls.Add(temp_Control5);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control6;
					temp_Control6 = createCancelButton();
					buttonPanel.Controls.Add(temp_Control6);
					break;
				
				default: 
					System.Windows.Forms.Label temp_label8;
					temp_label8 = new System.Windows.Forms.Label();
					temp_label8.Text = "The following problems were found in this module.";
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control7;
					temp_Control7 = temp_label8;
					reportBox.Controls.Add(temp_Control7);
					System.Windows.Forms.Label temp_label10;
					temp_label10 = new System.Windows.Forms.Label();
					temp_label10.Text = "If not fixed, they could cause bugs during game play.";
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control8;
					temp_Control8 = temp_label10;
					reportBox.Controls.Add(temp_Control8);
					System.Windows.Forms.ListBox list = new JList(warnings.toArray());
					//UPGRADE_ISSUE: Method 'javax.swing.JList.getVisibleRowCount' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJListgetVisibleRowCount'"
					list.setVisibleRowCount(Math.min(list.getVisibleRowCount(), warnings.size()));
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control9;
					temp_Control9 = new ScrollPane(list);
					reportBox.Controls.Add(temp_Control9);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control10;
					temp_Control10 = createOkButton();
					buttonPanel.Controls.Add(temp_Control10);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control11;
					temp_Control11 = createCancelButton();
					buttonPanel.Controls.Add(temp_Control11);
					break;
				
			}
			
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
		}
		
		private System.Windows.Forms.Button createCancelButton()
		{
			System.Windows.Forms.Button cancel = SupportClass.ButtonSupport.CreateStandardButton("Cancel");
			cancel.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(cancel);
			return cancel;
		}
		
		private System.Windows.Forms.Button createOkButton()
		{
			System.Windows.Forms.Button ok = SupportClass.ButtonSupport.CreateStandardButton("Ignore, save anyway");
			ok.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(ok);
			return ok;
		}
		
		public interface CallBack
		{
			void  ok();
			void  cancel();
		}
	}
}