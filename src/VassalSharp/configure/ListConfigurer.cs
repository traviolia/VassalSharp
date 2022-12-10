/*
* $Id$
*
* Copyright (c) 2006 by Rodney Kinney
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
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.configure
{
	
	/// <summary> Configures a variable-length list of objects
	/// 
	/// </summary>
	/// <author>  rkinney
	/// 
	/// </author>
	public abstract class ListConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(ListConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ListConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ListConfigurer enclosingInstance;
			public ListConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Configurer c = Enclosing_Instance.buildChildConfigurer();
				getListValue().add(c.getValue());
				Enclosing_Instance.updateControls();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(VassalSharp.configure.Configurer c, ListConfigurer enclosingInstance)
			{
				InitBlock(c, enclosingInstance);
			}
			private void  InitBlock(VassalSharp.configure.Configurer c, ListConfigurer enclosingInstance)
			{
				this.c = c;
				this.enclosingInstance = enclosingInstance;
			}
			//UPGRADE_NOTE: Final variable c was copied into class AnonymousClassActionListener1. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1023'"
			private VassalSharp.configure.Configurer c;
			private ListConfigurer enclosingInstance;
			public ListConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				getListValue().remove(c.getValue());
				Enclosing_Instance.updateControls();
				Enclosing_Instance.repack();
			}
		}
		private void  InitBlock()
		{
			base(key, name, val);
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			(List < Object >) getValue();
		}
		override public System.String ValueString
		{
			get
			{
				if (getListValue().isEmpty())
				{
					return "";
				}
				Configurer c = buildChildConfigurer();
				SequenceEncoder se = new SequenceEncoder(',');
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Object value: getListValue())
				{
					c.setValue(value_Renamed);
					se.append(c.ValueString);
				}
				return se.Value;
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (panel == null)
				{
					//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
					new BorderLayout();
					//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
					panel = new System.Windows.Forms.Panel();
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					controls = Box.createVerticalBox();
					//UPGRADE_NOTE: Final was removed from the declaration of 'scroll '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
					System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
					temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
					temp_scrollablecontrol.AutoScroll = true;
					temp_scrollablecontrol.Controls.Add(controls);
					System.Windows.Forms.ScrollableControl scroll = temp_scrollablecontrol;
					//UPGRADE_ISSUE: Method 'javax.swing.BorderFactory.createTitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBorderFactorycreateTitledBorder_javalangString'"
					controls.setBorder(BorderFactory.createTitledBorder(getName()));
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					configControls = Box.createVerticalBox();
					
					System.Windows.Forms.Button addButton = SupportClass.ButtonSupport.CreateStandardButton("New");
					addButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(addButton);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(addButton);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					controls.Controls.Add(configControls);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
					panel.Controls.Add(scroll);
					scroll.Dock = System.Windows.Forms.DockStyle.Fill;
					scroll.BringToFront();
					updateControls();
				}
				return panel;
			}
			
		}
		//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
		protected internal Box controls;
		//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
		protected internal Box configControls;
		protected internal System.Windows.Forms.Panel panel;
		protected internal System.Drawing.Size fixedSize;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		protected List < Configurer > configurers = new ArrayList < Configurer >();
		
		public ListConfigurer(System.String key, System.String name)
		{
			InitBlock();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			super(key, name, new ArrayList < Object >());
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public ListConfigurer(String key, String name, List < ? > val)
		
		public override void  setValue(System.String s)
		{
			getListValue().clear();
			if (s.Length > 0)
			{
				Configurer c = buildChildConfigurer();
				SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, ',');
				while (sd.hasMoreTokens())
				{
					c.setValue(sd.nextToken());
					getListValue().add(c.getValue());
				}
			}
			updateControls();
		}
		
		protected internal virtual void  updateValue()
		{
			noUpdate = true;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < Object > newArray = new ArrayList < Object >();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(Configurer c: configurers)
			{
				newArray.add(c.getValue());
			}
			setValue(newArray);
			noUpdate = false;
		}
		
		public override void  setValue(System.Object o)
		{
			if (o == null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				o = new ArrayList < Object >();
			}
			base.setValue(o);
			if (!noUpdate)
			{
				updateControls();
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < Object > getListValue()
		
		/// <summary> The objects in the list are specified by the Configurer returned here
		/// 
		/// </summary>
		/// <returns>
		/// </returns>
		protected internal abstract Configurer buildChildConfigurer();
		
		public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs evt)
		{
			updateValue();
		}
		
		protected internal virtual void  updateControls()
		{
			if (controls != null)
			{
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Configurer c: configurers)
				{
					c.removePropertyChangeListener(this);
				}
				configurers.clear();
				configControls.Controls.Clear();
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				for(Object value: getListValue())
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Configurer c = buildChildConfigurer();
					c.setValue(value_Renamed);
					c.PropertyChange += new SupportClass.PropertyChangeEventHandler(this.propertyChange);
					configurers.add(c);
					//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					Box b = Box.createHorizontalBox();
					System.Windows.Forms.Button delButton = SupportClass.ButtonSupport.CreateStandardButton("Remove");
					delButton.Click += new System.EventHandler(new AnonymousClassActionListener1(c, this).actionPerformed);
					SupportClass.CommandManager.CheckCommand(delButton);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					b.Controls.Add(delButton);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					b.Controls.Add(c.Controls);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					configControls.Controls.Add(b);
					if (configurers.size() > 5)
					{
						if (fixedSize.IsEmpty)
						{
							//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
							fixedSize = new System.Drawing.Size(((panel is VassalSharp.tools.ComponentSplitter.SplitPane || panel is VassalSharp.counters.Obscurable.Ed.AnonymousClassJPanel || panel is VassalSharp.counters.NonRectangular.Ed.AnonymousClassJPanel || panel is VassalSharp.build.widget.PieceSlot.Panel || panel is VassalSharp.build.module.map.View || panel is VassalSharp.build.module.map.boardPicker.board.Config.View || panel is VassalSharp.build.module.map.boardPicker.board.mapgrid.SquareGridNumbering.AnonymousClassJPanel || panel is VassalSharp.build.module.map.boardPicker.board.mapgrid.HexGridNumbering.AnonymousClassJPanel || panel is org.netbeans.modules.wizard.InstructionsPanel || panel is org.netbeans.api.wizard.displayer.WizardDisplayerImpl.AnonymousClassJPanel)?(System.Drawing.Size) SupportClass.InvokeMethodAsVirtual(panel, "getPreferredSize", new System.Object[]{}):panel.Size).Width + 20, 210);
						}
						//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
						panel.Size = fixedSize;
					}
					else
					{
						//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
						System.Drawing.Size tempAux = System.Drawing.Size.Empty;
						panel.Size = tempAux;
					}
				}
				repack();
			}
		}
		
		public virtual void  repack()
		{
			//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
			System.Windows.Forms.Form w = (System.Windows.Forms.Form) controls.Parent;
			if (w != null)
			{
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				w.pack();
			}
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public override int GetHashCode()
		{
			System.String valueString = ValueString;
			//UPGRADE_NOTE: Final was removed from the declaration of 'prime '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int prime = 31;
			int result = 1;
			result = prime * result + ((valueString == null)?0:valueString.GetHashCode());
			return result;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		public  override bool Equals(System.Object obj)
		{
			if (this == obj)
				return true;
			if (obj == null)
				return false;
			if (GetType() != obj.GetType())
				return false;
			ListConfigurer other = (ListConfigurer) obj;
			System.String valueString = ValueString;
			System.String otherValueString = other.ValueString;
			if (valueString == null)
			{
				if (otherValueString != null)
					return false;
			}
			else if (!valueString.Equals(otherValueString))
				return false;
			return true;
		}
	}
}