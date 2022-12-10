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
using BoardPicker = VassalSharp.build.module.map.BoardPicker;
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.build.module.map.boardPicker
{
	
	[Serializable]
	public class BoardSlot:System.Windows.Forms.Panel, System.Drawing.Image
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassItemListener' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassItemListener
		{
			public AnonymousClassItemListener(BoardSlot enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(BoardSlot enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private BoardSlot enclosingInstance;
			public BoardSlot Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  itemStateChanged(System.Object event_sender, System.EventArgs e)
			{
				if (event_sender is System.Windows.Forms.MenuItem)
					((System.Windows.Forms.MenuItem) event_sender).Checked = !((System.Windows.Forms.MenuItem) event_sender).Checked;
				if (Enclosing_Instance.Board != null)
				{
					Enclosing_Instance.Board.setReversed(Enclosing_Instance.reverseCheckBox.Checked);
					Enclosing_Instance.picker.repaint();
				}
			}
		}
		private void  InitBlock()
		{
			prompt = Resources.getString("BoardPicker.select_board");
		}
		virtual public Board Board
		{
			get
			{
				return board;
			}
			
			set
			{
				board = value;
				if (value != null)
				{
					reverseCheckBox.Visible = "true".Equals(value.getAttributeValueString(Board.REVERSIBLE)); //$NON-NLS-1$
					reverseCheckBox.setSelected(value.isReversed());
					
					board = value;
					
					//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_javaawtDimension'"
					//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					Size = Size;
					Invalidate();
					//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
					Refresh();
					
					// FIXME: do something in case the image fails to load
					/*
					picker.warn(Resources.getString("BoardPicker.loading", b.getLocalizedName())); //$NON-NLS-1$
					final javax.swing.Timer t = new javax.swing.Timer(1000, new ActionListener() {
					boolean toggle = false;
					
					public void actionPerformed(ActionEvent evt) {
					if (toggle) {
					picker.warn(Resources.getString("BoardPicker.loading", b.getLocalizedName())); //$NON-NLS-1$
					}
					else {
					picker.warn(Resources.getString("BoardPicker.loading2", b.getLocalizedName())); //$NON-NLS-1$
					}
					toggle = !toggle;
					}
					});
					new BackgroundTask() {
					public void doFirst() {
					//          if (board != null) {
					//            board.fixImage();
					//          }
					}
					
					public void doLater() {
					picker.warn(Resources.getString("BoardPicker.loaded", b.getLocalizedName())); //$NON-NLS-1$
					t.stop();
					setSize(getPreferredSize());
					revalidate();
					repaint();
					}
					}.start();
					t.start();*/
				}
				else
				{
					reverseCheckBox.Visible = false;
					// FIXME: does the order of these three matter? They're not the same above?
					Invalidate();
					//UPGRADE_TODO: Method 'java.awt.Component.setSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetSize_javaawtDimension'"
					//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					Size = Size;
					//UPGRADE_TODO: Method 'java.awt.Component.repaint' was converted to 'System.Windows.Forms.Control.Refresh' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentrepaint'"
					Refresh();
				}
			}
			
		}
		//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
		new virtual public int Height
		{
			// FIXME: This is confusing. The Icon should be an internal object.
			
			get
			{
				if (board != null)
				{
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					return (int) (picker.SlotScale * board.bounds().height);
				}
				else if (this == picker.getSlot(0) || picker.getSlot(0) == null)
				{
					return picker.DefaultSlotSize.Height;
				}
				else
				{
					return picker.getSlot(0).getIconHeight();
				}
			}
			
		}
		//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
		new virtual public int Width
		{
			get
			{
				if (board != null)
				{
					//UPGRADE_WARNING: Data types in Visual C# might be different.  Verify the accuracy of narrowing conversions. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1042'"
					return (int) (picker.SlotScale * board.bounds().width);
				}
				else if (this == picker.getSlot(0) || picker.getSlot(0) == null)
				{
					return picker.DefaultSlotSize.Width;
				}
				else
				{
					return picker.getSlot(0).getIconWidth();
				}
			}
			
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_NOTE: The initialization of  'prompt' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		private System.String prompt; //$NON-NLS-1$
		
		protected internal BoardPicker picker;
		protected internal Board board = null;
		
		protected internal System.Windows.Forms.ComboBox boards;
		protected internal System.Windows.Forms.CheckBox reverseCheckBox;
		
		public BoardSlot(BoardPicker bp):this(bp, Resources.getString("BoardPicker.select_board"))
		{ //$NON-NLS-1$
		}
		
		public BoardSlot(BoardPicker bp, System.String prompt)
		{
			InitBlock();
			this.prompt = prompt;
			picker = bp;
			boards = new System.Windows.Forms.ComboBox();
			boards.Items.Add(prompt);
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'lbn '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.String[] lbn = picker.getAllowableLocalizedBoardNames();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			for(String s: lbn)
			{
				boards.Items.Add(s);
			}
			boards.SelectedIndex = lbn.Length == 1?1:0;
			boards.SelectedValueChanged += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(boards);
			
			reverseCheckBox = SupportClass.CheckBoxSupport.CreateCheckBox(Resources.getString("BoardPicker.flip")); //$NON-NLS-1$
			reverseCheckBox.CheckedChanged += new System.EventHandler(new AnonymousClassItemListener(this).itemStateChanged);
			
			reverseCheckBox.Visible = false;
			
			//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.OverlayLayout.OverlayLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingOverlayLayout'"
			setLayout(new OverlayLayout(this));
			
			//UPGRADE_NOTE: Final was removed from the declaration of 'p '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Panel p = new System.Windows.Forms.Panel();
			//UPGRADE_NOTE: Final was removed from the declaration of 'b '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box b = Box.createHorizontalBox();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			b.Controls.Add(boards);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			b.Controls.Add(reverseCheckBox);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			p.Controls.Add(b);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setOpaque' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetOpaque_boolean'"
			p.setOpaque(false);
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentX' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentX_float'"
			p.setAlignmentX(0.5F);
			//UPGRADE_NOTE: Final was removed from the declaration of 'l '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
			System.Windows.Forms.Label temp_label;
			temp_label = new System.Windows.Forms.Label();
			temp_label.Image = this;
			System.Windows.Forms.Label l = temp_label;
			//UPGRADE_ISSUE: Method 'javax.swing.JComponent.setAlignmentX' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJComponentsetAlignmentX_float'"
			l.setAlignmentX(0.5F);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(p);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(l);
			
			actionPerformed(null);
		}
		
		public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			if (prompt.Equals(boards.SelectedItem))
			{
				Board = null;
			}
			else
			{
				System.String selectedBoard = (System.String) boards.SelectedItem;
				if (selectedBoard != null)
				{
					Board b = picker.getLocalizedBoard(selectedBoard);
					if (picker.getBoardsFromControls().contains(b))
					{
						b = b.copy();
					}
					Board = b;
				}
			}
		}
		
		//UPGRADE_NOTE: The equivalent of method 'javax.swing.Icon.paintIcon' is not an override method. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1143'"
		public virtual void  paintIcon(System.Windows.Forms.Control c, System.Drawing.Graphics g, int x, int y)
		{
			if (board != null)
			{
				board.draw(g, x, y, picker.SlotScale, c);
			}
			else
			{
				g.FillRegion(new System.Drawing.SolidBrush(SupportClass.GraphicsManager.manager.GetBackColor(g)), new System.Drawing.Region(new System.Drawing.Rectangle(x, y, Width, Height)));
			}
		}
	}
}