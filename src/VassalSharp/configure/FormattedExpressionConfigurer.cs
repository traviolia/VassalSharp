/*
* $Id$
*
* Copyright (c) 2008-2012 by Brent Easton
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
using Constraints = VassalSharp.build.module.properties.PropertyChangerConfigurer.Constraints;
using Decorator = VassalSharp.counters.Decorator;
using EditablePiece = VassalSharp.counters.EditablePiece;
using GamePiece = VassalSharp.counters.GamePiece;
using ExpressionBuilder = VassalSharp.script.expression.ExpressionBuilder;
using FormattedString = VassalSharp.tools.FormattedString;
using IconFactory = VassalSharp.tools.icon.IconFactory;
using IconFamily = VassalSharp.tools.icon.IconFamily;
namespace VassalSharp.configure
{
	
	/// <summary> A standard Formatted String configurer that has an additional
	/// Calculator icon that:
	/// a) Indicates to the user that $name$ variables can be used in this field
	/// b) Clicking on it opens up an Expression Builder that allows entry of
	/// in-line Calculated Properties (Not implemented yet)
	/// </summary>
	public class FormattedExpressionConfigurer:FormattedStringConfigurer
	{
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Windows.Forms.Panel p = (System.Windows.Forms.Panel) base.Controls;
				if (button == null)
				{
					button = buildButton();
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(button);
				}
				button.setSize(nameField.Size.Height);
				return p;
			}
			
		}
		protected internal ExpressionButton button;
		protected internal EditablePiece pieceTarget;
		
		public FormattedExpressionConfigurer(System.String key, System.String name):base(key, name)
		{
		}
		
		public FormattedExpressionConfigurer(System.String key, System.String name, System.String s):base(key, name)
		{
			setValue(s);
		}
		
		public FormattedExpressionConfigurer(System.String key, System.String name, FormattedString s):this(key, name, s.Format)
		{
		}
		
		public FormattedExpressionConfigurer(System.String key, System.String name, System.String s, EditablePiece p):this(key, name, s, (GamePiece) p)
		{
		}
		
		public FormattedExpressionConfigurer(System.String key, System.String name, System.String s, PropertyChangerConfigurer.Constraints p):this(key, name, s)
		{
			if (p is GamePiece)
			{
				storePiece((GamePiece) p);
			}
		}
		
		public FormattedExpressionConfigurer(System.String key, System.String name, System.String s, GamePiece p):this(key, name, s)
		{
			storePiece(p);
		}
		
		protected internal virtual void  storePiece(GamePiece p)
		{
			if (p is Decorator)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'gp '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				GamePiece gp = Decorator.getOutermost(p);
				if (gp is EditablePiece)
				{
					pieceTarget = (EditablePiece) gp;
				}
			}
		}
		
		public FormattedExpressionConfigurer(System.String key, System.String name, System.String[] options):base(key, name, options)
		{
		}
		
		protected internal virtual ExpressionButton buildButton()
		{
			return new ExpressionButton(this, nameField.Size.Height, pieceTarget);
		}
		
		/// <summary> A small 'Calculator' button added after the text to indicate this
		/// Configurer accepts Expressions. Clicking on the button will open
		/// an ExpressionConfigurer.
		/// 
		/// </summary>
		[Serializable]
		public class ExpressionButton:System.Windows.Forms.Button
		{
			private const long serialVersionUID = 1L;
			protected internal Configurer config;
			protected internal EditablePiece piece;
			
			public ExpressionButton(Configurer config, int size):this(config, size, null)
			{
			}
			
			public ExpressionButton(Configurer config, int size, EditablePiece piece)
			{
				this.config = config;
				this.piece = piece;
				Image = IconFactory.getIcon("calculator", IconFamily.XSMALL);
				setSize(size);
				SupportClass.ToolTipSupport.setToolTipText(this, "Expression Builder");
				Click += new System.EventHandler(this.actionPerformed);
				SupportClass.CommandManager.CheckCommand(this);
			}
			
			public virtual void  setSize(int size)
			{
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				Size = new System.Drawing.Size(size, size);
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMaximumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMaximumSize_javaawtDimension'"
				setMaximumSize(new System.Drawing.Size(size, size));
			}
			
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
				new ExpressionBuilder(config, (System.Windows.Forms.Form) getTopLevelAncestor(), piece).Visible = true;
			}
		}
	}
}