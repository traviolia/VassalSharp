/*
* $Id$
*
* Copyright (c) 2008-2009 by Joel Uckelman
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
//UPGRADE_TODO: The type 'java.util.concurrent.CancellationException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using CancellationException = java.util.concurrent.CancellationException;
//UPGRADE_TODO: The type 'java.util.concurrent.CountDownLatch' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using CountDownLatch = java.util.concurrent.CountDownLatch;
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutionException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutionException = java.util.concurrent.ExecutionException;
//UPGRADE_TODO: The type 'java.util.concurrent.TimeUnit' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TimeUnit = java.util.concurrent.TimeUnit;
//UPGRADE_TODO: The type 'java.util.concurrent.TimeoutException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using TimeoutException = java.util.concurrent.TimeoutException;
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
//UPGRADE_TODO: The type 'org.jdesktop.swingworker.SwingWorker' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SwingWorker = org.jdesktop.swingworker.SwingWorker;
//UPGRADE_TODO: The type 'org.jdesktop.swingx.JXBusyLabel' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using JXBusyLabel = org.jdesktop.swingx.JXBusyLabel;
//UPGRADE_TODO: The type 'org.jdesktop.swingx.JXHeader' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using JXHeader = org.jdesktop.swingx.JXHeader;
using Info = VassalSharp.Info;
using Resources = VassalSharp.i18n.Resources;
using DetailsButton = VassalSharp.tools.swing.DetailsButton;
using FlowLabel = VassalSharp.tools.swing.FlowLabel;
using VassalVersion = VassalSharp.tools.version.VassalVersion;
using VersionUtils = VassalSharp.tools.version.VersionUtils;
namespace VassalSharp.tools
{
	
	/// <since> 3.1.0
	/// </since>
	/// <author>  Joel Uckelman
	/// </author>
	[Serializable]
	public class BugDialog:System.Windows.Forms.Form
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(BugDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BugDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BugDialog enclosingInstance;
			public BugDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosed(System.Object event_sender, System.EventArgs e)
			{
				if (Enclosing_Instance.checkRequest != null)
					Enclosing_Instance.checkRequest.cancel(true);
				
				if (Enclosing_Instance.sendRequest != null)
					Enclosing_Instance.sendRequest.cancel(true);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction:SupportClass.ActionSupport
		{
			private void  InitBlock(BugDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BugDialog enclosingInstance;
			public BugDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction(BugDialog enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.Dispose();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction1:SupportClass.ActionSupport
		{
			private void  InitBlock(BugDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BugDialog enclosingInstance;
			public BugDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction1(BugDialog enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.showSendingBugReportPanel();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction2:SupportClass.ActionSupport
		{
			private void  InitBlock(BugDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BugDialog enclosingInstance;
			public BugDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction2(BugDialog enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.showEmergencySavePanel();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction3:SupportClass.ActionSupport
		{
			private void  InitBlock(BugDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BugDialog enclosingInstance;
			public BugDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction3(BugDialog enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.showEmergencySavePanel();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction4:SupportClass.ActionSupport
		{
			private void  InitBlock(BugDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BugDialog enclosingInstance;
			public BugDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction4(BugDialog enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.showEmergencySavePanel();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction5' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction5:SupportClass.ActionSupport
		{
			private void  InitBlock(BugDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BugDialog enclosingInstance;
			public BugDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction5(BugDialog enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.Dispose();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassAbstractAction6' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		private class AnonymousClassAbstractAction6:SupportClass.ActionSupport
		{
			private void  InitBlock(BugDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BugDialog enclosingInstance;
			public BugDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_TODO: Constructor 'javax.swing.AbstractAction.AbstractAction' was converted to 'ActionSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingAbstractActionAbstractAction_javalangString'"
			internal AnonymousClassAbstractAction6(BugDialog enclosingInstance, System.String Param1):base(Param1)
			{
				InitBlock(enclosingInstance);
			}
			private const long serialVersionUID = 1L;
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.Dispose();
			}
		}
		private class AnonymousClassRunnable : IThreadRunnable
		{
			public virtual void  Run()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'bd '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				BugDialog bd = new BugDialog(null, null);
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				bd.Visible = true;
			}
		}
		//UPGRADE_TODO: The following property was automatically generated and it must be implemented in order to preserve the class logic. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1232'"
		//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
		//UPGRADE_TODO: More than one of the Java class members are converted to this same member in .NET. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1231'"
		new virtual public bool Visible
		{
			get
			{
				return base.Visible;
			}
			
			set
			{
				//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
				if (value && !Visible)
				{
					checkRequest = new CheckRequest(this);
					checkRequest.execute();
				}
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				base.Visible = value;
			}
			
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		private System.Exception thrown;
		private System.String errorLog;
		
		private System.Windows.Forms.Panel contents;
		//UPGRADE_ISSUE: Class 'java.awt.CardLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
		private CardLayout deck;
		
		private System.Windows.Forms.Panel buttons;
		//UPGRADE_ISSUE: Class 'java.awt.CardLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
		private CardLayout button_deck;
		
		private System.Windows.Forms.TextBox descriptionArea;
		private System.Windows.Forms.TextBox emailField;
		
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Windows.Forms.ScrollableControl descriptionScroll;
		
		//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
		//UPGRADE_NOTE: Exception 'java.lang.Throwable' was converted to 'System.Exception' which has different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1100'"
		public BugDialog(System.Windows.Forms.Form owner, System.Exception thrown):base()
		{
			//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_boolean'"
			SupportClass.DialogSupport.SetDialog(this, owner);
			
			this.thrown = thrown;
			this.errorLog = BugUtils.ErrorLog;
			
			//
			// header
			//
			//UPGRADE_NOTE: Final was removed from the declaration of 'header '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.ImageIcon.ImageIcon' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingImageIconImageIcon_javanetURL'"
			//UPGRADE_TODO: Method 'java.lang.Class.getResource' was converted to 'System.Uri' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangClassgetResource_javalangString'"
			JXHeader header = new JXHeader(Resources.getString("BugDialog.heading"), Resources.getString("BugDialog.message"), new ImageIcon(new System.Uri(System.IO.Path.GetFullPath("/icons/48x48/bug.png"))));
			
			//
			// dialog
			//
			Text = Resources.getString("BugDialog.title");
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Closing += new System.ComponentModel.CancelEventHandler(this.BugDialog_Closing_DISPOSE_ON_CLOSE);
			FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			Closed += new System.EventHandler(new AnonymousClassWindowAdapter(this).windowClosed);
			
			add(header, System.Windows.Forms.DockStyle.Top);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = buildContentsPanel();
			Controls.Add(temp_Control);
			temp_Control.Dock = System.Windows.Forms.DockStyle.Fill;
			temp_Control.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control2;
			temp_Control2 = buildButtonPanel();
			Controls.Add(temp_Control2);
			temp_Control2.Dock = System.Windows.Forms.DockStyle.Bottom;
			temp_Control2.SendToBack();
			
			showVersionCheckPanel();
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
		}
		
		private System.Windows.Forms.Control buildContentsPanel()
		{
			//UPGRADE_ISSUE: Constructor 'java.awt.CardLayout.CardLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			deck = new CardLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			contents = new System.Windows.Forms.Panel();
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(contents.CreateGraphics(), 0, 0, contents.Width, contents.Height, System.Windows.Forms.Border3DStyle.Adjust);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = buildVersionCheckPanel();
			contents.Controls.Add(temp_Control);
			temp_Control.Dock = new System.Windows.Forms.DockStyle();
			temp_Control.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control2;
			temp_Control2 = buildCurrentVersionPanel();
			contents.Controls.Add(temp_Control2);
			temp_Control2.Dock = new System.Windows.Forms.DockStyle();
			temp_Control2.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control3;
			temp_Control3 = buildSendingBugReportPanel();
			contents.Controls.Add(temp_Control3);
			temp_Control3.Dock = new System.Windows.Forms.DockStyle();
			temp_Control3.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control4;
			temp_Control4 = buildOldVersionPanel();
			contents.Controls.Add(temp_Control4);
			temp_Control4.Dock = new System.Windows.Forms.DockStyle();
			temp_Control4.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control5;
			temp_Control5 = buildConnectionFailedPanel();
			contents.Controls.Add(temp_Control5);
			temp_Control5.Dock = new System.Windows.Forms.DockStyle();
			temp_Control5.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control6;
			temp_Control6 = buildEmergencySavePanel();
			contents.Controls.Add(temp_Control6);
			temp_Control6.Dock = new System.Windows.Forms.DockStyle();
			temp_Control6.BringToFront();
			
			return contents;
		}
		
		private System.Windows.Forms.Control buildButtonPanel()
		{
			//UPGRADE_ISSUE: Constructor 'java.awt.CardLayout.CardLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			button_deck = new CardLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			buttons = new System.Windows.Forms.Panel();
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = buildVersionCheckButtons();
			buttons.Controls.Add(temp_Control);
			temp_Control.Dock = new System.Windows.Forms.DockStyle();
			temp_Control.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control2;
			temp_Control2 = buildCurrentVersionButtons();
			buttons.Controls.Add(temp_Control2);
			temp_Control2.Dock = new System.Windows.Forms.DockStyle();
			temp_Control2.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control3;
			temp_Control3 = buildSendingBugReportButtons();
			buttons.Controls.Add(temp_Control3);
			temp_Control3.Dock = new System.Windows.Forms.DockStyle();
			temp_Control3.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control4;
			temp_Control4 = buildOldVersionButtons();
			buttons.Controls.Add(temp_Control4);
			temp_Control4.Dock = new System.Windows.Forms.DockStyle();
			temp_Control4.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control5;
			temp_Control5 = buildConnectionFailedButtons();
			buttons.Controls.Add(temp_Control5);
			temp_Control5.Dock = new System.Windows.Forms.DockStyle();
			temp_Control5.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control6;
			temp_Control6 = buildEmergencySaveButtons();
			buttons.Controls.Add(temp_Control6);
			temp_Control6.Dock = new System.Windows.Forms.DockStyle();
			temp_Control6.BringToFront();
			
			return buttons;
		}
		
		private System.Windows.Forms.Control buildVersionCheckPanel()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'spinner '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			JXBusyLabel spinner = new JXBusyLabel(new System.Drawing.Size(40, 40));
			spinner.setBusy(true);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FlowLabel label = new FlowLabel(Resources.getString("BugDialog.collecting_details"));
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new JPanel(new MigLayout("", "", "[]push[]push"));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(label);
			label.Dock = new System.Windows.Forms.DockStyle();
			label.BringToFront();
			panel.add(spinner, "cell 0 1, align center");
			
			return panel;
		}
		
		private System.Windows.Forms.Control buildVersionCheckButtons()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'cancelButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Constructor 'javax.swing.JButton.JButton' was converted to 'System.Windows.Forms.Button.Button' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJButtonJButton_javaxswingAction'"
			System.Windows.Forms.Button cancelButton = new System.Windows.Forms.Button();
			SupportClass.ActionSupport tmp_action = new AnonymousClassAbstractAction(this, Resources.getString(Resources.CANCEL));
			cancelButton.Click += new System.EventHandler(tmp_action.actionPerformed);
			cancelButton.Image = tmp_action.Icon;
			cancelButton.Text = tmp_action.Description;
			
			// FIXME: tags don't push buttons to ends?
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new JPanel(new MigLayout("align right"));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(cancelButton);
			cancelButton.Dock = new System.Windows.Forms.DockStyle();
			cancelButton.BringToFront();
			
			return panel;
		}
		
		private System.Windows.Forms.Control buildCurrentVersionPanel()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FlowLabel label = new FlowLabel(Resources.getString("BugDialog.current_version_instructions"));
			
			System.Windows.Forms.TextBox temp_TextBox;
			temp_TextBox = new System.Windows.Forms.TextBox();
			temp_TextBox.Multiline = true;
			temp_TextBox.WordWrap = false;
			temp_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			descriptionArea = temp_TextBox;
			descriptionArea.WordWrap = true;
			descriptionArea.WordWrap = true;
			
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
			temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol.AutoScroll = true;
			temp_scrollablecontrol.Controls.Add(descriptionArea);
			descriptionScroll = temp_scrollablecontrol;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'descriptionLabel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label;
			temp_label = new System.Windows.Forms.Label();
			temp_label.Text = Resources.getString("BugDialog.bug_description");
			System.Windows.Forms.Label descriptionLabel = temp_label;
			//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
			descriptionLabel.Font = new System.Drawing.Font(descriptionLabel.Font, (System.Drawing.FontStyle) System.Drawing.FontStyle.Bold);
			//UPGRADE_ISSUE: Method 'javax.swing.JLabel.setLabelFor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJLabelsetLabelFor_javaawtComponent'"
			descriptionLabel.setLabelFor(descriptionScroll);
			
			//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
			emailField = new System.Windows.Forms.TextBox();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'emailLabel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label2;
			temp_label2 = new System.Windows.Forms.Label();
			temp_label2.Text = Resources.getString("BugDialog.user_email_address");
			System.Windows.Forms.Label emailLabel = temp_label2;
			//UPGRADE_ISSUE: Method 'javax.swing.JLabel.setLabelFor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJLabelsetLabelFor_javaawtComponent'"
			emailLabel.setLabelFor(emailField);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'detailsScroll '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Windows.Forms.ScrollableControl detailsScroll = buildDetailsScroll();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'detailsButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			DetailsButton detailsButton = new DetailsButton(Resources.getString("Dialogs.show_details"), Resources.getString("Dialogs.hide_details"), detailsScroll);
			detailsButton.Buddy = label;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new JPanel(new MigLayout("hidemode 3", "", "[]unrel[]rel[]unrel[]unrel[]rel[]"));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(label);
			label.Dock = new System.Windows.Forms.DockStyle();
			label.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(descriptionLabel);
			descriptionLabel.Dock = new System.Windows.Forms.DockStyle();
			descriptionLabel.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(descriptionScroll);
			descriptionScroll.Dock = new System.Windows.Forms.DockStyle();
			descriptionScroll.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(emailLabel);
			emailLabel.Dock = new System.Windows.Forms.DockStyle();
			emailLabel.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(emailField);
			emailField.Dock = new System.Windows.Forms.DockStyle();
			emailField.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(detailsButton);
			detailsButton.Dock = new System.Windows.Forms.DockStyle();
			detailsButton.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(detailsScroll);
			detailsScroll.Dock = new System.Windows.Forms.DockStyle();
			detailsScroll.BringToFront();
			
			return panel;
		}
		
		private System.Windows.Forms.Control buildCurrentVersionButtons()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'sendButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Constructor 'javax.swing.JButton.JButton' was converted to 'System.Windows.Forms.Button.Button' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJButtonJButton_javaxswingAction'"
			System.Windows.Forms.Button sendButton = new System.Windows.Forms.Button();
			SupportClass.ActionSupport tmp_action2 = new AnonymousClassAbstractAction1(this, Resources.getString("BugDialog.send_button"));
			sendButton.Click += new System.EventHandler(tmp_action2.actionPerformed);
			sendButton.Image = tmp_action2.Icon;
			sendButton.Text = tmp_action2.Description;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'dontSendButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Constructor 'javax.swing.JButton.JButton' was converted to 'System.Windows.Forms.Button.Button' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJButtonJButton_javaxswingAction'"
			System.Windows.Forms.Button dontSendButton = new System.Windows.Forms.Button();
			SupportClass.ActionSupport tmp_action3 = new AnonymousClassAbstractAction2(this, Resources.getString("BugDialog.dont_send_button"));
			dontSendButton.Click += new System.EventHandler(tmp_action3.actionPerformed);
			dontSendButton.Image = tmp_action3.Icon;
			dontSendButton.Text = tmp_action3.Description;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new JPanel(new MigLayout("align right"));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(sendButton);
			sendButton.Dock = new System.Windows.Forms.DockStyle();
			sendButton.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(dontSendButton);
			dontSendButton.Dock = new System.Windows.Forms.DockStyle();
			dontSendButton.BringToFront();
			
			return panel;
		}
		
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Windows.Forms.ScrollableControl buildDetailsScroll()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'detailsArea '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.TextBox temp_TextBox;
			temp_TextBox = new System.Windows.Forms.TextBox();
			temp_TextBox.Multiline = true;
			temp_TextBox.WordWrap = false;
			temp_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			temp_TextBox.Text = errorLog;
			System.Windows.Forms.TextBox detailsArea = temp_TextBox;
			detailsArea.ReadOnly = !false;
			//UPGRADE_ISSUE: Method 'javax.swing.JTextArea.setTabSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTextAreasetTabSize_int'"
			detailsArea.setTabSize(2);
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
			temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol.AutoScroll = true;
			temp_scrollablecontrol.Controls.Add(detailsArea);
			return temp_scrollablecontrol;
		}
		
		private System.Windows.Forms.Control buildOldVersionPanel()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FlowLabel label = new FlowLabel(Resources.getString("BugDialog.old_version_instructions"));
			//UPGRADE_NOTE: Some methods of the 'javax.swing.event.HyperlinkListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			label.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(BrowserSupport.Listener.hyperlinkUpdate);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'detailsScroll '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Windows.Forms.ScrollableControl detailsScroll = buildDetailsScroll();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'detailsButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			DetailsButton detailsButton = new DetailsButton(Resources.getString("Dialogs.show_details"), Resources.getString("Dialogs.hide_details"), detailsScroll);
			detailsButton.Buddy = label;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new JPanel(new MigLayout("hidemode 3", "", "[]unrel[]rel[]"));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(label);
			label.Dock = new System.Windows.Forms.DockStyle();
			label.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(detailsButton);
			detailsButton.Dock = new System.Windows.Forms.DockStyle();
			detailsButton.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(detailsScroll);
			detailsScroll.Dock = new System.Windows.Forms.DockStyle();
			detailsScroll.BringToFront();
			
			return panel;
		}
		
		private System.Windows.Forms.Control buildOldVersionButtons()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'okButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Constructor 'javax.swing.JButton.JButton' was converted to 'System.Windows.Forms.Button.Button' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJButtonJButton_javaxswingAction'"
			System.Windows.Forms.Button okButton = new System.Windows.Forms.Button();
			SupportClass.ActionSupport tmp_action4 = new AnonymousClassAbstractAction3(this, Resources.getString(Resources.OK));
			okButton.Click += new System.EventHandler(tmp_action4.actionPerformed);
			okButton.Image = tmp_action4.Icon;
			okButton.Text = tmp_action4.Description;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new JPanel(new MigLayout("align right"));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(okButton);
			okButton.Dock = new System.Windows.Forms.DockStyle();
			okButton.BringToFront();
			
			return panel;
		}
		
		private System.Windows.Forms.Control buildConnectionFailedPanel()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'errorLogPath '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String errorLogPath = new System.IO.FileInfo(Info.ConfDir.FullName + "\\" + "errorLog").FullName;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FlowLabel label = new FlowLabel(Resources.getString("BugDialog.connection_failed_instructions", errorLogPath));
			//UPGRADE_NOTE: Some methods of the 'javax.swing.event.HyperlinkListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			label.LinkClicked += new System.Windows.Forms.LinkClickedEventHandler(BrowserSupport.Listener.hyperlinkUpdate);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'detailsScroll '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Windows.Forms.ScrollableControl detailsScroll = buildDetailsScroll();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'detailsButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			DetailsButton detailsButton = new DetailsButton(Resources.getString("Dialogs.show_details"), Resources.getString("Dialogs.hide_details"), detailsScroll);
			detailsButton.Buddy = label;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new JPanel(new MigLayout("hidemode 3", "", "[]unrel[]rel[]"));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(label);
			label.Dock = new System.Windows.Forms.DockStyle();
			label.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(detailsButton);
			detailsButton.Dock = new System.Windows.Forms.DockStyle();
			detailsButton.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(detailsScroll);
			detailsScroll.Dock = new System.Windows.Forms.DockStyle();
			detailsScroll.BringToFront();
			
			return panel;
		}
		
		private System.Windows.Forms.Control buildConnectionFailedButtons()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'okButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Constructor 'javax.swing.JButton.JButton' was converted to 'System.Windows.Forms.Button.Button' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJButtonJButton_javaxswingAction'"
			System.Windows.Forms.Button okButton = new System.Windows.Forms.Button();
			SupportClass.ActionSupport tmp_action5 = new AnonymousClassAbstractAction4(this, Resources.getString(Resources.OK));
			okButton.Click += new System.EventHandler(tmp_action5.actionPerformed);
			okButton.Image = tmp_action5.Icon;
			okButton.Text = tmp_action5.Description;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new JPanel(new MigLayout("align right"));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(okButton);
			okButton.Dock = new System.Windows.Forms.DockStyle();
			okButton.BringToFront();
			
			return panel;
		}
		
		private System.Windows.Forms.Control buildEmergencySavePanel()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FlowLabel label = new FlowLabel(Resources.getString("BugDialog.how_to_proceed"));
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new JPanel(new MigLayout("", "", "[]push"));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(label);
			label.Dock = new System.Windows.Forms.DockStyle();
			label.BringToFront();
			
			return panel;
		}
		
		private System.Windows.Forms.Control buildEmergencySaveButtons()
		{
			/*
			final JButton saveButton = new JButton(
			new AbstractAction("Save") {
			private static final long serialVersionUID = 1L;
			
			public void actionPerformed(ActionEvent e) {
			emergencySave();
			dispose();
			}
			}
			);
			
			final JButton dontSaveButton = new JButton(
			new AbstractAction("Don't Save") {
			private static final long serialVersionUID = 1L;
			
			public void actionPerformed(ActionEvent e) {
			dispose();
			}
			}
			);*/
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'okButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Constructor 'javax.swing.JButton.JButton' was converted to 'System.Windows.Forms.Button.Button' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJButtonJButton_javaxswingAction'"
			System.Windows.Forms.Button okButton = new System.Windows.Forms.Button();
			SupportClass.ActionSupport tmp_action6 = new AnonymousClassAbstractAction5(this, Resources.getString(Resources.OK));
			okButton.Click += new System.EventHandler(tmp_action6.actionPerformed);
			okButton.Image = tmp_action6.Icon;
			okButton.Text = tmp_action6.Description;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new JPanel(new MigLayout("align right"));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(okButton);
			okButton.Dock = new System.Windows.Forms.DockStyle();
			okButton.BringToFront();
			
			return panel;
		}
		
		private void  showVersionCheckPanel()
		{
			//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			deck.show(contents, "versionCheckPanel");
			//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			button_deck.show(buttons, "versionCheckButtons");
		}
		
		private void  showCurrentVersionPanel()
		{
			//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			deck.show(contents, "currentVersionPanel");
			//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			button_deck.show(buttons, "currentVersionButtons");
		}
		
		private void  showSendingBugReportPanel()
		{
			//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			deck.show(contents, "sendingBugReportPanel");
			//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			button_deck.show(buttons, "sendingBugReportButtons");
			
			sendRequest = new SendRequest(this);
			sendRequest.execute();
		}
		
		private void  showOldVersionPanel()
		{
			//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			deck.show(contents, "oldVersionPanel");
			//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			button_deck.show(buttons, "oldVersionButtons");
		}
		
		private void  showConnectionFailedPanel()
		{
			//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			deck.show(contents, "connectionFailedPanel");
			//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			button_deck.show(buttons, "connectionFailedButtons");
		}
		
		private void  showEmergencySavePanel()
		{
			//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			deck.show(contents, "emergencySavePanel");
			//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			button_deck.show(buttons, "emergencySaveButtons");
		}
		
		private CheckRequest checkRequest = null;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'CheckRequest' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_NOTE: Inner class 'CheckRequest' is now serializable, and this may become a security issue. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1234'"
		[Serializable]
		private class CheckRequest:SwingWorker
		{
			public CheckRequest(BugDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener
			{
				public AnonymousClassActionListener(CountDownLatch latch, CheckRequest enclosingInstance)
				{
					InitBlock(latch, enclosingInstance);
				}
				private void  InitBlock(CountDownLatch latch, CheckRequest enclosingInstance)
				{
					this.latch = latch;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable latch was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private CountDownLatch latch;
				private CheckRequest enclosingInstance;
				public CheckRequest Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					latch.countDown();
				}
			}
			private void  InitBlock(BugDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BugDialog enclosingInstance;
			public BugDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< Boolean, Void >
			private System.Timers.Timer timer = null;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			protected internal virtual System.Boolean doInBackground()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'latch '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				CountDownLatch latch = new CountDownLatch(1);
				
				// Wait 3 seconds before counting down the latch to ensure
				// that the user has sufficient time to read the message on
				// the first pane.
				timer = new System.Timers.Timer();
				timer.Elapsed += new System.Timers.ElapsedEventHandler(new AnonymousClassActionListener(latch, this).actionPerformed);
				timer.Interval = 2000;
				timer.Start();
				
				// Make the request to the server and wait for the latch.
				//UPGRADE_NOTE: Final was removed from the declaration of 'running '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				VassalVersion running = new VassalVersion(Info.Version);
				//UPGRADE_NOTE: Final was removed from the declaration of 'cur '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Boolean cur = VersionUtils.isCurrent(running);
				latch.await();
				return cur;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			protected internal virtual void  done()
			{
				try
				{
					if (get_Renamed(10, TimeUnit.SECONDS))
						Enclosing_Instance.showCurrentVersionPanel();
					//          else       showCurrentVersionPanel();
					else
						Enclosing_Instance.showOldVersionPanel();
					//          else       showConnectionFailedPanel();
				}
				catch (CancellationException e)
				{
					// cancelled by user, do nothing
					timer.Stop();
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
					timer.Stop();
					SupportClass.WriteStackTrace(e, Console.Error);
					Enclosing_Instance.showConnectionFailedPanel();
				}
				catch (ExecutionException e)
				{
					timer.Stop();
					e.printStackTrace();
					Enclosing_Instance.showConnectionFailedPanel();
				}
				catch (TimeoutException e)
				{
					timer.Stop();
					e.printStackTrace();
					Enclosing_Instance.showConnectionFailedPanel();
				}
			}
		}
		
		private System.Windows.Forms.Control buildSendingBugReportPanel()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'spinner '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			JXBusyLabel spinner = new JXBusyLabel(new System.Drawing.Size(40, 40));
			spinner.setBusy(true);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'label '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FlowLabel label = new FlowLabel(Resources.getString("BugDialog.sending_bug_report"));
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new JPanel(new MigLayout("", "", "[]push[]push"));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(label);
			label.Dock = new System.Windows.Forms.DockStyle();
			label.BringToFront();
			panel.add(spinner, "cell 0 1, align center");
			
			return panel;
		}
		
		private System.Windows.Forms.Control buildSendingBugReportButtons()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'cancelButton '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Constructor 'javax.swing.JButton.JButton' was converted to 'System.Windows.Forms.Button.Button' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJButtonJButton_javaxswingAction'"
			System.Windows.Forms.Button cancelButton = new System.Windows.Forms.Button();
			SupportClass.ActionSupport tmp_action7 = new AnonymousClassAbstractAction6(this, Resources.getString(Resources.CANCEL));
			cancelButton.Click += new System.EventHandler(tmp_action7.actionPerformed);
			cancelButton.Image = tmp_action7.Icon;
			cancelButton.Text = tmp_action7.Description;
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'panel '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel panel = new JPanel(new MigLayout("align right"));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			panel.Controls.Add(cancelButton);
			cancelButton.Dock = new System.Windows.Forms.DockStyle();
			cancelButton.BringToFront();
			
			return panel;
		}
		
		private SendRequest sendRequest = null;
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SendRequest' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_NOTE: Inner class 'SendRequest' is now serializable, and this may become a security issue. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1234'"
		[Serializable]
		private class SendRequest:SwingWorker
		{
			public SendRequest(BugDialog enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassActionListener
			{
				public AnonymousClassActionListener(CountDownLatch latch, SendRequest enclosingInstance)
				{
					InitBlock(latch, enclosingInstance);
				}
				private void  InitBlock(CountDownLatch latch, SendRequest enclosingInstance)
				{
					this.latch = latch;
					this.enclosingInstance = enclosingInstance;
				}
				//UPGRADE_NOTE: Final variable latch was copied into class AnonymousClassActionListener. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
				private CountDownLatch latch;
				private SendRequest enclosingInstance;
				public SendRequest Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
				{
					latch.countDown();
				}
			}
			private void  InitBlock(BugDialog enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BugDialog enclosingInstance;
			public BugDialog Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			< Void, Void >
			private System.Timers.Timer timer = null;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			protected internal virtual System.Void doInBackground()
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'latch '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				CountDownLatch latch = new CountDownLatch(1);
				
				// Wait 3 seconds before counting down the latch to ensure
				// that the user has sufficient time to read the message on
				// the first pane.
				timer = new System.Timers.Timer();
				timer.Elapsed += new System.Timers.ElapsedEventHandler(new AnonymousClassActionListener(latch, this).actionPerformed);
				timer.Interval = 2000;
				timer.Start();
				
				// Make the request to the server and wait for the latch.
				BugUtils.sendBugReport(Enclosing_Instance.emailField.Text, Enclosing_Instance.descriptionArea.Text, Enclosing_Instance.errorLog, Enclosing_Instance.thrown);
				
				latch.await();
				//UPGRADE_TODO: The 'System.Void' structure does not have an equivalent to NULL. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1291'"
				return null;
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Override
			protected internal virtual void  done()
			{
				try
				{
					get_Renamed(10, TimeUnit.SECONDS);
					Enclosing_Instance.showEmergencySavePanel();
				}
				catch (CancellationException e)
				{
					// cancelled by user, do nothing
					timer.Stop();
				}
				catch (System.Threading.ThreadInterruptedException e)
				{
					timer.Stop();
					SupportClass.WriteStackTrace(e, Console.Error);
					Enclosing_Instance.showConnectionFailedPanel();
				}
				catch (ExecutionException e)
				{
					timer.Stop();
					e.printStackTrace();
					Enclosing_Instance.showConnectionFailedPanel();
				}
				catch (TimeoutException e)
				{
					timer.Stop();
					e.printStackTrace();
					Enclosing_Instance.showConnectionFailedPanel();
				}
			}
		}
		
		// FIXME: add a page thanking the user for his bug report and providing
		// a link to it at SF.
		
		private void  emergencySave()
		{
			// FIXME: GameModule and GameState need save methods which take a filename
			/*
			final GameModule mod = GameModule.getGameModule();
			if (mod != null) mod.save(false);
			
			final GameState state = mod.getGameState();
			if (state != null && state.isModified()) {
			state.saveGame();
			}*/
		}
		
		[STAThread]
		public static void  Main(System.String[] args)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
			SwingUtilities.invokeLater(new AnonymousClassRunnable());
		}
		private void  BugDialog_Closing_DISPOSE_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 2);
		}
	}
}