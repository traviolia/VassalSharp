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
using GameModule = VassalSharp.build.GameModule;
using HelpFile = VassalSharp.build.module.documentation.HelpFile;
using ChangePiece = VassalSharp.command.ChangePiece;
using Command = VassalSharp.command.Command;
using IntConfigurer = VassalSharp.configure.IntConfigurer;
using NamedHotKeyConfigurer = VassalSharp.configure.NamedHotKeyConfigurer;
using StringConfigurer = VassalSharp.configure.StringConfigurer;
using PieceI18nData = VassalSharp.i18n.PieceI18nData;
using TranslatablePiece = VassalSharp.i18n.TranslatablePiece;
using NamedKeyStroke = VassalSharp.tools.NamedKeyStroke;
using ScrollPane = VassalSharp.tools.ScrollPane;
using SequenceEncoder = VassalSharp.tools.SequenceEncoder;
namespace VassalSharp.counters
{
	
	/// <summary> A Decorator class that endows a GamePiece with an editable
	/// spreadsheet (i.e. JTable) 
	/// </summary>
	public class TableInfo:Decorator, TranslatablePiece
	{
		//UPGRADE_NOTE: Field 'EnclosingInstance' was added to class 'AnonymousClassWindowAdapter' to access its enclosing instance. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1019'"
		private class AnonymousClassWindowAdapter
		{
			public AnonymousClassWindowAdapter(TableInfo enclosingInstance)
			{
				InitBlock(enclosingInstance);
			}
			private void  InitBlock(TableInfo enclosingInstance)
			{
				this.enclosingInstance = enclosingInstance;
			}
			private TableInfo enclosingInstance;
			public TableInfo Enclosing_Instance
			{
				get
				{
					return enclosingInstance;
				}
				
			}
			public void  windowClosing(System.Object event_sender, System.ComponentModel.CancelEventArgs evt)
			{
				evt.Cancel = true;
				//UPGRADE_ISSUE: Method 'javax.swing.JTable.editingStopped' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTableeditingStopped_javaxswingeventChangeEvent'"
				Enclosing_Instance.table.editingStopped(null);
				GamePiece outer = Decorator.getOutermost(Enclosing_Instance);
				if (outer.Id != null)
				{
					GameModule.getGameModule().sendAndLog(new ChangePiece(outer.Id, Enclosing_Instance.oldState, outer.State));
				}
			}
		}
		virtual public int RowCount
		{
			get
			{
				return nRows;
			}
			
		}
		virtual public int ColumnCount
		{
			get
			{
				return nCols;
			}
			
		}
		//UPGRADE_TODO: Interface 'java.awt.Shape' was converted to 'System.Drawing.Drawing2D.GraphicsPath' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		override public System.Drawing.Drawing2D.GraphicsPath Shape
		{
			get
			{
				return piece.Shape;
			}
			
		}
		/// <param name="val">a comma-separated list of table values
		/// </param>
		private System.String Values
		{
			set
			{
				SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(value, ',');
				
				for (int row = 0; row < nRows; ++row)
				{
					for (int col = 0; col < nCols; ++col)
					{
						((System.Data.DataTable) table.DataSource).Rows[row][col] = st.nextToken();
					}
				}
			}
			
		}
		virtual public System.String Description
		{
			get
			{
				return "Spreadsheet";
			}
			
		}
		virtual public VassalSharp.build.module.documentation.HelpFile HelpFile
		{
			get
			{
				return HelpFile.getReferenceManualPage("Spreadsheet.htm");
			}
			
		}
		public const System.String ID = "table;";
		
		protected internal System.String values;
		protected internal System.String oldState;
		protected internal int nRows, nCols;
		protected internal System.String command;
		protected internal NamedKeyStroke launchKey;
		protected internal KeyCommand launch;
		//UPGRADE_TODO: Class 'javax.swing.JTable' was converted to 'System.Windows.Forms.DataGrid' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
		protected internal System.Windows.Forms.DataGrid table;
		protected internal System.Windows.Forms.Form frame;
		
