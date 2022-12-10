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
using ScrollPane = VassalSharp.tools.ScrollPane;
namespace VassalSharp.counters
{
	
	[Serializable]
	public class MultiImagePicker:System.Windows.Forms.Panel
	{
		static private System.Int32 state529;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassListSelectionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassListSelectionListener
		{
			public AnonymousClassListSelectionListener(MultiImagePicker enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(MultiImagePicker enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MultiImagePicker enclosingInstance;
			public MultiImagePicker Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  valueChanged(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.showSelected();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassKeyAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassKeyAdapter
		{
			public AnonymousClassKeyAdapter(MultiImagePicker enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(MultiImagePicker enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MultiImagePicker enclosingInstance;
			public MultiImagePicker Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs evt)
			{
				//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyCode' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyCode'"
				if (evt.KeyValue == (int) System.Windows.Forms.Keys.Enter)
				{
					Enclosing_Instance.showSelected();
				}
			}
		}
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state529 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private void  InitBlock()
		{
			//UPGRADE_NOTE: Final was removed from the declaration of 'size '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			int size = imageListElements.Count;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			final ArrayList < String > names = new ArrayList < String >(size);
			for (int i = 0; i < size; ++i)
			{
				//UPGRADE_TODO: Method 'java.awt.Container.getComponent' was converted to 'System.Windows.Forms.Control.Controls' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContainergetComponent_int'"
				names.add((((ImagePicker) multiPanel.Controls[i]).getImageName()));
			}
			return names;
			return Collections.enumeration(getImageNameList());
		}
		virtual public System.Windows.Forms.ListBox List
		{
			get
			{
				return imageList;
			}
			
		}
		virtual public System.String[] ImageList
		{
			set
			{
				while (value.Length > (int) multiPanel.Controls.Count)
				{
					addEntry();
				}
				for (int i = 0; i < value.Length; ++i)
				{
					//UPGRADE_TODO: Method 'java.awt.Container.getComponent' was converted to 'System.Windows.Forms.Control.Controls' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContainergetComponent_int'"
					((ImagePicker) multiPanel.Controls[i]).setImageName(value[i]);
				}
			}
			
		}
		private const long serialVersionUID = 1L;
		
		protected internal System.Windows.Forms.ListBox imageList;
		//UPGRADE_TODO: Class 'javax.swing.DefaultListModel' was converted to 'System.Windows.Forms.ListBox.ObjectCollection' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingDefaultListModel'"
		protected internal System.Windows.Forms.ListBox.ObjectCollection imageListElements = new System.Windows.Forms.ListBox.ObjectCollection(new System.Windows.Forms.ListBox());
		//UPGRADE_ISSUE: Class 'java.awt.CardLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
		//UPGRADE_ISSUE: Constructor 'java.awt.CardLayout.CardLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
		protected internal CardLayout cl = new CardLayout();
		protected internal System.Windows.Forms.Panel multiPanel = new System.Windows.Forms.Panel();
		
		public MultiImagePicker()
		{
			InitBlock();
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.X_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
			setLayout(new BoxLayout(this, BoxLayout.X_AXIS));
			
			System.Windows.Forms.ListBox temp_ListBox;
			temp_ListBox = new System.Windows.Forms.ListBox();
			temp_ListBox.Items.AddRange(imageListElements);
			temp_ListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			imageList = temp_ListBox;
			imageList.SelectedValueChanged += new System.EventHandler(new AnonymousClassListSelectionListener(this).valueChanged);
			imageList.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.counters.MultiImagePicker.keyDown);
			imageList.KeyUp += new System.Windows.Forms.KeyEventHandler(new AnonymousClassKeyAdapter(this).keyReleased);
			//UPGRADE_ISSUE: Method 'javax.swing.JList.setVisibleRowCount' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJListsetVisibleRowCount_int'"
			imageList.setVisibleRowCount(4);
			//UPGRADE_TODO: Method 'javax.swing.JList.setPrototypeCellValue' was converted to 'System.Windows.Forms.ListBox.Width' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetPrototypeCellValue_javalangObject'"
			imageList.Width = (System.Int32) "Image 999";
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMinimumSize_javaawtDimension'"
			//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			imageList.setMinimumSize(imageList.Size);
			
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			multiPanel.setLayout(cl);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(multiPanel);
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Windows.Forms.ScrollableControl scroll = new ScrollPane(imageList);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setMinimumSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetMinimumSize_javaawtDimension'"
			//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
			//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			scroll.getViewport().setMinimumSize(imageList.Size);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(scroll);
			
			addEntry();
		}
		
		//UPGRADE_TODO: Interface 'javax.swing.event.ListSelectionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1095'"
		public virtual void  addListSelectionListener(ListSelectionListener l)
		{
			imageList.SelectedValueChanged += new System.EventHandler(l.valueChanged);
		}
		
		public virtual void  showSelected()
		{
			if (imageList.SelectedItem != null)
			{
				//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
				cl.show(multiPanel, (System.String) imageList.SelectedItem);
			}
		}
		
		public virtual void  addEntry()
		{
			System.String entry = "Image " + (imageListElements.Count + 1);
			imageListElements.Add(entry);
			ImagePicker pick = new ImagePicker();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javalangString_javaawtComponent'"
			multiPanel.Controls.Add(pick);
			//UPGRADE_TODO: Method 'javax.swing.JList.setSelectedIndex' was converted to 'System.Windows.Forms.ListBox.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetSelectedIndex_int'"
			imageList.SelectedIndex = imageListElements.Count - 1;
			//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			cl.show(multiPanel, (System.String) imageList.SelectedItem);
		}
		
		/// <summary> Returns a list of image names in this picker.
		/// 
		/// </summary>
		/// <returns> the list of image names
		/// </returns>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < String > getImageNameList()
		
		/// <summary>Use {@link #getImageNameList()} instead. </summary>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Deprecated
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public Enumeration < String > getImageNames()
		
		public virtual void  removeEntryAt(int index)
		{
			if (index < 0 || index >= imageListElements.Count)
			{
				return ;
			}
			
			multiPanel.Controls.RemoveAt(index);
			imageListElements.RemoveAt(index);
			if (index < imageListElements.Count)
			{
				//UPGRADE_TODO: Method 'javax.swing.JList.setSelectedIndex' was converted to 'System.Windows.Forms.ListBox.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetSelectedIndex_int'"
				imageList.SelectedIndex = index;
			}
			else if (index > 0)
			{
				//UPGRADE_TODO: Method 'javax.swing.JList.setSelectedIndex' was converted to 'System.Windows.Forms.ListBox.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetSelectedIndex_int'"
				imageList.SelectedIndex = index - 1;
			}
			if (imageList.SelectedItem != null)
			{
				//UPGRADE_ISSUE: Method 'java.awt.CardLayout.show' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
				cl.show(multiPanel, (System.String) imageList.SelectedItem);
			}
		}
		
		public virtual void  clear()
		{
			for (int i = 0; i < imageListElements.Count; ++i)
			{
				//UPGRADE_TODO: Method 'java.awt.Container.getComponent' was converted to 'System.Windows.Forms.Control.Controls' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContainergetComponent_int'"
				((ImagePicker) multiPanel.Controls[i]).setImageName(null);
			}
			multiPanel.Controls.Clear();
			imageListElements.Clear();
		}
		
		public virtual void  swap(int index1, int index2)
		{
			System.Windows.Forms.Control[] components = new System.Windows.Forms.Control[imageListElements.Count];
			for (int i = 0; i < imageListElements.Count; i++)
			{
				//UPGRADE_TODO: Method 'java.awt.Container.getComponent' was converted to 'System.Windows.Forms.Control.Controls' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContainergetComponent_int'"
				components[i] = multiPanel.Controls[i];
			}
			multiPanel.Controls.Clear();
			//UPGRADE_ISSUE: Constructor 'java.awt.CardLayout.CardLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtCardLayout'"
			cl = new CardLayout();
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			multiPanel.setLayout(cl);
			
			for (int i = 0; i < imageListElements.Count; i++)
			{
				System.Windows.Forms.Control c = null;
				if (i == index1)
				{
					c = components[index2];
				}
				else if (i == index2)
				{
					c = components[index1];
				}
				else
				{
					c = components[i];
				}
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
				multiPanel.Controls.Add(c);
				c.Dock = new System.Windows.Forms.DockStyle();
				c.BringToFront();
			}
			
			//UPGRADE_TODO: Method 'javax.swing.JList.setSelectedIndex' was converted to 'System.Windows.Forms.ListBox.SelectedIndex' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJListsetSelectedIndex_int'"
			imageList.SelectedIndex = index2;
			showSelected();
		}
	}
}