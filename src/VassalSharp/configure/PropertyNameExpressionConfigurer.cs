/*
* $Id: PropertyNameExpressionConfigurer.java 7725 2011-07-31 18:51:43Z uckelman $
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
using EditablePiece = VassalSharp.counters.EditablePiece;
using GamePiece = VassalSharp.counters.GamePiece;
using PropertyNameExpressionBuilder = VassalSharp.script.expression.PropertyNameExpressionBuilder;
namespace VassalSharp.configure
{
	
	/// <summary> A Configurer for Java Expressions</summary>
	public class PropertyNameExpressionConfigurer:FormattedExpressionConfigurer
	{
		
		
		public PropertyNameExpressionConfigurer(System.String key, System.String name):this(key, name, "")
		{
		}
		
		public PropertyNameExpressionConfigurer(System.String key, System.String name, System.String val):this(key, name, val, null)
		{
		}
		
		public PropertyNameExpressionConfigurer(System.String key, System.String name, PropertyExpression val):this(key, name, val.Expression)
		{
		}
		
		public PropertyNameExpressionConfigurer(System.String key, System.String name, PropertyExpression val, GamePiece piece):this(key, name, val.Expression, piece)
		{
		}
		
		public PropertyNameExpressionConfigurer(System.String key, System.String name, System.String val, GamePiece piece):base(key, name, val, piece)
		{
		}
		
		protected internal override ExpressionButton buildButton()
		{
			return new PropertyNameExpressionButton(this, nameField.Size.Height, pieceTarget);
		}
		
		[Serializable]
		public class PropertyNameExpressionButton:ExpressionButton
		{
			private const long serialVersionUID = 1L;
			
			public PropertyNameExpressionButton(Configurer config, int size, EditablePiece piece):base(config, size, piece)
			{
			}
			
			public override void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
				new PropertyNameExpressionBuilder(config, (System.Windows.Forms.Form) getTopLevelAncestor(), piece).Visible = true;
			}
		}
	}
}