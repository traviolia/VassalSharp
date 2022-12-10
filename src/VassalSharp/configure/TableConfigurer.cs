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
namespace VassalSharp.configure
{
	
	
	/// <author>  Brent Easton
	/// 
	/// TODO To change the template for this generated type comment go to
	/// Window - Preferences - Java - Code Style - Code Templates
	/// </author>
	public class TableConfigurer:Configurer
	{
		private void  InitBlock()
		{
			columns = new Column[0];
			this(key, name);
			columns = new Column[headings.length];
			for (int i = 0; i < headings.length; i++)
			{
				columns[i] = new Column(headings[i], types[i]);
			}
		}
		override public System.String ValueString
		{
			get
			{
				return null;
			}
			
		}
		virtual public char RowDelimiter
		{
			set
			{
				rowDelimiter = value;
			}
			
		}
		virtual public char ColDelimiter
		{
			set
			{
				colDelimiter = value;
			}
			
		}
		virtual public int ColumnCount
		{
			get
			{
				return columns.Length;
			}
			
		}
		override public System.Windows.Forms.Control Controls
		{
			get
			{
				if (controls == null)
				{
					buildControls();
				}
				return controls;
			}
			
		}
		
		public const System.String ADD_ACTION = "Add";
		public const System.String DEL_ACTION = "Remove";
		public const System.String INS_ACTION = "Insert";
		
		//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
		protected internal Box controls;
		protected internal System.Windows.Forms.Panel contents = null;
		
		//UPGRADE_NOTE: The initialization of  'columns' was moved to method 'InitBlock'. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1005'"
		protected internal Column[] columns;
		
		protected internal char rowDelimiter = ',';
		protected internal char colDelimiter = '|';
		
		//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
		public TableConfigurer(String key, String name, String [] headings, Class < ? > [] types)
		
		public TableConfigurer(System.String key, System.String name):base(key, name)
		{
			InitBlock();
		}
		
		public override void  setValue(System.String s)
		{
			
		}
		
