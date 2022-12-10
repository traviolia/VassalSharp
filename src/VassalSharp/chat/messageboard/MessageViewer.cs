/*
* $Id$
*
* Copyright (c) 2000-2007 by Rodney Kinney
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
using Resources = VassalSharp.i18n.Resources;
namespace VassalSharp.chat.messageboard
{
	
	[Serializable]
	public class MessageViewer:System.Windows.Forms.Panel
	{
		private void  InitBlock()
		{
			msgList.clear();
			//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
			msgText.Text = ""; //$NON-NLS-1$
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Vector < Vector < String >> rows = new Vector < Vector < String >>();
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			Vector < String > names = new Vector < String >();
			names.addElement(Resources.getString("Chat.sender")); //$NON-NLS-1$
			names.addElement(Resources.getString("Chat.date")); //$NON-NLS-1$
			while (msgEnum.hasMoreElements())
			{
				Message msg = msgEnum.nextElement();
				msgList.add(msg);
				//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
				Vector < String > cols = new Vector < String >();
				cols.addElement(msg.Sender);
				System.DateTime tempAux = msg.Date;
				//UPGRADE_NOTE: ref keyword was added to struct-type parameters. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1303'"
				cols.addElement(Resources.formatDate(ref tempAux));
				rows.addElement(cols);
			}
			model = new DefaultTableModel(rows, names);
			msgTable.DataSource = model;
			if (!msgList.isEmpty())
			{
				//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionModel'"
				msgTable.getSelectionModel().SetSelectionInterval(0, 0);
			}
		}
		private const long serialVersionUID = 1L;
		
		//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Windows.Forms.DataGrid msgTable;
		//UPGRADE_TODO: Class 'javax.swing.table.DefaultTableModel' was converted to 'System.Data.DataTable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		private System.Data.DataTable model;
		private System.Windows.Forms.TextBox msgText;
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		private List < Message > msgList = new ArrayList < Message >();
		
		public MessageViewer()
		{
			InitBlock();
			initComponents();
		}
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		void setMessages(Enumeration < Message > msgEnum)
		
		private void  initComponents()
		{
			//UPGRADE_TODO: Class 'javax.swing.JSplitPane' was converted to 'SupportClass.SplitterPanelSupport' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			SupportClass.SplitterPanelSupport split = new SupportClass.SplitterPanelSupport((int) System.Windows.Forms.Orientation.Horizontal);
			
			//UPGRADE_TODO: Constructor 'javax.swing.table.DefaultTableModel.DefaultTableModel' was converted to 'SupportClass.DataTableSupport.CreateDataTable' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtableDefaultTableModelDefaultTableModel_javalangObject[]_int'"
			model = SupportClass.DataTableSupport.CreateDataTable(new System.Object[]{Resources.getString("Chat.sender"), Resources.getString("Chat.date")}, 0); //$NON-NLS-1$ //$NON-NLS-2$
			System.Windows.Forms.DataGrid temp_DataGrid;
			temp_DataGrid = new System.Windows.Forms.DataGrid();
			temp_DataGrid.DataSource = model;
			msgTable = temp_DataGrid;
			//UPGRADE_ISSUE: Method 'javax.swing.ListSelectionModel.addListSelectionListener' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingListSelectionModeladdListSelectionListener_javaxswingeventListSelectionListener'"
			//UPGRADE_ISSUE: Method 'javax.swing.JTable.getSelectionModel' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablegetSelectionModel'"
			msgTable.getSelectionModel().addListSelectionListener(new ShowMsgText(this));
			//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol;
			temp_scrollablecontrol = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol.AutoScroll = true;
			temp_scrollablecontrol.Controls.Add(msgTable);
			System.Windows.Forms.ScrollableControl scroll = temp_scrollablecontrol;
			split.Add(scroll);
			
			System.Windows.Forms.TextBox temp_TextBox;
			temp_TextBox = new System.Windows.Forms.TextBox();
			temp_TextBox.Multiline = true;
			temp_TextBox.WordWrap = false;
			temp_TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			msgText = temp_TextBox;
			msgText.WordWrap = true;
			msgText.WordWrap = true;
			msgText.ReadOnly = !false;
			//UPGRADE_TODO: Constructor 'javax.swing.JScrollPane.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJScrollPaneJScrollPane_javaawtComponent'"
			System.Windows.Forms.ScrollableControl temp_scrollablecontrol2;
			temp_scrollablecontrol2 = new System.Windows.Forms.ScrollableControl();
			temp_scrollablecontrol2.AutoScroll = true;
			temp_scrollablecontrol2.Controls.Add(msgText);
			scroll = temp_scrollablecontrol2;
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			//UPGRADE_ISSUE: Constructor 'javax.swing.border.TitledBorder.TitledBorder' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingborderTitledBorder'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(scroll.CreateGraphics(), 0, 0, scroll.Width, scroll.Height, new TitledBorder(Resources.getString("Chat.message"))); //$NON-NLS-1$
			split.Add(scroll);
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			Controls.Add(split);
		}
		
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'ShowMsgText' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class ShowMsgText
		{
			public ShowMsgText(MessageViewer enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(MessageViewer enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private MessageViewer enclosingInstance;
			public MessageViewer Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public virtual void  valueChanged(System.Object event_sender, System.EventArgs evt)
			{
				int index = Enclosing_Instance.msgTable.CurrentRowIndex;
				if (index >= 0 && index < msgList.size())
				{
					Enclosing_Instance.msgText.setText(msgList.get_Renamed(index).getText());
				}
				else
				{
					//UPGRADE_TODO: Method 'javax.swing.text.JTextComponent.setText' was converted to 'System.Windows.Forms.TextBoxBase.Text' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingtextJTextComponentsetText_javalangString'"
					Enclosing_Instance.msgText.Text = ""; //$NON-NLS-1$
				}
			}
		}
	}
}