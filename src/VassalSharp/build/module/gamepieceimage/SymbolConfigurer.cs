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
	
	public class SymbolConfigurer:StringEnumConfigurer
	{
		override public System.Windows.Forms.ComboBox ComboBox
		{
			get
			{
				return (System.Windows.Forms.ComboBox) new SymbolComboBox(this);
			}
			
		}
		
		public SymbolConfigurer(System.String key, System.String name):base(key, name, Symbol.NatoUnitSymbolSet.SymbolNames)
		{
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SymbolComboBox' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		public class SymbolComboBox:System.Windows.Forms.ComboBox
		{
			private void  InitBlock(SymbolConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SymbolConfigurer enclosingInstance;
			public SymbolConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			internal const int sample_w = 20;
			internal const int sample_h = 13;
			
			public SymbolComboBox(SymbolConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
				System.String[] s = Symbol.NatoUnitSymbolSet.SymbolNames;
				for (int i = 0; i < s.Length; ++i)
				{
					Items.Add(s[i]);
				}
				SymbolRenderer renderer = new SymbolRenderer(this);
				//UPGRADE_ISSUE: Method 'javax.swing.JComboBox.setRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComboBoxsetRenderer_javaxswingListCellRenderer'"
				setRenderer(renderer);
			}
			
			//UPGRADE_TODO: Interface 'java.awt.event.ItemListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			public SymbolComboBox(SymbolConfigurer enclosingInstance, ItemListener l):this(enclosingInstance)
			{
				SelectedValueChanged += new System.EventHandler(l.itemStateChanged);
			}
			
			//UPGRADE_TODO: Interface 'java.awt.event.ItemListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
			public SymbolComboBox(SymbolConfigurer enclosingInstance, ItemListener l, System.String symbolName):this(enclosingInstance)
			{
				SelectedItem = symbolName;
				SelectedValueChanged += new System.EventHandler(l.itemStateChanged);
			}
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SymbolRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			//UPGRADE_ISSUE: Interface 'javax.swing.ListCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingListCellRenderer'"
			[Serializable]
			public class SymbolRenderer:System.Windows.Forms.Label, ListCellRenderer
			{
				private void  InitBlock(SymbolComboBox enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private SymbolComboBox enclosingInstance;
				public SymbolComboBox Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				private const long serialVersionUID = 1L;
				
				public SymbolRenderer(SymbolComboBox enclosingInstance)
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
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'img '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Bitmap img = ImageUtils.createCompatibleTranslucentImage(VassalSharp.build.module.gamepieceimage.SymbolConfigurer.SymbolComboBox.sample_w, VassalSharp.build.module.gamepieceimage.SymbolConfigurer.SymbolComboBox.sample_h);
					//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
					
					//UPGRADE_NOTE: Final was removed from the declaration of 'symbol1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String symbol1 = (System.String) value_Renamed;
					//UPGRADE_NOTE: Final was removed from the declaration of 'symbol2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.String symbol2 = Symbol.NatoUnitSymbolSet.NONE;
					//UPGRADE_NOTE: Final was removed from the declaration of 'bounds '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					System.Drawing.Rectangle bounds = new System.Drawing.Rectangle(0, 0, VassalSharp.build.module.gamepieceimage.SymbolConfigurer.SymbolComboBox.sample_w - 1, VassalSharp.build.module.gamepieceimage.SymbolConfigurer.SymbolComboBox.sample_h - 1);
					Symbol.NatoUnitSymbolSet.draw(symbol1, symbol2, g, bounds, Color.BLACK, Color.WHITE, Color.BLACK, 1.0f, ""); //$NON-NLS-1$
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