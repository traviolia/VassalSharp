/*
* $Id$
*
* Copyright (c) 2005 by Rodney Kinney
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
using Board = VassalSharp.build.module.map.boardPicker.Board;
using GridContainer = VassalSharp.build.module.map.boardPicker.board.mapgrid.GridContainer;
using GridNumbering = VassalSharp.build.module.map.boardPicker.board.mapgrid.GridNumbering;
using RegularGridNumbering = VassalSharp.build.module.map.boardPicker.board.mapgrid.RegularGridNumbering;
using Resources = VassalSharp.i18n.Resources;
using AdjustableSpeedScrollPane = VassalSharp.tools.AdjustableSpeedScrollPane;
namespace VassalSharp.build.module.map.boardPicker.board
{
	
	[Serializable]
	public abstract class GridEditor:System.Windows.Forms.Form
	{
		static private System.Int32 state142;
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(GridEditor enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(GridEditor enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GridEditor enclosingInstance;
			public GridEditor Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs we)
			{
				we.Cancel = true;
				Enclosing_Instance.cancel();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener
		{
			public AnonymousClassActionListener(GridEditor enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(GridEditor enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GridEditor enclosingInstance;
			public GridEditor Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.cancelSetMode();
				Enclosing_Instance.Visible = false;
				/*
				GameModule.getGameModule()
				.getDataArchive().clearTransformedImageCache();*/
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener1' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener1
		{
			public AnonymousClassActionListener1(GridEditor enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(GridEditor enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GridEditor enclosingInstance;
			public GridEditor Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.cancel();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener2' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener2
		{
			public AnonymousClassActionListener2(GridEditor enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(GridEditor enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GridEditor enclosingInstance;
			public GridEditor Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.startSetMode();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener3' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener3
		{
			public AnonymousClassActionListener3(GridEditor enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(GridEditor enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GridEditor enclosingInstance;
			public GridEditor Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				Enclosing_Instance.cancelSetMode();
			}
		}
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassActionListener4' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassActionListener4
		{
			public AnonymousClassActionListener4(GridEditor enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(GridEditor enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GridEditor enclosingInstance;
			public GridEditor Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
			{
				((RegularGridNumbering) Enclosing_Instance.grid.getGridNumbering()).setAttribute(RegularGridNumbering.VISIBLE, Boolean.valueOf(!Enclosing_Instance.grid.getGridNumbering().Visible));
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Enclosing_Instance.Refresh();
			}
		}
		private static void  keyDown(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			state142 = ((int) System.Windows.Forms.Control.MouseButtons  | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		private static void  mouseDown(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			state142 = ((int) e.Button | (int) System.Windows.Forms.Control.ModifierKeys);
		}
		virtual protected internal System.Drawing.Point NewOrigin
		{
			set
			{
				
				//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
				int width = (int) System.Math.Round(grid.Dx);
				//UPGRADE_TODO: Method 'java.lang.Math.round' was converted to 'System.Math.Round' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javalangMathround_double'"
				int height = (int) System.Math.Round(grid.Dy);
				
				if (value.X < (- width))
				{
					value.X += width;
				}
				else if (value.X > width)
				{
					value.X -= width;
				}
				
				if (value.Y < (- height))
				{
					value.Y += height;
				}
				else if (value.Y > height)
				{
					value.Y -= height;
				}
				
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				grid.setOrigin(ref value);
			}
			
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_NOTE: Final was removed from the declaration of 'SET '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'SET' was moved to static method 'VassalSharp.build.module.map.boardPicker.board.GridEditor'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String SET; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'CANCEL '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'CANCEL' was moved to static method 'VassalSharp.build.module.map.boardPicker.board.GridEditor'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String CANCEL;
		//UPGRADE_NOTE: Final was removed from the declaration of 'CANCEL_SET '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'CANCEL_SET' was moved to static method 'VassalSharp.build.module.map.boardPicker.board.GridEditor'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String CANCEL_SET; //$NON-NLS-1$
		//UPGRADE_NOTE: Final was removed from the declaration of 'OK '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'OK' was moved to static method 'VassalSharp.build.module.map.boardPicker.board.GridEditor'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String OK;
		//UPGRADE_NOTE: Final was removed from the declaration of 'NUMBERING '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
		//UPGRADE_NOTE: The initialization of  'NUMBERING' was moved to static method 'VassalSharp.build.module.map.boardPicker.board.GridEditor'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal static readonly System.String NUMBERING; //$NON-NLS-1$
		
		protected internal GridEditor.EditableGrid grid;
		protected internal Board board;
		
		protected internal System.Windows.Forms.Panel view;
		//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		protected internal System.Windows.Forms.ScrollableControl scroll;
		
		protected internal bool setMode;
		protected internal System.Drawing.Point hp1
		{
			get
			{
				return hp1_Renamed;
			}
			
			set
			{
				hp1_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Point hp2
		{
			get
			{
				return hp2_Renamed;
			}
			
			set
			{
				hp2_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Point hp3
		{
			get
			{
				return hp3_Renamed;
			}
			
			set
			{
				hp3_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Point hp1_Renamed, hp2_Renamed, hp3_Renamed;
		
		protected internal System.Windows.Forms.Button okButton, canSetButton, setButton, numberingButton;
		
		protected internal bool saveGridVisible, saveNumberingVisible;
		protected internal double saveDx, saveDy;
		protected internal System.Drawing.Point saveOrigin
		{
			get
			{
				return saveOrigin_Renamed;
			}
			
			set
			{
				saveOrigin_Renamed = value;
			}
			
		}
		protected internal System.Drawing.Point saveOrigin_Renamed;
		
		//UPGRADE_ISSUE: Constructor 'javax.swing.JDialog.JDialog' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJDialogJDialog'"
		public GridEditor(GridEditor.EditableGrid grid):base()
		{
			setTitle(Resources.getString("Editor.ModuleEditor.edit", grid.GridName)); //$NON-NLS-1$
			//UPGRADE_ISSUE: Method 'java.awt.Dialog.setModal' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtDialogsetModal_boolean'"
			setModal(true);
			this.grid = grid;
			GridContainer container = grid.getContainer();
			if (container != null)
			{
				board = container.getBoard();
			}
			saveGridVisible = grid.Visible;
			if (grid.getGridNumbering() != null)
			{
				saveNumberingVisible = grid.getGridNumbering().Visible;
				// if (saveGridVisible) {
				//  ((RegularGridNumbering) grid.getGridNumbering()).setAttribute(RegularGridNumbering.VISIBLE, Boolean.FALSE);
				// }
			}
			
			saveDx = grid.Dx;
			saveDy = grid.Dy;
			saveOrigin = grid.getOrigin();
			
			initComponents();
		}
		
		protected internal virtual void  initComponents()
		{
			Closing += new System.ComponentModel.CancelEventHandler(this.GridEditor_Closing_DO_NOTHING_ON_CLOSE);
			//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
			Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
			
			view = new GridPanel(this, board);
			
			view.MouseDown += new System.Windows.Forms.MouseEventHandler(VassalSharp.build.module.map.boardPicker.board.GridEditor.mouseDown);
			view.Click += new System.EventHandler(this.mouseClicked);
			view.MouseEnter += new System.EventHandler(this.mouseEntered);
			view.MouseLeave += new System.EventHandler(this.mouseExited);
			view.MouseDown += new System.Windows.Forms.MouseEventHandler(this.mousePressed);
			view.MouseUp += new System.Windows.Forms.MouseEventHandler(this.mouseReleased);
			view.KeyDown += new System.Windows.Forms.KeyEventHandler(VassalSharp.build.module.map.boardPicker.board.GridEditor.keyDown);
			view.KeyDown += new System.Windows.Forms.KeyEventHandler(this.keyPressed);
			view.KeyUp += new System.Windows.Forms.KeyEventHandler(this.keyReleased);
			view.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.keyTyped);
			view.setFocusable(true);
			
			//UPGRADE_ISSUE: Field 'javax.swing.ScrollPaneConstants.VERTICAL_SCROLLBAR_ALWAYS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingScrollPaneConstants'"
			//UPGRADE_ISSUE: Field 'javax.swing.ScrollPaneConstants.HORIZONTAL_SCROLLBAR_ALWAYS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingScrollPaneConstants'"
			scroll = new AdjustableSpeedScrollPane(view, JScrollPane.VERTICAL_SCROLLBAR_ALWAYS, JScrollPane.HORIZONTAL_SCROLLBAR_ALWAYS);
			
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			scroll.Size = new System.Drawing.Size(800, 600);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(scroll);
			scroll.Dock = System.Windows.Forms.DockStyle.Fill;
			scroll.BringToFront();
			
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box textPanel = Box.createVerticalBox();
			System.Windows.Forms.Label temp_label2;
			temp_label2 = new System.Windows.Forms.Label();
			temp_label2.Text = Resources.getString("Editor.GridEditor.arrow_keys");
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = temp_label2;
			textPanel.Controls.Add(temp_Control); //$NON-NLS-1$
			System.Windows.Forms.Label temp_label4;
			temp_label4 = new System.Windows.Forms.Label();
			temp_label4.Text = Resources.getString("Editor.GridEditor.control_arrow_keys");
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control2;
			temp_Control2 = temp_label4;
			textPanel.Controls.Add(temp_Control2); //$NON-NLS-1$
			System.Windows.Forms.Label temp_label6;
			temp_label6 = new System.Windows.Forms.Label();
			temp_label6.Text = Resources.getString("Editor.GridEditor.shift_key");
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control3;
			temp_Control3 = temp_label6;
			textPanel.Controls.Add(temp_Control3); //$NON-NLS-1$
			
			System.Windows.Forms.Panel buttonPanel = new System.Windows.Forms.Panel();
			
			okButton = SupportClass.ButtonSupport.CreateStandardButton(OK);
			okButton.Click += new System.EventHandler(new AnonymousClassActionListener(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(okButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonPanel.Controls.Add(okButton);
			
			System.Windows.Forms.Button canButton = SupportClass.ButtonSupport.CreateStandardButton(CANCEL);
			canButton.Click += new System.EventHandler(new AnonymousClassActionListener1(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(canButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonPanel.Controls.Add(canButton);
			
			setButton = SupportClass.ButtonSupport.CreateStandardButton(SET);
			setButton.Click += new System.EventHandler(new AnonymousClassActionListener2(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(setButton);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setRequestFocusEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetRequestFocusEnabled_boolean'"
			setButton.setRequestFocusEnabled(false);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonPanel.Controls.Add(setButton);
			
			canSetButton = SupportClass.ButtonSupport.CreateStandardButton(CANCEL_SET);
			canSetButton.Click += new System.EventHandler(new AnonymousClassActionListener3(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(canSetButton);
			canSetButton.Visible = false;
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setRequestFocusEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetRequestFocusEnabled_boolean'"
			canSetButton.setRequestFocusEnabled(false);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonPanel.Controls.Add(canSetButton);
			
			
			numberingButton = SupportClass.ButtonSupport.CreateStandardButton(NUMBERING);
			numberingButton.Click += new System.EventHandler(new AnonymousClassActionListener4(this).actionPerformed);
			SupportClass.CommandManager.CheckCommand(numberingButton);
			numberingButton.Enabled = grid.getGridNumbering() != null;
			numberingButton.Visible = true;
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setRequestFocusEnabled' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetRequestFocusEnabled_boolean'"
			numberingButton.setRequestFocusEnabled(false);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonPanel.Controls.Add(numberingButton);
			
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box controlPanel = Box.createVerticalBox();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			controlPanel.Controls.Add(textPanel);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			controlPanel.Controls.Add(buttonPanel);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent_javalangObject'"
			Controls.Add(controlPanel);
			controlPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			controlPanel.SendToBack();
			
			scroll.Invalidate();
			//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
			pack();
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			Refresh();
		}
		
		protected internal virtual void  cancel()
		{
			cancelSetMode();
			grid.Dx = saveDx;
			grid.Dy = saveDy;
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			grid.setOrigin(ref saveOrigin);
			//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
			//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
			Visible = false;
		}
		
		protected internal virtual void  cancelSetMode()
		{
			canSetButton.Visible = false;
			setButton.Visible = true;
			//UPGRADE_ISSUE: Member 'java.awt.Cursor.getPredefinedCursor' was converted to 'System.Windows.Forms.Cursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
			//UPGRADE_ISSUE: Member 'java.awt.Cursor.DEFAULT_CURSOR' was converted to 'System.Windows.Forms.Cursors.Default' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
			view.Cursor = System.Windows.Forms.Cursors.Default;
			setMode = false;
			grid.Visible = saveGridVisible;
			if (grid.getGridNumbering() != null && saveNumberingVisible)
			{
				((RegularGridNumbering) grid.getGridNumbering()).setAttribute(RegularGridNumbering.VISIBLE, Boolean.valueOf(saveNumberingVisible));
			}
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			Refresh();
		}
		
		protected internal virtual void  startSetMode()
		{
			hp1 = System.Drawing.Point.Empty;
			hp2 = System.Drawing.Point.Empty;
			hp3 = System.Drawing.Point.Empty;
			setMode = true;
			canSetButton.Visible = true;
			setButton.Visible = false;
			//UPGRADE_ISSUE: Member 'java.awt.Cursor.getPredefinedCursor' was converted to 'System.Windows.Forms.Cursor' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
			//UPGRADE_ISSUE: Member 'java.awt.Cursor.CROSSHAIR_CURSOR' was converted to 'System.Windows.Forms.Cursors.Cross' which cannot be assigned to an int. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1086'"
			view.Cursor = System.Windows.Forms.Cursors.Cross;
			grid.Visible = false;
			SupportClass.OptionPaneSupport.ShowMessageDialog(null, Resources.getString("Editor.GridEditor.click_on_3")); //$NON-NLS-1$
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			Refresh();
		}
		
		public virtual void  keyPressed(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (setMode)
			{
				return ;
			}
			
			bool sideways = grid.Sideways;
			
			//UPGRADE_TODO: Method 'java.awt.event.KeyEvent.getKeyCode' was converted to 'System.Windows.Forms.KeyEventArgs.KeyValue' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawteventKeyEventgetKeyCode'"
			switch (e.KeyValue)
			{
				
				case (int) System.Windows.Forms.Keys.Up: 
					if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control))
					{
						if (sideways)
						{
							adjustDx(event_sender, - 1, e);
						}
						else
						{
							adjustDy(event_sender, - 1, e);
						}
					}
					else
					{
						if (sideways)
						{
							adjustX0(event_sender, - 1, e);
						}
						else
						{
							adjustY0(event_sender, - 1, e);
						}
					}
					break;
				
				case (int) System.Windows.Forms.Keys.Down: 
					if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control))
					{
						if (sideways)
						{
							adjustDx(event_sender, 1, e);
						}
						else
						{
							adjustDy(event_sender, 1, e);
						}
					}
					else
					{
						if (sideways)
						{
							adjustX0(event_sender, 1, e);
						}
						else
						{
							adjustY0(event_sender, 1, e);
						}
					}
					break;
				
				case (int) System.Windows.Forms.Keys.Left: 
					if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control))
					{
						if (sideways)
						{
							adjustDy(event_sender, - 1, e);
						}
						else
						{
							adjustDx(event_sender, - 1, e);
						}
					}
					else
					{
						if (sideways)
						{
							adjustY0(event_sender, - 1, e);
						}
						else
						{
							adjustX0(event_sender, - 1, e);
						}
					}
					break;
				
				case (int) System.Windows.Forms.Keys.Right: 
					if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Control))
					{
						if (sideways)
						{
							adjustDy(event_sender, 1, e);
						}
						else
						{
							adjustDx(event_sender, 1, e);
						}
					}
					else
					{
						if (sideways)
						{
							adjustY0(event_sender, 1, e);
						}
						else
						{
							adjustX0(event_sender, 1, e);
						}
					}
					break;
				
				default: 
					return ;
				
			}
			
			
			//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
			Refresh();
			e.Handled = true;
		}
		
		public virtual void  rebuild()
		{
			
		}
		
		public virtual void  keyReleased(System.Object event_sender, System.Windows.Forms.KeyEventArgs e)
		{
			rebuild();
		}
		
		public virtual void  keyTyped(System.Object event_sender, System.Windows.Forms.KeyPressEventArgs e)
		{
		}
		
		public virtual void  mouseClicked(System.Object event_sender, System.EventArgs e)
		{
			if (setMode)
			{
				if (hp1.IsEmpty)
				{
					//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
					hp1 = e.getPoint();
				}
				else if (hp2.IsEmpty)
				{
					//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
					hp2 = e.getPoint();
				}
				else if (hp3.IsEmpty)
				{
					//UPGRADE_ISSUE: Method 'java.awt.event.MouseEvent.getPoint' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawteventMouseEventgetPoint'"
					hp3 = e.getPoint();
					calculate();
					cancelSetMode();
				}
				//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
				Refresh();
			}
		}
		
		public virtual void  mouseEntered(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mouseExited(System.Object event_sender, System.EventArgs e)
		{
		}
		
		public virtual void  mousePressed(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
		}
		
		public virtual void  mouseReleased(System.Object event_sender, System.Windows.Forms.MouseEventArgs e)
		{
			
		}
		
		protected internal const int DELTA = 1;
		protected internal const double DDELTA = 0.1;
		protected internal const int FAST = 5;
		protected internal const int ERROR_MARGIN = 5;
		
		//UPGRADE_TODO: The equivalent of method adjustX0 needs to be overloaded, to match all the event parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1142'"
		protected internal virtual void  adjustX0(System.Object event_sender, int direction, System.Windows.Forms.KeyEventArgs e)
		{
			int delta = direction * DELTA;
			if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift))
			{
				delta *= FAST;
			}
			System.Drawing.Point p = grid.getOrigin();
			NewOrigin = new System.Drawing.Point(p.X + delta, p.Y);
		}
		
		//UPGRADE_TODO: The equivalent of method adjustY0 needs to be overloaded, to match all the event parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1142'"
		protected internal virtual void  adjustY0(System.Object event_sender, int direction, System.Windows.Forms.KeyEventArgs e)
		{
			int delta = direction * DELTA;
			if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift))
			{
				delta *= FAST;
			}
			System.Drawing.Point p = grid.getOrigin();
			NewOrigin = new System.Drawing.Point(p.X, p.Y + delta);
		}
		
		//UPGRADE_TODO: The equivalent of method adjustDx needs to be overloaded, to match all the event parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1142'"
		protected internal virtual void  adjustDx(System.Object event_sender, int direction, System.Windows.Forms.KeyEventArgs e)
		{
			double delta = direction * DDELTA;
			if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift))
			{
				delta *= FAST;
			}
			grid.Dx = grid.Dx + delta;
		}
		
		//UPGRADE_TODO: The equivalent of method adjustDy needs to be overloaded, to match all the event parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1142'"
		protected internal virtual void  adjustDy(System.Object event_sender, int direction, System.Windows.Forms.KeyEventArgs e)
		{
			double delta = direction * DDELTA;
			if ((System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Shift))
			{
				delta *= FAST;
			}
			grid.Dy = grid.Dy + delta;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual bool isHorizontal(ref System.Drawing.Point p1, ref System.Drawing.Point p2)
		{
			return System.Math.Abs(p2.Y - p1.Y) <= ERROR_MARGIN;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual bool isVertical(ref System.Drawing.Point p1, ref System.Drawing.Point p2)
		{
			return System.Math.Abs(p2.X - p1.X) <= ERROR_MARGIN;
		}
		
		//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
		protected internal virtual bool isPerpendicular(ref System.Drawing.Point p1, ref System.Drawing.Point p2)
		{
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			return isHorizontal(ref p1, ref p2) || isVertical(ref p1, ref p2);
		}
		
		protected internal virtual void  reportShapeError()
		{
			SupportClass.OptionPaneSupport.ShowMessageDialog(null, Resources.getString("Editor.GridEditor.does_not_look", grid.GridName), Resources.getString("Editor.GridEditor.grid_shape_error"), (int) System.Windows.Forms.MessageBoxIcon.Error);
		}
		
		/*
		* Calculate and set the Origin and size of the grid
		* based on the the 3 selected points.
		*/
		public abstract void  calculate();
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'GridPanel' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		/*
		* Panel to display the Grid Editor
		*/
		[Serializable]
		protected internal class GridPanel:System.Windows.Forms.Panel
		{
			private void  InitBlock(GridEditor enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private GridEditor enclosingInstance;
			virtual public Board Board
			{
				get
				{
					return board;
				}
				
				set
				{
					board = value;
					//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_javaawtDimension'"
					Size = board.Size;
					//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					Size = board.Size;
				}
				
			}
			virtual public bool Focusable
			{
				get
				{
					return true;
				}
				
			}
			public GridEditor Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			
			private const long serialVersionUID = 1L;
			protected internal Board board;
			
			public GridPanel(GridEditor enclosingInstance):base()
			{
				InitBlock(enclosingInstance);
				setFocusTraversalKeysEnabled(false);
			}
			
			public GridPanel(GridEditor enclosingInstance, Board b):this(enclosingInstance)
			{
				Board = b;
			}
			
			protected override void  OnPaint(System.Windows.Forms.PaintEventArgs g_EventArg)
			{
				System.Drawing.Graphics g = null;
				if (g_EventArg != null)
					g = g_EventArg.Graphics;
				if (board != null)
				{
					//UPGRADE_TODO: Method 'javax.swing.JComponent.getVisibleRect' was converted to 'System.Windows.Forms.Control.DisplayRectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentgetVisibleRect'"
					System.Drawing.Rectangle b = DisplayRectangle;
					g.FillRegion(new System.Drawing.SolidBrush(SupportClass.GraphicsManager.manager.GetBackColor(g)), new System.Drawing.Region(new System.Drawing.Rectangle(b.X, b.Y, b.Width, b.Height)));
					board.draw(g, 0, 0, 1.0, this);
					if (Enclosing_Instance.setMode)
					{
						//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
						highlight(g, ref Enclosing_Instance.hp1);
						//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
						highlight(g, ref Enclosing_Instance.hp2);
						//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
						highlight(g, ref Enclosing_Instance.hp3);
					}
				}
				else
				{
					base.OnPaint(g_EventArg);
				}
			}
			
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			protected internal virtual void  highlight(System.Drawing.Graphics g, ref System.Drawing.Point p)
			{
				//UPGRADE_NOTE: Final was removed from the declaration of 'r1 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int r1 = 3;
				//UPGRADE_NOTE: Final was removed from the declaration of 'r2 '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
				int r2 = 10;
				
				if (!p.IsEmpty)
				{
					SupportClass.GraphicsManager.manager.SetColor(g, System.Drawing.Color.Red);
					g.FillEllipse(SupportClass.GraphicsManager.manager.GetPaint(g), p.X - r1 / 2, p.Y - r1 / 2, r1, r1);
					g.DrawEllipse(SupportClass.GraphicsManager.manager.GetPen(g), p.X - r2 / 2, p.Y - r2 / 2, r2, r2);
				}
			}
		}
		
		
		/*
		* Interface to be implemented by a class that wants to be edited
		* by RegularGridEditor
		*/
		public interface EditableGrid
		{
			double Dx
			{
				get;
				
				set;
				
			}
			double Dy
			{
				get;
				
				set;
				
			}
			bool Sideways
			{
				get;
				
				set;
				
			}
			bool Visible
			{
				get;
				
				set;
				
			}
			System.String GridName
			{
				get;
				
			}
			System.Drawing.Point getOrigin();
			//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
			void  setOrigin(ref System.Drawing.Point p);
			
			GridContainer getContainer();
			GridNumbering getGridNumbering();
		}
		private void  GridEditor_Closing_DO_NOTHING_ON_CLOSE(System.Object sender, System.ComponentModel.CancelEventArgs  e)
		{
			e.Cancel = true;
			SupportClass.CloseOperation((System.Windows.Forms.Form) sender, 0);
		}
		static GridEditor()
		{
			SET = Resources.getString("Editor.GridEditor.set_grid_shape");
			CANCEL = Resources.getString(Resources.CANCEL);
			CANCEL_SET = Resources.getString("Editor.GridEditor.cancel_set");
			OK = Resources.getString(Resources.SAVE);
			NUMBERING = Resources.getString("Editor.GridEditor.numbering");
		}
	}
}