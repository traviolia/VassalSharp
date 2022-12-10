/*
* $Id$
*
* Copyright (c) 2000-2008 by Rodney Kinney
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
using Command = VassalSharp.command.Command;
namespace VassalSharp.build.module.properties
{
	
	/// <summary> Command to change the value of a {@link MutableProperty}
	/// 
	/// </summary>
	/// <author>  rodneykinney
	/// 
	/// </author>
	public class ChangePropertyCommand : Command
	{
		virtual public MutableProperty Property
		{
			get
			{
				return property;
			}
			
		}
		virtual public System.String PropertyName
		{
			get
			{
				return propertyName;
			}
			
		}
		virtual public System.String NewValue
		{
			get
			{
				return newValue;
			}
			
		}
		virtual public System.String OldValue
		{
			get
			{
				return oldValue;
			}
			
		}
		private MutableProperty property;
		private System.String propertyName;
		private System.String newValue;
		private System.String oldValue;
		
		public ChangePropertyCommand(MutableProperty property, System.String propertyName, System.String oldValue, System.String newValue):base()
		{
			this.property = property;
			this.propertyName = propertyName;
			this.newValue = newValue;
			this.oldValue = oldValue;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'executeCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override void  executeCommand()
		{
			property.setPropertyValue(newValue);
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myUndoCommand' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override Command myUndoCommand()
		{
			return new ChangePropertyCommand(property, propertyName, newValue, oldValue);
		}
	}
}