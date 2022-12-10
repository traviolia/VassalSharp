/*
* $Id$
*
* Copyright (c) 2008-2009 by Brent Easton
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
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using BeanShellExpressionConfigurer = VassalSharp.configure.BeanShellExpressionConfigurer;
using Configurer = VassalSharp.configure.Configurer;
using EditablePiece = VassalSharp.counters.EditablePiece;
using BrowserSupport = VassalSharp.tools.BrowserSupport;
using ButtonFactory = VassalSharp.tools.ButtonFactory;
namespace VassalSharp.script.expression
{
	
	/// <summary> Interactively build inline(beanshell) expressions</summary>
	[Serializable]
	public class ExpressionBuilder:System.Windows.Forms.Form
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(ExpressionBuilder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ExpressionBuilder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ExpressionBuilder enclosingInstance;
			public ExpressionBuilder Enclosing_Instance
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
			public AnonymousClassActionListener1(ExpressionBuilder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ExpressionBuilder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ExpressionBuilder enclosingInstance;
			public ExpressionBuilder Enclosing_Instance
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
			public AnonymousClassActionListener2(ExpressionBuilder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ExpressionBuilder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ExpressionBuilder enclosingInstance;
			public ExpressionBuilder Enclosing_Instance
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
			public AnonymousClassWindowAdapter(ExpressionBuilder enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ExpressionBuilder enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ExpressionBuilder enclosingInstance;
			public ExpressionBuilder Enclosing_Instance
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
		virtual public System.String Expression
		{
			get
			{
				return expression == null?"":expression.ValueString;
			}
			
			set
			{
				if (expression == null)
				{
					System.String prompt = target.getName().Length == 0?"Expression:  ":target.getName();
					expression = new BeanShellExpressionConfigurer(null, prompt, value, pieceTarget);
				}
				expression.setValue(value);
			}
			
		}
		
		private const long serialVersionUID = 1L;
		protected internal BeanShellExpressionConfigurer expression;
		protected internal System.String save_Renamed_Field;
		protected internal Configurer target;
		protected internal EditablePiece pieceTarget;
		
		public ExpressionBuilder(Configurer c, System.Windows.Forms.Form parent):this(c, parent, null)
		{
		}
		
		public ExpressionBuilder(Configurer c, System.Windows.Forms.Form parent, EditablePiece piece):base()
		{
			//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.SetDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtDialog_javalangString_boolean'"
			SupportClass.DialogSupport.SetDialog(this, parent, "Expression Builder");
			target = c;
			pieceTarget = piece;
			save_Renamed_Field = target.ValueString;
			System.Windows.Forms.Panel p = new JPanel(new MigLayout("wrap 1,fill"));
			
			System.String value_Renamed = target.ValueString;
			
			if (value_Renamed.StartsWith("{") && value_Renamed.EndsWith("}"))
			{
				Expression = value_Renamed.Substring(1, (value_Renamed.Length - 1) - (1));
			}
			else
			{
				Expression = convert(value_Renamed);
			}
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			p.Controls.Add(expression.Controls);
			expression.Controls.Dock = new System.Windows.Forms.DockStyle();
			expression.Controls.BringToFront();
			
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
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(p);
			
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
			System.Windows.Forms.Control generatedAux10 = Parent;
			//UPGRADE_TODO: Method 'javax.swing.JDialog.setLocationRelativeTo' was converted to 'System.Windows.Forms.FormStartPosition.CenterParent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogsetLocationRelativeTo_javaawtComponent'"
			StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			Closing += new System.ComponentModel.CancelEventHandler(this.ExpressionBuilder_Closing_DO_NOTHING_ON_CLOSE);
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
		}
		
		/// <summary> OK button pressed. Set the expression back into the target configurer
		/// as an inline expression.
		/// </summary>
		public virtual void  save()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'expr '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String expr = expression.ValueString.Trim();
			if (expr.StartsWith("{") && expr.EndsWith("}"))
			{
				target.setValue(expr);
			}
			else
			{
				target.setValue("{" + expr + "}");
			}
			Dispose();
		}
		
		public virtual void  cancel()
		{
			Dispose();
		}
		
		/// <summary> Convert an old-style $variable$ string to a BeanShell Expression</summary>
		/// <param name="s">Old-style string
		/// </param>
		/// <returns> expression
		/// </returns>
		public virtual System.String convert(System.String s)
		{
			return Expression.createExpression(s).toBeanShellString();
		}
		private void  ExpressionBuilder_Closing_DO_NOTHING_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 0);
		}
	}
}