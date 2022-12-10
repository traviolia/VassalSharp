/*
* $Id$
*
* Copyright (c) 2000-2003 by Rodney Kinney
*
* This library is free software; you can redistribute it and/or modify it under
* the terms of the GNU Library General Public License (LGPL) as published by
* the Free Software Foundation.
*
* This library is distributed in the hope that it will be useful, but WITHOUT
* ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
* FOR A PARTICULAR PURPOSE. See the GNU Library General Public License for more
* details.
*
* You should have received a copy of the GNU Library General Public License
* along with this library; if not, copies are available at
* http://www.opensource.org.
*/
using System;
using ColorConfigurer = VassalSharp.configure.ColorConfigurer;
using Configurer = VassalSharp.configure.Configurer;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.build.module.gamepieceimage
{
	
	public class ColorSwatchConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassPropertyChangeListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassPropertyChangeListener
		{
			public AnonymousClassPropertyChangeListener(ColorSwatchConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ColorSwatchConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ColorSwatchConfigurer enclosingInstance;
			public ColorSwatchConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  propertyChange(System.Object event_sender, SupportClass.PropertyChangingEventArgs e)
			{
				System.Drawing.Color c = (System.Drawing.Color) Enclosing_Instance.config.getValue();
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				ColorSwatch cs = ColorManager.getColorManager().getColorSwatch(ref c);
				Enclosing_Instance.setValue(cs);
				Enclosing_Instance.buildSwatches();
				Enclosing_Instance.updateValue();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassItemListener
		{
			public AnonymousClassItemListener(ColorSwatchConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ColorSwatchConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ColorSwatchConfigurer enclosingInstance;
			public ColorSwatchConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs evt)
			{
				if (event_sender is System.Windows.Forms.MenuItem)
					((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
				Enclosing_Instance.updateValue();
			}
		}
		override public System.String ValueString
		{
			get
			{
				return "";
			}
			
		}
		virtual public System.Drawing.Color ValueColor
		{
			get
			{
				return ((ColorSwatch) value_Renamed).Color;
			}
			
		}
		virtual public ColorSwatch ValueColorSwatch
		{
			get
			{
				return (ColorSwatch) value_Renamed;
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (p == null)
				{
					p = new System.Windows.Forms.Panel();
					swatchPanel = new System.Windows.Forms.Panel();
					
					//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					p.setLayout(new BoxLayout(p, BoxLayout.Y_AXIS));
					
					//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					Box box = Box.createHorizontalBox();
					System.Windows.Forms.Label temp_label2;
					temp_label2 = new System.Windows.Forms.Label();
					temp_label2.Text = name;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = temp_label2;
					box.Controls.Add(temp_Control);
					buildSwatches();
					
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					box.Controls.Add(swatchPanel);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(box);
					
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					colorBox = Box.createHorizontalBox();
					config = new ColorConfigurer("", "Select Color  "); //$NON-NLS-1$
					config.PropertyChange += new SupportClass.PropertyChangeEventHandler(new AnonymousClassPropertyChangeListener(this).propertyChange);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					colorBox.Controls.Add(config.Controls);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(colorBox);
					
					repack();
				}
				return p;
			}
			
		}
		
		protected internal System.Windows.Forms.Panel p;
		protected internal System.Windows.Forms.Panel swatchPanel;
		protected internal System.Windows.Forms.ComboBox swatches;
		//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
		protected internal Box colorBox;
		protected internal ColorConfigurer config;
		
		public ColorSwatchConfigurer(System.String key, System.String name):this(key, name, ColorSwatch.getDefaultSwatch())
		{
		}
		
		public ColorSwatchConfigurer(System.String key, System.String name, ColorSwatch swatch):base(key, name)
		{
			setValue(swatch);
		}
		
		public ColorSwatchConfigurer(System.String key, System.String name, System.String swatchName):this(key, name, ColorManager.getColorManager().getColorSwatch(swatchName))
		{
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		public ColorSwatchConfigurer(System.String key, System.String name, ref System.Drawing.Color color):this(key, name, ColorManager.getColorManager().getColorSwatch(ref color))
		{
		}
		
		protected internal virtual void  buildSwatches()
		{
			if (swatchPanel == null)
			{
				return ;
			}
			
			if (swatches != null)
			{
				swatchPanel.Controls.Remove(swatches);
			}
			
			//UPGRADE_TODO: Interface 'java.awt.event.ItemListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			ItemListener l = new AnonymousClassItemListener(this);
			
			swatches = new SwatchComboBox(l, ((ColorSwatch) value_Renamed).getConfigureName());
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			swatchPanel.Controls.Add(swatches);
		}
		
		protected internal virtual void  updateValue()
		{
			System.String s = (System.String) swatches.SelectedItem;
			if (s.Equals(ColorManager.SELECT_COLOR))
			{
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				setValue(ColorManager.getColorManager().getColorSwatch(ref new System.Drawing.Color[]{(System.Drawing.Color) config.getValue()}[0]));
			}
			else
			{
				setValue(ColorManager.getColorManager().getColorSwatch(s));
			}
			repack();
		}
		
		protected internal virtual void  repack()
		{
			colorBox.Visible = ((ColorSwatch) getValue()).getConfigureName().Equals(ColorManager.SELECT_COLOR);
			//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
			System.Windows.Forms.Form w = (System.Windows.Forms.Form) colorBox.Parent;
			if (w != null)
			{
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				w.pack();
			}
		}
		
		public override void  setValue(System.String s)
		{
			base.setValue(new ColorSwatch(s));
			buildSwatches();
		}
		
		public static ColorSwatch decode(System.String s)
		{
			SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, '|');
			return new ColorSwatch(sd.nextToken(), sd.nextColor(Color.WHITE));
		}
		
		public static System.String encode(ColorSwatch f)
		{
			SequenceEncoder se = new SequenceEncoder(f.getConfigureName(), '|');
			System.Drawing.Color tempAux = f.Color;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			se.append(ColorConfigurer.colorToString(ref tempAux));
			return se.Value;
		}
	}
}