		protected internal virtual void  buildControls()
		{
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createVerticalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			controls = Box.createVerticalBox();
			controls.setBorder(System.Windows.Forms.Border3DStyle.Etched);
			System.Windows.Forms.Label temp_label2;
			temp_label2 = new System.Windows.Forms.Label();
			temp_label2.Text = getName();
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			System.Windows.Forms.Control temp_Control;
			temp_Control = temp_label2;
			controls.Controls.Add(temp_Control);
			
			updateContents();
			
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			//UPGRADE_ISSUE: Method 'javax.swing.Box.createHorizontalBox' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			Box buttonBox = Box.createHorizontalBox();
			System.Windows.Forms.Button addButton = SupportClass.ButtonSupport.CreateStandardButton(ADD_ACTION);
			addButton.Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(addButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonBox.Controls.Add(addButton);
			System.Windows.Forms.Button delButton = SupportClass.ButtonSupport.CreateStandardButton(DEL_ACTION);
			delButton.Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(delButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonBox.Controls.Add(delButton);
			System.Windows.Forms.Button insButton = SupportClass.ButtonSupport.CreateStandardButton(INS_ACTION);
			insButton.Click += new System.EventHandler(this.actionPerformed);
			SupportClass.CommandManager.CheckCommand(insButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			buttonBox.Controls.Add(insButton);
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			controls.Controls.Add(buttonBox);
			repack();
		}
		
		protected internal virtual void  repack()
		{
			//UPGRADE_TODO: Method 'javax.swing.SwingUtilities.getWindowAncestor' was converted to 'System.Windows.Forms.Control.Parent' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingSwingUtilitiesgetWindowAncestor_javaawtComponent'"
			System.Windows.Forms.Form w = (System.Windows.Forms.Form) controls.Parent;
			if (w != null)
			{
				//UPGRADE_ISSUE: Method 'java.awt.Window.pack' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaawtWindowpack'"
				w.pack();
			}
		}
		
		protected internal virtual void  updateContents()
		{
			if (contents != null)
			{
				controls.Controls.Remove(contents);
				contents = null;
			}
			contents = new System.Windows.Forms.Panel();
			//UPGRADE_TODO: Method 'javax.swing.JComponent.setBorder' was converted to 'System.Windows.Forms.ControlPaint.DrawBorder3D' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaxswingJComponentsetBorder_javaxswingborderBorder'"
			System.Windows.Forms.ControlPaint.DrawBorder3D(contents.CreateGraphics(), 0, 0, contents.Width, contents.Height, System.Windows.Forms.Border3DStyle.Etched);
			//UPGRADE_TODO: Constructor 'java.awt.GridLayout.GridLayout' was converted to 'System.Drawing.Rectangle.Rectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGridLayoutGridLayout_int_int'"
			//UPGRADE_TODO: Class 'java.awt.GridLayout' was converted to 'System.Drawing.Rectangle' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtGridLayout'"
			contents.Tag = new System.Drawing.Rectangle(0, 5, 0, 0);
			contents.Layout += new System.Windows.Forms.LayoutEventHandler(SupportClass.GridLayoutResize);
			
			for (int i = 0; i < columns.Length; i++)
			{
				System.Windows.Forms.Label temp_label2;
				temp_label2 = new System.Windows.Forms.Label();
				temp_label2.Text = columns[i].Name;
				//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
				System.Windows.Forms.Control temp_Control;
				temp_Control = temp_label2;
				contents.Controls.Add(temp_Control);
			}
			
			if (columns[0].RowCount > 0)
			{
				for (int row = 0; row < columns[0].RowCount; row++)
				{
					for (int col = 0; col < columns.Length; col++)
					{
						//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
						System.Windows.Forms.Control temp_Control2;
						temp_Control2 = columns[col].getControls(row);
						contents.Controls.Add(temp_Control2);
					}
				}
			}
			
			//UPGRADE_TODO: Method 'java.awt.Container.add' was converted to 'System.Windows.Forms.ContainerControl.Controls.Add' which has a different behavior. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1073_javaawtContaineradd_javaawtComponent'"
			controls.Controls.Add(contents);
			repack();
		}
		
		public virtual void  actionPerformed(System.Object event_sender, System.EventArgs e)
		{
			System.String action = SupportClass.CommandManager.GetCommand(event_sender);
			
			if (action.Equals(ADD_ACTION))
			{
				for (int i = 0; i < ColumnCount; i++)
				{
					columns[i].addRow();
				}
				updateContents();
			}
			else if (action.Equals(DEL_ACTION))
			{
				
			}
			else if (action.Equals(INS_ACTION))
			{
				
			}
		}
		
		protected internal class Column
		{
			public Column()
			{
				InitBlock();
			}
			private void  InitBlock()
			{
				this.name = name;
				this.type = type;
			}
			virtual public System.String Name
			{
				get
				{
					return name;
				}
				
			}
			virtual public int RowCount
			{
				get
				{
					return configurers.size();
				}
				
			}
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			protected Class < ? > type;
			protected internal System.String name;
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			protected List < Configurer > configurers = new ArrayList < Configurer >();
			//UPGRADE_ISSUE: Class 'javax.swing.Box' was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1000_javaxswingBox'"
			protected internal Box controls;
			protected internal System.Windows.Forms.Panel contents;
			
			//UPGRADE_ISSUE: The following fragment of code could not be parsed and was not converted. "ms-help://MS.VSCC.v80/dv_commoner/local/redirect.htm?index='!DefaultContextWindowIndex'&keyword='jlca1156'"
			public Column(String name, Class < ? > type)
			
			public virtual System.Windows.Forms.Control getControls(int row)
			{
				if (row >= 0 && row < RowCount)
				{
					return configurers.get_Renamed(row).getControls();
				}
				return null;
			}
			
			public virtual void  addRow()
			{
				Configurer c = AutoConfigurer.createConfigurer(type, null, "", null);
				configurers.add(c);
			}
		}
	}
}