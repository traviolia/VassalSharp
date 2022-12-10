/*
* $Id$
*
* Copyright (c) 2005 by Rodney Kinney, Brent Easton
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
using ImageUtils = VassalSharp.tools.image.ImageUtils;
namespace VassalSharp.build.module.gamepieceimage
{
	
	[Serializable]
	public class SwatchComboBox:System.Windows.Forms.ComboBox
	{
		private const long serialVersionUID = 1L;
		
		public SwatchComboBox()
		{
			System.String[] s = ColorManager.getColorManager().getColorNames();
			for (int i = 0; i < s.Length; ++i)
			{
				Items.Add(s[i]);
			}
			SwatchRenderer renderer = new SwatchRenderer(this);
			//UPGRADE_ISSUE: Method 'javax.swing.JComboBox.setRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComboBoxsetRenderer_javaxswingListCellRenderer'"
			setRenderer(renderer);
		}
		
		//UPGRADE_TODO: Interface 'java.awt.event.ItemListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public SwatchComboBox(ItemListener l):this()
		{
			SelectedValueChanged += new System.EventHandler(l.itemStateChanged);
		}
		
		//UPGRADE_TODO: Interface 'java.awt.event.ItemListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public SwatchComboBox(ItemListener l, System.String colorName):this()
		{
			SelectedItem = colorName;
			SelectedValueChanged += new System.EventHandler(l.itemStateChanged);
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SwatchRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_ISSUE: Interface 'javax.swing.ListCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingListCellRenderer'"
		[Serializable]
		public class SwatchRenderer:System.Windows.Forms.Label, ListCellRenderer
		{
			private void  InitBlock(SwatchComboBox enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SwatchComboBox enclosingInstance;
			public SwatchComboBox Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			public SwatchRenderer(SwatchComboBox enclosingInstance)
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
				
				ColorSwatch swatch = ColorManager.getColorManager().getColorSwatch((System.String) value_Renamed);
				
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
				
				//Set the icon and text. If icon was null, say so.
				//String name = (String) list.get
				//UPGRADE_NOTE: Final was removed from the declaration of 'img '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Bitmap img = ImageUtils.createCompatibleImage(25, 12);
				//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
				SupportClass.GraphicsManager.manager.SetColor(g, swatch.Color);
				g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, 0, 25, 12);
				SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.Black);
				g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), 0, 0, 24, 11);
				g.Dispose();
				
				Image = (System.Drawing.Image) img.Clone();
				Text = (System.String) value_Renamed;
				Font = list.Font;
				
				return this;
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SwatchTableRenderer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		//UPGRADE_ISSUE: Interface 'javax.swing.table.TableCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableTableCellRenderer'"
		[Serializable]
		internal class SwatchTableRenderer:System.Windows.Forms.Label, TableCellRenderer
		{
			private void  InitBlock(SwatchComboBox enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private SwatchComboBox enclosingInstance;
			public SwatchComboBox Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			public SwatchTableRenderer(SwatchComboBox enclosingInstance)
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
			//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			public virtual System.Windows.Forms.Control getTableCellRendererComponent(System.Windows.Forms.DataGrid table, System.Object value_Renamed, bool isSelected, bool hasFocus, int row, int column)
			{
				
				ColorSwatch swatch = (ColorSwatch) value_Renamed;
				
				if (isSelected)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionBackground' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionBackground'"
					BackColor = table.getSelectionBackground();
					//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionForeground' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionForeground'"
					ForeColor = table.getSelectionForeground();
				}
				else
				{
					BackColor = table.BackColor;
					ForeColor = table.ForeColor;
				}
				
				//Set the icon and text. If icon was null, say so.
				//String name = (String) list.get
				//UPGRADE_NOTE: Final was removed from the declaration of 'img '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Bitmap img = ImageUtils.createCompatibleImage(25, 12);
				//UPGRADE_NOTE: Final was removed from the declaration of 'g '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(img);
				SupportClass.GraphicsManager.manager.SetColor(g, swatch.Color);
				g.FillRectangle(SupportClass.GraphicsManager.manager.GetPaint(g), 0, 0, 25, 12);
				SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.Black);
				g.DrawRectangle(SupportClass.GraphicsManager.manager.GetPen(g), 0, 0, 24, 11);
				g.Dispose();
				
				Image = (System.Drawing.Image) img.Clone();
				Text = swatch.getConfigureName();
				Font = table.Font;
				
				return this;
			}
		}
	}
}