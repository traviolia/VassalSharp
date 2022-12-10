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
using Configurer = VassalSharp.configure.Configurer;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.build.module.gamepieceimage
{
	
	public class FontStyleConfigurer:Configurer
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassItemListener
		{
			public AnonymousClassItemListener(FontStyleConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(FontStyleConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private FontStyleConfigurer enclosingInstance;
			public FontStyleConfigurer Enclosing_Instance
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
		virtual public System.Drawing.Font ValueFont
		{
			get
			{
				return ((FontStyle) value_Renamed).Font;
			}
			
		}
		virtual public FontStyle ValueFontStyle
		{
			get
			{
				return (FontStyle) value_Renamed;
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (p == null)
				{
					p = new System.Windows.Forms.Panel();
					fontPanel = new System.Windows.Forms.Panel();
					
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
					
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					box.Controls.Add(fontPanel);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					p.Controls.Add(box);
				}
				buildFonts();
				return p;
			}
			
		}
		
		protected internal System.Windows.Forms.Panel p;
		protected internal System.Windows.Forms.Panel fontPanel;
		protected internal System.Windows.Forms.ComboBox fonts;
		
		public FontStyleConfigurer(System.String key, System.String name):base(key, name)
		{
		}
		
		public FontStyleConfigurer(System.String key, System.String name, FontStyle fontStyle):this(key, name)
		{
			setValue(fontStyle);
		}
		
		public FontStyleConfigurer(System.String key, System.String name, System.String styleName):this(key, name, FontManager.getFontManager().getFontStyle(styleName))
		{
		}
		
		protected internal virtual void  buildFonts()
		{
			if (fontPanel == null)
			{
				return ;
			}
			
			if (fonts != null)
			{
				fontPanel.Controls.Remove(fonts);
			}
			
			fonts = new System.Windows.Forms.ComboBox();
			System.String[] s = FontManager.getFontManager().getFontNames();
			for (int i = 0; i < s.Length; i++)
			{
				fonts.Items.Add(s[i]);
			}
			fonts.SelectedItem = value_Renamed == null?"Default":((FontStyle) value_Renamed).getConfigureName(); //$NON-NLS-1$
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			fontPanel.Controls.Add(fonts);
			
			//UPGRADE_TODO: Interface 'java.awt.event.ItemListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			ItemListener l = new AnonymousClassItemListener(this);
			
			fonts.SelectedValueChanged += new System.EventHandler(l.itemStateChanged);
		}
		
		protected internal virtual void  updateValue()
		{
			setValue(FontManager.getFontManager().getFontStyle((System.String) fonts.SelectedItem));
		}
		
		public override void  setValue(System.String s)
		{
			setValue(FontManager.getFontManager().getFontStyle(s));
			buildFonts();
		}
		
		public static FontStyle decode(System.String s)
		{
			SequenceEncoder.Decoder sd = new SequenceEncoder.Decoder(s, '|');
			return new FontStyle(sd.nextToken("Default"), FontConfigurer.decode(sd.nextToken(""))); //$NON-NLS-1$ //$NON-NLS-2$
		}
		
		public static System.String encode(FontStyle f)
		{
			SequenceEncoder se = new SequenceEncoder(f.getConfigureName(), '|');
			se.append(FontConfigurer.encode(f.Font));
			return se.Value;
		}
	}
}