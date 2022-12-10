/*
* $Id$
*
* Copyright (c) 2008-2009 Brent Easton
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
//UPGRADE_TODO: The type 'net.miginfocom.swing.MigLayout' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using MigLayout = net.miginfocom.swing.MigLayout;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using BeanShellExpressionConfigurer = VassalSharp.configure.BeanShellExpressionConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using EditablePiece = VassalSharp.counters.EditablePiece;
using BrowserSupport = VassalSharp.tools.BrowserSupport;
using ButtonFactory = VassalSharp.tools.ButtonFactory;
namespace VassalSharp.script.expression
{
	
	[Serializable]
	public class FunctionBuilder:System.Windows.Forms.Form
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(FunctionBuilder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(FunctionBuilder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private FunctionBuilder enclosingInstance;
			public FunctionBuilder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.save();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(FunctionBuilder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(FunctionBuilder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private FunctionBuilder enclosingInstance;
			public FunctionBuilder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.cancel();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(FunctionBuilder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(FunctionBuilder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private FunctionBuilder enclosingInstance;
			public FunctionBuilder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				BrowserSupport.openURL(HelpFile.getReferenceManualPage("ExpressionBuilder.htm").getContents().toString());
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(FunctionBuilder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(FunctionBuilder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private FunctionBuilder enclosingInstance;
			public FunctionBuilder Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs we)
			{
				we.Cancel = true;
				Enclosing_Instance.cancel();
			}
		}
		
		private const long serialVersionUID = 1L;
		protected internal System.String save_Renamed_Field;
		protected internal StringConfigurer target;
		protected internal System.String function;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < BeanShellExpressionConfigurer > configs = new ArrayList < BeanShellExpressionConfigurer >();
		protected internal EditablePiece targetPiece;
		
		public FunctionBuilder(StringConfigurer c, System.Windows.Forms.Form parent, System.String function, System.String desc, System.String[] parmDesc, EditablePiece piece):base()
		{
			//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtDialog_javalangString_boolean'"
			SupportClass.DialogSupport.SetDialog(this, parent, "Function Builder - " + function);
			target = c;
			targetPiece = piece;
			save_Renamed_Field = target.ValueString;
			this.function = function;
			setLayout(new MigLayout("fillx,ins 0"));
			
			System.Windows.Forms.Panel p = new JPanel(new MigLayout("wrap 1,fillx"));
			
			System.Windows.Forms.Label temp_label2;
			temp_label2 = new System.Windows.Forms.Label();
			temp_label2.Text = desc;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = temp_label2;
			p.Controls.Add(temp_Control);
			temp_Control.Dock = new System.Windows.Forms.DockStyle();
			temp_Control.BringToFront();
			for (int i = 0; i < parmDesc.Length; i++)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'config '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				BeanShellExpressionConfigurer config = new BeanShellExpressionConfigurer(null, parmDesc[i] + ":  ", "", targetPiece);
				configs.add(config);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				p.Controls.Add(config.Controls);
				config.Controls.Dock = new System.Windows.Forms.DockStyle();
				config.Controls.BringToFront();
			}
			
			System.Windows.Forms.Panel buttonBox = new JPanel(new MigLayout("", "[]rel[]rel[]"));
			System.Windows.Forms.Button okButton = ButtonFactory.OkButton;
			okButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(okButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonBox.Controls.Add(okButton);
			
			System.Windows.Forms.Button cancelButton = ButtonFactory.CancelButton;
			cancelButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(cancelButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonBox.Controls.Add(cancelButton);
			
			System.Windows.Forms.Button helpButton = ButtonFactory.getHelpButton();
			helpButton.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(helpButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonBox.Controls.Add(helpButton);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			p.Controls.Add(buttonBox);
			buttonBox.Dock = new System.Windows.Forms.DockStyle();
			buttonBox.BringToFront();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(p);
			p.Dock = new System.Windows.Forms.DockStyle();
			p.BringToFront();
			
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
			System.Windows.Forms.Control generatedAux10 = Parent;
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Closing += new System.ComponentModel.CancelEventHandler(this.FunctionBuilder_Closing_DO_NOTHING_ON_CLOSE);
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
		}
		
		/// <summary> Ok button pressed. Set the expression back into the target configurer.</summary>
		public virtual void  save()
		{
			System.String result = function + "(";
			bool first = true;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(BeanShellExpressionConfigurer fec: configs)
			{
				if (!first)
				{
					result += ",";
				}
				result += fec.getValueString();
				first = false;
			}
			result += ")";
			target.setValue(result);
			Dispose();
		}
		
		public virtual void  cancel()
		{
			Dispose();
		}
		private void  FunctionBuilder_Closing_DO_NOTHING_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 0);
		}
	}
}