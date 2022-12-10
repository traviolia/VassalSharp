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
using Configurer = VassalSharp.configure.Configurer;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using ScrollPane = VassalSharp.tools.ScrollPane;
namespace VassalSharp.build.module.gamepieceimage
{
	
	public class LayoutConfigurer:Configurer
	{
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
					
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					filler = Box.createHorizontalBox();
					filler.setPreferredSize(new System.Drawing.Size(50, 10));
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					panel.Controls.Add(filler);
					
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					visBox = Box.createHorizontalBox();
					//UPGRADE_ISSUE: Field 'java.awt.Component.CENTER_ALIGNMENT' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtComponentCENTER_ALIGNMENT_f'"
					visBox.setAlignmentX(Box.CENTER_ALIGNMENT);
					visualizer = new Visualizer(layout);
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
					
					itemPanel = new ItemPanel(this);
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					panel.Controls.Add(itemPanel);
					
					//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
					filler = Box.createHorizontalBox();
					filler.setPreferredSize(new System.Drawing.Size(50, 10));
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					panel.Controls.Add(filler);
					
					//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
					System.Windows.Forms.Form w = (System.Windows.Forms.Form) itemPanel.Parent;
					if (w != null)
					{
						//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
						w.pack();
					}
				}
				
				return panel;
			}
			
		}
		override public System.String ValueString
		{
			get
			{
				
				return null;
			}
			
		}
		
		protected internal const System.String ADD_SYMBOL = "Symbol";
		protected internal const System.String ADD_IMAGE = "Image";
		protected internal const System.String ADD_TEXT = "Label";
		protected internal const System.String ADD_TEXTBOX = "Text Box";
		protected internal const System.String ADD_SHAPE = "Shape";
		protected internal const System.String REMOVE = "Remove";
		protected internal const System.String UP = "Up";
		protected internal const System.String DOWN = "Down";
		protected internal const int NO_CURRENT_ITEM = - 1;
		
		protected internal System.Windows.Forms.Panel panel;
		protected internal ItemPanel itemPanel;
		protected internal System.Windows.Forms.Panel itemConfigPanel;
		protected internal System.Windows.Forms.Control currentItemControls;
		protected internal int currentItem = NO_CURRENT_ITEM;
		//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
		protected internal Box visBox;
		protected internal Visualizer visualizer = new Visualizer();
		protected internal System.Windows.Forms.Label visLabel;
		//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
		protected internal Box filler;
		protected internal GamePieceLayout layout;
		
		protected internal StringConfigurer defName;
		protected internal NewIntConfigurer height, width;
		
		protected internal LayoutConfigurer():base(null, null)
		{
		}
		
		protected internal LayoutConfigurer(System.String key, System.String name, GamePieceLayout def):base(key, name)
		{
			layout = def;
		}
		
		public override System.Object getValue()
		{
			if (layout != null)
			{
				
				layout.setConfigureName(defName.ValueString);
				layout.setHeight(((System.Int32) height.getValue()));
				layout.setWidth(((System.Int32) width.getValue()));
			}
			return layout;
		}
		
		public override void  setValue(System.String s)
		{
			if (itemPanel != null)
			{
				itemPanel.reshow();
			}
		}
		
		protected internal virtual void  repack()
		{
			
			//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
			System.Windows.Forms.Form w = (System.Windows.Forms.Form) panel.Parent;
			if (w != null)
			{
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				w.pack();
			}
			if (visualizer != null)
			{
				visualizer.rebuild();
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ItemPanel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		[Serializable]
		protected internal class ItemPanel:System.Windows.Forms.Panel
		{
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassListSelectionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			private class AnonymousClassListSelectionListener
			{
				public AnonymousClassListSelectionListener(ItemPanel enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(ItemPanel enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
				}
				private ItemPanel enclosingInstance;
				public ItemPanel Enclosing_Instance
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
						Enclosing_Instance.showItem(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.NO_CURRENT_ITEM);
					}
					else
					{
						int selectedRow = lsm.GetMinSelectionIndex();
						Enclosing_Instance.showItem(selectedRow);
					}
				}
			}
			private void  InitBlock(LayoutConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private LayoutConfigurer enclosingInstance;
			public LayoutConfigurer Enclosing_Instance
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
			protected internal System.Windows.Forms.Button addSymbolBtn, addTextBtn, addTextBoxBtn, addImageBtn, addShapeBtn, remBtn, upBtn, dnBtn;
			protected internal System.Windows.Forms.Panel mainPanel;
			
			public ItemPanel(LayoutConfigurer enclosingInstance)
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
				
				model = new MyTableModel(this);
				System.Windows.Forms.DataGrid temp_DataGrid;
				temp_DataGrid = new System.Windows.Forms.DataGrid();
				temp_DataGrid.DataSource = model;
				table = temp_DataGrid;
				//UPGRADE_ISSUE: Method 'javax.swing.JTable.setSelectionMode' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablesetSelectionMode_int'"
				//UPGRADE_TODO: The equivalent in .NET for field 'javax.swing.ListSelectionModel.SINGLE_SELECTION' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				table.setSelectionMode((int) System.Windows.Forms.SelectionMode.One);
				if (Enclosing_Instance.layout.getItemCount() > 0)
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
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				box = Box.createHorizontalBox();
				addSymbolBtn = SupportClass.ButtonSupport.CreateStandardButton(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.ADD_SYMBOL);
				if (addSymbolBtn is VassalSharp.tools.LaunchButton)
					((VassalSharp.tools.LaunchButton) addSymbolBtn).setToolTipText("Add a Symbol to the Layout");
				else
					SupportClass.ToolTipSupport.setToolTipText(addSymbolBtn, "Add a Symbol to the Layout");
				addSymbolBtn.Click += new System.EventHandler(this.actionPerformed);
				SupportClass.CommandManager.CheckCommand(addSymbolBtn);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(addSymbolBtn);
				addTextBtn = SupportClass.ButtonSupport.CreateStandardButton(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.ADD_TEXT);
				if (addTextBtn is VassalSharp.tools.LaunchButton)
					((VassalSharp.tools.LaunchButton) addTextBtn).setToolTipText("Add Text to the Layout");
				else
					SupportClass.ToolTipSupport.setToolTipText(addTextBtn, "Add Text to the Layout");
				addTextBtn.Click += new System.EventHandler(this.actionPerformed);
				SupportClass.CommandManager.CheckCommand(addTextBtn);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(addTextBtn);
				addTextBoxBtn = SupportClass.ButtonSupport.CreateStandardButton(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.ADD_TEXTBOX);
				if (addTextBoxBtn is VassalSharp.tools.LaunchButton)
					((VassalSharp.tools.LaunchButton) addTextBoxBtn).setToolTipText("Add Text Box to the Layout");
				else
					SupportClass.ToolTipSupport.setToolTipText(addTextBoxBtn, "Add Text Box to the Layout");
				addTextBoxBtn.Click += new System.EventHandler(this.actionPerformed);
				SupportClass.CommandManager.CheckCommand(addTextBoxBtn);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(addTextBoxBtn);
				addImageBtn = SupportClass.ButtonSupport.CreateStandardButton(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.ADD_IMAGE);
				if (addImageBtn is VassalSharp.tools.LaunchButton)
					((VassalSharp.tools.LaunchButton) addImageBtn).setToolTipText("Add an Image to the Layout");
				else
					SupportClass.ToolTipSupport.setToolTipText(addImageBtn, "Add an Image to the Layout");
				addImageBtn.Click += new System.EventHandler(this.actionPerformed);
				SupportClass.CommandManager.CheckCommand(addImageBtn);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(addImageBtn);
				addShapeBtn = SupportClass.ButtonSupport.CreateStandardButton(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.ADD_SHAPE);
				if (addShapeBtn is VassalSharp.tools.LaunchButton)
					((VassalSharp.tools.LaunchButton) addShapeBtn).setToolTipText("Add a Colored Shape to the Layout");
				else
					SupportClass.ToolTipSupport.setToolTipText(addShapeBtn, "Add a Colored Shape to the Layout");
				addShapeBtn.Click += new System.EventHandler(this.actionPerformed);
				SupportClass.CommandManager.CheckCommand(addShapeBtn);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(addShapeBtn);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				mainPanel.Controls.Add(box);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				box = Box.createHorizontalBox();
				remBtn = SupportClass.ButtonSupport.CreateStandardButton(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.REMOVE);
				if (remBtn is VassalSharp.tools.LaunchButton)
					((VassalSharp.tools.LaunchButton) remBtn).setToolTipText("Remove the selected Item");
				else
					SupportClass.ToolTipSupport.setToolTipText(remBtn, "Remove the selected Item");
				remBtn.Click += new System.EventHandler(this.actionPerformed);
				SupportClass.CommandManager.CheckCommand(remBtn);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(remBtn);
				upBtn = SupportClass.ButtonSupport.CreateStandardButton(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.UP);
				if (upBtn is VassalSharp.tools.LaunchButton)
					((VassalSharp.tools.LaunchButton) upBtn).setToolTipText("Move the selected Item up the list (draw earlier)");
				else
					SupportClass.ToolTipSupport.setToolTipText(upBtn, "Move the selected Item up the list (draw earlier)");
				upBtn.Click += new System.EventHandler(this.actionPerformed);
				SupportClass.CommandManager.CheckCommand(upBtn);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(upBtn);
				dnBtn = SupportClass.ButtonSupport.CreateStandardButton(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.DOWN);
				if (dnBtn is VassalSharp.tools.LaunchButton)
					((VassalSharp.tools.LaunchButton) dnBtn).setToolTipText("Move the selected Item down the list (draw later)");
				else
					SupportClass.ToolTipSupport.setToolTipText(dnBtn, "Move the selected Item down the list (draw later)");
				dnBtn.Click += new System.EventHandler(this.actionPerformed);
				SupportClass.CommandManager.CheckCommand(dnBtn);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				box.Controls.Add(dnBtn);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				mainPanel.Controls.Add(box);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(mainPanel);
				
				//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
				box = Box.createHorizontalBox();
				box.setPreferredSize(new System.Drawing.Size(50, 10));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				Controls.Add(box);
				
				Enclosing_Instance.itemConfigPanel = new System.Windows.Forms.Panel();
				//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
				System.Windows.Forms.ControlPaint.DrawBorder3D(Enclosing_Instance.itemConfigPanel.CreateGraphics(), 0, 0, Enclosing_Instance.itemConfigPanel.Width, Enclosing_Instance.itemConfigPanel.Height, System.Windows.Forms.Border3DStyle.Flat);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control2;
				temp_Control2 = new ScrollPane(Enclosing_Instance.itemConfigPanel);
				Controls.Add(temp_Control2);
				
				showItem(0);
			}
			
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				System.String action = SupportClass.CommandManager.GetCommand(event_sender);
				int pos = Enclosing_Instance.layout.getItemCount();
				int sel = table.CurrentRowIndex;
				
				if (action.Equals(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.ADD_SYMBOL))
				{
					addItem(new SymbolItem(Enclosing_Instance.layout, "Symbol" + pos)); //$NON-NLS-1$
				}
				else if (action.Equals(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.ADD_TEXT))
				{
					TextItem item = new TextItem(Enclosing_Instance.layout, "Text" + pos); //$NON-NLS-1$
					addItem(item);
				}
				else if (action.Equals(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.ADD_TEXTBOX))
				{
					TextBoxItem item = new TextBoxItem(Enclosing_Instance.layout, "TextBox" + pos); //$NON-NLS-1$
					addItem(item);
				}
				else if (action.Equals(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.ADD_IMAGE))
				{
					addItem(new ImageItem(Enclosing_Instance.layout, "Image" + pos)); //$NON-NLS-1$
				}
				else if (action.Equals(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.ADD_SHAPE))
				{
					addItem(new ShapeItem(Enclosing_Instance.layout, "Shape" + pos)); //$NON-NLS-1$
				}
				else if (action.Equals(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.REMOVE))
				{
					if (sel >= 0)
					{
						Enclosing_Instance.layout.removeItem(sel);
						//UPGRADE_ISSUE: Method 'javax.swing.table.AbstractTableModel.fireTableRowsDeleted' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableAbstractTableModelfireTableRowsDeleted_int_int'"
						model.fireTableRowsDeleted(sel, sel);
					}
					if (Enclosing_Instance.layout.getItemCount() > 1)
					{
						if (sel >= Enclosing_Instance.layout.getItemCount())
						{
							//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionModel'"
							table.getSelectionModel().setSelectionInterval(Enclosing_Instance.layout.getItemCount() - 1, Enclosing_Instance.layout.getItemCount() - 1);
						}
						else
						{
							//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionModel'"
							table.getSelectionModel().SetSelectionInterval(sel, sel);
						}
					}
				}
				else if (action.Equals(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.UP))
				{
					if (sel > 0)
					{
						moveItem(sel, sel - 1);
					}
				}
				else if (action.Equals(VassalSharp.build.module.gamepieceimage.LayoutConfigurer.DOWN))
				{
					if (sel < pos - 1)
					{
						moveItem(sel, sel + 1);
					}
				}
				
				rebuildViz();
			}
			
			protected internal virtual void  addItem(Item item)
			{
				Enclosing_Instance.layout.addItem(item);
				int pos = Enclosing_Instance.layout.getItemCount() - 1;
				//UPGRADE_ISSUE: Method 'javax.swing.table.AbstractTableModel.fireTableRowsInserted' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableAbstractTableModelfireTableRowsInserted_int_int'"
				model.fireTableRowsInserted(pos, pos);
				//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionModel'"
				table.getSelectionModel().SetSelectionInterval(pos, pos);
			}
			
			protected internal virtual void  moveItem(int from, int to)
			{
				Enclosing_Instance.layout.moveItem(from, to);
				//UPGRADE_ISSUE: Method 'javax.swing.table.AbstractTableModel.fireTableRowsUpdated' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtableAbstractTableModelfireTableRowsUpdated_int_int'"
				model.fireTableRowsUpdated(from, to);
				//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionModel'"
				table.getSelectionModel().SetSelectionInterval(to, to);
				rebuildViz();
			}
			
			protected internal virtual void  rebuildViz()
			{
				Enclosing_Instance.layout.setImageDefn(new GamePieceImage(Enclosing_Instance.layout));
				Enclosing_Instance.visualizer.rebuild();
			}
			
			protected internal virtual void  showItem(int itemNo)
			{
				
				if (Enclosing_Instance.currentItemControls != null)
				{
					Enclosing_Instance.itemConfigPanel.Controls.Remove(Enclosing_Instance.currentItemControls);
					Enclosing_Instance.currentItemControls = null;
					Enclosing_Instance.currentItem = VassalSharp.build.module.gamepieceimage.LayoutConfigurer.NO_CURRENT_ITEM;
				}
				
				if (itemNo != VassalSharp.build.module.gamepieceimage.LayoutConfigurer.NO_CURRENT_ITEM && Enclosing_Instance.layout.getItemCount() > 0 && itemNo < Enclosing_Instance.layout.getItemCount())
				{
					Item item = Enclosing_Instance.layout.getItem(itemNo);
					Configurer c = item.Configurer;
					Enclosing_Instance.currentItemControls = c.Controls;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					Enclosing_Instance.itemConfigPanel.Controls.Add(Enclosing_Instance.currentItemControls);
					Enclosing_Instance.currentItem = itemNo;
				}
				
				reshow();
			}
			
			public virtual void  reshow()
			{
				
				Enclosing_Instance.repack();
				rebuildViz();
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Enclosing_Instance.itemConfigPanel.Refresh();
			}
			
			//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'MyTableModel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
			//UPGRADE_TODO: Class 'javax.swing.table.AbstractTableModel' was converted to 'System.Data.DataTable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			[Serializable]
			internal class MyTableModel:System.Data.DataTable
			{
				public MyTableModel(ItemPanel enclosingInstance)
				{
					InitBlock(enclosingInstance);
				}
				private void  InitBlock(ItemPanel enclosingInstance)
				{
					this.enclosingInstance = enclosingInstance;
					return typeof(System.String);
				}
				private ItemPanel enclosingInstance;
				public ItemPanel Enclosing_Instance
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
					return Enclosing_Instance.Enclosing_Instance.layout.getItemCount();
				}
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.getColumnName' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public System.String getColumnName(int col)
				{
					return columnNames[col];
				}
				
				//UPGRADE_NOTE: The equivalent of method 'javax.swing.table.AbstractTableModel.getValueAt' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
				public System.Object getValueAt(int row, int col)
				{
					if (col == 0)
					{
						return (Enclosing_Instance.Enclosing_Instance.layout.getItem(row)).getConfigureName();
					}
					else if (col == 1)
					{
						return (Enclosing_Instance.Enclosing_Instance.layout.getItem(row)).getDisplayName();
					}
					else if (col == 2)
					{
						return (Enclosing_Instance.Enclosing_Instance.layout.getItem(row)).getLocation();
					}
					else
						return null;
				}
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				public Class < String > getColumnClass(int c)
			}
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'NewIntConfigurer' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		protected internal class NewIntConfigurer:IntConfigurer
		{
			private void  InitBlock(LayoutConfigurer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private LayoutConfigurer enclosingInstance;
			virtual public int Columns
			{
				set
				{
					//UPGRADE_ISSUE: Method 'javax.swing.JTextField.setColumns' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTextFieldsetColumns_int'"
					nameField.setColumns(value);
				}
				
			}
			public LayoutConfigurer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			internal NewIntConfigurer(LayoutConfigurer enclosingInstance, System.String name, System.String key, ref System.Int32 i):base(name, key, ref i)
			{
				InitBlock(enclosingInstance);
			}
			
			public virtual int getIntValue()
			{
				return ((System.Int32) getValue());
			}
		}
	}
}