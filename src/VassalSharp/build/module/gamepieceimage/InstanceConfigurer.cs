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
using Configurer = VassalSharp.configure.Configurer;
using StringArrayConfigurer = VassalSharp.configure.StringArrayConfigurer;
using ScrollPane = VassalSharp.tools.ScrollPane;
namespace VassalSharp.build.module.gamepieceimage
{
	
	/// <summary> Controls for configuring an individual ItemInstance</summary>
	public class InstanceConfigurer:Configurer
	{
		private void  InitBlock()
		{
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			(List < ItemInstance >) getValue();
			return ;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			(ArrayList < ItemInstance >) getValue();
			System.String[] p = new System.String[props.size()];
			int i = 0;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(ItemInstance prop: props)
			{
				p[i++] = prop.encode();
			}
			return StringArrayConfigurer.arrayToString(p);
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			ArrayList < ItemInstance > props = new ArrayList < ItemInstance >();
			System.String[] p = StringArrayConfigurer.stringToArray(s);
			for (int i = 0; i < p.Length; i++)
			{
				if (p[i].StartsWith(SymbolItem.TYPE))
				{
					props.add(new SymbolItemInstance(p[i], defn));
				}
				else if (p[i].StartsWith(TextBoxItem.TYPE))
				{
					props.add(new TextBoxItemInstance(p[i], defn));
				}
				else if (p[i].StartsWith(TextItem.TYPE))
				{
					props.add(new TextItemInstance(p[i], defn));
				}
				else if (p[i].StartsWith(ShapeItem.TYPE))
				{
					props.add(new ShapeItemInstance(p[i], defn));
				}
				else if (p[i].StartsWith(ImageItem.TYPE))
				{
					props.add(new ImageItemInstance(p[i], defn));
				}
			}
			return props;
		}
		override public System.String ValueString
		{
			get
			{
				return PropertiesToString(getValueList());
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (panel == null)
				{
					
					panel = new System.Windows.Forms.Panel();
					//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
					//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
					panel.setLayout(new BoxLayout(panel, BoxLayout.Y_AXIS));
					
					//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					Box filler = Box.createHorizontalBox();
					filler.setPreferredSize(new System.Drawing.Size(50, 10));
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					panel.Controls.Add(filler);
					
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					visBox = Box.createHorizontalBox();
					//UPGRADE_ISSUE: Field 'java.awt.Component.CENTER_ALIGNMENT' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentCENTER_ALIGNMENT_f'"
					visBox.setAlignmentX(Box.CENTER_ALIGNMENT);
					visualizer = new Visualizer(defn);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					System.Windows.Forms.Control temp_Control;
					temp_Control = new ScrollPane(visualizer);
					visBox.Controls.Add(temp_Control);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					panel.Controls.Add(visBox);
					
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					filler = Box.createHorizontalBox();
					filler.setPreferredSize(new System.Drawing.Size(50, 10));
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					panel.Controls.Add(filler);
					
					symbolPanel = new SymbolPanel(this);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					panel.Controls.Add(symbolPanel);
				}
				
				return panel;
			}
			
		}
		
		protected internal GamePieceImage defn;
		//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
		protected internal Box visBox;
		protected internal Visualizer visualizer = new Visualizer();
		protected internal System.Windows.Forms.Panel panel;
		//protected TextPanel itemPanel;
		protected internal SymbolPanel symbolPanel;
		protected internal InstanceConfigurer me;
		
		protected internal InstanceConfigurer():base(null, null)
		{
			InitBlock();
			me = this;
		}
		
		protected internal InstanceConfigurer(System.String key, System.String name, GamePieceImage defn):base(key, name)
		{
			InitBlock();
			this.defn = defn;
			setValue(defn.getInstances());
			me = this;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked)
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public List < ItemInstance > getValueList()
		
		/// <deprecated> Use {@link #getValueList()} instead. 
		/// </deprecated>
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		SuppressWarnings(unchecked) 
		@ Deprecated
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public ArrayList < ItemInstance > getValueArrayList()
		
		public override void  setValue(System.String s)
		{
			setValue(StringToProperties(s, defn));
			if (symbolPanel != null)
			{
				symbolPanel.reset();
			}
		}
		
		public static System.String PropertiesToString;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		(List < ItemInstance > props)
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public static List < ItemInstance > StringToProperties(String s, 
		GamePieceImage defn)
		
