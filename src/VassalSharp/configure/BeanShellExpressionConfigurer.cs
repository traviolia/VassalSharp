/*
* $Id$
*
* Copyright (c) 2007-2012 by Brent Easton
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
using AbstractBuildable = VassalSharp.build.AbstractBuildable;
using AbstractConfigurable = VassalSharp.build.AbstractConfigurable;
using Buildable = VassalSharp.build.Buildable;
using GameModule = VassalSharp.build.GameModule;
using GlobalOptions = VassalSharp.build.module.GlobalOptions;
using BoardPicker = VassalSharp.build.module.map.BoardPicker;
using Board = VassalSharp.build.module.map.boardPicker.Board;
using ZonedGrid = VassalSharp.build.module.map.boardPicker.board.ZonedGrid;
using GlobalProperties = VassalSharp.build.module.properties.GlobalProperties;
using PropertyNameSource = VassalSharp.build.module.properties.PropertyNameSource;
using Decorator = VassalSharp.counters.Decorator;
using EditablePiece = VassalSharp.counters.EditablePiece;
using GamePiece = VassalSharp.counters.GamePiece;
using Properties = VassalSharp.counters.Properties;
using FunctionBuilder = VassalSharp.script.expression.FunctionBuilder;
using IntBuilder = VassalSharp.script.expression.IntBuilder;
using StrBuilder = VassalSharp.script.expression.StrBuilder;
using IconFactory = VassalSharp.tools.icon.IconFactory;
using IconFamily = VassalSharp.tools.icon.IconFamily;
using MenuScroller = VassalSharp.tools.menu.MenuScroller;
using BeanShellExpressionValidator = bsh.BeanShellExpressionValidator;
namespace VassalSharp.configure
{
	
	/// <summary> A Configurer for Java Expressions</summary>
	public class BeanShellExpressionConfigurer:StringConfigurer
	{
		static private System.Int32 state414;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(BeanShellExpressionConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BeanShellExpressionConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BeanShellExpressionConfigurer enclosingInstance;
			public BeanShellExpressionConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.doPopup();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassKeyAdapter
		{
			public AnonymousClassKeyAdapter(BeanShellExpressionConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BeanShellExpressionConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BeanShellExpressionConfigurer enclosingInstance;
			public BeanShellExpressionConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs evt)
			{
				Enclosing_Instance.noUpdate = true;
				Enclosing_Instance.setValue(Enclosing_Instance.nameField.Text);
				Enclosing_Instance.validator.Invalidate();
				Enclosing_Instance.noUpdate = false;
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(BeanShellExpressionConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BeanShellExpressionConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BeanShellExpressionConfigurer enclosingInstance;
			public BeanShellExpressionConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.buildInteger();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(BeanShellExpressionConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BeanShellExpressionConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BeanShellExpressionConfigurer enclosingInstance;
			public BeanShellExpressionConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.buildString();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener3
		{
			public AnonymousClassActionListener3(System.String op, System.String desc, System.String[] parms, BeanShellExpressionConfigurer enclosingInstance)
			{
				InitBlock(op, desc, parms, enclosingInstance);
			}
			private void  InitBlock(System.String op, System.String desc, System.String[] parms, BeanShellExpressionConfigurer enclosingInstance)
			{
				this.op = op;
				this.desc = desc;
				this.parms = parms;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable op was copied into class AnonymousClassActionListener3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.String op;
			//UPGRADE_NOTE: Final variable desc was copied into class AnonymousClassActionListener3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.String desc;
			//UPGRADE_NOTE: Final variable parms was copied into class AnonymousClassActionListener3. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.String[] parms;
			private BeanShellExpressionConfigurer enclosingInstance;
			public BeanShellExpressionConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.buildFunction(op, desc, parms);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener4
		{
			public AnonymousClassActionListener4(System.String op, BeanShellExpressionConfigurer enclosingInstance)
			{
				InitBlock(op, enclosingInstance);
			}
			private void  InitBlock(System.String op, BeanShellExpressionConfigurer enclosingInstance)
			{
				this.op = op;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable op was copied into class AnonymousClassActionListener4. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.String op;
			private BeanShellExpressionConfigurer enclosingInstance;
			public BeanShellExpressionConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.insertName(op);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener5' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener5
		{
			public AnonymousClassActionListener5(System.String propName, BeanShellExpressionConfigurer enclosingInstance)
			{
				InitBlock(propName, enclosingInstance);
			}
			private void  InitBlock(System.String propName, BeanShellExpressionConfigurer enclosingInstance)
			{
				this.propName = propName;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable propName was copied into class AnonymousClassActionListener5. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private System.String propName;
			private BeanShellExpressionConfigurer enclosingInstance;
			public BeanShellExpressionConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.insertPropertyName(propName);
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener6' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener6
		{
			public AnonymousClassActionListener6(BeanShellExpressionConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BeanShellExpressionConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BeanShellExpressionConfigurer enclosingInstance;
			public BeanShellExpressionConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.insertPropertyName(((System.Windows.Forms.MenuItem) event_sender).Text);
			}
		}
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state414 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private void  InitBlock()
		{
			errorMessage.setValue(error);
			System.String s = "Vassal Properties:  " + (v == null?"":v.toString());
			variables.Text = s;
			s = "Methods:  " + (m == null?"":m.toString());
			methods.Text = s;
		}
		override public System.String ValueString
		{
			get
			{
				return (System.String) value_Renamed;
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (p == null)
				{
					expressionPanel = new JPanel(new MigLayout("fillx,ins 0", "[][grow][][]"));
					//expressionPanel.setLayout(new BoxLayout(expressionPanel, BoxLayout.X_AXIS));
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = getName();
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					expressionPanel.Controls.Add(temp_Control);
					validator = new Validator(this);
					//UPGRADE_TODO: Constructor 'javax.swing.JTextField.JTextField' was converted to 'System.Windows.Forms.TextBox' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTextFieldJTextField_int'"
					nameField = new System.Windows.Forms.TextBox();
					//nameField.setMaximumSize
					//  (new Dimension(nameField.getMaximumSize().width,
					//                          nameField.getPreferredSize().height));
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					nameField.Text = ValueString;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					expressionPanel.Controls.Add(nameField);
					nameField.Dock = new System.Windows.Forms.DockStyle();
					nameField.BringToFront();
					nameField.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.configure.BeanShellExpressionConfigurer.keyDown);
					nameField.KeyUp += new System.Windows.Forms.KeyEventHandler(new AnonymousClassKeyAdapter(this).keyReleased);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					expressionPanel.Controls.Add(validator);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					expressionPanel.Controls.Add(extraDetails);
					extraDetails.Dock = new System.Windows.Forms.DockStyle();
					extraDetails.BringToFront();
					validator.Invalidate();
					
					detailPanel = new System.Windows.Forms.Panel();
					//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					detailPanel.setLayout(new BoxLayout(detailPanel, BoxLayout.Y_AXIS));
					
					errorMessage = new StringConfigurer(null, "Error Message:  ", "");
					errorMessage.Controls.Enabled = false;
					System.Windows.Forms.Label temp_label3;
					temp_label3 = new System.Windows.Forms.Label();
					temp_label3.Text = "Vassal Properties:  ";
					variables = temp_label3;
					System.Windows.Forms.Label temp_label4;
					temp_label4 = new System.Windows.Forms.Label();
					temp_label4.Text = "Methods:  ";
					methods = temp_label4;
					
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					detailPanel.Controls.Add(errorMessage.Controls);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					detailPanel.Controls.Add(variables);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					detailPanel.Controls.Add(methods);
					detailPanel.Visible = false;
					
					p = new System.Windows.Forms.Panel();
					//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					p.setLayout(new BoxLayout(p, BoxLayout.Y_AXIS));
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(expressionPanel);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(detailPanel);
				}
				return p;
			}
			
		}
		virtual protected internal int MaxScrollItems
		{
			get
			{
				if (maxScrollItems == 0)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'itemSize '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					System.Drawing.Size itemSize = (new System.Windows.Forms.MenuItem("Testing")).Size;
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					//UPGRADE_ISSUE: Method 'java.awt.Toolkit.getDefaultToolkit' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtToolkit'"
					Toolkit.getDefaultToolkit();
					maxScrollItems = (int) (0.8 * System.Windows.Forms.Screen.PrimaryScreen.Bounds.Size.Height / itemSize.Height);
				}
				return maxScrollItems;
			}
			
		}
		
		protected internal static int maxScrollItems = 0;
		protected internal const int MAX_SCROLL_ITEMS = 40;
		
		protected internal System.Windows.Forms.Panel expressionPanel;
		protected internal System.Windows.Forms.Panel detailPanel;
		protected internal Validator validator;
		protected internal System.Windows.Forms.Button extraDetails;
		protected internal System.Drawing.Image up;
		protected internal System.Drawing.Image down;
		protected internal StringConfigurer errorMessage;
		protected internal System.Windows.Forms.Label variables;
		protected internal System.Windows.Forms.Label methods;
		protected internal EditablePiece target;
		
		public BeanShellExpressionConfigurer(System.String key, System.String name):this(key, name, "")
		{
		}
		
		public BeanShellExpressionConfigurer(System.String key, System.String name, System.String val):this(key, name, val, null)
		{
		}
		
		public BeanShellExpressionConfigurer(System.String key, System.String name, System.String val, GamePiece piece):base(key, name, val)
		{
			InitBlock();
			if (piece is EditablePiece)
			{
				target = (EditablePiece) piece;
			}
			else
			{
				target = null;
			}
			strip();
			up = IconFactory.getIcon("go-up", IconFamily.XSMALL);
			down = IconFactory.getIcon("go-down", IconFamily.XSMALL);
			extraDetails = SupportClass.ButtonSupport.CreateStandardButton("Insert");
			extraDetails.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(extraDetails);
		}
		
		protected internal virtual void  strip()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 's '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String s = ValueString.Trim();
			if (s.StartsWith("{") && s.EndsWith("}"))
			{
				setValue(s.Substring(1, (s.Length - 1) - (1)));
			}
		}
		
		public override void  setValue(System.String s)
		{
			if (!noUpdate && nameField != null)
			{
				//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
				nameField.Text = s;
			}
			setValue((System.Object) s);
		}
		
		protected internal virtual void  doPopup()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'popup '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Windows.Forms.ContextMenu popup = createPopup();
			//UPGRADE_TODO: Method 'javax.swing.JPopupMenu.show' was converted to 'System.Windows.Forms.ContextMenu.Show' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPopupMenushow_javaawtComponent_int_int'"
			popup.Show(extraDetails, new System.Drawing.Point(0, 0));
		}
		
		/// <summary> Toggle the display of additional details</summary>
		protected internal virtual void  toggleDetails()
		{
			//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
			extraDetails.Image = detailPanel.Visible?down:up;
			//UPGRADE_TODO: Method 'java.awt.Component.isVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentisVisible'"
			detailPanel.Visible = !detailPanel.Visible;
			repack();
		}
		
		protected internal virtual void  repack()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
			System.Windows.Forms.Form w = (System.Windows.Forms.Form) p.Parent;
			if (w != null)
			{
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				w.pack();
			}
		}
		
		/// <summary> Build a popup menu</summary>
		/// <returns>
		/// </returns>
		//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
		protected internal virtual System.Windows.Forms.ContextMenu createPopup()
		{
			//UPGRADE_ISSUE: Class hierarchy differences between 'javax.swing.JPopupMenu' and 'System.Windows.Forms.ContextMenu' may cause compilation errors. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1186'"
			System.Windows.Forms.ContextMenu popup = new System.Windows.Forms.ContextMenu();
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'constantMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem constantMenu = new System.Windows.Forms.MenuItem("Constant");
			//UPGRADE_NOTE: Final was removed from the declaration of 'integerItem '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem integerItem = new System.Windows.Forms.MenuItem("Number");
			SupportClass.ToolTipSupport.setToolTipText(integerItem, "A number");
			integerItem.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(integerItem);
			constantMenu.MenuItems.Add(integerItem);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'stringItem '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem stringItem = new System.Windows.Forms.MenuItem("String");
			SupportClass.ToolTipSupport.setToolTipText(stringItem, "A character string");
			stringItem.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(stringItem);
			constantMenu.MenuItems.Add(stringItem);
			popup.MenuItems.Add(constantMenu);
			
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'propertyMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem propertyMenu = new System.Windows.Forms.MenuItem("Property");
			
			if (target != null)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'pieceMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.MenuItem pieceMenu = new System.Windows.Forms.MenuItem("Piece Property");
				addProp(pieceMenu, VassalSharp.counters.Properties_Fields.MOVED);
				addProp(pieceMenu, VassalSharp.counters.Properties_Fields.SELECTED);
				addProp(pieceMenu, VassalSharp.counters.Properties_Fields.PIECE_ID);
				addPieceProps(pieceMenu, target);
				propertyMenu.MenuItems.Add(pieceMenu);
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'globalsMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem globalsMenu = new System.Windows.Forms.MenuItem("Global Property");
			buildGlobalMenu(globalsMenu, GameModule.getGameModule(), true);
			propertyMenu.MenuItems.Add(globalsMenu);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'vassalMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem vassalMenu = new System.Windows.Forms.MenuItem("Vassal Property");
			addProp(vassalMenu, GlobalOptions.PLAYER_SIDE);
			addProp(vassalMenu, GlobalOptions.PLAYER_NAME);
			addProp(vassalMenu, GlobalOptions.PLAYER_ID);
			propertyMenu.MenuItems.Add(vassalMenu);
			
			popup.MenuItems.Add(propertyMenu);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'operatorMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem operatorMenu = new System.Windows.Forms.MenuItem("Operator");
			addOperator(operatorMenu, "+", "Add");
			addOperator(operatorMenu, "-", "Subtract");
			addOperator(operatorMenu, "*", "Multiply");
			addOperator(operatorMenu, "/", "Divide");
			addOperator(operatorMenu, "%", "Modulus");
			popup.MenuItems.Add(operatorMenu);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'comparisonMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem comparisonMenu = new System.Windows.Forms.MenuItem("Comparison");
			addOperator(comparisonMenu, "==", "Equals");
			addOperator(comparisonMenu, "!=", "Not equals");
			addOperator(comparisonMenu, ">", "Greater than");
			addOperator(comparisonMenu, ">=", "Greater than or equal to");
			addOperator(comparisonMenu, "<", "Less than");
			addOperator(comparisonMenu, "<=", "Less than or equal to");
			addOperator(comparisonMenu, "=~", "Matches Regular Expression");
			addOperator(comparisonMenu, "!~", "Does not match Regular Expression");
			popup.MenuItems.Add(comparisonMenu);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'logicalMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem logicalMenu = new System.Windows.Forms.MenuItem("Logical");
			addOperator(logicalMenu, "&&", "And");
			addOperator(logicalMenu, "||", "Or");
			addOperator(logicalMenu, "(", "Left parenthesis");
			addOperator(logicalMenu, ")", "Right parenthesis");
			popup.MenuItems.Add(logicalMenu);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'functionMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem functionMenu = new System.Windows.Forms.MenuItem("Function");
			addFunction(functionMenu, "Alert", "Display text in a Dialog box", new System.String[]{"Text to display"});
			addFunction(functionMenu, "Compare", "Compare two Strings or other objects", new System.String[]{"Object 1", "Object 2"});
			addFunction(functionMenu, "GetProperty", "Get a property by name", new System.String[]{"Property name"});
			addFunction(functionMenu, "If", "Return a different result depending on a logical expression", new System.String[]{"Logical expression", "Result if true", "Result if false"});
			addFunction(functionMenu, "SumStack", "Sum the values of the named property in all counters in the same stack", new System.String[]{"Property name"});
			popup.MenuItems.Add(functionMenu);
			
			return popup;
		}
		
		protected internal virtual void  addFunction(System.Windows.Forms.MenuItem menu, System.String op, System.String desc, System.String[] parms)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'item '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem item = new System.Windows.Forms.MenuItem(op);
			SupportClass.ToolTipSupport.setToolTipText(item, desc);
			item.Click += new System.EventHandler(new AnonymousClassActionListener3(op, desc, parms, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(item);
			menu.MenuItems.Add(item);
		}
		
		protected internal virtual void  buildFunction(System.String op, System.String desc, System.String[] parmDesc)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringConfigurer result = new StringConfigurer(null, "", "");
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
			new FunctionBuilder(result, (System.Windows.Forms.Form) p.getTopLevelAncestor(), op, desc, parmDesc, target).Visible = true;
			if (result.getValue() != null && result.ValueString.Length > 0)
			{
				insertName(result.ValueString);
			}
		}
		
		protected internal virtual void  buildInteger()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringConfigurer result = new StringConfigurer(null, "", "");
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
			new IntBuilder(result, (System.Windows.Forms.Form) p.getTopLevelAncestor()).Visible = true;
			if (result.getValue() != null && result.ValueString.Length > 0)
			{
				insertName(result.ValueString);
			}
		}
		
		protected internal virtual void  buildString()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'result '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			StringConfigurer result = new StringConfigurer(null, "", "");
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
			new StrBuilder(result, (System.Windows.Forms.Form) p.getTopLevelAncestor()).Visible = true;
			if (result.getValue() != null && result.ValueString.Length > 0)
			{
				insertName(result.ValueString);
			}
		}
		
		protected internal virtual void  addOperator(System.Windows.Forms.MenuItem menu, System.String op, System.String desc)
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'item '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem item = new System.Windows.Forms.MenuItem(op);
			SupportClass.ToolTipSupport.setToolTipText(item, desc);
			item.Click += new System.EventHandler(new AnonymousClassActionListener4(op, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(item);
			menu.MenuItems.Add(item);
		}
		/// <summary> Add straight property name to a menu</summary>
		/// <param name="menu">parent menu
		/// </param>
		/// <param name="propName">property name to add
		/// </param>
		protected internal virtual void  addProp(System.Windows.Forms.MenuItem menu, System.String propName)
		{
			addProp(menu, propName, false);
		}
		
		protected internal virtual void  addProp(System.Windows.Forms.MenuItem menu, System.String propName, bool sort)
		{
			// Ignore any null propNames
			if (propName == null)
			{
				return ;
			}
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'item '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem item = new System.Windows.Forms.MenuItem(propName);
			item.Click += new System.EventHandler(new AnonymousClassActionListener5(propName, this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(item);
			if (sort)
			{
				int pos = - 1;
				for (int i = 0; i < menu.MenuItems.Count && pos < 0; i++)
				{
					if (String.CompareOrdinal(propName, menu.MenuItems[i].Text) <= 0)
					{
						pos = i;
					}
				}
				//UPGRADE_TODO: Method 'javax.swing.JMenu.add' was converted to 'System.Windows.Forms.MenuItem.MenuItems.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				menu.MenuItems.Add(pos, item);
			}
			else
			{
				menu.MenuItems.Add(item);
			}
		}
		
		/// <summary> Added the property names from an Editable Piece into their
		/// own menu
		/// </summary>
		/// <param name="menu">parent menu
		/// </param>
		/// <param name="piece">Piece containing property names
		/// </param>
		protected internal virtual void  addPieceProps(System.Windows.Forms.MenuItem menu, EditablePiece piece)
		{
			if (piece == null)
			{
				return ;
			}
			
			System.Windows.Forms.MenuItem pieceMenu = null;
			
			if (piece is PropertyNameSource)
			{
				//UPGRADE_NOTE: There is an untranslated Statement.  Please refer to original code. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1153'"
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String propName: propNames)
				{
					if (pieceMenu == null)
					{
						pieceMenu = new System.Windows.Forms.MenuItem();
						pieceMenu.Text = piece.Description;
					}
					//UPGRADE_NOTE: Final was removed from the declaration of 'item '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Windows.Forms.MenuItem item = new JMenuItem(propName);
					item.Click += new System.EventHandler(new AnonymousClassActionListener6(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(item);
					pieceMenu.MenuItems.Add(item);
				}
				
				if (pieceMenu != null)
				{
					menu.MenuItems.Add(pieceMenu);
				}
				
				if (piece is Decorator)
				{
					addPieceProps(menu, (EditablePiece) ((Decorator) piece).getInner());
				}
			}
		}
		
		/// <summary> Create a menu of Global Properties recorded in this module, based on
		/// the module build structure
		/// </summary>
		
		protected internal virtual void  buildGlobalMenu(System.Windows.Forms.MenuItem parentMenu, AbstractBuildable target, bool useParentMenu)
		{
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < Buildable > buildables = target.getBuildables();
			System.String menuName = ConfigureTree.getConfigureName(target.GetType());
			if (target is AbstractConfigurable)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'n '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.String n = ((AbstractConfigurable) target).getConfigureName();
				if (n != null && n.Length > 0)
				{
					menuName += (" " + n);
				}
			}
			//UPGRADE_NOTE: Final was removed from the declaration of 'myMenu '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.MenuItem myMenu = new System.Windows.Forms.MenuItem(menuName);
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final List < String > propNames = target.getPropertyNames();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String propName: propNames)
			{
				addProp(useParentMenu?parentMenu:myMenu, propName, true);
			}
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Buildable b: buildables)
			{
				if (b is AbstractConfigurable)
				{
					// Remove 'filler' menu levels due to intermediate holding components
					//UPGRADE_NOTE: Final was removed from the declaration of 'useParent '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					bool useParent = (b is GlobalProperties || b is Board || b is ZonedGrid);
					buildGlobalMenu(useParentMenu?parentMenu:myMenu, (AbstractConfigurable) b, useParent);
				}
				else if (b is BoardPicker)
				{
					buildGlobalMenu(myMenu, (AbstractBuildable) b, true);
				}
			}
			
			if (!useParentMenu & myMenu.MenuItems.Count > 0)
			{
				MenuScroller.setScrollerFor(myMenu, MaxScrollItems, 100);
				int pos = - 1;
				for (int i = 0; i < parentMenu.MenuItems.Count && pos < 0; i++)
				{
					if (String.CompareOrdinal(myMenu.Text, parentMenu.MenuItems[i].Text) <= 0)
					{
						pos = i;
					}
				}
				//UPGRADE_TODO: Method 'javax.swing.JMenu.add' was converted to 'System.Windows.Forms.MenuItem.MenuItems.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				parentMenu.MenuItems.Add(pos, myMenu);
			}
		}
		
		/// <summary> Insert a property name into the expression</summary>
		/// <param name="name">property name
		/// </param>
		protected internal virtual void  insertPropertyName(System.String name)
		{
			insertName(cleanName(name));
		}
		
		protected internal virtual void  insertName(System.String name)
		{
			System.String work = nameField.Text;
			int pos = nameField.SelectionStart;
			
			// Cut out any selected text
			//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.text.JTextComponent.getSelectedText' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			if (nameField.SelectedText != null)
			{
				int start = nameField.SelectionStart;
				int end = nameField.SelectionStart + nameField.SelectionLength;
				work = work.Substring(0, (start) - (0)) + work.Substring(end);
				if (pos >= start && pos <= end)
				{
					pos = start;
				}
			}
			
			System.String news = work.Substring(0, (pos) - (0)) + name + work.Substring(pos);
			//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
			nameField.Text = news;
			nameField.SelectionStart = pos + name.Length;
			
			// Update the text field and repaint it
			noUpdate = true;
			setValue(nameField.Text);
			validator.Invalidate();
			noUpdate = false;
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			nameField.Refresh();
			
			// Send focus back to text field
			nameField.requestFocusInWindow();
		}
		
		/*
		* If the property name is not a valid java variable name, it
		* needs to be returned using the GetProperty() function.
		*/
		protected internal virtual System.String cleanName(System.String name)
		{
			bool valid = true;
			for (int i = 0; i < name.Length && valid; i++)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				char c = name[i];
				if (i == 0)
				{
					valid = (System.Char.IsLetter(c) || c.CompareTo('$') == 0 || c.CompareTo('_') == 0);
				}
				else
				{
					//UPGRADE_ISSUE: Method 'java.lang.Character.isJavaIdentifierPart' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javalangCharacterisJavaIdentifierPart_char'"
					valid = Character.isJavaIdentifierPart(c);
				}
			}
			return valid?name:"GetProperty(\"" + name + "\")";
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void setDetails(String error, List < String > v, List < String > m)
		
		protected internal virtual void  setDetails()
		{
			setDetails("", null, null);
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'Validator' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/*
		* Class to check and reflect the validity of the current expression.
		*/
		//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
		[Serializable]
		protected internal class Validator:System.Windows.Forms.Label
		{
			private void  InitBlock(BeanShellExpressionConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
				status = UNKNOWN;
				validationThread = new ValidationThread(this);
			}
			private BeanShellExpressionConfigurer enclosingInstance;
			virtual public int Status
			{
				get
				{
					return status;
				}
				
				set
				{
					if (value == VALID)
					{
						Image = tick;
					}
					else if (value == INVALID)
					{
						Image = cross;
					}
					else
					{
						Image = none;
					}
					this.status = value;
				}
				
			}
			public BeanShellExpressionConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			protected internal const int INVALID = 0;
			protected internal const int VALID = 1;
			protected internal const int UNKNOWN = 2;
			
			protected internal System.Drawing.Image tick;
			protected internal System.Drawing.Image cross;
			//UPGRADE_TODO: Class 'javax.swing.ImageIcon' was converted to 'System.Drawing.Image' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingImageIcon'"
			protected internal System.Drawing.Image none;
			//UPGRADE_NOTE: The initialization of  'status' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			protected internal int status;
			protected internal bool validating = false;
			protected internal bool dirty = false;
			//UPGRADE_NOTE: The initialization of  'validationThread' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
			protected internal ValidationThread validationThread;
			
			private const long serialVersionUID = 1L;
			
			public Validator(BeanShellExpressionConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
				cross = IconFactory.getIcon("no", IconFamily.XSMALL);
				tick = IconFactory.getIcon("yes", IconFamily.XSMALL);
				
				System.Drawing.Bitmap image = new System.Drawing.Bitmap(cross.Width, cross.Height, (System.Drawing.Imaging.PixelFormat) System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				none = (System.Drawing.Image) image.Clone();
				
				Status = UNKNOWN;
			}
			
			/*
			*  Run the validation in a separate thread. If the expression is updated
			*  while validating, then revalidate.
			*/
			//UPGRADE_TODO: More than one of the Java class members are converted to this same member in .NET. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1231'"
			//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
			new virtual public  void  Invalidate()
			{
				if (validating)
				{
					dirty = true;
				}
				else
				{
					validating = true;
					Enclosing_Instance.validator.Status = UNKNOWN;
					//UPGRADE_ISSUE: Method 'javax.swing.SwingUtilities.invokeLater' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingSwingUtilities'"
					SwingUtilities.invokeLater(validationThread);
				}
			}
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ValidationThread' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			//UPGRADE_NOTE: Inner class 'ValidationThread' is now serializable, and this may become a security issue. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1234'"
			//UPGRADE_NOTE: The access modifier for this class or class field has been changed in order to prevent compilation errors due to the visibility level. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1296'"
			[Serializable]
			protected internal class ValidationThread : IThreadRunnable
			{
				public ValidationThread(Validator enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(Validator enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private Validator enclosingInstance;
				public Validator Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				
				public virtual void  Run()
				{
					if (Enclosing_Instance.Enclosing_Instance.ValueString.Length == 0)
					{
						Enclosing_Instance.Enclosing_Instance.validator.Status = VassalSharp.configure.BeanShellExpressionConfigurer.Validator.UNKNOWN;
						Enclosing_Instance.Enclosing_Instance.setDetails();
					}
					else
					{
						BeanShellExpressionValidator v = new BeanShellExpressionValidator(Enclosing_Instance.Enclosing_Instance.ValueString);
						if (v.isValid())
						{
							Enclosing_Instance.Enclosing_Instance.validator.Status = VassalSharp.configure.BeanShellExpressionConfigurer.Validator.VALID;
							setDetails(v.getError(), v.getVariables(), v.getMethods());
						}
						else
						{
							Enclosing_Instance.Enclosing_Instance.validator.Status = VassalSharp.configure.BeanShellExpressionConfigurer.Validator.INVALID;
							setDetails(v.getError(), v.getVariables(), v.getMethods());
						}
					}
					Enclosing_Instance.validating = false;
					if (Enclosing_Instance.dirty)
					{
						Enclosing_Instance.dirty = false;
						Enclosing_Instance.Invalidate();
					}
				}
			}
		}
	}
}