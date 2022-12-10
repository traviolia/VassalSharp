/*
* $Id$
*
* Copyright (c) 2000-2009 by Rodney Kinney, Brent Easton
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
//UPGRADE_TODO: The type 'java.util.concurrent.ExecutionException' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using ExecutionException = java.util.concurrent.ExecutionException;
//UPGRADE_TODO: The type 'org.jdesktop.swingworker.SwingWorker' could not be found. If it was not included in the conversion, there may be compiler issues. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1262'"
using SwingWorker = org.jdesktop.swingworker.SwingWorker;
using Player = VassalSharp.chat.Player;
using ServerStatus = VassalSharp.chat.ServerStatus;
using SimpleRoom = VassalSharp.chat.SimpleRoom;
using Resources = VassalSharp.i18n.Resources;
using ErrorDialog = VassalSharp.tools.ErrorDialog;
namespace VassalSharp.chat.ui
{
	
	/// <summary> Shows the current status of connections to the server</summary>
	[Serializable]
	public class ServerStatusView:System.Windows.Forms.TabControl
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(ServerStatusView enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerStatusView enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerStatusView enclosingInstance;
			public ServerStatusView Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.refresh();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassTreeExpansionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassTreeExpansionListener
		{
			public AnonymousClassTreeExpansionListener(ServerStatusView enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(ServerStatusView enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private ServerStatusView enclosingInstance;
			public ServerStatusView Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  treeExpanded(System.Object event_sender, System.Windows.Forms.TreeViewEventArgs event_Renamed)
			{
				System.Windows.Forms.Control c = (System.Windows.Forms.Control) event_sender;
				//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_javaawtDimension'"
				//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				c.Size = (c is VassalSharp.tools.ComponentSplitter.SplitPane || c is VassalSharp.counters.Obscurable.Ed.AnonymousClassJPanel || c is VassalSharp.counters.NonRectangular.Ed.AnonymousClassJPanel || c is VassalSharp.build.widget.PieceSlot.Panel || c is VassalSharp.build.module.map.View || c is VassalSharp.build.module.map.boardPicker.board.Config.View || c is VassalSharp.build.module.map.boardPicker.board.mapgrid.SquareGridNumbering.AnonymousClassJPanel || c is VassalSharp.build.module.map.boardPicker.board.mapgrid.HexGridNumbering.AnonymousClassJPanel || c is org.netbeans.modules.wizard.InstructionsPanel || c is org.netbeans.api.wizard.displayer.WizardDisplayerImpl.AnonymousClassJPanel)?(System.Drawing.Size) SupportClass.InvokeMethodAsVirtual(c, "getPreferredSize", new System.Object[]{}):c.Size;
				c.Invalidate();
			}
			
			public virtual void  treeCollapsed(System.Object event_sender, System.Windows.Forms.TreeViewEventArgs event_Renamed)
			{
			}
		}
		private const long serialVersionUID = 1L;
		
		public const System.String SELECTION_PROPERTY = "ServerStatusView.selection"; //$NON-NLS-1$
		private ServerStatus status;
		//UPGRADE_TODO: Class 'javax.swing.tree.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Windows.Forms.TreeNode model;
		//UPGRADE_TODO: Class 'javax.swing.tree.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Windows.Forms.TreeNode[] historicalModels;
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Windows.Forms.TreeView treeCurrent;
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Windows.Forms.TreeView[] historicalTrees;
		private int totalPlayers;
		
		public ServerStatusView(ServerStatus status)
		{
			this.status = status;
			initComponents();
		}
		
		private void  initComponents()
		{
			//UPGRADE_ISSUE: Constructor 'java.awt.BorderLayout.BorderLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtBorderLayout'"
			new BorderLayout();
			//UPGRADE_TODO: Constructor 'javax.swing.JPanel.JPanel' was converted to 'System.Windows.Forms.Panel.Panel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJPanelJPanel_javaawtLayoutManager'"
			System.Windows.Forms.Panel current = new System.Windows.Forms.Panel();
			System.Windows.Forms.ToolBar temp_ToolBar;
			System.Windows.Forms.ImageList temp_ImageList;
			temp_ImageList = new System.Windows.Forms.ImageList();
			temp_ToolBar = new System.Windows.Forms.ToolBar();
			temp_ToolBar.ButtonClick += new System.Windows.Forms.ToolBarButtonClickEventHandler(SupportClass.ToolBarButtonClicked);
			temp_ToolBar.ImageList = temp_ImageList;
			System.Windows.Forms.ToolBar toolbar = temp_ToolBar;
			
			//UPGRADE_ISSUE: Method 'javax.swing.JToolBar.setFloatable' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJToolBarsetFloatable_boolean'"
			toolbar.setFloatable(false);
			System.Windows.Forms.Button b = SupportClass.ButtonSupport.CreateStandardButton(Resources.getString("Chat.refresh")); //$NON-NLS-1$
			b.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(b);
			System.Windows.Forms.ToolBarButton temp_ToolBarButton;
			temp_ToolBarButton = new System.Windows.Forms.ToolBarButton(b.Text);
			temp_ToolBarButton.ToolTipText = SupportClass.ToolTipSupport.getToolTipText(b);
			toolbar.Buttons.Add(temp_ToolBarButton);
			if (b.Image != null)
			{
				toolbar.ImageList.Images.Add(b.Image);
				temp_ToolBarButton.ImageIndex = toolbar.ImageList.Images.Count - 1;
			}
			temp_ToolBarButton.Tag = b;
			b.Tag = temp_ToolBarButton;
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			current.Controls.Add(toolbar);
			toolbar.Dock = System.Windows.Forms.DockStyle.Top;
			toolbar.SendToBack();
			treeCurrent = createTree();
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol2;
			temp_scrollablecontrol2 = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol2.AutoScroll = true;
			temp_scrollablecontrol2.Controls.Add(treeCurrent);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = temp_scrollablecontrol2;
			current.Controls.Add(temp_Control);
			temp_Control.Dock = System.Windows.Forms.DockStyle.Fill;
			temp_Control.BringToFront();
			//UPGRADE_TODO: Method 'javax.swing.JTree.getModel' was converted to 'SupportClass.TreeSupport.GetModel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTreegetModel'"
			//UPGRADE_TODO: Class 'javax.swing.tree.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			model = (System.Windows.Forms.TreeNode) SupportClass.TreeSupport.GetModel(treeCurrent);
			//UPGRADE_TODO: Method 'javax.swing.JTabbedPane.addTab' was converted to 'SupportClass.TabControlSupport.AddTab' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTabbedPaneaddTab_javalangString_javaawtComponent'"
			SupportClass.TabControlSupport.AddTab(this, Resources.getString("Chat.current"), current); //$NON-NLS-1$
			SelectedIndexChanged += new System.EventHandler(this.stateChanged);
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.border.TitledBorder.TitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingborderTitledBorder'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(CreateGraphics(), 0, 0, Width, Height, new TitledBorder(Resources.getString("Chat.server_connections"))); //$NON-NLS-1$
			setStatusServer(status);
		}
		
		private void  buildHistoricalTabs()
		{
			//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.JTabbedPane.getTabCount' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
			while (TabCount > 1)
			{
				//UPGRADE_TODO: The equivalent in .NET for method 'javax.swing.JTabbedPane.getTabCount' may return a different value. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1043'"
				TabPages.RemoveAt(TabCount - 1);
			}
			if (status != null)
			{
				System.String[] supported = status.SupportedTimeRanges;
				//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				historicalTrees = new System.Windows.Forms.TreeView[supported.Length];
				//UPGRADE_TODO: Class 'javax.swing.tree.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
				historicalModels = new System.Windows.Forms.TreeNode[supported.Length];
				for (int i = 0; i < supported.Length; i++)
				{
					historicalTrees[i] = createTree();
					//UPGRADE_TODO: Method 'javax.swing.JTree.getModel' was converted to 'SupportClass.TreeSupport.GetModel' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTreegetModel'"
					//UPGRADE_TODO: Class 'javax.swing.tree.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					historicalModels[i] = (System.Windows.Forms.TreeNode) SupportClass.TreeSupport.GetModel(historicalTrees[i]);
					//UPGRADE_TODO: Method 'javax.swing.JTabbedPane.addTab' was converted to 'SupportClass.TabControlSupport.AddTab' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTabbedPaneaddTab_javalangString_javaawtComponent'"
					//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
					System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
					temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
					temp_scrollablecontrol.AutoScroll = true;
					temp_scrollablecontrol.Controls.Add(historicalTrees[i]);
					SupportClass.TabControlSupport.AddTab(this, supported[i], temp_scrollablecontrol);
				}
			}
		}
		
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Windows.Forms.TreeView createTree()
		{
			System.Windows.Forms.TreeNode root = new System.Windows.Forms.TreeNode(Resources.getString(Resources.VASSAL).ToString());
			//UPGRADE_TODO: Class 'javax.swing.tree.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Constructor 'javax.swing.tree.DefaultTreeModel.DefaultTreeModel' was converted to 'System.Windows.Forms.TreeNode' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtreeDefaultTreeModelDefaultTreeModel_javaxswingtreeTreeNode_boolean'"
			System.Windows.Forms.TreeNode m = root;
			//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			System.Windows.Forms.TreeView tree = SupportClass.TreeSupport.CreateTreeView(m);
			//UPGRADE_ISSUE: Method 'javax.swing.tree.TreeSelectionModel.setSelectionMode' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreeSelectionModel'"
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreegetSelectionModel'"
			//UPGRADE_ISSUE: Field 'javax.swing.tree.TreeSelectionModel.SINGLE_TREE_SELECTION' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreeSelectionModel'"
			tree.getSelectionModel().setSelectionMode(TreeSelectionModel.SINGLE_TREE_SELECTION);
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.setCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetCellRenderer_javaxswingtreeTreeCellRenderer'"
			tree.setCellRenderer(new Render());
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.expandRow' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreeexpandRow_int'"
			tree.expandRow(0);
			//UPGRADE_ISSUE: Method 'javax.swing.JTree.setLargeModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreesetLargeModel_boolean'"
			tree.setLargeModel(true);
			//UPGRADE_TODO: Method 'javax.swing.JTree.setRowHeight' was converted to 'System.Windows.Forms.TreeView.ItemHeight' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJTreesetRowHeight_int'"
			tree.ItemHeight = 18; // FIXME: check whether this is necessary
			tree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.valueChanged);
			tree.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(new AnonymousClassTreeExpansionListener(this).treeCollapsed);
			tree.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(new AnonymousClassTreeExpansionListener(this).treeExpanded);
			return tree;
		}
		
		public virtual void  stateChanged(System.Object event_sender, System.EventArgs e)
		{
			if (status == null)
				return ;
			refresh(SelectedIndex);
			fireSelectionChanged();
		}
		
		public virtual void  valueChanged(System.Object event_sender, System.Windows.Forms.TreeViewEventArgs e)
		{
			fireSelectionChanged();
		}
		
		private void  fireSelectionChanged()
		{
			System.Object selection = null;
			//UPGRADE_ISSUE: Class 'javax.swing.tree.TreePath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
			TreePath path = null;
			int sel = SelectedIndex;
			switch (sel)
			{
				
				case 0: 
					//UPGRADE_ISSUE: Method 'javax.swing.JTree.getSelectionPath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreegetSelectionPath'"
					path = treeCurrent.getSelectionPath();
					break;
				
				default: 
					//UPGRADE_ISSUE: Method 'javax.swing.JTree.getSelectionPath' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTreegetSelectionPath'"
					path = historicalTrees[sel - 1].getSelectionPath();
					break;
				
			}
			if (path != null)
			{
				//UPGRADE_ISSUE: Method 'javax.swing.tree.TreePath.getLastPathComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeTreePath'"
				selection = path.getLastPathComponent();
			}
			if (selection is System.Windows.Forms.TreeNode)
			{
				selection = ((System.Windows.Forms.TreeNode) selection).Tag;
			}
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.firePropertyChange' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentfirePropertyChange_javalangString_javalangObject_javalangObject'"
			firePropertyChange(SELECTION_PROPERTY, null, selection);
		}
		
		public virtual void  refresh()
		{
			refresh(0);
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private SwingWorker < ServerStatus.ModuleSummary [], Void > cur_request = null;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private SwingWorker < ServerStatus.ModuleSummary [], Void > hist_request = null;
		
		private void  refresh(int page)
		{
			if (page == 0)
			{
				if (cur_request != null && !cur_request.isDone())
					return ;
				
				//UPGRADE_ISSUE: Member 'java.awt.Cursor.getPredefinedCursor' was converted to 'System.Windows.Forms.Cursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
				//UPGRADE_ISSUE: Member 'java.awt.Cursor.WAIT_CURSOR' was converted to 'System.Windows.Forms.Cursors.WaitCursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
				Cursor = System.Windows.Forms.Cursors.WaitCursor;
				
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				cur_request = new SwingWorker < ServerStatus.ModuleSummary [], Void >()
				{
					//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
					Override
				}
			}
		}
		public virtual VassalSharp.chat.ModuleSummary[] doInBackground()
		{
			return status.Status;
		}
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		Override
		protected internal virtual void  done()
		{
			try
			{
				if (SelectedIndex == 0)
				{
					refresh(model, get_Renamed());
					fireSelectionChanged();
				}
			}
			catch (System.Threading.ThreadInterruptedException ex)
			{
				ErrorDialog.bug(ex);
			}
			// FIXME: review error message
			catch (ExecutionException ex)
			{
				ex.printStackTrace();
			}
			
			if (hist_request == null || hist_request.isDone())
			{
				//UPGRADE_ISSUE: Member 'java.awt.Cursor.getPredefinedCursor' was converted to 'System.Windows.Forms.Cursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
				//UPGRADE_ISSUE: Member 'java.awt.Cursor.DEFAULT_CURSOR' was converted to 'System.Windows.Forms.Cursors.Default' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
				Cursor = System.Windows.Forms.Cursors.Default;
			}
			
			cur_request = null;
		}
	}
	
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	cur_request.execute();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		if(hist_request != null && !hist_request.isDone()) return;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	setCursor(Cursor.getPredefinedCursor(Cursor.WAIT_CURSOR));
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	hist_request = new SwingWorker < ServerStatus.ModuleSummary [], Void >()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		@ Override
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public ServerStatus.ModuleSummary [] doInBackground()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		return status.getHistory(getTitleAt(page));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	@ Override
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	protected
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void done()
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final int sel = getSelectedIndex();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(sel == page)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// page didn't change, refresh with what we computed
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	try
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		refresh(historicalModels [sel - 1], get());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	// FIXME: review error message
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(InterruptedException ex)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ex.printStackTrace();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	// FIXME: review error message
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	catch(ExecutionException ex)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		ex.printStackTrace();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	fireSelectionChanged();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(sel != 0)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{
	// page changed, refresh that page instead
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	hist_request = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	refresh(sel);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	if(cur_request == null || cur_request.isDone()) 
	setCursor(Cursor.getPredefinedCursor(Cursor.DEFAULT_CURSOR));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	};
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	hist_request.execute();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void refresh(DefaultTreeModel m, 
	ServerStatus.ModuleSummary [] modules)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final MutableTreeNode root =(MutableTreeNode) m.getRoot();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	totalPlayers = 0;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	while(root.getChildCount() > 0)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		m.removeNodeFromParent((MutableTreeNode) root.getChildAt(0));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	if(modules.length == 0)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final DefaultMutableTreeNode n = new DefaultMutableTreeNode(
		Resources.getString(Chat.no_connections)); //$NON-NLS-1$
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	n.setAllowsChildren(false);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		for(ServerStatus.ModuleSummary s: modules)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		m.insertNodeInto(createNode(s), root, root.getChildCount());
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	// append total number of players on server to root node
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	root.setUserObject(
	Resources.getString(Resources.VASSAL) +  ( + totalPlayers + ));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	private DefaultMutableTreeNode createNode(Object o)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		Object [] children = null;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(o instanceof ServerStatus.ModuleSummary)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final ServerStatus.ModuleSummary ms =(ServerStatus.ModuleSummary) o;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	children = ms.getRooms();
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final int players = ms.numPlayers();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	ms.setModuleName(ms.getModuleName() +  ( + players + ));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	totalPlayers += players;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	else if(o instanceof SimpleRoom)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		final SimpleRoom r =(SimpleRoom) o;
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	final List < Player > l = r.getPlayerList();
	
	// append the number of players to each room name
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	r.setName(r.getName() +  ( + l.size() + ));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	children = l.toArray(new Player [l.size()]);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	final DefaultMutableTreeNode node = new DefaultMutableTreeNode(o);
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	if(children != null)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		for(Object c: children)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		node.add(createNode(c));
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	} 
	
	node.setAllowsChildren(children != null);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	return node;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	
	//UPGRADE_ISSUE: Class 'javax.swing.tree.DefaultTreeCellRenderer' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
	[Serializable]
	public class Render:DefaultTreeCellRenderer
	{
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Class 'javax.swing.JTree' was converted to 'System.Windows.Forms.TreeView' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		public System.Windows.Forms.Control getTreeCellRendererComponent(System.Windows.Forms.TreeView tree, System.Object value_Renamed, bool sel, bool expanded, bool leaf, int row, bool hasFocus)
		{
			//UPGRADE_ISSUE: Method 'javax.swing.tree.DefaultTreeCellRenderer.getTreeCellRendererComponent' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingtreeDefaultTreeCellRenderer'"
			base.getTreeCellRendererComponent(tree, value_Renamed, sel, expanded, leaf, row, hasFocus);
			if (leaf)
			{
				Image = null;
			}
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.putClientProperty' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentputClientProperty_javalangObject_javalangObject'"
			putClientProperty(Resources.getString("ServerStatusView.4"), true); //$NON-NLS-1$
			return this;
		}
	}
	
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	public
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	void setStatusServer(ServerStatus status)
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	{ 
		this.status = status;
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	buildHistoricalTabs();
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	setEnabled(status != null);
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
	//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
	}
}