		public virtual void  refresh()
		{
			if (symbolPanel != null)
			{
				symbolPanel.refresh();
			}
			visualizer.rebuild();
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SymbolPanel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		protected internal class SymbolPanel:System.Windows.Forms.Panel
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassListSelectionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassListSelectionListener
			{
				public AnonymousClassListSelectionListener(SymbolPanel enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(SymbolPanel enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private SymbolPanel enclosingInstance;
				public SymbolPanel Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				public virtual void  valueChanged(System.Object event_sender, System.EventArgs e)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.event.ListSelectionEvent.getValueIsAdjusting' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingeventListSelectionEventgetValueIsAdjusting'"
					if (e.getValueIsAdjusting())
						return ;
					
					//UPGRADE_TODO: Interface 'javax.swing.ListSelectionModel' was converted to 'SupportClass.ListSelectionModelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					//UPGRADE_TODO: The method 'javax.swing.event.ListSelectionEvent.getSource' needs to be in a event handling method in order to be properly converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1171'"
					SupportClass.ListSelectionModelSupport lsm = (SupportClass.ListSelectionModelSupport) e.getSource();
					if (lsm.SelectedItems.Count.Equals(0))
					{
						Enclosing_Instance.showItem(VassalSharp.build.module.gamepieceimage.InstanceConfigurer.SymbolPanel.NO_CURRENT_ITEM);
					}
					else
					{
						int selectedRow = lsm.GetMinSelectionIndex();
						Enclosing_Instance.showItem(selectedRow);
					}
				}
			}
			private void  InitBlock(InstanceConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private InstanceConfigurer enclosingInstance;
			public InstanceConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			private const long serialVersionUID = 1L;
			
			//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			protected internal System.Windows.Forms.DataGrid table;
			//UPGRADE_TODO: Class 'javax.swing.table.AbstractTableModel' was converted to 'System.Data.DataTable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			protected internal System.Data.DataTable model;
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			protected internal System.Windows.Forms.ScrollableControl scrollPane;
			protected internal System.Windows.Forms.Button addSymbolBtn, addTextBtn, remBtn;
			protected internal System.Windows.Forms.Panel mainPanel;
			protected internal System.Windows.Forms.Panel detailPanel;
			protected internal System.Windows.Forms.Control detailControls;
			protected internal int currentDetail;
			protected internal const int NO_CURRENT_ITEM = - 1;
			
			protected internal const int NAME_COL = 0;
			protected internal const int TYPE_COL = 1;
			protected internal const int LOC_COL = 2;
			protected internal const int MAX_COL = 2;
			
			public SymbolPanel(InstanceConfigurer enclosingInstance)
			{
				InitBlock(enclosingInstance);
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				setLayout(new BoxLayout(this, BoxLayout.Y_AXIS));
				
				mainPanel = new System.Windows.Forms.Panel();
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
				System.Windows.Forms.ControlPaint.DrawBorder3D(mainPanel.CreateGraphics(), 0, 0, mainPanel.Width, mainPanel.Height, System.Windows.Forms.Border3DStyle.Flat);
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				mainPanel.setLayout(new BoxLayout(mainPanel, BoxLayout.Y_AXIS));
				
				//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				Box box = Box.createHorizontalBox();
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = "Items";
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				box.Controls.Add(temp_Control);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				mainPanel.Controls.Add(box);
				
				model = new SymbolTableModel(this);
				System.Windows.Forms.DataGrid temp_DataGrid;
				temp_DataGrid = new System.Windows.Forms.DataGrid();
				temp_DataGrid.DataSource = model;
				table = temp_DataGrid;
				//UPGRADE_ISSUE: Method 'javax.swing.JTable.setSelectionMode' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablesetSelectionMode_int'"
				//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.ListSelectionModel.SINGLE_SELECTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				table.setSelectionMode((int) System.Windows.Forms.SelectionMode.One);
				if (getValueList() != null && getValueList().size() > 0)
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionModel'"
					table.getSelectionModel().SetSelectionInterval(0, 0);
				}
				//UPGRADE_TODO: Interface 'javax.swing.ListSelectionModel' was converted to 'SupportClass.ListSelectionModelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionModel'"
				SupportClass.ListSelectionModelSupport rowSM = table.getSelectionModel();
				//UPGRADE_ISSUE: Method 'javax.swing.ListSelectionModel.addListSelectionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingListSelectionModeladdListSelectionListener_javaxswingeventListSelectionListener'"
				rowSM.addListSelectionListener(new AnonymousClassListSelectionListener(this));
				
				scrollPane = new ScrollPane(table);
				//UPGRADE_ISSUE: Method 'javax.swing.JTable.setPreferredScrollableViewportSize' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablesetPreferredScrollableViewportSize_javaawtDimension'"
				table.setPreferredScrollableViewportSize(new System.Drawing.Size(300, 100));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				mainPanel.Controls.Add(scrollPane);
				
				detailPanel = new System.Windows.Forms.Panel();
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = new ScrollPane(detailPanel);
				mainPanel.Controls.Add(temp_Control2);
				
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(mainPanel);
				
				showItem(0);
			}
			
			protected internal virtual void  showItem(int itemNo)
			{
				
				if (detailControls != null)
				{
					detailPanel.Controls.Remove(detailControls);
					detailControls = null;
					currentDetail = NO_CURRENT_ITEM;
				}
				
				int count = getValueList().size();
				
				if (itemNo != NO_CURRENT_ITEM && count > 0 && itemNo < count)
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'instance '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					ItemInstance instance = getValueList().get_Renamed(itemNo);
					instance.Config = Enclosing_Instance.me;
					//UPGRADE_NOTE: Final was removed from the declaration of 'c '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					Configurer c = instance.Configurer;
					detailControls = c.Controls;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					detailPanel.Controls.Add(detailControls);
					currentDetail = itemNo;
				}
				
				reshow();
			}
			
			public virtual void  reset()
			{
				showItem(currentDetail);
			}
			
			public virtual void  reshow()
			{
				
				repack();
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				detailPanel.Refresh();
			}
			
			public virtual void  refresh()
			{
				showItem(currentDetail);
				reshow();
			}
			
			protected internal virtual void  repack()
			{
				
				//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
				System.Windows.Forms.Form w = (System.Windows.Forms.Form) Enclosing_Instance.panel.Parent;
				if (w != null)
				{
					//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
					w.pack();
				}
			}
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'SymbolTableModel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			//UPGRADE_TODO: Class 'javax.swing.table.AbstractTableModel' was converted to 'System.Data.DataTable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			[Serializable]
			internal class SymbolTableModel:System.Data.DataTable
			{
				public SymbolTableModel(SymbolPanel enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(SymbolPanel enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
					return typeof(System.String);
				}
				private SymbolPanel enclosingInstance;
				public SymbolPanel Enclosing_Instance
				{
					get
					{
						return enclosingInstance;
					}
					
				}
				private const long serialVersionUID = 1L;
				
				private System.String[] columnNames = new System.String[]{"Name", "Type", "Position"};
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.getColumnCount' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public int getColumnCount()
				{
					return columnNames.Length;
				}
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.getRowCount' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public int getRowCount()
				{
					return getValueList() == null?0:getValueList().size();
				}
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.getColumnName' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public System.String getColumnName(int col)
				{
					return columnNames[col];
				}
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.getValueAt' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public System.Object getValueAt(int row, int col)
				{
					if (col == VassalSharp.build.module.gamepieceimage.InstanceConfigurer.SymbolPanel.NAME_COL)
					{
						return getValueList().get_Renamed(row).getName();
					}
					else if (col == VassalSharp.build.module.gamepieceimage.InstanceConfigurer.SymbolPanel.TYPE_COL)
					{
						return getValueList().get_Renamed(row).getItem().getDisplayName();
					}
					else if (col == VassalSharp.build.module.gamepieceimage.InstanceConfigurer.SymbolPanel.LOC_COL)
					{
						return getValueList().get_Renamed(row).getLocation();
					}
					else
						return null;
				}
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				public Class < String > getColumnClass(int col)
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.isCellEditable' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public bool isCellEditable(int row, int col)
				{
					return false;
				}
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.setValueAt' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public void  setValueAt(System.Object value_Renamed, int row, int col)
				{
					
					//        fireTableCellUpdated(row, col);
					//        visualizer.rebuild();
				}
			}
		}
		
		
		
		public virtual void  rebuildViz()
		{
			if (visualizer != null)
			{
				visualizer.rebuild();
			}
		}
		
		public virtual void  repack()
		{
			if (panel != null)
			{
				//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
				System.Windows.Forms.Form w = (System.Windows.Forms.Form) panel.Parent;
				if (w != null)
				{
					//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
					w.pack();
				}
			}
			rebuildViz();
		}
	}
}