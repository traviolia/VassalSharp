/*
* $Id$
*
* Copyright (c) 2000-2006 by Rodney Kinney
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
using PieceAccess = VassalSharp.counters.PieceAccess;
using PlayerAccess = VassalSharp.counters.PlayerAccess;
using SideAccess = VassalSharp.counters.SideAccess;
using SpecifiedSideAccess = VassalSharp.counters.SpecifiedSideAccess;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.configure
{
	
	public class PieceAccessConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassItemListener
		{
			public AnonymousClassItemListener(PieceAccessConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceAccessConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceAccessConfigurer enclosingInstance;
			public PieceAccessConfigurer Enclosing_Instance
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
				Enclosing_Instance.updateValue();
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(Enclosing_Instance.sideConfig.Controls, "Visible", Enclosing_Instance.getValue() is SpecifiedSideAccess);
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
				if (Enclosing_Instance.controls.getTopLevelAncestor() is System.Windows.Forms.Form)
				{
					//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.getTopLevelAncestor' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentgetTopLevelAncestor'"
					((System.Windows.Forms.Form) Enclosing_Instance.controls.getTopLevelAncestor()).pack();
				}
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(PieceAccessConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(PieceAccessConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private PieceAccessConfigurer enclosingInstance;
			public PieceAccessConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
			{
				Enclosing_Instance.updateValue();
			}
		}
		override public System.String ValueString
		{
			get
			{
				return encode(PieceAccess);
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (controls == null)
				{
					controls = new System.Windows.Forms.Panel();
					//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					controls.setLayout(new BoxLayout(controls, BoxLayout.Y_AXIS));
					//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					Box box = Box.createHorizontalBox();
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = getName();
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					box.Controls.Add(temp_Control);
					selectType = SupportClass.ComboBoxSupport.CreateComboBox(Prompts);
					selectType.SelectedValueChanged += new System.EventHandler(new AnonymousClassItemListener(this).itemStateChanged);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					box.Controls.Add(selectType);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(box);
					sideConfig = new StringArrayConfigurer(null, null);
					sideConfig.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(sideConfig.Controls);
					updateControls();
				}
				return controls;
			}
			
		}
		virtual public System.String[] Prompts
		{
			get
			{
				return prompts;
			}
			
			set
			{
				this.prompts = value;
				if (selectType == null)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JComboBox.setModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComboBoxsetModel_javaxswingComboBoxModel'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.DefaultComboBoxModel.DefaultComboBoxModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingDefaultComboBoxModelDefaultComboBoxModel_javalangObject[]'"
					selectType.setModel(new DefaultComboBoxModel(value));
				}
			}
			
		}
		virtual public PieceAccess PieceAccess
		{
			get
			{
				return (PieceAccess) getValue();
			}
			
		}
		protected internal static System.String PLAYER = "player:";
		protected internal static System.String SIDE = "side:";
		protected internal static System.String SIDES = "sides:";
		protected internal System.Windows.Forms.Panel controls;
		protected internal System.String[] prompts = new System.String[]{"Any player", "Any side", "Any of the specified sides"};
		protected internal System.Windows.Forms.ComboBox selectType;
		protected internal StringArrayConfigurer sideConfig;
		
		public PieceAccessConfigurer(System.String key, System.String name, PieceAccess value_Renamed):base(key, name, value_Renamed)
		{
		}
		
		public override void  setValue(System.Object o)
		{
			base.setValue(o);
			updateControls();
		}
		
		public override void  setValue(System.String s)
		{
			setValue(decode(s));
		}
		
		protected internal virtual void  updateValue()
		{
			noUpdate = true;
			if (prompts[1].Equals(selectType.SelectedItem))
			{
				setValue(SideAccess.Instance);
			}
			else if (prompts[2].Equals(selectType.SelectedItem))
			{
				//UPGRADE_TODO: Method 'java.util.Arrays.asList' was converted to 'System.Collections.ArrayList' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javautilArraysasList_javalangObject[]'"
				setValue(new SpecifiedSideAccess(new System.Collections.ArrayList(sideConfig.StringArray)));
			}
			else
			{
				setValue(PlayerAccess.Instance);
			}
			noUpdate = false;
		}
		
		protected internal virtual void  updateControls()
		{
			if (!noUpdate && controls != null)
			{
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(sideConfig.Controls, "Visible", getValue() is SpecifiedSideAccess);
				if (getValue() is SideAccess)
				{
					selectType.SelectedIndex = 1;
				}
				else if (getValue() is SpecifiedSideAccess)
				{
					sideConfig.setValue(((SpecifiedSideAccess) PieceAccess).getSides().toArray(new System.String[0]));
					selectType.SelectedIndex = 2;
				}
				else
				{
					selectType.SelectedIndex = 0;
				}
			}
		}
		
		public static PieceAccess decode(System.String s)
		{
			if (SIDE.Equals(s))
			{
				return SideAccess.Instance;
			}
			else if (s != null && s.StartsWith(SIDES))
			{
				SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s.Substring(SIDES.Length), ':');
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				ArrayList < String > l = new ArrayList < String >();
				while (sd.hasMoreTokens())
				{
					l.add(sd.nextToken());
				}
				return new SpecifiedSideAccess(l);
			}
			else
			{
				return PlayerAccess.Instance;
			}
		}
		
		public static System.String encode(PieceAccess p)
		{
			System.String s = null;
			if (p is SideAccess)
			{
				s = SIDE;
			}
			else if (p is SpecifiedSideAccess)
			{
				SequenceEncoder se = new SequenceEncoder(':');
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(String side:((SpecifiedSideAccess) p).getSides()) se.append(side);
				s = se.Value == null?SIDES:SIDES + se.Value;
			}
			else if (p is PlayerAccess)
			{
				s = PLAYER;
			}
			return s;
		}
	}
}