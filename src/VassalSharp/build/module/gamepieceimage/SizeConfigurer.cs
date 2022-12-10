/*
* $Id$
*
* Copyright (c) 2005 by Rodney Kinney, Brent Easton
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
using ImageUtils = VassalSharp.tools.image.ImageUtils;
namespace VassalSharp.build.module.gamepieceimage
{
	
	public class SizeConfigurer:StringEnumConfigurer
	{
		override public System.Windows.Forms.ComboBox ComboBox
		{
			get
			{
				return (System.Windows.Forms.ComboBox) new SizeComboBox(this);
			}
			
		}
		
		public SizeConfigurer(System.String key, System.String name):base(key, name, Symbol.NatoUnitSymbolSet.SymbolSizes)
		{
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SizeComboBox' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		public class SizeComboBox:System.Windows.Forms.ComboBox
		{
			private void  InitBlock(SizeConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SizeConfigurer enclosingInstance;
			public SizeConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			public SizeComboBox(SizeConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
				System.String[] s = Symbol.NatoUnitSymbolSet.SymbolSizes;
				for (int i = 0; i < s.Length; ++i)
				{
					Items.Add(s[i]);
				}
				SizeRenderer renderer = new SizeRenderer(this);
				//UPGRADE_ISSUE: Method 'javax.swing.JComboBox.setRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComboBoxsetRenderer_javaxswingListCellRenderer'"
				setRenderer(renderer);
			}
			
			//UPGRADE_TODO: Interface 'java.awt.event.ItemListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			public SizeComboBox(SizeConfigurer enclosingInstance, ItemListener l):this(enclosingInstance)
			{
				SelectedValueChanged += new System.EventHandler(l.itemStateChanged);
			}
			
			//UPGRADE_TODO: Interface 'java.awt.event.ItemListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			public SizeComboBox(SizeConfigurer enclosingInstance, ItemListener l, System.String sizeName):this(enclosingInstance)
			{
				SelectedItem = sizeName;
				SelectedValueChanged += new System.EventHandler(l.itemStateChanged);
			}
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SizeRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			//UPGRADE_ISSUE: Interface 'javax.swing.ListCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingListCellRenderer'"
			[Serializable]
			public class SizeRenderer:System.Windows.Forms.Label, ListCellRenderer
			{
				private void  InitBlock(SizeComboBox enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private SizeComboBox enclosingInstance;
				public SizeComboBox Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				private const long serialVersionUID = 1L;
				
				public SizeRenderer(SizeComboBox enclosingInstance)
				{
					InitBlock(enclosingInstance);
					//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setOpaque' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetOpaque_boolean'"
					setOpaque(true);
					//UPGRADE_TODO: Method 'javax.swing.JLabel.setHorizontalAlignment' was converted to 'System.Windows.Forms.Label.ImageAlign' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJLabelsetHorizontalAlignment_int'"
					ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
					//UPGRADE_TODO: Method 'javax.swing.JLabel.setVerticalAlignment' was converted to 'System.Windows.Forms.Label.ImageAlign' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJLabelsetVerticalAlignment_int'"
					ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
				}
				
				/*
				* This method finds the image and text corresponding to the selected
				* value and returns the label, set up to display the text and image.
				*/
				public virtual System.Windows.Forms.Control getListCellRendererComponent(System.Windows.Forms.ListBox list, System.Object value_Renamed, int index, bool isSelected, bool cellHasFocus)
				{
					
					if (isSelected)
					{
						//UPGRADE_ISSUE: Method 'javax.swing.JList.getSelectionBackground' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJListgetSelectionBackground'"
						BackColor = list.getSelectionBackground();
						//UPGRADE_ISSUE: Method 'javax.swing.JList.getSelectionForeground' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJListgetSelectionForeground'"
						ForeColor = list.getSelectionForeground();
					}
					else
					{
						BackColor = list.BackColor;
						ForeColor = list.ForeColor;
					}
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'sample_w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int sample_w = 6;
					//UPGRADE_NOTE: Final was removed from the declaration of 'sample_h '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int sample_h = 12;
					//UPGRADE_NOTE: Final was removed from the declaration of 'sample_g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int sample_g = 1;
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int w = sample_w * 6 + sample_g * 5 + 1;
					//UPGRADE_NOTE: Final was removed from the declaration of 'h '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					int h = sample_h + 1;
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'img '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Bitmap img = ImageUtils.createCompatibleImage(w, h);
					//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
					SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.White);
					g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, 0, w, h);
					SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.Black);
					g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), 0, 0, w - 1, h - 1);
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'simg '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Bitmap simg = Symbol.NatoUnitSymbolSet.buildSizeImage((System.String) value_Renamed, sample_w, sample_h, sample_g);
					int x = (w / 2) - (simg.Width / 2);
					//UPGRADE_WARNING: Method 'java.awt.Graphics.drawImage' was converted to 'System.Drawing.Graphics.drawImage' which may throw an exception. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1101'"
					g.DrawImage(simg, x, 0);
					g.Dispose();
					
					Image = (System.Drawing.Image) img.Clone();
					Text = (System.String) value_Renamed;
					//UPGRADE_TODO: Method 'javax.swing.JLabel.setHorizontalTextPosition' was converted to 'System.Windows.Forms.Label.TextAlign' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJLabelsetHorizontalTextPosition_int'"
					this.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
					//UPGRADE_TODO: Method 'javax.swing.JLabel.setHorizontalAlignment' was converted to 'System.Windows.Forms.Label.ImageAlign' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJLabelsetHorizontalAlignment_int'"
					this.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
					Font = list.Font;
					
					return this;
				}
			}
		}
	}
}