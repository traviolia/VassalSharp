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
using Configurable = VassalSharp.build.Configurable;
namespace VassalSharp.configure
{
	
	/// <summary> A {@link Configurer} for configuring {@link Configurable} components
	/// (Is that as redundant as it sounds?)
	/// The invoking class must provide an array of Configurers,
	/// one for each attribute of the target Configurable object.
	/// It is usually easier for the target to implement AutoConfigurable
	/// and use the AutoConfigurer class.
	/// </summary>
	public class SimpleConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(SimpleConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(SimpleConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SimpleConfigurer enclosingInstance;
			public SimpleConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				if (VassalSharp.build.Configurable_Fields.NAME_PROPERTY.Equals(evt.PropertyName))
				{
					Enclosing_Instance.setName((System.String) evt.NewValue);
				}
			}
		}
		override public System.String ValueString
		{
			get
			{
				return target.getConfigureName();
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (p == null)
				{
					p = new System.Windows.Forms.Panel();
					//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					p.setLayout(new BoxLayout(p, BoxLayout.Y_AXIS));
					for (int i = 0; i < attConfig.Length; ++i)
					{
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
						p.Controls.Add(attConfig[i].Controls);
						// attConfig[i].addPropertyChangeListener(this);
					}
				}
				return p;
			}
			
		}
		private System.Windows.Forms.Panel p;
		private Configurer[] attConfig;
		private Configurable target;
		
		public SimpleConfigurer(Configurable c, Configurer[] attConfigurers):base(null, c.getConfigureName())
		{
			
			attConfig = attConfigurers;
			target = c;
			setValue(target);
			target.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
		}
		
		public override void  setValue(System.String s)
		{
			throw new System.NotSupportedException("Can't set Configurable from String");
		}
		
		public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs p1)
		{
			for (int i = 0; i < attConfig.Length; ++i)
			{
				// System.err.println(attConfig[i].getName()+" = "+attConfig[i].getValue());
				if (attConfig[i].getValue() == null)
				{
					setValue((System.Object) null);
					return ;
				}
				setValue(target);
			}
		}
	}
}