		public TableInfo():this(ID + "2;2;Show Data;S", null)
		{
		}
		
		public TableInfo(System.String type, GamePiece p)
		{
			mySetType(type);
			setInner(p);
		}
		
		public virtual void  mySetType(System.String s)
		{
			s = s.Substring(ID.Length);
			SequenceEncoder.Decoder st = new SequenceEncoder.Decoder(s, ';');
			nRows = st.nextInt(2);
			nCols = st.nextInt(2);
			command = st.nextToken();
			launchKey = st.nextNamedKeyStroke(null);
			frame = null;
			table = null;
		}
		
		public virtual void  draw(System.Drawing.Graphics g, int x, int y, System.Windows.Forms.Control obs, double zoom)
		{
			piece.draw(g, x, y, obs, zoom);
		}
		
		public override System.String getName()
		{
			return piece.getName();
		}
		
		public override System.Drawing.Rectangle boundingBox()
		{
			return piece.boundingBox();
		}
		
		public override System.String myGetState()
		{
			if (table == null)
			{
				return values;
			}
			else
			{
				SequenceEncoder se = new SequenceEncoder(',');
				for (int row = 0; row < nRows; ++row)
				{
					for (int col = 0; col < nCols; ++col)
					{
						System.String s = (System.String) ((System.Data.DataTable) table.DataSource).Rows[row][col];
						se.append(s == null?"":s);
					}
				}
				return se.Value;
			}
		}
		
		public override void  mySetState(System.String state)
		{
			if (table == null)
			{
				values = state;
			}
			else
			{
				Values = state;
			}
		}
		
		public override System.String myGetType()
		{
			SequenceEncoder se = new SequenceEncoder(';');
			se.append(nRows).append(nCols).append(command).append(launchKey);
			return ID + se.Value;
		}
		
		//UPGRADE_NOTE: Access modifiers of method 'myGetKeyCommands' were changed to 'public'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1204'"
		public override KeyCommand[] myGetKeyCommands()
		{
			if (launch == null)
			{
				launch = new KeyCommand(command, launchKey, Decorator.getOutermost(this), this);
			}
			return new KeyCommand[]{launch};
		}
		
		public override Command myKeyEvent(System.Windows.Forms.KeyEventArgs stroke)
		{
			myGetKeyCommands();
			if (launch.matches(stroke))
			{
				if (frame == null)
				{
					//UPGRADE_TODO: Constructor 'javax.swing.JDialog.JDialog' was converted to 'SupportClass.DialogSupport.CreateDialog' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJDialogJDialog_javaawtFrame_boolean'"
					//UPGRADE_TODO: Class 'java.awt.Frame' was converted to 'System.Windows.Forms.Form' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtFrame'"
					frame = SupportClass.DialogSupport.CreateDialog((System.Windows.Forms.Form) null);
					table = SupportClass.DataGridSupport.CreateDataGrid(nRows, nCols);
					Values = values;
					//UPGRADE_ISSUE: Method 'javax.swing.JTable.setTableHeader' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJTablesetTableHeader_javaxswingtableJTableHeader'"
					table.setTableHeader(null);
					//UPGRADE_TODO: Class 'javax.swing.JScrollPane' was converted to 'System.Windows.Forms.ScrollableControl' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					System.Windows.Forms.ScrollableControl scroll = new ScrollPane(table);
					//UPGRADE_TODO: Method 'javax.swing.JComponent.setPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					//UPGRADE_ISSUE: Method 'javax.swing.JScrollPane.getViewport' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingJScrollPanegetViewport'"
					//UPGRADE_TODO: Method 'javax.swing.JComponent.getPreferredSize' was converted to 'System.Windows.Forms.Control.Size' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073'"
					scroll.getViewport().Size = table.Size;
					//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
					frame.Controls.Add(scroll);
					System.Drawing.Point p = GameModule.getGameModule().getFrame().getLocation();
					if (getMap() != null)
					{
						p = getMap().getView().getLocationOnScreen();
						System.Drawing.Point p2 = getMap().componentCoordinates(Position);
						p.Offset(p2.X, p2.Y);
					}
					//UPGRADE_TODO: Method 'java.awt.Component.setLocation' was converted to 'System.Windows.Forms.Control.Location' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetLocation_int_int'"
					frame.Location = new System.Drawing.Point(p.X, p.Y);
					//UPGRADE_NOTE: Some methods of the 'java.awt.event.WindowListener' class are not used in the .NET Framework. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1308'"
					frame.Closing += new System.ComponentModel.CancelEventHandler(new AnonymousClassWindowAdapter(this).windowClosing);
					//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
					frame.pack();
				}
				frame.Text = getName();
				oldState = Decorator.getOutermost(this).State;
				//UPGRADE_TODO: Method 'java.awt.Component.setVisible' was converted to 'System.Windows.Forms.Control.Visible' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtComponentsetVisible_boolean'"
				//UPGRADE_TODO: 'System.Windows.Forms.Application.Run' must be called to start a main form. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1135'"
				//UPGRADE_NOTE: Since the declaration of the following entity is not virtual in .NET the modifier new was added. References to it may have been changed to InvokeMethodAsVirtual, GetPropertyAsVirtual or SetPropertyAsVirtual. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1195'"
				SupportClass.SetPropertyAsVirtual(frame, "Visible", true);
				return null;
			}
			else
			{
				return null;
			}
		}
		
