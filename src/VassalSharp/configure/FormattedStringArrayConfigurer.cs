/*
* $Id: FormattedStringArrayConfigurer.java 7861 2011-10-01 06:23:11Z swampwallaby $
*
* Copyright (c) 2011-2012 by Brent Easton
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
using GamePiece = VassalSharp.counters.GamePiece;
namespace VassalSharp.configure
{
	
	public class FormattedStringArrayConfigurer:StringArrayConfigurer
	{
		override protected internal System.Windows.Forms.Control TextComponent
		{
			get
			{
				if (config == null)
				{
					config = new FormattedExpressionConfigurer(null, "", "", target);
				}
				return config.Controls;
			}
			
		}
		override protected internal System.String TextValue
		{
			get
			{
				return config.ValueString;
			}
			
			set
			{
				config.setValue(value);
			}
			
		}
		
		protected internal FormattedExpressionConfigurer config;
		protected internal GamePiece target;
		
		public FormattedStringArrayConfigurer(System.String key, System.String name, System.Object val):base(key, name, val)
		{
		}
		
		public FormattedStringArrayConfigurer(System.String key, System.String name):base(key, name)
		{
		}
		
		public FormattedStringArrayConfigurer(System.String key, System.String name, PropertyChangerConfigurer.Constraints c):base(key, name)
		{
			if (c is GamePiece)
			{
				target = (GamePiece) c;
			}
		}
		
		public FormattedStringArrayConfigurer(System.String key, System.String name, GamePiece target):this(key, name)
		{
			this.target = target;
		}
		
		//UPGRADE_TODO: Interface 'java.awt.event.ActionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		protected internal override void  addTextActionListener(ActionListener a)
		{
			return ;
		}
	}
}