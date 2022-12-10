/*
* $Id$
*
* Copyright (c) 2000-2012 by Rodney Kinney, Brent Easton
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
using GameModule = VassalSharp.build.GameModule;
using ArrayUtils = VassalSharp.tools.ArrayUtils;
using ScrollPane = VassalSharp.tools.ScrollPane;
using FileChooser = VassalSharp.tools.filechooser.FileChooser;
using ImageFileFilter = VassalSharp.tools.filechooser.ImageFileFilter;
using Op = VassalSharp.tools.imageop.Op;
using OpIcon = VassalSharp.tools.imageop.OpIcon;
namespace VassalSharp.counters
{
	
	[Serializable]
	public class ImagePicker:System.Windows.Forms.Panel
	{
		static private System.Int32 state504;
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state504 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private const long serialVersionUID = 1L;
		private const System.String NO_IMAGE = "(No Image)";
		private System.String imageName = null;
		//UPGRADE_NOTE: If the given Font Name does not exist, a default Font instance is created. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1075'"
		protected internal static System.Drawing.Font FONT = new System.Drawing.Font("Dialog", 11, System.Drawing.FontStyle.Regular);
		//UPGRADE_NOTE: Final was removed from the declaration of 'noImage '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Windows.Forms.TextBox noImage;
		//UPGRADE_NOTE: Final was removed from the declaration of 'select '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Windows.Forms.ComboBox select;
		//UPGRADE_NOTE: Final was removed from the declaration of 'icon '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private OpIcon icon;
		//UPGRADE_NOTE: Final was removed from the declaration of 'imageView '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Windows.Forms.Label imageView;
		//UPGRADE_NOTE: Final was removed from the declaration of 'imageViewer '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		private System.Windows.Forms.Panel imageViewer;
		//UPGRADE_NOTE: Final was removed from the declaration of 'imageScroller '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Windows.Forms.ScrollableControl imageScroller;
		
		public ImagePicker()
		{
			System.Windows.Forms.TextBox temp_TextBox;
			temp_TextBox = new System.Windows.Forms.TextBox();
			temp_TextBox.Multiline = true;
			temp_TextBox.WordWrap = false;
			temp_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			noImage = temp_TextBox;
			noImage.Font = FONT;
			//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
			noImage.Text = "Double-click here to add new image";
			noImage.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.counters.ImagePicker.mouseDown);
			noImage.Click += new System.EventHandler(this.mouseClicked);
			noImage.MouseEnter += new System.EventHandler(this.mouseEntered);
			noImage.MouseLeave += new System.EventHandler(this.mouseExited);
			noImage.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mousePressed);
			noImage.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseReleased);
			noImage.ReadOnly = !false;
			noImage.WordWrap = true;
			noImage.WordWrap = true;
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMinimumSize_javaawtDimension'"
			noImage.setMinimumSize(new System.Drawing.Size(15, 32));
			icon = new OpIcon();
			System.Windows.Forms.Label temp_label;
			temp_label = new System.Windows.Forms.Label();
			temp_label.Image = icon;
			imageView = temp_label;
			imageView.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.counters.ImagePicker.mouseDown);
			imageView.Click += new System.EventHandler(this.mouseClicked);
			imageView.MouseEnter += new System.EventHandler(this.mouseEntered);
			imageView.MouseLeave += new System.EventHandler(this.mouseExited);
			imageView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mousePressed);
			imageView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseReleased);
			
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			new BorderLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			imageViewer = new System.Windows.Forms.Panel();
			//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.VERTICAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsVERTICAL_SCROLLBAR_AS_NEEDED_f'"
			//UPGRADE_TODO: Field 'javax.swing.ScrollPaneConstants.HORIZONTAL_SCROLLBAR_AS_NEEDED' was converted to 'true' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingScrollPaneConstantsHORIZONTAL_SCROLLBAR_AS_NEEDED_f'"
			imageScroller = new ScrollPane(imageView, true, true);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			imageViewer.Controls.Add(imageScroller);
			imageScroller.Dock = System.Windows.Forms.DockStyle.Fill;
			imageScroller.BringToFront();
			
			select = new JComboBox(ArrayUtils.prepend(GameModule.getGameModule().getDataArchive().getImageNames(), NO_IMAGE));
			select.SelectedValueChanged += new System.EventHandler(this.itemStateChanged);
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(noImage);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(select);
		}
		
		public virtual System.String getImageName()
		{
			return imageName;
		}
		
		protected internal virtual void  setViewSize()
		{
		}
		
		public virtual void  setImageName(System.String name)
		{
			imageName = name;
			Controls.RemoveAt(0);
			if (name == null || name.Trim().Length == 0 || name.Equals(NO_IMAGE))
			{
				imageName = "";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_int'"
				Controls.Add(noImage);
				Controls.SetChildIndex(noImage, 0);
			}
			else
			{
				icon.setOp(Op.load(imageName));
				System.Drawing.Size d = new System.Drawing.Size(icon.Width, icon.Height);
				if (d.Width > 200)
					d.Width = 200;
				if (d.Height > 200)
					d.Height = 200;
				else
					d.Height += 4;
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				imageScroller.Size = d;
				//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMinimumSize_javaawtDimension'"
				imageScroller.setMinimumSize(d);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_int'"
				Controls.Add(imageViewer);
				Controls.SetChildIndex(imageViewer, 0);
			}
			
			select.SelectedValueChanged -= new System.EventHandler(this.itemStateChanged);
			select.SelectedItem = name;
			if (name != null && !name.Equals(select.SelectedItem))
			{
				select.SelectedItem = name + ".gif";
			}
			select.SelectedValueChanged += new System.EventHandler(this.itemStateChanged);
			Invalidate();
			//UPGRADE_NOTE: Final was removed from the declaration of 'w '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
			System.Windows.Forms.Form w = (System.Windows.Forms.Form) this.Parent;
			if (w != null)
			{
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				w.pack();
			}
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			Refresh();
		}
		
		public virtual void  mouseEntered(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseExited(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseClicked(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
		}
		
		public virtual void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			if (e.Clicks > 1)
			{
				pickImage();
			}
		}
		
		public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs e)
		{
			if (event_sender is System.Windows.Forms.MenuItem)
				((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
			setImageName((System.String) select.SelectedItem);
		}
		
		public virtual void  pickImage()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'fc '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			FileChooser fc = GameModule.getGameModule().getFileChooser();
			fc.FileFilter = new ImageFileFilter();
			
			bool tmpBool;
			if (System.IO.File.Exists(fc.SelectedFile.FullName))
				tmpBool = true;
			else
				tmpBool = System.IO.Directory.Exists(fc.SelectedFile.FullName);
			if (fc.showOpenDialog(this) == FileChooser.APPROVE_OPTION && tmpBool)
			{
				System.String name = fc.SelectedFile.Name;
				GameModule.getGameModule().getArchiveWriter().addImage(fc.SelectedFile.FullName, name);
				//UPGRADE_ISSUE: Method 'javax.swing.JComboBox.setModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComboBoxsetModel_javaxswingComboBoxModel'"
				select.setModel(new DefaultComboBoxModel(ArrayUtils.prepend(GameModule.getGameModule().getDataArchive().getImageNames(), NO_IMAGE)));
				setImageName(name);
			}
			else
			{
				setImageName(NO_IMAGE);
			}
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			Refresh();
		}
	}
}