		public override PieceEditor getEditor()
		{
			return new Ed(this);
		}
		
		public override PieceI18nData getI18nData()
		{
			return getI18nData(command, "Table Info command");
		}
		
		private class Ed : PieceEditor
		{
			virtual public System.Windows.Forms.Control Controls
			{
				get
				{
					return panel;
				}
				
			}
			virtual public System.String Type
			{
				get
				{
					SequenceEncoder se = new SequenceEncoder(';');
					se.append(rowConfig.ValueString).append(colConfig.ValueString).append(commandConfig.ValueString).append(keyConfig.ValueString);
					return VassalSharp.counters.TableInfo.ID + se.Value;
				}
				
			}
			virtual public System.String State
			{
				get
				{
					//UPGRADE_NOTE: Final was removed from the declaration of 'buf '. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1003'"
					StringBuilder buf = new StringBuilder();
					int n = ((System.Int32) rowConfig.getValue()) * ((System.Int32) colConfig.getValue());
					while (--n > 0)
					{
						buf.append(',');
					}
					return buf.toString();
				}
				
			}
			private IntConfigurer rowConfig = new IntConfigurer(null, "Number of rows:  ");
			private IntConfigurer colConfig = new IntConfigurer(null, "Number of columns:  ");
			private StringConfigurer commandConfig = new StringConfigurer(null, "Menu Command:  ");
			private NamedHotKeyConfigurer keyConfig;
			private System.Windows.Forms.Panel panel;
			
			public Ed(TableInfo p)
			{
				rowConfig.setValue(p.nRows);
				colConfig.setValue(p.nCols);
				commandConfig.setValue(p.command);
				keyConfig = new NamedHotKeyConfigurer(null, "Keyboard Command:  ", p.launchKey);
				
				panel = new System.Windows.Forms.Panel();
				//UPGRADE_ISSUE: Method 'java.awt.Container.setLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtContainersetLayout_javaawtLayoutManager'"
				//UPGRADE_ISSUE: Constructor 'javax.swing.BoxLayout.BoxLayout' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				//UPGRADE_ISSUE: Field 'javax.swing.BoxLayout.Y_AXIS' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBoxLayout'"
				panel.setLayout(new BoxLayout(panel, BoxLayout.Y_AXIS));
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(commandConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(keyConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(rowConfig.Controls);
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				panel.Controls.Add(colConfig.Controls);
			}
		}
	}
}