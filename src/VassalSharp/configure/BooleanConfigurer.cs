/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
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
namespace VassalSharp.configure
{
	
	/// <summary> Configurer for Boolean values</summary>
	public class BooleanConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassItemListener
		{
			public AnonymousClassItemListener(BooleanConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BooleanConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BooleanConfigurer enclosingInstance;
			public BooleanConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs e)
			{
				if (event_sender is System.Windows.Forms.MenuItem)
					((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
				Enclosing_Instance.setValue(Boolean.valueOf(Enclosing_Instance.box.Checked));
			}
		}
		override public System.String ValueString
		{
			get
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'java.lang.Boolean.ToString' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				return booleanValue().ToString();
			}
			
		}
		virtual public bool ValueBoolean
		{
			get
			{
				return booleanValue();
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (box == null)
				{
					box = SupportClass.CheckBoxSupport.CreateCheckBox(getName());
					box.Checked = booleanValue();
					box.CheckedChanged += new System.EventHandler(new AnonymousClassItemListener(this).itemStateChanged);
				}
				return box;
			}
			
		}
		private System.Windows.Forms.CheckBox box;
		
		//UPGRADE_ISSUE: Parameter of type 'java.lang.Boolean' was migrated to type 'boolean' which is identical to 'boolean'. You will get a compilation error with method overloads. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1205'"
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public BooleanConfigurer(System.String key, System.String name, ref System.Boolean val):base(key, name, val)
		{
		}
		
		public BooleanConfigurer(System.String key, System.String name, bool val):base(key, name, val?true:false)
		{
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public BooleanConfigurer(System.String key, System.String name):this(key, name, ref tempAux)
		{
		}
		
		public override void  setValue(System.Object o)
		{
			base.setValue(o);
			if (box != null && !Boolean.valueOf(box.Checked).equals(o))
			{
				box.Checked = booleanValue();
			}
		}
		
		public override void  setValue(System.String s)
		{
			//UPGRADE_NOTE: Exceptions thrown by the equivalent in .NET of method 'java.lang.Boolean.valueOf' may be different. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1099'"
			setValue((System.Object) System.Boolean.Parse(s));
		}
		
		public override void  setName(System.String s)
		{
			base.setName(s);
			if (box != null)
			{
				box.Text = s;
			}
		}
		
		public virtual System.Boolean booleanValue()
		{
			return (System.Boolean) value_Renamed;
		}
		private static System.Boolean tempAux = false;
	}
}